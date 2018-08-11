namespace ContractPayroll.Forms
{
    partial class frmMthlyAttdProcess
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtPayPeriod = new DevExpress.XtraEditors.TextEdit();
            this.txtPayDesc = new DevExpress.XtraEditors.TextEdit();
            this.btnProcess = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmpUnqID = new DevExpress.XtraEditors.TextEdit();
            this.txtContName = new DevExpress.XtraEditors.TextEdit();
            this.txtEmpName = new DevExpress.XtraEditors.TextEdit();
            this.txtContCode = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpUnqID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 54;
            this.label3.Text = "PayPeriod :";
            // 
            // txtPayPeriod
            // 
            this.txtPayPeriod.Location = new System.Drawing.Point(83, 16);
            this.txtPayPeriod.Name = "txtPayPeriod";
            this.txtPayPeriod.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayPeriod.Properties.Appearance.Options.UseFont = true;
            this.txtPayPeriod.Properties.MaxLength = 10;
            this.txtPayPeriod.Size = new System.Drawing.Size(88, 22);
            this.txtPayPeriod.TabIndex = 0;
            this.txtPayPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayPeriod_KeyDown);
            this.txtPayPeriod.Validated += new System.EventHandler(this.txtPayPeriod_Validated);
            // 
            // txtPayDesc
            // 
            this.txtPayDesc.Location = new System.Drawing.Point(177, 16);
            this.txtPayDesc.Name = "txtPayDesc";
            this.txtPayDesc.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayDesc.Properties.Appearance.Options.UseFont = true;
            this.txtPayDesc.Properties.Mask.EditMask = "[A-Za-z 0-9]+";
            this.txtPayDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPayDesc.Properties.MaxLength = 100;
            this.txtPayDesc.Properties.ReadOnly = true;
            this.txtPayDesc.Size = new System.Drawing.Size(377, 22);
            this.txtPayDesc.TabIndex = 1;
            this.txtPayDesc.TabStop = false;
            // 
            // btnProcess
            // 
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.Location = new System.Drawing.Point(177, 112);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(238, 42);
            this.btnProcess.TabIndex = 6;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(21, 176);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(591, 35);
            this.pBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBar.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 65;
            this.label1.Text = "EmpUnqID :";
            // 
            // txtEmpUnqID
            // 
            this.txtEmpUnqID.Location = new System.Drawing.Point(83, 44);
            this.txtEmpUnqID.Name = "txtEmpUnqID";
            this.txtEmpUnqID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpUnqID.Properties.Appearance.Options.UseFont = true;
            this.txtEmpUnqID.Properties.Mask.EditMask = "[0-9]+";
            this.txtEmpUnqID.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtEmpUnqID.Properties.MaxLength = 10;
            this.txtEmpUnqID.Size = new System.Drawing.Size(88, 22);
            this.txtEmpUnqID.TabIndex = 2;
            this.txtEmpUnqID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmpUnqID_KeyDown);
            this.txtEmpUnqID.Validated += new System.EventHandler(this.txtEmpUnqID_Validated);
            // 
            // txtContName
            // 
            this.txtContName.Location = new System.Drawing.Point(177, 72);
            this.txtContName.Name = "txtContName";
            this.txtContName.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContName.Properties.Appearance.Options.UseFont = true;
            this.txtContName.Properties.Mask.EditMask = "[A-Za-z 0-9]+";
            this.txtContName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtContName.Properties.MaxLength = 100;
            this.txtContName.Properties.ReadOnly = true;
            this.txtContName.Size = new System.Drawing.Size(377, 22);
            this.txtContName.TabIndex = 5;
            this.txtContName.TabStop = false;
            // 
            // txtEmpName
            // 
            this.txtEmpName.Location = new System.Drawing.Point(177, 44);
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpName.Properties.Appearance.Options.UseFont = true;
            this.txtEmpName.Properties.Mask.EditMask = "[A-Za-z 0-9]+";
            this.txtEmpName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtEmpName.Properties.MaxLength = 100;
            this.txtEmpName.Properties.ReadOnly = true;
            this.txtEmpName.Size = new System.Drawing.Size(377, 22);
            this.txtEmpName.TabIndex = 3;
            this.txtEmpName.TabStop = false;
            // 
            // txtContCode
            // 
            this.txtContCode.Location = new System.Drawing.Point(83, 72);
            this.txtContCode.Name = "txtContCode";
            this.txtContCode.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContCode.Properties.Appearance.Options.UseFont = true;
            this.txtContCode.Properties.Mask.EditMask = "[A-Z]+";
            this.txtContCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtContCode.Properties.MaxLength = 10;
            this.txtContCode.Size = new System.Drawing.Size(88, 22);
            this.txtContCode.TabIndex = 4;
            this.txtContCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContCode_KeyDown);
            this.txtContCode.Validated += new System.EventHandler(this.txtContCode_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 69;
            this.label2.Text = "ContCode :";
            // 
            // frmMthlyAttdProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 225);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtContName);
            this.Controls.Add(this.txtEmpName);
            this.Controls.Add(this.txtContCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmpUnqID);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPayPeriod);
            this.Controls.Add(this.txtPayDesc);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMthlyAttdProcess";
            this.Text = "Monthly Attendance Process";
            this.Load += new System.EventHandler(this.frmMthlyAttdProcess_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpUnqID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtPayPeriod;
        private DevExpress.XtraEditors.TextEdit txtPayDesc;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtEmpUnqID;
        private DevExpress.XtraEditors.TextEdit txtContName;
        private DevExpress.XtraEditors.TextEdit txtEmpName;
        private DevExpress.XtraEditors.TextEdit txtContCode;
        private System.Windows.Forms.Label label2;
    }
}