using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Collections.Generic;

public partial class RestHourEntry : System.Web.UI.Page
{
    int VesselId = 0;
    public int CrewId
    {
        set
        {
            ViewState["CrewId"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["CrewId"]);
        }
    }
    public int ContractId
    {
        set
        {
            ViewState["ContractId"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["ContractId"]);
        }
    }
    public DateTime? SignOnDate
    {
        set
        {
            ViewState["SignOnDate"] = value;
        }
        get
        {
            try
            {
                DateTime dt = DateTime.Parse(ViewState["SignOnDate"].ToString());
                return dt;
            }
            catch { return null; }
        }
    }
    public DateTime? SignOffDate
    {
        set
        {
            ViewState["SignOffDate"] = value;
        }
        get
        {
            try
            {
                DateTime dt = DateTime.Parse(ViewState["SignOffDate"].ToString());
                return dt;
            }
            catch { return null; }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        VesselId = Common.CastAsInt32(Request.QueryString["v"]);
        ContractId = Common.CastAsInt32(Request.QueryString["t"]);
        if (!Page.IsPostBack)
        {
            CrewId = Common.CastAsInt32(Request.QueryString["c"]);

            for (int i = 2012; i <= DateTime.Today.Year; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ShowCrewData();
            ddlYear.SelectedValue = Request.QueryString["y"];
            ddlMonth.SelectedValue = Request.QueryString["m"];
            lblSelDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.sp_CP_IUDAILYWORKHRS " + VesselId.ToString() + "," + CrewId.ToString() + ",'" + lblSelDate.Text + "','','','','','C'");
            ddlReason.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CP_NCReason");
            ddlReason.DataTextField = "NCReasonName";
            ddlReason.DataValueField = "NCReasonId";
            ddlReason.DataBind();
            ddlReason.Items.Insert(0, new ListItem("< Select Remark >", "0"));
        }
    }
    public void ShowCrewData()
    {
        string sql = "select cpd.crewnumber,cpd.firstname+ ' ' + cpd.middlename + ' ' + cpd.lastname as  CrewName, " +
                     "rankcode, v.SignOnDate,v.SignOffDate,CrewStatusName " +
                     "from cp_vesselcrewlist v  " +
                     "left join crewpersonaldetails cpd on v.crewid=cpd.crewid " +
                     "left join rank on rank.rankid=v.rankid  " +
                     "left join crewstatus cs on cpd.CrewStatusId=cs.CrewStatusId " +
                     "where v.vesselid=" + VesselId.ToString() + " and v.crewid=" + CrewId.ToString() + " and v.contractid=" + ContractId.ToString();

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            lblCrwNumber.Text = dr["CrewNumber"].ToString();
            lblName.Text = dr["CrewName"].ToString();
            lblRank.Text = dr["rankcode"].ToString();

            DateTime? dt1 = null;
            lblSignOnDate.Text = "";
            if (!Convert.IsDBNull(dr["SignOnDate"]))
            {
                dt1 = Convert.ToDateTime(dr["SignOnDate"]);
                lblSignOnDate.Text = dt1.Value.ToString("dd-MMM-yyyy");
            }
            SignOnDate = dt1;
            dt1 = null;
            if (!Convert.IsDBNull(dr["SignOffDate"]))
            {
                dt1 = Convert.ToDateTime(dr["SignOffDate"]);
            }
            SignOffDate = dt1;

            lblStatus.Text = dr["CREWSTATUSNAME"].ToString();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        litCalender.Text = getCalender(Common.CastAsInt32(ddlMonth.SelectedValue), Common.CastAsInt32(ddlYear.SelectedValue));
        litHoursList.Text = getTimeLine();
    }
    protected void YearMonth_Changed(object sender, EventArgs e)
    {

    }
    protected void btnCalPost_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = new DateTime(Common.CastAsInt32(ddlYear.SelectedValue), Common.CastAsInt32(ddlMonth.SelectedValue), Common.CastAsInt32(txtCalDate.Text));
            lblSelDate.Text = dt.ToString("dd-MMM-yyyy");
            Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.sp_CP_IUDAILYWORKHRS " + VesselId.ToString() + "," + CrewId.ToString() + ",'" + lblSelDate.Text + "','','','','','C'");
        }
        catch { }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        List<TimeSlot> TsTempRest = new List<TimeSlot>();
        List<TimeSlot> TsTempWork = new List<TimeSlot>();
        DateTime dtSel = DateTime.Parse(lblSelDate.Text);
        //----------------------------
        double StartRest = -1;
        double StartWork = -1;
        char[] Values = txtValues.Text.ToCharArray();
        for (int i = 0; i <= 47; i++)
        {
            //-------------------------
            if (Values[i] == 'R')
            {
                if (StartRest == -1)
                    StartRest = i / 2.0;
            }
            else
            {
                if (StartRest != -1)
                {
                    double EndRest = (i / 2.0) - .5;
                    TsTempRest.Add(new TimeSlot(dtSel, StartRest, EndRest));
                    StartRest = -1;
                }
            }
            //-------------------------
            if (Values[i] == 'W')
            {
                if (StartWork == -1)
                    StartWork = i / 2.0;
            }
            else
            {
                if (StartWork != -1)
                {
                    double EndWork = (i / 2.0) - .5;
                    TsTempWork.Add(new TimeSlot(dtSel, StartWork, EndWork));
                    StartWork = -1;
                }
            }
        }
        if (StartRest != -1)
        {
            double EndRest = 23.5;
            TsTempRest.Add(new TimeSlot(dtSel, StartRest, EndRest));
            StartRest = -1;
        }
        if (StartWork != -1)
        {
            double EndWork = 23.5;
            TsTempWork.Add(new TimeSlot(dtSel, StartWork, EndWork));
            StartWork = -1;
        }
        ///---------------------------
        ///
        string HrsListRest = "", HrsList1Rest = "";
        for (int i = 0; i < TsTempRest.Count; i++)
        {
            HrsListRest = HrsListRest + "," + TsTempRest[i].FromTime;
            HrsList1Rest = HrsList1Rest + "," + TsTempRest[i].ToTime;
        }
        if (HrsListRest.Trim() != "") { HrsListRest = HrsListRest.Substring(1); };
        if (HrsList1Rest.Trim() != "") { HrsList1Rest = HrsList1Rest.Substring(1); };

        string HrsListWork = "", HrsList1Work = "";
        for (int i = 0; i < TsTempWork.Count; i++)
        {
            HrsListWork = HrsListWork + "," + TsTempWork[i].FromTime;
            HrsList1Work = HrsList1Work + "," + TsTempWork[i].ToTime;
        }

        if (HrsListWork.Trim() != "") { HrsListWork = HrsListWork.Substring(1); };
        if (HrsList1Work.Trim() != "") { HrsList1Work = HrsList1Work.Substring(1); };
        //----------------------------
        Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.sp_CP_IUDAILYWORKHRS " + VesselId.ToString() + "," + CrewId.ToString() + ",'" + dtSel.ToString("dd-MMM-yyyy") + "','" + HrsListRest + "','" + HrsList1Rest + "','" + HrsListWork + "','" + HrsList1Work + "','P'");
        //----------------------------
        if (txtNC.Text.Trim() != "")
        {
            Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.sp_CP_IUDNCReason " + VesselId.ToString() + "," + CrewId.ToString() + ",'" + lblSelDate.Text + "','" + ddlReason.SelectedValue + "'");
        }
    }
    protected void btnPrintComm_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('./Reports/NCList.aspx?v=" + VesselId.ToString() + "&c=" + CrewId.ToString() + "&m=" + ddlMonth.SelectedValue + "&y=" + ddlYear.SelectedValue + "');", true);
    }
    protected void btnPrintSheet_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('./Reports/HourReport.aspx?v=" + VesselId.ToString() + "&c=" + CrewId.ToString() + "&m=" + ddlMonth.SelectedValue + "&y=" + ddlYear.SelectedValue + "&cid=" + ContractId + "');", true);
    }
    //public void MakePendignDays(DateTime dt)
    //{
    //    for (int i = -6; i <= 6; i++)
    //    {
    //        DateTime dt1 = dt.AddDays(i);
    //        if (getSlotForDay(dt1).Count <= 0)
    //        {
    //            TimeSlotList.Add(new PrimaryKey(dt1, 0), new TimeSlot(dt1, 0, 23.5));
    //        }
    //    }
    //}
    //protected void RemoveSlotforDate(DateTime dt)
    //{
    //    for (int i = TimeSlotList.Count - 1; i >= 0; i--)
    //    {
    //        TimeSlot ts=TimeSlotList[TimeSlotList.Keys[i]];  
    //        if (ts.PK.Date == dt)
    //        {
    //            TimeSlotList.RemoveAt(i);
    //            if (ts.PK.Date > dt)
    //            {break;}
    //        }
    //    }
    //}
    public string getCalender(int Month, int Year)
    {
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CP_NonConformance WHERE VESSELID=" + VesselId + " AND CREWID=" + CrewId.ToString() + " AND  YEAR(NCDATE)=" + Year.ToString() + " AND MONTH(NCDATE)=" + Month.ToString());
        DataTable DtRH = Common.Execute_Procedures_Select_ByQueryCMS("select transdate,EnteredBy from DBO.CP_CrewDailyWorkRestHours where VESSELID=" + VesselId + " and CREWID=" + CrewId.ToString() + " and YEAR(transdate)=" + Year.ToString() + " AND MONTH(transdate)=" + Month.ToString());
        StringBuilder sb = new StringBuilder();
        DateTime D1 = new DateTime(Year, Month, 1);
        DateTime D2 = D1.AddMonths(1);
        D2 = D2.AddDays(-1);
        int Cnt = 1;
        int MaxDays = DateTime.DaysInMonth(Year, Month);
        int Startkey = (int)D1.DayOfWeek;
        int Day = 1;
        for (int i = 1; i <= 6; i++)
        {
            sb.Append("<tr>");
            for (int j = 1; j <= 7; j++)
            {
                if (Startkey <= Cnt - 1 && Day <= MaxDays)
                {
                    DateTime dtCalc = new DateTime(Year, Month, Day);
                    DataRow[] Drs = Dt.Select("NCDATE=#" + dtCalc.ToString("dd-MMM-yyyy") + "#");
                    DataRow[] DrsRH = DtRH.Select("transdate=#" + dtCalc.ToString("dd-MMM-yyyy") + "#");
                    string cls = "caltd";

                    if (dtCalc == DateTime.Today)
                    {
                        if (Drs.Length > 0)
                        {
                            if (dtCalc.ToString("dd-MMM-yyyy") == lblSelDate.Text.Trim())
                            {
                                cls = "seltdTNC";
                            }
                            else
                            {
                                cls = "caltdTNC";
                            }
                        }
                        else
                        {
                            if (dtCalc.ToString("dd-MMM-yyyy") == lblSelDate.Text.Trim())
                            {
                                cls = "seltdT";
                            }
                            else
                            {
                                cls = "caltdT";
                            }
                        }
                    }
                    if (dtCalc.ToString("dd-MMM-yyyy") == lblSelDate.Text.Trim())
                    {

                        bool NC7 = false, NC24 = false, NC6 = false, NC2 = false;
                        for (int t = 0; t <= Drs.Length - 1; t++)
                        {
                            if (Drs[t]["NCType"].ToString() == "7")
                                NC7 = true;
                            if (Drs[t]["NCType"].ToString() == "24")
                                NC24 = true;
                            if (Drs[t]["NCType"].ToString() == "6")
                                NC6 = true;
                            if (Drs[t]["NCType"].ToString() == "2")
                                NC2 = true;
                        }
                        txtNC.Text = (NC7) ? "Minimum 77 Hrs Rest in Each 7 Day Period." : "";
                        txtNC.Text += (NC24) ? "\nMinimum 10 Hrs Rest in Any 24 Hrs Period." : "";
                        txtNC.Text += (NC6) ? "\nNo Minimum 6 Hrs Consecutive Rest Period." : "";
                        txtNC.Text += (NC2) ? "\nMinimum Total Rest Comprises More Than 2 Periods." : "";
                        dvReason.Visible = NC7 || NC24 || NC6 || NC2;
                        DataTable dtLocation = Common.Execute_Procedures_Select_ByQueryCMS("Select Location from CP_CrewDailyLocation where VesselId=" + VesselId + " And CrewId=" + CrewId.ToString() + " AND TDate='" + lblSelDate.Text.Trim() + "'");
                        if (dtLocation.Rows.Count > 0)
                        {
                            Label1.Text = (dtLocation.Rows[0][0].ToString() == "0") ? "At Sea" : "In Port";
                        }
                        else
                        {
                            Label1.Text = "";
                        }
                        DataTable DtReasons = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REASON FROM CP_NonConformanceReason WHERE VESSELID=" + VesselId + " AND CREWID=" + CrewId.ToString() + " AND NCDATE='" + lblSelDate.Text.Trim() + "'");
                        if (DtReasons.Rows.Count > 0)
                            ddlReason.SelectedValue = DtReasons.Rows[0][0].ToString();
                        else
                            ddlReason.SelectedValue = "0";

                        if (Drs.Length > 0)
                        {
                            cls = "seltdNC";
                        }
                        else
                        {
                            if (dtCalc == DateTime.Today)
                            {
                                cls = "seltdT";
                            }
                            else
                            {
                                if (DrsRH.Length > 0)
                                    if (DrsRH[0]["EnteredBy"].ToString() == "True")
                                        cls = "seltdA";
                                    else
                                        cls = "seltd";
                            }
                        }
                    }
                    else
                    {
                        if (dtCalc != DateTime.Today)
                        {
                            if (Drs.Length > 0)
                            {
                                cls = "caltdNC";
                            }
                            else
                            {
                                if (DrsRH.Length > 0)
                                    if (DrsRH[0]["EnteredBy"].ToString() == "True")
                                        cls = "caltdA";
                                    else
                                        cls = "caltd";
                                else
                                    cls = "caltd";
                            }
                        }
                    }
                    sb.Append("<td class='" + cls + "' id='td" + i.ToString() + j.ToString() + "' onclick='SetDay(this)'>" + Day.ToString() + "</td>");
                    Day++;
                }
                else
                {
                    sb.Append("<td style='height :15px;' id='td" + i.ToString() + j.ToString() + "'></td>");
                }
                Cnt++;
            }
            sb.Append("<tr>");
        }
        return sb.ToString();
    }
    public string getTimeLine()
    {
        StringBuilder sb = new StringBuilder();
        DateTime dtSel = DateTime.Parse(lblSelDate.Text);
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("EXEC sp_CP_GETDAILYWORKHRS " + VesselId.ToString() + "," + CrewId.ToString() + ",'" + dtSel.ToString("dd-MMM-yyyy") + "'");
        double Rest = 0, Work = 0;
        for (int i = 0; i <= 47; i++)
        {
            DataRow dr = DT.Rows[i];
            if (dr["WR"].ToString() == "R")
                Rest += .5;
            else
                Work += .5;
            //string Res = "<td class='" + dr["WR"].ToString() + "' onmousemove='FillMe(this)' onmousedown='StartMe(this)' onmouseup='EndMe()' onclick='ClickMe(this)'>" + dr["WR"].ToString() + "</td>";
            string Res = "<td class='" + dr["WR"].ToString() + "' >" + dr["WR"].ToString() + "</td>";
            sb.Append(Res);
            //------------
            dvRest.InnerText = Math.Round(Rest, 1).ToString();
            dvWork.InnerText = Math.Round(Work, 1).ToString();
        }
        return sb.ToString();
    }
}
