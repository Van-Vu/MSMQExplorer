using System;
using System.Collections.Generic;
using System.Messaging;
using System.Windows;
using MSMQTestMessage;


namespace MSMQWpf
{
    using System.Configuration;
    using System.Xml.Linq;

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
            var boxName = txtBoxName.Text.Trim();
            var queueName = txtQueueName.Text.Trim();

            var queue = new Queue(boxName, queueName);

            XDocument doc = XDocument.Parse("<test>this is test</test>");
            queue.SendObject(doc.Root);
        }

        private void btnToXML_Click(object sender, RoutedEventArgs e)
        {
            dynamic theObject = theGrid.DataContext;

            var detail = Queue.SerializeTheMessage(theObject.m_model);

            txtXMLResult.Text = detail.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
