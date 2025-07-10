using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for SessionManager
/// </summary>
public class SessionManager
{
	public SessionManager()
	{
		
	}

    public static void SessionCheck_New()
    {
        if (HttpContext.Current.Session["loginid"] == null)
        {
            System.Web.UI.Page currentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;
            ScriptManager.RegisterStartupScript(currentPage, currentPage.GetType(), "axdxasda", "alert('Application session expired. Please login again.');", true);
            string sessionExpiredURL = ConfigurationManager.AppSettings["SessionExpired"];
            HttpContext.Current.Response.Redirect(sessionExpiredURL);
        }
    }
}