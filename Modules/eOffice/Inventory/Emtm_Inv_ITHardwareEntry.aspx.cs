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

public partial class emtm_Inventory_Emtm_Inv_ITHardwareEntry : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            BindCurrency();
            BindOffices();
            BindEmployees();
            
            MidCat = Common.CastAsInt32(Request.QueryString["MidCat"]);
            BindMinCategory();
            if (Request.QueryString["ITEMId"] != null && (Request.QueryString["Mode"].ToString() == "Edit" || Request.QueryString["Mode"].ToString() == "View"))
            {
                ItemID = Common.CastAsInt32(Request.QueryString["ITEMId"].ToString());
                ShowItem();
                BindAttachment();
            }
            if (Request.QueryString["Mode"].ToString() == "View")
            {
                lblPageHeader.Text = "[ IT -> Hardware ]";
                btnSave.Visible = false;
                btnSaveAttachment.Visible = false;
                btnCancel.Visible = false;
                trAttachment.Visible = false;
            }
        }
    }

    
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployees();
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtAssetCode.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter asset code.";
                txtAssetCode.Focus();
                return;
            }

            DateTime dt1;
            if (txtWarrentyExp.Text.Trim() != "")
                if (!DateTime.TryParse(txtWarrentyExp.Text.Trim(), out dt1))
                {
                    lblMsg.Text = "Please enter valid expiry date.";
                    txtWarrentyExp.Focus();
                    return;
                }

            DateTime dt;
            if (txtPurchaseDt.Text.Trim()!="")
            if (!DateTime.TryParse(txtPurchaseDt.Text.Trim(), out dt))
            {
                lblMsg.Text = "Please enter valid date.";
                txtPurchaseDt.Focus();
                return;
            }

            if (txtPurchaseDt.Text.Trim()!="")
            if (Convert.ToDateTime(txtPurchaseDt.Text.Trim()) > DateTime.Today.Date)
            {
                lblMsg.Text = "Purchase date can not be more than today.";
                txtPurchaseDt.Focus();
                return;

            }

            if (txtCreatedOn.Text.Trim() != "")
            {
                if (!DateTime.TryParse(txtCreatedOn.Text.Trim(), out dt))
                {
                    lblMsg.Text = "Please enter valid date.";
                    txtCreatedOn.Focus();
                    return;
                }
            }

            string sqlDuplicate = "SELECT * FROM IVM_ITEMS_IT_Hardware WHERE ItemId != " + ItemID + " AND AssetCode = '" + txtAssetCode.Text.Trim() + "' ";
            DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(sqlDuplicate);

            if (dtDuplicate.Rows.Count > 0)
            {
                lblMsg.Text = "Asset Code already exists.";
                txtAssetCode.Focus();
                return;
            }


            string sql = "EXEC DBO.HR_INV_InsertUpdateItems_ITHardware " + ItemID + ",'" + txtAssetCode.Text.Trim() + "'," + ddlMinCat.SelectedValue + ",'" + txtMaker.Text.Trim() + "','" + txtModelNo.Text.Trim() + "','" + txtSrNo.Text.Trim() + "'," + ((txtPurchaseDt.Text.Trim() == "") ? "null" : "'" + txtPurchaseDt.Text.Trim() + "'") + ",'"+txtVendorName.Text.Trim()+"'," + ((txtWarrentyExp.Text.Trim() == "") ? "null" : "'" + txtWarrentyExp.Text.Trim() + "'") + "," + Common.CastAsDecimal(txtPrice.Text.Trim()) + ",'" + ddlCurrency.SelectedValue.Trim() + "'," + Common.CastAsDecimal(lblTotalUSDoler.Text.Trim()) + "," + Common.CastAsDecimal(txtDepVal.Text.Trim()) + "," + ddlOffice.SelectedValue.Trim() + "," + ddlEmp.SelectedValue.Trim() + "," + ((txtCreatedOn.Text.Trim() == "") ? "null" : "'" + txtCreatedOn.Text.Trim() + "'") + " ; SELECT -1 ";


            DataTable dtInsertUpdate = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dtInsertUpdate != null)
            {
                if (ItemID == 0)
                    lblMsg.Text = "Item saved successfully.";
                else
                    lblMsg.Text = "Item updated successfully.";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ref", "RefreshParent();", true);
                ItemID = Common.CastAsInt32(dtInsertUpdate.Rows[0][0].ToString());
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save Item. Error : " + ex.Message.ToString();
            
        }
    }
    protected void txtPrice_TextChanged(object sender, EventArgs e)
    {
        if (txtPurchaseDt.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter purchase date.";
            txtPurchaseDt.Focus();
            return;
        }
        DateTime dt;

        if (!DateTime.TryParse(txtPurchaseDt.Text.Trim(), out dt))
        {
            lblMsg.Text = "Please enter valid date.";
            txtPurchaseDt.Focus();
            return;
        }
        if (Convert.ToDateTime(txtPurchaseDt.Text.Trim()) > DateTime.Today.Date)
        {
            lblMsg.Text = "Purchase date can not be more than today.";
            txtPurchaseDt.Focus();
            return;

        }
        CurrencyConversion();
    }


    // Function ------------------------------------------------------------------------
    public void ShowItem()
    {
        string sql = "SELECT AssetCode,MinCatID,Maker,ModelNumber,SRNumber,REPLACE(CONVERT(Varchar(15),PurchaseDate,106),' ','-') AS PurchaseDate,VendorName,REPLACE(CONVERT(Varchar(15),WarrantyExp,106),' ','-')WarrantyExp,Price,Currency,Amount,DepValue,Office,AssignTo,REPLACE(CONVERT(VARCHAR(15),AssignOn,106),' ','-') AS AssignOn FROM IVM_ITEMS_IT_Hardware WHERE ItemId = " + ItemID;
        DataTable dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtItems.Rows.Count > 0)
        {
            txtAssetCode.Text = dtItems.Rows[0]["AssetCode"].ToString();
            txtMaker.Text = dtItems.Rows[0]["Maker"].ToString();
            txtModelNo.Text = dtItems.Rows[0]["ModelNumber"].ToString();
            txtSrNo.Text = dtItems.Rows[0]["SRNumber"].ToString();
            txtPurchaseDt.Text = dtItems.Rows[0]["PurchaseDate"].ToString();
            txtVendorName.Text = dtItems.Rows[0]["VendorName"].ToString();
            txtWarrentyExp.Text = dtItems.Rows[0]["WarrantyExp"].ToString();
            txtPrice.Text = dtItems.Rows[0]["Price"].ToString();
            ddlCurrency.SelectedValue = dtItems.Rows[0]["Currency"].ToString();
            lblTotalUSDoler.Text = dtItems.Rows[0]["Amount"].ToString();
            txtDepVal.Text = dtItems.Rows[0]["DepValue"].ToString();
            ddlOffice.SelectedValue = dtItems.Rows[0]["Office"].ToString();
            ddlOffice_SelectedIndexChanged(null, null);
            ddlEmp.SelectedValue = dtItems.Rows[0]["AssignTo"].ToString();
            txtCreatedOn.Text = dtItems.Rows[0]["AssignOn"].ToString();
            ddlMinCat.SelectedValue = dtItems.Rows[0]["MinCatID"].ToString();

        }
        btnSave.Text = "Update";
    }
    private void BindAttachment()
    {
        string strSQL = "SELECT FILEID,ATTACHMENTTEXT,[FILENAME] FROM DBO.IVM_ITEMS_ATTACHMENTDETAILS WHERE FORMTYPE = 'ITHardware' AND ITEMID = " + ItemID;
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
    private void BindOffices()
    {
        string strOffices = "SELECT OfficeName,OfficeId FROM Office ";
        DataTable dtOffices = Common.Execute_Procedures_Select_ByQueryCMS(strOffices);

        ddlOffice.DataSource = dtOffices;
        ddlOffice.DataTextField = "OfficeName";
        ddlOffice.DataValueField = "OfficeId";
        ddlOffice.DataBind();
        ddlOffice.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void BindEmployees()
    {
        string strEmp = "SELECT EmpId,(FirstName + ' ' + MiddleName + ' ' + FamilyName) AS EmpName FROM  Hr_PersonalDetails WHERE Office = " + ddlOffice.SelectedValue;
        DataTable dtEmp = Common.Execute_Procedures_Select_ByQueryCMS(strEmp);

        ddlEmp.DataSource = dtEmp;
        ddlEmp.DataTextField = "EmpName";
        ddlEmp.DataValueField = "EmpId";
        ddlEmp.DataBind();
        ddlEmp.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    public void BindMinCategory()
    {
        string sql = "SELECT MinCatID,MinCatName FROM  dbo.IVM_MinCategory where MidCatID=" + MidCat + "";
        DataTable dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlMinCat.DataSource = dtItems;
        ddlMinCat.DataTextField = "MinCatName";
        ddlMinCat.DataValueField = "MinCatID";
        ddlMinCat.DataBind();
        ddlMinCat.Items.Insert(0, new ListItem("< None >", "0"));

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
            string sql = "select top 1 exc_rate from DBO.XCHANGEDAILY where RateDate<='" + ((txtPurchaseDt.Text.Trim() == "") ? DateTime.Today.ToShortDateString() : txtPurchaseDt.Text.Trim()) + "' and For_Curr='" + ddlCurrency.SelectedValue.Trim() + "' order by RateDate desc";
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
            string sql = "select top 1 exc_rate from DBO.XCHANGEDAILY where RateDate<='" + ((txtPurchaseDt.Text.Trim() == "") ? DateTime.Today.ToShortDateString() : txtPurchaseDt.Text.Trim()) + "' and For_Curr='" + ddlCurrency.SelectedValue.Trim() + "' order by RateDate desc";
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

            CalculateDepVal();

        }
    }
    public void CalculateDepVal()
    {
        if (txtPurchaseDt.Text.Trim() != "" && ddlCurrency.SelectedIndex != 0 && txtPrice.Text.Trim() != "")
        {
            decimal dep = Convert.ToDecimal(txtPrice.Text.Trim()) / 60;
            //int currMonth = DateTime.Today.Month;
            //int purMonth = Convert.ToDateTime(txtPurchaseDt.Text.Trim()).Month;
            string sql = "SELECT DATEDIFF(mm,'" + txtPurchaseDt.Text.Trim() + "',getdate())As Diff ";
            DataTable dtDiff = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            //TimeSpan monthDiff = DateTime.Today.Subtract(Convert.ToDateTime(txtPurchaseDt.Text.Trim()));
            //int diff =(monthDiff.Days)/30;
            int diff = Common.CastAsInt32(dtDiff.Rows[0]["Diff"].ToString());

            decimal depVal = (dep * diff);

            decimal depvalCal = Convert.ToDecimal(txtPrice.Text.Trim()) - depVal;

            txtDepVal.Text = Math.Round(depvalCal).ToString();
        }

    }

    #region ATTACHMENT

    protected void btnSaveAttachment_Click(object sender, EventArgs e)
    {
        FileUpload img = (FileUpload)flAttachDocs;
        if (ItemID == 0)
        {
            lblMsg.Text = "Please save the item first.";
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
            FileName = "ITHardware_" + ItemID.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);

            string sql = "EXEC DBO.HR_INV_InsertItem_Attachment 'ITHardware'," + ItemID + ", '" + txtAttachmentText.Text.Trim() + "', '" + FileName + "' ";
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
                lblMsg.Text = "Unable to upload document. Error :" + Common.getLastError();
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
}
