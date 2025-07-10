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
            ddlType_OnSelectedIndexChanged(sender, e);
            string sql = "SELECT Activity,COUNT(*) as NUMREC FROM dbo.[vw_My_Vendors] GROUP BY Activity";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            DataView dv=dt.DefaultView;
            DataTable dt1;

            dv.RowFilter = "Activity='Proposal'";
            dt1 = dv.ToTable();
            if (dt1.Rows.Count > 0)
                lblProposing.Text = dt1.Rows[0][1].ToString();

            //dv.RowFilter = "Activity='Accounts'";
            //dt1 = dv.ToTable();
            //if (dt1.Rows.Count > 0)
                //lblAccounts.Text = dt1.Rows[0][1].ToString();

            string sql1 = "SELECT Activity,COUNT(*) as NUMREC FROM dbo.[vw_My_Vendors] where ActivityUser = " + int.Parse(Session["loginid"].ToString()) + " GROUP BY Activity";
            DataTable dt2 = Common.Execute_Procedures_Select_ByQuery(sql1);
            DataView dv1 = dt2.DefaultView;
            DataTable dt3;

            //dv.RowFilter = "Activity='New Request'";
            //dt1 = dv.ToTable();
            //if (dt1.Rows.Count > 0)
            //    lblNewRequest.Text = dt1.Rows[0][1].ToString();

            dv1.RowFilter = "Activity='First Approval'";
            dt3 = dv1.ToTable();
            if (dt3.Rows.Count > 0)
                lblApp1.Text = dt3.Rows[0][1].ToString();

            dv1.RowFilter = "Activity='IInd Approval'";
            dt3 = dv1.ToTable();
            if (dt3.Rows.Count > 0)
                lblApp2.Text = dt3.Rows[0][1].ToString();
            
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
    public void btnEmailToVendor_Click(object sender, EventArgs e)
    {
        try
        {
            int __vrid = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select guid,EmailAddress from dbo.tbl_venderrequest where vrid=" + __vrid);
            if (dt1.Rows.Count > 0)
            {
                string guid = dt1.Rows[0]["guid"].ToString();
                string emailid = dt1.Rows[0]["EmailAddress"].ToString();
                string useremail = ProjectCommon.getUserEmailByID(Session["loginid"].ToString());
                string[] ToAdd = { emailid };
                string[] NoAdd = { "" };
                string[] CcAdd = { useremail };
                string[] BccAdd = { "emanager@energiossolutions.com" };
                string LinkText = ConfigurationManager.AppSettings["UpdateVendorProfile"].ToString() + guid;
                // string LinkText = "http://http://localhost:52513/vendormanagement/ModifyVendorProfile.aspx?_key=" + guid;
                string Message = "Dear Vendor/Supplier,<br/><br/>Your request successfully received by us.<br/><br/>Below is the link given to update your full profile.<br/><br/> <a href='" + LinkText + "' target='_blank'>Click Here</a> <br/>OR<br/>copy the link <br/>" + LinkText + "<br/><br/>and paste in a browser new window to update the profile.<br/><br/> Once you submit your profile to us we will continue your approval process.<br><br><br>Thank You<br> <br/><br/><br/>";
               
                if (ProjectCommon.SendMail(ToAdd, CcAdd, BccAdd, "EMANAGER : Vendor/Supplier Registration", Message, NoAdd))
                    Common.Execute_Procedures_Select_ByQuery("update dbo.tbl_venderrequest set AllowToEditByMail='Y' where vrid=" + __vrid);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('Email sent successfully.');", true);
            }
        }
       catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }









   
}

   