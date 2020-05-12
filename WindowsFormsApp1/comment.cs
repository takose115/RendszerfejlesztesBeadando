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
using System.Threading;
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
            Thread.Sleep(100);
            //MessageBox.Show("ide valamiért kell egy ilyen stop");
            LoadActualComments_Request(lab_titok.Text);

        }
        private static Mutex mut = new Mutex();
        SimpleTcpClient client;
        int clientid;

        private void LoadActualComments_Request(string id)
        {
            string uzenetasd = "actualcommentload " + id;
            client.WriteLineAndGetReply(uzenetasd, TimeSpan.FromSeconds(0));
        }

        private void AddRowToPanel(TableLayoutPanel panel, string[] rowElements)
        {
            if (panel.ColumnCount != rowElements.Length)
                throw new Exception("Elements number doesn't match!" + panel.ColumnCount + " " + rowElements.Length);
            panel.RowCount++;
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            for (int i = 0; i < rowElements.Length; i++)
            {
                //panel.Controls.Add(new Label() { Text = rowElements[i] }, i, panel.RowCount - 1);
                Label lb = new Label();
                lb.Text = rowElements[i];    
                panel.Controls.Add(lb, i, panel.RowCount - 1);


            }
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            Thread.CurrentThread.IsBackground = true;
            //MessageBox.Show("hi");
            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);

            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if (command == "commentload")
            {
                List<NagyTopic> topiclist = new List<NagyTopic>();
                string eredmeny = valasz.Substring(valasz.IndexOf(" ") + 1);
                topiclist = JsonNet.Deserialize<List<NagyTopic>>(eredmeny);
                if (lab_title.InvokeRequired)
                {
                    lab_title.Invoke((MethodInvoker)delegate ()
                    {
                        lab_title.Text = topiclist[0].title;
                    });
                }else
                {
                    lab_title.Text = topiclist[0].title;
                }

                if (lab_desc.InvokeRequired)
                {
                    lab_desc.Invoke((MethodInvoker)delegate ()
                    {
                        lab_desc.Text = topiclist[0].title;
                    });
                }
                else
                {
                    lab_desc.Text = topiclist[0].title;
                }
            }
            else if (command == "actualcommentload")
            {
                
                List<ListazottComment> commentlist = new List<ListazottComment>();
                string eredmeny = valasz.Substring(valasz.IndexOf(" ") + 1);
                commentlist = JsonNet.Deserialize<List<ListazottComment>>(eredmeny);
                if(InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        for (int i = panel.Controls.Count - 1; i >= 1; --i)
                            panel.Controls[i].Dispose();
                        panel.Controls.Clear();
                        panel.RowCount = 1;
                        string[] rowElements = { "Username", "Comment", "Date", };
                        if (panel.ColumnCount != rowElements.Length)
                            throw new Exception("Elements number doesn't match!");
                        for (int i = 0; i < rowElements.Length; i++)
                        {
                            panel.Controls.Add(new Label() { Text = rowElements[i] }, i, panel.RowCount - 1);
                        }

                        panel.RowCount = 2;
                        string[] uressor = { "   ", "   ", "   " };

                        if (panel.ColumnCount != uressor.Length)
                            throw new Exception("Elements number doesn't match!");
                        for (int i = 0; i < uressor.Length; i++)
                        {
                            panel.Controls.Add(new Label() { Text = uressor[i] }, i, panel.RowCount - 1);
                        }

                        //space a címsor alatt 


                        foreach (ListazottComment it in commentlist)
                        {
                            string[] row = {

                                it.username.ToString(),
                                it.comment.ToString(),
                                it.date.ToString(),


                        };
                            AddRowToPanel(panel, row);
                        }
                    }));
                }
                else
                {
                    for (int i = panel.Controls.Count - 1; i >= 1; --i)
                        panel.Controls[i].Dispose();
                    panel.Controls.Clear();
                    panel.RowCount = 1;
                    string[] rowElements = { "Username", "Comment", "Date", };
                    if (panel.ColumnCount != rowElements.Length)
                        throw new Exception("Elements number doesn't match!");
                    for (int i = 0; i < rowElements.Length; i++)
                    {
                        panel.Controls.Add(new Label() { Text = rowElements[i] }, i, panel.RowCount - 1);
                    }

                    panel.RowCount = 2;
                    string[] uressor = { "   ", "   ", "   " };

                    if (panel.ColumnCount != uressor.Length)
                        throw new Exception("Elements number doesn't match!");
                    for (int i = 0; i < uressor.Length; i++)
                    {
                        panel.Controls.Add(new Label() { Text = uressor[i] }, i, panel.RowCount - 1);
                    }

                    //space a címsor alatt 


                    foreach (ListazottComment it in commentlist)
                    {
                        string[] row = {

                                it.username.ToString(),
                                it.comment.ToString(),
                                it.date.ToString(),


                        };
                        AddRowToPanel(panel, row);
                    }
                }
                
            }
            //Thread.CurrentThread.Abort();

        }

        private void but_cancel_Click(object sender, EventArgs e)
        {
            
                this.Close();
            this.Dispose();
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

            MessageBox.Show("New comment has been posted!");
            string uzenet = "addcomment "+sqlid+",";
            var serializer = JsonNet.Serialize(commentlista);
            client.WriteLineAndGetReply(uzenet + serializer, TimeSpan.FromSeconds(0));


            //--------------------------------------------------------------------------------------------------------
            

            
        }

        private void comment_Load(object sender, EventArgs e)
        {

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

    public class ListazottComment
    {


        public string comment;
        public string date;
        public string username;



        public ListazottComment(string c, string d, string u)
        {
            comment = c;
            date = d;
            username = u;

        }
        public ListazottComment() { }

    }
}

