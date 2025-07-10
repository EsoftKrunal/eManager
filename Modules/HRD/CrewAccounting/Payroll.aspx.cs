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

public partial class CrewAccounting_Payroll : System.Web.UI.Page
{
    Authority Auth;
    AuthenticationManager auth1;
    public static string[] FieldNames=new string[12]; 
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
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
            btnPrint.Visible = Auth.isPrint;
            //-------------------
            ddl_Vessel.SelectedIndex = 0;
            ddl_Month.SelectedValue = DateTime.Now.Month.ToString();
            ddl_Year.SelectedValue = DateTime.Now.Year.ToString();
            DataTable dt_cmp = Budget.getTable("SELECT * FROM WAGESCALECOMPONENTS order by wagescalecomponentid").Tables[0];
            if (dt_cmp.Rows.Count > 0)
            {
                for (int i = 0;i < dt_cmp.Rows.Count; i++)
                { 
                    if (i<12)
                    {
                        FieldNames[i] = dt_cmp.Rows[i]["ComponentName"].ToString();
                    }

                }

                    //FieldNames[0] = dt_cmp.Rows[0]["ComponentName"].ToString();
                    //FieldNames[1] = dt_cmp.Rows[1]["ComponentName"].ToString();
                    //FieldNames[2] = dt_cmp.Rows[2]["ComponentName"].ToString();
                    //FieldNames[3] = dt_cmp.Rows[3]["ComponentName"].ToString();
                    //FieldNames[4] = dt_cmp.Rows[4]["ComponentName"].ToString();
                    //FieldNames[5] = dt_cmp.Rows[5]["ComponentName"].ToString();
                    //FieldNames[6] = dt_cmp.Rows[6]["ComponentName"].ToString();
                    //FieldNames[7] = dt_cmp.Rows[7]["ComponentName"].ToString();
                    //FieldNames[8] = dt_cmp.Rows[8]["ComponentName"].ToString();
                    //FieldNames[9] = dt_cmp.Rows[9]["ComponentName"].ToString();
                    //FieldNames[10] = dt_cmp.Rows[10]["ComponentName"].ToString();
                    //FieldNames[11] = dt_cmp.Rows[11]["ComponentName"].ToString();
            BtnPortageBillClosure.Visible = false;
            btnGeneratePaySlip.Visible = false; 
            }
        }
    }
    protected void btnHourG_Click(object sender, EventArgs e)
    {
        int Index =Common.CastAsInt32(((ImageButton)sender).CommandArgument)-1;
         DataRow dr_data = ((DataTable)Session["dt_Data"]).Rows[Index];
        DataRow dr_calc = ((DataTable)Session["dt_Calc"]).Rows[Index];
        DataRow dr_ded = ((DataTable)Session["dt_Ded"]).Rows[Index];
        DataRow dr_Bal = ((DataTable)Session["dt_Bal"]).Rows[Index];
        int PayrollId=Common.CastAsInt32(dr_data["PayrollId"]);
        if ( PayrollId <= 0)
        {
            Common.Set_Procedures("sp_InsertUpdateCrewPortageBill");
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
                    new MyParameter("@OT", dr_data["OTHRS"]),
                    new MyParameter("@CONTRACTID", dr_data["CONTRACTID"]),
                    new MyParameter("@CONT_COMP_1", dr_calc["CONT_COMP_1"]),
                    new MyParameter("@CONT_COMP_2", dr_calc["CONT_COMP_2"]),
                    new MyParameter("@CONT_COMP_3", dr_calc["CONT_COMP_3"]),
                    new MyParameter("@CONT_COMP_4", dr_calc["CONT_COMP_4"]),
                    new MyParameter("@CONT_COMP_5", dr_calc["CONT_COMP_5"]),
                    new MyParameter("@CONT_COMP_6", dr_calc["CONT_COMP_6"]),
                    new MyParameter("@CONT_COMP_7", dr_calc["CONT_COMP_7"]),
                    new MyParameter("@CONT_COMP_8", dr_calc["CONT_COMP_8"]),
                    new MyParameter("@CONT_COMP_9", dr_calc["CONT_COMP_9"]),
                    new MyParameter("@CONT_COMP_10", dr_calc["CONT_COMP_10"]),
                    new MyParameter("@CONT_COMP_11", dr_calc["CONT_COMP_11"]),
                    new MyParameter("@CONT_COMP_12", dr_calc["CONT_COMP_12"]),
                    new MyParameter("@MTMAllowance", dr_calc["MTMAllowance"]),
                    new MyParameter("@OTHERPAYMENTS", "0"),
                    new MyParameter("@ADDITIONALPAYMENT", "0"),
                    new MyParameter("@TOTALEARNINGS", dr_calc["TOTALEARNINGS"]),
                    new MyParameter("@ALLOTMENT", dr_ded["ALLOTMENT"]),
                    new MyParameter("@CASHADVANCE", dr_ded["CASHADVANCE"]),
                    new MyParameter("@BONDEDSTORE", dr_ded["BONDEDSTORE"]),
                    new MyParameter("@RADIOTELECALL", dr_ded["RADIOTELECALL"]),
                    new MyParameter("@OTHERDEDUCTIONS", dr_ded["OTHERDEDUCTIONS"]),
                    new MyParameter("@UNIONFEE", dr_data["UNIONFEE"]),

                    new MyParameter("@FBOWPAIDONBOARD", dr_ded["FBOWPAIDONBOARD"]),
                    new MyParameter("@TOTALDEDUCTIONS", dr_ded["TOTALDEDUCTIONS"]),
                    new MyParameter("@CURMONBAL", dr_Bal["Col1"]),
                    new MyParameter("@PrevMonBal", dr_Bal["Col2"]),
                    new MyParameter("@PAID", "0"),
                    new MyParameter("@BALANCEOFWAGES", dr_Bal["Col3"]),
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
        SearchData(VesselId, Month, Year);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "OpenPaySlipReport(" + PayrollId + ")", true); 
    }
    protected void SearchData(int VesselId,int Month,int Year)
    {
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.getContract_Wages " + VesselId + "," + Year.ToString() + "," + Month.ToString());
        rptPersonal.DataSource = dt_Data;
        rptPersonal.DataBind();
        lblTotCrew.Text = " ( " + dt_Data.Rows.Count.ToString() + " ) Crew";
        //---------------------
        DataTable dt_Calc = new DataTable();
        DataTable dt_Ded = new DataTable();
        DataTable dt_Bal = new DataTable();

        dt_Bal.Columns.Add("Col1", typeof(decimal));
        dt_Bal.Columns.Add("Col2", typeof(decimal));
        dt_Bal.Columns.Add("Col3", typeof(decimal));

        dt_Ded.Columns.Add("Allotment", typeof(decimal));
        dt_Ded.Columns.Add("CashAdvance", typeof(decimal));
        dt_Ded.Columns.Add("BondedStore", typeof(decimal));
        dt_Ded.Columns.Add("RadioTelecall", typeof(decimal));
        dt_Ded.Columns.Add("OtherDeductions", typeof(decimal));
        dt_Ded.Columns.Add("FbowPaidOnBoard", typeof(decimal));
        dt_Ded.Columns.Add("UnionFee", typeof(decimal));
        dt_Ded.Columns.Add("TotalDeductions", typeof(decimal));
        dt_Ded.Columns.Add("Paid", typeof(decimal));

        dt_Calc.Columns.Add("Cont_Comp_1", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_2", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_3", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_4", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_5", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_6", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_7", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_8", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_9", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_10", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_11", typeof(decimal));
        dt_Calc.Columns.Add("Cont_Comp_12", typeof(decimal));
        dt_Calc.Columns.Add("MTMAllowance", typeof(decimal));
        dt_Calc.Columns.Add("TotalEarnings", typeof(decimal));


        for (int i = 0; i <= dt_Data.Rows.Count - 1; i++)
        {
            dt_Calc.Rows.Add(dt_Calc.NewRow());
            decimal RowTotal = 0;
            int Fd = Common.CastAsInt32(((HiddenField)rptPersonal.Items[i].FindControl("hfdFD")).Value);
            int Td = Common.CastAsInt32(((HiddenField)rptPersonal.Items[i].FindControl("hfdTD")).Value);
            int OtHrs = Common.CastAsInt32(((HiddenField)rptPersonal.Items[i].FindControl("hfdOT")).Value);
            string CrewNo = ((Label)rptPersonal.Items[i].FindControl("lblCrewNo")).Text;
            int ContractId = Common.CastAsInt32(((HiddenField)rptPersonal.Items[i].FindControl("hfContractID")).Value);
                

            int DiffDays = Td - Fd + 1;
            if (Fd == 0 && Td == 0) DiffDays = 0;

            int MonthDays = 30;

            if (DateTime.DaysInMonth(Year, Month) == 31)
            {
                if (DiffDays == 30 || DiffDays == 31) 
                    MonthDays = DiffDays;
            }

            // ---------------------------- Calculative Wages for Current Month ----------------------------------------------------------------
            DataTable dt_WagesSaved = Budget.getTable("SELECT CONT_COMP_1 ,CONT_COMP_2 ,CONT_COMP_3 ,CONT_COMP_4 ,CONT_COMP_5 ,CONT_COMP_6 ,CONT_COMP_7 ,CONT_COMP_8 ,CONT_COMP_9 ,CONT_COMP_10 ,CONT_COMP_11 ,CONT_COMP_12 ,MTMAllowance,TOTALEARNINGS from CREWPORTAGEBILL WHERE VESSELID=" + VesselId.ToString() + " AND PAYMONTH=" + ddl_Month.SelectedValue + " AND PAYYEAR=" + ddl_Year.SelectedValue + " AND CREWNUMBER='" + CrewNo + "'").Tables[0];
            if (dt_WagesSaved.Rows.Count > 0)
            {
                DataRow dr_Wages = dt_WagesSaved.Rows[0];
                for (int c = 0; c <= dt_Calc.Columns.Count - 1; c++)
                {
                    dt_Calc.Rows[i][dt_Calc.Columns[c].ColumnName] = dr_Wages[dt_Calc.Columns[c].ColumnName];
                }
            }
            else
            {
                DataRow dr_Wages = dt_Data.Rows[i];
                for (int c = 0; c <= dt_Calc.Columns.Count - 1; c++)
                {
                    if (c == 13)  // ADDING ROW TOTAL
                        dt_Calc.Rows[i][dt_Calc.Columns[c].ColumnName] = RowTotal;
                    else if (c == 11) // Extra ot Rate
                    {
                        decimal Tmp = Math.Round((OtHrs * Common.CastAsDecimal(dr_Wages[dt_Calc.Columns[c].ColumnName])), 2);
                        RowTotal += Tmp;
                        dt_Calc.Rows[i][dt_Calc.Columns[c].ColumnName] = Tmp;
                    }
                    else // OTHER HEADS
                    {
                        decimal Tmp = Math.Round((DiffDays * Common.CastAsDecimal(dr_Wages[dt_Calc.Columns[c].ColumnName])) / MonthDays, 2);
                        RowTotal += Tmp;
                        dt_Calc.Rows[i][dt_Calc.Columns[c].ColumnName] = Tmp;
                    }
                }
            }

            // ---------------------------- Deduction for Current Month ----------------------------------------------------------------
            string sqlDED = "SELECT Allotment,CashAdvance,BondedStore,RadioTelecall,OtherDeductions,FbowPaidOnBoard,0 as UnionFee,TotalDeductions,Paid FROM DBO.PortageBillImported where EmpNumber='" + CrewNo + "' and Month=" + ddl_Month.SelectedValue + " and year=" + ddl_Year.SelectedValue + " and Vessel='" + GetVesselIDByCode(ddl_Vessel.SelectedValue) + "'";
            DataTable dt_DEDImported = Budget.getTable(sqlDED).Tables[0];
            DataTable dt_DEDSaved = Budget.getTable("SELECT Allotment,CashAdvance,BondedStore,RadioTelecall,OtherDeductions,FbowPaidOnBoard,isnull(UnionFee,0) as UnionFee,TotalDeductions,Paid from CREWPORTAGEBILL WHERE VESSELID=" + VesselId.ToString() + " AND PAYMONTH=" + ddl_Month.SelectedValue + " AND PAYYEAR=" + ddl_Year.SelectedValue + " AND CREWNUMBER='" + CrewNo + "' AND CONTRACTID=" + ContractId.ToString()).Tables[0];
            DataRow dr_Ded = null;
            int ColsCount = 0;
            if (dt_DEDSaved.Rows.Count > 0)
            {
                ColsCount = dt_DEDSaved.Columns.Count;
                dr_Ded = dt_DEDSaved.Rows[0];
            }
            else
            {
                ColsCount = dt_DEDImported.Columns.Count;
                if (dt_DEDImported.Rows.Count > 0)
                    dr_Ded = dt_DEDImported.Rows[0];
            }
            //------------------------------  
            DataRow dr_Temp = dt_Ded.NewRow();
            int CNT = 0;

            if (dr_Ded != null)
            {
                for (CNT = 0; CNT < ColsCount; CNT++)
                {
                    dr_Temp[CNT] = dr_Ded[CNT].ToString();
                }
            }
            else
            {
                for (CNT = 0; CNT < ColsCount; CNT++)
                {
                    dr_Temp[CNT] = "0";
                }
            }
            dt_Ded.Rows.Add(dr_Temp);

            // ---------------------------- Calculate Balance for Current Month ----------------------------------------------------------------
            decimal TotEarnings = Convert.ToDecimal(dt_Calc.Rows[i]["TotalEarnings"].ToString());
            decimal TotDed = Convert.ToDecimal(dt_Ded.Rows[i]["TotalDeductions"].ToString());
            decimal PrevBal = SetPrevMonthBal(Common.CastAsInt32(ddl_Vessel.SelectedValue), Common.CastAsInt32(ddl_Month.SelectedValue), Common.CastAsInt32(ddl_Year.SelectedValue), CrewNo);
            decimal TotBal = TotEarnings - TotDed;

            DataRow Drbal = dt_Bal.NewRow();
            Drbal[0] = TotBal;
            Drbal[1] = PrevBal;
            Drbal[2] = TotBal + PrevBal;
            dt_Bal.Rows.Add(Drbal);
        }

        rptWages.DataSource = dt_Data;
        rptWages.DataBind();

        rptWagesCalc.DataSource = dt_Calc;
        rptWagesCalc.DataBind();

        rptDED.DataSource = dt_Ded;
        rptDED.DataBind();

        rptBal.DataSource = dt_Bal;
        rptBal.DataBind();

        rptPayollDoc.DataSource = dt_Data;
        rptPayollDoc.DataBind();

        //---------------------------
        Session["dt_Data"] = dt_Data;
        Session["dt_Calc"] = dt_Calc;
        Session["dt_Ded"] = dt_Ded;
        Session["dt_Bal"] = dt_Bal;
        //--------------------------

        DataTable dtClos = Common.Execute_Procedures_Select_ByQueryCMS("select * from CrewportageBillClosure Where vessid=" + VesselId.ToString() + " and [month]=" + Month.ToString() + " and [year]=" + Year.ToString());
        if (dtClos.Rows.Count > 0)
        {
            BtnPortageBillClosure.Visible = false;
            btnGeneratePaySlip.Visible = true;
        }
        else
        {
            BtnPortageBillClosure.Visible = true;
            btnGeneratePaySlip.Visible = false;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CREWPORTAGEBILL WHERE VESSELID=" + VesselId.ToString() + " AND PAYMONTH=" + Month.ToString() + " AND PAYYEAR=" + Year.ToString() + " AND AUTOSAVED=1");
        btnWageContract_Click(sender, e);
        SearchData(VesselId, Month, Year);
    }
    protected void btnGeneratePaySlip_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", "window.open('../Reporting/PaySlipAll.aspx?Type=S&PayMonth=" + ddl_Month.SelectedValue + "&PayYear=" + ddl_Year.SelectedValue + "&VesselId=" + ddl_Vessel.SelectedValue + "');", true);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Reporting/WagePayableToCrew.aspx?vid=" + this.ddl_Vessel.SelectedItem.Value + "&wagemonth=" + ddl_Month.SelectedValue + "&wageyear=" + this.ddl_Year.SelectedValue);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", "window.open('../Reporting/PaySlipAll.aspx?Type=L&PayMonth=" + ddl_Month.SelectedValue + "&PayYear=" + ddl_Year.SelectedValue + "&VesselId=" + ddl_Vessel.SelectedValue + "');", true);

    }
    protected void gv_Main_PreRender(object sender, EventArgs e)
    {
        //if (ddl_Vessel.SelectedIndex <= 0 && ddl_Month.SelectedIndex <= 0 && ddl_Year.SelectedIndex <= 0)
        //{
        //    lbl_gv_Main.Text = "";
        //}
        //else if (gv_Main.Rows.Count <= 0)
        //{
        //    lbl_gv_Main.Text = "No Records Found..!";
        //}
        //else
        //{
        //    lbl_gv_Main.Text = "";
        //}
    }
    protected void btnPrintShipDetails_Click(object sender, EventArgs e)
    { 

    }
    protected void BtnPortageBillClosure_OnClick(object sender, EventArgs e)
    {
        //Validation
        bool AllVerified = true;
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.getContract_Wages " + ddl_Vessel.SelectedValue + "," + ddl_Year.SelectedValue + "," + ddl_Month.SelectedValue);
        foreach (DataRow Dr in dt_Data.Rows)
        {
            if (Dr["Verified"].ToString() == "False")
                AllVerified = false;
        }

        if (!AllVerified)
        {
            ScriptManager.RegisterStartupScript(Page,this.GetType(),"alert","alert('Payroll can not be closed.All Crew is not verified.')",true);
            return;
        }
        Common.Set_Procedures("DBOsp_CrewPortageBillClosure");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters
            (
            new MyParameter("@VessID",ddl_Vessel.SelectedValue),
            new MyParameter("@Month",ddl_Month.SelectedValue),
            new MyParameter("@Year",ddl_Year.SelectedValue),
            new MyParameter("@ClosedBy", Common.CastAsInt32(Session["loginid"]))            
            );
        DataSet ds = new DataSet();
        Boolean Res;
        Res = Common.Execute_Procedures_IUD(ds);
        if (Res)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Closure", "alert('Payroll has been closed successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Closure", "alert('Payroll could not be closed .')", true);
        }
    }
    protected void rptPayollDoc_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hfPayrollID = (HiddenField)e.Item.FindControl("hfPayrollID");
        Panel Panel1 = (Panel)e.Item.FindControl("Panel");
        HtmlContainerControl HtmlDIv = (HtmlContainerControl)e.Item.FindControl("div");

        string sql = "select * from CrewPayrollDocuments where PayrollID=" + hfPayrollID .Value+ "";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        StringBuilder sImages=new StringBuilder();
        //foreach (DataRow dr in DT.Rows)
        //{
        //    sImages.Append("<a onclick=OpenDocument(" + dr["TableID"].ToString() + ") style='cursor:pointer'>  ");
        //    sImages.Append("<img src='../Images/paperclip12.gif' title='" + dr["DocumentName"].ToString() + "'/>" + dr["DocumentName"].ToString());
        //    sImages.Append("</a>");
        //    sImages.Append("&nbsp;");
        //}

        if (DT.Rows.Count > 0)
        {
            sImages.Append("<a onclick=AddDocuments('V'," + hfPayrollID.Value + ") style='cursor:pointer'>  ");
            sImages.Append("<img src='../Images/paperclip12.gif' title='Click to view files'/>");
            sImages.Append("</a>");
            sImages.Append("&nbsp;");
        }

        if (auth1.IsUpdate)
        {
            sImages.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            sImages.Append("<a target='_blank' onclick=AddDocuments('E'," + hfPayrollID.Value + ") style='cursor:pointer'>  ");
            sImages.Append("<img src='../Images/Add12.gif'> ");
            sImages.Append("</a>");
        }

        HtmlDIv.InnerHtml = sImages.ToString();
    }
    //Function 
    public decimal SetPrevMonthBal(int VessID,int Month,int year, string CrewNo)
    {
        string sql = "select PrevMonBal from CrewPortageBill where VesselId=" + VessID + " and PayMonth=" + Month + " and PayYear=" + year + " and CrewNumber='" + CrewNo + "' ";
        DataTable dsDED = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        
        if (dsDED != null)
          if (dsDED.Rows.Count > 0)
          {
              return Convert.ToDecimal((dsDED.Rows[0]["PrevMonBal"].ToString()));
          }
        //---------------
        int PrevMonth;
        if (Month == 1)
        {
            PrevMonth = 12;
            year = year - 1;
        }
        else
        {
            PrevMonth = Month - 1;
        }
        sql = "select BalanceofWages from CrewPortageBill where VesselId=" + VessID + " and PayMonth=" + PrevMonth + " and PayYear=" + year + " and CrewNumber='" + CrewNo + "' ";
        dsDED = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dsDED != null)
            if (dsDED.Rows.Count > 0)
                return Convert.ToDecimal((dsDED.Rows[0][0].ToString() == "") ? "" : dsDED.Rows[0][0].ToString());
            else
                return 0;
        return 0;
    }
    public string GetVesselIDByCode(string VessID)
    {
        string sql = "select VesselCode from DBO.Vessel where VesselID=" + VessID + "";
        DataTable dt= Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
        }
        return "";
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

    protected void btnWageContract_Click(object sender, EventArgs e)
    {
        btnWageContract.CssClass = "selbtn";
        btnWageMonth.CssClass = "btn1";
        btnDeductions.CssClass = "btn1";
        btnBALANCE.CssClass = "btn1";
        btnDocuments.CssClass = "btn1";
        dv_4.Visible = true;
        dv_1.Visible = false;
        dv_2.Visible = false;
        dv_3.Visible = false;
        dv_5.Visible = false;

    }

    protected void btnWageMonth_Click(object sender, EventArgs e)
    {
        btnWageContract.CssClass = "btn1";
        btnWageMonth.CssClass = "selbtn";
        btnDeductions.CssClass = "btn1";
        btnBALANCE.CssClass = "btn1";
        btnDocuments.CssClass = "btn1";
        dv_4.Visible = false;
        dv_1.Visible = true;
        dv_2.Visible = false;
        dv_3.Visible = false;
        dv_5.Visible = false;
    }

    protected void btnDeductions_Click(object sender, EventArgs e)
    {
        btnWageContract.CssClass = "btn1";
        btnWageMonth.CssClass = "btn1";
        btnDeductions.CssClass = "selbtn";
        btnBALANCE.CssClass = "btn1";
        btnDocuments.CssClass = "btn1";
        dv_4.Visible = false;
        dv_1.Visible = false;
        dv_2.Visible = true;
        dv_3.Visible = false;
        dv_5.Visible = false;
    }

    protected void btnBALANCE_Click(object sender, EventArgs e)
    {
        btnWageContract.CssClass = "btn1";
        btnWageMonth.CssClass = "btn1";
        btnDeductions.CssClass = "btn1";
        btnBALANCE.CssClass = "selbtn";
        btnDocuments.CssClass = "btn1";
        dv_4.Visible = false;
        dv_1.Visible = false;
        dv_2.Visible = false;
        dv_3.Visible = true;
        dv_5.Visible = false;
    }

    protected void btnDocuments_Click(object sender, EventArgs e)
    {
        btnWageContract.CssClass = "btn1";
        btnWageMonth.CssClass = "btn1";
        btnDeductions.CssClass = "btn1";
        btnBALANCE.CssClass = "btn1";
        btnDocuments.CssClass = "selbtn";
        dv_4.Visible = false;
        dv_1.Visible = false;
        dv_2.Visible = false;
        dv_3.Visible = false;
        dv_5.Visible = true;
    }
}

