using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;

public partial class emtm_Emtm_MyTasks : System.Web.UI.Page
{
    public AuthenticationManager auth;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];    
    public int EmpId
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpId"]);
        }
        set
        {
            ViewState["EmpId"] = value;
        }
    }

    public int FollowupId
    {
        get
        {
            return Common.CastAsInt32(ViewState["FollowupId"]);
        }
        set
        {
            ViewState["FollowupId"] = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["ProfileId"]);

            if (EmpId > 0)
            {
                BindTasks(); 
            }
        }
    }

    public void BindTasks()
    {
        string sqlToday = "SELECT * FROM [dbo].[vw_FM_GetFollowUp] "; // WHERE [ResponsiblityId]=" + EmpId + " AND ";

        DataTable dtToday = Common.Execute_Procedures_Select_ByQueryCMS(sqlToday);
        rptTasks.DataSource = dtToday;
        rptTasks.DataBind();
    }

    protected void btnClosure_Click(object sender, EventArgs e)
    {
        FollowupId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        dv_Closure.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        byte[] FileContent = new byte[0];

        if (txt_ClosedDate.Text == "")
        {
            lblMsg.Text = "Please enter Closed Date.";
            txt_ClosedDate.Focus();
            return;
        }

        if (DateTime.Parse(txt_ClosedDate.Text) > DateTime.Today.Date)
        {
            lblMsg.Text = "Closed Date cannot be more than today.";
            txt_ClosedDate.Focus();
            return;
        }

        if (rdbflaws.SelectedIndex == -1)
        {
            lblMsg.Text = "Please select causes.";
            rdbflaws.Focus();
            return;
        }

        if (txt_ClosedRemarks.Text == "")
        {
            lblMsg.Text = "Please enter Closed Remarks.";
            txt_ClosedRemarks.Focus();
            return;
        }
        string Cause = "";

        foreach( ListItem chk in rdbflaws.Items )
        {
            if(chk.Selected)
            {
               Cause = Cause + chk.Value + ",";
            }
        }

        if(Cause.Length > 0)
        {
            Cause = Cause.Substring(0, Cause.Length -1);
        }

        if (!flp_Evidence.HasFile)
        {
            lblMsg.Text = "Please select evidance.";
            flp_Evidence.Focus();
            return; 
        }

        FileContent = flp_Evidence.FileBytes;

        try
        {

            Common.Set_Procedures("DBO.FM_Closure");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(new MyParameter("@FollowUpId", FollowupId),
                new MyParameter("@ClosureRemarks", txt_ClosedRemarks.Text),
                new MyParameter("@Evidence", FileContent),
                new MyParameter("@Cause", Cause),
                new MyParameter("@ClosedBy", Session["UserName"].ToString()),
                new MyParameter("@ClosedOn", txt_ClosedDate.Text));
            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                lblMsg.Text = "Followup closed successfully.";
            }
            else
            {
                lblMsg.Text = "Unable to close. Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to close. Error : " + ex.Message;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        FollowupId = 0;
        txt_ClosedDate.Text = "";
        rdbflaws.SelectedIndex = -1;
        txt_ClosedRemarks.Text = "";
        BindTasks();

        dv_Closure.Visible = false;
    }
}