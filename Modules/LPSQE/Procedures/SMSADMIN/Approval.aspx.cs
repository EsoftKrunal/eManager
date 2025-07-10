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

public partial class SMS_Approval : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("NotAuthorized.aspx");
        }

        lblMessage.Text = "";
        lblMsgAdminActionPopup.Text = "";
        if (!Page.IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["loginid"]);

            DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * FROM  dbo.SMS_ManualMaster ORDER BY MANUALNAME");
            ddlPendingForApprovalmanuals.DataSource = DT;
            ddlPendingForApprovalmanuals.DataTextField = "ManualName";
            ddlPendingForApprovalmanuals.DataValueField = "ManualId";
            ddlPendingForApprovalmanuals.DataBind();
            ddlPendingForApprovalmanuals.Items.Insert(0, new ListItem("< - - ALL - - >", "0"));   
            //if (EmpId == 19)
            //{
            //    radList.Items[0].Selected = true;
            //    ShowPendingForApproval();
            //    radList.Visible = false;
            //}
            //else
            //{
                ShowPendingForApprovalRequest();
                radList.Visible = (EmpId == 1);
                radList.Items[1].Selected = (EmpId == 1);                
            //}
            

        }
    }
    protected void radList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radList.SelectedIndex == 0)
        {
            ShowPendingForApproval();
        }
        else
        {
            ShowPendingForApprovalRequest();
        }
    }
    protected void ddlPendingForApprovalmanuals_SelectedIndexChanged(object sender, EventArgs e)
    {
        radList_SelectedIndexChanged(sender, e);
    }

    protected void btnSubmitPopupAdmin_Click(object sender, EventArgs e)
    {
        if (txtCommentsAdmin.Text.Trim() == "")
        {
            lblMsgAdminActionPopup.Text = "Please enter comments.";
            return;
        }

        Button SubmitBtn = (Button)sender;
        int ManualId = Common.CastAsInt32(SubmitBtn.CommandArgument);
        string SectionId = SubmitBtn.ToolTip;
        string AC = SubmitBtn.Text.Trim().ToLower();

        if (AC.Contains("reject"))
        {
            if (Section.RejectApprovalByMd(ManualId, SectionId, txtCommentsAdmin.Text.Trim()))
            {
                lblMsgAdminActionPopup.Text = "Request cancelled successfully.";
                ShowPendingForApproval();        
            }
            else
            {
                lblMsgAdminActionPopup.Text = "Unable to cancel request.";
            }

        }
        else
        {
            String Error = "";
            Error = txtCommentsAdmin.Text;
            if (Section.ApprovalFinished(ManualId, SectionId, ref Error))
            {
                lblMsgAdminActionPopup.Text = "Approved successfully.";
                ShowPendingForApproval();        
            }
            else
            {
                lblMsgAdminActionPopup.Text = "Unable to approve. Error : " + Error;
            }
        }
  
    }
    protected void btnClosePopupAdmin_Click(object sender, EventArgs e)
    {
        dvPOPUPAdmin.Visible = false;
    }

    protected void DoMDAction(object sender, EventArgs e)
    {
        dvPOPUPAdmin.Visible = true;
        txtCommentsAdmin.Text = "";
        LinkButton li = (LinkButton)sender;
        int ManualId = Common.CastAsInt32(li.CommandArgument);
        string SectionId = li.ToolTip;
        string AC = li.Text.Trim().ToLower();

        btnSubmitPopupAdmin.CommandArgument = ManualId.ToString();
        btnSubmitPopupAdmin.ToolTip = SectionId.ToString();
        btnSubmitPopupAdmin.Attributes.Add("AC", AC);
        if (AC.Contains("reject"))
            btnSubmitPopupAdmin.Text = " Reject ";
        else
            btnSubmitPopupAdmin.Text = " Approve ";        
    }
    protected void ShowPendingForApproval()
    {
       

        string SQL = "";

        //if (ddlPendingForApprovalmanuals.SelectedIndex == 0)
        //    SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,'Awaiting Approval' as Status from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED='R' AND STATUS='A' ORDER BY MD.MANUALID,MD.SECTIONID";
        //else
        //    SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,'Awaiting Approval' as Status from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED='R' AND STATUS='A' AND MD.MANUALID=" + ddlPendingForApprovalmanuals.SelectedValue + " ORDER BY MD.MANUALID,MD.SECTIONID";

        if (ddlPendingForApprovalmanuals.SelectedIndex == 0)
            SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,'Awaiting Approval' as Status from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED='R' ORDER BY MD.MANUALID,MD.SECTIONID";
        else
            SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,'Awaiting Approval' as Status from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED='R' AND MD.MANUALID=" + ddlPendingForApprovalmanuals.SelectedValue + " ORDER BY MD.MANUALID,MD.SECTIONID";

        dvPendingForApproval.Visible = true;
        dvPendingForApprovalRequest.Visible = false;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        RptManuals.DataSource = dt;
        RptManuals.DataBind();
    }
    //---------------------- USER SCREEN ------------------
    protected void DoUserAction(object sender, EventArgs e)
    {
        LinkButton li = (LinkButton)sender;
        int ManualId = Common.CastAsInt32(li.CommandArgument);
        string SectionId = li.ToolTip;
        string AC = li.Text.Trim().ToLower();
        if (AC.Contains("cancel"))
        {
            if (Section.CancelApprovalRequest(ManualId, SectionId))
            {
                lblMessage.Text = "Request cancelled successfully.";
                ShowPendingForApprovalRequest();
            }
            else
            {
                lblMessage.Text = "Unable to cancel request.";
            }
        }
        else
        {
            btnSummit1.CommandArgument = li.CommandArgument;
            btnSummit1.Attributes.Add("SectionId",li.ToolTip);
            dvPOPUPUser.Visible = true;
        }
    }
    protected void ShowPendingForApprovalRequest()
    {
        string SQL = "";

        //if (ddlPendingForApprovalmanuals.SelectedIndex == 0)
        //    SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED, " +
        //          "(CASE WHEN APPROVED='' THEN '' " +
        //          "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
        //          "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " + 
        //          "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
        //          "WHEN APPROVED='R' THEN 'Cancel' " +
        //          "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED<>'A' AND STATUS='A' ORDER BY MD.MANUALID,MD.SECTIONID";
        //else
        //    SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED, " +
        //          "(CASE WHEN APPROVED='' THEN '' " +
        //          "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
        //          "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " +
        //          "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
        //          "WHEN APPROVED='R' THEN 'Cancel' " +
        //          "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED<>'A' AND STATUS='A' AND MD.MANUALID=" + ddlPendingForApprovalmanuals.SelectedValue + " ORDER BY MD.MANUALID,MD.SECTIONID";


        if (ddlPendingForApprovalmanuals.SelectedIndex == 0)
            SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED, " +
                  "(CASE WHEN APPROVED='' THEN '' " +
                  "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
                  "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " +
                  "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
                  "WHEN APPROVED='R' THEN 'Cancel' " +
                  "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED<>'A' ORDER BY MD.MANUALID,MD.SECTIONID";
        else
            SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED, " +
                  "(CASE WHEN APPROVED='' THEN '' " +
                  "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
                  "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " +
                  "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
                  "WHEN APPROVED='R' THEN 'Cancel' " +
                  "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED<>'A' AND MD.MANUALID=" + ddlPendingForApprovalmanuals.SelectedValue + " ORDER BY MD.MANUALID,MD.SECTIONID";

        dvPendingForApprovalRequest.Visible = true;
        dvPendingForApproval.Visible = false;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptPendingForApprovalRequest.DataSource = dt;
        rptPendingForApprovalRequest.DataBind();
    }
    protected void btnBack1_Click(object sender, EventArgs e)
    {
        dvPOPUPUser.Visible = false;
    }
    protected void btnSummit1_Click(object sender, EventArgs e)
    {
        int ManualId = Common.CastAsInt32(btnSummit1.CommandArgument);
        string SectionId = btnSummit1.Attributes["SectionId"];
        if (Section.RequestApproval(ManualId, SectionId, txtComments1.Text))
        {
            lblMessage.Text = "Request created successfully.";
            ShowPendingForApprovalRequest();
        }
        else
        {
            lblMessage.Text = "Unable to create request.";
        }

        dvPOPUPUser.Visible = false;
        
    }

    //protected void btnRequestFullApprove_Click(object sender, EventArgs e)
    //{
    //    if(ddlPendingForApprovalmanuals.SelectedIndex >0)
    //    {
    //        if (Manual.RequestManualApproval(ddlPendingForApprovalmanuals.SelectedValue))
    //        {
    //            lblMessageApprovalReq.Text = "Approval request created successfully.";
    //        }
    //        else
    //        {
    //            lblMessageApprovalReq.Text = "Unable to request for approve.";
    //        }
    //    }
    //}
    //protected void btnFullApprove_Click(object sender, EventArgs e)
    //{
    //    if (ddlPendingForApprovalmanuals.SelectedIndex > 0)
    //    {
    //        if (Manual.ApproveManual(ddlPendingForApprovalmanuals.SelectedValue))
    //        {
    //            lblMessageApprovalReq.Text = "Approval done successfully.";
    //        }
    //        else
    //        {
    //            lblMessageApprovalReq.Text = "Unable to approve.";
    //        }
    //    }
    //}


    //---------------------- MD SCREEN ------------------

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Manuals');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "SetLastFocus('dvscroll_PendingForApprovalRequest');", true);
    }
}