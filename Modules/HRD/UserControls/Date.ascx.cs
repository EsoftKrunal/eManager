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

public partial class UserControls_Date : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        this.Text = DateTime.Now.ToShortDateString();
        base.OnInit(e);
    }
    public string Text
    {
        get { return txtCalendar.Text; }
        set { txtCalendar.Text = value; }
    }

}
