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

public partial class Drill_Home : System.Web.UI.Page
{
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
    public string RecordType
    {
        get { return ViewState["RecordType"].ToString(); }
        set { ViewState["RecordType"] = value; }
    }
    public int DTId
    {
        get { return Common.CastAsInt32(ViewState["DTId"]); }
        set { ViewState["DTId"] = value; }
    }
    protected void radioButton_OnCheckedChanged(object sender, EventArgs e)
    {
        if (r1.Checked)
            Response.Redirect("DrillPlanner.aspx");
        else if (r2.Checked)
            Response.Redirect("Home.aspx");
        else if (r3.Checked)
            Response.Redirect("DrillPlannerW.aspx");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
      
        if (!IsPostBack)
        {
            CurrentVessel = Session["CurrentShip"].ToString();            
            Bindgrid();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
     
        //txtFromDt.Text = "";
        //txtToDt.Text = "";
        //txtDays.Text = "";
        rad_D.Checked = false;
        rad_T.Checked = false;

        Bindgrid();
    } 
    protected void Bindgrid()
    {
        string SQL = "SELECT *,(select count(*) from [dbo].[DT_VSL_DrillTrainingsHistory] H WHERE H.VESSELCODE=DT.VESSELCODE AND H.DTID=DT.DTID AND H.DTRECORDTYPE=DT.RECORDTYPE AND OfficeRecOn IS NULL) AS COUNT_OP FROM [dbo].[vw_DrillTrainings] DT ";
        string Where = " WHERE 1=1 ";

        //if (txtFromDt.Text.Trim() != "")
        //{
        //    Where += " AND NextDueDate >= '" + txtFromDt.Text.Trim() + "' ";
        //}

        //if (txtToDt.Text.Trim() != "")
        //{
        //    Where += " AND NextDueDate <= '" + txtToDt.Text.Trim() + "' ";
        //}

        //if (txtDays.Text.Trim() != "")
        //{
        //    Where += " AND (DueINDays <= " + Common.CastAsInt32(txtDays.Text.Trim()) + " OR NextDueDate IS NULL) ";
        //}

        if (rad_T.Checked && !rad_D.Checked)
        {
            Where += " AND RecordType='T' ";
        }
        if (rad_D.Checked && !rad_T.Checked)
        {
            Where += " AND RecordType='D' ";
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " ORDER BY NextDueDate ");
        lblCount.Text= " ( " + dt.Rows.Count.ToString() + " ) Records Found.";
        rptSCMLIST.DataSource = dt;
        rptSCMLIST.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool Checked = false;

        foreach (RepeaterItem rpt in rptSCMLIST.Items)
        {
            CheckBox chkPlan = (CheckBox)rpt.FindControl("chkPlan");
            if (chkPlan.Checked)
            {
                Checked = true;
                break;
            }
        }

        if (!Checked)
        {
            lblMsg.Text = "Please select plan date to save planning.";
            return;
        }

        foreach (RepeaterItem rpt in rptSCMLIST.Items)
        {
            CheckBox chkPlan = (CheckBox)rpt.FindControl("chkPlan");
            if (chkPlan.Checked)
            {
                TextBox txtPlanDate = (TextBox)rpt.FindControl("txtPlanDate");

                if (txtPlanDate.Text.Trim() == "")
                {
                    lblMsg.Text = "Please enter plan date.";
                    txtPlanDate.Focus();
                    return;
                }

                DateTime dt;

                if (!DateTime.TryParse(txtPlanDate.Text.Trim(), out dt))
                {
                    lblMsg.Text = "Please enter valid plan date.";
                    txtPlanDate.Focus();
                    return;
                }
            }
        }

        foreach (RepeaterItem rpt in rptSCMLIST.Items)
        {
            CheckBox chkPlan = (CheckBox)rpt.FindControl("chkPlan");
            if (chkPlan.Checked)
            {
                int DTId = Common.CastAsInt32(chkPlan.Attributes["DTId"]);
                string RecordType = chkPlan.Attributes["RecordType"].ToString();
 
                TextBox txtPlanDate = (TextBox)rpt.FindControl("txtPlanDate");

                try
                {
                    Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[DT_VSL_DrillTrainings] SET PlanDate = '" + txtPlanDate.Text.Trim() + "' WHERE VesselCode='" + CurrentVessel + "' AND DTId= " + DTId + " AND RecordType = '" + RecordType + "' "); 
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Unable to save planning. Error: " + ex.Message;
                    return;
                }
            }
        }

        Bindgrid();
        lblMsg.Text = "Planning saved successfully.";

    }     

    //  Execute 
    protected void btnOpenExecute_Click(object sender, EventArgs e)
    {
        DTId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        RecordType = ((ImageButton)sender).Attributes["RecordType"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Execute", "OpenExecute(" + DTId + ",'" + RecordType + "');", true);
    }
    protected void btnClipText_Attachment_Click(object sender, EventArgs e)
    {
        DownloadFile();
    }
    protected void DownloadFile()
    {
        string TableName = (RecordType == "T" ? "[dbo].DT_TrainingMaster" : "[dbo].DT_DrillMaster");
        string SQL = "SELECT [AttachmentFileName],Attachment  FROM " + TableName + "  WHERE " + (RecordType == "T" ? "TrainingId" : "DrillId") + " = " + DTId;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

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

    // ----------------------------------


    // -------------- Import -------------------

    protected void btnImport_Click(object sender, EventArgs e)
    {
        dv_Import.Visible = true;
    }
    protected void btnSaveImport_Click(object sender, EventArgs e)
    {
        if (flp_Upload.HasFile)
        {
            string TargetDir = Server.MapPath("~/VIMS/TEMP/");
            string SchemaFile = "";
            string DataFile = "";
            string PacketType="";

            if (flp_Upload.PostedFile.FileName.Contains("DTM_" + CurrentVessel + "_O_ACK"))
            {
                SchemaFile = TargetDir + "DTM_Schema.xml";
                DataFile = TargetDir + "DTM_Data.xml";
                PacketType = "ACK";
            }
            else
            {
                SchemaFile = TargetDir + "SCHEMA_Matrix.xml";
                DataFile = TargetDir + "DATA_Matrix.xml";
                PacketType = "MAT";
            }

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
                ImportData(SchemaFile, DataFile, PacketType);
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
    protected void ImportData(string SchemaFile, string DataFile, string PacketType)
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

            if (PacketType=="MAT")
            {

                if (ds.Tables["DT_TrainingMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["DT_TrainingMaster"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_Import_TrainingMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_Import_TrainingMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                //if (i == 0)
                                //{
                                //    data = "S";
                                //}
                                //else
                                //{
                                data = dr[CommandParameters[i]];
                                //}
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["DT_DrillMaster"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["DT_DrillMaster"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_Import_DrillMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_Import_DrillMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = dr[CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

                if (ds.Tables["DT_MatrixMaster"].Rows.Count > 0)
                {
                    int MatrixId = Common.CastAsInt32(ds.Tables["DT_MatrixMaster"].Rows[0]["MatrixId"]);
                    //foreach (DataRow dr in ds.Tables["DT_MatrixMaster"].Rows)
                    {
                        //    //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_Import_MatrixMaster");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_Import_MatrixMaster";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = ds.Tables["DT_MatrixMaster"].Rows[0][CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }

                    if (ds.Tables["DT_MatrixDetails"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["DT_MatrixDetails"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "DT_Import_MatrixDetails");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "DT_Import_MatrixDetails";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                {
                                    data = dr[CommandParameters[i]];
                                }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.CommandText = "DELETE FROM [dbo].[DT_MatrixDetailsAttachments] WHERE [MatrixId]=" + MatrixId;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    if (ds.Tables["DT_MatrixDetailsAttachments"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["DT_MatrixDetailsAttachments"].Rows)
                        {
                            //-------------------------------
                            string[] CommandParameters = getCommandParameters(cmd, "DT_Import_MatrixDetailsAttachments");
                            cmd.Parameters.Clear();
                            cmd.CommandText = "DT_Import_MatrixDetailsAttachments";
                            cmd.CommandType = CommandType.StoredProcedure;
                            for (int i = 0; i <= CommandParameters.Length - 1; i++)
                            {
                                object data = DBNull.Value;
                                try
                                {
                                    data = dr[CommandParameters[i]];
                                }
                                catch { data = DBNull.Value; }
                                cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                            }
                            //------------------------------
                            int result = cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.Parameters.Clear();
                    cmd.CommandText = "DT_RESET_DRILL_TRAIINGS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("VSLCODE", CurrentVessel));
                    cmd.Parameters.Add(new SqlParameter("MATRIXID", MatrixId));
                    cmd.ExecuteNonQuery();

                }
            }

            if (PacketType == "ACK")
            {

                if (ds.Tables["DT_VSL_DrillTrainingsHistory"].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables["DT_VSL_DrillTrainingsHistory"].Rows)
                    {
                        //-------------------------------
                        string[] CommandParameters = getCommandParameters(cmd, "DT_Import_OfficeAck");
                        cmd.Parameters.Clear();
                        cmd.CommandText = "DT_Import_OfficeAck";
                        cmd.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i <= CommandParameters.Length - 1; i++)
                        {
                            object data = DBNull.Value;
                            try
                            {
                                data = dr[CommandParameters[i]];
                            }
                            catch { data = DBNull.Value; }
                            cmd.Parameters.Add(new SqlParameter(CommandParameters[i], data));
                        }
                        //------------------------------
                        int result = cmd.ExecuteNonQuery();
                    }
                }

            }

            trans.Commit();
            ProjectCommon.ShowMessage("Imported successfully.");
        }
        catch (Exception ex)
        {
            trans.Rollback();
            ProjectCommon.ShowMessage("Unable to import. Error : " + ex.Message);
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

    // ------------------- Show Office Guide Lines/ Attachment ------------
    protected void btnShowOGL_Click(object sender, EventArgs e)
    {
        DTId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        RecordType = ((ImageButton)sender).Attributes["RecordType"].ToString();

        string TableName = (RecordType == "T" ? "[dbo].DT_TrainingMaster" : "[dbo].DT_DrillMaster");         

        string SQL = "SELECT OfficeGuideLines  FROM " + TableName + "  WHERE " + (RecordType == "T" ? "TrainingId" : "DrillId") + " = " + DTId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt != null && dt.Rows.Count > 0)
        {
            txtShowOGL.Text = dt.Rows[0]["OfficeGuideLines"].ToString();
        }

        dv_OGL.Visible = true;

    }
    protected void btnCloseOGL_Click(object sender, EventArgs e)
    {
        DTId = 0;
        RecordType = "";
        txtShowOGL.Text = "";

        dv_OGL.Visible = false;
    }
    protected void btnShowAttachment_Click(object sender, EventArgs e)
    {
        DTId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        RecordType = ((ImageButton)sender).Attributes["RecordType"].ToString();
        DownloadFile();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string Where = " WHERE 1=1 ";

        //if (txtFromDt.Text.Trim() != "")
        //{
        //    Where += " AND NextDueDate >= '" + txtFromDt.Text.Trim() + "' ";
        //}

        //if (txtToDt.Text.Trim() != "")
        //{
        //    Where += " AND NextDueDate <= '" + txtToDt.Text.Trim() + "' ";
        //}

        //if (txtDays.Text.Trim() != "")
        //{
        //    Where += " AND (DueINDays <= " + Common.CastAsInt32(txtDays.Text.Trim()) + " OR NextDueDate IS NULL) ";
        //}

        if (rad_T.Checked && !rad_D.Checked)
        {
            Where += " AND RecordType='T' ";
        }
        if (rad_D.Checked && !rad_T.Checked)
        {
            Where += " AND RecordType='D' ";
        }

        Session["PrintWhere"] = Where;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "window.open('Print.aspx', '');", true);

    }
}
