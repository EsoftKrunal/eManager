using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class CrewOperation_CrewPlanningApproval : System.Web.UI.Page
{
    public int planningid
    {
        set { ViewState["planningid"] = value; }
        get { return Common.CastAsInt32(ViewState["planningid"]); }
    }   
    public int CrewID
    {
        set { ViewState["_CrewID"] = value; }
        get { return Common.CastAsInt32(ViewState["_CrewID"]); }
    }   
    public int LoginId
    {
        set { ViewState["LoginId"] = value; }
        get { return Common.CastAsInt32(ViewState["LoginId"]); }
    }
    public int SeleTableID
    {
        set { ViewState["SeleTableID"] = value; }
        get { return Common.CastAsInt32(ViewState["SeleTableID"]); }
    }
    public int LoginIDPosition
    {
        set { ViewState["_LoginIDPosition"] = value; }
        get { return Common.CastAsInt32(ViewState["_LoginIDPosition"]); }
    }
    public string PlannedCompany
    {
        set { ViewState["_PlannedCompany"] = value; }
        get { return Convert.ToString(ViewState["_PlannedCompany"]); }
    }
    public bool Top4Id
    {
        set { ViewState["_Top4Id"] = value; }
        get { return Convert.ToBoolean(ViewState["_Top4Id"]); }
    }

   
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsgSendForApprovalList.Text = "";
        if (!IsPostBack)
        {
            Top4Id = false;
            planningid = Common.CastAsInt32(Request.QueryString["_P"]);
            LoginId = Common.CastAsInt32(Session["LoginId"].ToString());
            ShowData();
            SetLoginUserPosition();
        }
    }
    public DataTable BindUserList(int Level)
    {
        string sql = " Select l.LoginId,FirstName+' '+LastName as UserName from dbo.UserLogin l " +
                    " inner join CrewPlanningApprovalAuthority a on a.LoginID=l.LoginId " +
                    " where StatusID='A' and a.Approval" + Level + "=1 order by UserName ";
        DataTable dt = Budget.getTable(sql).Tables[0];
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "<Select>";
        dt.Rows.InsertAt(dr, 0);
        return dt;
    }
    protected void ShowData()
    {
        Boolean CrewParticularUpdated=false, PEAP=false, CheckList=false, Picture=false;
        
        
        string sql = "select cpd.crewid,CREWNUMBER , FIRSTNAME + ' ' +  isnull(MIDDLENAME,'') + ' ' + LASTNAME AS CREWNAME,cpd.AGE,v.VesselName,o.OwnerShortName,cvph.vesselid,rc.RankCode as CurrentRank,Case when cvph.NewRankId is null then cvph.RelieverRankId Else cvph.NewRankId END As NewRankId ,Case when rn.RankCode is null then rc.RankCode Else  rn.RankCode END as PlannedRank,CountryName,DateOfBirth,DateFirstJoin,cpd.VerifiedBy,cpd.VerifiedOn,PhotoPath,Promotion,NewCrew,AppStatus,ApprovedBy,ApprovedOn,FwdForApprovalBy,cvph.Remark,FwdForApprovalOn,FwdForAppprovalRemarks, " +
                    "(select CrewAppraisalId from tbl_Assessment where ContractId in (select top 1 contractid from CrewContractHeader cch where cch.crewid=cpd.crewid and Status='A')) as CrewAppraisalId " +
                    "from CrewVesselPlanningHistory cvph " +
                    "inner join CREWPERSONALDETAILS cpd on cvph.RelieverId = cpd.crewid " +
                    "inner join vessel v on v.VesselId = cvph.VesselId " +
                    "inner join Owner O on O.OwnerID = v.OwnerID " +                    
                    "inner join rank rc on rc.rankid = cvph.RelieverRankId " +
                    "left join rank rn on rn.rankid = cvph.NewRankId " +
                    "left join COUNTRY c on c.countryid = cpd.nationalityid " +
                    "where Planningid=" + planningid.ToString();

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            CrewID = Common.CastAsInt32(dt.Rows[0]["crewid"]);
            int NewRankId= Common.CastAsInt32(dt.Rows[0]["NewRankId"]);
            Top4Id = (NewRankId == 1 || NewRankId == 2 || NewRankId == 12 || NewRankId == 15 || NewRankId == 13 || NewRankId == 169);

            lblID.Text = dt.Rows[0]["CREWNUMBER"].ToString();
            lblName.Text = dt.Rows[0]["CREWNAME"].ToString();
            lblRank.Text = dt.Rows[0]["CurrentRank"].ToString();
            lblPlannedRank.Text = dt.Rows[0]["PlannedRank"].ToString();
            lblNationality.Text = dt.Rows[0]["CountryName"].ToString();

            lblDOB.Text = Common.ToDateString(dt.Rows[0]["DateOfBirth"])+ " [ "+Common.CastAsInt32(dt.Rows[0]["Age"]) +" Years ] ";                

            lblplannedvessel.Text = dt.Rows[0]["VesselName"].ToString();
            PlannedCompany= dt.Rows[0]["OwnerShortName"].ToString(); 

            lblVerifiedByOn.Text = dt.Rows[0]["VerifiedBy"].ToString() + " / " + Common.ToDateString(dt.Rows[0]["VerifiedOn"]);
            lblDJC.Text = Common.ToDateString(dt.Rows[0]["dateFirstjoin"]);
            lblPlanningRemarks.Text = dt.Rows[0]["Remark"].ToString();

            lblProposedByOn.Text = dt.Rows[0]["FwdForApprovalBy"].ToString() + " / " + Common.ToDateString(dt.Rows[0]["FwdForApprovalOn"]);
            lblFwdRemark.Text = dt.Rows[0]["FwdForAppprovalRemarks"].ToString();


            Picture = File.Exists(Server.MapPath("~/EMANAGERBLOB/HRD/CrewPhotos/"));
            imgcrewpic.ImageUrl = ResolveClientUrl("~/EMANAGERBLOB/HRD/CrewPhotos/") + dt.Rows[0]["PhotoPath"].ToString();
            string AppStatus = Convert.ToString(dt.Rows[0]["AppStatus"]);

            divproposer.Visible = false;
            btnSendBacktoProposal.Visible = false;

            if (AppStatus == "A")
            {
                lblApprovalStatus.Text = "Approved";
                divproposer.Visible = true;
            }
            else if (AppStatus == "R")
            {
                lblApprovalStatus.Text = "Rejected";
                divproposer.Visible = true;
            }
            else
            {
                if (Convert.IsDBNull(dt.Rows[0]["FwdForApprovalOn"]))
                {
                    lblApprovalStatus.Text = "Pending Submission";

                }
                else
                {
                    lblApprovalStatus.Text = "Awaiting for Approval";
                    divproposer.Visible = true;
                    btnSendBacktoProposal.Visible = true && LoginId==1;
                }
            }

            Boolean NewCrew = false;
            Boolean Promotion = false;

            try {NewCrew = Convert.ToBoolean(dt.Rows[0]["NewCrew"]);}catch { }
            try {Promotion = Convert.ToBoolean(dt.Rows[0]["Promotion"]);}catch { }

            lblCrewStatus.Text = NewCrew ? "New-Hand" : "Ex-Hand";

            //  Section 1 -  Crew Particular Updated

            CrewParticularUpdated = !(Convert.IsDBNull((dt.Rows[0]["VerifiedOn"])) || Convert.ToDateTime(dt.Rows[0]["VerifiedOn"]) < DateTime.Today.AddMonths(-6));
            ShowIcon(litCrewParticularUpdated, CrewParticularUpdated);

            //  Section 2 -  Last PEAP
            //sql = "select top 2 row_number() over( order by CrewAppraisalId desc) as rowno,crewid,FromDate,ToDate,AppraiserRemarks,AppraiseeRemarks,VesselName=(select v.VesselName from vessel v where v.vesselid=crewappraisaldetails.VesselId),Recommended,ImagePath,N_PerfScrore,N_CompScore "+
            //    "  , (SELECT MAX(GRADE) FROM CREWCONTRACTBONUSDETAILS WHERE CREWBONUSID IN(SELECT CREWBONUSID FROM CREWCONTRACTBONUSMASTER WHERE CREWCONTRACTBONUSMASTER.CREWID = CREWAPPRAISALDETAILS.CREWID AND CREWCONTRACTBONUSMASTER.SignOnDate BETWEEN FROMDATE AND TODATE)) grade "+
            //    " from DBO.crewappraisaldetails where crewid=" + CrewID + " order by CrewAppraisalId desc";
            //DataTable dt11 = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            //if (dt11.Rows.Count > 0)
            //{
            //    PEAP = true;
            //    pnllastpeap.Visible = true;
            //    rptLastPeap.DataSource = dt11;
            //    rptLastPeap.DataBind();
            //}
            //else
            //{
            //    pnllastpeap.Visible = false;
            //    if (NewCrew)
            //    {
            //        PEAP = true;
            //        lblpeapmsg.Text = "( Peap Not Required )";
            //    }
            //    else
            //        lblpeapmsg.Text = "( No PEAP found for last contract )";

            //}
            //ShowIcon(litLastPEAP, PEAP);

            // Section 3 - Matrix Compliance

            //-------------------------------
            if (NewRankId == 1 || NewRankId == 169 || NewRankId == 2 || NewRankId == 12 || NewRankId == 15)
            {
                divMatrixCompliance.Visible = true;
                DataTable dtExp = Budget.getTable("Exec dbo.sp_MatrixCompliance " + Common.CastAsInt32(dt.Rows[0]["vesselid"]).ToString() + ", " + planningid.ToString()).Tables[0];
                Label10.Text = GetSum(dtExp, "AllTypeExp", " (SireRank='Master' OR SireRank='Chief Officer') and sireranktype='officer'");
                Label20.Text = GetSum(dtExp, "RankExp", "(SireRank='Master' OR SireRank='Chief Officer') and sireranktype='officer'");
                Label30.Text = GetSum(dtExp, "OperatorExp", "(SireRank='Master' OR SireRank='Chief Officer') and sireranktype='officer'");
                Label11.Text = GetSum(dtExp, "AllTypeExp", "(SireRank='Chief Engineer' OR SireRank='1st Engineer') and sireranktype='engineer'");
                Label21.Text = GetSum(dtExp, "RankExp", "(SireRank='Chief Engineer' OR SireRank='1st Engineer') and sireranktype='engineer'");
                Label31.Text = GetSum(dtExp, "OperatorExp", "(SireRank='Chief Engineer' OR SireRank='1st Engineer') and sireranktype='engineer'");
                lblMatrixCompliance.Text = "<i class='fa fa-check fa-2x success' ></i>";
            }
            else
                divMatrixCompliance.Visible = false;
            

            // Section 4 - CRM
            sql = "select top 5 cd.*,ct.CRMCategoryName,(select firstname + ' ' + lastname from userlogin u where u.loginid=cd.createdby) as username from CrewCRMDetails cd inner join CRMCategory ct on cd.CRMCategory=ct.CRMCategory where crewid="+ CrewID + " order by createdon desc";
            DataTable dt12 = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            rptCRM.DataSource = dt12;
            rptCRM.DataBind();
            litCRM.Text = "<i class='fa fa-check fa-2x success' ></i>";

            // Section 5 - Document Checklist
            sql = " Select 1 from CREW_Doc_CheckList where PlanningId=" + planningid + " and status<>'Y' ";
            DataTable dtCheck = Budget.getTable(sql).Tables[0];
            if (dtCheck.Rows.Count <= 0)
            {
                sql = "Select top 1 CheckedBy,CheckedOn from CREW_Doc_CheckList where PlanningId=" + planningid + " and status='Y' ";
                DataTable dtCheck1 = Budget.getTable(sql).Tables[0];
                if (dtCheck.Rows.Count > 0)
                {
                    lblDC_CompletedByOn.Text = dtCheck.Rows[0]["CheckedBy"].ToString() + " / " + Common.ToDateString(dtCheck.Rows[0]["CheckedOn"]);
                    CheckList = true;
                }                                
            }
            if(CheckList)
                litDocumentChecklist.Text = "<i class='fa fa-check fa-1x success' ></i>";
            else
                litDocumentChecklist.Text = "<i class='fa fa-exclamation fa-1x error' ></i>";
            

            // Dynamic Checklist
            sql = " select PlanningId,CheckListMasterId,cm.CheckListname,AssignedBy,AssignedOn,CompletedBy,CompletedOn, case when CompletedOn is null then ' fa-exclamation error ' else ' fa-check success ' end imgClass " +
                    " from CrewPlanningAssignedCheckList cl left join tblCheckListMaster cm on cm.ID = cl.CheckListMasterId where cl.PlanningId=" + planningid;

            sql = " SELECT m.id as CheckListMasterId,m.CheckListname,  " +
                  " (select Top 1 VerifiedBy from tbl_CrewPlanningCheckList c where c.PlanningID = "+ planningid + " and CheckListMasterId = m.ID) as CompletedBy, " +
                  " (select Top 1 VerifiedOn from tbl_CrewPlanningCheckList c where c.PlanningID = "+ planningid + " and CheckListMasterId = m.ID) as CompletedOn " +
                  " FROM tblCheckListMaster m " +
                  " inner join " +
                  " ( " +
                  "  select CheckListID from tblCheckListRankMapping c inner join CrewVesselPlanningHistory h on h.NewRankId = c.RankID where c.CheckListID not in(1, 2) and h.PlanningId =  " + planningid + "  " +
                  "  union " +
                  "  select 1 from CrewVesselPlanningHistory where PlanningId = "+planningid+" and NewCrew = 1 " +
                  "  union " +
                  "  select 2 from CrewVesselPlanningHistory where PlanningId =  " + planningid + "  and Promotion = 1 and exists(select 1 from rank where rankid=" + NewRankId + " and offcrew='O')" +
                  " ) d on m.ID = d.CheckListID ";
            dt = Budget.getTable(sql).Tables[0];
            rptCheckList.DataSource = dt;
            rptCheckList.DataBind();
            foreach(DataRow dr in dt.Rows)
            {
                CheckList = CheckList && (!Convert.IsDBNull(dr["CompletedOn"]));                
            }

            // Section 10 - Approval Summary
            sql = " SElect ROW_NUMBER() over(order by TableId)RowNo,TableId,PlanningId,ApprovalLevel,alm.ID as ApprovalLevelID ,alm.ApprovalName,ApprovalFwdTo,ud.FirstName +  ' ' +ud.LastName as ApprovedBy,uf.FirstName +  ' ' +uf.LastName as ApprovalFwdToName,pp.PositionName,Comments,ApprovedOn,Case Result when 'A' then 'Approved' When 'R' then 'Rejected' end as Result " +
                 "  from CrewPlanningApprovalComments c " +
                 "  left join tbl_ApprovalLevelMaster alm on alm.ID =c.ApprovalLevel  " +
                 "  left join UserLogin ud on c.ApprovedById = ud.LoginId " +
                 "  left join UserLogin uf on c.ApprovalFwdTo=uf.LoginId" +
                 "  left join UserMaster p on p.LoginId = ud.LoginId " +
                 "  left join Position pp on pp.PositionId = p.PositionId " +
                 "  where PlanningID = " + planningid;
            dt = Budget.getTable(sql).Tables[0];
            rptApprovalList.DataSource = dt;
            rptApprovalList.DataBind();
            DataView dataview = dt.DefaultView;
            dataview.RowFilter = "ApprovedBy=''";
            
            if (dataview.ToTable().Rows.Count > 0)
                ShowIcon(litApprovalSummary, false);
            else
                ShowIcon(litApprovalSummary, true);
        }

        //----------------------------
        if (lblApprovalStatus.Text == "Pending Submission")
        {
            btnPopupSendForApproval.Visible = true;// CrewParticularUpdated && PEAP && CheckList && Picture;
        }
        else
            btnPopupSendForApproval.Visible = false;


       


    }
    public void ShowIcon(Literal ctl,Boolean result)
    {
        if (result)
            ctl.Text = "<i class='fa fa-check fa-2x success' ></i>";
        else
            ctl.Text = "<i class='fa fa-exclamation fa-2x error' ></i>";
    }
    // Section 2 - Last PEAP
    protected void btnPeapAttachment_Click(object sender,EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string filename= btn.CommandArgument;
        if(filename.Trim()!="")
        {
            filename=Server.MapPath("~/EMANAGERBLOB/HRD/Documents/Appraisal/") + filename.Trim();
            if (System.IO.File.Exists(filename))
            {
                Response.WriteFile(filename);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "sdsdd", "alert('File not found.')", true);
            }
        }
    }

    // Section 3 - Matrix Compliance
    private string GetSum(DataTable dt, String Column, string RowFilter)
    {
        double res = 0;
        DataView dv = dt.DefaultView;
        dv.RowFilter = RowFilter;
        DataTable dtTo = dv.ToTable();
        foreach (DataRow dr in dtTo.Rows)
        {
            res = res + double.Parse("0" + dr[Column].ToString());
        }
        return res.ToString();
    }
    
    // Section 9 - Send For Approval Click
    protected void btnPopupSendForApproval_OnClick(object sender, EventArgs e)
    {
        divSenForApprovalList.Visible = true;

        DataTable dtTemp = new DataTable();
        dtTemp.Columns.Add(new DataColumn("ApprovalLevelID", typeof(System.Int32)));
        dtTemp.Columns.Add(new DataColumn("ApprovalLevelText", typeof(System.String)));

        string sql = " Select * from tbl_ApprovalLevelMaster order by ID";
        DataTable dtAL = Budget.getTable(sql).Tables[0];

        sql = " select r.NoOfApproval,r.ApprovalLevels from crewvesselplanninghistory c left join Rank r on r.RankId=c.NewRankId where PlanningID =" + planningid;
        DataTable dt = Budget.getTable(sql).Tables[0];
        string Levels = dt.Rows[0][1].ToString();
        string[] parts = Levels.Split(',');
        DataRow dr;
        for (int i = 0; i <= parts.Length-1; i++)
        {
            int k =Common.CastAsInt32(parts[i]);

            if (k > 0)
            {
                dr = dtTemp.NewRow();
                dr["ApprovalLevelID"] = k;
                dr["ApprovalLevelText"] = dtAL.Rows[k - 1]["ApprovalName"].ToString();
                dtTemp.Rows.Add(dr);
            }
        }
        rptApprovalLevelEntry.DataSource = dtTemp;
        rptApprovalLevelEntry.DataBind();

        //string sql = "Exec DBO.CMS_CrewSendForApproval "+planningid+"";
        //Budget.getTable(sql);
        //BindApprovalList();
    }
    protected void btnClose_divSenForApprovalList_Click(object sender, EventArgs e)
    {
        divSenForApprovalList.Visible = false;
    }    
    protected void btnSendBacktoProposal_OnClick(object sender, EventArgs e)
    {
        Common.Set_Procedures("CMS_CrewPlanning_SendBacktoProposalStage");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
             new MyParameter("@PlanningId", planningid)
            );
        Boolean res;
        DataSet Ds = new DataSet();
        res = Common.Execute_Procedures_IUD_CMS(Ds);
        if (res)
        {
            ShowData();
        }
    }
    protected void btnSendForApproval_OnClick(object sender, EventArgs e)
    {
        int[] ApprovalLevelPerson = { 0, 0, 0, 0, 0 };
        
        //lblMsgSendForApprovalList
        bool AllSelected = true;
        foreach (RepeaterItem itm in rptApprovalLevelEntry.Items)
        {
            HiddenField hfdApprovalLevel=(HiddenField)itm.FindControl("hfdApprovalLevel");
            DropDownList ddlApprovalPerson = (DropDownList)itm.FindControl("ddlApprovalPerson");
            
            if (ddlApprovalPerson.SelectedIndex > 0 )
            {
                ApprovalLevelPerson[Common.CastAsInt32(hfdApprovalLevel.Value) - 1] = Common.CastAsInt32(ddlApprovalPerson.SelectedValue);
            }
            else
            {
                AllSelected = false;
                break;
            }
            
        }
        if(!AllSelected)
        {
            lblMsgSendForApprovalList.Text = "Please select approval user for all approval level.";
            return;
        }

        if (txtCommentSendForApproval.Text.Trim() == "")
        {
            lblMsgSendForApprovalList.Text = "Please enter remarks.";
            txtCommentSendForApproval.Focus();
            return;
        }
        //string sql = "Exec DBO.CMS_CrewSendForApproval "+planningid+","+ ApprovalLevelPerson [0] + "," + ApprovalLevelPerson[1] + "," + ApprovalLevelPerson[2] + "," + ApprovalLevelPerson[3] + "," + ApprovalLevelPerson[4]+",'"+txtCommentSendForApproval.Text+"'";
        //Budget.getTable(sql);

        Common.Set_Procedures("CMS_CrewSendForApproval");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters(
             new MyParameter("@PlanningId", planningid),
             new MyParameter("@Approval1", ApprovalLevelPerson[0]),
             new MyParameter("@Approval2", ApprovalLevelPerson[1]),
             new MyParameter("@Approval3", ApprovalLevelPerson[2]),
             new MyParameter("@Approval4", ApprovalLevelPerson[3]),              
             new MyParameter("@Approval5", ApprovalLevelPerson[4]),
             new MyParameter("@FwdForApprovalBy", Session["UserName"].ToString()),
             new MyParameter("@Remarks", txtCommentSendForApproval.Text.Trim())
            );
        Boolean res;
        DataSet Ds = new DataSet();
        res = Common.Execute_Procedures_IUD_CMS(Ds);
        if (res)
        {
            ShowData();
            btnPopupSendForApproval.Visible = false;
            divSenForApprovalList.Visible = false;


            List<string> listMails = new List<string>();
            string userIDs= string.Join(",", ApprovalLevelPerson);
            string sql = " select Email from UserLogin where LoginId in("+ userIDs + ") and ISNULL(Email,'')<>'' ";
            DataTable dt = Budget.getTable(sql).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                listMails.Add(dr["Email"].ToString());
            }


            //string[] ToAdds = { "emanager@energiossolutions.com"};// listMails.ToArray();  
	        string[] ToAdds = listMails.ToArray();

            string[] CCAdds = { Session["EmailAddress"] != null ? Session["EmailAddress"].ToString()  : null } ;
         

                // MailTo.ToString().Split(Sep);
            string[] BCCAdds = {  };
            String Subject = " Crew Approval for Vessel Assignment ";


            String MailBody;
            MailBody = "Crew Name: " + lblName.Text + " || Crew Number:" + lblID.Text + " || Planned Rank: " + lblPlannedRank.Text + "";
            MailBody = MailBody + "<br><br>Planned Vessel: " + lblplannedvessel.Text + " || Crew Type: " + lblCrewStatus.Text;
            MailBody = MailBody + "<br><br>Remark: " + txtCommentSendForApproval.Text.Trim() + "";
            MailBody = MailBody + "<br><br>Thanks & Best Regards";
            //------------------
            string AttachmentFilePath = "";
            string Error = "";
            SendEmail.SendeMail(0, "emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAdds, CCAdds, BCCAdds, Subject, MailBody, out Error, AttachmentFilePath);
        }
    }
    protected void ddlApprovalPerson_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        Label lblCrewLeaveStatus = (Label)ddl.Parent.FindControl("lblCrewLeaveStatus");
        
        string sql = " select 'On Leave [ '+ convert(varchar,leavefrom,106)+' - '+ convert(varchar,leaveTo,106)+' ]' from HR_LeaveRequest where empid in ( select empid from Hr_PersonalDetails where userid=  " + Common.CastAsInt32(ddl.SelectedValue) +") and(LeaveFrom <= getdate() and LeaveTo >= Getdate())  "+
                     "   union " +
                     "   select 'On Travel [ '+ convert(varchar,leavefrom,106)+' - '+ convert(varchar,leaveTo,106)+' ]' from HR_OfficeAbsence where empid in ( select empid from Hr_PersonalDetails where userid=  " + Common.CastAsInt32(ddl.SelectedValue) + " ) and(ActFromDt <= getdate() and ActToDt >= Getdate())  ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            lblCrewLeaveStatus.Text = dt.Rows[0][0].ToString();
        }
        else
            lblCrewLeaveStatus.Text = "";
    }
    

    // Section 10 - Approval Click
    protected void btnPopupApprove_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        SeleTableID = Common.CastAsInt32(btn.CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("exec dbo.Is_Approval_Allowed " + planningid + "," + LoginId);
        if (dt.Rows.Count > 0)
        {
            int success = Common.CastAsInt32(dt.Rows[0][0]);
            if (success == 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "18565", "alert('Same user is not allowed to approve twice.');", true);
            else
                divApprovalScreen.Visible = true;
        }
    }
    protected void btnClosedivApprovalScreen_OnClick(object sender, EventArgs e)
    {
        divApprovalScreen.Visible = false;
    }
    protected void btnSaveApprovalScreen_OnClick(object sender, EventArgs e)
    {
        if (txtComments.Text.Trim() == "")
        {
            lblMsgApprovalScreen.Text = "Please enter comments.";
            txtComments.Focus();
            return;
        }
        
        Common.Set_Procedures("CMS_CrewApproveReject");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
             new MyParameter("@TableId", SeleTableID),             
             new MyParameter("@ApprovedById", LoginId),
             new MyParameter("@Comments", txtComments.Text.Trim()),
             new MyParameter("@Reply", rdolistResult.SelectedValue)
            );
        Boolean res;
        DataSet Ds = new DataSet();
        res = Common.Execute_Procedures_IUD_CMS(Ds);
        if (res)
        {
            divApprovalScreen.Visible = false;
            ShowData();
        }
        else
        {
            
        }        
    }

    //public void ShowOtherCheckList()
    //{
    //    string sql = " select  top 1 VerifiedBy,VerifiedOn  from tbl_CrewPlanningCheckList where planningID=" + planningid+" and checkListMasterID=1 ";
    //    DataTable dtCheck = Budget.getTable(sql).Tables[0];
    //    if (dtCheck.Rows.Count > 0)
    //    {
    //        lblPromotionalChecklist.Text = dtCheck.Rows[0]["VerifiedBy"].ToString() + " / " + Common.ToDateString(dtCheck.Rows[0]["VerifiedOn"]);
    //        if (Common.ToDateString(dtCheck.Rows[0]["VerifiedOn"]) == "")
    //            litPromotionChecklist.Text = "<i class='fa fa-exclamation fa-2x error' ></i>";
    //        else
    //            litPromotionChecklist.Text = "<i class='fa fa-exclamation fa-2x success' ></i>";
    //        //imgPromotionChecklist.Visible = true;
    //    }
    //    else
    //        litPromotionChecklist.Text = "<i class='fa fa-exclamation fa-2x error' ></i>"; 


    //    sql = " select  top 1 VerifiedBy,VerifiedOn  from tbl_CrewPlanningCheckList where planningID=" + planningid + " and checkListMasterID=2 ";
    //    DataTable dtCheck1 = Budget.getTable(sql).Tables[0];
    //    if (dtCheck1.Rows.Count > 0)
    //    {
    //        lblCompletedByOn_NCC.Text = dtCheck1.Rows[0]["VerifiedBy"].ToString() + " / " + Common.ToDateString(dtCheck1.Rows[0]["VerifiedOn"]);
    //        if (Common.ToDateString(dtCheck1.Rows[0]["VerifiedOn"]) == "")
    //            litNewCrewChecklist.Text = "<i class='fa fa-exclamation fa-2x error' ></i>";
    //        else
    //            litNewCrewChecklist.Text = "<i class='fa fa-exclamation fa-2x success' ></i>";

    //        //imgNewCrewChecklist.Visible = true;
    //    }
    //    else
    //        litNewCrewChecklist.Text = "<i class='fa fa-exclamation fa-2x error' ></i>";

    //}

    //protected void btnUploadFile_OnClick(object sender, EventArgs e)
    //{
    //    if (fileUpload1.HasFile)
    //    {
    //        Common.Set_Procedures("sp_IU_CrewPlanningCargoUpload");
    //        Common.Set_ParameterLength(4);
    //        Common.Set_Parameters(
    //             new MyParameter("@PlanningIdint", planningid),
    //             new MyParameter("@Discription", txtDiscription.Text.Trim()),                 
    //             new MyParameter("@FileName", fileUpload1.FileName ),
    //             new MyParameter("@FileContent", fileUpload1.FileContent )
    //            );
    //        Boolean res;
    //        DataSet Ds = new DataSet();
    //        res = Common.Execute_Procedures_IUD_CMS(Ds);
    //        if(res)
    //        {
    //            txtDiscription.Text = "";
    //            BindUploadedFile();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "569", " alert('File could not be uploaded.') ", true);
    //        }

    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "560", " alert('Please select any file to uploaded.') ", true);
    //    }

    //}
    //protected void btnDownloaFile_Click(object sender, EventArgs e)
    //{
    //    ImageButton btn = (ImageButton)sender;
    //    int UPID = Common.CastAsInt32(btn.CommandArgument);
    //    HiddenField hdfFileName = (HiddenField)btn.FindControl("hdfFileName");

    //    try
    //    {
    //        string extension = Path.GetExtension(hdfFileName.Value).Substring(1);
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", hdfFileName.Value));
    //        Response.ContentType = "application/" + extension;
    //        Response.BinaryWrite(getFileAttachment(UPID));
    //    }
    //    catch { }

    //}
    //protected void btnDeleteUploadedFile_Click(object sender, EventArgs e)
    //{
    //    ImageButton btn = (ImageButton)sender;
    //    int UPID = Common.CastAsInt32(btn.CommandArgument);

    //    string sql = " delete from CrewPlanningCargoUpload where id="+UPID;
    //    Budget.getTable(sql);
    //    BindUploadedFile();
    //}    
    //public static byte[] getFileAttachment(int TableID)
    //{
    //    byte[] ret = null;

    //    string sql = " Select PlanningIdint,FileName,FileContent from CrewPlanningCargoUpload where id=" + TableID + "";
    //    DataSet RetValue = Budget.getTable(sql);


    //    if (RetValue.Tables[0].Rows.Count > 0)
    //    {
    //        ret = (byte[])RetValue.Tables[0].Rows[0]["FileContent"];
    //    }
    //    return ret;
    //}
    //public void BindUploadedFile()
    //{
    //    string sql = " Select  ROW_NUMBER()over(order by id)RowNo,id,PlanningIdint,Discription,FileName,FileContent from CrewPlanningCargoUpload where PlanningIdint=" + planningid+"";
    //    DataTable dtExp = Budget.getTable(sql).Tables[0];
    //    rptUploadedFiles.DataSource = dtExp;
    //    rptUploadedFiles.DataBind();
    //}

    // Approval List---------------------------------------------------------------------------
    
    public void SetLoginUserPosition()
    {
        LoginIDPosition = 0;
        string sql = " Select Position from DBO.Hr_PersonalDetails Where UserID ="+LoginId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            LoginIDPosition = Common.CastAsInt32(dt.Rows[0][0]);
        }
    }
    public bool ORAllowed()
    {
        bool ret = false;
        string sql = " select* from Hr_PersonalDetails where position=85 and office = 3 and UserID="+LoginId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            ret = true;
        }
        return ret;
    }
    
    public bool ApprovaVisibility(int ApprovalFwdTo,string Result,int ApprovalLevelID)
    {
        SetLoginUserPosition();
        bool ret = false;
        if (((ApprovalLevelID == 5 && ORAllowed()) || (ApprovalLevelID==4 && LoginIDPosition==89 ) || (ApprovalFwdTo == LoginId) || ((ApprovalFwdTo == -1) &&( LoginId==19 || LoginId==116))) && (Result==""))
        {
            ret = true;
        }

        return ret;
    }


}