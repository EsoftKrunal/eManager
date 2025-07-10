using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class emtm_Inventory_Default : System.Web.UI.Page
{
    string SortField;
    //DataTable dtItems;
    string SortExpression = "AssetCode";

    string sql = "SELECT ItemId,VendorName,(SELECT MinCatName FROM  dbo.IVM_MinCategory  MC where MC.MinCatID =IT.MinCatID )MinCatName ,replace(convert(varchar,WarrantyExp,106),' ','-')WarrantyExp ,AssetCode,Maker,ModelNumber,SRNumber,CASE Replace(Convert(Varchar(15),PurchaseDate,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(Varchar(15),PurchaseDate,106),' ','-') END As PurchaseDate,VendorName,WarrantyExp,Price,Currency,Amount,DepValue,(FirstName + ' ' + MiddleName + ' ' + FamilyName) AS AssignTo,CASE Replace(Convert(Varchar(15),AssignOn,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(Varchar(15),AssignOn,106),' ','-') END As AssignOn FROM IVM_ITEMS_IT_Hardware IT " +
                 "LEFT JOIN Hr_PersonalDetails Emp ON IT.AssignTo = EMP.EmpId ";
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
            MidCat = Common.CastAsInt32(Request.QueryString["MidCat"]);
            BindItems();
            BindMinCategory();
        }
    }

    public void BindItems()
    {
        
        DataTable dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtItems.Rows.Count > 0)
        {
            rptITHardware.DataSource = dtItems;
            rptITHardware.DataBind();
        }
    }
    
    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        int ItemID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "openitemwin(" + ItemID + "," + MidCat + ");", true);

    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        int intItemID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string str = "delete  FROM  IVM_ITEMS_IT_Hardware WHERE ItemId=" + intItemID + " ; select -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt != null)
        {
            lblMsg.Text = "Item deleted successfully.";
            BindItems();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ITH", "window.open('Emtm_Inv_ITHardwareEntry.aspx?Mode=Add&MidCat=" + MidCat + "','','');", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindItems();
    }

    // **************** Sorting *********************
    void SortData(string SortExp)
    {
        if (ViewState["SortOrder"] == null)
        {
            ViewState["SortOrder"] = " ASC";
        }
        else if (ViewState["SortOrder"].ToString() == " ASC")
        {
            ViewState["SortOrder"] = " DESC";
        }
        else
        {
            ViewState["SortOrder"] = " ASC";
        }
        SortExpression = SortExp;
        btnSearch_Click(null, null);

    }

    protected void lbtnAssetCode_Click(object sender, EventArgs e)
    {
        SortField = "AssetCode";
        SortData(SortField);
    }
    protected void lbtnSrNo_Click(object sender, EventArgs e)
    {
        SortField = "SRNumber";
        SortData(SortField);
    }
    protected void lbtnModelNo_Click(object sender, EventArgs e)
    {
        SortField = "ModelNumber";
        SortData(SortField);
    }
    protected void lbtnPurchaseDt_Click(object sender, EventArgs e)
    {
        SortField = "PurchaseDate";
        SortData(SortField);
    }
    protected void lbtnAssignTo_Click(object sender, EventArgs e)
    {
        SortField = "AssignTo";
        SortData(SortField);
    }
    protected void lbtnAssignOn_Click(object sender, EventArgs e)
    {
        SortField = "AssignOn";
        SortData(SortField);
    }

    // ************************************************

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        //string sql = "SELECT ItemId,AssetCode,Maker,ModelNumber,SRNumber,Replace(Convert(Varchar(15),PurchaseDate,106),' ','-')As PurchaseDate,VendorName,WarrantyExp,Price,Currency,Amount,DepValue,(FirstName + ' ' + MiddleName + ' ' + FamilyName) AS AssignTo,Replace(Convert(Varchar(15),AssignOn,106),' ','-')As AssignOn FROM IVM_ITEMS_IT_Hardware IT " +
        //             "LEFT JOIN Hr_PersonalDetails Emp ON IT.AssignTo = EMP.EmpId ";
        
        string whereCond = "WHERE 1=1 ";

        if (txtAssetCode.Text.Trim() != "")
        {
            whereCond = whereCond +  "AND AssetCode LIKE '%" + txtAssetCode.Text.Trim().Replace("'","") + "%' ";
        }
        if (ddlMinCatID.SelectedIndex != 0)
        {
            whereCond = whereCond + "AND MinCatID LIKE " + ddlMinCatID.SelectedValue + " ";
        }
        if (txtSrNo.Text.Trim() != "")
        {
            whereCond = whereCond + "AND SRNumber LIKE '%" + txtSrNo.Text.Trim().Replace("'", "") + "%' ";
        }
        if (txtVendorName.Text.Trim() != "")
        {
            whereCond = whereCond + "AND VendorName LIKE '%" + txtVendorName.Text.Trim().Replace("'", "") + "%' ";
        }
        if (txtMakerName.Text.Trim() != "")
        {
            whereCond = whereCond + "AND Maker LIKE '%" + txtMakerName.Text.Trim().Replace("'", "") + "%' ";
        }

        if (txtWarrantyExpiry.Text.Trim() != "")
        {
            whereCond = whereCond + "AND WarrantyExp <='" + txtWarrantyExpiry.Text.Trim().Replace("'", "") + "' ";
        }

        if (ViewState["SortOrder"] != null)
        {
            sql = sql + whereCond + " ORDER BY " + SortExpression.ToString() + " " + ViewState["SortOrder"];
        }
        else
        {
            sql = sql + whereCond;
        }

        
        DataTable dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtItems != null)
        {
            rptITHardware.DataSource = dtItems;
            rptITHardware.DataBind();
        }

    }

    public void BindMinCategory()
    {
        string sql = "SELECT MinCatID,MinCatName FROM  dbo.IVM_MinCategory where MidCatID=" + MidCat + "";
        DataTable dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlMinCatID.DataSource = dtItems;
        ddlMinCatID.DataTextField = "MinCatName";
        ddlMinCatID.DataValueField = "MinCatID";
        ddlMinCatID.DataBind();
        ddlMinCatID.Items.Insert(0, new ListItem("< All >", "0"));

    }
}
