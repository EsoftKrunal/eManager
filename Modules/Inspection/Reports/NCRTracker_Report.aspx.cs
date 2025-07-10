using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Reports_NCRTracker_Report : System.Web.UI.Page
{
    int intVesselId = 0;
    string strNCRCat = "", strNCRFDate = "", strNCRTDate = "", strDueInDays = "", strStatus = "", strResponsiblity = "", strOverDue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            if (Page.Request.QueryString["NCRVslId"].ToString() != "")
            {
                intVesselId = int.Parse(Page.Request.QueryString["NCRVslId"].ToString());
            }
            if (Page.Request.QueryString["NCCatgryID"].ToString() != "")
            {
                strNCRCat = Page.Request.QueryString["NCCatgryID"].ToString();
            }
            if (Page.Request.QueryString["NCFromDate"].ToString() != "")
            {
                strNCRFDate = Page.Request.QueryString["NCFromDate"].ToString();
            }
            if (Page.Request.QueryString["NCTDate"].ToString() != "")
            {
                strNCRTDate = Page.Request.QueryString["NCTDate"].ToString();
            }
            if (Page.Request.QueryString["NCDueDays"].ToString() != "")
            {
                strDueInDays = Page.Request.QueryString["NCDueDays"].ToString();
            }
            if (Page.Request.QueryString["NCStatus"].ToString() != "")
            {
                strStatus = Page.Request.QueryString["NCStatus"].ToString();
            }
            if (Page.Request.QueryString["NCResponsibility"].ToString() != "")
            {
                strResponsiblity = Page.Request.QueryString["NCResponsibility"].ToString();
            }
            if (Page.Request.QueryString["NCOverDue"].ToString() != "")
            {
                strOverDue = Page.Request.QueryString["NCOverDue"].ToString();
            }
            if (!Page.IsPostBack)
            {
                IFRAME1.Attributes.Add("src", "NCRTracker_Crystal.aspx?NCRVslID=" + intVesselId + "&NCRCatId=" + strNCRCat + "&NCRFDt=" + strNCRFDate + "&NCRTDt=" + strNCRTDate + "&NCRDueDays=" + strDueInDays + "&NCRStatus=" + strStatus + "&NCRResp=" + strResponsiblity + "&NCROverDue=" + strOverDue);
            }
        }
        catch (Exception ex) { throw ex; }
    }
}
