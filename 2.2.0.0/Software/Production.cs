//Mario A. Dominguez Guerrero 
//July - 2020

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
#endregion

#region Project Libraries
using PCdatetime;
using PCMessages;

#endregion

namespace Software
{
    public partial class Production : Form
    {
        #region Variables
        private bool ReadInfo = false;
        #endregion

        #region Callbacks

        #endregion

        #region Threads

        #endregion

        #region Objects
        //Production page
        public static Production OProduction;
        DataTable OTableLot = new DataTable();
        DataTable OTableOEE = new DataTable();
        DataTable OTableProd = new DataTable();

        //OEE
        OEE OOEE = new OEE();
        //Reports
        Reports OReports = new Reports();
        #endregion

        public Production()
        {
            InitializeComponent();
            //Production page
            OProduction = this;
            //Machine status scan
            MachineStatus.Enabled = true;
            //Targets 
            txt_TargetCycle.Text = "12";
            txt_TargetStop.Text = "45";
            txt_TargetRun.Text = "480";
            txt_TargetPieces.Text = "3000";
            //Apply the target
            TargetValues();
            //Production Tables
            Production_Init();
        }

        #region Controls
        //Status of the machine
        private void MachineStatus_Tick(object sender, EventArgs e)
        {
            #region Availability    

            #region Cycle
            //Start Cycle
            /*if (btn_start.Checked && !OOEE.Status[0])
            {
                OOEE.Init_CycleTimer1();
            }

            //Stop Cycle
            if (btn_Stop.Checked && OOEE.Status[0])
            {
                OOEE.Stop_CycleTimer1();
            }*/
            #endregion

            #region Downtime
            if (!OOEE.Status[0] && !OOEE.Status[1])
            {
                #region Runtime
                //Check the Runtime
                int[] Result = OOEE.Counter_hms4(OOEE.CounterRuntime, OOEE.CounterCycleTime);
                OOEE.OAvailability.Runtime = Result[2].ToString("00") + ":" + Result[1].ToString("00") + ":" + Result[0].ToString("00");
                OOEE.OAvailability.Runtime_s = (Result[2] * 60 * 60) + (Result[1] * 60) + (Result[0]);
            #endregion

            //Initialize the Downtime
            OOEE.Init_Downtime();
            }

            if (OOEE.Status[0])
            {
            OOEE.Stop_Downtime();
            }
            #endregion

            //Scan the Availability parameters
            txt_CycleTime.Text = OOEE.OAvailability.CycleTime;
            txt_Downtime.Text = OOEE.OAvailability.Downtime;
            txt_Runtime.Text = OOEE.OAvailability.Runtime;

            chart_CycleTime.Value = (Int16)OOEE.OAvailability.CycleTime_s;
            chart_Downtime.Value = (Int16)OOEE.OAvailability.Downtime_s;
            chart_Runtime.Value = (Int16)OOEE.OAvailability.Runtime_s;

            chart_Availability.Value = (float)OOEE.func_Availability();

            #region Color Arc of the Availability gauge
            if (OOEE.Availability >= 0 && OOEE.Availability < 40)
            {
                Availability_arc.BackColor = Color.Red;
                Availability_arc.BackColor2 = Color.Red;
            }
            else if (OOEE.Availability >= 40 && OOEE.Availability < 60)
            {
                Availability_arc.BackColor = Color.Orange;
                Availability_arc.BackColor2 = Color.Orange;
            }
            else if (OOEE.Availability >= 60 && OOEE.Availability < 85)
            {
                Availability_arc.BackColor = Color.Yellow;
                Availability_arc.BackColor2 = Color.Yellow;
            }
            else
            {
                Availability_arc.BackColor = Color.Green;
                Availability_arc.BackColor2 = Color.Green;
            }
            #endregion

            #endregion

            #region Performance
            //Sacn the Performance parameters
            chart_Performance.Value = (float)OOEE.func_Performance();

            #region Color Arc of the Performance gauge
            if (OOEE.Performance >= 0 && OOEE.Performance < 40)
            {
                Performance_arc.BackColor = Color.Red;
                Performance_arc.BackColor2 = Color.Red;
            }
            else if (OOEE.Performance >= 40 && OOEE.Performance < 60)
            {
                Performance_arc.BackColor = Color.Orange;
                Performance_arc.BackColor2 = Color.Orange;
            }
            else if (OOEE.Performance >= 60 && OOEE.Performance < 85)
            {
                Performance_arc.BackColor = Color.Yellow;
                Performance_arc.BackColor2 = Color.Yellow;
            }
            else
            {
                Performance_arc.BackColor = Color.Green;
                Performance_arc.BackColor2 = Color.Green;
            }
            #endregion

            #endregion

            #region Quality

            //Scan the Quality parameters
            chart_OK.Value = OOEE.OQuality.OKParts;
            chart_NG.Value = OOEE.OQuality.NGParts;

            chart_Quality.Value = (float)OOEE.func_Quality();

            #region Color Arc of the Quality gauge
            if (OOEE.Quality >= 0 && OOEE.Quality < 40)
            {
                Quality_arc.BackColor = Color.Red;
                Quality_arc.BackColor2 = Color.Red;
            }
            else if (OOEE.Quality >= 40 && OOEE.Quality < 60)
            {
                Quality_arc.BackColor = Color.Orange;
                Quality_arc.BackColor2 = Color.Orange;
            }
            else if (OOEE.Quality >= 60 && OOEE.Quality < 85)
            {
                Quality_arc.BackColor = Color.Yellow;
                Quality_arc.BackColor2 = Color.Yellow;
            }
            else
            {
                Quality_arc.BackColor = Color.Green;
                Quality_arc.BackColor2 = Color.Green;
            }
            #endregion

            #endregion

            #region OEE
            //Scan the OEE parameters
            chart_OEE.Value = (float)OOEE.func_OEE();

            #region Color Arc of the OEE gauge
            if (OOEE._OEE >= 0 && OOEE._OEE < 40)
            {
                OEE_arc.BackColor = Color.Red;
                OEE_arc.BackColor2 = Color.Red;
            }
            else if (OOEE._OEE >= 40 && OOEE._OEE < 60)
            {
                OEE_arc.BackColor = Color.Orange;
                OEE_arc.BackColor2 = Color.Orange;
            }
            else if (OOEE._OEE >= 60 && OOEE._OEE < 85)
            {
                OEE_arc.BackColor = Color.Yellow;
                OEE_arc.BackColor2 = Color.Yellow;
            }
            else
            {
                OEE_arc.BackColor = Color.Green;
                OEE_arc.BackColor2 = Color.Green;
            }
            #endregion

            #endregion

            #region Production Status
            chart_ProdStatus.Value = (float)OOEE.func_ProducedPorcent();
            gauge_Total.Value = (float)OOEE.OQuality.TotalParts;
            #endregion

            #region Planned Production 
            //Scan the Target Parameters


            #endregion

            #region Reports: Lot, OEE and Production
            if (ReadInfo)
            {
                //Lot History
                OReports.ReadLotHistory(OTableLot);
                //OEE History
                OReports.ReadOEEHistory(OTableOEE);
                //Production History
                OReports.ReadProdHistory(OTableProd);

                ReadInfo = false;
            }
            #endregion

            #region Users System
            switch (UserLogSystem.OUserLogSystem.User)
            {
                case "Developer":
                    pnl_ProdPlan.Enabled = true;
                    break;
                case "Engineering":
                    pnl_ProdPlan.Enabled = false;
                    break;
                case "Production":
                    pnl_ProdPlan.Enabled = true;
                    break;
                case "Maintenance":
                    pnl_ProdPlan.Enabled = false;
                    break;
                //Operator Mode
                default:
                    pnl_ProdPlan.Enabled = false;
                    break;
            }
            #endregion
        }

        //Apply changes for OEE Targets
        private void btn_ApplyTargets_Click(object sender, EventArgs e)
        {
            DialogResult Answer;
            Answer = MessageBox.Show("Do you want to change the current targets?", "Production", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (Answer == DialogResult.OK)
            {
                //Apply the new target values setting up
                TargetValues();

                //Message
                HMI.OForm.ProdMessages("Production: The target values has been changed\n", "PLC");
            }
        }
        //Refresh info of the Reports: Lot, OEE and Production
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            ReadInfo = true;
        }
        #endregion

        #region Information

        #endregion

        #region Functions

        #region Public
        public void TargetValues()
        {
            if ((txt_TargetCycle.Text == "" || txt_TargetCycle.Text.Any(x => !char.IsNumber(x))) ||
                    (txt_TargetStop.Text == "" || txt_TargetStop.Text.Any(x => !char.IsNumber(x))) ||
                    (txt_TargetRun.Text == "" || txt_TargetRun.Text.Any(x => !char.IsNumber(x))) ||
                    (txt_TargetPieces.Text == "" || txt_TargetPieces.Text.Any(x => !char.IsNumber(x))))
            {
                MessageBox.Show("Targets: Value error, verify all fields are integer numbers");
            }
            else
            {

                //Target values
                OOEE.OProdPlan.TargetCycle = txt_TargetCycle.Text;
                OOEE.OProdPlan.TargetStop = txt_TargetStop.Text;
                OOEE.OProdPlan.TargetRun = txt_TargetRun.Text;
                OOEE.OProdPlan.TargetPieces = txt_TargetPieces.Text;
                //Reset the counters: Cycle Time, Runtime, Downtime
                for (int i = 0; i < OOEE.CounterCycleTime.Length; i++)
                {
                    OOEE.CounterCycleTime[i] = 0;
                    OOEE.CounterDowntime[i] = 0;
                    OOEE.CounterRuntime[i] = 0;
                }
                //Target values to chart range
                chart_CycleTime.RangeEnd = Convert.ToInt64(OOEE.OProdPlan.TargetCycle);
                chart_Downtime.RangeEnd = Convert.ToInt64(OOEE.OProdPlan.TargetStop) * 60;
                chart_Runtime.RangeEnd = Convert.ToInt64(OOEE.OProdPlan.TargetRun) * 60;
                chart_OK.RangeEnd = Convert.ToInt64(OOEE.OProdPlan.TargetPieces);
                chart_NG.RangeEnd = Convert.ToInt64(OOEE.OProdPlan.TargetPieces);
                gauge_Total.RangeEnd = Convert.ToInt64(OOEE.OProdPlan.TargetPieces);
            }
        }
        #endregion

        #region Private
        private void Production_Init()
        {
            #region Lot Record Table
            /*Production History*/
            //Defines of the Columns of the Lot record Table
            OTableLot.Columns.Add("Date", typeof(string));
            OTableLot.Columns.Add("Time", typeof(string));
            OTableLot.Columns.Add("Device ID", typeof(string));
            OTableLot.Columns.Add("Lot ID", typeof(string));
            OTableLot.Columns.Add("Quantity", typeof(string));
            OTableLot.Columns.Add("OK Parts", typeof(string));
            OTableLot.Columns.Add("NG Parts", typeof(string));
            OTableLot.Columns.Add("Total", typeof(string));
            OTableLot.Columns.Add("Yield (%)", typeof(string));
            //Bind the Table to Data Grid Viewer
            dataGridView_LotHistory.DataSource = OTableLot;
            #endregion

            #region OEE record Table
            //Defines of the Columns of the OEE record Table
            OTableOEE.Columns.Add("Date", typeof(string));
            OTableOEE.Columns.Add("Time", typeof(string));
            OTableOEE.Columns.Add("Device ID", typeof(string));
            OTableOEE.Columns.Add("Lot ID", typeof(string));
            OTableOEE.Columns.Add("Quantity", typeof(string));
            OTableOEE.Columns.Add("Produced (%)", typeof(string));
            OTableOEE.Columns.Add("OEE (%)", typeof(string));
            OTableOEE.Columns.Add("Availability (%)", typeof(string));
            OTableOEE.Columns.Add("Performance (%)", typeof(string));
            OTableOEE.Columns.Add("Quality (%)", typeof(string));
            //Bind the Table to Data Grid Viewer
            dataGridView_OEEHistory.DataSource = OTableOEE;
            #endregion

            #region Production record Table
            //Defines of the Columns of the Production record Table
            OTableProd.Columns.Add("Date", typeof(string));
            OTableProd.Columns.Add("Time", typeof(string)); ;
            OTableProd.Columns.Add("Total Parts", typeof(string));
            OTableProd.Columns.Add("OK Parts", typeof(string));
            OTableProd.Columns.Add("NG Parts", typeof(string));
            OTableProd.Columns.Add("Target Run", typeof(string));
            OTableProd.Columns.Add("Target Stop", typeof(string));
            OTableProd.Columns.Add("Target Cycle", typeof(string));
            OTableProd.Columns.Add("Target Pieces", typeof(string));
            //Bind the Table to Data Grid Viewer
            dataGridView_ProdHistory.DataSource = OTableProd;
            #endregion
        }
        #endregion

        #endregion
    }
}
