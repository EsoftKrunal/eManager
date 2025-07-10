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

public partial class NMAnalysis : System.Web.UI.Page
{
  
    int intLogin_Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        else
        {
            intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
        }

        lblmessage.Text = "";
        if(!(IsPostBack))
        {
            Load_vessel();
        }
    }
    private void Load_vessel()
    {
        DataSet dt = Budget.getTable("Select * from dbo.vessel where vesselstatusid<>2  order by vesselname");
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField= "VesselId";
        ddlVessel.DataBind();

        ddlVessel.Items.Insert(0,new ListItem(" < All >",""));
    }
    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "window.open('./NMAnalysis_Excel.aspx?VesselId=" + ddlVessel.SelectedValue + "&NMType=" + ddlNMType.SelectedValue + "&AccCat=" + ddlAcccategory.SelectedValue + "&Fdt=" + txtFromDate.Text + "&Tdt=" + txtToDate.Text + "','','');", true);  
    }
}
