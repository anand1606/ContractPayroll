using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Data.OleDb;
using System.Net.Mail;

namespace ContractPayroll.Classes
{
    
    
    class Globals
    {

        public static string MasterMachineIP = string.Empty;
        
       
        //used for checkpath
        public static string G_UpdateChkPath;

       
        public static string G_NetworkDomain;
        public static string G_NetworkUser;
        public static string G_NetworkPass;

       

        public static bool GetGlobalVars()
        {
            bool tset = false;

            DataSet ds = new DataSet();

            string sql = "Select Top 1 * from MastBCFlg ";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {                
                    
                }
            }

            sql = "Select top 1 * From MastNetwork ";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                tset = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    

                    G_UpdateChkPath = dr["UpdateChkPath"].ToString();
                    G_NetworkDomain = dr["NetworkDomain"].ToString();
                    G_NetworkUser = dr["NetworkUser"].ToString();
                    G_NetworkPass = dr["NetworkPass"].ToString();
                    
                    Utils.DomainUserConfig.DomainName = dr["NetworkDomain"].ToString();
                    Utils.DomainUserConfig.DomainUser = dr["NetworkUser"].ToString();
                    Utils.DomainUserConfig.DomainPassword = dr["NetworkPass"].ToString();

                }
                
            }
            else
            {
                tset = false ;
            }
                        
            return tset;


        }
       

        public static DateTime GetSystemDateTime()
        {
            DateTime dt = new DateTime();

            DataSet ds = new DataSet();
            string sql = "Select GetDate() as CurrentDate ";
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            dt = DateTime.Now;
            if (hasRows)
            {
               

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dt = (DateTime)dr["CurrentDate"];
                }

            }
            return dt;
        }

        public static string GetFormRights(string FormName1)
        {
            string frmrights = "XXXV";
            int FormID = 0;

             //Setting WaterIP
            DataSet ds = new DataSet();
            string sql = "select FormId from Cont_MastFrm where FormName ='" + FormName1.Trim() + "'";
            
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    FormID = Convert.ToInt32(dr["FormID"]);
                }
            }

            sql = "Select top 1 * From Cont_UserRights where FormId = '" + FormID.ToString() + "' and Userid = '" + Utils.User.GUserID + "'";
            ds = new DataSet();
            ds = Utils.Helper.GetData(sql, Utils.Helper.constr);
            hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    frmrights = ((Convert.ToBoolean(dr["Add1"])) ? "A" : "X")
                        + ((Convert.ToBoolean(dr["UpDate1"])) ? "U" : "X")
                        + ((Convert.ToBoolean(dr["Delete1"])) ? "D" : "X")
                        + ((Convert.ToBoolean(dr["View1"])) ? "V" : "X");

                }
            }

            return frmrights;
        }

        public static bool GetWrkGrpRights(int Formid, string WrkGrp, string EmpUnqID)
        {
            bool returnval = false;

            DataSet ds = new DataSet();

            if ( EmpUnqID != "")
            {
                WrkGrp = Utils.Helper.GetDescription("Select WrkGrp From MastEmp Where EmpUnqID ='" + EmpUnqID + "'", Utils.Helper.constr);
            }
            
            if (WrkGrp == "" && EmpUnqID == "")
            {
                return false;
            }
            

            string wkgsql = "Select * from UserSpRight where UserID = '" + Utils.User.GUserID + "' and FormID = '" + Formid.ToString() + "' and WrkGrp = '" + WrkGrp + "' and Active = 1";


            ds = Utils.Helper.GetData(wkgsql, Utils.Helper.constr);
            bool hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);

            if (hasRows)
            {
                returnval = true;
            }
            else
            {
                returnval = false;
            }

            return returnval;
        }


        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

    }

    


    class SheetName
    {
        public string sheetName { get; set; }
        public string sheetType { get; set; }
        public string sheetCatalog { get; set; }
        public string sheetSchema { get; set; }
    }


    class ExcelHelper
    {
        public static List<SheetName> GetSheetNames(OleDbConnection conn)
        {
            List<SheetName> sheetNames = new List<SheetName>();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DataTable excelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            foreach (DataRow row in excelSchema.Rows)
            {
                if (!row["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                {
                    sheetNames.Add(new SheetName() { sheetName = row["TABLE_NAME"].ToString(), sheetType = row["TABLE_TYPE"].ToString(), sheetCatalog = row["TABLE_CATALOG"].ToString(), sheetSchema = row["TABLE_SCHEMA"].ToString() });
                }
            }
            conn.Close();
            return sheetNames;
        }
    }


   


}
