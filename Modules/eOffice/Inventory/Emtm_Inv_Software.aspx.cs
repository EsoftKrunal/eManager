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

public partial class emtm_Inventory_Emtm_Inv_Software : System.Web.UI.Page
{
    string SortField;
    DataTable dtItems;
    string SortExpression = "LIC_SRNO";
    string sql = "SELECT ITEMID,SOFT_DESC,(SELECT MinCatName FROM  dbo.IVM_MinCategory  MC where MC.MinCatID =SFT.MinCatID )MinCatName ,License_Vendor,LIC_SRNO,LIC_AUTHNO,LIC_QTY,CASE REPLACE(CONVERT(Varchar(15),LIC_VALIDITY,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(Varchar(15),LIC_VALIDITY,106),' ','-') END AS LIC_VALIDITY,LICENSE_VENDOR,LICENSE_PARTNER,AMOUNT,CURRENCY,AMOUNTUSD,(FirstName + ' ' + MiddleName + ' ' + FamilyName) AS AssignTo,CASE Replace(Convert(Varchar(15),AssignOn,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE Replace(Convert(Varchar(15),AssignOn,106),' ','-') END As AssignOn FROM IVM_ITEMS_SOFTWARE SFT " +
                     "LEFT JOIN Hr_PersonalDetails Emp ON SFT.AssignTo = EMP.EmpId ";

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
            BindItems();
            MidCat = Common.CastAsInt32(Request.QueryString["MidCat"]);
            BindMinCategory();
        }
    }

    public void BindItems()
    {
        //string sql = "SELECT ITEMID,SOFT_DESC,LIC_SRNO,LIC_AUTHNO,LIC_QTY,REPLACE(CONVERT(Varchar(15),LIC_VALIDITY,106),' ','-') AS LIC_VALIDITY,LICENSE_VENDOR,LICENSE_PARTNER,AMOUNT,CURRENCY,AMOUNTUSD,(FirstName + ' ' + MiddleName + ' ' + FamilyName) AS AssignTo,Replace(Convert(Varchar(15),AssignOn,106),' ','-')As AssignOn FROM IVM_ITEMS_SOFTWARE SFT " +
        //             "LEFT JOIN Hr_PersonalDetails Emp ON SFT.AssignTo = EMP.EmpId ";
        
        dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql);
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
        string str = "DELETE  FROM  IVM_ITEMS_SOFTWARE WHERE ITEMID=" + intItemID + " ; select -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt != null)
        {
            lblMsg.Text = "Item deleted successfully.";
            BindItems();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Software", "window.open('Emtm_Inv_SoftwareEntry.aspx?Mode=Add&MidCat=" + MidCat + "','','');", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindItems();
    }

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

    protected void lbtnSrNo_Click(object sender, EventArgs e)
    {
        SortField = "LIC_SRNO";
        SortData(SortField);
    }

    protected void lblAuthNo_Click(object sender, EventArgs e)
    {
        SortField = "LIC_AUTHNO";
        SortData(SortField);
    }   

    protected void lblLicenceQty_Click(object sender, EventArgs e)
    {
        SortField = "LIC_QTY";
        SortData(SortField);
    }
    protected void lblLicenceValidity_Click(object sender, EventArgs e)
    {
        SortField = "LIC_VALIDITY";
        SortData(SortField);
    }
    protected void lblAssignTo_Click(object sender, EventArgs e)
    {
        SortField = "AssignTo";
        SortData(SortField);
    }
    protected void lblAssignOn_Click(object sender, EventArgs e)
    {
        SortField = "AssignOn";
        SortData(SortField);
    }
    

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        //string sql = "SELECT ITEMID,SOFT_DESC,LIC_SRNO,LIC_AUTHNO,LIC_QTY,REPLACE(CONVERT(Varchar(15),LIC_VALIDITY,106),' ','-') AS LIC_VALIDITY,LICENSE_VENDOR,LICENSE_PARTNER,AMOUNT,CURRENCY,AMOUNTUSD,(FirstName + ' ' + MiddleName + ' ' + FamilyName) AS AssignTo,Replace(Convert(Varchar(15),AssignOn,106),' ','-')As AssignOn FROM IVM_ITEMS_SOFTWARE SFT " +
        //             "LEFT JOIN Hr_PersonalDetails Emp ON SFT.AssignTo = EMP.EmpId ";
        string whereCond = "WHERE 1=1 ";
        
        if (txtSrNo.Text.Trim() != "")
        {
            whereCond = whereCond + "AND LIC_SRNO LIKE '%" + txtSrNo.Text.Trim() + "%' ";
        }
        if (ddlMinCatID.SelectedIndex != 0)
        {
            whereCond = whereCond + "AND MinCatID  LIKE '" + ddlMinCatID.SelectedItem + "' ";
        }
        if (txtVendorName.Text.Trim() != "")
        {
            whereCond = whereCond + "AND License_Vendor LIKE '%" + txtVendorName.Text.Trim() + "%' ";
        }
        if (txtVendorPartner.Text.Trim() != "")
        {
            whereCond = whereCond + "AND License_Partner =" + txtVendorPartner.Text.Trim() + " ";
        }
        if (txtLicVal.Text.Trim() != "")
        {
            whereCond = whereCond + "AND LIC_VALIDITY <= '" + txtLicVal.Text.Trim() + "' ";
        }
        if (ViewState["SortOrder"] != null)
        {
            sql = sql + whereCond + " ORDER BY " + SortExpression.ToString() + " " + ViewState["SortOrder"];
        }
        else
        {
            sql = sql + whereCond;
        }
        dtItems = Common.Execute_Procedures_Select_ByQueryCMS(sql);
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
