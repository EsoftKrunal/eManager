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

public partial class Registers_PortAgent : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_PortAgent_Message.Text = "";
        lblPort_agent.Text = "";

      
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
            DataSet ds = Budget.getTable("SELECT * FROM PORT WHERE STATUSID='A' ORDER BY PORTNAME ");
            this.lblOtherPorts.DataValueField = "PortId";
            this.lblOtherPorts.DataTextField = "PortName";
            this.lblOtherPorts.DataSource = ds;
            this.lblOtherPorts.DataBind();

            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender,e);
            
            BindStatusDropDown();
            BindGridPortAgent();
            Alerts.HidePanel(Portagentpanel);
            Alerts.HANDLE_AUTHORITY(1, btn_Portagent_add, btn_Portagent_save, btn_Portagent_Cancel, btn_Print_PortAgent, Auth);
       
        }
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
    private void BindStatusDropDown()
    {
        DataTable dt2 = PortAgent.selectDataStatus();
        this.ddstatus_Portagent.DataValueField = "StatusId";
        this.ddstatus_Portagent.DataTextField = "StatusName";
        this.ddstatus_Portagent.DataSource = dt2;
        this.ddstatus_Portagent.DataBind();
    }
    private void BindGridPortAgent()
    {
        string s;
        s = txt_PortAgent.Text.Trim(); 
        DataTable dt = PortAgent.selectDataPortAgentDetails(s);
        this.GvPortAgent.DataSource = dt;
        this.GvPortAgent.DataBind();
    }
    protected void GvPortAgent_SelectIndexChanged(object sender, EventArgs e)
    {
   
        HiddenField hfdPort;
        hfdPort = (HiddenField)GvPortAgent.Rows[GvPortAgent.SelectedIndex].FindControl("HiddenPortagentId");
        id = Convert.ToInt32(hfdPort.Value.ToString());

        Show_Record_PortAgent(id);
        Alerts.ShowPanel(Portagentpanel);
        Alerts.HANDLE_AUTHORITY(4, btn_Portagent_add, btn_Portagent_save, btn_Portagent_Cancel, btn_Print_PortAgent, Auth);     
  
    }
    protected void Show_Record_PortAgent(int Portagentid)
    {
        string Mess;
        Mess = "";

        HiddenPortagentpk.Value = Portagentid.ToString();
        DataTable dt3 = PortAgent.selectDataPortAgentDetailsByPortAgentId(Portagentid);
        foreach (DataRow dr in dt3.Rows)
        {
           
            DataTable dtcountry = PortPlanner.selectCountry(Convert.ToInt32(dr["PortId"].ToString()));
            foreach (DataRow drr in dtcountry.Rows)
            {
                
                Mess = Mess + Alerts.Set_DDL_Value(ddlCountry, drr["CountryId"].ToString(), "Country");
                BindPortDropDown();
            }
          
            txtPort_email.Text = dr["PortAgentEmail"].ToString();
            txt_port_company.Text = dr["Company"].ToString();
            txt_port_contactperson.Text = dr["ContactPerson"].ToString();
            txt_port_address.Text = dr["Address"].ToString();
            txt_port_phone.Text = dr["Phone"].ToString();
            txt_port_mobile.Text = dr["Mobile"].ToString();
            txt_VendorNo.Text = dr["VendorNo"].ToString();
           
            Mess = Mess + Alerts.Set_DDL_Value(dd_port_portname, dr["PortId"].ToString(), "Port");
            txtcreatedby_Portagent.Text = dr["CreatedBy"].ToString();
            txtcreatedon_Portagent.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_Portagent.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_Portagent.Text = dr["ModifiedOn"].ToString();
            ddstatus_Portagent.SelectedValue = dr["StatusId"].ToString();
            txtfax.Text = dr["FaxNo"].ToString(); 
        }
        
        foreach ( ListItem li in lblOtherPorts.Items)
        {
            DataTable dt = Budget.getTable("select * from PortAgentPorts where PortAgentId=" + Portagentid.ToString() + " And PortId=" + li.Value).Tables[0];
            li.Selected = (dt.Rows.Count > 0); 
        }
        if (Mess.Length > 0)
        {
            this.lbl_PortAgent_Message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
    
        }
    }
    protected void GvPortAgent_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdPortagent;
        hfdPortagent = (HiddenField)GvPortAgent.Rows[e.NewEditIndex].FindControl("HiddenPortagentId");
        id = Convert.ToInt32(hfdPortagent.Value.ToString());
  
        Show_Record_PortAgent(id);
        GvPortAgent.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(Portagentpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Portagent_add, btn_Portagent_save, btn_Portagent_Cancel, btn_Print_PortAgent, Auth);     
     
    }
    protected void GvPortAgent_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvPortAgent.Rows[e.RowIndex].FindControl("HiddenPortagentId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        PortAgent.deletePortAgentDetails("deletePortAgent", id,modifiedby );
        BindGridPortAgent();
        if (HiddenPortagentpk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_Portagent_add_Click(sender, e);
        }
       
    }
    protected void GvPortAgent_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvPortAgent, Auth);  
    }
    protected void GvPortAgent_PreRender(object sender, EventArgs e)
    {
        if (this.GvPortAgent.Rows.Count <= 0)
        {
            lblPort_agent.Text = "No Records Found..!";
        }
        
    }
    protected void btn_Portagent_add_Click(object sender, EventArgs e)
    {

        HiddenPortagentpk.Value = "";
        txtPort_email.Text = "";
        txt_port_company.Text = "";
        txt_port_contactperson.Text = "";
        txt_port_address.Text = "";
        txt_port_phone.Text = "";
        txt_port_mobile.Text = "";
        txtfax.Text = "";
        txt_VendorNo.Text = "";
        dd_port_portname.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        txtcreatedby_Portagent.Text = "";
        txtcreatedon_Portagent.Text = "";
        txtmodifiedby_Portagent.Text = "";
        GvPortAgent.SelectedIndex = -1;
        txtmodifiedon_Portagent.Text = "";
        ddstatus_Portagent.SelectedIndex = 0;
        lblOtherPorts.ClearSelection(); 
        Alerts.ShowPanel(Portagentpanel);
        Alerts.HANDLE_AUTHORITY(2, btn_Portagent_add, btn_Portagent_save, btn_Portagent_Cancel, btn_Print_PortAgent, Auth);    
 
    }
    protected void btn_Print_PortAgent_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Portagent_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GvPortAgent.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenCompanyName");
                hfd1 = (HiddenField)dg.FindControl("HiddenPortagentId");

                if (hfd.Value.ToString().ToUpper().Trim() == txt_port_company.Text.ToUpper().Trim())
                {
                    if (HiddenPortagentpk.Value.Trim() == "")
                    {
             
                        lbl_PortAgent_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenPortagentpk.Value.Trim() != hfd1.Value.ToString())
                    {
                      
                        lbl_PortAgent_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                   
                    lbl_PortAgent_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int portagentId = -1;
                int loginid = 0;
                
                string portAgentEmail = txtPort_email.Text;
                string portAgentCompany = txt_port_company.Text + "-" + dd_port_portname.SelectedItem.Text;
                string portAgentContactPerson = txt_port_contactperson.Text;
                string portAgentAddress = txt_port_address.Text;
                string portAgentPhone = txt_port_phone.Text;
                string portAgentMobile = txt_port_mobile.Text;
                string portAgentFax = txtfax.Text;
                string vendorno=txt_VendorNo.Text;

                int portid = Convert.ToInt32(dd_port_portname.SelectedValue);
                char status = Convert.ToChar(ddstatus_Portagent.SelectedValue);
                loginid = Convert.ToInt32(Session["loginid"].ToString());
                if (HiddenPortagentpk.Value.Trim() != "")
                {
                    portagentId = Convert.ToInt32(HiddenPortagentpk.Value);
                }
                string ids = "";
                for (int i = 0; i <= lblOtherPorts.Items.Count - 1; i++)
                {
                    if(lblOtherPorts.Items[i].Selected)
                    {
                        ids = ids + "," + lblOtherPorts.Items[i].Value;      
                    }
                }
                if (ids.Trim() != "") { ids = ids.Substring(1); }  
                //------------------------------------------------
                PortAgent.insertUpdatePortAgentDetails_More(
                                                         portagentId,
                                                         portAgentEmail,
                                                         txt_port_company.Text,
                                                         portAgentContactPerson,
                                                         portAgentAddress,
                                                         portAgentPhone,
                                                         portAgentMobile,
                                                         portAgentFax,
                                                         vendorno,
                                                         status,
                                                         loginid,
                                                         ids);

                BindGridPortAgent();
                lbl_PortAgent_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(Portagentpanel);
                Alerts.HANDLE_AUTHORITY(3, btn_Portagent_add, btn_Portagent_save, btn_Portagent_Cancel, btn_Print_PortAgent, Auth);    
           
            }
    }
    protected void btn_Portagent_Cancel_Click(object sender, EventArgs e)
    {
        
        GvPortAgent.SelectedIndex = -1;
        Alerts.HidePanel(Portagentpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_Portagent_add, btn_Portagent_save, btn_Portagent_Cancel, btn_Print_PortAgent, Auth);     
   
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPortDropDown();
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGridPortAgent();
    }

    protected void GvPortAgent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdPortagent;
            hfdPortagent = (HiddenField)GvPortAgent.Rows[Rowindx].FindControl("hdnPortAgentId");
            id = Convert.ToInt32(hfdPortagent.Value.ToString());

            Show_Record_PortAgent(id);
            GvPortAgent.SelectedIndex = Rowindx;
            Alerts.ShowPanel(Portagentpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_Portagent_add, btn_Portagent_save, btn_Portagent_Cancel, btn_Print_PortAgent, Auth);
        }
        }

    protected void btnEditPortAgent_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdPortagent;
        hfdPortagent = (HiddenField)GvPortAgent.Rows[Rowindx].FindControl("hdnPortAgentId");
        id = Convert.ToInt32(hfdPortagent.Value.ToString());

        Show_Record_PortAgent(id);
        GvPortAgent.SelectedIndex = Rowindx;
        Alerts.ShowPanel(Portagentpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Portagent_add, btn_Portagent_save, btn_Portagent_Cancel, btn_Print_PortAgent, Auth);
    }
    }
