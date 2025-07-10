using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MRV_FuelTypes : System.Web.UI.Page
{
    public int FuelTypeId
    {
        get { return Common.CastAsInt32(ViewState["FuelTypeId"]);}
        set { ViewState["FuelTypeId"] = value; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        Session["MM"] = 2;
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            FuelTypeId = 0;
            BindGrid();
        }
    }

    public void BindGrid()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FuelTypes Order BY FuelTypeName");
        rptData.DataSource = dt;
        rptData.DataBind();
    }

    protected void btn_Add_Click(object sender,EventArgs e)
    {
        ClearControls();
        dvModal.Visible = true;
        dvAddEdit.Visible = true;

    }
    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        int _key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowRecord(_key);        
    }
    
    private void ClearControls()
    {
        FuelTypeId = 0;
        txtFuelTypeName.Text = "";
        txtShortName.Text = "";
        txtCo2Per.Text = "";
    }
    private void ShowRecord(int _FuelTypeId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_FuelTypes WHERE FuelTypeId=" + _FuelTypeId);
        if (dt.Rows.Count > 0)
        {
            FuelTypeId = _FuelTypeId;
            txtFuelTypeName.Text = dt.Rows[0]["FuelTypeName"].ToString();
            txtShortName.Text = dt.Rows[0]["ShortName"].ToString();
            txtCo2Per.Text = dt.Rows[0]["Co2EmissionPerMT"].ToString();

            dvModal.Visible = true;
            dvAddEdit.Visible = true;
        }
        else
        {
            ShowMessage(true, "Sorry ! selected record does'nt exists in the database.");
            BindGrid();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dvModal.Visible = false;
        dvAddEdit.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("DBO.MRV_InsertUpdate_FuelType");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(new MyParameter("FuelTypeId", FuelTypeId),
            new MyParameter("@FuelTypeName", txtFuelTypeName.Text),
            new MyParameter("@ShortName", txtShortName.Text),
            new MyParameter("@Co2EmissionPerMT",Common.CastAsDecimal(txtCo2Per.Text)));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ShowMessage(false, "Record Saved Successfully.");
            ClearControls();    
            BindGrid();
        }
        else
        {
            ShowMessage(true, "Unable to save record. Error : " + Common.getLastError());
        }
    }
    public void ShowMessage(bool error,string Message)
    {
        lblMsg.Text = Message;
        lblMsg.CssClass= (error)? "modal_error" : "modal_success";
    }
}
