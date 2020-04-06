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
    public partial class Index : Form
    {//main page basically
        public Index(SimpleTcpClient clientm, int id)
        {
            InitializeComponent();
            client = clientm;
            clientid = id;
        }
        SimpleTcpClient client;
        int clientid;
        private void SignOutBtn_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            client.Dispose();
            Login f1 = new Login();
            this.Hide();
            f1.Show();
        }

        private void AddItemBtn_Click(object sender, EventArgs e)
        {
            AddItem f1 = new AddItem(client, clientid);
            this.Hide();
            f1.Show();
        }
    }
}
