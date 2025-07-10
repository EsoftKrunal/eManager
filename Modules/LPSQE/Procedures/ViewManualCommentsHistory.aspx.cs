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

public partial class ViewManualCommentsHistory : System.Web.UI.Page
{
    public Section ob_Section;
    public ManualBO mb;    

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
        string SectionId = Convert.ToString("" + Request.QueryString["SectionId"]);
        ob_Section = new Section(ManualId, SectionId);
        if(!IsPostBack)
        {
            ShowHeader();
            //PT=His  
            ShowComments();
            if (Request.QueryString["HID"] != null)
                ShowHeader_History();
            else if (Request.QueryString["RMS"] != null)
                ShowHeader_RMS();
            

            BindOffice();
            ShowEmployeeList();
        }

    }
    public void ShowHeader()
    {
        try
        {
            ManualBO mb = new ManualBO(ob_Section.ManualId);
            lblManualName.Text = mb.ManualName;
            lblMVersion.Text = "[" + mb.VersionNo + "]";
            lblSVersion.Text = "[" + ob_Section.Version + "]";
            if (ob_Section.Status == "A")
            {
                lblHeading.Text = ob_Section.SectionId + " : " + ob_Section.Heading;
                lblContent.Text = "[ " + ob_Section.SearchTags + " ]";
            }
            else
            {
                lblHeading.Text = ob_Section.SectionId + " : " + ob_Section.Heading;
                lblContent.Text = "[ NA ]";
            }

        }
        catch { } 
    }
    public void ShowHeader_History()
    {
        try
        {
            ReadHistorySection on_rhs = new ReadHistorySection(Common.CastAsInt32(Request.QueryString["HID"]));
            lblSVersion.Text = "[" + on_rhs.Version + "]";
            if (on_rhs.Status == "A")
            {
                lblHeading.Text = on_rhs.SectionId + " : " + on_rhs.Heading;
                lblContent.Text = "[ " + on_rhs.SearchTags + " ]";
            }
            else
            {
                lblHeading.Text = on_rhs.SectionId + " : " + on_rhs.Heading;
                lblContent.Text = "[ NA ]";
            }

        }
        catch { }
    }
    public void ShowHeader_RMS()
    {
        try
        {
            ReadSection on_rhs = new ReadSection(ob_Section.ManualId, ob_Section.SectionId);
            lblSVersion.Text = "[" + on_rhs.Version + "]";
            if (on_rhs.Status == "A")
            {
                lblHeading.Text = on_rhs.SectionId + " : " + on_rhs.Heading;
                lblContent.Text = "[ " + on_rhs.SearchTags + " ]";
            }
            else
            {
                lblHeading.Text = on_rhs.SectionId + " : " + on_rhs.Heading;
                lblContent.Text = "[ NA ]";
            }

        }
        catch { }
    }
    public void ShowComments()
    {
        string sManVersion, sSecVersion,Status;
        if (Request.QueryString["SecVersion"] != null)
        {
            sManVersion = Request.QueryString["ManVersion"].ToString();
            sSecVersion = Request.QueryString["SecVersion"].ToString();
            Status = "Close";
            dvInviteCommentsBtn.Visible = false;
        }
        else
        {
            ManualBO mb = new ManualBO(ob_Section.ManualId);
            sManVersion = mb.VersionNo;
            sSecVersion = ob_Section.Version;
            Status = "";
        }

        DataTable dtComments = ob_Section.getComments(sManVersion, sSecVersion, Status);
        StringBuilder sb = new StringBuilder();
        int Counter = 1;
        foreach (DataRow dr in dtComments.Rows)
        {
            sb.Append("<div class='enteredby'>" + (Counter++).ToString()  + ". " + dr["EnteredBy"].ToString() + " / " + Convert.ToDateTime(dr["EnteredOn"]).ToString("dd-MMM-yyyy") + " : </div><div class='comm' >" + dr["Comments"].ToString() + "</div>"); 
        }
        litHistory.Text = sb.ToString();
    }
    public void ShowMessage(string Mess, bool error)
    {
        lblMsg.Text = Mess;
        lblMsg.ForeColor=(error)?System.Drawing.Color.Red:System.Drawing.Color.Green;    
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ManualBO mb = new ManualBO(ob_Section.ManualId);

        if (txtComments.Text.Trim() == "")
        {
            ShowMessage("Please enter some comments to save.",true);
            txtComments.Focus();  
            return; 
        }
        //----------------------
        if (Section.SaveSectionComments(ob_Section.ManualId, ob_Section.SectionId, mb.VersionNo, ob_Section.Version, txtComments.Text.Trim(), Session["UserName"].ToString(),"I"))
      {
          ShowMessage("Comments saved successfully.",false);
      }
      else
      {
          ShowMessage("Unable to save comments.",true);
      }
      ShowComments();
    }


    // ----- Invite Comments
    protected void btnInviteCommentsPopup_OnClick(object sender, EventArgs e)
    {
        dvInviteComments.Visible = true;
    }
    protected void btnSendInvitation_OnClick(object sender, EventArgs e)
    {
        //ob_Section = new Section(Common.CastAsInt32(ob_Section.ManualId), ob_Section.SectionId);
        mb = new ManualBO(ob_Section.ManualId);
        CheckBox chk;
        HiddenField hfEmailID;
        string[] CC = { };
        string Link = "";
        int Counter = 0;
        //id = Common.CastAsInt32(Link.Substring(14, Link.IndexOf('?') - 14));   
        //sssMID = Common.CastAsInt32(Link.Substring(Link.IndexOf('?') + 1, Link.IndexOf('#') - Link.IndexOf('?') - 1));
        //sssSID = Link.Substring(Link.IndexOf('#') + 1, Link.IndexOf('$') - Link.IndexOf('#') - 1);
        int MaxCommentID = 0;
        Label lbl;
        HiddenField hfPositionName;
        string MailMsg = "";


        foreach (RepeaterItem Itm in rptEmployee.Items)
        {
            chk = (CheckBox)Itm.FindControl("chkSelectEmp");
            hfEmailID = (HiddenField)Itm.FindControl("hfEmailID");
            if (chk.Checked)
            {
                lbl = (Label)Itm.FindControl("lblEmpName");
                hfPositionName = (HiddenField)Itm.FindControl("hfPositionName");

                if (Section.SaveSectionComments(ob_Section.ManualId, ob_Section.SectionId, mb.VersionNo, ob_Section.Version, "", lbl.Text, "I"))
                {
                    MaxCommentID = GetMaxCommentID();
                    Link = "bdm-" + DateTime.Now.ToBinary().ToString().Substring(1, 10) + "!" + MaxCommentID.ToString() + "~" + ob_Section.ManualId + "@" + ob_Section.SectionId + "$" + "iok-" + DateTime.Now.ToBinary().ToString().Substring(6, 14);

                    MailMsg = MailMsg + SendMail.SendInviteCommentsMail(Session["EmailAddress"].ToString(), hfEmailID.Value, CC, "Manual Change-Review Request", lblManualName.Text.Trim(), lblHeading.Text.Trim(), "", Link, Session["UserName"].ToString(), hfPositionName.Value, true);
                    Counter = Counter + 1;
                }
            }
        }

        if (Counter > 0)
            lblMsgInvitation.Text = "Invitation send successfully." + MailMsg;
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
        string SQL = "Select (PD.FirstName+' '+PD.MiddleName+' '+PD.FamilyName) EmpName,UL.Email,P.PositionCode,P.PositionName,D.DeptName from dbo.Hr_PersonalDetails PD " +
                     " inner Join Dbo.UserLogin UL on UL.LoginID=PD.UserID " +
                     " Left Join dbo.Position P on P.PositionID=PD.Position " +
                     " Left Join dbo.HR_Department D on D.DeptID=PD.Department " +
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
