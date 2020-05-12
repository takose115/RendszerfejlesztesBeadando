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
    public partial class Subscribe : Form
    {
        public Subscribe(SimpleTcpClient clientm, int id)
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;

            client = clientm;
            clientid = id;
            client.DataReceived += Client_DataReceived;
        }
        SimpleTcpClient client;
        int clientid;

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);
            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if (command == "subscribe")
            {
                string eredmeny = valasz.Substring(valasz.IndexOf(" ") + 1, 4);
                if (eredmeny == "true")
                {
                    MessageBox.Show("Subscribe successful!");
                }
                else
                    MessageBox.Show("Subscribe failed please try again!");
            }
        }

        private void Button2_Click(object sender, EventArgs e) //cancel
        {
            Index f1 = new Index(client, clientid);
            this.Hide();
            f1.Show();
        }

        private void Button1_Click(object sender, EventArgs e) //subscribe
        {
            int type = comboBox1.SelectedIndex + 1;
            string uzenet = "subscribe ";
            client.WriteLineAndGetReply(uzenet + type + "," + clientid, TimeSpan.FromSeconds(0));
        }
    }
}
