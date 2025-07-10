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
using System.Text;
using System.Xml;
public partial class CrewApproval_ApplicantApproval : System.Web.UI.Page
{
    int LoginId = 0;
    public int PageSlot
    {
        get { return Common.CastAsInt32(ViewState["PageSlot"]); }
        set { ViewState["PageSlot"] = value; }

    }
    public int MaxPageSlot
    {
        get { return Common.CastAsInt32(ViewState["MaxPageSlot"]); }
        set { ViewState["MaxPageSlot"] = value; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblmsg.Text = "";
        try
        {
            LoginId = Convert.ToInt32(Session["loginid"].ToString());
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Your session has been expired.\\n Please login again.');window.close();", true);
        } 
        if (!Page.IsPostBack)
        {
            PageSlot = 1;
            MaxPageSlot = 20;

            this.ddlNat.DataTextField = "CountryName";
            this.ddlNat.DataValueField = "CountryId";
            this.ddlNat.DataSource = Budget.getTable("selectNationality");
            this.ddlNat.DataBind();
            ddlNat.Items[0].Text = "<All>";

            this.ddlRank.DataTextField = "RankCode";
            this.ddlRank.DataValueField = "RankId";
            this.ddlRank.DataSource = Budget.getTable("Select RankId,RankCode from DBO.Rank where statusid='A' and RankId Not In(48,49) order By RankLevel");
            this.ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("<All>", "0"));

            string vesseltypes = ConfigurationManager.AppSettings["VesselType"].ToString();
            this.ddlVType.DataTextField = "VesselTypeName";
            this.ddlVType.DataValueField = "VesselTypeId";
            this.ddlVType.DataSource = Budget.getTable("Select VesselTypeId,VesselTypeName from DBO.VesselType Where VesselTypeid in ("+ vesseltypes + ") order By VesselTypeName");
            this.ddlVType.DataBind();
            ddlVType.Items.Insert(0, new ListItem("<All>", "0"));
            binddata();
        }
    }
    protected void UpdateList(object sender, EventArgs e)
    {
        binddata();
    }
    private void binddata()
    {
        string Query = "select Row_number() over(order by CANDIDATEID )RowNumber,candidateid, " +
                        " (SELECT FirstName+' '+LastName FROM DBO.UserMaster  where LoginId=case when Status=3 then AppBy when Status=4 then RejBy else '0' end)   as AppRejBy,ModifiedBy,  " +
                        " replace(convert(varchar,ModifiedOn,106) ,' ','-')as ModifiedOn, " +
                        "  case when Status = 3 then  replace(convert(varchar,AppOn,106) ,' ','-') when Status = 4 then replace(convert(varchar,RejOn,106) ,' ','-') else '' end as AppRejOn, " +
                    "(Select RankCode from dbo.rank where Rank.Rankid=cpd.RankAppliedId) as Rank, FirstName + ' ' + MiddleName + ' ' + Lastname as [Name],City,  " +
                    "(select NationalityCode from dbo.Country where countryid=cpd.nationalityid) as Country, " +
                    "isnull(FileName,'') as FileName,replace(convert(varchar,AvailableFrom,106) ,' ','-') as AvailableFrom, " +
                    "ContactNo,ContactNo2,EmailId,Status,StatusName=(case when isnull(Status,1)=1 then 'Applicant' when isnull(Status,1)=2 then 'Ready for Approval' when isnull(Status,1)=3 then 'Approved' when isnull(Status,1)=4 then 'Rejected' when isnull(Status,1)=5 then 'Archived' else '-' End), " +
                    "(SELECT TOP 1 CD.DISCTYPE FROM dbo.CANDIDATEDISCUSSION CD WHERE CD.CANDIDATEID=CPD.CANDIDATEID ORDER BY DISC_DATE DESC) AS DISCTYPE,  " +
                    "(SELECT TOP 1 REPLACE(CONVERT(VARCHAR,DISC_DATE,106),' ','-') + ' [ ' +  " +
                    "(SELECT USERID FROM DBO.USERLOGIN WHERE LOGINID=CD.CALLEDBY) + ' ] ' + replace(CD.discussion,'''''','''') FROM dbo.CANDIDATEDISCUSSION CD WHERE CD.CANDIDATEID=CPD.CANDIDATEID ORDER BY DISC_DATE DESC) AS DISCUSSION  " +
                    "from dbo.CandidatePersonalDetails cpd ";

        //string WhereClause = " Where isnull(Status,1) in (2,3,4) ";

        string WhereClause = " Where 1=1 ";

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
            WhereClause = WhereClause + " And vesseltypes like '%" + ddlVType.SelectedValue + ",%'";
        }
        if (txt_SignOn_Date.Text != "")
        {
            WhereClause = WhereClause + " And AvailableFrom>='" + txt_SignOn_Date.Text + "'";
        }
        if (txt_SignOff_Date.Text != "")
        {
            WhereClause = WhereClause + " And AvailableFrom<='" + txt_SignOff_Date.Text + "'";
        }
        if (ddlStatus.SelectedIndex > 0)
        {
            WhereClause = WhereClause + " And isnull(Status,1) =" + ddlStatus.SelectedValue + "";
        }
       
        if (txtIDName.Text.Trim() != "")
        {
            int Id = 0;
            try
            { Id = int.Parse(txtIDName.Text); }
            catch { }

            if (Id > 0)
                WhereClause = WhereClause + " And CandidateId =" + Id.ToString() + "";
            else
                WhereClause = WhereClause + " And FirstName like '%" + txtIDName.Text + "%'" +
                                            "OR MiddleName like '%" + txtIDName.Text + "%'" +
                                            "OR LastName like '%" + txtIDName.Text + "%'";
        }
        //WhereClause = WhereClause + " order by FirstName,MiddleName";
        
        try
        {
            //----------------------------------------
                 string finalsql = "SELECT count(*) FROM ( " + Query + WhereClause + " ) A ";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(finalsql);
            if (dt.Rows.Count > 0)
                MaxPageSlot = Common.CastAsInt32(Math.Ceiling((decimal)(Common.CastAsInt32(dt.Rows[0][0]) / 20)));

            //lblRcount51.Text = " ( " + Common.CastAsInt32(dt.Rows[0][0]).ToString() + " Records )";
            //----------------------------------------

            finalsql = "SELECT TOP 20 * FROM ( " + Query + WhereClause + " ) A WHERE RowNumber >" + (20 * (PageSlot - 1)).ToString() + " ORDER BY RowNumber";

            lblCounter.Text = " Records " + (((PageSlot - 1) * 20) + 1) + " - " + (((PageSlot) * 20));
            DataTable dtResult;
            this.rptData.DataSource = Budget.getTable(finalsql) ;
        }
        catch (Exception ex)
        {
            Response.Write("error  : " + ex.Message);
            Response.Write(Query + WhereClause);
        }
        this.rptData.DataBind();
        lblRCount.Text = "[ " + rptData.Items.Count.ToString() + " ] records found.";

    }
    //protected void DirectApprove(object sender, EventArgs e)
    //{
    //    int CandidateId =Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    try
    //    {
    //        Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET STATUS=3,APPBY=" + LoginId.ToString() + ",APPON=GETDATE() WHERE CANDIDATEID=" + CandidateId.ToString());
    //        this.lblmsg.Text = "Candidate approved successfully.";
    //    }
    //    catch
    //    {
    //        this.lblmsg.Text = "Unable to approve candidate.";
    //    }
    //    //Budget.getTable("EXEC dbo.DirectApprove_Candidate " + CandidateId.ToString() + "," + LoginId.ToString());
    //    //transfercandidatedata(CandidateId, LoginId);
    //    //lblmsg.Text = "Approved Successfully.";
    //}
    //protected void btnReject_Click(object sender, EventArgs e)
    //{
    //    int CandidateId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    try
    //    {
    //        Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET STATUS=4,REJBY=" + LoginId.ToString() + ",REJON=GETDATE() WHERE CANDIDATEID=" + CandidateId.ToString());
       
    //        Budget.getTable("UPDATE DBO.CandidatePersonalDetails " +
    //                                             "SET " +
    //                                             "STATUS=5,ARCHIVEBY=" + LoginId.ToString() + ",ARCHIVEON=GETDATE() " +
    //                                             "WHERE CANDIDATEID=" + CandidateId.ToString());
       
    //        this.lblmsg.Text = "Candidate rejected & archived successfully.";
    //    }
    //    catch
    //    {
    //        this.lblmsg.Text = "Unable to reject candidate.";
    //    }
    //}
    public void transfercandidatedata(int candidateid,int createdby)
    {
        Budget.getTable("EXEC DBO.TransferCandidateInformation " + candidateid.ToString() + "," + createdby); 
    }


    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (PageSlot > 1)
            PageSlot--;

        binddata();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (PageSlot < MaxPageSlot)
            PageSlot++;

        binddata();
    }
}
