using System;
using System.Windows.Forms;
using Beebotte.API.Server.Net;

namespace BeebotteConsumer
{
    public partial class WriteData : Form
    {
        internal Connector bbtConnector;
        internal string channelName;
        internal string resource;
        internal BBTManagement parent;

        public WriteData()
        {
            InitializeComponent();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            bbtConnector.Write(channelName, resource, txtData.Text);
            this.Close();
        }

        private void WriteData_Load(object sender, EventArgs e)
        {

        }
    }
}
