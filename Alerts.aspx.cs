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


public partial class PageAlerts : System.Web.UI.Page
{
    public int UserId = 0;
    int FeedId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        //lblMess.Text = ""; 
        //pnlReply.Visible = false;
        //pnlNewPost.Visible = true;
        //if (Session["UserName"] == null)
        //{
        //    Response.Redirect("Login.aspx");
        //}
        //Session["Home Page"] = "Home Page";
        UserId = int.Parse(Session["UserId"].ToString());
        ////----------------
        //cms_Alert.Visible = new AuthenticationManager(1, UserId, ObjectType.Application).IsView;
        //DataTable dt;
        //dt = Alerts.getCRMAlert();
        //if (dt.Rows.Count > 0)
        //{
        //    lbl_CRM.Visible = true;
        //    li_CRM.Visible = true;  
        //    lbl_CRM.Text = "CRM Alert (" + dt.Rows.Count + ")";
        //}
        //dt = Alerts.get_DocumentAlert();
        //if (dt.Rows.Count > 0)
        //{
        //    lbl_Other.Visible = true;
        //    li_Other.Visible = true;
        //    lbl_Other.Text = "Document Alert (" + dt.Rows.Count + ")";
        //}
        //dt = Alerts.getSignOffCrewAlert();
        //if (dt.Rows.Count > 0)
        //{
        //    lbl_SignOffCrew.Visible = true;
        //    li_SignOffCrew.Visible = true;
        //    lbl_SignOffCrew.Text = "SignOff Crew Alert (" + dt.Rows.Count + ")";
        //}
        //dt = Alerts.getVesselManningAlert();
        //if (dt.Rows.Count > 0)
        //{
        //    lbl_VesselManning.Visible = true;
        //    li_VesselManning.Visible = true;
        //    lbl_VesselManning.Text = "Vessel Manning Alert (" + dt.Rows.Count + ")";
        //}
        //dt = Common.Execute_Procedures_Select_ByQuery_CMS("select CrewNumber,Firstname+ '' +  MiddleName + ' ' + LastName as FullName,  " +
        //                            "(select vesselcode from vessel where vessel.vesselid=cpd.lastvesselid) as Vessel, " +
        //                            "(select top 1 ContractReferenceNumber from crewcontractheader cch where cch.crewid=cpd.crewid And Status='A' order by contractid desc) as RefNumber, " +
        //                            "convert(Varchar,SignOffDate,101) as SignOffDate " +
        //                            "from crewpersonaldetails cpd where cpd.signoffdate is not null and isnull(lastvesselid,0) >0 and not exists " +
        //                            "(select top 1 crewid from crewappraisaldetails cad where cad.AppraisalOccasionId=10 and cad.createdon between cpd.signoffdate and dateadd(day,8,cpd.signoffdate)) and cpd.signoffdate between getdate() and dateadd(day,30,getdate())");
        //if (dt.Rows.Count > 0)
        //{
        //    lbl_SignOffAlert.Visible = true;
        //    li_SignOffAlert.Visible = true;
        //    lbl_SignOffAlert.Text = "EOC Appraisal Alert (" + dt.Rows.Count + ")";
        //}

        //dt = Common.Execute_Procedures_Select_ByQuery("select FeedBackid,FType,CType,Descr,left(convert(varchar,PostedOn,113),17) as PostedOn,um.userid,isnull((select '[' + am.APPLICATIONNAME + '-' + mm.MODULENAME + ']' from modulemaster mm inner join applicationmaster am on am.applicationid=mm.applicationid where mm.moduleid=uf.moduleid),'') as modulename from usersfeedback uf inner join usermaster um on um.loginid=uf.postedby order by refid desc,feedbackid ");
        //if (dt!=null )
        //{
        //    //rpt_feed.DataSource = dt;
        //    //rpt_feed.DataBind();  
        //}
        //if (!IsPostBack)
        //{
        //    ddl_Modules.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT (SELECT APPLICATIONNAME FROM APPLICATIONMASTER  AM WHERE AM.APPLICATIONID=MM.APPLICATIONID)+ '-' + MODULENAME AS MODULENAME,MODULEID FROM MODULEMASTER MM");
        //    ddl_Modules.DataTextField = "ModuleName";
        //    ddl_Modules.DataValueField = "ModuleId";
        //    ddl_Modules.DataBind();
        //}
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        //if (txtMess.Text.Trim().Length > 500 || txtMess.Text.Trim().Length < 1)
        //{
        //    //Msgbox.ShowMessage("Message length must be in between (1-500) Chars.", true);
        //    lblMess.Text = "Length must be in between (1-500) Chars."; 
        //    return;
        //}
        //try
        //{
        //    if (FeedId > 0)
        //    {
        //        Common.Execute_Procedures_Select_ByQuery("Insert Into UsersFeedback (FeedbackId,FType,CType,ModuleId,RefId,Descr,PostedBy,PostedOn) values(dbo.getMaxFeedId(),'A','O',0," + FeedId.ToString() + ",'" + txtMess.Text.Trim() + "'," + UserId.ToString() + ",getdate())");
        //    }
        //    else
        //    {
        //        Common.Execute_Procedures_Select_ByQuery("Insert Into UsersFeedback (FeedBackId,FType,CType,ModuleId,RefId,Descr,PostedBy,PostedOn) values(dbo.getMaxFeedId(),'Q','" + rad_FType.SelectedValue + "'," + ddl_Modules.SelectedValue + ",dbo.getMaxFeedId(),'" + txtMess.Text.Trim() + "'," + UserId.ToString() + ",getdate())");
        //    }
        //   // Msgbox.ShowMessage("Comment posted successfully.", false);
        //    txtMess.Text = "";  
        //    lblMess.Text = "Message posted successfully."; 
        //}
        //catch
        //{
        //    lblMess.Text = "Unable to post comment.";
        //}

    }
}

