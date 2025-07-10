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

public partial class VNAHome : System.Web.UI.Page
{
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgPOP.Text = "";
        if (!Page.IsPostBack)
        {
            CurrentVessel = Session["CurrentShip"].ToString();
            for (int i = DateTime.Today.Year+1; i >= 2012; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            ddlYear_OnSelectedIndexChanged(sender, e);
        }
    }

    protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        lblQ1.Visible = false;
        lblQ2.Visible = false;
        lblQ3.Visible = false;
        lblQ4.Visible = false;
        

        lblQ1.Text = CurrentVessel + "-" + ddlYear.SelectedValue + "-Q1";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VNA_MASTER WHERE VNANO='" + lblQ1.Text + "'");
        if (DT.Rows.Count > 0)
            lblQ1.Visible = true;

        lblQ2.Text = CurrentVessel + "-" + ddlYear.SelectedValue + "-Q2";
        DT = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VNA_MASTER WHERE VNANO='" + lblQ2.Text + "'");
        if (DT.Rows.Count > 0)
            lblQ2.Visible = true;

        lblQ3.Text = CurrentVessel + "-" + ddlYear.SelectedValue + "-Q3";
        DT = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VNA_MASTER WHERE VNANO='" + lblQ3.Text + "'");
        if (DT.Rows.Count > 0)
            lblQ3.Visible = true;

        lblQ4.Text = CurrentVessel + "-" + ddlYear.SelectedValue + "-Q4";
        DT = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VNA_MASTER WHERE VNANO='" + lblQ4.Text + "'");
        if (DT.Rows.Count > 0)
            lblQ4.Visible = true;

    }
    protected void lblQ_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this,this.GetType(),"s1","window.open('OpenVNA.aspx?Key=" + ((LinkButton)sender).Text + "','');",true);
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
            if (CurrentVessel == FileName.Substring(6, 3).ToString())
            {
                string TargetDir = Server.MapPath("TEMP/");
                string SchemaFile = TargetDir + "VNASchema.XML";
                string DataFile = TargetDir + "VNAData.XML";


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
            else
            {
                lblMsgPOP.Text = "Please check! wrong packet selected.";
                
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
        ddlYear_OnSelectedIndexChanged(sender, e);
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

        //int REGId = Convert.ToInt32(ds.Tables["Reg_Regulation"].Rows[0]["RegId"]);

        try
        {

            SqlCommand cmd = new SqlCommand("", Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            //------------------------------------------------      
            cmd.CommandType = CommandType.StoredProcedure;
            //------------------------------------------------      
            if (ds.Tables["VNA_MASTER"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["VNA_MASTER"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "Import_S_VNA_Master");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "Import_S_VNA_Master";
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

            if (ds.Tables["vna_details"].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables["vna_details"].Rows)
                {
                    //-------------------------------
                    string[] CommandParameters = getCommandParameters(cmd, "Import_S_VNA_Details");
                    cmd.Parameters.Clear();
                    cmd.CommandText = "Import_S_VNA_Details";
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
