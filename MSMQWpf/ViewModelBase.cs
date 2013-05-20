﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSMQWpf
{
    using System.ComponentModel;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public virtual void NotifyPropertyChanged(string propertyName)
        {
            //  Store the event handler - in case it changes between
            //  the line to check it and the line to fire it.
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            //  If the event has been subscribed to, fire it.
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
