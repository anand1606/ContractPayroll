namespace ContractPayroll.Forms
{
    partial class frmImportAttd
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
            this.txtParaDesc = new DevExpress.XtraEditors.TextEdit();
            this.btnImport = new System.Windows.Forms.Button();
            this.pBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParaDesc.Properties)).BeginInit();
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
            // txtParaDesc
            // 
            this.txtParaDesc.Location = new System.Drawing.Point(177, 16);
            this.txtParaDesc.Name = "txtParaDesc";
            this.txtParaDesc.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParaDesc.Properties.Appearance.Options.UseFont = true;
            this.txtParaDesc.Properties.Mask.EditMask = "[A-Za-z 0-9]+";
            this.txtParaDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtParaDesc.Properties.MaxLength = 100;
            this.txtParaDesc.Properties.ReadOnly = true;
            this.txtParaDesc.Size = new System.Drawing.Size(377, 22);
            this.txtParaDesc.TabIndex = 1;
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(185, 44);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(238, 42);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(15, 92);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(591, 35);
            this.pBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBar.TabIndex = 56;
            // 
            // frmImportAttd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 148);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPayPeriod);
            this.Controls.Add(this.txtParaDesc);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmImportAttd";
            this.Text = "Import Attendance to Pay Cycle";
            this.Load += new System.EventHandler(this.frmImportEmp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParaDesc.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtPayPeriod;
        private DevExpress.XtraEditors.TextEdit txtParaDesc;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ProgressBar pBar;
    }
}