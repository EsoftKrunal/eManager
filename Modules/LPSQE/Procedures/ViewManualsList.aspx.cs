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


public partial class ViewManualsList : System.Web.UI.Page
{
    public ManualBO ob_ManualBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            LoadManuals();
        }
    }
    public void LoadManuals()
    {
       DataTable dt=Manual.getManualList();

    }
}
