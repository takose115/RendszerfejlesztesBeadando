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
using Json.Net;
using System.Web.Script.Serialization;

namespace WindowsFormsApp1
{
    //txtUsername txtPassword
    public partial class Login : Form
    {        
        public Login()
        {
            InitializeComponent();
        }
        string ipaddress = "127.0.0.1";
        string port = "8910";

        SimpleTcpClient client;
        

        private void Login_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(ipaddress);
            client.Connect(ipaddress, Convert.ToInt32(port));
        }        
        //client.WriteLineAndGetReply(txtTest.Text, TimeSpan.FromSeconds(3));
        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);
            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if(command=="login")
            {
                string eredmeny = valasz.Substring(valasz.IndexOf(" ")+1);
                if(eredmeny=="true")
                    MessageBox.Show("login successfulllllll");
                else
                    MessageBox.Show("login failed :(");
            }
            
        }

        //szervernek küldeni a felhasználónevet, jelszót
        //txtUsername txtPassword

        /*
            class msg
            {
                string from
                
            }
            kimenet: {from:login,msg:{username:asd,password:asd}}
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(RegisteredUsers);
        */
        private void bntLogin_Click(object sender, EventArgs e)
        {
            //login username,password
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string uzenet = "login " + username + "," + password;
            client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register f1 = new Register(client);
            this.Hide();
            client.Disconnect();
            //f1.FormClosed+=(s, args) => this.Close();
            f1.Show();
        }
        
    }
}
