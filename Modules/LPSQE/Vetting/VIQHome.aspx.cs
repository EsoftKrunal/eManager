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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class Vetting_VIQHome : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;

        }
        if (!Page.IsPostBack)
        {
            //----------------
            string VesselList = getVessels();
            DataTable dtDue = Common.Execute_Procedures_Select_ByQuery("exec dbo.VettingPlannerReport '" + VesselList + "',60,9999");
            DataView dv=dtDue.DefaultView;
            dv.RowFilter = "( LastDone is not NULL) AND ( nextinspstatus in ('Planned','Executed') OR nextinspstatus IS NULL)";
            lbl_InsDue.Text = dv.ToTable().Rows.Count.ToString();
            //----------------
            DataTable dtPlanned = Common.Execute_Procedures_Select_ByQuery("exec dbo.VettingPlannerReport '" + VesselList + "',9999,30");
            DataView dv1 = dtDue.DefaultView;
            dv1.RowFilter = "( LastDone is not NULL) and nextinspstatus in ('Planned','Executed')";
            lbl_InsPlanned.Text = dv1.ToTable().Rows.Count.ToString();
            //----------------
            DataTable dtRespUpload = Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(*) FROM T_INSPECTIONDUE WHERE STATUS IN ('Observation','Response') AND ID IN (SELECT DISTINCT INSPECTIONDUEID FROM t_observations WHERE ISNULL(RESPONSEUPLOADED,'N')='N') AND INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE INSPECTIONGROUP IN (1,2)) ");
            lbl_InsResp.Text = dtRespUpload.Rows[0][0].ToString();
            //----------------
            string BONUSsql="SELECT DISTINCT VESSELNAME,T.INSPECTIONNO,T.STATUS,T.INSPECTIONID,T.ActualDate FROM T_INSPECTIONDUE T " +
"INNER JOIN DBO.VESSEL V ON T.VESSELID=V.VESSELID AND v.vesselstatusid=1  " +
"LEFT JOIN [dbo].[InspectionBonusMaster] BM ON T.ID=BM.INSPECTIONID AND BM.APPROVAL='N' WHERE T.INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE INSPECTIONGROUP IN (1,2))  AND T.STATUS NOT IN ('Closed','Planned','Executed') and ACTUALDATE>='01-SEP-2013' " +
"AND T.ID NOT IN  " +
"( " +
"SELECT DISTINCT IM.INSPECTIONID FROM   [dbo].[InspectionBonusMaster] IM WHERE IM.APPROVAL='Y' AND   " +
"IM.INSPECTIONID NOT IN (SELECT DISTINCT IM1.INSPECTIONID FROM   [dbo].[InspectionBonusMaster] IM1 WHERE IM1.APPROVAL='N')  " +
")";
            
            DataTable dtBonus = Common.Execute_Procedures_Select_ByQuery(BONUSsql);
            lbl_InsBonus.Text = dtBonus.Rows.Count.ToString();
            //----------------
            string sql = "SELECT COUNT(*) AS NOR FROM " +
                "( " +
                "SELECT TID.ID AS INSPDUEID,TOB.TABLEID AS OBSVID,'' AS FOLLOWUPITEM,'OBN' AS TBLNAME,TID.VESSELID,(SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=TID.VESSELID) AS VESSELNAME, " +
                "TOB.DEFICIENCY AS DEFICIENCY,TID.INSPECTIONNO AS SOURCE,TOB.TCLDate AS TGCLOSEDT,REPLACE(CONVERT(VARCHAR,TOB.TCLDate,106),' ','-') AS TARGETCLOSEDATE,TOB.RESPONSIBILITY AS RESPONSIBILITY,REPLACE(CONVERT(VARCHAR,TOB.ClosedOn,106),' ','-') AS COMPLETIONDATE,isnull(TOB.Closure,0) AS CLOSED,(SUBSTRING(TID.INSPECTIONNO,0,CHARINDEX('/',TID.INSPECTIONNO))) AS AA,(REVERSE(SUBSTRING(REVERSE(TID.INSPECTIONNO),0,CHARINDEX('/',REVERSE(TID.INSPECTIONNO))))) AS BB,'' AS CRITICAL " +
                "FROM T_INSPECTIONDUE TID INNER JOIN t_observationsnew TOB ON TOB.CLOSURE IS NULL AND TOB.INSPDUE_ID=TID.ID WHERE INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE FOLLOWUPCATEGORY IN (4)) " +
                "UNION " +
                "SELECT TID.ID AS INSPDUEID,TOB.ID AS OBSVID,TOB.FOLLOWUPITEM AS FOLLOWUPITEM,'OLD' AS TBLNAME,TID.VESSELID, " +
                "(SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=TID.VESSELID) AS VESSELNAME,TOB.DEFICIENCY AS DEFICIENCY,TID.INSPECTIONNO AS SOURCE,TOB.TARGETCLOSEDT AS TGCLOSEDT,REPLACE(CONVERT(VARCHAR,TOB.TARGETCLOSEDT,106),' ','-') AS TARGETCLOSEDATE,TOB.RESPONSIBILTY AS RESPONSIBILITY,REPLACE(CONVERT(VARCHAR,TOB.CLOSEDDATE,106),' ','-') AS COMPLETIONDATE,isnull(TOB.CLOSED,0) as CLOSED,(SUBSTRING(TID.INSPECTIONNO,0,CHARINDEX('/',TID.INSPECTIONNO))) AS AA,(REVERSE(SUBSTRING(REVERSE(TID.INSPECTIONNO),0,CHARINDEX('/',REVERSE(TID.INSPECTIONNO))))) AS BB,CRITICAL=CASE WHEN TOB.HIGHRISK=1 THEN 'Y' WHEN TOB.HIGHRISK=0 THEN 'N' ELSE '' END " +
                "FROM T_INSPECTIONDUE TID INNER JOIN T_OBSERVATIONS TOB ON ISNULL(CLOSED,'FALSE')='FALSE' AND TOB.INSPECTIONDUEID=TID.ID WHERE ISNULL(TOB.DEFICIENCY,'')<>'' AND (ISNULL(TOB.FOLLOWUPITEM,'N')='Y' OR TID.INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE INSPECTIONGROUP=(SELECT ID FROM M_INSPECTIONGROUP WHERE INSPECTIONTYPE='INTERNAL'))) AND INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE FOLLOWUPCATEGORY IN (4))   " +
                "UNION " +
                "SELECT FL.FP_Id AS INSPDUEID,'0' AS OBSVID,FL.FP_FOLLOWUP AS FOLLOWUPITEM,'NEW' AS TBLNAME,FL.FP_VesselId AS VESSELID,(SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=FL.FP_VesselId) AS VESSELNAME,FL.FP_Deficiency AS DEFICIENCY,FL.FP_InspNo AS SOURCE,FL.FP_TargetClosedDate AS TGCLOSEDT,REPLACE(CONVERT(VARCHAR,FL.FP_TargetClosedDate,106),' ','-') AS TARGETCLOSEDATE,FL.FP_Responsibility AS RESPONSIBILITY,REPLACE(CONVERT(VARCHAR,FL.FP_ClosedDate,106),' ','-') AS COMPLETIONDATE,FP_Closed AS CLOSED,(SUBSTRING(FL.FP_InspNo,0,CHARINDEX('/',FL.FP_InspNo))) AS AA,(REVERSE(SUBSTRING(REVERSE(FL.FP_InspNo),0,CHARINDEX('/',REVERSE(FL.FP_InspNo))))) AS BB,FP_Critical AS Critical  " +
                "FROM FR_FollowUpList FL WHERE ISNULL(FP_Closed,'FALSE')='FALSE' AND ISNULL(FL.FP_Deficiency,'')<>'' AND ISNULL(FL.FP_FOLLOWUP,'N')='Y' AND FP_FollowUpCatId IN (4)" +
                ") A WHERE VESSELID IN (SELECT VESSELID FROM DBO.VESSEL WHERE  VESSELSTATUSID=1) ";

            DataTable dtInsNotCleared = Common.Execute_Procedures_Select_ByQuery(sql);
            lbl_InsNotCleared.Text = dtInsNotCleared.Rows[0][0].ToString();
            //----------------
            DataTable dtDone = Common.Execute_Procedures_Select_ByQuery("select count(id) from t_inspectiondue where inspectionid in (select id from m_inspection where inspectiongroup in (1,2)) and Actualdate between dateadd(dd,-30,getdate()) and getdate()");
            lbl_InsDone.Text = dtDone.Rows[0][0].ToString();
            //----------------
            tr_VIQ.Visible = new AuthenticationManager(305, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page).IsView;
            tr_VPR.Visible = new AuthenticationManager(306, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page).IsView;
            tr_VP.Visible = new AuthenticationManager(307, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page).IsView;
            tr_VR.Visible = new AuthenticationManager(309, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page).IsView;
            tr_VA.Visible = new AuthenticationManager(310, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page).IsView;

        }
    }
    public string getVessels()
    {
        string str = "select vesselid from dbo.vessel where vesselstatusid=1 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(str);
        str = "";
        foreach (DataRow dr in dt.Rows)
        {
            str += "," + dr[0].ToString();
        }
        if (str.StartsWith(","))
            str = str.Substring(1);
        return str;
    }
    protected void btnShowDue_Click(object sender, EventArgs e)
    {
        DataTable dtDue = Common.Execute_Procedures_Select_ByQuery("exec dbo.VettingPlannerReport '" + getVessels() + "',60,9999");
        DataView dv = dtDue.DefaultView;
        if (rad_s.Checked)
            dv.RowFilter = "( LastDone is not NULL) and sire_cdi<>'CDI' and ( nextinspstatus in ('Planned','Executed') OR nextinspstatus IS NULL)";
        else
            dv.RowFilter = "( LastDone is not NULL) and sire_cdi='CDI' and (nextinspstatus in ('Planned','Executed') OR nextinspstatus IS NULL)";

        dv.Sort = "nextdue";
        DataTable dt1 = dv.ToTable();
        rpt_Due.DataSource = dt1;
        recCount.Text = dt1.Rows.Count.ToString() + " Records Found.";
        rpt_Due.DataBind();
    }
    protected void btnshowPlanned_Click(object sender, EventArgs e)
    {
        DataTable dtDue = Common.Execute_Procedures_Select_ByQuery("exec dbo.VettingPlannerReport '" + getVessels() + "',9999,30");
        DataView dv = dtDue.DefaultView;
        if (radS5.Checked)
            dv.RowFilter = "( LastDone is not NULL) and sire_cdi<>'CDI' and nextinspstatus in ('Planned','Executed')";
        else
            dv.RowFilter = "( LastDone is not NULL) and sire_cdi='CDI' and nextinspstatus in ('Planned','Executed')";

        dv.Sort = "PLANDATE_DATE";

        DataTable Dt = dv.ToTable();
        rpt_Planned.DataSource = Dt;
        lblPlannedCount.Text = Dt.Rows.Count.ToString() + " Records Found.";
        rpt_Planned.DataBind();
    }

    protected void btnshowResponse_Click(object sender, EventArgs e)
    {
        DataTable dtResp = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELNAME,INSPECTIONNO,STATUS,INSPECTIONID,DATEDIFF(DD,GETDATE(),DATEADD(DD,15,ACTUALDATE)) AS DAYSREM FROM T_INSPECTIONDUE T INNER JOIN DBO.VESSEL V ON T.VESSELID=V.VESSELID WHERE STATUS IN ('Observation','Response') AND ID IN (SELECT DISTINCT INSPECTIONDUEID FROM t_observations WHERE ISNULL(RESPONSEUPLOADED,'N')='N') AND INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE INSPECTIONGROUP IN (1,2)) ");
        DataView dv = dtResp.DefaultView;

        if (rad_S1.Checked)
            dv.RowFilter = " INSPECTIONID<>21 ";
        else
            dv.RowFilter = " INSPECTIONID=21 ";
        dv.Sort = "VESSELNAME,INSPECTIONNO";
        DataTable Dt = dv.ToTable();
        rpt_Resp.DataSource = Dt;
        lblRespCount.Text = Dt.Rows.Count.ToString() + " Records Found.";
        rpt_Resp.DataBind();
    }
    protected void btnshowBonus_Click(object sender, EventArgs e)
    {
		string sql="SELECT DISTINCT VESSELNAME,T.INSPECTIONNO,T.STATUS,T.INSPECTIONID,T.ActualDate FROM T_INSPECTIONDUE T " +
"INNER JOIN DBO.VESSEL V ON T.VESSELID=V.VESSELID AND v.vesselstatusid=1  " +
"LEFT JOIN [dbo].[InspectionBonusMaster] BM ON T.ID=BM.INSPECTIONID AND BM.APPROVAL='N' WHERE T.INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE INSPECTIONGROUP IN (1,2))  AND T.STATUS NOT IN ('Closed','Planned','Executed') and ACTUALDATE>='01-SEP-2013' " +
"AND T.ID NOT IN  " +
"( " +
"SELECT DISTINCT IM.INSPECTIONID FROM   [dbo].[InspectionBonusMaster] IM WHERE IM.APPROVAL='Y' AND   " +
"IM.INSPECTIONID NOT IN (SELECT DISTINCT IM1.INSPECTIONID FROM   [dbo].[InspectionBonusMaster] IM1 WHERE IM1.APPROVAL='N')  " +
")";
			
        DataTable dtResp = Common.Execute_Procedures_Select_ByQuery(sql);
         
        DataView dv = dtResp.DefaultView;

        if (rad_s2.Checked)
            dv.RowFilter = " INSPECTIONID<>21 ";
        else
            dv.RowFilter = " INSPECTIONID=21 ";

        dv.Sort = "VESSELNAME,INSPECTIONNO";

        DataTable Dt = dv.ToTable();
        rpt_Bonus.DataSource = Dt;
        lbl_Bonus_Count.Text = Dt.Rows.Count.ToString() + " Records Found.";
        rpt_Bonus.DataBind();
    }
    protected void btnshowDone_Click(object sender, EventArgs e)
    {
        DataTable dtResp = Common.Execute_Procedures_Select_ByQuery("select VESSELNAME,T.INSPECTIONNO,T.INSPECTIONID,ACTUALDATE FROM T_INSPECTIONDUE T INNER JOIN DBO.VESSEL V ON T.VESSELID=V.VESSELID where inspectionid in (select id from m_inspection where inspectiongroup in (1,2)) and Actualdate between dateadd(dd,-30,getdate()) and getdate()");
        DataView dv = dtResp.DefaultView;

        if (rad_s3.Checked)
            dv.RowFilter = " INSPECTIONID<>21 ";
        else
            dv.RowFilter = " INSPECTIONID=21 ";

        dv.Sort = "VESSELNAME,INSPECTIONNO";

        DataTable Dt = dv.ToTable();
        rpt_Done.DataSource = Dt;
        lblDoneCount.Text = Dt.Rows.Count.ToString() + " Records Found.";
        rpt_Done.DataBind();
    }
}
