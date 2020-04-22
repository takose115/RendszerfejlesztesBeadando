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
    public partial class AddTopic : Form
    {
        public AddTopic(SimpleTcpClient clientm, int id)
        {
            InitializeComponent();

            client = clientm;
            clientid = id;
            client.DataReceived += Client_DataReceived;
        }

        SimpleTcpClient client;
        int clientid;

        private void but_cancel_Click(object sender, EventArgs e)
        {
            AddTopic f1 = new AddTopic(client, clientid);
            this.Hide();
            f1.Show();
        }

        public class Topic
        {

            public int clientid;
            public string title;        
            public string desc;
            public string date;

            public Topic(int cid, string t, string d, string dat)
            {
                clientid = cid;
                title = t;
                desc = d;
                date = dat;
            }

        }


        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);
            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if (command == "newTopic")
            {
                string eredmeny = valasz.Substring(valasz.IndexOf(" ") + 1, 4);
                if (eredmeny == "true")
                {
                    MessageBox.Show("New topic has been posted!");
                    Invoke(new MethodInvoker(delegate ()
                    {
                        forum f1 = new forum(client, clientid);
                        this.Hide();
                        f1.Show();
                    }));
                }
                else
                    MessageBox.Show("Post failed please try again!");
            }
        }

        private void but_add_Click(object sender, EventArgs e)
        {
            string title = txt_title.Text;
            string desc = txt_desc.Text;
            string date = DateTime.Now.ToString("MM\\/dd\\/yyyy h\\:mm tt");

            List<Topic> topicLista = new List<Topic>();
            Topic topic = new Topic(clientid, title, desc, date);
            topicLista.Add(topic);

            string uzenet = "newTopic ";
            var serializer = JsonNet.Serialize(topic);
            client.WriteLineAndGetReply(uzenet + serializer, TimeSpan.FromSeconds(0));
        }
    }
}
