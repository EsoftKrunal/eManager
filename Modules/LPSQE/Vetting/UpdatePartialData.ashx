<%@ WebHandler Language="C#" Class="UpdatePartialData" %>
using System;
using System.Web;
using System.Data;

public class UpdatePartialData : IHttpHandler
{
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";
        int PageId =Common.CastAsInt32(context.Request.Form["PageId"]);
        string RequestData = context.Request.Form["RequestData"];
        string Param = context.Request.QueryString["Param"];

        switch (PageId)
        {
            case 1:
                switch (RequestData)
                {
                    case "QuestionDetails":
                        DataTable dt = Budget.getTable("SELECT * FROM DBO.m_Questions WHERE ID=" + Param).Tables[0];
                        DataTable dt1 = Budget.getTable("SELECT (CASE WHEN EXISTS(SELECT * FROM DBO.m_Questions_Ranks WHERE RANKID=MP_ALLRANK.RANKID AND QUESTIONID=" + Param + ") THEN 1 ELSE 0 END )AS RANK FROM DBO.MP_ALLRANK").Tables[0];
                        string s="";
                        foreach (DataRow dr in dt1.Rows) 
                        {
                            s +=","+ dr["RANK"].ToString();
                        }
                        if (s.StartsWith(","))
                            s = s.Substring(1);
                        string JsonResult = "{\"Question\":\" " + dt.Rows[0]["Question"].ToString() + "\", \"Responsibilites\":[" + s + "]}";
                        context.Response.Write(JsonResult);
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

}