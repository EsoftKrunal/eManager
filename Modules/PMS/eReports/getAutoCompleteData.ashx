<%@ WebHandler Language="C#" Class="getAutoCompleteData" %>

using System;
using System.Web;
using Newtonsoft.Json;
using System.Data;

public class getAutoCompleteData : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        if (Convert.ToString(context.Request.Form["Type"]) == "PORT")
        {
            string Key = Convert.ToString(context.Request.Form["Key"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 100 PortName FROM DBO.Port WHERE PortName LIKE '" + Key + "%' ORDER BY PortName ");
            string json = JsonConvert.SerializeObject(dt, Formatting.None);
            context.Response.Write("{\"totalResultsCount\" : " + dt.Rows.Count.ToString() + ",\"geonames\" : " + json + " }");
        }
        else if (Convert.ToString(context.Request.Form["Type"]) == "STORE_ITEMS")
        {
            string Key = Convert.ToString(context.Request.Form["Key"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ITEMNAME as PortName FROM [dbo].[MP_VSL_StoreItemMaster] WHERE ITEMNAME LIKE '%" + Key + "%' ORDER BY ITEMNAME ");
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