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

public partial class EventManagement_EventMaster : System.Web.UI.Page
{
    #region -------- PROPERTIES ------------------
    public int EventId
    {
        set { ViewState["EventId"] = value; }
        get { return Common.CastAsInt32(ViewState["EventId"]); }
    }
    #endregion -----------------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text = "";
        if (!IsPostBack)
        {
            BindEvents();
        }
    }

    public void BindEvents()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_EventMaster");

        if (dt.Rows.Count > 0)
        {
            rptEventMaster.DataSource = dt;
            rptEventMaster.DataBind();
        }
        else
        {
            rptEventMaster.DataSource = null;
            rptEventMaster.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtEventName.Text.Trim() == "")
        {
            txtEventName.Focus();
            lblMsg.Text = "Please enter Risk Topic.";
            return;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_EventMaster WHERE EventName = '" + txtEventName.Text.Trim() + "' AND EventId <> " + EventId + " ");
        if (dt.Rows.Count > 0)
        {
            txtEventName.Focus();
            lblMsg.Text = "Please check! Risk Topic already exists.";
            return;
        }

        try
        {
            Common.Set_Procedures("[dbo].[EV_InsertUpdateEventMaster]");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
               new MyParameter("@EventId", EventId),              
               new MyParameter("@EventName", txtEventName.Text.Trim())
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                EventId = 0;
                lblMsg.Text = "Risk Topic added/edited successfully.";                 
            }
            else
            {
                lblMsg.Text = "Unable to add/edit Risk Topic.Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to add/edit event.Error :" + ex.Message.ToString();
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        EventId = 0;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_EventMaster WHERE EventName LIKE '%" + txtSearch.Text.Trim() + "%' ");
        
        rptEventMaster.DataSource = dt;
        rptEventMaster.DataBind();

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EventId = Common.CastAsInt32( ((ImageButton)sender).CommandArgument);

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.EV_EventMaster WHERE EventId = " + EventId + " ");

        txtEventName.Text = dt.Rows[0]["EventName"].ToString();

        dv_AddEditEvent.Visible = true;
        
    }

    protected void btnAddEvent_Click(object sender, EventArgs e)
    {
        EventId = 0;
        txtEventName.Text = "";         
        dv_AddEditEvent.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        EventId = 0;         
        BindEvents();
        dv_AddEditEvent.Visible = false;
    }
    
}