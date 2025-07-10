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

public partial class Reporting_AppraisalReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int PeapID
    {
        get { return Convert.ToInt32("0" + ViewState["vPeapID"].ToString()); }
        set { ViewState["vPeapID"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ////***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("../AuthorityError.aspx");
        //}

        //ProcessCheckAuthority Auth = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);

        if (!Page.IsPostBack)
        {
            PeapID = Common.CastAsInt32(Page.Request.QueryString["PeapID"]);
        }
        Showreport();
    }
    public void Showreport()
    {
        string JR1 = "", JR2 = "", JR3 = "", JRScale1 = "", JRScale2 = "", JRScale3 = "", Competency1 = "", Competency2 = "", Competency3 = "", Competency4 = "", Competency5 = "";
        string Competency6 = "", Competency7 = "", Competency8 = "", CopmScale1 = "", CopmScale2 = "", CopmScale3 = "", CopmScale4 = "", CopmScale5 = "", CopmScale6 = "", CopmScale7 = "", CopmScale8 = "";
        string AppraiseeName = "", AppraiseeRank = "", AppraiseeVessel = "", AppraiseeOccasion = "", AppraiseeAppraisalFrom = "", AppraiseeAppraisalTo = "", AppraiseeDTJoinedVessel = "", AppraiseePerformanceScore = "", AppraiseeCompetencyScore = "", PerformanceSecTotal = "", CompetencySecTotal = "";
        string PotSecA = "", PotSecB = "", PotSecC1 = "", PotSecC2 = "", AppraiserRemark = "", AppraiserName = "", AppraiserRank = "", AppraiserDate = "", MasterRemark = "", MasterName = "", MasterCrewNo = "", MasterDate = "";
        string AppraiseeRemarks = "", AppraiserCrewNo = "", JRAss1 = "", JRAss2 = "", JRAss3 = "", ReviewedBy = "", ReviewedDate = "", ReviewedComment = "", AppraiseeCrewNo="";
        string CpmpetencyScore = "", PerformanceFormula = "", AppraisalRecievedDate="";
        DataSet ds;

        string sql="SELECT AssMgntID,VesselCode,PeapType,CrewNo,rank,DatejoinedComp "+
            " ,(AssName+' '+AssLname)AssName" +
            " ,(select VesselName  FROM Vessel V where V.VesselCode=A.VesselCode)VesselName " +
            " ,replace(convert(varchar,AppraisalFromDate,106),' ','-')AppraisalFromDate " +
            " ,replace(convert(varchar,AppraisalToDate,106),' ','-')AppraisalToDate " +
            " ,replace(convert(varchar,DatejoinedVessel,106),' ','-')  DatejoinedVessel " +

            " ,replace(convert(varchar,RemAppraiserDate,106),' ','-')  RemAppraiserDate" +
            " ,replace(convert(varchar,RemMasterDate,106),' ','-')  RemMasterDate" +
            " ,replace(convert(varchar,AppraisalRecievedDate,106),' ','-') AppraisalRecievedDate" +

            //" ,(select RankName from rank R where R.rankID=A.ShipSoftRank )ShipSoftRank "+
            " ,(select RankCode from Rank R where R.RankID in((select CurrentrankID from CrewPersonalDetails  CD where CD.CrewNumber=A.CrewNo))) ShipSoftRank" +
            " ,(select AppraisalOccasionName  from AppraisalOccasion o where a.Occasion=o.AppraisalOccasionID ) Occasion " +
            " ,PerformanceScore,CompetenciesScore,PerScale1,PerScale2,PerScale3 "+
  " ,(PerScale1+PerScale2+PerScale3)TotalPerFormanceScore "+
  " ,perAss1,perAss2,perAss3,AssScale1,AssScale2,AssScale3,AssScale4,AssScale5,AssScale6,AssScale7,AssScale8 "+
  " ,(AssScale1+AssScale2+AssScale3+AssScale4+AssScale5+AssScale6+AssScale7+AssScale8)TotalcompetencyScore "+
  " ,PotSecA,PotSecB,PotSecB1,PotSecB2,PotSecC1,PotSecC2,RemAppraiseeComment,RemAppraiseeName,RemAppraiseeRank,RemAppraiseeCrewNo,RemAppraiseeDate "+
  " ,RemAppraiserComment,RemAppraiserName,RemAppraiserCrewNo,RemAppraiserRank,RemMasterComments,RemMasterName "+
  " ,AppMasterCrewNo,DateJoinedVesselAppraisee,ReviewedBy "+
  " ,replace(convert(varchar,ReviewedDate ,106),' ','-')ReviewedDate " +
  " ,ReviewedComment,IsTrainingRequired,Status " +
  " FROM DBO.tbl_Assessment A where AssMgntID=" + PeapID + "";
        ds = Budget.getTable(sql);
        string PeapType = "";
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow Dr = ds.Tables[0].Rows[0];
                
                if (Dr["PerScale1"].ToString()=="1")
                    PeapType = "MANAGEMENT TEAM";
                else if (Dr["PerScale1"].ToString()=="2")
                    PeapType = "SUPPORT TEAM";
                 else if (Dr["PerScale1"].ToString()=="3")
                    PeapType = "OPERATION TEAM";

                
                JRScale1 = Dr["PerScale1"].ToString();
                JRScale2 = Dr["PerScale2"].ToString();
                JRScale3 = Dr["PerScale3"].ToString();

                JRAss1 = Dr["perAss1"].ToString();
                JRAss2 = Dr["perAss2"].ToString();
                JRAss3 = Dr["perAss3"].ToString();
                


                CopmScale1 = Dr["AssScale1"].ToString();
                CopmScale2 = Dr["AssScale2"].ToString();
                CopmScale3 = Dr["AssScale3"].ToString();
                CopmScale4 = Dr["AssScale4"].ToString();
                CopmScale5 = Dr["AssScale5"].ToString();
                CopmScale6 = Dr["AssScale6"].ToString();
                CopmScale7 = Dr["AssScale7"].ToString();
                CopmScale8 = Dr["AssScale8"].ToString();

                AppraiseeName = Dr["AssName"].ToString();
                AppraiseeRank = Dr["ShipSoftRank"].ToString();
                AppraiseeVessel = Dr["VesselName"].ToString();
                AppraiseeOccasion = Dr["Occasion"].ToString();
                AppraiseeAppraisalFrom = Dr["AppraisalFromDate"].ToString();
                AppraiseeAppraisalTo = Dr["AppraisalToDate"].ToString();
                AppraiseeDTJoinedVessel = Dr["DatejoinedVessel"].ToString();
                AppraiseePerformanceScore = Dr["PerformanceScore"].ToString();
                AppraiseeCompetencyScore = Dr["CompetenciesScore"].ToString();

                PerformanceSecTotal = Dr["TotalPerFormanceScore"].ToString();
                CompetencySecTotal = Dr["TotalcompetencyScore"].ToString();

                PotSecA = Dr["PotSecA"].ToString();
                PotSecB = Dr["PotSecB"].ToString();
                PotSecC1 = Dr["PotSecC1"].ToString();
                PotSecC2 = Dr["PotSecC1"].ToString();

                AppraiseeRemarks = Dr["RemAppraiseeComment"].ToString();

                AppraiserRemark = Dr["RemAppraiserComment"].ToString();
                AppraiserName = Dr["RemAppraiserName"].ToString();
                AppraiserRank = Dr["RemAppraiserRank"].ToString();
                AppraiserCrewNo = Dr["RemAppraiserCrewNo"].ToString();
                AppraiserDate = Dr["RemAppraiserDate"].ToString();

                MasterRemark = Dr["RemMasterComments"].ToString();
                MasterName = Dr["RemMasterName"].ToString();
                MasterCrewNo = Dr["AppMasterCrewNo"].ToString();
                MasterDate = Dr["RemMasterDate"].ToString();

                ReviewedBy = Dr["ReviewedBy"].ToString();
                ReviewedDate = Dr["ReviewedDate"].ToString();
                ReviewedComment = Dr["ReviewedComment"].ToString();

                AppraiseeCrewNo = Dr["CrewNo"].ToString();
                AppraisalRecievedDate = Dr["AppraisalRecievedDate"].ToString();
                
                

                if (Convert.ToInt32(Dr["PeapType"]) == 1)
                {
                    JR1 = "Compliance with & enforcement of vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and Zero off-hire. ";
                    JR2 = "Compliance with & enforcement of Policies & Procedures from Owners, Charterers & Company’s Safety Management System. ";
                    JR3 = "Compliance with & enforcement of all Statutory, Flag State, Port State, National and International Regulations. ";

                    Competency1 = "Team Management";
                    Competency2 = "Decision Making";
                    Competency3 = "Process Orientation";
                    Competency4 = "Leadership";
                    Competency5 = "Strategy Execution";
                    Competency6 = "Technical Know-How";
                    Competency7 = "Managing Pressure and Stress";
                    Competency8 = "Initiative";
                    PerformanceFormula = "Performance Score = Total Score÷ 3 ";
                    CpmpetencyScore = "Competency Score= Total Score ÷ 8 ";
                    
                }
                else if (Convert.ToInt32(Dr["PeapType"]) == 2)
                {
                    JR1 ="Compliance with Company’s Policies and Procedures including but not limited to Safety and Protection of environment. ";
                    JR2 ="Understanding of & Compliance with Company’s Safety Management System. ";
                    JR3 ="";

                    Competency1 = "Team Management";
                    Competency2 = "Decision Making";
                    Competency3 = "Drive and Resilience";
                    Competency4 = "Interpersonal Skills ";
                    Competency5 = "Strategy Execution";
                    Competency6 = "Technical Know-how";
                    Competency7 = "Initiative";
                    Competency8 = "";
                    PerformanceFormula = "Performance Score = Total Score÷ 2 ";
                    CpmpetencyScore = "Competency Score= Total Score ÷ 7 ";
                }
                else if (Convert.ToInt32(Dr["PeapType"]) == 3)
                {

                    JR1 = "Understanding of & Compliance with vessel’s KPIs including but not limited to Zero Spill, Zero Incidents and Zero off-hire.";
                    JR2 = "Understanding of & Compliance with Policies & Procedures of Owners, Charterers & Company’s Safety Management System. ";
                    JR3 = "Understanding of & Compliance with all Statutory, Flag State, Port State, National and International Regulations. ";

                    Competency1 = "Team Management";
                    Competency2 = "Decision Making";
                    Competency3 = "Drive and Resilience";
                    Competency4 = "Interpersonal Skills ";
                    Competency5 = "Strategy Execution";
                    Competency6 = "Technical Know-how";
                    Competency7 = "Initiative";
                    Competency8 = "";
                    PerformanceFormula = "Performance Score = Total Score÷ 3 ";
                    CpmpetencyScore = "Competency Score= Total Score ÷ 7 ";
                }
                
            }
        }
        else
            return;


        //string SqlTraining = "select * "+
        //    " ,(SELECT Trainingname  From Training T Where T.TrainingID=C.TrainingID)TrainingType" +
        //    " from CrewTrainingRequirement C where CrewID="+GetCrewID().ToString()+" and TrainingPlanningID=99";

        string SqlTraining = "select  "+
            " (Select TrainingTypeName from TrainingType TT where TrainingTypeID= "+
	        " (SELECT TypeofTraining From Training T Where T.TrainingID=C.TrainingID))TrainingTypetxt  "+
            " ,(SELECT Trainingname  From Training T Where T.TrainingID=C.TrainingID)TrainingName "+
            " ,replace(convert(varchar,N_DueDate ,106),' ','-')Todate  " +
            " from CrewTrainingRequirement C where CrewID=" + GetCrewID().ToString() + " and ISNULL(C.SOURCE,0)=0";
        DataSet DsTraining = Budget.getTable(SqlTraining);

        this.CrystalReportViewer1.Visible = true;
        rpt.Load(Server.MapPath("AppraisalReport.rpt"));
        CrystalReportViewer1.ReportSource = rpt;

        DsTraining.Tables[0].TableName = "ShowAppraisalreportTraining";
        rpt.SetDataSource(DsTraining.Tables[0]);
        DataTable dt1 = PrintCrewList.selectCompanyDetails();

        rpt.SetParameterValue("JR1", JR1);
        rpt.SetParameterValue("JR2", JR2);
        rpt.SetParameterValue("JR3", JR3);


        rpt.SetParameterValue("JRScale1", JRScale1);
        rpt.SetParameterValue("JRScale2", JRScale2);
        rpt.SetParameterValue("JRScale3", JRScale3);

        rpt.SetParameterValue("JRAss1", (JRAss1 == "") ? "NILL" : JRAss1);
        rpt.SetParameterValue("JRAss2", (JRAss2 == "") ? "NILL" : JRAss2);
        rpt.SetParameterValue("JRAss3", (JRAss3 == "") ? "NILL" : JRAss3);
        


        rpt.SetParameterValue("Competency1", Competency1);
        rpt.SetParameterValue("Competency2", Competency2);
        rpt.SetParameterValue("Competency3", Competency3);
        rpt.SetParameterValue("Competency4", Competency4);
        rpt.SetParameterValue("Competency5", Competency5);
        rpt.SetParameterValue("Competency6", Competency6);
        rpt.SetParameterValue("Competency7", Competency7);
        rpt.SetParameterValue("Competency8", Competency8);

        rpt.SetParameterValue("CopmScale1", CopmScale1);
        rpt.SetParameterValue("CopmScale2", CopmScale2);
        rpt.SetParameterValue("CopmScale3", CopmScale3);
        rpt.SetParameterValue("CopmScale4", CopmScale4);
        rpt.SetParameterValue("CopmScale5", CopmScale5);
        rpt.SetParameterValue("CopmScale6", CopmScale6);
        rpt.SetParameterValue("CopmScale7", CopmScale7);
        rpt.SetParameterValue("CopmScale8", CopmScale8);

        rpt.SetParameterValue("AppraiseeName", AppraiseeName);
        rpt.SetParameterValue("AppraiseeRank", AppraiseeRank);
        rpt.SetParameterValue("AppraiseeCrewNo", AppraiseeCrewNo);
        
        rpt.SetParameterValue("AppraiseeVessel", AppraiseeVessel);
        rpt.SetParameterValue("AppraiseeOccasion", AppraiseeOccasion);
        rpt.SetParameterValue("AppraiseeAppraisalFrom", AppraiseeAppraisalFrom);
        rpt.SetParameterValue("AppraiseeAppraisalTo", AppraiseeAppraisalTo);
        rpt.SetParameterValue("AppraiseeDTJoinedVessel", AppraiseeDTJoinedVessel);
        rpt.SetParameterValue("AppraiseePerformanceScore", AppraiseePerformanceScore);
        rpt.SetParameterValue("AppraiseeCompetencyScore", AppraiseeCompetencyScore);
        rpt.SetParameterValue("PerformanceSecTotal", PerformanceSecTotal);
        rpt.SetParameterValue("CompetencySecTotal", CompetencySecTotal);

        rpt.SetParameterValue("PotSecA", (PotSecA == "") ? "NILL" : PotSecA);
        rpt.SetParameterValue("PotSecB", (PotSecB == "") ? "NILL" : PotSecB);
        rpt.SetParameterValue("PotSecC1", (PotSecC1== "") ? "NILL" : PotSecC1);
        rpt.SetParameterValue("PotSecC2", (PotSecC2 == "") ? "NILL" : PotSecC2);

        rpt.SetParameterValue("AppraiseeRemarks", (AppraiseeRemarks == "") ? "NILL" : AppraiseeRemarks);
        rpt.SetParameterValue("AppraiserRemark", (AppraiserRemark == "") ? "NILL" : AppraiserRemark);
        rpt.SetParameterValue("AppraiserName", AppraiserName);
        rpt.SetParameterValue("AppraiserRank", AppraiserRank);
        rpt.SetParameterValue("AppraiserCrewNo", AppraiserCrewNo);
        rpt.SetParameterValue("AppraiserDate", AppraiserDate);

        rpt.SetParameterValue("MasterRemark", (MasterRemark == "") ? "NILL" : MasterRemark);
        rpt.SetParameterValue("MasterName", MasterName);
        rpt.SetParameterValue("MasterCrewNo", MasterCrewNo);
        rpt.SetParameterValue("MasterDate", MasterDate);

        rpt.SetParameterValue("ReviewedBy", ReviewedBy);
        rpt.SetParameterValue("ReviewedDate", ReviewedDate);
        rpt.SetParameterValue("ReviewedComment", ReviewedComment);
        rpt.SetParameterValue("Peaplevel", PeapType);

        rpt.SetParameterValue("PerformanceFormula", PerformanceFormula);
        rpt.SetParameterValue("CpmpetencyScore", CpmpetencyScore);
        rpt.SetParameterValue("AppraisalRecievedDate", AppraisalRecievedDate);
        

        //foreach (DataRow dr in dt1.Rows)
        //{
            //rpt.SetParameterValue("Company", dr["CompanyName"].ToString());
            //rpt.SetParameterValue("Address", dr["Address"].ToString());
            //rpt.SetParameterValue("TelePhone", dr["TelephoneNumber"].ToString());
            //rpt.SetParameterValue("Fax", dr["Faxnumber"].ToString());
            //rpt.SetParameterValue("RegistrationNo", dr["RegistrationNo"].ToString());
            //rpt.SetParameterValue("Email", dr["Email1"].ToString());
            //rpt.SetParameterValue("Website", dr["Website"].ToString());
        //}
    }
    public int GetCrewID()
    {
        string sql = "select CrewID from CrewPersonalDetails where CrewNumber=(select CrewNo from tbl_assessment where AssmgntID="+PeapID+")";
        DataSet Ds = Budget.getTable(sql);
        if (Ds != null)
        {
            if (Ds.Tables[0].Rows.Count > 0)
            {
                return Common.CastAsInt32(Ds.Tables[0].Rows[0][0]);
            }
        }
        return 0;
    }
}
