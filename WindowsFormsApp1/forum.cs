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
    

    public partial class forum : Form
    {
        public forum(SimpleTcpClient clientm, int id)
        {
            InitializeComponent();

            client = clientm;
            clientid = id;

            client.DataReceived += Client_DataReceived;
            LoadTopic_Request();

        }

        SimpleTcpClient client;
        int clientid;

        private void LoadTopic_Request()
        {
            string uzenet = "topicload ";
            client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));
        }

        

        private void AddRowToPanel(TableLayoutPanel panel, string[] rowElements, int sql_id)
        {
            if (panel.ColumnCount != rowElements.Length)
                throw new Exception("Elements number doesn't match!" + panel.ColumnCount + " " + rowElements.Length);
            panel.RowCount++;
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            for (int i = 0; i < rowElements.Length; i++)
            {
                //MessageBox.Show(rowElements[i]);
                //panel.Controls.Add(new Label() { Text = rowElements[i] }, i, panel.RowCount - 1);
                Label lb = new Label();
                lb.Text = rowElements[i];
                lb.Click += new EventHandler((sender, e) => topic_open_Click(sender, e, sql_id));
                panel.Controls.Add(lb, i, panel.RowCount - 1);


            }
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            
            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);
            
            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if (command == "topicload")
            {
                List<Topic> topiclist = new List<Topic>();
                string eredmeny = valasz.Substring(valasz.IndexOf(" ") + 1);
                topiclist = JsonNet.Deserialize<List<Topic>>(eredmeny);
                Invoke(new MethodInvoker(delegate ()
                {
                    for (int i = panel.Controls.Count - 1; i >= 1; --i)
                        panel.Controls[i].Dispose();
                    panel.Controls.Clear();
                    panel.RowCount = 1;
                    string[] rowElements = { "Title", "User", "Date", };
                    
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

                    //így oldom meg a spacet a cimsor alatt


                    foreach (Topic it in topiclist)
                    {
                        string[] row = {

                                it.title.ToString(),
                                it.user.ToString(),
                                it.date.ToString(),
                                
                        };
                        AddRowToPanel(panel, row, it.id_sql);
                    }
                }));
            }

        }

        private void topic_open_Click(object sender, EventArgs e, int sql_id)
        {
            
            comment f1 = new comment(client, clientid, sql_id);
            this.Hide();
            f1.Show();
        }

        private void but_cancel_Click(object sender, EventArgs e)
        {
            Index f1 = new Index(client, clientid);
            this.Hide();
            f1.Show();
        }

        private void but_newtopic_Click(object sender, EventArgs e)
        {
            AddTopic f1 = new AddTopic(client, clientid);
            this.Hide();
            f1.Show();
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

        public Topic()
        {
            
        }

    }
}
