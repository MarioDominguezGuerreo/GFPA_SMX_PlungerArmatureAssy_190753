//Mario A. Dominguez Guerrero 
//January - 2022

#region System Libraries
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

#region Project Libraries

# endregion

namespace Software
{
    class SQLDatabase
    {
        #region Variables

        #region SQL parameters
        //Station ID
        private static Int64 stationID;
        public Int64 StationID
        {
            get
            {
                return stationID;
            }

            set
            {
                stationID = value;
            }
        }
        //Serial Number of the Machine (MachineConfig)
        private static Int64 equipmentID;
        public Int64 EquipmentID
        {
            get
            {
                return equipmentID;
            }

            set
            {
                equipmentID = value;
            }
        }
        //Station name register on the DB (MachineConfig)
        private static string station;
        public string Station
        {
            get
            {
                return station;
            }

            set
            {
                station = value;
            }
        }
        //Product: Models availables into the DB
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
        //Next Station
        private static string nextStation;
        public string NextStation
        {
            get
            {
                return nextStation;
            }

            set
            {
                nextStation = value;
            }
        }
        //Product: Serial Number
        private static Int64 serial;
        public Int64 Serial
        {
            get
            {
                return serial;
            }

            set
            {
                serial = value;
            }
        }
        //Device Family
        private static string deviceFamily;
        public string DeviceFamily
        {
            get
            {
                return deviceFamily;
            }

            set
            {
                deviceFamily = value;
            }
        }
        #endregion

        #region SQL Settings
        /// Server & Database
        /// SQL Server 2008
        ///
        /*----------------------------------------------------------------------------*/
        //Cloud SQL Server
        //Data source: SAGPDBV02 (MachineConfig)
        private static string server; 
        public string Server
        {
            get
            {
                return server;
            }

            set
            {
                server = value;
            }
        }
        //Initial Catalog: APTProduction (MachineConfig)
        private static string dataBase;
        public string DataBase
        {
            get
            {
                return dataBase;
            }

            set
            {
                dataBase = value;
            }
        }
        // User and Password (permission for connection to DB) (MachineConfig)
        private static string sQLServerUser;
        public string SQLServerUser
        {
            get
            {
                return sQLServerUser;
            }

            set
            {
                sQLServerUser = value;
            }
        }
        private string SQLServerPassword = "*.mm,Tt,Y-r~*cICZ&ACcY,)M4%rJj";     
        // User and Password (permission for query) Applicatin Role (MachineConfig)
        private static string sQLqueryUser;
        public string SQLqueryUser
        {
            get
            {
                return sQLqueryUser;
            }

            set
            {
                sQLqueryUser = value;
            }
        }
        private string SQLqueryPassword = "]3~Eht,fXWJciykQ)K?T%@zd*]p4)T";
        private string MachineRoleQuery_set = "sys.sp_setapprole ";
        private string MachineRoleQuery_unset = "sys.sp_unsetapprole ";
        #endregion

        #region SQL Queries
        //Programmability
        //OFM: Prior op Check (*.odb) (MachineConfig)
        private static string tblDeviceIDs;
        public string TblDeviceIDs
        {
            get
            {
                return tblDeviceIDs;
            }

            set
            {
                tblDeviceIDs = value;
            }
        }
        //Queries
        SqlCommand DeviceIDsQuery;

        //Result History
        private static string tblResultsHistory;
        public string TblResultsHistory
        {
            get
            {
                return tblResultsHistory;
            }

            set
            {
                tblResultsHistory = value;
            }
        }
        SqlCommand ResultsHistoryQuery;
        //Master Parts Sequence View
        private static string tblMastersPartsSeqView;
        public string TblMastersPartsSeqView
        {
            get
            {
                return tblMastersPartsSeqView;
            }

            set
            {
                tblMastersPartsSeqView = value;
            }
        }
        SqlCommand MastersPartsSeqViewQuery;

        //Programmability
        #region GFPA: Prior op Check (*.odb) (MachineConfig)
        private static string tblPriorOpCheck;
        public string TblPriorOpCheck
        {
            get
            {
                return tblPriorOpCheck;
            }

            set
            {
                tblPriorOpCheck = value;
            }
        }
        private static int priorOpChk_Return;
        public int PriorOpChk_Return
        {
            get
            {
                return priorOpChk_Return;
            }

            set
            {
                priorOpChk_Return = value;
            }
        }
        //Prior Op Check Messages
        private static string pOChkMsg;
        public string POChkMsg
        {
            get
            {
                return pOChkMsg;
            }

            set
            {
                pOChkMsg = value;
            }
        }
        private static string pOChkMsg2;
        public string POChkMsg2
        {
            get
            {
                return pOChkMsg2;
            }

            set
            {
                pOChkMsg2 = value;
            }
        }
        private static string pOChkMsg3;
        public string POChkMsg3
        {
            get
            {
                return pOChkMsg3;
            }

            set
            {
                pOChkMsg3 = value;
            }
        }
        //Queries
        SqlCommand PriorOpChkQuery;
        #endregion

        #region GFPA: Check Components
        private static string tblCheckComponents;
        public string TblCheckComponents
        {
            get
            {
                return tblCheckComponents;
            }

            set
            {
                tblCheckComponents = value;
            }
        }
        private static int checkComponents_Return;
        public int CheckComponents_Return
        {
            get
            {
                return checkComponents_Return;
            }

            set
            {
                checkComponents_Return = value;
            }
        }
        //Update Device Status Messages
        private static string checkComponents_Msg;
        public string CheckComponents_Msg
        {
            get
            {
                return checkComponents_Msg;
            }

            set
            {
                checkComponents_Msg = value;
            }
        }

        //Queries
        SqlCommand CheckComponentsQuery;
        #endregion

        #region GFPA: Assign Components to Serial ID
        private static string tblAssignComponentsToSerial;
        public string TblAssignComponentsToSerial
        {
            get
            {
                return tblAssignComponentsToSerial;
            }

            set
            {
                tblAssignComponentsToSerial = value;
            }
        }
        private static int assignComponentsToSerial_Return;
        public int AssignComponentsToSerial_Return
        {
            get
            {
                return assignComponentsToSerial_Return;
            }

            set
            {
                assignComponentsToSerial_Return = value;
            }
        }
        //Update Device Status Messages
        private static string assignComponentsToSerial_Msg;
        public string AssignComponentsToSerial_Msg
        {
            get
            {
                return assignComponentsToSerial_Msg;
            }

            set
            {
                assignComponentsToSerial_Msg = value;
            }
        }

        //Queries
        SqlCommand AssignComponentsToSerialQuery;
        #endregion

        #region GFPA: Update Device Status (*.odb) (MachineConfig)
        private static string tblUpdateDeviceStatus;
        public string TblUpdateDeviceStatus
        {
            get
            {
                return tblUpdateDeviceStatus;
            }

            set
            {
                tblUpdateDeviceStatus = value;
            }
        }
        private static int updateDevice_Return;
        public int UpdateDevice_Return
        {
            get
            {
                return updateDevice_Return;
            }

            set
            {
                updateDevice_Return = value;
            }
        }
        //Update Device Status Messages
        private static string upDevStatMsg;
        public string UpDevStatMsg
        {
            get
            {
                return upDevStatMsg;
            }

            set
            {
                upDevStatMsg = value;
            }
        }        
        //Status of the process finished
        private static string statusFinished;
        public string StatusFinished
        {
            get
            {
                return statusFinished;
            }

            set
            {
                statusFinished = value;
            }
        }
 
        //Queries
        SqlCommand UpdateDeviceStatusQuery;
        #endregion

        #region GFPA:  Limits
        private static string tblLimits;
        public string TblLimits
        {
            get
            {
                return tblLimits;
            }

            set
            {
                tblLimits = value;
            }
        }
        //Return
        private static int limits_Return;
        public int Limits_Return
        {
            get
            {
                return limits_Return;
            }

            set
            {
                limits_Return = value;
            }
        }
        //Limits Messages
        private static string limits_Msg;
        public string Limits_Msg
        {
            get
            {
                return limits_Msg;
            }

            set
            {
                limits_Msg = value;
            }
        }

        SqlCommand LimitsQuery;
        #endregion

        //App Role @cookie
        private byte[] _appRoleEnableCookie;

        #endregion

        #region Limits
        //Measurements Limits
        public string pathLimitsCSVData = "ProductionData/Limits.csv";
        //Device ID List
        public string pathDeviceIDData = "ProductionData/DeviceID_List.txt";

        public const int Limits_SIZE = 34;
        public const int Master_SIZE = 10;
        public const int MasterParam_SIZE = 7;
        /// [0] = Device ID
        /// [1] = Torque Min
        /// [2] = Torque Max
        /// [3] = Limit Tol
        /// [4] = Angle Min
        /// [5] = Angle Max
        /// [6] = Spring Visual Inspection
        /// [7] = Armature Visual Inspection
        /// [8] = Clamp Torque Min
        /// [9] = Clamp Torque Max
        /// [10] = Seating point Min
        /// [11] = Seating Point max
        /// [12] = Clamp Angle Min
        /// [13] = Clamp Angle Max
        /// [14] = Standby position
        /// [15] = Ready position
        /// [16] = Test 1 position
        /// [17] = Test 2 position
        /// [18] = Screwdriving position
        /// [19] = Standby speed
        /// [20] = Ready speed
        /// [21] = Test 1 speed
        /// [22] = Test 2 speed
        /// [23] = Screwdriving speed
        /// [24] = Constant (K)
        /// [25] = Test 1: Ch X min
        /// [26] = Test 1: Ch X max
        /// [27] = Test 1: Ch Y min
        /// [28] = Test 1: Ch Y max
        /// [29] = Test 2: Ch X min
        /// [30] = Test 2: Ch X max
        /// [31] = Test 2: Ch Y min
        /// [32] = Test 2: Ch Y max

        private static string[] limits = new string[Limits_SIZE]
        { "","-0.000","0.000","0.000","0.000","0.000","000","000","000","000","000","000","000","000",
          "000.00","000.00","000.00","000.00","000.00","000","000","000","000","000","00.00","00.00","00.00","00.00","00.00","00.00","00.00","00.00","00.00","00.00"};
        public string[] Limits
        {
            get
            {
                return limits;
            }

            set
            {
                limits = value;
            }
        }
        ///Masters Parameters
        /// [0] = Serial
        /// [1] = Device ID
        /// [2] = Station
        /// [3] = Status
        /// [4] = Expected Value
        /// [5] = Sequence Number
        /// [6] = Nest
        //Target
        private static string[,] masterTarget = new string[Master_SIZE, MasterParam_SIZE];
        public string[,] MasterTarget
        {
            get
            {
                return masterTarget;
            }

            set
            {
                masterTarget = value;
            }
        }
        //Results
        private static string[,] masterResult = new string[Master_SIZE, MasterParam_SIZE];
        public string[,] MasterResult
        {
            get
            {
                return masterResult;
            }

            set
            {
                masterResult = value;
            }
        }
        /// Masters Status
        /// Omitted
        /// Waiting
        /// Done
        /// Good
        /// Fail
        private static int masterProcessStatus;
        public int MasterProcessStatus
        {
            get
            {
                return masterProcessStatus;
            }

            set
            {
                masterProcessStatus = value;
            }
        }
        #endregion

        #region Production and Lot Data
        public const int ProdLotParam_SIZE = 4;
        /// [0] = Device ID
        /// [1] = Lot ID
        /// [2] = Part Status (OK,NG)
        /// [3] = 
        private static string[] prodLot_Param = new string[ProdLotParam_SIZE]
        { "XXXX000-000","000000","00000","0"};
        public string[] ProdLot_Param
        {
            get
            {
                return prodLot_Param;
            }

            set
            {
                prodLot_Param = value;
            }
        }
        #endregion

        #endregion

        #region Callbacks

        #endregion

        #region Objects
        #endregion

        #region Controls

        #endregion

        #region Information

        #endregion

        #region Functions

        #region Public
        //SQL Server 2008: Connection management
        public string Connect()
        {
            string ConnectionParam = @"Data Source=" + server + ";Initial Catalog=" + dataBase + ";User ID=" + sQLServerUser + ";Password=" + SQLServerPassword;
            return ConnectionParam;
        }
        public void Disconnect(SqlConnection Connection)
        {
            //Close the Connection      
            Connection.Close();
        }
        //SQL Server 2008: Application Role management
        private Boolean ExecuteSetAppRole(SqlConnection Connection)
        {
            bool Succ = true;
            //Application Role: Enable
            try
            {
                //SQL command: Query permission
                SqlCommand cmd = new SqlCommand(MachineRoleQuery_set);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Connection;
                SqlParameter paramAppRoleName = new SqlParameter();
                paramAppRoleName.Direction = ParameterDirection.Input;
                paramAppRoleName.ParameterName = "@rolename";
                paramAppRoleName.Value = sQLqueryUser;
                cmd.Parameters.Add(paramAppRoleName);

                SqlParameter paramAppRolePwd = new SqlParameter();
                paramAppRolePwd.Direction = ParameterDirection.Input;
                paramAppRolePwd.ParameterName = "@password";
                paramAppRolePwd.Value = SQLqueryPassword;
                cmd.Parameters.Add(paramAppRolePwd);

                SqlParameter paramCreateCookie = new SqlParameter();
                paramCreateCookie.Direction = ParameterDirection.Input;
                paramCreateCookie.ParameterName = "@fCreateCookie";
                paramCreateCookie.DbType = DbType.Boolean;
                paramCreateCookie.Value = 1;
                cmd.Parameters.Add(paramCreateCookie);

                SqlParameter paramEncrypt = new SqlParameter();
                paramEncrypt.Direction = ParameterDirection.Input;
                paramEncrypt.ParameterName = "@encrypt";
                paramEncrypt.Value = "none";
                cmd.Parameters.Add(paramEncrypt);

                SqlParameter paramEnableCookie = new SqlParameter();
                paramEnableCookie.ParameterName = "@cookie";
                paramEnableCookie.DbType = DbType.Binary;
                paramEnableCookie.Direction = ParameterDirection.Output;
                paramEnableCookie.Size = 1000;
                cmd.Parameters.Add(paramEnableCookie);

                //Execute the query
                cmd.ExecuteNonQuery();
                SqlParameter outCookie = cmd.Parameters["@cookie"];
                // Store the enabled cookie so that approle  can be disabled with the cookie.
                _appRoleEnableCookie = (byte[])outCookie.Value;
            }
            catch (Exception e)
            {
                Succ = false;
                HMI.OForm.SystemMessages("SQL: Set App Role failed\n", "Error");
            }

            return Succ;
        }
        private Boolean ExecuteUnsetAppRole(SqlConnection Connection)
        {
            bool Succ = true;
            //Application Role: Disable
            try
            {
                SqlCommand cmd = new SqlCommand(MachineRoleQuery_unset);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = Connection;
                SqlParameter paramEnableCookie = new SqlParameter();
                paramEnableCookie.Direction = ParameterDirection.Input;
                paramEnableCookie.ParameterName = "@cookie";
                paramEnableCookie.Value = this._appRoleEnableCookie;
                cmd.Parameters.Add(paramEnableCookie);

                //Execute the query
                cmd.ExecuteNonQuery();
                _appRoleEnableCookie = null;
            }
            catch (Exception e)
            {
                Succ = false;
                HMI.OForm.SystemMessages("SQL: Unset App Role failed\n", "Error");
            }

            return Succ;
        }

        /*Production Queries*/
        /*SQLServer: Storage procedures */
        //Prior Operation Check
        public Boolean PriorOpCheck(Int64 SerialNumber,string DeviceID)
        {
            string ConnectionParam = Connect();
            try
            {
                //Connection to Server & DB
                using (SqlConnection SQLport = new SqlConnection(ConnectionParam))
                {
                    //Open the Conexion
                    SQLport.Open();
                    //Application Role Permission: Enable
                    ExecuteSetAppRole(SQLport);

                    //Query to Prior Op Check
                    #region Prior Op Check
                    try
                    {
                        //SQL command (Stored procedure)
                        using (PriorOpChkQuery = new SqlCommand(tblPriorOpCheck, SQLport))
                        {
                            //Specify the stored procedure command
                            PriorOpChkQuery.CommandType = CommandType.StoredProcedure;
                            //Add Parameters INPUT to the command
                            PriorOpChkQuery.Parameters.AddWithValue("@SerialNo", SerialNumber);
                            PriorOpChkQuery.Parameters.AddWithValue("@DeviceID", DeviceID);
                            PriorOpChkQuery.Parameters.AddWithValue("@StationName", station);
                            PriorOpChkQuery.Parameters.AddWithValue("@EquipmentID", equipmentID);

                            //Prepare the return value
                            PriorOpChkQuery.Parameters.Add("@return_value", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                            //Add Parameters OUTPUT to the command, for the Prior Op Check Messages
                            SqlParameter parameter1 = new SqlParameter();
                            parameter1.ParameterName = "@MessageText";
                            parameter1.SqlDbType = SqlDbType.VarChar;
                            parameter1.Direction = ParameterDirection.Output;
                            parameter1.Size = 500;
                            // Add the parameter to the Parameters collection. 
                            PriorOpChkQuery.Parameters.Add(parameter1);

                            //Last Station Name
                            SqlParameter parameter2 = new SqlParameter();
                            parameter2.ParameterName = "@LastStationName";
                            parameter2.SqlDbType = SqlDbType.VarChar;
                            parameter2.Direction = ParameterDirection.Output;
                            parameter2.Size = 200;

                            // Add the parameter to the Parameters collection. 
                            PriorOpChkQuery.Parameters.Add(parameter2);

                            //Part Status
                            SqlParameter parameter3 = new SqlParameter();
                            parameter3.ParameterName = "@PartStatus";
                            parameter3.SqlDbType = SqlDbType.VarChar;
                            parameter3.Direction = ParameterDirection.Output;
                            parameter3.Size = 200;

                            // Add the parameter to the Parameters collection. 
                            PriorOpChkQuery.Parameters.Add(parameter3);

                            //Execute the query            
                            PriorOpChkQuery.ExecuteNonQuery();
                            //Read the Status of the Serial number
                            priorOpChk_Return = (int)PriorOpChkQuery.Parameters["@return_value"].Value;
                            if (priorOpChk_Return == 0)
                            {
                                //Read the Message from the Prior Operation Check procedure
                                pOChkMsg = PriorOpChkQuery.Parameters["@MessageText"].Value.ToString();
                                pOChkMsg2 = PriorOpChkQuery.Parameters["@LastStationName"].Value.ToString();
                                pOChkMsg3 = PriorOpChkQuery.Parameters["@PartStatus"].Value.ToString();
                            }
                            else
                            {
                                pOChkMsg = PriorOpChkQuery.Parameters["@MessageText"].Value.ToString();
                                pOChkMsg2 = PriorOpChkQuery.Parameters["@LastStationName"].Value.ToString();
                                pOChkMsg3 = PriorOpChkQuery.Parameters["@PartStatus"].Value.ToString();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        priorOpChk_Return = 0;
                        pOChkMsg = "SQL Exception Error";
                        pOChkMsg2 = "SQL Exception Error";
                        pOChkMsg3 = "SQL Exception Error";
                        HMI.OForm.SystemMessages("SQL: Prior Op Check failed\n", "Error");
                    }
                    #endregion

                    //Application Role Permission: Disable
                    ExecuteUnsetAppRole(SQLport);
                    //Close the Connection
                    SQLport.Close();
                }
                return true;
            }
            catch (Exception)
            {
                HMI.OForm.SystemMessages("SQL: Server Connection failed\n", "Error");
                return false;
            }
        }
        //Update Device Status
        public Boolean UpdateDeviceStatus(Int64 SerialNumber, string DeviceID, string Status)
        {
            string ConnectionParam = Connect();
            try
            {
                //Connection to Server & DB
                using (SqlConnection SQLport = new SqlConnection(ConnectionParam))
                {
                    //Open the Conexion
                    SQLport.Open();
                    //Application Role Permission: Enable
                    ExecuteSetAppRole(SQLport);

                    //Query to Update Device Status
                    #region Update Device Status
                    try
                    {
                        //SQL command (Stored procedure)
                        using (UpdateDeviceStatusQuery = new SqlCommand(tblUpdateDeviceStatus, SQLport))
                        {
                            //Specify the stored procedure command
                            UpdateDeviceStatusQuery.CommandType = CommandType.StoredProcedure;
                            //Add Parameters to the command
                            UpdateDeviceStatusQuery.Parameters.AddWithValue("@SerialNo", SerialNumber);
                            UpdateDeviceStatusQuery.Parameters.AddWithValue("@DeviceID", DeviceID);
                            UpdateDeviceStatusQuery.Parameters.AddWithValue("@StationName", station);
                            UpdateDeviceStatusQuery.Parameters.AddWithValue("@EquipmentID", equipmentID);
                            UpdateDeviceStatusQuery.Parameters.AddWithValue("@Status", Status);
                            //PriorOpChkQuery.Parameters.AddWithValue("@MessageText", pOChkMsg);
                            UpdateDeviceStatusQuery.Parameters.Add("@return_value", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;
                            //Add Parameters to the command, for the Prior Op Check Messages
                            SqlParameter parameter1 = new SqlParameter();
                            //Messages text
                            parameter1.ParameterName = "@MessageText";
                            parameter1.SqlDbType = SqlDbType.VarChar;
                            parameter1.Direction = ParameterDirection.InputOutput;
                            parameter1.Size = 100;
                            parameter1.Value = upDevStatMsg;
                            // Add the parameter to the Parameters collection. 
                            UpdateDeviceStatusQuery.Parameters.Add(parameter1);
                            //Execute the query            
                            UpdateDeviceStatusQuery.ExecuteNonQuery();
                            //Read the Status of the Serial number
                            updateDevice_Return = (int)UpdateDeviceStatusQuery.Parameters["@return_value"].Value;
                            if (updateDevice_Return == 0)
                            {
                                //Read the Message from the Prior Operation Check procedure
                                upDevStatMsg = UpdateDeviceStatusQuery.Parameters["@MessageText"].Value.ToString();
                            }
                            else
                            {
                                upDevStatMsg = UpdateDeviceStatusQuery.Parameters["@MessageText"].Value.ToString();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        updateDevice_Return = 0;
                        upDevStatMsg = "SQL Exception Error";
                        HMI.OForm.SystemMessages("SQL: Update Device Status failed\n", "Error");
                    }
                    #endregion

                    //Application Role Permission: Disable
                    ExecuteUnsetAppRole(SQLport);
                    //Close the Connection
                    SQLport.Close();

                }//Close the Connection
                return true;
            }
            catch (Exception)
            {
                HMI.OForm.SystemMessages("SQL: Server Connection failed\n", "Error");
                return false;
            }
        }
        /// Device IDs
        /// Device Family is the Production Line (Ceramic, GVe, GFPA)
        public Boolean DeviceIDs(string DeviceFamily)
        {
            string ConnectionParam = Connect();
            try
            {
                //Connection to Server & DB
                using (SqlConnection SQLport = new SqlConnection(ConnectionParam))
                {
                    //Open the Conexion
                    SQLport.Open();
                    //Application Role Permission: Enable
                    ExecuteSetAppRole(SQLport);

                    //Query to Device IDs
                    #region Device IDs
                    try
                    {
                        //SQL command (Table)
                        using (DeviceIDsQuery = new SqlCommand("SELECT * FROM " + tblDeviceIDs + " WHERE DeviceFamily = @p1", SQLport))
                        {
                            //Parameters
                            DeviceIDsQuery.Parameters.Add(new SqlParameter("p1", DeviceFamily));

                            List<object[]> rowList = new List<object[]>();
                            using (SqlDataReader reader = DeviceIDsQuery.ExecuteReader())
                            {
                                // while there is another record present
                                while (reader.Read())
                                {
                                    //Get the Device ID List from Device Family DB
                                    //Add all data
                                    object[] values = new object[reader.FieldCount];
                                    reader.GetValues(values);
                                    rowList.Add(values);
                                }

                                #region Device IDs List    
                                //Clear file
                                File.WriteAllText(pathDeviceIDData, String.Empty);
                                //Open File with the current date
                                StreamWriter StartUpSoft = File.AppendText(pathDeviceIDData);
                                for (int i = 0; i < rowList.Count; i++)
                                {
                                    //Write the file
                                    StartUpSoft.WriteLine(rowList[i][0].ToString());
                                }
                                //Close File
                                StartUpSoft.Close();
                                #endregion
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        HMI.OForm.SystemMessages("SQL: Device IDs downloading failed\n", "Error");
                        return false;
                    }
                    #endregion

                    //Application Role Permission: Disable
                    ExecuteUnsetAppRole(SQLport);
                    //Close the Connection
                    SQLport.Close();

                }//Close the Connection
                return true;
            }
            catch (Exception)
            {
                HMI.OForm.SystemMessages("SQL: Server Connection failed\n", "Error");
                return false;
            }
        }
        //Limits 
        public Boolean LimitsDB(string DeviceID)
        {
            int bSucc = 0;
            string ConnectionParam = Connect();
            try
            {
                //Connection to Server & DB
                using (SqlConnection SQLport = new SqlConnection(ConnectionParam))
                {
                    //Open the Conexion
                    SQLport.Open();
                    //Application Role Permission: Enable
                    ExecuteSetAppRole(SQLport);

                    //Query to Limits
                    #region Get the Limits
                    try
                    {
                        //SQL command (Table)
                        using (LimitsQuery = new SqlCommand(tblLimits, SQLport))
                        {
                            //Specify the stored procedure command
                            LimitsQuery.CommandType = CommandType.StoredProcedure;

                            //Add Parameters to the command
                            LimitsQuery.Parameters.AddWithValue("@DeviceID", DeviceID);
                            LimitsQuery.Parameters.AddWithValue("@Category", station);
                            LimitsQuery.Parameters.Add("@return_value", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                            List<string[]> rowList = new List<string[]>();
                            //Execute the query            
                            using (SqlDataReader reader = LimitsQuery.ExecuteReader())
                            {

                                // while there is another record present
                                while (reader.Read())
                                {
                                    //Save all data
                                    string[] values = new string[reader.FieldCount];
                                    reader.GetValues(values);
                                    rowList.Add(values);

                                }
                                ///Database: Table Structure
                                ///      ParamName          ParamValue  ParamDescr 
                                ///[0]   Angle_Max          float       Angle higher limit
                                ///[1]   Angle_Min          float       Angle lower limit
                                ///[2]   ArmatureInspection int         Armature Visual Inspection
                                ///[3]   ClampAngle_Max     float       Clamp Angle higher limit    (MT Tools Program, Step 2)
                                ///[4]   ClampAngle_Min     float       Clamp Angle lower limit     (MT Tools Program, Step 2)
                                ///[5]   ClampTorque_Max    float       Clamp Torque higher limit   (MT Tools Program, Step 2)
                                ///[6]   ClampTorque_Min    float       Clamp Torque lower limit    (MT Tools Program, Step 2)
                                ///[7]   SP_Max             float       Seating Point Torque higher limit   (MT Tools Program, Step 2)
                                ///[8]   SP_Min             float       Seating Point Torque lower limit    (MT Tools Program, Step 2)
                                ///[9]   SpringInspection   int         Spring Visual Inspection
                                ///[10]  Torque_Max         float       Torque higher limit
                                ///[11]  Torque_Min         float       Torque lower limit
                                ///[12]  StandbyPos         float       Standby Position (mm), servo press
                                ///[13]  ReadyPos           float       Ready Position (mm), servo press
                                ///[14]  Test1Pos           float       Test 1 Position (mm), servo press
                                ///[15]  Test2Pos           float       Test 2 Position (mm), servo press
                                ///[16]  ScrewdrivingPos    float       Screwdriving Position (mm), servo press
                                ///[17]  StandbySpeed       float       Standby Speed (mm/s), servo press
                                ///[18]  ReadySpeed         float       Ready Speed (mm/s), servo press
                                ///[19]  Test1Speed         float       Test 1 Speed (mm/s), servo press
                                ///[20]  Test2Speed         float       Test 2 Speed (mm/s), servo press
                                ///[21]  ScrewdrivingSpeed  float       Screwdriving Speed (mm/s), servo press
                                ///[22]  ConstantK min      float       Constant Hook's Law min
                                ///[22]  ConstantK max      float       Constant Hook's Law max
                                ///[23]  Test1ChXmin        float       Displacement Sensor channel X, Kistler
                                ///[24]  Test1ChXmax        float       Displacement Sensor channel X, Kistler
                                ///[25]  Test1ChYmin        float       Load cell channel Y, Kistler
                                ///[26]  Test1ChYmax        float       Load cell channel Y, Kistler
                                ///[23]  Test2ChXmin        float       Displacement Sensor channel X, Kistler
                                ///[24]  Test2ChXmax        float       Displacement Sensor channel X, Kistler
                                ///[25]  Test2ChYmin        float       Load cell channel Y, Kistler
                                ///[26]  Test2ChYmax        float       Load cell channel Y, Kistler
                                ///      [0]                [1]         [2]

                                limits[(int)Prod_Limits.AngleMax]              = rowList[0][1];
                                limits[(int)Prod_Limits.AngleMin]              = rowList[1][1];
                                limits[(int)Prod_Limits.ArmatureVisionProg]    = rowList[2][1];
                                limits[(int)Prod_Limits.ClampMax]              = rowList[3][1];
                                limits[(int)Prod_Limits.ClampMin]              = rowList[4][1];
                                limits[(int)Prod_Limits.ClampAngleMax]         = rowList[5][1];
                                limits[(int)Prod_Limits.ClampAngleMin]         = rowList[6][1];
                                limits[(int)Prod_Limits.ConstantKmax]          = rowList[7][1];
                                limits[(int)Prod_Limits.ConstantKmin]          = rowList[8][1];
                                limits[(int)Prod_Limits.ReadyPos]              = rowList[9][1];
                                limits[(int)Prod_Limits.ReadySpeed]            = rowList[10][1];
                                limits[(int)Prod_Limits.ScrewdrivingPos]       = rowList[11][1];
                                limits[(int)Prod_Limits.ScrewdrivingSpeed]     = rowList[12][1];
                                limits[(int)Prod_Limits.SeatingPointMax]       = rowList[13][1];
                                limits[(int)Prod_Limits.SeatingPointMin]       = rowList[14][1];
                                limits[(int)Prod_Limits.SpringVisionProg]      = rowList[15][1];
                                limits[(int)Prod_Limits.StandbyPos]            = rowList[16][1];
                                limits[(int)Prod_Limits.StandbySpeed]          = rowList[17][1];
                                limits[(int)Prod_Limits.Test1ChXmax]           = rowList[18][1];
                                limits[(int)Prod_Limits.Test1ChXmin]           = rowList[19][1];
                                limits[(int)Prod_Limits.Test1ChYmax]           = rowList[20][1];
                                limits[(int)Prod_Limits.Test1ChYmin]           = rowList[21][1];
                                limits[(int)Prod_Limits.Test1Pos]              = rowList[22][1];
                                limits[(int)Prod_Limits.Test1Speed]            = rowList[23][1];
                                limits[(int)Prod_Limits.Test2ChXmax]           = rowList[24][1];
                                limits[(int)Prod_Limits.Test2ChXmin]           = rowList[25][1];
                                limits[(int)Prod_Limits.Test2ChYmax]           = rowList[26][1];
                                limits[(int)Prod_Limits.Test2ChYmin]           = rowList[27][1];
                                limits[(int)Prod_Limits.Test2Pos]              = rowList[28][1];
                                limits[(int)Prod_Limits.Test2Speed]            = rowList[29][1];
                                limits[(int)Prod_Limits.TorqueMax]             = rowList[30][1];
                                limits[(int)Prod_Limits.TorqueMin]             = rowList[31][1];    
                            }

                            //Read the Status of the query
                            limits_Return = (int)LimitsQuery.Parameters["@return_value"].Value;
                        }
                    }
                    catch (Exception e)
                    {
                        bSucc++;
                        HMI.OForm.SystemMessages("SQL: Limits downloading failed\n", "Error");
                    }
                    #endregion

                    //Application Role Permission: Disable
                    ExecuteUnsetAppRole(SQLport);
                    //Close the Connection
                    SQLport.Close();

                }//Close the Connection
            }
            catch (Exception e)
            {
                bSucc++;
                HMI.OForm.SystemMessages("SQL: Server Connection failed\n", "Error");
            }

            //There was any error?
            if (bSucc > 0)
            {
                return false;
            }
            else
                return true;

        }
        //Master Parts Sequence View
        public Boolean MasterPartsSequence(string DeviceID)
        {
            string ConnectionParam = Connect();
            try
            {
                //Connection to Server & DB
                using (SqlConnection SQLport = new SqlConnection(ConnectionParam))
                {
                    //Open the Conexion
                    SQLport.Open();
                    //Application Role Permission: Enable
                    ExecuteSetAppRole(SQLport);

                    //Query the Master Part Sequence
                    #region Master Parts Sequence
                    try
                    {
                        //SQL command (Table)
                        using (MastersPartsSeqViewQuery = new SqlCommand("SELECT * FROM " + tblMastersPartsSeqView + " WHERE DeviceID = @p1 AND Station = @p2", SQLport))
                        {
                            //Parameters
                            MastersPartsSeqViewQuery.Parameters.Add(new SqlParameter("p1", DeviceID));
                            MastersPartsSeqViewQuery.Parameters.Add(new SqlParameter("p2", station));

                            List<object[]> rowList = new List<object[]>();
                            using (SqlDataReader reader = MastersPartsSeqViewQuery.ExecuteReader())
                            {

                                // while there is another record present
                                while (reader.Read())
                                {
                                    //Add all data
                                    object[] values = new object[reader.FieldCount];
                                    reader.GetValues(values);
                                    rowList.Add(values);
                                }

                                LotManager.OLotManager.OTableMasters.Clear();

                                #region Save all the Masters Parameters
                                for (int i = 0; i < rowList.Count; i++)
                                {
                                    for (int k = 0; k < MasterParam_SIZE; k++)
                                    {
                                        masterTarget[i, k] = rowList[i][k].ToString();
                                    }
                                    LotManager.OLotManager.OTableMasters.Rows.Add(masterTarget[i, 0],
                                                                                    masterTarget[i, 1],
                                                                                    masterTarget[i, 2],
                                                                                    masterTarget[i, 3],
                                                                                    masterTarget[i, 4],
                                                                                    masterTarget[i, 5],
                                                                                    masterTarget[i, 6]);
                                }
                                #endregion
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        HMI.OForm.SystemMessages("SQL: Master Parts Sequence failed\n", "Error");
                        return false;
                    }
                    #endregion

                    //Application Role Permission: Disable
                    ExecuteUnsetAppRole(SQLport);
                    //Close the Connection
                    SQLport.Close();

                }//Close the Connection
                return true;
            }
            catch (Exception)
            {
                HMI.OForm.SystemMessages("SQL: Server Connection failed\n", "Error");
                return false;
            }
        }
        //Result History
        public Boolean ResultsDB(Int64 SerialNumber, string DeviceID, string LotID, string Torque, string Angle, string Clamp,  string ClampAngle, string SP, string PartStatus)
        {
            bool bSucc = false;
            string ConnectionParam = Connect();
            try
            {
                //Connection to Server & DB
                using (SqlConnection SQLport = new SqlConnection(ConnectionParam))
                {
                    //Open the Conexion
                    SQLport.Open();
                    //Application Role Permission: Enable
                    ExecuteSetAppRole(SQLport);

                    //Query to Spring Force test history
                    #region Save the Spring Force Test Results
                    try
                    {
                        //SQL command (Table)
                        using (ResultsHistoryQuery = new SqlCommand("INSERT INTO " + tblResultsHistory + " (SerialNo, DeviceID, TestTime, EquipmentID , LotID, PartStatus , Torque, Angle, Clamp, SeatingPoint, ClampAngle) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)", SQLport))
                        {
                            //Parameters
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p1", SerialNumber));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p2", DeviceID));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p3", DateTime.Now));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p4", equipmentID));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p5", LotID));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p6", PartStatus));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p7", Convert.ToDouble(Torque)));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p8", Convert.ToDouble(Angle)));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p9", Convert.ToDouble(Clamp)));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p10", Convert.ToDouble(SP)));
                            ResultsHistoryQuery.Parameters.Add(new SqlParameter("p11", Convert.ToDouble(ClampAngle)));

                            //Execute Query
                            ResultsHistoryQuery.ExecuteNonQuery();
                        }
                        bSucc = true;
                    }
                    catch (Exception e)
                    {
                        bSucc = false;
                        HMI.OForm.SystemMessages("SQL: Store Results History failed\n", "Error");
                    }
                    #endregion

                    //Application Role Permission: Disable
                    ExecuteUnsetAppRole(SQLport);
                    //Close the Connection
                    SQLport.Close();

                }//Close the Connection
                bSucc = true;
            }
            catch (Exception e)
            {
                bSucc = false;
                HMI.OForm.SystemMessages("SQL: Server Connection failed\n", "Error");
            }

            return bSucc;
        }
        //Components Lot Traceability
        //Check Components
        public Boolean CheckComponents(string DeviceID)
        {
            string ConnectionParam = Connect();
            try
            {
                //Connection to Server & DB
                using (SqlConnection SQLport = new SqlConnection(ConnectionParam))
                {
                    //Open the Conexion
                    SQLport.Open();
                    //Application Role Permission: Enable
                    ExecuteSetAppRole(SQLport);

                    //Query to Check Components
                    #region Check Components
                    try
                    {
                        //SQL command (Stored procedure)
                        using (CheckComponentsQuery = new SqlCommand(tblCheckComponents, SQLport))
                        {
                            //Specify the stored procedure command
                            CheckComponentsQuery.CommandType = CommandType.StoredProcedure;
                            //Add Parameters INPUT to the command
                            CheckComponentsQuery.Parameters.AddWithValue("@DeviceID", DeviceID);
                            CheckComponentsQuery.Parameters.AddWithValue("@StationID", stationID);
                            CheckComponentsQuery.Parameters.AddWithValue("@EquipmentID", 100100);    //Check Components query it doesn't use the Equipment ID of the machine or station

                            //Prepare the return value
                            CheckComponentsQuery.Parameters.Add("@return_value", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                            //Add Parameters OUTPUT to the command, for the Check Components Messages
                            SqlParameter parameter1 = new SqlParameter();
                            parameter1.ParameterName = "@MessageText";
                            parameter1.SqlDbType = SqlDbType.VarChar;
                            parameter1.Direction = ParameterDirection.Output;
                            parameter1.Size = 500;
                            // Add the parameter to the Parameters collection. 
                            CheckComponentsQuery.Parameters.Add(parameter1);

                            //Execute the query            
                            CheckComponentsQuery.ExecuteNonQuery();
                            //Read the Status of the Serial number
                            checkComponents_Return = (int)CheckComponentsQuery.Parameters["@return_value"].Value;
                            if (checkComponents_Return == 0)
                            {
                                //Read the Message from the Prior Operation Check procedure
                                checkComponents_Msg = CheckComponentsQuery.Parameters["@MessageText"].Value.ToString();
                            }
                            else
                            {
                                checkComponents_Msg = CheckComponentsQuery.Parameters["@MessageText"].Value.ToString();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        checkComponents_Return = 0;
                        checkComponents_Msg = "SQL Exception Error";
                        HMI.OForm.SystemMessages("SQL: Check Component failed\n", "Error");
                    }
                    #endregion

                    //Application Role Permission: Disable
                    ExecuteUnsetAppRole(SQLport);
                    //Close the Connection
                    SQLport.Close();
                }
                return true;
            }
            catch (Exception)
            {
                HMI.OForm.SystemMessages("SQL: Server Connection failed\n", "Error");
                return false;
            }
        }
        //Assign Components to Serial ID
        public Boolean AssignComponentsToSerial(Int64 SerialNumber, string DeviceID, string LotID)
        {
            string ConnectionParam = Connect();
            try
            {
                //Connection to Server & DB
                using (SqlConnection SQLport = new SqlConnection(ConnectionParam))
                {
                    //Open the Conexion
                    SQLport.Open();
                    //Application Role Permission: Enable
                    ExecuteSetAppRole(SQLport);

                    //Query to Assign Components To Serial
                    #region AssignComponentsToSerial
                    try
                    {
                        //SQL command (Stored procedure)
                        using (AssignComponentsToSerialQuery = new SqlCommand(tblAssignComponentsToSerial, SQLport))
                        {
                            //Specify the stored procedure command
                            AssignComponentsToSerialQuery.CommandType = CommandType.StoredProcedure;
                            //Add Parameters to the command
                            AssignComponentsToSerialQuery.Parameters.AddWithValue("@Station", stationID);
                            AssignComponentsToSerialQuery.Parameters.AddWithValue("@EquipmentID", 100100); //Check Components query it doesn't use the Equipment ID of the machine or station
                            AssignComponentsToSerialQuery.Parameters.AddWithValue("@DeviceID", DeviceID);
                            AssignComponentsToSerialQuery.Parameters.AddWithValue("@SerialNo", SerialNumber);
                            AssignComponentsToSerialQuery.Parameters.AddWithValue("@LotID", LotID);
                            AssignComponentsToSerialQuery.Parameters.AddWithValue("@AssignEquipmentID", equipmentID);
                            //PriorOpChkQuery.Parameters.AddWithValue("@MessageText", pOChkMsg);
                            AssignComponentsToSerialQuery.Parameters.Add("@return_value", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;
                            //Add Parameters to the command, for the Prior Op Check Messages
                            SqlParameter parameter1 = new SqlParameter();
                            //Messages text
                            parameter1.ParameterName = "@MessageText";
                            parameter1.SqlDbType = SqlDbType.VarChar;
                            parameter1.Direction = ParameterDirection.Output;
                            parameter1.Size = 100;
                            // Add the parameter to the Parameters collection. 
                            AssignComponentsToSerialQuery.Parameters.Add(parameter1);
                            //Execute the query            
                            AssignComponentsToSerialQuery.ExecuteNonQuery();
                            //Query's results
                            assignComponentsToSerial_Return = (int)AssignComponentsToSerialQuery.Parameters["@return_value"].Value;
                            //Query's messages
                            assignComponentsToSerial_Msg = AssignComponentsToSerialQuery.Parameters["@MessageText"].Value.ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        assignComponentsToSerial_Return = 0;
                        assignComponentsToSerial_Msg = "SQL Exception Error";
                        HMI.OForm.SystemMessages("SQL: Assign Components To Serial failed\n", "Error");
                    }
                    #endregion

                    //Application Role Permission: Disable
                    ExecuteUnsetAppRole(SQLport);
                    //Close the Connection
                    SQLport.Close();

                }//Close the Connection
                return true;
            }
            catch (Exception)
            {
                HMI.OForm.SystemMessages("SQL: Server Connection failed\n", "Error");
                return false;
            }
        }
        #endregion

        #region Public

        #region CSV file: Load Limits
        public bool LimitsCSV(string _deviceID)
        {
            bool bSucc = false;
            string Info;
            //Query the file Log
            StreamReader _File = new StreamReader(pathLimitsCSVData);
            while ((Info = _File.ReadLine()) != null)
            {
                //Verify the text line read it
                string[] CompleteChain = Info.Split(',');
                //Search the Device ID selected
                string DeviceID = CompleteChain[0];
                if (DeviceID == _deviceID)
                {
                    //Select the Limits
                    for (int i = 0; i < Limits_SIZE; i++)
                    {
                        limits[i] = CompleteChain[i];
                    }
                    bSucc = true;
                }
            }
            _File.Close();

            return bSucc;
        }
        #endregion

        #endregion

        #endregion

        #region Threads

        #endregion
    }
}
