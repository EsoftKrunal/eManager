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

public partial class Reports_FollowupTracker_Report : System.Web.UI.Page
{
    int intVesselId = 0;
    string strFollowUpCat = "", strFollowUpFDate = "", strFollowUpTDate = "", strDueInDays = "", strStatus = "", strCritical = "", strResponsiblity = "", strOverDue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            if (Page.Request.QueryString["FPVslId"].ToString() != "")
            {
                intVesselId = int.Parse(Page.Request.QueryString["FPVslId"].ToString());
            }
            if (Page.Request.QueryString["FPCatgryID"].ToString() != "")
            {
                strFollowUpCat = Page.Request.QueryString["FPCatgryID"].ToString();
            }
            if (Page.Request.QueryString["FPFromDate"].ToString() != "")
            {
                strFollowUpFDate = Page.Request.QueryString["FPFromDate"].ToString();
            }
            if (Page.Request.QueryString["FPTDate"].ToString() != "")
            {
                strFollowUpTDate = Page.Request.QueryString["FPTDate"].ToString();
            }
            if (Page.Request.QueryString["FPDueDays"].ToString() != "")
            {
                strDueInDays = Page.Request.QueryString["FPDueDays"].ToString();
            }
            if (Page.Request.QueryString["FPStatus"].ToString() != "")
            {
                strStatus = Page.Request.QueryString["FPStatus"].ToString();
            }
            if (Page.Request.QueryString["FPCritical"].ToString() != "")
            {
                strCritical = Page.Request.QueryString["FPCritical"].ToString();
            }
            if (Page.Request.QueryString["FPResponsibility"].ToString() != "")
            {
                strResponsiblity = Page.Request.QueryString["FPResponsibility"].ToString();
            }
            if (Page.Request.QueryString["FPOverDue"].ToString() != "")
            {
                strOverDue = Page.Request.QueryString["FPOverDue"].ToString();
            }
            if (!Page.IsPostBack)
            {
                IFRAME1.Attributes.Add("src", "FollowUpTracker_Crystal.aspx?VslID=" + intVesselId + "&FlpCatId=" + strFollowUpCat + "&FlpFDt=" + strFollowUpFDate + "&FlpTDt=" + strFollowUpTDate + "&FlpDueDays=" + strDueInDays + "&FlpStatus=" + strStatus + "&FlpCritical=" + strCritical + "&FlpResp=" + strResponsiblity + "&FlpOverDue=" + strOverDue);
            }
        }
        catch (Exception ex) { throw ex; }
    }
}
