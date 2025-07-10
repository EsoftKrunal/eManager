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

public partial class Registers_Department : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_Department_Message.Text = "";
        lblDepartment.Text = "";

       
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
            BindGridDepartment();
            Alerts.HidePanel(Departmentpanel);
            Alerts.HANDLE_AUTHORITY(1, btn_Department_add, btn_Department_save, btn_Department_Cancel, btn_Print_Department, Auth);
       
        }
    }
 
    private void BindStatusDropDown()
    {
        DataTable dt2 = Department.selectDataStatus();
        this.ddstatus_Department.DataValueField = "StatusId";
        this.ddstatus_Department.DataTextField = "StatusName";
        this.ddstatus_Department.DataSource = dt2;
        this.ddstatus_Department.DataBind();
    }
    private void BindGridDepartment()
    {
        DataTable dt = Department.selectDataDepartmentDetails();
        this.GvDepartment.DataSource = dt;
        this.GvDepartment.DataBind();
    }
    protected void GvDepartment_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdDepartment;
        hfdDepartment = (HiddenField)GvDepartment.Rows[GvDepartment.SelectedIndex].FindControl("HiddenDepartmentId");
        id = Convert.ToInt32(hfdDepartment.Value.ToString());
        Show_Record_Department(id);
        Alerts.ShowPanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(4, btn_Department_add, btn_Department_save, btn_Department_Cancel, btn_Print_Department, Auth);     
  
    }
    protected void Show_Record_Department(int departmentid)
    {

        HiddenDepartmentpk.Value = departmentid.ToString();
        DataTable dt3 = Department.selectDataDepartmentDetailsByDepartmentId(departmentid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtDepartmentname.Text = dr["DepartmentName"].ToString();
            txtcreatedby_Department.Text = dr["CreatedBy"].ToString();
            txtcreatedon_Department.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_Department.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_Department.Text = dr["ModifiedOn"].ToString();
            ddstatus_Department.SelectedValue = dr["StatusId"].ToString();
        }
      
    }
    protected void GvDepartment_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvDepartment.Rows[e.NewEditIndex].FindControl("HiddenDepartmentId");
        id = Convert.ToInt32(hfdrank.Value.ToString());
      
        Show_Record_Department(id);
        GvDepartment.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Department_add, btn_Department_save, btn_Department_Cancel, btn_Print_Department, Auth);     
     
    }
    protected void GvDepartment_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfddel;
        hfddel = (HiddenField)GvDepartment.Rows[e.RowIndex].FindControl("HiddenDepartmentId");
        id = Convert.ToInt32(hfddel.Value.ToString());
        Department.deleteDepartmentDetails("deleteDepartment", id, intModifiedBy);
        BindGridDepartment();
        if (HiddenDepartmentpk.Value.Trim() == hfddel.Value.ToString())
        {
            btn_Department_add_Click(sender, e);
        }
    }
    protected void GvDepartment_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvDepartment, Auth);  
    }
    protected void GvDepartment_PreRender(object sender, EventArgs e)
    {
        if (this.GvDepartment.Rows.Count <= 0)
        {
            lblDepartment.Text = "No Records Found..!";
        }
       
    }
    protected void btn_Department_add_Click(object sender, EventArgs e)
    {
        HiddenDepartmentpk.Value = "";
        txtDepartmentname.Text = "";
        txtcreatedby_Department.Text = "";
        GvDepartment.SelectedIndex = -1;
        txtcreatedon_Department.Text = "";
        txtmodifiedby_Department.Text = "";
        txtmodifiedon_Department.Text = "";
        ddstatus_Department.SelectedIndex = 0;
        Alerts.ShowPanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(2, btn_Department_add, btn_Department_save, btn_Department_Cancel, btn_Print_Department, Auth);    
 
    }
    protected void btn_Department_save_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GvDepartment.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenDepartmentName");
                hfd1 = (HiddenField)dg.FindControl("HiddenDepartmentId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtDepartmentname.Text.ToUpper().Trim())
                {
                    if (HiddenDepartmentpk.Value.Trim() == "")
                    {
                        lbl_Department_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenDepartmentpk.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_Department_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_Department_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int departmentId = -1;
                int createdby = 0, modifiedby = 0;
               
                string strdepartmentName = txtDepartmentname.Text;

                char status = Convert.ToChar(ddstatus_Department.SelectedValue);
                if (HiddenDepartmentpk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    departmentId = Convert.ToInt32(HiddenDepartmentpk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                Department.insertUpdateDepartmentDetails("InsertUpdateDepartmentDetails",
                                                          departmentId,
                                                          strdepartmentName,
                                                          createdby,
                                                          modifiedby,
                                                          status);
                BindGridDepartment();
                lbl_Department_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(Departmentpanel);
                Alerts.HANDLE_AUTHORITY(3, btn_Department_add, btn_Department_save, btn_Department_Cancel, btn_Print_Department, Auth);    
           
            }
    }
    protected void btn_Department_Cancel_Click(object sender, EventArgs e)
    {
       
        GvDepartment.SelectedIndex = -1;
        Alerts.HidePanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(6, btn_Department_add, btn_Department_save, btn_Department_Cancel, btn_Print_Department, Auth);     
   
    }
    protected void btn_Print_Department_Click(object sender, EventArgs e)
    {

    }

    protected void GvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdrank;
            hfdrank = (HiddenField)GvDepartment.Rows[Rowindx].FindControl("hdnDepartmentId");
            id = Convert.ToInt32(hfdrank.Value.ToString());

            Show_Record_Department(id);
            GvDepartment.SelectedIndex = Rowindx;
            Alerts.ShowPanel(Departmentpanel);
            Alerts.HANDLE_AUTHORITY(5, btn_Department_add, btn_Department_save, btn_Department_Cancel, btn_Print_Department, Auth);
        }
    }

    protected void btnEditDepartment_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvDepartment.Rows[Rowindx].FindControl("hdnDepartmentId");
        id = Convert.ToInt32(hfdrank.Value.ToString());

        Show_Record_Department(id);
        GvDepartment.SelectedIndex = Rowindx;
        Alerts.ShowPanel(Departmentpanel);
        Alerts.HANDLE_AUTHORITY(5, btn_Department_add, btn_Department_save, btn_Department_Cancel, btn_Print_Department, Auth);
    }
}
