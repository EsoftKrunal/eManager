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

public partial class ReliefPlanning_SignOn : System.Web.UI.Page
{
    public int PlanningCrewId
    {
        get { return Common.CastAsInt32(ViewState["PlanningCrewId"]); }
        set { ViewState["PlanningCrewId"] = value; } 
    }
    public int PlanningRankId
    {
        get { return Common.CastAsInt32(ViewState["PlanningRankId"]); }
        set { ViewState["PlanningRankId"] = value; }
    }
    public int PlanningVesselId
    {
        get { return Common.CastAsInt32(ViewState["PlanningVesselId"]); }
        set { ViewState["PlanningVesselId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Main.Text = "";

        if (!Page.IsPostBack)
        {
            #region --------------- SignOn ----------------
            Load_OwnerPool();
            Load_Vessel();
            Load_Vesseltype();
            Load_Rank();
            Load_Status();
            Load_RecruitingOffice();
            ddl_Year.Items.Add(new ListItem("Select", "0"));
            ddl_Year.Items.Add(new ListItem(DateTime.Today.Year.ToString(), DateTime.Today.Year.ToString()));
            ddl_Year.Items.Add(new ListItem((DateTime.Today.Year + 1).ToString(), (DateTime.Today.Year + 1).ToString()));
            PlanningCrewId =Common.CastAsInt32(Request.QueryString["CrewId"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CurrentRankId,CrewNumber,FirstName + ' ' +Middlename + ' ' + LastName as CrewName, CurrentVesselId,(SELECT VESSELCODE FROM DBO.VESSEL V WHERE V.VESSELID=CurrentVesselId) AS CurrentVesselCode,(SELECT OWNERCODE FROM DBO.OWNER O WHERE O.OWNERID IN (SELECT OwnerId FROM DBO.VESSEL V WHERE V.VESSELID=CurrentVesselId)) AS CurrentOwnerCode,(SELECT RANKNAME FROM RANK R WHERE R.RANKID=CrewPersonalDetails.CURRENTRANKID) AS RANKNAME FROM DBO.CrewPersonalDetails WHERE CREWID = " + PlanningCrewId);
            PlanningRankId = Common.CastAsInt32(dt.Rows[0]["CurrentRankId"]);
            PlanningVesselId = Common.CastAsInt32(dt.Rows[0]["CurrentVesselId"]);
            //lblGradingFor.Text = dt.Rows[0]["CurrentVesselCode"].ToString();
            ViewState["CurrentVesselCode"] = dt.Rows[0]["CurrentVesselCode"].ToString();
            ViewState["CurrentOwnerCode"] = dt.Rows[0]["CurrentOwnerCode"].ToString();
            
            lblPlanCrewName.Text = dt.Rows[0]["CrewNumber"].ToString() + " - " +  dt.Rows[0]["CrewName"].ToString() + " - " + dt.Rows[0]["RANKNAME"].ToString();
            #endregion
        }
    }
    #region PageLoaderControl-SignOn
    private void Load_Status()
    {
        DataSet ds = cls_SearchReliever.getMasterData("vw_PlanningStatus", "StatusId", "StatusName");
        ddl_Status.DataSource = ds.Tables[0];
        ddl_Status.DataTextField = "StatusName";
        ddl_Status.DataValueField = "StatusId";
        ddl_Status.DataBind();
    }
    private void Load_Rank()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Rank", "RankId", "RankCode");
        ddl_Rank.DataSource = ds.Tables[0];
        ddl_Rank.DataTextField = "RankCode";
        ddl_Rank.DataValueField = "RankId";
        ddl_Rank.DataBind();
        ddl_Rank.Items.Insert(0, new ListItem("< Select >", ""));
    }
    private void Load_Vessel()
    {
        //DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        //ddl_Vessel.DataSource = ds.Tables[0];
        //ddl_Vessel.DataValueField = "VesselId";
        //ddl_Vessel.DataTextField = "VesselName1";
        //ddl_Vessel.DataBind();
        //ddl_Vessel.Items.Insert(0, new ListItem("< Select >", ""));
    }
    private void Load_Vesseltype()
    {
        DataSet ds = cls_SearchReliever.getMasterData("VesselType", "VesselTypeId", "VesselTypeName");
        ddl_VesselType.DataSource = ds.Tables[0];
        ddl_VesselType.DataTextField = "VesselTypeName";
        ddl_VesselType.DataValueField = "VesselTypeId";
        ddl_VesselType.DataBind();
        ddl_VesselType.Items.Insert(0, new ListItem("< Select >", ""));
    }
    private void Load_OwnerPool()
    {
        DataSet ds = cls_SearchReliever.getMasterData("OwnerPool", "OwnerPoolId", "OwnerPoolName");
        ddl_OwnerPool.DataSource = ds.Tables[0];
        ddl_OwnerPool.DataTextField = "OwnerPoolName";
        ddl_OwnerPool.DataValueField = "OwnerPoolId";
        ddl_OwnerPool.DataBind();
        ddl_OwnerPool.Items.Insert(0, new ListItem("< Select >", ""));
    }
    private void Load_RecruitingOffice()
    {
        DataSet ds = cls_SearchReliever.getMasterData("RecruitingOffice", "RecruitingOfficeId", "RecruitingOfficeName");
        ddl_RecOff.DataSource = ds.Tables[0];
        ddl_RecOff.DataTextField = "RecruitingOfficeName";
        ddl_RecOff.DataValueField = "RecruitingOfficeId";
        ddl_RecOff.DataBind();
        ddl_RecOff.Items.Insert(0, new ListItem("< All >", ""));
    }
    #endregion
    #region --------------- SignOn ----------------
   
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        DataTable dtROff = Common.Execute_Procedures_Select_ByQueryCMS("select RecruitingOfficeId from userlogin where loginid=" + Session["loginid"].ToString());
        string AllowedOffices = "";
        if (dtROff.Rows.Count > 0)
            AllowedOffices = dtROff.Rows[0][0].ToString();

        string whereclause = " And Off_RecruitmentOfficeId in (0," + AllowedOffices + ")";

        int RelType = Convert.ToInt32(ddl_Status.SelectedValue);
        string Mode = (r1.Checked) ? "O" : "V";
        string sql = "EXEC DBO.GetReliversList1 " + RelType + ", '" + ddl_VesselType.SelectedValue + "','','" + ddl_OwnerPool.SelectedValue + "','" + ddl_Rank.SelectedValue + "','','N'," + Convert.ToInt32((chk_Exclude.Checked) ? "1" : "0") + ",'" + txt_FirstName.Text.Trim() + "','" + txt_LastName.Text.Trim() + "','" + txt_EmpNo.Text.Trim() + "','" + ddl_RecOff.SelectedValue + "','','" + ddl_Month.SelectedValue + "','" + ddl_Year.SelectedValue + "'," + PlanningVesselId + ",'" + Mode + "'," + Session["loginid"].ToString();
        
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        //lblGradingFor.Text = (Mode == "V") ? ViewState["CurrentVesselCode"].ToString() : ViewState["CurrentOwnerCode"].ToString();
        string Filter = "";

        DataView dv = dt.DefaultView;
        
        if (CheckBoxOR.Checked)
        {
            Filter += "  (OwnerRep = 'A' OR OwnerRep = 'B') ";
        }
        if (CheckBoxCH.Checked)
        {
            Filter += (Filter.Trim() == "" )? "" :  " AND " + "  (Charterer = 'A' OR Charterer = 'B') ";
        }
        if (CheckBoxTS.Checked)
        {
            Filter += (Filter.Trim() == "") ? "" : " AND " + "  (TechSupdt = 'A' OR TechSupdt = 'B') ";
        }
        if (CheckBoxFM.Checked)
        {
            Filter += (Filter.Trim() == "") ? "" : " AND " + "  (FleetMgr = 'A' OR FleetMgr = 'B') ";
        }
        if (CheckBoxMS.Checked)
        {
            Filter += (Filter.Trim() == "") ? "" : " AND " + "  (MarineSupdt = 'A' OR MarineSupdt = 'B') ";
        }

        Session["RP_FilterData"] = sql + "~" + Filter;
        dv.RowFilter = Filter;

        rpt_SignOnCrewList.DataSource = dv.ToTable();
        rpt_SignOnCrewList.DataBind();
    }
    protected void btnAssignCrew_Click(object sender, EventArgs e)
    {
        int ReliverId, ReliverRankId;
        bool res1, res2;
        res1 = false;
        res2 = false;

        ReliverId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ReliverRankId = Common.CastAsInt32(((ImageButton)sender).CssClass); 

        DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
        foreach (DataRow dr in dtroleid.Rows)
        {
            if (Convert.ToInt32(dr["RoleId"]) != 4)
            {
                int GroupId1, GroupId2;                  
                int res;
                    
                GroupId1 = cls_SearchReliever.getRankGroupId(Convert.ToInt32(PlanningRankId));
                GroupId2 = cls_SearchReliever.getRankGroupId(Convert.ToInt32(ReliverRankId));

                if (PlanningCrewId == ReliverId)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),"adxsada", "alert('Same Person Can not Relieve Himself.');",true);
                    return;
                }
                // RES1 - ALL THE DOCUMENT ARE NOT AVAILABLE FOR THAT VESSEL 
                // RES2 - RANK GROUP NOT MATCH
                res1 = (cls_SearchReliever.IS_DOCUMENTS_AVAILABLE(-1, PlanningCrewId, PlanningRankId, ReliverId) == 1);
                res2 = (GroupId1 != GroupId2);
                //--------------
                //ddl_Port.SelectedIndex = 0;
                txt_PRemarks.Text = "";
                if ((res1 == true) && (res2 == true))
                {
                    md1.Show();
                    lbl_MessText.Text = "Ranks does not match And Some Documents Are Also Missing. Do You Want To Continue?";
                    Session["RId"] = PlanningCrewId.ToString();
                    Session["RNId"] = PlanningRankId.ToString();
                    Session["RId1"] = ReliverId.ToString();
                    Session["RNId1"] = ReliverRankId.ToString();
                    return;
                }
                else if ((res1 == true) && (res2 == false))
                {
                    md1.Show();
                    lbl_MessText.Text = "Some Documents Are Also Missing. Do You Want To Continue?";
                    Session["RId"] = PlanningCrewId.ToString();
                    Session["RNId"] = PlanningRankId.ToString();
                    Session["RId1"] = ReliverId.ToString();
                    Session["RNId1"] = ReliverRankId.ToString();
                    return;
                }
                else if ((res1 == false) && (res2 == true))
                {
                    md1.Show();
                    lbl_MessText.Text = "Ranks Does Not Match. Do You Want To Continue?";
                    Session["RId"] = PlanningCrewId.ToString();
                    Session["RNId"] = PlanningRankId.ToString();
                    Session["RId1"] = ReliverId.ToString();
                    Session["RNId1"] = ReliverRankId.ToString();                     
                    return;
                }
                else
                {
                    md1.Show();
                    lbl_MessText.Text = "Please Provide the Details Below.";
                    Session["RId"] = PlanningCrewId.ToString();
                    Session["RNId"] = PlanningRankId.ToString();
                    Session["RId1"] = ReliverId.ToString();
                    Session["RNId1"] = ReliverRankId.ToString();
                    return;
                }                     
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "adxsada", "alert('ReadOnly Users Are Not Authorized to Assign any Reliever.');", true);
            }
        }

    }

    protected void Main_Click(object sender, EventArgs e)
    {
        int res;
        int ReliverId, ReliverRankId, ReliveeId, ReliveeRankId, Loginid;

        ReliveeId = Convert.ToInt32(Session["RId"].ToString());
        ReliveeRankId = Convert.ToInt32(Session["RNId"].ToString());
        ReliverId = Convert.ToInt32(Session["RId1"].ToString());
        ReliverRankId = Convert.ToInt32(Session["RNId1"].ToString());
        Loginid = Convert.ToInt32(Session["loginid"].ToString());

        if (NewPlanning.Check_RelieverStatus1(ReliverId, ReliveeId) > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "adxsada", "alert('This CrewMember is already Planned for the Same Vessel.');", true);
            return;
        }
        if (txt_PRemarks.Text.Length > 255) { txt_PRemarks.Text.Substring(0, 255); }
        //Navin-Need to fix this
        res = cls_SearchReliever.Add_Reliver(ReliverId, ReliverRankId, ReliveeId, ReliveeRankId, txt_PRemarks.Text, Loginid, ReliverRankId, "");
        if (res == -1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "adxsada", "alert('There are Already Two Relievers Exists.');", true);
        }
        if (res == 2)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "adxsada", "alert('You can not Assign same Reliver Twice.');", true);
        }
        if (res == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "adxsada", "alert('Reliver planned successfully.');", true);
        }
    }
    protected void lnkMoreDetails_Click(object sender, EventArgs e)
    {
        int Crewid = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string sql = "SELECT CCB.CREWBONUSID,CCB.CREWID,CPD.CREWNUMBER,CCB.CONTRACTID,CCB.ContractRefNumber,CPD.FIRSTNAME + ' ' +CPD.MIDDLENAME + ' ' +CPD.LASTNAME AS CREWNAME ,CCB.RANKID,RANKCODE,CCB.VESSELID,V.VESSELNAME, BonusApproved, BonusAmount, " +
                       "(SELECT TOP 1 MailSent FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailSent = 'Y') AS IsMailSent, " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 1) As OwnerRep, " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 2) As Charterer,  " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 3) As TechSupdt,  " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 4) As FleetMgr,  " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 5) As MarineSupdt, " +
                       "(SELECT SignOnDate FROM GET_CREW_HISTORY WHERE CREWID=CCB.CREWID AND CONTRACTID=CCB.CONTRACTID AND NewRankId = CCB.RANKID AND VesselId = CCB.VESSELID) As SignOndt, " +
                       "(SELECT SingOffDate FROM GET_CREW_HISTORY WHERE CREWID=CCB.CREWID AND CONTRACTID=CCB.CONTRACTID AND NewRankId = CCB.RANKID AND VesselId = CCB.VESSELID) As SignOffDt " +
                       "FROM CREWCONTRACTBONUSMASTER CCB " +
                       "INNER JOIN CREWPERSONALDETAILS CPD ON CCB.CREWID=CPD.CREWID " +
                       "INNER JOIN RANK R ON CCB.RANKID=R.RANKID " +
                       "INNER JOIN VESSEL V ON V.VESSELID=CCB.VESSELID " +
                       "WHERE STATUS='A' AND CCB.CREWID=" + Crewid.ToString() ;

        string Where = "";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql + Where + " Order By V.VESSELNAME , CAST(CCB.ContractRefNumber AS BIGINT) DESC ");         

        rprCrewAssessments.DataSource = dt;         
        rprCrewAssessments.DataBind();
        dv_Details.Visible = true;

    }
    protected void btnClose_MD_Click(object sender, EventArgs e)
    {
        dv_Details.Visible = false;
    }

    #endregion
}
