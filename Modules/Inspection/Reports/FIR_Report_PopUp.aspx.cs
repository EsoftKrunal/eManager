using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Reports_FIR_Report_PopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        string FileName = ConfigurationManager.AppSettings["FIRPath"] + "\\" + Request.QueryString["File"];
        try
        {
            Response.WriteFile(FileName);
        }
        catch {
            Response.Write("File Not Exists.");
        } 
    }
}
