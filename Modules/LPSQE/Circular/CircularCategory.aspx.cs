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
//using System.Xml.Linq;

public partial class FormReporting_CircularCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
    }
    
    //Events
    protected void OnClick_btnAddCat(object sender, EventArgs e)
    {
        
    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        
    }
    
}
