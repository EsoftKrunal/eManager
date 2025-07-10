using System;
using System.Collections;
using System.Collections.Specialized; 
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;     
  
/// <summary>
/// Page Name            : VendorRequestManagement.aspx
/// Purpose              :tabbing for vendor management
/// Author               :Laxmi Verma
/// Developed on         : 23 September 2015
/// </summary>

public partial class Vendor : System.Web.UI.Page
{     
    AuthenticationManager auth;
    #region ---------- PageLoad ------------    
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            auth = new AuthenticationManager(1065, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {
                Response.Redirect("~/NoPermission.aspx",false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------
    }   
    #endregion
    
    
    // Events ----------------------------------------------------------
    //added by laxmi on 23-sep-2015
   
    //for menu click
    protected void menu_Click(object sender, EventArgs e)
    {
        Session["CurrentHeaderModule"] = ((LinkButton)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    public void RefreshMenu()
    {
        //btnRequestManagement.CssClass = "btnNormal";
        //btnVendorRequest.CssClass = "btnNormal";
        //btnHome.CssClass = "btnNormal";       

        switch (Common.CastAsInt32(Session["CurrentHeaderModule"]))
        {
            //case 1:
            //    btnRequestManagement.CssClass = "btnSelected";
            //    break;
            case 2:
               // btnVendorRequest.CssClass = "btnSelected";
                break;
            case 3:
               // btnHome.CssClass = "btnSelected";
                break;      
            default:
                break;
        }

    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentHeaderModule"]))
        {
            //case 1:
            //    btnRequestManagement.CssClass = "btnSelected";
            //    iframe1.Attributes.Add("src", "VendorRequest.aspx");
            //    break;
            case 2:
               // btnVendorRequest.CssClass = "btnSelected";
                iframe1.Attributes.Add("src", "Vendor.aspx");
                break;
            case 3:
               // btnHome.CssClass = "btnSelected";
                iframe1.Attributes.Add("src", "VendorMgmtHome.aspx");
                break;        
            default:
                break;
        }
    }
    //end here   
}

   