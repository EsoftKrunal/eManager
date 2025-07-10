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

public partial class SeminarInvite : System.Web.UI.Page
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text= "";
        //------------------------------------
        SessionManager.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            SeminarId = Common.CastAsInt32(Request.QueryString["SeminarId"]);
            UserId =Common.CastAsInt32(Session["LoginId"]);
            LoadStatus();
            LoadRecruitingOffice();
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
    protected void btnDeleteInvite_Click(object sender, EventArgs e)
    {
        int TableId =Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (TableId > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.tbl_SeminarInvite WHERE TABLEID=" + TableId);
            ShowInviteDetails();
        }
    }
    protected void btnInvite_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rptCrewList.Items)
        {
            CheckBox ch=((CheckBox)ri.FindControl("chkSelect"));
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
        ShowMessage("Record saved sucessfully.", false);
    }
    
    public void ShowRecord()
    {
        //DataTable DT = Common.Execute_Procedures_Select_ByQuery("select * from DBO.tbl_Seminar where SeminarId=" + SeminarId );
        //if (DT.Rows.Count > 0)
        //{
        //    ddlCategory.SelectedValue = DT.Rows[0]["Category"].ToString();
        //    ddloffice.SelectedValue = DT.Rows[0]["OfficeId"].ToString();
        //    txtFdt.Text = Common.ToDateString(DT.Rows[0]["StartDate"]);
        //    txtTdt.Text = Common.ToDateString(DT.Rows[0]["EndDate"]);
        //    txtLocation.Text = DT.Rows[0]["Location"].ToString();
        //    txtTopic.Text = DT.Rows[0]["Topic"].ToString();
        //    ShowPresenterDetails();
        //}
    }
    public void LoadCrewList(object sender,EventArgs e)
    {
        string WhereClause = " WHERE 1=1 ";
        if (txtcrewn.Text.Trim() != "")
            WhereClause += " AND CREWNUMBER='" + txtcrewn.Text.Trim() + "'";
        if (txtCrewName.Text.Trim() != "")
            WhereClause += " AND FIRSTNAME + ' ' + MiddleName + ' ' + LastName LIKE '%" + txtCrewName.Text.Trim() + "%'";
        if (ddlCrewStatus.SelectedIndex>0)
            WhereClause += " AND P.CrewStatusId=" + ddlCrewStatus.SelectedValue;
        if (ddlRecuitingOffice.SelectedIndex > 0)
            WhereClause += " AND P.RecruitmentOfficeId=" + ddlRecuitingOffice.SelectedValue;

        string perrec = "";
        if (ddlnor.SelectedItem.Text != "ALL")
            perrec = " TOP " + ddlnor.SelectedItem.Text;

        string sql = "SELECT " + perrec + "  P.CREWID,CREWNUMBER,FIRSTNAME + ' ' + MiddleName + ' ' + LastName AS CREWNAME,CrewStatusName AS CREWSTATUS,RecruitingOfficeName " +
                      "FROM  " +
                      "DBO.CREWPERSONALDETAILS P  " +
                      "INNER JOIN DBO.CrewStatus S ON P.CrewStatusId=S.CrewStatusId " +
                      "INNER JOIN DBO.RecruitingOffice R ON P.RecruitmentOfficeId=R.RecruitingOfficeId " + WhereClause + " ORDER BY CREWNAME";

        rptCrewList.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptCrewList.DataBind();
    }
    public void ShowInviteDetails()
    {
       

        string sql = "select ROW_NUMBER() OVER(ORDER BY CREWNUMBER) AS SNO,TableId,sp.CREWID,CREWNUMBER,CREWNAME,CrewStatusName AS CREWSTATUS,RecruitingOfficeName,SP.RequstedOn,SP.ReplyStatus,SP.RepliedOn " +
                    "from " +
                    "DBO.tbl_SeminarInvite SP  " +
                    "INNER JOIN DBO.CREWPERSONALDETAILS P ON SP.CrewId=P.CrewId  " +
                    "INNER JOIN DBO.CrewStatus S ON P.CrewStatusId=S.CrewStatusId " +
                    "INNER JOIN DBO.RecruitingOffice R ON P.RecruitmentOfficeId=R.RecruitingOfficeId WHERE SP.SEMINARID=" + SeminarId;

        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        rptInvite.DataSource = DT;
        rptInvite.DataBind();
    }

    public void ShowMessage(string Message, bool Error)
    {
        lblMessage.Text = Message;
        lblMessage.ForeColor = (Error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvFrame.Visible = false;
        ShowInviteDetails();
    }
}

