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

public partial class emtm_Inventory_Emtm_Inv_AMC : System.Web.UI.Page
{
    string SortField;
    //DataTable dtItems;
    string SortExpression = "CONTRACTNO";

    string sql = "SELECT ITEMID,(SELECT MinCatName FROM  dbo.IVM_MinCategory  MC where MC.MinCatID =AMC.MinCatID )MinCatName ,CONTRACTNO,CASE REPLACE(CONVERT(VARCHAR(15),STARTDATE,106),' ','-')  WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(Varchar(15),STARTDATE,106),' ','-') END AS STARTDATE ,CASE REPLACE(CONVERT(VARCHAR(15),ENDDATE,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(Varchar(15),ENDDATE,106),' ','-') END AS ENDDATE,CONTRACT_AMOUNT,CURRENCY,AMOUNT_USD,CONTRACT_VENDOR,SUPPORT_CONTACT_DETAILS,(FirstName + ' ' + MiddleName + ' ' + FamilyName) AS AssignTo,CASE Replace(Convert(Varchar(15),AssignOn,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(Varchar(15),AssignOn,106),' ','-') END As AssignOn FROM IVM_ITEMS_AMC AMC " +
                 "LEFT JOIN Hr_PersonalDetails Emp ON AMC.AssignTo = EMP.EmpId  ";

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
        string str = "DELETE  FROM  IVM_ITEMS_AMC WHERE ITEMID=" + intItemID + " ; select -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt != null)
        {
            lblMsg.Text = "Item deleted successfully.";
            BindItems();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Software", "window.open('Emtm_Inv_AMCEntry.aspx?Mode=Add&MidCat=" + MidCat + "','','');", true);
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

    protected void lbtnContNo_Click(object sender, EventArgs e)
    {
        SortField = "CONTRACTNO";
        SortData(SortField);
    }
    protected void lbtnStartDt_Click(object sender, EventArgs e)
    {
        SortField = "STARTDATE";
        SortData(SortField);
    }
    protected void lbtnEndDt_Click(object sender, EventArgs e)
    {
        SortField = "ENDDATE";
        SortData(SortField);
    }
    protected void lbtnContVendr_Click(object sender, EventArgs e)
    {
        SortField = "CONTRACT_VENDOR";
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

        //string sql = "SELECT ITEMID,CONTRACTNO,REPLACE(CONVERT(VARCHAR(15),STARTDATE,106),' ','-') AS STARTDATE ,REPLACE(CONVERT(VARCHAR(15),ENDDATE,106),' ','-') AS ENDDATE,CONTRACT_AMOUNT,CURRENCY,AMOUNT_USD,CONTRACT_VENDOR,SUPPORT_CONTACT_DETAILS,(FirstName + ' ' + MiddleName + ' ' + FamilyName) AS AssignTo,Replace(Convert(Varchar(15),AssignOn,106),' ','-')As AssignOn FROM IVM_ITEMS_AMC AMC " +
        //             "LEFT JOIN Hr_PersonalDetails Emp ON AMC.AssignTo = EMP.EmpId  ";
        
        string whereCond = "WHERE 1=1 ";

        if (txtContNo.Text.Trim() != "")
        {
            whereCond = whereCond + "AND CONTRACTNO LIKE '%" + txtContNo.Text.Trim() + "%' ";
        }
        if (ddlMinCat.SelectedIndex != 0)
        {
            whereCond = whereCond + "AND MinCatID =" + ddlMinCat.SelectedValue;
        }
        if (txtVendor.Text.Trim() != "")
        {
            whereCond = whereCond + "AND Contract_Vendor LIKE '%" + txtVendor.Text.Trim() + "%' ";
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
        ddlMinCat.DataSource = dtItems;
        ddlMinCat.DataTextField = "MinCatName";
        ddlMinCat.DataValueField = "MinCatID";
        ddlMinCat.DataBind();
        ddlMinCat.Items.Insert(0, new ListItem("< All >", "0"));

    }
}
