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

public partial class CrewOperation_VerifyTraining : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()),147);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy_Training.aspx");

        }
        //*******************
        //ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        //OBJ.Invoke();
        //Session["Authority"] = OBJ.Authority;
        //Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            BindVessel();
            BindRankDropDown();
            LoadData();
        }
    }
    public void LoadData()
    {
        String qry = "SELECT TRAININGREQUIREMENTID,crewnumber," +
                   "FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS FULLNAME, " +
                   "T.TRAININGNAME, " +
                   "Verified=CASE WHEN ISNULL(N_CrewVerified,'N')='Y' THEN 'Yes' ELSE 'No' END," +
                   "Remark," + 
                   "REPLACE(CONVERT(VARCHAR,N_DUEDATE,106),' ','-') AS DUEDATE, " +
                   "Attended=CASE WHEN ISNULL(ATTENDED,'N')='Y' THEN 'Yes' ELSE 'No' END, " +
                   "REPLACE(CONVERT(VARCHAR,FROMDATE,106),' ','-') AS FROMDATE, " +
                   "REPLACE(CONVERT(VARCHAR,TODATE,106),' ','-') AS TODATE, " +
                   "(SELECT INSTITUTENAME FROM TRAININGINSTITUTE TA WHERE TA.INSTITUTEID=CTR.TRAININGPLANNINGID) AS INSTITUTE " +
                   "FROM " +
                   "CREWTRAININGREQUIREMENT CTR " +
                   "INNER JOIN CREWPERSONALDETAILS CPD ON CPD.CREWID=CTR.CREWID " +
                   "INNER JOIN TRAINING T ON T.TRAININGID=CTR.TRAININGID " +
                   "WHERE CTR.STATUSID='A' AND ISNULL(ATTENDED,'N')='Y'";

        string WhereClause = "";
        if (txt_MemberId.Text.Trim() != "")
            WhereClause = WhereClause + " and CPD.CrewNumber='" + txt_MemberId.Text.Replace("'","''") + "'";
        if (ddl_CrewStatus_Search.SelectedIndex!=0)
            WhereClause = WhereClause + " and CPD.CrewStatusID=" + ddl_CrewStatus_Search.SelectedValue ;
        if (ddl_Vessel.SelectedIndex != 0)
            WhereClause = WhereClause + " and CPD.CurrentVesselID=" + ddl_Vessel.SelectedValue;
        if (ddl_Rank_Search.SelectedIndex != 0)
            WhereClause = WhereClause + " and CPD.CurrentRankID=" + ddl_Rank_Search.SelectedValue;

        qry = qry + WhereClause;

        GridView_PlanTraining.DataSource = Budget.getTable(qry);
        GridView_PlanTraining.DataBind();
    }
    protected void btn_Save_PlanTraining_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridView_PlanTraining.Rows.Count - 1; i++)
        {
            HiddenField hfd = (HiddenField)GridView_PlanTraining.Rows[i].FindControl("hfdCrewId");
            CheckBox chk = (CheckBox)GridView_PlanTraining.Rows[i].FindControl("chkSelect");
            if (chk.Checked)
            {
                Budget.getTable("Update crewtrainingrequirement set N_CrewVerified='" + ((chk_Attended.Checked)?"Y":"N") + "',Remark='" + txtRemark.Text + "' Where trainingrequirementid=" + hfd.Value);
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
