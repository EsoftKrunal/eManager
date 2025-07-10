using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;

public partial class ViewManualSection : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public static Random r = new Random();
    public Section ob_Section;
    public ManualBO mb;    
    public string LastSectionId="";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        lblMsgAppReq.Text = "";
        lblMsgInvitation.Text = "";   
        //EnableDisable();
        if (Request.UrlReferrer.ToString().Contains("AddSection.aspx"))
        {
            Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);
            ReloadHeadings();
        }
        if (Page.Request.QueryString["AddSection"]!=null && Page.Request.QueryString["AddSection"].ToString() == "No")
        {
            Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);
            dvAdd.Style.Add("display", "none");
            dvDelete.Style.Add("display", "none");
            //dvEdit.Style.Add("display", Auth.IsUpdate == true ? "block" : "none");
            dvEdit.Visible = Auth.IsUpdate;
        }
        else
        {
            Auth = new AuthenticationManager(1057, UserId, ObjectType.Page);
            //dvAdd.Style.Add("display", Auth.IsAdd == true ? "block" : "none");
            //dvEdit.Style.Add("display", Auth.IsUpdate == true ? "block" : "none");
            //dvDelete.Style.Add("display", Auth.IsDelete == true ? "block" : "none");

            dvAdd.Visible = Auth.IsAdd;
            dvEdit.Visible = Auth.IsUpdate;
            dvDelete.Visible = Auth.IsDelete;
        }
        EnableDisable();
        dvHistory.Visible = false;

        if (!Page.IsPostBack)
        {
            BindOffice();
            ShowEmployeeList();
        }
    }
    
    public void EnableDisable()
    {
        int ManualId = Common.CastAsInt32("" + Request.QueryString["ManualId"]);
        string SectionId = Convert.ToString("" + Request.QueryString["SectionId"]);
        ob_Section = new Section(ManualId, SectionId);
        ManualBO mb=new ManualBO(ManualId);
        lblManualName.Text = mb.ManualName;
        lblMVersion.Text = "[" + mb.VersionNo + "]";
        lblSVersion.Text = "[" + ob_Section.Version + "]";
        LastSectionId = SectionId;
        if (ManualId <= 0)
        {
            dvAdd.Visible = false;
        }
        frmFile.Attributes.Add("src", "ReadManualSection.aspx?ManualId=" + ManualId.ToString() + "&SectionId=" + SectionId); 
        if (ob_Section.SectionId.Trim() == "")
        {
            dvEdit.Visible = false;
            dvDelete.Visible = false;
            dvActivate.Visible = false;
            dvComments.Visible=false;
            dvForms.Visible = false;
            dvPopUp.Visible = false;
            dvAppReq.Visible = false; 
        }
        else
        {
            if (ob_Section.Status == "A")
            {
                //dvAdd.Visible = true;
                //dvEdit.Visible = true;
                //dvDelete.Visible = true;

                dvAdd.Visible = Auth.IsAdd;
                dvEdit.Visible = Auth.IsUpdate;
                dvDelete.Visible = Auth.IsDelete;

                dvActivate.Visible = false;
                dvComments.Visible = true;
                lblHeading.Text = SectionId + " : " + ob_Section.Heading;
                lblContent.Text = "[ " + ob_Section.SearchTags + " ]";
                if (ob_Section._Approved!="A")
                    dvAppReq.Visible = true; 
                else
                    dvAppReq.Visible = false; 
            }
            else
            {
                dvAdd.Visible = false;
                dvEdit.Visible = false;
                dvDelete.Visible = false;
                dvActivate.Visible = true;
                dvComments.Visible = true;
                lblHeading.Text = SectionId + " : " + ob_Section.Heading;
                lblContent.Text = "[ NA ]";
                dvAppReq.Visible = false; 
            }
            
            ReloadImages();
            dvHistory.Visible = false;  
        }
    }
    public void ReloadHeadings()
    {
        EnableDisable();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "ab", "ReloadHeadings();", true);  
    }
    public void ReloadImages()
    {
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "ReloadImages(" + ob_Section.ManualId.ToString() + ",'" + ob_Section.SectionId.ToString() + "');", true);
    }
    protected void Activate_Section(object sender, EventArgs e)
    {
        ob_Section.Activate();
        ReloadHeadings();
    }
    protected void Delete_Section(object sender, EventArgs e)
    {
        ob_Section.Delete();
        ReloadHeadings();
    }
    protected void ReqApprove_Section(object sender, EventArgs e)
    {
        dvSendRequestForApproval.Visible = true;
        txtAppReqestComments.Text = "";
        //if (Section.RequestApproval(ob_Section.ManualId, ob_Section.SectionId,""))
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "ab", "alert('Approval reuqested successfully.');", true);  
        //}
    }
    protected void btnSubmitPopupAdmin_Click(object sender, EventArgs e)
    {
        if (txtAppReqestComments.Text.Trim() == "")
        {
            lblMsgAppReq.Text = "Please enter comments.";
            return;
        }
        dvSendRequestForApproval.Visible = false;
        if (Section.RequestApproval(ob_Section.ManualId, ob_Section.SectionId, txtAppReqestComments.Text.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ab", "alert('Approval reuqested successfully.');", true);
        }
        dvSendRequestForApproval.Visible = false;
    }
    protected void btnCloseSubmitPopupAdmin_Click(object sender, EventArgs e)
    {
        dvSendRequestForApproval.Visible = false;
    }




    protected void btnAttachment_Click(object sender, ImageClickEventArgs e)
    {
        int rnd = r.Next(10000);
        OpenAttachment();
        frmFile.Attributes.Add("src", "ReadManualSection.aspx?" + rnd); 
    }
    protected void lblFileName_Click(object sender, EventArgs e)
    {
        int rnd = r.Next(10000);
        OpenAttachment();
        frmFile.Attributes.Add("src", "ReadManualSection.aspx?" + rnd); 
    }
    public void OpenAttachment()
    {
        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", lblFileName.Text));
        //Response.ContentType = "application/" + Path.GetExtension(lblFileName.Text).Substring(1);
        //Response.BinaryWrite(ob_Section.getContentFile());
        File.WriteAllBytes( Server.MapPath("~/SMS/Manual.pdf"),ob_Section.getContentFile());
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

                    MailMsg = MailMsg + SendMail.SendInviteCommentsMail(Session["EmailAddress"].ToString(), hfEmailID.Value, CC, "Manual Change-Review Request", lblManualName.Text.Trim(), lblHeading.Text.Trim(),"", Link, Session["UserName"].ToString(), hfPositionName.Value, true);
                    Counter = Counter + 1;
                }
            }
        }

        if (Counter > 0)
            lblMsgInvitation.Text = "Invitation send successfully."+MailMsg ;
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
