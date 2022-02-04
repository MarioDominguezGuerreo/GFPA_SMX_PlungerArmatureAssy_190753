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
#endregion

#region Project Libraries
using SOEM;
#endregion

namespace Software
{
    class Kistler_5847B0
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

        public int NumberofSlavesDetected;
        //Message
        public const int MAX_COMMAND_SIZE = 3000;
        #endregion

        #region IO and Data Registers
        private const int Registers_SIZE = 220;
        private const int ControlBits_SIZE = 20;
        private const int Data_SIZE = 40;

        static float constantK = 999.99f;
        public float ConstantK
        {
            get
            {
                return constantK;
            }

            set
            {
                constantK = value;
            }
        }

        #region Inputs
        private Byte[] inputs = new Byte[Registers_SIZE];

        #region Control Bits
        //Inputs: Byte 0 - 3
        private static bool[] inputs_03 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Inputs_03
        {
            get
            {
                return inputs_03;
            }

            set
            {
                inputs_03 = value;
            }
        }
        //Inputs: Byte 4 - 7
        private static bool[] inputs_47 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Inputs_47
        {
            get
            {
                return inputs_47;
            }

            set
            {
                inputs_47 = value;
            }
        }
        //Inputs: Byte 8 - 11
        private static bool[] inputs_811 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Inputs_811
        {
            get
            {
                return inputs_811;
            }

            set
            {
                inputs_811 = value;
            }
        }
        //Inputs: Byte 12 - 15
        private static bool[] inputs_1215 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Inputs_1215
        {
            get
            {
                return inputs_1215;
            }

            set
            {
                inputs_1215 = value;
            }
        }
        //Inputs: Byte 16 - 19
        private static bool[] inputs_1619 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Inputs_1619
        {
            get
            {
                return inputs_1619;
            }

            set
            {
                inputs_1619 = value;
            }
        }
        #endregion

        #region Data: Bus field -> maXYmos Input
        //Inputs: Data
        private static Byte[] data_Input = new Byte[Data_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public Byte[] Data_Input
        {
            get
            {
                return data_Input;
            }

            set
            {
                data_Input = value;
            }
        }
        #endregion

        #endregion

        #region Outputs
        private Byte[] outputs = new Byte[Registers_SIZE];

        #region Control Bits
        //Outputs: Byte 0 - 3
        private static bool[] outputs_03 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Outputs_03
        {
            get
            {
                return outputs_03;
            }

            set
            {
                outputs_03 = value;
            }
        }
        //Outputs: Byte 4 - 7
        private static bool[] outputs_47 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Outputs_47
        {
            get
            {
                return outputs_47;
            }

            set
            {
                outputs_47 = value;
            }
        }
        //Outputs: Byte 8 - 11
        private static bool[] outputs_811 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Outputs_811
        {
            get
            {
                return outputs_811;
            }

            set
            {
                outputs_811 = value;
            }
        }
        //Outputs: Byte 12 - 15
        private static bool[] outputs_1215 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Outputs_1215
        {
            get
            {
                return outputs_1215;
            }

            set
            {
                outputs_1215 = value;
            }
        }
        //Outputs: Byte 16 - 19
        private static bool[] outputs_1619 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Outputs_1619
        {
            get
            {
                return outputs_1619;
            }

            set
            {
                outputs_1619 = value;
            }
        }
        #endregion

        #region Data: Bus field <- maXYmos Output
        //Outputs: Data
        private const int ChannelXY = 4;

        #region Channel X
        private static Byte[] data_Out_ChX = new Byte[ChannelXY];
        public Byte[] Data_Out_ChX
        {
            get
            {
                return data_Out_ChX;
            }

            set
            {
                data_Out_ChX = value;
            }
        }

        private static float channelX_Value = 0.0f;
        public float ChannelX_Value
        {
            get
            {
                return channelX_Value;
            }

            set
            {
                channelX_Value = value;
            }
        }
        #endregion

        #region Channel Y
        private static Byte[] data_Out_ChY = new Byte[ChannelXY];
        public Byte[] Data_Out_ChY
        {
            get
            {
                return data_Out_ChY;
            }

            set
            {
                data_Out_ChY = value;
            }
        }

        private static float channelY_Value = 0.0f;
        public float ChannelY_Value
        {
            get
            {
                return channelY_Value;
            }

            set
            {
                channelY_Value = value;
            }
        }
        #endregion

        #region Curves X. Y

        #region Curve X
        //Min
        private static Byte[] data_Out_CurveXMin = new Byte[ChannelXY];
        public Byte[] Data_Out_CurveXMin
        {
            get
            {
                return data_Out_CurveXMin;
            }

            set
            {
                data_Out_CurveXMin = value;
            }
        }

        private static float curveXMin_Value = 0.0f;
        public float CurveXMin_Value
        {
            get
            {
                return curveXMin_Value;
            }

            set
            {
                curveXMin_Value = value;
            }
        }
        //Max
        private static Byte[] data_Out_CurveXMax = new Byte[ChannelXY];
        public Byte[] Data_Out_CurveXMax
        {
            get
            {
                return data_Out_CurveXMax;
            }

            set
            {
                data_Out_CurveXMax = value;
            }
        }

        private static float curveXMax_Value = 0.0f;
        public float CurveXMax_Value
        {
            get
            {
                return curveXMax_Value;
            }

            set
            {
                curveXMax_Value = value;
            }
        }
        #endregion

        #region Curve Y
        //Min
        private static Byte[] data_Out_CurveYMin = new Byte[ChannelXY];
        public Byte[] Data_Out_CurveYMin
        {
            get
            {
                return data_Out_CurveYMin;
            }

            set
            {
                data_Out_CurveYMin = value;
            }
        }

        private static float curveYMin_Value = 0.0f;
        public float CurveYMin_Value
        {
            get
            {
                return curveYMin_Value;
            }

            set
            {
                curveYMin_Value = value;
            }
        }
        //Max
        private static Byte[] data_Out_CurveYMax = new Byte[ChannelXY];
        public Byte[] Data_Out_CurveYMax
        {
            get
            {
                return data_Out_CurveYMax;
            }

            set
            {
                data_Out_CurveYMax = value;
            }
        }

        private static float curveYMax_Value = 0.0f;
        public float CurveYMax_Value
        {
            get
            {
                return curveYMax_Value;
            }

            set
            {
                curveYMax_Value = value;
            }
        }
        #endregion

        #endregion

        #region Part Status (OK,NOK)
        //OK Parts
        private static Byte[] data_Out_OKParts = new Byte[ChannelXY];
        public Byte[] Data_Out_OKParts
        {
            get
            {
                return data_Out_OKParts;
            }

            set
            {
                data_Out_OKParts = value;
            }
        }

        private static Int32 oKParts_Value = 0;
        public Int32 OKParts_Value
        {
            get
            {
                return oKParts_Value;
            }

            set
            {
                oKParts_Value = value;
            }
        }
        //NOK Parts
        private static Byte[] data_Out_NOKParts = new Byte[ChannelXY];
        public Byte[] Data_Out_NOKParts
        {
            get
            {
                return data_Out_NOKParts;
            }

            set
            {
                data_Out_NOKParts = value;
            }
        }

        private static Int32 nOKParts_Value = 0;
        public Int32 NOKParts_Value
        {
            get
            {
                return nOKParts_Value;
            }

            set
            {
                nOKParts_Value = value;
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #endregion

        #region Callbacks

        #endregion

        #region Objects

        #endregion

        public Kistler_5847B0()
        {
            iP = "192.168.1.16";
        }

        #region Functions

        #region Connection
        //Connect to Kistler Module
        bool Connect()
        {
            bool bSucc = false;

            #region EtherCAT
            try
            {

                //Interface (Scan EtherCAT Slave devices)
                List<Tuple<String, String, String>> Interfaces = GetAvailableInterfaces();

                // Get the pcap name of this interface
                String PcapInterfaceName = "\\Device\\NPF_" + Interfaces[1].Item3;

                // Try start with a tiemout delay of 1s
                NumberofSlavesDetected = SoemInterrop.StartActivity(PcapInterfaceName, 1);
                Thread.Sleep(2000);

                //Open the connection
                SoemInterrop.Run();

                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)Kistler_Parameters.CommStatus] = true;
                bSucc = true;
            }
            catch (Exception es)
            {
                //Message
                MessageBox.Show("Kistler module: " + es);
                bSucc = false;
            }
            #endregion

            return bSucc;
        }
        //Disconnect to Kistler Module
        bool Disconnect()
        {
            bool bSucc = false;

            try
            {
                //Close the Session
                SoemInterrop.StopActivity();
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)Kistler_Parameters.CommStatus] = false;
                bSucc = true;
            }
            catch (Exception es)
            {
                //Message
                MessageBox.Show("Kistler module: " + es);
                bSucc = false;
            }

            return bSucc;
        }

        public static List<Tuple<String, String, String>> GetAvailableInterfaces()
        {
            List<Tuple<String, String, String>> ips = new List<Tuple<String, String, String>>();
            System.Net.NetworkInformation.NetworkInterface[] interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (System.Net.NetworkInformation.NetworkInterface inf in interfaces)
            {
                if (!inf.IsReceiveOnly && inf.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up &&
                    inf.SupportsMulticast && inf.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback)
                    ips.Add(new Tuple<string, string, string>(inf.Description, inf.Name, inf.Id));
            }
            return ips;
        }
        #endregion

        #region Data

        #region IO and Data Registers

        #region Control Bits
        //Scan Control Bits Inputs: PC -> maXYmos
        private void Inputs()
        {
            //Device Connected
            if (parameters[0])
            {
                //Read Byte 0 - 220
                for (int i = 0; i < Registers_SIZE; i++)
                {
                    //inputs[i] = eeipClient.O_T_IOData[i];
                }

                #region Control Bits
                for (int i = 0; i < ControlBits_SIZE; i++)
                {
                    //Bool Array <- Byte
                    bool[] section = DINTtoBitArray(inputs[i]);

                    #region Byte 0 - 19
                    switch (i)
                    {

                        #region Byte 0 - 3
                        case 0:
                            //Byte 0
                            for (int j = 0; j < 8; j++)
                            {
                                inputs_03[j] = section[j];
                            }
                            break;
                        case 1:
                            //Byte 1
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                inputs_03[j] = section[k];
                            }
                            break;
                        case 2:
                            //Byte 2
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                inputs_03[j] = section[k];
                            }
                            break;
                        case 3:
                            //Byte 3
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                inputs_03[j] = section[k];
                            }
                            break;
                        #endregion

                        #region Byte 4 - 7
                        case 4:
                            //Byte 4
                            for (int j = 0; j < 8; j++)
                            {
                                inputs_47[j] = section[j];
                            }
                            break;
                        case 5:
                            //Byte 5
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                inputs_47[j] = section[k];
                            }
                            break;
                        case 6:
                            //Byte 6
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                inputs_47[j] = section[k];
                            }
                            break;
                        case 7:
                            //Byte 7
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                inputs_47[j] = section[k];
                            }
                            break;
                        #endregion

                        #region Byte 8 - 11
                        case 8:
                            //Byte 8
                            for (int j = 0; j < 8; j++)
                            {
                                inputs_811[j] = section[j];
                            }
                            break;
                        case 9:
                            //Byte 9
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                inputs_811[j] = section[k];
                            }
                            break;
                        case 10:
                            //Byte 10
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                inputs_811[j] = section[k];
                            }
                            break;
                        case 11:
                            //Byte 11
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                inputs_811[j] = section[k];
                            }
                            break;
                        #endregion

                        #region Byte 12 - 15
                        case 12:
                            //Byte 12
                            for (int j = 0; j < 8; j++)
                            {
                                inputs_1215[j] = section[j];
                            }
                            break;
                        case 13:
                            //Byte 13
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                inputs_1215[j] = section[k];
                            }
                            break;
                        case 14:
                            //Byte 14
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                inputs_1215[j] = section[k];
                            }
                            break;
                        case 15:
                            //Byte 15
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                inputs_1215[j] = section[k];
                            }
                            break;
                        #endregion

                        #region Byte 16 - 19
                        case 16:
                            //Byte 16
                            for (int j = 0; j < 8; j++)
                            {
                                inputs_1619[j] = section[j];
                            }
                            break;
                        case 17:
                            //Byte 17
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                inputs_1619[j] = section[k];
                            }
                            break;
                        case 18:
                            //Byte 18
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                inputs_1619[j] = section[k];
                            }
                            break;
                        case 19:
                            //Byte 19
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                inputs_1619[j] = section[k];
                            }
                            break;
                        #endregion

                        default:
                            break;
                    }

                    #endregion
                }

                #endregion
            }
        }
        //Scan Control Bits Ouputs: maXYmos -> PC
        private void Outputs()
        {
            //Device Connected
            if (parameters[0])
            {
                //Read Byte 0 - 220
                SoemInterrop.GetInput(1, outputs);

                #region Status Bits
                for (int i = 0; i < ControlBits_SIZE; i++)
                {
                    bool[] section = DINTtoBitArray(outputs[i]);

                    #region Byte 0 - 19
                    switch (i)
                    {
                        #region Byte 0 - 3
                        case 0:
                            //Byte 0
                            for (int j = 0; j < 8; j++)
                            {
                                outputs_03[j] = section[j];
                            }
                            break;
                        case 1:
                            //Byte 1
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                outputs_03[j] = section[k];
                            }
                            break;
                        case 2:
                            //Byte 2
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                outputs_03[j] = section[k];
                            }
                            break;
                        case 3:
                            //Byte 3
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                outputs_03[j] = section[k];
                            }
                            break;
                        #endregion

                        #region Byte 4 - 7
                        case 4:
                            //Byte 4
                            for (int j = 0; j < 8; j++)
                            {
                                outputs_47[j] = section[j];
                            }
                            break;
                        case 5:
                            //Byte 5
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                outputs_47[j] = section[k];
                            }
                            break;
                        case 6:
                            //Byte 6
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                outputs_47[j] = section[k];
                            }
                            break;
                        case 7:
                            //Byte 7
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                outputs_47[j] = section[k];
                            }
                            break;
                        #endregion

                        #region Byte 8 - 11
                        case 8:
                            //Byte 8
                            for (int j = 0; j < 8; j++)
                            {
                                outputs_811[j] = section[j];
                            }
                            break;
                        case 9:
                            //Byte 9
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                outputs_811[j] = section[k];
                            }
                            break;
                        case 10:
                            //Byte 10
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                outputs_811[j] = section[k];
                            }
                            break;
                        case 11:
                            //Byte 11
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                outputs_811[j] = section[k];
                            }
                            break;
                        #endregion

                        #region Byte 12 - 15
                        case 12:
                            //Byte 12
                            for (int j = 0; j < 8; j++)
                            {
                                outputs_1215[j] = section[j];
                            }
                            break;
                        case 13:
                            //Byte 13
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                outputs_1215[j] = section[k];
                            }
                            break;
                        case 14:
                            //Byte 14
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                outputs_1215[j] = section[k];
                            }
                            break;
                        case 15:
                            //Byte 15
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                outputs_1215[j] = section[k];
                            }
                            break;
                        #endregion

                        #region Byte 16 - 19
                        case 16:
                            //Byte 16
                            for (int j = 0; j < 8; j++)
                            {
                                outputs_1619[j] = section[j];
                            }
                            break;
                        case 17:
                            //Byte 17
                            for (int j = 8; j < 16; j++)
                            {
                                int k = j - 8;
                                outputs_1619[j] = section[k];
                            }
                            break;
                        case 18:
                            //Byte 18
                            for (int j = 16; j < 24; j++)
                            {
                                int k = j - 16;
                                outputs_1619[j] = section[k];
                            }
                            break;
                        case 19:
                            //Byte 19
                            for (int j = 24; j < 32; j++)
                            {
                                int k = j - 24;
                                outputs_1619[j] = section[k];
                            }
                            break;
                        #endregion

                        default:
                            break;
                    }

                    #endregion
                }

                #endregion
            }
        }
        #endregion

        #region Data
        //Scan Data Inputs: PC -> maXYmos
        private void DataInputs()
        {
            //Device Connected
            if (parameters[0])
            {
                //Read Byte 0 - 220
                //SoemInterrop.SetOutput(1, inputs);

                #region Bus field -> maXYmos Input

                #endregion
            }
        }

        //Scan Data Outputs: maXYmos -> PC
        private void DataOutputs()
        {
            //Device Connected
            if (parameters[0])
            {
                //Read Byte 0 - 220
                SoemInterrop.GetInput(1, outputs);

                #region Bus field <- maXYmos Output
                int j = 0;

                #region Channel X
                //Get the bytes for Channel X: [20-23]
                for (int i = 20; i < 24; i++)
                {
                    j = i - 20;
                    data_Out_ChX[j] = outputs[i];
                }
                //Convert the bytes [20-23] to value
                channelX_Value = BitConverter.ToSingle(data_Out_ChX, 0);
                #endregion

                #region Channel Y
                //Get the bytes for Channel Y: [24-28]
                for (int i = 24; i < 28; i++)
                {
                    j = i - 24;
                    data_Out_ChY[j] = outputs[i];
                }
                //Convert the bytes [24-28] to value
                channelY_Value = BitConverter.ToSingle(data_Out_ChY, 0);
                #endregion

                #region Curve X Min
                //Get the bytes for Curve X Min
                for (int i = 64; i < 68; i++)
                {
                    j = i - 64;
                    data_Out_CurveXMin[j] = outputs[i];
                }
                //Convert the bytes [64-67] to value
                curveXMin_Value = BitConverter.ToSingle(data_Out_CurveXMin, 0);
                #endregion

                #region Curve X Max
                //Get the bytes for Curve X Min
                for (int i = 68; i < 72; i++)
                {
                    j = i - 68;
                    data_Out_CurveXMax[j] = outputs[i];
                }
                //Convert the bytes [68-71] to value
                curveXMax_Value = BitConverter.ToSingle(data_Out_CurveXMax, 0);
                #endregion

                #region Curve Y Min
                //Get the bytes for Curve X Min
                for (int i = 72; i < 76; i++)
                {
                    j = i - 72;
                    data_Out_CurveYMin[j] = outputs[i];
                }
                //Convert the bytes [72-75] to value
                curveYMin_Value = BitConverter.ToSingle(data_Out_CurveYMin, 0);
                #endregion

                #region Curve Y Max
                //Get the bytes for Curve X Min
                for (int i = 76; i < 80; i++)
                {
                    j = i - 76;
                    data_Out_CurveYMax[j] = outputs[i];
                }
                //Convert the bytes [76-79] to value
                curveYMax_Value = BitConverter.ToSingle(data_Out_CurveYMax, 0);
                #endregion

                #region OK Parts
                //Get the bytes for OK Parts: [164-167]
                for (int i = 164; i < 168; i++)
                {
                    j = i - 164;
                    data_Out_OKParts[j] = outputs[i];
                }
                //Convert the bytes [164-167] to value
                oKParts_Value = BitConverter.ToInt32(data_Out_OKParts, 0);

                #endregion

                #region NOK Parts
                //Get the bytes for NG parts: [168-171]
                for (int i = 168; i < 172; i++)
                {
                    j = i - 168;
                    data_Out_NOKParts[j] = outputs[i];
                }
                //Convert the bytes [168-171] to value
                nOKParts_Value = BitConverter.ToInt32(data_Out_NOKParts, 0);
                #endregion

                #endregion
            }
        }

        #endregion

        #endregion

        #region Conversions
        //Integer to Boolean Array
        public bool[] DINTtoBitArray(int Value)
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
            if (!parameters[(int)Kistler_Parameters.Control])
            {
                if (Connect())
                {
                    parameters[(int)Kistler_Parameters.Control] = true;
                    //Active the communication process
                    Thread _Tick = new Thread(new ThreadStart(() => Tick()));
                    _Tick.Start();
                    bSucc = true;
                }
                else
                {
                    parameters[(int)Kistler_Parameters.Control] = false;
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
            while (parameters[(int)Kistler_Parameters.Control])
            {
                Thread.Sleep(50);
                //Scanning digital Inputs
                Inputs();
                //Scanning digital Outputs
                Outputs();
                //Scanning digital Inputs
                DataInputs();
                //Scanning digital Outputs
                DataOutputs();
            }
        }
        //Close the communication
        public bool Close_Communication()
        {
            bool bSucc = false;
            if (parameters[(int)Kistler_Parameters.Control])
            {
                Disconnect();
                parameters[(int)Kistler_Parameters.Control] = false;
                bSucc = true;
            }
            else
            {
                parameters[(int)Kistler_Parameters.Control] = false;
                bSucc = false;
            }

            return bSucc;
        }
        #endregion
    }
}
