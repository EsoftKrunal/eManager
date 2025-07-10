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

public partial class ReadManualHistorySection : System.Web.UI.Page
{
    public static Random r = new Random();
    public ReadHistorySection ob_SectionHistory;
    public ReadManualBO mb;
    public string LastSectionId="";

    public int HistoryID
    {
        set { ViewState["HistoryID"] = value; }
        get { return Common.CastAsInt32(ViewState["HistoryID"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        EnableDisable();
        if (Request.UrlReferrer.ToString().Contains("AddSection.aspx"))
        {
            ReloadHeadings();
        }
    }
    public void EnableDisable()
    {
        HistoryID = Common.CastAsInt32(Request.QueryString["HistoryID"]);
        ob_SectionHistory = new ReadHistorySection(HistoryID);
        mb = new ReadManualBO(ob_SectionHistory.ManualId);
        lblManualName.Text = mb.ManualName;
        lblMVersion.Text = "[" + mb.VersionNo + "]";
        lblSVersion.Text = "[" + ob_SectionHistory.Version + "]";
        LastSectionId = ob_SectionHistory.SectionId;
        frmFile.Attributes.Add("src", "History_ReadManualSection.aspx?HistoryId=" + HistoryID.ToString());
        if (ob_SectionHistory.SectionId.Trim() == "")
        {
            dvForms.Visible = false;
            dvPopUp.Visible = false; 
        }
        else
        {
            if (ob_SectionHistory.Status == "A")
            {

                lblHeading.Text = ob_SectionHistory.SectionId + " : " + ob_SectionHistory.Heading;
                lblContent.Text = "[ " + ob_SectionHistory.SearchTags + " ]";
            }
            else
            {
                lblHeading.Text = ob_SectionHistory.SectionId + " : " + ob_SectionHistory.Heading;
                lblContent.Text = "[ NA ]"; 
            }
            
            ReloadImages();
            
        }
    }
    public void ReloadHeadings()
    {
        EnableDisable();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "ab", "ReloadHeadings();", true);  
    }
    public void ReloadImages()
    {
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "ReloadImages(" + ob_Section.ManualId.ToString() + ",'" + ob_Section.SectionId.ToString() + "');", true);
    }
    protected void btnAttachment_Click(object sender, ImageClickEventArgs e)
    {
        int rnd = r.Next(10000);
        OpenAttachment();
        frmFile.Attributes.Add("src", "ReadManualSection.aspx?" + rnd); 
    }
    protected void lblFileName_Click(object sender, EventArgs e)
    {
        int rnd = r.Next(10000);
        OpenAttachment();
        frmFile.Attributes.Add("src", "ReadManualSection.aspx?" + rnd); 
    }
    public void OpenAttachment()
    {
        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", lblFileName.Text));
        //Response.ContentType = "application/" + Path.GetExtension(lblFileName.Text).Substring(1);
        //Response.BinaryWrite(ob_Section.getContentFile());
        File.WriteAllBytes(Server.MapPath("~/SMS/Manual.pdf"), ob_SectionHistory.getContentFile_FOR_READ());
    }
}
