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
            
            string sql = "Select a.*,b.LWFFlg,b.DeathFlg,b.PTaxFlg,b.ESIFlg From Cont_MthlyAtn a,Cont_MastEmp b where a.PayPeriod = b.PayPeriod and a.EmpUnqID = b.EmpUnqID and a.PayPeriod ='" + txtPayPeriod.Text.Trim() + "'";

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


                foreach (DataRow dr in emplistds.Tables[0].Rows)
                {
                    SqlTransaction tr = cn.BeginTransaction();
                    Application.DoEvents();

                    sql = " SELECT [PayPeriod] " +
                          " ,[EmpUnqID] " +
                          " ,sum([Adj_TpaHrs]) as Adj_TpaHrs " +
                          " ,sum([Adj_TpaAmt]) as Adj_TpaAmt " +
                          " ,sum([Adj_DaysPay]) as Adj_DaysPay " +
                          " ,sum([Adj_DaysPayAmt]) as Adj_DaysPayAmt " +
                          " ,sum([Adj_Amt]) as Adj_Amt " +
                          " ,sum([Cal_Basic]) as Cal_Basic " +
                          " ,sum([Cal_DaysPay]) as Cal_DaysPay " +
                          " ,sum([Cal_WODays]) as Cal_WoDays " +
                          " ,sum([Cal_TpaHrs]) as Cal_TpaHrs " +
                          " ,sum([Cal_TpaAmt]) as Cal_TpaAmt " +
                          " ,(sum([Cal_DaysPay]) + sum([Adj_DaysPay])) as Tot_DaysPay " +
                          " ,(sum([Adj_DaysPayAmt]) + sum([Cal_Basic]) + sum([Adj_Amt])) as Tot_EarnBasic " +
                          " ,(sum([Adj_TpaHrs]) + sum([Cal_TpaHrs])) as Tot_TpaHrs " +
                          " ,(sum([Adj_TpaAmt]) + sum([Cal_TpaAmt])) as Tot_TpaAmt " +
                          " ,(sum([Adj_DaysPayAmt]) + sum([Cal_Basic]) + sum([Adj_TpaAmt]) + sum([Cal_TpaAmt]) + sum([Adj_Amt])  ) as Tot_Earnings " +
                          " ,sum([Cal_PF]) as Ded_PF " +
                          " ,sum([Cal_EPF]) as Cal_EPF " +
                          " ,sum([Cal_EPS]) as Cal_EPS " +
                          " ,sum([Cal_CoCommDays])  as Tot_CoCommDays " +
                          " ,sum([Cal_CoCommAmt]) + sum([Cal_CoCommWoAmt]) + sum([Adj_CoCommAmt]) as Tot_CoCommAmt " +
                          " ,sum([Cal_CoCommPFAmt]) as Tot_CoCommPFAmt " +
                          " ,sum([Tot_CoCommAmt]) as Tot_CoComm " +
                          " ,sum([Cal_CoServTaxAmt]) as Tot_CoServTax " +
                          " ,sum([Cal_CoEduTaxAmt]) as Tot_CoEduTax " + 
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
                            double Tot_Earnning = 0;
                            double Tot_EarnedBasic = 0;
                            double PF = 0;
                            double NetPay = 0;
                            double Tot_Ded = 0;

                            PF = Convert.ToDouble(mdr["Ded_PF"]);
                            Tot_EarnedBasic = Convert.ToDouble(mdr["Tot_EarnBasic"]);
                            Tot_Earnning = Convert.ToDouble(mdr["Tot_Earnings"]);
                           
                            
                            if (Tot_EarnedBasic > 0 )
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
                                     " where '" + Tot_Earnning + "' between FSlab and TSlab " +
                                     " and ParaCode = 'ESI'" +
                                     " and PayPeriod = '" + dr["PayPeriod"].ToString() + "'" +
                                     " and AppFlg = 1";

                                EsiRate = Convert.ToDouble(Utils.Helper.GetDescription(sql, Utils.Helper.constr));
                                if(EsiRate > 0)
                                {
                                    ESI = (Tot_Earnning * EsiRate / 100);
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

                            Tot_Ded = LWF + PTax + Death + OtherDed + MessDed + ESI;
                            NetPay = Tot_Earnning - Tot_Ded;
                            
                            #endregion

                            string delsql = "Delete from Cont_MthlyPay Where PayPeriod = '" + dr["PayPeriod"].ToString() + "' And EmpUnqID = '" + dr["EmpUnqID"].ToString() + "'";
                            SqlCommand cmd = new SqlCommand(delsql, cn, tr);
                            cmd.ExecuteNonQuery();

                            sql = "Insert into Cont_MthlyPay (PayPeriod,EmpUnqID," +
                                " Adj_TPAHrs,Adj_TPAAmt,Adj_DaysPay,Adj_DaysPayAmt,Adj_Amt, " +
                                " Cal_Basic,Cal_DaysPay,Cal_WODays,Cal_TpaHrs,Cal_TpaAmt,Tot_DaysPay, " +
                                " Tot_EarnBasic,Tot_TpaHrs,Tot_TpaAmt,Tot_Earnings,Ded_PF,Cal_EPF,Cal_EPS," +
                                " Ded_ESI,Ded_LWF,Ded_DeathFund,Ded_Other,Ded_Mess,Ded_PTax,Tot_Ded,NetPay," +
                                " Tot_CoCommDays,Tot_CoCommAmt,Tot_CoCommPFAmt,Tot_CoComm,Tot_CoServTax,Tot_CoEduTax,AddDt,AddID) Values (" +
                                "'" + mdr["PayPeriod"].ToString() + "'," +
                                "'" + mdr["EmpUnqID"].ToString() + "'," +
                                "'" + mdr["Adj_TPAHrs"].ToString() + "'," +
                                "'" + mdr["Adj_TPAAmt"].ToString() + "'," +
                                "'" + mdr["Adj_DaysPay"].ToString() + "'," +
                                "'" + mdr["Adj_DaysPayAmt"].ToString() + "'," +
                                "'" + mdr["Adj_Amt"].ToString() + "'," +
                                "'" + mdr["Cal_Basic"].ToString() + "'," +
                                "'" + mdr["Cal_DaysPay"].ToString() + "'," +
                                "'" + mdr["Cal_WODays"].ToString() + "'," +
                                "'" + mdr["Cal_TpaHrs"].ToString() + "'," +
                                "'" + mdr["Cal_TpaAmt"].ToString() + "'," +
                                "'" + mdr["Tot_DaysPay"].ToString() + "'," +
                                "'" + mdr["Tot_EarnBasic"].ToString() + "'," +
                                "'" + mdr["Tot_TpaHrs"].ToString() + "'," +
                                "'" + mdr["Tot_TpaAmt"].ToString() + "'," +
                                "'" + mdr["Tot_Earnings"].ToString() + "'," +
                                "'" + mdr["Ded_PF"].ToString() + "'," +
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
                                "'" + mdr["Tot_CoCommDays"].ToString() + "'," +
                                "'" + mdr["Tot_CoCommAmt"].ToString() + "'," +
                                "'" + mdr["Tot_CoCommPFAmt"].ToString() + "'," +
                                "'" + mdr["Tot_CoComm"].ToString() + "'," +
                                "'" + mdr["Tot_CoServTax"].ToString() + "'," +
                                "'" + mdr["Tot_CoEduTax"].ToString() + "',GetDate(),'" + Utils.User.GUserID + "')";

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
        }

        private void unLockCtrl()
        {
            txtPayPeriod.Enabled = true;
            txtPayDesc.Enabled = true;
            btnProcess.Enabled = true;
            grd_view.Enabled = true;
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

                sql = "select PayPeriod, ParaCode as DedCode, PValue as Amount,Convert(Bit,BCFlg) as BCFlg From Cont_ParaMast where  PayPeriod='" + txtPayPeriod.Text.Trim() + "' and BCFlg = 1 and AppFlg = 1";

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

    }
}
