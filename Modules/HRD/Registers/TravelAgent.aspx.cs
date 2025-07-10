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

public partial class Registers_TravelAgent : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_TravelAgent_Message.Text = "";
        lblTravel_agent.Text = "";
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
            BindStatusDropDown();
            BindGridTravelAgent();
            Alerts.HidePanel(travelagentpanel);
            Alerts.HANDLE_AUTHORITY(1, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);
       
        }
    }
   
    private void BindStatusDropDown()
    {
        DataTable dt2 = TravelAgent.selectDataStatus();
        this.ddstatus_travelagent.DataValueField = "StatusId";
        this.ddstatus_travelagent.DataTextField = "StatusName";
        this.ddstatus_travelagent.DataSource = dt2;
        this.ddstatus_travelagent.DataBind();
    }
    private void BindGridTravelAgent()
    {
        DataTable dt = TravelAgent.selectDataTravelAgentDetails();
        this.GvTravelAgent.DataSource = dt;
        this.GvTravelAgent.DataBind();
    }
    protected void GvTravelAgent_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvTravelAgent.Rows[GvTravelAgent.SelectedIndex].FindControl("HiddenTravelagentId");
        id = Convert.ToInt32(hfdrank.Value.ToString());
        Show_Record_TravelAgent(id);
        Alerts.ShowPanel(travelagentpanel);
        Alerts.HANDLE_AUTHORITY(4, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);     
  
    }
    protected void Show_Record_TravelAgent(int travelagentid)
    {
        HiddenTravelagentpk.Value = travelagentid.ToString();
        DataTable dt3 = TravelAgent.selectDataTravelAgentDetailsByTravelAgentId(travelagentid);
        foreach (DataRow dr in dt3.Rows)
        {
            txttravel_email.Text = dr["TravelAgentEmail"].ToString();
            txt_travel_company.Text = dr["Company"].ToString();
            txt_travel_contactperson.Text = dr["ContactPerson"].ToString();
            txt_travel_address.Text = dr["Address"].ToString();
            txt_travel_phone.Text = dr["Phone"].ToString();
            txt_travel_mobile.Text = dr["Mobile"].ToString();
            txt_VendorNo.Text = dr["VendorNo"].ToString();
            txtcreatedby_travelagent.Text = dr["CreatedBy"].ToString();
            txtcreatedon_travelagent.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_travelagent.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_travelagent.Text = dr["ModifiedOn"].ToString();
            ddstatus_travelagent.SelectedValue = dr["StatusId"].ToString();
            txtfax.Text = dr["faxno"].ToString();
        }
    }
    protected void GvTravelAgent_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdtravelagent;
        hfdtravelagent = (HiddenField)GvTravelAgent.Rows[e.NewEditIndex].FindControl("HiddenTravelagentId");
        id = Convert.ToInt32(hfdtravelagent.Value.ToString());
        Show_Record_TravelAgent(id);
        GvTravelAgent.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(travelagentpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);     
     
    }
    protected void GvTravelAgent_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvTravelAgent.Rows[e.RowIndex].FindControl("HiddenTravelagentId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        TravelAgent.deleteTravelAgentDetails("deleteTravelAgent", id, intModifiedBy);
        BindGridTravelAgent();
        if (HiddenTravelagentpk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_travelagent_add_Click(sender, e);
        }
    }
    protected void GvTravelAgent_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvTravelAgent, Auth);  
    }
    protected void GvTravelAgent_PreRender(object sender, EventArgs e)
    {
        if (this.GvTravelAgent.Rows.Count <= 0)
        {
            lblTravel_agent.Text = "No Records Found..!";
        }
       
    }
    protected void btn_travelagent_add_Click(object sender, EventArgs e)
    {
        HiddenTravelagentpk.Value = "";
        txttravel_email.Text = "";
        txt_travel_company.Text = "";
        txt_travel_contactperson.Text = "";
        txt_travel_address.Text = "";
        txt_travel_phone.Text = "";
        txt_travel_mobile.Text = "";
        txt_VendorNo.Text = ""; 
        txtfax.Text = "";
        txtcreatedby_travelagent.Text = "";
        GvTravelAgent.SelectedIndex = -1;
        txtcreatedon_travelagent.Text = "";
        txtmodifiedby_travelagent.Text = "";
        txtmodifiedon_travelagent.Text = "";
        ddstatus_travelagent.SelectedIndex = 0;
        Alerts.ShowPanel(travelagentpanel);
        Alerts.HANDLE_AUTHORITY(2, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);    
 
    }
    protected void btn_travelagent_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GvTravelAgent.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenCompany");
                hfd1 = (HiddenField)dg.FindControl("HiddenTravelagentId");

                if (hfd.Value.ToString().ToUpper().Trim() == txt_travel_company.Text.ToUpper().Trim())
                {
                    if (HiddenTravelagentpk.Value.Trim() == "")
                    {

                        lbl_TravelAgent_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenTravelagentpk.Value.Trim() != hfd1.Value.ToString())
                    {

                        lbl_TravelAgent_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {

                    lbl_TravelAgent_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int travelagentId = -1;
                int createdby = 0, modifiedby = 0;
              
                string TravelAgentEmail = txttravel_email.Text;
                string TravelAgentCompany = txt_travel_company.Text;
                string TravelAgentContactPerson = txt_travel_contactperson.Text;
                string TravelAgentAddress = txt_travel_address.Text;
                string TravelAgentPhone = txt_travel_phone.Text;
                string TravelAgentMobile = txt_travel_mobile.Text;
                string travelagentfax = txtfax.Text;
                string VendorNo = txt_VendorNo.Text; 
                char status = Convert.ToChar(ddstatus_travelagent.SelectedValue);
                if (HiddenTravelagentpk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    travelagentId = Convert.ToInt32(HiddenTravelagentpk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                TravelAgent.insertUpdateTravelAgentDetails("InsertUpdateTravelAgentDetails",
                                                              travelagentId,
                                                              TravelAgentEmail,
                                                              TravelAgentCompany,
                                                              TravelAgentContactPerson,
                                                              TravelAgentAddress,
                                                              TravelAgentPhone,
                                                              TravelAgentMobile,
                                                              travelagentfax,
                                                              VendorNo,
                                                              createdby,
                                                              modifiedby,
                                                              status);
                BindGridTravelAgent();
                lbl_TravelAgent_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(travelagentpanel);
                Alerts.HANDLE_AUTHORITY(3, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);    
           
            }
    }
    protected void btn_travelagent_Cancel_Click(object sender, EventArgs e)
    {
        
        GvTravelAgent.SelectedIndex = -1;
        Alerts.HidePanel(travelagentpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);     
   
    }
    protected void btn_Print_TravelAgent_Click(object sender, EventArgs e)
    {

    }

    protected void GvTravelAgent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdtravelagent;
            hfdtravelagent = (HiddenField)GvTravelAgent.Rows[Rowindx].FindControl("hdnTravelAgentId");
            id = Convert.ToInt32(hfdtravelagent.Value.ToString());
            Show_Record_TravelAgent(id);
            GvTravelAgent.SelectedIndex = Rowindx;
            Alerts.ShowPanel(travelagentpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);
        }
        }

    protected void btnEditTravelAgent_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdtravelagent;
        hfdtravelagent = (HiddenField)GvTravelAgent.Rows[Rowindx].FindControl("hdnTravelAgentId");
        id = Convert.ToInt32(hfdtravelagent.Value.ToString());
        Show_Record_TravelAgent(id);
        GvTravelAgent.SelectedIndex = Rowindx;
        Alerts.ShowPanel(travelagentpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);
    }
}
