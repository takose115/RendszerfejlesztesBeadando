using Json.Net;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

        public Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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

                                cmd = new SQLiteCommand("" +
                                "SELECT items.image,items.name, Types.name, Users.username ,buyout, end_date, max(bids.value), items.id FROM Items JOIN Bids ON items.id = Bids.itemID JOIN Types ON items.type = Types.id JOIN Users ON items.sellerid = Users.id where state=0 GROUP BY items.id", adatb.GetConnection());                                
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    Image img = Image.FromFile(reader.GetString(0));
                                    //Image img = resizeImage(img, new Size(80,50));
                                    byte[] imageArray = ImageToByteArray(img);
                                    string imageString = Convert.ToBase64String(imageArray);
                                    itemList.Add(new Item(
                                        imageString, //item.image 
                                        reader.GetString(1), //item.name                                       
                                        reader.GetString(2), //type.name
                                        reader.GetString(3), //user.username
                                        reader.GetInt32(4), //item.buyout
                                        reader.GetString(5), //item.end_date
                                        reader.GetInt32(6), //bids.value
                                        reader.GetInt32(7) //item.id
                                        ));
                                }
                                var stringjson = JsonNet.Serialize(itemList);
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

                case "commentload":
                    {

                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(uzenet);
                            txtStatus.AppendText(Environment.NewLine);
                            try
                            {

                                List<NagyTopic> itemList = new List<NagyTopic>();

                                string topicid = uzenet.Substring(uzenet.IndexOf(" ") + 1);

                                cmd = new SQLiteCommand("" +
                                    "SELECT forum.title, forum.description, users.username from Forum JOIN Users ON forum.userID = Users.id WHERE forum.id=" + topicid, adatb.GetConnection());

                                reader = cmd.ExecuteReader();

                                //MessageBox.Show(reader.GetString(0)+ reader.GetString(1)+ reader.GetString(2));

                                while (reader.Read())
                                {

                                    itemList.Add(new NagyTopic(

                                                reader.GetString(0), //title                                      
                                                reader.GetString(1), //desc
                                                reader.GetString(2) //username

                                                ));
                                }

                                //MessageBox.Show(itemList[0].title+ itemList[0].desc+ itemList[0].user);

                                var stringjson = JsonNet.Serialize(itemList);
                                e.ReplyLine("commentload " + stringjson);
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                hibaLabel.Text = ex.ToString();
                            }
                        });
                        break;
                    }

                case "topicload":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(command);
                            txtStatus.AppendText(Environment.NewLine);
                            try
                            {
                                List<Topic> topicList = new List<Topic>();

                                cmd = new SQLiteCommand("" +
                                "SELECT forum.title, forum.date, users.username, forum.id FROM Forum JOIN Users ON forum.userID = Users.id", adatb.GetConnection());
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    
                                    topicList.Add(new Topic(
                                        
                                        reader.GetString(0), //topic_name
                                        reader.GetString(1), //topic_date     
                                        reader.GetString(2), //topic_user                                             
                                        reader.GetInt32(3) //id
                                        ));
                                }
                                var stringjson = JsonNet.Serialize(topicList);
                                e.ReplyLine("topicload " + stringjson);
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                hibaLabel.Text = ex.ToString();
                            }
                        });
                        break;
                    }

                case "addcomment":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText("addcomment");
                            txtStatus.AppendText(Environment.NewLine);

                            List<NewComment> commentlist = new List<NewComment>();

                            string eredmeny = uzenet.Substring(uzenet.IndexOf(" ") + 1);
                           
                            commentlist = JsonNet.Deserialize<List<NewComment>>(eredmeny); //itt baj van 

                            //MessageBox.Show(comment.comment+comment.date+comment.postID.ToString()+comment.userid.ToString());

                            txtStatus.AppendText(commentlist[0].comment + ", " + commentlist[0].date + ", " + commentlist[0].userID + ", " + commentlist[0].postID);
                            txtStatus.AppendText(Environment.NewLine);

                            cmd.CommandText = "insert into COMMENT(postID,comment,date,userID) VALUES (" + commentlist[0].postID + ",'"+ commentlist[0].comment + "','" + commentlist[0].date + "'," + commentlist[0].userID + ")";
                            int a = cmd.ExecuteNonQuery();
                            if (a == 0)
                            {
                                e.ReplyLine("newComment false");
                                txtStatus.AppendText("newComment failed");
                            }
                            else
                            {
                                e.ReplyLine("newComment true");
                                txtStatus.AppendText("newComment succesfull");
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

                                cmd = new SQLiteCommand("SELECT items.image,items.name, Types.name, Users.username ,buyout, end_date, max(bids.value), items.id FROM Items JOIN Bids ON items.id = Bids.itemID JOIN Types ON items.type = Types.id JOIN Users ON items.sellerid = Users.id where items.name LIKE '%" + keyword + "%' and state=0 GROUP BY items.id", adatb.GetConnection());
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    Image img = Image.FromFile(reader.GetString(0));
                                    //Image img = resizeImage(img, new Size(80,50));
                                    byte[] imageArray = ImageToByteArray(img);
                                    string imageString = Convert.ToBase64String(imageArray);
                                    itemList.Add(new Item(
                                        imageString, //item.image 
                                        reader.GetString(1), //item.name                                       
                                        reader.GetString(2), //type.name
                                        reader.GetString(3), //user.username
                                        reader.GetInt32(4), //item.buyout
                                        reader.GetString(5), //item.end_date
                                        reader.GetInt32(6), //bids.value
                                        reader.GetInt32(7) //item.id
                                        ));
                                }
                                var stringjson = JsonNet.Serialize(itemList);
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
                case "newItem":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText("newItem");
                            txtStatus.AppendText(Environment.NewLine);
                            string eredmeny = uzenet.Substring(uzenet.IndexOf(" ") + 1);
                            NewItem newItem =JsonNet.Deserialize<NewItem>(eredmeny);
                            Image img = Image.FromStream(new MemoryStream(Convert.FromBase64String(newItem.image)));                            
                            string imgPath = "..//..//img//" + RandomString(10)+".jpg";
                            img.Save(imgPath, ImageFormat.Jpeg);
                            txtStatus.AppendText(newItem.clientid + ", " + newItem.name + ", " + newItem.buyoutPrice + ", " + newItem.startingBid + ", " + newItem.endDate + ", " + newItem.type);
                            txtStatus.AppendText(Environment.NewLine);
                            cmd.CommandText = "insert into ITEMS(sellerID,name,buyout,starting_bid,end_date,type,state,image) VALUES ("+newItem.clientid+",'"+newItem.name+"',"+newItem.buyoutPrice+","+newItem.startingBid+",'"+newItem.endDate+"',"+newItem.type+",0,'"+imgPath+"')";
                            int a = cmd.ExecuteNonQuery();
                            if (a == 0)
                            {
                                e.ReplyLine("newItem false");
                                txtStatus.AppendText("newItem failed");
                            }
                            else
                            {
                                e.ReplyLine("newItem true");
                                txtStatus.AppendText("newItem succesfull");
                            }
                            cmd = new SQLiteCommand("select id from items order by id desc limit 1", adatb.GetConnection());
                            reader = cmd.ExecuteReader();
                            reader.Read();
                            int itemid = reader.GetInt32(0);
                            reader.Close();
                            cmd.CommandText = "insert into Bids(itemID,userID,value) VALUES (" + itemid + "," + newItem.clientid + "," + newItem.startingBid + ")";
                            cmd.ExecuteNonQuery();
                        });
                        break;
                    }

                case "newTopic":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText("newTopic");
                            txtStatus.AppendText(Environment.NewLine);
                            string eredmeny = uzenet.Substring(uzenet.IndexOf(" ") + 1);
                            NewTopic newTopic = JsonNet.Deserialize<NewTopic>(eredmeny);
                            txtStatus.AppendText(newTopic.clientid + ", " + newTopic.title + ", " + newTopic.desc + ", " + newTopic.date);
                            txtStatus.AppendText(Environment.NewLine);
                            cmd.CommandText = "insert into FORUM(title,description,userID,date) VALUES ('" + newTopic.title + "','" + newTopic.desc + "'," + newTopic.clientid + ",'"+ newTopic.date + "')";
                            int a = cmd.ExecuteNonQuery();
                            if (a == 0)
                            {
                                e.ReplyLine("newTopic false");
                                txtStatus.AppendText("newTopic failed");
                            }
                            else
                            {
                                e.ReplyLine("newTopic true");
                                txtStatus.AppendText("newTopic succesfull");
                            }
                            
                        });
                        break;
                    }
                case "placebid":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(uzenet);
                            txtStatus.AppendText(Environment.NewLine);
                            /*string id = uzenet.Substring(uzenet.IndexOf(" ") + 1, uzenet.IndexOf(",") - uzenet.IndexOf(" ") - 1);
                            string value = uzenet.Substring(uzenet.IndexOf(",") + 1);*/
                            string[] parameters = uzenet.Substring(uzenet.IndexOf(" ") + 1).Split(',');

                            string query = "insert into Bids(itemID,userID,value) VALUES (" + parameters[0] + "," + parameters[2] + "," + parameters[1] + ")";
                            /*string query = "UPDATE Items SET starting_bid="+ parameters[1] + " WHERE Items.id="+parameters[0];*/
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                            e.ReplyLine("placebid ");
                        });
                            break;
                    }
                case "buyout":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(uzenet);
                            txtStatus.AppendText(Environment.NewLine);
                            /*string id = uzenet.Substring(uzenet.IndexOf(" ") + 1, uzenet.IndexOf(",") - uzenet.IndexOf(" ") - 1);
                            string value = uzenet.Substring(uzenet.IndexOf(",") + 1);*/
                            int clientid = int.Parse(uzenet.Substring(uzenet.IndexOf(",")+1));
                            int termekid = int.Parse(uzenet.Substring(uzenet.IndexOf(" ")+1, uzenet.IndexOf(",") - uzenet.IndexOf(" ")-1));

                            string query = "UPDATE Items SET state=1, vasarloid="+clientid+" WHERE Items.id="+termekid;
                            cmd.CommandText = query;
                            cmd.ExecuteNonQuery();
                            e.ReplyLine("buyout true");
                        });
                        break;
                    }
                default:
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText("unrecognized command: " + command);
                        });
                        break;
                    }
                
            }
         }
        //todo : sajátra ne lehessen bidelni
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
    }

    public class Topic
    {

        public string title;
        public string user;
        public string date;
        public int id_sql;

        public Topic(string tit, string dat, string u, int sql)
        {
            this.title = tit;
            this.date = dat;
            this.user = u;
            this.id_sql = sql;
        }
    }

    public class NewItem
    {
        public string image;
        public int clientid;
        public string name;
        public int buyoutPrice;
        public int startingBid;
        public string endDate;
        public int type;
        public NewItem(string image,int cid, string na, int bp, int sb, string ed, int ty)
        {
            this.image = image;
            clientid = cid;
            name = na;
            buyoutPrice = bp;
            startingBid = sb;
            endDate = ed;
            type = ty;
        }
        public NewItem() { }

    }

    public class NewTopic
    {

        public int clientid;
        public string title;
        public string desc;
        public string date;

        public NewTopic(int cid, string t, string d, string dat)
        {
            clientid = cid;
            title = t;
            desc = d;
            date = dat;
           
        }
        public NewTopic() { }

    }

    public class NagyTopic
    {

        public string title;
        public string user;
        public string desc;


        public NagyTopic(string tit, string des, string u)
        {
            this.title = tit;
            this.desc = des;
            this.user = u;
            
        }

        public NagyTopic()
        {

        }

    }

    public class NewComment
    {

        
        public string comment;
        public string date;
        public int userID;
        public int postID;


        public NewComment(string c, string d, int user, int post)
        {
            comment = c;
            date = d;
            userID = user;
            postID = post;

        }
        public NewComment() { }

    }


}


