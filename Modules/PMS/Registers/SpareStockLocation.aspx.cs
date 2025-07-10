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

public partial class Modules_PMS_Registers_SpareStockLocation : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public string strStockLocationId
    {
        set { ViewState["StockLocationId"] = value; }
        get { return ViewState["StockLocationId"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        //***********Code to check page acessing Permission

        Auth = new AuthenticationManager(1046, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("~/AuthorityError.aspx");
        }
        else
        {
            btnAdd.Visible = Auth.IsAdd;
            btnSave.Visible = Auth.IsAdd;
        }

        //*******************

        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 3;
            strStockLocationId = "";
            BindStockLocation();
        }
    }

    #region ------------------ USER DEFINED FUNCTIONS ----------------------------

    private void BindStockLocation()
    {
        DataTable dtJobs;
        String strJobs;
        strJobs = "SELECT StockLocationId,StockLocation FROM SpareStockLocation with(nolock) where status='A' ORDER BY StockLocation";
        dtJobs = Common.Execute_Procedures_Select_ByQuery(strJobs);
        rptItems.DataSource = dtJobs;
        rptItems.DataBind();
    }
    private Boolean IsValidated()
    {
        if (txtStockLocation.Text == "")
        {
            MessageBox1.ShowMessage("Please enter Stock Location.", true);
            txtStockLocation.Focus();
            return false;
        }
       
        return true;
    }
    #endregion

    #region ------------------- EVENTS -------------------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvJM');", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }
        DataTable dtStockLocation = new DataTable();
        string strStockLocation= "SELECT StockLocation FROM SpareStockLocation with(nolock) WHERE StockLocation ='" + txtStockLocation.Text.Trim() + "' AND StockLocationId <>'" + strStockLocationId + "' And Status='A'";
        dtStockLocation = Common.Execute_Procedures_Select_ByQuery(strStockLocation);
        if (dtStockLocation.Rows.Count > 0)
        {
            MessageBox1.ShowMessage("Stock Location already exists.", true);
            txtStockLocation.Focus();
            return;
        }
       

        Common.Set_Procedures("sp_InsertUpdateSpareStockLocation");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
           new MyParameter("@StockLocationId", strStockLocationId),
           new MyParameter("@StockLocation", txtStockLocation.Text.Trim()),
           new MyParameter("@Createdby", Convert.ToInt32(Session["loginid"].ToString()))
           );
        DataSet dsstrStockLocations = new DataSet();
        dsstrStockLocations.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsstrStockLocations);
        if (res)
        {
            MessageBox1.ShowMessage("Spare Stock Location Added Successfully.", false);
            BindStockLocation();
            btnAdd_Click(sender, e);
        }
        else
        {
            MessageBox1.ShowMessage("Unable to update Spare Stock Location.Error :" + Common.getLastError(), true);
        }
    }
    protected void btnStockLocation_Click(object sender, EventArgs e)
    {
        int StockLocationId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        String StrStockLocationDetails = "SELECT StockLocationId,StockLocation FROM SpareStockLocation with(nolock) " +
                               "WHERE StockLocationId =" + StockLocationId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(StrStockLocationDetails);
        if (dt.Rows.Count > 0)
        {
            txtStockLocation.Text = dt.Rows[0]["StockLocation"].ToString();
            strStockLocationId = Convert.ToString(StockLocationId);
            btnDelete.Visible = true;
        }



    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("sp_DeleteSpareStockLocation");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
           new MyParameter("@StockLocationId", strStockLocationId),
           new MyParameter("@Createdby", Convert.ToInt32(Session["loginid"].ToString()))
           );
        DataSet dsStockLocations = new DataSet();
        dsStockLocations.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsStockLocations);
        if (res)
        {
            MessageBox1.ShowMessage("Spare Stock Location Deleted Successfully.", false);
            BindStockLocation();
            btnAdd_Click(sender, e);
        }
        else
        {
            MessageBox1.ShowMessage("Unable to delete Spare Stock Location.Error :" + Common.getLastError(), true);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtStockLocation.Text = "";
        strStockLocationId = "";
    }
    #endregion
}