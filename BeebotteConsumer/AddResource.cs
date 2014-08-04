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
    public partial class AddResource : Form
    {
        public AddResource()
        {
            InitializeComponent();
        }

        internal Connector BBTConnector;
        internal string ChannelName;
        internal BBTManagement parent;

        private void btnAddResource_Click(object sender, EventArgs e)
        {

            Resource resource = new Resource(ChannelName, txtName.Text, txtLabel.Text, txtDescription.Text,
                                             ((KeyValuePair<int, string>) cmbType.SelectedItem).Value);
            BBTConnector.CreateResource(resource);
            this.Close();
           
            parent.BindResources();
        }

        private void AddResource_Load(object sender, EventArgs e)
        {
            var bbtTypes = typeof (BBTDataType).ToDictionary();
            cmbType.DataSource = new BindingSource(bbtTypes, null);
            cmbType.DisplayMember = "Value";
            cmbType.ValueMember = "Key";
        }
    }
}
