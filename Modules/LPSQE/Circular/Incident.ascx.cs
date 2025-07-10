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

public partial class Incident : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["FormSelection"] == "1")
        {
            RadioButtonList1.SelectedValue = "1";
        }
        if (Session["FormSelection"] == "2")
        {
            RadioButtonList1.SelectedValue = "2";
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            Response.Redirect("~/HSSQE/NearMiss.aspx");
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            Response.Redirect("~/HSSQE/Accident.aspx");
        }
        
    }
}
