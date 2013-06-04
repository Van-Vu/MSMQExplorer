using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSMQWpf
{
    public class Message
    {
        public string MessageName { get; set; }
        public List<PropertyValues> Properties { get; set; }
    }
}
