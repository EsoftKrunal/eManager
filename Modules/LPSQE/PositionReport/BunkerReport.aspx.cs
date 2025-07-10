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

public partial class BunkerReport : System.Web.UI.Page
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
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  CurrentVessel = Session["CurrentShip"].ToString();
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Session["UserName"].ToString();

            //txtFDate.Text = "01-Jan-" + DateTime.Today.Year;
            //txtTDate.Text = "31-Dec-" + DateTime.Today.Year;
          
            BindddlType();
            Bindgrid();
            
        }
    }
    private void BindddlType()
    {
        string sql = "SELECT    [FuelTypeName] ,[ShortName] FROM [dbo].[MRV_FuelTypes] with (nolock) order by [ShortName]";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlType.Items.Clear();
        ddlType.Items.Add(new ListItem { Text = "", Value = "" });
        foreach (DataRow dr in dt.Rows)
        {
            ddlType.Items.Add(new ListItem { Text = dr["FuelTypeName"].ToString(), Value = dr["ShortName"].ToString() });
        }
    }
    //protected void btnDownloadFile_Click(object sender, EventArgs e)
    //{
    //    int CId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Cir_Circular WHERE CId=" + CId.ToString());

    //    if (dt.Rows.Count > 0)
    //    {
    //        string FileName = dt.Rows[0]["AttachmentFileName"].ToString();
    //        //string Path = Server.MapPath("~/Attachments/" + FileName);
    //        if (FileName.Trim() != "")
    //        {
    //            byte[] buff = (byte[])dt.Rows[0]["Attachment"];
    //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
    //            Response.BinaryWrite(buff);
    //            Response.Flush();
    //            Response.End();
    //        }
    //    }
    //}
    //protected void btnShowClosure_Click(object sender, EventArgs e)
    //{
    //    Mail_CId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    dv_Closure.Visible = true;
    //}
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    int CId= Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    string CircularNumber = ((ImageButton)sender).CssClass;
    //    string SQL = "SELECT * FROM DBO.Cir_Vessel_Notifications WHERE CId = " + CId + " AND VesselCode='" + CurrentVessel + "'";
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    dt.TableName = "Cir_Vessel_Notifications";
    //    string SchemaFile = Server.MapPath("~/VIMS/TEMP/CircularNotificationSchema.xml");
    //    string DataFile = Server.MapPath("~/VIMS/TEMP/CircularNotificationData.xml");
    //    string ZipFile = Server.MapPath("~/VIMS/TEMP/" + CircularNumber.Replace("/", "-") + ".zip");
    //    List<string> BCCMails = new List<string>();
    //    dt.DataSet.WriteXmlSchema(SchemaFile);
    //    dt.DataSet.WriteXml(DataFile);
    //    using (ZipFile zip = new ZipFile())
    //    {
    //        zip.AddFile(SchemaFile);
    //        zip.AddFile(DataFile);
    //        zip.Save(ZipFile);
    //    }

    //    byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
    //    Response.AppendHeader("Content-Disposition", "attachment; filename=CircularACK_" + CurrentVessel + "_" + Path.GetFileName(ZipFile));
    //    Response.BinaryWrite(buff);
    //    Response.Flush();
    //    Response.End();
    //}

    //protected void btnClosureSave_Click(object sender, EventArgs e)
    //{

    //    int discussed=Common.CastAsInt32(rbtn1.SelectedValue);
    //    int noticed=Common.CastAsInt32(rbtn2.SelectedValue);
    //    if (discussed == 0 || noticed == 0)
    //    {
    //        lblmess.Visible = true;
    //        return;
    //    }

    //    Common.Execute_Procedures_Select_ByQuery("exec CIR_InsertUpdateCircular_Vessel_Notifications_VSL " + Mail_CId.ToString() + ",'" + CurrentVessel + "','" + DateTime.Today.ToString("dd-MMM-yyyy hh:ss tt") + "',''," + discussed + "," + noticed + "");
    //    Bindgrid();
    //    dv_Closure.Visible = false;
    //}
    //protected void btnClosureCancel_Click(object sender, EventArgs e)
    //{
    //    dv_Closure.Visible = false;
    //}

    //protected void btnSaveImport_Click(object sender, EventArgs e)
    //{
    //    if (flp_Upload.HasFile)
    //    {
    //        string TargetDir = Server.MapPath("~/VIMS/TEMP/");
    //        string SchemaFile = TargetDir + "CircularSchema.XML";
    //        string DataFile = TargetDir + "CircularData.XML";


    //        if (File.Exists(SchemaFile))
    //            File.Delete(SchemaFile);
    //        if (File.Exists(DataFile))
    //            File.Delete(DataFile);

    //        if (flp_Upload.HasFile)
    //        {
    //            using (ZipFile zip = ZipFile.Read(flp_Upload.PostedFile.InputStream))
    //            {

    //                foreach (ZipEntry ex in zip.EntriesSorted)
    //                {
    //                    try
    //                    {
    //                        ex.FileName = Path.GetFileName(ex.FileName);
    //                        ex.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently);
    //                    }
    //                    catch { continue; }
    //                }
    //            }
    //            ImportData(SchemaFile, DataFile);
    //            dv_Import.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        ProjectCommon.ShowMessage("Please select a file to import.");
    //    }

    //}
    //protected void btnCancelImport_Click(object sender, EventArgs e)
    //{
    //    dv_Import.Visible = false;
    //}
    //protected void btnShowImport_Click(object sender, EventArgs e)
    //{
    //    dv_Import.Visible = true;
    //}

    protected void Filter_Cir(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            int ReportPk = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
            BindBunkeringChildData(ReportPk);
        }
        catch(Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }

    }
    //protected void ImportData(string SchemaFile,string DataFile)
    //{
    //    string sqlConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString().Replace("Master", "eMANAGER");
    //    SqlTransaction trans;
    //    SqlConnection Con = new SqlConnection(sqlConnectionString);
    //    Con.Open();
    //    trans = Con.BeginTransaction();
    //    DataSet ds=new DataSet();
    //    ds.ReadXmlSchema(SchemaFile);
    //    ds.ReadXml(DataFile);
    //    ResetNULLDates(ref ds);

    //    int CId = Convert.ToInt32(ds.Tables["Cir_Circular"].Rows[0]["CId"]);

    //    try
    //    {

    //    SqlCommand cmd = new SqlCommand("", Con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Transaction = trans;
    //    //------------------------------------------------      
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    //------------------------------------------------      
    //    if (ds.Tables["Cir_Circular"].Rows.Count > 0)
    //    {
    //        foreach (DataRow dr in ds.Tables["Cir_Circular"].Rows)
    //        {
    //            //-------------------------------
    //            string[] CommandParameters = getCommandParameters(cmd, "CIR_InsertUpdateCircular_VSL");
    //            cmd.Parameters.Clear();
    //            cmd.CommandText = "CIR_InsertUpdateCircular_VSL";
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            for (int i = 0; i <= CommandParameters.Length - 1; i++)
    //            {
    //                object data = DBNull.Value;
    //                try
    //                { data = dr[CommandParameters[i]]; }
    //                catch { data = DBNull.Value; }
    //                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
    //            }
    //            //------------------------------
    //            int result = cmd.ExecuteNonQuery();
    //        }
    //    }

    //    trans.Commit();
    //    ProjectCommon.ShowMessage("Circular imported successfully.");
    //    Bindgrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        trans.Rollback();
    //        ProjectCommon.ShowMessage("Unable to import circular.");
    //    }
    //    finally
    //    {
    //        if (Con.State == ConnectionState.Open) { Con.Close(); }
    //    }
    //}
    //protected string[] getCommandParameters(SqlCommand cmd, string ProcName)
    //{
    //    string[] result;
    //    cmd.Parameters.Clear();
    //    cmd.CommandType = CommandType.Text;
    //    //---------------- CHECKING PACKET RECEIVED NO. IS GREATER THAN DB PACKET RECEIVED
    //    cmd.CommandText = "select replace(parameter_name,'@','') as parameter_name from information_schema.parameters where specific_name='" + ProcName + "' and ltrim(rtrim(parameter_name))<>''";
    //    DataTable dt = new DataTable();
    //    SqlDataAdapter adp = new SqlDataAdapter();
    //    adp.SelectCommand = cmd;
    //    adp.Fill(dt);
    //    result = new string[dt.Rows.Count];
    //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
    //    {
    //        result[i] = dt.Rows[i][0].ToString();
    //    }
    //    return result;
    //}
    //private void ResetNULLDates(ref DataSet ds_IN)
    //{
    //    DateTime dt_ref = new DateTime(1900, 1, 1);
    //    foreach (DataTable dt in ds_IN.Tables)
    //    {
    //        List<String> DateTimeCols = new List<String>();
    //        foreach (DataColumn dc in dt.Columns)
    //        {
    //            if (dc.DataType == System.Type.GetType("System.DateTime"))
    //            {
    //                DateTimeCols.Add(dc.ColumnName);
    //            }
    //        }
    //        if (DateTimeCols.Count > 0 && dt.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                foreach (string cName in DateTimeCols)
    //                {
    //                    if (!Convert.IsDBNull(dr[cName]))
    //                    {
    //                        DateTime dt_test = Convert.ToDateTime(dr[cName]);
    //                        if (dt_test <= dt_ref)
    //                        {
    //                            dr[cName] = DBNull.Value;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //protected void btnNewReport_Click(object sender, EventArgs e)
    //{
    //    //if (rptPR.Items.Count <= 0)
    //    //{
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "fas", "window.open('AddBunker.aspx','');", true);
    //   // }
    //   //else
    //   // {

    //   // }
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindgrid();
        rptBankingChildData.DataSource = null;
        rptBankingChildData.DataBind();
        rptBankingChildData.Visible = false;
    }
    protected void Bindgrid()
    {
        string Filter = " ";
        if (ddlType.SelectedIndex != 0)
        {
            Filter += " AND FuelType='" + ddlType.SelectedValue + "'";
        }
        if (txtVoyageNo.Text.Trim() != "")
        {
            Filter += " AND VoyageNumber LIKE '%" + txtVoyageNo.Text.Trim() + "%'";
        }

        if (txtFDate.Text.Trim() != "")
        {
            Filter += " AND LocalDate >='" + txtFDate.Text.Trim() + "'";
        }
        if (txtTDate.Text.Trim() != "")
        {
            Filter += " AND LocalDate <='" + txtTDate.Text.Trim() + "'";
        }

        if (ddlLocation.SelectedIndex != 0)
        {
                Filter += " AND Location = '"+ ddlLocation.SelectedValue + "'";
        }

        string SQL = "Select * from ( SELECT  'rowstyle' As RowCls,ReportPk,VoyageNumber, LocalDate, Port, ISNULL(Locked,'N') As isLocked,VesselId, FuelType,ROW_NUMBER() OVER(partition by ReportPk order by ReportPk) As Row_No FROM dbo.VSL_BunkerDetails with(nolock) WHERE 1=1 " + Filter + " Group by ReportPk,VoyageNumber, LocalDate, Port, ISNULL(Locked,'N'),VesselId,FuelType ) as a where a.Row_No = 1 ORDER BY ReportPk Desc ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptPR.DataSource = dt;
        rptPR.DataBind();
    }
    private void BindBunkeringChildData(int Reportpk)
    {
        if (Reportpk > 0)
        {
            string Filter = " ";
            if (ddlType.SelectedIndex != 0)
            {
                Filter += " AND FuelType='" + ddlType.SelectedValue + "'";
            }
            if (txtVoyageNo.Text.Trim() != "")
            {
                Filter += " AND VoyageNumber LIKE '%" + txtVoyageNo.Text.Trim() + "%'";
            }

            if (txtFDate.Text.Trim() != "")
            {
                Filter += " AND LocalDate >='" + txtFDate.Text.Trim() + "'";
            }
            if (txtTDate.Text.Trim() != "")
            {
                Filter += " AND LocalDate <='" + txtTDate.Text.Trim() + "'";
            }

            if (ddlLocation.SelectedIndex != 0)
            {
                Filter += " AND Location = '" + ddlLocation.SelectedValue + "'";
            }
            string SQL = "SELECT ReportPk,VesselId,FuelType,BunkerReceivedACC,ActualBunkerReceived FROM dbo.VSL_BunkerDetails with(nolock) WHERE 1=1 and ReportPk = " + Reportpk + " " + Filter + " ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count > 0)
            {
                rptBankingChildData.DataSource = dt;
                rptBankingChildData.DataBind();
                rptBankingChildData.Visible = true;
            }
            else
            {
                rptBankingChildData.DataSource = null;
                rptBankingChildData.DataBind();
                rptBankingChildData.Visible = false;
            }
        }
        else
        {
            rptBankingChildData.DataSource = null;
            rptBankingChildData.DataBind();
            rptBankingChildData.Visible = false;
        }

    }

    //public string GetLongitude(object i)
    //{
    //    try
    //    {
    //        int Lat = Convert.ToInt32(i);
    //        if (Lat == 1)
    //        {
    //            return "E";
    //        }
    //        else if (Lat == 2)
    //        {
    //            return "W";
    //        }
    //        else
    //        {
    //            return "";
    //        }
    //    }
    //    catch
    //    {
    //        return "";
    //    }
    //}
    //public string GetLattitude(object i)
    //{
    //    try
    //    {
    //        int Lon = Convert.ToInt32(i);
    //        if (Lon == 1)
    //        {
    //            return "N";
    //        }
    //        else if (Lon == 2)
    //        {
    //            return "S";
    //        }
    //        else
    //        {
    //            return "";
    //        }
    //    }
    //    catch
    //    {
    //        return "";
    //    }
    //}
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //int ReportsPK = Common.CastAsInt32(((ImageButton)sender).CommandArgument.Split('~').GetValue(0));
        //string VesselId = ((ImageButton)sender).CommandArgument.Split('~').GetValue(1).ToString();
        //string ReportType = ((ImageButton)sender).CommandArgument.Split('~').GetValue(2).ToString();
        //string voyNo = ((ImageButton)sender).Attributes["voyNo"].ToString();

        ////string TableName = "";

        //string Type = "";

        //switch (ReportType.Trim())
        //{
        //    case "A":
        //        Type = "Arrival";
        //        break;
        //    case "D":
        //        Type = "Departure";
        //        break;
        //    case "N":
        //        Type = "Noon";
        //        break;
        //    case "PA":
        //        Type = "Port-Anchorage";
        //        break;
        //    case "PB":
        //        Type = "Port-Berthing";
        //        break;
        //    case "PD":
        //        Type = "Port-Drift";
        //        break;
        //    case "SH":
        //        Type = "Shifting";
        //        break;
        //    default:
        //        Type = "";
        //        break;

        //}
       

        //try
        //{
        //    Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
        //    Common.Set_ParameterLength(5);
        //    Common.Set_Parameters(
        //        new MyParameter("@VesselCode", CurrentVessel),
        //        new MyParameter("@RecordType", "Position Report - ( " + Type + " )"),
        //        new MyParameter("@RecordId", ReportsPK),
        //        new MyParameter("@RecordNo", voyNo),
        //        new MyParameter("@CreatedBy", Session["FullUserName"].ToString().Trim())
        //    );

        //    DataSet ds = new DataSet();
        //    ds.Clear();
        //    Boolean res;
        //    res = Common.Execute_Procedures_IUD(ds);
        //    if (res)
        //    {
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('" + ds.Tables[0].Rows[0][0].ToString() + "');", true);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Sent for export successfully.');", true);
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + Common.getLastError() + "');", true);

        //    }
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + ex.Message + "');", true);
        //}
        
        ////DataSet ds = new DataSet();
        ////string SQL = "SELECT * FROM VSL_VPRNoonReport WHERE ReportsPK=" + ReportsPK + " AND VesselID='" + VesselId + "' ";
        ////DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        ////dt.TableName = TableName;
        ////ds.Tables.Add(dt.Copy());

        ////string SchemaFile = Server.MapPath("TEMP/PREP_Schema.xml"); 
        ////string DataFile = Server.MapPath("TEMP/PREP_Data.xml");
        ////string ZipFile = Server.MapPath("TEMP/PREP_" + VesselId + "_" + ReportType + "_" + ReportsPK.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyyy").Replace(":", "") + ".zip");
        
        ////ds.WriteXmlSchema(SchemaFile);
        ////ds.WriteXml(DataFile);

        //////btnSearch.Text = ZipFile;

        ////using (ZipFile zip = new ZipFile())
        ////{
        ////    zip.AddFile(SchemaFile);
        ////    zip.AddFile(DataFile);
        ////    zip.Save(ZipFile);
        ////}

        ////byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
        ////Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
        ////Response.BinaryWrite(buff);
        ////Response.Flush();
        ////Response.End();

    }
   
   

    
    
   


    //protected void lnkSummaryReport_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "fas", "window.open('PositionSummaryReport.aspx','');", true);
    //}


    protected string GetLocationText(object location)
    {
        if (location != null)
        {
            int locationValue = Convert.ToInt32(location);
            switch (locationValue)
            {
                
                case 1:
                    return "In Port";
                case 2:
                    return "At Anchorage";
                default:
                    return "Unknown";
            }
        }
        return "Unknown";
    }
}
