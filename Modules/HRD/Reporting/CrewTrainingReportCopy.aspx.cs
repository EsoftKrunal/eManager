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

public partial class Reporting_CRMReportCopy : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblmessage.Text = "";
        if (Page.IsPostBack == false)
        {
            BindNationalityDropDown();
            BindVessel();
            BindRank();
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 11);
            if (chpageauth <= 0)
            {
                Response.Redirect("~/Modules/HRD/CrewOperation/Dummy_Training1.aspx");
                //Response.Redirect("~/CrewOperation/DummyReport.aspx");
            }

            //*******************
          
            this.lblmessage.Text = "";
        }
        else
        {
            //btn_show_Click(sender, e);
        }
    }
    private void BindNationalityDropDown()
    {
        ProcessSelectNationality obj = new ProcessSelectNationality();
        obj.Invoke();
        ddl_Nationality.DataSource = obj.ResultSet.Tables[0];
        ddl_Nationality.DataTextField = "CountryName";
        ddl_Nationality.DataValueField = "CountryId";
        ddl_Nationality.DataBind();
        ddl_Nationality.Items.RemoveAt(0);
        ddl_Nationality.Items.Insert(0, new ListItem("< All >", "0"));
    }
    private void BindVessel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataSource = ds;
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< Select >", ""));
    }
    
    protected void btn_show_Click(object sender, EventArgs e)
    {
        //if (ddlFilterField.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please select the Vessel.')", true); ddl_Vessel.Focus(); return;
        //}
        BindReport(ddlFilterField.SelectedValue);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txt_MemberId_Search.Text = "";
        txtName.Text = "";
        ddl_Nationality.SelectedIndex = 0;
        txtTraining.Text = "";
        ddl_Vessel.SelectedIndex = 0;
        txtFromDt.Text = "";
        txtToDate.Text = "";
        chkRec.Checked = false;
        ddlFilterField.SelectedIndex = 0;
    }

    public void BindReport(string Val)
    {
        try
        {
            DateTime dt ;
            if(txtFromDt.Text.Trim()!="")
                dt = Convert.ToDateTime(txtFromDt.Text);
        }
        catch( Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Invalid from date.')", true);
            return;
        }
        try
        {
            DateTime dt ;
            if (txtToDate.Text.Trim() != "")
                dt = Convert.ToDateTime(txtToDate.Text);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Invalid To date.')", true);
            return;
        }
        IFRAME1.Attributes.Add("src", "CrewTrainingRContainer.aspx?CrNo=" + txt_MemberId_Search.Text + "&CrewName=" + txtName.Text + "&nat=" + ddl_Nationality.SelectedValue + "&train=" + txtTraining.Text + "&curvess=" + Common.CastAsInt32( ddl_Vessel.SelectedValue )+ "&fdt=" + txtFromDt.Text + "&tdt=" + txtToDate.Text +  "&promo=" + ((chkRec.Checked)?"1":"0") + "&rtype=" + ddlFilterField.SelectedValue+"&Rank="+ddlRank.SelectedValue+"&status=" + DropDownList1.SelectedValue);
    }
    private void BindRank()
    {
        string sql = "SELECT RankID,RankCode FROM Rank ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlRank.DataValueField = "RankID";
        ddlRank.DataTextField = "RankCode";
        ddlRank.DataSource = dt;
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("< All >", ""));
    }


    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
