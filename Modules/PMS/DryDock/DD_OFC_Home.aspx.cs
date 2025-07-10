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


public partial class DD_OFC_Home : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        btnDDTracker.CssClass = "btn1";
        btnDocket.CssClass = "btn1";
        btnJobMaster.CssClass = "btn1";
        btnDDPlanSettings.CssClass = "btn1";


        if (Session["MM"] == null)
        {
            Session["MM"] = 0;
        }
        switch (Common.CastAsInt32(Session["MM"]))
        {
            case 0:
                btnDDTracker.CssClass = "selbtn";
                break;
            case 1:
                btnDocket.CssClass = "selbtn";
                break;
            case 2:
                btnJobMaster.CssClass = "selbtn";
                break;
            case 3:
                btnDDPlanSettings.CssClass = "selbtn";
                break;
           
            default:
                break;
        }
        // -------------------------- SESSION CHECK ----------------------------------------------
        //ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            Session["DDPageId"] = "DD_OFC_Home";
            BindYear();
            LoadDockets();
        }
    }

    protected void RegisterSelect(object sender, EventArgs e)
    {
        btnDDTracker.CssClass = "btn1";
        btnDocket.CssClass = "btn1";
        btnJobMaster.CssClass = "btn1";
        btnDDPlanSettings.CssClass = "btn1";

        Button btn = (Button)sender;
        int CommArg = Common.CastAsInt32(btn.CommandArgument);
        Session["MM"] = CommArg;

        switch (CommArg)
        {
            case 0:
                Session["MM"] = 0;
                btnDDTracker.CssClass = "selbtn";
                frm.Attributes.Add("src", "DD_OFC_Tracker.aspx");
                break;
            case 1:
                Session["MM"] = 1;
                btnDocket.CssClass = "selbtn";
                frm.Attributes.Add("src", "DD_OFC_Docket.aspx");
                break;
            case 2:
                Session["MM"] = 2;
                btnJobMaster.CssClass = "selbtn";
                frm.Attributes.Add("src", "DD_OFC_JobMaster.aspx");
                break;
            case 3:
                Session["MM"] = 3;
                btnDDPlanSettings.CssClass = "selbtn";
                frm.Attributes.Add("src", "DD_OFC_PlanSettings.aspx");
                break;
            default:
                break;
        }

    }

    private void BindYear()
    {
        for (int i = 2014; i <= DateTime.Today.Year; i++)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlYear.SelectedValue = DateTime.Today.Year.ToString();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDockets();
    }
    protected void LoadDockets()
    {
        DataTable dt = new DataTable();
        string strSQL = "SELECT VesselId, VesselCode, VesselName, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 1 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc ) AS Jan, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 2 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Feb, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 3 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Mar, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 4 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Apr, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 5 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS May, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 6 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Jun, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 7 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Jul, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 8 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Aug, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 9 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Sep, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 10 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Oct, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 11 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS Nov, " +
                        "( SELECT TOP 1 [Status] FROM  [DD_DocketMaster] WHERE VesselId = V.VesselId AND Month([Startdate]) = 12 AND Year([Startdate]) = " + ddlYear.SelectedValue.Trim() + " ORDER BY [Startdate] Desc) AS [Dec] " +
                        "FROM dbo.vessel V WHERE VesselStatusId <> 2  order by VESSELNAME";
        dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptDocket.DataSource = dt;
        rptDocket.DataBind();
    }
    
}
