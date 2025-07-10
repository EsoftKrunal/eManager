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
public partial class PMS_UserControls_CMSCrewList : System.Web.UI.Page
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
                // Crew ListdvMSG
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
        string filter = " where 1=1";
        if (rdoOnBoardOnLeave.SelectedIndex == 0)
        {
            filter = filter + " and  (SignOffDate is null or SignOffDate >= GETDATE()) ";
        }
        else
        {
            filter = filter + " and  (SignOffDate <= GETDATE()) ";
        }
        if (txtCrewNoName.Text.Trim() != "")
        {
            filter = filter + " and ( CrewNumber='"+ txtCrewNoName.Text.Trim() + "' or CrewName like '%"+ txtCrewNoName.Text.Trim() + "%')";
        }
        if (ddlRank.SelectedIndex != 0)
        {
            filter = filter + " and CH.RankID=" + ddlRank.SelectedValue;
        }
        if (txtSignOnFrom.Text.Trim() != "")
        {
            filter = filter + " and SignOnDate>='" + txtSignOnFrom.Text.Trim() + "' ";
        }
        if (txtSignOnTo.Text.Trim() != "")
        {
            filter = filter + " and SignOnDate<='" + txtSignOnTo.Text.Trim() + "' ";
        }
        

        string sql = " select *,MP.RankName from PMS_CREW_HISTORY CH left join MP_AllRank MP  on MP.Rankid=CH.Rankid" + filter + " order by CH.Rankid,CH.CrewName ";
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