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

public partial class Reporting_TicketCancellation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblmessage.Text = "";
        this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.RadioButtonList1.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 133);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //****************
        if (Page.IsPostBack == false)
        {
            DataSet dt1 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
            this.ddl_Vessel.DataSource = dt1;
            this.ddl_Vessel.DataValueField = "VesselId";
            this.ddl_Vessel.DataTextField = "Name";
            this.ddl_Vessel.DataSource = dt1;
            this.ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
        }
        else
        {
            btn_show_Click(sender, e);
        }
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        char i = Convert.ToChar(RadioButtonList1.SelectedValue);
        IFRAME1.Attributes.Add("src", "TicketCancellation_Crystal.aspx?VID=" + ddl_Vessel.SelectedValue + "&RdLst=" + i);
    }
}
