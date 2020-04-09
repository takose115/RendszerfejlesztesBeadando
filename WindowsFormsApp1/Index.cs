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
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Index : Form
    {//main page basically
        public Index(SimpleTcpClient clientm, int id)
        {
            InitializeComponent();
            listview_items.View = View.Details;
            listview_items.Columns.Add("id");
            listview_items.Columns.Add("sellerid");
            listview_items.Columns.Add("name");
            listview_items.Columns.Add("buyout");
            listview_items.Columns.Add("starting_bid");
            listview_items.Columns.Add("end_date");
            listview_items.Columns.Add("type");
            

            client = clientm;
            clientid = id;
            client.DataReceived += Client_DataReceived;
            LoadItem_Request();
        }
        SimpleTcpClient client;
        int clientid;        
        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
           
            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);
            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if (command == "itemload")
            {
                
                List<Item> itemlist = new List<Item>();
                string eredmeny = valasz.Substring(valasz.IndexOf(" ") + 1);
                itemlist = JsonNet.Deserialize<List<Item>>(eredmeny);
                Invoke(new MethodInvoker(delegate ()
                {
                    listview_items.Items.Clear();
                    foreach (Item it in itemlist)
                    {
                        ListViewItem lvi = new ListViewItem(new[] {
                            it.id.ToString(),
                            it.sellerid.ToString(),
                            it.name.ToString(),
                            it.buyout.ToString(),
                            it.staringBid.ToString(),
                            it.endDate.ToString(),
                            it.type.ToString()
                        });
                        listview_items.Items.Add(lvi);
                    }
                }));

                 

                /*if (eredmeny == "true")
                {
                    int id = int.Parse(valasz.Substring(valasz.IndexOf(",") + 1));
                    MessageBox.Show("login successful");
                    Invoke(new MethodInvoker(delegate ()
                    {
                        Index f1 = new Index(client, id);
                        this.Hide();
                        f1.Show();
                    }));
                }
                else
                    MessageBox.Show("login failed");*/
            }

        }

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


        private void LoadItem_Request()
        {
            string uzenet = "itemload ";
            client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string keyword = SearchTextBox.Text.ToString();
            string uzenet = "search " + keyword;
            client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));
        }

    }
    public class Item
    {
        public int id;
        public int sellerid;
        public string name;
        public int buyout;
        public int staringBid;
        public string endDate;
        public int type;
        public Item(int id, int sellerid, string name, int buyout, int staringBid, string endDate, int type)
        {
            this.id = id;
            this.sellerid = sellerid;
            this.name = name;
            this.buyout = buyout;
            this.staringBid = staringBid;
            this.endDate = endDate;
            this.type = type;
        }
        public Item() { }
    }

}
