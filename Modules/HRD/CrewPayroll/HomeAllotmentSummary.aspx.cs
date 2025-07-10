using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class Modules_HRD_CrewPayroll_HomeAllotmentReport : System.Web.UI.Page
{
    Authority Auth;
    AuthenticationManager auth1;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        SessionManager.SessionCheck_New();
        //-----------------------------
        auth1 = new AuthenticationManager(4, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);

        Session["PageName"] = " - Portrage bill";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");
        }
        //*******************
        lbl_Message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            Session.Remove("vPayrollID");
            bindVesselNameddl();
            for (int i = DateTime.Today.Year; i >= 2009; i--)
                ddl_Year.Items.Add(new ListItem(i.ToString(), i.ToString()));
            //  btnPrint.Visible = Auth.isPrint;
            //-------------------
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            int PreviousMonth = 0;
            int previousYear = 0;
            if (currentMonth == 1)
            {
                PreviousMonth = 12;
                previousYear = currentYear - 1;
            }
            else
            {
                PreviousMonth = currentMonth - 1;
                previousYear = currentYear;
            }

            ddl_Vessel.SelectedIndex = 0;
            ddl_Month.SelectedValue = PreviousMonth.ToString();
            ddl_Year.SelectedValue = previousYear.ToString();

        }
    }

    protected void bindVesselNameddl()
    {
        //DataSet ds = Budget.getTable("select VesselID,VesselName as Name from dbo.Vessel where VesselStatusid<>2  ORDER BY VESSELNAME");
        DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataSource = ds.Tables[0];
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "Name";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("<Select>", "0"));
    }

    protected void SearchData(int VesselId, int Month, int Year)
    {
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.GetPayrollSummaryReport " + VesselId + "," + Year.ToString() + "," + Month.ToString() + ",2");
        rptPersonal.DataSource = dt_Data;
        rptPersonal.DataBind();
        lblTotCrew.Text = " ( " + dt_Data.Rows.Count.ToString() + " ) Crew";
       // Session["dt_Data"] = dt_Data;
        if (dt_Data.Rows.Count > 0)
        {
            decimal TotalRegularAllotment = 0;
            decimal TotalSpecialAllotment = 0;
            //decimal TotalCashAdvance = 0;
            //decimal TotalBondedStores = 0;
            
            foreach (DataRow dr in dt_Data.Rows)
            {
                TotalRegularAllotment = TotalRegularAllotment + Math.Round(Common.CastAsDecimal(dr["Regular_Allotment"].ToString()), 2);
                TotalSpecialAllotment = TotalSpecialAllotment + Math.Round(Common.CastAsDecimal(dr["Special_Allotment"].ToString()), 2);
                //TotalCashAdvance = TotalCashAdvance + Math.Round(Common.CastAsDecimal(dr["Cash_Advance"].ToString()), 2);
                //TotalBondedStores = TotalBondedStores + Math.Round(Common.CastAsDecimal(dr["Bonded_Stores"].ToString()), 2);

            }

            lblTotalRegularAllotment.Text = Math.Round(TotalRegularAllotment, 2).ToString();
            lblTotalSpecialAllotment.Text = Math.Round(TotalSpecialAllotment, 2).ToString();
            //lblTotalCashAdvance.Text = Math.Round(TotalCashAdvance, 2).ToString();
            //lblTotalBondedStores.Text = Math.Round(TotalBondedStores, 2).ToString();

        }

    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        rptPersonal.DataSource = null;
        rptPersonal.DataBind();
        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        SearchData(VesselId, Month, Year);
    }
}