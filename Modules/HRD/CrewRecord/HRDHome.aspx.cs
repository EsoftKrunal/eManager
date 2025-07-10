using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class HRDHome : System.Web.UI.Page
{

    string sql1 = "SELECT CREWNUMBER,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CREWNAME, VESSELCODE,R.RANKCODE,ETA,PORTNAME,CREWFLAG " +
                  "from portcalldetail DET " +
                  "INNER JOIN portcallheader MST ON MST.PORTCALLID=DET.PORTCALLID AND MST.STATUS='O' AND ETA<='" + DateTime.Today.AddDays(30).ToString("dd-MMM-yyyy") + "' " +
                  "INNER JOIN VESSEL V ON MST.VESSELID=V.VESSELID " +
                  "INNER JOIN CREWPERSONALDETAILS C ON C.CREWID=DET.CREWID " +
                  "INNER JOIN RANK R ON R.RANKID=C.CURRENTRANKID " +
                  "INNER JOIN PORT P ON P.PORTID=MST.PORTID ORDER BY VESSELCODE,CREWFLAG ";

    string sql5 = "SELECT VESSELCODE,CREWNUMBER,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CREWNAME, R.RANKCODE, RELIEFDUEDATE, " +
                  "(SELECT CREWNUMBER + ' ' + FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME FROM CREWPERSONALDETAILS CC WHERE CC.CREWID=C.FIRSTRELIEVERID) as RELIEVER, " +
          "PORTCALL=(SELECT H.PortReferenceNumber FROM PORTCALLHEADER H INNER JOIN PORTCALLDETAIL D ON H.PORTCALLID=D.PORTCALLID AND H.STATUS='O' AND H.VESSELID=C.CURRENTVESSELID AND CREWID=C.CREWID) " +
                  "FROM CREWPERSONALDETAILS C " +
                  "INNER JOIN RANK R ON R.RANKID=C.CURRENTRANKID " +
                  "INNER JOIN VESSEL V ON C.CURRENTVESSELID=V.VESSELID " +
                  "WHERE CREWSTATUSID=3 AND RELIEFDUEDATE<='" + DateTime.Today.AddDays(30).ToString("dd-MMM-yyyy") + "' ORDER BY VESSELCODE,RANKLEVEL ";

    string sql6 = "SELECT * FROM " +
                  "( " +
                  "SELECT VESSELNAME, " +
                  "(SELECT SUM(BUDGETMANNING) FROM VESSELBUDGETMANNING VBM WHERE VBM.VESSELID=V.VESSELID) AS BUDGETCOUNT, " +
                  "(SELECT SUM(SAFEMANNING) FROM (SELECT RankLevel,RANKCODE AS Grade, (SELECT COUNT(*) FROM CREWPERSONALDETAILS CPD WHERE CPD.crewstatusid=3 AND CPD.currentvesselid=V.VESSELID AND RANK.RANKID=CPD.CURRENTRANKID) as SafeManning FROM RANK) AA WHERE SAFEMANNING > 0  ) AS ACTUALCOUNT " +
                  "FROM  " +
                  "DBO.VESSEL V  " +
                  ") " +
                  "A " +
                  "WHERE ACTUALCOUNT > BUDGETCOUNT " +
                  "ORDER BY VESSELNAME ";

    string sql7 = "select Ass.status,App.AppraisalOccasionName as Occasion ,Ass.AssMgntID,Ass.AssName, Ass.AssLname, (Ass.AssName+' '+Ass.AssLname)AssNameMod,Ass.rank ,Ass.VesselCode,Ass.CrewNo " +
                ",(case when Ass.PeapType=1 then 'MANAGEMENT' when Ass.PeapType=2 then 'SUPPORT' when Ass.PeapType=3 then 'OPERATION' end )PeapType " +
                ",(select RankCode from Rank R where R.RankID in((select CurrentrankID from CrewPersonalDetails  CD where CD.CrewNumber=Ass.CrewNo))) ShipSoftRank " +
                ",replace (convert(varchar, Ass.AppraisalFromDate,106),' ','-')AppraisalFromDate " +
                ",replace (convert(varchar, Ass.AppraisalToDate ,106),' ','-')AppraisalToDate " +
                ",replace (convert(varchar, Ass.DatejoinedComp,106),' ','-')DatejoinedComp " +
                ",replace (convert(varchar, Ass.DatejoinedVessel ,106),' ','-')DatejoinedVessel " +
                ",replace (convert(varchar, Ass.AppraisalRecievedDate,106),' ','-')AppraisalRecievedDate " +
                "from tbl_Assessment Ass left join AppraisalOccasion App on Ass.Occasion=App.AppraisalOccasionID " +
                "where status=1";

    string sql51_crew = "SELECT CREWNUMBER , FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CREWNAME,VESSELNAME,RANKCODE,C.COUNTRYNAME,EXPECTEDJOINDATE,C1.COUNTRYCODE + '- '  + MobileNumber AS CONTACTNO,Email1  FROM " +
                       "DBO.CREWPERSONALDETAILS CPD  " +
                       "INNER JOIN RANK R ON CPD.CURRENTRANKID=R.RANKID " +
                       "INNER JOIN COUNTRY C ON C.COUNTRYID=CPD.NationalityId " +
                       "LEFT JOIN CREWCONTACTDETAILS CCD ON CCD.CREWID=CPD.CREWID AND CCD.ADDRESSTYPE='C' " +
                       "LEFT JOIN COUNTRY C1 ON C1.COUNTRYID=CCD.MobileCountryId " +
                       "LEFT JOIN VESSEL V ON V.VESSELID=CPD.LASTVESSELID " +
                       "WHERE CREWSTATUSID=2  AND CPD.CREWID NOT IN ( SELECT DISTINCT C1.FirstRelieverId FROM CREWPERSONALDETAILS C1 WHERE ISNULL(C1.FirstRelieverId,0)>0 ) ";

    string sql51_app = "SELECT CANDIDATEID as CREWNUMBER,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CREWNAME,'' AS VESSELNAME,R.RANKCODE,C.COUNTRYNAME,AVAILABLEFROM AS EXPECTEDJOINDATE " +
                       "FROM DBO.candidatepersonaldetails CPD " +
                       "INNER JOIN RANK R ON CPD.RANKAPPLIEDID=R.RANKID  " +
                       "INNER JOIN COUNTRY C ON C.COUNTRYID=CPD.NationalityId WHERE STATUS<>5 ";

    string sql8 = "SELECT CANDIDATEID as CREWNUMBER,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CREWNAME,'' AS VESSELNAME,R.RANKCODE,C.COUNTRYNAME,AVAILABLEFROM AS EXPECTEDJOINDATE " +
                       "FROM DBO.candidatepersonaldetails CPD " +
                       "INNER JOIN RANK R ON CPD.RANKAPPLIEDID=R.RANKID  " +
                       "INNER JOIN COUNTRY C ON C.COUNTRYID=CPD.NationalityId WHERE STATUS<>5";

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        if (!IsPostBack)
        {
            BindRankDropDown();
            BindRecruitingOffice();
            BindNationalityDropDown();
            ShowData();
        }
    }
    private void BindRecruitingOffice()
    {
        ProcessGetRecruitingOffice processgetrecruitingoffice = new ProcessGetRecruitingOffice();
        try
        {
            processgetrecruitingoffice.Invoke();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
        }
        ddlRO.DataValueField = "RecruitingOfficeId";
        ddlRO.DataTextField = "RecruitingOfficeName";
        ddlRO.DataSource = processgetrecruitingoffice.ResultSet;
        ddlRO.DataBind();
    }
    private void BindRankDropDown()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddlRank.DataSource = obj.ResultSet.Tables[0];
        ddlRank.DataTextField = "RankName";
        ddlRank.DataValueField = "RankId";
        ddlRank.DataBind();

    }
    private void BindNationalityDropDown()
    {
        ProcessSelectNationality obj = new ProcessSelectNationality();
        obj.Invoke();
        ddlNat.DataSource = obj.ResultSet.Tables[0];
        ddlNat.DataTextField = "CountryName";
        ddlNat.DataValueField = "CountryId";
        ddlNat.DataBind();
    }
    public void ShowData()
    {
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
        int Count1 = Common.CastAsInt32(dt1.Rows.Count);
        lblc1.Text = Count1.ToString();

        //--------------------

        DataTable dt2 = CrewApproval.Bind_CrewApprovalGrid(0, 0, 'N', 0, 'A', 0, "", Convert.ToInt32(Session["loginid"].ToString()));
        int Count2 = Common.CastAsInt32(dt2.Rows.Count);
        lblc2.Text = (Count2).ToString();

        //--------------------

        //DataTable dt3 = Common.Execute_Procedures_Select_ByQueryCMS("exec [dbo].[Report_TrainingDetails] '','',0,'',0,'01-01-2010','" + DateTime.Today.ToString("dd-MMM-yyyy") + "',0,0,0,0");
        //int Count3 = Common.CastAsInt32(dt3.Rows.Count);
        //lblc3.Text = (Count3).ToString();
        //--------------------

        //DataTable dt4 = Common.Execute_Procedures_Select_ByQueryCMS("exec DBO.GET_PENDING_REQ_DOCS");
        //int Count4 = Common.CastAsInt32(dt4.Rows.Count);
        //lblc4.Text = (Count4).ToString();

        //--------------------

        DataTable dt5 = Common.Execute_Procedures_Select_ByQueryCMS(sql5);
        int Count5 = Common.CastAsInt32(dt5.Rows.Count);
        lblc5.Text = (Count5).ToString();

        //--------------------

        DataTable dt6 = Common.Execute_Procedures_Select_ByQueryCMS(sql6);
        int Count6 = Common.CastAsInt32(dt6.Rows.Count);
        lblc6.Text = (Count6).ToString();

        //--------------------

        DataTable dt7 = Common.Execute_Procedures_Select_ByQueryCMS(sql7);
        int Count7 = Common.CastAsInt32(dt7.Rows.Count);
        lblc7.Text = (Count7).ToString();



        //--------------------

        DataTable dt51_crew = Common.Execute_Procedures_Select_ByQueryCMS(sql51_crew + " AND EXPECTEDJOINDATE <= '" + Common.ToDateString(DateTime.Today.AddDays(Common.CastAsInt32(txtNextDays.Text))) + "'");
        int Count51_crew = Common.CastAsInt32(dt51_crew.Rows.Count);

        DataTable dt51_app = Common.Execute_Procedures_Select_ByQueryCMS(sql51_app + " AND AVAILABLEFROM <= '" + Common.ToDateString(DateTime.Today.AddDays(Common.CastAsInt32(txtNextDays.Text))) + "'");
        int Count51_app = Common.CastAsInt32(dt51_app.Rows.Count);

        lblc51.Text = (Count51_crew + Count51_app).ToString();

        //--------------------
        DataTable dt8 = Common.Execute_Procedures_Select_ByQueryCMS(sql51_crew + " AND CPD.CURRENTRANKID NOT IN (48,49) AND ( AvailableFrom <= '" + Common.ToDateString(DateTime.Today.AddDays(Common.CastAsInt32(txtAvailibilityInDays.Text.Trim()))) + "' or AvailableFrom is null  ) ");
        int Count8 = Common.CastAsInt32(dt8.Rows.Count);
        lblc8.Text = (Count8).ToString();
    }
    protected void btn_Show1_Click(object sender, EventArgs e)
    {
        DataTable dt_CC = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
        rpt1.DataSource = dt_CC;
        lblRcount1.Text = " ( " + dt_CC.Rows.Count.ToString() + " Records )";
        rpt1.DataBind();
    }
    protected void btn_Show2_Click(object sender, EventArgs e)
    {
        DataTable dt_VessAssignApproval = CrewApproval.Bind_CrewApprovalGrid(0, 0, 'N', 0, 'A', 0, "", Convert.ToInt32(Session["loginid"].ToString()));
        rpt2.DataSource = dt_VessAssignApproval;
        lblRcount2.Text = " ( " + dt_VessAssignApproval.Rows.Count.ToString() + " Records )";
        rpt2.DataBind();
    }

    protected void btn_Show3_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("exec [dbo].[Report_TrainingDetails] '','',0,'',0,'01-01-2010','" + DateTime.Today.ToString("dd-MMM-yyyy") + "',0,0,0,0");
        DataView dv = dt.DefaultView;
        dv.Sort = "CrewNumber";
        rpt3.DataSource = dv.ToTable();
        lblRcount3.Text = " ( " + dt.Rows.Count.ToString() + " Records )";
        rpt3.DataBind();
    }
    protected void btn_Show4_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("exec DBO.GET_PENDING_REQ_DOCS");
        rpt4.DataSource = dt;
        lblRcount4.Text = " ( " + dt.Rows.Count.ToString() + " Records )";
        rpt4.DataBind();
    }
    protected void btn_Show5_Click(object sender, EventArgs e)
    {
        string s = sql5;
        if (ddlRO.SelectedIndex > 0)
        {
            s = s.Replace(" ORDER BY VESSELCODE,RANKLEVEL", "");
            s += " AND C.RECRUITMENTOFFICEID=" + ddlRO.SelectedValue + " ORDER BY VESSELCODE,RANKLEVEL";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(s);
        rpt5.DataSource = dt;
        lblRcount5.Text = " ( " + dt.Rows.Count.ToString() + " Records )";
        rpt5.DataBind();
    }
    protected void btn_Show6_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql6);
        rpt6.DataSource = dt;
        lblRcount6.Text = " ( " + dt.Rows.Count.ToString() + " Records )";
        rpt6.DataBind();
    }
    protected void btn_Show7_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql7);
        rpt7.DataSource = dt;
        lblRcount7.Text = " ( " + dt.Rows.Count.ToString() + " Records )";
        rpt7.DataBind();
    }
    protected void btn_Show51_Click(object sender, EventArgs e)
    {
        if (rad_CA.SelectedValue == "C")
        {
            string crewwhereclause = "";
            if (txtCrewNo.Text.Trim() != "")
            {
                crewwhereclause += " AND CPD.CREWNUMBER='" + txtCrewNo.Text.Trim() + "'";
            }
            if (txtCrewName.Text.Trim() != "")
            {
                crewwhereclause += " AND (FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME) LIKE '%" + txtCrewName.Text.Trim() + "%'";
            }
            if (ddlRank.SelectedIndex > 0)
            {
                crewwhereclause += " AND CPD.CURRENTRANKID=" + ddlRank.SelectedValue + "";
            }
            if (ddlNat.SelectedIndex > 0)
            {
                crewwhereclause += " AND CPD.NationalityId=" + ddlNat.SelectedValue + "";
            }
            if (Common.CastAsInt32(txtNextDays.Text) > 0)
            {
                crewwhereclause += " AND EXPECTEDJOINDATE <= '" + Common.ToDateString(DateTime.Today.AddDays(Common.CastAsInt32(txtNextDays.Text))) + "'";
            }

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql51_crew + crewwhereclause + " ORDER BY RANKLEVEL,EXPECTEDJOINDATE");

            rpt51.DataSource = dt;
            lblRcount51.Text = " ( " + dt.Rows.Count.ToString() + " Records )";
            rpt51.DataBind();
        }
        else
        {
            string appwhereclause = "";
            if (txtCrewNo.Text.Trim() != "")
            {
                appwhereclause += " AND CPD.CANDIDATEID=" + txtCrewNo.Text.Trim() + "";
            }
            if (txtCrewName.Text.Trim() != "")
            {
                appwhereclause += " AND (FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME) LIKE '%" + txtCrewName.Text.Trim() + "%'";
            }
            if (ddlRank.SelectedIndex > 0)
            {
                appwhereclause += " AND CPD.RANKAPPLIEDID=" + ddlRank.SelectedValue + "";
            }
            if (ddlNat.SelectedIndex > 0)
            {
                appwhereclause += " AND CPD.NationalityId=" + ddlNat.SelectedValue + "";
            }
            if (Common.CastAsInt32(txtNextDays.Text) > 0)
            {
                appwhereclause += " AND AVAILABLEFROM <= '" + Common.ToDateString(DateTime.Today.AddDays(Common.CastAsInt32(txtNextDays.Text))) + "'";
            }

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql51_app + appwhereclause + " ORDER BY RANKLEVEL,AVAILABLEFROM");

            rpt51.DataSource = dt;
            lblRcount51.Text = " ( " + dt.Rows.Count.ToString() + " Records )";
            rpt51.DataBind();
        }
    }
    protected void rad_CA_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        btn_Show51_Click(sender, e);
    }

    protected void OnTextChanged_txtAvailibilityInDays(object sender, EventArgs e)
    {
        ShowData();
    }

    protected void lbChart_Click(object sender, EventArgs e)
    {
        Response.Redirect("Chart.aspx");
    }

    protected void lbMissingDoc_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewMissingDocuments.aspx");
    }
}
