using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;
using System.Xml;
using Ionic.Zip;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public partial class Vims_CMSRestHourDateLine : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public string DayId
    {
        get { return ViewState["DayId"].ToString(); }
        set { ViewState["DayId"] = value; }
    }
    public string CurrentShip
    {
        get { return ViewState["CurrentShip"].ToString(); }
        set { ViewState["CurrentShip"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string UserName = Session["UserName"].ToString();
        if (!(UserName == "MASTER"))
        {
            Response.Redirect("~/UnAuthorized.aspx");
        }

        if (!IsPostBack)
        {
            CurrentShip=Session["CurrentShip"].ToString();
            ProjectCommon.LoadMonth(ddlMonth);
            ProjectCommon.LoadYear(ddlYear);
            DayId = "day_" + DateTime.Today.Day.ToString();
            showlog();
        }
    }
    protected void btnpost_Click(object sender, EventArgs e)
    {
        DayId = hfdid.Value;
    }
    protected void showcalender()

    {
        //  
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RH_DateLineSetting WHERE MONTH(RPERIOD)=" + ddlMonth.SelectedValue + " AND YEAR(RPERIOD)=" + ddlYear.SelectedValue + " ORDER BY RPERIOD,DATELINE");
        DataTable dtNC = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RH_NCList WHERE MONTH(ForDate)=" + ddlMonth.SelectedValue + " AND YEAR(ForDate)=" + ddlYear.SelectedValue + " ORDER BY ForDate,DateLine");

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
            int DayMode = 0;
            DataRow[] drs = null;
            if (repeatdate)
                drs = dtDays.Select("RPERIOD='" + tmpdt.ToString("dd-MMM-yyyy") + "' AND DATELINE=1");
            else
                drs = dtDays.Select("RPERIOD='" + tmpdt.ToString("dd-MMM-yyyy") + "'");

            string classname = " border";
            if (drs.Length == 1)
            {
                DayMode = Common.CastAsInt32(drs[0]["DateLine"]);
            }
            else if (drs.Length == 2)
            {
                DayMode = Common.CastAsInt32(drs[0]["DateLine"]);
            }
            if (drs.Length > 0)
                classname += " saved";

            sbdate.Append("<td class='border'><b>" + i.ToString() + "</b></td>");
            sbselection.Append("<td class='day border'><input type='radio' name='days' id='day_" + i.ToString() + (repeatdate ? "r" : "") + "' /></td>");
            sbday.Append("<td class='dateline " + classname + "'><b>" + ((DayMode == 0) ? "" : DayMode.ToString()) + "</b></td>");

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
    protected void ddlMonthYear_Changed(object sender, EventArgs e)
    {
        showcalender();
        showlog();
    }
    public void showlog()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.GET_CREW_PENDING_RH_LOG '" + CurrentShip + "'," + ddlMonth.SelectedValue + "," + ddlYear.SelectedValue);
        DataView dv = dt.DefaultView;
        DataTable dt1 = dv.ToTable(true, "crewnumber", "crewname");
        if (dt1.Rows.Count > 0)
        {
            dvmsglogs.Text = " ( " + dt1.Rows.Count.ToString() + " ) Crew(s) missing logs in this month. Click for more details.";
        }
        else
        {
            dvmsglogs.Text = "";
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        DataRow dr = null;
        int day = Common.CastAsInt32(DayId.Replace("r", "").Replace("day_", ""));
        lblForDate.Text = new DateTime(Common.CastAsInt32(ddlYear.SelectedValue), Common.CastAsInt32(ddlMonth.SelectedValue), day).ToString("dd-MMM-yyyy");

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.RH_DateLineSetting WHERE VESSELCODE='" + CurrentShip + "' AND RPERIOD='" + lblForDate.Text + "' ORDER BY RPERIOD,DATELINE");
        if (dt.Rows.Count <= 1)
        {
            int k = 0;
            if (dt.Rows.Count > 0)
                k = Common.CastAsInt32(dt.Rows[0]["DATELINE"]);

            if (k == 0)
                radDateLine.SelectedIndex = 0;

            if (k == -1)
                radDateLine.SelectedIndex = 1;

            //----------------

            datelineheader.Visible = true;
            datelinectl.Visible = true;
            DayId = DayId.Replace("r", "");

            if (dt.Rows.Count > 0)
                dr = dt.Rows[0];

        }
        else
        {
            if (DayId.EndsWith("r")) // repeat
            {
                datelineheader.Visible = true;
                datelinectl.Visible = true;

                radDateLine.SelectedIndex = 2; // +1
                dr = dt.Rows[1];
            }
            else
            {
              
                datelineheader.Visible = false;
                datelinectl.Visible = false;

                radDateLine.SelectedIndex = 0;
                dr = dt.Rows[0];
            }
        }

       showcalender();
       
        if (dr != null)
            radLocation.SelectedValue = dr["Location"].ToString();

       int DateLine = Common.CastAsInt32(radDateLine.SelectedValue);
       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "faaf", "selectdate('" + DayId + "');", true);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (radDateLine.SelectedIndex < 0)
        {
            lblMsg.Text = "Please select International DateLine.";
            return;
        }
        if (radLocation.SelectedIndex < 0)
        {
            lblMsg.Text = "Please select ship location.";
            return;

        }

        string SetupStartingDate = "2016-12-01 00:00:00";
        if (Convert.ToDateTime(lblForDate.Text) < Convert.ToDateTime(SetupStartingDate))
        {
            lblMsg.Text = "Please start setup from " + Convert.ToDateTime(SetupStartingDate).ToString("dd-MMM-yyyy") + ".";
            return;
        }
        string sql = " SELECT top 1 RPeriod FROM DBO.RH_DateLineSetting Where VesselCode='" + CurrentShip + "' and RPeriod>='"+ SetupStartingDate + "' and  RPeriod<'"+lblForDate.Text+"' order by RPeriod desc  ";
        DataTable DtLastEntry = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtLastEntry.Rows.Count > 0)
        {
            if (Convert.ToDateTime(DtLastEntry.Rows[0][0].ToString()) != Convert.ToDateTime(lblForDate.Text).AddDays(-1))
            {
                lblMsg.Text = "Please save data for previous date first.";
                return;
            }
        }
        else
        {
            if (Convert.ToDateTime(lblForDate.Text) != Convert.ToDateTime(SetupStartingDate))
            {
                lblMsg.Text = "Please start setup from "+Convert.ToDateTime(SetupStartingDate).ToString("dd-MMM-yyyy") +".";
                return;
            }
        }

        int DateLine = Common.CastAsInt32(radDateLine.SelectedValue);
        string DL = "NULL";
        if (radDateLine.Visible)
            DL = DateLine.ToString();

        Common.Execute_Procedures_Select_ByQuery("EXEC DBO.RH_ChangeDateLine '" + CurrentShip + "','" + lblForDate.Text + "'," + DL + ",'" + radLocation.SelectedValue + "','TEST'");
        //--------- sent
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DateTime startdt = new DateTime(Common.CastAsInt32(ddlYear.SelectedValue), Common.CastAsInt32(ddlMonth.SelectedValue), 1);

        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(*) from RH_DateLineSetting WHERE VESSELCODE = '" + CurrentShip +"' AND MONTH(RPERIOD) = " + ddlMonth.SelectedValue + " AND YEAR(RPERIOD) =" + ddlYear.SelectedValue);
        int SavedRecords = Common.CastAsInt32(dtDays.Rows[0][0]);
        int NoOfDaysInMonth =DateTime.DaysInMonth(startdt.Year, startdt.Month);
        if(SavedRecords<NoOfDaysInMonth)
        {
            lblMsg.Text = "Dates not assigned for entry.";
            return;
        }
        DataTable dtNCwithoutRemarks = Common.Execute_Procedures_Select_ByQuery("SELECT count(*) from RH_NCList WHERE VESSELCODE='" + CurrentShip + "' AND MONTH(FORDATE)=" + ddlMonth.SelectedValue + " AND YEAR(FORDATE)=" + ddlYear.SelectedValue + " AND LTRIM(RTRIM(ISNULL(REMARKS,'')))=''");
        int NCwithoutRemarks = Common.CastAsInt32(dtNCwithoutRemarks.Rows[0][0]);
        if (NCwithoutRemarks > 0)
        {
            lblMsg.Text = "Please check remark is not updated for all NC in selected month. for more details please check NC Reports";
            return;
        }
        
        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", CurrentShip),
                new MyParameter("@RecordType", "RESTHOUR-MONTH-PACKET"),
                new MyParameter("@RecordId", startdt.ToString("MMM-yyyy")),
                new MyParameter("@RecordNo", startdt.ToString("MMM-yyyy")),
                new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('" + ds.Tables[0].Rows[0][0].ToString() + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Sent for export successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + Common.getLastError() + "');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + ex.Message + "');", true);
        }
    }



    protected void dvmsglogs_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fasd", "window.open('RHMissingLog.aspx?Month=" + ddlMonth.SelectedValue + "&Year=" + ddlYear.SelectedValue + "','');",true);
    }
}

