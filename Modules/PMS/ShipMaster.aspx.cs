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
using System.IO;
using Ionic.Zip;   

public partial class ShipMaster : System.Web.UI.Page
{
    private int PageSize = 100;
    public string UserType
    {
        set { ViewState["UserType"] = value; }
        get { return ViewState["UserType"].ToString(); }
    }
    AuthenticationManager Auth;
    bool setScroll = true;
    public int cp_CompId
    {
        set { ViewState["CI"] = value; }
        get { return Common.CastAsInt32(ViewState["CI"]); }
    }
    public int cp_SpareID
    {
        set { ViewState["cp_SpareID"] = value; }
        get { return Common.CastAsInt32(ViewState["cp_SpareID"]); }
    }
    public string cp_OfficeShip
    {
        set { ViewState["cp_OfficeShip"] = value; }
        get { return Convert.ToString( ViewState["cp_OfficeShip"]); }
    }
    public bool CanUdateJob = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        //***********Code to check page acessing Permission
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1041, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");

            }
            else
            {
                btnAddComponents.Visible = Auth.IsAdd;
                imgbtneditSpec.Visible = Auth.IsUpdate;
                btnPring.Visible = Auth.IsPrint;
            }
            // Auth = new AuthenticationManager(1041, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            CanUdateJob = Auth.IsUpdate;
            if (!Auth.IsView)
            {
                btnAssignJobs.Visible = false;
            }
            else
            {
                btnAddJobs.Visible = Auth.IsAdd;
                imgbtnEditJobs.Visible = Auth.IsUpdate;
                btnPrintJobsReport.Visible = Auth.IsPrint;
                btnPrintJobs.Visible = Auth.IsPrint;
            }
            // Auth = new AuthenticationManager(1041, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!Auth.IsView)
            {
                btnAssignSpare.Visible = false;
            }
            else
            {
                btnAddSpares.Visible = Auth.IsAdd;
                btnSparePrint.Visible = Auth.IsPrint;
            }
        }
        //*******************

        if (!Page.IsPostBack)
        {
            UserType = Session["UserType"].ToString();
            Session["CurrentModule"] = 2;
            BindJobType();
            if (Session["UserType"].ToString() == "S")
            {
                btnCreateDB.Visible = false;
                ddlVessels.Items.Insert(0, new ListItem("< SELECT >", "0"));
                ddlVessels.Items.Insert(1, new ListItem(Session["CurrentShip"].ToString(), Session["CurrentShip"].ToString()));

                ddlVessels.SelectedIndex = 1;
                ddlVessels_SelectedIndexChanged(sender, e);
                ddlVessels.Visible = false;
                divSDRequest.Visible = true;
            }
            else
            {
                BindVessels();
                //trRhEntry.Visible = false;
                if (! string.IsNullOrEmpty(Session["IsSuperUser"].ToString()) && Session["IsSuperUser"].ToString() == "Y")
                {
                    btnCreateDB.Visible = true;
                }
                else
                {
                    btnCreateDB.Visible = false;
                }
            }
        }
        //else
        //{
        //    if (Session["UserType"].ToString() == "S")
        //    {
        //        if (Convert.ToInt32(ddlVessels.SelectedValue) > 0)
        //        {
        //            ddlVessels_SelectedIndexChanged(sender, e);
        //        }
        //    }
        //}
    }
    public void BindJobType()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "select JobId,JobName from dbo. JobMaster order by JobName  ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlJobType.DataSource = dtVessels;
            ddlJobType.DataTextField = "JobName";
            ddlJobType.DataValueField = "JobId";
            ddlJobType.DataBind();

            ddlJobType_H.DataSource = dtVessels;
            ddlJobType_H.DataTextField = "JobName";
            ddlJobType_H.DataValueField = "JobId";
            ddlJobType_H.DataBind();
            ddlJobType_H.Items.Add(new ListItem("DFR", "-1"));
            ddlJobType_H.Items.Add(new ListItem("UPJ", "-2"));
        }
        else
        {
            ddlJobType.DataSource = null;
            ddlJobType.DataBind();

            ddlJobType_H.DataSource = null;
            ddlJobType_H.DataBind();
        }
        ddlJobType.Items.Insert(0, "< All >");
        ddlJobType_H.Items.Insert(0, "< All >");
    }
    #region ---------------- USER DEFINED FUNCTIONS ---------------------------
    #region ---------------- Commom ---------------------------------

    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE VESSELSTATUSID=1 AND EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) AND VM.VesselId IN (Select uvr.VesselId from UserVesselRelation uvr with(nolock) where uvr.LoginId = "+ Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY VesselName ";
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
        string strSQL = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))=3 AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "'  AND CMV.Status='A' Order By ComponentCode";
        dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        tvComponents.Nodes.Clear();
        TreeNode root = new TreeNode();
        root.Text = "COMPONENT STRUCTURE";
        root.Value = "";
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
                String strQuery = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))=6 AND LEFT(CM.ComponentCode,3)='" + gn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' AND CMV.Status='A' Order By ComponentCode";
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
                        string SQL = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))= 9 AND LEFT(CM.ComponentCode,6)='" + sn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' AND CMV.Status='A' Order By ComponentCode";
                        dtSystem = Common.Execute_Procedures_Select_ByQuery(SQL);
                        if (dtSystem.Rows.Count > 0)
                        {
                            sn.PopulateOnDemand = true;
                        }
                        gn.ChildNodes.Add(sn);
                    }
                }
                root.ChildNodes.Add(gn);
                
            }
        }
        root.Expanded = true;
        tvComponents.Nodes.Add(root);
    }
    public void BindOnVesselChange()
    {
        // -------------- Specification Section --------------------
        hfCompId.Value = "";
        lblComponentCode.Text = "";
        lblLinkedto.Text = "";
        lblComponentName.Text = "";
        txtComponentDesc.Text = "";
        lblMaker.Text = "";
        lblMakerType.Text = "";
        chkClass.Checked = false;
        lblAccountCodes.Text="";
        txtAccountCodes.Text = "";
        chkOR.Checked = false;
        lblClassCode.Text = "";
        chkCritical.Checked = false;
        chkCE.Checked = false;
        chkCE.Visible = false;
        
        chkInactive.Checked = false;
        chkRHComponent.Checked = false;
        // -------------- Job Section --------------------
        rptVesselComponents.DataSource = null;
        rptVesselComponents.DataBind();

        // -------------- Spare Section --------------------
        rptComponentSpares.DataSource = null;
        rptComponentSpares.DataBind();

        // -------------- Job History Section --------------------
        rptJobHistory.DataSource = null;
        rptJobHistory.DataBind();

        // -------------- Critical Shutdown Section --------------------
        rptShutdown.DataSource = null;
        rptShutdown.DataBind();
    }
    #endregion

    #region ------------------ Assign Specification -----------------

    public void BindSpecification() 
    {
        string Code = tvComponents.SelectedNode.Value.Trim();
        DataTable dtSpec;
        //string strSpecSQL = "SELECT ComponentId,ComponentCode,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster WHERE LEN(ComponentCode)= (LEN('" + Code + "')-2) AND  LEFT(ComponentCode,(LEN('" + Code + "')-2)) = LEFT('" + Code + "',(LEN('" + Code + "')-2)))As LinkedTo,ComponentName,Descr,ClassEquip,SmsEquip,ClassEquipCode,SmsCode,CriticalEquip,CSMItem,Inactive,InheritParentJobs FROM ComponentMaster WHERE ComponentCode ='" + Code + "'";
        string strSpecSQL = "SELECT CM.ComponentId,CM.ComponentCode ,(Select LTRIM(RTRIM(CM1.ComponentCode)) + ' - ' + CM1.ComponentName from ComponentMaster CM1 WHERE LEN(CM1.ComponentCode)= (LEN('" + Code + "')-3) AND  LEFT(CM1.ComponentCode,(LEN('" + Code + "')-3)) = LEFT('" + Code + "',(LEN('" + Code + "')-3)))As LinkedTo,CM.ComponentName,CMV.Descr,CAST( ISNULL(CMV.ClassEquip,0) AS BIT) AS ClassEquip,CMV.ACCOUNTCODES,CM.CriticalEquip,CM.Inactive,CMV.Maker,CMV.MakerType,CMV.isOR,CMV.ClassEquipCode,CM.CriticalType,CM.RHComponent FROM ComponentMaster CM " +
                            "INNER JOIN VSL_ComponentMasterForVessel CMV ON CM.ComponentId = CMV.ComponentId AND CMV.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' " +
                            "WHERE CM.ComponentCode ='" + Code + "'";
        //,CM.RHComponent
        Session.Add("sSqlForPrint", strSpecSQL);
        lnkOpenSuppliers.Visible = false;
        dtSpec = Common.Execute_Procedures_Select_ByQuery(strSpecSQL);
        if (dtSpec.Rows.Count > 0)
        {
            if (Session["UserType"].ToString() == "O")
                lnkOpenSuppliers.Visible = true;

            hfCompId.Value = dtSpec.Rows[0]["ComponentId"].ToString().Trim();
            lblComponentCode.Text = dtSpec.Rows[0]["ComponentCode"].ToString().Trim();
            lblLinkedto.Text = dtSpec.Rows[0]["LinkedTo"].ToString().Trim();
            lblComponentName.Text = dtSpec.Rows[0]["ComponentName"].ToString();
            lblComponentDesc.Text = dtSpec.Rows[0]["Descr"].ToString();
            txtComponentDesc.Text = dtSpec.Rows[0]["Descr"].ToString();
            lblMaker.Text = dtSpec.Rows[0]["Maker"].ToString();
            txtMaker.Text = dtSpec.Rows[0]["Maker"].ToString();
            lblMakerType.Text = dtSpec.Rows[0]["MakerType"].ToString();
            txtMakerType.Text = dtSpec.Rows[0]["MakerType"].ToString();
            chkClass.Checked = Convert.ToBoolean(dtSpec.Rows[0]["ClassEquip"].ToString());
            lblAccountCodes.Text = dtSpec.Rows[0]["ACCOUNTCODES"].ToString();
            txtAccountCodes.Text = dtSpec.Rows[0]["ACCOUNTCODES"].ToString();
            
            chkOR.Checked =  (dtSpec.Rows[0]["isOR"] ==DBNull.Value)?false:Convert.ToBoolean(dtSpec.Rows[0]["isOR"]) ;
            //chkSms.Checked = Convert.ToBoolean(dtSpec.Rows[0]["SmsEquip"].ToString());
            lblClassCode.Text = dtSpec.Rows[0]["ClassEquipCode"].ToString();
            txtClassCode.Text = dtSpec.Rows[0]["ClassEquipCode"].ToString();
            //txtSmsCode.Text = dtSpec.Rows[0]["SmsCode"].ToString();
            chkCritical.Checked = Convert.ToBoolean(dtSpec.Rows[0]["CriticalEquip"].ToString());
            chkCE.Visible = chkCritical.Checked;
            chkCE.Checked = dtSpec.Rows[0]["CriticalType"].ToString()=="E";
            //chkCSM.Checked = Convert.ToBoolean(dtSpec.Rows[0]["CSMItem"].ToString());
            chkInactive.Checked = Convert.ToBoolean(dtSpec.Rows[0]["Inactive"].ToString());
            chkRHComponent.Checked = Convert.ToBoolean(dtSpec.Rows[0]["RHComponent"].ToString());
            //if (tvComponents.SelectedValue.Trim().Length == 9)
            //{
            //    if (dtSpec.Rows[0]["InheritParentJobs"] != null && dtSpec.Rows[0]["InheritParentJobs"].ToString() != "")
            //    {
            //        chkInhParent.Checked = Convert.ToBoolean(dtSpec.Rows[0]["InheritParentJobs"].ToString());
            //    }
            //}
        }
        else
        {
        }
    }


    #endregion

    #region ---------------- Assign Jobs ---------------------------

    private void BindComponentJobs()
    {
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1041, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            //imgbtnEditJobs.Visible = Auth.IsUpdate;
            btnAddJobs.Visible = Auth.IsAdd;
        }
        else
        {
            btnAddJobs.Visible = false;
            imgbtnEditJobs.Visible = false;
        }
        
        
        if (tvComponents.SelectedNode.Value.ToString().Trim().Length != 3)
        {
            DataTable dtJobDetails;
            string strSQL, WhereClause;
            strSQL = "SELECT  VCJM.VesselCode,LTRIM(RTRIM(CM.ComponentCode)) As CompCode,CM.ComponentName,CM.CriticalEquip AS IsCritical,CM.CriticalType,DM.DeptName,RM.RankCode,JM.JobName AS JobCode ,CJM.CompJobId,CJM.DescrSh AS JobName,CASE VCJM.IntervalId WHEN 1 THEN CASE VCJM.IntervalId_H WHEN 0 THEN JIM.IntervalName + '-' + CAST(VCJM.Interval AS VARCHAR) ELSE JIM.IntervalName + '-' + CAST(VCJM.Interval AS Varchar)   + '  OR  ' + (SELECT IntervalName FROM JobIntervalMaster WHERE IntervalId = VCJM.IntervalId_H ) + '-' + CAST(VCJM.Interval_H AS VARCHAR) END ELSE JIM.IntervalName + '-' + CAST(VCJM.Interval AS VARCHAR) END AS Interval,JM.JobId,VCJM.ComponentId,cjm.AttachmentForm,cjm.RiskAssessment,VCJM.Guidelines "+
                    " ,dbo.GetJobAttachmentCount(cjm.CompJobId,VCJM.VesselCode,'S') as AttachmentCount,vcjm.JobCost " +                            
                    " from VSL_VesselComponentJobMaster VCJM " +
                     "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId " +
                     "INNER JOIN ComponentsJobMapping CJM ON CJM.ComponentId = VCJM.ComponentId AND CJM.CompJobId = VCJM.CompJobId " +
                     "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                     "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
                     "INNER JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId " +                     
                     "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                     "WHERE VCJM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND CM.ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "' AND VCJM.Status='A' ";

            WhereClause = "";
            if (ddlJobType.SelectedIndex != 0)
            {
                WhereClause = " and JM.JobId=" + ddlJobType.SelectedValue;
            }

            Session.Add("sSqlForPrint", strSQL + WhereClause + " ORDER BY JobCode,CompCode");
            dtJobDetails = Common.Execute_Procedures_Select_ByQuery(strSQL + WhereClause + " ORDER BY JobCode,CompCode");

            if (dtJobDetails.Rows.Count > 0)
            {
                rptVesselComponents.DataSource = dtJobDetails;
                rptVesselComponents.DataBind();
                rptVesselComponents.Visible = true;

                if (Session["UserType"].ToString() == "O")
                {
                    Auth = new AuthenticationManager(1041, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
                    imgbtnEditJobs.Visible = Auth.IsUpdate;
                }
                //else
                //{
                //    imgbtnEditJobs.Visible = true;
                //}
            }
            else
            {
                rptVesselComponents.DataSource = null;
                rptVesselComponents.DataBind();
                rptVesselComponents.Visible = false;

                //imgbtnEditJobs.Visible = false;
               
            }
        }
        else
        {
            rptVesselComponents.DataSource = null;
            rptVesselComponents.DataBind();
            rptVesselComponents.Visible = false;

            btnAddJobs.Visible = false;
            imgbtnEditJobs.Visible = false;
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
        
    }
    protected void dlsparestatus_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        ShowComponentSpare(0);
    }
    public void ShowComponentSpare(int getPageNo)
    {
        
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1041, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            btnAddSpares.Visible = Auth.IsAdd;
            btnSparePrint.Visible = Auth.IsPrint;
            Session.Add("IsEdit", Auth.IsUpdate);

        }
        else
        {
            Session.Add("IsEdit", ""); 
        }
        string Code = tvComponents.SelectedNode.Value.ToString().Trim();
        DataTable dtSpareDetails;
        string strSQL = "";
        if (Session["UserType"].ToString() == "S")
        {
            strSQL = "SELECT VSM.*,isnull(VSM.Status,'A') as status1,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster WHERE LEN(ComponentCode)= (LEN('" + Code + "')-3) AND  LEFT(ComponentCode,(LEN('" + Code + "')-3)) = LEFT('" + Code + "',(LEN('" + Code + "')-3)))As LinkedTo,dbo.getROB(VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,getdate()) AS ROB,(SELECT TOP 1 StockLocation FROM VSL_StockInventory VSI WHERE VSI.VesselCode = VSM.VesselCode AND VSI.ComponentId = VSM.ComponentId AND VSI.Office_Ship = VSM.Office_Ship AND VSI.SpareId = VSM.SpareId ORDER BY VSI.UpdatedOn DESC ) AS StockLocation FROM VSL_VesselSpareMaster VSM " +
                     "INNER JOIN ComponentMaster CM ON VSM.ComponentId = CM.ComponentId " +
                     "WHERE CM.ComponentCode = '" + Code + "' AND VSM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "'" + ((dlsparestatus.SelectedIndex<=0)? "":" AND isnull(VSM.Status,'A') = '" + dlsparestatus.SelectedValue + "'" );
        }
        else
        {
            strSQL = "SELECT distinct VSM.*,isnull(VSM.Status,'A') as status1,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster with(nolock) WHERE LEN(ComponentCode)= (LEN('" + Code + "')-3) AND  LEFT(ComponentCode,(LEN('" + Code + "')-3)) = LEFT('" + Code + "',(LEN('" + Code + "')-3)))As LinkedTo,dbo.getROB(VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,getdate()) AS ROB, ch.StockLocation AS StockLocation FROM VSL_VesselSpareMaster VSM with(nolock) " +
            " INNER JOIN ComponentMaster CM with(nolock) ON VSM.ComponentId = CM.ComponentId " +
            " LEFT OUTER JOIN  (Select a.*, sl.StockLocation from VSL_StockInventory b with(nolock) Inner JOIN (SELECT VSI.VesselCode,VSI.ComponentId,VSI.Office_Ship,VSI.SpareId,max(VSI.UpdatedOn) As UpdatedOn FROM VSL_StockInventory VSI with(nolock) where VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' group by VesselCode,ComponentId,Office_Ship,SpareId ) a on a.VesselCode = b.VesselCode and a.ComponentId = b.ComponentId and a.Office_Ship = b.Office_Ship and a.SpareId = b.SpareId Inner join SpareStockLocation sl with(nolock) on b.StockLocationId = sl.StockLocationId ) ch on ch.ComponentId = CM.ComponentId and ch.SpareId = VSM.SpareId and VSM.Office_Ship = ch.Office_Ship and VSM.VesselCode = ch.VesselCode  " + 
           
            " WHERE CM.ComponentCode = '" + Code + "' AND VSM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "'" + ((dlsparestatus.SelectedIndex<=0) ? "" : " AND isnull(VSM.Status,'A') = '" + dlsparestatus.SelectedValue + "'");

        } 
        Session.Add("sSqlForPrint", strSQL);
        dtSpareDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtSpareDetails.Rows.Count > 0)
        {
            //PagedDataSource pdsData = new PagedDataSource();
            //DataView dv = new DataView(dtSpareDetails);
            //pdsData.DataSource = dv;
            //pdsData.AllowPaging = true;
            //pdsData.PageSize = iPageSize;
            //if (ViewState["PageNumber"] != null)
            //    pdsData.CurrentPageIndex = Convert.ToInt32(ViewState["PageNumber"]);
            //else
            //    pdsData.CurrentPageIndex = 0;
            //if (pdsData.PageCount > 1)
            //{
            //    rptComponentSparesPaging.Visible = true;
            //    ArrayList alPages = new ArrayList();
            //    for (int i = 1; i <= pdsData.PageCount; i++)
            //        alPages.Add((i).ToString());
            //    rptComponentSparesPaging.DataSource = alPages;
            //    rptComponentSparesPaging.DataBind();
            //}
            //else
            //{
            //    rptComponentSparesPaging.Visible = false;
            //}
            // Step 2: Create PagedDataSource instance and set its properties
            //PagedDataSource pds = new PagedDataSource();
            //pds.DataSource = dtSpareDetails.DefaultView;
            //pds.AllowPaging = true;
            //pds.PageSize = 100;
            //if (SelectedPage > (pds.PageCount - 1))
            //  SelectedPage = pds.PageCount - 1;
            //if (SelectedPage < 0)
            //    SelectedPage = 0;
            //pds.CurrentPageIndex = SelectedPage;
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dtSpareDetails.DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = 100;

            Session["CPNo"] = getPageNo;

            if (getPageNo > (pds.PageCount - 1))
                getPageNo = pds.PageCount - 1;
            if (getPageNo < 0)
                getPageNo = 0;

            pds.CurrentPageIndex = getPageNo;

            if (dtSpareDetails.Rows.Count > 0)
            {
                lblSpareCount.Text = dtSpareDetails.Rows.Count.ToString();
                rptComponentSpares.DataSource = pds;
                rptComponentSpares.DataBind();
            }
            else
            {
                rptComponentSpares.DataSource = null;
                rptComponentSpares.DataBind();
            }


            //enable & disabled previous and Next button
            if (pds.IsLastPage)
                lbNext.Enabled = false;
            else
                lbNext.Enabled = true;

            if (pds.IsFirstPage)
                lbPrevious.Enabled = false;
            else
                lbPrevious.Enabled = true;

            // Step 3: Bind PagedDataSource to Repeater and set LinkButtons' behavior
            rptComponentSpares.DataSource = pds;
            rptComponentSpares.DataBind();
            //if (pds.IsLastPage)
            //    lbNext.Enabled = false;
            //else
            //    lbNext.Enabled = true;

            //if (pds.IsFirstPage)
            //    lbPrevious.Enabled = false;
            //else
            //    lbPrevious.Enabled = true;
            //int recordCount = Convert.ToInt32(dtSpareDetails.Rows.Count);
            //this.PopulatePager(recordCount, pageIndex);

        }
        else
        {
            rptComponentSpares.DataSource = null;
            rptComponentSpares.DataBind();
        }
    }

    protected void lnkbuttonNext_Click(object sender, EventArgs e)
    {
        Session["CPNo"] = (int.Parse(Session["CPNo"].ToString()) + 1).ToString();
        ShowComponentSpare(int.Parse(Session["CPNo"].ToString()));
    }
    protected void lnkbuttonPrev_Click(object sender, EventArgs e)
    {
        Session["CPNo"] = (int.Parse(Session["CPNo"].ToString()) - 1).ToString();
        ShowComponentSpare(int.Parse(Session["CPNo"].ToString()));
    }

    //protected void rptComponentSparesPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    ViewState["PageNumber"] = Convert.ToInt32(e.CommandArgument);
    //    ShowComponentSpare();
    //}
    #endregion

    #region ----------------- Job History -------------------------
    public void ShowHistory()
    {
        string strJobHistorySQL = "SELECT * FROM " +
                                 "(  " +
                                 "SELECT JM.JobId,JH.VesselCode,convert(varchar,JH.HistoryId) as PK,CASE JH.[Action] WHEN 'R' THEN 'REPORT' WHEN 'P' THEN 'POSTPONE' WHEN 'C' THEN 'CANCEL' END AS [Action],CASE JH.[Action] WHEN 'R' THEN REPLACE(Convert(Varchar,JH.DoneDate,106),' ','-') WHEN 'P' THEN REPLACE(Convert(Varchar, JH.PostPoneDate,106),' ','-') WHEN 'C' THEN REPLACE(Convert(Varchar, JH.CancellationDate,106),' ','-') END AS ACTIONDATE,CASE JH.[Action] WHEN 'R' THEN DoneHour ELSE '' END  AS DoneHour,CASE JH.[Action] WHEN 'R' THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(JH.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,JH.DoneDate,JH.LastDueDate) END ELSE '' END AS [Difference],CASE [Action] WHEN 'R' THEN RM.RankCode WHEN 'P' THEN RMP.RankCode WHEN 'C' THEN RMC.RankCode END AS DoneBy,CASE [Action] WHEN 'R' THEN DoneBy_Code WHEN 'P' THEN P_DoneBy_Code WHEN 'C' THEN C_DoneBy_Code END AS EmpNo,CASE [Action] WHEN 'R' THEN DoneBy_Name WHEN 'P' THEN P_DoneBy_Name WHEN 'C' THEN C_DoneBy_Name END AS EmpName,ISNULL(REPLACE(Convert(Varchar, LastDueDate,106),' ','-'),'') AS DueDate,CASE JH.[Action] WHEN 'R' THEN LastDueHours ELSE '' END AS DueHour,CASE JH.[Action] WHEN 'R' THEN CASE UpdateRemarks WHEN 1 THEN 'Planned Job' WHEN 2 THEN CASE WHEN LEN(Specify) > 45 THEN SUBSTRING(Specify,0,45) + '...' ELSE Specify END WHEN 3 THEN 'BREAK DOWN' END WHEN 'P' THEN CASE PostPoneReason WHEN 1 THEN 'Equipment in working condition' WHEN 2 THEN 'Waiting for spares' WHEN 3 THEN 'Dry docking' END WHEN 'C' THEN CASE WHEN LEN(CancellationReason)> 45 THEN SUBSTRING(CancellationReason,0,45) + '...' ELSE CancellationReason END END AS Remarks,ConditionAfter ,JobCode,DESCRSH AS JobDesc  " +
                                 "FROM   " +
                                 "VSL_VesselJobUpdateHistory JH   " +
                                 "INNER JOIN VSL_VesselComponentJobMaster CJM1 ON JH.VESSELCODE=CJM1.VESSELCODE AND JH.COMPJOBID=CJM1.COMPJOBID AND CJM1.STATUS='A' " +
                                 "INNER JOIN VSL_ComponentMasterForVessel CMV ON JH.VESSELCODE=CMV.VESSELCODE AND JH.COMPONENTID=CMV.COMPONENTID AND CMV.STATUS='A' " +
                                 "INNER JOIN jobmaster jm on jm.jobid=JH.jobid " +
                                 "LEFT JOIN ComponentsJobMapping CJM ON CJM.COMPJOBID=JH.COMPJOBID " +
                                 "LEFT JOIN Rank RM ON RM.RankId = JH.DoneBy   " +
                                 "LEFT JOIN Rank RMP ON RMP.RankId = JH.P_DoneBy   " +
                                 "LEFT JOIN Rank RMC ON RMC.RankId = JH.C_DoneBy   " +
                                 "WHERE JH.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND JH.ComponentId =( SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "')    " +
                                 "UNION ALL   " +
                                 "SELECT -1 as JobId,VesselCode,DefectNo,'DEFECT',REPLACE(CONVERT(VARCHAR(12),COMPLETIONDt,106),' ','-'),'','','','','',REPLACE(CONVERT(VARCHAR(12),TargetDt,106),' ','-'),'',DefectDetails,'','DFR','Defect Report' " +
                                 "FROM Vsl_DefectDetailsMaster   " +
                                 "WHERE VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND ComponentId =( SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "') AND HistoryId = 0   " +
                                 "UNION ALL " +
                                 "SELECT -2 as JobId,vesselcode,convert(varchar,UPId),'UNPLANNED',REPLACE(CONVERT(VARCHAR(12),DONEDATE,106),' ','-'),'','',RANKCODE,DONEBY_CODE,DONEBY_NAME,REPLACE(CONVERT(VARCHAR(12),DUEDATE,106),' ','-'),0,'',CONDITIONAFTER,'UPJ','Un-Planned Job' FROM dbo.VSL_UnPlannedJobs " +
                                 "INNER JOIN Rank R ON R.RANKID=ASSIGNEDTO  " +
                                 "WHERE VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND ComponentId =( SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "') " +
                                 ") AA  " +
                                 " ";


        string WhereClause = "";
        if (ddlJobType_H.SelectedIndex != 0)
        {
            WhereClause = " where AA.JobId=" + ddlJobType_H.SelectedValue;
        }
        strJobHistorySQL = strJobHistorySQL + WhereClause  + " ORDER BY CAST(ACTIONDATE AS DATETIME) DESC, JobCode ASC  ";

        DataTable dtJobHistory = Common.Execute_Procedures_Select_ByQuery(strJobHistorySQL);
        if (dtJobHistory.Rows.Count > 0)
        {
            rptJobHistory.DataSource = dtJobHistory;
            rptJobHistory.DataBind();
        }
        else
        {
            rptJobHistory.DataSource = null;
            rptJobHistory.DataBind();
        }

    }
    #endregion

    //#region ---------------- Assign Running Hour ---------------------
    //public void ShowRunningHourDetails()
    //{

    //    //lblRhComponent.Text = hfComponent.Value.Trim().Replace(":", "-");

    //    //string strCompId = "SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + hfComponent.Value.Split(':').GetValue(0).ToString().Trim() + "' ";
    //    //DataTable dtCode = Common.Execute_Procedures_Select_ByQuery(strCompId);
    //    //string CompId = dtCode.Rows[0]["ComponentId"].ToString();
    //    //hfRhCompId.Value = CompId;
    //    string strRunningHourSQL = "SELECT CM.ComponentId ,CM.ComponentCode + ' - ' +CM.ComponentName AS ComponentCode,StartDate=(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(StartDate,''),106),' ','-') AS StartDate FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ORDER BY UpdatedOn DESC ), " +
    //                               "StartupHour=(SELECT TOP 1 StartupHour FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ORDER BY UpdatedOn DESC ), " +
    //                               "AvgRunningHrPerDay=(SELECT TOP 1 AvgRunningHrPerDay FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ORDER BY UpdatedOn DESC ), " +
    //                               "(select UserName  from ShipUserMaster where UserId IN (SELECT TOP 1 UpdatedBy FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ORDER BY UpdatedOn DESC )) AS  UpdatedBy, " +
    //                               "(SELECT TOP 1 REPLACE(convert(varchar(15),ISNULL(UpdatedOn,''),106),' ','-')  FROM VSL_VesselRunningHourMaster VRH WHERE VRH.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ORDER BY UpdatedOn DESC )  AS UpdatedOn " +
    //                               "FROM ComponentMaster CM WHERE CM.ComponentId IN (SELECT DISTINCT ComponentId FROM VSL_VesselComponentJobMaster WHERE IntervalId = 1 AND VesselCode = '" + ddlVessels.SelectedValue.Trim() + "') ";                 
                                   
    //    DataTable dtRunningHour = Common.Execute_Procedures_Select_ByQuery(strRunningHourSQL);
    //    if (dtRunningHour.Rows.Count > 0)
    //    {
    //        rptRunningHour.DataSource = dtRunningHour;
    //        rptRunningHour.DataBind();
    //    }
    //    else
    //    {
    //        rptRunningHour.DataSource = null;
    //        rptRunningHour.DataBind();
    //        //msgRunHour.ShowMessage("Running hour details does not exists for selected component.", true);
    //    }
    //}
    //#endregion
    #endregion

    #region ---------------- Events -------------------------------------------

    #region ---------------- Commom ---------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (setScroll)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Componenttree');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvJobs');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvSpares');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('divShutdown');", true);
        }
    }
    protected void ddlVessels_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["VesselCode"] = ddlVessels.SelectedValue.ToString();

        if (ddlVessels.SelectedIndex != 0)
        {
            BindGroupsAndSystems();
            btnSearchComponents.Visible = true;
        }
        else
        {
            tvComponents.Nodes.Clear();
            btnSearchComponents.Visible = false;
        }
        btnSpecification_Click(sender, e);
        BindOnVesselChange();
    }
    protected void tvComponents_SelectedNodeChanged(object sender, EventArgs e)
    {
        //divPageMenu.Visible = true;
        //hfComponent.Value = tvComponents.SelectedNode.Text.Trim();
        //if (!plSpecs.Visible && !plJobs.Visible && !plSpare.Visible && !PlRunningHour.Visible)
        //{
        //    plSpecs.Visible = true;
        //}

        string SQL = "SELECT CM.CriticalEquip FROM VSL_ComponentMasterForVessel CMV " +
                     "INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID " +
                     "WHERE CMV.VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND CM.ComponentCode ='" + tvComponents.SelectedNode.Value.ToString().Trim() + "' AND CMV.Status='A'";
        DataTable dtCritical = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dtCritical.Rows[0][0].ToString() == "True")
        {
            if (!btnShutdownReq.Visible)
            {
                btnShutdownReq.Visible = true;
                plShutdown.Visible = true;
            }
        }
        else
        {
            btnShutdownReq.Visible = false;
            plShutdown.Visible = false;

            if (!plSpecs.Visible && !plJobs.Visible && !plSpare.Visible && !PlHistory.Visible)
            {
                plSpecs.Visible = true;
            }
        }
        
        if (plSpecs.Visible)
        {
            lblSpecCompCode.Text = tvComponents.SelectedNode.Text;
            btnSpecification_Click(sender, e);
        }
        if (plJobs.Visible)
        {
            lblCompCodeJobs.Text = tvComponents.SelectedNode.Text;
            BindComponentJobs();
        }
        if (plSpare.Visible)
        {
            lblCompCodeSpare.Text = tvComponents.SelectedNode.Text;
            ShowComponentSpare(0);
        }
        if (PlHistory.Visible)
        {
            ShowHistory();
        }
        if (plShutdown.Visible)
        {
            BindShutdownRecords();
        }

        

    }
    protected void tvComponents_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        DataTable dtSubSystems;
        DataTable dtUnits;
        string strUnits = "";
        string strSubSystems = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))=" + (e.Node.Value.Trim().Length + 3) + " AND LEFT(CM.ComponentCode," + e.Node.Value.Trim().Length + ")='" + e.Node.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' AND CMV.Status='A' Order By ComponentCode";
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
                    strUnits = "SELECT CM.ComponentCode, CM.ComponentName FROM VSL_ComponentMasterForVessel CMV INNER JOIN COMPONENTMASTER CM ON CM.COMPONENTID = CMV.COMPONENTID where LEN(LTRIM(RTRIM(CM.ComponentCode)))= 12 AND LEFT(CM.ComponentCode, 9 )='" + ssn.Value.Trim() + "' AND CMV.VesselCode ='" + ddlVessels.SelectedValue.Trim() + "' AND CMV.Status='A' Order By ComponentCode";
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
            valuepath = "/" + hfSearchCode.Value.ToString().Trim() + "         ";
        }
        if (hfSearchCode.Value.Trim().Length == 6)
        {
            Group = hfSearchCode.Value.Trim().Substring(0, 3);
            valuepath = "/" + Group + "         /" + hfSearchCode.Value.Trim().ToString() + "      ";
        }
        if (hfSearchCode.Value.Trim().Length == 9)
        {
            Group = hfSearchCode.Value.Trim().Substring(0, 3);
            system = hfSearchCode.Value.Trim().Substring(0, 6);
            foreach (TreeNode tnc in tvComponents.Nodes)
            {
                if (tnc.ChildNodes.Count > 0)
                {
                    foreach (TreeNode tnch in tnc.ChildNodes)
                    {
                        if (tnch.Value.Trim() == Group) // Group 01
                        {
                            if (tnch.ChildNodes.Count > 0)
                            {
                                foreach (TreeNode tnchch in tnch.ChildNodes) // System 0101
                                {
                                    if (tnchch.Value.Trim() == system)
                                    {
                                        tnchch.Expand();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            valuepath = "/" + Group + "         /" + system + "      /" + hfSearchCode.Value.ToString().Trim() + "   ";
        }
        if (hfSearchCode.Value.Trim().Length == 12)
        {
            Group = hfSearchCode.Value.Trim().Substring(0, 3);
            system = hfSearchCode.Value.Trim().Substring(0, 6);
            subsystem = hfSearchCode.Value.Trim().Substring(0, 9);
            TreeNode tng = new TreeNode();
            tng.Value = hfSearchCode.Value.Trim();
            foreach (TreeNode tnc in tvComponents.Nodes) // root
            {
                if (tnc.ChildNodes.Count > 0)
                {
                    foreach (TreeNode tnGroup in tnc.ChildNodes)
                    {
                        if (tnGroup.ChildNodes.Count > 0) // group 01
                        {
                            if (tnGroup.Value.Trim() == Group)
                            {
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

                    }
                }

            }
            valuepath = "/" + Group + "         /" + system + "      /" + subsystem + "   /" + hfSearchCode.Value.ToString();
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
        if (tn.ToolTip != "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "fds", "setFocus('" + tn.ToolTip + "');", true);
        }
    }
    protected void btnSearchComponents_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex != 0)
        {
            Session["VesselCode"] = ddlVessels.SelectedValue.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opensearchwindow();", true);
        }
        else
        {
            MessageBox1.ShowMessage("Please select Vessel.", true);
        }
    }
    protected void btnSpecification_Click(object sender, EventArgs e)
    {
        plSpecs.Visible = true;
        plJobs.Visible = false;
        plSpare.Visible = false;
        PlHistory.Visible = false;
        plShutdown.Visible = false;
        tdComponentTree.Visible = true;
        btnSpecification.CssClass = "selbtn";
        btnAssignJobs.CssClass = "btn1";
        btnAssignSpare.CssClass = "btn1";
        btnJobHistory.CssClass = "btn1";
        btnShutdownReq.CssClass = "btn1";
        if (tvComponents.SelectedNode != null)
        {
            btnRefresh_Click(sender, e);
        }
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1041, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            imgbtneditSpec.Visible = Auth.IsUpdate;
            btnAddComponents.Visible = Auth.IsAdd;
            btnPring.Visible = Auth.IsPrint;
        }
        else
        {
            imgbtneditSpec.Visible = false;
            btnAddComponents.Visible = false;
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
            BindComponentJobs();
        }
        if (plSpare.Visible)
        {
            lblCompCodeSpare.Text = tvComponents.SelectedNode.Text;
            ShowComponentSpare(0);
        }
        if (PlHistory.Visible)
        {
            ShowHistory();
        }
        if (plShutdown.Visible)
        {
            BindShutdownRecords();
        }
        //BindGroupsAndSystems();
    }
    protected void btnPrintCompList_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel.", true);
            ddlVessels.Focus();
            return;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "report", "opencompreport('Ship','" + ddlVessels.SelectedValue.Trim() + "');", true);
    }
    protected void btnPrintJobs_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel.", true);
            ddlVessels.Focus();
            return;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "report", "opencompjobreport('Ship','" + ddlVessels.SelectedValue.Trim() + "');", true);
    }
    protected void btnAssignJobs_Click(object sender, EventArgs e)
    {
        plSpecs.Visible = false;
        plJobs.Visible = true;
        plSpare.Visible = false;
        PlHistory.Visible = false;
        plShutdown.Visible = false;
        tdComponentTree.Visible = true;
        btnSpecification.CssClass = "btn1";
        btnAssignJobs.CssClass = "selbtn";
        btnAssignSpare.CssClass = "btn1";
        btnJobHistory.CssClass = "btn1";
        btnShutdownReq.CssClass = "btn1";
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1041, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            btnAddJobs.Visible = Auth.IsAdd;
            imgbtnEditJobs.Visible = Auth.IsUpdate;
            btnPrintJobsReport.Visible = Auth.IsPrint;
            //btnPrintJobs.Visible = Auth.IsPrint;
            //btnAddJobs.Visible = true;
            //imgbtnEditJobs.Visible = true;
        }
        else
        {
            btnAddJobs.Visible = false;
            imgbtnEditJobs.Visible = false;
        }
        if (tvComponents.SelectedNode != null)
        {
            btnRefresh_Click(sender, e);
        }
    }
    protected void btnAssignSpare_Click(object sender, EventArgs e)
    {
        PageSize = 100; 
        plSpecs.Visible = false;
        plJobs.Visible = false;
        plSpare.Visible = true;
        PlHistory.Visible = false;
        plShutdown.Visible = false;
        tdComponentTree.Visible = true;
        btnSpecification.CssClass = "btn1";
        btnAssignSpare.CssClass = "selbtn";
        btnAssignJobs.CssClass = "btn1";
        btnJobHistory.CssClass = "btn1";
        btnShutdownReq.CssClass = "btn1";
        if (tvComponents.SelectedNode != null)
        {
            btnRefresh_Click(sender, e);
        }
    }
    protected void btnRefTv_Click(object sender, EventArgs e)
    {
        ddlVessels_SelectedIndexChanged(sender, e);
        //string code = hfSearchCode.Value;
        //btnSearchedCode_Click(sender, e);
    }
    protected void btnJobHistory_Click(object sender, EventArgs e)
    {
        plSpecs.Visible = false;
        plJobs.Visible = false;
        plSpare.Visible = false;
        PlHistory.Visible = true;
        plShutdown.Visible = false;
        tdComponentTree.Visible = true;
        btnSpecification.CssClass = "btn1";
        btnAssignSpare.CssClass = "btn1";
        btnAssignJobs.CssClass = "btn1";
        btnJobHistory.CssClass = "selbtn";
        btnShutdownReq.CssClass = "btn1";
        
        if (tvComponents.SelectedNode != null)
        {
            btnRefresh_Click(sender, e);
        }

    }
    protected void btnShutdownReq_Click(object sender, EventArgs e)
    {
        plSpecs.Visible = false;
        plJobs.Visible = false;
        plSpare.Visible = false;
        PlHistory.Visible = false;
        plShutdown.Visible = true;
        tdComponentTree.Visible = true;
        btnSpecification.CssClass = "btn1";
        btnAssignSpare.CssClass = "btn1";
        btnAssignJobs.CssClass = "btn1";
        btnJobHistory.CssClass = "btn1";
        btnShutdownReq.CssClass = "selbtn";

        if (tvComponents.SelectedNode != null)
        {
            btnRefresh_Click(sender, e);
        }

    }
    //protected void btnAssignRunningHour_Click(object sender, EventArgs e)
    //{
    //    plSpecs.Visible = false;
    //    plJobs.Visible = false;
    //    plSpare.Visible = false;
    //    //PlRunningHour.Visible = true;
    //    tdComponentTree.Visible = false;
    //    btnSpecification.CssClass = "btn1";
    //    btnAssignSpare.CssClass = "btn1";
    //    btnAssignJobs.CssClass = "btn1";
    //    //btnAssignRunningHour.CssClass = "selbtn";
    //    //btnRunHSave.Visible = (Session["UserType"].ToString() == "S");
    //    if (ddlVessels.SelectedIndex != 0)
    //    {
    //        //hfComponent.Value = tvComponents.SelectedNode.Text.Trim();
    //        btnRefresh_Click(sender, e);
    //    }
    //    else
    //    {
    //        btnAssignJobs_Click(sender, e);
    //        MessageBox1.ShowMessage("Please select a vessel first.", true);
    //    }
    //}
    //protected void btnAddStructure_Click(object sender, EventArgs e)
    //{
    //    if (btnAddStructure.Text == "Add Components")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openstructurewindow('" + ddlVessels.SelectedValue.ToString() + "','ADD');", true);
    //    }
    //    if (btnAddStructure.Text == "Modify Components")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openstructurewindow('" + ddlVessels.SelectedValue.ToString() + "','EDIT');", true);
    //    }
    //}
    //protected void btnRefTv_Click(object sender, EventArgs e)
    //{
    //    ddlVessels_SelectedIndexChanged(sender, e);
    //}
    #endregion
    #region --------------- Specification --------------------------
    protected void imgbtneditSpec_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel.", true);
            return;
        }
        if (tvComponents.SelectedNode == null)
        {
            MessageBox1.ShowMessage("Please select a component.", true);
            return;
        }
        btnSaveSpec.Visible = true;
        btnCancelSpec.Visible = true;

        lblMaker.Visible = false;
        lblMakerType.Visible = false;
        lblClassCode.Visible = false;
        lblComponentDesc.Visible = false;
        lblAccountCodes.Visible=false ;

        txtMaker.Visible = true;
        txtMakerType.Visible = true;
        chkClass.Checked = false; 
        chkClass.Enabled = true;
        txtClassCode.Visible = true;
        txtComponentDesc.Visible = true;
        txtAccountCodes.Visible=true ;
        chkOR.Enabled = true;
    }
    protected void btnCancelSpec_Click(object sender, EventArgs e)
    {
        BindSpecification();
        btnSaveSpec.Visible = false;
        btnCancelSpec.Visible = false;

        lblMaker.Visible = true;
        lblMakerType.Visible = true;
        lblClassCode.Visible = true;
        lblComponentDesc.Visible = true;
        lblAccountCodes.Visible = true;

        txtMaker.Visible = false;
        txtMakerType.Visible = false;
        txtClassCode.Visible = false;
        txtComponentDesc.Visible = false;
        txtAccountCodes.Visible = false;

    }
    protected void btnSaveSpec_Click(object sender, EventArgs e)
    {
        //if (txtMaker.Text != "" || txtMakerType.Text != "")
        //{

        string ClassEQCode = "";
        if (chkClass.Checked)
            ClassEQCode = txtClassCode.Text.Trim();

            try
            {
                Common.Set_Procedures("sp_Update_Ship_Specifications");
                Common.Set_ParameterLength(9);
                Common.Set_Parameters(
                    new MyParameter("@VesselCode", ddlVessels.SelectedValue.ToString().Trim()),
                    new MyParameter("@ComponentCode", tvComponents.SelectedNode.Value.Trim()),
                    new MyParameter("@Maker", txtMaker.Text.Trim()),
                    new MyParameter("@MakerType", txtMakerType.Text.Trim()),
                    new MyParameter("@ClassEquipCode", ClassEQCode),
                    new MyParameter("@Descr", txtComponentDesc.Text.Trim()),
                    new MyParameter("@CLASSEQUIP", (chkClass.Checked?"1":"0")),
                    new MyParameter("@ACCOUNTCODES", txtAccountCodes.Text.Trim()),
                    new MyParameter("@isOR", chkOR.Checked)
                    
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
                MessageBox1.ShowMessage("Unable to update component.Error :" + ex.Message + Common.getLastError(), true);
            }
        //}
    }
    protected void btnAddComponents_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel.", true);
            ddlVessels.Focus();
            return;
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "openaddcomponentwindow('" + ddlVessels.SelectedValue.Trim() + "','" + tvComponents.SelectedValue.Trim() + "');", true);

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
                    MessageBox1.ShowMessage("Please select a Vessel.", true);
                    return;
                }
                if (tvComponents.SelectedNode.Value.ToString() == "" || tvComponents.SelectedNode.Value.ToString().Trim().Length == 3)
                {
                    MessageBox1.ShowMessage("Please select a component.", true);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openjobwindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "');", true);
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
    //    Boolean Count = false;
    //    if (tvComponents.SelectedNode == null)
    //    {
    //        MessageBox1.ShowMessage("Please select a component.", true);
    //        return;
    //    }
    //    if (tvComponents.SelectedNode.Value.ToString() == "" || tvComponents.SelectedNode.Value.ToString().Trim().Length == 2)
    //    {
    //        MessageBox1.ShowMessage("Please select a component.", true);
    //        return;
    //    }
    //    foreach (RepeaterItem rptItem in rptVesselComponents.Items)
    //    {
    //        CheckBox chkSelectJobs = (CheckBox)rptItem.FindControl("chkSelectJobs");
    //        if (chkSelectJobs.Checked)
    //        {
    //            Count = true;
    //        }
    //    }
    //    if (Count == false)
    //    {
    //        MessageBox1.ShowMessage("Please select a job to delete.", true);
    //        return;
    //    }
    //    else
    //    {
    //        foreach (RepeaterItem rptItem in rptVesselComponents.Items)
    //        {
    //            CheckBox chkSelectJobs = (CheckBox)rptItem.FindControl("chkSelectJobs");
    //            HiddenField hfVcId = (HiddenField)rptItem.FindControl("hfVcId");
    //            HiddenField hfdJobId = (HiddenField)rptItem.FindControl("hfdJobId");
    //            if (chkSelectJobs.Checked)
    //            {
    //                string strSQL = "DELETE FROM VesselComponentJobMaster WHERE VesselCode='" + ddlVessels.SelectedValue.ToString().Trim() + "' AND VesselComponentId = " + hfVcId.Value + " AND JobId=" + hfdJobId.Value + " ; SELECT -1 ";
    //                DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //                if (dt.Rows.Count > 0)
    //                {
    //                    MessageBox1.ShowMessage("Job deleted successfully.", false);
    //                }
    //                else
    //                {
    //                    MessageBox1.ShowMessage("Unable to delete job", true);
    //                    chkSelectJobs.Focus();
    //                    return;
    //                }
    //            }
    //        }
    //        BindVesselComponents();
    //    }
    //}
    #endregion

    #region ---------------- Assign Spares ---------------------------
    //protected void lbtnSelect_Click(object sender, EventArgs e)
    //{
    //    string compId = "";
    //    DataTable dt = new DataTable();
    //    string strCompId = "SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "'";
    //    dt = Common.Execute_Procedures_Select_ByQuery(strCompId);
    //    compId = dt.Rows[0]["ComponentId"].ToString();
    //    int SpareId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    string strSQL = "DELETE FROM VSL_VesselSpareMaster WHERE VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND ComponentId = " + compId + " AND SpareId =" + SpareId + " ";
    //    Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    ShowComponentSpare();
    //}
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
        HiddenField hfOffice_Ship = (HiddenField)((ImageButton)sender).FindControl("hfOffice_Ship");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "','" + SpareId + "','" + hfOffice_Ship.Value.Trim() + "');", true);
    }

    protected void btnEditStock_Click(object sender, EventArgs e)
    {
        int SpareId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        HiddenField hfOffice_Ship = (HiddenField)((ImageButton)sender).FindControl("hfOffice_Ship");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow_stock('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "','" + SpareId + "','" + hfOffice_Ship.Value.Trim() + "');", true);
    }
    #endregion -----------------------------------------------------

    //#region ----------------- Assign Running Hour -------------------
    //protected void btnRunHSave_Click(object sender, EventArgs e)
    //{
    //    foreach (RepeaterItem rptItem in rptRunningHour.Items)
    //    {
    //        CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");

    //        if (chkSelect.Checked)
    //        {
    //            TextBox txtStartupHour = (TextBox)rptItem.FindControl("txtStartupHour");
    //            TextBox txtStartDate = (TextBox)rptItem.FindControl("txtStartDate");
    //            TextBox txtAvgRunHour = (TextBox)rptItem.FindControl("txtAvgRunHour");
    //            HiddenField hfRhCompId = (HiddenField)rptItem.FindControl("hfRhCompId");

    //            string str = "SELECT TOP 1 *,UpdatedOn AS Updated1 FROM VSL_VesselRunningHourMaster WHERE VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' AND ComponentId = " + hfRhCompId.Value.Trim() + "  ORDER BY StartDate,Updated1 Desc";
    //            DataTable dtRH = Common.Execute_Procedures_Select_ByQuery(str);
    //            if (txtStartupHour.Text.Trim() == "")
    //            {
    //                msgRunHour.ShowMessage("Please enter new hr.", true);
    //                txtStartupHour.Focus();
    //                return;
    //            }
    //            if (dtRH.Rows.Count > 0)
    //            {
    //                if (int.Parse(txtStartupHour.Text.Trim()) < int.Parse(dtRH.Rows[0]["StartupHour"].ToString()))
    //                {
    //                    msgRunHour.ShowMessage("New Hr. can not be less than last done hour.", true);
    //                    txtStartupHour.Focus();
    //                    return;
    //                }
    //            }
    //            if (txtStartDate.Text.Trim() == "")
    //            {
    //                msgRunHour.ShowMessage("Please enter changed dt.", true);
    //                txtStartDate.Focus();
    //                return;
    //            }
    //            DateTime dt;
    //            if (!DateTime.TryParse(txtStartDate.Text.Trim(), out dt))
    //            {
    //                msgRunHour.ShowMessage("Please enter valid date.", true);
    //                txtStartDate.Focus();
    //                return;
    //            }
    //            if (DateTime.Parse(txtStartDate.Text.Trim()) > DateTime.Today.Date)
    //            {
    //                msgRunHour.ShowMessage("Changed dt. can not be more than today.", true);
    //                txtStartDate.Focus();
    //                return;
    //            }
    //            if (dtRH.Rows.Count > 0)
    //            {
    //                if (DateTime.Parse(txtStartDate.Text.Trim()) < DateTime.Parse(dtRH.Rows[0]["StartDate"].ToString()))
    //                {
    //                    msgRunHour.ShowMessage("Changed dt. can not be less than last changed dt.", true);
    //                    txtStartDate.Focus();
    //                    return;
    //                }
    //            }
    //            if (txtAvgRunHour.Text.Trim() == "")
    //            {
    //                msgRunHour.ShowMessage("Please enter average hr.", true);
    //                txtAvgRunHour.Focus();
    //                return;
    //            }
    //            if (int.Parse(txtAvgRunHour.Text.Trim()) > 25)
    //            {
    //                msgRunHour.ShowMessage("Average hr. can not be more than 25.", true);
    //                txtAvgRunHour.Focus();
    //                return;
    //            }
    //        }
    //    }

    //    foreach (RepeaterItem Item in rptRunningHour.Items)
    //    {
    //        CheckBox chkSelect = (CheckBox)Item.FindControl("chkSelect");
    //        if (chkSelect.Checked)
    //        {
    //            TextBox txtStartupHour = (TextBox)Item.FindControl("txtStartupHour");
    //            TextBox txtStartDate = (TextBox)Item.FindControl("txtStartDate");
    //            TextBox txtAvgRunHour = (TextBox)Item.FindControl("txtAvgRunHour");
    //            HiddenField hfRhCompId = (HiddenField)Item.FindControl("hfRhCompId");

    //            Common.Set_Procedures("sp_Ship_InsertRunningHour");
    //            Common.Set_ParameterLength(6);
    //            Common.Set_Parameters(
    //                new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim()),
    //                new MyParameter("@ComponentId", Common.CastAsInt32(hfRhCompId.Value)),
    //                new MyParameter("@StartupHour", Common.CastAsInt32(txtStartupHour.Text.Trim())),
    //                new MyParameter("@AvgRunningHrPerDay", Common.CastAsInt32(txtAvgRunHour.Text.Trim())),
    //                new MyParameter("@StartDate", txtStartDate.Text.Trim()),
    //                new MyParameter("@UpdatedBy", Session["loginid"].ToString())
    //                );

    //            DataSet dsComponents = new DataSet();
    //            dsComponents.Clear();
    //            Boolean res;
    //            res = Common.Execute_Procedures_IUD(dsComponents);
    //            if (res)
    //            {
    //                msgRunHour.ShowMessage("Running Hour Details Added Successfully.", false);
    //            }
    //            else
    //            {
    //                msgRunHour.ShowMessage("Unable to Add Running Hour Details.", true);
    //            }
    //        }
    //    }
    //    ShowRunningHourDetails();
    //}
    //#endregion

    #region -------------- CREATE DB ------------------------

    protected void btnCreateDB_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "load", "alert('Please select vessel.');", true);
            return;
        }
        try
        {
            Common.Set_Procedures("sp_VesselDataScript");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(new MyParameter("@VSL", ddlVessels.SelectedValue),
                new MyParameter("@ExportedBy", Session["loginid"].ToString())
                );
            DataSet ds = Common.Execute_Procedures_Select();

            string MainSchema = Server.MapPath("~/Modules/PMS/SetUp/MAIN_SCHEMA.sql");
            string DataFile = Server.MapPath("~/Modules/PMS/SetUp/DATA_" + ddlVessels.SelectedValue + ".xml");
            string SchemaFile = Server.MapPath("~/Modules/PMS/SetUp/SCHEMA_" + ddlVessels.SelectedValue + ".xml");
            if (ds.Tables.Count == 44)
            {
                ds.Tables[0].TableName = "DeptMaster";
                ds.Tables[1].TableName = "Rank";
                ds.Tables[2].TableName = "Settings";
                ds.Tables[3].TableName = "ShipUserMaster";

                ds.Tables[4].TableName = "JobMaster";
                ds.Tables[5].TableName = "JobIntervalMaster";

                ds.Tables[6].TableName = "ComponentMaster";
                ds.Tables[7].TableName = "ComponentsJobMapping";
                ds.Tables[8].TableName = "ComponentsJobMapping_Attachments";

                ds.Tables[9].TableName = "VSL_ComponentMasterForVessel";
                ds.Tables[10].TableName = "VSL_VesselComponentJobMaster";
                ds.Tables[11].TableName = "VSL_VesselComponentJobMaster_Updates";
                ds.Tables[12].TableName = "VSL_VesselComponentJobMaster_Attachments";

                ds.Tables[13].TableName = "VSL_VesselRunningHourMaster";
                ds.Tables[14].TableName = "VSL_VesselSpareMaster";
                ds.Tables[15].TableName = "VSL_PlanMaster";

                ds.Tables[16].TableName = "VSL_VesselJobUpdateHistory";
                ds.Tables[17].TableName = "VSL_DefectDetailsMaster";
                ds.Tables[18].TableName = "VSL_DefectSpareDetails";

                ds.Tables[19].TableName = "VSL_VesselJobUpdateHistoryAttachments";
                ds.Tables[20].TableName = "VSL_VesselJobUpdateHistorySpareDetails";

                ds.Tables[21].TableName = "VSL_StockInventory";

                ds.Tables[22].TableName = "VSL_UnPlannedJobs";
                ds.Tables[23].TableName = "VSL_UnPlannedJobs_Attachments";
                ds.Tables[24].TableName = "VSL_UnPlannedJobs_SpareDetails";

                ds.Tables[25].TableName = "VSL_CriticalEquipShutdownRequest";
                ds.Tables[26].TableName = "VSL_CriticalShutdownExtensions";
                ds.Tables[27].TableName = "VSL_CriticalShutdownApproval";
                ds.Tables[28].TableName = "VSL_CriticalShutdownExtensionApproval";
                ds.Tables[29].TableName = "VSL_DefectRemarks";
                ds.Tables[30].TableName = "VSL_Defects_Attachments";

                ds.Tables[31].TableName = "IE_PacketCreated";

                //ds.Tables[32].TableName = "SMS_Forms";
                //ds.Tables[33].TableName = "SMS_APP_ManualMaster";
                //ds.Tables[34].TableName = "SMS_APP_ManualDetails";
                //ds.Tables[35].TableName = "SMS_APP_ManualDetailsForms";
                //ds.Tables[36].TableName = "SMS_APP_COM_ManualDetails";

                //ds.Tables[37].TableName = "VSL_SMS_PacketSent";

                ds.Tables[32].TableName = "VSL_SMS_PacketSent";
                ds.Tables[33].TableName = "Vessel";
                ds.Tables[34].TableName = "SpareStockLocation";
                ds.Tables[35].TableName = "MP_Products";
                ds.Tables[36].TableName = "MP_Products_Packaging";
                ds.Tables[37].TableName = "ER_Registers";
                ds.Tables[38].TableName = "ER_RegisterOptions";
                ds.Tables[39].TableName = "sql_tblSMDPRAccounts";
                ds.Tables[40].TableName = "Cir_Category";
                ds.Tables[41].TableName = "SMS_FormDepartment";
                ds.Tables[42].TableName = "SMS_FormsCategoryMaster";
                ds.Tables[43].TableName = "RA_ConsequencesMaster";
                //ds.Tables[35].TableName = "Cir_Category";
                //ds.Tables[36].TableName = "RH_NCType";
                ds.WriteXmlSchema(SchemaFile);
                ds.WriteXml(DataFile);
                string WatchKeeperMainSchema = "";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(select FlagStateName from FlagState where FlagStateID=V.FlagStateID)FlagStateName FROM VESSEL V where VesselCode ='" + ddlVessels.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    DataRow Dr = dt.Rows[0];
                    String VesselCode = Dr["VesselCode"].ToString();
                    String VesselName = Dr["VesselName"].ToString();
                    String LrimoNumber = Dr["LrimoNumber"].ToString();
                    String FlagStateName = Dr["FlagStateName"].ToString();
                    string Data = File.ReadAllText(Server.MapPath("~/Modules/PMS/SetUp/eShip-WatchKeeper.sql"));
                    Data = Data.Replace("#VCODE#", VesselCode);
                    Data = Data.Replace("#VNAME#", VesselName);
                    Data = Data.Replace("#LRIMO#", LrimoNumber);
                    Data = Data.Replace("#FLAG#", FlagStateName);
                    File.WriteAllText(Server.MapPath("~/Modules/PMS/SetUp/eShip-WatchKeeper_" + VesselCode + ".sql"), Data);
                    //ScriptManager.RegisterStartupScript(this,this.GetType(),"a","window.open('MTM-WatchKeeper_" + VesselCode + ".sql');",true);

                    WatchKeeperMainSchema = Server.MapPath("~/Modules/PMS/SetUp/eShip-WatchKeeper_" + VesselCode + ".sql");


                }

                string ZipData = Server.MapPath("~/Modules/PMS/SetUp/" + ddlVessels.SelectedValue + ".zip");
                if (File.Exists(ZipData)) { File.Delete(ZipData); }

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(MainSchema);
                    zip.AddFile(SchemaFile);
                    zip.AddFile(DataFile);
                    if (WatchKeeperMainSchema != "")
                    {
                        zip.AddFile(WatchKeeperMainSchema);
                    }     
                    zip.Save(ZipData);
                    Response.Clear();
                    Response.ContentType = "application/zip";
                    Response.AddHeader("Content-Type", "application/zip");
                    Response.AddHeader("Content-Disposition", "inline;filename=" + ddlVessels.SelectedValue + ".zip");
                    Response.WriteFile(ZipData);
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable To Create DB. Error : " + ex + "." + Common.getLastError() , true);
        }
    }
    #endregion


    // Code By Umakant
    protected void btnPring_Click(object sener, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel.", true);
            ddlVessels.Focus();
            return;
        }
        if (tvComponents.SelectedValue == "")
        {
            MessageBox1.ShowMessage("Please select a component.", true);
            tvComponents.Focus();
            return;
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('Reports/ShipMasterSpecification.aspx');", true);
    }

    protected void btnPrintJobsReport_Click(object sener, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel.", true);
            ddlVessels.Focus();
            return;
        }
        if (tvComponents.SelectedValue == "")
        {
            MessageBox1.ShowMessage("Please select a component.", true);
            tvComponents.Focus();
            return;
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('Reports/PrintCrystal.aspx?ReportType=ShipMasterJobs&ComponentName=" + lblCompCodeJobs .Text.Trim()+ "');", true);
    }

    protected void btnSparePrint_Click(object sener, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel.", true);
            ddlVessels.Focus();
            return;
        }
        if (tvComponents.SelectedValue == "")
        {
            MessageBox1.ShowMessage("Please select a component.", true);
            tvComponents.Focus();
            return;
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('Reports/PrintCrystal.aspx?ReportType=Spare&ComponentName=" + lblCompCodeSpare.Text + "');", true);        
    }

    protected void btnPrintHistory_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel.", true);
            ddlVessels.Focus();
            return;
        }
        if (tvComponents.SelectedValue == "")
        {
            MessageBox1.ShowMessage("Please select a component.", true);
            tvComponents.Focus();
            return;
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "JH", "window.open('Reports/JobHistory.aspx?VC=" + ddlVessels.SelectedValue.Trim() + "&CompCode=" + tvComponents.SelectedValue.Trim() + "');", true);
    }

    protected void btnAddShutdownReq_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a vessel.", true);
            ddlVessels.Focus();
            return;
        }
        if (tvComponents.SelectedValue == "")
        {
            MessageBox1.ShowMessage("Please select a component.", true);
            tvComponents.Focus();
            return;
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "SD", "openShutdownwindow('" + tvComponents.SelectedValue.Trim() + "','" + ddlVessels.SelectedValue.Trim() + "','0');", true);
        
    }
    public void BindShutdownRecords()
    {
        string strSQL = "SELECT SR.ShutdownId,SR.VesselCode,'" + tvComponents.SelectedValue.Trim() + "' AS CompCode,ComponentId,DefectNo,FormNo,[Version],REPLACE(CONVERT(varchar(11), RequestDate,106),' ','-') AS RequestDate,MasterCEName,Job_Defect,Pl_ShutDownTotalHrs,Pl_FromDateTime, Pl_ToDateTime, " +
                        "REPLACE(CONVERT(varchar(11), Ma_CommencedDateTime,106),' ','-') AS Ma_CommencedDateTime,REPLACE(CONVERT(varchar(11), Ma_CompletedDateTime,106),' ','-') AS Ma_CompletedDateTime,Maintenence_Remarks,IssuedBy,REPLACE(CONVERT(varchar(11), IssueDate,106),' ','-') AS IssueDate,CASE WHEN SA.Approver_Name IS NULL THEN 'NO' ELSE 'Yes' END AS Approved " +
                        "FROM VSL_CriticalEquipShutdownRequest SR LEFT JOIN VSL_CriticalShutdownApproval SA ON SA.ShutdownId = SR.ShutdownId  AND SA.VESSELCODE = SR.VESSELCODE WHERE SR.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND ComponentId = (SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + tvComponents.SelectedNode.Value.ToString().Trim() + "') ";
        DataTable dtShutdown = Common.Execute_Procedures_Select_ByQuery(strSQL);

        rptShutdown.DataSource = dtShutdown;
        rptShutdown.DataBind();
    }



    #endregion

    protected void ddlJobType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindComponentJobs();
    }

    protected void btnCopySpareLink_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfOffice_Ship = (HiddenField)btn.Parent.FindControl("hfOffice_Ship");
        HiddenField hfCompID = (HiddenField)btn.Parent.FindControl("hfCompID");
        
        cp_SpareID = Common.CastAsInt32(btn.CommandArgument);
        cp_CompId = Common.CastAsInt32(hfCompID.Value);
        cp_OfficeShip = hfOffice_Ship.Value;
        

        dvMoveSpare.Visible = true;

        string Code = tvComponents.SelectedNode.Value.ToString().Trim();
        string sql = " Select ComponentID,ComponentCode +' - '+ComponentName as ComponentName from ComponentMaster where ComponentCode =LEFT('"+Code+ "',len(ComponentCode)) and ComponentCode<>'" + Code + "'  order by ComponentCode ";
        DataTable dtSpareDetails = Common.Execute_Procedures_Select_ByQuery(sql);

        ddlComponent.DataSource = dtSpareDetails;
        ddlComponent.DataTextField = "ComponentName";
        ddlComponent.DataValueField= "ComponentID";
        ddlComponent.DataBind();
        ddlComponent.Items.Insert(0,new ListItem("Select", ""));
    }

    
    protected void btnCloseMoveSpare_Click(object sender, EventArgs e)
    {
        dvMoveSpare.Visible = false;
    }
    
    protected void btnMoveSpare_OnClick(object sender, EventArgs e)
    {
        
        try
        {
            if (ddlComponent.SelectedIndex == 0)
            {
                MessageBox2.ShowMessage("Please select component.", true);
                return;
            }
            Common.Set_Procedures("sp_Ship_MoveSpare");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim()),
                new MyParameter("@Old_ComponentId", cp_CompId),
                new MyParameter("@Old_Office_Ship", cp_OfficeShip),
                new MyParameter("@Old_SpareId", cp_SpareID),
		new MyParameter("@New_Office_Ship", Session["UserType"].ToString()),
                new MyParameter("@ComponentId", ddlComponent.SelectedValue)
                );

            DataSet dsActive = new DataSet();
            dsActive.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsActive);

            if (res)
            {
                MessageBox2.ShowMessage("Spare moved successfully.", false);
                ShowComponentSpare(0);
                //btnActive.Visible = true;
                //btnInactive.Visible = false;
            }
	else
	{

		MessageBox2.ShowMessage("Unable to move spare." + Session["CurrentShip"].ToString() + ddlComponent.SelectedValue.ToString(), false);
	}


        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to inactive spare.Error :" + ex.Message + Common.getLastError(), true);
        }
    }
    protected void ddlJobType_H_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ShowHistory();
    }
    protected void lnkOpenSuppliers_OnClick(object sender, EventArgs e)
    {
        string Code = tvComponents.SelectedNode.Value.Trim();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsaf", "window.open('ComponentSuppliers.aspx?vsl=" + ddlVessels.SelectedValue.Trim() + "&ccode=" + Code + "','');", true);
    }
}
