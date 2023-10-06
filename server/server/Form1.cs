using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class Form1 : Form
    {
        IDictionary<Socket, string> socketsAndUsers = new Dictionary<Socket, string>(); // Dictionary to send messages.
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        int ID;
        bool terminating = false;
        bool listening = false;
       
        List<string> connectedUsers = new List<string>();
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if(Int32.TryParse(textBox_port.Text, out serverPort))
            {
                //First create the end point. Our local server is our IP.
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint); // Then bind the socket with our serverSocket
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;
                //textBox_message.Enabled = true;
               // button_send.Enabled = true;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while(listening)
            {
                try
                {
                  
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                   // logs.AppendText("A client is connected.\n");

                    Thread receiveThread = new Thread(() => Receive(newClient)); // updated
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient) // updated
        {
            // int lineCount = File.ReadLines(@"../../posts.txt").Count();
            bool deleteFriend = false;
            bool deletePost = false;
            bool showMyPosts = false;
            bool showFriends = false;
            bool showFriendsPosts = false;
            bool connected = true;
            bool test = false;
            bool notUser = false;
            bool comingPost = false;
            bool allPosts = false;
            bool addFriend = false;
            while (connected && !terminating)
            {
                try
                {
                   /* string lastLine;
                    //Finding the last line of the post file to give a unique id.
                    int lineCount = File.ReadLines(@"C:\file.txt").Count();
                    logs.AppendText("Line ount: "+lineCount + "\n");
                    if (lineCount > 0)
                    {
                         lastLine = File.ReadLines(@"../../posts.txt").Last();
                        string[] a1 = lastLine.Split('/');
                        
                        string id = a1[3];
                        int lastId = Int16.Parse(id);
                        lastId++;
                        ID = lastId;
                    }
                    else
                    {
                        ID = 0;
                    }
                    logs.AppendText("ID: " + ID + "\n");*/
                    Byte[] buffer = new Byte[8048];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (incomingMessage.Length > 13 && incomingMessage.Substring(incomingMessage.Length-12) == "disconnected")
                    {
                        logs.AppendText(incomingMessage + "\n");
                        string userName = incomingMessage.Substring(0,incomingMessage.Length - 16);
                        connectedUsers.Remove(userName);
                        socketsAndUsers.Remove(thisClient);
                        notUser = true;
                    }
                    if (incomingMessage[0] == '{')//Delete Friend.
                    {
                        deleteFriend = true;
                        string myName = incomingMessage.Substring(1).Split(' ')[0];
                        string friendName = incomingMessage.Substring(1).Split(' ')[1];
                        bool isMyFriend = false;
                        string[] myInitialFriends = { };
                        string[] myInitialFriends2 = { };
                        foreach (string line in System.IO.File.ReadLines(@"../../friends.txt"))
                        {
                            string[] allFriends = line.Split('+');
                            allFriends = allFriends.Skip(1).ToArray();
                            if (myName ==line.Split('+')[0] && allFriends.Contains(friendName))
                            {
                                isMyFriend = true;
                                myInitialFriends = allFriends;
                            }
                        }

                        foreach (string line in System.IO.File.ReadLines(@"../../friends.txt"))
                        {
                            string[] allFriends = line.Split('+');
                            allFriends = allFriends.Skip(1).ToArray();
                            if (friendName == line.Split('+')[0] && allFriends.Contains(myName))
                            {
                               // isMyFriend = true;
                                myInitialFriends2 = allFriends;
                            }
                        }
                        if (myName == friendName)
                        {
                            logs.AppendText(myName + " you cannot delete your self" +"\n");
                            Byte[] buffer3 = Encoding.Default.GetBytes("%" + myName + " you cannot delete your self");
                            thisClient.Send(buffer3);
                        }
                        else if (!isMyFriend)
                        {
                            logs.AppendText(myName + " you dont have friend called: " + friendName + "\n");
                            Byte[] buffer3 = Encoding.Default.GetBytes("%" + myName + " you dont have friend called: " + friendName);
                            thisClient.Send(buffer3);

                           
                        }
                        else
                        {
                            myInitialFriends = myInitialFriends.Where(val => val != friendName).ToArray();
                            string myNewFriends = "";
                            
                            foreach(string a in myInitialFriends)
                            {
                                myNewFriends += "+"+a;
                            }
                            var tempFile = Path.GetTempFileName();
                            var linesToKeep = File.ReadLines(@"../../friends.txt").Where(l =>
                            l.Split('+')[0] != myName);

                            File.WriteAllLines(tempFile, linesToKeep);

                            File.Delete(@"../../friends.txt");
                            File.Move(tempFile, @"../../friends.txt");

                            logs.AppendText(myName + " you have successfuly delete friend called: " + friendName + "\n");
                            Byte[] buffer3 = Encoding.Default.GetBytes("%" + myName + " you have successfuly delete friend called: " + friendName);
                            thisClient.Send(buffer3);

                            bool isOnline = false;
                            foreach (var a in socketsAndUsers)
                            {
                                if (a.Value == friendName)
                                {
                                    isOnline = true;
                                    Byte[] buffer8 = Encoding.Default.GetBytes("-" + friendName + ": NEW MESSAGE: " + myName + " removed you as a friend!");

                                    a.Key.Send(buffer8);
                                }
                            }


                            if (!isOnline)
                            {
                                using (StreamWriter file = new StreamWriter("../../messages.txt", append: true))
                                {
                                    file.WriteLine(friendName + ": NEW MESSAGE: " + myName + " removed you as a friend!");
                                }
                            }


                            //For friend
                            myInitialFriends2 = myInitialFriends2.Where(val => val != myName).ToArray();
                            string myNewFriends2 = "";
                            foreach (string a in myInitialFriends2)
                            {
                                myNewFriends2 +="+" + a;
                            }
                            var tempFile2 = Path.GetTempFileName();
                            var linesToKeep2 = File.ReadLines(@"../../friends.txt").Where(l =>
                            l.Split('+')[0] != friendName);

                            File.WriteAllLines(tempFile2, linesToKeep2);

                            File.Delete(@"../../friends.txt");
                            File.Move(tempFile2, @"../../friends.txt");

                            //other
                            string result = myName + myNewFriends;
                            using (StreamWriter file = new StreamWriter("../../friends.txt", append: true))
                            {
                                file.WriteLine(result);
                            }
                            string result2 = friendName + myNewFriends2;
                            using (StreamWriter file = new StreamWriter("../../friends.txt", append: true))
                            {
                                file.WriteLine(result2);
                            }

                            bool isMyNameAlone = false;
                            bool isFriendAlone = false;
                            //Look myName is single or not
                            foreach (string line in System.IO.File.ReadLines(@"../../friends.txt"))
                            {
                                string[] allFriends = line.Split('+');
                                allFriends = allFriends.Skip(1).ToArray();
                                if (myName == line.Split('+')[0])
                                {
                                    if (allFriends.Length == 0)
                                    {
                                        isMyNameAlone = true;
                                    }
                                }
                            }
                            if (isMyNameAlone)
                            {
                                //logs.AppendText(myName + " is single\n");
                                var tempFile3 = Path.GetTempFileName();
                                var linesToKeep3 = File.ReadLines(@"../../friends.txt").Where(l =>
                                l.Contains('+'));

                                File.WriteAllLines(tempFile3, linesToKeep3);

                                File.Delete(@"../../friends.txt");
                                File.Move(tempFile3, @"../../friends.txt");
                            }
                           
                              foreach (string line in System.IO.File.ReadLines(@"../../friends.txt"))
                              {
                                  string[] allFriends = line.Split('+');
                                  allFriends = allFriends.Skip(1).ToArray();
                                  if (friendName == line.Split('+')[0])
                                  {
                                      if (allFriends.Length == 0)
                                      {
                                        isFriendAlone = true;
                                      }
                                  }
                              }
                            if (isFriendAlone)
                            {
                              //  logs.AppendText(friendName + " is single\n");
                                var tempFile4 = Path.GetTempFileName();
                                var linesToKeep4 = File.ReadLines(@"../../friends.txt").Where(l =>
                                l.Contains("+"));

                                File.WriteAllLines(tempFile4, linesToKeep4);

                                File.Delete(@"../../friends.txt");
                                File.Move(tempFile4, @"../../friends.txt");
                            }
                        }



                    }
                    if (incomingMessage[0] == '-')//Delete Post.
                    {
                        deletePost = true;
                        string lineDelete = "";
                        string myName = incomingMessage.Substring(1).Split(' ')[0];
                        string postID = incomingMessage.Substring(1).Split(' ')[1];
                        bool isMyPost = true;
                        bool isPostIdExist = false;
                        foreach (string line in System.IO.File.ReadLines(@"../../posts.txt"))
                        {

                            if (line.Split('/')[3] == postID && line.Split('/')[0] != myName)
                            {
                                isMyPost = false;
                            }
                            if (line.Split('/')[3] == postID)
                            {
                                isPostIdExist = true;
                            }
                        }
                        if (!isPostIdExist)
                        {
                            logs.AppendText("There is no post with id: " + postID + " " + myName + "\n");
                            Byte[] buffer3 = Encoding.Default.GetBytes("?" + "There is no post with id: " + postID + " " + myName);
                            thisClient.Send(buffer3);
                        }
                        else
                        {


                            if (isMyPost)
                            {
                                var tempFile = Path.GetTempFileName();
                                var linesToKeep = File.ReadLines(@"../../posts.txt").Where(l =>
                                l.Split('/')[3] != postID);

                                File.WriteAllLines(tempFile, linesToKeep);

                                File.Delete(@"../../posts.txt");
                                File.Move(tempFile, @"../../posts.txt");
                                logs.AppendText("Post id " + postID + " is deleted " + myName + "\n");
                                Byte[] buffer3 = Encoding.Default.GetBytes("?" + "Post id " + postID + " is deleted " + myName);
                                thisClient.Send(buffer3);
                            }
                            else
                            {
                                logs.AppendText("You can only delete your own posts " + myName + "\n");
                                Byte[] buffer3 = Encoding.Default.GetBytes("?" + "You can only delete your own posts " + myName);
                                thisClient.Send(buffer3);
                                
                            }


                        }
                    }
                    if (incomingMessage[0] == '£')//Show my posts.
                    {
                            
                        showMyPosts = true;
                        string finalResult = "";
                        string username1 = incomingMessage.Substring(1);
                        logs.AppendText("Showed SELF messages for " + username1 + "\n");
                        foreach (string line in System.IO.File.ReadLines(@"../../posts.txt"))
                        {

                            string allPost = line;

                            string[] a1 = allPost.Split('/');
                            string username2 = a1[0];
                            string post = a1[1];
                            string time = a1[2];
                            string id = a1[3];
                            if (username2 == username1) // If the username is existed in database
                            {
                                finalResult += "Username: " + username2 + "\n" +
                                               "PostID: " + id + "\n" +
                                               "Post: " + post + "\n" +
                                               "Time: " + time + "\n";


                            }


                        }
                        Byte[] buffer3 = Encoding.Default.GetBytes("!!!" + finalResult);
                        thisClient.Send(buffer3);
                       // allPosts = true;
                    }
                    

                        if (incomingMessage[0] == '(')//Show friends.
                    {
                        showFriends = true;
                        string[] myFriends;
                        string myFriendsLine = "";
                        string myName = incomingMessage.Substring(1);
                        logs.AppendText("Showed friends for " + myName + "\n");
                        foreach (string line in System.IO.File.ReadLines(@"../../friends.txt"))
                        {
                            
                            
                            

                            if (line.Split('+')[0] == myName)
                            {
                               
                                myFriendsLine = line;
                            }
                        }
                        
                        myFriends = myFriendsLine.Split('+');
                        myFriends = myFriends.Skip(1).ToArray();//delete first element
                        Byte[] buffer3 = Encoding.Default.GetBytes("(" + myFriendsLine);
                        thisClient.Send(buffer3);


                    }
                        if (incomingMessage[0] == '$')//Show friend's posts only.
                    {
                        showFriendsPosts = true;
                        bool hasFriend = false;
                        string[] myFriends;
                        string myFriendsLine = "";
                        string myName = incomingMessage.Substring(1);
                        
                        foreach (string line in System.IO.File.ReadLines(@"../../friends.txt"))
                        {
                            if (line.Split('+')[0] == myName)
                            {
                                hasFriend = true;
                                myFriendsLine = line;
                            }
                        }
                        if (!hasFriend)
                        {
                            logs.AppendText(myName + " has no friends!\n");
                            Byte[] buffer3 = Encoding.Default.GetBytes("*" + myName + ", you don't have any friends!");
                            thisClient.Send(buffer3);
                        }
                        else
                        {
                            string finalResult = "";
                            myFriends = myFriendsLine.Split('+');
                            myFriends = myFriends.Skip(1).ToArray();//delete first element
                         //   logs.AppendText(myFriendsLine + "\n");
                            logs.AppendText("Showed all FRIENDS messages for " + myName + "\n");
                            foreach (string line in System.IO.File.ReadLines(@"../../posts.txt"))
                            {

                                string allPost = line;

                                string[] a1 = allPost.Split('/');
                                string username2 = a1[0];
                                string post = a1[1];
                                string time = a1[2];
                                string id = a1[3];
                                if (myFriends.Contains(username2)) // If the username is existed in database
                                {
                                    finalResult += "Username: " + username2 + "\n" +
                                                   "PostID: " + id + "\n" +
                                                   "Post: " + post + "\n" +
                                                   "Time: " + time + "\n";


                                }


                            }
                            //logs.AppendText(finalResult + "\n");
                            Byte[] buffer3 = Encoding.Default.GetBytes("!!" + finalResult);
                            thisClient.Send(buffer3);

                        }



                    }


                        if (incomingMessage[0] == '%')//Adding friend
                    {

                        bool isFriendUser = false;
                        addFriend = true;
                        bool ismyNameE = false;
                        bool isfriendNameE = false;
                        string coming = incomingMessage.Substring(1);
                        string[] coming2 = coming.Split(' ');
                        string myName = coming2[0];
                        string friendName = coming2[1];
                        if (myName == friendName)
                        {
                            logs.AppendText(myName + " you cannot add yourself as a friend!!\n");
                            Byte[] buffer5 = Encoding.Default.GetBytes("%" + myName + " you cannot add yourself as a friend!!");
                            thisClient.Send(buffer5);
                        }
                        else
                        {
                            foreach (string line in System.IO.File.ReadLines(@"../../user-db.txt"))
                            {
                                if (line == friendName)
                                {
                                    isFriendUser = true;
                                }
                            }
                            if (!isFriendUser)
                            {
                                logs.AppendText(friendName + " is not in the user database!!\n");
                                
                                Byte[] buffer5 = Encoding.Default.GetBytes("%" + myName + " you cannot add "+friendName + ",it is not in the user database!!");
                                thisClient.Send(buffer5);
                            }
                            else
                            {
                                string existedLine1 = "";
                                string existedLine2 = "";
                                /* //******************
                                 var tempFile = Path.GetTempFileName();
                                 var linesToKeep = File.ReadLines(@"../../friends.txt").Where(l => 
                                 l.Split('+')[0] != myName);

                                 File.WriteAllLines(tempFile, linesToKeep);

                                 File.Delete(@"../../friends.txt");
                                 File.Move(tempFile, @"../../friends.txt");
                                 //***********************/
                                foreach (string line in System.IO.File.ReadLines(@"../../friends.txt"))
                                {
                                    string[] myLine = line.Split('+');
                                    string name = myLine[0];

                                    if (name == myName)
                                    {
                                        ismyNameE = true;
                                        existedLine1 = line;
                                        logs.AppendText(myName + " is existed in friends.txt!!\n");
                                    }
                                    if (name == friendName)
                                    {
                                        isfriendNameE = true;
                                        existedLine2 = line;
                                        logs.AppendText(friendName + " is existed in friends.txt!!\n");
                                    }
                                }
                                //For myName s old friends
                                string[] mustAddFriends = existedLine1.Split('+');
                                mustAddFriends = mustAddFriends.Skip(1).ToArray();//delete first element
                                string mustAddFriends2 = "";
                                foreach (string a in mustAddFriends)
                                {
                                    mustAddFriends2 += '+' + a;
                                }
                                //For friends old friends
                                string[] mustAddFriends3 = existedLine2.Split('+');
                                mustAddFriends3 = mustAddFriends3.Skip(1).ToArray();//delete first element
                                string mustAddFriends4 = "";
                                foreach (string a in mustAddFriends3)
                                {
                                    mustAddFriends4 += '+' + a;
                                }
                                if (ismyNameE) // if my name existed  First delete that line, after that add the correct line.
                                {
                                    var tempFile = Path.GetTempFileName();
                                    var linesToKeep = File.ReadLines(@"../../friends.txt").Where(l =>
                                    l.Split('+')[0] != myName);

                                    File.WriteAllLines(tempFile, linesToKeep);

                                    File.Delete(@"../../friends.txt");
                                    File.Move(tempFile, @"../../friends.txt");

                                    string result = myName + "+" + friendName + mustAddFriends2;
                                    using (StreamWriter file = new StreamWriter("../../friends.txt", append: true))
                                    {
                                        file.WriteLine(result);
                                    }

                                }
                                else // if my name doesnt exist, ı how to only append the line(myName+friendName)
                                {
                                    string result = myName + "+" + friendName;
                                    using (StreamWriter file = new StreamWriter("../../friends.txt", append: true))
                                    {
                                        file.WriteLine(result);
                                    }
                                }
                                //Doing same operation for friends name
                                if (isfriendNameE) // if my name existed  First delete that line, after that add the correct line.
                                {
                                    var tempFile = Path.GetTempFileName();
                                    var linesToKeep = File.ReadLines(@"../../friends.txt").Where(l =>
                                    l.Split('+')[0] != friendName);

                                    File.WriteAllLines(tempFile, linesToKeep);

                                    File.Delete(@"../../friends.txt");
                                    File.Move(tempFile, @"../../friends.txt");

                                    string result = friendName + "+" + myName + mustAddFriends4;
                                    using (StreamWriter file = new StreamWriter("../../friends.txt", append: true))
                                    {
                                        file.WriteLine(result);
                                    }

                                }
                                else // if my name doesnt exist, ı how to only append the line(myName+friendName)
                                {
                                    string result = friendName + "+" + myName;
                                    using (StreamWriter file = new StreamWriter("../../friends.txt", append: true))
                                    {
                                        file.WriteLine(result);
                                    }
                                }

                                logs.AppendText(myName + " added " + friendName + " as a friend.\n");
                                Byte[] buffer5 = Encoding.Default.GetBytes("%" + myName + " successfully added " + friendName + " as a friend!");
                                thisClient.Send(buffer5);
                             /*   logs.AppendText("Connected users: \n");
                                foreach (var a in socketsAndUsers)
                                {
                                    logs.AppendText(a.Value+"\n");
                                }*/
                                bool isOnline = false;
                                foreach (var a in socketsAndUsers)
                                {
                                    if(connectedUsers.Contains(friendName) && a.Value == friendName)
                                    {
                                        isOnline = true;
                                        Byte[] buffer8 = Encoding.Default.GetBytes("-"+friendName + ": NEW MESSAGE- " + myName + " added you as a friend!");

                                        a.Key.Send(buffer8);
                                    }
                                }


                                if (!isOnline)
                                {
                                    using (StreamWriter file = new StreamWriter("../../messages.txt", append: true))
                                    {
                                        file.WriteLine(friendName + ": NEW MESSAGE: " + myName + " added you as a friend!");
                                    }
                                }
                                


                            }
                        }
                    }



                    if(incomingMessage[0] == '*')
                    {

                      //  ID++;
                         string lastLine;
                        //Finding the last line of the post file to give a unique id.
                        bool existed = File.Exists(@"../../posts.txt");

                      //  logs.AppendText("Line ount: " + existed + "\n");
                        
                   
                    if (existed )
                    {
                            int lineCount = File.ReadLines(@"../../posts.txt").Count();
                          //  logs.AppendText("Line ount: " + lineCount + "\n");
                            lastLine = File.ReadLines(@"../../posts.txt").Last();
                            if (lineCount > 0 && lastLine.Length > 5)
                            {
                                lastLine = File.ReadLines(@"../../posts.txt").Last();
                                string[] b = lastLine.Split('/');

                                string id2 = b[3];
                                int lastId = Int16.Parse(id2);
                                lastId++;
                                ID = lastId;
                            }
                            else
                            {
                                ID = 1;
                            }
                        
                    }
                    else if(!existed)
                    {
                        ID = 1;
                    }
                  //  logs.AppendText("ID: " + ID + "\n");
                        incomingMessage = incomingMessage + "/" + ID;
                        using (StreamWriter file = new StreamWriter("../../posts.txt", append: true))
                        {
                            //string lastLine = File.ReadLines("../../posts.txt").Last();
                            //Console.WriteLine(lastLine);
                           // logs.AppendText("last line:"+lastLine + "\n");
                         //   if (lastLine == "")
                         //   {
                                file.WriteLine(incomingMessage.Substring(1));
                          //  }else if(lastLine != "")
                          /*  {
                                //lastLine = File.ReadLines("../../posts.txt").Last();
                                string[] last1 = lastLine.Split('/');
                                string idLast = last1[3];
                                int x = Int32.Parse(idLast) + 1;
                                string idUnique = x.ToString() ;
                                file.WriteLine(incomingMessage.Substring(1) + '/' + idUnique);
                            }*/
                            
                        }
                        comingPost = true;
                        string a = incomingMessage.Substring(1);
                        string[] a1 = a.Split('/');
                        string username = a1[0];
                        string post = a1[1];
                        string time = a1[2];
                        string id = a1[3];
                        logs.AppendText(username+" has sent a post:" + "\n");
                        logs.AppendText(post + "\n");


                    }
                    if(incomingMessage[0] == '?') // Show all posts
                    {
                        allPosts = true;
                        string finalResult= "";
                        string username1 = incomingMessage.Substring(1);
                        logs.AppendText("Showed all messages for "+username1 + "\n");
                        foreach (string line in System.IO.File.ReadLines(@"../../posts.txt"))
                        {
                            
                            string allPost = line;

                             string[] a1 = allPost.Split('/');
                             string username2 = a1[0];
                            string post = a1[1];
                            string time = a1[2];
                            string id = a1[3];
                            if (username1 != username2) // If the username is existed in database
                            {
                                finalResult += "Username: " + username2 + "\n" +
                                               "PostID: " + id + "\n" +
                                               "Post: " + post + "\n" +
                                               "Time: " + time + "\n";


                            }


                        }
                       Byte[] buffer3 = Encoding.Default.GetBytes("!"+finalResult);
                        thisClient.Send(buffer3);
                        
                    }
                    if (!notUser && !comingPost && !allPosts && !addFriend && !showFriendsPosts && !showFriends && !showMyPosts && !deletePost && !deleteFriend)
                    {
                        //We have to check that the username is already existed or not.
                        //  string phrase = incomingMessage;
                        // string[] words = phrase.Split(' ');
                        string username1 = incomingMessage;
                        bool isExisted = false;
                        Byte[] buffer2;
                        Byte[] buffer5;
                        //We are now looking for that username if it is existed in user-db
                        foreach (string line in System.IO.File.ReadLines(@"../../user-db.txt"))
                        {

                            string username2 = line;
                            // string[] a1 = a.Split(' ');
                            //string username2 = a1[0];

                            if (username1 == username2 ) // If the username is existed in database
                            {
                                if (connectedUsers.Contains(username1))
                                {
                                    logs.AppendText(username1 + " has already connected to the server.\n");
                                    buffer5 = Encoding.Default.GetBytes("Already connected");
                                    thisClient.Send(buffer5);
                                    isExisted = true;
                                }
                                else
                                {
                                    buffer2 = Encoding.Default.GetBytes("Valid user name");
                                    thisClient.Send(buffer2);
                                    logs.AppendText(username1 + " has connected to the server!\n");
                                    
                                    //Sending the old message when user has logged in.
                                    bool hasMessage = false;
                                    foreach (string line2 in System.IO.File.ReadLines(@"../../messages.txt"))
                                    {
                                        logs.AppendText(line2+"\n");
                                        if(line2.Split(':')[0]== username1)
                                        {
                                            logs.AppendText(line2 + "\n");
                                            buffer5 = Encoding.Default.GetBytes("|"+line2);
                                            thisClient.Send(buffer5);
                                            hasMessage = true;
                                            
                                        }
                                        


                                    }
                                    if (hasMessage)
                                    {
                                        var tempFile = Path.GetTempFileName();
                                        var linesToKeep = File.ReadLines(@"../../messages.txt").Where(l =>
                                        l.Split(':')[0] != username1);

                                        File.WriteAllLines(tempFile, linesToKeep);

                                        File.Delete(@"../../messages.txt");
                                        File.Move(tempFile, @"../../messages.txt");
                                    }


                                    /* buffer2 = Encoding.Default.GetBytes("There is already an account with this username");
                                     isExisted = true;
                                     thisClient.Send(buffer2);
                                     logs.AppendText(username1 + " cannot create an account since this username is used!\n");*/
                                    connectedUsers.Add(username1);
                                    socketsAndUsers.Add(thisClient, username1);
                                    isExisted = true;
                                    test = true;
                                    break;
                                }

                               

                            }


                        }
                        if (!isExisted)
                        {
                            buffer2 = Encoding.Default.GetBytes("Invalid user name");
                            thisClient.Send(buffer2);
                            logs.AppendText(username1 + " tried to connect to the server but cannot\n");
                            // logs.AppendText(username1 + " has logged in!\n");
                        }

                        /*  if (!isExisted)
                          {
                              buffer2 = Encoding.Default.GetBytes("You have created an account!\n");
                              thisClient.Send(buffer2);
                              logs.AppendText(username1 + " has created an account!\n");


                              using (StreamWriter file = new StreamWriter("../../database.txt", append: true))
                              {
                                  file.WriteLine(incomingMessage);
                              }
                             // logs.AppendText("Client: " + incomingMessage + "\n");
                          }*/
                    }
                }
                catch
                {
                    if (!terminating && test)
                    {
                       // logs.AppendText("A client has disconnected!\n");
                    }
                    thisClient.Close();
                   // socketsAndUsers.Remove(thisClient);
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

      /*  private void button_send_Click(object sender, EventArgs e)
        {
            string message = textBox_message.Text;
            if(message != "" && message.Length <= 64)
            {
                Byte[] buffer = Encoding.Default.GetBytes(message);
                foreach (Socket client in clientSockets)
                {
                    try
                    {
                        client.Send(buffer);
                    }
                    catch
                    {
                        logs.AppendText("There is a problem! Check the connection...\n");
                        terminating = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                        textBox_port.Enabled = true;
                        button_listen.Enabled = true;
                        serverSocket.Close();
                    }

                }
            }
        }*/
    }
}


/* Helpful and useful for assignments or projects:

             var lines = File.ReadLines(@"../../database.txt");



            using (StreamWriter file = new StreamWriter("../../database.txt", append: true))
            {
               file.WriteLine(x + "-" + y );
            }
*/
