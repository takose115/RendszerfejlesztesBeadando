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
    public partial class comment : Form
    {

        public comment(SimpleTcpClient clientm, int id, int sql_id)
        {
            InitializeComponent();

            client = clientm;
            clientid = id;
            string topicid = sql_id.ToString();
            lab_titok.Text = topicid;

            client.DataReceived += Client_DataReceived;
            LoadComments_Request(topicid);

        }

        SimpleTcpClient client;
        int clientid;

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {

            //MessageBox.Show("hi");

            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);

            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if (command == "commentload")
            {
                List<NagyTopic> topiclist = new List<NagyTopic>();
                string eredmeny = valasz.Substring(valasz.IndexOf(" ") + 1);
                topiclist = JsonNet.Deserialize<List<NagyTopic>>(eredmeny);
               

                Invoke(new MethodInvoker(delegate ()
                {
                    lab_title.Text = topiclist[0].title;
                    lab_desc.Text= topiclist[0].desc;
                }));
            }
            else if (command == "newComment")
            {
                string eredmeny = valasz.Substring(valasz.IndexOf(" ") + 1, 4);
                if (eredmeny == "true")
                {
                    MessageBox.Show("New comment has been posted!");
                }
                else
                    MessageBox.Show("Comment failed please try again!");
            }

        }

        private void but_cancel_Click(object sender, EventArgs e)
        {
            forum f1 = new forum(client, clientid);
            this.Hide();
            f1.Show();
        }

        private void LoadComments_Request(string id)
        {
            
            string uzenet = "commentload "+ id;
            client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));
        }

        private void but_comment_Click(object sender, EventArgs e)
        {

            string date = DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm tt");
            
            int sqlid = int.Parse(lab_titok.Text);

            List<Comment> commentlista = new List<Comment>();
            Comment comment = new Comment(txt_comment.Text, date, clientid, sqlid);
            commentlista.Add(comment);

            string uzenet = "addcomment ";
            var serializer = JsonNet.Serialize(commentlista);
            client.WriteLineAndGetReply(uzenet + serializer, TimeSpan.FromSeconds(0));


            //--------------------------------------------------------------------------------------------------------
            

            
        }
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
            this.user = u;
        }

        public NagyTopic()
        {

        }

    }

    public class Comment { 

        public string comment;
        public string date;
        public int userID;
        public int postID;


        public Comment(string c, string d, int user, int postid)
        {
            this.comment = c;
            this.date = d;
            this.userID = user;
            this.postID = postid;
            
        }

        public Comment()
        {

        }

    }
}

