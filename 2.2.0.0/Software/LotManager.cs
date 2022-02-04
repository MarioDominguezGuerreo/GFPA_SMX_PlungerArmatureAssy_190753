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
using System.Diagnostics;
#endregion

#region Project Libraries
using PCdatetime;
using PCMessages;
#endregion

namespace Software
{
    public partial class LotManager : Form
    {
        #region Variables
        //Device ID List
        public string pathDeviceIDData = "ProductionData/DeviceID_List.txt";
        
        //Current Lot
        public struct Lot
        {
            private static double yield;
            private static int nGParts;
            private static int oKParts;
            private static double produced;
            private static double quantity;
            private static double total;
            //Bad parts or NG
            public int NGParts
            {
                get
                {
                    return nGParts;
                }

                set
                {
                    nGParts = value;
                }
            }
            //Good parts or OK
            public int OKParts
            {
                get
                {
                    return oKParts;
                }

                set
                {
                    oKParts = value;
                }
            }
            //Total Parts produced
            public double Produced
            {
                get
                {
                    return produced;
                }

                set
                {
                    produced = value;
                }
            }
            //Target Parts
            public double Quantity
            {
                get
                {
                    return quantity;
                }

                set
                {
                    quantity = value;
                }
            }
            //OK parts + NG Parts
            public double Total
            {
                get
                {
                    return total;
                }

                set
                {
                    total = value;
                }
            }
            //Quality OK Parts vs NG Parts
            public double Yield
            {
                get
                {
                    return yield;
                }

                set
                {
                    yield = value;
                }
            }
        }
        //New Lot
        public struct NewLot
        {
            //Device ID
            private static string deviceID;
            public string DeviceID
            {
                get
                {
                    return deviceID;
                }

                set
                {
                    deviceID = value;
                }
            }
            //Lot ID
            private static string lotID;
            public string LotID
            {
                get
                {
                    return lotID;
                }

                set
                {
                    lotID = value;
                }
            }
            //Quantity of the New Lot
            private int quantity;
            public int Quantity
            {
                get
                {
                    return quantity;
                }

                set
                {
                    quantity = value;
                }
            }
        }

        #region Alarms & Events
        private static bool[] lotManager_AE = new bool[10];
        public bool[] LotManager_AE
        {
            get
            {
                return lotManager_AE;
            }

            set
            {
                lotManager_AE = value;
            }
        }
        private static bool[] lotManager_Lock = new bool[10];
        public bool[] LotManager_Lock
        {
            get
            {
                return lotManager_Lock;
            }

            set
            {
                lotManager_Lock = value;
            }
        }
        #endregion

        #endregion

        #region Callbacks

        #endregion 

        #region Objects
        //Lot Manager page
        public static LotManager OLotManager;

        //Lot data
        public Lot OLot = new Lot();
        public NewLot ONewLot = new NewLot();
        //Reports
        Reports OReports = new Reports();
        //Kistler: Force and Displacement Instrument
        Kistler_5847B0 OKistler = new Kistler_5847B0();
        //PCON-CB: Servomotor controller
        IAI_PCON OPCON = new IAI_PCON();
        //Process
        ProdProcess OProcess = new ProdProcess();
        //SQL Database
        SQLDatabase OSQLServer = new SQLDatabase();
        //OEE
        OEE OOEE = new OEE();
        //Master Info
        public DataTable OTableMasters = new DataTable();
        #endregion

        /*Initialization of the Lot Manager form*/
        public LotManager()
        {
            InitializeComponent();
            //Lot Manager page
            OLotManager = this;

            #region Lot Production
            //Current Lot
            lb_DeviceID.Text = "empty";
            lb_LotID.Text = "empty";
            lb_LotQuantity.Text = "0000";
            OLot.OKParts = 0;
            OLot.NGParts = 0;
            // New Lot
            ONewLot.Quantity = 0;
            ONewLot.LotID = "empty";
            ONewLot.DeviceID = "empty";
            lotManager_AE[(int)LotManager_parameters.LotActive] = true;
            //Controls
            btn_EndLot.Enabled = false;
            btn_NewLot.Enabled = true;
            #endregion

            //Master Info
            MasterInfoTable();

            //Machine Status
            MachineStatus.Enabled = true;
        }

        private void LotManager_Load(object sender, EventArgs e)
        {
            //Get the Device IDs (Part Numbers) from DB
            OSQLServer.DeviceIDs(OSQLServer.DeviceFamily);

            //Load the Device List 
            LoadDeviceIDList();
        }

        #region Controls
        //Machine status
        private void MachineStatus_Tick(object sender, EventArgs e)
        {

            #region New Lot
            //Current Lot: Device ID
            lb_DeviceID.Text = ONewLot.DeviceID;
            OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID] = ONewLot.DeviceID;
            //Current Lot: Lot ID
            lb_LotID.Text = ONewLot.LotID.ToString();
            OSQLServer.ProdLot_Param[(int)Prod_Lot.LotID] = ONewLot.LotID.ToString();
            //Current Lot: Quantity
            lb_LotQuantity.Text = ONewLot.Quantity.ToString();
            OLot.Quantity = ONewLot.Quantity;
            #endregion

            #region End Lot the Current Lot
            Auto_Endlot();
            #endregion

            #region Current Lot: gauge Chart
            // Lot Production status (%)
            gauge_Produced.Value = (float)ProducedPorcent();
            // Total Parts
            gauge_Total.Value = (float)TotalParts();
            // Yield of the Lot
            gauge_Yield.Value = (float)Yield();
            // OK or Good Parts
            gauge_OKParts.Value = (float)OLot.OKParts;
            // NG or Bad Parts
            gauge_NGParts.Value = (float)OLot.NGParts;
            #endregion

            #region Color Arc of the Yield gauge
            if (OLot.Yield >= 0 && OLot.Yield < 40)
            {
                Yield_arc.BackColor = Color.Red;
                Yield_arc.BackColor2 = Color.Red;
            }
            else if (OLot.Yield >= 40 && OLot.Yield < 60)
            {
                Yield_arc.BackColor = Color.Orange;
                Yield_arc.BackColor2 = Color.Orange;
            }
            else if (OLot.Yield >= 60 && OLot.Yield < 85)
            {
                Yield_arc.BackColor = Color.Yellow;
                Yield_arc.BackColor2 = Color.Yellow;
            }
            else
            {
                Yield_arc.BackColor = Color.Green;
                Yield_arc.BackColor2 = Color.Green;
            }
            #endregion
        }
        // Lot 
        //Add New Lot 
        private void btn_NewLot_Click(object sender, EventArgs e)
        {
            Manual_NewLot();
        }
        //Cancel th current Lot 
        private void btn_EndLot_Click(object sender, EventArgs e)
        {
            DialogResult Answer;
            Answer = MessageBox.Show("Do you want to finish the current Lot?", "Lot Manager", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (Answer == DialogResult.OK)
            {
                //End Lot
                if (Process_EndLot())
                {
                    //Message
                    HMI.OForm.ProdMessages("Lot: " + ONewLot.LotID + " has been stopped before to finish\n", "PLC");
                }
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

        #region Public
        /// Lot Production status (%)
        /// Produced = (Total Parts * 100) / Target Parts
        public double ProducedPorcent()
        {
            OLot.Produced = (OLot.Total * 100) / OLot.Quantity;
            return OLot.Produced;
        }
        /// Total Parts of the current Lot
        /// Total = OK Parts + NG Parts;
        public double TotalParts()
        {
            OLot.Total = OLot.OKParts + OLot.NGParts;
            return OLot.Total;
        }
        /// Yield of the current Lot
        /// Yield = (OK Parts * 100) / Total parts 
        public double Yield()
        {
            OLot.Yield = (OLot.OKParts * 100) / OLot.Total;
            return OLot.Yield;
        }
        //New Lot
        public void Manual_NewLot()
        {
            int bSucess = 0;
            //All fields must be fill out to proceed the Start production
            //Verify length value of the Lot ID and Lot Qty are defined, with purpose no exceed the standard numbers
            //Verify if the value is alfanumeric or numeric
            if ((txt_LotID.Text == "" || txt_LotID.TextLength > 20) ||
                (txt_LotQuantity.Text == "" || txt_LotQuantity.TextLength > 10 || txt_LotQuantity.Text.Any(x => !char.IsNumber(x))) ||
                cbx_DeviceID.Text == "")
            {
                DialogResult Result;
                Result = MessageBox.Show("There are empty fields or the values are characters, Please check and try again!", "Lot Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bSucess++;
            }
            else
            { 
                //Set the new values for the New Lot
                ONewLot.LotID = txt_LotID.Text;
                ONewLot.DeviceID = cbx_DeviceID.Text;
                ONewLot.Quantity = Convert.ToInt32(txt_LotQuantity.Text);
                //OEE info
                Int64 TargetPieces = Convert.ToInt64(OOEE.OProdPlan.TargetPieces);
                Int64 QuantityAvailable = TargetPieces - OOEE.OQuality.TotalParts;
                //Verify to doesn't excess the quantity over Lot quantity
                if (ONewLot.Quantity <= QuantityAvailable)
                {
                    #region To query the limits (Database or CSV file) on base of the Device ID
                    if (Engineering.OEngineering.OMsgTrace.StoreMeas)
                    {
                        //Message
                        HMI.OForm.ProdMessages("Lot: Instrument 1: Quering the limits\n", "PLC");
                    }
                    //Select the path of the limits, from DB or CSV Limit file
                    if (Engineering.OEngineering.OFunctions.QueryLimits)
                    {
                        if (OSQLServer.LimitsDB(ONewLot.DeviceID))
                        {
                            //Message
                            HMI.OForm.ProdMessages("Lot: The limits: " + OSQLServer.Limits[(int)Prod_Limits.TorqueMin] + "/" + OSQLServer.Limits[(int)Prod_Limits.TorqueMax] + " , "
                                                                       + OSQLServer.Limits[(int)Prod_Limits.AngleMin] + "/" + OSQLServer.Limits[(int)Prod_Limits.AngleMax] + " were Successful downloaded  from DB\n", "OK");
                            //Verify if the DB or CSV limits are into the range for the PCON controller
                            if (VerificationLimits())
                            {
                                if (OSQLServer.MasterPartsSequence(ONewLot.DeviceID))
                                {
                                    //Message
                                    HMI.OForm.ProdMessages("Lot: Masters Target were Successful downloaded  from DB\n", "OK");
                                }
                                else
                                {
                                    
                                    HMI.OForm.ProdMessages("Lot: Masters Target can't downloaded due DB issue\n", "Error");
                                    bSucess++;
                                }
                            }
                            else
                            {
                                DialogResult Result;
                                Result = MessageBox.Show("The limits are out of range (PCON Servomotor)", "Lot Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bSucess++;
                            }
                        } 
                        else
                        { 
                            //Message
                            HMI.OForm.ProdMessages("Lot: The limits can't downloaded due DB issue\n", "Error");
                            bSucess++;
                        }
                    }
                    else
                    {
                        if (OSQLServer.LimitsCSV(ONewLot.DeviceID))
                        {
                            //Message
                            HMI.OForm.ProdMessages("Lot: The limits: " + OSQLServer.Limits[(int)Prod_Limits.TorqueMin] + "/" + OSQLServer.Limits[(int)Prod_Limits.TorqueMax] + " , " 
                                                                       + OSQLServer.Limits[(int)Prod_Limits.AngleMin] + "/" + OSQLServer.Limits[(int)Prod_Limits.AngleMax] + " were Successful downloaded from Local CSV file\n", "OK");
                            //Verify if the DB or CSV limits are into the range for the PCON controller
                            if (!VerificationLimits())
                            {
                                //Message
                                DialogResult Result;
                                Result = MessageBox.Show("The limits are out of range (PCON Servomotor)", "Lot Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bSucess++;
                            }
                        }
                        else
                        {
                            //Message
                            HMI.OForm.ProdMessages("Lot:  The limits can't downloaded due file missing\n", "Error");
                            bSucess++;
                        }
                    }
                    #endregion

                    #region To load the limits (Database or CSV file) to the Instruments
                    if (Engineering.OEngineering.OMsgTrace.Scanning)
                    {
                        //Message
                        HMI.OForm.ProdMessages("Lot: Instrument 1: Loading the limits\n", "PLC");
                    }
                   
                    #endregion
                }
                else
                {
                    DialogResult Result;
                    Result = MessageBox.Show("The Lot quantity is more than production quantity target, Please check and try again!", "Lot Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bSucess++;
                } 
            }
            //To verify if all process was successfully
            if (bSucess == 0)
            {
                DialogResult Result;
                Result = MessageBox.Show("The New Lot: " + ONewLot.LotID + " was loaded successfully", "Lot Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Message
                HMI.OForm.ProdMessages("Lot: " + ONewLot.LotID + " was Loaded\n", "PLC");

                //Lot ready, the machine is enable to Automatic
                #region Reset the current Lot for the New Lot values 
                OLot.OKParts = 0;
                OLot.NGParts = 0;
                //Controls
                btn_EndLot.Enabled = true;
                btn_NewLot.Enabled = false;
                gbx_NewLot.Enabled = false;
                //New values to chart range
                gauge_OKParts.RangeEnd = Convert.ToInt64(ONewLot.Quantity);
                gauge_NGParts.RangeEnd = Convert.ToInt64(ONewLot.Quantity);
                gauge_Total.RangeEnd = Convert.ToInt64(ONewLot.Quantity);
                #endregion

                lotManager_AE[(int)LotManager_parameters.LotActive] = false;
            }
            else
            {
                DialogResult Result;
                Result = MessageBox.Show("The New Lot: " + ONewLot.LotID + " was not loaded due CSV file or DB issue", "Lot Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Message
                HMI.OForm.ProdMessages("Lot: " + ONewLot.LotID + " couldn't be loaded\n", "Error");
            }
        }
        //End lot at time
        public bool Process_EndLot()
        {
            bool bSucess = false;

            #region Lot status: save Logs and History 
            //Log into file
            if (OReports.WriteLotHistory())
            {
                bSucess = true;
            }
            else
                bSucess = false;

            if (OReports.WriteOEEHistory())
            {
                bSucess = true;
            }
            else
                bSucess = false;

            if (OReports.WriteProdHistory())
            {
                bSucess = true;
            }
            else
                bSucess = false;
            //Message

            #endregion

            #region Reset the New Lot values 
            cbx_DeviceID.Text = "";
            txt_LotID.Text = "";
            txt_LotQuantity.Text = "";
            ONewLot.Quantity = 0;
            lotManager_AE[(int)LotManager_parameters.LotActive] = true;
            //Controls
            btn_EndLot.Enabled = false;
            btn_NewLot.Enabled = true;
            gbx_NewLot.Enabled = true;
            #endregion

            if (bSucess)
            {
                //Message
                HMI.OForm.ProdMessages("Lot: " + ONewLot.LotID + " has finished\n", "PLC");
            }
            return bSucess;
        }
        // Automatic End Lot process
        public void Auto_Endlot()
        {
            if ((OLot.Quantity == OLot.Total) && (OLot.Quantity != 0) && (OLot.Total != 0))
            {
                Process_EndLot();
                MessageBox.Show("The production Lot has finished, please Load New Lot", "Lot Manager");
            }
        }
        #endregion

        #region Private
        //Load Device List (All models ables)
        private bool LoadDeviceIDList()
        {
            bool bSucess = false;
            cbx_DeviceID.Items.Clear();
            try
            {
                String List;
                StreamReader _File = new StreamReader(pathDeviceIDData);
                while ((List = _File.ReadLine()) != null)
                {
                    cbx_DeviceID.Items.Add(List);
                }
                _File.Close();
                bSucess = true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.ProdMessages("User Log System: The Device ID List.txt Files wasn't loaded\n", "Error");
                bSucess = false;
            }
            return bSucess;
        }
        //Master Info Table
        private void MasterInfoTable()
        {
            //Defines of the Columns of the Master parameters Table
            OTableMasters.Columns.Add("Serial No.", typeof(string));
            OTableMasters.Columns.Add("Device ID", typeof(string));
            OTableMasters.Columns.Add("Station", typeof(string));
            OTableMasters.Columns.Add("Status", typeof(string));
            OTableMasters.Columns.Add("Expected Value", typeof(string));
            OTableMasters.Columns.Add("Sequence No.", typeof(string));
            OTableMasters.Columns.Add("Nest", typeof(string));
            //Bind the Table to Data Grid Viewer
            dataGridView_Masters.DataSource = OTableMasters;
        }
        //Limits verificacion: PCON Controller Servomotor
        private bool VerificationLimits()
        {
            bool bSucc = true;

            #region PCON factory limits
            double[] LimitVerification = new double[20];
            try
            {
                /// Factory limits of the Servomotor
                ///                         min     max
                /// Position            =   0.15 to 200.15  mm
                /// Speed               =   1    to 210     mm/s
                /// Acc/Decc            =   0.01 to 1.0     G
                /// Position Band       =   0.01 to 200.30  mm/s2
                /// Press Current Limit =   0    to 175  
                /// ----------------------------------------------
                /// Values for general purpose 
                /// Acc/Decc             =   0.30 mm/s
                /// Position Band        =   Position Target
                /// Press current limit  =   125
                /// ----------------------------------------------
                for (int i = 0; i < 10; i++)
                {
                    LimitVerification[i] = Convert.ToDouble(OPCON.Factory_Limits[i]);
                }
                //Position
                LimitVerification[10] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.StandbyPos]);
                LimitVerification[11] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ReadyPos]);
                LimitVerification[12] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test1Pos]);
                LimitVerification[13] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test2Pos]);
                LimitVerification[14] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ScrewdrivingPos]);
                //Speed
                LimitVerification[15] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.StandbySpeed]);
                LimitVerification[16] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ReadySpeed]);
                LimitVerification[17] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test1Speed]);
                LimitVerification[18] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.Test2Speed]);
                LimitVerification[19] = Convert.ToDouble(OSQLServer.Limits[(int)Prod_Limits.ScrewdrivingSpeed]);

                //Verification of the displacement are into the range
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.PosTargetmin] <= LimitVerification[10] && LimitVerification[10] <= LimitVerification[(int)PCON_Factory_Limits.PosTargetmax];
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.PosTargetmin] <= LimitVerification[11] && LimitVerification[11] <= LimitVerification[(int)PCON_Factory_Limits.PosTargetmax];
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.PosTargetmin] <= LimitVerification[12] && LimitVerification[12] <= LimitVerification[(int)PCON_Factory_Limits.PosTargetmax];
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.PosTargetmin] <= LimitVerification[13] && LimitVerification[13] <= LimitVerification[(int)PCON_Factory_Limits.PosTargetmax];
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.PosTargetmin] <= LimitVerification[14] && LimitVerification[14] <= LimitVerification[(int)PCON_Factory_Limits.PosTargetmax];
                //Verification of the Speed are into the range
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmin] <= LimitVerification[15] && LimitVerification[15] <= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmax];
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmin] <= LimitVerification[16] && LimitVerification[16] <= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmax];
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmin] <= LimitVerification[17] && LimitVerification[17] <= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmax];
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmin] <= LimitVerification[18] && LimitVerification[18] <= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmax];
                bSucc &= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmin] <= LimitVerification[19] && LimitVerification[19] <= LimitVerification[(int)PCON_Factory_Limits.SpeedTargetmax];

                //Verification of the Position band are into the range
                //->Constant, Position Band = Position Target
                //Verification of the Acc / Decc are into the range
                //->Constant,  
                //Verification of the Press Current Limit are into the range
                //->Constant, 
            }
            catch (Exception)
            {
                bSucc = false;
            }

            #endregion

            return bSucc;
        }
        #endregion

        #endregion

        #region Threads

        #endregion 
    }
}
