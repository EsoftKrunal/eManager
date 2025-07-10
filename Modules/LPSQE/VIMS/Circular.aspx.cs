using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;
using System.IO;

public partial class Circular : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int Mail_CId 
    {
        get { return Common.CastAsInt32(ViewState["Mail_CId"]); }
        set { ViewState["Mail_CId"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["MHSS"] = "9";
            CurrentVessel = Session["CurrentShip"].ToString();
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Session["UserName"].ToString();

            txtFDate.Text = "01-Jan-" + DateTime.Today.Year;
            txtTDate.Text = "31-Dec-" + DateTime.Today.Year;

            BindCategory();
            Bindgrid();
        }
    }
    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        int CId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Cir_Circular WHERE CId=" + CId.ToString());
        
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentFileName"].ToString();
            //string Path = Server.MapPath("~/Attachments/" + FileName);
            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void btnShowClosure_Click(object sender, EventArgs e)
    {
        Mail_CId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        dv_Closure.Visible = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int CId= Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string CircularNumber = ((ImageButton)sender).CssClass;

        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", CurrentVessel),
                new MyParameter("@RecordType", "Circular"),
                new MyParameter("@RecordId", CId),
                new MyParameter("@RecordNo", CircularNumber),
                new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('" + ds.Tables[0].Rows[0][0].ToString() + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Sent for export successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + Common.getLastError() + "');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + ex.Message + "');", true);
        }
        
        //string SQL = "SELECT * FROM DBO.Cir_Vessel_Notifications WHERE CId = " + CId + " AND VesselCode='" + CurrentVessel + "'";
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //dt.TableName = "Cir_Vessel_Notifications";
        //string SchemaFile = Server.MapPath("~/VIMS/TEMP/CircularNotificationSchema.xml");
        //string DataFile = Server.MapPath("~/VIMS/TEMP/CircularNotificationData.xml");
        //string ZipFile = Server.MapPath("~/VIMS/TEMP/" + CircularNumber.Replace("/", "-") + ".zip");
        //List<string> BCCMails = new List<string>();
        //dt.DataSet.WriteXmlSchema(SchemaFile);
        //dt.DataSet.WriteXml(DataFile);
        //using (ZipFile zip = new ZipFile())
        //{
        //    zip.AddFile(SchemaFile);
        //    zip.AddFile(DataFile);
        //    zip.Save(ZipFile);
        //}

        //byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
        //Response.AppendHeader("Content-Disposition", "attachment; filename=CircularACK_" + CurrentVessel + "_" + Path.GetFileName(ZipFile));
        //Response.BinaryWrite(buff);
        //Response.Flush();
        //Response.End();
    }
    
    protected void btnClosureSave_Click(object sender, EventArgs e)
    {

        int discussed=Common.CastAsInt32(rbtn1.SelectedValue);
        int noticed=Common.CastAsInt32(rbtn2.SelectedValue);
        if (discussed == 0 || noticed == 0)
        {
            lblmess.Visible = true;
            return;
        }

        Common.Execute_Procedures_Select_ByQuery("exec CIR_InsertUpdateCircular_Vessel_Notifications_VSL " + Mail_CId.ToString() + ",'" + CurrentVessel + "','" + DateTime.Today.ToString("dd-MMM-yyyy hh:ss tt") + "',''," + discussed + "," + noticed + "");
        Bindgrid();
        dv_Closure.Visible = false;
    }
    protected void btnClosureCancel_Click(object sender, EventArgs e)
    {
        dv_Closure.Visible = false;
    }

    protected void btnSaveImport_Click(object sender, EventArgs e)
    {
        if (flp_Upload.HasFile)
        {
            string TargetDir = Server.MapPath("~/VIMS/TEMP/");
            string SchemaFile = TargetDir + "CircularSchema.XML";
            string DataFile = TargetDir + "CircularData.XML";


            if (File.Exists(SchemaFile))
                File.Delete(SchemaFile);
            if (File.Exists(DataFile))
                File.Delete(DataFile);

            if (flp_Upload.HasFile)
            {
                using (ZipFile zip = ZipFile.Read(flp_Upload.PostedFile.InputStream))
                {

                    foreach (ZipEntry ex in zip.EntriesSorted)
                    {
                        try
                        {
                            ex.FileName = Path.GetFileName(ex.FileName);
                            ex.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently);
                        }
                        catch { continue; }
                    }
                }
                ImportData(SchemaFile, DataFile);
                dv_Import.Visible = false;
            }
        }
        else
        {
            ProjectCommon.ShowMessage("Please select a file to import.");
        }
        
    }
    protected void btnCancelImport_Click(object sender, EventArgs e)
    {
        dv_Import.Visible = false;
    }
    protected void btnShowImport_Click(object sender, EventArgs e)
    {
        dv_Import.Visible = true;
    }

    protected void Filter_Cir(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void ImportData(string SchemaFile,string DataFile)
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString().Replace("Master", "eMANAGER");
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString);
        Con.Open();
        trans = Con.BeginTransaction();
        DataSet ds=new DataSet();
        ds.ReadXmlSchema(SchemaFile);
        ds.ReadXml(DataFile);
        ResetNULLDates(ref ds);

        int CId = Convert.ToInt32(ds.Tables["Cir_Circular"].Rows[0]["CId"]);

        try
        {

        SqlCommand cmd = new SqlCommand("", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Transaction = trans;
        //------------------------------------------------      
        cmd.CommandType = CommandType.StoredProcedure;
        //------------------------------------------------      
        if (ds.Tables["Cir_Circular"].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables["Cir_Circular"].Rows)
            {
                //-------------------------------
                string[] CommandParameters = getCommandParameters(cmd, "CIR_InsertUpdateCircular_VSL");
                cmd.Parameters.Clear();
                cmd.CommandText = "CIR_InsertUpdateCircular_VSL";
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i <= CommandParameters.Length - 1; i++)
                {
                    object data = DBNull.Value;
                    try
                    { data = dr[CommandParameters[i]]; }
                    catch { data = DBNull.Value; }
                    cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                }
                //------------------------------
                int result = cmd.ExecuteNonQuery();
            }
        }

        trans.Commit();
        ProjectCommon.ShowMessage("Circular imported successfully.");
        Bindgrid();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            ProjectCommon.ShowMessage("Unable to import circular.");
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
    protected string[] getCommandParameters(SqlCommand cmd, string ProcName)
    {
        string[] result;
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.Text;
        //---------------- CHECKING PACKET RECEIVED NO. IS GREATER THAN DB PACKET RECEIVED
        cmd.CommandText = "select replace(parameter_name,'@','') as parameter_name from information_schema.parameters where specific_name='" + ProcName + "' and ltrim(rtrim(parameter_name))<>''";
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter();
        adp.SelectCommand = cmd;
        adp.Fill(dt);
        result = new string[dt.Rows.Count];
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            result[i] = dt.Rows[i][0].ToString();
        }
        return result;
    }
    private void ResetNULLDates(ref DataSet ds_IN)
    {
        DateTime dt_ref = new DateTime(1900, 1, 1);
        foreach (DataTable dt in ds_IN.Tables)
        {
            List<String> DateTimeCols = new List<String>();
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == System.Type.GetType("System.DateTime"))
                {
                    DateTimeCols.Add(dc.ColumnName);
                }
            }
            if (DateTimeCols.Count > 0 && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (string cName in DateTimeCols)
                    {
                        if (!Convert.IsDBNull(dr[cName]))
                        {
                            DateTime dt_test = Convert.ToDateTime(dr[cName]);
                            if (dt_test <= dt_ref)
                            {
                                dr[cName] = DBNull.Value;
                            }
                        }
                    }
                }
            }
        }
    }
    protected void BindCategory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Cir_Category ORDER BY CirCatName");         
        ddlCategory.DataSource =dt;
        ddlCategory.DataTextField = "CirCatName";
        ddlCategory.DataValueField = "CirCatId";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("< ALL >", "0"));

    }
    protected void Bindgrid()
    {
        string CIRCatSQL = "";

        if (ddlCategory.SelectedIndex > 0)
        {
            CIRCatSQL += " WHERE C.[Category] =" + ddlCategory.SelectedValue + "";
        }

        string SQL = "SELECT [CId], [CircularNumber], [CircularDate], [CirCatName], [CType],[Source],[Topic], [Details], [NextReviewDate], [AttachmentFileName],  " +
                     "[CreatedBy],[CreatedOn], CASE WHEN [Status] = 1 THEN 'Active'  WHEN [Status] = 2 THEN 'InActive' WHEN [Status] = 3 THEN 'In SMS' ELSE '' END AS [StatusText], [Status], " +
                     "(SELECT AckOn from Cir_Vessel_Notifications WHERE CId=C.CId AND VESSELCODE='" + CurrentVessel + "') AS CLOSURE " +
                     "FROM dbo.Cir_Circular C " +
                     "INNER JOIN dbo.Cir_Category CC ON CC.[CirCatId] = C.[Category] ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + CIRCatSQL + " ORDER BY CircularDate Desc ");

        if (dt != null)
        {
            DataView dv = dt.DefaultView;
            string Filter = " 1=1 ";

            if (txtFDate.Text.Trim() != "")
            {
                Filter += " AND CircularDate >='" + txtFDate.Text + "'";
            }
            if (txtTDate.Text.Trim() != "")
            {
                Filter += " AND CircularDate <='" + txtTDate.Text + "'";
            }
            if (txtSearchText.Text.Trim() != "")
            {
                Filter += " AND Details like '%" + txtSearchText.Text.Trim() + "%'";
            }
            if (ddlStatus.SelectedIndex != 0)
            {
                Filter += " AND Status=" + ddlStatus.SelectedValue;
            }
            if (ddlType_Search.SelectedIndex != 0)
            {
                Filter += " AND CType='" + ddlType_Search.SelectedValue + "'";
            }
             
            dv.RowFilter = Filter;

            rptCIR.DataSource = dv.ToTable();
            rptCIR.DataBind();
        }
        else
        {
            rptCIR.DataSource = null;
            rptCIR.DataBind();
        }
    }
}
