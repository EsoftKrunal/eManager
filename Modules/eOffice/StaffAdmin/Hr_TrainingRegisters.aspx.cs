using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class emtm_StaffAdmin_Emtm_Hr_TrainingRegisters : System.Web.UI.Page
{
    public int SelectedId
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedId"]);
        }
        set
        {
            ViewState["SelectedId"] = value;
        }
    }
    public string Mode
    {
        get
        {
            return "" + ViewState["Mode"].ToString();
        }
        set
        {
            ViewState["Mode"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lbl_TrainingType_Message.Text = "";
        if (!Page.IsPostBack)
        {
            BindTrainingGroups();
        }

    }

    public void BindTrainingGroups()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TrainingGroupId,TrainingGroupName,Case WHEN StatusId = 'A' THEN 'Active' ELSE 'InActive' END AS StatusId  FROM HR_TrainingGroup");
        rptTrainingGroup.DataSource = dt;
        rptTrainingGroup.DataBind();

    }
    protected void btnTGView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TrainingGroupId,TrainingGroupName,StatusId FROM HR_TrainingGroup WHERE TrainingGroupId=" + SelectedId );

        BindTrainingGroups();
        txt_TrainingGroup.Text = dt.Rows[0]["TrainingGroupName"].ToString();
        ddlStatus.SelectedValue = dt.Rows[0]["StatusId"].ToString();

        btnsave.Visible = false;
        divAddEdit.Visible = true;        
        btncancel.Visible = true;
        btnAddNew.Visible = false;


    }
    protected void btnTGDelete_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        //--------------------- Checking Dependency in Training Master --------------------------
        DataTable dtCheck = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM  HR_TrainingMaster WHERE TrainingGroupId =" + SelectedId + " ");
        if (dtCheck.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Can not delete this group. It is being used.');", true);
            return;
        }

        //---------------------------------------------------------------------------------------

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM HR_TrainingGroup WHERE TrainingGroupId=" + SelectedId + "; SELECT -1");
        if (dt.Rows.Count > 0)
        {
            lbl_TrainingType_Message.Text = "Training group deleted successfully.";
            SelectedId = 0;
            BindTrainingGroups();
        }
        else
        {
            lbl_TrainingType_Message.Text = "Unable to delete training group.";
        }
    }
    protected void btnTGEdit_Click(object sender, ImageClickEventArgs e)
    {
        Mode = "E";
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TrainingGroupId,TrainingGroupName,StatusId FROM HR_TrainingGroup WHERE TrainingGroupId=" + SelectedId);

        BindTrainingGroups();
        txt_TrainingGroup.Text = dt.Rows[0]["TrainingGroupName"].ToString();
        ddlStatus.SelectedValue = dt.Rows[0]["StatusId"].ToString();

        divAddEdit.Visible = true;
        btnsave.Visible = true;
        btncancel.Visible = true;
        btnAddNew.Visible = false;

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txt_TrainingGroup.Text.Trim() == "")
        {
            lbl_TrainingType_Message.Text = "Please enter training group.";
            return;
        }

        if (Mode == "A")
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_TrainingGroup WHERE TrainingGroupName = '" + txt_TrainingGroup.Text.Trim() + "' ");

            if (dt.Rows.Count > 0)
            {
                lbl_TrainingType_Message.Text = "Training group already exists.";
                return;
            }
        }
        if (Mode == "E")
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_TrainingGroup WHERE TrainingGroupName = '" + txt_TrainingGroup.Text.Trim() + "' AND TrainingGroupId <> " + SelectedId + " ");

            if (dt.Rows.Count > 0)
            {
                lbl_TrainingType_Message.Text = "Training group already exists.";
                return;
            }
        }

                
        Common.Set_Procedures("HR_InsertUpdateTrainingGroup");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@TrainingGroupId", SelectedId),
            new MyParameter("@TrainingGroupName", txt_TrainingGroup.Text.Trim()),
            new MyParameter("@StatusId", ddlStatus.SelectedValue.Trim()));
        DataSet ds = new DataSet();

        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            BindTrainingGroups();
            btncancel_Click(sender, e);
            ScriptManager.RegisterStartupScript(Page,this.GetType(),"success","alert('Record saved successfully.');",true);   
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);   
        }

        
    }
        
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Mode = "";
        SelectedId = 0;
        txt_TrainingGroup.Text = "";
        ddlStatus.SelectedIndex = 0;
        btnsave.Visible = false;
        btncancel.Visible = false;
        btnAddNew.Visible = true;
        divAddEdit.Visible = false;
        BindTrainingGroups();

    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Mode = "A";
        divAddEdit.Visible = true;
        btnAddNew.Visible = false;
        btnsave.Visible = true;
        btncancel.Visible = true;
    }
}
