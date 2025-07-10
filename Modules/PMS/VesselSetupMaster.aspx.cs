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
using System.IO.Compression;
using System.IO;
using Ionic.Zip;  
  
public partial class VesselSetupMaster : System.Web.UI.Page
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
            Auth = new AuthenticationManager(1044, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");

            }
            else
            {
                btnPrintCompList.Visible = Auth.IsPrint;
            }
           // Auth = new AuthenticationManager(277, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!Auth.IsView)
            {
                btnSpecification.Visible = false;
                plSpecs.Visible = false;
            }
            else
            {
                btnSpecification.Visible = true;
            }
           // Auth = new AuthenticationManager(278, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!Auth.IsView)
            {
                btnAssignJobs.Visible = false;
            }
            Auth = new AuthenticationManager(1044, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!Auth.IsView)
            {
                btnAssignSpare.Visible = false;
            }
            Auth = new AuthenticationManager(1044, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!Auth.IsView)
            {
                btnAssignRunningHour.Visible = false;
            }
        }
        //***********
        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 3;
            BindVessels();
            ddlVessels.SelectedValue = Session["VC"].ToString();
            lblVesselName.Text = "[ " + ddlVessels.SelectedValue + " ] " + ddlVessels.SelectedItem.Text;
            ddlVessels_SelectedIndexChanged(sender, e);
            btnSpecification_Click(sender, e);
        }
    }

    #region ---------------- USER DEFINED FUNCTIONS ---------------------------
    #region ---------------- Commom ---------------------------------

    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM dbo.Vessel ORDER BY VesselName";
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
        string strSQL = "SELECT CM.ComponentCode, CM.ComponentName FROM ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))=3 AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' Order By ComponentCode";
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
                String strQuery = "SELECT CM.ComponentCode, CM.ComponentName FROM ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))=6 AND LEFT(CM.ComponentCode,3)='" + gn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' Order By ComponentCode";
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
                        string SQL = "SELECT CM.ComponentCode, CM.ComponentName FROM ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))= " + (sn.Value.Trim().Length + 3) + " AND LEFT(CM.ComponentCode," + sn.Value.Trim().Length + ")='" + sn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' Order By ComponentCode";
                        //string SQL = "SELECT ComponentCode, ComponentName,Descr FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=6 AND LEFT(ComponentCode,4)='" + sn.Value.Trim() + "' Order By ComponentCode";
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
    #endregion

    #region ---------------- Assign Units ---------------------------
    //public void BindComponentUnits()
    //{
    //    if (tvComponents.SelectedNode.Value.ToString().Trim().Length != 2)
    //    {
    //        DataTable dtComponents = new DataTable();
    //        string strSelectComponents = "";
    //        strSelectComponents = "SELECT COM.* FROM ( SELECT ROW_NUMBER() OVER(PARTITION BY CM.ComponentId ORDER BY CM.ComponentId)AS Rank, " +
    //                              "VCM.VesselComponentId,ltrim(rtrim(CM.ComponentCode))+' : '+ CM.ComponentName as ComponentName,COUNT(VCM.ComponentName) OVER(PARTITION BY CM.ComponentCode)AS Units,CM.ComponentId " +
    //                              "FROM VesselComponentMaster VCM INNER JOIN ComponentMaster CM ON CM.ComponentId = VCM.ComponentId " +
    //                              "WHERE VCM.VesselCode ='" + ddlVessels.SelectedValue.ToString() + "' AND LEN(CM.ComponentCode)<> 4  AND LEFT(CM.ComponentCode," + tvComponents.SelectedNode.Value.ToString().Trim().Length + ") = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "' ) AS COM " +
    //                              "WHERE Rank = 1 ";
    //        dtComponents = Common.Execute_Procedures_Select_ByQuery(strSelectComponents);
    //        if (dtComponents.Rows.Count > 0)
    //        {
    //            rptComponentsUnits.DataSource = dtComponents;
    //            rptComponentsUnits.DataBind();
    //        }
    //        else
    //        {
    //            rptComponentsUnits.DataSource = null;
    //            rptComponentsUnits.DataBind();
    //        }
    //    }
    //    else
    //    {
    //        //lblMessage.Text = "Please select a component.";
    //        MessageBox1.ShowMessage("Please select a component.", true);
    //    }
    //    EnableDisablePlUnitButtons();
    //}    
    //private void EnableDisablePlUnitButtons()
    //{
    //    if (tvComponents.SelectedNode.Value.ToString().Trim().Length != 2)
    //    {
    //        if (tvComponents.SelectedNode.Value.ToString().Trim().Length == 6 && rptComponentsUnits.Items.Count > 0)
    //        {
    //            btnAddUnits.Visible = false;
    //            btnEditUnits.Visible = true;
    //        }
    //        else if (tvComponents.SelectedNode.Value.ToString().Trim().Length == 6 && rptComponentsUnits.Items.Count == 0)
    //        {
    //            btnAddUnits.Visible = true;
    //            btnEditUnits.Visible = false;
    //        }
    //        else if (tvComponents.SelectedNode.Value.ToString().Trim().Length == 4 && rptComponentsUnits.Items.Count == 0)
    //        {
    //            btnAddUnits.Visible = false;
    //            btnEditUnits.Visible = false;
    //        }
    //        else if (tvComponents.SelectedNode.Value.ToString().Trim().Length == 4 && rptComponentsUnits.Items.Count > 0)
    //        {
    //            btnAddUnits.Visible = false;
    //            btnEditUnits.Visible = true;
    //        }
    //    }
    //}
    #endregion

    #region ------------------ Assign Specification -----------------

    public void BindSpecification()
    {
        string Code = tvComponents.SelectedNode.Value.Trim();
        DataTable dtSpec;
        //string strSpecSQL = "SELECT ComponentId,ComponentCode,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster WHERE LEN(ComponentCode)= (LEN('" + Code + "')-2) AND  LEFT(ComponentCode,(LEN('" + Code + "')-2)) = LEFT('" + Code + "',(LEN('" + Code + "')-2)))As LinkedTo,ComponentName,Descr,ClassEquip,SmsEquip,ClassEquipCode,SmsCode,CriticalEquip,CSMItem,Inactive,InheritParentJobs FROM ComponentMaster WHERE ComponentCode ='" + Code + "'";
        string strSpecSQL = "SELECT CM.ComponentId,CM.ComponentCode,(Select LTRIM(RTRIM(CM1.ComponentCode)) + ' - ' + CM1.ComponentName from ComponentMaster CM1 WHERE LEN(CM1.ComponentCode)= (LEN('" + Code + "')-3) AND  LEFT(CM1.ComponentCode,(LEN('" + Code + "')-3)) = LEFT('" + Code + "',(LEN('" + Code + "')-3)))As LinkedTo,CM.ComponentName,CMV.Descr,CAST( ISNULL(CMV.ClassEquip,0) AS BIT) AS ClassEquip,CMV.ACCOUNTCODES,CM.CriticalEquip,CM.Inactive,CMV.Maker,CMV.MakerType,CMV.ClassEquipCode,CM.CriticalType FROM ComponentMaster CM " +
                            "INNER JOIN ComponentMasterForVessel CMV ON CM.ComponentId = CMV.ComponentId AND CMV.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
                            "WHERE CM.ComponentCode ='" + Code + "'";
        dtSpec = Common.Execute_Procedures_Select_ByQuery(strSpecSQL);
        if (dtSpec.Rows.Count > 0)
        {
            hfCompId.Value = dtSpec.Rows[0]["ComponentId"].ToString().Trim();
            txtComponentCode.Text = dtSpec.Rows[0]["ComponentCode"].ToString().Trim();
            lblLinkedto.Text = dtSpec.Rows[0]["LinkedTo"].ToString().Trim();
            txtComponentName.Text = dtSpec.Rows[0]["ComponentName"].ToString();
            txtComponentDesc.Text = dtSpec.Rows[0]["Descr"].ToString();
            txtMaker.Text = dtSpec.Rows[0]["Maker"].ToString();
            txtMakerType.Text = dtSpec.Rows[0]["MakerType"].ToString();
            chkClass.Checked = Convert.ToBoolean(dtSpec.Rows[0]["ClassEquip"].ToString());
            txtAccountCodes.Text = dtSpec.Rows[0]["ACCOUNTCODES"].ToString();
            txtClassCode.Text = dtSpec.Rows[0]["ClassEquipCode"].ToString();
            chkCritical.Checked = Convert.ToBoolean(dtSpec.Rows[0]["CriticalEquip"].ToString());
            chkCE.Visible = chkCritical.Checked;
            chkCE.Checked = dtSpec.Rows[0]["CriticalType"].ToString() == "E";
            chkInactive.Checked = Convert.ToBoolean(dtSpec.Rows[0]["Inactive"].ToString());
            
            if (chkClass.Checked)
            {
                txtClassCode.ReadOnly = false;
            }
            else
            {
                txtClassCode.ReadOnly = true;
            }

        }
        else
        {
        }
    }
     

    #endregion

    #region ---------------- Assign Jobs ---------------------------

    private void BindVesselComponents()
    {
        if (tvComponents.SelectedNode.Value.ToString().Trim().Length != 3)
        {
            DataTable dtJobDetails;
            string strSQL;
            strSQL = "SELECT  VCJM.VesselCode,LTRIM(RTRIM(CM.ComponentCode)) As CompCode,CM.CriticalEquip AS IsCritical,CM.CriticalType,DM.DeptName,RM.RankCode,JM.JobCode AS JobCode,CJM.DescrSh AS JobName,JIM.IntervalName,VCJM.Interval,CJM.CompJobId ,VCJM.ComponentId,cjm.AttachmentForm,cjm.RiskAssessment,VCJM.Guidelines "+
                    " ,dbo.GetJobAttachmentCount(cjm.CompJobId,VCJM.VesselCode,'V') as AttachmentCount " +                                    
            " from VesselComponentJobMaster VCJM " +
                     "INNER JOIN ComponentsJobMapping CJM ON CJM.CompJobId = VCJM.CompJobId " +
                     "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId " +                      
                     "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                     "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
                     "INNER JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId " +
                     "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                     "WHERE VCJM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND CM.ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "' ORDER BY JobCode,CompCode";
            dtJobDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);            
            rptVesselComponents.DataSource = dtJobDetails;
            rptVesselComponents.DataBind();
            rptVesselComponents.Visible = true;
        }
        else
        {
            rptVesselComponents.DataSource = null;
            rptVesselComponents.DataBind();
            rptVesselComponents.Visible = false;
        }
    }
    public DataTable BindInterval()
    {
        DataTable dtInterval = new DataTable();
        string strSQL = "SELECT IntervalId,IntervalName FROM JobIntervalMaster";
        dtInterval = Common.Execute_Procedures_Select_ByQuery(strSQL);
        return dtInterval;
    }
    public DataTable BindRanks()
    {
        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankCode";
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        return dtRanks;
    }    

    #endregion

    #region ---------------- Assign Spares ---------------------------

    private void ShowParent()
    {
        string Code = tvComponents.SelectedNode.Value.ToString().Trim();
        DataTable dtSpareDetails;
        string strSQL = "Select ComponentCode, ComponentName, (Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName  from ComponentMaster WHERE LEN(ComponentCode)= (LEN('" + Code + "')-3) AND  LEFT(ComponentCode,(LEN('" + Code + "')-3)) = LEFT('" + Code + "',(LEN('" + Code + "')-3)))As LinkedTo FROM ComponentMaster WHERE ComponentCode = '" + Code + "'";
        dtSpareDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        //if (dtSpareDetails.Rows.Count > 0)
        //{
        //lblLinkedTo.Text = dtSpareDetails.Rows[0]["LinkedTo"].ToString();
        //lblComponent.Text = dtSpareDetails.Rows[0]["ComponentCode"].ToString() + " - " + dtSpareDetails.Rows[0]["ComponentName"].ToString();     
        //}
    }
    public void ShowComponentSpare()
    {
        string Code = tvComponents.SelectedNode.Value.ToString().Trim();
        DataTable dtSpareDetails;
        string strSQL = "SELECT VSM.*,CASE VSM.Maker WHEN '' THEN CMV.Maker ELSE VSM.Maker END AS Maker1,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster WHERE LEN(ComponentCode)= (LEN('" + Code + "')-3) AND  LEFT(ComponentCode,(LEN('" + Code + "')-3)) = LEFT('" + Code + "',(LEN('" + Code + "')-3)))As LinkedTo FROM VesselSpareMaster VSM " +
                        "INNER JOIN ComponentMaster CM ON VSM.ComponentId = CM.ComponentId " +
                        "INNER JOIN ComponentMasterForVessel CMV ON CMV.VesselCode = VSM.VesselCode AND CMV.ComponentId = VSM.ComponentId " +
                        "WHERE CM.ComponentCode = '" + Code + "' AND VSM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "'";
        dtSpareDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtSpareDetails.Rows.Count > 0)
        {
            rptComponentSpares.DataSource = dtSpareDetails;
            rptComponentSpares.DataBind();
        }
        else
        {
            rptComponentSpares.DataSource = null;
            rptComponentSpares.DataBind();
            //ClearFields();
        }
    }
    #endregion
    #region ---------------- Assign Running Hour ---------------------
    public void ShowRunningHourDetails()
    {
        Auth = new AuthenticationManager(280, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        string strRunningHourSQL = "SELECT ROW_NUMBER() OVER(ORDER BY CM.ComponentCode) AS SrNo,CM.ComponentId ,CM.ComponentCode,CM.ComponentName,ISNULL(VRM.StartupHour,'') AS StartupHour ,ISNULL(VRM.AvgRunningHrPerDay,'') AS AvgRunningHrPerDay,REPLACE( CASE CONVERT(varchar(15),ISNULL(VRM.StartDate,''),106) WHEN '01 Jan 1900' THEN '' ELSE CONVERT(varchar(15),ISNULL(VRM.StartDate,''),106) END ,' ','-')  AS StartDate " +
                                   "FROM ComponentMaster CM " +
                                   "LEFT JOIN  VesselRunningHourMaster VRM ON VRM.ComponentId = CM.ComponentId  AND VRM.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
                                   "WHERE CM.ComponentId IN (SELECT DISTINCT ComponentId FROM VesselComponentJobMaster WHERE IntervalId = 1 AND VesselCode = '" + ddlVessels.SelectedValue.Trim() + "')";
        DataTable dtRunningHour = Common.Execute_Procedures_Select_ByQuery(strRunningHourSQL);
        if (dtRunningHour.Rows.Count > 0)
        {
            rptRunningHour.DataSource = dtRunningHour;
            rptRunningHour.DataBind();
            btnRunHSave.Visible = Auth.IsAdd;
        }
        else
        {
            rptRunningHour.DataSource = null;
            rptRunningHour.DataBind();
            btnRunHSave.Visible = false;
            msgRunHour.ShowMessage("No hour based components exists for selected vessel.", true);
        }
    }
    #endregion
    #region --------------- Finalise Setup ---------------------------
    //private void ShowExportDetails()
    //{
    //    lblExportBy.Text = Session["FullName"].ToString();
    //    lblExportDate.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
    //}

    #endregion
    #endregion
    
    #region ---------------- Events -------------------------------------------

    #region ---------------- Commom ---------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Componenttree');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvJobs');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvSpares');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvRH');", true);
    }
    protected void ddlVessels_SelectedIndexChanged(object sender, EventArgs e)
    {
        Auth = new AuthenticationManager(1044, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        Session["VC"] = ddlVessels.SelectedValue.ToString();
        if (ddlVessels.SelectedIndex != 0)
        {
            BindGroupsAndSystems();
            if (tvComponents.Nodes.Count > 0)
            {
                btnAddStructure.ImageUrl = "~/Modules/PMS/Images/edit.png";
                btnAddStructure.ToolTip = "Edit Components";  
                btnSearchComponents.Visible = true;
                btnAddStructure.Visible = Auth.IsUpdate;
            }
            else
            {
                btnAddStructure.ImageUrl = "~/Modules/PMS/Images/add.png";
                btnAddStructure.ToolTip = "Add Components";
                btnSearchComponents.Visible = false;
                btnAddStructure.Visible = Auth.IsAdd;
            }
            
        }
        else
        {
            tvComponents.Nodes.Clear();
            btnAddStructure.Visible = false;
            btnSearchComponents.Visible = false;
        }
    }
    protected void tvComponents_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (plSpecs.Visible)
        {
            lblSpecCompCode.Text = tvComponents.SelectedNode.Text;
            BindSpecification();
        }
        if (plJobs.Visible)
        {
            lblCompCodeJobs.Text = tvComponents.SelectedNode.Text;
            BindVesselComponents();
        }
        if(plSpare.Visible)
        {
            lblCompCodeSpare.Text = tvComponents.SelectedNode.Text;
            ShowComponentSpare();
        }
    }
    protected void tvComponents_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        DataTable dtSubSystems;
        DataTable dtUnits;
        string strUnits = "";
        //string sncode = e.Node.Text.ToString().Split(':').GetValue(0).ToString().Trim();
        string strSubSystems = "SELECT CM.ComponentCode, CM.ComponentName FROM ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))=" + (e.Node.Value.Trim().Length + 3) + " AND LEFT(CM.ComponentCode," + e.Node.Value.Trim().Length + ")='" + e.Node.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' Order By ComponentCode";
        //string strSubSystems = "SELECT ComponentCode, ComponentId, ComponentName FROM ComponentMaster where LEN(LTRIM(RTRIM(ComponentCode)))=" + (sncode.Trim().Length + 2) + " AND LEFT(ComponentCode," + sncode.Trim().Length + ")='" + sncode + "' Order By ComponentCode";        
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
                    strUnits = "SELECT CM.ComponentCode, CM.ComponentName FROM ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))= 12 AND LEFT(CM.ComponentCode, 9 )='" + ssn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' Order By ComponentCode";
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
            Session["VC"] = ddlVessels.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opensearchwindow();", true);
        }
        else
        {
            MessageBox1.ShowMessage( "Please select Vessel.",true);
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (plSpecs.Visible)
        {
            lblSpecCompCode.Text = tvComponents.SelectedNode.Text;
            BindSpecification();
        }
        if (plJobs.Visible)
        {
            lblCompCodeJobs.Text = tvComponents.SelectedNode.Text;
            BindVesselComponents();
        }
        if (plSpare.Visible)
        {
            lblCompCodeSpare.Text = tvComponents.SelectedNode.Text;
            ShowComponentSpare();
        }
        if (PlRunningHour.Visible)
        {
            ShowRunningHourDetails();
        }
        //if (plFinaliseSetup.Visible)
        //{
        //    ShowExportDetails();
        //}
    }
    protected void btnSpecification_Click(object sender, EventArgs e)
    {
        Auth = new AuthenticationManager(1044, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        plSpecs.Visible = true;
        plJobs.Visible = false;
        plSpare.Visible = false;
        PlRunningHour.Visible = false;
        //plFinaliseSetup.Visible = false;
        tdComponentTree.Visible = true;
        btnSpecification.CssClass = "selbtn";
        btnAssignJobs.CssClass = "btn1";
        btnAssignSpare.CssClass = "btn1";
        btnAssignRunningHour.CssClass = "btn1";
        //btnFinaliseSetup.CssClass = "btnorange";
        imgbtneditSpec.Visible = Auth.IsUpdate;
        if (tvComponents.SelectedNode != null)
        {
            btnRefresh_Click(sender, e);
        }

    }
    protected void btnAssignJobs_Click(object sender, EventArgs e)
    {
        Auth = new AuthenticationManager(1044, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        plSpecs.Visible = false;
        plJobs.Visible = true;
        plSpare.Visible = false;
        PlRunningHour.Visible = false;
        //plFinaliseSetup.Visible = false;
        tdComponentTree.Visible = true;
        btnSpecification.CssClass = "btn1";
        btnAssignJobs.CssClass = "selbtn";
        btnAssignSpare.CssClass = "btn1";
        btnAssignRunningHour.CssClass = "btn1";
        //btnFinaliseSetup.CssClass = "btn1";
        btnAddJobs.Visible = Auth.IsAdd;
        imgbtnEditJobs.Visible = Auth.IsUpdate;
        ImgbtnDeleteJobs.Visible = Auth.IsDelete;
        if (tvComponents.SelectedNode != null)
        {
            btnRefresh_Click(sender, e);
        }
    }
    protected void btnAssignSpare_Click(object sender, EventArgs e)
    {
        Auth = new AuthenticationManager(1044, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        plSpecs.Visible = false;
        plJobs.Visible = false;
        plSpare.Visible = true;
        PlRunningHour.Visible = false;
        //plFinaliseSetup.Visible = false;
        tdComponentTree.Visible = true;
        btnSpecification.CssClass = "btn1";
        btnAssignSpare.CssClass = "selbtn";
        btnAssignJobs.CssClass = "btn1";
        btnAssignRunningHour.CssClass = "btn1";
        //btnFinaliseSetup.CssClass = "btn1";
        btnAddSpares.Visible = Auth.IsAdd;
       
        Session.Add("VSPEDIT", Auth.IsUpdate);
        Session.Add("VSPDELETE", Auth.IsDelete);
        
        if (tvComponents.SelectedNode != null)
        {
            btnRefresh_Click(sender, e);
        }
    }
    protected void btnAssignRunningHour_Click(object sender, EventArgs e)
    {
        //Auth = new AuthenticationManager(280, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        plSpecs.Visible = false;
        plJobs.Visible = false;
        plSpare.Visible = false;
        PlRunningHour.Visible = true;
        //plFinaliseSetup.Visible = false;
        tdComponentTree.Visible = false;
        btnSpecification.CssClass = "btn1";
        btnAssignSpare.CssClass = "btn1";
        btnAssignJobs.CssClass = "btn1";
        btnAssignRunningHour.CssClass = "selbtn";
        //btnFinaliseSetup.CssClass = "btn1";
        //btnRunHSave.Visible = Auth.IsAdd;
        if (ddlVessels.SelectedIndex != 0)
        {
            btnRefresh_Click(sender, e);
        }
        else
        {
            btnAssignJobs_Click(sender, e);
            MessageBox1.ShowMessage("Please select a vessel first.", true);
        }
    }
    protected void btnFinaliseSetup_Click(object sender, EventArgs e)
    {
        plSpecs.Visible = false;
        plJobs.Visible = false;
        plSpare.Visible = false;
        PlRunningHour.Visible = false;
        //plFinaliseSetup.Visible = true;
        tdComponentTree.Visible = true;
        btnSpecification.CssClass = "btn1";
        btnAssignSpare.CssClass = "btn1";
        btnAssignJobs.CssClass = "btn1";
        btnAssignRunningHour.CssClass = "btn1";
        //btnFinaliseSetup.CssClass = "selbtn";
        if (ddlVessels.SelectedIndex != 0)
        {
            btnRefresh_Click(sender, e);
        }
        else
        {
            btnAssignJobs_Click(sender, e);
            MessageBox1.ShowMessage("Please select a vessel first.", true);
        }

    }
    protected void btnAddStructure_Click(object sender, EventArgs e)
    {
        if (btnAddStructure.ImageUrl.Contains("add.png"))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openstructurewindow('" + ddlVessels.SelectedValue.ToString() + "','ADD');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openstructurewindow('" + ddlVessels.SelectedValue.ToString() + "','EDIT');", true);
        }
    }
    protected void btnRefTv_Click(object sender, EventArgs e)
    {
        ddlVessels_SelectedIndexChanged(sender, e);
    }
    protected void btnPrintCompList_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "report", "opencompreport('Vessel','" + ddlVessels.SelectedValue.Trim() + "','" + ddlVessels.SelectedItem.Text + "');", true);
    }
    protected void btnPrintJobs_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "report", "opencompjobreport('Vessel','" + ddlVessels.SelectedValue.Trim() + "','" + ddlVessels.SelectedItem.Text + "');", true);
    }
    #endregion

    //#region ---------------- Assign Units ---------------------------
    //protected void btnAddUnits_Click(object sender, EventArgs e)
    //{
    //    if (ddlVessels.SelectedValue != null && tvComponents.SelectedNode.Value != null)
    //    {
    //        if (ddlVessels.SelectedValue.ToString() == "0")
    //        {
    //            //lblMessage.Text = "Please select a Vessel.";
    //            MessageBox1.ShowMessage("Please select a Vessel.", true);
    //            return;
    //        }
    //        if (tvComponents.SelectedNode.Value.ToString().Trim() == "" || tvComponents.SelectedNode.Value.ToString().Trim().Length == 2)
    //        {
    //            //lblMessage.Text = "Please select a component.";
    //            MessageBox1.ShowMessage("Please select a component.", true);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddunitwindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString() + "');", true);
    //        }
    //    }
    //}
    //protected void btnEditUnits_Click(object sender, EventArgs e)
    //{
    //    string CompIds = "";
    //    if (ddlVessels.SelectedValue != null && tvComponents.SelectedNode.Value != null)
    //    {
    //        if (ddlVessels.SelectedValue.ToString() == "0")
    //        {
    //            //lblMessage.Text = "Please select a Vessel.";
    //            MessageBox1.ShowMessage("Please select a Vessel.", true);
    //            return;
    //        }
    //        if (tvComponents.SelectedNode.Value.ToString().Trim() == "" || tvComponents.SelectedNode.Value.ToString().Trim().Length == 2)
    //        {
    //            //lblMessage.Text = "Please select a component.";
    //            MessageBox1.ShowMessage("Please select a component.", true);
    //            return;
    //        }
    //        if (rptComponentsUnits.Items.Count > 0)
    //        {
    //            foreach (RepeaterItem rptItem in rptComponentsUnits.Items)
    //            {
    //                CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
    //                HiddenField hfComponentId = (HiddenField)rptItem.FindControl("hfComponentId");
    //                if (chkSelect.Checked)
    //                {
    //                    CompIds = CompIds + hfComponentId.Value.ToString() + ",";
    //                }
    //            }
    //        }
    //        else
    //        {
    //            //lblMessage.Text = "No unit exist for selected component.";
    //            MessageBox1.ShowMessage("No unit exist for selected component.", true);
    //            return;
    //        }
    //        if (CompIds == "")
    //        {
    //            //lblMessage.Text = "Please select a component to Edit Units.";
    //            MessageBox1.ShowMessage("Please select a component to Edit Units.", true);
    //            return;
    //        }
    //        else
    //        {
    //            string strCompIds = CompIds.Remove(CompIds.Length - 1, 1);
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openeditunitwindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString() + "','" + strCompIds + "');", true);
    //        }
    //    }
    //}
    //protected void imgbtnDeleteUnits_Click(object sender, EventArgs e)
    //{
    //    int vesselunitId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    string strSQL = "DELETE FROM VesselComponentMaster WHERE VesselCode='" + ddlVessels.SelectedValue.ToString() + "' AND VesselComponentId=" + vesselunitId + " ";
    //    Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    BindComponentUnits();
    //}    
    //#endregion

    #region --------------- Specification --------------------------
    protected void imgbtneditSpec_Click(object sender, ImageClickEventArgs e)
    {
        btnSaveSpec.Visible = true;
        btnCancelSpec.Visible = true;
    }
    protected void btnCancelSpec_Click(object sender, EventArgs e)
    {
        BindSpecification();
        btnSaveSpec.Visible = false;
        btnCancelSpec.Visible = false;
    }
    protected void chkClass_OnCheckedChanged(object sender, EventArgs e)
    {
        txtClassCode.ReadOnly =! (chkClass.Checked);
        if(!(chkClass.Checked))
            txtClassCode.Text="";
    }
    protected void btnSaveSpec_Click(object sender, EventArgs e)
    {
       
        string classqeuipcode="";
        if(chkClass.Checked)
            classqeuipcode=txtClassCode.Text.Trim();
            //if (txtMaker.Text != "" || txtMakerType.Text != "")
            //{
                try
                {
                    Common.Set_Procedures("sp_Update_Vessel_Specifications");
                    Common.Set_ParameterLength(8);
                    Common.Set_Parameters(
                        new MyParameter("@VesselCode", ddlVessels.SelectedValue.ToString().Trim()),
                        new MyParameter("@ComponentCode", tvComponents.SelectedNode.Value.Trim()),
                        new MyParameter("@Maker", txtMaker.Text.Trim()),
                        new MyParameter("@MakerType", txtMakerType.Text.Trim()),
                        new MyParameter("@ClassEquipCode", classqeuipcode),
                        new MyParameter("@Descr", txtComponentDesc.Text.Trim()),
                        new MyParameter("@CLASSEQUIP", (chkClass.Checked?"1":"0")),
                        new MyParameter("@ACCOUNTCODES", txtAccountCodes.Text.Trim())
                        );

                    DataSet dsUpdate = new DataSet();
                    dsUpdate.Clear();
                    Boolean res;
                    res = Common.Execute_Procedures_IUD(dsUpdate);

                    if (res)
                    {
                        MessageBox1.ShowMessage("Component updated successfully.", false);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox1.ShowMessage("Unable to update Component.Error :" + ex.Message + Common.getLastError(), true);
                }

                //string strUpdateSQL = "UPDATE ComponentMasterForVessel SET Maker = '" + txtMaker.Text.Trim() + "',MakerType='" + txtMakerType.Text.Trim() + "',ClassEquipCode ='" + txtClassCode.Text.Trim() + "' WHERE VesselCode= '" + ddlVessels.SelectedValue.Trim() + "' and ComponentId = " + hfCompId.Value + "; SELECT -1 ";
                //DataTable dtUpdate = Common.Execute_Procedures_Select_ByQuery(strUpdateSQL);
                //if (tvComponents.SelectedNode.Value.Trim().Length != 3)
                //{
                //         string strSelectSQL = "SELECT CMV.ComponentId FROM ComponentMaster CM INNER JOIN ComponentMasterForVessel CMV ON CM.ComponentId = CMV.ComponentId AND CMV.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
                //                               "WHERE LEN(CM.ComponentCode)= (LEN('" + tvComponents.SelectedNode.Value.Trim() + "')+3) AND  LEFT(CM.ComponentCode,(LEN('" + tvComponents.SelectedNode.Value.Trim() + "'))) = LEFT('" + tvComponents.SelectedNode.Value.Trim() + "',(LEN('" + tvComponents.SelectedNode.Value.Trim() + "'))) " +
                //                               "OR LEN(CM.ComponentCode)= (LEN('" + tvComponents.SelectedNode.Value.Trim() + "')+6) AND  LEFT(CM.ComponentCode,(LEN('" + tvComponents.SelectedNode.Value.Trim() + "'))) = LEFT('" + tvComponents.SelectedNode.Value.Trim() + "',(LEN('" + tvComponents.SelectedNode.Value.Trim() + "'))) ";
                //         DataTable dtChilds = Common.Execute_Procedures_Select_ByQuery(strSelectSQL);
                //         if (dtChilds.Rows.Count > 0)
                //         {
                //            string ComponentIds = "";
                //            foreach (DataRow dr in dtChilds.Rows)
                //            {
                //                ComponentIds = ComponentIds + dr["ComponentId"].ToString() + ",";
                //            }
                //            ComponentIds = ComponentIds.Remove(ComponentIds.Length - 1);
                //            string strUpdateChilds = "UPDATE ComponentMasterForVessel SET Maker = '" + txtMaker.Text.Trim() + "',MakerType='" + txtMakerType.Text.Trim() + "',ClassEquipCode ='" + txtClassCode.Text.Trim() + "' WHERE VesselCode= '" + ddlVessels.SelectedValue.Trim() + "' and ComponentId IN(" + ComponentIds + "); SELECT -1 ";
                //            DataTable dtUpdateChilds = Common.Execute_Procedures_Select_ByQuery(strUpdateChilds);
                //        }
                //}
                //if (dtUpdate.Rows.Count > 0)
                //{
                //    MessageBox1.ShowMessage("Component updated successfully.", false);
                //}
                //else
                //{
                //    MessageBox1.ShowMessage("Unable to update component.", true);
                //}
            //}
        }

    #endregion -------------------------------------------------------------------------

    #region ---------------- Assign Jobs ---------------------------
    protected void btnAddJobs_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex != 0)
        {
            if (tvComponents.SelectedNode != null)
            {
                if (ddlVessels.SelectedValue.ToString().Trim() == "")
                {   
                    MessageBox1.ShowMessage("Please select a Vessel.",true);
                    return;
                }
                if (tvComponents.SelectedNode.Value.ToString() == "" || tvComponents.SelectedNode.Value.ToString().Trim().Length == 3)
                {
                    MessageBox1.ShowMessage("Please select a component.", true);
                    return;
                }
                //if (tvComponents.SelectedNode.Value.ToString().Trim().Length == 6)
                //{
                //    DataTable dtUnitsDetails;
                //    string strSQL;
                //    strSQL = "SELECT VCM.ComponentName  FROM VesselComponentMaster VCM  " +
                //             "INNER JOIN ComponentMaster CM ON VCM.ComponentId = CM.ComponentId " +
                //             "WHERE VCM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND CM.ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "'";
                //    dtUnitsDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
                //    if (dtUnitsDetails.Rows.Count > 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openjobwindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "');", true);
                //    }
                //    else
                //    {
                //        //lblMessage.Text = "No Unit exists for the selected component.";
                //        MessageBox1.ShowMessage("No Unit exists for the selected component.", true);
                //    }
                //}
                //else
                //{
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openjobwindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "');", true);
                //}
            }
            else
            {
                MessageBox1.ShowMessage("Please select a component.", true);
            }
        }
        else
        {
            MessageBox1.ShowMessage("Please select a Vessel.", true);
        }
    }
    protected void imgbtnEditJobs_Click(object sender, ImageClickEventArgs e)
    {
        Boolean count = false;
        string jobIds = "";
        foreach (RepeaterItem rptItem in rptVesselComponents.Items)
        {
            CheckBox chkSelectJobs = (CheckBox)rptItem.FindControl("chkSelectJobs");
            HiddenField hfdJobId = (HiddenField)rptItem.FindControl("hfdJobId");
            if (chkSelectJobs.Checked)
            {
                count = true;
                jobIds = jobIds + hfdJobId.Value + ",";
            }
        }        
        if (count == false)
        {
            MessageBox1.ShowMessage("Please select a job to edit.", true);
            return;
        }
        jobIds = jobIds.Remove(jobIds.Length - 1);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openeditjobwindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "','" + jobIds + "');", true);
    }
    //protected void ImgbtnDeleteJobs_Click(object sender, ImageClickEventArgs e)
    //{
    //    if (ddlVessels.SelectedValue.ToString().Trim() == "")
    //    {
    //        MessageBox1.ShowMessage("Please select a Vessel.", true);
    //        return;
    //    }
    //    if (tvComponents.SelectedNode.Value.ToString() == "" || tvComponents.SelectedNode.Value.ToString().Trim().Length == 2)
    //    {
    //        MessageBox1.ShowMessage("Please select a component.", true);
    //        return;
    //    }
    //    if (tvComponents.SelectedNode.Value.ToString().Trim().Length == 6)
    //    {
    //        DataTable dtUnitsDetails;
    //        string strSQL;
    //        strSQL = "SELECT VCM.ComponentName  FROM VesselComponentMaster VCM  " +
    //                 "INNER JOIN ComponentMaster CM ON VCM.ComponentId = CM.ComponentId " +
    //                 "WHERE VCM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND CM.ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "'";
    //        dtUnitsDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //        if (dtUnitsDetails.Rows.Count > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "opendeletejobwindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "');", true);
    //        }
    //        else
    //        {
    //            MessageBox1.ShowMessage("No Unit exists for the selected component.", true);
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "opendeletejobwindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "');", true);
    //    }

    //}            
    protected void ImgbtnDeleteJobs_Click(object sender, ImageClickEventArgs e)
    {
        Boolean Count = false;
        if (tvComponents.SelectedNode == null)
        {
            MessageBox1.ShowMessage("Please select a component.", true);
            return;
        }
        if (tvComponents.SelectedNode.Value.ToString() == "" || tvComponents.SelectedNode.Value.ToString().Trim().Length == 3)
        {
            MessageBox1.ShowMessage("Please select a component.", true);
            return;
        }
        foreach (RepeaterItem rptItem in rptVesselComponents.Items)
        {
            CheckBox chkSelectJobs = (CheckBox)rptItem.FindControl("chkSelectJobs");
            if (chkSelectJobs.Checked)
            {
                Count = true;
            }
        }
        if (Count == false)
        {
            MessageBox1.ShowMessage("Please select a job to delete.", true);
            return;
        }
        else
        {               
            foreach (RepeaterItem rptItem in rptVesselComponents.Items)
            {
                CheckBox chkSelectJobs = (CheckBox)rptItem.FindControl("chkSelectJobs");
                HiddenField hfVcId = (HiddenField)rptItem.FindControl("hfVcId");
                HiddenField hfdJobId = (HiddenField)rptItem.FindControl("hfdJobId");
                if (chkSelectJobs.Checked)
                {
                    Common.Set_Procedures("sp_Delete_Vessel_Jobs");
                    Common.Set_ParameterLength(3);
                    Common.Set_Parameters(
                        new MyParameter("@VesselCode", ddlVessels.SelectedValue.ToString().Trim()),
                        new MyParameter("@ComponentId", hfVcId.Value),
                        new MyParameter("@CompJobId", hfdJobId.Value)
                        );

                    DataSet dsDelete = new DataSet();
                    dsDelete.Clear();
                    Boolean res;
                    res = Common.Execute_Procedures_IUD(dsDelete);

                    if (res)
                    {
                        MessageBox1.ShowMessage("Job deleted successfully.", false);
                    }
                    else
                    {
                        MessageBox1.ShowMessage("Unable to delete job.Error :" + Common.getLastError(), true);
                        chkSelectJobs.Focus();
                    }

                    //string strSQL = "DELETE FROM VesselComponentJobMaster WHERE VesselCode='" + ddlVessels.SelectedValue.ToString().Trim() + "' AND ComponentId = " + hfVcId.Value + " AND CompJobId=" + hfdJobId.Value + " ; SELECT -1 ";
                    //DataTable dt =  Common.Execute_Procedures_Select_ByQuery(strSQL);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    MessageBox1.ShowMessage("Job deleted successfully.", false);
                    //}
                    //else
                    //{
                    //    MessageBox1.ShowMessage("Unable to delete job", true);
                    //    chkSelectJobs.Focus();
                    //    return;
                    //}
                }
            }
            BindVesselComponents();
        }
    }
    #endregion

    #region ---------------- Assign Spares ---------------------------
    public void lbtnSelect_Click(object sender, EventArgs e)
    {
        string compId = "";
        string Off_Ship = "";
        DataTable dt = new DataTable();
        string strCompId = "SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "'";
        dt = Common.Execute_Procedures_Select_ByQuery(strCompId);
        compId = dt.Rows[0]["ComponentId"].ToString();
        int SpareId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        HiddenField hf_Off_Ship = (HiddenField)((ImageButton)sender).FindControl("hf_Off_Ship");
        Off_Ship = hf_Off_Ship.Value.Trim();

        string strAttachment = "SELECT Attachment FROM VesselSpareMaster WHERE VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND ComponentId = " + compId + " AND Office_Ship = '" + Off_Ship +"' AND SpareId = " + SpareId;
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strAttachment);
        string FileName = dtAttachment.Rows[0]["Attachment"].ToString().Trim();
        try
        {
            Common.Set_Procedures("sp_Delete_Vessel_Spares");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", ddlVessels.SelectedValue.ToString().Trim()),
                new MyParameter("@ComponentId", compId),
                new MyParameter("@Office_Ship", Off_Ship.Trim()),
                new MyParameter("@SpareId", SpareId)
                );

            DataSet dsDelete = new DataSet();
            dsDelete.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsDelete);

            if (res)
            {
                if (FileName.Trim() != "")
                {
                    string path = Server.MapPath("~/UploadFiles/UploadSpareDocs/");
                    System.IO.File.Delete(path + FileName);
                }
                lblMessage.Text = "Spare Deleted Successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refresh()", true);
            }            
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Unable to Delete Spare.Error :" + ex.Message + Common.getLastError();
        }

        //string strSQL = "DELETE FROM VesselSpareMaster WHERE VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND ComponentId = " + compId + " AND SpareId =" + SpareId + " ";
        //Common.Execute_Procedures_Select_ByQuery(strSQL);
        ShowComponentSpare();
    }
    protected void btnAddSpares_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlVessels.SelectedIndex != 0)
        {
            if (tvComponents.SelectedNode != null)
            {
                if (tvComponents.SelectedNode.Value.ToString().Trim() == "" || tvComponents.SelectedNode.Value.ToString().Trim().Length == 2)
                {
                    MessageBox1.ShowMessage("Please Select a component.", true);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "',' ');", true);
            }
            else
            {
                MessageBox1.ShowMessage("Please Select a component.", true);
            }
        }
        else
        {
            MessageBox1.ShowMessage("Please select a Vessel.", true);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int SpareId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "','" + SpareId + "');", true);
    }
     
    #endregion -----------------------------------------------------

    #region ----------------- Assign Running Hour -------------------
    protected void btnPrintRunnongHours_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "prnt", "window.open('Reports/VesselSetupRunningHour.aspx?VesselCode=" + ddlVessels.SelectedValue.Trim() + "', '', '');", true);   
    }
    protected void btnRunHSave_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem rpt in rptRunningHour.Items)
        {
            TextBox txtStartupHour = (TextBox)rpt.FindControl("txtStartupHour");
            TextBox txtAvgRunHour = (TextBox)rpt.FindControl("txtAvgRunHour");
            TextBox txtDueDate = (TextBox)rpt.FindControl("txtDueDate");

            //if (txtStartupHour.Text.Trim() == "" && txtDueDate.Text.Trim() == "" && txtAvgRunHour.Text.Trim()  == "")
            //{continue;}
            ////---------------------------
            //if (txtStartupHour.Text == "")
            //{
            //    MessageBox1.ShowMessage("Please enter new hr.", true);
            //    txtStartupHour.Focus();
            //    return;
            //}
            //if (txtDueDate.Text == "")
            //{
            //    MessageBox1.ShowMessage("Please enter as on dt.", true);
            //    return;
            //}
            //if (txtAvgRunHour.Text == "")
            //{
            //    MessageBox1.ShowMessage("Please enter avg hr.", true);
            //    txtAvgRunHour.Focus();
            //    return;
            //}
            //---------------------------
            if (Common.CastAsInt32(txtAvgRunHour.Text) > 25)
            {
                MessageBox1.ShowMessage("Avg hr. should not be more than 25.", true);
                txtAvgRunHour.Focus();
                return;
            }
        }

        foreach (RepeaterItem rptItem in rptRunningHour.Items)
        {
            object Dt = DBNull.Value;
            HiddenField hfCompId = (HiddenField)rptItem.FindControl("hfCompId");
            TextBox txtStartupHour = (TextBox)rptItem.FindControl("txtStartupHour");
            TextBox txtAvgRunHour = (TextBox)rptItem.FindControl("txtAvgRunHour");
            TextBox txtDueDate = (TextBox)rptItem.FindControl("txtDueDate");
            if (txtDueDate.Text.Trim() != "")
            {
                try
                {
                    Dt = Convert.ToDateTime(txtDueDate.Text);
                }
                catch { Dt = DBNull.Value; }
            }
            
            Common.Set_Procedures("sp_InsertUpdateRunningHour");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim()),
                new MyParameter("@ComponentId", Common.CastAsInt32(hfCompId.Value)),
                new MyParameter("@StartupHour", Common.CastAsInt32(txtStartupHour.Text.Trim())),
                new MyParameter("@AvgRunningHrPerDay", Common.CastAsInt32(txtAvgRunHour.Text.Trim())),
                new MyParameter("@StartDate", Dt),
                new MyParameter("@UpdatedBy", 0)
                );

            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                msgRunHour.ShowMessage("Running Hour Details Added/Udpated Successfully.", false);
            }
            else
            {
                msgRunHour.ShowMessage("Unable to Add/Update Running Hour Details.Error :" + Common.getLastError(), true);
            }
        }
    }
    #endregion

    #region ------------------ Finalise Setup ------------------------
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel first.", true);
            return;
        }
        try
        {
            Common.Set_Procedures("sp_ExportVessel");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters( 
               new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim())
               );
            DataSet dsExport = new DataSet();
            dsExport.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsExport);
            if (res)
            { 
              MessageBox1.ShowMessage("Exported Successfully.", false);
            }
            else
            {
                MessageBox1.ShowMessage("Unable To Export.Error :" + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable To Export.Error :" + ex.Message + Common.getLastError(), true);
        }
    }
    #endregion

    #endregion
}
