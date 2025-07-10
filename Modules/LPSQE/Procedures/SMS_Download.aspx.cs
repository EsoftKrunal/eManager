using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SMS_SMS_Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.WriteFile(@"C:\inetpub\wwwroot\SHIPSOFT\Auto Alert\SMS PDF Merging\SMS_Publish\" + Request.QueryString["File"]);
    }
}