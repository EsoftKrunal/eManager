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

public partial class Registers_SupplierPortAgentMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //-------------- NEED TO CHANGE THIS
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");
        }
        //--------------
        if (!IsPostBack)
        {
            loadSuppliers();
            ddlSuppliers_SelectedIndexChanged(sender, e);
        }
    }
    private void loadSuppliers()
    {
        DataSet ds = Budget.getTable("Select SupplierId,Company as 'VendorName' from SupplierMaster where statusid='A' ORDER BY Company");
        this.ddlSuppliers.DataValueField = "SupplierId";
        this.ddlSuppliers.DataTextField = "VendorName";
        this.ddlSuppliers.DataSource = ds;
        this.ddlSuppliers.DataBind();
    }
    private void loadPortAgents(Int32 SupplierId)
    {
        if(SupplierId>0)
        {
            DataSet ds = Budget.getTable("Select PortAgentId,Company as 'VendorName' from PortAgent Where statusid='A' And PortAgent.PortAgentId in (Select PortAgentId from SupplierPortAgentmapping Where SupplierId=" + SupplierId  + ") order by company");
            this.lstUsed.DataValueField = "PortAgentId";
            this.lstUsed.DataTextField = "VendorName";
            this.lstUsed.DataSource = ds;
            this.lstUsed.DataBind();
        }
        else
        {
            DataSet ds = Budget.getTable("Select PortAgentId,Company as 'VendorName' from PortAgent Where statusid='A' And PortAgent.PortAgentId not in (Select PortAgentId from SupplierPortAgentmapping) order by company");
            this.lblRest.DataValueField = "PortAgentId";
            this.lblRest.DataTextField = "VendorName";
            this.lblRest.DataSource = ds;
            this.lblRest.DataBind();
        }
    }
    protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPortAgents(Int32.Parse(ddlSuppliers.SelectedValue));
        loadPortAgents(0);  
 
    }
    protected void btnRight_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i <= lblRest.Items.Count - 1; i++)
        {
            if (lblRest.Items[i].Selected)
            {
                Budget.getTable("INSERT INTO SUPPLIERPORTAGENTMAPPING(SUPPLIERID,PORTAGENTID) VALUES(" + ddlSuppliers.SelectedValue + "," + lblRest.Items[i].Value  +  ")");      
            }
        }

        ddlSuppliers_SelectedIndexChanged(sender, e);
    }
    protected void btnLeft_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i <= lstUsed.Items.Count - 1; i++)
        {
            if (lstUsed.Items[i].Selected)
            {
                Budget.getTable("DELETE FROM SUPPLIERPORTAGENTMAPPING WHERE PORTAGENTID=" + lstUsed.Items[i].Value);
            }
        }
        ddlSuppliers_SelectedIndexChanged(sender, e);
    }
}
