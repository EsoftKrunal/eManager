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

public partial class Registers_NearestAirport : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_NearestAirport.Text = "";
        lbl_NearestAirport_Message.Text = "";
      
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
            bindNearestAirportGrid();
            bindCountryNameDDL();
            bindStatusDDL();
            Alerts.HidePanel(pnl_NearestAirport);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_NearestAirport, btn_Save_NearestAirport, btn_Cancel_NearestAirport, btn_Print_NearestAirport, Auth);
        }
    }
    public void bindNearestAirportGrid()
    {
        string s;
        s = txt_Airport.Text.Trim();  
        DataTable dt1 = NearestAirport.selectDataNearestAirportDetails(s);
        this.GridView_NearestAirport.DataSource = dt1;
        this.GridView_NearestAirport.DataBind();
    }
    public void bindCountryNameDDL()
    {
        DataTable dt4 = NearestAirport.selectDataCountryName();
        this.ddlCountryName.DataValueField = "CountryId";
        this.ddlCountryName.DataTextField = "CountryName";
        this.ddlCountryName.DataSource = dt4;
        this.ddlCountryName.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = NearestAirport.selectDataStatusDetails();
        this.ddlStatus_NearestAirport.DataValueField = "StatusId";
        this.ddlStatus_NearestAirport.DataTextField = "StatusName";
        this.ddlStatus_NearestAirport.DataSource = dt2;
        this.ddlStatus_NearestAirport.DataBind();
    }
    protected void btn_Add_NearestAirport_Click(object sender, EventArgs e)
    {
        ddlCountryName.SelectedIndex=0;
        txtNearestAirportName.Text = "";
        txtCreatedBy_NearestAirport.Text = "";
        txtCreatedOn_NearestAirport.Text = "";
        txtModifiedBy_NearestAirport.Text = "";
        txtModifiedOn_NearestAirport.Text = "";
        GridView_NearestAirport.SelectedIndex = -1;
        ddlStatus_NearestAirport.SelectedIndex = 0;
        HiddenNearestAirport.Value = "";
      
        Alerts.ShowPanel(pnl_NearestAirport);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_NearestAirport, btn_Save_NearestAirport, btn_Cancel_NearestAirport, btn_Print_NearestAirport, Auth);
    }
    protected void btn_Save_NearestAirport_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_NearestAirport.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenNearestAirportName");
                hfd1 = (HiddenField)dg.FindControl("HiddenNearestAirportId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtNearestAirportName.Text.ToUpper().Trim())
                {
                    if (HiddenNearestAirport.Value.Trim() == "")
                    {
                     
                        lbl_NearestAirport_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenNearestAirport.Value.Trim() != hfd1.Value.ToString())
                    {
                       
                        lbl_NearestAirport_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                  
                    lbl_NearestAirport_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intNearestAirportId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;

                if (HiddenNearestAirport.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intNearestAirportId = Convert.ToInt32(HiddenNearestAirport.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                int intCountryId = Convert.ToInt32(ddlCountryName.SelectedValue);
                string strNearestAirportName = txtNearestAirportName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_NearestAirport.SelectedValue);

                NearestAirport.insertUpdateNearestAirportDetails("InsertUpdateNearestAirportDetails",
                                              intNearestAirportId,
                                              intCountryId,
                                              strNearestAirportName,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindNearestAirportGrid();
                lbl_NearestAirport_Message.Text = "Record Successfully Saved.";
           
                Alerts.HidePanel(pnl_NearestAirport);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_NearestAirport, btn_Save_NearestAirport, btn_Cancel_NearestAirport, btn_Print_NearestAirport, Auth);
            }
       
    }
    protected void btn_Cancel_NearestAirport_Click(object sender, EventArgs e)
    {
        GridView_NearestAirport.SelectedIndex = -1;
      
        Alerts.HidePanel(pnl_NearestAirport);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_NearestAirport, btn_Save_NearestAirport, btn_Cancel_NearestAirport, btn_Print_NearestAirport, Auth);
    }
    protected void btn_Print_NearestAirport_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_NearestAirport_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_NearestAirport, Auth);
    }
    protected void Show_Record_NearestAirport(int NearestAirportid)
    {
        string Mess;

        Mess = "";
        HiddenNearestAirport.Value = NearestAirportid.ToString();
        DataTable dt3 = NearestAirport.selectDataNearestAirportDetailsByNearestAirportId(NearestAirportid);
        foreach (DataRow dr in dt3.Rows)
        {
        
            Mess= Mess + Alerts.Set_DDL_Value(ddlCountryName, dr["CountryId"].ToString(),"Country");

            txtNearestAirportName.Text = dr["NearestAirportName"].ToString();
            txtCreatedBy_NearestAirport.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_NearestAirport.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_NearestAirport.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_NearestAirport.Text = dr["ModifiedOn"].ToString();
            ddlStatus_NearestAirport.SelectedValue = dr["StatusId"].ToString();
        }
        if (Mess.Length>0)
        {
            this.lbl_NearestAirport_Message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            this.lbl_NearestAirport_Message.Visible = true;
        }
      
        
    }
 
    protected void GridView_NearestAirport_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        HiddenField hfdNearestAirport;
        hfdNearestAirport = (HiddenField)GridView_NearestAirport.Rows[GridView_NearestAirport.SelectedIndex].FindControl("HiddenNearestAirportId");
        id = Convert.ToInt32(hfdNearestAirport.Value.ToString());
        Show_Record_NearestAirport(id);
       
        Alerts.ShowPanel(pnl_NearestAirport);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_NearestAirport, btn_Save_NearestAirport, btn_Cancel_NearestAirport, btn_Print_NearestAirport, Auth);
    }
  
    protected void GridView_NearestAirport_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdNearestAirport;
        hfdNearestAirport = (HiddenField)GridView_NearestAirport.Rows[e.NewEditIndex].FindControl("HiddenNearestAirportId");
        id = Convert.ToInt32(hfdNearestAirport.Value.ToString());
        Show_Record_NearestAirport(id);
        GridView_NearestAirport.SelectedIndex = e.NewEditIndex;
      
        Alerts.ShowPanel(pnl_NearestAirport);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_NearestAirport, btn_Save_NearestAirport, btn_Cancel_NearestAirport, btn_Print_NearestAirport, Auth);
    }
  
    protected void GridView_NearestAirport_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdNearestAirport;
        hfdNearestAirport = (HiddenField)GridView_NearestAirport.Rows[e.RowIndex].FindControl("HiddenNearestAirportId");
        id = Convert.ToInt32(hfdNearestAirport.Value.ToString());
        NearestAirport.deleteNearestAirportDetailsById("DeleteNearestAirportById", id,modifiedby);
        bindNearestAirportGrid();
        if (HiddenNearestAirport.Value.ToString() == hfdNearestAirport.Value.ToString())
        {
            btn_Add_NearestAirport_Click(sender, e);
        }
    }
    protected void GridView_NearestAirport_PreRender(object sender, EventArgs e)
    {
        if (GridView_NearestAirport.Rows.Count <= 0) { lbl_GridView_NearestAirport.Text = "No Records Found..!"; }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        bindNearestAirportGrid();
    }

    protected void GridView_NearestAirport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdNearestAirport;
            hfdNearestAirport = (HiddenField)GridView_NearestAirport.Rows[Rowindx].FindControl("hdnNearestAirportId");
            id = Convert.ToInt32(hfdNearestAirport.Value.ToString());
            Show_Record_NearestAirport(id);
            GridView_NearestAirport.SelectedIndex = Rowindx;

            Alerts.ShowPanel(pnl_NearestAirport);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_NearestAirport, btn_Save_NearestAirport, btn_Cancel_NearestAirport, btn_Print_NearestAirport, Auth);
        }
        }

    protected void btnEditNearestAirport_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdNearestAirport;
        hfdNearestAirport = (HiddenField)GridView_NearestAirport.Rows[Rowindx].FindControl("hdnNearestAirportId");
        id = Convert.ToInt32(hfdNearestAirport.Value.ToString());
        Show_Record_NearestAirport(id);
        GridView_NearestAirport.SelectedIndex = Rowindx;

        Alerts.ShowPanel(pnl_NearestAirport);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_NearestAirport, btn_Save_NearestAirport, btn_Cancel_NearestAirport, btn_Print_NearestAirport, Auth);
    }
}
