using Json.Net;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    

    public partial class forum : Form
    {
        public forum(SimpleTcpClient clientm, int id)
        {
            InitializeComponent();

            client = clientm;
            clientid = id;
            
        }

        SimpleTcpClient client;
        int clientid;

        private void but_cancel_Click(object sender, EventArgs e)
        {
            Index f1 = new Index(client, clientid);
            this.Hide();
            f1.Show();
        }

        private void but_newtopic_Click(object sender, EventArgs e)
        {
            AddTopic f1 = new AddTopic(client, clientid);
            this.Hide();
            f1.Show();
        }
    }
}
