
using System;

namespace MSMQWpf
{
    using System.Messaging;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public class Queue
    {
        private string msmqConfigString;

        public Queue(string machineName, string queueName)
        {
            msmqConfigString = string.Format("FormatName:Direct=OS:{0}\\private$\\{1}", machineName, queueName);
        }

        public void SendObject(object message)
        {
            using (var messageQueue = new MessageQueue(msmqConfigString))
            {
                var detail = SerializeTheMessage(message);
                messageQueue.Send(detail, "From Test MSMQ tool", MessageQueueTransactionType.Single);
            }

        }

        public static XElement SerializeTheMessage(object data)
        {
            var serializer = new XmlSerializer(data.GetType());
            serializer.Serialize(Console.Out,data);
            Console.WriteLine();
            Console.ReadLine();
            var doc = new XDocument();

            using (XmlWriter xw = doc.CreateWriter())
            {
                serializer.Serialize(xw, data);
                xw.Close();
            }

            return doc.Root;
        }
    }
}
