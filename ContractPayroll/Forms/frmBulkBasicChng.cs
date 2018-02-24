using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Columns;
using ContractPayroll.Classes;

namespace ContractPayroll.Forms
{
    public partial class frmBulkBasicChng : DevExpress.XtraEditors.XtraForm
    {
        public string GRights = "XXXV";

        DataTable dt = new DataTable();

        public frmBulkBasicChng()
        {
            InitializeComponent();
            
        }

        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openKeywordsFileDialog = new OpenFileDialog();
            openKeywordsFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openKeywordsFileDialog.Multiselect = false;
            openKeywordsFileDialog.ValidateNames = true;
            //openKeywordsFileDialog.CheckFileExists = true;
            openKeywordsFileDialog.DereferenceLinks = false;        //Will return .lnk in shortcuts.
            openKeywordsFileDialog.Filter = "Files|*.xls;*.xlsx;*.xlsb";
            openKeywordsFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(OpenKeywordsFileDialog_FileOk);
            var dialogResult = openKeywordsFileDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                //first check if already exits if found return..
                string filenm = openKeywordsFileDialog.FileName.ToString();
                if (string.IsNullOrEmpty(filenm))
                    return;
                try
                {
                    txtBrowse.Text = openKeywordsFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    txtBrowse.Text = "";
                }
            }
            else
            {
                txtBrowse.Text = "";
            }
        }

        void OpenKeywordsFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenFileDialog fileDialog = sender as OpenFileDialog;
            string selectedFile = fileDialog.FileName;
            if (string.IsNullOrEmpty(selectedFile) || selectedFile.Contains(".lnk"))
            {
                MessageBox.Show("Please select a valid File");
                e.Cancel = true;
            }
            return;
        }

        private string DataValidate(string tEmpUnqID,string tPayPeriod,DateTime tFromDt,DateTime tToDt,double tBasic)
        {
            string err = string.Empty;

            if (string.IsNullOrEmpty(tEmpUnqID))
            {
                err = err + "Invalid EmpUnqID..." + Environment.NewLine;
            }

            if (tBasic <= 0)
            {
                err = err + "Invalid Employee Basic Amount..." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(tPayPeriod))
            {
                err = err + "Invalid PayPeriod..." + Environment.NewLine;
            }


            if (tToDt < tFromDt)
            {
                err = err + "Invalid Date Range.." + Environment.NewLine;
                return err;
            }


            string sql = "Select EmpUnqID From Cont_MastEmp where EmpUnqId = '" + tEmpUnqID + "' " +
                " and PayPeriod =  '" + tPayPeriod + "'";

            string tMaxDt = Utils.Helper.GetDescription(sql, Utils.Helper.constr);

            if (string.IsNullOrEmpty(tMaxDt))
            {
                err = err + "Employee Does not Exist in PayPeriod...." + Environment.NewLine;
                return err;
            }

            sql = "Select count(*) From Cont_MastPayPeriod where PayPeriod = '" + tPayPeriod + "'";
            tMaxDt = Utils.Helper.GetDescription(sql, Utils.Helper.constr);

            if (tMaxDt != "1")
            {
                err = err + "Invalid PayPeriod...." + Environment.NewLine;
                return err;
            }

            sql = "Select case when (select isLocked From Cont_MastPayPeriod where isLocked = 1 and PayPeriod = '" + tPayPeriod + "') = 1 	then 1 else 0 end ";
            tMaxDt = Utils.Helper.GetDescription(sql, Utils.Helper.constr);

            if (tMaxDt == "1")
            {
                err = err + "PayPeriod is locked...." + Environment.NewLine;
                return err;
            }
            
            sql = "Select * From Cont_MastPayPeriod Where PayPeriod ='" + tPayPeriod + "' and isLocked = 0";
            DataSet ds = Utils.Helper.GetData(sql,Utils.Helper.constr);

            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            DateTime pFromDt;
            DateTime pToDt;
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    pFromDt = Convert.ToDateTime(dr["FromDt"]);
                    pToDt = Convert.ToDateTime(dr["ToDt"]);

                    if (tFromDt < pFromDt || tToDt < pFromDt)
                    {
                        err = err + "FromDate/ToDate should not be less than PayPeriod" + Environment.NewLine;
                        return err;
                    }

                    if (tToDt > pToDt || tFromDt > pToDt)
                    {
                        err = err + "FromDate/ToDate should not be grater than PayPeriod" + Environment.NewLine;
                        return err;
                    }


                }
            }
            else
            {
                err += "invalid PayPeriod";
            }


            return err;
        }


        private void btnImport_Click(object sender, EventArgs e)
        {

            

            DataTable dtMaterial = new DataTable();
            DataTable sortedDT = new DataTable();
            try
            {
                

                foreach (GridColumn column in grd_view1.VisibleColumns)
                {
                    if (column.FieldName != string.Empty)
                        dtMaterial.Columns.Add(column.FieldName, column.ColumnType);
                }


                for (int i = 0; i < grd_view1.DataRowCount; i++)
                {
                    DataRow row = dtMaterial.NewRow();

                    foreach (GridColumn column in grd_view1.VisibleColumns)
                    {
                        row[column.FieldName] = grd_view1.GetRowCellValue(i, column);
                    }
                    dtMaterial.Rows.Add(row);
                }

                DataView dv = dtMaterial.DefaultView;
                dv.Sort = "EmpUnqID asc";
                sortedDT = dv.ToTable();

                using (SqlConnection con = new SqlConnection(Utils.Helper.constr))
                {
                    try
                    {
                        con.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    #region deleteallfirst

                    //delete all basic first
                    foreach (DataRow dr in sortedDT.Rows)
                    {
                        string tEmpUnqID = dr["EmpUnqID"].ToString();
                        string tPayPeriod = dr["PayPeriod"].ToString();
                        int tSrno = 0;
                        if (!int.TryParse(dr["SrNo"].ToString(), out tSrno))
                        {
                            dr["Remarks"] = "invalid SrNo";
                            continue;
                        }
                        else
                        {
                            if (tSrno <= 0)
                            {
                                dr["Remarks"] = "invalid SrNo";
                                continue;
                            }
                        }
                        
                        double tBasic = 0;

                        if (!double.TryParse(dr["cBasic"].ToString(), out tBasic))
                        {
                            dr["Remarks"] = "invalid Amount";
                            continue;
                        }
                        DateTime tFromDt = Convert.ToDateTime(dr["FromDt"]);
                        DateTime tToDt = Convert.ToDateTime(dr["ToDT"]);
                        string err = DataValidate(tEmpUnqID, tPayPeriod, tFromDt, tToDt, tBasic);

                        if (!string.IsNullOrEmpty(err))
                        {
                            dr["Remarks"] = err;
                            continue;
                        }
                        
                        try
                        {
                            string sql = "Delete From Cont_MastBasic Where PayPeriod = '" + tPayPeriod.ToString() + "' And EmpUnqID = '" + tEmpUnqID.ToString() + "'";
                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                            dr["Remarks"] = ex.ToString();
                            continue;
                        }

                    }//end first loop
                    con.Close();
                    #endregion

                    Cursor.Current = Cursors.WaitCursor;
                    foreach (DataRow dr in sortedDT.Rows)
                    {

                        try
                        {
                            con.Open();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        #region prmycheck

                        string tEmpUnqID = dr["EmpUnqID"].ToString();
                        string tPayPeriod = dr["PayPeriod"].ToString();
                        int tSrno = 0;
                        if(!int.TryParse(dr["SrNo"].ToString(),out tSrno))
                        {
                            dr["Remarks"] = "invalid SrNo";
                            continue;
                        }else{
                            if(tSrno <= 0){
                                dr["Remarks"] = "invalid SrNo";
                                continue;
                            }
                        }

                        DateTime tFromDt = Convert.ToDateTime(dr["FromDt"]);
                        DateTime tToDt = Convert.ToDateTime(dr["ToDT"]);

                        double tBasic = 0 ;
                        
                        if(!double.TryParse(dr["cBasic"].ToString(),out tBasic))
                        {
                            dr["Remarks"] = "invalid Amount";
                            continue;
                        }
                        
                        
                        string err = DataValidate(tEmpUnqID, tPayPeriod,tFromDt,tToDt,tBasic);

                        if (!string.IsNullOrEmpty(err))
                        {
                            dr["Remarks"] = err;
                            continue;
                        }
                        #endregion

                        SqlTransaction tr = con.BeginTransaction();
                    
                        try
                        {
                        
                            string sql = "Insert into Cont_MastBasic (PayPeriod,EmpUnqID,SrNo,FromDt,ToDt,cBasic,AddDt,AddID) Values" +
                                " ('{0}','{1}','{2}','{3:yyyy-MM-dd}','{4:yyyy-MM-dd}','{5}',GetDate(),'{6}')";
                            sql = string.Format(sql, tPayPeriod, tEmpUnqID, tSrno.ToString(), tFromDt,tToDt,tBasic.ToString(),Utils.User.GUserID);

                            SqlCommand cmd = new SqlCommand(sql, con, tr);
                            cmd.ExecuteNonQuery();
                            dr["remarks"] = "Record saved...";
                            tr.Commit();
                            tr.Dispose();
                        }                            
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            tr.Dispose();
                            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        con.Close();
                    }//end second foreach loop

                    
                }//end using con

                Cursor.Current = Cursors.Default;
                MessageBox.Show("file uploaded Successfully, please check the remarks for indivisual record status...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            DataSet ds = new DataSet();
            ds.Tables.Add(sortedDT);
            grd_view.DataSource = ds;
            grd_view.DataMember = ds.Tables[0].TableName;
            grd_view.Refresh();

            Cursor.Current = Cursors.Default;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBrowse.Text.Trim().ToString()))
            {
                MessageBox.Show("Please Select Excel File First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnBrowse.Enabled = false;

            if (GRights.Contains("A") || GRights.Contains("U") || GRights.Contains("D"))
            {
                btnImport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
            }

            

            Cursor.Current = Cursors.WaitCursor;
            grd_view.DataSource = null;
            string filePath = txtBrowse.Text.ToString();

            string sexcelconnectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";
            //string sexcelconnectionstring = @"provider=microsoft.jet.oledb.4.0;data source=" + filePath + ";extended properties=" + "\"excel 8.0;hdr=yes;IMEX=1;\"";

            OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
            List<SheetName> sheets = ExcelHelper.GetSheetNames(oledbconn);

            try
            {
                oledbconn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sheetname = "[" + sheets[0].sheetName.Replace("'", "") + "]";
            try
            {
                string myexceldataquery = "select PayPeriod,EmpUnqID,SrNo,FromDt,ToDt,cBasic,'' as Remarks from " + sheetname;
                OleDbDataAdapter oledbda = new OleDbDataAdapter(myexceldataquery, oledbconn);
                dt.Clear();
                oledbda.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    if (string.IsNullOrEmpty(row["EmpUnqID"].ToString().Trim()))
                        row.Delete();
                }
                oledbconn.Close();
            }
            catch (Exception ex)
            {
                oledbconn.Close();
                MessageBox.Show("Please Check upload template..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                btnImport.Enabled = false;
                oledbconn.Close();
                return;
            }
            

            DataView dv = dt.DefaultView;
            dv.Sort = "EmpUnqID asc";
            DataTable sortedDT = dv.ToTable();




            grd_view.DataSource = sortedDT;

            if (GRights.Contains("A") || GRights.Contains("U") || GRights.Contains("D"))
            {
                btnImport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                if (saveDialog.ShowDialog() != DialogResult.Cancel)
                {
                    string exportFilePath = saveDialog.FileName;
                    string fileExtenstion = new FileInfo(exportFilePath).Extension;

                    switch (fileExtenstion)
                    {
                        case ".xls":
                            grd_view.ExportToXls(exportFilePath);
                            break;
                        case ".xlsx":
                            grd_view.ExportToXlsx(exportFilePath);
                            break;
                        case ".rtf":
                            grd_view.ExportToRtf(exportFilePath);
                            break;
                        case ".pdf":
                            grd_view.ExportToPdf(exportFilePath);
                            break;
                        case ".html":
                            grd_view.ExportToHtml(exportFilePath);
                            break;
                        case ".mht":
                            grd_view.ExportToMht(exportFilePath);
                            break;
                        default:
                            break;
                    }

                    if (File.Exists(exportFilePath))
                    {
                        try
                        {
                            //Try to open the file and let windows decide how to open it.
                            System.Diagnostics.Process.Start(exportFilePath);
                        }
                        catch
                        {
                            String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                        MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void frmBulkBasicChng_Load(object sender, EventArgs e)
        {
            GRights = ContractPayroll.Classes.Globals.GetFormRights(this.Name);
            grd_view.DataSource = null;
            btnImport.Enabled = false;
        }
    }
}