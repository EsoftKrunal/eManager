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

public partial class Office_RunningHour : System.Web.UI.Page
{
    AuthenticationManager Auth;
    bool setScroll = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //***********Code to check page acessing Permission
        Auth = new AuthenticationManager(1040, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (!Auth.IsView)
        {
            Response.Redirect("~/AuthorityError.aspx");
        }
        else
        {
            btnPring.Visible = Auth.IsPrint;
        }
        //*******************
        if (!Page.IsPostBack)
        {
            BindVessels();
            int i = DateTime.Today.Year;
            for (; i >= 2010; i--)
            {
                ddlyears.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            //btnRunHSave.Visible = (Session["UserType"].ToString() == "S");

            ShowRunningHourDetails();

        }
    }

    #region ---------------- USER DEFINED FUNCTIONS ---------------------------
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) and vesselstatusid=1 ORDER BY VesselName ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();
        }
        ddlVessels.Items.Insert(0, "< SELECT >");
    }
    #region ---------------- Assign Running Hour ---------------------
    public void ShowRunningHourDetails()
    {
		//string strRunningHourSQL = "SELECT row_number() over(order by B.ComponentCode) as SNO,(SELECT ShipName FROM Settings WHERE ShipCode = '" + ddlVessels.SelectedValue.Trim() + "') AS VesselName, " +
        //                        "*, " +
        //                        "StartDate=(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(StartDate,''),106),' ','-') AS StartDate FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "'  AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC ), " +
        //                        "StartupHour=(SELECT TOP 1 StartupHour FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC ),  " +
        //                        "AvgRunningHrPerDay=(SELECT TOP 1 AvgRunningHrPerDay FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC ),  " +
        //                        "(select UserName  from ShipUserMaster where UserId IN (SELECT TOP 1 UpdatedBy FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC )) AS  UpdatedBy,  " +
        //                        "(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(UpdatedOn,''),106),' ','-')  FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC )  AS UpdatedOn,  " +
        //                        "(SELECT TOP 1 UpdatedOn FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC )  AS UpdatedOn1 ,B.CriticalType   " +
        //                        "FROM  " +
        //                        "( " +
        //                        "SELECT DISTINCT CM.ComponentId ,CM.ComponentCode,CM.ComponentName,CM.CriticalType " +
        //                        "FROM  " +
        //                        "COMPONENTMASTER CM " +
        //                        "INNER JOIN  " +
        //                        "( " +
        //                        "    SELECT COMPONENTCODE  " +
        //                        "    FROM ComponentMaster CM1  " +
        //                        "    WHERE CM1.ComponentId IN  " +
        //                        "    ( " +
        //                        "        SELECT DISTINCT ComponentId FROM VSL_VesselComponentJobMaster WHERE IntervalId = 1 AND Status='A' AND VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
        //                        "    ) " +
        //                        ") A " +
        //                        "ON LEFT(A.COMPONENTCODE,LEN(CM.COMPONENTCODE))=CM.COMPONENTCODE " +
        //                        ") B WHERE ( (LEN(B.COMPONENTCODE)=3 AND B.COMPONENTCODE<>'651') OR ( LEN(B.COMPONENTCODE)=6 AND LEFT(B.COMPONENTCODE,3)='651') ) order by B.ComponentCode ";



            string strRunningHourSQL = 
            "SELECT *, " +
            "FEB-JAN AS  JAN_CONS," +
            "MAR-FEB AS  FEB_CONS," +
            "APR-MAR AS  MAR_CONS," +
            "MAY-APR AS  APR_CONS," +
            "JUN-MAY AS  MAY_CONS," +
            "JUL-JUN AS  JUN_CONS," +
            "AUG-JUL AS  JUL_CONS," +
            "SEP-AUG AS  AUG_CONS," +
            "OCT-SEP AS  SEP_CONS," +
            "NOV-OCT AS  OCT_CONS," +
            "DEC-NOV AS  NOV_CONS," +
            "JAN1-DEC AS DEC_CONS " +
            "FROM " +
            "( " +
            "SELECT row_number() over(order by B.ComponentCode) as SNO,(SELECT ShipName FROM Settings WHERE ShipCode = '" + ddlVessels.SelectedValue.Trim() + "') AS VesselName,*,  " +
            "StartDate=(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(StartDate,''),106),' ','-') AS StartDate FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "'  AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC ),  " +
            "StartupHour=(SELECT TOP 1 StartupHour FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC ),   " +
            "AvgRunningHrPerDay=(SELECT TOP 1 AvgRunningHrPerDay FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC ),   " +
            "(select UserName  from ShipUserMaster where UserId IN (SELECT TOP 1 UpdatedBy FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC )) AS  UpdatedBy,   " +
            "(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(UpdatedOn,''),106),' ','-')  FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC )  AS UpdatedOn,   " +
            "(SELECT TOP 1 UpdatedOn FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND VRH.ComponentId = B.ComponentId ORDER BY UpdatedOn DESC )  AS UpdatedOn1   " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,NULL) AS LASTHOURS " +
            ",(SELECT REPLACE(CONVERT(Varchar(11),dbo.getRHAsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,NULL),106),' ','-')) AS LASTHOURDATE " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-JAN-" + ddlyears.SelectedValue + "') AS JAN " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-FEB-" + ddlyears.SelectedValue + "') AS FEB " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-MAR-" + ddlyears.SelectedValue + "') AS MAR " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-APR-" + ddlyears.SelectedValue + "') AS APR " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-MAY-" + ddlyears.SelectedValue + "') AS MAY " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-JUN-" + ddlyears.SelectedValue + "') AS JUN " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-JUL-" + ddlyears.SelectedValue + "') AS JUL " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-AUG-" + ddlyears.SelectedValue + "') AS AUG " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-SEP-" + ddlyears.SelectedValue + "') AS SEP " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-OCT-" + ddlyears.SelectedValue + "') AS OCT " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-NOV-" + ddlyears.SelectedValue + "') AS NOV " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-DEC-" + ddlyears.SelectedValue + "') AS [DEC] " +
            ",dbo.getRunningHrsOnDate('" + ddlVessels.SelectedValue.Trim() + "',B.ComponentCode,'01-JAN-" + Convert.ToString(Common.CastAsInt32(ddlyears.SelectedValue)+1) + "') AS JAN1 " +
            "FROM   " +
            "(  " +
            "SELECT DISTINCT CM.ComponentId ,CM.ComponentCode,CM.ComponentName,CM.CriticalType  " +
            "FROM   " +
            "COMPONENTMASTER CM  " +
            "INNER JOIN " +
            "(  " +
            "SELECT COMPONENTCODE " +
            "FROM ComponentMaster CM1 " +
            "WHERE CM1.ComponentId IN " + 
            "(  " +
            "SELECT DISTINCT j.ComponentId FROM " + 
	    "VSL_VesselComponentJobMaster j INNER JOIN VSL_ComponentMasterForVessel c on j.Componentid=c.componentid and j.vesselcode=c.vesselcode " + 
	    "WHERE IntervalId = 1 AND J.Status='A' AND j.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "'  " +
            ")  " +
            ") A  " +
            "ON LEFT(A.COMPONENTCODE,LEN(CM.COMPONENTCODE))=CM.COMPONENTCODE  " +
            " UNION " +
" select a.ComponentId,a.ComponentCode,a.ComponentName,a.CriticalType from ComponentMaster a with(nolock) inner join VSL_ComponentMasterForVessel b with(nolock) on a.ComponentId = b.ComponentId where Isnull(a.RHComponent,0) = 1 and b.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
            ") B WHERE ( (LEN(B.COMPONENTCODE)=3 AND B.COMPONENTCODE NOT IN ('651','411','334')) OR ( LEN(B.COMPONENTCODE)=6 AND ( LEFT(B.COMPONENTCODE,3) IN ('651','411','334'))) OR ( B.COMPONENTID IN (SELECT ComponentId from ComponentMaster where RHComponent = 1) ) )  " +
            ") C order by ComponentCode  ";

		Session.Add("sSqlForPrint", ddlVessels.SelectedValue.Trim());
        rptRunningHour.DataSource = Common.Execute_Procedures_Select_ByQuery(strRunningHourSQL);
        //rptRunningHour.DataSource = Common.Execute_Procedures_Select_ByQuery("exec dbo.Office_GteRunningHrs '" + ddlVessels.SelectedValue.Trim() + "'," + ddlyears.SelectedValue);
        rptRunningHour.DataBind();
    }
    #endregion
    #endregion

    #region ---------------- Events -------------------------------------------

    #region ---------------- Commom ---------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvRH');", true);
        lblCount.Text = "( " + rptRunningHour.Items.Count + " ) records found.";   
    }
    #endregion

    // Code By Umakant
    protected void btnPring_Click(object sener, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            msgRunHour.ShowMessage("Please select a vessel.", true);
            ddlVessels.Focus();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('OfficeRunningHourPrint.aspx?ReportType=RuningHour');", true);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('Reports/PrintCrystal.aspx?ReportType=RuningHour');", true);
        }
    }
    #endregion
    protected void Show_Click(object sender, EventArgs e)
    {
    
        
        if (ddlVessels.SelectedIndex != 0)
        {
            ShowRunningHourDetails();
        }
        else
        {
            rptRunningHour.DataSource = null;
            rptRunningHour.DataBind();
        }
    }

    public int getConsumedHours(int Month, string CompCode)
    {
        string VSlCode = ddlVessels.SelectedValue.Trim();
        int retSt = 0, retEnd = 0; ;
        DateTime MonthStartDate = new DateTime(Common.CastAsInt32(ddlyears.SelectedValue), Month, 1);
        DateTime MonthEndDate = MonthStartDate.AddMonths(1);

        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDate('" + VSlCode + "','" + CompCode + "','" + MonthStartDate.ToString("dd-MMM-yyyy") + "')");
        DataTable dtEnd = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDate('" + VSlCode + "','" + CompCode + "','" + MonthEndDate.ToString("dd-MMM-yyyy") + "')");

        //DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDate_SingleCompCode('" + VSlCode + "','" + CompCode + "','" + MonthStartDate.ToString("dd-MMM-yyyy") + "')");
        //DataTable dtEnd = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDate_SingleCompCode('" + VSlCode + "','" + CompCode + "','" + MonthEndDate.ToString("dd-MMM-yyyy") + "')");

        if (dtStart.Rows.Count > 0)
            retSt = Common.CastAsInt32(dtStart.Rows[0][0]);
        if (dtEnd.Rows.Count > 0)
            retEnd = Common.CastAsInt32(dtEnd.Rows[0][0]);
        return retEnd - retSt;
    }
    public int getLastHours(string CompCode)
    {
        int Hrs = 0;
        string VSlCode = ddlVessels.SelectedValue.Trim();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDate('" + VSlCode + "','" + CompCode + "',NULL)");
        if (dtStart.Rows.Count > 0)
            Hrs = Common.CastAsInt32(dtStart.Rows[0][0]);
        return Hrs;
    }
    public string getLastHoursDate(string CompCode)
    {
        string LastDt = "";
        string VSlCode = ddlVessels.SelectedValue.Trim();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT REPLACE(CONVERT(Varchar(11),dbo.getRHAsOnDate('" + VSlCode + "','" + CompCode + "',NULL),106),' ','-')");
        if (dtStart.Rows.Count > 0)
            LastDt = dtStart.Rows[0][0].ToString();
        return LastDt;
    }
    
}
