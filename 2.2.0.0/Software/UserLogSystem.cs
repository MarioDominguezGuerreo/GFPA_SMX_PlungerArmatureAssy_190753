//Mario A. Dominguez Guerrero 
//July - 2020

#region System Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
#endregion

#region Project Libraries
using PCdatetime;
using PCMessages;
#endregion

namespace Software
{
    public partial class UserLogSystem : Form
    {
        #region Variables
        //Paths for History of Production
        public string pathUserData = "MachineData/Users.txt";
        public string pathPasswordData = "MachineData/Passwords.txt";

        //Users and Passwords
        public string Password;
        private static string user;
        public string User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }
        //Login / Logout status
        private static bool logInOutStatus;
        public bool LogInOutStatus
        {
            get
            {
                return logInOutStatus;
            }

            set
            {
                logInOutStatus = value;
            }
        }

        #endregion

        #region Callbacks

        #endregion

        #region Objects
        //User log System page
        public static UserLogSystem OUserLogSystem;
        #endregion

        /*Initialization of the Users Systems form*/
        public UserLogSystem()
        {
            InitializeComponent();
            //User log System page
            OUserLogSystem = this;

            //Load the User List
            LoadUsersList();

            //Login / logout status
            logInOutStatus = false;
            user = "Operator";
        }

        #region Controls
        //Cancel the logging
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            //Hide the window
            Form.ActiveForm.Hide();
        }
        //Log in or Log out
        private void btn_LogInOut_Click(object sender, EventArgs e)
        {
            DialogResult Result;
            //Check the User and Password set
            User = cbx_Users.Text;
            Password = txt_Password.Text;
            //Any user other than operation
            if (!logInOutStatus)
            {
                if (LogIn())
                {
                    Result = MessageBox.Show("Login Successfully", "Users System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //Controls 
                    btn_LogInOut.Text = "Logout";
                    cbx_Users.Text = "";
                    cbx_Users.Enabled = false;
                    txt_Password.Text = "";
                    txt_Password.Enabled = false;
                }    
            }
            else
            {
                LogOut();
                Result = MessageBox.Show("Operation Mode", "Users System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Controls
                btn_LogInOut.Text = "Login";
                cbx_Users.Text = "";
                cbx_Users.Enabled = true;
                txt_Password.Text = "";
                txt_Password.Enabled = true;
            }         
        }
        //Keyboard
        private void btn_Keyboard_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Windows\SysWOW64\osk.exe");
        }
        #endregion

        #region Information

        #endregion

        #region Functions
        //Load User List
        public bool LoadUsersList()
        {
            bool bSucess = false;
            try
            {
                String List;
                StreamReader _File = new StreamReader(pathUserData);
                while ((List = _File.ReadLine()) != null)
                {
                    cbx_Users.Items.Add(List);
                }
                _File.Close();
                bSucess = true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.ProdMessages("User Log System: The UsersList.txt Files wasn't loaded\n", "Error");
                bSucess = false;
            }
            return bSucess;
        }
        //Search and Compare the User, Password
        public bool VerifyUserPassword()
        {
            bool bSucess = false;
            string Info;
            //Query the file Log
            StreamReader _File = new StreamReader(pathPasswordData);
            while ((Info = _File.ReadLine()) != null)
            {
                //Verify the text line read it
                string[] CompleteChain = Info.Split(',');
                //Select the User
                string user = CompleteChain[0];
                string pass;
                if (user == User)
                {
                    //Select the password
                    pass = CompleteChain[1];
                    if (pass == Password)
                    {
                        bSucess = true;
                        break;
                    }
                    else
                        bSucess = false;
                }
                else
                    bSucess = false;
            }
            _File.Close();
            return bSucess;
        }
        //LogOut
        public bool LogOut()
        {
            bool bSucess = false;
            //Logout: Operation mode
            logInOutStatus = false;
            //Message
            HMI.OForm.SystemMessages("User Log System: Logout the User " + User + ", ID: " + Password + "\n", "System");
            user = "Operator";
            return bSucess;
        }
        //LogIn
        public bool LogIn()
        {
            bool bSucess = false;
            //Verification of the User and Password
            if (VerifyUserPassword())
            {
                logInOutStatus = true;
                //Message
                HMI.OForm.SystemMessages("User Log System: Login the User: " + User + ", ID: " + Password + "\n", "System");
                bSucess = true;
            }
            else
                user = "Operator";
            return bSucess;
        }
        #endregion

        #region Threads

        #endregion
    }
}
