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
    public partial class frmPayPeriod : Form
    {

        public string mode = "NEW";
        public string GRights = "XXXV";
        public string oldCode = "";
        
        public frmPayPeriod()
        {
            InitializeComponent();
        }

        private string DataValidate()
        {
            string err = string.Empty;

            if(mode != "NEW")
            {
                if (string.IsNullOrEmpty(txtPayPeriod.Text))
                {
                    err = err + "Please select Pay Period " + Environment.NewLine;
                }
                return err;
            }

            if (string.IsNullOrEmpty(txtParaDesc.Text))
            {
                err = err + "Please Enter Description.." + Environment.NewLine;
                return err;
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
                        string sql = "Insert into Cont_MastPayPeriod " +
                            "(PayPeriod,PayDesc,FromDt,ToDt,isLocked," +
                            " AddDt,AddID) Values ('{0}','{1}','{2:yyyy-MM-dd}','{3:yyyy-MM-dd}','{4}',GetDate(),'{5}') ";

                        sql = string.Format(sql, txtPayPeriod.Text.Trim().ToString(), txtParaDesc.Text.Trim().ToString(),
                            txtFromDt.DateTime.Date, txtToDt.DateTime.Date,((chkLocked.Checked) ? "1" : "0"),
                            Utils.User.GUserID
                            );

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        //insert default paraval records for payperiod

                        sql = "Insert into Cont_ParaMast ([PayPeriod],[ParaCode],[ParaDesc],[RsPer],[PValue],[FSlab],[TSlab],[BCFLG],[AddDt],[AddID] ) " +
                            " Select '" + txtPayPeriod.Text.Trim().ToString() + "',[ParaCode],[ParaDesc],[RsPer],[PValue],[FSlab],[TSlab],[BCFLG],GetDate()," +
                            " '" + Utils.User.GUserID + "' From Cont_ParaMast where PayPeriod = 0 ";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();


                        MessageBox.Show("Record saved...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetCtrl();
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private bool PayPeriod_HasTran()
        {
            if (string.IsNullOrEmpty(txtPayPeriod.Text.Trim()))
                return true;

            if (txtPayPeriod.Text.Trim() == "0")
                return true;


            bool err = true;
            string count = Utils.Helper.GetDescription("Select count(*) from Cont_MthlyAtn where PayPeriod ='" + txtPayPeriod.Text.Trim() + "'", Utils.Helper.constr);
            if (string.IsNullOrEmpty(count))
            {
                err = false;
            }
            else
            {
                if (Convert.ToInt32(count) > 0)
                    err = true;
                else
                    err = false;
            }

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

            if (!PayPeriod_HasTran())
            {
                using (SqlConnection cn = new SqlConnection(Utils.Helper.constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {

                            cn.Open();
                            cmd.Connection = cn;
                            string sql = "Update Cont_MastPayPeriod Set PayDesc = '" + txtParaDesc.Text.Trim() + "', FromDt ='" + txtFromDt.DateTime.Date.ToString("yyyy-MM-dd") + "', " +
                                " ToDate = '" + txtToDt.DateTime.Date.ToString("yyyy-MM-dd") + "', isLocked = '" + (chkLocked.Checked ? 1 : 0) + "',UpdDt = GetDate()," +
                                " UpdID ='" + Utils.User.GUserID + "' where PayPeriod = '" + txtPayPeriod.Text.Trim() + "'";

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
            else
            {
                MessageBox.Show("System does not allow to update, Transaction already made", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


            if (PayPeriod_HasTran())
            {
                MessageBox.Show("System does not allow to delete, Transaction already made", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(err))
            {

                DialogResult qs = MessageBox.Show("Are You Sure to Delete this PayPeriod...?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                            string sql = "Delete From Cont_MastPayPeriod where PayPeriod = '" + txtPayPeriod.Text.Trim() + "'";
                            cmd.CommandText = sql;
                            cmd.Connection = cn;
                            cmd.ExecuteNonQuery();


                            MessageBox.Show("Record Deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetCtrl();
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
            SetRights();           
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

               
                sql = "Select PayPeriod,PayDesc,FromDt,ToDt from Cont_MastPayPeriod Where 1 = 1  ";
                

                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "PayPeriod", "PayPeriod", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {
                    txtPayPeriod.Text = "";

                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtPayPeriod.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtPayPeriod.Text = "0";

                    return;
                }
                else
                {

                    txtPayPeriod.Text = obj.ElementAt(0).ToString();


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

            txtPayPeriod.Text = "";
            txtParaDesc.Text = "";
            txtFromDt.EditValue = null;
            txtToDt.EditValue = null;
            
            chkLocked.CheckState = CheckState.Unchecked;

            oldCode = "";
            mode = "NEW";
        }

        private void SetRights()
        {
            if (txtPayPeriod.Text.Trim() != "" && mode == "NEW" && GRights.Contains("A"))
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            else if (txtPayPeriod.Text.Trim() != "" && mode == "OLD")
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
                txtPayPeriod.Text = Utils.Helper.GetDescription("SELECT isnull(Max(PayPeriod),0) + 1 FROM Cont_MastPayPeriod", Utils.Helper.constr);
                mode = "NEW";
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
                        txtParaDesc.Text = dr["PayDesc"].ToString();
                        txtFromDt.EditValue = dr["FromDt"].ToString();
                        txtToDt.EditValue = dr["ToDt"].ToString();
                        chkLocked.Checked = ((Convert.ToBoolean(dr["IsLocked"])) ? true : false);
                        mode = "OLD";
                        
                    }
                }
                else
                {
                    txtPayPeriod.Text = Utils.Helper.GetDescription("SELECT isnull(Max(PayPeriod),0) + 1 FROM Cont_MastPayPeriod", Utils.Helper.constr);
                    mode = "NEW";
                    
                }                
            }

            SetRights();
        }

        private void frmPayPeriod_Load(object sender, EventArgs e)
        {
            ResetCtrl();
            SetRights();
        }


    }
}
