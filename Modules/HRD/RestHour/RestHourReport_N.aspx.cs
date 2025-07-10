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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp;
using System.Collections.Generic;

public partial class RestHourReport_N : System.Web.UI.Page
{
    Random r = new Random();
    byte[] filedata;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblpopmsg.Text = "";
        if (!IsPostBack)
        {
            BindFleet();
            for (int i = DateTime.Today.Year; i >= 2016; i--)
            {
                ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem(" Month ", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem(new DateTime(2015, i, 1).ToString("MMM"), i.ToString()));
            }
            ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
            BindGrid(sender, e);
        }
    }
    public void BindFleet()
    {
        //ddlVessel
        string sql = "SELECT * FROM DBO.FleetMaster order by FleetName";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        ddlFleet.DataSource = dt;
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetId";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" All Fleet ", ""));
    }
    protected void BindGrid(object sender, EventArgs e)
    {
        string SQL = "SELECT V.VESSELID,VESSELNAME,MNTH,datename(month,'" + ddlYear.SelectedValue + "-' + convert(varchar,MNTH) + '-01')as MOnthName,MailSent, " +
                    "(SELECT COUNT(DISTINCT CREWID) FROM DBOCP_NonConformance NC WHERE NC.VESSELID=V.VesselId AND NCTYPE IN (2,6,24) AND YEAR(NCDate)=" + ddlYear.SelectedValue + " AND MONTH(NCDate)=MNTH) AS NC_CREW_COUNT, " +
                    "( " +
                    "    SELECT COUNT(*) FROM " +
                    "    ( " +
                    "        SELECT crewid,COUNT(DISTINCT ncdate) AS NC_COUNT FROM DBO.CP_NonConformance NC WHERE NC.VESSELID=V.VesselId AND NCTYPE IN (2,6,24) AND YEAR(NCDate)=" + ddlYear.SelectedValue + " AND MONTH(NCDate)=MNTH group by crewid having COUNT(DISTINCT ncdate) >= 3  " +
                    "    ) A " +
                    ") AS NC_CREW_COUNT_3 " +
                    ",OfficeCommentsBy,OfficeCommentsOn " +
                    "FROM DBO.VESSEL V CROSS JOIN  " +
                    "(SELECT 1 AS MNTH UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12) AS MNTHS " +
                    " LEFT JOIN DBO.CP_OfficeComments OC ON OC.VESSELID=V.VESSELID AND OC.YEAR=" + ddlYear.SelectedValue + " AND OC.MONTH=MNTH " +
                    "WHERE V.VESSELSTATUSID=1 and V.VESSELID in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  ";

        SQL = " SELECT V.VesselID, V.VesselCode,VESSELNAME,MNTH,datename(month,'" + ddlYear.SelectedValue + "-' + convert(varchar,MNTH) + '-01')as MOnthName,MailSent " +
              "  , (SELECT COUNT(DISTINCT CrewNumber) FROM dbo.RH_NCList NC WHERE NC.VESSELCODE = V.VesselCode AND NCId IN(2, 6, 24) AND YEAR(FORDATE) = " + ddlYear.SelectedValue + " AND MONTH(FORDATE) = MNTH) AS NC_CREW_COUNT " +
              "          , (SELECT COUNT(*) FROM(SELECT CrewNumber, COUNT(DISTINCT FORDATE) AS NC_COUNT FROM dbo.RH_NCList NC WHERE NC.VESSELCODE = V.VESSELCODE AND NCID IN(2, 6, 24) AND YEAR(FORDATE) = " + ddlYear.SelectedValue + " AND MONTH(FORDATE) = MNTH group by CrewNumber having COUNT(DISTINCT FORDATE) >= 3) A ) AS NC_CREW_COUNT_3 " +
              "  , OfficeCommentsBy, OfficeCommentsOn " +
              "  FROM DBO.VESSEL V " +
              "  CROSS JOIN " +
              "  ( " +
              "      SELECT 1 AS MNTH UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12 " +
              "  ) AS MNTHS " +
              "  LEFT JOIN DBO.RH_OfficeComments OC ON OC.VesselCode = V.VesselCode AND OC.YEAR = "+ ddlYear.SelectedValue + " AND OC.MONTH = MNTH " +
              "  WHERE V.VESSELSTATUSID = 1 and V.VESSELID in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  ";

        if (ddlFleet.SelectedIndex > 0)
        {
            SQL += " AND V.FLEETID= " + ddlFleet.SelectedValue;
        }
        if (ddlMonth.SelectedIndex > 0)
        {
            SQL += " AND MNTH= " + ddlMonth.SelectedValue;
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + " Order By VesselName,MNTH");
        rpt_data.DataSource = dt;
        rpt_data.DataBind();
    }

    protected void btnAddComments_Click(object sender, EventArgs e)
    {
        int VesselId = Common.CastAsInt32(hdfVsl.Text);
        int Month = Common.CastAsInt32(hdfMnth.Text);
        int Year = Common.CastAsInt32(ddlYear.SelectedValue);

        if (VesselId <= 0 || Month <= 0 || Year <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fasfa", "alert('Please select a record to continue.');", true);
            return;
        }

        txtComments.Text = lblOfficeComments.Text;
        lblVessel1.Text = lblVesselName.Text + " - Office Comments";
        dv_AddComments.Visible = true;
    }
    protected void btnSaveComments_Click(object sender, EventArgs e)
    {
        int VesselId = Common.CastAsInt32(hdfVsl.Text);
        string VesselCode = hdfVslCode.Text;
        int Month = Common.CastAsInt32(hdfMnth.Text);
        int Year = Common.CastAsInt32(ddlYear.SelectedValue);

        if (VesselId <= 0 || Month <= 0 || Year <= 0)
        {
            lblpopmsg.Text = "Please select a record to continue.";
            return;
        }

        if (txtComments.Text.Trim() == "")
        {
            lblpopmsg.Text = "Please enter comments to continue.";
            return;
        }
        try
        {
            Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.RH_INSERTUPDATE_RESTHR_OFFICECOMMENTS '" + VesselCode + "'," + Year + "," + Month + ",'" + txtComments.Text.Trim().Replace("'", "`") + "','" + Session["UserName"].ToString() + "'");
            dv_AddComments.Visible = false;
            BindGrid(sender, e);
            btnPost_Click(sender, e);
        }
        catch (Exception ex)
        {
            lblpopmsg.Text = "Unable to save comments. Error : " + ex.Message;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dv_AddComments.Visible = false;
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        string CellWidth = "20px";
        int VesselId = Common.CastAsInt32(hdfVsl.Text);
        string VesselCode = hdfVslCode.Text;
        int Month = Common.CastAsInt32(hdfMnth.Text);
        int Year = Common.CastAsInt32(ddlYear.SelectedValue);
        int DiM = DateTime.DaysInMonth(Year, Month);

        lblOfficeComments.Text = "";
        lblOfficeCommentsByOn.Text = "";

        DataTable dtComments = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.RH_OfficeComments WHERE VESSELCode='" + VesselCode + "' and month=" + Month + " and year=" + Year);
        if (dtComments.Rows.Count > 0)
        {
            lblOfficeComments.Text = dtComments.Rows[0]["OfficeComments"].ToString();
            lblOfficeCommentsByOn.Text = dtComments.Rows[0]["OfficeCommentsBy"].ToString() + " / " + Common.ToDateString(dtComments.Rows[0]["OfficeCommentsOn"]);
        }

        DataTable dtvsl = Common.Execute_Procedures_Select_ByQueryCMS("SELECT VesselName FROM DBO.VESSEL WHERE VESSELID=" + VesselId);
        if (dtvsl.Rows.Count > 0)
            lblVesselName.Text = "Vessel : " + dtvsl.Rows[0]["VesselName"].ToString() + " , Period : " + new DateTime(Year, Month, 1).ToString("MMM-yyyy") + " ( Rest Hour Violation ) ";

        DataTable dtDailyHrs = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWNUMBER,ForDate,RestIn24 AS HRS ,isnull(Remarks,'') as Remarks FROM dbo.RH_CrewMonthData WHERE VESSELCode='" + VesselCode + "' AND MONTH(ForDate)=" + Month + " AND YEAR(ForDate)=" + Year + "  ");
        DataTable dtMonthNCDays = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWNUMBER,FORDATE FROM dbo.RH_NCList WHERE VESSELCODE='" + VesselCode + "' AND MONTH(FORDATE)=" + Month + " AND YEAR(FORDATE)=" + Year + " AND  NCid IN (2,6,24)");

        StringBuilder sb = new StringBuilder();
        sb.Append(@"<td style='width:40px;text-align:left'>Crew #</td>" +
                   "<td style='text-align:left'>Crew Name</td>" +
                    "<td style='width:40px;text-align:center'>Rank</td>" +
                    "<td style='width:70px;text-align:center'>Sign On Dt.</td>" +
                    "<td style='width:70px;text-align:center'>Sign Off Dt.</td>");

        for (int i = 1; i <= DiM; i++)
        {
            sb.Append("<td style='width:" + CellWidth + ";text-align:center'>" + i.ToString() + "</td>");
        }
        sb.Append("<td style='width:20px;text-align:center'>&nbsp;</td>");
        litheader.Text = sb.ToString();

        DateTime Stdt = new DateTime(Year, Month, 1);
        DateTime Enddt = Stdt.AddMonths(1);
        

        //string sql = "SELECT  " +
        //                    "A.*,CPD.CrewNumber,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CrewName ,RANKCODE,L.SignOnDate,L.SignOffDate  " +
        //                    "FROM   " +
        //                    "(SELECT DISTINCT CREWNUMBER FROM dbo.RH_NCList WHERE VESSELCODE=" + VesselCode + " AND YEAR(FORDATE)=" + Year + " AND MONTH(FORDATE)=" + Month + ") A  " +
        //                    "INNER JOIN DBO.CP_VesselCrewList L ON A.CREWID=L.CREWID AND L.VESSELID=" + VesselId +
        //                    "INNER JOIN DBO.RANK R ON L.RankId=R.RANKID  " +
        //                    "INNER JOIN DBO.CrewPersonalDetails CPD ON CPD.CrewId=A.CrewId " +
        //                    "WHERE (L.SignOnDate<'" + Enddt.ToString("dd-MMM-yyyy") + "' AND (L.SignOffDate IS NULL OR L.SignOffDate>='" + Stdt.ToString("dd-MMM-yyyy") + "')) " +
        //                    "ORDER BY CREWNAME";

        string sql = " SELECT ch.CREWID,CPD.CrewNumber,AR.RankCode,ch.ContractId ,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CrewName,NewRankId as Rankid, RankName ,ch.SignOnDate,ch.SingOffDate as SignOffDate " +
                     "   from DBO.get_Crew_History ch " +
                     "   INNER JOIN DBO.CrewPersonalDetails CPD ON CPD.CrewId = ch.CrewId " +
                     "   LEFT JOIN DBO.Rank AR ON AR.RANKID = ch.NewRankId " +
                     "   WHERE VesselId = " + VesselId + " AND ch.SignOnDate< '" + Enddt + "' AND (CH.SingOffDate >= '"+ Stdt + "' OR CH.SingOffDate IS NULL) "+
                     " and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  order by FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        sb = new StringBuilder();
        int CrewId = 0;
        string CrewNumber = "";
        foreach (DataRow dr in dt.Rows)
        {
            CrewId = Common.CastAsInt32(dr["CrewId"]);
            CrewNumber = dr["CrewNumber"].ToString();
            sb.Append(@"<tr>");
            sb.Append("<td style='width:40px;text-align:left'>" + dr["CrewNumber"].ToString() + "</td>" +
                       "<td style='text-align:left'>" + dr["CrewName"].ToString() + "</td>" +
                       "<td style='width:40px;text-align:center'>" + dr["RANKCODE"].ToString() + "</td>" +
                       "<td style='width:70px;text-align:center'>" + Common.ToDateString(dr["SignOnDate"]) + "</td>" +
                       "<td style='width:70px;text-align:center'>" + Common.ToDateString(dr["SignOffDate"]) + "</td>");
            for (int i = 1; i <= DiM; i++)
            {
                DateTime ForDate = new DateTime(Year, Month, i);
                if (dtMonthNCDays.Select("CrewNumber='" + CrewNumber + "' AND FORDATE='" + ForDate.ToString("dd-MMM-yyyy") + "'").Length > 0)
                {
                    DataRow[] drs = dtDailyHrs.Select("CrewNumber='" + CrewNumber + "' AND FORDATE='" + ForDate.ToString("dd-MMM-yyyy") + "'");
                    if (drs.Length > 0)
                    {
                        string remarks = drs[0]["Remarks"].ToString().Replace("'","`");
                        decimal Resthrs = Common.CastAsDecimal(drs[0]["HRS"]);
                        string classname = (Resthrs >= 10) ? "ncgreen" : "ncred";

                        sb.Append("<td title='" + remarks + "' class='" + classname + "' style='width:" + CellWidth + ";text-align:center;'>" + drs[0]["HRS"].ToString() + "</td>");
                    }
                    else
                        sb.Append("<td class='ncred' style='width:" + CellWidth + ";text-align:center;'>&nbsp;</td>");
                }
                else
                    sb.Append("<td style='width:" + CellWidth + ";text-align:center'>&nbsp;</td>");
            }
            sb.Append("<td style='width:20px;text-align:center'>&nbsp;</td>");
            sb.Append(@"</tr>");
        }
        litdata.Text = sb.ToString();
    }
    protected void btnPrintForm_Click(object sender, EventArgs e)
    {
        ImageButton ctl = (ImageButton)sender;

        int VesselId = Common.CastAsInt32(ctl.Attributes["vesselid"]);
        int Month = Common.CastAsInt32(ctl.Attributes["month"]);
        int Year = Common.CastAsInt32(ddlYear.SelectedValue);
        int DiM = DateTime.DaysInMonth(Year, Month);
        PrintForm(false, VesselId, Month, Year, DiM);
    }

    public void PrintForm(bool StreamNeeded,int VesselId, int Month, int Year, int DiM)
    {
        List<int> ColsWidth = new List<int>();
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add(new DataColumn("Crew#", typeof(string)));
        dtResult.Columns.Add(new DataColumn("CrewName", typeof(string)));
        dtResult.Columns.Add(new DataColumn("Rank", typeof(string)));
        dtResult.Columns.Add(new DataColumn("SignOnDt", typeof(string)));
        dtResult.Columns.Add(new DataColumn("SignOffDt", typeof(string)));

        DataTable dtResultColor = new DataTable();
        dtResultColor.Columns.Add(new DataColumn("Crew#", typeof(string)));
        dtResultColor.Columns.Add(new DataColumn("CrewName", typeof(string)));
        dtResultColor.Columns.Add(new DataColumn("Rank", typeof(string)));
        dtResultColor.Columns.Add(new DataColumn("SignOnDt", typeof(string)));
        dtResultColor.Columns.Add(new DataColumn("SignOffDt", typeof(string)));

        ColsWidth.Add(5);
        ColsWidth.Add(10);
        ColsWidth.Add(5);
        ColsWidth.Add(6);
        ColsWidth.Add(6);

        for (int i = 1; i <= DiM; i++)
        {
            dtResult.Columns.Add(new DataColumn("D" + i.ToString(), typeof(string)));
            dtResultColor.Columns.Add(new DataColumn("D" + i.ToString(), typeof(string)));
            ColsWidth.Add(2);
        }

        //---------------------------------------------- 
        string OfficeComments = "";
        string OfficeCommentsByOn = "";

        DataTable dtvsl = Common.Execute_Procedures_Select_ByQueryCMS("SELECT vesselcode,VesselName FROM DBO.VESSEL WHERE VESSELID=" + VesselId);
        string VesselCode = "";
        if (dtvsl.Rows.Count > 0)
        {
            VesselCode = dtvsl.Rows[0]["vesselcode"].ToString();
            lblVesselName.Text = "Vessel : " + dtvsl.Rows[0]["VesselName"].ToString() + " , Period : " + new DateTime(Year, Month, 1).ToString("MMM-yyyy") + " ( Rest Hour Violation ) ";
        }

        //DataTable dtComments = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.CP_OfficeComments WHERE VESSELID=" + VesselId + " and month=" + Month + " and year=" + Year);
        DataTable dtComments = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.RH_OfficeComments WHERE VESSELCode='" + VesselCode + "' and month=" + Month + " and year=" + Year);

        if (dtComments.Rows.Count > 0)
        {
            OfficeComments = dtComments.Rows[0]["OfficeComments"].ToString();
            OfficeCommentsByOn = dtComments.Rows[0]["OfficeCommentsBy"].ToString() + " / " + Common.ToDateString(dtComments.Rows[0]["OfficeCommentsOn"]);
        }

       

        //DataTable dtDailyHrs = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWID,TRANSDATE,SUM(DURATION) AS HRS FROM CP_CrewDailyWorkRestHours WHERE VESSELID=" + VesselId + " AND MONTH(TRANSDATE)=" + Month + " AND YEAR(TRANSDATE)=" + Year + " AND WORKREST='R' GROUP BY CREWID,TRANSDATE");
        //DataTable dtMonthNCDays = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWID,NCDATE FROM DBO.CP_NonConformance WHERE VESSELID=" + VesselId + " AND MONTH(NCDate)=" + Month + " AND YEAR(NCDate)=" + Year + " AND  NCTYPE IN (2,6,24)");

        DataTable dtDailyHrs = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWNUMBER,ForDate,RestIn24 AS HRS FROM dbo.RH_CrewMonthData WHERE VESSELCode='" + VesselCode + "' AND MONTH(ForDate)=" + Month + " AND YEAR(ForDate)=" + Year + "  ");
        DataTable dtMonthNCDays = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWNUMBER,FORDATE FROM dbo.RH_NCList WHERE VESSELCODE='" + VesselCode + "' AND MONTH(FORDATE)=" + Month + " AND YEAR(FORDATE)=" + Year + " AND  NCid IN (2,6,24)");


        DateTime Stdt = new DateTime(Year, Month, 1);
        DateTime Enddt = Stdt.AddMonths(1);


        //string sql = "SELECT  " +
        //                    "A.*,CPD.CrewNumber,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CrewName ,RANKCODE,L.SignOnDate,L.SignOffDate  " +
        //                    "FROM   " +
        //                    "(SELECT DISTINCT CREWID FROM DBO.CP_NonConformance WHERE VESSELID=" + VesselId + " AND YEAR(NCDATE)=" + Year + " AND MONTH(NCDATE)=" + Month + ") A  " +
        //                    "INNER JOIN DBO.CP_VesselCrewList L ON A.CREWID=L.CREWID AND L.VESSELID=" + VesselId +
        //                    "INNER JOIN DBO.RANK R ON L.RankId=R.RANKID  " +
        //                    "INNER JOIN DBO.CrewPersonalDetails CPD ON CPD.CrewId=A.CrewId " +
        //                    "WHERE (L.SignOnDate<'" + Enddt.ToString("dd-MMM-yyyy") + "' AND (L.SignOffDate IS NULL OR L.SignOffDate>='" + Stdt.ToString("dd-MMM-yyyy") + "')) " +
        //                    "ORDER BY CREWNAME";

        string sql = " SELECT ch.CREWID,CPD.CrewNumber,AR.RankCode,ch.ContractId ,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CrewName,NewRankId as Rankid, RankName ,ch.SignOnDate,ch.SingOffDate as SignOffDate " +
             "   from DBO.get_Crew_History ch " +
             "   INNER JOIN DBO.CrewPersonalDetails CPD ON CPD.CrewId = ch.CrewId " +
             "   LEFT JOIN DBO.Rank AR ON AR.RANKID = ch.NewRankId " +
             "   WHERE VesselId = " + VesselId + " AND ch.SignOnDate< '" + Enddt + "' AND(CH.SingOffDate >= '" + Stdt + "' OR CH.SingOffDate IS NULL) " +
             "   order by FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME ";


        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        StringBuilder sb = new StringBuilder();
        int CrewId = 0;
        foreach (DataRow dr in dt.Rows)
        {
            DataRow drNR = dtResult.NewRow();
            DataRow drNRColor = dtResultColor.NewRow();

            CrewId = Common.CastAsInt32(dr["CrewId"]);
            string CrewNumber= dr["CrewNumber"].ToString();

            drNR["Crew#"] = dr["CrewNumber"].ToString();
            drNR["CrewName"] = dr["CrewName"].ToString();
            drNR["Rank"] = dr["RANKCODE"].ToString();
            drNR["SignOnDt"] = Common.ToDateString(dr["SignOnDate"]);
            drNR["SignOffDt"] = Common.ToDateString(dr["SignOffDate"]);

            for (int i = 1; i <= DiM; i++)
            {
                DateTime ForDate = new DateTime(Year, Month, i);
                if (dtMonthNCDays.Select("CrewNumber='" + CrewNumber + "' AND FORDATE='" + ForDate.ToString("dd-MMM-yyyy") + "'").Length > 0)
                {
                    DataRow[] drs = dtDailyHrs.Select("CrewNumber='" + CrewNumber + "' AND FORDATE='" + ForDate.ToString("dd-MMM-yyyy") + "'");
                    if (drs.Length > 0)
                    {
                        decimal Resthrs = Common.CastAsDecimal(drs[0]["HRS"]);
                        string classname = (Resthrs >= 10) ? "ncgreen" : "ncred";

                        drNR["D" + i.ToString()] = drs[0]["HRS"].ToString();
                        drNRColor["D" + i.ToString()] = classname;
                        //sb.Append("<td class='" + classname + "' style='width:" + CellWidth + ";text-align:center;'>" + drs[0]["HRS"].ToString() + "</td>");
                    }
                    else
                    {
                        drNR["D" + i.ToString()] = "";
                        drNRColor["D" + i.ToString()] = "";
                    }
                    //sb.Append("<td class='ncred' style='width:" + CellWidth + ";text-align:center;'>&nbsp;</td>");
                }
                else
                {
                    drNR["D" + i.ToString()] = "";
                    drNRColor["D" + i.ToString()] = "";
                }

                //sb.Append("<td style='width:" + CellWidth + ";text-align:center'>&nbsp;</td>");
            }
            
                //sb.Append("<td style='width:20px;text-align:center'>&nbsp;</td>");
                //sb.Append(@"</tr>");
                dtResult.Rows.Add(drNR);
            dtResultColor.Rows.Add(drNRColor);
        }
        //litdata.Text = sb.ToString();


        //----------------------------=================================================== EXPORT TO PDF  //----------------------------=================================================== 
        Export_PDF(dtResult, dtResultColor, ColsWidth.ToArray(), OfficeComments, OfficeCommentsByOn, StreamNeeded);
    }

    protected void Export_PDF(DataTable DtResult, DataTable DtResultColor, int[] Widths, string OfficeComments, string OfficeCommentsByOn, bool StreamNeeded)
    {
        try
        {
            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10);
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("eMANAGER");
            document.AddSubject("Rest Hour Violation - ( Office Remarks )");

            Font BlackText = FontFactory.GetFont("ARIAL", 5, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            Font BlueText = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLUE);
            Font RedText = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.RED);

            Font f_head = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD);
            Font f_10 = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.UNDERLINE);
            Font f_7 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD);
            Font f_7_NORMAL = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
            Font f_7_NORMAL_I = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC);

            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            tb_header.Width = 100;
            tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.BorderWidth = 0;
            tb_header.BorderColor = iTextSharp.text.Color.WHITE;
            tb_header.Cellspacing = 1;
            tb_header.Cellpadding = 1;

            Phrase p2 = new Phrase();
            p2.Add(new Phrase("\nRest Hour Management " + "", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            Phrase p3 = new Phrase();
            p3.Add(new Phrase(lblVesselName.Text.Replace("( Rest Hour Violation )", ""), FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.UNDERLINE)));
            Cell c3 = new Cell(p3);
            c3.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c3);

            HeaderFooter header = new HeaderFooter(new Phrase(""), false);
            document.Header = header;

            HeaderFooter footer = new HeaderFooter(new Phrase("This is computer generated report, signature not required.", FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------
            document.Open();
            document.Add(tb_header);
            document.Add(new Phrase("\n"));
            document.Add(new Phrase("\n"));
            document.Add(new Phrase("Following crew have not complied with the rest hour rules. Numbers given in the date cell refers the total rest taken in hours during the violation period."));
            document.Add(new Phrase("\n"));

            iTextSharp.text.Table tb_crew = new iTextSharp.text.Table(Widths.Length);
            tb_crew.Width = 100;
            //tb_crew.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //tb_crew.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_crew.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            //tb_crew.BorderWidth = 0;
            //tb_crew.BorderColor = iTextSharp.text.Color.WHITE;
            tb_crew.DefaultVerticalAlignment = Element.ALIGN_TOP;
            tb_crew.Cellspacing = 1;
            tb_crew.Cellpadding = 1;
            tb_crew.SetWidths(Widths);
            //------------------------------------------ header ---------------------------------
            for (int i = 0; i <= Widths.Length - 1; i++)
            {

                Cell c_1;
                if (i <= 4)
                {
                    c_1 = new Cell(new Phrase(DtResult.Columns[i].ColumnName, f_7));
                    c_1.HorizontalAlignment = Element.ALIGN_LEFT;
                }
                else
                {
                    c_1 = new Cell(new Phrase(DtResult.Columns[i].ColumnName.ToString().Replace("D", ""), f_7));
                    c_1.HorizontalAlignment = Element.ALIGN_RIGHT;
                }
                c_1.VerticalAlignment = Element.ALIGN_TOP;
                tb_crew.AddCell(c_1);
            }
            //------------------------------------------
            for (int r = 0; r <= DtResult.Rows.Count - 1; r++)
            {
                for (int i = 0; i <= Widths.Length - 1; i++)
                {

                    Cell c_1 = new Cell(new Phrase(DtResult.Rows[r][i].ToString(), f_7_NORMAL));
                    if (i <= 4)
                        c_1.HorizontalAlignment = Element.ALIGN_LEFT;
                    else
                        c_1.HorizontalAlignment = Element.ALIGN_RIGHT;

                    string color = DtResultColor.Rows[r][i].ToString();
                    if (color == "ncred")
                    {
                        c_1.BackgroundColor = iTextSharp.text.Color.RED;
                    }
                    else if (color == "ncgreen")
                    {
                        c_1.BackgroundColor = iTextSharp.text.Color.YELLOW;
                    }

                    c_1.VerticalAlignment = Element.ALIGN_TOP;
                    tb_crew.AddCell(c_1);
                }
            }
            //------------------------------------------
            document.Add(tb_crew);
            document.Add(new Phrase("\n"));

            iTextSharp.text.Table tb_comments = new iTextSharp.text.Table(1);
            tb_comments.Width = 100;
            tb_comments.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_comments.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_comments.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_comments.BorderWidth = 0;
            tb_comments.BorderColor = iTextSharp.text.Color.WHITE;
            tb_comments.DefaultVerticalAlignment = Element.ALIGN_TOP;
            tb_comments.Cellspacing = 1;
            tb_comments.Cellpadding = 1;

            Cell c_51 = new Cell(new Phrase("Office Reply :", f_10));
            c_51.HorizontalAlignment = Element.ALIGN_LEFT;
            c_51.VerticalAlignment = Element.ALIGN_TOP;
            tb_comments.AddCell(c_51);

            c_51 = new Cell(new Phrase(OfficeComments, f_7_NORMAL_I));
            c_51.HorizontalAlignment = Element.ALIGN_LEFT;
            c_51.VerticalAlignment = Element.ALIGN_TOP;
            tb_comments.AddCell(c_51);

            Cell c_52 = new Cell(new Phrase("Replied By/On:", f_10));
            c_52.HorizontalAlignment = Element.ALIGN_LEFT;
            c_52.VerticalAlignment = Element.ALIGN_TOP;
            tb_comments.AddCell(c_52);

            c_52 = new Cell(new Phrase(OfficeCommentsByOn, f_7_NORMAL_I));
            c_52.HorizontalAlignment = Element.ALIGN_LEFT;
            c_52.VerticalAlignment = Element.ALIGN_TOP;
            tb_comments.AddCell(c_52);


            document.Add(tb_comments);
            //document.Add(new Phrase("\n"));
            //document.Add(new Phrase("This is computer generated report signature not required."));
            document.Add(new Phrase("\n"));
            document.Close();
            byte[] bb = msReport.ToArray();

            if (StreamNeeded)
            {
                filedata = bb;
            }
            else
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=file_" + r.NextDouble().ToString().Replace(".", "") + ".pdf");
                Response.AddHeader("Content-Length", bb.Length.ToString());
                Response.OutputStream.Write(bb, 0, bb.Length);
                Response.End();
            }
        }
        catch (System.Exception ex)
        {
            //lblmessage.Text = ex.Message.ToString();
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        ImageButton ctl = (ImageButton)sender;
        string sendername = "";
        string senderemail = "";
        string vesselemail = "";
        string senderposition = "";


        int VesselId = Common.CastAsInt32(ctl.Attributes["vesselid"]);
        int Month = Common.CastAsInt32(ctl.Attributes["month"]);
        int Year = Common.CastAsInt32(ddlYear.SelectedValue);
        int DiM = DateTime.DaysInMonth(Year, Month);

        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS("select c.fIRSTNAME+ ' ' + c.MiddleName + ' ' + c.FamilyName sendername, u.Email as senderemail,PositionName,(select VesselEmailNew from vessel v where vesselid=" + VesselId + ") as vesselemail from Hr_PersonalDetails c Left Join Position p on c.Position=p.PositionId left join UserLogin u on c.UserId=u.LoginId where c.userid=" + Session["loginid"].ToString());
        if (dt1.Rows.Count > 0)
        {
            sendername = dt1.Rows[0]["sendername"].ToString();
            senderemail = dt1.Rows[0]["senderemail"].ToString();
            vesselemail = dt1.Rows[0]["vesselemail"].ToString();
            senderposition = dt1.Rows[0]["PositionName"].ToString();
        }

        PrintForm(true, VesselId, Month, Year, DiM);
        string[] Toadd = { vesselemail };
        //string[] Toadd = { "emanager@energiossolutions.com" };
        string[] CCadd = { senderemail };
        string[] Noadd = { };
        string error = "";
        string Filepath = Server.MapPath("~/RestHour/Temp/file_" + r.NextDouble().ToString().Replace(".", "") + ".pdf");
        using (FileStream fs = File.OpenWrite(Filepath))
        {
            DateTime dt = new DateTime(Year, Month, 1);
            fs.Write(filedata, 0, filedata.Length);
            fs.Close();

            if (SendEmail.SendeMailAsync(1, "emanager@energiossolutions.com", "emanager@energiossolutions.com", Toadd, CCadd, Noadd, "Rest Hour Office Reply - " + dt.ToString("MMM-yyyy"), "Dear Captain,<br/><br/>Attached please find office reply for NON-Compliance for subject month.<br/>If any question please contact undersigned.<br/><br/>Thank You<br/><b>" + sendername + "( " + senderposition + " )" + "</b>", out error, Filepath)) ;
            {
                Common.Execute_Procedures_Select_ByQueryCMS("UPDATE DBO.CP_OfficeComments SET MailSent=1 WHERE VESSELID=" + VesselId + " and month=" + Month + " and year=" + Year);
                BindGrid(sender, e);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fafsd", "alert('Mail sent successfully.')", true);
            }
        }
    }
}
