using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;


public partial class InsuranceRecordManagement_InsuranceHome : System.Web.UI.Page
{
    public Authority Auth;     
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 311);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------

        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }

        if (!Page.IsPostBack)
        {
            ShowData();
        }

    }

    public void ShowData()
    {
        string Sql1 = "SELECT *  FROM IRM_PolicyMaster WHERE DATEADD(dd, 0, DATEDIFF(dd, 0, ExpiryDt))  >= DATEADD(dd, 0, DATEDIFF(dd, 0, getdate()))  AND DATEADD(dd, 0, DATEDIFF(dd, 0, ExpiryDt)) <= DateAdd(dd,30,DATEADD(dd, 0, DATEDIFF(dd, 0, getdate()))) ";
        DataTable dt1 = Budget.getTable(Sql1).Tables[0];
        int Count1 = Common.CastAsInt32(dt1.Rows.Count);
        lblc1.Text = Count1.ToString();

        //--------------------

        string Sql2 = "SELECT * FROM IRM_CaseMaster WHERE CaseStatus = 1 "; 
        DataTable dt2 = Budget.getTable(Sql2).Tables[0];
        int Count2 = Common.CastAsInt32(dt2.Rows.Count);
        lblc2.Text = (Count2).ToString();

        //--------------------

        

       
    }
}