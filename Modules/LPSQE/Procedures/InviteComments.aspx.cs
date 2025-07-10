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

public partial class InviteComments : System.Web.UI.Page
{
    public Section ob_Section;
    public ManualBO mb;
    public AuthenticationManager auth;
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
    public int ManualId
    {
        get
        {
            return Common.CastAsInt32(ViewState["ManualId"]);
        }
        set
        {
            ViewState["ManualId"] = value;
        }
    }
    public string SectionId
    {
        get
        {
            return Convert.ToString(ViewState["SectionId"]);
        }
        set
        {
            ViewState["SectionId"] = value;
        }
    }
    public string ManualText
    {
        get
        {
            return Convert.ToString(ViewState["_ManualText"]);
        }
        set
        {
            ViewState["_ManualText"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMessage.Text = "";
        lblMsgInvitation.Text = "";        
        if (!Page.IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["loginid"]);

            DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * FROM  dbo.SMS_ManualMaster ORDER BY MANUALNAME");
            ddlPendingForApprovalmanuals.DataSource = DT;
            ddlPendingForApprovalmanuals.DataTextField = "ManualName";
            ddlPendingForApprovalmanuals.DataValueField = "ManualId";
            ddlPendingForApprovalmanuals.DataBind();
            ddlPendingForApprovalmanuals.Items.Insert(0, new ListItem("< - - ALL - - >", "0"));   
            if (EmpId == 19)
            {
                
            }
            else
            {
                ShowPendingForApprovalRequest();
            }
            BindOffice();
            ShowEmployeeList();
        }
    }
    
    protected void ddlPendingForApprovalmanuals_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPendingForApprovalRequest();
    }
    
    //---------------------- USER SCREEN ------------------
    protected void ShowPendingForApprovalRequest()
    {
        string SQL = "";

        if (ddlPendingForApprovalmanuals.SelectedIndex == 0)
            SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],FILECONTENT,SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED, " +
                  "(CASE WHEN APPROVED='' THEN '' " +
                  "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
                  "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " + 
                  "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
                  "WHEN APPROVED='R' THEN 'Cancel' " +
                  "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED<>'A' AND STATUS='A' ORDER BY MD.MANUALID,MD.SECTIONID";
        else
            SQL = "select ROW_NUMBER() OVER(ORDER BY MD.MANUALID,MD.SECTIONID) AS SNO,MD.MANUALID,MD.SECTIONID,MM.MANUALNAME,MD.SECTIONID,HEADING,[FILENAME],FILECONTENT,SVERSION,MODIFIEDbY,MODIFIEDON,APPROVED, " +
                  "(CASE WHEN APPROVED='' THEN '' " +
                  "WHEN APPROVED='R' THEN 'Awaiting Approval' " +
                  "WHEN APPROVED='J' THEN 'Rejected' END) AS Status, " +
                  "(CASE WHEN APPROVED='' THEN 'Submit for Approval' " +
                  "WHEN APPROVED='R' THEN 'Cancel' " +
                  "WHEN APPROVED='J' THEN 'Re-Submit for Approval' END) AS ACTION from dbo.SMS_ManualDetails MD INNER JOIN dbo.SMS_ManualMaster MM ON MM.MANUALID=MD.MANUALID where APPROVED<>'A' AND STATUS='A' AND MD.MANUALID=" + ddlPendingForApprovalmanuals.SelectedValue + " ORDER BY MD.MANUALID,MD.SECTIONID";

        dvPendingForApprovalRequest.Visible = true;
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
        if (Section.RequestApproval(ManualId, SectionId, ""))
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

    // ----- Invite Comments
    protected void btnInviteCommentsPopup_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfManualID = (HiddenField)btn.FindControl("hfManualID");
        Label lblSecID = (Label)btn.FindControl("lblSecID");
        Label lblManualName = (Label)btn.FindControl("lblManualName");

        
        ManualId = Common.CastAsInt32(hfManualID.Value);
        SectionId = lblSecID.Text;
        ManualText = lblManualName.Text;
        dvInviteComments.Visible = true;
    }
    protected void btnSendInvitation_OnClick(object sender, EventArgs e)
    {
        ob_Section = new Section(Common.CastAsInt32(ManualId), SectionId);
        mb = new ManualBO(ob_Section.ManualId);
        CheckBox chk ;
        HiddenField hfEmailID;
        string []CC={};
        string Link = "";
        int Counter = 0;        
        //id = Common.CastAsInt32(Link.Substring(14, Link.IndexOf('?') - 14));   
        //sssMID = Common.CastAsInt32(Link.Substring(Link.IndexOf('?') + 1, Link.IndexOf('#') - Link.IndexOf('?') - 1));
        //sssSID = Link.Substring(Link.IndexOf('#') + 1, Link.IndexOf('$') - Link.IndexOf('#') - 1);
        int MaxCommentID = 0;
        Label lbl;
        string MailMsg = "";


        foreach (RepeaterItem Itm in rptEmployee.Items)
        {
            chk = (CheckBox)Itm.FindControl("chkSelectEmp");
            hfEmailID = (HiddenField)Itm.FindControl("hfEmailID");
            if (chk.Checked)
            {
                lbl = (Label)Itm.FindControl("lblEmpName");
                if (Section.SaveSectionComments(ob_Section.ManualId, ob_Section.SectionId, mb.VersionNo, ob_Section.Version, "", lbl.Text, "I"))
                {
                    MaxCommentID = GetMaxCommentID();
                    Link = "bdm-" + DateTime.Now.ToBinary().ToString().Substring(1, 10)+"!"+MaxCommentID.ToString()+"~"+ManualId.ToString()+"@"+SectionId+"$"+"iok-"+DateTime.Now.ToBinary().ToString().Substring(6, 14);

                    //MailMsg=MailMsg+ SendMail.SendInviteCommentsMail(hfEmailID.Value, CC, "Comments Invitation", ManualText, "", Link, true);
                    Counter = Counter + 1;
                }
            }
        }
        
        if (Counter>0)
            lblMsgInvitation.Text = "Invitation send successfully.";
        else
            lblMsgInvitation.Text = "Please select employee.";
    }

    protected void btnCloseInvitationPopup_OnClick(object sender, EventArgs e)
    {
        dvInviteComments.Visible = false;
    }
    protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ShowEmployeeList();
    }
    
    //---------------------- USER SCREEN ------------------
    public void BindOffice()
    {
        string SQL = "Select OfficeID,OfficeName from dbo.Office";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null)
        {
            ddlOffice.DataSource = dt;
            ddlOffice.DataTextField = "OfficeName";
            ddlOffice.DataValueField = "OfficeID";
            ddlOffice.DataBind();
        }
    }
    public void ShowEmployeeList()
    {
        string SQL = "Select (PD.FirstName+' '+PD.MiddleName+' '+PD.FamilyName) EmpName,UL.Email,P.PositionCode,D.DeptName from dbo.Hr_PersonalDetails PD " +
                     " inner Join Dbo.UserLogin UL on UL.LoginID=PD.UserID " +
                     " Left Join dbo.Position P on P.PositionID=PD.Position "+
                     " Left Join dbo.HR_Department D on D.DeptID=PD.Department "+
                     " Where Office=" + ddlOffice.SelectedValue + " Order By  (PD.FirstName+' '+PD.MiddleName+' '+PD.FamilyName)";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null)
        {
            rptEmployee.DataSource = dt;
            rptEmployee.DataBind(); 
        }
    }
    public int GetMaxCommentID()
    {
        string SQL = "Select Max(CommentID)CommentID from Dbo.SMS_ManualDetails_Comments";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null)
        {
            return Common.CastAsInt32(dt.Rows[0]["CommentID"]);
        }
        else
            return 0;
    }
    
}