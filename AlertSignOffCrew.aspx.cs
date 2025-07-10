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

public partial class AlertSignOffCrew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        DataTable dt;
        dt = Alerts.getSignOffCrewAlert();
        gv_SignOff.DataSource = dt;
        gv_SignOff.DataBind(); 
    }
    protected void gv_SignOff_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hfd;
        int i;
        hfd = (HiddenField)e.Row.FindControl("hiddenAvalue");
        if (hfd != null)
        {
            i = Convert.ToInt32(hfd.Value);
            if (i <= 0)
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#99ff33");
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#fcc2bc");

            }
        }
    }
}
