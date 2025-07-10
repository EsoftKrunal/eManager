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

public partial class Ship_RunningHour : System.Web.UI.Page
{
    public string MainId
    {
        get { try { return ViewState["MainId"].ToString(); } catch { return ""; } }
        set { ViewState["MainId"] = value; }
    }
    public string MainCode
    {
        get { try { return ViewState["MainCode"].ToString(); } catch { return ""; } }
        set { ViewState["MainCode"] = value; }
    }
    public int LastRunningHr
    {
        get { try { return Common.CastAsInt32(ViewState["LastRunningHr"]); } catch { return 0; } }
        set { ViewState["LastRunningHr"] = value; }
    }
    public string Mode
    {
        get { try { return ViewState["Mode"].ToString(); } catch { return ""; } }
        set { ViewState["Mode"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["CurrentShip"].ToString()
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //***********Code to check page acessing Permission
        if (!Page.IsPostBack)
        {
            int i = DateTime.Today.Year;
            for (; i >= 2010;i--)
            {
                ddlyears.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            Session["CurrentModule"] = 7;
            ShowRunningHourMaster();

            divRHEntry.Visible = (Session["UserName"].ToString().Trim().ToUpper() == "CE" || Session["UserName"].ToString().Trim().ToUpper() == "DEMO");
        }
    }

    #region ---------------- Assign Running Hour ---------------------
    public void ShowRunningHourMaster()
    {
        string strRunningHourSQL = "SELECT row_number() over(order by B.ComponentCode) as SNO,(SELECT ShipName FROM Settings WHERE ShipCode = '" + Session["CurrentShip"].ToString() + "') AS VesselName, " + 
                                "*, " +
                                "StartDate=(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(StartDate,''),106),' ','-') AS StartDate FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "'  AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC ), " +
                                "StartupHour=(SELECT TOP 1 StartupHour FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC ),  " +
                                "AvgRunningHrPerDay=(SELECT TOP 1 AvgRunningHrPerDay FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC ),  " +
                                "(select UserName  from ShipUserMaster where UserId IN (SELECT TOP 1 UpdatedBy FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC )) AS  UpdatedBy,  " +
                                "(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(UpdatedOn,''),106),' ','-')  FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC )  AS UpdatedOn,  " +
                                "(SELECT TOP 1 UpdatedOn FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC )  AS UpdatedOn1 ,B.CriticalType   " +
                                "FROM  " +
                                "( " +
                                "SELECT DISTINCT CM.ComponentId ,CM.ComponentCode,CM.ComponentName,CM.CriticalType " +
                                "FROM  " +
                                "COMPONENTMASTER CM " +
                                "INNER JOIN  " +
                                "( " +
                                "    SELECT COMPONENTCODE  " +
                                "    FROM ComponentMaster CM1  " +
                                "    WHERE CM1.ComponentId IN  " +
                                "    ( " +
                                "        SELECT DISTINCT ComponentId FROM VSL_VesselComponentJobMaster WHERE IntervalId = 1 AND Status='A' AND VesselCode = '" + Session["CurrentShip"].ToString() + "' " +
                                "    ) " +
                                ") A " +
                                "ON LEFT(A.COMPONENTCODE,LEN(CM.COMPONENTCODE))=CM.COMPONENTCODE " +
                                ") B WHERE ( (LEN(B.COMPONENTCODE)=3 AND B.COMPONENTCODE NOT IN ('651','411','334','601')) OR ( LEN(B.COMPONENTCODE)=6 AND ( LEFT(B.COMPONENTCODE,3) IN ('651','411','334') ) ) )order by B.ComponentCode ";

        rptRunningHourMaster.DataSource = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.Office_GteRunningHrs '" + Session["CurrentShip"].ToString() + "'," + DateTime.Today.Year.ToString());
        rptRunningHourMaster.DataBind();
    }
    public void ShowRunningHourDetails()
    {
        //string strRunningHourSQL = "";
        //strRunningHourSQL = "SELECT (SELECT ShipName FROM Settings WHERE ShipCode = '" + Session["CurrentShip"].ToString() + "') AS VesselName,CM.ComponentId ,CM.ComponentCode,CM.ComponentName,StartDate=(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(StartDate,''),106),' ','-') AS StartDate FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "'  AND VRH.ComponentId = CM.ComponentId ORDER BY UpdatedOn DESC ), " +
        //                        "StartupHour=(SELECT TOP 1 StartupHour FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = CM.ComponentId ORDER BY UpdatedOn DESC ), " +
        //                        "AvgRunningHrPerDay=(SELECT TOP 1 AvgRunningHrPerDay FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = CM.ComponentId ORDER BY UpdatedOn DESC ), " +
        //                        "(select UserName  from ShipUserMaster where UserId IN (SELECT TOP 1 UpdatedBy FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = CM.ComponentId ORDER BY UpdatedOn DESC )) AS  UpdatedBy, " +
        //                        "(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(UpdatedOn,''),106),' ','-')  FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = CM.ComponentId ORDER BY UpdatedOn DESC )  AS UpdatedOn, " +
        //                        "(SELECT TOP 1 UpdatedOn FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND VRH.ComponentId = CM.ComponentId ORDER BY UpdatedOn DESC )  AS UpdatedOn1 " +
        //                        //",CM.CriticalType,'" + txtRhrs.Text + "' as NewHr,'" + txtDate.Text + "' as NewSt,'" + txtAvg.Text + "' as NewAvg " +
        //                        ",CM.CriticalType,'' as NewHr,'' as NewSt,'' as NewAvg " +
        //                        "FROM ComponentMaster CM WHERE CM.ComponentId IN (SELECT DISTINCT ComponentId FROM VSL_VesselComponentJobMaster WHERE IntervalId = 1 AND Status='A' AND VesselCode = '" + Session["CurrentShip"].ToString() + "') AND LEFT(CM.ComponentCode," + MainCode.Length.ToString() + ")='" + MainCode + "' and LEN(CM.ComponentCode)>" + MainCode.Length.ToString() + "";
        //strRunningHourSQL += " order by CM.ComponentCode ";
        //Session.Add("sSqlForPrint", Session["CurrentShip"].ToString());
        //DataTable dtRunningHour = Common.Execute_Procedures_Select_ByQuery(strRunningHourSQL);
        //if (dtRunningHour.Rows.Count > 0)
        //{
        //    rptRunningHour.DataSource = dtRunningHour;
        //    rptRunningHour.DataBind();
        //    if (Session["UserType"].ToString() == "S")
        //    {
        //        btnRunHSave.Visible = true;
        //    }
        //}
        //else
        //{
        //    rptRunningHour.DataSource = null;
        //    rptRunningHour.DataBind();
        //    if (Session["UserType"].ToString() == "S")
        //    {
        //        btnRunHSave.Visible = false;
        //    }
        //}
    }
    #endregion

    #region ---------------- Events -------------------------------------------
    protected void btnMainSelect_Click(object sender, EventArgs e)
    {
        MainCode = txtMainCode.Text.Trim();
        DataTable dtMainid = Common.Execute_Procedures_Select_ByQuery("select ComponentId from componentmaster where ltrim(rtrim(componentcode))=ltrim(rtrim('" + MainCode.Trim() + "'))");
        if (dtMainid.Rows.Count > 0)
        {
            MainId = dtMainid.Rows[0][0].ToString();
        }
        ShowRunningHourMaster();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvRH');", true);
        //lblCount.Text = "( " + rptRunningHour.Items.Count + " ) records found.";
    }
    #region ----------------- Assign Running Hour -------------------
    protected void btnRunHSave_Click(object sender, EventArgs e)
    {

        //bool IsChecked = false;
        //string CompCode = "";
        //foreach (RepeaterItem Item in rptRunningHour.Items)
        //{
        //    CheckBox chkBox = (CheckBox)Item.FindControl("chkSelect");
        //    if (chkBox.Checked)
        //    {
        //        IsChecked = true;
        //        break;
        //    }
        //}
        //if (!IsChecked)
        //{
        //    ProjectCommon.ShowMessage("Please select a component.");
        //    return;
        //}

        if (MainId.ToString() == "")
        {
            ProjectCommon.ShowMessage("Please select a component.");
            return;
        }

        if (!getLastFilledHours(txtMainCode.Text.Trim()))
        {
            ProjectCommon.ShowMessage("Please update running hrs for " + ViewState["LastMonthLastDt"].ToString());
            return;
        }

        string StartupHour = txtRhrs.Text;
        string StartDate = txtDate.Text;
        string AvgRunHour = txtAvg.Text;

        if (StartupHour.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter new hr.");
            return;
        }

        int i;
        if (!(int.TryParse(StartupHour.Trim(), out i)))
        {
            ProjectCommon.ShowMessage("Please enter valid hrs. (DECIMAL NOT ALLOWED)");
            return;
        }

        if (StartDate.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter changed dt.");
            return;
        }
        DateTime dt;
        if (!DateTime.TryParse(StartDate.Trim(), out dt))
        {
            ProjectCommon.ShowMessage("Please enter valid date.");
            return;
        }
        if (AvgRunHour.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter average hr.");
            return;
        }
        int j;
        if (!int.TryParse(AvgRunHour.Trim(), out j))
        {
            ProjectCommon.ShowMessage("Please enter valid average hr.");
            return;
        }
        if (int.Parse(AvgRunHour.Trim()) > 25)
        {
            ProjectCommon.ShowMessage("Average hr. can not be more than 25.");
            return;
        }

        int LastRunningHr = getLastHours(txtMainCode.Text.Trim());
        if (Common.CastAsInt32(StartupHour.Trim()) < LastRunningHr)
        {
            ProjectCommon.ShowMessage("New Hr. can not be less than last running hour ( " + LastRunningHr.ToString() + " ).");
            return;
        }

        DateTime LastRunningHrDate = DateTime.Parse(getLastHoursDate(txtMainCode.Text.Trim()) == "" ? "01/01/1900" : getLastHoursDate(txtMainCode.Text.Trim()));
        if (DateTime.Parse(StartDate.Trim()) < LastRunningHrDate)
        {
            ProjectCommon.ShowMessage("New Date can not be less than last running hour date ( " + LastRunningHrDate.ToString("dd-MMM-") + " ).");
            return;
        }

        //int LastHistoryHr = getLastHistoryHours(txtMainCode.Text.Trim());
        int LastHistoryHr = getLastHistoryHours(txtMainCode.Text.Trim(), StartDate);
        if (Common.CastAsInt32(StartupHour.Trim()) < LastHistoryHr)
        {
            // Show Message for Confirmation on POPUP -------------- [ History has hours more than you are entering. Do you want to correct hostiry first? ]
            Mode = "H";
            //lblConfirmMsg.Text = "History has hours more than you are entering. Do you want to correct history first? ";
            lblConfirmMsg.Text = "Job history of this component has been updated with higher running hr number (" + LastHistoryHr.ToString() + ") in the past.<br /> Do you want to correct the history record ?";
            divConfirm.Visible = true;

            return;
        }
        //DateTime LastHistoryHrDate = DateTime.Parse(getLastHistoryHoursDate(txtMainCode.Text.Trim()) == "" ? "01/01/1900" : getLastHistoryHoursDate(txtMainCode.Text.Trim()));
        //if (DateTime.Parse(StartDate.Trim()) < LastHistoryHrDate)
        //{
        //    // Show Message for Confirmation on POPUP -------------- [ History has date more than you are entering. Do you want to correct hostiry first? ]
        //    Mode = "D";
        //    //lblConfirmMsg.Text = "History has date more than you are entering. Do you want to correct history first? ";
        //    lblConfirmMsg.Text = "Job history of this component has been updated with higher date (" + LastHistoryHrDate.ToString("dd-MMM-yyyy") + ") in the past.<br /> Do you want to correct the history record ?";
        //    divConfirm.Visible = true;

        //    return;
        //}

        int MaxHours = 0;
        DateTime MaxDate;

        MaxDate = LastRunningHrDate;

        if (LastRunningHr > LastHistoryHr)
        {
            MaxHours = LastRunningHr;
            //MaxDate = LastRunningHrDate;
        }
        else
        {
            MaxHours = LastHistoryHr;
            //MaxDate = LastHistoryHrDate;
        }

        if (MaxHours > 0)
        {
            int MaxIncreasedHrs = DateTime.Parse(StartDate.Trim()).Subtract(MaxDate).Days * 24;
            if (Common.CastAsInt32(StartupHour.Trim()) - MaxHours > MaxIncreasedHrs)
            {
                Mode = "M";
                lblConfirmMsg.Text = "Invalid startup Hrs.( Max Allowed " + MaxIncreasedHrs.ToString() + " due to last update on " + MaxDate.ToString("dd-MMM-yyyy") + " with ( " + MaxHours + " ) hrs. ).<br/> Do you want to continue? ";
                divConfirm.Visible = true;

                return;
            }
        }

        //------------------ CHECKS COMPLETED --------------------------
        SaveRunningHrs();
    }
    protected void SaveRunningHrs()
    {
        object Dt = DBNull.Value;
        string StartupHour = txtRhrs.Text;
        string StartDate = txtDate.Text;
        string AvgRunHour = txtAvg.Text;

        if (StartDate.Trim() != "")
        {
            try
            {
                Dt = Convert.ToDateTime(StartDate.Trim());
            }
            catch { Dt = DBNull.Value; }
        }
        // SAVING RUNNING OF SELECTED COMPONENT SELF
        try
        {
            Common.Set_Procedures("sp_Ship_InsertRunningHour");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", Session["CurrentShip"].ToString()),
                new MyParameter("@ComponentId", Common.CastAsInt32(MainId)),
                new MyParameter("@StartupHour", Common.CastAsInt32(StartupHour.Trim())),
                new MyParameter("@AvgRunningHrPerDay", Common.CastAsInt32(AvgRunHour.Trim())),
                new MyParameter("@StartDate", Dt),
                new MyParameter("@UpdatedBy", Session["loginid"].ToString())
                );
            DataSet dsMainComponents = new DataSet();
            dsMainComponents.Clear();
            Boolean resMain;
            resMain = Common.Execute_Procedures_IUD(dsMainComponents);
            if (resMain)
            {
                MainId = "";
                ProjectCommon.ShowMessage("Running Hour Details Added Successfully.");
            }
            else
            {
                ProjectCommon.ShowMessage("Error saving records. " + Common.ErrMsg);
            }
        }
        catch
        {
            ProjectCommon.ShowMessage("Unable to Add Running Hour Details.Error." + Common.getLastError());
        }
        
        ShowRunningHourMaster();
        //ShowRunningHourDetails();
    }
    #endregion
    // Code By Umakant
    protected void btnPring_Click(object sener, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('OfficeRunningHourPrint.aspx?ReportType=RuningHour');", true);
    }
    #endregion
    protected void Onyear_Changed(object sender, EventArgs e)
    {
        ShowRunningHourMaster();
    }
    public int getConsumedHours(int Month, string CompCode)
    {
        string VSlCode = Session["CurrentShip"].ToString();
        int retSt = 0, retEnd = 0; ;
        DateTime MonthStartDate = new DateTime(Common.CastAsInt32(ddlyears.SelectedValue), Month, 1);
        DateTime MonthEndDate = MonthStartDate.AddMonths(1);

        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDate('" + VSlCode + "','" + CompCode + "','" + MonthStartDate.ToString("dd-MMM-yyyy") + "')");
        DataTable dtEnd = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDate('" + VSlCode + "','" + CompCode + "','" + MonthEndDate.ToString("dd-MMM-yyyy") + "')");
        if (dtStart.Rows.Count > 0)
            retSt = Common.CastAsInt32(dtStart.Rows[0][0]);
        if (dtEnd.Rows.Count > 0)
            retEnd = Common.CastAsInt32(dtEnd.Rows[0][0]);
        return retEnd - retSt;
    }

    public int getLastHours(string CompCode)
    {
        int Hrs = 0;
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDate('" + VSlCode + "','" + CompCode + "',NULL)");
        if (dtStart.Rows.Count > 0)
            Hrs=Common.CastAsInt32(dtStart.Rows[0][0]);
        return Hrs;
    }
    public string getLastHoursDate(string CompCode)
    {
        string LastDt = "";
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT REPLACE(CONVERT(Varchar(11),dbo.getRHAsOnDate('" + VSlCode + "','" + CompCode + "',NULL),106),' ','-')");
        if (dtStart.Rows.Count > 0)
            LastDt = dtStart.Rows[0][0].ToString();
        return LastDt;
    }
    public Boolean getLastFilledHours(string CompCode)
    {
        DateTime MinDate = new DateTime(2012,12,31); 
        try
        {
            DataTable dt_MinDate = Common.Execute_Procedures_Select_ByQuery("select convert(varchar,StartDate,106) from VSL_SHIPSETTINGS");
            if (dt_MinDate != null)
                if (dt_MinDate.Rows.Count > 0)
                {
                    MinDate = Convert.ToDateTime(dt_MinDate.Rows[0][0]);
                }
        }
        catch {  }

        string VSlCode = Session["CurrentShip"].ToString().Trim();

        DateTime Date = Convert.ToDateTime(txtDate.Text.Trim());
        int CurrentMonth = Date.Month;
        int currentYear = Date.Year;
        
        
        DateTime MonthStartDate = new DateTime(currentYear, CurrentMonth, 1);
        DateTime MonthEndDate = MonthStartDate.AddDays(-1);
        

        ViewState["LastMonthLastDt"] = MonthEndDate.ToString("dd-MMM-yyyy");


        string SQL = "SELECT * FROM VSL_VesselRunningHourMaster " +
                     "WHERE VesselCode='" + VSlCode + "' AND ComponentId IN " +
                     "( " +
                     "	SELECT CM.ComponentId FROM COMPONENTMASTER CM " +
                     "	INNER JOIN VSL_COMPONENTMASTERFORVESSEL CMV ON CMV.COMPONENTID=CM.COMPONENTID AND VESSELCODE='" + VSlCode + "' AND LEFT(COMPONENTCODE,LEN('" + CompCode + "'))='" + CompCode + "' AND CMV.STATUS='A' " +
                     "	INNER JOIN VSL_VesselComponentJobMaster VCJM ON CMV.VESSELCODE=VCJM.VESSELCODE AND VCJM.STATUS='A' AND VCJM.IntervalId = 1 " +
                     ") " +
                     "AND YEAR(StartDate)= " + currentYear + " AND MONTH(StartDate) = " + CurrentMonth + " ";

        DataTable dtCheck = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dtCheck.Rows.Count > 0)
        {
            return true;
        }
        
        if (MonthEndDate < MinDate)
        {
            return true;
        }
        else
        {

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnFixedDate('" + VSlCode + "','" + CompCode + "','" + MonthEndDate.ToString("dd-MMM-yyyy") + "')");

            //if (dt.Rows.Count > 0)
            //{
            if (dt.Rows[0][0].ToString() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
            //}
        }
        
    }

    public int getLastHistoryHours(string CompCode)
    {
        int Hrs = 0;
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDateFromHistory('" + VSlCode + "','" + CompCode + "',NULL)");
        if (dtStart.Rows.Count > 0)
            Hrs = Common.CastAsInt32(dtStart.Rows[0][0]);
        return Hrs;
    }
    public int getLastHistoryHours(string CompCode,string OnDate)
    {
        int Hrs = 0;
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDateFromHistory('" + VSlCode + "','" + CompCode + "','" + OnDate + "')");
        if (dtStart.Rows.Count > 0)
            Hrs = Common.CastAsInt32(dtStart.Rows[0][0]);
        return Hrs;
    }
    public string getLastHistoryHoursDate(string CompCode)
    {
        string LastDt = "";
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT REPLACE(CONVERT(Varchar(11),dbo.getRunningHrsOnDateFromHistoryDate('" + VSlCode + "','" + CompCode + "',NULL),106),' ','-')");
        if (dtStart.Rows.Count > 0)
            LastDt = dtStart.Rows[0][0].ToString();
        return LastDt;
    }

    protected void lnkEditLasthr_Click(object sender, EventArgs e)
    {
        MainCode = ((LinkButton)sender).CommandArgument;
        LastRunningHr = Common.CastAsInt32(((LinkButton)sender).Text.ToString().Split('/').GetValue(0).ToString().Trim());
        ViewState["LastRunningHrDt"] = ((LinkButton)sender).Text.ToString().Split('/').GetValue(1).ToString().Trim();
        lblLastDt.Text = ViewState["LastRunningHrDt"].ToString();

        if (LastRunningHr != 0)
        {
            divEditRH.Visible = true;
        }
    }

    protected void btnEditRH_Click(object sender, EventArgs e)
    {
        if (txtEditRunningHrs.Text.Trim() == "")
        {
            lblEditRHMsg.Text = "Please enter running hrs.";
            txtEditRunningHrs.Focus();
            return;
        }
        int i;
        if (!int.TryParse(txtEditRunningHrs.Text.Trim(), out i))
        {
            lblEditRHMsg.Text = "Please enter valid hrs.";
            txtEditRunningHrs.Focus();
            return;
        }

        string SQL = "SELECT dbo.getRunningHrsOnDate_ExcludeLast('" + Session["CurrentShip"].ToString() + "','" + MainCode + "','" + ViewState["LastRunningHrDt"].ToString() + "'," + Common.CastAsInt32(txtEditRunningHrs.Text.Trim()) + ")";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (Common.CastAsInt32(txtEditRunningHrs.Text.Trim()) < Common.CastAsInt32(dt.Rows[0][0].ToString()))
        {
            lblEditRHMsg.Text = "Running hrs can not be less than last running hrs (" + dt.Rows[0][0].ToString() + ").";
            return;
        }

        Common.Set_Procedures("UpdateRunningHrs");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
            new MyParameter("@VSLID", Session["CurrentShip"].ToString()),
            new MyParameter("@COMPCODE", MainCode),
            new MyParameter("@OLDHRS", LastRunningHr),
            new MyParameter("@HRS", Common.CastAsInt32(txtEditRunningHrs.Text.Trim())),
            new MyParameter("@UpdatedBy", Session["loginid"].ToString())
            );
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD(ds))
        {
            lblEditRHMsg.Text = "Running hrs updated successfully.";            
            ShowRunningHourMaster();
            MainCode = "";
            LastRunningHr = 0;
        }
        else
        {
            lblEditRHMsg.Text = "unable to update running hrs.";
        }

    }

    protected void btnCancelEditRH_Click(object sender, EventArgs e)
    {
        lblEditRHMsg.Text = "";
        txtEditRunningHrs.Text = "";
        MainCode = "";
        LastRunningHr = 0;
        divEditRH.Visible = false;
    }

    protected void btnConfirmYes_Click(object sender, EventArgs e)
    {
        if (Mode == "H")
        {
            ProjectCommon.ShowMessage("REMARK : C/E MUST CORRECT THE RUNNING HR HISTORY OF THIS COMPONENT FROM JOB VERIFICATION.");
            divConfirm.Visible = false;
        }

        if (Mode == "D")
        {
            ProjectCommon.ShowMessage("REMARK : C/E MUST CORRECT THE RUNNING HR DATE HISTORY OF THIS COMPONENT FROM JOB VERIFICATION.");
            divConfirm.Visible = false;
            
        }

        if (Mode == "M")
        {
            SaveRunningHrs();
        }
        else
        {
            Common.Set_Procedures("sp_Ship_UpdateRunningHour_History");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@Mode", Mode),
                new MyParameter("@VesselCode", Session["CurrentShip"].ToString()),
                new MyParameter("@ComponentCode", txtMainCode.Text.Trim()),
                new MyParameter("@NewHrs", txtRhrs.Text.Trim()),
                new MyParameter("@NewDate", txtDate.Text.Trim())
                );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                Mode = "";
                divConfirm.Visible = false;
                //ProjectCommon.ShowMessage("Running Hour Details Added Successfully.");
            }
            else
            {
                //ProjectCommon.ShowMessage("Unable to Add Running Hour Details.Error." + Common.getLastError());
            }
        }
    }
    protected void btnConfirmNo_Click(object sender, EventArgs e)
    {
        Mode = "";
        lblConfirmMsg.Text = "";
        divConfirm.Visible = false;
    }
}
