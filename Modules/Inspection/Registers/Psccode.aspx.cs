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

public partial class Psccode : System.Web.UI.Page
{
    public int PSCCode
    {
        set { ViewState["PSCCode"] = value; }
        get { return int.Parse("0" + ViewState["PSCCode"]); }
    }

    Authority Auth;
    string Mode = "New";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 152);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
   
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 70);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("Dummy.aspx");
        //}
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 12);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        lblMessege.Text = "";
        if (!Page.IsPostBack)
        {
            bindPscCode();
        }
    }

    // --------  Events
    protected void btnSavePscCode_OnClick(object sender, EventArgs e)
    {

        Common.Set_Procedures("dbo.sp_IU_m_PscCode");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@ID", PSCCode),
            new MyParameter("@PSCCODE", txtPscCode.Text.Trim()),
            new MyParameter("@DESCRIPTION", txtDescription.Text.Trim())
        );
        Boolean Res;
        DataSet Ds = new DataSet();
        Res = Common.Execute_Procedures_IUD(Ds);
        if (Res)
        {
            lblMessege.Text = "Data saved successfully.";
        }
        else
        {
            lblMessege.Text = "Data could not be saved.";
        }
        PSCCode = 0;
        bindPscCode();
        DivAddPscCode.Visible = false;
    }

    protected void btnAddPscCode_OnClick(object sender, EventArgs e)
    {
        PSCCode = 0;
        txtPscCode.Text = "";
        txtDescription.Text = "";

        DivAddPscCode.Visible = true;
    }
    protected void btnCloseAddHeadPopup_Click(object sender, EventArgs e)
    {
        DivAddPscCode.Visible = false;
    }

    protected void BtnEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfPscCode = (HiddenField)btn.Parent.FindControl("hfPscCode");
        Label lblPscCode = (Label)btn.Parent.FindControl("lblPscCode");
        Label lblDescription = (Label)btn.Parent.FindControl("lblDescription");

        PSCCode = Common.CastAsInt32(hfPscCode.Value);
        txtPscCode.Text = lblPscCode.Text;
        txtDescription.Text = lblDescription.Text;

        DivAddPscCode.Visible = true;
    }
    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfKPIID = (HiddenField)btn.Parent.FindControl("hfKPIID");

        string sql = "DELETE FROM m_KPIParameters WHERE ID=" + Common.CastAsInt32(hfKPIID.Value);
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        lblMessege.Text = "Data deleted successfully.";
        bindPscCode();
    }


    protected void GridView_InsGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPscCode.PageIndex = e.NewPageIndex;
        grdPscCode.SelectedIndex = -1;
        bindPscCode();
    }
    
    // --------  Finction
    public void bindPscCode()
    {
        string sql = "select row_number() over(order by id )Row,ID, PSCCODE ,DESCRIPTION from m_PscCode ";

        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            this.grdPscCode.DataSource = Dt;
            this.grdPscCode.DataBind();
        }
    }
}
