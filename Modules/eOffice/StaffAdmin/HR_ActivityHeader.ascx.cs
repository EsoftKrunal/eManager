using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_Emtm_HR_ActivityHeader : System.Web.UI.UserControl
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
    public void RefreshMenu()
    {
        //btntraveldoc.ImageUrl = btntraveldoc.ImageUrl.Replace("_b", "_g");

        //switch (Common.CastAsInt32(Session["CurrentModule"]))
        //{
        //    case 1:
        //        btntraveldoc.ImageUrl = btntraveldoc.ImageUrl.Replace("_g", "_b");
        //        break;
        //    case 2:
        //        btnCertificate.ImageUrl = btnCertificate.ImageUrl.Replace("_g", "_b");
        //        break;
        //    case 3:
        //        btnMedical.ImageUrl = btnMedical.ImageUrl.Replace("_g", "_b");
        //        break;
        //    default:
        //        break;
        //}
    }
    public void RedirectToPage()
    {
        //switch (Common.CastAsInt32(Session["CurrentModule"]))
        //{
        //    case 1:
        //        btntraveldoc.ImageUrl = btntraveldoc.ImageUrl.Replace("_g", "_b");
        //        Response.Redirect("Emtm_HR_TravelDocs.aspx");
        //        break;
        //    case 2:
        //        btnCertificate.ImageUrl = btnCertificate.ImageUrl.Replace("_g", "_b");
        //        Response.Redirect("Emtm_HR_CertificateDocs.aspx");
        //        break;
        //    case 3:
        //        btnMedical.ImageUrl = btnMedical.ImageUrl.Replace("_g", "_b");
        //        Response.Redirect("Emtm_HR_MedicalCertificateDocs.aspx");
        //        break;
        //    default:
        //        break;
        //}
    }
    protected void Imgbtn_Personal_Click(object sender, ImageClickEventArgs e)
    {
        Session["HrDocs"] = "hrdocs";
        Response.Redirect("HR_PersonalDetail.aspx");
    }

    protected void imgbtn_Documents_Click(object sender, ImageClickEventArgs e)
    {
        Session["HrDocs"] = "hrdocs";
        Response.Redirect("HR_TravelDocs.aspx");
    }
}
