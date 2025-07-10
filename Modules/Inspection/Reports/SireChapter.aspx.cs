using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;

public partial class Reports_SireChapter : System.Web.UI.Page
{
    public string TableName
    {
        set { ViewState["TableName"] = value; }
        get { return ViewState["TableName"].ToString(); }
    }
    public string TableName1
    {
        set { ViewState["TableName1"] = value; }
        get { return ViewState["TableName1"].ToString(); }
    }
    public int TableId
    {
        set { ViewState["TableId"] = value; }
        get { return Common.CastAsInt32(ViewState["TableId"]); }
    }
    public int TableId1
    {
        set { ViewState["TableId1"] = value; }
        get { return Common.CastAsInt32(ViewState["TableId1"]); }
    }
    public int InspectionDueId
    {
        set { ViewState["InspectionDueId"] = value; }
        get { return Common.CastAsInt32(ViewState["InspectionDueId"]); }
    }

    public string FromDate
    {
        set { ViewState["FromDate"] = value; }
        get { return ViewState["FromDate"].ToString(); }
    }
    public string ToDate
    {
        set { ViewState["ToDate"] = value; }
        get { return ViewState["ToDate"].ToString(); }
    }
    public string Status
    {
        set { ViewState["Status"] = value; }
        get { return ViewState["Status"].ToString(); }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {

        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 163);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        if (!Page.IsPostBack)
        {
            FromDate = "" + Request.QueryString["FDT"].ToString();
            ToDate = "" + Request.QueryString["TDT"].ToString();
            Status = "" + Request.QueryString["Status"].ToString();
            ShowData();
        }
    }
    
    protected void ShowData()
    {
        string datefilter = "";

        if (FromDate.Trim() != "")
            datefilter = "ActualDate >='" + FromDate + "'";

        if (ToDate.Trim() != "")
            datefilter += (((datefilter == "") ? "" : " AND ") + "ActualDate <='" + ToDate + "'");

        string sql = "select *,'<br/>&nbsp;RC : ' + [dbo].[getSireChapters](ChapId) as CausesList from vw_ObservationsAllSireChapter where InspectionDueId in (select id from t_InspectionDue where " + ((datefilter == "") ? "" : datefilter + " And ");
        string Filter = "";
        List<string> InsFilter = new List<string>();
        char[] sep = { ',' };
        
        Filter = " Inspectionid In (select id from m_Inspection where InspectionGroup=4 union select 43) )";

        if (Status == "O")
        {
            Filter += " And Status='O'";
        }
        else if (Status == "C")
        {
            Filter += " And Status='C'";
        }

        sql += Filter;


        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql + " Order By InspectionNo");
        rptList1.DataSource = dt;
        rptList1.DataBind();
        lblRowsCount1.Text = " ( " + dt.Rows.Count.ToString() + " ) Rows Found.";
    }
    protected void Select_SireChapter(object sender, EventArgs e)
    {
        LoadSireChapters();
        string[] parts = ((ImageButton)sender).CssClass.Split('|');
        InspectionDueId = Common.CastAsInt32(parts[0]);
        TableId1 = Common.CastAsInt32(parts[1]);
        TableName1 = parts[2];
        dvSireChapter.Visible = true;
    }
    protected void LoadSireChapters()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select id,ChapterName from dbo.m_chapters where inspectiongroup=1");
        rptSireChapter.DataSource = dt;
        rptSireChapter.DataBind();
    }
    protected void btnSireClose_Click(object sender, ImageClickEventArgs e)
    {
        dvSireChapter.Visible = false;
    }
    protected void btnSireSave_Click(object sender, EventArgs e)
    {
        string SireChapIds = "";
        foreach (RepeaterItem ri in rptSireChapter.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkSireChapter");
            if (ch.Checked)
            {
                SireChapIds += "," + ch.CssClass;
            }
        }
        if (SireChapIds.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select Sire Chapter.');", true);
        }
        else
        {
            if (SireChapIds.Trim().StartsWith(","))
                SireChapIds = SireChapIds.Substring(1);

            string sql = "INSERT INTO t_InspectionObsSireChapters(InspectionDueId,TABLENAME,ID,ChapId,CLOSEDBY,CLOSEDON)";
            sql += "VALUES(" + InspectionDueId.ToString() + ",'" + TableName1 + "'," + TableId1 + ",'" + SireChapIds + "','" + Session["UserName"].ToString() + "',getdate())";
            Common.Execute_Procedures_Select_ByQuery(sql);
            ShowData();
            dvSireChapter.Visible = false;
        }
    }
    
}
