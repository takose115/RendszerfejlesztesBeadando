using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Buyout : Form
    {
        public Buyout(SimpleTcpClient clientm, int id, int tid, string tnev)
        {
            InitializeComponent();
            client = clientm;
            clientid = id;
            termekid = tid;
            termeknev = tnev;
            TermekNevLabel.Text = tnev;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);
            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if(command=="buyout")
            {
                string eredmeny = e.MessageString.Substring(0, e.MessageString.Length - 1).Substring(valasz.IndexOf(" ") + 1, 4);
                if (eredmeny == "true")
                {
                    MessageBox.Show("Buyout succesful");
                    this.Hide();
                }
                else if(eredmeny== "time")
                {
                    MessageBox.Show("This action has expired!");
                    this.Hide();
                }
                else
                    MessageBox.Show("Buyout Failed!");
            }
        }

        SimpleTcpClient client;
        int clientid;
        int termekid;
        string termeknev;
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            string uzenet = "buyout " + termekid + "," + clientid;
            client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));
        }
    }
}
