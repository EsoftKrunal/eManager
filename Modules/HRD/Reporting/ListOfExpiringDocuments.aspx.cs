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


using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class Reporting_ListOfExpiringDocuments : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        
        if (!Page.IsPostBack)
        {
            BindCrewStatusDropDown();
            BindRecruitingOffice();
        }
        else
        {
            Button1_Click(sender, e);
        }
    }
    private void BindCrewStatusDropDown()
    {
        ProcessSelectCrewStatus obj = new ProcessSelectCrewStatus();
        obj.Invoke();
        ddlStatus.DataSource = obj.ResultSet.Tables[0];
        ddlStatus.DataTextField = "CrewStatusName";
        ddlStatus.DataValueField = "CrewStatusId";
        ddlStatus.DataBind();
        ddlStatus.Items.RemoveAt(0);
        ddlStatus.Items.Insert(0,new ListItem("< All >", ""));  
    }
    private void BindRecruitingOffice()
    {
        ProcessGetRecruitingOffice obj = new ProcessGetRecruitingOffice();
        obj.Invoke();
        ddlReOff.DataValueField = "RecruitingOfficeId";
        ddlReOff.DataTextField = "RecruitingOfficeName";
        ddlReOff.DataSource = obj.ResultSet;
        ddlReOff.DataBind();
        ddlReOff.Items.RemoveAt(0);
        ddlReOff.Items.Insert(0,new ListItem("< All >", ""));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        IFRAME1.Attributes.Add("src", "ListOfExpiringDocumentsCrystal.aspx?Status=" + ddlStatus.SelectedValue + "&DocType=" + ddlDocType.SelectedValue + "&RecOff=" + ddlReOff.SelectedValue + "&Exp=" + ((chkExp.Checked)?"1":"0"));
    }
}
