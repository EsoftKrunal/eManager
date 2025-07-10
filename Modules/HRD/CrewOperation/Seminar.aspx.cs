using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;

public partial class Seminar : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
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
        SessionManager.SessionCheck_New();
        //------------------------------------
        lblMessageAttendies.Text = "";
       if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Session["UserName"].ToString();
            BindOffice();
            BindSeminarCategory();
            Bindgrid();
        }
    }
    protected void Filter_Visits(object sender, EventArgs e)
    {
        Bindgrid();
    }
    private void BindOffice()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RecruitingOffice ORDER BY RecruitingOfficeName");
        
        ddloffice.DataSource = dt;
        ddloffice.DataTextField = "RecruitingOfficeName";
        ddloffice.DataValueField = "RecruitingOfficeId";
        ddloffice.DataBind();
        ddloffice.Items.Insert(0, new ListItem(" -- All --- ", "0"));
    }
    private void BindSeminarCategory()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tbl_SeminarCat ORDER BY SeminarCatName");
        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "SeminarCatName";
        ddlCategory.DataValueField = "SeminarCatId";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem(" -- All --- ", "0"));
    }
    
    protected void Bindgrid()
    {
        string sql = "SELECT ROW_NUMBER() OVER(ORDER BY STARTDATE DESC) AS SNO,* FROM DBO.vw_tbl_Seminar WHERE 1=1 ";

        if (ddloffice.SelectedIndex > 0)
            sql += " AND OFFICEID=" + ddloffice.SelectedValue;
        if (ddlCategory.SelectedIndex > 0)
            sql += " AND CATEGORYID=" + ddloffice.SelectedValue;

        if(txtFromDate.Text.Trim()!="")
            sql += " AND STARTDATE>='" + txtFromDate.Text.Trim()+"'";
        if (txtTODate.Text.Trim() != "")
            sql += " AND ENDDATE<='" + txtTODate.Text.Trim() + "'";

        sql += " ORDER BY STARTDATE DESC";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptSeminars.DataSource = dt;
        rptSeminars.DataBind();
    }
    protected void ddloffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Bindgrid();
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = true;
        frame1.Attributes.Add("Src", "AddEditSeminar.aspx?SeminarId=0");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int SeminarId=Common.CastAsInt32(((ImageButton)sender).CommandArgument );
        dvFrame.Visible = true;
        frame1.Attributes.Add("Src", "AddEditSeminar.aspx?SeminarId=" + SeminarId);
    }
    protected void btnAgenda_Click(object sender, EventArgs e)
    {
        int SeminarId=Common.CastAsInt32(((ImageButton)sender).CommandArgument );
        dvFrame.Visible = true;
        frame1.Attributes.Add("Src", "SeminarAgenda.aspx?SeminarId=" + SeminarId);
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
     
    }
    protected void btnInvite_Click(object sender, EventArgs e)
    {
        int SeminarId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        dvFrame.Visible = true;
        frame1.Attributes.Add("Src", "SeminarInvite.aspx?SeminarId=" + SeminarId);
    }
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = false;
        Bindgrid();
    }
    
    /////////////////////////////////////

    protected void lnkInviteClick_OnClick(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        DivInvitedPerson.Visible = true;
        BindInviteDetails(Common.CastAsInt32(lnk.CommandArgument));
    }

    protected void btnClose_DivInvitedPerson_OnClick(object sender, EventArgs e)
    {
        DivInvitedPerson.Visible = false;
        divUpdatePresence.Visible = false;
    }
    public void BindInviteDetails(int SeminarId)
    {
        string sql = "SELECT ROW_NUMBER() OVER(ORDER BY CREWNUMBER) AS SNO,TableId,sp.CREWID,CREWNUMBER,CREWNAME,CrewStatusName AS CREWSTATUS,RecruitingOfficeName,SP.RequstedOn,(CASE WHEN SP.ReplyStatus='P' then 'Yes' WHEN SP.ReplyStatus='A' then 'No' WHEN SP.ReplyStatus='N' then 'TBC' else '' end) as ReplyStatus,SP.ReplyStatus as ReplyStatusCode,SP.RepliedOn,R1.RankCode,SP.City,GUID,EMAIL  " +
                    "from " +
                    "DBO.tbl_SeminarInvite SP  " +
                    "INNER JOIN DBO.CREWPERSONALDETAILS P ON SP.CrewId=P.CrewId  " +
                    "INNER JOIN DBO.CrewStatus S ON P.CrewStatusId=S.CrewStatusId " +
                    "INNER JOIN DBO.Rank R1 ON R1.RankID=P.CurrentRankID " +
                    "INNER JOIN DBO.RecruitingOffice R ON P.RecruitmentOfficeId=R.RecruitingOfficeId WHERE SP.SEMINARID=" + SeminarId;


        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        rptInvite.DataSource = DT;
        rptInvite.DataBind();
    }
    protected void btEditAttendies_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfGUID = (HiddenField)btn.FindControl("hfdGUID");
        HiddenField hfReplyStatusCode = (HiddenField)btn.FindControl("hfReplyStatusCode");
        Label lblCrewNumner = (Label)btn.FindControl("lblCrewNumner");
        Label lblCrewName = (Label)btn.FindControl("lblCrewName");

        

        SelGuid = hfGUID.Value;
        lblCrewNumber.Text = lblCrewNumner.Text + " [ " + lblCrewName.Text+" ]" ;
        rdoGrade.SelectedValue = hfReplyStatusCode.Value;

        divUpdatePresence.Visible = true;
    }

    protected void btnUpdateAttendies_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.tbl_SeminarInvite SET REPLYSTATUS='" + rdoGrade.SelectedValue + "',RepliedOn=GETDATE() WHERE GUID='" + SelGuid + "'");
        lblMessageAttendies.Text = "Saved";
        rdoGrade.ClearSelection();
        lblCrewNumber.Text = "";
        BindInviteDetails(1);
    }

    protected void btnCloseAttendies_Click(object sender, EventArgs e)
    {
        SelGuid = "";
        lblCrewNumber.Text = "";
        divUpdatePresence.Visible = false;
    }
}
