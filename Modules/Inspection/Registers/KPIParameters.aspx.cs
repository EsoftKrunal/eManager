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

public partial class KPIParameters : System.Web.UI.Page
{
    public int KPIID
    {
        set { ViewState["KPIID"] = value;}
        get { return int.Parse("0" + ViewState["KPIID"]);}
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
        lblMsg.Text = "";
        lblMessege.Text = "";
        if (!Page.IsPostBack)
        {
            BindYear();
            ddlYear.SelectedValue = DateTime.Today.Year.ToString(); ;
            bindKPI();
        }
    }

    // --------  Events
    protected void btn_Save_InspGrp_Click(object sender, EventArgs e)
    {

        Common.Set_Procedures("dbo.PR_ADMS_m_KPIParameters");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@ID", KPIID),
            new MyParameter("@KPI", txtMTMKPI.Text.Trim())
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
        KPIID = 0;
        bindKPI();
        DivAddNewHead.Visible = false;
    }
    protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindKPI();
    }


    protected void btnAddNewHead_Click(object sender, EventArgs e)
    {
        KPIID = 0;
        txtMTMKPI.Text = "";
        DivAddNewHead.Visible = true;
    }
    protected void btnCloseAddHeadPopup_Click(object sender, EventArgs e)
    {
        DivAddNewHead.Visible = false;
    }

    protected void btnSaveKPIParameterValue_OnClick(object sender, EventArgs e)
    {
        foreach (RepeaterItem rptItm in rptKPIValues.Items)
        {
            TextBox txtPV = (TextBox)rptItm.FindControl("txtKPIValues");
            HiddenField hfKPIID= (HiddenField)rptItm.FindControl("hfKPIID");

            Common.Set_Procedures("dbo.PR_ADMS_m_KPIParametersValue");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                    new MyParameter("@KPID", hfKPIID.Value),
                    new MyParameter("@KPIYEAR", ddlYear.SelectedValue),
                    new MyParameter("@KPIVALUE", txtPV.Text)
                );
            Boolean Res;
            DataSet DS = new DataSet();
            Res = Common.Execute_Procedures_IUD(DS);
        }
        bindKPI();
        lblMsg.Text = " Data saved successfully.";
    }
    protected void lnkAddParameterValues_OnClick(object sender, EventArgs e)
    {
        lblYear.Text = "Year : "+ddlYear.SelectedValue;
        DivAddChamgeParameterValues.Visible = true;
    }
    protected void btnCloseKPIParameterValue_OnClick(object sender, EventArgs e)
    {
        DivAddChamgeParameterValues.Visible = false;
    }
    
    

    protected void BtnEdit_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfKPIID = (HiddenField)btn.Parent.FindControl("hfKPIID");
        Label lblKPI = (Label)btn.Parent.FindControl("lblKPI");

        KPIID = Common.CastAsInt32(hfKPIID.Value);
        txtMTMKPI.Text = lblKPI.Text;
        DivAddNewHead.Visible = true;
    }
    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfKPIID = (HiddenField)btn.Parent.FindControl("hfKPIID");

        string sql = "DELETE FROM m_KPIParameters WHERE ID=" + Common.CastAsInt32(hfKPIID.Value);
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        lblMessege.Text = "Data deleted successfully.";
        bindKPI();
    }


    protected void GridView_InsGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_InsGrp.PageIndex = e.NewPageIndex;
        GridView_InsGrp.SelectedIndex = -1;
        bindKPI();
    }
    
    // --------  Finction
    public void bindKPI()
    {
        string sql = "select row_number() over(order by id )Row,ID,KPI, " +
             " (select KPIValue from m_KPIParametersValue KPV where KPV.KPID=KP.ID and KPV.KPIYEAR=" + ddlYear.SelectedValue + ")KPIVALUE " +
             " from m_KPIParameters KP";

        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            this.GridView_InsGrp.DataSource = Dt;
            this.GridView_InsGrp.DataBind();


            this.rptKPIValues.DataSource = Dt;
            this.rptKPIValues.DataBind();
        }
    }
    public void BindYear()
    {
        int CY=DateTime.Today.Year;
        for (CY = DateTime.Today.Year-1; CY <= DateTime.Today.Year+1; CY++)
        {
            if (ddlYear.Items.Count == 0)
                ddlYear.Items.Add(CY.ToString());
            else
                ddlYear.Items.Insert(0, CY.ToString());
        }
    }
}
