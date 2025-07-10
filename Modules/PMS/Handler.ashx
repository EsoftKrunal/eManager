<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler,System.Web.SessionState.IRequiresSessionState  {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if(""+context.Request.QueryString["key"]=="POP3")
        {
            //context.Session["pop_importstatus"] = DateTime.Now.ToLongTimeString();
            context.Response.Write("" + context.Session["pop_importstatus"]);
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}