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

public partial class VesselPositionReporting : System.Web.UI.MasterPage
{
    public Boolean ShowMenu = true;
    public Boolean ShowHeaderbar= true;
    public string PageName
    {
        get {return "" + ViewState["PageName"]; }
        set { ViewState["PageName"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------

        
    }
}
