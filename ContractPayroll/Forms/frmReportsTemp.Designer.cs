namespace ContractPayroll.Forms
{
    partial class frmReportsTemp
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
            this.btnRun = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPayPeriod = new DevExpress.XtraEditors.TextEdit();
            this.txtPayDesc = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtToDt = new DevExpress.XtraEditors.DateEdit();
            this.txtFromDt = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtContCode = new DevExpress.XtraEditors.TextEdit();
            this.chkBreak = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDt.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(109, 181);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(317, 52);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 58;
            this.label3.Text = "PayPeriod :";
            // 
            // txtPayPeriod
            // 
            this.txtPayPeriod.Location = new System.Drawing.Point(111, 15);
            this.txtPayPeriod.Margin = new System.Windows.Forms.Padding(4);
            this.txtPayPeriod.Name = "txtPayPeriod";
            this.txtPayPeriod.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayPeriod.Properties.Appearance.Options.UseFont = true;
            this.txtPayPeriod.Properties.MaxLength = 10;
            this.txtPayPeriod.Size = new System.Drawing.Size(115, 22);
            this.txtPayPeriod.TabIndex = 0;
            this.txtPayPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayPeriod_KeyDown);
            this.txtPayPeriod.Validated += new System.EventHandler(this.txtPayPeriod_Validated);
            // 
            // txtPayDesc
            // 
            this.txtPayDesc.Location = new System.Drawing.Point(111, 45);
            this.txtPayDesc.Margin = new System.Windows.Forms.Padding(4);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 59;
            this.label1.Text = "Period Desc.:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(264, 79);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 17);
            this.label17.TabIndex = 65;
            this.label17.Text = "To Date :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 76);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 17);
            this.label18.TabIndex = 64;
            this.label18.Text = "From Date :";
            // 
            // txtToDt
            // 
            this.txtToDt.EditValue = null;
            this.txtToDt.Location = new System.Drawing.Point(355, 75);
            this.txtToDt.Margin = new System.Windows.Forms.Padding(4);
            this.txtToDt.Name = "txtToDt";
            this.txtToDt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtToDt.Properties.Appearance.Options.UseFont = true;
            this.txtToDt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtToDt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtToDt.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtToDt.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtToDt.Properties.ReadOnly = true;
            this.txtToDt.Size = new System.Drawing.Size(133, 22);
            this.txtToDt.TabIndex = 3;
            this.txtToDt.TabStop = false;
            // 
            // txtFromDt
            // 
            this.txtFromDt.EditValue = null;
            this.txtFromDt.Location = new System.Drawing.Point(111, 75);
            this.txtFromDt.Margin = new System.Windows.Forms.Padding(4);
            this.txtFromDt.Name = "txtFromDt";
            this.txtFromDt.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFromDt.Properties.Appearance.Options.UseFont = true;
            this.txtFromDt.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFromDt.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFromDt.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.txtFromDt.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.txtFromDt.Properties.ReadOnly = true;
            this.txtFromDt.Size = new System.Drawing.Size(133, 22);
            this.txtFromDt.TabIndex = 2;
            this.txtFromDt.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 106);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 67;
            this.label2.Text = "ContCode :";
            // 
            // txtContCode
            // 
            this.txtContCode.Location = new System.Drawing.Point(111, 105);
            this.txtContCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtContCode.Name = "txtContCode";
            this.txtContCode.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContCode.Properties.Appearance.Options.UseFont = true;
            this.txtContCode.Properties.MaxLength = 10;
            this.txtContCode.Properties.ReadOnly = true;
            this.txtContCode.Size = new System.Drawing.Size(117, 22);
            this.txtContCode.TabIndex = 4;
            this.txtContCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContCode_KeyDown);
            // 
            // chkBreak
            // 
            this.chkBreak.AutoSize = true;
            this.chkBreak.Location = new System.Drawing.Point(111, 135);
            this.chkBreak.Name = "chkBreak";
            this.chkBreak.Size = new System.Drawing.Size(160, 21);
            this.chkBreak.TabIndex = 68;
            this.chkBreak.Text = "With Wages Breakup";
            this.chkBreak.UseVisualStyleBackColor = true;
            // 
            // frmReportsTemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 246);
            this.Controls.Add(this.chkBreak);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtContCode);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtToDt);
            this.Controls.Add(this.txtFromDt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPayPeriod);
            this.Controls.Add(this.txtPayDesc);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmReportsTemp";
            this.Text = "Report Viewer";
            this.Load += new System.EventHandler(this.frmReportsTemp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDt.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtPayPeriod;
        private DevExpress.XtraEditors.TextEdit txtPayDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private DevExpress.XtraEditors.DateEdit txtToDt;
        private DevExpress.XtraEditors.DateEdit txtFromDt;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtContCode;
        private System.Windows.Forms.CheckBox chkBreak;
    }
}