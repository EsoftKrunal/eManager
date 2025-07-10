using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class emtm_Inventory_Emtm_Inv_Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        if (!Page.IsPostBack)
        {
            BindMainCategory();
            ddlMainCat_SelectedIndexChanged(sender, e);
        }
    }

    #region --------------- UDF --------------------------------

    public void BindMainCategory()
    {
        string SqlMainCategory = "SELECT MainCatID,MainCatName FROM  dbo.IVM_Category Order By MainCatName";
        DataTable dtMainCat = Common.Execute_Procedures_Select_ByQueryCMS(SqlMainCategory);
        if (dtMainCat.Rows.Count > 0)
        {
            ddlMainCat.DataSource = dtMainCat;
            ddlMainCat.DataTextField = "MainCatName";
            ddlMainCat.DataValueField = "MainCatID";
            ddlMainCat.DataBind();
            ddlMainCat.Items.Insert(0, new ListItem("< Select >", "0"));
        }

    }
    public void BindMidCategory()
    {
        ddlMidCat.Items.Clear();
        string sql = "SELECT MidCatId,MidCatName FROM DBO.IVM_MidCategory WHERE MainCatId = " + ddlMainCat.SelectedValue.Trim() + " Order By  MidCatName";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            ddlMidCat.DataSource = dt;
            ddlMidCat.DataTextField = "MidCatName";
            ddlMidCat.DataValueField = "MidCatId";
            ddlMidCat.DataBind();
        }
        ddlMidCat.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    #endregion -------------------------------------------------

    #region --------------- EVENTS --------------------------------

    #region --------------- Common --------------------------------

    protected void ddlMainCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMidCategory();

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlMainCat.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select main category.";
            ddlMainCat.Focus();
            return;
        }

        if (ddlMidCat.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select mid category.";
            ddlMidCat.Focus();
            return;
        }

        if (ddlMainCat.SelectedValue.Trim() == "1" && ddlMidCat.SelectedValue.Trim() == "1")
        {
            pnlITHardware.Visible = true;
            pnlSoftware.Visible = false;
            pnlAMC.Visible = false;
            btnITHSearch_Click(null, null);
        }
        if (ddlMainCat.SelectedValue.Trim() == "1" && ddlMidCat.SelectedValue.Trim() == "2")
        {
            pnlSoftware.Visible = true;
            pnlITHardware.Visible = false;
            pnlAMC.Visible = false;
            btnSoftSearch_Click(null, null);
        }
        if (ddlMainCat.SelectedValue.Trim() == "1" && ddlMidCat.SelectedValue.Trim() == "3")
        {
            pnlAMC.Visible = true;
            pnlITHardware.Visible = false;
            pnlSoftware.Visible = false;
            btnAMCSearch_Click(null, null);
        }

    }

    #endregion -------------------------------------------------

    #region --------------- IT HARDWARE --------------------------------

    protected void btnITHSearch_Click(object sender, EventArgs e)
    {
        string sql = "SELECT ItemId,AssetCode,Maker,ModelNumber,SRNumber,PurchaseDate,VendorName,WarrantyExp,Price,Currency,Amount,DepValue FROM IVM_ITEMS_IT_Hardware ";
        string whereCond = "WHERE 1=1  ";

        if (txtAssetCode.Text.Trim() != "")
        {
            whereCond = whereCond + "AND AssetCode  LIKE '%" + txtAssetCode.Text.Trim().Replace("'", "`").ToString() + "%' ";
        }
        if (txtMaker.Text.Trim() != "")
        {
            whereCond = whereCond + "AND Maker = '" + txtMaker.Text.Trim() + "' ";
        }
        if (txtModelNo.Text.Trim() != "")
        {
            whereCond = whereCond + "AND ModelNumber = '" + txtModelNo.Text.Trim() + "' ";
        }
        //if (txtPurchaseDt.Text.Trim() != "")
        //{
        //    whereCond = whereCond + "AND PurchaseDate = '" + txtPurchaseDt.Text.Trim() + "' ";
        //}
        
        DataTable dtITItems = Common.Execute_Procedures_Select_ByQueryCMS(sql + whereCond);
        if (dtITItems != null)
        {
            rptITHardware.DataSource = dtITItems;
            rptITHardware.DataBind();
        }

    }

    protected void imgViewITH_OnClick(object sender, EventArgs e)
    {
        int ItemID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "View", "openitemwin(" + ItemID + ");", true);

    }

    #endregion -------------------------------------------------

    #region --------------- SOFTWARE --------------------------------

    protected void btnSoftSearch_Click(object sender, EventArgs e)
    {
        string sql = "SELECT ITEMID,SOFT_DESC,LIC_SRNO,LIC_AUTHNO,LIC_QTY,REPLACE(CONVERT(Varchar(15),LIC_VALIDITY,106),' ','-') AS LIC_VALIDITY,LICENSE_VENDOR,LICENSE_PARTNER,AMOUNT,CURRENCY,AMOUNTUSD FROM IVM_ITEMS_SOFTWARE ";
        string whereCond = "WHERE 1=1  ";

        if (txtSoftDescr.Text.Trim() != "")
        {
            whereCond = whereCond + "AND SOFT_DESC  LIKE '%" + txtSoftDescr.Text.Trim().Replace("'", "`").ToString() + "%' ";
        }
        if (txtSoftAuthNo.Text.Trim() != "")
        {
            whereCond = whereCond + "AND LIC_AUTHNO = '" + txtSoftAuthNo.Text.Trim() + "' ";
        }
        if (txtSoftLicqty.Text.Trim() != "")
        {
            whereCond = whereCond + "AND LIC_QTY = " + txtSoftLicqty.Text.Trim() + " ";
        }

        DataTable dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql + whereCond);
        if (dtItems != null)
        {
            rptSoftware.DataSource = dtItems;
            rptSoftware.DataBind();
        }

    }

    protected void imgViewSoft_Click(object sender, EventArgs e)
    {
        int ItemID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "openSoftwin(" + ItemID + ");", true);

    }

    #endregion -------------------------------------------------

    #region --------------- AMC --------------------------------
    protected void btnAMCSearch_Click(object sender, EventArgs e)
    {
        string sql = "SELECT ITEMID,CONTRACTNO,REPLACE(CONVERT(VARCHAR(15),STARTDATE,106),' ','-') AS STARTDATE ,REPLACE(CONVERT(VARCHAR(15),ENDDATE,106),' ','-') AS ENDDATE,CONTRACT_AMOUNT,CURRENCY,AMOUNT_USD,CONTRACT_VENDOR,SUPPORT_CONTACT_DETAILS FROM IVM_ITEMS_AMC ";
        string whereCond = "WHERE 1=1  ";

        if (txtAMCContNo.Text.Trim() != "")
        {
            whereCond = whereCond + "AND CONTRACTNO  LIKE '%" + txtAMCContNo.Text.Trim().Replace("'", "`").ToString() + "%' ";
        }
        if (txtAMCStDt.Text.Trim() != "")
        {
            whereCond = whereCond + "AND STARTDATE = '" + txtAMCStDt.Text.Trim() + "' ";
        }
        if (txtAMCEndDt.Text.Trim() != "")
        {
            whereCond = whereCond + "AND ENDDATE = '" + txtAMCEndDt.Text.Trim() + "' ";
        }
        
        DataTable dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql + whereCond);
        if (dtItems != null)
        {
            rptAMC.DataSource = dtItems;
            rptAMC.DataBind();
        }

    }

    protected void imgViewAMC_OnClick(object sender, EventArgs e)
    {
        int ItemID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "View", "openAmcwin(" + ItemID + ");", true);

    }

    #endregion -------------------------------------------------

    #endregion -------------------------------------------------

    
}
