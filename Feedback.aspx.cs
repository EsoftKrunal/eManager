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


public partial class FeedBack : System.Web.UI.Page
{
    int UserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        UserId = int.Parse(Session["UserId"].ToString());
        rpt_feed.DataSource = Common.Execute_Procedures_Select_ByQuery("select FeedBackId,isnull(Approved,'N') as Approved,(SELECT APPLICATIONNAME FROM APPLICATIONMASTER am where am.ApplicationId=moduleid) as APPLICATIONNAME,case when ctype='F' THEN 'FeedBack' else 'Request' end as RF, " +
                                                                    "Descr as [Message],(select userid from usermaster u where u.loginid=uf.postedby) as UserId, " +
                                                                    "convert(varchar,postedon,107) as PostedOn " +
                                                                    "from usersfeedback uf order by postedon desc");
        rpt_feed.DataBind();  
    }
}

