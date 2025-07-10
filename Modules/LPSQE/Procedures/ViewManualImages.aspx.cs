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

public partial class ViewManualImages : System.Web.UI.Page
{
    public Section ob_Section;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
        string SectionId = Convert.ToString("" + Request.QueryString["SectionId"]);
        ob_Section = new Section(ManualId, SectionId);
        ShowImages();
    }
    public void ShowImages()
    {
        DataTable dtComments = ob_Section.getImages();
        StringBuilder sb = new StringBuilder();
        int Counter = 1;
        foreach (DataRow dr in dtComments.Rows)
        {
            sb.Append("<img onclick='window.open(this.src);' title='" + dr["Description"].ToString() + "' src='/MTMPMS/SMS/Attachment/Images/" + dr["ImageName"].ToString() + "' style='height:120px; margin:0px 5px 5px 0px;cursor:pointer;'/>"); 
        }
        litHistory.Text = sb.ToString();
    }
}
