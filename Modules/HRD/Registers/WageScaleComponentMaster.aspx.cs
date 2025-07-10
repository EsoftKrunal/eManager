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

public partial class CrewOperation_WageScaleComponentMaster : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_WageScaleComponent.Text = "";
        lbl_WageScaleComponent_Message.Text = "";
         //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");

        }
        //******************* 
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            loadVessel(); 
            bindgridSearch();
            Alerts.HidePanel(pnl_WageScaleComponent);
            Alerts.HANDLE_AUTHORITY(1, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
        }
    }
   
    public void bindgridSearch()
    {
        DataTable dt2;
        dt2 = wagecomponent.w_componentgdsearch();
        GridView_WageScaleComponentMaster.DataSource = dt2;
        GridView_WageScaleComponentMaster.DataBind();
    }
    public void loadVessel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        chklst_Vessel.DataSource = ds.Tables[0];
        chklst_Vessel.DataTextField = "VesselName";
        chklst_Vessel.DataValueField = "VesselId";
        chklst_Vessel.DataBind();
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        txtComponentName.Text = "";
        HiddenWageScaleComponent.Value = "";
        txtTotalWorkHours.Text = "";
        ddlCurrency.SelectedIndex = 0;
        ddlTotalWorkHours.SelectedIndex = 0;
        Alerts.ShowPanel(pnl_WageScaleComponent);
        Alerts.HANDLE_AUTHORITY(2, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        GridView_WageScaleComponentMaster.SelectedIndex = -1;
        
        Alerts.HidePanel(pnl_WageScaleComponent);
        Alerts.HANDLE_AUTHORITY(6, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_WageScaleComponentMaster_DataBound(object sender, EventArgs e)
    {
        try
        {
           GridView_WageScaleComponentMaster.Columns[1].Visible = Auth.isEdit;
        }
        catch
        {
            GridView_WageScaleComponentMaster.Columns[1].Visible = false;
        }
    }
    protected void Show_Record_WageScaleComponent(int WageScaleid)
    {
        string Mess = "";
      
        HiddenWageScaleComponent.Value = WageScaleid.ToString();
        DataTable dt3 = wagecomponent.selectDataWageScaleComponentMasterDetailsByWageScaleId(WageScaleid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtComponentName.Text = dr["WageScaleName"].ToString();
            if (! string.IsNullOrWhiteSpace(dr["Currency"].ToString()))
            {
                ddlCurrency.SelectedValue = dr["Currency"].ToString();
            }
            else
            {
                ddlCurrency.SelectedIndex = 0;
            }
            if (!string.IsNullOrWhiteSpace(dr["WorkingHoursCategory"].ToString()))
            {
                ddlTotalWorkHours.SelectedValue = dr["WorkingHoursCategory"].ToString();
            }
            else
            {
                ddlTotalWorkHours.SelectedIndex = 0;
            }

            if (!string.IsNullOrWhiteSpace(dr["WorkingHours"].ToString()))
            {
                txtTotalWorkHours.Text = dr["WorkingHours"].ToString();
            }
            else
            {
                txtTotalWorkHours.Text = "";
            }

        }
       
    }
    protected void GridView_WageScaleComponentMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdWageScaleComponentMaster;
        hfdWageScaleComponentMaster = (HiddenField)GridView_WageScaleComponentMaster.Rows[GridView_WageScaleComponentMaster.SelectedIndex].FindControl("HiddenWageScaleComponentMasterId");
        id = Convert.ToInt32(hfdWageScaleComponentMaster.Value.ToString());
        Show_Record_WageScaleComponent(id);
        for (int i = 0; i < chklst_Vessel.Items.Count; i++)
        {
            Int32 VesselId = Convert.ToInt32(chklst_Vessel.Items[i].Value);
            chklst_Vessel.Items[i].Selected = VesselDetailsGeneral.Check_Vessels_InWageScale(id, VesselId);
        }
        Alerts.ShowPanel(pnl_WageScaleComponent);
        Alerts.HANDLE_AUTHORITY(4, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }
    protected void GridView_WageScaleComponentMaster_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdWageScaleComponentMaster;
        hfdWageScaleComponentMaster = (HiddenField)GridView_WageScaleComponentMaster.Rows[e.NewEditIndex].FindControl("HiddenWageScaleComponentMasterId");
        id = Convert.ToInt32(hfdWageScaleComponentMaster.Value.ToString());
        Show_Record_WageScaleComponent(id);
        GridView_WageScaleComponentMaster.SelectedIndex = e.NewEditIndex;
        for (int i = 0; i < chklst_Vessel.Items.Count; i++)
        {
            Int32 VesselId = Convert.ToInt32(chklst_Vessel.Items[i].Value);
            chklst_Vessel.Items[i].Selected = VesselDetailsGeneral.Check_Vessels_InWageScale(id, VesselId);
        }
        Alerts.ShowPanel(pnl_WageScaleComponent);
        Alerts.HANDLE_AUTHORITY(5, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }
    protected void GridView_WageScaleComponentMaster_PreRender(object sender, EventArgs e)
    {
        if (GridView_WageScaleComponentMaster.Rows.Count <= 0) { lbl_GridView_WageScaleComponent.Text = "No Records Found..!"; }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;
        if (! string.IsNullOrEmpty(txtTotalWorkHours.Text) && ddlTotalWorkHours.SelectedValue == "0")
        {
            lbl_WageScaleComponent_Message.Text = "Please select Total working hours category.";
            ddlTotalWorkHours.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtTotalWorkHours.Text) && ddlTotalWorkHours.SelectedValue != "0")
        {
            lbl_WageScaleComponent_Message.Text = "Please enter Total working hours.";
            txtTotalWorkHours.Focus();
            return;
        }
        foreach (GridViewRow dg in GridView_WageScaleComponentMaster.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("HiddenWageScaleName");
            hfd1 = (HiddenField)dg.FindControl("HiddenWageScaleComponentMasterId");
            if (hfd.Value.ToString().ToUpper().Trim() == txtComponentName.Text.ToUpper().Trim())
            {
                if (HiddenWageScaleComponent.Value.Trim() == "")
                {
                    
                    lbl_WageScaleComponent_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
                else if (HiddenWageScaleComponent.Value.Trim() != hfd1.Value.ToString())
                {
                    
                    lbl_WageScaleComponent_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
              
                lbl_WageScaleComponent_Message.Text = "";
            }
        }

        if (Duplicate == 0)
        {
            int wagescaleid = -1;
            int modifiedby = 0;
            if (HiddenWageScaleComponent.Value.ToString().Trim() == "")
            {
            }
            else
            {
                wagescaleid = Convert.ToInt32(HiddenWageScaleComponent.Value);
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
           // string wagescalename = txtComponentName.Text;
            char statusid = 'A';
            try
            {
                decimal totalworkinghrs = Convert.ToDecimal(txtTotalWorkHours.Text) > 0 ? Convert.ToDecimal(txtTotalWorkHours.Text) : 0 ;
                string totalworkinghrscat = ddlTotalWorkHours.SelectedValue != "0" ? ddlTotalWorkHours.SelectedValue : "";
                wagecomponent.updateWageName(wagescaleid, txtComponentName.Text, ddlCurrency.SelectedValue, modifiedby, statusid, totalworkinghrs, totalworkinghrscat);
                if (wagescaleid > 0)
                {
                    string datastr = "";
                    for (int count = 0; count < chklst_Vessel.Items.Count; count++)
                    {
                        if (chklst_Vessel.Items[count].Selected)
                        {
                            datastr = datastr + ',' + chklst_Vessel.Items[count].Value;
                        }
                    }
                if (datastr != "") { datastr = datastr.Substring(1); };
                WagesMaster.InsertVessels(wagescaleid, datastr);
                }
                
                bindgridSearch();
                lbl_WageScaleComponent_Message.Text = "Record Saved Successfully.";
               
                Alerts.HidePanel(pnl_WageScaleComponent);
                Alerts.HANDLE_AUTHORITY(3, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);

            }
            catch
            {
                bindgridSearch();
                lbl_WageScaleComponent_Message.Text = "Record Can't Saved.";
               
                Alerts.HidePanel(pnl_WageScaleComponent);
                Alerts.HANDLE_AUTHORITY(3, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);

            }
        }
      }



    protected void GridView_WageScaleComponentMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdWageScaleComId;
            hfdWageScaleComId = (HiddenField)GridView_WageScaleComponentMaster.Rows[Rowindx].FindControl("HiddenWageScaleComId");
            id = Convert.ToInt32(hfdWageScaleComId.Value.ToString());

            Show_Record_WageScaleComponent(id);
            GridView_WageScaleComponentMaster.SelectedIndex = Rowindx;
            for (int i = 0; i < chklst_Vessel.Items.Count; i++)
            {
                Int32 VesselId = Convert.ToInt32(chklst_Vessel.Items[i].Value);
                chklst_Vessel.Items[i].Selected = VesselDetailsGeneral.Check_Vessels_InWageScale(id, VesselId);
            }
            Alerts.ShowPanel(pnl_WageScaleComponent);
            Alerts.HANDLE_AUTHORITY(5, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
        }
    }

    protected void GridView_WageScaleComponentMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void btnEditWageScaleComponent_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdWageScaleComId;
        hfdWageScaleComId = (HiddenField)GridView_WageScaleComponentMaster.Rows[Rowindx].FindControl("HiddenWageScaleComId");
        id = Convert.ToInt32(hfdWageScaleComId.Value.ToString());

        Show_Record_WageScaleComponent(id);
        GridView_WageScaleComponentMaster.SelectedIndex = Rowindx;
        for (int i = 0; i < chklst_Vessel.Items.Count; i++)
        {
            Int32 VesselId = Convert.ToInt32(chklst_Vessel.Items[i].Value);
            chklst_Vessel.Items[i].Selected = VesselDetailsGeneral.Check_Vessels_InWageScale(id, VesselId);
        }
        Alerts.ShowPanel(pnl_WageScaleComponent);
        Alerts.HANDLE_AUTHORITY(5, btn_Add, btn_Save, btn_Cancel, btn_Print, Auth);
    }
}


