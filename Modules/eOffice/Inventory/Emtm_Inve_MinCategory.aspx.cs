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

public partial class Emtm_Inve_MinCategory : System.Web.UI.Page
{
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
    public int MinCatID
    {
        set
        {
            ViewState["MinCatID"] = value;
        }
        get
        {
            return Common.CastAsInt32(ViewState["MinCatID"]);
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
            BindMidCategory();
            BindMinCategory();
        }
    }


    //Function ------------------------------------------------------------------------------
    
    public void BindMidCategory()
    {
        string sql = "SELECT MidCatId,MidCatName FROM DBO.IVM_MidCategory Order By  MidCatName";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            ddlMidCat.DataSource = dt;
            ddlMidCat.DataTextField = "MidCatName";
            ddlMidCat.DataValueField = "MidCatId";
            ddlMidCat.DataBind();
            ddlMidCat.Items.Insert(0, new ListItem("< Select Mid Category >", "0"));
        }
    }

    public void BindMinCategory()
    {
        string sql = "SELECT IC.MainCatId,MainCatName,MIC.MidCatId,MidCatName,MinCatId,MinCatName FROM DBO.IVM_MinCategory MINC " +
                     "INNER Join DBO.IVM_MidCategory MIC ON MINC.MidCatId = MIC.MidCatId " +
                     "INNER JOIN DBO.IVM_Category IC ON IC.MainCatId = MIC.MainCatId ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt != null)
        {
            rptMinCategory.DataSource = dt;
            rptMinCategory.DataBind();
        }

    }

    //Events ------------------------------------------------------------------------------
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (ddlMidCat.SelectedIndex == 0)
            {
                lblMsg.Text = "Please select mid category.";
                ddlMidCat.Focus(); return;
            }

            if (txtMinCategory.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter min category name.";
                txtMinCategory.Focus(); return;
            }

            if (MidCatID != Common.CastAsInt32(ddlMidCat.SelectedValue.Trim()) || MinCatID == 0)
            {
                string strCheckDuplicate = "SELECT * FROM  dbo.IVM_MinCategory Where MidCatId= " + ddlMidCat.SelectedValue.Trim() + " AND MinCatName = '" + txtMinCategory.Text.Trim() + "' ";
                DataTable dtDuplicate = Common.Execute_Procedures_Select_ByQueryCMS(strCheckDuplicate);
                if (dtDuplicate.Rows.Count > 0)
                {
                    lblMsg.Text = "Min Category already exists.";
                    txtMinCategory.Focus();
                    return;
                }
            }

            string sql = "";
            if (MinCatID == 0)
                sql = "INSERT INTO dbo.IVM_MinCategory values(" + ddlMidCat.SelectedValue.Trim() + ", '" + txtMinCategory.Text.Trim().Replace("'", "`") + "' ) ;select -1";
            else
                sql = "UPDATE dbo.IVM_MinCategory SET MidCatId= " + ddlMidCat.SelectedValue.Trim() + ",  MinCatName='" + txtMinCategory.Text.Trim().Replace("'", "`") + "' WHERE MinCatId= " + MinCatID + "  ;select -1";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt != null)
            {
                if (MinCatID == 0)
                    lblMsg.Text = "Record saved successfully.";
                else
                    lblMsg.Text = "Record updated successfully.";
                BindMinCategory();
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
        ddlMidCat.SelectedIndex = 0;
        txtMinCategory.Text = "";
        btnSave.Text = "Save";
        MidCatID = 0;
        MinCatID = 0;
        tblAddEdit.Visible = false;
        divAdd.Visible = true;

    }

    //Repeater
    protected void imgEdit_OnClick(object sender, EventArgs e)
    {
        string Ids = ((ImageButton)sender).CommandArgument;
        ddlMidCat.SelectedValue = Ids.Split(':').GetValue(0).ToString().Trim();
        MidCatID = Common.CastAsInt32(Ids.Split(':').GetValue(0).ToString().Trim());
        MinCatID = Common.CastAsInt32(Ids.Split(':').GetValue(1).ToString().Trim());
        Label lblMinCategory = (Label)((ImageButton)sender).FindControl("lblMinCategory");

        txtMinCategory.Text = lblMinCategory.Text;
        btnSave.Text = "Update";
        tblAddEdit.Visible = true;
        divAdd.Visible = false;

    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        int intMinCatID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string str = "delete  FROM  dbo.IVM_MinCategory where MinCatId=" + intMinCatID + " ; select -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt != null)
        {
            lblMsg.Text = "Record deleted successfully.";
            BindMinCategory();
            btnCancel_OnClick(sender, e);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        tblAddEdit.Visible = true;
        divAdd.Visible = false;
    }
  
}

    