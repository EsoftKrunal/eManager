using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Text;
using Ionic.Zip;

public partial class SeminarPresenter : System.Web.UI.Page
{
    public int SeminarId
    {
        get
        {return Common.CastAsInt32(ViewState["SeminarId"]);}
        set { ViewState["SeminarId"] = value; }
    }
    public int TableId
    {
        get
        { return Common.CastAsInt32(ViewState["TableId"]); }
        set { ViewState["TableId"] = value; }
    }
    public int UserId
    {
        get
        { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public string SelGuid
    {
        get
        { return ViewState["SelGuid"].ToString(); }
        set { ViewState["SelGuid"] = value; }
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //------------------------------------
        
        //------------------------------------
        if (!Page.IsPostBack)
        {
            SeminarId = Common.CastAsInt32(Request.QueryString["SeminarId"]);
            UserId =Common.CastAsInt32(Session["LoginId"]);
            LoadStatus();
            LoadRank();
            LoadRecruitingOffice();
            //LoadEmployees();
            ShowRecord();            
            ShowInviteDetails();
        }
    }
    public void LoadStatus()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.CrewStatus ORDER BY CrewStatusName");
        ddlCrewStatus.DataSource = dt;
        ddlCrewStatus.DataTextField = "CrewStatusName";
        ddlCrewStatus.DataValueField = "CrewStatusId";
        ddlCrewStatus.DataBind();
        ddlCrewStatus.Items.Insert(0, new ListItem(" -- Select --- ", "0"));
    }
    public void LoadRecruitingOffice()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RecruitingOffice ORDER BY RecruitingOfficeName");
        ddlRecuitingOffice.DataSource = dt;
        ddlRecuitingOffice.DataTextField = "RecruitingOfficeName";
        ddlRecuitingOffice.DataValueField = "RecruitingOfficeId";
        ddlRecuitingOffice.DataBind();
        ddlRecuitingOffice.Items.Insert(0, new ListItem(" -- Select --- ", "0"));
    }
    public void LoadRank()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RANK ORDER BY RANKLEVEL");
        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "RankCode";
        ddlRank.DataValueField = "RankId";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem(" -- Select --- ", "0"));
    }
    
    
    public void ShowRecord()
    {
        DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * from DBO.vw_tbl_Seminar where SeminarId=" + SeminarId);
        if (DT.Rows.Count > 0)
        {
            lblCategoryName.Text = DT.Rows[0]["SeminarCatName"].ToString();
            lblOfficeName.Text = DT.Rows[0]["OfficeName"].ToString();
            lblDuration.Text = Common.ToDateString(DT.Rows[0]["StartDate"]) + " to " + Common.ToDateString(DT.Rows[0]["EndDate"]);
            lblEventLocation.Text = DT.Rows[0]["Location"].ToString();
            lblRemarks.Text = DT.Rows[0]["Topic"].ToString();
            ddlRecuitingOffice.SelectedValue = DT.Rows[0]["OfficeId"].ToString();
            ddlRecuitingOffice.Enabled = false;

            lblContactPerson.Text = DT.Rows[0]["ContactPerson"].ToString();
            lblContactNumber.Text = DT.Rows[0]["ContactNumber"].ToString();
            lblContactEmail.Text = DT.Rows[0]["ContactEmail"].ToString();

            //----------------------------------

            //DT = Common.Execute_Procedures_Select_ByQuery("select * from dbo.tbl_SeminarPresenters where SeminarId=" + SeminarId);
            //foreach (ListItem li in chkPresenters.Items)
            //{
            //    if (DT.Select("PresenterId=" + li.Value).Length > 0)
            //        li.Selected = true;
            //}
        }

    }

    
    
    
    
    protected void btDownloadAttachment_Click(object sender, EventArgs e)
    {
        TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tbl_SeminarAgenda WHERE TABLEID=" + TableId);
        if (TableId > 0)
        {
            string filename = dt.Rows[0]["AttachmentName"].ToString();
            byte[] filedata = (byte[])dt.Rows[0]["Attachment"];
            //--------------
            Response.Clear();
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(filedata);
            Response.Flush();
            Response.End();
        }
    }
    
    
    protected void btnDeleteInvite_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (TableId > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.tbl_SeminarInvite WHERE TABLEID=" + TableId);
            ShowInviteDetails();
        }
    }
    protected void btnInvite_Click(object sender, EventArgs e)
    {
        divAttendies.Visible = true;
    }
    //public void LoadEmployees()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT USERID,FIRSTNAME + ' ' + MIDDLENAME + ' ' + FAMILYNAME AS EMPNAME FROM DBO.Hr_PersonalDetails WHERE USERID IN (SELECT PresenterId FROM DBO.tbl_SeminarPresenters where SeminarId=" + SeminarId + ") ORDER BY EMPNAME");
    //    ddlPresenter.DataSource = dt;
    //    ddlPresenter.DataTextField = "EMPNAME";
    //    ddlPresenter.DataValueField = "USERID";
    //    ddlPresenter.DataBind();
    //    ddlPresenter.Items.Insert(0, new ListItem("----- Select -----", "0"));
    //}
    public void LoadCrewList()
    {
        string WhereClause = "WHERE P.CREWID NOT IN (SELECT CREWID FROM DBO.tbl_SeminarInvite WHERE SEMINARID=" + SeminarId + ") ";

        if (txtcrewn.Text.Trim() != "")
            WhereClause += " AND CREWNUMBER='" + txtcrewn.Text.Trim() + "'";
        if (txtCrewName.Text.Trim() != "")
            WhereClause += " AND FIRSTNAME + ' ' + MiddleName + ' ' + LastName LIKE '%" + txtCrewName.Text.Trim() + "%'";
        if (ddlCrewStatus.SelectedIndex > 0)
            WhereClause += " AND P.CrewStatusId=" + ddlCrewStatus.SelectedValue;
        if (ddlRecuitingOffice.SelectedIndex > 0)
            WhereClause += " AND P.RecruitmentOfficeId=" + ddlRecuitingOffice.SelectedValue;
        if (ddlRank.SelectedIndex > 0)
            WhereClause += " AND P.CurrentRankId=" + ddlRank.SelectedValue;
        if (txtCity.Text!="")
            WhereClause += " AND City LIKE '%" + txtCity.Text + "%'";
        

        if (ddlOR.SelectedIndex > 0)
            WhereClause += " AND P.CurrentRankId IN (SELECT RR.RANKID FROM DBO.RANK RR WHERE OffCrew='" + ddlOR.SelectedValue + "') ";

        string perrec = "";
        if (ddlnor.SelectedItem.Text != "ALL")
            perrec = " TOP " + ddlnor.SelectedItem.Text;

        string sql = "SELECT " + perrec + "  P.CREWID,CREWNUMBER,FIRSTNAME + ' ' + MiddleName + ' ' + LastName AS CREWNAME,CrewStatusName AS CREWSTATUS,RecruitingOfficeName,RankCode,City " +
                      "FROM  " +
                      "DBO.CREWPERSONALDETAILS P  " +
                      "INNER JOIN DBO.CrewStatus S ON P.CrewStatusId=S.CrewStatusId " +
                      "LEFT JOIN DBO.CREWCONTACTDETAILS C ON  C.CrewId=P.CrewId and C.AddressType='C' " +
                      "INNER JOIN DBO.Rank R1 ON R1.RankId=P.CurrentRankId " +
                      "INNER JOIN DBO.RecruitingOffice R ON P.RecruitmentOfficeId=R.RecruitingOfficeId " + WhereClause + " ORDER BY CREWNAME";

        rptCrewList.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptCrewList.DataBind();
    }
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rptCrewList.Items)
        {
            CheckBox ch = ((CheckBox)ri.FindControl("chkSelect"));
            if (ch.Checked)
            {
                Common.Set_Procedures("DBO.InsertUpdateSeminarInvite");
                Common.Set_ParameterLength(4);
                Common.Set_Parameters(

                    new MyParameter("@TableId", TableId),
                    new MyParameter("@SeminarId", SeminarId),
                    new MyParameter("@CrewIdList", ch.CssClass),
                    new MyParameter("@ModifiedBy", UserId));

                DataSet ds = new DataSet();
                Common.Execute_Procedures_IUD(ds);
            }
        }
        ShowInviteDetails();
        ShowMessage("Record saved sucessfully.", false);
    }
    protected void btnClose2_Click(object sender, EventArgs e)
    {
        divAttendies.Visible = false;
    }
    public void ShowInviteDetails()
    {
        string whereclause = "";
        if (ddlStatus.SelectedIndex != 0)
        {
            whereclause = " and SP.ReplyStatus='"+ddlStatus.SelectedValue+"'";
        }
        string sql = "SELECT ROW_NUMBER() OVER(ORDER BY CREWNUMBER) AS SNO,TableId,sp.CREWID,CREWNUMBER,CREWNAME,CrewStatusName AS CREWSTATUS,RecruitingOfficeName,SP.RequstedOn,(CASE WHEN SP.ReplyStatus='P' then 'Yes' WHEN SP.ReplyStatus='A' then 'No' WHEN SP.ReplyStatus='N' then 'TBC'  else '' end) as ReplyStatus,SP.ReplyStatus as ReplyStatusCode,SP.RepliedOn,R1.RankCode,SP.City,GUID,EMAIL  " +
                    "from " +
                    "DBO.tbl_SeminarInvite SP  " +
                    "INNER JOIN DBO.CREWPERSONALDETAILS P ON SP.CrewId=P.CrewId  " +
                    "INNER JOIN DBO.CrewStatus S ON P.CrewStatusId=S.CrewStatusId " +
                    "INNER JOIN DBO.Rank R1 ON R1.RankID=P.CurrentRankID " +
                    "INNER JOIN DBO.RecruitingOffice R ON P.RecruitmentOfficeId=R.RecruitingOfficeId WHERE SP.SEMINARID=" + SeminarId;


        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql + whereclause);
        rptInvite.DataSource = DT;
        rptInvite.DataBind();
    }
    protected void btnInviteMail_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rptInvite.Items)
        {
            CheckBox ch = ((CheckBox)ri.FindControl("chkSelect"));
            if (ch.Checked)
            {
                string GUID=((HiddenField)ri.FindControl("hfdGUID")).Value;
                string Email=((HiddenField)ri.FindControl("hfdEmail")).Value;
                string RankCode = ((HiddenField)ri.FindControl("lblRankCode")).Value;
                string CrewName = ((HiddenField)ri.FindControl("lblName")).Value;

                Email = "pankaj.k@esoftech.com";
                if (Email.Trim() != "")
                {

                    string[] ToAddress={Email};
                    string[] NoAddress={};
                    string Link = "http://emanagershore.energiosmaritime.com/public/Seminar/ConfirmAttendance.aspx?_K=" + GUID;
                    //string Link = "http://localhost:50192/public/Seminar/ConfirmAttendance.aspx?_K=" + GUID;
                    StringBuilder Message = new StringBuilder("Dear " + CrewName + " ( " + RankCode + " )," + "<br/><br/>");
                    Message.Append("Event Duration : " + lblDuration.Text + "<br/><br/>");
                    Message.Append("Location : " + lblEventLocation.Text + "<br/><br/>");
                    Message.Append("You are requested to confirm your attendance to join the above event." + "<br/><br/>");
                    Message.Append("For confirmation:" + "<br/><br/>");
                    Message.Append("Please <a href='" + Link + "'>Click Here</a> OR copy below url in your Internet browser." + "<br/><br/>");
                    Message.Append(Link + "<br/><br/>");
                    Message.Append("For further details you can contact to  your  MTM Crew Welfare officer." + "<br/><br/>");
                    Message.Append("Look forward to see you." + "<br/><br/><br/>");
                    Message.Append("Thanks," + "<br/><br/>");
                    Message.Append("eMANAGER");

                    string Error = "";
                    if (SendMail.SendeMailAsync("emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAddress, NoAddress, NoAddress, "Seminar Attendance Confirmation", Message.ToString(), out Error, ""))
                    {
                        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.tbl_SeminarInvite SET RequstedOn=GETDATE() WHERE GUID='" + GUID + "'");
                    }
                }
            }
        }
        
        ShowInviteDetails();
    }
    protected void btEditAttendies_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfGUID = (HiddenField)btn.FindControl("hfdGUID");
        HiddenField hfReplyStatusCode = (HiddenField)btn.FindControl("hfReplyStatusCode");
        SelGuid = hfGUID.Value;
        try
        {
            rdoGrade.SelectedValue = hfReplyStatusCode.Value;
        }catch{ rdoGrade.SelectedIndex = -1; }
        divEditAttendies.Visible = true;
    }
    protected void btnUpdateAttendies_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.tbl_SeminarInvite SET REPLYSTATUS='" + rdoGrade.SelectedValue + "',RepliedOn=GETDATE() WHERE GUID='" + SelGuid + "'");
        lblMessageAttendies.Text = "Data saved successfully.";
        ShowInviteDetails();
    }
    protected void btnCloseAttendies_Click(object sender, EventArgs e)
    {
        divEditAttendies.Visible = false;
        SelGuid = "";
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadCrewList();
    }

    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ShowInviteDetails();
    }

    public void ShowMessage(string Message, bool Error)
    {
        lblMessage1.Text = Message;
        lblMessage1.ForeColor = (Error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }
    //---------
}

