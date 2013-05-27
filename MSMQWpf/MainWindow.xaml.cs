using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;


namespace MSMQWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //txtBoxName.Text = ConfigurationManager.AppSettings["Machine"];
            //txtQueueName.Text = ConfigurationManager.AppSettings["QueueName"];

            //var test = MessageQueue.GetPrivateQueuesByMachine(".");
            ////var smt = new CollectionTest();
            DataContext = new MainWindowViewModel();
        }



        private void btnLoadProperties_Click(object sender, RoutedEventArgs e)
        {
            //var type = (Type)cboMessageList.SelectedItem;

            //theGrid.BindToList(type);
        }

        private void btnSendToQueue_Click(object sender, RoutedEventArgs e)
        {

            //var boxName = txtBoxName.Text.Trim();
            //var queueName = txtQueueName.Text.Trim();

            //var queue = new Queue(boxName, queueName);

            //XDocument doc = XDocument.Parse("<test>this is test</test>");
            //queue.SendObject(doc.Root);
            //foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
        }

        private void btnToXML_Click(object sender, RoutedEventArgs e)
        {
            dynamic theObject = theGrid.DataContext;

            var detail = Queue.SerializeTheMessage(theObject.m_model);

            //txtXMLResult.Text = detail.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
