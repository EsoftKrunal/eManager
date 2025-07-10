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

public partial class CrewOperation_UpdateTraining : System.Web.UI.Page
{
    AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 146);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy_Training1.aspx");

        }
        //*******************
        Auth = new AuthenticationManager(146, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        btn_Save_PlanTraining.Visible = Auth.IsUpdate; 
        //ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        //OBJ.Invoke();
        //Session["Authority"] = OBJ.Authority;
        //Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            ddl_TrainingReq_Training.DataSource = Budget.getTable("Select InstituteId,InstituteName from TrainingInstitute");
            ddl_TrainingReq_Training.DataTextField = "InstituteName";
            ddl_TrainingReq_Training.DataValueField= "InstituteId";
            ddl_TrainingReq_Training.DataBind();
            ddl_TrainingReq_Training.Items.Insert(0, new ListItem("< Select >", "0"));

            BindVessel();
            BindRankDropDown();
       }
    }
    public void LoadData()
    {
        String qry = "SELECT TRAININGREQUIREMENTID,crewnumber," +
                   "FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS FULLNAME, " +
                   "T.TRAININGNAME, " +
                   "REPLACE(CONVERT(VARCHAR,N_DUEDATE,106),' ','-') AS DUEDATE, " +
                   " replace(convert(varchar, (dbo.sp_getNextPlanDate(CPD.CrewID,CTR.TrainingID)),106),' ','-')PlanDate " +

                   " ,(case when isnull(CTR.source,0)=0 then 'PEAP' when isnull(CTR.source,0)=1 then 'ASSIGNED' when isnull(CTR.source,0)=2 then 'MATRIX' else '' end)Source " +
                   " ,Attended=CASE WHEN ISNULL(ATTENDED,'N')='Y' THEN 'Yes' ELSE 'No' END, " +
                    "REPLACE(CONVERT(VARCHAR,FROMDATE,106),' ','-') AS FROMDATE, " +
                   "REPLACE(CONVERT(VARCHAR,TODATE,106),' ','-') AS TODATE, " +
                   "(SELECT INSTITUTENAME FROM TRAININGINSTITUTE TA WHERE TA.INSTITUTEID=CTR.TRAININGPLANNINGID) AS INSTITUTE " +
                   "FROM " +
                   "CREWTRAININGREQUIREMENT CTR " +
                   "INNER JOIN CREWPERSONALDETAILS CPD ON CPD.CREWID=CTR.CREWID " +
                   "INNER JOIN TRAINING T ON T.TRAININGID=CTR.TRAININGID " +
                   "WHERE CTR.STATUSID='A' AND ISNULL(N_CREWVERIFIED,'N')='N' ";

        string WhereClause = "";
        if (txt_MemberId.Text.Trim() != "")
            WhereClause = WhereClause + " and CPD.CrewNumber='" + txt_MemberId.Text.Replace("'", "''") + "'";
        if (ddl_CrewStatus_Search.SelectedIndex != 0)
            WhereClause = WhereClause + " and CPD.CrewStatusID=" + ddl_CrewStatus_Search.SelectedValue;
        else
            WhereClause = WhereClause + " and CPD.CrewStatusID Not In(4,5)" ;

        if (ddl_Vessel.SelectedIndex != 0)
            WhereClause = WhereClause + " and CPD.CurrentVesselID=" + ddl_Vessel.SelectedValue;
        if (ddl_Rank_Search.SelectedIndex != 0)
            WhereClause = WhereClause + " and CPD.CurrentRankID=" + ddl_Rank_Search.SelectedValue;
        if (txtTrainingName.Text.Trim() != "")
            WhereClause = WhereClause + " and T.TRAININGNAME LIKE '" + txtTrainingName.Text + "%'";

        if (ddlSource.SelectedIndex != 0)
            WhereClause = WhereClause + " and ISNULL(CTR.Source,0)=" + ddlSource.SelectedValue;
        
        qry = qry + WhereClause;

        GridView_PlanTraining.DataSource = Budget.getTable(qry);
        GridView_PlanTraining.DataBind();
    }
    protected void btn_Save_PlanTraining_Click(object sender, EventArgs e)
    {
        string Dt1="", Dt2="";
        Dt1 = "NULL";
        Dt2 = "NULL";
        if (txt_FromDate.Text.Trim() != "") Dt1 = "'" + txt_FromDate.Text.Trim() + "'";
        if (txt_ToDate.Text.Trim() != "") Dt2 = "'" + txt_ToDate.Text.Trim() + "'";

        for (int i = 0; i <= GridView_PlanTraining.Items.Count - 1; i++)
        {
            bool attended = ((Dt1 != "NULL") || (Dt2 != "NULL"));

            HiddenField hfd = (HiddenField)GridView_PlanTraining.Items[i].FindControl("hfdCrewId");
            CheckBox chk = (CheckBox)GridView_PlanTraining.Items[i].FindControl("chkSelect");
            if (chk.Checked)
            {
                Budget.getTable("Update crewtrainingrequirement set fromdate=" + Dt1 + ",todate=" + Dt2 + ",trainingplanningid=" + ddl_TrainingReq_Training.SelectedValue + ",Attended='" + ((attended) ? "Y" : "N") + "',N_CrewTrainingStatus='C',N_CrewVerified='Y',N_VerifiedBy=0,N_VerifiedOn=getdate() Where trainingrequirementid=" + hfd.Value);
            }
        }
        LoadData(); 
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    private void BindVessel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataSource = ds;
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
    }
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddl_Rank_Search.DataSource = obj.ResultSet.Tables[0];
        ddl_Rank_Search.DataTextField = "RankName";
        ddl_Rank_Search.DataValueField = "RankId";
        ddl_Rank_Search.DataBind();
        ddl_Rank_Search.Items.RemoveAt(0);
        ddl_Rank_Search.Items.Insert(0, new ListItem("< All >", ""));

    }
}
