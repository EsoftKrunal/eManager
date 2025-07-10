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
        
        if (type == "HAZARD")
        {
            string Key = Convert.ToString(context.Request.Form["Key"]);

            if (Key == null)
                Key = Convert.ToString(context.Request.QueryString["Key"]);
            
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT HAZARDCODE + ' : ' + HAZARDNAME AS HNAME,HAZARDID FROM MTMPMS.DBO.EV_HAZARDMASTER WHERE HAZARDNAME LIKE '" + Key + "%' ORDER BY HAZARDNAME ");
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