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
            client.DataReceived += Client_DataReceived;
            LoadItem_Request();
        }
        SimpleTcpClient client;
        int clientid;
        public void PlaceBid(object sender, EventArgs e)
        {                                                
            string buttonName = ((Button)sender).Name;
            string row = buttonName.Substring(7);
            Control ctr = panel.GetControlFromPosition(7, int.Parse(row)-1);
            
            int sqlid = int.Parse(ctr.Name.Substring(7));
            string uzenet = "placebid " + sqlid + "," + ctr.Text + "," +clientid;
            client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));
        }

        public void Buyout(object sender, EventArgs e)
        {
            string buttonName = ((Button)sender).Name;
            string row = buttonName.Substring(10);
            Control ctr = panel.GetControlFromPosition(7, int.Parse(row) - 1);
            Control ctr2 = panel.GetControlFromPosition(1, int.Parse(row) - 1);
            string termeknev = ctr2.Text;
            int sqlid = int.Parse(ctr.Name.Substring(7));
            Buyout f1 = new Buyout(client, clientid, sqlid, termeknev);
            f1.Show();
        }

        private void AddRowToPanel(TableLayoutPanel panel, string[] rowElements, int sql_id)
        {            
            if (panel.ColumnCount != rowElements.Length)
                throw new Exception("Elements number doesn't match!"+panel.ColumnCount+" "+rowElements.Length);
            panel.RowCount++;
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            for (int i = 0; i < rowElements.Length; i++)
            {
                if(i==rowElements.Length-3)
                {
                    TextBox tx = new TextBox();
                    tx.Name = "bidtxt_" + sql_id; 
                    panel.Controls.Add(tx, i, panel.RowCount - 1);
                }
                else if(i==rowElements.Length-2)
                {
                    Button bn = new Button();
                    bn.Text = "Place bid";
                    bn.Name = "bidbtn_" + panel.RowCount;
                    bn.Click += new EventHandler(PlaceBid);
                    panel.Controls.Add(bn, i, panel.RowCount - 1);
                }
                else if (i == rowElements.Length - 1)
                {
                    Button bn = new Button();
                    bn.Text = "Buyout";
                    bn.Name = "buyoutbtn_" + panel.RowCount;
                    bn.Click += new EventHandler(Buyout);
                    panel.Controls.Add(bn, i, panel.RowCount - 1);
                }
                else if(i==0)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(rowElements[0])));
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    panel.Controls.Add(pb, i, panel.RowCount - 1);
                }
                else
                {
                    panel.Controls.Add(new Label() { Text = rowElements[i] }, i, panel.RowCount - 1);
                }                
            }
        }
               
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
                    for (int i = panel.Controls.Count - 1; i >= 1; --i)
                            panel.Controls[i].Dispose();
                    panel.Controls.Clear();
                    panel.RowCount = 1;
                    string[] rowElements = { "Image", "Item name", "Item type", "Seller name", "Buyout price", "End date", "Current bid", "Add bid", "", "Buyout" };
                    if (panel.ColumnCount != rowElements.Length)
                        throw new Exception("Elements number doesn't match!");
                    for (int i = 0; i < rowElements.Length; i++)
                    {
                        panel.Controls.Add(new Label() { Text = rowElements[i] }, i, panel.RowCount - 1);
                    }
                    foreach (Item it in itemlist)
                    {
                        string[] row = {
                                it.image, //image
                                it.name, //name
                                it.typeName, //type
                                it.seller_name, //seller_name	
                                it.buyout.ToString(), //buyout	
                                it.endDate, //end_date	
                                it.current_bid.ToString(), //current_bid
                                "",
                                "",
                                ""
                        };                        
                        AddRowToPanel(panel, row,it.sql_id);
                    }                    
                }));
            }
            else if(command == "placebid")
            {
                string eredmeny = e.MessageString.Substring(0, e.MessageString.Length - 1).Substring(valasz.IndexOf(" ") + 1, 4);
                if (eredmeny == "true")
                {
                    LoadItem_Request();
                    MessageBox.Show("Bid is placed");
                }else if(eredmeny == "time")
                {
                    LoadItem_Request();
                    MessageBox.Show("This action is expired!");
                }                    
            }
        }

    private void SignOutBtn_Click(object sender, EventArgs e)
    {
        client.Disconnect();
            client.Dispose();
        Login f1 = new Login();
        this.Hide();
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

        private void but_forum_Click(object sender, EventArgs e)
        {
            forum f1 = new forum(client, clientid);
            this.Hide();
            f1.Show();
        }
    }
    public class Item
    {
        public int id;
        public int sellerid;
        public int staringBid;
        public int type;

        public string image;
        public string name;
        public string typeName;
        public string seller_name;
        public int buyout;
        public string endDate;
        public int current_bid;

        public int sql_id;

        public Item(string image, string name, string typeName, string seller_name, int buyout, string endDate, int current_bid, int sql_id)
        {
            this.image = image;
            this.name = name;
            this.typeName = typeName;
            this.seller_name = seller_name;
            this.buyout = buyout;
            this.endDate = endDate;
            this.current_bid = current_bid;
            this.sql_id = sql_id;
        }


        public Item()
        {
           
        }
    }

}
