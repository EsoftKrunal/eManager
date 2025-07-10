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

public partial class Purchase_Masters_StoreManagement : System.Web.UI.Page
{
    AuthenticationManager Auth;

    public int SelDepth
    {
        get
        { return Common.CastAsInt32(ViewState["_SelDepth"]); }
        set
        { ViewState["_SelDepth"] = value; }

    }
    public int SelCatID
    {
        get
        { return Common.CastAsInt32(ViewState["_SelCatID"]); }
        set
        { ViewState["_SelCatID"] = value; }
    }
    public string SelCatCode
    {
        get
        { return Convert.ToString(ViewState["_SelCatCode"]); }
        set
        { ViewState["_SelCatCode"] = value; }
    }
    public int SelSubCatID
    {
        get
        { return Common.CastAsInt32(ViewState["_SelSubCatID"]); }
        set
        { ViewState["_SelSubCatID"] = value; }
    }
    public string SelSubCatCode
    {
        get
        { return Convert.ToString(ViewState["_SelSubCatCode"]); }
        set
        { ViewState["_SelSubCatCode"] = value; }
    }

    public int AddingCatID
    {
        get
        { return Common.CastAsInt32(ViewState["_AddingCatID"]); }
        set
        { ViewState["_AddingCatID"] = value; }
    }
    public int AddingSubCatID
    {
        get
        { return Common.CastAsInt32(ViewState["_AddingSubCatID"]); }
        set
        { ViewState["_AddingSubCatID"] = value; }
    }
    
    public int ProductID
    {
        get
        { return Common.CastAsInt32(ViewState["_ProductID"]); }
        set
        { ViewState["_ProductID"] = value; }

    }
    public string Location
    {
        get
        { return Convert.ToString(ViewState["_Location"]); }
        set
        { ViewState["_Location"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgProduct.Text = "";        
        lblMSgProductLL.Text = "";
        //---------------------------------------
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1062, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
            Location = "O";
          //  SSmenu1.Visible = true;
        }
        else
        {
            Location = "S";
          //  SSmenu1.Visible = false;
        }
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            btnAddProductLLPopup.Visible = (Location == "O");
            btnAddProduct.Visible = (Location == "O");
            BindCategories();
            BindProducts();
            BindCageroryAdd();
            BindUnits();
            BindVessels();
        }
    }
    protected void BindVessels()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSEL WHERE VESSELSTATUSID=1 AND ISNULL(FLEETID,0)>0  ORDER BY VESSELNAME");
        chkvessels.DataSource = dt;
        chkvessels.DataTextField = "VesselName";
        chkvessels.DataValueField = "VesselCode";
        chkvessels.DataBind();
    }

    //Add Product-------------------------------------------   
    protected void btnAddProduct_OnClick(object sender, EventArgs e)
    {
        lblHeadingPopup.Text = "Add Category";
        AddingCatID = 0;
        AddingSubCatID = 0;
        ClearProduct();
        if (SelCatID > 0)
        {
            rdoCatSubCate.SelectedIndex = 1;
            trSelectCategory.Visible = true;
            ddlCategoryAdd.SelectedValue = SelCatCode;
        }
        divAddProduct.Visible = true;
    }
    protected void btnClsoseProductPopup_OnClick(object sender, EventArgs e)
    {
        divAddProduct.Visible = false;
    }
    protected void btnSaveProduct_OnClick(object sender, EventArgs e)
    {
        //-----------------------------
        if (rdoCatSubCate.SelectedIndex == 1)
        {
            if (ddlCategoryAdd.SelectedIndex == 0)
            {
                lblMsgProduct.Text = "Please select category";
                txtProductName.Focus();
                return;
            }
        }
        if (txtProductName.Text.Trim() == "")
        {
            lblMsgProduct.Text = "Please enter category name";
            txtProductName.Focus();
            return;
        }
        string vlist = "";
        foreach (ListItem li in chkvessels.Items)
        {
            if (li.Selected)
                vlist +=","+li.Value;
        }
        if (vlist.StartsWith(","))
            vlist = vlist.Substring(1);
        //-----------------------------
        int IdToEdit = (SelDepth == 0) ? AddingCatID : AddingSubCatID;
        Common.Set_Procedures("[dbo].sp_IU_AddSubCategory");
        Common.Set_ParameterLength(6);
        Common.Set_Parameters(
            new MyParameter("@PID", IdToEdit),
            new MyParameter("@ParentCode", ddlCategoryAdd.SelectedValue),
            new MyParameter("@PName", txtProductName.Text.Trim()),            
            new MyParameter("@Active", ddlProductStatus.SelectedValue),
            new MyParameter("@Vessels", vlist),            
            new MyParameter("@ModifiedBy", Session["UserName"].ToString())
            );        
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res)
        {
            lblMsgProduct.Text = "Record saved successfully.";
            BindCategories();          
            if(IdToEdit==0)
                ClearProduct();
        }
        else
        {
            lblMsgProduct.Text = "Error while saving record." + Common.ErrMsg;
        }
    }
    protected void btnEditProduct_OnClick(object sender, EventArgs e)
    {
        lblHeadingPopup.Text = "Edit Category";
        ShowProductDataEditing();
        rdoCatSubCate.Enabled = false;
        ddlCategoryAdd.Enabled = false;
        if (SelDepth == 0)
        {
            rdoCatSubCate.SelectedIndex = 0;            
            trSelectCategory.Visible = false;
        }
        else
        {
            rdoCatSubCate.SelectedIndex = 1;
            trSelectCategory.Visible = true;
            ddlCategoryAdd.SelectedValue = SelCatCode;
        }
        divAddProduct.Visible = true;
    }    
    public void ClearProduct()
    {
        //rdoCatSubCate.SelectedIndex = 0;
        //trSelectCategory.Visible = false;
        //ddlCategoryAdd.SelectedIndex = 0;
        txtProductName.Text = "";
        ddlProductStatus.SelectedIndex = 0;
        rdoCatSubCate.Enabled = true;
        ddlCategoryAdd.Enabled = true;
        chkvessels.ClearSelection(); 
    }
    public void ShowProductDataEditing()
    {
        string sql = " ";
        if (SelDepth==0)
            sql = " select * from dbo.tblStoreItems where PID="+SelCatID+""; //?????Prob
        else
            sql = " select * from dbo.tblStoreItems where PID=" + SelSubCatID + ""; //?????Prob
        chkvessels.ClearSelection();

        DataTable dtP = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtP.Rows.Count > 0)
        {
            txtProductName.Text = dtP.Rows[0]["PName"].ToString();
            ddlProductStatus.SelectedValue= ((dtP.Rows[0]["Active"].ToString()=="True")?"1":"0");
            if (SelDepth == 1)
            {
                SelCatCode = dtP.Rows[0]["PCode"].ToString().Split('.')[0];
                DataTable dtI = Common.Execute_Procedures_Select_ByQuery("select * from DBO.tblStoreItemsVA where pid=" + SelSubCatID + " and status='I'");
                foreach(DataRow dr in dtI.Rows)
                {
                    chkvessels.Items.FindByValue(dr["VesselCode"].ToString()).Selected = true;
                }                
            }
        }
    }

    //Add Product Last level-------------------------------------------    
    protected void btnAddProductLLPopup_OnClick(object sender, EventArgs e)
    {
        divAddProductLL.Visible = true;
        btnEditProductLL.Visible = false;
        hfdProductIdLL.Value = "";
        hfdProductCodeLL.Value = "";
        ShowParentSubCategory();

    }
    protected void btnTempProductLL_OnClick(object sender, EventArgs e)
    {
        btnEditProductLL.Visible = true && Location == "O";
        BindProducts();        
    }
   
    protected void btnCloseProductLLPopup_OnClick(object sender, EventArgs e)
    {
        divAddProductLL.Visible = false;
    }
    protected void btnSaveProductLL_OnClick(object sender, EventArgs e)
    {
        if (txtProductNameLL.Text.Trim() == "")
        {
            lblMSgProductLL.Text = "Please enter product name";
            txtProductNameLL.Focus();
            return;
        }
        if (ddlProductUnit.SelectedIndex== 0)
        {
            lblMSgProductLL.Text = "Please select unit";
            ddlProductUnit.Focus();
            return;
        }
        Common.Set_Procedures("[dbo].sp_IU_AddProduct");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters(
            new MyParameter("@PID", Common.CastAsInt32(hfdProductIdLL.Value)),
            new MyParameter("@ParentCode",SelSubCatCode), //????? prob
            new MyParameter("@PName", txtProductNameLL.Text.Trim()),
            new MyParameter("@Plevel", 3),
            new MyParameter("@PUnit", ddlProductUnit.SelectedValue),
            new MyParameter("@impa", txtimpa.Text.Trim()),            
            new MyParameter("@Active", ddlProductStatusLL.SelectedValue),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString())
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res)
        {
            lblMSgProductLL.Text = "Record saved successfully.";
            BindProducts();
            ClearProductLL();
        }
        else
        {
            lblMSgProductLL.Text = "Error while saving record.";
        }
    }    
    protected void btnEditProductLL_OnClick(object sender, EventArgs e)
    {
        ShowProductLLDataEditing();
        divAddProductLL.Visible = true;        
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        BindProducts();
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
    public void ShowProductLLDataEditing()
    {
        ShowParentSubCategory();
        string sql = " select * from dbo.tblStoreItems where PID=" + Common.CastAsInt32(hfdProductIdLL.Value) + " ";
        DataTable dtP = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtP.Rows.Count > 0)
        {
            txtProductNameLL.Text = dtP.Rows[0]["PName"].ToString();
            ddlProductUnit.SelectedValue= dtP.Rows[0]["Punit"].ToString();
            ddlProductStatusLL.SelectedValue = ((dtP.Rows[0]["Active"].ToString() == "True") ? "1" : "0");
            txtimpa.Text = dtP.Rows[0]["impano"].ToString();
        }
    }    
    public void ClearProductLL()
    {
        txtProductNameLL.Text = "";
        ddlProductStatusLL.SelectedIndex = 0;
        ddlProductUnit.SelectedIndex = 0;
        txtimpa.Text = "";
    }
    public void ShowParentSubCategory()
    {
        string sql = " select PCode+' : '+Pname from dbo.tblStoreItems where PID=" + SelSubCatID + " ";
        DataTable dtP = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtP.Rows.Count > 0)
        {
            lblParentSubCategory.Text = dtP.Rows[0][0].ToString();
        }

    }

    //Tree View-----------------------------------------------
    protected void tvCategories_SelectedNodeChanged(object sender, EventArgs e)
    {

        string[] CatCode = tvCategories.SelectedNode.Text.Trim().Split(':');
        SelDepth = tvCategories.SelectedNode.Depth;
        btnEditProduct.Visible = true && Location == "O";        
        if (SelDepth == 0)
        {
            AddingCatID= SelCatID = Common.CastAsInt32(tvCategories.SelectedNode.Value);
            SelCatCode = CatCode[0].Trim();
            btnAddProductLLPopup.Visible = false;
        }
        else
        {   
            AddingSubCatID = SelSubCatID = Common.CastAsInt32(tvCategories.SelectedNode.Value);
            SelSubCatCode = CatCode[0].Trim();
            btnAddProductLLPopup.Visible = true && Location == "O";
        }
        BindProducts();
    }
    protected void tvCategories_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {

        DataTable dtProducts;
        string strProducts = "SELECT CategoryId,CategoryName,Inactive FROM [dbo].[MP_MainCategory] WHERE ParentCategoryId =" + e.Node.Value.Trim() + " AND Level = 'P' Order By CategoryName";
        string sqlCategory = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName from dbo.tblStoreItems where Active=1 and Plevel=1 order by PID ";

        dtProducts = Common.Execute_Procedures_Select_ByQuery(sqlCategory);
        if (dtProducts != null)
        {
            for (int k = 0; k < dtProducts.Rows.Count; k++)
            {
                TreeNode ssn = new TreeNode();
                ssn.Text = dtProducts.Rows[k]["CodeName"].ToString();
                ssn.Value = dtProducts.Rows[k]["PID"].ToString();
                ssn.ToolTip = dtProducts.Rows[k]["CodeName"].ToString();
                ssn.Expanded = false;
                e.Node.ChildNodes.Add(ssn);
            }
        }
    }
    protected void rdoCatSubCate_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoCatSubCate.SelectedIndex == 0)
            trSelectCategory.Visible = false;
        else
            trSelectCategory.Visible = true;
    }
    

    private void BindCategories()
    {
        DataTable dtCategory = new DataTable();
        string sqlCategory = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName,Active from dbo.tblStoreItems where Plevel=1 order by PID ";

        dtCategory = Common.Execute_Procedures_Select_ByQuery(sqlCategory);
        tvCategories.Nodes.Clear();

        if (dtCategory.Rows.Count > 0)
        {
            for (int i = 0; i < dtCategory.Rows.Count; i++)
            {
                TreeNode gn = new TreeNode();
                gn.Text = dtCategory.Rows[i]["CodeName"].ToString();
                gn.Value = dtCategory.Rows[i]["PID"].ToString();
                gn.ToolTip = dtCategory.Rows[i]["CodeName"].ToString();
                gn.Expanded = false;
                DataTable dtSubCategory;
                string sql = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName,Active from dbo.tblStoreItems where PCode like '" + dtCategory.Rows[i]["PCode"].ToString() + ".%' and Plevel=2 order by PID ";
                dtSubCategory = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dtSubCategory != null)
                {
                    for (int j = 0; j < dtSubCategory.Rows.Count; j++)
                    {
                        TreeNode sn = new TreeNode();
                        sn.Text = dtSubCategory.Rows[j]["CodeName"].ToString();
                        sn.Value = dtSubCategory.Rows[j]["PID"].ToString();
                        sn.ToolTip = dtSubCategory.Rows[j]["CodeName"].ToString();
                        sn.Expanded = false;
                        if (dtSubCategory.Rows[j]["Active"].ToString() == "False")
                        {
                            sn.Text += "<i style='color:#d81818;font-size:10px;'>(&nbsp;Inactive)<i>";
                        }
                        gn.ChildNodes.Add(sn);
                    }
                }
                if(dtCategory.Rows[i]["Active"].ToString()=="False")
                {
                    gn.Text += "<i style='color:#d81818;font-size:10px;'>(&nbsp;Inactive)<i>";
                }
                tvCategories.Nodes.Add(gn);
            }
        }
    }
    private void BindProducts()
    {
        string sql = "";
        string sSelCode = (SelDepth == 0) ? SelCatCode : SelSubCatCode;
        string whereClause = "";
        if (txtProductCodeS.Text.Trim() != "")
            whereClause = whereClause + " and PCode like '%" + txtProductCodeS.Text.Trim() + "%'";
        if (txtProductNameS.Text.Trim() != "")
            whereClause = whereClause + " and PName like '%" + txtProductNameS.Text.Trim() + "%'";
        if (txtIMPAS.Text.Trim() != "")
            whereClause = whereClause + " and impano like '%" + txtIMPAS.Text.Trim() + "%'";
        if (ddlStatus.SelectedIndex>0)
            whereClause = whereClause + " and isnull(active,0)=" + ddlStatus.SelectedValue + "";
        
        if (sSelCode == "")
            sql = " select *,um.UnitName,StatusName=case when isnull(Active,0)=1 then 'Active' else 'Inactive' end from dbo.tblStoreItems si left join [dbo].[tbl_StoreUnitMaster] um on um.UnitId=si.Punit where Active=1 and Plevel=3 " + whereClause + " order by PCode";
        else
            sql = " select *,um.UnitName,StatusName=case when isnull(Active,0)=1 then 'Active' else 'Inactive' end from dbo.tblStoreItems si left join [dbo].[tbl_StoreUnitMaster] um on um.UnitId=si.Punit where PCode like '" + sSelCode + ".%' and Active=1 and Plevel=3  " + whereClause + "  order by PCode";

        DataTable dtP = Common.Execute_Procedures_Select_ByQuery(sql);
        rptProductLL.DataSource = dtP;
        rptProductLL.DataBind();
    }
    private void BindCageroryAdd()
    {
        string sqlCategory = " select PID,PCode,Pname,PCode+' : '+Pname as CodeName from dbo.tblStoreItems where Active=1 and Plevel=1 order by PID ";
        DataTable dtUOM = Common.Execute_Procedures_Select_ByQuery(sqlCategory);

        ddlCategoryAdd.DataSource = dtUOM;
        ddlCategoryAdd.DataTextField = "CodeName";
        ddlCategoryAdd.DataValueField = "PCode";
        ddlCategoryAdd.DataBind();
        ddlCategoryAdd.Items.Insert(0, new ListItem("< Select >", ""));

    }
}