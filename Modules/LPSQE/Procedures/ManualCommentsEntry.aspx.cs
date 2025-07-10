using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;

public partial class ManualCommentsEntry : System.Web.UI.Page
{
    public Section ob_Section;
    public static Random r = new Random();
    string FileName = "";
    string RandomString = "";
    public int CommentID
    {
        get
        {
            return Common.CastAsInt32 (ViewState["CommentID"]);
        }
        set { ViewState["CommentID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        string qString = Request.QueryString[0].ToString();

        CommentID = Common.CastAsInt32(qString.Substring(qString.IndexOf('!') + 1, qString.IndexOf('~') - qString.IndexOf('!') - 1));
        int ManualId = Common.CastAsInt32(qString.Substring(qString.IndexOf('~') + 1, qString.IndexOf('@') - qString.IndexOf('~') - 1));
        string SectionId = qString.Substring(qString.IndexOf('@') + 1, qString.IndexOf('$') - qString.IndexOf('@') - 1);

        ob_Section = new Section(ManualId, SectionId);
        FileName = ob_Section.FileName; 
        if(!IsPostBack)
        {
            ShowHeader();
            //ShowComments();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ManualBO mb = new ManualBO(ob_Section.ManualId);

        if (txtComments.Text.Trim() == "")
        {
            ShowMessage("Please enter some comments to save.",true);
            txtComments.Focus();  
            return; 
        }
        //----------------------
        string sql = "Update DBO.SMS_ManualDetails_Comments set Comments='" + txtComments.Text.Replace("'","''").Trim() + "' , EnteredOn=Getdate() Where CommentID=" + CommentID.ToString() + "";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ShowMessage("Comments updated successfully.", false);


      //  if (Section.SaveSectionComments(ob_Section.ManualId, ob_Section.SectionId, mb.VersionNo, ob_Section.Version, txtComments.Text.Trim(), Session["UserName"].ToString(),"I"))
      //{
      //    ShowMessage("Comments saved successfully.",false);
      //}
      //else
      //{
      //    ShowMessage("Unable to save comments.",true);
      //}
      //ShowComments();
    }

    protected void btnAttachment_Click(object sender, ImageClickEventArgs e)
    {
        int rnd = r.Next(10000);
        OpenAttachment();
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        string FullFileName = "/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + RandomString + "/" + FileName + "?" + rnd;
        //window.location='" + FullFileName + "'
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "this.parent.FileName='" + FullFileName + "';window.location='" + FullFileName + "';", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('" + FullFileName + "')", true);
        //Response.Redirect(FullFileName);
    }

    // ------------------------ Function
    public void ShowHeader()
    {
        try
        {
            ManualBO mb = new ManualBO(ob_Section.ManualId);
            lblManualName.Text = mb.ManualName;
            lblMVersion.Text = "[" + mb.VersionNo + "]";
            lblSVersion.Text = "[" + ob_Section.Version + "]";
            if (ob_Section.Status == "A")
            {
                lblHeading.Text = ob_Section.SectionId + " : " + ob_Section.Heading;
                lblContent.Text = "[ " + ob_Section.SearchTags + " ]";
            }
            else
            {
                lblHeading.Text = ob_Section.SectionId + " : " + ob_Section.Heading;
                lblContent.Text = "[ NA ]";
            }

        }
        catch { }
    }
    public void ShowComments()
    {
        DataTable dtComments = ob_Section.getComments();
        StringBuilder sb = new StringBuilder();
        int Counter = 1;
        foreach (DataRow dr in dtComments.Rows)
        {
            sb.Append("<div class='enteredby'>" + (Counter++).ToString() + ". " + dr["EnteredBy"].ToString() + " / " + Convert.ToDateTime(dr["EnteredOn"]).ToString("dd-MMM-yyyy") + " : </div><div class='comm' >" + dr["Comments"].ToString() + "</div>");
        }
        litHistory.Text = sb.ToString();
    }
    public void ShowMessage(string Mess, bool error)
    {
        lblMsg.Text = Mess;
        lblMsg.ForeColor = (error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }
    public void OpenAttachment()
    {
        RandomString = "File_" + r.Next();
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        if (!Directory.Exists(Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + RandomString)))
        {
            Directory.CreateDirectory(Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + RandomString));
        }
        foreach (string file in Directory.GetFiles(Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + RandomString)))
        {
            File.Delete(file);
        }
        File.WriteAllBytes(Server.MapPath("/" + appname + "/EMANAGERBLOB/LPSQE/Procedures/" + RandomString + "/" + FileName), ob_Section.getContentFile());
    }
}
