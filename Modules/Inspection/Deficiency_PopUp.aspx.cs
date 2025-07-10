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

public partial class Transactions_Deficiency_PopUp : System.Web.UI.Page
{
    string ObsDefText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            ObsDefText = Session["ObsDef"].ToString();
            txtObsDef.Text = ObsDefText;
        }
        catch { }
    }
}
