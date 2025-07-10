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

public partial class Ship_AddEditSpares : System.Web.UI.Page
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
    public int pKId
    {
        set { ViewState["PKID"] = value; }
        get { return Common.CastAsInt32(ViewState["PKID"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            SpareId = "";
            if (Request.QueryString["CompCode"] != null && Request.QueryString["VC"] != null)
            {
                VesselCode = Request.QueryString["VC"].ToString();
                CompCode = Request.QueryString["CompCode"].ToString();
                //BindStockLocation();
                GetComponent();
                GetMakerDetails();
                if (Session["UserType"].ToString() == "O")
                {

                    //trStockUpdate.Visible = false;
                    //trStockAdd.Visible = false;
                }
                
            }
            if (Request.QueryString["SPID"] != null && Request.QueryString["SPID"].ToString().Trim() != "")
            {
                SpareId = Request.QueryString["SPID"].ToString();
                hfOffice_Ship.Value = Request.QueryString["OffShip"].ToString(); 
                ShowDetails();
                //flAttachDocs.Enabled = false;
                
                //trStockUpdate.Visible = true;
                //BindStock();
                

                if (Session["UserType"].ToString() == "S")
                {

                    //trStockUpdate.Visible = false;
                    //trStockAdd.Visible = true;
                }
            }
            
            

        }

    }

    #region ---------------- USER DEFINED FUNCTIONS ---------------------------
    private void ShowDetails()
    {
        DataTable dtSpareDetails;
        string strSQL = "SELECT VSM.*,(Select LTRIM(RTRIM(ComponentCode)) + ' - ' + ComponentName from ComponentMaster WHERE LEN(ComponentCode)= (LEN('" + CompCode.Trim() + "')-2) AND  LEFT(ComponentCode,(LEN('" + CompCode.Trim() + "')-2)) = LEFT('" + CompCode.Trim() + "',(LEN('" + CompCode.Trim() + "')-2)))As LinkedTo FROM VSL_VesselSpareMaster VSM " +
                        "INNER JOIN ComponentMaster CM ON VSM.ComponentId = CM.ComponentId " +                      
                        "WHERE CM.ComponentCode = '" + CompCode.Trim() + "' AND VSM.VesselCode = '" + VesselCode.Trim() + "' AND SpareId = '" + SpareId + "' AND Office_Ship='" + hfOffice_Ship.Value.ToString() + "' ";
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
            //if (Convert.ToInt32(dtSpareDetails.Rows[0]["VStockLocationId"]) > 0)
            //{
            //    ddlStockLocation.SelectedValue = dtSpareDetails.Rows[0]["VStockLocationId"].ToString();
            //}
            

            
            if (dtSpareDetails.Rows[0]["Attachment"].ToString() != "")
            {
                Image1.ImageUrl = "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + dtSpareDetails.Rows[0]["Attachment"].ToString();
                Image2.ImageUrl = "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + dtSpareDetails.Rows[0]["Attachment"].ToString();
                ancFile.HRef = "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + dtSpareDetails.Rows[0]["Attachment"].ToString();
                
                ancFile.Visible = true;
                Image1.Visible = false;
                Image2.Visible = true;
            }
            if (Session["UserType"].ToString() == "O")
            {
                

                btnSave.Visible = true;
                trCritical.Visible = false;

            }
            else
            {
                if (dtSpareDetails.Rows[0]["Critical"].ToString() == "C" || dtSpareDetails.Rows[0]["isOR"].ToString() == "True")
                {
                    btnSave.Visible = false;
                    trCritical.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                    trCritical.Visible = false;
                }
            }
            btnActive.Visible = (dtSpareDetails.Rows[0]["Status"].ToString().Trim() == "I");
            btnInactive.Visible = (dtSpareDetails.Rows[0]["Status"].ToString().Trim() == "A");
        }
        else
        {
            ClearFields();
        }
    }
    //private void BindStockLocation()
    //{
    //    DataTable dtStockLocations = new DataTable();
    //    string strStockLocation = "select StockLocationId,StockLocation from SpareStockLocation with(nolock) where status = 'A'";
    //    dtStockLocations = Common.Execute_Procedures_Select_ByQuery(strStockLocation);

    //    ddlStockLocation.DataSource = dtStockLocations;
    //    ddlStockLocation.DataTextField = "StockLocation";
    //    ddlStockLocation.DataValueField = "StockLocationId";
    //    ddlStockLocation.DataBind();
    //    ddlStockLocation.Items.Insert(0, "< Select >");
    //}
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
            MessageBox1.ShowMessage("Please select a component.", true);
            return false;
        }
        CompCode = CompCode.Trim();
        //string[] codes = { "644","651","731","432","501" ,"554", "721" , "801" };
        string[] codes = { "644", "651", "601" };
        foreach (string code in codes)
        {
            if(CompCode.StartsWith(code) && CompCode.Length > code.Length)
            {
                MessageBox1.ShowMessage("Spare entry on this component is not allowed. Please contact to office.", true);
                return false;
            }
        }
       
        if (txtName.Text == "")
        {
            MessageBox1.ShowMessage("Please enter Spare Name.", true);
            txtName.Focus();
            return false;
        }
        if (txtMaker.Text == "")
        {
            MessageBox1.ShowMessage("Please enter Maker.", true);
            txtMaker.Focus();
            return false;
        }

       
        //if (txtMakerType.Text == "")
        //{
        //    lblMessage.Text = "Please enter Maker Type.";
        //    txtMakerType.Focus();
        //    return false;
        //}
        string _Office_Ship = (SpareId.Trim() == "") ? Session["UserType"].ToString() : hfOffice_Ship.Value.Trim();        

        if (txtPart.Text.Trim() != "")
        {
            string strDuplicate = "SELECT PartNo FROM VSL_VesselSpareMaster WHERE vesselcode = '" + VesselCode.Trim() + "' AND ComponentId = " + CompId + " AND Office_Ship = '" + _Office_Ship + "' AND PartNo = '" + txtPart.Text.Trim() + "' AND SpareId <> '" + SpareId.Trim() + "'  ";
            DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQuery(strDuplicate);

            if (dtDuplicate.Rows.Count > 0)
            {
                MessageBox1.ShowMessage("Part# already exists.", true);
                txtPart.Focus();
                return false;
            }

            return true;
        }
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
            MessageBox1.ShowMessage("Specification can not be greater than 500 characters.", true);
            txtSpecs.Focus();
            return false;
        }

        //if (Convert.ToInt32(ddlStockLocation.SelectedValue) == 0)
        //{
        //    MessageBox1.ShowMessage("Please select Spare Stock Location.", true);
        //    ddlStockLocation.Focus();
        //    return false;
        //}
        return true;
    }
    private void ClearFields()
    {
        //lblLinkedTo.Text = "";
        txtName.Text = "";
        //ddlStockLocation.SelectedIndex = 0;
       
        txtMaker.Text = "";
        txtMakerType.Text = "";
        txtPart.Text = "";
        //txtPartName.Text = "";
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
        string strSQL = "SELECT Maker,MakerType FROM VSL_ComponentMasterForVessel WHERE VesselCode = '" + VesselCode.Trim() + "' AND ComponentId = " + CompId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt.Rows.Count > 0)
        {
            txtMaker.Text = dt.Rows[0]["Maker"].ToString();
            txtMakerType.Text = dt.Rows[0]["MakerType"].ToString();
        }
    }
    //public void BindStock()
    //{
    //    btnAdd.Text = "Add";

    //    string strSQL = "SELECT REPLACE(CONVERT(VARCHAR(12), RecdDate, 106),' ','-') AS RecdDate,QtyRecd,StockLocation,SpareId FROM VSL_StockInventory WHERE VesselCode ='" + VesselCode + "' AND ComponentId = " + CompId + " AND Office_Ship = '" + hfOffice_Ship.Value.Trim() + "' AND SpareId = " + SpareId + " And Status='A'";
    //    DataTable dtStock = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    if (dtStock.Rows.Count > 0)
    //    {
    //        rptStock.DataSource = dtStock;
    //        rptStock.DataBind();
    //        lblTotRecdQty.Text = dtStock.Compute("sum(QtyRecd)","").ToString();            
    //    }
    //    else
    //    {
    //        rptStock.DataSource = null;
    //        rptStock.DataBind();
    //    }
    //    BindInventoryStatus();
        
        
    //}
    //private void BindInventoryStatus()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC dbo.sp_getInventorySummary '" + VesselCode + "'," + CompId + ",'" + hfOffice_Ship.Value.Trim() + "'," + SpareId + ",'" + DateTime.Today.ToString("dd-MMM-yyyy") + "'");
    //    lblTQtyRecd.Text=dt.Rows[0][0].ToString();
    //    lblTQtyCons.Text = dt.Rows[0][1].ToString();
    //    lblTROB.Text = dt.Rows[0][2].ToString();
    //    if (Common.CastAsInt32(lblTROB.Text) < 0)
    //        lblTROB.Text = "0";
    //}
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
        string _Office_Ship = (SpareId.Trim() == "") ? Session["UserType"].ToString() : hfOffice_Ship.Value.Trim();
        string strCompId = "SELECT ComponentId FROM ComponentMaster WHERE ComponentCode = '" + CompCode.Trim() + "'";
        dt = Common.Execute_Procedures_Select_ByQuery(strCompId);
        compId = dt.Rows[0]["ComponentId"].ToString();
        Common.Set_Procedures("sp_Ship_InsertUpdateSpares");
        Common.Set_ParameterLength(17);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode.Trim()),
            new MyParameter("@ComponentId", compId.Trim()),
            new MyParameter("@Office_Ship", _Office_Ship),
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
            new MyParameter("@Attachment", ""),
            new MyParameter("@Location", "")
           // new MyParameter("@stockLocationId", Convert.ToInt32(ddlStockLocation.SelectedValue))
            );

        DataSet dsSpares = new DataSet();
        dsSpares.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsSpares);
        if (res)
        {
            hfOffice_Ship.Value = _Office_Ship;
            if (SpareId.Trim() == "")
            {
                SpareId = dsSpares.Tables[0].Rows[0]["SpareId"].ToString();
                //******************** File Upload *****************************************

                FileUpload img = (FileUpload)flAttachDocs;
                string FileName = "";
                if (img.HasFile && img.PostedFile != null)
                {
                    HttpPostedFile File = flAttachDocs.PostedFile;
                    FileName = VesselCode + "_" + compId + "_" + Session["UserType"].ToString() + "_" + SpareId + "_" + System.IO.Path.GetFileName(File.FileName);
                    string path = Server.MapPath("~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/");
                    flAttachDocs.SaveAs(path + FileName);


                    string strFileSQL = "UPDATE VSL_VesselSpareMaster  SET Attachment ='" + FileName + "' WHERE VesselCode='" + VesselCode.Trim() + "' AND ComponentId = " + compId.Trim() + " AND Office_Ship ='" + hfOffice_Ship.Value + "' AND SpareId = " + SpareId + " ";
                    DataTable dtFileupdate = Common.Execute_Procedures_Select_ByQuery(strFileSQL);
                                        
                    Image1.ImageUrl = "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + FileName;
                    Image2.ImageUrl = "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + FileName;
                    ancFile.HRef = "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + FileName;

                    ancFile.Visible = true;
                    Image1.Visible = false;
                    Image2.Visible = true;

                }

                //
            }
            else
            {

                //******************** File Upload *****************************************

                FileUpload img = (FileUpload)flAttachDocs;
                string FileName = "";
                if (img.HasFile && img.PostedFile != null)
                {
                    if (Image1.ImageUrl.ToString() != "")
                    {
                        System.IO.File.Delete(Server.MapPath(Image1.ImageUrl));
                    }

                    

                    HttpPostedFile File = flAttachDocs.PostedFile;
                    FileName = VesselCode + "_" + compId + "_" + Session["UserType"].ToString() + "_" + SpareId + "_" + System.IO.Path.GetFileName(File.FileName);
                    string path = Server.MapPath("~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/");
                    flAttachDocs.SaveAs(path + FileName);


                    string strFileSQL = "UPDATE VSL_VesselSpareMaster  SET Attachment ='" + FileName + "' WHERE VesselCode='" + VesselCode.Trim() + "' AND ComponentId = " + compId.Trim() + " AND Office_Ship ='" + hfOffice_Ship.Value + "' AND SpareId = " + SpareId + " ";
                    DataTable dtFileupdate = Common.Execute_Procedures_Select_ByQuery(strFileSQL);

                    
                    Image1.ImageUrl = "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + FileName;
                    Image2.ImageUrl = "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + FileName;
                    ancFile.HRef = "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + FileName;
                    ancFile.Visible = true;
                    Image1.Visible = false;
                        Image2.Visible = true;

                }

                //
                
                
            }
            MessageBox1.ShowMessage("Spare Added/Udpated Successfully.", false);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refresh();", true);
            if (Session["UserType"].ToString() == "S")
            {
                trStockUpdate.Visible = true;
            }

            //if (Request.QueryString["SPID"].Trim() == "")
            //{
            //    SpareId = "";
            //}
            //SpareId = dsSpares.Tables[0].Rows[0]["SpareId"].ToString();
        }
        else
        {
            MessageBox1.ShowMessage("Unable to Add/Update Spare.Error :" + Common.getLastError(), true);
        }

    }
    protected void btnActive_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("sp_Update_Ship_SpareStatus");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode.Trim()),
                new MyParameter("@ComponentId", CompId),
                new MyParameter("@Office_Ship", hfOffice_Ship.Value.ToString().Trim()),
                new MyParameter("@SpareId", SpareId),
                new MyParameter("@Status", "A")
                );

            DataSet dsActive = new DataSet();
            dsActive.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsActive);

            if (res)
            {
                MessageBox1.ShowMessage("Spare activated successfully.", false);
                btnInactive.Visible = true;
                btnActive.Visible = false;
            }

        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to active spare.Error :" + ex.Message + Common.getLastError(), true);
        }

    }
    protected void btnInactive_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("sp_Update_Ship_SpareStatus");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode.Trim()),
                new MyParameter("@ComponentId", CompId),
                new MyParameter("@Office_Ship", hfOffice_Ship.Value.ToString().Trim()),
                new MyParameter("@SpareId", SpareId),
                new MyParameter("@Status", "I")
                );

            DataSet dsActive = new DataSet();
            dsActive.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsActive);

            if (res)
            {
                MessageBox1.ShowMessage("Spare inactivated successfully.", false);
                btnActive.Visible = true;
                btnInactive.Visible = false;
            }

        }
        catch (Exception ex)
        {
            MessageBox1.ShowMessage("Unable to inactive spare.Error :" + ex.Message + Common.getLastError(), true);
        }
    }

    #region ********************* SPARE INVENTORY **************************
    
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    if (SpareId.Trim() == "")
    //    {
    //        MessageBox2.ShowMessage("Please add spare first.", true);            
    //        return;
    //    }
    //    if (txtRecdDt.Text.Trim() == "")
    //    {
    //        MessageBox2.ShowMessage("Please enter Recd date.", true);
    //        txtRecdDt.Focus();
    //        return;
    //    }
    //    DateTime dt;
    //    if (!DateTime.TryParse(txtRecdDt.Text.Trim(), out dt))
    //    {
    //        MessageBox2.ShowMessage("Please enter valid date.", true);
    //        txtRecdDt.Focus();
    //        return;
    //    }
    //    if (txtRecdQty.Text.Trim() == "")
    //    {
    //        MessageBox2.ShowMessage("Please enter Recd qty.", true);
    //        txtRecdQty.Focus();
    //        return;
    //    }
    //    if (txtStockLocation.Text.Trim() == "")
    //    {
    //        MessageBox2.ShowMessage("Please enter stock location.", true);
    //        txtStockLocation.Focus();
    //        return;
    //    }

    //    Common.Set_Procedures("sp_InsertSparesInventory");
    //    Common.Set_ParameterLength(8);
    //    Common.Set_Parameters(
    //        new MyParameter("@VesselCode", VesselCode.Trim()),
    //        new MyParameter("@ComponentId", CompId),
    //        new MyParameter("@Office_Ship", hfOffice_Ship.Value.Trim()),
    //        new MyParameter("@SpareId", SpareId.Trim()),
    //        new MyParameter("@PkId", pKId),
    //        new MyParameter("@RecdDate", txtRecdDt.Text.Trim()),
    //        new MyParameter("@QtyRecd", txtRecdQty.Text.Trim()),
    //        new MyParameter("@StockLocation", txtStockLocation.Text.Trim())
            
    //        );

    //    DataSet dsStock = new DataSet();
    //    dsStock.Clear();
    //    Boolean res;
    //    res = Common.Execute_Procedures_IUD(dsStock);
    //    if (res)
    //    {
    //        MessageBox2.ShowMessage("Stock updated successfully.", false);
    //        BindStock();
    //        btnAdd.Text = "Add";
    //    }
    //    else
    //    {
    //        MessageBox2.ShowMessage("Unable to Add/Update Stock.Error :" + Common.getLastError(), true);
    //    }
    //}

    //protected void btnEditStock_Click(object sender, ImageClickEventArgs e)
    //{
    //    int spareId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

    //    string strSQL = "SELECT REPLACE(CONVERT(VARCHAR(12), RecdDate, 106),' ','-') AS RecdDate,QtyRecd,StockLocation,PKID FROM VSL_StockInventory WHERE VesselCode ='" + VesselCode + "' AND ComponentId = " + CompId + " AND Office_Ship = '" + hfOffice_Ship.Value.Trim() + "' AND SpareId = " + SpareId + " ";
    //    DataTable dtStock = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    if (dtStock.Rows.Count > 0)
    //    {
    //        txtRecdDt.Text = dtStock.Rows[0]["RecdDate"].ToString();
    //        txtRecdQty.Text = dtStock.Rows[0]["QtyRecd"].ToString();
    //        txtStockLocation.Text = dtStock.Rows[0]["StockLocation"].ToString();
    //        pKId = Common.CastAsInt32(dtStock.Rows[0]["PKID"].ToString());

    //        btnAdd.Text = "Edit";

    //    }
    //}

    #endregion

    #endregion
    
}
