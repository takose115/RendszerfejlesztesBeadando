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
    public partial class AddItem : Form
    {//domján ide irod a cuccaid
        public AddItem(SimpleTcpClient clientm, int id)
        {
            InitializeComponent();
            client = clientm;
            clientid = id;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            //ide jon majd a valasz
        }

        SimpleTcpClient client;
        int clientid;

        class Item
        {
            int clientid;
            string name;
            int buyoutPrice;
            int startingBid;
            string endDate;
            string type;
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Index f1 = new Index(client, clientid);
            this.Hide();
            f1.Show();
        }
        
        private void SendBtn_Click(object sender, EventArgs e)
        {
            //termek nevu classba pakold az adatokat
            Item termek=new Item();
            string uzenet="newItem ";
            var serializer = new JavaScriptSerializer();
            uzenet+= serializer.Serialize(termek);
            client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));
        }
    }
}
