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

public partial class JobCard_Office : System.Web.UI.Page
{
    public bool IsVerified
    {
        set { ViewState["IsVerified"] = value; }
        get { return Convert.ToBoolean(ViewState["IsVerified"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSaveMsg.Text = "";
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            if (Request.QueryString["VC"] != null && Request.QueryString["HID"] != null && Request.QueryString["RP"] != null)
            {
                if (Request.QueryString["RP"].ToString().Trim() == "R")
                {
                    plUpdateJobs.Visible = true;
                    //BindRanks();
                    ShowReportHistoryDetails();
                    ShowRating();
                    ShowSpares();
                    ShowExecuteAttachments();
                    trAddAttachments.Visible = Session["UserType"].ToString() == "S"; // && (!IsVerified);
                    btnSave.Visible = Session["UserType"].ToString() == "S";
                    //ShowImages();
                }
            }
        }

    }
    public void ShowImages()
    {
        DataTable _dt = new DataTable();
        _dt.Columns.Add("filename", typeof(string));
        _dt.Columns.Add("description", typeof(string));
        string path = ProjectCommon.getUploadFolder(DateTime.Parse(lblDoneDate.Text));
        DataTable dtimages = Common.Execute_Procedures_Select_ByQuery("select attachmentid,filename,description from dbo.VSL_JobExecAttachmentsDetails	where vesselcode='" + Request.QueryString["VC"].ToString().Trim() + "' and historyid=" + Request.QueryString["HID"].ToString());
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

                string path = Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(lblDoneDate.Text)));
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
            txtComments.Focus();
            return;
        }
        if (ddlflpNeeded.SelectedIndex == 0)
        {
            ProjectCommon.ShowMessage("Please select follow up job.");
            ddlflpNeeded.Focus();
            return;
        }
        if (ddlNextVisit.Enabled)
        {
            if (ddlNextVisit.SelectedIndex == 0)
            {
                ProjectCommon.ShowMessage("Please answer the selected question.");
                ddlNextVisit.Focus();
                return;
            }
        }
        string Rating = "";
        if (radA.Enabled & radB.Enabled & radC.Enabled & radD.Enabled)
        {
            Rating = (radA.Checked) ? "A" : ((radB.Checked) ? "B" : ((radC.Checked) ? "C" : ((radD.Checked) ? "D" : "")));
            if (Rating == "")
            {
                ProjectCommon.ShowMessage("Please select condition of equipment."); return;
            }

        }

        

        string UserName = Session["UserName"].ToString();

        Common.Set_Procedures("OfficeVerifyJobHistory");
        Common.Set_ParameterLength(7);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", Page.Request.QueryString["VC"].ToString()),
            new MyParameter("@HistoryId", Page.Request.QueryString["HID"].ToString()),
            new MyParameter("@Comments", txtComments.Text.Trim().Replace("'", "`")),
            new MyParameter("@ReFollowUp", ddlflpNeeded.SelectedValue),
            new MyParameter("@VerifyNextVisit", (ddlNextVisit.SelectedValue)),
            new MyParameter("@Rating",Rating ),
            new MyParameter("@VerifiedBy", UserName));
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD(ds))
        {
            ProjectCommon.ShowMessage("Verifed Successfully.");
            Page.ClientScript.RegisterStartupScript(this.GetType(),"ss","window.opener.ReloadPage();",true);
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
            FileName = Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(lblDoneDate.Text)) + FileName);
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
    //public void BindRanks()
    //{
    //    DataTable dtRanks = new DataTable();
    //    string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankLevel";
    //    dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);

    //    ddlRank.DataSource = dtRanks;
    //    ddlRank.DataTextField = "RankCode";
    //    ddlRank.DataValueField = "RankId";
    //    ddlRank.DataBind();
    //    ddlRank.Items.Insert(0, "< SELECT >");
    //    ddlRank.Items[0].Value = "0";
    //}
    public void ShowReportHistoryDetails()
    {
        string strJobHistorySQL = "SELECT CM.ComponentCode,Cm.ComponentName,JM.JobCode,VCJM.DESCRM,CJM.DescrSh As JobName,JIM.IntervalName,VCJM.Interval, RM.RankCode AS DoneBy,REPLACE(Convert(Varchar, LastDueDate,106),' ','-') AS LastDueDate,LastDueHours,REPLACE(Convert(Varchar, JH.NextDueDate,106),' ','-') AS NextDueDate,NextDueHours,REPLACE(Convert(Varchar, LastRunningHourDate,106),' ','-') AS LastRunningHourDate,LastRunningHour,DoneBy_Code,DoneBy_Name,REPLACE(Convert(Varchar, DoneDate,106),' ','-') AS DoneDate,DoneHour,ServiceReport,ConditionBefore,ConditionAfter,UpdateRemarks,Specify,CASE UpdateRemarks WHEN 1 THEN 'Planned Job' WHEN 3 THEN 'BREAK DOWN' ELSE '' END AS Reason,((select UserName from ShipUserMaster where UserId=JH.ModifiedBy) + ' / ' + replace(convert(varchar,JH.ModifiedOn,106), ' ', '-')) as UpdatedByName,FileName,ISNULL(Verified,0) AS Verified,VerifiedBy,REPLACE(CONVERT(VARCHAR(15),VerifiedOn,106),' ','-') AS VerifiedOn, JH.DoneBy AS RankId,VCJM.IntervalId_H, VCJM.Interval_H " +
				",(SELECT TOP 1 DONEHOUR FROM VSL_VesselJobUpdateHistory JH1 WHERE JH1.VesselCode = JH.VesselCode AND JH1.COMPJOBID=JH.COMPJOBID AND JH1.HistoryId <JH.HistoryId ORDER BY DONEHOUR DESC) AS LASTCALCHRS " +
				  "FROM VSL_VesselJobUpdateHistory JH " +
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
            lblVesselCode.Text = Request.QueryString["VC"].ToString().Trim();
            lblUpdateComponent.Text = dtJobHistory.Rows[0]["ComponentCode"].ToString() + " - " + dtJobHistory.Rows[0]["ComponentName"].ToString();
            lblUpdateInterval.Text = dtJobHistory.Rows[0]["Interval"].ToString() + " - " + dtJobHistory.Rows[0]["IntervalName"].ToString();
            lblUpdateJob.Text = dtJobHistory.Rows[0]["JobCode"].ToString() + " - " + dtJobHistory.Rows[0]["JobName"].ToString();

            lblEmpNo.Text = dtJobHistory.Rows[0]["DoneBy_Code"].ToString();
            lblEmpName.Text = dtJobHistory.Rows[0]["DoneBy_Name"].ToString();
            lblRank.Text = dtJobHistory.Rows[0]["DoneBy"].ToString();
            //ddlRank.SelectedValue = dtJobHistory.Rows[0]["RankId"].ToString();

            // lblRemarks.Text = dtJobHistory.Rows[0]["UpdateRemarks"].ToString();
            if (dtJobHistory.Rows[0]["UpdateRemarks"].ToString() == "2")
            {
                trSpecify.Visible = true;
                lblSpecify.Text = dtJobHistory.Rows[0]["Specify"].ToString();
            }
            else
            {
                if(dtJobHistory.Rows[0]["UpdateRemarks"].ToString() == "1")
                    lblRemarks.Text = "Planned";

                if (dtJobHistory.Rows[0]["UpdateRemarks"].ToString() == "3")
                    lblRemarks.Text = "Breakdown";

                trSpecify.Visible = false;
            }

            lblLastDoneDt.Text = dtJobHistory.Rows[0]["LastDueDate"].ToString();
            lblInterval.Text = dtJobHistory.Rows[0]["Interval"].ToString() + " - " + dtJobHistory.Rows[0]["IntervalName"].ToString();
            lblJobDescr.Text = dtJobHistory.Rows[0]["DESCRM"].ToString();
            lblDoneHour.Text = dtJobHistory.Rows[0]["DoneHour"].ToString();
            lblNextHour.Text = dtJobHistory.Rows[0]["NextDueHours"].ToString();
            lblDuedt.Text = dtJobHistory.Rows[0]["LastDueDate"].ToString();
            lblDoneDate.Text = dtJobHistory.Rows[0]["DoneDate"].ToString();
            lblNextDueDt.Text = dtJobHistory.Rows[0]["NextDueDate"].ToString();

            lblServiceReport.Text = dtJobHistory.Rows[0]["ServiceReport"].ToString();
            lblCondBefore.Text = dtJobHistory.Rows[0]["ConditionBefore"].ToString();
            lblCondAfter.Text = dtJobHistory.Rows[0]["ConditionAfter"].ToString();
            lblUpdatedByOn.Text = dtJobHistory.Rows[0]["UpdatedByName"].ToString();

            hfIntervalId_H.Value = dtJobHistory.Rows[0]["IntervalId_H"].ToString();
            hfInterval_H.Value = dtJobHistory.Rows[0]["Interval_H"].ToString();

            if (dtJobHistory.Rows[0]["IntervalName"].ToString() == "H")
            {
                trHr.Visible = true;
                //lblLastHour.Text = (dtJobHistory.Rows[0]["LastDueHours"].ToString() == "0" ? "" : dtJobHistory.Rows[0]["LastDueHours"].ToString()); ;
                lblLastHour.Text = (dtJobHistory.Rows[0]["LASTCALCHRS"].ToString() == "0" ? "" : dtJobHistory.Rows[0]["LASTCALCHRS"].ToString()); ;
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


            string OfficeSQL = "select VerifiedBy + ' / ' +replace(convert(varchar,verifiedOn,106),' ','-') as Verify,Comments,Verified,verifiedOn FROM VSL_VesselJobUpdateHistory_OfficeComments WHERE VESSELCODE='" + Request.QueryString["VC"].ToString().Trim() + "' AND HISTORYID=" + Request.QueryString["HID"].ToString().Trim();
            DataTable dtOfficeSQL = Common.Execute_Procedures_Select_ByQuery(OfficeSQL);

            string NextVisitSQL = " select VesselCode,HistoryId,replace(CONVERT(varchar,RecdOn,106),' ','-')RecdOn,case when isnull(NextVisitVerify,0)=1 then 'Yes' else 'No' end as NextVisitVerify,Rating from " +
                                  "  VSL_VesselJobUpdateHistory_OBVerification where VesselCode='" + Request.QueryString["VC"].ToString().Trim() + "' and HistoryId=" + Request.QueryString["HID"].ToString().Trim() + " ";
            DataTable dtNextVisit = Common.Execute_Procedures_Select_ByQuery(NextVisitSQL);
            
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
                
               

                if (dtNextVisit.Rows.Count > 0)
                {
                    lblNextVisitVerified.Text = dtNextVisit.Rows[0]["NextVisitVerify"].ToString();
                    lblRating.Text = dtNextVisit.Rows[0]["Rating"].ToString();
                    lblRecievedOn.Text = dtNextVisit.Rows[0]["RecdOn"].ToString();
                }

                if (dtOfficeSQL.Rows.Count > 0)
                {
                    bool OfficeVerified = (dtOfficeSQL.Rows[0]["Verified"].ToString() == "True");
                    
                    lblFUPShip.Text = ((!OfficeVerified) && (!Convert.IsDBNull(dtOfficeSQL.Rows[0]["verifiedOn"]))) ? "Yes" : "No";
                    if (lblFUPShip.Text != "")
                        ddlflpNeeded.SelectedValue = lblFUPShip.Text.Substring(0,1).ToUpper();

                    
                    if(OfficeVerified)
                    {
                        trOffCommLabel.Visible = false; 
                        trOffComm.Visible = false;
                    }
                    else
                    {
                        trOffCommLabel.Visible = true;
                        trOffComm.Visible = true;
                    }

                    trOffVerifyLabel.Visible = true; trOffVerify.Visible = true;

                    lblOfficeVerified.Text = dtOfficeSQL.Rows[0][0].ToString();
                    lblRemark.Text = dtOfficeSQL.Rows[0][1].ToString();

                    lblOfficeVerified.Text = dtOfficeSQL.Rows[0][0].ToString();
                    lblRemark.Text = dtOfficeSQL.Rows[0][1].ToString();
                    txtComments.Text = lblRemark.Text;


                    
                    if (dtNextVisit.Rows.Count > 0)
                    {
                        //if (dtNextVisit.Rows[0]["Rating"].ToString() == "A") radA.Checked = true;
                        //if (dtNextVisit.Rows[0]["Rating"].ToString() == "B") radB.Checked = true;
                        //if (dtNextVisit.Rows[0]["Rating"].ToString() == "C") radC.Checked = true;
                        //if (dtNextVisit.Rows[0]["Rating"].ToString() == "D") radD.Checked = true;


                        //if (dtNextVisit.Rows[0]["NextVisitVerify"].ToString() == "Yes")
                        //    chkNextVisit.Checked = true;
                        //else
                        //    chkNextVisit.Checked = false;

                        //chkNextVisit.Enabled = false;
                        //radA.Enabled = false; radB.Enabled = false; radC.Enabled = false; radD.Enabled = false;
                        
                    }
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
    protected void btnSendForOBVerification_OnClick(object sender, EventArgs e)
    {

    }
    private void BindAttachment()
    {
        string strSQL = "SELECT * FROM VSL_VesselJobUpdateHistoryAttachments WHERE VesselCode = '" + Request.QueryString["VC"].ToString().Trim() + "' AND HistoryId = " + Request.QueryString["HID"].ToString() + " AND STATUS='A'";
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtAttachment.Rows.Count > 0)
        {
            rptAttachment.DataSource = dtAttachment;
            rptAttachment.DataBind();
        }
        else
        {
            rptAttachment.DataSource = null;
            rptAttachment.DataBind();
        }
    }
    public void ShowSpares()
    {
        string strSQL = "select SpareName,Maker,PartNo,QtyCons,QtyRob from dbo.VSL_VesselJobUpdateHistorySpareDetails jb LEFT JOIN dbo.VSL_VesselSpareMaster sp " +
                        "on jb.VesselCode=sp.VesselCode and jb.componentid=sp.componentid and sp.office_ship=jb.office_ship and jb.spareid=sp.spareid " +
                        "WHERE jb.VESSELCODE='" + Request.QueryString["VC"].ToString().Trim() + "' AND HISTORYID=" + Request.QueryString["HID"].ToString().Trim() + " order by sparename";
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtAttachment.Rows.Count > 0)
        {
            rptSpares.DataSource = dtAttachment;
            rptSpares.DataBind();
            trSpare.Visible = true;
            trSpareHeader.Visible = true;
        }
        else
        {
            rptSpares.DataSource = null;
            rptSpares.DataBind();
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
        }

    }
    
    protected void btnRejectJob_Click(object sender,EventArgs e)
    {
        divCorrection.Visible=true;
    }
    protected void btnCancelRejection_Click(object sender,EventArgs e)
    {
        divCorrection.Visible=false;
        txtRemarks.Text="";
    }
    protected void btnSaveRejection_Click(object sender,EventArgs e)
    {
        if(txtRemarks.Text.Trim()=="")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Va", "alert('Please enter your comments.');", true);
            return;
        }
        Common.Set_Procedures("DBO.JobCorrection_Init");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", Request.QueryString["VC"].ToString()),
            new MyParameter("@HistoryId", Request.QueryString["HID"].ToString()),
            new MyParameter("@Remarks",txtRemarks.Text),
            new MyParameter("@UserName", Session["FullName"].ToString())
            );
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "aD", "alert('Correction planned successfully.');", true);
            btnCancelRejection_Click(sender,e);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Va", "alert('Unable to plan correction.');", true);
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
        //if (txtEmpNo.Text == "")
        //{
        //    lblSaveMsg.Text = "Please enter employee number.";
        //    txtEmpNo.Focus();
        //    return;
        //}
        //Regex reg = new Regex("^[S/Y]\\d\\d\\d\\d\\d$");
        //if (!reg.IsMatch(txtEmpNo.Text.Trim().ToUpper()))
        //{
        //    lblSaveMsg.Text = "Please enter valid employee number.";
        //    txtEmpNo.Focus();
        //    return;
        //}
        //if (txtEmpName.Text == "")
        //{
        //    lblSaveMsg.Text = "Please enter employee name.";
        //    txtEmpName.Focus();
        //    return;
        //}
        //if (ddlRank.SelectedIndex == 0)
        //{
        //    lblSaveMsg.Text = "Please select rank.";
        //    ddlRank.Focus();
        //    return;
        //}
        //if (ddlRemarks.SelectedIndex == 0)
        //{
        //    lblSaveMsg.Text = "Please select remarks.";
        //    ddlRemarks.Focus();
        //    return;
        //}
        //if (ddlRemarks.SelectedValue == "2")
        //{
        //    if (txtSpecify.Text == "")
        //    {
        //        lblSaveMsg.Text = "Please specify remarks.";
        //        txtSpecify.Focus();
        //        return;
        //    }
        //}
        //if (ddlRemarks.SelectedValue == "3")
        //{
        //    //if (Session["defectsData"] == null)
        //    //{
        //    //    mbUpdateJob.ShowMessage("Please add defects.", true);
        //    //    ddlRemarks.Focus();
        //    //    return;
        //    //}
        //    if (rdoBreakdownReason.SelectedIndex == -1)
        //    {
        //        lblSaveMsg.Text = "Please select equipment status.";
        //        rdoBreakdownReason.Focus();
        //        return;
        //    }
        //}
        //if (lblInterval.Text.Split(' ').GetValue(1).ToString() == "H")
        //{
        //    if (txtDoneHour.Text == "")
        //    {
        //        lblSaveMsg.Text = "Please enter done hour.";
        //        txtDoneHour.Focus();
        //        return;
        //    }
        //    int i;
        //    if (!(int.TryParse(txtDoneHour.Text.Trim(), out i)))
        //    {
        //        lblSaveMsg.Text = "Please enter valid done hour.";
        //        txtDoneHour.Focus();
        //        return;
        //    }
        //    if (txtLastHour.Text != "")
        //    {
        //        if (int.Parse(txtDoneHour.Text.Trim()) < int.Parse(txtLastHour.Text.Trim()))
        //        {
        //            lblSaveMsg.Text = "Done hour can not be less than last hour.";
        //            txtDoneHour.Focus();
        //            return;
        //        }
        //    }

        //}
        //if (txtDoneDate.Text == "")
        //{
        //    lblSaveMsg.Text = "Please enter done date.";
        //    txtDoneDate.Focus();
        //    return;
        //}
        //DateTime dt;
        //if (!(DateTime.TryParse(txtDoneDate.Text.Trim(), out dt)))
        //{
        //    lblSaveMsg.Text = "Please enter valid done date.";
        //    txtDoneDate.Focus();
        //    return;
        //}
        //if ((DateTime.Parse(txtDoneDate.Text.Trim())) > DateTime.Today.Date)
        //{
        //    lblSaveMsg.Text = "Done date can not be greater than today.";
        //    txtDoneDate.Focus();
        //    return;
        //}
        //if (lblLastDoneDt.Text.Trim() != "")
        //{
        //    if ((DateTime.Parse(txtDoneDate.Text.Trim())) < DateTime.Parse(lblLastDoneDt.Text.Trim()))
        //    {
        //        lblSaveMsg.Text = "Done date can not be less than last done date.";
        //        txtDoneDate.Focus();
        //        return;
        //    }
        //}
        //if (lblInterval.Text.Split(' ').GetValue(1).ToString() == "H")
        //{
        //    if (txtNextDueDt.Text == "")
        //    {
        //        lblSaveMsg.Text = "Running hour does no exist.can not calculate next due date.";
        //        txtNextDueDt.Focus();
        //        return;
        //    }
        //}
        //if (txtServiceReport.Text.Trim() == "")
        //{
        //    lblSaveMsg.Text = "Please enter service report.";
        //    txtServiceReport.Focus();
        //    return;
        //}
        //if (txtServiceReport.Text.Trim().Length > 5000)
        //{
        //    lblSaveMsg.Text = "Service report should not be more than 5000 characters.";
        //    txtServiceReport.Focus();
        //    return;
        //}
        //if (txtCondBefore.Text == "")
        //{
        //    lblSaveMsg.Text = "Please enter before condition.";
        //    txtCondBefore.Focus();
        //    return;
        //}
        //if (txtCondAfter.Text == "")
        //{
        //    lblSaveMsg.Text = "Please enter after condition.";
        //    txtCondAfter.Focus();
        //    return;
        //}

        //Common.Set_Procedures("sp_UpdateServiceReport");
        //Common.Set_ParameterLength(15);
        //Common.Set_Parameters(
        //    new MyParameter("@VesselCode", Request.QueryString["VC"].ToString().Trim()),
        //    new MyParameter("@HistoryId", Request.QueryString["HID"].ToString()),
        //    new MyParameter("@DoneDate", txtDoneDate.Text.Trim()),
        //    new MyParameter("@DoneHour", txtDoneHour.Text.Trim()),
        //    new MyParameter("@DoneBy", ddlRank.SelectedValue),
        //    new MyParameter("@EmployeeNo", txtEmpNo.Text.Trim()),
        //    new MyParameter("@EmployeeName", txtEmpName.Text.Trim()),
        //    new MyParameter("@UpdateRemarks", ddlRemarks.SelectedValue),
        //    new MyParameter("@RemarkSpecify", txtSpecify.Text.Trim()),
        //    new MyParameter("@ServiceReport", txtServiceReport.Text.Trim()),
        //    new MyParameter("@ConditionBefore", txtCondBefore.Text.Trim()),
        //    new MyParameter("@ConditionAfter", txtCondAfter.Text.Trim()),
        //    new MyParameter("@NextDueDate", txtNextDueDt.Text),
        //    new MyParameter("@NextDueHours", txtNextHour.Text),
        //    new MyParameter("@UpdatedBy", Session["loginid"].ToString())
        //    );
        //DataSet dsUpdateJobPlanning = new DataSet();
        //dsUpdateJobPlanning.Clear();
        //Boolean res;
        //res = Common.Execute_Procedures_IUD(dsUpdateJobPlanning);
        //if (res)
        //{
        //    lblSaveMsg.Text = "Service report updated successfully.";
        //}
        //else
        //{
        //    lblSaveMsg.Text = "Unable to update service report.";
        //}
    }
    protected void ddlflpNeeded_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlflpNeeded.SelectedIndex == 2)
        {
            ddlNextVisit.Enabled = true;
            radA.Enabled = true; radB.Enabled = true; radC.Enabled = true; radD.Enabled = true;

            
            
        }
        else
        {
            ddlNextVisit.Enabled = false;
            radA.Enabled = false;
            radB.Enabled = false;
            radC.Enabled = false;
            radD.Enabled = false;
            ddlNextVisit.SelectedIndex = 0;
            radA.Checked = false; radB.Checked = false; radC.Checked = false; radD.Checked = false;
        }
    }
    

    //protected void ddlRemarks_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlRemarks.SelectedValue == "2")
    //    {
    //        trSpecify.Visible = true;
    //    }
    //    else
    //    {
    //        trSpecify.Visible = false;
    //    }
    //}
    //protected void txtDoneDate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime dat;
    //    if (!(DateTime.TryParse(txtDoneDate.Text.Trim(), out dat)))
    //    {
    //        lblSaveMsg.Text = "Please enter valid done date.";
    //        txtDoneDate.Focus();
    //        return;
    //    }
    //    string intervalType = lblInterval.Text.Split('-').GetValue(1).ToString().Trim();
    //    int interval = Common.CastAsInt32(lblInterval.Text.Split('-').GetValue(0).ToString().Trim());
    //    if (ddlRemarks.SelectedValue != "3")
    //    {
    //        if (intervalType == "H")
    //        {
    //            int intType = Common.CastAsInt32(hfIntervalId_H.Value);
    //            double dInterval_H = Convert.ToDouble(hfInterval_H.Value);
    //            int AvgRunningHrPerDay;

    //            string SQL = "SELECT TOP 1 *,UpdatedOn AS Updated1 FROM VSL_VesselRunningHourMaster WHERE VesselCode = '" + Request.QueryString["VC"].ToString() + "' AND ComponentId =(SELECT ComponentId FROM ComponentMaster  WHERE ComponentCode = '" + lblUpdateComponent.Text.Split('-').GetValue(0).ToString().Trim() + "') ORDER BY UpdatedOn Desc";
    //            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
    //            if (dt.Rows.Count > 0)
    //            {
    //                AvgRunningHrPerDay = Common.CastAsInt32(dt.Rows[0]["AvgRunningHrPerDay"].ToString());
    //                if (AvgRunningHrPerDay == 0)
    //                {
    //                    return;
    //                }

    //                if (intType == 0)
    //                {
    //                    DateTime nextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval / AvgRunningHrPerDay);
    //                    txtNextDueDt.Text = nextDate.ToString("dd-MMM-yyyy");
    //                }
    //                else
    //                {
    //                    DateTime dtHNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval / AvgRunningHrPerDay);
    //                    DateTime dtORNextDate = DateTime.Now.Date;
    //                    switch (intType)
    //                    {
    //                        case 2:
    //                            dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(dInterval_H);
    //                            txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                            break;
    //                        case 3:
    //                            dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(dInterval_H * 7);
    //                            txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                            break;
    //                        case 4:
    //                            dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddMonths(Common.CastAsInt32(dInterval_H));
    //                            txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                            break;
    //                        case 5:
    //                            dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddYears(Common.CastAsInt32(dInterval_H));
    //                            txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                            break;
    //                        default:
    //                            break;
    //                    }

    //                    if (dtHNextDate < dtORNextDate)
    //                    {
    //                        txtNextDueDt.Text = dtHNextDate.ToString("dd-MMM-yyyy");
    //                    }
    //                    else
    //                    {
    //                        txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                    }
    //                }
    //            }
    //        }

    //        if (intervalType == "D")
    //        {
    //            DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval);
    //            txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

    //        }

    //        if (intervalType == "W")
    //        {
    //            DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval * 7);
    //            txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

    //        }

    //        if (intervalType == "M")
    //        {
    //            DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddMonths(interval);
    //            txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

    //        }

    //        if (intervalType == "Y")
    //        {
    //            DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddYears(interval);
    //            txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");
    //        }

    //    }
    //    else
    //    {
    //        if (rdoBreakdownReason.SelectedIndex != -1)
    //        {
    //            if (rdoBreakdownReason.SelectedValue == "1")
    //            {
    //                if (intervalType == "H")
    //                {
    //                    int intType = Common.CastAsInt32(hfIntervalId_H.Value);
    //                    double dInterval_H = Convert.ToDouble(hfInterval_H.Value);
    //                    int AvgRunningHrPerDay;

    //                    string SQL = "SELECT TOP 1 *,UpdatedOn AS Updated1 FROM VSL_VesselRunningHourMaster WHERE VesselCode = '" + Request.QueryString["VC"].ToString() + "' AND ComponentId =(SELECT ComponentId FROM ComponentMaster  WHERE ComponentCode = '" + lblUpdateComponent.Text.Split('-').GetValue(0).ToString().Trim() + "') ORDER BY StartDate,Updated1 Desc";
    //                    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        AvgRunningHrPerDay = Common.CastAsInt32(dt.Rows[0]["AvgRunningHrPerDay"].ToString());

    //                        if (AvgRunningHrPerDay == 0)
    //                        {
    //                            return;
    //                        }

    //                        if (intType == 0)
    //                        {
    //                            DateTime nextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval / AvgRunningHrPerDay);
    //                            txtNextDueDt.Text = nextDate.ToString("dd-MMM-yyyy");
    //                        }
    //                        else
    //                        {
    //                            DateTime dtHNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval / AvgRunningHrPerDay);
    //                            DateTime dtORNextDate = DateTime.Now.Date;
    //                            switch (intType)
    //                            {
    //                                case 2:
    //                                    dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(dInterval_H);
    //                                    txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                                    break;
    //                                case 3:
    //                                    dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(dInterval_H * 7);
    //                                    txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                                    break;
    //                                case 4:
    //                                    dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddMonths(Common.CastAsInt32(dInterval_H));
    //                                    txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                                    break;
    //                                case 5:
    //                                    dtORNextDate = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddYears(Common.CastAsInt32(dInterval_H));
    //                                    txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                                    break;
    //                                default:
    //                                    break;
    //                            }

    //                            if (dtHNextDate < dtORNextDate)
    //                            {
    //                                txtNextDueDt.Text = dtHNextDate.ToString("dd-MMM-yyyy");
    //                            }
    //                            else
    //                            {
    //                                txtNextDueDt.Text = dtORNextDate.ToString("dd-MMM-yyyy");
    //                            }
    //                        }
    //                    }
    //                }

    //                if (intervalType == "D")
    //                {
    //                    DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval);
    //                    txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

    //                }

    //                if (intervalType == "W")
    //                {
    //                    DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddDays(interval * 7);
    //                    txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

    //                }

    //                if (intervalType == "M")
    //                {
    //                    DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddMonths(interval);
    //                    txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");

    //                }

    //                if (intervalType == "Y")
    //                {
    //                    DateTime nextHour = Convert.ToDateTime(txtDoneDate.Text.ToString()).AddYears(interval);
    //                    txtNextDueDt.Text = nextHour.ToString("dd-MMM-yyyy");
    //                }
    //            }
    //            else
    //            {
    //                txtNextDueDt.Text = txtDuedt.Text.Trim();
    //            }
    //        }
    //        else
    //        {
    //            lblSaveMsg.Text = "Please select breakdown reason.";
    //            rdoBreakdownReason.Focus();
    //            txtDoneDate.Text = "";
    //        }
    //    }
    //}
    //protected void txtDoneHour_TextChanged(object sender, EventArgs e)
    //{
    //    int i;
    //    if (!int.TryParse(txtDoneHour.Text.Trim(), out i))
    //    {
    //        lblSaveMsg.Text = "Please enter valid hour.";
    //        txtDoneHour.Focus();
    //        return;
    //    }
    //    if (txtDoneHour.Text.Trim() != "")
    //    {
    //        string intervalType = lblInterval.Text.Split('-').GetValue(1).ToString().Trim();
    //        int interval = Common.CastAsInt32(lblInterval.Text.Split('-').GetValue(0).ToString().Trim());
    //        if (intervalType == "H")
    //        {
    //            if (ddlRemarks.SelectedValue != "3")
    //            {
    //                txtNextHour.Text = Convert.ToString(Common.CastAsInt32(txtDoneHour.Text.ToString()) + interval);
    //            }
    //            else
    //            {
    //                if (rdoBreakdownReason.SelectedValue == "1")
    //                {
    //                    txtNextHour.Text = Convert.ToString(Common.CastAsInt32(txtDoneHour.Text.ToString()) + interval);
    //                }
    //                else
    //                {
    //                    txtNextHour.Text = txtLastHour.Text;
    //                }
    //            }
    //        }
    //    }
    //}
}