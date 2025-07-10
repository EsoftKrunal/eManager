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

public partial class Reports_RCA : System.Web.UI.Page
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
        lblRowsCount.Text = "";
        if (!Page.IsPostBack)
        {
            FromDate= ""+Request.QueryString["FDT"].ToString();
            ToDate = "" + Request.QueryString["TDT"].ToString();
            Status = "" + Request.QueryString["Status"].ToString();
            ShowData();
        }
    }
    protected void ShowData()
    {
        //---------------------------------------------------------------------
        string datefilter = "";
        //---------------------------------------------------------------------
        if (FromDate.Trim() != "")
            datefilter = "ActualDate >='" + FromDate + "'";

        if (ToDate.Trim() != "")
            datefilter += (((datefilter == "") ? "" : " AND ") + "ActualDate <='" + ToDate + "'");

        string sql = "select *,'<br/>&nbsp;RC : ' + [dbo].[getCauses](causeid) as CausesList from vw_ObservationsAll where InspectionDueId in (select id from t_InspectionDue where " + ((datefilter == "") ? "" : datefilter + " And ");
        string Filter = "";
        Filter = " Inspectionid In (46,89) ) ";
        if (Status=="O")
        {
            Filter += " And Status='O'";
        }
        else if (Status == "C")
        {
            Filter += " And Status='C'";
        }
        sql += Filter;
        //---------------------------------------------------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql + " Order By InspectionNo");
        rptList.DataSource = dt;
        rptList.DataBind();
        
        lblRowsCount.Text = " ( " + dt.Rows.Count.ToString() + " ) Rows Found.";
        //---------------------------------------------------------------------
        //btn_Show1_Click(sender, e);
        //---------------------------------------------------------------------
    }
    
    protected void Select_RootCause(object sender, EventArgs e)
    {
        LoadRootCause();
        string[] parts = ((ImageButton)sender).CssClass.Split('|');
        InspectionDueId = Common.CastAsInt32(parts[0]);
        TableId = Common.CastAsInt32(parts[1]);
        TableName = parts[2];
        dvRootCause.Visible = true;
    }
    protected void LoadRootCause()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM m_tblRootCause ORDER BY CAUSENAME");
        rptRootCause.DataSource = dt;
        rptRootCause.DataBind();
    }
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        dvRootCause.Visible = false; 
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string CauseIds = "";
        foreach (RepeaterItem ri in rptRootCause.Items)
        {
            CheckBox ch =(CheckBox)ri.FindControl("chkRootCause");
            if (ch.Checked)
            {
                CauseIds += "," + ch.CssClass;
            }
        }
        if (CauseIds.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select root cause.');", true);
        }
        else
        {
            if(CauseIds.Trim().StartsWith(","))
                CauseIds=CauseIds.Substring(1);

            string sql = "INSERT INTO t_InspectionObsClosure(InspectionDueId,TABLENAME,ID,CAUSEID,CLOSEDBY,CLOSEDON)";
            sql += "VALUES(" + InspectionDueId.ToString() + ",'" + TableName + "'," + TableId + ",'" + CauseIds + "','" + Session["UserName"].ToString() + "',getdate())";
            Common.Execute_Procedures_Select_ByQuery(sql);
            ShowData();
            dvRootCause.Visible = false;
        }
    }

}
