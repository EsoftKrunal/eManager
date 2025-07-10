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

public partial class UserControls_HeaderMenu: System.Web.UI.UserControl
{
    AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        LinkButton li = new LinkButton();
        tdImg.Style.Add("background-Image", "url(" + li.ResolveClientUrl("~/Images/header_bg.jpg") + ")");
        tdPmsIcon.Style.Add("background-Image", "url(" + li.ResolveClientUrl("~/Images/pms_icon.jpg") + ")");

        if (Session["loginid"] == null || "" + Session["loginid"] == "")
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            int UserId = Common.CastAsInt32("" + Session["loginid"]);
            if (!IsPostBack)
            {
               /* RefreshMenu()*/;
                if (Session["UserType"].ToString().Trim() == "S")
                {
                    //tr_MF.Visible = true;
                    tab_Header.Visible = true;
                    tr_StoreManagement.Visible = true;
                    //btnLogOut.Visible = true;
                    tr_AdminDelete.Visible = false;
                    //btnplanning.Visible = true;
                    btnHome.PostBackUrl = "~/ShipHome.aspx";
                    DataTable dtShip=Common.Execute_Procedures_Select_ByQuery("SELECT SHIPNAME FROM SETTINGS WHERE SHIPCODE='" + Session["CurrentShip"].ToString() + "'");
                    if (dtShip.Rows.Count > 0)
                    {
                        lblVessel.Text = dtShip.Rows[0][0].ToString();    
                    }
                    DataTable dtUser = Common.Execute_Procedures_Select_ByQuery("SELECT UserName FROM ShipUserMaster WHERE UserId =" + UserId);
                    if (dtUser.Rows.Count > 0)
                    {
                        lblUser.Text = "Welcome " + dtUser.Rows[0][0].ToString();
                    }

                    //tr_MenuPlanner.Visible = true;
                    //tr_Purchase.Visible = true;
                    btnSettings.Visible = true;
                    //btn_MRV.Visible = true;
                }
                else
                {
                    btnSettings.Visible = false;
                    //tr_MF.Visible = false;
                    //tr_VIMS.Visible = false;
                    tab_Header.Visible = false;
                    tr_StoreManagement.Visible = false;
                    //btnLogOut.Visible = false;
                    //tr_AdminDelete.Visible = true;
                    //tr_MenuPlanner.Visible = false;
                    //tr_Purchase.Visible = false;
                    btnHome.PostBackUrl = "~/Office_Home.aspx";
                    //btn_MRV.Visible = false;
                }
            }
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
    public void RefreshMenu()
    {
        btnplanning.ImageUrl = btnplanning.ImageUrl.Replace("_b", "_g");
        btnshipmaster.ImageUrl = btnshipmaster.ImageUrl.Replace("_b", "_g");
        btnRegisters.ImageUrl = btnRegisters.ImageUrl.Replace("_b","_g");
        btnofficemaster.ImageUrl = btnofficemaster.ImageUrl.Replace("_b", "_g");
        btnvesselsetup.ImageUrl = btnvesselsetup.ImageUrl.Replace("_b", "_g");        
        btnreports.ImageUrl = btnreports.ImageUrl.Replace("_b", "_g");
        btnDefectReport.ImageUrl = btnDefectReport.ImageUrl.Replace("_b", "_g");
        btnAdmindelete.ImageUrl = btnAdmindelete.ImageUrl.Replace("_b", "_g");
        btnDryDock.ImageUrl = btnDryDock.ImageUrl.Replace("_b", "_g");
        btnRiskMangement.ImageUrl = btnRiskMangement.ImageUrl.Replace("_b", "_g");
        btnSpareManagement.ImageUrl = btnSpareManagement.ImageUrl.Replace("_b", "_g");
        //btn_MRV.ImageUrl = btn_MRV.ImageUrl.Replace("_b", "_g");
        
        //btnMF.ImageUrl = btnMF.ImageUrl.Replace("_b", "_g");
        //btnVIMS.ImageUrl = btnVIMS.ImageUrl.Replace("_b", "_g");
        //btnMenuPlanner.ImageUrl = btnMenuPlanner.ImageUrl.Replace("_b", "_g");
        //btnPurchase.ImageUrl = btnPurchase.ImageUrl.Replace("_b", "_g");

        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btnplanning.ImageUrl = btnplanning.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btnshipmaster.ImageUrl = btnshipmaster.ImageUrl.Replace("_g", "_b");
                break;
            case 3:
                btnRegisters.ImageUrl = btnRegisters.ImageUrl.Replace("_g","_b");
                break;
            case 4:
                btnofficemaster.ImageUrl = btnofficemaster.ImageUrl.Replace("_g", "_b");
                break;
            case 5:
                btnvesselsetup.ImageUrl = btnvesselsetup.ImageUrl.Replace("_g", "_b");
                break;
            case 6:
                btnreports.ImageUrl = btnreports.ImageUrl.Replace("_g", "_b");
                break;
            case 7:
                btnrunninghr.ImageUrl = btnrunninghr.ImageUrl.Replace("_g", "_b");
                break;
            case 8:
                btnDefectReport.ImageUrl = btnDefectReport.ImageUrl.Replace("_g", "_b");
                break;
            case 9:
                btnAdmindelete.ImageUrl = btnAdmindelete.ImageUrl.Replace("_g", "_b");
                break;
            //case 10:
            //    btnMF.ImageUrl = btnMF.ImageUrl.Replace("_g", "_b");
            //    break;
            //case 11:
            //    btnVIMS.ImageUrl = btnVIMS.ImageUrl.Replace("_g", "_b");
            //    break;
            //case 12:
            //    btnMenuPlanner.ImageUrl = btnMenuPlanner.ImageUrl.Replace("_g", "_b");
            //    break;
            //case 13:
            //    btnPurchase.ImageUrl = btnPurchase.ImageUrl.Replace("_g", "_b");
            //    break;
            case 14:
                btnDryDock.ImageUrl = btnDryDock.ImageUrl.Replace("_g", "_b");
                break;
            case 15:
                btnRiskMangement.ImageUrl = btnRiskMangement.ImageUrl.Replace("_g", "_b");
                break;
            case 16:
                btnSpareManagement.ImageUrl = btnSpareManagement.ImageUrl.Replace("_g", "_b");
                break;
            case 17:
                btnStoreManagement.ImageUrl = btnStoreManagement.ImageUrl.Replace("_g", "_b");
                break;
            //case 18:
            //    btn_MRV.ImageUrl = btn_MRV.ImageUrl.Replace("_g", "_b");
            //    break;

            default:
                break; 
        }
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(37, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Module);
            btnplanning.Visible = true && Auth.IsView;

            Auth = new AuthenticationManager(39, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Module);
            tr_btnshipmaster.Visible = true && Auth.IsView;

            Auth = new AuthenticationManager(35, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Module);
            tr_btnRegisters.Visible = true && Auth.IsView;

            Auth = new AuthenticationManager(32, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Module);
            tr_btnofficemaster.Visible = true && Auth.IsView;

            Auth = new AuthenticationManager(33, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Module);
            tr_btnvesselsetup.Visible = true && Auth.IsView;

            Auth = new AuthenticationManager(34, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Module);
            tr_btnreports.Visible = true && Auth.IsView;

            Auth = new AuthenticationManager(36, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Module);
            tr_AdminDelete.Visible = true && Auth.IsView;

            Auth = new AuthenticationManager(38, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Module);
            tr_shiprunninghr.Visible = true && Auth.IsView;

            Auth = new AuthenticationManager(40, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Module);
            tr_defectreport.Visible = true && Auth.IsView;

            tr_DryDock.Visible = true;

            tr_RiskMangement.Visible = false;

            ////////////////////////////
            //btnplanning.Visible = true;

            //tr_btnshipmaster.Visible = true;
            //tr_btnRegisters.Visible = true;
            //tr_btnofficemaster.Visible = true;
            //tr_btnvesselsetup.Visible = true;
            //tr_btnreports.Visible = true;
            //tr_AdminDelete.Visible = true;

            //btnplanning.Visible = true;
            
            //btnshipmaster.Visible = true;
            //btnRegisters.Visible = true;
            //btnofficemaster.Visible = true;
            //btnvesselsetup.Visible = true;
            //btnreports.Visible = true;
            //btnAdmindelete.Visible = true;
        }
        else
        {
            btnplanning.Visible = true;
            btnshipmaster.Visible = true;

            btnRegisters.Visible = false;
            btnofficemaster.Visible = false;
            btnvesselsetup.Visible = false;
            btnreports.Visible = true;
            btnAdmindelete.Visible = false;

            btnplanning.Visible = true;
            tr_btnshipmaster.Visible = true;

            tr_btnRegisters.Visible = false;
            tr_btnofficemaster.Visible = false;
            tr_btnvesselsetup.Visible = false;
            tr_btnreports.Visible = true;
            tr_AdminDelete.Visible = false;
            tr_DryDock.Visible = false;
            tr_RiskMangement.Visible = true;
            //tr_MRV.Visible = true;
        }
    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btnplanning.ImageUrl = btnplanning.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/JobPlanning.aspx");
                break;
            case 2:
                btnshipmaster.ImageUrl = btnshipmaster.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/ShipMaster.aspx");
                break;
            case 3:
                btnRegisters.ImageUrl = btnRegisters.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/Registers.aspx");
                break;
            case 4:
                btnofficemaster.ImageUrl = btnofficemaster.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/Components.aspx");
                break;
            case 5:
                btnvesselsetup.ImageUrl = btnvesselsetup.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/VesselSelection.aspx");
                break;
            case 6:
                btnreports.ImageUrl = btnreports.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/ReportViewer.aspx");
                break;
            case 7:
                btnrunninghr.ImageUrl = btnrunninghr.ImageUrl.Replace("_g", "_b");
                if (Session["UserType"].ToString().Trim() == "S")
                {
                    Response.Redirect("~/Ship_RunningHour.aspx");
                }
                else
                {
                    Response.Redirect("~/Office_RunningHour.aspx");
                }
                break;
            case 8:
                btnDefectReport.ImageUrl = btnDefectReport.ImageUrl.Replace("_g", "_b");
                if (Session["UserType"].ToString().Trim() == "S")
                {
                    Response.Redirect("~/DefectReport.aspx");
                }
                else
                {
                    Response.Redirect("~/DefectReport_Office.aspx");
                }
                break;
            case 9:
                btnAdmindelete.ImageUrl = btnAdmindelete.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/Office_Admin_Delete.aspx");
                break;
            //case 10:
            //    btnMF.ImageUrl = btnMF.ImageUrl.Replace("_g", "_b");
            //    Response.Redirect("~/SMS/Search.aspx");
            //    break;
            //case 11:
            //    btnVIMS.ImageUrl = btnVIMS.ImageUrl.Replace("_g", "_b");
            //    Response.Redirect("~/VimsInspections.aspx");
            //    break;
            //case 12:
            //    btnMenuPlanner.ImageUrl = btnMenuPlanner.ImageUrl.Replace("_g", "_b");
            //    Response.Redirect("~/MenuPlanner/MenuCalculation.aspx?S=");
            //    break;
            //case 13:
            //    btnPurchase.ImageUrl = btnPurchase.ImageUrl.Replace("_g", "_b");
            //    Response.Redirect("~/MenuPlanner/Provision.aspx?S=");
            //    break;
            case 14:
                btnDryDock.ImageUrl = btnDryDock.ImageUrl.Replace("_g", "_b");
                if (Session["usertype"].ToString().Trim() == "s")
                {
                    Response.Redirect("~/drydock/dd_vsl_home.aspx");
                }
                else
                {
                    Response.Redirect("~/drydock/DD_OFC_Tracker.aspx");
                }
                break;
            case 15:
                btnRiskMangement.ImageUrl = btnRiskMangement.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/RiskManagement/RiskAnalysis.aspx");
                break;
            case 16:
                btnSpareManagement.ImageUrl = btnSpareManagement.ImageUrl.Replace("_g", "_b");
                if (Session["usertype"].ToString().Trim() == "S")
                {
                    Response.Redirect("~/VSL_SpareManagement.aspx");
                }
                else
                {
                    Response.Redirect("~/SpareManagement.aspx");
                }
                break;
            case 17:
                btnStoreManagement.ImageUrl = btnStoreManagement.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/StoreManagement.aspx");
                break;
            case 18:
                btnRiskMangement.ImageUrl = btnRiskMangement.ImageUrl.Replace("_g", "_b");
                Response.Redirect("~/MRV/Home.aspx");
                break;
            default:
                break; 
        }
    }
    protected void btnLogOut_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Default.aspx"); 
    }
    protected void btnSettings_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShipSettings.aspx"); 
    }
}
