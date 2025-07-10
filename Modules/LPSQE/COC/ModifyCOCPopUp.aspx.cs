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

public partial class FormReporting_ModifyCOCPopUp : System.Web.UI.Page
{
    int intCOCId = 0;
    string strResponsibility = "";
    string strDoc = "";
    string FileName = "";
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
                BindVessel();
                ShowRecord();
            }
        }
        catch (Exception ex) { throw ex; }
        btn_Save.Enabled = new AuthenticationManager(1080, int.Parse(Session["loginid"].ToString()), ObjectType.Page).IsUpdate;     
    }
    public void BindVessel()
    {
        try
        {
            DataSet dsVessel = Inspection_Master.getMasterDataforInspection("Vessel", "VesselId", "VesselName as Name", Convert.ToInt32(Session["loginid"].ToString()));
            this.ddlvessel.DataSource = dsVessel;
            this.ddlvessel.DataValueField = "VesselId";
            this.ddlvessel.DataTextField = "Name";
            this.ddlvessel.DataBind();
            ddlvessel.Items.Insert(0, "<Select>");
            ddlvessel.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ShowRecord()
    {
        DataTable dt1 = COC.GetFCOCDetailsByCOCId(intCOCId, 0, "", "", "", "", "", "", 0, 0, "BYID", "", 0, "", "", "", "", "", "","", "", "", 0,"","");
        if (dt1.Rows.Count > 0)
        {
            txt_COCIssuedFrom.Text = dt1.Rows[0]["IssuedFrom"].ToString();
            txt_COCNum.Text = dt1.Rows[0]["CO_Number"].ToString();
            ddlvessel.SelectedValue = dt1.Rows[0]["CO_VesselId"].ToString();
            txt_TargetCDate.Text = dt1.Rows[0]["TARGETCLOSEDATE"].ToString();
            txt_CreatedBy.Text = dt1.Rows[0]["CreatedBy"].ToString();
            txt_CreatedOn.Text = dt1.Rows[0]["CreatedOn"].ToString();
            txt_FModifiedBy.Text = dt1.Rows[0]["ModifiedBy"].ToString();
            txt_FModifiedOn.Text = dt1.Rows[0]["ModifiedOn"].ToString();
            if (dt1.Rows[0]["Closed"].ToString() == "True")
            {
                btn_Save.Enabled = false;
                btn_Cancel.Enabled = false;
            }
            else
            {
                btn_Save.Enabled = true;
                btn_Cancel.Enabled = true;
            }
            //ddl_IssuedBy.SelectedValue = dt1.Rows[0]["NC_IssuedBy"].ToString();
            txt_IssueDt.Text = dt1.Rows[0]["ISSUEDATE"].ToString();
            txt_SurveyStDt.Text = dt1.Rows[0]["IMMACTDATE"].ToString(); ;
            txt_SurveyEndDt.Text = dt1.Rows[0]["PRVACTDATE"].ToString(); ;
            txt_Desc.Text = dt1.Rows[0]["CO_Description"].ToString();
            txt_Desc0.Text = dt1.Rows[0]["CO_Description1"].ToString();
            txt_COCIssuedPlace.Text = dt1.Rows[0]["CO_PlaceIssued"].ToString();
            txt_COCSurveyor.Text = dt1.Rows[0]["CO_Suerveyor"].ToString();
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ddlvessel.SelectedIndex = 0;
        txt_TargetCDate.Text = "";
        txt_IssueDt.Text = "";
        txt_SurveyStDt.Text = "";
        txt_SurveyEndDt.Text = "";
        txt_Desc.Text = "";
        btn_Save.Enabled = true;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_COCIssuedFrom.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter Issue from.";
                txt_COCIssuedFrom.Focus();
                return;
            }

            if (ddlvessel.SelectedValue == "0")
            {
                lblmessage.Text = "Please select Vessel.";
                ddlvessel.Focus();
                return;
            }

            if (txt_IssueDt.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter Issue Date.";
                txt_IssueDt.Focus();
                return;
            }

            if (txt_TargetCDate.Text == "")
            {
                lblmessage.Text = "Please enter Target Closure Date.";
                txt_TargetCDate.Focus();
                return;
            }

            //if (txt_TargetCDate.Text != "" && txt_IssueDt.Text != "")
            //{
            //    if (DateTime.Parse(txt_TargetCDate.Text.Trim()) > DateTime.Parse(DateTime.Parse(txt_IssueDt.Text.Trim()).AddDays(90).ToString()))
            //    {
            //        lblmessage.Text = "Target Closure Date cannot be more than 90 days from COC Issue Date.";
            //        txt_TargetCDate.Focus();
            //        return;
            //    }
            //}

            if (txt_Desc.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter \"What's Wrong\"";
                txt_TargetCDate.Focus();
                return;
            }

            if (!(HiddenField_File.Value.ToString().Trim() == ""))
            {
                if ((HiddenField_File.Value.ToString().Trim() == "") && (!flp_COCUpload.HasFile))
                {
                    lblmessage.Text = "Please select a document.";
                    flp_COCUpload.Focus();
                    return;
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
                            strDoc = "UserUploadedDocuments/COC_Tracker/" + flp_COCUpload.FileName.Trim();
                            FileName = um.UploadFileToServer(file1, strfilename, "", "COC");
                            if (FileName.StartsWith("?"))
                            {
                                lblmessage.Text = FileName.Substring(1);
                                return;
                            }
                        }
                        else
                        {
                            lblmessage.Text = "Invalid File Type. (Valid Files Are .doc, .docx, .xls, .xlsx, .pdf)";
                            flp_COCUpload.Focus();
                            return;
                        }
                    }
                    else
                    {
                        FileName = HiddenField_File.Value;
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
                        strDoc = "UserUploadedDocuments/COC_Tracker/" + flp_COCUpload.FileName.Trim();
                        FileName = um.UploadFileToServer(file1, strfilename, "", "COC");
                        if (FileName.StartsWith("?"))
                        {
                            lblmessage.Text = FileName.Substring(1);
                            return;
                        }
                    }
                    else
                    {
                        lblmessage.Text = "Invalid File Type. (Valid Files Are .doc, .docx, .xls, .xlsx, .pdf)";
                        flp_COCUpload.Focus();
                        return;
                    }
                }
            }
            DataTable dt2 = COC.GetFCOCDetailsByCOCId(intCOCId, int.Parse(ddlvessel.SelectedValue), "", txt_TargetCDate.Text.Trim(), strResponsibility, "", "", "", int.Parse(Session["loginid"].ToString()), 0, "MODIFY", FileName, 0, "", txt_IssueDt.Text.Trim(), txt_SurveyStDt.Text.Trim(), txt_SurveyEndDt.Text.Trim(), "", txt_Desc.Text.Trim(), txt_Desc0.Text.Trim(), "", "", 0,txt_COCSurveyor.Text,txt_COCIssuedPlace.Text);
            lblmessage.Text = "Record Saved Sucessfully.";
            btn_Save.Enabled = false;
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
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
}
