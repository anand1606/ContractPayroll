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

namespace ContractPayroll.Forms
{
    public partial class frmImportEmp : Form
    {
        public string GRights = "XXXV";
        public string mode = "NEW";
        public bool IsLocked = false;
       
        public frmImportEmp()
        {
            InitializeComponent();
        }

        private void ResetCtrl()
        {
            btnImport.Enabled = false;
            

            object s = new object();
            EventArgs e = new EventArgs();

            txtPayPeriod.Properties.ReadOnly = true;
            GRights = ContractPayroll.Classes.Globals.GetFormRights(this.Name);
            txtEmpUnqID.Text = "";
            txtPayPeriod.Text = "";
            txtPayDesc.Text = "";
            pBar.Minimum = 0;
            pBar.Value = 0;
            IsLocked = false;
            mode = "NEW";
            
        }

        private void SetRights()
        {
            if (txtPayPeriod.Text.Trim() != "" &&  GRights.Contains("A"))
            {
                btnImport.Enabled = true;
                if(mode == "NEW")
                    btnImport.Enabled = false;
                
            }
            else if (txtPayPeriod.Text.Trim() != "" )
            {
                btnImport.Enabled = false;

                if (GRights.Contains("U") || GRights.Contains("D"))
                {
                    btnImport.Enabled = true;
                    if (mode == "NEW")
                        btnImport.Enabled = false;
                }                    
                
            }

            if (GRights.Contains("XXXV"))
            {
                btnImport.Enabled = false;                
            }


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
                return err;
            }
            
            
            if (string.IsNullOrEmpty(txtPayDesc.Text))
            {
                err = err + "Please Enter Description.." + Environment.NewLine;
                return err;
            }

            if (mode != "OLD")
            {
                err = err + "Invalid Pay Period.." + Environment.NewLine;
                return err;
            }

            return err;
        }
        
        private void btnImport_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime pFromDt ;
            DateTime pToDt;


            DataSet payds = Utils.Helper.GetData("Select * from Cont_MastPayPeriod where PayPeriod ='" + txtPayPeriod.Text.Trim() + "'",Utils.Helper.constr);
            bool hasRows = payds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (!hasRows)
            {
                MessageBox.Show("payperiod is not created...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                pFromDt = Convert.ToDateTime(payds.Tables[0].Rows[0]["FromDt"]);
                pToDt = Convert.ToDateTime(payds.Tables[0].Rows[0]["ToDt"]);

                if(pFromDt == DateTime.MinValue)
                {
                    MessageBox.Show("Pay Period FromDate has invalid value...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if(pToDt == DateTime.MinValue)
                {
                    MessageBox.Show("Pay Period ToDate has invalid value...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }catch(Exception ex){
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sql = string.Empty;

            if(string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()))
            {
                sql = "Select * From v_EmpMast where Active = 1 and CompCode = '01' and WrkGrp = 'Cont' and Basic > 0 ";
            }else{
                sql = "Select * from v_EmpMast Where EmpUnqID ='" + txtEmpUnqID.Text.Trim() + "' and WrkGrp = 'Cont' and CompCode = '01'";
            }

            

            DataSet emplistds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            hasRows = emplistds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            pBar.Value = 0;
            if (!hasRows)
            {
                MessageBox.Show("No Employee Found...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }else{
                pBar.Maximum = emplistds.Tables[0].Rows.Count;
            }

            
            this.Cursor = Cursors.WaitCursor;

            using(SqlConnection cn = new SqlConnection(Utils.Helper.constr))
            {
               
                LockCtrl();
                try
                {
                    cn.Open();
                }
                catch (SqlException sex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(sex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    unLockCtrl();
                    return;
                }

                #region set_DefFlgs

                string PFFlg = "0";
                sql = "select isnull(Max(Convert(int,AppFlg)),0) FROM [Cont_ParaMast] " +
                  " where ParaCode = 'PF' " +
                  " and PayPeriod = '" + txtPayPeriod.Text.Trim() + "'" +
                  " and AppFlg = 1";
                
                PFFlg = Utils.Helper.GetDescription(sql,Utils.Helper.constr);

                string PTaxFlg = "0"; 
                sql = "select isnull(Max(Convert(int,AppFlg)),0) FROM [Cont_ParaMast] " +
                  " where ParaCode = 'PTAX' " +
                  " and PayPeriod = '" + txtPayPeriod.Text.Trim() + "'" +
                  " and AppFlg = 1";

                PTaxFlg = Utils.Helper.GetDescription(sql, Utils.Helper.constr); 
                
                
                string ESIFlg = "0";
                sql = "select isnull(Max(Convert(int,AppFlg)),0) FROM [Cont_ParaMast] " +
                  " where ParaCode = 'ESI' " +
                  " and PayPeriod = '" + txtPayPeriod.Text.Trim() + "'" +
                  " and AppFlg = 1";

                ESIFlg = Utils.Helper.GetDescription(sql, Utils.Helper.constr); 


                string LWFFlg = "0";
                sql = "select isnull(Max(Convert(int,AppFlg)),0) FROM [Cont_ParaMast] " +
                  " where ParaCode = 'LWF' " +
                  " and PayPeriod = '" + txtPayPeriod.Text.Trim() + "'" +
                  " and AppFlg = 1";

                LWFFlg = Utils.Helper.GetDescription(sql, Utils.Helper.constr); 


                string DeathFlg = "0";
                sql = "select isnull(Max(Convert(int,AppFlg)),0) FROM [Cont_ParaMast] " +
                  " where ParaCode = 'DEATH' " +
                  " and PayPeriod = '" + txtPayPeriod.Text.Trim() + "'" +
                  " and AppFlg = 1";

                DeathFlg = Utils.Helper.GetDescription(sql, Utils.Helper.constr);


                #endregion

                foreach (DataRow dr in emplistds.Tables[0].Rows)
                {
                    SqlTransaction tr = cn.BeginTransaction();
                    Application.DoEvents();
                    sql = "Select * from Cont_MastEmp Where EmpUnqID = '" + dr["EmpUnqID"].ToString() + "' and PayPeriod ='" + txtPayPeriod.Text.Trim() + "'";
                    DataSet empds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                    hasRows = empds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                    if (hasRows)
                    {
                        sql = "Update Cont_MastEmp set EmpName ='" + dr["EmpName"] + "', FatherName='" + dr["FatherName"].ToString() + "'," +
                            " BirthDt ='" + Convert.ToDateTime(dr["BirthDt"]).ToString("yyyy-MM-dd") + "'," +
                            " JoinDt ='" + Convert.ToDateTime(dr["JoinDt"]).ToString("yyyy-MM-dd") + "'," +
                            " Gender ='" + (Convert.ToBoolean(dr["Sex"])?"M":"F") + "'," +
                            " UnitCode='" + dr["UnitCode"].ToString() + "'," +
                            " UnitDesc='" + dr["UnitName"].ToString() + "'," +
                            " DeptCode='" + dr["DeptCode"].ToString() + "'," +
                            " DeptDesc='" + dr["DeptDesc"].ToString() + "'," +
                            " StatCode='" + dr["Statcode"].ToString() + "'," +
                            " StatDesc='" + dr["StatDesc"].ToString() + "'," +
                            " CatCode ='" + dr["CatCode"].ToString() + "'," + 
                            " CatDesc='" + dr["CatDesc"].ToString() + "'," +
                            " DesgCode ='" + dr["DesgCode"].ToString() + "'," +
                            " DesgDesc='" + dr["DesgDesc"].ToString() + "'," +
                            " GradeCode='" + dr["GradCode"].ToString() + "'," +
                            " GradeDesc='" + dr["GradeDesc"].ToString() + "'," +
                            " ContCode='" + dr["ContCode"].ToString() + "'," + 
                            " ContDesc='" + dr["ContName"].ToString() + "'," +
                            " ESINo ='" + dr["ESINo"].ToString() + "'," +
                            " cBasic='" + dr["Basic"].ToString() + "'," + 
                            " PFNo ='" + 0 + "'," +
                            " PFFlg = '" + PFFlg + "'," +
                            " PTaxFlg = '" + PTaxFlg + "'," + 
                            " DeathFlg = '" + DeathFlg + "', " +
                            " LWFFlg = '" + LWFFlg + "', " +                             
                            " ESIFlg = '" + ESIFlg + "', " +
                            " UpdDt=GetDate() ," +
                            " UpdID ='" + Utils.User.GUserID + "' Where EmpUnqID ='" + dr["EmpUnqID"].ToString() + "' " +
                            " and PayPeriod ='" + txtPayPeriod.Text.Trim() + "'";

                        try
                        {
                            SqlCommand cmd = new SqlCommand(sql, cn, tr);
                            cmd.ExecuteNonQuery();

                            try
                            {
                                tr.Commit();
                                tr.Dispose();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tr.Rollback();
                                tr.Dispose();
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                    }else
                    {
                        sql = "Insert into Cont_MastEmp (PayPeriod,EmpUnqID,EmpName,FatherName,BirthDt,JoinDt,Gender,UnitCode," +
                        " UnitDesc,DeptCode,DeptDesc,Statcode,StatDesc,CatCode,CatDesc,DesgCode,DesgDesc," +
                        " GradeCode,GradeDesc,ContCode,ContDesc,ESINo,cBasic,PFNo,PFFlg,PTaxFlg,DeathFlg," +
                        " LWFFlg,ESIFlg,Active,AddDt,AddID ) values ('" + txtPayPeriod.Text.Trim() + "'," +
                        " '" + dr["EmpUnqID"].ToString() + "'," +
                        " '" + dr["EmpName"].ToString() + "'," +
                        " '" + dr["FatherName"].ToString() + "'," +
                        " '" + Convert.ToDateTime(dr["BirthDt"]).ToString("yyyy-MM-dd") + "'," +
                        " '" + Convert.ToDateTime(dr["JoinDt"]).ToString("yyyy-MM-dd") + "'," +
                        " '" + (Convert.ToBoolean(dr["Sex"]) ? "M" : "F") + "'," +
                        " '" + dr["UnitCode"].ToString() + "'," +
                        " '" + dr["UnitName"].ToString() + "'," +
                        " '" + dr["DeptCode"].ToString() + "'," +
                        " '" + dr["DeptDesc"].ToString() + "'," +
                        " '" + dr["Statcode"].ToString() + "'," +
                        " '" + dr["StatDesc"].ToString() + "'," +
                        " '" + dr["CatCode"].ToString() + "'," +
                        " '" + dr["CatDesc"].ToString() + "'," +
                        " '" + dr["DesgCode"].ToString() + "'," +
                        " '" + dr["DesgDesc"].ToString() + "'," +
                        " '" + dr["GradCode"].ToString() + "'," +
                        " '" + dr["GradeDesc"].ToString() + "'," +
                        " '" + dr["ContCode"].ToString() + "'," +
                        " '" + dr["ContName"].ToString() + "'," +
                        " '" + dr["ESINo"].ToString() + "'," +
                        " '" + dr["Basic"].ToString() + "'," +
                        " '" + 0 + "'," +
                        " '" + PFFlg + "'," +
                        " '" + PTaxFlg + "'," +
                        " '" + DeathFlg + "', " +
                        " '" + LWFFlg + "', " +
                        " '" + ESIFlg + "', " +
                        "  1, " +
                        " GetDate() ," +
                        " '" + Utils.User.GUserID + "')";
                        try
                        {
                            SqlCommand cmd = new SqlCommand(sql, cn, tr);
                            cmd.ExecuteNonQuery();

                            //insert into Cont_MastBasic
                            sql = "Delete From Cont_MastBasic where PayPeriod='" + txtPayPeriod.Text.Trim() + "' And EmpUnqID = '" + dr["EmpUnqID"].ToString() + "' ";
                            SqlCommand cmd1 = new SqlCommand(sql, cn, tr);
                            cmd1.ExecuteNonQuery();

                            sql = "Insert into Cont_MastBasic (PayPeriod,EmpUnqID,SrNo,FromDt,ToDt,cBasic,AddDt,AddID) values (" +
                                " '" + txtPayPeriod.Text.Trim() + "','" + dr["EmpUnqID"].ToString() + "',1," +
                                " '" + pFromDt.ToString("yyyy-MM-dd") + "'," +
                                " '" + pToDt.ToString("yyyy-MM-dd") + "'," +
                                " '" + dr["Basic"].ToString() + "',GetDate(), '" + Utils.User.GUserID + "')";

                            SqlCommand cmd2 = new SqlCommand(sql, cn, tr);
                            cmd2.ExecuteNonQuery();

                            try
                            {
                                tr.Commit();
                                tr.Dispose();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                tr.Rollback();
                                tr.Dispose();
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }                      

                    }
                    
                    pBar.Value += 1;
                    pBar.Update();


                }


                

                unLockCtrl();

                
            }


            this.Cursor = Cursors.Default;
            MessageBox.Show("Process Completed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void LockCtrl()
        {
            txtPayPeriod.Enabled = false;
            txtPayDesc.Enabled = false;
            btnImport.Enabled = false;
            txtEmpUnqID.Enabled = false;
        }

        private void unLockCtrl()
        {
            txtPayPeriod.Enabled = true;
            txtPayDesc.Enabled = true;
            btnImport.Enabled = true;
            txtEmpUnqID.Enabled = true;
        }

        private void txtPayPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select PayPeriod,PayDesc,FromDt,ToDt from Cont_MastPayPeriod Where 1 = 1  Order by PayPeriod Desc";


                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "PayPeriod", "PayPeriod", typeof(int), Utils.Helper.constr, "System.Data.SqlClient",
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
                    IsLocked = ((Convert.ToBoolean(dr["IsLocked"])) ? true : false);
                    btnImport.Enabled = true;
                    mode = "OLD";
                }
            }
            else
            {
                btnImport.Enabled = false;
                mode = "NEW";

            }           

            SetRights();
        }

        private void frmImportEmp_Load(object sender, EventArgs e)
        {
            ResetCtrl();
        }

    }
}
