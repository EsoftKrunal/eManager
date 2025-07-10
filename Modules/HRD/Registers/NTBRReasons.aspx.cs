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

public partial class Registers_NTBRReasons : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_ntbrresons_Message.Text = "";
        lblntbrresons.Text = "";
       
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
            BindGridNTBRReasons();
            Alerts.HidePanel(NTBRReasonspanel);
            Alerts.HANDLE_AUTHORITY(1, btn_ntbr_reason_add, btn_ntbr_reason_save, btn_ntbr_reason_Cancel, btn_Print_NTBRReasons, Auth);
       
        }
    }
  
    private void BindStatusDropDown()
    {
        DataTable dt2 = NTBRReasons.selectDataStatus();
        this.ddstatus_ntbr_reason.DataValueField = "StatusId";
        this.ddstatus_ntbr_reason.DataTextField = "StatusName";
        this.ddstatus_ntbr_reason.DataSource = dt2;
        this.ddstatus_ntbr_reason.DataBind();
    }
    private void BindGridNTBRReasons()
    {
        DataTable dt = NTBRReasons.selectDataNTBRReasonsDetails();
        this.GvNTBR_reasons.DataSource = dt;
        this.GvNTBR_reasons.DataBind();
    }
    protected void GvNTBR_reasons_SelectIndexChanged(object sender, EventArgs e)
    {
       
        HiddenField hfdNTBR;
        hfdNTBR = (HiddenField)GvNTBR_reasons.Rows[GvNTBR_reasons.SelectedIndex].FindControl("HiddenNTBRReasonId");
        id = Convert.ToInt32(hfdNTBR.Value.ToString());

        Show_Record_NTBR_reasons(id);
        Alerts.ShowPanel(NTBRReasonspanel);
        Alerts.HANDLE_AUTHORITY(4, btn_ntbr_reason_add, btn_ntbr_reason_save, btn_ntbr_reason_Cancel, btn_Print_NTBRReasons, Auth);     
  
    }
    protected void Show_Record_NTBR_reasons(int ntbrreasonid)
    {
        HiddenNTBRReasonspk.Value = ntbrreasonid.ToString();
        DataTable dt3 = NTBRReasons.selectDataNTBRReasonsDetailsByNTBRReasonId(ntbrreasonid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtntbrreasonname.Text = dr["NTBRReasonName"].ToString();
            txtcreatedby_ntbr_reason.Text = dr["CreatedBy"].ToString();
            txtcreatedon_ntbr_reason.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_ntbr_reason.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_ntbr_reason.Text = dr["ModifiedOn"].ToString();
            ddstatus_ntbr_reason.SelectedValue = dr["StatusId"].ToString();
        }
    }
    protected void GvNTBR_reasons_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdNTBR;
        hfdNTBR = (HiddenField)GvNTBR_reasons.Rows[e.NewEditIndex].FindControl("HiddenNTBRReasonId");
        id = Convert.ToInt32(hfdNTBR.Value.ToString());
        Show_Record_NTBR_reasons(id);
        GvNTBR_reasons.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(NTBRReasonspanel);
        Alerts.HANDLE_AUTHORITY(5, btn_ntbr_reason_add, btn_ntbr_reason_save, btn_ntbr_reason_Cancel, btn_Print_NTBRReasons, Auth);     
     
    }
    protected void GvNTBR_reasons_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvNTBR_reasons.Rows[e.RowIndex].FindControl("HiddenNTBRReasonId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        NTBRReasons.deleteNTBRReasonDetails("deleteNTBRReason", id,modifiedby);
        BindGridNTBRReasons();
        if (HiddenNTBRReasonspk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_ntbr_reason_add_Click(sender, e);
        }
    }
    protected void GvNTBR_reasons_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvNTBR_reasons, Auth);  
    }
    protected void GvNTBR_reasons_PreRender(object sender, EventArgs e)
    {
        if (this.GvNTBR_reasons.Rows.Count <= 0)
        {
            lblntbrresons.Text = "No Records Found..!";
        }
       
    }
    protected void btn_ntbr_reason_add_Click(object sender, EventArgs e)
    {
        HiddenNTBRReasonspk.Value = "";
        txtntbrreasonname.Text = "";
        txtcreatedby_ntbr_reason.Text = "";
        txtcreatedon_ntbr_reason.Text = "";
        txtmodifiedby_ntbr_reason.Text = "";
        txtmodifiedon_ntbr_reason.Text = "";
        ddstatus_ntbr_reason.SelectedIndex = 0;
        GvNTBR_reasons.SelectedIndex = -1;
        Alerts.ShowPanel(NTBRReasonspanel);
        Alerts.HANDLE_AUTHORITY(2, btn_ntbr_reason_add, btn_ntbr_reason_save, btn_ntbr_reason_Cancel, btn_Print_NTBRReasons, Auth);    
 
    }
    protected void btn_Print_NTBRReasons_Click(object sender, EventArgs e)
    {

    }
    protected void btn_ntbr_reason_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GvNTBR_reasons.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenNTBRReasonName");
                hfd1 = (HiddenField)dg.FindControl("HiddenNTBRReasonId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtntbrreasonname.Text.ToUpper().Trim())
                {
                    if (HiddenNTBRReasonspk.Value.Trim() == "")
                    {
                        lbl_ntbrresons_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenNTBRReasonspk.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_ntbrresons_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_ntbrresons_Message.Text = "Already Entered.";
                }
            }
            if (Duplicate == 0)
            {
                int ntbrreasonId = -1;
                int createdby = 0, modifiedby = 0;
               
                string strntbrreasonName = txtntbrreasonname.Text;

                char status = Convert.ToChar(ddstatus_ntbr_reason.SelectedValue);
                if (HiddenNTBRReasonspk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    ntbrreasonId = Convert.ToInt32(HiddenNTBRReasonspk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                NTBRReasons.insertUpdateNTBRReasonDetails("InsertUpdateNTBRReasonDetails",
                                                              ntbrreasonId,
                                                              strntbrreasonName,
                                                              createdby,
                                                              modifiedby,
                                                              status);
                BindGridNTBRReasons();
                lbl_ntbrresons_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(NTBRReasonspanel);
                Alerts.HANDLE_AUTHORITY(3, btn_ntbr_reason_add, btn_ntbr_reason_save, btn_ntbr_reason_Cancel, btn_Print_NTBRReasons, Auth);    
           
            }
    }
    protected void btn_ntbr_reason_Cancel_Click(object sender, EventArgs e)
    {
        
        GvNTBR_reasons.SelectedIndex = -1;
        Alerts.HidePanel(NTBRReasonspanel);
        Alerts.HANDLE_AUTHORITY(6, btn_ntbr_reason_add, btn_ntbr_reason_save, btn_ntbr_reason_Cancel, btn_Print_NTBRReasons, Auth);     
   
    }

    protected void GvNTBR_reasons_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdNTBR;
            hfdNTBR = (HiddenField)GvNTBR_reasons.Rows[Rowindx].FindControl("hdnNTBRReasonId");
            id = Convert.ToInt32(hfdNTBR.Value.ToString());
            Show_Record_NTBR_reasons(id);
            GvNTBR_reasons.SelectedIndex = Rowindx;
            Alerts.ShowPanel(NTBRReasonspanel);
            Alerts.HANDLE_AUTHORITY(5, btn_ntbr_reason_add, btn_ntbr_reason_save, btn_ntbr_reason_Cancel, btn_Print_NTBRReasons, Auth);
        }
    }
    protected void btnEditNTBRReason_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdNTBR;
        hfdNTBR = (HiddenField)GvNTBR_reasons.Rows[Rowindx].FindControl("hdnNTBRReasonId");
        id = Convert.ToInt32(hfdNTBR.Value.ToString());
        Show_Record_NTBR_reasons(id);
        GvNTBR_reasons.SelectedIndex = Rowindx;
        Alerts.ShowPanel(NTBRReasonspanel);
        Alerts.HANDLE_AUTHORITY(5, btn_ntbr_reason_add, btn_ntbr_reason_save, btn_ntbr_reason_Cancel, btn_Print_NTBRReasons, Auth);
    }
}
