using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
namespace client
{
    public partial class Form1 : Form
    {
        int ID = 0;
        bool terminating = false;
        bool connected = false;
        bool notServer = false;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;

            int portNum;
            if (Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {


                    clientSocket.Connect(IP, portNum);
                   // button_connect.Enabled = false;
                    //textBox_message.Enabled = true;
                    // button_send.Enabled = true;
                    connected = true;
                    //Enable objects after successful connection
                   // nameText.Enabled = true;
                   // surnameText.Enabled = true;
                    usernameText.Enabled = true;
                   // passwordText.Enabled = true;
                   // createAccount.Enabled = true;
                   // disconnect.Enabled = true;



                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                logs.AppendText("Check the port\n");
            }
            //----------------
            bool isProblem = false;
            // string name = nameText.Text;
            //  string surName = surnameText.Text;
            string userName = usernameText.Text;
            // string password = passwordText.Text;

            if (connected)
            {
                if (userName == "")
                {
                    logs.AppendText("Pleas enter your username!\n");
                    isProblem = true;
                }


                string result = userName;
                if (result != "" && result.Length <= 256 && !isProblem)
                {
                    Byte[] buffer = Encoding.Default.GetBytes(result);
                    clientSocket.Send(buffer);
                }

            } 
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[8048];
                    clientSocket.Receive(buffer);
                    notServer = false;
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (incomingMessage == "Already connected")
                    {
                        logs.AppendText( usernameText.Text + " Has already connected to the server." + "\n");
                    }
                    if (incomingMessage == "Valid user name")
                    {
                        
                        
                        logs.AppendText("Hello "+usernameText.Text + "! You are connected to the server." + "\n");

                        disconnect.Enabled = true;
                        button_connect.Enabled = false;

                        posText.Enabled = true;
                        posText.Text = "";
                        send.Enabled = true;
                        button2.Enabled = true;

                        friendNameBox.Enabled = true;
                        addFriendButton.Enabled = true;

                        showAllFriends.Enabled = true;
                        friendPostButton.Enabled = true;

                        myPostsButton.Enabled = true;
                        postIDBox.Enabled = true;
                        deletePostButton.Enabled = true;

                        deleteFriendButton.Enabled = true;
                        removeFriendName.Enabled = true;
                        //connected = true;
                    }               
                    else if(incomingMessage == "Invalid user name")
                    {
                        logs.AppendText("Please enter a valid username"+ "\n");
                        //clientSocket.Close();
                        connected = false;
                    }
                    else if (incomingMessage[0] == '(')
                    {
                        string[] myFriends;
                        incomingMessage = incomingMessage.Substring(1);
                        myFriends = incomingMessage.Split('+');
                        myFriends = myFriends.Skip(1).ToArray();//delete first element
                        logs.AppendText("My Friends: " + "\n");
                        foreach (string a in myFriends)
                        {
                            logs.AppendText(a + "\n");
                        }

                    }
                    else if(incomingMessage[0] == '!')
                    {
                        if(incomingMessage[2] == '!')
                        {
                            incomingMessage = incomingMessage.Substring(3);
                            logs.AppendText("Showing all posts from ME: " + "\n");
                        }
                        else
                        {
                            if (incomingMessage[1] == '!')
                            {
                                incomingMessage = incomingMessage.Substring(2);
                                logs.AppendText("Showing all posts from FRIENDS: " + "\n");
                            }
                            else
                            {
                                incomingMessage = incomingMessage.Substring(1);
                                logs.AppendText("Showing all posts from clients: " + "\n");
                            }
                        }
                        
                        
                        
                        logs.AppendText(incomingMessage + "\n");
                    }else if(incomingMessage[0] == '%')
                    {
                        incomingMessage = incomingMessage.Substring(1);
                        logs.AppendText(incomingMessage+"\n");
                    }else if (incomingMessage[0] == '?')
                    {
                        incomingMessage = incomingMessage.Substring(1);
                        logs.AppendText(incomingMessage + "\n");
                    }
                    else if (incomingMessage[0] == '%')
                    {
                        incomingMessage = incomingMessage.Substring(1);
                        logs.AppendText(incomingMessage + "\n");
                    }else if (incomingMessage[0] == '|')
                    {
                        incomingMessage = incomingMessage.Substring(1);
                        logs.AppendText(incomingMessage + "\n");
                    }
                    else if (incomingMessage[0] == '-')
                    {
                        incomingMessage = incomingMessage.Substring(1);
                        logs.AppendText(incomingMessage + "\n");
                    }
                    else if (incomingMessage[0] == '*')
                    {
                        incomingMessage = incomingMessage.Substring(1);
                        logs.AppendText(incomingMessage + "\n");
                    }
                    /*
                    if (incomingMessage == "There is already an account with this username")
                    {
                        usernameText.Text = "";

                    }
                    logs.AppendText("Server: " + incomingMessage + "\n");*/
                }
                catch
                {
                    if (!terminating )
                    {
                        logs.AppendText("The server has disconnected\n");
                        button_connect.Enabled = true;

                       // nameText.Enabled = false;
                      //  surnameText.Enabled = false;
                      //  usernameText.Enabled = false;
                      //  passwordText.Enabled = false;
                      //  createAccount.Enabled = false;

                      //  createAccount.Text = "Create Account";
                      //  nameText.Text = "";
                     //   surnameText.Text = "";
                        //usernameText.Text = "";
                        button2.Enabled = false;
                        send.Enabled = false;
                        disconnect.Enabled = false;
                        posText.Enabled = false;
                        friendNameBox.Enabled = false;
                        addFriendButton.Enabled = false;
                        removeFriendName.Enabled = false;
                        deleteFriendButton.Enabled = false;
                        showAllFriends.Enabled = false;
                        postIDBox.Enabled = false;
                        deletePostButton.Enabled = false;
                        friendPostButton.Enabled = false;
                        myPostsButton.Enabled = false;
                        //   passwordText.Text = "";
                        //  createAccount.Text = "";

                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (connected)
            {
                if (usernameText.Text == "")
                {

                }
                else
                {
                    Byte[] buffer = Encoding.Default.GetBytes(usernameText.Text + " is " + "disconnected");
                    clientSocket.Send(buffer);
                } 
            }
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void createAccount_Click(object sender, EventArgs e)
        {
            bool isProblem = false;
           // string name = nameText.Text;
          //  string surName = surnameText.Text;
            string userName = usernameText.Text;
           // string password = passwordText.Text;
           
           
            if (userName == "")
            {
                logs.AppendText("Pleas enter your username!\n");
                isProblem = true;
            }


            string result = userName;
            if (result != "" && result.Length <= 256 && !isProblem)
            {
                Byte[] buffer = Encoding.Default.GetBytes(result);
                clientSocket.Send(buffer);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            int portNum;
            if (Int32.TryParse(textBox_port.Text, out portNum))
            {
                notServer = true;
                Byte[] buffer = Encoding.Default.GetBytes(usernameText.Text+" is "+"disconnected");
                clientSocket.Send(buffer);
                logs.AppendText("Successfuly disconnected\n");
                // Byte[] buffer = Encoding.Default.GetBytes("disconnect");
                // clientSocket.Send(buffer);
                 connected = false;
                // terminating = true;
              //  clientSocket.Close();
                disconnect.Enabled = false;
              //  nameText.Enabled = false;
               // surnameText.Enabled = false;
                //usernameText.Enabled = false;
              //  passwordText.Enabled = false;
               // createAccount.Enabled = false;
                button_connect.Enabled = true;
              //  createAccount.Text = "Create Account";
              //  nameText.Text = "";
             //   surnameText.Text = "";
                usernameText.Text = "";
                posText.Text = "";
                send.Enabled = false;
                button2.Enabled = false;
                posText.Enabled = false;
                //
                friendNameBox.Enabled = false;
                addFriendButton.Enabled = false;
                removeFriendName.Enabled = false;
                deleteFriendButton.Enabled = false;
                showAllFriends.Enabled = false;
                postIDBox.Enabled = false;
                deletePostButton.Enabled = false;
                friendPostButton.Enabled = false;
                myPostsButton.Enabled = false;
              //  passwordText.Text = "";
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string post = posText.Text;
            DateTime now = DateTime.Now;
            string time = now.ToString();
           
            int uniqueID = ID;
            string result = "*"+usernameText.Text + "/" + post + "/" + time;
            Byte[] buffer = Encoding.Default.GetBytes(result);
            clientSocket.Send(buffer);
            logs.AppendText(usernameText.Text+": "+post+"\n");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = "?" + usernameText.Text;
            Byte[] buffer = Encoding.Default.GetBytes(result);
            clientSocket.Send(buffer);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox1 = new ListBox();
        }

        private void addFriendButton_Click(object sender, EventArgs e)
        {
            string result5 = "%" + usernameText.Text+ " "+friendNameBox.Text;
            Byte[] buffer = Encoding.Default.GetBytes(result5);
            clientSocket.Send(buffer);
        }

        private void friendPostButton_Click(object sender, EventArgs e)
        {
            string result5 = "$" + usernameText.Text;
            Byte[] buffer = Encoding.Default.GetBytes(result5);
            clientSocket.Send(buffer);
        }

        private void showAllFriends_Click(object sender, EventArgs e)
        {
            string result5 = "(" + usernameText.Text;
            Byte[] buffer = Encoding.Default.GetBytes(result5);
            clientSocket.Send(buffer);
        }

        private void myPostsButton_Click(object sender, EventArgs e)
        {
            string result5 = "£" + usernameText.Text;
            Byte[] buffer = Encoding.Default.GetBytes(result5);
            clientSocket.Send(buffer);
        }

        private void deletePostButton_Click(object sender, EventArgs e)
        {
            string result5 = "-" + usernameText.Text+" " +postIDBox.Text;
            Byte[] buffer = Encoding.Default.GetBytes(result5);
            clientSocket.Send(buffer);
        }

        private void deleteFriendButton_Click(object sender, EventArgs e)
        {
            string result5 = "{" + usernameText.Text + " " + removeFriendName.Text;
            Byte[] buffer = Encoding.Default.GetBytes(result5);
            clientSocket.Send(buffer);
        }
    }
}
