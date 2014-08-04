using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Beebotte.API.Server.Net;

namespace BeebotteConsumer
{
    public partial class ReadData : Form
    {
        internal Connector bbtConnector;
        internal string channelName;
        internal string resource;
        internal BBTManagement parent;

        public ReadData()
        {
            InitializeComponent();
        }

        private void ReadData_Load(object sender, EventArgs e)
        {
            List<ResourceRecord> data = bbtConnector.Read(channelName, resource);
            dataGridData.DataSource = data;

        }
    }
}
