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
     
//using Microsoft.Office.Interop.Outlook;    
/// <summary>
/// Page Name            : ReqFromVessels.aspx
/// Purpose              : Listing Of Files Received From Vessel
/// Author               : Shobhita Singh
/// Developed on         : 15 September 2010
/// </summary>

public partial class FindVendor : System.Web.UI.Page
{
    #region ---------- PageLoad ------------    
    AuthenticationManager auth;
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
                Response.Redirect("~/NoPermission.aspx", false);
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------         
        if (!Page.IsPostBack)
        {
            DataTable dtVendorList = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblVendorBusinessesList ORDER BY Vendorlistname");
            ddlServices.DataSource = dtVendorList;
            ddlServices.DataTextField = "Vendorlistname";
            ddlServices.DataValueField = "Vendorlistid";
            ddlServices.DataBind();
            ddlServices.Items.Insert(0, new ListItem("", ""));

            DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.Country ORDER BY Countryname");
            ddlCountry.DataSource = dtCountry;
            ddlCountry.DataTextField = "Countryname";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("", ""));
            //--------------------------
            BindVendor(sender,e);
        }

    }
    protected void BindVendor(object sender,EventArgs e)
    {
        string whereclause = "";
        
        if(txtVendor.Text.Trim() !="")
            whereclause += " AND SupplierName like '" + txtVendor.Text + "%' ";

        if (txtEmail.Text.Trim() != "")
            whereclause += " AND SupplierEmail like '" + txtEmail.Text + "%' ";

        if (txtTravCode.Text.Trim() != "")
            whereclause += " AND TravID ='" + txtTravCode.Text + "' ";

        if (ddlActive.SelectedIndex > 0)
            whereclause += " AND Active=" + ddlActive.SelectedValue;

        if (ddlApprovalType.SelectedIndex > 0)
            whereclause += " AND SecondApporvalType=" + ddlApprovalType.SelectedValue;

        if (ddlCountry.SelectedIndex > 0)
            whereclause += " AND Coyntry=" + ddlApprovalType.SelectedValue;

        if (ddlServices.SelectedIndex > 0)
            whereclause += " AND COMPANYBUSINESSES+',' LIKE '%" + ddlServices.SelectedValue + ",%' ";

        if (ddlValidity.SelectedIndex > 0)
        {
            switch(Common.CastAsInt32( ddlValidity.SelectedValue))
            {
                case 1:
                    whereclause += " AND ValidityDate <='" + DateTime.Today.AddDays(7).ToString("dd-MMM-yyyy") + "' ";
                    break;
                case 2:
                    whereclause += " AND ValidityDate <='" + DateTime.Today.AddMonths(1).ToString("dd-MMM-yyyy") + "' ";
                    break;
                case 3:
                    whereclause += " AND ValidityDate <='" + DateTime.Today.AddMonths(6).ToString("dd-MMM-yyyy") + "' ";
                    break;
                case 4:
                    whereclause += " AND ValidityDate <='" + DateTime.Today.AddYears(1).ToString("dd-MMM-yyyy") + "' ";
                    break;
                case 5:
                    whereclause += " AND ValidityDate >'" + DateTime.Today.AddYears(1).ToString("dd-MMM-yyyy") + "' ";
                    break;
            }
        }


        string sql = "SELECT  Row_Number() over(order by SupplierName) as srno,* FROM DBO.VW_ALL_VENDERS WHERE 1=1 " + whereclause + " order by SupplierName";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptVendor.DataSource = dt;
        lblTotRec.Text = " ( " + dt.Rows.Count.ToString() + " ) Records Found.";
        rptVendor.DataBind();
    }
    #endregion
}

   