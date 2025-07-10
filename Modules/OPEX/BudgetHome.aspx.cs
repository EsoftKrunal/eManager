using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data ;
using System.Data.SqlClient;  


public partial class BudgetHome : System.Web.UI.Page
{
    AuthenticationManager authRecInv;
    public void Manage_Menu()
    {
        AuthenticationManager auth = new AuthenticationManager(5, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trCurrBudget.Visible = auth.IsView;
        //auth = new AuthenticationManager(28, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trAnalysis.Visible = auth.IsView;
        //auth = new AuthenticationManager(29, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trBudgetForecast.Visible = auth.IsView;
        //auth = new AuthenticationManager(30, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trPublish.Visible = auth.IsView;
    }
    protected void rdoList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoList.Items[0].Selected)
        {
            ddlFleet.Visible = true;
            ddlOwner.Visible = false;
            lblFC.Text = "Fleet";
        }
        else
        {
            ddlOwner.Visible = true;
            ddlFleet.Visible = false;
            lblFC.Text = "Company";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1066, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                Response.Redirect("~/Unauthorized.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Unauthorized.aspx");
        }
        #endregion ----------------------------------------

        //if (Session["loginid"].ToString() == "1")
        //    trBudgetTracking.Visible = true;
        //else
        //    trBudgetTracking.Visible = false;

        if (!IsPostBack)
        { 
            Manage_Menu(); 
            BindFleet();
            BindOwner();
            for (int i = DateTime.Today.Year; i >= 2006; i--)
                ddlYear.Items.Add(i.ToString());

            ddlMonth.Items.Add(new ListItem("Jan", "1"));
            ddlMonth.Items.Add(new ListItem("Feb", "2"));
            ddlMonth.Items.Add(new ListItem("Mar", "3"));
            ddlMonth.Items.Add(new ListItem("Apr", "4"));
            ddlMonth.Items.Add(new ListItem("May", "5"));
            ddlMonth.Items.Add(new ListItem("Jun", "6"));
            ddlMonth.Items.Add(new ListItem("Jul", "7"));
            ddlMonth.Items.Add(new ListItem("Aug", "8"));
            ddlMonth.Items.Add(new ListItem("Sep", "9"));
            ddlMonth.Items.Add(new ListItem("Oct", "10"));
            ddlMonth.Items.Add(new ListItem("Nov", "11"));
            ddlMonth.Items.Add(new ListItem("Dec", "12"));

            int DefaultYear = Convert.ToDateTime("01-" + ddlMonth.Items[DateTime.Now.Month - 1].Text + DateTime.Today.Year).AddMonths(-1).Year;
            int DefaultMonth = Convert.ToDateTime("01-" + ddlMonth.Items[DateTime.Now.Month - 1].Text + DateTime.Today.Year).AddMonths(-1).Month;

            ddlYear.SelectedValue = DefaultYear.ToString();
            ddlMonth.SelectedValue = DefaultMonth.ToString();
        }
    }
    // Event ----------------------------------------------------------------
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    protected void imgClear_Click(object sender, EventArgs e)
    {


    }
    // Function ----------------------------------------------------------------
    protected void BindFleet()
    {
        DataTable dtFleet = Common.Execute_Procedures_Select_ByQueryCMS("select * from FleetMaster");
        if (dtFleet != null)
        {
            if (dtFleet.Rows.Count >= 0)
            {
                ddlFleet.DataSource = dtFleet;
                ddlFleet.DataTextField = "FleetName";
                ddlFleet.DataValueField = "FleetID";
                ddlFleet.DataBind();
                ddlFleet.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select Fleet >", "0"));
            }
        }
    }
    protected void BindOwner()
    {
        try
        {
            this.ddlOwner.DataTextField = "OwnerName";
            this.ddlOwner.DataValueField = "OwnerId";
            this.ddlOwner.DataSource = Common.Execute_Procedures_Select_ByQuery("select OwnerId,OwnerName from dbo.Owner");
            this.ddlOwner.DataBind();
            this.ddlOwner.Items.Insert(0, new ListItem("< Select Owner >", "0"));
            this.ddlOwner.Items[0].Value = "0";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void BindRepeater()
    {
        DataTable Dt_Res = new DataTable();
        Dt_Res.Columns.Add("Vessel");
        Dt_Res.Columns.Add("Manning", typeof(float));
        Dt_Res.Columns.Add("Manning_amt", typeof(float));
        Dt_Res.Columns.Add("Consumables", typeof(float));
        Dt_Res.Columns.Add("Consumables_amt", typeof(float));
        Dt_Res.Columns.Add("Lube Oils", typeof(float));
        Dt_Res.Columns.Add("Lube Oils_amt", typeof(float));
        Dt_Res.Columns.Add("Spares, Maintenance & Repair", typeof(float));
        Dt_Res.Columns.Add("Spares, Maintenance & Repair_amt", typeof(float));
        Dt_Res.Columns.Add("General Expenses", typeof(float));
        Dt_Res.Columns.Add("General Expenses_amt", typeof(float));
        Dt_Res.Columns.Add("Insurance", typeof(float));
        Dt_Res.Columns.Add("Insurance_amt", typeof(float));
        Dt_Res.Columns.Add("Management & Admin Fees", typeof(float));
        Dt_Res.Columns.Add("Management & Admin Fees_amt", typeof(float));
        Dt_Res.Columns.Add("Var", typeof(float));
        Dt_Res.Columns.Add("Var_amt", typeof(float));

        //Dt_Res.Columns.Add("Manning_val", typeof(float));
        //Dt_Res.Columns.Add("Consumables_val", typeof(float));
        //Dt_Res.Columns.Add("Lube Oils_val", typeof(float));
        //Dt_Res.Columns.Add("Spares, Maintenance & Repair_val", typeof(float));
        //Dt_Res.Columns.Add("General Expenses_val", typeof(float));
        //Dt_Res.Columns.Add("Insurance_val", typeof(float));
        //Dt_Res.Columns.Add("Management & Admin Fees_val", typeof(float));
        //Dt_Res.Columns.Add("Var_val", typeof(float));

        Dt_Res.Columns.Add("Manning_tot", typeof(float));
        Dt_Res.Columns.Add("Consumables_tot", typeof(float));
        Dt_Res.Columns.Add("Lube Oils_tot", typeof(float));
        Dt_Res.Columns.Add("Spares, Maintenance & Repair_tot", typeof(float));
        Dt_Res.Columns.Add("General Expenses_tot", typeof(float));
        Dt_Res.Columns.Add("Insurance_tot", typeof(float));
        Dt_Res.Columns.Add("Management & Admin Fees_tot", typeof(float));
        Dt_Res.Columns.Add("Var_tot", typeof(float));
        //-------------------------
        string Qry = "", InnerSql = "";
        if (ddlFleet.SelectedIndex > 0)
        {
            Qry = "SELECT VESSELCODE,VESSELName FROM DBO.VESSEL WHERE FLEETID=" + ddlFleet.SelectedValue + " AND VesselStatusid<>2  ORDER BY VESSELCODE";
        }
        else if (ddlOwner.SelectedIndex > 0)
        {
            Qry = "SELECT VESSELCODE,VESSELName FROM DBO.VESSEL WHERE OwnerId=" + ddlOwner.SelectedValue + " AND VesselStatusid<>2  ORDER BY VESSELCODE";
        }
        else
        {
            rptShip.DataSource = null;
            rptShip.DataBind();
            return;
        }

        DataTable ds_Ship = Common.Execute_Procedures_Select_ByQueryCMS(Qry);
        //-------------------------
        foreach (DataRow dr in ds_Ship.Rows)
        {
            SqlConnection cn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
            SqlCommand cmd = new SqlCommand("getVariancePer_New",cn1);
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.Add(new SqlParameter("@glYear", ddlYear.SelectedValue));
            cmd.Parameters.Add(new SqlParameter("@glPer", ddlMonth.SelectedValue));
            cmd.Parameters.Add(new SqlParameter("@VSL", dr["VESSELCODE"].ToString()));
            DataTable InnerDT = new DataTable();
            cn1.Open();
            try
            {
                InnerDT.Load(cmd.ExecuteReader());
            }
            catch { }
            finally
            {
                cn1.Close();
            }
            Dt_Res.Rows.Add(Dt_Res.NewRow());
            for (int j = 0; j <= Dt_Res.Columns.Count - 1; j++)
            {
                Dt_Res.Rows[Dt_Res.Rows.Count - 1][j] = InnerDT.Rows[0][j];
            }
           
            //----------------
        }
        Dt_Res.Rows.Add(Dt_Res.NewRow());
        Dt_Res.Rows[Dt_Res.Rows.Count - 1][0] = "<b>Total : </b>";
        foreach (DataColumn dc in Dt_Res.Columns)
        {
            if (dc.ColumnName.Trim() != "Vessel")
            {
                string colname = dc.ColumnName.Trim();

                if (dc.ColumnName.EndsWith("_amt"))
                {
                    string str = "SUM([" + colname + "])";
                    decimal amt = Common.CastAsDecimal(Dt_Res.Compute(str, ""));
                    Dt_Res.Rows[Dt_Res.Rows.Count - 1][dc] = String.Format("{0:0}", amt);
                }
                else if (dc.ColumnName.EndsWith("_tot")) 
                {

                }
                else
                {
                    decimal amt = Common.CastAsDecimal(Dt_Res.Compute("SUM([" + colname + "_amt])", ""));
                    decimal totamt = 0;
                     try
                    {
                        totamt = Convert.ToDecimal(Dt_Res.Compute("SUM([" + colname + "_tot])", ""));
                    }
                    catch { }
                    if(totamt!=0)
                        Dt_Res.Rows[Dt_Res.Rows.Count - 1][dc] = String.Format("{0:0.00}", (amt*100/ totamt));
                    else
                        Dt_Res.Rows[Dt_Res.Rows.Count - 1][dc] =0;
                }
            }
        }
        
        rptShip.DataSource = Dt_Res;
        rptShip.DataBind();  
    }
    protected void MonthYearChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlOwner.SelectedIndex = 0;
        BindRepeater();
    }
    protected void ddlOwner_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0; 
        BindRepeater();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string QString="";
        if(ddlFleet.SelectedIndex >0) 
            QString = "?FleetID=" + ddlFleet.SelectedValue + "&Year=" + ddlYear.SelectedValue + "&Month=" + ddlMonth.SelectedValue;
        else
            QString = "?Owner=" + ddlOwner.SelectedValue + "&Year=" + ddlYear.SelectedValue + "&Month=" + ddlMonth.SelectedValue;
        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "window.open('Report/BudgetVarianceReportCrystal.aspx" + QString + "')", true);
    }
}