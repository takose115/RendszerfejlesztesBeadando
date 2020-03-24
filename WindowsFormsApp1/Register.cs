using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Register : Form
    {

        string ipaddress = "127.0.0.1";
        string port = "8910";
        public Register(SimpleTcpClient bejovoClient)
        {
            InitializeComponent();
            //client = bejovoClient;
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(ipaddress);
            client.Connect(ipaddress, Convert.ToInt32(port));
            //hello
        }
        SimpleTcpClient client;
        //txtUsername txtPassword1 txtPassword2 txtEmail
        //A VÁLASZ -> "register username,password1,email"        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string Username = txtUsername.Text;
            string pw1 = txtPassword1.Text;
            string pw2 = txtPassword2.Text;
            string email = txtEmail.Text;
            if (Username != "" && pw1 != "" && pw2 != "" && email != "" && pw1 == pw2)
            {                
                string uzenet = "register " + Username + "," + pw1 + "," + email;
                client.WriteLineAndGetReply(uzenet, TimeSpan.FromSeconds(0));
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            client.DataReceived += Client_DataReceived;
        }
        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string valasz = e.MessageString.Substring(0, e.MessageString.Length - 1);
            string command = valasz.Substring(0, valasz.IndexOf(" "));
            if (command == "register")
            {
                string eredmeny = valasz.Substring(valasz.IndexOf(" ")+1);
                if (eredmeny == "true")
                {     
                    MessageBox.Show("register successfulllllll");
                    Login f1 = new Login();
                    client.Disconnect();
                    client.Dispose();
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Close();
                    });
                    Application.Run(f1);
                }
                else
                    MessageBox.Show("register failed :(");
            }

        }
    }
}


