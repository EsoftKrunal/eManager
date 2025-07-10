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

public partial class CrewApproval_ApplicantHomeNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        
        if (!IsPostBack)
        {
            for (int i = DateTime.Today.Year; i >= 2005; i--)
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            ShowData();
        }
    }
    protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ShowData();
    }
    
    protected void btn_PostClick(object sender, EventArgs e)
    {
        int OfficeId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        //string sql = "select isnull(Office,0) as Office,c.FirstName + ' ' + c.MiddleName + ' ' + c.LastName as UserName,count(*) as nor " +
        //            "from dbo.CandidatePersonalDetails c left " +
        //            "join DBO.Hr_PersonalDetails p " +
        //            "on c.ModifiedById = p.UserId where isnull(Office,0)=" + OfficeId.ToString() + 
        //            "group by isnull(Office, 0),c.FirstName + ' ' + c.MiddleName + ' ' + c.LastName";
        string sql = "";
        if(OfficeId>0)
        sql= "select a.userid,a.FirstName + ' ' + a.MiddleName + ' ' + a.FamilyName as UserName , " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.ModifiedById = a.userid AND ISNULL(D.STATUS, 1) = 1) AS Applicant, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.ModifiedById = a.userid AND ISNULL(D.STATUS, 1) = 2) AS 'Ready for Approval',  " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.ModifiedById = a.userid AND ISNULL(D.STATUS, 1)= 3) AS Approved, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.ModifiedById = a.userid AND ISNULL(D.STATUS, 1) = 4) AS Rejected, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.ModifiedById = a.userid AND ISNULL(D.STATUS, 1) = 5) AS Archived, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.ModifiedById = a.userid AND ISNULL(LastVesselId, 0) > 0) AS Employed, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.ModifiedById = a.userid) AS Total " +
                    "from DBO.Hr_PersonalDetails a " +
                    "where " +
                    "userid in( " +
                    "select modifiedbyid from " +
                    "dbo.CandidatePersonalDetails where  office=" + OfficeId + ") ORDER BY UserName";
        else
            sql = "select UserId,UserName , " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.CountryId = a.UserId AND ISNULL(D.STATUS, 1) = 1) AS Applicant, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.CountryId = a.UserId AND ISNULL(D.STATUS, 1) = 2) AS 'Ready for Approval',  " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.CountryId = a.UserId AND ISNULL(D.STATUS, 1)= 3) AS Approved, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.CountryId = a.UserId AND ISNULL(D.STATUS, 1) = 4) AS Rejected, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.CountryId = a.UserId AND ISNULL(D.STATUS, 1) = 5) AS Archived, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.CountryId = a.UserId AND ISNULL(LastVesselId, 0) > 0) AS Employed, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = " + OfficeId + " AND CreatedYear=" + ddlYear.SelectedValue + " AND D.CountryId = a.UserId) AS Total " +
                    "from ( select 70 as UserId,'India' as UserName union select 95,'Myanmar' union select 116,'Phillippines' union select 0,'Others' ) a ORDER BY UserName";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptdata1.DataSource = dt;
        rptdata1.DataBind();
    }
    public void ShowData()
    {
        string sql= "select OfficeId, Officename, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = a.OFFICEID AND CreatedYear=" + ddlYear.SelectedValue + " AND ISNULL(D.STATUS, 1) = 1) AS Applicant, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = a.OFFICEID AND CreatedYear=" + ddlYear.SelectedValue + " AND ISNULL(D.STATUS, 1) = 2) AS 'Ready for Approval', " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = a.OFFICEID AND CreatedYear=" + ddlYear.SelectedValue + " AND ISNULL(D.STATUS, 1)= 3) AS Approved, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = a.OFFICEID AND CreatedYear=" + ddlYear.SelectedValue + " AND ISNULL(D.STATUS, 1) = 4) AS Rejected, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = a.OFFICEID AND CreatedYear=" + ddlYear.SelectedValue + " AND ISNULL(D.STATUS, 1) = 5) AS Archived, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = a.OFFICEID AND CreatedYear=" + ddlYear.SelectedValue + " AND ISNULL(LastVesselId, 0) > 0) AS Employed, " +
                    "(SELECT COUNT(CandidateId) FROM DBO.VW_CPS D WHERE D.OFFICE = a.OFFICEID AND CreatedYear=" + ddlYear.SelectedValue + ") AS Total " +
                    "from " +
                    "(select OFFICEID,OFFICENAME from DBO.office " +
                    "UNION " +
                    "select 0,'WebSite') a";


        
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptData.DataSource = dt;
        rptData.DataBind();


    }   
}
