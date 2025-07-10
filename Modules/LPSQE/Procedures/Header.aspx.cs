using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class SMS_Header : System.Web.UI.Page
{
    AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] == null || "" + Session["loginid"] == "")
        {
            //Response.Redirect("~/Default.aspx");
        }
        else
        {
            int UserId = Common.CastAsInt32("" + Session["loginid"]);
            if (!IsPostBack)
            {
                RefreshMenu();
                if (Session["UserType"].ToString().Trim() == "S")
                {
                    tab_Header.Visible = true;
                    btnLogOut.Visible = true;
                    tr_AdminDelete.Visible = false;
                    //btnplanning.Visible = true;
                    btnHome.PostBackUrl = "~/ShipHome.aspx";
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
                }
                else
                {
                    tab_Header.Visible = false;
                    btnLogOut.Visible = false;
                    //tr_AdminDelete.Visible = true;
                    btnHome.PostBackUrl = "~/Office_Home.aspx";
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
        btnRegisters.ImageUrl = btnRegisters.ImageUrl.Replace("_b", "_g");
        btnofficemaster.ImageUrl = btnofficemaster.ImageUrl.Replace("_b", "_g");
        btnvesselsetup.ImageUrl = btnvesselsetup.ImageUrl.Replace("_b", "_g");
        btnreports.ImageUrl = btnreports.ImageUrl.Replace("_b", "_g");
        btnDefectReport.ImageUrl = btnDefectReport.ImageUrl.Replace("_b", "_g");
        btnAdmindelete.ImageUrl = btnAdmindelete.ImageUrl.Replace("_b", "_g");

        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btnplanning.ImageUrl = btnplanning.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btnshipmaster.ImageUrl = btnshipmaster.ImageUrl.Replace("_g", "_b");
                break;
            case 3:
                btnRegisters.ImageUrl = btnRegisters.ImageUrl.Replace("_g", "_b");
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
        }
    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/JobPlanning.aspx") + "';", true);
                break;
            case 2:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/ShipMaster.aspx") + "';", true);
                break;
            case 3:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/jobTypeMaster.aspx") + "';", true);
                break;
            case 4:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/Components.aspx") + "';", true);
                break;
            case 5:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/VesselSelection.aspx") + "';", true);
                break;
            case 6:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/ReportViewer.aspx") + "';", true);
                break;
            case 7:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/Default.aspx") + "';", true);
                if (Session["UserType"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/Ship_RunningHour.aspx") + "';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/Office_RunningHour.aspx") + "';", true);
                }
                break;
            case 8:
                if (Session["UserType"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/DefectReport.aspx") + "';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/DefectReport_Office.aspx") + "';", true);
                }
                break;
            case 9:
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/Office_Admin_Delete.aspx") + "';", true);
                break;
            default:
                break;
        }
    }
    protected void btnLogOut_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "top.location='" + (new LinkButton()).ResolveClientUrl("~/Default.aspx") + "';", true);
    }
}
