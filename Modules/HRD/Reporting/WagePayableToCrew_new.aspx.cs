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

public partial class Reporting_WagePayableToCrew_new : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddemp.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        this.ddmonth.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        this.ddyear.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()),33);
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
            }
            ddyear.Items[1].Selected = true;
            this.ddmonth.SelectedValue = Page.Request.QueryString["wagemonth"].ToString();
            if (this.ddyear.Items.IndexOf(this.ddyear.Items.FindByText(Page.Request.QueryString["wageyear"].ToString())) < 0)
            {
                ddyear.Items.Add(Page.Request.QueryString["wageyear"].ToString());
            }

            int VesselId, Month, Year;
            VesselId =Convert.ToInt32(Page.Request.QueryString["vi"].ToString());
            Month = Convert.ToInt32(Page.Request.QueryString["wagemonth"].ToString()) ;
            Year = Convert.ToInt32(Page.Request.QueryString["wageyear"].ToString());
            try
            {
              DataSet ds = MonthleWageToCrew.getempname("Payroll_getCrewMembersList_Show",VesselId,Month,Year);
                this.ddemp.DataTextField="Crewnumber";
                this.ddemp.DataValueField="crewid";
                this.ddemp.DataSource=ds;
                this.ddemp.DataBind(); 
            }
            catch
            {

            }
            ddyear.SelectedValue = Page.Request.QueryString["wageyear"].ToString();
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        int crewid = 0;
        int vesselid = 0;
        if (ddemp.Items.Count == 0)
        {
            this.lblMessage.Text = "Select Any Employee.";
            return;

        }
        DataTable dt22 = ReportPrintCV.selectCrewIdCrewNumber(this.ddemp.SelectedItem.Text);
        if (dt22.Rows.Count == 0)
        {
            this.lblMessage.Text = "Invalid Emp#.";
            return;

        }
        foreach (DataRow dr in dt22.Rows)
        {
            crewid = Convert.ToInt32(dr["CrewId"].ToString());
        }
        IFRAME1.Attributes.Add("src", "WagePayableCrystal_new.aspx?Month=" + ddmonth.SelectedValue + "&Year=" + ddyear.SelectedValue + "&CrewId=" + crewid + "&MnthNm=" + ddmonth.SelectedItem.Text);

        //lblMessage.Visible = true;
        //lblMessage.Text = "";
        //int crewid = 0;
        //int vesselid = 0;
        //if (ddemp.Items.Count == 0)
        //{
        //    this.lblMessage.Text = "Select Any Employee.";
        //    return;

        //}
        //DataTable dt22 = ReportPrintCV.selectCrewIdCrewNumber(this.ddemp.SelectedItem.Text );
        //if (dt22.Rows.Count == 0)
        //{
        //    this.lblMessage.Text = "Invalid Emp#.";
        //    return;

        //}
        //foreach (DataRow dr in dt22.Rows)
        //{
        //    crewid = Convert.ToInt32(dr["CrewId"].ToString());
        //}
        //DataTable dt = MonthleWageToCrew.selectwage1(vesselid, Convert.ToInt16(ddmonth.SelectedValue), Convert.ToInt32(ddyear.SelectedItem.Text), crewid); ;
       
        //if (dt.Rows.Count > 0)
        //{
        //    CrystalReportViewer1.Visible = true;

        //    CrystalReportViewer1.ReportSource = rpt;
        //    rpt.Load(Server.MapPath("WagePayableCrystalReport.rpt"));
        //    rpt.SetDataSource(dt);

        //    DataSet ds = cls_SearchReliever.getMasterData("WageScaleComponents", "WageScaleComponentId", "ComponentName");
        //    for (int p = 0; p < 9; p++)
        //    {
        //        rpt.SetParameterValue("@P" + Convert.ToString(p + 1), ds.Tables[0].Rows[p][1].ToString());
        //    }

        //    DataTable dt1 = PrintCrewList.selectCompanyDetails();
        //    foreach (DataRow dr in dt1.Rows)
        //    {
        //        rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
        //    }
        //}
        //else
        //{
        //    lblMessage.Text = "No Record Found";
        //    CrystalReportViewer1.Visible = false;
        //}
    }
}
