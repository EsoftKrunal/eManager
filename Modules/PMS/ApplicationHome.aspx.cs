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


public partial class ApplicationHome : System.Web.UI.Page
{
    int UserId = 0;
    public int CurrentModule = 0;
    AuthenticationManager Auth;

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (Session["loginid"] == null || "" + Session["loginid"] == "")
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            int UserId = Common.CastAsInt32("" + Session["loginid"]);
            if (!IsPostBack)
            {
                if (Session["UserType"].ToString().Trim() == "S")
                {
                    tab_Header.Visible = true;
                    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("SELECT SHIPNAME FROM SETTINGS WHERE SHIPCODE='" + Session["CurrentShip"].ToString() + "'");
                    if (dtShip.Rows.Count > 0)
                    {
                        lblVessel.Text = dtShip.Rows[0][0].ToString();
                    }
                    DataTable dtUser = Common.Execute_Procedures_Select_ByQuery("SELECT UserName FROM ShipUserMaster WHERE UserId =" + UserId);
                    if (dtUser.Rows.Count > 0)
                    {
                        lblUser.Text = "Welcome " + dtUser.Rows[0][0].ToString();
                    }

                    //Li9.Visible = (UserId == 0);
                }
            }
        }
        SetButtons();
    }
    private void SetButtons()
    {
        btn_PMS.Attributes.Add("onclick", "DoPost(1)");
        btn_VIMS.Attributes.Add("onclick", "DoPost(2)");
        btn_NWC.Attributes.Add("onclick", "DoPost(3)");
        btn_Purchase.Attributes.Add("onclick", "DoPost(4)");
        btn_eReports.Attributes.Add("onclick", "DoPost(5)");
        btn_MF.Attributes.Add("onclick", "DoPost(6)");
        btn_Drills.Attributes.Add("onclick", "DoPost(7)");
        btn_Communication.Attributes.Add("onclick", "DoPost(8)");
        btn_CMS.Attributes.Add("onclick", "DoPost(10)");
        btn_StoreManagement.Attributes.Add("onclick", "DoPost(11)");
        btn_HSSQE.Attributes.Add("onclick", "DoPost(12)");
    }
    protected void btn_LogOutApp_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Default.aspx");
    }
    protected void btn_POST_Click(object sender, EventArgs e)
    {
        btn_PMS.Attributes.Remove("class");
        ((HtmlControl)btn_PMS.Parent).Attributes.Add("class", "");

        btn_VIMS.Attributes.Remove("class");
        ((HtmlControl)btn_VIMS.Parent).Attributes.Add("class", "");

        btn_NWC.Attributes.Remove("class");
        ((HtmlControl)btn_NWC.Parent).Attributes.Add("class", "");

        btn_Purchase.Attributes.Remove("class");
        ((HtmlControl)btn_Purchase.Parent).Attributes.Add("class", "");

        btn_eReports.Attributes.Remove("class");
        ((HtmlControl)btn_eReports.Parent).Attributes.Add("class", "");

        btn_MF.Attributes.Remove("class");
        ((HtmlControl)btn_MF.Parent).Attributes.Add("class", "");

        btn_Drills.Attributes.Remove("class");
        ((HtmlControl)btn_Drills.Parent).Attributes.Add("class", "");

        btn_Communication.Attributes.Remove("class");
        ((HtmlControl)btn_Communication.Parent).Attributes.Add("class", "");

        btn_CMS.Attributes.Remove("class");
        ((HtmlControl)btn_CMS.Parent).Attributes.Add("class", "");

        btn_StoreManagement.Attributes.Remove("class");
        ((HtmlControl)btn_StoreManagement.Parent).Attributes.Add("class", "");

        btn_HSSQE.Attributes.Remove("class");
        ((HtmlControl)btn_HSSQE.Parent).Attributes.Add("class", "");

        CurrentModule = int.Parse(hfd_CB.Value);
        Session["CurrentModule"] = CurrentModule;
        switch (CurrentModule)
        {
            case 1:
                btn_PMS.Attributes.Add("class", "new");
                ((HtmlControl)btn_PMS.Parent).Attributes.Add("class", "current");
                Logo.Attributes.Add("src", "Images/Application/pms_icon.png");
                frmmain.Attributes.Add("src", "ShipHome.aspx");
                break;
            case 2:
                btn_VIMS.Attributes.Add("class", "new");
                ((HtmlControl)btn_VIMS.Parent).Attributes.Add("class", "current");
                Logo.Attributes.Add("src", "Images/Application/vims_icon.png");
                frmmain.Attributes.Add("src", "VIMS/VimsInspections.aspx");
                break;
            case 3:
                btn_NWC.Attributes.Add("class", "new");
                ((HtmlControl)btn_NWC.Parent).Attributes.Add("class", "current");
                frmmain.Attributes.Add("src", "MenuPlanner/MenuCalculation.aspx?S=");
                break;
            case 4:
                btn_Purchase.Attributes.Add("class", "new");
                ((HtmlControl)btn_Purchase.Parent).Attributes.Add("class", "current");
                frmmain.Attributes.Add("src", "MenuPlanner/Provision.aspx?S=");
                break;
            case 5:
                btn_eReports.Attributes.Add("class", "new");
                ((HtmlControl)btn_eReports.Parent).Attributes.Add("class", "current");
                frmmain.Attributes.Add("src", "eReports/Home.aspx");
                break;
            case 6:
                btn_MF.Attributes.Add("class", "new");
                ((HtmlControl)btn_MF.Parent).Attributes.Add("class", "current");
                frmmain.Attributes.Add("src", "SMS/Search.aspx");
                break;
            case 7:
                btn_Drills.Attributes.Add("class", "new");
                ((HtmlControl)btn_Drills.Parent).Attributes.Add("class", "current");
                //frmmain.Attributes.Add("src", "Drill/DrillPlanner.aspx");
                frmmain.Attributes.Add("src", "eReports/Home.aspx");
                
                break;
            case 8:
                btn_Communication.Attributes.Add("class", "new");
                ((HtmlControl)btn_Communication.Parent).Attributes.Add("class", "current");
                frmmain.Attributes.Add("src", "Communication.aspx");
                break;
            case 10:
                btn_CMS.Attributes.Add("class", "new");
                ((HtmlControl)btn_CMS.Parent).Attributes.Add("class", "current");
                frmmain.Attributes.Add("src", "CMS.aspx");
                break;
            case 11:
                btn_StoreManagement.Attributes.Add("class", "new");
                ((HtmlControl)btn_StoreManagement.Parent).Attributes.Add("class", "current");
                frmmain.Attributes.Add("src", "MRV/Home.aspx");
                this.Title = "PMS-MRV";
                break;

            case 12:
                btn_HSSQE.Attributes.Add("class", "new");
                ((HtmlControl)btn_HSSQE.Parent).Attributes.Add("class", "current");
                frmmain.Attributes.Add("src", "Drill/DrillPlanner.aspx");
                //frmmain.Attributes.Add("src", "eReports/Home.aspx");
                this.Title = "PMS-MRV";
                break;
            default:
                break;

        }
    }
    //Response.Redirect("~/VimsInspections.aspx");
}
