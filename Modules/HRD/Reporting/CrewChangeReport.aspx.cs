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

public partial class Reporting_CrewChangeReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblMessage.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        if (!(IsPostBack))
        {
            DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
            this.ddl_Vessel.DataSource = dt8;
            this.ddl_Vessel.DataValueField = "VesselId";
            this.ddl_Vessel.DataTextField = "Name";
            this.ddl_Vessel.DataSource = dt8;
            this.ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
        }

       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txt_from.Text) > Convert.ToDateTime(txt_to.Text))
        {
            this.lblMessage.Text = "From Date Should be less than To Date.";
            IFRAME1.Attributes.Add("src", "");
        }
        else
        {
            this.lblMessage.Text = "";
            int i = Convert.ToInt32(RadioButtonList1.SelectedValue);
            IFRAME1.Attributes.Add("src", "Crew_Change_Report.aspx?VID=" + ddl_Vessel.SelectedValue + "&FromDate=" + txt_from.Text + "&ToDate=" + txt_to.Text + "&RdLst=" + i);
        }
    }
}
