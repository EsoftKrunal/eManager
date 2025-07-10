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

public partial class Reporting_ConciseMonthlyWagesReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.txt_EmpNo.Attributes.Add("onkeydown","javascript:if(event.keycode==13){document.getElementById('"+btn_show.ClientID+"').focus();}");
        this.txt_from.Attributes.Add("onkeydown","javascript:if(event.keycode==13){document.getElementById('"+btn_show.ClientID+"').focus();}");
        this.txt_to.Attributes.Add("onkeydown", "javascript:if(event.keycode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 131);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //******************
        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {

        }
        else
        {
            btn_show_Click(sender, e);
        }
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        IFRAME1.Attributes.Add("src", "ConciseMonthlyWageReport_Crystal.aspx?EmpNo=" + txt_EmpNo.Text + "&From=" + txt_from.Text + "&To=" + txt_to.Text);
    }
}
