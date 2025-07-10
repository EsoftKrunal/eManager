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

public partial class Reports_SireChapterReport : System.Web.UI.Page
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
    
    public DataTable dtChartData
    {
        set { ViewState["dtChartData"] = value; }
        get { return (DataTable)(ViewState["dtChartData"]); }
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
            ShowData1();
        }
    }
    protected void ShowData1()
    {
        string datefilter = "";

        if (FromDate.Trim() != "")
            datefilter = "AND ActualDate >='" + FromDate + "'";

        if (ToDate != "")
            datefilter = "AND ActualDate <='" + ToDate + "'";

        string sql = "select *,'<br/>&nbsp;RC : ' + [dbo].[getSireChapters](ChapId) as CausesList from vw_ObservationsAllSireChapter where InspectionDueId in ( " +
                     "select id from t_InspectionDue d where d.InspectionId In (select id from m_inspection where InspectionGroup=4 union select 43) " + ((datefilter == "") ? "" : datefilter) + ") ";
        string Filter = "";
        char[] sep = { ',' };

        if (Status == "O")
        {
            Filter += " And Status='O'";
        }
        else if (Status == "C")
        {
            Filter += " And Status='C'";
        }
        sql += Filter;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        dtChartData = Common.Execute_Procedures_Select_ByQuery("DBO.RC_SIRE_ANALYSIS '" + FromDate + "','" + ToDate + "'");
        BindChart1("43,4".Split(sep), "SAFETY,PSC".Split(sep));
    }
    protected void BindChart1(string[] InsFilter, string[] InsNames)
    {
        StringBuilder sbm = new StringBuilder();
        sbm.Append("<td style='text-align:left;'>Root Cause</td>");
        foreach (string item in InsNames)
        {
            sbm.Append("<td colspan='3' style='text-align:center;'>" + item + "</td>");
        }
        Literal1.Text = sbm.ToString();

        StringBuilder sb = new StringBuilder();
        sb.Append("<td style='text-align:left;'>&nbsp;</td>");
        foreach (string item in InsFilter)
        {
            sb.Append("<td style='width:100px;text-align:center;'>Count</td>");
            sb.Append("<td style='width:100px;text-align:center;'>Insp. Count</td>");
            sb.Append("<td style='width:100px;text-align:center;'>Count PER Insp.</td>");
        }
        Literal2.Text = sb.ToString();

        //sbm = new StringBuilder();
        //sb = new StringBuilder();

        //int c = 1;
        //foreach (string item in InsFilter)
        //{
        //    sbm.Append(", ROUND((CAST (RCCount" + c.ToString() + " AS FLOAT) / (CASE WHEN InsCount" + c.ToString() + "=0 THEN 1 ELSE InsCount" + c.ToString() + " END) ),2) AS PER" + c.ToString());
        //    //---------------
        //    sb.Append(",(select count(*) from vw_ObservationsAllSireChapter o where status='C' And o.InspectionDueId in (select i.id from t_InspectionDue i where " + InsFilter[c - 1] + " And " + ((datefilter == "") ? "" : datefilter) + " and (',' + o.CHAPID + ',') LIKE ('%,' + CONVERT(VARCHAR,m.Id) + ',%'))) AS RCCount" + c.ToString() + " ");
        //    sb.Append(",(select count(i.id) from t_InspectionDue i where " + ((datefilter == "") ? "" : datefilter + " And ") + InsFilter[c - 1] + ") AS InsCount" + c.ToString() + " ");
        //    //---------------
        //    c++;
        //}
        //string sql = " SELECT *" + sbm.ToString() + " FROM ( " +
        //             " SELECT ChapterName" + sb.ToString() +
        //             " FROM m_chapters m where inspectiongroup=1 " +
        //             " ) A";
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);


        sbm = new StringBuilder();
        sb = new StringBuilder();

        int c = 1;
        foreach (string item in InsFilter)
        {
            sbm.Append(",0.0 AS PER" + c.ToString() + ",0.0 AS RCCount" + c.ToString() + ",0.0 AS InsCount" + c.ToString());
            //---------------
            c++;
        }

        string sql = " SELECT ChapterName,Id as ChapterId" + sbm.ToString() + " FROM m_chapters where inspectiongroup=1 order by ChapterName";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        DataView dv = dtChartData.DefaultView;

        foreach (DataRow dr in dt.Rows)
        {
            c = 1;
            foreach (string item in InsFilter)
            {
                dv.RowFilter = "ChapterId=" + dr["ChapterId"].ToString() + " And InspectionId=" + item;
                DataTable dtres = dv.ToTable();
                if (dtres.Rows.Count == 1)
                {
                    dr[(c * 3) - 1] = dtres.Rows[0]["PER"];
                    dr[(c * 3)] = dtres.Rows[0]["ObsCount"];
                    dr[(c * 3) + 1] = dtres.Rows[0]["InsCount"];
                }
                //---------------
                c++;
            }

        }

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
            Chart1.Series[item].IsValueShownAsLabel = true;
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

    protected void download_click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        DataTable dtSire = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 1,'" + FromDate + "','" + ToDate + "'");
        DataTable dtPSC = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 4,'" + FromDate + "','" + ToDate + "'");
        DataTable dtSafety = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 0,'" + FromDate + "','" + ToDate + "'");

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
        int C = 0;
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("<tr>");
            sb.Append("<td>" + dr["ChapterName"].ToString() + "</td>");
            sb.Append("<td>" + dtSire.Rows[C][3].ToString() + "</td>");
            sb.Append("<td>" + dtSire.Rows[C][4].ToString() + "</td>");
            sb.Append("<td>" + dtSire.Rows[C][2].ToString() + "</td>");

            sb.Append("<td>" + dtPSC.Rows[C][3].ToString() + "</td>");
            sb.Append("<td>" + dtPSC.Rows[C][4].ToString() + "</td>");
            sb.Append("<td>" + dtPSC.Rows[C][2].ToString() + "</td>");

            sb.Append("<td>" + dtSafety.Rows[C][3].ToString() + "</td>");
            sb.Append("<td>" + dtSafety.Rows[C][4].ToString() + "</td>");
            sb.Append("<td>" + dtSafety.Rows[C][2].ToString() + "</td>");
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
