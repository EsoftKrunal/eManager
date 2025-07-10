using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class VIMS_MWUC_AddEditCategory : System.Web.UI.Page
{
    public int CatId
    {
        get { return Common.CastAsInt32(ViewState["CatId"]); }
        set { ViewState["CatId"] = value; }
    }
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            CurrentVessel = Session["CurrentShip"].ToString();
            Bindgrid();
        }
    }

    protected void Bindgrid()
    {
        string SQL = "SELECT * FROM MWUC_CATMASTER WHERE VESSELCODE = '" + CurrentVessel + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + " ORDER BY CATNAME ");

        if (dt != null && dt.Rows.Count > 0)
        {
            rptCat.DataSource = dt;
            rptCat.DataBind();
        }
        else
        {
            rptCat.DataSource = null;
            rptCat.DataBind();
        }
    }

    protected void btnAddNewCat_Click(object sender, EventArgs e)
    {
        CatId = 0;
        dv_AddNew.Visible = true;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        CatId = Common.CastAsInt32(((ImageButton)sender).CommandArgument); 
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select CatName FROM MWUC_CATMASTER WHERE VESSELCODE ='" + CurrentVessel + "' AND CATID=" + CatId);
        txtCatName.Text = dt.Rows[0]["CatName"].ToString(); 
        dv_AddNew.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtCatName.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter category name.";
            txtCatName.Focus();
            return;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select CatName FROM MWUC_CATMASTER WHERE CatName='" + txtCatName.Text.Trim() + "' AND VESSELCODE ='" + CurrentVessel + "' AND CATID <> " + CatId);
        if (dt.Rows.Count > 0)
        {
            lblMsg.Text = "Please Check! category name already exists.";
            txtCatName.Focus();
            return;
        }


        try
        { 
            Common.Set_Procedures("[dbo].[MWUC_InsertUpdateCategory]");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
               new MyParameter("@VESSELCODE", CurrentVessel),
               new MyParameter("@CATID", CatId),               
               new MyParameter("@CATNAME", txtCatName.Text));
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblMsg.Text = "Category added/ updated successfully.";

            }
            else
            {
                lblMsg.Text = "Unable to add/ update category.Error :" + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to add/ update category.Error :" + ex.Message.ToString();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        CatId = 0;
        txtCatName.Text = "";
        Bindgrid();

        dv_AddNew.Visible = false;
    }
}