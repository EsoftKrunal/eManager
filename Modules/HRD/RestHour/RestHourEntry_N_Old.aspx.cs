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

public partial class VIMS_RestHourEntry_N_Old : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public string DayId
    {
        get { return ViewState["DayId"].ToString(); }
        set { ViewState["DayId"] = value; }
    }
    public int PageDateLine
    {
        get { return Common.CastAsInt32(ViewState["DateLine"]); }
        set { ViewState["DateLine"] = value; }
    }
    public int ContractID
    {
        get { return Common.CastAsInt32(ViewState["_ContractID"]); }
        set { ViewState["_ContractID"] = value; }
    }
    public int CrewID
    {
        get { return Common.CastAsInt32(ViewState["_CrewID"]); }
        set { ViewState["_CrewID"] = value; }
    }
    public string CrewNumber
    {
        get { return ViewState["CrewNumber"].ToString(); }
        set { ViewState["CrewNumber"] = value; }
    }
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        //ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //***********Code to check page acessing Permission

        lblMsg.Text = "";

        if (!IsPostBack)
        {
            VesselCode = Request.QueryString["vsc"];
            CrewNumber = Request.QueryString["CrewNumber"];
            ContractID = Common.CastAsInt32( Request.QueryString["t"]);
            CrewID = Common.CastAsInt32(Request.QueryString["CrewID"]);
            LoadMonth(ddlMonth);
            LoadYear(ddlYear);
            DayId = "day_" + DateTime.Today.Day.ToString();
            showcrewdata();
           
        }
    }
    protected void showcrewdata()
    {
        string sql = " select cp.crewNumber,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS crewname,ch.signondate  "+
                     "   from DBO.get_Crew_History ch " +
                     "   inner " +
                     "   join DBO.crewpersonalDetails cp on cp.crewid = ch.CrewID " +
                     "   where CrewNumber = '"+CrewNumber+"' and ch.ContractID ="+ContractID;

        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.PMS_CREW_HISTORY WHERE CREWNUMBER='" + CrewNumber + "'");
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.PMS_CREW_HISTORY WHERE CREWNUMBER='" + CrewNumber + "'");
        if (dt.Rows.Count > 0)
        {
            lblcrewnumber.Text = dt.Rows[0]["crewnumber"].ToString();
            lblname.Text = dt.Rows[0]["crewname"].ToString();
            lblfdt.Text = Common.ToDateString(dt.Rows[0]["signondate"]);
            //lbltdt.Text = Common.ToDateString(dt.Rows[0]["reliefduedate"]);
        }
    }
    protected void showlog(DataRow dr)
    {
        int day = Common.CastAsInt32(DayId.Replace("r", "").Replace("day_", ""));
        string _fordate= new DateTime(Common.CastAsInt32(ddlYear.SelectedValue), Common.CastAsInt32(ddlMonth.SelectedValue), day).ToString("dd-MMM-yyyy");
        string Log = "";
        radStatus.ClearSelection();
        txtRemarks.Text = "";
        if (dr != null)
        {
            Log = dr["WorkLog"].ToString();
            radStatus.SelectedValue= dr["STATUS"].ToString();

            //try
            //{ lbllocation.Text = (dr["Location"].ToString()=="I")?"In Port":((dr["Location"].ToString() == "A") ? "At Sea":""); }
            //catch{ lbllocation.Text = ""; }

            txtRemarks.Text =dr["Remarks"].ToString();
        }

        char[] logparts=Log.ToCharArray();
        StringBuilder sb = new StringBuilder();
        DateTime dt=DateTime.Today;
        sb.Append("<table border='1' cellpadding='6'>");
        sb.Append("<tr>");            
        for (int i=1;i<=24;i++)
        {
            sb.Append("<td colspan='2'>" + dt.ToString("hh:mm") + "</td>");
            dt = dt.AddHours(1);
        }
        sb.Append("</tr>");
        sb.Append("<tr>");
        for (int i = 1; i <= 48; i++)
        {
            string classname = "rest";
            char wr = 'R';
            try
            {
                wr = logparts[i - 1];
                classname = ( (wr=='W') ? "work" : "rest");
            }
            catch { }
            sb.Append("<td class='timeslot " + classname + "' index='" + (i-1) + "'>" + wr + "</td>");
            dt = dt.AddHours(1);
        }
        sb.Append("</tr>");
        sb.Append("</table>");
        
        litLog.Text = sb.ToString();
    }
    protected void showcalender()
    {
        DataTable dtDays  = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RH_DateLineSetting WHERE VESSELCODE='" + VesselCode + "' AND MONTH(RPeriod)=" + ddlMonth.SelectedValue + " AND YEAR(RPeriod)=" + ddlYear.SelectedValue + " ORDER BY RPeriod,DATELINE");
        DataTable dtCrewLog = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RH_CrewMonthData WHERE VESSELCODE='" + VesselCode + "' AND CREWNUMBER='" + CrewNumber + "' AND MONTH(FORDATE)=" + ddlMonth.SelectedValue + " AND YEAR(FORDATE)=" + ddlYear.SelectedValue + " ORDER BY FORDATE,DATELINE");
        DataTable dtNC = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RH_NCList WHERE VESSELCODE='" + VesselCode + "' AND CREWNUMBER='" + CrewNumber + "' AND MONTH(FORDATE)=" + ddlMonth.SelectedValue + " AND YEAR(FORDATE)=" + ddlYear.SelectedValue + " ORDER BY FORDATE,DATELINE");

        DateTime startdt = new DateTime(Common.CastAsInt32(ddlYear.SelectedValue), Common.CastAsInt32(ddlMonth.SelectedValue), 1);
        StringBuilder sb = new StringBuilder();
        StringBuilder sbday = new StringBuilder();
        StringBuilder sbselection = new StringBuilder();
        StringBuilder sbdate = new StringBuilder();
        sb.Append("<table width='100%' style='margin:0 auto;'>");
        sbday.Append("<tr>");
        sbdate.Append("<tr  style='background-color:#736e6e;color:white;'>");
        sbselection.Append("<tr>");
        DateTime tmpdt = startdt;
        int i = 1;
        bool repeatdate = false;

        while (i <= DateTime.DaysInMonth(startdt.Year, startdt.Month))
        {
            
            DataRow[] drs = null;
            DateTime dtCalcDate;
            int DateLine = 0;
            //----------------------
            dtCalcDate = tmpdt;
            if (repeatdate)
            {
                drs = dtDays.Select("RPeriod='" + tmpdt.ToString("dd-MMM-yyyy") + "' AND DATELINE=1");
                DateLine = 1;
            }
            else
            {
                drs = dtDays.Select("RPeriod='" + tmpdt.ToString("dd-MMM-yyyy") + "'");
                if (drs.Length > 0)
                    DateLine = Common.CastAsInt32(drs[0]["DateLine"]);
            }
            
            sbdate.Append("<td class='border'><b>" + i.ToString() + "</b></td>");
            string classname = " border";

            if (drs.Length > 0) // data saved in dateline log 
            {
                sbselection.Append("<td class='day border'><input type='radio' name='days' id='day_" + i.ToString() + (repeatdate ? "r" : "") + "' /></td>");
                DataRow[] drcrew = dtCrewLog.Select("ForDate = '" + tmpdt.ToString("dd-MMM-yyyy") + "' AND DATELINE = " + DateLine);
                if (drcrew.Length > 0)
                {
                    if (drcrew[0]["Status"].ToString() == "S")
                        classname += " saved";

                    if (drcrew[0]["Status"].ToString() == "P")
                        classname += " planned";
                }

                DataRow[] DrNC = dtNC.Select("FORDATE='" + tmpdt.ToString("dd-MMM-yyyy") + "' and DATELINE=" + DateLine);
                if (DrNC.Length > 0)
                    classname += " nc";

                if (tmpdt.ToString("dd-MMM-yyyy") == DateTime.Today.ToString("dd-MMM-yyyy"))
                    classname += " today";

                sbday.Append("<td class='dateline " + classname + "'><b>" + ((DateLine == 0) ? "" : DateLine.ToString()) + "</b></td>");
            }
            else
            {
                sbselection.Append("<td class='day border'>&nbsp;</td>");
                sbday.Append("<td class='dateline border'>&nbsp;</td>");
            }
            
            // reset 
            if (drs.Length == 2)
            {
                repeatdate = true;
            }
            else
            { 
                repeatdate = false;
                tmpdt = tmpdt.AddDays(1);
                i++;
            }
        }

        sbday.Append("</tr>");
        sbselection.Append("</tr>");
        sbdate.Append("</tr>");
        sb.Append(sbdate.ToString());
        sb.Append(sbday.ToString());
        sb.Append(sbselection.ToString());
        sb.Append("</table>");
        litcalander.Text = sb.ToString();
    }
    protected void ddlMonthYear_Changed(object sender,EventArgs e)
    {
        showcalender();
    }

    protected void btnpost_Click(object sender, EventArgs e)
    {
        DayId = hfdid.Value;
        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        DataRow dr = null;
        int day = Common.CastAsInt32(DayId.Replace("r", "").Replace("day_", ""));
       
        lblForDate.Text = new DateTime(Common.CastAsInt32(ddlYear.SelectedValue), Common.CastAsInt32(ddlMonth.SelectedValue), day).ToString("dd-MMM-yyyy");
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RH_DateLineSetting WHERE VESSELCODE='" + VesselCode + "' AND RPERIOD='" + lblForDate.Text + "' ORDER BY RPeriod,DATELINE");
        if (dt.Rows.Count <= 1)
        {
            if (dt.Rows.Count > 0)
            {
                PageDateLine = Common.CastAsInt32(dt.Rows[0]["DATELINE"]);
                lbllocation.Text = (dt.Rows[0]["Location"].ToString() == "I") ? "In Port" : ((dt.Rows[0]["Location"].ToString() == "A") ? "At Sea" : "");
            }
            //----------------
            DayId = DayId.Replace("r", "");
        }
        else
        {
            if (DayId.EndsWith("r")) // repeat
            {
                PageDateLine = 1; // +1
                lbllocation.Text = (dt.Rows[1]["Location"].ToString() == "I") ? "In Port" : ((dt.Rows[1]["Location"].ToString() == "A") ? "At Sea" : "");
            }
            else
            {
                PageDateLine = 0;
                lbllocation.Text = (dt.Rows[0]["Location"].ToString() == "I") ? "In Port" : ((dt.Rows[0]["Location"].ToString() == "A") ? "At Sea" : "");
            }
        }

        DataTable dtLog = Common.Execute_Procedures_Select_ByQuery("SELECT DATELINE,WORKLOG,STATUS,Remarks FROM DBO.RH_CrewMonthData WHERE CREWNUMBER='" + CrewNumber + "' AND FORDATE='" + lblForDate.Text + "' AND DATELINE=" + PageDateLine + " ORDER BY FORDATE,DATELINE");
        if (dtLog.Rows.Count > 0)
            dr = dtLog.Rows[0];

        showcalender();
        if (!IsPostBack)
        {
            btnsave.Visible = false;
            btnClear.Visible = false;
            lbllocation.Text = "";
            lblForDate.Text = "";
            return;
        }
        showlog(dr);
        if (PageDateLine == -1)
        {
            btnsave.Visible = false;
            btnClear.Visible = false;
        }
        else
        {
            btnsave.Visible = true;
            btnClear.Visible = true;
        }
        lblidl.Text = PageDateLine.ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "faaf", "selectdate('" +  DayId + "');", true);
        ShowAllNC();
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("EXEC DBO.RH_ClearWorkLog '" + CrewNumber + "','" + lblForDate.Text + "'," + PageDateLine + ",'TEST'");
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (radStatus.SelectedIndex<0)
        {
            lblMsg.Text = "Please select status ( Planned or Final Log ).";
            return;

        }
        string log = hfdSlots.Value;
        //double WorkLogCount = 0;
        //double RestLogCount = 0;
        //char[] parts = log.ToCharArray();
        //foreach(char c in parts)
        //{
        //    WorkLogCount += ((c == 'W') ? 0.5 : 0);
        //}
        //RestLogCount = 24 - WorkLogCount;
        Common.Execute_Procedures_Select_ByQuery("EXEC DBO.RH_SaveWorkLog '" + VesselCode + "','" + CrewNumber + "','" + lblForDate.Text + "'," + PageDateLine + ",'" + log + "','"+Session["UserName"].ToString()+"','" + radStatus.SelectedValue + "'");

    }

    public void ShowAllNC()
    {
        int day = Common.CastAsInt32(DayId.Replace("r", "").Replace("day_", ""));
        string date = day + "-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue;
        string sql = " select * from dbo.RH_NCList where VesselCode='" + VesselCode + "' AND CrewNumber='" + CrewNumber + "' And FORDATE= '" + date + "' And DateLine=" + PageDateLine;
        DataTable dtNC = Common.Execute_Procedures_Select_ByQuery(sql);
        rptNC.DataSource = dtNC;
        rptNC.DataBind();

        if (rptNC.Items.Count > 0)
            btnUpdateRemarks.Visible = true;
        else
            btnUpdateRemarks.Visible = false;
    }

    protected void btnUpdateRemarks_OnClick(object sender, EventArgs e)
    {
        if (txtRemarks.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter remarks.";
            return;
        }

        try
        {
            int day = Common.CastAsInt32(DayId.Replace("r", "").Replace("day_", ""));
            string date = day + "-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue;

            Common.Set_Procedures("[DBO].[RH_UpdateRemakrs]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@CrewNumber", CrewNumber),
                new MyParameter("@ForDate", date),
                new MyParameter("@DateLine", PageDateLine),
                new MyParameter("@Remarks", txtRemarks.Text.Trim()),
                new MyParameter("ModifiedBy", Session["UserName"].ToString())
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsg.Text = "Remarks updated successfully.";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error while saving record.";
        }
    }


    //----------------------------------------
    protected void btnPrintWorkRestRecord_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('./Reports/HourReport_N.aspx?v=" + VesselCode + "&c=" + CrewNumber + "&m=" + ddlMonth.SelectedValue + "&y=" + ddlYear.SelectedValue + "&cid="+ContractID+"');", true);
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('./Reports/NCList_N.aspx?v=" + VesselCode.ToString() + "&c=" + CrewNumber + "&m=" + ddlMonth.SelectedValue + "&y=" + ddlYear.SelectedValue + "&cid=" + ContractID + "');", true);
    }


    public void LoadYear(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year - 1), Convert.ToString(DateTime.Today.Year - 1)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year), Convert.ToString(DateTime.Today.Year)));
        Ob.Items.Add(new ListItem(Convert.ToString(DateTime.Today.Year + 1), Convert.ToString(DateTime.Today.Year + 1)));
        Ob.SelectedValue = DateTime.Today.Year.ToString();
    }
    public void LoadMonth(DropDownList Ob)
    {
        Ob.Items.Clear();
        Ob.Items.Add(new ListItem("Jan", Convert.ToString(1)));
        Ob.Items.Add(new ListItem("Feb", Convert.ToString(2)));
        Ob.Items.Add(new ListItem("Mar", Convert.ToString(3)));
        Ob.Items.Add(new ListItem("Apr", Convert.ToString(4)));
        Ob.Items.Add(new ListItem("May", Convert.ToString(5)));
        Ob.Items.Add(new ListItem("Jun", Convert.ToString(6)));
        Ob.Items.Add(new ListItem("Jul", Convert.ToString(7)));
        Ob.Items.Add(new ListItem("Aug", Convert.ToString(8)));
        Ob.Items.Add(new ListItem("Sep", Convert.ToString(9)));
        Ob.Items.Add(new ListItem("Oct", Convert.ToString(10)));
        Ob.Items.Add(new ListItem("Nov", Convert.ToString(11)));
        Ob.Items.Add(new ListItem("Dec", Convert.ToString(12)));
        Ob.SelectedValue = DateTime.Today.Month.ToString();
    }
}

