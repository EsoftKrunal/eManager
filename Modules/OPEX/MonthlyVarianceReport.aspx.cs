using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;

public partial class Modules_OPEX_MonthlyVarianceReport : System.Web.UI.Page
{
    public AuthenticationManager authRecInv;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    DataSet DsValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();

        //---------------------------------------
        if (Request.Form["company"] != null && Request.Form["vessel"] != null && Request.Form["year"] != null && Request.Form["majcatid"] != null && (!IsPostBack))
        {
            string company = Request.Form["company"];
            string vessel = Request.Form["vessel"];
            string year = Request.Form["year"];
            int majcatid = Common.CastAsInt32(Request.Form["majcatid"]);
            int midcatid = Common.CastAsInt32(Request.Form["midcatid"]);
            int mincatid = Common.CastAsInt32(Request.Form["mincatid"]);

            if (mincatid > 0)
                WriteInResponse(getAccounts(company, vessel, year, majcatid, midcatid, mincatid));
            if (midcatid > 0)
                WriteInResponse(getMinCategory(company, vessel, year, majcatid, midcatid));
            else if (majcatid > 0)
                WriteInResponse(getMidCategory(company, vessel, year, majcatid));

            return;
        }
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1068, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                Response.Redirect("~/Unauthorized.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Unauthorized.aspx");
        }
        #endregion ----------------------------------------
        try
        {
           // LblNoRow.Text = "";
            if (!Page.IsPostBack)
            {
                Manage_Menu();
                BindddlYear();
                BindCompany();
                BindVessel(ddlCompany.SelectedValue);
                //LoadSession();
                //ddlReportLevel.SelectedIndex = 2;
               // imgPrint.Visible = authRecInv.IsPrint;
            }
        }
        catch { Exception ex; }
    }

    public void BindddlYear()
    {
        string sql = "Select  distinct CurFinYear from AccountCompanyBudgetMonthyear with(nolock) where Cocode = '" + ddlCompany.SelectedValue + "'";
        DataTable dtCurrentyear = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCurrentyear != null)
        {
            ddlyear.DataSource = dtCurrentyear;
            ddlyear.DataTextField = "CurFinYear";
            ddlyear.DataValueField = "CurFinYear";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("<Select>", ""));
        }
    }
    public void Manage_Menu()
    {
        AuthenticationManager auth = new AuthenticationManager(5, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        
    }
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "selECT cmp.[Company Name] as CompanyName FROM vw_sql_tblSMDPRCompany cmp where Company='" + ddlCompany.SelectedValue + "'";
        DataTable DtName = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtName != null)
        {
            if (DtName.Rows.Count > 0)
            {
                ddlCompany.ToolTip = DtName.Rows[0][0].ToString();
            }
        }
        BindVessel(ddlCompany.SelectedValue);
        BindddlYear();
        if (ddlCompany.SelectedValue == "0")
        {
            return;
        }
        if (ddlVessel.SelectedValue == "0")
        {
            return;
        }

        if (ddlyear.SelectedValue == "0")
        {
            return;
        }

        divHeader.Visible = true;
        lblSearchText.Text = ddlyear.SelectedItem.Text + " - "  + ddlVessel.SelectedItem.Text ;
        BindMajorCategory();
        //Search();
    }
    public void WriteInResponse(string data)
    {
        Response.Clear();
        Response.Write(data);
        Response.End();
    }

    //protected void ddlReportLevel_OnSelectedIndexChanged(object sener, EventArgs e)
    //{
    //    if (ddlReportLevel.SelectedIndex == 0)
    //        return;
    //    if (ddlCompany.SelectedIndex == 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select Company.')", true);
    //        ddlReportLevel.SelectedIndex = 0;
    //        return;
    //    }
    //    if (ddlVessel.SelectedIndex == 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "b", "alert('Please select Vessel.')", true);
    //        ddlReportLevel.SelectedIndex = 0;
    //        return;
    //    }
    //    divHeader.Visible = true;
    //    lblSearchText.Text = ddlyear.SelectedItem.Text + " - " + ddlVessel.SelectedItem.Text + " - " + ddlReportLevel.SelectedItem.Text;
    //    BindMajorCategory();
    //    //Search();

    //}
    protected void ddlVessel_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue == "0")
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select Company.')", true);
            return;
        }
        if (ddlVessel.SelectedValue == "0")
        {
           //ScriptManager.RegisterStartupScript(this, this.GetType(), "b", "alert('Please select Vessel.')", true);
            return;
        }
        lblSearchText.Text = ddlyear.SelectedItem.Text + " - " + ddlVessel.SelectedItem.Text ;
        BindMajorCategory();
        //Search();
    }
    
    protected void ddlyear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue == "0")
        {
            return;
        }
        if (ddlVessel.SelectedValue == "0")
        {
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "b", "alert('Please select Vessel.')", true);
            return;
        }
        divHeader.Visible = true;
        lblSearchText.Text = ddlyear.SelectedItem.Text + " - " + ddlVessel.SelectedItem.Text ;
        getMonYearforAccountCompany(ddlCompany.SelectedValue, ddlyear.SelectedItem.Text);
        BindMajorCategory();
        //Search();
    }

    protected void getMonYearforAccountCompany(string cocode, string curfyyr)
    {
        DataTable dtMthYr = Common.Execute_Procedures_Select_ByQuery("Select MonthName+'-'+CAST(RIGHT(Year,2) AS varchar(2)) As MthYr from AccountCompanyBudgetMonthyear where Cocode='" + cocode + "' and CurFinYear = '" + curfyyr + "'");
        if (dtMthYr.Rows.Count > 0)
        {
            lblMon1.Text = dtMthYr.Rows[0][0].ToString();
            lblMon2.Text = dtMthYr.Rows[1][0].ToString();
            lblMon3.Text = dtMthYr.Rows[2][0].ToString();
            lblMon4.Text = dtMthYr.Rows[3][0].ToString();
            lblMon5.Text = dtMthYr.Rows[4][0].ToString();
            lblMon6.Text = dtMthYr.Rows[5][0].ToString();
            lblMon7.Text = dtMthYr.Rows[6][0].ToString();
            lblMon8.Text = dtMthYr.Rows[7][0].ToString();
            lblMon9.Text = dtMthYr.Rows[8][0].ToString();
            lblMon10.Text = dtMthYr.Rows[9][0].ToString();
            lblMon11.Text = dtMthYr.Rows[10][0].ToString();
            lblMon12.Text = dtMthYr.Rows[11][0].ToString();
        }
    }

    protected void imgClear_Click(object sender, EventArgs e)
    {
        //vGeneralSummary.Visible = false;
        //divBudgetSummary.Visible = false;
        //divAccountSummary.Visible = false;
        //divAccountDetails.Visible = false;
        divHeader.Visible = false;
        ClearSession();
        ddlyear.SelectedIndex = 0;  
        ddlCompany.SelectedIndex = 0;
        BindVessel(ddlCompany.SelectedValue);
        ddlVessel.SelectedIndex = 0;
        rptData.DataSource = null;
        rptData.DataBind();
        rptMidCats.DataSource = null;
        rptData.DataBind();
        rptMinCats.DataSource = null;
        rptData.DataBind();
        rptAccounts.DataSource = null;
        rptData.DataBind();
    }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue == "0")
        {
            return;
        }
        if (ddlVessel.SelectedValue == "0")
        {
            return;
        }
        if (ddlyear.SelectedValue == "0")
        {
            return;
        }
        divHeader.Visible = true;
        lblSearchText.Text = ddlyear.SelectedItem.Text + " - " + ddlVessel.SelectedValue ;
        BindMajorCategory();
        //Search();
    }

    public int GetVesselNum()
    {
        string sql = "SELECT Vesselno FROM vw_sql_tblSMDPRVessels where shipid='" + ddlVessel.SelectedValue + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                return Common.CastAsInt32(dt.Rows[0][0]);
            }
        }
        return 0;
    }

    protected void BindMajorCategory()
    {
        if (ddlCompany.SelectedIndex <= 0 || ddlVessel.SelectedIndex <= 0)
        {
            rptData.DataSource = null;
            rptData.DataBind();
            return;
        }

        DataSet DsValue = GetTrackingData(ddlCompany.SelectedValue, ddlyear.SelectedItem.Text, ddlVessel.SelectedValue);

        if (DsValue != null)
        {
            rptData.DataSource = DsValue.Tables[0];
            rptData.DataBind();
            //Set Period
            if (DsValue.Tables[0].Rows.Count > 0)
            {
                string sMonth = DateTime.Now.ToString("MM");
                lblTargetUtilisation.Text = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(sMonth) / 12) * 100), 0) + " %";
            }
            else
            {
                // LblNoRow.Text = "No Records Found ";
            }
        }
        else
        {
            //LblNoRow.Text = "No Records Found ";
        }
    }

    //protected void BudgetSummary()
    //{
    //    if (ddlCompany.SelectedIndex <= 0 || ddlVessel.SelectedIndex <= 0)
    //    {
    //        rptBudgetSummary.DataSource = null;
    //        rptBudgetSummary.DataBind();
    //        return;
    //    }

    //    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
    //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec Sp_GetMonthwiseVariantReport_VSL_BudgetTracking '" + ddlCompany.SelectedValue + "','" + ddlVessel.SelectedValue + "'," + ddlyear.SelectedValue + "", con);
    //    cmd.CommandTimeout = 300;
    //    cmd.CommandType = CommandType.Text;

    //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
    //    adp.SelectCommand = cmd;
    //    DsValue = new DataSet();
    //    adp.Fill(DsValue);

    //    if (DsValue != null)
    //    {
    //        rptBudgetSummary.DataSource = DsValue.Tables[1];
    //        rptBudgetSummary.DataBind();
    //        //Set Period
    //        if (DsValue.Tables[0].Rows.Count > 0)
    //        {
    //            string sMonth = DateTime.Now.ToString("MM");
    //            lblTargetUtilisation.Text = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(sMonth) / 12) * 100), 0) + " %";
    //        }
    //        else
    //        {
    //            // LblNoRow.Text = "No Records Found ";
    //        }
    //    }
    //    else
    //    {
    //        //LblNoRow.Text = "No Records Found ";
    //    }
    //}

    //protected void AccountSummary()
    //{
    //    if (ddlCompany.SelectedIndex <= 0 || ddlVessel.SelectedIndex <= 0)
    //    {
    //        rptAccountsummary.DataSource = null;
    //        rptAccountsummary.DataBind();
    //        return;
    //    }

    //    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
    //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec Sp_GetMonthwiseVariantReport_VSL_BudgetTracking '" + ddlCompany.SelectedValue + "','" + ddlVessel.SelectedValue + "'," + ddlyear.SelectedValue + "", con);
    //    cmd.CommandTimeout = 300;
    //    cmd.CommandType = CommandType.Text;

    //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
    //    adp.SelectCommand = cmd;
    //    DsValue = new DataSet();
    //    adp.Fill(DsValue);

    //    if (DsValue != null)
    //    {
    //        rptAccountsummary.DataSource = DsValue.Tables[2];
    //        rptAccountsummary.DataBind();
    //        //Set Period
    //        if (DsValue.Tables[0].Rows.Count > 0)
    //        {
    //            string sMonth = DateTime.Now.ToString("MM");
    //            lblTargetUtilisation.Text = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(sMonth) / 12) * 100), 0) + " %";
    //        }
    //        else
    //        {
    //            // LblNoRow.Text = "No Records Found ";
    //        }
    //    }
    //    else
    //    {
    //        //LblNoRow.Text = "No Records Found ";
    //    }
    //}

    //protected void AccountSummaryDetails()
    //{
    //    if (ddlCompany.SelectedIndex <= 0 || ddlVessel.SelectedIndex <= 0)
    //    {
    //        rptAccountDetails.DataSource = null;
    //        rptAccountDetails.DataBind();
    //        return;
    //    }

    //    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
    //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec Sp_GetMonthwiseVariantReport_VSL_BudgetTracking '" + ddlCompany.SelectedValue + "','" + ddlVessel.SelectedValue + "'," + ddlyear.SelectedValue + "", con);
    //    cmd.CommandTimeout = 300;
    //    cmd.CommandType = CommandType.Text;

    //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
    //    adp.SelectCommand = cmd;
    //    DsValue = new DataSet();
    //    adp.Fill(DsValue);

    //    if (DsValue != null)
    //    {
    //        rptAccountDetails.DataSource = DsValue.Tables[3];
    //        rptAccountDetails.DataBind();
    //        //Set Period
    //        if (DsValue.Tables[0].Rows.Count > 0)
    //        {
    //            string sMonth = DateTime.Now.ToString("MM");
    //            lblTargetUtilisation.Text = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(sMonth) / 12) * 100), 0) + " %";
    //        }
    //        else
    //        {
    //            // LblNoRow.Text = "No Records Found ";
    //        }
    //    }
    //    else
    //    {
    //        //LblNoRow.Text = "No Records Found ";
    //    }
    //}
    //public void Search()
    //{
    //    if (Common.CastAsInt32(ddlReportLevel.SelectedValue) == 1)
    //    {
    //        divGeneralSummary.Visible = true;
    //        divBudgetSummary.Visible = false;
    //        divAccountSummary.Visible = false;
    //        divAccountDetails.Visible = false;
    //        GeneralSummary();
    //    }
    //    else if (Common.CastAsInt32(ddlReportLevel.SelectedValue) == 2)
    //    {
    //        divGeneralSummary.Visible = false;
    //        divBudgetSummary.Visible = true;
    //        divAccountSummary.Visible = false;
    //        divAccountDetails.Visible = false;
    //        BudgetSummary();
    //    }
    //    else if (Common.CastAsInt32(ddlReportLevel.SelectedValue) == 3)
    //    {
    //        divGeneralSummary.Visible = false;
    //        divBudgetSummary.Visible = false;
    //        divAccountSummary.Visible = true;
    //        divAccountDetails.Visible = false;
            
    //        AccountSummary();
    //    }
    //    else if (Common.CastAsInt32(ddlReportLevel.SelectedValue) == 4)
    //    {
    //        divGeneralSummary.Visible = false;
    //        divBudgetSummary.Visible = false;
    //        divAccountSummary.Visible = false;
    //        divAccountDetails.Visible = true;
    //        AccountSummaryDetails();
    //    }
    //}
    //public void BindYear()
    //{
    //    //ddlyear.Items.Add(new ListItem("Select","0"));
    //    for (int i = System.DateTime.Now.Year, j = 1; ; i--, j++)
    //    {
    //        ddlyear.Items.Add(i.ToString());
    //        if (j == 11)
    //            break;
    //    }
    //}
    
    public void BindCompany()
    {
        string sql = "selECT cmp.Company, cmp.[Company Name] as CompanyName, cmp.Active, cmp.InAccts FROM vw_sql_tblSMDPRCompany cmp WHERE (((cmp.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            if (DtCompany.Rows.Count > 0)
            {
                ddlCompany.DataSource = DtCompany;
                ddlCompany.DataTextField = "Company";
                ddlCompany.DataValueField = "Company";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("<Select>", ""));
                BindddlYear();
            }
        }

    }
    public void BindVessel(string Comp)
    {
        string sql = "SELECT vsl.ShipID, vsl.ShipName, vsl.Company, vsl.VesselNo, vsl.Active FROM vw_sql_tblSMDPRVessels vsl WHERE (((vsl.Active)='A')) and vsl.Company='" + Comp + "' and VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+")";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlVessel.DataSource = DtVessel;
            ddlVessel.DataTextField = "ShipName";
            ddlVessel.DataValueField = "ShipID";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("< Select >", ""));
        }

    }
    public string SetColorForVPer(object val)
    {
        if (Convert.ToDecimal(val) >= 0)
            return "error_msg";
        else
            return "";
    }
    public string SetColorForYearPer(object val)
    {
        if (Convert.ToDecimal(val) >= 100)
            return "error_msg";
        else
            return "";
    }
    private void ClearSession()
    {
        Session["ReportingAnalysis"] = null;
    }

    public string getMidCategory(string Company, string vessel, string Year, int MAJCATID)
    {
        int Month = DateTime.Today.Month;
        DataSet DsValue = GetTrackingData(Company, Year, vessel);

        DataTable Dt = DsValue.Tables[1];
        DataView DV = Dt.DefaultView;
        DV.RowFilter = "MAJCATID=" + MAJCATID.ToString();
        //-------------------
        rptMidCats.DataSource = DV.ToTable();
        rptMidCats.DataBind();
        StringBuilder sb = new StringBuilder();
        StringWriter TW = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(TW);
        rptMidCats.RenderControl(hw);
        ////----------------------
        //rptMidCats.DataSource = null;
        //rptMidCats.DataBind();

        return sb.ToString();
    }
    public string getMinCategory(string Company, string vessel, string Year, int MAJCATID, int MIDCATID)
    {

        //System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Common.ConnectionString);
        //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec getVarianceRepport_vsl_BudgetTracking '" + Company + "'," + DateTime.Today.Month + "," + lblBudgetYear.Text + ",'" + vessel + "'", con); //???????
        //cmd.CommandTimeout = 300;
        //cmd.CommandType = CommandType.Text;

        //System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
        //adp.SelectCommand = cmd;
        //DataSet DsValue = new DataSet();
        //adp.Fill(DsValue);
        int Month = DateTime.Today.Month;
        DataSet DsValue = GetTrackingData(Company, Year, vessel);

        DataTable Dt = DsValue.Tables[2];
        DataView DV = Dt.DefaultView;
        DV.RowFilter = "MAJCATID=" + MAJCATID.ToString() + " and MIDCATID=" + MIDCATID.ToString();
        //-------------------
        rptMinCats.DataSource = DV.ToTable();
        rptMinCats.DataBind();
        StringBuilder sb = new StringBuilder();
        StringWriter TW = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(TW);
        rptMinCats.RenderControl(hw);
        ////----------------------
        //rptMidCats.DataSource = null;
        //rptMidCats.DataBind();

        return sb.ToString();
    }
    public string getAccounts(string Company, string vessel, string Year, int MAJCATID, int MIDCATID, int MINCATID)
    {

        //System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Common.ConnectionString);
        //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec getVarianceRepport_vsl_BudgetTracking '" + Company + "'," + DateTime.Today.Month + "," + lblBudgetYear.Text + ",'" + vessel + "'", con); //???????
        //cmd.CommandTimeout = 300;
        //cmd.CommandType = CommandType.Text;

        //System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
        //adp.SelectCommand = cmd;
        //DataSet DsValue = new DataSet();
        //adp.Fill(DsValue);
        int Month = DateTime.Today.Month;
        DataSet DsValue = GetTrackingData(Company, Year, vessel);

        DataTable Dt = DsValue.Tables[3];
        DataView DV = Dt.DefaultView;
        DV.RowFilter = "MAJCATID=" + MAJCATID.ToString() + " and MIDCATID=" + MIDCATID.ToString() + " and MINCATID=" + MINCATID.ToString();
        //-------------------
        rptAccounts.DataSource = DV.ToTable();
        rptAccounts.DataBind();
        StringBuilder sb = new StringBuilder();
        StringWriter TW = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(TW);
        rptAccounts.RenderControl(hw);
        ////----------------------
        //rptMidCats.DataSource = null;
        //rptMidCats.DataBind();

        return sb.ToString();
    }

    public DataSet GetTrackingData(string Company, string BudgetYear, string VesselCode)
    {
        DataSet DS = new DataSet();
        //if (Cache["TrackingDatas"] == null)        
        if (true)
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec Sp_GetMonthwiseVariantReport_VSL_BudgetTracking '" + Company + "','" + VesselCode + "','" + BudgetYear + "'", con);
            cmd.CommandTimeout = 300;
            cmd.CommandType = CommandType.Text;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
            adp.SelectCommand = cmd;
            adp.Fill(DS);
            Cache["TrackingDatas"] = DS;
        }
        else
        {
            DS = (DataSet)(Cache["TrackingDatas"]);
        }
        return DS;
    }
}