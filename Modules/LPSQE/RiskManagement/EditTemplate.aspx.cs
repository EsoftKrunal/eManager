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

public partial class HSSQE_RiskManagement_EditTemplate : System.Web.UI.Page
{
    public int EventId
    {
        set { ViewState["EventId"] = value; }
        get { return Common.CastAsInt32(ViewState["EventId"]); }
    }
    public int TemplateId
    {
        set { ViewState["TemplateId"] = value; }
        get { return Common.CastAsInt32(ViewState["TemplateId"]); }
    }
    public string UserName
    {
        set { ViewState["UserName"] = value; }
        get { return ViewState["UserName"].ToString(); }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text = "";
        lblMess.Text = "";

        if (!IsPostBack)
        {
           ViewState["HazardTableId"]="0";
           UserName = Session["UserName"].ToString();
            //ddlTask.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT ConsequencesId,ConsequencesCode + ' : ' + ConsequencesName AS ConsequencesName FROM DBO.RA_ConsequencesMaster with(nolock) ORDER BY ConsequencesCode + ' : ' + ConsequencesName");
            //ddlTask.DataTextField = "ConsequencesName";
            //ddlTask.DataValueField = "ConsequencesId";
            //ddlTask.DataBind();
            //ddlTask.Items.Insert(0, new ListItem("< Select > ", ""));

            CreateTables();
           ShowMasterDetails(); 
        }
    }
    public void ShowMasterDetails()
    {
        string TempCode = Convert.ToString(Request.QueryString["TC"]);
        string SQL = "SELECT * FROM [dbo].[EV_TemplateMaster] WHERE TemplateCode='" + TempCode + "' AND Status = 'P'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            TemplateId = Common.CastAsInt32(dt.Rows[0]["TemplateId"]);
            
            lblTempCode.Text = dt.Rows[0]["TemplateCode"].ToString();
            EventId = Common.CastAsInt32(dt.Rows[0]["EventId"]);
            lblEventName.Text = dt.Rows[0]["EventCode"].ToString() + " : " + dt.Rows[0]["EventName"].ToString();
            lblCreatedByOn.Text = dt.Rows[0]["CreatedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["CreatedOn"]);
            lblModifiedByOn.Text = dt.Rows[0]["ModifiedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["ModifiedOn"]);
            lblApprovedByOn.Text = dt.Rows[0]["ApprovedBy"].ToString() + "/ " + Common.ToDateString(dt.Rows[0]["ApprovedOn"]);
            dtHazardsNew = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[RA_Template_Hazards] WHERE TemplateId=" + TemplateId );
            dtTasksNew = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[RA_TemplateDetails] WHERE TemplateId=" + TemplateId);
            BindHazardsNew();            
            btnSave.Visible = (Convert.IsDBNull(dt.Rows[0]["ApprovedOn"]));
            btnApproveTemplate.Visible = (!Convert.IsDBNull(dt.Rows[0]["RequestedOn"]) && Convert.IsDBNull(dt.Rows[0]["ApprovedOn"]));
            btnRequestApproval.Visible = !btnApproveTemplate.Visible;
        }
        else
        {
            btnApproveTemplate.Visible = false;

            EventId = Common.CastAsInt32(Request.QueryString["EventId"]);
            if (EventId > 0)
            {
                SQL = "SELECT * FROM [dbo].[EV_EVENTMASTER] WHERE EventId=" + EventId;
                dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblTempCode.Text = "";
                    lblEventName.Text = dt.Rows[0]["EventCode"].ToString() + " : " + dt.Rows[0]["EventName"].ToString();
                }
            }
            else if (TempCode != "")
            {
                SQL = "SELECT * FROM [dbo].[EV_TemplateMaster] WHERE TemplateCode='" + TempCode + "' AND Status = 'A'";
                dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblTempCode.Text = TempCode;
                    EventId = Common.CastAsInt32(dt.Rows[0]["EventId"]);
                    lblEventName.Text = dt.Rows[0]["EventCode"].ToString() + " : " + dt.Rows[0]["EventName"].ToString();
                    //---------------------------------
                    int ActiveTemplateId = Common.CastAsInt32(dt.Rows[0]["TemplateId"]);
                    DataTable _dtHazards = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[RA_Template_Hazards] WHERE TemplateId=" + ActiveTemplateId);
                    DataTable _dtTasks = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[RA_TemplateDetails] WHERE TemplateId=" + ActiveTemplateId);
                    AddHazardFromDB(_dtHazards, _dtTasks);
                    BindHazardsNew();

                    //---------------------------------
                }
            }
            else
            {
                btnSave.Visible = false;
            }
        }
    }
    public void CreateTables()
    {
        dtHazardsNew = new DataTable();

        dtHazardsNew.Columns.Add("Hazard_TableId", typeof(int));
        dtHazardsNew.Columns.Add("TemplateId", typeof(int));
        dtHazardsNew.Columns.Add("HazardId", typeof(int));
        dtHazardsNew.Columns.Add("HazardCode", typeof(string));
        dtHazardsNew.Columns.Add("HazardName", typeof(string));
        dtHazardsNew.Columns.Add("Status", typeof(string));


        dtHazardsNew.AcceptChanges();

        dtTasksNew = new DataTable();

        dtTasksNew.Columns.Add("TableId", typeof(int));
        dtTasksNew.Columns.Add("TemplateId", typeof(int));
        dtTasksNew.Columns.Add("Hazard_TableId", typeof(int));
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
    //public void BindTasks()
    //{
    //    DataView dv = dtTasks.DefaultView;
    //    dv.RowFilter = "Status='A'";
    //    rptTasks.DataSource = dv.ToTable();
    //    rptTasks.DataBind();

    //    ShowExtResRisk();
    //}
    public void BindHazardsNew()
    {
        DataView dv = dtHazardsNew.DefaultView;
        dv.RowFilter = "Status='A' ";
        //dv.Sort = "HazardName";
        rptHazardsNew.DataSource = dv.ToTable();
        rptHazardsNew.DataBind();

        ShowExtResRisk();
    }
    public DataTable BindTasksNew(int Hazard_TableId)
    {
        DataView dv = dtTasksNew.DefaultView;
        dv.RowFilter = "Status='A' AND Hazard_TableId=" + Hazard_TableId;
        return dv.ToTable();
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

        object RiskLevel = dtRiskLevel.Compute("MAX(RiskLevel)","");
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
    public string GetTaskName(object TableId)
    {
        string ret = "";
        DataRow[] drs= dtTasksNew.Select("TableId=" + TableId);
        if (drs.Length > 0)
        {
            drs = dtTasksNew.Select("Hazard_TableId=" + drs[0]["Hazard_TableId"].ToString());
            if (drs.Length > 0)
                ret = drs[0]["ConsequencesName"].ToString();
        }
        return ret;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //------------------------------------
        if (("" + Request.QueryString["TC"]) == "")
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.EV_TemplateMaster WHERE EVENTID=" + EventId + " and TemplateId<>" + TemplateId);
            if (dt.Rows.Count > 0)
            {
                lblMsg.Text = "Template already exists for same event.";
                return;
            }
        }
        //------------------------------------
        try
        {
            Common.Set_Procedures("[dbo].[EV_InsertUpdateTemplateMaster]");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
               new MyParameter("@TemplateId", TemplateId),
               new MyParameter("@TemplateCode", lblTempCode.Text.Trim()),
               new MyParameter("@EventId", EventId),
               new MyParameter("@EventCode", lblEventName.Text.Split(':').GetValue(0).ToString().Trim()),
               new MyParameter("@EventName", lblEventName.Text.Split(':').GetValue(1).ToString().Trim()),
               new MyParameter("@ModifiedBy", UserName),
               new MyParameter("@ModifiedOn", DateTime.Today.Date.ToString("dd-MMM-yyyy")),
               new MyParameter("@Status", "")

               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                TemplateId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);

                foreach (DataRow dr in dtHazardsNew.Rows)
                {
                    Common.Set_Procedures("[dbo].[RA_InsertUpdateTemplateHazards]");
                    Common.Set_ParameterLength(6);
                    Common.Set_Parameters(
                       new MyParameter("@Hazard_TableId", Common.CastAsInt32(dr["Hazard_TableId"])),
                       new MyParameter("@TemplateId", TemplateId),
                       new MyParameter("@HazardId", Common.CastAsInt32(dr["HazardId"])),
                       new MyParameter("@HazardCode", dr["HazardCode"].ToString()),
                       new MyParameter("@HazardName", dr["HazardName"].ToString()),
                       new MyParameter("@Status", dr["Status"].ToString())
                       );
                    DataSet ds1 = new DataSet();
                    Boolean res1;
                    res1 = Common.Execute_Procedures_IUD(ds1);

                    if (res1)
                    {
                        if (dr["Status"].ToString() == "A")
                        {
                            int Task_TableId = Common.CastAsInt32(ds1.Tables[0].Rows[0][0]);

                            DataView dv = dtTasksNew.DefaultView;
                            dv.RowFilter = "TemplateId=" + dr["TemplateId"] + " AND Hazard_TableId=" + dr["Hazard_TableId"];
                            foreach (DataRow dr1 in dv.ToTable().Rows)
                            {
                                Common.Set_Procedures("[dbo].[RA_InsertUpdateTemplateDetails]");
                                Common.Set_ParameterLength(36);
                                Common.Set_Parameters(
                                   new MyParameter("@TableId", Common.CastAsInt32(dr1["TableId"])),
                                   new MyParameter("@TemplateId", TemplateId),
                                   new MyParameter("@Hazard_TableId", Task_TableId),
                                   new MyParameter("@ConsequencesId", Common.CastAsInt32(dr1["ConsequencesId"])),
                                   new MyParameter("@ConsequencesCode", dr1["ConsequencesCode"].ToString()),
                                   new MyParameter("@ConsequencesName", dr1["ConsequencesName"].ToString()),
                                   new MyParameter("@ControlMeasures", dr1["ControlMeasures"].ToString()),
                                   new MyParameter("@SeverityPI", Common.CastAsInt32(dr1["SeverityPI"])),
                                   new MyParameter("@LikeliHoodPI", Common.CastAsInt32(dr1["LikeliHoodPI"])),
                                   new MyParameter("@RiskLevelPI", Common.CastAsInt32(dr1["RiskLevelPI"])),
                                   new MyParameter("@SeverityEI", Common.CastAsInt32(dr1["SeverityEI"])),
                                   new MyParameter("@LikeliHoodEI", Common.CastAsInt32(dr1["LikeliHoodEI"])),
                                   new MyParameter("@RiskLevelEI", Common.CastAsInt32(dr1["RiskLevelEI"])),
                                   new MyParameter("@SeverityAI", Common.CastAsInt32(dr1["SeverityAI"])),
                                   new MyParameter("@LikeliHoodAI", Common.CastAsInt32(dr1["LikeliHoodAI"])),
                                   new MyParameter("@RiskLevelAI", Common.CastAsInt32(dr1["RiskLevelAI"])),
                                   new MyParameter("@SeverityRI", Common.CastAsInt32(dr1["SeverityRI"])),
                                   new MyParameter("@LikeliHoodRI", Common.CastAsInt32(dr1["LikeliHoodRI"])),
                                   new MyParameter("@RiskLevelRI", Common.CastAsInt32(dr1["RiskLevelRI"])),
                                   new MyParameter("@ADD_CONTROL_MEASURES", dr1["ADD_CONTROL_MEASURES"].ToString()),
                                   new MyParameter("@Re_SeverityPF", Common.CastAsInt32(dr1["Re_SeverityPF"])),
                                   new MyParameter("@Re_LikeliHoodPF", Common.CastAsInt32(dr1["Re_LikeliHoodPF"])),
                                   new MyParameter("@Re_RiskLevelPF", Common.CastAsInt32(dr1["Re_RiskLevelPF"])),
                                    new MyParameter("@Re_SeverityEF", Common.CastAsInt32(dr1["Re_SeverityEF"])),
                                   new MyParameter("@Re_LikeliHoodEF", Common.CastAsInt32(dr1["Re_LikeliHoodEF"])),
                                   new MyParameter("@Re_RiskLevelEF", Common.CastAsInt32(dr1["Re_RiskLevelEF"])),
                                    new MyParameter("@Re_SeverityAF", Common.CastAsInt32(dr1["Re_SeverityAF"])),
                                   new MyParameter("@Re_LikeliHoodAF", Common.CastAsInt32(dr1["Re_LikeliHoodAF"])),
                                   new MyParameter("@Re_RiskLevelAF", Common.CastAsInt32(dr1["Re_RiskLevelAF"])),
                                    new MyParameter("@Re_SeverityRF", Common.CastAsInt32(dr1["Re_SeverityRF"])),
                                   new MyParameter("@Re_LikeliHoodRF", Common.CastAsInt32(dr1["Re_LikeliHoodRF"])),
                                   new MyParameter("@Re_RiskLevelRF", Common.CastAsInt32(dr1["Re_RiskLevelRF"])),
                                 //  new MyParameter("@GRVNo", Common.CastAsInt32(dr1["GRVNo"])),
                                   new MyParameter("@Proceed", dr1["Proceed"].ToString()),
                                   new MyParameter("@AGREED_TIME", dr1["AGREED_TIME"].ToString()),
                                   new MyParameter("@PIC_NAME", dr1["PIC_NAME"].ToString()),
                                   new MyParameter("@Status", dr1["Status"].ToString())
                                   );
                                DataSet ds2 = new DataSet();
                                Boolean res2;
                                res2 = Common.Execute_Procedures_IUD(ds2);
                            }
                        }
                    }
                }

                BindHazardsNew();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "refresh", "RefreshParent();", true);
                lblMsg.Text = "Template added/ updated successfully.";
            }
            else
            {
                lblMsg.Text = "Unable to add/ update template.Error : " + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = "Unable to add/ update template.Error : " + ex.Message.ToString();
        }
    }
    //protected void btnSelectTask_Click(object sender, EventArgs e)
    //{
    //    int TaskId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    string TaskName = ((ImageButton)sender).ToolTip;
    //    try
    //    {
    //        DataView dv = dtTasks.DefaultView;
    //        dv.RowFilter = "TemplateId=" + TemplateId + " AND TaskId=" + TaskId + " AND Status='A'";
    //        if (dv.ToTable().Rows.Count > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please Check! Task already exists.');", true);
    //            return;
    //        }

    //        DataRow dr = dtTasks.NewRow();
    //        int Key= -(Common.CastAsInt32(dtTasks.Rows.Count));
    //        dr["Task_TableId"] = Key;
    //        dr["TemplateId"] = TemplateId;
    //        dr["TaskId"] = TaskId;
    //        dr["TaskName"] = TaskName;
    //        dr["Status"] = "A";

    //        dtTasks.Rows.Add(dr);
    //        dtTasks.AcceptChanges();
    //        BindTasks();
    //        dvNewTask.Visible = false;
    //        //----
    //        ViewState["TaskTableId"] = Key;
    //        ShowAddHazard();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = "Unable to add. Error : " + ex.Message.ToString();
    //    }

    //}
    //protected void btnAddTask_Click(object sender, EventArgs e)
    //{
    //    rptAddTasks.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[EV_TaskMaster] ORDER BY TaskName");
    //    rptAddTasks.DataBind();
    //    dvNewTask.Visible = true;
    //}
    protected void btnCancelNew_Click(object sender, EventArgs e)
    {
        dvNewHazard.Visible = false;
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
  
    protected void btnDeleteHazard_Click(object sender, EventArgs e)
    {
        int _Hazard_TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataRow[] drs=dtHazardsNew.Select("Hazard_TableId=" + _Hazard_TableId);
        foreach (DataRow dr in drs)
        {
            dr["Status"] = "D";
            DataRow[] drs1 = dtTasksNew.Select("Hazard_TableId=" + dr["Hazard_TableId"].ToString());
            foreach (DataRow dr1 in drs)
            {
                dr1["Status"] = "D";
            }
        }
        BindHazardsNew();
    }
    protected void btnDeleteTask_Click(object sender, EventArgs e)
    {
        int _TableId = Common.CastAsInt32(((Button)sender).CommandArgument);
        int Task_TableId = 0;
        DataRow[] drs1 = dtTasksNew.Select("TableId=" + _TableId);
        foreach (DataRow dr1 in drs1)
        {
            Task_TableId = Common.CastAsInt32(dr1["Hazard_TableId"]);
            dr1["Status"] = "D";
        }

        DataView dv = dtTasksNew.DefaultView;
        dv.RowFilter = "Hazard_TableId=" + Task_TableId + " AND Status='A'";

        if (dv.ToTable().Rows.Count <= 0)
        {
            DataRow[] drs = dtHazardsNew.Select("Hazard_TableId=" + Task_TableId);

            foreach (DataRow dr in drs)
            {
                dr["Status"] = "D";
            }
        }

        BindHazardsNew();

        dv_NewTask.Visible = false;
    }
    //protected void btnSelectTask_Click(object sender, EventArgs e)
    //{
    //    dv_NewTask.Visible = false;
    //    int TaskId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    string TaskName = ((ImageButton)sender).Attributes["TaskName"].ToString();

    //    try
    //    {
    //        DataView dv = dtTasks.DefaultView;
    //        dv.RowFilter = "TemplateId=" + TemplateId + " AND Task_TableId=" + Common.CastAsInt32(hfdTaskId.Text) + " AND TaskId=" + TaskId + " AND Status='A'";
    //        if (dv.ToTable().Rows.Count > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please Check! Consequences already exists.');", true);
    //            return;
    //        }

    //        DataRow dr = dtHazards.NewRow();
    //        dr["TableId"] = -(Common.CastAsInt32(dtHazards.Rows.Count));
    //        dr["TemplateId"] = TemplateId;
    //        dr["Task_TableId"] = Common.CastAsInt32(hfdTaskId.Text);
    //        dr["TaskId"] = TaskId;
    //        dr["TaskName"] = TaskName;
    //        dr["ControlMeasures"] = "";
    //        dr["Severity"] = DBNull.Value;
    //        dr["LikeliHood"] = DBNull.Value;
    //        dr["RiskLevel"] = DBNull.Value;
    //        dr["ADD_CONTROL_MEASURES"] = "";
    //        dr["Re_Severity"] = DBNull.Value;
    //        dr["Re_LikeliHood"] = DBNull.Value;
    //        dr["Re_RiskLevel"] = DBNull.Value;
    //        dr["Status"] = "A";

    //        dtTasks.Rows.Add(dr);
    //        dtTasks.AcceptChanges();

    //        BindTasks();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = "Unable to add. Error : " + ex.Message.ToString();
    //    }

    //}
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
        btnDeleteTask.Visible = false;

        dv_NewTask.Visible = false;
    }
    protected void btnEditTask_Click(object sender, EventArgs e)
    {
        hfdTaskIdNew.Value = ((ImageButton)sender).CommandArgument.Trim();
        int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        btnDeleteTask.CommandArgument = hfdTaskIdNew.Value;
        DataRow[] drs = dtTasksNew.Select("TableId=" + _TableId);
        foreach (DataRow dr in drs)
        {
            DataView dv = dtHazardsNew.DefaultView;
            dv.RowFilter = "Hazard_TableId=" + dr["Hazard_TableId"];
            hfdTaskId.Text= dr["ConsequencesId"].ToString(); ;
             txtTaskname.Text = getTaskName(hfdTaskId.Text);
             txtTaskname.Enabled = false;
            //ddlTask.SelectedValue = dr["ConsequencesId"].ToString();
            //ddlTask.Enabled = false;

            txtStdCM.Text = dr["ControlMeasures"].ToString();

            // for Person
            ddlSeveritypi.SelectedValue = dr["SeverityPI"].ToString();
            ddlLikelihoodpi.SelectedValue = dr["LikeliHoodPI"].ToString();
            lblR13pi.Text = dr["RiskLevelPI"].ToString();

            imgbtnSeveritypi.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityPI"]));
            lblLikelihoodTextpi.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodPI"]));
            string ColorPI = GetCSSColor(lblR13pi.Text);
            if (Common.CastAsInt32(lblR13pi.Text) != 0)
                lblR13pi.Text = lblR13pi.Text + " - " + GetRisk(ColorPI);
            else
                lblR13pi.Text = "";            
            rd_Rlpi.Attributes.Add("class", ColorPI);
            lblRiskTextpi.Text = GetAction(ColorPI);

            // for Environment
            ddlSeverityEI.SelectedValue = dr["SeverityEI"].ToString();
            ddlLikelihoodEI.SelectedValue = dr["LikeliHoodEI"].ToString();
            lblR13EI.Text = dr["RiskLevelEI"].ToString();

            lblSeverityTextEI.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityEI"]));
            lblLikelihoodTextEI.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodEI"]));
            string ColorEI = GetCSSColor(lblR13EI.Text);
            if (Common.CastAsInt32(lblR13EI.Text) != 0)
                lblR13EI.Text = lblR13EI.Text + " - " + GetRisk(ColorEI);
            else
                lblR13EI.Text = "";
            rd_RlEI.Attributes.Add("class", ColorEI);
            lblRiskTextEI.Text = GetAction(ColorEI);

            // for Asset
            ddlSeverityAI.SelectedValue = dr["SeverityAI"].ToString();
            ddlLikelihoodAI.SelectedValue = dr["LikeliHoodAI"].ToString();
            lblR13AI.Text = dr["RiskLevelAI"].ToString();

            lblSeverityTextAI.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityAI"]));
            lblLikelihoodTextAI.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodAI"]));
            string ColorAI = GetCSSColor(lblR13AI.Text);
            if (Common.CastAsInt32(lblR13AI.Text) != 0)
                lblR13AI.Text = lblR13AI.Text + " - " + GetRisk(ColorAI);
            else
                lblR13AI.Text = "";
            rd_RlAI.Attributes.Add("class", ColorAI);
            lblRiskTextAI.Text = GetAction(ColorAI);

            // for Reputation
            ddlSeverityRI.SelectedValue = dr["SeverityRI"].ToString();
            ddlLikelihoodRI.SelectedValue = dr["LikeliHoodRI"].ToString();
            lblR13RI.Text = dr["RiskLevelRI"].ToString();

            lblSeverityTextRI.ToolTip = GetSeverityText(Common.CastAsInt32(dr["SeverityRI"]));
            lblLikelihoodTextRI.Text = GetLikelihoodText(Common.CastAsInt32(dr["LikeliHoodRI"]));
            string ColorRI = GetCSSColor(lblR13RI.Text);
            if (Common.CastAsInt32(lblR13RI.Text) != 0)
                lblR13RI.Text = lblR13RI.Text + " - " + GetRisk(ColorRI);
            else
                lblR13RI.Text = "";
            rd_RlRI.Attributes.Add("class", ColorRI);
            lblRiskTextRI.Text = GetAction(ColorRI);

            txtACM.Text = dr["ADD_CONTROL_MEASURES"].ToString();

            //txtGRANo.Text = dr["GRVNo"].ToString();

            // For Peopel Final Risk
            if (! string.IsNullOrWhiteSpace(dr["Re_SeverityPF"].ToString()))
            {
                ddlReSeverityPF.SelectedValue = dr["Re_SeverityPF"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dr["Re_LikeliHoodPF"].ToString()))
            {
                ddlReLikelihoodPF.SelectedValue = dr["Re_LikeliHoodPF"].ToString();
            }

            lblReR13PF.Text = dr["Re_RiskLevelPF"].ToString();
            ibReSeverityPF.ToolTip = GetSeverityText(Common.CastAsInt32(dr["Re_SeverityPF"]));
            lblReLikelihoodTextPF.Text = GetLikelihoodText(Common.CastAsInt32(dr["Re_LikeliHoodPF"]));
            string Color = GetCSSColor(lblReR13PF.Text);
            if (Common.CastAsInt32(lblReR13PF.Text) != 0)
                lblReR13PF.Text = lblReR13PF.Text + " - " + GetRisk(Color);
            else
                lblReR13PF.Text = ""; 
            rd_ReR1PF.Attributes.Add("class", Color);
            lblReRiskTextPF.Text = GetAction(Color);

            // For Environment Final Risk
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
            //txtAgreedtime.Text = dr["AGREED_TIME"].ToString();
            txtPIC.Text = dr["PIC_NAME"].ToString();

            btnDeleteTask.Visible = true;
            dv_NewTask.Visible = true;
        }
        
    }
    protected void btnViewTask_Click(object sender, EventArgs e)
    {
        int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        btnDeleteTask.CommandArgument = hfdTaskIdNew.Value;
        DataRow[] drs = dtTasksNew.Select("TableId=" + _TableId);
        foreach (DataRow dr in drs)
        {
            DataView dv = dtHazardsNew.DefaultView;
            dv.RowFilter = "Hazard_TableId=" + dr["Hazard_TableId"];

            hfdTaskId.Text = dr["ConsequencesId"].ToString();
            // ddlTask.SelectedValue = dr["ConsequencesId"].ToString();
            // ddlTask.Enabled = false;
            txtTaskname.Text = getTaskName(hfdTaskId.Text);
            txtTaskname.Enabled = false;

            txtStdCM.Text = dr["ControlMeasures"].ToString();
            // For People
            if (! string.IsNullOrWhiteSpace(dr["SeverityPI"].ToString()))
            {
                ddlSeveritypi.SelectedValue = dr["SeverityPI"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dr["LikeliHoodPI"].ToString()))
            {
                ddlLikelihoodpi.SelectedValue = dr["LikeliHoodPI"].ToString();
            }
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
            if (!string.IsNullOrWhiteSpace(dr["SeverityEI"].ToString()))
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
            if (!string.IsNullOrWhiteSpace(dr["Re_SeverityPF"].ToString()))
            {
                ddlReSeverityPF.SelectedValue = dr["Re_SeverityPF"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dr["Re_LikeliHoodPF"].ToString()))
            {
                ddlReLikelihoodPF.SelectedValue = dr["Re_LikeliHoodPF"].ToString();
            }
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
            btnDeleteTask.Visible = false;
            dv_NewTask.Visible = true;
        }

        //ShowExtResRisk();

    }
    protected string getTaskName(string _TaskId)
    {
        string SQL = "SELECT ConsequencesCode + ' - ' + ConsequencesName AS ConsequencesName,ConsequencesId FROM dbo.RA_ConsequencesMaster with(nolock) where  ConsequencesId=" + _TaskId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
            return dt.Rows[0][0].ToString();
        else
            return "";

    }
    protected void btnAddHazard_Click1(object sender, EventArgs e)
    {
        ViewState["HazardTableId"] = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowAddHazard();
    }
    protected void ShowAddHazard()
    {
        DataRow[] dr1s = dtHazardsNew.Select("Hazard_TableId=" + ViewState["HazardTableId"].ToString());
        if (dr1s.Length > 0)
        {
            lblHazardName.Text = dr1s[0]["HazardName"].ToString();
            dv_NewTask.Visible = true;
        }
    }
    protected void btnSaveSingle_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtStdCM.Text))
        {
            lblMess.Text = "Please fill Control Measures";
            txtStdCM.Focus();
            return;
        }

        if (ddlSeveritypi.SelectedIndex == 0 || ddlLikelihoodpi.SelectedIndex == 0 || lblR13pi.Text == "" ) /*|| txtStdCM.Text == ""*/
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
        else if (ddlReSeverityPF.SelectedIndex == 0 || ddlReLikelihoodPF.SelectedIndex == 0 || lblReR13PF.Text == "") /*|| txtStdCM.Text == ""*/
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
                int Hazard_TableId = Common.CastAsInt32(ViewState["HazardTableId"]);
                if (Common.CastAsInt32(hfdTaskId.Text)<=0)
                {
                    lblMess.Text = "Please select Consequences.";
                    txtTaskname.Focus();
                    return;
                }
                DataView dv1 = dtTasksNew.DefaultView;
                dv1.RowFilter = "TemplateId=" + TemplateId + " AND Hazard_TableId=" + Hazard_TableId + " AND ConsequencesId=" + Common.CastAsInt32(hfdTaskId.Text) + " AND Status='A'";
                if (dv1.ToTable().Rows.Count > 0)
                {
                    lblMess.Text = "Please Check! Consequences already exists.";
                    txtTaskname.Focus();
                    return;
                }

                
                DataRow dr1 = dtTasksNew.NewRow();
                dr1["TableId"] = -(Common.CastAsInt32(dtTasksNew.Rows.Count) + 1);
                dr1["TemplateId"] = TemplateId;
                dr1["Hazard_TableId"] = Hazard_TableId;
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
                hfdTaskId.Text= "0";
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
    public void AddHazardFromDB(DataTable _dtHazards, DataTable _dtTasks)
    {
        foreach (DataRow drt in _dtHazards.Rows)
        {
            DataRow dr = dtHazardsNew.NewRow();
            int _Hazard_TableId = -(Common.CastAsInt32(dtHazardsNew.Rows.Count) + 1);
            dr["Hazard_TableId"] = _Hazard_TableId;
            dr["TemplateId"] = 0;
            dr["HazardId"] = drt["HazardId"].ToString();
            dr["HazardCode"] = drt["HazardCode"].ToString();
            dr["HazardName"] = drt["HazardName"].ToString();
            dr["Status"] = "A";

            dtHazardsNew.Rows.Add(dr);
            DataRow[] drtks= _dtTasks.Select("Hazard_TableId=" + drt["Hazard_TableId"].ToString()); 
            foreach (DataRow drt1 in drtks)
            {
                DataRow dr1 = dtTasksNew.NewRow();
                dr1["TableId"] = -(Common.CastAsInt32(dtTasksNew.Rows.Count) + 1);
                dr1["TemplateId"] = 0;
                dr1["Hazard_TableId"] = _Hazard_TableId;
                dr1["ConsequencesId"] = drt1["ConsequencesId"];
                dr1["ConsequencesCode"] = drt1["ConsequencesCode"];
                dr1["ConsequencesName"] = drt1["ConsequencesName"];
                dr1["ControlMeasures"] = drt1["ControlMeasures"];

                dr1["SeverityPI"] = drt1["SeverityPI"];
                dr1["LikeliHoodPI"] = drt1["LikeliHoodPI"];
                dr1["RiskLevelPI"] = drt1["RiskLevelPI"];

                dr1["SeverityEI"] = drt1["SeverityEI"];
                dr1["LikeliHoodEI"] = drt1["LikeliHoodEI"];
                dr1["RiskLevelEI"] = drt1["RiskLevelEI"];

                dr1["SeverityAI"] = drt1["SeverityAI"];
                dr1["LikeliHoodAI"] = drt1["LikeliHoodAI"];
                dr1["RiskLevelAI"] = drt1["RiskLevelAI"];

                dr1["SeverityRI"] = drt1["SeverityRI"];
                dr1["LikeliHoodRI"] = drt1["LikeliHoodRI"];
                dr1["RiskLevelRI"] = drt1["RiskLevelRI"];

                dr1["ADD_CONTROL_MEASURES"] = drt1["ADD_CONTROL_MEASURES"];
               // dr1["GRVNo"] = drt1["GRVNo"];
                dr1["Re_SeverityPF"] = drt1["Re_SeverityPF"];
                dr1["Re_LikeliHoodPF"] = drt1["Re_LikeliHoodPF"];
                dr1["Re_RiskLevelPF"] = drt1["Re_RiskLevelPF"];

                dr1["Re_SeverityEF"] = drt1["Re_SeverityEF"];
                dr1["Re_LikeliHoodEF"] = drt1["Re_LikeliHoodEF"];
                dr1["Re_RiskLevelEF"] = drt1["Re_RiskLevelEF"];

                dr1["Re_SeverityAF"] = drt1["Re_SeverityAF"];
                dr1["Re_LikeliHoodAF"] = drt1["Re_LikeliHoodAF"];
                dr1["Re_RiskLevelAF"] = drt1["Re_RiskLevelAF"];

                dr1["Re_SeverityRF"] = drt1["Re_SeverityRF"];
                dr1["Re_LikeliHoodRF"] = drt1["Re_LikeliHoodRF"];
                dr1["Re_RiskLevelRF"] = drt1["Re_RiskLevelRF"];

                dr1["Status"] = "A";
                dtTasksNew.Rows.Add(dr1);
            }
        }
        dtTasksNew.AcceptChanges();
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
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (TemplateId > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("DBO.EV_ApproveTemplate " + TemplateId + ",'" + UserName + "'");
            lblMsg.Text = "Template approved successfully.";
        }
        else
        {
            lblMsg.Text = "Please create template first.";
        }
    }
    protected void btnRequestAppprove_Click(object sender, EventArgs e)
    {
        if (TemplateId > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.EV_TemplateMaster SET REQUESTEDBY='" + UserName + "',REQUESTEDON=GETDATE() WHERE TEMPLATEID=" + TemplateId);
            lblMsg.Text = "Template sent for approval process.";
        }
        else
        {
            lblMsg.Text = "Please create template first.";
        }
    }

    //protected void ddlHazard_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlHazard.SelectedValue) > 0)
    //    {
    //        hfdHAZid.Text = ddlHazard.SelectedValue;
    //    }
    //}

    //protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlTask.SelectedValue) > 0)
    //    {
    //        hfdTaskId.Text = ddlTask.SelectedValue;
    //    }
    //}

    protected void btnAddHazard_Click(object sender, EventArgs e)
    {
        rptAddHazard.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[EV_HazardMaster] ORDER BY HazardName");
        rptAddHazard.DataBind();
        dvNewHazard.Visible = true;
    }

    protected void btnSelectHazard_Click(object sender, EventArgs e)
    {
        int HazardId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string HazardName = ((ImageButton)sender).ToolTip;
        try
        {
            DataView dv = dtHazardsNew.DefaultView;
            dv.RowFilter = "TemplateId=" + TemplateId + " AND HazardId=" + HazardId + " AND Status='A'";
            if (dv.ToTable().Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please Check! Hazard already exists.');", true);
                return;
            }

            DataRow dr = dtHazardsNew.NewRow();
            int Key = -(Common.CastAsInt32(dtHazardsNew.Rows.Count));
            dr["Hazard_TableId"] = Key;
            dr["TemplateId"] = TemplateId;
            dr["HazardId"] = HazardId;
            dr["HazardName"] = HazardName;
            dr["Status"] = "A";

            dtHazardsNew.Rows.Add(dr);
            dtHazardsNew.AcceptChanges();
            BindHazardsNew();
            dvNewHazard.Visible = false;
            //----
            ViewState["HazardTableId"] = Key;
            ShowAddHazard();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to add. Error : " + ex.Message.ToString();
        }

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

    protected void ibCloseNewTask_Click(object sender, EventArgs e)
    {
        dv_NewTask.Visible = false;
    }
}