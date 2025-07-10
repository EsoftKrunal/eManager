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

public partial class Registers_CostCentre : System.Web.UI.Page
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
            bindCostCentreGrid();
          
            bindStatusDDL();
            HANDLE_AUTHORITY();
        }
    }
    private void HANDLE_AUTHORITY()
    {
        if (Mode == "New")
        {
            btn_Add_CostCentre.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode=="New" || Mode=="Edit"));
            btn_Save_CostCentre.Visible = false;
            btn_Cancel_CostCentre.Visible = false;
            pnl_CostCentre.Visible = false;
        }
        else if (Mode == "Edit")
        {
            btn_Add_CostCentre.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
            btn_Save_CostCentre.Visible = false;
            btn_Cancel_CostCentre.Visible = false;
            pnl_CostCentre.Visible = false;
        }
        else // Mode=View
        {
            btn_Add_CostCentre.Visible = false;
            btn_Save_CostCentre.Visible = false;
            btn_Cancel_CostCentre.Visible = false;
            pnl_CostCentre.Visible = false;
        }
    }
    public void bindCostCentreGrid()
    {
        DataTable dt1 = CostCentre.selectDataCostCentreDetails();
        this.GridView_CostCentre.DataSource = dt1;
        this.GridView_CostCentre.DataBind();
    }
    //public void bindCostCentreGroupNameDDL()
    //{
    //    DataTable dt4 = CostCentre.selectDataCostCentreGroupName();
    //    this.ddlCostCentreGroupName.DataValueField = "CostCentreGroupId";
    //    this.ddlCostCentreGroupName.DataTextField = "CostCentreGroupName";
    //    this.ddlCostCentreGroupName.DataSource = dt4;
    //    this.ddlCostCentreGroupName.DataBind();
    //}
    public void bindStatusDDL()
    {
        DataTable dt2 = CostCentre.selectDataStatusDetails();
        this.ddlStatus_CostCentre.DataValueField = "StatusId";
        this.ddlStatus_CostCentre.DataTextField = "StatusName";
        this.ddlStatus_CostCentre.DataSource = dt2;
        this.ddlStatus_CostCentre.DataBind();
    }
    protected void btn_Add_CostCentre_Click(object sender, EventArgs e)
    {
        //ddlCostCentreGroupName.SelectedIndex = 0;
        txtCostCentreName.Text = "";
        txtCreatedBy_CostCentre.Text = "";
        txtCreatedOn_CostCentre.Text = "";
        txtModifiedBy_CostCentre.Text = "";
        txtModifiedOn_CostCentre.Text = "";
        ddlStatus_CostCentre.SelectedIndex = 0;
        GridView_CostCentre.SelectedIndex = -1;
        HiddenCostCentre.Value = "";
        pnl_CostCentre.Visible = true;
        btn_Save_CostCentre.Visible = true;
        btn_Cancel_CostCentre.Visible = true;
        btn_Add_CostCentre.Visible = false;
        lbl_CostCentre_Message.Visible = false;
        btn_Print_CostCentre.Visible = true;
    }
    protected void btn_Save_CostCentre_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_CostCentre.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenCostCentreName");
                hfd1 = (HiddenField)dg.FindControl("HiddenCostCentreId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtCostCentreName.Text.ToUpper().Trim())
                {
                    if (HiddenCostCentre.Value.Trim() == "")
                    {
                        lbl_CostCentre_Message.Visible = true;
                        lbl_CostCentre_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenCostCentre.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_CostCentre_Message.Visible = true;
                        lbl_CostCentre_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_CostCentre_Message.Visible = false;
                    lbl_CostCentre_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intCostCentreSystemId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenCostCentre.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intCostCentreSystemId = Convert.ToInt32(HiddenCostCentre.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strCostCentreName = txtCostCentreName.Text;
                int intCostCentreId = 0;
                char charStatusId = Convert.ToChar(ddlStatus_CostCentre.SelectedValue);

                CostCentre.insertUpdateCostCentreDetails("InsertUpdateCostCentreDetails",
                                                              intCostCentreSystemId,
                                                              intCostCentreId,
                                                              strCostCentreName,
                                                              intCreatedBy,
                                                              intModifiedBy,
                                                              charStatusId);

                bindCostCentreGrid();

                btn_Add_CostCentre.Visible = false;
                btn_Add_CostCentre_Click(sender, e);
                btn_Cancel_CostCentre_Click(sender, e);
                lbl_CostCentre_Message.Visible = true;
                lbl_CostCentre_Message.Text = "Record Successfully Saved.";
            }
      
    }
    protected void btn_Cancel_CostCentre_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btn_Add_CostCentre.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_Add_CostCentre.Visible = (Auth.isAdd || Auth.isEdit);
        }
        pnl_CostCentre.Visible = false;
        btn_Save_CostCentre.Visible = false;
        btn_Cancel_CostCentre.Visible = false;
        btn_Add_CostCentre.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        GridView_CostCentre.SelectedIndex = -1;
        lbl_CostCentre_Message.Visible = false;
        btn_Print_CostCentre.Visible = false;
    }
    protected void btn_Print_CostCentre_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_CostCentre_DataBound(object sender, EventArgs e)
    {
        try
        {
            //GridView_CostCentre.Columns[1].Visible = false;
            //GridView_CostCentre.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify

            this.GridView_CostCentre.Columns[1].Visible = Auth.isEdit;

            this.GridView_CostCentre.Columns[2].Visible = Auth.isDelete;
           

            // Can Print
            if (Auth.isPrint)
            {
            }

        }
        catch
        {
            //GridView_CostCentre.Columns[1].Visible = false;
            //GridView_CostCentre.Columns[2].Visible = false;
        }
    }
    protected void Show_Record_CostCentre(int CostCentreid)
    {
        lbl_CostCentre_Message.Visible = false;
        HiddenCostCentre.Value = CostCentreid.ToString();
        DataTable dt3 = CostCentre.selectDataCostCentreDetailsByCostCentreId(CostCentreid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtCostCentreName.Text = dr["CostCentreName"].ToString();
            //ddlCostCentreGroupName.SelectedValue = dr["CostCentreGroupId"].ToString();
            txtCreatedBy_CostCentre.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_CostCentre.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_CostCentre.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_CostCentre.Text = dr["ModifiedOn"].ToString();
            ddlStatus_CostCentre.SelectedValue = dr["StatusId"].ToString();
        }
        btn_Print_CostCentre.Visible = true;
    }
    // VIEW THE RECORD
    protected void GridView_CostCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        HiddenField hfdCostCentre;
        hfdCostCentre = (HiddenField)GridView_CostCentre.Rows[GridView_CostCentre.SelectedIndex].FindControl("HiddenCostCentreId");
        id = Convert.ToInt32(hfdCostCentre.Value.ToString());
        pnl_CostCentre.Visible = true;
        Show_Record_CostCentre(id);
        btn_Add_CostCentre.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save_CostCentre.Visible = false;
        btn_Cancel_CostCentre.Visible = true;
    }
    //EDIT THE RECORD
    protected void GridView_CostCentre_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdCostCentre;
        hfdCostCentre = (HiddenField)GridView_CostCentre.Rows[e.NewEditIndex].FindControl("HiddenCostCentreId");
        id = Convert.ToInt32(hfdCostCentre.Value.ToString());
        Show_Record_CostCentre(id);
        GridView_CostCentre.SelectedIndex = e.NewEditIndex;
        pnl_CostCentre.Visible = true;
        btn_Add_CostCentre.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save_CostCentre.Visible = true;
        btn_Cancel_CostCentre.Visible = true;
    }
    // DELETE THE RECORD
    protected void GridView_CostCentre_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdCostCentre;
        hfdCostCentre= (HiddenField)GridView_CostCentre.Rows[e.RowIndex].FindControl("HiddenCostCentreId");
        id = Convert.ToInt32(hfdCostCentre.Value.ToString());
        CostCentre.deleteCostCentreDetailsById("DeleteCostCentreDetailsById", id, intModifiedBy);
        bindCostCentreGrid();
        if (HiddenCostCentre.Value.ToString() == hfdCostCentre.Value.ToString())
        {
            btn_Add_CostCentre_Click(sender, e);
        }
        lbl_CostCentre_Message.Visible = false;
    }
    protected void GridView_CostCentre_PreRender(object sender, EventArgs e)
    {
        if (GridView_CostCentre.Rows.Count <= 0) { lbl_GridView_CostCentre.Text = "No Records Found..!"; }
    }
}
