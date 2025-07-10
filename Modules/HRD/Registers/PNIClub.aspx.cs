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

public partial class Registers_PNIClub : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_PNIClub_Message.Text = "";
        lblPNIClub.Text = "";

       
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
            BindGridPNIClub();

            Alerts.HidePanel(PNIClubpanel);
            Alerts.HANDLE_AUTHORITY(1, btn_PNIClub_add, btn_PNIClub_save, btn_PNIClub_Cancel, btn_Print_PNIClub, Auth);
        }
    }
    private void BindStatusDropDown()
    {
        DataTable dt2 = PNIClub.selectDataStatus();
        this.ddstatus_PNIClub.DataValueField = "StatusId";
        this.ddstatus_PNIClub.DataTextField = "StatusName";
        this.ddstatus_PNIClub.DataSource = dt2;
        this.ddstatus_PNIClub.DataBind();
    }
    private void BindGridPNIClub()
    {
        DataTable dt = PNIClub.selectDataPNIClubDetails();
        this.GvPNI.DataSource = dt;
        this.GvPNI.DataBind();
    }
    protected void GvPNI_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdpni;
        hfdpni = (HiddenField)GvPNI.Rows[GvPNI.SelectedIndex].FindControl("HiddenPNIClubId");
        id = Convert.ToInt32(hfdpni.Value.ToString());
        Show_Record_PNIClub(id);
     
        Alerts.ShowPanel(PNIClubpanel);
        Alerts.HANDLE_AUTHORITY(4, btn_PNIClub_add, btn_PNIClub_save, btn_PNIClub_Cancel, btn_Print_PNIClub, Auth);
    }
    protected void Show_Record_PNIClub(int PNIClibid)
    {
        HiddenPNIClubpk.Value = PNIClibid.ToString();
        DataTable dt3 = PNIClub.selectDataPNIClubDetailsByPNIClubId(PNIClibid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtPNIClubname.Text = dr["PNIClubName"].ToString();
            txtcreatedby_PNIClub.Text = dr["CreatedBy"].ToString();
            txtcreatedon_PNIClub.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_PNIClub.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_PNIClub.Text = dr["ModifiedOn"].ToString();
            ddstatus_PNIClub.SelectedValue = dr["StatusId"].ToString();
        }

    }
    protected void GvPNI_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdpni;
        hfdpni = (HiddenField)GvPNI.Rows[e.NewEditIndex].FindControl("HiddenPNIClubId");
        id = Convert.ToInt32(hfdpni.Value.ToString());
        Show_Record_PNIClub(id);
        GvPNI.SelectedIndex = e.NewEditIndex;
       
        Alerts.ShowPanel(PNIClubpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_PNIClub_add, btn_PNIClub_save, btn_PNIClub_Cancel, btn_Print_PNIClub, Auth);
    }
    protected void GvPNI_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvPNI.Rows[e.RowIndex].FindControl("HiddenPNIClubId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        PNIClub.deletePNIClubDetails("deletePNIClub", id, modifiedBy);
        BindGridPNIClub();
        if (HiddenPNIClubpk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_PNIClub_add_Click(sender, e);
        }
    }
    protected void GvPNI_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvPNI, Auth);
    }
    protected void GvPNI_PreRender(object sender, EventArgs e)
    {
        if (this.GvPNI.Rows.Count <= 0)
        {
            lblPNIClub.Text = "No Records Found..!";
        }
       
    }
    protected void btn_PNIClub_add_Click(object sender, EventArgs e)
    {
        HiddenPNIClubpk.Value = "";
        txtPNIClubname.Text = "";
        txtcreatedby_PNIClub.Text = "";
        txtcreatedon_PNIClub.Text = "";
        txtmodifiedby_PNIClub.Text = "";
        txtmodifiedon_PNIClub.Text = "";
        ddstatus_PNIClub.SelectedIndex = 0;
        GvPNI.SelectedIndex = -1;
       
        Alerts.ShowPanel(PNIClubpanel);
        Alerts.HANDLE_AUTHORITY(2, btn_PNIClub_add, btn_PNIClub_save, btn_PNIClub_Cancel, btn_Print_PNIClub, Auth);
     
    }
    protected void btn_PNIClub_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GvPNI.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenPNIClubName");
                hfd1 = (HiddenField)dg.FindControl("HiddenPNIClubId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtPNIClubname.Text.ToUpper().Trim())
                {
                    if (HiddenPNIClubpk.Value.Trim() == "")
                    {
                        lbl_PNIClub_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenPNIClubpk.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_PNIClub_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_PNIClub_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int pniclubId = -1;
                int createdby = 0, modifiedby = 0;
               
                string strpniclubName = txtPNIClubname.Text;

                char status = Convert.ToChar(ddstatus_PNIClub.SelectedValue);
                if (HiddenPNIClubpk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    pniclubId = Convert.ToInt32(HiddenPNIClubpk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                PNIClub.insertUpdatePNIClubDetails("InsertUpdatePNIClubDetails",
                                                       pniclubId,
                                                       strpniclubName,
                                                       createdby,
                                                       modifiedby,
                                                       status);
                BindGridPNIClub();
               
                lbl_PNIClub_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(PNIClubpanel);
                Alerts.HANDLE_AUTHORITY(3, btn_PNIClub_add, btn_PNIClub_save, btn_PNIClub_Cancel, btn_Print_PNIClub, Auth);
              
            }
    }
    protected void btn_PNIClub_Cancel_Click(object sender, EventArgs e)
    {
        GvPNI.SelectedIndex = -1;
      
        Alerts.HidePanel(PNIClubpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_PNIClub_add, btn_PNIClub_save, btn_PNIClub_Cancel, btn_Print_PNIClub, Auth);
      
    }
    protected void btn_Print_PNIClub_Click(object sender, EventArgs e)
    {

    }

    protected void GvPNI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdpni;
            hfdpni = (HiddenField)GvPNI.Rows[Rowindx].FindControl("hdnPNIClubId");
            id = Convert.ToInt32(hfdpni.Value.ToString());
            Show_Record_PNIClub(id);
            GvPNI.SelectedIndex = Rowindx;

            Alerts.ShowPanel(PNIClubpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_PNIClub_add, btn_PNIClub_save, btn_PNIClub_Cancel, btn_Print_PNIClub, Auth);
        }
    }

    protected void btnEditPNIClub_click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdpni;
        hfdpni = (HiddenField)GvPNI.Rows[Rowindx].FindControl("hdnPNIClubId");
        id = Convert.ToInt32(hfdpni.Value.ToString());
        Show_Record_PNIClub(id);
        GvPNI.SelectedIndex = Rowindx;

        Alerts.ShowPanel(PNIClubpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_PNIClub_add, btn_PNIClub_save, btn_PNIClub_Cancel, btn_Print_PNIClub, Auth);

    }
}
