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
    public partial class pnl_SoftwareInfo : Form
    {
        #region Variables

        #endregion

        #region Callbacks

        #endregion

        #region Threads

        #endregion

        #region Objects

        #endregion

        public pnl_SoftwareInfo()
        {
            InitializeComponent();

            #region CSV Settings
            //VS 2015 Software Info
            txt_SoftwareDate.Text = HMI.OForm.CSVfile[1];
            txt_SoftwareVer.Text = HMI.OForm.CSVfile[0];
            //GX3 Works Info
            txt_GX3ProgVer.Text = HMI.OForm.CSVfile[18];
            txt_GX3ProgDateVer.Text = HMI.OForm.CSVfile[19];
            txt_GX3ProgName.Text = HMI.OForm.CSVfile[20];
            //Flexi Soft Designer Info
            txt_FSPProgVer.Text = HMI.OForm.CSVfile[24];
            txt_FSPProgDateVer.Text = HMI.OForm.CSVfile[25];
            txt_FSPProgName.Text = HMI.OForm.CSVfile[26];
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
