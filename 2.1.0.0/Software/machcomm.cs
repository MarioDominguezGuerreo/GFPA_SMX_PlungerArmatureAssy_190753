//Mario A. Dominguez Guerrero 
//July - 2020

#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

#region project Libraries

#endregion

namespace Software
{
    class machcomm
    {
        #region Variables
        //Result Codes
        public string[] ResultCodes = new string[]
        {
            "No Result",
            "Good",
            "No Read 2D Code",
            "Prior Op",
            "Low Limit Torque",         //Low Limit Peak/Final Torque
            "High Limit Torque",        //High Limit Peak/Final Torque
            "Low Limit  Angle",         //Low Limit Peak/Final Angle
            "High Limit Angle",         //High Limit Peak/Final Angle
            "Error Measurement",
            "Machine Failure",
            "Check Components",
            "Assign Components",
            "Low Limit Clamp",          //Low Limit Clamp Torque
            "High Limit Clamp",         //Low Limit Clamp Torque
            "Low Limit SP",             //Low Limit Seating Point Torque
            "High Limit SP",            //High Limit Seating Point Torque
            "Low Clamp Angle",          //Low Limit Clamp Angle
            "High Clamp Angle",         //Low Limit Clamp Angle
        };
        #endregion

        #region Functions
        //Manage the Result Codes
        public string ResultCode(int Code)
        {
            string msg = ResultCodes[Code];
            return msg;
        }
        #endregion
    }

    #region LotManager
    enum LotManager_parameters
    {
        LotActive
    }
    #endregion

    #region Production Process Parameters

    enum ProcessNumber
    {
        Process1,
        Process2,
        Process3
    }

    enum StepNumber
    {
        Step1,
        Step2,
        Step3A,
        Step4,
        Step5,
        Step6,
        Step7,
        Step8,
        Step3B,
        Step_CheckComponents,
        Step_AssignComponents,
        StepFDTest1,
        StepFDTest2,
        StepFDConstK
    }

    enum StatusNo
    {
        NG,
        OK,
        Omitted,
        Rework,
        Wait,
        InProcess,
        Done
    }

    #region Master Parameters
    enum MasterProcessStatus
    {
        Ommitted,
        Waiting,
        Done,
        Fail,
        Good,
    }

    ///Masters
    /// [0] = Serial
    /// [1] = Device ID
    /// [2] = Station
    /// [3] = Status
    /// [4] = Expected Value
    /// [5] = Sequence Number
    /// [6] = Nest
    enum MastersParameters
    {
        SequenceNo,
        Satus,
        ExpecValue
    }
    #endregion

    //Result Codes for Production
    enum ResultCodes
    {
        NoResult,
        Good,
        NoRead2D,
        PriorOp,
        LowLimitTorque,
        HighLimitTorque,
        LowLimitAngle,
        HighLimiAngle,
        ErrorMeas,
        MachineFailure,
        CheckComponents,
        AssignComponents,
        LowLimitClamp,
        HighLimitClamp,
        LowLimitSP,
        HighLimitSP,
        LowClampAngle,
        HighClampAngle
    }
    #endregion

    #region CSV File Machine Settings
    enum CSV_MachConfig
    {
        SoftwareVersion,
        SoftwareVersionDate,
        MachineName,
        StationID,
        EquipmentID,
        PartListID,
        ElectricalDrawingID,
        SerialMachine,
        SQLServerName,
        SQLDBName,
        SQLUser,
        AppRoleUser,
        PriorOpCheck,
        UpdateDeviceStatus,
        DeviceIDTable,
        StationName,
        PLCEnable,
        PLCIP,
        PLCProgramVersion,
        PLCProgramVersionDate,
        PLCProgramName,
        SafetyPLCEnable,
        SafetyPLCIP,
        SafetyPLCPort,
        SafetyPLCProgramVersion,
        SafetyPLCProgramDateVersion,
        SafetyPLCProgramName,
        PCName,
        PCIP,
        PCDNS,
        ScannerIP,
        ScannerPort,
        Instrument1IP,
        Instrument1Port,
        MachineNumber,
        Instrument2IP,
        Instrument2Port,
        LimitsOperParams,
        ResultsHistory,
        MasterPartsSeqView,
        DeviceFamily,
        CheckComponents,
        AssignComponents,
        KistlerIP,
        PCONIP
    }

    public enum Devices
    {
        PLC,
        SafetyPLC,
        Scanner,
        Screwdriver,
        VisionSensor,
        Kistler,
        ServoPress
    }

    #endregion

    #region SQL Server & Batabase Parameters
    //Lot & Production
    enum Prod_Lot
    {
        DeviceID,
        LotID,
        PartStatus
    }
    //Instrument 1: Limits
    enum Instrument_1
    {
        DeviceID,
        TorqueMin,
        TorqueMax,
        LimitTol,
        AngleMin,
        AngleMax,
        SpringVisionProg,
        ArmatureVisionProg,
        ClampMin,
        ClampMax,
        SeatingPointMin,
        SeatingPointMax,
        ClampAngleMin,
        ClampAngleMax,
        StandbyPos,
        ReadyPos,
        Test1Pos,
        Test2Pos,
        ScrewdrivingPos,
        StandbySpeed,
        ReadySpeed,
        Test1Speed,
        Test2Speed,
        ScrewdrivingSpeed,
        ConstantK,
        Test1ChXmin,
        Test1ChXmax,
        Test1ChYmin,
        Test1ChYmax,
        Test2ChXmin,
        Test2ChXmax,
        Test2ChYmin,
        Test2ChYmax
    }
    enum Instrument_2
    {
        DeviceID,
        ProgNo
    }

    #endregion

    #region PLC & Safety PLC Parameters

    /// PLC Communicacion parameters[#]
    /// 0 = Communication Status (Disconnected = 0, Connected = 1)
    /// 1 = Read / Write Coils, (Read = 0, Write = 1)
    /// 2 = Control (Disable = 0 / Enable = 1)
    enum PLC_Comm
    {
        CommStatus,
        RW_Coils,
        Control
    }


    #endregion

    #region Scanner
    /// 0 = Communication Status (Disconnected = 0, Connected = 1)
    /// 1 = 
    /// 2 = Control (Disable = 0 / Enable = 1)
    enum Scanner_Comm
    {
        CommStatus,
        RW_Coils,
        Control
    }

    #endregion

    #region Instruments

    enum Instrument_Comm
    {
        CommStatus,
        RW_Coils,
        Control
    }

    ///[0]  CMD
    ///[1]
    ///[2]  Final Result
    ///[3]  Tool    0
    ///[4]  Result  0
    ///[5]  Measure 0
    ///[6]  Tool    1
    ///[7]  Result  1
    ///[8]  Measure 1
    ///[9]  Tool    2
    ///[10] Result  2
    ///[11] Measure 2
    ///
    /// [0],[1]  ,[2],[3],[4],[5]  ,[6],[7],[8]  ,[9],[10],[11]
    /// RT  00000 NG  00  NG  00000 01  NG  00000 02  NG    00000
    enum VisionSensor_Results
    {
        CMD,
        None,
        FinalResult,
        Tool0,
        Result0,
        Measure0,
        Tool1,
        Result1,
        Measure1,
        Tool2,
        Result2,
        Measure2,
    }

    #region Kistler
    /// 0 = Communication Status (Disconnected = 0, Connected = 1)
    /// 1 = Read / Write Coils, (Read = 0, Write = 1)
    /// 2 = Control (Disable = 0 / Enable = 1)
    enum Kistler_Parameters
    {
        CommStatus,
        RW,
        Control
    }
    #endregion

    #region PCON
    /// 0 = Communication Status (Disconnected = 0, Connected = 1)
    /// 1 = Read / Write Coils, (Read = 0, Write = 1)
    /// 2 = Control (Disable = 0 / Enable = 1)
    enum PCON_Parameters
    {
        CommStatus,
        RW,
        Control
    }

    #region Parameter No. 84: Half Direct Value Mode (Number of Occupied Bytes: 16)

    //PLC/PC -> PCON Inputs: Control Signal
    enum PC_To_PCON_Reg
    {
        DSTR,       //Positioning command: A move command is issued when this signal turns ON.
        HOME,       //Home return: A home-return command is issued when this signal turns ON.
        STP,        //Pause: A pause command is issued when this signal turns ON.
        RES,        //Reset: A reset is performed when this signal turns ON.
        SON,        //Servo ON command: The servo turns ON when this signal turns ON.
        JISL,       //Jog/inch switching: Jog operation is performed when this signal is OFF, and inch
                    //operation is performed when the signal is ON.
        JVEL,       //Jog-speed/inch-distance switching: The values set in parameter No. 26, “Jog speed” and parameter
                    //No. 48, “Inch distance” are used when this signal is OFF, and the values set in parameter No. 47, “Jog speed 2” and
                    //parameter No. 49, “Inch distance 2” are used when the signal is ON.
        JOG_REV,    //Jog -: "ON” for movement in the home direction
        JOG_FWD,    //Jog +: “ON” for movement in the opposite direction of home
        disable_9,  //Unavailable
        disable_10, //Unavailable
        disable_11, //Unavailable
        PUSH,       //Pressing specification: Positioning operation is performed when this signal is OFF, 
                    //and pressing operation is performed when the signal is ON.
        DIR,        //Pressing direction specification: “OFF” for the direction reducing the positioning band from the
                    //target position,“ON” for the direction adding the positioning band to the target position
        RMOD,       //Operating mode selector: The AUTO mode is selected when this
                    //signal is OFF, and the MANU mode is selected when the signal is ON.
        BKRL        //Forced brake release: When it is turned ON, the brake is released.
    }

    //PLC/PC <- PCON Outputs: Status Signal
    enum PCON_Reg_To_PC
    {
        PEND,       //Positioning completion signal: This signal turns ON when positioning is completed.
        HEND,       //Home return completion: This signal turns ON when home return is completed.
        MOVE,       //Moving signal: This signal remains ON while the actuator is moving.
        ALM,        //Alarm: This signal turns ON when an alarm occurs.
        SV,         //Operation preparation end: This signal turns ON when the servo turns ON.
        PSEL,       //Pressing and a Miss: This signal turns ON when the actuator missed the work part in pressing operation.
        disable_6,  //Unavailable
        disable_7,  //Unavailable
        RMDS,       //Position Zone
        disable_9,  //Unavailable
        disable_10, //Unavailable
        disable_11, //Unavailable
        ZONE1,      //Operation preparation end
        ZONE2,      //Emergency stop
        PWR,        //Alarm
        EMGS,       //Unavailable
    }
    #endregion

    #endregion

    #endregion
}
