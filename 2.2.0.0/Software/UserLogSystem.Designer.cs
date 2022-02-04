namespace Software
{
    partial class UserLogSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserLogSystem));
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.btn_LogInOut = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cbx_Users = new System.Windows.Forms.ComboBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Keyboard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Keyboard)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(13, -44);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(506, 217);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 9;
            this.pictureBox8.TabStop = false;
            // 
            // btn_LogInOut
            // 
            this.btn_LogInOut.Location = new System.Drawing.Point(153, 231);
            this.btn_LogInOut.Name = "btn_LogInOut";
            this.btn_LogInOut.Size = new System.Drawing.Size(104, 37);
            this.btn_LogInOut.TabIndex = 10;
            this.btn_LogInOut.Text = "Log In";
            this.btn_LogInOut.UseVisualStyleBackColor = true;
            this.btn_LogInOut.Click += new System.EventHandler(this.btn_LogInOut_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Location = new System.Drawing.Point(263, 231);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(104, 37);
            this.btn_Cancel.TabIndex = 11;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // cbx_Users
            // 
            this.cbx_Users.FormattingEnabled = true;
            this.cbx_Users.Location = new System.Drawing.Point(246, 133);
            this.cbx_Users.Name = "cbx_Users";
            this.cbx_Users.Size = new System.Drawing.Size(121, 24);
            this.cbx_Users.TabIndex = 12;
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(246, 163);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(121, 22);
            this.txt_Password.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "User";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Password";
            // 
            // btn_Keyboard
            // 
            this.btn_Keyboard.Image = ((System.Drawing.Image)(resources.GetObject("btn_Keyboard.Image")));
            this.btn_Keyboard.Location = new System.Drawing.Point(460, 231);
            this.btn_Keyboard.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Keyboard.Name = "btn_Keyboard";
            this.btn_Keyboard.Size = new System.Drawing.Size(59, 37);
            this.btn_Keyboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Keyboard.TabIndex = 16;
            this.btn_Keyboard.TabStop = false;
            this.btn_Keyboard.Click += new System.EventHandler(this.btn_Keyboard_Click);
            // 
            // UserLogSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(532, 280);
            this.Controls.Add(this.btn_Keyboard);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.cbx_Users);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_LogInOut);
            this.Controls.Add(this.pictureBox8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserLogSystem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Access | [Station Name]";
            this.TransparencyKey = System.Drawing.Color.Silver;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Keyboard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Button btn_LogInOut;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cbx_Users;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btn_Keyboard;
    }
}