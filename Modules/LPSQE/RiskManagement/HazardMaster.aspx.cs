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

public partial class EventManagement_HazardMaster : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------
    public int HazardId
    {
        set { ViewState["HazardId"] = value; }
        get { return Common.CastAsInt32(ViewState["HazardId"]); }
    }
    public int EventId
    {
        set { ViewState["EventId"] = value; }
        get { return Common.CastAsInt32(ViewState["EventId"]); }
    }
    public int ConsequencesId
    {
        set { ViewState["ConsequencesId"] = value; }
        get { return Common.CastAsInt32(ViewState["ConsequencesId"]); }
    }
    #endregion -----------------------------------

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text = "";
        lblEventMSG.Text = "";
        lblMsg_Task.Text = "";
        // -------------------------- SESSION CHECK ----------------------------------------------
        // ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            Session["RM"] = "H";
            BindEvents();
            //BindddlEvents();
    
        }
    }
    //public void BindddlEvents()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_EventMaster");

    //    if (dt.Rows.Count > 0)
    //    {
      
    //        ddlEvent.DataSource = dt;
    //        ddlEvent.DataTextField = "EventName";
    //        ddlEvent.DataValueField = "EventId";
    //        ddlEvent.DataBind();

    //        ddlEvent.Items.Insert(0, new ListItem("< Select >", "0"));


    //        rptEvents.DataSource = dt;
    //        rptEvents.DataBind();
    //    }
    //}
    public void BindEvents()
    {
        DataTable dt;
        if(txtSearchText.Text.Trim()=="")
            dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_EventMaster order by eventcode");
        else
            dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_EventMaster WHERE EVENTNAME LIKE '%" + txtSearchText.Text.Trim() + "%'  order by eventcode");

        if (dt.Rows.Count > 0)
        {
            rptEvents.DataSource = dt;
            rptEvents.DataBind();
        }
        else
        {
            rptEvents.DataSource = null;
            rptEvents.DataBind();
        }
    }
    public void BindHazards()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_HazardMaster ");

        if (dt.Rows.Count > 0)
        {
            rptHazard.DataSource = dt;
            rptHazard.DataBind();
        }
        else
        {
            rptHazard.DataSource = null;
            rptHazard.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtHazardName.Text.Trim() == "")
        {
            txtHazardName.Focus();
            lblMsg.Text = "Please enter hazard name.";
            return;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_HazardMaster WHERE HazardName = '" + txtHazardName.Text.Trim() + "' AND HazardId <> " + HazardId + " ");
        if (dt.Rows.Count > 0)
        {
            txtHazardName.Focus();
            lblMsg.Text = "Please check! Hazard already exists.";
            return;
        }

        try
        {
            Common.Set_Procedures("[dbo].[EV_InsertUpdateHazardMaster]");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
               new MyParameter("@HazardId", HazardId),
               new MyParameter("@HazardName", txtHazardName.Text.Trim())               
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                //lblMsg.Text = "Hazard added/edited successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Hazard added/edited successfully.');", true);
                btnClose_Click(sender, e); 
            }
            else
            {
                lblMsg.Text = "Unable to add/edit hazard.Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to add/edit hazard.Error :" + ex.Message.ToString();
        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //BindddlEvents();
        HazardId = Common.CastAsInt32( ((ImageButton)sender).CommandArgument);

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_HazardMaster WHERE HazardId = " + HazardId + " ");

        //ddlEvent.SelectedValue = dt.Rows[0]["EventId"].ToString();
        //ddlEvent.Enabled = false;  
        txtHazardName.Text = dt.Rows[0]["HazardName"].ToString();
        txtHazardCode.Text = dt.Rows[0]["HazardCode"].ToString();
        //txtSCM.Text = dt.Rows[0]["STD_CONTROL_MEASURES"].ToString();
        //txtACM.Text = dt.Rows[0]["ADD_CONTROL_MEASURES"].ToString();

        dv_AddEditHazard.Visible = true;
        
    }
    //protected void btnSelectEvent_Click(object sender, EventArgs e)
    //{
    //    EventId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
    //    BindEvents();
    //    BindHazards();
    //}
    protected void btnEditEvent_Click(object sender, EventArgs e)
    {
        EventId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_EventMaster WHERE EventId = " + EventId + " ");
        
        txtEventCode.Text = dt.Rows[0]["EventCode"].ToString();
        txtEventName.Text = dt.Rows[0]["EventName"].ToString();
        if (dt.Rows[0]["OfficeApprovalRequired"].ToString() == "Y")
        {
            chkOfficeAppRequired.Checked = true;
        }
        else
        {
            chkOfficeAppRequired.Checked = false;
        }

        dv_AddEditEvent.Visible = true;
    }
    
    protected void btnAddHazard_Click(object sender, EventArgs e)
    {
        HazardId = 0;
        txtHazardName.Text = "";
        txtHazardCode.Text = "";
        txtHazardName.Focus();
        dv_AddEditHazard.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        HazardId = 0;
        BindHazards();
        dv_AddEditHazard.Visible = false;
    }

    protected void btnAddEvent_Click(object sender, EventArgs e)
    {
        EventId = 0;
        txtEventName.Text = "";
        txtEventCode.Text = "";
        dv_AddEditEvent.Visible = true;
	chkOfficeAppRequired.Checked=false;
        txtEventCode.Focus();
    }

    protected void btnSaveEvent_Click(object sender, EventArgs e)
    {
      
        if (txtEventName.Text.Trim() == "")
        {
            txtEventName.Focus();
            lblEventMSG.Text = "Please enter Risk Topic.";
            return;
        }
        
        

        try
        {
            Common.Set_Procedures("[dbo].[EV_InsertUpdateEventMaster]");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
               new MyParameter("@EventId", EventId),
               new MyParameter("@EventName", txtEventName.Text.Trim()),
	           new MyParameter("@OfficeApprovalRequired", ((chkOfficeAppRequired.Checked)?"Y":"N") )
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                //lblEventMSG.Text = "Risk Topic added/edited successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Risk Topic added/edited successfully.');", true);
                btnCloseEvent_Click(sender, e);
            }
            else
            {
                lblEventMSG.Text = "Unable to add/edit Risk Topic.Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblEventMSG.Text = "Unable to add/edit event.Error :" + ex.Message.ToString();
        }

    }
    protected void btnCloseEvent_Click(object sender, EventArgs e)
    {
        EventId = 0;
        BindEvents();
        dv_AddEditEvent.Visible = false;
    }
    
    protected void btnSearchEvent_Click(object sender, EventArgs e)
    {
        dv_SearchEvent.Visible = true;
    }
    protected void btnClearEvent_Click(object sender, EventArgs e)
    {
        txtSearchText.Text = "";
        dv_SearchEvent.Visible = false;
        BindEvents();
        BindHazards();
    }
    protected void btnSearchNow_Click(object sender, EventArgs e)
    {
        dv_SearchEvent.Visible = false;
        BindEvents();
        //BindHazards();
    }
    protected void btnCancelSearch_Click(object sender, EventArgs e)
    {
        dv_SearchEvent.Visible = false;
        BindEvents();
        //BindHazards();
    }
    protected void rdoType_CheckedChanged(object sender, EventArgs e)
    {
        if (rdo_Risk.Checked)
        {
            dv_Risk.Visible = true;
            dv_Hazard.Visible = false;
            dv_Task.Visible = false;
            BindEvents();
        }
        if (rdo_Hazard.Checked)
        {
            dv_Risk.Visible = false;
            dv_Hazard.Visible = true;
            dv_Task.Visible = false;
            BindHazards();
        }
        if (rdo_Task.Checked)
        {
            dv_Risk.Visible = false;
            dv_Hazard.Visible = false;
            dv_Task.Visible = true;
            BindTasks();
        }

    }

    public void BindTasks()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.RA_ConsequencesMaster ");

        if (dt.Rows.Count > 0)
        {
            rpt_Task.DataSource = dt;
            rpt_Task.DataBind();
        }
        else
        {
            rpt_Task.DataSource = null;
            rpt_Task.DataBind();
        }
    }
    protected void btnAddTask_Click(object sender, EventArgs e)
    {
        ConsequencesId = 0;
        txtTaskName.Text = "";
        txtTaskCode.Text = "";
        dv_AddEditTask.Visible = true;
    }
    protected void btnEditTask_Click(object sender, EventArgs e)
    {
        ConsequencesId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.RA_ConsequencesMaster WHERE ConsequencesId = " + ConsequencesId + " ");
        txtTaskName.Text = dt.Rows[0]["ConsequencesName"].ToString();
        txtTaskCode.Text = dt.Rows[0]["ConsequencesCode"].ToString();
        dv_AddEditTask.Visible = true;

    }
    protected void btnSaveTask_Click(object sender, EventArgs e)
    {
        if (txtTaskName.Text.Trim() == "")
        {
            txtTaskName.Focus();
            lblMsg_Task.Text = "Please enter Consequences name.";
            return;
        }


        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.RA_ConsequencesMaster WHERE ConsequencesName = '" + txtTaskName.Text.Trim() + "' AND ConsequencesId <> " + ConsequencesId + " ");
        if (dt.Rows.Count > 0)
        {
            txtTaskName.Focus();
            lblMsg_Task.Text = "Please check! Consequences already exists.";
            return;
        }

        try
        {
            Common.Set_Procedures("[dbo].[RA_InsertUpdateConsequencesMaster]");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
               new MyParameter("@ConsequencesId", ConsequencesId),
               new MyParameter("@ConsequencesName", txtTaskName.Text.Trim())
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                //lblMsg_Task.Text = "Task added/edited successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Consequences added/edited successfully.');", true);
                btnCloseTask_Click(sender, e);
            }
            else
            {
                lblMsg_Task.Text = "Unable to add/edit Consequences.Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg_Task.Text = "Unable to add/edit Consequences.Error :" + ex.Message.ToString();
        }
    }
    protected void btnCloseTask_Click(object sender, EventArgs e)
    {
        ConsequencesId = 0;
        BindTasks();
        dv_AddEditTask.Visible = false;
    }
    

}