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

public partial class SWCommunication : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public int EmpId
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpId"]);
        }
        set
        {
            ViewState["EmpId"] = value;
        }
    }
    public bool Action
    {
        get
        {
            return Convert.ToBoolean(ViewState["Action"]);
        }
        set
        {
            ViewState["Action"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);

        Action = Auth.IsAdd;

        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }

        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["loginid"]);
            DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * FROM dbo.Vessel Where Vesselstatusid=1  ORDER BY vesselname");
            ddlVessel.DataSource = DT;
            ddlVessel.DataTextField = "vesselname";
            ddlVessel.DataValueField = "vesselId";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("< - - ALL - - >", "0"));   
            if (EmpId == 19)
            {
                
            }
            else
            {
                ShowManualsList();
            }
        }
    }
    //---------------------- Events 
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowManualsList();
    }
    //---------------------- Function
    protected void ShowManualsList()
    {
        string SQL = "";
        if (ddlVessel.SelectedIndex > 0)
        {
            dvPendingForApprovalRequest.Visible = true;
            SQL = "select ROW_NUMBER() OVER(ORDER BY MANUALNAME) AS SNO,MANUALID,MANUALNAME,VERSIONNO from dbo.SMS_ManualMaster M WHERE M.MANUALID IN (SELECT C.MANUALID FROM dbo.SMS_ManualVesselType C WHERE VESSELTYPEID=" + ddlVessel.SelectedValue + ") ORDER BY MANUALNAME";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            rptPendingForApprovalRequest.DataSource = dt;
            rptPendingForApprovalRequest.DataBind();
        }
        else
        {
            rptPendingForApprovalRequest.DataSource = null;
            rptPendingForApprovalRequest.DataBind();
        }

        
       

        //if (ddlVessel.SelectedIndex == 0)
        //    SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],FILECONTENT,SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED,ApprovedOn, " +
        //          " (Select Top 1 SentDate from dbo.vw_SMS_GetSendManuals VW Where VW.ManualID=MD.ManualID And VW.SectionID=MD.SectionID Order by SentDate desc)LastSent ," +
        //          " (Select Count(VesselID) from dbo.SMS_APP_COM_ManualDetails Where ManualID=MD.MANUALID And SectionID=MD.SECTIONID And Scheduled=1 )IsAnyScheduled ," +
        //          "(CASE WHEN APPROVED='' THEN 'Heading Changed' " +
        //          "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
        //          "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " + 
        //          "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
        //          "WHEN APPROVED='R' THEN 'Cancel' " +
        //          "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where MD.APPROVED='A' AND MD.STATUS='A' ORDER BY MD.MANUALID,MD.SECTIONID";
        //else
        //    SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],FILECONTENT,SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED,ApprovedOn, " +
        //          " (Select Top 1 SentDate from dbo.vw_SMS_GetSendManuals VW Where VW.ManualID=MD.ManualID And VW.SectionID=MD.SectionID Order by SentDate desc)LastSent," +
        //          " (Select Count(VesselID) from dbo.SMS_APP_COM_ManualDetails Where ManualID=MD.MANUALID And SectionID=MD.SECTIONID And Scheduled=1)IsAnyScheduled ," +
        //          "(CASE WHEN APPROVED='' THEN 'Heading Changed' " +
        //          "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
        //          "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " +
        //          "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
        //          "WHEN APPROVED='R' THEN 'Cancel' " +
        //          "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where MD.APPROVED='A' AND MD.STATUS='A' AND MD.MANUALID=" + ddlPendingForApprovalmanuals.SelectedValue + " ORDER BY MD.MANUALID,MD.SECTIONID";

       
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_ApprovalRequest');", true);
    }
    protected void btnSchedule_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem ri in rptPendingForApprovalRequest.Items)
            {
                CheckBox ch = ((CheckBox)ri.FindControl("chkSelect"));
                if (ch.Checked)
                {
                    int ManualId = Common.CastAsInt32(((HiddenField)ri.FindControl("hfdManualId")).Value);
                    Common.Execute_Procedures_Select_ByQuery("EXEC DBO.SMS_SCHEDULE_WHOLE_MANUAL " + ddlVessel.SelectedValue + "," + ManualId.ToString() + ",'" + Session["UserName"].ToString() + "'");
                }
            }
            lblMessage.Text = "Scheduled Successfully.";
        }
        catch
        {
            lblMessage.Text = "Unable to schedule.";
        }
    }
}