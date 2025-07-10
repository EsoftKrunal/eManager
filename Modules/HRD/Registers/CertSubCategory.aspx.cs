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

public partial class Registers_CertSubCategory : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_CertCat_Message.Text = "";
        lbl_GridView_CertCat.Text = "";
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
            BindCertCatGrid();
            BindStatusDDL();
            LoadCat();
            Alerts.HidePanel(pnl_CertCat);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_CertCat, btn_Save_CertCat, btn_Cancel_CertCat, btn_Print_CertCat, Auth);
       
        }
    }
    public void LoadCat()
    {
        DataTable dt1 = Budget.getTable("SELECT * FROM CertCategory order by CertCatName").Tables[0];
        ddlCat.DataSource = dt1;
        ddlCat.DataTextField = "CertCatName";
        ddlCat.DataValueField = "CertCatId";
        ddlCat.DataBind(); 
        
    }
    public void BindCertCatGrid()
    {
        DataTable dt1 = Budget.getTable("SELECT *,(select certcatname from certcategory where certcatid=CertSubCategory.CertCatId) as CategoryName,case when statusid='A' then 'Active' else 'InActive' end as StatusName FROM CertSubCategory").Tables[0];
        this.GridView_CertCat.DataSource = dt1;
        this.GridView_CertCat.DataBind();
    }
    public void BindStatusDDL()
    {
        DataTable dt2 = RecruitingOffice.selectDataStatusDetails();
        this.ddlStatus_CertCat.DataValueField = "StatusId";
        this.ddlStatus_CertCat.DataTextField = "StatusName";
        this.ddlStatus_CertCat.DataSource = dt2;
        this.ddlStatus_CertCat.DataBind();
    }
    protected void btn_Add_RecruitingOffice_Click(object sender, EventArgs e)
    {
        txtCertCatName.Text = "";
        txtCreatedBy_CertCat.Text = "";
        txtCreatedOn_CertCat.Text = "";
        txtModifiedBy_CertCat.Text = "";
        txtModifiedOn_CertCat.Text = "";
        ddlStatus_CertCat.SelectedIndex = 0;
        GridView_CertCat.SelectedIndex = -1;
        HiddenCertCatId.Value = "";
        Alerts.ShowPanel(pnl_CertCat);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_CertCat, btn_Save_CertCat, btn_Cancel_CertCat, btn_Print_CertCat, Auth);    
 
    }
    protected void btn_Print_RecruitingOffice_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Save_RecruitingOffice_Click(object sender, EventArgs e)
    {
        int Duplicate=0;

        foreach (GridViewRow dg in GridView_CertCat.Rows)
            {
                Label hfd;
                HiddenField hfd1;
                hfd = (Label)dg.FindControl("lblCertCatName");
                hfd1 = (HiddenField)dg.FindControl("CertCatId");

                if (hfd.Text.ToString().ToUpper().Trim() == txtCertCatName.Text.ToUpper().Trim())
                {
                    if (HiddenCertCatId.Value.Trim() == "")
                    {

                        lbl_CertCat_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenCertCatId.Value.Trim() != hfd1.Value.ToString())
                    {
                      
                        lbl_CertCat_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    
                    lbl_CertCat_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intRecruitingOfficeId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenCertCatId.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                    Budget.getTable("Insert into CertSubCategory(CertSubCatName,CertCatId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,StatusId) values('" + txtCertCatName.Text.Trim() + "'," + ddlCat.SelectedValue + "," + intCreatedBy.ToString() + ",'" + DateTime.Today.ToString("MM/dd/yyyy") + "',NULL,NULL,'" + ddlStatus_CertCat.SelectedValue + "')");   
                }
                else
                {
                    intRecruitingOfficeId = Convert.ToInt32(HiddenCertCatId.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                    Budget.getTable("Update CertSubCategory set CertSubCatName='" + txtCertCatName.Text.Trim() + "',CertCatid=" + ddlCat.SelectedValue + ",ModifiedBy=" + intModifiedBy.ToString() + ",ModifiedOn='" + DateTime.Today.ToString("MM/dd/yyyy") + "',StatusId='" + ddlStatus_CertCat.SelectedValue + "' Where CertSubCatId=" + HiddenCertCatId.Value);   

                }
                BindCertCatGrid();
                lbl_CertCat_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(pnl_CertCat);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_CertCat, btn_Save_CertCat, btn_Cancel_CertCat, btn_Print_CertCat, Auth);    
            }
       
    }
    protected void btn_Cancel_RecruitingOffice_Click(object sender, EventArgs e)
    {
        
        GridView_CertCat.SelectedIndex = -1;
        Alerts.HidePanel(pnl_CertCat);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_CertCat, btn_Save_CertCat, btn_Cancel_CertCat, btn_Print_CertCat, Auth);     
   
    }
    protected void GridView_RecruitingOffice_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_CertCat, Auth);  
    }
    protected void Show_Record_RecruitingOffice(int RecruitingOfficeid)
    {
        HiddenCertCatId.Value = RecruitingOfficeid.ToString();
        DataTable dt3 = Budget.getTable("SELECT CertSubCatName,CertCatId,(SELECT FirstName+' '+LastName from UserLogin where LoginId=CertSubCategory.CreatedBy) as CreatedBy,replace(convert(Varchar,CreatedOn,106),' ','-') as CreatedOn,(SELECT FirstName+' '+LastName from UserLogin where LoginId=CertSubCategory.ModifiedBy) as ModifiedBy,replace(convert(Varchar,ModifiedOn,106),' ','-') as ModifiedOn,StatusId FROM CertSubCategory where CertSubCatId=" + HiddenCertCatId.Value).Tables[0];
        foreach (DataRow dr in dt3.Rows)
        {
            txtCertCatName.Text = dr["CertSubCatName"].ToString();
            ddlCat.SelectedValue = dr["CertCatId"].ToString();
            txtCreatedBy_CertCat.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_CertCat.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_CertCat.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_CertCat.Text = dr["ModifiedOn"].ToString();
            ddlStatus_CertCat.SelectedValue = dr["StatusId"].ToString();
        }

    }
    // VIEW THE RECORD
    protected void GridView_RecruitingOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        HiddenField hfdCerCat;
        hfdCerCat = (HiddenField)GridView_CertCat.Rows[GridView_CertCat.SelectedIndex].FindControl("CertCatId");
        id = Convert.ToInt32(hfdCerCat.Value.ToString());
        Show_Record_RecruitingOffice(id);
        Alerts.ShowPanel(pnl_CertCat);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_CertCat, btn_Save_CertCat, btn_Cancel_CertCat, btn_Print_CertCat, Auth);     
  
    }
    //EDIT THE RECORD
    protected void GridView_RecruitingOffice_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        Mode = "Edit";
        HiddenField hfdCerCat;
        hfdCerCat = (HiddenField)GridView_CertCat.Rows[e.NewEditIndex].FindControl("CertCatId");
        id = Convert.ToInt32(hfdCerCat.Value.ToString());
        Show_Record_RecruitingOffice(id);
        GridView_CertCat.SelectedIndex = e.NewEditIndex;
        Alerts.ShowPanel(pnl_CertCat);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_CertCat, btn_Save_CertCat, btn_Cancel_CertCat, btn_Print_CertCat, Auth);     
     
    }
    // DELETE THE RECORD
    protected void GridView_RecruitingOffice_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdCerCat;
        hfdCerCat = (HiddenField)GridView_CertCat.Rows[e.RowIndex].FindControl("CertCatId");
        Budget.getTable("Update CertSubCategory set statusid='D' Where CertSubCatId=" + hfdCerCat.Value);
        if (HiddenCertCatId.Value.ToString() == hfdCerCat.Value.ToString())
        {
            btn_Add_RecruitingOffice_Click(sender, e);
        }
        BindCertCatGrid();
    }
    protected void GridView_RecruitingOffice_PreRender(object sender, EventArgs e)
    {
        if (GridView_CertCat.Rows.Count <= 0) { lbl_GridView_CertCat.Text = "No Records Found..!"; }
    }


    protected void GridView_CertCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdCerCat;
            hfdCerCat = (HiddenField)GridView_CertCat.Rows[Rowindx].FindControl("hdnCertSubCatId");
            id = Convert.ToInt32(hfdCerCat.Value.ToString());
            Show_Record_RecruitingOffice(id);
            GridView_CertCat.SelectedIndex = Rowindx;
            Alerts.ShowPanel(pnl_CertCat);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_CertCat, btn_Save_CertCat, btn_Cancel_CertCat, btn_Print_CertCat, Auth);
        }
        }

    protected void btnEditCertSubCat_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdCerCat;
        hfdCerCat = (HiddenField)GridView_CertCat.Rows[Rowindx].FindControl("hdnCertSubCatId");
        id = Convert.ToInt32(hfdCerCat.Value.ToString());
        Show_Record_RecruitingOffice(id);
        GridView_CertCat.SelectedIndex = Rowindx;
        Alerts.ShowPanel(pnl_CertCat);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_CertCat, btn_Save_CertCat, btn_Cancel_CertCat, btn_Print_CertCat, Auth);
    }
    }
