using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using Ionic.Zip;

public partial class CrewOperation_MonitorTraining_Shore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //----------------------------- sorebae 0
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        if (!Page.IsPostBack)
        {
            txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            BindYear();            
            LoadOffice();
            ddlMonthTO.SelectedValue = DateTime.Today.Month.ToString();
            ddlMonthFrom.SelectedValue = "1";

            ShowVesselList();
        }
    }
    // Event ----------------------------------------------------------------------------------------------------
    protected void lnkCrewNumber_OnClick(object sender, EventArgs e)
    {
        DateTime FromDate = Convert.ToDateTime("1-" + ddlMonthFrom.SelectedItem.Text + "-" + ddlYearFrom.SelectedValue + "");
        DateTime TODate = FromDate.AddMonths(1).AddDays(-1);
        LinkButton btn = (LinkButton)sender;
        string CrewNumber = btn.CommandArgument.ToString();
        Response.Redirect("RestHourEntry.aspx?c=" + CrewNumber + "&t=" + btn.CssClass + "&v=" + btn.Attributes["vessel"] + "&m=" + DateTime.Now.Month.ToString() + "&y=" + DateTime.Now.Year.ToString());
    }
    protected void Show_Click(object sender, EventArgs e)
    {
        ShowVesselList();
    }
    protected void btnShow11_OnClick(object sender, EventArgs e)
    {
        ShowCrewPortalUser();
    }
    protected void lnlVessel_OnClick(object sender, EventArgs e)
    {
        //LinkButton btn = (LinkButton)sender;
        //VesselID = Common.CastAsInt32(btn.CommandArgument);
        ////lblSelVessel.Text = btn.Text +" as on "+GetMonthName( ddlMonthFrom.SelectedValue)+"-"+ddlYearFrom.SelectedValue+"  To  "+GetMonthName(ddlMonthTO.SelectedValue)+"-"+ddlYearTO.SelectedValue+"";

        //lblSelVessel.Text = btn.Text + " as on ";
        //ShowCrewPortalUser();
        //tblSearchPanel.Visible = false;
    }
    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        //Response.Redirect("CrewList.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //ddlCrewStatus.SelectedIndex = 0;
        
        ddlOffice.SelectedIndex = 0;

        ddlMonthTO.SelectedValue = DateTime.Today.Month.ToString();
        ddlMonthFrom.SelectedValue = DateTime.Today.Month.ToString();
        //ShowVesselList();

    }
    // Functon --------------------------------------------------------------------------------------------------------
    public void ShowCrewPortalUser()
    {
        divCrewPortalUser.Visible = true;
        divVesselList.Visible = false;
        string TodayDate = txtDate.Text;//DateTime.Today.ToString("dd-MMM-yyyy");
        string MonthStartDate = Convert.ToDateTime(TodayDate).ToString("01-MMM-yyyy");

        DateTime FromDate = Convert.ToDateTime("1-" + ddlMonthFrom.SelectedItem.Text + "-" + ddlYearFrom.SelectedValue + "");
        DateTime TODate = Convert.ToDateTime("1-" + ddlMonthTO.SelectedItem.Text + "-" + ddlYearTO.SelectedValue + "").AddMonths(1).AddDays(-1);

        DateTime FromDateTM = Convert.ToDateTime(DateTime.Today.ToString("01-MMM-yyyy"));
        DateTime TODateTM = FromDateTM.AddMonths(1).AddDays(-1);

        int Rank = Common.CastAsInt32(Session["RankId"]);

        string sql = "SELECT " +
                         " VesselId,CrewId,VCL.ContractId ,DateDiff(d,SignOnDate,coalesce(SingOffDate,getdate())) DaysOnBoard" +
                         " ,(select FirstName+' '+MiddleName+' ' +LastName as CrewName from CrewPersonalDetails CP where CP.CrewId=VCL.CrewId) CrewName " +
                         " ,(select CrewNumber as CrewNumber from CrewPersonalDetails CP where CP.CrewId=VCL.CrewId) CrewNumber " +
                         " ,(select ContractReferenceNumber from dbo.CrewContractHeader CCH where CCH.ContractId=VCL.ContractId)ContractNumber " +
                         " ,(select RankCode from rank R where R.RankID=VCL.NewRankId)RankName " +
                         " ,replace(convert(varchar,SignOnDate,106),' ','-')SignOnDate " +
                         " ,replace(convert(varchar,SingOffDate,106),' ','-')SignOffDate,1 As WatchKeeper " +

                         "  ,(case when " +
                         "  (SELECT COUNT(DISTINCT CALCDATE) FROM CP_NONCONFORMANCE NC WHERE NC.VESSELID=VCL.VESSELID AND NC.CREWID=VCL.CREWID AND NCDATE>='" + MonthStartDate + "' AND NCDATE<='" + TodayDate + "' AND NCTYPE=7 ) + " +
                         "  (SELECT COUNT(*) FROM CP_NONCONFORMANCE NC WHERE NC.VESSELID=VCL.VESSELID AND NC.CREWID=VCL.CREWID AND NCDATE>='" + MonthStartDate + "' AND NCDATE<='" + TodayDate + "' AND NCTYPE<>7 ) " +
                         "   >0 then 'Red' else '' end )CSSTM " +

                         " , (case when (" +
                         "  (SELECT COUNT(DISTINCT CALCDATE) FROM CP_NONCONFORMANCE NC WHERE NC.VESSELID=VCL.VESSELID AND NC.CREWID=VCL.CREWID AND NCDATE>=VCL.SignOnDate AND NCDATE<='" + TodayDate + "' AND NCTYPE=7 ) + " +
                         "  (SELECT COUNT(*) FROM CP_NONCONFORMANCE NC WHERE NC.VESSELID=VCL.VESSELID AND NC.CREWID=VCL.CREWID AND NCDATE>=VCL.SignOnDate  AND NCDATE<='" + TodayDate + "' AND NCTYPE<>7 ) " +
                         "  ) >0 then 'Red' else '' end) as CSS " +


                         " ,(" +
                         "  (SELECT COUNT(DISTINCT CALCDATE) FROM CP_NONCONFORMANCE NC WHERE NC.VESSELID=VCL.VESSELID AND NC.CREWID=VCL.CREWID AND NCDATE>=VCL.SignOnDate AND NCDATE<='" + TodayDate + "' AND NCTYPE=7) + " +
                         "  (SELECT COUNT(*) FROM CP_NONCONFORMANCE NC WHERE NC.VESSELID=VCL.VESSELID AND NC.CREWID=VCL.CREWID AND NCDATE>=VCL.SignOnDate  AND NCDATE<='" + TodayDate + "' AND NCTYPE<>7 ) " +
                         "  )  AS NCCOUNT " +

                         " ,(" +
                         "  (SELECT COUNT(DISTINCT CALCDATE) FROM CP_NONCONFORMANCE NC WHERE NC.VESSELID=VCL.VESSELID AND NC.CREWID=VCL.CREWID AND NCDATE>='" + MonthStartDate + "' AND NCDATE<='" + TodayDate + "' AND NCTYPE=7 ) + " +
                         "  (SELECT COUNT(*) FROM CP_NONCONFORMANCE NC WHERE NC.VESSELID=VCL.VESSELID AND NC.CREWID=VCL.CREWID AND NCDATE>='" + MonthStartDate + "' AND NCDATE<='" + TodayDate + "' AND NCTYPE<>7 ) " +
                         "  )  AS NCCOUNTThisMonth " +


                         " , (SELECT COUNT(DISTINCT TRANSDATE) FROM dbo.CP_CrewDailyWorkRestHours WHERE ENTEREDBY=1 AND VESSELID=VCL.VesselId AND CREWID=VCL.CrewId AND TRANSDATE>=VCL.SignOnDate AND TRANSDATE<='" + TodayDate + "' ) as ME " +
                         " , (SELECT  COUNT(DISTINCT TRANSDATE) FROM dbo.CP_CrewDailyWorkRestHours WHERE ENTEREDBY=1 AND VESSELID=VCL.VesselId AND CREWID=VCL.CrewId AND TRANSDATE<='" + TodayDate + "' AND TRANSDATE>='" + MonthStartDate + "' ) as METM " +
                         " FROM get_crew_history VCL  WHERE VCL.SignOnDate<='" + TodayDate + "' AND ( VCL.SINGOFFDATE>='" + TodayDate + "' or VCL.SINGOFFDATE is null) ";

        string WhereClause = "";
        //-----------------------------
        //if (VesselID > 0)
        //{
        //    WhereClause += " AND VCL.VESSELID=" + VesselID;
        //}

        string OrderBy = " ORDER BY (select RankLevel from rank R where R.RankID=VCL.NewRankId)";

        sql = sql + WhereClause + OrderBy;
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptCrewPortalUser.DataSource = Dt;
        rptCrewPortalUser.DataBind();
    }
    public void ShowVesselList()
    {
        divCrewPortalUser.Visible = false;
        divVesselList.Visible = true;

        //if (ddlCrewStatus.SelectedIndex == 0)
        //{
        //    lblMsg.Text = "Please select training location.";
        //    ddlCrewStatus.Focus();
        //    return;
        //}


        string FleetOfficeClause = "";

        //DateTime dtFrom = new DateTime(Common.CastAsInt32(ddlYearFrom.SelectedValue.Trim()),Common.CastAsInt32(ddlMonthFrom.SelectedValue.Trim()), 1);       

        string FromDay = "01-" + ddlMonthFrom.SelectedItem.Text.Trim() + "-" + ddlYearFrom.SelectedValue.Trim();
        string ToDay = "01-" + ddlMonthTO.SelectedItem.Text.Trim() + "-" + ddlYearTO.SelectedValue.Trim();

        DateTime dt = Convert.ToDateTime(ToDay).AddMonths(1).AddDays(-1);

        ToDay = dt.ToString("dd-MMM-yyyy");
        if (true)
        {
            if (ddlOffice.SelectedIndex > 0)
            {
                FleetOfficeClause = " WHERE OFFICEID=" + ddlOffice.SelectedValue + " ";
            }

            //string sql = "SELECT  OFFICENAME," +
            //             "(SELECT MAX(RecordCreatedOn) FROM DBO.CREWTRAININGREQUIREMENT WHERE OFFICEID=MST.OFFICEID) AS LastUpdateRecdOn," +
            //             "(SELECT COUNT(TRAININGID) FROM (SELECT DISTINCT CREWID,TRAININGID,NextDueComputed FROM DBO.CREWTRAININGREQUIREMENT WHERE OFFICEID=MST.OFFICEID) A WHERE (SELECT MAX(NextDueComputed) FROM DBO.CREWTRAININGREQUIREMENT WHERE CREWID=A.CREWID AND TRAININGID=A.TRAININGID) <= GETDATE()) AS odcOUNT," +

            //             " (SELECT COUNT(DISTINCT CREWID) FROM DBO.CREWTRAININGREQUIREMENT WHERE ( TODATE BETWEEN  '" + FromDay + "' AND '" + ToDay + "' ) ) AS ByCrew, " +
            //             " (SELECT COUNT(DISTINCT CrewRankId) FROM DBO.CREWTRAININGREQUIREMENT WHERE ( TODATE BETWEEN  '" + FromDay + "' AND '" + ToDay + "' )) AS ByRank , " +
            //             " (SELECT COUNT(DISTINCT TT.TRAININGTYPENAME) FROM DBO.CREWTRAININGREQUIREMENT DET INNER JOIN DBO.TrainingType TT ON TT.TRAININGTYPEID=DET.TRAININGID WHERE OFFICEID=MST.OFFICEID AND ( TODATE BETWEEN '" + FromDay + "' AND '" + ToDay + "' ) ) AS ByTrainingType " +
            //             "FROM DBO.OFFICE MST " + FleetOfficeClause + " ORDER BY OFFICENAME ";

            //AND N_DUEDATE BETWEEN '" + FromDay + "' AND '" + ToDay + "'

            string sql = "SELECT RecruitingOfficeId,RecruitingOfficeName, " +
                        "(" +
                        "    SELECT COUNT(DISTINCT CREWID) FROM DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=MST.RecruitingOfficeiD " +
                        ") AS ByCrew_Due, " +
                        "(" +
                        "   SELECT COUNT(DISTINCT CurrentRankId) FROM DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=MST.RecruitingOfficeiD " +
                        ") AS ByRank_Due, " +
                        "(" +
                        "    SELECT COUNT(DISTINCT TrainingId) FROM DBO.VW_DUE_TRAININGS_ONSHORE WHERE RecruitmentOfficeId=MST.RecruitingOfficeiD " +
                        ") AS ByTrainingType_Due, " +
                        "(" +
                        "    SELECT COUNT(DISTINCT CREWID) FROM DBO.CREWTRAININGREQUIREMENT CT WHERE TRAININGPLANNINGID<>3 AND " +
                        "    CT.CREWID IN(SELECT CREWID FROM CrewPersonalDetails WHERE RecruitmentOfficeId = MST.RecruitingOfficeiD) AND(TODATE BETWEEN  '" + FromDay + "' AND '" + ToDay + "')" +
                        ") AS ByCrew, " +
                        "(" +
                        "   SELECT COUNT(DISTINCT CrewRankId) FROM DBO.CREWTRAININGREQUIREMENT CT WHERE TRAININGPLANNINGID<>3 AND " +
                        "   CT.CREWID IN(SELECT CREWID FROM CrewPersonalDetails WHERE RecruitmentOfficeId = MST.RecruitingOfficeiD) AND(TODATE BETWEEN  '" + FromDay + "' AND '" + ToDay + "')" +
                        ") AS ByRank, " +
                        "(" +
                        "    SELECT COUNT(DISTINCT T.TRAININGID) FROM DBO.CREWTRAININGREQUIREMENT DET " +
                        "    inner join DBO.training T ON T.TRAININGID = DET.TRAININGID " +
                        "    INNER JOIN DBO.TrainingType TT ON T.TYPEOFTRAINING = TT.TRAININGTYPEID WHERE TRAININGPLANNINGID<>3 AND " +
                        "    DET.CREWID IN (SELECT CREWID FROM CrewPersonalDetails WHERE RecruitmentOfficeId = MST.RecruitingOfficeiD) AND (TODATE BETWEEN '" + FromDay + "' AND '" + ToDay + "') " +
                        ") AS ByTrainingType " +

                    " FROM DBO.RecruitingOffice MST  ORDER BY RecruitingOfficeName";

            DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            //dv_VesselWiseList.Visible = false;
            div_OfficewiseList.Visible = true;
            lblVessel_Office.Text = "Office";

            rptOfficewiseList.DataSource = Dt;
            rptOfficewiseList.DataBind();

        }

        ////String FleetWhereClause = "";
        ////if (ddlFleet.SelectedIndex != 0)
        ////    FleetWhereClause = " and V.FleetID=" + ddlFleet.SelectedValue;

        ////if (ddlVesselType.SelectedIndex != 0)
        ////    FleetWhereClause = FleetWhereClause + " and V.VesselTypeID=" + ddlVesselType.SelectedValue + " ";

        ////FleetWhereClause = FleetWhereClause + " Order by VesselName";

        ////string Sql = "select Row_Number() over(order by VesselName )Row,VesselID,VesselName, " +
        ////             "(case when exists(select top 1 * from cp_crewdailyworkresthours c where c.vesselid=V.vesselid) then 'green' else 'black' end ) as VColor, " +
        ////             "(SELECT TOP 1 RECDON FROM CP_VesselRestHourPackets D WHERE D.VESSELCODE=V.VESSELCODE ORDER BY RECDON DESC) AS LASTUPDATE" +

        ////             " from Vessel V where V.VesselStatusid=1 " + FleetWhereClause;

        //string SQL = "SELECT Row_Number() over(order by VesselName )Row, VESSELCODE, VESSELNAME," + Ship_Shore + " FROM DBO.VESSEL WHERE VESSELSTATUSID=1  " + FleetFilter + " ORDER BY VESSELNAME ";



    }
    public string getCountVerified(string VesselId)
    {
        //DateTime FromDate = Convert.ToDateTime("1-" + ddlMonthFrom.SelectedItem.Text + "-" + ddlYearFrom.SelectedValue + "");
        //DateTime TODate = Convert.ToDateTime("1-" + ddlMonthTO.SelectedItem.Text + "-" + ddlYearTO.SelectedValue + "").AddMonths(1).AddDays(-1);
        //string strFrm, strTo;
        //strFrm = "'" + FromDate.ToString("dd-MMM-yyyy") + "'";
        //strTo = "'" + TODate.ToString("dd-MMM-yyyy") + "'";

        //string Reasion = "";
        //if (ddlReason.SelectedIndex != 0)
        //    Reasion = " and exists(select * from dbo.CP_NonConformanceReason NCR where NCR.VesselID=c.VesselID and NCR.NCDate=c.NCDate and NCR.Reason=" + ddlReason.SelectedValue + ")";

        //string Sql = " select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c inner join CP_NONCONFORMANCE_verification v1 on c.vesselid=v1.vesselid and c.crewid=v1.crewid and c.ncdate=v1.ncdate and c.nctype=v1.nctype where c.nctype<>7 and c.NCDate>=" + strFrm + "  and c.NCDate<=" + strTo + " and c.VesselID=" + VesselId + Reasion + " " +
        //             " union " +
        //             " select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c inner join CP_NONCONFORMANCE_verification v1 on c.vesselid=v1.vesselid and c.crewid=v1.crewid and c.ncdate=v1.ncdate and c.nctype=v1.nctype where c.nctype=7 and c.NCDate>=" + strFrm + " and c.NCDate<=" + strTo + " and c.VesselID=" + VesselId + Reasion + " ";
        //DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
        //if (Dt.Rows.Count > 0)
        //{
        //    return Dt.Rows.Count.ToString();
        //}
        //else
        //{
        return "";
        //}

    }
    public string getCountUnVerified(string VesselId)
    {
        int cnt = Common.CastAsInt32(getCountTotal(VesselId)) - Common.CastAsInt32(getCountVerified(VesselId));
        if (cnt > 0)
            return cnt.ToString();
        else
            return "";
    }
    public string getCountTotal(string VesselId)
    {
        //DateTime FromDate = Convert.ToDateTime("1-" + ddlMonthFrom.SelectedItem.Text + "-" + ddlYearFrom.SelectedValue + "");
        //DateTime TODate = Convert.ToDateTime("1-" + ddlMonthTO.SelectedItem.Text + "-" + ddlYearTO.SelectedValue + "").AddMonths(1).AddDays(-1);
        //string strFrm, strTo;
        //strFrm = "'" + FromDate.ToString("dd-MMM-yyyy") + "'";
        //strTo = "'" + TODate.ToString("dd-MMM-yyyy") + "'";

        //string Reasion = "";
        //if (ddlReason.SelectedIndex != 0)
        //    Reasion = " and exists(select * from dbo.CP_NonConformanceReason NCR where NCR.VesselID=c.VesselID and NCR.NCDate=c.NCDate and NCR.Reason=" + ddlReason.SelectedValue + ")";

        //string Sql = " select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c where c.nctype<>7 and c.NCDate>=" + strFrm + " 	and c.NCDate<=" + strTo + " and c.VesselID=" + VesselId + Reasion + "" +
        //             " union " +
        //             " select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c where c.nctype=7 and c.NCDate>=" + strFrm + " 	and c.NCDate<=" + strTo + " and c.VesselID=" + VesselId + Reasion + "";

        //DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
        //if (Dt.Rows.Count > 0)
        //{
        //    return Dt.Rows.Count.ToString();
        //}
        //else
        //{
        return "";
        //}

    }
    public string getCountNC(string VesselId, int Type)
    {
        //DateTime FromDate = Convert.ToDateTime("1-" + ddlMonthFrom.SelectedItem.Text + "-" + ddlYearFrom.SelectedValue + "");
        //DateTime TODate = Convert.ToDateTime("1-" + ddlMonthTO.SelectedItem.Text + "-" + ddlYearTO.SelectedValue + "").AddMonths(1).AddDays(-1);
        //string strFrm, strTo;
        //strFrm = "'" + FromDate.ToString("dd-MMM-yyyy") + "'";
        //strTo = "'" + TODate.ToString("dd-MMM-yyyy") + "'";

        //string Reasion = "";
        //if (ddlReason.SelectedIndex != 0)
        //    Reasion = " and exists(select * from dbo.CP_NonConformanceReason NCR where NCR.VesselID=c.VesselID and NCR.NCDate=c.NCDate and NCR.Reason=" + ddlReason.SelectedValue + ")";

        //string JoinSQL = "";
        //string WhereSQL = "";
        //if (chkUVNC.Checked)
        //{
        //    JoinSQL = " left join CP_NonConformance_Verification v on c.vesselid=v.vesselid and c.crewid=v.crewid and c.ncdate=v.ncdate and c.nctype=v.nctype";
        //    WhereSQL = " and V.VERIFIEDBY IS NULL";
        //}

        //string sql = "";
        //if (Type == 1)
        //    sql = "select distinct c.vesselid,c.crewid,c.ncdate,c.nctype from CP_NONCONFORMANCE c " + JoinSQL + " where C.NCType=2 and C.NCDate>=" + strFrm + " and C.NCDate<=" + strTo + " " + WhereSQL + " AND C.VesselID=" + VesselId + Reasion;
        //else if (Type == 2)
        //    sql = "select distinct c.vesselid,c.crewid,c.ncdate,c.nctype  from CP_NONCONFORMANCE c " + JoinSQL + " where C.NCType=6 and C.NCDate>=" + strFrm + "  and C.NCDate<=" + strTo + " " + WhereSQL + " AND C.VesselID=" + VesselId + Reasion;
        //else if (Type == 3)
        //    sql = "select distinct c.vesselid,c.crewid,c.ncdate,c.nctype  from CP_NONCONFORMANCE c " + JoinSQL + " where C.NCType=7 and C.NCDate>=" + strFrm + "  and C.NCDate<=" + strTo + " " + WhereSQL + " AND C.VesselID=" + VesselId + Reasion;
        //else if (Type == 4)
        //    sql = "select distinct c.vesselid,c.crewid,c.ncdate,c.nctype  from CP_NONCONFORMANCE c " + JoinSQL + " where C.NCType=24 and C.NCDate>=" + strFrm + "  and C.NCDate<=" + strTo + " " + WhereSQL + " AND C.VesselID=" + VesselId + Reasion;

        //DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        //if (Dt.Rows.Count > 0)
        //{
        //    return Dt.Rows.Count.ToString();
        //}
        //else
        //{
        return "";
        //}

    }
    public void BindYear()
    {
        for (int i = DateTime.Now.Year; i >= 2000; i--)
        {
            ddlYearFrom.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            ddlYearTO.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
        }
        ddlYearFrom.SelectedValue = DateTime.Now.Year.ToString();
        ddlYearTO.SelectedValue = DateTime.Now.Year.ToString();
    }
    
    public void LoadOffice()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT RecruitingOfficeId, RecruitingOfficeName FROM DBO.RecruitingOffice");
        ddlOffice.DataSource = dt;
        ddlOffice.DataTextField = "RecruitingOfficeName";
        ddlOffice.DataValueField = "RecruitingOfficeId";
        ddlOffice.DataBind();
        ddlOffice.Items.Insert(0, new ListItem("< ALL >", "0"));
    }
    public string Total(string a, string b, string c, string d)
    {

        int i = Common.CastAsInt32(Common.CastAsInt32(a) + Common.CastAsInt32(b) + Common.CastAsInt32(c) + Common.CastAsInt32(d));
        if (i == 0)
            return "";
        else
            return i.ToString();
    }
    public static string GetMonthName(String MonthId)
    {
        int mid = Convert.ToInt16(MonthId);
        switch (mid)
        {
            case 1:
                return "Jan";
            case 2:
                return "Feb";
            case 3:
                ;
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
    //------------------------------------------------------
    //------------------------------------------------------
    //------------------------------------------------------
    //------------------------------------------------------
    protected void btnMail_Click(object sender, EventArgs e)
    {
        int VesselId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        CreateTrainingHTMLFiles(VesselId);


        //if (GenerateWCPaketHTMLForm(VesselId.ToString()))
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Mail sent successfully.');", true);
        //}

    }
    public bool GenerateWCPaketHTMLForm(string VesselID)
    {
        //goto Last;

        string FilePath = "C:\\inetpub\\wwwroot\\SHIPSOFT\\Auto Alert\\RestHourTemplate" + ".htm";
        if (!(File.Exists(FilePath)))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Mail template file not exists.');", true);
            return false;
        }

        string VesselCode = "";
        StringBuilder sForm = new StringBuilder();
        string sql = "select V1.VESSELCode,V1.VesselName,cpd.crewnumber,cpd.firstname + ' ' + cpd.middlename + ' ' + cpd.lastname as CrewName,replace(Convert(Varchar, c.NCDate,106),' ','-')NCDate,TP.NCTYPENAME,v.VerifiedBy,V.VerifiedOn,V.Remarks,rm.NCReasonName,C.VESSELID,C.CREWID,C.NCTYPE,isnull(R.REASON,0) as REASON,rk.rankcode " +
                   "from " +
                   " ( select distinct vesselid,crewid,ncdate,nctype from CP_NONCONFORMANCE ) c " +
                   "inner join CP_NCTYPE TP ON TP.NCTYPEID=C.NCTYPE " +
                   "inner join vessel v1 on v1.vesselid=c.vesselid " +
                   "inner join crewpersonaldetails cpd on cpd.crewid=c.crewid " +
                   "inner join rank rk on rk.rankid=cpd.currentrankid " +
                   "left join CP_NONCONFORMANCE_verification v on c.vesselid=v.vesselid and c.crewid=v.crewid and c.ncdate=v.ncdate and c.nctype=v.nctype  " +
                   "left join CP_NonConformanceReason r on c.vesselid=r.vesselid and c.crewid=r.crewid and c.ncdate=r.ncdate  " +
                   "left join cp_NCREASON rm on r.reason=rm.NCReasonId where v.VERIFIEDBY IS NULL and c.vesselid='" + VesselID + "' order by v1.VESSELCode,c.ncdate";

        DataTable dtRes = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dtRes.Rows.Count > 0)
        {

            string sFileText = File.ReadAllText(FilePath);

            int L1 = sFileText.Substring(0, sFileText.IndexOf("<!--$$End$$-->")).Length;
            int L2 = sFileText.Substring(0, sFileText.IndexOf("<!--$$Start$$-->")).Length;
            int fLenth = L1 - L2;
            string tr = sFileText.Substring(sFileText.IndexOf("<!--$$Start$$-->"), fLenth);

            string FirstPart = sFileText.Substring(0, sFileText.IndexOf("<!--$$Start$$-->"));
            string LastPart = sFileText.Substring(sFileText.IndexOf("<!--$$End$$-->"));
            FirstPart = FirstPart.Replace("$$Count$$", dtRes.Rows.Count.ToString());


            tr = tr.Replace("<!--$$Start$$-->", "");
            int cnt = 1;
            string trTotal = "";
            foreach (DataRow dr in dtRes.Rows)
            {
                string s1 = tr;
                FirstPart = FirstPart.Replace("$$VesselName$$", dr["VesselName"].ToString() + " As on : " + DateTime.Today.ToString("dd-MMM-yyyy"));
                VesselCode = dr["VESSELCode"].ToString();

                s1 = s1.Replace("lblCrewNo", "lblCrewNo" + cnt.ToString());
                s1 = s1.Replace("hfVesselID", "hfVesselID" + cnt.ToString());
                s1 = s1.Replace("hfCrewID", "hfCrewID" + cnt.ToString());
                s1 = s1.Replace("hfNCTypeID", "hfNCTypeID" + cnt.ToString());
                s1 = s1.Replace("lblName", "lblName" + cnt.ToString());
                s1 = s1.Replace("lblRank", "lblRank" + cnt.ToString());
                s1 = s1.Replace("lblDate", "lblDate" + cnt.ToString());
                s1 = s1.Replace("lblNCType", "lblNCType" + cnt.ToString());
                s1 = s1.Replace("ddlRestHour", "ddlRestHour" + cnt.ToString());
                s1 = s1.Replace("txtRemarks", "txtRemarks" + cnt.ToString());

                s1 = s1.Replace(" $$CrewNumber$$", dr["crewnumber"].ToString());
                s1 = s1.Replace("$$VesselID$$", dr["VESSELID"].ToString());
                s1 = s1.Replace("$$CrewID$$", dr["CrewID"].ToString());
                s1 = s1.Replace("$$NCTypeID$$", dr["NCTYPE"].ToString());
                s1 = s1.Replace("$$Name$$", dr["CrewName"].ToString());
                s1 = s1.Replace("$$Rank$$", dr["rankcode"].ToString());
                s1 = s1.Replace("$$Date$$", dr["NCDate"].ToString());
                s1 = s1.Replace("$$NCType$$", dr["NCTYPENAME"].ToString());

                s1 = s1.Replace("<option value=\"" + dr["REASON"].ToString() + "\">", "<option value=\"" + dr["REASON"].ToString() + "\" selected>");

                trTotal = trTotal + s1;
                cnt++;

            }


            //---------------------------
            //Last :

            string SenderAddress = "emanager@energiossolutions.com";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT isnull(EMAIL,''),isnull(GROUPEMAIL,'') FROM DBO.VESSEL WHERE VESSELID=" + VesselID.ToString());

            string ToEmail = dt.Rows[0][0].ToString();
            string GroupEmail = dt.Rows[0][1].ToString();

            string[] ToAddress = { ToEmail };
            string[] CCAddress = { "emanager@energiossolutions.com", GroupEmail };
            //---------------------------

            StringBuilder msgFormat = new StringBuilder();
            msgFormat.Append("<html><head></head><body>" + "\n");
            msgFormat.Append("<table width=100% align=center> " + "\n");
            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">Dear Captain, </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\"> We have received a Non Conformity for the Breach of  Rest Hours from your vessel as attached. </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">  </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">  </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> We would like to reiterate that failing to comply with rest hour requirements may lead to fatigue which in turn could lead to accidents on board.In order to close out this Non Conformity please send us the following information as soon as possible; </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> 1)      The detailed reasons/circumstances which led to this non-conformity. </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> 2)      Could this have been avoided? If Yes, then how? </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> 3)      The work plan for the next 7 days to ensure compliance. </td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">&nbsp;" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">Thank you" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"> <br/> HSQE Dept. </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\"></td></tr>" + "\n");

            msgFormat.Append("</table>" + "\n");
            msgFormat.Append("</body></html>" + "\n");

            string Error = "";

            string DestPath = Server.MapPath("RestHour.htm");

            using (StreamWriter sw = new StreamWriter(DestPath, false))
            {
                sw.Write(FirstPart + trTotal + LastPart);
            }
            SendEmail.SendeMail(0, SenderAddress, SenderAddress, ToAddress, CCAddress, CCAddress, "Rest Hour Update Packet - [ " + VesselCode + "-" + DateTime.Today.ToString("dd-MMM-yyyy") + " ] ", msgFormat.ToString(), out Error, DestPath);
            //-----------------------------
            return true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('NC not exists.');", true);
            return false;

        }
    }
    //protected void ddlCrewStatus_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCrewStatus.SelectedValue.Trim() == "" || ddlCrewStatus.SelectedValue.Trim() == "3")
    //    {
    //        lblFleet_Office.Text = "Fleet : ";
    //        ddlFleet.Visible = true;
    //        ddlOffice.Visible = false;
    //        ddlFleet.SelectedIndex = 0;
    //    }
    //    else
    //    {
    //        lblFleet_Office.Text = "Office : ";
    //        ddlFleet.Visible = false;
    //        ddlOffice.Visible = true;
    //        ddlOffice.SelectedIndex = 0;
    //    }

    //}
    #region -------------------------- Crew Training HTML Files -----------------------
    private void CreateTrainingHTMLFiles(int _VesselId)
    {
        string TrainingTemplatePath = Server.MapPath("~\\CrewOperation\\CrewTraining.htm");
        string TrainingFolderPath = Server.MapPath("~\\CrewOperation\\TrainingHTML");
        Dictionary<string, string> dctVessels = new Dictionary<string, string>();

        if (Directory.Exists(TrainingFolderPath))
        {
            foreach (string fls in Directory.GetFiles(TrainingFolderPath))
            {
                File.Delete(fls);
            }
        }
        string SQL = "select v.vesselcode,v.vesselname,c.crewid,crewnumber,firstname+ ' '  + middlename + ' ' + lastname as CrewName,rankcode,isnull(Email,'') as Email " +
                                 "from crewpersonaldetails c " +
                                 "inner join vessel v on c.currentvesselid=v.vesselid and v.vesselid= " + _VesselId.ToString() + " " +
                                 "inner join rank r on c.currentrankid=r.rankid " +
                                 "where c.crewstatusid=3 " +
                                 "order by v.vesselcode,ranklevel";

        DataTable dtData = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        if (dtData.Rows.Count > 0)
        {
            //-----------------------------------
            string vslcode = dtData.Rows[0]["VesselCode"].ToString();
            string DestFolder = TrainingFolderPath + "\\" + vslcode;
            if (!Directory.Exists(DestFolder))
            {
                Directory.CreateDirectory(DestFolder);
            }
            else
            {
                foreach (string fls in Directory.GetFiles(DestFolder))
                {
                    File.Delete(fls);
                }

            }
            //-----------------------------------
            foreach (DataRow dr in dtData.Rows)
            {
                if (dr["Email"].ToString().Trim() == "") continue;
                try
                {
                    dctVessels.Add(vslcode, dr["Email"].ToString());
                }
                catch { }

                //-----------------------------
                string SourceFileContent = File.ReadAllText(TrainingTemplatePath);
                string DestFile = TrainingFolderPath + "\\" + dr["VesselCode"].ToString() + "\\" + dr["CrewNumber"].ToString() + ".htm";
                SourceFileContent = SourceFileContent.Replace("$VESSELCODE$", dr["VesselCode"].ToString());
                SourceFileContent = SourceFileContent.Replace("$CREWNUMBER$", dr["CrewNumber"].ToString());
                SourceFileContent = SourceFileContent.Replace("$CREWNAME$", dr["CrewName"].ToString());
                SourceFileContent = SourceFileContent.Replace("$RANK$", dr["rankCode"].ToString());

                int SrNo = 1;
                string DataRows = "";


                //DataTable dtTraining = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.sp_getTrainingMatrix " + dr["CrewId"].ToString());
                DataTable dtTraining = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM " +
                                                                                   "(  " +
                                                                                   "SELECT DISTINCT A.TrainingId,dbo.fn_getSimilerTrainingsName(A.TRAININGID) AS SIMILERTRAININGS,[dbo].[sp_getNextDue_DB](A.CREWID,A.TRAININGID) AS NextDue,[dbo].[sp_getNextDuePK_DB](A.CREWID,A.TRAININGID) AS PK " +
                                                                                   "FROM  " +
                                                                                   "(  " +
                                                                                   "    SELECT DISTINCT CREWID,TRAININGID from CREWTRAININGREQUIREMENT WHERE CREWID=" + dr["CrewId"].ToString() +
                                                                                   ") A  " +
                                                                                   ") B WHERE NextDue IS NOT NULL");


                for (int i = 0; i <= dtTraining.Rows.Count - 1; i++)
                {
                    DataRows = DataRows + GetdataRow(SrNo.ToString(), dtTraining.Rows[i]["TrainingId"].ToString(), dtTraining.Rows[i]["PK"].ToString(), dtTraining.Rows[i]["SIMILERTRAININGS"].ToString(), Common.ToDateString(dtTraining.Rows[i]["NextDue"]));
                    SrNo = SrNo + 1;
                }

                if (SrNo == 1) continue;

                SourceFileContent = SourceFileContent.Replace("$MaxRowCount$", dtTraining.Rows.Count.ToString());
                SourceFileContent = SourceFileContent.Replace("$DATAROWS$", DataRows);
                File.WriteAllText(DestFile, SourceFileContent);


                //l1.Text = l1.Text + ", " + DestFile.ToString();
                //-----------------------------
            }
        }
        //-----------------------------


        foreach (KeyValuePair<string, string> kvp in dctVessels)
        {
            string SourceFolder = TrainingFolderPath + "\\" + kvp.Key;
            string DestFolder = TrainingFolderPath;
            string zipFileName = kvp.Key + ".zip";
            using (ZipFile zip = new ZipFile())
            {
                string[] Files = Directory.GetFiles(SourceFolder);
                if (Files.Length <= 0)
                {
                    Directory.Delete(SourceFolder);
                    continue;
                }
                for (int i = 0; i <= Files.Length - 1; i++)
                {
                    zip.AddFile(Files[i]);
                }
                zip.Save(DestFolder + "\\" + zipFileName);
            }
            //-------------------------
            SendTrainingRequirementMailToVessel(kvp.Key, kvp.Value, DestFolder + "\\" + zipFileName);
        }


        //-----------------------------
    }
    private String GetdataRow(string Sno, string TID, string PKId, string Tname, string NextDue)
    {
        string DataRow = "<tr id='tr" + Sno + "'> " +
                        "<td> <label name='lblSrNo'>" + Sno + "</label> </td> " +
                        "<td>  " +
                           "<label name='lblTrainingName' >" + Tname + "</label>  " +
                           "<input type='hidden' id='hfTrainingID" + Sno + "' value='" + TID + "' /> " +
                           "<input type='hidden' id='hfTrainingRequirementID" + Sno + "' value='" + PKId + "' /> " +
                        "</td> " +
                        "<td> <label id='lblDueDate" + Sno + "' >" + NextDue + "</label> </td> " +
                        "<td> " +
                           "<input type='text' style='width:80px; font-weight:bold;' id='lblFromDate" + Sno + "' maxlength='10' onfocus=\"showCalendar('',this,this,'','holder1',5,22,1)\" title='Click here to enter date ' /> " +
                        "</td> " +
                        "<td> " +
                           "<input type='text' style='width:80px; font-weight:bold;' id='lblToDate" + Sno + "' maxlength='10' onfocus=\"showCalendar('',this,this,'','holder1',5,22,1)\" title='Click here to enter date '  /> " +
                        "</td> " +
                        "<td> " +
                            "<select style='width:130px; font-weight:bold;' id='ddlInstitute" + Sno + "' > " +
                                "<option value='0'  >Select</option> " +
                                "<option value='1'  >MTM YGM</option> " +
                                "<option value='2'  >DMA YGN(JV)</option> " +
                                "<option value='3' selected='selected' >ONBOARD</option> " +
                                "<option value='4'  >MMMC YGN(JV)</option> " +
                                "<option value='5'  >MOSA YGN(JV)</option> " +
                                "<option value='6'  >MTM INDIA</option> " +
                            "</select> " +
                        "</td> " +
                    "</tr>     ";
        return DataRow;
    }
    public void SendTrainingRequirementMailToVessel(string VesselCode, string MailAddress, string FileName)
    {
        string FileFullName = FileName;
        FileName = Path.GetFileName(FileName);
        string VslCode = FileName.Substring(0, 3);
        string DateString = DateTime.Today.ToString("dd-MMM-yyyy");
        List<string> ToAddresses = new List<string>();
        List<string> CCAddresses = new List<string>();
        StringBuilder msgFormat = new StringBuilder();

        ToAddresses.Add("emanager@energiossolutions.com");
        //ToAddresses.Add(MailAddress)
        //CCAddresses.Add("asingh@energiossolutions.com");

        msgFormat.Append("<html><head></head><body>" + "\n");
        msgFormat.Append("<table width=100% align=center> " + "\n");
        msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "******* Onboard Training Record Update ****" + "</td></tr>" + "\n");
        msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">&nbsp;" + "</td></tr>" + "\n");
        msgFormat.Append("<tr><td width=100% style=\"text-align: left\">" + "Dear Captain,  " + "</td></tr>" + "\n");
        msgFormat.Append("<tr><td width=100% height=40 style=\"text-align: left\">Attached please find the Training requirements for your crew onboard.If any of the  training is completed on board kindly update the same and send the xml file to asingh@energiossolutions.com.</td></tr>" + "\n");
        msgFormat.Append("<tr><td width=100% style=\"text-align: left\">Incase if any issue please contact IT Dept on emanager@energiossolutions.com" + "</td></tr>" + "\n");

        msgFormat.Append("<tr><td width=100% style=\"text-align: left\">&nbsp;" + "</td></tr>" + "\n");
        msgFormat.Append("<tr><td width=100% style=\"text-align: left\">Thank you" + "</td></tr>" + "\n");
        msgFormat.Append("<tr><td width=100% style=\"text-align: left\">&nbsp;" + "</td></tr>" + "\n");
        msgFormat.Append("<tr><td width=100% style=\"text-align: left\">P.S: This email will be sent automatically every 1st and 15th of the month.Please do not reply to this email." + "</td></tr>" + "\n");
        msgFormat.Append("<tr><td width=100% style=\"text-align: left\">&nbsp;" + "</td></tr>" + "\n");

        msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">***************" + "</td></tr>" + "\n");
        msgFormat.Append("</table>" + "\n");
        msgFormat.Append("</body></html>" + "\n");
        if (SendMail(ToAddresses.ToArray(), CCAddresses.ToArray(), "Onboard Training Record Update : " + VesselCode, msgFormat.ToString(), FileFullName, "Onboard training update"))
        {
            lblMsg.Text = "Mail sent successfully.";
        }
        else
        {
            lblMsg.Text = "Unable to send mail.";
        }
    }
    public bool SendMail(string[] ToAddresses, string[] CCAddresses, string Subject, string BodyContent, string AttachMentPath, string MailDetails)
    {
        string MailClient = ConfigurationManager.AppSettings["SMTPServerName"].ToString();
        int Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
        string SenderAddress = ConfigurationManager.AppSettings["FromAddress"];
        string SenderUserName = ConfigurationManager.AppSettings["SMTPUserName"];
        string SenderPass = ConfigurationManager.AppSettings["SMTPUserPwd"];

        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        StringBuilder msgFormat = new StringBuilder();
        string strToAddresses = string.Join(";", ToAddresses);
        string strCCAddresses = string.Join(";", CCAddresses);
        try
        {
            //if (chkTest.Checked)
            //{

            //    objMessage.From = new MailAddress("pankaj.k@esoftech.com");
            //    objSmtpClient.Host = "smtp.gmail.com";
            //    objSmtpClient.Port = 25;
            //    objMessage.To.Add("asingh@energiossolutions.com");
            //    objMessage.To.Add("asingh@energiossolutions.com");
            //    objMessage.To.Add("asingh@energiossolutions.com");

            //    objSmtpClient.EnableSsl = true;
            //    objMessage.To.Add("umakant.v@esoftech.com");
            //    objSmtpClient.Credentials = new NetworkCredential("asingh@energiossolutions.comm", "soft99");
            //    objMessage.Body = "ToAddressees : " + strToAddresses + "</br></br>CCAddresses : " + strCCAddresses + "</br></br>Body : ----------------------------</br></br>" + BodyContent + "";
            //}
            //else
            //{
            objMessage.From = new MailAddress(SenderAddress);
            objSmtpClient.Credentials = new NetworkCredential(SenderUserName, SenderPass);
            objSmtpClient.Host = MailClient;
            objSmtpClient.Port = Port;

            foreach (string add in ToAddresses)
            {
                objMessage.To.Add(add);
            }
            if (CCAddresses != null)
            {
                foreach (string add in CCAddresses)
                {
                    objMessage.CC.Add(add);
                }
            }
            objSmtpClient.EnableSsl = true;
            objMessage.Body = BodyContent;
            //}

            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            if (File.Exists(AttachMentPath))
                objMessage.Attachments.Add(new System.Net.Mail.Attachment(AttachMentPath));
            objSmtpClient.Send(objMessage);
            //AddMessage(MailDetails + "mail sent successfully. FileName:" + AttachMentPath + "");
            return true;
        }
        catch (System.Exception ex)
        {
            //AddMessage("Error while sending " + MailDetails + "mail. FileName :" + AttachMentPath, ex.Message);
            return false;
        }
    }

    #endregion
    protected void btnTrainingMatrix_Click(object sender, EventArgs e)
    {
        int vesselId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "TM", "window.open('popupTrainingMatrix.aspx?VSL=" + vesselId + "', '_blank', '')", true);
    }
    protected void btnLink_Click(object sender, EventArgs e)
    {
        int officeid = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string mode = ((LinkButton)sender).CssClass;

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "TM", "window.open('popupMonitorTrainingDetails_New.aspx?OfficeId=" + officeid  + "&FD=01-"+ GetMonthName(ddlMonthFrom.SelectedValue) + "-" + ddlYearFrom.SelectedValue.ToString() + "&TD=01-" + GetMonthName(ddlMonthTO.SelectedValue) + "-" + ddlYearTO.SelectedValue.ToString() + "&Mode=" + mode + "', '_blank', '')", true);
        if(mode.EndsWith("Due"))
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TM", "window.open('popupMonitorTrainingDetails_Due.aspx?OfficeId=" + officeid + "&FD=01-" + GetMonthName(ddlMonthFrom.SelectedValue) + "-" + ddlYearFrom.SelectedValue.ToString() + "&TD=01-" + GetMonthName(ddlMonthTO.SelectedValue) + "-" + ddlYearTO.SelectedValue.ToString() + "&Mode=" + mode + "', '_blank', '')", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TM", "window.open('popupMonitorTrainingDetails_New.aspx?OfficeId=" + officeid + "&FD=01-" + GetMonthName(ddlMonthFrom.SelectedValue) + "-" + ddlYearFrom.SelectedValue.ToString() + "&TD=01-" + GetMonthName(ddlMonthTO.SelectedValue) + "-" + ddlYearTO.SelectedValue.ToString() + "&Mode=" + mode + "', '_blank', '')", true);
        
    }
}