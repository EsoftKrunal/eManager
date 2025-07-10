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

public partial class PositionReport : System.Web.UI.Page
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
            CurrentVessel = Session["CurrentShip"].ToString();
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Session["UserName"].ToString();

            txtFDate.Text = "01-Jan-" + DateTime.Today.Year;
            txtTDate.Text = "31-Dec-" + DateTime.Today.Year;
            
            Bindgrid();
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
        int ReportId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.Split('~').GetValue(0));
        string VesselId = ((ImageButton)sender).CommandArgument.Split('~').GetValue(1).ToString();
        //string ReportType = ((ImageButton)sender).CommandArgument.Split('~').GetValue(2).ToString();

        string ReportPage = "";
       
        //switch(ReportType.Trim())
        //{
        //    case "A": 
        //        ReportPage = "ArrivalReport.aspx";
        //        break;
        //    case "D":
        //        ReportPage = "DepartureReport.aspx";
        //        break;
        //    case "N":
        //        ReportPage = "NoonReport.aspx";
        //        break;
        //    case "PA":
        //        ReportPage = "PortAnchoringReport.aspx";
        //        break;
        //    case "PB":
        //        ReportPage = "PortBerthingReport.aspx";
        //        break;
        //    case "PD":
        //        ReportPage = "PortDriftReport.aspx";
        //        break;
        //    default : 
        //        ReportPage = "";
        //        break;

        //}
        ReportPage = "NoonReport.aspx";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Show Report", "window.open('" + ReportPage + "?Key=" + ReportId + "', '_blank', '');", true);

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

    protected void btnNewReport_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fas", "window.open('NoonReport.aspx','');", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void Bindgrid()
    {
        string Filter = " ";
        if (ddlReportType.SelectedIndex != 0)
        {
            Filter += " AND ACTIVITY_CODE='" + ddlReportType.SelectedValue + "'";
        }
        if (txtVoyageNo.Text.Trim() != "")
        {
            Filter += " AND VoyageNo LIKE '%" + txtVoyageNo.Text.Trim() + "%'";
        }

        if (ddlLocation.SelectedIndex != 0)
        {
            if (ddlLocation.SelectedValue == "A")
            {
                Filter += " AND ACTIVITY_CODE = 'N'";
            }

            if (ddlLocation.SelectedValue == "I")
            {
                Filter += " AND ReportTypeCode <> 'N'";
            }
        }

        string SQL = "SELECT * FROM VW_VSL_VPRNoonReport WHERE VesselId = '" + CurrentVessel + "' AND ReportDate >='" + txtFDate.Text + "' AND ReportDate <='" + txtTDate.Text + "' " + Filter;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + " ORDER BY ReportsPK Desc ");
        rptPR.DataSource = dt;
        rptPR.DataBind();

        //------------------- bind summary

        string SQL1 = "SELECT SUM(RECIFO45) AS RECIFO45,SUM(RECMGO1) AS RECMGO1,SUM(RECMDO) AS RECMDO,SUM(RECMECC) AS RECMECC,SUM(RECMECYL) AS RECMECYL,SUM(RECAECC) AS RECAECC,SUM(RECHYD) AS RECHYD,SUM(RECFRESHWATER) as RECFRESHWATER,SUM(MEIFO45+AEIFO45+CargoHeatingIFO45) as IFOTOAL,SUM(MEMGO1+AEMGO1+CargoHeatingMGO1) AS MGOTOTAL,SUM(MEMDO+AEMDO+CargoHeatingMDO) AS MDOTOTAL,SUM(MECCLube) AS SUMLUBE1,SUM(MECYLLube) AS SUMLUBE2,SUM(AECCLube) AS SUMLUBE3,SUM(HYDLube) AS SUMLUBE4,SUM(ConsumedFreshWater) SUMWATER FROM VW_VSL_VPRNoonReport WHERE VesselId = '" + CurrentVessel + "' AND ReportDate >='" + txtFDate.Text + "' AND ReportDate <='" + txtTDate.Text + "' " + Filter;
        DataTable dtSum = Common.Execute_Procedures_Select_ByQuery(SQL1);
        //----------------------------------------------------

        lblIFORec.Text = dtSum.Rows[0]["RECIFO45"].ToString();
        lblMGORec.Text = dtSum.Rows[0]["RECMGO1"].ToString();
        lblMDORec.Text = dtSum.Rows[0]["RECMDO"].ToString();
        



        lblIFORec.Text = dtSum.Rows[0]["RECMECC"].ToString();
        lblMGORec.Text = dtSum.Rows[0]["RECMECYL"].ToString();
        lblMDORec.Text = dtSum.Rows[0]["RECAECC"].ToString();
        lblMDORec.Text = dtSum.Rows[0]["RECHYD"].ToString();

        lblFWRec.Text = dtSum.Rows[0]["RECFRESHWATER"].ToString();

        //----------------------------------------------------

        lblIFOTotal.Text = dtSum.Rows[0]["IFOTOAL"].ToString();
        //lblIFOTotal.Text = dt.Compute("SUM(MEIFO1+AEIFO1+CargoHeatingIFO1)", "").ToString();
        //lblMGOTotal.Text = dt.Compute("SUM(MEMGO5+AEMGO5+CargoHeatingMGO5)", "").ToString();
        lblMGOTotal.Text = dtSum.Rows[0]["MGOTOTAL"].ToString();
        lblMDOTotal.Text = dtSum.Rows[0]["MDOTOTAL"].ToString();


        lblMECCTotal.Text = dtSum.Rows[0]["SUMLUBE1"].ToString();
        lblMECYLTotal.Text = dtSum.Rows[0]["SUMLUBE2"].ToString();
        lblAECCTotal.Text = dtSum.Rows[0]["SUMLUBE3"].ToString();
        lblHYDTotal.Text = dtSum.Rows[0]["SUMLUBE4"].ToString();

        lblFreshWaterTotal.Text = dtSum.Rows[0]["SUMWATER"].ToString();

        //----------------------------------------------------
        //string SQLROB = "SELECT TOP 1 * FROM VW_VSL_VPRNoonReport WHERE VesselId = '" + CurrentVessel + "' AND ReportDate <='" + txtTDate.Text + "' " + Filter;
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQLROB + " ORDER BY ReportsPK Desc ");
        if (dt.Rows.Count > 0)
        {
            lblROBIFOTotal.Text = dt.Rows[0]["ROBIFO45Fuel"].ToString();
            //lblIFOTotal.Text = dt.Compute("SUM(MEIFO1+AEIFO1+CargoHeatingIFO1)", "").ToString();
            //lblMGOTotal.Text = dt.Compute("SUM(MEMGO5+AEMGO5+CargoHeatingMGO5)", "").ToString();
            lblROBMGOTotal.Text = dt.Rows[0]["ROBMGO1Fuel"].ToString();
            lblROBMDOTotal.Text = dt.Rows[0]["ROBMDOFuel"].ToString();


            lblROBMECCTotal.Text = dt.Rows[0]["ROBIFO45Lube"].ToString();
            lblROBMECYLTotal.Text = dt.Rows[0]["ROBIFO1Lube"].ToString();
            lblROBAECCTotal.Text = dt.Rows[0]["ROBMGO5Lube"].ToString();
            lblROBHYDTotal.Text = dt.Rows[0]["ROBMGO1Lube"].ToString();

            lblROBFreshWaterTotal.Text = dt.Rows[dt.Rows.Count - 1]["ROBIFO45FreshWater"].ToString();
        }
        //----------------------------------------------------
    }
   
    public string GetLongitude(object i)
    {
        try
        {
            int Lat = Convert.ToInt32(i);
            if (Lat == 1)
            {
                return "E";
            }
            else if (Lat == 2)
            {
                return "W";
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }
    public string GetLattitude(object i)
    {
        try
        {
            int Lon = Convert.ToInt32(i);
            if (Lon == 1)
            {
                return "N";
            }
            else if (Lon == 2)
            {
                return "S";
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int ReportsPK = Common.CastAsInt32(((ImageButton)sender).CommandArgument.Split('~').GetValue(0));
        string VesselId = ((ImageButton)sender).CommandArgument.Split('~').GetValue(1).ToString();
        string ReportType = ((ImageButton)sender).CommandArgument.Split('~').GetValue(2).ToString();
        string voyNo = ((ImageButton)sender).Attributes["voyNo"].ToString();

        //string TableName = "";

        string Type = "";

        switch (ReportType.Trim())
        {
            case "A":
                Type = "Arrival";
                break;
            case "D":
                Type = "Departure";
                break;
            case "N":
                Type = "Noon";
                break;
            case "PA":
                Type = "Port-Anchorage";
                break;
            case "PB":
                Type = "Port-Berthing";
                break;
            case "PD":
                Type = "Port-Drift";
                break;
            case "SH":
                Type = "Shifting";
                break;
            default:
                Type = "";
                break;

        }
       

        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", CurrentVessel),
                new MyParameter("@RecordType", "Position Report - ( " + Type + " )"),
                new MyParameter("@RecordId", ReportsPK),
                new MyParameter("@RecordNo", voyNo),
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
        
        //DataSet ds = new DataSet();
        //string SQL = "SELECT * FROM VSL_VPRNoonReport WHERE ReportsPK=" + ReportsPK + " AND VesselID='" + VesselId + "' ";
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //dt.TableName = TableName;
        //ds.Tables.Add(dt.Copy());

        //string SchemaFile = Server.MapPath("TEMP/PREP_Schema.xml"); 
        //string DataFile = Server.MapPath("TEMP/PREP_Data.xml");
        //string ZipFile = Server.MapPath("TEMP/PREP_" + VesselId + "_" + ReportType + "_" + ReportsPK.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyyy").Replace(":", "") + ".zip");
        
        //ds.WriteXmlSchema(SchemaFile);
        //ds.WriteXml(DataFile);

        ////btnSearch.Text = ZipFile;

        //using (ZipFile zip = new ZipFile())
        //{
        //    zip.AddFile(SchemaFile);
        //    zip.AddFile(DataFile);
        //    zip.Save(ZipFile);
        //}

        //byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
        //Response.BinaryWrite(buff);
        //Response.Flush();
        //Response.End();

    }
}
