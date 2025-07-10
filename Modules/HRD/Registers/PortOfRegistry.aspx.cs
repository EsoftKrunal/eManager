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

public partial class Registers_PortOfRegistry : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 5);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            bindPortOfRegistryGrid();
            bindStatusDDL();
            HANDLE_AUTHORITY();
        }
    }
    private void HANDLE_AUTHORITY()
    {
        if (Mode == "New")
        {
            btn_Add_PortOfRegistry.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
            btn_Save_PortOfRegistry.Visible = false;
            btn_Cancel_PortOfRegistry.Visible = false;
            pnl_PortOfRegistry.Visible = false;
        }
        else if (Mode == "Edit")
        {
            btn_Add_PortOfRegistry.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
            btn_Save_PortOfRegistry.Visible = false;
            btn_Cancel_PortOfRegistry.Visible = false;
            pnl_PortOfRegistry.Visible = false;
        }
        else // Mode=View
        {
            btn_Add_PortOfRegistry.Visible = false;
            btn_Save_PortOfRegistry.Visible = false;
            btn_Cancel_PortOfRegistry.Visible = false;
            pnl_PortOfRegistry.Visible = false;
        }
    }
    public void bindPortOfRegistryGrid()
    {
        DataTable dt1 = PortOfRegistry.selectDataPortOfRegistryDetails();
        this.GridView_PortOfRegistry.DataSource = dt1;
        this.GridView_PortOfRegistry.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = Port.selectDataStatusDetails();
        this.ddlStatus_PortOfRegistry.DataValueField = "StatusId";
        this.ddlStatus_PortOfRegistry.DataTextField = "StatusName";
        this.ddlStatus_PortOfRegistry.DataSource = dt2;
        this.ddlStatus_PortOfRegistry.DataBind();
    }
    protected void btn_Add_PortOfRegistry_Click(object sender, EventArgs e)
    {
        txtPortOfRegistryName.Text = "";
        txtCreatedBy_PortOfRegistry.Text = "";
        txtCreatedOn_PortOfRegistry.Text = "";
        txtModifiedBy_PortOfRegistry.Text = "";
        txtModifiedOn_PortOfRegistry.Text = "";
        ddlStatus_PortOfRegistry.SelectedIndex = 0;
        HiddenPortOfRegistry.Value = "";
        pnl_PortOfRegistry.Visible = true;
        btn_Save_PortOfRegistry.Visible = true;
        btn_Cancel_PortOfRegistry.Visible = true;
        btn_Add_PortOfRegistry.Visible = false;
        lbl_PortOfRegistry_Message.Visible = false;
        btn_Print_PortOfRegistry.Visible = true;
    }
    protected void btn_Save_PortOfRegistry_Click(object sender, EventArgs e)
    {
        int intPortOfRegistryId = -1;
        int intCreatedBy = 0;
        int intModifiedBy = 0;
       if (HiddenPortOfRegistry.Value.ToString().Trim() == "")
            {
                intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                intPortOfRegistryId = Convert.ToInt32(HiddenPortOfRegistry.Value);
                intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            string strPortOfRegistryName = txtPortOfRegistryName.Text;
            char charStatusId = Convert.ToChar(ddlStatus_PortOfRegistry.SelectedValue);

            PortOfRegistry.insertUpdatePortOfRegistryDetails("InsertUpdatePortOfRegistryDetails",
                                          intPortOfRegistryId,
                                          strPortOfRegistryName,
                                          intCreatedBy,
                                          intModifiedBy,
                                          charStatusId);

            bindPortOfRegistryGrid();

            btn_Add_PortOfRegistry.Visible = false;
            btn_Add_PortOfRegistry_Click(sender, e);
            btn_Cancel_PortOfRegistry_Click(sender, e);
            lbl_PortOfRegistry_Message.Visible = true;
            lbl_PortOfRegistry_Message.Text = "Record Successfully Saved.";
       
    }
    protected void btn_Cancel_PortOfRegistry_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btn_Add_PortOfRegistry.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_Add_PortOfRegistry.Visible = (Auth.isAdd || Auth.isEdit);
        }
        pnl_PortOfRegistry.Visible = false;
        btn_Save_PortOfRegistry.Visible = false;
        btn_Cancel_PortOfRegistry.Visible = false;
        btn_Add_PortOfRegistry.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        GridView_PortOfRegistry.SelectedIndex = -1;
        lbl_PortOfRegistry_Message.Visible = false;
        btn_Print_PortOfRegistry.Visible = false;
    }
    protected void btn_Print_PortOfRegistry_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_PortOfRegistry_DataBound(object sender, EventArgs e)
    {
        try
        {
            //GridView_PortOfRegistry.Columns[1].Visible = false;
            //GridView_PortOfRegistry.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify

            this.GridView_PortOfRegistry.Columns[1].Visible = Auth.isEdit;

            this.GridView_PortOfRegistry.Columns[2].Visible = Auth.isDelete;
           
            // Can Print
            if (Auth.isPrint)
            {
            }

        }
        catch
        {
            //GridView_PortOfRegistry.Columns[1].Visible = false;
            //GridView_PortOfRegistry.Columns[2].Visible = false;
        }
    }
    protected void Show_Record_PortOfRegistry(int PortOfRegistryid)
    {
        lbl_PortOfRegistry_Message.Visible = false;
        HiddenPortOfRegistry.Value = PortOfRegistryid.ToString();
        DataTable dt3 = PortOfRegistry.selectDataPortOfRegistryDetailsByPortOfRegistryId(PortOfRegistryid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtPortOfRegistryName.Text = dr["PortOfRegistryName"].ToString();
            txtCreatedBy_PortOfRegistry.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_PortOfRegistry.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_PortOfRegistry.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_PortOfRegistry.Text = dr["ModifiedOn"].ToString();
            ddlStatus_PortOfRegistry.SelectedValue = dr["StatusId"].ToString();
        }
        btn_Print_PortOfRegistry.Visible = true;
    }
    // VIEW THE RECORD
    protected void GridView_PortOfRegistry_SelectedIndexChanged(object sender, EventArgs e)
    {
     
        HiddenField hfdPortOfRegistry;
        hfdPortOfRegistry = (HiddenField)GridView_PortOfRegistry.Rows[GridView_PortOfRegistry.SelectedIndex].FindControl("HiddenPortOfRegistryId");
        id = Convert.ToInt32(hfdPortOfRegistry.Value.ToString());
        pnl_PortOfRegistry.Visible = true;
        Show_Record_PortOfRegistry(id);
        btn_Add_PortOfRegistry.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save_PortOfRegistry.Visible = false;
        btn_Cancel_PortOfRegistry.Visible = true;
    }
    //EDIT THE RECORD
    protected void GridView_PortOfRegistry_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdPortOfRegistry;
        hfdPortOfRegistry = (HiddenField)GridView_PortOfRegistry.Rows[e.NewEditIndex].FindControl("HiddenPortOfRegistryId");
        id = Convert.ToInt32(hfdPortOfRegistry.Value.ToString());
        Show_Record_PortOfRegistry(id);
        GridView_PortOfRegistry.SelectedIndex = e.NewEditIndex;
        pnl_PortOfRegistry.Visible = true;
        btn_Add_PortOfRegistry.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save_PortOfRegistry.Visible = true;
        btn_Cancel_PortOfRegistry.Visible = true;
    }
    // DELETE THE RECORD
    protected void GridView_PortOfRegistry_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdPortOfRegistry;
        hfdPortOfRegistry = (HiddenField)GridView_PortOfRegistry.Rows[e.RowIndex].FindControl("HiddenPortOfRegistryId");
        id = Convert.ToInt32(hfdPortOfRegistry.Value.ToString());
        PortOfRegistry.deletePortOfRegistryDetailsById("DeletePortOfRegistryDetailsById", id, intModifiedBy);
        bindPortOfRegistryGrid();
        if (HiddenPortOfRegistry.Value.ToString() == hfdPortOfRegistry.Value.ToString())
        {
            btn_Add_PortOfRegistry_Click(sender, e);
        }
        lbl_PortOfRegistry_Message.Visible = false;
    }
    protected void GridView_PortOfRegistry_PreRender(object sender, EventArgs e)
    {
        if (GridView_PortOfRegistry.Rows.Count <= 0) { lbl_GridView_PortOfRegistry.Text = "No Records Found..!"; }
    }
}
