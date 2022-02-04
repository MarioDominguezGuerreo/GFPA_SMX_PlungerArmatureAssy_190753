using System;
using System.Windows.Forms;

namespace Software
{
    partial class HMI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMI));
            this.Footer = new System.Windows.Forms.StatusStrip();
            this.lb_Hora = new System.Windows.Forms.ToolStripStatusLabel();
            this.lb_Fecha = new System.Windows.Forms.ToolStripStatusLabel();
            this.Header = new System.Windows.Forms.MenuStrip();
            this.lb_Station = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_ConnSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Initialize = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Shutdown = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_UserAccess = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_HardwareInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_SoftwareInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_ProductInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Home = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.CheckBox();
            this.btn_ManualAuto = new System.Windows.Forms.CheckBox();
            this.btn_Reset = new System.Windows.Forms.CheckBox();
            this.btn_LotManager = new System.Windows.Forms.Button();
            this.btn_Diagnostics = new System.Windows.Forms.Button();
            this.btn_Production = new System.Windows.Forms.Button();
            this.btn_Engineering = new System.Windows.Forms.Button();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pnl_Messages = new System.Windows.Forms.TabControl();
            this.tab_MsgProdSys = new System.Windows.Forms.TabPage();
            this.txt_ProductionMsg = new System.Windows.Forms.RichTextBox();
            this.txt_SystemMsg = new System.Windows.Forms.RichTextBox();
            this.tab_MsgAlarms = new System.Windows.Forms.TabPage();
            this.dataGridView_Alarms = new System.Windows.Forms.DataGridView();
            this.LocalClock = new System.Windows.Forms.Timer(this.components);
            this.MachineStatus = new System.Windows.Forms.Timer(this.components);
            this.ProductionProcess = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gauge_CycleProgress = new Telerik.WinControls.UI.Gauges.RadRadialGauge();
            this.txt_NoScrewdriving = new System.Windows.Forms.Label();
            this.txt_StopProcess = new System.Windows.Forms.Label();
            this.txt_MsgTryAgain = new System.Windows.Forms.Label();
            this.txt_InstallPlungerArmtr = new System.Windows.Forms.Label();
            this.txt_Screwdrinving = new System.Windows.Forms.Label();
            this.txt_MsgCheckScanner = new System.Windows.Forms.Label();
            this.txt_CycleResult = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_MachineMode = new System.Windows.Forms.Label();
            this.txt_MachineStatus = new System.Windows.Forms.Label();
            this.Process_Arc = new Telerik.WinControls.UI.Gauges.RadialGaugeArc();
            this.radialGaugeArc10 = new Telerik.WinControls.UI.Gauges.RadialGaugeArc();
            this.radialGaugeSingleLabel5 = new Telerik.WinControls.UI.Gauges.RadialGaugeSingleLabel();
            this.lb_Scan = new System.Windows.Forms.Label();
            this.lb_PriorOpCheck = new System.Windows.Forms.Label();
            this.lb_StoreMeas = new System.Windows.Forms.Label();
            this.lb_UpdateDeviceStatus = new System.Windows.Forms.Label();
            this.lb_Screwdriving = new System.Windows.Forms.Label();
            this.txt_Scan = new System.Windows.Forms.Label();
            this.txt_Measurement1 = new System.Windows.Forms.Label();
            this.txt_DBInsertMeas = new System.Windows.Forms.Label();
            this.lb_PartPresent = new MetroFramework.Controls.MetroCheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_StationNo = new System.Windows.Forms.TextBox();
            this.txt_StationName = new System.Windows.Forms.TextBox();
            this.lb_PressUp = new MetroFramework.Controls.MetroCheckBox();
            this.lb_PressDown = new MetroFramework.Controls.MetroCheckBox();
            this.lb_SafetySys = new MetroFramework.Controls.MetroCheckBox();
            this.lb_AirSupply = new MetroFramework.Controls.MetroCheckBox();
            this.lb_ContinueSequence = new MetroFramework.Controls.MetroCheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txt_MeasAngle = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_MeasTorque = new System.Windows.Forms.Label();
            this.img_SystemProd = new System.Windows.Forms.PictureBox();
            this.img_AlarmsEvents = new System.Windows.Forms.PictureBox();
            this.txt_DBInsertResults = new System.Windows.Forms.Label();
            this.lb_StoreResults = new System.Windows.Forms.Label();
            this.txt_Measurement2 = new System.Windows.Forms.Label();
            this.lb_VisualInspSpring = new System.Windows.Forms.Label();
            this.lb_VisualInspPlunger = new System.Windows.Forms.Label();
            this.lb_ChkComp = new System.Windows.Forms.Label();
            this.lb_AssignComp = new System.Windows.Forms.Label();
            this.txt_ChkComp = new System.Windows.Forms.TextBox();
            this.txt_PriorOpCheck = new System.Windows.Forms.TextBox();
            this.txt_AssignComp = new System.Windows.Forms.TextBox();
            this.txt_UpdateDeviceStatus = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_MeasSP = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_MeasClamp = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_MeasClampAngle = new System.Windows.Forms.Label();
            this.txt_DisplayChX = new System.Windows.Forms.Label();
            this.txt_DisplayChY = new System.Windows.Forms.Label();
            this.txt_PCON_CurrPos = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lb_Test2 = new System.Windows.Forms.Label();
            this.lb_Test1 = new System.Windows.Forms.Label();
            this.txt_Test1ChYMeas = new System.Windows.Forms.Label();
            this.txt_Test2ChYMeas = new System.Windows.Forms.Label();
            this.lb_Measurements = new System.Windows.Forms.Label();
            this.lb_ConstantK = new System.Windows.Forms.Label();
            this.txt_KistlerConstK = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_Test1ChXMeas = new System.Windows.Forms.Label();
            this.txt_Test2ChXMeas = new System.Windows.Forms.Label();
            this.lb_SafetyCurtains = new MetroFramework.Controls.MetroCheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.Footer.SuspendLayout();
            this.Header.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.pnl_Messages.SuspendLayout();
            this.tab_MsgProdSys.SuspendLayout();
            this.tab_MsgAlarms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Alarms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gauge_CycleProgress)).BeginInit();
            this.gauge_CycleProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_SystemProd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_AlarmsEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Footer.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Footer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lb_Hora,
            this.lb_Fecha});
            this.Footer.Location = new System.Drawing.Point(0, 872);
            this.Footer.Name = "Footer";
            this.Footer.Padding = new System.Windows.Forms.Padding(19, 0, 1, 0);
            this.Footer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Footer.Size = new System.Drawing.Size(1344, 25);
            this.Footer.TabIndex = 5;
            // 
            // lb_Hora
            // 
            this.lb_Hora.Name = "lb_Hora";
            this.lb_Hora.Size = new System.Drawing.Size(51, 20);
            this.lb_Hora.Text = "--:--:--";
            // 
            // lb_Fecha
            // 
            this.lb_Fecha.Name = "lb_Fecha";
            this.lb_Fecha.Size = new System.Drawing.Size(69, 20);
            this.lb_Fecha.Text = "--/--/----";
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Header.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Header.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lb_Station,
            this.btn_UserAccess,
            this.helpToolStripMenuItem});
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Padding = new System.Windows.Forms.Padding(8, 5, 0, 5);
            this.Header.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.Header.Size = new System.Drawing.Size(1344, 34);
            this.Header.TabIndex = 4;
            // 
            // lb_Station
            // 
            this.lb_Station.BackColor = System.Drawing.Color.Transparent;
            this.lb_Station.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_ConnSettings,
            this.btn_Initialize,
            this.btn_Shutdown,
            this.btn_Exit});
            this.lb_Station.ForeColor = System.Drawing.Color.Black;
            this.lb_Station.Image = ((System.Drawing.Image)(resources.GetObject("lb_Station.Image")));
            this.lb_Station.Name = "lb_Station";
            this.lb_Station.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lb_Station.Size = new System.Drawing.Size(88, 24);
            this.lb_Station.Text = "Station";
            this.lb_Station.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // btn_ConnSettings
            // 
            this.btn_ConnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btn_ConnSettings.Image")));
            this.btn_ConnSettings.Name = "btn_ConnSettings";
            this.btn_ConnSettings.Size = new System.Drawing.Size(157, 26);
            this.btn_ConnSettings.Text = "Settings";
            this.btn_ConnSettings.Click += new System.EventHandler(this.btn_ConnSettings_Click);
            // 
            // btn_Initialize
            // 
            this.btn_Initialize.ForeColor = System.Drawing.Color.Black;
            this.btn_Initialize.Image = ((System.Drawing.Image)(resources.GetObject("btn_Initialize.Image")));
            this.btn_Initialize.Name = "btn_Initialize";
            this.btn_Initialize.Size = new System.Drawing.Size(157, 26);
            this.btn_Initialize.Text = "Connect ";
            this.btn_Initialize.Click += new System.EventHandler(this.btn_Initialize_Click);
            // 
            // btn_Shutdown
            // 
            this.btn_Shutdown.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Shutdown.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_Shutdown.Image = ((System.Drawing.Image)(resources.GetObject("btn_Shutdown.Image")));
            this.btn_Shutdown.Name = "btn_Shutdown";
            this.btn_Shutdown.Size = new System.Drawing.Size(157, 26);
            this.btn_Shutdown.Text = "Disconnect";
            this.btn_Shutdown.Click += new System.EventHandler(this.btn_Shutdown_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(157, 26);
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_UserAccess
            // 
            this.btn_UserAccess.Image = ((System.Drawing.Image)(resources.GetObject("btn_UserAccess.Image")));
            this.btn_UserAccess.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.btn_UserAccess.Name = "btn_UserAccess";
            this.btn_UserAccess.Size = new System.Drawing.Size(76, 24);
            this.btn_UserAccess.Text = "Users";
            this.btn_UserAccess.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_UserAccess.Click += new System.EventHandler(this.btn_UserAccess_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_HardwareInfo,
            this.btn_SoftwareInfo,
            this.btn_ProductInfo});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // btn_HardwareInfo
            // 
            this.btn_HardwareInfo.ForeColor = System.Drawing.Color.Black;
            this.btn_HardwareInfo.Name = "btn_HardwareInfo";
            this.btn_HardwareInfo.Size = new System.Drawing.Size(179, 26);
            this.btn_HardwareInfo.Text = "Hardware Info";
            this.btn_HardwareInfo.Click += new System.EventHandler(this.btn_HardwareInfo_Click);
            // 
            // btn_SoftwareInfo
            // 
            this.btn_SoftwareInfo.ForeColor = System.Drawing.Color.Black;
            this.btn_SoftwareInfo.Name = "btn_SoftwareInfo";
            this.btn_SoftwareInfo.Size = new System.Drawing.Size(179, 26);
            this.btn_SoftwareInfo.Text = "Software Info";
            this.btn_SoftwareInfo.Click += new System.EventHandler(this.btn_SoftwareInfo_Click);
            // 
            // btn_ProductInfo
            // 
            this.btn_ProductInfo.ForeColor = System.Drawing.Color.Black;
            this.btn_ProductInfo.Name = "btn_ProductInfo";
            this.btn_ProductInfo.Size = new System.Drawing.Size(179, 26);
            this.btn_ProductInfo.Text = "Product Info";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.btn_Home);
            this.panel1.Controls.Add(this.btn_Stop);
            this.panel1.Controls.Add(this.btn_ManualAuto);
            this.panel1.Controls.Add(this.btn_Reset);
            this.panel1.Controls.Add(this.btn_LotManager);
            this.panel1.Controls.Add(this.btn_Diagnostics);
            this.panel1.Controls.Add(this.btn_Production);
            this.panel1.Controls.Add(this.btn_Engineering);
            this.panel1.Location = new System.Drawing.Point(0, 34);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(169, 846);
            this.panel1.TabIndex = 6;
            // 
            // btn_Home
            // 
            this.btn_Home.BackColor = System.Drawing.Color.LightGray;
            this.btn_Home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Home.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Home.FlatAppearance.BorderColor = System.Drawing.Color.SandyBrown;
            this.btn_Home.FlatAppearance.BorderSize = 2;
            this.btn_Home.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_Home.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.btn_Home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Home.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Home.Image = ((System.Drawing.Image)(resources.GetObject("btn_Home.Image")));
            this.btn_Home.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Home.Location = new System.Drawing.Point(0, 785);
            this.btn_Home.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Home.Name = "btn_Home";
            this.btn_Home.Size = new System.Drawing.Size(168, 49);
            this.btn_Home.TabIndex = 409;
            this.btn_Home.Text = "Home";
            this.btn_Home.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Home.UseVisualStyleBackColor = false;
            this.btn_Home.Click += new System.EventHandler(this.btn_Home_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Appearance = System.Windows.Forms.Appearance.Button;
            this.btn_Stop.BackColor = System.Drawing.Color.LightGray;
            this.btn_Stop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Stop.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_Stop.FlatAppearance.BorderSize = 2;
            this.btn_Stop.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Stop.Image = ((System.Drawing.Image)(resources.GetObject("btn_Stop.Image")));
            this.btn_Stop.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Stop.Location = new System.Drawing.Point(1, 592);
            this.btn_Stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(167, 49);
            this.btn_Stop.TabIndex = 408;
            this.btn_Stop.Text = "STOP";
            this.btn_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_Stop.UseVisualStyleBackColor = false;
            this.btn_Stop.Visible = false;
            // 
            // btn_ManualAuto
            // 
            this.btn_ManualAuto.Appearance = System.Windows.Forms.Appearance.Button;
            this.btn_ManualAuto.BackColor = System.Drawing.Color.LightGray;
            this.btn_ManualAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ManualAuto.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_ManualAuto.FlatAppearance.BorderSize = 2;
            this.btn_ManualAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ManualAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ManualAuto.Image = ((System.Drawing.Image)(resources.GetObject("btn_ManualAuto.Image")));
            this.btn_ManualAuto.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_ManualAuto.Location = new System.Drawing.Point(0, 663);
            this.btn_ManualAuto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ManualAuto.Name = "btn_ManualAuto";
            this.btn_ManualAuto.Size = new System.Drawing.Size(167, 49);
            this.btn_ManualAuto.TabIndex = 406;
            this.btn_ManualAuto.Text = "MANUAL ";
            this.btn_ManualAuto.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_ManualAuto.UseVisualStyleBackColor = false;
            // 
            // btn_Reset
            // 
            this.btn_Reset.Appearance = System.Windows.Forms.Appearance.Button;
            this.btn_Reset.BackColor = System.Drawing.Color.LightGray;
            this.btn_Reset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Reset.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_Reset.FlatAppearance.BorderSize = 2;
            this.btn_Reset.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Reset.Image = ((System.Drawing.Image)(resources.GetObject("btn_Reset.Image")));
            this.btn_Reset.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Reset.Location = new System.Drawing.Point(1, 727);
            this.btn_Reset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(167, 49);
            this.btn_Reset.TabIndex = 404;
            this.btn_Reset.Text = "Reset      ";
            this.btn_Reset.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_Reset.UseVisualStyleBackColor = false;
            // 
            // btn_LotManager
            // 
            this.btn_LotManager.BackColor = System.Drawing.Color.LightGray;
            this.btn_LotManager.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LotManager.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_LotManager.FlatAppearance.BorderSize = 2;
            this.btn_LotManager.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_LotManager.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.btn_LotManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LotManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LotManager.Image = ((System.Drawing.Image)(resources.GetObject("btn_LotManager.Image")));
            this.btn_LotManager.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_LotManager.Location = new System.Drawing.Point(0, 92);
            this.btn_LotManager.Margin = new System.Windows.Forms.Padding(4);
            this.btn_LotManager.Name = "btn_LotManager";
            this.btn_LotManager.Size = new System.Drawing.Size(168, 49);
            this.btn_LotManager.TabIndex = 11;
            this.btn_LotManager.Text = "Lot Manager";
            this.btn_LotManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_LotManager.UseVisualStyleBackColor = false;
            this.btn_LotManager.Click += new System.EventHandler(this.btn_LotManager_Click);
            // 
            // btn_Diagnostics
            // 
            this.btn_Diagnostics.BackColor = System.Drawing.Color.LightGray;
            this.btn_Diagnostics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Diagnostics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Diagnostics.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_Diagnostics.FlatAppearance.BorderSize = 2;
            this.btn_Diagnostics.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Diagnostics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.btn_Diagnostics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Diagnostics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Diagnostics.Image = ((System.Drawing.Image)(resources.GetObject("btn_Diagnostics.Image")));
            this.btn_Diagnostics.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Diagnostics.Location = new System.Drawing.Point(0, 206);
            this.btn_Diagnostics.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Diagnostics.Name = "btn_Diagnostics";
            this.btn_Diagnostics.Size = new System.Drawing.Size(168, 49);
            this.btn_Diagnostics.TabIndex = 10;
            this.btn_Diagnostics.Text = "Diagnostics";
            this.btn_Diagnostics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Diagnostics.UseVisualStyleBackColor = false;
            this.btn_Diagnostics.Click += new System.EventHandler(this.btn_Diagnostics_Click);
            // 
            // btn_Production
            // 
            this.btn_Production.BackColor = System.Drawing.Color.LightGray;
            this.btn_Production.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Production.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_Production.FlatAppearance.BorderSize = 2;
            this.btn_Production.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Production.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.btn_Production.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Production.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Production.Image = ((System.Drawing.Image)(resources.GetObject("btn_Production.Image")));
            this.btn_Production.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Production.Location = new System.Drawing.Point(0, 34);
            this.btn_Production.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Production.Name = "btn_Production";
            this.btn_Production.Size = new System.Drawing.Size(168, 49);
            this.btn_Production.TabIndex = 8;
            this.btn_Production.Text = "Production";
            this.btn_Production.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Production.UseVisualStyleBackColor = false;
            this.btn_Production.Click += new System.EventHandler(this.btn_Production_Click);
            // 
            // btn_Engineering
            // 
            this.btn_Engineering.BackColor = System.Drawing.Color.LightGray;
            this.btn_Engineering.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Engineering.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btn_Engineering.FlatAppearance.BorderSize = 2;
            this.btn_Engineering.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btn_Engineering.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.btn_Engineering.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Engineering.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Engineering.Image = ((System.Drawing.Image)(resources.GetObject("btn_Engineering.Image")));
            this.btn_Engineering.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Engineering.Location = new System.Drawing.Point(0, 149);
            this.btn_Engineering.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Engineering.Name = "btn_Engineering";
            this.btn_Engineering.Size = new System.Drawing.Size(168, 49);
            this.btn_Engineering.TabIndex = 9;
            this.btn_Engineering.Text = "Engineering";
            this.btn_Engineering.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Engineering.UseVisualStyleBackColor = false;
            this.btn_Engineering.Click += new System.EventHandler(this.btn_Engineering_Click);
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(485, -39);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(453, 240);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 8;
            this.pictureBox8.TabStop = false;
            // 
            // pnl_Messages
            // 
            this.pnl_Messages.Controls.Add(this.tab_MsgProdSys);
            this.pnl_Messages.Controls.Add(this.tab_MsgAlarms);
            this.pnl_Messages.Location = new System.Drawing.Point(169, 697);
            this.pnl_Messages.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_Messages.Name = "pnl_Messages";
            this.pnl_Messages.SelectedIndex = 0;
            this.pnl_Messages.Size = new System.Drawing.Size(1175, 180);
            this.pnl_Messages.TabIndex = 7;
            // 
            // tab_MsgProdSys
            // 
            this.tab_MsgProdSys.BackColor = System.Drawing.Color.White;
            this.tab_MsgProdSys.Controls.Add(this.txt_ProductionMsg);
            this.tab_MsgProdSys.Controls.Add(this.txt_SystemMsg);
            this.tab_MsgProdSys.Location = new System.Drawing.Point(4, 25);
            this.tab_MsgProdSys.Margin = new System.Windows.Forms.Padding(4);
            this.tab_MsgProdSys.Name = "tab_MsgProdSys";
            this.tab_MsgProdSys.Padding = new System.Windows.Forms.Padding(4);
            this.tab_MsgProdSys.Size = new System.Drawing.Size(1167, 151);
            this.tab_MsgProdSys.TabIndex = 0;
            this.tab_MsgProdSys.Text = "System | Production";
            // 
            // txt_ProductionMsg
            // 
            this.txt_ProductionMsg.BackColor = System.Drawing.SystemColors.Control;
            this.txt_ProductionMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_ProductionMsg.Location = new System.Drawing.Point(559, 7);
            this.txt_ProductionMsg.Margin = new System.Windows.Forms.Padding(4);
            this.txt_ProductionMsg.Name = "txt_ProductionMsg";
            this.txt_ProductionMsg.ReadOnly = true;
            this.txt_ProductionMsg.Size = new System.Drawing.Size(600, 138);
            this.txt_ProductionMsg.TabIndex = 1;
            this.txt_ProductionMsg.Text = "";
            this.txt_ProductionMsg.DoubleClick += new System.EventHandler(this.txt_ProductionMsg_DoubleClick);
            // 
            // txt_SystemMsg
            // 
            this.txt_SystemMsg.BackColor = System.Drawing.SystemColors.Control;
            this.txt_SystemMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_SystemMsg.Location = new System.Drawing.Point(4, 7);
            this.txt_SystemMsg.Margin = new System.Windows.Forms.Padding(4);
            this.txt_SystemMsg.Name = "txt_SystemMsg";
            this.txt_SystemMsg.ReadOnly = true;
            this.txt_SystemMsg.Size = new System.Drawing.Size(547, 138);
            this.txt_SystemMsg.TabIndex = 0;
            this.txt_SystemMsg.Text = "";
            this.txt_SystemMsg.DoubleClick += new System.EventHandler(this.txt_SystemMsg_DoubleClick);
            // 
            // tab_MsgAlarms
            // 
            this.tab_MsgAlarms.BackColor = System.Drawing.Color.White;
            this.tab_MsgAlarms.Controls.Add(this.dataGridView_Alarms);
            this.tab_MsgAlarms.Location = new System.Drawing.Point(4, 25);
            this.tab_MsgAlarms.Margin = new System.Windows.Forms.Padding(4);
            this.tab_MsgAlarms.Name = "tab_MsgAlarms";
            this.tab_MsgAlarms.Padding = new System.Windows.Forms.Padding(4);
            this.tab_MsgAlarms.Size = new System.Drawing.Size(1167, 151);
            this.tab_MsgAlarms.TabIndex = 1;
            this.tab_MsgAlarms.Text = "Alarms & Events";
            // 
            // dataGridView_Alarms
            // 
            this.dataGridView_Alarms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Alarms.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_Alarms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_Alarms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Alarms.Location = new System.Drawing.Point(4, 7);
            this.dataGridView_Alarms.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_Alarms.Name = "dataGridView_Alarms";
            this.dataGridView_Alarms.Size = new System.Drawing.Size(1159, 133);
            this.dataGridView_Alarms.TabIndex = 1;
            // 
            // LocalClock
            // 
            this.LocalClock.Interval = 1000;
            this.LocalClock.Tick += new System.EventHandler(this.LocalClock_Tick);
            // 
            // MachineStatus
            // 
            this.MachineStatus.Interval = 50;
            this.MachineStatus.Tick += new System.EventHandler(this.MachineStatus_Tick);
            // 
            // ProductionProcess
            // 
            this.ProductionProcess.Interval = 50;
            this.ProductionProcess.Tick += new System.EventHandler(this.ProductionProcess_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(183, 297);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(405, 335);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // gauge_CycleProgress
            // 
            this.gauge_CycleProgress.BackColor = System.Drawing.Color.White;
            this.gauge_CycleProgress.CausesValidation = false;
            this.gauge_CycleProgress.Controls.Add(this.txt_NoScrewdriving);
            this.gauge_CycleProgress.Controls.Add(this.txt_StopProcess);
            this.gauge_CycleProgress.Controls.Add(this.txt_MsgTryAgain);
            this.gauge_CycleProgress.Controls.Add(this.txt_InstallPlungerArmtr);
            this.gauge_CycleProgress.Controls.Add(this.txt_Screwdrinving);
            this.gauge_CycleProgress.Controls.Add(this.txt_MsgCheckScanner);
            this.gauge_CycleProgress.Controls.Add(this.txt_CycleResult);
            this.gauge_CycleProgress.Controls.Add(this.label14);
            this.gauge_CycleProgress.Controls.Add(this.txt_MachineMode);
            this.gauge_CycleProgress.Controls.Add(this.txt_MachineStatus);
            this.gauge_CycleProgress.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.Process_Arc,
            this.radialGaugeArc10,
            this.radialGaugeSingleLabel5});
            this.gauge_CycleProgress.Location = new System.Drawing.Point(955, 225);
            this.gauge_CycleProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gauge_CycleProgress.Name = "gauge_CycleProgress";
            this.gauge_CycleProgress.Size = new System.Drawing.Size(365, 450);
            this.gauge_CycleProgress.StartAngle = 90D;
            this.gauge_CycleProgress.SweepAngle = 360D;
            this.gauge_CycleProgress.TabIndex = 26;
            this.gauge_CycleProgress.Text = "radRadialGauge3";
            this.gauge_CycleProgress.Value = 99.96F;
            // 
            // txt_NoScrewdriving
            // 
            this.txt_NoScrewdriving.AutoSize = true;
            this.txt_NoScrewdriving.BackColor = System.Drawing.Color.Transparent;
            this.txt_NoScrewdriving.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_NoScrewdriving.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txt_NoScrewdriving.Location = new System.Drawing.Point(71, 247);
            this.txt_NoScrewdriving.Name = "txt_NoScrewdriving";
            this.txt_NoScrewdriving.Size = new System.Drawing.Size(222, 17);
            this.txt_NoScrewdriving.TabIndex = 439;
            this.txt_NoScrewdriving.Text = "El atornillado no ha sido realizado";
            this.txt_NoScrewdriving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_NoScrewdriving.Visible = false;
            // 
            // txt_StopProcess
            // 
            this.txt_StopProcess.AutoSize = true;
            this.txt_StopProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_StopProcess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txt_StopProcess.Location = new System.Drawing.Point(127, 423);
            this.txt_StopProcess.Name = "txt_StopProcess";
            this.txt_StopProcess.Size = new System.Drawing.Size(138, 25);
            this.txt_StopProcess.TabIndex = 429;
            this.txt_StopProcess.Text = "Ciclo abortado";
            this.txt_StopProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_StopProcess.Visible = false;
            // 
            // txt_MsgTryAgain
            // 
            this.txt_MsgTryAgain.AutoSize = true;
            this.txt_MsgTryAgain.BackColor = System.Drawing.Color.Transparent;
            this.txt_MsgTryAgain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MsgTryAgain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txt_MsgTryAgain.Location = new System.Drawing.Point(112, 240);
            this.txt_MsgTryAgain.Name = "txt_MsgTryAgain";
            this.txt_MsgTryAgain.Size = new System.Drawing.Size(157, 25);
            this.txt_MsgTryAgain.TabIndex = 428;
            this.txt_MsgTryAgain.Text = "Intente de nuevo";
            this.txt_MsgTryAgain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_MsgTryAgain.Visible = false;
            // 
            // txt_InstallPlungerArmtr
            // 
            this.txt_InstallPlungerArmtr.AutoSize = true;
            this.txt_InstallPlungerArmtr.BackColor = System.Drawing.Color.Transparent;
            this.txt_InstallPlungerArmtr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_InstallPlungerArmtr.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txt_InstallPlungerArmtr.Location = new System.Drawing.Point(113, 142);
            this.txt_InstallPlungerArmtr.Name = "txt_InstallPlungerArmtr";
            this.txt_InstallPlungerArmtr.Size = new System.Drawing.Size(139, 24);
            this.txt_InstallPlungerArmtr.TabIndex = 411;
            this.txt_InstallPlungerArmtr.Text = "Instalar Plunger";
            this.txt_InstallPlungerArmtr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_InstallPlungerArmtr.Visible = false;
            // 
            // txt_Screwdrinving
            // 
            this.txt_Screwdrinving.AutoSize = true;
            this.txt_Screwdrinving.BackColor = System.Drawing.Color.Transparent;
            this.txt_Screwdrinving.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Screwdrinving.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txt_Screwdrinving.Location = new System.Drawing.Point(105, 142);
            this.txt_Screwdrinving.Name = "txt_Screwdrinving";
            this.txt_Screwdrinving.Size = new System.Drawing.Size(154, 24);
            this.txt_Screwdrinving.TabIndex = 410;
            this.txt_Screwdrinving.Text = "Atornillar Plunger";
            this.txt_Screwdrinving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_Screwdrinving.Visible = false;
            // 
            // txt_MsgCheckScanner
            // 
            this.txt_MsgCheckScanner.AutoSize = true;
            this.txt_MsgCheckScanner.BackColor = System.Drawing.Color.Transparent;
            this.txt_MsgCheckScanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MsgCheckScanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txt_MsgCheckScanner.Location = new System.Drawing.Point(95, 240);
            this.txt_MsgCheckScanner.Name = "txt_MsgCheckScanner";
            this.txt_MsgCheckScanner.Size = new System.Drawing.Size(184, 25);
            this.txt_MsgCheckScanner.TabIndex = 409;
            this.txt_MsgCheckScanner.Text = "Revise el codigo 2D";
            this.txt_MsgCheckScanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_MsgCheckScanner.Visible = false;
            // 
            // txt_CycleResult
            // 
            this.txt_CycleResult.AutoSize = true;
            this.txt_CycleResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CycleResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(162)))), ((int)(((byte)(92)))));
            this.txt_CycleResult.Location = new System.Drawing.Point(125, 265);
            this.txt_CycleResult.Name = "txt_CycleResult";
            this.txt_CycleResult.Size = new System.Drawing.Size(117, 69);
            this.txt_CycleResult.TabIndex = 37;
            this.txt_CycleResult.Text = "OK";
            this.txt_CycleResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label14.Location = new System.Drawing.Point(235, 202);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 20);
            this.label14.TabIndex = 31;
            this.label14.Text = "%";
            // 
            // txt_MachineMode
            // 
            this.txt_MachineMode.AutoSize = true;
            this.txt_MachineMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MachineMode.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txt_MachineMode.Location = new System.Drawing.Point(103, 18);
            this.txt_MachineMode.Name = "txt_MachineMode";
            this.txt_MachineMode.Size = new System.Drawing.Size(176, 32);
            this.txt_MachineMode.TabIndex = 38;
            this.txt_MachineMode.Text = "Manual/Auto";
            this.txt_MachineMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_MachineStatus
            // 
            this.txt_MachineStatus.AutoSize = true;
            this.txt_MachineStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MachineStatus.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txt_MachineStatus.Location = new System.Drawing.Point(114, 395);
            this.txt_MachineStatus.Name = "txt_MachineStatus";
            this.txt_MachineStatus.Size = new System.Drawing.Size(176, 29);
            this.txt_MachineStatus.TabIndex = 404;
            this.txt_MachineStatus.Text = "Machine Status";
            this.txt_MachineStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Process_Arc
            // 
            this.Process_Arc.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Process_Arc.BackColor2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Process_Arc.BindEndRange = true;
            this.Process_Arc.BorderBoxStyle = Telerik.WinControls.BorderBoxStyle.SingleBorder;
            this.Process_Arc.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.Process_Arc.Name = "Process_Arc";
            this.Process_Arc.RangeEnd = 99.959999084472656D;
            this.Process_Arc.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.Process_Arc.UseCompatibleTextRendering = false;
            this.Process_Arc.Width = 20D;
            // 
            // radialGaugeArc10
            // 
            this.radialGaugeArc10.BackColor = System.Drawing.SystemColors.ControlLight;
            this.radialGaugeArc10.BackColor2 = System.Drawing.SystemColors.ControlLight;
            this.radialGaugeArc10.BindStartRange = true;
            this.radialGaugeArc10.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.radialGaugeArc10.FocusBorderWidth = 3;
            this.radialGaugeArc10.Name = "radialGaugeArc10";
            this.radialGaugeArc10.RangeEnd = 100D;
            this.radialGaugeArc10.RangeStart = 99.959999084472656D;
            this.radialGaugeArc10.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.radialGaugeArc10.UseCompatibleTextRendering = false;
            this.radialGaugeArc10.Width = 20D;
            // 
            // radialGaugeSingleLabel5
            // 
            this.radialGaugeSingleLabel5.BindValue = true;
            this.radialGaugeSingleLabel5.BorderInnerColor = System.Drawing.SystemColors.ControlLightLight;
            this.radialGaugeSingleLabel5.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.radialGaugeSingleLabel5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.radialGaugeSingleLabel5.LabelFontSize = 10F;
            this.radialGaugeSingleLabel5.LabelText = "Text";
            this.radialGaugeSingleLabel5.LocationPercentage = new System.Drawing.SizeF(0F, -0.1F);
            this.radialGaugeSingleLabel5.Name = "radialGaugeSingleLabel5";
            this.radialGaugeSingleLabel5.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.radialGaugeSingleLabel5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.radialGaugeSingleLabel5.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.radialGaugeSingleLabel5.UseCompatibleTextRendering = false;
            // 
            // lb_Scan
            // 
            this.lb_Scan.AutoSize = true;
            this.lb_Scan.BackColor = System.Drawing.Color.White;
            this.lb_Scan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Scan.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_Scan.Location = new System.Drawing.Point(625, 238);
            this.lb_Scan.Name = "lb_Scan";
            this.lb_Scan.Size = new System.Drawing.Size(155, 17);
            this.lb_Scan.TabIndex = 30;
            this.lb_Scan.Text = "Escaneando codigo 2D";
            // 
            // lb_PriorOpCheck
            // 
            this.lb_PriorOpCheck.AutoSize = true;
            this.lb_PriorOpCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_PriorOpCheck.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_PriorOpCheck.Location = new System.Drawing.Point(625, 297);
            this.lb_PriorOpCheck.Name = "lb_PriorOpCheck";
            this.lb_PriorOpCheck.Size = new System.Drawing.Size(104, 17);
            this.lb_PriorOpCheck.TabIndex = 32;
            this.lb_PriorOpCheck.Text = "Prior Op Check";
            // 
            // lb_StoreMeas
            // 
            this.lb_StoreMeas.AutoSize = true;
            this.lb_StoreMeas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_StoreMeas.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_StoreMeas.Location = new System.Drawing.Point(625, 588);
            this.lb_StoreMeas.Name = "lb_StoreMeas";
            this.lb_StoreMeas.Size = new System.Drawing.Size(136, 17);
            this.lb_StoreMeas.TabIndex = 34;
            this.lb_StoreMeas.Text = "Guardar mediciones";
            // 
            // lb_UpdateDeviceStatus
            // 
            this.lb_UpdateDeviceStatus.AutoSize = true;
            this.lb_UpdateDeviceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_UpdateDeviceStatus.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_UpdateDeviceStatus.Location = new System.Drawing.Point(625, 647);
            this.lb_UpdateDeviceStatus.Name = "lb_UpdateDeviceStatus";
            this.lb_UpdateDeviceStatus.Size = new System.Drawing.Size(145, 17);
            this.lb_UpdateDeviceStatus.TabIndex = 36;
            this.lb_UpdateDeviceStatus.Text = "Update Device Status";
            // 
            // lb_Screwdriving
            // 
            this.lb_Screwdriving.AutoSize = true;
            this.lb_Screwdriving.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Screwdriving.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_Screwdriving.Location = new System.Drawing.Point(625, 465);
            this.lb_Screwdriving.Name = "lb_Screwdriving";
            this.lb_Screwdriving.Size = new System.Drawing.Size(75, 17);
            this.lb_Screwdriving.TabIndex = 35;
            this.lb_Screwdriving.Text = "Atornillado";
            // 
            // txt_Scan
            // 
            this.txt_Scan.AutoSize = true;
            this.txt_Scan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Scan.ForeColor = System.Drawing.Color.Gray;
            this.txt_Scan.Location = new System.Drawing.Point(829, 238);
            this.txt_Scan.Name = "txt_Scan";
            this.txt_Scan.Size = new System.Drawing.Size(68, 17);
            this.txt_Scan.TabIndex = 37;
            this.txt_Scan.Text = "------------";
            this.txt_Scan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_Measurement1
            // 
            this.txt_Measurement1.AutoSize = true;
            this.txt_Measurement1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Measurement1.ForeColor = System.Drawing.Color.Gray;
            this.txt_Measurement1.Location = new System.Drawing.Point(829, 325);
            this.txt_Measurement1.Name = "txt_Measurement1";
            this.txt_Measurement1.Size = new System.Drawing.Size(68, 17);
            this.txt_Measurement1.TabIndex = 39;
            this.txt_Measurement1.Text = "------------";
            this.txt_Measurement1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_DBInsertMeas
            // 
            this.txt_DBInsertMeas.AutoSize = true;
            this.txt_DBInsertMeas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DBInsertMeas.ForeColor = System.Drawing.Color.Gray;
            this.txt_DBInsertMeas.Location = new System.Drawing.Point(829, 588);
            this.txt_DBInsertMeas.Name = "txt_DBInsertMeas";
            this.txt_DBInsertMeas.Size = new System.Drawing.Size(68, 17);
            this.txt_DBInsertMeas.TabIndex = 41;
            this.txt_DBInsertMeas.Text = "------------";
            this.txt_DBInsertMeas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_PartPresent
            // 
            this.lb_PartPresent.AutoSize = true;
            this.lb_PartPresent.BackColor = System.Drawing.Color.Gainsboro;
            this.lb_PartPresent.Checked = true;
            this.lb_PartPresent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lb_PartPresent.CustomBackground = true;
            this.lb_PartPresent.CustomForeColor = true;
            this.lb_PartPresent.Enabled = false;
            this.lb_PartPresent.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lb_PartPresent.Location = new System.Drawing.Point(281, 586);
            this.lb_PartPresent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_PartPresent.Name = "lb_PartPresent";
            this.lb_PartPresent.Size = new System.Drawing.Size(94, 17);
            this.lb_PartPresent.Style = MetroFramework.MetroColorStyle.Green;
            this.lb_PartPresent.TabIndex = 392;
            this.lb_PartPresent.Text = "Part Present";
            this.lb_PartPresent.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lb_PartPresent.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label8.Location = new System.Drawing.Point(573, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 32);
            this.label8.TabIndex = 394;
            this.label8.Text = "|";
            // 
            // txt_StationNo
            // 
            this.txt_StationNo.BackColor = System.Drawing.Color.White;
            this.txt_StationNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_StationNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_StationNo.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_StationNo.Location = new System.Drawing.Point(485, 130);
            this.txt_StationNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_StationNo.Name = "txt_StationNo";
            this.txt_StationNo.ReadOnly = true;
            this.txt_StationNo.Size = new System.Drawing.Size(83, 31);
            this.txt_StationNo.TabIndex = 395;
            this.txt_StationNo.Text = "A000";
            this.txt_StationNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_StationName
            // 
            this.txt_StationName.BackColor = System.Drawing.Color.White;
            this.txt_StationName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_StationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_StationName.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_StationName.Location = new System.Drawing.Point(601, 130);
            this.txt_StationName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_StationName.Name = "txt_StationName";
            this.txt_StationName.ReadOnly = true;
            this.txt_StationName.Size = new System.Drawing.Size(479, 31);
            this.txt_StationName.TabIndex = 396;
            this.txt_StationName.Text = "[Station No]";
            // 
            // lb_PressUp
            // 
            this.lb_PressUp.AutoSize = true;
            this.lb_PressUp.BackColor = System.Drawing.Color.White;
            this.lb_PressUp.Checked = true;
            this.lb_PressUp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lb_PressUp.CustomBackground = true;
            this.lb_PressUp.CustomForeColor = true;
            this.lb_PressUp.Enabled = false;
            this.lb_PressUp.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lb_PressUp.Location = new System.Drawing.Point(353, 382);
            this.lb_PressUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_PressUp.Name = "lb_PressUp";
            this.lb_PressUp.Size = new System.Drawing.Size(76, 17);
            this.lb_PressUp.Style = MetroFramework.MetroColorStyle.Green;
            this.lb_PressUp.TabIndex = 405;
            this.lb_PressUp.Text = "Press Up";
            this.lb_PressUp.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lb_PressUp.UseVisualStyleBackColor = false;
            // 
            // lb_PressDown
            // 
            this.lb_PressDown.AutoSize = true;
            this.lb_PressDown.BackColor = System.Drawing.Color.White;
            this.lb_PressDown.Checked = true;
            this.lb_PressDown.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lb_PressDown.CustomBackground = true;
            this.lb_PressDown.CustomForeColor = true;
            this.lb_PressDown.Enabled = false;
            this.lb_PressDown.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lb_PressDown.Location = new System.Drawing.Point(353, 405);
            this.lb_PressDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_PressDown.Name = "lb_PressDown";
            this.lb_PressDown.Size = new System.Drawing.Size(92, 17);
            this.lb_PressDown.Style = MetroFramework.MetroColorStyle.Green;
            this.lb_PressDown.TabIndex = 406;
            this.lb_PressDown.Text = "Press Down";
            this.lb_PressDown.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lb_PressDown.UseVisualStyleBackColor = false;
            // 
            // lb_SafetySys
            // 
            this.lb_SafetySys.AutoSize = true;
            this.lb_SafetySys.Checked = true;
            this.lb_SafetySys.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lb_SafetySys.CustomBackground = true;
            this.lb_SafetySys.CustomForeColor = true;
            this.lb_SafetySys.Enabled = false;
            this.lb_SafetySys.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lb_SafetySys.Location = new System.Drawing.Point(183, 267);
            this.lb_SafetySys.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_SafetySys.Name = "lb_SafetySys";
            this.lb_SafetySys.Size = new System.Drawing.Size(104, 17);
            this.lb_SafetySys.Style = MetroFramework.MetroColorStyle.Red;
            this.lb_SafetySys.TabIndex = 408;
            this.lb_SafetySys.Text = "Safety System";
            this.lb_SafetySys.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lb_SafetySys.UseVisualStyleBackColor = true;
            // 
            // lb_AirSupply
            // 
            this.lb_AirSupply.AutoSize = true;
            this.lb_AirSupply.Checked = true;
            this.lb_AirSupply.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lb_AirSupply.CustomBackground = true;
            this.lb_AirSupply.CustomForeColor = true;
            this.lb_AirSupply.Enabled = false;
            this.lb_AirSupply.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lb_AirSupply.Location = new System.Drawing.Point(183, 245);
            this.lb_AirSupply.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_AirSupply.Name = "lb_AirSupply";
            this.lb_AirSupply.Size = new System.Drawing.Size(83, 17);
            this.lb_AirSupply.Style = MetroFramework.MetroColorStyle.Red;
            this.lb_AirSupply.TabIndex = 407;
            this.lb_AirSupply.Text = "Air Supply";
            this.lb_AirSupply.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lb_AirSupply.UseVisualStyleBackColor = true;
            // 
            // lb_ContinueSequence
            // 
            this.lb_ContinueSequence.AutoSize = true;
            this.lb_ContinueSequence.BackColor = System.Drawing.Color.White;
            this.lb_ContinueSequence.Checked = true;
            this.lb_ContinueSequence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lb_ContinueSequence.CustomBackground = true;
            this.lb_ContinueSequence.CustomForeColor = true;
            this.lb_ContinueSequence.Enabled = false;
            this.lb_ContinueSequence.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lb_ContinueSequence.Location = new System.Drawing.Point(437, 622);
            this.lb_ContinueSequence.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_ContinueSequence.Name = "lb_ContinueSequence";
            this.lb_ContinueSequence.Size = new System.Drawing.Size(115, 17);
            this.lb_ContinueSequence.Style = MetroFramework.MetroColorStyle.Green;
            this.lb_ContinueSequence.TabIndex = 409;
            this.lb_ContinueSequence.Text = "Start / Continue";
            this.lb_ContinueSequence.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lb_ContinueSequence.UseVisualStyleBackColor = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(683, 206);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(27, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 410;
            this.pictureBox2.TabStop = false;
            // 
            // txt_MeasAngle
            // 
            this.txt_MeasAngle.AutoSize = true;
            this.txt_MeasAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MeasAngle.ForeColor = System.Drawing.Color.Gray;
            this.txt_MeasAngle.Location = new System.Drawing.Point(829, 488);
            this.txt_MeasAngle.Name = "txt_MeasAngle";
            this.txt_MeasAngle.Size = new System.Drawing.Size(68, 17);
            this.txt_MeasAngle.TabIndex = 421;
            this.txt_MeasAngle.Text = "------------";
            this.txt_MeasAngle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label7.Location = new System.Drawing.Point(757, 488);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 17);
            this.label7.TabIndex = 420;
            this.label7.Text = "Angle (°)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label1.Location = new System.Drawing.Point(723, 465);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 419;
            this.label1.Text = "Torque (mNm)";
            // 
            // txt_MeasTorque
            // 
            this.txt_MeasTorque.AutoSize = true;
            this.txt_MeasTorque.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MeasTorque.ForeColor = System.Drawing.Color.Gray;
            this.txt_MeasTorque.Location = new System.Drawing.Point(829, 465);
            this.txt_MeasTorque.Name = "txt_MeasTorque";
            this.txt_MeasTorque.Size = new System.Drawing.Size(68, 17);
            this.txt_MeasTorque.TabIndex = 418;
            this.txt_MeasTorque.Text = "------------";
            this.txt_MeasTorque.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // img_SystemProd
            // 
            this.img_SystemProd.BackColor = System.Drawing.Color.Transparent;
            this.img_SystemProd.Image = ((System.Drawing.Image)(resources.GetObject("img_SystemProd.Image")));
            this.img_SystemProd.InitialImage = null;
            this.img_SystemProd.Location = new System.Drawing.Point(291, 671);
            this.img_SystemProd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.img_SystemProd.Name = "img_SystemProd";
            this.img_SystemProd.Size = new System.Drawing.Size(29, 26);
            this.img_SystemProd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.img_SystemProd.TabIndex = 423;
            this.img_SystemProd.TabStop = false;
            // 
            // img_AlarmsEvents
            // 
            this.img_AlarmsEvents.BackColor = System.Drawing.Color.Transparent;
            this.img_AlarmsEvents.Image = ((System.Drawing.Image)(resources.GetObject("img_AlarmsEvents.Image")));
            this.img_AlarmsEvents.InitialImage = null;
            this.img_AlarmsEvents.Location = new System.Drawing.Point(399, 671);
            this.img_AlarmsEvents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.img_AlarmsEvents.Name = "img_AlarmsEvents";
            this.img_AlarmsEvents.Size = new System.Drawing.Size(29, 26);
            this.img_AlarmsEvents.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.img_AlarmsEvents.TabIndex = 422;
            this.img_AlarmsEvents.TabStop = false;
            // 
            // txt_DBInsertResults
            // 
            this.txt_DBInsertResults.AutoSize = true;
            this.txt_DBInsertResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DBInsertResults.ForeColor = System.Drawing.Color.Gray;
            this.txt_DBInsertResults.Location = new System.Drawing.Point(829, 676);
            this.txt_DBInsertResults.Name = "txt_DBInsertResults";
            this.txt_DBInsertResults.Size = new System.Drawing.Size(68, 17);
            this.txt_DBInsertResults.TabIndex = 426;
            this.txt_DBInsertResults.Text = "------------";
            this.txt_DBInsertResults.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_StoreResults
            // 
            this.lb_StoreResults.AutoSize = true;
            this.lb_StoreResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_StoreResults.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_StoreResults.Location = new System.Drawing.Point(625, 676);
            this.lb_StoreResults.Name = "lb_StoreResults";
            this.lb_StoreResults.Size = new System.Drawing.Size(154, 17);
            this.lb_StoreResults.TabIndex = 425;
            this.lb_StoreResults.Text = "Guardando resultados ";
            // 
            // txt_Measurement2
            // 
            this.txt_Measurement2.AutoSize = true;
            this.txt_Measurement2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Measurement2.ForeColor = System.Drawing.Color.Gray;
            this.txt_Measurement2.Location = new System.Drawing.Point(829, 349);
            this.txt_Measurement2.Name = "txt_Measurement2";
            this.txt_Measurement2.Size = new System.Drawing.Size(68, 17);
            this.txt_Measurement2.TabIndex = 428;
            this.txt_Measurement2.Text = "------------";
            this.txt_Measurement2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_VisualInspSpring
            // 
            this.lb_VisualInspSpring.AutoSize = true;
            this.lb_VisualInspSpring.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_VisualInspSpring.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_VisualInspSpring.Location = new System.Drawing.Point(625, 325);
            this.lb_VisualInspSpring.Name = "lb_VisualInspSpring";
            this.lb_VisualInspSpring.Size = new System.Drawing.Size(180, 17);
            this.lb_VisualInspSpring.TabIndex = 429;
            this.lb_VisualInspSpring.Text = "Inspeccion visual: Resortes";
            // 
            // lb_VisualInspPlunger
            // 
            this.lb_VisualInspPlunger.AutoSize = true;
            this.lb_VisualInspPlunger.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_VisualInspPlunger.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_VisualInspPlunger.Location = new System.Drawing.Point(625, 349);
            this.lb_VisualInspPlunger.Name = "lb_VisualInspPlunger";
            this.lb_VisualInspPlunger.Size = new System.Drawing.Size(172, 17);
            this.lb_VisualInspPlunger.TabIndex = 430;
            this.lb_VisualInspPlunger.Text = "Inspeccion visual: Plunger";
            // 
            // lb_ChkComp
            // 
            this.lb_ChkComp.AutoSize = true;
            this.lb_ChkComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ChkComp.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_ChkComp.Location = new System.Drawing.Point(625, 267);
            this.lb_ChkComp.Name = "lb_ChkComp";
            this.lb_ChkComp.Size = new System.Drawing.Size(144, 17);
            this.lb_ChkComp.TabIndex = 431;
            this.lb_ChkComp.Text = "Checar Componentes";
            // 
            // lb_AssignComp
            // 
            this.lb_AssignComp.AutoSize = true;
            this.lb_AssignComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_AssignComp.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_AssignComp.Location = new System.Drawing.Point(625, 617);
            this.lb_AssignComp.Name = "lb_AssignComp";
            this.lb_AssignComp.Size = new System.Drawing.Size(147, 17);
            this.lb_AssignComp.TabIndex = 433;
            this.lb_AssignComp.Text = "Asignar Componentes";
            // 
            // txt_ChkComp
            // 
            this.txt_ChkComp.BackColor = System.Drawing.Color.White;
            this.txt_ChkComp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_ChkComp.Location = new System.Drawing.Point(833, 267);
            this.txt_ChkComp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ChkComp.Name = "txt_ChkComp";
            this.txt_ChkComp.ReadOnly = true;
            this.txt_ChkComp.Size = new System.Drawing.Size(153, 15);
            this.txt_ChkComp.TabIndex = 435;
            this.txt_ChkComp.Text = "------------";
            // 
            // txt_PriorOpCheck
            // 
            this.txt_PriorOpCheck.BackColor = System.Drawing.Color.White;
            this.txt_PriorOpCheck.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_PriorOpCheck.Location = new System.Drawing.Point(833, 297);
            this.txt_PriorOpCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_PriorOpCheck.Name = "txt_PriorOpCheck";
            this.txt_PriorOpCheck.ReadOnly = true;
            this.txt_PriorOpCheck.Size = new System.Drawing.Size(153, 15);
            this.txt_PriorOpCheck.TabIndex = 436;
            this.txt_PriorOpCheck.Text = "------------";
            // 
            // txt_AssignComp
            // 
            this.txt_AssignComp.BackColor = System.Drawing.Color.White;
            this.txt_AssignComp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_AssignComp.Location = new System.Drawing.Point(835, 617);
            this.txt_AssignComp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_AssignComp.Name = "txt_AssignComp";
            this.txt_AssignComp.ReadOnly = true;
            this.txt_AssignComp.Size = new System.Drawing.Size(153, 15);
            this.txt_AssignComp.TabIndex = 437;
            this.txt_AssignComp.Text = "------------";
            // 
            // txt_UpdateDeviceStatus
            // 
            this.txt_UpdateDeviceStatus.BackColor = System.Drawing.Color.White;
            this.txt_UpdateDeviceStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_UpdateDeviceStatus.Location = new System.Drawing.Point(835, 647);
            this.txt_UpdateDeviceStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_UpdateDeviceStatus.Name = "txt_UpdateDeviceStatus";
            this.txt_UpdateDeviceStatus.ReadOnly = true;
            this.txt_UpdateDeviceStatus.Size = new System.Drawing.Size(153, 15);
            this.txt_UpdateDeviceStatus.TabIndex = 438;
            this.txt_UpdateDeviceStatus.Text = "------------";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label20.Location = new System.Drawing.Point(603, 588);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(16, 17);
            this.label20.TabIndex = 456;
            this.label20.Text = "8";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label21.Location = new System.Drawing.Point(603, 465);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(16, 17);
            this.label21.TabIndex = 455;
            this.label21.Text = "7";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label18.Location = new System.Drawing.Point(603, 374);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(16, 17);
            this.label18.TabIndex = 454;
            this.label18.Text = "6";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label19.Location = new System.Drawing.Point(603, 349);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(16, 17);
            this.label19.TabIndex = 453;
            this.label19.Text = "5";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label12.Location = new System.Drawing.Point(603, 325);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 17);
            this.label12.TabIndex = 452;
            this.label12.Text = "4";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label16.Location = new System.Drawing.Point(603, 297);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 17);
            this.label16.TabIndex = 451;
            this.label16.Text = "3";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label11.Location = new System.Drawing.Point(603, 267);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 17);
            this.label11.TabIndex = 450;
            this.label11.Text = "2";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label10.Location = new System.Drawing.Point(603, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 17);
            this.label10.TabIndex = 449;
            this.label10.Text = "1";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(603, 617);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 17);
            this.label2.TabIndex = 457;
            this.label2.Text = "9";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(595, 645);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 17);
            this.label3.TabIndex = 458;
            this.label3.Text = "10";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_MeasSP
            // 
            this.txt_MeasSP.AutoSize = true;
            this.txt_MeasSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MeasSP.ForeColor = System.Drawing.Color.Gray;
            this.txt_MeasSP.Location = new System.Drawing.Point(829, 563);
            this.txt_MeasSP.Name = "txt_MeasSP";
            this.txt_MeasSP.Size = new System.Drawing.Size(68, 17);
            this.txt_MeasSP.TabIndex = 462;
            this.txt_MeasSP.Text = "------------";
            this.txt_MeasSP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label5.Location = new System.Drawing.Point(635, 563);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 17);
            this.label5.TabIndex = 461;
            this.label5.Text = "Seating Point Torque (mNm)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label6.Location = new System.Drawing.Point(679, 512);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 17);
            this.label6.TabIndex = 460;
            this.label6.Text = "Clamp Torque (mNm)";
            // 
            // txt_MeasClamp
            // 
            this.txt_MeasClamp.AutoSize = true;
            this.txt_MeasClamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MeasClamp.ForeColor = System.Drawing.Color.Gray;
            this.txt_MeasClamp.Location = new System.Drawing.Point(829, 512);
            this.txt_MeasClamp.Name = "txt_MeasClamp";
            this.txt_MeasClamp.Size = new System.Drawing.Size(68, 17);
            this.txt_MeasClamp.TabIndex = 459;
            this.txt_MeasClamp.Text = "------------";
            this.txt_MeasClamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label4.Location = new System.Drawing.Point(715, 537);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 17);
            this.label4.TabIndex = 464;
            this.label4.Text = "Clamp Angle (°)";
            // 
            // txt_MeasClampAngle
            // 
            this.txt_MeasClampAngle.AutoSize = true;
            this.txt_MeasClampAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MeasClampAngle.ForeColor = System.Drawing.Color.Gray;
            this.txt_MeasClampAngle.Location = new System.Drawing.Point(829, 537);
            this.txt_MeasClampAngle.Name = "txt_MeasClampAngle";
            this.txt_MeasClampAngle.Size = new System.Drawing.Size(68, 17);
            this.txt_MeasClampAngle.TabIndex = 463;
            this.txt_MeasClampAngle.Text = "------------";
            this.txt_MeasClampAngle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_DisplayChX
            // 
            this.txt_DisplayChX.AutoSize = true;
            this.txt_DisplayChX.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DisplayChX.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_DisplayChX.Location = new System.Drawing.Point(195, 322);
            this.txt_DisplayChX.Name = "txt_DisplayChX";
            this.txt_DisplayChX.Size = new System.Drawing.Size(60, 17);
            this.txt_DisplayChX.TabIndex = 465;
            this.txt_DisplayChX.Text = "000.000";
            // 
            // txt_DisplayChY
            // 
            this.txt_DisplayChY.AutoSize = true;
            this.txt_DisplayChY.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DisplayChY.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_DisplayChY.Location = new System.Drawing.Point(195, 306);
            this.txt_DisplayChY.Name = "txt_DisplayChY";
            this.txt_DisplayChY.Size = new System.Drawing.Size(60, 17);
            this.txt_DisplayChY.TabIndex = 466;
            this.txt_DisplayChY.Text = "000.000";
            // 
            // txt_PCON_CurrPos
            // 
            this.txt_PCON_CurrPos.AutoSize = true;
            this.txt_PCON_CurrPos.BackColor = System.Drawing.Color.White;
            this.txt_PCON_CurrPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_PCON_CurrPos.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_PCON_CurrPos.Location = new System.Drawing.Point(195, 405);
            this.txt_PCON_CurrPos.Name = "txt_PCON_CurrPos";
            this.txt_PCON_CurrPos.Size = new System.Drawing.Size(60, 17);
            this.txt_PCON_CurrPos.TabIndex = 467;
            this.txt_PCON_CurrPos.Text = "000.000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label9.Location = new System.Drawing.Point(261, 306);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 17);
            this.label9.TabIndex = 468;
            this.label9.Text = "N";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label13.Location = new System.Drawing.Point(261, 322);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 17);
            this.label13.TabIndex = 469;
            this.label13.Text = "mm";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label15.Location = new System.Drawing.Point(261, 405);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 17);
            this.label15.TabIndex = 470;
            this.label15.Text = "mm";
            // 
            // lb_Test2
            // 
            this.lb_Test2.AutoSize = true;
            this.lb_Test2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Test2.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_Test2.Location = new System.Drawing.Point(647, 423);
            this.lb_Test2.Name = "lb_Test2";
            this.lb_Test2.Size = new System.Drawing.Size(48, 17);
            this.lb_Test2.TabIndex = 475;
            this.lb_Test2.Text = "Test 2";
            // 
            // lb_Test1
            // 
            this.lb_Test1.AutoSize = true;
            this.lb_Test1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Test1.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_Test1.Location = new System.Drawing.Point(647, 401);
            this.lb_Test1.Name = "lb_Test1";
            this.lb_Test1.Size = new System.Drawing.Size(48, 17);
            this.lb_Test1.TabIndex = 474;
            this.lb_Test1.Text = "Test 1";
            // 
            // txt_Test1ChYMeas
            // 
            this.txt_Test1ChYMeas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Test1ChYMeas.ForeColor = System.Drawing.Color.Gray;
            this.txt_Test1ChYMeas.Location = new System.Drawing.Point(833, 404);
            this.txt_Test1ChYMeas.Name = "txt_Test1ChYMeas";
            this.txt_Test1ChYMeas.Size = new System.Drawing.Size(89, 16);
            this.txt_Test1ChYMeas.TabIndex = 473;
            this.txt_Test1ChYMeas.Text = "------------";
            this.txt_Test1ChYMeas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_Test2ChYMeas
            // 
            this.txt_Test2ChYMeas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Test2ChYMeas.ForeColor = System.Drawing.Color.Gray;
            this.txt_Test2ChYMeas.Location = new System.Drawing.Point(833, 423);
            this.txt_Test2ChYMeas.Name = "txt_Test2ChYMeas";
            this.txt_Test2ChYMeas.Size = new System.Drawing.Size(89, 17);
            this.txt_Test2ChYMeas.TabIndex = 472;
            this.txt_Test2ChYMeas.Text = "------------";
            this.txt_Test2ChYMeas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_Measurements
            // 
            this.lb_Measurements.AutoSize = true;
            this.lb_Measurements.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Measurements.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_Measurements.Location = new System.Drawing.Point(625, 374);
            this.lb_Measurements.Name = "lb_Measurements";
            this.lb_Measurements.Size = new System.Drawing.Size(65, 17);
            this.lb_Measurements.TabIndex = 471;
            this.lb_Measurements.Text = "Resortes";
            // 
            // lb_ConstantK
            // 
            this.lb_ConstantK.AutoSize = true;
            this.lb_ConstantK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ConstantK.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.lb_ConstantK.Location = new System.Drawing.Point(620, 442);
            this.lb_ConstantK.Name = "lb_ConstantK";
            this.lb_ConstantK.Size = new System.Drawing.Size(77, 17);
            this.lb_ConstantK.TabIndex = 477;
            this.lb_ConstantK.Text = "Constant K";
            // 
            // txt_KistlerConstK
            // 
            this.txt_KistlerConstK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_KistlerConstK.ForeColor = System.Drawing.Color.Gray;
            this.txt_KistlerConstK.Location = new System.Drawing.Point(705, 442);
            this.txt_KistlerConstK.Name = "txt_KistlerConstK";
            this.txt_KistlerConstK.Size = new System.Drawing.Size(85, 17);
            this.txt_KistlerConstK.TabIndex = 476;
            this.txt_KistlerConstK.Text = "------------";
            this.txt_KistlerConstK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label25.Location = new System.Drawing.Point(595, 675);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(24, 17);
            this.label25.TabIndex = 478;
            this.label25.Text = "11";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Test1ChXMeas
            // 
            this.txt_Test1ChXMeas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Test1ChXMeas.ForeColor = System.Drawing.Color.Gray;
            this.txt_Test1ChXMeas.Location = new System.Drawing.Point(701, 401);
            this.txt_Test1ChXMeas.Name = "txt_Test1ChXMeas";
            this.txt_Test1ChXMeas.Size = new System.Drawing.Size(89, 22);
            this.txt_Test1ChXMeas.TabIndex = 480;
            this.txt_Test1ChXMeas.Text = "------------";
            this.txt_Test1ChXMeas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_Test2ChXMeas
            // 
            this.txt_Test2ChXMeas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Test2ChXMeas.ForeColor = System.Drawing.Color.Gray;
            this.txt_Test2ChXMeas.Location = new System.Drawing.Point(702, 423);
            this.txt_Test2ChXMeas.Name = "txt_Test2ChXMeas";
            this.txt_Test2ChXMeas.Size = new System.Drawing.Size(89, 17);
            this.txt_Test2ChXMeas.TabIndex = 479;
            this.txt_Test2ChXMeas.Text = "------------";
            this.txt_Test2ChXMeas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_SafetyCurtains
            // 
            this.lb_SafetyCurtains.AutoSize = true;
            this.lb_SafetyCurtains.Checked = true;
            this.lb_SafetyCurtains.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lb_SafetyCurtains.CustomBackground = true;
            this.lb_SafetyCurtains.CustomForeColor = true;
            this.lb_SafetyCurtains.Enabled = false;
            this.lb_SafetyCurtains.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lb_SafetyCurtains.Location = new System.Drawing.Point(281, 622);
            this.lb_SafetyCurtains.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lb_SafetyCurtains.Name = "lb_SafetyCurtains";
            this.lb_SafetyCurtains.Size = new System.Drawing.Size(110, 17);
            this.lb_SafetyCurtains.Style = MetroFramework.MetroColorStyle.Red;
            this.lb_SafetyCurtains.TabIndex = 481;
            this.lb_SafetyCurtains.Text = "Safety Curtains";
            this.lb_SafetyCurtains.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lb_SafetyCurtains.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label17.Location = new System.Drawing.Point(797, 404);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(30, 17);
            this.label17.TabIndex = 482;
            this.label17.Text = "mm";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label22.Location = new System.Drawing.Point(928, 404);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(18, 17);
            this.label22.TabIndex = 483;
            this.label22.Text = "N";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label23.Location = new System.Drawing.Point(928, 423);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(18, 17);
            this.label23.TabIndex = 484;
            this.label23.Text = "N";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label24.Location = new System.Drawing.Point(797, 423);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(30, 17);
            this.label24.TabIndex = 485;
            this.label24.Text = "mm";
            // 
            // HMI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1344, 897);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lb_SafetyCurtains);
            this.Controls.Add(this.txt_Test1ChXMeas);
            this.Controls.Add(this.txt_Test2ChXMeas);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.lb_ConstantK);
            this.Controls.Add(this.txt_KistlerConstK);
            this.Controls.Add(this.lb_Test2);
            this.Controls.Add(this.lb_Test1);
            this.Controls.Add(this.txt_Test1ChYMeas);
            this.Controls.Add(this.txt_Test2ChYMeas);
            this.Controls.Add(this.lb_Measurements);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_PCON_CurrPos);
            this.Controls.Add(this.txt_DisplayChY);
            this.Controls.Add(this.txt_DisplayChX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_MeasClampAngle);
            this.Controls.Add(this.txt_MeasSP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_MeasClamp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txt_UpdateDeviceStatus);
            this.Controls.Add(this.txt_AssignComp);
            this.Controls.Add(this.txt_PriorOpCheck);
            this.Controls.Add(this.txt_ChkComp);
            this.Controls.Add(this.lb_AssignComp);
            this.Controls.Add(this.lb_ChkComp);
            this.Controls.Add(this.lb_VisualInspPlunger);
            this.Controls.Add(this.lb_VisualInspSpring);
            this.Controls.Add(this.txt_Measurement2);
            this.Controls.Add(this.txt_DBInsertResults);
            this.Controls.Add(this.lb_StoreResults);
            this.Controls.Add(this.img_SystemProd);
            this.Controls.Add(this.img_AlarmsEvents);
            this.Controls.Add(this.txt_MeasAngle);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_MeasTorque);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lb_ContinueSequence);
            this.Controls.Add(this.lb_SafetySys);
            this.Controls.Add(this.lb_AirSupply);
            this.Controls.Add(this.lb_PressDown);
            this.Controls.Add(this.lb_PressUp);
            this.Controls.Add(this.txt_StationName);
            this.Controls.Add(this.txt_StationNo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lb_PartPresent);
            this.Controls.Add(this.txt_DBInsertMeas);
            this.Controls.Add(this.txt_Measurement1);
            this.Controls.Add(this.txt_Scan);
            this.Controls.Add(this.lb_UpdateDeviceStatus);
            this.Controls.Add(this.lb_Screwdriving);
            this.Controls.Add(this.lb_Scan);
            this.Controls.Add(this.lb_StoreMeas);
            this.Controls.Add(this.lb_PriorOpCheck);
            this.Controls.Add(this.Footer);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_Messages);
            this.Controls.Add(this.gauge_CycleProgress);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HMI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sensata Technologies | [Station Name]";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HMI_FormClosed);
            this.Load += new System.EventHandler(this.HMI_Load);
            this.Footer.ResumeLayout(false);
            this.Footer.PerformLayout();
            this.Header.ResumeLayout(false);
            this.Header.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.pnl_Messages.ResumeLayout(false);
            this.tab_MsgProdSys.ResumeLayout(false);
            this.tab_MsgAlarms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Alarms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gauge_CycleProgress)).EndInit();
            this.gauge_CycleProgress.ResumeLayout(false);
            this.gauge_CycleProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_SystemProd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_AlarmsEvents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip Footer;
        private System.Windows.Forms.ToolStripStatusLabel lb_Hora;
        private System.Windows.Forms.ToolStripStatusLabel lb_Fecha;
        private System.Windows.Forms.MenuStrip Header;
        private System.Windows.Forms.ToolStripMenuItem lb_Station;
        private System.Windows.Forms.ToolStripMenuItem btn_Initialize;
        private System.Windows.Forms.ToolStripMenuItem btn_Shutdown;
        private System.Windows.Forms.ToolStripMenuItem btn_Exit;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btn_HardwareInfo;
        private System.Windows.Forms.ToolStripMenuItem btn_SoftwareInfo;
        private System.Windows.Forms.ToolStripMenuItem btn_ProductInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Diagnostics;
        private System.Windows.Forms.Button btn_Production;
        private System.Windows.Forms.Button btn_Engineering;
        private System.Windows.Forms.TabControl pnl_Messages;
        private System.Windows.Forms.TabPage tab_MsgProdSys;
        private System.Windows.Forms.TabPage tab_MsgAlarms;
        private System.Windows.Forms.RichTextBox txt_ProductionMsg;
        private System.Windows.Forms.RichTextBox txt_SystemMsg;
        private System.Windows.Forms.DataGridView dataGridView_Alarms;
        private System.Windows.Forms.Timer LocalClock;
        private System.Windows.Forms.Timer MachineStatus;
        private System.Windows.Forms.Timer ProductionProcess;
        private System.Windows.Forms.Button btn_LotManager;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.ToolStripMenuItem btn_UserAccess;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.UI.Gauges.RadRadialGauge gauge_CycleProgress;
        private Label label14;
        private Label lb_Scan;
        private Telerik.WinControls.UI.Gauges.RadialGaugeArc Process_Arc;
        private Telerik.WinControls.UI.Gauges.RadialGaugeArc radialGaugeArc10;
        private Telerik.WinControls.UI.Gauges.RadialGaugeSingleLabel radialGaugeSingleLabel5;
        private Label lb_PriorOpCheck;
        private Label lb_StoreMeas;
        private Label lb_UpdateDeviceStatus;
        private Label lb_Screwdriving;
        private Label txt_MachineMode;
        private Label txt_CycleResult;
        private Label txt_Scan;
        private Label txt_Measurement1;
        private Label txt_DBInsertMeas;
        private MetroFramework.Controls.MetroCheckBox lb_PartPresent;
        private Label label8;
        private TextBox txt_StationNo;
        private TextBox txt_StationName;
        private ToolStripMenuItem btn_ConnSettings;
        private CheckBox btn_ManualAuto;
        private CheckBox btn_Reset;
        private Label txt_MachineStatus;
        private MetroFramework.Controls.MetroCheckBox lb_PressUp;
        private MetroFramework.Controls.MetroCheckBox lb_PressDown;
        private MetroFramework.Controls.MetroCheckBox lb_SafetySys;
        private MetroFramework.Controls.MetroCheckBox lb_AirSupply;
        private Label txt_MsgCheckScanner;
        private Label txt_Screwdrinving;
        private Label txt_InstallPlungerArmtr;
        private MetroFramework.Controls.MetroCheckBox lb_ContinueSequence;
        private PictureBox pictureBox2;
        private Label txt_MeasAngle;
        private Label label7;
        private Label label1;
        private Label txt_MeasTorque;
        private PictureBox img_SystemProd;
        private PictureBox img_AlarmsEvents;
        private Label txt_DBInsertResults;
        private Label lb_StoreResults;
        private CheckBox btn_Stop;
        private Label txt_MsgTryAgain;
        private Label txt_StopProcess;
        private Label txt_Measurement2;
        private Label lb_VisualInspSpring;
        private Label lb_VisualInspPlunger;
        private Label lb_ChkComp;
        private Label lb_AssignComp;
        private Label txt_NoScrewdriving;
        private TextBox txt_ChkComp;
        private TextBox txt_PriorOpCheck;
        private TextBox txt_AssignComp;
        private TextBox txt_UpdateDeviceStatus;
        private Label label20;
        private Label label21;
        private Label label18;
        private Label label19;
        private Label label12;
        private Label label16;
        private Label label11;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label txt_MeasSP;
        private Label label5;
        private Label label6;
        private Label txt_MeasClamp;
        private Label label4;
        private Label txt_MeasClampAngle;
        private Label txt_DisplayChX;
        private Label txt_DisplayChY;
        private Label txt_PCON_CurrPos;
        private Label label9;
        private Label label13;
        private Label label15;
        private Label lb_Test2;
        private Label lb_Test1;
        private Label txt_Test1ChYMeas;
        private Label txt_Test2ChYMeas;
        private Label lb_Measurements;
        private Label lb_ConstantK;
        private Label txt_KistlerConstK;
        private Label label25;
        private Label txt_Test1ChXMeas;
        private Label txt_Test2ChXMeas;
        private Button btn_Home;
        private MetroFramework.Controls.MetroCheckBox lb_SafetyCurtains;
        private Label label17;
        private Label label22;
        private Label label23;
        private Label label24;
    }
}

