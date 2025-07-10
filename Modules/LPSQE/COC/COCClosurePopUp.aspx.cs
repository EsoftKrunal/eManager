using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO; 
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class FormReporting_COCClosurePopUp : System.Web.UI.Page
{
    int intCOCId = 0;
    string strflaws = "";
    int imgtemp = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        try
        {
            if (Page.Request.QueryString["COC_Id"].ToString() != "")
                intCOCId = int.Parse(Page.Request.QueryString["COC_Id"].ToString());
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
        dt1 = COC.GetFCOCDetailsByCOCId(intCOCId, 0, "", "", "", "", "", "", 0, 0, "BYID", "", 0, "", "", "", "", "", "", "", "","", 0,"","");
        if (dt1.Rows.Count > 0)
        {
            txt_ClosedDate.Text = dt1.Rows[0]["COMPLETIONDATE"].ToString();
            txt_ClosedRemarks.Text = dt1.Rows[0]["CO_ClosedRemarks"].ToString();
            txt_ClosedBy.Text = dt1.Rows[0]["ClosedBy"].ToString();
            txt_ClosedOn.Text = dt1.Rows[0]["ClosedOn"].ToString();
            if (dt1.Rows[0]["CO_Flaws"].ToString() != "")
            {
                char[] c = { ',' };
                Array a = dt1.Rows[0]["CO_Flaws"].ToString().Split(c);
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
                    if (a.GetValue(r).ToString() == "Technology")
                        rdbflaws.Items[2].Selected = true;
                }
            }
            else
                rdbflaws.SelectedIndex = -1;

            HiddenField_VslId.Value = dt1.Rows[0]["CO_VesselId"].ToString();
            HiddenField_Def.Value = dt1.Rows[0]["CO_DefCode"].ToString();
            HiddenField_TrClDt.Value = dt1.Rows[0]["TARGETCLOSEDATE"].ToString();
            HiddenField_ModBy.Value = dt1.Rows[0]["CO_ModifiedBy"].ToString();
            HiddenField_Resp.Value = dt1.Rows[0]["CO_Responsibility"].ToString();
            if (dt1.Rows[0]["Closed"].ToString() == "True")
            {
                btn_Save.Enabled = false;
                btn_Cancel.Enabled = false;
            }
            else
            {
                txt_ClosedBy.Text = "";
                txt_ClosedOn.Text = "";
                btn_Save.Enabled = true;
                btn_Cancel.Enabled = true;
            }
            HiddenField_IssuedBy.Value = "0";
            HiddenField_EmpNo.Value = dt1.Rows[0]["CO_EmpNo"].ToString();
            HiddenField_IssueDate.Value = dt1.Rows[0]["ISSUEDATE"].ToString();
            HiddenField_ImAcDt.Value = dt1.Rows[0]["IMMACTDATE"].ToString(); ;
            HiddenField_PrAcDt.Value = dt1.Rows[0]["PRVACTDATE"].ToString(); ;
            HiddenField_FUDt.Value = dt1.Rows[0]["FOLLOWUPDATE"].ToString();
            HiddenField_Desc.Value = dt1.Rows[0]["CO_Description"].ToString();
            HiddenField_Desc1.Value = dt1.Rows[0]["CO_Description1"].ToString();
            HiddenField_FileUpl.Value = dt1.Rows[0]["CO_Filepload"].ToString();

            HiddenField_IBFirstName.Value = dt1.Rows[0]["CO_IBFirstName"].ToString();
            HiddenField_IBLastName.Value = dt1.Rows[0]["CO_IBLastName"].ToString(); ;
            HiddenField_IBRank.Value = dt1.Rows[0]["CO_IBRank"].ToString();

            a_file.Visible = (dt1.Rows[0]["CO_ClosureFile"].ToString().Trim() != "");
            a_file.HRef = "\\EMANAGERBLOB\\LPSQE\\COC_Closure\\" + dt1.Rows[0]["CO_ClosureFile"].ToString().Trim(); 
        }
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

            string FileName = "";
            if (flp_COCUpload.PostedFile != null && flp_COCUpload.FileContent.Length > 0)
            {
                string strfilename = flp_COCUpload.FileName;
                HttpPostedFile file1 = flp_COCUpload.PostedFile;
                UtilityManager um = new UtilityManager();
                if (chk_FileExtension(Path.GetExtension(flp_COCUpload.FileName).ToLower()) == true)
                {
                    string strDoc = "EMANAGERBLOB/LPSQE/COC_Closure/" + flp_COCUpload.FileName.Trim();
                    FileName = um.UploadFileToServer(file1, strfilename, "", "COCClosure");
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
                DataTable dt2 = COC.GetFCOCDetailsByCOCId(intCOCId, int.Parse(HiddenField_VslId.Value), HiddenField_Def.Value, HiddenField_TrClDt.Value, HiddenField_Resp.Value, txt_ClosedDate.Text.Trim(), strflaws, txt_ClosedRemarks.Text.Trim(), Common.CastAsInt32(HiddenField_ModBy.Value), int.Parse(Session["loginid"].ToString()), "CLOSE", FileName , int.Parse(HiddenField_IssuedBy.Value), HiddenField_EmpNo.Value, HiddenField_IssueDate.Value, HiddenField_ImAcDt.Value, HiddenField_PrAcDt.Value, HiddenField_FUDt.Value, HiddenField_Desc.Value, HiddenField_Desc1.Value, HiddenField_IBFirstName.Value, HiddenField_IBLastName.Value, int.Parse(HiddenField_IBRank.Value), "", "");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "abc", "alert('Record Saved Successfully');window.opener.document.getElementById('btn_Show').click();window.close();", true);
                btn_Save.Enabled = false;
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        rdbflaws.SelectedIndex = -1;
        txt_ClosedDate.Text = "";
        txt_ClosedRemarks.Text = "";
        btn_Save.Enabled = true;
    }
}
