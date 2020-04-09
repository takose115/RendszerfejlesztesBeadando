using Json.Net;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RendszerfejlesztesServer
{
    public partial class Server : Form
    {        
        //important: datareceived function runs on a different thread, cannot reach any ui element without invoking, keep in mind when creating new forms and navigating between them
        static db adatb = new db();
        SQLiteDataReader reader;
        SQLiteCommand cmd;
        public Server()
        {
            InitializeComponent();
            adatb.openConnection();
            cmd = new SQLiteCommand(adatb.GetConnection());
        }
        SimpleTcpServer server;

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            string temp2 = e.MessageString;
            string uzenet= e.MessageString.Substring(0, e.MessageString.Length - 1);
            string command = uzenet.Substring(0, uzenet.IndexOf(" "));
            switch(command)
            {
                //login username,password
                case "login":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate()
                        {
                            string username = uzenet.Substring(uzenet.IndexOf(" ") + 1, uzenet.IndexOf(",") - uzenet.IndexOf(" ") - 1);
                            string password = uzenet.Substring(uzenet.IndexOf(",") + 1);
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(username + " login try");
                            txtStatus.AppendText(Environment.NewLine);
                            cmd = new SQLiteCommand("Select COUNT(id) from Users where username='" + username + "' and password='" + password + "'", adatb.GetConnection());
                            reader = cmd.ExecuteReader();
                            reader.Read();
                            if (reader.GetInt32(0) > 0)
                            {
                                int id;
                                reader.Close();
                                cmd = new SQLiteCommand("Select id from Users where username='" + username + "' and password='" + password + "'", adatb.GetConnection());
                                reader = cmd.ExecuteReader();
                                reader.Read();
                                id = reader.GetInt32(0);
                                e.ReplyLine("login true,"+id);
                                txtStatus.AppendText("login succesfull "+id);
                            }
                            else
                            {
                                e.ReplyLine("login false");
                                txtStatus.AppendText("login failed");
                            }
                            reader.Close();
                        });
                        break;
                    }
                //register username,password1,email
                //TODO: ne lehessen regelni olyannal ami már van
                case "register":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            string[] temp = (uzenet.Substring(uzenet.IndexOf(" ") + 1)).Split(',');
                            string username = temp[0];
                            string password = temp[1];
                            string email = temp[2];
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText("register try: " + username + " " + email);
                            txtStatus.AppendText(Environment.NewLine);
                            try
                            {
                                cmd.CommandText = "INSERT INTO Users(username, password, permission, premium, email) VALUES('" + username + "','" + password + "', 1, 1,'" + email + "')";
                                int a = cmd.ExecuteNonQuery();
                                if (a == 0)
                                {
                                    e.ReplyLine("register false");
                                    txtStatus.AppendText("register failed");
                                }
                                else
                                {
                                    e.ReplyLine("register true");
                                    txtStatus.AppendText("register succesfull");
                                }
                            }
                            catch (Exception ex)
                            {
                                hibaLabel.Text = ex.ToString();
                            }
                        });
                        break;
                    }
                case "itemload": 
                    {                        
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(command);
                            txtStatus.AppendText(Environment.NewLine);
                            try
                            {
                                List<Item> itemList = new List<Item>();

                                cmd = new SQLiteCommand("Select * from Items ", adatb.GetConnection());                                
                                reader = cmd.ExecuteReader();
                                while(reader.Read())
                                {
                                    itemList.Add(new Item(
                                        reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetString(2),
                                        reader.GetInt32(3),
                                        reader.GetInt32(4),
                                        reader.GetString(5),
                                        reader.GetInt32(6)
                                        ));
                                }
                                var stringjson = JsonNet.Serialize(itemList);
                                txtStatus.AppendText(stringjson);
                                e.ReplyLine("itemload " + stringjson);
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                hibaLabel.Text = ex.ToString();
                            }
                        });
                        break;
                    }
                case "search":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(uzenet);
                            txtStatus.AppendText(Environment.NewLine);
                            try
                            {
                                List<Item> itemList = new List<Item>();

                                string keyword = uzenet.Substring(uzenet.IndexOf(" ") + 1);

                                cmd = new SQLiteCommand("Select * from Items where name LIKE '%"+ keyword + "%'", adatb.GetConnection());
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    itemList.Add(new Item(
                                        reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetString(2),
                                        reader.GetInt32(3),
                                        reader.GetInt32(4),
                                        reader.GetString(5),
                                        reader.GetInt32(6)
                                        ));
                                }
                                var stringjson = JsonNet.Serialize(itemList);
                                txtStatus.AppendText(stringjson);
                                txtStatus.AppendText(keyword);
                                e.ReplyLine("itemload " + stringjson);
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                hibaLabel.Text = ex.ToString();
                            }
                        });
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
         }
            /*
            txtStatus.Invoke((MethodInvoker)delegate ()
                {
                    txtStatus.Text += e.MessageString;
                    TestClass teszt = new TestClass();
                    teszt.szam = 69;
                    teszt.szoveg = "domján a kedvenc sakk játékosom";
                    teszt.idk = false;
                    String XML;
                    XML = Serialize(teszt);
                    e.ReplyLine(string.Format(XML));
                });
            
            txtTest2.Invoke((MethodInvoker)delegate ()
            {
                TestClass teszt = new TestClass();
                string valasz = e.MessageString;
                txtTest.Text = valasz;
                valasz = valasz.Substring(0, valasz.Length-1);
                teszt = DeSerialize(valasz, typeof(TestClass)) as TestClass;
                txtTest2.Text += teszt.szam.ToString() + "    " + teszt.szoveg + "     " + teszt.idk.ToString();
            });

              */
        

        private void btnStart_Click(object sender, EventArgs e)
        {
            System.Net.IPAddress ip = System.Net.IPAddress.Parse("127.0.0.1");
            server.Start(ip, Convert.ToInt32(txtPort.Text));
            txtStatus.Text += "server started\n";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
            {
                server.Stop();
                txtStatus.AppendText(Environment.NewLine);
                txtStatus.Text += "server stopped";
            }
        }

        private void Server_FormClosed(object sender, FormClosedEventArgs e)
        {
            adatb.closeConnection();
            if (server.IsStarted)
            {
                server.Stop();
            }
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
    }
}
