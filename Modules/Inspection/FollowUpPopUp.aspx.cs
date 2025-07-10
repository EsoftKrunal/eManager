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
using System.Text.RegularExpressions;
using System.IO;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class Transactions_FollowUpPopUp : System.Web.UI.Page
{
    string strflaws = "";
    string strResponsibility = "";
    int imgtemp = 0;

    public int InspectionId
    {
        get { return Common.CastAsInt32(ViewState["InspectionId"]); }
        set { ViewState["InspectionId"] = value; }
    }
    public int FollowUpId
    {
        get { return Common.CastAsInt32(ViewState["intObsId"]); }
        set { ViewState["intObsId"] = value; }
    }
    public int DocID
    {
        set { ViewState["DocID"] = value; }
        get { return Common.CastAsInt32(ViewState["DocID"]); }
    }
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
        if (!IsPostBack)
        {
            InspectionId = Common.CastAsInt32(Request.QueryString["InspId"]);
            FollowUpId = Common.CastAsInt32(Request.QueryString["ObsId"]);
            ShowRecord();
        }
    }
    protected void ShowRecord()
    {
        try
        {
            DataTable Dt = new DataTable();
            Dt = Inspection_FollowUp.InspectionObservation(FollowUpId, "", "", "", "", 0, "", "SELECT", "", 0, 0, 0);
            if (Dt.Rows.Count > 0)
            {

                DataTable dt1 = Budget.getTable("SELECT * FROM t_Observations WHERE Id=" + FollowUpId.ToString()).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    a_file.Visible = (dt1.Rows[0]["F_ClosureFile"].ToString().Trim() != "");
                    a_file.HRef = "..\\EMANAGERBLOB\\Inspection\\FollowUpClosure\\" + dt1.Rows[0]["F_ClosureFile"].ToString().Trim();
                }
                else
                {
                    a_file.Visible = false;
                }


                txtremark.Text = Dt.Rows[0]["Remarks"].ToString();
                txt_Observation.Text = Dt.Rows[0]["Deficiency"].ToString();
                if (Dt.Rows[0]["TargetCloseDt"].ToString() != "")
                    txttargetclosedt.Text = Dt.Rows[0]["TargetCloseDt"].ToString();
                else
                    txttargetclosedt.Text = "";
                txtcorrective.Text = Dt.Rows[0]["CorrectiveActions"].ToString();
                if (Dt.Rows[0]["TargetCloseDt"].ToString() != "")
                    txtcloseddt.Text = Dt.Rows[0]["ClosedDate"].ToString();
                else
                    txtcloseddt.Text = "";
                if (Dt.Rows[0]["Flaws"].ToString() != "")
                {
                    char[] c = { ',' };
                    Array a = Dt.Rows[0]["Flaws"].ToString().Split(c);
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
                if (Dt.Rows[0]["Closed"].ToString() != "")
                {
                    if (Dt.Rows[0]["Closed"].ToString() == "False")
                    {
                        rdbclosed.SelectedValue = "0";
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        rdbclosed.SelectedValue = "1";
                        btnSave.Enabled = false;
                    }
                }
                else
                {
                    rdbclosed.SelectedIndex = -1;
                    btnSave.Enabled = true;
                }
                if (Dt.Rows[0]["Responsibilty"].ToString() != "")
                {
                    char[] resp = { ',' };
                    Array rs = Dt.Rows[0]["Responsibilty"].ToString().Split(resp);
                    for (int l = 0; l < chklst_Responsibility.Items.Count; l++)
                    {
                        chklst_Responsibility.Items[l].Selected = false;
                    }
                    for (int m = 0; m < rs.Length; m++)
                    {
                        if (rs.GetValue(m).ToString() == "Vessel")
                            chklst_Responsibility.Items[0].Selected = true;
                        if (rs.GetValue(m).ToString() == "Office")
                            chklst_Responsibility.Items[1].Selected = true;
                    }
                }
                else
                    chklst_Responsibility.SelectedIndex = -1;

            }
            DataTable Dt11 = Inspection_FollowUp.InspectionObservation(FollowUpId, "", "", "", "", 0, "", "SELECT", "", 0, 0, 0);
            if (Dt11.Rows.Count > 0)
            {
                txtCreatedBy_DocumentType.Text = Dt11.Rows[0]["F_CreatedBy"].ToString();
                txtCreatedOn_DocumentType.Text = Dt11.Rows[0]["F_CreatedOn"].ToString();
                txtModifiedBy_DocumentType.Text = Dt11.Rows[0]["F_Modified_By"].ToString();
                txtModifiedOn_DocumentType.Text = Dt11.Rows[0]["F_Modified_On"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["Insp_Id"] == null) { lblmessage.Text = "Please save Planning first."; return; }
        try
        {
            if (ViewState["intObsId"].ToString() != "")
            {
                if (rdbflaws.SelectedValue == "")
                {
                    lblmessage.Text = "Please select Cause.";
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
                if (txtcorrective.Text.Trim() == "")
                {
                    lblmessage.Text = "Please enter Corrective Actions.";
                    return;
                }
                //if ((txttargetclosedt.Text != "") && (txtdonedt.Text != ""))
                //{
                //    if (DateTime.Parse(txttargetclosedt.Text) < DateTime.Parse(txtdonedt.Text))
                //    {
                //        lblmessage.Text = "Target Closed Date cannot be less than Inspection Done Date.";
                //        return;
                //    }
                //}
                if (rdbclosed.SelectedValue == "")
                {
                    lblmessage.Text = "Please select Closed.";
                    return;
                }
                if (rdbclosed.SelectedValue == "1")
                {
                    if (txtcloseddt.Text == "")
                    {
                        lblmessage.Text = "Please enter Closed Date.";
                        return;
                    }
                }
                //if ((txtcloseddt.Text != "") && (txtdonedt.Text != ""))
                //{
                //    if (DateTime.Parse(txtcloseddt.Text) < DateTime.Parse(txtdonedt.Text))
                //    {
                //        lblmessage.Text = "Closed Date cannot be less than Inspection Done Date.";
                //        return;
                //    }
                //}
                if (rdbclosed.SelectedValue == "1")
                {
                    if (txtremark.Text.Trim() == "")
                    {
                        lblmessage.Text = "Please enter Remarks.";
                        return;
                    }
                }

                for (int k = 0; k < chklst_Responsibility.Items.Count; k++)
                {
                    if (chklst_Responsibility.Items[k].Selected == true)
                    {
                        if (strResponsibility == "")
                        {
                            strResponsibility = chklst_Responsibility.Items[k].Value;
                        }
                        else
                        {
                            strResponsibility = strResponsibility + "," + chklst_Responsibility.Items[k].Value;
                        }
                    }
                }

                //----------------------------------------------------
                string FileName = "";

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
                        lblmessage.Text = "Invalid File Type. (Valid Files Are .doc, .docx, .xls, .xlsx, .pdf)";
                        flp_COCUpload.Focus();
                        imgtemp = 1;
                        return;
                    }
                }
                if (imgtemp != 1)
                {
                    DataTable dt = Inspection_FollowUp.InspectionObservation(int.Parse(ViewState["intObsId"].ToString()), txtcorrective.Text, strflaws, txtremark.Text, txttargetclosedt.Text, int.Parse(rdbclosed.SelectedValue), txtcloseddt.Text, "MODIFY", strResponsibility, int.Parse(Session["loginid"].ToString()), int.Parse(Session["loginid"].ToString()), 0);
                    Budget.getTable("UPDATE t_Observations SET F_ClosureFile='" + FileName + "' WHERE Id=" + ViewState["intObsId"].ToString());
                    lblmessage.Text = "FollowUp Updated Sucessfully.";
                    btnSave.Enabled = false;
                    btnNotify.Enabled = true;
                }
            }
            else
            {
                lblmessage.Text = "Select an Observation.";
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        string strInspNum = "", strQuestNum = "", strDeficiency = "", strCorrectiveActions = "", strResponsibility = "", strTargetCloseDt = "";
        try
        {
            DataTable dt1 = Inspection_FollowUp.GetFollowUpMailDetails(int.Parse(Session["Insp_Id"].ToString()), int.Parse(ViewState["intObsId"].ToString()));
            if (dt1.Rows.Count > 0)
            {
                strInspNum = dt1.Rows[0]["InspectionNo"].ToString();
                strQuestNum = dt1.Rows[0]["QuestionNo"].ToString();
                strDeficiency = dt1.Rows[0]["Deficiency"].ToString();
                strCorrectiveActions = dt1.Rows[0]["CorrectiveActions"].ToString();
                strResponsibility = dt1.Rows[0]["Responsibilty"].ToString();
                strTargetCloseDt = dt1.Rows[0]["TargetCloseDate"].ToString();
            }
        }
        catch { }
        try
        {
            SendMail.FollowUpMail("FollowUp", strInspNum, strQuestNum, strDeficiency, strCorrectiveActions, strResponsibility, strTargetCloseDt);
            lblmessage.Text = "Mail Send Successfully.";
        }
        catch { }
    }

    protected void btnColsureEvidence_OnClick(object sender, EventArgs e)
    {
        DivClosureDocs.Visible = true;
        BindDocs();
    }
    protected void btnCloseDocuments_OnClick(object sender, EventArgs e)
    {
        DivClosureDocs.Visible = false;
    }
    protected void btnSaveDoc_Click(object sender, EventArgs e)
    {

        int ObsId = Common.CastAsInt32(ViewState["intObsId"]);
        //if (CaseID == 0)
        //{
        //    lblMessage.Text = "Please save the case first.";
        //    return;
        //}
        if (DocID == 0)
            if (!flAttachDocs.HasFile)
            {
                lblMsgDoc.Text = "Please select a file.";
                flAttachDocs.Focus();
                return;
            }
        FileUpload img = (FileUpload)flAttachDocs;
        Byte[] imgByte = new Byte[0];
        string FileName = "";

        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;

            if (Path.GetExtension(File.FileName).ToString() == ".pdf" || Path.GetExtension(File.FileName).ToString() == ".txt" || Path.GetExtension(File.FileName).ToString() == ".doc" || Path.GetExtension(File.FileName).ToString() == ".docx" || Path.GetExtension(File.FileName).ToString() == ".xls" || Path.GetExtension(File.FileName).ToString() == ".xlsx" || Path.GetExtension(File.FileName).ToString() == ".ppt" || Path.GetExtension(File.FileName).ToString() == ".pptx")
            {
                FileName = "Observation" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);


                string path = "../EMANAGERBLOB/Inspection/Observation/";
                flAttachDocs.SaveAs(Server.MapPath(path) + FileName);
            }
            else
            {
                lblMsgDoc.Text = "File type not supported. Only pdf and microsoft ofiice files accepted.";
            }


        }
        string strSQL = "EXEC sp_InsertUpdatet_FollowUP " + DocID + "," + ObsId + ",'" + txt_Desc.Text.Trim().Replace("'", "`") + "','" + FileName + "','" + Convert.ToInt32(Session["loginid"].ToString()) + "'";
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            lblMsgDoc.Text = "Record Successfully Saved.";
            txt_Desc.Text = "";
            BindDocs();
            DocID = 0;
        }
    }
    protected void imgEditDoc_OnClick(object sender, EventArgs e)
    {
        ImageButton ImgEdit = (ImageButton)sender;
        HiddenField hfDocID = (HiddenField)ImgEdit.Parent.FindControl("hfDocID");
        DocID = Common.CastAsInt32(hfDocID.Value);
        Label lblDesc = (Label)ImgEdit.Parent.FindControl("lblDesc");
        txt_Desc.Text = lblDesc.Text;

    }
    protected void imgDelDoc_OnClick(object sender, EventArgs e)
    {
        ImageButton ImgDel = (ImageButton)sender;
        HiddenField hfDocID = (HiddenField)ImgDel.Parent.FindControl("hfDocID");
        DocID = Common.CastAsInt32(hfDocID.Value);

        string sql = "delete from t_FollowUP  where DocID=" + hfDocID.Value + "";
        DataSet dtGroups = Budget.getTable(sql);
        BindDocs();
        DocID = 0;
    }
    public void BindDocs()
    {
        int intObsId = Common.CastAsInt32(ViewState["intObsId"]);
        string strSQL = "select replace(convert(varchar,UploadedOn ,106) ,' ','-')UploadedDate,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= CS.UploadedBy)UploadedBy,* from t_FollowUP CS where DeficiencyID=" + intObsId;
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            rptDocs.DataSource = dtDocs;
            rptDocs.DataBind();
        }
        else
        {
            rptDocs.DataSource = null;
            rptDocs.DataBind();
        }
    }
}
