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
using System.Data.SqlClient;
using System.Xml;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows;

public partial class KPI : System.Web.UI.Page
{
    int intLogin_Id = 0;
    Authority Auth;
    static Random r = new Random();
    public int TotalVesselCount
    {
        set { ViewState["TotalVesselCount"] = value; }
        get { return int.Parse("0" + ViewState["TotalVesselCount"]); }
    }
    //LTIF
    public decimal TotalLTIF
    {
        set { ViewState["TotalLTIF"] = value; }
        get { return decimal.Parse("0" + ViewState["TotalLTIF"]); }
    }
    public decimal SumLtifEH
    {
        set { ViewState["SumLtifEH"] = value; }
        get { return decimal.Parse("0" + ViewState["SumLtifEH"]); }
    }
    public decimal SumLtiByMon
    {
        set { ViewState["SumLtiByMon"] = value; }
        get { return decimal.Parse("0" + ViewState["SumLtiByMon"]); }
    }
    //TRCF
    public decimal TotalTRC
    {
        set { ViewState["TRC"] = value; }
        get { return decimal.Parse("0" + ViewState["TRC"]); }
    }
    public decimal TotalEH
    {
        set { ViewState["TotalEH"] = value; }
        get { return decimal.Parse("0" + ViewState["TotalEH"]); }
    }
    
    public int TotalAuditInspection
    {
        set { ViewState["TotalAuditInspection"] = value; }
        get { return int.Parse("0" + ViewState["TotalAuditInspection"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 164);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
           try
           {
            //this.Form.DefaultButton = this.btn_Show.UniqueID.ToString();
            //lblMessage.Text = ""+Session["ms"];
            if (Session["loginid"] == null) 
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            else
            {
                intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
            }
            if (Session["loginid"] != null)
            {
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()),13);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }
            if (Session["loginid"] != null)
            {
                HiddenField_LoginId.Value = Session["loginid"].ToString();
            }
            lit1.Text = "";
            if (!Page.IsPostBack)
            {
                lit1.Text = "<script type='text/javascript'> document.getElementById('aaa').focus();function f(){__doPostBack('aaa','');}setTimeout('f()',3);</script>";
                Session["FormSelection"] = "1";
                for(int i=DateTime.Today.Year+1 ;i>=2010;i--)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));  
                }
                ddlYear.SelectedValue = DateTime.Today.Year.ToString();
                BindFleet();
                BindVessel();
                //btn_Show_Click(sender, e);
            }
            else
            {
                Session.Remove("FormSelection");
            }
        }
        catch (Exception ex) { throw ex; }
    }

    // -------------- Events
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        //try
        //{
            TotalAuditInspection = 0;
            TotalLTIF = 0;
            TotalTRC = 0;
            TotalEH = 0;

            SumLtifEH = 0;
            SumLtiByMon = 0;

            SetVesselCode();
            GrsKPIs.DataSource = null;
            GrsKPIs.DataBind();

            //string sql = "select ID,KPI, " +
            // " (select KPIValue from m_KPIParametersValue KPV where KPV.KPID=KP.ID and KPV.KPIYEAR=" + ddlYear.SelectedValue + ")KPIVALUE " +
            // " from m_KPIParameters KP";
            //DataTable dt3 = VesselReporting.getTable(sql).Tables[0]; 

            Common.Set_Procedures("dbo.sp_GetKPIData");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                    new MyParameter("@FleetID", ddlFleet.SelectedValue),
                    new MyParameter("@VesselID", ddlVessel.SelectedValue),
                    new MyParameter("@Year",ddlYear.SelectedValue )
                );

            DataSet dt3 = Common.Execute_Procedures_Select();
            
            GrsKPIs.DataSource = dt3;
            GrsKPIs.DataBind();

            //foreach (RepeaterItem Itm in GrsKPIs.Items)
            //{
            //    HiddenField hfID = (HiddenField)Itm.FindControl("hfID");
            //    Label lblTotal = (Label)Itm.FindControl("lblTotal");
            //    Label lblKPIValues = (Label)Itm.FindControl("lblKPIValues");

            //    if (hfID.Value == "5")
            //    {   
            //        int KPI = Common.CastAsInt32(lblKPIValues.Text);
            //        if (KPI != 0)
            //            lblTotal.Text = Convert.ToString(Math.Round(((Common.CastAsDecimal(lblM1.Text) + Common.CastAsInt32(lblM2.Text) + Common.CastAsInt32(lblM3.Text) + Common.CastAsInt32(lblM4.Text) + Common.CastAsInt32(lblM5.Text) + Common.CastAsInt32(lblM6.Text) + Common.CastAsInt32(lblM7.Text) + Common.CastAsInt32(lblM8.Text) + Common.CastAsInt32(lblM9.Text) + Common.CastAsInt32(lblM10.Text) + Common.CastAsInt32(lblM11.Text) + Common.CastAsInt32(lblM12.Text)) * (KPI / 12)), 2)) ;
            //    }
            //    if (hfID.Value == "4" || hfID.Value == "1" || hfID.Value == "6" || hfID.Value == "2" || hfID.Value == "7" )
            //    {
            //        lblTotal.Text = "0";
            //    }
            //    if(hfID.Value == "8" || hfID.Value == "9")
            //        lblTotal.Text = "< 1.5";
            //}
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
        //BindGrid();
    }    
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        BindVessel();
        ddlVessel.SelectedIndex = 0;
        btn_Show_Click(sender, e);
    }

    protected void GrsKPIs_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
        Label lblPercentValue = (Label)e.Item.FindControl("lblPercentValue");
        HiddenField hfID = (HiddenField)e.Item.FindControl("hfID");

        int lblValuesM1  = Common.CastAsInt32 (((Label )e.Item.FindControl("lblValuesM1")).Text);
        int  lblValuesM2 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM2")).Text);
        int  lblValuesM3 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM3")).Text);
        int  lblValuesM4 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM4")).Text);
        int  lblValuesM5 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM5")).Text);
        int  lblValuesM6 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM6")).Text);
        int  lblValuesM7 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM7")).Text);
        int  lblValuesM8 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM8")).Text);
        int  lblValuesM9 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM9")).Text);
        int  lblValuesM10 =Common.CastAsInt32 (( (Label)e.Item.FindControl("lblValuesM10")).Text);
        int  lblValuesM11 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM11")).Text);
        int  lblValuesM12 = Common.CastAsInt32 (((Label)e.Item.FindControl("lblValuesM12")).Text);

        int Recieved = Common.CastAsInt32(((Label)e.Item.FindControl("lblRecieved")).Text);

        if (hfID.Value == "5") //Near Miss
            lblPercentValue.Text = Math.Round((Common.CastAsDecimal(Recieved) / TotalVesselCount), 2).ToString() + "/M";
        else if (hfID.Value == "4" || hfID.Value == "1" || hfID.Value == "6" || hfID.Value == "2" || hfID.Value == "7" || hfID.Value == "8" || hfID.Value == "9") //Major Accident
            lblPercentValue.Text = "0.00";
    }

    protected void lnkKPI_OnClick(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        HiddenField hfID = (HiddenField)lnk.Parent.FindControl("hfID");

        if (hfID.Value == "1") // Fatality
        {
            dvFatality.Visible = true;
            BindFatalityPerVessel();
        }
        if (hfID.Value == "2") // PSC Detention
        {
            divPSCDetention.Visible = true;
            BindPSCDetentionPerVessel();
        }

        if (hfID.Value == "3") // Polution
        {
            divPolution.Visible = true;
            BindPolutionVessel();
        }
        else if (hfID.Value == "4") // Major Accident
        {
            dvMajorAccidentPopup.Visible = true;
            BindMajorAccPerVessel();
            GenerateAccidentGraph();
            BindAccidentForImport();
        }
        else if (hfID.Value == "5")  // NearMiss
        {
            dvNearMissPopUp.Visible = true;
            BindNearMissperVessel();
            GenerateVessNearMissGraph();
            BindNearMissDataForImport();
        }
        else if (hfID.Value == "6") // FlawLess
        {
            dvFlawLess.Visible = true;
            BindFlawlessPerVessel();
        }
        else if (hfID.Value == "7") // AUDIT & INSPECTION
        {
            divAuditInspection.Visible = true;
            BindAuditInspctionVessel();
        }
        else if (hfID.Value == "8") // LTIF
        {
            divLTIF.Visible = true;
            BindLTIFPopup();
            GenerateLTIFGraph();
        }
        else if (hfID.Value == "9") // TRCF
        {
            divTRCFPopup.Visible = true;
            BindTRCFPopup();
            GenerateTRCFGraph();
        }
        else if (hfID.Value == "11") // CREW SIGN OFF ON MEDICAL GROUND 
        {
            divCrewSignOffMedical.Visible = true;
            BindCrewSignOffMedicalPerVessel();
        }
        else if (hfID.Value == "12") // Retention Rate
        {
            lnk.CssClass = "UnlinkBtn";
            //divRetentionRate.Visible = true;
            //BindRetentionRatePerVessel();
        }
        
    }
    
    protected void ddlCloseKPIPerVesselPopUp_OnClick(object sender, EventArgs e)
    {
        dvNearMissPopUp.Visible = false;
    }
    protected void btnCloseMajorAccPopup_OnClick(object sender, EventArgs e)
    {
        dvMajorAccidentPopup.Visible = false;
    }
    protected void btnCloseFatality_OnClick(object sender, EventArgs e)
    {
        dvFatality.Visible = false;
    }
    protected void btnCloseTRCF_OnClick(object sender, EventArgs e)
    {
        divTRCFPopup.Visible = false;
    }
    protected void btnCloseLTIF_OnClick(object sender, EventArgs e)
    {
        divLTIF.Visible = false;
    }
    protected void btnClosePolution_OnClick(object sender, EventArgs e)
    {
        divPolution.Visible = false;
    }
    protected void btnClosePSCDetention_OnClick(object sender, EventArgs e)
    {
        divPSCDetention.Visible = false;
    }
    protected void btnCloseAuditInspection_OnClick(object sender, EventArgs e)
    {
        divAuditInspection.Visible = false;
    }
    protected void btnCloseCrewSignOff_OnClick(object sender, EventArgs e)
    {
        divCrewSignOffMedical.Visible = false;
    }
    protected void btnCloseRR_OnClick(object sender, EventArgs e)
    {
        divRetentionRate.Visible = false;
    }
    protected void btnRRVerification_OnClick(object sender, EventArgs e)
    {
        divRRVerification.Visible = false;
    }
    

    protected void btnExportNearMiss_OnClick(object sender, EventArgs e)
    {
        string sql = "  Exec sp_GetNearMissDataImport " + ddlYear.SelectedValue;
        DataSet ds = VesselReporting.getTable(sql);
        ds.Tables[0].Columns[1].ColumnName = "Category";
        ds.Tables[0].Columns[2].ColumnName = "Jan";
        ds.Tables[0].Columns[3].ColumnName = "Feb";
        ds.Tables[0].Columns[4].ColumnName = "Mar";
        ds.Tables[0].Columns[5].ColumnName = "Apr";
        ds.Tables[0].Columns[6].ColumnName = "May";
        ds.Tables[0].Columns[7].ColumnName = "Jun";
        ds.Tables[0].Columns[8].ColumnName = "Jul";
        ds.Tables[0].Columns[9].ColumnName = "Aug";
        ds.Tables[0].Columns[10].ColumnName = "Sep";
        ds.Tables[0].Columns[11].ColumnName = "Oct";
        ds.Tables[0].Columns[12].ColumnName = "Nov";
        ds.Tables[0].Columns[13].ColumnName = "Dec";
        ExportDatatable(ds.Tables[0], "NearMiss " + ddlYear.SelectedValue);
    }
    protected void btnAccidentForImport_OnClick(object sender, EventArgs e)
    {
        string sql = "  Exec sp_GetKPIAccidentDataImport " + ddlYear.SelectedValue;
        DataSet ds = VesselReporting.getTable(sql);
        ds.Tables[0].Columns[1].ColumnName = "Category";
        ds.Tables[0].Columns[2].ColumnName = "Jan";
        ds.Tables[0].Columns[3].ColumnName = "Feb";
        ds.Tables[0].Columns[4].ColumnName = "Mar";
        ds.Tables[0].Columns[5].ColumnName = "Apr";
        ds.Tables[0].Columns[6].ColumnName = "May";
        ds.Tables[0].Columns[7].ColumnName = "Jun";
        ds.Tables[0].Columns[8].ColumnName = "Jul";
        ds.Tables[0].Columns[9].ColumnName = "Aug";
        ds.Tables[0].Columns[10].ColumnName = "Sep";
        ds.Tables[0].Columns[11].ColumnName = "Oct";
        ds.Tables[0].Columns[12].ColumnName = "Nov";
        ds.Tables[0].Columns[13].ColumnName = "Dec";
        ExportDatatable(ds.Tables[0], "Accident " + ddlYear.SelectedValue);
    }
    
    protected void btnFlawLess_OnClick(object sender, EventArgs e)
    {
        dvFlawLess.Visible = false;
    }
    protected void btnShowGraphPop_OnClick(object sender, EventArgs e)
    {
        GenerateVessNearMissGraph();
    }

    protected void ddlLtifScale_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateLTIFGraph();
    }
    protected void ddlTrcfScale_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateTRCFGraph();
    }
    protected void ddlScaleAccident_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateAccidentGraph();
    }

    protected void ShowMonthVerification(object sender,EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        int Month = Common.CastAsInt32( lnk.CommandArgument);
        HiddenField hfKPIID = (HiddenField)lnk.Parent.FindControl("hfID");
        if (hfKPIID.Value == "12")
        {

            Common.Set_Procedures("dbo.sp_GetRetentionRatePerMonth");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                    new MyParameter("@YEAR", ddlYear.SelectedValue),
                    new MyParameter("@MONTH", Month.ToString())
                );
            DataSet ds = Common.Execute_Procedures_Select();
            if (ds != null)
            {
                lblRRHeading.Text = "Retention Rate Verification for - " + GetMonthNameByNumber(Month) + " " + ddlYear.SelectedValue;

                lblS.Text = ds.Tables[0].Rows[0]["S"].ToString();
                lblUT.Text = ds.Tables[0].Rows[0]["UT"].ToString();
                lblBT.Text = ds.Tables[0].Rows[0]["BT"].ToString();
                lblAE.Text = ds.Tables[0].Rows[0]["AE"].ToString();
                lblResult.Text = ds.Tables[0].Rows[0]["Result"].ToString();
                divRRVerification.Visible=true;
            }

        }
    }
    // -------------- Function
    public string ShowKPIValues(string KPID, string ColumnName)
    {
        string ValueToReturn = "";
        if (KPID == "1") //Fatality 
        {
            ValueToReturn = GetFatalityValues(ColumnName);
        }
        else if (KPID == "2") //PSC Detention
        {
            ValueToReturn = GetPSCDetentionValues(ColumnName);
        }
        else if (KPID == "4") //Major Accident
        {
            ValueToReturn = GetMajorAccidentValues(ColumnName);
        }
        else if (KPID == "5") //Near Miss
        {
            ValueToReturn = GetNearMissValues(ColumnName);
        }
        else if (KPID == "6") //FLAWLESS PSC PERFORMANCE
        {
            ValueToReturn = GetFlawLessValues(ColumnName);
        }
        else if (KPID == "7") //Audit and Insspection On Time
        {
            ValueToReturn = GetAuditInspectionValues(ColumnName);
        }
        else if (KPID == "8") //LTIF
        {
            ValueToReturn = GetLTIFValues(ColumnName);
        }
        else if (KPID == "9") //TRCF
        {
            ValueToReturn = GetTRCFValues(ColumnName);
        }
        
        return ValueToReturn;
    }
    public void ExportDatatable(DataTable dt, String FileName)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        dg.DataSource = dt;
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }


    public void BindFleet()
    {
        string Query = "select * from dbo.FleetMaster";
        ddlFleet.DataSource = Budget.getTable(Query);
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetID";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("<--All-->", "0"));
    }
    public void BindVessel()
    {
        string WhereClause = "";
        string sql = "SELECT VesselID,Vesselname FROM DBO.Vessel v Where 1=1 ";
        if (ddlFleet.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and fleetID=" + ddlFleet.SelectedValue + "";
        }
        sql = sql + WhereClause + "ORDER BY VESSELNAME";
        ddlVessel.DataSource = VesselReporting.getTable(sql);

        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("<--All-->", "0"));
    }    
    public string GetVesselCount(string Month)
    {
        string ValueToReturn = "";

        string VesselFileter = "";
        if (ddlVessel.SelectedIndex != 0)
            VesselFileter = " And VesselID=" + ddlVessel.SelectedValue;
        else if (ddlFleet.SelectedIndex != 0)
        {
            VesselFileter = " And VesselID in(select VesselID from dbo.Vessel Where FleetID=" + ddlFleet.SelectedValue + ")";
        }

        string StartDate = "01-" + Month + "-"+ddlYear.SelectedValue;
        string EndDate = Convert.ToDateTime(StartDate).AddMonths(1).AddDays(-1).ToString();

        string sql = " select Count(*) from dbo.Vessel " +
                     " Where ((EffectiveDate <= '" + EndDate + "' and ReleaseDate is null) " +
                     " or (EffectiveDate <='" + EndDate + "' and ReleaseDate >='" + StartDate + "' )) " + VesselFileter;

            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt != null)
                ValueToReturn = dt.Rows[0][0].ToString();
        

        return ValueToReturn;
    }
    public void SetVesselCode()
    {
        int CM = DateTime.Today.Month;
        int SY=Common.CastAsInt32( ddlYear.SelectedValue);
        int cnt = 1;

        lblM1.Text = "0";
        lblM2.Text = "0";
        lblM3.Text = "0";
        lblM4.Text = "0";
        lblM5.Text = "0";
        lblM6.Text = "0";
        lblM7.Text = "0";
        lblM8.Text = "0";
        lblM9.Text = "0";
        lblM10.Text = "0";
        lblM11.Text = "0";
        lblM12.Text = "0";
        if (SY < DateTime.Today.Year)
        {
            lblM1.Text = GetVesselCount("Jan");
            lblM2.Text = GetVesselCount("Feb");
            lblM3.Text = GetVesselCount("Mar");
            lblM4.Text = GetVesselCount("Apr");
            lblM5.Text = GetVesselCount("May");
            lblM6.Text = GetVesselCount("Jun");
            lblM7.Text = GetVesselCount("Jul");
            lblM8.Text = GetVesselCount("Aug");
            lblM9.Text = GetVesselCount("Sep");
            lblM10.Text = GetVesselCount("Oct");
            lblM11.Text = GetVesselCount("Nov");
            lblM12.Text = GetVesselCount("Dec");
        }
        else
        {
            if (cnt <= CM)
            {
                lblM1.Text = GetVesselCount("Jan");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM2.Text = GetVesselCount("Feb");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM3.Text = GetVesselCount("Mar");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM4.Text = GetVesselCount("Apr");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM5.Text = GetVesselCount("May");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM6.Text = GetVesselCount("Jun");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM7.Text = GetVesselCount("Jul");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM8.Text = GetVesselCount("Aug");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM9.Text = GetVesselCount("Sep");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM10.Text = GetVesselCount("Oct");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM11.Text = GetVesselCount("Nov");
                cnt++;
            }
            if (cnt <= CM)
            {
                lblM12.Text = GetVesselCount("Dec");
                cnt++;
            }
        }

        TotalVesselCount = Common.CastAsInt32(lblM1.Text) + Common.CastAsInt32(lblM2.Text) + Common.CastAsInt32(lblM3.Text) + Common.CastAsInt32(lblM4.Text) + Common.CastAsInt32(lblM5.Text) + Common.CastAsInt32(lblM6.Text) + Common.CastAsInt32(lblM7.Text) + Common.CastAsInt32(lblM8.Text) + Common.CastAsInt32(lblM9.Text) + Common.CastAsInt32(lblM10.Text) + Common.CastAsInt32(lblM11.Text) + Common.CastAsInt32(lblM12.Text);
        lblAllVessCount.Text = TotalVesselCount.ToString();
    }
    public string GetMonthNoByName(string MonthName)
    {
        switch (MonthName)
        {
            case "Jan":
                return "1";
            case "Feb":
                return "2";
            case "Mar":
                return "3";
            case "Apr":
                return "4";
            case "May":
                return "5";
            case "Jun":
                return "6";
            case "Jul":
                return "7";
            case "Aug":
                return "8";
            case "Sep":
                return "9";
            case "Oct":
                return "10";
            case "Nov":
                return "11";
            case "Dec":
                return "12";
            default:
                return "0";

        }
    }
    public string GetMonthNameByNumber(int MonthNo)
    {
        switch (MonthNo)
        {
            case 1:
                return "Jan";
            case 2:
                return "Feb";
            case 3:
                return "Mar";
            case 4:
                return "Apr";
            case 5:
                return "May";
            case 6:
                return "Jun";
            case 7:
                return "Jul";
            case 8:
                return "Aug";
            case 9:
                return "Sep";
            case 10:
                return "Oct";
            case 11:
                return "Nov";
            case 12:
                return "Dec";
            default:
                return "";

        }
    }
    public string ConvertToInteger(string val, string PKIID)
    {
        if (PKIID == "1" || PKIID == "2" || PKIID == "3" || PKIID == "4" || PKIID == "5" || PKIID == "6" || PKIID == "7" || PKIID == "10" || PKIID == "11" || PKIID == "999")
            return Common.CastAsInt32(val).ToString();
        else
            return val;
    }
    public string ConvertToIntegerForAchiev(string val, string PKIID)
    {
        if (PKIID == "1" || PKIID == "2" || PKIID == "3" || PKIID == "4" || PKIID == "6" || PKIID == "7" || PKIID == "11" || PKIID == "999")
        {
            if (PKIID == "6")
                return val+" % ";
            else
                return Common.CastAsInt32(val).ToString();
        }
        else
            return val;
    }
    public string SetAchivCSS(string KPIID,string Achiv)
    {
        if (KPIID == "8" || KPIID == "9")
        {
            if (Common.CastAsDecimal(Achiv) >= Common.CastAsDecimal(1.5))
                return "red";
            else
                return "nocss";
        }
        else if (KPIID == "7")
        {
            if (Common.CastAsDecimal(Achiv) > Common.CastAsDecimal(0))
                return "red";
            else
                return "nocss";
        }
        else
        {
            if (Common.CastAsInt32(Achiv) < Common.CastAsInt32(0))
                return "red";
            else
                return "nocss";

        }
    }
    // --------------------- Near MIss Values and Calculation
    public void GenerateVessNearMissGraph()
    {
        try
        {
            DataTable Dt = new DataTable();
            int Cnt = 0;
            if (chkInjuryCategory.Checked)
                Cnt++;
            if (chkPollutionCategory.Checked)
                Cnt++;
            if (chkPreDamageCategory.Checked)
                Cnt++;

            StringFormat sf1 = new StringFormat();
            sf1.Alignment = StringAlignment.Near;
            sf1.LineAlignment = StringAlignment.Near;
            
            Bitmap b = new Bitmap(980, 560);
            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int M;
            int X = 40;
            int Y = 300;
            int BarWidth = 0;

            if (Cnt == 1)
                BarWidth = 60;
            if (Cnt == 2)
                BarWidth = 30;
            if (Cnt == 3)
                BarWidth = 20;

            int ICPer = 0, PCPer = 0, PDCPer = 0;
            Font myFont = new System.Drawing.Font("Helvetica", 8, FontStyle.Regular);
            Font myFontHeader = new System.Drawing.Font("Helvetica", 12, FontStyle.Bold);

            Type t = typeof(Brushes);
            Brush brushGreen = (Brush)t.GetProperty("LightGreen").GetValue(null, null);
            Brush brushRed = (Brush)t.GetProperty("LightPink").GetValue(null, null);
            Brush brushBlue = (Brush)t.GetProperty("LightSkyBlue").GetValue(null, null);

            Color VC;
            VC = System.Drawing.Color.LightGray;
            Pen P = new Pen(VC, 1);
            P.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            if (Cnt != 0)
                for (M = 1; M <= 12; M++)
                {

                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    Dt = RenderingImageData(M);
                    DataRow dr = Dt.Rows[0];
                    ICPer = Common.CastAsInt32(dr["InjuryCategory"]) * 3;
                    PCPer = Common.CastAsInt32(dr["PollutionCategory"]) * 3;
                    PDCPer = Common.CastAsInt32(dr["ProDamageCategory"]) * 3;

                    //----------------------
                    g.DrawString(GetMonthNameByNumber(M) , myFont, Brushes.Black, new Point(X + 30, Y + 185), sf);
                    g.DrawString( " [ " + dr["VessPerMon"].ToString() + " ]", myFont, Brushes.Black, new Point(X + 30, Y + 198), sf);

                    if (chkInjuryCategory.Checked)
                    {
                        g.FillRectangle(brushGreen, new Rectangle(X, Y + 160 - ICPer, BarWidth, ICPer));
                        g.DrawString(ICPer.ToString(), myFont, new SolidBrush(Color.Black), new Point(X + 10, Y + 170), sf);
                        X = X + BarWidth;
                    }

                    //----------------------
                    if (chkPollutionCategory.Checked)
                    {

                        g.FillRectangle(brushRed, new Rectangle(X, Y + 160 - PCPer, BarWidth, PCPer));
                        g.DrawString(PCPer.ToString(), myFont, Brushes.Black, new Point(X + 10, Y + 170), sf);
                        X = X + BarWidth;
                    }
                    //----------------------
                    if (chkPreDamageCategory.Checked)
                    {

                        g.FillRectangle(brushBlue, new Rectangle(X, Y + 160 - PDCPer, BarWidth, PDCPer));
                        g.DrawString(PDCPer.ToString(), myFont, Brushes.Black, new Point(X + 10, Y + 170), sf);

                    }
                    g.DrawLine(P, new Point(X + BarWidth + 8, 454), new Point(X + BarWidth + 8, 466));

                    X = X + BarWidth + 16;
                }

            //----- Header
            g.DrawString("Near Miss Count by Category ", myFontHeader, Brushes.Black, new Point(360, 10), sf1);
            g.DrawString(ddlYear.SelectedValue, myFontHeader, Brushes.Black, new Point(460, 30), sf1);

            //Vertical Line
            g.DrawLine(P, new Point(30, 58), new Point(30, Y + 165));

            //10 horizental Line

            int RRH = Y + 165-5;
            int Counter = 0;
            for (int i = 0; i < 17; i++)
            {
                g.DrawLine(P, new Point(28, RRH), new Point(945, RRH));
                g.DrawString(Counter.ToString().PadLeft(3, ' '), myFont, Brushes.Black, new Point(3, RRH - 6), sf1);

                RRH = RRH - 25;
                Counter = Counter + 25;
            }

            g.FillRectangle(brushGreen, new Rectangle(300, Y+230, 15, 15));
            g.DrawString("Injury Category", myFont, Brushes.Black, new Point(320, Y + 230), sf1);

            g.FillRectangle(brushRed, new Rectangle(430, Y + 230, 15, 15));
            g.DrawString("Pollution Category", myFont, Brushes.Black, new Point(450, Y + 230), sf1);

            g.FillRectangle(brushBlue, new Rectangle(570, Y + 230, 15, 15));
            g.DrawString("Pro Damage Category", myFont, Brushes.Black, new Point(590, Y + 230), sf1);

            if (File.Exists("Graph.jpg"))
                File.Delete("Graph.jpg");

            b.Save(Server.MapPath("Graph.jpg"));
            imgGraph.ImageUrl = "Graph.jpg?" + r.NextDouble().ToString();
        }
        catch (Exception ex)
        {

        }
    }
    public DataTable RenderingImageData(int Month)
    {
        //string sql = "Select VesselCode,VesselName  " +
        //            " , (select count(*) from dbo.FR_NearMiss NM where NM.VesselID=v.vesselid And ISNULL(NM.InjuryCategory,'')<>'' And Year(DateOfOccurrence)="+ddlYear.SelectedValue+" )InjuryCategory " +
        //            " , (select count(*) from dbo.FR_NearMiss NM where NM.VesselID=v.vesselid And ISNULL(NM.PollutionCategory,'')<>'' And Year(DateOfOccurrence)=" + ddlYear.SelectedValue + ")PollutionCategory " +
        //            " , (select count(*) from dbo.FR_NearMiss NM where NM.VesselID=v.vesselid And ISNULL(NM.ProDamageCategory,'')<>'' And Year(DateOfOccurrence)=" + ddlYear.SelectedValue + ")ProDamageCategory " +
        //            " from dbo.Vessel v Where v.VesselStatusid<>2 and v.vesselid not in (41,43,44)";

        string StartDate = "01-" + GetMonthNameByNumber(Month) + "-" + ddlYear.SelectedValue;
        string EndDate = Convert.ToDateTime(StartDate).AddMonths(1).AddDays(-1).ToString();

        string sql = " Select " +
                     " (select count(*) from dbo.FR_NearMiss NM where ISNULL(NM.InjuryCategory,'')<>'' And Year(DateOfOccurrence)=" + ddlYear.SelectedValue + " And Month(DateOfOccurrence)=" + Month.ToString() + ")InjuryCategory  " +
                     " ,(select count(*) from dbo.FR_NearMiss NM where ISNULL(NM.PollutionCategory,'')<>'' And Year(DateOfOccurrence)=" + ddlYear.SelectedValue + " And Month(DateOfOccurrence)=" + Month.ToString() + ")PollutionCategory  " +
                     " ,(select count(*) from dbo.FR_NearMiss NM where ISNULL(NM.ProDamageCategory,'')<>'' And Year(DateOfOccurrence)=" + ddlYear.SelectedValue + "  And Month(DateOfOccurrence)=" + Month.ToString() + ")ProDamageCategory " +
                     " , (select Count(*) from dbo.Vessel Where (EffectiveDate <= '" + EndDate + "' and ReleaseDate is null) or (EffectiveDate <='" + EndDate + "' and ReleaseDate >='" + StartDate + "' ))VessPerMon";
        DataSet ds = VesselReporting.getTable(sql);

        return ds.Tables[0];
    }
    public string GetNearMissValues(string ColumnName)
    {
        string ValueToReturn = "";

        string VesselFileter = "";
        if (ddlVessel.SelectedIndex != 0)
            VesselFileter = " And VesselID=" + ddlVessel.SelectedValue;
        else if (ddlFleet.SelectedIndex != 0)
        {
            VesselFileter = " And VesselID in(select VesselID from dbo.Vessel Where FleetID=" + ddlFleet.SelectedValue + ")";
        }

        if (ColumnName == "Recd")
        {
            string sql = "select Count(*)cnt from FR_NearMiss Where Year(DateOfOccurrence)=" + ddlYear.SelectedValue + VesselFileter;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt != null)
                ValueToReturn = dt.Rows[0][0].ToString();
        }
        else
        {
            if (Common.CastAsInt32(GetMonthNoByName(ColumnName)) > DateTime.Today.Month)
                ValueToReturn = "0";
            else
            {
                string sql = "select Count(*)cnt from FR_NearMiss Where Month(DateOfOccurrence)=" + GetMonthNoByName(ColumnName) + " and Year(DateOfOccurrence)=" + ddlYear.SelectedValue + VesselFileter;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt != null)
                    ValueToReturn = dt.Rows[0][0].ToString();
            }
        }

        return ValueToReturn;
    }
    public void BindNearMissperVessel()
    {
        string sql = "Select VesselCode,VesselName  " +
                    " , (select count(*) from dbo.FR_NearMiss NM where NM.VesselID=v.vesselid And ISNULL(NM.InjuryCategory,'')<>'' And Year(DateOfOccurrence)=" + ddlYear.SelectedValue + " )InjuryCategory " +
                    " , (select count(*) from dbo.FR_NearMiss NM where NM.VesselID=v.vesselid And ISNULL(NM.PollutionCategory,'')<>'' And Year(DateOfOccurrence)=" + ddlYear.SelectedValue + ")PollutionCategory " +
                    " , (select count(*) from dbo.FR_NearMiss NM where NM.VesselID=v.vesselid And ISNULL(NM.ProDamageCategory,'')<>'' And Year(DateOfOccurrence)=" + ddlYear.SelectedValue + ")ProDamageCategory " +
                    " from dbo.Vessel v Where v.VesselStatusid<>2 ";
        sql = sql + " ORDER BY VESSELNAME";
        DataSet ds = VesselReporting.getTable(sql);

        rptKPIperVessel.DataSource = ds;
        rptKPIperVessel.DataBind();
    }
    public void BindNearMissDataForImport()
    {
        string sql = "  Exec sp_GetNearMissDataImport " + ddlYear.SelectedValue;
        DataSet ds = VesselReporting.getTable(sql);

        grdNearMiss.DataSource = ds;
        grdNearMiss.DataBind();
    }

    // --------------------- Major Accident Values and Calculation
    public string GetMajorAccidentValues(string ColumnName)
    {
        string ValueToReturn = "";

        string VesselFileter = "";
        if (ddlVessel.SelectedIndex != 0)
            VesselFileter = " And VesselID=" + ddlVessel.SelectedValue;
        else if (ddlFleet.SelectedIndex != 0)
        {
            VesselFileter = " And VesselID in(select VesselID from dbo.Vessel Where FleetID=" + ddlFleet.SelectedValue + ")";
        }

        if (ColumnName == "Recd")
        {
            string sql = "Select Count(*) from FR_Accident Where AccidentSeverity=3 And Year(ReportDate)=" + ddlYear.SelectedValue  + VesselFileter;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt != null)
                ValueToReturn = dt.Rows[0][0].ToString();
        }
        else
        {
            if (Common.CastAsInt32(GetMonthNoByName(ColumnName)) > DateTime.Today.Month)
                ValueToReturn = "0";
            else
            {
                string sql = "Select Count(*) from FR_Accident Where AccidentSeverity=3 And Month(ReportDate)=" + GetMonthNoByName(ColumnName) + " And Year(ReportDate)="+ddlYear.SelectedValue+" " + VesselFileter;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt != null)
                    ValueToReturn = dt.Rows[0][0].ToString();
            }
        }

        return ValueToReturn;
    }
    public void BindMajorAccPerVessel()
    {
        string sql = "Select VesselCode,VesselName  " +
                     " ,(Select count(*) from FR_Accident Acc where Acc.AccidentSeverity=3 And Acc.VesselID=v.VesselID And Year(ReportDate)="+ddlYear.SelectedValue +"  )MajorAccCount " +
                    " from dbo.Vessel v Where v.VesselStatusid<>2 and v.vesselid not in (41,43,44)";
        sql = sql + " ORDER BY VESSELNAME";
        DataSet ds = VesselReporting.getTable(sql);

        rptMajorAccident.DataSource = ds;
        rptMajorAccident.DataBind();
    }

    public void GenerateAccidentGraph()
    {
        try
        {
            DataTable Dt = new DataTable();
            
            StringFormat sf1 = new StringFormat();
            sf1.Alignment = StringAlignment.Near;
            sf1.LineAlignment = StringAlignment.Near;

            Font myFont = new System.Drawing.Font("Helvetica", 8, FontStyle.Regular);
            Font myFontHeader = new System.Drawing.Font("Helvetica", 12, FontStyle.Bold);

            Type t = typeof(Brushes);
            Brush brushGreen = (Brush)t.GetProperty("LightGreen").GetValue(null, null);
            Brush brushRed = (Brush)t.GetProperty("LightPink").GetValue(null, null);
            Brush brushBlue = (Brush)t.GetProperty("LightSkyBlue").GetValue(null, null);

            Bitmap b = new Bitmap(980, 650);
            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int M;
            int X = 100;
            int Y = 300;
            int BarWidth = 20;

            int AccCount = 0;

            Color VC;
            VC = System.Drawing.Color.LightGray;
            Pen P = new Pen(VC, 1);
            P.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            int TX = X;
            int TY = Y;
            for (M = 1; M <= 12; M++)
            {

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                Dt = RenderingImageDataAccident(M);
                DataRow dr = Dt.Rows[0];
                AccCount = Common.CastAsInt32(dr["MajorAccCount"]) * 5;

                //----------------------
                g.DrawString(GetMonthNameByNumber(M), myFont, Brushes.Black, new Point(TX +10, TY + 185), sf);
                g.DrawString(" [ " + dr["VessPerMon"].ToString() + " ]", myFont, Brushes.Black, new Point(TX + 10, TY + 198), sf);

                g.FillRectangle(brushGreen, new Rectangle(TX, TY + 160 - AccCount, BarWidth, AccCount));
                g.DrawString(Common.CastAsInt32(dr["MajorAccCount"]).ToString(), myFont, new SolidBrush(Color.Black), new Point(TX + 10, TY + 170), sf);
                TX = TX + BarWidth;
                
                g.DrawLine(P, new Point(TX + BarWidth + 1, 454), new Point(TX + BarWidth + 1, 466));

                TX = TX + BarWidth + 16;
            }
            //----- Header
            g.DrawString("Major Accident - " + ddlYear.SelectedValue, myFontHeader, Brushes.Black, new Point(360, 350-25 - (ddlScaleAccident.SelectedIndex * 25)), sf1);
            //----- Vertical Line
            g.DrawLine(P, new Point(X - 21, 350-( ddlScaleAccident.SelectedIndex*25)), new Point(X - 21, Y + 165));

            //10 horizental Line
            int RRH = Y + 165 - 5;
            int Counter = 0,i=0;
            while (Counter <= Common.CastAsInt32(ddlScaleAccident.SelectedValue))
            {
                g.DrawLine(P, new Point(X-30, RRH), new Point(780, RRH));
                g.DrawString(Counter.ToString().PadLeft(3, ' '), myFont, Brushes.Black, new Point(X - 50, RRH - 6), sf1);

                RRH = RRH - 25;
                Counter = Counter + 5;
                i++;
            }
            g.FillRectangle(brushGreen, new Rectangle(400, Y + 230, 15, 15));
            g.DrawString("Major Accident", myFont, Brushes.Black, new Point(420, Y + 230), sf1);

            if (File.Exists("Graph.jpg"))
                File.Delete("Graph.jpg");

            b.Save(Server.MapPath("Graph.jpg"));
            AccidentGraph.ImageUrl = "Graph.jpg?" + r.NextDouble().ToString();
        }
        catch (Exception ex)
        {

        }
    }
    public DataTable RenderingImageDataAccident(int Month)
    {
        string StartDate = "01-" + GetMonthNameByNumber(Month) + "-" + ddlYear.SelectedValue;
        string EndDate = Convert.ToDateTime(StartDate).AddMonths(1).AddDays(-1).ToString();
        string sql = " Select " +
                     "  (Select count(*) from FR_Accident Acc where Acc.AccidentSeverity=3 And Year(ReportDate)=" + ddlYear.SelectedValue + " And Month(ReportDate)=" +Month.ToString()+ " )MajorAccCount " +
                     " , (select Count(*) from dbo.Vessel Where (EffectiveDate <= '" + EndDate + "' and ReleaseDate is null) or (EffectiveDate <='" + EndDate + "' and ReleaseDate >='" + StartDate + "' ))VessPerMon";
        DataSet ds = VesselReporting.getTable(sql);
        return ds.Tables[0];
    }
    public void BindAccidentForImport()
    {
        string sql = "  Exec sp_GetKPIAccidentDataImport " + ddlYear.SelectedValue;
        DataSet ds = VesselReporting.getTable(sql);

        grdAccidentDataForImport.DataSource = ds;
        grdAccidentDataForImport.DataBind();
    }

    // --------------------- Fatality Values and Calculation
    public string GetFatalityValues(string ColumnName)
    {
        string ValueToReturn = "";

        string VesselFileter = "";
        if (ddlVessel.SelectedIndex != 0)
            VesselFileter = " And VesselID=" + ddlVessel.SelectedValue;
        else if (ddlFleet.SelectedIndex != 0)
        {
            VesselFileter = " And VesselID in(select VesselID from dbo.Vessel Where FleetID=" + ddlFleet.SelectedValue + ")";
        }

        if (ColumnName == "Recd")
        {
            string sql = "Select Count(*) from dbo.CrewOnVesselHistory Where SignOffReasonID=7 And SignOnSignOff='O' And Year(SignOffDate)=" + ddlYear.SelectedValue + VesselFileter;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt != null)
                ValueToReturn = dt.Rows[0][0].ToString();
        }
        else
        {
            if (Common.CastAsInt32(GetMonthNoByName(ColumnName)) > DateTime.Today.Month && Common.CastAsInt32(ddlYear.SelectedValue)==DateTime.Today.Year )
                ValueToReturn = "0";
            else
            {
                string sql = "Select Count(*) from dbo.CrewOnVesselHistory Where SignOffReasonID=7 And SignOnSignOff='O' And Month(SignOffDate)=" + GetMonthNoByName(ColumnName) + " And Year(SignOffDate)=" + ddlYear.SelectedValue + " " + VesselFileter;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt != null)
                    ValueToReturn = dt.Rows[0][0].ToString();
            }
        }

        return ValueToReturn;
    }
    public void BindFatalityPerVessel()
    {
        string sql = "Select VesselCode,VesselName  " +
                     " ,(Select count(*) from dbo.CrewOnVesselHistory CH Where SignOffReasonID=7 And SignOnSignOff='O' And CH.VesselID=v.VesselID And Year(SignOffDate)=" + ddlYear.SelectedValue + "  )FatalityCount " +
                    " from dbo.Vessel v Where v.VesselStatusid<>2 ";
        sql = sql + " ORDER BY VESSELNAME";
        DataSet ds = VesselReporting.getTable(sql);

        rptFatality.DataSource = ds;
        rptFatality.DataBind();
    }

    // --------------------- FlawLess Performance Values and Calculation
    public string GetFlawLessValues(string ColumnName)
    {
        string ValueToReturn = "";

        string VesselFileter = "";
        if (ddlVessel.SelectedIndex != 0)
            VesselFileter = " And VesselID=" + ddlVessel.SelectedValue;
        else if (ddlFleet.SelectedIndex != 0)
        {
            VesselFileter = " And VesselID in(select VesselID from dbo.Vessel Where FleetID=" + ddlFleet.SelectedValue + ")";
        }

        if (ColumnName == "Recd")
        {
            string sql = " select count(isnull(O.InspectionDueID,-1)) from T_inspectionDue ID  " +
                       " Left join t_Observations O on O.InspectionDueID=ID.ID  " +
                       "  Where ID.InspectionID in(Select ID from m_Inspection Where InspectionGroup=4) " +
                       " And InspectionDueID is null And Year(ActualDate)=" + ddlYear.SelectedValue + " ";

            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt != null)
                ValueToReturn = dt.Rows[0][0].ToString();
        }
        else
        {
            if (Common.CastAsInt32(GetMonthNoByName(ColumnName)) > DateTime.Today.Month && Common.CastAsInt32(ddlYear.SelectedValue) == DateTime.Today.Year)
                ValueToReturn = "0";
            else
            {
                string sql = " select count(isnull(O.InspectionDueID,-1)) from T_inspectionDue ID  "+
                       " Left join t_Observations O on O.InspectionDueID=ID.ID  " +
                       "  Where ID.InspectionID in(Select ID from m_Inspection Where InspectionGroup=4) "+
                       " And InspectionDueID is null And Month(ActualDate)=" + GetMonthNoByName(ColumnName) + " And Year(ActualDate)="+ddlYear.SelectedValue+" ";

                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt != null)
                    ValueToReturn = dt.Rows[0][0].ToString();
            }
        }

        return ValueToReturn;
    }
    public void BindFlawlessPerVessel()
    {
        string sql = "Select VesselCode,VesselName  " +
                     " ,( select count(*) from T_inspectionDue ID  Left join t_Observations O on O.InspectionDueID=ID.ID  Where ID.InspectionID in(Select ID from m_Inspection Where InspectionGroup=4) And ID.VesselID=v.VesselID And InspectionDueID is null And Year(ActualDate)=" + ddlYear.SelectedValue +" ) FlawLessCount "+
                    " from dbo.Vessel v Where v.VesselStatusid<>2 ";
        sql = sql + " ORDER BY VESSELNAME";
        DataSet ds = VesselReporting.getTable(sql);

        rptFlawLess.DataSource = ds;
        rptFlawLess.DataBind();
    }

    // --------------------- PSC Detention
    public string GetPSCDetentionValues(string ColumnName)
    {
        string ValueToReturn = "";

        string VesselFileter = "";
        if (ddlVessel.SelectedIndex != 0)
            VesselFileter = " And VesselID=" + ddlVessel.SelectedValue;
        else if (ddlFleet.SelectedIndex != 0)
        {
            VesselFileter = " And VesselID in(select VesselID from dbo.Vessel Where FleetID=" + ddlFleet.SelectedValue + ")";
        }

        if (ColumnName == "Recd")
        {
            string sql = " Select Count(*) from t_InspectionDue ID Inner Join t_Observations O on O.InspectionDueID=ID.ID  " +
                             " Where ID.InspectionID in(Select ID from m_Inspection Where InspectionGroup=4) And O.PscCOde in(select ID from dbo.m_psccode Where PscCode=30)  " +
                             " And Year(ID.ActualDate)=" + ddlYear.SelectedValue ;

            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dt != null)
                ValueToReturn = dt.Rows[0][0].ToString();
        }
        else
        {
            if (Common.CastAsInt32(GetMonthNoByName(ColumnName)) > DateTime.Today.Month && Common.CastAsInt32(ddlYear.SelectedValue) == DateTime.Today.Year)
                ValueToReturn = "0";
            else
            {
                string sql = " Select Count(*) from t_InspectionDue ID Inner Join t_Observations O on O.InspectionDueID=ID.ID  "+
                             " Where ID.InspectionID in(Select ID from m_Inspection Where InspectionGroup=4) And O.PscCOde in(select ID from dbo.m_psccode Where PscCode=30) " +
                             " And Year(ID.ActualDate)=" + ddlYear.SelectedValue + " And Month(ID.ActualDate)=" + GetMonthNoByName(ColumnName) + "";

                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt != null)
                    ValueToReturn = dt.Rows[0][0].ToString();
            }
        }

        return ValueToReturn;
    }
    public void BindPSCDetentionPerVessel()
    {
        string sql = " Select VesselCode,VesselName  " +
                     " , ( Select Count(*) from t_InspectionDue ID Inner Join t_Observations O on O.InspectionDueID=ID.ID  Where ID.VesselID=v.VesselID And ID.InspectionID in(Select ID from m_Inspection Where InspectionGroup=4) And O.PscCOde in(select ID from dbo.m_psccode Where PscCode=30) And Year(ID.ActualDate)=" + ddlYear.SelectedValue +" ) PscCounter"+
                     " from dbo.Vessel v Where v.VesselStatusid<>2 ";
        sql = sql + " ORDER BY VESSELNAME";
        
        DataSet ds = VesselReporting.getTable(sql);

        rptPSCDetention.DataSource = ds;
        rptPSCDetention.DataBind();
    }
    // --------------------- Audit Inspection    
    public string GetAuditInspectionValues(string ColumnName)
    {
        string ValueToReturn = "";

        string VesselFileter = "";
        if (ddlVessel.SelectedIndex != 0)
            VesselFileter = " And VesselID=" + ddlVessel.SelectedValue;
        else if (ddlFleet.SelectedIndex != 0)
        {
            VesselFileter = " And VesselID in(select VesselID from dbo.Vessel Where FleetID=" + ddlFleet.SelectedValue + ")";
        }

        if (ColumnName == "Recd")
        {
            //string sql = " Select Count(*) from t_InspectionDue ID Inner Join t_Observations O on O.InspectionDueID=ID.ID  " +
            //                 " Where ID.InspectionID in(Select ID from m_Inspection Where InspectionGroup=4) And O.PscCOde in(select ID from dbo.m_psccode Where PscCode=30)  " +
            //                 " And Year(ID.ActualDate)=" + ddlYear.SelectedValue;

            //DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            //if (dt != null)
            //    ValueToReturn = dt.Rows[0][0].ToString();
            ValueToReturn = TotalAuditInspection.ToString();
        }
        else
        {
            if (Common.CastAsInt32(GetMonthNoByName(ColumnName)) > DateTime.Today.Month && Common.CastAsInt32(ddlYear.SelectedValue) == DateTime.Today.Year)
                ValueToReturn = "0";
            else
            {
                Common.Set_Procedures("dbo.sp_AuditInspectionOnTime");
                Common.Set_ParameterLength(4);
                Common.Set_Parameters(
                        new MyParameter("@FleetID", Common.CastAsInt32( ddlFleet.SelectedValue)),
                        new MyParameter("@ParameterVesselID", Common.CastAsInt32( ddlVessel.SelectedValue)),
                        new MyParameter("@Month", GetMonthNoByName(ColumnName)),
                        new MyParameter("@Year", ddlYear.SelectedValue)
                    );
                DataSet Ds = Common.Execute_Procedures_Select();

                if (Ds != null)
                    ValueToReturn = Ds.Tables[0].Rows[0][0].ToString();

                TotalAuditInspection = TotalAuditInspection + Common.CastAsInt32(ValueToReturn);
            }
        }

        return ValueToReturn;
    }
    public void BindAuditInspctionVessel()
    {
        Common.Set_Procedures("dbo.sp_GetAuditInspectionPerVessel");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
                new MyParameter("@YEAR", ddlYear.SelectedValue)
            );
        DataSet ds = Common.Execute_Procedures_Select();
        rptAuditInspection.DataSource = ds;
        rptAuditInspection.DataBind();
    }
    // --------------------- LTIF
    public string GetLTIFValues(string ColumnName)
    {
        string ValueToReturn = "";
        if (ColumnName == "Recd")
        {
            ValueToReturn = Math.Round((SumLtiByMon * 1000000) / SumLtifEH, 2).ToString();
        }
        else
        {
            //if (Common.CastAsInt32(GetMonthNoByName(ColumnName)) > DateTime.Today.Month && Common.CastAsInt32(ddlYear.SelectedValue) == DateTime.Today.Year)
            //    ValueToReturn = "0";
            //else
            //{
                Common.Set_Procedures("snq.dbo.sp_GetLTIFByMonth");
                Common.Set_ParameterLength(4);
                Common.Set_Parameters(
                        new MyParameter("@FleetID", Common.CastAsInt32(ddlFleet.SelectedValue)),
                        new MyParameter("@VesselID", Common.CastAsInt32(ddlVessel.SelectedValue)),
                        new MyParameter("@Month", GetMonthNoByName(ColumnName)),
                        new MyParameter("@Year", ddlYear.SelectedValue)
                    );
                DataSet Ds = Common.Execute_Procedures_Select();

                if (Ds != null)
                    ValueToReturn = Ds.Tables[0].Rows[0][0].ToString();

                SumLtifEH = SumLtifEH + Common.CastAsDecimal(Ds.Tables[0].Rows[0]["EH"]);
                SumLtiByMon = SumLtiByMon + Common.CastAsDecimal(Ds.Tables[0].Rows[0]["LTIFYTD"]);
            //}
        }
        return ValueToReturn;
    }
    public void BindLTIFPopup()
    {
        string sql = "Exec sp_GetLTIFByMonthDescription " + ddlFleet.SelectedValue + "," + ddlVessel.SelectedValue + ","+ddlYear.SelectedValue;
        DataSet ds = VesselReporting.getTable(sql);

        rptLTIF.DataSource = ds;
        rptLTIF.DataBind();

        lblLTRIFSumEH.Text = ds.Tables[1].Rows[0]["SumEH"].ToString();
        lblLTRIFSumLTIByMonth.Text = ds.Tables[1].Rows[0]["SumTotLTIByMonth"].ToString();
        lblLTRIFYTD.Text = ds.Tables[1].Rows[0]["FinalLTIFYtd"].ToString();

    }

    public void GenerateLTIFGraph()
    {
        try
        {
            DataTable Dt = new DataTable();

            StringFormat sf1 = new StringFormat();
            sf1.Alignment = StringAlignment.Near;
            sf1.LineAlignment = StringAlignment.Near;

            Font myFont = new System.Drawing.Font("Helvetica", 8, FontStyle.Regular);
            Font myFontHeader = new System.Drawing.Font("Helvetica", 12, FontStyle.Bold);

            Type t = typeof(Brushes);
            Brush brushGreen = (Brush)t.GetProperty("LightGreen").GetValue(null, null);
            Brush brushRed = (Brush)t.GetProperty("LightPink").GetValue(null, null);
            Brush brushBlue = (Brush)t.GetProperty("LightSkyBlue").GetValue(null, null);

            Bitmap b = new Bitmap(1050, 450);
            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int M;
            int X = 120;
            int Y = 150;
            int BarWidth = 20;

            int LTIFByMonth = 0;
            int LTIFYTD = 0;

            Color VC;
            VC = System.Drawing.Color.LightGray;
            Pen P = new Pen(VC, 1);
            P.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Dt = RenderingImageDataLTIF();

            int TX = X;
            int TY = Y;
            for (M = 1; M <= 12; M++)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                DataRow dr;
                try
                {
                    dr = Dt.Rows[M-1];
                    LTIFByMonth = Common.CastAsInt32(dr["LTIFByMonth"])*10 ;
                    LTIFYTD = Common.CastAsInt32(dr["LTIFYTD"])*10 ;
                }
                catch(Exception ex)
                {
                    LTIFByMonth = 0;
                    LTIFYTD = 0;
                    dr = Dt.NewRow();
                }
                //----------------------
                g.DrawString(GetMonthNameByNumber(M), myFont, Brushes.Black, new Point(TX + 20, TY + 170), sf);
                //g.DrawString(" [ " + dr["VessPerMon"].ToString() + " ]", myFont, Brushes.Black, new Point(X + 30, Y + 198), sf);

                g.FillRectangle(brushGreen, new Rectangle(TX, TY + 160 - LTIFByMonth, BarWidth, LTIFByMonth));
                g.DrawString(dr["LTIFByMonth"].ToString(), myFont, new SolidBrush(Color.Black), new Point(TX + 20, TY + 165 + 25), sf);
                g.DrawString(dr["LTIFYTD"].ToString(), myFont, Brushes.Black, new Point(TX + 20, TY +165 + 45), sf);
                TX = TX + BarWidth;

                g.FillRectangle(brushRed, new Rectangle(TX, TY + 160 - LTIFYTD, BarWidth, LTIFYTD));
                
                TX = TX + BarWidth;

                g.DrawLine(P, new Point(TX + BarWidth + 1, 300), new Point(TX + BarWidth + 1, 380));

                TX = TX + BarWidth + 16;
            }

            //10 horizental Line
            int RRH = Y + 225 - 5;
            int Counter = 0,i=0;
            while (Counter<=Common.CastAsInt32(ddlLtifScale.SelectedValue))
            {
                g.DrawLine(P, new Point(X - 30, RRH), new Point(1025, RRH));

                if (i > 2)
                {
                    g.DrawString(Counter.ToString().PadLeft(3, ' '), myFont, Brushes.Black, new Point(X - 50, RRH - 6), sf1);
                    g.DrawLine(P, new Point(X - 30, RRH), new Point(1025, RRH));
                    Counter = Counter + 2;
                }
                else
                {
                    g.DrawLine(P, new Point(0, RRH), new Point(1025, RRH));
                }
                RRH = RRH - 20;
                i++;
            }
            //----- Header
            g.DrawString("LTIF " + ddlYear.SelectedValue, myFontHeader, Brushes.Black, new Point(360, 170-(ddlLtifScale.SelectedIndex*20)), sf1);
            //Vertical Line
            g.DrawLine(P, new Point(X - 20, 265 - (ddlLtifScale.SelectedIndex * 20)), new Point(X - 20, Y + 165 + 60)); 


            //g.DrawLine(P, new Point(5, Y + 165 - 5 + 20), new Point(945, Y + 165 - 5 + 20));
            g.DrawString("LTIF By Month", myFont, Brushes.Black, new Point(1, Y + 165 - 5 + 25), sf1);

            //g.DrawLine(P, new Point(5, Y + 165 - 5 + 40), new Point(945, Y + 165 - 5 + 40));
            g.DrawString("LTIF(Year to Date)", myFont, Brushes.Black, new Point(1, Y + 165 - 5 + 45), sf1);

            //g.DrawLine(P, new Point(5, Y + 165 - 5 + 60), new Point(945, Y + 165 - 5 + 60));

            //----- Legend
            g.FillRectangle(brushGreen, new Rectangle(300, Y + 240, 15, 15));
            g.DrawString("LTIF By  Month", myFont, Brushes.Black, new Point(320, Y + 240), sf1);
            g.FillRectangle(brushRed, new Rectangle(430, Y + 240, 15, 15));
            g.DrawString("LTIF YTD", myFont, Brushes.Black, new Point(450, Y + 240), sf1);

            if (File.Exists("Graph.jpg"))
                File.Delete("Graph.jpg");

            b.Save(Server.MapPath("Graph.jpg"));
            LtifGraph.ImageUrl = "Graph.jpg?" + r.NextDouble().ToString();
        }
        catch (Exception ex)
        {

        }
    }
    public DataTable RenderingImageDataLTIF()
    {
        string sql = "Exec sp_GetLTIFByMonthDescription " + ddlFleet.SelectedValue + "," + ddlVessel.SelectedValue + "," + ddlYear.SelectedValue;
        DataSet ds = VesselReporting.getTable(sql);
        return ds.Tables[0];
    }

    // --------------------- TRCF
    public string GetTRCFValues(string ColumnName)
    {
        string ValueToReturn = "";
        if (ColumnName == "Recd")
        {
            ValueToReturn = Math.Round((TotalTRC * 1000000) / TotalEH,2).ToString();
        }
        else
        {
            //if (Common.CastAsInt32(GetMonthNoByName(ColumnName)) > DateTime.Today.Month && Common.CastAsInt32(ddlYear.SelectedValue) == DateTime.Today.Year)
            //    ValueToReturn = "0";
            //else
            //{
                Common.Set_Procedures("dbo.sp_GetTRCFByMonth");
                Common.Set_ParameterLength(4);
                Common.Set_Parameters(
                        new MyParameter("@FleetID", Common.CastAsInt32(ddlFleet.SelectedValue)),
                        new MyParameter("@VesselID", Common.CastAsInt32(ddlVessel.SelectedValue)),
                        new MyParameter("@Month", GetMonthNoByName(ColumnName)),
                        new MyParameter("@Year", ddlYear.SelectedValue)
                    );
                DataSet Ds = Common.Execute_Procedures_Select();

                if (Ds != null)
                    ValueToReturn = Ds.Tables[0].Rows[0]["TRCFByMonth"].ToString();

                TotalTRC = TotalTRC + Common.CastAsDecimal(Ds.Tables[0].Rows[0]["TRC"]);
                TotalEH = TotalEH + Common.CastAsDecimal(Ds.Tables[0].Rows[0]["EH"]);
            //}
        }
        return ValueToReturn;
    }
    public void BindTRCFPopup()
    {
        string sql = "Exec sp_GetTRCFByMonthDescription "+ddlFleet.SelectedValue+","+ddlVessel.SelectedValue+","+ddlYear.SelectedValue;
        DataSet ds = VesselReporting.getTable(sql);

        rptTRCF.DataSource = ds;
        rptTRCF.DataBind();

        lblSumEH.Text = ds.Tables[1].Rows[0]["SumEH"].ToString();
        lblSumOfLTIByMonth.Text = ds.Tables[1].Rows[0]["SumTotLTIByMonth"].ToString();
        lblFinalLTIDYtd.Text = ds.Tables[1].Rows[0]["FinalTRCFYtd"].ToString();
        
    }

    public void GenerateTRCFGraph()
    {
        try
        {
            DataTable Dt = new DataTable();

            StringFormat sf1 = new StringFormat();
            sf1.Alignment = StringAlignment.Near;
            sf1.LineAlignment = StringAlignment.Near;

            Font myFont = new System.Drawing.Font("Helvetica", 8, FontStyle.Regular);
            Font myFontHeader = new System.Drawing.Font("Helvetica", 12, FontStyle.Bold);

            Type t = typeof(Brushes);
            Brush brushGreen = (Brush)t.GetProperty("LightGreen").GetValue(null, null);
            Brush brushRed = (Brush)t.GetProperty("LightPink").GetValue(null, null);
            Brush brushBlue = (Brush)t.GetProperty("LightSkyBlue").GetValue(null, null);

            Bitmap b = new Bitmap(1060, 450);
            Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int M;
            int X = 125;
            int Y = 150;
            int BarWidth = 20;

            int LTIFByMonth = 0;
            int LTIFYTD = 0;

            Color VC;
            VC = System.Drawing.Color.LightGray;
            Pen P = new Pen(VC, 1);
            P.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Dt = RenderingImageDataTRCF();
            int TX = X;
            int TY = Y;
            for (M = 1; M <= 12; M++)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                DataRow dr;
                try
                {
                    dr = Dt.Rows[M - 1];
                    LTIFByMonth = Common.CastAsInt32(dr["TRCFByMonth"]) * 10;
                    LTIFYTD = Common.CastAsInt32(dr["TRCFYTD"]) * 10;
                }
                catch (Exception ex)
                {
                    LTIFByMonth = 0;
                    LTIFYTD = 0;
                    dr = Dt.NewRow();
                }
                //----------------------
                g.DrawString(GetMonthNameByNumber(M), myFont, Brushes.Black, new Point(TX + 20, TY + 170), sf);
                //g.DrawString(" [ " + dr["VessPerMon"].ToString() + " ]", myFont, Brushes.Black, new Point(X + 30, Y + 198), sf);

                g.FillRectangle(brushGreen, new Rectangle(TX, TY + 160 - LTIFByMonth, BarWidth, LTIFByMonth));
                g.DrawString(dr["TRCFByMonth"].ToString(), myFont, new SolidBrush(Color.Black), new Point(TX + 20, TY + 165 + 25), sf);
                g.DrawString(dr["TRCFYTD"].ToString(), myFont, Brushes.Black, new Point(TX + 20, TY + 165 + 45), sf);
                TX = TX + BarWidth;

                g.FillRectangle(brushRed, new Rectangle(TX, TY + 160 - LTIFYTD, BarWidth, LTIFYTD));
                TX = TX + BarWidth;

                g.DrawLine(P, new Point(TX + BarWidth + 1, 300), new Point(TX + BarWidth + 1, 380));
                TX = TX + BarWidth + 16;
            }

            //----- Header
            g.DrawString("TRCF " + ddlYear.SelectedValue, myFontHeader, Brushes.Black, new Point(560, 170 - (ddlTrcfScale.SelectedIndex * 20)), sf1);
            //g.DrawString(ddlYear.SelectedValue, myFontHeader, Brushes.Black, new Point(460, 30), sf1);

            //Vertical Line
            g.DrawLine(P, new Point(X - 20, 225 - (ddlTrcfScale.SelectedIndex * 20)), new Point(X - 20, Y + 165+60));

            //10 horizental Line
            int RRH = Y + 225 - 5;
            int Counter = 0, i = 0;
            while (Counter <= Common.CastAsInt32(ddlTrcfScale.SelectedValue))
            {
                g.DrawLine(P, new Point(X - 30, RRH), new Point(1025, RRH));

                if (i > 2)
                {
                    g.DrawString(Counter.ToString().PadLeft(3, ' '), myFont, Brushes.Black, new Point(X - 50, RRH - 6), sf1);
                    g.DrawLine(P, new Point(X - 30, RRH), new Point(1025, RRH));
                    Counter = Counter + 2;
                }
                else
                {
                    g.DrawLine(P, new Point(0, RRH), new Point(1025, RRH));
                }
                RRH = RRH - 20;
                i++;
            }

            g.DrawString("TRCF By Month", myFont, Brushes.Black, new Point(1, Y + 165 - 5 + 25), sf1);
            g.DrawString("TRCF(Year to Date)", myFont, Brushes.Black, new Point(1, Y + 165 - 5 + 45), sf1);

            //---- Legend
            g.FillRectangle(brushGreen, new Rectangle(400, Y + 240, 15, 15));
            g.DrawString("TRCF By  Month", myFont, Brushes.Black, new Point(420, Y + 240), sf1);

            g.FillRectangle(brushRed, new Rectangle(530, Y + 240, 15, 15));
            g.DrawString("TRCF YTD", myFont, Brushes.Black, new Point(550, Y + 240), sf1);

            if (File.Exists("Graph.jpg"))
                File.Delete("Graph.jpg");

            b.Save(Server.MapPath("Graph.jpg"));
            TRCFGraph.ImageUrl = "Graph.jpg?" + r.NextDouble().ToString();
        }
        catch (Exception ex)
        {

        }
    }
    public DataTable RenderingImageDataTRCF()
    {
        string sql = "Exec sp_GetTRCFByMonthDescription " + ddlFleet.SelectedValue + "," + ddlVessel.SelectedValue + "," + ddlYear.SelectedValue;
        DataSet ds = VesselReporting.getTable(sql);
        return ds.Tables[0];
    }
    
    // --------------------- Polution
    public void BindPolutionVessel()
    {
        string sql = "Select VesselCode,VesselName  " +
                     " ,(Select count(*) from FR_Accident Acc where ACCIDENTCATEGORY LIKE '%1%' AND OFFICECATEGORY LIKE '%4%' And Acc.VesselID=v.VesselID And Year(ReportDate)=" + ddlYear.SelectedValue + "  )PolutionCount " +
                    " from dbo.Vessel v Where v.VesselStatusid<>2 ";
        sql = sql + " ORDER BY VESSELNAME";
        DataSet ds = VesselReporting.getTable(sql);

        rptPolution.DataSource = ds;
        rptPolution.DataBind();
    }

    //CREW SIGN OFF MEDICAL GROUND
    public void BindCrewSignOffMedicalPerVessel()
    {
        string sql = " Select VesselCode,VesselName  " +
                     " , (Select Count(*) from dbo.CrewOnVesselHistory CH  Where SignOnSignOff='O'  And SignOffReasonID=13 And Year(SignOffDate)=2010 And CH.VesselID =v.vesselid  )CrewSignOffCounter" +
                     " from dbo.Vessel v Where v.VesselStatusid<>2 ";
        sql = sql + " ORDER BY VESSELNAME";
        DataSet ds = VesselReporting.getTable(sql);  

        rptSignOffMedical.DataSource = ds;
        rptSignOffMedical.DataBind();
    }
    
    //CREW SIGN OFF MEDICAL GROUND
    public void BindRetentionRatePerVessel()
    {
        string sql = "EXEC sp_GetRetentionRate " + ddlYear.SelectedValue;
        DataSet ds = VesselReporting.getTable(sql);

        rptRetentionRate.DataSource = ds;
        rptRetentionRate.DataBind();
    }
  }

