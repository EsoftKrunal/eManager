using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StaffAdmin_Compensation_CB_Menu : System.Web.UI.UserControl
{
public AuthenticationManager auth; 
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

int UserID = Common.CastAsInt32(Session["loginid"]);
auth = new AuthenticationManager(336, UserID, ObjectType.Page);
            if (!(auth.IsView))
                    {
                        Response.Redirect("NoPermission.aspx");
                    }         


    }
    protected void menu_Click(object sender, EventArgs e)
    {
        Session["CurrentModule"] = ((Button)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {
        btnHome.CssClass = "selbtn";
        btnRegister.CssClass = "btn1";
        btnPaySlip.CssClass = "btn1";
        btnRevision.CssClass = "btn1";
        //btnDocument.CssClass = "tabmenu";

        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btnHome.CssClass = "selbtn";     
                           
                break;
            case 2:
                btnRegister.CssClass = "selbtn";
                btnHome.CssClass = "btn1";
                break;
            case 3:
                btnPaySlip.CssClass = "selbtn";
                btnHome.CssClass = "btn1";
                break;
            case 4:
                btnRevision.CssClass = "selbtn";
                btnHome.CssClass = "btn1";
                break;
            //case 5:
            //    btnDocument.CssClass = "tabmenu active";
            //    btnHome.CssClass = "tabmenu";
            //    break;
            default:
                break;
        }
    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                //btnHome.CssClass = "tabmenu active";
                //btnRegister.CssClass = "tabmenu";                
                Response.Redirect("CompensationBenifits.aspx");
                break;
            case 2:
                //btnRegister.CssClass = "tabmenu active";
                //btnHome.CssClass = "tabmenu";
                Response.Redirect("Register.aspx");
                break;
            case 3:
                //btnRegister.CssClass = "tabmenu active";
                //btnHome.CssClass = "tabmenu";
                Response.Redirect("PaySlip.aspx");
                break;
            case 4:
                //btnRevision.CssClass = "tabmenu active";
                //btnHome.CssClass = "tabmenu";
                Response.Redirect("Revision.aspx");
                break;
            //case 5:
            //    //btnRevision.CssClass = "tabmenu active";
            //    //btnHome.CssClass = "tabmenu";
            //    Response.Redirect("Documents.aspx");
            //    break;
            default:
                break;
        }
    }
}
