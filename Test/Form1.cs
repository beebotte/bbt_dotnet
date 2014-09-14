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
namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var connector = new Connector("67705d7fc5464f3b15431ec1b08cbcdb",
            //                              "ebc056c92ccf30db397bd59fd7e30ae4731efae4be6811faa8a0d121bf3c3ef5");

            var connector = new Connector("502b09f9113252ba91d0fa24b2e69c1e",
                                          "88303a6fdc866caeea2fe3bf4746611d16dce93196973d77e2037887f8fc6197");


            connector.GetAllConnections();

            connector.GetConnection("chakib");

            connector.DeleteConnection("chakib");
            connector.GetConnection("chakib");
            
        }
    }
}
