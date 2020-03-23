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
using System.Xml.Serialization;

namespace RendszerfejlesztesServer
{
    public partial class Server : Form
    {

        //teszt rész
        public class TestClass
        {
            public int szam { get; set; }
            public string szoveg { get; set; }
            public bool idk { get; set; }
        }
        public static string Serialize(object AnObject)
        {
            XmlSerializer Xml_Serializer = new XmlSerializer(AnObject.GetType());
            StringWriter Writer = new StringWriter();

            Xml_Serializer.Serialize(Writer, AnObject);
            return Writer.ToString();
        }

        // teszt rész vége
        public Server()
        {
            InitializeComponent();
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
        }

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
                txtStatus.Text += "server stopped\n";
            }
        }
        
    }
}
