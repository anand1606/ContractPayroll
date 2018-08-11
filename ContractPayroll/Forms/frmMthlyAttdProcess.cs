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
using System.Diagnostics;

namespace ContractPayroll.Forms
{
    public partial class frmMthlyAttdProcess : Form
    {
        public string GRights = "XXXV";
        public string mode = "NEW";
        public bool IsLocked = false;
        public DateTime pFromDt;
        public DateTime pToDt;

        public frmMthlyAttdProcess()
        {
            InitializeComponent();
        }

        private void ResetCtrl()
        {
            btnProcess.Enabled = false;
            

            object s = new object();
            EventArgs e = new EventArgs();

            txtPayPeriod.Properties.ReadOnly = true;
            GRights = ContractPayroll.Classes.Globals.GetFormRights(this.Name);

            txtPayPeriod.Text = "";
            txtPayDesc.Text = "";

            txtEmpUnqID.Enabled = true;
            txtContCode.Enabled = true;
            txtContCode.Text = "";
            txtContName.Text = "";
            txtEmpUnqID.Text = "";
            txtEmpName.Text = "";

           
            pBar.Minimum = 0;
            pBar.Value = 0;
            IsLocked = false;
            mode = "NEW";
            pFromDt = DateTime.MinValue;
            pToDt = DateTime.MinValue;

        }

        private void SetRights()
        {
            if (txtPayPeriod.Text.Trim() != "" &&  GRights.Contains("A"))
            {
                btnProcess.Enabled = true;
                if(mode == "NEW")
                    btnProcess.Enabled = false;
                
            }
            else if (txtPayPeriod.Text.Trim() != "" )
            {
                btnProcess.Enabled = false;

                if (GRights.Contains("U") || GRights.Contains("D"))
                {
                    btnProcess.Enabled = true;
                    if (mode == "NEW")
                        btnProcess.Enabled = false;
                }                    
                
            }

            if (GRights.Contains("XXXV"))
            {
                btnProcess.Enabled = false;                
            }


        }

        private string DataValidate()
        {
            string err = string.Empty;

            
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

            if (txtEmpUnqID.Text.Trim().ToString() != "")
            {
                txtContCode.Text = "";
                txtContName.Text = "";
            }

            if (txtContCode.Text.Trim().ToString() != "")
            {
                txtEmpUnqID.Text = "";
                txtEmpName.Text = "";
            }

            return err;
        }
        
        private void btnProcess_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int pPay = 0;

            DataSet payds = Utils.Helper.GetData("Select * from Cont_MastPayPeriod where PayPeriod ='" + txtPayPeriod.Text.Trim() + "'  ",Utils.Helper.constr);
            bool hasRows = payds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (!hasRows)
            {
                MessageBox.Show("did not found payperiod...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                pFromDt = Convert.ToDateTime(payds.Tables[0].Rows[0]["FromDt"]);
                pToDt = Convert.ToDateTime(payds.Tables[0].Rows[0]["ToDt"]);
                pPay = Convert.ToInt32(payds.Tables[0].Rows[0]["PayPeriod"]);

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

            if (string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) && string.IsNullOrEmpty(txtContCode.Text.Trim()))
            {

                 sql = "Select *,b.PFFlg,b.active From Cont_MthlyAtn a, Cont_MastEmp b " +
                        " where a.PayPeriod = b.PayPeriod and a.EmpUnqID = b.EmpUnqID " +
                        " And a.PayPeriod ='" + txtPayPeriod.Text.Trim() + "'";
            }
            else if (!string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) && string.IsNullOrEmpty(txtContCode.Text.Trim()))
            {
                sql = "Select *,b.PFFlg , b.active From Cont_MthlyAtn a, Cont_MastEmp b " +
                        " where a.PayPeriod = b.PayPeriod and a.EmpUnqID = b.EmpUnqID " +
                        " And a.PayPeriod ='" + txtPayPeriod.Text.Trim() + "' and a.EmpUnqID ='" + txtEmpUnqID.Text.Trim() + "'";
            }
            else if (!string.IsNullOrEmpty(txtContCode.Text.Trim()))
            {
                sql = "Select *,b.PFFlg , b.active From Cont_MthlyAtn a, Cont_MastEmp b " +
                        " where a.PayPeriod = b.PayPeriod and a.EmpUnqID = b.EmpUnqID " +
                        " And a.PayPeriod ='" + txtPayPeriod.Text.Trim() + "' and a.ContCode ='" + txtContCode.Text.Trim() + "'";
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

                //Load Pay Cycle wise Parameters
                DataSet PayPara = Utils.Helper.GetData("Select * from Cont_ParaMast where PayPeriod='" + pPay + "' and AppFlg = 1", Utils.Helper.constr);
                hasRows = PayPara.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                var para = PayPara.Tables[0].Copy().AsEnumerable();


                //define para vars
                double PFRate = 0;
                double EPFRate = 0;
                double EPSRate = 0;
                double CoPFRate = 0;
                double CoEduRate = 0;
                double CoServTaxRate = 0;
                double CoServAddTaxRate = 0;
                double CoEduAddRate = 0;
                try{
                    /*
                     EPF       
                     EPS       
                     PF                        
                     * */
                    
                    PFRate = Convert.ToDouble(((from t in para where (t.Field<string>("ParaCode") == "PF" && t.Field<bool>("AppFlg") == true) select t).First())["PValue"]);
                    EPFRate = Convert.ToDouble(((from t in para where (t.Field<string>("ParaCode") == "EPF" && t.Field<bool>("AppFlg") == true) select t).First())["PValue"]);
                    EPSRate = Convert.ToDouble(((from t in para where (t.Field<string>("ParaCode") == "EPS" && t.Field<bool>("AppFlg") == true) select t).First())["PValue"]);
                    
                    CoPFRate = Convert.ToDouble(((from t in para where (t.Field<string>("ParaCode") == "COPFRATE" && t.Field<bool>("AppFlg") == true) select t).First())["PValue"]);
                    CoEduRate = Convert.ToDouble(((from t in para where (t.Field<string>("ParaCode") == "COEDUTAX" && t.Field<bool>("AppFlg") == true)  select t).First())["PValue"]);
                    CoServTaxRate = Convert.ToDouble(((from t in para where (t.Field<string>("ParaCode") == "COSERVTAX" && t.Field<bool>("AppFlg") == true) select t).First())["PValue"]);
                    CoServAddTaxRate = Convert.ToDouble(((from t in para where (t.Field<string>("ParaCode") == "COSERV25TAX" && t.Field<bool>("AppFlg") == true) select t).First())["PValue"]);
                    CoEduAddRate = Convert.ToDouble(((from t in para where (t.Field<string>("ParaCode") == "COEDU25TAX" && t.Field<bool>("AppFlg") == true) select t).First())["PValue"]);


                }catch(Exception ex){

                }
                
                
                foreach (DataRow dr in emplistds.Tables[0].Rows)
                {
                    
                    Application.DoEvents();


                    if (!Convert.ToBoolean(dr["Active"]))
                    {
                        sql = "Delete From Cont_MthlyAtn Where PayPeriod ='" + txtPayPeriod.Text.Trim() + "' And EmpUnqID = '" + dr["EmpUnqID"].ToString() + "'";
                        SqlCommand cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();

                        sql = "Delete From Cont_MthlyPay Where PayPeriod ='" + txtPayPeriod.Text.Trim() + "' And EmpUnqID = '" + dr["EmpUnqID"].ToString() + "'";
                        cmd = new SqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                        continue;
                    } 

                    sql = " SELECT [PayPeriod] " +
                          " ,[EmpUnqID] " +
                          " ,[SrNo] " +
                          " , Max(CostCode) as CostCode" +
                          " ,sum([TpaHrs]) as Cal_TpaHrs " +
                          " ,sum([TpaAmt]) as Cal_TpaAmt " +
                          " ,sum([DaysPay]) as Cal_DaysPay " +
                          " ,sum([WODays]) as Cal_WoDays " +
                          " ,sum([Cal_Basic]) as Cal_Basic " +
                          " ,sum([CoCommAmt]) as Cal_CoCommAmt " +
                          " ,sum([CoCommWOAmt]) as Cal_CoCommWoAmt " +
                          " ,sum([Cal_SplAmt]) as Cal_SPLAmt " +
                          " ,sum([Cal_BAAmt]) as Cal_BAAmt " +                          
                          " ,avg(CoCommRate) as CoCommRate " + 
                          " FROM [Cont_DailyOth] " +
                          " group by PayPeriod,EmpUnqID,SrNo " +
                          " having PayPeriod = '" + dr["PayPeriod"].ToString() + "' " +
                    " and EmpUnqID = '" + dr["EmpUnqID"].ToString() + "' and SrNo = '" + dr["SrNo"].ToString() + "'";

                    //Debug.Assert(dr["EmpUnqID"].ToString() != "40055116");

                    DataSet attds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                    hasRows = attds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                    if (hasRows)
                    {
                        foreach (DataRow adr in attds.Tables[0].Rows)
                        {
                            try
                            {
                                
                                double Cal_TpaHrs = Convert.ToDouble(adr["Cal_TpaHrs"]) ;                                
                                double Cal_Basic = Math.Round(Convert.ToDouble(adr["Cal_Basic"]),2);
                                double Cal_TpaAmt = Math.Round(Convert.ToDouble(adr["Cal_TpaAmt"]),0);
                                double Cal_DaysPay = Convert.ToDouble(adr["Cal_DaysPay"]);
                                double Cal_WoDays = Convert.ToDouble(adr["Cal_WoDays"]);

                                double Cal_SPLAmt = Math.Round(Convert.ToDouble(adr["Cal_SPLAmt"]),2);
                                double Cal_BAAmt = Math.Round(Convert.ToDouble(adr["Cal_BAAmt"]),2);

                                double Cal_CoCommDays = Cal_DaysPay ;
                                double Cal_CoWoCommDays = Cal_WoDays;
                                double Cal_CoCommAmt = Convert.ToDouble(adr["Cal_CoCommAmt"]);
                                double Cal_CoCommWoAmt = Convert.ToDouble(adr["Cal_CoCommWoAmt"]);

                                double Adj_AdjDays = Convert.ToDouble(dr["Adj_DaysPay"]);
                                double Adj_AdjDayAmt = Convert.ToDouble(dr["Adj_DaysPayAmt"]);

                                double Adj_SPLRate = Convert.ToDouble(dr["Adj_SPLRate"]);
                                double Adj_SPLDays = Convert.ToDouble(dr["Adj_SPLDaysPay"]);
                                double Adj_SPLAmt = Convert.ToDouble(dr["Adj_SPLAmt"]);

                                double Adj_BARate = Convert.ToDouble(dr["Adj_BARate"]);
                                double Adj_BADays = Convert.ToDouble(dr["Adj_BADaysPay"]);
                                double Adj_BAAmt = Convert.ToDouble(dr["Adj_BAAmt"]);

                                double Adj_TpaHrs = Convert.ToDouble(dr["Adj_TpaHrs"]);
                                double Adj_TpaAmt = Math.Round(Convert.ToDouble(dr["Adj_TpaAmt"]),0);
                                double Adj_Amt = Convert.ToDouble(dr["Adj_Amt"]);

                                double Adj_CoCommDays = Adj_AdjDays;

                                double CoCommRate = Convert.ToDouble(adr["CoCommRate"]);
                                double Adj_CoCommAmt = CoCommRate * Adj_CoCommDays;

                                double Tot_EarnBasic = Cal_Basic + Adj_AdjDayAmt ;
                                double Tot_TpaHrs = Cal_TpaHrs + Adj_TpaHrs;
                                double Tot_TpaAmt = Cal_TpaAmt + Adj_TpaAmt;
                                double Tot_DaysPay = Cal_DaysPay + Adj_AdjDays ;

                                double Tot_SPLAmt = Cal_SPLAmt + Adj_SPLAmt  ;
                                double Tot_BAAmt =  Cal_BAAmt + Adj_BAAmt;

                                
                                double Tot_CoCommDays = Cal_CoCommDays + Adj_CoCommDays + Cal_CoWoCommDays;
                                double Cal_CoCommPFAmt = ((Tot_EarnBasic) * CoPFRate / 100);
                                double Tot_CoCommAmt = Cal_CoCommAmt + Cal_CoCommWoAmt + Adj_CoCommAmt + Cal_CoCommPFAmt + Tot_EarnBasic  + Tot_SPLAmt + Tot_BAAmt;

                                double Tot_Earning = Tot_EarnBasic  +  Tot_SPLAmt + Tot_BAAmt + Adj_Amt;

                                double Cal_PF =0;
                                double Cal_EPF =0;
                                double Cal_EPS = 0;
                                
                                //check for emp pf flg
                                if (Convert.ToBoolean(dr["PFFlg"]))
                                {
                                    Cal_PF = Math.Round((Cal_Basic * PFRate / 100), MidpointRounding.AwayFromZero) + Math.Round((Adj_AdjDayAmt * PFRate / 100), MidpointRounding.AwayFromZero);

                                    Cal_EPF = ((Cal_PF) * EPFRate / 100);
                                    Cal_EPS = ((Cal_PF) * EPSRate / 100);

                                    if (Cal_EPS > 541)
                                    {
                                        Cal_EPF = Cal_EPF + (Cal_EPS - 541);
                                        Cal_EPS = 541;
                                    }
                                }
                                
                                
                                double Cal_CoServTaxAmt = ((Tot_CoCommAmt) * CoServTaxRate/100) ;
                                double Cal_CoEduTaxAmt = ((Cal_CoServTaxAmt) * CoEduRate / 100);
                                double Cal_CoServAddTaxAmt = ((Cal_CoServTaxAmt) * CoServAddTaxRate / 100);
                                double Cal_CoEduAddTaxAmt = ((Cal_CoEduTaxAmt) * CoEduAddRate / 100);
                                
                                

                                sql = "Update Cont_MthlyAtn Set " +
                                    " CostCode = '" + adr["CostCode"].ToString() + "'" +
                                    " ,Cal_Basic = '" + Cal_Basic.ToString() + "'" +
                                    " ,Cal_DaysPay = '" + Cal_DaysPay.ToString() + "'" +
                                    " ,Cal_WODays = '" + Cal_WoDays.ToString() + "'" +
                                    " ,Cal_TpaHrs = '" + Cal_TpaHrs.ToString() + "'" +
                                    " ,Cal_TpaAmt = '" + Cal_TpaAmt.ToString() + "'" +
                                    " ,Tot_DaysPay = '" + Cal_DaysPay.ToString() + "'" +
                                    " ,Tot_EarnBasic = '" + Tot_EarnBasic.ToString() + "'" +
                                    " ,Tot_TpaHrs = '" + Tot_TpaHrs.ToString() + "'" +
                                    " ,Tot_TpaAmt = '" + Tot_TpaAmt.ToString() + "'" +
                                    " ,CAL_SPLAmt = '" + Cal_SPLAmt.ToString() + "'" +
                                    " ,CAL_BAAmt = '" + Cal_BAAmt.ToString() + "'" +                                    
                                    " ,Tot_SPLAmt = '" + Tot_SPLAmt.ToString() + "'" +
                                    " ,Tot_BAAmt = '" + Tot_BAAmt.ToString() + "'" +                                    
                                    " ,Tot_Earnings = '" + Tot_Earning.ToString() + "'" +
                                    " ,Cal_PF = '" + Cal_PF.ToString() + "'" +
                                    " ,Cal_EPF = '" + Cal_EPF.ToString() + "'" +
                                    " ,Cal_EPS = '" + Cal_EPS.ToString() + "'" +
                                    " ,CoCommRate = '" + CoCommRate.ToString() + "'" +
                                    " ,Adj_CoCommDays = '" + Adj_CoCommDays.ToString() + "'" +
                                    " ,Cal_CoCommDays = '" + Cal_CoCommDays.ToString() + "'" +
                                    " ,Cal_CoWoCommDays = '" + Cal_CoWoCommDays.ToString() + "'" +
                                    " ,Tot_CoCommDays = '" + Tot_CoCommDays.ToString() + "'" +
                                    " ,Adj_CoCommAmt = '" + Adj_CoCommAmt.ToString() + "'" +
                                    " ,Cal_CoCommAmt = '" + Cal_CoCommAmt.ToString() + "'" +
                                    " ,Cal_CoCommWoAmt = '" + Cal_CoCommWoAmt.ToString() + "'" +
                                    " ,Tot_CoCommAmt = '" + Tot_CoCommAmt.ToString() + "'" +
                                    " ,Cal_CoCommPFAmt = '" + Cal_CoCommPFAmt.ToString() + "'" +
                                    " ,Cal_CoServTaxAmt = '" + Cal_CoServTaxAmt.ToString() + "'" +
                                    " ,Cal_CoEduTaxAmt = '" + Cal_CoEduTaxAmt.ToString() + "'" +
                                    " ,Cal_CoServTax25Amt ='" + Cal_CoServAddTaxAmt.ToString() + "'" +
                                    " ,Cal_CoEduTax25Amt = '" + Cal_CoEduAddTaxAmt.ToString() + "'" +
                                    " ,UpdDT = GetDate() " +
                                    " ,UpdID = '" + Utils.User.GUserID + "' " +
                                    " Where PayPeriod ='" + dr["PayPeriod"].ToString() + "' " +
                                    " And EmpUnqID ='" + dr["EmpUnqID"].ToString() + "' " +
                                    " And SrNo = '" + dr["SrNo"].ToString() + "'";                                

                                SqlCommand cmd = new SqlCommand(sql, cn);
                                cmd.ExecuteNonQuery();

                               

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }

                        } //foreach date

                    }//if has attdrecs    

                    pBar.Value += 1;
                    pBar.Update();
                    Application.DoEvents();
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
            btnProcess.Enabled = false;
            txtEmpUnqID.Enabled = false;
            txtContCode.Enabled = false;
        }

        private void unLockCtrl()
        {
            txtPayPeriod.Enabled = true;
            txtPayDesc.Enabled = true;
            btnProcess.Enabled = true;
            txtEmpUnqID.Enabled = true;
            txtContCode.Enabled = true;
        }

        private void txtPayPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select PayPeriod,PayDesc,FromDt,ToDt from Cont_MastPayPeriod Where 1 = 1  Order by PayPeriod desc";


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
                    pFromDt = Convert.ToDateTime(dr["FromDt"]);
                    pToDt = Convert.ToDateTime(dr["ToDt"]);
                    btnProcess.Enabled = true;
                    mode = "OLD";
                }
            }
            else
            {
                btnProcess.Enabled = false;
                mode = "NEW";
                pFromDt = DateTime.MinValue;
                pToDt = DateTime.MinValue;

            }           

            SetRights();
        }

        private void frmMthlyAttdProcess_Load(object sender, EventArgs e)
        {
            ResetCtrl();
        }

        private void txtEmpUnqID_Validated(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select EmpName From Cont_MastEmp where  PayPeriod = '" + txtPayPeriod.Text.Trim().ToString() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtEmpName.Text = dr["EmpName"].ToString();
                    txtContCode.Text = "";
                    txtContName.Text = "";
                   
                }
            }
            else
            {
                txtEmpName.Text = "";
                txtEmpUnqID.Text = "";

            }

            SetRights();
        }

        private void txtContCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select ContCode,ContName from MastCont Where CompCode = '01' and WrkGrp = 'Cont' ";


                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "ContCode", "ContCode", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
                   100, 300, 400, 600, 100, 100);
                }

                if (obj.Count == 0)
                {
                    txtContCode.Text = "";
                    txtContName.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtContCode.Text = "";
                    txtContName.Text = "";
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtContCode.Text = "";
                    txtContName.Text = "";
                    return;
                }
                else
                {
                    txtContCode.Text = obj.ElementAt(0).ToString();
                    txtContName.Text = obj.ElementAt(1).ToString();

                }
            }
        }

        private void txtContCode_Validated(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select Distinct ContCode,ContDesc From Cont_MastEmp Where PayPeriod = '" + txtPayPeriod.Text.Trim().ToString() + "' and ContCode = '" + txtContCode.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtEmpUnqID.Text = "";
                    txtEmpName.Text = "";
                    txtContName.Text = dr["ContDesc"].ToString();
                    
                }
            }
            else
            {
                txtContName.Text = "";
                txtContCode.Text = "";
            }

            SetRights();
        }

        private void txtEmpUnqID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 || e.KeyCode == Keys.F2)
            {
                List<string> obj = new List<string>();

                Help_F1F2.ClsHelp hlp = new Help_F1F2.ClsHelp();
                string sql = "";


                sql = "Select EmpUnqID,EmpName,ContCode from Cont_MastEmp Where PayPeriod = '" + txtPayPeriod.Text.Trim().ToString() + "'";

                if (e.KeyCode == Keys.F1)
                {
                    obj = (List<string>)hlp.Show(sql, "EmpUnqID", "EmpUnqID", typeof(string), Utils.Helper.constr, "System.Data.SqlClient",
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


    }
}
