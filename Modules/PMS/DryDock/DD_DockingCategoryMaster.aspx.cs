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

public partial class DryDock_DD_DockingCategoryMaster : System.Web.UI.Page
{
    public int JobCatId
    {
        set { ViewState["JobCategoryId"] = value; }
        get { return Common.CastAsInt32(ViewState["JobCategoryId"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblCalMag.Text = "";
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            LoadCategory();
        }
    }
    protected void LoadCategory()
    {
        DataTable dtGroups = new DataTable();
        string strSQL = "SELECT CatId,CatCode,CatName FROM DD_JobCategory Order By CatCode";
        dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptJobCats.DataSource = dtGroups;
        rptJobCats.DataBind();
    }

    //------------- Add/ Edit Docking Category Section
    protected void btnAddCat_Click(object sender, EventArgs e)
    {
        JobCatId = 0;
        btnEditCat.Visible = false;
        dv_JobCategory.Visible = true;
        txtCatCode.Focus();
        LoadCategory();
    }
    protected void btnEditCat_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CatCode,CatName FROM DD_JobCategory WHERE CatId=" + JobCatId);
        if (dt.Rows.Count > 0)
        {
            txtCatCode.Text = dt.Rows[0]["CatCode"].ToString();
            txtCatName.Text = dt.Rows[0]["CatName"].ToString();
        }

        dv_JobCategory.Visible = true;
        txtCatCode.Focus();
    }
    protected void btnSelectCat_Click(object sender, EventArgs e)
    {
        JobCatId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        LoadCategory(); 
        btnEditCat.Visible = true;
    }
    protected void btnSaveCat_Click(object sender, EventArgs e)
    {
        if (txtCatCode.Text.Trim() == "")
        {
            txtCatCode.Focus();
            lblCalMag.Text = "Please enter Docking Category Code.";
            return;
        }
        if (txtCatName.Text.Trim() == "")
        {
            txtCatName.Focus();
            lblCalMag.Text = "Please enter Docking Category Name.";
            return;
        }
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CatCode,CatName FROM DD_JobCategory WHERE CatCode='" + txtCatCode.Text.Trim() + "' AND CatId <> " + JobCatId);

        //if (dt.Rows.Count > 0)
        //{
        //    txtCatCode.Focus();
        //    lblCalMag.Text = "Please check! Docking Category Code already exists.";
        //    return;
        //}

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT CatCode,CatName FROM DD_JobCategory WHERE CatName='" + txtCatName.Text.Trim() + "' AND CatId <> " + JobCatId);

        if (dt1.Rows.Count > 0)
        {
            txtCatName.Focus();
            lblCalMag.Text = "Please check! Docking Category Name already exists.";
            return;
        }

        try
        {
            Common.Set_Procedures("[dbo].[DD_InsertUpdateJobCategory]");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
               new MyParameter("@CatId", JobCatId),
               new MyParameter("@CatCode", txtCatCode.Text.Trim()),
               new MyParameter("@CatName", txtCatName.Text.Trim())
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblCalMag.Text = "Docking Category added/ edited successfully.";
            }
        }
        catch (Exception ex)
        {
            lblCalMag.Text = "Unable to add/ edit Docking Category.Error :" + ex.Message.ToString();
        }
    }
    protected void btnCancelCat_Click(object sender, EventArgs e)
    {
        txtCatCode.Text = "";
        txtCatName.Text = "";
        LoadCategory();
        dv_JobCategory.Visible = false;
    }
}