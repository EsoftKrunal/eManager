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
using System.Text.RegularExpressions;

public partial class Popup_ExecuteJob : System.Web.UI.Page
{
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public int ComponentId
    {
        set { ViewState["CI"] = value; }
        get { return Common.CastAsInt32(ViewState["CI"]); }
    }
    public int JobId
    {
        set { ViewState["JI"] = value; }
        get { return Common.CastAsInt32(ViewState["JI"]); }
    }
    public string HistoryId
    {
        set { ViewState["HI"] = value; }
        get { return ViewState["HI"].ToString(); }
    }

    public DataTable dtSpareDetails
    {
        set { Session["_dtSpareDetails"] = value; }
        get { return (DataTable)Session["_dtSpareDetails"];  }
    }

    //public static DataTable dtSpareDetails;
    public string tempfoldername
    {
        set { ViewState["tempfoldername"] = value; }
        get { return ViewState["tempfoldername"].ToString(); }
    }
    public DataTable dtimages
    {
        set { ViewState["dtimages"] = value; }
        get {

            if (ViewState["dtimages"] == null)
            {
                DataTable _dt = new DataTable();
                _dt.Columns.Add("filename", typeof(string));
                _dt.Columns.Add("description", typeof(string));
                return _dt;
            }
            else
                return (DataTable)ViewState["dtimages"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (!Page.IsPostBack)
        {
            Session["defectsData"] = null;
            Session["SparesAdded"] = null;

            if (Request.QueryString["CID"] != null && Request.QueryString["JID"] != null)
            {
                dtSpareDetails = null; 
                VesselCode = Session["CurrentShip"].ToString();
                ComponentId = Common.CastAsInt32(Request.QueryString["CID"].ToString());
                JobId = Common.CastAsInt32(Request.QueryString["JID"].ToString());
                HistoryId = "";
                bindattachments();
                ShowCompJobDetails();
                BindRanks();
                BindPlannedJobs();
                CheckrunningHourAvailability();
                ShowRatings();
                try
                {
                    ddlRank.SelectedValue = Page.Request.QueryString["PR"].ToString();
                }
                catch (Exception ex)
                { ddlRank.SelectedIndex = 0; }
            }
        }
    }
    #region USER DEFINED FUNCTIONS
    public void bindattachments()
    {
        rptAttachments.DataSource= Common.Execute_Procedures_Select_ByQuery("select ROW_NUMBER() OVER(ORDER BY ATTACHMENTID) AS SNO,* from dbo.JobExecAttachmentsMaster where CompJobId=" + JobId + " order by AttachmentId ");
        rptAttachments.DataBind();
    }
    public void BindRanks()
    {

        DataTable dtRanks = new DataTable();
        string strSQL = "";
        try
        {
            strSQL = "SELECT RankId,RankCode FROM Rank WHERE RANKID IN (select rankid from [dbo].[ComponentJobMapping_OtherRanks] where compjobid=" + JobId + ") ORDER BY RankLevel ";
            dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        }
        catch{}
        if (dtRanks == null)
        {
            strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankLevel";
        }
        else if (dtRanks.Rows.Count==0)
        {
            strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankLevel";
        }
        
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);

        ddlRank.DataSource = dtRanks;
        ddlRank.DataTextField = "RankCode";
        ddlRank.DataValueField = "RankId";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, "< SELECT >");
        ddlRank.Items[0].Value = "0";
    }
    private void BindPlannedJobs()
    {
        string strSQL = "SELECT CM.ComponentCode,CM.ComponentName,JM.JobCode,CJM.DescrSh AS JobName,JIM.IntervalName ,RM.RankId,RM.RankCode,PM.CompJobId,ISNULL(PM.DoneBy,0) AS DoneBy,PM.DoneBy_Code,PM.DoneBy_Name,ISNULL(PM.UpdateRemarks,0) AS UpdateRemarks,PM.Specify, CASE REPLACE(CONVERT(VARCHAR(11), PM.DoneDate,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), PM.DoneDate,106),' ','-') END  AS DoneDate,PM.ServiceReport,PM.ConditionBefore,PM.ConditionAfter,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As DueDate,PM.NextDueHours,CASE REPLACE(CONVERT(VARCHAR(15),PM.DoneDate,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15),PM.DoneDate,106),' ','-') END AS LastDone,PM.DoneHour AS LastHour,REPLACE(CONVERT(VARCHAR(15),PM.PlanDate,106),' ','-') AS PlanDate,VCJM.IntervalId_H, VCJM.Interval_H FROM VSL_PlanMaster PM  " +
                        "INNER JOIN VSL_VesselComponentJobMaster VCJM ON VCJM.ComponentId = PM.ComponentId AND VCJM.CompJobId = PM.CompJobId AND VCJM.VesselCode = PM.VesselCode  " +
                        "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId AND VCJM.VesselCode = VCJMU.VesselCode " +
                        "INNER JOIN ComponentMaster CM ON CM.ComponentId = PM.ComponentId " +
                        "INNER JOIN ComponentsJobMapping CJM  ON PM.CompJobId = CJM.CompJobId " +
                        "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                        "INNER Join JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                        "INNER JOIN Rank RM ON RM.RankId = PM.AssignedTo " +
                        "WHERE PM.VesselCode = '" + VesselCode + "' AND PM.ComponentId = " + ComponentId + " AND PM.CompJobId = " + JobId + " ";
        DataTable dtPlannedJobs = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtPlannedJobs.Rows.Count > 0)
        {
            //lblComponent.Text = dtPlannedJobs.Rows[0]["ComponentCode"].ToString() + " : " + dtPlannedJobs.Rows[0]["ComponentName"].ToString();
            //lblJobDesc.Text = dtPlannedJobs.Rows[0]["JobCode"].ToString() + " : " + dtPlannedJobs.Rows[0]["JobName"].ToString();
            lblInterval.Text = dtPlannedJobs.Rows[0]["Interval"].ToString() + " " + dtPlannedJobs.Rows[0]["IntervalName"].ToString();
            txtDuedt.Text = dtPlannedJobs.Rows[0]["DueDate"].ToString();
            lblLastDoneDt.Text = (dtPlannedJobs.Rows[0]["LastDone"].ToString()== "0" ? "" : dtPlannedJobs.Rows[0]["LastDone"].ToString());
            hfIntervalId_H.Value = dtPlannedJobs.Rows[0]["IntervalId_H"].ToString();
            hfInterval_H.Value = dtPlannedJobs.Rows[0]["Interval_H"].ToString();

            if (dtPlannedJobs.Rows[0]["IntervalName"].ToString() == "H")
            {
                trHr.Visible = true;
                txtLastHour.Text = (dtPlannedJobs.Rows[0]["LastHour"].ToString()== "0" ? "" : dtPlannedJobs.Rows[0]["LastHour"].ToString());;
            }
            else
            {
                trHr.Visible = false;
            }
        }
    }
    public void UserDetails()
    {
        string strSQL = "Select ((select UserName from ShipUserMaster where UserId=VSL_PlanMaster.ModifiedBy) + ' / ' + replace(convert(varchar,VSL_PlanMaster.ModifiedOn,106), ' ', '-')) as UpdatedByName FROM VSL_PlanMaster WHERE VesselCode = '" + VesselCode + "' AND ComponentId = " + ComponentId + " AND CompJobId = " + JobId + " ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt.Rows[0]["UpdatedByName"].ToString() != "")
        {
            lblUpdatedByOn.Text = dt.Rows[0]["UpdatedByName"].ToString();
        }
    }
    private void ShowCompJobDetails()
    {
        string strCompJobDetails = "SELECT (SELECT ShipName FROM Settings WHERE ShipCode = '" + VesselCode + "') AS VesselName, CM.ComponentCode,CM.ComponentName ,JM.JobCode,CJM.DescrSh AS JobName,JIM.IntervalName,VCJM.Interval,CJM.AttachmentForm,CJM.RiskAssessment,dbo.getRootPath(CM.COMPONENTCODE) as Parent FROM VSL_VesselComponentJobMaster VCJM " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId " +
                                    "INNER JOIN ComponentsJobMapping CJM  ON VCJM.CompJobId = CJM.CompJobId " +
                                    "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                                    "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                                    "WHERE VCJM.VesselCode = '" + VesselCode + "' AND VCJM.ComponentId =" + ComponentId + " AND VCJM.CompJobId=" + JobId + " ";
        DataTable dtCompJobDetails = Common.Execute_Procedures_Select_ByQuery(strCompJobDetails);
        if (dtCompJobDetails.Rows.Count > 0)
        {
            ViewState["__compcode"] = dtCompJobDetails.Rows[0]["ComponentCode"].ToString();
            hfdcompid.Value = ComponentId.ToString();
            hfdcompcode.Value = ViewState["__compcode"].ToString();
            lblUpdateComponent.Text = dtCompJobDetails.Rows[0]["ComponentCode"].ToString() + " - " + dtCompJobDetails.Rows[0]["ComponentName"].ToString();
            lblParent.Text = dtCompJobDetails.Rows[0]["Parent"].ToString();
            lblUpdateJob.Text = dtCompJobDetails.Rows[0]["JobCode"].ToString() + " - " + dtCompJobDetails.Rows[0]["JobName"].ToString();
            lblUpdateInterval.Text = dtCompJobDetails.Rows[0]["Interval"].ToString() + " - " + dtCompJobDetails.Rows[0]["IntervalName"].ToString();

            //********************* Note Section *****************************
            //if(dtCompJobDetails.Rows[0]["AttachmentForm"].ToString().Trim() != "")
            //{
            //    lblAttachForm.Text = dtCompJobDetails.Rows[0]["AttachmentForm"].ToString().Trim();
            //    ancAttach.HRef = "~/UploadFiles/AttachmentForm/" + dtCompJobDetails.Rows[0]["AttachmentForm"].ToString().Trim();
            //    trAttach.Visible = true;
            //}
            //if(dtCompJobDetails.Rows[0]["RiskAssessment"].ToString().Trim() != "")
            //{
            //    lblRiskAssessment.Text = dtCompJobDetails.Rows[0]["RiskAssessment"].ToString().Trim();
            //    ancRisk.HRef = "~/UploadFiles/RiskAssessment/" + dtCompJobDetails.Rows[0]["RiskAssessment"].ToString().Trim();
            //    trRisk.Visible = true;
            //}

            //
            string AttachmentSql = "select row_number() over(order by TableId) as Sno ,* from ( " +
                                   " select ('ShipDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName ,row_number() over(order by TableId) as Sno,* from VSL_VesselComponentJobMaster_Attachments " +
                                   " WHERE STATUS='A' AND VesselCode='" + VesselCode + "' and CompJobID=" + JobId + "" +
                                   " Union " +
                                   " select ('OfficeDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName,row_number() over(order by TableId) as Sno,'' as VesselCode, * from ComponentsJobMapping_attachments  where CompJobID=" + JobId + " AND STATUS='A' " +
                                   " )tbl";
            rptFiles.DataSource = Common.Execute_Procedures_Select_ByQuery(AttachmentSql);
            rptFiles.DataBind();

        }
    }
    public void CheckrunningHourAvailability()
    {
        if (lblInterval.Text.Split(' ').GetValue(1).ToString() == "H")
        {
            string SQL = "SELECT TOP 1 *,UpdatedOn AS Updated1 FROM VSL_VesselRunningHourMaster WHERE VesselCode = '" + VesselCode + "' AND ComponentId =" + ComponentId + " ORDER BY StartDate,Updated1 Desc";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "checkrhavailability();", true);
            }
        }
    }
    public void BindCompSpares()
    {
        ddlSparesList.Items.Clear();
        string Parents = ProjectCommon.getParentComponents_Chain(ComponentId);
        string strSpares = "SELECT VESSELCODE+'#'+CONVERT(VARCHAR,COMPONENTID)+'#'+OFFICE_SHIP+'#'+CONVERT(VARCHAR,SPAREID) AS PKID ,SPARENAME + ' - ' + PARTNO AS SPARENAME from VSL_VesselSpareMaster WHERE VesselCode = '" + Session["CurrentShip"].ToString().Trim() + "' AND ComponentId IN (" + ComponentId + "," + Parents + ") order by SPARENAME ";
        DataTable dtspares = Common.Execute_Procedures_Select_ByQuery(strSpares);
        if (dtspares.Rows.Count > 0)
        {
            ddlSparesList.DataSource = dtspares;
            ddlSparesList.DataTextField = "SPARENAME";
            ddlSparesList.DataValueField = "PKID";
            ddlSparesList.DataBind();
        }
        ddlSparesList.Items.Insert(0, new ListItem("< SELECT >", "0"));

    }
    public void ShowComponentSpare()
    {
        rptComponentSpares.DataSource = null;
        rptComponentSpares.DataBind();
        
        dtSpareDetails = SparesDataTable();
        rptComponentSpares.DataSource = dtSpareDetails;
        rptComponentSpares.DataBind();
    }
    private void BindInventoryStatus()
    {
        if (ddlSparesList.SelectedIndex > 0)
        {
            txtQtyRob.Text = ProjectCommon.getROB(ddlSparesList.SelectedValue,DateTime.Today).ToString();
        }
        else
        {
            txtQtyRob.Text = "";
        }
    }
    private void BindAttachment()
    {
        string strSQL = "SELECT * FROM VSL_VesselJobUpdateHistoryAttachments WHERE VesselCode = '" + VesselCode + "' AND HistoryId = " + HistoryId.Trim() + " AND STATUS='A'";
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtAttachment.Rows.Count > 0)
        {
            rptAttachment.DataSource = dtAttachment;
            rptAttachment.DataBind();
            dvAttachment.Visible = true;
        }
        else
        {
            rptAttachment.DataSource = null;
            rptAttachment.DataBind();
        }
    }
    private void ShowRatings()
    {
        string strSQL = "SELECT RatingRequired FROM ComponentsJobMapping WHERE CompJobId = " + JobId;
        DataTable dtRating = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtRating.Rows.Count > 0)
        {
            if(dtRating.Rows[0]["RatingRequired"].ToString().Trim() != "")
            {

                trRating.Visible = Convert.ToBoolean(dtRating.Rows[0]["RatingRequired"].ToString());
            }
        }
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        pnlUpload.Visible = false;
        string temppath = Server.MapPath("~/TEMPUPLOAD");

        if (!Directory.Exists(temppath))
            Directory.CreateDirectory(temppath);
        tempfoldername = Path.GetRandomFileName();

        string localpath = temppath + "\\" + tempfoldername + "\\";
        if (!Directory.Exists(localpath))
            Directory.CreateDirectory(localpath);
        foreach (RepeaterItem ri in rptAttachments.Items)
        {
            FileUpload fu = (FileUpload)ri.FindControl("flpfile");
            if (fu.FileName.EndsWith(".png") || fu.FileName.EndsWith(".jpg") || fu.FileName.EndsWith(".jpeg"))
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fdsaf", "Please select only image files to upload.", true);
                return;
            }            
        }
        DataTable _dt = new DataTable();
        _dt.Columns.Add("filename", typeof(string));
        _dt.Columns.Add("description", typeof(string));
        foreach (RepeaterItem ri in rptAttachments.Items)
        {
            FileUpload fu = (FileUpload)ri.FindControl("flpfile");
            if(fu.HasFile)
            {
                string randomname = fu.CssClass.ToString() + "_" + fu.FileName;
                _dt.Rows.Add(_dt.NewRow());
                _dt.Rows[_dt.Rows.Count-1]["filename"] = ".\\tempupload\\" + tempfoldername + "\\" + randomname;                
                GenerateThumbnails(fu.PostedFile.InputStream, localpath + randomname);                
            }
        }
        dtimages = _dt;
        rptiamges.DataSource = dtimages;
        rptiamges.DataBind();
        lblfilesmessage.Text = "Your have uploaded ( " + rptAttachments.Items.Count.ToString() + " ) files with this job. That will saved automatically in documents when you press save button. Please do not hit browser refresh or back button.";
    }
    public void ShowImages()
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("filename", typeof(string));
        _dt.Columns.Add("description", typeof(string));
        string path = ProjectCommon.getUploadFolder(DateTime.Parse(txtDoneDate.Text));
        DataTable dtimages = Common.Execute_Procedures_Select_ByQuery("select attachmentid,filename,description from dbo.VSL_JobExecAttachmentsDetails	where vesselcode='" + VesselCode + "' and historyid=" + HistoryId);
        foreach (DataRow dr in dtimages.Rows)
        {
            _dt.Rows.Add(_dt.NewRow());
            _dt.Rows[_dt.Rows.Count - 1]["filename"] = "./" + path + dr["filename"].ToString();
            _dt.Rows[_dt.Rows.Count - 1]["description"] = dr["description"].ToString();
        }
        dtimages = _dt;
        rptiamges.DataSource = dtimages;
        rptiamges.DataBind();
    }
    private void GenerateThumbnails(Stream sourcePath,string targetPath)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromStream(sourcePath))
        {
            double scaleFactor = 800.0/image.Width;
            // can given width of image as we want
            var newWidth = (int)(image.Width * scaleFactor);
            // can given height of image as we want
            var newHeight = (int)(image.Height * scaleFactor);

            var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
            var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }
    #endregion
    #region EVENTS
    protected void ddlRemarks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRemarks.SelectedValue == "2")
        {
            trSpecify.Visible = true;
        }
        else
        {
            trSpecify.Visible = false;
        }
        if (ddlRemarks.SelectedValue == "3")
            rdoBreakdownReason.Visible = true;
        else
            rdoBreakdownReason.Visible = false;

        //if (ddlRemarks.SelectedValue == "3")
        //{
        //    btnAddDefectDetails.Visible = true;
        //    rdoBreakdownReason.Visible = true;
        //    ddlRemarks.Style.Add("width", "135px");
        //}
        //else
        //{
        //    btnAddDefectDetails.Visible = false;
        //    rdoBreakdownReason.Visible = false;
        //    ddlRemarks.Style.Remove("width");
        //}
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtEmpNo.Text == "")
        {
            mbUpdateJob.ShowMessage("Please enter employee number.", true);
            txtEmpNo.Focus();
            return;
        }
        Regex reg = new Regex("^[S/Y]\\d\\d\\d\\d\\d$");
        if (!reg.IsMatch(txtEmpNo.Text.Trim().ToUpper()))
        {
            mbUpdateJob.ShowMessage("Please enter valid employee number.", true);
            txtEmpNo.Focus();
            return;
        }
        if (txtEmpName.Text == "")
        {
            mbUpdateJob.ShowMessage("Please enter employee name.", true);
            txtEmpName.Focus();
            return;
        }
        if (ddlRank.SelectedIndex == 0)
        {
            mbUpdateJob.ShowMessage("Please select rank.", true);
            ddlRank.Focus();
            return;
        }
        if (ddlRemarks.SelectedIndex == 0)
        {
            mbUpdateJob.ShowMessage("Please select remarks.", true);
            ddlRemarks.Focus();
            return;
        }
        if (ddlRemarks.SelectedValue == "2")
        {
            if (txtSpecify.Text == "")
            {
                mbUpdateJob.ShowMessage("Please specify remarks.", true);
                txtSpecify.Focus();
                return;
            }
        }
        if (ddlRemarks.SelectedValue == "3")
        {
            if (Session["defectsData"] == null)
            {
                mbUpdateJob.ShowMessage("Please add defects.", true);
                ddlRemarks.Focus();
                return;
            }
            if (rdoBreakdownReason.SelectedIndex == -1)
            {
                mbUpdateJob.ShowMessage("Please select equipment status.", true);
                rdoBreakdownReason.Focus();
                return;
            }
        }
        if (lblInterval.Text.Split(' ').GetValue(1).ToString() == "H")
        {
            if (txtDoneHour.Text == "")
            {
                mbUpdateJob.ShowMessage("Please enter done hour.", true);
                txtDoneHour.Focus();
                return;
            }
            int i;
            if (!(int.TryParse(txtDoneHour.Text.Trim(), out i)))
            {
                mbUpdateJob.ShowMessage("Please enter valid done hour.(DECIMAL NOT ALLOWED)", true);
                txtDoneHour.Focus();
                return;
            }
            if (txtLastHour.Text != "")
            {
                if (int.Parse(txtDoneHour.Text.Trim()) < int.Parse(txtLastHour.Text.Trim()))
                {
                    mbUpdateJob.ShowMessage("Done hour can not be less than last hour.", true);
                    txtDoneHour.Focus();
                    return;
                }
            }

        }
        if (txtDoneDate.Text == "")
        {
            mbUpdateJob.ShowMessage("Please enter done date.", true);
            txtDoneDate.Focus();
            return;
        }
        DateTime dt;
        if (!(DateTime.TryParse(txtDoneDate.Text.Trim(), out dt)))
        {
            mbUpdateJob.ShowMessage("Please enter valid done date.", true);
            txtDoneDate.Focus();
            return;
        }
        if ((DateTime.Parse(txtDoneDate.Text.Trim())) > DateTime.Today.Date)
        {
            mbUpdateJob.ShowMessage("Done date can not be greater than today.", true);
            txtDoneDate.Focus();
            return;
        }
        if (lblLastDoneDt.Text.Trim() != "")
        {
            if ((DateTime.Parse(txtDoneDate.Text.Trim())) < DateTime.Parse(lblLastDoneDt.Text.Trim()))
            {
                mbUpdateJob.ShowMessage("Done date can not be less than last done date.", true);
                txtDoneDate.Focus();
                return;
            }
        }
        if (lblInterval.Text.Split(' ').GetValue(1).ToString() == "H")
        {
            if (txtNextDueDt.Text == "")
            {
                mbUpdateJob.ShowMessage("Running hour does no exist.can not calculate next due date.", true);
                txtNextDueDt.Focus();
                return;
            }
        }
        if (txtServiceReport.Text.Trim().Length > 5000)
        {
            mbUpdateJob.ShowMessage("Service report should not be more than 5000 characters.", true);
            txtServiceReport.Focus();
            return;
        }
        if (txtCondBefore.Text == "")
        {
            mbUpdateJob.ShowMessage("Please enter before condition.", true);
            txtCondBefore.Focus();
            return;
        }
        if (txtCondAfter.Text == "")
        {
            mbUpdateJob.ShowMessage("Please enter after condition.", true);
            txtCondAfter.Focus();
            return;
        }
        if (trRating.Visible)
        {
            if (ddlCoating.SelectedValue == "0")
            {
                mbUpdateJob.ShowMessage("Please select rating for coating.", true);
                ddlCoating.Focus();
                return;
            }
            if (ddlCorrosion.SelectedValue == "0")
            {
                mbUpdateJob.ShowMessage("Please select rating for corrosion.", true);
                ddlCorrosion.Focus();
                return;
            }
            if (ddlDeformation.SelectedValue == "0")
            {
                mbUpdateJob.ShowMessage("Please select rating for deformation.", true);
                ddlDeformation.Focus();
                return;
            }
            if (ddlCracks.SelectedValue == "0")
            {
                mbUpdateJob.ShowMessage("Please select rating for cracks.", true);
                ddlCracks.Focus();
                return;
            }
            if (ddlORating.SelectedValue == "0")
            {
                mbUpdateJob.ShowMessage("Please select overall rating.", true);
                ddlORating.Focus();
                return;
            }
        }
        //------------------------------
        if (lblLastDoneDt.Text.Trim() != "" && txtLastHour.Text.Trim() != "")
        {
            int MaxHours = 0;
            string MainCode = lblUpdateComponent.Text.Substring(0, 3);
            DateTime LastRunningHrDate = DateTime.Parse(getLastHoursDate(MainCode) == "" ? "01/01/1900" : getLastHoursDate(MainCode));
            DateTime LastHistoryHrDate = DateTime.Parse(getLastHistoryHoursDate(MainCode) == "" ? "01/01/1900" : getLastHistoryHoursDate(MainCode));
            int LastRunningHr = getLastHours(MainCode);
            int LastHistoryHr = getLastHistoryHours(MainCode, txtDoneDate.Text);
            DateTime MaxDate = LastRunningHrDate;

            if (LastRunningHr > LastHistoryHr)
            {
                MaxHours = LastRunningHr;
                MaxDate = LastRunningHrDate;
            }
            else
            {
                MaxHours = LastHistoryHr;
                MaxDate = LastHistoryHrDate;
            }

            if (MaxHours > 0)
            {
                int MaxIncreasedHrs = DateTime.Parse(txtDoneDate.Text).Subtract(MaxDate).Days * 24;
                if (Common.CastAsInt32(txtDoneHour.Text) - MaxHours > MaxIncreasedHrs && MaxIncreasedHrs >= 0)
                {
                    mbUpdateJob.ShowMessage("Invalid startup Hrs.( Max Allowed " + MaxIncreasedHrs.ToString() + " due to last update on " + MaxDate.ToString("dd-MMM-yyyy") + " with ( " + MaxHours + " ) hrs. ).", true);
                    return;
                }
            }
        }
        //------------------------------

        Byte[] imgByte = new Byte[0];
        string CompStatus = "";
        string _ComponentCode = lblUpdateComponent.Text.Trim().Split('-').GetValue(0).ToString().Trim();
        if (ddlRemarks.SelectedValue == "3")
        {
            if (rdoBreakdownReason.SelectedValue == "1")
            {
                CompStatus = "W";
            }
            else
            {
                CompStatus = "N";
            }
        }
        else
        {
            CompStatus = " ";
        }
        
        Common.Set_Procedures("sp_UpdatePlannedJob");
        Common.Set_ParameterLength(26);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@HistoryId", HistoryId),
            new MyParameter("@ComponentId", ComponentId),
            new MyParameter("@CompjobId", JobId),
            new MyParameter("@ComponentCode", _ComponentCode),
            new MyParameter("@DoneDate", txtDoneDate.Text.Trim()),
            new MyParameter("@DoneHour", txtDoneHour.Text.Trim()),
            new MyParameter("@DoneBy", ddlRank.SelectedValue),
            new MyParameter("@EmployeeNo", txtEmpNo.Text.Trim()),
            new MyParameter("@EmployeeName", txtEmpName.Text.Trim()),
            new MyParameter("@UpdateRemarks", ddlRemarks.SelectedValue),
            new MyParameter("@RemarkSpecify", txtSpecify.Text.Trim()),
            new MyParameter("@ServiceReport", txtServiceReport.Text.Trim()),
            new MyParameter("@ConditionBefore", txtCondBefore.Text.Trim()),
            new MyParameter("@ConditionAfter", txtCondAfter.Text.Trim()),
            new MyParameter("@FileName", ""),
            new MyParameter("@DocumentFile", imgByte),
            new MyParameter("@Coating", ddlCoating.SelectedValue.Trim()),
            new MyParameter("@Corrosion", ddlCorrosion.SelectedValue.Trim()),
            new MyParameter("@Deformation", ddlDeformation.SelectedValue.Trim()),
            new MyParameter("@Cracks", ddlCracks.SelectedValue.Trim()),
            new MyParameter("@OverallRating", ddlORating.SelectedValue.Trim()),
            new MyParameter("@NextDueDate", txtNextDueDt.Text),
            new MyParameter("@NextDueHours", txtNextHour.Text),
            new MyParameter("@UpdatedBy", Session["loginid"].ToString()),
            new MyParameter("@CompStatus", CompStatus)
            );
        DataSet dsJobPlanning = new DataSet();
        dsJobPlanning.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsJobPlanning);
        if (res)
        {
            HistoryId = dsJobPlanning.Tables[0].Rows[0]["HistoryId"].ToString();
            //--------------------------- SCHEDULE COMMENTS PACKET FOR OFFICE
            try
            {
                DataTable dt114 = Common.Execute_Procedures_Select_ByQuery("DBO.GETJOBCOMMENTS_SHIP '" + VesselCode + "'," + JobId + "");
                //----------------------------------------
                string row_count = dt114.Rows[0][0].ToString();
                dt114 = null;
                int allrows = Common.CastAsInt32(row_count);
                if (allrows > 0)
                {

                    //----------------------------------------
                    Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
                    Common.Set_ParameterLength(5);
                    Common.Set_Parameters(
                        new MyParameter("@VesselCode", VesselCode),
                        new MyParameter("@RecordType", "JOB-COMMENTS"),
                        new MyParameter("@RecordId", JobId),
                        new MyParameter("@RecordNo", VesselCode + "-" + ViewState["__compcode"].ToString() + "-" + JobId.ToString()),
                        new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
                    );
                    //----------------------------------------
                    DataSet ds11 = new DataSet();
                    ds11.Clear();
                    Common.Execute_Procedures_Select();
                    //----------------------------------------
                }
            }
            catch (Exception ex)
            {

            }

            //--------------------------- SAVE FILES FROM TEMP TO FINAL TABLE
            string localpath = Server.MapPath("~/TEMPUPLOAD") + "\\" + tempfoldername + "\\";
            DataTable filedetails = Common.Execute_Procedures_Select_ByQuery("select attachmentid,attachmentdetails from dbo.JobExecAttachmentsMaster where CompJobId = " + JobId + " order by AttachmentId ");
            if (Directory.Exists(localpath))
            {
                String[] fls = Directory.GetFiles(localpath);
                foreach(string s in fls)
                {
                    string filename = Path.GetFileName(s);
                    string attachmentid = filename.Substring(0, filename.IndexOf("_"));
                    string filedescription = "";
                    foreach (DataRow dr in filedetails.Rows)
                    {
                        if (Common.CastAsInt32(attachmentid) == Common.CastAsInt32(dr["attachmentid"]))
                            filedescription = dr["attachmentdetails"].ToString();
                    }
                    string computedFileName = VesselCode + "_" + HistoryId.Trim() + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(filename);
                    Common.Set_Procedures("sp_Insert_VSL_JobExecAttachmentsDetails");
                    Common.Set_ParameterLength(6);
                    Common.Set_Parameters(
                        new MyParameter("@VesselCode", VesselCode.Trim()),
                        new MyParameter("@HistoryId", HistoryId),
                        new MyParameter("@compjobid", JobId),
                        new MyParameter("@attachmentid", attachmentid),                        
                        new MyParameter("@description", filedescription),
                        new MyParameter("@fileName", computedFileName));
                    DataSet dsAttachment = new DataSet();
                    dsAttachment.Clear();
                    Boolean result = Common.Execute_Procedures_IUD(dsAttachment);
                    string path = Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(txtDoneDate.Text)));
                    if (!System.IO.Directory.Exists(path ))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    File.Move(localpath + filename, path + computedFileName);
                }
                ShowImages();
            }
            //--------------------------------

            if (ddlSparesReqd.SelectedValue == "1" && rptComponentSpares.Items.Count > 0)
            {
                foreach ( RepeaterItem item in rptComponentSpares.Items )
                {
                    HiddenField hfdComponentId = (HiddenField)item.FindControl("hfdComponentId");
                    HiddenField hfOffice_Ship = (HiddenField)item.FindControl("hfOffice_Ship");
                    HiddenField hfSpareId = (HiddenField)item.FindControl("hfSpareId");

                    Label lblQtyCons = (Label)item.FindControl("lblQtyCons");
                    Label lblQtyRob = (Label)item.FindControl("lblQtyRob");

                    Common.Set_Procedures("sp_InsertSpares_Execute");
                    Common.Set_ParameterLength(7);
                    Common.Set_Parameters(
                        new MyParameter("@VesselCode", Session["CurrentShip"].ToString().Trim()),
                        new MyParameter("@ComponentId", hfdComponentId.Value),
                        new MyParameter("@Office_Ship", hfOffice_Ship.Value.ToString().Trim()),
                        new MyParameter("@SpareId", hfSpareId.Value.ToString().Trim()),
                        new MyParameter("@HistoryId", HistoryId),
                        new MyParameter("@QtyCons", lblQtyCons.Text.Trim().ToString()),
                        new MyParameter("@QtyRob", lblQtyRob.Text.Trim().ToString())
                        );

                    DataSet dsComponentSpare = new DataSet();
                    dsComponentSpare.Clear();
                    Boolean result;
                    result = Common.Execute_Procedures_IUD(dsComponentSpare);
                }
            }

            //if (ddlRemarks.SelectedValue == "3")
            //{
                if (Session["defectsData"] != null)
                {
                    
                    DataTable dtDefects = (DataTable)Session["defectsData"];
                    if(rdoBreakdownReason.SelectedValue == "1")
                    if (dtDefects.Rows.Count > 0)
                    {
                        Common.Set_Procedures("sp_InsertDefects");
                        Common.Set_ParameterLength(22);
                        Common.Set_Parameters(
                            new MyParameter("@VesselCode", Session["CurrentShip"].ToString().Trim()),
                            new MyParameter("@ComponentId", ComponentId),
                            new MyParameter("@DefectNo", dtDefects.Rows[0]["DefectNo"].ToString().Trim()),
                            new MyParameter("@HistoryId", HistoryId),
                            new MyParameter("@ReportDt", dtDefects.Rows[0]["ReportDt"].ToString().Trim()),
                            new MyParameter("@TargetDt", dtDefects.Rows[0]["TargetDt"].ToString().Trim()),
                            new MyParameter("@CompletionDt", dtDefects.Rows[0]["CompletionDt"].ToString().Trim()),
                            new MyParameter("@Vessel", dtDefects.Rows[0]["Vessel"].ToString().Trim()),
                            new MyParameter("@Spares", dtDefects.Rows[0]["Spares"].ToString().Trim()),
                            new MyParameter("@ShoreAssist", dtDefects.Rows[0]["ShoreAssist"].ToString().Trim()),
                            new MyParameter("@Drydock", dtDefects.Rows[0]["Drydock"].ToString().Trim()),
                            new MyParameter("@Guarentee", dtDefects.Rows[0]["Guarentee"].ToString().Trim()),
                            new MyParameter("@DefectDetails", dtDefects.Rows[0]["DefectDetails"].ToString().Trim()),
                            new MyParameter("@RepairsDetails", dtDefects.Rows[0]["RepairsDetails"].ToString().Trim()),
                            new MyParameter("@SparesOnBoard", dtDefects.Rows[0]["SparesOnBoard"].ToString().Trim()),
                            new MyParameter("@RqnNo", dtDefects.Rows[0]["RqnNo"].ToString().Trim()),
                            new MyParameter("@RqnDate", dtDefects.Rows[0]["RqnDate"].ToString().Trim()),
                            new MyParameter("@SparesRequired", dtDefects.Rows[0]["SparesRequired"].ToString().Trim()),
                            new MyParameter("@Supdt", dtDefects.Rows[0]["Supdt"].ToString().Trim()),
                            new MyParameter("@ChiefOfficer", dtDefects.Rows[0]["ChiefOfficer"].ToString().Trim()),
                            new MyParameter("@ChiefEngg", dtDefects.Rows[0]["ChiefEngg"].ToString().Trim()),
                            new MyParameter("@CompStatus", rdoBreakdownReason.SelectedValue == "1" ? "W" : "N")
                            );

                        DataSet dsComponents = new DataSet();
                        dsComponents.Clear();
                        Boolean resDefects;
                        resDefects = Common.Execute_Procedures_IUD(dsComponents);
                        if (resDefects)
                        {
                            if (Session["SparesAdded"] != null)
                            {
                                DataTable dtSpares = (DataTable)Session["SparesAdded"];
                                if (dtSpares.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dtSpares.Rows)
                                    {
                                        Common.Set_Procedures("sp_InsertSpares_BreakDown");
                                        Common.Set_ParameterLength(7);
                                        Common.Set_Parameters(
                                            new MyParameter("@VesselCode", Session["CurrentShip"].ToString().Trim()),
                                            new MyParameter("@ComponentId", dr["ComponentId"].ToString()),
                                            new MyParameter("@Office_Ship", dr["Office_Ship"].ToString()),
                                            new MyParameter("@SpareId", dr["SpareId"].ToString()),
                                            new MyParameter("@DefectNo", dtDefects.Rows[0]["DefectNo"].ToString().Trim()),
                                            new MyParameter("@QtyCons", dr["QtyCons"].ToString()),
                                            new MyParameter("@QtyRob", dr["QtyRob"].ToString())
                                            );

                                        DataSet dsComponentSpare = new DataSet();
                                        dsComponentSpare.Clear();
                                        Boolean result;
                                        result = Common.Execute_Procedures_IUD(dsComponentSpare);
                                    }
                                }
                            }
                        }
                    }
                }
            //}
            lblfilesmessage.Text = "";
            lblfilesmessage.Visible = false;
            mbUpdateJob.ShowMessage("Job Updated Successfully.Upload all the required Documents before closing this screen.", false);
            UserDetails();
          
        }
        else
        {
            mbUpdateJob.ShowMessage("Unable to Update Job.Error :" + Common.getLastError(), true);
        }
    }
    //=====================================
    public int getLastHours(string CompCode)
    {
        int Hrs = 0;
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDate('" + VSlCode + "','" + CompCode + "',NULL)");
        if (dtStart.Rows.Count > 0)
            Hrs = Common.CastAsInt32(dtStart.Rows[0][0]);
        return Hrs;
    }
    public string getLastHoursDate(string CompCode)
    {
        string LastDt = "";
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT REPLACE(CONVERT(Varchar(11),dbo.getRHAsOnDate('" + VSlCode + "','" + CompCode + "',NULL),106),' ','-')");
        if (dtStart.Rows.Count > 0)
            LastDt = dtStart.Rows[0][0].ToString();
        return LastDt;
    }
    public int getLastHistoryHours(string CompCode)
    {
        int Hrs = 0;
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDateFromHistory('" + VSlCode + "','" + CompCode + "',NULL)");
        if (dtStart.Rows.Count > 0)
            Hrs = Common.CastAsInt32(dtStart.Rows[0][0]);
        return Hrs;
    }
    public int getLastHistoryHours(string CompCode, string OnDate)
    {
        int Hrs = 0;
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT dbo.getRunningHrsOnDateFromHistory('" + VSlCode + "','" + CompCode + "','" + OnDate + "')");
        if (dtStart.Rows.Count > 0)
            Hrs = Common.CastAsInt32(dtStart.Rows[0][0]);
        return Hrs;
    }
    public string getLastHistoryHoursDate(string CompCode)
    {
        string LastDt = "";
        string VSlCode = Session["CurrentShip"].ToString();
        DataTable dtStart = Common.Execute_Procedures_Select_ByQuery("SELECT REPLACE(CONVERT(Varchar(11),dbo.getRunningHrsOnDateFromHistoryDate('" + VSlCode + "','" + CompCode + "',NULL),106),' ','-')");
        if (dtStart.Rows.Count > 0)
            LastDt = dtStart.Rows[0][0].ToString();
        return LastDt;
    }
    //=====================================
    protected void txtDoneDate_TextChanged(object sender, EventArgs e)
    {
        DateTime dat;
        if (!(DateTime.TryParse(txtDoneDate.Text.Trim(), out dat)))
        {
            mbUpdateJob.ShowMessage("Please enter valid done date.", true);
            txtDoneDate.Focus();
            return;
        }
        string intervalType = lblInterval.Text.Split(' ').GetValue(1).ToString();
        int interval = Common.CastAsInt32(lblInterval.Text.Split(' ').GetValue(0).ToString());
        if (ddlRemarks.SelectedValue != "3")
        {
            if (intervalType == "H")
            {
                int intType = Common.CastAsInt32(hfIntervalId_H.Value);
                double dInterval_H = Convert.ToDouble(hfInterval_H.Value);
                int AvgRunningHrPerDay;

                string SQL = "SELECT TOP 1 *,UpdatedOn AS Updated1 FROM VSL_VesselRunningHourMaster WHERE VesselCode = '" + VesselCode + "' AND ComponentId =" + ComponentId + " ORDER BY UpdatedOn Desc";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                if (dt.Rows.Count > 0)
                {
                    AvgRunningHrPerDay = Common.CastAsInt32(dt.Rows[0]["AvgRunningHrPerDay"].ToString());
                    if (AvgRunningHrPerDay == 0)
                    {
                        return;
                    }

                    if (intType == 0)
                    {   
                        DateTime nextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval / AvgRunningHrPerDay);
                        txtNextDueDt.Text = nextDate.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        DateTime dtHNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval / AvgRunningHrPerDay);
                        DateTime dtORNextDate = DateTime.Now.Date;
                        switch (intType)
                        {
                            case 2:
                                dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(dInterval_H);
                                txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                                break;
                            case 3:
                                dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(dInterval_H * 7);
                                txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                                break;
                            case 4:
                                dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddMonths(Common.CastAsInt32(dInterval_H));
                                txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                                break;
                            case 5: 
                                dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddYears(Common.CastAsInt32(dInterval_H));
                                txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                                break;
                            default:
                                break;
                        }

                        if (dtHNextDate < dtORNextDate)
                        {
                            txtNextDueDt.Text = dtHNextDate.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                        }
                    }
                }
            }

            if (intervalType == "D")
            {
                DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval);
                txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

            }

            if (intervalType == "W")
            {
                DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval * 7);
                txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

            }

            if (intervalType == "M")
            {
                DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddMonths(interval);
                txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

            }

            if (intervalType == "Y")
            {
                DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddYears(interval);
                txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");
            }

        }
        else
        {
            if (rdoBreakdownReason.SelectedIndex != -1)
            {
                if (rdoBreakdownReason.SelectedValue == "1")
                {
                    if (intervalType == "H")
                    {
                        int intType = Common.CastAsInt32(hfIntervalId_H.Value);
                        double dInterval_H = Convert.ToDouble(hfInterval_H.Value);
                        int AvgRunningHrPerDay;                       

                        string SQL = "SELECT TOP 1 *,UpdatedOn AS Updated1 FROM VSL_VesselRunningHourMaster WHERE VesselCode = '" + VesselCode + "' AND ComponentId =" + ComponentId + " ORDER BY StartDate,Updated1 Desc";
                        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                        if (dt.Rows.Count > 0)
                        {
                            AvgRunningHrPerDay = Common.CastAsInt32(dt.Rows[0]["AvgRunningHrPerDay"].ToString());

                            if (AvgRunningHrPerDay == 0)
                            {
                                return;
                            }

                            if (intType == 0)
                            {
                                DateTime nextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval / AvgRunningHrPerDay);
                                txtNextDueDt.Text = nextDate.ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                DateTime dtHNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval / AvgRunningHrPerDay);
                                DateTime dtORNextDate = DateTime.Now.Date;
                                switch (intType)
                                {
                                    case 2:
                                        dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(dInterval_H);
                                        txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                                        break;
                                    case 3:
                                        dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(dInterval_H * 7);
                                        txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                                        break;
                                    case 4:
                                        dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddMonths(Common.CastAsInt32(dInterval_H));
                                        txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                                        break;
                                    case 5:
                                        dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddYears(Common.CastAsInt32(dInterval_H));
                                        txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                                        break;
                                    default:
                                        break;
                                }

                                if (dtHNextDate < dtORNextDate)
                                {
                                    txtNextDueDt.Text = dtHNextDate.ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
                                }
                            }
                        }
                    }

                    if (intervalType == "D")
                    {
                        DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval);
                        txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

                    }

                    if (intervalType == "W")
                    {
                        DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval * 7);
                        txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

                    }

                    if (intervalType == "M")
                    {
                        DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddMonths(interval);
                        txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

                    }

                    if (intervalType == "Y")
                    {
                        DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddYears(interval);
                        txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");
                    }
                }
                else
                {
                    txtNextDueDt.Text = txtDuedt.Text.Trim();
                }
            }
            else
            {
                mbUpdateJob.ShowMessage("Please select breakdown reason.", true);
                rdoBreakdownReason.Focus();
                txtDoneDate.Text = "";
            }
        }
    }
    protected void txtDoneHour_TextChanged(object sender, EventArgs e)
    {
        int i;
        if (!int.TryParse(txtDoneHour.Text.Trim(), out i))
        {
            mbUpdateJob.ShowMessage("Please enter valid hour.", true);
            txtDoneHour.Focus();
            return;
        }
        if (txtDoneHour.Text.Trim() != "")
        {
            string intervalType = lblInterval.Text.Split(' ').GetValue(1).ToString();
            int interval = Common.CastAsInt32(lblInterval.Text.Split(' ').GetValue(0).ToString());
            if (intervalType == "H")
            {
                if (ddlRemarks.SelectedValue != "3")
                {
                    txtNextHour.Text = Convert.ToString(Common.CastAsInt32(txtDoneHour.Text.ToString()) + interval);
                }
                else
                {
                    if (rdoBreakdownReason.SelectedValue == "1")
                    {
                        txtNextHour.Text = Convert.ToString(Common.CastAsInt32(txtDoneHour.Text.ToString()) + interval);
                    }
                    else
                    {
                        txtNextHour.Text = txtLastHour.Text;
                    }
                }
            }
        }
    }
    protected void btnAddDefectDetails_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openadddefects('" + lblUpdateComponent.Text.ToString().Split('-').GetValue(0).ToString().Trim() + "','" + JobId + "');", true);
    }

    #region SPARES ENTRY

    protected void ddlSparesReqd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSparesReqd.SelectedValue == "1")
        {
            BindCompSpares();
            tblSpares.Visible = true;

            //111111
            rptComponentSpares.DataSource = dtSpareDetails;
            rptComponentSpares.DataBind();
            //UpdatePanel4.Update();
        }
        else
        {
            tblSpares.Visible = false;
        }

    }
    protected void ddlSparesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSparesList.SelectedIndex != 0)
        {
            BindInventoryStatus();
        }
    }
    protected void btnAddSpare_Click(object sender, EventArgs e)
    {
        if (dtSpareDetails == null)
        {
            dtSpareDetails = SparesDataTable();
        }
        if (ddlSparesList.SelectedIndex == 0)
        {
            mbUpdateJob.ShowMessage("Please select a spare.", true);
            ddlSparesList.Focus();
            return;
        }
        if (dtSpareDetails != null)
        {
            foreach (DataRow row in dtSpareDetails.Rows)
            {
                if (row["RowId"].ToString() == ddlSparesList.SelectedValue.ToString())
                {
                    mbUpdateJob.ShowMessage("Selected spare already added.", true);
                    ddlSparesList.Focus();
                    return;
                }
            }
        }
        if (txtQtyCon.Text.Trim() == "")
        {
            mbUpdateJob.ShowMessage("Please enter consumed quantity.", true);
            txtQtyCon.Focus();
            return;
        }
        if (txtQtyRob.Text.Trim() == "")
        {
            mbUpdateJob.ShowMessage("Please enter rob quantity.", true);
            txtQtyRob.Focus();
            return;
        }
        rptComponentSpares.DataSource = null;
        rptComponentSpares.DataBind();
        
        string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;
        ProjectCommon.setSpareKeys(ddlSparesList.SelectedValue, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);

        string SQL = "SELECT VesselCode,ComponentId,Office_Ship,SpareId,SpareName,Maker,PartNo," + txtQtyCon.Text.Trim() + " AS QtyCons," + txtQtyRob.Text.Trim() + " AS QtyRob FROM VSL_VesselSpareMaster WHERE VesselCode = '" + _VesselCode + "' AND ComponentId = " + _ComponentId + " AND Office_Ship='" + _Office_Ship + "' AND SpareId =" + _SpareId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        DataRow dr = dtSpareDetails.NewRow();

        dr["RowId"] = ddlSparesList.SelectedValue;
        dr["VesselCode"] = dt.Rows[0]["VesselCode"].ToString();
        dr["ComponentId"] = dt.Rows[0]["ComponentId"].ToString();
        dr["Office_Ship"] = dt.Rows[0]["Office_Ship"].ToString();
        dr["SpareId"] = dt.Rows[0]["SpareId"].ToString();
        dr["SpareName"] = dt.Rows[0]["SpareName"].ToString();
        dr["Maker"] = dt.Rows[0]["Maker"].ToString();
        dr["PartNo"] = dt.Rows[0]["PartNo"].ToString();
        dr["QtyCons"] = dt.Rows[0]["QtyCons"].ToString();
        dr["QtyRob"] = dt.Rows[0]["QtyRob"].ToString();

        dtSpareDetails.Rows.Add(dr);
        if (dtSpareDetails.Rows[0]["SpareId"].ToString() == "")
        {
            dtSpareDetails.Rows[0].Delete();
        }
        dtSpareDetails.AcceptChanges();
        rptComponentSpares.DataSource = dtSpareDetails;
        rptComponentSpares.DataBind();
    }

    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        string spareid = ((ImageButton)sender).CommandArgument.Trim();    
        for(int i=0;i <=dtSpareDetails.Rows.Count -1;i++)
        {
            if (dtSpareDetails.Rows[i]["RowId"].ToString().Trim() == spareid)
            {
                dtSpareDetails.Rows.RemoveAt(i);
                break; 
            }
        }
        rptComponentSpares.DataSource = dtSpareDetails;
        rptComponentSpares.DataBind();
    }

    public DataTable SparesDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RowId");
        dt.Columns.Add("VesselCode");
        dt.Columns.Add("ComponentId");
        dt.Columns.Add("Office_Ship");
        dt.Columns.Add("SpareId");
        dt.Columns.Add("SpareName");
        dt.Columns.Add("Maker");
        dt.Columns.Add("PartNo");
        dt.Columns.Add("QtyCons");
        dt.Columns.Add("QtyRob");

        DataRow dr = dt.NewRow();
        dr["RowId"] = "";
        dr["VesselCode"] = "";
        dr["ComponentId"] = "";
        dr["Office_Ship"] = "";
        dr["SpareId"] = "";
        dr["SpareName"] = "";
        dr["Maker"] = "";
        dr["PartNo"] = "";
        dr["QtyCons"] = "";
        dr["QtyRob"] = "";

        dt.Rows.Add(dr);
        return dt;
    }
    protected void imgAddSpare_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + lblUpdateComponent.Text.Trim().Split('-').GetValue(0).ToString().Trim() + "','" + Session["CurrentShip"].ToString().Trim() + "',' ');", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindCompSpares();
    }

    #endregion

    #region ATTACHMENT

    protected void btnSaveAttachment_Click(object sender, EventArgs e)
    {
        if (HistoryId.Trim() == "")
        {
            mbUpdateJob.ShowMessage("First update the job.", true);
            return;
        }

        FileUpload img = (FileUpload)flAttachDocs;
        Byte[] imgByte = new Byte[0];
        string FileName = "";
        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;
            FileName = VesselCode + "_" + HistoryId.Trim() + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);

            Common.Set_Procedures("sp_InsertExecuteAttachment");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode.Trim()),
                new MyParameter("@HistoryId", HistoryId),
                new MyParameter("@AttachmentText", txtAttachmentText.Text.Trim()),
                new MyParameter("@FileName", FileName)

                );

            DataSet dsAttachment = new DataSet();
            dsAttachment.Clear();
            Boolean result;
            result = Common.Execute_Procedures_IUD(dsAttachment);
            if (result)
            {

                string path = Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(txtDoneDate.Text)));
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                flAttachDocs.SaveAs(path + FileName);

                trAttachments.Visible = true;
                BindAttachment();
                mbUpdateJob.ShowMessage("Document uploaded successfully." , false);
            }
            else
            {
                mbUpdateJob.ShowMessage("Unable to upload document.Error :" + Common.getLastError(), true);
            }
        }
        else
        {
            mbUpdateJob.ShowMessage("Please select a file to upload.", true);
            img.Focus();
            return;
        }
    }
    protected void DeleteAttachment_OnClick(object sender, EventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        string VesselCode = imgbtn.CssClass;
        string TableId = imgbtn.CommandArgument;
        Common.Set_Procedures("sp_DeleteExecuteAttachment");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@TableId", TableId),
            new MyParameter("@Mode", "P")
            );
        DataSet ds1 = new DataSet();
        Boolean res = Common.Execute_Procedures_IUD(ds1);
        if (res)
        {
            string FileName = ds1.Tables[0].Rows[0][0].ToString();
            FileName =Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(txtDoneDate.Text)) + FileName);
            if (File.Exists(FileName))
            {
                File.Delete(FileName);  
            }
            mbUpdateJob.ShowMessage("Attachment deleted successfully.", false);
            BindAttachment();
        }
        else
        {
            mbUpdateJob.ShowMessage("Unable to remove attachment.Error :" + Common.getLastError(), true);
        }
    }
    #endregion

    #endregion

}
