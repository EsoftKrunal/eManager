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

 

public partial class Registers_CountryDetails : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblcountry.Text = "";
        this.lbl_Country_Message.Text = "";
       
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
            bindgrid();
            bindStatusDDL();
      
        }
      
        Alerts.HidePanel(pnl_country);
        Alerts.HANDLE_AUTHORITY(1, btn_Add, btn_Save, btn_cancel, btn_Print, Auth);     
    }
    
    private void bindgrid()
    {       
        gvDocument.DataSource = CountryDetails.selectcountrydetails() ;
        gvDocument.DataBind();
    }

    protected void gvDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        int j;
        j = gvDocument.SelectedIndex;
        showdata(j);
       trfields.Visible = true;
     
       Alerts.ShowPanel(pnl_country);
       Alerts.HANDLE_AUTHORITY(4, btn_Add, btn_Save,btn_cancel,btn_Print, Auth);     
         
       
    }
    private void showdata(int i)
    {
       
        this.txtcountrycode.Text = this.gvDocument.Rows[i].Cells[3].Text;
        this.txtcountryname.Text = this.gvDocument.Rows[i].Cells[4].Text;
        this.txtnationalitycode.Text = this.gvDocument.Rows[i].Cells[4].Text;
        this.txtnationalityname.Text = this.gvDocument.Rows[i].Cells[5].Text;
        //this.txtcreatedby.Text = this.gvDocument.Rows[i].Cells[6].Text;
        //this.txtcreatedon.Text = this.gvDocument.Rows[i].Cells[7].Text;
        //this.txtmodifiedby.Text = this.gvDocument.Rows[i].Cells[8].Text;
        //this.txtmodifiedon.Text = this.gvDocument.Rows[i].Cells[9].Text;
        Label lid = ((Label)this.gvDocument.Rows[i].FindControl("lblsname"));
                
        Label lcby = ((Label)this.gvDocument.Rows[i].FindControl("lblcby"));
        Label lcon = ((Label)this.gvDocument.Rows[i].FindControl("lblcon"));
        Label lmby = ((Label)this.gvDocument.Rows[i].FindControl("lblmby"));
        Label lmon = ((Label)this.gvDocument.Rows[i].FindControl("lblmon"));
        this.txtcreatedby.Text = lcby.Text;
        this.txtcreatedon.Text = lcon.Text;
        this.txtmodifiedby.Text = lmby.Text;
        this.txtmodifiedon.Text = lmon.Text;
        if (lid.Text == null)
        {
            this.ddlStatus_Country.SelectedValue = lid.Text;
        }
        else
        {
            this.ddlStatus_Country.SelectedIndex = 0;
        }
       
    }

    protected void gvDocument_RowEditing(object sender, GridViewEditEventArgs e)
    {
      
        int j;
        j = e.NewEditIndex;
        showdata(j);
        gvDocument.SelectedIndex = j;
       
        trfields.Visible = true;
        hcid.Value = gvDocument.DataKeys[j].Value.ToString();
       
        Alerts.ShowPanel(pnl_country);
        Alerts.HANDLE_AUTHORITY(5, btn_Add, btn_Save, btn_cancel, btn_Print, Auth);     
            
      
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        
        trfields.Visible = true;
        this.txtcountrycode.Text = "";
        this.txtcountryname.Text = "";
        this.txtnationalitycode.Text = "";
        this.txtnationalityname.Text = "";
        gvDocument.SelectedIndex = -1;
        this.txtcreatedby.Text = "";
        this.txtcreatedon.Text = "";
        this.txtmodifiedby.Text = "";
        this.txtmodifiedon.Text = "";
        this.hcid.Value = "";
        this.ddlStatus_Country.SelectedIndex = 0;
        //----------------------
        Alerts.ShowPanel(pnl_country);
        Alerts.HANDLE_AUTHORITY(2,this.btn_Add,this.btn_Save,this.btn_cancel,this.btn_Print, Auth);    
        
        
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in gvDocument.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenCountryName");
                hfd1 = (HiddenField)dg.FindControl("HiddenCountryId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtcountryname.Text.ToUpper().Trim())
                {
                    if (hcid.Value.Trim() == "")
                    {
                       
                        lbl_Country_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (hcid.Value.Trim() != hfd1.Value.ToString())
                    {
                       
                        lbl_Country_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    
                    lbl_Country_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
              
                int id, createdby, modifiedby;
                string ccode, cname, ncode, nname;
                try
                {
                    if (this.hcid.Value == "")
                    {
                        id = -1;
                        createdby = Convert.ToInt32(Session["loginid"].ToString());
                        modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                    }
                    else
                    {
                        id = Convert.ToInt32(this.hcid.Value); ;
                        createdby = Convert.ToInt32(Session["loginid"].ToString());
                        modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                    }
                    ccode = this.txtcountrycode.Text;
                    cname = this.txtcountryname.Text;
                    ncode = this.txtnationalitycode.Text;
                    nname = this.txtnationalityname.Text;
                    CountryDetails.insertupdatecountry("InsertUpdateCountryDetails", id, ccode, cname, ncode, nname, createdby, modifiedby, Convert.ToChar(this.ddlStatus_Country.SelectedValue));



                }
                catch (Exception ex)
                {
                  
                }
                bindgrid();

               
                                        
                lbl_Country_Message.Text = "Record Successfully Saved.";
              
                Alerts.HidePanel(this.pnl_country);
                Alerts.HANDLE_AUTHORITY(3, btn_Add,btn_Save, btn_cancel, btn_Print, Auth);    
            }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        trfields.Visible = false;
        gvDocument.SelectedIndex = -1;
       
        Alerts.HidePanel(pnl_country);
        Alerts.HANDLE_AUTHORITY(6, btn_Add, btn_Save, btn_cancel, btn_Print, Auth);     
    }
    protected void gvDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Alerts.HANDLE_GRID(gvDocument, Auth);  
    }
    protected void gvDocument_PreRender(object sender, EventArgs e)
    {
        if (this.gvDocument.Rows.Count <= 0)
        {
            lblcountry.Text = "No Records Found..!";
        }
        else
        {
            lblcountry.Text = "";
        }
    }
    protected void gvDocument_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedby = Convert.ToInt32(Session["loginid"].ToString());
       int  id = Convert.ToInt32(gvDocument.DataKeys[e.RowIndex].Value.ToString());
       CountryDetails.deleteCountryById("DeleteCountryDetailsIyId", id, modifiedby);
        bindgrid(); 
        btn_Add_Click(sender, e);
        
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = CostCentre.selectDataStatusDetails();
        this.ddlStatus_Country.DataValueField = "StatusId";
        this.ddlStatus_Country.DataTextField = "StatusName";
        this.ddlStatus_Country.DataSource = dt2;
        this.ddlStatus_Country.DataBind();
    }

    protected void gvDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";

            showdata(Rowindx);
            gvDocument.SelectedIndex = Rowindx;

            trfields.Visible = true;
            hcid.Value = gvDocument.DataKeys[Rowindx].Value.ToString();

            Alerts.ShowPanel(pnl_country);
            Alerts.HANDLE_AUTHORITY(5, btn_Add, btn_Save, btn_cancel, btn_Print, Auth);
        }
        }

    protected void btnEditCountry_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";

        showdata(Rowindx);
        gvDocument.SelectedIndex = Rowindx;

        trfields.Visible = true;
        hcid.Value = gvDocument.DataKeys[Rowindx].Value.ToString();

        Alerts.ShowPanel(pnl_country);
        Alerts.HANDLE_AUTHORITY(5, btn_Add, btn_Save, btn_cancel, btn_Print, Auth);
    }
}
