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
        
        if (type == "PONO")
        {
            string Key = Convert.ToString(context.Request.Form["Key"]);

            if (Key == null)
                Key = Convert.ToString(context.Request.QueryString["Key"]);
            
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select top 15 bidponum,bidid from [dbo].[VW_tblSMDPOMasterBid] WHERE VW_tblSMDPOMasterBid.BidId NOT In (select distinct tblApEntries.bidid from dbo.[tblApEntries] where intrav=1) and bidponum LIKE '" + Key + "%' AND ispo=1 ORDER BY bidponum");
            string json = JsonConvert.SerializeObject(dt, Formatting.None);
            context.Response.Write("{\"totalResultsCount\" : " + dt.Rows.Count.ToString() + ",\"geonames\" : " + json + " }");
        }
        else if (type == "VEN")
        {
            string Key = Convert.ToString(context.Request.Form["Key"]);
            if (Key == null)
                Key = Convert.ToString(context.Request.QueryString["Key"]);
            
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SupplierId,ISNULL(SUPPLIERNAME,'') AS SupplierName ,ISNULL(SUPPLIERNAME,'') + ' - (' + ISNULL(TravID,'') + ')' as SupplierNameCode,Active FROM dbo.VW_ALL_VENDERS WHERE Active=1 and SupplierName LIKE '" + Key.Replace("'", "`") + "%' ORDER BY SupplierName");
            string json = JsonConvert.SerializeObject(dt, Formatting.None);
            context.Response.Write("{\"totalResultsCount\" : " + dt.Rows.Count.ToString() + ",\"geonames\" : " + json + " }");
        }
        else  if (type == "VENALL")
        {
            string Key = Convert.ToString(context.Request.Form["Key"]);
            if (Key == null)
                Key = Convert.ToString(context.Request.QueryString["Key"]);
            
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SupplierId,ISNULL(SUPPLIERNAME,'') AS SupplierName ,ISNULL(SUPPLIERNAME,'') + ' - (' + ISNULL(TravID,'') + ')' as SupplierNameCode,Active FROM dbo.VW_ALL_VENDERS WHERE SupplierName LIKE '" + Key.Replace("'", "`") + "%' ORDER BY SupplierName");
            string json = JsonConvert.SerializeObject(dt, Formatting.None);
            context.Response.Write("{\"totalResultsCount\" : " + dt.Rows.Count.ToString() + ",\"geonames\" : " + json + " }");
        }
        else if (type == "APPONO")
        {
            string Key = Convert.ToString(context.Request.Form["Key"]);

            if (Key == null)
                Key = Convert.ToString(context.Request.QueryString["Key"]);
            
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select top 15 bidponum,bidid from [dbo].[VW_tblSMDPOMasterBid] WHERE BidStatusID = 3 and bidponum LIKE '" + Key + "%' AND ispo=1 ORDER BY bidponum");
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