using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraPrinting.Native;
using DevExpress.Utils;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using ContractPayroll.Reports.DS_rptMthlySalDTTableAdapters;

namespace ContractPayroll.Forms
{
    public partial class frmReportsTemp : Form
    {
        public string RptType = string.Empty;
        public string GRights = "XXXV";
        public string mode = "NEW";

        public frmReportsTemp()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            DataSet Ds = new DataSet();
            
            if (RptType == "SALREGDEF")
            {
                int tPay = 0;
                
                if(!int.TryParse(txtPayPeriod.Text.Trim(),out tPay))
                {
                    return;
                }
                else
                {
                    if (tPay <= 0)
                    {
                        return;
                    }
                }
                this.Cursor = Cursors.WaitCursor;
                string tContCode = txtContCode.Text.ToString().Trim();
                if(txtContCode.Text.ToString().Trim() != "")
                {
                    tContCode = txtContCode.Text.Trim();
                }
                

                if (chkBreak.Checked == true)
                {
                    var HeaderTBL = new Reports.DS_rptMthlySalDT.sp_Cont_MthlySalTPARegisterDataTable();
                    var DetailTBL = new Reports.DS_rptMthlySalDT.sp_Cont_MthlySalDTDataTable();

                    var HeaderTa = new Reports.DS_rptMthlySalDTTableAdapters.sp_Cont_MthlySalTPARegisterTableAdapter();
                    sp_Cont_MthlySalDTTableAdapter DetailTa = new Reports.DS_rptMthlySalDTTableAdapters.sp_Cont_MthlySalDTTableAdapter();

                    HeaderTa.Connection.ConnectionString = Utils.Helper.constr;
                    HeaderTa.ClearBeforeFill = true;
                    DetailTa.Connection.ConnectionString = Utils.Helper.constr;
                    DetailTa.ClearBeforeFill = true;
                    DetailTa.SelectCommandTimeout = 0;
                    //HeaderTBL.Constraints.Clear();
                    
                    HeaderTBL = HeaderTa.GetData(tPay, tContCode);
                    DetailTBL = DetailTa.GetData(tPay, tContCode);

                    Ds.Tables.Add(HeaderTBL);
                    Ds.Tables.Add(DetailTBL);


                    DataRelation newRelation = new DataRelation("sp_Cont_MthlySalTPARegister_sp_Cont_MthlySalDT",
                        new DataColumn[] { HeaderTBL.Columns["PayPeriod"], HeaderTBL.Columns["EmpUnqID"] },
                        new DataColumn[] { DetailTBL.Columns["PayPeriod"], DetailTBL.Columns["EmpUnqID"] }
                    );

                    Ds.Relations.Add(newRelation);
                }
                else
                {
                    var HeaderTBL = new Reports.DS_rptMthlySalReg.sp_Cont_MthlySalTPARegisterDataTable();
                    var HeaderTa = new Reports.DS_rptMthlySalRegTableAdapters.sp_Cont_MthlySalTPARegisterTableAdapter();
                    HeaderTa.Connection.ConnectionString = Utils.Helper.constr;
                    HeaderTBL = HeaderTa.GetData(tPay, tContCode);
                    Ds.Tables.Add(HeaderTBL);
                }


                bool hasRows = Ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                if (hasRows)
                {
                    if (chkBreak.Checked == true)
                    {
                        DevExpress.XtraReports.UI.XtraReport report = new Reports.rptMthlySAL();
                        report.DataSource = Ds;
                        this.Cursor = Cursors.Default;
                        report.ShowPreviewDialog();
                    }
                    else
                    {
                        DevExpress.XtraReports.UI.XtraReport report = new Reports.rptMthlySalRegDt();
                        report.DataSource = Ds;
                        this.Cursor = Cursors.Default;
                        report.ShowPreviewDialog();
                    }
                    
                }
            }
            else if  (RptType == "TPAREGDEF")
            {
                int tPay = 0;

                if (!int.TryParse(txtPayPeriod.Text.Trim(), out tPay))
                {
                    return;
                }
                else
                {
                    if (tPay <= 0)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }

                string tContCode = " ";
                if (txtContCode.Text.Trim() != "")
                {
                    tContCode = txtContCode.Text.Trim();
                }

                if (chkBreak.Checked == true)
                {
                    var HeaderTBL = new Reports.DS_rptMthlyTPADT.sp_Cont_MthlySalTPARegisterDataTable();
                    var DetailTBL = new Reports.DS_rptMthlyTPADT.sp_Cont_MthlyTPADTDataTable();

                    var HeaderTa = new Reports.DS_rptMthlyTPADTTableAdapters.sp_Cont_MthlySalTPARegisterTableAdapter();
                    var DetailTa = new Reports.DS_rptMthlyTPADTTableAdapters.sp_Cont_MthlyTPADTTableAdapter();

                    HeaderTa.Connection.ConnectionString = Utils.Helper.constr;
                    HeaderTa.ClearBeforeFill = true;
                    DetailTa.Connection.ConnectionString = Utils.Helper.constr;
                    DetailTa.ClearBeforeFill = true;
                    DetailTa.SelectCommandTimeout = 0;
                    //HeaderTBL.Constraints.Clear();
                    HeaderTBL = HeaderTa.GetData(tPay, tContCode);
                    DetailTBL = DetailTa.GetData(tPay, tContCode);

                    Ds.Tables.Add(HeaderTBL);
                    Ds.Tables.Add(DetailTBL);


                    DataRelation newRelation = new DataRelation("sp_Cont_MthlySalTPARegister_sp_Cont_MthlyTPADT",
                        new DataColumn[] { HeaderTBL.Columns["PayPeriod"], HeaderTBL.Columns["EmpUnqID"] },
                        new DataColumn[] { DetailTBL.Columns["PayPeriod"], DetailTBL.Columns["EmpUnqID"] }
                    );

                    Ds.Relations.Add(newRelation);
                }
                else
                {
                    var HeaderTBL = new Reports.DS_rptMthlySalReg.sp_Cont_MthlySalTPARegisterDataTable();
                    var HeaderTa = new Reports.DS_rptMthlySalRegTableAdapters.sp_Cont_MthlySalTPARegisterTableAdapter();
                    HeaderTa.Connection.ConnectionString = Utils.Helper.constr;
                    HeaderTBL = HeaderTa.GetData(tPay, tContCode);
                    Ds.Tables.Add(HeaderTBL);
                }
                

                bool hasRows = Ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                if (hasRows)
                {
                    if (chkBreak.Checked == true)
                    {
                        DevExpress.XtraReports.UI.XtraReport report = new Reports.rptMthlyTPADt();
                        report.DataSource = Ds;
                        this.Cursor = Cursors.Default;
                        report.ShowPreviewDialog();
                    }
                    else
                    {
                        DevExpress.XtraReports.UI.XtraReport report = new Reports.rptMthlyTPAReg();
                        report.DataSource = Ds;
                        this.Cursor = Cursors.Default;
                        report.ShowPreviewDialog();
                    }
                    
                }
            }

            this.Cursor = Cursors.Default;
            
        }

        private void ResetCtrl()
        {
            btnRun.Enabled = false;


            object s = new object();
            EventArgs e = new EventArgs();

            txtPayPeriod.Properties.ReadOnly = true;

            if (RptType == "SALREGDEF")
            {
                GRights = ContractPayroll.Classes.Globals.GetFormRights("frmReportsSalReg");
                
            }
            else if (RptType == "TPAREGDEF")
            {
                GRights = ContractPayroll.Classes.Globals.GetFormRights("frmReportsTpaReg");
                
            }
            mode = "NEW";
            chkBreak.Checked = false;
            txtPayPeriod.Text = "";
            txtPayDesc.Text = "";
            txtFromDt.EditValue = null;
            txtToDt.EditValue = null;
            txtContCode.Text = "";
            

        }

        private void SetRights()
        {
            if (txtPayPeriod.Text.Trim() != "" && GRights.Contains("A"))
            {
                btnRun.Enabled = true;
                if (mode == "NEW")
                    btnRun.Enabled = false;

            }
            else if (txtPayPeriod.Text.Trim() != "")
            {
                btnRun.Enabled = false;

                if (GRights.Contains("U") || GRights.Contains("D"))
                {
                    btnRun.Enabled = true;
                    if (mode == "NEW")
                        btnRun.Enabled = false;
                }

            }

            if (GRights.Contains("XXXV"))
            {
                btnRun.Enabled = false;
            }


        }

        private void txtPayPeriod_Validated(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select * From Cont_MastPayPeriod where  PayPeriod='" + txtPayPeriod.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtPayDesc.Text = dr["PayDesc"].ToString();
                    mode = "OLD";
                    txtFromDt.DateTime = Convert.ToDateTime(dr["FromDt"]);
                    txtToDt.DateTime = Convert.ToDateTime(dr["ToDt"]);
                    btnRun.Enabled = true;
                }
            }
            else
            {
                btnRun.Enabled = false;
                mode = "NEW";
                txtFromDt.EditValue = null;
                txtToDt.EditValue = null;

            }

            SetRights();
        }

        private void txtPayPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select PayPeriod,PayDesc,FromDt,ToDt from Cont_MastPayPeriod Where 1 = 1 Order by PayPeriod Desc ";


                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "PayPeriod", "PayPeriod", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {
                    txtPayPeriod.Text = "";
                    txtPayDesc.Text = "";
                    txtFromDt.EditValue = null;
                    txtToDt.EditValue = null;
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtPayPeriod.Text = "";
                    txtPayDesc.Text = "";
                    txtFromDt.EditValue = null;
                    txtToDt.EditValue = null;
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtPayPeriod.Text = "";
                    txtPayDesc.Text = "";
                    txtFromDt.EditValue = null;
                    txtToDt.EditValue = null;
                    return;
                }
                else
                {

                    txtPayPeriod.Text = obj.ElementAt(0).ToString();
                    txtPayDesc.Text = obj.ElementAt(1).ToString();
                }
            }
        }

        private void frmReportsTemp_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            SetRights();
        }

        private void txtContCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select Distinct ContCode,ContDesc from Cont_MastEmp Where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' Order by ContCode Asc ";


                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "ContCode", "ContCode", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {
                    txtContCode.Text = "";                    
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtContCode.Text = "";    
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtContCode.Text = "";    
                    return;
                }
                else
                {

                   txtContCode.Text = obj.ElementAt(0).ToString();
                  
                }
            }
        }
    }
}
