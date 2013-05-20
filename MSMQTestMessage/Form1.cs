using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MSMQTestMessage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var factory = new Factory();
            var messageList = factory.GetMessageList(textBox1.Text);
            cboMessageList.DataSource = messageList;
        }

        private void btnLoadMessage_Click(object sender, EventArgs e)
        {
            var factory = new Factory();
           var messageList = factory.GetMessageProperties((Type)cboMessageList.SelectedItem);
            theGrid.DataSource = messageList;
            //theGrid.

        }
    }
}
