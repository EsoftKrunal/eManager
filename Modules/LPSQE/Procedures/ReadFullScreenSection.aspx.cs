using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SMS_ReadFullScreenSection : System.Web.UI.Page
{
    public static Random r = new Random();
    public ReadSection ob_Section;
    public ReadManualBO mb;
    public int ManualId;
    public string SectionId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            if (Page.Request.QueryString["FW"] == null)
            {
                ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
                SectionId = Convert.ToString("" + Request.QueryString["SectionId"]);
                ob_Section = new ReadSection(ManualId, SectionId);
                mb = new ReadManualBO(ManualId);
                lblManualName.Text = mb.ManualName;
                lblMVersion.Text = "[" + mb.VersionNo + "]";
                lblSVersion.Text = "[" + ob_Section.Version + "]";
                if (ob_Section.Status == "A")
                {
                    lblHeading.Text = SectionId + " : " + ob_Section.Heading;
                    lblContent.Text = "[ " + ob_Section.SearchTags + " ]";
                }
                else
                {
                    lblHeading.Text = SectionId + " : " + ob_Section.Heading;
                    lblContent.Text = "[ NA ]";
                }
            }
            else
            {
                dvHeader.Style.Add("display", "none");
            }
             
        }
        catch { } 
        frmFile.Attributes.Add("src", Convert.ToString("" + Request.QueryString["FileName"]));  
    }
    protected void lnkSMSReview_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('OfficeComments.aspx?ManualId=" + ManualId.ToString() + "&SectionId=" + SectionId + "&ManVersion=" + mb.VersionNo + "&SecVersion=" + ob_Section.Version + "', '_blank', '');", true);
    }
}