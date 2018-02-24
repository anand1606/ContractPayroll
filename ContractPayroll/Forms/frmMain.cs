using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using ContractPayroll.Classes;
using System.Threading;
using System.IO;
using System.Net;
using ConnectUNCWithCredentials;
using System.Reflection;
using ContractPayroll.Forms;

namespace ContractPayroll
{
    public partial class frmMain : XtraForm
    {
        public static string cnstr = Utils.Helper.constr;
        public static Utils.DbCon tdb = Utils.Helper.ReadConDb("DBCON");
        

        public frmMain()
        {
            InitializeComponent();
            stsUserID.Text = Utils.User.GUserID;
            stsUserDesc.Text = Utils.User.GUserName;

            this.Text = "Contract Payroll System : (Server->" + tdb.DataSource + ")";
        }

        private void mnuUserRights_Click(object sender, EventArgs e)
        {
           
            Form t = Application.OpenForms["frmUserRights"];

            if (t == null)
            {
                frmUserRights m = new frmUserRights();
                m.MdiParent = this;
                m.Show();
            }

        }

        private void mnuLogOff_Click(object sender, EventArgs e)
        {
            Utils.User.GUserID = string.Empty;
            Utils.User.GUserName = string.Empty;
            Utils.User.GUserPass = string.Empty;
            Utils.User.IsAdmin = false;


            Program.OpenMDIFormOnClose = false;
            this.Hide();
            Application.Restart();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           


            ToolStripItemCollection tmnu = menuStrip1.Items;
            SetToolStripItems(tmnu);

            mnuAdmin.Enabled = true;
           
            mnuMast.Enabled = true;        
           
            
            mnuTranS.Enabled = true;
            mnuChangePass.Enabled = true;
            mnuLogOff.Enabled = true;

            DataSet ds = new DataSet();
            string sql = "select menuname from  Cont_MastFrm where formid in (select FormId from Cont_UserRights where UserId ='" + Utils.User.GUserID + "' and View1=1) order by seqid";
            ds = Utils.Helper.GetData(sql,cnstr);
            
            mnuUser.Enabled = true;
            Boolean hasRows = ds.Tables.Cast<DataTable>()
                           .Any(table => table.Rows.Count != 0);
                
            
            if (hasRows)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string mnu = dr["menuname"].ToString();

                    ToolStripItem[] t = tmnu.Find(mnu, true);

                    foreach (ToolStripItem ti in t)
                    {
                        ti.Enabled = true;
                    }

                }    
            }

            this.mnuHelp.Enabled = true;
            this.mnuAbout.Enabled = true;
           


           
            //get localmodification date
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string localfile = Uri.UnescapeDataString(uri.Path);
            
            if (IsNetworkPath(localfile))
            {
                MessageBox.Show("Does not allow to run from remote location/shared folder..," +
                    Environment.NewLine +
                    "Please Copy to Local Drive and Run Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }


           
            //check for update version.
            DateTime servermodified = new DateTime();
            DateTime localmodified = new DateTime();

            if (!string.IsNullOrEmpty(Globals.G_UpdateChkPath))
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                    
                using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials())
                {
                    if (unc.NetUseWithCredentials(Globals.G_UpdateChkPath,
                                                    Globals.G_NetworkUser,
                                                    Globals.G_NetworkDomain,
                                                    Globals.G_NetworkPass))
                    {
                        string fullpath = Path.Combine(Globals.G_UpdateChkPath, "ContractPayroll.exe");
                        if (File.Exists(fullpath))
                        {
                            servermodified = File.GetLastWriteTime(fullpath);
                        }
                    }
                }

                    
                localmodified = File.GetLastWriteTime(localfile);
                if (servermodified > localmodified)
                {
                    MessageBox.Show("New Upgrade is available, please update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                        
                }
                this.Cursor = Cursors.Default;
            }           

        }

        public static string GetNetworkPathFromServerName(string serverName)
        {
            // Assume we can't connect to the server to start with.
            var networkPath = String.Empty;

            // If this is a rooted path, just make sure it is available.
            if (Path.IsPathRooted(serverName))
            {
                // If the path exists, use it.
                if (Directory.Exists(serverName))
                    networkPath = serverName;
            }
            // Else this is a network path.
            else
            {
                // If the server name has a backslash in it, remove the backslash and everything after it.
                serverName = serverName.Trim(@"\".ToCharArray());
                if (serverName.Contains(@"\"))
                    serverName = serverName.Remove(serverName.IndexOf(@"\", StringComparison.Ordinal));

                try
                {
                    // If the server is available, format the network path properly to use it.
                    if (Dns.GetHostEntry(serverName) != null)
                    {
                        // Root the path as a network path (i.e. add \\ to the front of it).
                        networkPath = String.Format("\\\\{0}", serverName);
                    }
                }
                // Eat any Host Not Found exceptions for if we can't connect to the server.
                catch (System.Net.Sockets.SocketException)
                { }
            }

            return networkPath;
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            

        }

        private void SetToolStripItems(ToolStripItemCollection dropDownItems)
        {
            try
            {
                foreach (object obj in dropDownItems)
                //for each object.
                {
                    ToolStripMenuItem subMenu = obj as ToolStripMenuItem;
                    //Try cast to ToolStripMenuItem as it could be toolstrip separator as well.

                    if (subMenu != null)
                    //if we get the desired object type.
                    {
                        if (subMenu.HasDropDownItems) // if subMenu has children
                        {
                            SetToolStripItems(subMenu.DropDownItems); // Call recursive Method.
                        }
                        else // Do the desired operations here.
                        {
                            subMenu.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SetToolStripItems",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mnuChangePass_Click(object sender, EventArgs e)
        {   
            Form t = Application.OpenForms["frmChangePass"];

            if (t == null)
            {
                frmChangePass m = new frmChangePass();
                m.MdiParent = this;
                m.Show();
            }

        }
        
        private void mnuDBConn_Click(object sender, EventArgs e)
        {
            
            Form t = Application.OpenForms["FrmConnection"];

            if (t == null)
            {
                FrmConnection m = new FrmConnection();
                m.MdiParent = this;
                m.typeofcon = "DBCON";
                m.Show();
            }

        }

        private void mnuConfig_Click(object sender, EventArgs e)
        {

        }

        private void mnuCreateUser_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmUserRights"];

            if (t == null)
            {
                frmUserRights m = new frmUserRights();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            string msg = "Contract Payroll System" + Environment.NewLine +
                "Version 2.1 " + Environment.NewLine +
                "Design & Devloped By : Anand Acharya " + Environment.NewLine;

            MessageBox.Show(msg, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static Boolean IsNetworkPath(String path)
        {

            try
            {
                Uri uri = new Uri(path);
                if (uri.IsUnc)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
            
            
            
        }

        private void mnuOtherConfig_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmOtherConfig"];

            if (t == null)
            {
                frmOtherConfig m = new frmOtherConfig();
                m.defSet = true;
                m.MdiParent = this;
                m.Show();
            }
        }

        

        private void mnuPayPeriod_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmPayPeriod"];

            if (t == null)
            {
                frmPayPeriod m = new frmPayPeriod();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuPayCyclePara_Click(object sender, EventArgs e)
        {
            frmOtherConfig m = new frmOtherConfig();
            m.defSet = false;
            m.MdiParent = this;
            m.Show();
        }

        private void mnuImportEmp_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmImportEmp"];
            if (t == null)
            {
                frmImportEmp m = new frmImportEmp();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuImportAttd_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmImportAttd"];
            if (t == null)
            {
                frmImportAttd m = new frmImportAttd();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMastEmp_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmEmpMaster"];
            if (t == null)
            {
                frmEmpMaster m = new frmEmpMaster();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMthlyAttdProc_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMthlyAttdProcess"];
            if (t == null)
            {
                frmMthlyAttdProcess m = new frmMthlyAttdProcess();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMthlyAdj_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMthlyAdj"];
            if (t == null)
            {
                frmMthlyAdj m = new frmMthlyAdj();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMthlyDed_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmBulkDed"];
            if (t == null)
            {
                frmBulkDed m = new frmBulkDed();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuMthlyCalc_Click(object sender, EventArgs e)
        {
            Form t = Application.OpenForms["frmMthlyCalc"];
            if (t == null)
            {
                frmMthlyCalc m = new frmMthlyCalc();
                m.MdiParent = this;
                m.Show();
            }
        }

        private void mnuBulkBasicChng_Click(object sender, EventArgs e)
        {
            //mnuBulkBasicChng
            Form t = Application.OpenForms["frmBulkBasicChng"];
            if (t == null)
            {
                frmBulkBasicChng m = new frmBulkBasicChng();
                m.MdiParent = this;
                m.Show();
            }
        
        
        }
        

    }
}