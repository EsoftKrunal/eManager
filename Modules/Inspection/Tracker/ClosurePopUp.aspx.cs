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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
public partial class FormReporting_ClosurePopUp : System.Web.UI.Page
{
    int intInspDueId = 0, intObsvId = 0;
    string strFlagName = "", strflaws = "";
    int imgtemp = 0;
    public bool chk_FileExtension(string str)
    {
        string extension = str;
        switch (extension)
        {
            case ".doc":
                return true;
            case ".docx":
                return true;
            case ".xls":
                return true;
            case ".xlsx":
                return true;
            case ".ppt":
                return true;
            case ".pptx":
                return true;
            case ".jpg":
                return true;
            case ".jpeg":
                return true;
            case ".png":
                return true;
            case ".gif":
                return true;
            case ".pdf":
                return true;
            default:
                return false;
                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        try
        {
            if (Page.Request.QueryString["INSPDId"].ToString() != "")
                intInspDueId = int.Parse(Page.Request.QueryString["INSPDId"].ToString());
            if (Page.Request.QueryString["OBSVTNID"].ToString() != "")
                intObsvId = int.Parse(Page.Request.QueryString["OBSVTNID"].ToString());
            if (Page.Request.QueryString["TBLFLAG"].ToString() != "")
                strFlagName = Page.Request.QueryString["TBLFLAG"].ToString();
            if (!Page.IsPostBack)
            {
                ShowRecord();
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void ShowRecord()
    {
        DataTable dt1;
        if (strFlagName == "OLD")
        {
            dt1 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, intObsvId, "", "", "SELECT", 0, 0, "OLD", "", "", "", 0, "", "");
            if (dt1.Rows.Count > 0)
            {
                a_file.Visible = (dt1.Rows[0]["F_ClosureFile"].ToString().Trim() != "");
                a_file.HRef = "..\\UserUploadedDocuments\\FollowUpClosure\\" + dt1.Rows[0]["F_ClosureFile"].ToString().Trim();
            }
  
        }
        else if (strFlagName == "OBN")
        {
            dt1 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, intObsvId, "", "", "SELECT", 0, 0, "OBN", "", "", "", 0, "", "");
            if (dt1.Rows.Count > 0)
            {
                a_file.Visible = (dt1.Rows[0]["F_ClosureFile"].ToString().Trim() != "");
                a_file.HRef = "..\\UserUploadedDocuments\\FollowUpClosure\\DeficiencyClosure_" + intObsvId + dt1.Rows[0]["F_ClosureFile"].ToString().Trim();
            }

        }
        else
        {
            dt1 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, intObsvId, "", "", "SELECT", 0, 0, "NEW", "", "", "", 0, "", "");
            if (dt1.Rows.Count > 0)
            {
                a_file.Visible = (dt1.Rows[0]["FP_ClosureFile"].ToString().Trim() != "");
                a_file.HRef = "..\\UserUploadedDocuments\\FollowUpClosure\\" + dt1.Rows[0]["FP_ClosureFile"].ToString().Trim();
            }
        }
        if (dt1.Rows.Count > 0)
        {
            txt_ClosedDate.Text = dt1.Rows[0]["ClosedDate"].ToString();
            txt_ClosedRemarks.Text = dt1.Rows[0]["Remarks"].ToString();
            txt_ClosedBy.Text = dt1.Rows[0]["Closed_By"].ToString();
            txt_ClosedOn.Text = dt1.Rows[0]["Closed_On"].ToString();
            HiddenField_InspDoneDt.Value = dt1.Rows[0]["InspDoneDate"].ToString();
            if (dt1.Rows[0]["Flaws"].ToString() != "")
            {
                char[] c = { ',' };
                Array a = dt1.Rows[0]["Flaws"].ToString().Split(c);
                for (int j = 0; j <= rdbflaws.Items.Count - 1; j++)
                {
                    rdbflaws.Items[j].Selected = false;
                }
                for (int r = 0; r < a.Length; r++)
                {
                    if (a.GetValue(r).ToString() == "People")
                        rdbflaws.Items[0].Selected = true;
                    if (a.GetValue(r).ToString() == "Process")
                        rdbflaws.Items[1].Selected = true;
                    if (a.GetValue(r).ToString() == "Technology" || a.GetValue(r).ToString() == "Equipment")
                        rdbflaws.Items[2].Selected = true;
                }
            }
            else
                rdbflaws.SelectedIndex = -1;

            HiddenField_Def.Value = dt1.Rows[0]["Deficiency"].ToString();
            HiddenField_Corr.Value = dt1.Rows[0]["CorrectiveActions"].ToString();
            HiddenField_TrClDt.Value = dt1.Rows[0]["TargetCloseDt"].ToString();
            HiddenField_ModBy.Value = dt1.Rows[0]["ModifiedBy"].ToString();
            HiddenField6.Value = dt1.Rows[0]["Modified_On"].ToString();
            HiddenField_Resp.Value = dt1.Rows[0]["Responsibilty"].ToString();
            if (dt1.Rows[0]["Closed"].ToString() == "True")
            {
                btn_Save.Visible = false;
                btn_Cancel.Visible = false;
            }
            else
            {
                txt_ClosedBy.Text = "";
                txt_ClosedOn.Text = "";
                btn_Save.Visible = true;
                btn_Cancel.Visible = true;
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_ClosedDate.Text == "")
            {
                lblmessage.Text = "Please enter Closed Date.";
                txt_ClosedDate.Focus();
                return;
            }
            if (txt_ClosedRemarks.Text == "")
            {
                lblmessage.Text = "Please enter Closed Remarks.";
                txt_ClosedRemarks.Focus();
                return;
            }
            if (txt_ClosedDate.Text.Trim() != "" && HiddenField_InspDoneDt.Value.Trim() !="")
            {
                   if (DateTime.Parse(txt_ClosedDate.Text) < DateTime.Parse(HiddenField_InspDoneDt.Value))
                    {
                        lblmessage.Text = "Closed Date cannot be less than Inspection Done Date.";
                        return;
                    }
            }
            if (!(flp_COCUpload.HasFile))
            {
                lblmessage.Text = "Please upload closure evidance file.";
                flp_COCUpload.Focus();
                return;
            }

            for (int cnt = 0; cnt <= rdbflaws.Items.Count - 1; cnt++)
            {
                if (rdbflaws.Items[cnt].Selected == true)
                {
                    if (strflaws == "")
                    {
                        strflaws = rdbflaws.Items[cnt].Value;
                    }
                    else
                    {
                        strflaws = strflaws + "," + rdbflaws.Items[cnt].Value;
                    }
                }
            }
            int ModifiedBy = 0;
            if (HiddenField_ModBy.Value.Trim() == "")
            {
                ModifiedBy = int.Parse(Session["loginid"].ToString());
            }
            else
            {
                ModifiedBy = int.Parse(HiddenField_ModBy.Value);
            }

            //----------------------------------------------------
            string FileName = "";
            string FileType = "";
            if (strFlagName == "OBN")
            {
                if (flp_COCUpload.PostedFile != null && flp_COCUpload.FileContent.Length > 0)
                {
                    HttpPostedFile file1 = flp_COCUpload.PostedFile;
                    UtilityManager um = new UtilityManager();
                    if (chk_FileExtension(Path.GetExtension(flp_COCUpload.FileName).ToLower()) == true)
                    {
                        FileName = "DeficiencyClosure_" + intObsvId.ToString() + Path.GetExtension(flp_COCUpload.FileName).ToLower();
                        FileType = Path.GetExtension(flp_COCUpload.FileName).ToLower();

                        if (File.Exists(Server.MapPath("~/UserUploadedDocuments/FollowUpClosure/" + FileName)))
                            File.Delete(Server.MapPath("~/UserUploadedDocuments/FollowUpClosure/" + FileName));
                        flp_COCUpload.SaveAs(Server.MapPath("~/UserUploadedDocuments/FollowUpClosure/" + FileName));
                    }
                    else
                    {
                        lblmessage.Text = "Invalid File Type. (Valid Files Are .doc, .docx, .xls, .xlsx, .ppt, .pptx, .pdf, .jpg, .jpeg, .png, .gif )";
                        flp_COCUpload.Focus();
                        return;
                    }
                }
            }
            else
            {
                if (flp_COCUpload.PostedFile != null && flp_COCUpload.FileContent.Length > 0)
                {
                    string strfilename = flp_COCUpload.FileName;
                    HttpPostedFile file1 = flp_COCUpload.PostedFile;
                    UtilityManager um = new UtilityManager();
                    if (chk_FileExtension(Path.GetExtension(flp_COCUpload.FileName).ToLower()) == true)
                    {
                        string strDoc = "EMANAGERBLOB/Inspection/FollowUpClosure/" + flp_COCUpload.FileName.Trim();
                        FileName = um.UploadFileToServer(file1, strfilename, "", "FollowUpClosure");
                        if (FileName.StartsWith("?"))
                        {
                            lblmessage.Text = FileName.Substring(1);
                            imgtemp = 1;
                            return;
                        }
                    }
                    else
                    {
                        lblmessage.Text = "Invalid File Type. (Valid Files Are .doc, .docx, .xls, .xlsx, .ppt, .pptx, .pdf, .jpg, .jpeg, .png, .gif )";
                        flp_COCUpload.Focus();
                        imgtemp = 1;
                        return;
                    }
                }
            }
            if (imgtemp != 1)
            {
                if (strFlagName == "OLD")
                {
                    DataTable dt2 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, intObsvId, txt_ClosedDate.Text.Trim(), txt_ClosedRemarks.Text.Trim(), "MODIFY", 1, int.Parse(Session["loginid"].ToString()), "OLD", "", "", "", 0, "", strflaws);
                    Budget.getTable("UPDATE t_Observations SET F_ClosureFile='" + FileName + "' WHERE Id=" + intObsvId.ToString() + " AND InspectionDueId=" + intInspDueId.ToString());
                    //string AlertKey = "2|" + intObsvId.ToString();
                    //ProjectCommon.ClearAlert(AlertKey);
                }
                if (strFlagName == "OBN")
                {
                    //DataTable dt2 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, intObsvId, txt_ClosedDate.Text.Trim(), txt_ClosedRemarks.Text.Trim(), "MODIFY", 1, int.Parse(Session["loginid"].ToString()), "OLD", "", "", "", 0, "", strflaws);
                    //Budget.getTable("UPDATE t_Observationsnew SET Closure=1,F_ClosureFile='" + FileName + "' WHERE Id=" + intObsvId.ToString() + " AND InspectionDueId=" + intInspDueId.ToString());
                    Budget.getTable("UPDATE dbo.t_Observationsnew set Closure=1,ClosedBy='" + Session["UserName"].ToString() + "',ClosedOn='" + txt_ClosedDate.Text.Trim() + "',ClosureRemarks='" + txt_ClosedRemarks.Text.Replace("'", "''").Trim() + "',flaws='" + strflaws + "',FileType='" + FileType + "' Where TableID=" + intObsvId.ToString() + "");
                    //string AlertKey = "1|" + intObsvId.ToString();
                    //ProjectCommon.ClearAlert(AlertKey);
                }
                else
                {
                    DataTable dt2 = FollowUp_Tracker.GetFollowUpDetailsByInspObsvId(intInspDueId, intObsvId, txt_ClosedDate.Text.Trim(), txt_ClosedRemarks.Text.Trim(), "MODIFY", 1, int.Parse(Session["loginid"].ToString()), "NEW", HiddenField_Corr.Value, HiddenField_TrClDt.Value, HiddenField_Resp.Value, ModifiedBy, HiddenField_Def.Value, strflaws);
                    Budget.getTable("UPDATE FR_FollowUpList SET FP_ClosureFile='" + FileName + "' WHERE FP_Id=" + intInspDueId.ToString());
                    //string AlertKey = "3|" + intObsvId.ToString();
                    //ProjectCommon.ClearAlert(AlertKey);
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "abc", "alert('Record Saved Successfully');window.opener.document.getElementById('btn_Show').click();window.close();", true);
                btn_Save.Enabled = false;
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txt_ClosedDate.Text = "";
        txt_ClosedRemarks.Text = "";
        txt_ClosedBy.Text = "";
        txt_ClosedOn.Text = "";
        btn_Save.Enabled = true;
    }
}
