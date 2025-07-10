using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class VerifyJobsPopUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblVerifiedBy.Text = Session["FullName"].ToString();
        lblVerifiedOn.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        lblCompName.Text = Page.Request.QueryString["CompName"].ToString();
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        
        if (txtComment.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter comments.");
            return;
        }
        string UserName = Session["UserName"].ToString();
        Common.Set_Procedures("OfficeVerifyJobHistory");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", Page.Request.QueryString["VesselCode"].ToString()),
            new MyParameter("@HistoryId", Page.Request.QueryString["HistoryId"].ToString()),
            new MyParameter("@Comments", txtComment.Text.Trim()),
            new MyParameter("@VerifiedBy", UserName));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ProjectCommon.ShowMessage("Verifed Successfully.");
            Page.RegisterStartupScript("ss", "<script>RefereshBackPage()</script>");
        }
        else
        {
            ProjectCommon.ShowMessage("Unable to Verify.");
        }
        
    }   
    
}
