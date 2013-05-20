using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSMQWpf
{
    using System.ComponentModel;

    public class PropertyValues: INotifyPropertyChanged
    {
        private string _Value;
        private string _Name;

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                OnPropertyChanged("Value");
            }
        }

        public string Name { get; set; }

        public void OnPropertyChanged (string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
