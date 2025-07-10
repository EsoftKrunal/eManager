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

public partial class AddSpares : System.Web.UI.Page
{
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public string CompCode 
    {
        set { ViewState["CC"] = value; }
        get { return ViewState["CC"].ToString(); }
    }
    public string SpareId
    {
        set { ViewState["SPI"] = value; }
        get { return ViewState["SPI"].ToString(); }
    }
    public int CompId
    {
        set { ViewState["CI"] = value; }
        get { return Common.CastAsInt32(ViewState["CI"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        lblMessage.Text = "";                
        #region --------- USER RIGHTS MANAGEMENT -----------
        //try
        //{
        //    AuthenticationManager auth = new AuthenticationManager(206, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        //    if (!(auth.IsView))
        //    {
        //        Response.Redirect("~/NoPermission.aspx", false);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        //}
        #endregion ----------------------------------------

        if (!Page.IsPostBack)
        {
            SpareId = "";
            if (Request.QueryString["CompCode"] != null && Request.QueryString["VC"] != null)
            {                
                VesselCode = Request.QueryString["VC"].ToString();
                CompCode = Request.QueryString["CompCode"].ToString();
                GetComponent();
                GetMakerDetails();
            }
            if (Request.QueryString["SPID"] != null && Request.QueryString["SPID"].ToString().Trim() != "")
            {
                SpareId = Request.QueryString["SPID"].ToString();
                ShowDetails();
                //flAttachDocs.Enabled = false;
                //ancFile.Visible = true;
            }
        }

    }

    #region ---------------- USER DEFINED FUNCTIONS ---------------------------
    private void ShowDetails()
    {        
        DataTable dtSpareDetails;
        string strSQL = "SELECT VSM.*,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster WHERE LEN(ComponentCode)= (LEN('" + CompCode.Trim() + "')-2) AND  LEFT(ComponentCode,(LEN('" + CompCode.Trim() + "')-2)) = LEFT('" + CompCode.Trim() + "',(LEN('" + CompCode.Trim() + "')-2)))As LinkedTo FROM VesselSpareMaster VSM " +
                        "INNER JOIN ComponentMaster CM ON VSM.ComponentId = CM.ComponentId " +                        
                        "WHERE CM.ComponentCode = '" + CompCode.Trim() + "' AND VSM.VesselCode = '" + VesselCode.Trim() + "' AND SpareId = '" + SpareId + "' ";
        dtSpareDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtSpareDetails.Rows.Count > 0)
        {
            
            //strSpareId = dtSpareDetails.Rows[0]["SpareId"].ToString();
            txtName.Text = dtSpareDetails.Rows[0]["SpareName"].ToString();
            txtMaker.Text = dtSpareDetails.Rows[0]["Maker"].ToString();
            txtMakerType.Text = dtSpareDetails.Rows[0]["MakerType"].ToString();
            txtPart.Text = dtSpareDetails.Rows[0]["PartNo"].ToString();
            //txtPartName.Text = dtSpareDetails.Rows[0]["PartName"].ToString();
            txtAltPart.Text = dtSpareDetails.Rows[0]["AltPartNo"].ToString();
            //txtLocation.Text = dtSpareDetails.Rows[0]["Location"].ToString();
            txtMinQty.Text = dtSpareDetails.Rows[0]["MinQty"].ToString();
            txtMaxQty.Text = dtSpareDetails.Rows[0]["MaxQty"].ToString();
            txtStatutory.Text = dtSpareDetails.Rows[0]["StatutoryQty"].ToString();
            txtWeight.Text = dtSpareDetails.Rows[0]["Weight"].ToString();

          


            //txtStock.Text = dtSpareDetails.Rows[0]["Stock"].ToString();
            txtDrawingNo.Text = dtSpareDetails.Rows[0]["DrawingNo"].ToString();
            txtSpecs.Text = dtSpareDetails.Rows[0]["Specification"].ToString();
            hfOffice_Ship.Value = dtSpareDetails.Rows[0]["Office_Ship"].ToString();
            if (dtSpareDetails.Rows[0]["Attachment"].ToString() != "")
            {
                Image1.ImageUrl = "~/UploadFiles/UploadSpareDocs/" + dtSpareDetails.Rows[0]["Attachment"].ToString();
                ancFile.HRef = "~/UploadFiles/UploadSpareDocs/" + dtSpareDetails.Rows[0]["Attachment"].ToString();

                ancFile.Visible = true;
                Image1.Visible = true;
            }
        }
        else
        {
            ClearFields();
        }
    }
    private void GetComponent()
    {
        DataTable dtCompId = new DataTable();
        string strSQL = "SELECT ComponentId,ComponentCode,ComponentName FROM ComponentMaster WHERE ComponentCode = '" + Request.QueryString["CompCode"].ToString().Trim() + "' ";
        dtCompId = Common.Execute_Procedures_Select_ByQuery(strSQL);        
        lblComponent.Text = dtCompId.Rows[0]["ComponentCode"].ToString() + " : " + dtCompId.Rows[0]["ComponentName"].ToString();
        CompId = Common.CastAsInt32(dtCompId.Rows[0]["ComponentId"].ToString());
    }
    private Boolean IsValidated()
    {
        if (CompCode == "" || CompCode.Trim().Length == 2)
        {
            lblMessage.Text = "Please select a component.";
            return false;
        }
        if (txtName.Text == "")
        {
            lblMessage.Text = "Please enter Spare Name.";
            txtName.Focus();
            return false;
        }
        if (txtMaker.Text == "")
        {
            lblMessage.Text = "Please enter Maker.";
            txtMaker.Focus();
            return false;
        }
        //if (txtMakerType.Text == "")
        //{
        //    lblMessage.Text = "Please enter Maker Type.";
        //    txtMakerType.Focus();
        //    return false;
        //}
        //if (txtPart.Text == "")
        //{
        //    lblMessage.Text = "Please enter Part#.";
        //    txtPart.Focus();
        //    return false;
        //}
        //if (txtAltPart.Text == "")
        //{
        //    lblMessage.Text = "Please enter Alt Part#.";
        //    txtAltPart.Focus();
        //    return false;
        //}
        //if (txtLocation.Text == "")
        //{
        //    lblMessage.Text = "Please enter Location.";
        //    txtLocation.Focus();
        //    return false;
        //}
        //if (txtMinQty.Text == "")
        //{
        //    lblMessage.Text = "Please enter Min Qty.";
        //    txtMinQty.Focus();
        //    return false;
        //}
        //if (txtMaxQty.Text == "")
        //{
        //    lblMessage.Text = "Please enter Max Qty.";
        //    txtMaxQty.Focus();
        //    return false;
        //}
        //if (txtStatutory.Text == "")
        //{
        //    lblMessage.Text = "Please enter Statutory Qty.";
        //    txtStatutory.Focus();
        //    return false;
        //}
        //if (txtWeight.Text == "")
        //{
        //    lblMessage.Text = "Please enter Weight.";
        //    txtWeight.Focus();
        //    return false;
        //}
        //if (txtStock.Text == "")
        //{
        //    lblMessage.Text = "Please enter Stock.";
        //    txtStock.Focus();
        //    return false;
        //}
        //if (txtDrawingNo.Text == "Please enter Drawing No.")
        //{
        //    lblMessage.Text = "";
        //    txtDrawingNo.Focus();
        //    return false;
        //}
        if (txtSpecs.Text.Trim().Length > 500)
        {
            lblMessage.Text = "Specification can not be greater than 500 characters.";
            txtSpecs.Focus();
            return false;
        }
        return true;
    }
    private void ClearFields()
    {
        //lblLinkedTo.Text = "";
        txtName.Text = "";
        txtMaker.Text = "";
        txtMakerType.Text = "";
        txtPart.Text = "";
        txtAltPart.Text = "";
        //txtLocation.Text = "";
        txtMinQty.Text = "";
        txtMaxQty.Text = "";
        txtStatutory.Text = "";
        txtWeight.Text = "";
        //txtStock.Text = "";
        txtDrawingNo.Text = "";
        txtSpecs.Text = "";       
    }
    public void GetMakerDetails()
    {
        string strSQL = "SELECT Maker,MakerType FROM ComponentMasterForVessel WHERE VesselCode = '" + VesselCode.Trim() + "' AND ComponentId = " + CompId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt.Rows.Count > 0)
        {
            txtMaker.Text = dt.Rows[0]["Maker"].ToString();
            txtMakerType.Text = dt.Rows[0]["MakerType"].ToString();
        }
    }
    #endregion ----------------------------------------------------------------

    #region ---------------- Events -------------------------------------------    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }        
        string compId = "";
       
        DataTable dt = new DataTable();
        string strCompId = "SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + CompCode.Trim() + "'";
        dt = Common.Execute_Procedures_Select_ByQuery(strCompId);
        compId = dt.Rows[0]["ComponentId"].ToString();

       
        Common.Set_Procedures("sp_InsertUpdateSpares");
        Common.Set_ParameterLength(16);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode.Trim()),
            new MyParameter("@ComponentId", compId.Trim()),
            new MyParameter("@Office_Ship", SpareId.Trim() == "" ? Session["UserType"].ToString() : hfOffice_Ship.Value.Trim()),
            new MyParameter("@SpareId", SpareId.Trim()),
            new MyParameter("@SpareName", txtName.Text.Trim()),
            new MyParameter("@Maker", txtMaker.Text.Trim()),
            new MyParameter("@MakerType", txtMakerType.Text.Trim()),
            new MyParameter("@PartNo", txtPart.Text.Trim()),
            new MyParameter("@AltPartNo", txtAltPart.Text.Trim()),
            //new MyParameter("@Location", txtLocation.Text.Trim()),
            new MyParameter("@MinQty", txtMinQty.Text.Trim()),
            new MyParameter("@MaxQty", txtMaxQty.Text.Trim()),
            new MyParameter("@StatutoryQty", txtStatutory.Text.Trim()),
            new MyParameter("@Weight", txtWeight.Text.Trim()),
            //new MyParameter("@Stock", txtStock.Text.Trim()),
            new MyParameter("@Specification", txtSpecs.Text.Trim()),
            new MyParameter("@DrawingNo", txtDrawingNo.Text.Trim()),
            new MyParameter("@Attachment", ""));

        DataSet dsSpares = new DataSet();
        dsSpares.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsSpares);
        if (res)
        {
            if (SpareId.Trim() == "")
            {
                string spareId = dsSpares.Tables[0].Rows[0]["SpareId"].ToString();
                //******************** File Upload *****************************************

                FileUpload img = (FileUpload)flAttachDocs;
                string FileName = "";
                string off_Ship = Session["UserType"].ToString();
                if (img.HasFile && img.PostedFile != null)
                {
                    HttpPostedFile File = flAttachDocs.PostedFile;
                    FileName = VesselCode + "_" + compId + "_" + off_Ship.Trim() + "_" + spareId + "_" + System.IO.Path.GetFileName(File.FileName);
                    string path = Server.MapPath("~/UploadFiles/UploadSpareDocs/");
                    flAttachDocs.SaveAs(path + FileName);

                    string strFileSQL = "UPDATE VesselSpareMaster  SET Attachment ='" + FileName + "' WHERE VesselCode='" + VesselCode.Trim() + "' AND ComponentId = " + compId.Trim() + " AND Office_Ship ='" + off_Ship.Trim() + "' AND SpareId = " + spareId + " ";
                    DataTable dtFileupdate = Common.Execute_Procedures_Select_ByQuery(strFileSQL);

                    Image1.ImageUrl = "~/UploadFiles/UploadSpareDocs/" + FileName;
                    ancFile.HRef = "~/UploadFiles/UploadSpareDocs/" + FileName;

                    ancFile.Visible = true;
                    Image1.Visible = true;
                }

                //
            }
            else
            {
                if (Image1.ImageUrl.ToString() != "")
                {
                    System.IO.File.Delete(Server.MapPath(Image1.ImageUrl));
                }

                //******************** File Upload *****************************************

                FileUpload img = (FileUpload)flAttachDocs;
                string FileName = "";
                string off_Ship = Session["UserType"].ToString();
                if (img.HasFile && img.PostedFile != null)
                {
                    HttpPostedFile File = flAttachDocs.PostedFile;
                    FileName = VesselCode + "_" + compId + "_" + off_Ship.Trim() + "_" + SpareId.Trim() + "_" + System.IO.Path.GetFileName(File.FileName);
                    string path = Server.MapPath("~/UploadFiles/UploadSpareDocs/");
                    flAttachDocs.SaveAs(path + FileName);

                    string strFileSQL = "UPDATE VesselSpareMaster  SET Attachment ='" + FileName + "' WHERE VesselCode='" + VesselCode.Trim() + "' AND ComponentId = " + compId.Trim() + " AND Office_Ship ='" + off_Ship.Trim() + "' AND SpareId = " + SpareId.Trim() + " ";
                    DataTable dtFileupdate = Common.Execute_Procedures_Select_ByQuery(strFileSQL);

                    Image1.ImageUrl = "~/UploadFiles/UploadSpareDocs/" + FileName;
                    ancFile.HRef = "~/UploadFiles/UploadSpareDocs/" + FileName;

                    ancFile.Visible = true;
                    Image1.Visible = true;
                }

                //
            }
            lblMessage.Text = "Spare Added/Udpated Successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refresh();", true);
           
        }
        else
        {
            lblMessage.Text = "Unable to Add/Update Spare.Error :" + Common.getLastError();
        }
        
    }    
    #endregion
}
