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

public partial class Reporting_CRMReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblmessage.Text = "";
        if (Page.IsPostBack == false)
        {
            //-----------------------------
            SessionManager.SessionCheck_New();
            //-----------------------------
            this.ddl_crmcategory.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 132);
            if (chpageauth <= 0)
            {
                Response.Redirect("DummyReport.aspx");
            }
            //*******************
            bindCRMCategoryDDL();
            this.lblmessage.Text = "";
            DataTable dt1 = CRMReport.selectCRMReportDetails(-1);
            Session.Add("rptsource22", dt1);
        }
        try
        {
            DataTable dt = PrintCrewList.selectCompanyDetails();
            DataTable dt1 = ((DataTable)Session["rptsource22"]);
            if (dt1.Rows.Count > 0)
            {
                btn_show_Click(new object(), new EventArgs());  
            }
            else
            {
                IFRAME1.Attributes.Add("src", ""); 
            }
        }
        catch
        {

        }
    }
    public void bindCRMCategoryDDL()
    {
        DataTable dt = CRMReport.selectCRMCategory();
        this.ddl_crmcategory.DataValueField = "CRMCategory";
        this.ddl_crmcategory.DataTextField = "CRMCategoryName";
        this.ddl_crmcategory.DataSource = dt;
        this.ddl_crmcategory.DataBind();
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        int selvalue = int.Parse(ddl_crmcategory.SelectedValue);
        DataTable dt1 = CRMReport.selectCRMReportDetails(Convert.ToInt32(ddl_crmcategory.SelectedValue));
        if (dt1.Rows.Count > 0)
        {
            IFRAME1.Attributes.Add("src", "CrewCRMContainer.aspx?selindex=" + selvalue);  
        }
        else
        {
             IFRAME1.Attributes.Add("src", ""); 
            this.lblmessage.Text = "No Crew Member Exists.";
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
