using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Purchase_Invoice_LubeConsumptionSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindVessel();
                BindYear();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    public void BindVessel()
    {
        string WhereClause = "";
        string sql = "SELECT VesselId,VesselCode,Vesselname FROM Vessel v Where 1=1 ";
        sql = sql + WhereClause + " and v.VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ORDER BY VESSELNAME";
        ddlVessel.DataSource = VesselReporting.getTable(sql);

        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselCode";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("Select", "0"));
    }

    private void BindYear()
    {
        ddlYear.Items.Add(new ListItem("< ALL >", "0"));
        for (int i = DateTime.Today.Year; i >= 2023; i--)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlVessel.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please select Vessel.');", true);
                ddlVessel.Focus();
                return;
            }
            GetConsumptionSummary(ddlVessel.SelectedValue, Convert.ToInt32(ddlYear.SelectedValue));
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    protected void imgbtnView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clearLubeConsumpSummary();
            int bidItemId = 0;
            int bidId = 0;
            bidItemId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            bidId = Common.CastAsInt32(((ImageButton)sender).Attributes["BidId"]);
            if (bidItemId > 0 && bidId > 0)
            {
                dv_LubeSummary.Visible = true;
                DataSet ds = new DataSet();
                ds = GetPOItemDetails(bidItemId, bidId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblVesselName.Text = ddlVessel.SelectedItem.Text;
                    lblSupplierPort.Text = ds.Tables[0].Rows[0]["DeliveryPort"].ToString();
                    lblSupplierDt.Text = Common.ToDateString(ds.Tables[0].Rows[0]["DeliveryDate"]);
                    lblItemDescription.Text = Convert.ToString(ds.Tables[0].Rows[0]["description"]);
                    lblReceivedQty.Text = Convert.ToString(ds.Tables[0].Rows[0]["QtyRecd"]);
                    lblUOM.Text = Convert.ToString(ds.Tables[0].Rows[0]["Uom"]);
                    lblUnitPrice.Text = ds.Tables[0].Rows[0]["PriceFor"].ToString();
                    lblTotalReceivedCost.Text = ds.Tables[0].Rows[0]["ForTotal"].ToString();
                }
                else
                {
                    lblVesselName.Text = ddlVessel.SelectedItem.Text;
                    
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    rptConsumptionDetails.DataSource = ds.Tables[1];
                    rptConsumptionDetails.DataBind();
                }
             }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }

    protected void clearLubeConsumpSummary()
    {
        lblSupplierPort.Text = "";
        lblSupplierDt.Text = "";
        lblItemDescription.Text = "";
        lblReceivedQty.Text = "";
        lblUOM.Text = "";
        lblUnitPrice.Text = "";
        lblTotalReceivedCost.Text = "";
        rptConsumptionDetails.DataSource = null;
        rptConsumptionDetails.DataBind();
    }

    protected DataSet GetPOItemDetails(int BidItemId, int Bidid)
    {
        Common.Set_Procedures("GetLubeConsumptionDetails");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@VesselCode", ddlVessel.SelectedValue),
            new MyParameter("@BidItemId", BidItemId),
            new MyParameter("@BidId", Bidid)
          );
        DataSet result = new DataSet();
        return result = Common.Execute_Procedures_Select();
    }

    private void GetConsumptionSummary(string  vesselCode, int year)
    { 
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC Get_LubeConsumptionSummary '" + vesselCode + "'," + year + " ");
        if (dt.Rows.Count > 0)
        {
            RptPOConsumption.DataSource = dt;
            RptPOConsumption.DataBind();
        }
        else
        {
            RptPOConsumption.DataSource = null;
            RptPOConsumption.DataBind();
        }
    }

    protected void btnClose1_Click(object sender, EventArgs e)
    {
        dv_LubeSummary.Visible = false;
    }
}