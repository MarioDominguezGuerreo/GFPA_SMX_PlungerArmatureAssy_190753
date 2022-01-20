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
    public partial class StationConnectionSettings : Form
    {
        #region Variables
        private const int DeviceConnections_SIZE = 7;
        
        private bool[] deviceConnections = new bool[DeviceConnections_SIZE] { false, false, false, false, false, false, false };
        public bool[] DeviceConnections
        {
            get
            {
                return deviceConnections;
            }

            set
            {
                deviceConnections = value;
            }
        }
        #endregion

        #region Callbacks

        #endregion

        #region Objects
        //Station Connection Settings page
        public static StationConnectionSettings OStationConnSettings;
        #endregion

        public StationConnectionSettings()
        {
            InitializeComponent();
            //Station Connection Settings page
            OStationConnSettings = this;
            //Machine Status
            MachineStatus.Enabled = true;
        }

        #region Controls
        //Status of the Connection switches
        private void MachineStatus_Tick(object sender, EventArgs e)
        {
            deviceConnections[(int)Devices.PLC] = btn_PLCConn.Checked;
            deviceConnections[(int)Devices.SafetyPLC] = btn_SafetyPLCConn.Checked;
            deviceConnections[(int)Devices.Scanner] = btn_ScannerConn.Checked;
            deviceConnections[(int)Devices.Screwdriver] = btn_Instrument1Conn.Checked;
            deviceConnections[(int)Devices.VisionSensor] = btn_Instrument2Conn.Checked;
            deviceConnections[(int)Devices.Kistler] = btn_KistlerConn.Checked;
            deviceConnections[(int)Devices.ServoPress] = btn_ServoPressConn.Checked;
        }
        //Disable all devices to Connect
        private void btn_DisableFuncAll_Click(object sender, EventArgs e)
        {
            //btn_PLCConn.Checked = false;
            btn_SafetyPLCConn.Checked = false;
            btn_ScannerConn.Checked = false;
            btn_Instrument1Conn.Checked = false;
            btn_Instrument2Conn.Checked = false;
            btn_KistlerConn.Checked = false;
            btn_ServoPressConn.Checked = false;
        }
        //Enable all devices to connect
        private void btn_EnableFuncAll_Click(object sender, EventArgs e)
        {   
            //btn_PLCConn.Checked = true;
            btn_SafetyPLCConn.Checked = true;
            btn_ScannerConn.Checked = true;
            btn_Instrument1Conn.Checked = true;
            btn_Instrument2Conn.Checked = true;
            btn_KistlerConn.Checked = true;
            btn_ServoPressConn.Checked = true;
        }

        #endregion

        #region Information

        #endregion

        #region Functions

        #endregion

        #region Threads

        #endregion
    }
}
