using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class AutoLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int Crewid = Common.CastAsInt32(Request.QueryString["Cid"]);
        int LoginId = Common.CastAsInt32(Request.QueryString["Lid"]);
        Session["loginid"] = LoginId.ToString();

        CreateSession(LoginId);
        Session["CrewId"] = Crewid.ToString();
        Response.Redirect("CrewRecord/CrewDetails.aspx?id=" + Crewid.ToString());
    }
    protected void CreateSession(int LoginId)
    {
        DataTable dt=Budget.getTable("SELECT USERID,PASSWORD FROM DBO.USERLOGIN WHERE LOGINID=" + LoginId.ToString()).Tables[0];
        string Uname=dt.Rows[0][0].ToString();
        string Pword = dt.Rows[0][1].ToString();
       
        ProcessCheckAuthority Auth = new ProcessCheckAuthority(LoginId, 1);
        Auth.Invoke();

        Session["UserName"] = UserLogin.getUserName(LoginId);
        Session["UN"] = Uname;
        Session["Pwd"] = Pword;
        DataTable dt33 = UserLogin.getEmailAddress(LoginId);
        foreach (DataRow dr in dt33.Rows)
        {
            Session["EmailAddress"] = dr["Email"].ToString();
        }
        Session["Authority"] = Auth.Authority;
        Session["Mode"] = "View";
        Session["NoMenu"] = "Y";
    }
}