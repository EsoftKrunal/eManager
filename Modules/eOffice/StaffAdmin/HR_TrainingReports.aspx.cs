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

public partial class emtm_StaffAdmin_Emtm_HR_TrainingReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            BindYear();
        }
    }

    public void BindYear()
    {
        for (int i = DateTime.Now.Year + 1; i >= 2012; i--)
        {
            ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
        }
        ddlYear.SelectedValue = DateTime.Today.Year.ToString();
        //ddlYear_SelectedIndexChanged(new object(), new EventArgs());
    }
}