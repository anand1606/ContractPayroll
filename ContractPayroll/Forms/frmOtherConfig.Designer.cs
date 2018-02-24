namespace ContractPayroll.Forms
{
    partial class frmOtherConfig
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
            this.Group2 = new System.Windows.Forms.GroupBox();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPayDesc = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPayPeriod = new DevExpress.XtraEditors.TextEdit();
            this.txtTSlab = new DevExpress.XtraEditors.TextEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFSlab = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.grpUserRights = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.chkFixed = new DevExpress.XtraEditors.CheckEdit();
            this.txtParaDesc = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtParaCode = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txtpValue = new DevExpress.XtraEditors.TextEdit();
            this.txtParaType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkAppFlg = new DevExpress.XtraEditors.CheckEdit();
            this.Group2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTSlab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFSlab.Properties)).BeginInit();
            this.grpUserRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFixed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParaDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParaCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParaType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAppFlg.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Group2
            // 
            this.Group2.Controls.Add(this.grid);
            this.Group2.Location = new System.Drawing.Point(2, 147);
            this.Group2.Name = "Group2";
            this.Group2.Size = new System.Drawing.Size(865, 352);
            this.Group2.TabIndex = 8;
            this.Group2.TabStop = false;
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 16);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(859, 333);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.GridControl = this.grid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsFilter.FilterEditorUseMenuForOperandsAndOperators = false;
            this.gridView1.OptionsFind.AllowFindPanel = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView1.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.gridView1.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.gridView1.OptionsMenu.ShowSplitItem = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAppFlg);
            this.groupBox1.Controls.Add(this.txtPayDesc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPayPeriod);
            this.groupBox1.Controls.Add(this.txtTSlab);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtFSlab);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.grpUserRights);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkFixed);
            this.groupBox1.Controls.Add(this.txtParaDesc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtParaCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtpValue);
            this.groupBox1.Controls.Add(this.txtParaType);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(865, 151);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtPayDesc
            // 
            this.txtPayDesc.Location = new System.Drawing.Point(200, 16);
            this.txtPayDesc.Name = "txtPayDesc";
            this.txtPayDesc.Properties.MaxLength = 10;
            this.txtPayDesc.Properties.ReadOnly = true;
            this.txtPayDesc.Size = new System.Drawing.Size(398, 20);
            this.txtPayDesc.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "PayPeriod :";
            // 
            // txtPayPeriod
            // 
            this.txtPayPeriod.Location = new System.Drawing.Point(80, 16);
            this.txtPayPeriod.Name = "txtPayPeriod";
            this.txtPayPeriod.Properties.MaxLength = 10;
            this.txtPayPeriod.Properties.ReadOnly = true;
            this.txtPayPeriod.Size = new System.Drawing.Size(117, 20);
            this.txtPayPeriod.TabIndex = 45;
            this.txtPayPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPayPeriod_KeyDown);
            this.txtPayPeriod.Validated += new System.EventHandler(this.txtPayPeriod_Validated);
            // 
            // txtTSlab
            // 
            this.txtTSlab.Location = new System.Drawing.Point(267, 67);
            this.txtTSlab.Name = "txtTSlab";
            this.txtTSlab.Properties.Mask.BeepOnError = true;
            this.txtTSlab.Properties.Mask.EditMask = "[0-9.]+";
            this.txtTSlab.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtTSlab.Size = new System.Drawing.Size(117, 20);
            this.txtTSlab.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(205, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Slab End :";
            // 
            // txtFSlab
            // 
            this.txtFSlab.Location = new System.Drawing.Point(80, 67);
            this.txtFSlab.Name = "txtFSlab";
            this.txtFSlab.Properties.Mask.BeepOnError = true;
            this.txtFSlab.Properties.Mask.EditMask = "[0-9.]+";
            this.txtFSlab.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtFSlab.Size = new System.Drawing.Size(117, 20);
            this.txtFSlab.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(390, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Para Value :";
            // 
            // grpUserRights
            // 
            this.grpUserRights.Controls.Add(this.btnClose);
            this.grpUserRights.Controls.Add(this.btnCancel);
            this.grpUserRights.Controls.Add(this.btnDelete);
            this.grpUserRights.Controls.Add(this.btnUpdate);
            this.grpUserRights.Controls.Add(this.btnAdd);
            this.grpUserRights.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpUserRights.Location = new System.Drawing.Point(3, 96);
            this.grpUserRights.Name = "grpUserRights";
            this.grpUserRights.Size = new System.Drawing.Size(859, 52);
            this.grpUserRights.TabIndex = 7;
            this.grpUserRights.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Cornsilk;
            this.btnClose.Location = new System.Drawing.Point(520, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Clos&e";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Cornsilk;
            this.btnCancel.Location = new System.Drawing.Point(440, 14);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancle";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Cornsilk;
            this.btnDelete.Location = new System.Drawing.Point(359, 14);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 32);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Cornsilk;
            this.btnUpdate.Location = new System.Drawing.Point(278, 14);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Cornsilk;
            this.btnAdd.Location = new System.Drawing.Point(197, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 32);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Slab Start :";
            // 
            // chkFixed
            // 
            this.chkFixed.Location = new System.Drawing.Point(640, 67);
            this.chkFixed.Name = "chkFixed";
            this.chkFixed.Properties.Caption = "Is Optional :";
            this.chkFixed.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkFixed.Size = new System.Drawing.Size(78, 19);
            this.chkFixed.TabIndex = 6;
            // 
            // txtParaDesc
            // 
            this.txtParaDesc.Location = new System.Drawing.Point(200, 40);
            this.txtParaDesc.Name = "txtParaDesc";
            this.txtParaDesc.Properties.Mask.EditMask = "[A-Za-z 0-9]+";
            this.txtParaDesc.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtParaDesc.Properties.MaxLength = 100;
            this.txtParaDesc.Properties.ReadOnly = true;
            this.txtParaDesc.Size = new System.Drawing.Size(398, 20);
            this.txtParaDesc.TabIndex = 1;
            this.txtParaDesc.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "ParaCode :";
            // 
            // txtParaCode
            // 
            this.txtParaCode.Location = new System.Drawing.Point(80, 41);
            this.txtParaCode.Name = "txtParaCode";
            this.txtParaCode.Properties.MaxLength = 10;
            this.txtParaCode.Properties.ReadOnly = true;
            this.txtParaCode.Size = new System.Drawing.Size(117, 20);
            this.txtParaCode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(608, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Para Type :";
            // 
            // txtpValue
            // 
            this.txtpValue.Location = new System.Drawing.Point(461, 67);
            this.txtpValue.Name = "txtpValue";
            this.txtpValue.Properties.Mask.BeepOnError = true;
            this.txtpValue.Properties.Mask.EditMask = "f";
            this.txtpValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtpValue.Size = new System.Drawing.Size(137, 20);
            this.txtpValue.TabIndex = 5;
            // 
            // txtParaType
            // 
            this.txtParaType.Location = new System.Drawing.Point(676, 38);
            this.txtParaType.Name = "txtParaType";
            this.txtParaType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtParaType.Properties.Items.AddRange(new object[] {
            "R",
            "P"});
            this.txtParaType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.txtParaType.Size = new System.Drawing.Size(42, 20);
            this.txtParaType.TabIndex = 4;
            // 
            // chkAppFlg
            // 
            this.chkAppFlg.Location = new System.Drawing.Point(724, 67);
            this.chkAppFlg.Name = "chkAppFlg";
            this.chkAppFlg.Properties.Caption = "Is Applicable :";
            this.chkAppFlg.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkAppFlg.Size = new System.Drawing.Size(91, 19);
            this.chkAppFlg.TabIndex = 48;
            // 
            // frmOtherConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 505);
            this.Controls.Add(this.Group2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmOtherConfig";
            this.Text = "Default Config";
            this.Load += new System.EventHandler(this.frmOtherConfig_Load);
            this.Group2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPayPeriod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTSlab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFSlab.Properties)).EndInit();
            this.grpUserRights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkFixed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParaDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParaCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParaType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAppFlg.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Group2;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txtTSlab;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit txtFSlab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpUserRights;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.CheckEdit chkFixed;
        private DevExpress.XtraEditors.TextEdit txtParaDesc;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtParaCode;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtpValue;
        private DevExpress.XtraEditors.ComboBoxEdit txtParaType;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtPayPeriod;
        private DevExpress.XtraEditors.TextEdit txtPayDesc;
        private DevExpress.XtraEditors.CheckEdit chkAppFlg;
    }
}