using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;

public partial class APP_ReadManualSection : System.Web.UI.Page
{
    public static Random r = new Random();
    public ReadSection ob_Section;
    string FileName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        EnableDisable(); 
    }
    public void EnableDisable()
    {
        int ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
        string SectionId = Convert.ToString("" + Request.QueryString["SectionId"]);
        ob_Section = new ReadSection(ManualId, SectionId);
        ReadManualBO mb = new ReadManualBO(ManualId);
        FileName = ob_Section.FileName; 
        if (ob_Section.SectionId.Trim() == "")
        {
            lblFileName.Visible = false;
            btnAttachment.Visible = false;
        }
        else
        {
            if (ob_Section.Status == "A")
            {
                lblFileName.Text = ob_Section.FileName;
                lblFileName.Visible = true;
                btnAttachment.Visible = (lblFileName.Text.Trim() != "");
            }
            else
            {
                lblFileName.Visible = false;
                btnAttachment.Visible = false;
            }
        }
    }
    protected void btnAttachment_Click(object sender, ImageClickEventArgs e)
    {
        int rnd = r.Next(10000);
        OpenAttachment();
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        string FullFileName = "/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + Session.SessionID + "/" + FileName + "?" + rnd;
        //window.location='" + FullFileName + "'
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "this.parent.FileName='" + FullFileName + "';window.location='" + FullFileName + "';", true);
        //Response.Redirect(FullFileName);
    }
    protected void lblFileName_Click(object sender, EventArgs e)
    {
        int rnd = r.Next(10000);
        OpenAttachment();
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        string FullFileName = "/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + Session.SessionID + "/" + FileName + "?" + rnd;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "this.parent.FileName='" + FullFileName + "';window.location='" + FullFileName + "';", true);
    }
    protected void btnDownLoadFile(object sender, EventArgs e)
    {
        LinkButton BTN = (LinkButton)sender;
        HiddenField hfFileName = (HiddenField)BTN.Parent.FindControl("hfFileName");
        
        int FormId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string FormName = hfFileName.Value;
        DownloadAttachment(FormId, FormName);
    }
    public void DownloadAttachment(int FormId, string FileName)
    {
        if (FileName.Trim() != "")
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FileName));
            Response.ContentType = "application/" + Path.GetExtension(FileName).Substring(1);
            Response.BinaryWrite(Forms.getFormAttachment(FormId));
        }
    }
    public void OpenAttachment()
    {
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        if (!Directory.Exists(Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + Session.SessionID)))
        {
            Directory.CreateDirectory(Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + Session.SessionID));
        }
        foreach (string file in Directory.GetFiles(Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + Session.SessionID)))
        {
            File.Delete(file);
        }
        File.WriteAllBytes(Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + Session.SessionID +"/"+ FileName), ob_Section.getContentFile_FOR_READ());
    }
}
