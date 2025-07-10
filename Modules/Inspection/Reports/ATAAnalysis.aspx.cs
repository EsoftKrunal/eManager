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
    public string Inspections
    {
        set { ViewState["Inspections"] = value; }
        get { return ViewState["Inspections"].ToString(); }
    }
    public string InsNames
    {
        set { ViewState["InsNames"] = value; }
        get { return ViewState["InsNames"].ToString(); }
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
            Inspections = "" + Request.QueryString["Inspections"].ToString();
            InsNames = "" + Request.QueryString["InsNames"].ToString();
        }
    }
    protected void load_click(object sender, EventArgs e)
    {
        DataTable dtSire = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 1,'" + FromDate + "','" + ToDate + "'");
        DataTable dtPSC = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 4,'" + FromDate + "','" + ToDate + "'");
        DataTable dtSafety = Common.Execute_Procedures_Select_ByQuery("exec dbo.getAnalysisData 0,'" + FromDate + "','" + ToDate + "'");
        
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
