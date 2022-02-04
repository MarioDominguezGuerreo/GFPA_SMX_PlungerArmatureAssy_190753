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
#endregion

#region Project Libraries
using PCdatetime;
using PCMessages;
# endregion

namespace Software
{
    public partial class Engineering : Form
    {
        #region Variables
        public struct Messages
        {
            public bool PriorOpCheck,
                        UpdateDeviceStatus,
                        StoreMeas,
                        Scanning,
                        StoreResults,
                        PLC,
                        SafetyControllers,
                        Instrument1,
                        Instrument2,
                        Kistler,
                        PCON,
                        Process1,
                        Process2,
                        Process3,
                        ChkComp,
                        AssignComp;
        }
        public struct Functions
        {
            public bool PriorOpCheck,
                        UpdateDeviceStatus,
                        StoreMeasurements,
                        StoreResults,
                        QueryLimits,
                        MasterPartsSeq,
                        Scanning,
                        Measurement1,
                        Screwdriving,
                        FDTest1,
                        FDTest2,
                        ConstKcalc,
                        ChkComp,
                        AssignComp;
        }
        #endregion

        #region Callbacks

        #endregion

        #region Threads

        #endregion

        #region Objects
        //Engineering page
        public static Engineering OEngineering;

        //Messages 
        private static Messages oMsgTrace = new Messages();
        public Messages OMsgTrace
        {
            get
            {
                return oMsgTrace;
            }

            set
            {
                oMsgTrace = value;
            }
        }
        //Messages 
        private static Functions oFunctions = new Functions();
        public Functions OFunctions
        {
            get
            {
                return oFunctions;
            }

            set
            {
                oFunctions = value;
            }
        }
        #endregion

        /*Initialization of the Engineering form*/
        public Engineering()
        {
            InitializeComponent();
            //Engineering page
            OEngineering = this;

            //Machine Status
            MachineStatus.Enabled = true;

            #region Production Custom parameters
            //Scanning
            btn_ScanningFunc.Checked = true;
            //Prior Operation Check
            btn_PriorOpChkFunc.Checked = true;
            //Store Measurements
            btn_StoreMeasureFunc.Checked = false;
            //Store Results
            btn_StoreResultsFunc.Checked = true;
            //Update device status
            btn_UpdateDevStsFunc.Checked = true;
            //Visual Inspection
            btn_VisualInspFunc.Checked = true;
            //Screwdriving
            btn_ScrewdrivingFunc.Checked = true;
            //Force & Displacement Test 1
            btn_FDTest1.Checked = true;
            //Force & Displacement Test 2
            btn_FDTest2.Checked = true;
            //Constant K calculation
            btn_ConstKCalc.Checked = true;
            //Query Limits from DB
            btn_LimitsDBFunc.Checked = true;
            //Master Parts Sequence
            btn_MasterPartsSeq.Checked = true;
            //Check Components
            btn_ChkCompFunc.Checked = false;
            //Assign Components to Serial ID
            btn_AssignComp.Checked = false;
            #endregion
        }

        #region Controls
        //Machine Status
        private void MachineStatus_Tick(object sender, EventArgs e)
        {
            #region Messages Management
            //Status the Prior Operation Check messages
            oMsgTrace.PriorOpCheck = cbx_PriorOpCheck.Checked;
            //Status the Update Device Status messages
            oMsgTrace.UpdateDeviceStatus = cbx_UpdateDeviceStatus.Checked;
            //Status the Store Measurements
            oMsgTrace.StoreMeas = cbx_StoreMeasurements.Checked;
            //Status the Scanning
            oMsgTrace.Scanning = cbx_Scanning.Checked;
            //Status the Store results
            oMsgTrace.StoreResults = cbx_StoreResults.Checked;
            //Status the PLC messages
            oMsgTrace.PLC = cbx_PLC.Checked;
            //Status the Safety Controller messages
            oMsgTrace.SafetyControllers = cbx_SafetyController.Checked;
            //Status the Instrument 1 messages        
            oMsgTrace.Instrument1 = cbx_Instrument1.Checked;
            //Status the Kistler messages  
            oMsgTrace.Kistler = cbx_Kistler.Checked;
            //Status the Servo Press (PCON) messages  
            oMsgTrace.Kistler = cbx_PCON.Checked;
            //Status the Process 1 messages  
            oMsgTrace.Process1 = cbx_Process1.Checked;
            //Status the Process 2 messages  
            oMsgTrace.Process2 = cbx_Process2.Checked;
            //Status the Process 3 messages  
            oMsgTrace.Process3 = cbx_Process3.Checked;
            //Check Components
            oMsgTrace.ChkComp = cbx_CheckComp.Checked;
            //Assign Components to Serial ID
            oMsgTrace.AssignComp = cbx_AssignComp.Checked;
            #endregion

            #region Functions Management

            #region Conditions of the Function selection
            //Check if the machine is in Manual mode, thus the functions are available to modify
            if (!HMI.OForm.MachineMode)
            {
                pnl_FuncManagement.Enabled = true;
                //Without any serial info, the system can not proceed with DB queries
                if (btn_ScanningFunc.Checked)
                {
                    //The Limits will be queried from the DB or CSV
                    btn_LimitsDBFunc.Enabled = true;
                    //Master Parts Sequence
                    btn_MasterPartsSeq.Enabled = true;
                   
                    //Check Components
                    btn_ChkCompFunc.Enabled = true;
                    if (btn_ChkCompFunc.Checked)
                    {
                        //Assign Components to Serial ID
                        btn_AssignComp.Enabled = true;
                    }
                    else
                    {
                        //Assign Components to Serial ID
                        btn_AssignComp.Enabled = false;
                        btn_AssignComp.Checked = false;
                    }

                    //Prior Op Check enable
                    btn_PriorOpChkFunc.Enabled = true;
                    if (btn_PriorOpChkFunc.Checked)
                    {
                        //Update device status enable
                        btn_UpdateDevStsFunc.Enabled = true;
                        //Store Results to DB
                        btn_StoreResultsFunc.Enabled = true;
                    }
                    else
                    {
                        //Update device status enable
                        btn_UpdateDevStsFunc.Enabled = false;
                        btn_UpdateDevStsFunc.Checked = false;
                        //Store Results to DB
                        btn_StoreResultsFunc.Enabled = false;
                        btn_StoreResultsFunc.Checked = false;
                    }

                    //Store the Mesurements to DB
                    btn_StoreMeasureFunc.Enabled = true;
                }
                else
                {
                    //The Limits will be queried from the CSV file
                    btn_LimitsDBFunc.Enabled = false;
                    btn_LimitsDBFunc.Checked = false;
                    //Prior Op Check disable
                    btn_PriorOpChkFunc.Checked = false;
                    btn_PriorOpChkFunc.Enabled = false;
                    //Update device status disable
                    btn_UpdateDevStsFunc.Checked = false;
                    btn_UpdateDevStsFunc.Enabled = false;
                    //Store Measurements to DB disable
                    btn_StoreMeasureFunc.Checked = false;
                    btn_StoreMeasureFunc.Enabled = false;
                    //Store Results to DB disable
                    btn_StoreResultsFunc.Enabled = false;
                    btn_StoreResultsFunc.Checked = false;
                    //Master Parts Sequence
                    btn_MasterPartsSeq.Enabled = false;
                    btn_MasterPartsSeq.Checked = false;
                    //Check Components
                    btn_ChkCompFunc.Enabled = false;
                    btn_ChkCompFunc.Checked = false;
                    //Assign Components to Serial ID
                    btn_AssignComp.Enabled = false;
                    btn_AssignComp.Checked = false;
                }
                //Force & Displacement, Constant K
                if (btn_FDTest1.Checked && btn_FDTest2.Checked)
                    btn_ConstKCalc.Enabled = true;
                else
                {
                    btn_ConstKCalc.Enabled = false;
                    btn_ConstKCalc.Checked = false;
                }

            }
            else
            {
                pnl_FuncManagement.Enabled = false;
            }
            #endregion

            #region Functions Selection status
            //Status the Prior Operation Check messages
            oFunctions.PriorOpCheck = btn_PriorOpChkFunc.Checked;
            //Status the Update Device Status messages
            oFunctions.UpdateDeviceStatus = btn_UpdateDevStsFunc.Checked;
            //Status the Store Measurements DB
            oFunctions.StoreMeasurements = btn_StoreMeasureFunc.Checked;
            //Status the Store Results
            oFunctions.StoreResults = btn_StoreResultsFunc.Checked;
            //Status the Scanning
            oFunctions.Scanning = btn_ScanningFunc.Checked;
            //Status the Measurement 1
            oFunctions.Measurement1 = btn_VisualInspFunc.Checked;
            //Status the MTFocus 6K Screwdriving
            oFunctions.Screwdriving = btn_ScrewdrivingFunc.Checked;
            //Status the Kistler
            oFunctions.FDTest1 = btn_FDTest1.Checked;
            //Status the Kistler
            oFunctions.FDTest2 = btn_FDTest2.Checked;
            //Constant K calculation
            oFunctions.ConstKcalc = btn_ConstKCalc.Checked;
            //Query Limits
            oFunctions.QueryLimits = btn_LimitsDBFunc.Checked;
            //Master Parts Sequence
            oFunctions.MasterPartsSeq = btn_MasterPartsSeq.Checked;
            //Check Components
            oFunctions.ChkComp = btn_ChkCompFunc.Checked;
            //Assign Components to Serial ID
            oFunctions.AssignComp = btn_AssignComp.Checked;
            #endregion

            #endregion
        }
        //Enable all messages
        private void btn_EnableMsgAll_Click(object sender, EventArgs e)
        {
            EnableMsgAll();
        }
        //Disable all messages
        private void btn_DisableMsgAll_Click(object sender, EventArgs e)
        {
            DisableMsgAll();
        }
        //Enable all functions
        private void btn_EnableFuncAll_Click(object sender, EventArgs e)
        {
            EnableFuncAll();
        }
        //Disable all functions
        private void btn_DisableFuncAll_Click(object sender, EventArgs e)
        {
            DisableFiuncAll();
        }
        #endregion

        #region Information

        #endregion

        #region Functions

        #region Public

        #endregion

        #region Private
        //Enable all messages
        public void EnableMsgAll()
        {
            //Prior Operation Check messages
            cbx_PriorOpCheck.Checked = true;
            //Update Device Status messages
            cbx_UpdateDeviceStatus.Checked = true;
            //Store Measurements
            cbx_StoreMeasurements.Checked = true;
            //Scanning
            cbx_Scanning.Checked = true;
            //Store Results
            cbx_StoreResults.Checked = true;
            //PLC messages
            cbx_PLC.Checked = true;
            //Safety Controller messages
            cbx_SafetyController.Checked = true;
            //Instrument 1 messages        
            cbx_Instrument1.Checked = true;
            //Instrument 2 messages  
            cbx_Instrument2.Checked = true;
            //Kistler messages        
            cbx_Kistler.Checked = true;
            //Servo press PCON messages  
            cbx_PCON.Checked = true;
            //Process 1 messages  
            cbx_Process1.Checked = true;
            //Process 2 messages  

            //Process 3 messages 

            //Check Components
            cbx_CheckComp.Checked = true;
            //Assign Components to Serial ID
            cbx_AssignComp.Checked = true;
        }
        //Disable all messages
        public void DisableMsgAll()
        {
            //Prior Operation Check messages
            cbx_PriorOpCheck.Checked = false;
            //Update Device Status messages
            cbx_UpdateDeviceStatus.Checked = false;
            //Store Measurements
            cbx_StoreMeasurements.Checked = false;
            //Scanning
            cbx_Scanning.Checked = false;
            //Store Results
            cbx_StoreResults.Checked = false;
            //PLC messages
            cbx_PLC.Checked = false;
            //Safety Controller messages
            cbx_SafetyController.Checked = false;
            //Instrument 1 messages        
            cbx_Instrument1.Checked = false;
            //Instrument 2 messages  
            cbx_Instrument2.Checked = false;
            //Kistler messages        
            cbx_Kistler.Checked = false;
            //Servo press PCON messages  
            cbx_PCON.Checked = false;
            //Process 1 messages  
            cbx_Process1.Checked = false;
            //Process 2 messages  

            //Process 3 messages 

            //Check Components
            cbx_CheckComp.Checked = false;
            //Assign Components to Serial ID
            cbx_AssignComp.Checked = false;
        }
        //Enable all functions
        public void EnableFuncAll()
        {
            //Scanning
            btn_ScanningFunc.Checked = true;
            //Prior Operation Check
            btn_PriorOpChkFunc.Checked = true;
            //Store Measurements
            btn_StoreMeasureFunc.Checked = true;
            //Store Results
            btn_StoreResultsFunc.Checked = true;
            //Update device status
            btn_UpdateDevStsFunc.Checked = true;
            //Measurement 1
            btn_VisualInspFunc.Checked = true;
            //Measurement 2
            btn_ScrewdrivingFunc.Checked = true;
            //Force & Displacement Test 1
            btn_FDTest1.Checked = true;
            //Force & Displacement Test 2
            btn_FDTest2.Checked = true;
            //Constant K calculation
            btn_ConstKCalc.Checked = true;
            //Query Limits from DB
            btn_LimitsDBFunc.Checked = true;
            //Master Parts Sequence
            btn_MasterPartsSeq.Checked = true;
            //Check Components
            btn_ChkCompFunc.Checked = true;
            //Assign Components to Serial ID
            btn_AssignComp.Checked = true;
        }
        //Disable all functions
        public void DisableFiuncAll()
        {
            //Scanning
            btn_ScanningFunc.Checked = false;
            //Prior Operation Check
            btn_PriorOpChkFunc.Checked = false;
            //Store Measurements
            btn_StoreMeasureFunc.Checked = false;
            //Store Results
            btn_StoreResultsFunc.Checked = false;
            //Update device status
            btn_UpdateDevStsFunc.Checked = false;
            //Measurement 1
            btn_VisualInspFunc.Checked = false;
            //Measurement 2
            btn_ScrewdrivingFunc.Checked = false;
            //Force & Displacement Test 1
            btn_FDTest1.Checked = false;
            //Force & Displacement Test 2
            btn_FDTest2.Checked = false;
            //Constant K calculation
            btn_ConstKCalc.Checked = false;
            //Query Limits from DB
            btn_LimitsDBFunc.Checked = false;
            //Master Parts Sequence
            btn_MasterPartsSeq.Checked = false;
            //Check Components
            btn_ChkCompFunc.Checked = false;
            //Assign Components to Serial ID
            btn_AssignComp.Checked = false;
        }
        #endregion

        #endregion
    }
}
