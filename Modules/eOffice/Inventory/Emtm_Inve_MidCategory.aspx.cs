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

public partial class Emtm_Inve_MidCategory : System.Web.UI.Page
{
    public int CatID
    {
        set
        {
            ViewState["CatID"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["CatID"]);
        }
    }
    public int MidCatID
    {
        set
        {
            ViewState["MidCatID"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["MidCatID"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            BindMainCategory();
            BindMidCategory();
        }
    }


    //Function ------------------------------------------------------------------------------
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
            ddlMainCat.Items.Insert(0, new ListItem("< Select Main Category >", "0"));
        }

    }
    public void BindMidCategory()
    {
        string sql = "SELECT MidCatId,MIC.MainCatId,MainCatName,MidCatName FROM DBOIVM_MidCategory MIC " +
                     "INNER JOIN DBO.IVM_Category IC ON IC.MainCatId = MIC.MainCatId Order By MidCatName";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt != null)
        {
            rptMidCategory.DataSource = dt;
            rptMidCategory.DataBind();
        }
    }

    //Events ------------------------------------------------------------------------------
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (ddlMainCat.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select main category.";
                ddlMainCat.Focus(); return;
            }

            if (txtMidCategory.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter mid category name.";
                txtMidCategory.Focus(); return;
            }

            if (CatID != Common.CastAsInt32(ddlMainCat.SelectedValue.Trim()) || MidCatID == 0)
            {

                string strCheckDuplicate = "SELECT * FROM  dbo.IVM_MidCategory Where MainCatId = " + ddlMainCat.SelectedValue.Trim() + " AND  MidCatName = '" + txtMidCategory.Text.Trim() + "' ";
                DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(strCheckDuplicate);
                if (dtDuplicate.Rows.Count > 0)
                {
                    lblMsg.Text = "Mid Category already exists.";
                    txtMidCategory.Focus();
                    return;
                }
            }

            string sql = "";
            if (MidCatID == 0)
                sql = "INSERT INTO dbo.IVM_MidCategory values(" + ddlMainCat.SelectedValue.Trim() + ", '" + txtMidCategory.Text.Trim().Replace("'", "`") + "' ) ;select -1";
            else
                sql = "UPDATE dbo.IVM_MidCategory SET MainCatId = " + ddlMainCat.SelectedValue.Trim() + ",  MidCatName='" + txtMidCategory.Text.Trim().Replace("'", "`") + "' WHERE MidCatId= " + MidCatID + "  ;select -1";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt != null)
            {
                if (MidCatID == 0)
                    lblMsg.Text = "Record saved successfully.";
                else
                    lblMsg.Text = "Record updated successfully.";
                BindMidCategory();
                btnCancel_OnClick(sender, e);

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error while saving. " + ex.Message.ToString();
        }
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        ddlMainCat.SelectedIndex = 0;
        txtMidCategory.Text = "";
        btnSave.Text = "Save";
        CatID = 0;
        MidCatID = 0;
        tblAddEdit.Visible = false;
        divAdd.Visible = true;

    }

    //Repeater
    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        string Ids  = ((ImageButton)sender).CommandArgument;
        ddlMainCat.SelectedValue = Ids.Split(':').GetValue(1).ToString().Trim();
        CatID = Common.CastAsInt32(Ids.Split(':').GetValue(1).ToString().Trim());
        MidCatID = Common.CastAsInt32(Ids.Split(':').GetValue(0).ToString().Trim());
        Label lblMidCategory = (Label)((ImageButton)sender).FindControl("lblMidCategory");

        txtMidCategory.Text = lblMidCategory.Text;
        btnSave.Text = "Update";
        tblAddEdit.Visible = true;
        divAdd.Visible = false;

    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        int intMidCatID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string str = "delete  FROM  dbo.IVM_MidCategory where MidCatId=" + intMidCatID + " ; select -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt != null)
        {
            lblMsg.Text = "Record deleted successfully.";
            BindMidCategory();
            btnCancel_OnClick(sender, e);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tblAddEdit.Visible = true;
        divAdd.Visible = false;
    }
  
}

    