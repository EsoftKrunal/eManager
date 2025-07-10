using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using Ionic.Zip;
using System.Collections.Generic;

public partial class Vetting_VIQPreparation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnShow_Click(sender, e);
        }
    }
    protected void grp_OnCheckedChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    public void BindGrid()
    {
        int Status= 0;
        string whereclause = "";
        
        if (radOpen.Checked)
            Status = 0;
        else if (radClosed.Checked)
            Status = 1;
        else if (radAll.Checked)
            Status = -1;

        string sql = "SELECT *,((DONEQ*100)/(CASE WHEN NOQ=0 THEN 1 ELSE NOQ END)) AS PERDONE FROM ( " +
                     "SELECT Row_Number() Over(ORDER BY v.SHIPNAME,VIQID) AS SNO,VM.*,SHIPNAME AS VESSELNAME, " +
                     "(SELECT COUNT(*) FROM [dbo].[vw_VIQQuestionRanks] D WHERE D.VESSELCODE=VM.VESSELCODE AND D.VIQID=VM.VIQID ) AS NOQ , " +
                     "(SELECT COUNT(*) FROM [dbo].[vw_VIQQuestionRanks] D WHERE D.VESSELCODE=VM.VESSELCODE AND D.VIQID=VM.VIQID AND ISNULL(OfficeClosureStatus,0)>0) AS DONEQ  " +
                     "FROM DBO.VIQ_VIQMaster VM INNER JOIN SETTINGS V ON V.SHIPCODE=VM.VESSELCODE WHERE V.SHIPCODE='" + Session["CurrentShip"].ToString() + "' " +
                     " ) A ";
        if (Status >= 0)
        {
            whereclause += " WHERE VIQStatus=" + Status;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql + whereclause + " ORDER BY VESSELNAME,VIQID ");
        rpt_Questions.DataSource = dt;
        rpt_Questions.DataBind();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {   
        string TargetDir = Server.MapPath("~/VIMS/TEMP");
        string SchemaFile = TargetDir + "/VETTING_SCHEMA.XML";
        string DataFile = TargetDir + "/VETTING_DATA.XML";
        
        if (File.Exists(SchemaFile))
            File.Delete(SchemaFile);
        if (File.Exists(DataFile))
            File.Delete(DataFile);

        if (flpImport.HasFile)
        {
            using (ZipFile zip = ZipFile.Read(flpImport.PostedFile.InputStream))
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
            ImportData(SchemaFile,DataFile);
        }
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
    protected void ImportData(string SchemaFile, string DataFile)
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString().Replace("Master", "eMANAGER");
        SqlTransaction trans;
        SqlConnection Con = new SqlConnection(sqlConnectionString);
        DataSet ds = new DataSet();
        SqlCommand cmdCheck = new SqlCommand("", Con);
        SqlDataAdapter Sda = new SqlDataAdapter(cmdCheck);

        Con.Open();
        trans = Con.BeginTransaction();
        ds.ReadXmlSchema(SchemaFile);
        ds.ReadXml(DataFile);
        ResetNULLDates(ref ds);

        string VesselCode = ds.Tables["VIQ_VIQMaster"].Rows[0]["VesselCode"].ToString();
        int VIQId = Convert.ToInt32(ds.Tables["VIQ_VIQMaster"].Rows[0]["VIQId"]);
                    
        cmdCheck.Transaction = trans;
        cmdCheck.CommandType = CommandType.Text;

        //---------------- CHECKING PACKET IS FOR SAME VESSEL OR NOT

        DataSet DsTemp = new DataSet();
        cmdCheck.CommandText = "SELECT * FROM DBO.SETTINGS";
        Sda.Fill(DsTemp, "Set");

        //if (DsTemp.Tables[0].Rows.Count != 1)
        //{
        //    ProjectCommon.ShowMessage("Settings table data is invalid.");
        //    return;
        //}
        //else
        //{
        //    string In_VesselCode = DsTemp.Tables[0].Rows[0]["SHIPCODE"].ToString();
        //    if (VesselCode != In_VesselCode)
        //    {
        //        ProjectCommon.ShowMessage("Importing packet's VESSEL is not matching with VESSEL.");
        //        return;
        //    }
        //}

        ////------------------------ 

        try
        {
            SqlCommand cmd = new SqlCommand("", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            //------------------------------------------------      
            cmd.CommandType = CommandType.StoredProcedure;
            //------------------------------------------------      
            if (ds.Tables["VIQ_VIQMaster"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["VIQ_VIQMaster"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "VIQ_VSL_IMPORT_VIQMaster");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "VIQ_VSL_IMPORT_VIQMaster";
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
            //------------------------------------------------      
            if (ds.Tables["VIQ_VIQDetails"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["VIQ_VIQDetails"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "VIQ_VSL_IMPORT_VIQDetails");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "VIQ_VSL_IMPORT_VIQDetails";
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
            //------------------------------------------------      
            if (ds.Tables["VIQ_VIQDetailsRanks"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["VIQ_VIQDetailsRanks"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "VIQ_VSL_IMPORT_VIQDetailsRanks");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "VIQ_VSL_IMPORT_VIQDetailsRanks";
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
            //------------------------------------------------      

            trans.Commit();
            ProjectCommon.ShowMessage("VIQ imported successfully.");
        }
        catch (Exception ex)
        {
            trans.Rollback();
            ProjectCommon.ShowMessage("Unable to import VIQ data.");
        }
        finally
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
        }
    }
}
