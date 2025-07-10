using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class Modules_Admin_SendMailConfiguartion : System.Web.UI.Page
{
    int id;
    AuthenticationManager Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //-----------------------------
            SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_SendMail_Message.Text = "";
        lblSendMail.Text = "";
        Auth = new AuthenticationManager(145, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);

        // int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 46);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("Dummy.aspx");

        //}

        // ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 5);
        // OBJ.Invoke();
        //  Session["Authority"] = OBJ.Authority;
        //  Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            BindStatusDropDown();
            BindProcessDropDown();
            BindGridSendMail();
             Alerts.HidePanel(SendMailpanel);
           // Alerts.HANDLE_AUTHORITY(1, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        }
        }
        catch (Exception ex)
        {
            lbl_SendMail_Message.Text = ex.Message.ToString();
        }
    }


    private void BindStatusDropDown()
    {
        DataTable dt2 = SendMailConfiguartion.selectDataStatus();
        this.ddstatus_SendMail.DataValueField = "StatusId";
        this.ddstatus_SendMail.DataTextField = "StatusName";
        this.ddstatus_SendMail.DataSource = dt2;
        this.ddstatus_SendMail.DataBind();
    }

    private void BindProcessDropDown()
    { 
        string sql = "Select '0' As EmailProId , ' < Select > ' As EmailProName UNION ALL SELECT EmailProId,EmailProName FROM [dbo].[EmailProcess] with(nolock) where EmailProStatus = 'A'";
        DataTable dt = Budget.getTable(sql).Tables[0];
        this.ddl_ProcessName.DataValueField = "EmailProId";
        this.ddl_ProcessName.DataTextField = "EmailProName";
        this.ddl_ProcessName.DataSource = dt;
        this.ddl_ProcessName.DataBind();
    }
    private void BindGridSendMail()
    {
        DataTable dt = SendMailConfiguartion.selectDataSendMailDetails();
        this.GvSendMail.DataSource = dt;
        this.GvSendMail.DataBind();
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
       try
        {
            int Duplicate = 0;

            foreach (GridViewRow dg in GvSendMail.Rows)
            {
                HiddenField hfd;
                HiddenField hfd1;
                hfd = (HiddenField)dg.FindControl("HiddenSMC_ProcessName");
                hfd1 = (HiddenField)dg.FindControl("HiddenSMC_Id");

                if (hfd.Value.ToString().ToUpper().Trim() == ddl_ProcessName.SelectedItem.Text.ToUpper().Trim())
                {
                    if (HiddenSendMailpk.Value.Trim() == "")
                    {
                        lbl_SendMail_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                    else if (HiddenSendMailpk.Value.Trim() != hfd1.Value.ToString())
                    {
                        lbl_SendMail_Message.Text = "Already Entered.";
                        Duplicate = 1;
                        break;
                    }
                }
                else
                {
                    lbl_SendMail_Message.Text = "";
                }
            }
            if (Duplicate == 0)
            {
                int SMC_Id = -1;
                int createdby = 0, modifiedby = 0;

                string strSMC_ProcessName = ddl_ProcessName.SelectedItem.Text;
                int processId = Convert.ToInt32(ddl_ProcessName.SelectedValue);
                string strSMC_Frommail = txtFromMail.Text.Trim();
                string strSMC_BCC = txtBCCMail.Text.Trim();
                string strSMC_Body = txtBody.Text.Trim();
                string strSMC_Subject = txtSubject.Text.Trim();
                char status = Convert.ToChar(ddstatus_SendMail.SelectedValue);
                if (HiddenSendMailpk.Value.Trim() == "")
                {
                    createdby = Convert.ToInt32(Session["loginid"].ToString());
                }
                else
                {
                    SMC_Id = Convert.ToInt32(HiddenSendMailpk.Value);
                    modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                }
                SendMailConfiguartion.insertUpdateSendMailDetails("InsertUpdateSendMailDetails",
                                                          SMC_Id,
                                                          strSMC_ProcessName,
                                                          strSMC_Frommail,
                                                          strSMC_BCC,
                                                          strSMC_Body,
                                                          createdby,
                                                          modifiedby,
                                                          status,
                                                          strSMC_Subject,
                                                          processId);
                BindGridSendMail();
                lbl_SendMail_Message.Text = "Record Successfully Saved.";
                Alerts.HidePanel(SendMailpanel);
                btn_add.Visible = true;
                btn_save.Visible = false;
                btn_Cancel.Visible = false;
                btn_Print.Visible = false;
                //Alerts.HANDLE_AUTHORITY(3, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

            }
        }
        catch(Exception ex)
        {
            lbl_SendMail_Message.Text = ex.Message.ToString();
        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
       try
        {
            HiddenSendMailpk.Value = "";
            ddl_ProcessName.SelectedIndex = 0;
            txtFromMail.Text = "";
            txtBCCMail.Text = "";
            txtBody.Text = "";
            txtcreatedby.Text = "";
            GvSendMail.SelectedIndex = -1;
            txtcreatedon.Text = "";
            txtmodifiedby.Text = "";
            txtmodifiedon.Text = "";
            ddstatus_SendMail.SelectedIndex = 0;
            txtSubject.Text = "";
            Alerts.ShowPanel(SendMailpanel);
            btn_add.Visible = false;
            btn_save.Visible = true;
            btn_Cancel.Visible = true;
            btn_Print.Visible = false;
            string SenderAddress = ConfigurationManager.AppSettings["FromAddress"];
            txtFromMail.Text = SenderAddress;
            //Alerts.HANDLE_AUTHORITY(2, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
        }
        catch (Exception ex)
        {
            lbl_SendMail_Message.Text = ex.Message.ToString();
        }
    }

    protected void GvSendMail_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HiddenField hfdContractTemplate;
            hfdContractTemplate = (HiddenField)GvSendMail.Rows[GvSendMail.SelectedIndex].FindControl("HiddenSMC_Id");
            id = Convert.ToInt32(hfdContractTemplate.Value.ToString());
            Show_Record_SendMail(id);
            Alerts.ShowPanel(SendMailpanel);
            // Alerts.HANDLE_AUTHORITY(4, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
         }
        catch (Exception ex)
        {
            lbl_SendMail_Message.Text = ex.Message.ToString();
        }
    }

    protected void Show_Record_SendMail(int SMC_Id)
    {

        HiddenSendMailpk.Value = SMC_Id.ToString();
        DataTable dt3 = SendMailConfiguartion.selectDataSendMailDetailsById(SMC_Id);
        foreach (DataRow dr in dt3.Rows)
        {
           if (! string.IsNullOrEmpty(dr["SMC_ProcessId"].ToString()))
            {
                ddl_ProcessName.SelectedValue = dr["SMC_ProcessId"].ToString();
            }
            
            txtFromMail.Text = dr["SMC_Frommail"].ToString();
            txtBCCMail.Text = dr["SMC_BCC"].ToString();
            txtBody.Text = dr["SMC_Body"].ToString();
            txtcreatedby.Text = dr["CreatedBy"].ToString();
            txtcreatedon.Text = dr["CreatedOn"].ToString();
            txtmodifiedby.Text = dr["ModifiedBy"].ToString();
            txtmodifiedon.Text = dr["ModifiedOn"].ToString();
            if (!string.IsNullOrEmpty(dr["StatusId"].ToString().Trim()))
            {
                ddstatus_SendMail.SelectedValue = dr["StatusId"].ToString().Trim();
            }
            txtSubject.Text = dr["SMC_Subject"].ToString().Trim();
        }

    }

    protected void btn_Print_Click(object sender, EventArgs e)
    {

    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
       try
        { 
        GvSendMail.SelectedIndex = -1;
        Alerts.HidePanel(SendMailpanel);
            //Alerts.HANDLE_AUTHORITY(6, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
        }
        catch (Exception ex)
        {
            lbl_SendMail_Message.Text = ex.Message.ToString();
        }
    }

    protected void GvSendMail_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int intModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            HiddenField hfdSendMail;
            hfdSendMail = (HiddenField)GvSendMail.Rows[e.RowIndex].FindControl("HiddenSMC_Id");
            id = Convert.ToInt32(hfdSendMail.Value.ToString());
            SendMailConfiguartion.deleteSendMailDetails("deleteSendMail", id, intModifiedBy);
            BindGridSendMail();
            if (HiddenSendMailpk.Value.Trim() == hfdSendMail.Value.ToString())
            {
                btn_add_Click(sender, e);
            }
        }
        catch (Exception ex)
        {
            lbl_SendMail_Message.Text = ex.Message.ToString();
        }
    }

    protected void GvSendMail_DataBound(object sender, EventArgs e)
    {
        //Alerts.HANDLE_GRID(GvSendMail, Auth);
    }

    protected void GvSendMail_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (this.GvSendMail.Rows.Count <= 0)
            {
                lblSendMail.Text = "No Records Found..!";
            }
        }
        catch (Exception ex)
        {
            lbl_SendMail_Message.Text = ex.Message.ToString();
        }
    }

    protected void GvSendMail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            Mode = "Edit";
            HiddenField hfdrank;
            hfdrank = (HiddenField)GvSendMail.Rows[Rowindx].FindControl("HiddenSMC_Id");
            id = Convert.ToInt32(hfdrank.Value.ToString());

            Show_Record_SendMail(id);
            GvSendMail.SelectedIndex = Rowindx;
            Alerts.ShowPanel(SendMailpanel);
           // Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);
        }
        }
        catch (Exception ex)
        {
            lbl_SendMail_Message.Text = ex.Message.ToString();
        }
    }

    protected void GvSendMail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void btnEditSendMail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        Mode = "Edit";
        HiddenField hfdrank;
        hfdrank = (HiddenField)GvSendMail.Rows[Rowindx].FindControl("HiddenSMC_Id");
        id = Convert.ToInt32(hfdrank.Value.ToString());

        Show_Record_SendMail(id);
        GvSendMail.SelectedIndex = Rowindx;
        // Alerts.ShowPanel(Departmentpanel);
        // Alerts.HANDLE_AUTHORITY(5, btn_add, btn_save, btn_Cancel, btn_Print, Auth);

        SendMailpanel.Visible = true;
        btn_Cancel.Visible = true;
        btn_save.Visible = true;
        btn_add.Visible = false;
    }
        catch (Exception ex)
        {
            lbl_SendMail_Message.Text = ex.Message.ToString();
        }
    }
}