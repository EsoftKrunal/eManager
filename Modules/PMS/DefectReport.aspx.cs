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

public partial class DefectReport : System.Web.UI.Page
{
    bool setScroll = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserName=Session["UserName"].ToString();
        btnUnplanned.Visible = (UserName == "MASTER" || UserName == "CE" || UserName == "CO");
        btnBreakDowns.Visible = (UserName == "MASTER" || UserName == "CE" || UserName == "CO");
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 8;
            if (Session["UserType"].ToString() == "S")
            {
                ddlVessels.Items.Insert(0, new ListItem("< SELECT >", "0"));
                ddlVessels.Items.Insert(1, new ListItem(Session["CurrentShip"].ToString(), Session["CurrentShip"].ToString()));

                ddlVessels.SelectedIndex = 1;
                ddlVessels_SelectedIndexChanged(sender, e);
                ddlVessels.Visible = false;
                btnAdd.Visible = false;
                btnAddUps.Visible = false; 
                btnDefects_Click(sender, e);
            }
            else
            {
                BindVessels();
                btnAdd.Visible = false;
            }
        }

    }
    #region ---------------- USER DEFINED FUNCTIONS ---------------------------

    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
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
        ddlVessels.Items.Insert(0, "< SELECT VESSEL >");
    }
    private void BindGroupsAndSystems()
    {
        DataTable dtGroups = new DataTable();
        DataTable dtSystem;
        string strSQL = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))=3 AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' AND CMV.Status = 'A' Order By ComponentCode";
        dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        tvComponents.Nodes.Clear();
        if (dtGroups.Rows.Count > 0)
        {
            for (int i = 0; i < dtGroups.Rows.Count; i++)
            {
                TreeNode gn = new TreeNode();
                gn.Text = dtGroups.Rows[i]["ComponentCode"].ToString() + " : " + dtGroups.Rows[i]["ComponentName"].ToString();
                gn.Value = dtGroups.Rows[i]["ComponentCode"].ToString();
                gn.ToolTip = dtGroups.Rows[i]["ComponentName"].ToString();
                gn.Expanded = false;
                DataTable dtSystems;
                String strQuery = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))=6 AND LEFT(CM.ComponentCode,3)='" + gn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' AND CMV.Status = 'A' Order By ComponentCode";
                dtSystems = Common.Execute_Procedures_Select_ByQuery(strQuery);
                if (dtSystems != null)
                {
                    for (int j = 0; j < dtSystems.Rows.Count; j++)
                    {
                        TreeNode sn = new TreeNode();
                        sn.Text = dtSystems.Rows[j]["ComponentCode"].ToString() + " : " + dtSystems.Rows[j]["ComponentName"].ToString();
                        sn.Value = dtSystems.Rows[j]["ComponentCode"].ToString();
                        sn.ToolTip = dtSystems.Rows[j]["ComponentName"].ToString();
                        sn.Expanded = false;
                        string SQL = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))= " + (sn.Value.Trim().Length + 3) + " AND LEFT(CM.ComponentCode," + sn.Value.Trim().Length + ")='" + sn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' AND CMV.Status = 'A' Order By ComponentCode";
                        dtSystem = Common.Execute_Procedures_Select_ByQuery(SQL);
                        if (dtSystem.Rows.Count > 0)
                        {
                            sn.PopulateOnDemand = true;
                        }
                        gn.ChildNodes.Add(sn);
                    }
                }
                gn.Expanded = false;
                tvComponents.Nodes.Add(gn);
            }
        }
    }
    public void BindSpecification()
    {
        string Code = tvComponents.SelectedNode.Value.Trim();
        DataTable dtSpec;
        string strSpecSQL = "SELECT CM.ComponentId,CM.ComponentCode,(Select LTRIM(RTRIM(CM1.ComponentCode)) + ' - ' + CM1.ComponentName from ComponentMaster CM1 WHERE LEN(CM1.ComponentCode)= (LEN('" + Code + "')-3) AND  LEFT(CM1.ComponentCode,(LEN('" + Code + "')-3)) = LEFT('" + Code + "',(LEN('" + Code + "')-3)))As LinkedTo,CM.ComponentName,CM.Descr,CM.ClassEquip,CM.CriticalEquip,CM.Inactive,CMV.Maker,CMV.MakerType FROM ComponentMaster CM " +
                            "INNER JOIN VSL_ComponentMasterForVessel CMV ON CM.ComponentId = CMV.ComponentId AND CMV.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
                            "WHERE CM.ComponentCode ='" + Code + "'";
        dtSpec = Common.Execute_Procedures_Select_ByQuery(strSpecSQL);
        if (dtSpec.Rows.Count > 0)
        {
            //hfCompId.Value = dtSpec.Rows[0]["ComponentId"].ToString().Trim();
            //lblComponentCode.Text = dtSpec.Rows[0]["ComponentCode"].ToString().Trim();
            //lblLinkedto.Text = dtSpec.Rows[0]["LinkedTo"].ToString().Trim();
            //lblComponentName.Text = dtSpec.Rows[0]["ComponentName"].ToString();
            //lblComponentDesc.Text = dtSpec.Rows[0]["Descr"].ToString();
            //lblMaker.Text = dtSpec.Rows[0]["Maker"].ToString();
            //lblMakerType.Text = dtSpec.Rows[0]["MakerType"].ToString();
            //chkClass.Checked = Convert.ToBoolean(dtSpec.Rows[0]["ClassEquip"].ToString());
            ////txtClassCode.Text = dtSpec.Rows[0]["ClassEquipCode"].ToString();
            //chkCritical.Checked = Convert.ToBoolean(dtSpec.Rows[0]["CriticalEquip"].ToString());
            
            //chkInactive.Checked = Convert.ToBoolean(dtSpec.Rows[0]["Inactive"].ToString());
        }
        else
        {
        }
    }
    public void ShowAllDefects()
    {
        string WhereCondition = "";
        
        if (ddlDefectStatus.SelectedIndex != 0)
        {
            if (ddlDefectStatus.SelectedValue == "1")
                WhereCondition = WhereCondition + " AND DD.CompletionDt is null ";
            else
                WhereCondition = WhereCondition + " AND DD.CompletionDt is not null";
        }

        string strAllDefects = "SELECT  RTRIM(CM.ComponentCode) AS ComponentCode,CM.ComponentName,DD.DefectNo,CASE REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') END AS ReportDt,CASE REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  END AS TargetDt  ,DD.RqnNo,CASE REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-') END AS RqnDate, CASE DD.CompStatus WHEN 'W' THEN 'Working' WHEN 'N' THEN 'Not Working' ELSE ' ' END AS CompStatus,CASE REPLACE(CONVERT(varchar, DD.CompletionDt,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.CompletionDt,106),' ','-') END AS CompletionDt, CM.CriticalType, CASE WHEN (DD.TargetDt < getdate() AND CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [Status]  FROM Vsl_DefectDetailsMaster DD " +
                               "INNER JOIN VSL_ComponentMasterForVessel CMV ON CMV.ComponentId = DD.ComponentId AND CMV.Status = 'A' AND CMV.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
                               "INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId " +
                               "WHERE DD.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND DD.Status = 'A' ";
        DataTable dtAlldefects = Common.Execute_Procedures_Select_ByQuery(strAllDefects + WhereCondition + " ORDER BY right(left(DD.DefectNo,6),2) desc,substring(DD.DefectNo,8,len(DD.DefectNo)-7) desc ");

        if (dtAlldefects.Rows.Count > 0)
        {
            rptDefects.DataSource = dtAlldefects;
            rptDefects.DataBind();
            btnPrintDefectReport.Visible = true;
        }
        else
        {
            rptDefects.DataSource = null;
            rptDefects.DataBind();
            btnPrintDefectReport.Visible = false;
        }
    }
    public void ShowDefectReport()
    {
        string WhereCondition = "";

        if (ddlDefectStatus.SelectedIndex != 0)
        {
            if (ddlDefectStatus.SelectedValue == "1")
                WhereCondition = WhereCondition + " AND DD.CompletionDt is null ";
            else
                WhereCondition = WhereCondition + " AND DD.CompletionDt is not null";
        }

        string strDefects = "SELECT  RTRIM(CM.ComponentCode) AS ComponentCode,CM.ComponentName,DD.DefectNo,CASE REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') END AS ReportDt,CASE REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  END AS TargetDt  ,DD.RqnNo,CASE REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-') END AS RqnDate,CASE DD.CompStatus WHEN 'W' THEN 'Working' WHEN 'N' THEN 'Not Working' ELSE ' ' END AS CompStatus,CASE REPLACE(CONVERT(varchar, DD.CompletionDt,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.CompletionDt,106),' ','-') END AS CompletionDt, CM.CriticalType, CASE WHEN (DD.TargetDt < getdate() AND CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [Status] FROM Vsl_DefectDetailsMaster DD " +
                            "INNER JOIN VSL_ComponentMasterForVessel CMV ON CMV.ComponentId = DD.ComponentId AND CMV.Status = 'A' AND CMV.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId " +
                            "WHERE DD.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND DD.Status = 'A' AND LEFT(CM.ComponentCode,LEN(RTRIM('" + tvComponents.SelectedValue.Trim() + "'))) = '" + tvComponents.SelectedValue.Trim() + "' ";
        DataTable dtdefects = Common.Execute_Procedures_Select_ByQuery(strDefects + WhereCondition + " ORDER BY right(left(DD.DefectNo,6),2) desc,substring(DD.DefectNo,8,len(DD.DefectNo)-7) desc ");
        if (dtdefects.Rows.Count > 0)
        {
            rptDefects.DataSource = dtdefects;
            rptDefects.DataBind();
            btnPrintDefectReport.Visible = true;
        }
        else
        {
            rptDefects.DataSource = null;
            rptDefects.DataBind();
            btnPrintDefectReport.Visible = false;
        }
        if (Session["UserType"].ToString() == "S")
        {
            if (tvComponents.SelectedValue.Trim().Length == 3)
            {
                btnAdd.Visible = false;
            }
            else
            {
                btnAdd.Visible = true;
            }
        }
    }
    public void ShowAllUnplannedJobs()
    {
        string WhereCondition = "";

        if (ddlDefectStatus.SelectedIndex != 0)
        {
            if (ddlDefectStatus.SelectedValue == "1")
                WhereCondition = WhereCondition + " AND DONEDATE is null ";
            else
                WhereCondition = WhereCondition + " AND DONEDATE is not null";
        }

        string SQL = "SELECT UP.VESSELCODE,CM.COMPONENTCODE,CM.COMPONENTNAME,UPId, UP.SHORTDESCR,UP.LONGDESCR,DEPTNAME,RANKCODE,DUEDATE,DoneDate,(CASE WHEN DONEDATE IS NULL THEN 'OPEN' ELSE 'CLOSED' END ) STATUS,CM.CriticalType, CASE WHEN (DueDate < getdate() AND DONEDATE IS NULL) THEN 'OD' ELSE '' END AS [ODStatus] " +
                 "FROM  " +
                 "dbo.VSL_UnPlannedJobs UP inner join componentmaster cm on CM.COMPONENTID=UP.COMPONENTID AND STATUS='A' " +
                 "left join Rank RM ON RM.RANKID=UP.ASSIGNEDTO " +
                 "LEFT JOIN DEPTMASTER DM ON DM.DEPTID=UP.DEPTID WHERE VesselCode = '" + ddlVessels.SelectedValue.Trim() + "'";
        DataTable dtAllUps = Common.Execute_Procedures_Select_ByQuery(SQL + WhereCondition);
        if (dtAllUps.Rows.Count > 0)
        {
            rptUpJobs.DataSource = dtAllUps;
            rptUpJobs.DataBind();
            btnPrintUps.Visible = true;
        }
        else
        {
            rptUpJobs.DataSource = null;
            rptUpJobs.DataBind();
            btnPrintUps.Visible = false;
        }
    }
    public void ShowUnplannedJobs()
    {
        string WhereCondition = "";

        if (ddlDefectStatus.SelectedIndex != 0)
        {
            if (ddlDefectStatus.SelectedValue == "1")
                WhereCondition = WhereCondition + " AND DONEDATE is null ";
            else
                WhereCondition = WhereCondition + " AND DONEDATE is not null";
        }

        string SQL = "SELECT UP.VESSELCODE,CM.COMPONENTCODE,CM.COMPONENTNAME,UPId, UP.SHORTDESCR,UP.LONGDESCR,DEPTNAME,RANKCODE,DUEDATE,DoneDate,(CASE WHEN DONEDATE IS NULL THEN 'OPEN' ELSE 'CLOSED' END ) STATUS,CM.CriticalType, CASE WHEN (DueDate < getdate() AND DONEDATE IS NULL) THEN 'OD' ELSE '' END AS [ODStatus] " +
                   "FROM  " +
                   "dbo.VSL_UnPlannedJobs UP inner join componentmaster cm on CM.COMPONENTID=UP.COMPONENTID AND STATUS='A' " +
                   "left join Rank RM ON RM.RANKID=UP.ASSIGNEDTO " +
                   "LEFT JOIN DEPTMASTER DM ON DM.DEPTID=UP.DEPTID WHERE VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND LEFT(CM.ComponentCode,LEN(RTRIM('" + tvComponents.SelectedValue.Trim() + "'))) = '" + tvComponents.SelectedValue.Trim() + "' ";
        DataTable dtUps = Common.Execute_Procedures_Select_ByQuery(SQL + WhereCondition + " ORDER BY ComponentCode ");
        if (dtUps.Rows.Count > 0)
        {
            rptUpJobs.DataSource = dtUps;
            rptUpJobs.DataBind();
            btnPrintUps.Visible = true;
        }
        else
        {
            rptUpJobs.DataSource = null;
            rptUpJobs.DataBind();
            btnPrintUps.Visible = false;
        }
        if (Session["UserType"].ToString() == "S")
        {
            if (tvComponents.SelectedValue.Trim().Length == 3)
            {
                btnAddUps.Visible = false;
            }
            else
            {
                btnAddUps.Visible = true;
            }
        }
    }
    public void ShowComponentSpare()
    {
        //string Code = tvComponents.SelectedNode.Value.ToString().Trim();
        //DataTable dtSpareDetails;
        //string strSQL = "SELECT VSM.*,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster WHERE LEN(ComponentCode)= (LEN('" + Code + "')-3) AND  LEFT(ComponentCode,(LEN('" + Code + "')-3)) = LEFT('" + Code + "',(LEN('" + Code + "')-3)))As LinkedTo FROM VSL_VesselSpareMaster VSM " +
        //                "INNER JOIN ComponentMaster CM ON VSM.ComponentId = CM.ComponentId " +
        //                "WHERE CM.ComponentCode = '" + Code + "' AND VSM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "'";
        //dtSpareDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        //if (dtSpareDetails.Rows.Count > 0)
        //{
        //    rptComponentSpares.DataSource = dtSpareDetails;
        //    rptComponentSpares.DataBind();
        //}
        //else
        //{
        //    rptComponentSpares.DataSource = null;
        //    rptComponentSpares.DataBind();
        //}
    }

    public void ShowBreakDownJobs()
    {
        string WhereCondition = "";

        if (ddlDefectStatus.SelectedIndex != 0)
        {
            if (ddlDefectStatus.SelectedValue == "1")
                WhereCondition = WhereCondition + " AND DD.CompletionDt is null ";
            else
                WhereCondition = WhereCondition + " AND DD.CompletionDt is not null";
        }

        string strDefects = "SELECT  DD.VesselCode,RTRIM(CM.ComponentCode) AS ComponentCode,CM.ComponentName,DD.BreakDownNo,CASE REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') END AS ReportDt,CASE REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  END AS TargetDt  ,DD.RqnNo,CASE REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-') END AS RqnDate,CASE DD.CompStatus WHEN 'W' THEN 'Working' WHEN 'N' THEN 'Not Working' ELSE ' ' END AS CompStatus,CASE REPLACE(CONVERT(varchar, DD.CompletionDt,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(varchar, DD.CompletionDt,106),' ','-') END AS CompletionDt, CM.CriticalType, CASE WHEN (DD.TargetDt < getdate() AND CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [Status] FROM VSL_BreakDownMaster DD " +
                            "INNER JOIN VSL_ComponentMasterForVessel CMV ON CMV.ComponentId = DD.ComponentId AND CMV.Status = 'A' AND CMV.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId " +
                            "WHERE DD.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND DD.Status = 'A' AND LEFT(CM.ComponentCode,LEN(RTRIM('" + tvComponents.SelectedValue.Trim() + "'))) = '" + tvComponents.SelectedValue.Trim() + "' ";
        DataTable dtdefects = Common.Execute_Procedures_Select_ByQuery(strDefects + WhereCondition + " ORDER BY right(left(DD.BreakDownNo,6),2) desc,substring(DD.BreakDownNo,8,len(DD.BreakDownNo)-7) desc ");
        if (dtdefects.Rows.Count > 0)
        {
            rptBreakDownJobs.DataSource = dtdefects;
            rptBreakDownJobs.DataBind();
            //btnPrintBDReport.Visible = true;
        }
        else
        {
            rptBreakDownJobs.DataSource = null;
            rptBreakDownJobs.DataBind();
            //btnPrintBDReport.Visible = false;
        }
        if (Session["UserType"].ToString() == "S")
        {
            if (tvComponents.SelectedValue.Trim().Length == 3)
            {
                btnAddBD.Visible = false;
            }
            else
            {
                btnAddBD.Visible = true;
                //btnAddBD.Visible = false ;
            }
        }   
    }
    protected void btnAddBD_Click(object sender, EventArgs e)
    {
        if (tvComponents.SelectedValue != null && tvComponents.SelectedValue != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openaddbd('" + tvComponents.SelectedValue.Trim() + "');", true);
        }
        else
        {
            MessageBox1.ShowMessage("Please select a component.", true);
        }
    }
    protected void btnPrintBDReport_Click(object sender, EventArgs e)
    {
        string strSearch = "SELECT ROW_NUMBER() OVER( ORDER BY DD.VesselCode,CM.ComponentCode) AS SrNo,DD.VesselCode,CM.ComponentId,CM.ComponentCode,CM.ComponentName,DD.BreakDownNo,REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') AS ReportDt,REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  AS TargetDt,  CASE DD.CompStatus WHEN 'W' THEN 'Working' WHEN 'N' THEN 'Not Working' ELSE ' ' END AS CompStatus ,DD.RqnNo,REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-') AS RqnDate,CM.CriticalType,(Case when CompletionDt is null then 'Open' else 'Closed' end )DefectStatus ,(replace(convert(varchar,CompletionDt,106),' ','-'))CompletionDt, CASE WHEN (DD.TargetDt < getdate() AND CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [Status],( SELECT TOP 1 Remarks FROM VSL_DefectRemarks DR WHERE DR.VesselCode = DD.vesselcode AND DR.BreakDownNo = DD.BreakDownNo ORDER BY EnteredOn DESC ) AS Remarks, " +
                           "DefectDetails, " +
                           "( " +
                              "(CASE WHEN Vessel = 'Y' THEN 'Vessel,' ELSE '' END) +" +
                              "(CASE WHEN Spares = 'Y' THEN 'Spares,' ELSE '' END) +" +
                              "(CASE WHEN ShoreAssist = 'Y' THEN 'ShoreAssist,' ELSE '' END) + " +
                              "(CASE WHEN Drydock = 'Y' THEN 'Drydock,' ELSE '' END) + " +
                              "(CASE WHEN Guarentee = 'Y' THEN 'Guarentee' ELSE '' END)" +
                            ") as Responsibility" +
                            ",(RTRIM(CM.ComponentCode)+ ' : ' + CM.ComponentName) AS Component, '' AS FleetName " +
                           "FROM VSL_BreakDownMaster DD INNER JOIN VSL_ComponentMasterForVessel CMV ON CMV.VesselCode = DD.VesselCode AND CMV.ComponentId = DD.ComponentId AND CMV.Status = 'A' AND DD.Status = 'A' INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId ";
        
        string where = "WHERE DD.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ";

        if (tvComponents.SelectedNode != null)
        {
            where = where + "AND LEFT(CM.ComponentCode,LEN(RTRIM('" + tvComponents.SelectedValue.Trim() + "'))) = '" + tvComponents.SelectedValue.Trim() + "' ";
        }

        if (ddlDefectStatus.SelectedIndex != 0)
        {
            if (ddlDefectStatus.SelectedValue == "1")
                where = where + " AND DD.CompletionDt is null ";
            else
                where = where + " AND DD.CompletionDt is not null";
        }

        Session["SQLDefectReport_Ship"] = strSearch + where;

        if (Session["SQLDefectReport_Ship"] != null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "OpenPrintWindowBD();", true);
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openprint('" + tvComponents.SelectedValue.Trim() + "');", true);
    }
    #endregion

    #region ---------------- Events -------------------------------------------

    #region ---------------- Common ---------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Componenttree');", true);
    }
    protected void ddlVessels_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex != 0)
        {
            BindGroupsAndSystems();
            ShowAllDefects();
            ShowAllUnplannedJobs();
            btnSearchComponents.Visible = true;
        }
        else
        {
            tvComponents.Nodes.Clear();
            btnSearchComponents.Visible = false;
        }
    }
    protected void tvComponents_SelectedNodeChanged(object sender, EventArgs e)
    {
        ShowDefectReport();
        ShowUnplannedJobs();
        ShowBreakDownJobs();
    }
    protected void tvComponents_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        DataTable dtSubSystems;
        DataTable dtUnits;
        string strUnits = "";
        string strSubSystems = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))=" + (e.Node.Value.Trim().Length + 3) + " AND LEFT(CM.ComponentCode," + e.Node.Value.Trim().Length + ")='" + e.Node.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' Order By ComponentCode";
        dtSubSystems = Common.Execute_Procedures_Select_ByQuery(strSubSystems);
        if (dtSubSystems != null)
        {
            for (int k = 0; k < dtSubSystems.Rows.Count; k++)
            {
                TreeNode ssn = new TreeNode();
                ssn.Text = dtSubSystems.Rows[k]["ComponentCode"].ToString() + " : " + dtSubSystems.Rows[k]["ComponentName"].ToString();
                ssn.Value = dtSubSystems.Rows[k]["ComponentCode"].ToString();
                ssn.ToolTip = dtSubSystems.Rows[k]["ComponentName"].ToString();
                ssn.Expanded = false;
                if (e.Node.Value.Trim().Length == 6)
                {
                    strUnits = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))= 12 AND LEFT(CM.ComponentCode, 9 )='" + ssn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' Order By ComponentCode";
                    dtUnits = Common.Execute_Procedures_Select_ByQuery(strUnits);
                    if (dtUnits.Rows.Count > 0)
                    {
                        ssn.PopulateOnDemand = true;
                    }
                }
                e.Node.ChildNodes.Add(ssn);
            }
        }
    }
    protected void btnSearchedCode_Click(object sender, EventArgs e)
    {
        string Group = "";
        string system = "";
        string subsystem = "";
        string valuepath = "";
        if (tvComponents.SelectedNode != null)
        {
            tvComponents.SelectedNode.Selected = false;
        }
        if (hfSearchCode.Value.Trim().Length == 3)
        {
            valuepath = hfSearchCode.Value.ToString();
        }
        if (hfSearchCode.Value.Trim().Length == 6)
        {
            Group = hfSearchCode.Value.Trim().Substring(0, 3);
            valuepath = Group + "         /" + hfSearchCode.Value.ToString();
        }
        if (hfSearchCode.Value.Trim().Length == 9)
        {
            Group = hfSearchCode.Value.Trim().Substring(0, 3);
            system = hfSearchCode.Value.Trim().Substring(0, 6);
            foreach (TreeNode tnGroup in tvComponents.Nodes) // Group 01
            {
                if (tnGroup.Value.Trim() == Group)
                {
                    tnGroup.Expand();
                    if (tnGroup.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode tnSystem in tnGroup.ChildNodes) // System 0101
                        {
                            if (tnSystem.Value.Trim() == system)
                            {
                                tnSystem.Expand();
                                break;
                            }
                        }
                    }
                }
            }
            valuepath = Group + "         /" + system + "      /" + hfSearchCode.Value.ToString();
        }
        if (hfSearchCode.Value.Trim().Length == 12)
        {
            Group = hfSearchCode.Value.Trim().Substring(0, 3);
            system = hfSearchCode.Value.Trim().Substring(0, 6);
            subsystem = hfSearchCode.Value.Trim().Substring(0, 9);
            foreach (TreeNode tnGroup in tvComponents.Nodes) // Group 01
            {
                if (tnGroup.Value.Trim() == Group)
                {
                    tnGroup.Expand();
                    if (tnGroup.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode tnSystem in tnGroup.ChildNodes) // System 0101
                        {
                            if (tnSystem.Value.Trim() == system)
                            {
                                tnSystem.Expand();
                                if (tnSystem.ChildNodes.Count > 0)
                                {
                                    foreach (TreeNode tnSubSystem in tnSystem.ChildNodes) // SubSystem 010101
                                    {
                                        if (tnSubSystem.Value.Trim() == subsystem)
                                        {
                                            tnSubSystem.Expand();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            valuepath = Group + "         /" + system + "      /" + subsystem + "   /" + hfSearchCode.Value.ToString();
        }
        tvComponents.FindNode(valuepath).Select();
        TreeNode tn = tvComponents.SelectedNode;
        if (hfSearchCode.Value.Trim().Length == 6)
        {
            tn.Parent.Expand();
        }
        if (hfSearchCode.Value.Trim().Length == 9)
        {
            tn.Parent.Expand();
            tn.Parent.Parent.Expand();
        }
        if (hfSearchCode.Value.Trim().Length == 12)
        {
            tn.Parent.Expand();
            tn.Parent.Parent.Expand();
            tn.Parent.Parent.Parent.Expand();
        }
        tvComponents_SelectedNodeChanged(sender, e);
        setScroll = false;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "fds", "setFocus('" + tn.ToolTip + "');", true);
    }
    protected void btnSearchComponents_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex != 0)
        {
            Session["VesselCode"] = ddlVessels.SelectedValue.Trim();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opensearchwindow();", true);
        }
        else
        {
            MessageBox1.ShowMessage("Please select Vessel.", true);
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ShowDefectReport();
        ShowUnplannedJobs();
        ShowBreakDownJobs();
    }
    #endregion
    #endregion
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (tvComponents.SelectedValue != null && tvComponents.SelectedValue != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openadddefects('" + tvComponents.SelectedValue.Trim() + "');", true);
        }
        else
        {
            MessageBox1.ShowMessage("Please select a component.", true);
        }
    }
    protected void btnUPJ_Click(object sender, EventArgs e)
    {
        if (tvComponents.SelectedValue != null && tvComponents.SelectedValue != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openaddupj('" + ddlVessels.SelectedValue.Trim() + "','" + tvComponents.SelectedValue.Trim() + "');", true);
        }
        else
        {
            MessageBox1.ShowMessage("Please select a component.", true);
        }
    }
    protected void btnDefects_Click(object sender, EventArgs e)
    {
        plSpecs.Visible = true;
        pnUPJ.Visible = false;
        btnBDJ.Visible = false;
        btnDefects.CssClass = "btnhighlight";
        btnUnplanned.CssClass = "btnorange";
        btnBreakDowns.CssClass = "btnorange";
    }
    protected void btnUnplanned_Click(object sender, EventArgs e)
    {
        plSpecs.Visible = false;
        pnUPJ.Visible = true;
        btnBDJ.Visible = false;
        btnDefects.CssClass = "btnorange";
        btnUnplanned.CssClass = "btnhighlight";
        btnBreakDowns.CssClass = "btnorange";
    }
    protected void btnBreakDowns_Click(object sender, EventArgs e)
    {
        plSpecs.Visible = false;
        pnUPJ.Visible = false;
        btnBDJ.Visible = true;
        btnDefects.CssClass = "btnorange";
        btnUnplanned.CssClass = "btnorange";
        btnBreakDowns.CssClass = "btnhighlight";
    }
    protected void btnPrintUPReport_Click(object sender, EventArgs e)
    { 
    
    }
    protected void btnPrintDefectReport_Click(object sender, EventArgs e)
    {
        string strSearch = "SELECT ROW_NUMBER() OVER( ORDER BY DD.VesselCode,CM.ComponentCode) AS SrNo,DD.VesselCode,CM.ComponentId,CM.ComponentCode,CM.ComponentName,DD.DefectNo,REPLACE(CONVERT(varchar, DD.ReportDt,106),' ','-') AS ReportDt,REPLACE(CONVERT(varchar, DD.TargetDt,106),' ','-')  AS TargetDt,  CASE DD.CompStatus WHEN 'W' THEN 'Working' WHEN 'N' THEN 'Not Working' ELSE ' ' END AS CompStatus ,DD.RqnNo,REPLACE(CONVERT(varchar, DD.RqnDate,106),' ','-') AS RqnDate,CM.CriticalType,(Case when CompletionDt is null then 'Open' else 'Closed' end )DefectStatus ,(replace(convert(varchar,CompletionDt,106),' ','-'))CompletionDt, CASE WHEN (DD.TargetDt < getdate() AND CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [Status],( SELECT TOP 1 Remarks FROM VSL_DefectRemarks DR WHERE DR.VesselCode = DD.vesselcode AND DR.DefectNo = DD.DefectNo ORDER BY EnteredOn DESC ) AS Remarks, " +
                           "DefectDetails, " +
                           "( " +
                              "(CASE WHEN Vessel = 'Y' THEN 'Vessel,' ELSE '' END) +" +
                              "(CASE WHEN Spares = 'Y' THEN 'Spares,' ELSE '' END) +" +
                              "(CASE WHEN ShoreAssist = 'Y' THEN 'ShoreAssist,' ELSE '' END) + " +
                              "(CASE WHEN Drydock = 'Y' THEN 'Drydock,' ELSE '' END) + " +
                              "(CASE WHEN Guarentee = 'Y' THEN 'Guarentee' ELSE '' END)" +
                            ") as Responsibility" +
                            ",(RTRIM(CM.ComponentCode)+ ' : ' + CM.ComponentName) AS Component, '' AS FleetName " +
                           "FROM Vsl_DefectDetailsMaster DD INNER JOIN VSL_ComponentMasterForVessel CMV ON CMV.VesselCode = DD.VesselCode AND CMV.ComponentId = DD.ComponentId AND CMV.Status = 'A' AND DD.Status = 'A' INNER JOIN ComponentMaster CM ON CM.ComponentId = CMV.ComponentId ";
                           //"INNER JOIN dbo.Vessel V ON CMV.VesselCode = V.VesselCode " +
                           //"INNER JOIN dbo.FleetMaster FM ON FM.FleetId = V.FleetId ";

        string where = "WHERE DD.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ";

        if (tvComponents.SelectedNode != null)
        {
            where = where + "AND LEFT(CM.ComponentCode,LEN(RTRIM('" + tvComponents.SelectedValue.Trim() + "'))) = '" + tvComponents.SelectedValue.Trim() + "' ";
        }

        if (ddlDefectStatus.SelectedIndex != 0)
        {
            if (ddlDefectStatus.SelectedValue == "1")
                where = where + " AND DD.CompletionDt is null ";
            else
                where = where + " AND DD.CompletionDt is not null";
        }

        Session["SQLDefectReport_Ship"] = strSearch + where;

        if (Session["SQLDefectReport_Ship"] != null)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "OpenPrintWindow();", true);
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openprint('" + tvComponents.SelectedValue.Trim() + "');", true);
    }
    protected void ddlDefectStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tvComponents.SelectedNode != null)
        {
            ShowDefectReport();
            //ShowUnplannedJobs();
            ShowBreakDownJobs();
        }
        else
        {
            ShowAllDefects();
            ShowAllUnplannedJobs();
            ShowBreakDownJobs();
        }
    }
}
