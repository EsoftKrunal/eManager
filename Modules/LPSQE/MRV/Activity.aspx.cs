using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MRV_Activity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        Session["MM"] = 3;
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            BindActivity();
        }
    }

    public void BindActivity()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select ActivityId,ActivityName from dbo.MRV_Activity order by ActivityId");
        rptActivity.DataSource = dt;
        rptActivity.DataBind();
    }

}
