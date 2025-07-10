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

public partial class CrewAccounting_Supplier : System.Web.UI.Page
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
            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender,e);
          
            BindGridSupplier();
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
    private void bindcountrynameddl()
    {
        DataTable dt3 = PortPlanner.selectCountryName();
        this.ddlCountry.DataValueField = "CountryId";
        this.ddlCountry.DataTextField = "CountryName";
        this.ddlCountry.DataSource = dt3;
        this.ddlCountry.DataBind();
    }
    private void BindPortDropDown()
    {
        DataTable dt12 = PortAgent.selectPortName(Convert.ToInt32(ddlCountry.SelectedValue));
        this.dd_port_portname.DataValueField = "PortId";
        this.dd_port_portname.DataTextField = "PortName";
        this.dd_port_portname.DataSource = dt12;
        this.dd_port_portname.DataBind();
    }
    private void BindGridSupplier()
    {
        string s;
        DataView dv;
        s=txt_Supplier.Text.Trim();
        DataTable dt = Supplier.selectSupplierDetails(s);
        dv = dt.DefaultView;
        dv.Sort="Company";
        this.GvTravelAgent.DataSource = dv;
        this.GvTravelAgent.DataBind();

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
        txtvendorno.Text = "";
        txtfax.Text = "";
        txtcreatedby_travelagent.Text = "";
        txtcreatedon_travelagent.Text = "";
        txtmodifiedby_travelagent.Text = "";
        txtmodifiedon_travelagent.Text = "";
        ddstatus_travelagent.SelectedIndex = 0;
        dd_port_portname.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
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
                hfd = (HiddenField)dg.FindControl("HiddenField1");
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
                int SupplierId = -1;
                int createdby = 0, modifiedby = 0;
                
                string SupplierEmail = txttravel_email.Text;
                string SupplierCompany = txt_travel_company.Text;
                string SupplierContactPerson = txt_travel_contactperson.Text;
                string SupplierAddress = txt_travel_address.Text;
                string SupplierPhone = txt_travel_phone.Text;
                string SupplierMobile = txt_travel_mobile.Text;
                string SupplierFax = txtfax.Text;
                int portid=Convert.ToInt32(this.dd_port_portname.SelectedValue);
                string vendorno = txtvendorno.Text;
                char status = Convert.ToChar(ddstatus_travelagent.SelectedValue);
                if (HiddenTravelagentpk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                    if (Chk_Company(SupplierCompany) == true)
                    {
                        lbl_TravelAgent_Message.Visible = true;
                        lbl_TravelAgent_Message.Text = "Company Already Exists.";
                        return;
                    }
                }
                else
                {
                    SupplierId = Convert.ToInt32(HiddenTravelagentpk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }


                Supplier.insertUpdateSupplierDetails("InsertUpdateSupplierDetails",
                                                              SupplierId,
                                                              SupplierEmail,
                                                              SupplierCompany,
                                                              SupplierContactPerson,
                                                              SupplierAddress,
                                                              SupplierPhone,
                                                              SupplierMobile,
                                                              SupplierFax,
                                                              portid,
                                                              vendorno,
                                                              createdby,
                                                              modifiedby,
                                                              status);
                BindGridSupplier();
                lbl_TravelAgent_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(travelagentpanel);
                Alerts.HANDLE_AUTHORITY(3, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);    
           
            }
    }
    private bool Chk_Company(string company)
    {
        bool functionReturnValue = false;
               
        DataTable dtt2 = Supplier.selectDataSupplierCompany(company);
        if (dtt2.Rows.Count > 0)
        {
            functionReturnValue = true;
        }
        else
        {
            functionReturnValue = false;

        }
        return functionReturnValue;
      
       
       
    } 
    protected void btn_travelagent_Cancel_Click(object sender, EventArgs e)
    {
        
        GvTravelAgent.SelectedIndex = -1;
        Alerts.HidePanel(travelagentpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);     
   
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
    protected void GvTravelAgent_Row_Editing(object sender, GridViewEditEventArgs e)
    {
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
        Supplier.deleteSupplierDetails("deleteSupplier", id, intModifiedBy);
        BindGridSupplier();
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
    protected void Show_Record_TravelAgent(int travelagentid)
    {
          string Mess;
        Mess = "";

        HiddenTravelagentpk.Value = travelagentid.ToString();
        DataTable dt3 = Supplier.selectDataTravelAgentDetailsByTravelAgentId(travelagentid);
        foreach (DataRow dr in dt3.Rows)
        {
            //****** To Get Country According To Port
            DataTable dtcountry = PortPlanner.selectCountry(Convert.ToInt32(dr["PortId"].ToString()));
            foreach (DataRow drr in dtcountry.Rows)
            {
              
                Mess = Mess + Alerts.Set_DDL_Value(ddlCountry,  drr["CountryId"].ToString(), "Country");
                BindPortDropDown();
            }
            //***********
            txttravel_email.Text = dr["SupplierEmail"].ToString();
            txt_travel_company.Text = dr["Company"].ToString();
            txt_travel_contactperson.Text = dr["ContactPerson"].ToString();
            txt_travel_address.Text = dr["Address"].ToString();
            txt_travel_phone.Text = dr["Phone"].ToString();
            txt_travel_mobile.Text = dr["Mobile"].ToString();
            txtfax.Text = dr["FaxNo"].ToString();
            txtvendorno.Text = dr["Vendorno"].ToString();
           
            Mess = Mess + Alerts.Set_DDL_Value(dd_port_portname, dr["portid"].ToString(), "Port");
            txtcreatedby_travelagent.Text = dr["CreatedBy"].ToString();
            txtcreatedon_travelagent.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_travelagent.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_travelagent.Text = dr["ModifiedOn"].ToString();
            ddstatus_travelagent.SelectedValue = dr["StatusId"].ToString();
        }
        if (Mess.Length > 0)
        {
            this.lbl_TravelAgent_Message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }
    }
    protected void btn_Print_TravelAgent_Click(object sender, EventArgs e)
    {

    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPortDropDown();
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGridSupplier();
    }

    protected void GvTravelAgent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdtravelagent;
            hfdtravelagent = (HiddenField)GvTravelAgent.Rows[Rowindx].FindControl("HiddenTravelagentId");
            id = Convert.ToInt32(hfdtravelagent.Value.ToString());
            Show_Record_TravelAgent(id);
            GvTravelAgent.SelectedIndex = Rowindx;
            Alerts.ShowPanel(travelagentpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);
        }
    }

    protected void btnEditSupplier_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdtravelagent;
        hfdtravelagent = (HiddenField)GvTravelAgent.Rows[Rowindx].FindControl("HiddenTravelagentId");
        id = Convert.ToInt32(hfdtravelagent.Value.ToString());
        Show_Record_TravelAgent(id);
        GvTravelAgent.SelectedIndex = Rowindx;
        Alerts.ShowPanel(travelagentpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_travelagent_add, btn_travelagent_save, btn_travelagent_Cancel, btn_Print_TravelAgent, Auth);
    }
}

