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
        //PCON-CB: Servomotor controller
        IAI_PCON OPCON = new IAI_PCON();
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

            /// Factory limits of the Servomotor
            /// Position            =   0.15 to 200.15  mm
            /// Speed               =   1 to 210        mm/s
            /// Acc/Decc            =   0.01 to 1.00    G
            /// Position Band       =   0.01 to 200.30  mm/s2
            /// Press Current Limit =   0 to 175   
            /// ----------------------------------------------
            lb_PCON_StrokeMin.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.PosTargetmin];
            lb_PCON_StrokeMax.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.PosTargetmax];
            lb_PCON_PosBandMin.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.PosBandmin];
            lb_PCON_PosBandMax.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.PosBandmax];
            lb_PCON_SpeedMin.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.SpeedTargetmin];
            lb_PCON_SpeedMax.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.SpeedTargetmax];
            lb_PCON_AccDeccMin.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.AccDeccTargetmin];
            lb_PCON_AccDeccMax.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.AccDeccTargetmax];
            lb_PCON_PressCurrLimitMin.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.PressCurrLimitmin];
            lb_PCON_PressCurrLimitMax.Text = OPCON.Factory_Limits[(int)PCON_Factory_Limits.PressCurrLimitmax];
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
