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

public partial class Reporting_AllotmentRequestReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.ddl_Month.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.ddl_Year.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        
        if (!Page.IsPostBack)
        {
            ddl_Month.SelectedValue = Convert.ToString(DateTime.Now.Month);
            BindYearDropDown();
            ddl_Vessel.DataSource = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
            ddl_Vessel.DataTextField = "VesselName";
            ddl_Vessel.DataValueField = "VesselId";
            ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
        }
        else
        {
            Button1_Click(sender,e);
        }
    }
    private void BindYearDropDown()
    {
        int i;
        i = DateTime.Today.Year;
        ddl_Year.Items.Add(new ListItem((i - 1).ToString(), (i - 1).ToString()));
        ddl_Year.Items.Add(new ListItem(i.ToString(), i.ToString()));
        ddl_Year.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
        ddl_Year.Items[1].Selected = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int vid =Convert.ToInt32(ddl_Vessel.SelectedValue);
        int month = Convert.ToInt32(ddl_Month.SelectedValue);
        int year = Convert.ToInt32(ddl_Year.SelectedValue);

        string vesselname = ddl_Vessel.SelectedItem.Text;
        string Monthname = ddl_Month.SelectedItem.Text;
        string yearname = ddl_Year.SelectedItem.Text;

        IFRAME1.Attributes.Add("src", "MonthlyComittedCostContainer.aspx?month=" + month + "&year=" + year + "&vessel=" + vid + "&monthname=" + Monthname + "&yearname=" + yearname + "&vesselname=" + vesselname);  
    }
}
