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

public partial class Registers_VesselReportGroup : System.Web.UI.Page
{
    public int GroupId
    {
        set { ViewState["GroupId"] = value; }
        get { return Common.CastAsInt32(ViewState["GroupId"]); }
    }
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1101);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------

        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 10);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        lblMessege.Text = "";
        if (!Page.IsPostBack)
        {
            BindGroups();
        }
    }

    public void BindGroups()
    {
        string sql = "select row_number() over(order by GroupId )Row, GroupId, GroupName from m_VesselReportGroups ORDER BY GroupName ";

        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            grdReportsCode.DataSource = Dt;
            grdReportsCode.DataBind();
        }


    }

    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        string SQL = "SELECT * FROM [dbo].[m_VesselReportGroups] WHERE [GroupName]= '" + txtGroupName.Text.Trim() + "' AND [GroupId] <> " + GroupId.ToString() ;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            lblMessege.Text = "Please check! Group already exists.";
            txtGroupName.Focus();
            return;
        }

        Common.Set_Procedures("dbo.sp_IU_VesselReportGroupMaster");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@GroupId", GroupId),
            new MyParameter("@GroupName", txtGroupName.Text.Trim())
        );
        Boolean Res;
        DataSet Ds = new DataSet();
        Res = Common.Execute_Procedures_IUD(Ds);
        if (Res)
        {
            lblMessege.Text = "Data saved successfully.";
            btnCancel_Click(sender, e);
            BindGroups();
        }
        else
        {
            lblMessege.Text = "Data could not be saved.";
        }
    }
    protected void BtnEdit_OnClick(object sender, EventArgs e)
    {
        GroupId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM m_VesselReportGroups WHERE GroupId=" + GroupId.ToString());
        if (dt.Rows.Count > 0)
        {
            txtGroupName.Text = dt.Rows[0]["GroupName"].ToString();
            btnAddGroup.Text = "Update Group";
            btnCancel.Visible = true;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {   
        GroupId = 0;
        txtGroupName.Text = "";
        btnAddGroup.Text = "Add Group";
        btnCancel.Visible = false;
    }
}