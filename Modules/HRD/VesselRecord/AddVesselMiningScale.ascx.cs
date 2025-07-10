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

using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
public partial class VesselRecord_AddVesselMiningScale : System.Web.UI.UserControl
{
    #region Declare Property
    private int _vesselid;
    public int Vesselid
    {
        get { try { return Convert.ToInt32(ViewState["VesselId"].ToString()); } catch { return 0; } }
        set { ViewState["VesselId"] = value; }
    }

    #endregion
    Authority Auth;
    private void BindFlagNameDropDown()
    {
        DataTable dt1 = VesselDetailsGeneral.selectDataFlag();
        //this.ddlFlagStateName.DataValueField = "FlagStateId";
        //this.ddlFlagStateName.DataTextField = "FlagStateName";
        //this.ddlFlagStateName.DataSource = dt1;
        //this.ddlFlagStateName.DataBind();
    }
    
    protected void btn_Import_Click(object sender, EventArgs e)
    {
        string sql = "delete from VESSELBUDGETMANNING where vesselid=" + Session["VesselId"].ToString() + " and byear=" + DateTime.Today.Year;
        Common.Execute_Procedures_Select_ByQueryCMS(sql);
        sql = "insert into VESSELBUDGETMANNING select Vesselid,RankId,BudgetManning,BudgetNationality," + DateTime.Today.Year.ToString() + ",Wages from VESSELBUDGETMANNING where vesselid = " + Session["VesselId"].ToString() + " and byear = " + (DateTime.Today.Year-1).ToString() + "";
        Common.Execute_Procedures_Select_ByQueryCMS(sql);

        VesselBudgetManning1.BudgetYear = Common.CastAsInt32(ddlYear.SelectedValue);
        VesselBudgetManning1.showdata();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 41);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        Obj.Invoke();
        Session["Authority"] = Obj.Authority;
        Auth = (Authority)Session["Authority"];
        if (!IsPostBack)
        {
            try
            {
                BindYear();
                BindFlagNameDropDown();
                VesselBudgetManning1.BudgetYear = Common.CastAsInt32(ddlYear.SelectedValue);
            }
            catch { }
            btnImport.Visible = Auth.isEdit || Auth.isAdd;
            VesselBudgetManning1.Vesselid = this.Vesselid;
            VesselSafeManning1.Vesselid = this.Vesselid;
            pan_BudgetManning.Visible = false;   
        }
        //**********
    }
    protected void ManningType_Changed(object sender, EventArgs e)
    {
        pan_SafeManning.Visible = Rad_Manning.SelectedValue == "S";
        pan_BudgetManning.Visible = Rad_Manning.SelectedValue == "B";
        tblYear.Visible = Rad_Manning.SelectedValue == "B";
    }
    protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        VesselBudgetManning1.BudgetYear = Common.CastAsInt32(ddlYear.SelectedValue);
        VesselBudgetManning1.showdata();
    }
    public void BindYear()
    {
        for (int i = DateTime.Today.Year; i >= 2017; i--)
        {
            ddlYear.Items.Add(i.ToString());
        }
    }
}

