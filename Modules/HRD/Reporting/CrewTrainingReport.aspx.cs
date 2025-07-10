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

public partial class Reporting_CRMReport : System.Web.UI.Page
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
            //BindRecruitingOffice();
            BindVessel();
            ddlYear.Items.Add(new ListItem("Year","0"));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            ddlYear.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            ddlYear.Items.Add(new ListItem((DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString()));
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 83);
            if (chpageauth <= 0)
            {
                Response.Redirect("DummyReport.aspx");
            }

            //*******************
          
            this.lblmessage.Text = "";
        }
        else
        {
            btn_show_Click(sender, e);
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
    }
    private void BindRecruitingOffice()
    {
        //ProcessGetRecruitingOffice processgetrecruitingoffice = new ProcessGetRecruitingOffice();
        //try
        //{
        //    processgetrecruitingoffice.Invoke();
        //}
        //catch (Exception ex)
        //{
        //    //Response.Write(ex.Message.ToString());
        //}
        //ddl_Recr_Office.DataValueField = "RecruitingOfficeId";
        //ddl_Recr_Office.DataTextField = "RecruitingOfficeName";
        //ddl_Recr_Office.DataSource = processgetrecruitingoffice.ResultSet;
        //ddl_Recr_Office.DataBind();
    }
    private void BindVessel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataSource = ds;
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        string SearchStr = "";
        //----------------------------------- Crew Number
        if (txt_MemberId_Search.Text.Trim() != "")
        {
            SearchStr = "CPD.CrewNumber Like '" + txt_MemberId_Search.Text.Replace("'", "''") + "%'"; 
        }
        //----------------------------------- Member Name
        if (txtName.Text.Trim() != "")
        {
            SearchStr = ((SearchStr.Trim() == "") ? "" : SearchStr + " AND ") + "(CPD.FirstName Like '%" + txtName.Text.Trim().Replace("'", "''") + "%' OR CPD.LastName Like '%" + txtName.Text.Replace("'", "''") + "%')";
        }
        //----------------------------------- Nationality
        if (ddl_Nationality.SelectedIndex >0)
        {
            SearchStr = ((SearchStr.Trim() == "") ? "" : SearchStr + " AND ") + " CPD.NationalityId =" + ddl_Nationality.SelectedValue;
        }
        //----------------------------------- Due Month
        if (ddlMonth.SelectedIndex > 0)
        {
            SearchStr = ((SearchStr.Trim() == "") ? "" : SearchStr + " AND ") + " Month(CTR.N_DueDate)=" + ddlMonth.SelectedValue + "";
        }
        //----------------------------------- Due Year
        if (ddlYear.SelectedIndex > 0)
        {
            SearchStr = ((SearchStr.Trim() == "") ? "" : SearchStr + " AND ") + " Year(CTR.N_DueDate)=" + ddlYear.SelectedValue + "";
        }
        ////----------------------------------- Recr Off
        //if (ddl_Recr_Office.SelectedIndex >0)
        //{
        //    SearchStr = ((SearchStr.Trim() == "") ? "" : SearchStr + " AND ") + " CPD.RecruitmentOfficeId=" + ddl_Recr_Office.SelectedValue;
        //}

        //----------------------------------- Recommended for Promotion
        if (chkRec.Checked)
        {
            SearchStr = ((SearchStr.Trim() == "") ? "" : SearchStr + " AND ") + " EXISTS (SELECT CREWID FROM CREWAPPRAISALDETAILS V WHERE V.N_RECOMMENDEDNew='Y' AND V.CrewAppraisalId=CTR.N_CrewAppraisalId AND V.CREWID=CPD.CREWID)";
        }
        //----------------------------------- Vessel
        if (ddl_Vessel.SelectedIndex > 0)
        {
            SearchStr = ((SearchStr.Trim() == "") ? "" : SearchStr + " AND ") + " CAD.VesselId=" + ddl_Vessel.SelectedValue;
        }
        //----------------------------------- Training Status
        if (ddlStatus.SelectedIndex > 0)
        {
            SearchStr = ((SearchStr.Trim() == "") ? "" : SearchStr + " AND ") + " CTR.N_CrewTrainingStatus='" + ddlStatus.SelectedValue + "'";
        }
        //-----------------------------------
        //-----------------------------------
        //-----------------------------------
        //-----------------------------------
        Session["SearchStr"] = SearchStr;   
        IFRAME1.Attributes.Add("src", "CrewTrainingRContainer.aspx");  
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
