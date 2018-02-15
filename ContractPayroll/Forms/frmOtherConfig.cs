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
    public partial class frmOtherConfig : Form
    {

        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";
        public bool defSet;
        public bool IsLocked;

        public frmOtherConfig()
        {
            InitializeComponent();
        }

        private void LoadGrid()
        {
            if (string.IsNullOrEmpty(txtPayPeriod.Text.Trim()))
            {
                txtPayPeriod.Text = "0";
            }

            DataSet ds = new DataSet();
            string sql = "select * from Cont_ParaMast where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' Order By ParaCode ";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);

            Boolean hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

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
                txtParaCode.Text = gridView1.GetRowCellValue(info.RowHandle, "ParaCode").ToString();
                txtParaDesc.Text = gridView1.GetRowCellValue(info.RowHandle, "ParaDesc").ToString();
                object o = new object();
                EventArgs e = new EventArgs();
                mode = "OLD";
                oldCode = txtParaCode.Text.ToString();
                txtParaCode_Validated(o, e);
            }


        }

       

        private void txtParaCode_Validated(object sender, EventArgs e)
        {
            if ( txtParaCode.Text.Trim() == "")
            {
                mode = "NEW";
                return;
            }

            DataSet ds = new DataSet();
            string sql = "select * From  Cont_ParaMast where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and ParaCode ='" + txtParaCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtPayPeriod.Text = dr["PayPeriod"].ToString();
                    txtParaCode.Text = dr["ParaCode"].ToString();
                    txtParaDesc.Text = dr["ParaDesc"].ToString();
                    txtpValue.Text = dr["PValue"].ToString();
                    txtParaType.Text =dr["rsPer"].ToString();
                    txtFSlab.Text = dr["FSlab"].ToString();
                    txtTSlab.Text = dr["TSlab"].ToString();
                    chkFixed.CheckState = (Convert.ToBoolean(dr["BCFlg"])) ? CheckState.Checked : CheckState.Unchecked;

                    mode = "OLD";
                    oldCode = dr["ParaCode"].ToString();
                }
            }
            else
            {
                mode = "NEW";
                oldCode = "";
            }

            SetRights();
        }

        private void ResetCtrl()
        {
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            object s = new object();
            EventArgs e = new EventArgs();

            if (this.defSet)
            {
                this.Text = "Default Configuration";
                txtPayPeriod.Text = "0";
                txtPayPeriod.Properties.ReadOnly = true;
                GRights = ContractPayroll.Classes.Globals.GetFormRights(this.Name);
            }
            else
            {
                this.Text = "Pay Period Wise Configuration";
                txtPayPeriod.Properties.ReadOnly = true;
                GRights = ContractPayroll.Classes.Globals.GetFormRights("frmPayCyclePara");
            }

            txtParaCode.Text = "";
            txtParaDesc.Text = "";
            txtFSlab.EditValue = null;
            txtTSlab.EditValue = null;
            txtParaType.EditValue = null;
            txtpValue.EditValue = null;

            chkFixed.CheckState = CheckState.Unchecked;

            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if (txtParaCode.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A"))
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (txtParaCode.Text.Trim() != "" && mode == "OLD")
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

        private void frmOtherConfig_Load(object sender, EventArgs e)
        {
            ResetCtrl();
           
            SetRights();
            
        }

        private string DataValidate()
        {
            string err = string.Empty;


            if (IsLocked)
            {
                err = err + "Does not allowed to change in locked period.." + Environment.NewLine;
                return err;
            }
            
            if (string.IsNullOrEmpty(txtPayPeriod.Text))
            {
                err = err + "Please select Pay Period " + Environment.NewLine;
            }
            
            if (string.IsNullOrEmpty(txtParaCode.Text))
            {
                err = err + "Please Enter ParaCode " + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(txtParaDesc.Text))
            {
                err = err + "Please Enter Description.." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(txtParaType.Text))
            {
                err = err + "Please Para Type.." + Environment.NewLine;
            }


            if (string.IsNullOrEmpty(txtpValue.Text))
            {
                err = err + "Please Enter Para Value.." + Environment.NewLine;
            }

            if (defSet == false && txtPayPeriod.Text.Trim() == "0")
            {
                err = err + "Does not allowed to change default values.." + Environment.NewLine;
            }


            if (defSet && txtPayPeriod.Text.Trim() != "0")
            {
                err = err + "Does not allowed to change PayPeriod Para values.." + Environment.NewLine;
            }



            return err;
        }

        private void btnAdd_Click(object sender, EventArgs e)
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
                        string sql = "Insert into Cont_ParaMast " +
                            "(ParaCode,ParaDesc,RsPer,PValue,FSlab,TSlab,BCFlg," +
                            " AddDt,AddID,PayPeriod) Values ('{0}','{1}','{2}','{3}','{4}','{5}',GetDate(),'{6}','{7}') ";                            
                          
                        sql = string.Format(sql,  txtParaCode.Text.Trim().ToString(), txtParaDesc.Text.Trim().ToString(),
                            txtParaType.Text.Trim().ToString(), txtFSlab.Text.Trim().ToString(), txtTSlab.Text.Trim().ToString(), ((chkFixed.Checked) ? "1" : "0"),
                            Utils.User.GUserID,
                            txtPayPeriod.Text.Trim()

                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();
                        LoadGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {

                        cn.Open();
                        cmd.Connection = cn;
                        string sql = "Update Cont_ParaMast Set ParaDesc = '" + txtParaDesc.Text.Trim() + "', RsPer ='" + txtParaType.Text.Trim() + "', " +
                            " PValue = " + ((string.IsNullOrEmpty(txtpValue.Text.ToString())) ? "null" : "'" + txtpValue.Text.ToString() + "'") + " ,FSlab =" + ((string.IsNullOrEmpty(txtFSlab.Text.ToString())) ? "null" : "'" + txtFSlab.Text.ToString() + "'") + ", TSlab =" + ((string.IsNullOrEmpty(txtTSlab.Text.ToString())) ? "null" : "'" + txtTSlab.Text.ToString() + "'") + "," +
                            " BCFlg ='" + (chkFixed.Checked ? 1 : 0) + "',UpdDt = GetDate() , UpdID ='" + Utils.User.GUserID + "' where ParaCode ='" + txtParaCode.Text.Trim() + "' And PayPeriod = '" + txtPayPeriod.Text.Trim() + "'";

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        ResetCtrl();
                        LoadGrid();
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

                DialogResult qs = MessageBox.Show("Are You Sure to Delete this Shift...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (qs == DialogResult.No)
                {
                    return;
                }

                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cn.Open();
                            string sql = "Delete From Cont_ParaMast where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and ParaCode = '" + txtParaCode.Text.Trim().ToString() + "'";
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
                            LoadGrid();
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            // MessageBox.Show("Not Implemented...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetCtrl();
            //GRights = ContractPayroll.Classes.Globals.GetFormRights(this.Name);
            SetRights();
            grid.DataSource = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPayPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";

                if (defSet)
                {
                    sql = "Select distinct PayPeriod from Cont_ParaMast Where  PayPeriod = 0 ";
                }
                else
                {
                    sql = "Select PayPeriod,PayDesc,FromDt,ToDt from Cont_MastPayPeriod Where 1 = 1  ";
                }

                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "PayPeriod", "PayPeriod", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }
                
                if (obj.Count == 0)
                {
                    txtPayPeriod.Text = "0";
                    txtParaCode.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtPayPeriod.Text = "0";
                    txtParaCode.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtPayPeriod.Text = "0";
                    txtParaCode.Text = "";
                    return;
                }
                else
                {

                    txtPayPeriod.Text = obj.ElementAt(0).ToString();

                   
                }
            }
        }
        
        private void txtPayPeriod_Validated(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select * From Cont_MastPayPeriod where PayPeriod='" + txtPayPeriod.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtPayPeriod.Text = dr["PayPeriod"].ToString();
                    txtPayDesc.Text = dr["PayDesc"].ToString();
                    IsLocked = ((Convert.ToBoolean(dr["IsLocked"])) ? true : false);                    
                }
            }
            else
            {
                IsLocked = false;
                txtPayPeriod.Text = "0";
                txtPayDesc.Text = "Default Configuration";
            }               
            
            LoadGrid();
        }
    }
}
