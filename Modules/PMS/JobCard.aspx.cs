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
using Ionic.Zip;
using System.Net.Mail;
using System.Net;
using System.Text;

public partial class JobCard : System.Web.UI.Page
{
    public bool IsVerified    
    {
        set { ViewState["IsVerified"] = value; }
        get { return Convert.ToBoolean(ViewState["IsVerified"]); }
    }
    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return ViewState["VesselCode"].ToString(); }
    }
    public Int32 HistoryId
    {
        set { ViewState["HistoryId"] = value; }
        get { return Convert.ToInt32(ViewState["HistoryId"]); }
    }
    public Int32 ComponentId
    {
        set { ViewState["_ComponentId"] = value; }
        get { return Convert.ToInt32(ViewState["_ComponentId"]); }
    }
    public static DataTable dtSpareDetails;
    public bool EditSpare
    {
        set { ViewState["_EditSpare"] = value; }
        get { return Convert.ToBoolean(ViewState["_EditSpare"]); }
    }
    public bool Modify
    {
        set { ViewState["_Modify"] = value; }
        get { return Convert.ToBoolean(ViewState["_Modify"]); }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        lblSaveMsg.Text = "";
        lblMSgSpare.Text = "";
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            if (Request.QueryString["VC"] != null && Request.QueryString["HID"] != null && Request.QueryString["RP"] != null)
            {
                if (Request.QueryString["RP"].ToString().Trim() == "R")
                {
                    dtSpareDetails = null;

                    VesselCode = Request.QueryString["VC"];
                    HistoryId =Convert.ToInt32(Request.QueryString["HID"]);
                    Modify = ((""+ Page.Request.QueryString["ModifySpare"]) == "Y");


                    plUpdateJobs.Visible = true;
                    lblPagetitle.Text = "Job Card";
                    BindRanks();
                    ShowReportHistoryDetails();
                    ShowRating();
                    ShowSpares();
                    ShowExecuteAttachments();
                    trAddAttachments.Visible = Session["UserType"].ToString() == "S" && (!IsVerified);                    
                    btnSave.Visible = Session["UserType"].ToString() == "S" && (!IsVerified);
                    ShowImages();
                    BindCompSpares();
                }
            }
        }
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
            _dt.Rows[_dt.Rows.Count - 1]["filename"] = "./" + path +  dr["filename"].ToString();
            _dt.Rows[_dt.Rows.Count - 1]["description"] = dr["description"].ToString();
        }
        dtimages = _dt;
        rptiamges.DataSource = dtimages;
        rptiamges.DataBind();
    }
    //  ------------------  Events
    protected void btnSaveAttachment_Click(object sender, EventArgs e)
    {
       FileUpload img = (FileUpload)flAttachDocs;
        Byte[] imgByte = new Byte[0];
        string FileName = "";
        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;
            FileName = Request.QueryString["VC"].ToString().Trim() + "_" + Request.QueryString["HID"].ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);

            Common.Set_Procedures("sp_InsertExecuteAttachment");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", Request.QueryString["VC"].ToString().Trim()),
                new MyParameter("@HistoryId", Request.QueryString["HID"].ToString()),
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

                trAddAttachments.Visible = true;
                BindAttachment();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "Document uploaded successfully.", false);
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "Unable to upload document", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "Please select a file to upload.", true);
            img.Focus();
            return;
        }
    }
    protected void btnVerify_OnClick(object sender, EventArgs e)
    {
        if (txtComments.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter comments.");
            return;
        }
        string UserName = Session["UserName"].ToString();
        Common.Set_Procedures("OfficeVerifyJobHistory");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", Page.Request.QueryString["VC"].ToString()),
            new MyParameter("@HistoryId", Page.Request.QueryString["HID"].ToString()),
            new MyParameter("@Comments", txtComments.Text.Trim().Replace("'","`")),
            new MyParameter("@VerifiedBy", UserName));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ProjectCommon.ShowMessage("Verifed Successfully.");
            Page.RegisterStartupScript("ss", "<script> window.opener.ReloadPage()</script>");
            ShowReportHistoryDetails();
        }
        else
        {
            ProjectCommon.ShowMessage("Unable to Verify.");
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
            new MyParameter("@Mode", "V")
            );
        DataSet ds1 = new DataSet();
        Boolean res = Common.Execute_Procedures_IUD(ds1);
        if (res)
        {
            string FileName = ds1.Tables[0].Rows[0][0].ToString();
            FileName = Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(txtDoneDate.Text)) + FileName);
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Attachment deleted successfully.');", true);
            ShowExecuteAttachments();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Unable to remove attachment.Error :" + Common.getLastError() + "');", true);
        }
    }

    //  ------------------  Function
    public void BindRanks()
    {
        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankLevel";
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);

        ddlRank.DataSource = dtRanks;
        ddlRank.DataTextField = "RankCode";
        ddlRank.DataValueField = "RankId";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, "< SELECT >");
        ddlRank.Items[0].Value = "0";
    }
    public void ShowReportHistoryDetails()
    {
        string strJobHistorySQL = "SELECT CM.ComponentCode,Cm.ComponentId,Cm.ComponentName,JM.JobCode,VCJM.DESCRM,CJM.DescrSh As JobName,JIM.IntervalName,VCJM.Interval, RM.RankCode AS DoneBy,REPLACE(Convert(Varchar, LastDueDate,106),' ','-') AS LastDueDate,LastDueHours,REPLACE(Convert(Varchar, JH.NextDueDate,106),' ','-') AS NextDueDate,NextDueHours,REPLACE(Convert(Varchar, LastRunningHourDate,106),' ','-') AS LastRunningHourDate,LastRunningHour,DoneBy_Code,DoneBy_Name,REPLACE(Convert(Varchar, DoneDate,106),' ','-') AS DoneDate,DoneHour,ServiceReport,ConditionBefore,ConditionAfter,UpdateRemarks,Specify,CASE UpdateRemarks WHEN 1 THEN 'Planned Job' WHEN 3 THEN 'BREAK DOWN' ELSE '' END AS Reason,((select UserName from ShipUserMaster where UserId=JH.ModifiedBy) + ' / ' + replace(convert(varchar,JH.ModifiedOn,106), ' ', '-')) as UpdatedByName,FileName,ISNULL(Verified,0) AS Verified,VerifiedBy,REPLACE(CONVERT(VARCHAR(15),VerifiedOn,106),' ','-') AS VerifiedOn, JH.DoneBy AS RankId,VCJM.IntervalId_H, VCJM.Interval_H,CompStatus FROM VSL_VesselJobUpdateHistory JH " +
                                  "INNER JOIN ComponentMaster CM ON CM.ComponentId = JH.ComponentId " +
                                  "INNER JOIN ComponentsJobMapping CJM ON CJM.CompJobId = JH.CompJobId " +
                                  "INNER JOIN JobMaster JM ON JM.jobId = JH.jobId " +
                                  "INNER JOIN Rank RM ON RM.RankId = JH.DoneBy " +
                                  "INNER JOIN VSL_VesselComponentJobMaster VCJM ON VCJM.VesselCode = JH.VesselCode AND VCJM.CompJobId = JH.CompJobId " +
                                  "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                                  "WHERE JH.VesselCode = '" + Request.QueryString["VC"].ToString().Trim() + "' AND JH.HistoryId =" + Request.QueryString["HID"].ToString() + " ";
        DataTable dtJobHistory = Common.Execute_Procedures_Select_ByQuery(strJobHistorySQL);
        if (dtJobHistory.Rows.Count > 0)
        {
            ComponentId = Common.CastAsInt32(dtJobHistory.Rows[0]["ComponentId"].ToString());
            ViewState["ComponentCode"] = dtJobHistory.Rows[0]["ComponentCode"].ToString();
            lblUpdateComponent.Text = dtJobHistory.Rows[0]["ComponentCode"].ToString() + " - " + dtJobHistory.Rows[0]["ComponentName"].ToString();
            lblUpdateInterval.Text = dtJobHistory.Rows[0]["Interval"].ToString() + " - " + dtJobHistory.Rows[0]["IntervalName"].ToString();
            lblUpdateJob.Text = dtJobHistory.Rows[0]["JobCode"].ToString() + " - " + dtJobHistory.Rows[0]["JobName"].ToString();

            txtEmpNo.Text = dtJobHistory.Rows[0]["DoneBy_Code"].ToString();
            txtEmpName.Text = dtJobHistory.Rows[0]["DoneBy_Name"].ToString();
            //lblRank.Text = dtJobHistory.Rows[0]["DoneBy"].ToString();
            ddlRank.SelectedValue = dtJobHistory.Rows[0]["RankId"].ToString();
            ddlRemarks.Text = dtJobHistory.Rows[0]["UpdateRemarks"].ToString();
            if (dtJobHistory.Rows[0]["UpdateRemarks"].ToString() == "2")
            {
                trSpecify.Visible = true;
                txtSpecify.Text = dtJobHistory.Rows[0]["Specify"].ToString();
            }
            else
            {
                trSpecify.Visible = false;
            }
            lblLastDoneDt.Text = dtJobHistory.Rows[0]["LastRunningHourDate"].ToString();
            lblInterval.Text = dtJobHistory.Rows[0]["Interval"].ToString() + " - " + dtJobHistory.Rows[0]["IntervalName"].ToString();
            lblJobDescr.Text = dtJobHistory.Rows[0]["DESCRM"].ToString();
            txtDoneHour.Text = dtJobHistory.Rows[0]["DoneHour"].ToString();
            txtNextHour.Text = dtJobHistory.Rows[0]["NextDueHours"].ToString();
            txtDuedt.Text = dtJobHistory.Rows[0]["LastDueDate"].ToString();
            txtDoneDate.Text = dtJobHistory.Rows[0]["DoneDate"].ToString();
            txtNextDueDt.Text = dtJobHistory.Rows[0]["NextDueDate"].ToString();
            txtServiceReport.Text = dtJobHistory.Rows[0]["ServiceReport"].ToString();
            txtCondBefore.Text = dtJobHistory.Rows[0]["ConditionBefore"].ToString();
            txtCondAfter.Text = dtJobHistory.Rows[0]["ConditionAfter"].ToString();
            lblUpdatedByOn.Text = dtJobHistory.Rows[0]["UpdatedByName"].ToString();

            hfIntervalId_H.Value = dtJobHistory.Rows[0]["IntervalId_H"].ToString();
            hfInterval_H.Value = dtJobHistory.Rows[0]["Interval_H"].ToString();

            DataTable dtBd = Common.Execute_Procedures_Select_ByQuery("SELECT [CompStatus] FROM Vsl_DefectDetailsMaster WHERE VESSELCODE='" + Request.QueryString["VC"].ToString().Trim() + "' AND HISTORYID=" + Common.CastAsInt32(Request.QueryString["HID"]));
            rdoBreakdownReason.SelectedIndex = -1;
            rdoBreakdownReason.Visible = false;
            if (dtBd.Rows.Count>0)
            {
                rdoBreakdownReason.Visible = true;
                string compstatus = dtBd.Rows[0]["CompStatus"].ToString();
                if (compstatus == "W")
                    rdoBreakdownReason.SelectedValue = "1";
                else if (compstatus == "N")
                    rdoBreakdownReason.SelectedValue = "2";
            }

            if (dtJobHistory.Rows[0]["IntervalName"].ToString() == "H")
            {
                trHr.Visible = true;
                txtLastHour.Text = (dtJobHistory.Rows[0]["LastRunningHour"].ToString() == "0" ? "" : dtJobHistory.Rows[0]["LastRunningHour"].ToString()); ;
            }
            else
            {
                trHr.Visible = false;
            }
            IsVerified = false;
            if (Session["UserType"].ToString() == "S")
            {
                if (dtJobHistory.Rows[0]["Verified"].ToString() == "True")
                {
                    IsVerified = true;
                    lblVerified.Text = dtJobHistory.Rows[0]["VerifiedBy"].ToString() + " / " + dtJobHistory.Rows[0]["VerifiedOn"].ToString();

                    trShipVerification.Visible = false;
                }

                if (!IsVerified && (Session["UserName"].ToString().Trim().ToUpper() == "MASTER" || Session["UserName"].ToString().Trim().ToUpper() == "CE"))
                {
                    trShipVerification.Visible = true;

                    if (dtJobHistory.Rows[0]["IntervalName"].ToString() == "H") 
                    {
                        lblVerifyNote.Visible = true;
                    }
                }

            }
            else
            {
                if (dtJobHistory.Rows[0]["Verified"].ToString() == "True")
                {
                    IsVerified = true;
                    lblVerified.Text = dtJobHistory.Rows[0]["VerifiedBy"].ToString() + " / " + dtJobHistory.Rows[0]["VerifiedOn"].ToString();
                }
            }


            string OfficeSQL = "select VerifiedBy + ' / ' +replace(convert(varchar,verifiedOn,106),' ','-') as Verify,Comments FROM VSL_VesselJobUpdateHistory_OfficeComments WHERE VESSELCODE='" + Request.QueryString["VC"].ToString().Trim() + "' AND HISTORYID=" + Request.QueryString["HID"].ToString().Trim();
            DataTable dtOfficeSQL = Common.Execute_Procedures_Select_ByQuery(OfficeSQL);

            if (Session["UserType"].ToString() == "S")
            {
                trOffVerifyLabel.Visible = true; trOffVerify.Visible = true;
                trOffCommLabel.Visible = false; trOffComm.Visible = false;
                if (dtOfficeSQL.Rows.Count > 0)
                {
                    lblOfficeVerified.Text = dtOfficeSQL.Rows[0][0].ToString();
                    lblRemark.Text = dtOfficeSQL.Rows[0][1].ToString();
                }
            }
            else
            {
                if (dtOfficeSQL.Rows.Count > 0)
                {
                    trOffVerifyLabel.Visible = true; trOffVerify.Visible = true;
                    trOffCommLabel.Visible = false; trOffComm.Visible = false;

                    lblOfficeVerified.Text = dtOfficeSQL.Rows[0][0].ToString();
                    lblRemark.Text = dtOfficeSQL.Rows[0][1].ToString();
                }
                else
                {
                    trOffVerifyLabel.Visible = false;
                    trOffVerify.Visible = false;
                    trOffCommLabel.Visible = true;
                    trOffComm.Visible = true;
                }
            }
        }
    }
    private void BindAttachment()
    {
        string strSQL = "SELECT * FROM VSL_VesselJobUpdateHistoryAttachments WHERE VesselCode = '" + Request.QueryString["VC"].ToString().Trim() + "' AND HistoryId = " + Request.QueryString["HID"].ToString() + " AND STATUS='A'";
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtAttachment.Rows.Count > 0)
        {
            rptAttachment.DataSource = dtAttachment;
            rptAttachment.DataBind();

            trAttachmentHeader.Visible = true;
            trAttachment.Visible = true;
        }
        else
        {
            rptAttachment.DataSource = null;
            rptAttachment.DataBind();
        }
    }
    public void ShowSpares()
    {
        string strSQL = "select jb.HistoryId,jb.VESSELCODE+'#'+CONVERT(VARCHAR,jb.COMPONENTID)+'#'+jb.OFFICE_SHIP+'#'+CONVERT(VARCHAR,jb.SPAREID) AS PKID ,SpareName,Maker,PartNo,QtyCons,QtyRob from dbo.VSL_VesselJobUpdateHistorySpareDetails jb LEFT JOIN dbo.VSL_VesselSpareMaster sp " +
                        "on jb.VesselCode=sp.VesselCode and jb.componentid=sp.componentid and sp.office_ship=jb.office_ship and jb.spareid=sp.spareid " +
                        "WHERE jb.VESSELCODE='" + Request.QueryString["VC"].ToString().Trim() + "' AND HISTORYID=" + Request.QueryString["HID"].ToString().Trim() + " order by sparename";
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);
        
        if (dtAttachment.Rows.Count > 0)
        {
            dtSpareDetails = dtAttachment;
            rptSpares.DataSource = dtAttachment;
            rptSpares.DataBind();
            //trSpare.Visible = true;
            //trSpareHeader.Visible = true;
        }
        else
        {
            rptSpares.DataSource = null;
            rptSpares.DataBind();
            dtSpareDetails = null;
        }

    }
    public void ShowExecuteAttachments()
    {
        string strSQL = "SELECT * FROM VSL_VesselJobUpdateHistoryAttachments WHERE VesselCode = '" + Request.QueryString["VC"].ToString().Trim() + "' AND HistoryId = " + Request.QueryString["HID"].ToString().Trim() + " And Status='A'";
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtAttachment.Rows.Count > 0)
        {
            rptAttachment.DataSource = dtAttachment;
            rptAttachment.DataBind();
            trAttachment.Visible = true;
            trAttachmentHeader.Visible = true;
        }
        else
        {
            rptAttachment.DataSource = null;
            rptAttachment.DataBind();

            trAttachment.Visible = false;
            trAttachmentHeader.Visible = false;
        }

    }
    public void ShowRating()
    {
        string strRatings = "SELECT CJM.RatingRequired, " +
                            "CASE JH.Coating WHEN 1 THEN 'Good' WHEN 2 THEN 'Fair' WHEN 3 THEN 'Poor' ELSE '' END AS Coating, " + 
                            "CASE JH.Corrosion WHEN 1 THEN 'None' WHEN 2 THEN 'Light' WHEN 3 THEN 'Medium' WHEN 4 THEN 'Heavy' ELSE '' END AS Corrosion, " + 
                            "CASE JH.Deformation WHEN 1 THEN 'None' WHEN 2 THEN 'Minor' WHEN 3 THEN 'Major' ELSE '' END AS Deformation, " + 
                            "CASE JH.Cracks WHEN 1 THEN 'None' WHEN 2 THEN 'Visible' ELSE '' END AS Cracks, " + 
                            "CASE JH.OverallRating WHEN 1 THEN 'Good' WHEN 2 THEN 'Fair' WHEN 3 THEN 'Poor' ELSE '' END AS OverallRating " + 
                            "FROM ComponentsJobMapping CJM " +
                            "INNER JOIN VSL_VesselJobUpdateHistory JH ON JH.CompJobId = CJM.CompJobId WHERE JH.HistoryId = " + Request.QueryString["HID"].ToString() + " ";
        
        DataTable dtRatings = Common.Execute_Procedures_Select_ByQuery(strRatings);
        if (dtRatings.Rows.Count > 0)
        {
            if (dtRatings.Rows[0]["RatingRequired"].ToString() == "True")
            {
                trRatingsHeader.Visible = true;
                trRating.Visible = true;

                lblCoating.Text = dtRatings.Rows[0]["Coating"].ToString();
                lblCorrosion.Text = dtRatings.Rows[0]["Corrosion"].ToString();
                lblDeformation.Text = dtRatings.Rows[0]["Deformation"].ToString();
                lblCracks.Text = dtRatings.Rows[0]["Cracks"].ToString();
                lblOAllRating.Text = dtRatings.Rows[0]["OverallRating"].ToString();
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtEmpNo.Text == "")
        {
            lblSaveMsg.Text = "Please enter employee number.";
            txtEmpNo.Focus();
            return;
        }
        Regex reg = new Regex("^[S/Y]\\d\\d\\d\\d\\d$");
        if (!reg.IsMatch(txtEmpNo.Text.Trim().ToUpper()))
        {
            lblSaveMsg.Text = "Please enter valid employee number.";
            txtEmpNo.Focus();
            return;
        }
        if (txtEmpName.Text == "")
        {
            lblSaveMsg.Text = "Please enter employee name.";
            txtEmpName.Focus();
            return;
        }
        if (ddlRank.SelectedIndex == 0)
        {
            lblSaveMsg.Text = "Please select rank.";
            ddlRank.Focus();
            return;
        }
        if (ddlRemarks.SelectedIndex == 0)
        {
            lblSaveMsg.Text = "Please select remarks.";
            ddlRemarks.Focus();
            return;
        }
        if (ddlRemarks.SelectedValue == "2")
        {
            if (txtSpecify.Text == "")
            {
                lblSaveMsg.Text = "Please specify remarks.";
                txtSpecify.Focus();
                return;
            }
        }
        if (ddlRemarks.SelectedValue == "3")
        {
            //if (Session["defectsData"] == null)
            //{
            //    mbUpdateJob.ShowMessage("Please add defects.", true);
            //    ddlRemarks.Focus();
            //    return;
            //}
            if (rdoBreakdownReason.SelectedIndex == -1)
            {
                lblSaveMsg.Text = "Please select equipment status.";
                rdoBreakdownReason.Focus();
                return;
            }
        }
        if (lblInterval.Text.Split(' ').GetValue(1).ToString() == "H")
        {
            if (txtDoneHour.Text == "")
            {
                lblSaveMsg.Text = "Please enter done hour.";
                txtDoneHour.Focus();
                return;
            }
            int i;
            if (!(int.TryParse(txtDoneHour.Text.Trim(), out i)))
            {
                lblSaveMsg.Text = "Please enter valid done hour.";
                txtDoneHour.Focus();
                return;
            }
            if (txtLastHour.Text != "")
            {
                if (int.Parse(txtDoneHour.Text.Trim()) < int.Parse(txtLastHour.Text.Trim()))
                {
                    lblSaveMsg.Text = "Done hour can not be less than last hour.";
                    txtDoneHour.Focus();
                    return;
                }
            }

        }
        if (txtDoneDate.Text == "")
        {
            lblSaveMsg.Text = "Please enter done date.";
            txtDoneDate.Focus();
            return;
        }
        DateTime dt;
        if (!(DateTime.TryParse(txtDoneDate.Text.Trim(), out dt)))
        {
            lblSaveMsg.Text = "Please enter valid done date.";
            txtDoneDate.Focus();
            return;
        }
        if ((DateTime.Parse(txtDoneDate.Text.Trim())) > DateTime.Today.Date)
        {
            lblSaveMsg.Text = "Done date can not be greater than today.";
            txtDoneDate.Focus();
            return;
        }
        if (lblLastDoneDt.Text.Trim() != "")
        {
            if ((DateTime.Parse(txtDoneDate.Text.Trim())) < DateTime.Parse(lblLastDoneDt.Text.Trim()))
            {
                lblSaveMsg.Text = "Done date can not be less than last done date.";
                txtDoneDate.Focus();
                return;
            }
        }
        if (lblInterval.Text.Split(' ').GetValue(1).ToString() == "H")
        {
            if (txtNextDueDt.Text == "")
            {
                lblSaveMsg.Text = "Running hour does no exist.can not calculate next due date.";
                txtNextDueDt.Focus();
                return;
            }
        }
        if (txtServiceReport.Text.Trim() == "")
        {
            lblSaveMsg.Text = "Please enter service report.";
            txtServiceReport.Focus();
            return;
        }
        if (txtServiceReport.Text.Trim().Length > 5000)
        {
            lblSaveMsg.Text = "Service report should not be more than 5000 characters.";
            txtServiceReport.Focus();
            return;
        }
        if (txtCondBefore.Text == "")
        {
            lblSaveMsg.Text = "Please enter before condition.";
            txtCondBefore.Focus();
            return;
        }
        if (txtCondAfter.Text == "")
        {
            lblSaveMsg.Text = "Please enter after condition.";
            txtCondAfter.Focus();
            return;
        }

        Common.Set_Procedures("sp_UpdateServiceReport");
        Common.Set_ParameterLength(15);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", Request.QueryString["VC"].ToString().Trim()),
            new MyParameter("@HistoryId", Request.QueryString["HID"].ToString()),
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
            new MyParameter("@NextDueDate", txtNextDueDt.Text),
            new MyParameter("@NextDueHours", txtNextHour.Text),
            new MyParameter("@UpdatedBy", Session["loginid"].ToString())            
            );
        DataSet dsUpdateJobPlanning = new DataSet();
        dsUpdateJobPlanning.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsUpdateJobPlanning);
        if (res)
        {
            lblSaveMsg.Text = "Service report updated successfully.";
            ScriptManager.RegisterStartupScript(this,this.GetType(),"aaa", "Refresh()",true);
        }
        else
        {
            lblSaveMsg.Text = "Unable to update service report.";
        }
    }

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
    }
    protected void txtDoneDate_TextChanged(object sender, EventArgs e)
    {
        DateTime dat;
        if (!(DateTime.TryParse(txtDoneDate.Text.Trim(), out dat)))
        {
            lblSaveMsg.Text = "Please enter valid done date.";
            txtDoneDate.Focus();
            return;
        }
        string intervalType = lblInterval.Text.Split('-').GetValue(1).ToString().Trim();
        int interval = Common.CastAsInt32(lblInterval.Text.Split('-').GetValue(0).ToString().Trim());
        if (ddlRemarks.SelectedValue != "3")
        {
            if (intervalType == "H")
            {
                int intType = Common.CastAsInt32(hfIntervalId_H.Value);
                double dInterval_H = Convert.ToDouble(hfInterval_H.Value);
                int AvgRunningHrPerDay;

                string SQL = "SELECT TOP 1 *,UpdatedOn AS Updated1 FROM VSL_VesselRunningHourMaster WHERE VesselCode = '" + Request.QueryString["VC"].ToString() + "' AND ComponentId =(SELECT ComponentId FROM ComponentMaster  WHERE ComponentCode = '" + lblUpdateComponent.Text.Split('-').GetValue(0).ToString().Trim() + "') ORDER BY UpdatedOn Desc";
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

                        string SQL = "SELECT TOP 1 *,UpdatedOn AS Updated1 FROM VSL_VesselRunningHourMaster WHERE VesselCode = '" + Request.QueryString["VC"].ToString() + "' AND ComponentId =(SELECT ComponentId FROM ComponentMaster  WHERE ComponentCode = '" + lblUpdateComponent.Text.Split('-').GetValue(0).ToString().Trim() + "') ORDER BY StartDate,Updated1 Desc";
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
                lblSaveMsg.Text = "Please select breakdown reason.";
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
            lblSaveMsg.Text = "Please enter valid hour.";
            txtDoneHour.Focus();
            return;
        }
        if (txtDoneHour.Text.Trim() != "")
        {
            string intervalType = lblInterval.Text.Split('-').GetValue(1).ToString().Trim();
            int interval = Common.CastAsInt32(lblInterval.Text.Split('-').GetValue(0).ToString().Trim());
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
    protected void chkVerifed_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ch = ((CheckBox)sender);
        string UserName = Session["UserName"].ToString();
        string VesselCode = Request.QueryString["VC"].ToString().Trim();
        string HistoryId = Request.QueryString["HID"].ToString();
        Common.Set_Procedures("VerifyJobHistory");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@HistoryId", HistoryId),
            new MyParameter("@VerifiedBy", UserName));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ProjectCommon.ShowMessage("Verifed Successfully.");
        }
        else
        {
            ProjectCommon.ShowMessage("Unable to Verify.");
        }
        ShowReportHistoryDetails();
        ShowExecuteAttachments();

        trAddAttachments.Visible = false;
        btnSave.Visible = false; 
    }
    
    //---------------------------06 April 2016 
    //working
    protected void btnSendAttachment_OnClick(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@RecordType", "JOBHISTORY-ATTACHMENTS"),
                new MyParameter("@RecordId",  HistoryId ),
                new MyParameter("@RecordNo", ViewState["ComponentCode"].ToString() + " - " + txtDoneDate.Text),
                new MyParameter("@CreatedBy", Session["UserName"].ToString())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('" + ds.Tables[0].Rows[0][0].ToString() + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Sent for export successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + Common.getLastError() + "');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + ex.Message + "');", true);
        }
    }

    public bool SendPacketMail(string FilePath, string Type)
    {
        string FileName = Path.GetFileName(FilePath);
        //MemoryStream ms = new MemoryStream();
        //using(FileStream fs=new FileStream(FilePath,FileMode.Open))
        //{
        //    CopyStream(fs,ms);
        //} 

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM MailSettings");
        if (dt.Rows.Count > 0)
        {
            string toaddress, senderaddress, senderusername, senderpass, mailclient;
            int port;
            bool encryption;
            toaddress = dt.Rows[0]["Toaddress"].ToString();
            senderaddress = dt.Rows[0]["SenderAddress"].ToString();
            senderusername = dt.Rows[0]["SenderUserName"].ToString();
            senderpass = dt.Rows[0]["senderPass"].ToString();
            mailclient = dt.Rows[0]["MailClient"].ToString();
            port = Common.CastAsInt32(dt.Rows[0]["Port"]);
            encryption = dt.Rows[0]["Mail_Encryption"].ToString() == "SSL";
            string[] CCAddress = { "emanager@energiossolutions.com" };
            string[] BCCAddress = { };
            string message = "Dear Receiver, <br><br> Please receive attached <b>" + Type + "</b> from ship email system.<br><br>Thanks <br />" + senderaddress;
            string Error = "";
            bool result = SendMailWithAttachment(senderaddress, toaddress, CCAddress, BCCAddress, "Ship Communication : " + Type, message, FilePath, FileName, senderusername, senderpass, mailclient, port, encryption, out Error);
            if (result)
            {
                lblMsg.Text = "Mail sent successfully.";

            }
            else
            {
                lblMsg.Text = "Unable to send mail. Error : " + Error;
            }

            return result;
        }
        else
        {
            lblMsg.Text = "Settings not found. Please save settings before send mail.";
            return false;
        }
    }
    public bool SendMailWithAttachment(string FromMail, string toAddress, string[] CCAddresses, string[] BCCAddresses, string Subject, string BodyText, string FilePath, string AttachmentFileName, string senderusername, string senderpass, string HostName, int Port, bool SSL, out string Error)
    {
        Error = "";
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient(HostName, Port);
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();
            objMessage.From = objfromAddress;
            objSmtpClient.Credentials = new NetworkCredential(senderusername, senderpass);
            try
            {
                objMessage.To.Add(toAddress);
                objMessage.Body = BodyText;
                objMessage.Subject = Subject;
                objMessage.IsBodyHtml = true;

                foreach (string CCadrs in CCAddresses)
                {
                    objMessage.CC.Add(CCadrs);
                }

                foreach (string adrs in BCCAddresses)
                {
                    objMessage.Bcc.Add(adrs);
                }
                if (AttachmentFileName != "")
                {
                    Attachment attachFile = new Attachment(FilePath);
                    objMessage.Attachments.Add(attachFile);
                }

                //objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //objSmtpClient.Send(objMessage);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            return false;
        }
    }


    //------------------
    protected void ddlSparesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSparesList.SelectedIndex != 0)
        {
            BindInventoryStatus();
        }
    }
    protected void btnAddSpare_Click(object sender, EventArgs e)
    {
        //if (dtSpareDetails == null)
        //{
        //    dtSpareDetails = SparesDataTable();
        //}
        if (ddlSparesList.SelectedIndex == 0)
        {
            lblMSgSpare.Text = "Please select a spare.";
            ddlSparesList.Focus();
            return;
        }
        if (!EditSpare)
        {
            //foreach (DataRow row in dtSpareDetails.Rows)
            //{
            //    if (row["RowId"].ToString() == ddlSparesList.SelectedValue.ToString())
            //    {
            //        lblSaveMsg.Text = "Selected spare already added.";
            //        ddlSparesList.Focus();
            //        return;
            //    }
            //}
            foreach (RepeaterItem  itm in rptSpares.Items)
            {
                HiddenField hdid = (HiddenField)itm.FindControl("SparePK");
                if (hdid.Value == ddlSparesList.SelectedValue.ToString())
                {
                    lblMSgSpare.Text = "Selected spare already added.";
                    ddlSparesList.Focus();
                    return;
                }
            }
        }
        if (txtQtyCon.Text.Trim() == "")
        {
            lblMSgSpare.Text = "Please enter consumed quantity.";
            txtQtyCon.Focus();
            return;
        }
        if (txtQtyRob.Text.Trim() == "")
        {
            lblMSgSpare.Text = "Please enter rob quantity.";
            txtQtyRob.Focus();
            return;
        }
        rptSpares.DataSource = null;
        rptSpares.DataBind();

        string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;
        ProjectCommon.setSpareKeys(ddlSparesList.SelectedValue, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);

        //string SQL = "SELECT VesselCode,ComponentId,Office_Ship,SpareId,SpareName,Maker,PartNo," + txtQtyCon.Text.Trim() + " AS QtyCons," + txtQtyRob.Text.Trim() + " AS QtyRob FROM VSL_VesselSpareMaster WHERE VesselCode = '" + _VesselCode + "' AND ComponentId = " + _ComponentId + " AND Office_Ship='" + _Office_Ship + "' AND SpareId =" + _SpareId;
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        //DataRow dr = dtSpareDetails.NewRow();

        //dr["RowId"] = ddlSparesList.SelectedValue;
        //dr["VesselCode"] = dt.Rows[0]["VesselCode"].ToString();
        //dr["ComponentId"] = dt.Rows[0]["ComponentId"].ToString();
        //dr["Office_Ship"] = dt.Rows[0]["Office_Ship"].ToString();
        //dr["SpareId"] = dt.Rows[0]["SpareId"].ToString();
        //dr["SpareName"] = dt.Rows[0]["SpareName"].ToString();
        //dr["Maker"] = dt.Rows[0]["Maker"].ToString();
        //dr["PartNo"] = dt.Rows[0]["PartNo"].ToString();
        //dr["QtyCons"] = dt.Rows[0]["QtyCons"].ToString();
        //dr["QtyRob"] = dt.Rows[0]["QtyRob"].ToString();

        //dtSpareDetails.Rows.Add(dr);
        //if (dtSpareDetails.Rows[0]["SpareId"].ToString() == "")
        //{
        //    dtSpareDetails.Rows[0].Delete();
        //}
        //dtSpareDetails.AcceptChanges();
        //rptSpares.DataSource = dtSpareDetails;
        //rptSpares.DataBind();


        Common.Set_Procedures("sp_InsertSpares_Execute");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", Session["CurrentShip"].ToString().Trim()),
            new MyParameter("@ComponentId", _ComponentId),
            new MyParameter("@Office_Ship", _Office_Ship),
            new MyParameter("@SpareId", _SpareId),
            new MyParameter("@HistoryId", HistoryId),
            new MyParameter("@QtyCons", txtQtyCon.Text.Trim()),
            new MyParameter("@QtyRob", txtQtyRob.Text.Trim())
            );

        DataSet dsComponentSpare = new DataSet();
        dsComponentSpare.Clear();
        Boolean result;
        result = Common.Execute_Procedures_IUD(dsComponentSpare);
        if (result)
        {
            lblMSgSpare.Text = "Spare added successfully.";
            EditSpare = false;
            SetEditSpareControlState();
            ShowSpares();
            ddlSparesList.SelectedIndex = 0;
            txtQtyCon.Text = "";
            txtQtyRob.Text = "";
        }
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindCompSpares();
    }
    protected void imgDeleteSpare_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField SparePK = (HiddenField)btn.Parent.FindControl("SparePK");
        HiddenField hfHistoryId = (HiddenField)btn.Parent.FindControl("hfHistoryId");

        string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;

        //
        ProjectCommon.setSpareKeys(SparePK.Value, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);
        string sql = " delete from  VSL_VesselJobUpdateHistorySpareDetails where VesselCode='" + Session["CurrentShip"].ToString().Trim() + "' and ComponentId=" + _ComponentId + " and Office_Ship='" + _Office_Ship + "' and SpareId=" + _SpareId + " and HistoryId=" + hfHistoryId.Value + " ";
        DataTable dtspares = Common.Execute_Procedures_Select_ByQuery(sql);
        lblMSgSpare.Text = "Spare deleted successfully.";
        ShowSpares();
    }
    protected void imgEditSpare_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField SparePK = (HiddenField)btn.Parent.FindControl("SparePK");
        HiddenField hfHistoryId = (HiddenField)btn.Parent.FindControl("hfHistoryId");

        Label lblQtyCons = (Label)btn.Parent.FindControl("lblQtyCons");
        Label lblQtyRob = (Label)btn.Parent.FindControl("lblQtyRob");

        string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;
        ProjectCommon.setSpareKeys(SparePK.Value, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);

        ddlSparesList.SelectedValue = SparePK.Value;
        txtQtyCon.Text = lblQtyCons.Text;
        txtQtyRob.Text = lblQtyRob.Text;
        EditSpare = true;        
        SetEditSpareControlState();
    }
    protected void btnClearSpare_Click(object sender, EventArgs e)
    {
        ddlSparesList.SelectedIndex = 0;
        txtQtyCon.Text = "";
        txtQtyRob.Text = "";

        EditSpare = false;
        SetEditSpareControlState();
    }
    protected void imgAddSpare_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + lblUpdateComponent.Text.Trim().Split('-').GetValue(0).ToString().Trim() + "','" + Session["CurrentShip"].ToString().Trim() + "',' ');", true);
    }

    private void BindInventoryStatus()
    {
        if (ddlSparesList.SelectedIndex > 0)
        {
            txtQtyRob.Text = ProjectCommon.getROB(ddlSparesList.SelectedValue, DateTime.Today).ToString();
        }
        else
        {
            txtQtyRob.Text = "";
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
    public void SetEditSpareControlState()
    {
        if (EditSpare)
        {
            btnAddSpare.Text = "Update";
            ddlSparesList.Enabled = false;
        }
        else
        {
            btnAddSpare.Text = "Add";
            ddlSparesList.Enabled = true;
        }
    }
}
