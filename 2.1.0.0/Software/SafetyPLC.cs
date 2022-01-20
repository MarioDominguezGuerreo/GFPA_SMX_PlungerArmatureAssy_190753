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
using EasyModbus;
#endregion

namespace Software
{
    class SafetyPLC
    {
        #region Variables

        #region Modbus TCP: Communications
        //Modbus TCP parameters
        //IP
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
        //Port
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
        #endregion

        #region Modbus TCP: IO
        //SICK FX0-GMOD as slave — Data addressing
        //1100 Get input data set 1 data [0-49]
        public const int InputDataSet1 = 1100;
        public const int InputDataSet1_size = 50;
        public int[] GetHoldingRegister;
        //1999 Set input data set 1 data [0-9]
        public const int OutputDataSet1 = 1999;
        public const int OutputDataSet1_size = 10;
        public int[] SetHoldingRegister = new int[OutputDataSet1_size];
        //Modbus TCP Digital IO
        #region Inputs
        //XTIO Module 1 and 2
        private static bool[] inputs = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Inputs
        {
            get
            {
                return inputs;
            }

            set
            {
                inputs = value;
            }
        }
        //XTIO Module 3 and 4
        private static bool[] inputs2 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Inputs2
        {
            get
            {
                return inputs2;
            }

            set
            {
                inputs2 = value;
            }
        }
        //Flags to Read: DirectOut 28 (PLC -> PC)
        private static bool[] directOut28 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] DirectOut28
        {
            get
            {
                return directOut28;
            }

            set
            {
                directOut28 = value;
            }
        }
        //Flags to Read: DirectOut 28 (PLC -> PC)
        private static bool[] directOut30 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] DirectOut30
        {
            get
            {
                return directOut30;
            }

            set
            {
                directOut30 = value;
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
        //Lock Pop up messages
        #region Locks
        private static bool[] lockerLock = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] LockerLock
        {
            get
            {
                return lockerLock;
            }

            set
            {
                lockerLock = value;
            }
        }
        #endregion
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

        #region Outputs
        //XTIO Module 1 and 2
        private static bool[] outputs = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Outputs
        {
            get
            {
                return outputs;
            }

            set
            {
                outputs = value;
            }
        }
        //XTIO Module 3 and 4
        private static bool[] outputs2 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] Outputs2
        {
            get
            {
                return outputs2;
            }

            set
            {
                outputs2 = value;
            }
        }
        //Flags to Write: SB1.B0 (PC -> PLC)
        private static bool[] s1B0_1 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] S1B0_1
        {
            get
            {
                return s1B0_1;
            }

            set
            {
                s1B0_1 = value;
            }
        }
        //Flags to Write: SB1.B2 (PC -> PLC)
        private static bool[] s1B2_3 = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool[] S1B2_3
        {
            get
            {
                return s1B2_3;
            }

            set
            {
                s1B2_3 = value;
            }
        }
        #endregion

        #endregion

        #endregion

        #region Callbacks

        #endregion

        #region Objects

        // IP: 192.168.37.32 and PORT: 502 for SICK Modbus TCP gateway module (FX0-GMOD00000)
        ModbusClient modbusClient = new ModbusClient(iP, port);
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
                modbusClient.Connect(iP,port);
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)PLC_Comm.CommStatus] = modbusClient.Connected;
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
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)PLC_Comm.Control] = false;
                //Close the connection   
                modbusClient.Disconnect();
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)PLC_Comm.CommStatus] = modbusClient.Connected;
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
            //Device Connected
            if (parameters[(int)PLC_Comm.Control])
            {
                try
                {
                    //Read Input Data Set 1
                    GetHoldingRegister = modbusClient.ReadHoldingRegisters(InputDataSet1, InputDataSet1_size);
                    //XTIO Module 1 and 2
                    inputs = DINTtoBitArray(GetHoldingRegister[1]);
                    //XTIO Module 3 and 4
                    inputs2 = DINTtoBitArray(GetHoldingRegister[2]);

                    //DirectOut 28 - 29
                    directOut28 = DINTtoBitArray(GetHoldingRegister[13]);
                    //DirectOut 30 - 31
                    directOut30 = DINTtoBitArray(GetHoldingRegister[14]);
                }
                catch (Exception)
                {
                    //Message
                    HMI.OForm.SystemMessages("Inputs: Communication has been lost\n", "Error");
                }        
            }
        }
        //Scan Digital Ouputs
        private void DigitalOutputs()
        {
            //Device Connected
            if (parameters[(int)PLC_Comm.Control])
            {
                try
                {
                    //Write Output Data Set 1: SB1.B0 - SB1.B1
                    SetHoldingRegister[0] = BooleanArrayToDINT16(s1B0_1);
                    modbusClient.WriteMultipleRegisters(OutputDataSet1, SetHoldingRegister);
                    //Write Output Data Set 1: SB1.B2 - SB1.B3
                    SetHoldingRegister[1] = BooleanArrayToDINT16(s1B2_3);
                    modbusClient.WriteMultipleRegisters(OutputDataSet1, SetHoldingRegister);

                    //Read Output Data Set 1
                    //XTIO Module 1 and 2
                    outputs = DINTtoBitArray(GetHoldingRegister[7]);
                    //XTIO Module 3 and 4
                    outputs2 = DINTtoBitArray(GetHoldingRegister[8]);
                }
                catch (Exception)
                {
                    //Message
                    HMI.OForm.SystemMessages("Outputs: Communication has been lost\n", "Error");
                }          
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
            }
        }
        //Close the communication
        public bool Close_Communication()
        {
            if (parameters[(int)PLC_Comm.Control])
            {
                parameters[(int)PLC_Comm.Control] = false;
                Disconnect();
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
