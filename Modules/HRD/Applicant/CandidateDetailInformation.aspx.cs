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
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Transactions;
using System.Activities.Expressions;
using System.EnterpriseServices;
using System.Runtime.Remoting.Messaging;

public partial class CandidateDetailInformation : System.Web.UI.Page
{
    public int PageNo
    {
        set { ViewState["PageNo"] = value; }
        get { return int.Parse("0" + ViewState["PageNo"]); }
    }
    public int PagesSlot
    {
        set { ViewState["PagesSlot"] = value; }
        get { return int.Parse("0" + ViewState["PagesSlot"]); }
    }
    public int TotalPages
    {
        set { ViewState["TotalPages"] = value; }
        get { return int.Parse("0" + ViewState["TotalPages"]); }
    }

    string BCCMail;
    string ToMail;
    string UserFullName;
    string crewFullName;
    string crewRank;
    string EmailBody;
    Int32 candidateId;
    string EmailSubject;
    string randomPwd;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_License_Message.Text = "";
        if (!Page.IsPostBack)
        {
            this.ddlNat.DataTextField = "CountryName";
            this.ddlNat.DataValueField = "CountryId";
            this.ddlNat.DataSource = Budget.getTable("SELECT CountryId,CountryName FROM COUNTRY with(nolock)").Tables[0];
            this.ddlNat.DataBind();
            ddlNat.Items[0].Text = "<All>";

            this.ddlRank.DataTextField = "RankCode";
            this.ddlRank.DataValueField = "RankId";
            this.ddlRank.DataSource = Budget.getTable("Select RankId,RankCode from DBO.Rank with(nolock) where statusid='A' and RankId Not In(48,49) order By RankLevel").Tables[0];
            this.ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("<All>", "0"));

            string vesseltypes = ConfigurationManager.AppSettings["VesselType"].ToString();
            this.ddlVType.DataTextField = "VesselTypeName";
            this.ddlVType.DataValueField = "VesselTypeId";
            this.ddlVType.DataSource = Budget.getTable("Select VesselTypeId,VesselTypeName from DBO.VesselType with(nolock) Where VesselTypeid in ("+ vesseltypes + ") order By VesselTypeName").Tables[0];
            this.ddlVType.DataBind();
            ddlVType.Items.Insert(0, new ListItem("<All>", "0"));

            this.ddlOffice.DataTextField = "OfficeName";
            this.ddlOffice.DataValueField = "OfficeId";
            this.ddlOffice.DataSource = Budget.getTable("Select * from dbo.office with(nolock) where officeId > 0 order by OfficeName").Tables[0];
            this.ddlOffice.DataBind();

            ddlOffice.Items.Insert(0, new ListItem("<All>", ""));
            ddlOffice.Items.Add( new ListItem("WebSite", "0"));

            //BindCreatedBy();

            if (Request.QueryString["OfficeId"] != null)
                ddlOffice.SelectedValue = Request.QueryString["OfficeId"].ToString();

            if (Request.QueryString["Status"] != null)
                ddlStatus.SelectedValue = Request.QueryString["Status"].ToString();

            //if (Request.QueryString["CID"] != null)
            //    ddlCreatedBy.SelectedValue = Request.QueryString["CID"].ToString();

            
            binddata();
        }
    }
    protected void UpdateList(object sender, EventArgs e)
    {
     try
        {
            binddata();
        }
        catch { }
        
    }
    private void binddata()
    {
        int MaxRecordsCount=22;
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        string Query = "select Row_number() over(order by CANDIDATEID )RowNumber,candidateid,isnull(ModifiedBy,'WebSite') as ModifiedBy,ModifiedOn, " +
                       "(Select RankCode from dbo.rank where Rank.Rankid=cpd.RankAppliedId) as Rank, " +
                       "FirstName + ' ' + MiddleName + ' ' + Lastname as [Name],City, " +
                       "(select NationalityCode from dbo.Country where countryid=cpd.nationalityid) as Country,isnull(FileName,'') as FileName,replace(convert(varchar,AvailableFrom,106) ,' ','-') as AvailableFrom,ContactNo,ContactNo2,EmailId," +
                       "Status,StatusName=(case " + 
                       "when isnull(Status,1)=1 then 'Applicant' " +
                       "when isnull(Status,1)=2 then 'Awaiting Manning Approval' " +
                       "when isnull(Status,1)=3 then 'Ready for Proposal' " +
                       "when isnull(Status,1)=4 then 'Changes to Manning Rejected' " +
                       "when isnull(Status,1)=5 then 'Archived' " +
                       "else '-' End),(SELECT TOP 1 CD.DISCTYPE FROM dbo.CANDIDATEDISCUSSION CD WHERE CD.CANDIDATEID=CPD.CANDIDATEID ORDER BY DISC_DATE DESC) AS DISCTYPE, " +
                       "(SELECT TOP 1 REPLACE(CONVERT(VARCHAR,DISC_DATE,106),' ','-') + ' [ ' + (SELECT USERID FROM DBO.USERLOGIN WHERE LOGINID=CD.CALLEDBY) + ' ] ' + replace(CD.discussion,'''''','''')" +   
                       " FROM dbo.CANDIDATEDISCUSSION CD WHERE CD.CANDIDATEID=CPD.CANDIDATEID ORDER BY DISC_DATE DESC) AS DISCUSSION, " +
                        " (Select top 1 CASE WHEN Isnull(PTO_OwnerAppRej,'') = 'A'  then 'Client Approved' WHEN Isnull(PTO_OwnerAppRej,'') = 'R'  then 'Client Rejected' WHEN Isnull(PTO_OwnerAppRej,'') = 'W'  then 'Withdraw Proposal'  ELSE 'Awaiting Client Approval'  END from ProposalToOwner with(nolock) where PTO_CandidateId = cpd.CandidateId) As ClientStatus, " +
                        " (Select CrewNumber from CrewPersonalDetails cp with(nolock) where cp.CandidateId = cpd.candidateid)  As CrewNumber, '"+ appname + "' As AppName " +
                       "from dbo.CandidatePersonalDetails cpd ";

        string WhereClause = " Where 1=1 and cpd.candidateid not in (Select CandidateId from CrewPersonalDetails with(nolock) where isnull(CandidateId,0) <> 0)  ";

        if (ddlNat.SelectedIndex > 0)
        {
            WhereClause = WhereClause + " And cpd.nationalityid=" + ddlNat.SelectedValue;  
        }
        if (ddlRank.SelectedIndex > 0)
        {
            WhereClause = WhereClause + " And cpd.RankAppliedId=" + ddlRank.SelectedValue;
        }
        if (ddlVType.SelectedIndex > 0)
        {
            WhereClause = WhereClause + " And vesseltypes like '%" + ddlVType.SelectedValue + "%'";
        }
         if (ddlStatus.SelectedIndex > 0)
        {
            WhereClause = WhereClause + " And isnull(Status,1) =" + ddlStatus.SelectedValue + "";
        }
        if (ddlOffice.SelectedIndex > 0)
        {
         if (ddlOffice.SelectedValue == "0")
            {
                WhereClause = WhereClause + " And cpd.CreatedBy = 0 ";
            }
         else
            {
                WhereClause = WhereClause + " And cpd.CreatedBy In (Select LoginId from UserLogin where RecruitingOfficeId like '%" + ddlOffice.SelectedValue + "%') ";
            }
           
        }
        //if (ddlCreatedBy.SelectedIndex > 0)
        //{
        //    WhereClause = WhereClause + " And cpd.ModifiedById='"+ddlCreatedBy.SelectedValue+"'";
        //}

        if (txt_SignOn_Date.Text  !="")
        {
            WhereClause = WhereClause + " And AvailableFrom>='" + txt_SignOn_Date.Text + "'";
        }
        if (txt_SignOff_Date.Text != "")
        {
            WhereClause = WhereClause + " And AvailableFrom<='" + txt_SignOff_Date.Text + "'";
        }
        if (txtcity.Text.Trim()!= "")
        {
            WhereClause = WhereClause + " And City Like '%" +txtcity.Text + "%'";
        }
        if (txtPassportNo.Text.Trim() != "")
        {
            WhereClause = WhereClause + " And PassportNo Like '%" + txtPassportNo.Text + "%'";
        }
        
        if (txtIDName.Text.Trim()!="")
        {
            int Id = 0;
            try
            {   Id = int.Parse(txtIDName.Text);}
            catch { }

            if (Id >0)
                WhereClause = WhereClause + " And CandidateId =" + Id.ToString() + "";
            else
                WhereClause = WhereClause + " And FirstName like '%" + txtIDName.Text + "%'" +
                                            "OR MiddleName like '%" + txtIDName.Text + "%'" +
                                            "OR LastName like '%" + txtIDName.Text + "%'";
        }
        WhereClause = WhereClause + " order by candidateid";

            int StartRow=0,EndRow=0;
        if (PageNo != 0)
            StartRow = (MaxRecordsCount * (PageNo - 1)) + 1;
        else
            StartRow = 1;
        EndRow = StartRow + MaxRecordsCount;
        DataTable dtCandidateDetails = Budget.getTable(Query + WhereClause).Tables[0];
        DataView dtFiltered = dtCandidateDetails.DefaultView;
        dtFiltered.RowFilter = "RowNumber>="+StartRow+" and RowNumber<"+EndRow+"";


        TotalPages = dtCandidateDetails.Rows.Count / MaxRecordsCount;
        if (dtCandidateDetails.Rows.Count > TotalPages * MaxRecordsCount)
            TotalPages =TotalPages + 1;

        BindPageRepeater();       


            //DataRow[] Dr;
            //Dr = dtCandidateDetails.("RowNumber>=0 and RowNumber<=10");
            //for(int i=0;i<=Dr .Length;i++)
            //{
            //    dtFiltered.Rows.Add(Dr[i]);
            //}

            try
            {
                this.rptData.DataSource = dtFiltered;
            }
            catch (Exception ex)
            {
                Response.Write("error  : " + ex.Message);
                Response.Write(Query + WhereClause);
            }
        this.rptData.DataBind();
        lblRCount.Text = "[ " + dtCandidateDetails.Rows.Count.ToString() + " ] records found.";    
        
   }
    protected void gvcandidate_PreRender(object sender, EventArgs e)
    {
        if (rptData.Items.Count == 0)
        {
            this.lbl_License_Message.Text = "No Record Found...";
        }
        else
        {
            this.lbl_License_Message.Text = "";
        }
    }
    protected void Open_Candidate(object sender, EventArgs e)
    {
        ImageButton ib=((ImageButton)sender);
        int candidateid=Convert.ToInt32(ib.CommandArgument);
        // Response.Redirect("CandidateDetailPopUp.aspx?candidate=" + candidateid.ToString() + "&M=" + ib.ToolTip);
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('CandidateDetailPopUp.aspx?candidate=" + candidateid.ToString() + "&M=" + ib.ToolTip + "','','');", true);
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('CandidateDetailPopUp.aspx?candidate=" + candidateid.ToString() + "&M=App','','');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('ShipjobAppDetailPopUp.aspx?candidate=" + candidateid.ToString() + "&M=App','','');", true);
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ABC", "SetLastFocus('dvscroll_cdinfo');", true);
    }
    protected void lnPageNumber_OnClick(object sender, EventArgs e)
    {
        LinkButton lnPageNumber = (LinkButton)sender;
        PageNo = Common.CastAsInt32(lnPageNumber.Text);
        binddata();
    }
    protected void lnkNext20Pages_OnClick(object sender, EventArgs e)
    {
        PageNo = 0;
        PagesSlot = PagesSlot + 1;
        if (TotalPages < ((PagesSlot * 20) + 20))
            lnkNext20Pages.Visible = false;

        if (PagesSlot != 0)
            lnkPrev20Pages.Visible = true;

        BindPageRepeater();
    }
    protected void lnkPrev20Pages_OnClick(object sender, EventArgs e)
    {
        PageNo = 0;
        if (PagesSlot > 0)
            PagesSlot = PagesSlot - 1;

        if (TotalPages > ((PagesSlot * 20) + 20))
            lnkNext20Pages.Visible = true;
        if (PagesSlot == 0)
            lnkPrev20Pages.Visible = false;
            
        BindPageRepeater();
    }
    public void BindPageRepeater()
    {
        DataTable DtPages = new DataTable();
        int i = 1;
        int StartRowNumber = 0,EndRowNumber=0;
        StartRowNumber = (PagesSlot * 20) + 1;

        EndRowNumber = StartRowNumber + 19;
        if (EndRowNumber > TotalPages)
            EndRowNumber = TotalPages;
            

        DtPages.Columns.Add("PageNumber", typeof(int));

        for (i = StartRowNumber; i <= EndRowNumber; i++)
        {
            DtPages.Rows.Add(i.ToString());
        }
        rptPageNumber.DataSource = DtPages;
        rptPageNumber.DataBind();
    }

    //public void BindCreatedBy()
    //{
    //    string sql = "";
    //    if (ddlOffice.SelectedIndex==0)
    //     sql = " select userid,firstname + ' ' + middlename + ' ' + familyname as Name from Hr_PersonalDetails where drc is null order by Name"; 
    //    else
    //        sql = " select userid,firstname + ' ' + middlename + ' ' + familyname as Name from Hr_PersonalDetails where drc is null and userid in (select distinct ModifiedById from vw_cps where office=" + ddlOffice.SelectedValue + ") order by Name";

    //    DataTable dt = Budget.getTable(sql).Tables[0];
    //    ddlCreatedBy.DataSource = dt;
    //    ddlCreatedBy.DataTextField = "Name";
    //    ddlCreatedBy.DataValueField= "userid";
    //    ddlCreatedBy.DataBind();
    //    ddlCreatedBy.Items.Insert(0, new ListItem("All", "0"));
    //}

    protected void btnNewApplicant_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('NewAppEntry.aspx','','');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('ShipjobAppform.aspx','','');", true);
    }

    protected void ProposalToOwner(object sender, EventArgs e)
    {
        try
        { 
            lblMessage.Text = "";
            LinkButton ib = ((LinkButton)sender);
            candidateId = 0;
            candidateId = Convert.ToInt32(ib.CommandArgument);
            hdnCandidateId.Value = Convert.ToString(candidateId);
            dvProposalToOwner.Visible = true;
            BindDDlOwnList();
            //BindDDlVessel();
            GetloginIdDetail();
            GetCompanyDetail();
            GetProposalDetails(candidateId);
            GetMaildetail();
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "abc", "window.open('ProposalSendOwner.aspx?candidate=" + candidateid.ToString() + "','','');", true);
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void GetProposalToOwnerDetail(int candidateId)
    {
        string sql = "Select top 1 * from ProposalToOwner with(nolock) where PTO_CandidateId = " + candidateId + " Order by PTO_Id Desc";
        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            ddl_Ownerlist.SelectedValue = dt.Rows[0]["PTO_OwnerId"].ToString();
            txtFromAddress.Text = dt.Rows[0]["PTO_FromEmail"].ToString();
            txtToEmail.Text = dt.Rows[0]["PTO_ToEmail"].ToString();
            txtBCCEmail.Text = dt.Rows[0]["PTO_BCCEmail"].ToString();
            txtSubject.Text = dt.Rows[0]["PTO_Subject"].ToString();
            litMessage.Text = dt.Rows[0]["PTO_Body"].ToString();
            randomPwd = dt.Rows[0]["PTO_RandomPassword"].ToString();
            txtManningOfficerRemarks.Text = dt.Rows[0]["PTO_OfficerRemarks"].ToString();
            ddl_Vessel.SelectedValue = dt.Rows[0]["PTO_VesselId"].ToString();
        }
    }

    protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField statusName = e.Item.FindControl("hdfStatusName") as HiddenField;
            HiddenField clientStatus = e.Item.FindControl("hdnClientStatus") as HiddenField;
            LinkButton lkbutton = e.Item.FindControl("lbProposalToOwner") as LinkButton;
            LinkButton lkResendProposal = e.Item.FindControl("lbResendProposal") as LinkButton;
            LinkButton lbWithDraw = e.Item.FindControl("lbWithDraw") as LinkButton;
            Label lblSlase = e.Item.FindControl("lblslase") as Label;
            HiddenField crewnumber = e.Item.FindControl("hdnCrewNumber") as HiddenField; 
            if (statusName.Value.Trim().ToUpper() == "READY FOR PROPOSAL" && string.IsNullOrWhiteSpace(crewnumber.Value))
            {
                lkbutton.Visible = true;
                if (!string.IsNullOrWhiteSpace(clientStatus.Value) && clientStatus.Value != "" && (clientStatus.Value.Trim().ToUpper() == "AWAITING CLIENT APPROVAL" || clientStatus.Value.Trim().ToUpper() == "CLIENT APPROVED"))
                {
                    lkbutton.Visible = false;
                    lkResendProposal.Visible = true;
                    lbWithDraw.Visible = true;
                    lblSlase.Visible = true;
                }  
            }
            else
            {
                lkbutton.Visible = false;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvProposalToOwner.Visible = false;
        ddl_Ownerlist.SelectedIndex = 0;
        //ddl_Vessel.SelectedIndex = 0;
        txtFromAddress.Text = "";
        txtToEmail.Text = "";
        txtBCCEmail.Text = "";
        litMessage.Text = "";
        txtManningOfficerRemarks.Text = "";
        txtSubject.Text = "";
        binddata();
        
    }

    protected void btnSendProposal_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Ownerlist.SelectedValue == "0")
            {
                lblMessage.Text = "Please select Owner name."; return;
            }
            if (ddl_Vessel.SelectedValue == "0")
            {
            lblMessage.Text = "Please select vessel."; return;
            }

            if (txtManningOfficerRemarks.Text == "" || string.IsNullOrWhiteSpace(txtManningOfficerRemarks.Text))
            {
            lblMessage.Text = "Please fill Manning officer remarks."; return;
            }

            if (txtToEmail.Text == "" || string.IsNullOrWhiteSpace(txtToEmail.Text))
            {
            lblMessage.Text = "Please fill To Email field."; return;
            }

            if (txtSubject.Text.Trim() == "")
            {
            lblMessage.Text = "Please fill subject field."; return;
            }



            char[] Sep = { ';' };
            string[] ToAdds = txtToEmail.Text.Split(Sep);
            //string[] ToAdds = {"pankaj.k@esoftech.com","asingh@energiossolutions.com"};
            string[] CCAdds = {""};
            string[] BCCAdds = txtBCCEmail.Text.Split(Sep);
            //------------------
            string ErrMsg = "";
            string AttachmentFilePath = "";
            Int32 ownerid = Convert.ToInt32(ddl_Ownerlist.SelectedValue);
            Int32 vesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
            string officerremarks = txtManningOfficerRemarks.Text.Trim();
            Int32 loginId = Common.CastAsInt32(Session["loginid"]);
            string mailStatus = "";
            Boolean OwnerAppRejStatus = false;
            DataTable Dt = Budget.getTable(" Select PTO_MailStatus,PTO_OwnerAppRej  from ProposalToOwner with(nolock) where PTO_CandidateId =" + hdnCandidateId.Value.ToString()).Tables[0];

            if (Dt.Rows.Count > 0)
            {
                mailStatus = Dt.Rows[0]["PTO_MailStatus"].ToString();
                if (Dt.Rows[0]["PTO_OwnerAppRej"].ToString() == "W" || Dt.Rows[0]["PTO_OwnerAppRej"].ToString() == "R")
                {
                    OwnerAppRejStatus = true;
                }
            }
            if (mailStatus != "S" || (hdnResendProposal.Value == "true") || OwnerAppRejStatus)
            {
                insertProposalToOwnerDetails("InsertProposalToOwner",
                                                      Convert.ToInt32(hdnCandidateId.Value),
                                                      ownerid,
                                                      officerremarks,
                                                      txtFromAddress.Text.Trim(),
                                                      txtToEmail.Text.Trim(),
                                                      txtBCCEmail.Text.Trim(),
                                                      txtSubject.Text.Trim(),
                                                      litMessage.Text.Trim(),
                                                      loginId,
                                                      hdnRandomPwd.Value.Trim(), vesselId);

                if (SendEmail.SendeMail(Common.CastAsInt32(Session["loginid"]), txtFromAddress.Text.Trim(), txtFromAddress.Text.Trim(), ToAdds, CCAdds, BCCAdds, txtSubject.Text.Trim(), litMessage.Text.Trim(), out ErrMsg, AttachmentFilePath))
                {
                    Budget.getTable("UPDATE DBO.ProposalToOwner SET PTO_MailStatus='S' WHERE PTO_CandidateId=" + hdnCandidateId.Value.ToString());
                    lblMessage.Text = "Mail sent successfully.";
                    dvProposalToOwner.Visible = false;
                    UpdateList(sender, e);
                }
                else
                {
                    Budget.getTable("UPDATE DBO.ProposalToOwner SET PTO_MailStatus='F' WHERE PTO_CandidateId=" + hdnCandidateId.Value.ToString());
                    lblMessage.Text = "Unable to send Mail. Error : " + ErrMsg;
                }
                hdnResendProposal.Value = "false";
            }
            else
            {
                lblMessage.Text = "Mail already sent.";
            }
         
        }
        catch (SystemException ex)
        {
            this.lblMessage.Text = "Unable to send mail : " + ex.Message;
        }
    }

    private void BindDDlOwnList()
    {
        string sql = "";
        sql = " select OwnerId,OwnerName from Owner with(nolock) where StatusId = 'A' order by OwnerName";
        DataTable dt = Budget.getTable(sql).Tables[0];
        ddl_Ownerlist.DataSource = dt;
        ddl_Ownerlist.DataTextField = "OwnerName";
        ddl_Ownerlist.DataValueField = "OwnerId";
        ddl_Ownerlist.DataBind();
        ddl_Ownerlist.Items.Insert(0, new ListItem(" < Select > ", "0"));
    }

    private void BindDDlVessel( int OwnerId)
    {
        string sql = "";
        sql = " Select VesselId, VesselName from Vessel with(nolock) where StatusId = 'A' and OwnerId = " + OwnerId  + " and VesselStatusId in (1,3) order by VesselName ";
        DataTable dt = Budget.getTable(sql).Tables[0];
        ddl_Vessel.DataSource = dt;
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem(" < Select > ", "0"));
    }

    private void GetloginIdDetail()
    {
        BCCMail = "";
        UserFullName = "";
        string sql = " Select Email, FirstName + ' ' + LastName As Full_Name  from UserLogin with(nolock) where LoginId = " + Session["loginid"].ToString();
         DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
           BCCMail = dt.Rows[0]["Email"].ToString().Trim();
           UserFullName = dt.Rows[0]["Full_Name"].ToString().Trim();
        }
    }

    private void GetCompanyDetail()
    {
        DataTable dtCompany = Budget.getTable("Select top 1 CompanyName from Company with(nolock) where DefaultCompany='Y' and  StatusId = 'A' ").Tables[0];
        if (dtCompany.Rows.Count > 0)
        {
            hdnCompany.Value = dtCompany.Rows[0]["CompanyName"].ToString().Trim();
        }
    }

    protected void ddl_Ownerlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        { 
            ToMail = "";
            string sql = " Select Mail1, Mail2 from Owner with(nolock) where StatusId = 'A' and OwnerId = " + ddl_Ownerlist.SelectedValue.ToString();
            DataTable dt = Budget.getTable(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string mail1 = "";
                string mail2 = "";
                foreach (DataRow dr in dt.Rows)
                {
                    mail1 = dr["Mail1"].ToString().Trim();
                    mail2 = dr["Mail2"].ToString().Trim();
                }

                ToMail = mail1 + ";" + mail2;
                txtToEmail.Text = ToMail;
            }
            BindDDlVessel(Convert.ToInt32(ddl_Ownerlist.SelectedValue));
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    private void GetMaildetail()
    {
        //string sql, sql1;
        string strcandidateId = Convert.ToString(candidateId);
        EmailBody = "";
        EmailSubject = "";
        randomPwd = "";
        //Int32 processid = 0;
        //sql1 = "Select EmailProId from [dbo].[EmailProcess] with(nolock) where EmailProStatus = 'A' and  EmailProName = 'Proposal To Owner' ";
        //DataTable dtProcess = Budget.getTable(sql1).Tables[0];
        //if (dtProcess.Rows.Count > 0)
        //{
        //    processid = Convert.ToInt32(dtProcess.Rows[0]["EmailProId"]);
        //}

        DataTable dtRanPwd = Budget.getTable("EXEC DBO.GENEREATERamdomPassword " + strcandidateId + " ").Tables[0];

        if (dtRanPwd.Rows.Count > 0)
        {
            randomPwd = dtRanPwd.Rows[0]["RandomPwd"].ToString();
            hdnRandomPwd.Value = randomPwd;
        }
        //sql = "Select top 1 SMC_Frommail,SMC_BCC, SMC_Body, SMC_Subject  from SendMailConfiguration with(nolock) where SMC_StatusId = 'A' and SMC_ProcessId = '" + processid.ToString() +"'";
        //DataTable dt = Budget.getTable(sql).Tables[0];


        //if (dt.Rows.Count > 0)
        //{
            txtFromAddress.Text = ConfigurationManager.AppSettings["FromAddress"];
        //    if (BCCMail != "")
        //    {
        //        BCCMail = BCCMail +";"+ dt.Rows[0]["SMC_BCC"].ToString();
        //        txtBCCEmail.Text = BCCMail;
        //    }
        //    else
        //    {
        //        txtBCCEmail.Text = dt.Rows[0]["SMC_BCC"].ToString();
        //    }
        //    EmailBody = dt.Rows[0]["SMC_Body"].ToString();
        //    EmailBody = string.Format(EmailBody, candidateId, crewFullName, crewRank, randomPwd, UserFullName);
        //    txtEmailBody.Text = EmailBody;
        //    EmailSubject = dt.Rows[0]["SMC_Subject"].ToString(); 
        //    txtSubject.Text = EmailSubject;
        //}

        txtSubject.Text = "Crew Approval Request From eMANAGER";
        string MailContent = System.IO.File.ReadAllText(Server.MapPath("~/Modules/HRD/Applicant/SendProposaltoClient.htm"));
        string SendProposalMailURL = ConfigurationManager.AppSettings["SendProposalMail"];

        string URl = SendProposalMailURL + candidateId.ToString();

        MailContent = MailContent.Replace("$SendProposalLINK$", URl);
        MailContent = MailContent.Replace("$ProposalId$", strcandidateId);
        MailContent = MailContent.Replace("$crewFullName$", crewFullName);
        MailContent = MailContent.Replace("$crewRank$", crewRank);
        MailContent = MailContent.Replace("$randomPwd$", randomPwd);
        MailContent = MailContent.Replace("$UserFullName$", UserFullName);
        litMessage.Text = MailContent;
    }

    private void GetProposalDetails(Int32 CandidateId)
    {
        crewFullName = "";
        crewRank = "";
        string sql = " Select cd.CandidateId, cd.FirstName + ' ' + cd.MiddleName + ' ' + cd.LastName As Crew_FullName, (Select RankName + '(' + RankCode +')' from Rank with(nolock) where RankId = cd.RankAppliedId and StatusId = 'A' ) As Rank from CandidatePersonalDetails cd with(nolock) where cd.CandidateId = '" + CandidateId.ToString() + "'";
        DataTable dt = Budget.getTable(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            crewFullName = dt.Rows[0]["Crew_FullName"].ToString();
            crewRank = dt.Rows[0]["Rank"].ToString();
        }
    }

    public void insertProposalToOwnerDetails(string _strProc, Int32 _candidateId, Int32 _ownerId, string _OfficerRemarks, string _FromEmail, string _ToEmail, string _BCCEmail, string _Subject, string _body, Int32 _createdby, string _randompwd, Int32 _vesselId)
    {
        Database oDatabase = DatabaseFactory.CreateDatabase();

        DbCommand odbCommand = oDatabase.GetStoredProcCommand(_strProc);
        oDatabase.AddInParameter(odbCommand, "@CandidateId", DbType.Int32, _candidateId);
        oDatabase.AddInParameter(odbCommand, "@ownerId", DbType.Int32, _ownerId);
        oDatabase.AddInParameter(odbCommand, "@officerremarks", DbType.String, _OfficerRemarks);
        oDatabase.AddInParameter(odbCommand, "@fromEmail", DbType.String, _FromEmail);
        oDatabase.AddInParameter(odbCommand, "@ToEmail", DbType.String, _ToEmail);
        oDatabase.AddInParameter(odbCommand, "@BCCEmail", DbType.String, _BCCEmail);
        oDatabase.AddInParameter(odbCommand, "@subject", DbType.String, _Subject.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@body", DbType.String, _body.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@createdby", DbType.Int32, _createdby);
        oDatabase.AddInParameter(odbCommand, "@randomPwd", DbType.String, _randompwd.ToString().Trim());
        oDatabase.AddInParameter(odbCommand, "@vesselId", DbType.Int32, _vesselId);
        using (TransactionScope scope = new TransactionScope())
        {
            try
            {
                // execute command and get records
                oDatabase.ExecuteNonQuery(odbCommand);
                scope.Complete();
            }
            catch (Exception ex)
            {
                // if error is coming throw that error
                throw ex;
            }
            finally
            {
                // after used dispose commmond            
                odbCommand.Dispose();
            }
        }
    }
    protected void lbResendProposal_click(object sender, EventArgs e)
    {
        try
        {
            hdnResendProposal.Value = "true";
            LinkButton ib = ((LinkButton)sender);
            candidateId = 0;
            candidateId = Convert.ToInt32(ib.CommandArgument);
            hdnCandidateId.Value = Convert.ToString(candidateId);
            dvProposalToOwner.Visible = true;
            BindDDlOwnList();
            //BindDDlVessel();
            GetProposalToOwnerDetail(candidateId);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void lbWithDraw_click(object sender, EventArgs e)
    {
        try
        {
            LinkButton ib = ((LinkButton)sender);
            candidateId = 0;
            candidateId = Convert.ToInt32(ib.CommandArgument);
            hdnCandidateId.Value = Convert.ToString(candidateId);
            string strwithDraw = "W";
            Budget.getTable("UPDATE DBO.ProposalToOwner SET PTO_OwnerAppRej='" + strwithDraw + "' , PTO_OwnerAppRejDate = GETDATE() WHERE PTO_CandidateId=" + hdnCandidateId.Value.ToString() + " and PTO_StatusId = 'A' ");
            binddata();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Crew proposal withdraw successfully.');", true);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }



    protected void ddl_Vessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddl_Vessel.SelectedValue) > 0)
            {
                if (! string.IsNullOrWhiteSpace(txtSubject.Text))
                {
                    txtSubject.Text = string.Format(txtSubject.Text, ddl_Vessel.SelectedItem.Text, hdnCompany.Value);
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void ibClose_Click(object sender, ImageClickEventArgs e)
    {
        btnCancel_Click(sender, e);
    }
}
