namespace client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.usernameText = new System.Windows.Forms.TextBox();
            this.disconnect = new System.Windows.Forms.Button();
            this.send = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.posText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.friendPostButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.deletePostButton = new System.Windows.Forms.Button();
            this.postIDBox = new System.Windows.Forms.TextBox();
            this.deleteFriendButton = new System.Windows.Forms.Button();
            this.addFriendButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.friendNameBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.removeFriendName = new System.Windows.Forms.TextBox();
            this.myPostsButton = new System.Windows.Forms.Button();
            this.showAllFriends = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(58, 28);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(88, 20);
            this.textBox_ip.TabIndex = 2;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(196, 28);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(88, 20);
            this.textBox_port.TabIndex = 3;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(104, 130);
            this.button_connect.Margin = new System.Windows.Forms.Padding(2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(88, 30);
            this.button_connect.TabIndex = 4;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(331, 62);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(326, 289);
            this.logs.TabIndex = 5;
            this.logs.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Username:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // usernameText
            // 
            this.usernameText.Location = new System.Drawing.Point(104, 90);
            this.usernameText.Margin = new System.Windows.Forms.Padding(2);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(88, 20);
            this.usernameText.TabIndex = 15;
            // 
            // disconnect
            // 
            this.disconnect.BackColor = System.Drawing.SystemColors.Control;
            this.disconnect.Enabled = false;
            this.disconnect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.disconnect.Location = new System.Drawing.Point(455, 355);
            this.disconnect.Margin = new System.Windows.Forms.Padding(2);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(88, 34);
            this.disconnect.TabIndex = 18;
            this.disconnect.Text = "Disconnect";
            this.disconnect.UseVisualStyleBackColor = false;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // send
            // 
            this.send.Enabled = false;
            this.send.Location = new System.Drawing.Point(190, 430);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(85, 26);
            this.send.TabIndex = 19;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(331, 443);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 26);
            this.button2.TabIndex = 20;
            this.button2.Text = "All Posts";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // posText
            // 
            this.posText.Enabled = false;
            this.posText.Location = new System.Drawing.Point(84, 436);
            this.posText.Name = "posText";
            this.posText.Size = new System.Drawing.Size(100, 20);
            this.posText.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 443);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Post:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // friendPostButton
            // 
            this.friendPostButton.Enabled = false;
            this.friendPostButton.Location = new System.Drawing.Point(458, 443);
            this.friendPostButton.Name = "friendPostButton";
            this.friendPostButton.Size = new System.Drawing.Size(85, 26);
            this.friendPostButton.TabIndex = 24;
            this.friendPostButton.Text = "Friend Posts";
            this.friendPostButton.UseVisualStyleBackColor = true;
            this.friendPostButton.Click += new System.EventHandler(this.friendPostButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 490);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Post ID:";
            this.label4.Click += new System.EventHandler(this.label4_Click_1);
            // 
            // deletePostButton
            // 
            this.deletePostButton.Enabled = false;
            this.deletePostButton.Location = new System.Drawing.Point(190, 477);
            this.deletePostButton.Name = "deletePostButton";
            this.deletePostButton.Size = new System.Drawing.Size(85, 26);
            this.deletePostButton.TabIndex = 26;
            this.deletePostButton.Text = "Delete Post";
            this.deletePostButton.UseVisualStyleBackColor = true;
            this.deletePostButton.Click += new System.EventHandler(this.deletePostButton_Click);
            // 
            // postIDBox
            // 
            this.postIDBox.Enabled = false;
            this.postIDBox.Location = new System.Drawing.Point(84, 483);
            this.postIDBox.Name = "postIDBox";
            this.postIDBox.Size = new System.Drawing.Size(100, 20);
            this.postIDBox.TabIndex = 27;
            // 
            // deleteFriendButton
            // 
            this.deleteFriendButton.Enabled = false;
            this.deleteFriendButton.Location = new System.Drawing.Point(208, 293);
            this.deleteFriendButton.Name = "deleteFriendButton";
            this.deleteFriendButton.Size = new System.Drawing.Size(94, 26);
            this.deleteFriendButton.TabIndex = 28;
            this.deleteFriendButton.Text = "Remove Friend";
            this.deleteFriendButton.UseVisualStyleBackColor = true;
            this.deleteFriendButton.Click += new System.EventHandler(this.deleteFriendButton_Click);
            // 
            // addFriendButton
            // 
            this.addFriendButton.Enabled = false;
            this.addFriendButton.Location = new System.Drawing.Point(208, 238);
            this.addFriendButton.Name = "addFriendButton";
            this.addFriendButton.Size = new System.Drawing.Size(85, 26);
            this.addFriendButton.TabIndex = 29;
            this.addFriendButton.Text = "Add Friend";
            this.addFriendButton.UseVisualStyleBackColor = true;
            this.addFriendButton.Click += new System.EventHandler(this.addFriendButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 238);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Friend name:";
            this.label5.Click += new System.EventHandler(this.label5_Click_1);
            // 
            // friendNameBox
            // 
            this.friendNameBox.Enabled = false;
            this.friendNameBox.Location = new System.Drawing.Point(92, 238);
            this.friendNameBox.Name = "friendNameBox";
            this.friendNameBox.Size = new System.Drawing.Size(100, 20);
            this.friendNameBox.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 293);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Friend name:";
            // 
            // removeFriendName
            // 
            this.removeFriendName.Enabled = false;
            this.removeFriendName.Location = new System.Drawing.Point(92, 293);
            this.removeFriendName.Name = "removeFriendName";
            this.removeFriendName.Size = new System.Drawing.Size(100, 20);
            this.removeFriendName.TabIndex = 33;
            // 
            // myPostsButton
            // 
            this.myPostsButton.Enabled = false;
            this.myPostsButton.Location = new System.Drawing.Point(572, 443);
            this.myPostsButton.Name = "myPostsButton";
            this.myPostsButton.Size = new System.Drawing.Size(85, 26);
            this.myPostsButton.TabIndex = 34;
            this.myPostsButton.Text = "My Posts";
            this.myPostsButton.UseVisualStyleBackColor = true;
            this.myPostsButton.Click += new System.EventHandler(this.myPostsButton_Click);
            // 
            // showAllFriends
            // 
            this.showAllFriends.Enabled = false;
            this.showAllFriends.Location = new System.Drawing.Point(92, 355);
            this.showAllFriends.Name = "showAllFriends";
            this.showAllFriends.Size = new System.Drawing.Size(110, 26);
            this.showAllFriends.TabIndex = 35;
            this.showAllFriends.Text = "Show all friends";
            this.showAllFriends.UseVisualStyleBackColor = true;
            this.showAllFriends.Click += new System.EventHandler(this.showAllFriends_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 549);
            this.Controls.Add(this.showAllFriends);
            this.Controls.Add(this.myPostsButton);
            this.Controls.Add(this.removeFriendName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.friendNameBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.addFriendButton);
            this.Controls.Add(this.deleteFriendButton);
            this.Controls.Add(this.postIDBox);
            this.Controls.Add(this.deletePostButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.friendPostButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.posText);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.send);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.usernameText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox usernameText;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox posText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button friendPostButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deletePostButton;
        private System.Windows.Forms.TextBox postIDBox;
        private System.Windows.Forms.Button deleteFriendButton;
        private System.Windows.Forms.Button addFriendButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox friendNameBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox removeFriendName;
        private System.Windows.Forms.Button myPostsButton;
        private System.Windows.Forms.Button showAllFriends;
    }
}

