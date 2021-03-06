﻿using Json.Net;
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
using System.Net.Mail;
using System.Text;
using System.Threading;
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
        private static Mutex mut = new Mutex();
        private void CheckExpiredAcutions()
        {
            SQLiteCommand cmd2 = new SQLiteCommand(adatb.GetConnection());
            SQLiteDataReader reader2;
            while (true)
            {
                mut.WaitOne();
                string enddate;
                int id;
                cmd = new SQLiteCommand("select items.id, end_date, bids.userID, max(value) from bids, items where bids.itemID=items.id and (state=0 or state=1) group by items.id ", adatb.GetConnection());
                reader2= cmd.ExecuteReader();
                while (reader2.Read())
                {
                    id = reader2.GetInt32(0);
                    enddate = reader2.GetString(1);
                    DateTime now = DateTime.Now;
                    DateTime endingdate = DateTime.Parse(enddate+":59");
                    double dif = (now - endingdate).TotalSeconds;
                    if (endingdate < now)
                    {
                        if(dif<60)
                        {
                            cmd2.CommandText = "update items set state=1 where id=" + id;
                            cmd2.ExecuteNonQuery();
                        }else
                        {
                            cmd2.CommandText = "update items set state=3 where id=" + id;
                            cmd2.ExecuteNonQuery();
                            cmd2.CommandText = "insert into winners (userid,termekid,bid_amount) values ("+reader2.GetInt32(2)+","+reader2.GetInt32(0)+","+reader2.GetInt32(3)+")";
                            cmd2.ExecuteNonQuery();
                        }
                        
                    }
                }
                reader2.Close();
                mut.ReleaseMutex();
                Thread.Sleep(1000);
            }
            
        }
        //state : 0 vehető, 1 premium vehető, 2 megvett, 3 lejárt
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
            mut.WaitOne();
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
                                cmd.CommandText = "INSERT INTO Users(username, password, permission, premium, email) VALUES('" + username + "','" + password + "', 1, 0,'" + email + "')";
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
                                hibaLabel.Text = "1"+ex.ToString();
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
                                "SELECT items.image,items.name, Types.name, Users.username ,buyout, end_date, max(bids.value), items.id FROM Items JOIN Bids ON items.id = Bids.itemID JOIN Types ON items.type = Types.id JOIN Users ON items.sellerid = Users.id where state=0 or state=1 GROUP BY items.id", adatb.GetConnection());                                
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
                                hibaLabel.Text = "2" + ex.ToString();
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
                                hibaLabel.Text = "3" + ex.ToString();
                            }
                        });
                        break;
                    }

                case "actualcommentload":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(uzenet);
                            txtStatus.AppendText(Environment.NewLine);
                            try
                            {
                                List<Comment> commentlist = new List<Comment>();
                                
                                string topicid = uzenet.Substring(uzenet.IndexOf(" ") + 1);
                               

                                cmd = new SQLiteCommand(
                                "SELECT Comment.comment, Comment.date, Users.username FROM Comment JOIN Users ON Comment.userID = Users.id WHERE Comment.postID ="+topicid, adatb.GetConnection());
                                reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {

                                    commentlist.Add(new Comment(

                                        reader.GetString(0), //comment
                                        reader.GetString(1), //date     
                                         reader.GetString(2) //username
                                        ));
                                }
                                var stringjson = JsonNet.Serialize(commentlist);
                                e.ReplyLine("actualcommentload " + stringjson);
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                hibaLabel.Text = "4" + ex.ToString();
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
                                hibaLabel.Text = "5" + ex.ToString();
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

                            string eredmeny = uzenet.Substring(uzenet.IndexOf(",") + 1);
                           
                            commentlist = JsonNet.Deserialize<List<NewComment>>(eredmeny); 

                            //MessageBox.Show(comment.comment+comment.date+comment.postID.ToString()+comment.userid.ToString());

                            txtStatus.AppendText(commentlist[0].comment + ", " + commentlist[0].date + ", " + commentlist[0].userID + ", " + commentlist[0].postID);
                            txtStatus.AppendText(Environment.NewLine);

                            cmd.CommandText = "insert into COMMENT(postID,comment,date,userID) VALUES (" + commentlist[0].postID + ",'"+ commentlist[0].comment + "','" + commentlist[0].date + "'," + commentlist[0].userID + ")";
                            int a = cmd.ExecuteNonQuery();
                            if (a == 0)
                            {
                                txtStatus.AppendText("newComment failed");
                            }
                            else
                            {
                                txtStatus.AppendText("newComment succesfull");
                            }
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(uzenet);
                            txtStatus.AppendText(Environment.NewLine);
                            try
                            {
                                List<Comment> commentlist2 = new List<Comment>();

                                string topicid = uzenet.Substring(uzenet.IndexOf(" ") + 1, uzenet.IndexOf(",")-uzenet.IndexOf(" ")-1);


                                cmd = new SQLiteCommand(
                                "SELECT Comment.comment, Comment.date, Users.username FROM Comment JOIN Users ON Comment.userID = Users.id WHERE Comment.postID =" + topicid, adatb.GetConnection());
                                reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {

                                    commentlist2.Add(new Comment(

                                        reader.GetString(0), //comment
                                        reader.GetString(1), //date     
                                         reader.GetString(2) //username
                                        ));
                                }
                                var stringjson = JsonNet.Serialize(commentlist2);
                                e.ReplyLine("actualcommentload " + stringjson);
                                reader.Close();
                            }
                            catch (Exception ex)
                            {
                                hibaLabel.Text = "4" + ex.ToString();
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
                                //uzenet: "search" keyword min max type
                                string[] str = uzenet.Split(' ');
                                List<Item> itemList = new List<Item>();

                                string keyword = str[1];
                                string min = str[2];
                                string max = str[3];
                                string type = str[4];
                                string[] whereQuery = { "", "", "", ""};
                                int feltSum = 0;
                                int havingSum = 0;
                                if(keyword != "_")
                                {
                                    whereQuery[0] = "items.name LIKE '%" + keyword + "%'";
                                    feltSum++;
                                }
                                if(min != "_")
                                {
                                    whereQuery[1] = "max(Bids.value) > " + min;
                                    havingSum++;
                                }
                                if(max != "_")
                                {
                                    whereQuery[2] = "max(Bids.value) < " + max;
                                    havingSum++;
                                }
                                if (type != "_")
                                {
                                    whereQuery[3] = "Types.name='"+type+"'";
                                    feltSum++;
                                }
                                string queryString = "SELECT items.image,items.name, Types.name, Users.username ,buyout, end_date, max(bids.value), items.id FROM Items JOIN Bids ON items.id = Bids.itemID JOIN Types ON items.type = Types.id JOIN Users ON items.sellerid = Users.id WHERE (state=0 or state=1)";

                                if (feltSum==1)
                                {
                                    
                                    queryString += " AND "+whereQuery[0] + whereQuery[3];
                                }
                                else if(feltSum==2)
                                {
                                    queryString += " AND " + whereQuery[0] +" AND "+ whereQuery[3];
                                }

                                queryString += " GROUP BY items.id";

                                if (havingSum == 1)
                                {

                                    queryString += " HAVING " + whereQuery[1] + whereQuery[2];
                                }
                                else if (havingSum == 2)
                                {
                                    queryString += " HAVING " + whereQuery[1] + " AND " + whereQuery[2];
                                }


                                //cmd = new SQLiteCommand("SELECT items.image,items.name, Types.name, Users.username ,buyout, end_date, max(bids.value), items.id FROM Items JOIN Bids ON items.id = Bids.itemID JOIN Types ON items.type = Types.id JOIN Users ON items.sellerid = Users.id where items.name LIKE '%" + keyword + "%' and state=0 GROUP BY items.id", adatb.GetConnection());
                                cmd = new SQLiteCommand(queryString, adatb.GetConnection());

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
                                hibaLabel.Text = "6" + ex.ToString();
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


                            cmd = new SQLiteCommand("SELECT Email FROM Users JOIN Subscription ON Users.id = Subscription.userID WHERE Subscription.typeID =" + newItem.type, adatb.GetConnection());
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                string email = reader.GetString(0);

                                MailMessage mail = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                                string htmlString = @"
                                        <html>
	                                    <head></head>
	                                    <body>
		                                    <h1>New Item!</h1>
		                                    <p>Item name: " + newItem.name+ @" </p>
		                                    <p>Starting bid: " + newItem.startingBid + @" </p>
		                                    <p>Buyout price: " + newItem.buyoutPrice + @" </p>
		                                    <p>Ending date: " + newItem.endDate + @" </p>
	                                    </body>
                                    </html>
                                ";


                                mail.From = new MailAddress("utazoszervezo@gmail.com");
                                mail.To.Add(email);
                                mail.Subject = "New item!";
                                mail.IsBodyHtml = true;
                                mail.Body = htmlString;

                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential("utazoszervezo", "Admin100!");
                                SmtpServer.EnableSsl = true;

                                SmtpServer.Send(mail);
                            }


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
                            //premiumcheck
                            bool premium = false;
                            cmd = new SQLiteCommand("select premium from users where id=" + parameters[2], adatb.GetConnection());
                            reader = cmd.ExecuteReader();
                            reader.Read();
                            if (reader.GetInt32(0) == 1)
                                premium = true;
                            reader.Close();
                            cmd = new SQLiteCommand("select state from items where id=" + parameters[0], adatb.GetConnection());                            
                            reader = cmd.ExecuteReader();
                            reader.Read();
                            if (reader.GetInt32(0) == 3 || reader.GetInt32(0) == 2 || (reader.GetInt32(0)==1 && !premium))
                            {
                                e.ReplyLine("placebid time");
                                reader.Close();
                            }
                            else
                            {
                                reader.Close();
                                string query = "insert into Bids(itemID,userID,value) VALUES (" + parameters[0] + "," + parameters[2] + "," + parameters[1] + ")";
                                /*string query = "UPDATE Items SET starting_bid="+ parameters[1] + " WHERE Items.id="+parameters[0];*/
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                                e.ReplyLine("placebid true");
                            }
                                
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
                            cmd = new SQLiteCommand("select state from items where id=" + termekid, adatb.GetConnection());
                            reader = cmd.ExecuteReader();
                            reader.Read();
                            if(reader.GetInt32(0)!=0)
                            {
                                reader.Close();
                                e.ReplyLine("buyout time");
                            }
                            else
                            {
                                reader.Close();
                                string query = "UPDATE Items SET state=2, vasarloid=" + clientid + " WHERE Items.id=" + termekid;
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                                e.ReplyLine("buyout true");
                            }
                            
                        });
                        break;
                    }
                case "subscribe":
                    {
                        txtStatus.Invoke((MethodInvoker)delegate ()
                        {
                            txtStatus.AppendText(Environment.NewLine);
                            txtStatus.AppendText(uzenet);
                            txtStatus.AppendText(Environment.NewLine);
                            int clientid = int.Parse(uzenet.Substring(uzenet.IndexOf(",") + 1));
                            int type = int.Parse(uzenet.Substring(uzenet.IndexOf(" ") + 1, uzenet.IndexOf(",") - uzenet.IndexOf(" ") - 1));
                            cmd = new SQLiteCommand("SELECT COUNT(userID) FROM Subscription WHERE userID="+clientid+" AND typeID="+type, adatb.GetConnection());

                            reader = cmd.ExecuteReader();
                            reader.Read();
                            if (reader.GetInt32(0) != 0)
                            {
                                MessageBox.Show("Already subscribed to that type!");
                                reader.Close();
                            }
                            else
                            {
                                reader.Close();
                                string query = "insert into Subscription(typeID,userID) VALUES (" + type + "," + clientid + ")";
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                                e.ReplyLine("subscribe true");
                            }

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
            mut.ReleaseMutex();
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
            Thread t = new Thread(new ThreadStart(CheckExpiredAcutions));
            t.Start();
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

    public class Comment
    {


        public string comment;
        public string date;
        public string username;
        


        public Comment(string c, string d, string u)
        {
            comment = c;
            date = d;
            username = u;

        }
        public Comment() { }

    }
}


