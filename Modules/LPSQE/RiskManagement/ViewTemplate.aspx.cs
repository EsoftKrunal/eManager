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

public partial class HSSQE_RiskManagement_ViewTemplate : System.Web.UI.Page
{
    public int TemplateId
    {
        set { ViewState["TemplateId"] = value; }
        get { return Common.CastAsInt32(ViewState["TemplateId"]); }
    }
    //public DataTable dtHazards
    //{
    //    get
    //    {
    //        object o = ViewState["dtHazards"];
    //        return (DataTable)o;
    //    }
    //    set
    //    {
    //        ViewState["dtHazards"] = value;
    //    }
    //}

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
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            TemplateId = Common.CastAsInt32(Request.QueryString["TemplateId"]);
            string TemplateCode = (Request.QueryString["TemplateCode"] == null ? "" : Request.QueryString["TemplateCode"].ToString());
            
            if (TemplateId > 0)
            {
                ShowMasterData();
            }
            
            if (TemplateCode.Trim() != "")
            {
                ShowHistoryData(TemplateCode);
            }
        }
    }
    public void ShowMasterData()
    {
        string SQL = "SELECT * FROM [dbo].[EV_TemplateMaster] WHERE TemplateId=" + TemplateId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblTempCode.Text = dt.Rows[0]["TemplateCode"].ToString();
            lblEventName.Text = dt.Rows[0]["EventName"].ToString();
            lblCreatedByOn.Text = dt.Rows[0]["CreatedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["CreatedOn"]);
            lblModifiedByOn.Text = dt.Rows[0]["ModifiedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["ModifiedOn"]);
            lblApprovedByOn.Text = dt.Rows[0]["ApprovedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["ApprovedOn"]);
            BindHazardsNew(Common.CastAsInt32(dt.Rows[0]["TemplateId"]));
        }
        else
        {
            btnModify.Visible = false;
        }
    } 
    public void ShowHistoryData(string TemplateCode)
    {
        string SQL = "SELECT * FROM [dbo].[EV_TemplateMaster] WHERE TemplateCode='" + TemplateCode + "' AND Status='H'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblTempCode.Text = dt.Rows[0]["TemplateCode"].ToString();
            lblEventName.Text = dt.Rows[0]["EventName"].ToString();
            lblCreatedByOn.Text = dt.Rows[0]["CreatedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["CreatedOn"]);
            lblModifiedByOn.Text = dt.Rows[0]["ModifiedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["ModifiedOn"]);
            lblApprovedByOn.Text = dt.Rows[0]["ApprovedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["ApprovedOn"]);
            BindHazardsNew(Common.CastAsInt32(dt.Rows[0]["TemplateId"]));
        }

        btnModify.Visible = false;
    } 
    public void BindHazardsNew(int TemplateId)
    {
        string SQL = "SELECT left(Hazardname ,50) as Hazardname1,*, 'A' As Status FROM [dbo].[RA_Template_Hazards] with(nolock) WHERE TemplateId=" + TemplateId; 
        dtTasksNew = Common.Execute_Procedures_Select_ByQuery("SELECT *,ConsequencesName as ConsequencesName1,left(ControlMeasures ,50) as ControlMeasures1,'A' As Status FROM [dbo].[RA_TemplateDetails] H with(nolock) WHERE H.TemplateId= " + TemplateId);

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptHazardsNew.DataSource = ((dt != null) ? dt : null);
        rptHazardsNew.DataBind();

        ShowExtResRisk();
    }
    //public DataTable BindHazards(int Task_TableId)
    //{
        ////string SQL = "SELECT *,left(Taskname ,50) as Taskname1,left(hazardname ,50) as HazardName1,left(ControlMeasures ,50) as ControlMeasures1,CSSCOLOR=Case when RiskLevel>=16 then 'r' when RiskLevel>=12 then 'a' when RiskLevel>=8 then 'b' else 'g' end FROM [dbo].[EV_Template_Tasks] T INNER JOIN [dbo].[EV_TemplateDetails] H  ON T.TemplateId=H.TemplateId AND T.Task_TableId=H.Task_TableId WHERE T.TemplateId=" + TemplateId;
        //string SQL = "SELECT *,left(hazardname ,50) as HazardName1,left(ControlMeasures ,50) as ControlMeasures1,CSSCOLOR=Case when RiskLevel>=16 then 'r' when RiskLevel>=12 then 'a' when RiskLevel>=8 then 'b' else 'g' end FROM [dbo].[EV_TemplateDetails] H WHERE H.TemplateId= " + TemplateId + " AND H.Task_TableId = " + Task_TableId;       
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //return dt;
        ////rptTaskHazards.DataSource = ((dt != null) ? dt : null);
        //rptTaskHazards.DataBind();

        //DataView dv = dtHazards.DefaultView;
        //dv.RowFilter = "Task_TableId=" + Task_TableId;
        //return dv.ToTable();
    //}

    public DataTable BindTasksNew(int Hazard_TableId)
    {
        DataView dv = dtTasksNew.DefaultView;
        dv.RowFilter = "Status='A' AND Hazard_TableId=" + Hazard_TableId;
        return dv.ToTable();
    }
    //public void ShowExtResRisk()
    //{
    //    object RiskLevel = dtTasksNew.Compute("MAX(RiskLevelPI)", "");
    //    string ExtColor = GetCSSColor(RiskLevel);
    //    imgER.ImageUrl = "~/Modules/HRD/Images/" + ExtColor + ".png";
    //    lblExtAction.Text = GetAction(ExtColor);

    //    object Re_RiskLevel = dtTasksNew.Compute("MAX(Re_RiskLevelPF)", "");
    //    string ResColor = GetCSSColor(Re_RiskLevel);
    //    if (ResColor.Trim() != "")
    //    {
    //        imgRR.Visible = true;
    //        imgRR.ImageUrl = "~/Modules/HRD/Images/" + ResColor + ".png";
    //    }
    //    else
    //    {
    //        imgRR.Visible = false;
    //    }

    //    lblResAction.Text = GetAction(ResColor);
    //} 

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
    protected void btnModify_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditTemplate.aspx?TC=" + lblTempCode.Text);
    }
    protected void btnViewHazard_Click(object sender, EventArgs e)
    {
        int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT TD.*, (SELECT (ISNULL(HazardCode, '') + ' : ' + HazardName) FROM [dbo].[RA_Template_Hazards] WHERE Hazard_TableId = TD.Hazard_TableId) AS Hazard FROM [dbo].[RA_TemplateDetails] TD WHERE TableId = " + _TableId;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if(dt!=null && dt.Rows.Count > 0)
        {
            lblTask.Text = dt.Rows[0]["Hazard"].ToString();
            lblHazard.Text = dt.Rows[0]["ConsequencesCode"].ToString() + " : " + dt.Rows[0]["ConsequencesName"].ToString();
            txtStdCM.Text = dt.Rows[0]["ControlMeasures"].ToString();
            lblR11PI.Text = GetSeverity(Common.CastAsInt32(dt.Rows[0]["SeverityPI"]));
            lblR12PI.Text = GetLikelihood(Common.CastAsInt32(dt.Rows[0]["LikeliHoodPI"]));
            lblR13PI.Text = dt.Rows[0]["RiskLevelPI"].ToString();
            imgbtnSeveritypi.ToolTip = GetSeverityText(Common.CastAsInt32(dt.Rows[0]["SeverityPI"]));
            lblLikelihoodTextPI.Text = GetLikelihoodText(Common.CastAsInt32(dt.Rows[0]["LikeliHoodPI"]));
            string Color = GetCSSColor(lblR13PI.Text);
            rd_RlPI.Attributes.Add("class", Color);
            if (Common.CastAsInt32(lblR13PI.Text) != 0)
                lblR13PI.Text = lblR13PI.Text + " - " + GetRisk(Color);
            else
                lblR13PI.Text = "";
            lblRiskTextPI.Text = GetAction(Color);

            lblR11EI.Text = GetSeverity(Common.CastAsInt32(dt.Rows[0]["SeverityEI"]));
            lblR12EI.Text = GetLikelihood(Common.CastAsInt32(dt.Rows[0]["LikeliHoodEI"]));
            lblR13EI.Text = dt.Rows[0]["RiskLevelEI"].ToString();
            imgbtnSeverityEI.ToolTip = GetSeverityText(Common.CastAsInt32(dt.Rows[0]["SeverityEI"]));
            lblLikelihoodTextEI.Text = GetLikelihoodText(Common.CastAsInt32(dt.Rows[0]["LikeliHoodEI"]));
            Color = GetCSSColor(lblR13EI.Text);
            rd_RlEI.Attributes.Add("class", Color);
            if (Common.CastAsInt32(lblR13EI.Text) != 0)
                lblR13EI.Text = lblR13EI.Text + " - " + GetRisk(Color);
            else
                lblR13EI.Text = "";
            lblRiskTextEI.Text = GetAction(Color);

            lblR11AI.Text = GetSeverity(Common.CastAsInt32(dt.Rows[0]["SeverityAI"]));
            lblR12AI.Text = GetLikelihood(Common.CastAsInt32(dt.Rows[0]["LikeliHoodAI"]));
            lblR13AI.Text = dt.Rows[0]["RiskLevelAI"].ToString();
            imgbtnSeverityAI.ToolTip = GetSeverityText(Common.CastAsInt32(dt.Rows[0]["SeverityAI"]));
            lblLikelihoodTextAI.Text = GetLikelihoodText(Common.CastAsInt32(dt.Rows[0]["LikeliHoodAI"]));
            Color = GetCSSColor(lblR13AI.Text);
            rd_RlAI.Attributes.Add("class", Color);
            if (Common.CastAsInt32(lblR13AI.Text) != 0)
                lblR13AI.Text = lblR13AI.Text + " - " + GetRisk(Color);
            else
                lblR13AI.Text = "";
            lblRiskTextAI.Text = GetAction(Color);

            lblR11RI.Text = GetSeverity(Common.CastAsInt32(dt.Rows[0]["SeverityRI"]));
            lblR12RI.Text = GetLikelihood(Common.CastAsInt32(dt.Rows[0]["LikeliHoodRI"]));
            lblR13RI.Text = dt.Rows[0]["RiskLevelRI"].ToString();
            imgbtnSeverityRI.ToolTip = GetSeverityText(Common.CastAsInt32(dt.Rows[0]["SeverityRI"]));
            lblLikelihoodTextRI.Text = GetLikelihoodText(Common.CastAsInt32(dt.Rows[0]["LikeliHoodRI"]));
            Color = GetCSSColor(lblR13RI.Text);
            rd_RlRI.Attributes.Add("class", Color);
            if (Common.CastAsInt32(lblR13RI.Text) != 0)
                lblR13RI.Text = lblR13RI.Text + " - " + GetRisk(Color);
            else
                lblR13RI.Text = "";
            lblRiskTextRI.Text = GetAction(Color);

            txtACM.Text = dt.Rows[0]["ADD_CONTROL_MEASURES"].ToString();

            lblReR11PF.Text = GetSeverity(Common.CastAsInt32(dt.Rows[0]["Re_SeverityPF"]));
            lblReR12PF.Text = GetLikelihood(Common.CastAsInt32(dt.Rows[0]["Re_LikeliHoodPF"]));
            lblReR13PF.Text = dt.Rows[0]["Re_RiskLevelPF"].ToString();
            ibReSeverityPF.ToolTip = GetSeverityText(Common.CastAsInt32(dt.Rows[0]["Re_SeverityPF"]));
            lblReLikelihoodTextPF.Text = GetLikelihoodText(Common.CastAsInt32(dt.Rows[0]["Re_LikeliHoodPF"]));
            Color = GetCSSColor(lblReR13PF.Text);
            rd_ReR1PF.Attributes.Add("class", Color);
            if (Common.CastAsInt32(lblReR13PF.Text) != 0)
                lblReR13PF.Text = lblReR13PF.Text + " - " + GetRisk(Color);
            else
                lblReR13PF.Text = "";
            lblReRiskTextPF.Text = GetAction(Color);

            lblReR11EF.Text = GetSeverity(Common.CastAsInt32(dt.Rows[0]["Re_SeverityEF"]));
            lblReR12EF.Text = GetLikelihood(Common.CastAsInt32(dt.Rows[0]["Re_LikeliHoodEF"]));
            lblReR13EF.Text = dt.Rows[0]["Re_RiskLevelEF"].ToString();
            ibReSeverityEF.ToolTip = GetSeverityText(Common.CastAsInt32(dt.Rows[0]["Re_SeverityEF"]));
            lblReLikelihoodTextEF.Text = GetLikelihoodText(Common.CastAsInt32(dt.Rows[0]["Re_LikeliHoodEF"]));
            Color = GetCSSColor(lblReR13EF.Text);
            rd_ReR1EF.Attributes.Add("class", Color);
            if (Common.CastAsInt32(lblReR13EF.Text) != 0)
                lblReR13EF.Text = lblReR13EF.Text + " - " + GetRisk(Color);
            else
                lblReR13EF.Text = "";
            lblReRiskTextEF.Text = GetAction(Color);

            lblReR11AF.Text = GetSeverity(Common.CastAsInt32(dt.Rows[0]["Re_SeverityAF"]));
            lblReR12AF.Text = GetLikelihood(Common.CastAsInt32(dt.Rows[0]["Re_LikeliHoodAF"]));
            lblReR13AF.Text = dt.Rows[0]["Re_RiskLevelAF"].ToString();
            ibReSeverityAF.ToolTip = GetSeverityText(Common.CastAsInt32(dt.Rows[0]["Re_SeverityAF"]));
            lblReLikelihoodTextAF.Text = GetLikelihoodText(Common.CastAsInt32(dt.Rows[0]["Re_LikeliHoodAF"]));
            Color = GetCSSColor(lblReR13AF.Text);
            rd_ReR1AF.Attributes.Add("class", Color);
            if (Common.CastAsInt32(lblReR13AF.Text) != 0)
                lblReR13AF.Text = lblReR13AF.Text + " - " + GetRisk(Color);
            else
                lblReR13AF.Text = "";
            lblReRiskTextAF.Text = GetAction(Color);

            lblReR11RF.Text = GetSeverity(Common.CastAsInt32(dt.Rows[0]["Re_SeverityRF"]));
            lblReR12RF.Text = GetLikelihood(Common.CastAsInt32(dt.Rows[0]["Re_LikeliHoodRF"]));
            lblReR13RF.Text = dt.Rows[0]["Re_RiskLevelRF"].ToString();
            ibReSeverityRF.ToolTip = GetSeverityText(Common.CastAsInt32(dt.Rows[0]["Re_SeverityRF"]));
            lblReLikelihoodTextRF.Text = GetLikelihoodText(Common.CastAsInt32(dt.Rows[0]["Re_LikeliHoodRF"]));
            Color = GetCSSColor(lblReR13RF.Text);
            rd_ReR1AF.Attributes.Add("class", Color);
            if (Common.CastAsInt32(lblReR13RF.Text) != 0)
                lblReR13RF.Text = lblReR13RF.Text + " - " + GetRisk(Color);
            else
                lblReR13RF.Text = "";
            lblReRiskTextRF.Text = GetAction(Color);


            dv_NewTask.Visible = true;
        }

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
    public string GetSeverity(int Severity)
    {
        string Text = "";

        switch (Severity)
        {
            case 1: Text = " 1 - Negligible ";
                break;
            case 2: Text = " 2 - Minor ";
                break;
            case 3: Text = " 3 - Major ";
                break;
            case 4: Text = " 4 - Severe ";
                break;
            case 5: Text = " 5 - Catastropic ";
                break;
            default: Text = "";
                break;

        }

        return Text;
    }
    public string GetLikelihood(int Likelihood)
    {
        string Text = "";

        switch (Likelihood)
        {
            case 1: Text = " 1 - Almost Nil Chances ";
                break;
            case 2: Text = " 2 - Highly Unlikely ";
                break;
            case 3: Text = " 3 - Unlikely ";
                break;
            case 4: Text = " 4 - Likely ";
                break;
            case 5: Text = " 5 - Very Likely ";
                break;
            default: Text = "";
                break;

        }

        return Text;
    }
    public string GetSeverityText(int Severity)
    {
        string Text = "";

        switch (Severity)
        {
            case 1: Text = "* No effect on reputation\n* Negligible economic loss which can be restored \n* Nill to sea : contained onboard";
                break;
            case 2: Text = "* Small reduction of reputation in the short run\n* Economic loss upto US$10,000 which can be restored \n* Sheen on sea : evidance of loss to sea";
                break;
            case 3: Text = "* Reduction of reputation that may influence trust and respect\n* Economic loss between US$10,000 and US$100,000 which can be restored \n* Less than 1 m3 to sea";
                break;
            case 4: Text = "* Serious loss of reputation that will influence trust and respect for a long time\n* Lagre economic loss more than US$100,000 that can be restored \n* 1 to 5 m3 to sea";
                break;
            case 5: Text = "* Serious loss of reputation which is devastating for trust and respect\n* Considerable economic loss which can not be restored\n* More than 5 m3 to sea   ";
                break;
            default: Text = "";
                break;

        }

        return Text;
    }
    public string GetLikelihoodText(int Likelihood)
    {
        string Text = "";

        switch (Likelihood)
        {
            case 1: Text = " Never heard within the industry ";
                break;
            case 2: Text = " Occurs less than 0.1% of the time/ cases ";
                break;
            case 3: Text = " Occurs between 0.1% and 1% of the time/ cases ";
                break;
            case 4: Text = " Occurs between 1% and 10% of the time/ cases ";
                break;
            case 5: Text = " More frequently than 10% of the time/ cases ";
                break;
            default: Text = "";
                break;

        }

        return Text;
    }
    public string GetAction(string Color)
    {
        string Action = "";

        if (Color == "")
        {
            return Action;
        }

        if (Color == "r")
        {
            Action = "Do not undertake task.If operation is already in progress, abort and inform office.";
        }
        else if (Color == "a")
        {
            Action = "Job to be under taken only with office Approval.";
        }
        else if (Color == "b")
        {
            Action = "Job can be under taken by ship staff with direct supervision of Master and/ or Chief Engineer.";
        }
        else
        {
            Action = "Job can be under taken by ship staff.";
        }

        return Action;
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
    protected void btnCancelHazard_Click(object sender, EventArgs e)
    {
        lblTask.Text = "";
        lblHazard.Text = "";
        txtStdCM.Text = "";

        lblR11PI.Text = "";
        lblR12PI.Text = "";
        lblR13PI.Text = "";
        lblRiskTextPI.Text = "";

        lblR11EI.Text = "";
        lblR12EI.Text = "";
        lblR13EI.Text = "";
        lblRiskTextEI.Text = "";

        lblR11AI.Text = "";
        lblR12AI.Text = "";
        lblR13AI.Text = "";
        lblRiskTextAI.Text = "";

        lblR11RI.Text = "";
        lblR12RI.Text = "";
        lblR13RI.Text = "";
        lblRiskTextRI.Text = "";


        txtACM.Text = "";

        lblReR11PF.Text = "";
        lblReR12PF.Text = "";
        lblReR13PF.Text = "";
        lblReRiskTextPF.Text = "";

        lblReR11EF.Text = "";
        lblReR12EF.Text = "";
        lblReR13EF.Text = "";
        lblReRiskTextEF.Text = "";

        lblReR11AF.Text = "";
        lblReR12AF.Text = "";
        lblReR13AF.Text = "";
        lblReRiskTextAF.Text = "";

        lblReR11RF.Text = "";
        lblReR12RF.Text = "";
        lblReR13RF.Text = "";
        lblReRiskTextRF.Text = "";

        dv_NewTask.Visible = false;
    }
}