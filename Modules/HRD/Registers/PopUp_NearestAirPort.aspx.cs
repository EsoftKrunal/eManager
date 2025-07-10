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

public partial class Registers_PopUp_NearestAirPort : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Page.IsPostBack == false)
        {
            bindCountryNameDDL();
        }

    }
    public void bindCountryNameDDL()
    {
        DataTable dt4 = NearestAirport.selectDataCountryName();
        this.ddlCountryName.DataValueField = "CountryId";
        this.ddlCountryName.DataTextField = "CountryName";
        this.ddlCountryName.DataSource = dt4;
        this.ddlCountryName.DataBind();
    }
    protected void btn_Add_NearestAirport_Click(object sender, EventArgs e)
    {
       
        DataTable dt = NearestAirport.CheckDuplicatePort(Convert.ToInt32(this.ddlCountryName.SelectedValue), this.txtNearestAirportName.Text);
        if (dt.Rows.Count > 0)
        {
            this.Label1.Text = "Port Name Already Exist For This Country";
        }
        
        else
        {
            NearestAirport.insertUpdateNearestAirportDetails("InsertUpdateNearestAirportDetails",
                                              -1,
                                              Convert.ToInt32(this.ddlCountryName.SelectedValue),
                                              txtNearestAirportName.Text,
                                              Convert.ToInt32(Session["loginid"].ToString()),
                                              -1,
                                              Convert.ToChar("A"));
            this.Label1.Text = "Record Saved Successfully";

        }
    }
   
}
