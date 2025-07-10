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
using System.IO;

public partial class eReports_G118_ER_G118_Report : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public int ReportId
    {
        get { return Common.CastAsInt32(ViewState["ReportId"]); }
        set { ViewState["ReportId"] = value; }
    }
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        ReportId = Common.CastAsInt32(Request.QueryString["RId"].ToString());
        VesselCode = Request.QueryString["VC"].ToString();
        ShowReport();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {

        rpt.Close();
        rpt.Dispose();
    }
    protected void ShowReport()
    {
        string SQLReport = "SELECT * FROM [dbo].[vw_Accident_PrintReport] WHERE [VesselCode]='" + VesselCode + "' and VesselCode in (Select v.VesselCode from UserVesselRelation vw with(nolock) inner join Vessel v on vw.VesselId = v.VesselId where vw.Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") AND [ReportId]=" + ReportId;
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQLReport);
        
        DataSet ds = new DataSet();
        dt1.TableName = "vw_Accident_PrintReport";
        
        ds.Tables.Add(dt1.Copy());
        

        //string MTC = "";
        //string LTI = "";
        //string TRC = "";

        //string SQLParam = "SELECT MTC, LTI, (MTC + LTI + RWC) AS TRC FROM (SELECT " +
        //             "(SELECT Count([OCI_MI_Reporting]) FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode] = '" + VesselCode + "' AND ISNULL(Status, 'A') = 'A' AND [ReportId] = " + ReportId.ToString() + " AND [OCI_MI_Reporting] IN (24,26,28)) AS MTC, " +
        //             "(SELECT Count([OCI_MI_Reporting]) FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode] = '" + VesselCode + "' AND ISNULL(Status, 'A') = 'A' AND [ReportId] = " + ReportId.ToString() + " AND [OCI_MI_Reporting] IN (25,27,29,26)) AS LTI, " +
        //             "(SELECT Count([OCI_MI_Reporting]) FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode] = '" + VesselCode + "' AND ISNULL(Status, 'A') = 'A' AND [ReportId] = " + ReportId.ToString() + " AND [OCI_MI_Reporting] IN (28)) AS RWC " +
        //             ")a ";
        //DataTable dtParam = Common.Execute_Procedures_Select_ByQuery(SQLParam);

        //if (dtParam.Rows.Count > 0)
        //{
        //    MTC = dtParam.Rows[0]["MTC"].ToString();
        //    LTI = dtParam.Rows[0]["LTI"].ToString();
        //    TRC = dtParam.Rows[0]["TRC"].ToString();
        //}

        //CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("NCRReportNew.rpt"));
        rpt.SetDataSource(ds);
        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/LPSQE/IncidentReport/NCR/NCRReport.pdf"));
        byte[] fileBytes = File.ReadAllBytes(Server.MapPath("~/Modules/LPSQE/IncidentReport/NCR/NCRReport.pdf"));
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment; filename=NCRReport.pdf");
        Response.AddHeader("Content-Length", fileBytes.Length.ToString());
        Response.BinaryWrite(fileBytes);
        Response.End();
        //rpt.SetParameterValue("MTC", MTC);
        //rpt.SetParameterValue("LTI", LTI);
        //rpt.SetParameterValue("TRC", TRC);

        //rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("NCRReport.pdf"));
        ////Response.Clear();
        //Response.WriteFile(Server.MapPath("NCRReport.pdf"));
        //Response.End();

        //---------------------------------------------------------------------------------------------------------------

        //  string sql = "SELECT [NCRID] " +
        //" ,[NCRNO] " +
        //" ,[VesselID] " +

        //" ,(select VesselName from dbo.vessel V where V.VesselCode=NCR.VesselCode)FullVesselName" +
        //" ,[VesselCode] " +
        //" ,[AreaAudited] " +

        //" ,(case when AuditValueOther !='' then AuditValueOther else (case when AuditType='ext' then (select extvalue from tbl_External E where E.EXTID=NCR.Auditvalue) else " +
        //"    (select intvalue from tbl_Internal I where  I.INTID=NCR.Auditvalue) end)end)AuditedText" +

        //" ,(case when AuditType='ext' then 'External' else 'Internal' end )AuditedlableText " +

        //" ,[AuditValueOther] " +
        //" ,[NonConformanceDes] " +
        //" ,[NCRMaker] " +
        //" ,replace( convert(varchar,[NCRCreatedDate],106),' ','-')NCRCreatedDate " +
        //" ,[NCRMakerPos] " +
        //" ,[PersonRecvNCR] " +
        //" ,[RankRecvNCR] " +
        //" ,[NameofGeneralManager] " +
        //" ,replace( convert(varchar,[NCRTargetCompDate],106),' ','-')NCRTargetCompDate  " +
        //" ,[ImmediateCorrectiveAction] " +
        //" ,[IMCName] " +
        //" ,replace( convert(varchar,[IMCCompletedDate],106),' ','-')IMCCompletedDate  " +
        //" ,[IMCRanck] " +
        //" ,[IncorrectPurchase] " +
        //" ,[InadequateDesignMaterial] " +
        //" ,[IneffictiveTraining] " +
        //" ,[InadequateLeadershipSupervision] " +
        //" ,[LackOfExperiance] " +
        //" ,[InadequateMaintenace] " +
        //" ,[LackOfKnowledgeSkills] " +
        //" ,[InadequateManagementOfChange] " +
        //" ,[LackOfMotivation] " +
        //" ,[InadequateSelectionRecuritment] " +
        //" ,[LackOfRestPeriod] " +
        //" ,[InadequateToolsEquipment] " +
        //" ,[PhysicalCapabilityHealth] " +
        //" ,[InadequateLackOfProcedure] " +
        //" ,[RCOther] " +
        //" ,[OhterRootCause] " +
        //" ,[PreventivAction] " +
        //" ,[PAName] " +
        //" ,[PADate] " +
        //" ,[PAPos] " +
        //" ,[OfficeComment] " +
        //" ,[OfficeCommentVarefiedBy] " +
        //" ,replace( convert(varchar,[OfficeCommentVarefiedDate],106),' ','-')OfficeCommentVarefiedDate " +
        //" ,[VerifiedPersonPOS] " +
        //" ,replace( convert(varchar,[ClosureDate],106),' ','-')ClosureDate  " +
        //" ,[ClosureRemarks] " +
        //" ,[ClosureEvidence] " +
        //" ,[ClosedBy] " +
        //" ,[ClosedOn] " +
        //" ,[NCRStatus] " +
        //" FROM [dbo].[tbl_NCR] NCR where NCRID=" + NCRId + " ";

        //  DataSet DS = Budget.getTable(sql);
        //  DataTable dt = DS.Tables[0];
        //  //string VesselName= "",AreaAudited= "",NCRNumber= "",External= "",Internal= "",NCRDetails= "",NCRMaker= "",NCRMakerPosition= "",NCRMakingDate= "",NCRReciever= "";
        //  //string NCRrecieverPosition= "",NCRRecievingDate= "",MasterName= "",MasterCrewNo= "",MasterDate= "",TargetDate= "",CorrectiveActonCompletedOn= "",CorrectiveActionName= "",CorrectiveActionrank= "",IncorrectPurchase= "";
        //  //string InAdequateDesign= "",IneffectiveTraining= "",InadequateLeadership= "",LackofExperience= "",InadequateMaintenance = "",LackofKnowledge= "",InadequateManagementofchange= "",LackofMotivation= "",InadequateSelection= "",LackofRestPeriod= "";
        //  //string InadequateTools = "",PhysicalCapability= "",LackofProcedure= "",OtherRootCause= "",PreventiveAction= "",ImmediatCorrectiveAction= "";


        //  if (dt.Rows.Count > 0)
        //  {
        //      DataRow Dr = dt.Rows[0];

        //      this.CrystalReportViewer1.Visible = true;
        //      CrystalReportViewer1.ReportSource = rpt;
        //      //CrystalReportViewer1.ReportSource = rpt;
        //      //rpt.Load(Server.MapPath("~/Reports/NCRReportNew.rpt"));
        //      //NCRReportNewForTesting

        //      rpt.Load(Server.MapPath("~/Reports/NCRReportNewParrent.rpt"));




        //      //rpt.SetDataSource(dt);
        //      rpt.SetParameterValue("VesselName", Dr["FullVesselName"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("AreaAudited", Dr["AreaAudited"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("NCRNumber", Dr["NCRNO"].ToString().ToUpper(), "NCRReportNew.rpt");

        //      rpt.SetParameterValue("AuditedlableText", Dr["AuditedlableText"].ToString() + "  :", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("ExternalOrInternal", Dr["AuditedText"].ToString().ToUpper(), "NCRReportNew.rpt");


        //      rpt.SetParameterValue("NCRDetails", Dr["NonConformanceDes"].ToString().ToUpper(), "NCRReportNew.rpt");

        //      rpt.SetParameterValue("NCRMaker", Dr["NCRMaker"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("NCRMakerPosition", Dr["NCRMakerPos"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("NCRMakingDate", Dr["NCRCreatedDate"].ToString().ToUpper(), "NCRReportNew.rpt");

        //      rpt.SetParameterValue("NCRReciever", Dr["PersonRecvNCR"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("NCRrecieverPosition", Dr["RankRecvNCR"].ToString().ToUpper(), "NCRReportNew.rpt");

        //      rpt.SetParameterValue("NCRRecievingDate", "------------------", "NCRReportNew.rpt");//@@@@@@@@@@@@

        //      rpt.SetParameterValue("MasterName", Dr["NameofGeneralManager"].ToString().ToUpper(), "NCRReportNew.rpt");

        //      rpt.SetParameterValue("MasterCrewNo", "--------------------", "NCRReportNew.rpt"); //Have to delete 
        //      rpt.SetParameterValue("MasterDate", "----------------------", "NCRReportNew.rpt");//Have to delete 

        //      rpt.SetParameterValue("TargetDate", Dr["NCRTargetCompDate"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("CorrectiveActonCompletedOn", Dr["IMCCompletedDate"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("CorrectiveActionName", Dr["IMCName"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("CorrectiveActionrank", Dr["IMCRanck"].ToString().ToUpper(), "NCRReportNew.rpt");

        //      rpt.SetParameterValue("IncorrectPurchase", (Dr["IncorrectPurchase"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("InAdequateDesign", (Dr["InadequateDesignMaterial"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("IneffectiveTraining", (Dr["IneffictiveTraining"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("InadequateLeadership", (Dr["InadequateLeadershipSupervision"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("LackofExperience", (Dr["LackOfExperiance"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("InadequateMaintenance ", (Dr["InadequateMaintenace"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");//@@@@@@@@
        //      rpt.SetParameterValue("LackofKnowledge", (Dr["LackOfKnowledgeSkills"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("InadequateManagementofchange", (Dr["InadequateManagementOfChange"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("LackofMotivation", (Dr["LackOfMotivation"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("InadequateSelection", (Dr["InadequateSelectionRecuritment"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("LackofRestPeriod", (Dr["LackOfRestPeriod"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("InadequateTools ", (Dr["InadequateToolsEquipment"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt"); //@@@@@@
        //      rpt.SetParameterValue("PhysicalCapability", (Dr["PhysicalCapabilityHealth"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("LackofProcedure", (Dr["InadequateLackOfProcedure"].ToString() == "True") ? "√" : "", "NCRReportNew.rpt");
        //      rpt.SetParameterValue("OtherRootCause", Dr["OhterRootCause"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      if (Dr["OhterRootCause"].ToString().ToUpper().Trim() != "")
        //      {
        //          rpt.SetParameterValue("OtherCheck", "√", "NCRReportNew.rpt");
        //      }
        //      else
        //      {
        //          rpt.SetParameterValue("OtherCheck", "", "NCRReportNew.rpt");
        //      }

        //      rpt.SetParameterValue("PreventiveAction", Dr["PreventivAction"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("ImmediatCorrectiveAction", Dr["ImmediateCorrectiveAction"].ToString().ToUpper(), "NCRReportNew.rpt");

        //      // Office comments
        //      rpt.SetParameterValue("VerifiedBy", Dr["OfficeCommentVarefiedBy"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("VerifiedRank", Dr["VerifiedPersonPOS"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("VerifiedDate", Dr["OfficeCommentVarefiedDate"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("VerifiedComments", Dr["OfficeComment"].ToString().ToUpper(), "NCRReportNew.rpt");


        //      // Closure comments
        //      rpt.SetParameterValue("ClosurName", Dr["ClosedBy"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("ClosurDate", Dr["ClosureDate"].ToString().ToUpper(), "NCRReportNew.rpt");
        //      rpt.SetParameterValue("ClosurRemarks", (Dr["ClosureRemarks"].ToString() == "") ? "NILL" : Dr["ClosureRemarks"].ToString().ToUpper(), "NCRReportNew.rpt");

        //  }
        //  else
        //  {
        //      //lblmessage.Text = "No Record Found.";
        //      //this.CrystalReportViewer1.Visible = false;
        //  }
    }
}