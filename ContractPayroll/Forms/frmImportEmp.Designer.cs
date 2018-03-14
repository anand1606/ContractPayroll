namespace ContractPayroll.Forms
{
    partial class frmImportEmp
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
            this.btnImport = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmpUnqID = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpUnqID.Properties)).BeginInit();
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
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(177, 73);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(238, 42);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(7, 121);
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
            this.label1.TabIndex = 61;
            this.label1.Text = "EmpUnqID :";
            // 
            // txtEmpUnqID
            // 
            this.txtEmpUnqID.Location = new System.Drawing.Point(83, 44);
            this.txtEmpUnqID.Name = "txtEmpUnqID";
            this.txtEmpUnqID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpUnqID.Properties.Appearance.Options.UseFont = true;
            this.txtEmpUnqID.Properties.MaxLength = 10;
            this.txtEmpUnqID.Size = new System.Drawing.Size(88, 22);
            this.txtEmpUnqID.TabIndex = 60;
            // 
            // frmImportEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 170);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmpUnqID);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPayPeriod);
            this.Controls.Add(this.txtPayDesc);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmImportEmp";
            this.Text = "Import Employee to Pay Cycle";
            this.Load += new System.EventHandler(this.frmImportEmp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpUnqID.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtPayPeriod;
        private DevExpress.XtraEditors.TextEdit txtPayDesc;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ProgressBar pBar;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtEmpUnqID;
    }
}