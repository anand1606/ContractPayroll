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
    public partial class frmImportAttd : Form
    {
        public string GRights = "XXXV";
        public string mode = "NEW";
        public bool IsLocked = false;
        public DateTime pFromDt;
        public DateTime pToDt;

        public frmImportAttd()
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
        
        private void btnImport_Click(object sender, EventArgs e)
        {
            string err = DataValidate();
            if (!string.IsNullOrEmpty(err))
            {
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int pPay = 0;

            DataSet payds = Utils.Helper.GetData("Select * from Cont_MastPayPeriod where PayPeriod ='" + txtPayPeriod.Text.Trim() + "'",Utils.Helper.constr);
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
                sql = "Select * From Cont_MastEmp where Active = 1 and PayPeriod ='" + txtPayPeriod.Text.Trim() + "'";
            }
            else if(!string.IsNullOrEmpty(txtContCode.Text.Trim()) && string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()))
            {
                sql = "Select * From Cont_MastEmp where Active = 1 and ContCode = '" + txtContCode.Text.Trim() + "'  and PayPeriod ='" + txtPayPeriod.Text.Trim() + "' ";
               
            }
            else if (!string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()) )
            {
                sql = "Select * From Cont_MastEmp where Active = 1 and PayPeriod ='" + txtPayPeriod.Text.Trim() + "' and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";
               
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
                DataSet PayPara = Utils.Helper.GetData("Select * from Cont_ParaMast where PayPeriod='" + pPay + "'", Utils.Helper.constr);
                hasRows = PayPara.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                var para = PayPara.Tables[0].Copy().AsEnumerable();


                //define para vars
                double CoCommRate = 0;
              

                try{
                   /*
                    COCOMM
                    COEDUTAX
                    COPFRATE
                    COSERVTAX                
                    * */                    
                    //CoCommRate = Convert.ToDouble(from q in para.AsEnumerable() where q.Field<string>("ParaCode") == "COCOMM" select q["PValue"]);
                    CoCommRate = Convert.ToDouble(((from t in para where t.Field<string>("ParaCode") == "COCOMM" select t).First())["PValue"]);
                
                }catch(Exception ex){

                }
                
                
                foreach (DataRow dr in emplistds.Tables[0].Rows)
                {
                    
                    Application.DoEvents();
                    sql = "Select tDate,EmpUnqID,LeaveTyp,ConsWrkHrs,ConsOverTime,[Status],rptStatus,ActualStatus,CostCode,[HalfDay] from AttdData " + 
                        " Where EmpUnqID = '" + dr["EmpUnqID"].ToString() + "' and CompCode = '01' and WrkGrp = 'CONT' " + 
                        " and tdate between '" + pFromDt.ToString("yyyy-MM-dd") + "' and '" + pToDt.ToString("yyyy-MM-dd") + "' Order By tDate";
                   
                    DataSet attds = Utils.Helper.GetData(sql, Utils.Helper.constr);
                    hasRows = attds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                    if (hasRows)
                    {
                        foreach (DataRow adr in attds.Tables[0].Rows)
                        {

                            #region setingstatus

                            DateTime tDate = Convert.ToDateTime(adr["tDate"]);
                            int BasicSr = 1;
                            int BASr = 1;
                            int SPLSr = 1;
                           
                            double cBasic = 0;
                            double CalBasic = 0;
                            
                            double cBAAll = 0;
                            double CalBAAll = 0;
                            double cSPLAll = 0;
                            double CalSPLAll = 0;

                            double WrkHrs = Convert.ToDouble(adr["ConsWrkHrs"]);
                            double TpaHrs = Convert.ToDouble(adr["ConsOverTime"]);
                            double TpaAmt = 0;
                            int WoDays = 0;
                            double DaysPay = 0;
                            double CoCommAmt =0 ;
                            double CoWoAmt = 0;



                            string ABPR = adr["Status"].ToString();
                            string LeaveTyp = adr["LeaveTyp"].ToString();                            
                            string CostCode = adr["CostCode"].ToString();

                            if (TpaHrs >= 8 && LeaveTyp.Contains("WO"))
                            {
                                WoDays = 1;
                                DaysPay = 0;
                                ABPR = "S";                               
                            }
                            else if (TpaHrs < 8 && LeaveTyp.Contains("WO"))
                            {
                                DaysPay = 0;
                                WoDays = 0;
                                ABPR = "S";                               
                            }
                            else if (LeaveTyp.Contains("AB"))
                            {
                                ABPR = "A";
                                DaysPay = 0;
                                WoDays = 0;
                                TpaHrs = 0;                               
                            }
                            else if (LeaveTyp.Contains("OD"))
                            {
                                ABPR = "P";
                                DaysPay = 1;
                                WoDays = 0;                               
                            }
                            else if (LeaveTyp.Contains("AL"))
                            {
                                ABPR = "P";
                                TpaHrs = 0;
                                DaysPay = 1;
                                WoDays = 0;
                            }                            
                            else if (adr["Status"].ToString() == "P")
                            {
                                DaysPay = 1;
                                ABPR = "P";
                                WoDays = 0;

                                if(WrkHrs >= 4 && WrkHrs < 8)
                                {
                                    DaysPay = 0.5;
                                    ABPR  = "P2";
                                    TpaHrs = 0;
                                }

                                if (WrkHrs < 4)
                                {
                                    DaysPay = 0;
                                    ABPR = "A";
                                    TpaHrs = 0;
                                }
                            }
                            else if(adr["Status"].ToString() == "A")
                            {
                                DaysPay = 0;
                                WoDays = 0;
                                ABPR = "A";
                                TpaHrs = 0;
                            }

                            if (TpaHrs > 0)
                                TpaHrs = Math.Truncate(TpaHrs);
                            
                            sql = "select srno,cBasic from [Cont_MastBasic] " +
                                    " where PayPeriod = '" + pPay.ToString() + "' and EmpUnqID = '" + adr["EmpUnqID"].ToString() + "' " +
                                    " and Convert(date,'" + Convert.ToDateTime(adr["tDate"]).ToString("yyyy-MM-dd") + "',121) " +
                                    " between fromdt and ToDt ";

                            DataSet BasicDs = Utils.Helper.GetData(sql, Utils.Helper.constr);
                            hasRows = BasicDs.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                            if (hasRows)
                            {
                                BasicSr = Convert.ToInt32(BasicDs.Tables[0].Rows[0]["SrNo"]);
                                cBasic = Convert.ToDouble(BasicDs.Tables[0].Rows[0]["cBasic"]);
                            }

                            //*** BA Allow. ****//
                            sql = "select srno,cBAAmt from [Cont_MastBAAll] " +
                                    " where PayPeriod = '" + pPay.ToString() + "' and EmpUnqID = '" + adr["EmpUnqID"].ToString() + "' " +
                                    " and Convert(date,'" + Convert.ToDateTime(adr["tDate"]).ToString("yyyy-MM-dd") + "',121) " +
                                    " between fromdt and ToDt ";

                            DataSet BADs = Utils.Helper.GetData(sql, Utils.Helper.constr);
                            hasRows = BADs.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                            if (hasRows)
                            {
                                BASr = Convert.ToInt32(BADs.Tables[0].Rows[0]["SrNo"]);
                                cBAAll = Convert.ToDouble(BADs.Tables[0].Rows[0]["cBAAmt"]);
                            }

                            //*** BA Allow. ****//

                            //*** SPL Allow. ****//
                            sql = "select srno,cSPLAmt from [Cont_MastSPLAll] " +
                                    " where PayPeriod = '" + pPay.ToString() + "' and EmpUnqID = '" + adr["EmpUnqID"].ToString() + "' " +
                                    " and Convert(date,'" + Convert.ToDateTime(adr["tDate"]).ToString("yyyy-MM-dd") + "',121) " +
                                    " between fromdt and ToDt ";

                            DataSet SPLDs = Utils.Helper.GetData(sql, Utils.Helper.constr);
                            hasRows = SPLDs.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
                            if (hasRows)
                            {
                                SPLSr = Convert.ToInt32(SPLDs.Tables[0].Rows[0]["SrNo"]);
                                cSPLAll = Convert.ToDouble(SPLDs.Tables[0].Rows[0]["cSPLAmt"]);
                            }
                            //*** SPL Allow. ****//

                            
                            
                            //insert blank record into mthlyatn if not exist
                            sql = "Select Count(*) from  Cont_MthlyAtn Where PayPeriod = '" + pPay.ToString() + "' and EmpUnqID = '" + adr["EmpUnqID"].ToString() + "' and SrNo = '" + BasicSr.ToString() + "'";
                            int atncnt = Convert.ToInt32(Utils.Helper.GetDescription(sql, Utils.Helper.constr));
                            

                            if (TpaHrs > 0)
                            {
                                TpaAmt = TpaHrs * (cBasic / 8);
                            }

                            if (WoDays > 0)
                            {
                                CoWoAmt = CoCommRate;
                                CalBasic = 0;
                                CoCommAmt = 0;
                                
                            }else if (DaysPay > 0)
                            {
                                CalBasic = (DaysPay * cBasic);
                                CoCommAmt = (DaysPay * CoCommRate);
                                CoWoAmt = 0;
                            }
                            else
                            {
                                CalBasic = 0;
                                CoCommAmt = 0;
                                CoWoAmt = 0;
                            }

                            if ( DaysPay > 0)
                            {
                                CalBAAll = (cBAAll / 26) * (DaysPay);
                                CalSPLAll = (cSPLAll / 26) * (DaysPay);
                            }


                            #endregion
                            SqlTransaction tr = cn.BeginTransaction();
                            try
                            {
                                sql = "Delete From Cont_DailyOth Where PayPeriod ='" + pPay.ToString() + "' " +
                                        " And EmpUnqID ='" + adr["EmpUnqID"].ToString() + "' and tDate ='" + tDate.ToString("yyyy-MM-dd") + "'";
                                 
                                SqlCommand cmd = new SqlCommand(sql, cn, tr);
                                cmd.ExecuteNonQuery();

                                if(string.IsNullOrEmpty(CostCode.Trim()))
                                {
                                    CostCode = dr["CostCode"].ToString();
                                }

                                sql = "Insert into Cont_DailyOth (PayPeriod,EmpUnqID,tDate,SrNo,LeaveTyp,ABPR,WrkHrs,TpaHrs,CBasic," +
                                      " Cal_Basic,WoDays,Dayspay,tpaAmt,CostCode,CoCommRate,CoCommAmt,CoCommWoRate,CoCommWOAmt,AddDt,AddId,Cal_SplAmt,CAL_BAAmt,ContCode) Values " +
                                      " ('" + pPay.ToString() + "','" + adr["EmpUnqID"].ToString() + "','" + tDate.ToString("yyyy-MM-dd") + "'," +
                                      " '" + BasicSr.ToString() + "','" + LeaveTyp + "','" + ABPR + "','" + WrkHrs.ToString() + "','" + TpaHrs.ToString() + "'," +
                                      " '" + cBasic.ToString() + "','" + CalBasic.ToString() + "','" + WoDays.ToString() + "','" + DaysPay.ToString() + "','" + TpaAmt.ToString() + "'," +
                                      " '" + CostCode + "','" + CoCommRate.ToString() + "','" + CoCommAmt.ToString() + "','" + CoCommRate.ToString() + "','" + CoWoAmt.ToString() + "',GetDate()," +
                                      " '" + Utils.User.GUserID + "','" + CalSPLAll.ToString() + "','" + CalBAAll.ToString() + "','" + dr["ContCode"].ToString() + "')";

                                cmd = new SqlCommand(sql, cn, tr);
                                cmd.ExecuteNonQuery();

                                if (atncnt == 0)
                                {
                                    sql = "Insert into Cont_MthlyAtn (PayPeriod,EmpUnqID,SrNo,AddDt,Addid) Values ('" + pPay.ToString() + "','" + adr["EmpUnqID"].ToString() + "','" + BasicSr.ToString() + "',GetDate(),'" + Utils.User.GUserID + "')";
                                    cmd = new SqlCommand(sql, cn, tr);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    sql = "Update Cont_MthlyAtn Set  " +
                                        " Cal_Basic = 0 " +
                                        " ,Cal_DaysPay = 0 " +
                                        " ,Cal_WODays = 0 " +
                                        " ,Cal_TpaHrs = 0 " +
                                        " ,Cal_TpaAmt = 0 " +
                                        " ,Adj_SPLRate = 0 " +
                                        " ,Adj_SPLDaysPay = 0 " +
                                        " ,Adj_BARate = 0 " +
                                        " ,Adj_BADaysPay = 0 " +
                                        " ,Cal_SPLAmt = 0 " +
                                        " ,Cal_BAAmt = 0 " +
                                        " ,Tot_SPLAmt = 0 " +
                                        " ,Tot_BAAmt = 0 " +
                                        " ,Tot_DaysPay = 0 " +
                                        " ,Tot_EarnBasic = 0 " +
                                        " ,Tot_TpaHrs = 0 " +
                                        " ,Tot_TpaAmt = 0 " +
                                        " ,Tot_Earnings = 0 " +
                                        " ,Cal_PF = 0 " +
                                        " ,Cal_EPF = 0 " +
                                        " ,Cal_EPS = 0 " +
                                        " ,CoCommRate = 0 " +
                                        " ,Adj_CoCommDays = 0 " +
                                        " ,Cal_CoCommDays = 0 " +
                                        " ,Cal_CoWoCommDays = 0 " +
                                        " ,Tot_CoCommDays = 0 " +
                                        " ,Adj_CoCommAmt = 0 " +
                                        " ,Cal_CoCommAmt = 0 " +
                                        " ,Cal_CoCommWoAmt = 0 " +
                                        " ,Tot_CoCommAmt = 0 " +
                                        " ,Cal_CoCommPFAmt = 0 " +
                                        " ,Cal_CoServTaxAmt = 0 " +
                                        " ,Cal_CoEduTaxAmt = 0 " + 
                                        " Where PayPeriod ='" + pPay.ToString() + "' and EmpUnqID = '" + adr["EmpUnqID"].ToString() + "' and SrNo = '" + BasicSr.ToString() + "'";
                                    
                                    cmd = new SqlCommand(sql, cn, tr);
                                    cmd.ExecuteNonQuery();
                                }
                               
                                


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
                                tr.Dispose();
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
            btnImport.Enabled = false;
            txtEmpUnqID.Enabled = false;
            txtContCode.Enabled = false;

        }

        private void unLockCtrl()
        {
            txtPayPeriod.Enabled = true;
            txtPayDesc.Enabled = true;
            btnImport.Enabled = true;
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
                    btnImport.Enabled = true;
                    mode = "OLD";
                }
            }
            else
            {
                btnImport.Enabled = false;
                mode = "NEW";
                pFromDt = DateTime.MinValue;
                pToDt = DateTime.MinValue;

            }           

            SetRights();
        }

        private void frmImportAttd_Load(object sender, EventArgs e)
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
