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

public partial class Registers_DangerousCargoEndorsement : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblCargo.Text = "";
        lbl_DCE_Message.Text = "";
      
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
            BindStatusDropDown();
            BindGridCargo();
            Alerts.HidePanel(Cargopanel);
            Alerts.HANDLE_AUTHORITY(1, btn_Cargo_add, btn_Cargo_save, btn_Cargo_Cancel, btn_Print_Cargo, Auth);     
        }
    }
    private void BindStatusDropDown()
    {
        DataTable dt2 = DangerousCargoEndorsement.selectDataStatus();
        this.ddstatus_Cargo.DataValueField = "StatusId";
        this.ddstatus_Cargo.DataTextField = "StatusName";
        this.ddstatus_Cargo.DataSource = dt2;
        this.ddstatus_Cargo.DataBind();
    }
    private void BindGridCargo()
    {
        DataTable dt = DangerousCargoEndorsement.selectDataCargoDetails();
        this.GvDCE.DataSource = dt;
        this.GvDCE.DataBind();
    }
    protected void GvDCE_SelectIndexChanged(object sender, EventArgs e)
    {
       
        HiddenField hfdcargo;
        hfdcargo = (HiddenField)GvDCE.Rows[GvDCE.SelectedIndex].FindControl("HiddenCargoId");
        id = Convert.ToInt32(hfdcargo.Value.ToString());
        Show_Record_cargo(id);
       
        Alerts.ShowPanel(Cargopanel);
        Alerts.HANDLE_AUTHORITY(4, btn_Cargo_add, btn_Cargo_save, btn_Cargo_Cancel, btn_Print_Cargo, Auth);
    }
    protected void Show_Record_cargo(int Cargoid)
    {
        HiddenCargopk.Value = Cargoid.ToString();
        DataTable dt3 = DangerousCargoEndorsement.selectDataCargoDetailsByCargoId(Cargoid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtCargoname.Text = dr["CargoName"].ToString();
            txtcreatedby_Cargo.Text = dr["CreatedBy"].ToString();
            txtcreatedon_Cargo.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_Cargo.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_Cargo.Text = dr["ModifiedOn"].ToString();
            ddstatus_Cargo.SelectedValue = dr["StatusId"].ToString();
        }
    }
    protected void btn_Print_Cargo_Click(object sender, EventArgs e)
    {

    }
    protected void GvDCE_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdcargo;
        hfdcargo = (HiddenField)GvDCE.Rows[e.NewEditIndex].FindControl("HiddenCargoId");
        id = Convert.ToInt32(hfdcargo.Value.ToString());
        Show_Record_cargo(id);
        GvDCE.SelectedIndex = e.NewEditIndex;
      
        Alerts.ShowPanel(Cargopanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Cargo_add, btn_Cargo_save, btn_Cargo_Cancel, btn_Print_Cargo, Auth);
    }
    protected void GvDCE_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvDCE.Rows[e.RowIndex].FindControl("HiddenCargoId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        DangerousCargoEndorsement.deleteCargoDetails("deleteDangerousCargoEndorsement", id, modifiedBy);
        BindGridCargo();
        if (HiddenCargopk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_Cargo_add_Click(sender, e);
        }
    }
    protected void GvDCE_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvDCE, Auth);
    }
    protected void GvDCE_PreRender(object sender, EventArgs e)
    {
        if (this.GvDCE.Rows.Count <= 0)
        {
            lblCargo.Text = "No Records Found..!";
        }
        else
        {
            lblCargo.Text = "";
        }
    }
    protected void btn_Cargo_add_Click(object sender, EventArgs e)
    {
        HiddenCargopk.Value = "";
        txtCargoname.Text = "";
        txtcreatedby_Cargo.Text = "";
        txtcreatedon_Cargo.Text = "";
        txtmodifiedby_Cargo.Text = "";
        txtmodifiedon_Cargo.Text = "";
        ddstatus_Cargo.SelectedIndex = 0;
        GvDCE.SelectedIndex = -1;
      
        Alerts.ShowPanel(Cargopanel);
        Alerts.HANDLE_AUTHORITY(2, btn_Cargo_add, btn_Cargo_save, btn_Cargo_Cancel, btn_Print_Cargo, Auth);    
    }
    protected void btn_Cargo_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GvDCE.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenCargoName");
                hfd1 = (HiddenField)dg.FindControl("HiddenCargoId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtCargoname.Text.ToUpper().Trim())
                {
                    if (HiddenCargopk.Value.Trim() == "")
                    {
                        lbl_DCE_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenCargopk.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_DCE_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_DCE_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int cargoId = -1;
                int createdby = 0, modifiedby = 0;
                string strCargoname = txtCargoname.Text;
                char status = Convert.ToChar(ddstatus_Cargo.SelectedValue);
                if (HiddenCargopk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    cargoId = Convert.ToInt32(HiddenCargopk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                DangerousCargoEndorsement.insertUpdateCargoDetails("InsertUpdateCargoDetails",
                                                                      cargoId,
                                                                      strCargoname,
                                                                      createdby,
                                                                      modifiedby,
                                                                      status);
                BindGridCargo();
                lbl_DCE_Message.Text = "Record Successfully Saved.";
                
                Alerts.HidePanel(Cargopanel);
                Alerts.HANDLE_AUTHORITY(3, btn_Cargo_add, btn_Cargo_save, btn_Cargo_Cancel, btn_Print_Cargo, Auth);
            }
    }
    protected void btn_Cargo_Cancel_Click(object sender, EventArgs e)
    {
        GvDCE.SelectedIndex = -1;
      
        Alerts.HidePanel(Cargopanel);
        Alerts.HANDLE_AUTHORITY(6, btn_Cargo_add, btn_Cargo_save, btn_Cargo_Cancel, btn_Print_Cargo, Auth);
    }

    protected void GvDCE_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdcargo;
            hfdcargo = (HiddenField)GvDCE.Rows[Rowindx].FindControl("hdnCargoId");
            id = Convert.ToInt32(hfdcargo.Value.ToString());
            Show_Record_cargo(id);
            GvDCE.SelectedIndex = Rowindx;

            Alerts.ShowPanel(Cargopanel);
            Alerts.HANDLE_AUTHORITY(5, btn_Cargo_add, btn_Cargo_save, btn_Cargo_Cancel, btn_Print_Cargo, Auth);
        }
    }
    protected void btnEditCargo_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdcargo;
        hfdcargo = (HiddenField)GvDCE.Rows[Rowindx].FindControl("hdnCargoId");
        id = Convert.ToInt32(hfdcargo.Value.ToString());
        Show_Record_cargo(id);
        GvDCE.SelectedIndex = Rowindx;

        Alerts.ShowPanel(Cargopanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Cargo_add, btn_Cargo_save, btn_Cargo_Cancel, btn_Print_Cargo, Auth);
    }
}
