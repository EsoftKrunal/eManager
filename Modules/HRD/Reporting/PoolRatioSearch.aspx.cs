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
using ShipSoft.CrewManager.Operational;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;

public partial class PoolRatioSearch : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //this.txt_from.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 184);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("DummyReport.aspx");

        //}
        //*******************
        if (!Page.IsPostBack)
        {
            BindRecruitingOffice();   
        }
        //==========
        lblmessage.Text = "";
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text.Trim() == "")
        {
            lblmessage.Text = "Please enter From date.";
            return;
        }
        if (txtToDate.Text.Trim() == "")
        {
            lblmessage.Text = "Please enter To date.";
            return;
        }
        //if (ddlLocation.SelectedIndex==0)
        //{
        //    lblmessage.Text = "Please select location.";
        //    return;
        //}
        IFRAME1.Attributes.Add("src", "PoolRatioReport.aspx?FromDate=" + txtFromDate.Text.Trim() + "&ToDate=" + txtToDate.Text.Trim() + "&RECID=" + ddlLocation.SelectedValue + "&OffCrewID=" + ddlOffCrew .SelectedValue+ "&Location="+ddlLocation.SelectedItem.Text+"");
    }
    public void BindRecruitingOffice()
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
        ddlLocation.DataValueField = "RecruitingOfficeId";
        ddlLocation.DataTextField = "RecruitingOfficeName";
        ddlLocation.DataSource = processgetrecruitingoffice.ResultSet;
        ddlLocation.DataBind();
        ddlLocation.Items.RemoveAt(0);
        ddlLocation.Items.Insert(0, new ListItem("< All >", "0"));

    }
}
