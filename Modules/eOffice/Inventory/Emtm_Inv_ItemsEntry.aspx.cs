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

public partial class emtm_Inventory_Emtm_Inv_ItemsEntry : System.Web.UI.Page
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
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ITH", "window.open('Emtm_Inv_ITHardware.aspx','','');", true);
            iframItems.Attributes.Add("src", "Emtm_Inv_ITHardware.aspx?MidCat="+ddlMidCat.SelectedValue+"");
        }
        if (ddlMainCat.SelectedValue.Trim() == "1" && ddlMidCat.SelectedValue.Trim() == "2")
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Soft", "window.open('Emtm_Inv_Software.aspx','','');", true);
            iframItems.Attributes.Add("src", "Emtm_Inv_Software.aspx?MidCat=" + ddlMidCat.SelectedValue + "");
        }
        if (ddlMainCat.SelectedValue.Trim() == "1" && ddlMidCat.SelectedValue.Trim() == "3")
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Amc", "window.open('Emtm_Inv_AMC.aspx','','');", true);
            iframItems.Attributes.Add("src", "Emtm_Inv_AMC.aspx?MidCat=" + ddlMidCat.SelectedValue + "");
            
        }

    }
    

    #endregion -------------------------------------------------
    
}
