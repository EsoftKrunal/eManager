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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class Registers_Currency : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_Currency_Message.Text = "";
        lbl_GridView_Currency.Text = "";
       
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
       
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindCurrencyGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_Currency);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_Currency, btn_Save_Currency, btn_Cancel_Currency, btn_Print_Currency, Auth);
        }
    }
    public void bindCurrencyGrid()
    {
        DataTable dt1 = Currency.selectDataCurrencyDetails();
        this.GridView_Currency.DataSource = dt1;
        this.GridView_Currency.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = Currency.selectDataStatusDetails();
        this.ddlStatus_Currency.DataValueField = "StatusId";
        this.ddlStatus_Currency.DataTextField = "StatusName";
        this.ddlStatus_Currency.DataSource = dt2;
        this.ddlStatus_Currency.DataBind();
    }
    protected void btn_Add_Currency_Click(object sender, EventArgs e)
    {
        txtCurrencyName.Text = "";
        txtExchangeRate.Text = "";
        txtCreatedBy_Currency.Text = "";
        txtCreatedOn_Currency.Text = "";
        txtModifiedBy_Currency.Text = "";
        txtModifiedOn_Currency.Text = "";
        ddlStatus_Currency.SelectedIndex = 0;
        HiddenCurrency.Value = "";
        GridView_Currency.SelectedIndex = -1;
       
        Alerts.ShowPanel(pnl_Currency);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_Currency, btn_Save_Currency, btn_Cancel_Currency, btn_Print_Currency, Auth);
    }
    protected void btn_Save_Currency_Click(object sender, EventArgs e)
    {
         int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_Currency.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenCurrencyName");
                hfd1 = (HiddenField)dg.FindControl("HiddenCurrencyId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtCurrencyName.Text.ToUpper().Trim())
                {
                    if (HiddenCurrency.Value.Trim() == "")
                    {
                        
                        lbl_Currency_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenCurrency.Value.Trim() != hfd1.Value.ToString())
                    {
                        
                        lbl_Currency_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    
                    lbl_Currency_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intCurrencyId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;

                if (HiddenCurrency.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intCurrencyId = Convert.ToInt32(HiddenCurrency.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strCurrencyName = txtCurrencyName.Text;
                double dblExchangeRate = Convert.ToDouble(txtExchangeRate.Text);
                char charStatusId = Convert.ToChar(ddlStatus_Currency.SelectedValue);

                Currency.insertUpdateCurrencyDetails("InsertUpdateCurrencyDetails",
                                              intCurrencyId,
                                              strCurrencyName,
                                              dblExchangeRate,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindCurrencyGrid();
                
                lbl_Currency_Message.Text = "Record Successfully Saved.";
               
                Alerts.HidePanel(pnl_Currency);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_Currency, btn_Save_Currency, btn_Cancel_Currency, btn_Print_Currency, Auth);
            }
      
    }
    protected void btn_Cancel_Currency_Click(object sender, EventArgs e)
    {
        
        GridView_Currency.SelectedIndex = -1;
        
        Alerts.HidePanel(pnl_Currency);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_Currency, btn_Save_Currency, btn_Cancel_Currency, btn_Print_Currency, Auth);
    }
    protected void btn_Print_Currency_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_Currency_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_Currency, Auth);
    }
    protected void Show_Record_Currency(int Currencyid)
    {
        
        HiddenCurrency.Value = Currencyid.ToString();
        DataTable dt3 = Currency.selectDataCurrencyDetailsByCurrencyId(Currencyid);
        foreach (DataRow dr in dt3.Rows)
        {
            double dbl=Convert.ToDouble(dr["ExchangeRate"].ToString());
            txtCurrencyName.Text = dr["CurrencyName"].ToString();
            txtExchangeRate.Text = Convert.ToString (Math.Round(dbl,2));
       
            txtCreatedBy_Currency.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_Currency.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_Currency.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_Currency.Text = dr["ModifiedOn"].ToString();
            ddlStatus_Currency.SelectedValue = dr["StatusId"].ToString();
        }
        
    }
   
    protected void GridView_Currency_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        HiddenField hfdCurrency;
        hfdCurrency = (HiddenField)GridView_Currency.Rows[GridView_Currency.SelectedIndex].FindControl("HiddenCurrencyId");
        id = Convert.ToInt32(hfdCurrency.Value.ToString());
        
        Show_Record_Currency(id);
       
        Alerts.ShowPanel(pnl_Currency);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_Currency, btn_Save_Currency, btn_Cancel_Currency, btn_Print_Currency, Auth);
    }
  
    protected void GridView_Currency_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        
        HiddenField hfdCurrency;
        hfdCurrency = (HiddenField)GridView_Currency.Rows[e.NewEditIndex].FindControl("HiddenCurrencyId");
        id = Convert.ToInt32(hfdCurrency.Value.ToString());
        Show_Record_Currency(id);
        GridView_Currency.SelectedIndex = e.NewEditIndex;
      
        Alerts.ShowPanel(pnl_Currency);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Currency, btn_Save_Currency, btn_Cancel_Currency, btn_Print_Currency, Auth);
    }
  
    protected void GridView_Currency_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdCurrency;
        hfdCurrency = (HiddenField)GridView_Currency.Rows[e.RowIndex].FindControl("HiddenCurrencyId");
        id = Convert.ToInt32(hfdCurrency.Value.ToString());
        Currency.deleteCurrencyDetailsById("DeleteCurrencyById", id, modifiedby);
        bindCurrencyGrid();
        if (HiddenCurrency.Value.ToString() == hfdCurrency.Value.ToString())
        {
            btn_Add_Currency_Click(sender, e);
        }
        
    }
    protected void GridView_Currency_PreRender(object sender, EventArgs e)
    {
        if (GridView_Currency.Rows.Count <= 0) { lbl_GridView_Currency.Text = "No Records Found..!"; }
    }

    protected void GridView_Currency_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdCurrency;
            hfdCurrency = (HiddenField)GridView_Currency.Rows[Rowindx].FindControl("hdnCurrencyId");
            id = Convert.ToInt32(hfdCurrency.Value.ToString());
            Show_Record_Currency(id);
            GridView_Currency.SelectedIndex = Rowindx;

            Alerts.ShowPanel(pnl_Currency);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_Currency, btn_Save_Currency, btn_Cancel_Currency, btn_Print_Currency, Auth);
        }
        }
    protected void btnEditCurrcency_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdCurrency;
        hfdCurrency = (HiddenField)GridView_Currency.Rows[Rowindx].FindControl("hdnCurrencyId");
        id = Convert.ToInt32(hfdCurrency.Value.ToString());
        Show_Record_Currency(id);
        GridView_Currency.SelectedIndex = Rowindx;

        Alerts.ShowPanel(pnl_Currency);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Currency, btn_Save_Currency, btn_Cancel_Currency, btn_Print_Currency, Auth);
    }
    }
