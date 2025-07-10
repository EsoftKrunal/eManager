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

public partial class Register_AssessmentMaster : System.Web.UI.Page
{
    int id;
    Authority Auth;
    AuthenticationManager Auth1;
    public int AM_ID
    {
        get { return Common.CastAsInt32(ViewState["_AM_ID"]); }
        set { ViewState["_AM_ID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 136);
        if (chpageauth <= 0)
        {
            Response.Redirect("~/CrewOperation/Dummy_Training1.aspx");
        }
        Auth = new Authority();
        Auth1 = new AuthenticationManager(136, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        Auth.isAdd = Auth1.IsAdd;
        Auth.isDelete = Auth1.IsDelete;
        Auth.isEdit = Auth1.IsUpdate;
        Auth.isPrint = Auth1.IsPrint;
        Auth.isVerify = Auth1.IsVerify;
        Auth.isVerify2 = Auth1.IsVerify2;

        if (!Page.IsPostBack)
        {
            bindAssessmentmaster();
        }
    }
    protected void btn_Add_AssessmentMaster_Click(object sender, EventArgs e)
    {
        AM_ID = 0;
        txtAssessmentName.Text = "";
        HiddenTrainingType.Value = "";
        divAddAssessment.Visible = true;
    }
    protected void btn_Save_AssessmentMaster_Click(object sender, EventArgs e)
    {
        if (txtAssessmentName.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter assessment";
            return;
        }

        Common.Set_Procedures("InsertUpdate_tbl_CrewAssessmentMaster");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@AM_id", AM_ID),
            new MyParameter("@AM_Name", txtAssessmentName.Text.Trim()),
            new MyParameter("@AllowOnCrewPortal", chkAllow.Checked ? 1:0));

        DataSet ds = new DataSet();
        try
        {
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                lblMsg.Text = "Record Successfully Saved.";
                txtAssessmentName.Text = "";
                AM_ID = 0;
            }
            else
            {
                lblMsg.Text = "Error while saving record. Error : "+  Common.ErrMsg;
            }
        }
        catch
        {
            lblMsg.Text = "Error while saving record.";
        }

        bindAssessmentmaster();
    }
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfAM_ID = (HiddenField)btn.Parent.FindControl("hfAM_ID");
        Label lblname = (Label)btn.Parent.FindControl("lblname");
        Label lblAllow = (Label)btn.Parent.FindControl("lblAllow");
        AM_ID = Common.CastAsInt32(hfAM_ID.Value);
        txtAssessmentName.Text = lblname.Text;
        chkAllow.Checked = lblAllow.Text == "Yes";
        divAddAssessment.Visible = true;
    }
    protected void btn_Cancel_AssessmentMaster_Click(object sender, EventArgs e)
    {
        AM_ID = 0;
        divAddAssessment.Visible = false;
    }
    public void bindAssessmentmaster()
    {
        string sql = " select * from tbl_CrewAssessmentMaster order by AM_name ";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        this.rptAssessmentMaster.DataSource = dt1;
        this.rptAssessmentMaster.DataBind();
        lblCountRegister.Text = dt1.Rows.Count.ToString() + " records found.";
    }
}