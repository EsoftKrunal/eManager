using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class RCAanalysis : System.Web.UI.Page
{
    public DataTable DtData
    {
        get {
            if (ViewState["_data"] != null)
            {
                return (DataTable)ViewState["_data"];
            }
            else
            {
                string SQL = " WITH Hierarchy(AnalysisID, Cause, Generation, ParentAnalysisID) " +
                     "  AS " +
                     "  ( " +
                     "      SELECT AnalysisID, Cause, 0, ParentAnalysisID " +
                     "          FROM dbo.ER_S115_Report_RCA_Analysis AS FirtGeneration " +
                     "          WHERE ParentAnalysisID = 0 " +
                     "      UNION ALL " +
                     "      SELECT NextGeneration.AnalysisID, NextGeneration.Cause, Parent.Generation + 1, Parent.AnalysisID " +
                     "          FROM dbo.ER_S115_Report_RCA_Analysis AS NextGeneration " +
                     "          INNER JOIN Hierarchy AS Parent ON NextGeneration.ParentAnalysisID = Parent.AnalysisID " +
                     "  ) " +
                     "  SELECT AnalysisID, Cause, ParentAnalysisID, Generation FROM Hierarchy order by Generation,AnalysisID ";
                SQL = " select * from dbo.ER_S115_Report_RCA_Analysis where ReportID="+ReportID+" and VesselCode='"+VesselCode+"' ";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                ViewState["_data"] = dt;
                return dt;
            }
        }
    }
    public int Generation
    {
        get { return Common.CastAsInt32(ViewState["_Generation"]); }
        set { ViewState["_Generation"] = value; }
    }
    public int ReportID
    {
        get { return Common.CastAsInt32(ViewState["_ReportID"]); }
        set { ViewState["_ReportID"] = value; }
    }
    public string VesselCode
    {
        get { return Convert.ToString(ViewState["_VesselCode"]); }
        set { ViewState["_VesselCode"] = value; }
    }
    public StringBuilder myHtml = new StringBuilder();
    public int counter=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportID = Common.CastAsInt32(Page.Request.QueryString["ReportId"]);
        VesselCode = Convert.ToString(Page.Request.QueryString["VesselCode"]);
        litFocalPoint.Text = Convert.ToString(Page.Request.QueryString["focalpoint"]);
        SHow(0);
        
        litTree.Text = myHtml.ToString();
        
    }

    protected void SetRootCause(object sender, EventArgs e)
    {
        int AnalysisID = 0;
        string sql = "update ER_S115_Report_RCA_Analysis set Status='R' Where AnalysisID="+ AnalysisID;
        Common.Execute_Procedures_Select_ByQuery(sql);

    }
    
    protected void RemoveCause(object sender, EventArgs e)
    {
    }
    
        

    public void SHow(int ParentAnalysisID )
    {
        DataView dv = DtData.DefaultView;
        dv.RowFilter = "ParentAnalysisID="+ ParentAnalysisID;
        if (dv.ToTable().Rows.Count != 0)
        {
            if (counter == 0)
                myHtml.Append("<ul style='margin-top:0px; margin-left:35px;'>");
            else
                myHtml.Append("<ul>");

            counter++;

            foreach (DataRow Dr in dv.ToTable().Rows)
            {
                myHtml.Append("<li>");
                myHtml.Append("<a href='#'>");
                myHtml.Append("<span class='fa "+ StatusClass ( Dr["Status"].ToString()) + " terminated'></span>");
                myHtml.Append("<div class='causeheading'>" + ((Dr["HasChilds"].ToString()=="True")?"WHY ?":"Cause") + "</div>");
                myHtml.Append("<div class='causesummary'>" + Dr["Cause"]  + "</div>");
                myHtml.Append("</a>");
                myHtml.Append("<div class='action' key='"+ Dr["AnalysisID"].ToString() + "'>");
                myHtml.Append("<i class='fa fa-chevron-down'></i>");
                myHtml.Append("</div>");
                SHow(Common.CastAsInt32(Dr["AnalysisID"]));
                myHtml.Append("</li>");
            }
            myHtml.Append("</ul>");
        }
        
    }

    public string StatusClass(string Cause)
    {
        string ret = "";
        switch (Cause)
        {
            case "C":
                ret = "causestatus";
                break;
            case "R":
                ret = "rootcause";
                break;
            case "T":
                ret = "terminated";
                break;
            default:
                ret = "causestatus";
                break;
        }
        return ret;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string terminatecause(int aid)
    {
        string sql = "update dbo.ER_S115_Report_RCA_Analysis set Status='T' Where AnalysisID=" + aid;        
        Common.Execute_Procedures_Select_ByQuery(sql);

        sql = " exec dbo.ER_S115_RemoveChilds " + aid;
        Common.Execute_Procedures_Select_ByQuery(sql);

        return aid.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string setrootcause(int aid)
    {
        string sql = "update dbo.ER_S115_Report_RCA_Analysis set Status='R' Where AnalysisID=" + aid;
        //string sql = "update ER_S115_Report_RCA_Analysis set Status='T' Where AnalysisID=999";
        Common.Execute_Procedures_Select_ByQuery(sql);

        sql = " exec dbo.ER_S115_RemoveChilds " + aid;
        Common.Execute_Procedures_Select_ByQuery(sql);

        return aid.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string updatecause(int aid,string cause)
    {
        string sql = "update dbo.ER_S115_Report_RCA_Analysis set cause='"+ cause.Replace("'","`") + "' Where AnalysisID=" + aid;
        //string sql = "update ER_S115_Report_RCA_Analysis set Status='T' Where AnalysisID=999";
        Common.Execute_Procedures_Select_ByQuery(sql);
        return aid.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string removecause(int aid)
    {
        string sql = "exec dbo.ER_S115_RemoveRootCause " + aid;        
        Common.Execute_Procedures_Select_ByQuery(sql);
        return aid.ToString();
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static string savecause(int aid,int reportid,string vesselcode,string cause,string mode)
    {
        
        string ret = "error";
        try
        {
            Common.Set_Procedures("[DBO].ER_S115_IU_ER_S115_Report_RCA_Analysis");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@AnalysisID", aid),
                new MyParameter("@ReportId", reportid),
                new MyParameter("@VesselCode", vesselcode),                
                new MyParameter("@Cause", cause),
                new MyParameter("@mode", mode)
                
                );
            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                ret = "OK";
            }
        }
        catch (Exception ex)
        {
            
        }

        return ret;
    }
}