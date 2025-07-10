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
using System.Text.RegularExpressions;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml;
using Ionic.Zip;
using System.Text;

public partial class VIMS_RHMissingLog : System.Web.UI.Page
{
    public string CurrentShip
    {
        get { return ViewState["CurrentShip"].ToString(); }
        set { ViewState["CurrentShip"] = value; }
    }
    public int Month
    {
        get { return Common.CastAsInt32(ViewState["Month"]); }
        set { ViewState["Month"] = value; }
    }
    public int Year
    {
        get { return Common.CastAsInt32(ViewState["Year"]); }
        set { ViewState["Year"] = value; }
    }

    AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CurrentShip = Session["CurrentShip"].ToString();
            Year = Common.CastAsInt32(Request.QueryString["Year"]);
            Month = Common.CastAsInt32(Request.QueryString["Month"]);
            lblmy.Text = new DateTime(Year, Month, 1).ToString("MMM-yyyy");
            BindCrew();
            ShowCrewList();
            //ShowCrewList_1();
            ShowCrewList_2();
        }
    }
    protected void ddlcrew_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ShowCrewList();
        //ShowCrewList_1();
        ShowCrewList_2();
    }

    public void BindCrew()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.RH_GET_CREW_PENDING_LOG '" + CurrentShip + "'," + Month + "," + Year);
        ddlcrew.DataSource = dt.DefaultView.ToTable(true, "CrewNumber", "CrewName");
        ddlcrew.DataValueField = "CrewNumber";
        ddlcrew.DataTextField = "CrewName";
        ddlcrew.DataBind();
        ddlcrew.Items.Insert(0, new ListItem("All", "0"));
    }

    public void ShowCrewList()
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.RH_GET_CREW_PENDING_LOG '" + CurrentShip + "'," + Month + "," +Year);
        //DataView dv = dt.DefaultView;
        //if(ddlcrew.SelectedIndex>0)
        //{
        //    dv.RowFilter = "CrewNumber='" + ddlcrew.SelectedValue + "'";
        //}      
        //rptLog.DataSource = dv.ToTable();
        //rptLog.DataBind();
    }
    public void ShowCrewList_1()
    {
        return;
        int NoOfDaysInMonth = 0;
        NoOfDaysInMonth = DateTime.DaysInMonth(Year, Month);
        StringBuilder sb = new StringBuilder();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.RH_GET_CREW_PENDING_LOG '" + CurrentShip + "'," + Month + "," + Year);
        DataView dv = dt.DefaultView;
        ViewState["dt_data"] = dt;
        if (ddlcrew.SelectedIndex > 0)
        {
            dv.RowFilter = "CrewNumber='" + ddlcrew.SelectedValue + "'";
        }

        DataTable Dt_distinct = dv.ToTable(true, "CREWNUMBER", "CREWNAME", "RANKNAME");


        sb.Append(" <table cellpadding = '0' cellspacing = '0' border = '0' style = 'text-align:left; border-collapse:collapse;' width = '100%' class='bordered'>   ");
        sb.Append(" <tr style = 'background-color:#e4c82c;color:white; font-weight:bold;' > ");
        sb.Append(" <td style='width:70px;'>Crew#</td> ");
        sb.Append(" <td> Crew Name</td> ");
        sb.Append(" <td>Rank Name</td> ");
        //sb.Append(" < td style = 'width:100px;' > Missing Date</td> ");
        for (int i = 1; i <= NoOfDaysInMonth; i++)
        {
            sb.Append("<td> " + i.ToString()); sb.Append("</td>");
        }
        sb.Append(" </tr>");
        foreach (DataRow Dr in Dt_distinct.Rows)
        {
            sb.Append("<tr>");
            //--------------------
            sb.Append("<td> " + Dr["CREWNUMBER"].ToString() + ""); sb.Append("</td>");
            sb.Append("<td> " + Dr["CREWNAME"].ToString() + ""); sb.Append("</td>");
            sb.Append("<td> " + Dr["RANKNAME"].ToString() + ""); sb.Append("</td>");
            for (int i = 1; i <= NoOfDaysInMonth; i++)
            {
                //sb.Append("<td> " + Ispresent(Dr["CREWNUMBER"].ToString(),i) + ""); sb.Append("</td>");
            }
            //--------------------
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        litData.Text = sb.ToString();
    }
    public void ShowCrewList_2()
    {
        int NoOfDaysInMonth = 0;
        string MonthStart = "";
        string MonthEnd = "";
        NoOfDaysInMonth = DateTime.DaysInMonth(Year, Month);

        MonthStart = new DateTime(Year, Month, 1).ToString("dd-MMM-yyyy");
        MonthEnd = new DateTime(Year, Month, NoOfDaysInMonth).AddDays(1).ToString("dd-MMM-yyyy");
        StringBuilder sb = new StringBuilder();
        //EXEC DBO.RH_GET_CREW_PENDING_LOG '" + CurrentShip + "'," + Month + "," + Year
        DataTable dtrow = Common.Execute_Procedures_Select_ByQuery(" select CH.CrewId,CH.CrewNumber,CH.CrewName,AR.RankName,SignOnDate,signoffdate from PMS_CREW_HISTORY CH LEFT JOIN MP_AllRank AR ON AR.RANKID=CH.RANKID " +
                                               " where VesselCode = '" + CurrentShip + "' and SignOnDate < '" + MonthEnd + "' and(signoffdate is null or signoffdate >= '" + MonthStart + "') order by AR.RANKID");

        DataTable dtData = Common.Execute_Procedures_Select_ByQuery(" select CrewNumber,ForDate,Status,Day(ForDate)Date_Part from RH_CrewMonthData where VesselCode='" + CurrentShip + "' and year(ForDate)=" + Year + " and month(ForDate)=" + Month + "  ");
        DataTable dtNC = Common.Execute_Procedures_Select_ByQuery(" select *,Day(ForDate)Date_Part from RH_NCList where VESSELCODE='" + CurrentShip + "' and year(FORDATE)=" + Year + "  and month(FORDATE)=" + Month + "");
        

        DataView dv = dtrow.DefaultView;

        if (ddlcrew.SelectedIndex > 0)
        {
            dv.RowFilter = "CrewNumber='" + ddlcrew.SelectedValue + "'";
        }


        sb.Append(" <table cellpadding = '0' cellspacing = '0' border = '0' style = 'text-align:left; border-collapse:collapse;' width = '100%' class='my_bordered'>   ");
        sb.Append(" <tr style = 'background-color:#e4c82c;color:white; font-weight:bold;' > ");
        sb.Append(" <td style='width:30px;'>SR#</td> ");
        sb.Append(" <td style='width:70px;'>Crew#</td> ");
        sb.Append(" <td style='text-align:left;'> Crew Name</td> ");
        sb.Append(" <td style='text-align:left;'>Rank Name</td> ");
        //sb.Append(" < td style = 'width:100px;' > Missing Date</td> ");
        for (int i = 1; i <= NoOfDaysInMonth; i++)
        {
            sb.Append("<td> " + i.ToString()); sb.Append("</td>");
        }
        sb.Append(" </tr>");
        int c = 1;
        foreach (DataRow Dr in dv.ToTable().Rows)
        {
            DateTime? SignOnDate = Convert.ToDateTime(Dr["SignOnDate"]);
            DateTime? SignOffDate = null;
            if(!(Convert.IsDBNull(Dr["SignOffDate"])))
                SignOffDate = Convert.ToDateTime(Dr["SignOffDate"]);


            sb.Append("<tr>");
            //--------------------
            sb.Append("<td > " + c.ToString() + " </td>");
            sb.Append("<td > " + Dr["CREWNUMBER"].ToString() + ""); sb.Append("</td>");
            sb.Append("<td style='text-align:left;'> " + Dr["CREWNAME"].ToString() + ""); sb.Append("</td>");
            sb.Append("<td style='text-align:left;'> " + Dr["RANKNAME"].ToString() + ""); sb.Append("</td>");
            for (int i = 1; i <= NoOfDaysInMonth; i++)
            {
                DateTime calcdate = new DateTime(Year, Month, i);
                if (calcdate >= SignOnDate && (SignOffDate == null || SignOffDate >= calcdate))
                {
                    sb.Append("<td class=" + ISNC(dtNC, Dr["CREWNUMBER"].ToString(), i) + "> " + Ispresent(dtData, Dr["CREWNUMBER"].ToString(), i) + "");
                    sb.Append("</td>");
                }
                else
                {
                    sb.Append("<td style='background-color:#c2c2c2'>&nbsp;</td>");
                    sb.Append("</td>");
                }
            }
            //--------------------
            sb.Append("</tr>");
            c++;
        }
        sb.Append("</table>");
        litData.Text = sb.ToString();
    }

    public string Ispresent(DataTable data, string CrewNumber, int d)
    {
        DataView dv = data.DefaultView;
        dv.RowFilter = "CrewNumber='" + CrewNumber + "' and Date_Part=" + d;
        if (dv.ToTable().Rows.Count > 0)
        {
            DataRow dr = dv.ToTable().Rows[0];
            if (dr["Status"].ToString() == "P")
                return "<div> <img src='../Images/exclamation-mark-yellow.png'>  <div>";
            else
                return "<div> <img src='../Images/checked-mark-green.png'>  <div>";
        }
        else
            return "<div> <img src='../Images/exclamation-mark-Red.png'>  <div>";


    }
    public string ISNC(DataTable data, string CrewNumber, int d)
    {
        DataView dv = data.DefaultView;
        dv.RowFilter = "CrewNumber='" + CrewNumber + "' and Date_Part=" + d;
        if (dv.ToTable().Rows.Count > 0)
            return "NC";
        else
            return "";
    }
}
