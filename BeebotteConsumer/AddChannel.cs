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
    public partial class AddChannel : Form
    {
        internal Connector BBTConnector;
        internal BBTManagement parent;
        private List<Resource> resources;
        public AddChannel()
        {
            InitializeComponent();
        }

        private void btnAddChannel_Click(object sender, EventArgs e)
        {
            Channel channel = new Channel();
            channel.Name = txtName.Text;
            channel.Label = txtLabel.Text;
            channel.Description = txtDescription.Text;
            channel.IsPublic = chkPublic.Checked;
            channel.Resources = resources;
            BBTConnector.CreateChannel(channel);
            this.Close();
            parent.BindChannels();
        }

        private void AddChannel_Load(object sender, EventArgs e)
        {
            resources = new List<Resource>();
            var bbtTypes = typeof(BBTDataType).ToDictionary();
            cmbType.DataSource = new BindingSource(bbtTypes, null);
            cmbType.DisplayMember = "Value";
            cmbType.ValueMember = "Key";
        }

        private void btnAddResource_Click(object sender, EventArgs e)
        {   
            resources.Add(new Resource(
                txtResourceName.Text,
                txtResourceLabel.Text,
                txtResourceDescription.Text,
                ((KeyValuePair<int, string>)cmbType.SelectedItem).Value
                ));
            BindResources();
        }

        private void BindResources()
        {
            BindingSource bindingSource = new BindingSource(resources, null);
            this.dataGridResources.DataSource = bindingSource;
        }
    }
}
