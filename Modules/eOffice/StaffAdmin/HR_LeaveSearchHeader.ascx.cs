using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_LeaveSearchHeader : System.Web.UI.UserControl
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
        Session["CurrentPage"] = ((ImageButton)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }


    public void RefreshMenu()
    {
        btnLeaveSearch.ImageUrl = btnLeaveSearch.ImageUrl.Replace("_b", "_g");
        btnHolidayMaster.ImageUrl = btnHolidayMaster.ImageUrl.Replace("_b", "_g");
        btnWeeklyOff.ImageUrl = btnWeeklyOff.ImageUrl.Replace("_b", "_g");
        btnLeaveRequest.ImageUrl = btnLeaveRequest.ImageUrl.Replace("_b", "_g");
        btnComapnyEvent.ImageUrl = btnComapnyEvent.ImageUrl.Replace("_b", "_g");
        btnPosition.ImageUrl = btnPosition.ImageUrl.Replace("_b", "_g");
        switch (Common.CastAsInt32(Session["CurrentPage"]))
        {
            case 1:
                btnLeaveSearch.ImageUrl = btnLeaveSearch.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btnHolidayMaster.ImageUrl = btnHolidayMaster.ImageUrl.Replace("_g", "_b");
                break;
            case 3:
                btnWeeklyOff.ImageUrl = btnWeeklyOff.ImageUrl.Replace("_g", "_b");
                break;
            case 4:
                btnLeaveRequest.ImageUrl = btnLeaveRequest.ImageUrl.Replace("_g", "_b");
                break;
            case 5:
                btnLeaveRegister.ImageUrl = btnLeaveRegister.ImageUrl.Replace("_g", "_b");
                break;
            case 6:
                btnperiodClosure.ImageUrl = btnperiodClosure.ImageUrl.Replace("_g", "_b");
                break;
            case 7:
                btnAbsencePurpose.ImageUrl = btnAbsencePurpose.ImageUrl.Replace("_g", "_b");
                break;
            case 8:
                btnComapnyEvent.ImageUrl = btnComapnyEvent.ImageUrl.Replace("_g", "_b");
                break;
            case 9:
                btnPosition.ImageUrl = btnPosition.ImageUrl.Replace("_g", "_b");
                break;
            default:
                break;
        }
    }


    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentPage"]))
        {
            case 1:
                btnLeaveSearch.ImageUrl = btnLeaveSearch.ImageUrl.Replace("_g", "_b");
                Response.Redirect("LeaveSearch.aspx");
                break;
            case 2:
                btnHolidayMaster.ImageUrl = btnHolidayMaster.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_HolidayMaster.aspx");
                break;
            case 3:
                btnWeeklyOff.ImageUrl = btnWeeklyOff.ImageUrl.Replace("_g", "_b");
                Response.Redirect("OfficeWeeklyoffMaster.aspx");
                break;
            case 4:
                btnLeaveRequest.ImageUrl = btnLeaveRequest.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_LeaveRequest.aspx");
                break;
            case 5:
                btnLeaveRegister.ImageUrl = btnLeaveRegister.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Hr_LeaveRegister.aspx");
                break;
            case 6:
                btnperiodClosure.ImageUrl = btnLeaveRegister.ImageUrl.Replace("_g", "_b");
                Response.Redirect("PeriodClosure.aspx");
                break;
            case 7:
                btnAbsencePurpose.ImageUrl = btnAbsencePurpose.ImageUrl.Replace("_g", "_b");
                Response.Redirect("OA_PurposeMaster.aspx");
                break;
            case 8:
                btnComapnyEvent.ImageUrl = btnComapnyEvent.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_ComapanyEvents.aspx");
                break;
            case 9:
                btnPosition.ImageUrl = btnPosition.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_PositionMaster.aspx");
                break;
            default:
                break;
        }
    }
}
