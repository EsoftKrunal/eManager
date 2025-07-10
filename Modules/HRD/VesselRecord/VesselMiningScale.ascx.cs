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
public partial class VesselRecord_VesselMiningScale : System.Web.UI.UserControl
{
    #region Declare Property
    private int _vesselid;
    public int Vesselid
    {
        get { try
            {
                if (ViewState["VesselId"] != null)
                {
                    return Convert.ToInt32(ViewState["VesselId"].ToString());
                }
                else
                {
                    return 0;
                }
                 
            } catch { return 0; } 
        }
        set { ViewState["VesselId"] = value; }
    }
    #endregion
    private void BindFlagNameDropDown()
    {
        //DataTable dt1 = VesselDetailsGeneral.selectDataFlag();
        //this.ddlFlagStateName.DataValueField = "FlagStateId";
        //this.ddlFlagStateName.DataTextField = "FlagStateName";
        //this.ddlFlagStateName.DataSource = dt1;
        //this.ddlFlagStateName.DataBind();
    }
    string Mode;
    protected void Page_Load(object sender, EventArgs e)
    {
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 41);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        if (!IsPostBack)
        {
            BindYearManning();
            try
            {
                try
                {
                    Mode = Session["VMode"].ToString();
                }
                catch { Mode = "New"; }

                btnAddmanning.Visible = ((Mode == "New") || (Mode == "Edit"));
                BindActualManning();
                BindFlagNameDropDown();
                //txtVesselName.Text = Session["VesselName"].ToString();
                //txtFormerVesselName.Text = Session["FormerName"].ToString();
                //ddlFlagStateName.SelectedValue = Session["FlagStateId"].ToString();
            }
            catch { }
            //VesselBudgetManning1.Vesselid = this.Vesselid;
            //VesselSafeManning1.Vesselid = this.Vesselid;
            //pan_BudgetManning.Visible = false;   
        }
        //**********
    }
    protected void ManningType_Changed(object sender, EventArgs e)
    {
        //pan_SafeManning.Visible = Rad_Manning.SelectedValue == "S";
        //pan_BudgetManning.Visible = Rad_Manning.SelectedValue == "B";
    }

    

    //---------------------
    public void BindYearManning()
    {
        for (int i = DateTime.Today.Year ; i >= 2015; i--)
        {
            ddlYearManning.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

    }
    
    protected void ddlYearManning_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindActualManning();
    }
    public string FormatCurrency(object o)
    {
        return String.Format("{0:0.00}", Common.CastAsDecimal(o));
    }
    public string HighLight(object b, object a)
    {
        decimal budget = Common.CastAsDecimal(b);
        decimal actual = Common.CastAsDecimal(a);

        if (actual > budget)
            return "highlight";
        else
            return "";
    }
    //----------------------------------------------------------------------
    public void BindActualManning()
    {
        string sql = " Select RankCode "+
                     " ,(select safemanning  from vesselsafemanning vsm where VesselId = " + Vesselid.ToString() + " and ManningGradeId = R.RankId )SafeManning " +

                     " ,(select BUDGETMANNING from VESSELBUDGETMANNING Where VesselID = " + Vesselid.ToString() + " and RankID = R.RankId and BYear = " + ddlYearManning.SelectedValue + ")BudgetManning " + 
                     " ,(select C.COUNTRYNAME from VESSELBUDGETMANNING V inner join COUNTRY C on V.BudgetNationality = C.COUNTRYID Where VesselID = " + Vesselid.ToString() + " and RankID = R.RankId and BYear = " + ddlYearManning.SelectedValue + ")BudgetCounty " +
                     " ,(select Wages from VESSELBUDGETMANNING Where VesselID = " + Vesselid.ToString() + " and RankID = R.RankId and BYear = " + ddlYearManning.SelectedValue + ")BudgetWages " +

                     " ,(SELECT COUNT(*) FROM CREWPERSONALDETAILS CPD WHERE CPD.crewstatusid=3 AND CPD.currentvesselid=" + Vesselid.ToString() + " AND CPD.CURRENTRANKID=R.RANKID) as ActualManning " +
                     " ,dbo.GET_Country_Manning (" + Vesselid.ToString() + ",R.RANKID)as ActualManningCountries" +
                     " ,((select isnull(SUM(case when COMPONENTTYPE = 'E' THEN amount ELSE 0 END),0) - isnull(SUM(case when COMPONENTTYPE = 'D' THEN amount ELSE 0 END),0) from crewcontractdetails ccd WHERE ccd.contractid in (select contractid from crewpersonaldetails where currentvesselid = " + Vesselid.ToString() + " and currentrankid = R.RANKID and crewstatusid = 3)) " +
		     " +(select isnull(sum(otheramount),0) from crewcontractheader cch WHERE cch.contractid in (select contractid from crewpersonaldetails where currentvesselid = 71 and currentrankid = R.RANKID and crewstatusid = 3))) as ACTUALWAGES " +
                     "      from Rank R order by RankLevel ";


        sql = " with Tbl "+
              "  as " +
              " ( " +
              "     Select mm.GradeName,R.RANKID,RankCode, RankLevel " +
              "     ,(select safemanning from vesselsafemanning vsm where VesselId = " + Vesselid.ToString() + " and ManningGradeId = mm.gradeid )SafeManning " +
              "     ,(select BUDGETMANNING from VESSELBUDGETMANNING Where VesselID = " + Vesselid.ToString() + " and RankID = R.RankId and BYear = " + ddlYearManning.SelectedValue + ")BudgetManning " +
              "     ,(select MIN(RANKID) from MANNINGGRADEdetails DD1 Where DD1.GRADEID = MM.GRADEID) MinRank " +
              "     ,(select C.COUNTRYNAME from VESSELBUDGETMANNING V inner join COUNTRY C on V.BudgetNationality = C.COUNTRYID Where VesselID = " + Vesselid.ToString() + " and RankID = R.RankId and BYear = " + ddlYearManning.SelectedValue + ")BudgetCounty " +
              "      ,(select Wages from VESSELBUDGETMANNING Where VesselID = " + Vesselid.ToString() + " and RankID = R.RankId and BYear = " + ddlYearManning.SelectedValue + ")BudgetWages "+
              "   ,(SELECT COUNT(*) FROM CREWPERSONALDETAILS CPD WHERE CPD.crewstatusid = 3 AND CPD.currentvesselid = " + Vesselid.ToString() + " AND CPD.CURRENTRANKID = R.RANKID) as ActualManning  "+
              "  ,dbo.GET_Country_Manning(" + Vesselid.ToString() + ", R.RANKID) as ActualManningCountries " +
              "  ,((select isnull(SUM(case when COMPONENTTYPE = 'E' THEN amount ELSE 0 END), 0) - isnull(SUM(case when COMPONENTTYPE = 'D' THEN amount ELSE 0 END), 0) from crewcontractdetails ccd WHERE ccd.contractid in (select contractid from crewpersonaldetails where currentvesselid = " + Vesselid.ToString() + " and currentrankid = R.RANKID and crewstatusid = 3))  +(select isnull(sum(otheramount), 0) from crewcontractheader cch WHERE cch.contractid in (select contractid from crewpersonaldetails where currentvesselid = " + Vesselid.ToString() + " and currentrankid = R.RANKID and crewstatusid = 3))) as ACTUALWAGES " +
              " from Rank R " +
              " left join MANNINGGRADEdetails md on r.rankid = md.rankid " +
              " left join MANNINGGRADEMASTER mm on md.gradeid = mm.gradeid " +
              " ) " +
              " select *,case when MInRank=RankId then SafeManning ELSE NULL END AS NewSafeManning from Tbl " +
              " where BudgetManning> 0 or ActualManning> 0 " +
              " order by RankLevel  ";

        //Session["sqlVesselManningReport"] =ddlYearManning.SelectedValue +"~" + sql;

        DataTable dtData = Budget.getTable(sql).Tables[0];
        rptSafeManningData.DataSource = dtData;
        rptSafeManningData.DataBind();
        
        lblTotManning.Text=dtData.Compute("SUM(NewSafeManning)", "").ToString();
        lblTotBudgetManning.Text=dtData.Compute("SUM(BudgetManning)","").ToString();
        lblTotActualManning.Text=dtData.Compute("SUM(ActualManning)", "").ToString();

        lblTotActualWages.Text = FormatCurrency( dtData.Compute("SUM(ACTUALWAGES)", ""));
        lblTotBudgetWages.Text = FormatCurrency(dtData.Compute("SUM(BudgetWages)", ""));
    }
}



