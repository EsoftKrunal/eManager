<%@ WebHandler Language="C#" Class="getAutoCompleteData" %>
using System;
using System.Web;
using Newtonsoft.Json;
using System.Data;

public class getAutoCompleteData : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string type = Convert.ToString(context.Request.Form["Type"]);
        if (type == null)
            type = Convert.ToString(context.Request.QueryString["Type"]);
        
        if (type == "KPI")
        {
            string Key = Convert.ToString(context.Request.Form["Key"]);

            if (Key == null)
                Key = Convert.ToString(context.Request.QueryString["Key"]);
            
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select entryid,sno + ' - ' + kpiname as kname from dbo.kpi_entry where sno LIKE '" + Key + "%' or kpiname LIKE '" + Key + "%' order by kpiname");
            string json = JsonConvert.SerializeObject(dt, Formatting.None);
            context.Response.Write("{\"totalResultsCount\" : " + dt.Rows.Count.ToString() + ",\"geonames\" : " + json + " }");
        }
        else
        {
            context.Response.Write("adadad");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}