namespace Software
{
    partial class Engineering
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Engineering));
            this.MachineStatus = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_Config = new System.Windows.Forms.TabPage();
            this.pnl_FuncManagement = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_AssignComp = new MetroFramework.Controls.MetroToggle();
            this.btn_UpdateDevStsFunc = new MetroFramework.Controls.MetroToggle();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_ChkCompFunc = new MetroFramework.Controls.MetroToggle();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_MasterPartsSeq = new MetroFramework.Controls.MetroToggle();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_LimitsDBFunc = new MetroFramework.Controls.MetroToggle();
            this.btn_StoreResultsFunc = new MetroFramework.Controls.MetroToggle();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_DisableFuncAll = new System.Windows.Forms.Button();
            this.btn_EnableFuncAll = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_StoreMeasureFunc = new MetroFramework.Controls.MetroToggle();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Measure2Func = new MetroFramework.Controls.MetroToggle();
            this.btn_Measure1Func = new MetroFramework.Controls.MetroToggle();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_PriorOpChkFunc = new MetroFramework.Controls.MetroToggle();
            this.label65 = new System.Windows.Forms.Label();
            this.btn_ScanningFunc = new MetroFramework.Controls.MetroToggle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbx_AssignComp = new System.Windows.Forms.CheckBox();
            this.btn_DisableMsgAll = new System.Windows.Forms.Button();
            this.btn_EnableMsgAll = new System.Windows.Forms.Button();
            this.cbx_CheckComp = new System.Windows.Forms.CheckBox();
            this.cbx_StoreResults = new System.Windows.Forms.CheckBox();
            this.cbx_Scanning = new System.Windows.Forms.CheckBox();
            this.cbx_SafetyController = new System.Windows.Forms.CheckBox();
            this.cbx_StoreMeasurements = new System.Windows.Forms.CheckBox();
            this.cbx_UpdateDeviceStatus = new System.Windows.Forms.CheckBox();
            this.cbx_Process3 = new System.Windows.Forms.CheckBox();
            this.cbx_Process2 = new System.Windows.Forms.CheckBox();
            this.cbx_Process1 = new System.Windows.Forms.CheckBox();
            this.cbx_Kistler = new System.Windows.Forms.CheckBox();
            this.cbx_Instrument2 = new System.Windows.Forms.CheckBox();
            this.cbx_Instrument1 = new System.Windows.Forms.CheckBox();
            this.cbx_PLC = new System.Windows.Forms.CheckBox();
            this.cbx_PriorOpCheck = new System.Windows.Forms.CheckBox();
            this.btn_ForceDisplacement = new MetroFramework.Controls.MetroToggle();
            this.label11 = new System.Windows.Forms.Label();
            this.cbx_PCON = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tab_Config.SuspendLayout();
            this.pnl_FuncManagement.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MachineStatus
            // 
            this.MachineStatus.Interval = 50;
            this.MachineStatus.Tick += new System.EventHandler(this.MachineStatus_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_Config);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(888, 666);
            this.tabControl1.TabIndex = 1;
            // 
            // tab_Config
            // 
            this.tab_Config.BackColor = System.Drawing.SystemColors.Control;
            this.tab_Config.Controls.Add(this.pnl_FuncManagement);
            this.tab_Config.Controls.Add(this.groupBox1);
            this.tab_Config.Cursor = System.Windows.Forms.Cursors.Default;
            this.tab_Config.Location = new System.Drawing.Point(4, 25);
            this.tab_Config.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_Config.Name = "tab_Config";
            this.tab_Config.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tab_Config.Size = new System.Drawing.Size(880, 637);
            this.tab_Config.TabIndex = 0;
            this.tab_Config.Text = "Configuration";
            // 
            // pnl_FuncManagement
            // 
            this.pnl_FuncManagement.Controls.Add(this.btn_ForceDisplacement);
            this.pnl_FuncManagement.Controls.Add(this.label11);
            this.pnl_FuncManagement.Controls.Add(this.label9);
            this.pnl_FuncManagement.Controls.Add(this.btn_AssignComp);
            this.pnl_FuncManagement.Controls.Add(this.btn_UpdateDevStsFunc);
            this.pnl_FuncManagement.Controls.Add(this.label8);
            this.pnl_FuncManagement.Controls.Add(this.btn_ChkCompFunc);
            this.pnl_FuncManagement.Controls.Add(this.label10);
            this.pnl_FuncManagement.Controls.Add(this.btn_MasterPartsSeq);
            this.pnl_FuncManagement.Controls.Add(this.label7);
            this.pnl_FuncManagement.Controls.Add(this.btn_LimitsDBFunc);
            this.pnl_FuncManagement.Controls.Add(this.btn_StoreResultsFunc);
            this.pnl_FuncManagement.Controls.Add(this.label6);
            this.pnl_FuncManagement.Controls.Add(this.btn_DisableFuncAll);
            this.pnl_FuncManagement.Controls.Add(this.btn_EnableFuncAll);
            this.pnl_FuncManagement.Controls.Add(this.label5);
            this.pnl_FuncManagement.Controls.Add(this.btn_StoreMeasureFunc);
            this.pnl_FuncManagement.Controls.Add(this.label4);
            this.pnl_FuncManagement.Controls.Add(this.btn_Measure2Func);
            this.pnl_FuncManagement.Controls.Add(this.btn_Measure1Func);
            this.pnl_FuncManagement.Controls.Add(this.label3);
            this.pnl_FuncManagement.Controls.Add(this.label2);
            this.pnl_FuncManagement.Controls.Add(this.label1);
            this.pnl_FuncManagement.Controls.Add(this.btn_PriorOpChkFunc);
            this.pnl_FuncManagement.Controls.Add(this.label65);
            this.pnl_FuncManagement.Controls.Add(this.btn_ScanningFunc);
            this.pnl_FuncManagement.Location = new System.Drawing.Point(5, 6);
            this.pnl_FuncManagement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnl_FuncManagement.Name = "pnl_FuncManagement";
            this.pnl_FuncManagement.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnl_FuncManagement.Size = new System.Drawing.Size(398, 537);
            this.pnl_FuncManagement.TabIndex = 3;
            this.pnl_FuncManagement.TabStop = false;
            this.pnl_FuncManagement.Text = "Fucntions Management";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label9.Location = new System.Drawing.Point(7, 351);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(166, 17);
            this.label9.TabIndex = 428;
            this.label9.Text = "Assign Components (DB)";
            // 
            // btn_AssignComp
            // 
            this.btn_AssignComp.AutoSize = true;
            this.btn_AssignComp.BackColor = System.Drawing.SystemColors.Control;
            this.btn_AssignComp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AssignComp.CustomBackground = true;
            this.btn_AssignComp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AssignComp.Location = new System.Drawing.Point(264, 348);
            this.btn_AssignComp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AssignComp.Name = "btn_AssignComp";
            this.btn_AssignComp.Size = new System.Drawing.Size(80, 21);
            this.btn_AssignComp.TabIndex = 427;
            this.btn_AssignComp.Text = "Off";
            this.btn_AssignComp.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_AssignComp.UseStyleColors = true;
            this.btn_AssignComp.UseVisualStyleBackColor = false;
            // 
            // btn_UpdateDevStsFunc
            // 
            this.btn_UpdateDevStsFunc.AutoSize = true;
            this.btn_UpdateDevStsFunc.BackColor = System.Drawing.SystemColors.Control;
            this.btn_UpdateDevStsFunc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_UpdateDevStsFunc.CustomBackground = true;
            this.btn_UpdateDevStsFunc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UpdateDevStsFunc.Location = new System.Drawing.Point(264, 379);
            this.btn_UpdateDevStsFunc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_UpdateDevStsFunc.Name = "btn_UpdateDevStsFunc";
            this.btn_UpdateDevStsFunc.Size = new System.Drawing.Size(80, 21);
            this.btn_UpdateDevStsFunc.TabIndex = 408;
            this.btn_UpdateDevStsFunc.Text = "Off";
            this.btn_UpdateDevStsFunc.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_UpdateDevStsFunc.UseStyleColors = true;
            this.btn_UpdateDevStsFunc.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label8.Location = new System.Drawing.Point(7, 132);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 17);
            this.label8.TabIndex = 426;
            this.label8.Text = "Check Components (DB)";
            // 
            // btn_ChkCompFunc
            // 
            this.btn_ChkCompFunc.AutoSize = true;
            this.btn_ChkCompFunc.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ChkCompFunc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ChkCompFunc.CustomBackground = true;
            this.btn_ChkCompFunc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ChkCompFunc.Location = new System.Drawing.Point(264, 129);
            this.btn_ChkCompFunc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ChkCompFunc.Name = "btn_ChkCompFunc";
            this.btn_ChkCompFunc.Size = new System.Drawing.Size(80, 21);
            this.btn_ChkCompFunc.TabIndex = 425;
            this.btn_ChkCompFunc.Text = "Off";
            this.btn_ChkCompFunc.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_ChkCompFunc.UseStyleColors = true;
            this.btn_ChkCompFunc.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label10.Location = new System.Drawing.Point(7, 68);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(156, 17);
            this.label10.TabIndex = 424;
            this.label10.Text = "Master Parts Sequence";
            // 
            // btn_MasterPartsSeq
            // 
            this.btn_MasterPartsSeq.AutoSize = true;
            this.btn_MasterPartsSeq.BackColor = System.Drawing.SystemColors.Control;
            this.btn_MasterPartsSeq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_MasterPartsSeq.CustomBackground = true;
            this.btn_MasterPartsSeq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MasterPartsSeq.Location = new System.Drawing.Point(264, 65);
            this.btn_MasterPartsSeq.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_MasterPartsSeq.Name = "btn_MasterPartsSeq";
            this.btn_MasterPartsSeq.Size = new System.Drawing.Size(80, 21);
            this.btn_MasterPartsSeq.TabIndex = 423;
            this.btn_MasterPartsSeq.Text = "Off";
            this.btn_MasterPartsSeq.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_MasterPartsSeq.UseStyleColors = true;
            this.btn_MasterPartsSeq.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label7.Location = new System.Drawing.Point(7, 100);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 17);
            this.label7.TabIndex = 412;
            this.label7.Text = "Query Limits (DB)";
            // 
            // btn_LimitsDBFunc
            // 
            this.btn_LimitsDBFunc.AutoSize = true;
            this.btn_LimitsDBFunc.BackColor = System.Drawing.SystemColors.Control;
            this.btn_LimitsDBFunc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LimitsDBFunc.CustomBackground = true;
            this.btn_LimitsDBFunc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LimitsDBFunc.Location = new System.Drawing.Point(264, 97);
            this.btn_LimitsDBFunc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_LimitsDBFunc.Name = "btn_LimitsDBFunc";
            this.btn_LimitsDBFunc.Size = new System.Drawing.Size(80, 21);
            this.btn_LimitsDBFunc.TabIndex = 411;
            this.btn_LimitsDBFunc.Text = "Off";
            this.btn_LimitsDBFunc.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_LimitsDBFunc.UseStyleColors = true;
            this.btn_LimitsDBFunc.UseVisualStyleBackColor = false;
            // 
            // btn_StoreResultsFunc
            // 
            this.btn_StoreResultsFunc.AutoSize = true;
            this.btn_StoreResultsFunc.BackColor = System.Drawing.SystemColors.Control;
            this.btn_StoreResultsFunc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_StoreResultsFunc.CustomBackground = true;
            this.btn_StoreResultsFunc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_StoreResultsFunc.Location = new System.Drawing.Point(264, 316);
            this.btn_StoreResultsFunc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_StoreResultsFunc.Name = "btn_StoreResultsFunc";
            this.btn_StoreResultsFunc.Size = new System.Drawing.Size(80, 21);
            this.btn_StoreResultsFunc.TabIndex = 410;
            this.btn_StoreResultsFunc.Text = "Off";
            this.btn_StoreResultsFunc.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_StoreResultsFunc.UseStyleColors = true;
            this.btn_StoreResultsFunc.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label6.Location = new System.Drawing.Point(7, 319);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 17);
            this.label6.TabIndex = 409;
            this.label6.Text = "Store Results (DB)";
            // 
            // btn_DisableFuncAll
            // 
            this.btn_DisableFuncAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DisableFuncAll.Location = new System.Drawing.Point(282, 502);
            this.btn_DisableFuncAll.Margin = new System.Windows.Forms.Padding(4);
            this.btn_DisableFuncAll.Name = "btn_DisableFuncAll";
            this.btn_DisableFuncAll.Size = new System.Drawing.Size(109, 28);
            this.btn_DisableFuncAll.TabIndex = 407;
            this.btn_DisableFuncAll.Text = "Disable All";
            this.btn_DisableFuncAll.UseVisualStyleBackColor = true;
            this.btn_DisableFuncAll.Click += new System.EventHandler(this.btn_DisableFuncAll_Click);
            // 
            // btn_EnableFuncAll
            // 
            this.btn_EnableFuncAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EnableFuncAll.Location = new System.Drawing.Point(282, 467);
            this.btn_EnableFuncAll.Margin = new System.Windows.Forms.Padding(4);
            this.btn_EnableFuncAll.Name = "btn_EnableFuncAll";
            this.btn_EnableFuncAll.Size = new System.Drawing.Size(109, 28);
            this.btn_EnableFuncAll.TabIndex = 406;
            this.btn_EnableFuncAll.Text = "Enable All";
            this.btn_EnableFuncAll.UseVisualStyleBackColor = true;
            this.btn_EnableFuncAll.Click += new System.EventHandler(this.btn_EnableFuncAll_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label5.Location = new System.Drawing.Point(7, 381);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 17);
            this.label5.TabIndex = 407;
            this.label5.Text = "Update Device Status (DB)";
            // 
            // btn_StoreMeasureFunc
            // 
            this.btn_StoreMeasureFunc.AutoSize = true;
            this.btn_StoreMeasureFunc.BackColor = System.Drawing.SystemColors.Control;
            this.btn_StoreMeasureFunc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_StoreMeasureFunc.CustomBackground = true;
            this.btn_StoreMeasureFunc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_StoreMeasureFunc.Location = new System.Drawing.Point(264, 284);
            this.btn_StoreMeasureFunc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_StoreMeasureFunc.Name = "btn_StoreMeasureFunc";
            this.btn_StoreMeasureFunc.Size = new System.Drawing.Size(80, 21);
            this.btn_StoreMeasureFunc.TabIndex = 406;
            this.btn_StoreMeasureFunc.Text = "Off";
            this.btn_StoreMeasureFunc.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_StoreMeasureFunc.UseStyleColors = true;
            this.btn_StoreMeasureFunc.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(7, 287);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 17);
            this.label4.TabIndex = 405;
            this.label4.Text = "Store Measurements";
            // 
            // btn_Measure2Func
            // 
            this.btn_Measure2Func.AutoSize = true;
            this.btn_Measure2Func.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Measure2Func.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Measure2Func.CustomBackground = true;
            this.btn_Measure2Func.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Measure2Func.Location = new System.Drawing.Point(264, 252);
            this.btn_Measure2Func.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Measure2Func.Name = "btn_Measure2Func";
            this.btn_Measure2Func.Size = new System.Drawing.Size(80, 21);
            this.btn_Measure2Func.TabIndex = 404;
            this.btn_Measure2Func.Text = "Off";
            this.btn_Measure2Func.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_Measure2Func.UseStyleColors = true;
            this.btn_Measure2Func.UseVisualStyleBackColor = false;
            // 
            // btn_Measure1Func
            // 
            this.btn_Measure1Func.AutoSize = true;
            this.btn_Measure1Func.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Measure1Func.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Measure1Func.CustomBackground = true;
            this.btn_Measure1Func.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Measure1Func.Location = new System.Drawing.Point(264, 193);
            this.btn_Measure1Func.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Measure1Func.Name = "btn_Measure1Func";
            this.btn_Measure1Func.Size = new System.Drawing.Size(80, 21);
            this.btn_Measure1Func.TabIndex = 403;
            this.btn_Measure1Func.Text = "Off";
            this.btn_Measure1Func.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_Measure1Func.UseStyleColors = true;
            this.btn_Measure1Func.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(7, 255);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 17);
            this.label3.TabIndex = 402;
            this.label3.Text = "Torque - Angle Measurements";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(7, 196);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 401;
            this.label2.Text = "Visual Inspection";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(7, 164);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 17);
            this.label1.TabIndex = 400;
            this.label1.Text = "Prior Op Check (DB)";
            // 
            // btn_PriorOpChkFunc
            // 
            this.btn_PriorOpChkFunc.AutoSize = true;
            this.btn_PriorOpChkFunc.BackColor = System.Drawing.SystemColors.Control;
            this.btn_PriorOpChkFunc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_PriorOpChkFunc.CustomBackground = true;
            this.btn_PriorOpChkFunc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PriorOpChkFunc.Location = new System.Drawing.Point(264, 161);
            this.btn_PriorOpChkFunc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_PriorOpChkFunc.Name = "btn_PriorOpChkFunc";
            this.btn_PriorOpChkFunc.Size = new System.Drawing.Size(80, 21);
            this.btn_PriorOpChkFunc.TabIndex = 399;
            this.btn_PriorOpChkFunc.Text = "Off";
            this.btn_PriorOpChkFunc.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_PriorOpChkFunc.UseStyleColors = true;
            this.btn_PriorOpChkFunc.UseVisualStyleBackColor = false;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label65.Location = new System.Drawing.Point(7, 36);
            this.label65.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(67, 17);
            this.label65.TabIndex = 398;
            this.label65.Text = "Scanning";
            // 
            // btn_ScanningFunc
            // 
            this.btn_ScanningFunc.AutoSize = true;
            this.btn_ScanningFunc.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ScanningFunc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ScanningFunc.CustomBackground = true;
            this.btn_ScanningFunc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ScanningFunc.Location = new System.Drawing.Point(264, 33);
            this.btn_ScanningFunc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ScanningFunc.Name = "btn_ScanningFunc";
            this.btn_ScanningFunc.Size = new System.Drawing.Size(80, 21);
            this.btn_ScanningFunc.TabIndex = 397;
            this.btn_ScanningFunc.Text = "Off";
            this.btn_ScanningFunc.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_ScanningFunc.UseStyleColors = true;
            this.btn_ScanningFunc.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbx_PCON);
            this.groupBox1.Controls.Add(this.cbx_AssignComp);
            this.groupBox1.Controls.Add(this.btn_DisableMsgAll);
            this.groupBox1.Controls.Add(this.btn_EnableMsgAll);
            this.groupBox1.Controls.Add(this.cbx_CheckComp);
            this.groupBox1.Controls.Add(this.cbx_StoreResults);
            this.groupBox1.Controls.Add(this.cbx_Scanning);
            this.groupBox1.Controls.Add(this.cbx_SafetyController);
            this.groupBox1.Controls.Add(this.cbx_StoreMeasurements);
            this.groupBox1.Controls.Add(this.cbx_UpdateDeviceStatus);
            this.groupBox1.Controls.Add(this.cbx_Process3);
            this.groupBox1.Controls.Add(this.cbx_Process2);
            this.groupBox1.Controls.Add(this.cbx_Process1);
            this.groupBox1.Controls.Add(this.cbx_Kistler);
            this.groupBox1.Controls.Add(this.cbx_Instrument2);
            this.groupBox1.Controls.Add(this.cbx_Instrument1);
            this.groupBox1.Controls.Add(this.cbx_PLC);
            this.groupBox1.Controls.Add(this.cbx_PriorOpCheck);
            this.groupBox1.Location = new System.Drawing.Point(643, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(231, 537);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Messages Management";
            // 
            // cbx_AssignComp
            // 
            this.cbx_AssignComp.AutoSize = true;
            this.cbx_AssignComp.Checked = true;
            this.cbx_AssignComp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_AssignComp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_AssignComp.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_AssignComp.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_AssignComp.Location = new System.Drawing.Point(21, 63);
            this.cbx_AssignComp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_AssignComp.Name = "cbx_AssignComp";
            this.cbx_AssignComp.Size = new System.Drawing.Size(155, 21);
            this.cbx_AssignComp.TabIndex = 409;
            this.cbx_AssignComp.Text = "Assign Components";
            this.cbx_AssignComp.UseVisualStyleBackColor = true;
            // 
            // btn_DisableMsgAll
            // 
            this.btn_DisableMsgAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DisableMsgAll.Location = new System.Drawing.Point(119, 502);
            this.btn_DisableMsgAll.Margin = new System.Windows.Forms.Padding(4);
            this.btn_DisableMsgAll.Name = "btn_DisableMsgAll";
            this.btn_DisableMsgAll.Size = new System.Drawing.Size(109, 28);
            this.btn_DisableMsgAll.TabIndex = 407;
            this.btn_DisableMsgAll.Text = "Disable All";
            this.btn_DisableMsgAll.UseVisualStyleBackColor = true;
            this.btn_DisableMsgAll.Click += new System.EventHandler(this.btn_DisableMsgAll_Click);
            // 
            // btn_EnableMsgAll
            // 
            this.btn_EnableMsgAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EnableMsgAll.Location = new System.Drawing.Point(119, 466);
            this.btn_EnableMsgAll.Margin = new System.Windows.Forms.Padding(4);
            this.btn_EnableMsgAll.Name = "btn_EnableMsgAll";
            this.btn_EnableMsgAll.Size = new System.Drawing.Size(109, 28);
            this.btn_EnableMsgAll.TabIndex = 406;
            this.btn_EnableMsgAll.Text = "Enable All";
            this.btn_EnableMsgAll.UseVisualStyleBackColor = true;
            this.btn_EnableMsgAll.Click += new System.EventHandler(this.btn_EnableMsgAll_Click);
            // 
            // cbx_CheckComp
            // 
            this.cbx_CheckComp.AutoSize = true;
            this.cbx_CheckComp.Checked = true;
            this.cbx_CheckComp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_CheckComp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_CheckComp.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_CheckComp.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_CheckComp.Location = new System.Drawing.Point(21, 36);
            this.cbx_CheckComp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_CheckComp.Name = "cbx_CheckComp";
            this.cbx_CheckComp.Size = new System.Drawing.Size(152, 21);
            this.cbx_CheckComp.TabIndex = 408;
            this.cbx_CheckComp.Text = "Check Components";
            this.cbx_CheckComp.UseVisualStyleBackColor = true;
            // 
            // cbx_StoreResults
            // 
            this.cbx_StoreResults.AutoSize = true;
            this.cbx_StoreResults.Checked = true;
            this.cbx_StoreResults.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_StoreResults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_StoreResults.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_StoreResults.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_StoreResults.Location = new System.Drawing.Point(21, 198);
            this.cbx_StoreResults.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_StoreResults.Name = "cbx_StoreResults";
            this.cbx_StoreResults.Size = new System.Drawing.Size(115, 21);
            this.cbx_StoreResults.TabIndex = 11;
            this.cbx_StoreResults.Text = "Store Results";
            this.cbx_StoreResults.UseVisualStyleBackColor = true;
            // 
            // cbx_Scanning
            // 
            this.cbx_Scanning.AutoSize = true;
            this.cbx_Scanning.Checked = true;
            this.cbx_Scanning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Scanning.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_Scanning.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_Scanning.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_Scanning.Location = new System.Drawing.Point(21, 171);
            this.cbx_Scanning.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_Scanning.Name = "cbx_Scanning";
            this.cbx_Scanning.Size = new System.Drawing.Size(89, 21);
            this.cbx_Scanning.TabIndex = 10;
            this.cbx_Scanning.Text = "Scanning";
            this.cbx_Scanning.UseVisualStyleBackColor = true;
            // 
            // cbx_SafetyController
            // 
            this.cbx_SafetyController.AutoSize = true;
            this.cbx_SafetyController.Checked = true;
            this.cbx_SafetyController.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_SafetyController.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_SafetyController.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_SafetyController.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_SafetyController.Location = new System.Drawing.Point(21, 252);
            this.cbx_SafetyController.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_SafetyController.Name = "cbx_SafetyController";
            this.cbx_SafetyController.Size = new System.Drawing.Size(135, 21);
            this.cbx_SafetyController.TabIndex = 9;
            this.cbx_SafetyController.Text = "Safety Controller";
            this.cbx_SafetyController.UseVisualStyleBackColor = true;
            // 
            // cbx_StoreMeasurements
            // 
            this.cbx_StoreMeasurements.AutoSize = true;
            this.cbx_StoreMeasurements.Checked = true;
            this.cbx_StoreMeasurements.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_StoreMeasurements.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_StoreMeasurements.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_StoreMeasurements.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_StoreMeasurements.Location = new System.Drawing.Point(21, 144);
            this.cbx_StoreMeasurements.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_StoreMeasurements.Name = "cbx_StoreMeasurements";
            this.cbx_StoreMeasurements.Size = new System.Drawing.Size(161, 21);
            this.cbx_StoreMeasurements.TabIndex = 8;
            this.cbx_StoreMeasurements.Text = "Store Measurements";
            this.cbx_StoreMeasurements.UseVisualStyleBackColor = true;
            // 
            // cbx_UpdateDeviceStatus
            // 
            this.cbx_UpdateDeviceStatus.AutoSize = true;
            this.cbx_UpdateDeviceStatus.Checked = true;
            this.cbx_UpdateDeviceStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_UpdateDeviceStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_UpdateDeviceStatus.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_UpdateDeviceStatus.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_UpdateDeviceStatus.Location = new System.Drawing.Point(21, 117);
            this.cbx_UpdateDeviceStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_UpdateDeviceStatus.Name = "cbx_UpdateDeviceStatus";
            this.cbx_UpdateDeviceStatus.Size = new System.Drawing.Size(167, 21);
            this.cbx_UpdateDeviceStatus.TabIndex = 7;
            this.cbx_UpdateDeviceStatus.Text = "Update Device Status";
            this.cbx_UpdateDeviceStatus.UseVisualStyleBackColor = true;
            // 
            // cbx_Process3
            // 
            this.cbx_Process3.AutoSize = true;
            this.cbx_Process3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_Process3.Enabled = false;
            this.cbx_Process3.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_Process3.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_Process3.Location = new System.Drawing.Point(20, 438);
            this.cbx_Process3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_Process3.Name = "cbx_Process3";
            this.cbx_Process3.Size = new System.Drawing.Size(93, 21);
            this.cbx_Process3.TabIndex = 6;
            this.cbx_Process3.Text = "Process 3";
            this.cbx_Process3.UseVisualStyleBackColor = true;
            // 
            // cbx_Process2
            // 
            this.cbx_Process2.AutoSize = true;
            this.cbx_Process2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_Process2.Enabled = false;
            this.cbx_Process2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_Process2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_Process2.Location = new System.Drawing.Point(20, 411);
            this.cbx_Process2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_Process2.Name = "cbx_Process2";
            this.cbx_Process2.Size = new System.Drawing.Size(93, 21);
            this.cbx_Process2.TabIndex = 5;
            this.cbx_Process2.Text = "Process 2";
            this.cbx_Process2.UseVisualStyleBackColor = true;
            // 
            // cbx_Process1
            // 
            this.cbx_Process1.AutoSize = true;
            this.cbx_Process1.Checked = true;
            this.cbx_Process1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Process1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_Process1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_Process1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_Process1.Location = new System.Drawing.Point(20, 384);
            this.cbx_Process1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_Process1.Name = "cbx_Process1";
            this.cbx_Process1.Size = new System.Drawing.Size(93, 21);
            this.cbx_Process1.TabIndex = 4;
            this.cbx_Process1.Text = "Process 1";
            this.cbx_Process1.UseVisualStyleBackColor = true;
            // 
            // cbx_Kistler
            // 
            this.cbx_Kistler.AutoSize = true;
            this.cbx_Kistler.Checked = true;
            this.cbx_Kistler.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Kistler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_Kistler.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_Kistler.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_Kistler.Location = new System.Drawing.Point(21, 334);
            this.cbx_Kistler.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_Kistler.Name = "cbx_Kistler";
            this.cbx_Kistler.Size = new System.Drawing.Size(92, 21);
            this.cbx_Kistler.TabIndex = 3;
            this.cbx_Kistler.Text = "Kistler NC";
            this.cbx_Kistler.UseVisualStyleBackColor = true;
            // 
            // cbx_Instrument2
            // 
            this.cbx_Instrument2.AutoSize = true;
            this.cbx_Instrument2.Checked = true;
            this.cbx_Instrument2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Instrument2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_Instrument2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_Instrument2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_Instrument2.Location = new System.Drawing.Point(21, 306);
            this.cbx_Instrument2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_Instrument2.Name = "cbx_Instrument2";
            this.cbx_Instrument2.Size = new System.Drawing.Size(191, 21);
            this.cbx_Instrument2.TabIndex = 3;
            this.cbx_Instrument2.Text = "Vision Sensor (IV2-G500)\r\n";
            this.cbx_Instrument2.UseVisualStyleBackColor = true;
            // 
            // cbx_Instrument1
            // 
            this.cbx_Instrument1.AutoSize = true;
            this.cbx_Instrument1.Checked = true;
            this.cbx_Instrument1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_Instrument1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_Instrument1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_Instrument1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_Instrument1.Location = new System.Drawing.Point(21, 279);
            this.cbx_Instrument1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_Instrument1.Name = "cbx_Instrument1";
            this.cbx_Instrument1.Size = new System.Drawing.Size(124, 21);
            this.cbx_Instrument1.TabIndex = 2;
            this.cbx_Instrument1.Text = "MTFocus 6000";
            this.cbx_Instrument1.UseVisualStyleBackColor = true;
            // 
            // cbx_PLC
            // 
            this.cbx_PLC.AutoSize = true;
            this.cbx_PLC.Checked = true;
            this.cbx_PLC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_PLC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_PLC.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_PLC.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_PLC.Location = new System.Drawing.Point(21, 225);
            this.cbx_PLC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_PLC.Name = "cbx_PLC";
            this.cbx_PLC.Size = new System.Drawing.Size(56, 21);
            this.cbx_PLC.TabIndex = 1;
            this.cbx_PLC.Text = "PLC";
            this.cbx_PLC.UseVisualStyleBackColor = true;
            // 
            // cbx_PriorOpCheck
            // 
            this.cbx_PriorOpCheck.AutoSize = true;
            this.cbx_PriorOpCheck.Checked = true;
            this.cbx_PriorOpCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_PriorOpCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_PriorOpCheck.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_PriorOpCheck.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_PriorOpCheck.Location = new System.Drawing.Point(21, 90);
            this.cbx_PriorOpCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_PriorOpCheck.Name = "cbx_PriorOpCheck";
            this.cbx_PriorOpCheck.Size = new System.Drawing.Size(126, 21);
            this.cbx_PriorOpCheck.TabIndex = 0;
            this.cbx_PriorOpCheck.Text = "Prior Op Check";
            this.cbx_PriorOpCheck.UseVisualStyleBackColor = true;
            // 
            // btn_ForceDisplacement
            // 
            this.btn_ForceDisplacement.AutoSize = true;
            this.btn_ForceDisplacement.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ForceDisplacement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ForceDisplacement.CustomBackground = true;
            this.btn_ForceDisplacement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ForceDisplacement.Location = new System.Drawing.Point(264, 223);
            this.btn_ForceDisplacement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ForceDisplacement.Name = "btn_ForceDisplacement";
            this.btn_ForceDisplacement.Size = new System.Drawing.Size(80, 21);
            this.btn_ForceDisplacement.TabIndex = 430;
            this.btn_ForceDisplacement.Text = "Off";
            this.btn_ForceDisplacement.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btn_ForceDisplacement.UseStyleColors = true;
            this.btn_ForceDisplacement.UseVisualStyleBackColor = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label11.Location = new System.Drawing.Point(7, 226);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(239, 17);
            this.label11.TabIndex = 429;
            this.label11.Text = "Force - Displacement Measurements";
            // 
            // cbx_PCON
            // 
            this.cbx_PCON.AutoSize = true;
            this.cbx_PCON.Checked = true;
            this.cbx_PCON.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_PCON.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbx_PCON.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cbx_PCON.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cbx_PCON.Location = new System.Drawing.Point(21, 359);
            this.cbx_PCON.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbx_PCON.Name = "cbx_PCON";
            this.cbx_PCON.Size = new System.Drawing.Size(160, 21);
            this.cbx_PCON.TabIndex = 410;
            this.cbx_PCON.Text = "Servo Press (PCON)";
            this.cbx_PCON.UseVisualStyleBackColor = true;
            // 
            // Engineering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 690);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Engineering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[Station Name] | Engineering";
            this.tabControl1.ResumeLayout(false);
            this.tab_Config.ResumeLayout(false);
            this.pnl_FuncManagement.ResumeLayout(false);
            this.pnl_FuncManagement.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer MachineStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_Config;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbx_SafetyController;
        private System.Windows.Forms.CheckBox cbx_StoreMeasurements;
        private System.Windows.Forms.CheckBox cbx_UpdateDeviceStatus;
        private System.Windows.Forms.CheckBox cbx_Process3;
        private System.Windows.Forms.CheckBox cbx_Process2;
        private System.Windows.Forms.CheckBox cbx_Process1;
        private System.Windows.Forms.CheckBox cbx_Kistler;
        private System.Windows.Forms.CheckBox cbx_Instrument2;
        private System.Windows.Forms.CheckBox cbx_Instrument1;
        private System.Windows.Forms.CheckBox cbx_PLC;
        private System.Windows.Forms.CheckBox cbx_PriorOpCheck;
        private System.Windows.Forms.CheckBox cbx_StoreResults;
        private System.Windows.Forms.CheckBox cbx_Scanning;
        private System.Windows.Forms.GroupBox pnl_FuncManagement;
        private MetroFramework.Controls.MetroToggle btn_ScanningFunc;
        private MetroFramework.Controls.MetroToggle btn_UpdateDevStsFunc;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroToggle btn_StoreMeasureFunc;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroToggle btn_Measure2Func;
        private MetroFramework.Controls.MetroToggle btn_Measure1Func;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroToggle btn_PriorOpChkFunc;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Button btn_DisableFuncAll;
        private System.Windows.Forms.Button btn_EnableFuncAll;
        private System.Windows.Forms.Button btn_DisableMsgAll;
        private System.Windows.Forms.Button btn_EnableMsgAll;
        private MetroFramework.Controls.MetroToggle btn_StoreResultsFunc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private MetroFramework.Controls.MetroToggle btn_LimitsDBFunc;
        private System.Windows.Forms.Label label10;
        private MetroFramework.Controls.MetroToggle btn_MasterPartsSeq;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroToggle btn_AssignComp;
        private System.Windows.Forms.Label label8;
        private MetroFramework.Controls.MetroToggle btn_ChkCompFunc;
        private System.Windows.Forms.CheckBox cbx_AssignComp;
        private System.Windows.Forms.CheckBox cbx_CheckComp;
        private MetroFramework.Controls.MetroToggle btn_ForceDisplacement;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbx_PCON;
    }
}