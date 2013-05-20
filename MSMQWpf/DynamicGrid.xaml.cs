using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DynamicViewModel;

namespace MSMQWpf
{
    using System.ComponentModel;
    using System.Dynamic;

    using MSMQTestMessage;

    /// <summary>
    /// Interaction logic for DynamicGrid.xaml
    /// </summary>
    public partial class DynamicGrid : UserControl
    {

        public DynamicGrid()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty CurrentObjectProperty = DependencyProperty.Register("CurrentObject", typeof(Type), typeof(DynamicGrid), new PropertyMetadata(null));

        public Type CurrentObject
        {
            get { return GetValue(CurrentObjectProperty).GetType(); }
            set
            {
                SetValue(CurrentObjectProperty, value);
                GenerateDynamicViewModel(value);
            }
        }

        public void GenerateDynamicViewModel(Type type)
        {
            if (type != null)
            {
                Type genericType = typeof(DynamicViewModel<>);
                Type finalType = genericType.MakeGenericType(new Type[1] { type });

                var theMessage = Activator.CreateInstance(type);

                dynamic viewModel = Activator.CreateInstance(finalType, theMessage);

                var fact = new ReflectionHelper();
                var theList = fact.GetMessageProperties(type);
                var propertyValue = theList.Select(item => new PropertyValues { Name = item }).ToList();

                viewModel.childrens = propertyValue;

                DataContext = viewModel;
            }
        }
    }
}
