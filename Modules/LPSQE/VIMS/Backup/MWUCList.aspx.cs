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

public partial class MWUCList : System.Web.UI.Page
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

            //txtFDate.Text = "01-Jan-" + DateTime.Today.Year;
            //txtTDate.Text = "31-Dec-" + DateTime.Today.Year;
            
            Bindgrid();
        }
    }
    

    protected void Filter_Cir(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Show Report", "window.open('MWUC_EntryMaster.aspx?Id=" + TableId + "', '_blank', '');", true);

    } 
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }
    
    protected void Bindgrid()
    {
        string Filter = " ";

        if (txtFDate.Text.Trim() != "")
        {
            Filter += " AND FORDATE >='" + txtFDate.Text + "'";
        }
        if (txtTDate.Text.Trim() != "")
        {
            Filter += " AND FORDATE <='" + txtTDate.Text + "'";
        }


        string SQL = "SELECT * FROM MWUC_MASTER WHERE VESSELCODE = '" + CurrentVessel + "' " + Filter;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + " ORDER BY FORDATE Desc ");

        if (dt != null && dt.Rows.Count > 0)
        {
            rptPR.DataSource = dt;
            rptPR.DataBind();
        }
        else
        {
            rptPR.DataSource = null;
            rptPR.DataBind();
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string MasterCrewNo = ((ImageButton)sender).CssClass;

        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", CurrentVessel),
                new MyParameter("@RecordType", "MWUC"),
                new MyParameter("@RecordId", TableId),
                new MyParameter("@RecordNo", MasterCrewNo),
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
        //string SQL = "SELECT * FROM MWUC_MASTER WHERE VESSELCODE = '" + CurrentVessel + "' AND TABLEID =" + TableId;
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //dt.TableName = "MWUC_MASTER";
        //ds.Tables.Add(dt.Copy());

        //SQL = "SELECT * FROM MWUC_DETAILS WHERE VESSELCODE = '" + CurrentVessel + "' AND TABLEID =" + TableId;
        //dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //dt.TableName = "MWUC_DETAILS";
        //ds.Tables.Add(dt.Copy());

        //SQL = "SELECT * FROM MWUC_CATMASTER WHERE VESSELCODE = '" + CurrentVessel + "' ";
        //dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //dt.TableName = "MWUC_CATMASTER";
        //ds.Tables.Add(dt.Copy());

        //string SchemaFile = Server.MapPath("TEMP/MWUC_Schema.xml"); 
        //string DataFile = Server.MapPath("TEMP/MWUC_Data.xml");
        //string ZipFile = Server.MapPath("TEMP/MWUC_" + CurrentVessel + "_S_"  + TableId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip");
        
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

    protected void btnImport_Click(object sender, EventArgs e)
    {
        dvImport.Visible = true;
    }
    protected void btnSaveImport_Click(object sender, EventArgs e)
    {
        if (flp_Upload.HasFile)
        {
            string FileName = Path.GetFileNameWithoutExtension(flp_Upload.PostedFile.FileName);
            if ((CurrentVessel != FileName.Split('_').GetValue(1).ToString().Trim()) || (FileName.Split('_').GetValue(2).ToString().Trim() != "O"))
            {
                lblMsgPOP.Text = "Please check! wrong packet selected.";
                return;
            }
            //else if (Convert.ToDateTime(FileName.Split('_').GetValue(4).ToString()).AddDays(7) < DateTime.Today.Date)
            //{
            //    lblMsgPOP.Text = "Please check! packet is expired.";
            //    return;
            //}
            else
            {
                
                string TargetDir = Server.MapPath("TEMP/");
                string SchemaFile = TargetDir + "MWUC_Schema.xml";
                string DataFile = TargetDir + "MWUC_Data.xml"; 

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
                }

            }
        }
        else
        {
            lblMsgPOP.Text = "Please select a file to import.";
            flp_Upload.Focus();
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Bindgrid();
        dvImport.Visible = false;
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
        //ResetNULLDates(ref ds);
               

        try
        {

            SqlCommand cmd = new SqlCommand("", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            //------------------------------------------------      
            cmd.CommandType = CommandType.StoredProcedure;
            //------------------------------------------------      
            if (ds.Tables["MWUC_MASTER"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["MWUC_MASTER"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "MWUC_ImportMaster");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "MWUC_ImportMaster";
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

            if (ds.Tables["MWUC_DETAILS"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["MWUC_DETAILS"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "MWUC_ImportDetails");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "MWUC_ImportDetails";
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
            lblMsgPOP.Text = "Imported successfully.";

        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMsgPOP.Text = "Unable to import. Error : " + ex.Message;
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

}
