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

public partial class Ship_AddEditStock : System.Web.UI.Page
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
           

            if (Request.QueryString["SPID"] != null && Request.QueryString["SPID"].ToString().Trim() != "" && Request.QueryString["CompCode"] != null && Request.QueryString["VC"] != null)
            {

                VesselCode = Request.QueryString["VC"].ToString();
                CompCode = Request.QueryString["CompCode"].ToString();
                SpareId = Request.QueryString["SPID"].ToString();
                hfOffice_Ship.Value = Request.QueryString["OffShip"].ToString();

                GetComponent();
                ShowDetails();
                BindStock();
                BindConsumedStock();
                if (Session["UserType"].ToString() == "O")
                {
                    trStockAdd.Visible = false;
                }
                if (Session["UserType"].ToString() == "S")
                {
                    trStockAdd.Visible = true;
                }


            }
        }
    }


    // Function ------------------------------------------------------------------------
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
            //txtMaker.Text = dtSpareDetails.Rows[0]["Maker"].ToString();
            //txtMakerType.Text = dtSpareDetails.Rows[0]["MakerType"].ToString();
            //txtPart.Text = dtSpareDetails.Rows[0]["PartNo"].ToString();
            ////txtPartName.Text = dtSpareDetails.Rows[0]["PartName"].ToString();
            //txtAltPart.Text = dtSpareDetails.Rows[0]["AltPartNo"].ToString();
            ////txtLocation.Text = dtSpareDetails.Rows[0]["Location"].ToString();
            //txtMinQty.Text = dtSpareDetails.Rows[0]["MinQty"].ToString();
            //txtMaxQty.Text = dtSpareDetails.Rows[0]["MaxQty"].ToString();
            //txtStatutory.Text = dtSpareDetails.Rows[0]["StatutoryQty"].ToString();
            //txtWeight.Text = dtSpareDetails.Rows[0]["Weight"].ToString();
            ////txtStock.Text = dtSpareDetails.Rows[0]["Stock"].ToString();
            //txtDrawingNo.Text = dtSpareDetails.Rows[0]["DrawingNo"].ToString();
            //txtSpecs.Text = dtSpareDetails.Rows[0]["Specification"].ToString();
            //hfOffice_Ship.Value = dtSpareDetails.Rows[0]["Office_Ship"].ToString();
            //txtLocation.Text = dtSpareDetails.Rows[0]["Location"].ToString();

            
            //if (dtSpareDetails.Rows[0]["Attachment"].ToString() != "")
            //{
            //    //Image1.ImageUrl = "~/UploadFiles/UploadSpareDocs/" + dtSpareDetails.Rows[0]["Attachment"].ToString();
            //    ancFile.HRef = "~/UploadFiles/UploadSpareDocs/" + dtSpareDetails.Rows[0]["Attachment"].ToString();
                
            //    ancFile.Visible = true;
            //    Image1.Visible = true;
            //}
            //if (Session["UserType"].ToString() == "O")
            //{
            //    btnActive.Visible = (dtSpareDetails.Rows[0]["Status"].ToString().Trim() == "I");
            //    btnInactive.Visible = (dtSpareDetails.Rows[0]["Status"].ToString().Trim() == "A");

            //    btnSave.Visible = true;
            //    trCritical.Visible = false;

            //}
            //else
            //{
            //    if (dtSpareDetails.Rows[0]["Critical"].ToString() == "C")
            //    {
            //        btnSave.Visible = false;
            //        trCritical.Visible = true;
            //    }
            //    else
            //    {
            //        btnSave.Visible = true;
            //        trCritical.Visible = false;
            //    }
            //}
        }
        else
        {
            //ClearFields();
        }
    }
    private void GetComponent()
    {
        DataTable dtCompId = new DataTable();
        //string strSQL = "SELECT CM.ComponentId,CM.ComponentCode,CM.ComponentName,VSM.PartNo FROM ComponentMaster CM left join VSL_VesselSpareMaster VSM  ON VSM.ComponentId = CM.ComponentId   WHERE ComponentCode = '" + Request.QueryString["CompCode"].ToString().Trim() + "' ";
        string strSQL = "SELECT CM.ComponentId,CM.ComponentCode,CM.ComponentName,VSM.PartNo FROM ComponentMaster CM left join VSL_VesselSpareMaster VSM  ON VSM.ComponentId = CM.ComponentId   WHERE VesselCode='"+VesselCode+ "' and ComponentCode = '" + Request.QueryString["CompCode"].ToString().Trim() + "' and Office_Ship='"+hfOffice_Ship.Value+"'  and SpareId="+SpareId+"";
        dtCompId = Common.Execute_Procedures_Select_ByQuery(strSQL);
        lblComponent.Text = dtCompId.Rows[0]["ComponentCode"].ToString() + " : " + dtCompId.Rows[0]["ComponentName"].ToString();
        lblPartNo.Text = dtCompId.Rows[0]["PartNo"].ToString() ;
        CompId = Common.CastAsInt32(dtCompId.Rows[0]["ComponentId"].ToString());
    }   
    public void BindStock()
    {
        btnAdd.Text = "Add";

        string strSQL = "SELECT REPLACE(CONVERT(VARCHAR(12), RecdDate, 106),' ','-') AS RecdDate,pkid,QtyRecd,StockLocation,SpareId FROM VSL_StockInventory WHERE VesselCode ='" + VesselCode + "' AND ComponentId = " + CompId + " AND Office_Ship = '" + hfOffice_Ship.Value.Trim() + "' AND SpareId = " + SpareId + " And Status='A'";
        DataTable dtStock = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtStock.Rows.Count > 0)
        {
            rptStock.DataSource = dtStock;
            rptStock.DataBind();
        }
        else
        {
            rptStock.DataSource = null;
            rptStock.DataBind();
        }
        BindInventoryStatus();
        
        
    }
    private void BindInventoryStatus()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC dbo.sp_getInventorySummary '" + VesselCode + "'," + CompId + ",'" + hfOffice_Ship.Value.Trim() + "'," + SpareId + ",'" + DateTime.Today.ToString("dd-MMM-yyyy") + "'");
        lblTQtyRecd.Text=dt.Rows[0][0].ToString();
        lblTQtyCons.Text = dt.Rows[0][1].ToString();
        lblTROB.Text = dt.Rows[0][2].ToString();
        if (Common.CastAsInt32(lblTROB.Text) < 0)
            lblTROB.Text = "0";
    }
    


    // Events ------------------------------------------------------------------------
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (SpareId.Trim() == "")
        {
            MessageBox2.ShowMessage("Please add spare first.", true);            
            return;
        }
        if (txtRecdDt.Text.Trim() == "")
        {
            MessageBox2.ShowMessage("Please enter Recd date.", true);
            txtRecdDt.Focus();
            return;
        }
        DateTime dt;
        if (!DateTime.TryParse(txtRecdDt.Text.Trim(), out dt))
        {
            MessageBox2.ShowMessage("Please enter valid date.", true);
            txtRecdDt.Focus();
            return;
        }
        if (txtRecdQty.Text.Trim() == "")
        {
            MessageBox2.ShowMessage("Please enter Recd qty.", true);
            txtRecdQty.Focus();
            return;
        }
        if (txtStockLocation.Text.Trim() == "")
        {
            MessageBox2.ShowMessage("Please enter stock location.", true);
            txtStockLocation.Focus();
            return;
        }

        Common.Set_Procedures("sp_InsertSparesInventory");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", VesselCode.Trim()),
            new MyParameter("@ComponentId", CompId),
            new MyParameter("@Office_Ship", hfOffice_Ship.Value.Trim()),
            new MyParameter("@SpareId", SpareId.Trim()),
            new MyParameter("@PkId", pKId),
            new MyParameter("@RecdDate", txtRecdDt.Text.Trim()),
            new MyParameter("@QtyRecd", txtRecdQty.Text.Trim()),
            new MyParameter("@StockLocation", txtStockLocation.Text.Trim())
            
            );

        DataSet dsStock = new DataSet();
        dsStock.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsStock);
        if (res)
        {
            MessageBox2.ShowMessage("Stock updated successfully.", false);
            BindStock();
            Clear();
            btnAdd.Text = "Add";
        }
        else
        {
            MessageBox2.ShowMessage("Unable to Add/Update Stock.Error :" + Common.getLastError(), true);
        }
    }
    protected void btnEditStock_Click(object sender, ImageClickEventArgs e)
    {
        //int spareId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());
        pKId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());
        string strSQL = "SELECT REPLACE(CONVERT(VARCHAR(12), RecdDate, 106),' ','-') AS RecdDate,QtyRecd,StockLocation,PKID FROM VSL_StockInventory WHERE VesselCode ='" + VesselCode + "' AND ComponentId = " + CompId + " AND Office_Ship = '" + hfOffice_Ship.Value.Trim() + "' AND SpareId = " + SpareId + " and PKID=" + pKId;
        DataTable dtStock = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtStock.Rows.Count > 0)
        {
            txtRecdDt.Text = dtStock.Rows[0]["RecdDate"].ToString();
            txtRecdQty.Text = dtStock.Rows[0]["QtyRecd"].ToString();
            txtStockLocation.Text = dtStock.Rows[0]["StockLocation"].ToString();
            //pKId = Common.CastAsInt32(dtStock.Rows[0]["PKID"].ToString());

            btnAdd.Text = "Update";

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    public void Clear()
    {
        txtRecdDt.Text = "";
        txtRecdQty.Text = "";
        txtStockLocation.Text = "";
        pKId = 0;
    }

    // Consumed Stock Section
    public void BindConsumedStock()
    {
        string strSQL = " Select DoneDate,Qty,ConsumedIn_Type_Id,ConsumedIn_Type_Name,ReferenceID,ReferenceText,HistoryId,UPId,JobDetails " +
                        " from dbo.tfn_GetSpareCosumedDetails('"+VesselCode+"',"+ CompId + ",'"+ hfOffice_Ship .Value+ "',"+ SpareId + ") ";
        DataTable dtStock = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtStock.Rows.Count > 0)
        {
            rptItemConsumed.DataSource = dtStock;
            rptItemConsumed.DataBind();
        }
        else
        {
            rptItemConsumed.DataSource = null;
            rptItemConsumed.DataBind();
        }
    }

}
