//Mario A. Dominguez Guerrero 
//January - 2022

#region System Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
#endregion

#region Project Libraries
//DLL .NET 4.5.2
using Sres.Net.EEIP;
#endregion

namespace Software
{
    class IAI_PCON
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
        //Message
        public const int MAX_COMMAND_SIZE = 3000;
        #endregion

        #region IO and Data Registers: Manual No.: ME0278-10D (January 2017)
        private const int Registers_SIZE = 16;
        private const int ControlBits_SIZE = 2;
        private const int Data_SIZE = 16;

        /// Factory limits of the Servomotor
        /// Position            =   0.15 to 200.15  mm
        /// Speed               =   1 to 210        mm/s
        /// Acc/Decc            =   0.01 to 1.00    G
        /// Position Band       =   0.01 to 200.30  mm/s2
        /// Press Current Limit =   0 to 175   
        /// ----------------------------------------------
        /// Values for general purpose 
        /// Acc/Decc             =   0.30 mm/s
        /// Position Band        =   Position Target
        /// Press current limit  =   125
        /// ----------------------------------------------
        static string[] factory_Limits = new string[10] { "000.15", "200.15",
                                                          "001", "210",
                                                          "0.01", "1.00",
                                                          "0.01", "200.30",
                                                          "000", "175" };
        public string[] Factory_Limits
        {
            get
            {
                return factory_Limits;
            }
        }

        public const string AccDecc = "0.30";
        public const string PressCurrLimit = "125";

        string _measurement;

        #region Inputs
        //Parameter No. 84: Half Direct Value Mode (Number of Occupied Bytes: 16)
        //Bytes 0 - 15
        private Byte[] Inputs_OT = new Byte[Data_SIZE];
        //Convertions
        static string[] lastValue = new string[5];
        public string[] LastValue
        {
            get
            {
                return lastValue;
            }
            set
            {
                lastValue = value;
            }
        }

        #region Target Position (TP)
        //Byte 0 - 3 (0-1 Lower, 2-3 upper)
        // -999999 to 999999 (Unit: 0.01mm)
        // Binary to Decimal from signed 2's complement
        private static Byte[] targetPosition_In = new Byte[4];
        public Byte[] TargetPosition_In
        {
            get
            {
                return targetPosition_In;
            }

            set
            {
                targetPosition_In = value;
            }
        }
        //Target Position decimal Value
        private static string targetPosition_value = "00.00";
        public string TargetPosition_value
        {
            get
            {
                return targetPosition_value;
            }

            set
            {
                targetPosition_value = value;
            }
        }

        #endregion

        #region Position Band (PB)
        //Byte 4 - 7 (4-5 Lower, 6-7 upper)
        // 1 to 999999 (Unit: 0.01mm)
        // Decimal from signed 2's complement
        private static Byte[] positionBand_In = new Byte[4];
        public Byte[] PositionBand_In
        {
            get
            {
                return positionBand_In;
            }

            set
            {
                positionBand_In = value;
            }
        }
        //Position Band decimal Value
        private static string positionBand_value = "00.00";
        public string PositionBand_value
        {
            get
            {
                return positionBand_value;
            }

            set
            {
                positionBand_value = value;
            }
        }
        #endregion

        #region Speed (S)
        //Byte 8 - 9
        // 1 to 999999 (Unit: 0.01mm/s)
        // Binary to Decimal 
        private static Byte[] speed_In = new Byte[4];
        public Byte[] Speed_In
        {
            get
            {
                return speed_In;
            }

            set
            {
                speed_In = value;
            }
        }
        //Speed decimal Value
        private static string speed_value = "0000";
        public string Speed_value
        {
            get
            {
                return speed_value;
            }

            set
            {
                speed_value = value;
            }
        }
        #endregion

        #region Acceleration / deceleration (ACCDCC)
        //Byte 10 - 11
        // 1 to 300 (Unit: 0.01G)
        // Binary to Decimal 
        private static Byte[] accDecc_In = new Byte[4];
        public Byte[] AccDecc_In
        {
            get
            {
                return accDecc_In;
            }

            set
            {
                accDecc_In = value;
            }
        }
        //Acceleration/Decelaration decimal Value
        private static string accDecc_value = "0.00";
        public string AccDecc_value
        {
            get
            {
                return accDecc_value;
            }

            set
            {
                accDecc_value = value;
            }
        }
        #endregion

        #region Pressing current-limiting value (PCL)
        // Byte 12 - 13
        // 0 to 255 (Unit: 0-100 %)
        // Binary to Decimal 
        private static Byte[] pressCurrLimit_In = new Byte[4];
        public Byte[] PressCurrLimit_In
        {
            get
            {
                return pressCurrLimit_In;
            }

            set
            {
                pressCurrLimit_In = value;
            }
        }
        //Pressing current Limit decimal Value
        private static string pressCurrLimit_value = "0000";
        public string PressCurrLimit_value
        {
            get
            {
                return pressCurrLimit_value;
            }

            set
            {
                pressCurrLimit_value = value;
            }
        }
        #endregion

        #region Control Signal (CS)
        //Byte 14 - 15
        private static bool[] controlSignal_In = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] ControlSignal_In
        {
            get
            {
                return controlSignal_In;
            }

            set
            {
                controlSignal_In = value;
            }
        }
        #endregion

        #endregion

        #region Outputs
        //Parameter No. 84: Half Direct Value Mode (Number of Occupied Bytes: 16)
        //Bytes 0 - 15
        private Byte[] Outputs_TO = new Byte[Data_SIZE];

        #region Current position (CP)
        // Byte 0 - 3 (0-1 Lower, 2-3 upper)
        // -999999 to 999999 (Unit: 0.01 mm)
        // Binary to Decimal from signed 2's complement
        private static Byte[] currentPosition_Out = new Byte[4];
        public Byte[] CurrentPosition_Out
        {
            get
            {
                return currentPosition_Out;
            }

            set
            {
                currentPosition_Out = value;
            }
        }
        //Current position decimal Value
        private static float currentPosition_value = 0.0f;
        public float CurrentPosition_value
        {
            get
            {
                return currentPosition_value;
            }

            set
            {
                currentPosition_value = value;
            }
        }

        #endregion

        #region Command current (CC)
        // Byte 4 - 7 (4-5 Lower, 6-7 upper)
        // 1 to 999999 (Unit: 1 mA)
        // Binary to Decimal 
        private static Byte[] cmdCurrent_Out = new Byte[4];
        public Byte[] CmdCurrent_Out
        {
            get
            {
                return cmdCurrent_Out;
            }

            set
            {
                cmdCurrent_Out = value;
            }
        }
        //Command current decimal Value
        private static float cmdCurrent_value = 0.0f;
        public float CmdCurrent_value
        {
            get
            {
                return cmdCurrent_value;
            }

            set
            {
                cmdCurrent_value = value;
            }
        }
        #endregion

        #region Current speed (CS)
        // Byte 8 - 11
        // Binary to Decimal 
        private static Byte[] currSpeed_Out = new Byte[4];
        public Byte[] CurrSpeed_Out
        {
            get
            {
                return currSpeed_Out;
            }

            set
            {
                currSpeed_Out = value;
            }
        }
        //Current speed decimal Value
        private static float currSpeed_value = 0.0f;
        public float CurrSpeed_value
        {
            get
            {
                return currSpeed_value;
            }

            set
            {
                currSpeed_value = value;
            }
        }
        #endregion

        #region Alarm code (AC)
        // Byte 12 - 13
        // Binary to Decimal
        private static Byte[] alarmCode_Out = new Byte[4];
        public Byte[] AlarmCode_Out
        {
            get
            {
                return alarmCode_Out;
            }

            set
            {
                alarmCode_Out = value;
            }
        }
        //Alarm code decimal Value
        private static float alarmCode_value = 0.0f;
        public float AlarmCode_value
        {
            get
            {
                return alarmCode_value;
            }

            set
            {
                alarmCode_value = value;
            }
        }
        #endregion

        #region Status Signal (SS)
        //Outputs: Byte 14 - 15
        private static bool[] statuSignal_Out = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] StatuSignal_Out
        {
            get
            {
                return statuSignal_Out;
            }

            set
            {
                statuSignal_Out = value;
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region Callbacks

        #endregion

        #region Objects
        //PCON-CB-56PWAI-EP-0-0 module: Ethernet/IP
        EEIPClient eeipClient = new EEIPClient();
        #endregion

        public IAI_PCON()
        {
            iP = "192.168.1.17";
        }

        #region Functions

        #region Connection
        //Connect to Kistler Module
        bool Connect()
        {
            bool bSucc = false;

            #region Ethernet/IP (Implicit Messaging)
            try
            {
                // PCON-CB-56PWAI-EP-0-0 module
                eeipClient.IPAddress = iP;
                // Initialize Session
                eeipClient.RegisterSession();

                ////Parameters from Originator -> Target
                eeipClient.O_T_InstanceID = 0x96;           //Instance ID of Outputs
                eeipClient.O_T_Length = eeipClient.Detect_O_T_Length();                    //The Method "Detect_O_T_Length" detect the Length using an UCMM Message
                eeipClient.O_T_RealTimeFormat = Sres.Net.EEIP.RealTimeFormat.Header32Bit;   //Header Format
                eeipClient.O_T_OwnerRedundant = false;
                eeipClient.O_T_Priority = Sres.Net.EEIP.Priority.Scheduled;
                eeipClient.O_T_VariableLength = false;
                eeipClient.O_T_ConnectionType = Sres.Net.EEIP.ConnectionType.Point_to_Point;
                eeipClient.RequestedPacketRate_O_T = 500000;        //500ms is the Standard value

                //Parameters from Target -> Originator
                eeipClient.T_O_InstanceID = 0x64;           //Instance ID of Inputs
                eeipClient.T_O_Length = eeipClient.Detect_T_O_Length();
                eeipClient.T_O_RealTimeFormat = Sres.Net.EEIP.RealTimeFormat.Modeless;
                eeipClient.T_O_OwnerRedundant = false;
                eeipClient.T_O_Priority = Sres.Net.EEIP.Priority.Scheduled;
                eeipClient.T_O_VariableLength = false;
                eeipClient.T_O_ConnectionType = Sres.Net.EEIP.ConnectionType.Point_to_Point;
                eeipClient.RequestedPacketRate_T_O = 500000;        //RPI in  500ms is the Standard value

                //Forward open initiates the Implicit Messaging
                eeipClient.ForwardOpen();

                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)PCON_Parameters.CommStatus] = true;
                bSucc = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("PCON-CB module: " + ex);
                bSucc = false;
            }
            #endregion

            return bSucc;
        }
        //Disconnect to PCON Module
        bool Disconnect()
        {
            bool bSucc = false;

            try
            {
                //Close the Session with PCON-CB-56PWAI-EP-0-0
                eeipClient.ForwardClose();
                eeipClient.UnRegisterSession();
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)PCON_Parameters.CommStatus] = false;
                bSucc = true;
            }
            catch (Exception es)
            {
                //Message
                MessageBox.Show("PCON-CB module: " + es);
                bSucc = false;
            }

            return bSucc;
        }
        #endregion

        #region IO and Data Registers

        #region IO Registers
        //Scan Input registers: PCON <- PC/PLC
        void Inputs()
        {
            //Device Connected
            if (parameters[(int)PCON_Parameters.CommStatus])
            {

                #region Target position
                //Convert the bytes [0-3] to value
                lastValue[0] = "";          
                positionBand_value = targetPosition_value;
                char[] a = targetPosition_value.ToCharArray();
                if (a.Length >= 4)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a.Length == 5)
                        {
                            if (i != 2) lastValue[0] += a[i];
                        }
                        else
                             if (i != 3) lastValue[0] += a[i];
                    }

                    Int64 c = Convert.ToInt64(lastValue[0]);

                    targetPosition_In = BitConverter.GetBytes(c);
                }

                int k = 0;
                //Get the bytes for Target position: [0-3]
                for (int i = 3; i >= 0; i--)
                {
                    Inputs_OT[i] = targetPosition_In[k];
                    k++;
                }
                #endregion

                #region Position Band
                //Convert the bytes [4-7] to value
                lastValue[1] = "";
                a = positionBand_value.ToCharArray();
                if (a.Length >= 4)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a.Length == 5)
                        {
                            if (i != 2) lastValue[1] += a[i];
                        }
                        else
                             if (i != 3) lastValue[1] += a[i];
                    }

                    Int64 c = Convert.ToInt64(lastValue[1]);

                    positionBand_In = BitConverter.GetBytes(c);
                }

                k = 0;
                //Get the bytes for Position Band: [4-7]
                for (int i = 7; i >= 4; i--)
                {
                    Inputs_OT[i] = positionBand_In[k];
                    k++;
                }
                #endregion

                #region Speed 
                //Convert the bytes [8-9] to value
                lastValue[2] = "";
                a = speed_value.ToCharArray();
                if (a.Length >= 3)
                {
                    for (int i = 0; i < 3; i++) lastValue[2] += a[i];

                    Int64 c = Convert.ToInt64(lastValue[2]);

                    speed_In = BitConverter.GetBytes(c);
                }

                k = 0;
                //Get the bytes for Speed: [8-9]
                for (int i = 9; i >= 8; i--)
                {
                    Inputs_OT[i] = speed_In[k];
                    k++;
                }

                #endregion

                #region Acceleration/Deceleration
                //Convert the bytes [10-11] to value
                lastValue[3] = "";
                accDecc_value = AccDecc;
                a = accDecc_value.ToCharArray();
                if (a.Length >= 4)
                {
                    for (int i = 0; i < 4; i++)
                        if (i != 1) lastValue[3] += a[i];

                    Int64 c = Convert.ToInt64(lastValue[3]);

                    accDecc_In = BitConverter.GetBytes(c);
                }

                k = 0;
                //Get the bytes for Acceleration/Deceleration: [10-11]
                for (int i = 11; i >= 10; i--)
                {
                    Inputs_OT[i] = accDecc_In[k];
                    k++;
                }
                #endregion

                #region Pressing current Limit
                //Convert the bytes [12-13] to value
                lastValue[4] = "";
                pressCurrLimit_value = PressCurrLimit;
                a = pressCurrLimit_value.ToCharArray();
                if (a.Length >= 3)
                {
                    for (int i = 0; i < 3; i++) lastValue[4] += a[i];

                    Int64 c = Convert.ToInt64(lastValue[4]);

                    pressCurrLimit_In = BitConverter.GetBytes(c);
                }

                k = 0;
                //Get the bytes for Pressing current Limit: [12-13]
                for (int i = 13; i >= 12; i--)
                {
                    Inputs_OT[i] = pressCurrLimit_In[k];
                    k++;
                }
                #endregion

                #region Control Signal
                //Get the bytes for Control signal: [14-15]
                bool[] section = new bool[16];

                for (int i = 14; i < 16; i++)
                {
                    switch (i)
                    {
                        case 15:
                            //Byte 14
                            for (int j = 0; j < 8; j++)
                            {
                                section[j] = controlSignal_In[j];
                            }
                            Inputs_OT[15] = Convert.ToByte(BooleanArrayToDINT(section));
                            break;
                        case 14:
                            //Byte 15
                            for (int j = 8; j < 16; j++)
                            {
                                int l = j - 8;
                                section[l] = controlSignal_In[j];
                            }
                            Inputs_OT[14] = Convert.ToByte(BooleanArrayToDINT(section));
                            break;
                        default:
                            break;
                    }
                }
                #endregion

                //Read Byte 0 - 15
                for (int i = 0; i < eeipClient.Detect_O_T_Length(); i++)
                {
                    eeipClient.O_T_IOData[i] = Inputs_OT[i];
                }
            }
        }
        //Scan Ouput registers: PCON -> PC/PLC
        void Outputs()
        {
            //Device Connected
            if (parameters[(int)PCON_Parameters.CommStatus])
            {
                //Read Byte 0 - 15
                for (int i = 0; i < eeipClient.Detect_T_O_Length(); i++)
                {
                    Outputs_TO[i] = eeipClient.T_O_IOData[i];
                }

                #region Current position
                int k = 0;
                Decimal _converted = 0;
                _measurement = "";

                //Get the bytes for Current position: [0-3]
                for (int i = 0; i < 4; i++)
                {
                    k = i;
                    //Convert to Binary string
                    string BINConvertion = Convert.ToString(Outputs_TO[i], 2);
                    _measurement += BINConvertion.PadLeft(8, '0');
                }

                _converted = Convert.ToInt64(_measurement, 2);
                double value = Convert.ToSingle(_converted);
                //constant unit (0.01mm)
                value *= 0.01;

                //Convert the bytes [0-3] to value
                currentPosition_value = (float)value;
                #endregion

                #region Command Current
                _measurement = "";
                //Get the bytes for Command Current: [4-7]
                for (int i = 4; i < 8; i++)
                {
                    k = i - 4;

                    string BINConvertion = Convert.ToString(Outputs_TO[i], 2);
                    _measurement += BINConvertion.PadLeft(8, '0');
                }

                _converted = Convert.ToInt64(_measurement, 2);
                double value2 = Convert.ToSingle(_converted);

                //Convert the bytes [4-7] to value
                cmdCurrent_value = (float)value2;
                #endregion

                #region Current Speed
                _measurement = "";
                //Get the bytes for Current Speed: [8-11]
                for (int i = 8; i < 12; i++)
                {
                    k = i - 8;
                    string BINConvertion = Convert.ToString(Outputs_TO[i], 2);
                    _measurement += BINConvertion.PadLeft(8, '0');
                }

                _converted = Convert.ToInt64(_measurement, 2);
                double value3 = Convert.ToSingle(_converted);
                value3 *= 0.01;

                //Convert the bytes [8-11] to value
                currSpeed_value = (float)value3;
                #endregion

                #region Alarm Code
                _measurement = "";
                //Get the bytes for Alarm Code: [12-13]
                for (int i = 12; i < 14; i++)
                {
                    k = i - 12;
                    string BINConvertion = Convert.ToString(Outputs_TO[i], 2);
                    _measurement += BINConvertion.PadLeft(8, '0');
                }

                _converted = Convert.ToInt32(_measurement, 2);
                double value4 = Convert.ToSingle(_converted);

                //Convert the bytes [12-13] to value
                alarmCode_value = (float)value4;
                #endregion

                #region Control bits
                //Get the bytes for Control signal: [14-15]
                for (int i = 14; i < 16; i++)
                {
                    bool[] section = DINTtoBoolArray(Outputs_TO[i]);

                    switch (i)
                    {
                        case 15:
                            //Byte 14
                            for (int j = 0; j < 8; j++)
                            {
                                statuSignal_Out[j] = section[j];
                            }
                            break;
                        case 14:
                            //Byte 15
                            for (int j = 8; j < 16; j++)
                            {
                                int l = j - 8;
                                statuSignal_Out[j] = section[l];
                            }
                            break;
                        default:
                            break;
                    }
                }
                #endregion
            }
        }
        #endregion

        #region Conversions
        //Integer to Boolean Array
        public bool[] DINTtoBoolArray(int Value)
        {
            bool[] Inputs = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

            BitArray ba = new BitArray(new int[] { Value });
            for (int i = 0; i < ba.Length; i++)
            {
                Inputs[i] = ba[i];
            }
            return Inputs;
        }

        //Boolean Array to Integer
        public int BooleanArrayToDINT(bool[] Outputs)
        {
            int resultado = 0;

            for (int i = 0; i < Outputs.Length; i++)
            {
                if (Outputs[i])
                {
                    // Usamos la potencia de 2, según la posición
                    resultado += (int)Math.Pow(2, i);
                }
            }
            return resultado;
        }
        //Boolean Array to Integer 16 bits
        public Int16 BooleanArrayToDINT16(bool[] Outputs)
        {
            Int16 resultado = 0;

            for (int i = 0; i < Outputs.Length; i++)
            {
                if (Outputs[i])
                {
                    // Usamos la potencia de 2, según la posición
                    resultado += (short)Math.Pow(2, i);
                }
            }
            return resultado;
        }
        //Hex to Float
        public float ByteArrayToFloat(byte[] Value)
        {
            float Resultado = 0.0f;
            //get the String Value
            string Valor = BitConverter.ToString(Value);


            //Resultado = BitConverter.ToSingle(Valor, 0);

            return Resultado;
        }
        #endregion

        #endregion

        #endregion

        #region Threads
        //Open the communication
        public bool Open_Communication()
        {
            bool bSucc = false;
            if (!parameters[(int)PCON_Parameters.Control])
            {
                if (Connect())
                {
                    parameters[(int)PCON_Parameters.Control] = true;
                    //Active the communication process
                    Thread _Tick = new Thread(new ThreadStart(() => Tick()));
                    _Tick.Start();
                    bSucc = true;
                }
                else
                {
                    parameters[(int)PCON_Parameters.Control] = false;
                    bSucc = false;
                }
            }
            else
                bSucc = false;

            return bSucc;
        }
        //Scan process for Input/Output Data
        private void Tick()
        {
            while (parameters[(int)PCON_Parameters.Control])
            {
                Thread.Sleep(50);
                //Scan Input registers: PCON <- PC/PLC
                Inputs();
                //Scan Ouput registers: PCON -> PC/PLC
                Outputs();
            }
        }
        //Close the communication
        public bool Close_Communication()
        {
            bool bSucc = false;
            if (parameters[(int)PCON_Parameters.Control])
            {
                Disconnect();
                parameters[(int)PCON_Parameters.Control] = false;
                bSucc = true;
            }
            else
            {
                parameters[(int)PCON_Parameters.Control] = false;
                bSucc = false;
            }

            return bSucc;
        }
        #endregion
    }
}
