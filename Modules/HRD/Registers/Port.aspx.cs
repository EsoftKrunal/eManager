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

public partial class Registers_Port : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_Port.Text = "";
        lbl_Port_Message.Text = "";
     
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
            bindPortGrid();
            bindStatusDDL();
            bindCountryDDL();
            Alerts.HidePanel(pnl_Port);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_Port, btn_Save_Port, btn_Cancel_Port, btn_Print_Port, Auth);
        }
    }

    public void bindPortGrid()
    {
        string s;
        s = txt_port.Text.Trim(); 
        DataTable dt1 = Port.selectDataPortDetails(s);
        this.GridView_Port.DataSource = dt1;
        this.GridView_Port.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = Port.selectDataStatusDetails();
        this.ddlStatus_Port.DataValueField = "StatusId";
        this.ddlStatus_Port.DataTextField = "StatusName";
        this.ddlStatus_Port.DataSource = dt2;
        this.ddlStatus_Port.DataBind();
    }
    public void bindCountryDDL()
    {
        DataTable dt22 = Port.selectDataCountryDetails();
        ddl_P_Country.DataValueField = "CountryId";
        ddl_P_Country.DataTextField = "CountryName";
        ddl_P_Country.DataSource = dt22;
        ddl_P_Country.DataBind();
    }
    protected void btn_Add_Port_Click(object sender, EventArgs e)
    {
        txtPortName.Text = "";
        txtCreatedBy_Port.Text = "";
        txtCreatedOn_Port.Text = "";
        txtModifiedBy_Port.Text = "";
        ddl_P_Country.SelectedIndex = 0;
        txtModifiedOn_Port.Text = "";
        ddlStatus_Port.SelectedIndex=0;
        GridView_Port.SelectedIndex = -1;
        HiddenPort.Value = "";
        Alerts.ShowPanel(pnl_Port);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_Port, btn_Save_Port, btn_Cancel_Port, btn_Print_Port, Auth);    
 
    }
    protected void btn_Save_Port_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_Port.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenPortName");
                hfd1 = (HiddenField)dg.FindControl("HiddenPortId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtPortName.Text.ToUpper().Trim())
                {
                    if (HiddenPort.Value.Trim() == "")
                    {
                        lbl_Port_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenPort.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_Port_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_Port_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intportId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenPort.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intportId = Convert.ToInt32(HiddenPort.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strPortName = txtPortName.Text;
                char charStatusId = Convert.ToChar(ddlStatus_Port.SelectedValue);
                int countryid = Convert.ToInt32(ddl_P_Country.SelectedValue);

                Port.insertUpdatePortDetails("InsertUpdatePortDetails",
                                              intportId,
                                              countryid,
                                              strPortName,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindPortGrid();
                lbl_Port_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(pnl_Port);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_Port, btn_Save_Port, btn_Cancel_Port, btn_Print_Port, Auth);    
           
            }
      
    }
    protected void btn_Cancel_Port_Click(object sender, EventArgs e)
    {
        GridView_Port.SelectedIndex = -1;
        Alerts.HidePanel(pnl_Port);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_Port, btn_Save_Port, btn_Cancel_Port, btn_Print_Port, Auth);     
   
    }
    protected void btn_Print_Port_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_Port_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_Port, Auth);  
    }
    protected void Show_Record_Port(int portid)
    {
        string Mess = "";
        HiddenPort.Value = portid.ToString();
        DataTable dt3 = Port.selectDataPortDetailsByPortId(portid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtPortName.Text = dr["PortName"].ToString();

        
            Mess = Mess + Alerts.Set_DDL_Value(ddl_P_Country, dr["CountryId"].ToString(), "Country");
            txtCreatedBy_Port.Text=dr["CreatedBy"].ToString();
            txtCreatedOn_Port.Text=dr["CreatedOn"].ToString();
            txtModifiedBy_Port.Text=dr["ModifiedBy"].ToString();
            txtModifiedOn_Port.Text=dr["ModifiedOn"].ToString();
            ddlStatus_Port.SelectedValue = dr["StatusId"].ToString();
        }
        if (Mess.Length > 0)
        {
            this.lbl_Port_Message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }

    }
   
    protected void GridView_Port_SelectedIndexChanged(object sender, EventArgs e)
    {
     
        HiddenField hfdport;
        hfdport = (HiddenField)GridView_Port.Rows[GridView_Port.SelectedIndex].FindControl("HiddenPortId");
        id = Convert.ToInt32(hfdport.Value.ToString());
        Show_Record_Port(id);
        Alerts.ShowPanel(pnl_Port);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_Port, btn_Save_Port, btn_Cancel_Port, btn_Print_Port, Auth);     
  
    }
   
    protected void GridView_Port_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdport;
        hfdport = (HiddenField)GridView_Port.Rows[e.NewEditIndex].FindControl("HiddenPortId");
        id = Convert.ToInt32(hfdport.Value.ToString());
        Show_Record_Port(id);
        GridView_Port.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(pnl_Port);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Port, btn_Save_Port, btn_Cancel_Port, btn_Print_Port, Auth);     
     
    }
  
    protected void GridView_Port_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int modifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdport;
        hfdport = (HiddenField)GridView_Port.Rows[e.RowIndex].FindControl("HiddenPortId");
        id = Convert.ToInt32(hfdport.Value.ToString());
        Port.deletePortDetailsById("DeletePortDetailsById", id, modifiedBy);
        bindPortGrid();
        if (HiddenPort.Value.ToString() == hfdport.Value.ToString())
        {
            btn_Add_Port_Click(sender,e);   
        }
    }
    protected void GridView_Port_PreRender(object sender, EventArgs e)
    {
        if (GridView_Port.Rows.Count <= 0) { lbl_GridView_Port.Text = "No Records Found..!"; }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        bindPortGrid();
    }

    protected void GridView_Port_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdport;
            hfdport = (HiddenField)GridView_Port.Rows[Rowindx].FindControl("hdnPortId");
            id = Convert.ToInt32(hfdport.Value.ToString());
            Show_Record_Port(id);
            GridView_Port.SelectedIndex = Rowindx;
            Alerts.ShowPanel(pnl_Port);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_Port, btn_Save_Port, btn_Cancel_Port, btn_Print_Port, Auth);
        }
    }

    protected void btnEditPort_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdport;
        hfdport = (HiddenField)GridView_Port.Rows[Rowindx].FindControl("hdnPortId");
        id = Convert.ToInt32(hfdport.Value.ToString());
        Show_Record_Port(id);
        GridView_Port.SelectedIndex = Rowindx;
        Alerts.ShowPanel(pnl_Port);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Port, btn_Save_Port, btn_Cancel_Port, btn_Print_Port, Auth);
    }
}
