using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class emtm_Emtm_Home : System.Web.UI.Page
{
    public static Random r = new Random();
    string FileName = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //a1.Visible = Session["loginid"].ToString() == "1";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "window.open('Emtm_Alerts.aspx', '_blank');", true);

    }
    
    public void OpenAttachment()
    {
        if (!Directory.Exists(Server.MapPath("~/UserUploadedDocuments/" + Session.SessionID)))
        {
            Directory.CreateDirectory(Server.MapPath("~/UserUploadedDocuments/" + Session.SessionID));
        }
        foreach (string file in Directory.GetFiles(Server.MapPath("~/UserUploadedDocuments/" + Session.SessionID)))
        {
            File.Delete(file);
        }
        File.WriteAllBytes(Server.MapPath("~/UserUploadedDocuments/" + Session.SessionID + "/" + FileName), getContentFile());
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        string mode = ((ImageButton)sender).CommandArgument;
        switch (mode)
        {
            case "BA": frmm.Attributes.Add("src", "Emtm_BirthdayAlert.aspx");
                break;
            case "OA": frmm.Attributes.Add("src", "Emtm_OfficeAbsenceHome.aspx");
                break;
            case "MP": frmm.Attributes.Add("src", "Emtm_Performance.aspx");
                break;
            case "PP": frmm.Attributes.Add("src", "Emtm_PolicyProcedure.aspx");
                break;
            case "MA": frmm.Attributes.Add("src", "Emtm_AlertHome.aspx");
                break;
            case "SH": frmm.Attributes.Add("src", "Emtm_Vesselassignment.aspx");
                break;
            default:
                break;
        }
    }
    protected void btnPost1_Click(object sender, EventArgs e)
    {
        string mode = ((LinkButton)sender).CommandArgument;
        switch (mode)
        {
            case "BA": frmm.Attributes.Add("src", "Emtm_BirthdayAlert.aspx");
                break;
            case "OA":
                //frmm.Attributes.Add("src", "Emtm_OfficeAbsenceHome.aspx");
                frmm.Attributes.Add("src", "Emtm_OfficeAbsenceHome1.aspx");
                break;
            case "MP": frmm.Attributes.Add("src", "Emtm_Performance.aspx");
                break;
            case "PP": frmm.Attributes.Add("src", "Emtm_PolicyProcedure.aspx");
                break;
            case "MA": frmm.Attributes.Add("src", "Emtm_AlertHome.aspx");
                break;
            case "SH": frmm.Attributes.Add("src", "Emtm_Vesselassignment.aspx");
                break;
            default:
                break;
        }
    }

    public byte[] getContentFile()
    {
        byte[] ret = null;
        DataSet RetValue = new DataSet();

        //SqlConnection myConnection = new SqlConnection("Data Source=172.30.1.4;Initial Catalog=eMANAGER;Persist Security Info=True;User Id=sa;Password=Esoft^%$#@!");
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        SqlCommand Cmd = new SqlCommand("select FileName,FileContent from SMS_ManualDetails WHERE ManualId=12 and rtrim(ltrim(SectionId))='7.2'", myConnection);
        SqlDataAdapter Adp = new SqlDataAdapter(Cmd);
        Adp.Fill(RetValue, "File");
        if (RetValue.Tables["File"].Rows.Count > 0)
        {
            ret = (byte[])RetValue.Tables["File"].Rows[0]["FileContent"];
        }
        return ret;
    }
    public string GetFileName()
    {
        string ret = "";
        DataSet RetValue = new DataSet();
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ToString());
        SqlCommand Cmd = new SqlCommand("select FileName from dbo.SMS_ManualDetails where  ManualID=12 And SectionID='7.2'", myConnection);
        SqlDataAdapter Adp = new SqlDataAdapter(Cmd);
        Adp.Fill(RetValue, "File");
        if (RetValue.Tables["File"].Rows.Count > 0)
        {
            ret = Convert.ToString(RetValue.Tables["File"].Rows[0]["FileName"]);
        }
        return ret;
    }
}
