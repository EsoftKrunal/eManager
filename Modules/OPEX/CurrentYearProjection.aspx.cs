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
using iTextSharp.text;
using iTextSharp.text.pdf;
using FreeTextBoxControls;
public partial class Modules_OPEX_CurrentYearProjection : System.Web.UI.Page
{
    static Random R = new Random();
    AuthenticationManager authRecInv;
    public DataTable FleetSummary
    {
        set
        {
            Cache.Add("FS", value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 2, 0), System.Web.Caching.CacheItemPriority.Normal, null);
        }
        get
        {
            if (Cache["FS"] == null)
            {
                return BindFleetView(false);
            }
            else
            {
                return (DataTable)Cache["FS"];
            }
        }
    }
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    public void Manage_Menu()
    {
        AuthenticationManager auth = new AuthenticationManager(27, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trCurrBudget.Visible = auth.IsView;
        //auth = new AuthenticationManager(28, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trAnalysis.Visible = auth.IsView;
        //auth = new AuthenticationManager(29, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trBudgetForecast.Visible = auth.IsView;
        //auth = new AuthenticationManager(30, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trPublish.Visible = auth.IsView;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(271, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                Response.Redirect("~/NoPermissionBudget.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermissionBudget.aspx?Message=" + ex.Message);
        }

        #endregion ----------------------------------------
        if (!IsPostBack)
        {
          
            // btnCurrentYearProjection.CssClass = "selbtn";

            btnEdit.Visible = authRecInv.IsUpdate;
            Print.Visible = authRecInv.IsPrint;
            Manage_Menu();
            lblBudgetYear.Text = Convert.ToString(System.DateTime.Now.Year + 1);  //$$$$ remove - 1
            lblYr.Text = " " + System.DateTime.Now.Year.ToString();
            lblYr1.Text = " " + System.DateTime.Now.Year.ToString();
            lblYr3.Text = " " + System.DateTime.Now.Year.ToString();
            lblYrCurrCY.Text = " " + System.DateTime.Now.Year.ToString();
            lblYrCurrCY1.Text = " " + System.DateTime.Now.Year.ToString();
            lblYrCurrCY2.Text = " " + System.DateTime.Now.Year.ToString();

            lblYr1_.Text = Convert.ToString(System.DateTime.Now.Year + 1);
            lblYrNext.Text = " " + lblBudgetYear.Text;

            lblsummary.Text = "Year : " + Convert.ToString(System.DateTime.Now.Year + 1) + " [ " + (DateTime.Parse("31-Dec-" + (DateTime.Today.Year + 1).ToString()).Subtract(DateTime.Parse("1-Jan-" + (DateTime.Today.Year + 1).ToString())).Days + 1).ToString() + " ] days.";

            // Current Year Projection

            lblBudgetYearCYP.Text = Convert.ToString(System.DateTime.Now.Year);  //$$$$ remove - 1
            lblYrCYP.Text = " " + Convert.ToString(System.DateTime.Now.Year);
            lblYr1CYP.Text = " " + Convert.ToString(System.DateTime.Now.Year);
            lblYr3CYP.Text = " " + Convert.ToString(System.DateTime.Now.Year);
            lblYrCurrCYP.Text = Convert.ToString(System.DateTime.Now.Year);
            ddlMonthCYP.SelectedValue = DateTime.Today.Month.ToString();

            BindFleet();
            BindCompany();
            BindRepeater();


            PanelCurrentYP.Visible = true;
            pnl_Vessel.Visible = false;
            pnl_Fleet.Visible = false;
            
        }
    }
    protected void Comment_Click(object sender, EventArgs e)
    {
        ImageButton immgb = ((ImageButton)sender);
        string MidcatId = immgb.CommandArgument;
        if (ddlCompany.SelectedIndex > 0 && ddlShip.SelectedIndex > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('ForeCastComments.aspx?Comp=" + ddlCompany.SelectedValue + "&Ship=" + ddlShip.SelectedValue + "&Fyear=" + lblBudgetYear.Text + "&MidCat=" + MidcatId + "&HeadName=" + immgb.ToolTip + "','','');", true);
        }
    }
    public string FormatCurrency(object InValue)
    {
        string StrIn = InValue.ToString();
        string OutValue = "";
        int Len = StrIn.Length;
        while (Len > 3)
        {
            if (OutValue.Trim() == "")
                OutValue = StrIn.Substring(Len - 3);
            else
                OutValue = StrIn.Substring(Len - 3) + "," + OutValue;

            StrIn = StrIn.Substring(0, Len - 3);
            Len = StrIn.Length;
        }
        OutValue = StrIn + "," + OutValue;
        if (OutValue.EndsWith(",")) { OutValue = OutValue.Substring(0, OutValue.Length - 1); }
        return OutValue;
    }
    protected void btnClose2_Click(object sender, EventArgs e)
    {
        dvUpdateBox.Visible = false;
    }
    protected void btnCloseActComm_Click(object sender, EventArgs e)
    {
        dvUpdateActComm.Visible = false;
    }

    // Button
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    protected void imgClear_Click(object sender, EventArgs e)
    {


    }
    protected void imgBudgetForcasting_Click(object sender, EventArgs e)
    {
        Response.Redirect("BudgetForecasting.aspx");
    }
    protected void imgReportingAndAnalysis_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportingAndAnalysis.aspx");
    }
    protected void Fleet_Select(object sender, EventArgs e)
    {
        radVesselView.Font.Bold = false;
        radFleetView.Font.Bold = true;

        pnl_Vessel.Visible = false;
        pnl_Fleet.Visible = true;

        ddlCompany_OnSelectedIndexChanged(sender, e);
    }
    protected void Vessel_Select(object sender, EventArgs e)
    {
        radVesselView.Font.Bold = true;
        radFleetView.Font.Bold = false;

        pnl_Vessel.Visible = true;
        pnl_Fleet.Visible = false;

        ddlShip_OnSelectedIndexChanged(sender, e);
    }
    protected void rdoList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoList.Items[0].Selected)
        {
            ddlFleet.Visible = true;
            ddlCompanyF.Visible = false;
            lblFleetOrCompany.Text = "Fleet";
        }
        else
        {
            ddlCompanyF.Visible = true;
            ddlFleet.Visible = false;
            lblFleetOrCompany.Text = "Company";
        }
    }
    // --- Current Year Projection
    protected double getNewProjected(float Budget, float ActComm)
    {
        double result = 0;

        return result;
    }
    protected void Update_Amount(object sender, EventArgs e)
    {
        ImageButton immgb = ((ImageButton)sender);
        string MidcatId = immgb.CommandArgument;
        ViewState["MidCat"] = MidcatId;
        TextBox txt = (TextBox)immgb.Parent.FindControl("txtProj");
        HiddenField hfdComm = (HiddenField)immgb.Parent.FindControl("hfdComm");
        dvUpdateBox.Visible = true;
        txtOldAmt.Text = txt.Text;
        txtNewAmt.Text = txt.Text;
        txtComments.Text = hfdComm.Value;
        txtNewAmt.Focus();
    }
    protected void Update_ActComm(object sender, EventArgs e)
    {
        ImageButton immgb = ((ImageButton)sender);
        string MidcatId = immgb.CommandArgument;
        ViewState["MidCat"] = MidcatId;
        TextBox txt = (TextBox)immgb.Parent.FindControl("txtActComm");
        HiddenField hfdComm = (HiddenField)immgb.Parent.FindControl("hfdComm");
        HiddenField hfdbud = (HiddenField)immgb.Parent.FindControl("hfdBud");
        ViewState["Budget"] = hfdbud.Value;

        dvUpdateActComm.Visible = true;
        txtOld1.Text = txt.Text;
        txtNew1.Text = txt.Text;
        txtComments1.Text = hfdComm.Value;
        txtNew1.Focus();
    }
    protected void btnSaveProjected_click(object sender, EventArgs e)
    {
        if (ddlCompanyCYP.SelectedIndex <= 0)
        {
            lblPMessage.Text = "Please Select Company";
            ddlCompanyCYP.Focus();
            return;
        }
        else if (ddlVesselCYP.SelectedIndex <= 0)
        {
            lblPMessage.Text = "Please Select Vessel.";
            ddlVesselCYP.Focus();
            return;
        }

        if (txtNewAmt.Text != txtOldAmt.Text)
        {
            if (txtComments.Text.Trim() == "")
            {
                lblPMessage.Text = "Please Enter Comments.";
                return;
            }
        }

        if (Common.CastAsInt32(txtNewAmt.Text) < 0)
            lblPMessage.Text = "Please Enter valid Amount.";
        else
        {

            string sql = "SELECT * FROM tblVesselMonthlyProjectedAmount WHERE COCODE='" + ddlCompanyCYP.SelectedValue + "' AND VESSEL='" + ddlVesselCYP.SelectedValue + "' AND MIDCATID=" + ViewState["MidCat"].ToString() + " AND PYEAR=" + lblBudgetYearCYP.Text + " AND PMONTH=" + ddlMonthCYP.SelectedValue;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt.Rows.Count > 0)
                sql = "UPDATE tblVesselMonthlyProjectedAmount SET PROJECTEDAMOUNT=" + txtNewAmt.Text + ",COMMENTS='" + txtComments.Text.Trim().Replace("'", "`") + "' WHERE COCODE='" + ddlCompanyCYP.SelectedValue + "' AND VESSEL='" + ddlVesselCYP.SelectedValue + "' AND MIDCATID=" + ViewState["MidCat"].ToString() + " AND PYEAR=" + lblBudgetYearCYP.Text + " AND PMONTH=" + ddlMonthCYP.SelectedValue;
            else
                sql = "INSERT INTO tblVesselMonthlyProjectedAmount(COCODE,VESSEL,PYEAR,PMONTH,MIDCATID,PROJECTEDAMOUNT,COMMENTS) VALUES('" + ddlCompanyCYP.SelectedValue + "','" + ddlVesselCYP.SelectedValue + "'," + lblBudgetYearCYP.Text + "," + ddlMonthCYP.SelectedValue + "," + ViewState["MidCat"].ToString() + "," + txtNewAmt.Text + ",'" + txtComments.Text.Trim().Replace("'", "`") + "')";
            try
            {
                Common.Execute_Procedures_Select_ByQuery(sql);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "p", "alert('Comment saved successfully.');", true);
                BindRepeaterCYP();
            }
            catch { }

            dvUpdateBox.Visible = false;

        }
    }
    protected void btnSaveActComm_click(object sender, EventArgs e)
    {
        if (ddlCompanyCYP.SelectedIndex <= 0)
        {
            lblPMessage.Text = "Please Select Company";
            ddlCompanyCYP.Focus();
            return;
        }
        else if (ddlVesselCYP.SelectedIndex <= 0)
        {
            lblPMessage.Text = "Please Select Vessel.";
            ddlVesselCYP.Focus();
            return;
        }

        //if (txtNewAmt.Text != txtOldAmt.Text)
        //{
        //    if (txtComments.Text.Trim() == "")
        //    {
        //        lblPMessage.Text = "Please Enter Comments.";
        //        return;
        //    }
        //}

        if (Common.CastAsInt32(txtNew1.Text) < 0)
            lblPMessage1.Text = "Please Enter valid Amount.";
        else
        {

            string sql = "SELECT * FROM tblVesselMonthlyActCommAmount WHERE COCODE='" + ddlCompanyCYP.SelectedValue + "' AND VESSEL='" + ddlVesselCYP.SelectedValue + "' AND MIDCATID=" + ViewState["MidCat"].ToString() + " AND PYEAR=" + lblBudgetYearCYP.Text + " AND PMONTH=" + ddlMonthCYP.SelectedValue;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt.Rows.Count > 0)
                sql = "UPDATE tblVesselMonthlyActCommAmount SET ACTCOMM=" + txtNew1.Text + ",COMMENTS='" + txtComments1.Text.Trim().Replace("'", "`") + "' WHERE COCODE='" + ddlCompanyCYP.SelectedValue + "' AND VESSEL='" + ddlVesselCYP.SelectedValue + "' AND MIDCATID=" + ViewState["MidCat"].ToString() + " AND PYEAR=" + lblBudgetYearCYP.Text + " AND PMONTH=" + ddlMonthCYP.SelectedValue;
            else
                sql = "INSERT INTO tblVesselMonthlyActCommAmount(COCODE,VESSEL,PYEAR,PMONTH,MIDCATID,ACTCOMM,COMMENTS) VALUES('" + ddlCompanyCYP.SelectedValue + "','" + ddlVesselCYP.SelectedValue + "'," + lblBudgetYearCYP.Text + "," + ddlMonthCYP.SelectedValue + "," + ViewState["MidCat"].ToString() + "," + txtNew1.Text + ",'" + txtComments1.Text.Trim().Replace("'", "`") + "')";
            try
            {
                Common.Execute_Procedures_Select_ByQuery(sql);
            }
            catch { }

            //----------------------
            decimal Bud = Common.CastAsDecimal(ViewState["Budget"]);
            decimal NewProjected = 0;
            DataTable dtProj = Common.Execute_Procedures_Select_ByQuery("select [dbo].[getNewProjected]('" + ddlCompanyCYP.SelectedValue + "','" + ddlVesselCYP.SelectedValue + "'," + lblBudgetYearCYP.Text + "," + ddlMonthCYP.SelectedValue + "," + txtNew1.Text + "," + Bud.ToString() + ")");
            if (dtProj.Rows.Count > 0)
            {
                NewProjected = Common.CastAsInt32(dtProj.Rows[0][0]);
            }

            string sql1 = "SELECT * FROM tblVesselMonthlyProjectedAmount WHERE COCODE='" + ddlCompanyCYP.SelectedValue + "' AND VESSEL='" + ddlVesselCYP.SelectedValue + "' AND MIDCATID=" + ViewState["MidCat"].ToString() + " AND PYEAR=" + lblBudgetYearCYP.Text + " AND PMONTH=" + ddlMonthCYP.SelectedValue;
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql1);
            if (dt1.Rows.Count > 0)
                sql = "UPDATE tblVesselMonthlyProjectedAmount SET PROJECTEDAMOUNT=" + NewProjected.ToString() + ",COMMENTS='" + txtComments1.Text.Trim().Replace("'", "`") + "' WHERE COCODE='" + ddlCompanyCYP.SelectedValue + "' AND VESSEL='" + ddlVesselCYP.SelectedValue + "' AND MIDCATID=" + ViewState["MidCat"].ToString() + " AND PYEAR=" + lblBudgetYearCYP.Text + " AND PMONTH=" + ddlMonthCYP.SelectedValue;
            else
                sql = "INSERT INTO tblVesselMonthlyProjectedAmount(COCODE,VESSEL,PYEAR,PMONTH,MIDCATID,PROJECTEDAMOUNT,COMMENTS) VALUES('" + ddlCompanyCYP.SelectedValue + "','" + ddlVesselCYP.SelectedValue + "'," + lblBudgetYearCYP.Text + "," + ddlMonthCYP.SelectedValue + "," + ViewState["MidCat"].ToString() + "," + NewProjected.ToString() + ",'" + txtComments1.Text.Trim().Replace("'", "`") + "')";
            try
            {
                Common.Execute_Procedures_Select_ByQuery(sql);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "p", "alert('Comment saved successfully.');", true);
            }
            catch { }

            BindRepeaterCYP();

            //----------------------

            dvUpdateActComm.Visible = false;

        }
    }

    protected void btnSaveCYP_OnClick(object sender, EventArgs e)
    {
        foreach (RepeaterItem itm in rptCurrentYearProjectionHeades.Items)
        {
            TextBox txt = (TextBox)itm.FindControl("txtProj");
            HiddenField hfMajCatID = (HiddenField)itm.FindControl("hfMajCatID");

            Common.Set_Procedures("sp_InsertIntotblVesselMonthlyProjectedAmount");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
                    new MyParameter("@Cocode", ddlCompanyCYP.SelectedValue),
                    new MyParameter("@Vessel", ddlVesselCYP.SelectedValue),
                    new MyParameter("@PYear", lblBudgetYearCYP.Text),
                    new MyParameter("@PMonth", ddlMonthCYP.SelectedValue),
                    new MyParameter("@MidCatId", hfMajCatID.Value),
                    new MyParameter("@ProjectedAmount", Common.CastAsDecimal(txt.Text)),
                    new MyParameter("@Comments", "")
                );
            Boolean Res;
            DataSet DS = new DataSet();
            Res = Common.Execute_Procedures_IUD(DS);
        }

        foreach (RepeaterItem itm in rptCurrentYearProjectionValues.Items)
        {
            TextBox txt = (TextBox)itm.FindControl("txtProj");
            HiddenField hfMajCatID = (HiddenField)itm.FindControl("hfMajCatID");

            Common.Set_Procedures("sp_InsertIntotblVesselMonthlyProjectedAmount");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
                    new MyParameter("@Cocode", ddlCompanyCYP.SelectedValue),
                    new MyParameter("@Vessel", ddlVesselCYP.SelectedValue),
                    new MyParameter("@PYear", lblBudgetYearCYP.Text),
                    new MyParameter("@PMonth", ddlMonthCYP.SelectedValue),
                    new MyParameter("@MidCatId", hfMajCatID.Value),
                    new MyParameter("@ProjectedAmount", Common.CastAsDecimal(txt.Text)),
                    new MyParameter("@Comments", "")

                );
            Boolean Res;
            DataSet DS = new DataSet();
            Res = Common.Execute_Procedures_IUD(DS);
        }

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Record saved successfully.')", true);
    }

    protected void btnNextYearProjectionUC_OnClick(object sender, EventArgs e)
    {
        PanelCurrentYP.Visible = false;
        pnl_Vessel.Visible = false;
        pnl_Fleet.Visible = false;
       

        radVesselView.Visible = true;
        radFleetView.Visible = true;

       
    }
    protected void btnNextYearProjection_OnClick(object sender, EventArgs e)
    {
        PanelCurrentYP.Visible = false;
        pnl_Vessel.Visible = false;
       

        pnl_Fleet.Visible = true;

        radVesselView.Visible = true;
        radFleetView.Visible = true;

       
    }
    protected void btnCurrentYearProjection_OnClick(object sender, EventArgs e)
    {
        PanelCurrentYP.Visible = true;
        pnl_Vessel.Visible = false;
        pnl_Fleet.Visible = false;
        

        radVesselView.Visible = false;
        radFleetView.Visible = false;

      
    }

    protected void ddlCompanyCYP_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselCYP();
        BindRepeaterCYP();
        ShowPublishedByOn();
    }
    protected void ddlMonthCYP_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeaterCYP();
        ShowPublishedByOn();
    }
    protected void btnExportCYP_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('Print.aspx?Mode=Export&Month=" + ddlMonthCYP.SelectedValue + "&VessCode=" + ddlVesselCYP.SelectedValue + "&Company=" + ddlCompanyCYP.SelectedItem.Text + "&Vessel=" + ddlVesselCYP.SelectedItem.Text + "&CurrentYearProjection=1', '', '');", true);
        ShowPublishedByOn();
    }
    protected void Print_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('Print.aspx?Company=" + ddlCompanyCYP.SelectedItem.Text + "&Month=" + ddlMonthCYP.SelectedValue + "&Vessel=" + ddlVesselCYP.SelectedItem.Text + "&CurrentYearProjection=1', '', '');", true);
    }

    //protected void txtProj_OnTextChanged(object sender, EventArgs e)
    //{
    //    decimal Total = 0;
    //    foreach (RepeaterItem itm in rptCurrentYearProjectionHeades.Items)
    //    {
    //        TextBox txt = (TextBox)itm.FindControl("txtProj");
    //        Total = Total + Common.CastAsDecimal(txt.Text);
    //    }
    //    lblProjectedCYP.Text = FormatCurrency(Total);

    //    decimal Val1 = Common.CastAsDecimal(lblProjectedCYP.Text);
    //    decimal Val2 = Common.CastAsDecimal(lblBudgetCYP.Text);
    //    lblVar1CYP.Text = Math.Round(((Val1 - Val2) / Val2) * 100, 0).ToString();            
    //    //Math.Round(((ColumnSum3 - ColumnSum) / ColumnSum) * 100, 0).ToString();

    //    //------------------
    //    DateTime DateToday=System.DateTime.Today;
    //    DateTime DateNextYear=DateToday.AddYears(1);

    //    TimeSpan TD=DateNextYear.Subtract(DateToday);
    //    lblProjected1CYP.Text =  Convert.ToString(Math.Round(  Common.CastAsDecimal(lblProjectedCYP.Text) / (TD.Days+1),0 ) );

    //    // Show Values for II repeater
    //    decimal Total1=0;
    //    foreach (RepeaterItem itm in rptCurrentYearProjectionValues.Items)
    //    {
    //        TextBox txt = (TextBox)itm.FindControl("txtProj");
    //        Total1 = Total1 + Common.CastAsDecimal(txt.Text);
    //    }
    //    lblProj_TotalCYP.Text = FormatCurrency( Total1 + Total);

    //    Val1 = Common.CastAsDecimal(lblProj_TotalCYP.Text);
    //    Val2 = Common.CastAsDecimal(lblBdg_TotalCYP.Text);

    //    lblVar1_TotalCYP.Text=Math.Round(((Val1 - Val2) / Val2) * 100, 0).ToString();
    //}
    protected void Comment_ClickCYP(object sender, EventArgs e)
    {
        ImageButton immgb = ((ImageButton)sender);
        string MidcatId = immgb.CommandArgument;
        TextBox txt = (TextBox)immgb.Parent.FindControl("txtProj");
        string val = "";
        try
        {
            val = txt.Text;
        }
        catch
        {
            val = lblProj_TotalCYP.Text;
        }
        //if(txt==null)
        //    val=txt.Text;
        //else
        //    val = lblProj_TotalCYP.Text;

        if (ddlCompanyCYP.SelectedIndex > 0 && ddlVesselCYP.SelectedIndex > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "window.open('ProjectedComments.aspx?Comp=" + ddlCompanyCYP.SelectedValue + "&Ship=" + ddlVesselCYP.SelectedValue + "&Fyear=" + lblBudgetYearCYP.Text + "&MidCat=" + MidcatId + "&HeadName=" + immgb.ToolTip + "&Proj=" + val + "','','');", true);
        }
    }
    public void BindVesselCYP()
    {
        string sql = "SELECT VW_sql_tblSMDPRVessels.ShipID, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels " +
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='" + ddlCompanyCYP.SelectedValue + "')) ";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlVesselCYP.DataSource = DtVessel;
            ddlVesselCYP.DataTextField = "ShipNameCode";
            ddlVesselCYP.DataValueField = "ShipID";
            ddlVesselCYP.DataBind();
            ddlVesselCYP.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
        }

    }
    protected void ddlVesselCYP_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeaterCYP();
        ShowPublishedByOn();
    }
    protected void BindRepeaterCYP()
    {
        int Bmonth = Common.CastAsInt32(ddlMonthCYP.SelectedValue);
        int BYear = Common.CastAsInt32(lblBudgetYearCYP.Text) + 1;
        string Qry = "select FORECAST from dbo.tblsmdbudgetforecastyear where cocode='" + ddlCompanyCYP.SelectedValue + "' AND YEAR=" + (BYear - 2).ToString() + " AND ACCOUNTNUMBER=5100 AND SHIPID='" + ddlVesselCYP.SelectedValue + "'";
        DataTable dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        int Amt_5100 = 0;
        if (dtheader.Rows.Count > 0)
        {
            Amt_5100 = Common.CastAsInt32(dtheader.Rows[0][0]);
        }
        //-------------------
        Qry = "select YearDays,approvedby,replace(convert(varchar,approvedon,106),' ','-') as ApprovedOn,UpdatedBy, " +
                           "Importedby,replace(convert(varchar,ImportedOn,106),' ','-') as ImportedOn,replace(convert(varchar,UpdatedOn,106),' ','-') as UpdatedOn," +
                           "replace(convert(varchar,vessstart,106),' ','-') as vessstart,replace(convert(varchar,vessend,106),' ','-') as vessend " +
                           "from dbo.tblsmdbudgetforecastyear where cocode='" + ddlCompanyCYP.SelectedValue + "' and shipid='" + ddlVesselCYP.SelectedValue + "' and [year]=" + (BYear - 1).ToString();
        dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        if (dtheader.Rows.Count > 0)
        {
            txtStartDate.Text = dtheader.Rows[0]["VessStart"].ToString();
            txtEndDate.Text = dtheader.Rows[0]["VessEnd"].ToString();
            lblDays.Text = dtheader.Rows[0]["YearDays"].ToString();
        }
        else
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            lblDays.Text = "";
        }

        //---------------------------

        DataTable dtPrint = new DataTable();
        dtPrint.Columns.Add("SNO");
        dtPrint.Columns.Add("RowType");
        dtPrint.Columns.Add("AccountHead");
        dtPrint.Columns.Add("ActComm", typeof(double));
        dtPrint.Columns.Add("Proj", typeof(double));
        dtPrint.Columns.Add("Bud", typeof(double));
        dtPrint.Columns.Add("Var1", typeof(double));
        dtPrint.Columns.Add("Comments");

        DataTable dt = new DataTable();
        DataTable dt_1 = new DataTable();
        dt.Columns.Add("MidCatId");
        dt.Columns.Add("AccountHead");
        dt.Columns.Add("ActComm");
        dt.Columns.Add("Proj");
        dt.Columns.Add("Bud");
        dt.Columns.Add("Fcast");
        dt.Columns.Add("Var1");
        dt.Columns.Add("Var2");
        dt.Columns.Add("Var3");
        dt.Columns.Add("Comments");
        //---------------------------
        dt_1.Columns.Add("MidCatId");
        dt_1.Columns.Add("AccountHead");
        dt_1.Columns.Add("ActComm");
        dt_1.Columns.Add("Proj");
        dt_1.Columns.Add("Bud");
        dt_1.Columns.Add("Fcast");
        dt_1.Columns.Add("Var1");
        dt_1.Columns.Add("Var2");
        dt_1.Columns.Add("Var3");
        dt_1.Columns.Add("Comments");
        //---------------------------
        string[] Names = { "Manning :", "Consumables :", "Lube Oils :", "Spare, Maintenance & Repair :", "General Expenses :", "Insurance :", "Management & Admin Fee:" };
        string[] Names1 = { "Damage / Repairs :", "Principle Controlled Expenses :", "Capital Expenditure :", "Dry Docking :", "Pre-Delivery Expenses :", "Pre-Delivery Mgmt Fees :" };
        int start = 2;

        decimal ColumnSum = 0, ColumnSumLast = 0;
        decimal ColumnSum2 = 0, ColumnSum3 = 0;
        //---------------------------
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + ddlVesselCYP.SelectedValue + "' AND year=" + (BYear - 1).ToString() + " order by days desc");
        DataTable dtDaysLast = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + ddlVesselCYP.SelectedValue + "' AND year=" + (BYear - 2).ToString() + " order by days desc");
        DataTable dtStartDate = Common.Execute_Procedures_Select_ByQueryCMS("select EffectiveDate from dbo.vessel where VesselCode='" + ddlVesselCYP.SelectedValue + "'");
        int ActCommDays = 1;

        int DaysCnt = 1;
        if (dtDays != null)
        {
            if (dtDays.Rows.Count > 0)
            {
                DaysCnt = Common.CastAsInt32(dtDays.Rows[0][0]);
            }
        }
        int DaysCntLast = 1;
        if (dtDaysLast != null)
        {
            if (dtDaysLast.Rows.Count > 0)
            {
                DaysCntLast = Common.CastAsInt32(dtDaysLast.Rows[0][0]);
            }
        }


        if (dtStartDate.Rows.Count > 0)
        {
            try
            {
                DateTime dtYearSt = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime dtMonthLast = new DateTime(DateTime.Today.Year, Common.CastAsInt32(ddlMonthCYP.SelectedValue), 1);

                dtMonthLast = dtMonthLast.AddMonths(1);
                dtMonthLast = dtMonthLast.AddDays(-1);

                DateTime dtVesselSt = Convert.ToDateTime(dtStartDate.Rows[0][0]);
                if (dtVesselSt.Year == dtMonthLast.Year)
                {
                    ActCommDays = dtMonthLast.Subtract(dtVesselSt).Days + 1;
                }
                else if (dtVesselSt.Year > DateTime.Today.Year)
                {
                    ActCommDays = 1;
                }
                else
                {
                    ActCommDays = dtMonthLast.Subtract(dtYearSt).Days + 1;
                }
            }
            catch
            {
            }
        }

        //--------------------------
        DataTable dtActCom_Proj = Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE] '" + ddlCompanyCYP.SelectedValue + "'," + Bmonth.ToString() + "," + (BYear - 1).ToString() + ",'" + ddlVesselCYP.SelectedValue + "'");
        DataView dv = dtActCom_Proj.DefaultView;
        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 ORDER BY MajSeqNo,MidSeqNo");
        //---------------------------
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            dv.RowFilter = "MidCatId=" + dtAccts.Rows[i][0].ToString();
            DataTable dt1 = dv.ToTable();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;
            Object Ret = GetCalProjected(dtAccts.Rows[i][0].ToString());
            Object RetActComm = GetCalActComm(dtAccts.Rows[i][0].ToString());

            if (i != 0) { Amt_5100 = 0; }
            if (Amt_5100 > 0)
            {
                // OLD LINE --------------------

                //if (RetActComm == null)
                //    ActComm = 0;
                //else
                //    ActComm = Common.CastAsInt32(RetActComm);

                // NEW LINE --------------------

                if (RetActComm == null)
                    ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                else
                    ActComm = Common.CastAsDecimal(RetActComm);

                ColumnSum2 += Common.CastAsInt32(ActComm);

                // NEW LINE END --------------

                //-----------------------
                if (Ret == null)
                    Projected = Amt_5100;
                else
                    Projected = Common.CastAsInt32(Ret);

                ColumnSum3 += Projected;
            }
            else
            {
                if (dt1.Rows.Count > 0)
                {

                    if (RetActComm == null)
                        ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                    else
                        ActComm = Common.CastAsDecimal(RetActComm);
                    //-----------------------
                    if (Ret == null)
                        Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
                    else
                        Projected = Common.CastAsInt32(Ret);

                    ColumnSum2 += Common.CastAsInt32(ActComm);
                    ColumnSum3 += Common.CastAsInt32(Projected);

                }
                else
                {
                    if (RetActComm == null)
                        ActComm = 0;
                    else
                        ActComm = Common.CastAsInt32(RetActComm);
                    //-----------------------
                    if (Ret == null)
                        Projected = 0;
                    else
                        Projected = Common.CastAsInt32(Ret);

                    ColumnSum3 += Common.CastAsInt32(Projected);
                }

            }

            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlVesselCYP.SelectedValue + "' AND year=" + (BYear - 2).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                else
                {
                    Budget = 0;
                }
            }
            else
            {
                Budget = 0;
            }

            DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlVesselCYP.SelectedValue + "' AND year=" + (BYear - 2).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShipLast != null)
            {
                if (dtShipLast.Rows.Count > 0)
                {
                    ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
                else
                {
                    ForeCast = 0;
                }
            }
            else
            {
                ForeCast = 0;
            }

            DataRow dr = dt.NewRow();
            dr["MidcatId"] = dtAccts.Rows[i][0].ToString();
            dr["AccountHead"] = Names[i];
            dr["ActComm"] = ActComm.ToString();
            dr["Proj"] = Projected.ToString();
            dr["Bud"] = Budget.ToString();
            dr["Fcast"] = ForeCast.ToString();

            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
                Budget = ((ForeCast - Budget) / Budget) * 100;
            else
                Budget = 0;

            start++;

            dr["Var1"] = Math.Round(ActComm, 0);
            dr["Var2"] = Math.Round(Budget, 0);
            dr["Var3"] = Math.Round(Projected, 0);
            dr["Comments"] = getComments(dtAccts.Rows[i][0].ToString());

            dt.Rows.Add(dr);

            //---- Print
            DataRow drp = dtPrint.NewRow();
            drp["SNO"] = start;
            drp["RowType"] = 0;
            drp["AccountHead"] = dr["AccountHead"];
            drp["ActComm"] = dr["ActComm"];
            drp["Proj"] = dr["Proj"];
            drp["Bud"] = dr["Bud"];
            drp["Var1"] = Math.Round(ActComm, 0);
            drp["Comments"] = dr["Comments"];
            dtPrint.Rows.Add(drp);
        }

        //----------------------------------------------
        lblActComm_SumCYP.Text = FormatCurrency(ColumnSum2);
        lblProjectedCYP.Text = FormatCurrency(ColumnSum3);
        lblBudgetCYP.Text = FormatCurrency(ColumnSum);

        if (ColumnSum > 0)
            lblVar1CYP.Text = Math.Round(((ColumnSum3 - ColumnSum) / ColumnSum) * 100, 0).ToString();
        else
            lblVar1CYP.Text = "";

        DataRow drps = dtPrint.NewRow();
        drps["SNO"] = start;
        drps["RowType"] = 1;
        drps["AccountHead"] = "Total (US$):";
        drps["ActComm"] = ColumnSum2;
        drps["Proj"] = ColumnSum3;
        drps["Bud"] = ColumnSum;
        drps["Var1"] = Common.CastAsDecimal(lblVar1CYP.Text);
        drps["Comments"] = "";
        dtPrint.Rows.Add(drps);

        start++;

        rptCurrentYearProjectionHeades.DataSource = dt;
        rptCurrentYearProjectionHeades.DataBind();
        //----------------------------------------------
        lblActComm_Sum1CYP.Text = FormatCurrency(Math.Round(ColumnSum2 / ActCommDays, 0));
        lblProjected1CYP.Text = FormatCurrency(Math.Round(ColumnSum3 / DaysCntLast, 0));
        lblBudget1CYP.Text = FormatCurrency(Math.Round(ColumnSum / DaysCntLast, 0));

        DataRow drps1 = dtPrint.NewRow();
        drps1["SNO"] = start;
        drps1["RowType"] = 1;
        drps1["AccountHead"] = "Avg Daily Cost(US$):";
        drps1["ActComm"] = lblActComm_Sum1CYP.Text;
        drps1["Proj"] = lblProjected1CYP.Text;
        drps1["Bud"] = lblBudget1CYP.Text;
        drps1["Var1"] = Common.CastAsDecimal(lblVar1CYP.Text);
        drps1["Comments"] = "";
        dtPrint.Rows.Add(drps1);

        start++;

        //----------------------------------------------
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            dv.RowFilter = "MidCatId=" + dtAccts1.Rows[i][0].ToString();
            DataTable dt1 = dv.ToTable();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;
            Object Ret = GetCalProjected(dtAccts1.Rows[i][0].ToString());
            Object RetActComm = GetCalActComm(dtAccts1.Rows[i][0].ToString());

            if (dt1.Rows.Count > 0)
            {
                if (RetActComm == null)
                    ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                else
                    ActComm = Common.CastAsInt32(RetActComm);
                //----------------------------
                if (Ret == null)
                    Projected = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                else
                    Projected = Common.CastAsInt32(Ret);

                ColumnSum2 += Common.CastAsInt32(ActComm);
                ColumnSum3 += Common.CastAsInt32(Projected);

            }
            else
            {
                if (RetActComm == null)
                    ActComm = 0;
                else
                    ActComm = Common.CastAsInt32(RetActComm);

                if (Ret == null)
                    Projected = 0;
                else
                    Projected = Common.CastAsInt32(Ret);

                ColumnSum3 += Common.CastAsInt32(Projected);
            }

            //if (dt1.Rows.Count > 0)
            //{
            //    if (RetActComm == null)
            //        ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
            //    else
            //        ActComm = Common.CastAsInt32(RetActComm);

            //    Projected = ActComm;

            //    ColumnSum2 += Common.CastAsInt32(ActComm);
            //    ColumnSum3 += Common.CastAsInt32(Projected);

            //}
            //else
            //{
            //    if (RetActComm == null)
            //        ActComm = 0;
            //    else
            //        ActComm = Common.CastAsInt32(RetActComm);

            //    Projected = 0;              
            //    ColumnSum3 += Common.CastAsInt32(Projected);
            //}


            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlVesselCYP.SelectedValue + "' AND year=" + (BYear - 2).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                else
                {
                    Budget = 0;
                }
            }
            else
            {
                Budget = 0;
            }

            DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlVesselCYP.SelectedValue + "' AND year=" + (BYear - 2).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShipLast != null)
            {
                if (dtShipLast.Rows.Count > 0)
                {
                    ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
                else
                {
                    ForeCast = 0;
                }
            }
            else
            {
                ForeCast = 0;
            }

            DataRow dr = dt_1.NewRow();
            dr["MidcatId"] = dtAccts1.Rows[i][0].ToString();
            dr["AccountHead"] = Names1[i];
            dr["ActComm"] = ActComm.ToString();
            dr["Proj"] = Projected.ToString();
            dr["Bud"] = Budget.ToString();
            dr["Fcast"] = ForeCast.ToString();

            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
                Budget = ((ForeCast - Budget) / Budget) * 100;
            else
                Budget = 0;

            start++;

            dr["Var1"] = Math.Round(ActComm, 0);
            dr["Var2"] = Math.Round(Budget, 0);
            dr["Var3"] = Math.Round(Projected, 0);
            dr["Comments"] = getComments(dtAccts1.Rows[i][0].ToString());
            dt_1.Rows.Add(dr);

            rptCurrentYearProjectionValues.DataSource = dt_1;
            rptCurrentYearProjectionValues.DataBind();

            //---- Print
            DataRow drp = dtPrint.NewRow();
            drp["SNO"] = start;
            drp["RowType"] = 0;
            drp["AccountHead"] = dr["AccountHead"];
            drp["ActComm"] = dr["ActComm"];
            drp["Proj"] = dr["Proj"];
            drp["Bud"] = dr["Bud"];
            drp["Var1"] = Math.Round(ActComm, 0);
            drp["Comments"] = dr["Comments"];
            dtPrint.Rows.Add(drp);
        }
        //----------------------------------------------
        lblActComm_TotalCYP.Text = FormatCurrency(ColumnSum2);
        lblProj_TotalCYP.Text = FormatCurrency(ColumnSum3);
        lblBdg_TotalCYP.Text = FormatCurrency(ColumnSum);

        if (ColumnSum > 0)
            lblVar1_TotalCYP.Text = Math.Round(((ColumnSum3 - ColumnSum) / ColumnSum) * 100, 0).ToString();
        else
            lblVar1_TotalCYP.Text = "";


        drps = dtPrint.NewRow();
        drps["SNO"] = start;
        drps["RowType"] = 1;
        drps["AccountHead"] = "Gross Total (US$):";
        drps["ActComm"] = ColumnSum2;
        drps["Proj"] = ColumnSum3;
        drps["Bud"] = ColumnSum;
        drps["Var1"] = Common.CastAsDecimal(lblVar1_TotalCYP.Text);
        drps["Comments"] = "";
        dtPrint.Rows.Add(drps);

        Session.Add("sDT", dtPrint);
    }
    protected string getComments(string MidCatID)
    {
        string sql = "SELECT Comments FROM dbo.tblVesselMonthlyProjectedAmount WHERE Cocode='" + ddlCompanyCYP.SelectedValue + "' AND Vessel='" + ddlVesselCYP.SelectedValue + "' AND PYear=" + lblBudgetYearCYP.Text + " AND PMonth=" + ddlMonthCYP.SelectedValue + " AND MidCatId=" + MidCatID + "";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
            return Dt.Rows[0][0].ToString();
        else
            return "";
    }
    public object GetCalProjected(string MidCatID)
    {
        string sql = "SELECT ProjectedAmount FROM dbo.tblVesselMonthlyProjectedAmount WHERE Cocode='" + ddlCompanyCYP.SelectedValue + "' AND Vessel='" + ddlVesselCYP.SelectedValue + "' AND PYear=" + lblBudgetYearCYP.Text + " AND PMonth=" + ddlMonthCYP.SelectedValue + " AND MidCatId=" + MidCatID + "";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
            return Dt.Rows[0][0];
        else
            return null;
    }
    public object GetCalActComm(string MidCatID)
    {
        string sql = "SELECT ActComm FROM dbo.tblVesselMonthlyActCommAmount WHERE Cocode='" + ddlCompanyCYP.SelectedValue + "' AND Vessel='" + ddlVesselCYP.SelectedValue + "' AND PYear=" + lblBudgetYearCYP.Text + " AND PMonth=" + ddlMonthCYP.SelectedValue + " AND MidCatId=" + MidCatID + "";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
            return Dt.Rows[0][0];
        else
            return null;
    }
    public void ShowPublishedByOn()
    {
        string sql = "select * from RuningCostProjection  WHERE VesselCode='" + ddlVesselCYP.SelectedValue + "' AND PYear=" + Common.CastAsInt32(lblBudgetYearCYP.Text) + " AND PMonth=" + Common.CastAsInt32(ddlMonthCYP.SelectedValue) + "";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        if (Dt.Rows.Count > 0)
        {
            lblPublishedByOn.Text = "Published By/On : " + Dt.Rows[0]["PublishedBy"].ToString() + " / " + Convert.ToDateTime(Dt.Rows[0]["PublishedOn"]).ToString("dd-MMM-yyyy");
        }
        else
        {
            lblPublishedByOn.Text = "";
        }
    }

    // VESSEL SECTION
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
        BindRepeater();
    }
    public void ddlShip_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    public void BindCompany()
    {
        string sql = "SELECT VW_sql_tblSMDPRCompany.Company, VW_sql_tblSMDPRCompany.ReportCo " +
            " ,(VW_sql_tblSMDPRCompany.Company + '-' + VW_sql_tblSMDPRCompany.[Company Name]) as CompName" +
        " FROM VW_sql_tblSMDPRCompany WHERE (((VW_sql_tblSMDPRCompany.InAccts)=1)) and (((VW_sql_tblSMDPRCompany.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            ddlCompany.DataSource = DtCompany;
            ddlCompany.DataTextField = "CompName";
            ddlCompany.DataValueField = "Company";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
            ddlCompany.SelectedIndex = 0;
            BindVessel();

            ddlCompanyF.DataSource = DtCompany;
            ddlCompanyF.DataTextField = "CompName";
            ddlCompanyF.DataValueField = "Company";
            ddlCompanyF.DataBind();
            ddlCompanyF.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
            ddlCompanyF.SelectedIndex = 0;
            BindVessel();

            ddlCompanyCYP.DataSource = DtCompany;
            ddlCompanyCYP.DataTextField = "CompName";
            ddlCompanyCYP.DataValueField = "Company";
            ddlCompanyCYP.DataBind();
            ddlCompanyCYP.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
            ddlCompanyCYP.SelectedIndex = 0;
            BindVessel();
        }

    }
    public void BindVessel()
    {
        string sql = "SELECT VW_sql_tblSMDPRVessels.ShipID, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels " +
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='" + ddlCompany.SelectedValue + "')) ";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlShip.DataSource = DtVessel;
            ddlShip.DataTextField = "ShipNameCode";
            ddlShip.DataValueField = "ShipID";
            ddlShip.DataBind();
            ddlShip.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));

            ddlVesselCYP.DataSource = DtVessel;
            ddlVesselCYP.DataTextField = "ShipNameCode";
            ddlVesselCYP.DataValueField = "ShipID";
            ddlVesselCYP.DataBind();
            ddlVesselCYP.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
        }

    }
    protected void BindRepeater()
    {
        string Qry = "select FORECAST from dbo.tblsmdbudgetforecastyear where cocode='" + ddlCompany.SelectedValue + "' AND YEAR=" + (Common.CastAsInt32(lblBudgetYear.Text) - 2).ToString() + " AND ACCOUNTNUMBER=5100 AND SHIPID='" + ddlShip.SelectedValue + "'";
        DataTable dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        int Amt_5100 = 0;
        if (dtheader.Rows.Count > 0)
        {
            Amt_5100 = Common.CastAsInt32(dtheader.Rows[0][0]);
        }
        //-------------------
        Qry = "select YearDays,approvedby,replace(convert(varchar,approvedon,106),' ','-') as ApprovedOn,UpdatedBy, " +
                           "Importedby,replace(convert(varchar,ImportedOn,106),' ','-') as ImportedOn,replace(convert(varchar,UpdatedOn,106),' ','-') as UpdatedOn," +
                           "replace(convert(varchar,vessstart,106),' ','-') as vessstart,replace(convert(varchar,vessend,106),' ','-') as vessend " +
                           "from dbo.tblsmdbudgetforecastyear where cocode='" + ddlCompany.SelectedValue + "' and shipid='" + ddlShip.SelectedValue + "' and [year]=" + (Common.CastAsInt32(lblBudgetYear.Text) - 1).ToString();
        dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        if (dtheader.Rows.Count > 0)
        {
            txtStartDate.Text = dtheader.Rows[0]["VessStart"].ToString();
            txtEndDate.Text = dtheader.Rows[0]["VessEnd"].ToString();
            lblDays.Text = dtheader.Rows[0]["YearDays"].ToString();
        }
        else
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            lblDays.Text = "";
        }

        //---------------------------
        DataTable dt = new DataTable();
        DataTable dt_1 = new DataTable();
        dt.Columns.Add("MidCatId");
        dt.Columns.Add("AccountHead");
        dt.Columns.Add("ActComm");
        dt.Columns.Add("Proj");
        dt.Columns.Add("Bud");
        dt.Columns.Add("Fcast");
        dt.Columns.Add("Var1");
        dt.Columns.Add("Var2");
        dt.Columns.Add("Var3");
        dt.Columns.Add("Comments");
        //---------------------------
        dt_1.Columns.Add("MidCatId");
        dt_1.Columns.Add("AccountHead");
        dt_1.Columns.Add("ActComm");
        dt_1.Columns.Add("Proj");
        dt_1.Columns.Add("Bud");
        dt_1.Columns.Add("Fcast");
        dt_1.Columns.Add("Var1");
        dt_1.Columns.Add("Var2");
        dt_1.Columns.Add("Var3");
        dt_1.Columns.Add("Comments");
        //---------------------------
        string[] Names = { "Manning :", "Consumables :", "Lube Oils :", "Spare, Maintenance & Repair :", "General Expenses :", "Insurance :", "Management & Admin Fee:" };
        string[] Names1 = { "Damage / Repairs :", "Principle Controlled Expenses :", "Capital Expenditure :", "Dry Docking :" };
        int start = 2;

        decimal ColumnSum = 0, ColumnSumLast = 0;
        decimal ColumnSum2 = 0, ColumnSum3 = 0;
        //---------------------------
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + ddlShip.SelectedValue + "' AND year=" + (DateTime.Today.Year).ToString() + " order by days desc");
        DataTable dtDaysLast = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + ddlShip.SelectedValue + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " order by days desc");

        int DaysCnt = 1;
        if (dtDays != null)
        {
            if (dtDays.Rows.Count > 0)
            {
                DaysCnt = Common.CastAsInt32(dtDays.Rows[0][0]);
            }
        }
        int DaysCntLast = 1;
        if (dtDaysLast != null)
        {
            if (dtDaysLast.Rows.Count > 0)
            {
                DaysCntLast = Common.CastAsInt32(dtDaysLast.Rows[0][0]);
            }
        }
        //---------------------------
        DataTable dtActCom_Proj = Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE] '" + ddlCompany.SelectedValue + "'," + (DateTime.Today.Month).ToString() + "," + (DateTime.Today.Year).ToString() + ",'" + ddlShip.SelectedValue + "'");
        DataView dv = dtActCom_Proj.DefaultView;
        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 And MidcatId not in (25,26) ORDER BY MajSeqNo,MidSeqNo");
        //---------------------------
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            dv.RowFilter = "MidCatId=" + dtAccts.Rows[i][0].ToString();
            DataTable dt1 = dv.ToTable();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;

            if (i != 0) { Amt_5100 = 0; }
            if (Amt_5100 > 0)
            {
                ColumnSum3 += Amt_5100;

                ActComm = 0;
                Projected = Amt_5100;
            }
            else
            {
                if (dt1.Rows.Count > 0)
                {
                    ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                    ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

                    ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                    Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
                }
                else
                {
                    ActComm = 0;
                    Projected = 0;
                }
            }
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlShip.SelectedValue + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                else
                {
                    Budget = 0;
                }
            }
            else
            {
                Budget = 0;
            }

            DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlShip.SelectedValue + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShipLast != null)
            {
                if (dtShipLast.Rows.Count > 0)
                {
                    ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
                else
                {
                    ForeCast = 0;
                }
            }
            else
            {
                ForeCast = 0;
            }

            DataRow dr = dt.NewRow();
            dr["MidcatId"] = dtAccts.Rows[i][0].ToString();
            dr["AccountHead"] = Names[i];
            dr["ActComm"] = ActComm.ToString();
            dr["Proj"] = Projected.ToString();
            dr["Bud"] = Budget.ToString();
            dr["Fcast"] = ForeCast.ToString();

            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
                Budget = ((ForeCast - Budget) / Budget) * 100;
            else
                Budget = 0;

            //rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(ActComm, 0) + "%", "BudgetForecastReport_Summary.rpt");
            //rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(Projected, 0) + "%", "BudgetForecastReport_Summary.rpt");
            //rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(Budget, 0) + "%", "BudgetForecastReport_Summary.rpt");

            start++;

            dr["Var1"] = Math.Round(ActComm, 0);
            dr["Var2"] = Math.Round(Budget, 0);
            dr["Var3"] = Math.Round(Projected, 0);
            dt.Rows.Add(dr);
        }

        //----------------------------------------------
        lblActComm_Sum.Text = FormatCurrency(ColumnSum2);
        lblProjected.Text = FormatCurrency(ColumnSum3);
        lblBudget.Text = FormatCurrency(ColumnSum);
        lblForeCast.Text = FormatCurrency(ColumnSumLast);

        if (ColumnSum > 0)
            lblVar1.Text = Math.Round(((ColumnSum3 - ColumnSum) / ColumnSum) * 100, 0).ToString();
        else
            lblVar1.Text = "";

        if (ColumnSum > 0)
            lblVar2.Text = Math.Round(((ColumnSumLast - ColumnSum) / ColumnSum) * 100, 0).ToString();
        else
            lblVar2.Text = "";

        if (ColumnSum3 > 0)
            lblVar3.Text = Math.Round(((ColumnSumLast - ColumnSum3) / ColumnSum3) * 100, 0).ToString();
        else
            lblVar3.Text = "";

        start++;

        Repeater1.DataSource = dt;
        Repeater1.DataBind();
        //----------------------------------------------
        lblActComm_Sum1.Text = FormatCurrency(Math.Round(ColumnSum2 / DaysCntLast, 0));
        lblProjected1.Text = FormatCurrency(Math.Round(ColumnSum3 / DaysCntLast, 0));
        lblBudget1.Text = FormatCurrency(Math.Round(ColumnSum / DaysCntLast, 0));
        lblForeCast1.Text = FormatCurrency(Math.Round(ColumnSumLast / DaysCnt, 0));
        start++;
        //----------------------------------------------
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            dv.RowFilter = "MidCatId=" + dtAccts1.Rows[i][0].ToString();
            DataTable dt1 = dv.ToTable();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;

            if (dt1.Rows.Count > 0)
            {
                ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

                ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
            }
            else
            {
                ActComm = 0;
                Projected = 0;
            }
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlShip.SelectedValue + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                else
                {
                    Budget = 0;
                }
            }
            else
            {
                Budget = 0;
            }

            DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlShip.SelectedValue + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShipLast != null)
            {
                if (dtShipLast.Rows.Count > 0)
                {
                    ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
                else
                {
                    ForeCast = 0;
                }
            }
            else
            {
                ForeCast = 0;
            }

            DataRow dr = dt_1.NewRow();
            dr["MidcatId"] = dtAccts1.Rows[i][0].ToString();
            dr["AccountHead"] = Names1[i];
            dr["ActComm"] = ActComm.ToString();
            dr["Proj"] = Projected.ToString();
            dr["Bud"] = Budget.ToString();
            dr["Fcast"] = ForeCast.ToString();


            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
                Budget = ((ForeCast - Budget) / Budget) * 100;
            else
                Budget = 0;

            //rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(ActComm, 0) + "%", "BudgetForecastReport_Summary.rpt");
            //rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(Projected, 0) + "%", "BudgetForecastReport_Summary.rpt");
            //rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(Budget, 0) + "%", "BudgetForecastReport_Summary.rpt");

            start++;

            dr["Var1"] = Math.Round(ActComm, 0);
            dr["Var2"] = Math.Round(Budget, 0);
            dr["Var3"] = Math.Round(Projected, 0);
            dt_1.Rows.Add(dr);

            Repeater2.DataSource = dt_1;
            Repeater2.DataBind();

            //----------------------------------------------
            lblActComm_Total.Text = FormatCurrency(ColumnSum2);
            lblProj_Total.Text = FormatCurrency(ColumnSum3);
            lblBdg_Total.Text = FormatCurrency(ColumnSum);
            lblFcast_Total.Text = FormatCurrency(ColumnSumLast);

            if (ColumnSum > 0)
                lblVar1_Total.Text = Math.Round(((ColumnSum3 - ColumnSum) / ColumnSum) * 100, 0).ToString();
            else
                lblVar1_Total.Text = "";

            if (ColumnSum > 0)
                lblVar2_Total.Text = Math.Round(((ColumnSumLast - ColumnSum) / ColumnSum) * 100, 0).ToString();
            else
                lblVar2_Total.Text = "";

            if (ColumnSum3 > 0)
                lblVar3_Total.Text = Math.Round(((ColumnSumLast - ColumnSum3) / ColumnSum3) * 100, 0).ToString();
            else
                lblVar3_Total.Text = "";
        }
    }
    // FLEET SECTION
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFleet.SelectedIndex > 0) ddlCompanyF.SelectedIndex = 0;
        BindVesselBYFleet();
        BindFleetView(true);
    }
    protected void ddlFleetComp_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompanyF.SelectedIndex > 0) ddlFleet.SelectedIndex = 0;
        BindVesselBYOwner();
        BindFleetView(true);
    }
    public void BindFleet()
    {
        DataTable dtFleet = Common.Execute_Procedures_Select_ByQueryCMS("select * from FleetMaster");
        if (dtFleet != null)
        {
            if (dtFleet.Rows.Count >= 0)
            {
                ddlFleet.DataSource = dtFleet;
                ddlFleet.DataTextField = "FleetName";
                ddlFleet.DataValueField = "FleetID";
                ddlFleet.DataBind();
                ddlFleet.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< All >", "0"));
            }
        }
    }
    public void BindVesselBYOwner()
    {
        string sql = "SELECT VW_sql_tblSMDPRVessels.ShipID, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels " +
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='" + ddlCompanyF.SelectedValue + "')) AND ACTIVE='A'";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlVesselF.DataSource = DtVessel;
            ddlVesselF.DataTextField = "ShipNameCode";
            ddlVesselF.DataValueField = "ShipID";
            ddlVesselF.DataBind();
            ddlVesselF.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Select>", "0"));
        }

    }
    public void BindVesselBYFleet()
    {
        string sql = "";
        sql = "select VesselCode,vesselName from Vessel where fleetID=" + ddlFleet.SelectedValue + " And VesselStatusid<>2  order by vesselName ";
        DataTable dtFleet = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtFleet != null)
        {
            if (dtFleet.Rows.Count >= 0)
            {
                ddlVesselF.DataSource = dtFleet;
                ddlVesselF.DataTextField = "vesselName";
                ddlVesselF.DataValueField = "VesselCode";
                ddlVesselF.DataBind();
                ddlVesselF.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
            }
        }
    }
    protected DataTable BindFleetView(bool Show)
    {
        DataTable dt_Result = new DataTable();
        int[] ColumnSum;
        int[] VesselDays;
        ColumnSum = new int[ddlVesselF.Items.Count - 1];
        VesselDays = new int[ddlVesselF.Items.Count - 1];
        StringBuilder sb = new StringBuilder();
        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 ORDER BY MajSeqNo,MidSeqNo");
        sb.Append("<table border='1' width='100%' cellpadding='3' height='390' style='font-size:10px;'");
        sb.Append("<tr>");
        sb.Append("<td class='header' style='font-size:10px;'>Budget Head<br/>Year Built</td>");
        dt_Result.Columns.Add("BudgetHead");
        string Ships = "";
        for (int i = 0; i < ddlVesselF.Items.Count; i++)
        {
            Ships = Ships + ((Ships.Trim() == "") ? "" : ",") + ddlVesselF.Items[i].Value;
            if (i != 0)
            {
                sb.Append("<td class='header' style='font-size:10px;'><input type='radio' name='radVSL' value='" + ddlVesselF.Items[i].Value + "' id='rad" + ddlVesselF.Items[i].Value + "'><label for='rad" + ddlVesselF.Items[i].Value + "'>" + ddlVesselF.Items[i].Value + "</label>");
                DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + ddlVesselF.Items[i].Value + "' AND year=" + (DateTime.Today.Year).ToString() + " order by days desc");
                if (dtDays != null)
                {
                    if (dtDays.Rows.Count > 0)
                    {
                        VesselDays[i - 1] = Common.CastAsInt32(dtDays.Rows[0][0]);
                    }
                }

                DataTable dtYB = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt from dbo.vessel Where vesselcode='" + ddlVesselF.Items[i].Value + "'");
                if (dtYB != null)
                {
                    if (dtYB.Rows.Count > 0)
                    {
                        if (dtYB.Rows[0][0].ToString().Trim() != "")
                        {
                            sb.Append("</br> [" + dtYB.Rows[0][0].ToString() + "]");
                        }
                    }
                }

                sb.Append("</td>");
                dt_Result.Columns.Add(ddlVesselF.Items[i].Value);
            }
        }
        hfd_Ships.Value = Ships;
        sb.Append("<td class='header' style='font-size:10px;'>Total</td>");
        dt_Result.Columns.Add("Total");
        sb.Append("</tr>");
        // YEAR BUILT
        //-----------------
        DataRow dr_yb = dt_Result.NewRow();
        dr_yb[0] = "Year Built";
        for (int j = 0; j < ddlVesselF.Items.Count; j++)
        {
            if (j != 0)
            {
                DataTable dt_yb = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt from vessel where vesselcode='" + ddlVesselF.Items[j].Value + "'");
                if (dt_yb.Rows.Count > 0)
                {
                    dr_yb[ddlVesselF.Items[j].Value] = "";// "[ " + dt_yb.Rows[0][0].ToString() + " ]";
                }
            }
        }
        dr_yb[dt_Result.Columns.Count - 1] = "";
        dt_Result.Rows.Add(dr_yb);
        //-----------------
        // DATA ROWS 
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            DataRow dr = dt_Result.NewRow();
            sb.Append("<tr>");
            sb.Append("<td>" + dtAccts.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts.Rows[i][1].ToString();
            for (int j = 0; j < ddlVesselF.Items.Count; j++)
            {
                if (j != 0)
                {
                    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select NEXTYEARFORECASTAMOUNT from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlVesselF.Items[j].Value + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
                    if (dtShip != null)
                    {
                        if (dtShip.Rows.Count > 0)
                        {
                            sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                            ColumnSum[j - 1] += Common.CastAsInt32(dtShip.Rows[0][0]);
                            RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                            dr[ddlVesselF.Items[j].Value] = FormatCurrency(dtShip.Rows[0][0]);
                        }
                        else
                        {
                            sb.Append("<td></td>");
                            dr[ddlVesselF.Items[j].Value] = "0";
                        }
                    }
                    else
                    {
                        sb.Append("<td></td>");
                        dr[ddlVesselF.Items[j].Value] = "0";
                    }
                }
            }
            sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            dr[dt_Result.Columns.Count - 1] = FormatCurrency(RowSum);
            sb.Append("</tr>");
            dt_Result.Rows.Add(dr);
        }
        //---------------
        // TOTAL
        int VSLhaving_TotalMoreThan0 = 0;
        DataRow dr1 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Total(US$)</td>");
        dr1[0] = "Total(US$)";
        int GrossSum = 0;
        for (int i = 0; i < ddlVesselF.Items.Count; i++)
        {
            if (i != 0)
            {
                sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i - 1]) + "</td>");
                GrossSum += ColumnSum[i - 1];
                dr1[i] = FormatCurrency(ColumnSum[i - 1]);
                if (ColumnSum[i - 1] > 0)
                {
                    VSLhaving_TotalMoreThan0++;
                }
            }
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        dr1[dt_Result.Columns.Count - 1] = FormatCurrency(GrossSum);
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr1);

        // PER DAY CALC
        DataRow dr2 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Avg Daily Cost(US$)</td>");
        dr2[0] = "Avg Daily Cost(US$)";
        //int GrossSum = 0;
        for (int i = 0; i < ddlVesselF.Items.Count; i++)
        {
            if (i != 0)
            {
                try
                {
                    sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((ColumnSum[i - 1] / VesselDays[i - 1])) + "</td>");
                    dr2[i] = FormatCurrency((ColumnSum[i - 1] / VesselDays[i - 1]));
                }
                catch (DivideByZeroException ex)
                {
                    sb.Append("<td style='font-size:10px;text-align:right'>0</td>");
                    dr2[i] = "0";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //GrossSum += ColumnSum[i - 1];
            }
        }
        if (VSLhaving_TotalMoreThan0 > 0)
        {
            sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((GrossSum / 366) / VSLhaving_TotalMoreThan0) + "</td>");
            dr2[dt_Result.Columns.Count - 1] = FormatCurrency((GrossSum / 366) / VSLhaving_TotalMoreThan0);
        }
        else
        {
            sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((GrossSum / 366)) + "</td>");
            dr2[dt_Result.Columns.Count - 1] = FormatCurrency((GrossSum / 366));
        }
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr2);

        // DATA ROWS  - 2
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            DataRow dr = dt_Result.NewRow();
            sb.Append("<tr>");
            sb.Append("<td>" + dtAccts1.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts1.Rows[i][1].ToString();
            for (int j = 0; j < ddlVesselF.Items.Count; j++)
            {
                if (j != 0)
                {
                    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select NEXTYEARFORECASTAMOUNT from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlVesselF.Items[j].Value + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
                    if (dtShip != null)
                    {
                        if (dtShip.Rows.Count > 0)
                        {
                            sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                            ColumnSum[j - 1] += Common.CastAsInt32(dtShip.Rows[0][0]);
                            RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                            dr[ddlVesselF.Items[j].Value] = FormatCurrency(dtShip.Rows[0][0]);
                        }
                        else
                        {
                            sb.Append("<td></td>");
                            dr[ddlVesselF.Items[j].Value] = "0";
                        }
                    }
                    else
                    {
                        sb.Append("<td></td>");
                        dr[ddlVesselF.Items[j].Value] = "0";
                    }
                }
            }
            sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            sb.Append("</tr>");
            dr[dt_Result.Columns.Count - 1] = FormatCurrency(RowSum);
            dt_Result.Rows.Add(dr);
        }

        // GROSS TOTAL
        DataRow dr3 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Gross Total(US$)</td>");
        dr3[0] = "Gross Total(US$)";
        GrossSum = 0;
        for (int i = 0; i < ddlVesselF.Items.Count; i++)
        {
            if (i != 0)
            {
                sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i - 1]) + "</td>");
                GrossSum += ColumnSum[i - 1];
                dr3[i] = FormatCurrency(ColumnSum[i - 1]);
            }
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        dr3[dt_Result.Columns.Count - 1] = FormatCurrency(GrossSum);
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr3);
        sb.Append("</table>");
        //--------------
        if (Show)
            lit1.Text = sb.ToString();

        return dt_Result;
    }
    private void ExportToPDF(string Company, DataTable dt)
    {
        try
        {
            //Document document = new Document(PageSize.LETTER.Rotate, 10, 10, 30, 10);
            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 30, 10);

            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("MTMSM");
            document.AddSubject("Fleet Summary");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\MTMMLogo.jpg"));
            Chunk chk = new Chunk(logoImg, 0, 0, true);
            //Phrase p1 = new Phrase();
            //p1.Add(chk);

            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            tb_header.Width = 100;
            tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.BorderWidth = 0;
            tb_header.BorderColor = iTextSharp.text.Color.WHITE;
            tb_header.Cellspacing = 1;
            tb_header.Cellpadding = 1;

            Cell c1 = new Cell(chk);
            c1.HorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.AddCell(c1);

            Phrase p2 = new Phrase();
            p2.Add(new Phrase(Company + "\n" + "\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            //Chunk ch = new Chunk();

            HeaderFooter header = new HeaderFooter(new Phrase(""), false);
            document.Header = header;

            //header.Alignment = Element.ALIGN_LEFT;
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //'Adding Footer in document
            string foot_Txt = "";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------
            document.Open();
            document.Add(tb_header);
            //------------ TABLE HEADER FONT 
            iTextSharp.text.Font fCapText = FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_5 = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
            //------------ TABLE HEADER ROW 
            int ColumnsCount = dt.Columns.Count;
            iTextSharp.text.Table tb1 = new iTextSharp.text.Table(ColumnsCount);
            tb1.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb1.Width = 100;

            float[] ws = new float[ColumnsCount];
            ws[0] = 15;
            for (int i = 1; i <= ws.Length - 1; i++)
                ws[i] = 80 / (ws.Length - 1);

            ws[ws.Length - 1] = ws[ws.Length - 1] + 5;
            tb1.Widths = ws;

            tb1.Alignment = Element.ALIGN_CENTER;
            tb1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb1.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb1.BorderColor = iTextSharp.text.Color.WHITE;
            tb1.Cellspacing = 1;
            tb1.Cellpadding = 1;

            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                Cell tc = new Cell(new Phrase(dt.Columns[i].ColumnName, fCapText));
                tb1.AddCell(tc);
            }

            DataRow dr_yb = dt.Rows[0];
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                Cell tc = new Cell(new Phrase(dr_yb[i].ToString(), fCapText_5));
                tb1.AddCell(tc);
            }

            document.Add(tb1);
            //------------ TABLE DATA ROW 
            // data rows
            iTextSharp.text.Table tbdata = new iTextSharp.text.Table(ColumnsCount);
            tbdata.Width = 100;
            tbdata.Widths = ws;
            tbdata.Alignment = Element.ALIGN_CENTER;
            tbdata.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbdata.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbdata.BorderColor = iTextSharp.text.Color.GRAY;
            tbdata.Cellspacing = 1;
            tbdata.Cellpadding = 1;
            for (int k = 1; k < dt.Rows.Count - 1; k++)
            {
                DataRow dr = dt.Rows[k];
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    Cell tc = new Cell(new Phrase(dr[i].ToString(), fCapText_5));
                    if (k == 8 || k == 9)
                    {
                        tc.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
                    }
                    if (i == 0)
                        tc.HorizontalAlignment = Element.ALIGN_LEFT;
                    else
                        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbdata.AddCell(tc);
                }
            }
            document.Add(tbdata);
            //------------ TABLE FOOTER ROW 
            iTextSharp.text.Table tb2 = new iTextSharp.text.Table(ColumnsCount);
            tb2.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb2.Width = 100;
            tb2.Widths = ws;
            tb2.Alignment = Element.ALIGN_CENTER;
            tb2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb2.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb2.BorderColor = iTextSharp.text.Color.WHITE;
            tb2.Cellspacing = 1;
            tb2.Cellpadding = 1;
            DataRow drF = dt.Rows[dt.Rows.Count - 1];
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                Cell tc = new Cell(new Phrase(drF[i].ToString(), fCapText_5));
                if (i == 0)
                    tc.HorizontalAlignment = Element.ALIGN_LEFT;
                else
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;

                tb2.AddCell(tc);
            }
            //------------------------------------
            document.Add(tb2);
            document.Close();
            if (File.Exists(Server.MapPath("~\\Budget_Forecast.pdf")))
            {
                File.Delete(Server.MapPath("~\\Budget_Forecast.pdf"));
            }

            FileStream fs = new FileStream(Server.MapPath("~\\Budget_Forecast.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('./Budget_Forecast.pdf?rnd=" + R.NextDouble().ToString() + "');", true);
        }
        catch (System.Exception ex)
        {

        }
    }
    protected void btnEdit2_Click(object sender, EventArgs e)
    {
        if ("" + Request.Form["radVSL"] != "")
        {
            char[] sep = { ',' };
            string[] Ships = Request.Form["radVSL"].Split(sep);
            if (Ships.Length > 1)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('Please select only one vessel.');", true);
            }
            else
            {
                Response.Redirect("BudgetForecastingNextyear.aspx?Vsl=" + Request.Form["radVSL"], true);
            }
        }
    }
    protected void btnPrntSummary_Click(object sender, EventArgs e)
    {
        //---------------------------------
        string days = " [ " + (DateTime.Parse("31-Dec-" + (DateTime.Today.Year + 1).ToString()).Subtract(DateTime.Parse("1-Jan-" + (DateTime.Today.Year + 1).ToString())).Days + 1).ToString() + " ] days";
        if (ddlCompanyF.SelectedIndex > 0)
        {
            DataTable dt = BindFleetView(false);
            ExportToPDF("Fleet Summary\n" + ddlCompanyF.SelectedItem.Text + " ( Budget - " + lblBudgetYear.Text + " )\n" + days, dt);
        }
        else
        {
            DataTable dt = BindFleetView(false);
            ExportToPDF("Fleet Summary\n" + ddlFleet.SelectedItem.Text + " ( Budget - " + lblBudgetYear.Text + " )\n" + days, dt);
        }
        //---------------------------------
    }
    protected void btnPrntDetails_Click(object sender, EventArgs e)
    {
        //---------------------------------
        if ("" + Request.Form["radVSL"] != "")
        {
            char[] sep = { ',' };
            string[] Ships = Request.Form["radVSL"].Split(sep);

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "window.open('Print2.aspx?VSL=" + Request.Form["radVSL"] + "');", true);
        }
        //---------------------------------
    }

  
    
    
}
