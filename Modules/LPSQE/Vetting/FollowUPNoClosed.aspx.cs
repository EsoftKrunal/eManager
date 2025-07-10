using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;

public partial class VIMS_FollowUPNoClosed : System.Web.UI.Page
{
    public string SelVessel
    {
        get { return ViewState["SelVessel"].ToString(); }
        set { ViewState["SelVessel"]=value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!Page.IsPostBack)
        {
            SelVessel = "";
            BindFleet();
            BindVessels();
        }
    }
    protected void lnk_SelectVessel(object sender, EventArgs e)
    {
        SelVessel = ((LinkButton)sender).CommandArgument;
        BindVessels();
        ShowDetails();
    }
    public void BindVessels()
    {
        string VesselIds = "";
        DataTable dtv;
        if (ddlFleet.SelectedIndex <= 0)
            dtv = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID FROM DBO.VESSEL WHERE  VESSELSTATUSID=1");
        else
            dtv = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID FROM DBO.VESSEL WHERE  VESSELSTATUSID=1 AND FLEETID=" + ddlFleet.SelectedValue);

        foreach (DataRow dr in dtv.Rows)
        {
            VesselIds += "," + dr["vesselid"].ToString();
        }
        if (VesselIds.StartsWith(","))
            VesselIds = VesselIds.Substring(1);

        //---------------------------------------------

        string sql ="SELECT VESSELID,VESSELNAME,COUNT(*) AS NOR FROM " +
                    "( " +
                    "SELECT TID.ID AS INSPDUEID,TOB.TABLEID AS OBSVID,'' AS FOLLOWUPITEM,'OBN' AS TBLNAME,TID.VESSELID,(SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=TID.VESSELID) AS VESSELNAME, " +
                    "TOB.DEFICIENCY AS DEFICIENCY,TID.INSPECTIONNO AS SOURCE,TOB.TCLDate AS TGCLOSEDT,REPLACE(CONVERT(VARCHAR,TOB.TCLDate,106),' ','-') AS TARGETCLOSEDATE,TOB.RESPONSIBILITY AS RESPONSIBILITY,REPLACE(CONVERT(VARCHAR,TOB.ClosedOn,106),' ','-') AS COMPLETIONDATE,isnull(TOB.Closure,0) AS CLOSED,(SUBSTRING(TID.INSPECTIONNO,0,CHARINDEX('/',TID.INSPECTIONNO))) AS AA,(REVERSE(SUBSTRING(REVERSE(TID.INSPECTIONNO),0,CHARINDEX('/',REVERSE(TID.INSPECTIONNO))))) AS BB,'' AS CRITICAL " +
                    "FROM T_INSPECTIONDUE TID INNER JOIN t_observationsnew TOB ON TOB.CLOSURE IS NULL AND TOB.INSPDUE_ID=TID.ID WHERE INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE FOLLOWUPCATEGORY IN (4)) " +
                    "UNION " +
                    "SELECT TID.ID AS INSPDUEID,TOB.ID AS OBSVID,TOB.FOLLOWUPITEM AS FOLLOWUPITEM,'OLD' AS TBLNAME,TID.VESSELID, (SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=TID.VESSELID) AS VESSELNAME,TOB.DEFICIENCY AS DEFICIENCY,TID.INSPECTIONNO AS SOURCE,TOB.TARGETCLOSEDT AS TGCLOSEDT,REPLACE(CONVERT(VARCHAR,TOB.TARGETCLOSEDT,106),' ','-') AS TARGETCLOSEDATE,TOB.RESPONSIBILTY AS RESPONSIBILITY,REPLACE(CONVERT(VARCHAR,TOB.CLOSEDDATE,106),' ','-') AS COMPLETIONDATE,isnull(TOB.CLOSED,0) as CLOSED,(SUBSTRING(TID.INSPECTIONNO,0,CHARINDEX('/',TID.INSPECTIONNO))) AS AA,(REVERSE(SUBSTRING(REVERSE(TID.INSPECTIONNO),0,CHARINDEX('/',REVERSE(TID.INSPECTIONNO))))) AS BB,CRITICAL=CASE WHEN TOB.HIGHRISK=1 THEN 'Y' WHEN TOB.HIGHRISK=0 THEN 'N' ELSE '' END " +
                    "FROM T_INSPECTIONDUE TID INNER JOIN T_OBSERVATIONS TOB ON ISNULL(CLOSED,'FALSE')='FALSE' AND TOB.INSPECTIONDUEID=TID.ID WHERE ISNULL(TOB.DEFICIENCY,'')<>'' AND (ISNULL(TOB.FOLLOWUPITEM,'N')='Y' OR TID.INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE INSPECTIONGROUP=(SELECT ID FROM M_INSPECTIONGROUP WHERE INSPECTIONTYPE='INTERNAL'))) AND INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE FOLLOWUPCATEGORY IN (4))   " +
                    "UNION " +
                    "SELECT FL.FP_Id AS INSPDUEID,'0' AS OBSVID,FL.FP_FOLLOWUP AS FOLLOWUPITEM,'NEW' AS TBLNAME,FL.FP_VesselId AS VESSELID,(SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=FL.FP_VesselId) AS VESSELNAME,FL.FP_Deficiency AS DEFICIENCY,FL.FP_InspNo AS SOURCE,FL.FP_TargetClosedDate AS TGCLOSEDT,REPLACE(CONVERT(VARCHAR,FL.FP_TargetClosedDate,106),' ','-') AS TARGETCLOSEDATE,FL.FP_Responsibility AS RESPONSIBILITY,REPLACE(CONVERT(VARCHAR,FL.FP_ClosedDate,106),' ','-') AS COMPLETIONDATE,FP_Closed AS CLOSED,(SUBSTRING(FL.FP_InspNo,0,CHARINDEX('/',FL.FP_InspNo))) AS AA,(REVERSE(SUBSTRING(REVERSE(FL.FP_InspNo),0,CHARINDEX('/',REVERSE(FL.FP_InspNo))))) AS BB,FP_Critical AS Critical  " +
                    "FROM FR_FollowUpList FL WHERE ISNULL(FP_Closed,'FALSE')='FALSE' AND ISNULL(FL.FP_Deficiency,'')<>'' AND ISNULL(FL.FP_FOLLOWUP,'N')='Y' AND FP_FollowUpCatId IN (4) " +
                    ") A WHERE VESSELID IN (" + VesselIds + ") GROUP BY VESSELID,VESSELNAME";
      
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        rpt_Vessels.DataSource = dt1;
        rpt_Vessels.DataBind();

      
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
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessels();
    }
    public void ShowDetails()
    {
        string VesselIds = "";
        DataTable dtv;
        if (ddlFleet.SelectedIndex <= 0)
            dtv = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID FROM DBO.VESSEL WHERE  VESSELSTATUSID=1");
        else
            dtv = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELID FROM DBO.VESSEL WHERE  VESSELSTATUSID=1 AND FLEETID=" + ddlFleet.SelectedValue);

        foreach (DataRow dr in dtv.Rows)
        {
            VesselIds += "," + dr["vesselid"].ToString();
        }
        if (VesselIds.StartsWith(","))
            VesselIds = VesselIds.Substring(1);

        string sqlDetails = "SELECT ROW_NUMBER() OVER(PARTITION BY VESSELID ORDER BY A.AA,A.BB) AS SNO,* FROM " +
                "( " +
                "SELECT TID.ID AS INSPDUEID,TOB.TABLEID AS OBSVID,'' AS FOLLOWUPITEM,'OBN' AS TBLNAME,TID.VESSELID,(SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=TID.VESSELID) AS VESSELNAME, " +
                "TOB.DEFICIENCY AS DEFICIENCY,TID.INSPECTIONNO AS SOURCE,TOB.TCLDate AS TGCLOSEDT,REPLACE(CONVERT(VARCHAR,TOB.TCLDate,106),' ','-') AS TARGETCLOSEDATE,TOB.RESPONSIBILITY AS RESPONSIBILITY,REPLACE(CONVERT(VARCHAR,TOB.ClosedOn,106),' ','-') AS COMPLETIONDATE,isnull(TOB.Closure,0) AS CLOSED,(SUBSTRING(TID.INSPECTIONNO,0,CHARINDEX('/',TID.INSPECTIONNO))) AS AA,(REVERSE(SUBSTRING(REVERSE(TID.INSPECTIONNO),0,CHARINDEX('/',REVERSE(TID.INSPECTIONNO))))) AS BB,'' AS CRITICAL " +
                "FROM T_INSPECTIONDUE TID INNER JOIN t_observationsnew TOB ON TOB.CLOSURE IS NULL AND TOB.INSPDUE_ID=TID.ID WHERE INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE FOLLOWUPCATEGORY IN (4)) " +
                "UNION " +
                "SELECT TID.ID AS INSPDUEID,TOB.ID AS OBSVID,TOB.FOLLOWUPITEM AS FOLLOWUPITEM,'OLD' AS TBLNAME,TID.VESSELID, (SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=TID.VESSELID) AS VESSELNAME,TOB.DEFICIENCY AS DEFICIENCY,TID.INSPECTIONNO AS SOURCE,TOB.TARGETCLOSEDT AS TGCLOSEDT,REPLACE(CONVERT(VARCHAR,TOB.TARGETCLOSEDT,106),' ','-') AS TARGETCLOSEDATE,TOB.RESPONSIBILTY AS RESPONSIBILITY,REPLACE(CONVERT(VARCHAR,TOB.CLOSEDDATE,106),' ','-') AS COMPLETIONDATE,isnull(TOB.CLOSED,0) as CLOSED,(SUBSTRING(TID.INSPECTIONNO,0,CHARINDEX('/',TID.INSPECTIONNO))) AS AA,(REVERSE(SUBSTRING(REVERSE(TID.INSPECTIONNO),0,CHARINDEX('/',REVERSE(TID.INSPECTIONNO))))) AS BB,CRITICAL=CASE WHEN TOB.HIGHRISK=1 THEN 'Y' WHEN TOB.HIGHRISK=0 THEN 'N' ELSE '' END " +
                "FROM T_INSPECTIONDUE TID INNER JOIN T_OBSERVATIONS TOB ON ISNULL(CLOSED,'FALSE')='FALSE' AND TOB.INSPECTIONDUEID=TID.ID WHERE ISNULL(TOB.DEFICIENCY,'')<>'' AND (ISNULL(TOB.FOLLOWUPITEM,'N')='Y' OR TID.INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE INSPECTIONGROUP=(SELECT ID FROM M_INSPECTIONGROUP WHERE INSPECTIONTYPE='INTERNAL'))) AND INSPECTIONID IN (SELECT ID FROM M_INSPECTION WHERE FOLLOWUPCATEGORY IN (4))   " +
                "UNION " +
                "SELECT FL.FP_Id AS INSPDUEID,'0' AS OBSVID,FL.FP_FOLLOWUP AS FOLLOWUPITEM,'NEW' AS TBLNAME,FL.FP_VesselId AS VESSELID,(SELECT VSL.VESSELNAME FROM DBO.VESSEL VSL WHERE VSL.VESSELID=FL.FP_VesselId) AS VESSELNAME,FL.FP_Deficiency AS DEFICIENCY,FL.FP_InspNo AS SOURCE,FL.FP_TargetClosedDate AS TGCLOSEDT,REPLACE(CONVERT(VARCHAR,FL.FP_TargetClosedDate,106),' ','-') AS TARGETCLOSEDATE,FL.FP_Responsibility AS RESPONSIBILITY,REPLACE(CONVERT(VARCHAR,FL.FP_ClosedDate,106),' ','-') AS COMPLETIONDATE,FP_Closed AS CLOSED,(SUBSTRING(FL.FP_InspNo,0,CHARINDEX('/',FL.FP_InspNo))) AS AA,(REVERSE(SUBSTRING(REVERSE(FL.FP_InspNo),0,CHARINDEX('/',REVERSE(FL.FP_InspNo))))) AS BB,FP_Critical AS Critical  " +
                "FROM FR_FollowUpList FL WHERE ISNULL(FP_Closed,'FALSE')='FALSE' AND ISNULL(FL.FP_Deficiency,'')<>'' AND ISNULL(FL.FP_FOLLOWUP,'N')='Y' AND FP_FollowUpCatId IN (4) " +
                ") A WHERE VESSELID IN (" + VesselIds + ") ORDER BY A.AA,A.BB";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sqlDetails);
        DataView dv = dt.DefaultView;
        dv.RowFilter = "VesselId=" + SelVessel;
        rpt_Defs.DataSource = dv;
        rpt_Defs.DataBind();
    }
}