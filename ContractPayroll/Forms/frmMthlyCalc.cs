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
    public partial class frmMthlyCalc : Form
    {
        public string GRights = "XXXV";
        public string mode = "NEW";
        public bool IsLocked = false;
        public DataSet optDed;
        public DataSet AppDed;


        public frmMthlyCalc()
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
            DateTime pFromDt ;
            DateTime pToDt;

            DataSet payds = Utils.Helper.GetData("Select * from Cont_MastPayPeriod where PayPeriod ='" + txtPayPeriod.Text.Trim() + "' ",Utils.Helper.constr);
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
                sql = "Select a.*,b.LWFFlg,b.DeathFlg,b.PTaxFlg,b.ESIFlg,b.Active From Cont_MthlyAtn a,Cont_MastEmp b where a.PayPeriod = b.PayPeriod and a.EmpUnqID = b.EmpUnqID and a.PayPeriod ='" + txtPayPeriod.Text.Trim() + "'";

            }
            else if(string.IsNullOrEmpty(txtContCode.Text.Trim()) && 
                !string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()))
            {
                 sql = "Select a.*,b.LWFFlg,b.DeathFlg,b.PTaxFlg,b.ESIFlg,b.Active From Cont_MthlyAtn a,Cont_MastEmp b where a.PayPeriod = b.PayPeriod and a.EmpUnqID = b.EmpUnqID and a.PayPeriod ='" + txtPayPeriod.Text.Trim() + "' and a.EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";

            }
            else if(!string.IsNullOrEmpty(txtContCode.Text.Trim()) && 
                string.IsNullOrEmpty(txtEmpUnqID.Text.Trim()))
            {
                sql = "Select a.*,b.LWFFlg,b.DeathFlg,b.PTaxFlg,b.ESIFlg,b.Active From Cont_MthlyAtn a,Cont_MastEmp b where a.PayPeriod = b.PayPeriod and a.EmpUnqID = b.EmpUnqID and a.PayPeriod ='" + txtPayPeriod.Text.Trim() + "' and a.ContCode = '" + txtContCode.Text.Trim() + "'";

            }
                
               
            DataSet emplistds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            hasRows = emplistds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            pBar.Value = 0;
            if (!hasRows)
            {
                MessageBox.Show("No Monthly Attendance Found, please process monthly attendance first...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                //get a list of current 
                DataTable HrlyView = optDed.Tables["RESULT"].Copy();

                double LWF = 0;               
                double Death = 0;


                foreach (DataRow ddr in optDed.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(ddr["BCFlg"]))
                    {
                        switch (ddr["DedCode"].ToString().ToUpper())
                        {
                            case "LWF":
                                LWF = Convert.ToDouble(ddr["Amount"]);
                                break;
                            case "DEATH" :
                                Death = Convert.ToDouble(ddr["Amount"]);
                                break;

                            default:
                                break;
                        }
                    }
                }

                //Get Daily Co-commision Rate
                sql = "Select PValue from Cont_ParaMast Where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' And ParaCode = 'COCOMM'";

                string strCoCommRate = Utils.Helper.GetDescription(sql, Utils.Helper.constr);
                double CoCommRate = 0;
                if (!string.IsNullOrEmpty(strCoCommRate))
                {
                    double.TryParse(strCoCommRate, out CoCommRate);
                }

                //Get Co-PF Rate
                sql = "Select PValue from Cont_ParaMast Where PayPeriod = '" + txtPayPeriod.Text.Trim() + "' and ParaCode = 'COPFRATE'";
                string strCoPFRate = Utils.Helper.GetDescription(sql, Utils.Helper.constr);
                double CoPFRate = 0;
                if (!string.IsNullOrEmpty(strCoPFRate))
                {
                    double.TryParse(strCoPFRate, out CoPFRate);
                }

                foreach (DataRow dr in emplistds.Tables[0].Rows)
                {
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
                    
                    
                    SqlTransaction tr = cn.BeginTransaction();
                    Application.DoEvents();

                    sql = " SELECT [PayPeriod] " +
                          " ,[EmpUnqID] " +
                          " ,Max([CostCode]) as CostCode " +
                          " ,sum([Adj_TpaHrs]) as Adj_TpaHrs " +
                          " ,sum([Adj_TpaAmt]) as Adj_TpaAmt " +
                          " ,sum([Adj_DaysPay]) as Adj_DaysPay " +
                          " ,sum([Adj_DaysPayAmt]) as Adj_DaysPayAmt " +
                          " ,sum([Adj_Amt]) as Adj_Amt " +
                          " ,sum([Adj_SPLAmt]) as Adj_SPLAmt " +
                          " ,sum([Adj_BAAmt]) as Adj_BAAmt " +
                          " ,sum([Cal_Basic]) as Cal_Basic " +
                          " ,sum([Cal_DaysPay]) as Cal_DaysPay " +
                          " ,sum([Cal_WODays]) as Cal_WoDays " +
                          " ,sum([Cal_TpaHrs]) as Cal_TpaHrs " +
                          " ,sum([Cal_TpaAmt]) as Cal_TpaAmt " +
                          
                          " ,sum([Adj_SPLDaysPay]) as Adj_SPLDaysPay " +
                          " ,sum([Adj_BADaysPay]) as Adj_BADaysPay " +                          
                          " ,sum([Cal_SPLAmt]) as Cal_SPLAmt " +
                          " ,sum([Cal_BAAmt]) as Cal_BAAmt " +
                          " ,sum([Adj_SPLAmt]) as Adj_SPLAmt " +
                          " ,sum([Adj_BAAmt]) as Adj_BAAmt " +
                          " ,sum([Tot_SPLAmt]) as Tot_SPLAmt " +
                          " ,sum([Tot_BAAmt]) as Tot_BAAmt " +                          
                          " ,(sum([Cal_DaysPay]) + sum([Adj_DaysPay])) as Tot_DaysPay " +
                          " ,(sum([Adj_DaysPayAmt]) + sum([Cal_Basic]) ) as Tot_EarnBasic " +
                          " ,(sum([Adj_TpaHrs]) + sum([Cal_TpaHrs])) as Tot_TpaHrs " +
                          " ,(sum([Adj_TpaAmt]) + sum([Cal_TpaAmt])) as Tot_TpaAmt " +
                          " ,(sum([Adj_DaysPayAmt]) + sum([Cal_Basic])  + sum([Adj_Amt]) + sum([Tot_SPLAmt]) + sum([Tot_BAAmt]) ) as Tot_Earnings " +
                          " ,sum([Cal_PF]) as Ded_PF " +
                          " ,sum([Cal_EPF]) as Cal_EPF " +
                          " ,sum([Cal_EPS]) as Cal_EPS " +
                          " ,sum([Cal_CoCommDays])  as Tot_CoCommDays " +
                          " ,sum([Cal_CoCommAmt]) + sum([Adj_CoCommAmt]) as Tot_CoCommAmt " +
                          " ,sum([Cal_CoCommPFAmt]) as Tot_CoCommPFAmt " +
                          " ,sum([Tot_CoCommAmt]) as Tot_CoComm " +
                          " ,sum([Cal_CoServTaxAmt]) as Tot_CoServTax " +
                          " ,sum([Cal_CoEduTaxAmt]) as Tot_CoEduTax " + 
                          " ,Sum([Cal_CoServTax25Amt]) as Tot_CoServTax25 " +
                          " ,Sum([Cal_CoEduTax25Amt]) as Tot_CoEduTax25 " +
                          " FROM [Cont_MthlyAtn]  " +
                          " group by PayPeriod,EmpUnqID " +
                          " having PayPeriod = '" + dr["PayPeriod"].ToString() + "' " +
                    " and EmpUnqID = '" + dr["EmpUnqID"].ToString() + "'";

                    DataSet MthlyDs = Utils.Helper.GetData(sql, Utils.Helper.constr);


                    try
                    {

                        foreach (DataRow mdr in MthlyDs.Tables[0].Rows)
                        {

                            #region Cal_Ded

                            double ESI = 0;
                            double OtherDed = 0;
                            double MessDed = 0;
                            double PTax = 0;
                            double Tot_Ded = 0;
                            double Adj_DaysPay = Convert.ToDouble(mdr["Adj_DaysPay"]);
                            double Adj_Amt = Convert.ToDouble(mdr["Adj_Amt"]);
                            double Adj_SPLDaysPay = Convert.ToDouble(mdr["Adj_SPLDaysPay"]);
                            double Adj_BADaysPay = Convert.ToDouble(mdr["Adj_BADaysPay"]);
                            double Adj_SPLAmt = Convert.ToDouble(mdr["Adj_SPLAmt"]);
                            double Adj_BAAmt = Convert.ToDouble(mdr["Adj_BAAmt"]);
                            double CAL_SPLAmt = Convert.ToDouble(mdr["Cal_SPLAmt"]);
                            double CAL_BAAmt = Convert.ToDouble(mdr["Cal_BAAmt"]);
                            double Tot_SPLAmt = Math.Round(Convert.ToDouble(mdr["Tot_SPLAmt"]),2);
                            double Tot_BAAmt = Math.Round(Convert.ToDouble(mdr["Tot_BAAmt"]),2);
                            

                            double Tot_EarnedBasic = 0;
                            double actTot_EarnedBasic = 0;
                            double RoundoffTot_EarnedBasic = 0;

                            double NetPay = 0;
                            double actNetPay = 0;
                            double roundoffNetPay = 0;                            

                            double PF = 0;
                            double actPF = 0;
                            double roundoffPF = 0;

                            //double Cal_OTAmt = Convert.ToDouble(mdr["Cal_TpaAmt"]);
                            double Adj_OTAmt = Math.Round(Convert.ToDouble(mdr["Adj_TpaAmt"]),0);


                            double OTAmt = 0;
                            double actOTAmt = 0;
                            double roundoffOTAmt = 0;

                            double Tot_Earnning = 0;
                            double actTot_Earnings = 0;
                            double RoundoffTot_Earnings = 0;

                            double Tot_CoCommDays = Convert.ToDouble(mdr["Cal_DaysPay"]) + Adj_DaysPay;
                            double Tot_CoWoCommDays = Convert.ToDouble(mdr["Cal_WODays"]);
                            double Tot_CoCommAmt = (Tot_CoCommDays) * CoCommRate;
                            double Tot_CoWoCommAmt = Tot_CoWoCommDays * CoCommRate;
                            double Tot_CoComm = Tot_CoCommAmt + Tot_CoWoCommAmt;
                           

                            actTot_EarnedBasic =  Convert.ToDouble(mdr["Tot_EarnBasic"]);
                            Tot_EarnedBasic = Math.Round(actTot_EarnedBasic, 2);
                            //RoundoffTot_EarnedBasic = Tot_EarnedBasic - actTot_EarnedBasic ;
                            double Tot_CoCommPF =  Math.Round((Tot_EarnedBasic * CoPFRate / 100),MidpointRounding.AwayFromZero);

                            actOTAmt = Convert.ToDouble(mdr["Tot_TpaAmt"]);
                            OTAmt = Math.Round(actOTAmt, MidpointRounding.AwayFromZero);
                            roundoffOTAmt =  OTAmt - actOTAmt;


                            actPF = Convert.ToDouble(mdr["Ded_PF"]);
                            //PF = Math.Round(actPF, MidpointRounding.AwayFromZero);
                            PF = Math.Round(actPF, 0);
                            roundoffPF = PF - actPF;
                            
                            
                            //Tot_Earnning = Convert.ToDouble(mdr["Tot_Earnings"]);
                            actTot_Earnings = Tot_EarnedBasic +  Tot_SPLAmt + Tot_BAAmt ;
                            //Tot_Earnning = Math.Round(actTot_Earnings, MidpointRounding.AwayFromZero);
                            //RoundoffTot_Earnings = Tot_Earnning - actTot_Earnings;
                            Tot_Earnning = actTot_Earnings;

                            if (Tot_Earnning > 0)
                            {
                                sql = "select isnull(Max(PValue),0) FROM [Cont_ParaMast] " +
                                     " where '" + Tot_Earnning + "' between FSlab and TSlab " +
                                     " and ParaCode = 'PTAX'" +
                                     " and PayPeriod = '" + dr["PayPeriod"].ToString() + "'" +
                                     " and AppFlg = 1";
                                PTax = Convert.ToDouble(Utils.Helper.GetDescription(sql, Utils.Helper.constr));
                            }

                            if (Tot_Earnning > 0)
                            {
                                double EsiRate = 0;
                                sql = "select isnull(Max(PValue),0) FROM [Cont_ParaMast] " +
                                     " where ParaCode = 'ESI'" +
                                     " and PayPeriod = '" + dr["PayPeriod"].ToString() + "'" +
                                     " and AppFlg = 1";

                                EsiRate = Convert.ToDouble(Utils.Helper.GetDescription(sql, Utils.Helper.constr));
                                if(EsiRate > 0)
                                {
                                    ESI = (Tot_EarnedBasic * EsiRate / 100);
                                }
                            }
                            else
                            {
                                LWF = 0;
                                Death = 0;
                            }


                            if (!Convert.ToBoolean(dr["LWFFlg"]))
                            {
                                LWF = 0;
                            }

                            if (!Convert.ToBoolean(dr["DeathFlg"]))
                            {
                                Death = 0;
                            }

                            if (!Convert.ToBoolean(dr["PTaxFlg"]))
                            {
                                PTax = 0;
                            }

                            if (!Convert.ToBoolean(dr["ESIFlg"]))
                            {
                                ESI = 0;
                            }
                           


                            sql = "SELECT isnull(Max([Amount]),0) FROM [Cont_MthlyDed] where " +
                                " EmpUnqID = '" + dr["EmpUnqID"].ToString()  +"' and PayPeriod = '" + dr["PayPeriod"].ToString() + "' and DedCode = 'MESS'" ;
                            MessDed = Convert.ToDouble(Utils.Helper.GetDescription(sql, Utils.Helper.constr));

                            sql = "SELECT isnull(Max([Amount]),0) FROM [Cont_MthlyDed] where " +
                                " EmpUnqID = '" + dr["EmpUnqID"].ToString() + "' and PayPeriod = '" + dr["PayPeriod"].ToString() + "' and DedCode = 'MISC'";
                            OtherDed = Convert.ToDouble(Utils.Helper.GetDescription(sql, Utils.Helper.constr));

                            Tot_Ded = PF + LWF + PTax + Death + OtherDed + MessDed + ESI;
                            
                            actNetPay = (Tot_EarnedBasic + Tot_SPLAmt + Tot_BAAmt) - Tot_Ded;
                            NetPay = Math.Round(actNetPay, MidpointRounding.AwayFromZero);
                            roundoffNetPay = Math.Round((Tot_EarnedBasic + Tot_SPLAmt + Tot_BAAmt) - (Tot_Ded + NetPay),2);




                            #endregion

                            string delsql = "Delete from Cont_MthlyPay Where PayPeriod = '" + dr["PayPeriod"].ToString() + "' And EmpUnqID = '" + dr["EmpUnqID"].ToString() + "'";
                            SqlCommand cmd = new SqlCommand(delsql, cn, tr);
                            cmd.ExecuteNonQuery();

                            sql = "Insert into Cont_MthlyPay (PayPeriod,EmpUnqID,CostCode," +
                                " Adj_TPAHrs,Adj_TPAAmt,Adj_DaysPay,Adj_DaysPayAmt,Adj_Amt, " +
                                " Cal_Basic,Cal_DaysPay,Cal_WODays,Cal_TpaHrs,Cal_TpaAmt,Tot_DaysPay, " +
                                " Tot_EarnBasic,Tot_EarnBasicRoundOff,Tot_TpaHrs,Tot_TpaAmt,Tot_TpaRoundoff,Tot_Earnings,Tot_EarningsRoundoff,Ded_PF,Ded_PF_Roundoff,Cal_EPF,Cal_EPS," +
                                " Ded_ESI,Ded_LWF,Ded_DeathFund,Ded_Other,Ded_Mess,Ded_PTax,Tot_Ded,NetPay,NetPay_RoundOff," +
                                " Tot_CoCommDays,Tot_CoCommAmt, Tot_CoWoCommDays,Tot_CoWoCommAmt,Tot_CoCommPFAmt,Tot_CoComm,Tot_CoServTax,Tot_CoEduTax,Tot_CoServTax25,Tot_CoEduTax25,AddDt,AddID," +
                                " Adj_SPLDaysPay,Adj_SPLAmt,Adj_BADaysPay,Adj_BAAmt,Cal_SplAmt,Cal_BAAmt,Tot_SPLAmt,Tot_BAAmt,ContCode) Values (" +
                                "'" + mdr["PayPeriod"].ToString() + "'," +
                                "'" + mdr["EmpUnqID"].ToString() + "'," +
                                "'" + mdr["CostCode"].ToString() + "'," +
                                "'" + mdr["Adj_TPAHrs"].ToString() + "'," +
                                "'" + Adj_OTAmt.ToString() + "'," +
                                "'" + mdr["Adj_DaysPay"].ToString() + "'," +
                                "'" + mdr["Adj_DaysPayAmt"].ToString() + "'," +
                                "'" + mdr["Adj_Amt"].ToString() + "'," +
                                "'" + mdr["Cal_Basic"].ToString() + "'," +
                                "'" + mdr["Cal_DaysPay"].ToString() + "'," +
                                "'" + mdr["Cal_WODays"].ToString() + "'," +
                                "'" + mdr["Cal_TpaHrs"].ToString() + "'," +
                                "'" + mdr["Cal_TpaAmt"].ToString() + "'," +
                                "'" + mdr["Tot_DaysPay"].ToString() + "'," +
                                "'" + Tot_EarnedBasic.ToString() + "'," +
                                "'" + RoundoffTot_EarnedBasic.ToString() + "'," +
                                "'" + mdr["Tot_TpaHrs"].ToString() + "'," +
                                "'" + OTAmt.ToString() + "'," +
                                "'" + roundoffOTAmt.ToString() + "'," +
                                "'" + Tot_Earnning.ToString() + "'," +
                                "'" + RoundoffTot_Earnings.ToString() + "'," +
                                "'" + PF.ToString() + "'," +
                                "'" + roundoffPF.ToString() + "'," +
                                "'" + mdr["Cal_EPF"].ToString() + "'," +
                                "'" + mdr["Cal_EPS"].ToString() + "'," +
                                "'" + ESI.ToString() + "'," +
                                "'" + LWF.ToString() + "'," +
                                "'" + Death.ToString() + "'," +
                                "'" + OtherDed.ToString() + "'," +
                                "'" + MessDed.ToString() + "'," +
                                "'" + PTax.ToString() + "'," +
                                "'" + Tot_Ded.ToString() + "'," +
                                "'" + NetPay.ToString() + "'," +
                                "'" + roundoffNetPay.ToString() + "'," +
                                "'" + Tot_CoCommDays.ToString() + "'," +
                                "'" + Tot_CoCommAmt.ToString() + "'," +
                                "'" + Tot_CoWoCommDays.ToString() + "'," +
                                "'" + Tot_CoWoCommAmt.ToString() + "'," +
                                "'" + Tot_CoCommPF.ToString() + "'," +
                                "'" + Tot_CoComm.ToString() + "'," +
                                "'" + mdr["Tot_CoServTax"].ToString() + "'," +
                                "'" + mdr["Tot_CoEduTax"].ToString() + "', " +
                                "'" + mdr["Tot_CoServTax25"].ToString() + "', " +
                                "'" + mdr["Tot_CoEduTax25"].ToString() + "', GetDate(),'" + Utils.User.GUserID + "'," +
                                "'" + Adj_SPLDaysPay.ToString() + "'," +
                                "'" + Adj_SPLAmt.ToString() + "'," +
                                "'" + Adj_BADaysPay.ToString() + "'," +
                                "'" + Adj_BAAmt.ToString()  + "'," +
                                "'" + CAL_SPLAmt.ToString() + "'," +
                                "'" + CAL_BAAmt.ToString() + "'," +
                                "'" + Tot_SPLAmt.ToString() +"'," +
                                "'" + Tot_BAAmt.ToString() +"'," +
                                "'" + dr["ContCode"].ToString() + "' " +
                                ")";

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
                        }

                    }
                    catch (Exception ex)
                    {
                        tr.Dispose();
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    pBar.Value += 1;
                    pBar.Update();


                }//for each loop


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
            grd_view.Enabled = false;
            txtEmpUnqID.Enabled = false;
            txtContCode.Enabled = false;

        }

        private void unLockCtrl()
        {
            txtPayPeriod.Enabled = true;
            txtPayDesc.Enabled = true;
            btnProcess.Enabled = true;
            grd_view.Enabled = true;
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
                    grd_view.DataSource = null;
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "0")
                {
                    txtPayPeriod.Text = "";
                    txtPayDesc.Text = "";
                    grd_view.DataSource = null;
                    return;
                }
                else if (obj.ElementAt(0).ToString() == "")
                {
                    txtPayPeriod.Text = "";
                    txtPayDesc.Text = "";
                    grd_view.DataSource = null;
                    return;
                }
                else
                {

                    txtPayPeriod.Text = obj.ElementAt(0).ToString();
                    txtPayDesc.Text = obj.ElementAt(1).ToString();
                    txtPayPeriod_Validated(sender, e);
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
                    btnProcess.Enabled = true;
                    mode = "OLD";
                }

                sql = "select PayPeriod, ParaCode as DedCode, PValue as Amount,Convert(Bit,'FALSE') as BCFlg From Cont_ParaMast where  PayPeriod='" + txtPayPeriod.Text.Trim() + "' and BCFlg = 1 and AppFlg = 1";

                optDed = Utils.Helper.GetData(sql, Utils.Helper.constr);
                grd_view.DataSource = optDed;
                grd_view.DataMember = ds.Tables[0].TableName;

                sql = "Select * FRom Cont_ParaMast Where PayPeriod='" + txtPayPeriod.Text.Trim() + "' and BCFlg = 0 and AppFlg = 1";
                AppDed = Utils.Helper.GetData(sql, Utils.Helper.constr);

            }
            else
            {
                optDed = new DataSet();
                AppDed = new DataSet();
                btnProcess.Enabled = false;
                mode = "NEW";
                grd_view.DataSource = null;

            }           

            SetRights();
        }

        private void frmMthlyCalc_Load(object sender, EventArgs e)
        {
            ResetCtrl();
        }

        private void grd_view_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpUnqID_Validated(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select EmpName From Cont_MastEmp where  PayPeriod = '" + txtPayPeriod.Text.Trim().ToString() + " and EmpUnqID = '" + txtEmpUnqID.Text.Trim() + "'";

            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtContCode.Text = "";
                    txtContName.Text = "";
                    txtEmpName.Text = dr["EmpName"].ToString();
                    
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


                sql = "Select Distinct ContCode,ContDesc from Cont_MastEmp Where PayPeriod ='" + txtPayPeriod.Text.Trim().ToString() + "'";


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
                txtContCode.Text = "";
                txtContName.Text = "";
               
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
