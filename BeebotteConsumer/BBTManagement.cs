using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Beebotte.API.Server.Net;

namespace BeebotteConsumer
{
    public partial class BBTManagement : Form
    {
        private Connector bbtConnector;
        private List<Resource> channelResources;
        private Button deleteRowButton = new Button();
        private string channelName;
        private string resourceName;

        public BBTManagement()
        {
         
            InitializeComponent();
            if (ConfigurationSettings.AppSettings != null)
                bbtConnector = new Connector(

                    ConfigurationSettings.AppSettings[ConfigurationKeys.AccessKey].ToString(),
                    ConfigurationSettings.AppSettings[ConfigurationKeys.SecurityKey].ToString(),
                    ConfigurationSettings.AppSettings[ConfigurationKeys.Hostname].ToString(),
                    ConfigurationSettings.AppSettings[ConfigurationKeys.Protocol].ToString(),
                    int.Parse(ConfigurationSettings.AppSettings[ConfigurationKeys.Port].ToString()),
                    ConfigurationSettings.AppSettings[ConfigurationKeys.Version].ToString());
            BindChannels();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void dataGridChannels_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                channelName = dataGridChannels[4, e.RowIndex].Value.ToString();
                if (e.ColumnIndex == 0)
                {
                    bbtConnector.DeleteChannel(channelName);
                    BindChannels();

                }
                else if (e.ColumnIndex == 1)
                {
                    BindResources();
                }
            }
           
        }

        internal void BindChannels()
        {
            List<Channel> channels = bbtConnector.GetAllChannels();
            dataGridChannels.DataSource = channels;
        }

        internal void BindResources()
        {
            dataGridResources.Visible = true;
            groupResources.Visible = true;
          
            List<Resource> resources = bbtConnector.GetAllResources(channelName);
            dataGridResources.DataSource = resources;
        }

        private void dataGridResources_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resourceName = dataGridResources[8, e.RowIndex].Value.ToString();
                if (e.ColumnIndex == 0)
                {
                    bbtConnector.DeleteResource(channelName, resourceName);
                    BindResources();
                }
                else if (e.ColumnIndex == 1)
                {
                    WriteData form = new WriteData();
                    form.bbtConnector = bbtConnector;
                    form.channelName = channelName;
                    form.resource = resourceName;
                    form.parent = this;
                    form.ShowDialog(this);
                }
                else if (e.ColumnIndex == 2)
                {
                    ReadData form = new ReadData();
                    form.bbtConnector = bbtConnector;
                    form.channelName = channelName;
                    form.resource = resourceName;
                    form.parent = this;
                    form.ShowDialog(this);
                }
            }
        }

        private void btnAddResource_Click(object sender, EventArgs e)
        {
            AddResource form = new AddResource();
            form.BBTConnector = bbtConnector;
            form.ChannelName = channelName;
            form.parent = this;
            form.ShowDialog(this);
        }

        private void btnAddChannel_Click(object sender, EventArgs e)
        {
            AddChannel form = new AddChannel();
            form.BBTConnector = bbtConnector;
            form.parent = this;
            form.ShowDialog(this);
        }
    }
}
    