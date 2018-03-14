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

                var HeaderTBL = new Reports.DS_rptMthlySalReg.DtMthlySalDataTable();
                var HeaderTa = new Reports.DS_rptMthlySalRegTableAdapters.DtMthlySalTableAdapter();
                HeaderTa.Connection.ConnectionString = Utils.Helper.constr;
                HeaderTBL = HeaderTa.GetData(tPay);

                DataSet Ds = new DataSet();
                Ds.Tables.Add(HeaderTBL);
                
                bool hasRows = Ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                if (hasRows)
                {
                    DevExpress.XtraReports.UI.XtraReport report = new Reports.rptMthlySalReg();
                    report.DataSource = Ds;
                    report.ShowPreviewDialog();
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
                        return;
                    }
                }

                var HeaderTBL = new Reports.DS_rptMthlySalReg.DtMthlySalDataTable();
                var HeaderTa = new Reports.DS_rptMthlySalRegTableAdapters.DtMthlySalTableAdapter();
                HeaderTa.Connection.ConnectionString = Utils.Helper.constr;
                HeaderTBL = HeaderTa.GetData(tPay);

                DataSet Ds = new DataSet();
                Ds.Tables.Add(HeaderTBL);

                bool hasRows = Ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                if (hasRows)
                {
                    DevExpress.XtraReports.UI.XtraReport report = new Reports.rptMthlyTPAReg();
                    report.DataSource = Ds;
                    report.ShowPreviewDialog();
                }
            }
            
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

            txtPayPeriod.Text = "";
            txtPayDesc.Text = "";
            txtFromDt.EditValue = null;
            txtToDt.EditValue = null;

            

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


                sql = "Select PayPeriod,PayDesc,FromDt,ToDt from Cont_MastPayPeriod Where 1 = 1  ";


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
    }
}
