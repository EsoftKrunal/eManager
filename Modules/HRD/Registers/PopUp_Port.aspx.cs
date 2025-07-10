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

public partial class Registers_PopUp_Port : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Page.IsPostBack == false)
        {
            bindCountryDDL();
        }
    }
    public void bindCountryDDL()
    {
        DataTable dt22 = Port.selectDataCountryDetails();
        ddl_P_Country.DataValueField = "CountryId";
        ddl_P_Country.DataTextField = "CountryName";
        ddl_P_Country.DataSource = dt22;
        ddl_P_Country.DataBind();
    }
    protected void btn_Save_Port_Click(object sender, EventArgs e)
    {
       
        DataTable dt = Port.CheckDuplicatePort(Convert.ToInt32(this.ddl_P_Country.SelectedValue), this.txtPortName.Text);
        if (dt.Rows.Count > 0)
        {
            lbl_Port_Message.Text = "Port Name Already Exist For This Country";
        }
       
        else
        {
            try
            {
                Port.insertUpdatePortDetails("InsertUpdatePortDetails",
                                               -1,
                                               Convert.ToInt32(this.ddl_P_Country.SelectedValue),
                                               txtPortName.Text,
                                               Convert.ToInt32(Session["loginid"].ToString()),
                                               -1,
                                               Convert.ToChar("A"));
                lbl_Port_Message.Text = "Record Saved Successfully";
            }
            catch(SystemException es)
            {
                lbl_Port_Message.Text = "Error in saving the data ,Please Try Again";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
