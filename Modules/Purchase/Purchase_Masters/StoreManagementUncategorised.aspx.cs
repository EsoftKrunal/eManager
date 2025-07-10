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
using System.Text.RegularExpressions;

public partial class Purchase_Masters_StoreManagementUncategorised : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public int SelTableID
    {
        get { return Common.CastAsInt32(ViewState["_SelTableID"]); }
        set { ViewState["_SelTableID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgMoveItem.Text = "";
        //---------------------------------------
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1062, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
            //SSmenu1.Visible = true;
        }
        else
        {
            //SSmenu1.Visible = false;
        }
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            BindProducts();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindProducts();
    }
        
    private void BindProducts()
    {
        string sql = "";
        string whereClause = "";        
        if (txtItemName_F.Text.Trim() != "")
            whereClause = whereClause + " and ItemName like '%" + txtItemName_F.Text.Trim() + "%'";
        if (txtIMPAS_F.Text.Trim() != "")
            whereClause = whereClause + " and ISSAIMPA like '%" + txtIMPAS_F.Text.Trim() + "%'";
        
        sql = " Select * from MP_VSL_StoreMiscellneousItems where transferredBy is null " + whereClause + " order by ItemName";
        

        DataTable dtP = Common.Execute_Procedures_Select_ByQuery(sql);
        rptProductLL.DataSource = dtP;
        rptProductLL.DataBind();
    }

    // Popup -----------------------------------------------------------------------------
    protected void btnMoveItemPopup_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        BindCategory();
        BindUnits();

        SelTableID = Common.CastAsInt32(btn.CommandArgument);
        Label lblGridItemName = (Label)btn.Parent.FindControl("lblGridItemName");
        Label lblGridISSAIMPA = (Label)btn.Parent.FindControl("lblGridISSAIMPA");

        txtItemName.Text = lblGridItemName.Text;
        txtImpaNo.Text = lblGridISSAIMPA.Text;
        divMoveItem.Visible = true;
    }
    protected void btnClosePopupMoveItem_OnClick(object sender, EventArgs e)
    {
        divMoveItem.Visible = false;
    }
    protected void btnDeleteItem_OnClick(object sender, EventArgs e)
    {
        int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM MP_VSL_StoreMiscellneousItems WHERE TableID=" + _TableId);
        BindProducts();
    }
    protected void btnMoveItem_OnClick(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex == 0)
        {
            lblMsgMoveItem.Text = "Please select category";
            ddlCategory.Focus();
            return;
        }
        if (ddlSubCategory.SelectedIndex == 0)
        {
            lblMsgMoveItem.Text = "Please select sub category";
            ddlSubCategory.Focus();
            return;
        }
        if (txtItemName.Text.Trim() == "")
        {
            lblMsgMoveItem.Text = "Please enter item name";
            txtItemName.Focus();
            return;
        }
        if (ddlProductUnit.SelectedIndex == 0)
        {
            lblMsgMoveItem.Text = "Please select unit";
            ddlProductUnit.Focus();
            return;
        }
        Common.Set_Procedures("[dbo].sp_Move_TempItemToProduct");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
            new MyParameter("@PID", 0),
            new MyParameter("@TableID", SelTableID),
            new MyParameter("@ParentCode", ddlSubCategory.SelectedValue),
            new MyParameter("@PName", txtItemName.Text.Trim()),
            new MyParameter("@Plevel", 3),
            new MyParameter("@PUnit", ddlProductUnit.SelectedValue),
            new MyParameter("@impa", txtImpaNo.Text.Trim()),
            new MyParameter("@Active", 1),
            new MyParameter("@transferredBy", Session["UserName"].ToString())
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res)
        {
            txtItemName.Text = "Record saved successfully.";
            BindProducts();
            ClearFields();
            divMoveItem.Visible = false;
        }
        else
        {
            txtItemName.Text = "Error while saving record.";
        }
    }
    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCategory();
    }
    

    private void BindCategory()
    {
        string sql = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName,isnull(Active,1) as Active from dbo.tblStoreItems where Plevel=1 order by PID ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "CodeName";
        ddlCategory.DataValueField= "PCode";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("Select", ""));
    }
    private void BindSubCategory()
    {
        string sql = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName,isnull(Active,1) as Active from dbo.tblStoreItems where PCode like '" + ddlCategory.SelectedValue + ".%' and Plevel=2 order by PID ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        ddlSubCategory.DataSource = dt;
        ddlSubCategory.DataTextField = "CodeName";
        ddlSubCategory.DataValueField = "PCode";
        ddlSubCategory.DataBind();
        ddlSubCategory.Items.Insert(0, new ListItem("Select", ""));
    }
    private void BindUnits()
    {
        string strSQL = "SELECT UnitId, UnitName FROM [dbo].[tbl_StoreUnitMaster]";
        DataTable dtUOM = Common.Execute_Procedures_Select_ByQuery(strSQL);

        ddlProductUnit.DataSource = dtUOM;
        ddlProductUnit.DataTextField = "UnitName";
        ddlProductUnit.DataValueField = "UnitId";
        ddlProductUnit.DataBind();

        ddlProductUnit.Items.Insert(0, new ListItem("< Select >", "0"));

    }
    private void ClearFields()
    {
        ddlCategory.SelectedIndex = 0;

        ddlSubCategory.Items.Clear();
        txtItemName.Text = "";
        txtImpaNo.Text = "";
        ddlProductUnit.SelectedIndex = 0;
    }
    
}