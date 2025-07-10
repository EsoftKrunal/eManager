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
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;
using System.IO;
using AjaxControlToolkit;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

public partial class RiskManagement_AddRisk : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------
    public string RiskStatus
    {
        set { ViewState["RiskStatus"] = value; }
        get { return Convert.ToString(ViewState["RiskStatus"]); }
    }
    public int RiskId
    {
        set { ViewState["RiskId"] = value; }
        get { return Common.CastAsInt32(ViewState["RiskId"]); }
    }
    public int EventId
    {
        set { ViewState["EventId"] = value; }
        get { return Common.CastAsInt32(ViewState["EventId"]); }
    }


    public int SRKey
    {
        set { ViewState["SRKey"] = value; }
        get { return Common.CastAsInt32(ViewState["SRKey"]); }
    }
    public int HazardId
    {
        set { ViewState["HazardId"] = value; }
        get { return Common.CastAsInt32(ViewState["HazardId"]); }
    }
    public DataTable HazardsList
    {
        set { ViewState["AditionalHazard"] = value; }
        get { return (DataTable)ViewState["AditionalHazard"]; }
    }

    public int TemplateId
    {
        set { ViewState["TemplateId"] = value; }
        get { return Common.CastAsInt32(ViewState["TemplateId"]); }
    }
    public DataTable dtTasksNew
    {
        get
        {
            object o = ViewState["dtTasksNew"];
            return (DataTable)o;
        }
        set
        {
            ViewState["dtTasksNew"] = value;
        }
    }

    public DataTable dtHazardsNew
    {
        get
        {
            object o = ViewState["dtHazardsNew"];
            return (DataTable)o;
        }
        set
        {
            ViewState["dtHazardsNew"] = value;
        }
    }

    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return Convert.ToString(ViewState["VesselCode"]); }
    }

    public string ApprovalNotRequiredMsg = "Office Approval Not Required";
    public string ApprovalRequiredMsg = "Office Approval Required";

    #endregion -----------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            TabPanel1.Enabled = true;
            TabPanel2.Enabled = false;
            TabPanel3.Enabled = false;
            RiskId = Common.CastAsInt32(Request.QueryString["RiskId"]);
           // EventId = Common.CastAsInt32(Request.QueryString["EventId"]);
            VesselCode = Request.QueryString["VesselCode"].ToString();

            DataTable dtVessel = Common.Execute_Procedures_Select_ByQuery("Select VesselName from Vessel  where VesselCode = '" + VesselCode + "'");
            if (dtVessel.Rows.Count > 0)
            {
                lblVesselName.Text = dtVessel.Rows[0][0].ToString();
            }
            DataTable dtRank = Common.Execute_Procedures_Select_ByQuery("Select RankId, LTRIM(RTRIM(RankCode)) As RankCode from Rank with(nolock) where StatusId = 'A'");
            if (dtRank.Rows.Count > 0)
            {
                ddlPosition.DataSource = dtRank;
                ddlPosition.DataTextField = "RankCode";
                ddlPosition.DataValueField = "RankId";
                ddlPosition.DataBind();
                ddlPosition.Items.Insert(0, new ListItem("< Select >", "0"));

                ddlVerifiedRank.DataSource = dtRank;
                ddlVerifiedRank.DataTextField = "RankCode";
                ddlVerifiedRank.DataValueField = "RankId";
                ddlVerifiedRank.DataBind();
                ddlVerifiedRank.Items.Insert(0, new ListItem("<Select>", ""));
            }
            DataTable dtPTWType = Common.Execute_Procedures_Select_ByQuery("Select * from PTWTypeMaster with(nolock) where PTWStatusId = 'A'");
            if (dtVessel.Rows.Count > 0)
            {
                ddlTypeoFPTW.DataSource = dtPTWType;
                ddlTypeoFPTW.DataTextField = "PTWType";
                ddlTypeoFPTW.DataValueField = "PTWId";
                ddlTypeoFPTW.DataBind();
                ddlTypeoFPTW.Items.Insert(0, new ListItem("< Select >", "0"));
            }
            if (RiskId > 0) // EDIT DATA
            {
                ShowRecord();
            }
            //else // NEW RECORD
            //{
            //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("select REPLACE(STR(ISNULL(MAX(CAST( RIGHT(REFNO,3) AS INT)),0) + 1,3),' ','0') from RA_VSL_RISKMGMT_MASTER WHERE YEAR(EVENTDATE)=" + DateTime.Today.Year.ToString());
            //    if (dt.Rows.Count > 0)
            //    {
            //        lblRefNo.Text = Session["CurrentShip"].ToString() + "-RA-" + DateTime.Today.Year.ToString() + "-" + dt.Rows[0][0].ToString();
            //    }

            //    CreateTables();
            //    ShowMasterDetails();
            //    RiskStatus = "O";
            //}
          //  ShowEventDetails();
        }
        //spnAdd.Visible = RiskStatus == "O";
    }
    public void ShowEventDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM EV_EventMaster WHERE EventId=" + EventId.ToString());
        if (dt.Rows.Count > 0)
        {
            lblEventName.Text = dt.Rows[0]["EventName"].ToString();
        }
    }
    public void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM RA_VSL_RISKMGMT_MASTER WHERE VESSELCODE='" + VesselCode + "' AND RISKID=" + RiskId.ToString());
        if (dt.Rows.Count > 0)
        {

            DataTable dtVessel = Common.Execute_Procedures_Select_ByQuery("Select VesselName from Vessel  where VesselCode = '" + dt.Rows[0]["VESSELCODE"].ToString() + "'");
            if (dtVessel.Rows.Count > 0)
            {
                lblVesselName.Text = dtVessel.Rows[0][0].ToString();
            }
            EventId = Common.CastAsInt32(dt.Rows[0]["EventId"]);
            ShowEventDetails();
            txtEventDate.Text = Common.ToDateString(dt.Rows[0]["EVENTDATE"]);
            lblRefNo.Text = dt.Rows[0]["REFNO"].ToString();
            txtGrographicLocation.Text = dt.Rows[0]["GeographLocation"].ToString();
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["PTWRequired"].ToString()))
            {
                ddlPTWRequired.SelectedValue = dt.Rows[0]["PTWRequired"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["PTWType"].ToString()))
            {
                ddlTypeoFPTW.SelectedValue = ddlTypeoFPTW.Items.FindByText(dt.Rows[0]["PTWType"].ToString()).Value;
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["PersonReqTask"].ToString()))
            {
                ddlPerRefTask.SelectedValue = ddlPerRefTask.Items.FindByText(dt.Rows[0]["PersonReqTask"].ToString()).Value;
            }

            txtNoofPerTask.Text = dt.Rows[0]["NoofPersonReqTask"].ToString();
            txtRiskDescr.Text = dt.Rows[0]["RiskDescr"].ToString();
            ddlAlt.SelectedValue = dt.Rows[0]["ALTERNATEMETHODS"].ToString();
            txtDetails.Text = dt.Rows[0]["Details"].ToString();
            txtDetails.Visible = (ddlAlt.SelectedValue.Trim() == "Y");
            txtHOD.Text = dt.Rows[0]["HOD_NAME"].ToString();
            txtSO.Text = dt.Rows[0]["SAF_OFF_NAME"].ToString();
            txtMaster.Text = dt.Rows[0]["MASTER_NAME"].ToString();
            //lblCommentsByOn.Text = "<b>" + dt.Rows[0]["OFFICECOMMENTBY"].ToString() + " </b> ( <i style='color:blue'>" + dt.Rows[0]["DESIGNATION"].ToString() + " </i> ) / " + Common.ToDateString(dt.Rows[0]["COMMENTDATE"]);
            txtOfficeCommnets.Text = dt.Rows[0]["OFFICE_COMMENTS"].ToString();
            lblApprovalAuthority.Text = "<b>" + dt.Rows[0]["OFFICECOMMENTBY"].ToString() + " </b> / " + Common.ToDateString(dt.Rows[0]["COMMENTDATE"]);
            txtCreatedBy.Text = dt.Rows[0]["CREATED_BY"].ToString();
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["POSITION"].ToString()))
            {
                ddlPosition.SelectedValue = ddlPosition.Items.FindByText(dt.Rows[0]["POSITION"].ToString()).Value;
            }
            // btnSave.Visible = btnExport.Visible = (dt.Rows[0]["Status"].ToString() == "O");
            btnSaveOfficeTab.Visible = btnFinalSubmission.Visible =  (dt.Rows[0]["Status"].ToString() == "O");
           // btnExport.Visible = true;
            btnPrint.Visible = true;
            btnSave.Visible = false;
            btnVerify.Visible = false;
            btnSaveSingle.Visible = false;
           
            lblVerifiedOn.Text = dt.Rows[0]["VerifiedBy"].ToString() + " ( " + dt.Rows[0]["VerifiedRank"].ToString() + " ) " + " / " + Common.ToDateString(dt.Rows[0]["VerifiedOn"].ToString());


            RiskStatus = dt.Rows[0]["Status"].ToString();

            dtTasksNew = Common.Execute_Procedures_Select_ByQuery("SELECT ROW_NUMBER() OVER (ORDER BY TABLEID) AS SRKey,* FROM dbo.RA_VSL_RISKMGMT_DETAILS D WHERE VESSELCODE='" + VesselCode + "' AND RISKID=" + RiskId.ToString());
            
            ShowMasterDetails();

            DataTable dtOffApproval = Common.Execute_Procedures_Select_ByQuery("Select Verify_Needed,OFFICE_COMMENTS, OFFICECOMMENTBY, COMMENTDATE from VW_RISKDATA Where VESSELCODE='" + VesselCode + "' AND RISKID=" + RiskId.ToString() + " AND  EVENTID = " + EventId.ToString());

            if (dtOffApproval.Rows.Count > 0)
            {
                if (dtOffApproval.Rows[0]["Verify_Needed"].ToString() == "Y")
                {
                    lblApprovalRequiredMsg.Text = ApprovalRequiredMsg;
                    trOfficeComments.Visible = true;
                    txtOfficeCommnets.Text = dtOffApproval.Rows[0]["OFFICE_COMMENTS"].ToString();
                    lblApprovalAuthority.Text = dt.Rows[0]["OFFICECOMMENTBY"].ToString() + " / " + Common.ToDateString(dt.Rows[0]["COMMENTDATE"].ToString());
                }
                else
                {
                    lblApprovalRequiredMsg.Text = ApprovalNotRequiredMsg;
                    trOfficeComments.Visible = false;
                    txtOfficeCommnets.Text = "";
                    lblApprovalAuthority.Text = "";
                   
                }
            }
           
        }
    }

    public void BindHazardsNew()
    {
        DataView dv = dtHazardsNew.DefaultView;
        dv.RowFilter = "Status='A' ";
        //dv.Sort = "HazardName";
        rptHazardsNew.DataSource = dv.ToTable();
        rptHazardsNew.DataBind();

        ShowExtResRisk();
    }

    public void ShowMasterDetails()
    {
        string TempCode = "";
        string sql1 = "Select * from EV_TemplateMaster WHERE EventId='" + EventId + "'";
        DataTable dtTempCode = Common.Execute_Procedures_Select_ByQuery(sql1);
        if (dtTempCode.Rows.Count > 0)
        {
            TempCode = dtTempCode.Rows[0]["TemplateCode"].ToString();
        }

        string SQL = "SELECT * FROM [dbo].[EV_TemplateMaster] WHERE EventId='" + EventId + "' AND Status = 'A'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            TemplateId = Common.CastAsInt32(dt.Rows[0]["TemplateId"]);
            lblTempCode.Text = dt.Rows[0]["TemplateCode"].ToString();
            EventId = Common.CastAsInt32(dt.Rows[0]["EventId"]);
            lblEventName.Text = dt.Rows[0]["EventName"].ToString();
            //lblCreatedByOn.Text = dt.Rows[0]["CreatedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["CreatedOn"]);
            //lblModifiedByOn.Text = dt.Rows[0]["ModifiedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["ModifiedOn"]);
            //lblApprovedByOn.Text = dt.Rows[0]["ApprovedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["ApprovedOn"]);
            if (RiskId > 0)
            {
                dtHazardsNew = Common.Execute_Procedures_Select_ByQuery("SELECT Distinct HAZARDID As Hazard_TableId,HAZARDNAME,  'A' As Status FROM [dbo].[RA_VSL_RISKMGMT_DETAILS] WHERE RiskId=" + RiskId);
                dtTasksNew = Common.Execute_Procedures_Select_ByQuery("SELECT RD.*, 'A' As Status ,(Select top 1 STATUS from RA_VSL_RISKMGMT_MASTER  RM with(nolock) where RM.RISKID =  RD.RiskId) As RAStatus FROM [dbo].[RA_VSL_RISKMGMT_DETAILS] RD WHERE RD.RiskId=" + RiskId);
            }
            else
            {
                dtHazardsNew = Common.Execute_Procedures_Select_ByQuery("SELECT Distinct HAZARDID As Hazard_TableId,HAZARDNAME, 'A' As Status FROM [dbo].[RA_Template_Hazards] WHERE TemplateId=" + TemplateId);
                dtTasksNew = Common.Execute_Procedures_Select_ByQuery("SELECT a.*, (Select b.HazardId from RA_Template_Hazards b where b.Hazard_TableId = a.Hazard_TableId) As HazardId, 'A' As Status, 'O' As RAStatus FROM [dbo].[RA_TemplateDetails] a WHERE a.TemplateId=" + TemplateId);
            }

            BindHazardsNew();

            if (dt.Rows[0]["OfficeApprovalRequired"].ToString() == "Y")
            {
                lblApprovalRequiredMsg.Text = "Office Approval Required";
                trOfficeComments.Visible = true;
                txtOfficeCommnets.Text = "";
                lblApprovalAuthority.Text = "";
            }
            else
            {
                lblApprovalRequiredMsg.Text = "Office Approval Not Required";
                trOfficeComments.Visible = false;
                txtOfficeCommnets.Text = "";
                lblApprovalAuthority.Text = "";
            }
            
            //btnSave.Visible = (Convert.IsDBNull(dt.Rows[0]["ApprovedOn"]));
            //btnApproveTemplate.Visible = (!Convert.IsDBNull(dt.Rows[0]["RequestedOn"]) && Convert.IsDBNull(dt.Rows[0]["ApprovedOn"]));
            //btnRequestApproval.Visible = !btnApproveTemplate.Visible;
        }
        else
        {
            //btnApproveTemplate.Visible = false;

            EventId = Common.CastAsInt32(Request.QueryString["EventId"]);
            if (EventId > 0)
            {
                SQL = "SELECT * FROM [dbo].[EV_EVENTMASTER] WHERE EventId=" + EventId;
                dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblTempCode.Text = "";
                    //lblEventName.Text = dt.Rows[0]["EventCode"].ToString() + " : " + dt.Rows[0]["EventName"].ToString();
                    lblEventName.Text = dt.Rows[0]["EventName"].ToString();
                }
            }
            //else if (TempCode != "")
            //{
            //    SQL = "SELECT * FROM [dbo].[EV_TemplateMaster] WHERE TemplateCode='" + TempCode + "' AND Status = 'A'";
            //    dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        lblTempCode.Text = TempCode;
            //        EventId = Common.CastAsInt32(dt.Rows[0]["EventId"]);
            //        lblEventName.Text = dt.Rows[0]["EventCode"].ToString() + " : " + dt.Rows[0]["EventName"].ToString();
            //        //---------------------------------
            //        int ActiveTemplateId = Common.CastAsInt32(dt.Rows[0]["TemplateId"]);
            //        DataTable _dtHazards = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[RA_Template_Hazards] WHERE TemplateId=" + ActiveTemplateId);
            //        DataTable _dtTasks = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[RA_TemplateDetails] WHERE TemplateId=" + ActiveTemplateId);
            //        AddHazardFromDB(_dtHazards, _dtTasks);
            //        BindHazardsNew();

            //        //---------------------------------
            //    }
            //}
            else
            {
                btnSave.Visible = false;
            }
        }
    }

    //public void AddHazardFromDB(DataTable _dtHazards, DataTable _dtTasks)
    //{
    //    foreach (DataRow drt in _dtHazards.Rows)
    //    {
    //        DataRow dr = dtHazardsNew.NewRow();
    //        int _Hazard_TableId = -(Common.CastAsInt32(dtHazardsNew.Rows.Count) + 1);
    //        dr["Hazard_TableId"] = _Hazard_TableId;
    //        dr["TemplateId"] = 0;
    //        dr["HazardId"] = drt["HazardId"].ToString();
    //        dr["HazardCode"] = drt["HazardCode"].ToString();
    //        dr["HazardName"] = drt["HazardName"].ToString();
    //        dr["Status"] = "A";

    //        dtHazardsNew.Rows.Add(dr);
    //        DataRow[] drtks = _dtTasks.Select("HazardId=" + drt["Hazard_TableId"].ToString());
    //        foreach (DataRow drt1 in drtks)
    //        {
    //            DataRow dr1 = dtTasksNew.NewRow();
    //            dr1["TableId"] = -(Common.CastAsInt32(dtTasksNew.Rows.Count) + 1);
    //            //dr1["TemplateId"] = 0;
    //            //dr1["Hazard_TableId"] = drt["Hazard_TableId"].ToString();
    //            dr1["HazardId"] = drt["HazardId"].ToString();
    //            dr1["HazardName"] = drt["HazardName"].ToString();
    //            dr1["ConsequencesId"] = drt1["ConsequencesId"];
    //            dr1["ConsequencesCode"] = drt1["ConsequencesCode"];
    //            dr1["ConsequencesName"] = drt1["ConsequencesName"];
    //            dr1["ControlMeasures"] = drt1["ControlMeasures"];

    //            dr1["SeverityPI"] = drt1["SeverityPI"];
    //            dr1["LikeliHoodPI"] = drt1["LikeliHoodPI"];
    //            dr1["RiskLevelPI"] = drt1["RiskLevelPI"];

    //            dr1["SeverityEI"] = drt1["SeverityEI"];
    //            dr1["LikeliHoodEI"] = drt1["LikeliHoodEI"];
    //            dr1["RiskLevelEI"] = drt1["RiskLevelEI"];

    //            dr1["SeverityAI"] = drt1["SeverityAI"];
    //            dr1["LikeliHoodAI"] = drt1["LikeliHoodAI"];
    //            dr1["RiskLevelAI"] = drt1["RiskLevelAI"];

    //            dr1["SeverityRI"] = drt1["SeverityRI"];
    //            dr1["LikeliHoodRI"] = drt1["LikeliHoodRI"];
    //            dr1["RiskLevelRI"] = drt1["RiskLevelRI"];

    //            dr1["ADD_CONTROL_MEASURES"] = drt1["ADD_CONTROL_MEASURES"];
    //            // dr1["GRVNo"] = drt1["GRVNo"];
    //            dr1["Re_SeverityPF"] = drt1["Re_SeverityPF"];
    //            dr1["Re_LikeliHoodPF"] = drt1["Re_LikeliHoodPF"];
    //            dr1["Re_RiskLevelPF"] = drt1["Re_RiskLevelPF"];

    //            dr1["Re_SeverityEF"] = drt1["Re_SeverityEF"];
    //            dr1["Re_LikeliHoodEF"] = drt1["Re_LikeliHoodEF"];
    //            dr1["Re_RiskLevelEF"] = drt1["Re_RiskLevelEF"];

    //            dr1["Re_SeverityAF"] = drt1["Re_SeverityAF"];
    //            dr1["Re_LikeliHoodAF"] = drt1["Re_LikeliHoodAF"];
    //            dr1["Re_RiskLevelAF"] = drt1["Re_RiskLevelAF"];

    //            dr1["Re_SeverityRF"] = drt1["Re_SeverityRF"];
    //            dr1["Re_LikeliHoodRF"] = drt1["Re_LikeliHoodRF"];
    //            dr1["Re_RiskLevelRF"] = drt1["Re_RiskLevelRF"];

    //            dr1["Status"] = "A";
    //            dtTasksNew.Rows.Add(dr1);
    //        }
    //    }
    //    dtTasksNew.AcceptChanges();
    //}
    public void CreateTables()
    {
        dtHazardsNew = new DataTable();

        dtHazardsNew.Columns.Add("Hazard_TableId", typeof(int));
        //dtHazardsNew.Columns.Add("TemplateId", typeof(int));
        //dtHazardsNew.Columns.Add("HazardId", typeof(int));
        //dtHazardsNew.Columns.Add("HazardCode", typeof(string));
        dtHazardsNew.Columns.Add("HazardName", typeof(string));
        dtHazardsNew.Columns.Add("Status", typeof(string));


        dtHazardsNew.AcceptChanges();

        dtTasksNew = new DataTable();

        dtTasksNew.Columns.Add("TableId", typeof(int));
        //dtTasksNew.Columns.Add("TemplateId", typeof(int));
        //dtTasksNew.Columns.Add("Hazard_TableId", typeof(int));
        dtTasksNew.Columns.Add("HazardId", typeof(int));
        dtTasksNew.Columns.Add("HazardName", typeof(string));
        dtTasksNew.Columns.Add("ConsequencesId", typeof(int));
        dtTasksNew.Columns.Add("ConsequencesCode", typeof(string));
        dtTasksNew.Columns.Add("ConsequencesName", typeof(string));
        dtTasksNew.Columns.Add("ControlMeasures", typeof(string));
        dtTasksNew.Columns.Add("SeverityPI", typeof(int));
        dtTasksNew.Columns.Add("LikeliHoodPI", typeof(int));
        dtTasksNew.Columns.Add("RiskLevelPI", typeof(int));
        dtTasksNew.Columns.Add("SeverityEI", typeof(int));
        dtTasksNew.Columns.Add("LikeliHoodEI", typeof(int));
        dtTasksNew.Columns.Add("RiskLevelEI", typeof(int));
        dtTasksNew.Columns.Add("SeverityAI", typeof(int));
        dtTasksNew.Columns.Add("LikeliHoodAI", typeof(int));
        dtTasksNew.Columns.Add("RiskLevelAI", typeof(int));
        dtTasksNew.Columns.Add("SeverityRI", typeof(int));
        dtTasksNew.Columns.Add("LikeliHoodRI", typeof(int));
        dtTasksNew.Columns.Add("RiskLevelRI", typeof(int));
        dtTasksNew.Columns.Add("ADD_CONTROL_MEASURES", typeof(string));
        dtTasksNew.Columns.Add("Re_SeverityPF", typeof(int));
        dtTasksNew.Columns.Add("Re_LikeliHoodPF", typeof(int));
        dtTasksNew.Columns.Add("Re_RiskLevelPF", typeof(int));
        dtTasksNew.Columns.Add("Re_SeverityEF", typeof(int));
        dtTasksNew.Columns.Add("Re_LikeliHoodEF", typeof(int));
        dtTasksNew.Columns.Add("Re_RiskLevelEF", typeof(int));
        dtTasksNew.Columns.Add("Re_SeverityAF", typeof(int));
        dtTasksNew.Columns.Add("Re_LikeliHoodAF", typeof(int));
        dtTasksNew.Columns.Add("Re_RiskLevelAF", typeof(int));
        dtTasksNew.Columns.Add("Re_SeverityRF", typeof(int));
        dtTasksNew.Columns.Add("Re_LikeliHoodRF", typeof(int));
        dtTasksNew.Columns.Add("Re_RiskLevelRF", typeof(int));
        // dtTasksNew.Columns.Add("GRVNo", typeof(int));
        dtTasksNew.Columns.Add("Proceed", typeof(string));
        dtTasksNew.Columns.Add("AGREED_TIME", typeof(string));
        dtTasksNew.Columns.Add("PIC_NAME", typeof(string));
        dtTasksNew.Columns.Add("Status", typeof(string));

        dtTasksNew.AcceptChanges();
    }

    public void ShowExtResRisk()
    {
        DataTable dtRiskLevel = new DataTable();
        dtRiskLevel.Columns.Add("RiskLevel", typeof(int));

        DataRow drPI = dtRiskLevel.NewRow();
        drPI["RiskLevel"] = dtTasksNew.Compute("MAX(RiskLevelPI)", "Status='A'");
        dtRiskLevel.Rows.Add(drPI);

        DataRow drEI = dtRiskLevel.NewRow();
        drEI["RiskLevel"] = dtTasksNew.Compute("MAX(RiskLevelEI)", "Status='A'");
        dtRiskLevel.Rows.Add(drEI);

        DataRow drAI = dtRiskLevel.NewRow();
        drAI["RiskLevel"] = dtTasksNew.Compute("MAX(RiskLevelAI)", "Status='A'");
        dtRiskLevel.Rows.Add(drAI);

        DataRow drRI = dtRiskLevel.NewRow();
        drRI["RiskLevel"] = dtTasksNew.Compute("MAX(RiskLevelRI)", "Status='A'");
        dtRiskLevel.Rows.Add(drRI);

        object RiskLevel = dtRiskLevel.Compute("MAX(RiskLevel)", "");
        //object RiskLevel = dtTasksNew.Compute("MAX(RiskLevelPI)", "Status='A'");
        string ExtColor = GetCSSColor(RiskLevel);
        if (ExtColor.Trim() != "")
        {
            imgER.Visible = true;
            imgER.ImageUrl = "~/Modules/HRD/Images/" + ExtColor + ".png";
        }
        else
        {
            imgER.Visible = false;
        }
        lblExtAction.Text = GetAction(ExtColor);

        DataTable dtReRiskLevel = new DataTable();
        dtReRiskLevel.Columns.Add("Re_RiskLevel", typeof(int));

        DataRow drPF = dtReRiskLevel.NewRow();
        drPF["Re_RiskLevel"] = dtTasksNew.Compute("MAX(Re_RiskLevelPF)", "Status='A'");
        dtReRiskLevel.Rows.Add(drPF);

        DataRow drEF = dtReRiskLevel.NewRow();
        drEF["Re_RiskLevel"] = dtTasksNew.Compute("MAX(Re_RiskLevelEF)", "Status='A'");
        dtReRiskLevel.Rows.Add(drEF);

        DataRow drAF = dtReRiskLevel.NewRow();
        drAF["Re_RiskLevel"] = dtTasksNew.Compute("MAX(Re_RiskLevelAF)", "Status='A'");
        dtReRiskLevel.Rows.Add(drAF);

        DataRow drRF = dtReRiskLevel.NewRow();
        drRF["Re_RiskLevel"] = dtTasksNew.Compute("MAX(Re_RiskLevelRF)", "Status='A'");
        dtReRiskLevel.Rows.Add(drRF);

        object Re_RiskLevel = dtReRiskLevel.Compute("MAX(Re_RiskLevel)", "");
        //object Re_RiskLevel = dtTasksNew.Compute("MAX(Re_RiskLevelPF)", "Status='A'");
        string ResColor = GetCSSColor(Re_RiskLevel);
        if (ResColor.Trim() != "")
        {
            imgRR.Visible = true;
            imgRR.ImageUrl = "~/Modules/HRD/Images/" + ResColor + ".png";
        }
        else
        {
            imgRR.Visible = false;
        }

        lblResAction.Text = GetAction(ResColor);
    }

    public string GetCSSColor(object RiskLevel)
    {
        int Level = Common.CastAsInt32(RiskLevel);
        string Color = "";

        if (Level == 0)
        {
            return Color;
        }

        if (Level >= 16)
        {
            Color = "r";
        }
        else if (Level >= 12)
        {
            Color = "a";
        }
        else if (Level >= 8)
        {
            Color = "b";
        }
        else
        {
            Color = "g";
        }

        return Color;
    }
    public string GetRisk(string Color)
    {
        string Action = "";

        if (Color == "r")
        {
            Action = "High";
        }
        else if (Color == "a")
        {
            Action = "Warning";
        }
        else if (Color == "b")
        {
            Action = "Medium";
        }
        else if (Color == "g")
        {
            Action = "Low";
        }
        else
        {

        }

        return Action;
    }

    public string GetAction(string Color)
    {
        string Action = "";

        if (Color == "r")
        {
            Action = "Do not undertake Consequences.If operation is already in progress, abort and inform office.";
        }
        else if (Color == "a")
        {
            Action = "Job to be under taken only with office Approval.";
        }
        else if (Color == "b")
        {
            Action = "Job can be under taken by ship staff with direct supervision of Master and/ or Chief Engineer.";
        }
        else if (Color == "g")
        {
            Action = "Job can be under taken by ship staff.";
        }
        else
        {

        }

        return Action;
    }
    //protected void BindGrid()
    //{
    //    rptRisk.DataSource = HazardsList;
    //    rptRisk.DataBind();

    //    ShowFinalRatings();
    //}
    //public void BindHazards()
    //{
    //    HazardsList = Common.Execute_Procedures_Select_ByQuery("SELECT  ROW_NUMBER() OVER (ORDER BY HAZARDID) AS SRKey,0 AS TABLID,0 AS RISKID,'' AS VESSELCODE,'N' AS ROUTINE,HAZARDID,HAZARDNAME,0 AS LIKELIHOOD,0 AS CONSEQUENCES,'' AS RISKRANK,0 AS Re_LIKELIHOOD,0 AS Re_CONSEQUENCES,'' AS Re_RISKRANK,'' AS STD_CONTROL_MESASRUES,'' AS ADD_CONTROL_MEASURES, '' AS AGREED_TIME,'' AS PIC_NAME FROM dbo.EV_HazardMaster WHERE EventId=" + EventId);
    //    BindGrid();

    //    ShowFinalRatings();
    //}

    //protected void btnRiskAssessment_Click(object sender, EventArgs e)
    //{
    //    SRKey = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
    //    dv_RiskAssessment.Visible = true;
    //}
    //protected void btnCloseRA_Click(object sender, EventArgs e)
    //{
    //    SRKey = 0;
    //    dv_RiskAssessment.Visible = false;
    //}
    protected void chkAlt_OnCheckedChanged(object sender, EventArgs e)
    {
        txtDetails.Visible = (ddlAlt.SelectedValue.Trim() == "Y");
        if (!txtDetails.Visible)
            txtDetails.Text = "";
    }
    //protected void btnFillRA_Click(object sender, EventArgs e)
    //{
    //    string[] values = ((Button)sender).CommandArgument.Trim().Split(',');

    //    string Liklihood = values[0];
    //    string Consequence = values[1];
    //    string RiskRank = values[2];

    //    hfHazards.Value = Liklihood.Trim() + "," + Consequence.Trim() + "," + RiskRank.Trim();

    //    lblIR1.Text = getFullText(Liklihood, "L");
    //    lblIR2.Text = getFullText(Consequence, "C");
    //    lblIR3.Text = ((Button)sender).Text;

    //}
    //protected void btnFillResidual_Click(object sender, EventArgs e)
    //{
    //    string[] values = ((Button)sender).CommandArgument.Trim().Split(',');

    //    string Liklihood = values[0];
    //    string Consequence = values[1];
    //    string RiskRank = values[2];

    //    hfResidual.Value = Liklihood.Trim() + "," + Consequence.Trim() + "," + RiskRank.Trim();

    //    lblRR1.Text = getFullText(Liklihood, "L");
    //    lblRR2.Text = getFullText(Consequence, "C");
    //    lblRR3.Text = ((Button)sender).Text;
    //}
    //protected void ShowMangementPlan(object sender, EventArgs e)
    //{
    //    SRKey = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
    //    HazardId = Common.CastAsInt32(((LinkButton)sender).Attributes["HazardId"]);

    //    DataRow[] drs = HazardsList.Select("SRKey=" + SRKey);
    //    if (drs.Length > 0)
    //    {
    //        hfHazards.Value = drs[0]["LIKELIHOOD"].ToString().Trim() + "," + drs[0]["CONSEQUENCES"].ToString().Trim() + "," + drs[0]["RISKRANK"].ToString().Trim();
    //        hfResidual.Value = drs[0]["Re_LIKELIHOOD"].ToString().Trim() + "," + drs[0]["Re_CONSEQUENCES"].ToString().Trim() + "," + drs[0]["Re_RISKRANK"].ToString().Trim();

    //        lblIR1.Text = getFullText(drs[0]["LIKELIHOOD"].ToString().Trim(), "L");
    //        lblIR2.Text = getFullText(drs[0]["CONSEQUENCES"].ToString().Trim(), "C");
    //        lblIR3.Text = (drs[0]["RISKRANK"].ToString().Trim() == "L") ? "Low Risk" : ((drs[0]["RISKRANK"].ToString().Trim() == "M") ? "Medium Risk" : (drs[0]["RISKRANK"].ToString().Trim() == "H") ? "High Risk" : "");

    //        lblRR1.Text = getFullText(drs[0]["Re_LIKELIHOOD"].ToString().Trim(), "L");
    //        lblRR2.Text = getFullText(drs[0]["Re_CONSEQUENCES"].ToString().Trim(), "C");
    //        lblRR3.Text = (drs[0]["Re_RISKRANK"].ToString().Trim() == "L") ? "Low Risk" : ((drs[0]["Re_RISKRANK"].ToString().Trim() == "M") ? "Medium Risk" : (drs[0]["Re_RISKRANK"].ToString().Trim() == "H") ? "High Risk" : "");

    //        rad_R_yes.Checked = (drs[0]["ROUTINE"].ToString().Trim() == "Y");
    //        rad_R_no.Checked = !rad_R_yes.Checked;

    //        txtSCM.Text = drs[0]["STD_CONTROL_MESASRUES"].ToString();
    //        txtACM.Text = drs[0]["ADD_CONTROL_MEASURES"].ToString();
    //        txtAgreedTime.Text = drs[0]["AGREED_TIME"].ToString();
    //        //try
    //        //{
    //        //    ddlRAC.SelectedValue = drs[0]["RISK_AFTER_CONTROL"].ToString();
    //        //}
    //        //catch { }
    //        txtPN.Text = drs[0]["PIC_NAME"].ToString();
    //    }
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.EV_HazardMaster WHERE HAZARDID=" + HazardId);
    //    if (dt.Rows.Count > 0)
    //    {
    //        dv_SCM.InnerHtml = dt.Rows[0]["STD_CONTROL_MEASURES"].ToString();
    //        dvACM.InnerHtml = dt.Rows[0]["ADD_CONTROL_MEASURES"].ToString();
    //    }
    //    dv_MgmtPlan.Visible = true;
    //}
    //protected void btnCloseMP_Click(object sender, EventArgs e)
    //{
    //    hfd_MP.Value = "";
    //    txtSCM.Text = "";
    //    txtACM.Text = "";
    //    txtAgreedTime.Text = "";
    //    //ddlRAC.SelectedIndex = 0;
    //    txtPN.Text = "";

    //    dv_MgmtPlan.Visible = false;
    //}
    //protected void btnSaveMP_Click(object sender, EventArgs e)
    //{
    //    if (hfHazards.Value.Trim() == "" || hfHazards.Value.Trim() == ",," || hfHazards.Value.Contains("0"))
    //    {
    //        lblMsg.ShowMessage("Please select Inherent Risk.", true);
    //        return;
    //    }
    //    if (hfResidual.Value.Trim() == "" || hfResidual.Value.Trim() == ",," || hfResidual.Value.Contains("0"))
    //    {
    //        lblMsg.ShowMessage("Please select Residual Risk.", true);
    //        return;
    //    }

    //    string[] values = hfHazards.Value.Trim().Split(',');

    //    string Liklihood = values[0];
    //    string Consequence = values[1];
    //    string RiskRank = values[2];

    //    string[] values1 = hfResidual.Value.Trim().Split(',');

    //    string Liklihood1 = values1[0];
    //    string Consequence1 = values1[1];
    //    string RiskRank1 = values1[2];

    //    foreach (DataRow dr in HazardsList.Rows)
    //    {
    //        if (SRKey == Common.CastAsInt32(dr["SRKey"]))
    //        {
    //            dr["LIKELIHOOD"] = Liklihood;
    //            dr["CONSEQUENCES"] = Consequence;
    //            dr["RISKRANK"] = RiskRank;

    //            dr["Re_LIKELIHOOD"] = Liklihood1;
    //            dr["Re_CONSEQUENCES"] = Consequence1;
    //            dr["Re_RISKRANK"] = RiskRank1;
    //        }
    //    }

    //    foreach (DataRow dr in HazardsList.Rows)
    //    {
    //        if (SRKey == Common.CastAsInt32(dr["SRKey"]))
    //        {
    //            dr["ROUTINE"] = (rad_R_yes.Checked) ? "Y" : "N";
    //            dr["STD_CONTROL_MESASRUES"] = txtSCM.Text.Trim();
    //            dr["ADD_CONTROL_MEASURES"] = txtACM.Text.Trim();
    //            dr["AGREED_TIME"] = txtAgreedTime.Text.Trim();

    //            //dr["RISK_AFTER_CONTROL"] = ddlRAC.SelectedValue.Trim();
    //            dr["PIC_NAME"] = txtPN.Text.Trim();
    //        }
    //    }
    //    BindGrid();
    //    lblMsg.ShowMessage("Record saved successfully.",true);
    //}

    //protected void lnkAddHazard_Click(object sender, EventArgs e)
    //{
    //    dv_NewHazard.Visible = true;
    //    txtHazardName.Text = "";
    //}
    //protected void btnAddHazard_Click(object sender, EventArgs e)
    //{
    //    DataRow dr = HazardsList.NewRow();
    //    HazardsList.Rows.Add(dr);
    //    int Max = Common.CastAsInt32(HazardsList.Compute("Max(SRKey)", ""));
    //    dr["SRKey"] = Max + 1;
    //    dr["HAZARDNAME"] = txtHazardName.Text;
    //    lblM1.Text = "Added Successfully";
    //    BindGrid();
    //}
    //protected void btnCloseAddHazard_Click(object sender, EventArgs e)
    //{
    //    dv_NewHazard.Visible = false;
    //}

    //protected void btnAddHazard_Click(object sender, EventArgs e)
    //{
    //    rptAddHazard.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[EV_HazardMaster] ORDER BY HazardName");
    //    rptAddHazard.DataBind();
    //    dvNewHazard.Visible = true;
    //}

    protected void btnSelectHazard_Click(object sender, EventArgs e)
    {
        int HazardId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string HazardName = ((ImageButton)sender).ToolTip;
        try
        {
            DataView dv = dtHazardsNew.DefaultView;
            dv.RowFilter = "Hazard_TableId=" + HazardId + " AND Status='A'";
            if (dv.ToTable().Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please Check! Hazard already exists.');", true);
                return;
            }

            DataRow dr = dtHazardsNew.NewRow();
            dr["Hazard_TableId"] = HazardId;
            dr["HazardName"] = HazardName;
            dr["Status"] = "A";

            dtHazardsNew.Rows.Add(dr);
            dtHazardsNew.AcceptChanges();
            BindHazardsNew();
            dvNewHazard.Visible = false;
            //----
            ViewState["HazardId"] = HazardId;
            ShowAddHazard();

        }
        catch (Exception ex)
        {
            msg1.ShowMessage("Unable to add. Error : " + ex.Message.ToString(), true);

        }

    }

    protected void btnCancelNew_Click(object sender, EventArgs e)
    {
        dvNewHazard.Visible = false;
    }

    protected void ShowAddHazard()
    {
        DataRow[] dr1s = dtHazardsNew.Select("Hazard_TableId=" + ViewState["HazardId"].ToString());
        if (dr1s.Length > 0)
        {
            lblHazardName.Text = dr1s[0]["HazardName"].ToString();
            dv_NewTask.Visible = true;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int _RiskId = 0;

        if (txtCreatedBy.Text.Trim() == "")
        {
            txtCreatedBy.Focus();
            msg1.ShowMessage("Please enter Created By.", true);
            return;
        }
        if (txtHOD.Text.Trim() == "")
        {
            txtHOD.Focus();
            msg1.ShowMessage("Please enter HOD Name.", true);
            return;
        }
        if (ddlPosition.SelectedItem.Text == "< Select >")
        {
            ddlPosition.Focus();
            msg1.ShowMessage("Please select Position.", true);
            return;
        }


        if (txtEventDate.Text.Trim() == "")
        {
            txtEventDate.Focus();
            msg1.ShowMessage("Please enter event date.", true);
            return;
        }

        if (txtSO.Text.Trim() == "")
        {
            txtSO.Focus();
            msg1.ShowMessage("Please enter Safety Officer Name.", true);
            return;
        }
        if (txtMaster.Text.Trim() == "")
        {
            txtMaster.Focus();
            msg1.ShowMessage("Please enter Master Name.", true);
            return;
        }
        if (ddlAlt.SelectedValue.Trim() == "Y" && txtDetails.Text.Trim() == "")
        {
            txtDetails.Focus();
            msg1.ShowMessage("Please enter Details.", true);
            return;
        }

        _RiskId = RiskId;
        try
        {
            Common.Set_Procedures("[dbo].[RA_InsertUpdateRiskMaster]");
            Common.Set_ParameterLength(19);
            Common.Set_Parameters(

               new MyParameter("@RISKID", _RiskId),
               new MyParameter("@VESSELCODE", VesselCode.ToString().Trim()),
               new MyParameter("@EventId", EventId),
               new MyParameter("@EVENTDATE", txtEventDate.Text.Trim()),
               new MyParameter("@REFNO", lblRefNo.Text.Trim()),
               new MyParameter("@GeographLocation", txtGrographicLocation.Text.Trim()),
               new MyParameter("@PTWRequired", ddlPTWRequired.SelectedValue),
               new MyParameter("@PTWType", ddlTypeoFPTW.SelectedItem.Text.Trim()),
               new MyParameter("@PersonReqTask", ddlPerRefTask.SelectedItem.Text.Trim()),
               new MyParameter("@NoofPersonReqTask", txtNoofPerTask.Text.Trim()),
               new MyParameter("@RiskDescr", txtRiskDescr.Text.Trim()),
               new MyParameter("@ALTERNATEMETHODS", ddlAlt.SelectedValue.Trim()),
               new MyParameter("@Details", txtDetails.Text.Trim()),
               new MyParameter("@HOD_NAME", txtHOD.Text.Trim()),
               new MyParameter("@SAF_OFF_NAME", txtSO.Text.Trim()),
               new MyParameter("@MASTER_NAME", txtMaster.Text.Trim()),
               new MyParameter("@CREATED_BY", txtCreatedBy.Text.Trim()),
               new MyParameter("@CREATED_ON", txtEventDate.Text.Trim()),
               new MyParameter("@POSITION", ddlPosition.SelectedItem.Text.Trim())
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                _RiskId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                foreach (DataRow drHazard in dtHazardsNew.Rows)
                {
                    DataRow[] drs = dtTasksNew.Select("HazardId=" + drHazard["Hazard_TableId"]);
                    foreach (DataRow dr in drs)
                    {
                        Common.Set_Procedures("[dbo].[RA_InsertRiskDetails]");
                        Common.Set_ParameterLength(36);
                        Common.Set_Parameters(
                           new MyParameter("@RISKID", _RiskId),
                           new MyParameter("@VESSELCODE", VesselCode.ToString().Trim()),
                           //new MyParameter("@TemplateId", TemplateId),
                           //new MyParameter("@Hazard_TableId", Common.CastAsInt32(drHazard["Hazard_TableId"])),
                           new MyParameter("@HAZARDID", Common.CastAsInt32(drHazard["Hazard_TableId"])),
                           new MyParameter("@HAZARDNAME", drHazard["HAZARDNAME"].ToString()),
                           new MyParameter("@ConsequencesId", Common.CastAsInt32(dr["ConsequencesId"])),
                            new MyParameter("@ConsequencesCode", dr["ConsequencesCode"].ToString()),
                            new MyParameter("@ConsequencesName", dr["ConsequencesName"].ToString()),
                            new MyParameter("@ControlMeasures", dr["ControlMeasures"].ToString()),
                            new MyParameter("@SeverityPI", Common.CastAsInt32(dr["SeverityPI"])),
                            new MyParameter("@LikeliHoodPI", Common.CastAsInt32(dr["LikeliHoodPI"])),
                            new MyParameter("@RiskLevelPI", Common.CastAsInt32(dr["RiskLevelPI"])),
                            new MyParameter("@SeverityEI", Common.CastAsInt32(dr["SeverityEI"])),
                            new MyParameter("@LikeliHoodEI", Common.CastAsInt32(dr["LikeliHoodEI"])),
                            new MyParameter("@RiskLevelEI", Common.CastAsInt32(dr["RiskLevelEI"])),
                            new MyParameter("@SeverityAI", Common.CastAsInt32(dr["SeverityAI"])),
                            new MyParameter("@LikeliHoodAI", Common.CastAsInt32(dr["LikeliHoodAI"])),
                            new MyParameter("@RiskLevelAI", Common.CastAsInt32(dr["RiskLevelAI"])),
                            new MyParameter("@SeverityRI", Common.CastAsInt32(dr["SeverityRI"])),
                            new MyParameter("@LikeliHoodRI", Common.CastAsInt32(dr["LikeliHoodRI"])),
                            new MyParameter("@RiskLevelRI", Common.CastAsInt32(dr["RiskLevelRI"])),
                            new MyParameter("@ADD_CONTROL_MEASURES", dr["ADD_CONTROL_MEASURES"].ToString()),
                            new MyParameter("@Re_SeverityPF", Common.CastAsInt32(dr["Re_SeverityPF"])),
                            new MyParameter("@Re_LikeliHoodPF", Common.CastAsInt32(dr["Re_LikeliHoodPF"])),
                            new MyParameter("@Re_RiskLevelPF", Common.CastAsInt32(dr["Re_RiskLevelPF"])),
                            new MyParameter("@Re_SeverityEF", Common.CastAsInt32(dr["Re_SeverityEF"])),
                            new MyParameter("@Re_LikeliHoodEF", Common.CastAsInt32(dr["Re_LikeliHoodEF"])),
                            new MyParameter("@Re_RiskLevelEF", Common.CastAsInt32(dr["Re_RiskLevelEF"])),
                            new MyParameter("@Re_SeverityAF", Common.CastAsInt32(dr["Re_SeverityAF"])),
                            new MyParameter("@Re_LikeliHoodAF", Common.CastAsInt32(dr["Re_LikeliHoodAF"])),
                            new MyParameter("@Re_RiskLevelAF", Common.CastAsInt32(dr["Re_RiskLevelAF"])),
                            new MyParameter("@Re_SeverityRF", Common.CastAsInt32(dr["Re_SeverityRF"])),
                            new MyParameter("@Re_LikeliHoodRF", Common.CastAsInt32(dr["Re_LikeliHoodRF"])),
                            new MyParameter("@Re_RiskLevelRF", Common.CastAsInt32(dr["Re_RiskLevelRF"])),
                            //  new MyParameter("@GRVNo", Common.CastAsInt32(dr1["GRVNo"])),
                            new MyParameter("@Proceed", dr["Proceed"].ToString()),
                            new MyParameter("@AGREED_TIME", dr["AGREED_TIME"].ToString()),
                            new MyParameter("@PIC_NAME", dr["PIC_NAME"].ToString())
                           );
                        DataSet ds1 = new DataSet();
                        Boolean res1;
                        res1 = Common.Execute_Procedures_IUD(ds1);

                    }
                }
                RiskId = _RiskId;
                if (lblApprovalRequiredMsg.Text == ApprovalRequiredMsg)
                {
                   // btnExport.Visible = false;
                    btnVerify.Visible = true;
                }
                else
                {
                  //  btnExport.Visible = false;
                    if (Session["UserName"].ToString().ToLower() == "master" || Session["UserName"].ToString().ToLower() == "cheng")
                    {
                        btnVerify.Visible = true;
                    }
                    else
                    {
                        btnVerify.Visible = false;
                    }
                }
                // 
                msg1.ShowMessage("Risk added successfully.", false);
            }
            else
            {
                msg1.ShowMessage("Unable to add Risk.Error : " + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            msg1.ShowMessage("Unable to add Risk.Error :" + ex.Message.ToString(), true);
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (RiskId > 0)
        {

            try
            {
                DataSet ds = new DataSet();
                string SQL = "SELECT * FROM DBO.RA_VSL_RISKMGMT_MASTER with(nolock) WHERE VESSELCODE='" + VesselCode + "' AND RISKID=" + RiskId.ToString();
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                dt.TableName = "RA_VSL_RISKMGMT_MASTER";
                ds.Tables.Add(dt.Copy());
                if (dt.Rows.Count > 0)
                {
                    string RefNo = dt.Rows[0]["REFNO"].ToString();
                    string ZipFileName = "OFFICE-" + RefNo + ".zip";
                    string SQL1 = "SELECT EventName FROM DBO.EV_EventMaster with(nolock) WHERE EventId = " + dt.Rows[0]["EVENTID"].ToString();
                    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL1);
                    string SchemaFile = Server.MapPath("~/Modules/LPSQE/TEMP/RA_Schema.xml");
                    string DataFile = Server.MapPath("~/Modules/LPSQE/TEMP/RA_Data.xml");
                    string ConfigFile = Server.MapPath("~/Modules/LPSQE/TEMP/PacketConfig.xml");
                    string Contents = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                                        "<configuration>" +
                                        "    <PacketName>" + ZipFileName + "</PacketName>" +
                                        "    <PacketType>DATA</PacketType>" +
                                        "    <PacketDataType>RISKDATA</PacketDataType>" +
                                        "    <Reply>N</Reply>" +
                                        "</configuration>";
                    File.WriteAllText(ConfigFile, Contents);
                    string ZipFile = Server.MapPath("~/Modules/LPSQE/TEMP/" + ZipFileName);

                    if (File.Exists(ZipFile))
                    {
                        File.Delete(ZipFile);
                    }
                    ds.WriteXmlSchema(SchemaFile);
                    ds.WriteXml(DataFile);

                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AddFile(SchemaFile);
                        zip.AddFile(DataFile);
                        zip.AddFile(ConfigFile);
                        zip.Save(ZipFile);
                    }

                    //byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
                    //Response.BinaryWrite(buff);
                    //Response.Flush();
                    //Response.End();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<br/><br/>");
                    sb.Append("***********************************************************************");
                    sb.Append("<br/>");
                    sb.Append("<br/>");
                    // sb.Append("<b>Template # : </b>" + dt.Rows[0]["TemplateCode_Revision"].ToString());
                    sb.Append("<br/>");
                    sb.Append("<b>RA # : </b>" + dt.Rows[0]["REFNO"].ToString());
                    sb.Append("<br/>");
                    sb.Append("<br/>");
                    sb.Append(dt1.Rows[0]["EventName"].ToString());
                    sb.Append("<br/>");
                    sb.Append("<br/>");
                    sb.Append("***********************************************************************");
                    sb.Append("<br/>");
                    sb.Append("<br/>");

                    string Subject = dt.Rows[0]["REFNO"].ToString();
                    List<string> CCMails = new List<string>();
                    List<string> BCCMails = new List<string>();

                    SQL = "SELECT Email,VesselEmailNew FROM [dbo].[Vessel] with(nolock)  WHERE VesselCode='" + VesselCode + "'";
                    dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                    string VesselEmail = ((dt != null & dt.Rows.Count > 0) ? dt.Rows[0]["VesselEmailNew"].ToString() : "");
                    string CCEmail = ((dt != null & dt.Rows.Count > 0) ? dt.Rows[0]["Email"].ToString() : "");
                    if (!string.IsNullOrEmpty(CCEmail))
                    {
                        CCMails.Add(CCEmail);
                    }

                    string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
                  //  string UserEmail = ProjectCommon.getUserEmailByID(UserId.ToString());
                    string result = SendMail.SendSimpleMail(fromAddress, VesselEmail, CCMails.ToArray(), BCCMails.ToArray(), Subject, sb.ToString(), ZipFile);
                    if (result == "SENT")
                    {
                        SQL = "INSERT INTO DBO.RA_VSL_RISKMGMT_EXPORT_DETAILS (RiskId,OfficeExportedBy,OfficeExportedOn) VALUES (" + RiskId + ",'" + Session["UserName"].ToString() + "',getdate())";
                        Common.Execute_Procedures_Select_ByQuery(SQL);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Exported to ship successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to export.Error : " + Common.getLastError() + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to export.Error : " + ex.Message + "');", true);
            }


            //try
            //{
            //    Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            //    Common.Set_ParameterLength(5);
            //    Common.Set_Parameters(
            //        new MyParameter("@VesselCode", VesselCode.ToString()),
            //        new MyParameter("@RecordType", "Risk Assessment"),
            //        new MyParameter("@RecordId", RiskId),
            //        new MyParameter("@RecordNo", lblRefNo.Text),
            //        new MyParameter("@CreatedBy", Session["FullUserName"].ToString().Trim())
            //    );

            //    DataSet ds1 = new DataSet();
            //    ds1.Clear();
            //    Boolean res;
            //    res = Common.Execute_Procedures_IUD(ds1);
            //    if (res)
            //    {
            //        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('" + ds1.Tables[0].Rows[0][0].ToString() + "');", true);
            //        }
            //        else
            //        {
            //            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.RA_VSL_RISKMGMT_MASTER SET ExportBy='" + Session["UserName"].ToString() + "',ExportOn=GETDATE() Where RISKID=" + RiskId.ToString());
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Sent for export successfully.');", true);
            //        }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + Common.getLastError() + "');", true);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + ex.Message + "');", true);
            //}



            //msg1.ShowMessage("Risk Assessment Exported successfully.", false);
            //btnSave.Visible = false;
        }
    }

    public string getFullText(string Code, string Type)
    {
        string res = "";
        if (Type == "L")
        {
            switch (Code)
            {
                case "1": res = "Unlikely";
                    break;
                case "2": res = "Possible";
                    break;
                case "3": res = "Quite Possible";
                    break;
                case "4": res = "Likely";
                    break;
                case "5": res = "Very Likely";
                    break;
                default: res = "";
                    break;
            }
        }
        if (Type == "C")
        {
            switch (Code)
            {
                case "1": res = "Negligible";
                    break;
                case "2": res = "Slight";
                    break;
                case "3": res = "Moderate";
                    break;
                case "4": res = "High";
                    break;
                case "5": res = "Very High";
                    break;
                default: res = "";
                    break;
            }
        }
        if (Type == "R")
        {
            switch (Code)
            {
                case "L": res = "Low Risk";
                    break;
                case "M": res = "Medium Risk";
                    break;
                case "H": res = "High Risk";
                    break;
                default: res = "";
                    break;
            }
        }
        return res;
    }
    public string getColor(string R)
    {
        string res = "";
        if (R == "L")
        {
            res = "#80E6B2";
        }
        else if (R == "M")
        {
            res = "#FFFFAD";
        }
        else if (R == "H")
        {
            res = "#FF7373";
        }
        else
        {
            res = "#ffffff";
        }
        return res;
    }
    //public void ShowFinalRatings()
    //{
    //    if (HazardsList.Rows.Count > 0)
    //    {
    //        lbl_In_Probability.Text = getFullText(HazardsList.Compute("MAX(LIKELIHOOD)", "").ToString(), "L");
    //        lbl_In_Impact.Text = getFullText(HazardsList.Compute("MAX(CONSEQUENCES)", "").ToString(), "C");
    //        string MaxRank = "";
    //        MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(RISKRANK)", "RISKRANK='H'")) > 0) ? "H" : "";
    //        if (MaxRank == "")
    //            MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(RISKRANK)", "RISKRANK='M'")) > 0) ? "M" : "";
    //        if (MaxRank == "")
    //            MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(RISKRANK)", "RISKRANK='L'")) > 0) ? "L" : "";

    //        lbl_In_Rating.Text = getFullText(MaxRank, "R");
    //        tdInRating.Style.Add("background-color", getColor(MaxRank));

    //        MaxRank = "";
    //        lbl_Re_Probability.Text = getFullText(HazardsList.Compute("MAX(Re_LIKELIHOOD)", "").ToString(), "L");
    //        lbl_Re_Impact.Text = getFullText(HazardsList.Compute("MAX(Re_CONSEQUENCES)", "").ToString(), "C");

    //        MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(Re_RISKRANK)", "Re_RISKRANK='H'")) > 0) ? "H" : "";
    //        if (MaxRank == "")
    //            MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(Re_RISKRANK)", "Re_RISKRANK='M'")) > 0) ? "M" : "";
    //        if (MaxRank == "")
    //            MaxRank = (Common.CastAsInt32(HazardsList.Compute("Count(Re_RISKRANK)", "Re_RISKRANK='L'")) > 0) ? "L" : "";

    //        lbl_Re_Rating.Text = getFullText(MaxRank, "R");
    //        tdReRating.Style.Add("background-color", getColor(MaxRank));
    //    }
    //}

    public DataTable BindTasksNew(int Hazard_TableId)
    {
        //if (RiskId > 0)
        //{
        DataView dv = dtTasksNew.DefaultView;
        dv.RowFilter = "Status='A' AND HazardId=" + Hazard_TableId;
        return dv.ToTable();
        //}
        //else
        //{
        //    DataView dv = dtTasksNew.DefaultView;
        //    dv.RowFilter = "Status='A' AND Hazard_TableId=" + Hazard_TableId;
        //    return dv.ToTable();
        //}

    }


    protected void btnFillResidual_Click(object sender, EventArgs e)
    {
        //Button bb=((Button)sender);
        //string s=bb.CommandArgument;
        //char[] sep={','};
        //string[] parts=s.Split(sep);

        //lblR11.Text = parts[0];
        //lblR12.Text = parts[1];
        //lblR13.Text = bb.Text;

        int Severity = Common.CastAsInt32(ddlSeveritypi.SelectedValue);
        int Likelihood = Common.CastAsInt32(ddlLikelihoodpi.SelectedValue);
        imgbtnSeveritypi.ToolTip = GetSeverityText(Severity);
        lblLikelihoodTextpi.Text = GetLikelihoodText(Likelihood);
        lblR13pi.Text = Convert.ToString(Severity * Likelihood);
        string Color = GetCSSColor(lblR13pi.Text);
        if (Common.CastAsInt32(lblR13pi.Text) != 0)
            lblR13pi.Text = lblR13pi.Text + " - " + GetRisk(Color);
        else
            lblR13pi.Text = "";
        rd_Rlpi.Attributes.Add("class", Color);
        lblRiskTextpi.Text = GetAction(Color);
    }
    protected void btnFillResidualEI_Click(object sender, EventArgs e)
    {
        //Button bb=((Button)sender);
        //string s=bb.CommandArgument;
        //char[] sep={','};
        //string[] parts=s.Split(sep);

        //lblR11.Text = parts[0];
        //lblR12.Text = parts[1];
        //lblR13.Text = bb.Text;

        int SeverityEI = Common.CastAsInt32(ddlSeverityEI.SelectedValue);
        int LikelihoodEI = Common.CastAsInt32(ddlLikelihoodEI.SelectedValue);
        lblSeverityTextEI.ToolTip = GetSeverityText(SeverityEI);
        lblLikelihoodTextEI.Text = GetLikelihoodText(LikelihoodEI);
        lblR13EI.Text = Convert.ToString(SeverityEI * LikelihoodEI);
        string Color = GetCSSColor(lblR13EI.Text);
        if (Common.CastAsInt32(lblR13EI.Text) != 0)
            lblR13EI.Text = lblR13EI.Text + " - " + GetRisk(Color);
        else
            lblR13EI.Text = "";
        rd_RlEI.Attributes.Add("class", Color);
        lblRiskTextEI.Text = GetAction(Color);
    }
    protected void btnFillResidualAI_Click(object sender, EventArgs e)
    {
        //Button bb=((Button)sender);
        //string s=bb.CommandArgument;
        //char[] sep={','};
        //string[] parts=s.Split(sep);

        //lblR11.Text = parts[0];
        //lblR12.Text = parts[1];
        //lblR13.Text = bb.Text;

        int SeverityAI = Common.CastAsInt32(ddlSeverityAI.SelectedValue);
        int LikelihoodAI = Common.CastAsInt32(ddlLikelihoodAI.SelectedValue);
        lblSeverityTextAI.ToolTip = GetSeverityText(SeverityAI);
        lblLikelihoodTextAI.Text = GetLikelihoodText(LikelihoodAI);
        lblR13AI.Text = Convert.ToString(SeverityAI * LikelihoodAI);
        string Color = GetCSSColor(lblR13AI.Text);
        if (Common.CastAsInt32(lblR13AI.Text) != 0)
            lblR13AI.Text = lblR13AI.Text + " - " + GetRisk(Color);
        else
            lblR13AI.Text = "";
        rd_RlAI.Attributes.Add("class", Color);
        lblRiskTextAI.Text = GetAction(Color);
    }
    protected void btnFillResidualRI_Click(object sender, EventArgs e)
    {
        //Button bb=((Button)sender);
        //string s=bb.CommandArgument;
        //char[] sep={','};
        //string[] parts=s.Split(sep);

        //lblR11.Text = parts[0];
        //lblR12.Text = parts[1];
        //lblR13.Text = bb.Text;

        int SeverityRI = Common.CastAsInt32(ddlSeverityRI.SelectedValue);
        int LikelihoodRI = Common.CastAsInt32(ddlLikelihoodRI.SelectedValue);
        lblSeverityTextRI.ToolTip = GetSeverityText(SeverityRI);
        lblLikelihoodTextRI.Text = GetLikelihoodText(LikelihoodRI);
        lblR13RI.Text = Convert.ToString(SeverityRI * LikelihoodRI);
        string Color = GetCSSColor(lblR13RI.Text);
        if (Common.CastAsInt32(lblR13RI.Text) != 0)
            lblR13RI.Text = lblR13RI.Text + " - " + GetRisk(Color);
        else
            lblR13RI.Text = "";
        rd_RlRI.Attributes.Add("class", Color);
        lblRiskTextRI.Text = GetAction(Color);
    }
    protected void btnReFillResidual_Click(object sender, EventArgs e)
    {
        int SeverityPF = Common.CastAsInt32(ddlReSeverityPF.SelectedValue);
        int LikelihoodPF = Common.CastAsInt32(ddlReLikelihoodPF.SelectedValue);
        ibReSeverityPF.ToolTip = GetSeverityText(SeverityPF);
        lblReLikelihoodTextPF.Text = GetLikelihoodText(LikelihoodPF);
        lblReR13PF.Text = Convert.ToString(SeverityPF * LikelihoodPF);
        string Color = GetCSSColor(lblReR13PF.Text);
        if (Common.CastAsInt32(lblReR13PF.Text) != 0)
            lblReR13PF.Text = lblReR13PF.Text + " - " + GetRisk(Color);
        else
            lblReR13PF.Text = "";
        rd_ReR1PF.Attributes.Add("class", Color);
        lblReRiskTextPF.Text = GetAction(Color);
    }

    protected void btnReFillResidualEF_Click(object sender, EventArgs e)
    {
        int SeverityEF = Common.CastAsInt32(ddlReSeverityEF.SelectedValue);
        int LikelihoodEF = Common.CastAsInt32(ddlReLikelihoodEF.SelectedValue);
        ibReSeverityEF.ToolTip = GetSeverityText(SeverityEF);
        lblReLikelihoodTextEF.Text = GetLikelihoodText(LikelihoodEF);
        lblReR13EF.Text = Convert.ToString(SeverityEF * LikelihoodEF);
        string Color = GetCSSColor(lblReR13EF.Text);
        if (Common.CastAsInt32(lblReR13EF.Text) != 0)
            lblReR13EF.Text = lblReR13EF.Text + " - " + GetRisk(Color);
        else
            lblReR13EF.Text = "";
        rd_ReR1EF.Attributes.Add("class", Color);
        lblReRiskTextEF.Text = GetAction(Color);
    }

    protected void btnReFillResidualAF_Click(object sender, EventArgs e)
    {
        int SeverityAF = Common.CastAsInt32(ddlReSeverityAF.SelectedValue);
        int LikelihoodAF = Common.CastAsInt32(ddlReLikelihoodAF.SelectedValue);
        ibReSeverityAF.ToolTip = GetSeverityText(SeverityAF);
        lblReLikelihoodTextAF.Text = GetLikelihoodText(LikelihoodAF);
        lblReR13AF.Text = Convert.ToString(SeverityAF * LikelihoodAF);
        string Color = GetCSSColor(lblReR13AF.Text);
        if (Common.CastAsInt32(lblReR13AF.Text) != 0)
            lblReR13AF.Text = lblReR13AF.Text + " - " + GetRisk(Color);
        else
            lblReR13AF.Text = "";
        rd_ReR1AF.Attributes.Add("class", Color);
        lblReRiskTextAF.Text = GetAction(Color);
    }

    protected void btnReFillResidualRF_Click(object sender, EventArgs e)
    {
        int SeverityRF = Common.CastAsInt32(ddlReSeverityRF.SelectedValue);
        int LikelihoodRF = Common.CastAsInt32(ddlReLikelihoodRF.SelectedValue);
        ibReSeverityRF.ToolTip = GetSeverityText(SeverityRF);
        lblReLikelihoodTextRF.Text = GetLikelihoodText(LikelihoodRF);
        lblReR13RF.Text = Convert.ToString(SeverityRF * LikelihoodRF);
        string Color = GetCSSColor(lblReR13RF.Text);
        if (Common.CastAsInt32(lblReR13RF.Text) != 0)
            lblReR13RF.Text = lblReR13RF.Text + " - " + GetRisk(Color);
        else
            lblReR13RF.Text = "";
        rd_ReR1RF.Attributes.Add("class", Color);
        lblReRiskTextRF.Text = GetAction(Color);
    }

    protected void btnFillResidualPF_Click(object sender, EventArgs e)
    {

        int SeverityPF = Common.CastAsInt32(ddlReSeverityPF.SelectedValue);
        int LikelihoodPF = Common.CastAsInt32(ddlReLikelihoodPF.SelectedValue);
        ibReSeverityPF.ToolTip = GetSeverityText(SeverityPF);
        lblReLikelihoodTextPF.Text = GetLikelihoodText(LikelihoodPF);
        lblReR13PF.Text = Convert.ToString(SeverityPF * LikelihoodPF);
        string Color = GetCSSColor(lblReR13PF.Text);
        if (Common.CastAsInt32(lblReR13PF.Text) != 0)
            lblReR13PF.Text = lblReR13PF.Text + " - " + GetRisk(Color);
        else
            lblReR13PF.Text = "";
        rd_ReR1PF.Attributes.Add("class", Color);
        lblReRiskTextPF.Text = GetAction(Color);
    }

    protected void btnFillResidualEF_Click(object sender, EventArgs e)
    {

        int SeverityEF = Common.CastAsInt32(ddlReSeverityEF.SelectedValue);
        int LikelihoodEF = Common.CastAsInt32(ddlReLikelihoodEF.SelectedValue);
        ibReSeverityEF.ToolTip = GetSeverityText(SeverityEF);
        lblReLikelihoodTextEF.Text = GetLikelihoodText(LikelihoodEF);
        lblReR13EF.Text = Convert.ToString(SeverityEF * LikelihoodEF);
        string Color = GetCSSColor(lblReR13EF.Text);
        if (Common.CastAsInt32(lblReR13EF.Text) != 0)
            lblReR13EF.Text = lblReR13EF.Text + " - " + GetRisk(Color);
        else
            lblReR13EF.Text = "";
        rd_ReR1EF.Attributes.Add("class", Color);
        lblReRiskTextEF.Text = GetAction(Color);
    }

    protected void btnFillResidualAF_Click(object sender, EventArgs e)
    {

        int SeverityAF = Common.CastAsInt32(ddlReSeverityAF.SelectedValue);
        int LikelihoodAF = Common.CastAsInt32(ddlReLikelihoodAF.SelectedValue);
        ibReSeverityAF.ToolTip = GetSeverityText(SeverityAF);
        lblReLikelihoodTextAF.Text = GetLikelihoodText(LikelihoodAF);
        lblReR13AF.Text = Convert.ToString(SeverityAF * LikelihoodAF);
        string Color = GetCSSColor(lblReR13AF.Text);
        if (Common.CastAsInt32(lblReR13AF.Text) != 0)
            lblReR13AF.Text = lblReR13AF.Text + " - " + GetRisk(Color);
        else
            lblReR13AF.Text = "";
        rd_ReR1AF.Attributes.Add("class", Color);
        lblReRiskTextAF.Text = GetAction(Color);
    }

    protected void btnFillResidualRF_Click(object sender, EventArgs e)
    {

        int SeverityRF = Common.CastAsInt32(ddlReSeverityRF.SelectedValue);
        int LikelihoodRF = Common.CastAsInt32(ddlReLikelihoodRF.SelectedValue);
        ibReSeverityRF.ToolTip = GetSeverityText(SeverityRF);
        lblReLikelihoodTextRF.Text = GetLikelihoodText(LikelihoodRF);
        lblReR13RF.Text = Convert.ToString(SeverityRF * LikelihoodRF);
        string Color = GetCSSColor(lblReR13RF.Text);
        if (Common.CastAsInt32(lblReR13RF.Text) != 0)
            lblReR13RF.Text = lblReR13RF.Text + " - " + GetRisk(Color);
        else
            lblReR13RF.Text = "";
        rd_ReR1RF.Attributes.Add("class", Color);
        lblReRiskTextRF.Text = GetAction(Color);
    }


    public string GetSeverityText(int Severity)
    {
        string Text = "";

        switch (Severity)
        {
            case 1:
                Text = "* No effect on reputation\n* Negligible economic loss which can be restored \n* Nill to sea : contained onboard";
                break;
            case 2:
                Text = "* Small reduction of reputation in the short run\n* Economic loss upto US$10,000 which can be restored \n* Sheen on sea : evidance of loss to sea";
                break;
            case 3:
                Text = "* Reduction of reputation that may influence trust and respect\n* Economic loss between US$10,000 and US$100,000 which can be restored \n* Less than 1 m3 to sea";
                break;
            case 4:
                Text = "* Serious loss of reputation that will influence trust and respect for a long time\n* Lagre economic loss more than US$100,000 that can be restored \n* 1 to 5 m3 to sea";
                break;
            case 5:
                Text = "* Serious loss of reputation which is devastating for trust and respect\n* Considerable economic loss which can not be restored\n* More than 5 m3 to sea   ";
                break;
            default:
                Text = "";
                break;

        }

        return Text;
    }
    public string GetLikelihoodText(int Likelihood)
    {
        string Text = "";

        switch (Likelihood)
        {
            case 1:
                Text = " Never heard within the industry ";
                break;
            case 2:
                Text = " Occurs less than 0.1% of the time/ cases ";
                break;
            case 3:
                Text = " Occurs between 0.1% and 1% of the time/ cases ";
                break;
            case 4:
                Text = " Occurs between 1% and 10% of the time/ cases ";
                break;
            case 5:
                Text = " More frequently than 10% of the time/ cases ";
                break;
            default:
                Text = "";
                break;

        }

        return Text;
    }
    protected void btnViewTask_Click(object sender, EventArgs e)
    {
        lblMess.Text = "";
        int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        //btnDeleteTask.CommandArgument = hfdTaskIdNew.Value;
        DataRow[] drs = dtTasksNew.Select("TableId=" + _TableId);
        foreach (DataRow dr in drs)
        {
            DataView dv = dtHazardsNew.DefaultView;
            dv.RowFilter = "Hazard_TableId=" + dr["HazardId"];

            hfdTaskId.Text = dr["ConsequencesId"].ToString();
            // ddlTask.SelectedValue = dr["ConsequencesId"].ToString();
            // ddlTask.Enabled = false;
            txtTaskname.Text = dr["ConsequencesCode"].ToString() + ":" + dr["ConsequencesName"].ToString();
            txtTaskname.Enabled = false;

            txtStdCM.Text = dr["ControlMeasures"].ToString();
            // For People
            ddlSeveritypi.SelectedValue = dr["SeverityPI"].ToString();
            ddlLikelihoodpi.SelectedValue = dr["LikeliHoodPI"].ToString();
            lblR13pi.Text = dr["RiskLevelPI"].ToString();
            imgbtnSeveritypi.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityPI"]));
            lblLikelihoodTextpi.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodPI"]));
            string Color = GetCSSColor(lblR13pi.Text);
            if (Common.CastAsInt32(lblR13pi.Text) != 0)
                lblR13pi.Text = lblR13pi.Text + " - " + GetRisk(Color);
            else
                lblR13pi.Text = "";
            rd_Rlpi.Attributes.Add("class", Color);
            lblRiskTextpi.Text = GetAction(Color);

            // For Enviroment
            if (! string.IsNullOrWhiteSpace(dr["SeverityEI"].ToString()))
            {
                ddlSeverityEI.SelectedValue = dr["SeverityEI"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dr["LikeliHoodEI"].ToString()))
            {
                ddlLikelihoodEI.SelectedValue = dr["LikeliHoodEI"].ToString();
            }
            lblR13EI.Text = dr["RiskLevelEI"].ToString();
            lblSeverityTextEI.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityEI"]));
            lblLikelihoodTextEI.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodEI"]));
            Color = GetCSSColor(lblR13EI.Text);
            if (Common.CastAsInt32(lblR13EI.Text) != 0)
                lblR13EI.Text = lblR13EI.Text + " - " + GetRisk(Color);
            else
                lblR13EI.Text = "";
            rd_RlEI.Attributes.Add("class", Color);
            lblRiskTextEI.Text = GetAction(Color);

            // For Assest
            if (!string.IsNullOrWhiteSpace(dr["SeverityAI"].ToString()))
            {
                ddlSeverityAI.SelectedValue = dr["SeverityAI"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dr["LikeliHoodAI"].ToString()))
            {
                ddlLikelihoodAI.SelectedValue = dr["LikeliHoodAI"].ToString();
            }

            lblR13AI.Text = dr["RiskLevelAI"].ToString();
            lblSeverityTextAI.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityAI"]));
            lblLikelihoodTextAI.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodAI"]));
            Color = GetCSSColor(lblR13AI.Text);
            if (Common.CastAsInt32(lblR13AI.Text) != 0)
                lblR13AI.Text = lblR13AI.Text + " - " + GetRisk(Color);
            else
                lblR13AI.Text = "";
            rd_RlAI.Attributes.Add("class", Color);
            lblRiskTextAI.Text = GetAction(Color);

            // For reputation
            if (!string.IsNullOrWhiteSpace(dr["SeverityRI"].ToString()))
            {
                ddlSeverityRI.SelectedValue = dr["SeverityRI"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dr["LikeliHoodRI"].ToString()))
            {
                ddlLikelihoodRI.SelectedValue = dr["LikeliHoodRI"].ToString();
            }
            lblR13RI.Text = dr["RiskLevelRI"].ToString();
            lblSeverityTextRI.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityRI"]));
            lblLikelihoodTextRI.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodRI"]));
            Color = GetCSSColor(lblR13RI.Text);
            if (Common.CastAsInt32(lblR13RI.Text) != 0)
                lblR13RI.Text = lblR13RI.Text + " - " + GetRisk(Color);
            else
                lblR13RI.Text = "";
            rd_RlRI.Attributes.Add("class", Color);
            lblRiskTextRI.Text = GetAction(Color);

            txtACM.Text = dr["ADD_CONTROL_MEASURES"].ToString();

            //txtGRANo.Text = dr["GRVNo"].ToString();

            // For People Final Risk
            ddlReSeverityPF.SelectedValue = dr["Re_SeverityPF"].ToString();
            ddlReLikelihoodPF.SelectedValue = dr["Re_LikeliHoodPF"].ToString();
            lblReR13PF.Text = dr["Re_RiskLevelPF"].ToString();
            ibReSeverityPF.ToolTip = GetSeverityText(Common.CastAsInt32(dr["Re_SeverityPF"]));
            lblReLikelihoodTextPF.Text = GetLikelihoodText(Common.CastAsInt32(dr["Re_LikeliHoodPF"]));
            Color = GetCSSColor(lblReR13PF.Text);
            if (Common.CastAsInt32(lblReR13PF.Text) != 0)
                lblReR13PF.Text = lblReR13PF.Text + " - " + GetRisk(Color);
            else
                lblReR13PF.Text = "";
            rd_ReR1PF.Attributes.Add("class", Color);
            lblReRiskTextPF.Text = GetAction(Color);

            // For Enviroment Final Risk
            if (!string.IsNullOrWhiteSpace(dr["Re_SeverityEF"].ToString()))
            {
                ddlReSeverityEF.SelectedValue = dr["Re_SeverityEF"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dr["Re_LikeliHoodEF"].ToString()))
            {
                ddlReLikelihoodEF.SelectedValue = dr["Re_LikeliHoodEF"].ToString();
            }
            lblReR13EF.Text = dr["Re_RiskLevelEF"].ToString();
            ibReSeverityEF.ToolTip = GetSeverityText(Common.CastAsInt32(dr["Re_SeverityEF"]));
            lblReLikelihoodTextEF.Text = GetLikelihoodText(Common.CastAsInt32(dr["Re_LikeliHoodEF"]));
            Color = GetCSSColor(lblReR13EF.Text);
            if (Common.CastAsInt32(lblReR13EF.Text) != 0)
                lblReR13EF.Text = lblReR13EF.Text + " - " + GetRisk(Color);
            else
                lblReR13EF.Text = "";
            rd_ReR1EF.Attributes.Add("class", Color);
            lblReRiskTextEF.Text = GetAction(Color);

            // For Assest Final Risk
            if (!string.IsNullOrWhiteSpace(dr["Re_SeverityAF"].ToString()))
            {
                ddlReSeverityAF.SelectedValue = dr["Re_SeverityAF"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dr["Re_LikeliHoodAF"].ToString()))
            {
                ddlReLikelihoodAF.SelectedValue = dr["Re_LikeliHoodAF"].ToString();
            }
            lblReR13AF.Text = dr["Re_RiskLevelAF"].ToString();
            ibReSeverityAF.ToolTip = GetSeverityText(Common.CastAsInt32(dr["Re_SeverityAF"]));
            lblReLikelihoodTextAF.Text = GetLikelihoodText(Common.CastAsInt32(dr["Re_LikeliHoodAF"]));
            Color = GetCSSColor(lblReR13AF.Text);
            if (Common.CastAsInt32(lblReR13AF.Text) != 0)
                lblReR13AF.Text = lblReR13AF.Text + " - " + GetRisk(Color);
            else
                lblReR13AF.Text = "";
            rd_ReR1AF.Attributes.Add("class", Color);
            lblReRiskTextAF.Text = GetAction(Color);

            // For Reputation Final Risk
            if (!string.IsNullOrWhiteSpace(dr["Re_SeverityRF"].ToString()))
            {
                ddlReSeverityRF.SelectedValue = dr["Re_SeverityRF"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dr["Re_LikeliHoodRF"].ToString()))
            {
                ddlReLikelihoodRF.SelectedValue = dr["Re_LikeliHoodRF"].ToString();
            }
            lblReR13RF.Text = dr["Re_RiskLevelRF"].ToString();
            ibReSeverityRF.ToolTip = GetSeverityText(Common.CastAsInt32(dr["Re_SeverityRF"]));
            lblReLikelihoodTextRF.Text = GetLikelihoodText(Common.CastAsInt32(dr["Re_LikeliHoodRF"]));
            Color = GetCSSColor(lblReR13RF.Text);
            if (Common.CastAsInt32(lblReR13RF.Text) != 0)
                lblReR13RF.Text = lblReR13RF.Text + " - " + GetRisk(Color);
            else
                lblReR13RF.Text = "";
            rd_ReR1RF.Attributes.Add("class", Color);
            lblReRiskTextRF.Text = GetAction(Color);

            rdoProceed_Y.Checked = (dr["Proceed"].ToString() == "Y");
            rdoProceed_N.Checked = (dr["Proceed"].ToString() == "N");
            // txtAgreedtime.Text = dr["AGREED_TIME"].ToString();
            txtPIC.Text = dr["PIC_NAME"].ToString();

            btnSaveSingle.Visible = false;
            //btnDeleteTask.Visible = false;
            dv_NewTask.Visible = true;
        }

    }
    public string GetTaskName(object TableId)
    {
        string ret = "";
        DataRow[] drs = dtTasksNew.Select("TableId=" + TableId);
        if (drs.Length > 0)
        {
            drs = dtTasksNew.Select("Hazard_TableId=" + drs[0]["Hazard_TableId"].ToString());
            if (drs.Length > 0)
                ret = drs[0]["ConsequencesName"].ToString();
        }
        return ret;
    }

    //public string GetTaskName(string _TaskId)
    //{
    //    string SQL = "SELECT TaskCode + ' - ' + TaskName AS TNAME,TaskId FROM dbo.EV_TaskMaster with(nolock) where  TaskId=" + _TaskId;
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    if (dt.Rows.Count > 0)
    //        return dt.Rows[0][0].ToString();
    //    else
    //        return "";

    //}

    protected string getTaskName(string _TaskId)
    {
        string SQL = "SELECT TaskCode + ' - ' + TaskName AS TNAME,TaskId FROM dbo.EV_TaskMaster with(nolock) where  TaskId=" + _TaskId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
            return dt.Rows[0][0].ToString();
        else
            return "";

    }
    protected void btnSaveSingle_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtStdCM.Text))
        {
            lblMess.Text = "Please fill Control Measures";
            txtStdCM.Focus();
            return;
        }

        if (ddlSeveritypi.SelectedIndex == 0 || ddlLikelihoodpi.SelectedIndex == 0 || lblR13pi.Text == "") /*|| txtStdCM.Text == ""*/
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood.";
            return;
        }
        else if (ddlSeverityEI.SelectedIndex == 0 || ddlLikelihoodEI.SelectedIndex == 0 || lblR13EI.Text == "") /*|| txtStdCM.Text == ""*/
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood.";
            return;
        }
        else if (ddlSeverityAI.SelectedIndex == 0 || ddlLikelihoodAI.SelectedIndex == 0 || lblR13AI.Text == "") /*|| txtStdCM.Text == ""*/
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood.";
            return;
        }
        else if (ddlSeverityRI.SelectedIndex == 0 || ddlLikelihoodRI.SelectedIndex == 0 || lblR13RI.Text == "") /*|| txtStdCM.Text == ""*/
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood.";
            return;
        }
        if (ddlReSeverityPF.SelectedIndex == 0 || ddlReLikelihoodPF.SelectedIndex == 0 || lblReR13PF.Text == "") /*|| txtStdCM.Text == ""*/
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood.";
            return;
        }
        else if (ddlReSeverityEF.SelectedIndex == 0 || ddlReLikelihoodEF.SelectedIndex == 0 || lblReR13EF.Text == "") /*|| txtStdCM.Text == ""*/
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood.";
            return;
        }
        else if (ddlReSeverityAF.SelectedIndex == 0 || ddlReLikelihoodAF.SelectedIndex == 0 || lblReR13AF.Text == "") /*|| txtStdCM.Text == ""*/
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood.";
            return;
        }
        else if (ddlReSeverityRF.SelectedIndex == 0 || ddlReLikelihoodRF.SelectedIndex == 0 || lblReR13RF.Text == "") /*|| txtStdCM.Text == ""*/
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood.";
            return;
        }
        else if (txtPIC.Text.Trim() == "")
        {
            txtPIC.Focus();
            lblMess.Text = "Please enter Person Incharge.";
            return;
        }
        try
        {
            if (Common.CastAsInt32(hfdTaskIdNew.Value) != 0)
            {
                DataRow[] dr1s = dtTasksNew.Select("TableId=" + hfdTaskIdNew.Value);
                if (dr1s.Length > 0)
                {
                    DataRow dr1 = dr1s[0];
                    dr1["ControlMeasures"] = txtStdCM.Text.Trim();

                    // for People
                    dr1["SeverityPI"] = Common.CastAsInt32(ddlSeveritypi.SelectedValue);
                    dr1["LikeliHoodPI"] = Common.CastAsInt32(ddlLikelihoodpi.SelectedValue);
                    dr1["RiskLevelPI"] = Common.CastAsInt32(lblR13pi.Text.Split('-').GetValue(0));
                    // for Enviroment
                    dr1["SeverityEI"] = Common.CastAsInt32(ddlSeverityEI.SelectedValue);
                    dr1["LikeliHoodEI"] = Common.CastAsInt32(ddlLikelihoodEI.SelectedValue);
                    dr1["RiskLevelEI"] = Common.CastAsInt32(lblR13EI.Text.Split('-').GetValue(0));
                    // For Assest
                    dr1["SeverityAI"] = Common.CastAsInt32(ddlSeverityAI.SelectedValue);
                    dr1["LikeliHoodAI"] = Common.CastAsInt32(ddlLikelihoodAI.SelectedValue);
                    dr1["RiskLevelAI"] = Common.CastAsInt32(lblR13AI.Text.Split('-').GetValue(0));
                    // For Reputation
                    dr1["SeverityRI"] = Common.CastAsInt32(ddlSeverityRI.SelectedValue);
                    dr1["LikeliHoodRI"] = Common.CastAsInt32(ddlLikelihoodRI.SelectedValue);
                    dr1["RiskLevelRI"] = Common.CastAsInt32(lblR13RI.Text.Split('-').GetValue(0));

                    dr1["ADD_CONTROL_MEASURES"] = txtACM.Text.Trim();

                    // dr1["GRVNo"] = Common.CastAsInt32(txtGRANo.Text.Trim());

                    // For People Final Risk
                    dr1["Re_SeverityPF"] = Common.CastAsInt32(ddlReSeverityPF.SelectedValue);
                    dr1["Re_LikeliHoodPF"] = Common.CastAsInt32(ddlReLikelihoodPF.SelectedValue);
                    dr1["Re_RiskLevelPF"] = Common.CastAsInt32(lblReR13PF.Text.Split('-').GetValue(0));

                    // For Environment Final Risk
                    dr1["Re_SeverityEF"] = Common.CastAsInt32(ddlReSeverityEF.SelectedValue);
                    dr1["Re_LikeliHoodEF"] = Common.CastAsInt32(ddlReLikelihoodEF.SelectedValue);
                    dr1["Re_RiskLevelEF"] = Common.CastAsInt32(lblReR13EF.Text.Split('-').GetValue(0));

                    // For Assest Final Risk
                    dr1["Re_SeverityAF"] = Common.CastAsInt32(ddlReSeverityAF.SelectedValue);
                    dr1["Re_LikeliHoodAF"] = Common.CastAsInt32(ddlReLikelihoodAF.SelectedValue);
                    dr1["Re_RiskLevelAF"] = Common.CastAsInt32(lblReR13AF.Text.Split('-').GetValue(0));

                    // For Reputation Final Risk
                    dr1["Re_SeverityRF"] = Common.CastAsInt32(ddlReSeverityRF.SelectedValue);
                    dr1["Re_LikeliHoodRF"] = Common.CastAsInt32(ddlReLikelihoodRF.SelectedValue);
                    dr1["Re_RiskLevelRF"] = Common.CastAsInt32(lblReR13RF.Text.Split('-').GetValue(0));

                    dr1["Proceed"] = (rdoProceed_Y.Checked ? "Y" : "N");
                    dr1["PIC_NAME"] = txtPIC.Text.Trim();
                    dr1["AGREED_TIME"] = "";

                    dr1["Status"] = "A";
                    //dr1["Proceed"] = txtACM.Text.Trim();


                }
            }
            else
            {
                int HazardId = Common.CastAsInt32(ViewState["HazardId"]);
                if (Common.CastAsInt32(hfdTaskId.Text) <= 0)
                {
                    lblMess.Text = "Please select Consequences.";
                    txtTaskname.Focus();
                    return;
                }
                DataView dv1 = dtTasksNew.DefaultView;
                dv1.RowFilter = "HazardId=" + HazardId + " AND ConsequencesId=" + Common.CastAsInt32(hfdTaskId.Text) + " AND Status='A'";
                if (dv1.ToTable().Rows.Count > 0)
                {
                    lblMess.Text = "Please Check! Consequences already exists.";
                    txtTaskname.Focus();
                    return;
                }


                DataRow dr1 = dtTasksNew.NewRow();
                dr1["TableId"] = -(Common.CastAsInt32(dtTasksNew.Rows.Count) + 1);
                //dr1["TemplateId"] = TemplateId;
                dr1["HazardId"] = HazardId;
                dr1["ConsequencesId"] = Common.CastAsInt32(hfdTaskId.Text);
                dr1["ConsequencesCode"] = txtTaskname.Text.Split(':').GetValue(0).ToString().Trim();
                dr1["ConsequencesName"] = txtTaskname.Text.Split(':').GetValue(1).ToString().Trim();
                dr1["ControlMeasures"] = txtStdCM.Text.Trim();

                // for People
                dr1["SeverityPI"] = Common.CastAsInt32(ddlSeveritypi.SelectedValue);
                dr1["LikeliHoodPI"] = Common.CastAsInt32(ddlLikelihoodpi.SelectedValue);
                dr1["RiskLevelPI"] = Common.CastAsInt32(lblR13pi.Text.Split('-').GetValue(0));
                // for Enviroment
                dr1["SeverityEI"] = Common.CastAsInt32(ddlSeverityEI.SelectedValue);
                dr1["LikeliHoodEI"] = Common.CastAsInt32(ddlLikelihoodEI.SelectedValue);
                dr1["RiskLevelEI"] = Common.CastAsInt32(lblR13EI.Text.Split('-').GetValue(0));
                // For Assest
                dr1["SeverityAI"] = Common.CastAsInt32(ddlSeverityAI.SelectedValue);
                dr1["LikeliHoodAI"] = Common.CastAsInt32(ddlLikelihoodAI.SelectedValue);
                dr1["RiskLevelAI"] = Common.CastAsInt32(lblR13AI.Text.Split('-').GetValue(0));
                // For Reputation
                dr1["SeverityRI"] = Common.CastAsInt32(ddlSeverityRI.SelectedValue);
                dr1["LikeliHoodRI"] = Common.CastAsInt32(ddlLikelihoodRI.SelectedValue);
                dr1["RiskLevelRI"] = Common.CastAsInt32(lblR13RI.Text.Split('-').GetValue(0));

                dr1["ADD_CONTROL_MEASURES"] = txtACM.Text.Trim();
                //    dr1["GRVNo"] = Common.CastAsInt32(txtGRANo.Text.Trim());
                // for People Final Risk
                dr1["Re_SeverityPF"] = Common.CastAsInt32(ddlReSeverityPF.SelectedValue);
                dr1["Re_LikeliHoodPF"] = Common.CastAsInt32(ddlReLikelihoodPF.SelectedValue);
                dr1["Re_RiskLevelPF"] = Common.CastAsInt32(lblReR13PF.Text.Split('-').GetValue(0));

                // for Enviroment Final Risk
                dr1["Re_SeverityEF"] = Common.CastAsInt32(ddlReSeverityEF.SelectedValue);
                dr1["Re_LikeliHoodEF"] = Common.CastAsInt32(ddlReLikelihoodEF.SelectedValue);
                dr1["Re_RiskLevelEF"] = Common.CastAsInt32(lblReR13EF.Text.Split('-').GetValue(0));

                // for Assest Final Risk
                dr1["Re_SeverityAF"] = Common.CastAsInt32(ddlReSeverityAF.SelectedValue);
                dr1["Re_LikeliHoodAF"] = Common.CastAsInt32(ddlReLikelihoodAF.SelectedValue);
                dr1["Re_RiskLevelAF"] = Common.CastAsInt32(lblReR13AF.Text.Split('-').GetValue(0));

                // for Reputation Final Risk
                dr1["Re_SeverityRF"] = Common.CastAsInt32(ddlReSeverityRF.SelectedValue);
                dr1["Re_LikeliHoodRF"] = Common.CastAsInt32(ddlReLikelihoodRF.SelectedValue);
                dr1["Re_RiskLevelRF"] = Common.CastAsInt32(lblReR13RF.Text.Split('-').GetValue(0));

                dr1["Proceed"] = (rdoProceed_Y.Checked ? "Y" : "N");
                dr1["PIC_NAME"] = txtPIC.Text.Trim();
                dr1["AGREED_TIME"] = "";

                dr1["Status"] = "A";
                dtTasksNew.Rows.Add(dr1);
            }

            dtTasksNew.AcceptChanges();
            BindHazardsNew();

            //--------------------------------- 
            if (Common.CastAsInt32(hfdTaskIdNew.Value) == 0)
            {
                hfdTaskIdNew.Value = "0";
                hfdTaskId.Text = "0";
                // ddlTask.SelectedIndex = 0;
                txtTaskname.Text = "";
                txtStdCM.Text = "";
                txtACM.Text = "";
                //  txtGRANo.Text = "";

                // People
                ddlSeveritypi.SelectedIndex = 0;
                ddlLikelihoodpi.SelectedIndex = 0;
                lblR13pi.Text = "";
                rd_Rlpi.Attributes.Remove("class");

                // Enviroment
                ddlSeverityEI.SelectedIndex = 0;
                ddlLikelihoodEI.SelectedIndex = 0;
                lblR13EI.Text = "";
                rd_RlEI.Attributes.Remove("class");

                // Asset
                ddlSeverityAI.SelectedIndex = 0;
                ddlLikelihoodAI.SelectedIndex = 0;
                lblR13AI.Text = "";
                rd_RlAI.Attributes.Remove("class");

                // Reputation
                ddlSeverityRI.SelectedIndex = 0;
                ddlLikelihoodRI.SelectedIndex = 0;
                lblR13RI.Text = "";
                rd_RlRI.Attributes.Remove("class");

                ddlReSeverityPF.SelectedIndex = 0;
                ddlReLikelihoodPF.SelectedIndex = 0;
                lblReR13PF.Text = "";
                rd_ReR1PF.Attributes.Remove("class");

                ddlReSeverityEF.SelectedIndex = 0;
                ddlReLikelihoodEF.SelectedIndex = 0;
                lblReR13EF.Text = "";
                rd_ReR1EF.Attributes.Remove("class");

                ddlReSeverityAF.SelectedIndex = 0;
                ddlReLikelihoodAF.SelectedIndex = 0;
                lblReR13AF.Text = "";
                rd_ReR1AF.Attributes.Remove("class");

                ddlReSeverityRF.SelectedIndex = 0;
                ddlReLikelihoodRF.SelectedIndex = 0;
                lblReR13RF.Text = "";
                rd_ReR1RF.Attributes.Remove("class");
            }
            //--------------------------------- 
            lblMess.Text = "Record saved/ updated successfully.";
        }
        catch (Exception ex)
        {
            lblMess.Text = "Unable to add/ update. Error : " + ex.Message.ToString();
        }
    }
    protected void btnCancelTask_Click(object sender, EventArgs e)
    {
        txtTaskname.Text = "";

        hfdTaskId.Text = "0";

        // ddlTask.SelectedIndex = 0;

        txtStdCM.Text = "";
        ddlSeveritypi.SelectedIndex = 0;
        ddlLikelihoodpi.SelectedIndex = 0;
        lblR13pi.Text = "";
        rd_Rlpi.Attributes.Remove("class");

        ddlSeverityEI.SelectedIndex = 0;
        ddlLikelihoodEI.SelectedIndex = 0;
        lblR13EI.Text = "";
        rd_RlEI.Attributes.Remove("class");

        ddlSeverityAI.SelectedIndex = 0;
        ddlLikelihoodAI.SelectedIndex = 0;
        lblR13AI.Text = "";
        rd_RlAI.Attributes.Remove("class");

        ddlSeverityRI.SelectedIndex = 0;
        ddlLikelihoodRI.SelectedIndex = 0;
        lblR13RI.Text = "";
        rd_RlRI.Attributes.Remove("class");

        txtACM.Text = "";

        //txtGRANo.Text = "";


        ddlReSeverityPF.SelectedIndex = 0;
        ddlReLikelihoodPF.SelectedIndex = 0;
        lblReR13PF.Text = "";

        ddlReSeverityEF.SelectedIndex = 0;
        ddlReLikelihoodEF.SelectedIndex = 0;
        lblReR13EF.Text = "";

        ddlReSeverityAF.SelectedIndex = 0;
        ddlReLikelihoodAF.SelectedIndex = 0;
        lblReR13AF.Text = "";

        ddlReSeverityRF.SelectedIndex = 0;
        ddlReLikelihoodRF.SelectedIndex = 0;
        lblReR13RF.Text = "";

        imgbtnSeveritypi.ToolTip = "";
        lblLikelihoodTextpi.Text = "";
        lblRiskTextpi.Text = "";

        lblSeverityTextEI.ToolTip = "";
        lblLikelihoodTextEI.Text = "";
        lblRiskTextEI.Text = "";

        lblSeverityTextAI.ToolTip = "";
        lblLikelihoodTextAI.Text = "";
        lblRiskTextAI.Text = "";

        lblSeverityTextRI.ToolTip = "";
        lblLikelihoodTextRI.Text = "";
        lblRiskTextRI.Text = "";

        ibReSeverityPF.ToolTip = "";
        lblReLikelihoodTextPF.Text = "";
        lblReRiskTextPF.Text = "";
        rd_ReR1PF.Attributes.Remove("class");

        ibReSeverityEF.ToolTip = "";
        lblReLikelihoodTextEF.Text = "";
        lblReRiskTextEF.Text = "";
        rd_ReR1EF.Attributes.Remove("class");

        ibReSeverityAF.ToolTip = "";
        lblReLikelihoodTextAF.Text = "";
        lblReRiskTextAF.Text = "";
        rd_ReR1AF.Attributes.Remove("class");

        ibReSeverityRF.ToolTip = "";
        lblReLikelihoodTextRF.Text = "";
        lblReRiskTextRF.Text = "";
        rd_ReR1RF.Attributes.Remove("class");

        rdoProceed_Y.Checked = false;
        rdoProceed_N.Checked = false;
        //txtAgreedtime.Text = "";
        txtPIC.Text = "";
        hfdTaskIdNew.Value = "";

        //ddlTask.Enabled = true;

        txtTaskname.Enabled = true;

        btnSaveSingle.Visible = true;
        //btnDeleteTask.Visible = false;

        dv_NewTask.Visible = false;
    }
    //protected void btnEditTask_Click(object sender, EventArgs e)
    //{
    //    lblMess.Text = "";
    //    hfdTaskIdNew.Value = ((ImageButton)sender).CommandArgument.Trim();
    //    int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    //btnDeleteTask.CommandArgument = hfdTaskIdNew.Value;
    //    DataRow[] drs = dtTasksNew.Select("TableId=" + _TableId);
    //    foreach (DataRow dr in drs)
    //    {
    //        DataView dv = dtHazardsNew.DefaultView;
    //        dv.RowFilter = "Hazard_TableId=" + dr["HazardId"];
    //        hfdTaskId.Text = dr["ConsequencesId"].ToString();
    //        txtTaskname.Text = dr["ConsequencesCode"].ToString() + ":" + dr["ConsequencesName"].ToString();
    //        txtTaskname.Enabled = false;
    //        //ddlTask.SelectedValue = dr["ConsequencesId"].ToString();
    //        //ddlTask.Enabled = false;

    //        txtStdCM.Text = dr["ControlMeasures"].ToString();

    //        // for Person
    //        ddlSeveritypi.SelectedValue = dr["SeverityPI"].ToString();
    //        ddlLikelihoodpi.SelectedValue = dr["LikeliHoodPI"].ToString();
    //        lblR13pi.Text = dr["RiskLevelPI"].ToString();

    //        imgbtnSeveritypi.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityPI"]));
    //        lblLikelihoodTextpi.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodPI"]));
    //        string ColorPI = GetCSSColor(lblR13pi.Text);
    //        if (Common.CastAsInt32(lblR13pi.Text) != 0)
    //            lblR13pi.Text = lblR13pi.Text + " - " + GetRisk(ColorPI);
    //        else
    //            lblR13pi.Text = "";
    //        rd_Rlpi.Attributes.Add("class", ColorPI);
    //        lblRiskTextpi.Text = GetAction(ColorPI);

    //        // for Environment
    //        if (!string.IsNullOrWhiteSpace(dr["SeverityEI"].ToString()))
    //        {
    //            ddlSeverityEI.SelectedValue = dr["SeverityEI"].ToString();
    //        }
    //        if (!string.IsNullOrWhiteSpace(dr["LikeliHoodEI"].ToString()))
    //        {
    //            ddlLikelihoodEI.SelectedValue = dr["LikeliHoodEI"].ToString();
    //        }
    //        lblR13EI.Text = dr["RiskLevelEI"].ToString();

    //        lblSeverityTextEI.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityEI"]));
    //        lblLikelihoodTextEI.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodEI"]));
    //        string ColorEI = GetCSSColor(lblR13EI.Text);
    //        if (Common.CastAsInt32(lblR13EI.Text) != 0)
    //            lblR13EI.Text = lblR13EI.Text + " - " + GetRisk(ColorEI);
    //        else
    //            lblR13EI.Text = "";
    //        rd_RlEI.Attributes.Add("class", ColorEI);
    //        lblRiskTextEI.Text = GetAction(ColorEI);

    //        // for Asset
    //        if (!string.IsNullOrWhiteSpace(dr["SeverityAI"].ToString()))
    //        {
    //            ddlSeverityAI.SelectedValue = dr["SeverityAI"].ToString();
    //        }
    //        if (!string.IsNullOrWhiteSpace(dr["LikeliHoodAI"].ToString()))
    //        {
    //            ddlLikelihoodAI.SelectedValue = dr["LikeliHoodAI"].ToString();
    //        }
    //        lblR13AI.Text = dr["RiskLevelAI"].ToString();

    //        lblSeverityTextAI.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityAI"]));
    //        lblLikelihoodTextAI.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodAI"]));
    //        string ColorAI = GetCSSColor(lblR13AI.Text);
    //        if (Common.CastAsInt32(lblR13AI.Text) != 0)
    //            lblR13AI.Text = lblR13AI.Text + " - " + GetRisk(ColorAI);
    //        else
    //            lblR13AI.Text = "";
    //        rd_RlAI.Attributes.Add("class", ColorAI);
    //        lblRiskTextAI.Text = GetAction(ColorAI);

    //        // for Reputation
    //        if (!string.IsNullOrWhiteSpace(dr["SeverityRI"].ToString()))
    //        {
    //            ddlSeverityRI.SelectedValue = dr["SeverityRI"].ToString();
    //        }
    //        if (!string.IsNullOrWhiteSpace(dr["LikeliHoodRI"].ToString()))
    //        {
    //            ddlLikelihoodRI.SelectedValue = dr["LikeliHoodRI"].ToString();
    //        }
    //        lblR13RI.Text = dr["RiskLevelRI"].ToString();

    //        lblSeverityTextRI.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityRI"]));
    //        lblLikelihoodTextRI.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodRI"]));
    //        string ColorRI = GetCSSColor(lblR13RI.Text);
    //        if (Common.CastAsInt32(lblR13RI.Text) != 0)
    //            lblR13RI.Text = lblR13RI.Text + " - " + GetRisk(ColorRI);
    //        else
    //            lblR13RI.Text = "";
    //        rd_RlRI.Attributes.Add("class", ColorRI);
    //        lblRiskTextRI.Text = GetAction(ColorRI);

    //        txtACM.Text = dr["ADD_CONTROL_MEASURES"].ToString();

    //        //txtGRANo.Text = dr["GRVNo"].ToString();

    //        // For Peopel Final Risk
    //        if (!string.IsNullOrWhiteSpace(dr["Re_SeverityPF"].ToString()))
    //        {
    //            ddlReSeverityPF.SelectedValue = dr["Re_SeverityPF"].ToString();
    //        }
    //        if (!string.IsNullOrWhiteSpace(dr["Re_LikeliHoodPF"].ToString()))
    //        {
    //            ddlReLikelihoodPF.SelectedValue = dr["Re_LikeliHoodPF"].ToString();
    //        }
    //        lblReR13PF.Text = dr["Re_RiskLevelPF"].ToString();
    //        ibReSeverityPF.ToolTip = GetSeverityText(Common.CastAsInt32(dr["Re_SeverityPF"]));
    //        lblReLikelihoodTextPF.Text = GetLikelihoodText(Common.CastAsInt32(dr["Re_LikeliHoodPF"]));
    //        string Color = GetCSSColor(lblReR13PF.Text);
    //        if (Common.CastAsInt32(lblReR13PF.Text) != 0)
    //            lblReR13PF.Text = lblReR13PF.Text + " - " + GetRisk(Color);
    //        else
    //            lblReR13PF.Text = "";
    //        rd_ReR1PF.Attributes.Add("class", Color);
    //        lblReRiskTextPF.Text = GetAction(Color);

    //        // For Environment Final Risk
    //        if (!string.IsNullOrWhiteSpace(dr["Re_SeverityEF"].ToString()))
    //        {
    //            ddlReSeverityEF.SelectedValue = dr["Re_SeverityEF"].ToString();
    //        }
    //        if (!string.IsNullOrWhiteSpace(dr["Re_LikeliHoodEF"].ToString()))
    //        {
    //            ddlReLikelihoodEF.SelectedValue = dr["Re_LikeliHoodEF"].ToString();
    //        }
    //        lblReR13EF.Text = dr["Re_RiskLevelEF"].ToString();
    //        ibReSeverityEF.ToolTip = GetSeverityText(Common.CastAsInt32(dr["Re_SeverityEF"]));
    //        lblReLikelihoodTextEF.Text = GetLikelihoodText(Common.CastAsInt32(dr["Re_LikeliHoodEF"]));
    //        Color = GetCSSColor(lblReR13EF.Text);
    //        if (Common.CastAsInt32(lblReR13EF.Text) != 0)
    //            lblReR13EF.Text = lblReR13EF.Text + " - " + GetRisk(Color);
    //        else
    //            lblReR13EF.Text = "";
    //        rd_ReR1EF.Attributes.Add("class", Color);
    //        lblReRiskTextEF.Text = GetAction(Color);

    //        // For Assest Final Risk
    //        if (!string.IsNullOrWhiteSpace(dr["Re_SeverityAF"].ToString()))
    //        {
    //            ddlReSeverityAF.SelectedValue = dr["Re_SeverityAF"].ToString();
    //        }
    //        if (!string.IsNullOrWhiteSpace(dr["Re_LikeliHoodAF"].ToString()))
    //        {
    //            ddlReLikelihoodAF.SelectedValue = dr["Re_LikeliHoodAF"].ToString();
    //        }
    //        lblReR13AF.Text = dr["Re_RiskLevelAF"].ToString();
    //        ibReSeverityAF.ToolTip = GetSeverityText(Common.CastAsInt32(dr["Re_SeverityAF"]));
    //        lblReLikelihoodTextAF.Text = GetLikelihoodText(Common.CastAsInt32(dr["Re_LikeliHoodAF"]));
    //        Color = GetCSSColor(lblReR13AF.Text);
    //        if (Common.CastAsInt32(lblReR13AF.Text) != 0)
    //            lblReR13AF.Text = lblReR13AF.Text + " - " + GetRisk(Color);
    //        else
    //            lblReR13AF.Text = "";
    //        rd_ReR1AF.Attributes.Add("class", Color);
    //        lblReRiskTextAF.Text = GetAction(Color);

    //        // For Reputation Final Risk
    //        if (!string.IsNullOrWhiteSpace(dr["Re_SeverityRF"].ToString()))
    //        {
    //            ddlReSeverityRF.SelectedValue = dr["Re_SeverityRF"].ToString();
    //        }
    //        if (!string.IsNullOrWhiteSpace(dr["Re_LikeliHoodRF"].ToString()))
    //        {
    //            ddlReLikelihoodRF.SelectedValue = dr["Re_LikeliHoodRF"].ToString();
    //        }
    //        lblReR13RF.Text = dr["Re_RiskLevelRF"].ToString();
    //        ibReSeverityRF.ToolTip = GetSeverityText(Common.CastAsInt32(dr["Re_SeverityRF"]));
    //        lblReLikelihoodTextRF.Text = GetLikelihoodText(Common.CastAsInt32(dr["Re_LikeliHoodRF"]));
    //        Color = GetCSSColor(lblReR13RF.Text);
    //        if (Common.CastAsInt32(lblReR13RF.Text) != 0)
    //            lblReR13RF.Text = lblReR13RF.Text + " - " + GetRisk(Color);
    //        else
    //            lblReR13RF.Text = "";
    //        rd_ReR1RF.Attributes.Add("class", Color);
    //        lblReRiskTextRF.Text = GetAction(Color);

    //        rdoProceed_Y.Checked = (dr["Proceed"].ToString() == "Y");
    //        rdoProceed_N.Checked = (dr["Proceed"].ToString() == "N");
    //        //txtAgreedtime.Text = dr["AGREED_TIME"].ToString();
    //        txtPIC.Text = dr["PIC_NAME"].ToString();
    //        btnSaveSingle.Visible = true;
    //        //btnDeleteTask.Visible = true;
    //        dv_NewTask.Visible = true;
    //    }

    //}

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtEventDate.Text))
        {
            txtEventDate.Focus();
            msg1.ShowMessage("Please enter Event Date.", true);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtGrographicLocation.Text))
        {
            txtGrographicLocation.Focus();
            msg1.ShowMessage("Please enter Grographic Location.", true);
            return;
        }
        if (ddlPTWRequired.SelectedItem.Text == "< Select >")
        {
            ddlPTWRequired.Focus();
            msg1.ShowMessage("Please select PTW Required.", true);
            return;
        }
        if (ddlPTWRequired.SelectedIndex > 0)
        {
            if (ddlPTWRequired.SelectedValue == "Y" && ddlTypeoFPTW.SelectedItem.Text == "<Select>")
            {
                ddlTypeoFPTW.Focus();
                msg1.ShowMessage("Please select Type of PTW.", true);
                return;
            }
        }
        if (ddlPerRefTask.SelectedItem.Text == "< Select >")
        {
            ddlPerRefTask.Focus();
            msg1.ShowMessage("Please select Personnel required for the Task.", true);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtRiskDescr.Text.Trim()))
        {
            txtRiskDescr.Focus();
            msg1.ShowMessage("Please enter Risk Desc.", true);
            return;
        }

        if (ddlAlt.SelectedIndex == 0)
        {
            ddlAlt.Focus();
            msg1.ShowMessage("Please select Alternate methods of work considered.", true);
            return;
        }

        if (ddlAlt.SelectedValue.Trim() == "Y" && txtDetails.Text.Trim() == "")
        {
            txtDetails.Focus();
            msg1.ShowMessage("Please enter Details.", true);
            return;
        }

        if (txtCreatedBy.Text.Trim() == "")
        {
            txtCreatedBy.Focus();
            msg1.ShowMessage("Please enter Created By.", true);
            return;
        }
        if (txtHOD.Text.Trim() == "")
        {
            txtHOD.Focus();
            msg1.ShowMessage("Please enter HOD Name.", true);
            return;
        }
        if (ddlPosition.SelectedItem.Text == "< Select >")
        {
            ddlPosition.Focus();
            msg1.ShowMessage("Please select Position.", true);
            return;
        }
        if (txtSO.Text.Trim() == "")
        {
            txtSO.Focus();
            msg1.ShowMessage("Please enter Safety Officer Name.", true);
            return;
        }
        if (txtMaster.Text.Trim() == "")
        {
            txtMaster.Focus();
            msg1.ShowMessage("Please enter Master Name.", true);
            return;
        }
        TabPanel1.Enabled = false;
        TabPanel2.Enabled = true;
        TabPanel3.Enabled = false;

    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        TabPanel1.Enabled = true;
        TabPanel2.Enabled = false;
        TabPanel3.Enabled = false;
    }

    protected void btnNextTabPanel2_Click(object sender, EventArgs e)
    {
        TabPanel1.Enabled = false;
        TabPanel2.Enabled = false;
        TabPanel3.Enabled = true;
    }
    protected void btnPreviousTabPanel3_Click(object sender, EventArgs e)
    {
        TabPanel1.Enabled = false;
        TabPanel2.Enabled = true;
        TabPanel3.Enabled = false;
    }

protected void btnCloseVerified_Click(object sender, EventArgs e)
    {
        dvTaskVerification.Visible = false;
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        dvTaskVerification.Visible = true;
        txtVerifiedOn.Text = "";
        txtVerifiedBy.Text = "";
        ddlVerifiedRank.SelectedIndex = 0;

    }

    protected void btnSaveVerified_Click(object sender, EventArgs e)
    {
        try
        {
            if (RiskId <= 0)
            {
                return;
            }

            if (txtVerifiedBy.Text.Trim() == "")
            {
                txtVerifiedBy.Focus();
                lblVerifyMsg.Text = "Please enter Verified By. ";
                return;
            }
            if (txtVerifiedOn.Text.Trim() == "")
            {
                txtVerifiedOn.Focus();
                lblVerifyMsg.Text = "Please enter Verified On. ";
                return;
            }
            if (ddlVerifiedRank.SelectedIndex == 0)
            {
                ddlVerifiedRank.Focus();
                lblVerifyMsg.Text = "Please enter Verified Rank. ";
                return;
            }
            if (lblApprovalRequiredMsg.Text == ApprovalNotRequiredMsg)
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.RA_VSL_RISKMGMT_MASTER SET VerifiedBy='" + txtVerifiedBy.Text.Trim() + "',VerifiedRank='" + ddlVerifiedRank.SelectedItem.Text + "',VerifiedOn='" + txtVerifiedOn.Text.Trim() + "', Status = 'C' Where RISKID=" + RiskId.ToString());
                lblVerifyMsg.Text = "Risk Assessment Verified and Closed successfully";
            }
            else
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.RA_VSL_RISKMGMT_MASTER SET VerifiedBy='" + txtVerifiedBy.Text.Trim() + "',VerifiedRank='" + ddlVerifiedRank.SelectedItem.Text + "',VerifiedOn='" + txtVerifiedOn.Text.Trim() + "' Where RISKID=" + RiskId.ToString());
                lblVerifyMsg.Text = "Risk Assessment Verified successfully";
            }

            dvTaskVerification.Visible = false;
            btnSave.Visible = false;
            btnVerify.Visible = false;
          //  btnExport.Visible = true;
            btnPrint.Visible = true;
        }
        catch (Exception ex)
        {
            lblVerifyMsg.Text = ex.Message.ToString();
        }
    }
    protected void btnCloseRefresh_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "refreshparent();window.close();", true);
        //ScriptManager.RegisterStartupScript(Page,this.GetType(),"close", "<script language=javascript>refreshParent(); </ script > ", true);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "refreshParent();", true);
    }

    protected void ibCloseNewTask_Click(object sender, EventArgs e)
    {
        dv_NewTask.Visible = false;
    }

    protected void lbRAMatrix_Click(object sender, EventArgs e)
    {
        divRAMatrix.Visible = true;
    }

    protected void ibCloseRAMatrix_Click(object sender, ImageClickEventArgs e)
    {
        divRAMatrix.Visible = false;
    }

    protected void btnCloseRAMatrix_Click(object sender, EventArgs e)
    {
        divRAMatrix.Visible = false;
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (RiskId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "window.open('RAPrint.aspx?RId=" + RiskId + "&VSL=" + VesselCode + "','');", true);
        }
        else
        {
            msg1.ShowMessage("Data does not exist.", true);
        }
    }

    protected void btnSaveOfficeTab_Click(object sender, EventArgs e)
    {
        try
        {
            if ( string.IsNullOrEmpty(txtOfficeCommnets.Text.Trim()))
            {
                txtOfficeCommnets.Focus();
                lblOffCommentMsg.Text = "Please enter office Comments.";
                return;
            }
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.RA_VSL_RISKMGMT_MASTER SET OFFICE_COMMENTS='"+txtOfficeCommnets.Text.Trim()+ "',  OFFICECOMMENTBY='" + Session["UserName"].ToString() + "',COMMENTDATE=GETDATE() Where RISKID=" + RiskId.ToString());

            lblOffCommentMsg.Text = "Office Comments updated successfully";
            btnFinalSubmission.Visible = true;
        }
        catch(Exception ex)
        {
            lblOffCommentMsg.Text = ex.Message.ToString();
        }
    }
    protected void btnFinalSubmission_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtOfficeCommnets.Text.Trim()))
            {
                txtOfficeCommnets.Focus();
                lblOffCommentMsg.Text = "Please enter office Comments.";
                return;
            }

            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.RA_VSL_RISKMGMT_MASTER SET OFFICE_COMMENTS='" + txtOfficeCommnets.Text.Trim() + "',  OFFICECOMMENTBY='" + Session["UserName"].ToString() + "',COMMENTDATE=GETDATE(), Status='C' Where RISKID=" + RiskId.ToString());

            Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "refreshParent();", true);

        }
        catch (Exception ex)
        {
            lblOffCommentMsg.Text = ex.Message.ToString();
        }
    }
}