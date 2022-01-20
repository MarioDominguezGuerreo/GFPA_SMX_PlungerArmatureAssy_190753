//Mario A. Dominguez Guerrero 
//Version 1.6 - July/2020
//Version 2.0 - January/2022

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
using System.Threading;
using System.IO;
#endregion

#region Project Libraries
using PCdatetime;
using PCMessages;
using Keyence.IV2.Sdk;
#endregion

namespace Software
{
    public partial class Diagnostics : Form
    {
        #region Variables
        public string pathProdData3 = "MachineData/IOLabels.csv";
        //CSV File machine settings
        #region CSV File
        //Machine config file 1 - 16
        private const int MAX_CSVfields = 100;
        private static string[] cSVfile = new string[MAX_CSVfields] {  "none","none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none",
                                                                       "none","none","none","none","none","none"};
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
        //Array for CHeck boxes
        CheckBox[] IODef = new CheckBox[MAX_CSVfields];

        #endregion

        #region Callbacks
        //Display the Torque and Angle results
        delegate void TorqueAngleDataCallback(int TorqueAngleTotalSamples);
        //IV2-G500MA: Image (thread safe calls)
        delegate void IV2G500MAImgCallback();
        #endregion

        #region Threads

        #endregion

        #region Objects
        //Diagnostics page
        public static Diagnostics ODiagnostics;
        //Messages
        Messages MachineMsg = new Messages();

        //PLC
        PLC OPLC = new PLC();
        //Safety Controller
        SafetyPLC OSafetyPLC = new SafetyPLC();
        //SQL Server
        SQLDatabase OSQLServer = new SQLDatabase();
        //Scanner OMRON V430-F
        Scanner OScanner = new Scanner();
        //Screwdriver Controller: MTFocus 6K
        MTFocus6K OMTFocus6K = new MTFocus6K();
        //Vision Sensor: Keyence, IV2-G500MA
        IV2G500MA OVisionSensor = new IV2G500MA();
        //Kistler: Force and Displacement Instrument
        Kistler_5847B0 OKistler = new Kistler_5847B0();
        //PCON-CB: Servomotor controller
        IAI_PCON OPCON = new IAI_PCON();

        //Reports
        Reports OReports = new Reports();
        //Torque - Angle Results
        DataTable TableResults = new DataTable();
        
        #endregion

        /*Initialization of the Diagnostics form*/
        public Diagnostics()
        {
            InitializeComponent();
            //Diagnostics page
            ODiagnostics = this;
            //Machine Status
            MachineStatus.Enabled = true;
            //Read the IO labels CSV File
            ReadMachineConfigFile();
            //Assign Check box enables to IO definitions Array
            AssignCheckboxToIOLabels();
            //Set the text to Check box text from IO definitions array
            AssignIOLabels();

            #region Torque-Angle Table Results
            //Columns of the table
            TableResults.Columns.Add("Sample", typeof(string));
            TableResults.Columns.Add("Angle", typeof(string));
            TableResults.Columns.Add("Torque", typeof(string));

            //Bind the Table with the Data grid
            dataGridView1.DataSource = TableResults;
            #endregion
        }
        private void Diagnostics_Load(object sender, EventArgs e)
        {
            //PLC IP info
            txt_PLCIP.Text = OPLC.IP;
            //Safety PLC info
            txt_SafetyPLC.Text = OSafetyPLC.IP;
            //Scanner: OMRON V430-F
            txt_Scanner.Text = OScanner.IP;
            //Instrument 1: MTFocus 6K
            txt_InstruIP.Text = OMTFocus6K.IP;
            //Instrument 2: IV2-G500MA
            txt_Instru2IP.Text = OVisionSensor.IP;
            //Instrument 3: Kistler Module NC
            txt_KistlerIP.Text = OKistler.IP;
            //Instrument 4: IAI PCON Servo controller
            txt_PCONIP.Text = OPCON.IP;
            //CSV file for Store Measurements
            txt_LimitsCSV.Text = OSQLServer.pathLimitsCSVData;
            txt_StoreMeasurementsCSV.Text = OReports.PathProdData4;
            txt_StoreResultsCSV.Text = OReports.PathProdData5;
            //Info about SQL Server, Database and tables
            txt_SQLServer.Text = OSQLServer.Server;
            txt_Database.Text = OSQLServer.DataBase;
            txt_StationName.Text = OSQLServer.Station;
            txt_StationID.Text = OSQLServer.StationID.ToString();
            txt_EquipID.Text = OSQLServer.EquipmentID.ToString();
            //Prior Op Check & Update device status
            txt_PriorOpCheck.Text = OSQLServer.TblPriorOpCheck;
            txt_UpdateDeviceStatus.Text = OSQLServer.TblUpdateDeviceStatus;
            //Limits
            txt_LimitsDB.Text = OSQLServer.TblLimits;
            //Results History
            txt_StoreResultsDB.Text = OSQLServer.TblResultsHistory;
            //Masters parts sequence
            txt_MasterPartsSeq.Text = OSQLServer.TblMastersPartsSeqView;
            //Components Traceability
            txt_ChkComp.Text = OSQLServer.TblCheckComponents;
            txt_AssignComp.Text = OSQLServer.TblAssignComponentsToSerial;
        }
        //Winform View Close
        private void Diagnostics_FormClosed(object sender, EventArgs e)
        {
            //Control for the PLC's Outputs as auto mode
            btn_PLCManualMode.Checked = false;
            //Control for the Safety PLC's Outputs as auto mode
            btn_SafetyManualMod.Checked = false;
            //Control for the Scanner's Control as remote mode
            btn_ScanTrigRemote.Checked = true;
            //Control for the Instrument's Control as remote mode 
            btn_InstrumentRemote.Checked = true;
        }

        #region Controls
        //Machine Status
        private void MachineStatus_Tick(object sender, EventArgs e)
        {

            #region PLC

            #region Connection Status
            //Connection status
            if (OPLC.Parameters[(int)PLC_Comm.CommStatus])
            {
                txt_PLCConnStatus.Text = "Connected";
                txt_PLCConnStatus.ForeColor = Color.Green;
            }
            else
            {
                txt_PLCConnStatus.Text = "Disconnected";
                txt_PLCConnStatus.ForeColor = Color.Black;
            }
                
            #endregion

            #region Inputs

            #region Slot 0 (K2M0 - K2M16)
            for (int i = 0; i < 8; i++)
            {
                IODef[i].Checked = OPLC.K2M0_Array[i];
            }
            for (int i = 8; i < 16; i++)
            {
                int j = i - 8;
                IODef[i].Checked = OPLC.K2M8_Array[j];
            }
            #endregion

            #region Slot 2 (K2M48 - K2M63)
            for (int i = 32; i < 40; i++)
            {
                int j = i - 32;
                IODef[i].Checked = OPLC.K2M48_Array[j];
            }
            for (int i = 40; i < 48; i++)
            {
                int j = i - 40;
                IODef[i].Checked = OPLC.K2M56_Array[j];
            }
            #endregion

            #endregion

            #region Outputs   
            //System Mode: Permission to enable the Outputs in manual mode
            //If any Manual/Auto physical button or the Manual/Auto software button is activated, as default the mode will be Manual
            if (!OPLC.K2M0_Array[3] && !HMI.OForm.MachineMode) //Manual Mode
            {
                //Enable to change the PLC mode to Manual (Write the outputs)
                btn_PLCManualMode.Enabled = true;
                //Enable to change the SafetyPLC mode to Manual (Write the outputs)
                btn_SafetyManualMod.Enabled = true;
            }
            else //Automatic Mode
            {
                //Disable to change the PLC mode to Manual (Write the outputs)
                btn_PLCManualMode.Enabled = false;
                //Disable to change the SafetyPLC mode to Manual (Write the outputs)
                btn_SafetyManualMod.Enabled = false;
            }
            // Enable PLC Manual mode over the Outputs
            OPLC.K4M132_Array[3] = btn_PLCManualMode.Checked;
            OPLC.Parameters[(int)PLC_Comm.RW_Coils] = OPLC.K4M132_Array[3];

            if (btn_PLCManualMode.Checked)
            {
                pnl_PLCOutputs.Enabled = true;
                OPLC.Parameters[(int)PLC_Comm.RW_Coils] = true;
           
                #region Slot 1 (K2M32 - K2M47)
                for (int i = 16; i < 24; i++)
                {
                    int j = i - 16;
                    OPLC.K2M32_Array[j] = IODef[i].Checked;
                }
                for (int i = 24; i < 32; i++)
                {
                    int j = i - 24;
                    OPLC.K2M40_Array[j] = IODef[i].Checked;
                }
                #endregion

                #region Slot 3 (K2M80 - K2M95)
                for (int i = 48; i < 56; i++)
                {
                    int j = i - 48;
                    OPLC.K2M64_Array[j] = IODef[i].Checked;
                }
                for (int i = 56; i < 64; i++)
                {
                    int j = i - 56;
                    OPLC.K2M72_Array[j] = IODef[i].Checked;
                }
                #endregion
            }
            else
            {
                pnl_PLCOutputs.Enabled = false;
                OPLC.Parameters[(int)PLC_Comm.RW_Coils] = false;

                #region Slot 1 (K2M16 - K2M31)
                for (int i = 16; i < 24; i++)
                {
                    int j = i - 16;
                    IODef[i].Checked = OPLC.K2M16_Array[j];
                }
                for (int i = 24; i < 32; i++)
                {
                    int j = i - 24;
                    IODef[i].Checked = OPLC.K2M24_Array[j];
                }
                #endregion

                #region Slot 3 (K2M64 - K2M79)
                for (int i = 48; i < 56; i++)
                {
                    int j = i - 48;
                    IODef[i].Checked = OPLC.K2M64_Array[j];
                }
                for (int i = 56; i < 64; i++)
                {
                    int j = i - 56;
                    IODef[i].Checked = OPLC.K2M72_Array[j];
                }
                #endregion
            }
            #endregion

            #endregion

            #region Safety PLC
            //Connection Status
            if (OSafetyPLC.Parameters[(int)PLC_Comm.CommStatus])
            {
                txt_SafetyConnStatus.Text = "Connected";
                txt_SafetyConnStatus.ForeColor = Color.Green;
            }
            else
            {
                txt_SafetyConnStatus.Text = "Disconnected";
                txt_SafetyConnStatus.ForeColor = Color.Black;
            }               

            #region Inputs
            //XTIO Module 0
            for (int i = 64; i < 72; i++)
            {
                int j = i - 64;
                IODef[i].Checked = OSafetyPLC.Inputs[j];
            }
            //XTIO Module 1
            for (int i = 76; i < 84; i++)
            {
                int j = (i - 76) + 8;
                IODef[i].Checked = OSafetyPLC.Inputs[j];
            }
            //XTIO Module 3
            for (int i = 88; i < 96; i++)
            {
                int j = (i - 88);
                IODef[i].Checked = OSafetyPLC.Inputs2[j];
            }
            #endregion

            #region Outputs
            // Enable Safety Controller Manual mode over the Outputs
            if (btn_SafetyManualMod.Checked)
            {
                pnl_SafetyControllerOutputs.Enabled = true;
                OSafetyPLC.Parameters[(int)PLC_Comm.RW_Coils] = true;

                //Force Outputs
                OSafetyPLC.S1B0_1[12] = true;

                #region Outputs
                //S1.B0 (0-3)
                for (int i = 72; i < 76; i++)
                {
                    int j = i - 72;
                    OSafetyPLC.S1B0_1[j] = IODef[i].Checked;
                }
                //S1.B0 (4-7)
                for (int i = 84; i < 88; i++)
                {
                    int j = (i - 84) + 4;
                    OSafetyPLC.S1B0_1[j] = IODef[i].Checked;
                }
                //S1.B1 (0-3)
                for (int i = 96; i < 100; i++)
                {
                    int j = (i - 96) + 8;
                    OSafetyPLC.S1B0_1[j] = IODef[i].Checked;
                }
                #endregion
            }
            else
            {
                pnl_SafetyControllerOutputs.Enabled = false;
                OSafetyPLC.Parameters[(int)PLC_Comm.RW_Coils] = false;

                //Force Outputs
                OSafetyPLC.S1B0_1[12] = false;

                #region Outputs
                //XTIO Module 1
                for (int i = 72; i < 76; i++)
                {
                    int j = i - 72;
                    IODef[i].Checked = OSafetyPLC.Outputs[j];
                }
                //XTIO Module 2
                for (int i = 84; i < 88; i++)
                {
                    int j = (i - 84) + 8;
                    IODef[i].Checked = OSafetyPLC.Outputs[j];
                }
                //XTIO Module 3
                for (int i = 96; i < 100; i++)
                {
                    int j = (i - 96);
                    IODef[i].Checked = OSafetyPLC.Outputs2[j];
                }
                #endregion
            }
            #endregion

            #endregion

            #region Scanner

            #region Connection Status
            if (OScanner.Parameters[(int)Scanner_Comm.CommStatus])
            {
                txt_ScannerConnStatus.Text = "Connected";
                txt_ScannerConnStatus.ForeColor = Color.Green;
            }
            else
            {
                txt_ScannerConnStatus.Text = "Disconnected";
                txt_ScannerConnStatus.ForeColor = Color.Black;
            }    
            #endregion

            //If any Manual/Auto physical button or the Manual/Auto software button is activated, as default the mode will be Manual
            #region Control Mode
            if (OScanner.Parameters[(int)Scanner_Comm.CommStatus] && !HMI.OForm.MachineMode)
            {
                //Trigger control is enable to modify (Remote or Manual)
                pnl_ScannerControl.Enabled = true;
                //Remote and Manual selection
                if (btn_ScanTrigRemote.Checked)
                {
                    //Manual trig is disable
                    btn_ScanTrigMan.Visible = false;
                }
                else
                {
                    //Manual trig is enable
                    btn_ScanTrigMan.Visible = true;
                }
            }
            else
            {
                //Trigger control is enable to modify (Remote or Manual)
                pnl_ScannerControl.Enabled = false;
            }
            #endregion

            //Scanner Values
            txt_ScannerMsg.Text = OScanner.Values_Msg;
            txt_ScanReadData.Text = OScanner.Value_Info;
            txt_2DCodeQuality.Text = OScanner.Value_Quality;
            #endregion

            #region Instruments

            #region Instrument 1: MTFocus 6K

            #region Connection Status
            //Connection status
            if (OMTFocus6K.Parameters[(int)Instrument_Comm.CommStatus])
            {
                txt_InstruConnStatus.Text = "Connected";
                txt_InstruConnStatus.ForeColor = Color.Green;
            }
            else
            {
                txt_InstruConnStatus.Text = "Disconnected";
                txt_InstruConnStatus.ForeColor = Color.Black;
            }     
            #endregion

            #region Control Mode
            if (OMTFocus6K.Parameters[(int)Instrument_Comm.CommStatus] && !HMI.OForm.MachineMode)
            {
                //Control is enable to modify (Remote or Manual)
                pnl_InstrumentControl.Enabled = true;
                //Remote and Manual selection
                if (btn_InstrumentRemote.Checked)
                {
                    //Get the measurement is disable
                    btn_ActiveBatch.Visible = false;
                }
                else
                {
                    //Get the measurement is enable
                    btn_ActiveBatch.Visible = true;
                }
            }
            else
            {
                //Control is enable to modify (Remote or Manual)
                pnl_InstrumentControl.Enabled = false;
            }
            #endregion

            #region Results
            //Part = OK or NG
            //Assessment Result
            if (OMTFocus6K.DataField_MID1202[2, 5] == "1")
            {
                txt_ResultStatus.Text = "OK";
                pnl_OKNG.BackColor = Color.LightGreen;
            }
            else
            {
                txt_ResultStatus.Text = "NG";
                pnl_OKNG.BackColor = Color.FromArgb(255, 192, 192);
            }
            //Controller Name
            txt_ControllerName.Text = OMTFocus6K.DataField_MID1202[8, 5];
            //Program Loaded
            txt_ProgName.Text = OMTFocus6K.DataField_MID1202[16, 5];
            //Batch Loaded
            txt_BatchName.Text = OMTFocus6K.DataField_MID1202[21, 5];
            //Total Torque
            txt_TotalTorque.Text = OMTFocus6K.DataField_MID1202[31, 5];
            //Total Angle
            txt_TotalAngle.Text = OMTFocus6K.DataField_MID1202[32, 5];

            #region MT Focus 6K: Step 2
            //Clamp Torque
            txt_ClampTorque.Text = OMTFocus6K.Clamp;
            //Clamp Angle
            txt_ClampAngle.Text = OMTFocus6K.ClampAngle;
            //Seating Point Torque
            txt_SeatingPoint.Text = OMTFocus6K.SeatingPoint;
            #endregion

            #endregion

            //Limits
            txt_TorqueMin.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.TorqueMin];
            txt_TorqueMax.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.TorqueMax];
            txt_AngleMin.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.AngleMin];
            txt_AngleMax.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.AngleMax];
            //Clamp and Seating Point 
            txt_ClampMin.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.ClampMin];
            txt_ClampMax.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.ClampMax];
            txt_ClampAngleMin.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.ClampAngleMin];
            txt_ClampAngleMax.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.ClampAngleMax];
            txt_SPMin.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.SeatingPointMin];
            txt_SPMax.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.SeatingPointMax];
            #endregion

            #region Instrument 2: IV2-G500MA

            #region Connection Status
            //Connection status
            if (OVisionSensor.Parameters[(int)Instrument_Comm.CommStatus])
            {
                txt_Instru2ConnStatus.Text = "Connected";
                txt_Instru2ConnStatus.ForeColor = Color.Green;
            }
            else
            {
                txt_Instru2ConnStatus.Text = "Disconnected";
                txt_Instru2ConnStatus.ForeColor = Color.Black;
            }
            #endregion

            #region Control Mode
            if (OVisionSensor.Parameters[(int)Instrument_Comm.CommStatus] && !HMI.OForm.MachineMode)
            {
                //Trigger control is enable to modify (Remote or Manual)
                pnl_VisionSensorControl.Enabled = true;
                //Remote and Manual selection
                if (btn_VisionSensorTrigRemote.Checked)
                {
                    //Manual trig is disable
                    btn_VisionSensorTrigMan.Visible = false;
                    btn_ChangeProg.Visible = false;
                    btn_GetVisionProgList.Visible = false;
                    cbx_ProgramList.Enabled = false;
                }
                else
                {
                    //Manual trig is enable
                    btn_VisionSensorTrigMan.Visible = true;
                    btn_ChangeProg.Visible = true;
                    btn_GetVisionProgList.Visible = true;
                    cbx_ProgramList.Enabled = true;
                }
            }
            else
            {
                //Trigger control is enable to modify (Remote or Manual)
                pnl_VisionSensorControl.Enabled = false;
            }
            #endregion

            #region Data

            #region IV2-G500MA: Get Sensor Info 
            txt_ModelName.Text = OVisionSensor._SensorInfo.ModelName;
            txt_DeviceName.Text = OVisionSensor._SensorInfo.DeviceName;
            txt_SerialName.Text = OVisionSensor._SensorInfo.SerialNumber;
            txt_Version.Text = OVisionSensor._SensorInfo.Version;
            #endregion

            #region IV2-G500MA: Program Info
            txt_VisionProgNumber.Text = OVisionSensor._SensorProgSettings.ProgramName;
            txt_ProgNumber.Text = OVisionSensor._SensorProgSettings.ProgramNumber.ToString();
            txt_ExtTrigger.Text = OVisionSensor._SensorProgSettings.ExternalTrigger.ToString();
            txt_TrigCycle.Text = OVisionSensor._SensorProgSettings.TriggerCycle.ToString();
            #endregion

            #region IV2-G500MA: Results
            txt_InspResult.Text = OVisionSensor.TextResult;
            img_IV2G500MA.Image = OVisionSensor.Image;
            img_IV2G500MA_Master.Image = OVisionSensor.Image_Master;
            if (OVisionSensor.TextResult == "OK")
            {
                txt_InspResult.ForeColor = Color.Green;
            }
            else
                txt_InspResult.ForeColor = Color.Red;
            #endregion

            #endregion

            //Limits
            txt_SpringVisionProg.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.SpringVisionProg];
            txt_ArmatureVisionProg.Text = OSQLServer.Instrument1_Limits[(int)Instrument_1.ArmatureVisionProg];
            #endregion

            #region Instrument 3: Kistler Module NC

            #region Connection
            //Connection status
            if (OKistler.Parameters[(int)Instrument_Comm.CommStatus])
            {
                txt_KistlerConnStatus.Text = "Connected";
                txt_KistlerConnStatus.ForeColor = Color.Green;
            }
            else
            {
                txt_KistlerConnStatus.Text = "Disconnected";
                txt_KistlerConnStatus.ForeColor = Color.Black;
            }
            #endregion

            #region Control bits

            #endregion

            #region Status bits
            //Kistler Ready to measure
            cbx_Kistler_Ready.Checked = OKistler.Outputs_47[16];
            //Measurement was OK or NG
            cbx_Kistler_OKNG.Checked = OKistler.Outputs_47[19];
            //Warning
            cbx_Kistler_Warning.Checked = OKistler.Outputs_47[22];
            //Alarm
            cbx_Kistler_Alarm.Checked = OKistler.Outputs_47[23];
            //Transmission failure
            cbx_Kistler_TxFault.Checked = OKistler.Outputs_811[8];
            //Device available
            cbx_Kistler_DevAvailable.Checked = OKistler.Outputs_811[11];
            #endregion

            #region Data
            //Current Measurements of each channel (X,Y)
            txt_MesureChX.Text = OKistler.ChannelX_Value.ToString();
            txt_MesureChY.Text = OKistler.ChannelY_Value.ToString();
            //Curves Measurements of each channel (X,Y)
            txt_MeasCurveXmin.Text = OKistler.CurveXMin_Value.ToString();
            txt_MeasCurveXmax.Text = OKistler.CurveXMax_Value.ToString();
            txt_MeasCurveYmin.Text = OKistler.CurveYMin_Value.ToString();
            txt_MeasCurveYmax.Text = OKistler.CurveYMax_Value.ToString();
            //Constant K
            txt_ConstantK.Text = OKistler.ConstantK.ToString("000.000");
            #endregion

            #region Limits
            //Test 1

            //Test 2

            //Constant K

            #endregion

            #endregion

            #region Instrument 4: IAI PCON Servo controller

            #region Connection
            //Connection status
            if (OPCON.Parameters[(int)Instrument_Comm.CommStatus])
            {
                txt_PCONConnStatus.Text = "Connected";
                txt_PCONConnStatus.ForeColor = Color.Green;
            }
            else
            {
                txt_PCONConnStatus.Text = "Disconnected";
                txt_PCONConnStatus.ForeColor = Color.Black;
            }
            #endregion

            #region Status Signal (PCON -> PC)
            cbx_PCON_PEND.Checked       = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.PEND];
            cbx_PCON_HEND.Checked       = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.HEND];
            cbx_PCON_Move.Checked       = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.MOVE];
            cbx_PCON_Alarm.Checked      = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.ALM];
            cbx_PCON_SV.Checked         = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.SV];
            cbx_PCON_PSEL.Checked       = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.PSEL];
            cbx_PCON_Disable.Checked    = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.disable_6];
            cbx_PCON_Disable2.Checked   = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.disable_7];
            cbx_PCON_RDMS.Checked       = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.RMDS];
            cbx_PCON_Disable3.Checked   = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.disable_9];
            cbx_PCON_Disable4.Checked   = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.disable_10];
            cbx_PCON_Disable5.Checked   = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.disable_11];
            cbx_PCON_Zone1.Checked      = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.ZONE1];
            cbx_PCON_Zone2.Checked      = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.ZONE2];
            cbx_PCON_Power.Checked      = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.PWR];
            cbx_PCON_EStop.Checked      = OPCON.StatuSignal_Out[(int)PCON_Reg_To_PC.EMGS];
            #endregion

            #region Data (PCON -> PC)
            txt_PCON_CurrPos.Text       = OPCON.CurrentPosition_value.ToString("000.000");
            txt_PCON_Current.Text       = OPCON.CmdCurrent_value.ToString("000.000");
            txt_PCON_CurrSpeed.Text     = OPCON.CurrSpeed_value.ToString("000.000");
            txt_PCON_AlarmCode.Text     = OPCON.AlarmCode_value.ToString("000.000");
            #endregion

            #region Control bits (PC -> PCON)
            if (!btn_PCON_Remote.Checked)
            {
                pnl_PCON_Control.Enabled = true;

                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR]        = btn_PCON_PosCMD.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.HOME]        = btn_PCON_Home.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.STP]         = cbx_PCON_STP.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.RES]         = cbx_PCON_RES.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.SON]         = btn_PCON_ServoON.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.JISL]        = cbx_PCON_JISL.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.JVEL]        = cbx_PCON_JVEL.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.JOG_REV]     = cbx_PCON_JogDown.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.JOG_FWD]     = cbx_PCON_JogUP.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.disable_9]   = cbx_PCON_Disable6.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.disable_10]  = cbx_PCON_Disable7.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.disable_11]  = cbx_PCON_Disable8.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.PUSH]        = cbx_PCON_Push.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DIR]         = cbx_PCON_Dir.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.RMOD]        = cbx_PCON_RMOD.Checked;
                OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.BKRL]        = cbx_PCON_BKRL.Checked;

                //Control buttons
                btn_PCON_Home.Visible = true;
                btn_PCON_PosCMD.Visible = true;
                btn_PCON_ServoON.Visible = true;
                btn_PCON_Ready.Visible = true;
                btn_PCON_Standby.Visible = true;
                btn_PCON_Test1.Visible = true;
                btn_PCON_Test2.Visible = true;
                btn_Screwdriving.Visible = true;
            }
            else
            {
                pnl_PCON_Control.Enabled = false;

                cbx_PCON_DSTR.Checked       = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DSTR];
                cbx_PCON_Home.Checked       = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.HOME];
                cbx_PCON_STP.Checked        = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.STP];
                cbx_PCON_RES.Checked        = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.RES];
                cbx_PCON_SON.Checked        = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.SON];
                cbx_PCON_JISL.Checked       = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.JISL];
                cbx_PCON_JVEL.Checked       = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.JVEL];
                cbx_PCON_JogDown.Checked    = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.JOG_REV];
                cbx_PCON_JogUP.Checked      = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.JOG_FWD];
                cbx_PCON_Disable6.Checked   = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.disable_9];
                cbx_PCON_Disable7.Checked   = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.disable_10];
                cbx_PCON_Disable8.Checked   = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.disable_11];
                cbx_PCON_Push.Checked       = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.PUSH];
                cbx_PCON_Dir.Checked        = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.DIR];
                cbx_PCON_RMOD.Checked       = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.RMOD];
                cbx_PCON_BKRL.Checked       = OPCON.ControlSignal_In[(int)PC_To_PCON_Reg.BKRL];

                //Control buttons
                btn_PCON_Home.Visible = false;
                btn_PCON_PosCMD.Visible = false;
                btn_PCON_ServoON.Visible = false;
                btn_PCON_Ready.Visible = false;
                btn_PCON_Standby.Visible = false;
                btn_PCON_Test1.Visible = false;
                btn_PCON_Test2.Visible = false;
                btn_Screwdriving.Visible = false;
            }
            #endregion

            #region Data (PC -> PCON)
            txt_PCON_TargPos.Text           = OPCON.TargetPosition_value;
            txt_PCON_PosBand.Text           = OPCON.PositionBand_value;
            txt_PCON_TargSpeed.Text         = OPCON.Speed_value;
            txt_PCON_AccDecc.Text           = OPCON.AccDecc_value;
            txt_PCON_PressCurrLimit.Text    = OPCON.PressCurrLimit_value;
            #endregion

            #region Limits

            #endregion

            #endregion

            #endregion

            #region SQL Server
            txt_qStatus.Text = OSQLServer.PriorOpChk_Return.ToString();
            txt_qMessages.Text = OSQLServer.POChkMsg;
            txt_qLastStationMsg.Text = OSQLServer.POChkMsg2;
            txt_qPartStatusMsg.Text = OSQLServer.POChkMsg3;
            #endregion
        }
        //Prior Operation Check
        private void btn_PriorOpChk_Click(object sender, EventArgs e)
        { 
            string DeviceID = txt_qDeviceID.Text;
            //All fields must be fill out to proceed the Prior Op Check
            if (DeviceID == "" || txt_qSerial.Text.Any(x => !char.IsNumber(x)))
            {
                DialogResult Result;
                Result = MessageBox.Show("There are empty fields or the values are characters, Please check and try again!", "Diagnostics", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int Serial = Convert.ToInt32(txt_qSerial.Text);
                try
                {
                    OSQLServer.PriorOpCheck(Serial, DeviceID);
                }
                catch (Exception)
                {
                    HMI.OForm.SystemMessages("The connection to the SQL Server was failed\n","Error");
                }
            }
        }
        //Scanner: Control as Manual Mode
        private void btn_ScanTrigMan_Click(object sender, EventArgs e)
        {
            OScanner.SendCommand("< >",OScanner.Info);
        }
        //Scanner: Check the 2D Code Quality
        private void btn_ScanQuality_Click(object sender, EventArgs e)
        {
            OScanner.SendCommand("<VAL3>", 1);
        }
        //MTFocus6K: Select the batch to load
        private void btn_ActiveBatch_Click(object sender, EventArgs e)
        {
            string BatchValue = txt_BatchID.Value.ToString();
            if (OMTFocus6K.Parameters[(int)Instrument_Comm.CommStatus])
            {
                switch (BatchValue)
                {
                    case "1":
                        OMTFocus6K.SendCommand(OMTFocus6K.BSet1);
                        break;
                    case "2":
                        OMTFocus6K.SendCommand(OMTFocus6K.BSet2);
                        break;
                    case "3":
                        OMTFocus6K.SendCommand(OMTFocus6K.BSet3);
                        break;
                    case "4":
                        OMTFocus6K.SendCommand(OMTFocus6K.BSet4);
                        break;
                    default:
                        break;
                }
            }
        }
        //MTFocus6K: Disable the Screwdriver
        private void btn_DisableTool_Click(object sender, EventArgs e)
        {
            if (OMTFocus6K.Parameters[(int)Instrument_Comm.CommStatus])
            {
                OMTFocus6K.SendCommand(OMTFocus6K.MID0224);
            }
        }
        //MTFocus6K: Enable the Screwdriver
        private void btn_EnableTool_Click(object sender, EventArgs e)
        {
            if (OMTFocus6K.Parameters[(int)Instrument_Comm.CommStatus])
            {
                OMTFocus6K.SendCommand(OMTFocus6K.MID0225);
            }
        }
        
        //Vision Sensor: Trigger Manual
        private void btn_VisionSensorTrigMan_Click(object sender, EventArgs e)
        {
            OVisionSensor.Trigger();
        }

        //Vision Sensor: Change new program (000 = Spring Inspection, 001 = Armature Inspection)
        private void btn_ChangeProg_Click(object sender, EventArgs e)
        {
            OVisionSensor.SwitchProg(cbx_ProgramList.SelectedIndex);
        }
        //Vision Sensor: Get the Program List
        private void btn_GetVisionProgList_Click(object sender, EventArgs e)
        {
            if (OVisionSensor.Parameters[(int)Instrument_Comm.CommStatus])
            {
                //IV2-G500MA: Get the Program List
                OVisionSensor.GetProgramList();

                cbx_ProgramList.Items.Clear();

                for (int i = 0; i < OVisionSensor.ProgList.Items.Count; i++)
                {
                    cbx_ProgramList.Items.Add(OVisionSensor.ProgList.Items[i]);
                }
            } 
        }

        #region PCON
        private void btn_PCON_Standby_Click(object sender, EventArgs e)
        {

        }

        private void btn_PCON_Ready_Click(object sender, EventArgs e)
        {

        }

        private void btn_PCON_Test1_Click(object sender, EventArgs e)
        {

        }

        private void btn_PCON_Test2_Click(object sender, EventArgs e)
        {

        }

        private void btn_Screwdriving_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion

        #region Information

        #endregion

        #region Functions

        #region Public
        //MTFocus 6K
        public void TorqueAngleResults(int TorqueAngleSamples)
        {
            if (dataGridView1.InvokeRequired)
            {
                //Callback 
                TorqueAngleDataCallback d = new TorqueAngleDataCallback(TorqueAngleResults);
                this.Invoke(d, new object[] { TorqueAngleSamples });
            }
            else
            {
                //Update the results
                TableResults.Rows.Clear();
                Chart_TorqueAngle.Series["f(x)"].Points.Clear();

                //Log production data
                for (int i = 0; i < TorqueAngleSamples; i++)
                {
                    TableResults.Rows.Add(i,OMTFocus6K.Samples_Angle[i], OMTFocus6K.Samples_Torque[i]);

                    #region Chart of the process
                    Chart_TorqueAngle.Series["f(x)"].Points.AddXY(OMTFocus6K.Samples_Angle[i], OMTFocus6K.Samples_Torque[i]);
                    #endregion
                }
            }
        }

        //IV2-G500MA: Image
        public void GetImage()
        {
            if (img_IV2G500MA.InvokeRequired)
            {
                IV2G500MAImgCallback d = new IV2G500MAImgCallback(GetImage);
                this.Invoke(d, new object[] { });
            }
            else
            {
                img_IV2G500MA.Image = OVisionSensor.Image;
                img_IV2G500MA_Master.Image = OVisionSensor.Image_Master;
            }
        }

        #endregion

        #region Private
        //Read the CSV file (IO Labels)
        private void ReadMachineConfigFile()
        {
            try
            {
                #region IO Labels CSV file
                //Open the Production Data file to query the Device ID loaded
                String List;
                int i = 0;
                //Query the file Log
                StreamReader _File = new StreamReader(pathProdData3);
                while ((List = _File.ReadLine()) != null)
                {
                    string[] Data = List.Split(',');
                    //Configuration File
                    cSVfile[i] = Data[1];
                    i++;
                }
                _File.Close();
                #endregion
            }
            catch (Exception a)
            {
                Console.WriteLine(a);
            }
        }
        //Assign Array to Check box
        private void AssignCheckboxToIOLabels()
        {
            #region PLC

            #region Slot 1 (K2M0 - K2M8)
            IODef[0] = cbx_I000;
            IODef[1] = cbx_I001;
            IODef[2] = cbx_I002;
            IODef[3] = cbx_I003;
            IODef[4] = cbx_I004;
            IODef[5] = cbx_I005;
            IODef[6] = cbx_I006;
            IODef[7] = cbx_I007;
            IODef[8] = cbx_I008;
            IODef[9] = cbx_I009;
            IODef[10] = cbx_I010;
            IODef[11] = cbx_I011;
            IODef[12] = cbx_I012;
            IODef[13] = cbx_I013;
            IODef[14] = cbx_I014;
            IODef[15] = cbx_I015;
            #endregion

            #region Slot 2 (K2M16 - K2M31)
            IODef[16] = cbx_Q200;
            IODef[17] = cbx_Q201;
            IODef[18] = cbx_Q202;
            IODef[19] = cbx_Q203;
            IODef[20] = cbx_Q204;
            IODef[21] = cbx_Q205;
            IODef[22] = cbx_Q206;
            IODef[23] = cbx_Q207;
            IODef[24] = cbx_Q208;
            IODef[25] = cbx_Q209;
            IODef[26] = cbx_Q210;
            IODef[27] = cbx_Q211;
            IODef[28] = cbx_Q212;
            IODef[29] = cbx_Q213;
            IODef[30] = cbx_Q214;
            IODef[31] = cbx_Q215;
            #endregion

            #region Slot 3 (K2M48 - K2M63)
            IODef[32] = cbx_I100;
            IODef[33] = cbx_I101;
            IODef[34] = cbx_I102;
            IODef[35] = cbx_I103;
            IODef[36] = cbx_I104;
            IODef[37] = cbx_I105;
            IODef[38] = cbx_I106;
            IODef[39] = cbx_I107;
            IODef[40] = cbx_I108;
            IODef[41] = cbx_I109;
            IODef[42] = cbx_I110;
            IODef[43] = cbx_I111;
            IODef[44] = cbx_I112;
            IODef[45] = cbx_I113;
            IODef[46] = cbx_I114;
            IODef[47] = cbx_I115;
            #endregion

            #region Slot 4 (K2M64 - K2M72)
            IODef[48] = cbx_Q300;
            IODef[49] = cbx_Q301;
            IODef[50] = cbx_Q302;
            IODef[51] = cbx_Q303;
            IODef[52] = cbx_Q304;
            IODef[53] = cbx_Q305;
            IODef[54] = cbx_Q306;
            IODef[55] = cbx_Q307;
            IODef[56] = cbx_Q308;
            IODef[57] = cbx_Q309;
            IODef[58] = cbx_Q310;
            IODef[59] = cbx_Q311;
            IODef[60] = cbx_Q312;
            IODef[61] = cbx_Q313;
            IODef[62] = cbx_Q314;
            IODef[63] = cbx_Q315;
            #endregion

            #endregion

            #region Safety PLC

            #region XTIO 0
            IODef[64] = SafetyPLC_I001;
            IODef[65] = SafetyPLC_I002;
            IODef[66] = SafetyPLC_I003;
            IODef[67] = SafetyPLC_I004;
            IODef[68] = SafetyPLC_I005;
            IODef[69] = SafetyPLC_I006;
            IODef[70] = SafetyPLC_I007;
            IODef[71] = SafetyPLC_I008;
            IODef[72] = SafetyPLC_Q001;
            IODef[73] = SafetyPLC_Q002;
            IODef[74] = SafetyPLC_Q003;
            IODef[75] = SafetyPLC_Q004;

            #endregion

            #region XTIO 1
            IODef[76] = SafetyPLC_I101;
            IODef[77] = SafetyPLC_I102;
            IODef[78] = SafetyPLC_I103;
            IODef[79] = SafetyPLC_I104;
            IODef[80] = SafetyPLC_I105;
            IODef[81] = SafetyPLC_I106;
            IODef[82] = SafetyPLC_I107;
            IODef[83] = SafetyPLC_I108;
            IODef[84] = SafetyPLC_Q101;
            IODef[85] = SafetyPLC_Q102;
            IODef[86] = SafetyPLC_Q103;
            IODef[87] = SafetyPLC_Q104;
            #endregion

            #region XTIO 2
            IODef[88] = SafetyPLC_I201;
            IODef[89] = SafetyPLC_I202;
            IODef[90] = SafetyPLC_I203;
            IODef[91] = SafetyPLC_I204;
            IODef[92] = SafetyPLC_I205;
            IODef[93] = SafetyPLC_I206;
            IODef[94] = SafetyPLC_I207;
            IODef[95] = SafetyPLC_I208;
            IODef[96] = SafetyPLC_Q201;
            IODef[97] = SafetyPLC_Q202;
            IODef[98] = SafetyPLC_Q203;
            IODef[99] = SafetyPLC_Q204;
            #endregion

            #endregion
        }
        //Assign the text to Check box labels from IO definitions array
        private void AssignIOLabels()
        {
            for (int i = 0; i < MAX_CSVfields; i++)
            {
                IODef[i].Text = cSVfile[i];
            }
        }

        #endregion

        #endregion

    }
}


