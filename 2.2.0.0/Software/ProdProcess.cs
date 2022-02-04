//Mario A. Dominguez Guerrero 
//January - 2022

#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

#region Project Libraries

#endregion

namespace Software
{
    class ProdProcess
    {
        #region Variables

        string ResultCode = "No Result";

        #region Process parameters
        ///[0] = Process 1
        ///[1] = Process 2
        ///[2] = Process 3
        ///[3] = Process 4
        private const int MAX_PROCESS_PARAM = 4;
        //Process Status & Control (0 = Stopped, 1 = Running, 2 = Ready)
        private static bool[] status = new bool[MAX_PROCESS_PARAM] { false, false, false, false };
        public bool[] Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
        //Process progress (0 - 100 %)
        private static double[] progress = new double[MAX_PROCESS_PARAM] { 0.0f, 0.0f, 0.0f, 0.0f };
        public double[] Progress
        {
            get
            {
                return progress;
            }

            set
            {
                progress = value;
            }
        }
        //Process Result (true = OK, false =  NG)
        private static bool[] result = new bool[MAX_PROCESS_PARAM] { false, false, false, false};
        public bool[] Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }

        //Stop Request: Abort the process
        private static bool stopRequest;
        public bool StopRequest
        {
            get
            {
                return stopRequest;
            }

            set
            {
                stopRequest = value;
            }
        }

        //Step Status & results
        private const int MAX_STEPS = 14;
        private const int MAX_MEAS = 6;
        public class StepParameters
        {
            //Steps results (1 - 14)
            private static string[] result = new string[MAX_STEPS] { "NG", "NG", "NG", "NG", "NG", "NG", "NG","NG", "NG", "NG","NG", "NG", "NG", "NG" };
            public string[] Result
            {
                get
                {
                    return result;
                }

                set
                {
                    result = value;
                }
            }
            //Step status (0 = NG, 1 = OK, 2 = Ommited)
            private static int[] status = new int[MAX_STEPS] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            public int[] Status
            {
                get
                {
                    return status;
                }

                set
                {
                    status = value;
                }
            }
            //Step state (Wait -> In Process -> Done)
            private static int[] state = new int[MAX_STEPS] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            public int[] State
            {
                get
                {
                    return state;
                }

                set
                {
                    state = value;
                }
            }
            //Steps Measurements (1 - 6)
            //[0] = Test 1: Force
            //[1] = Test 1: Displacement
            //[2] = Test 2: Force
            //[3] = Test 2: Displacement
            //[4] = Constant k
            //[5]
            private static string[] measurements = new string[MAX_MEAS] { "NG", "NG", "NG", "NG", "NG", "NG" };
            public string[] Measurements
            {
                get
                {
                    return measurements;
                }

                set
                {
                    measurements = value;
                }
            }
        }
       
        #endregion

        #endregion

        #region Callbacks

        #endregion

        #region Objects
        //OEE
        OEE OOEE = new OEE();
        //Scanner
        Scanner OScanner = new Scanner();
        //Reports
        Reports OReports = new Reports();
        //Step progress complete
        public StepParameters Process1_StepProgress = new StepParameters();
        //PLC
        PLC OPLC = new PLC();
        //Safety Controller
        SafetyPLC OSafetyPLC = new SafetyPLC();
        //Screwdriver Controller: MTFocus 6K
        MTFocus6K OMTFocus6K = new MTFocus6K();
        //Vision Sensor: Keyence, IV2-G500MA
        IV2G500MA OVisionSensor = new IV2G500MA();
        //Kistler: Force and Displacement Instrument
        Kistler_5847B0 OKistler = new Kistler_5847B0();
        //PCON-CB: Servomotor controller
        IAI_PCON OPCON = new IAI_PCON();
        //SQL Server
        SQLDatabase OSQLServer = new SQLDatabase();
        //Machcomm
        machcomm Omachcomm = new machcomm();
        #endregion

        #region Controls

        #endregion

        #region Information

        #endregion

        #region Functions

        #region Public
        //PLC Handshaking: Await response 5 attemps per miliseconds (Timeout)
        private bool LockerReady(int Timeout)
        {
            bool bSucc = false;
            int MAX_Timeout = 40;
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                if (OSafetyPLC.Inputs[10] || stopRequest)
                {
                    bSucc = true;
                    i = MAX_Timeout;
                }
                Thread.Sleep(Timeout);
            };

            return bSucc;
        }
        private bool PressReady(int Timeout)
        {
            bool bSucc = false;
            int MAX_Timeout = 40; 
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                if (OSafetyPLC.Inputs[8] || stopRequest)
                {
                    bSucc = true;
                    i = MAX_Timeout;
                }
                Thread.Sleep(Timeout);
            }

            return bSucc;
        }
        private bool SubProcessTimeout(int Timeout)
        {
            bool bSucc = false;
            int MAX_Timeout = 200; //Timeout 60s
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                //Continue optical button
                if (!OSafetyPLC.Inputs[6])
                {
                    for (int j = 0; j < MAX_Timeout; j++)
                    {
                        if (OSafetyPLC.Inputs[6])
                        {
                            bSucc = true;
                            j = MAX_Timeout;
                            i = MAX_Timeout;
                        }
                        //Stop request
                        if (stopRequest)
                        {
                            j = MAX_Timeout;
                            i = MAX_Timeout;
                        }
                        Thread.Sleep(Timeout);
                    }
                }
                Thread.Sleep(Timeout);
            }
            return bSucc;
        }
        private bool ScrewdrivingTimeout(int Timeout)
        {
            bool bSucc = false;
            bool bLock = false;
            int MAX_Timeout = 200; //Timeout 60s
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                //Continue optical button (unpress -> press)
                if (!OSafetyPLC.Inputs[6])
                {
                    for (int j = 0; j < MAX_Timeout; j++)
                    {
                        //To continue the process the screwdriving must be done, otherwise the process will keep on standby.
                        if (OSafetyPLC.Inputs[6] && OMTFocus6K.ScrewDone)
                        {
                            bSucc = true;

                            j = MAX_Timeout;
                            i = MAX_Timeout;
                            Thread.Sleep(500);
                            //Disable the Screwdriver
                            OMTFocus6K.SendCommand(OMTFocus6K.MID0224);  
                        }
                        //Wait for Screw Results
                        if (OMTFocus6K.ScrewDone && !bLock)
                        {
                            bLock = true;
                            Thread.Sleep(500);
                            //Disable the Screwdriver
                            OMTFocus6K.SendCommand(OMTFocus6K.MID0224);
                        }
                        //Stop request
                        if (stopRequest)
                        {
                            j = MAX_Timeout;
                            i = MAX_Timeout;
                        }
                        Thread.Sleep(Timeout);
                    }
                }
                Thread.Sleep(Timeout);
            }
            return bSucc;
        }

        #region PCON Servomotor
        //Standby Position
        public bool StandbyPosition()
        {
            bool bSucc = false;

            //DSTR OFF (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
            //Get Limits
            OPCON.TargetPosition_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.StandbyPos]).ToString("000.00");
            //Set: Position and Speed
            OPCON.Speed_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.StandbySpeed]).ToString("000");
            //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = true;

            Thread.Sleep(1000);

            int MAX_Timeout = 800;
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                if (!OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.MOVE] || stopRequest)
                {
                    bSucc = true;
                    //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
                    OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
                    i = MAX_Timeout;
                }
                Thread.Sleep(500);
            }

            return bSucc;
        }
        //Ready Position
        public bool ReadyPosition()
        {
            bool bSucc = false;

            //DSTR OFF (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
            //Get Limits
            OPCON.TargetPosition_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ReadyPos]).ToString("000.00");
            //Set: Position and Speed
            OPCON.Speed_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ReadySpeed]).ToString("000");
            //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = true;

            Thread.Sleep(1000);

            int MAX_Timeout = 800;
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                if (!OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.MOVE] || stopRequest)
                {
                    bSucc = true;
                    //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
                    OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
                    i = MAX_Timeout;
                }
                Thread.Sleep(500);
            }

            return bSucc;
        }
        //Test 1 Position
        public bool Test1Position()
        {
            bool bSucc = false;

            //DSTR OFF (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
            //Get Limits
            OPCON.TargetPosition_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test1Pos]).ToString("000.00");
            //Set: Position and Speed
            OPCON.Speed_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test1Speed]).ToString("000");
            //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = true;

            Thread.Sleep(1000);

            int MAX_Timeout = 800;
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                if (!OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.MOVE] || stopRequest)
                {
                    bSucc = true;
                    //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
                    OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
                    i = MAX_Timeout;
                }
                Thread.Sleep(500);
            }

            return bSucc;
        }
        //Test 2 Position
        public bool Test2Position()
        {
            bool bSucc = false;

            //DSTR OFF (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
            //Get Limits
            OPCON.TargetPosition_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test2Pos]).ToString("000.00");
            //Set: Position and Speed
            OPCON.Speed_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test2Speed]).ToString("000");
            //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = true;

            Thread.Sleep(1000);

            int MAX_Timeout = 800;
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                if (!OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.MOVE] || stopRequest)
                {
                    bSucc = true;
                    //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
                    OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
                    i = MAX_Timeout;
                }
                Thread.Sleep(500);
            }

            return bSucc;
        }
        //Screwdriving Position
        public bool ScrewdrivingPosition()
        {
            bool bSucc = false;

            //DSTR OFF (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
            //Get Limits
            OPCON.TargetPosition_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ScrewdrivingPos]).ToString("000.00");
            //Set: Position and Speed
            OPCON.Speed_value = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ScrewdrivingSpeed]).ToString("000");
            //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = true;

            Thread.Sleep(1000);

            int MAX_Timeout = 800;
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                if (!OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.MOVE] || stopRequest)
                {
                    bSucc = true;
                    //DSTR ON (Positioning command: A move command is issued when this signal turns ON)
                    OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
                    i = MAX_Timeout;
                }
                Thread.Sleep(500);
            }

            return bSucc;
        }
        #endregion

        #endregion

        #region Private
        //Step 1: Scann the Serial ID 
        private int Step1(bool EnableStep)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step has begun
                Process1_StepProgress.State[(int)StepNumber.Step1] = (int)StatusNo.InProcess;
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.Scanning)
                    HMI.OForm.ProdMessages("Step 1: Scan the serial ID\n", "System");
 
                //Read the 2D Code
                OScanner.SendCommand("< >", OScanner.Info);
                Thread.Sleep(1000);
                //Step 1: Result
                //Get Serial Number of the product
                if (OScanner.Value_Info == null || OScanner.Value_Info == "No Read" || OScanner.Value_Info.Any(x => !char.IsNumber(x)))
                {
                    Process1_StepProgress.Result[(int)StepNumber.Step1] = "No Read";
                    //Step 1: Status
                    Process1_StepProgress.Status[(int)StepNumber.Step1] = (int)StatusNo.NG;
                    //Step 1: Result Code
                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.NoRead2D);
                    HMI.OForm.ProdMessages("Step 1: Error to Scan the Serial ID, cause: Doesn't read, Empty or Characters into the Serial number string \n", "Error");
                }
                else
                {
                    Process1_StepProgress.Result[(int)StepNumber.Step1] = OScanner.Value_Info;
                    //Step 1: Status
                    Process1_StepProgress.Status[(int)StepNumber.Step1] = (int)StatusNo.OK;
                    OSQLServer.Serial = Convert.ToInt64(OScanner.Value_Info);
                }

                if (Engineering.OEngineering.OMsgTrace.Scanning)
                {
                    HMI.OForm.ProdMessages("Step 1: Serial ID: " + Process1_StepProgress.Result[(int)StepNumber.Step1] + "\n", "System");
                }
                //The Step has finished
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.Scanning)
                    HMI.OForm.ProdMessages("Step 1: End scan the serial ID\n", "System");
                Process1_StepProgress.State[(int)StepNumber.Step1] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.Step1] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.Step1] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step1];
        }
        //Step 2: Prior Op Check
        private int Step2(bool EnableStep)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step has begun
                Process1_StepProgress.State[(int)StepNumber.Step2] = (int)StatusNo.InProcess;
                //Check if the Step 1: Code Readable
                if (Process1_StepProgress.Status[(int)StepNumber.Step1] == (int)StatusNo.OK)
                {
                    //Step 2: Execute (Prior Op Check)
                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.PriorOpCheck) HMI.OForm.ProdMessages("Step 2: Prior Op Check\n", "PLC");

                    try
                    {
                        //Step2: Query
                        Int64 Serial = Convert.ToInt64(Process1_StepProgress.Result[(int)StepNumber.Step1]);
                        OSQLServer.PriorOpCheck(Serial, OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID]);
                        //Step 2: Result
                        if (OSQLServer.PriorOpChk_Return == 1)
                        {
                            Process1_StepProgress.Result[(int)StepNumber.Step2] = "Good";
                        }
                        else
                            Process1_StepProgress.Result[(int)StepNumber.Step2] = "Fail";

                    }
                    catch (Exception)
                    {
                        HMI.OForm.SystemMessages("The connection to the SQL Server was failed\n", "Error");
                        //Step 2: Status
                        Process1_StepProgress.Result[(int)StepNumber.Step2] = "Fail";
                    }
                    //Step 2: Status
                    if (Process1_StepProgress.Result[(int)StepNumber.Step2] != "Fail")
                    {
                        Process1_StepProgress.Status[(int)StepNumber.Step2] = (int)StatusNo.OK;
                    }
                    else
                    {
                        Process1_StepProgress.Status[(int)StepNumber.Step2] = (int)StatusNo.NG;
                        //Step 2: Result Code
                        ResultCode = Omachcomm.ResultCode((int)ResultCodes.PriorOp);
                        HMI.OForm.ProdMessages("Step 2: Error Prior Op Check\n", "Error");
                    }
                }
                //The Step has finished
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.PriorOpCheck) HMI.OForm.ProdMessages("Step 2: End Prior Op Check\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.Step2] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.Step2] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.Step2] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step2];
        }
        //Step Check Components
        private int Step_CheckComponents(bool EnableStep)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step has begun
                Process1_StepProgress.State[(int)StepNumber.Step_CheckComponents] = (int)StatusNo.InProcess;
                //Check if the Step 1: Code Readable
                if (Process1_StepProgress.Status[(int)StepNumber.Step1] == (int)StatusNo.OK)
                {
                    //Step Check Components: Execute (Check Components)
                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.ChkComp) HMI.OForm.ProdMessages("Step Check Components starting...\n", "PLC");

                    try
                    {
                        //Step Check Components: Query
                        Int64 Serial = Convert.ToInt64(Process1_StepProgress.Result[(int)StepNumber.Step1]);
                        OSQLServer.CheckComponents(OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID]);
                        //Step Check Components: Result
                        if (OSQLServer.CheckComponents_Return == 1)
                        {
                            Process1_StepProgress.Result[(int)StepNumber.Step_CheckComponents] = "Good";
                        }
                        else
                            Process1_StepProgress.Result[(int)StepNumber.Step_CheckComponents] = "Fail";

                    }
                    catch (Exception)
                    {
                        HMI.OForm.SystemMessages("The connection to the SQL Server was failed\n", "Error");
                        //Step Check Components: Status
                        Process1_StepProgress.Result[(int)StepNumber.Step_CheckComponents] = "Fail";
                    }
                    //Step Check Components: Status
                    if (Process1_StepProgress.Result[(int)StepNumber.Step_CheckComponents] != "Fail")
                    {
                        Process1_StepProgress.Status[(int)StepNumber.Step_CheckComponents] = (int)StatusNo.OK;
                    }
                    else
                    {
                        Process1_StepProgress.Status[(int)StepNumber.Step_CheckComponents] = (int)StatusNo.NG;
                        //Step Check Components: Result Code
                        ResultCode = Omachcomm.ResultCode((int)ResultCodes.CheckComponents);
                        HMI.OForm.ProdMessages("Step Check Components: Error\n", "Error");
                    }
                }
                //The Step has finished
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.ChkComp) HMI.OForm.ProdMessages("Step Check Components finished\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.Step_CheckComponents] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.Step_CheckComponents] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.Step_CheckComponents] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step_CheckComponents];
        }
        //Step Assign Components to Serial ID
        private int Step_AssignComponents(bool EnableStep)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step has begun
                Process1_StepProgress.State[(int)StepNumber.Step_AssignComponents] = (int)StatusNo.InProcess;
                //Check if the Step 1: Code Readable
                if (Process1_StepProgress.Status[(int)StepNumber.Step1] == (int)StatusNo.OK)
                {
                    //Step Assign Components: Execute (Assign Components)
                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.AssignComp) HMI.OForm.ProdMessages("Step Assign Components starting...\n", "PLC");

                    try
                    {
                        //Step Assign Components: Query
                        Int64 Serial = Convert.ToInt64(Process1_StepProgress.Result[(int)StepNumber.Step1]);
                        OSQLServer.AssignComponentsToSerial(Serial, OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID], OSQLServer.ProdLot_Param[(int)Prod_Lot.LotID]);
                        //Step Assign Components: Result
                        if (OSQLServer.AssignComponentsToSerial_Return == 1)
                        {
                            Process1_StepProgress.Result[(int)StepNumber.Step_AssignComponents] = "Good";
                        }
                        else
                            Process1_StepProgress.Result[(int)StepNumber.Step_AssignComponents] = "Fail";

                    }
                    catch (Exception)
                    {
                        HMI.OForm.SystemMessages("The connection to the SQL Server was failed\n", "Error");
                        //Step Assign Components: Status
                        Process1_StepProgress.Result[(int)StepNumber.Step_AssignComponents] = "Fail";
                    }
                    //Step Assign Components: Status
                    if (Process1_StepProgress.Result[(int)StepNumber.Step_AssignComponents] != "Fail")
                    {
                        Process1_StepProgress.Status[(int)StepNumber.Step_AssignComponents] = (int)StatusNo.OK;
                    }
                    else
                    {
                        Process1_StepProgress.Status[(int)StepNumber.Step_AssignComponents] = (int)StatusNo.NG;
                        //Step Assign Components: Result Code
                        ResultCode = Omachcomm.ResultCode((int)ResultCodes.AssignComponents);
                        HMI.OForm.ProdMessages("Step Assign Components: Error\n", "Error");
                    }
                }
                //The Step has finished
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.AssignComp) HMI.OForm.ProdMessages("Step Assign Components finished\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.Step_AssignComponents] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.Step_AssignComponents] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.Step_AssignComponents] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step_AssignComponents];
        }
        //Step 3A: Spring Inspection
        private int Step3A(bool EnableStep, bool Permission)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step has begun
                Process1_StepProgress.State[(int)StepNumber.Step3A] = (int)StatusNo.InProcess;
                if (Permission)
                {
                    //Check if the Step 2: Prior Operation Check was good
                    if (Process1_StepProgress.Status[(int)StepNumber.Step2] == (int)StatusNo.OK || Process1_StepProgress.Status[(int)StepNumber.Step2] == (int)StatusNo.Omitted)
                    {
                        //Step 3: Measurement 1
                        //Trace enable
                        if (Engineering.OEngineering.OMsgTrace.Instrument2) HMI.OForm.ProdMessages("Step 3A: Start Vision Sensor\n", "PLC");

                        //Measure
                        //Change to Program 00X (Spring Inspection)   
                        if (OSQLServer.Limits[(int)Prod_Limits.SpringVisionProg] != OVisionSensor._SensorProgSettings.ProgramNumber.ToString())
                        {
                            OVisionSensor.SwitchProg(Convert.ToInt16(OSQLServer.Limits[(int)Prod_Limits.SpringVisionProg]));
                        }
                        Thread.Sleep(800);
                        //Trigger
                        OVisionSensor.Trigger();
                        Thread.Sleep(800);
                        //Get the results
                        Process1_StepProgress.Result[(int)StepNumber.Step3A] = OVisionSensor.TextResult;
                        
                        //Step 3: Status
                        if (Process1_StepProgress.Result[(int)StepNumber.Step3A] != "NG")
                        {
                            Process1_StepProgress.Status[(int)StepNumber.Step3A] = (int)StatusNo.OK;
                        }
                        else
                        {
                            Process1_StepProgress.Result[(int)StepNumber.Step3A] = "Inspection Failure";
                            Process1_StepProgress.Status[(int)StepNumber.Step3A] = (int)StatusNo.NG;
                            HMI.OForm.ProdMessages("Step 3A: Error Vision Test\n", "Error");
                        }
                    }

                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.Instrument1)
                    {
                        HMI.OForm.ProdMessages("Step 3A: Vision Sensor: " + Process1_StepProgress.Result[(int)StepNumber.Step3A] + "\n", "PLC");
                    }
                }
                else
                {
                    Process1_StepProgress.Result[(int)StepNumber.Step3A] = "Locker Failure";
                    //if the step is disabled, by Default is true
                    Process1_StepProgress.Status[(int)StepNumber.Step3A] = (int)StatusNo.NG;
                }
                //The Step has finished
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.Instrument2) HMI.OForm.ProdMessages("Step 3A: End Vision Sensor\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.Step3A] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.Step3A] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.Step3A] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step3A];
        }
        //Step 3B: Plunger Installation
        private int Step3B(bool EnableStep, bool Permission)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step has begun
                Process1_StepProgress.State[(int)StepNumber.Step3B] = (int)StatusNo.InProcess;
                if (Permission)
                {
                    //Check if the Step 2: Prior Operation Check was good
                    if (Process1_StepProgress.Status[(int)StepNumber.Step2] == (int)StatusNo.OK || Process1_StepProgress.Status[(int)StepNumber.Step2] == (int)StatusNo.Omitted)
                    {
                        //Step 3B: Measurement 2
                        //Trace enable
                        if (Engineering.OEngineering.OMsgTrace.Instrument2) HMI.OForm.ProdMessages("Step 3B: Start Vision Sensor\n", "PLC");

                        //Measure
                        //Change to Program 00X (Plunger Inspection)
                        if (OSQLServer.Limits[(int)Prod_Limits.ArmatureVisionProg] != OVisionSensor._SensorProgSettings.ProgramNumber.ToString())
                        {
                            OVisionSensor.SwitchProg(Convert.ToInt16(OSQLServer.Limits[(int)Prod_Limits.ArmatureVisionProg]));
                        }
                        Thread.Sleep(800);
                        //Trigger
                        OVisionSensor.Trigger();
                        Thread.Sleep(800);
                        //Get the results
                        Process1_StepProgress.Result[(int)StepNumber.Step3B] = OVisionSensor.TextResult;
                        //Step 3B: Status
                        if (Process1_StepProgress.Result[(int)StepNumber.Step3B] != "NG")
                        {
                            Process1_StepProgress.Status[(int)StepNumber.Step3B] = (int)StatusNo.OK;
                        }
                        else
                        {
                            Process1_StepProgress.Result[(int)StepNumber.Step3B] = "Inspection Failure";
                            Process1_StepProgress.Status[(int)StepNumber.Step3B] = (int)StatusNo.NG;
                            HMI.OForm.ProdMessages("Step 3B: Error Vision Test\n", "Error");
                        }
                    }

                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.Instrument1)
                    {
                        HMI.OForm.ProdMessages("Step 3B: Vision Sensor: " + Process1_StepProgress.Result[(int)StepNumber.Step3B] + "\n", "PLC");
                    }
                }
                else
                {
                    Process1_StepProgress.Result[(int)StepNumber.Step3B] = "Locker Failure";
                    //if the step is disabled, by Default is true
                    Process1_StepProgress.Status[(int)StepNumber.Step3B] = (int)StatusNo.NG;
                }
                //The Step has finished
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.Instrument2) HMI.OForm.ProdMessages("Step 3B: End Vision Sensor\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.Step3B] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.Step3B] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.Step3B] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step3B];
        }
        //Step: Spring Force Test 1
        private int Step_SpringForceTest1(bool EnableStep, bool Permission)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step Spring Force Test 1 has begun
                Process1_StepProgress.State[(int)StepNumber.StepFDTest1] = (int)StatusNo.InProcess;

                //Servo press: Test 1 Position
                Permission &= Test1Position();

                if (Permission)
                {
                    #region Limits
                    //Displacement
                    double LimmitChXmin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test1ChXmin]);
                    double LimmitChXmax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test1ChXmax]);
                    //Force
                    double LimmitChYmin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test1ChYmin]);
                    double LimmitChYmax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test1ChYmax]);
                    #endregion

                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.Instrument1)
                        HMI.OForm.ProdMessages("Step Spring Force Test 1: Start Force & Displacement Measurements\n", "PLC");

                    //Step Spring Force Test 1: Status
                    Thread.Sleep(2000);
                    //Read Measurements
                    Process1_StepProgress.Measurements[(int)Measurements.Test1Force] = OKistler.ChannelY_Value.ToString();
                    Process1_StepProgress.Measurements[(int)Measurements.Test1Displacement] = OKistler.ChannelX_Value.ToString();

                    //Compare the Channel X (Displacement, mm) value into the range. 
                    if ((LimmitChXmin <= OKistler.ChannelX_Value) && (OKistler.ChannelX_Value <= LimmitChXmax))
                    {
                        //Step 4: Status
                        Process1_StepProgress.Status[(int)StepNumber.StepFDTest1] = (int)StatusNo.OK;


                        //Compare the Channel Y (Force, N) value into the range.
                        if ((LimmitChYmin <= OKistler.ChannelY_Value) && (OKistler.ChannelY_Value <= LimmitChYmax))
                        {
                            //Step Spring Force Test 1: Status
                            Process1_StepProgress.Status[(int)StepNumber.StepFDTest1] = (int)StatusNo.OK;
                        }
                        else
                        {
                            //Step Spring Force Test 1: Status
                            Process1_StepProgress.Status[(int)StepNumber.StepFDTest1] = (int)StatusNo.NG;
                            //Verify the result code, was Low or High
                            if (LimmitChYmin > OKistler.ChannelY_Value)
                            {
                                ResultCode = Omachcomm.ResultCode((int)ResultCodes.Test1LowChY);
                            }
                            if (OKistler.ChannelY_Value > LimmitChYmax)
                            {
                                ResultCode = Omachcomm.ResultCode((int)ResultCodes.Test1HighChY);
                            }
                        }
                    }
                    else
                    {
                        //Step Spring Force Test 1: Status
                        Process1_StepProgress.Status[(int)StepNumber.StepFDTest1] = (int)StatusNo.NG;
                        //Verify the result code, was Low or High
                        if (LimmitChXmin > OKistler.ChannelX_Value)
                        {
                            ResultCode = Omachcomm.ResultCode((int)ResultCodes.Test1LowChX);
                        }
                        if (OKistler.ChannelX_Value > LimmitChXmax)
                        {
                            ResultCode = Omachcomm.ResultCode((int)ResultCodes.Test1HighChX);
                        }
                    }
                }
                else
                {
                    //Step Spring Force Test 1: Result
                    Process1_StepProgress.Result[(int)StepNumber.StepFDTest1] = "Vision Inspection NG or Press test 1 position failed";
                    //Step Spring Force Test 1: Status
                    Process1_StepProgress.Status[(int)StepNumber.StepFDTest1] = (int)StatusNo.NG;
                    //Step Spring Force Test 1: Result Code
                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.MachineFailure);
                }
                //The Step has begun
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.Instrument1)
                    HMI.OForm.ProdMessages("Step Spring Force Test 1 Measurements has finished\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.StepFDTest1] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.StepFDTest1] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.StepFDTest1] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.StepFDTest1];
        }
        //Step: Spring Force Test 2
        private int Step_SpringForceTest2(bool EnableStep, bool Permission)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step Spring Force Test 2 has begun
                Process1_StepProgress.State[(int)StepNumber.StepFDTest2] = (int)StatusNo.InProcess;

                //Servo press: Test 1 Position
                Permission &= Test2Position();

                if (Permission)
                {
                    #region Limits
                    //Displacement
                    double LimmitChXmin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test2ChXmin]);
                    double LimmitChXmax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test2ChXmax]);
                    //Force
                    double LimmitChYmin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test2ChYmin]);
                    double LimmitChYmax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test2ChYmax]);
                    #endregion

                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.Instrument1)
                        HMI.OForm.ProdMessages("Step Spring Force Test 2: Start Force & Displacement Measurements\n", "PLC");

                    //Step Spring Force Test 2: Status
                    Thread.Sleep(2000);
                    //Read Measurements
                    Process1_StepProgress.Measurements[(int)Measurements.Test2Force] = OKistler.ChannelY_Value.ToString();
                    Process1_StepProgress.Measurements[(int)Measurements.Test2Displacement] = OKistler.ChannelX_Value.ToString();

                    //Compare the Channel X (Displacement, mm) value into the range. 
                    if ((LimmitChXmin <= OKistler.ChannelX_Value) && (OKistler.ChannelX_Value <= LimmitChXmax))
                    {
                        //Step Spring Force Test 2: Status
                        Process1_StepProgress.Status[(int)StepNumber.StepFDTest2] = (int)StatusNo.OK;


                        //Compare the Channel Y (Force, N) value into the range.
                        if ((LimmitChYmin <= OKistler.ChannelY_Value) && (OKistler.ChannelY_Value <= LimmitChYmax))
                        {
                            //Step 4: Status
                            Process1_StepProgress.Status[(int)StepNumber.StepFDTest2] = (int)StatusNo.OK;
                        }
                        else
                        {
                            //Step Spring Force Test 2: Status
                            Process1_StepProgress.Status[(int)StepNumber.StepFDTest2] = (int)StatusNo.NG;
                            //Verify the result code, was Low or High
                            if (LimmitChYmin > OKistler.ChannelY_Value)
                            {
                                ResultCode = Omachcomm.ResultCode((int)ResultCodes.Test2LowChY);
                            }
                            if (OKistler.ChannelY_Value > LimmitChYmax)
                            {
                                ResultCode = Omachcomm.ResultCode((int)ResultCodes.Test2HighChY);
                            }
                        }
                    }
                    else
                    {
                        //Step Spring Force Test 2: Status
                        Process1_StepProgress.Status[(int)StepNumber.StepFDTest2] = (int)StatusNo.NG;
                        //Verify the result code, was Low or High
                        if (LimmitChXmin > OKistler.ChannelX_Value)
                        {
                            ResultCode = Omachcomm.ResultCode((int)ResultCodes.Test2LowChX);
                        }
                        if (OKistler.ChannelX_Value > LimmitChXmax)
                        {
                            ResultCode = Omachcomm.ResultCode((int)ResultCodes.Test2HighChX);
                        }
                    }
                }
                else
                {
                    //Step Spring Force Test 2: Result
                    Process1_StepProgress.Result[(int)StepNumber.StepFDTest2] = "Spring Force Test 2 or test 2 position failed";
                    //Step Spring Force Test 2: Status
                    Process1_StepProgress.Status[(int)StepNumber.StepFDTest2] = (int)StatusNo.NG;
                    //Step Spring Force Test 2: Result Code
                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.MachineFailure);
                }
                //The Step has begun
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.Instrument1)
                    HMI.OForm.ProdMessages("Step Spring Force Test 2 Measurements has finished\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.StepFDTest2] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.StepFDTest2] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.StepFDTest2] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.StepFDTest2];
        }
        //Step: FD Constant K: Hook's law
        private int Step_ConstKCalc(bool EnableStep, bool Permission)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step Constant K calculation has begun
                Process1_StepProgress.State[(int)StepNumber.StepFDConstK] = (int)StatusNo.InProcess;
                if (Permission)
                {
                    #region Limits
                    //Constant K
                    double LimmitChXmin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ConstantKmin]);
                    double LimmitChXmax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ConstantKmax]);
                    #endregion

                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.Instrument1)
                        HMI.OForm.ProdMessages("Step Constant K calculating....\n", "PLC");

                    //Step Constant K calculation: Status
                    Thread.Sleep(200);
                    //Get Test 1 and Test 2 measurements
                    double Force = 0.0f;
                    double Displacement1 = 0.0f;
                    double Displacement2 = 0.0f;
                    //Calculate the Constant K = Force Test 2 / (Displacement Test 2 - Displacement Test 1)
                    Force = Convert.ToDouble(Process1_StepProgress.Measurements[(int)Measurements.Test2Force]);
                    Displacement2 = Convert.ToDouble(Process1_StepProgress.Measurements[(int)Measurements.Test2Displacement]);
                    Displacement2 = Convert.ToDouble(Process1_StepProgress.Measurements[(int)Measurements.Test1Displacement]);
                    double ConstantKValue = Force / (Displacement2-Displacement1);
                    Process1_StepProgress.Measurements[(int)Measurements.ConstantK] = ConstantKValue.ToString("000.000");

                    //Compare the Constant K calculation
                    if ((LimmitChXmin <= ConstantKValue) && (ConstantKValue <= LimmitChXmax))
                    {
                        //Constant K calculation: Status
                        Process1_StepProgress.Status[(int)StepNumber.StepFDConstK] = (int)StatusNo.OK;
                    }
                    else
                    {
                        //Step Constant K calculation: Status
                        Process1_StepProgress.Status[(int)StepNumber.StepFDConstK] = (int)StatusNo.NG;
                        //Verify the result code, was Low or High
                        if (LimmitChXmin > ConstantKValue)
                        {
                            ResultCode = Omachcomm.ResultCode((int)ResultCodes.LowConstantK);
                        }
                        if (ConstantKValue > LimmitChXmax)
                        {
                            ResultCode = Omachcomm.ResultCode((int)ResultCodes.HighConstantK);
                        }
                    }
                }
                else
                {
                    //Step Constant K calculation: Result
                    Process1_StepProgress.Result[(int)StepNumber.StepFDConstK] = "Constant K fail";
                    //Step Constant K calculation: Status
                    Process1_StepProgress.Status[(int)StepNumber.StepFDConstK] = (int)StatusNo.NG;
                    //Step Constant K calculation: Result Code
                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.MachineFailure);
                }
                //The Step has begun
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.Instrument1)
                    HMI.OForm.ProdMessages("Step Constant K calculation has finished\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.StepFDConstK] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.StepFDConstK] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.StepFDConstK] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.StepFDConstK];
        }
        //Step 4: Torque & Angle Measurements: MTFocus 6000
        private int Step4(bool EnableStep, bool Permission)
        {
            //Check if the step is enable from Engineering Winform
            if (EnableStep)
            {
                //The Step has begun
                Process1_StepProgress.State[(int)StepNumber.Step4] = (int)StatusNo.InProcess;

                if (Permission)
                {
                    #region Limits
                    //Peak/Final torque
                    double TorqueMin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.TorqueMin]);
                    double TorqueMax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.TorqueMax]);
                    //Final Angle
                    double AngleMin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.AngleMin]);
                    double AngleMax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.AngleMax]);
                    //Clamp & Seating point 
                    double ClampMin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ClampMin]);
                    double ClampMax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ClampMax]);
                    double ClampAngleMin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ClampAngleMin]);
                    double ClampAngleMax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ClampAngleMax]);
                    double SeatingPointMin = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.SeatingPointMin]);
                    double SeatingPointMax = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.SeatingPointMax]);
                    #endregion

                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.Instrument1)
                        HMI.OForm.ProdMessages("Step 4: Start Torque & Angle Measurements\n", "PLC");

                    //Measurements
                    double TorqueValue = 0.0f;
                    double AngleValue = 0.0f;

                    double ClampValue = 0.0f;
                    double ClampAngleValue = 0.0f;
                    double SeatingPointValue = 0.0f;

                    //Step 4: Status
                    if (OMTFocus6K.DataField_MID1202[31, 5] == null)
                    {
                        Process1_StepProgress.Result[(int)StepNumber.Step4] = "Error measurement";
                        //Step 4: Status
                        Process1_StepProgress.Status[(int)StepNumber.Step4] = (int)StatusNo.NG;
                    }
                    else
                    {
                        #region Assessments
                        //Torque value
                        Process1_StepProgress.Result[(int)StepNumber.Step4] = OMTFocus6K.DataField_MID1202[31, 5];
                        //Convert the value to Double, if the value is valid
                        TorqueValue = Convert.ToDouble(Process1_StepProgress.Result[(int)StepNumber.Step4]);
                       
                        //Compare the value into the range, the Limits are from the DB or CSV file
                        if (TorqueMin <= TorqueValue && TorqueValue <= TorqueMax)
                        {
                            //Angle value
                            Process1_StepProgress.Result[(int)StepNumber.Step4] = OMTFocus6K.DataField_MID1202[32, 5];
                            //Convert the value to Double, if the value is valid
                            AngleValue = Convert.ToDouble(Process1_StepProgress.Result[(int)StepNumber.Step4]);

                            if (AngleMin <= AngleValue && AngleValue <= AngleMax)
                            {
                                //Clamp torque value
                                ClampValue = Convert.ToDouble(OMTFocus6K.Clamp);

                                if (ClampMin <= ClampValue && ClampValue <= ClampMax)
                                {
                                    //Clamp angle value
                                    ClampAngleValue = Convert.ToDouble(OMTFocus6K.ClampAngle);

                                    if (ClampAngleMin <= ClampAngleValue && ClampAngleValue <= ClampAngleMax)
                                    {
                                        //Seating Point torque value 
                                        SeatingPointValue = Convert.ToDouble(OMTFocus6K.SeatingPoint);

                                        if (SeatingPointMin <= SeatingPointValue && SeatingPointValue <= SeatingPointMax)
                                        {
                                            //Step 4: Status
                                            Process1_StepProgress.Status[(int)StepNumber.Step4] = (int)StatusNo.OK;
                                        }
                                        else
                                        {
                                            //Step 4: Status
                                            Process1_StepProgress.Status[(int)StepNumber.Step4] = (int)StatusNo.NG;
                                            //Verify the result code, was Low or High
                                            if (SeatingPointMin > SeatingPointValue)
                                            {
                                                ResultCode = Omachcomm.ResultCode((int)ResultCodes.LowLimitSP);
                                            }
                                            if (SeatingPointValue > SeatingPointMax)
                                            {
                                                ResultCode = Omachcomm.ResultCode((int)ResultCodes.HighLimitSP);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Step 4: Status
                                        Process1_StepProgress.Status[(int)StepNumber.Step4] = (int)StatusNo.NG;
                                        //Verify the result code, was Low or High
                                        if (ClampAngleMin > ClampAngleValue)
                                        {
                                            ResultCode = Omachcomm.ResultCode((int)ResultCodes.LowClampAngle);
                                        }
                                        if (ClampAngleValue > ClampAngleMax)
                                        {
                                            ResultCode = Omachcomm.ResultCode((int)ResultCodes.HighClampAngle);
                                        }
                                    }
                                }
                                else
                                {
                                    //Step 4: Status
                                    Process1_StepProgress.Status[(int)StepNumber.Step4] = (int)StatusNo.NG;
                                    //Verify the result code, was Low or High
                                    if (ClampMin > ClampValue)
                                    {
                                        ResultCode = Omachcomm.ResultCode((int)ResultCodes.LowLimitClamp);
                                    }
                                    if (ClampValue > ClampMax)
                                    {
                                        ResultCode = Omachcomm.ResultCode((int)ResultCodes.HighLimitClamp);
                                    }
                                }
                            }
                            else
                            {
                                //Step 4: Status
                                Process1_StepProgress.Status[(int)StepNumber.Step4] = (int)StatusNo.NG;
                                //Verify the result code, was Low or High
                                if (AngleMin > AngleValue)
                                {
                                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.LowLimitAngle);
                                }
                                if (AngleValue > AngleMax)
                                {
                                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.HighLimiAngle);
                                }
                            }
                        }
                        else
                        {
                            //Step 4: Status
                            Process1_StepProgress.Status[(int)StepNumber.Step4] = (int)StatusNo.NG;
                            //Verify the result code, was Low or High
                            if (TorqueMin > TorqueValue)
                            {
                                ResultCode = Omachcomm.ResultCode((int)ResultCodes.LowLimitTorque);
                            }
                            if (TorqueValue > TorqueMax)
                            {
                                ResultCode = Omachcomm.ResultCode((int)ResultCodes.HighLimitTorque);
                            }
                        }
                        #endregion

                        //Trace enable
                        if (Engineering.OEngineering.OMsgTrace.Instrument1)
                            HMI.OForm.ProdMessages("Step 4: Torque-Angle: " + TorqueValue +"," + AngleValue +"," + ClampValue +"," + SeatingPointValue + "\n", "PLC");
                    }
                }
                else
                {
                    //Step 4: Result
                    Process1_StepProgress.Result[(int)StepNumber.Step4] = "Vision Inspection NG or Press Failure";
                    //Step 4: Status
                    Process1_StepProgress.Status[(int)StepNumber.Step4] = (int)StatusNo.NG;
                    //Step 4: Result Code
                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.MachineFailure);
                }
                //The Step has begun
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.Instrument1)
                    HMI.OForm.ProdMessages("Step 4: End Torque & Angle Measurements\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.Step4] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.Step4] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.Step4] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step4];
        }
        //Step 5: Store Measurements: DB = True, CSV = False, Save without Part Status
        private int Step5(bool SelectStoreSource)
        {
            //The Step has begun
            Process1_StepProgress.State[(int)StepNumber.Step5] = (int)StatusNo.InProcess;
            //Trace enable
            if (Engineering.OEngineering.OMsgTrace.StoreMeas) HMI.OForm.ProdMessages("Step 5: Store Measurements\n", "PLC");
 
            //Select the path (DB = true or CSV file = false) to store the measurements from Engineering Winform
            if (SelectStoreSource)
            {
                //Store the measurements on DB

            }
            else
            {
                //Store the measurements on CSV
                if (OReports.WriteStoreMeasurements())
                {
                    //Step 5: Result
                    Process1_StepProgress.Result[(int)StepNumber.Step5] = "Done";
                    //Step 5: Status
                    Process1_StepProgress.Status[(int)StepNumber.Step5] = (int)StatusNo.OK;
                }
                else
                {
                    //Step 5: Result
                    Process1_StepProgress.Result[(int)StepNumber.Step5] = "Error";
                    //Step 5: Status
                    Process1_StepProgress.Status[(int)StepNumber.Step5] = (int)StatusNo.NG;
                }
            }
            //The Step has finished
            //Trace enable
            if (Engineering.OEngineering.OMsgTrace.StoreMeas) HMI.OForm.ProdMessages("Step 5: End Store Measurements\n", "PLC");
            Process1_StepProgress.State[(int)StepNumber.Step5] = (int)StatusNo.Done;
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step5];
        }
        //Step 6: PC to PLC Handshaking
        private int Step6(bool PLC_Alarms)
        {
            //The Step has begun
            Process1_StepProgress.State[(int)StepNumber.Step6] = (int)StatusNo.InProcess;
            //Trace enable
            if (Engineering.OEngineering.OMsgTrace.PLC) HMI.OForm.ProdMessages("Step 6: Process finished to PLC\n", "PLC");

            if (!PLC_Alarms)
            {
                //Step 6: Result
                Process1_StepProgress.Result[(int)StepNumber.Step6] = "Good";
                //Step 6: Status
                Process1_StepProgress.Status[(int)StepNumber.Step6] = (int)StatusNo.OK;
            }
            else
            {
                //Step 6: Result
                Process1_StepProgress.Result[(int)StepNumber.Step6] = "PLC Alarm";
                //Step 6: Status
                Process1_StepProgress.Status[(int)StepNumber.Step6] = (int)StatusNo.NG;
            }
            //The Step has finished
            //Trace enable
            if (Engineering.OEngineering.OMsgTrace.PLC) HMI.OForm.ProdMessages("Step 6: End Process finished to PLC\n", "PLC");
            Process1_StepProgress.State[(int)StepNumber.Step6] = (int)StatusNo.Done;
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step6];
        }
        //Step 7: Update Device Status
        private int Step7(bool EnableStep, string PartStatus)
        {
            //Check if the step is enable from Engineering Winform and the Prior Operation Check was "Good"
            //With this conditional we avoid to assign new status to the Part, if by any reason the part was set into the station again. 
            if (EnableStep && Process1_StepProgress.Result[(int)StepNumber.Step2] == "Good")
            {
                //The Step has begun
                Process1_StepProgress.State[(int)StepNumber.Step7] = (int)StatusNo.InProcess;
                //Check if the Step 1: Code Readable
                if (Process1_StepProgress.Status[(int)StepNumber.Step1] == (int)StatusNo.OK)
                {
                    //Step 7: Update Device Status
                    //Trace enable
                    if (Engineering.OEngineering.OMsgTrace.UpdateDeviceStatus) HMI.OForm.ProdMessages("Step 7: Update Device Status\n", "PLC");

                    try
                    {
                        //Step2: Query
                        Int64 Serial = Convert.ToInt64(Process1_StepProgress.Result[(int)StepNumber.Step1]);

                        //Step 2: Result
                        if (OSQLServer.UpdateDeviceStatus(Serial, OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID], PartStatus))
                        {
                            Process1_StepProgress.Result[(int)StepNumber.Step7] = PartStatus;
                        }
                        else
                            Process1_StepProgress.Result[(int)StepNumber.Step7] = "Error";

                    }
                    catch (Exception)
                    {
                        HMI.OForm.SystemMessages("The connection to the SQL Server was failed\n", "Error");
                        //Step 2: Status
                        Process1_StepProgress.Result[(int)StepNumber.Step7] = "Error";
                    }

                    //Step 7: Status
                    if (Process1_StepProgress.Result[(int)StepNumber.Step7] != "Error")
                    {
                        Process1_StepProgress.Status[(int)StepNumber.Step7] = (int)StatusNo.OK;
                    }
                    else
                    {
                        HMI.OForm.ProdMessages("Step 7: Error Update Device Status\n", "Error");
                        Process1_StepProgress.Status[(int)StepNumber.Step7] = (int)StatusNo.NG;
                    }
                }
                //The Step has finished
                //Trace enable
                if (Engineering.OEngineering.OMsgTrace.UpdateDeviceStatus) HMI.OForm.ProdMessages("Step 7: End Update Device Status\n", "PLC");
                Process1_StepProgress.State[(int)StepNumber.Step7] = (int)StatusNo.Done;
            }
            else
            {
                Process1_StepProgress.Result[(int)StepNumber.Step7] = "Disabled";
                //if the step is disabled, by Default is true
                Process1_StepProgress.Status[(int)StepNumber.Step7] = (int)StatusNo.Omitted;
            }
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step7];
        }
        //Step 8: Store Results: DB = True, CSV = False, Save with Part Status
        private int Step8(bool SelectStoreSource, string PartStatus)
        {
            //Step 8: Store Results process
            //The Step has begun
            Process1_StepProgress.State[(int)StepNumber.Step8] = (int)StatusNo.InProcess;
            //Trace enable
            if (Engineering.OEngineering.OMsgTrace.StoreResults) HMI.OForm.ProdMessages("Step 8: Store Results process\n", "PLC");

            //Select the path (DB = true or CSV file = false) to store the results from Engineering Winform
            if (SelectStoreSource)
            {
                #region Database Query
                //Step 8: Query
                Int64 Serial = Convert.ToInt64(Process1_StepProgress.Result[(int)StepNumber.Step1]);
                string Torque = OMTFocus6K.DataField_MID1202[31, 5];
                string Angle = OMTFocus6K.DataField_MID1202[32, 5];
                string Clamp = OMTFocus6K.Clamp;
                string ClampAngle = OMTFocus6K.ClampAngle;
                string SeatingPoint = OMTFocus6K.SeatingPoint;

                //Store the Results on DB with Part Status
                try
                {
                    if (OSQLServer.ResultsDB(Serial, OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID], OSQLServer.ProdLot_Param[(int)Prod_Lot.LotID], Torque, Angle, Clamp, ClampAngle, SeatingPoint,  PartStatus))
                    {
                        //Step 8: Result
                        Process1_StepProgress.Result[(int)StepNumber.Step8] = "Done";

                        //Step 8: Status
                        Process1_StepProgress.Status[(int)StepNumber.Step8] = (int)StatusNo.OK;
                    }
                    else
                    {
                        //Step 8: Result
                        Process1_StepProgress.Result[(int)StepNumber.Step8] = "Error";
                        //Step 8: Status
                        Process1_StepProgress.Status[(int)StepNumber.Step8] = (int)StatusNo.NG;
                    }
                }
                catch (Exception)
                {
                    HMI.OForm.SystemMessages("The connection to the SQL Server was failed\n", "Error");
                }
                #endregion
            }
            else
            {
                //Store the results on CSV
                if (OReports.WriteStoreResults())
                {
                    //Step 8: Result
                    Process1_StepProgress.Result[(int)StepNumber.Step8] = "Done";

                    //Step 8: Status
                    Process1_StepProgress.Status[(int)StepNumber.Step8] = (int)StatusNo.OK;
                }
                else
                {
                    //Step 8: Result
                    Process1_StepProgress.Result[(int)StepNumber.Step8] = "Error";
                    //Step 8: Status
                    Process1_StepProgress.Status[(int)StepNumber.Step8] = (int)StatusNo.NG;
                }
            }
            //The Step has begun
            //Trace enable
            if (Engineering.OEngineering.OMsgTrace.StoreResults) HMI.OForm.ProdMessages("Step 8: End Store Results process\n", "PLC");
            Process1_StepProgress.State[(int)StepNumber.Step8] = (int)StatusNo.Done;
            //Final status of the Step
            return Process1_StepProgress.Status[(int)StepNumber.Step8];
        }
        #endregion

        #endregion

        #region Threads

        #region Process 1
        //Start
        public void InitProcess1()
        {
            if (!status[(int)ProcessNumber.Process1])
            {
                status[(int)ProcessNumber.Process1] = true;
                //Active the process
                Thread Tick = new Thread(new ThreadStart(() => Process1(MAX_STEPS)));
                Tick.Start(); 
            }
        }
        //Process
        public int Process1(int Steps)
        {
            int Fail = 0;
            ResultCode = "No Result";

            #region Process secuence
            while (status[(int)ProcessNumber.Process1])
            {
                #region Initial values for the Process
                //Progress of the process 1
                progress[(int)ProcessNumber.Process1] = 0.0f;
                //Result of the process 1
                result[(int)ProcessNumber.Process1] = false;
                //Start Cycle Time of the process
                OOEE.Init_CycleTimer1();
                //PLC: Process Finished
                OSafetyPLC.S1B2_3[0] = false;
                //PCON: Servo ON
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.SON] = true;
                #endregion

                //Message
                if (Engineering.OEngineering.OMsgTrace.Process1)
                {
                    HMI.OForm.ProdMessages("Starting process 1\n", "PLC");
                }

                #region Steps

                #region Initial values for the Steps
                //Initial status of the steps
                for (int i = 0; i < MAX_STEPS; i++)
                {
                    Process1_StepProgress.Status[i] = (int)StatusNo.NG;
                    Process1_StepProgress.State[i] = (int)StatusNo.Wait;
                }
                //Initial status of the results
                for (int i = 0; i < MAX_STEPS; i++)
                {
                    Process1_StepProgress.Result[i] = "0";
                }
                //Initial status of the measurements
                for (int i = 0; i < Process1_StepProgress.Measurements.Length; i++)
                {
                    Process1_StepProgress.Measurements[i] = "0.0";
                }

                //Initial Values To PLC results
                //PLC: Locker Permission
                OSafetyPLC.S1B2_3[1] = false;
                //PLC: Press Permission
                OSafetyPLC.S1B2_3[5] = false;
                //To PLC: Screwdriver Permission
                OSafetyPLC.S1B2_3[6] = false;

                //Screwdriving process enabled?
                if (Engineering.OEngineering.OFunctions.Screwdriving)
                {
                    //Torque value
                    OMTFocus6K.DataField_MID1202[31, 5] = "0.0";
                    //Angle value
                    OMTFocus6K.DataField_MID1202[32, 5] = "0.0";
                    //Clamp Torque
                    OMTFocus6K.Clamp = "0.0";
                    //Seating Point Torque
                    OMTFocus6K.SeatingPoint = "0.0";
                    //Disable the Screwdriver
                    OMTFocus6K.SendCommand(OMTFocus6K.MID0224);
                }
                #endregion

                #region Start the sequence
                //Step 1: Execute (Scann the Serial ID )
                if (Step1(Engineering.OEngineering.OFunctions.Scanning) == (int)StatusNo.NG)
                {
                    Fail = 100;
                    //To PLC: Scanning (Press Permission)
                    OSafetyPLC.S1B2_3[1] = false;
                    //Product ANDON Yellow = No Read
                    OSafetyPLC.S1B2_3[4] = true;
                }
                else
                {
                    //Product ANDON Yellow = No Read
                    OSafetyPLC.S1B2_3[4] = false;

                    //Update the progress
                    progress[(int)ProcessNumber.Process1] = (1 * 100) / Steps;
                    //Timeout
                    Thread.Sleep(100);

                    //Step Check Components: Execute
                    if (Step_CheckComponents(Engineering.OEngineering.OFunctions.ChkComp) == (int)StatusNo.NG)
                    {
                        Fail = 100;
                        //To PLC: Check Components (Press Permission)
                        OSafetyPLC.S1B2_3[1] = false;
                    }
                    else
                    {
                        //Update the progress
                        progress[(int)ProcessNumber.Process1] = (2 * 100) / Steps;
                        //Timeout
                        Thread.Sleep(100);

                        //Step 2: Execute (Prior Op Check)
                        if (Step2(Engineering.OEngineering.OFunctions.PriorOpCheck) == (int)StatusNo.NG)
                        {
                            Fail = 200;
                            //To PLC: Prior Op Check (Press Permission)
                            OSafetyPLC.S1B2_3[1] = false;
                        }
                        else
                        {
                            //Update the progress
                            progress[(int)ProcessNumber.Process1] = (3 * 100) / Steps;
                            //Timeout
                            Thread.Sleep(100);

                            //To PLC: Scanning & Prior Op Check (Locker Permission)
                            OSafetyPLC.S1B2_3[5] = true;
                            //From PLC: Locker in position to perfom the Vision inspection
                            bool _LockerReady = LockerReady(100);

                            //To PLC: Permission to entry (Plunger Installation)
                            OSafetyPLC.S1B2_3[7] = true;

                            //Step 3A: Spring Inspection (Retry)
                            #region  Spring Inspection
                            while (Step3A(Engineering.OEngineering.OFunctions.Measurement1, _LockerReady) == (int)StatusNo.NG)
                            {
                                if (OSafetyPLC.DirectOut28[3])
                                {
                                    Fail = 300;
                                    Process1_StepProgress.Result[(int)StepNumber.Step3A] = "Machine Failure";
                                    break;
                                }
                                else
                                {
                                    //Stop Process button
                                    if (stopRequest)
                                    {
                                        Fail = 300;
                                        break;
                                    }
                                    //Wait for continue button to retry again
                                    SubProcessTimeout(500);
                                }
                            }
                            #endregion

                            //Machine failure: Abort Process
                            if (Fail != 300)
                            {
                                //Waiting for Plunger inspection permission
                                SubProcessTimeout(500);

                                //Update the progress
                                progress[(int)ProcessNumber.Process1] = (4 * 100) / Steps;
                                Thread.Sleep(100);

                                //Step 3B: Plunger Installation (Retry)
                                #region Plunger Installation
                                while (Step3B(Engineering.OEngineering.OFunctions.Measurement1, _LockerReady) == (int)StatusNo.NG)
                                {
                                    if (OSafetyPLC.DirectOut28[3])
                                    {
                                        Fail = 300;
                                        Process1_StepProgress.Result[(int)StepNumber.Step3B] = "Machine Failure";
                                        break;
                                    }
                                    else
                                    {
                                        //Stop Process button
                                        if (stopRequest)
                                        {
                                            Fail = 300;
                                            break;
                                        }
                                        //Wait for continue button to retry again
                                        SubProcessTimeout(500);
                                    }
                                }
                                #endregion

                                //Machine failure: Abort Process
                                if (Fail != 300)
                                {
                                    //To PLC: Permission to entry (Plunger Installation)
                                    OSafetyPLC.S1B2_3[7] = false;

                                    //Update the progress
                                    progress[(int)ProcessNumber.Process1] = (5 * 100) / Steps;
                                    Thread.Sleep(500);

                                    //Vision Inspection was successful 
                                    //To PLC: Press Permission to move down
                                    OSafetyPLC.S1B2_3[1] = true;

                                    //Servo press: Ready position
                                    bool _PressReady = ReadyPosition();

                                    //Step: Spring Force Test 1
                                    if (Step_SpringForceTest1(Engineering.OEngineering.OFunctions.FDTest1, _PressReady) == (int)StatusNo.NG)
                                    {
                                        Fail = 200;
                                        //To PLC: Press Permission to move up
                                        OSafetyPLC.S1B2_3[1] = false;
                                    }
                                    else
                                    {
                                        //Update the progress
                                        progress[(int)ProcessNumber.Process1] = (6 * 100) / Steps;
                                        Thread.Sleep(100);

                                        //Step: Spring Force Test 2
                                        if (Step_SpringForceTest2(Engineering.OEngineering.OFunctions.FDTest2, _PressReady) == (int)StatusNo.NG)
                                        {
                                            Fail = 200;
                                            //To PLC: Press Permission to move up
                                            OSafetyPLC.S1B2_3[1] = false;
                                            Thread.Sleep(200);
                                        }
                                        else
                                        {
                                            //Update the progress
                                            progress[(int)ProcessNumber.Process1] = (7 * 100) / Steps;
                                            Thread.Sleep(100);

                                            //Step: Constant K calculation
                                            if (Step_ConstKCalc(Engineering.OEngineering.OFunctions.ConstKcalc, _PressReady) == (int)StatusNo.NG)
                                            {
                                                Fail = 200;
                                                //To PLC: Press Permission to move up
                                                OSafetyPLC.S1B2_3[1] = false;
                                            }
                                            else
                                            {
                                                //Update the progress
                                                progress[(int)ProcessNumber.Process1] = (8 * 100) / Steps;
                                                Thread.Sleep(100);

                                                //Machine failure or Abort Process
                                                if (OSafetyPLC.DirectOut28[3] || stopRequest)
                                                {
                                                    Fail = 300;
                                                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.MachineFailure);
                                                }
                                                else
                                                {
                                                    //Servo press: Screwdriving position
                                                    _PressReady &= ScrewdrivingPosition();

                                                    //Servo Press in position to sccrewdriving?
                                                    if (_PressReady)
                                                    {
                                                        //Screwdriving Process enabled?
                                                        if (Engineering.OEngineering.OFunctions.Screwdriving)
                                                        {
                                                            //To PLC: Screwdriver Permission
                                                            OSafetyPLC.S1B2_3[6] = true;
                                                            //Enable the Screwdriver
                                                            OMTFocus6K.SendCommand(OMTFocus6K.MID0225);
                                                            Thread.Sleep(500);
                                                            //Wait for Screw Results
                                                            OMTFocus6K.ScrewDone = false;
                                                            bool ScrewdriverFinished = ScrewdrivingTimeout(500);
                                                            //You've 30s to screwdriving
                                                            if (!ScrewdriverFinished)
                                                            {
                                                                Fail = 300;
                                                                ResultCode = Omachcomm.ResultCode((int)ResultCodes.MachineFailure);
                                                                HMI.OForm.ProdMessages("Timeout to install the plunger or Screwdriver \n", "Error");
                                                            }
                                                        }
                                                        //To PLC: Screwdriver Permission
                                                        OSafetyPLC.S1B2_3[6] = false;

                                                        //To PLC: Press Permission to move up
                                                        OSafetyPLC.S1B2_3[1] = false;

                                                        if (OSafetyPLC.DirectOut28[3] || stopRequest)
                                                        {
                                                            Fail = 300;
                                                            ResultCode = Omachcomm.ResultCode((int)ResultCodes.MachineFailure);
                                                        }
                                                        else
                                                        {
                                                            //Step 4: Screwdriver permission and Torque & Angle Measurements
                                                            if (Step4(Engineering.OEngineering.OFunctions.Screwdriving, _PressReady) == (int)StatusNo.NG)
                                                            {
                                                                //On this step, we cannot try again the process because the part had a physical impact 
                                                                Fail++;
                                                            }

                                                            //Update the progress
                                                            progress[(int)ProcessNumber.Process1] = (9 * 100) / Steps;
                                                            Thread.Sleep(100);

                                                            //Step 5: Store Measurements
                                                            if (Step5(Engineering.OEngineering.OFunctions.StoreMeasurements) == (int)StatusNo.NG)
                                                            {
                                                                Fail++;
                                                            }
                                                            //Update the progress
                                                            progress[(int)ProcessNumber.Process1] = (10 * 100) / Steps;
                                                            Thread.Sleep(100);

                                                            //Step 6: Check if pop up any alarm during the process
                                                            if (Step6(OSafetyPLC.DirectOut28[3]) == 0)
                                                            {
                                                                Fail++;
                                                            }
                                                            //Update the progress
                                                            progress[(int)ProcessNumber.Process1] = (11 * 100) / Steps;
                                                            Thread.Sleep(100);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Fail = 300;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }                                                
                    }
                }
                #endregion

                #endregion

                //To PLC:(Locker Permission)
                OSafetyPLC.S1B2_3[5] = false;

                //Disable the Screwdriver
                //Screwdriving process enabled?
                if (Engineering.OEngineering.OFunctions.Screwdriving) OMTFocus6K.SendCommand(OMTFocus6K.MID0224);

                //To PLC: Press Permission to move up
                OSafetyPLC.S1B2_3[1] = false;
                //Servo press: Standby position
                if(!OSafetyPLC.DirectOut28[3]) StandbyPosition();

                //Safety PLC:  PC_Process1_Finished Safe to remove the part as safety way
                OSafetyPLC.S1B2_3[0] = true;

                //Process 1: Results without Update to DB
                //Preparing the results to send them to the DB
                #region Final Results: Update Device Status Process

                if (Fail > 0)
                {
                    //Fail 100 = if 2D Code wasn't read it, it can not count as bad part neither its status.
                    //Fail 200 = if the part has a prior op Check is counted as bad part, but it doesn't update its status.
                    //Fail 300 = if somwthing wrong with the machine is counted as bad part, but it doesn't update its status.

                    if (Fail != 100 && Fail != 200 && Fail != 300)
                    {
                        //Part Fail or NG
                        result[(int)ProcessNumber.Process1] = false;

                        #region Assign Components to Serial ID
                        if (Step_AssignComponents(Engineering.OEngineering.OFunctions.AssignComp) == (int)StatusNo.NG)
                        {
                            Fail = 200;
                        }
                        else
                        {
                            //Update the progress
                            progress[(int)ProcessNumber.Process1] = (12 * 100) / Steps;
                            Thread.Sleep(200);

                            //The part will update to the DB as a NG Part + Result Code
                            #region Step 7
                            //Step 7: Update Device Status
                            if (Step7(Engineering.OEngineering.OFunctions.UpdateDeviceStatus, ResultCode) == (int)StatusNo.NG)
                            {
                                Fail = 200;
                            }
                            //Update the progress
                            progress[(int)ProcessNumber.Process1] = (13 * 100) / Steps;
                            Thread.Sleep(800);
                            #endregion
                        }
                        #endregion
                    }
 
                    if (Fail == 200)
                    {
                        //Part Fail or NG
                        result[(int)ProcessNumber.Process1] = false;
                    }
                }
                else
                {
                    //Part GOOD or OK
                    result[(int)ProcessNumber.Process1] = true;
                    //Final Result Code
                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.Good);

                    #region Assign Components to Serial ID
                    if (Step_AssignComponents(Engineering.OEngineering.OFunctions.AssignComp) == (int)StatusNo.NG)
                    {
                        Fail = 200;
                        //Error at assign the components to DB, therefor the Part is NG 
                        result[(int)ProcessNumber.Process1] = false;
                    }
                    else
                    {
                        //Update the progress
                        progress[(int)ProcessNumber.Process1] = (12 * 100) / Steps;
                        Thread.Sleep(200); 

                        //The part will update to the DB as a GOOD
                        #region Step 7
                        //Step 7: Update Device Status
                        if (Step7(Engineering.OEngineering.OFunctions.UpdateDeviceStatus, ResultCode) == (int)StatusNo.NG)
                        {
                            Fail = 200;
                        }
                        //Update the progress
                        progress[(int)ProcessNumber.Process1] = (13 * 100) / Steps;
                        Thread.Sleep(800);
                        #endregion
                    }
                    #endregion
                }
                #endregion

                //Process 1: Final Results 
                //If the results were check it and they were updated, 
                #region Final Results
                //Fail 100 = if 2D Code wasn't read it, it can not count as bad part neither its status.
                //Fail 200 = if the part has a prior op Check is counted as bad part, but it doesn't update its status.
                //Fail 300 = if somwthing wrong with the machine is counted as bad part, but it doesn't update its status.

                //Read OK and Part OK
                if (result[(int)ProcessNumber.Process1] && (Fail != 100 && Fail != 200 && Fail != 300))
                {
                    //Final result of the process
                    LotManager.OLotManager.OLot.OKParts++;
                    //Add the Parts produced to Production control
                    OOEE.OQuality.OKParts++;
                    //Final Result Code
                    ResultCode = Omachcomm.ResultCode((int)ResultCodes.Good);

                    //Status of the Part = OK
                    OSQLServer.ProdLot_Param[(int)Prod_Lot.PartStatus] = "OK";
                    //Store Results
                    #region Step 8
                    Step8(Engineering.OEngineering.OFunctions.StoreResults, ResultCode);
                    //Update the progress
                    progress[(int)ProcessNumber.Process1] = (14 * 100) / Steps;
                    #endregion
                }
                //Read OK and Part NG
                if (!result[(int)ProcessNumber.Process1] && Fail != 100)
                {
                    //Final result of the process
                    LotManager.OLotManager.OLot.NGParts++;
                    //Add the Parts produced to Production control
                    OOEE.OQuality.NGParts++;

                    //Status of the Part = NG
                    OSQLServer.ProdLot_Param[(int)Prod_Lot.PartStatus] = "NG";
                    //Store Results
                    #region Step 8
                    Step8(Engineering.OEngineering.OFunctions.StoreResults, ResultCode);
                    //Update the progress
                    progress[(int)ProcessNumber.Process1] = (14 * 100) / Steps;
                    #endregion
                }
                #endregion

                //Stop Cycle time of the process
                OOEE.Stop_CycleTimer1();

                //PCON: DSTR OFF
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR] = false;
                //PCON: Servo OFF
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.SON] = false;
                
                //Message
                if (Engineering.OEngineering.OMsgTrace.Process1)
                {
                    HMI.OForm.ProdMessages("Process 1 has finished\n", "PLC");
                }

                //End the Process
                status[(int)ProcessNumber.Process1] = false;              
            }
            #endregion

            return Fail;
        }
        //Stop
        public void StopProcess1()
        {
            status[(int)ProcessNumber.Process1] = false;
        }
        #endregion

        #endregion
    }
}
