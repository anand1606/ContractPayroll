namespace ContractPayroll
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDBConn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreateUser = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUserRights = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOtherConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUser = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangePass = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogOff = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMast = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayPeriod = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPayCyclePara = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportEmp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMastEmp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBulkBasicChng = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBulkSPLChng = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBulkBAChng = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTranS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportAttd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMthlyAdj = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMthlyDed = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMthlyAttdProc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMthlyCalc = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRptMthlySalReg = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRptMthlyTPAReg = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRptOthers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUserID = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUserDesc = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsExtra = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAdmin,
            this.mnuUser,
            this.mnuMast,
            this.mnuTranS,
            this.reportsToolStripMenuItem,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1100, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuAdmin
            // 
            this.mnuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDBConn,
            this.mnuCreateUser,
            this.mnuUserRights,
            this.mnuOtherConfig});
            this.mnuAdmin.Name = "mnuAdmin";
            this.mnuAdmin.Size = new System.Drawing.Size(55, 20);
            this.mnuAdmin.Text = "&Admin";
            // 
            // mnuDBConn
            // 
            this.mnuDBConn.Name = "mnuDBConn";
            this.mnuDBConn.Size = new System.Drawing.Size(187, 22);
            this.mnuDBConn.Text = "Database Connection";
            this.mnuDBConn.Click += new System.EventHandler(this.mnuDBConn_Click);
            // 
            // mnuCreateUser
            // 
            this.mnuCreateUser.Name = "mnuCreateUser";
            this.mnuCreateUser.Size = new System.Drawing.Size(187, 22);
            this.mnuCreateUser.Text = "Create User";
            this.mnuCreateUser.Click += new System.EventHandler(this.mnuCreateUser_Click);
            // 
            // mnuUserRights
            // 
            this.mnuUserRights.Name = "mnuUserRights";
            this.mnuUserRights.Size = new System.Drawing.Size(187, 22);
            this.mnuUserRights.Text = "User Rights";
            this.mnuUserRights.Click += new System.EventHandler(this.mnuUserRights_Click);
            // 
            // mnuOtherConfig
            // 
            this.mnuOtherConfig.Name = "mnuOtherConfig";
            this.mnuOtherConfig.Size = new System.Drawing.Size(187, 22);
            this.mnuOtherConfig.Text = "Default Parameters";
            this.mnuOtherConfig.Click += new System.EventHandler(this.mnuOtherConfig_Click);
            // 
            // mnuUser
            // 
            this.mnuUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChangePass,
            this.mnuLogOff});
            this.mnuUser.Name = "mnuUser";
            this.mnuUser.Size = new System.Drawing.Size(71, 20);
            this.mnuUser.Text = "&Users Info";
            // 
            // mnuChangePass
            // 
            this.mnuChangePass.Name = "mnuChangePass";
            this.mnuChangePass.Size = new System.Drawing.Size(168, 22);
            this.mnuChangePass.Text = "Change &Password";
            this.mnuChangePass.Click += new System.EventHandler(this.mnuChangePass_Click);
            // 
            // mnuLogOff
            // 
            this.mnuLogOff.Name = "mnuLogOff";
            this.mnuLogOff.Size = new System.Drawing.Size(168, 22);
            this.mnuLogOff.Text = "&Log Off";
            this.mnuLogOff.Click += new System.EventHandler(this.mnuLogOff_Click);
            // 
            // mnuMast
            // 
            this.mnuMast.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPayPeriod,
            this.mnuPayCyclePara,
            this.mnuImportEmp,
            this.mnuMastEmp,
            this.mnuBulkBasicChng,
            this.mnuBulkSPLChng,
            this.mnuBulkBAChng});
            this.mnuMast.Name = "mnuMast";
            this.mnuMast.Size = new System.Drawing.Size(60, 20);
            this.mnuMast.Text = "&Masters";
            // 
            // mnuPayPeriod
            // 
            this.mnuPayPeriod.Name = "mnuPayPeriod";
            this.mnuPayPeriod.Size = new System.Drawing.Size(232, 22);
            this.mnuPayPeriod.Text = "Pay Cycle";
            this.mnuPayPeriod.Click += new System.EventHandler(this.mnuPayPeriod_Click);
            // 
            // mnuPayCyclePara
            // 
            this.mnuPayCyclePara.Name = "mnuPayCyclePara";
            this.mnuPayCyclePara.Size = new System.Drawing.Size(232, 22);
            this.mnuPayCyclePara.Text = "Pay CyclePara";
            this.mnuPayCyclePara.Click += new System.EventHandler(this.mnuPayCyclePara_Click);
            // 
            // mnuImportEmp
            // 
            this.mnuImportEmp.Name = "mnuImportEmp";
            this.mnuImportEmp.Size = new System.Drawing.Size(232, 22);
            this.mnuImportEmp.Text = "Import Employee To PayCycle";
            this.mnuImportEmp.Click += new System.EventHandler(this.mnuImportEmp_Click);
            // 
            // mnuMastEmp
            // 
            this.mnuMastEmp.Name = "mnuMastEmp";
            this.mnuMastEmp.Size = new System.Drawing.Size(232, 22);
            this.mnuMastEmp.Text = "Employee Master";
            this.mnuMastEmp.Click += new System.EventHandler(this.mnuMastEmp_Click);
            // 
            // mnuBulkBasicChng
            // 
            this.mnuBulkBasicChng.Name = "mnuBulkBasicChng";
            this.mnuBulkBasicChng.Size = new System.Drawing.Size(232, 22);
            this.mnuBulkBasicChng.Text = "Bulk Wages Change";
            this.mnuBulkBasicChng.Click += new System.EventHandler(this.mnuBulkBasicChng_Click);
            // 
            // mnuBulkSPLChng
            // 
            this.mnuBulkSPLChng.Name = "mnuBulkSPLChng";
            this.mnuBulkSPLChng.Size = new System.Drawing.Size(232, 22);
            this.mnuBulkSPLChng.Text = "Bulk Special All Change";
            this.mnuBulkSPLChng.Click += new System.EventHandler(this.mnuBulkSPLChng_Click);
            // 
            // mnuBulkBAChng
            // 
            this.mnuBulkBAChng.Name = "mnuBulkBAChng";
            this.mnuBulkBAChng.Size = new System.Drawing.Size(232, 22);
            this.mnuBulkBAChng.Text = "Bulk BA All Change";
            this.mnuBulkBAChng.Click += new System.EventHandler(this.mnuBulkBAChng_Click);
            // 
            // mnuTranS
            // 
            this.mnuTranS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImportAttd,
            this.mnuMthlyAdj,
            this.mnuMthlyDed,
            this.mnuMthlyAttdProc,
            this.mnuMthlyCalc});
            this.mnuTranS.Name = "mnuTranS";
            this.mnuTranS.Size = new System.Drawing.Size(80, 20);
            this.mnuTranS.Text = "&Transaction";
            // 
            // mnuImportAttd
            // 
            this.mnuImportAttd.Name = "mnuImportAttd";
            this.mnuImportAttd.Size = new System.Drawing.Size(232, 22);
            this.mnuImportAttd.Text = "Import Daily Attendance";
            this.mnuImportAttd.Click += new System.EventHandler(this.mnuImportAttd_Click);
            // 
            // mnuMthlyAdj
            // 
            this.mnuMthlyAdj.Name = "mnuMthlyAdj";
            this.mnuMthlyAdj.Size = new System.Drawing.Size(232, 22);
            this.mnuMthlyAdj.Text = "Monthly Adjustment";
            this.mnuMthlyAdj.Click += new System.EventHandler(this.mnuMthlyAdj_Click);
            // 
            // mnuMthlyDed
            // 
            this.mnuMthlyDed.Name = "mnuMthlyDed";
            this.mnuMthlyDed.Size = new System.Drawing.Size(232, 22);
            this.mnuMthlyDed.Text = "Monthly Deduction Upload";
            this.mnuMthlyDed.Click += new System.EventHandler(this.mnuMthlyDed_Click);
            // 
            // mnuMthlyAttdProc
            // 
            this.mnuMthlyAttdProc.Name = "mnuMthlyAttdProc";
            this.mnuMthlyAttdProc.Size = new System.Drawing.Size(232, 22);
            this.mnuMthlyAttdProc.Text = "Monthly Attendance Proccess";
            this.mnuMthlyAttdProc.Click += new System.EventHandler(this.mnuMthlyAttdProc_Click);
            // 
            // mnuMthlyCalc
            // 
            this.mnuMthlyCalc.Name = "mnuMthlyCalc";
            this.mnuMthlyCalc.Size = new System.Drawing.Size(232, 22);
            this.mnuMthlyCalc.Text = "Salary Calculation";
            this.mnuMthlyCalc.Click += new System.EventHandler(this.mnuMthlyCalc_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRptMthlySalReg,
            this.mnuRptMthlyTPAReg,
            this.mnuRptOthers});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // mnuRptMthlySalReg
            // 
            this.mnuRptMthlySalReg.Name = "mnuRptMthlySalReg";
            this.mnuRptMthlySalReg.Size = new System.Drawing.Size(198, 22);
            this.mnuRptMthlySalReg.Text = "Monthly Salary Register";
            this.mnuRptMthlySalReg.Click += new System.EventHandler(this.mnuRptMthlySalReg_Click);
            // 
            // mnuRptMthlyTPAReg
            // 
            this.mnuRptMthlyTPAReg.Name = "mnuRptMthlyTPAReg";
            this.mnuRptMthlyTPAReg.Size = new System.Drawing.Size(198, 22);
            this.mnuRptMthlyTPAReg.Text = "Monthly TPA Register";
            this.mnuRptMthlyTPAReg.Click += new System.EventHandler(this.mnuRptMthlyTPAReg_Click);
            // 
            // mnuRptOthers
            // 
            this.mnuRptOthers.Name = "mnuRptOthers";
            this.mnuRptOthers.Size = new System.Drawing.Size(198, 22);
            this.mnuRptOthers.Text = "Other Reports";
            this.mnuRptOthers.Click += new System.EventHandler(this.mnuRptOthers_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.stsUserID,
            this.toolStripStatusLabel3,
            this.stsUserDesc,
            this.stsExtra});
            this.statusStrip1.Location = new System.Drawing.Point(0, 678);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1100, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel1.Text = "User ID : ";
            // 
            // stsUserID
            // 
            this.stsUserID.Name = "stsUserID";
            this.stsUserID.Size = new System.Drawing.Size(49, 17);
            this.stsUserID.Text = "GUserID";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(74, 17);
            this.toolStripStatusLabel3.Text = "User Name : ";
            // 
            // stsUserDesc
            // 
            this.stsUserDesc.Name = "stsUserDesc";
            this.stsUserDesc.Size = new System.Drawing.Size(70, 17);
            this.stsUserDesc.Text = "GUserName";
            // 
            // stsExtra
            // 
            this.stsExtra.Name = "stsExtra";
            this.stsExtra.Size = new System.Drawing.Size(0, 17);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Contract Payroll System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuAdmin;
        private System.Windows.Forms.ToolStripMenuItem mnuCreateUser;
        private System.Windows.Forms.ToolStripMenuItem mnuUserRights;
        private System.Windows.Forms.ToolStripMenuItem mnuUser;
        private System.Windows.Forms.ToolStripMenuItem mnuChangePass;
        private System.Windows.Forms.ToolStripMenuItem mnuLogOff;
        private System.Windows.Forms.ToolStripMenuItem mnuMast;
        private System.Windows.Forms.ToolStripMenuItem mnuTranS;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel stsUserID;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel stsUserDesc;
        private System.Windows.Forms.ToolStripStatusLabel stsExtra;
        private System.Windows.Forms.ToolStripMenuItem mnuDBConn;
        private System.Windows.Forms.ToolStripMenuItem mnuOtherConfig;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuPayPeriod;
        private System.Windows.Forms.ToolStripMenuItem mnuImportAttd;
        private System.Windows.Forms.ToolStripMenuItem mnuPayCyclePara;
        private System.Windows.Forms.ToolStripMenuItem mnuMastEmp;
        private System.Windows.Forms.ToolStripMenuItem mnuMthlyCalc;
        private System.Windows.Forms.ToolStripMenuItem mnuImportEmp;
        private System.Windows.Forms.ToolStripMenuItem mnuMthlyAdj;
        private System.Windows.Forms.ToolStripMenuItem mnuMthlyAttdProc;
        private System.Windows.Forms.ToolStripMenuItem mnuMthlyDed;
        private System.Windows.Forms.ToolStripMenuItem mnuBulkBasicChng;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRptMthlySalReg;
        private System.Windows.Forms.ToolStripMenuItem mnuRptMthlyTPAReg;
        private System.Windows.Forms.ToolStripMenuItem mnuRptOthers;
        private System.Windows.Forms.ToolStripMenuItem mnuBulkSPLChng;
        private System.Windows.Forms.ToolStripMenuItem mnuBulkBAChng;


    }
}
