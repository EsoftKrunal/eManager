using System;
using System.Data;
using System.Configuration;
using System.Reflection;
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
public partial class Vessel_BudgetManipulation : System.Web.UI.Page
{
    #region PAGE LOADER
    public void bindddl_VesselName()
    {
        DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_VesselName.DataSource = dt8;
        this.ddl_VesselName.DataValueField = "VesselId";
        this.ddl_VesselName.DataTextField = "Name";
        this.ddl_VesselName.DataSource = dt8;
        this.ddl_VesselName.DataBind();
        ddl_VesselName.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void BindBudgetTypeDropDown()
    {
        DataSet ds = Budget.getVesselBudgetType("getVesselBudgetType");
        this.ddlBudgetType.DataValueField = "VesselBudgetTypeId";
        this.ddlBudgetType.DataTextField = "VesselBudgetTypeName";
        this.ddlBudgetType.DataSource = ds.Tables[0];
        this.ddlBudgetType.DataBind();
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ////***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 36);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("../AuthorityError.aspx");

        //}
        //*******************
        try
        {
            if (Session["UserName"] == "")
            {
                Response.Redirect("default.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("default.aspx");
            return;
        }
        if (!(IsPostBack))
        {
            bindddl_VesselName();
            BindBudgetTypeDropDown();
            ddlYear.Items.Add((DateTime.Today.Year-1).ToString());
            ddlYear.Items.Add(DateTime.Today.Year.ToString());
            ddlYear.Items.Add((DateTime.Today.Year+1).ToString());
            
            ddl_VesselName.SelectedValue = Request.QueryString["Vid"];
            ddlYear.SelectedValue = Request.QueryString["yr"];
            ddlBudgetType.SelectedValue = Request.QueryString["bt"];
        }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        float total = 0;
       DataSet ds = Budget.getTable("SELECT VESSELBUDGETID,(select case when vesselbudget.budgettype=1 then accountheadnumbercostplus else accountheadnumbercls end from accounthead ah where ah.accountheadid=vesselbudget.accountheadid) as AccountHead,CONVERT(NUMERIC(8,0),JAN) AS JAN,CONVERT(NUMERIC(8,0),FEB) AS FEB,CONVERT(NUMERIC(8,0),MAR) AS MAR,CONVERT(NUMERIC(8,0),APR) AS APR,CONVERT(NUMERIC(8,0),MAY) AS MAY,CONVERT(NUMERIC(8,0),JUN) AS JUN,CONVERT(NUMERIC(8,0),JUL) AS JUL,CONVERT(NUMERIC(8,0),AUG) AS AUG,CONVERT(NUMERIC(8,0),SEP) AS SEP,CONVERT(NUMERIC(8,0),OCT) AS OCT,CONVERT(NUMERIC(8,0),NOV) AS NOV,CONVERT(NUMERIC(8,0),DEC) AS DEC,CONVERT(NUMERIC(10,0),(JAN+FEB+MAR+APR+MAY+JUN+JUL+AUG+SEP+OCT+NOV+DEC)) AS TOTAL FROM VESSELBUDGET WHERE VESSELID=" + ddl_VesselName.SelectedValue + " AND BUDGETTYPE=" + ddlBudgetType.SelectedValue + " AND BUDGETYEAR=" + ddlYear.SelectedValue);
       gv_Budget.DataSource = ds;
       for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
       {
           total = total + float.Parse(ds.Tables[0].Rows[i]["Total"].ToString());   
       }
       lbl_Total.Text = total.ToString();  
       gv_Budget.DataBind();  
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= gv_Budget.Rows.Count - 1; i++)
        {
            Int32 VbId = Int32.Parse(((HiddenField)gv_Budget.Rows[i].FindControl("hfd_VBId")).Value);

            Budget.UpdateBudget(VbId, getValue(i, 0), getValue(i, 1), getValue(i, 2), getValue(i, 3), getValue(i, 4), getValue(i, 5), getValue(i, 6), getValue(i, 7), getValue(i, 8), getValue(i, 9), getValue(i, 10), getValue(i, 11), Convert.ToInt32(Session["loginid"].ToString()));
        }
        btn_Show_Click(sender, e); 
    }
    public Int32 getValue(Int32 row,int col)
    {
        Int32 ret = 0;
        string str = ((TextBox)gv_Budget.Rows[row].FindControl("txt_" + col.ToString() )).Text;
        try
        {
            ret = Int32.Parse(str); 
        }
        catch
        {}
        return ret;
    }
}