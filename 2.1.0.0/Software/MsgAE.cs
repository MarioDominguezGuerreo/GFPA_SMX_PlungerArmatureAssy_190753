//Mario A. Dominguez Guerrero 
//July - 2020

#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#endregion

#region Project Libraries

# endregion

namespace Software
{    
    class MsgAE
    {
        #region Variables

        #region Press Alarms
        private static string[] pressAlarms = new string[]
         {
            "OK",
            "Press Timeout Down",
            "Press Timeout Up"
         };

        public string[] PressAlarms
        {
            get
            {
                return pressAlarms;
            }
        }
        #endregion

        #region Locker Alarms
        private static string[] lockerAlarms = new string[]
         {
            "OK",
            "Locker Timeout Retract",
            "Locker Timeout Extend"
         };

        public string[] LockerAlarms
        {
            get
            {
                return lockerAlarms;
            }
        }
        #endregion

        #region Safety Alarms
        private static string[] safetyAlarms = new string[]
         {
            "OK",
            "Something happend into the SICK module",
            "The Safety Curtain is activated during the cycle running",
            "The system has not air pressure or it isn't enough"
         };

        public string[] SafetyAlarms
        {
            get
            {
                return safetyAlarms;
            }
        }
        #endregion

        #region Lot Manager Alarms & Events
        private static string[] lotManagerAlarms = new string[]
         {
            "Por favor agregue un lote de produccion, para poder accesar a modo automatico",
         };

        public string[] LotManagerAlarms
        {
            get
            {
                return lotManagerAlarms;
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

        #endregion

        #region Private

        #endregion

        #endregion

        #region Threads

        #endregion
    }
}