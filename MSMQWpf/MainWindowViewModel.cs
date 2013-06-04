using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using ICSharpCode.AvalonEdit.Document;

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

        private List<Type> _messageList = new List<Type>();
        public List<Type> MessageList
        {
            get
            {
                return _messageList;
            }
            set
            {
                _messageList = value;
                NotifyPropertyChanged("MessageList");
            }
        }

        private Type _selectedType;
        public Type SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                SelectedMessage = GenerateDynamicViewModel(_selectedType);
                NotifyPropertyChanged("SelectedMessage");
            }
        }

        private List<PropertyValues> _selectedMessage;
        public List<PropertyValues> SelectedMessage
        {
            get
            {
                return _selectedMessage;
            }
            set
            {
                _selectedMessage = value;
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
                var propertyValue = theList.Select(name => new PropertyValues { Name = name, Value = string.Empty }).ToList();

                message.AddRange(propertyValue);

                return message;
            }
            return null;
        }

        private TextDocument _document;
        public TextDocument Document
        {
            get { return _document; }
            set { _document = value;
            NotifyPropertyChanged("Document");
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged("Message");
            }
        }

        private string _resultXml;
        public string ResultXml
        {
            get
            {
                return _resultXml;
            }
            set
            {
                _resultXml = value;
                NotifyPropertyChanged("ResultXml");
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
            var transformMessage = new Message {MessageName = SelectedType.FullName, Properties = SelectedMessage};

            var processor = new XslCompiledTransform();
            var serialize = SerializeToXmlReader(transformMessage);

            processor.Load("template.xsl");

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            processor.Transform(serialize, null, sw);

            Message = sw.ToString();
        }

        private XmlReader SerializeToXmlReader(object message)
        {
            var xmlSerializer = new XmlSerializer(message.GetType());

            var stream = new BufferedStream(new MemoryStream());
            xmlSerializer.Serialize(stream, message);
            stream.Position = 0;

            var reader = XmlReader.Create(stream);
            return reader;
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
