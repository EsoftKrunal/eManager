using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SMS_FullScreenSection : System.Web.UI.Page
{
    public static Random r = new Random();
    public Section ob_Section;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            if (Page.Request.QueryString["FW"] == null)
            {
                int ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
                string SectionId = Convert.ToString("" + Request.QueryString["SectionId"]);
                ob_Section = new Section(ManualId, SectionId);
                ManualBO mb = new ManualBO(ManualId);
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
}