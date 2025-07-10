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

public partial class FC : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int Mail_FocusCampId 
    {
        get { return Common.CastAsInt32(ViewState["FocusCampId"]); }
        set { ViewState["FocusCampId"] = value; }
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
            Session["MHSS"] = "8";
            CurrentVessel = Session["CurrentShip"].ToString();
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Session["UserName"].ToString();

            BindFocusCampCat();
            Bindgrid();
        }
    }
    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        int FocusCampId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.FocusCamp WHERE FocusCampId=" + FocusCampId.ToString());
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentFileName"].ToString();
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
        Mail_FocusCampId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        dv_Closure.Visible = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int FocusCampId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string LFINumber = ((ImageButton)sender).CssClass;

        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", CurrentVessel),
                new MyParameter("@RecordType", "Focus Campaign"),
                new MyParameter("@RecordId", FocusCampId),
                new MyParameter("@RecordNo", LFINumber),
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
        
        //string SQL = "SELECT * FROM DBO.FocusCamp_Vessel_Notifications WHERE FocusCampID = " + FocusCampId + " AND VesselCode='" + CurrentVessel + "'";
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //dt.TableName = "FocusCamp_Vessel_Notifications";
        //string SchemaFile = Server.MapPath("~/VIMS/TEMP/FocusCampNotificationSchema.xml");
        //string DataFile = Server.MapPath("~/VIMS/TEMP/FocusCampNotificationData.xml");
        //string ZipFile = Server.MapPath("~/VIMS/TEMP/" + LFINumber + ".zip");
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
        //Response.AppendHeader("Content-Disposition", "attachment; filename=FCACK_" + CurrentVessel + "_" + Path.GetFileName(ZipFile));
        //Response.BinaryWrite(buff);
        //Response.Flush();
        //Response.End();
    }

    protected void btnClosureSave_Click(object sender, EventArgs e)
    {

        int discussed = Common.CastAsInt32(rbtn1.SelectedValue);
        int noticed = Common.CastAsInt32(rbtn2.SelectedValue);
        if (discussed == 0 || noticed == 0)
        {
            lblmess.Visible = true;
            return;
        }


        Common.Execute_Procedures_Select_ByQuery("exec InsertUpdateFocusCamp_Vessel_Notifications_VSL " + Mail_FocusCampId.ToString() + ",'" + CurrentVessel + "','" + DateTime.Today.ToString("dd-MMM-yyyy hh:ss tt") + "',''," + discussed + "," + noticed + "");
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
            string SchemaFile = TargetDir + "FocusCampSchema.XML";
            string DataFile = TargetDir + "FocusCampData.XML";


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
            ProjectCommon.ShowMessage("Please select a file to upload.");
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


    protected void Filter_FocusCamp(object sender, EventArgs e)
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

        int FocusCampId = Convert.ToInt32(ds.Tables["FocusCamp"].Rows[0]["FocusCampId"]);

        try
        {

        SqlCommand cmd = new SqlCommand("", Con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Transaction = trans;
        //------------------------------------------------      
        cmd.CommandType = CommandType.StoredProcedure;
        //------------------------------------------------      
        if (ds.Tables["FocusCamp"].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables["FocusCamp"].Rows)
            {
                //-------------------------------
                string[] CommandParameters = getCommandParameters(cmd, "InsertUpdateFocusCamp_VSL");
                cmd.Parameters.Clear();
                cmd.CommandText = "InsertUpdateFocusCamp_VSL";
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
        ProjectCommon.ShowMessage("FocusCamp imported successfully.");
        Bindgrid();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            ProjectCommon.ShowMessage("Unable to import FocusCamp.");
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
    protected void BindFocusCampCat()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.FocusCampCategory ORDER BY FocusCampCatName");
        ddlFocusCampCat.DataSource =dt; 
        ddlFocusCampCat.DataTextField = "FocusCampCatName";
        ddlFocusCampCat.DataValueField = "FocusCampCatId";
        ddlFocusCampCat.DataBind();
        ddlFocusCampCat.Items.Insert(0,new ListItem("< ALL >", "0"));

    }
    protected void Bindgrid()
    {
        string FocusCampCatSQL = "";
        
        if (ddlFocusCampCat.SelectedIndex>0)
        {
            FocusCampCatSQL = " WHERE FocusCampID IN ( SELECT FocusCampID FROM DBO.FocusCamp_Categories WHERE FocusCampCATID=" + ddlFocusCampCat.SelectedValue + ")";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FocusCampID,STATUS,FocusCampNUMBER,FocusCampDATE,TITLE,ATTACHMENTFILENAME,(SELECT AckOn from FocusCamp_Vessel_Notifications WHERE FocusCampID=FocusCamp.FocusCampID AND VESSELCODE='" + CurrentVessel + "') AS CLOSURE,(SELECT dbo.getFocusCampCategories(FocusCampID)) AS FocusCampCATS from DBO.FocusCamp " + FocusCampCatSQL);
       
        if (dt != null)
        {
            DataView dv = dt.DefaultView;
            string Filter = " 1=1 ";

            if(txtFDate.Text.Trim()!="")
            {
                Filter += " AND FocusCampDATE >='" + txtFDate.Text + "'";
            }
            if (txtTDate.Text.Trim() != "")
            {
                Filter += " AND FocusCampDATE <='" + txtTDate.Text + "'";
            }

            dv.RowFilter = Filter;
            rptFocusCamp.DataSource = dv.ToTable();
            rptFocusCamp.DataBind();      
        }
        else
        {
            rptFocusCamp.DataSource = null;
            rptFocusCamp.DataBind();
        }
    }
}
