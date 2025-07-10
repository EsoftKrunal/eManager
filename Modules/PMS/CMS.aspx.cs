using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Xml;
using Ionic.Zip;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public partial class CMS : System.Web.UI.Page
{
    public int SelectedTab
    {
        get { return Common.CastAsInt32(ViewState["SelectedTab"]); }
        set { ViewState["SelectedTab"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelectedTab = 1;
            RefreshMenu();
            RedirectToPage();
            string UserName = Session["UserName"].ToString();
            if (UserName == "MASTER")
            {
                btnRestAdmin.Visible = true;
            }
            else
            {
                btnRestAdmin.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                frm1.Attributes.Add("src", "Vims/CMSCrewList.aspx");
            }
        }
    }
   
    //------- Menu Section --------------
    protected void menu_Click(object sender, EventArgs e)
    {
        SelectedTab = Common.CastAsInt32(((Button)sender).CommandArgument);
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {
        btnCrewList.CssClass = "btnNormal";
        btnRestAdmin.CssClass = "btnNormal";
        btnRestHour.CssClass = "btnNormal";
        btnTraining.CssClass = "btnNormal";

        btnPeap.CssClass = "btnNormal";
        btnMedicalReport.CssClass = "btnNormal";
        switch (SelectedTab)
        {
            case 1:
                btnCrewList.CssClass = "btnSelected";
                break;
            case 2:
                btnRestAdmin.CssClass = "btnSelected";
                break;
            case 3:
                btnRestHour.CssClass = "btnSelected";
                break;

            case 4:
                btnPeap.CssClass = "btnSelected";
                break;
            case 5:
                btnMedicalReport.CssClass = "btnSelected";
                break;
            case 6:
                btnTraining.CssClass = "btnSelected";
                break;

            default:
                break;
        }

    }
    public void RedirectToPage()
    {
        switch (SelectedTab)
        {
            case 1:
                btnCrewList.CssClass = "btnSelected";
                frm1.Attributes.Add("src","Vims/CMSCrewList.aspx");
                break;
            case 2:
                btnRestAdmin.CssClass = "btnSelected";
                frm1.Attributes.Add("src", "Vims/CMSRestHourDateLine.aspx");
                break;
            case 3:
                btnRestHour.CssClass = "btnSelected";
                frm1.Attributes.Add("src", "Vims/CMSCrewListRestHour.aspx");
                break;

            case 4:
                btnPeap.CssClass = "btnSelected";
                frm1.Attributes.Add("src", "eReports/G113/G113_List.aspx");
                break;
            case 5:
                btnMedicalReport.CssClass = "btnSelected";
                frm1.Attributes.Add("src", "eReports/D110/D110_List.aspx");
                break;
            case 6:
                btnTraining.CssClass = "btnSelected";
                frm1.Attributes.Add("src", "CMSCrewTraining.aspx");
                break;
            default:
                break;
        }
    }
    //------- Backup Section --------------    
    
}