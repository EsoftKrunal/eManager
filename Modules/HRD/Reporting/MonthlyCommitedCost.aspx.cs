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

public partial class Reporting_MonthlyCommitedCost : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        this.ddmonth.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        this.ddyear.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 122);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************
        this.lblMessage.Text = "";
        if (Page.IsPostBack == false)
        {
            int yr;
            yr = (Convert.ToInt16(System.DateTime.Today.Year));
            for (int i = yr - 1; i <= yr + 1; i++)
            {
                this.ddyear.Items.Add(i.ToString());
                // 
            }
            ddyear.Items[1].Selected = true;
            ddmonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
        }
        if (Page.IsPostBack != false)
        {
            showdata();
        }
        if (!(IsPostBack))
        {
            this.ddvessel.DataTextField = "Column1";
            this.ddvessel.DataValueField = "VesselId";
            this.ddvessel.DataSource = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselCode + ':' + VesselName", Convert.ToInt32(Session["loginid"].ToString()));
            this.ddvessel.DataBind();
            this.ddvessel.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    private void showdata()
    {
        IFRAME1.Attributes.Add("src", "MonthlyComittedCostContainer.aspx?month=" + ddmonth.SelectedValue + "&year=" + ddyear.SelectedValue + "&vessel=" + ddvessel.SelectedValue + "&monthname=" + ddmonth.SelectedItem.Text + "&yearname=" + ddyear.SelectedItem.Text + "&vesselname=" + ddvessel.SelectedItem.Text );  
        lblMessage.Text = "";
    }
}
