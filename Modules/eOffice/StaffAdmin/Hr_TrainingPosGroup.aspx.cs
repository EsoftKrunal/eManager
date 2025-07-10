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

public partial class Emtm_Hr_TrainingPosGroup : System.Web.UI.Page
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
            BindGroupMaster();
        }
    }

   
    public void BindGroupMaster()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_TrainingPositionGroup Order By GroupName");
        rptTrainingGroup.DataSource = dt;
        rptTrainingGroup.DataBind();

    }
    protected void btnTGView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGroupMaster();
        ShowTrainings();
    }
    public void ShowTrainings()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT PositionID,OFFICENAME,POSITIONNAME + '( ' + PositionCODE + ' )' AS POSITIONNAME FROM " +
                               "POSITION INNER JOIN OFFICE ON Position.OfficeId = Office.OfficeId WHERE Position.TrainingPositionGroup = " + SelectedId + " ORDER BY OFFICENAME, PositionNAME");
        rptTrainings.DataSource = dt;
        rptTrainings.DataBind();
    }
    protected void btnTGDelete_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        //--------------------- Checking Dependency in Training Master --------------------------

        DataTable dtCheck = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM  Position WHERE TrainingPositionGroup =" + SelectedId + " ");
        if (dtCheck.Rows.Count > 0)
        {
            lbl_TrainingType_Message.Text="Can not delete this group. Some positions belongs to this group.";
            return;
        }

        //---------

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM HR_TrainingPositionGroup WHERE GroupId=" + SelectedId + "; SELECT -1");
        if (dt.Rows.Count > 0)
        {
            lbl_TrainingType_Message.Text = "Training deleted successfully.";
            SelectedId = 0;
            BindGroupMaster();
            ShowTrainings();
        }
        else
        {
            lbl_TrainingType_Message.Text = "Unable to delete training.";
        }
    }
    protected void btnTrainingDelete_Click(object sender, ImageClickEventArgs e)
    {
        int key= Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("UPDATE POSITION SET TrainingPositionGroup=NULL WHERE PositionID=" + key + "; SELECT -1");
        if (dt.Rows.Count > 0)
        {
            lbl_TrainingType_Message.Text = "Training deleted successfully.";
           
            ShowTrainings();
        }
        else
        {
            lbl_TrainingType_Message.Text = "Unable to delete training.";
        }
    }
    
    protected void btnTGEdit_Click(object sender, ImageClickEventArgs e)
    {
        Mode = "E";
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_TrainingPositionGroup WHERE GroupId=" + SelectedId);

        txt_GroupName.Text = dt.Rows[0]["GroupName"].ToString();
        dvAddGroup.Visible = true;
        btnsave.Visible = true;
        btncancel.Visible = true;
        
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txt_GroupName.Text.Trim() == "")
        {
            lbl_TrainingType_Message.Text = "Please enter group name.";
            return;
        }
            
            Common.Set_Procedures("HR_InsertUpdateTrainingPositionGroup");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(new MyParameter("@GroupId", SelectedId),
                new MyParameter("@GroupName", txt_GroupName.Text.Trim()));
            DataSet ds = new DataSet();

            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                BindGroupMaster();
                btncancel_Click(sender, e);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
            }            

        
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        txt_GroupName.Text = "";
        btnsave.Visible = false;
        btncancel.Visible = false;
        dvAddGroup.Visible = false;
        ShowTrainings();
    }
    protected void btnCancelAdd_Click(object sender, EventArgs e)
    {
        dvPositions.Visible = false;
        
    }
    
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Mode = "A";
        dvAddGroup.Visible = true;
        btnsave.Visible = true;
        btncancel.Visible = true;
    }
    protected void btnAddTrainings_Click(object sender, EventArgs e)
    {
        if (SelectedId <= 0)
        {
            lbl_TrainingType_Message.Text = "Please select group to add the trainings.";
        }
        else
        {
            dvPositions.Visible = true;
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT PositionID,OFFICENAME,POSITIONNAME + '( ' + PositionCODE + ' )' AS POSITIONNAME FROM " +
                              "POSITION INNER JOIN OFFICE ON Position.OfficeId = Office.OfficeId WHERE ISNULL(Position.TrainingPositionGroup,0)<=0 ORDER BY OFFICENAME, PositionNAME");
            rptAddPositions.DataSource = dt;
            rptAddPositions.DataBind();
        }
    }
    protected void btnSavePos_Click(object sender, EventArgs e)
    {
        foreach(RepeaterItem item in rptAddPositions.Items)
        {
            CheckBox ch = (CheckBox)item.FindControl("chksel");
            if(ch.Checked)
                Common.Execute_Procedures_Select_ByQueryCMS("UPDATE POSITION SET TrainingPositionGroup=" + SelectedId + " WHERE PositionID=" + ch.CssClass);
        }
        ShowTrainings();
        dvPositions.Visible = false;
    }
    
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvTG');", true);
    }
}
