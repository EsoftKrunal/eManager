using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_Emtm_AlertHome : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int MyPosition
    {
        get { return Common.CastAsInt32(ViewState["MyPosition"]); }
        set{ ViewState["MyPosition"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            DataTable dtPositionId = Common.Execute_Procedures_Select_ByQueryCMS("SELECT POSITION,(SELECT VesselPositions FROM POSITION p WHERE p.POSITIONID=per.position) AS VESSELPOSITION FROM Hr_PersonalDetails per WHERE USERID=" + UserId.ToString());
            MyPosition = Common.CastAsInt32(dtPositionId.Rows[0]["VESSELPOSITION"]);
            LoadCompanyData();
            LoadMyData();
        }
    }
    
    public void LoadMyData()
    {
        string SQL = "SELECT * FROM ApplicationMaster ORDER BY ApplicationName";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptAppName_MA.DataSource = dt;
        rptAppName_MA.DataBind();

        //string MYVsls = "( TECHSUPDT=" + UserId.ToString() + " OR MARINESUPDT=" + UserId.ToString() + " OR FLEETMANAGER=" + UserId.ToString() + " OR TECHASSISTANT=" + UserId.ToString() + " OR MARINEASSISTANT=" + UserId.ToString() + " OR SPA=" + UserId.ToString() + " OR ACCTOFFICER=" + UserId.ToString() + " ) ";
        //string SQL = "SELECT VESSELNAME,M.ALERTTYPENAME,DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) AS AlertStartDate,M.*,T.*  " +
        //             "FROM VW_ALERTS T " +
        //             "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
        //             "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  AND " + MYVsls + " " +
        //             "WHERE DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() AND M.VesselRanks + ',' LIKE '%" + MyPosition + ",%' ";

        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //if (dt.Rows.Count > 0)
        //{
        //    DataRow [] drMWUC = dt.Select("AlertTypeId = 1");
        //    lnkMwucAlerts.Text = drMWUC.Length.ToString();

        //    DataRow[] drFUP = dt.Select("AlertTypeId = 2");
        //    lnkFollowupAlerts.Text = drFUP.Length.ToString();

        //    DataRow[] drMotor = dt.Select("AlertTypeId = 3");
        //    lnkMotorAlerts.Text = drMotor.Length.ToString();

        //    DataRow[] drDefects = dt.Select("AlertTypeId = 4");
        //    lnkDefectAlerts.Text = drDefects.Length.ToString();

        //    DataRow[] drDrilltraining = dt.Select("AlertTypeId = 5");
        //    lnkDrillTrainingAlerts.Text = drDrilltraining.Length.ToString();

        //    DataRow[] drVI = dt.Select("AlertTypeId = 6");
        //    lnkVI.Text = drVI.Length.ToString();

        //    DataRow[] drPVI = dt.Select("AlertTypeId = 7");
        //    lnkPVI.Text = drPVI.Length.ToString();

        //    DataRow[] drISM = dt.Select("AlertTypeId = 8");
        //    lnkISMAlerts.Text = drISM.Length.ToString();

        //    DataRow[] drISPS = dt.Select("AlertTypeId = 9");
        //    lnkISPSAlerts.Text = drISPS.Length.ToString();

        //    DataRow[] drTECH = dt.Select("AlertTypeId = 10");
        //    lnkTECHAlerts.Text = drTECH.Length.ToString();

        //    DataRow[] dr14001 = dt.Select("AlertTypeId = 11");
        //    lnk14001Alerts.Text = dr14001.Length.ToString();

        //    DataRow[] drSafety = dt.Select("AlertTypeId = 12");
        //    lnkSafetyAlerts.Text = drSafety.Length.ToString();

        //    DataRow[] drNAV = dt.Select("AlertTypeId = 13");
        //    lnkNAVAlerts.Text = drNAV.Length.ToString();
        //}
        
    } 
    public DataTable LoadMyAlerts(int ApplicationId)
    {
        return Common.Execute_Procedures_Select_ByQuery("SELECT Row_Number() over(order by AlertTypeId) As SrNo, * FROM tbl_AlertTypes WHERE ApplicationId = " + ApplicationId);
    } 
    public string GetMyAlertCount(int AlertTypeId)
    {
        if (AlertTypeId == 6)
        {
            string MYVsls = "SELECT VESSELID,(SELECT AlertDays FROM SHIPSOFT_ADMIN.DBO.TBL_ALERTTYPES WHERE AlertTypeId=6) AS NUMDAYS FROM DBO.VESSEL V WHERE V.VESSELSTATUSID=1  AND ( TECHSUPDT=" + UserId.ToString() + " OR MARINESUPDT=" + UserId.ToString() + " OR FLEETMANAGER=" + UserId.ToString() + " OR TECHASSISTANT=" + UserId.ToString() + " OR MARINEASSISTANT=" + UserId.ToString() + " OR SPA=" + UserId.ToString() + " OR ACCTOFFICER=" + UserId.ToString() + " ) ";
            int NumDays=0;
            string MYVsls_List = "";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(MYVsls);
            foreach (DataRow dr in dt.Rows)
            {
                NumDays=Common.CastAsInt32(dr["NUMDAYS"]);
                MYVsls_List +="," + dr["VESSELID"].ToString();
            }
            if (MYVsls_List.StartsWith(","))
                MYVsls_List = MYVsls_List.Substring(1);

            string SQL = "exec dbo.VettingPlannerReport '" + MYVsls_List + "'," + NumDays.ToString() + ",9999";
            dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            DataView dv = dt.DefaultView;
            dv.RowFilter = "( LastDone is not NULL) and sire_cdi<>'CDI' and nextinspstatus in ('Executed')";
            DataTable dtOut = dv.ToTable();
            return dtOut.Rows.Count.ToString();
        }
        else if (AlertTypeId == 14)
        {
            string SQL = "SELECT * FROM [dbo].[HR_LeaveRequest] INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =14  WHERE FORWARDEDTO= " + Session["ProfileId"].ToString() + " AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),LeaveFrom) <= getdate() and status='V' ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            return dt.Rows.Count.ToString();
        }
        else if (AlertTypeId == 15)
        {
            string SQL = "SELECT S.* FROM [dbo].[tbl_Publish_InformSuptd] S  " +
                         "INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =14  " +
                         "WHERE S.YEAR=YEAR(GETDATE()) AND MONTH=MONTH(GETDATE())-1 AND NOT EXISTS(SELECT CommCo FROM  dbo.tblbudgetlevelcomments C WHERE C.CommCo=S.Cocode AND C.CommYear=S.year AND C.CommPer=S.Month AND CommMajID=6 AND ISNULL(convert(varchar,COMMENT),'')<>'' )    " +
                         "AND S.CoCode IN (SELECT OWNERCODE FROM DBO.OWNER WHERE OWNERID IN (SELECT OWNERID FROM DBO.VESSEL WHERE TECHSUPDT=" + UserId.ToString() + " OR MARINESUPDT=" + UserId.ToString() + " OR FLEETMANAGER=" + UserId.ToString() + " OR TECHASSISTANT=" + UserId.ToString() + " OR MARINEASSISTANT=" + UserId.ToString() + " OR SPA=" + UserId.ToString() + " OR ACCTOFFICER=" + UserId.ToString() + " ))  " +
                         "AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),S.sendon) <= getdate() ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            return dt.Rows.Count.ToString();
        }
        if (AlertTypeId == 16)
        {
            string SQL = "SELECT S.* FROM [dbo].[tbl_Publish_InformSuptd] S  " +
                         "INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =16  " +
                         "WHERE S.YEAR=YEAR(GETDATE()) AND MONTH=MONTH(GETDATE())-1  " +
                         "AND EXISTS(SELECT CommCo FROM  dbo.tblbudgetlevelcomments C WHERE C.CommCo=S.Cocode AND C.CommYear=S.year AND C.CommPer=S.Month AND CommMajID=6 AND ISNULL(convert(varchar,COMMENT),'')<>'' )  " +
                         "AND not EXISTS( SELECT Cocode FROM dbo.tbl_PublishComments R WHERE R.cocode=S.Cocode AND R.year=S.year AND R.month=S.Month )" +
                         "AND S.CoCode IN (SELECT OWNERCODE FROM DBO.OWNER WHERE OWNERID IN (SELECT OWNERID FROM DBO.VESSEL WHERE TECHSUPDT=" + UserId.ToString() + " OR MARINESUPDT=" + UserId.ToString() + " OR FLEETMANAGER=" + UserId.ToString() + " OR TECHASSISTANT=" + UserId.ToString() + " OR MARINEASSISTANT=" + UserId.ToString() + " OR SPA=" + UserId.ToString() + " OR ACCTOFFICER=" + UserId.ToString() + " ))  " +
                         "AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),S.sendon) <= getdate() ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            return dt.Rows.Count.ToString();

        }
        else
        {
            string MYVsls = "( TECHSUPDT=" + UserId.ToString() + " OR MARINESUPDT=" + UserId.ToString() + " OR FLEETMANAGER=" + UserId.ToString() + " OR TECHASSISTANT=" + UserId.ToString() + " OR MARINEASSISTANT=" + UserId.ToString() + " OR SPA=" + UserId.ToString() + " OR ACCTOFFICER=" + UserId.ToString() + " ) ";
            string SQL = "SELECT VESSELNAME,M.ALERTTYPENAME,DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) AS AlertStartDate,M.*,T.*  " +
                         "FROM VW_ALERTS T " +
                         "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                         "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  AND " + MYVsls + " " +
                         "WHERE T.AlertTypeId = " + AlertTypeId + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() AND M.VesselRanks + ',' LIKE '%" + MyPosition + ",%' ";

            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            return dt.Rows.Count.ToString();
        }
    }

    public void LoadCompanyData() 
    {
        string SQL = "SELECT * FROM ApplicationMaster ORDER BY ApplicationName";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptAppName_CA.DataSource = dt;
        rptAppName_CA.DataBind();

        //string SQL = "SELECT VESSELNAME,M.ALERTTYPENAME,DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) AS AlertStartDate,M.*,T.*  " +
        //             "FROM VW_ALERTS T " +
        //             "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
        //             "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  " +
        //             "WHERE DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() ";

        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //if (dt.Rows.Count > 0)
        //{
        //    DataRow[] drMWUC = dt.Select("AlertTypeId = 1");
        //    lnkMwucAlerts_Comp.Text = drMWUC.Length.ToString();

        //    DataRow[] drFUP = dt.Select("AlertTypeId = 2");
        //    lnkFollowupAlerts_Comp.Text = drFUP.Length.ToString();

        //    DataRow[] drMotor = dt.Select("AlertTypeId = 3"); 
        //    lnkMotorAlerts_Comp.Text = drMotor.Length.ToString();

        //    DataRow[] drDefects = dt.Select("AlertTypeId = 4"); 
        //    lnkDefectAlerts_Comp.Text = drDefects.Length.ToString();

        //    DataRow[] drDrilltraining = dt.Select("AlertTypeId = 5");
        //    lnkDrillTrainingAlerts_Comp.Text = drDrilltraining.Length.ToString();

        //    DataRow[] drVI = dt.Select("AlertTypeId = 6");
        //    lnkVI_Comp.Text = drVI.Length.ToString();

        //    DataRow[] drPVI = dt.Select("AlertTypeId = 7");
        //    lnkPVI_Comp.Text = drPVI.Length.ToString();

        //    DataRow[] drISM = dt.Select("AlertTypeId = 8");
        //    lnkISMAlerts_Comp.Text = drISM.Length.ToString();

        //    DataRow[] drISPS = dt.Select("AlertTypeId = 9");
        //    lnkISPSAlerts_Comp.Text = drISPS.Length.ToString();

        //    DataRow[] drTECH = dt.Select("AlertTypeId = 10");
        //    lnkTECHAlerts_Comp.Text = drTECH.Length.ToString();

        //    DataRow[] dr14001 = dt.Select("AlertTypeId = 11");
        //    lnk14001Alerts_Comp.Text = dr14001.Length.ToString();

        //    DataRow[] drSafety = dt.Select("AlertTypeId = 12");
        //    lnkSafetyAlerts_Comp.Text = drSafety.Length.ToString();

        //    DataRow[] drNAV = dt.Select("AlertTypeId = 13");
        //    lnkNAVAlerts_Comp.Text = drNAV.Length.ToString();
        //}

    }
    public DataTable LoadCompAlerts(int ApplicationId)
    {
        return Common.Execute_Procedures_Select_ByQuery("SELECT Row_Number() over(order by AlertTypeId) As SrNo, * FROM tbl_AlertTypes WHERE ApplicationId = " + ApplicationId);
    }
    public string GetCompAlertCount(int AlertTypeId)
    {
        if (AlertTypeId == 6)
        {
            string MYVsls = "SELECT VESSELID,(SELECT AlertDays FROM SHIPSOFT_ADMIN.DBO.TBL_ALERTTYPES WHERE AlertTypeId=6) AS NUMDAYS FROM DBO.VESSEL V WHERE V.VESSELSTATUSID=1  ";
            int NumDays = 0;
            string MYVsls_List = "";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(MYVsls);
            foreach (DataRow dr in dt.Rows)
            {
                NumDays = Common.CastAsInt32(dr["NUMDAYS"]);
                MYVsls_List += "," + dr["VESSELID"].ToString();
            }
            if (MYVsls_List.StartsWith(","))
                MYVsls_List = MYVsls_List.Substring(1);

            string SQL = "exec dbo.VettingPlannerReport '" + MYVsls_List + "'," + NumDays.ToString() + ",9999";
            dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            DataView dv=dt.DefaultView;
            dv.RowFilter = "( LastDone is not NULL) and sire_cdi<>'CDI' and nextinspstatus in ('Executed')";
            DataTable dtOut = dv.ToTable();
            return dtOut.Rows.Count.ToString();
        }
        else if (AlertTypeId == 14)
        {
            string SQL = "SELECT * FROM [dbo].[HR_LeaveRequest] INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =14  WHERE DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),LeaveFrom) <= getdate() and status='V'";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            return dt.Rows.Count.ToString();
        }
        else if (AlertTypeId == 15)
        {
            string SQL = "SELECT S.* FROM [dbo].[tbl_Publish_InformSuptd] S  " +
                         "INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =14  " +
                         "WHERE S.YEAR=YEAR(GETDATE()) AND MONTH=MONTH(GETDATE())-1 AND NOT EXISTS(SELECT CommCo FROM  dbo.tblbudgetlevelcomments C WHERE C.CommCo=S.Cocode AND C.CommYear=S.year AND C.CommPer=S.Month AND CommMajID=6 AND ISNULL(convert(varchar,COMMENT),'')<>'' )  " +
                         "AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),S.sendon) <= getdate() ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            return dt.Rows.Count.ToString();
        }
        if (AlertTypeId == 16)
        {
            string SQL = "SELECT S.* FROM [dbo].[tbl_Publish_InformSuptd] S  " +
                         "INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =16  " +
                         "WHERE S.YEAR=YEAR(GETDATE()) AND MONTH=MONTH(GETDATE())-1  " +
                         "AND EXISTS(SELECT CommCo FROM  dbo.tblbudgetlevelcomments C WHERE C.CommCo=S.Cocode AND C.CommYear=S.year AND C.CommPer=S.Month AND CommMajID=6 AND ISNULL(convert(varchar,COMMENT),'')<>'' )  " +
                         "AND not EXISTS( SELECT Cocode FROM dbo.tbl_PublishComments R WHERE R.cocode=S.Cocode AND R.year=S.year AND R.month=S.Month )" +
                         "AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),S.sendon) <= getdate() ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            return dt.Rows.Count.ToString();

        }
        else
        {
            string SQL = "SELECT VESSELNAME,M.ALERTTYPENAME,DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) AS AlertStartDate,M.*,T.*  " +
                         "FROM VW_ALERTS T " +
                         "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                         "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  " +
                         "WHERE T.AlertTypeId = " + AlertTypeId + " AND  DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() ";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            return dt.Rows.Count.ToString();
        }
    }
    
    protected void lnkMyAlerts_Click(object sender, EventArgs e)
    {
        int AT = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string Text = ((LinkButton)sender).Text;

        if (Text.Trim() != "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=" + AT + "&MP="+ MyPosition+"', '', '');", true);
        }

    }
    //protected void lnkFollowupAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkFollowupAlerts.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=2&MP=" + MyPosition + "', '', '');", true);
    //    }

    //}
    //protected void lnkMotorAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkMotorAlerts.Text.ToString() != "0")
    //    {
    //      ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=3&MP=" + MyPosition + "', '', '');", true);
    //    }

    //}
    //protected void lnkDefectAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkDefectAlerts.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=4&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}
    //protected void lnkDrillTrainingAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkDrillTrainingAlerts.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=5&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}
    //protected void lnkVIAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkVI.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=6&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}
    //protected void lnkPVIAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkPVI.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=7&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}
    //protected void lnkISMAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkISMAlerts.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=8&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}
    //protected void lnkISPSAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkISPSAlerts.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=9&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}
    //protected void lnkTECHAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkTECHAlerts.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=10&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}
    //protected void lnk14001Alerts_Click(object sender, EventArgs e)
    //{
    //    if (lnk14001Alerts.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=11&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}
    //protected void lnkSafetyAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkSafetyAlerts.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=12&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}
    //protected void lnkNAVAlerts_Click(object sender, EventArgs e)
    //{
    //    if (lnkNAVAlerts.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?MA=1&AT=13&MP=" + MyPosition + "', '', '');", true);
    //    }
    //}

    protected void lnkCompAlerts_Click(object sender, EventArgs e)
    {
        int AT = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        string Text = ((LinkButton)sender).Text;

        if (Text.Trim() != "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=" + AT + "', '', '');", true);
        }
    }
    //protected void lnkFollowupAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkFollowupAlerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=2', '', '');", true);
    //    }
    //}
    //protected void lnkMotorAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkMotorAlerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=3', '', '');", true);
    //    }
    //}
    //protected void lnkDefectAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkDefectAlerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=4', '', '');", true);
    //    }
    //}
    //protected void lnkDrillTrainingAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkDrillTrainingAlerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=5', '', '');", true);
    //    }
    //}
    //protected void lnkVIAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkVI_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=6', '', '');", true);
    //    }
    //}
    //protected void lnkPVIAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkPVI_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=7', '', '');", true);
    //    }
    //}
    //protected void lnkISMAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkISMAlerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=8', '', '');", true);
    //    }
    //}
    //protected void lnkISPSAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkISPSAlerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=9', '', '');", true);
    //    }
    //}
    //protected void lnkTECHAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkTECHAlerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=10', '', '');", true);
    //    }
    //}
    //protected void lnk14001Alerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnk14001Alerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=11', '', '');", true);
    //    }
    //}
    //protected void lnkSafetyAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkSafetyAlerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=12', '', '');", true);
    //    }
    //}
    //protected void lnkNAVAlerts_Comp_Click(object sender, EventArgs e)
    //{
    //    if (lnkNAVAlerts_Comp.Text.ToString() != "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('Emtm_Alerts.aspx?AT=13', '', '');", true);
    //    }
    //}

}