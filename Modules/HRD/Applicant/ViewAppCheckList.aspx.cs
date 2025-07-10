using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Applicant_ViewAppCheckList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ShowData(Common.CastAsInt32(Request.QueryString["_C"]));
    }
    protected void ShowData(int candidateid)
    {
        DataTable dt = Budget.getTable("select Firstname,Middlename,Lastname, " +
                        "replace(convert(varchar,DateOfBirth,106) ,' ','-') as DOB, " +
                        "(select rankname from DBO.rank r where r.rankid=rankappliedid) as Rank, " +
                        "replace(convert(varchar,AvailableFrom,106) ,' ','-') as AvailableFrom, " +
                        "(select countryname from DBO.country c where c.countryid=nationalityid) as Nationality, " +
                        "ContactNo,ContactNo2,EmailId,VesselTypes,Address,City,PassportNo,FileName,isnull(status,1) as status,CreatedBy,CreatedOn,AppSentBy,AppSentOn,AppBy,AppOn,RejBy,RejOn,ArchiveBy,ArchiveOn,ApprovalId " +
                        "from DBO.candidatepersonaldetails where candidateid=" + candidateid.ToString()).Tables[0];
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                lblID.Text = candidateid.ToString();
                lblName.Text = dr["Firstname"].ToString() + " " + dr["Middlename"].ToString() + " " + dr["Lastname"].ToString();
                lblDOB.Text = dr["DOB"].ToString();
                lblPPNo.Text = dr["PassportNo"].ToString();
                lblNationality.Text = dr["Nationality"].ToString();
                lblRank.Text = dr["Rank"].ToString();
            }

        Repeater1.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (SELECT LICENSENAME FROM License L WHERE LICENSEID=DC.DocumentNameId) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=1 AND CANDIDATEID=" + candidateid.ToString());
        Repeater1.DataBind();

        Repeater2.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (select CourseName from CourseCertificate where CourseCertificateId=DC.DocumentNameId) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=2 AND CANDIDATEID=" + candidateid.ToString());
        Repeater2.DataBind();

        Repeater3.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (select CargoName from DangerousCargoEndorsement where cargoId=DC.DocumentNameId) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=3 AND CANDIDATEID=" + candidateid.ToString());
        Repeater3.DataBind();

        Repeater4.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (Case when DC.DocumentNameId=0 then 'Passport' when DC.DocumentNameId=1 then 'Visa' else 'Seaman-Book' end) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=4 AND CANDIDATEID=" + candidateid.ToString());
        Repeater5.DataBind();

        Repeater5.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CANDIDATEID,DOCUMENTTYPEID,DOCUMENTNAMEID, (select MedicaldocumentName from Medicaldocuments where MedicaldocumentId=DC.DocumentNameId) AS DOCUMENTNAME,STATUS,REMARK FROM DBO.Candidate_Doc_CheckList DC WHERE DOCUMENTTYPEID=5 AND CANDIDATEID=" + candidateid.ToString());
        Repeater5.DataBind();

        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT (SELECT FIRSTNAME+' '+LASTNAME FROM USERLOGIN WHERE LOGINID=CHECKEDBY) AS CHECKEDBY,CHECKEDON from DBO.Candidate_Doc_CheckList CDC WHERE CDC.CANDIDATEID=" + candidateid.ToString());
        if (DT.Rows.Count > 0)
        {
           // lblMess.Text = DT.Rows[0]["CHECKEDBY"].ToString() + " / " + Common.ToDateString(DT.Rows[0]["CHECKEDON"].ToString());
        }
    }
}