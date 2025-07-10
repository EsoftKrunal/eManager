using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class emtm_Emtm_Alerts : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int MA
    {
        get { return Common.CastAsInt32(ViewState["MA"]); }
        set { ViewState["MA"] = value; }
    }
    public int AT
    {
        get { return Common.CastAsInt32(ViewState["AT"]); }
        set { ViewState["AT"] = value; }
    }
    public int MP
    {
        get { return Common.CastAsInt32(ViewState["MP"]); }
        set { ViewState["MP"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
          SessionManager.SessionCheck_New();
        //-----------------------------
        
        if (!IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            MA = Common.CastAsInt32(Request.QueryString["MA"]);
            AT = Common.CastAsInt32(Request.QueryString["AT"]);
            MP = Common.CastAsInt32(Request.QueryString["MP"]);
            LoadFleet();
            BindVessels();
            BindOffice();
            
            if (MA == 1)
            {
                LoadMyData();
            }
            else
            {
                dv_Fleet.Visible = true;
                LoadCompanyData();
            }

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM tbl_AlertTypes WHERE ALERTTYPEID=" + AT);
            if (dt.Rows.Count > 0)
                lblAlertType.Text = dt.Rows[0]["AlertTypeName"].ToString();
        }
    }
    public void LoadFleet()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.FLEETMASTER");
        ddlFleet.DataSource = dt;
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetId";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("< ALL >", "0"));
    }
    public void BindVessels()
    {
        string SQL = "";
        
        if (ddlFleet.SelectedIndex == 0)
        {
            SQL = "SELECT VesselCode, VesselName FROM DBO.VESSEL WHERE VESSELSTATUSID <>2  ORDER BY VESSELNAME";
        }
        else
        {
            SQL = "SELECT VesselCode, VesselName FROM DBO.VESSEL WHERE VESSELSTATUSID <>2  AND FleetID=" + ddlFleet.SelectedValue + " ORDER BY VESSELNAME";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField = "VesselCode";
        ddlVessel.DataBind();

        ddlVessel.Items.Insert(0, new ListItem("< All Vessel >", ""));

    }
    public void BindOffice()
    {
        string SQL = "SELECT OfficeId, OfficeName FROM [dbo].[Office] ORDER BY OfficeName";
        

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        ddlOffice.DataSource = dt;
        ddlOffice.DataTextField = "OfficeName";
        ddlOffice.DataValueField = "OfficeId";
        ddlOffice.DataBind();

        ddlOffice.Items.Insert(0, new ListItem("< All Office >", ""));

    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (MA == 1)
        {
            LoadMyData();
        }
        else
        {
            LoadCompanyData();
        }
    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessels();
        ddlVessel_SelectedIndexChanged(sender, e);
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (MA == 1)
        {
            LoadMyData();
        }
        else
        {
            LoadCompanyData();
        }
    }
    protected string GetClass(object _dt)
    {
        try{
            DateTime dt = Convert.ToDateTime(_dt);
            if (dt < DateTime.Today)
                return "od";
            else
                return "";

        }
        catch
        {return "";}
    }
    public void LoadMyData()
    {
        string MYVsls = "( TECHSUPDT=" + UserId.ToString() + " OR MARINESUPDT=" + UserId.ToString() + " OR FLEETMANAGER=" + UserId.ToString() + " OR TECHASSISTANT=" + UserId.ToString() + " OR MARINEASSISTANT=" + UserId.ToString() + " OR SPA=" + UserId.ToString() + " OR ACCTOFFICER=" + UserId.ToString() + " ) ";
        string SQL = "";

        if (AT == 1)
        {

            SQL = "SELECT VESSELNAME,T.REFNUMBER,T.DATE_FIELD " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  AND " + MYVsls + " " + (ddlVessel.SelectedIndex == 0 ? "" : " AND VESSELCODE ='" + ddlVessel.SelectedValue + "' ") +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() AND   M.VesselRanks + ',' LIKE '%" + MP + ",%'  ORDER By VESSELNAME";
        }
        if (AT == 2)
        {
            SQL = "SELECT VESSELNAME,T.REFNUMBER ,T.DATE_FIELD, " + 
                  "CASE T.Key1  " + 
                  "WHEN 1 THEN (SELECT Deficiency FROM DBO.t_Observations WHERE Id = T.PK )  " +  
                  "WHEN 2 THEN (SELECT Deficiency FROM DBO.t_Observationsnew WHERE TableId = T.PK ) " +
                  "WHEN 3 THEN (SELECT FP_Deficiency FROM DBO.FR_FollowUpList WHERE FP_Id = T.PK )  " + 
                  "ELSE '' END AS FollowupText " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  AND " + MYVsls + " " + (ddlVessel.SelectedIndex == 0 ? "" : " AND VESSELCODE ='" + ddlVessel.SelectedValue + "' ") +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() AND   M.VesselRanks + ',' LIKE '%" + MP + ",%'  ORDER By VESSELNAME";
        }
        if (AT == 3)
        {
            SQL = "SELECT VESSELNAME,T.REFNUMBER,(SELECT Convert(varchar, DateADD(M,1,  convert(datetime, cast(RMonth AS varchar) + '/' + '05' + '/' + cast(RYear as varchar), 101)), 106) FROM DBO.MORTORMASTER WHERE MID = T.PK)  AS 'Due Date' " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  AND " + MYVsls + " " + (ddlVessel.SelectedIndex == 0 ? "" : " AND VESSELCODE ='" + ddlVessel.SelectedValue + "' ") +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() AND   M.VesselRanks + ',' LIKE '%" + MP + ",%'  ORDER By VESSELNAME";
        }
        if (AT == 4)
        {
            SQL = "SELECT VESSELNAME,T.REFNUMBER,(SELECT DEFECTDetails FROM [dbo].[VSL_DefectDetailsMaster] WHERE DefectNo = T.PK)  AS Defects, Date_Field " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  AND " + MYVsls + " " + (ddlVessel.SelectedIndex == 0 ? "" : " AND VESSELCODE ='" + ddlVessel.SelectedValue + "' ") +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() AND   M.VesselRanks + ',' LIKE '%" + MP + ",%'  ORDER By VESSELNAME";
        }
        if (AT == 5)
        {
            SQL = "SELECT VESSELNAME,substring(T.PK,1,1) AS TYPE, " +
                  "(SELECT DRILLNAME FROM  " +
                  "(  " +
                  "SELECT 'D'+CONVERT(vARCHAR,DRILLID) AS PK,DRILLNAME FROM DBO.DT_DRILLMASTER WHERE RECORDTYPE='D'  " +
                  "UNION  " +
                  "SELECT 'T'+CONVERT(vARCHAR,TRAININGID) AS PK,TraininingName FROM DBO.DT_TRAININGMASTER WHERE RECORDTYPE='T'  " +
                  ") K WHERE K.PK=T.PK) AS DRILLNAME,  " +
                  "T.Date_Field " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1 AND " + MYVsls + " " + (ddlVessel.SelectedIndex == 0 ? "" : " AND VESSELCODE ='" + ddlVessel.SelectedValue + "' ") +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() AND   M.VesselRanks + ',' LIKE '%" + MP + ",%'  ORDER By VESSELNAME";
        }
        if (AT == 6)
        {
            string MYVsls1 = "SELECT VESSELID,(SELECT AlertDays FROM SHIPSOFT_ADMIN.DBO.TBL_ALERTTYPES WHERE AlertTypeId=6) AS NUMDAYS FROM DBO.VESSEL V WHERE V.VESSELSTATUSID=1  AND ( TECHSUPDT=" + UserId.ToString() + " OR MARINESUPDT=" + UserId.ToString() + " OR FLEETMANAGER=" + UserId.ToString() + " OR TECHASSISTANT=" + UserId.ToString() + " OR MARINEASSISTANT=" + UserId.ToString() + " OR SPA=" + UserId.ToString() + " OR ACCTOFFICER=" + UserId.ToString() + " ) ";
            int NumDays = 0;
            string MYVsls_List = "";
            DataTable dtVsls = Common.Execute_Procedures_Select_ByQuery(MYVsls1);
            foreach (DataRow dr in dtVsls.Rows)
            {
                NumDays = Common.CastAsInt32(dr["NUMDAYS"]);
                MYVsls_List += "," + dr["VESSELID"].ToString();
            }
            if (MYVsls_List.StartsWith(","))
                MYVsls_List = MYVsls_List.Substring(1);

            SQL = "exec dbo.VettingPlannerReport '" + MYVsls_List + "'," + NumDays.ToString() + ",9999";
           
        }
        if (AT == 8 || AT == 9 || AT == 10 || AT == 11 || AT == 12 || AT == 13)
        {
            SQL = "SELECT VESSELNAME,T.Date_Field, T.REFNUMBER " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  AND " + MYVsls + " " + (ddlVessel.SelectedIndex == 0 ? "" : " AND VESSELCODE ='" + ddlVessel.SelectedValue + "' ") +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() AND   M.VesselRanks + ',' LIKE '%" + MP + ",%'  ORDER By VESSELNAME";
        }
        if (AT == 7)
        {
            SQL = "SELECT VESSELNAME,T.Date_Field, T.REFNUMBER,(SELECT PlanRemark FROM DBO.t_inspectiondue WHERE Id =T.PK) AS PlanRemark, (SELECT FIRSTNAME+ ' ' + LASTNAME FROM DBO.USERMASTER WHERE LOGINID IN (select TOP 1 SUPERINTENDENTID from dbo.t_InspSupt where inspectiondueid=T.PK AND ATTENDING=1)) As SupName " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  AND " + MYVsls + " " + (ddlVessel.SelectedIndex == 0 ? "" : " AND VESSELCODE ='" + ddlVessel.SelectedValue + "' ") +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() AND   M.VesselRanks + ',' LIKE '%" + MP + ",%'  ORDER By VESSELNAME";
        }

        if (AT == 14)
        {
            dv_Fleet.Visible = false;
            dv_Vessel.Visible = false;
            dv_Office.Visible = true;

            string WhereCond = "";

            if (ddlOffice.SelectedIndex != 0)
            {
                WhereCond = " WHERE OfficeId = " + ddlOffice.SelectedValue;
            }

            SQL = "SELECT * FROM (SELECT (SELECT [EmpCode] FROM [dbo].[Hr_PersonalDetails] WHERE EmpId = L.[EmpId]) AS EmpCode, (SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [dbo].[Hr_PersonalDetails] WHERE EmpId = L.[EmpId]) AS EmployeeName, (SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [dbo].[Hr_PersonalDetails] WHERE EmpId = L.ForwardedTo) AS ForwardedTo, (SELECT Office FROM [dbo].[Hr_PersonalDetails] WHERE EmpId = L.[EmpId]) AS OfficeId, LeaveFrom, LeaveTo, Reason, RequestDate As ForwardedOn,(SELECT [OfficeName] FROM [dbo].[Office] WHERE [OfficeId] = (SELECT Office FROM  [dbo].[Hr_PersonalDetails] WHERE EmpId = L.[EmpId])) AS Office FROM [dbo].[HR_LeaveRequest] L INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =14 WHERE STATUS='V' and L.FORWARDEDTO= " + Session["ProfileId"].ToString() + " AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),LeaveFrom) <= getdate() ) A " + WhereCond + " ORDER BY EmployeeName"; 
        }
        
        if (AT == 15)
        {
            SQL = "SELECT S.*,dateadd(day,7,sendon) as NextDate FROM [dbo].[tbl_Publish_InformSuptd] S  " +
                  "INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =15  " +
                  "WHERE S.YEAR=YEAR(GETDATE()) AND MONTH=MONTH(GETDATE())-1 AND NOT EXISTS(SELECT CommCo FROM  dbo.tblbudgetlevelcomments C WHERE C.CommCo=S.Cocode AND C.CommYear=S.year AND C.CommPer=S.Month AND CommMajID=6 AND ISNULL(convert(varchar,COMMENT),'')<>'' )    " +
                  "AND S.CoCode IN (SELECT OWNERCODE FROM DBO.OWNER WHERE OWNERID IN (SELECT OWNERID FROM DBO.VESSEL WHERE TECHSUPDT=" + UserId.ToString() + " OR MARINESUPDT=" + UserId.ToString() + " OR FLEETMANAGER=" + UserId.ToString() + " OR TECHASSISTANT=" + UserId.ToString() + " OR MARINEASSISTANT=" + UserId.ToString() + " OR SPA=" + UserId.ToString() + " OR ACCTOFFICER=" + UserId.ToString() + " ))  " +
                  "AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),S.sendon) <= getdate() ";
        }

        if (AT == 16)
        {
            SQL = "SELECT S.* FROM [dbo].[tbl_Publish_InformSuptd] S  " +
                  "INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =16  " +
                  "WHERE S.YEAR=YEAR(GETDATE()) AND MONTH=MONTH(GETDATE())-1  " +
                  "AND EXISTS(SELECT CommCo FROM  dbo.tblbudgetlevelcomments C WHERE C.CommCo=S.Cocode AND C.CommYear=S.year AND C.CommPer=S.Month AND CommMajID=6 AND ISNULL(convert(varchar,COMMENT),'')<>'' )  " +
                  "AND not EXISTS( SELECT Cocode FROM dbo.tbl_PublishComments R WHERE R.cocode=S.Cocode AND R.year=S.year AND R.month=S.Month )" +
                  "AND S.CoCode IN (SELECT OWNERCODE FROM DBO.OWNER WHERE OWNERID IN (SELECT OWNERID FROM DBO.VESSEL WHERE TECHSUPDT=" + UserId.ToString() + " OR MARINESUPDT=" + UserId.ToString() + " OR FLEETMANAGER=" + UserId.ToString() + " OR TECHASSISTANT=" + UserId.ToString() + " OR MARINEASSISTANT=" + UserId.ToString() + " OR SPA=" + UserId.ToString() + " OR ACCTOFFICER=" + UserId.ToString() + " ))  " +
                  "AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),S.sendon) <= getdate() ";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            if (AT == 1)
            {
                dv_MWUC.Visible = true;

                rptAlerts_MWUC.DataSource = dt;
                rptAlerts_MWUC.DataBind();
            }
            if (AT == 2)
            {
                dv_Followup.Visible = true;

                rptAlerts_Fup.DataSource = dt;
                rptAlerts_Fup.DataBind();
            }
            if (AT == 3)
            {
                dv_Motor.Visible = true;

                rptAlerts_Motor.DataSource = dt;
                rptAlerts_Motor.DataBind();
            }
            if (AT == 4)
            {
                dv_Defects.Visible = true;

                rptAlert_Defects.DataSource = dt;
                rptAlert_Defects.DataBind();
            }
            if (AT == 5)
            {
                dv_DrillTraining.Visible = true;

                rptAlerts_DT.DataSource = dt;
                rptAlerts_DT.DataBind();
            }

            if (AT == 6 )
            {
                dv_6.Visible = true;

                DataView dv = dt.DefaultView;
                dv.RowFilter = "( LastDone is not NULL) and sire_cdi<>'CDI' and ( nextinspstatus in ('Executed') or nextinspstatus is null)";
                DataTable dtOut = dv.ToTable();

                rptAlerts_6.DataSource = dtOut;
                rptAlerts_6.DataBind();

                if (dtOut.Rows.Count > 0)
                    lblRecCount.Text = dtOut.Rows.Count.ToString() + " Records found.";
                else
                    lblRecCount.Text = "No Record found.";
            }
            if (AT == 8 || AT == 9 || AT == 10 || AT == 11 || AT == 12 || AT == 13)
            {
                dv_VI.Visible = true;

                rptAlerts_VI.DataSource = dt;
                rptAlerts_VI.DataBind();
            }

            if (AT == 7)
            {
                dv_PVI.Visible = true;

                rptAlerts_PVI.DataSource = dt;
                rptAlerts_PVI.DataBind();
            }

            if (AT == 14)
            {
                dv_Emtm_LR.Visible = true;

                rptAlerts_LR.DataSource = dt;
                rptAlerts_LR.DataBind();
            }
            if (AT == 15)
            {
                dv_15.Visible = true;

                rptAlerts_15.DataSource = dt;
                rptAlerts_15.DataBind();
            }
            if (AT == 16)
            {
                dv_16.Visible = true;

                rptAlerts_16.DataSource = dt;
                rptAlerts_16.DataBind();
            }

            if (AT != 6)
            {
                lblRecCount.Text = dt.Rows.Count.ToString() + " Records found.";
            }
            
        }
        else
        {
            rptAlerts_MWUC.DataSource = null;
            rptAlerts_MWUC.DataBind();

            rptAlerts_Fup.DataSource = null;
            rptAlerts_Fup.DataBind();

            rptAlerts_Motor.DataSource = null;
            rptAlerts_Motor.DataBind();

            rptAlert_Defects.DataSource = null;
            rptAlert_Defects.DataBind();

            rptAlerts_DT.DataSource = null;
            rptAlerts_DT.DataBind();

            rptAlerts_6.DataSource = null;
            rptAlerts_6.DataBind();

            rptAlerts_PVI.DataSource = null;
            rptAlerts_PVI.DataBind();

            rptAlerts_VI.DataSource = null;
            rptAlerts_VI.DataBind();

            rptAlerts_LR.DataSource = null;
            rptAlerts_LR.DataBind();

            rptAlerts_15.DataSource = null;
            rptAlerts_15.DataBind();

            rptAlerts_16.DataSource = null;
            rptAlerts_16.DataBind();

            lblRecCount.Text = "No Record found.";
        }

    }
    public void LoadCompanyData()
    {
        string SQL = "";

        string Where = "";

        if (ddlFleet.SelectedIndex != 0)
        {
            Where = Where + " AND V.FLEETID = " + ddlFleet.SelectedValue;
        }
        if (ddlVessel.SelectedIndex != 0)
        {
            Where = Where + " AND V.VESSELCODE ='" + ddlVessel.SelectedValue + "' ";
        }

        if (AT == 1)
        {
            SQL = "SELECT VESSELNAME,T.REFNUMBER, T.DATE_FIELD " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  " + Where +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() ORDER By VESSELNAME";
        }
        if (AT == 2)
        {
            SQL = "SELECT VESSELNAME,T.REFNUMBER , T.DATE_FIELD, " +
                  "CASE T.Key1  " +
                  "WHEN 1 THEN (SELECT Deficiency FROM DBO.t_Observations WHERE Id = T.PK )  " +
                  "WHEN 2 THEN (SELECT Deficiency FROM DBO.t_Observationsnew WHERE TableId = T.PK ) " +
                  "WHEN 3 THEN (SELECT FP_Deficiency FROM DBO.FR_FollowUpList WHERE FP_Id = T.PK )  " +
                  "ELSE '' END AS FollowupText " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  " + Where +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() ORDER By VESSELNAME";
        }
        if (AT == 3)
        {
            SQL = "SELECT VESSELNAME,T.REFNUMBER,(SELECT Convert(varchar, DateADD(M,1,  convert(datetime, cast(RMonth AS varchar) + '/' + '05' + '/' + cast(RYear as varchar), 101)), 106) FROM DBO.MORTORMASTER WHERE MID = T.PK)  AS 'Due Date' " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  " + Where +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() ORDER By VESSELNAME";
        }
        if (AT == 4)
        {
            SQL = "SELECT VESSELNAME,T.REFNUMBER,(SELECT DEFECTDetails FROM [dbo].[VSL_DefectDetailsMaster] WHERE DefectNo = T.PK)  AS Defects, Date_Field " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  " + Where +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() ORDER By VESSELNAME";
        }
        if (AT == 5)
        {
            SQL = "SELECT VESSELNAME,substring(T.PK,1,1) AS TYPE, " +
                  "(SELECT DRILLNAME FROM  " +
                  "(  " +
                  "SELECT 'D'+CONVERT(vARCHAR,DRILLID) AS PK,DRILLNAME FROM DBO.DT_DRILLMASTER WHERE RECORDTYPE='D'  " +
                  "UNION  " +
                  "SELECT 'T'+CONVERT(vARCHAR,TRAININGID) AS PK,TraininingName FROM DBO.DT_TRAININGMASTER WHERE RECORDTYPE='T'  " +
                  ") K WHERE K.PK=T.PK) AS DRILLNAME,  " +
                  "T.Date_Field " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  " + Where +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() ORDER By VESSELNAME";
        }
        if (AT == 6)
        {
            string MYVsls = "SELECT VESSELID,(SELECT AlertDays FROM SHIPSOFT_ADMIN.DBO.TBL_ALERTTYPES WHERE AlertTypeId=6) AS NUMDAYS FROM DBO.VESSEL V WHERE V.VESSELSTATUSID=1  ";
            int NumDays = 0;
            string MYVsls_List = "";
            DataTable dtVsls = Common.Execute_Procedures_Select_ByQuery(MYVsls);
            foreach (DataRow dr in dtVsls.Rows)
            {
                NumDays = Common.CastAsInt32(dr["NUMDAYS"]);
                MYVsls_List += "," + dr["VESSELID"].ToString();
            }
            if (MYVsls_List.StartsWith(","))
                MYVsls_List = MYVsls_List.Substring(1);

            SQL = "exec dbo.VettingPlannerReport '" + MYVsls_List + "'," + NumDays.ToString() + ",9999";
            
        }
        if (AT == 8 || AT == 9 || AT == 10 || AT == 11 || AT == 12 || AT == 13)
        {
            SQL = "SELECT VESSELNAME,T.Date_Field, T.REFNUMBER " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  " + Where +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() ORDER By VESSELNAME";
        }
        if (AT == 7)
        {
            SQL = "SELECT VESSELNAME,T.Date_Field, T.REFNUMBER,(SELECT PlanRemark FROM DBO.t_inspectiondue WHERE Id =T.PK) AS PlanRemark, (SELECT FIRSTNAME+ ' ' + LASTNAME FROM DBO.USERMASTER WHERE LOGINID IN (select TOP 1 SUPERINTENDENTID from dbo.t_InspSupt where inspectiondueid=T.PK AND ATTENDING=1)) As SupName  " +
                  "FROM VW_ALERTS T " +
                  "INNER JOIN TBL_ALERTTYPES M ON T.ALERTTYPEID=M.AlertTypeId " +
                  "INNER JOIN DBO.VESSEL V ON V.VESSELID=T.VESSELID AND V.VESSELSTATUSID=1  " + Where +
                  "WHERE M.AlertTypeId = " + AT + " AND DATEADD(DAY,(CASE WHEN AlertMode='Before' THEN -AlertDays ELSE AlertDays END),T.DATE_FIELD) <= getdate() ORDER By VESSELNAME";
        }
        if (AT == 14)
        {
            dv_Fleet.Visible = false;
            dv_Vessel.Visible = false;
            dv_Office.Visible = true;

            string WhereCond = "";

            if (ddlOffice.SelectedIndex != 0)
            {
                WhereCond = " WHERE OfficeId = " + ddlOffice.SelectedValue;
            }

            SQL = "SELECT * FROM ( SELECT (SELECT [EmpCode] FROM [dbo].[Hr_PersonalDetails] WHERE EmpId = L.[EmpId]) AS EmpCode, (SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [dbo].[Hr_PersonalDetails] WHERE EmpId = L.[EmpId]) AS EmployeeName, (SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [dbo].[Hr_PersonalDetails] WHERE EmpId = L.ForwardedTo) AS ForwardedTo, (SELECT Office FROM [dbo].[Hr_PersonalDetails] WHERE EmpId = L.[EmpId]) AS OfficeId, LeaveFrom, LeaveTo, Reason, RequestDate As ForwardedOn,(SELECT [OfficeName] FROM [dbo].[Office] WHERE [OfficeId] = (SELECT Office FROM  [dbo].[Hr_PersonalDetails] WHERE EmpId = L.[EmpId])) AS Office FROM [dbo].[HR_LeaveRequest] L INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =14 WHERE STATUS='V' AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),LeaveFrom) <= getdate() ) A " + WhereCond + " ORDER BY EmployeeName";
        }
        if (AT == 15)
        {
            dv_Fleet.Visible = false;
            dv_Vessel.Visible = false;

            SQL = "SELECT S.*,dateadd(day,7,sendon) as NextDate FROM [dbo].[tbl_Publish_InformSuptd] S  " +
                  "INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =15  " +
                  "WHERE S.YEAR=YEAR(GETDATE()) AND MONTH=MONTH(GETDATE())-1 AND NOT EXISTS(SELECT CommCo FROM  dbo.tblbudgetlevelcomments C WHERE C.CommCo=S.Cocode AND C.CommYear=S.year AND C.CommPer=S.Month AND CommMajID=6 AND ISNULL(convert(varchar,COMMENT),'')<>'' )    " +
                  "AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),S.sendon) <= getdate() ";
        }
        if (AT == 16)
        {
            dv_Fleet.Visible = false;
            dv_Vessel.Visible = false;

            SQL = "SELECT S.* FROM [dbo].[tbl_Publish_InformSuptd] S  " +
                  "INNER JOIN [dbo].[tbl_AlertTypes] A ON A.[AlertTypeId] =16  " +
                "WHERE S.YEAR=YEAR(GETDATE()) AND MONTH=MONTH(GETDATE())-1  " +
                "AND EXISTS(SELECT CommCo FROM  dbo.tblbudgetlevelcomments C WHERE C.CommCo=S.Cocode AND C.CommYear=S.year AND C.CommPer=S.Month AND CommMajID=6 AND ISNULL(convert(varchar,COMMENT),'')<>'' )  " +
                "AND not EXISTS( SELECT Cocode FROM dbo.tbl_PublishComments R WHERE R.cocode=S.Cocode AND R.year=S.year AND R.month=S.Month )" +
                "AND DATEADD(DAY,(CASE WHEN A.AlertMode='Before' THEN -A.AlertDays ELSE A.AlertDays END),S.sendon) <= getdate() ";
        }


        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            if (AT == 1)
            {
                dv_MWUC.Visible = true;

                rptAlerts_MWUC.DataSource = dt;
                rptAlerts_MWUC.DataBind();
            }
            if (AT == 2)
            {
                dv_Followup.Visible = true;

                rptAlerts_Fup.DataSource = dt;
                rptAlerts_Fup.DataBind();
            }
            if (AT == 3)
            {
                dv_Motor.Visible = true;

                rptAlerts_Motor.DataSource = dt;
                rptAlerts_Motor.DataBind();
            }
            if (AT == 4)
            {
                dv_Defects.Visible = true;

                rptAlert_Defects.DataSource = dt;
                rptAlert_Defects.DataBind();
            }
            if (AT == 5)
            {
                dv_DrillTraining.Visible = true;

                rptAlerts_DT.DataSource = dt;
                rptAlerts_DT.DataBind();
            }
            if (AT == 6)
            {
                dv_6.Visible = true;

                DataView dv = dt.DefaultView;
                dv.RowFilter = "( LastDone is not NULL) and sire_cdi<>'CDI' and (nextinspstatus in ('Executed') or nextinspstatus is null)";
                DataTable dtOut = dv.ToTable();

                rptAlerts_6.DataSource = dtOut;
                rptAlerts_6.DataBind();

                if (dtOut.Rows.Count > 0)
                    lblRecCount.Text = dtOut.Rows.Count.ToString() + " Records found.";
                else
                    lblRecCount.Text = "No Record found.";
            }
            if (AT == 8 || AT == 9 || AT == 10 || AT == 11 || AT == 12 || AT == 13)
            {
                dv_VI.Visible = true;

                rptAlerts_VI.DataSource = dt;
                rptAlerts_VI.DataBind();
            }
            if (AT == 7)
            {
                dv_PVI.Visible = true;

                rptAlerts_PVI.DataSource = dt;
                rptAlerts_PVI.DataBind();
            }
            if (AT == 14)
            {
                dv_Emtm_LR.Visible = true;

                rptAlerts_LR.DataSource = dt;
                rptAlerts_LR.DataBind();
            }
            if (AT == 15)
            {
                dv_15.Visible = true;

                rptAlerts_15.DataSource = dt;
                rptAlerts_15.DataBind();
            }
            if (AT == 16)
            {
                dv_16.Visible = true;

                rptAlerts_16.DataSource = dt;
                rptAlerts_16.DataBind();
            }
            if (AT != 6)
            {
                lblRecCount.Text = dt.Rows.Count.ToString() + " Records found.";
            }
        }
        else
        {
            rptAlerts_MWUC.DataSource = null;
            rptAlerts_MWUC.DataBind();

            rptAlerts_Fup.DataSource = null;
            rptAlerts_Fup.DataBind();

            rptAlerts_Motor.DataSource = null;
            rptAlerts_Motor.DataBind();

            rptAlert_Defects.DataSource = null;
            rptAlert_Defects.DataBind();

            rptAlerts_DT.DataSource = null;
            rptAlerts_DT.DataBind();

            rptAlerts_6.DataSource = null;
            rptAlerts_6.DataBind();

            rptAlerts_PVI.DataSource = null;
            rptAlerts_PVI.DataBind();

            rptAlerts_VI.DataSource = null;
            rptAlerts_VI.DataBind();

            rptAlerts_LR.DataSource = null;
            rptAlerts_LR.DataBind();

            rptAlerts_15.DataSource = null;
            rptAlerts_15.DataBind();

            rptAlerts_16.DataSource = null;
            rptAlerts_16.DataBind();

            lblRecCount.Text = "No Record found.";
        }

    } 
}