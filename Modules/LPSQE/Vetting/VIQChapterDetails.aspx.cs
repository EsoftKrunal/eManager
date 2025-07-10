using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VIMS_VIQChapterDetails : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    DataTable dtDetails;
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }
    public int VIQId
    {
        get { return Common.CastAsInt32(ViewState["VIQId"]); }
        set { ViewState["VIQId"] = value; }
    }
    public int VIQStatus
    {
        get { return Common.CastAsInt32(ViewState["VIQStatus"]); }
        set { ViewState["VIQStatus"] = value; }
    }
    public int ChapterId
    {
        get { return Common.CastAsInt32(ViewState["ChapterId"]); }
        set { ViewState["ChapterId"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        Auth = new AuthenticationManager(306, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (!Auth.IsView)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
       

        try
        {
            int LoginId = Common.CastAsInt32(Session["loginid"].ToString());
            UserName = ProjectCommon.getUserNameByID(LoginId.ToString());
        }
        catch 
        {
            ProjectCommon.ShowMessage("Session expired. Please login again.");
            btnSaveComments.Visible = false; 
            return; 
        } 
        if (!IsPostBack)
        {
            VesselCode = Request.QueryString["VSL"].ToString();
            VIQId = Common.CastAsInt32(Request.QueryString["VIQId"]);

            DataTable dtviq = Common.Execute_Procedures_Select_ByQuery("SELECT VIQSTATUS FROM DBO.VIQ_VIQMaster WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
            VIQStatus = Common.CastAsInt32(dtviq.Rows[0][0]);

            btnSaveComments.Visible = VIQStatus <= 0;

            ChapterId = Common.CastAsInt32(Request.QueryString["ChapterId"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT RANKID,RANKCODE FROM DBO.vw_VIQQuestionRanks WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RANKCODE"); ;
            ddlRank.DataSource = dt;
            ddlRank.DataTextField = "RANKCODE";
            ddlRank.DataValueField = "RANKID";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("< All >", "0"));

            BindQuestions();
        }
        
    }
    protected void ddlRankStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindQuestions();
    }
    protected void btnSaveofcComments_Click(object sender, EventArgs e)
    {
      
        int RankId = Common.CastAsInt32(hfd_RankId.Value);
        int QId = Common.CastAsInt32(hfd_QId.Value);
        string sql = "";
        sql = "DBO.VIQ_OFFICE_InsertUpdateOffficeComments '" + VesselCode + "'," + VIQId.ToString() + "," + QId + "," + RankId + ",'" + UserName + "','" + DateTime.Today.ToString("dd-MMM-yyyy") + "','" + txtOFCComments.Text.Replace("'", "`") + "',1";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        BindQuestions();
    }

    protected void BindQuestions()
    {

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from DBO.vw_VIQQuestionRanks R WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RANKCODE,SORTORDER ");
        string rankfilter = "";
        if (ddlRank.SelectedIndex > 0)
        {
            rankfilter = " RANKID=" + ddlRank.SelectedValue + " AND ";
        }
        if (ddlStatus.SelectedIndex > 0)
        {
            if (ddlStatus.SelectedValue=="0")
                rankfilter += " (isnull(OfficeClosureStatus,0) <=0) AND ";
            if (ddlStatus.SelectedValue == "1")
                rankfilter += " (isnull(OfficeClosureStatus,0) >0) AND ";
        }

        dt = Common.Execute_Procedures_Select_ByQuery("select *,[dbo].[getInvolvedRanks](R.VESSELCODE,QUESTIONID," + VIQId.ToString() + ") AS RanksInvolved,(CASE WHEN ISNULL(OFFICECLOSURESTATUS,0) > 0 THEN 'Y' ELSE 'N' END) AS Office_Closed from DBO.vw_VIQQuestionRanks R WHERE VESSELCODE='" + VesselCode + "' AND " + rankfilter + " VIQID=" + VIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RANKCODE,SORTORDER ");
        
        rpt_ChaptersQuestions.DataSource = dt;
        rpt_ChaptersQuestions.DataBind();
    }

}