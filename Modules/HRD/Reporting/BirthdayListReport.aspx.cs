using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ShipSoft.CrewManager.BusinessLogicLayer;

public partial class BirthdayListReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()),15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************
        if (!(IsPostBack))
        {
            BindCrewStatusDropDown();
            BindRecruitingOffice();
        }
    }
    private void BindCrewStatusDropDown()
    {
        ProcessSelectCrewStatus obj = new ProcessSelectCrewStatus();
        obj.Invoke();
        ddl_Status.DataSource = obj.ResultSet.Tables[0];
        ddl_Status.DataTextField = "CrewStatusName";
        ddl_Status.DataValueField = "CrewStatusId";
        ddl_Status.DataBind();
        ddl_Status.Items[0].Text = "< All >";
        ddl_Status.Items[0].Value = "";
    }
    private void BindRecruitingOffice()
    {
        ProcessGetRecruitingOffice processgetrecruitingoffice = new ProcessGetRecruitingOffice();
        try
        {
            processgetrecruitingoffice.Invoke();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
        }
        ddlRecOff.DataValueField = "RecruitingOfficeId";
        ddlRecOff.DataTextField = "RecruitingOfficeName";
        ddlRecOff.DataSource = processgetrecruitingoffice.ResultSet;
        ddlRecOff.DataBind();
        ddlRecOff.Items[0].Text = "< All >";
        ddlRecOff.Items[0].Value = "";
    }
    public void ShowData()
    {
        IFRAME1.Attributes.Add("src", "BirthdayListContainer.aspx?CrewStatus=" + ddl_Status.SelectedValue + "&Month=" + ddlMonth.SelectedValue + "&RecOff=" + ddlRecOff.SelectedValue + "&OffRat=" + ddlOffRat.SelectedValue);  
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ShowData();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
       
    }
}
