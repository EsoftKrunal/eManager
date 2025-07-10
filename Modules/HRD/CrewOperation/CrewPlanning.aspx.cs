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

public partial class CrewOperation_CrewPlanning : System.Web.UI.Page
{
    Authority Auth;
    public AuthenticationManager authPage = new AuthenticationManager(0, 0, ObjectType.Page);
    public int SignOffCrewId
    {
        get { return Common.CastAsInt32(ViewState["SignOffCrewId"]); }
        set { ViewState["SignOffCrewId"] = value; }

    }
    public int SignOnCrewId
    {
        get { return Common.CastAsInt32(ViewState["SignOnCrewId"]); }
        set { ViewState["SignOnCrewId"] = value; }
    }
    public int PlanVesselId
    {
        get { return Common.CastAsInt32(ViewState["PlanVesselId"]); }
        set { ViewState["PlanVesselId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessRemarksOffSigner.Text = "";
        lblRemarksUpdatedByOnSigner.Text = "";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        //*******************
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
        OBJ.Invoke();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select office from dbo.Hr_PersonalDetails where userid="+ Session["loginid"].ToString() );
        int OfficeID = 0;
        int LoginId = Convert.ToInt32(Session["loginid"].ToString());

        if (dt.Rows.Count > 0)
            OfficeID = Common.CastAsInt32(dt.Rows[0][0]);

        if (OfficeID == 3 || LoginId == 1)
        {
            box_assign.Visible = true;
        }
        else
        {
            box_assign.Visible = false;
        }

        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        authPage = new AuthenticationManager(3, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (!Page.IsPostBack)
        {
            BindRank();
            BindOffice();
            DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS("select Firstname + ' ' + LastName as username,LogiNid from userlogin where RoleId=9 and StatusId='A'");
            //ddlManningUser.DataSource = Dt;
            //ddlManningUser.DataTextField = "username";
            //ddlManningUser.DataValueField = "LogiNid";
            //ddlManningUser.DataBind();
            //ddlManningUser.Items.Insert(0, new ListItem(" < UnAsssigned > ", ""));

            ddlPrintByManningUser.DataSource = Dt;
            ddlPrintByManningUser.DataTextField = "username";
            ddlPrintByManningUser.DataValueField = "LogiNid";
            ddlPrintByManningUser.DataBind();
            ddlPrintByManningUser.Items.Insert(0, new ListItem(" < Select > ", ""));

            if (ddlPrintByManningUser.Items.FindByValue(LoginId.ToString()) != null)
            {
                ddlPrintByManningUser.SelectedValue = LoginId.ToString();
                ddlPrintByManningUser.Enabled = false;

            }

            #region --------------- SignOff ----------------
                //this.txt_to.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            BindFleet();
            BindOwner();
            BindVessel();
            #endregion
        }
    }

    #region PageLoaderControl-SignOff
    protected void BindFleet()
    {
        this.ddlFleet.DataValueField = "FLEETId";
        this.ddlFleet.DataTextField = "FLEETName";
        this.ddlFleet.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM FLEETMASTER ORDER BY FLEETNAME");
        this.ddlFleet.DataBind();
        this.ddlFleet.Items.Insert(0, new ListItem(" All Fleet ", "0"));
    }
    protected void BindOwner()
    {
        this.ddlOwner.DataValueField = "OWNERID";
        this.ddlOwner.DataTextField = "OWNERNAME";
        this.ddlOwner.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM OWNER ORDER BY OWNERNAME");
        this.ddlOwner.DataBind();
        this.ddlOwner.Items.Insert(0, new ListItem(" All Owner ", "0"));
    }
    protected void BindOffice()
    {
        this.ddlOffice.DataValueField = "OfficeID";
        this.ddlOffice.DataTextField = "OfficeName";
        this.ddlOffice.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("select OfficeID,OfficeName  from Office");
        this.ddlOffice.DataBind();
        this.ddlOffice.Items.Insert(0, new ListItem(" All Office ", "0"));

        int LoginId = Common.CastAsInt32(Session["loginid"].ToString());
        if (LoginId != 1)
        {
            string sql = "select office from Hr_PersonalDetails where UserId = " + LoginId;
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            int OfficeId = 0;
            try
            {
                OfficeId = Common.CastAsInt32(dt.Rows[0][0]);
                ddlOffice.SelectedValue = OfficeId.ToString();
		
		//if(OfficeId!=3) 
  //              	ddlOffice.Enabled = false;
            }
            catch { }

        }
    }

    protected void FleetOwner_Changed(object sender,EventArgs e)
    {
        BindVessel();
    }
    protected void BindVessel()
    {
        string whereclause = "";
        if (ddlFleet.SelectedIndex > 0)
            whereclause +="And FleetId=" + ddlFleet.SelectedValue;
        if (ddlOwner.SelectedIndex > 0)
            whereclause += "And OwnerId=" + ddlOwner.SelectedValue;
        if (ddlVessel.SelectedIndex > 0)
            whereclause += "And VESSELID=" + ddlVessel.SelectedValue;

        //string sql = "SELECT VESSELID,VESSELNAME FROM VESSEL WHERE vesselstatusid=1 and manningOfficeID in( select office from Hr_PersonalDetails where UserId=" + Common.CastAsInt32(Session["loginid"].ToString()) + " )  AND vesselid in (select UV.vesselid from uservesselrelation UV where UV.loginid=" + Common.CastAsInt32(Session["loginid"].ToString()) + ") " + whereclause + "  ORDER BY VESSELNAME";
        
        string sql = "SELECT VESSELID,VESSELNAME FROM VESSEL WHERE vesselstatusid=1  " + ((ddlOffice.SelectedIndex>0)  ? " and ( isnull(ManningOfficeId,0)=0 OR ManningOfficeId=" + ddlOffice.SelectedValue + ")":"") + " AND vesselid in (select UV.vesselid from uservesselrelation UV where UV.loginid=" + Common.CastAsInt32(Session["loginid"].ToString()) + ")   ORDER BY VESSELNAME";

        this.ddlVessel.DataValueField = "VesselId";
        this.ddlVessel.DataTextField = "VESSELNAME";
        this.ddlVessel.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(sql); ;
        this.ddlVessel.DataBind();

        this.ddlVessel.Items.Insert(0, new ListItem(" All Vessel ", "0"));
    }

    #endregion
    protected void BindSignOffGrid()
    {
        DataTable dtROff = Common.Execute_Procedures_Select_ByQueryCMS("select RecruitingOfficeId from userlogin where loginid=" + Session["loginid"].ToString());
        string AllowedOffices = "";
        if (dtROff.Rows.Count > 0)
            AllowedOffices = dtROff.Rows[0][0].ToString();

        string whereclause = " And ( Off_RecruitmentOfficeId in (-1," + AllowedOffices + ") OR ManningUserId=" + Session["loginid"].ToString() + " OR ON_RecruitmentOfficeId in (-1," + AllowedOffices + ") ) ";

        if (ddlOR.SelectedIndex > 0)
            whereclause += " And OffCrew='" + ddlOR.SelectedValue + "'";

        if (chkTop.Checked)
            whereclause += " And PLANRANKID IN (1,2,12,15,13,60,169)";

        string VesselClause = "SELECT V.VESSELID FROM DBO.VESSEL V WHERE V.VESSELSTATUSID=1 and V.Vesselid in (Select VesselId from UserVesselRelation with(nolock) where LoginId = "+ Session["loginid"].ToString() + ")  ";

        if (ddlOwner.SelectedIndex > 0)
            VesselClause += " AND OWNERID=" + ddlOwner.SelectedValue;
        if (ddlFleet.SelectedIndex > 0)
            VesselClause += " AND FLEETID=" + ddlFleet.SelectedValue;
        if (ddlVessel.SelectedIndex > 0)
            VesselClause += " AND VESSELID=" + ddlVessel.SelectedValue;

        VesselClause += ((ddlOffice.SelectedIndex > 0) ? " and ( isnull(v.ManningOfficeId,0)=0 OR v.ManningOfficeId=" + ddlOffice.SelectedValue + ")" : "");



        if (txtReliefDueInDays.Text.Trim() != "")
        {
            whereclause += " AND ( ReliefDueDate <= '" + Common.ToDateString(DateTime.Today.AddDays(Common.CastAsInt32(txtReliefDueInDays.Text.Trim()))) + "' or ReliefDueDate is null )";
            Session["Heading"] = " ( Relief due in next " + txtReliefDueInDays.Text.Trim() + " days. )";
        }
        else
            Session["Heading"] = "";


        if (ddlsRank.SelectedIndex > 0)
            whereclause += " AND CURRENTRANKID=" + ddlsRank.SelectedValue;


        string Sql = "SELECT S.*,COLOR=CASE WHEN ISNULL(ON_cREWID,0)<=0 AND RELIEFDUEDATE<=DATEADD(DAY,0,GETDATE()) THEN '#ff9980' else '' end,V1.VesselName,ApprovalStatusName=case when APP_STATUS='A' then 'Approved' when APP_STATUS='R' then 'Rejected' when APP_STATUS='P' then 'Pending' else '' end FROM VW_CREWPLANNINGS S left JOIN DBO.VESSEL V1 ON S.CURRENTVESSELID=V1.VESSELID";
                     
	string Sql1 =  " WHERE PLANVESSELID IN (" + VesselClause + ") " + whereclause ;

	//Response.Write(Sql + Sql1 + " Order by PLANRANKLEVEL");

        Session["SqlPrintCrewPlanning"] = Sql;
	Session["SqlPrintCrewPlanning_where"] = Sql1;        

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql + Sql1 + " Order by PLANRANKLEVEL");
        rpt_SignOffList.DataSource = dt;
        rpt_SignOffList.DataBind();

    }

    //#region --------------- SignOff ----------------

    protected void btnSearchSignOff_Click(object sender, EventArgs e)
    {
        BindSignOffGrid();
    }
    //protected void imgReliver_Remove_Click(object sender, EventArgs e)
    //{
    //    DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
    //    foreach (DataRow dr in dtroleid.Rows)
    //    {
    //        if (Convert.ToInt32(dr["RoleId"]) != 4)
    //        {
    //            ImageButton btn=((ImageButton)sender);
    //            int CrewId = Convert.ToInt32(btn.CommandArgument);
    //            int RelieverId = Convert.ToInt32(btn.Attributes["RelieverId"]);
    //            //****************** Code to Check Deletion of Crew Member
    //            DataTable dtck = SearchSignOff.DeleteCrewfromPlanning(RelieverId);
    //            foreach (DataRow drd in dtck.Rows)
    //            {
    //                if (Convert.ToInt32(drd[0].ToString()) <= 0)
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Crew Member Exists in an Open Port Call.');", true);
    //                    return;
    //                }
    //            }
    //            //******************
    //            SearchSignOff.UpdReliver_Tempplanning(CrewId, RelieverId, 0, 1);
    //            BindSignOffGrid();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this,this.GetType(),"a","alert('ReadOnly Users Are Not Authorized to Delete.');",true);
    //        }
    //    }
    //}
    //protected void imgReliver_Plan_Click(object sender, EventArgs e)
    //{
    //    ImageButton btn=((ImageButton)sender);
    //    int CrewId = Convert.ToInt32(btn.CommandArgument);
    //    frmSignOn.Attributes.Add("src", "ReliefPlanning_SignOn.aspx?CrewId=" + CrewId);
    //    dv_SignOn.Visible = true;
    //}
    protected void btn_Close_Search_Click(object sender, EventArgs e)
    {
        BindSignOffGrid();
        dv_SignOn.Visible = false;
    }

    //--Print --------------------------------------------------------------------------
    
    protected void btnOpenPrintPopup_OnClick(object sender, EventArgs e)
    {
        divPrintPopup.Visible = true;
    }

    protected void btnClosePrintPopup_Click(object sender, EventArgs e)
    {
        divPrintPopup.Visible = false;
    }
    protected void FilterRadios_OnCheckedChanged(object sender, EventArgs e)
    {
        if (radAll.Checked)
            ddlPrintByManningUser.Visible = false;
        else
            ddlPrintByManningUser.Visible = true;
    }
    protected void ddlPrintByManningUser_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlManningUser
    }
    protected void btnGoToPrint_OnClick(object sender, EventArgs e)
    {
        if(!(radAll.Checked || radAssignedtoManningUser.Checked))
        {
            lblMsgPrint.Text = "Please select radio.";
            return;
        }

        string mf = "";
        if (radAll.Checked)
        {
            mf = "0";
        }
        else
        {
            if (ddlPrintByManningUser.SelectedIndex == 0)
            {
                lblMsgPrint.Text = "Please select manning agent.";
                return;
            }
            mf = ddlPrintByManningUser.SelectedValue;
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "aaaa", " window.open('../Reporting/CrewPlanning.aspx?mf="+ mf + "'); ", true);
    }

    //#endregion

    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        BindSignOffGrid();
    }

    // Magage Sign on and Off
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OpenDetails_Click(object sender, EventArgs e)
    {
        divSignOnOff.Visible = true;
        ImageButton btn = (ImageButton)sender;

        HiddenField hfdVesselId = (HiddenField)btn.Parent.FindControl("hfdVesselId");
        HiddenField hfdSignOffId = (HiddenField)btn.Parent.FindControl("hfdSignOffId");
        HiddenField hfdSignOnId = (HiddenField)btn.Parent.FindControl("hfdSignOnId");

        PlanVesselId = Common.CastAsInt32(hfdVesselId.Value);
        SignOffCrewId = Common.CastAsInt32(hfdSignOffId.Value);
        SignOnCrewId = Common.CastAsInt32(hfdSignOnId.Value);

        ViewState["Off_CrewId"] = SignOffCrewId;
        ViewState["Off_VslId"] = PlanVesselId;

        ShowSingOn_and_OffDetails();
    }
    protected void btnCloseSignOnOff_OnClick(object sender, EventArgs e)
    {
        divSignOnOff.Visible = false;
        BindSignOffGrid();
    }
    protected void btnDeleteOnSigner_OnClick(object sender, EventArgs e)
    {
        if(SignOffCrewId > 0)
        {
            Delete1(SignOffCrewId, SignOnCrewId);
        }
        else
        {
            Delete2_from_VesselPlanning(SignOnCrewId, PlanVesselId);
        }

        string Sql = "SELECT On_crewid FROM VW_CREWPLANNINGS where OFF_crewid=" + SignOffCrewId + " and PLANVESSELid=" + PlanVesselId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
        rptOffSignerDetails.DataSource = dt;
        SignOnCrewId = Common.CastAsInt32(dt.Rows[0][0]);
        ShowSingOn_and_OffDetails();
    }
    

    protected void ShowSingOn_and_OffDetails()
    {
        txtRemarkOffsigner.Text = "";
        txtCommentOnSigner.Text = "";

        lbkAddSigner.Visible = false;
        dvOffSignerDetails.Visible = false;

        string Sql = "";
        if (SignOffCrewId > 0)
        {
            dvOffSignerDetails.Visible = true;

            Sql = "SELECT * FROM VW_CREWPLANNINGS where OFF_crewid=" + SignOffCrewId + " and PLANVESSELid=" + PlanVesselId;
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
            rptOffSignerDetails.DataSource = dt;
            rptOffSignerDetails.DataBind();
            txtRemarkOffsigner.Text = dt.Rows[0]["Off_Plan_Remarks"].ToString();
            if(dt.Rows[0]["Off_Plan_RemarksUpdatedBy"].ToString()!="")
                lblRemarksUpdatedByOffSigner.Text = dt.Rows[0]["Off_Plan_RemarksUpdatedBy"].ToString()+" / " + Common.ToDateString(dt.Rows[0]["Off_Plan_RemarksUpdatedOn"].ToString());


			string sql1 = "SELECT IsVacancy FROM crewpersonaldetails where crewid=" + SignOffCrewId.ToString();
            DataTable dt01 = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
            bool IsVacancy = false;
             IsVacancy=Convert.ToBoolean(dt01.Rows[0][0].ToString());
            if(IsVacancy)
            {
				chkAssigntoManing.Checked=true;
                lbAddVacancy.Visible = true;
				//ddlManningUser.SelectedValue=ManningUserId.ToString();
				//chkAssigntoManing_OnCheckedChanged(new Object(),new EventArgs());
            }
            else
            {
				chkAssigntoManing.Checked=false;
                lbAddVacancy.Visible = false;
                //ddlManningUser.SelectedValue="";
                //chkAssigntoManing_OnCheckedChanged(new Object(),new EventArgs());
            }
        }
        else
        {
            rptOffSignerDetails.DataSource = null;
            rptOffSignerDetails.DataBind();
        }

        if (SignOnCrewId > 0)
        {
            Sql = "SELECT * FROM VW_CREWPLANNINGS where On_CREWid=" + SignOnCrewId + " and PLANVESSELid=" + PlanVesselId;
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
            rptOnSignerDetails.DataSource = dt;
            rptOnSignerDetails.DataBind();

            divOnSignerData.Visible = true;
            
            txtCommentOnSigner.Text = dt.Rows[0]["On_Plan_Remarks"].ToString();
            if (dt.Rows[0]["On_Plan_RemarksUpdatedBy"].ToString() != "")
                lblRemarksUpdatedByOnSigner.Text = dt.Rows[0]["On_Plan_RemarksUpdatedBy"].ToString() + " / " + Common.ToDateString(dt.Rows[0]["On_Plan_RemarksUpdatedOn"].ToString());
        }
        else
        {
            lbkAddSigner.Visible = true;

            rptOnSignerDetails.DataSource = null;
            rptOnSignerDetails.DataBind();
            divOnSignerData.Visible = false;
        }
    }

    public void Delete1(int CrewId,int RelieverId)
    {
        DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
        foreach (DataRow dr in dtroleid.Rows)
        {
            if (Convert.ToInt32(dr["RoleId"]) != 4)
            {
                //****************** Code to Check Deletion of Crew Member
                DataTable dtck = SearchSignOff.DeleteCrewfromPlanning(RelieverId);
                foreach (DataRow drd in dtck.Rows)
                {
                    if (Convert.ToInt32(drd[0].ToString()) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Crew Member Exists in an Open Port Call.');", true);
                        return;
                    }
                }
                //******************
                //SearchSignOff.UpdReliver_Tempplanning(CrewId, RelieverId, 0, 1);
                Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.Cancel_RelieverPlanning " + CrewId + "," + RelieverId + ",0,1,'" + Session["UserName"].ToString() + "'");
                BindSignOffGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('ReadOnly Users Are Not Authorized to Delete.');", true);
            }
        }
    }
    public void Delete2_from_VesselPlanning(int CrewId,int VesselID)
    {
        int res;
        DataTable dtroleid = SearchSignOff.getCrewRoleId(Convert.ToInt32(Session["loginid"].ToString()));
        foreach (DataRow dr in dtroleid.Rows)
        {
            if (Convert.ToInt32(dr["RoleId"]) != 4)
            {
                
                res = SearchSignOff.Check_Planning_Deleted(Convert.ToInt32(VesselID), CrewId);
                if (res == 1)
                {
                    lb_msg.Text = "Contract has been created for this Crew Member.";
                    return;
                }
                else if (res == 2)
                {
                    lb_msg.Text = "RFQ has been created for this Crew Member.";
                    return;
                }
                else if (res == 3)
                {
                    lb_msg.Text = "RFQ has been created for this Crew Member.";
                    return;
                }
                else
                {
                    SearchSignOff.DeleteReliver_planning(CrewId, Convert.ToInt32(VesselID));
                }
            }
            else
            {
                lb_msg.Text = "ReadOnly Users Are Not Authorized to Delete.";
            }
        }
    }

    
    protected void btnUpdateRemarksOffSigner_OnClick(object sender, EventArgs e)
    {
        string remarks=txtRemarkOffsigner.Text.Trim().Replace("'", "`");

        try
        {
            string Sql = " UPDATE crewpersonaldetails set  " +
                  "  Plan_Remarks = '"+ remarks + "',Plan_RemarksUpdatedBy ='"+Session["UserName"].ToString()+"' ,Plan_RemarksUpdatedOn = getdate() " +

                  "  where CrewId =" + SignOffCrewId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
            ShowSingOn_and_OffDetails();
            lblMessRemarksOffSigner.Text = "Record updated successfully.";
        }
        catch(Exception ex)
        {
            lblMessRemarksOffSigner.Text = "Error while update record.";
        }
    }
    protected void btnUpdateRemarksOnSigner_OnClick(object sender, EventArgs e)
    {
        string remarks = txtCommentOnSigner.Text.Trim().Replace("'", "`");
        try
        {
            string Sql = " UPDATE crewpersonaldetails set  " +
                  "  Plan_Remarks = '" + remarks + "',Plan_RemarksUpdatedBy ='" + Session["UserName"].ToString() + "' ,Plan_RemarksUpdatedOn = getdate() " +

                  "  where CrewId =" + SignOnCrewId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
            ShowSingOn_and_OffDetails();
            lblMessRemarksOnSigner.Text = "Record updated successfully.";
        }
        catch (Exception ex)
        {
            lblMessRemarksOffSigner.Text = "Error while update record.";
        }
    }

    //--------------------------------------------------------------------
    protected void lbkAddSigner_OnClick(object sender, EventArgs e)
    {
        divSignOnOff.Visible = false;
        dvAddSignerDetails.Visible = true;
        ifmAddSignerDetails.Attributes.Add("src", "ReliefPlanning_SignOn.aspx?CrewId="+ SignOffCrewId.ToString());   


    }
    protected void btnCloseAddSignerDiv_Click(object sender, EventArgs e)
    {
        string Sql = "SELECT On_crewid FROM VW_CREWPLANNINGS where OFF_crewid=" + SignOffCrewId + " and PLANVESSELid=" + PlanVesselId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(Sql);
        rptOffSignerDetails.DataSource = dt;
        SignOnCrewId = Common.CastAsInt32(dt.Rows[0][0]);

        divSignOnOff.Visible = true;
        dvAddSignerDetails.Visible = false;
        ShowSingOn_and_OffDetails();
    }
    protected void lnlPlaningHistiry_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('CrewPlanningHistory.aspx?CrewId=" + ViewState["Off_CrewId"] .ToString() + "&VesselId=" + ViewState["Off_VslId"].ToString() + "','');", true);
    }
    

    protected void BindRank()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddlsRank.DataSource = obj.ResultSet.Tables[0];
      
        obj.ResultSet.Tables[0].Rows[0]["RankName"]="All Rank";
        ddlsRank.DataTextField = "RankName";
        ddlsRank.DataValueField = "RankId";
        ddlsRank.DataBind();
    }
    protected void chkAssigntoManing_OnCheckedChanged(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CREWPERSONALDETAILS SET IsVacancy=1 WHERE CREWID=" + SignOffCrewId.ToString());
        lbAddVacancy.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Vacancy assigned successfully.');", true);
        //ddlManningUser.Visible = chkAssigntoManing.Checked;
        //btnAssigntoManning.Visible = chkAssigntoManing.Checked;
    }
    //protected void btnAssigntoManning_OnClick(object sender, EventArgs e)
    //{
    //    Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CREWPERSONALDETAILS SET ManningUserId=" + Common.CastAsInt32(ddlManningUser.SelectedValue) + " WHERE CREWID=" + SignOffCrewId.ToString());
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Slot assigned successfully.');", true);
    //}



    protected void lbAddVacancy_Click(object sender, EventArgs e)
    {
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('/"+ appname + "/Modules/HRD/Vacancy/AddUpdateVacancy.aspx?Mode=New&Vacancyid=0&Key=1');", true);
    }
}