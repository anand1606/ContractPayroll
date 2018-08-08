namespace ContractPayroll.Forms
{
    partial class frmMthlyCalc
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grd_view = new DevExpress.XtraGrid.GridControl();
            this.grd_view1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PayPeriod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DedCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OptionalDed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmpUnqID = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_view1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpUnqID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 54;
            this.label3.Text = "PayPeriod :";
            // 
            // txtPayPeriod
            // 
            this.txtPayPeriod.Location = new System.Drawing.Point(80, 12);
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
            this.txtPayDesc.Location = new System.Drawing.Point(182, 12);
            this.txtPayDesc.Name = "txtPayDesc";
            this.txtPayDesc.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayDesc.Properties.Appearance.Options.UseFont = true;
            this.txtPayDesc.Properties.Mask.EditMask = "[A-Za-z 0-9]+";
            this.txtPayDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPayDesc.Properties.MaxLength = 100;
            this.txtPayDesc.Properties.ReadOnly = true;
            this.txtPayDesc.Size = new System.Drawing.Size(421, 22);
            this.txtPayDesc.TabIndex = 1;
            // 
            // btnProcess
            // 
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.Location = new System.Drawing.Point(188, 237);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(238, 42);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pBar
            // 
            this.pBar.Location = new System.Drawing.Point(12, 285);
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(591, 35);
            this.pBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBar.TabIndex = 56;
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.Appearance.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.grd_view);
            this.groupControl2.Location = new System.Drawing.Point(8, 74);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(595, 157);
            this.groupControl2.TabIndex = 57;
            this.groupControl2.Text = "Other Deduction Options";
            // 
            // grd_view
            // 
            this.grd_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd_view.Location = new System.Drawing.Point(2, 21);
            this.grd_view.MainView = this.grd_view1;
            this.grd_view.Name = "grd_view";
            this.grd_view.Size = new System.Drawing.Size(591, 134);
            this.grd_view.TabIndex = 2;
            this.grd_view.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grd_view1});
            this.grd_view.Click += new System.EventHandler(this.grd_view_Click);
            // 
            // grd_view1
            // 
            this.grd_view1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.PayPeriod,
            this.DedCode,
            this.Amount,
            this.OptionalDed});
            this.grd_view1.GridControl = this.grd_view;
            this.grd_view1.Name = "grd_view1";
            this.grd_view1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.grd_view1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.grd_view1.OptionsCustomization.AllowFilter = false;
            this.grd_view1.OptionsCustomization.AllowGroup = false;
            this.grd_view1.OptionsCustomization.AllowQuickHideColumns = false;
            this.grd_view1.OptionsCustomization.AllowRowSizing = true;
            this.grd_view1.OptionsCustomization.AllowSort = false;
            this.grd_view1.OptionsDetail.AllowZoomDetail = false;
            this.grd_view1.OptionsDetail.EnableMasterViewMode = false;
            this.grd_view1.OptionsDetail.ShowDetailTabs = false;
            this.grd_view1.OptionsDetail.SmartDetailExpand = false;
            this.grd_view1.OptionsMenu.EnableColumnMenu = false;
            this.grd_view1.OptionsMenu.EnableFooterMenu = false;
            this.grd_view1.OptionsMenu.EnableGroupPanelMenu = false;
            this.grd_view1.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.grd_view1.OptionsMenu.ShowAutoFilterRowItem = false;
            this.grd_view1.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.grd_view1.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.grd_view1.OptionsMenu.ShowSplitItem = false;
            this.grd_view1.OptionsNavigation.EnterMoveNextColumn = true;
            this.grd_view1.OptionsView.ShowDetailButtons = false;
            this.grd_view1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grd_view1.OptionsView.ShowGroupPanel = false;
            // 
            // PayPeriod
            // 
            this.PayPeriod.Caption = "PayPeriod";
            this.PayPeriod.FieldName = "PayPeriod";
            this.PayPeriod.Name = "PayPeriod";
            this.PayPeriod.OptionsColumn.AllowEdit = false;
            this.PayPeriod.Visible = true;
            this.PayPeriod.VisibleIndex = 0;
            this.PayPeriod.Width = 58;
            // 
            // DedCode
            // 
            this.DedCode.Caption = "DedCode";
            this.DedCode.FieldName = "DedCode";
            this.DedCode.Name = "DedCode";
            this.DedCode.OptionsColumn.AllowEdit = false;
            this.DedCode.Visible = true;
            this.DedCode.VisibleIndex = 1;
            this.DedCode.Width = 57;
            // 
            // Amount
            // 
            this.Amount.Caption = "Amount";
            this.Amount.FieldName = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.Amount.Visible = true;
            this.Amount.VisibleIndex = 2;
            this.Amount.Width = 66;
            // 
            // OptionalDed
            // 
            this.OptionalDed.Caption = "Optional Dedection ( Check if required )";
            this.OptionalDed.FieldName = "BCFlg";
            this.OptionalDed.Name = "OptionalDed";
            this.OptionalDed.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.OptionalDed.Visible = true;
            this.OptionalDed.VisibleIndex = 3;
            this.OptionalDed.Width = 392;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 67;
            this.label1.Text = "EmpUnqID :";
            // 
            // txtEmpUnqID
            // 
            this.txtEmpUnqID.Location = new System.Drawing.Point(80, 40);
            this.txtEmpUnqID.Name = "txtEmpUnqID";
            this.txtEmpUnqID.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpUnqID.Properties.Appearance.Options.UseFont = true;
            this.txtEmpUnqID.Properties.MaxLength = 10;
            this.txtEmpUnqID.Size = new System.Drawing.Size(88, 22);
            this.txtEmpUnqID.TabIndex = 66;
            // 
            // frmMthlyCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 389);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmpUnqID);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.pBar);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPayPeriod);
            this.Controls.Add(this.txtPayDesc);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMthlyCalc";
            this.Text = "Monthly Salary Calculation";
            this.Load += new System.EventHandler(this.frmMthlyCalc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_view1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmpUnqID.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtPayPeriod;
        private DevExpress.XtraEditors.TextEdit txtPayDesc;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ProgressBar pBar;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grd_view;
        private DevExpress.XtraGrid.Views.Grid.GridView grd_view1;
        private DevExpress.XtraGrid.Columns.GridColumn PayPeriod;
        private DevExpress.XtraGrid.Columns.GridColumn DedCode;
        private DevExpress.XtraGrid.Columns.GridColumn Amount;
        private DevExpress.XtraGrid.Columns.GridColumn OptionalDed;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtEmpUnqID;
    }
}