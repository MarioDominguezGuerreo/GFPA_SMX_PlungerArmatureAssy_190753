//Mario A. Dominguez Guerrero 
//September - 2020

#region System Libraries
using System;
using System.Collections;
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
    class MTFocus6K
    {
        #region Variables

        #region TCP/IP Communication
        ///Communicacion parameters[#]
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
        private static string port;
        public string Port
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
        private static bool subscribeTightenDone;
        public bool SubscribeTightenDone
        {
            get
            {
                return subscribeTightenDone;
            }

            set
            {
                subscribeTightenDone = value;
            }
        }
        #endregion

        #region Open Protocol: Commands
        //Message
        public const int MAX_COMMAND_SIZE = 20000;
        //Screw Results has been received
        private static bool screwDone;
        public bool ScrewDone
        {
            get
            {
                return screwDone;
            }

            set
            {
                screwDone = value;
            }
        }

        #region Commands to Send

        #region Communication
        //Open Connection
        private const string mID0001 = "002000010060        ";
        public string MID0001
        {
            get
            {
                return mID0001;
            }
        }
        //Close Connection
        private const string mID0003 = "002000030010        ";
        public string MID0003
        {
            get
            {
                return mID0003;
            }
        }
        //Keep Alive the communication
        private const string keepAlive = " P       R    E  =  @            =     H    @ P    [  002099990010";
        public string KeepAlive
        {
            get
            {
                return keepAlive;
            }
        }
        #endregion

        #region Tightening
        //Subscribe Tightening Results
        private const string mID0008 = "006000080010        1201001310000000000000000000000000000001 ";
        public string MID0008
        {
            get
            {
                return mID0008;
            }
        }
        //Unsubscribe Tightening Results
        private const string mID0009 = "002900090010        120100100 ";
        public string MID0009
        {
            get
            {
                return mID0009;
            }
        }
        //Subscribe  Tightening Graphs (MID 0900)
        private const string mID0900sub = "006700080010        0900001380                             02001002";
        public string MID0900sub
        {
            get
            {
                return mID0900sub;
            }
        }
        //Unsubscribe Tightening Graphs 
        private const string mID0900unsub = "003700090010        09000010802001002";
        public string MID0900unsub
        {
            get
            {
                return mID0900unsub;
            }
        }
        #endregion

        #region Program
        //Subscribe Pset Selection MID 0015
        private const string mID0015 = "002900080010        001500100 ";
        public string MID0015
        {
            get
            {
                return mID0015;
            }
        }
        //Unsubscribe Pset Selection
        private const string mID0015a = "002900090010        001500100 ";
        public string MID0015a
        {
            get
            {
                return mID0015a;
            }
        }
        #endregion

        #region Batch
        //Subscribe Bset Selection MID 0035
        private const string mID0035 = "002900080010        003500200 ";
        public string MID0035
        {
            get
            {
                return mID0035;
            }
        }
        //Unsubscribe Bset Selection
        private const string mID0035a = "002900090010        003500100 ";
        public string MID0035a
        {
            get
            {
                return mID0035a;
            }
        }
        #endregion

        #region Screwdriver Control
        //Disable Tool (MID 0224)
        private const string mID0224 = "002302240010        008";
        public string MID0224
        {
            get
            {
                return mID0224;
            }
        }
        //Enable Tool (MID 0225)
        private const string mID0225 = "002302250010        008";
        public string MID0225
        {
            get
            {
                return mID0225;
            }
        }
        #endregion

        #region Pset
        //Pset ID 1
        private const string pSet1 = "002300180010        001";
        public string PSet1
        {
            get
            {
                return pSet1;
            }
        }
        //Pset ID 2
        private const string pSet2 = "002300180010        002";
        public string PSet2
        {
            get
            {
                return pSet2;
            }
        }
        //Pset ID 3
        private const string pSet3 = "002300180010        003";
        public string PSet3
        {
            get
            {
                return pSet3;
            }
        }
        //Pset ID 4
        private const string pSet4 = "002300180010        004";
        public string PSet4
        {
            get
            {
                return pSet4;
            }
        }
        #endregion

        #region Batch
        //Bset ID 1
        private const string bSet1 = "002400380020        0001";
        public string BSet1
        {
            get
            {
                return bSet1;
            }
        }
        //Bset ID 2
        private const string bSet2 = "002400380020        0002";
        public string BSet2
        {
            get
            {
                return bSet2;
            }
        }
        //Bset ID 3
        private const string bSet3 = "002400380020        0003";
        public string BSet3
        {
            get
            {
                return bSet3;
            }
        }
        //Bset ID 4
        private const string bSet4 = "002400380020        0004";
        public string BSet4
        {
            get
            {
                return bSet4;
            }
        }
        #endregion

        #endregion

        #region Commands to Receive

        #region Open Protocol: Header
        //Header Size configuration
        public const int HEADER_SIZE = 9;

        public enum Header
        {
            Length = 4,
            MID = 4,
            Revision = 3,
            NoAckFlag = 1,
            StationID = 2,
            SpindleID = 2,
            SequenceNo = 2,
            MsgParts = 1,
            MsgPartNo = 1
        }
        public int[] Header_Sections = new int[HEADER_SIZE] {   (int)Header.Length,
                                                    (int)Header.MID,
                                                    (int)Header.Revision,
                                                    (int)Header.NoAckFlag,
                                                    (int)Header.StationID,
                                                    (int)Header.SpindleID,
                                                    (int)Header.SequenceNo,
                                                    (int)Header.MsgParts,
                                                    (int)Header.MsgPartNo};
        //Header
        private static string[] header_MID = new string[HEADER_SIZE];
        public string[] Header_MID
        {
            get
            {
                return header_MID;
            }

            set
            {
                header_MID = value;
            }
        }
        #endregion

        #region Open Protocol: Data Result: MID1202
        //Message Size configuration
        public const int MsgData_MID1202_SIZE = 5;
        //Data Field Size configuration
        //The size of the Data field depends of the Pset configuration 
        //Step 1 = [68] positions
        //Step 1 and Step 2 = [83] positions
        //Registers
        private static int dataField_SIZE = 83;
        public int DataField_SIZE
        {
            get
            {
                return dataField_SIZE;
            }

            set
            {
                dataField_SIZE = value;
            }
        }
        //Parameters
        public const int Parameters_SIZE = 6;
        //Message data
        public enum eMsgData_MID1202
        {
            TotalNoOfMsg = 3,
            MessageNo = 3,
            ResultDataID = 10,
            ObjectID = 4,
            DataFieldCount = 3
        }
        public int[] MsgData_MID1202_Sections = new int[MsgData_MID1202_SIZE] {  (int)eMsgData_MID1202.TotalNoOfMsg,
                                                                                 (int)eMsgData_MID1202.MessageNo,
                                                                                 (int)eMsgData_MID1202.ResultDataID,
                                                                                 (int)eMsgData_MID1202.ObjectID,
                                                                                 (int)eMsgData_MID1202.DataFieldCount};

        private static string[] MsgData_mID1202 = new string[MsgData_MID1202_SIZE];
        public string[] MsgData_MID1202
        {
            get
            {
                return MsgData_mID1202;
            }

            set
            {
                MsgData_mID1202 = value;
            }
        }
        //Data Fields
        public enum eDataFields_MID1202
        {
            ParameterID = 5,
            Length = 3,
            DataType = 2,
            Unit = 3,
            StepNo = 4,
            Value = 30
        }
        public int[] DataField_MID1202_Sections = new int[Parameters_SIZE] {   (int)eDataFields_MID1202.ParameterID,
                                                                               (int)eDataFields_MID1202.Length,
                                                                               (int)eDataFields_MID1202.DataType,
                                                                               (int)eDataFields_MID1202.Unit,
                                                                               (int)eDataFields_MID1202.StepNo,
                                                                               (int)eDataFields_MID1202.Value};
        private static string[,] DataField_mID1202 = new string[200, Parameters_SIZE];
        public string[,] DataField_MID1202
        {
            get
            {
                return DataField_mID1202;
            }

            set
            {
                DataField_mID1202 = value;
            }
        }
        #endregion

        #region Open Protocol: Data Result: MID0900
        //Message Size configuration
        public const int MsgData_MID0900_SIZE = 22;
        //Data Field Size configuration
        //Registers
        private static int samples_SIZE_MID0900;
        public int Samples_SIZE_MID0900
        {
            get
            {
                return samples_SIZE_MID0900;
            }

            set
            {
                samples_SIZE_MID0900 = value;
            }
        }
        //Parameters
        public const int Parameters_SIZE_MID0900 = 1;
        //Message data
        public enum eMsgData_MID0900
        {
            ResultDataID = 10,
            TimeStamp = 19,
            DataFieldCount = 3,
            Datafield_ParameterID = 5,
            Datafield_Length = 3,
            Datafield_DataType = 2,
            Datafield_Unit = 3,
            Datafield_StepNo = 4,
            Datafield_Value = 30,
            TraceType = 2,
            TransducerID = 2,
            Unit = 3,
            PrameterFieldCount = 3,
            Parametersfield = 0,
            ResolutionFieldCount = 3,
            Resolutionfields_FirstIndex = 5,
            Resolutionfields_LastIndex = 5,
            Resolutionfields_Length = 3,
            Resolutionfields_DataType = 2,
            Resolutionfields_Unit = 3,
            Resolutionfields_TimeValue = 30,
            SampleCount = 5,
        }
        public int[] MsgData_MID0900_Sections = new int[MsgData_MID0900_SIZE] {  (int)eMsgData_MID0900.ResultDataID,
                                                                                 (int)eMsgData_MID0900.TimeStamp,
                                                                                 (int)eMsgData_MID0900.DataFieldCount,
                                                                                 (int)eMsgData_MID0900.Datafield_ParameterID,
                                                                                 (int)eMsgData_MID0900.Datafield_Length,
                                                                                 (int)eMsgData_MID0900.Datafield_DataType,
                                                                                 (int)eMsgData_MID0900.Datafield_Unit,
                                                                                 (int)eMsgData_MID0900.Datafield_StepNo,
                                                                                 (int)eMsgData_MID0900.Datafield_Value,
                                                                                 (int)eMsgData_MID0900.TraceType,
                                                                                 (int)eMsgData_MID0900.TransducerID,
                                                                                 (int)eMsgData_MID0900.Unit,
                                                                                 (int)eMsgData_MID0900.PrameterFieldCount,
                                                                                 (int)eMsgData_MID0900.Parametersfield,
                                                                                 (int)eMsgData_MID0900.ResolutionFieldCount,
                                                                                 (int)eMsgData_MID0900.Resolutionfields_FirstIndex,
                                                                                 (int)eMsgData_MID0900.Resolutionfields_LastIndex,
                                                                                 (int)eMsgData_MID0900.Resolutionfields_Length,
                                                                                 (int)eMsgData_MID0900.Resolutionfields_DataType,
                                                                                 (int)eMsgData_MID0900.Resolutionfields_Unit,
                                                                                 (int)eMsgData_MID0900.Resolutionfields_TimeValue,
                                                                                 (int)eMsgData_MID0900.SampleCount};

        private static string[] MsgData_mID0900 = new string[MsgData_MID0900_SIZE];
        public string[] MsgData_MID0900
        {
            get
            {
                return MsgData_mID0900;
            }

            set
            {
                MsgData_mID0900 = value;
            }
        }

        //Samples
        public enum eSamples_MID0900
        {
            Value = 1
        }
        public int[] Samples_MID0900_Sections = new int[Parameters_SIZE_MID0900] { (int)eSamples_MID0900.Value };

        //Torque
        private static string[] Samples_angle = new string[2000];
        public string[] Samples_Angle
        {
            get
            {
                return Samples_angle;
            }

            set
            {
                Samples_angle = value;
            }
        }
        //Angle
        private static string[] Samples_torque = new string[2000];
        public string[] Samples_Torque
        {
            get
            {
                return Samples_torque;
            }

            set
            {
                Samples_torque = value;
            }
        }

        #endregion

        #endregion

        #endregion

        static string clamp;
        public string Clamp
        {
            get
            {
                return clamp;
            }

            set
            {
                clamp = value;
            }
        }
        static string clampAngle;
        public string ClampAngle
        {
            get
            {
                return clampAngle;
            }

            set
            {
                clampAngle = value;
            }
        }
        static string seatingPoint;       
        public string SeatingPoint
        {
            get
            {
                return seatingPoint;
            }

            set
            {
                seatingPoint = value;
            }
        }
        #endregion

        #region Callbacks
        #endregion

        #region Objects


        #endregion

        #region Functions

        #region Private
        //Conect to the MT Focus 6K Controller (Server)
        private bool Connect()
        {
            try
            {
                //Open the connection
                //Configuracion del Servidor
                PC_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ServidorDireccion = new IPEndPoint(IPAddress.Parse(iP), Convert.ToInt16(port));
                PC_Client.Connect(ServidorDireccion);
                //Send Command MID to Connect MID 0001
                SendCommand(mID0001);
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)Instrument_Comm.CommStatus] = true;
                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Connection Error\n", "Error");
                return false;
            }
        }
        //Disconnect to the MT Focus 6K Controller (Server)
        private bool Disconnect()
        {
            try
            {
                //Send Command MID to Connect MID 0002
                SendCommand(mID0003);
                Thread.Sleep(200);
                //Close the connection   
                PC_Client.Disconnect(true);
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)Instrument_Comm.CommStatus] = false;
                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Disconnection Error\n", "Error");
                return false;
            }
        }

        //Reading the MT Focus 6K Controller Messages
        private string ReadMessage()
        {
            //Get the message
            byte[] Msg = new byte[MAX_COMMAND_SIZE];
            //Get the message length         
            Int64 Size = PC_Client.Receive(Msg);
            //Convert the message to char array
            char[] RxMasg = ByteArrayToCharArray(Msg, Size);
            //Convert the message  to String
            string MTFocus6K_Rx = CharArrayToString(RxMasg);

            //Restart the Counter Sync 
            int Start = 0;

            try
            {
                if (MTFocus6K_Rx != "")
                {
                    //Read Header
                    //Get the MID 1202 Header
                    string[] MID_Number = Read_Header(RxMasg, Start);

                    if (MID_Number[1] == "1202")
                    {
                        //Counter Sync 
                        Start += SectionByteLength(Header_Sections);
                        //Read the Message data for to get the Data field values
                        string FieldCount = Read_MessageData_MID1202(RxMasg, Start);
                        //Counter Sync 
                        Start += SectionByteLength(MsgData_MID1202_Sections);
                        //Data field SIZE: Total results
                        dataField_SIZE = Convert.ToInt32(FieldCount);
                        //Read the Data Field
                        Read_DataField(RxMasg, out DataField_mID1202, dataField_SIZE, Parameters_SIZE, DataField_MID1202_Sections, Start);
                        //Counter Sync 
                        Start = Convert.ToInt32(MID_Number[0]) + 1;
                        //Get the MID 0900 Header for angle Trace 
                        MID_Number = Read_Header(RxMasg, Start);
                        //Get the Trace samples
                        if (MID_Number[1] == "0900")
                        {
                            //Counter Sync 
                            Start += SectionByteLength(Header_Sections);
                            //Read the Message Data for to get Coefficient, Trace Type, Sample Count to define the samples values of the Torque curve
                            Read_MessageData_MID0900(RxMasg, Start);
                            //SampleCount
                            int SampleCount = Convert.ToInt32(MsgData_mID0900[21]);
                            double Coefficient = Convert.ToDouble(MsgData_mID0900[8]);
                            //Counter Sync 
                            Start += SectionByteLength(MsgData_MID0900_Sections);
                            //Get the Samples values for angle Trace
                            Read_Samples(RxMasg, out Samples_angle, SampleCount, Coefficient, Start, out Start);

                            //Get the MID 0900 Header for torque Trace 
                            MID_Number = Read_Header(RxMasg, Start);
                            if (MID_Number[1] == "0900")
                            {
                                //Counter Sync 
                                Start += SectionByteLength(Header_Sections);
                                //Read the Message Data for to get Coefficient, Trace Type, Sample Count to define the samples values of the Angle curve
                                Read_MessageData_MID0900(RxMasg, Start);
                                //SampleCount
                                SampleCount = Convert.ToInt32(MsgData_mID0900[21]);
                                Coefficient = Convert.ToDouble(MsgData_mID0900[8]);
                                //Counter Sync 
                                Start += SectionByteLength(MsgData_MID0900_Sections);
                                //Get the Samples values for torque Trace
                                Read_Samples(RxMasg, out Samples_torque, SampleCount, Coefficient, Start, out Start);
                            }

                            Diagnostics.ODiagnostics.TorqueAngleResults(SampleCount);
                        }
                        //Clamp Torque and Seating Point torque
                        //if the Screwdring process was successful, check the clamp and seating point torques
                        if (DataField_MID1202[2, 5] == "1")
                        {
                            clamp = DataField_MID1202[81, 5];
                            clampAngle = DataField_MID1202[82, 5];
                            seatingPoint = DataField_MID1202[86, 5];
                        }
                        else
                        {
                            clamp = "0.0";
                            clampAngle = "0.0";
                            seatingPoint = "0.0";
                        }
                        
                        //Screw Results have been received
                        screwDone = true;
                    }
                }
            }
            catch (Exception)
            {
                //Screw Results have been received
                screwDone = true;
                //Message
                HMI.OForm.SystemMessages("MT Focus 6K Rx Comm Error\n", "Error");
            }
            return MTFocus6K_Rx;
        }

        //Read the Header of the message
        private string[] Read_Header(char[] Message, int Start)
        {
            //Header
            Header_MID = Read_Section_1D(Message, HEADER_SIZE, Header_Sections, Start);
            //Get the MID number 
            string[] MID_Number = Header_MID;

            return MID_Number;
        }
        //Read the MID 1202 Message Data
        private string Read_MessageData_MID1202(char[] Message, int Start)
        {
            //Header
            MsgData_mID1202 = Read_Section_1D(Message, MsgData_MID1202_SIZE, MsgData_MID1202_Sections, Start);
            //Get the MID number 
            return MsgData_mID1202[4];
        }
        //Read the MID 1202 Data Field
        public void Read_DataField(char[] Message, out string[,] DataField, int DataField_SIZE, int Parameters_SIZE, int[] Parameters_Length, int carry_in)
        {
            //Data Field
            DataField = Read_Section_2D_MID1202(Message, DataField_SIZE, Parameters_SIZE, Parameters_Length, carry_in);
        }
        //Read the MID 0900 Message Data
        private string[] Read_MessageData_MID0900(char[] Message, int Start)
        {
            //Header
            MsgData_mID0900 = Read_Section_1D_MID0900(Message, MsgData_MID0900_SIZE, MsgData_MID0900_Sections, Start);
            return MsgData_mID0900;
        }
        //Read the MID 0900 Samples
        public void Read_Samples(char[] Message, out string[] Samples, int Sample_SIZE, double Coefficient, int carry_in, out int carry_out)

        {
            //Samples
            Samples = Read_Samples_MID0900(Message, Sample_SIZE, Coefficient, carry_in, out carry_out);
        }

        #region Convertions
        //Read Sections 2D MID 1202
        public string[,] Read_Section_2D_MID1202(char[] Message, int SectionA_Length, int SectionB_Length, int[] Parameters_Length, int carry_in)
        {
            //Define the Section to read
            string[,] Section = new string[SectionA_Length, SectionB_Length];
            //Define the length of the Last "Value" parameter on base of the "Length" Parameter
            int Next_ValueLength = 0;
            string ValueLength = "0";
            //Counter secuence
            int j = carry_in;
            //Start reading
            for (int i = 0; i < SectionA_Length; i++)
            {
                for (int k = 0; k < SectionB_Length; k++)
                {
                    //Select the value from the parameter 1 (DataField.Length)
                    if (Section[i, 1] != null)
                    {
                        ValueLength = Section[i, 1];
                    }
                    //Assign the new value for the parameter 5 (DataField.Value)
                    if (k == 5)
                    {
                        //Convert the Value to Integer
                        Next_ValueLength = Convert.ToInt16(ValueLength);
                    }
                    else
                    {
                        Next_ValueLength = Parameters_Length[k];
                    }

                    Section[i, k] = Counter(j, Next_ValueLength, Message);
                    j += Next_ValueLength;
                }
            }
            //Result
            return Section;
        }
        //Read sections 1D MID 1202
        public string[] Read_Section_1D(char[] Message, int Section_Length, int[] Parameters_Length, int carry_in)
        {
            //Define the Section to read
            string[] Section = new string[Section_Length];
            //Counter secuence
            int j = carry_in;
            //Start reading
            for (int i = 0; i < Section_Length; i++)
            {
                Section[i] = Counter(j, Parameters_Length[i], Message);
                j += Parameters_Length[i];
            }
            //Result
            return Section;
        }
        //Read sections 1D MID 0900
        public string[] Read_Section_1D_MID0900(char[] Message, int Section_Length, int[] Parameters_Length, int carry_in)
        {
            //Define the Section to read
            string[] Section = new string[Section_Length];
            //Define the length of the Last "Value" parameter on base of the "Length" Parameter
            int Next_ValueLength = 0;
            string ValueLength = "0";
            //Counter secuence
            int j = carry_in;
            //Start reading
            for (int i = 0; i < Section_Length; i++)
            {
                //Select the value from the parameter 4 (DataField.Length)
                if (Section[4] != null)
                {
                    ValueLength = Section[4];
                }
                //Select the value from the parameter 17 (ResolutionField.Length)
                if (Section[17] != null)
                {
                    ValueLength = Section[17];
                }
                //Assign the new value for the parameter 8 (DataField.Value) or parameter 20 (ResolutionField.Value)
                if (i == 8 || i == 20)
                {
                    //Convert the Value to Integer
                    Next_ValueLength = Convert.ToInt16(ValueLength);
                }
                else
                {
                    Next_ValueLength = Parameters_Length[i];
                }
                //Get values from the Message
                Section[i] = Counter(j, Next_ValueLength, Message);

                MsgData_MID0900_Sections[i] = Section[i].Length;

                j += Next_ValueLength;
            }
            //Result
            return Section;
        }
        //Read Samples's Sections
        public string[] Read_Samples_MID0900(char[] Message, int SectionA_Length, double Coefficient, int carry_in, out int carry_out)
        {
            //Define the Section to read
            string[] Section = new string[SectionA_Length];
            //Counter secuence
            int j = carry_in;
            int k = carry_in;

            //Start reading ASCII values
            for (int i = 0; i < SectionA_Length; i++)
            {
                Section[i] = ASCIItoDecimalValues(k, Coefficient, Message);
                k += 2;
            }
            carry_out = k + 1;
            //carry_out = j + 1;
            //Result
            return Section;
        }

        //Counter
        private string Counter(int StartCount, int Length, char[] Message)
        {
            string Result = "";

            for (int i = StartCount; i < StartCount + Length; i++)
            {

                Result += Message[i];
            }
            return Result;
        }
        //Counter
        private string ASCIItoDecimalValues(int StartCount, double Coefficient, char[] Message)
        {
            string Result = "";
            int value = 0;
            string value_init = "";
            /// Concatenate 2 chars.
            /// [Byte 1]
            /// [Byte 2]
            /// Binary conversion and Concatenation
            /// └ [Byte 1 Byte 2]
            ///   └ [0000000000000000]

            for (int i = StartCount; i < StartCount + 2; i++)
            {
                //BIN Conversion 
                value_init = Convert.ToString(Convert.ToByte(Convert.ToChar(Message[i])), 2);
                value = Convert.ToInt32(value_init);
                //Concatenation of 2 bytes BIN string 
                Result += value.ToString("D8");
            }

            //INTEGER Conversion, [0000000000000000] = [0]
            double RealResult = Convert.ToInt32(Convert.ToInt64(Result, 10).ToString("D8"), 2);
            RealResult *= Coefficient;
            Result = RealResult.ToString();

            return Result;
        }
        //Measure the Section Length
        private int SectionByteLength(int[] Section)
        {
            int Result = 0;

            for (int i = 0; i < Section.Length; i++)
            {
                Result += Section[i];
            }

            return Result;
        }
        //Converter Byte Array to Char Array
        private char[] ByteArrayToCharArray(byte[] Message, Int64 Size)
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
        private string CharArrayToString(char[] Message)
        {
            string resultado = "";

            int k = 0;
            for (int i = 0; i < Message.Length; i++)
            {
                //Adding characters to String
                resultado += Message[i].ToString();
                k++;
            }

            if (k == Message.Length)
            {
                Console.WriteLine(k);
            }

            return resultado;
        }
        #endregion

        #endregion

        #region Public
        public void SendCommand(string Command)
        {
            PC_Client.Send(System.Text.Encoding.ASCII.GetBytes(Command), 0, Command.Length, SocketFlags.None);
        }
        private static void SendCommand()
        {
            PC_Client.Send(System.Text.Encoding.ASCII.GetBytes(mID0008), 0, mID0008.Length, SocketFlags.None);
        }
        #endregion

        #endregion

        #region Threads
        //Open the communication
        public bool Open_Communication()
        {
            if (!parameters[(int)Instrument_Comm.Control])
            {
                //Connect to the MTFocus6K
                if (Connect())
                {
                    parameters[(int)Instrument_Comm.Control] = true;
                    //Active the communication process
                    Thread Tick = new Thread(new ThreadStart(() => Communication()));                
                    Tick.Start();
                    return true;
                }
                else
                {
                    parameters[(int)Instrument_Comm.Control] = false;
                    return false;
                }
            }
            else
                return false;
        }
        //Communication Proccess
        private void Communication()
        {
            while (parameters[(int)Instrument_Comm.CommStatus])
            {
                //Reading the MT Focus 6K Controller Messages  
                ReadMessage();
                Thread.Sleep(500);
            }
        }
        //Close the communication
        public bool Close_Communication()
        {
            if (parameters[(int)Instrument_Comm.Control])
            {
                Disconnect();
                parameters[(int)Instrument_Comm.Control] = false;
                return true;
            }
            else
            {
                parameters[(int)Instrument_Comm.Control] = false;
                return false;
            }
        }
        #endregion
    }
}
