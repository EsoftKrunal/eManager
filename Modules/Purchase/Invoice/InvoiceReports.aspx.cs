using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Invoice_InvoiceReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            bindUser();
            bindOwnerddl();
            bindVesselNameddl();
            //txt_FDate1.Text = Common.ToDateString(DateTime.Today);
            //txt_FDate2.Text = Common.ToDateString(DateTime.Today);
        }
    }

    protected void bindUser()
    {
        string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Entry=1) AND statusId='A' Order By UserName";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        ddlEnteredBy.DataValueField = "LoginId";
        ddlEnteredBy.DataTextField = "UserName";
        ddlEnteredBy.DataSource = dt1;
        ddlEnteredBy.DataBind();
        ddlEnteredBy.Items.Insert(0, new ListItem("All", "0"));

        SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval=1) AND statusId='A' Order By UserName";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        ddlProcessedBy.DataValueField = "LoginId";
        ddlProcessedBy.DataTextField = "UserName";
        ddlProcessedBy.DataSource = dt1;
        ddlProcessedBy.DataBind();
        ddlProcessedBy.Items.Insert(0, new ListItem("All", "0"));

        SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Payment=1) AND statusId='A' Order By UserName";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        ddlPaidBy.DataValueField = "LoginId";
        ddlPaidBy.DataTextField = "UserName";
        ddlPaidBy.DataSource = dt1;
        ddlPaidBy.DataBind();
        ddlPaidBy.Items.Insert(0, new ListItem("All", "0"));
    }
    protected void bindVesselNameddl()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT shipid,shipid + ' - ' + SHIPNAME AS SHIPNAME from VW_ACTIVEVESSELS where VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY SHIPNAME");
        ddlVessel.DataSource = dt;
        ddlVessel.DataValueField = "shipid";
        ddlVessel.DataTextField = "shipname";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("< All >", ""));
    }
    protected void bindOwnerddl()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select COMPANY,COMPANY + ' - ' + [COMPANY NAME] AS 'COMPANY NAME' from [dbo].[AccountCompany] where active='Y' ORDER BY [COMPANY NAME]");
        ddlOwner.DataSource = dt;
        ddlOwner.DataValueField = "company";
        ddlOwner.DataTextField = "Company Name";
        ddlOwner.DataBind();
        ddlOwner.Items.Insert(0, new ListItem("All", ""));
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        if(ddlType.SelectedIndex==0)
            //frReport.Attributes.Add("src", "InvoiceEnteredByDate_Report.aspx?Stage=" + ddlStage.SelectedValue + "&EnteredBy=" + ddlEnteredBy.SelectedValue.Trim() + "&ProcessedBy=" + ddlProcessedBy.SelectedValue.Trim() + "&PaidBy=" + ddlPaidBy.SelectedValue + "&RecFrom=" + txtRecFrom.Text + "&RecTo=" + txtRecTo.Text + "&PeriodFrom=" + txt_FDate1.Text + "&PeriodTo=" + txt_FDate2.Text + "&Vendor=" + txtF_Vendor.Text + "&Owner=" + ddlOwner.SelectedValue + "&Vessel=" + ddlVessel.SelectedValue + "&FB=" + ddlEnteredBy.SelectedValue);
            frReport.Attributes.Add("src", "InvoiceEnteredByDate_Report.aspx?Stage=" + ddlStage.SelectedValue + "&EnteredBy=" + ddlEnteredBy.SelectedValue.Trim() + "&ProcessedBy=" + ddlProcessedBy.SelectedValue.Trim() + "&PaidBy=" + ddlPaidBy.SelectedValue + "&RecFrom=" + txtRecFrom.Text + "&RecTo=" + txtRecTo.Text + "&Vendor=" + txtF_Vendor.Text + "&Owner=" + ddlOwner.SelectedValue + "&Vessel=" + ddlVessel.SelectedValue + "&FB=" + ddlEnteredBy.SelectedValue);
        else
            //frReport.Attributes.Add("src", "InvoiceEnteredByDate_Report1.aspx?Stage=" + ddlStage.SelectedValue + "&EnteredBy=" + ddlEnteredBy.SelectedValue.Trim() + "&ProcessedBy=" + ddlProcessedBy.SelectedValue.Trim() + "&PaidBy=" + ddlPaidBy.SelectedValue + "&RecFrom=" + txtRecFrom.Text + "&RecTo=" + txtRecTo.Text + "&PeriodFrom=" + txt_FDate1.Text + "&PeriodTo=" + txt_FDate2.Text + "&Vendor=" + txtF_Vendor.Text + "&Owner=" + ddlOwner.SelectedValue + "&Vessel=" + ddlVessel.SelectedValue + "&FB=" + ddlEnteredBy.SelectedValue);
            frReport.Attributes.Add("src", "InvoiceEnteredByDate_Report1.aspx?Stage=" + ddlStage.SelectedValue + "&EnteredBy=" + ddlEnteredBy.SelectedValue.Trim() + "&ProcessedBy=" + ddlProcessedBy.SelectedValue.Trim() + "&PaidBy=" + ddlPaidBy.SelectedValue + "&RecFrom=" + txtRecFrom.Text + "&RecTo=" + txtRecTo.Text + "&Vendor=" + txtF_Vendor.Text + "&Owner=" + ddlOwner.SelectedValue + "&Vessel=" + ddlVessel.SelectedValue + "&FB=" + ddlEnteredBy.SelectedValue);
    }
    protected void ddlReportType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        frReport.Attributes.Add("src", "");
        tblRT_1.Visible = false;
        //tblRT_2.Visible = false;
        //if (ddlReportType.SelectedIndex == 1)
        //{
            bindUser();
            tblRT_1.Visible = true;
        //}
        //if (ddlReportType.SelectedIndex == 2)
        //{
        //    tblRT_2.Visible = true;
        //}
    }
    
}