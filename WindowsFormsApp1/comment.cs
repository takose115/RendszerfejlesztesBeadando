using Json.Net;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class comment : Form
    {

        public comment(SimpleTcpClient clientm, int id, string username)
        {
            InitializeComponent();

            client = clientm;
            clientid = id;
            string authorname = username;

            LoadComments_Request();

        }

        SimpleTcpClient client;
        int clientid;

        private void but_cancel_Click(object sender, EventArgs e)
        {
            forum f1 = new forum(client, clientid);
            this.Hide();
            f1.Show();
        }

        private void LoadComments_Request()
        {
            string uzenet = "commentload ";
            client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));
        }
    }
}
