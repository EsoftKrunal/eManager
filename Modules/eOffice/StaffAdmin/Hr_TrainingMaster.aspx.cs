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

public partial class emtm_StaffAdmin_Emtm_Hr_TrainingMaster : System.Web.UI.Page
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
            BindTrainingMaster();
            BindTrainingGroup();
        }
    }

    public void BindTrainingGroup()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TrainingGroupId,TrainingGroupName FROM HR_TrainingGroup");
        ddlTrainingGroup.DataSource = dt;
        ddlTrainingGroup.DataTextField = "TrainingGroupName";
        ddlTrainingGroup.DataValueField = "TrainingGroupId";
        ddlTrainingGroup.DataBind();

        ddlTrainingGroup.Items.Insert(0,new ListItem("< Select >","0"));

    }

    public void BindTrainingMaster()
    {
        string sql = "SELECT TrainingId,TrainingName,ShortName,TG.TrainingGroupName,TG.TrainingGroupId,ValidityPeriod,Case WHEN TM.StatusId = 'A' THEN 'Active' ELSE 'InActive' END AS StatusId,case when isnull(ShowInMatrix,'N')='Y' then  'Yes' else 'No' End as ShowInMatrix FROM HR_TrainingMaster TM INNER JOIN HR_TrainingGroup TG ON TG.TrainingGroupId = TM.TrainingGroupId where 1=1 ";
        if (ddlRStatus.SelectedIndex > 0)
            sql += " And TM.StatusId='" + ddlRStatus.SelectedValue + "'";
        if (ddlMatrix.SelectedIndex > 0)
            sql += " And isnull(ShowInMatrix,'N')='" + ddlMatrix.SelectedValue + "'";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptTrainingGroup.DataSource = dt;
        rptTrainingGroup.DataBind();

    }
    protected void btnTGView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TrainingId,TrainingName,ShortName,TrainingGroupId,StatusId,isnull(ShowInMatrix,'N') as ShowInMatrix FROM HR_TrainingMaster WHERE TrainingId=" + SelectedId);

        BindTrainingMaster();
        txt_TrainingName.Text = dt.Rows[0]["TrainingName"].ToString();
        ddlTrainingGroup.SelectedValue = dt.Rows[0]["TrainingGroupId"].ToString();
        ddlStatus.SelectedValue = dt.Rows[0]["StatusId"].ToString();
        txtShortName.Text = dt.Rows[0]["ShortName"].ToString();
        chkShowInMatrix.Checked = dt.Rows[0]["ShowInMatrix"].ToString()=="Y";
        btnsave.Visible = false;
        divAddEdit.Visible = true;
        btncancel.Visible = true;
        btnAddNew.Visible = false;


    }
    protected void btnTGDelete_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        //--------------------- Checking Dependency in Training Master --------------------------

        DataTable dtCheck = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM  HR_TrainingRecommended WHERE TrainingId =" + SelectedId + " ");
        if (dtCheck.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Can not delete this training. It is being used.');", true);
            return;
        }

        //---------

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM HR_TrainingMaster WHERE TrainingId=" + SelectedId + "; SELECT -1");
        if (dt.Rows.Count > 0)
        {
            lbl_TrainingType_Message.Text = "Training deleted successfully.";
            SelectedId = 0;
            BindTrainingMaster();
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
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TrainingId,TrainingName,ShortName,TrainingGroupId,ValidityPeriod,StatusId,isnull(ShowInMatrix,'N') as ShowInMatrix FROM HR_TrainingMaster WHERE TrainingId=" + SelectedId);

        BindTrainingMaster();
        txt_TrainingName.Text = dt.Rows[0]["TrainingName"].ToString();
        ddlTrainingGroup.SelectedValue = dt.Rows[0]["TrainingGroupId"].ToString();
        ddlStatus.SelectedValue = dt.Rows[0]["StatusId"].ToString();
        txtShortName.Text = dt.Rows[0]["ShortName"].ToString();
        txtValidity.Text = dt.Rows[0]["ValidityPeriod"].ToString();
        chkShowInMatrix.Checked = dt.Rows[0]["ShowInMatrix"].ToString() == "Y";
        divAddEdit.Visible = true;
        btnsave.Visible = true;
        btncancel.Visible = true;
        btnAddNew.Visible = false;

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txt_TrainingName.Text.Trim() == "")
        {
            lbl_TrainingType_Message.Text = "Please enter training name.";
            return;
        }
        if (ddlTrainingGroup.SelectedIndex == 0)
        {
            lbl_TrainingType_Message.Text = "Please select training group.";
            return;
        }
        if (Mode == "A")
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_TrainingMaster WHERE TrainingName = '" + txt_TrainingName.Text.Trim().Replace("'","''").ToString() + "' AND  TrainingGroupId = " + ddlTrainingGroup.SelectedValue.Trim() + " ");

            if (dt.Rows.Count > 0)
            {
                lbl_TrainingType_Message.Text = "Training already exists.";
                return;
            }
        }
        if (Mode == "E")
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_TrainingMaster WHERE TrainingName = '" + txt_TrainingName.Text.Trim().Replace("'", "''").ToString() + "' AND  TrainingGroupId = " + ddlTrainingGroup.SelectedValue.Trim() + " AND TrainingId <> " + SelectedId + " ");

            if (dt.Rows.Count > 0)
            {
                lbl_TrainingType_Message.Text = "Training already exists.";
                return;
            }
        }
        
            
            Common.Set_Procedures("HR_InsertUpdateTrainingMaster");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(new MyParameter("@TrainingId", SelectedId),
                new MyParameter("@TrainingName", txt_TrainingName.Text.Trim()),
                new MyParameter("@TrainingGroupId", ddlTrainingGroup.SelectedValue.Trim()),
                new MyParameter("@StatusId", ddlStatus.SelectedValue.Trim()),
                new MyParameter("@ShortName", txtShortName.Text.Trim()),
                new MyParameter("@ValidityPeriod",Common.CastAsInt32(txtValidity.Text.Trim())),
                new MyParameter("@ShowInMatrix",chkShowInMatrix.Checked?"Y":"N")
                
                );
            DataSet ds = new DataSet();

            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                BindTrainingMaster();
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
        txt_TrainingName.Text = "";
        txtShortName.Text = "";
        ddlTrainingGroup.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        btnsave.Visible = false;
        btncancel.Visible = false;
        btnAddNew.Visible = true;
        divAddEdit.Visible = false;
        BindTrainingMaster();

    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Mode = "A";
        divAddEdit.Visible = true;
        btnAddNew.Visible = false;
        btnsave.Visible = true;
        btncancel.Visible = true;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvTG');", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindTrainingMaster();
    }
}
