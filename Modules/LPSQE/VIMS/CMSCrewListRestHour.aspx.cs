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
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Xml;
using Ionic.Zip;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public partial class Vims_CMSCrewListRestHour : System.Web.UI.Page
{
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            CurrentVessel = Session["CurrentShip"].ToString();
            ClearTempFiles();
            
            if (!Page.IsPostBack)
            {
                ProjectCommon.LoadMonth(ddlMonth);
                ProjectCommon.LoadYear(ddlYear);
                ddlMonthFilter.SelectedValue = DateTime.Today.Month.ToString();
                // Crew ListdvMSG
                BindYear();
                BindRank();
                ShowCrewList();
            }
        }
    }
    protected void ClearTempFiles()
    {
        string[] files=Directory.GetFiles(Server.MapPath("~/TEMP"));
        foreach (string fl in files)
            try{File.Delete(fl);}catch { }

        string[] folders = Directory.GetDirectories(Server.MapPath("~/TEMP"));
        foreach (string folder in folders)
            try { Directory.Delete(folder, true); }
            catch { }

    }
   
    // Crew List------------------------------------------------------------
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        ShowCrewList();
    }
    public void ShowCrewList()
    {
        string StartDate = "01 " + ddlMonthFilter.SelectedItem.Text + " " + ddlYearFilter.SelectedValue;
        string EndDateNext = Convert.ToDateTime(StartDate).AddMonths(1).ToString("dd MMM yyyy");
        string EndDate = Convert.ToDateTime(StartDate).AddMonths(1).AddDays(-1).ToString("dd MMM yyyy");
        


        string filter = " where 1=1";
        filter = filter + " and (SignOnDate<'"+ Convert.ToDateTime(EndDate).AddDays(1).ToString("dd MMM yyyy")  + "'  and ( SignOffDate>='"+ StartDate + "' or SignOffDate is null) )";

        //if (rdoOnBoardOnLeave.SelectedIndex == 0)
        //{
        //    filter = filter + " and  (SignOffDate is null or SignOffDate >= GETDATE()) ";
        //}
        //else
        //{
        //    filter = filter + " and  (SignOffDate <= GETDATE()) ";
        //}

        if (txtCrewNoName.Text.Trim() != "")
        {
            filter = filter + " and ( CrewNumber='"+ txtCrewNoName.Text.Trim() + "' or CrewName like '%"+ txtCrewNoName.Text.Trim() + "%')";
        }
        if (ddlRank.SelectedIndex != 0)
        {
            filter = filter + " and CH.RankID=" + ddlRank.SelectedValue;
        }


        //if (txtSignOnFrom.Text.Trim() != "")
        //{
        //    filter = filter + " and SignOnDate>='" + txtSignOnFrom.Text.Trim() + "' ";
        //}
        //if (txtSignOnTo.Text.Trim() != "")
        //{
        //    filter = filter + " and SignOnDate<='" + txtSignOnTo.Text.Trim() + "' ";
        //}


        //" ,(select count(1) from RH_CrewMonthData where VesselCode = CH.VesselCode and CrewNumber = CH.CrewNumber and Status='S' and Year(Fordate) = " +ddlYearFilter.SelectedValue+" and MONTH(Fordate) = "+ddlMonthFilter.SelectedValue+")LogDays "+
        string sql = " select *,dbo.fn_Get_DaysRequired(CH.SignOnDate,CH.SignOffDate,'" + StartDate + "', '" + EndDate + "')DaysRequired " +
                     ",(select count(1) from RH_CrewMonthData WHERE VesselCode = CH.VesselCode and CrewNumber = CH.CrewNumber and Status='S' AND FORDATE<(CASE WHEN CH.SIGNOFFDATE<'" + EndDateNext + "' THEN DATEADD(DAY,1,CH.SIGNOFFDATE) ELSE '" + EndDateNext + "' END) AND FORDATE>=(CASE WHEN CH.SIGNONDATE>'" + StartDate + "' THEN CH.SIGNONDATE ELSE '" + StartDate + "' END)) AS LogDays " +
                     ",MP.RankName from PMS_CREW_HISTORY CH left join MP_AllRank MP  on MP.Rankid=CH.Rankid" + filter + " order by CH.Rankid,CH.CrewName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptCrewList.DataSource = dt;
        rptCrewList.DataBind();
    }
    public void BindRank()
    {
        string sql = " select * from MP_AllRank order by RankiD";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "RankCode";
        ddlRank.DataValueField = "RankID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("All", ""));
    }
    public void BindYear()
    {
        //ddlYearFilter.Items.Add(new ListItem("Select", "0"));
        int limit = DateTime.Now.Year - 100;
        for (int yr = DateTime.Now.Year; yr >= limit; yr--)
        {
            ddlYearFilter.Items.Add(new ListItem(yr.ToString(), yr.ToString()));
        }
    }
    


    protected void btnprint_Click(object sender, EventArgs e)
    {
        if(radtype.SelectedIndex==0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('./Reports/HourReport.aspx?v=" + CurrentVessel + "&c=" + ViewState["crewnumber"] + "&m=" + ddlMonth.SelectedValue + "&y=" + ddlYear.SelectedValue + "&cid=" + ViewState["contractid"] + "');", true);
        else if (radtype.SelectedIndex == 1)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('./Reports/NCList.aspx?v=" + CurrentVessel + "&c=" + ViewState["crewnumber"] + "&m=" + ddlMonth.SelectedValue + "&y=" + ddlYear.SelectedValue + "&cid=" + ViewState["contractid"] + "');", true);

    }
    protected void btnreport_Click(object sender, ImageClickEventArgs e)
    {
        string arg = ((ImageButton)sender).CommandArgument;
        string[] parts = arg.Split(',');
        ViewState["contractid"] = parts[0];
        ViewState["crewnumber"] = parts[1];
        dvModal.Visible = true;
    }
    protected void btnclosemodal_Click(object sender, EventArgs e)
    {
        dvModal.Visible = false;
    }
        
}