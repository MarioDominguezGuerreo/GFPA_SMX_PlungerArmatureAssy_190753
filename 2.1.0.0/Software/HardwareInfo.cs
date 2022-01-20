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
#endregion

#region Project Libraries

# endregion

namespace Software
{
    public partial class pnl_HardwareInfo : Form
    {
        #region Variables

        #endregion

        #region Callbacks

        #endregion

        #region Threads

        #endregion

        #region Objects

        #endregion

        public pnl_HardwareInfo()
        {
            InitializeComponent();

            #region CSV Settings
            txt_MachineName.Text = HMI.OForm.CSVfile[2];
            txt_MachineSerial.Text = HMI.OForm.CSVfile[7];
            txt_MachineED.Text = HMI.OForm.CSVfile[6];
            txt_MachinePL.Text = HMI.OForm.CSVfile[5];
            txt_SoftwareDate.Text = HMI.OForm.CSVfile[1];
            txt_SoftwareVer.Text = HMI.OForm.CSVfile[0];
            #endregion
        }

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
    }
}
