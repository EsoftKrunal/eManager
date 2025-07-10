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
public partial class VesselBudget : System.Web.UI.UserControl
{
    #region Declare Property
    private int _vesselid;
    public int Vesselid
    {
        get {return _vesselid; }
        set {  _vesselid = value; }
    }

    #endregion
    Authority Auth;
    string Mode;
    protected void Page_Load(object sender, EventArgs e)
    {
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 38);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        // CODE FOR UDATING THE AUTHORITY
        ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        Obj.Invoke();
        try
        {
            Mode = Session["VMode"].ToString();
        }
        catch { Mode = "New"; }
        Session["Authority"] = Obj.Authority;
        Auth =(Authority) Session["Authority"];
        if (!Page.IsPostBack)
        {
            //********* Code To Bind DropDown
            BindFlagNameDropDown();
            BindYearDropDown();
            BindVesselDropDown();
            BindBudgetTypeDropDown();
            ddlVesselYear.SelectedValue = DateTime.Today.Year.ToString();
            ddl_B_Year.SelectedValue = DateTime.Today.Year.ToString();
            ddl_B_Year_SelectedIndexChanged(sender,e);
            showdata(Convert.ToInt32(ddlVesselYear.SelectedValue), Convert.ToInt32(ddlBudgetType.SelectedValue));   
            btn_save.Visible = ((Mode == "New") || (Mode == "Edit")) && ((Auth.isAdd) || (Auth.isEdit));
            showdata(Convert.ToInt32(ddl_B_Year.SelectedValue), Convert.ToInt32(ddlBudgetType.SelectedValue));
            showdata1(Convert.ToInt32(ddl_B_Year.SelectedValue), Convert.ToInt32(ddlBudgetType.SelectedValue));
            showdata1(Convert.ToInt32(ddlVesselYear.SelectedValue), Convert.ToInt32(ddlBudgetType.SelectedValue));   
        }
        try
        {
            txtVesselName.Text = Session["VesselName"].ToString();
            txtFormerVesselName.Text = Session["FormerName"].ToString();
            ddlFlagStateName.SelectedValue = Session["FlagStateId"].ToString();
            btn_Print.Visible = Auth.isPrint;
            if (ddlBudgetType.SelectedIndex <= 0)
            {
                gv_VDoc.DataBind();
            }
        }
        catch
        {
           
        }
    }
    protected void btn_refresh_Click(object sender, EventArgs e)
    {
        showdata(Convert.ToInt32(ddl_B_Year.SelectedValue), Convert.ToInt32(ddlBudgetType.SelectedValue));
    }
    protected void btn_manip_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Budgetmanipulation.aspx");  
        Page.ClientScript.RegisterStartupScript(this.GetType(), "op", "window.open('Budgetmanipulation.aspx?Vid=" + this.Vesselid.ToString() + "&yr=" + ddl_B_Year.SelectedValue + "&bt=" + ddlBudgetType.SelectedValue + "');", true);  
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        int acid,jan,feb,mar,apr,may,jun,jul,aug,sep,oct,nov,dec;
        
        for (int i = 0; i <= gv_VDoc.Rows.Count - 1; i++)
        {
            HiddenField hfd;
            int FM,TM;
            TextBox txt;
            DropDownList ddl;
            double Amount;
            double[] Month=new double[12];
            hfd = (HiddenField)gv_VDoc.Rows[i].FindControl("HiddenId");   
            acid=Convert.ToInt32(hfd.Value);
            try
            {
                FM = Convert .ToInt32(  ddl_FromMonth.SelectedValue  );
                TM = Convert.ToInt32( ddl_ToMonth.SelectedValue );
                txt = (TextBox)gv_VDoc.Rows[i].FindControl("txt_Amoount");
                Amount =Math.Abs(Convert.ToInt32(txt.Text)); 
                Amount = Convert.ToDouble(Math.Round(Convert.ToDecimal(Amount/(TM-FM+1)),0));

                if (Amount < 0){ continue; }

                for (int j = 1; j <= 12; j++)
                {
                    
                    if (j < FM)
                    {
                        Month[j - 1] = -1;
                    }
                    else if ((j >= FM && j <= TM))
                    {
                        Month[j - 1] = Amount;
                    }
                    else
                    {
                        Month[j - 1] = 0;
                    }
                }

                jan = Convert.ToInt32(Month[0]);
                feb = Convert.ToInt32(Month[1]);
                mar = Convert.ToInt32(Month[2]);
                apr = Convert.ToInt32(Month[3]);
                may = Convert.ToInt32(Month[4]);
                jun = Convert.ToInt32(Month[5]);
                jul = Convert.ToInt32(Month[6]);
                aug = Convert.ToInt32(Month[7]);
                sep = Convert.ToInt32(Month[8]);
                oct = Convert.ToInt32(Month[9]);
                nov = Convert.ToInt32(Month[10]);
                dec = Convert.ToInt32(Month[11]);
                //****************
                //if (Session["btn_Copy_Session"].ToString() == "1")
                //{
                //    Budget.insertRow("InsertUpdateVesselBudget", Convert.ToInt32(ddlBudgetType.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue), acid, Convert.ToInt32(ddl_B_Year.SelectedValue), Convert.ToInt32(ddl_FromMonth.SelectedValue), Convert.ToInt32(ddl_ToMonth.SelectedValue), jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec, Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["loginid"].ToString()));
                //}
                //else
                //{
                    Budget.insertRow("InsertUpdateVesselBudget", Convert.ToInt32(ddlBudgetType.SelectedValue), Vesselid, acid, Convert.ToInt32(ddl_B_Year.SelectedValue), Convert.ToInt32(ddl_FromMonth.SelectedValue), Convert.ToInt32(ddl_ToMonth.SelectedValue), jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec, Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["loginid"].ToString()));
                //}
                //****************
            }
            catch
            {

            }
        }
        showdata(Convert.ToInt32(ddl_B_Year.SelectedValue), Convert.ToInt32(ddlBudgetType.SelectedValue));
        lbl_message_budget.Visible = true;
    }
    private void showdata(int year, int budgettype)
    {
        DataSet ds = Budget.getData("getVesselBudget", Vesselid,year,budgettype);
        gv_VDoc.DataSource = ds;
        gv_VDoc.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (!(Convert.IsDBNull(ds.Tables[0].Rows[0]["FromDate"])))
            {
                ddl_FromMonth.SelectedValue = ds.Tables[0].Rows[0]["FromDate"].ToString();
                ddl_ToMonth.SelectedValue = ds.Tables[0].Rows[0]["ToDate"].ToString();
                ddlBudgetType.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["BudgetType"].ToString());
            }
            else
            {
                ddl_FromMonth.SelectedIndex = 0;
                ddl_ToMonth.SelectedIndex = 0;
                ddlBudgetType.SelectedIndex = 0; 
            }
        }
        if (ddlBudgetType.SelectedIndex <= 0)
        {
            gv_VDoc.DataSource = null;
            gv_VDoc.DataBind();
        }
    }
    private void BindFlagNameDropDown()
    {
        DataTable dt1 = VesselDetailsGeneral.selectDataFlag();
        this.ddlFlagStateName.DataValueField = "FlagStateId";
        this.ddlFlagStateName.DataTextField = "FlagStateName";
        this.ddlFlagStateName.DataSource = dt1;
        this.ddlFlagStateName.DataBind();
    }
    private void BindVesselDropDown()
    {
        DataSet ds = Budget.getAllVessels("SelectAllVesels", Vesselid);   
        this.ddlVessel.DataValueField = "VesselId";
        this.ddlVessel.DataTextField = "VesselName";
        this.ddlVessel.DataSource = ds.Tables[0];
        this.ddlVessel.DataBind();
    }
    private void BindBudgetTypeDropDown()
    {
        DataSet ds = Budget.getVesselBudgetType("getVesselBudgetType");
        this.ddlBudgetType.DataValueField = "VesselBudgetTypeId";
        this.ddlBudgetType.DataTextField = "VesselBudgetTypeName";
        this.ddlBudgetType.DataSource = ds.Tables[0];
        this.ddlBudgetType.DataBind();
    }
    private void BindYearDropDown()
    {
        int i;
        i = DateTime.Today.Year;   
        ddlVesselYear.Items.Add(new ListItem((i-1).ToString(), (i-1).ToString()));  
        ddlVesselYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
        ddlVesselYear.Items.Add(new ListItem((i+1).ToString(), (i+1).ToString()));  
        ddl_B_Year.Items.Add(new ListItem((i - 1).ToString(), (i-1).ToString()));
        ddl_B_Year.Items.Add(new ListItem(i.ToString(), i.ToString()));
        ddl_B_Year.Items.Add(new ListItem((i + 1).ToString(), (i+1).ToString()));
    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        DataSet ds = Budget.getData1("getVesselBudget1", Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(ddlVesselYear.SelectedValue),Convert.ToInt32(ddlBudgetType.SelectedValue));
        gv_VDoc.DataSource = ds;
        gv_VDoc.DataBind();
        Session["btn_Copy_Session"] = "1";
    }
    protected void ddl_B_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        showdata(Convert.ToInt32(ddl_B_Year.SelectedValue), Convert.ToInt32(ddlBudgetType.SelectedValue));
    }
    protected void gv_VDoc_DataBound(object sender, EventArgs e)
    {
        double d;
        d = 0;
        for (int i = 0; i <= gv_VDoc.Rows.Count - 1; i++)
        {
            d = d + Convert.ToDouble(gv_VDoc.Rows[i].Cells[gv_VDoc.Columns.Count - 1].Text);     
        }
        lbl_Total.Text = d.ToString(); 
    }
    private void showdata1(int year, int budgettype)
    {
        DataSet ds = Budget.getData("getVesselBudget", Vesselid, year, budgettype);
        gv_VDoc.DataSource = ds;
        gv_VDoc.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (!(Convert.IsDBNull(ds.Tables[0].Rows[0]["FromDate"])))
            {
                ddl_FromMonth.SelectedValue = ds.Tables[0].Rows[0]["FromDate"].ToString();
                ddl_ToMonth.SelectedValue = ds.Tables[0].Rows[0]["ToDate"].ToString();
                //ddlBudgetType.SelectedIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["BudgetType"].ToString());
            }
            else
            {
                ddl_FromMonth.SelectedIndex = 0;
                ddl_ToMonth.SelectedIndex = 0;
                //ddlBudgetType.SelectedIndex = 0;
            }
        }
    }
    protected void ddlBudgetType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBudgetType.SelectedIndex > 0)
        {
            showdata1(Convert.ToInt32(ddl_B_Year.SelectedValue), Convert.ToInt32(ddlBudgetType.SelectedValue));
        }
        else
        {
            gv_VDoc.DataBind();
        }
    }
}