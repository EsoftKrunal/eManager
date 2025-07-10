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

public partial class VendorMgmtDetails : System.Web.UI.Page
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
            int Mode=Common.CastAsInt32(Request.QueryString["mode"]);
            string sql = "SELECT * FROM dbo.[vw_My_Vendors] ";
            switch(Mode)
            {   
                case 1:
                    lblHeading.Text = "Proposal Stage";
                    sql += "WHERE Activity='Proposal'";
                    break;
                case 2:
                    lblHeading.Text = "First Approval Stage";
                    sql += "WHERE Activity='First Approval'";
                    break;
                case 3:
                    lblHeading.Text = "IInd Approval Stage";
                    sql += "WHERE Activity='IInd Approval'";
                    break;
                case 4:
                    lblHeading.Text = "Accounts Stage";
                    sql += "WHERE Activity='Accounts'";
                    break;
                default:
                    lblHeading.Text = "";
                    break;
            }
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql + "order by suppliername ");
            rptVendor.DataSource = dt;
            lblTotRec.Text=" ( " + dt.Rows.Count.ToString() + " ) Records Found.";
            rptVendor.DataBind();
        }

    }   
    #endregion
}

   