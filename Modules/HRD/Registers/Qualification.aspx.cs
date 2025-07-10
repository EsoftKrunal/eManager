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

public partial class Registers_Qualification : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_GridView_Qualification.Text = "";
        lbl_Qualification_Message.Text = "";
      
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
            bindQualificationGrid();
            bindStatusDDL();
            Alerts.HidePanel(pnl_Qualification);
            Alerts.HANDLE_AUTHORITY(1, btn_Add_Qualification, btn_Save_Qualification, btn_Cancel_Qualification, btn_Print_Qualification, Auth);
        }
    }
    public void bindQualificationGrid()
    {
        DataTable dt1 = Qualification.selectDataQualificationDetails();
        this.GridView_Qualification.DataSource = dt1;
        this.GridView_Qualification.DataBind();
    }
    public void bindStatusDDL()
    {
        DataTable dt2 = Qualification.selectDataStatusDetails();
        this.ddlStatus_Qualification.DataValueField = "StatusId";
        this.ddlStatus_Qualification.DataTextField = "StatusName";
        this.ddlStatus_Qualification.DataSource = dt2;
        this.ddlStatus_Qualification.DataBind();
    }
    protected void btn_Add_Qualification_Click(object sender, EventArgs e)
    {
        txtQualificationName.Text = "";
        txtQualificationFlag.Text = "";
        txtCreatedBy_Qualification.Text = "";
        txtCreatedOn_Qualification.Text = "";
        txtModifiedBy_Qualification.Text = "";
        txtModifiedOn_Qualification.Text = "";
        ddlStatus_Qualification.SelectedIndex = 0;
        GridView_Qualification.SelectedIndex = -1;
        HiddenQualification.Value = "";
     
        Alerts.ShowPanel(pnl_Qualification);
        Alerts.HANDLE_AUTHORITY(2, btn_Add_Qualification, btn_Save_Qualification, btn_Cancel_Qualification, btn_Print_Qualification, Auth);
    }
    protected void btn_Save_Qualification_Click(object sender, EventArgs e)
    {
        int Duplicate=0;
        
            foreach (GridViewRow dg in GridView_Qualification.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenQualificationName");
                hfd1 = (HiddenField)dg.FindControl("HiddenQualificationId");

                if (hfd.Value.ToString().ToUpper().Trim() == txtQualificationName.Text.ToUpper().Trim())
                {
                    if (HiddenQualification.Value.Trim() == "")
                    {
                        lbl_Qualification_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenQualification.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_Qualification_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_Qualification_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int intQualificationId = -1;
                int intCreatedBy = 0;
                int intModifiedBy = 0;
                if (HiddenQualification.Value.ToString().Trim() == "")
                {
                    intCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    intQualificationId = Convert.ToInt32(HiddenQualification.Value);
                    intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                }
                string strQualificationName = txtQualificationName.Text;
                string strQualificationFlag = txtQualificationFlag.Text;
                char charStatusId = Convert.ToChar(ddlStatus_Qualification.SelectedValue);

                Qualification.insertUpdateQualificationDetails("InsertUpdateQualificationDetails",
                                              intQualificationId,
                                              strQualificationName,
                                              strQualificationFlag,
                                              intCreatedBy,
                                              intModifiedBy,
                                              charStatusId);

                bindQualificationGrid();
                lbl_Qualification_Message.Text = "Record Successfully Saved.";
              
                Alerts.HidePanel(pnl_Qualification);
                Alerts.HANDLE_AUTHORITY(3, btn_Add_Qualification, btn_Save_Qualification, btn_Cancel_Qualification, btn_Print_Qualification, Auth);
            }
      
    }
    protected void btn_Cancel_Qualification_Click(object sender, EventArgs e)
    {
        GridView_Qualification.SelectedIndex = -1;
        
        Alerts.HidePanel(pnl_Qualification);
        Alerts.HANDLE_AUTHORITY(6, btn_Add_Qualification, btn_Save_Qualification, btn_Cancel_Qualification, btn_Print_Qualification, Auth);
    }
    protected void btn_Print_Qualification_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_Qualification_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GridView_Qualification, Auth);
    }
    protected void Show_Record_Qualification(int Qualificationid)
    {
        HiddenQualification.Value = Qualificationid.ToString();
        DataTable dt3 = Qualification.selectDataQualificationDetailsByQualificationId(Qualificationid);
        foreach (DataRow dr in dt3.Rows)
        {
            txtQualificationName.Text = dr["QualificationName"].ToString();
            txtQualificationFlag.Text = dr["QualificationFlag"].ToString();
            txtCreatedBy_Qualification.Text = dr["CreatedBy"].ToString();
            txtCreatedOn_Qualification.Text = dr["CreatedOn"].ToString();
            txtModifiedBy_Qualification.Text = dr["ModifiedBy"].ToString();
            txtModifiedOn_Qualification.Text = dr["ModifiedOn"].ToString();
            ddlStatus_Qualification.SelectedValue = dr["StatusId"].ToString();
        }
    }
    
    protected void GridView_Qualification_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        HiddenField hfdQualification;
        hfdQualification = (HiddenField)GridView_Qualification.Rows[GridView_Qualification.SelectedIndex].FindControl("HiddenQualificationId");
        id = Convert.ToInt32(hfdQualification.Value.ToString());
        Show_Record_Qualification(id);
       
        Alerts.ShowPanel(pnl_Qualification);
        Alerts.HANDLE_AUTHORITY(4, btn_Add_Qualification, btn_Save_Qualification, btn_Cancel_Qualification, btn_Print_Qualification, Auth);
    }
   
    protected void GridView_Qualification_Row_Editing(object sender, GridViewEditEventArgs e)
    {
        HiddenField hfdQualification;
        hfdQualification = (HiddenField)GridView_Qualification.Rows[e.NewEditIndex].FindControl("HiddenQualificationId");
        id = Convert.ToInt32(hfdQualification.Value.ToString());
        Show_Record_Qualification(id);
        GridView_Qualification.SelectedIndex = e.NewEditIndex;
      
        Alerts.ShowPanel(pnl_Qualification);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Qualification, btn_Save_Qualification, btn_Cancel_Qualification, btn_Print_Qualification, Auth);
    }
   
    protected void GridView_Qualification_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdQualification;
        hfdQualification = (HiddenField)GridView_Qualification.Rows[e.RowIndex].FindControl("HiddenQualificationId");
        id = Convert.ToInt32(hfdQualification.Value.ToString());
        Qualification.deleteQualificationDetailsById("DeleteQualificationById", id, intModifiedBy);
        bindQualificationGrid();
        if (HiddenQualification.Value.ToString() == hfdQualification.Value.ToString())
        {
            btn_Add_Qualification_Click(sender, e);
        }
    }
    protected void GridView_Qualification_PreRender(object sender, EventArgs e)
    {
        if (GridView_Qualification.Rows.Count <= 0) { lbl_GridView_Qualification.Text = "No Records Found..!"; }
    }

    protected void GridView_Qualification_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdQualification;
            hfdQualification = (HiddenField)GridView_Qualification.Rows[Rowindx].FindControl("hdnQualificationId");
            id = Convert.ToInt32(hfdQualification.Value.ToString());
            Show_Record_Qualification(id);
            GridView_Qualification.SelectedIndex = Rowindx;

            Alerts.ShowPanel(pnl_Qualification);
            Alerts.HANDLE_AUTHORITY(5, btn_Add_Qualification, btn_Save_Qualification, btn_Cancel_Qualification, btn_Print_Qualification, Auth);
        }
        }

    protected void btnEditQualification_click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdQualification;
        hfdQualification = (HiddenField)GridView_Qualification.Rows[Rowindx].FindControl("hdnQualificationId");
        id = Convert.ToInt32(hfdQualification.Value.ToString());
        Show_Record_Qualification(id);
        GridView_Qualification.SelectedIndex = Rowindx;

        Alerts.ShowPanel(pnl_Qualification);
        Alerts.HANDLE_AUTHORITY(5, btn_Add_Qualification, btn_Save_Qualification, btn_Cancel_Qualification, btn_Print_Qualification, Auth);
    }
}
