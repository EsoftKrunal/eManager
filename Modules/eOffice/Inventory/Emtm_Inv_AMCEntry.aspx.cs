using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

public partial class emtm_Inventory_Emtm_Inv_AMCEntry : System.Web.UI.Page
{
    public int ItemID
    {
        set
        {
            ViewState["ItemID"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["ItemID"]);
        }
    }
    public int MidCat
    {
        set
        {
            ViewState["MidCat"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["MidCat"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        lblEqpMsg.Text = "";
        if (!Page.IsPostBack)
        {
            MidCat = Common.CastAsInt32(Request.QueryString["MidCat"]);

            BindCurrency();
            BindEquipmentddl();
            BindMinCategory();
            if (Request.QueryString["ITEMId"] != null && (Request.QueryString["Mode"].ToString() == "Edit" || Request.QueryString["Mode"].ToString() == "View"))
            {
                ItemID = Common.CastAsInt32(Request.QueryString["ITEMId"].ToString());
                ShowItem();
                BindEquipmentGrid();
                BindAttachment();
                BindEquipmentddl();
            }
            if (Request.QueryString["Mode"].ToString() == "View")
            {
                lblPageHeader.Text = "[ AMC ]";
                btnSave.Visible = false;                
                btnCancel.Visible = false;
                trEquipment.Visible = false;
                trAttachment.Visible = false;
            }
        }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtContractNo.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter Contract#.";
                txtContractNo.Focus();
                return;
            }
            if (txtStartDt.Text.Trim() != "" && txtEndDt.Text.Trim() != "")
            {
                if (Convert.ToDateTime(txtStartDt.Text.Trim()) == Convert.ToDateTime(txtEndDt.Text.Trim()))
                {
                    lblMsg.Text = "Start date and end date can not be same.";
                    txtStartDt.Focus();
                    return;
                }
                if (Convert.ToDateTime(txtStartDt.Text.Trim()) > Convert.ToDateTime(txtEndDt.Text.Trim()))
                {
                    lblMsg.Text = "End date can not be less than start date.";
                    txtEndDt.Focus();
                    return;
                }
            }

           

            string sqlDuplicate = "SELECT * FROM IVM_ITEMS_AMC WHERE ItemId != " + ItemID + " AND CONTRACTNO = '" + txtContractNo.Text.Trim() + "' ";
            DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(sqlDuplicate);

            if (dtDuplicate.Rows.Count > 0)
            {
                lblMsg.Text = "Contract# already exists.";
                txtContractNo.Focus();
                return;
            }


            string sql = "EXEC DBO.HR_INV_InsertUpdateItems_AMC " + ItemID + "," + ddlMinCat.SelectedValue + ",'" + txtContractNo.Text.Trim() + "'," + ((txtStartDt.Text.Trim() == "") ? "null" : ("'"+txtStartDt.Text.Trim())+"'") + "," + ((txtEndDt.Text.Trim() == "") ? "null" : ("'"+txtEndDt.Text.Trim())+"'") + ",'" + txtConVendor.Text.Trim() + "','" + txtConDetails.Text.Trim() + "'," + Common.CastAsDecimal(txtPrice.Text.Trim()) + ",'" + ddlCurrency.SelectedValue.Trim() + "'," + Common.CastAsDecimal(lblTotalUSDoler.Text.Trim()) + " ; SELECT -1 ";


            DataTable dtInsertUpdate = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dtInsertUpdate != null)
            {
                if (ItemID == 0)
                    lblMsg.Text = "Item saved successfully.";
                else
                    lblMsg.Text = "Item updated successfully.";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ref", "RefreshParent();", true);
                ItemID = Common.CastAsInt32(dtInsertUpdate.Rows[0][0].ToString());
                BindEquipmentddl();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save Item. Error : " + ex.Message.ToString();

        }
    }
    
    //public void CalculateDepVal()
    //{
    //    if (txtPurchaseDt.Text.Trim() != "" && ddlCurrency.SelectedIndex != 0 && txtPrice.Text.Trim() != "")
    //    {
    //        decimal dep = Convert.ToDecimal(txtPrice.Text.Trim()) / 60;
    //        //int currMonth = DateTime.Today.Month;
    //        //int purMonth = Convert.ToDateTime(txtPurchaseDt.Text.Trim()).Month;
    //        string sql = "SELECT DATEDIFF(mm,'" + txtPurchaseDt.Text.Trim() + "',getdate())As Diff ";
    //        DataTable dtDiff = Common.Execute_Procedures_Select_ByQueryCMS(sql);

    //        //TimeSpan monthDiff = DateTime.Today.Subtract(Convert.ToDateTime(txtPurchaseDt.Text.Trim()));
    //        //int diff =(monthDiff.Days)/30;
    //        int diff = Common.CastAsInt32(dtDiff.Rows[0]["Diff"].ToString());

    //        decimal depVal = (dep * diff);

    //        decimal depvalCal = Convert.ToDecimal(txtPrice.Text.Trim()) - depVal;

    //        txtDepVal.Text = Math.Round(depvalCal).ToString();
    //    }

    //}
    protected void txtPrice_TextChanged(object sender, EventArgs e)
    {
        //if (txtPurchaseDt.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Please enter purchase date.";
        //    txtPurchaseDt.Focus();
        //    return;
        //}
        //DateTime dt;

        //if (!DateTime.TryParse(txtPurchaseDt.Text.Trim(), out dt))
        //{
        //    lblMsg.Text = "Please enter valid date.";
        //    txtPurchaseDt.Focus();
        //    return;
        //}
        CurrencyConversion();
    }

    // Function ------------------------------------------------------------------------------------
    public void ShowItem()
    {
        string sql = "SELECT CONTRACTNO,MinCatID,REPLACE(CONVERT(VARCHAR(15),STARTDATE,106),' ','-') AS STARTDATE ,REPLACE(CONVERT(VARCHAR(15),ENDDATE,106),' ','-') AS ENDDATE,CONTRACT_AMOUNT,CURRENCY,AMOUNT_USD,CONTRACT_VENDOR,SUPPORT_CONTACT_DETAILS,Office,AssignTo,REPLACE(CONVERT(VARCHAR(15),AssignOn,106),' ','-') AS AssignOn FROM IVM_ITEMS_AMC WHERE ITEMID = " + ItemID;
        DataTable dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtItems.Rows.Count > 0)
        {
            txtContractNo.Text = dtItems.Rows[0]["CONTRACTNO"].ToString();
            txtStartDt.Text = dtItems.Rows[0]["STARTDATE"].ToString();
            txtEndDt.Text = dtItems.Rows[0]["ENDDATE"].ToString();
            txtPrice.Text = dtItems.Rows[0]["CONTRACT_AMOUNT"].ToString();
            txtConVendor.Text = dtItems.Rows[0]["CONTRACT_VENDOR"].ToString();
            txtConDetails.Text = dtItems.Rows[0]["SUPPORT_CONTACT_DETAILS"].ToString();
            ddlCurrency.SelectedValue = dtItems.Rows[0]["CURRENCY"].ToString();
            lblTotalUSDoler.Text = dtItems.Rows[0]["AMOUNT_USD"].ToString();
            ddlMinCat.SelectedValue = dtItems.Rows[0]["MinCatID"].ToString();

        }
        btnSave.Text = "Update";
    }
    private void BindAttachment()
    {
        string strSQL = "SELECT FILEID,ATTACHMENTTEXT,[FILENAME] FROM DBO.IVM_ITEMS_ATTACHMENTDETAILS WHERE FORMTYPE = 'AMC' AND ITEMID = " + ItemID;
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

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
    public void BindCurrency()
    {
        DataSet ds = new DataSet();

        try
        {
            // To run on client side
            string strSQL = "SELECT Curr FROM DBO.VW_tblWebCurr ORDER BY Curr";
            ds = Budget.getTable(strSQL);
        }
        catch (Exception ex)
        {
            // To run My side
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = myConnection;
            SqlDataAdapter Adp = new SqlDataAdapter();
            Adp.SelectCommand = cmd;
            cmd.CommandText = "SELECT Curr FROM VW_tblWebCurr ORDER BY Curr";
            Adp.Fill(ds, "tbl");
        }


        ddlCurrency.DataSource = ds;
        ddlCurrency.DataTextField = "Curr";
        ddlCurrency.DataValueField = "Curr";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("< Select >", "0"));
        ddlCurrency.SelectedIndex = 0;
    }
    public void CurrencyConversion()
    {
        DataSet ds = new DataSet();

        // To run on client side
        try
        {
            string sql = "select top 1 exc_rate from DBO.XCHANGEDAILY where RateDate<= getdate()  and For_Curr='" + ddlCurrency.SelectedValue.Trim() + "' order by RateDate desc";
            ds = Budget.getTable(sql);
        }
        catch (Exception ex)
        {
            //To run on my side
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = myConnection;
            SqlDataAdapter Adp = new SqlDataAdapter();
            string sql = "select top 1 exc_rate from DBO.XCHANGEDAILY where RateDate<= getdate() and For_Curr='" + ddlCurrency.SelectedValue.Trim() + "' order by RateDate desc";
            Adp.SelectCommand = cmd;
            cmd.CommandText = sql;
            Adp.Fill(ds, "tbl");
        }


        if (ds != null)
        {
            DataTable Dt = ds.Tables[0];
            decimal CurrRate = 0;
            if (Dt != null)
            {
                if (Dt.Rows.Count > 0)
                {
                    CurrRate = Math.Round(Common.CastAsDecimal(Dt.Rows[0][0]), 2);
                }
            }
            if (txtPrice.Text != "")
            {
                if (CurrRate != 0)
                    lblTotalUSDoler.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtPrice.Text) / CurrRate), 2));
                else
                    lblTotalUSDoler.Text = "0";
            }
            else
                lblTotalUSDoler.Text = "0";

            //CalculateDepVal();

        }
    }
    public void BindMinCategory()
    {
        string sql = "SELECT MinCatID,MinCatName FROM  dbo.IVM_MinCategory where MidCatID="+MidCat+"";
        DataTable dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlMinCat.DataSource = dtItems;
        ddlMinCat.DataTextField = "MinCatName";
        ddlMinCat.DataValueField = "MinCatID";
        ddlMinCat.DataBind();
        ddlMinCat.Items.Insert(0, new ListItem("< None >", "0"));

    }



    #region ATTACHMENT

    protected void btnSaveAttachment_Click(object sender, EventArgs e)
    {
        FileUpload img = (FileUpload)flAttachDocs;
        if (ItemID == 0)
        {
            lblMsg.Text = "First save the item.";
            return;
        }

        if (txtAttachmentText.Text.Trim() == "" && img.HasFile)
        {
            lblMsg.Text = "Please enter attachment text.";
            txtAttachmentText.Focus();
            return;
        }

        if (txtAttachmentText.Text.Trim() != "" && !img.HasFile)
        {
            lblMsg.Text = "Please select a file to upload.";
            img.Focus();
            return;
        }

        string FileName = "";
        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;
            FileName = "AMC_" + ItemID.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);

            string sql = "EXEC DBO.HR_INV_InsertItem_Attachment 'AMC'," + ItemID + ", '" + txtAttachmentText.Text.Trim() + "', '" + FileName + "' ";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);


            if (dt.Rows.Count > 0)
            {

                string path = Server.MapPath("~/EMANAGERBLOB/Inventory/" + FileName);
                //if (!System.IO.Directory.Exists(path))
                //{
                //    System.IO.Directory.CreateDirectory(path);
                //}
                flAttachDocs.SaveAs(path);


                BindAttachment();
                lblMsg.Text = "Document uploaded successfully.";
            }
            else
            {
                lblMsg.Text = "Unable to upload document.Error :" + Common.getLastError();
            }
        }
        else
        {
            lblMsg.Text = "Please select a file to upload.";
            img.Focus();
            return;
        }
    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        int intFileID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string strdetail = "SELECT [FILENAME] FROM DBO.IVM_ITEMS_ATTACHMENTDETAILS WHERE FILEID =" + intFileID;
        DataTable dtFile = Common.Execute_Procedures_Select_ByQueryCMS(strdetail);
        string FileName = dtFile.Rows[0]["FILENAME"].ToString();


        string str = "DELETE  FROM  DBO.IVM_ITEMS_ATTACHMENTDETAILS WHERE FILEID=" + intFileID + " ; select -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt != null)
        {
            if (File.Exists(Server.MapPath("~/EMANAGERBLOB/Inventory/" + FileName)))
            {
                File.Delete(Server.MapPath("~/EMANAGERBLOB/Inventory/" + FileName));
            }

            lblMsg.Text = "Document deleted successfully.";
            BindAttachment();
        }
    }

    #endregion

    #region EQUIPMENTS
    private void BindEquipmentddl()
    {
        ddlEquipment.Items.Clear();

        string sql = "SELECT AssetCode,ItemId FROM IVM_ITEMS_IT_Hardware WHERE ItemId NOT IN (SELECT ITHardware_ItemId FROM IVM_AMC_Details WHERE AMC_ItemId =" + ItemID + " )";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            ddlEquipment.DataSource = dt;
            ddlEquipment.DataTextField = "AssetCode";
            ddlEquipment.DataValueField = "ItemId";
            ddlEquipment.DataBind();
        }
        ddlEquipment.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void BindEquipmentGrid()
    {
        rptEqipDetails.DataSource = null;
        rptEqipDetails.DataBind();

        string strSQL = "SELECT Id,AssetCode,Maker,ModelNumber,SRNumber,REPLACE(CONVERT(varchar(15),PurchaseDate,106),' ','-') AS PurchaseDate,VendorName,WarrantyExp,Price,Currency,Amount,DepValue FROM IVM_ITEMS_IT_Hardware IT " +
                        "INNER JOIN IVM_AMC_Details AD ON IT.ItemId = AD.ITHardware_ItemId " +
                        "WHERE AD.AMC_ItemId = " + ItemID;
        DataTable dtEqpDetail = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);
        if (dtEqpDetail.Rows.Count > 0)
        {   
            rptEqipDetails.DataSource = dtEqpDetail;
            rptEqipDetails.DataBind();
        }
    }
    protected void btnSaveEquip_Click(object sender, EventArgs e)
    {
        if (ItemID == 0)
        {
            lblMsg.Text = "Please save item first.";
            return;
        }
        if (ddlEquipment.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select an equipment.";
            ddlEquipment.Focus();
            return;
        }

        string insertSql = "INSERT INTO IVM_AMC_Details (AMC_ItemId,ITHardware_ItemId) VALUES (" + ItemID + "," + ddlEquipment.SelectedValue.Trim() + " ); SELECT -1 ";
        DataTable dtDetails = Common.Execute_Procedures_Select_ByQueryCMS(insertSql);
        if (dtDetails.Rows.Count > 0)
        {
            lblMsg.Text = "Equipment saved successfully.";
            BindEquipmentddl();
            BindEquipmentGrid();
        }
    }

    protected void imgDelEqip_OnClick(object sender, EventArgs e)
    {
        int Id = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

        string delSQL = "DELETE FROM IVM_AMC_Details WHERE Id = " + Id + " ; SELECT -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(delSQL);
        if (dt.Rows.Count > 0)
        {
            lblMsg.Text = "Equipment deleted successfully.";
            BindEquipmentddl();
            BindEquipmentGrid();
        }
        else
        {
            lblMsg.Text = "Unable to save equipment.ERROR : " + Common.getLastError(); 
        }
            

    }

    #endregion
    
}
