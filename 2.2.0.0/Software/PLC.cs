//Mario A. Dominguez Guerrero 
//July - 2020

#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
#endregion

#region Project Libraries
using ActUtlTypeLib;
using AxActUtlTypeLib;
#endregion

namespace Software
{
    class PLC
    {
        #region Variables
        //PLC Mitsubishi iQR04 Parameters
        //MX Component Configuration
        //Station Logical Number
        private static int logicalStationNumber = 1;
        public int LogicalStationNumber
        {
            get
            {
                return logicalStationNumber;
            }
        }
        //IP
        private static string iP = "192.168.1.21";
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

        ///Communicacion parameters[#]
        /// 0 = Communication Status (Disconnected = 0, Connected = 1)
        /// 1 = Read / Write Coils, (Read = 0, Write = 1)
        /// 2 = Control (Disable = 0 / Enable = 1)
        private const int MAX_COMM_PARAM = 4;
        private static bool[] parameters = new bool[MAX_COMM_PARAM] { false, false, false, false };
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

        //MX Component Digital IO

        #region Digital Inputs
        //Inputs
        #region PLC (Slot 0): (M0-M15) 
        //PLC (Slot 0): K2X0 -> K2M0  (M0-M7)  
        private const string K2M0 = "K2M0";
        private static Int32 read_K2M0;
        public int Read_K2M0
        {
            get
            {
                return read_K2M0;
            }

            set
            {
                read_K2M0 = value;
            }
        }

        private static bool[] k2M0_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M0_Array
        {
            get
            {
                return k2M0_Array;
            }

            set
            {
                k2M0_Array = value;
            }
        }

        //PLC (Slot 0): K2X8 -> K2M8  (M8-M15)
        private const string K2M8 = "K2M8";
        private static Int32 read_K2M8;
        public int Read_K2M8
        {
            get
            {
                return read_K2M8;
            }

            set
            {
                read_K2M8 = value;
            }
        }

        private static bool[] k2M8_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M8_Array
        {
            get
            {
                return k2M8_Array;
            }

            set
            {
                k2M8_Array = value;
            }
        }
        #endregion

        #region PLC (Slot 2): (M48-M63)
        //PLC (Slot 2): K2X20 -> K2M48  (M48-M55)
        private const string K2M48 = "K2M48";
        private static Int32 read_K2M48;
        public int Read_K2M48
        {
            get
            {
                return read_K2M48;
            }

            set
            {
                read_K2M48 = value;
            }
        }
        private static bool[] k2M48_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M48_Array
        {
            get
            {
                return k2M48_Array;
            }

            set
            {
                k2M48_Array = value;
            }
        }

        //PLC (Slot 2): K2X28 -> K2M56  (M56-M63)
        private const string K2M56 = "K2M56";
        private static Int32 read_K2M56;
        public int Read_K2M56
        {
            get
            {
                return read_K2M56;
            }

            set
            {
                read_K2M56 = value;
            }
        }
        private static bool[] k2M56_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M56_Array
        {
            get
            {
                return k2M56_Array;
            }

            set
            {
                k2M56_Array = value;
            }
        }
        #endregion

        #endregion

        #region Digital outputs
        //Outputs
        #region PLC (Slot 1): (M16-M31)
        //PLC (Slot 1): K2Y10 -> K2M16      
        private const string K2M16 = "K2M16";
        private static Int32 read_K2M16;
        public int Read_K2M16
        {
            get
            {
                return read_K2M16;
            }

            set
            {
                read_K2M16 = value;
            }
        }

        private static bool[] k2M16_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M16_Array
        {
            get
            {
                return k2M16_Array;
            }

            set
            {
                k2M16_Array = value;
            }
        }

        //PLC (Slot 1): K2Y18 -> K2M24   
        private const string K2M24 = "K2M24";
        private static Int32 read_K2M24;
        public int Read_K2M24
        {
            get
            {
                return read_K2M24;
            }

            set
            {
                read_K2M24 = value;
            }
        }

        private static bool[] k2M24_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M24_Array
        {
            get
            {
                return k2M24_Array;
            }

            set
            {
                k2M24_Array = value;
            }
        }

        //PLC (Write): K2Y10 -> K2M32      
        private const string K2M32 = "K2M32";
        private static Int32 read_K2M32;
        public int Read_K2M32
        {
            get
            {
                return read_K2M32;
            }

            set
            {
                read_K2M32 = value;
            }
        }

        private static bool[] k2M32_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M32_Array
        {
            get
            {
                return k2M32_Array;
            }

            set
            {
                k2M32_Array = value;
            }
        }

        //PLC (Write): K2Y18 -> K2M40   
        private const string K2M40 = "K2M40";
        private static Int32 read_K2M40;
        public int Read_K2M40
        {
            get
            {
                return read_K2M40;
            }

            set
            {
                read_K2M40 = value;
            }
        }

        private static bool[] k2M40_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M40_Array
        {
            get
            {
                return k2M40_Array;
            }

            set
            {
                k2M40_Array = value;
            }
        }
        #endregion

        #region PLC (Slot 3): (M64-M79)
        //PLC (Slot 3): K2Y36 -> K2M64 
        private const string K2M64 = "K2M64";
        private static Int32 read_K2M64;
        public int Read_K2M64
        {
            get
            {
                return read_K2M64;
            }

            set
            {
                read_K2M64 = value;
            }
        }

        private static bool[] k2M64_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M64_Array
        {
            get
            {
                return k2M64_Array;
            }

            set
            {
                k2M64_Array = value;
            }
        }

        //PLC (Slot 3): K2Y44 -> K2M72   
        private const string K2M72 = "K2M72";
        private static Int32 read_K2M72;
        public int Read_K2M72
        {
            get
            {
                return read_K2M72;
            }

            set
            {
                read_K2M72 = value;
            }
        }

        private static bool[] k2M72_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M72_Array
        {
            get
            {
                return k2M72_Array;
            }

            set
            {
                k2M72_Array = value;
            }
        }
        #endregion

        #endregion

        //MX Component Flags
        #region General purpose Flags

        #region Magnet Feeder Left Alarms
        /// PLC Flags: M200 - M207
        private const string K2M200 = "K2M200";
        private static Int32 read_K2M200;
        public int Read_K2M200
        {
            get
            {
                return read_K2M200;
            }

            set
            {
                read_K2M200 = value;
            }
        }

        private static bool[] k2M200_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M200_Array
        {
            get
            {
                return k2M200_Array;
            }
        }

        //Lock Pop up messages
        #region Locks
        private static bool[] alarmsMFLLock = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] AlarmsMFLLock
        {
            get
            {
                return alarmsMFLLock;
            }

            set
            {
                alarmsMFLLock = value;
            }
        }
        #endregion

        #endregion

        #region Magnet Feeder Right Alarms
        /// PLC Flags: M208 - M2015
        private const string K2M208 = "K2M208";
        private static Int32 read_K2M208;
        public int Read_K2M208
        {
            get
            {
                return read_K2M208;
            }

            set
            {
                read_K2M208 = value;
            }
        }

        private static bool[] k2M208_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M208_Array
        {
            get
            {
                return k2M208_Array;
            }
        }

        //Lock Pop up messages
        #region Locks
        private static bool[] alarmsMFRLock = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] AlarmsMFRLock
        {
            get
            {
                return alarmsMFRLock;
            }

            set
            {
                alarmsMFRLock = value;
            }
        }
        #endregion

        #endregion

        #region Press Alarms
        /// PLC Flags: M116 - M124
        private const string K2M216 = "K2M216";
        private static Int32 read_K2M216;
        public int Read_K2M216
        {
            get
            {
                return read_K2M216;
            }
        }

        private static bool[] k2M216_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M216_Array
        {
            get
            {
                return k2M216_Array;
            }
        }

        //Lock Pop up messages
        #region Locks
        private static bool[] pressLock = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] PressLock
        {
            get
            {
                return pressLock;
            }

            set
            {
                pressLock = value;
            }
        }
        #endregion

        #endregion

        #region Marker Arm Alarms
        /// PLC Flags: M224 - M231
        private const string K2M224 = "K2M224";
        private static Int32 read_K2M224;
        public int Read_K2M224
        {
            get
            {
                return read_K2M224;
            }
        }

        private static bool[] k2M224_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M224_Array
        {
            get
            {
                return k2M224_Array;
            }
        }

        //Lock Pop up messages
        #region Locks
        private static bool[] markerArmLock = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] MarkerArmLock
        {
            get
            {
                return markerArmLock;
            }

            set
            {
                markerArmLock = value;
            }
        }
        #endregion

        #endregion

        #region Marker Alarms
        /// PLC Flags: M116 - M123
        private const string K2M232 = "K2M232";
        private static Int32 read_K2M232;
        public int Read_K2M232
        {
            get
            {
                return read_K2M232;
            }
        }

        private static bool[] k2M232_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M232_Array
        {
            get
            {
                return k2M232_Array;
            }
        }

        //Lock Pop up messages
        #region Locks
        private static bool[] markerLock = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] MarkerLock
        {
            get
            {
                return markerLock;
            }

            set
            {
                markerLock = value;
            }
        }
        #endregion

        #endregion

        #region Safety Alarms
        /// PLC Flags: M124 - M131
        private const string K2M240 = "K2M240";
        private static Int32 read_K2M240;
        public int Read_K2M240
        {
            get
            {
                return read_K2M240;
            }
        }

        private static bool[] k2M240_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K2M240_Array
        {
            get
            {
                return k2M240_Array;
            }
        }

        //Lock Pop up messages
        #region Locks
        private static bool[] safetyLock = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] SafetyLock
        {
            get
            {
                return safetyLock;
            }

            set
            {
                safetyLock = value;
            }
        }
        #endregion

        #endregion

        #region PC Flags: Communication purpose (Inputs)
        /// PLC Flags: M132 - M147
        private const string K4M132 = "K4M132";
        private static Int32 read_K4M132;
        public int Read_K4M132
        {
            set
            {
                read_K4M132 = value;
            }
            get
            {
                return read_K4M132;
            }
        }

        private static bool[] k4M132_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K4M132_Array
        {
            set
            {
                k4M132_Array = value;
            }
            get
            {
                return k4M132_Array;
            }
        }

        #endregion

        #region PC Flags: Communication purpose (Outputs)
        /// PLC Flags: M148 - M163
        private const string K4M148 = "K4M148";
        private static Int32 read_K4M148;
        public int Read_K4M148
        {
            set
            {
                read_K4M148 = value;
            }
            get
            {
                return read_K4M148;
            }
        }

        private static bool[] k4M148_Array = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] K4M148_Array
        {
            set
            {
                k4M148_Array = value;
            }
            get
            {
                return k4M148_Array;
            }
        }

        #endregion

        #region Events
        

        #endregion

        #endregion

        #endregion

        #region Callbacks

        #endregion

        #region Objects
        //PLC Mitsubishi iQR04
        public ActUtlType CPU = new ActUtlType();
        #endregion

        #region Controls

        #endregion

        #region Information

        #endregion

        #region Functions

        #region Public

        #endregion

        #region Private
        //Conect to the Modbus TCP Module
        private bool Connect()
        {
            try
            {
                //Open the connection
                CPU.ActLogicalStationNumber = LogicalStationNumber;
                CPU.Open();
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)PLC_Comm.CommStatus] = true;
                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Connection Error\n", "Error");
                return false;
            }
        }
        //Disconnect to the Modbus TCP Module
        private bool Disconnect()
        {
            try
            {
                //Close the connection   
                CPU.Close();
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)PLC_Comm.CommStatus] = false;
                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Disconnection Error\n", "Error");
                return false;
            }
        }

        //Scan Digital Inputs
        private void DigitalInputs()
        {
            //PLC Connected
            if (parameters[(int)PLC_Comm.CommStatus])
            {
                //Read Inputs
                Thread.Sleep(50);

                #region PLC (Slot 0): (M0-M15) 
                //Read Inputs K2M0
                CPU.GetDevice(K2M0, out read_K2M0);
                k2M0_Array = DINTtoBitArray(read_K2M0);
                //Read Inputs K2M8
                CPU.GetDevice(K2M8, out read_K2M8);
                k2M8_Array = DINTtoBitArray(read_K2M8);
                #endregion

                #region PLC (Slot 2): (M48-M63)
                //Read Inputs K2M48     
                CPU.GetDevice(K2M48, out read_K2M48);
                k2M48_Array = DINTtoBitArray(read_K2M48);
                //Read Inputs K2M56      
                CPU.GetDevice(K2M56, out read_K2M56);
                k2M56_Array = DINTtoBitArray(read_K2M56);
                #endregion
            }
        }
        //Scan Digital Ouputs
        private void DigitalOutputs()
        {
            //PLC Connected
            if (parameters[(int)PLC_Comm.CommStatus])
            {
                //Read Inputs
                Thread.Sleep(50);
                //Manual Mode is activated
                if (parameters[(int)PLC_Comm.RW_Coils])
                {
                    #region PLC (Slot 1): (M32-M47)
                    //Write Coils K2M32
                    int DataK2M32 = BooleanArrayToDINT(k2M32_Array);
                    CPU.SetDevice(K2M32, DataK2M32);

                    //Write Coils K2M40
                    int DataK2M40 = BooleanArrayToDINT(k2M40_Array);
                    CPU.SetDevice(K2M40, DataK2M40);
                    #endregion

                    #region PLC (Slot 3): (M80-M95) Disable

                    #endregion
                }
                //Automatic Mode is activated
                else
                {
                    #region PLC (Slot 1): (M16-M31)
                    //Read Coils K2M16
                    CPU.GetDevice(K2M16, out read_K2M16);
                    k2M16_Array = DINTtoBitArray(read_K2M16);
                    //Read Coils K2M24
                    CPU.GetDevice(K2M24, out read_K2M24);
                    k2M24_Array = DINTtoBitArray(read_K2M24);
                    #endregion

                    #region PLC (Slot 3): (M64-M79) Disable
                    //Read Coils K2M64     
                    CPU.GetDevice(K2M64, out read_K2M64);
                    k2M64_Array = DINTtoBitArray(read_K2M64);
                    //Read Coils K2M72      
                    CPU.GetDevice(K2M72, out read_K2M72);
                    k2M72_Array = DINTtoBitArray(read_K2M72);
                    #endregion
                }
            }
        }
        //Scan the PC Flags: Communication purpose (Inputs)
        private void In_PCFlags()
        {
            //PLC Connected
            if (parameters[(int)PLC_Comm.CommStatus])
            {
                //Write Coils K4M132
                int DataK4M132 = BooleanArrayToDINT(k4M132_Array);
                CPU.SetDevice(K4M132, DataK4M132);
            }
        }
        //Scan the PC Flags: Communication purpose (Outputs)
        private void Out_PCFlags()
        {
            //PLC Connected
            if (parameters[(int)PLC_Comm.CommStatus])
            {
                //Read Coils K2M72      
                CPU.GetDevice(K4M148, out read_K4M148);
                k4M148_Array = DINTtoBitArray(read_K4M148);
            }
        }
        //Scan Alarms
        private void Alarms()
        {
            //PLC Connected
            if (parameters[(int)PLC_Comm.CommStatus])
            {
                //Read Inputs
                Thread.Sleep(50);
                //Array K2M200
                CPU.GetDevice(K2M200, out read_K2M200);
                k2M200_Array = DINTtoBitArray(read_K2M200);
                //Array K2M208
                CPU.GetDevice(K2M208, out read_K2M208);
                k2M208_Array = DINTtoBitArray(read_K2M208);
                //Array K2M216
                CPU.GetDevice(K2M216, out read_K2M216);
                k2M216_Array = DINTtoBitArray(read_K2M216);
                //Array K2M224
                CPU.GetDevice(K2M224, out read_K2M224);
                k2M224_Array = DINTtoBitArray(read_K2M224);
                //Array K2M224
                CPU.GetDevice(K2M232, out read_K2M232);
                k2M232_Array = DINTtoBitArray(read_K2M232);
                //Array K2M224
                CPU.GetDevice(K2M240, out read_K2M240);
                k2M240_Array = DINTtoBitArray(read_K2M240);
            }
        }
        //Scan Events
        private void Events()
        {
            //PLC Connected
            if (parameters[(int)PLC_Comm.CommStatus])
            {
                //Read Inputs
                Thread.Sleep(50);

            }
        }

        #region Conversions DINT / Boolean
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
        #endregion
        
        #endregion

        #endregion

        #region Threads
        //Open the communication
        public bool Open_Communication()
        {
            if (!parameters[(int)PLC_Comm.Control])
            {
                if (Connect())
                {
                    parameters[(int)PLC_Comm.Control] = true;
                    //Active the communication process
                    Thread Tick = new Thread(new ThreadStart(() => Communication()));
                    Tick.Start();
                    return true;
                }
                else
                {
                    parameters[(int)PLC_Comm.Control] = false;
                    return false;
                }
            }
            else
                return false;
        }
        //Scan process for Digital IO
        private void Communication()
        {
            while (parameters[(int)PLC_Comm.Control])
            {
                Thread.Sleep(50);
                //Scanning digital Inputs
                DigitalInputs();
                //Scanning digital Outputs
                DigitalOutputs();
                //Scan the PC Flags: Communication purpose (Inputs)
                In_PCFlags();
                //Scan the PC Flags: Communication purpose (Outputs)
                Out_PCFlags();
                //Scanning Alarms
                Alarms();
                //Scanning Events
                Events();
            }
        }
        //Close the communication
        public bool Close_Communication()
        {
            if (parameters[(int)PLC_Comm.Control])
            {
                Disconnect();
                parameters[(int)PLC_Comm.Control] = false;
                return true;
            }
            else
            {
                parameters[(int)PLC_Comm.Control] = false;
                return false;
            }
        }
        #endregion
    }
}
