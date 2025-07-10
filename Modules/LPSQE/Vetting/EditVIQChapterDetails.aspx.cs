using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VIMS_EditVIQChapterDetails : System.Web.UI.Page
{
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }
    public int PVIQId
    {
        get { return Common.CastAsInt32(ViewState["PVIQId"]); }
        set { ViewState["PVIQId"] = value; }
    }
    public int VIQId
    {
        get { return Common.CastAsInt32(ViewState["VIQId"]); }
        set { ViewState["VIQId"] = value; }
    }
    public int ChapterId
    {
        get { return Common.CastAsInt32(ViewState["ChapterId"]); }
        set { ViewState["ChapterId"] = value; }
    }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            int LoginId = Common.CastAsInt32(Session["loginid"].ToString());
            UserName = ProjectCommon.getUserNameByID(LoginId.ToString());

        }
        catch
        {
            ProjectCommon.ShowMessage("Session expired. Please login again.");
            btnAddTOVIQ.Visible = false;
            return;
        }
        if (!IsPostBack)
        {
            UserId = Convert.ToInt32(Session["loginid"].ToString());
            VesselCode = Request.QueryString["VSL"].ToString();
            PVIQId = Common.CastAsInt32(Request.QueryString["PVIQId"]);
            VIQId = Common.CastAsInt32(Request.QueryString["VIQId"]);

            ChapterId = Common.CastAsInt32(Request.QueryString["ChapterId"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT RANKID,RANKCODE FROM DBO.vw_VIQQuestionRanks WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + PVIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RANKCODE"); ;
            ddlRank.DataSource = dt;
            ddlRank.DataTextField = "RANKCODE";
            ddlRank.DataValueField = "RANKID";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("< All >", "0"));


            dt = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT RiskScore FROM DBO.vw_VIQQuestionRanks WHERE ISNULL(RiskScore,'')<>'' AND VESSELCODE='" + VesselCode + "' AND VIQID=" + PVIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RiskScore");
            if (dt.Rows.Count > 0)
            {
                if (!(Convert.IsDBNull(dt.Rows[0][0])))
                {
                    chkScores.DataSource = dt;
                    chkScores.DataTextField = "RiskScore";
                    chkScores.DataValueField = "RiskScore";
                    chkScores.DataBind();
                    lblscoreheading.Visible = true;
                }
            }

            dt = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT RiskScore FROM DBO.vw_VIQQuestionRanks WHERE ISNULL(RiskScore,'')<>'' AND VESSELCODE='" + VesselCode + "' AND VIQID=" + PVIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RiskScore");
            if (dt.Rows.Count > 0)
            {
                if (!(Convert.IsDBNull(dt.Rows[0][0])))
                {
                    chkScores.DataSource = dt;
                    chkScores.DataTextField = "RiskScore";
                    chkScores.DataValueField = "RiskScore";
                    chkScores.DataBind();
                    lblscoreheading.Visible = true;
                }
            }

            BindQuestions();
        }

    }
    protected void ddlRankStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindQuestions();
    }
  

    protected void btnFinalSave_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rpt_ChaptersQuestions.Items)
        {
            CheckBox ch = ((CheckBox)ri.FindControl("chkAddMe"));
            if (ch.Checked)
            {
                int QId = Common.CastAsInt32(ch.CssClass);
                int RId = Common.CastAsInt32(ch.Attributes["rankid"]);
                string sql = "EXEC DBO.VIQ_AddQuestion '" + VesselCode + "'," + VIQId + "," + QId + "," + RId + "";
                Common.Execute_Procedures_Select_ByQuery(sql);
            }
        }
        BindQuestions();
    }
    //protected void btnAddQ_Click(object sender, EventArgs e)
    //{
    //    CheckBox ch = (CheckBox)sender;
    //    int QId = Common.CastAsInt32(ch.CssClass);
    //    int RId = Common.CastAsInt32(ch.Attributes["rankid"]);
    //    string sql = "EXEC DBO.VIQ_AddQuestion '" + VesselCode + "'," + VIQId + "," + QId + "," + RId + "";
    //    Common.Execute_Procedures_Select_ByQuery(sql);
    //    BindQuestions();
    //}
    protected void btnRemSelQ_Click(object sender, EventArgs e)
    {
        int QId = Common.CastAsInt32(((ImageButton)sender).CssClass);
        int RId = Common.CastAsInt32(((ImageButton)sender).Attributes["rankid"]);
        string sql = "EXEC DBO.VIQ_RemoveQuestion '" + VesselCode + "'," + VIQId + "," + QId + "," + RId + "";
        Common.Execute_Procedures_Select_ByQuery(sql);
        BindQuestions();
    }

    protected void BindQuestions()
    {

        string sql = "SELECT * FROM [dbo].VIQ_VIQMaster WHERE VESSELCODE='" + VesselCode + "' AND VIQId=" + VIQId.ToString() + " AND VIQStatus=0";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        btnAddTOVIQ.Visible = (dt.Rows.Count > 0);


        string rankfilter = "", obsfilter = "", scorefilter = "";
        if (ddlRank.SelectedIndex > 0)
        {
            rankfilter = " AND RANKID=" + ddlRank.SelectedValue + " ";
        }
        int ObsCount = Common.CastAsInt32(txtMinObs.Text);
        if (ObsCount > 0)
        {
            obsfilter = " AND ObsCount >=" + ObsCount + " ";
        }
        if (chkScores.Visible)
        {
            string list_scores = "";
            foreach (ListItem li in chkScores.Items)
            {
                if (li.Selected)
                    list_scores += ",'" + li.Value.Trim() + "'";
            }
            if (list_scores.StartsWith(","))
            {
                list_scores = list_scores.Substring(1);
                scorefilter = " AND RiskScore In (" + list_scores + ") ";
            }
        }

        dt = Common.Execute_Procedures_Select_ByQuery("select *,[dbo].[getInvolvedRanks](R.VESSELCODE,QUESTIONID," + PVIQId.ToString() + ") AS RanksInvolved,(CASE WHEN EXISTS (SELECT 1 FROM DBO.vw_VIQQuestionRanks V WHERE V.VESSELCODE='" + VesselCode + "' AND V.VIQID=" + VIQId + " AND V.RANKID=R.RANKID AND V.QUESTIONID=R.QuestionId) THEN 'Y' ELSE 'N' END) AS SELECTED from DBO.vw_VIQQuestionRanks R WHERE VESSELCODE='" + VesselCode + "' " + rankfilter + obsfilter + scorefilter + " AND VIQID=" + PVIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RANKCODE,SORTORDER ");

        rpt_ChaptersQuestions.DataSource = dt;
        rpt_ChaptersQuestions.DataBind();
    }
}