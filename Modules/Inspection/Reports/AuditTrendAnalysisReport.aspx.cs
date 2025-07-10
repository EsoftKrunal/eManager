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

public partial class Reports_AuditTrendAnalysisReport : System.Web.UI.Page
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
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        lblRowsCount.Text = "";
        if (!Page.IsPostBack)
        {
            txtfromdate.Text = DateTime.Today.ToString("01-JAN-yyyy");
            txttodate.Text = DateTime.Today.ToString("31-DEC-yyyy");
            LoadGroup();
            ddlInspGroup_OnSelectedIndexChanged(sender, e);
        }
    }
    protected void LoadGroup()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select id,code from [dbo].[m_InspectionGroup] order by code");
        ddlInspGroup.DataSource = dt;
        ddlInspGroup.DataTextField = "Code";
        ddlInspGroup.DataValueField = "Id";
        ddlInspGroup.DataBind();
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        ddlInspGroup.SelectedIndex = 0;
        ddlInsp.SelectedIndex = 0;
        txtfromdate.Text = "";
        txttodate.Text = "";
    }
    protected void ddlInspGroup_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string selected = "";

        foreach (ListItem li in ddlInspGroup.Items)
        {
            if (li.Selected)
                selected += "," + li.Value;
        }
        if (selected.StartsWith(","))
            selected = selected.Substring(1);
        if (selected.Trim() != "")
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select id,code from [dbo].[m_Inspection] where inspectiongroup in (" + selected + ") order by code");
            ddlInsp.DataSource = dt;
            ddlInsp.DataTextField = "Code";
            ddlInsp.DataValueField = "Id";
            ddlInsp.DataBind();
        }
        else
        {
            ddlInsp.DataSource = null;
            ddlInsp.DataTextField = "Code";
            ddlInsp.DataValueField = "Id";
            ddlInsp.DataBind();
        }
    }
    
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        //---------------------------------------------------------------------
        string datefilter = "";
        //---------------------------------------------------------------------
        if (txtfromdate.Text.Trim() != "")
            datefilter = "ActualDate >='" + txtfromdate.Text + "'";

        if (txttodate.Text.Trim() != "")
            datefilter += (((datefilter == "") ? "" : " AND ") + "ActualDate <='" + txttodate.Text + "'");

        string sql = "select *,'<br/>&nbsp;RC : ' + [dbo].[getCauses](causeid) as CausesList from vw_ObservationsAll where InspectionDueId in (select id from t_InspectionDue where " + ((datefilter == "") ? "" : datefilter + " And ");
        string Filter = "";
        List<string> InsFilter = new List<string>();
        List<string> InsNames = new List<string>();
        string ids = "";
        foreach (ListItem li in ddlInsp.Items)
        {
            if (li.Selected)
            {
                InsFilter.Add(" Inspectionid=" + li.Value);
                InsNames.Add(li.Text);
                ids += "," + li.Value;
            }
        }
        if (ids.StartsWith(","))
            ids = ids.Substring(1);

        if (ids.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select any Inspection.');", true);
            return;
        }

        Filter = " Inspectionid In (" + ids + ") )";
        if (rad_O.Checked)
        {
            Filter += " And Status='O'";
        }
        else if (rad_C.Checked)
        {
            Filter += " And Status='C'";
        }
        sql += Filter;
        //---------------------------------------------------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptList.DataSource = dt;
        rptList.DataBind();
        BindChart(datefilter, InsFilter.ToArray(), InsNames.ToArray());
        lblRowsCount.Text = " ( " + dt.Rows.Count.ToString() + " ) Rows Found.";
        //---------------------------------------------------------------------
        btn_Show1_Click(sender, e);
        //---------------------------------------------------------------------
    }
    protected void BindChart(string datefilter, string[] InsFilter, string[] InsNames)
    {
        StringBuilder sbm = new StringBuilder();
        sbm.Append("<td style='text-align:left;'>Root Cause</td>");
        foreach (string item in InsNames)
        {
            sbm.Append("<td colspan='3' style='text-align:center;'>" + item + "</td>");
        }
        litHeaderMain.Text = sbm.ToString();

        StringBuilder sb = new StringBuilder();
        sb.Append("<td style='text-align:left;'>&nbsp;</td>");
        foreach (string item in InsFilter)
        {
            sb.Append("<td style='width:100px;text-align:center;'>Count</td>");
            sb.Append("<td style='width:100px;text-align:center;'>Insp. Count</td>");
            sb.Append("<td style='width:100px;text-align:center;'>Count PER Insp.</td>");
        }
        litHeaders.Text = sb.ToString();

        sbm = new StringBuilder();
        sb = new StringBuilder();

        int c = 1;
        foreach (string item in InsFilter)
        {
            sbm.Append(", ROUND((CAST (RCCount" + c.ToString() + " AS FLOAT) / (CASE WHEN InsCount" + c.ToString() + "=0 THEN 1 ELSE InsCount" + c.ToString() + " END) ),2) AS PER" + c.ToString());
            //---------------
            sb.Append(",(select count(*) from vw_observationsall o where status='C' And o.InspectionDueId in (select i.id from t_InspectionDue i where " + InsFilter[c - 1] + " And " + ((datefilter == "") ? "" : datefilter) + " and (',' + o.CauseId + ',') LIKE ('%,' + CONVERT(VARCHAR,m.CAUSEID) + ',%'))) AS RCCount" + c.ToString() + " ");
            sb.Append(",(select count(i.id) from t_InspectionDue i where " + ((datefilter == "") ? "" : datefilter + " And ") + InsFilter[c - 1] + ") AS InsCount" + c.ToString() + " ");
            //---------------
            c++;
        }
        string sql = " SELECT *" + sbm.ToString() + " FROM ( " +
                     " SELECT CauseName" + sb.ToString() +
                     " FROM m_tblRootCause m" +
                     " ) A";
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        sb = new StringBuilder();

        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("<tr>");
            int k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k == 0)
                    sb.Append("<td>" + dr[dc].ToString() + "</td>");
                else
                    sb.Append("<td style='width:100px;'>" + dr[dc].ToString() + "</td>");
                k++;
            }
            sb.Append("</tr>");
        }
        litDataRows.Text = sb.ToString();

        foreach (string item in InsNames)
        {
            TAChart.Series.Add(new System.Web.UI.DataVisualization.Charting.Series(item));
            TAChart.Series[item].LegendText = item;
        }

        foreach (DataRow dr in dt.Rows)
        {
            int k = 1;
            foreach (string item in InsNames)
            {
                TAChart.Series[item].Points.AddXY(dr[0].ToString(), dr["PER" + k.ToString()]);
                k++;
            }
        }

        //TAChart.Series["TAData"].Points.Clear();
        //foreach (DataRow dr in dt.Rows)
        //{
        //    TAChart.Series["TAData"].Points.AddXY(dr[0].ToString(), dr["PER"]);

        //}
        TAChart.Legends.Add("hits");
        TAChart.DataBind();
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
            btn_Show_Click(sender, e);
            dvRootCause.Visible = false;
        }
    }

    
    protected void btn_Show1_Click(object sender, EventArgs e)
    {
        string datefilter = "";

        if (txtfromdate.Text.Trim() != "")
            datefilter = "ActualDate >='" + txtfromdate.Text + "'";

        if (txttodate.Text.Trim() != "")
            datefilter += (((datefilter == "") ? "" : " AND ") + "ActualDate <='" + txttodate.Text + "'");

        string sql = "select *,'<br/>&nbsp;RC : ' + [dbo].[getSireChapters](ChapId) as CausesList from vw_ObservationsAllSireChapter where InspectionDueId in (select id from t_InspectionDue where " + ((datefilter == "") ? "" : datefilter + " And ");
        string Filter = "";
        List<string> InsFilter = new List<string>();
        List<string> InsNames = new List<string>();
        string ids = "";
        foreach (ListItem li in ddlInsp.Items)
        {
            if (li.Selected)
            {
                InsFilter.Add(" Inspectionid=" + li.Value);
                InsNames.Add(li.Text);
                ids += "," + li.Value;
            }
        }
        if (ids.StartsWith(","))
            ids = ids.Substring(1);

        Filter = " Inspectionid In (" + ids + ") )";

        if (rad_O.Checked)
        {
            Filter += " And Status='O'";
        }
        else if (rad_C.Checked)
        {
            Filter += " And Status='C'";
        }

        sql += Filter;


        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptList1.DataSource = dt;
        rptList1.DataBind();
        BindChart1(datefilter, InsFilter.ToArray(), InsNames.ToArray());
        lblRowsCount1.Text = " ( " + dt.Rows.Count.ToString() + " ) Rows Found.";
        //rptList1
    }
    protected void BindChart1(string datefilter, string[] InsFilter, string[] InsNames)
    {
        StringBuilder sbm = new StringBuilder();
        sbm.Append("<td style='text-align:left;'>Root Cause</td>");
        foreach (string item in InsNames)
        {
            sbm.Append("<td colspan='3' style='text-align:center;'>" + item + "</td>");
        }
        litHeaderMain.Text = sbm.ToString();

        StringBuilder sb = new StringBuilder();
        sb.Append("<td style='text-align:left;'>&nbsp;</td>");
        foreach (string item in InsFilter)
        {
            sb.Append("<td style='width:100px;text-align:center;'>Count</td>");
            sb.Append("<td style='width:100px;text-align:center;'>Insp. Count</td>");
            sb.Append("<td style='width:100px;text-align:center;'>Count PER Insp.</td>");
        }
        litHeaders.Text = sb.ToString();

        sbm = new StringBuilder();
        sb = new StringBuilder();

        int c = 1;
        foreach (string item in InsFilter)
        {
            sbm.Append(", ROUND((CAST (RCCount" + c.ToString() + " AS FLOAT) / (CASE WHEN InsCount" + c.ToString() + "=0 THEN 1 ELSE InsCount" + c.ToString() + " END) ),2) AS PER" + c.ToString());
            //---------------
            sb.Append(",(select count(*) from vw_ObservationsAllSireChapter o where status='C' And o.InspectionDueId in (select i.id from t_InspectionDue i where " + InsFilter[c - 1] + " And " + ((datefilter == "") ? "" : datefilter) + " and (',' + o.CHAPID + ',') LIKE ('%,' + CONVERT(VARCHAR,m.Id) + ',%'))) AS RCCount" + c.ToString() + " ");
            sb.Append(",(select count(i.id) from t_InspectionDue i where " + ((datefilter == "") ? "" : datefilter + " And ") + InsFilter[c - 1] + ") AS InsCount" + c.ToString() + " ");
            //---------------
            c++;
        }
        string sql = " SELECT *" + sbm.ToString() + " FROM ( " +
                     " SELECT ChapterName" + sb.ToString() +
                     " FROM m_chapters m where inspectiongroup=1 " +
                     " ) A";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        sb = new StringBuilder();

        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("<tr>");
            int k = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (k == 0)
                    sb.Append("<td>" + dr[dc].ToString() + "</td>");
                else
                    sb.Append("<td style='width:100px;'>" + dr[dc].ToString() + "</td>");
                k++;
            }
            sb.Append("</tr>");
        }
        LitDataRows1.Text = sb.ToString();

        foreach (string item in InsNames)
        {
            Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series(item));
            Chart1.Series[item].LegendText = item;
        }

        foreach (DataRow dr in dt.Rows)
        {
            int k = 1;
            foreach (string item in InsNames)
            {
                Chart1.Series[item].Points.AddXY(dr[0].ToString(), dr["PER" + k.ToString()]);
                k++;
            }
        }

        Chart1.Legends.Add("hits");
        Chart1.DataBind();
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
            btn_Show1_Click(sender, e);
            dvSireChapter.Visible = false;
        }
    }
    protected void load_click(object sender, EventArgs e)
    {
        dvSireChapter.Visible = false;
        DataTable dtSire = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 1,'" + txtfromdate.Text.Trim() + "','" + txttodate.Text.Trim() + "'");
        DataTable dtPSC = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 4,'" + txtfromdate.Text.Trim() + "','" + txttodate.Text.Trim() + "'");
        DataTable dtSafety = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 0,'" + txtfromdate.Text.Trim() + "','" + txttodate.Text.Trim() + "'");
        
        AnalysisChart.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("SIRE"));
        AnalysisChart.Series[0].LegendText = "SIRE";
        AnalysisChart.Series[0].IsValueShownAsLabel = true;

        AnalysisChart.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("PSC"));
        AnalysisChart.Series[1].LegendText = "PSC";
        AnalysisChart.Series[1].IsValueShownAsLabel = true;

        AnalysisChart.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("SAFETY"));
        AnalysisChart.Series[2].LegendText = "SAFETY";
        AnalysisChart.Series[2].IsValueShownAsLabel = true;

        foreach (DataRow dr in dtSire.Rows)
        {
            AnalysisChart.Series[0].Points.AddXY(dr["name"].ToString(), dr["data"].ToString());
        }
        foreach (DataRow dr in dtPSC.Rows)
        {
            AnalysisChart.Series[1].Points.AddXY(dr["name"].ToString(), dr["data"].ToString());
        }
        foreach (DataRow dr in dtSafety.Rows)
        {
            AnalysisChart.Series[2].Points.AddXY(dr["name"].ToString(), dr["data"].ToString());
        }


        AnalysisChart.Legends.Add("hits");
        AnalysisChart.Titles.Add(" [ Avg No of Observations Per Inspection X 100 ]");
        AnalysisChart.Titles[0].Font = new System.Drawing.Font("Arial", 15);

        AnalysisChart.DataBind();

        //string[]  
        //AnalysisChart
    }
    protected void download_click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        dvSireChapter.Visible = false;

        DataTable dtSire = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 1,'" + txtfromdate.Text.Trim() + "','" + txttodate.Text.Trim() + "'");
        DataTable dtPSC = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 4,'" + txtfromdate.Text.Trim() + "','" + txttodate.Text.Trim() + "'");
        DataTable dtSafety = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 0,'" + txtfromdate.Text.Trim() + "','" + txttodate.Text.Trim() + "'");
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select id,ChapterName from dbo.m_chapters where inspectiongroup=1");
        sb.Append("<table border='1'>");
        sb.Append("<tr>");
        sb.Append("<td>Chapter Name</td>");
        sb.Append("<td>SIRE-Obs</td>");
        sb.Append("<td>SIRE-Ins</td>");
        sb.Append("<td>SIRE(Per)</td>");
        sb.Append("<td>PSC-Obs</td>");
        sb.Append("<td>PSC-Ins</td>");
        sb.Append("<td>PSC(Per)</td>");
        sb.Append("<td>SAFETY-Obs</td>");
        sb.Append("<td>SAFETY-Ins</td>");
        sb.Append("<td>SAFETY(Per)</td>");
        sb.Append("</tr>");
        int C=0;
        foreach(DataRow dr in dt.Rows)
        {
            sb.Append("<tr>");
            sb.Append("<td>" + dr["ChapterName"].ToString() +"</td>");
            sb.Append("<td>" + dtSire.Rows[C][3].ToString() + "</td>");
            sb.Append("<td>" + dtSire.Rows[C][4].ToString() + "</td>");
            sb.Append("<td>" + dtSire.Rows[C][2].ToString() +"</td>");

            sb.Append("<td>" + dtPSC.Rows[C][3].ToString() + "</td>");
            sb.Append("<td>" + dtPSC.Rows[C][4].ToString() + "</td>");
            sb.Append("<td>" + dtPSC.Rows[C][2].ToString() +"</td>");

            sb.Append("<td>" + dtSafety.Rows[C][3].ToString() + "</td>");
            sb.Append("<td>" + dtSafety.Rows[C][4].ToString() + "</td>");
            sb.Append("<td>" + dtSafety.Rows[C][2].ToString() +"</td>");
            sb.Append("</tr>");
            C++;
        }
        sb.Append("</table>");

        Response.Clear();
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
        Response.Write(sb.ToString());
        Response.End();
    }
    
}
