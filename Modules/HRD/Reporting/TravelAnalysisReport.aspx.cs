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

public partial class Reporting_TravelAnalysisReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    int VesselId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 178);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Show.ClientID + "').focus();}");
        this.ddl_ToMonth.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Show.ClientID + "').focus();}");
        this.ddl_Year.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Show.ClientID + "').focus();}");
        //*******************
        if (!(IsPostBack))
        {
            bindddlTravelAgent();
            DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
            this.ddl_Vessel.DataSource = dt8;
            this.ddl_Vessel.DataValueField = "VesselId";
            this.ddl_Vessel.DataTextField = "Name";
            this.ddl_Vessel.DataSource = dt8;
            this.ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
            for (int i = 2000; i < 2050; i++)
            {
                ddl_Year.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("Select", "0"));
            ddl_Year.SelectedValue = DateTime.Today.Year.ToString();
            ddl_ToMonth.SelectedIndex = DateTime.Today.Month;
        }
        else
        {
            ShowData();
        }
    }
    public void bindddlTravelAgent()
    {
        DataSet dt2 = Budget.getTable("SELECT TRAVELAGENTID,COMPANY FROM TRAVELAGENT");
        this.ddl_Vendor.DataValueField = "TRAVELAGENTID";
        this.ddl_Vendor.DataTextField = "COMPANY";
        this.ddl_Vendor.DataSource = dt2.Tables[0];
        this.ddl_Vendor.DataBind();
        ddl_Vendor.Items.Insert(0, new ListItem(" < All > ", "0"));
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void ShowData()
    {
        IFRAME1.Attributes.Add("src", "TravelAnalysisRContainer.aspx?vid=" + ddl_Vessel.SelectedValue + "&month=" + ddl_ToMonth.SelectedValue + "&year=" + ddl_Year.SelectedValue + "&vendor=" + ddl_Vendor.SelectedValue + "&vname=" + ddl_Vessel.SelectedItem.Text + "&monthname=" + ddl_ToMonth.SelectedItem.Text + "&yearname=" + ddl_Year.SelectedItem.Text + "&vendorname=" + ddl_Vendor.SelectedItem.Text);
    }
    protected void Show_Click(object sender, EventArgs e)
    {
      ShowData();
    }
}
