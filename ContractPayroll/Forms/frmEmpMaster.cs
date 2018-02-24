using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ContractPayroll.Forms
{
    public partial class frmEmpMaster : Form
    {
        public string mode = "NEW";
        public string dtlmode = "NEW";

        public string GRights = "XXXV";
        public string oldCode = "";
        public bool isLocked = false;

        public DateTime PFromDt;
        public DateTime pToDt;

        public frmEmpMaster()
        {
            InitializeComponent();
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
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            object s = new object();
            EventArgs e = new EventArgs();

            txtPayPeriod.Properties.ReadOnly = true;
            GRights = ContractPayroll.Classes.Globals.GetFormRights(this.Name);

            //txtPayPeriod.Text = "";
            //txtParaDesc.Text = "";
            txtFromDt.EditValue = null;
            txtToDt.EditValue = null;
            txtSrNo.Text = "";
            txtcBasic.Text = "";

            txtEmpUnqID.Text = "";
            txtEmpName.Text = "";
            txtFatherName.Text = "";
            txtUnitCode.Text = "";
            txtUnitDesc.Text = "";
            txtDeptCode.Text = "";
            txtDeptDesc.Text = "";
            txtStatCode.Text = "";
            txtStatDesc.Text = "";
            txtDesgCode.Text = "";
            txtDesgDesc.Text = "";
            txtCatCode.Text = "";
            txtCatDesc.Text = "";
            txtGradeCode.Text = "";
            txtGradeDesc.Text = "";
            txtESINo.Text = "";
            txtPFNum.Text = "";
            txtContCode.Text = "";
            txtContDesc.Text = "";
            txtGender.Text = "";
            txtBirthDt.EditValue = null;
            txtJoinDt.EditValue = null;
            txtLeftDt.EditValue = null;

            chkDeathFlg.Checked = false;
            chkESIFlg.Checked = false;
            chkLWFFlg.Checked = false;
            chkPFLG.Checked = false;
            chkPTaxFlg.Checked = false;

            LoadGrid();
            PFromDt = DateTime.MinValue;
            pToDt = DateTime.MinValue;

            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            
            
            if (txtEmpUnqID.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A"))
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (txtEmpUnqID.Text.Trim() != "" && mode == "OLD")
            {
                btnAdd.Enabled = false;

                if (GRights.Contains("U"))
                    btnUpdate.Enabled = true;
                if (GRights.Contains("D"))
                    btnDelete.Enabled = true;
            }

            if (GRights.Contains("XXXV"))
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
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

            

            //if (txtFromDt.EditValue == null || txtFromDt.DateTime == DateTime.MinValue)
            //{
            //    err = err + "Please Enter From Date.." + Environment.NewLine;
            //    return err;
            //}

            //if (txtToDt.EditValue == null || txtToDt.DateTime == DateTime.MinValue)
            //{
            //    err = err + "Please Enter To Date.." + Environment.NewLine;
            //    return err;
            //}

            //DateTime FrmDt = txtFromDt.DateTime.Date;
            //DateTime Todt = txtToDt.DateTime.Date;

            //if (Todt < FrmDt)
            //{
            //    err = err + "Invalid Date Range.." + Environment.NewLine;
            //    return err;
            //}


            return err;
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
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {

                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Update Cont_MastEmp Set PFNo = '" + txtPFNum.Text.Trim() + "', LeftDt = " + ((txtLeftDt.DateTime == DateTime.MinValue || txtLeftDt.EditValue == null) ? "null" :  "'" + txtLeftDt.DateTime.Date.ToString("yyyy-MM-dd") + "'" ) + ", " +
                            " Active = '" + ((txtLeftDt.DateTime == DateTime.MinValue || txtLeftDt.EditValue == null) ? 1 : 0) + "', ESINo = '" + txtESINo.Text.Trim() + "'," +
                            " PFFlg = " + (chkPFLG.Checked ? 1 : 0) + ",PTaxFlg = " + (chkPTaxFlg.Checked ? 1 : 0) + "," +
                            " ESIFlg = " + (chkESIFlg.Checked ? 1 : 0) + ", LWFFlg= " + +(chkLWFFlg.Checked ? 1 : 0) + ", " +
                            " DeathFlg =" + +(chkDeathFlg.Checked ? 1 : 0) + ", UpdDt = GetDate(), " +
                            " UpdID ='" + Utils.User.GUserID + "' where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' And EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        ResetCtrl();

                        MessageBox.Show("Record Updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (string.IsNullOrEmpty(err))
            {

                DialogResult qs = MessageBox.Show("Are You Sure to Delete this Employee...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (qs == DialogResult.No)
                {
                    return;
                }

                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    
                    try
                    {

                        SqlCommand cmd = new SqlCommand();

                        cn.Open();
                        SqlTransaction tr = cn.BeginTransaction();
                        string sql = "Delete From Cont_MastEmp where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID ='" + txtEmpUnqID.Text.Trim() + "'";
                        cmd = new SqlCommand(sql, cn, tr);
                        cmd.ExecuteNonQuery();


                        sql = "Delete From Cont_MastBassic where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID ='" + txtEmpUnqID.Text.Trim() + "'";
                        cmd = new SqlCommand(sql, cn, tr);
                        cmd.ExecuteNonQuery();

                        sql = "Delete From Cont_DailyOth where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID ='" + txtEmpUnqID.Text.Trim() + "'";
                        cmd = new SqlCommand(sql, cn, tr);
                        cmd.ExecuteNonQuery();

                        sql = "Delete From Cont_MthlyAtn where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID ='" + txtEmpUnqID.Text.Trim() + "'";
                        cmd = new SqlCommand(sql, cn, tr);
                        cmd.ExecuteNonQuery();

                        sql = "Delete From Cont_MthlyPay where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID ='" + txtEmpUnqID.Text.Trim() + "'";
                        cmd = new SqlCommand(sql,cn,tr);
                        cmd.ExecuteNonQuery();

                        try
                        {
                            tr.Commit();
                            MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
                            return;
                        }
                        catch (SqlException sex)
                        {
                            tr.Rollback();
                            MessageBox.Show(sex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                            
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                }//using sqlconnection
            }

            // MessageBox.Show("Not Implemented...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetCtrl();
            SetRights();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEmpUnqID_Validated(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) || string.IsNullOrEmpty(txtPayPeriod.Text.Trim()))
            {
                ResetCtrl();
                LoadGrid();
                return;
            }
            string sql = "Select * from Cont_MastEmp where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";

            DataSet empds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            Boolean hasRows = empds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in empds.Tables[0].Rows)
                {
                    txtEmpName.Text = dr["EmpName"].ToString();
                    txtFatherName.Text = dr["FatherName"].ToString();
                    txtBirthDt.DateTime = Convert.ToDateTime(dr["BirthDt"]) ;
                    txtJoinDt.DateTime = Convert.ToDateTime(dr["JoinDt"]);
                    txtLeftDt.EditValue = ( dr["LeftDt"] == null ? null : dr["LeftDt"]);
                    txtGender.Text = dr["Gender"].ToString(); 
                    txtUnitCode.Text = dr["UnitCode"].ToString() ;
                    txtUnitDesc.Text = dr["UnitDesc"].ToString() ;
                    txtDeptCode.Text = dr["DeptCode"].ToString() ;
                    txtDeptDesc.Text = dr["DeptDesc"].ToString() ;
                    txtStatCode.Text= dr["Statcode"].ToString() ;
                    txtStatDesc.Text= dr["StatDesc"].ToString() ;
                    txtCatCode.Text = dr["CatCode"].ToString()  ;
                    txtCatDesc.Text= dr["CatDesc"].ToString() ;
                    txtDesgCode.Text = dr["DesgCode"].ToString(); 
                    txtDesgDesc.Text= dr["DesgDesc"].ToString() ;
                    txtGradeCode.Text= dr["GradeCode"].ToString() ;
                    txtGradeDesc.Text= dr["GradeDesc"].ToString() ;
                    txtContCode.Text= dr["ContCode"].ToString()  ;
                    txtContDesc.Text= dr["ContDesc"].ToString() ;
                    txtESINo.Text = dr["ESINo"].ToString() ;
                    txtPFNum.Text = dr["PFNo"].ToString();
                    txtcBasic.Text = dr["cBasic"].ToString()  ;
                    
                    chkPFLG.Checked =  (Convert.ToBoolean(dr["PFFLG"]));       
                    chkPTaxFlg.Checked =  (Convert.ToBoolean(dr["PTaxFlg"]));
                    chkDeathFlg.Checked = (Convert.ToBoolean(dr["DeathFlg"])) ;
                    chkLWFFlg.Checked = (Convert.ToBoolean(dr["LWFFlg"]));
                    chkESIFlg.Checked = (Convert.ToBoolean(dr["ESIFLG"]));
                    mode = "OLD";

                    txtPayPeriod_Validated(sender, e);

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
            if(string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) || string.IsNullOrEmpty(txtPayPeriod.Text.Trim()))
            {
                grid.DataSource = null;
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * from Cont_MastBasic where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' Order By SrNo";

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
        }

        private void txtEmpUnqID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select EmpUnqID,EmpName,PayPeriod from Cont_MastEmp Where PayPeriod ='" + txtPayPeriod.Text.Trim() + "'";


                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "EmpUnqID", "EmpUnqID", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                else if(e.KeyCode == Keys.F2)
                {
                    obj = (List<string>)hlp.Show(sql, "EmpName", "EmpName", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {
                    txtEmpUnqID.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtEmpUnqID.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtEmpUnqID.Text = "";
                    return;
                }
                else
                {
                    txtEmpUnqID.Text = obj.ElementAt(0).ToString();

                    txtPayPeriod.Text = obj.ElementAt(2).ToString();
                }
            }
        }

        private void frmEmpMaster_Load(object sender, EventArgs e)
        {
            ResetCtrl();
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
                txtFromDt.DateTime = Convert.ToDateTime(gridView1.GetRowCellValue(info.RowHandle, "FromDt"));
                txtToDt.DateTime = Convert.ToDateTime(gridView1.GetRowCellValue(info.RowHandle, "ToDt"));
                txtcBasic.Text = gridView1.GetRowCellValue(info.RowHandle, "cBasic").ToString();
                
                object o = new object();
                EventArgs e = new EventArgs();
                dtlmode = "OLD";
                oldCode = txtSrNo.Text.ToString();
                txtSrNo_Validated(o, e);
            }
        }
        
        private string DataValidateDTL()
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
                err = err + "Please Select valid payperiod.." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtEmpUnqID.Text))
            {
                err = err + "Please Select Employee Code.." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtEmpName.Text))
            {
                err = err + "Please Select Employee Code.." + Environment.NewLine;
            }

            

            double cBasic = 0;

            if (double.TryParse(txtcBasic.Text.Trim(), out cBasic))
            {
                if (cBasic == 0)
                {
                    err = err + "Please enter basic.." + Environment.NewLine;

                }
            }

            if (txtFromDt.EditValue == null || txtFromDt.DateTime == DateTime.MinValue)
            {
                err = err + "Please Enter From Date.." + Environment.NewLine;
                return err;
            }

            if (txtToDt.EditValue == null || txtToDt.DateTime == DateTime.MinValue)
            {
                err = err + "Please Enter To Date.." + Environment.NewLine;
                return err;
            }

            DateTime FrmDt = txtFromDt.DateTime.Date;
            DateTime Todt = txtToDt.DateTime.Date;

            if (Todt < FrmDt)
            {
                err = err + "Invalid Date Range.." + Environment.NewLine;
                return err;
            }

            if (txtLeftDt.DateTime != DateTime.MinValue)
            {
                err = err + "Employee is left alredy, Updation is not allowed" + Environment.NewLine;
                return err;
            }

            if (FrmDt < PFromDt || Todt < PFromDt )
            {
                err = err + "FromDate/ToDate should not be less than PayPeriod" + Environment.NewLine;
                return err;
            }

            if (Todt > pToDt || FrmDt > pToDt)
            {
                err = err + "FromDate/ToDate should not be grater than PayPeriod" + Environment.NewLine;
                return err;
            }

            return err;
        }

        private void btnAddDtl_Click(object sender, EventArgs e)
        {
            string err = DataValidateDTL();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                string sql = "";

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
                SqlCommand cmd = new SqlCommand();

                if (dtlmode == "OLD")
                {
                    sql = "Delete from  Cont_MastBasic where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' and Srno = '" + txtSrNo.Text.Trim() + "'";
                    cmd = new SqlCommand(sql, cn, tr);
                    cmd.ExecuteNonQuery();
                }

                sql = "Insert into Cont_MastBasic (PayPeriod,EmpUnqID,Srno,FromDt,ToDt,cBasic,AddDt,Addid) values (" +
                    " '" + txtPayPeriod.Text.Trim() + "','" + txtEmpUnqID.Text.Trim() + "','" + txtSrNo.Text.Trim() + "'," +
                    " '" + txtFromDt.DateTime.Date.ToString("yyyy-MM-dd") + "','" + txtToDt.DateTime.Date.ToString("yyyy-MM-dd") + "'," +
                    " '" + txtcBasic.Text.Trim() + "',GetDate(),'" + Utils.User.GUserID + "')";

                cmd = new SqlCommand(sql, cn, tr);
                cmd.ExecuteNonQuery();

                try
                {
                    tr.Commit();
                    MessageBox.Show("Record Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                LoadGrid();
            }
            
        }

        private void btnDelDtl_Click(object sender, EventArgs e)
        {
            string err = DataValidateDTL();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
                string sql = "";

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
                SqlCommand cmd = new SqlCommand();

                if (dtlmode == "OLD")
                {
                    sql = "Delete from  Cont_MastBasic where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' and Srno = '" + txtSrNo.Text.Trim() + "'";
                    cmd = new SqlCommand(sql, cn, tr);
                    cmd.ExecuteNonQuery();
                }

                try
                {
                    tr.Commit();
                    MessageBox.Show("Record Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                LoadGrid();
            }

        }

        private void txtSrNo_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) ||
                string.IsNullOrEmpty(txtPayPeriod.Text.Trim()))
                
            {
                
                return;
            }
            string sql = string.Empty;

            if (string.IsNullOrEmpty(txtSrNo.Text.Trim()))
            {
                sql = "Select Isnull(Max(Srno),0) + 1 from Cont_MastBasic where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";
                txtSrNo.Text = Utils.Helper.GetDescription(sql, Utils.Helper.constr);
                oldCode = "";
                dtlmode = "NEW";
                btnAddDtl.Enabled = true;
                btnDelDtl.Enabled = false;
                return;
            }

            sql = "Select * from Cont_MastBasic where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "' and Srno = '" + txtSrNo.Text.Trim() + "'";

            DataSet empds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            Boolean hasRows = empds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in empds.Tables[0].Rows)
                {
                    oldCode = dr["SrNo"].ToString();
                    txtSrNo.Text = dr["SrNo"].ToString();
                    txtFromDt.DateTime = Convert.ToDateTime(dr["FromDt"]);
                    txtToDt.DateTime = Convert.ToDateTime(dr["ToDt"]);
                    txtcBasic.Text = dr["cBasic"].ToString();
                    dtlmode = "OLD";
                    btnAddDtl.Enabled = true;
                    btnDelDtl.Enabled = true;
                }
            }
            else
            {
                dtlmode = "NEW";
                oldCode = "";
                btnAddDtl.Enabled = true;
                btnDelDtl.Enabled = false;
                sql = "Select Isnull(Max(Srno),0) + 1 from Cont_MastBasic where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";
                txtSrNo.Text = Utils.Helper.GetDescription(sql, Utils.Helper.constr);
                txtFromDt.EditValue = null;
                txtToDt.EditValue  = null;
                txtcBasic.Text = "0";
            }

            

        }
    }


}
