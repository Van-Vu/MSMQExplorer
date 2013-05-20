// http://www.codeproject.com/Articles/139630/MVVM-using-POCOs-with-NET-4-0-and-the-DynamicViewM
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace DynamicViewModel
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Windows;

    using MSMQTestMessage;

    using MSMQWpf;

    public sealed class DynamicViewModel<TModel>
        : DynamicObject, INotifyPropertyChanged //where TModel : class
    {
        /// <summary>
        /// Dictionary that holds information about the TModel public
        /// instance methods.
        /// </summary>
        /// <remarks>
        /// CA1810: Initialize reference type static fields inline.
        /// http://msdn.microsoft.com/en-us/library/ms182275(v=VS.100).aspx
        /// </remarks>
        private static readonly IDictionary<String, MethodInfo> s_methodInfos
            = GetPublicInstanceMethods();

        /// <summary>
        /// Dictionary that holds information about the TModel public
        /// instance properties.
        /// </summary>
        /// <remarks>
        /// CA1810: Initialize reference type static fields inline.
        /// http://msdn.microsoft.com/en-us/library/ms182275(v=VS.100).aspx
        /// </remarks>
        private static readonly IDictionary<String, PropertyInfo> s_propInfos
            = GetPublicInstanceProperties();

        public readonly TModel m_model;

        /// <summary>
        /// Dictionary that holds information about the current 
        /// values of the TModel public instance properties.
        /// </summary>
        private IDictionary<String, Object> m_propertyValues;

        public ObservableCollection<PropertyValues> childrens { get; set; }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DynamicViewModel&lt;TModel&gt;"/> class without parameter to do serialization.
        /// </summary>
        public DynamicViewModel()
        {
            NotifyChangedProperties();
        }
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DynamicViewModel&lt;TModel&gt;"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public DynamicViewModel(TModel model)
        {
            m_model = model;

            PropertyChanged = Child_PropertyChanged;

            //var childrens = new ObservableCollection<PropertyValues>();
            //childrens.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e)
            //{
            //    // Subscribe event
            //    switch (e.Action)
            //    {
            //        case NotifyCollectionChangedAction.Add:
            //            // Subscribe
            //            foreach (INotifyPropertyChanged propertyChanged in e.NewItems)
            //            {
            //                propertyChanged.PropertyChanged += PropertyChanged;
            //            }
            //            break;

            //        case NotifyCollectionChangedAction.Remove:
            //            // Unsubscribe
            //            foreach (INotifyPropertyChanged propertyChanged in e.OldItems)
            //            {
            //                propertyChanged.PropertyChanged -= PropertyChanged;
            //            }
            //            break;
            //    }
            //};

            NotifyChangedProperties();
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DynamicViewModel&lt;TModel&gt;"/> class.
        /// </summary>
        /// <param name="delegate">The @delegate.</param>
        public DynamicViewModel(Func<TModel> @delegate)
            : this(@delegate.Invoke()) { }

        /// <summary>
        /// Provides the implementation for operations that invoke a member.
        /// </summary>
        /// <param name="binder">Provides information about the dynamicoperation.
        /// </param>
        /// <param name="args">The arguments that are passed to the 
        /// object member during the invoke operation.</param>
        /// <param name="result">The result of the member invocation.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false.
        /// </returns>
        public override Boolean TryInvokeMember(InvokeMemberBinder binder,
            Object[] args, out Object result)
        {
            result = null;

            MethodInfo methodInfo;
            if (!s_methodInfos.TryGetValue(binder.Name,
                out methodInfo)) { return false; }

            methodInfo.Invoke(m_model, args);
            NotifyChangedProperties();
            return true;
        }

        /// <summary>
        /// Gets the property value of the member.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <param name="result">The result of the get operation. 
        /// For example, if the method is called for a property,
        /// you can assign the property value to 
        /// <paramref name="result"/>.</param>
        /// <returns>True with the result is set.</returns>
        public override Boolean TryGetMember(GetMemberBinder binder, out Object result)
        {
            var isExtend = s_propInfos.Select(x => x.Key == binder.Name).ToList();
            if (isExtend.Count <= 0)
            {
                result = childrens;
                return true;
            }

            var propertyValues = Interlocked.CompareExchange(
                ref m_propertyValues, GetPropertyValues(), null);

            if (!propertyValues.TryGetValue(binder.Name,
                out result)) { return false; }

            return true;
        }

        /// <summary>
        /// Sets the property value of the member.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <param name="value">The value to set to the member. For example, 
        /// for sampleObject.SampleProperty = "Test", where sampleObject is 
        /// an instance of the class derived from the 
        /// <see cref="T:System.Dynamic.DynamicObject"/> class, 
        /// the <paramref name="value"/> is "Test".</param>
        /// <returns>True with the result is set.</returns>
        public override Boolean TrySetMember(SetMemberBinder binder, Object value)
        {
            var isExtend = s_propInfos.Where(x => x.Key == binder.Name).ToList();
            if (isExtend.Count <= 0)
            {
                var theList = (List<PropertyValues>)value;
                if (childrens == null)
                {
                    childrens = new ObservableCollection<PropertyValues>();
                }

                theList.ForEach(x =>
                {
                    childrens.Add(x);
                    x.PropertyChanged += PropertyChanged;
                });
                return true;
            }
            PropertyInfo propInfo = s_propInfos[binder.Name];
            propInfo.SetValue(m_model, value, null);

            NotifyChangedProperties();
            return true;
        }

        /// <summary>
        /// Setting a property sometimes results in multiple properties
        /// with changed values too. For ex.: By changing the FirstName
        /// and the LastName the FullName will get updated. This method
        /// compares the m_propertyValues dictionary with the one that
        /// is obtained inside this method body. For each changed prop
        /// the PropertyChanged event is raised, notifying the callers.
        /// </summary>
        public void NotifyChangedProperties()
        {
            Interlocked.CompareExchange(
                ref m_propertyValues, GetPropertyValues(), null);

            // Store the previous values in a field.
            IDictionary<String, Object> previousPropValues
                = m_propertyValues;

            // Store the  current values in a field.
            IDictionary<String, Object> currentPropValues
                = GetPropertyValues();

            // Since we will be raising the PropertyChanged event
            // we want the caller to bind in the current values
            // and not the previous.
            m_propertyValues
                = currentPropValues;

            foreach (KeyValuePair<String, Object> propValue
                in currentPropValues.Except(previousPropValues))
            {
                RaisePropertyChanged(propValue.Key);
            }
        }

        /// <summary>
        /// Gets the public instance methods of the TModel type.
        /// </summary>
        /// <returns>
        /// A dictionary that holds information about TModel public
        /// instance properties.
        /// </returns>
        private static IDictionary<String, MethodInfo> GetPublicInstanceMethods()
        {
            var methodInfoDictionary = new Dictionary<String, MethodInfo>();
            MethodInfo[] methodInfos = typeof(TModel).GetMethods(
                BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo methodInfo in methodInfos)
            {
                if (methodInfo.Name.StartsWith("get_") ||
                    methodInfo.Name.StartsWith("set_")) { continue; }
                methodInfoDictionary.Add(methodInfo.Name, methodInfo);
            }

            return methodInfoDictionary;
        }

        /// <summary>
        /// Gets the public instance properties of the TModel type.
        /// </summary>
        /// <returns>
        /// A dictionary that holds information about TModel public
        /// instance properties.
        /// </returns>
        private static IDictionary<String, PropertyInfo> GetPublicInstanceProperties()
        {
            var propInfoDictionary = new Dictionary<String, PropertyInfo>();
            PropertyInfo[] propInfos = typeof(TModel).GetProperties(
                BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propInfo in propInfos)
            {
                propInfoDictionary.Add(propInfo.Name, propInfo);
            }
            return propInfoDictionary;
        }

        /// <summary>
        /// Gets the property values about the TModel public instance properties.
        /// </summary>
        /// <returns>A dictionary that holds information about the current 
        /// values of the TModel public instance properties.</returns>
        private IDictionary<String, Object> GetPropertyValues()
        {
            var bindingPaths = new Dictionary<String, Object>();
            PropertyInfo[] propInfos = typeof(TModel).GetProperties(
                BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propInfo in propInfos)
            {
                bindingPaths.Add(
                    propInfo.Name,
                    propInfo.GetValue(m_model, null));
            }

            return bindingPaths;
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChanged(String propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> 
        /// instance containing the event data.</param>
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler temp =
                Interlocked.CompareExchange(ref PropertyChanged, null, null);

            if (temp != null)
            {
                temp(this, e);
            }
        }

        private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is PropertyValues)
            {
                var childObject = (PropertyValues)sender;
                var theProperty = s_propInfos.FirstOrDefault(x => x.Key == childObject.Name);
                var propInfo = theProperty.Value;

                if (childObject.Value.Is(propInfo.PropertyType))
                    propInfo.SetValue(m_model, childObject.Value.Parse(propInfo.PropertyType), null);
            }
        }

        /// <summary>
        /// Called when a property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}