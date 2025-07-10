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

public partial class SCMLIST : System.Web.UI.Page
{
    public int ReportsPk
    {
        get { return Common.CastAsInt32(ViewState["ReportsPk"]); }
        set { ViewState["ReportsPk"] = value; }
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
            Bindgrid();
        }
    }
    //protected void btnDownloadFile_Click(object sender, EventArgs e)
    //{
    //    int LFIId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.LFI WHERE LFIId=" + LFIId.ToString());
    //    if (dt.Rows.Count > 0)
    //    {
    //        string FileName = dt.Rows[0]["AttachmentFileName"].ToString();
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
    //    Mail_LFIId= Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    dv_Closure.Visible = true;
    //}
    protected void btnExport_Click(object sender, EventArgs e)
    {
        int ReportsPk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", CurrentVessel),
                new MyParameter("@RecordType", "SCM"),
                new MyParameter("@RecordId", ReportsPk),
                new MyParameter("@RecordNo", ""),
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
        //string SQL = "SELECT * FROM DBO.SCM_Master WHERE ReportsPK = " + ReportsPk + " AND VesselCode='" + CurrentVessel + "'";
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //dt.TableName = "SCM_Master";
        //ds.Tables.Add(dt.Copy());

        //SQL = "SELECT * FROM DBO.SCM_RANKDETAILS WHERE ReportsPk=" + ReportsPk + " AND VesselCode='" + CurrentVessel + "'";
        //dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //dt.TableName = "SCM_RANKDETAILS";
        //ds.Tables.Add(dt.Copy());

        //SQL = "SELECT * FROM [dbo].[SCM_NCRDETAILS] WHERE [ReportsPK] = " + ReportsPk + "  AND [VesselCode] = '" + CurrentVessel + "' ";
        //dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //dt.TableName = "SCM_NCRDETAILS";
        //ds.Tables.Add(dt.Copy());
        
        //string SchemaFile = Server.MapPath("~/VIMS/TEMP/SCMSchema.xml");
        //string DataFile = Server.MapPath("~/VIMS/TEMP/SCMData.xml");
        //string ZipFile = Server.MapPath("~/VIMS/TEMP/SCM_S_" + CurrentVessel + "_" + ReportsPk + ".zip");
        
        //ds.WriteXmlSchema(SchemaFile);
        //ds.WriteXml(DataFile);
        
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

    //protected void btnClosureSave_Click(object sender, EventArgs e)
    //{

    //    int discussed = Common.CastAsInt32(rbtn1.SelectedValue);
    //    int noticed = Common.CastAsInt32(rbtn2.SelectedValue);
    //    if (discussed == 0 || noticed == 0)
    //    {
    //        lblmess.Visible = true;
    //        return;
    //    }

    //    Common.Execute_Procedures_Select_ByQuery("exec InsertUpdateLFI_Vessel_Notifications_VSL " + Mail_LFIId.ToString() + ",'" + CurrentVessel + "','" + DateTime.Today.ToString("dd-MMM-yyyy hh:ss tt") + "',''," + discussed + "," + noticed + "");
    //    Bindgrid();
    //    dv_Closure.Visible = false;
    //}
    //protected void btnClosureCancel_Click(object sender, EventArgs e)
    //{
    //    dv_Closure.Visible = false;
    //}

    protected void btnSaveImport_Click(object sender, EventArgs e)
    {
        if (flp_Upload.HasFile)
        {
            string TargetDir = Server.MapPath("~/VIMS/TEMP/");
            string SchemaFile = TargetDir + "SCMSchema.xml";
            string DataFile = TargetDir + "SCMData.xml";


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
                Bindgrid();
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

    protected void Filter_LFI(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void ImportData(string SchemaFile, string DataFile)
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString().Replace("Master", "eMANAGER");
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString);
        Con.Open();
        trans = Con.BeginTransaction();
        DataSet ds = new DataSet();
        ds.ReadXmlSchema(SchemaFile);
        ds.ReadXml(DataFile);
        ResetNULLDates(ref ds);

        
        try
        {

            SqlCommand cmd = new SqlCommand("", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            //------------------------------------------------      
            cmd.CommandType = CommandType.StoredProcedure;
            //------------------------------------------------      
            if (ds.Tables["SCM_MASTER"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["SCM_MASTER"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "SCM_IMPORT_SCM_MASTER");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SCM_IMPORT_SCM_MASTER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i <= CommandParameters.Length - 1; i++)
                    {
                        object data = DBNull.Value;
                        try
                        {
                            if (i == 0)
                            {
                                data = "S";
                            }
                            else
                            {
                                data = dr[CommandParameters[i]];
                            }
                        }
                        catch { data = DBNull.Value; }
                        cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                    }
                    //------------------------------
                    int result = cmd.ExecuteNonQuery();
                }
            }

            trans.Commit();
            ProjectCommon.ShowMessage("SCM imported successfully.");
            Bindgrid();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            ProjectCommon.ShowMessage("Unable to import SCM.");
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
    
    protected void Bindgrid()
    {
        string SQL = "select [ReportsPK],[VesselCode],replace(convert(varchar, SDate,106),' ','-') AS SDate, SDate AS SDate1, [ShipPosFrom],[ShipPosTo],[CommTime],[CompTime], " +
        "(case Ocassion when 'M' then 'Monthly' when 'N' then 'NON-Routine' when 'S' then 'SUPTD Visit' else '' end ) AS [Ocassion], [Ocassion] AS [Ocassion1],  " +
        "(Case ShipPosition when 'S' then 'At Sea' else 'In Port' end) AS [ShipPosition], UpdatedBy , " +
        "(CASE WHEN SM.[Ocassion] = 'S' THEN  (SELECT  top 1 Name  FROM [dbo].[SCM_RANKDETAILS] RD WHERE RD.RankName ='SUPERINTENDENT' AND RD.ReportsPK = SM.ReportsPK AND RD.VesselCode='" + CurrentVessel + "' AND Absent=0)  ELSE  (SELECT  top 1 Name FROM [dbo].[SCM_RANKDETAILS] RD WHERE RD.RankName ='MASTER' AND Remarks='Chairman' AND RD.ReportsPK = SM.ReportsPK AND RD.VesselCode='" + CurrentVessel + "' AND Absent=0) END) " +
        "AS MASTER  " +
        "from SCM_Master SM  where VesselCode='" + CurrentVessel + "' ORDER BY SDate1 DESC ";

         //string SQL = "select [ReportsPK],[VesselCode],replace(convert(varchar, SDate,106),' ','-') AS SDate,[ShipPosFrom],[ShipPosTo],[CommTime],[CompTime], " +
         //            "(case Ocassion when 'M' then 'Monthly' when 'N' then 'NON-Routine' when 'S' then 'SUPTD Visit' else '' end ) AS [Ocassion], [Ocassion] AS [Ocassion1], (Case ShipPosition when 'S' then 'At Sea' else 'In Port' end) AS [ShipPosition], UpdatedBy, " +
         //            "(SELECT Name  FROM [dbo].[SCM_RANKDETAILS] RD WHERE RD.RankName = (CASE WHEN SM.[Ocassion] = 'S' THEN 'SUPERINTENDENT' ELSE  'MASTER' END) AND RD.ReportsPK = SM.ReportsPK AND RD.VesselCode='" + CurrentVessel + "') AS MASTER " +
         //            "from SCM_Master SM  where VesselCode='" + CurrentVessel + "' ORDER BY SDate DESC";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
       
        if (dt != null)
        {
            rptSCMLIST.DataSource = dt;
            rptSCMLIST.DataBind();      
        }
        else
        {
            rptSCMLIST.DataSource = null;
            rptSCMLIST.DataBind();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        string CA = ((ImageButton)sender).CommandArgument;

        int ReportsPk = Common.CastAsInt32(CA.Split('~').GetValue(0));
        string Occasion = CA.Split('~').GetValue(1).ToString().Trim();

        if (Occasion != "S")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "View", "OpenSCMWindow(" + ReportsPk + ", 'V');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "View", "window.open('SCM_SupdtVisit.aspx?Mode=V&pk=" + ReportsPk + "' , '');", true);
        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string CA = ((ImageButton)sender).CommandArgument;

        int ReportsPk = Common.CastAsInt32(CA.Split('~').GetValue(0));
        string Occasion = CA.Split('~').GetValue(1).ToString().Trim();

        if (Occasion != "S")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "OpenSCMWindow(" + ReportsPk + ", 'E');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "window.open('SCM_SupdtVisit.aspx?Mode=E&pk=" + ReportsPk + "' , '');", true);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        int ReportsPk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "window.open('../Reports/SCM.aspx?pk=" + ReportsPk + "' , '');", true);
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ddlOccasion.SelectedIndex = 0;
        dvAddNew.Visible = true;
    }

    protected void btnAddNewSCM_Click(object sender, EventArgs e)
    {
        if (ddlOccasion.SelectedValue == "M" || ddlOccasion.SelectedValue == "N")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Add", "AddNewSCM('" + ddlOccasion.SelectedValue.Trim() + "');", true);
        }

        if (ddlOccasion.SelectedValue == "S")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Add", "window.open('SCM_SupdtVisit.aspx?Mode=A', '');", true);
        }

        btnClose_Click(sender, e);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        //Bindgrid();
        dvAddNew.Visible = false;
    }
    
}
