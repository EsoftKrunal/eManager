<%@ WebHandler Language="C#" Class="getPartialData" %>
using System;
using System.Web;
using System.Data;

public class getPartialData : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        int PageId =Common.CastAsInt32(context.Request.QueryString["PageId"]);
        string RequestData = context.Request.QueryString["RequestData"];
        string Param = context.Request.QueryString["Param"];

        switch (PageId)
        {
            case 1:
                switch (RequestData)
                {
                    case "QuestionDetails":
                        DataTable dt = Budget.getTable("SELECT * FROM DBO.m_Questions WHERE ID=" + Param).Tables[0];
                        DataTable dt1 = Budget.getTable("SELECT (CASE WHEN EXISTS(SELECT * FROM DBO.m_Questions_Ranks WHERE RANKID=MP_ALLRANK.RANKID AND QUESTIONID=" + Param + ") THEN 1 ELSE 0 END )AS RANK FROM DBO.MP_ALLRANK WHERE LTRIM(RTRIM(RANKID)) IN(1,2,4,12,15)").Tables[0];
                        string s="";
                        foreach (DataRow dr in dt1.Rows) 
                        {
                            s +=","+ dr["RANK"].ToString();
                        }
                        if (s.StartsWith(","))
                            s = s.Substring(1);


                        DataTable dt2 = Budget.getTable("SELECT (CASE WHEN EXISTS(SELECT * FROM DBO.m_Questions_Ranks_Inv WHERE RANKID=MP_ALLRANK.RANKID AND QUESTIONID=" + Param + ") THEN 1 ELSE 0 END )AS RANK FROM DBO.MP_ALLRANK WHERE LTRIM(RTRIM(RANKID)) IN(6,9,28,34,30,35,36,16,17,25,21,38,40,42,45)").Tables[0];
                        string s1="";
                        foreach (DataRow dr in dt2.Rows) 
                        {
                            s1 +=","+ dr["RANK"].ToString();
                        }
                        if (s1.StartsWith(","))
                            s1 = s1.Substring(1);
                        
                        
                        int PreVetting = 0;
                        PreVetting = Common.CastAsInt32(dt.Rows[0]["PreVetting"]);
                        string JsonResult = "{\"Qno\":\"" + dt.Rows[0]["QuestionNo"].ToString() + "\",\"PreVetting\":\" " + PreVetting + "\",\"Description\":\" " + Format_JSON(dt.Rows[0]["Description"]) + "\",\"Question\":\" " + Format_JSON(dt.Rows[0]["Question"]) + "\",\"OfficeRemarks\":\" " + Format_JSON(dt.Rows[0]["OfficeRemarks"]) + "\", \"Responsibilites\":[" + s + "],\"Responsibilites1\":[" + s1 + "]}";
                        context.Response.Write(JsonResult);
                        break;
                }
                break; 
                case 2:
                switch (RequestData)
                {
                    case "NextDueDate":
                        string[] parts = Param.Split('|');
                        int vslid = Common.CastAsInt32(parts[0]);
                        int Group =Common.CastAsInt32(parts[1]);
                        DataTable dt = Budget.getTable("EXEC DBO.PR_RPT_OperatorReporting_Summary '" + vslid + "'").Tables[0];
                        string nextDate = "",lastDate="",lastInspName="";
                        if (dt.Rows.Count>0)
                        {
                            if (Group == 1)
                            {
                                lastInspName = dt.Rows[0]["LastInspGrpName"].ToString();
                                lastDate=Common.ToDateString(dt.Rows[0]["LastInspGrpDoneDt"]);
                                nextDate =Common.ToDateString(dt.Rows[0]["NextInspGrpDueDt"]);
                            }
                            else
                            {
                                lastInspName = "CDI";
                                lastDate=Common.ToDateString(dt.Rows[0]["LastCDIDate"]);
                                nextDate = Common.ToDateString(dt.Rows[0]["NextCDIDate"].ToString());
                            }
                        }
                        string LastVIQDate = "";
                        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 TARGETDATE FROM [dbo].[VIQ_VIQMaster] WHERE VESSELCODE IN ( SELECT VESSELCODE FROM DBO.VESSEL WHERE VESSELID=" + vslid + ") AND INSPGROUPID=" + Group + " ORDER BY TARGETDATE DESC");
                        if (dt1.Rows.Count > 0)
                        {
                            LastVIQDate = Common.ToDateString(dt1.Rows[0][0]);
                        }
                        context.Response.Write("{\"LastVIQDate\":\"" + LastVIQDate + "\",\"LastInspName\":\"" + lastInspName + "\",\"LastDoneDate\":\"" + lastDate + "\",\"NextDueDate\":\"" + nextDate + "\"}");
                        
                        break;
                }
                break;
        }
            
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public string Format_JSON(object in_str)
    {
        return in_str.ToString().Replace("\r", "").Replace("\n", "").Replace("'", "`").Replace(@"\", @"\\").Replace("\"", "\\\"");
        //return in_str.ToString().Replace("\r", "").Replace("\n", "").Replace("'", "").Replace("\"", "");
    }
}