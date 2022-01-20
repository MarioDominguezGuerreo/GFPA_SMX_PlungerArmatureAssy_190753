//Mario A. Dominguez Guerrero 
//September - 2020

#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#endregion

#region Project Libraries

#endregion

namespace Software
{
    class Scanner
    {
        #region Variables

        #region Communicacion Parameters[#]
        /// 0 = Communication Status (Disconnected = 0, Connected = 1)
        /// 1 = Read / Write Coils, (Read = 0, Write = 1)
        /// 2 = Control (Disable = 0 / Enable = 1)
        private static bool[] parameters = new bool[4] { false, false, false, false };
        public bool[] Parameters
        {
            get
            {
                return parameters;
            }

            set
            {
                parameters = value;
            }
        }
        #endregion

        #region Command Types
        /// 0 = Info
        /// 1 = Code Quality
        private const int info = 0;
        public int Info
        {
            get
            {
                return info;
            }
        }
        private const int codeQuality = 1;
        public int CodeQuality
        {
            get
            {
                return codeQuality;
            }
        }
        //Command Type
        private static int cmdType;
        public int CmdType
        {
            get
            {
                return cmdType;
            }

            set
            {
                cmdType = value;
            }
        }
        //Values for each Cmd type
        private static string value_Info;
        public string Value_Info
        {
            get
            {
                return value_Info;
            }

            set
            {
                value_Info = value;
            }
        }
        private static string value_Quality;
        public string Value_Quality
        {
            get
            {
                return value_Quality;
            }

            set
            {
                value_Quality = value;
            }
        }
        private static string values_Msg;
        public string Values_Msg
        {
            get
            {
                return values_Msg;
            }

            set
            {
                values_Msg = value;
            }
        }
        #endregion

        #region TCP/IP Client Settings
        private static string iP;
        public string IP
        {
            get
            {
                return iP;
            }

            set
            {
                iP = value;
            }
        }
        private static int port;
        public int Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }
        public static Socket PC_Client;
        //Message
        public const int MAX_COMMAND_SIZE = 3000;
        #endregion

        #endregion

        #region Callbacks

        #endregion

        #region Objects

        #endregion

        #region Controls

        #endregion

        #region Functions
        //Connect to the Omron_V430F (Server)
        private bool Connect()
        {
            try
            {
                //Open the connection
                //Configuracion del Servidor
                PC_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ServidorDireccion = new IPEndPoint(IPAddress.Parse(iP), port);
                PC_Client.Connect(ServidorDireccion);

                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)Scanner_Comm.CommStatus] = true;
                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Connection Error\n", "Error");

                return false;
            }
        }
        //Disconnect to the Omron_V430F (Server)
        private bool Disconnect()
        {
            try
            {
                //Close the connection   
                PC_Client.Disconnect(true);
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)Scanner_Comm.CommStatus] = false;
                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Disconnection Error\n", "Error");
                return false;
            }
        }
        //Read data from the Omron_V430F (Server)
        private void ReadMessage()
        {
            //Connection is Open
            if (parameters[(int)Scanner_Comm.CommStatus])
            {
                //Get the message
                byte[] Msg = new byte[MAX_COMMAND_SIZE];
                //Get the message length         
                int Size = PC_Client.Receive(Msg);
                //Convert the message to char array
                char[] RxMasg = ByteArrayToCharArray(Msg, Size);
                //Convert the message  to String
                values_Msg = CharArrayToString(RxMasg);
                //Selection the Command type
                switch (cmdType)
                {
                    case info:
                        if (Size > 0 && values_Msg != "\r\n")
                        {
                            value_Info = Read_Info(values_Msg);
                        }
                        break;
                    case codeQuality:
                        if (Size > 400)
                        {
                            value_Quality = Read_CodeQuality(RxMasg);
                        }
                        break;
                    default:
                        break;
                } 
            }
        }
        //Send command to the Omron_V430F (Server)
        public void SendCommand(string Command, int TypeCmd)
        {
            if (parameters[(int)Scanner_Comm.CommStatus])
            {
                cmdType = TypeCmd;
                PC_Client.Send(System.Text.Encoding.ASCII.GetBytes(Command), 0, Command.Length, SocketFlags.None);
            }
        }

        //Converter Byte Array to Char Array
        private char[] ByteArrayToCharArray(byte[] Message, int Size)
        {
            //Define the Array variable to save the message on base of its length
            char[] resultado = new char[Size];

            //Read all byte array 
            for (int i = 0; i < Size; i++)
            {
                //Convert the byte to Char of the message and then added the value to the char array 
                resultado[i] += Convert.ToChar(Message[i]);
            }
            //Message converted
            return resultado;
        }
        //Converter Char Array to String
        private String CharArrayToString(char[] Message)
        {
            string resultado = "";

            for (int i = 0; i < Message.Length; i++)
            {
                //Adding characters to String
                resultado += Message[i].ToString();
            }
            return resultado;
        }
        private string Read_Info(string Msg)
        {
            string Msg_Info = "";
            string[] CompleteChain = Msg.Split(',');
            //Get the value from the first section of the message
            Msg_Info = CompleteChain[0];
            int Size = Msg_Info.Length;
            //Check each character to verify if there is a \n , \r or both
            char[] Characters = Msg_Info.ToCharArray();
            Msg_Info = "";
            for (int i = 0; i < Size; i++)
            {
                if ((Characters[i] != '\n') && (Characters[i] != '\r'))
                {
                    Msg_Info += Characters[i];
                }
            }
            //Result 
            return Msg_Info;
        }
        private string Read_CodeQuality(char[] Msg)
        {
            //Get the value of the section of the caracter
            string Msg_OverallGrade = Msg[477].ToString() + "," +
                                     Msg[557].ToString() + "," +
                                     Msg[637].ToString() + "," +
                                     Msg[717].ToString() + "," +
                                     Msg[797].ToString() + "," +
                                     Msg[877].ToString() + "," +
                                     Msg[957].ToString() + "," +
                                     Msg[1037].ToString();
            return Msg_OverallGrade;
        }
        #endregion

        #region Threads
        //Open the communication
        public bool Open_Communication()
        {
            if (!parameters[(int)Scanner_Comm.Control])
            {
                if (Connect())
                {
                    parameters[(int)Scanner_Comm.Control] = true;
                    //Active the communication process
                    Thread Tick = new Thread(new ThreadStart(() => Communication()));
                    Tick.Start();
                    return true;
                }
                else
                {
                    parameters[(int)Scanner_Comm.Control] = false;
                    return false;
                }
            }
            else
                return false;
        }
        //Communication Proccess
        private void Communication()
        {
            while (parameters[(int)Scanner_Comm.CommStatus])
            {
                //Reading the OMRON V430 F Read Messages
                Thread.Sleep(50);
                ReadMessage();
            }
        }
        //Close the communication
        public bool Close_Communication()
        {
            if (parameters[(int)Scanner_Comm.Control])
            {
                Disconnect();
                parameters[(int)Scanner_Comm.Control] = false;
                return true;
            }
            else
            {
                parameters[(int)Scanner_Comm.Control] = false;
                return false;
            }
        }
        #endregion
    }
}
