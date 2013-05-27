using System;
using System.Collections.Generic;
using System.Linq;
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
            var detail = Queue.SerializeTheMessage(SelectedMessage);
            //ResultXml = detail.ToString();
            //ResultXml = "<Section xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xml:space=\"preserve\" TextAlignment=\"Left\" LineHeight=\"Auto\" IsHyphenationEnabled=\"False\" xml:lang=\"en-us\" FlowDirection=\"LeftToRight\" NumberSubstitution.CultureSource=\"User\" NumberSubstitution.Substitution=\"AsCulture\" FontFamily=\"Segoe UI\" FontStyle=\"Normal\" FontWeight=\"Normal\" FontStretch=\"Normal\" FontSize=\"12\" Foreground=\"#FF000000\" Typography.StandardLigatures=\"True\" Typography.ContextualLigatures=\"True\" Typography.DiscretionaryLigatures=\"False\" Typography.HistoricalLigatures=\"False\" Typography.AnnotationAlternates=\"0\" Typography.ContextualAlternates=\"True\" Typography.HistoricalForms=\"False\" Typography.Kerning=\"True\" Typography.CapitalSpacing=\"False\" Typography.CaseSensitiveForms=\"False\" Typography.StylisticSet1=\"False\" Typography.StylisticSet2=\"False\" Typography.StylisticSet3=\"False\" Typography.StylisticSet4=\"False\" Typography.StylisticSet5=\"False\" Typography.StylisticSet6=\"False\" Typography.StylisticSet7=\"False\" Typography.StylisticSet8=\"False\" Typography.StylisticSet9=\"False\" Typography.StylisticSet10=\"False\" Typography.StylisticSet11=\"False\" Typography.StylisticSet12=\"False\" Typography.StylisticSet13=\"False\" Typography.StylisticSet14=\"False\" Typography.StylisticSet15=\"False\" Typography.StylisticSet16=\"False\" Typography.StylisticSet17=\"False\" Typography.StylisticSet18=\"False\" Typography.StylisticSet19=\"False\" Typography.StylisticSet20=\"False\" Typography.Fraction=\"Normal\" Typography.SlashedZero=\"False\" Typography.MathematicalGreek=\"False\" Typography.EastAsianExpertForms=\"False\" Typography.Variants=\"Normal\" Typography.Capitals=\"Normal\" Typography.NumeralStyle=\"Normal\" Typography.NumeralAlignment=\"Normal\" Typography.EastAsianWidths=\"Normal\" Typography.EastAsianLanguage=\"Normal\" Typography.StandardSwashes=\"0\" Typography.ContextualSwashes=\"0\" Typography.StylisticAlternates=\"0\"><Paragraph><Run>This is the </Run><Run FontWeight=\"Bold\">RichTextBox</Run></Paragraph></Section>";
            //Document = new TextDocument(detail.ToString());
            //Document.Text = ;
            Message = detail.ToString();
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
