using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;

/// <summary>
/// Page Name            : VendorRequest.aspx
/// Purpose              : Listing Of Files Vendor Request
/// Author               : Laxmi Verma
/// Developed on         : 23 September 2015
/// </summary>

public partial class Vendor : System.Web.UI.Page
{

    #region ---------- PageLoad ------------
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        lblOuterMessage.Visible = false;

        if (!IsPostBack)
        {
            //---------------------------------------
            ProjectCommon.SessionCheck();
            //---------------------------------------
            #region --------- USER RIGHTS MANAGEMENT -----------
            try
            {
                AuthenticationManager auth = new AuthenticationManager(1065, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
                if (!(auth.IsView))
                {
                    Response.Redirect("~/NoPermission.aspx", false);
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
            }
            #endregion ---------------------------------------- 
            btnSearchProfile_Click(sender, e);
        }
    }
    #endregion
    // Events ----------------------------------------------------------    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    protected void rptVendorRequest_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string ID = DataBinder.Eval(e.Item.DataItem, "VRID").ToString();
        ImageButton btnedit = (ImageButton)e.Item.FindControl("ImgEdit");
        hdn_VIDVendor.Value = ID;
    }
    protected void rptVendorRequest_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ImageButton btnmail = (ImageButton)e.Item.FindControl("ImgEmail");
        ImageButton btnsttaus = (ImageButton)e.Item.FindControl("ImgRequestStatus");

        if (e.CommandName == "emailToVendor")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string guid = commandArgs[0];
            string emailaddress = commandArgs[1];
            SendMail(Convert.ToString(guid), emailaddress);
            ShowOuterMessage("Mail sent successfully.", false);
        }
        if (e.CommandName == "EditVednor")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string guid = commandArgs[0];
            string vrid = commandArgs[1];
            ScriptManager.RegisterStartupScript(this, this.GetType(), "afdf", "window.open('ModifyVendorProfile.aspx?VRID=" + vrid + "');", true);
        }
        if (e.CommandName == "ActivityStatus")
        {
            int ID = Common.CastAsInt32(e.CommandArgument);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('ModifyVendorProfile_Proposal.aspx?VRID=" + ID + "');", true);
        }
        if (e.CommandName == "AllowResubmit")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string guid = commandArgs[0];
            string vrid = commandArgs[1];
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.tbl_VenderRequest SET AllowToEditByMail='Y' WHERE VRID=" + vrid);
            ShowOuterMessage("Allowed successfully.", false);
        }

    }
    protected void btnSearchProfile_Click(object sender, EventArgs e)
    {
        BindVendor();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }
    //for  first/second approval
    protected void btn_SaveRequestStatus_Click(object sender, EventArgs e)
    {
        //Common.Set_Procedures("dbo.sp_RequestApprval");
        //Common.Set_ParameterLength(10);

        //if (rd_ApprovalAcrion.SelectedValue == "A")
        //{
        //    if (txt_ApprovedBy.Text != "" && txt_ApprovedPosition.Text != "" && txt_ValidityDate.Text != "" && ddlApprovalType.SelectedIndex != 0 && txt_ApprovedRemakrs.Text != "")
        //    {
        //        if (Convert.ToDateTime(txt_ValidityDate.Text) > DateTime.Now.Date)
        //        {
        //            Common.Set_Parameters(new MyParameter("@VRID", Common.CastAsInt32(hdn_VRID.Value)),
        //            new MyParameter("@ApprovedBy", txt_ApprovedBy.Text.Trim().Replace("'", "`")),
        //            new MyParameter("@ApprovedPosition", txt_ApprovedPosition.Text.Trim().Replace("'", "`")),
        //            new MyParameter("@ApprovalType", Common.CastAsInt32(ddlApprovalType.SelectedValue)),
        //            new MyParameter("@ApprovedRemarks", txt_ApprovedRemakrs.Text.Trim().Replace("'", "`")),

        //            new MyParameter("@CurrentStatus", Common.CastAsInt32(ddlApprovalType.SelectedValue)),
        //            new MyParameter("@CurrentStatusBy", txt_ApprovedBy.Text.Trim().Replace("'", "`")),
        //            new MyParameter("@RequestApprovalStatus", Common.CastAsInt32(hdn_RequestStatus.Value)),
        //            new MyParameter("@ApprovalAction", 'A'),
        //            new MyParameter("@ValidTill", txt_ValidityDate.Text)

        //            );
        //        }
        //        else
        //        {
        //            ShowMessage("Validate date should be greater than current date", true);
        //        }
        //    }
        //    else
        //    {
        //        ShowMessage("Please enter all details", true);
        //        return;
        //    }
        //}
        //else
        //{
        //    if (txt_ApprovedBy.Text != "" && txt_ApprovedPosition.Text != "" && txt_ApprovedRemakrs.Text != "")
        //    {
        //        Common.Set_Parameters(new MyParameter("@VRID", Common.CastAsInt32(hdn_VRID.Value)),
        //            new MyParameter("@ApprovedBy", txt_ApprovedBy.Text.Trim().Replace("'", "`")),
        //            new MyParameter("@ApprovedPosition", txt_ApprovedPosition.Text.Trim().Replace("'", "`")),
        //            new MyParameter("@ApprovalType", Common.CastAsInt32(ddlApprovalType.SelectedValue)),
        //            new MyParameter("@ApprovedRemarks", txt_ApprovedRemakrs.Text.Trim().Replace("'", "`")),

        //            new MyParameter("@CurrentStatus", 0),
        //            new MyParameter("@CurrentStatusBy", ""),
        //            new MyParameter("@RequestApprovalStatus", Common.CastAsInt32(hdn_RequestStatus.Value)),
        //            new MyParameter("@ApprovalAction", 'R'),
        //            new MyParameter("@ValidTill", DBNull.Value)

        //            );
        //    }
        //    else
        //    {
        //        ShowMessage("Please enter all details", true);
        //        return;
        //    }
        //}          

        //DataSet DT = new DataSet();
        //if (Common.Execute_Procedures_IUD(DT))
        //{
        //    txt_ApprovedBy.Text = "";
        //    txt_ApprovedPosition.Text = "";
        //    ddlApprovalType.SelectedIndex = 0;
        //    txt_ApprovedRemakrs.Text = "";
        //    txt_ValidityDate.Text = "";
        //    modalBox.Visible = false;
        //    modalframeVendroProfile.Visible = false;
        //    BindVendor();
        //}
        //else
        //{
        //    ShowMessage("Unable to save record.", true);
        //}
    }
    protected void btn_closemodel_Click(object sender, EventArgs e)
    {
        //ddlApprovalType.SelectedIndex = 0;
        modalBox.Visible = false;
        modalframeVendroProfile.Visible = false;
    }
    protected void btn_Proposal_Click(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "showmsg1", "VendorActions('" + hdn_VIDVendor.Value + "');", true);
        modalBox.Visible = false;
        modalframeVendroProfile.Visible = false;
        //Response.Redirect("~/ModifyVendorProfile_Proposal.aspx?_ProfileId=" + hdn_VIDVendor.Value);
    }
    protected void btn_Approval_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    // Function ----------------------------------------------------------
    public void BindVendor()
    {
        string emailaddress, compnayname, Activity, StatusFilter;
        string sql = "select * from VW_VEndorRequest ";
        //string whereclause = "WHERE ( RequestApprovalStatus <> 5 )";
        string whereclause = "WHERE ( not (requestapprovalstatus=5 and currentstatus is not null and isnull(supplierid,0) >0 ) )";

        compnayname = txt_CompanyName.Text.Trim().Replace("'", "`");
        emailaddress = txt_EmailAddress.Text.Trim().Replace("'", "`");
        Activity = ddlActivity.SelectedItem.Text;
        StatusFilter = ddlStatus.SelectedItem.Text;
        if (compnayname != "")
        {
            whereclause += "and CompanyName like '%" + compnayname + "%' ";
        }
        if (emailaddress != "")
        {
            whereclause += "and EmailAddress like '%" + emailaddress + "%' ";
        }
        if (ddlActivity.SelectedIndex != 0)
        {
            whereclause += "and ActivityStatus like '%" + Activity + "%' ";
        }
        if (ddlStatus.SelectedIndex != 0)
        {
            whereclause += "and statusText like '%" + StatusFilter + "%' ";
        }

        DataTable DTVendorRequest = Common.Execute_Procedures_Select_ByQuery(sql + whereclause + "order by CompanyName");
        rptVendorRequest.DataSource = DTVendorRequest;
        rptVendorRequest.DataBind();
    }
    public void SendMail(string guid, string emailid)
    {
        string[] ToAdd = { emailid };
        string useremail = ProjectCommon.getUserEmailByID(Session["loginid"].ToString());
        string[] NoAdd = { "" };
        string[] CcAdd = { useremail };
        string[] BccAdd = { "emanager@energiossolutions.com" };
        //string LinkText = "http://localhost:50192/public/VendorManagement/ModifyVendorProfile.aspx?_key=" + guid;
        string LinkText = ConfigurationManager.AppSettings["UpdateVendorProfile"].ToString()+ guid;
        string space = "</br></br>";
        string spacesingle = "</br>";
        string Message = "Dear Sirs/Madam" + space +
                         "We’re in the midst of refreshing, re-evaluating & verifying our existing vendors in our approved vendors list." + space +
                         "As part of our commitment to continually improve our vendor management process and keeping the latest update of all our vendor’s profile is just one of them. " + space +
                         "In line with this policy, we request you furnish the latest details of your esteemed company via the link below:- " + space +
                         "<a href='" + LinkText + "' target='_blank'>Click Here</a>" + space +
                         "OR" + space +
                         "copy the link " + space +
                         LinkText +
                         " and paste in a browser new window to update the profile." + space +
                         "We take this opportunity to thank you for your continued support to our common interests & our goal of achieving excellence in Vendor Management. " + space + space +
                         "Thank you with best regards" + space +
                         "<b>Vendor Management<b>" + spacesingle +
                         "";

        //string Message = "Dear Vendor/Supplier,<br><br>Your request successfully received by us.<br><br>Below is the link given to update your full profile.<br><br> <a href='" + LinkText + "' target='_blank'>Click Here</a> <br>OR<br>copy the link <br>" + LinkText + "<br><br>and paste in a browser new window to update the profile.<br><br> Once you submit your profile to use we will continue your approval process.<br><br><br>Thanks<br>";
        
        if (useremail.Trim() != "")
            NoAdd[0] = useremail;
        ProjectCommon.SendMail(ToAdd, CcAdd, BccAdd, "Vendor/Supplier Registeration Verfication", Message, NoAdd);
    }
    protected void rd_ApprovalAcrion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rd_ApprovalAcrion.SelectedIndex == 0)
        //{
        //    div_approvaltype.Visible = true;
        //}
        //else 
        //{
        //    div_approvaltype.Visible = false;
        //}
    }
    public void ShowMessage(string Msg, bool Error)
    {
        lblMessage.Visible = true;
        if (Error && Common.ErrMsg.Trim() != "")
            lblMessage.InnerHtml = Msg + " Error :" + Common.ErrMsg;
        else
            lblMessage.InnerHtml = Msg;
        lblMessage.Attributes.Add("class", (Error) ? "msgbox error" : "msgbox success");

    }
    public void ShowOuterMessage(string Msg, bool Error)
    {
        lblOuterMessage.Visible = true;
        if (Error && Common.ErrMsg.Trim() != "")
            lblOuterMessage.InnerHtml = Msg + " Error :" + Common.ErrMsg;
        else
            lblOuterMessage.InnerHtml = Msg;
        lblOuterMessage.Attributes.Add("class", (Error) ? "msgbox error" : "msgbox success");
    }
    // -------------------------------------------------------------------
}
