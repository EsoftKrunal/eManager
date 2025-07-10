using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_HR_LeaveSummaryReport : System.Web.UI.Page
{
    DateTime ToDay;

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ToDay = DateTime.Today;

        if (!Page.IsPostBack)
        {
            ControlLoader.LoadControl(ddlOffice,DataName.Office,"All","0");     
            for(int i=2011;i<=ToDay.Year;i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString()));       
            }
            ddlYear.SelectedValue = ToDay.Year.ToString();
            ddlMonth.Items.Add(new ListItem("Jan", "1"));
            ddlMonth.Items.Add(new ListItem("Feb", "2"));
            ddlMonth.Items.Add(new ListItem("Mar", "3"));
            ddlMonth.Items.Add(new ListItem("Apr", "4"));
            ddlMonth.Items.Add(new ListItem("may", "5"));
            ddlMonth.Items.Add(new ListItem("Jun", "6"));
            ddlMonth.Items.Add(new ListItem("Jul", "7"));
            ddlMonth.Items.Add(new ListItem("Aug", "8"));
            ddlMonth.Items.Add(new ListItem("Sep", "9"));
            ddlMonth.Items.Add(new ListItem("Oct", "10"));
            ddlMonth.Items.Add(new ListItem("Nov", "11"));
            ddlMonth.Items.Add(new ListItem("Dec", "12"));
            ddlMonth.SelectedValue = ToDay.Month.ToString();   
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        IFRAME1.Attributes.Add("src", "Emtm_HR_LeaveSummaryReport_Crystal.aspx?Type=" + ddlReportType.SelectedValue +  "&OfficeName=" + ddlOffice.SelectedItem.Text + "&Office=" + ddlOffice.SelectedValue + "&Year=" + ddlYear.SelectedValue + "&Month=" + ddlMonth.SelectedValue + "&MonthName=" + ddlMonth.SelectedItem.Text);  
    }
}
