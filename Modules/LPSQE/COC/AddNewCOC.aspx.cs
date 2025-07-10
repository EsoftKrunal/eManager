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

public partial class FormReporting_AddNewCOC : System.Web.UI.Page
{
    string strResponsibility = "";
    string strDoc = "", FileName = "";
    int imgtemp = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {
            BindVessel();
        }
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

            if (txt_TarClDate.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter Closure Date.";
                txt_TarClDate.Focus();
                return;
            }
           
            if (txt_TarClDate.Text != "" && txt_IssueDt.Text != "")
            {
                if (DateTime.Parse(txt_TarClDate.Text.Trim()) > DateTime.Parse(DateTime.Parse(txt_IssueDt.Text.Trim()).AddDays(90).ToString()))
                {
                    lblmessage.Text = "Target Closure Date cannot be more than 90 days from COC Issue Date.";
                    txt_TarClDate.Focus();
                    return;
                }
            }

            if (txt_Desc.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter \"What's Wrong\"";
                txt_TarClDate.Focus();
                return;
            }

            if (Session["loginid"] != null)
            {
                if (flp_COCUpload.PostedFile != null && flp_COCUpload.FileContent.Length > 0)
                {
                    string strfilename = flp_COCUpload.FileName;
                    HttpPostedFile file1 = flp_COCUpload.PostedFile;
                    UtilityManager um = new UtilityManager();
                    if (chk_FileExtension(Path.GetExtension(flp_COCUpload.FileName).ToLower()) == true)
                    {
                        strDoc = "/EMANAGERBLOB/LPSQE/COC_Tracker/" + flp_COCUpload.FileName.Trim();
                        FileName = um.UploadFileToServer(file1, strfilename, "", "COC");
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
                    DataTable dt1 = COC.InserCOCTrackerDetails(int.Parse(ddlvessel.SelectedValue), txt_COCIssuedFrom.Text, txt_COCNo.Text.Trim(), "", txt_TarClDate.Text.Trim(), strResponsibility, FileName, int.Parse(Session["loginid"].ToString()), 0, "", "", 0, "", txt_IssueDt.Text.Trim(), txt_SurveyStDt.Text.Trim(), txt_SurveyEndDt.Text.Trim(), "", txt_Desc.Text.Trim(), txt_Desc0.Text.Trim(), txt_COCSurveyor.Text,txt_COCIssuedPlace.Text);
                    lblmessage.Text = "Record Saved Successfully.";
                    btn_Save.Visible = false;
                    btn_Close.Visible = true;
                }
            }
            else
            {
                lblmessage.Text = "Record Not Saved.";
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txt_COCIssuedFrom.Text = "";
        txt_COCNo.Text = "";
        ddlvessel.SelectedIndex = -1;
        txt_TarClDate.Text = "";
        txt_Desc.Text = "";   
        txt_Desc0.Text = "";  
        btn_Save.Visible = true;
        btn_Close.Visible = false;
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
    public bool CheckCrewNum(string str)
    {
        string digits = "1234567890";
        string temp = "";
        string CrewId = str.Substring(0, str.Length);
        string frst = str.Substring(0, 1);
        if (frst == "S" || frst == "s" || frst == "Y" || frst == "y")
        {
            for (int i = 1; i < CrewId.Length; i++)
            {
                temp = CrewId.Substring(i, 1);
                if (digits.IndexOf(temp).ToString() == "-1")
                    return false;
            }
        }
        else
        {
            return false;
        }
        return true;
    }
}
