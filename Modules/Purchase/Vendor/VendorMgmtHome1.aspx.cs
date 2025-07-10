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

public partial class VendorMgmtHome : System.Web.UI.Page
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
            auth = new AuthenticationManager(209, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
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
            ddlType_OnSelectedIndexChanged(sender, e);
            string sql = "SELECT Activity,COUNT(*) as NUMREC FROM dbo.[vw_My_Vendors] GROUP BY Activity";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            DataView dv=dt.DefaultView;
            DataTable dt1;

            //dv.RowFilter = "Activity='New Request'";
            //dt1 = dv.ToTable();
            //if (dt1.Rows.Count > 0)
            //    lblNewRequest.Text = dt1.Rows[0][1].ToString();

            dv.RowFilter = "Activity='Proposal'";
            dt1 = dv.ToTable();
            if (dt1.Rows.Count > 0)
                lblProposing.Text = dt1.Rows[0][1].ToString();

            dv.RowFilter = "Activity='First Approval'";
            dt1 = dv.ToTable();
            if (dt1.Rows.Count > 0)
                lblApp1.Text = dt1.Rows[0][1].ToString();

            dv.RowFilter = "Activity='IInd Approval'";
            dt1 = dv.ToTable();
            if (dt1.Rows.Count > 0)
                lblApp2.Text = dt1.Rows[0][1].ToString();
            
            dv.RowFilter = "Activity='Accounts'";
            dt1 = dv.ToTable();
            if (dt1.Rows.Count > 0)
                lblAccounts.Text = dt1.Rows[0][1].ToString();

        }

    }   
    #endregion

    public void ShowMessage(string Msg, bool Error)
    {
        lblMessage.Text = Msg;
        lblMessage.ForeColor = (Error) ? System.Drawing.Color.Green : System.Drawing.Color.Red;
    }
    protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "SELECT * FROM DBO.vw_My_Vendors where 1=1 ";//( isnull(ActivityUser,0)= 0 OR ActivityUser=" + Session["loginid"].ToString() + ") ";

        string whereclause = "";
        if (txtVendor.Text.Trim() != "")
        {
            whereclause += " and SupplierName like '" + txtVendor.Text.Trim() + "%'";
        }
        if (txtEmail.Text.Trim() != "")
        {
            whereclause += " and EmailAddress like '" + txtEmail.Text.Trim() + "%'";
        }

        if(ddlType.SelectedIndex>0)
            whereclause += " and Activity ='" + ddlType.SelectedValue + "'";
      
        DataTable DTVendor = Common.Execute_Procedures_Select_ByQuery(sql + whereclause);
        rptVendor.DataSource = DTVendor;
        rptVendor.DataBind();
        lblTotRec.Text =DTVendor.Rows.Count + " Records Found."  ;
    }
    
}

   