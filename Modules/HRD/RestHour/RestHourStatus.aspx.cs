using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class RestHourStatus : System.Web.UI.Page
{
    int UserId = 0;

    public int VesselID
    {
        set { ViewState["VesselID"] = value; }
        get 
        {
            return Common.CastAsInt32(ViewState["VesselID"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //-------------------------
        lblMsg.Text = "";
        if (!Page.IsPostBack)
        {
            Session["MM"] = "RH";
            Session["SM"] = "RH1";
            BindYear();
            LoadFleet();
            
            //ShowCrewList();
            

        }
    }
    
    protected void Show_Click(object sender, EventArgs e)
    {
        ShowCrewList();
    }
    
    
    
    public void ShowCrewList()
    {
        string Sql = "exec sp_GetVesselRunningHourStatus "+ddlYear.SelectedValue+","+ddlFleet.SelectedValue+","+ Convert.ToInt32(Session["loginid"].ToString()) + "";
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
        rptCrewList.DataSource = Dt;
        rptCrewList.DataBind();
    }
    public void BindYear()
    {
        for (int i = DateTime.Now.Year; i >= 2000; i--)
        {
            ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));

        }
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();

    }
    public void LoadFleet()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM FLEETMASTER");
        ddlFleet.DataSource = dt;
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetId";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("All Fleet", "0"));
    }

}
