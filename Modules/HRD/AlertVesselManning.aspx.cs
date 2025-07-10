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

public partial class AlertVesselManning : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        DataTable dt;
        dt=Alerts.getVesselManningAlert();
        gv_VesselManning.DataSource = dt;
        gv_VesselManning.DataBind();
    }
    protected void gv_VesselManning_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hfd;
        int i;
        hfd = (HiddenField)e.Row.FindControl("hiddenAvalue");
        if (hfd != null)
        {
            i = Convert.ToInt32(hfd.Value);
            if (i==1)
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#99ff33");
            }
            else if (i==2)
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#fcc2bc");

            }
        }
    }
}
