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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
#endregion

#region Project Libraries
using PCdatetime;
using PCMessages;
#endregion

namespace Software
{
    public partial class HMI : Form
    {
        #region Variables
        //Paths for Log of Production Lot & System
        public string pathProdData1 = "ProductionData/ProdLot_Log.txt";
        public string pathProdData2 = "MachineData/System_Log.txt";
        public string pathProdData3 = "MachineData/MachineConfig.csv";

        #region External Control
        // Automatic = false, Manual = true
        private static bool machineMode;
        public bool MachineMode
        {
            get
            {
                return machineMode;
            }

            set
            {
                machineMode = value;
            }
        }
        
        public enum Devices
        {
            PLC,
            SafetyPLC,
            Scanner,
            Instrument1,
            Instrument2,
            Kistler,
            PCON
        }
        public struct OneShot
        {
            public bool Home,
                        Reset,
                        ManualAuto;
        }
   
        #endregion

        #region CSV File machine settings
        //Machine config file 1 - 45
        private const int MAX_CSVfields = 45;
        private static string[] cSVfile = new string[MAX_CSVfields] {  "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none",
                                                                       "none"};
        public string[] CSVfile
        {
            get
            {
                return cSVfile;
            }

            set
            {
                cSVfile = value;
            }
        }
        #endregion

        #endregion

        #region Callbacks
        //System messages (thread safe calls)
        delegate void SystemMsgCallback(string Texto, string Tipo);
        //Production messages (thread safe calls)
        delegate void ProdMsgCallback(string Texto, string Tipo);
        #endregion

        #region Objects

        #region Structures
        OneShot OOneShot;
        #endregion

        #region Class & Libreries
        //Time form the system (PC desktop)
        FechaHora LocalTime = new FechaHora();
        //Messages
        Messages MachineMsg = new Messages();
        //Alarms & Events
        DataTable OTableAE = new DataTable();
        //PLC
        PLC OPLC = new PLC();
        //Safety Controller
        SafetyPLC OSafetyPLC = new SafetyPLC();
        //Lot manager
        LotManager OLotManager = new LotManager();
        //Message of Alarms & Events
        MsgAE OMsgAE = new MsgAE();
        //Process
        ProdProcess OProcess = new ProdProcess();
        //Reports
        Reports OReports = new Reports();
        //SQL Database
        SQLDatabase OSQLServer = new SQLDatabase();
        //Scanner: OMRON, V430-F
        Scanner OScanner = new Scanner();
        //Screwdriver Controller: Atlas Copco, MTFocus 6000
        MTFocus6K OMTFocus6K = new MTFocus6K();
        //Vision Sensor: Keyence, IV2-G500MA
        IV2G500MA OVisionSensor = new IV2G500MA();
        //Kistler: Force and Displacement Instrument
        Kistler_5847B0 OKistler = new Kistler_5847B0();
        //PCON-CB: Servomotor controller
        IAI_PCON OPCON = new IAI_PCON();
        #endregion

        #region Pages (winforms)
        //HMI
        public static HMI OForm;
        //Production Page
        Production ProdPag = new Production();
        //Lot Manager Page
        LotManager LotManagerPag = new LotManager();
        //Engineering Page
        Engineering EngineeringPag = new Engineering();
        //Diagnostics Page
        Diagnostics DiagnosticsPage = new Diagnostics();
        //User Access Page
        UserLogSystem UserControlPage = new UserLogSystem();
        //Station Connection Settings page
        StationConnectionSettings ConnSettingsPage = new StationConnectionSettings();
        #endregion

        #endregion

        /*Initialization of the HMI form*/
        public HMI()
        {
            InitializeComponent();
            //HMI Page
            OForm = this;

            //Notification
            img_SystemProd.Visible = false;
            img_AlarmsEvents.Visible = false;

            //Reading the CSV machine config file
            ReadMachineConfigFile();

            //Create or verify the Store measurements CSV File
            OReports.WriteStoreMeasurements();
            //Create or verify the Store Results CSV File
            OReports.WriteStoreResults();

            //Assign the values on base of the Machine Config file settings
            #region Machine Labels
            txt_StationNo.Text = cSVfile[(int)CSV_MachConfig.MachineNumber];
            txt_StationName.Text = cSVfile[(int)CSV_MachConfig.MachineName];
            //Winforms headers labels
            OForm.Text = "Sensata Technologies | " + cSVfile[(int)CSV_MachConfig.MachineName];
            Diagnostics.ODiagnostics.Text = cSVfile[(int)CSV_MachConfig.MachineName] + " | Diagnostics";
            Production.OProduction.Text = cSVfile[(int)CSV_MachConfig.MachineName] + " | Production";
            Engineering.OEngineering.Text = cSVfile[(int)CSV_MachConfig.MachineName] + " | Engineering";
            LotManager.OLotManager.Text = cSVfile[(int)CSV_MachConfig.MachineName] + " | Lot Manager";
            UserLogSystem.OUserLogSystem.Text = cSVfile[(int)CSV_MachConfig.MachineName] + " | Users Access";
            StationConnectionSettings.OStationConnSettings.Text = cSVfile[(int)CSV_MachConfig.MachineName] + " | Communications";
            #endregion

            #region SQL Database settings
            //Station ID
            OSQLServer.StationID = Convert.ToInt64(cSVfile[(int)CSV_MachConfig.StationID]);
            //Equipment ID
            OSQLServer.EquipmentID = Convert.ToInt64(cSVfile[(int)CSV_MachConfig.EquipmentID]);           
            //Server name
            OSQLServer.Server= cSVfile[(int)CSV_MachConfig.SQLServerName];
            //Database name
            OSQLServer.DataBase = cSVfile[(int)CSV_MachConfig.SQLDBName];
            //User
            OSQLServer.SQLServerUser = cSVfile[(int)CSV_MachConfig.SQLUser];
            //App Role User
            OSQLServer.SQLqueryUser= cSVfile[(int)CSV_MachConfig.AppRoleUser];
            //Prior Op Check table
            OSQLServer.TblPriorOpCheck = cSVfile[(int)CSV_MachConfig.PriorOpCheck];
            //Update Device Status table
            OSQLServer.TblUpdateDeviceStatus = cSVfile[(int)CSV_MachConfig.UpdateDeviceStatus];
            //Limits table
            OSQLServer.TblLimits = cSVfile[(int)CSV_MachConfig.LimitsOperParams];
            //Results History
            OSQLServer.TblResultsHistory = cSVfile[(int)CSV_MachConfig.ResultsHistory];
            //Master Parts Sequence View
            OSQLServer.TblMastersPartsSeqView = cSVfile[(int)CSV_MachConfig.MasterPartsSeqView];
            //Device ID table 
            OSQLServer.TblDeviceIDs = cSVfile[(int)CSV_MachConfig.DeviceIDTable];
            //Device Family 
            OSQLServer.DeviceFamily = cSVfile[(int)CSV_MachConfig.DeviceFamily];
            //Station name
            OSQLServer.Station = cSVfile[(int)CSV_MachConfig.StationName];
            //Check Components
            OSQLServer.TblCheckComponents = cSVfile[(int)CSV_MachConfig.CheckComponents];
            //Assign Component to Serial ID
            OSQLServer.TblAssignComponentsToSerial = cSVfile[(int)CSV_MachConfig.AssignComponents];
            #endregion

            #region PLC & Safety PLC settings
            //PLC IP
            OPLC.IP = cSVfile[(int)CSV_MachConfig.PLCIP];
            //Safety PLC IP
            OSafetyPLC.IP = cSVfile[(int)CSV_MachConfig.SafetyPLCIP];
            //Safety PLC Port
            OSafetyPLC.Port = Convert.ToInt16(cSVfile[(int)CSV_MachConfig.SafetyPLCPort]);
            #endregion

            #region Scanner Settings
            //Safety PLC IP
            OScanner.IP = cSVfile[(int)CSV_MachConfig.ScannerIP];
            //Safety PLC Port
            OScanner.Port = Convert.ToInt16(cSVfile[(int)CSV_MachConfig.ScannerPort]);
            #endregion

            #region Instrumentation Settings
            //MT Focus 6K Atlas Copco
            OMTFocus6K.IP       = cSVfile[(int)CSV_MachConfig.Instrument1IP];
            OMTFocus6K.Port     = cSVfile[(int)CSV_MachConfig.Instrument1Port];
            //Keyence IV2-G500MA
            OVisionSensor.IP    = cSVfile[(int)CSV_MachConfig.Instrument2IP];
            OVisionSensor.Port = Convert.ToInt32(cSVfile[(int)CSV_MachConfig.Instrument2Port]);
            OVisionSensor.StartPoint  = cSVfile[(int)CSV_MachConfig.PCIP];
            //Kistler module: IP Address
            OKistler.IP = cSVfile[(int)CSV_MachConfig.KistlerIP];
            //PCON Controller: IP Address
            OPCON.IP = cSVfile[(int)CSV_MachConfig.PCONIP];
            #endregion

            //Time form the system (PC desktop)
            LocalClock.Enabled = true;
            //Station
            lb_Station.Image = Image.FromFile("StationDisconnected.gif");
            lb_Station.ForeColor = Color.Red;
            btn_Initialize.Enabled = true;
            btn_Shutdown.Enabled = false;
        }
        /*Initialization of the HMI Winform*/
        private void HMI_Load(object sender, EventArgs e)
        {
            /*Alarms*/
            //Defines of the Columns of the Alarms & Events Table
            OTableAE.Columns.Add("Date/Time", typeof(string));
            OTableAE.Columns.Add("Section", typeof(string));
            OTableAE.Columns.Add("Notification", typeof(string));
            //Bind the Table to Data Grid Viewer
            dataGridView_Alarms.DataSource = OTableAE;
        }
        //Winform View Close
        private void HMI_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Close the current Lot
            if (!LotManager.OLotManager.LotManager_AE[(int)LotManager_parameters.LotActive])
            {
                OLotManager.Process_EndLot();
            }

            Thread.Sleep(500);
            //Shutdown the system
            ShuttingDownSystem();
            Thread.Sleep(500);
            //Close the application
            Environment.Exit(Environment.ExitCode);
        }

        #region Controls
        //Time form the system (PC desktop)
        private void LocalClock_Tick(object sender, EventArgs e)
        {
            //Date from PC
            lb_Fecha.Text = LocalTime.PCFecha();
            //Local Time from PC 
            lb_Hora.Text = LocalTime.PCHora();

            #region Screwdriver MTFocus6K
            //Keep Alive for MTFocus 6K Communication purpose      
            if (OMTFocus6K.Parameters[(int)Instrument_Comm.CommStatus])
            {
                //MT Focus 6K Controller needs a Keep Alive communication
                OMTFocus6K.SendCommand(OMTFocus6K.KeepAlive);

                if (!OMTFocus6K.SubscribeTightenDone)
                {
                    OMTFocus6K.SendCommand(OMTFocus6K.MID0008);
                    Thread.Sleep(500);
                    OMTFocus6K.SendCommand(OMTFocus6K.MID0900sub);
                    OMTFocus6K.SubscribeTightenDone = true;
                }
            }
            else
            {
                OMTFocus6K.SubscribeTightenDone = false;
            }
            #endregion

        }
        //Status of the machine system
        private void MachineStatus_Tick(object sender, EventArgs e)
        {
            #region Users System
            btn_UserAccess.Text = UserLogSystem.OUserLogSystem.User;

            switch (UserLogSystem.OUserLogSystem.User)
            {
                case "Developer":
                    btn_Production.Enabled = true;
                    btn_Diagnostics.Enabled = true;
                    btn_Engineering.Enabled = true;
                    btn_ConnSettings.Enabled = true;
                    break;
                case "Engineering":
                    btn_Production.Enabled = true;
                    btn_Diagnostics.Enabled = false;
                    btn_Engineering.Enabled = true;
                    btn_ConnSettings.Enabled = false;
                    break;
                case "Production":
                    btn_Production.Enabled = true;
                    btn_Diagnostics.Enabled = false;
                    btn_Engineering.Enabled = false;
                    btn_ConnSettings.Enabled = false;
                    break;
                case "Maintenance":
                    btn_Production.Enabled = false;
                    btn_Diagnostics.Enabled = true;
                    btn_Engineering.Enabled = false;
                    btn_ConnSettings.Enabled = true;
                    break;
                //Operator Mode
                default:
                    btn_Production.Enabled = false;
                    btn_Diagnostics.Enabled = false;
                    btn_Engineering.Enabled = false;
                    btn_ConnSettings.Enabled = false;
                    break;
            }
            #endregion

            #region Status of the Machine

            #region PLC IO

            #region PLC
            //lb_PartPresentPress.Checked = OPLC.K2M0_Array[2];
            #endregion

            #region Safety PLC
            //Part Present
            lb_PartPresent.Checked = OSafetyPLC.Inputs[15];
            //Air Supply
            lb_AirSupply.Checked = !OSafetyPLC.Inputs[14];
            //Safety System OK
            lb_SafetySys.Checked = !OSafetyPLC.Outputs[2];
            //Safety Curtains
            lb_SafetyCurtains.Checked = !OSafetyPLC.Inputs[4];
            //Continue Sequence
            lb_ContinueSequence.Checked = OSafetyPLC.Inputs[6];
            #endregion

            #endregion

            #region Servo Press (PCON)
            txt_PCON_CurrPos.Text = OPCON.CurrentPosition_value.ToString("000.000");
            //Servo press Up
            lb_PressUp.Checked = Math.Round(OPCON.CurrentPosition_value) == Math.Round(Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.StandbyPos]));
            //Servo press Down
            lb_PressDown.Checked = Math.Round(OPCON.CurrentPosition_value) == Math.Round(Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ScrewdrivingPos]));
            #endregion

            #region Process 1

            //Progresss of the Process
            gauge_CycleProgress.Value = (float)OProcess.Progress[(int)ProcessNumber.Process1];

            #region Step 1: Read 2D Code
            //Results
            txt_Scan.Text = OProcess.Process1_StepProgress.Result[(int)StepNumber.Step1];
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step1] == (int)StatusNo.InProcess) lb_Scan.BackColor = Color.LightCyan;
            else lb_Scan.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step1] == (int)StatusNo.OK)
            {
                txt_Scan.ForeColor = Color.Green;
                //Message: if the Scanner couldn't read, check the label 
                txt_MsgCheckScanner.Visible = false;
            }
            else
            {
                txt_Scan.ForeColor = Color.Red;
                if (OProcess.Process1_StepProgress.Result[(int)StepNumber.Step1] == "No Read")
                {
                    //Message: if the Scanner couldn't read, check the label 
                    txt_MsgCheckScanner.Visible = true;
                }
            }
            #endregion

            #region Step 2: Prior Op Check
            //Results
            if (Engineering.OEngineering.OFunctions.PriorOpCheck)
            {
                //Display all Prior Op Check messages
                txt_PriorOpCheck.Text = OSQLServer.POChkMsg;
            }
            else
            {
                //Disable message
                txt_PriorOpCheck.Text = OProcess.Process1_StepProgress.Result[(int)StepNumber.Step2];
            }
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step2] == (int)StatusNo.InProcess) lb_PriorOpCheck.BackColor = Color.LightCyan;
            else lb_PriorOpCheck.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step2] == (int)StatusNo.OK)
            {
                txt_PriorOpCheck.ForeColor = Color.Green;
            }
            else
                txt_PriorOpCheck.ForeColor = Color.Red;
            #endregion

            #region Step: Check Components
            //Results
            if (Engineering.OEngineering.OFunctions.ChkComp)
            {
                //Display all Check Components messages
                txt_ChkComp.Text = OSQLServer.CheckComponents_Msg;
            }
            else
            {
                //Disable message
                txt_ChkComp.Text = OProcess.Process1_StepProgress.Result[(int)StepNumber.Step_CheckComponents];
            }
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step_CheckComponents] == (int)StatusNo.InProcess) lb_ChkComp.BackColor = Color.LightCyan;
            else lb_ChkComp.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step_CheckComponents] == (int)StatusNo.OK)
            {
                txt_ChkComp.ForeColor = Color.Green;
            }
            else
                txt_ChkComp.ForeColor = Color.Red;

            #endregion

            #region Step 3A: Visual Inspection: Spring
            //Results
            txt_Measurement1.Text = OProcess.Process1_StepProgress.Result[(int)StepNumber.Step3A];
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step3A] == (int)StatusNo.InProcess) lb_VisualInspSpring.BackColor = Color.LightCyan;
            else lb_VisualInspSpring.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step3A] == (int)StatusNo.OK)
            {
                txt_Measurement1.ForeColor = Color.Green;
                //Message: Inspection failure 
                txt_MsgTryAgain.Visible = false;
            }
            else
            {
                txt_Measurement1.ForeColor = Color.Red;
                if (OProcess.Process1_StepProgress.Result[(int)StepNumber.Step3A] == "Machine Failure")
                {
                    //Message: Inspection failure 
                    txt_MsgTryAgain.Visible = false;
                }
                else
                {
                    if (OProcess.Process1_StepProgress.Result[(int)StepNumber.Step3A] == "Inspection Failure")
                    {
                        //Message: Inspection failure 
                        txt_MsgTryAgain.Visible = true;
                    }
                }
            }

            #endregion

            #region Step 3B: Visual Inspection: Plunger
            //Spring Inspection and Install Plunger
            txt_InstallPlungerArmtr.Visible = OSafetyPLC.S1B2_3[7] && (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step3A] == (int)StatusNo.OK);
            //Results
            txt_Measurement2.Text = OProcess.Process1_StepProgress.Result[(int)StepNumber.Step3B];
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step3B] == (int)StatusNo.InProcess) lb_VisualInspPlunger.BackColor = Color.LightCyan;
            else lb_VisualInspPlunger.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step3B] == (int)StatusNo.OK)
            {
                txt_Measurement2.ForeColor = Color.Green;
                //Message: Inspection failure 
                txt_MsgTryAgain.Visible = false;
            }
            else
            {
                txt_Measurement2.ForeColor = Color.Red;
                if (OProcess.Process1_StepProgress.Result[(int)StepNumber.Step3B] == "Machine Failure")
                {
                    //Message: Machine failure 
                    txt_StopProcess.Visible = true;
                    //Message: Inspection failure 
                    txt_MsgTryAgain.Visible = false;
                }
                else
                {
                    if (OProcess.Process1_StepProgress.Result[(int)StepNumber.Step3B] == "Inspection Failure")
                    {
                        //Message: Inspection failure 
                        txt_MsgTryAgain.Visible = true;
                        //Message: Machine failure 
                        txt_StopProcess.Visible = false;
                    }
                }
            }

            #endregion

            #region Step: Force & Displacement (FD)
            //Display Kistler current values
            txt_DisplayChX.Text = OKistler.ChannelX_Value.ToString("000.000");
            txt_DisplayChY.Text = OKistler.ChannelY_Value.ToString("000.000");
            #endregion

            #region Step: Spring Force Test 1
            //Results 
            txt_Test1ChXMeas.Text = OProcess.Process1_StepProgress.Measurements[(int)Measurements.Test1Displacement];
            txt_Test1ChYMeas.Text = OProcess.Process1_StepProgress.Measurements[(int)Measurements.Test1Force];
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.StepFDTest1] == (int)StatusNo.InProcess) lb_Test1.BackColor = Color.LightCyan;
            else lb_Test1.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.StepFDTest1] == (int)StatusNo.OK)
            {
                txt_Test1ChXMeas.ForeColor = Color.Green;
                txt_Test1ChYMeas.ForeColor = Color.Green;
            }
            else
            {
                txt_Test1ChXMeas.ForeColor = Color.Red;
                txt_Test1ChYMeas.ForeColor = Color.Red;
                if (OProcess.Process1_StepProgress.Result[(int)StepNumber.StepFDTest1] == "Machine Failure")
                {
                    //Message: Error measurement due machine failure
                    txt_MsgTryAgain.Visible = true;
                }
            }
            #endregion

            #region Step: Spring Force Test 2
            //Results 
            txt_Test2ChXMeas.Text = OProcess.Process1_StepProgress.Measurements[(int)Measurements.Test2Displacement];
            txt_Test2ChYMeas.Text = OProcess.Process1_StepProgress.Measurements[(int)Measurements.Test2Force];
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.StepFDTest2] == (int)StatusNo.InProcess) lb_Test2.BackColor = Color.LightCyan;
            else lb_Test2.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.StepFDTest2] == (int)StatusNo.OK)
            {
                txt_Test2ChXMeas.ForeColor = Color.Green;
                txt_Test2ChYMeas.ForeColor = Color.Green;
            }
            else
            {
                txt_Test2ChXMeas.ForeColor = Color.Red;
                txt_Test2ChYMeas.ForeColor = Color.Red;
                if (OProcess.Process1_StepProgress.Result[(int)StepNumber.StepFDTest2] == "Machine Failure")
                {
                    //Message: Error measurement due machine failure
                    txt_MsgTryAgain.Visible = true;
                }
            }
            #endregion

            #region Step: Constant K
            //Results 
            txt_KistlerConstK.Text = OProcess.Process1_StepProgress.Measurements[(int)Measurements.ConstantK];
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.StepFDConstK] == (int)StatusNo.InProcess) lb_ConstantK.BackColor = Color.LightCyan;
            else lb_ConstantK.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.StepFDConstK] == (int)StatusNo.OK)
            {
                txt_KistlerConstK.ForeColor = Color.Green;
                txt_KistlerConstK.ForeColor = Color.Green;
            }
            else
            {
                txt_KistlerConstK.ForeColor = Color.Red;
                txt_KistlerConstK.ForeColor = Color.Red;
                if (OProcess.Process1_StepProgress.Result[(int)StepNumber.StepFDConstK] == "Machine Failure")
                {
                    //Message: Error measurement due machine failure
                    txt_MsgTryAgain.Visible = true;
                }
            }
            #endregion

            #region Step 4: Measurements Torque and Angle
            //Screwdriving Proces label
            txt_Screwdrinving.Visible = OSafetyPLC.S1B2_3[6];
            //Screwdriving process status
            txt_NoScrewdriving.Visible = OSafetyPLC.Inputs[6] && !OMTFocus6K.ScrewDone && OSafetyPLC.S1B2_3[6];
            //Results
            //Total Torque
            txt_MeasTorque.Text = OMTFocus6K.DataField_MID1202[31, 5];
            //Total Angle
            txt_MeasAngle.Text = OMTFocus6K.DataField_MID1202[32, 5];
            //Clamp Torque
            txt_MeasClamp.Text = OMTFocus6K.Clamp;
            //Clamp Angle
            txt_MeasClampAngle.Text = OMTFocus6K.ClampAngle;
            //Seating Point torque
            txt_MeasSP.Text = OMTFocus6K.SeatingPoint;

            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step4] == (int)StatusNo.InProcess) lb_Screwdriving.BackColor = Color.LightCyan;
            else lb_Screwdriving.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step4] == (int)StatusNo.OK)
            {
                txt_MeasTorque.ForeColor = Color.Green;
                txt_MeasAngle.ForeColor = Color.Green;
                txt_MeasClamp.ForeColor = Color.Green;
                txt_MeasClampAngle.ForeColor = Color.Green;
                txt_MeasSP.ForeColor = Color.Green;
            }
            else
            {
                txt_MeasTorque.ForeColor = Color.Red;
                txt_MeasAngle.ForeColor = Color.Red;
                txt_MeasClamp.ForeColor = Color.Red;
                txt_MeasClampAngle.ForeColor = Color.Red;
                txt_MeasSP.ForeColor = Color.Red;
            }
            #endregion

            #region Step 5: Store Measurements
            //Results
            txt_DBInsertMeas.Text = OProcess.Process1_StepProgress.Result[(int)StepNumber.Step5];
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step5] == (int)StatusNo.InProcess) lb_StoreMeas.BackColor = Color.LightCyan;
            else lb_StoreMeas.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step5] == (int)StatusNo.OK)
            {
                txt_DBInsertMeas.ForeColor = Color.Green;
            }
            else
                txt_DBInsertMeas.ForeColor = Color.Red;
            #endregion

            #region Step 6

            #endregion

            #region Step: Assign Components to Serial ID
            //Results
            if (Engineering.OEngineering.OFunctions.AssignComp)
            {
                //Display all Check Components messages
                txt_AssignComp.Text = OSQLServer.AssignComponentsToSerial_Msg;
            }
            else
            {
                //Disable message
                txt_AssignComp.Text = OProcess.Process1_StepProgress.Result[(int)StepNumber.Step_AssignComponents];
            }
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step_AssignComponents] == (int)StatusNo.InProcess) lb_AssignComp.BackColor = Color.LightCyan;
            else lb_AssignComp.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step_AssignComponents] == (int)StatusNo.OK)
            {
                txt_AssignComp.ForeColor = Color.Green;
            }
            else
                txt_AssignComp.ForeColor = Color.Red;
            #endregion

            #region Step 7: Update Device Status
            //Results
            txt_UpdateDeviceStatus.Text = OProcess.Process1_StepProgress.Result[(int)StepNumber.Step7];
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step7] == (int)StatusNo.InProcess) lb_UpdateDeviceStatus.BackColor = Color.LightCyan;
            else lb_UpdateDeviceStatus.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step7] == (int)StatusNo.OK)
            {
                txt_UpdateDeviceStatus.ForeColor = Color.Green;
            }
            else
                txt_UpdateDeviceStatus.ForeColor = Color.Red;
            #endregion

            #region Step 8: Store Results
            //Results
            txt_DBInsertResults.Text = OProcess.Process1_StepProgress.Result[(int)StepNumber.Step8];
            //State
            if (OProcess.Process1_StepProgress.State[(int)StepNumber.Step8] == (int)StatusNo.InProcess) lb_StoreResults.BackColor = Color.LightCyan;
            else lb_StoreResults.BackColor = Color.FromKnownColor(KnownColor.White);
            //Status
            if (OProcess.Process1_StepProgress.Status[(int)StepNumber.Step8] == (int)StatusNo.OK)
            {
                txt_DBInsertResults.ForeColor = Color.Green;
            }
            else
                txt_DBInsertResults.ForeColor = Color.Red;
            #endregion

            #region Process result
            //Display OK/NG
            if (OProcess.Result[(int)ProcessNumber.Process1] && gauge_CycleProgress.Value == 100)
            {
                txt_CycleResult.Text = "OK";
                txt_CycleResult.ForeColor = Color.Green;
                //ANDON of the product OK = Green
                OSafetyPLC.S1B2_3[2] = true;
                //ANDON of the product NG = Red
                OSafetyPLC.S1B2_3[3] = false;
            }
            else
            {
                txt_CycleResult.Text = "----";
                txt_CycleResult.ForeColor = Color.Blue;

                if (gauge_CycleProgress.Value < 100)
                {
                    //ANDON of the product OK = Green
                    OSafetyPLC.S1B2_3[2] = false;
                    //ANDON of the product NG = Red
                    OSafetyPLC.S1B2_3[3] = false;
                }

                if (!OProcess.Result[(int)ProcessNumber.Process1] && gauge_CycleProgress.Value == 100)
                {
                    txt_CycleResult.Text = "NG";
                    txt_CycleResult.ForeColor = Color.Red;
                    //ANDON of the product OK = Green
                    OSafetyPLC.S1B2_3[2] = false;
                    //ANDON of the product NG = Red
                    OSafetyPLC.S1B2_3[3] = true;    
                }
            }

            #region Color Arc of the Process result
            if (OProcess.Result[(int)ProcessNumber.Process1] && gauge_CycleProgress.Value == 100)
            {
                Process_Arc.BackColor = Color.FromArgb(168, 211, 169);
                Process_Arc.BackColor2 = Color.FromArgb(168, 211, 169);
            }
            else
            {
                Process_Arc.BackColor = Color.FromArgb(102, 210, 248);
                Process_Arc.BackColor2 = Color.FromArgb(102, 210, 248);

                if (!OProcess.Result[(int)ProcessNumber.Process1] && gauge_CycleProgress.Value == 100)
                {
                    Process_Arc.BackColor = Color.FromArgb(234, 166, 168);
                    Process_Arc.BackColor2 = Color.FromArgb(234, 166, 168);
                }
            }

            #endregion

            #region Messages

            #endregion

            #endregion

            #endregion

            #endregion

            #region External Control
            //Machine Control
            //Start Cycle of the Process1
            StartProcess1();
            //Reset (one shot)
            Reset();
            //Manual | Auto  (toggle)
            ManualAuto();
            //Stop Process
            StopProcess1();
            //Display the Machine Mode: Manual / Automatic
            if (!machineMode)
            {
                txt_MachineMode.Text =      "     Manual    ";
                txt_MachineMode.ForeColor = Color.Peru;
            }
            else
            {
                txt_MachineMode.Text =      "      Auto     ";
                txt_MachineMode.ForeColor = Color.Green;
            }
            //Display the Machine Status: Ready / No Ready
            if (!OSafetyPLC.DirectOut28[0])
            {
                txt_MachineStatus.Text =    "Esperando pieza";
                txt_MachineStatus.ForeColor = Color.Peru;
            }
            else
            {
                txt_MachineStatus.Text =    "      Listo    ";
                txt_MachineStatus.ForeColor = Color.Green;
            }
            #endregion

            #region Alarms and Events
            Alarms();
            Eventos();
            #endregion
        }
        //Status of the production process
        private void ProductionProcess_Tick(object sender, EventArgs e)
        {

        }
        //Startup the communications with the external devices
        private void btn_Initialize_Click(object sender, EventArgs e)
        {
            DialogResult Answer;
            Answer = MessageBox.Show("Do you want to start up the system?", "HMI", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (Answer == DialogResult.OK)
            {
                //Initialization Process
                if (InitializationSystem())
                {
                    //Controls
                    btn_Shutdown.Enabled = true;
                    btn_Initialize.Enabled = false;
                }
                else
                {
                    //Controls
                    btn_Shutdown.Enabled = false;
                    btn_Initialize.Enabled = true;
                }
            }
        }
        //Shutting down the communications with the external devices
        private void btn_Shutdown_Click(object sender, EventArgs e)
        {
            DialogResult Answer;
            Answer = MessageBox.Show("Do you want to shutdown the system?", "HMI", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (Answer == DialogResult.OK)
            {
                //Shutdown Process
                ShuttingDownSystem();
                //Controls
                btn_Initialize.Enabled = true;
                btn_Shutdown.Enabled = false;
            }

        }
        //Exit of the Application
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult Answer;
            Answer = MessageBox.Show("Do you want to close the application?", cSVfile[2], MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (Answer == DialogResult.OK)
            {
                ShuttingDownSystem();
                Application.Exit();
                Environment.Exit(Environment.ExitCode);
            }
        }
        //Servo Press: Home position 
        private void btn_Home_Click(object sender, EventArgs e)
        {
            DialogResult Answer;
            Answer = MessageBox.Show("Be careful, the servo press going to move", "PCON: Home Position", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (Answer == DialogResult.OK)
            {
                tskHomePos();
            }
        }

        #region Pages       
        //Production Page
        private void btn_Production_Click(object sender, EventArgs e)
        {
            ProdPag.ShowDialog();
        }
        //Lot manager Page
        private void btn_LotManager_Click(object sender, EventArgs e)
        {
            LotManagerPag.ShowDialog();
        }
        //Engineering Page
        private void btn_Engineering_Click(object sender, EventArgs e)
        {
            EngineeringPag.ShowDialog();
        }
        //Diagnostics Page
        private void btn_Diagnostics_Click(object sender, EventArgs e)
        {
            DiagnosticsPage.ShowDialog();
        }
        ///Users Access:
        ///Developer & Mechanization: user = Developer / pass = Employee No.
        ///Engineering: user = Engineering / pass = Employee No.
        ///Maintenance: user = Maintenance / pass = Employee No.
        ///Operation: Error to entry the password or Log out
        ///All users and password will be storaged into a list (UsersList.txt)
        private void btn_UserAccess_Click(object sender, EventArgs e)
        {
            UserControlPage.ShowDialog();
        }
        //Connection of the enable devices Page
        private void btn_ConnSettings_Click(object sender, EventArgs e)
        {
            ConnSettingsPage.ShowDialog();
        }
        #endregion

        //Save the Log of the Lot Production 
        private void txt_ProductionMsg_DoubleClick(object sender, EventArgs e)
        {
            //Variables
            Stream Content;
            //Objects
            MemoryStream LogData = new MemoryStream();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            txt_ProductionMsg.SaveFile(LogData, RichTextBoxStreamType.PlainText);

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Content = saveFileDialog1.OpenFile();
                LogData.Position = 0;
                LogData.WriteTo(Content);
                Content.Close();
                //Reset the content log 
                txt_ProductionMsg.Clear();
            }
        }
        //Save the Log of the System events
        private void txt_SystemMsg_DoubleClick(object sender, EventArgs e)
        {
            //Variables
            Stream Content;
            //Objects
            MemoryStream LogData = new MemoryStream();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            txt_SystemMsg.SaveFile(LogData, RichTextBoxStreamType.PlainText);

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Content = saveFileDialog1.OpenFile();
                LogData.Position = 0;
                LogData.WriteTo(Content);
                Content.Close();
            }
        }
        #endregion

        #region Information
        //System Messages
        public void SystemMessages(string text, string type)
        {
            if (txt_SystemMsg.InvokeRequired)
            {
                SystemMsgCallback d = new SystemMsgCallback(SystemMessages);
                this.Invoke(d, new object[] { text, type });
            }
            else
            {
                MachineMsg.Message(txt_SystemMsg, text, type);
            }
        }
        //Production Messages
        public void ProdMessages(string text, string type)
        {
            if (txt_ProductionMsg.InvokeRequired)
            {
                ProdMsgCallback d = new ProdMsgCallback(ProdMessages);
                this.Invoke(d, new object[] { text, type });
            }
            else
            {
                MachineMsg.Message(txt_ProductionMsg, text, type);
            }
        }

        ///Information about the Software installed in the PC 
        ///and the PLC programming software
        private void btn_HardwareInfo_Click(object sender, EventArgs e)
        {
            pnl_HardwareInfo HardwareInfopage = new pnl_HardwareInfo();
            HardwareInfopage.ShowDialog();
        }
        //Information about the Hardware installed in the machine
        private void btn_SoftwareInfo_Click(object sender, EventArgs e)
        {
            pnl_SoftwareInfo SoftwareInfopage = new pnl_SoftwareInfo();
            SoftwareInfopage.ShowDialog();
        }
        #endregion

        #region Functions
        //Read the CSV file (MachineConfig)
        public void ReadMachineConfigFile()
        {
            try
            {
                #region Machine config CSV file
                //Open the Production Data file to query the Device ID loaded
                String List;   
                int i = 0;
                //Query the file Log
                StreamReader _File = new StreamReader(pathProdData3);
                while ((List = _File.ReadLine()) != null)
                {
                    string[] Data  = List.Split(',');
                    //Configuration File
                    cSVfile[i] = Data[1];
                    i++;
                }
                _File.Close();
                #endregion
            }
            catch (Exception a)
            {
                //Message
                SystemMessages("Error to read the Machine Config CSV file\n", "Error");
                Console.WriteLine(a);
            }
        } 

        #region Initialization System
        public Boolean InitializationSystem()
        {
            bool bSuccess = false;
            int Error = 0;

            #region Initialization's Secuence for PLC, Safety and Instrumentation
            /*if (ConnSettingsPage.DeviceConnections[(int)Devices.PLC])
            {
                //PLC
                if (OPLC.Open_Communication())
                {
                    SystemMessages("PLC Connected\n", "System");
                }
                else
                {
                    Error++;
                }
            }*/

            if (ConnSettingsPage.DeviceConnections[(int)Devices.SafetyPLC])
            {
                //Safety PLC
                if (OSafetyPLC.Open_Communication())
                {
                    SystemMessages("Safety PLC Connected\n", "System");
                }
                else
                {
                    Error++;
                }
            }

            //Scanner OMRON V430-F
            if (ConnSettingsPage.DeviceConnections[(int)Devices.Scanner])
            {
                if (OScanner.Open_Communication())
                {
                    SystemMessages("Scanner Connected\n", "System");
                }
                else
                {
                    Error++;
                }
            }

            //Screwdriver Controller: MTFocus6K
            if (ConnSettingsPage.DeviceConnections[(int)Devices.Instrument1])
            {
                if (OMTFocus6K.Open_Communication())
                {
                    SystemMessages("MTFocus6K Connected\n", "System");
                }
                else
                {
                    Error++;
                }
            }

            //Vision Sensor: Keyence, IV2-G500MA
            if (ConnSettingsPage.DeviceConnections[(int)Devices.Instrument2])
            {
                if (OVisionSensor.Open_Communication())
                {
                    SystemMessages("IV2-G500MA Connected\n", "System");
                }
                else
                {
                    Error++;
                }
            }
            //Force & Displacement Module: Kistler, NC 5847
            if (ConnSettingsPage.DeviceConnections[(int)Devices.Kistler])
            {
                if (OKistler.Open_Communication())
                {
                    SystemMessages("Kistler Module Connected\n", "System");
                }
                else
                {
                    Error++;
                }
            }
            //Servmotor: IAI, PCON Controller
            if (ConnSettingsPage.DeviceConnections[(int)Devices.PCON])
            {
                if (OPCON.Open_Communication())
                {
                    SystemMessages("PCON Controller Connected\n", "System");
                }
                else
                {
                    Error++;
                }
            }

            #endregion

            #region Verification of the PLC, Safety and Instrumentation Connection
            if (Error == 0)
            {
                SystemMessages("Initialization process: OK\n", "OK");
                btn_Initialize.Enabled = false;
                btn_Shutdown.Enabled = true;
                //Production Process
                ProductionProcess.Enabled = true;
                //Machine Status
                MachineStatus.Enabled = true;
                lb_Station.Image = Image.FromFile("StationConnected.gif");
                lb_Station.ForeColor = Color.Green;
                bSuccess = true;
            }
            else
            {
                SystemMessages("Initialization process: Error\n", "Error");
                //Production Process
                ProductionProcess.Enabled = false;
                //Machine Status
                MachineStatus.Enabled = false;
                lb_Station.Image = Image.FromFile("StationDisconnected.gif");
                lb_Station.ForeColor = Color.Orange;
                bSuccess = false;

            }
            #endregion
            return bSuccess;
        }
        #endregion

        #region Shutting Down System
        public Boolean ShuttingDownSystem()
        {
            bool bSuccess = false;
            int Error = 0;

            #region Shutting down's Secuence for PLC, Safety and Instrumentation
            /*if (ConnSettingsPage.DeviceConnections[(int)Devices.PLC])
            {
                //PLC
                if (OPLC.Close_Communication())
                {
                    SystemMessages("PLC Disconnected\n", "System");
                }
                else
                {
                    Error++;
                }
            }*/
            
            if (ConnSettingsPage.DeviceConnections[(int)Devices.SafetyPLC])
            {
                //Safety PLC
                if (OSafetyPLC.Close_Communication())
                {
                    SystemMessages("Safety PLC Disconnected\n", "System");
                }
                else
                {
                    Error++;
                }
            }
            
            if (ConnSettingsPage.DeviceConnections[(int)Devices.Scanner])
            {
                //Scanner OMRON V430-F
                if (OScanner.Close_Communication())
                {
                    SystemMessages("Scanner Disconnected\n", "System");
                }
                else
                {
                    Error++;
                }
            }

            //Screwdriver Controller: MTFocus6K
            if (ConnSettingsPage.DeviceConnections[(int)Devices.Instrument1])
            {
                if (OMTFocus6K.Close_Communication())
                {
                    SystemMessages("MTFocus6K Disconnected\n", "System");
                }
                else
                {
                    Error++;
                }
            }

            //Vision Sensor: Keyence, IV2-G500MA
            if (ConnSettingsPage.DeviceConnections[(int)Devices.Instrument2])
            {
                if (OVisionSensor.Close_Communication())
                {
                    SystemMessages("IV2-G500MA Disconnected\n", "System");
                }
                else
                {
                    Error++;
                }
            }
            //Force & Displacement Module: Kistler, NC 5847
            if (ConnSettingsPage.DeviceConnections[(int)Devices.Kistler])
            {
                if (OKistler.Close_Communication())
                {
                    SystemMessages("Kistler Module Disconnected\n", "System");
                }
                else
                {
                    Error++;
                }
            }
            //Servmotor: IAI, PCON Controller
            if (ConnSettingsPage.DeviceConnections[(int)Devices.PCON])
            {
                if (OPCON.Close_Communication())
                {
                    SystemMessages("PCON Controller Disconnected\n", "System");
                }
                else
                {
                    Error++;
                }
            }
            #endregion

            #region Verification of the PLC, Safety and Instrumentation Disconnection
            if (Error == 0)
            {
                SystemMessages("Shutting down process: OK\n", "OK");
                btn_Initialize.Enabled = false;
                btn_Shutdown.Enabled = true;
                //Production Process
                ProductionProcess.Enabled = true;
                //Machine Status
                MachineStatus.Enabled = true;
                lb_Station.Image = Image.FromFile("StationDisconnected.gif");
                lb_Station.ForeColor = Color.Red;
                bSuccess = true;
            }
            else
            {
                SystemMessages("Shutting down process: Error\n", "Error");
                //Production Process
                ProductionProcess.Enabled = false;
                //Machine Status
                MachineStatus.Enabled = false;
                lb_Station.Image = Image.FromFile("StationDisconnected.gif");
                lb_Station.ForeColor = Color.Orange;
                bSuccess = false;

            }
            #endregion

            return bSuccess;
        }
        #endregion

        #region ANDON

        #endregion

        #region External Control     
        //Home Position
        private async Task tskHomePos()
        {
            await Task.Run(() => HomePos());
        }
        private void HomePos()
        {
            //Home OFF (Home return: A home-return command is issued when this signal turns ON.)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.HOME] = false;
            Thread.Sleep(500);
            //PCON: Servo ON
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.SON] = true;

            Thread.Sleep(500);

            //Home ON (Home return: A home-return command is issued when this signal turns ON.)
            OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.HOME] = true;
            Thread.Sleep(2000);

            int MAX_Timeout = 40;
            //Timeout for waiting the PLC response
            for (int i = 0; i < MAX_Timeout; i++)
            {
                if (!OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.MOVE])
                {
                    //Home ON (Home return: A home-return command is issued when this signal turns ON.)
                    OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.HOME] = false;
                    Thread.Sleep(500);
                    //PCON: Servo ON
                    OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.SON] = false;
                    i = MAX_Timeout;
                }
                Thread.Sleep(800);
            }
        }

        //PLC or Safety PLC: FB Reset
        public void Reset()
        {
            if (btn_Reset.Checked & !OOneShot.Reset)
            {
                OOneShot.Reset = true;
                //Clear the Alarms & Events Table
                OTableAE.Clear();
                for (int i = 0; i < 32; i++)
                {
                    //PLC
                    if(i< OPLC.AlarmsMFLLock.Length) OPLC.AlarmsMFLLock[i] = false;
                    if (i < OPLC.AlarmsMFRLock.Length) OPLC.AlarmsMFRLock[i] = false;
                    if (i < OPLC.PressLock.Length) OPLC.PressLock[i] = false;
                    if (i < OPLC.MarkerArmLock.Length) OPLC.MarkerArmLock[i] = false;
                    if (i < OPLC.MarkerLock.Length) OPLC.MarkerLock[i] = false;
                    if (i < OPLC.SafetyLock.Length) OPLC.SafetyLock[i] = false;
                    //Safety PLC
                    if (i < OSafetyPLC.PressLock.Length) OSafetyPLC.PressLock[i] = false;
                    if (i < OSafetyPLC.LockerLock.Length) OSafetyPLC.LockerLock[i] = false;
                    if (i < OSafetyPLC.SafetyLock.Length) OSafetyPLC.SafetyLock[i] = false;
                    //Lot Manager
                    if (i < LotManager.OLotManager.LotManager_Lock.Length) LotManager.OLotManager.LotManager_Lock[i] = false;
                }
            }
            if (!btn_Reset.Checked & OOneShot.Reset)
            {
                OOneShot.Reset = false;
            }
            //PLC
            //OPLC.K4M132_Array[1] = OOneShot.Reset;
            //Safety PLC
            OSafetyPLC.S1B0_1[14] = OOneShot.Reset;
        }
       
        //PLC or Safety PLC: FB Manual/Auto
        public void ManualAuto()
        {
            //Automatic Mode
            if (btn_ManualAuto.Checked)
            {
                btn_ManualAuto.Text = "MANUAL";
                btn_ManualAuto.ForeColor = Color.DarkOrange;
                btn_ManualAuto.Image = Image.FromFile("ManualMode.png");
                //Home function Disable
                btn_Home.Visible = false;
                //Reset function Disable
                btn_Reset.Visible = false;
                btn_Reset.Checked = false;
                //Manual mode activated
                machineMode = true;
                OOneShot.ManualAuto = true;
            }
            //Manual Mode
            else
            {
                btn_ManualAuto.Text = "AUTO";
                btn_ManualAuto.ForeColor = Color.Green;
                btn_ManualAuto.Image = Image.FromFile("AutoMode.png");
                //Home function Enable
                btn_Home.Visible = true;             
                //Reset function Enable
                btn_Reset.Visible = true;              
                //Auto mode activated
                machineMode = false;
                OOneShot.ManualAuto =  false;
                //Stop process button
                btn_Stop.Checked = false;
                //Stop Process button
                btn_Stop.Visible = false;
                //Safety PLC:  PC_Process1_Finished Safe to remove the part as safety way
                OSafetyPLC.S1B2_3[0] = true;
            }
            //PLC
            //OPLC.K4M132_Array[2] = OOneShot.ManualAuto;
            //Safety PLC
            OSafetyPLC.S1B0_1[15] = OOneShot.ManualAuto;

            //Check the Status of the process, to prevent turns manual mode during the process execution
            if (OProcess.Status[(int)ProcessNumber.Process1] && machineMode)
            {
                //Auto/Manual button
                btn_ManualAuto.Enabled = false;
            }

            if (!OProcess.Status[(int)ProcessNumber.Process1] && machineMode)
            {
                //Auto/Manual button
                btn_ManualAuto.Enabled = true;
            }

            //If the emergency stop was pressed or the Lot isn't loaded, the machine turns Manual mode
            if (OSafetyPLC.DirectOut28[3] || LotManager.OLotManager.LotManager_AE[(int)LotManager_parameters.LotActive])
            {
                btn_ManualAuto.Enabled = false;
                btn_ManualAuto.Checked = false;
            }
            else
            {
                btn_ManualAuto.Enabled = true;
            }
        }
        //PLC or Safety PLC: FB Start Process 1
        public void StartProcess1()
        {
            //PC_Process 1_Start
            if (OSafetyPLC.DirectOut28[1] && !OProcess.Status[(int)ProcessNumber.Process1] && machineMode && !btn_Stop.Checked)
            {
                OProcess.InitProcess1();
                //Message: if the Scanner couldn't read, check the label 
                txt_MsgCheckScanner.Visible = false;
                //Message: Machine failure 
                txt_MsgTryAgain.Visible = false;
                //Message: Inspection failure 
                txt_StopProcess.Visible = false;
            }
        }
        //PLC or Safety PLC: FB Stop Process 1
        public void StopProcess1()
        {
            //Message: Machine failure 
            txt_StopProcess.Visible = btn_Stop.Checked;

            if (machineMode)
            {
                //PC_Process 1_Stop
                OProcess.StopRequest = btn_Stop.Checked;
                btn_Stop.Visible = true;
            }
            else
            {
                //PC_Process 1_Stop
                btn_Stop.Checked = false;
                btn_Stop.Visible = false;
            }
        }
        #endregion

        #region Alarms & Events
        public void Alarms()
        {
            int j = 0;
            int k = 0;

            #region Press Alarms
            
            for (int i = 0; i < 3; i++)
            {
                /*
                if (OSafetyPLC.DirectOut30[i])
                {
                    if (!OSafetyPLC.PressLock[i])
                    {
                        string Alarm = OMsgAE.PressAlarms[j];
                        OTableAE.Rows.Add(DateTime.Now.ToString(), "Press", "Alarm: " + Alarm);
                        OSafetyPLC.PressLock[i] = true;
                    }
                }
                else
                {
                    OSafetyPLC.PressLock[i] = false;
                }
                */
                j++;
            }
            
            #endregion

            #region Locker Alarms
            //PLC Alarms
            for (int i = 3; i < 5; i++)
            {
                if (OSafetyPLC.DirectOut30[i])
                {
                    if (!OSafetyPLC.LockerLock[i])
                    {
                        string Alarm = OMsgAE.LockerAlarms[j-3];
                        OTableAE.Rows.Add(DateTime.Now.ToString(), "Locker", "Alarm: " + Alarm);
                        OSafetyPLC.LockerLock[i] = true;
                    }
                }
                else
                {
                    OSafetyPLC.LockerLock[i] = false;
                }
                j++;
            }
            #endregion

            #region Safety Alarms
            //PLC Alarms
            for (int i = 8; i < 16; i++)
            {
                if (OSafetyPLC.DirectOut30[i])
                {
                    if (!OSafetyPLC.SafetyLock[i])
                    {
                        k = i - 8;
                        string Alarm = OMsgAE.SafetyAlarms[k];
                        OTableAE.Rows.Add(DateTime.Now.ToString(), "Safety", "Alarm: " + Alarm);
                        OSafetyPLC.SafetyLock[i] = true;
                    }
                }
                else
                {
                    OSafetyPLC.SafetyLock[i] = false;
                }
                k++;
            }
            #endregion

            #region Lot Manager 
            //System Alarms
            for (int i = 0; i < LotManager.OLotManager.LotManager_AE.Length; i++)
            {
                if (LotManager.OLotManager.LotManager_AE[i])
                {
                    if (!LotManager.OLotManager.LotManager_Lock[i])
                    {
                        k = i;
                        string Alarm = OMsgAE.LotManagerAlarms[k];
                        OTableAE.Rows.Add(DateTime.Now.ToString(), "Lot Manager", "Event: " + Alarm);
                        LotManager.OLotManager.LotManager_Lock[i] = true;
                    }
                }
                else
                {
                    LotManager.OLotManager.LotManager_Lock[i] = false;
                }
                k++;
            }
            #endregion
        }
        public void Eventos()
        {
            if (OTableAE.Rows.Count > 0)
            {
                img_AlarmsEvents.Visible = true;
            }
            else
            {
                img_AlarmsEvents.Visible = false;
            }
        }
        #endregion

        #endregion

        #region Threads

        #endregion

    }
}
