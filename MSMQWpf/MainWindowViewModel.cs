using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace MSMQWpf
{
    using MSMQTestMessage;

    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() 
        {
            LoadAssembly();
            LoadPropertiesCommand = new RelayCommand(param => LoadProperties(), param => CanLoadProperTies());
            ConvertToXmlCommand = new RelayCommand(param => ConvertToXml(), param => CanConvertToXml());
        }

        private List<Type> _MessageList = new List<Type>();
        public List<Type> MessageList
        {
            get
            {
                return _MessageList;
            }
            set
            {
                _MessageList = value;
                NotifyPropertyChanged("MessageList");
            }
        }

        private Type _SelectedType;
        public Type SelectedType
        {
            get
            {
                return _SelectedType;
            }
            set
            {
                _SelectedType = value;
                SelectedMessage = GenerateDynamicViewModel(_SelectedType);
                NotifyPropertyChanged("SelectedMessage");
            }
        }

        private List<PropertyValues> _SelectedMessage;
        public List<PropertyValues> SelectedMessage
        {
            get
            {
                return _SelectedMessage;
            }
            set
            {
                _SelectedMessage = value;
                NotifyPropertyChanged("SelectedMessage");
            }
        }

        public List<PropertyValues> GenerateDynamicViewModel(Type type)
        {
            if (type != null)
            {
                var message = new List<PropertyValues>();

                var fact = new ReflectionHelper();
                var theList = fact.GetMessageProperties(type);
                var propertyValue = theList.Select(name => new PropertyValues { Name = name,Value = string.Empty}).ToList();

                message.AddRange(propertyValue);

                return message;
            }
            return null;
        }

        private List<string> _resultXML = new List<string>();
        public List<string> ResultXML
        {
            get
            {
                return _resultXML;
            }
            set
            {
                _resultXML = value;
                NotifyPropertyChanged("ResultXML");
            }
        }

        private List<string> _machineName = new List<string>();
        public List<string> MachineName
        {
            get
            {
                return _machineName;
            }
            set
            {
                _machineName = value;
                NotifyPropertyChanged("MachineName");
            }
        }

        private List<string> _queueName = new List<string>();
        public List<string> QueueName
        {
            get
            {
                return _queueName;
            }
            set
            {
                _queueName = value;
                NotifyPropertyChanged("QueueName");
            }
        }

        public RelayCommand LoadPropertiesCommand { get; set; }
        public RelayCommand ConvertToXmlCommand { get; set; }

        private RelayCommand _ConvertToXML;
        public RelayCommand ConvertToXML
        {
            get
            {
                return _ConvertToXML;
            }
        }

        private RelayCommand _SendToMSMQ;
        public RelayCommand SendToMSMQ
        {
            get
            {
                return _SendToMSMQ;
            }
        }

        private bool CanLoadProperTies()
        {
            if (MessageList.Count > 0) return true; 
            return false;
        }

        private bool CanConvertToXml()
        {
            if (SelectedMessage != null) return true;
            return false;
        }
        
        private void LoadProperties()
        {
            //theGrid.BindToList(type);
            var test = "asdfa";
        }

        private void ConvertToXml()
        {
            var detail = Queue.SerializeTheMessage(SelectedMessage);
        }

        private void LoadAssembly()
        {
            var factory = new ReflectionHelper();
            //var messageList = factory.GetMessageList(txtText.Text);
            var messageList = factory.GetMessageList("Messages.dll");
            MessageList = messageList;
        }

    }
}
