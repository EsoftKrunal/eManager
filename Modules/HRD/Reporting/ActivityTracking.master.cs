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

public partial class Reporting_ActivityTracking : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 97);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************

        if (Session["PageCode"] == "1")
        {
            RadioButtonList1.SelectedValue = "1";
        }
        if (Session["PageCode"] == "2")
        {
            RadioButtonList1.SelectedValue = "2";
        }
        if (Session["PageCode"] == "3")
        {
            RadioButtonList1.SelectedValue = "3";
        }
        if (Session["PageCode"] == "4")
        {
            RadioButtonList1.SelectedValue = "4";
        }
        if (Session["PageCode"] == "5")
        {
            RadioButtonList1.SelectedValue = "5";
        }
        if (Session["PageCode"] == "6")
        {
            RadioButtonList1.SelectedValue = "6";
        }
        if (Session["PageCode"] == "7")
        {
            RadioButtonList1.SelectedValue = "7";
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "1")
        {
            Response.Redirect("PortCallReport.aspx");
        }
        if (RadioButtonList1.SelectedValue == "2")
        {
            Response.Redirect("TravelAgentHeaderReport.aspx");
        }
        if (RadioButtonList1.SelectedValue == "3")
        {
            Response.Redirect("PortAgentBookingReport.aspx");
        }
        if (RadioButtonList1.SelectedValue == "4")
        {
            Response.Redirect("ContractDetails.aspx");
        }
        if (RadioButtonList1.SelectedValue == "5")
        {
            Response.Redirect("SignOnOffActivityTracking.aspx");
        }
        if (RadioButtonList1.SelectedValue == "6")
        {
            Response.Redirect("PromotioDetails.aspx");
        }
        if (RadioButtonList1.SelectedValue == "7")
        {
            Response.Redirect("NTBRDetails.aspx");
        }
    }
}
