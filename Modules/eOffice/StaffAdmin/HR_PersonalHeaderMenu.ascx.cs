using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StaffAdmin_HR_PersonalHeaderMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginid"] == null || "" + Session["loginid"] == "")
        {
        }
        int UserId = Common.CastAsInt32("" + Session["loginid"]);
        if (!IsPostBack)
        {
            RefreshMenu();
        }
    }
    protected void Unnamed1_MenuItemClick(object sender, MenuEventArgs e)
    {
        e.Item.ImageUrl = e.Item.ImageUrl.Replace("_g", "_b");  
    }
    protected void menu_Click(object sender, ImageClickEventArgs e)
    {
        Session["CurrentModule"] = ((ImageButton)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    protected void menu_Click1(object sender, EventArgs e)
    {
        Session["CurrentModule"] = ((Button)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {
        btngeneral.ImageUrl = btngeneral.ImageUrl.Replace("_b", "_g");
        btncontact.ImageUrl = btncontact.ImageUrl.Replace("_b", "_g");
        btnfamily.ImageUrl = btnfamily.ImageUrl.Replace("_b", "_g");
        btnexperience.ImageUrl = btnexperience.ImageUrl.Replace("_b", "_g");
        btnTrainingRecord.Style.Remove("background-color");
        btnTrainingRecord.Style.Remove("color");
        btnFeedBack.Style.Remove("background-color");
        btnFeedBack.Style.Remove("color");
        
        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btngeneral.ImageUrl = btngeneral.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btncontact.ImageUrl = btncontact.ImageUrl.Replace("_g", "_b");
                break;
            case 3:
                btnfamily.ImageUrl = btnfamily.ImageUrl.Replace("_g","_b");
                break;
            case 4:
                btnexperience.ImageUrl = btnexperience.ImageUrl.Replace("_g", "_b");
                break;
            case 5:
                btnTrainingRecord.Style.Add("background-color", "#4371a5");
                btnTrainingRecord.Style.Add("color", "white");
                break;
            case 6:
                btnFeedBack.Style.Add("background-color", "#4371a5");
                btnFeedBack.Style.Add("color", "white");
                break;
            default:
                break;
        }
    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btngeneral.ImageUrl = btngeneral.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_PersonalDetail.aspx");
                break;
            case 2:
                btncontact.ImageUrl = btncontact.ImageUrl.Replace("_g", "_b");
                Session["HrContact"] = "HR";
                Response.Redirect("HR_Contacts.aspx");
                break;
            case 3:
                btnfamily.ImageUrl = btnfamily.ImageUrl.Replace("_g", "_b");
                Session["HrFamily"] = "HrFamily"; 
                Response.Redirect("HR_Familydetails.aspx");
                break;
            case 4:
                btnexperience.ImageUrl = btnexperience.ImageUrl.Replace("_g", "_b");
                Session["Experience"] = "HrExperience";
                Response.Redirect("HR_Experiance.aspx?exp=bbb");
                break;
            case 5:
                btnTrainingRecord.Style.Add("background-color", "#4371a5");
                btnTrainingRecord.Style.Add("color", "white");
                Session["TrainingRecord"] = "HrTrainingRecord";
                Response.Redirect("HR_TrainingRecords.aspx");
                break;
            case 6:
                btnFeedBack.Style.Add("background-color", "#4371a5");
                btnFeedBack.Style.Add("color", "white");
                Session["FeedBack"] = "HrFeedBack";
                Response.Redirect("Hr_EmployeeFeedBack.aspx");
                break;
            default:
                break;
        }
    }
    protected void imgbtn_Documents_Click(object sender, ImageClickEventArgs e)
    {
        Session["HrDocs"] = "hrdocs";
        Response.Redirect("HR_TravelDocs.aspx");
    }
    protected void imgbtn_Search_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtn_Activity_Click(object sender, ImageClickEventArgs e)
    {
        Session["HrActivity"] = "Activity";
        Response.Redirect("HR_Activity.aspx");
    }
}

