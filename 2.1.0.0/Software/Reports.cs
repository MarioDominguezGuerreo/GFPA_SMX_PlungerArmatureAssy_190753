//Mario A. Dominguez Guerrero 
//July - 2020

#region System Libraries
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
#endregion

#region Project Libraries
using PCdatetime;
using PCMessages;
using System.Data;
#endregion

namespace Software
{
    class Reports
    {
        #region Variables
        //Paths for History of Production
        public string pathProdData1 = "ProductionData/Lot_History.txt";
        public string pathProdData2 = "ProductionData/OEE_History.txt";
        public string pathProdData3 = "ProductionData/Production_History.txt";
        //This path is changing on base of the date
        private static string pathProdData4;
        public string PathProdData4
        {
            get
            {
                return pathProdData4;
            }

            set
            {
                pathProdData4 = value;
            }
        }
        private static string pathProdData5;
        public string PathProdData5
        {
            get
            {
                return pathProdData5;
            }

            set
            {
                pathProdData5 = value;
            }
        }
        #endregion

        #region Callbacks

        #endregion

        #region Objects
        //Time form the system (PC desktop)
        FechaHora LocalTime = new FechaHora();
        //Messages
        Messages MachineMsg = new Messages();

        //OEE
        OEE OOEE = new OEE();
        //Screwdriver Controller: MTFocus 6K
        MTFocus6K OMTFocus6K = new MTFocus6K();
        //Scanner
        Scanner OScanner = new Scanner();
        //SQL Server
        SQLDatabase OSQLServer = new SQLDatabase();
        #endregion

        #region Controls

        #endregion

        #region Information

        #endregion

        #region Functions

        #region Public
        //To Write the Lot History TXT file
        public bool WriteLotHistory()
        {
            try
            {
                //Open File
                StreamWriter StartUpSoft = File.AppendText(pathProdData1);
                //----------------------------------------------------------------------------------------------------------------------------------
                StartUpSoft.WriteLine(LocalTime.PCFecha() + "," + LocalTime.PCHora() + "," + OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID] +
                                                                                       "," + OSQLServer.ProdLot_Param[(int)Prod_Lot.LotID] +
                                                                                       "," + LotManager.OLotManager.OLot.Quantity +
                                                                                       "," + LotManager.OLotManager.OLot.OKParts +
                                                                                       "," + LotManager.OLotManager.OLot.NGParts +
                                                                                       "," + LotManager.OLotManager.OLot.Total +
                                                                                       "," + LotManager.OLotManager.OLot.Yield);
                //----------------------------------------------------------------------------------------------------------------------------------          
                //Close File
                StartUpSoft.Close();
                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.ProdMessages("Lot Finished: " + OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID] + "/" + OSQLServer.ProdLot_Param[(int)Prod_Lot.LotID] + "\n", "PLC");
                return false;
            }
            
        }
        //To Read the Lot History TXT file and Write the data table
        public bool ReadLotHistory(DataTable TextArea)
        {
            try
            {
                #region Lot History record
                //Open the Production Data file to query the Device ID loaded
                String List;
                //Clear the record
                TextArea.Clear();
                //Query the file Log
                StreamReader _File = new StreamReader(pathProdData1);
                while ((List = _File.ReadLine()) != null)
                {
                    string[] CompleteChain = List.Split(',');
                    TextArea.Rows.Add(CompleteChain[0], 
                                      CompleteChain[1], 
                                      CompleteChain[2], 
                                      CompleteChain[3], 
                                      CompleteChain[4], 
                                      CompleteChain[5], 
                                      CompleteChain[6],
                                      CompleteChain[7],
                                      CompleteChain[8]);
                }
                _File.Close();
                #endregion

                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Error to read the Lot History file\n", "Error");
                return false;
            }
        }
        //To Write the OEE History TXT file    
        public bool WriteOEEHistory()
        {
            try
            {
                //Open File
                StreamWriter StartUpSoft = File.AppendText(pathProdData2);
                //----------------------------------------------------------------------------------------------------------------------------------------
                StartUpSoft.WriteLine(LocalTime.PCFecha() + "," + LocalTime.PCHora() + "," + OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID] +
                                                                                       "," + OSQLServer.ProdLot_Param[(int)Prod_Lot.LotID] +
                                                                                       "," + LotManager.OLotManager.OLot.Quantity +
                                                                                       "," + OOEE.Produced +
                                                                                       "," + OOEE._OEE +
                                                                                       "," + OOEE.Availability +
                                                                                       "," + OOEE.Performance +
                                                                                       "," + OOEE.Quality);
                //----------------------------------------------------------------------------------------------------------------------------------------
                //Close File
                StartUpSoft.Close();
                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.ProdMessages("The OEE History registered\n", "PLC");
                return false;
            }
            
        }
        //To Read the OEE History TXT file and Write the data table
        public bool ReadOEEHistory(DataTable TextArea)
        {
            try
            {
                #region OEE History record
                //Open the Production Data file to query the Device ID loaded
                String List;
                //Clear the record
                TextArea.Clear();
                //Query the file Log
                StreamReader _File = new StreamReader(pathProdData2);
                while ((List = _File.ReadLine()) != null)
                {
                    string[] CompleteChain = List.Split(',');
                    TextArea.Rows.Add(CompleteChain[0],
                                      CompleteChain[1],
                                      CompleteChain[2],
                                      CompleteChain[3],
                                      CompleteChain[4],
                                      CompleteChain[5],
                                      CompleteChain[6],
                                      CompleteChain[7],
                                      CompleteChain[8],
                                      CompleteChain[9]);
                }
                _File.Close();
                #endregion

                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Error to read the OEE History file\n", "Error");
                return false;
            }
        }
        //To Write the Production History TXT file
        public bool WriteProdHistory()
        {
            try
            {
                //Open File
                StreamWriter StartUpSoft = File.AppendText(pathProdData3);
                //------------------------------------------------------------------------------------------------------------------------
                StartUpSoft.WriteLine(LocalTime.PCFecha() + "," + LocalTime.PCHora() + "," + OOEE.OQuality.TotalParts +
                                                                                       "," + OOEE.OQuality.OKParts +
                                                                                       "," + OOEE.OQuality.NGParts +
                                                                                       "," + OOEE.OProdPlan.TargetRun +
                                                                                       "," + OOEE.OProdPlan.TargetStop +
                                                                                       "," + OOEE.OProdPlan.TargetCycle +
                                                                                       "," + OOEE.OProdPlan.TargetPieces);
                //-------------------------------------------------------------------------------------------------------------------------
                //Close File
                StartUpSoft.Close();
                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.ProdMessages("The Production Status History registered\n", "PLC");
                return false;
            }
            
        }
        //To Read the Production History TXT file and Write the data table
        public bool ReadProdHistory(DataTable TextArea)
        {
            try
            {
                #region Production History record
                //Open the Production Data file to query the Device ID loaded
                String List;
                //Clear the record
                TextArea.Clear();
                //Query the file Log
                StreamReader _File = new StreamReader(pathProdData3);
                while ((List = _File.ReadLine()) != null)
                {
                    string[] CompleteChain = List.Split(',');
                    TextArea.Rows.Add(CompleteChain[0],
                                      CompleteChain[1],
                                      CompleteChain[2],
                                      CompleteChain[3],
                                      CompleteChain[4],
                                      CompleteChain[5],
                                      CompleteChain[6],
                                      CompleteChain[7],
                                      CompleteChain[8]);
                }
                _File.Close();
                #endregion

                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Error to read the Production History file\n", "Error");
                return false;
            }
        }
        //Write the Results of the Instrument 1: Store Measurements
        public bool WriteStoreMeasurements()
        {
            try
            {
                //Assign Date for the path file
                //Every day the CSV file name will change
                pathProdData4 = "ProductionData/StoreMeasurements";
                pathProdData4 += ("_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".csv");             
                //Verify if the current file exists with the same date
                if (!File.Exists(pathProdData4))
                {
                    //Create a File with the current date
                    File.Create(pathProdData4);
                }
                else
                {
                    //Open File with the current date
                    StreamWriter StartUpSoft = File.AppendText(pathProdData4);
                    //----------------------------------------------------------------------------------------------------------------------------------
                    StartUpSoft.WriteLine(LocalTime.PCFecha() + "," + LocalTime.PCHora() + "," + OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID] +
                                                                                           "," + OSQLServer.ProdLot_Param[(int)Prod_Lot.LotID] +
                                                                                           "," + OSQLServer.Serial +
                                                                                           "," + OMTFocus6K.DataField_MID1202[31, 5] +
                                                                                           "," + OMTFocus6K.DataField_MID1202[32, 5] +
                                                                                           "," + OMTFocus6K.Clamp +
                                                                                           "," + OMTFocus6K.ClampAngle +
                                                                                           "," + OMTFocus6K.SeatingPoint);
                    //----------------------------------------------------------------------------------------------------------------------------------          
                    //Close File
                    StartUpSoft.Close();
                }
                
                return true;
            }
            catch (Exception)
            {               
                return false;
            }
        }
        //Write the Results of the Process
        public bool WriteStoreResults()
        {
            try
            {
                //Assign Date for the path file
                //Every day the CSV file name will change
                pathProdData5 = "ProductionData/StoreResults";
                pathProdData5 += ("_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".csv");
                //Verify if the current file exists with the same date
                if (!File.Exists(pathProdData5))
                {
                    //Create a File with the current date
                    File.Create(pathProdData5);
                }
                else
                {
                    //Open File with the current date
                    StreamWriter StartUpSoft = File.AppendText(pathProdData5);
                    //----------------------------------------------------------------------------------------------------------------------------------
                    StartUpSoft.WriteLine(LocalTime.PCFecha() + "," + LocalTime.PCHora() + "," + OSQLServer.ProdLot_Param[(int)Prod_Lot.DeviceID] +
                                                                                           "," + OSQLServer.ProdLot_Param[(int)Prod_Lot.LotID] +
                                                                                           "," + OSQLServer.Serial +
                                                                                           "," + OMTFocus6K.DataField_MID1202[31, 5] +
                                                                                           "," + OMTFocus6K.DataField_MID1202[32, 5] +
                                                                                           "," + OMTFocus6K.Clamp +
                                                                                           "," + OMTFocus6K.ClampAngle +
                                                                                           "," + OMTFocus6K.SeatingPoint +
                                                                                           "," + OSQLServer.ProdLot_Param[(int)Prod_Lot.PartStatus]);
                    //----------------------------------------------------------------------------------------------------------------------------------          
                    //Close File
                    StartUpSoft.Close();
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Private

        #endregion

        #endregion

        #region Threads

        #endregion
    }
}
