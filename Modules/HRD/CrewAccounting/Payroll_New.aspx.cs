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
using Microsoft.SqlServer.Management.Smo;
using System.Diagnostics.Contracts;

public partial class Modules_HRD_CrewAccounting_Payroll_New : System.Web.UI.Page
{
    Authority Auth;
    AuthenticationManager auth1;

    public int Vesselid
    {
        get { return Common.CastAsInt32(ViewState["Vesselid"]); }
        set { ViewState["Vesselid"] = value; }
    }
    public int Month
    {
        get { return Common.CastAsInt32(ViewState["Month"]); }
        set { ViewState["Month"] = value; }
    }
    public int Year
    {
        get { return Common.CastAsInt32(ViewState["Year"]); }
        set { ViewState["Year"] = value; }
    }
    public int PayRollID
    {
        get { return Common.CastAsInt32(ViewState["PayRollID"]); }
        set { ViewState["PayRollID"] = value; }
    }
    public int CONTRACTID
    {
        get { return Common.CastAsInt32(ViewState["CONTRACTID"]); }
        set { ViewState["CONTRACTID"] = value; }
    }
    public string CrewNo
    {
        get { return (Convert.ToString(ViewState["CrewNo"])); }
        set { ViewState["CrewNo"] = value; }
    }
    public string CrewName
    {
        get { return (Convert.ToString(ViewState["CrewName"])); }
        set { ViewState["CrewName"] = value; }
    }
    public string RankName
    {
        get { return (Convert.ToString(ViewState["RankName"])); }
        set { ViewState["RankName"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionManager.SessionCheck_New();
        //-----------------------------
        auth1 = new AuthenticationManager(32, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);

        Session["PageName"] = " - Portrage bill";
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 32);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy.aspx");
        }
        //*******************
        lbl_Message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 3);
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
            ddl_Vessel.SelectedIndex = 0;
            ddl_Month.SelectedValue = DateTime.Now.Month.ToString();
            ddl_Year.SelectedValue = DateTime.Now.Year.ToString();
            
        }
    }
    protected void bindVesselNameddl()
    {
        DataSet ds = Budget.getTable("select VesselID,VesselName as Name from dbo.Vessel where VesselStatusid<>2  ORDER BY VESSELNAME");
        ddl_Vessel.DataSource = ds.Tables[0];
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "Name";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("<Select>", "0"));
    }

    protected void SearchData(int VesselId, int Month, int Year)
    {
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.getContract_WagesHeader " + VesselId + "," + Year.ToString() + "," + Month.ToString());
        rptPersonal.DataSource = dt_Data;
        rptPersonal.DataBind();
        lblTotCrew.Text = " ( " + dt_Data.Rows.Count.ToString() + " ) Crew";
        Session["dt_Data"] = dt_Data;
    }

    protected void btnHourG_Click(object sender, EventArgs e)
    {
        int Index = Common.CastAsInt32(((ImageButton)sender).CommandArgument) - 1;
        DataRow dr_data = ((DataTable)Session["dt_Data"]).Rows[Index];
        //DataRow dr_calc = ((DataTable)Session["dt_Calc"]).Rows[Index];
        //DataRow dr_ded = ((DataTable)Session["dt_Ded"]).Rows[Index];
        //DataRow dr_Bal = ((DataTable)Session["dt_Bal"]).Rows[Index];
        int PayrollId = Common.CastAsInt32(dr_data["PayrollId"]);
        if (PayrollId <= 0)
        {
            Common.Set_Procedures("sp_InsertUpdateCrewPortageBillHeader");
            Common.Set_ParameterLength(43);
            Common.Set_Parameters(
                    new MyParameter("@PAYROLLID", dr_data["PayrollId"]),
                    new MyParameter("@VESSELID", ddl_Vessel.SelectedValue),
                    new MyParameter("@PAYMONTH", ddl_Month.SelectedValue),
                    new MyParameter("@PAYYEAR", ddl_Year.SelectedValue),
                    new MyParameter("@CREWNUMBER", dr_data["CREWNUMBER"]),
                    new MyParameter("@CREWNAME", dr_data["CREWNAME"]),
                    new MyParameter("@RANK", dr_data["RANKCODE"]),
                    new MyParameter("@FD", dr_data["STARTDAY"]),
                    new MyParameter("@TD", dr_data["ENDDAY"]),
                    new MyParameter("@ExtraOTRate", dr_data["ExtraOTRate"]),
                    new MyParameter("@CONTRACTID", dr_data["CONTRACTID"]),          
                    new MyParameter("@UNIONFEE", dr_data["UNIONFEE"]),
                    new MyParameter("@VERIFIED", "0"),
                    new MyParameter("@VERIFIEDBY", ""),
                    new MyParameter("@VERIFIEDON", DateTime.Today.ToString("dd/MMM/yyyy")),
                    new MyParameter("@AUTOSAVED", "1"));

            DataSet ds = new DataSet();
            bool res = Common.Execute_Procedures_IUD_CMS(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                PayrollId = Common.CastAsInt32(ds.Tables[0].Rows[0][0].ToString());
                dr_data["PayrollId"] = PayrollId.ToString();
            }
        }
        Session.Add("vPayrollID", PayrollId.ToString());
        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
       // SearchData(VesselId, Month, Year);

       // ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "OpenPaySlipReport(" + PayrollId + ")", true);
    }
    public void ShowData()
    {
        string StrMonthYear = "";
        //string sql = "SELECT * FROM DBO.CrewPortageBill WHERE VesselId=" + Vesselid + " AND PayMonth=" + Month + " AND PayYear=" + Year + " AND CrewNumber='" + CrewNo + "'";
        string sql = "SELECT (select VesselName from DBO.Vessel V where V.VesselID=CP.VesselID) VesselName " +
            " ,(select FirstName+' '+LastName from UserLogin U where U.LoginID=CP.Verifiedby)VerifiedbyText" +
            " ,replace(convert(varchar,verifiedOn ,106),' ','-')verifiedOnText " +
            ",* FROM DBO.CrewPortageBillHeader  CP WHERE CP.PayRollID=" + PayRollID + "";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataRow Dr = DS.Tables[0].Rows[0];

                PayRollID = Common.CastAsInt32(Dr["PAYROLLID"]);
                CrewName = Dr["CREWNAME"].ToString();
                RankName = Dr["RANK"].ToString();
                CrewNo = Dr["CREWNUMBER"].ToString();
                CONTRACTID = Common.CastAsInt32(Dr["CONTRACTID"]);
                Month = Common.CastAsInt32(Dr["PAYMONTH"]);
                Year = Common.CastAsInt32(Dr["PAYYEAR"]);
                Vesselid = Common.CastAsInt32(Dr["VESSELID"]);

                //ShowLastMonthData(Vesselid, Month, Year, CrewNo);

              //  StrMonthYear = ConvertMMMToM(Month) + " " + Year.ToString();

              //  lblThisMonthWages.Text = ConvertMMMToM(Month) + " " + Year.ToString();
              //  lblLastMonthWages.Text = Convert.ToDateTime("1-" + ConvertMMMToM(Month) + "-" + Year.ToString()).AddMonths(-1).ToString("MMM yyyy").ToUpper();

                lblWagesForDuration.Text = StrMonthYear;

                //if (Month == 1)
                //{
                //    lblBowFromDuration.Text = ConvertMMMToM(12) + " " + Convert.ToString(Year - 1);
                //}
                //else
                //    lblBowFromDuration.Text = ConvertMMMToM(Month - 1) + " " + Year.ToString();

                // Master Date
                lblName.Text = Dr["CREWNAME"].ToString();
                lblrank.Text = Dr["RANK"].ToString();
                txtFD.Text = Dr["FD"].ToString();
                txtTD.Text = Dr["TD"].ToString();
                txtOT.Text = Dr["ExtraOTdays"].ToString();
                lblVesselName.Text = Dr["VesselName"].ToString();
                lblCrewNo.Text = Dr["CREWNUMBER"].ToString();


                

                
                if (Dr["Verified"].ToString() == "True")
                {
                 //   lblVerifiedByOn.Text = Dr["VerifiedbyText"].ToString() + " / " + Dr["verifiedOnText"].ToString();
                 //   txtRemark.Text = Dr["Remarks"].ToString();
                    //btnVerify.Visible = false;
                    //trVerified.Visible = true;
                    //btnSave.Visible = false;
                    //imgUpdateBowFrom.Visible = false;
                }
                else
                {
                    //lblVerifiedByOn.Text = "";
                    //txtRemark.Text = "";
                    //btnVerify.Visible = true;
                    //trVerified.Visible = false ;
                    //imgUpdateBowFrom.Visible = true && auth.IsVerify;
                }
                //// Button Vesibility

                string sqlVesibility = "SELECT * FROM DBO.CrewPortageBillClosure WHERE VessId=" + Vesselid + " AND [Month]=" + Month + " AND [Year]=" + Year;
                DataSet DSVesibility = Budget.getTable(sqlVesibility);
                if (DSVesibility != null)
                {
                    //if (DSVesibility.Tables[0].Rows.Count > 0)
                    //{
                    //    btnSave.Visible = false;
                    //    imgUpdateBowFrom.Visible = false;
                    //}
                    //else
                    //{
                    //    btnSave.Visible = true && auth.IsUpdate;
                    //    imgUpdateBowFrom.Visible = true && auth.IsUpdate;
                    //}
                }

            }
        }


    }


    protected void btn_search_Click(object sender, EventArgs e)
    {
        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CREWPORTAGEBILLHEADERDetails WHERE PayrollId in (Select PayrollId FROM CREWPORTAGEBILLHEADER WHERE VESSELID=" + VesselId.ToString() + " AND PAYMONTH=" + Month.ToString() + " AND PAYYEAR=" + Year.ToString() + " AND AUTOSAVED=1");
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CREWPORTAGEBILLHEADER WHERE VESSELID=" + VesselId.ToString() + " AND PAYMONTH=" + Month.ToString() + " AND PAYYEAR=" + Year.ToString() + " AND AUTOSAVED=1");

        SearchData(VesselId, Month, Year);
    }
}