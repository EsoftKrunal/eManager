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

public partial class CrewOperation_TrainingCounterDetails : System.Web.UI.Page
{
    public int TrainingMatrixId
    {
        get { return Common.CastAsInt32(ViewState["_TrainingMatrixId"]); }
        set { ViewState["_TrainingMatrixId"] = value; }
    }
    public int VesselID
    {
        get { return Common.CastAsInt32(ViewState["_VesselID"]); }
        set { ViewState["_VesselID"] = value; }
    }
    public int source
    {
        get { return Common.CastAsInt32(ViewState["source"]); }
        set { ViewState["source"] = value; }
    }
    public string Mode
    {
        get { return Convert.ToString(ViewState["_Mode"]); }
        set { ViewState["_Mode"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            if (Page.Request.QueryString["vid"] != null)
                VesselID = Common.CastAsInt32(Page.Request.QueryString["vid"]);
            else
                return;

            if (Page.Request.QueryString["m"] != null)
                Mode = Convert.ToString(Page.Request.QueryString["m"]);
            else
                return;

            if (Page.Request.QueryString["source"] != null)
                source = Common.CastAsInt32(Page.Request.QueryString["source"]);
            else
                return;

            

            if (Mode.ToLower() == "d")
            {
                litPageHeading.Text = "Completed Trainings";
                TrainingsDone();
            }
            if (Mode.ToLower() == "p")
            {
                litPageHeading.Text = "Pending Trainings";
                TrainingsPending();
            }
            if (VesselID > 0)
            {
                string sql = "select  vesselname from DBO.vessel where vesselid=" + VesselID;
                DataTable dtvs = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if(dtvs.Rows.Count>0)
                    litPageHeading.Text += " - " + dtvs.Rows[0]["VesselName"]; 

            }

            switch (source)
            {
                case 1:
                    litPageHeading.Text += " - Assigned"; break;
                case 2:
                    litPageHeading.Text += " - Matrix"; break;
                case 3:
                    litPageHeading.Text += " - Peap"; break;
                default:
                    break;
            }

        }
    }
    
    // Functon --------------------------------------------------------------------------------------------------------
    public void TrainingsDone()
    {
        divVesselList.Visible = true;
        string sourceclause = "";
        if(source==1)
        {
            sourceclause = " and ISNULL(SOURCE, 0) = 1";
        }
        else if (source == 2)
        {
            sourceclause = " and ISNULL(SOURCE, 0) = 2";
        }
        else if (source == 3)
        {
            sourceclause = " and ISNULL(SOURCE, 0) not in(1,2)";
        }
        string sql = "  select ROW_NUMBER()over(order by ct.CREWID,Fromdate)as SNO,TrainingRequirementId,DBO.fn_getSimilerTrainingsName(ct.TRAININGID) as TrainingName,PlannedFor,FromDate  " +
              "   , ToDate,cpd.crewnumber,cpd.firstname + ' ' + cpd.middlename + ' ' + cpd.lastname as crewname , Attended, institutename, CASE WHEN ISNULL(SOURCE, 0) = 1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)= 2 THEN 'MATRIX' ELSE 'PEAP' END as SourceName " +
              "   ,Case when isnull(AttachmentName, '') = '' then 0 else 1 end HasFile " +
              "   from DBO.CrewTrainingRequirement ct " +
              "   inner join CrewPersonalDetails cpd on ct.CrewId = cpd.CrewId and cpd.CrewStatusId = 3 and ct.StatusId = 'A' " +
              "   left join DBO.traininginstitute i on ct.TrainingPlanningId = i.instituteid " +
              "   where ct.StatusId = 'A' AND N_CrewTrainingStatus = 'C' and PlannedFor is not null and cpd.CurrentVesselId = "+ VesselID + sourceclause;
            
            DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptTrainingsDone.DataSource = Dt;
        rptTrainingsDone.DataBind();
        TrainingCounter.Text = Dt.Rows.Count + " records found.";
    }
    public void TrainingsPending()
    {
        string sourceclause = "";
        if (source == 1)
        {
            sourceclause = " and ISNULL(SOURCE, 0) = 1";
        }
        else if (source == 2)
        {
            sourceclause = " and ISNULL(SOURCE, 0) = 2";
        }
        else if (source == 3)
        {
            sourceclause = " and ISNULL(SOURCE, 0) not in(1,2)";
        }
        string sql = "  select ROW_NUMBER()over(order by ct.CREWID,PlannedFor)as SNO,TrainingRequirementId,DBO.fn_getSimilerTrainingsName(ct.TRAININGID) as TrainingName,PlannedFor,FromDate  " +
              "   , ToDate,cpd.crewnumber,cpd.firstname + ' ' + cpd.middlename + ' ' + cpd.lastname as crewname , Attended, institutename, CASE WHEN ISNULL(SOURCE, 0) = 1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)= 2 THEN 'MATRIX' ELSE 'PEAP' END as SourceName " +
              "   ,Case when isnull(AttachmentName, '') = '' then 0 else 1 end HasFile " +
              "   from DBO.CrewTrainingRequirement ct " +
              "   inner join CrewPersonalDetails cpd on ct.CrewId = cpd.CrewId and cpd.CrewStatusId = 3 and ct.StatusId = 'A' " +
              "   left join DBO.traininginstitute i on ct.TrainingPlanningId = i.instituteid " +
              "   where ct.StatusId = 'A' AND N_CrewTrainingStatus = 'O' and PlannedFor is not null and cpd.CurrentVesselId = " + VesselID + sourceclause;

        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptTrainingsDone.DataSource = Dt;
        rptTrainingsDone.DataBind();
        TrainingCounter.Text = Dt.Rows.Count + " records found.";
    }

    protected void SetTrainingMatrixID(int VesselId)
    {
        int TrainingMatrixId = 0;
        string sql = " select * from TrainingMatrixForVessel Where VesselID=" + VesselId;
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (Dt.Rows.Count > 0)
        {
            TrainingMatrixId = Common.CastAsInt32(Dt.Rows[0]["TrainingMatrixId"]);
        }
        
    }
}