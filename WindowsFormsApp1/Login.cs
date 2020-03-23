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
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        //teszt
        public class TestClass
        {
            public int szam { get; set; }
            public string szoveg { get; set; }
            public bool idk { get; set; }
        }
        public static Object DeSerialize(string XmlOfAnObject, Type ObjectType)
        {
            StringReader StrReader = new StringReader(XmlOfAnObject);
            XmlSerializer Xml_Serializer = new XmlSerializer(ObjectType);
            XmlTextReader XmlReader = new XmlTextReader(StrReader);
            try
            {
                Object AnObject = Xml_Serializer.Deserialize(XmlReader);
                return AnObject;
            }
            finally
            {
                XmlReader.Close();
                StrReader.Close();
            }
        }
        //teszt vége
        public Login()
        {
            InitializeComponent();
        }
        string ipaddress = "127.0.0.1";
        string port = "8910";

        SimpleTcpClient client;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(ipaddress);
            client.Connect(ipaddress, Convert.ToInt32(port));
        }

        private void Login_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtTest2.Invoke((MethodInvoker)delegate ()
            {
                TestClass teszt = new TestClass();
                string valasz = e.MessageString;
                txtTest.Text = valasz;
                valasz = valasz.Substring(0, valasz.Length-1);
                teszt = DeSerialize(valasz, typeof(TestClass)) as TestClass;
                txtTest2.Text += teszt.szam.ToString() + "    " + teszt.szoveg + "     " + teszt.idk.ToString();
            });
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            client.WriteLineAndGetReply(txtTest.Text, TimeSpan.FromSeconds(3));
        }

        private void btnClass_Click(object sender, EventArgs e)
        {
            client.WriteLineAndGetReply("kérem a classomat te gci", TimeSpan.FromSeconds(3));
        }
    }
}
