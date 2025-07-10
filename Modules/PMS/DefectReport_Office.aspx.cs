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

public partial class DefectReport_Office : System.Web.UI.Page
{
    AuthenticationManager Auth;
    bool setScroll = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        //***********Code to check page acessing Permission
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1042, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        //***********
        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 8;
            BindYear();
            BindFleet();
            BindVessels();

            ddlDefectStatus.SelectedValue = "1";
            btnSearch_Click(sender, e);

        }
    }
    protected void BindYear()
    {
        for (int i = DateTime.Today.Year; i >= 2011; i--)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        } 
        ddlYear.Items.Insert(0, new ListItem("< All >", "0"));

        for (int i = DateTime.Today.Year; i >= 2011; i--)
        {
            ddlBYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlBYear.Items.Insert(0, new ListItem("< All >", "0"));
        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        lblRecordCount.Text = "( " + rptDefects.Items.Count + " ) records found.";   
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('rptDefects');", true);
    }

    #region ---------------- USER DEFINED FUNCTIONS ---------------------------
    public void BindFleet()
    {
        try
        {
            DataTable dtFleet = Common.Execute_Procedures_Select_ByQuery("SELECT FleetId,FleetName as Name FROM dbo.FleetMaster");
            this.ddlFleet.DataSource = dtFleet;
            this.ddlFleet.DataValueField = "FleetId";
            this.ddlFleet.DataTextField = "Name";
            this.ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem("< ALL Fleet >", "0"));

            // For unplanned Jobs
            this.ddlFleetUPJ.DataSource = dtFleet;
            this.ddlFleetUPJ.DataValueField = "FleetId";
            this.ddlFleetUPJ.DataTextField = "Name";
            this.ddlFleetUPJ.DataBind();
            ddlFleetUPJ.Items.Insert(0, new ListItem("< ALL Fleet >", "0"));

            this.ddlBFleet.DataSource = dtFleet;
            this.ddlBFleet.DataValueField = "FleetId";
            this.ddlBFleet.DataTextField = "Name";
            this.ddlBFleet.DataBind();
            ddlBFleet.Items.Insert(0, new ListItem("< ALL Fleet >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM dbo.Vessel WHERE VESSELSTATUSID=1 AND ISNULL(IsExported,0) = 1 ORDER BY VesselName";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();

            // For unplanned Jobs
            ddlVesselUPJ.DataSource = dtVessels;
            ddlVesselUPJ.DataTextField = "VesselName";
            ddlVesselUPJ.DataValueField = "VesselCode";
            ddlVesselUPJ.DataBind();


            ddlBVessel.DataSource = dtVessels;
            ddlBVessel.DataTextField = "VesselName";
            ddlBVessel.DataValueField = "VesselCode";
            ddlBVessel.DataBind();
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();

            // For unplanned Jobs
            ddlVesselUPJ.DataSource = null;
            ddlVesselUPJ.DataBind();

            ddlBVessel.DataSource = null;
            ddlBVessel.DataBind();
        }
        ddlVessels.Items.Insert(0, "< All >");
        ddlVesselUPJ.Items.Insert(0, "< All >");
        ddlBVessel.Items.Insert(0, new ListItem("< All >","0"));
    }
    #endregion
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFleet.SelectedIndex != 0)
        {
            DataTable dtVessels = new DataTable();
            string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel WHERE ISNULL(IsExported,0) = 1 AND VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ") ORDER BY VesselName";
            dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
            ddlVessels.Items.Clear();
            if (dtVessels.Rows.Count > 0)
            {
                ddlVessels.DataSource = dtVessels;
                ddlVessels.DataTextField = "VesselName";
                ddlVessels.DataValueField = "VesselCode";
                ddlVessels.DataBind();
            }
            else
            {
                ddlVessels.DataSource = null;
                ddlVessels.DataBind();
            }
            ddlVessels.Items.Insert(0, "< All >");
        }
        else
        {
            ddlVessels.Items.Clear();
            BindVessels();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strSearch = "SELECT ROW_NUMBER() OVER( ORDER BY DD.VesselCode,CM.ComponentCode) AS SrNo,DD.VesselCode,CM.ComponentId,CM.ComponentCode,CM.ComponentName,DD.DefectNo,REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') AS ReportDt,REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  AS TargetDt,  CASE DD.CompStatus WHEN 'W' THEN 'Working' WHEN 'N' THEN 'Not Working' ELSE ' ' END AS CompStatus ,DD.RqnNo,REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-') AS RqnDate,CM.CriticalType,(Case when CompletionDt is null then 'Open' else 'Closed' end )DefectStatus ,(replace(convert(varchar,CompletionDt,106),' ','-'))CompletionDt, CASE WHEN (DD.TargetDt < getdate() AND CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [Status],( SELECT TOP 1 Remarks FROM VSL_DefectRemarks DR WHERE DR.VesselCode = DD.vesselcode AND DR.DefectNo = DD.DefectNo ORDER BY EnteredOn DESC ) AS Remarks, " +
                           "DefectDetails, " +
                           "( " +
                              "(CASE WHEN Vessel = 'Y' THEN 'Vessel,' ELSE '' END) +" + 
                              "(CASE WHEN Spares = 'Y' THEN 'Spares,' ELSE '' END) +" + 
                              "(CASE WHEN ShoreAssist = 'Y' THEN 'ShoreAssist,' ELSE '' END) + " +
                              "(CASE WHEN Drydock = 'Y' THEN 'Drydock,' ELSE '' END) + " + 
                              "(CASE WHEN Guarentee = 'Y' THEN 'Guarentee' ELSE '' END)" +
                            ") as Responsibility"  +
                            ",(RTRIM(CM.ComponentCode)+ ' : ' + CM.ComponentName) AS Component, FM.FleetName " +
                           "FROM Vsl_DefectDetailsMaster DD INNER JOIN VSL_ComponentMasterForVessel CMV ON CMV.VesselCode = DD.VesselCode AND CMV.ComponentId = DD.ComponentId AND CMV.Status = 'A' AND DD.Status = 'A' INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId " +
                           "INNER JOIN dbo.Vessel V ON CMV.VesselCode = V.VesselCode " +
                           "INNER JOIN dbo.FleetMaster FM ON FM.FleetId = V.FleetId ";

        //string strSearch = "SELECT * FROM dbo.VW_DefectReport_OfficeData ";

        string WhereCondition = "WHERE V.VESSELSTATUSID=1 ";

        if (ddlFleet.SelectedIndex != 0)
        {
            if (ddlVessels.SelectedIndex == 0)
            {
                string Vessels = "";
                for (int i = 0; i < ddlVessels.Items.Count; i++)
                {
                    if (i != 0)
                    {
                        Vessels = Vessels + "'" + ddlVessels.Items[i].Text + "'" + ",";
                    }
                }
                if (Vessels.Length > 0)
                {
                    Vessels = Vessels.Remove(Vessels.Length - 1);
                }
                WhereCondition = WhereCondition + "AND DD.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ")";
            }
            else
            {
                WhereCondition = WhereCondition + "AND DD.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
            }
        }
        else if (ddlVessels.SelectedIndex != 0)
        {
            WhereCondition = WhereCondition + "AND DD.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
        }
        if (ddlYear.SelectedIndex != 0)
        {
            WhereCondition = WhereCondition + " AND year(DD.ReportDt)=" + ddlYear.SelectedValue;
        }
        if (txtCompCode.Text != "")
        {
            WhereCondition = WhereCondition + " AND  CM.ComponentCode LIKE '%" + txtCompCode.Text.Trim().ToString() + "%' ";
        }
        if (txtCompName.Text != "")
        {
            WhereCondition = WhereCondition + " AND CM.ComponentName LIKE '%" + txtCompName.Text.Trim().ToString() + "%' ";
        }
        if (ddlDefectStatus.SelectedIndex != 0)
        {
            if (ddlDefectStatus.SelectedValue == "1")
                WhereCondition = WhereCondition + " AND DD.CompletionDt is null ";
            else
                WhereCondition = WhereCondition + " AND DD.CompletionDt is not null";
        }
        WhereCondition = WhereCondition + " order by DD.VesselCode,CAST(right(left(DD.DefectNo,6),2) AS INT) Desc,CAST( replace(DD.DefectNo,left(DD.DefectNo,7),'') AS INT) Desc ";

        Session["SQLDefectReport_Office"] = strSearch + WhereCondition;

        DataTable dtSearchData = Common.Execute_Procedures_Select_ByQuery(strSearch + WhereCondition);
        if (dtSearchData.Rows.Count > 0)
        {
            rptDefects.DataSource = dtSearchData;
            rptDefects.DataBind(); 
        }
        else
        {
            rptDefects.DataSource = null;
            rptDefects.DataBind();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        ddlVessels.SelectedIndex = 0;
        ddlDefectStatus.SelectedIndex = 0;
        txtCompCode.Text = "";
        txtCompName.Text = "";
    }

    protected void ddlBFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBFleet.SelectedIndex != 0)
        {
            DataTable dtVessels = new DataTable();
            string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel WHERE ISNULL(IsExported,0) = 1 AND VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlBFleet.SelectedValue + ") ORDER BY VesselName";
            dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
            ddlBVessel.Items.Clear();
            if (dtVessels.Rows.Count > 0)
            {
                ddlBVessel.DataSource = dtVessels;
                ddlBVessel.DataTextField = "VesselName";
                ddlBVessel.DataValueField = "VesselCode";
                ddlBVessel.DataBind();
            }
            else
            {
                ddlBVessel.DataSource = null;
                ddlBVessel.DataBind();
            }
            ddlBVessel.Items.Insert(0, new ListItem("< All >", "0"));
        }
        else
        {
            ddlBVessel.Items.Clear();
            BindVessels();
        }
    }
    protected void btnBSearch_Click(object sender, EventArgs e)
    {
        string strSearch = "SELECT ROW_NUMBER() OVER( ORDER BY DD.VesselCode,CM.ComponentCode) AS SrNo,DD.VesselCode,CM.ComponentId,CM.ComponentCode,CM.ComponentName,DD.BreakDownNo,REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') AS ReportDt,REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  AS TargetDt,  CASE DD.CompStatus WHEN 'W' THEN 'Working' WHEN 'N' THEN 'Not Working' ELSE ' ' END AS CompStatus ,DD.RqnNo,REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-') AS RqnDate,CM.CriticalType,(Case when CompletionDt is null then 'Open' else 'Closed' end )DefectStatus ,(replace(convert(varchar,CompletionDt,106),' ','-'))CompletionDt, CASE WHEN (DD.TargetDt < getdate() AND CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [Status],( SELECT TOP 1 Remarks FROM VSL_BreakDownRemarks DR WHERE DR.VesselCode = DD.vesselcode AND DR.BreakDownNo = DD.BreakDownNo ORDER BY EnteredOn DESC ) AS Remarks, " +
                           "DefectDetails, " +
                           "( " +
                              "(CASE WHEN Vessel = 'Y' THEN 'Vessel,' ELSE '' END) +" +
                              "(CASE WHEN Spares = 'Y' THEN 'Spares,' ELSE '' END) +" +
                              "(CASE WHEN ShoreAssist = 'Y' THEN 'ShoreAssist,' ELSE '' END) + " +
                              "(CASE WHEN Drydock = 'Y' THEN 'Drydock,' ELSE '' END) + " +
                              "(CASE WHEN Guarentee = 'Y' THEN 'Guarentee' ELSE '' END)" +
                            ") as Responsibility" +
                            ",(RTRIM(CM.ComponentCode)+ ' : ' + CM.ComponentName) AS Component, FM.FleetName " +
                           "FROM VSL_BreakDownMaster DD INNER JOIN VSL_ComponentMasterForVessel CMV ON CMV.VesselCode = DD.VesselCode AND CMV.ComponentId = DD.ComponentId AND CMV.Status = 'A' AND DD.Status = 'A' INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId " +
                           "INNER JOIN dbo.Vessel V ON CMV.VesselCode = V.VesselCode " +
                           "INNER JOIN dbo.FleetMaster FM ON FM.FleetId = V.FleetId ";

        //string strSearch = "SELECT * FROM dbo.VW_DefectReport_OfficeData ";

        string WhereCondition = "WHERE V.VESSELSTATUSID=1 ";

        if (ddlFleet.SelectedIndex != 0)
        {
            if (ddlVessels.SelectedIndex == 0)
            {
                string Vessels = "";
                for (int i = 0; i < ddlVessels.Items.Count; i++)
                {
                    if (i != 0)
                    {
                        Vessels = Vessels + "'" + ddlVessels.Items[i].Text + "'" + ",";
                    }
                }
                if (Vessels.Length > 0)
                {
                    Vessels = Vessels.Remove(Vessels.Length - 1);
                }
                WhereCondition = WhereCondition + "AND DD.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ")";
            }
            else
            {
                WhereCondition = WhereCondition + "AND DD.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
            }
        }
        else if (ddlVessels.SelectedIndex != 0)
        {
            WhereCondition = WhereCondition + "AND DD.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
        }
        if (ddlYear.SelectedIndex != 0)
        {
            WhereCondition = WhereCondition + " AND year(DD.ReportDt)=" + ddlYear.SelectedValue;
        }
        if (txtCompCode.Text != "")
        {
            WhereCondition = WhereCondition + " AND  CM.ComponentCode LIKE '%" + txtCompCode.Text.Trim().ToString() + "%' ";
        }
        if (txtCompName.Text != "")
        {
            WhereCondition = WhereCondition + " AND CM.ComponentName LIKE '%" + txtCompName.Text.Trim().ToString() + "%' ";
        }
        if (ddlBStatus.SelectedIndex != 0)
        {
            if (ddlBStatus.SelectedValue == "1")
                WhereCondition = WhereCondition + " AND DD.CompletionDt is null ";
            else
                WhereCondition = WhereCondition + " AND DD.CompletionDt is not null";
        }
        WhereCondition = WhereCondition + " order by DD.VesselCode,CAST(right(left(DD.BreakDownNo,6),2) AS INT) Desc,CAST( replace(DD.BreakDownNo,left(DD.BreakDownNo,7),'') AS INT) Desc ";

        Session["SQLDefectReport_Office_bd"] = strSearch + WhereCondition;

        DataTable dtSearchData = Common.Execute_Procedures_Select_ByQuery(strSearch + WhereCondition);
        if (dtSearchData.Rows.Count > 0)
        {
            rptBreakDowns.DataSource = dtSearchData;
            rptBreakDowns.DataBind();
        }
        else
        {
            rptBreakDowns.DataSource = null;
            rptBreakDowns.DataBind();
        }
    }
    protected void btnBClear_Click(object sender, EventArgs e)
    {
        ddlBFleet.SelectedIndex = 0;
        ddlBVessel.SelectedIndex = 0;
        ddlBStatus.SelectedIndex = 0;
        txtBCompCode.Text = "";
        txtBCompName.Text = "";
    }

    //  ------ Tabs
    protected void btnDefect_onclick(object sender, EventArgs e)
    {
        btnDefect.CssClass = "selbtn";
        btnUnPlannedJobs.CssClass = "btn1";
        btnBreakDown.CssClass = "btn1";

        tblDefect.Visible = true;
        tblUnPlannedJobs.Visible = false;
        tblBreakDown.Visible = false;
    }
    protected void btnUnPlannedJobs_onclick(object sender, EventArgs e)
    {
        btnDefect.CssClass = "btn1";
        btnUnPlannedJobs.CssClass = "selbtn";
        btnBreakDown.CssClass = "btn1";

        tblDefect.Visible = false;
        tblUnPlannedJobs.Visible = true;
        tblBreakDown.Visible = false;

        ddlJobStatusUPJ.SelectedValue = "1";
        btnSearchUPJ_Click(sender, e);
    }
    protected void btnBreakDown_onclick(object sender, EventArgs e)
    {
        btnDefect.CssClass = "btn1";
        btnUnPlannedJobs.CssClass = "btn1";
        btnBreakDown.CssClass = "selbtn";
        
        tblDefect.Visible = false;
        tblUnPlannedJobs.Visible = false;
        tblBreakDown.Visible = true;
    }
    

    //  ------ UnPlanned Jobs Event
    protected void btnSearchUPJ_Click(object sender, EventArgs e)
    {
        string strSearch = "SELECT UPJ.VesselCode,UPJ.UPID,UPJ.ShortDescr,CM.ComponentId,CM.ComponentCode,CM.ComponentName,REPLACE(CONVERT(varchar, UPJ.DueDate,106),' ','-') AS DueDate " +
                    " ,CASE WHEN UPJ.DoneDate is null THEN 'Open' ELSE 'Closed' END AS CompletionStatus  " +
                    " ,CM.CriticalType,(replace(convert(varchar,UPJ.DoneDate ,106),' ','-'))CompletionDt  " +
                    " FROM VSL_UnPlannedJobs UPJ  " +
                    " INNER JOIN VSL_ComponentMasterForVessel CMV ON CMV.VesselCode = UPJ.VesselCode AND CMV.ComponentId = UPJ.ComponentId AND CMV.Status = 'A'  " +
                    " INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId ";

        string WhereCondition = "WHERE  1=1 ";

        if (ddlFleetUPJ.SelectedIndex != 0)
        {
            if (ddlVesselUPJ.SelectedIndex == 0)
            {
                string Vessels = "";
                for (int i = 0; i < ddlVesselUPJ.Items.Count; i++)
                {
                    if (i != 0)
                    {
                        Vessels = Vessels + "'" + ddlVesselUPJ.Items[i].Text + "'" + ",";
                    }
                }
                if (Vessels.Length > 0)
                {
                    Vessels = Vessels.Remove(Vessels.Length - 1);
                }
                WhereCondition = WhereCondition + "AND UPJ.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleetUPJ.SelectedValue + ")";
            }
            else
            {
                WhereCondition = WhereCondition + "AND UPJ.VesselCode = '" + ddlVesselUPJ.SelectedValue.ToString() + "' ";
            }
        }
        else if (ddlVesselUPJ.SelectedIndex != 0)
        {
            WhereCondition = WhereCondition + "AND UPJ.VesselCode = '" + ddlVesselUPJ.SelectedValue.ToString() + "' ";
        }
        if (txtCompCodeUPJ.Text != "")
        {
            WhereCondition = WhereCondition + " AND  CM.ComponentCode LIKE '%" + txtCompCodeUPJ.Text.Trim().ToString() + "%' ";
        }
        if (txtCompNameUPJ.Text != "")
        {
            WhereCondition = WhereCondition + " AND CM.ComponentName LIKE '%" + txtCompNameUPJ.Text.Trim().ToString() + "%' ";
        }
        if (ddlJobStatusUPJ.SelectedIndex != 0)
        {
            if (ddlJobStatusUPJ.SelectedValue == "1")
                WhereCondition = WhereCondition + " AND UPJ.DoneDate is null ";
            else
                WhereCondition = WhereCondition + " AND UPJ.DoneDate is not null";
        }
        WhereCondition = WhereCondition + " ORDER BY UPJ.VesselCode,CM.ComponentCode";

        DataTable dtSearchData = Common.Execute_Procedures_Select_ByQuery(strSearch + WhereCondition);
        if (dtSearchData.Rows.Count > 0)
        {
            rptUnPlannedJobs.DataSource = dtSearchData;
            rptUnPlannedJobs.DataBind();
        }
        else
        {
            rptDefects.DataSource = null;
            rptDefects.DataBind();
        }
        lblCountUPJ.Text = "(" + dtSearchData .Rows.Count+ " )Records Found.";
    }
    protected void ddlFleetUPJ_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFleetUPJ.SelectedIndex != 0)
        {
            DataTable dtVessels = new DataTable();
            string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel WHERE ISNULL(IsExported,0) = 1 AND VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleetUPJ.SelectedValue + ") ORDER BY VesselName";
            dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
            ddlVessels.Items.Clear();
            if (dtVessels.Rows.Count > 0)
            {
                ddlVesselUPJ.DataSource = dtVessels;
                ddlVesselUPJ.DataTextField = "VesselName";
                ddlVesselUPJ.DataValueField = "VesselCode";
                ddlVesselUPJ.DataBind();
            }
            else
            {
                ddlVesselUPJ.DataSource = null;
                ddlVesselUPJ.DataBind();
            }
            ddlVessels.Items.Insert(0, "< All >");
        }
        else
        {
            ddlVessels.Items.Clear();
            BindVessels();
        }
    }
    protected void btnUnPlannedCancel_Click(object sender, EventArgs e)
    {
        ddlFleetUPJ.SelectedIndex = 0;
        ddlVesselUPJ.SelectedIndex = 0;
        ddlJobStatusUPJ.SelectedIndex = 0;
        txtCompCodeUPJ.Text = "";
        txtCompNameUPJ.Text = "";

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        if (Session["SQLDefectReport_Office"] != null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "OpenPrintWindow();", true);
        }

    }


    //  ------ Tabs
    protected void btnBPrint_Click(object sender, EventArgs e)
    {

        if (Session["SQLDefectReport_Office_bd"] != null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "OpenPrintWindow_bd();", true);
        }

    }
}

