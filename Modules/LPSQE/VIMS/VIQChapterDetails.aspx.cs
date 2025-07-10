using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VIMS_VIQChapterDetails : System.Web.UI.Page
{
    DataTable dtDetails;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string VesselCode = "";
            try
            {
                VesselCode = Session["CurrentShip"].ToString();
            }
            catch
            {
                ProjectCommon.ShowMessage("Your session is expired. Please login again.");
                return;
            }

            VIQId = Common.CastAsInt32(Request.QueryString["VIQId"]);
            DataTable dtviq = Common.Execute_Procedures_Select_ByQuery("SELECT VIQSTATUS FROM VIQ_VIQMaster WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString());
            VIQStatus = Common.CastAsInt32(dtviq.Rows[0][0]);

            ChapterId = Common.CastAsInt32(Request.QueryString["ChapterId"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT RANKID,RANKCODE FROM vw_VIQQuestionRanks WHERE VESSELCODE='" + VesselCode + "' AND VIQID=" + VIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RANKCODE"); ;
            ddlRank.DataSource = dt;
            ddlRank.DataTextField = "RANKCODE";
            ddlRank.DataValueField = "RANKID";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("< All >", "0"));

            BindChapters();
        }
        
    }
    protected void ddlRankStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindChapters();
    }
    protected void btnSaveComments_Click(object sender, EventArgs e)
    {
        if (VIQStatus >= 1)
        {
            ProjectCommon.ShowMessage("Cant not update comments. VIQ is closed.");
            return; 
        }

        int RankId = Common.CastAsInt32(hfd_RankId.Value);
        int QId = Common.CastAsInt32(hfd_QId.Value);
        string sql = "DBO.VIQ_VSL_InsertUpdateShipComments '" + Session["CurrentShip"].ToString() + "'," + VIQId.ToString() + "," + QId + "," + RankId + ",'" + Session["FullName"] + "','" + DateTime.Today.ToString("dd-MMM-yyyy") + "','" + txtUserComments.Text.Replace("'", "`") + "',0";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        BindChapters();
    }
    protected void BindChapters()
    {

        string sql = "SELECT * FROM VIQ_VIQmASTER WHERE VESSELCODE='" + Session["CurrentShip"].ToString() + "' AND VIQId=" + VIQId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        lblviq.Text = dt.Rows[0]["VIQNO"].ToString();
        lblcdate.Text = Common.ToDateString(dt.Rows[0]["TARGETDATE"]);

        dt = Common.Execute_Procedures_Select_ByQuery("select * from vw_VIQQuestionRanks R WHERE VESSELCODE='" + Session["CurrentShip"].ToString() + "' AND VIQID=" + VIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RANKCODE,SORTORDER ");
        if (dt.Rows.Count > 0)
        {
            lblchapno.Text = dt.Rows[0]["CHAPTERNO"].ToString();
            lblchapname.Text = dt.Rows[0]["CHAPTERNAME"].ToString();
        }
        string rankfilter = "";
        if (ddlRank.SelectedIndex > 0)
        {
            rankfilter = " RANKID=" + ddlRank.SelectedValue + " AND ";
        }
        if (ddlStatus.SelectedIndex > 0)
        {
            if (ddlStatus.SelectedValue=="0")
                rankfilter += " (DONEDATE IS NULL) AND ";
            if (ddlStatus.SelectedValue == "1")
                rankfilter += " (DONEDATE IS NOT NULL) AND ";
        }

        dt = Common.Execute_Procedures_Select_ByQuery("select *,[dbo].[getInvolvedRanks](R.VESSELCODE,QUESTIONID," + VIQId.ToString() + ") AS RanksInvolved,(CASE WHEN ISNULL(OFFICECLOSURESTATUS,0) > 0 THEN 'Y' ELSE 'N' END) AS Office_Closed from vw_VIQQuestionRanks R WHERE VESSELCODE='" + Session["CurrentShip"].ToString() + "' AND " + rankfilter + " VIQID=" + VIQId.ToString() + " AND CHAPTERID=" + ChapterId.ToString() + " ORDER BY RANKCODE,SORTORDER ");
        
        rpt_ChaptersQuestions.DataSource = dt;
        rpt_ChaptersQuestions.DataBind();
    }

}