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

public partial class Emtm_Inve_MainCategory : System.Web.UI.Page
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            BindCategory();
        }
    }


    //Function ------------------------------------------------------------------------------
    public void BindCategory()
    {
        string sql = "SELECT MainCatID,MainCatName FROM  dbo.IVM_Category";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt != null)
        {
            rptCategory.DataSource = dt;
            rptCategory.DataBind();
        }
    }

    //Events ------------------------------------------------------------------------------
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (txtMainCategory.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter category name.";
                txtMainCategory.Focus(); return;
            }

            if (CatID == 0)
            {
                string strCheckDuplicate = "SELECT * FROM  dbo.IVM_Category Where MainCatName = '" + txtMainCategory.Text.Trim() + "' ";
                DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(strCheckDuplicate);
                if (dtDuplicate.Rows.Count > 0)
                {
                    lblMsg.Text = "Category already exists.";
                    txtMainCategory.Focus();
                    return;
                }
            }

            string sql = "";
            if (CatID == 0)
                sql = "INSERT INTO dbo.IVM_Category values('" + txtMainCategory.Text.Trim().Replace("'", "`") + "' ) ;select -1";
            else
                sql = "UPDATE dbo.IVM_Category SET MainCatName='" + txtMainCategory.Text.Trim().Replace("'", "`") + "' WHERE MainCatID=" + CatID + " ;select -1";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt != null)
            {
                if (CatID == 0)
                    lblMsg.Text = "Record saved successfully.";
                else
                    lblMsg.Text = "Record updated successfully.";
                BindCategory();
                //CatID = 0;
                //btnSave.Text = "Save";
                //tblAddEdit.Visible = false;
                //divAdd.Visible = true;
                btnCancel_OnClick(sender, e);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error while saving. "+ex.Message.ToString();
        }
    }
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        txtMainCategory.Text = "";
        btnSave.Text = "Save";
        CatID = 0;
        tblAddEdit.Visible = false;
        divAdd.Visible = true;

    }
    
    //Repeater
    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
       CatID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
       Label lblCategory = (Label)((ImageButton)sender).FindControl("lblCategory");

       txtMainCategory.Text = lblCategory.Text;
       btnSave.Text = "Update";
       tblAddEdit.Visible = true;
       divAdd.Visible = false;
       
    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        int intCatID=Common.CastAsInt32( ((ImageButton)sender).CommandArgument);
        string str = "delete  FROM  dbo.IVM_Category where MainCatID=" + intCatID + " ; select -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt != null)
        {
            lblMsg.Text = "Record deleted successfully.";
            BindCategory();
            btnSave.Text = "Save";
            CatID = 0;
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tblAddEdit.Visible = true;
        divAdd.Visible = false;
    }
}

    