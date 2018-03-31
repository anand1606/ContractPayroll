using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContractPayroll.Forms
{
    public partial class frmMthlyAdj : Form
    {

        public string mode = "NEW";
       
        public string GRights = "XXXV";
        public string oldCode = "";
        public bool isLocked = false;
        public string oldDedCode = "";
        public string dedmode = "NEW";

        public DateTime PFromDt;
        public DateTime pToDt;
        
        public frmMthlyAdj()
        {
            InitializeComponent();
        }

        private void txtEmpUnqID_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) || string.IsNullOrEmpty(txtPayPeriod.Text.Trim()))
            {
                ResetCtrl();
                LoadGrid();
                return;
            }

            string sql = "Select EmpUnqID,EmpName,PayPeriod from Cont_MastEmp  " +          
            "  where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";

            DataSet empds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            Boolean hasRows = empds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in empds.Tables[0].Rows)
                {
                    txtEmpName.Text = dr["EmpName"].ToString();
                    
                }

                LoadGrid();
            }
            else
            {
                ResetCtrl();

            }

            SetRights();
        }

        private void LoadGrid()
        {
            if (string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) || string.IsNullOrEmpty(txtPayPeriod.Text.Trim()))
            {
                grid.DataSource = null;
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select SrNo,Adj_Basic,Adj_TPAHrs,Adj_TpaAmt,Adj_DaysPay,Adj_DaysPayAmt,Adj_Amt,Adj_Remarks from Cont_MthlyAtn where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' Order By SrNo";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

            Boolean hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                grid.DataSource = ds;
                grid.DataMember = ds.Tables[0].TableName;
            }
            else
            {
                grid.DataSource = null;
            }

            sql = "select DedCode,Amount,AddDt,AddID,UpdDt,UpdID from Cont_MthlyDed where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

            hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                gridControl1.DataSource = ds;
                gridControl1.DataMember = ds.Tables[0].TableName;
            }
            else
            {
                gridControl1.DataSource = null;
            }





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
                else if (e.KeyCode == Keys.F2)
                {
                    obj = (List<string>)hlp.Show(sql, "PayDesc", "PayDesc", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                  100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {
                    txtPayPeriod.Text = "";
                    txtPayDesc.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtPayPeriod.Text = "";
                    txtPayDesc.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtPayPeriod.Text = "";
                    txtPayDesc.Text = "";
                    return;
                }
                else
                {
                    txtPayPeriod.Text = obj.ElementAt(0).ToString();
                    txtPayDesc.Text = obj.ElementAt(1).ToString();
                }
            }
        }

        private void ResetCtrl()
        {
            //btnAdd.Enabled = false;
            //btnUpdate.Enabled = false;
            //btnDelete.Enabled = false;

            object s = new object();
            EventArgs e = new EventArgs();

            txtPayPeriod.Properties.ReadOnly = true;
            GRights = ContractPayroll.Classes.Globals.GetFormRights(this.Name);

            //txtPayPeriod.Text = "";
            //txtParaDesc.Text = "";
            
            txtEmpUnqID.Text = "";
            txtEmpName.Text = "";

            txtSrNo.Text = "";
            txtRemarks.Text = "";
            
            txtAdjAmt.Value = 0;
            txtAdjBasic.Value = 0;
            txtAdjTpaHrs.Value = 0;
            txtAdjDays.Value = 0;

            txtDedCode.Text = "";
            txtDedDesc.Text = "";
            txtDedAmt.Value = 0;


            LoadGrid();
            PFromDt = DateTime.MinValue;
            pToDt = DateTime.MinValue;

            oldCode = "";
            oldDedCode = "";
            mode = "NEW";
            dedmode = "NEW";

        }

        private void SetRights()
        {
            if (txtEmpUnqID.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A"))
            {
                //btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                //btnDelete.Enabled = false;
            }
            else if (txtEmpUnqID.Text.Trim() != "" && txtSrNo.Text.Trim() != ""  && mode == "OLD")
            {
                //btnAdd.Enabled = false;

                //if (GRights.Contains("U"))
                    btnUpdate.Enabled = true;
                //if (GRights.Contains("D"))
                //    btnDelete.Enabled = true;
            }
            else if (txtEmpUnqID.Text.Trim() != "" && txtSrNo.Text.Trim() == "" && mode == "OLD")
            {
                btnUpdate.Enabled = false;
            }

            if (GRights.Contains("XXXV"))
            {
                //btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                //btnDelete.Enabled = false;
            }
        }

        private void txtPayPeriod_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPayPeriod.Text.Trim()) || txtPayPeriod.Text.Trim() == "0")
            {
                isLocked = false;
                PFromDt = DateTime.MinValue;
                pToDt = DateTime.MinValue;
            }
            else
            {
                DataSet ds = new DataSet();
                string sql = "select * From Cont_MastPayPeriod where  PayPeriod='" + txtPayPeriod.Text.Trim() + "'";

                ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                bool hasRows = ds.Tables.Cast<DataTable>()
                               .Any(table => table.Rows.Count != 0);

                if (hasRows)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        txtPayDesc.Text = dr["PayDesc"].ToString();
                        PFromDt = Convert.ToDateTime(dr["FromDt"]);
                        pToDt = Convert.ToDateTime(dr["ToDt"]);

                        isLocked = ((Convert.ToBoolean(dr["IsLocked"])) ? true : false);
                    }
                }
                else
                {
                    PFromDt = DateTime.MinValue;
                    pToDt = DateTime.MinValue;
                }
            }
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if (isLocked)
            {
                err = err + "Does not allowed to change in locked period.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtPayPeriod.Text))
            {
                err = err + "Please select Pay Period " + Environment.NewLine;
                return err;

            }

            if (string.IsNullOrEmpty(txtPayDesc.Text))
            {
                err = err + "Please Select Description.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtEmpUnqID.Text))
            {
                err = err + "Please Select Employee Code.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtEmpName.Text))
            {
                err = err + "Please Select Employee Code.." + Environment.NewLine;
                return err;
            }

            //check if

            if (string.IsNullOrEmpty(txtSrNo.Text))
            {
                err = err + "Please Enter SrNo.." + Environment.NewLine;
                return err;
            }

            if (txtAdjDays.Value > 0 && txtAdjBasic.Value <= 0)
            {
                err = err + "Please Enter Adj. Basic.." + Environment.NewLine;
                return err;
            }

            if (txtAdjTpaHrs.Value > 0 && txtAdjBasic.Value <= 0)
            {
                err = err + "Please Enter Adj. Basic.." + Environment.NewLine;
                return err;
            }


            return err;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick(view, pt);
        }

        private void DoRowDoubleClick(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                txtSrNo.Text = gridView1.GetRowCellValue(info.RowHandle, "SrNo").ToString();
                //txtAdjAmt.Value = Convert.ToDecimal(gridView1.GetRowCellValue(info.RowHandle, "Adj_Amt").ToString());
                //txtAdjBasic.Value = Convert.ToDecimal(gridView1.GetRowCellValue(info.RowHandle, "Adj_Basic").ToString());
                //txtAdjDays.Value = Convert.ToDecimal(gridView1.GetRowCellValue(info.RowHandle, "Adj_DaysPay").ToString());
                //txtAdjTpaHrs.Value = Convert.ToDecimal(gridView1.GetRowCellValue(info.RowHandle, "Adj_TPAHrs").ToString());

            }
            

            object o = new object();
            EventArgs e = new EventArgs();

            oldCode = txtSrNo.Text.ToString();
            txtSrNo_Validated(o, e);
               

        }

        private void txtSrNo_Validated(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select SrNo,Adj_Basic,Adj_TPAHrs,Adj_TpaAmt,Adj_DaysPay,Adj_DaysPayAmt,Adj_Amt,Adj_Remarks from Cont_MthlyAtn" + 
                " where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' " + 
                " And SrNo ='" + txtSrNo.Text.Trim() + "' Order By SrNo";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

            Boolean hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                mode = "OLD";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtRemarks.Text = dr["Adj_Remarks"].ToString();
                    txtSrNo.Text = dr["SrNo"].ToString();
                    txtAdjAmt.Value = Convert.ToDecimal(dr["Adj_Amt"]);
                    txtAdjBasic.Value = Convert.ToDecimal(dr["Adj_Basic"]) ;
                    txtAdjTpaHrs.Value = Convert.ToDecimal(dr["Adj_TpaHrs"]);
                    txtAdjDays.Value = Convert.ToDecimal(dr["Adj_DaysPay"]);

                }
            }
            else
            {
                mode = "NEW";
                txtRemarks.Text = "";
                txtSrNo.Text = "";
                txtAdjAmt.Value = 0;
                txtAdjBasic.Value = 0;
                txtAdjTpaHrs.Value = 0;
                txtAdjDays.Value = 0;
            }
            SetRights();
        }

        private void txtEmpUnqID_KeyDown(object sender, KeyEventArgs e)
        {
            if(string.IsNullOrEmpty(txtPayPeriod.Text.Trim()))
                return;
            
            
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select EmpUnqID,EmpName,PayPeriod from Cont_MastEmp " +                    
                    " Where PayPeriod = '" + txtPayPeriod.Text.Trim() + "'";
                
                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "EmpUnqID", "EmpUnqID", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else if (e.KeyCode == Keys.F2)
                {
                    obj = (List<string>)hlp.Show(sql, "EmpName", "EmpName", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                  100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {
                    txtEmpUnqID.Text = "";
                    txtEmpName.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtEmpUnqID.Text = "";
                    txtEmpName.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtEmpUnqID.Text = "";
                    txtEmpName.Text = "";
                    return;
                }
                else
                {
                    txtEmpUnqID.Text = obj.ElementAt(0).ToString();
                    txtEmpName.Text = obj.ElementAt(1).ToString();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlTransaction tr = cn.BeginTransaction();
                string sql = string.Empty;

                try
                {
                    sql = "Update Cont_MthlyAtn Set " +
                    " Adj_Basic = '" + txtAdjBasic.Value.ToString() + "'," +
                    " Adj_TpaHrs ='" + txtAdjTpaHrs.Value.ToString() + "', " +
                    " Adj_DaysPay ='" + txtAdjDays.Value.ToString() + "', " +
                    " Adj_Amt ='" + txtAdjAmt.Value.ToString() + "', " +
                    " Adj_Remarks ='" + txtRemarks.Text.Trim() + "', " +
                    " [Cal_Basic] = 0 " +
                    "  ,[Cal_DaysPay] = 0 " +
                    "  ,[Cal_WODays] = 0 " +
                    "  ,[Cal_TpaHrs] = 0 " +
                    "  ,[Cal_TpaAmt] = 0 " +
                    "  ,[Tot_DaysPay] = 0 " +
                    "  ,[Tot_EarnBasic] = 0 " +
                    "  ,[Tot_TpaHrs]= 0 " +
                    "  ,[Tot_TpaAmt]= 0 " +
                    "  ,[Tot_Earnings]= 0 " +
                    "  ,[Cal_PF]= 0 " +
                    "  ,[Cal_EPF]= 0 " +
                    "  ,[Cal_EPS]= 0 " +
                    "  ,[Cal_CoCommAmt]= 0 " +
                    "  ,[Cal_CoCommWoAmt]= 0 " +
                    "  ,[Cal_CoCommPFAmt]= 0 " +
                    "  ,[Cal_CoServTaxAmt] = 0 " +
                    "  ,[Cal_CoEduTaxAmt] = 0 " +
                    " ,UpdDt = GetDate(), UpdID ='" + Utils.User.GUserID + "'" +
                    " Where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' And EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";

                    SqlCommand cmd = new SqlCommand(sql, cn, tr);
                    cmd.ExecuteNonQuery();

                    sql = "Update Cont_MthlyPay Set " +
                        //"  Adj_Basic = 0 " +
                        " Adj_TPAHrs = 0 " +
                        " ,Adj_TPAAmt = 0 " +
                        " ,Adj_DaysPay = 0 " +
                        " ,Adj_DaysPayAmt = 0 " +
                        " ,Adj_Amt = 0 " +
                        " ,Cal_Basic = 0 " +
                        " ,Cal_DaysPay = 0 " +
                        " ,Cal_WODays = 0 " +
                        " ,Cal_TpaHrs = 0 " +
                        " ,Cal_TpaAmt = 0 " +
                        " ,Tot_DaysPay = 0 " +
                        " ,Tot_EarnBasic = 0 " +
                        " ,Tot_TpaHrs = 0 " +
                        " ,Tot_TpaAmt = 0 " +
                        " ,Tot_Earnings = 0 " +
                        " ,Ded_PF = 0 " +
                        " ,Cal_EPF = 0 " +
                        " ,Cal_EPS = 0 " +
                        " ,Ded_ESI = 0 " +
                        " ,Ded_LWF = 0 " +
                        " ,Ded_DeathFund = 0 " +
                        " ,Ded_Other = 0 " +
                        " ,Ded_Mess = 0 " +
                        " ,Tot_Ded = 0 " +
                        " ,NetPay = 0 " +
                        " ,Tot_CoCommAmt = 0 " +
                       // " ,Tot_CoCommWoAmt = 0 " +
                        " ,Tot_CoCommPFAmt = 0 " +
                        " ,Tot_CoComm = 0 " +
                        " ,Tot_CoServTax = 0 " +
                        " ,Tot_CoEduTax = 0 " +
                        " ,UpdDt = GetDate(), UpdID ='" + Utils.User.GUserID + "'" +
                        " Where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' And EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";

                    cmd = new SqlCommand(sql, cn, tr);
                    cmd.ExecuteNonQuery();



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    tr.Commit();
                    mode = "NEW";
                    txtRemarks.Text = "";
                    txtSrNo.Text = "";
                    txtAdjAmt.Value = 0;
                    txtAdjBasic.Value = 0;
                    txtAdjTpaHrs.Value = 0;
                    txtAdjDays.Value = 0;

                    LoadGrid();
                    MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    tr.Dispose();
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }//using sql connection
        }

        private void frmMthlyAdj_Load(object sender, EventArgs e)
        {
            ResetCtrl();
        }

        private string DataValidate2()
        {
            string err = string.Empty;

            if (isLocked)
            {
                err = err + "Does not allowed to change in locked period.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtPayPeriod.Text))
            {
                err = err + "Please select Pay Period " + Environment.NewLine;
                return err;

            }

            if (string.IsNullOrEmpty(txtPayDesc.Text))
            {
                err = err + "Please Select Description.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtEmpUnqID.Text))
            {
                err = err + "Please Select Employee Code.." + Environment.NewLine;
                return err;
            }

            if (string.IsNullOrEmpty(txtEmpName.Text))
            {
                err = err + "Please Select Employee Code.." + Environment.NewLine;
                return err;
            }

            //check if

            if (string.IsNullOrEmpty(txtDedCode.Text.Trim()))
            {
                err = err + "Please Select  DedCode.." + Environment.NewLine;
                return err;
            }

            string dedcodes = "MESS,MISC";

            if (!dedcodes.Contains(txtDedCode.Text.Trim()))
            {
                err = err + "invalid dedcode it should be from (" + dedcodes + ")" + Environment.NewLine;
                return err;
            }

            return err;
        }


        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowDoubleClick2(view, pt);
        }//end function

        private void DoRowDoubleClick2(GridView view, Point pt)
        {
            GridHitInfo info = view.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                txtDedCode.Text = gridView2.GetRowCellValue(info.RowHandle, "DedCode").ToString();
                txtDedAmt.Value = Convert.ToDecimal(gridView2.GetRowCellValue(info.RowHandle, "Amount").ToString());
                
            }


            object o = new object();
            EventArgs e = new EventArgs();

            oldCode = txtSrNo.Text.ToString();
            txtDedCode_Validated(o, e);


        }

        private void txtDedCode_Validated(object sender, EventArgs e)
        {
            if (txtDedCode.Text.Trim() == "MESS")
                txtDedDesc.Text = "Mess Deduction";
            else if (txtDedCode.Text.Trim() == "MISC")
                txtDedDesc.Text = "Miscellaneous Deduction";
            else
                txtDedDesc.Text = "";


            DataSet ds = new DataSet();
            string sql = "select * from Cont_MthlyDed" +
                " where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' " +
                " And DedCode ='" + txtDedCode.Text.Trim() + "' ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

            Boolean hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                dedmode = "OLD";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtDedAmt.Value = Convert.ToDecimal(dr["Amount"]);                    
                }
            }
            else
            {
                dedmode = "NEW";                
                txtDedAmt.Value = 0;
                
            }


        }

        private void btnODedAdd_Click(object sender, EventArgs e)
        {
            string err = DataValidate2();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlTransaction tr = cn.BeginTransaction();
                string sql = string.Empty;

                if(dedmode == "NEW")
                {
                    sql = "Insert into Cont_MthlyDed (PayPeriod,EmpUnqID,DedCode,Amount,AddDt,AddID) values (" +
                        "'" + txtPayPeriod.Text.Trim() + "'," +
                        "'" + txtEmpUnqID.Text.Trim() + "'," +
                        "'" + txtDedCode.Text.Trim() + "'," +
                        "'" + txtDedAmt.Value.ToString() + "',GetDate()," +
                        "'" + Utils.User.GUserID + "')";
                }
                else
                {
                    sql = "Update Cont_MthlyDed Set Amount ='" + txtDedAmt.Text.Trim() + "',UpdDt = GetDate(),UpdID ='" + Utils.User.GUserID + "' Where " +
                        " PayPeriod = '" + txtPayPeriod.Text.Trim() + "' " +
                        " and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";                        
                }

                try
                {
                    
                    SqlCommand cmd = new SqlCommand(sql, cn, tr);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    tr.Commit();
                    dedmode = "NEW";
                    txtDedCode.Text = "";
                    txtDedDesc.Text = "";
                    txtDedAmt.Value = 0;

                    LoadGrid();
                    MessageBox.Show("Record Updated...Salary Calculation required to reflact the changes.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    tr.Dispose();
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }//using sql connection
        }

        private void btnODedDel_Click(object sender, EventArgs e)
        {
            string err = DataValidate2();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                try
                {
                    cn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlTransaction tr = cn.BeginTransaction();
                string sql = string.Empty;

                if (dedmode == "OLD")
                {
                    sql = "Delete From Cont_MthlyDed Where " +
                        " PayPeriod = '" + txtPayPeriod.Text.Trim() + "' " +
                        " And EmpUnqID ='" + txtEmpUnqID.Text.Trim() + "' " +
                        " And DedCode ='" + txtDedCode.Text.Trim() + "' ";
                       


                    try
                    {

                        SqlCommand cmd = new SqlCommand(sql, cn, tr);
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        tr.Commit();
                        dedmode = "NEW";
                        txtDedCode.Text = "";
                        txtDedDesc.Text = "";
                        txtDedAmt.Value = 0;

                        LoadGrid();
                        MessageBox.Show("Record Updated...Salary Calculation required to reflact the changes.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        tr.Dispose();
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }//using sql connection
        }

        private void txtDedCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select 'MESS' as DedCode , '*'  Union Select 'MISC' as DedCode, '*' ";


                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "DedCode", "DedCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {
                    txtDedCode.Text = "";

                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtDedCode.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtDedCode.Text = "";
                    return;
                }
                else
                {

                    txtDedCode.Text = obj.ElementAt(0).ToString();
                    
                    txtDedCode_Validated(sender, e);
                }
            }
        }
    }
}
