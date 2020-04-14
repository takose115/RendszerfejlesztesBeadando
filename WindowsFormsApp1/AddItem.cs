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
    public partial class AddItem : Form
    {//domján ide irod a cuccaid
        //oksa, köszi Ákos
        public AddItem(SimpleTcpClient clientm, int id)
        {
            InitializeComponent();

            string name = text_name.Text;
            string startbid = num_start.Value.ToString();
            string buyout = num_buyout.Value.ToString();
            string enddate = date_ending.Value.ToString();
            string type = list_type.SelectedIndex.ToString();

            list_type.Items.Add("1. TV");
            list_type.Items.Add("2. Telefon");
            list_type.Items.Add("3. Autó");

            string rrr = text_name.Text;

            client = clientm;
            clientid = id;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);
            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if (command == "newItem")
            {
                string eredmeny = valasz.Substring(valasz.IndexOf(" ") + 1, 4);
                if (eredmeny == "true")
                {                   
                    MessageBox.Show("Upload successful!");
                    Invoke(new MethodInvoker(delegate ()
                    {
                        Index f1 = new Index(client, clientid);
                        this.Hide();
                        f1.Show();
                    }));
                }
                else
                    MessageBox.Show("Upload failed please try again!");
            }
        }

        SimpleTcpClient client;
        int clientid;

        public class Item
        {
            
            public int clientid;
            public string name;
            public int buyoutPrice;
            public int startingBid;
            public string endDate;
            public int type;
            public Item(int cid, string na, int bp, int sb, string ed, int ty)
            {
                clientid = cid;
                name = na;
                buyoutPrice = bp;
                startingBid = sb;
                endDate = ed;
                type = ty;
            }

        }
        

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Index f1 = new Index(client, clientid);
            this.Hide();
            f1.Show();
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            string name = text_name.Text;
            int startbid = int.Parse(num_start.Value.ToString());
            int buyout = int.Parse(num_buyout.Value.ToString());
            string enddate = date_ending.Value.ToString();
            int type = list_type.SelectedIndex+1;

            List<Item> termekLista = new List<Item>();
            Item termek = new Item(clientid,name,startbid,buyout,enddate,type);
            termekLista.Add(termek);

            string uzenet = "newItem ";
            var serializer = JsonNet.Serialize(termek);
            client.WriteLineAndGetReply(uzenet+serializer, TimeSpan.FromSeconds(0));
        }
        
    }
}
        
    

