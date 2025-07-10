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

public partial class Modules_HRD_Registers_ContractTemplate : System.Web.UI.Page
{
    int id;
    Authority Auth;
    string Mode = "New";

    protected void Page_Load(object sender, EventArgs e)
    {
        

        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_ContractTemplate_Message.Text = "";
        lblContractTemplate.Text = "";


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
            BindGridContractTemplate();
            Alerts.HidePanel(ContractTemplatepanel);
            Alerts.HANDLE_AUTHORITY(1, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
    }

    private void BindStatusDropDown()
    {
        DataTable dt2 = ContractTemplate.selectDataStatus();
        this.ddstatus_ContractTemplate.DataValueField = "StatusId";
        this.ddstatus_ContractTemplate.DataTextField = "StatusName";
        this.ddstatus_ContractTemplate.DataSource = dt2;
        this.ddstatus_ContractTemplate.DataBind();
    }
    private void BindGridContractTemplate()
    {
        DataTable dt = ContractTemplate.selectDataContractTemplateDetails();
        this.GvContractTemplate.DataSource = dt;
        this.GvContractTemplate.DataBind();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;

        foreach (GridViewRow dg in GvContractTemplate.Rows)
        {
            HiddenField hfd;
            HiddenField hfd1;
            hfd = (HiddenField)dg.FindControl("HiddenCCT_Name");
            hfd1 = (HiddenField)dg.FindControl("HiddenCCT_Id");

            if (hfd.Value.ToString().ToUpper().Trim() == txtContractTemplate.Text.ToUpper().Trim())
            {
                if (HiddenContractTemplatepk.Value.Trim() == "")
                {
                    lbl_ContractTemplate_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
                else if (HiddenContractTemplatepk.Value.Trim() != hfd1.Value.ToString())
                {
                    lbl_ContractTemplate_Message.Text = "Already Entered.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
                lbl_ContractTemplate_Message.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int CCT_Id = -1;
            int createdby = 0, modifiedby = 0;

            string strCCT_Name = txtContractTemplate.Text;
            string strTemplateURL = txtTemplateURL.Text.Trim();
            char status = Convert.ToChar(ddstatus_ContractTemplate.SelectedValue);
            if (HiddenContractTemplatepk.Value.Trim() == "")
            {
                createdby = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                CCT_Id = Convert.ToInt32(HiddenContractTemplatepk.Value);
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            ContractTemplate.insertUpdateContactTemplateDetails("InsertUpdateContractTemplateDetails",
                                                      CCT_Id,
                                                      strCCT_Name,
                                                      strTemplateURL,
                                                      createdby,
                                                      modifiedby,
                                                      status);
            BindGridContractTemplate();
            lbl_ContractTemplate_Message.Text = "Record Successfully Saved.";
            Alerts.HidePanel(ContractTemplatepanel);
            Alerts.HANDLE_AUTHORITY(3, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        HiddenContractTemplatepk.Value = "";
        txtContractTemplate.Text = "";
        txtcreatedby_ContractTemplate.Text = "";
        GvContractTemplate.SelectedIndex = -1;
        txtcreatedon_ContractTemplate.Text = "";
        txtmodifiedby_ContractTemplate.Text = "";
        txtmodifiedon_ContractTemplate.Text = "";
        ddstatus_ContractTemplate.SelectedIndex = 0;
        txtTemplateURL.Text = "";
        Alerts.ShowPanel(ContractTemplatepanel);
        Alerts.HANDLE_AUTHORITY(2, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }

    protected void GvContractTemplate_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdContractTemplate;
        hfdContractTemplate = (HiddenField)GvContractTemplate.Rows[GvContractTemplate.SelectedIndex].FindControl("HiddenCCT_Id");
        id = Convert.ToInt32(hfdContractTemplate.Value.ToString());
        Show_Record_ContractTemplate(id);
        Alerts.ShowPanel(ContractTemplatepanel);
        Alerts.HANDLE_AUTHORITY(4, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }

    protected void Show_Record_ContractTemplate(int CCT_Id)
    {

        HiddenContractTemplatepk.Value = CCT_Id.ToString();
        DataTable dt3 = ContractTemplate.selectDataContractTemplateDetailsById(CCT_Id);
        foreach (DataRow dr in dt3.Rows)
        {
            txtContractTemplate.Text = dr["CCT_Name"].ToString();
            txtTemplateURL.Text = dr["CCT_PageURL"].ToString();
            txtcreatedby_ContractTemplate.Text = dr["CreatedBy"].ToString();
            txtcreatedon_ContractTemplate.Text = dr["CreatedOn"].ToString();
            txtmodifiedby_ContractTemplate.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon_ContractTemplate.Text = dr["ModifiedOn"].ToString();
            ddstatus_ContractTemplate.SelectedValue = dr["StatusId"].ToString().Trim();
        }

    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        GvContractTemplate.SelectedIndex = -1;
        Alerts.HidePanel(ContractTemplatepanel);
        Alerts.HANDLE_AUTHORITY(6, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
    }

    protected void GvContractTemplate_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
        HiddenField hfdContractTemplate;
        hfdContractTemplate = (HiddenField)GvContractTemplate.Rows[e.RowIndex].FindControl("HiddenCCT_Id");
        id = Convert.ToInt32(hfdContractTemplate.Value.ToString());
        ContractTemplate.deleteContractTemplateDetails("deleteContractTemplate", id, intModifiedBy);
        BindGridContractTemplate();
        if (HiddenContractTemplatepk.Value.Trim() == hfdContractTemplate.Value.ToString())
        {
            btn_add_Click(sender, e);
        }
    }

    protected void GvContractTemplate_DataBound(object sender, EventArgs e)
    {
        Alerts.HANDLE_GRID(GvContractTemplate, Auth);
    }

    protected void GvContractTemplate_PreRender(object sender, EventArgs e)
    {
        if (this.GvContractTemplate.Rows.Count <= 0)
        {
            lblContractTemplate.Text = "No Records Found..!";
        }
    }

    protected void GvContractTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdrank;
            hfdrank = (HiddenField)GvContractTemplate.Rows[Rowindx].FindControl("HiddenCCT_Id");
            id = Convert.ToInt32(hfdrank.Value.ToString());

            Show_Record_ContractTemplate(id);
            GvContractTemplate.SelectedIndex = Rowindx;
            Alerts.ShowPanel(ContractTemplatepanel);
            Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
        }
    }

    protected void GvContractTemplate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void btnEditContractTemplate_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvContractTemplate.Rows[Rowindx].FindControl("HiddenCCT_Id");
        id = Convert.ToInt32(hfdrank.Value.ToString());

        Show_Record_ContractTemplate(id);
        GvContractTemplate.SelectedIndex = Rowindx;
        // Alerts.ShowPanel(Departmentpanel);
        // Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        ContractTemplatepanel.Visible = true;
        btn_Cancel.Visible = true;
        btn_save.Visible = true;
        btn_add.Visible = false;
    }
}