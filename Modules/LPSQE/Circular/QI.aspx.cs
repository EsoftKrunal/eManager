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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;   


public partial class CircularForApproval : System.Web.UI.Page
{
    public Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1081);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 6);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }

        if (!Page.IsPostBack)
        {
            btnCircularNew_OnClick(sender, e);
        }
    }

    // Events ----------------------------------------------------------------------------------------------------------------------------
    protected void btnCircular_OnClick(object sneder, EventArgs e)
    {
        SetSelBtn(2);
        Ifram1.Attributes.Add("src", "CircularForApproval.aspx");
    }
    protected void btnCircularNew_OnClick(object sneder, EventArgs e)
    {
        SetSelBtn(5);
        Ifram1.Attributes.Add("src", "CircularNew.aspx");
    }


    protected void btnSafetyAlert_OnClick(object sneder, EventArgs e)
    {
        SetSelBtn(3);
        Ifram1.Attributes.Add("src", "SafetyAlert.aspx");
    }
    protected void btnRegulation_OnClick(object sneder, EventArgs e)
    {
        SetSelBtn(4);
        Ifram1.Attributes.Add("src", "Regulation.aspx");
    }


    public void SetSelBtn(int No)
    {

        btnCircular.CssClass = "btn1";
        btnSafetyAlert.CssClass = "btn1";
        btnRegulation.CssClass = "btn1";
        btnCircularNew.CssClass = "btn1";
        switch (No)
        {
            case 2:
                btnCircular.CssClass = "selbtn";
                break;
            case 3:
                btnSafetyAlert.CssClass = "selbtn";
                break;
            case 4:
                btnRegulation.CssClass = "selbtn";
                break;
            case 5:
                btnCircularNew.CssClass = "selbtn";
                break;
            default:
                break;
        }

    }

}
