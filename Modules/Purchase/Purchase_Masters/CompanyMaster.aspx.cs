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

public partial class CompanyMaster : System.Web.UI.Page
{
    public string SelectedCompId
    {
        get { return hfPRID.Value; }
        set { hfPRID.Value = value.ToString(); }
    }
    public AuthenticationManager authPR = new AuthenticationManager(0, 0, ObjectType.Page);
    #region ---------- PageLoad ------------
    // PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        
         
        try
        {
            authPR = new AuthenticationManager(1062, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authPR.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }

            //authRFQList = new AuthenticationManager(227, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------


        if (!Page.IsPostBack)
        {
            BindVessel();
        }
    }
    #endregion
    private void BindVessel()
    {
        string sql = "SELECT Company,[Company Name]CompanyName,Active,InAccts,ReportCo  FROM AccountCompany with(nolock) ";
        DataTable dtComp = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtComp != null)
        {
            RptComp.DataSource = dtComp;
            RptComp.DataBind();
            if (dtComp.Rows.Count <= 0)
            {
                lblmsg.Text = "No Data Found.";
            }
        }
        else
        {
            lblmsg.Text = "No Data Found.";
        }

    }
}
