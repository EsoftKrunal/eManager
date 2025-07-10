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

public partial class Reporting_ContractDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblmessage.Text = "";
        this.ddl_UserName.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.txt_from.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.txt_to.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        if (!(IsPostBack))
        {
            Session["PageCode"] = "4";
            bindVesselDDL();
        }
        else
        {
            Session.Remove("PageCode");
            
        }
    }
    public void bindVesselDDL()
    {
        DataTable dt = ContractDetails.selectUserNames();
        this.ddl_UserName.DataValueField = "LoginId";
        this.ddl_UserName.DataTextField = "UserName";
        this.ddl_UserName.DataSource = dt;
        this.ddl_UserName.DataBind();
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        IFRAME1.Attributes.Add("src", "ContractDetails_Crystal.aspx?UID=" + ddl_UserName.SelectedValue + "&FromDate=" + txt_from.Text + "&ToDate=" + txt_to.Text);
       
    }
}
