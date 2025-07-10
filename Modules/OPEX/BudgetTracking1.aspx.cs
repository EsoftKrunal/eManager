using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Configuration;


public partial class BudgetTracking1 : System.Web.UI.Page
{
    // PAGE PROPERTIES ------------------
    AuthenticationManager authRecInv;
    public string SelCompanyCode
    {
        get
        { return Convert.ToString(ViewState["SelCompanyCode"]); }
        set
        {ViewState["SelCompanyCode"] = value;}
    }    
    public string SelVesselCode
    {
        get
        { return  Convert.ToString(ViewState["SelVesselCode"]); }
        set
        { ViewState["SelVesselCode"] = value; }
    }
    public int SelBidID
    {
        get
        { return Common.CastAsInt32(ViewState["_SelBidID"]); }
        set
        { ViewState["_SelBidID"] = value; }
    }
    public string BudgetYear
    {
        get
        { return Convert.ToString(ViewState["_BudgetYear"]); }
        set
        { ViewState["_BudgetYear"] = value; }
    }
      public int TaskID
    {
        get
        { return Common.CastAsInt32(ViewState["_TaskID"]); }
        set
        { ViewState["_TaskID"] = value; }
    }
    public int SelAccountID
    {
        get
        { return int.Parse("0" + ViewState["SelAccountID"]); }
        set
        { ViewState["SelAccountID"] = value; }
    }
    public string SelClosedBy
    {
        get
        { return Convert.ToString(ViewState["SelClosedBy"]); }
        set
        { ViewState["SelClosedBy"] = value; }
    } 

    public bool IndianFinYear
    {
        get
        { return Convert.ToBoolean(ViewState["IndianFinYear"]); }
        set
        { ViewState["IndianFinYear"] = value; }
    }
    public DataSet DsTrackingData;
    //DataSet DsValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();

        if (Request.Form["company"] != null && Request.Form["vessel"] != null && Request.Form["year"] != null && Request.Form["majcatid"] != null && Request.Form["IsIndianFinYear"] != null  && (!IsPostBack))
        {
            string company = Request.Form["company"];
            string vessel = Request.Form["vessel"];
            string year = Request.Form["year"];
            int majcatid = Common.CastAsInt32(Request.Form["majcatid"]);
            int midcatid = Common.CastAsInt32(Request.Form["midcatid"]);
            int mincatid = Common.CastAsInt32(Request.Form["mincatid"]);
            bool IsIndianFinYear =  Convert.ToBoolean(Request.Form["IsIndianFinYear"]);
            int month = 0;
            if (Request.Form["month"] != null)
            {
                month = Common.CastAsInt32(Request.Form["month"]);
            }
            
            if (mincatid > 0)
                WriteInResponse(getAccounts(company, vessel, year, majcatid, midcatid, mincatid, month, IsIndianFinYear));
            if (midcatid > 0)
                WriteInResponse(getMinCategory(company, vessel, year, majcatid, midcatid, month, IsIndianFinYear));
            else if (majcatid > 0)
                WriteInResponse(getMidCategory(company, vessel, year, majcatid, month, IsIndianFinYear));

            return;
        }



        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1071, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('You have not permissions to access this page.');window.close();", true);
		return;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Your session is expired.');window.close();", true);
		return;
        }

        #endregion ----------------------------------------

        lblMsgTrackingTask.Text = "";
        lblMsgTaskListPopup.Text = "";
        if (!Page.IsPostBack)
        {
            BudgetYear = Convert.ToString(DateTime.Today.Year);

            //for (int i = DateTime.Today.Year; i >= 2016; i--)
            //    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

            lblBudgetYear.Text = BudgetYear.ToString();
            BindCompany();
            BindddlMonth();
        }
    }
    
    
    public void WriteInResponse(string data)
    {
        Response.Clear();
        Response.Write(data);
        Response.End();
    }

    public void BindCompany()
    {
        string sql = "SELECT VW_sql_tblSMDPRCompany.Company, VW_sql_tblSMDPRCompany.ReportCo " +
            " ,(VW_sql_tblSMDPRCompany.Company + '-' + VW_sql_tblSMDPRCompany.[Company Name]) as CompName" +
        " FROM VW_sql_tblSMDPRCompany WHERE (((VW_sql_tblSMDPRCompany.InAccts)=1)) and (((VW_sql_tblSMDPRCompany.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            ddlCompany.DataSource = DtCompany;
            ddlCompany.DataTextField = "CompName";
            ddlCompany.DataValueField = "Company";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("<Select>", "0"));
            ddlCompany.SelectedIndex = 0;
            BindVessel();
            BindddlYear();
        }
    }
    public void BindVessel()
    {
        string sql = "SELECT VW_sql_tblSMDPRVessels.ShipID, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels " +
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='" + ddlCompany.SelectedValue + "')) and VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlShip.DataSource = DtVessel;
            ddlShip.DataTextField = "ShipNameCode";
            ddlShip.DataValueField = "ShipID";
            ddlShip.DataBind();
            ddlShip.Items.Insert(0, new ListItem("<Select>", ""));
        }

    }

    public void BindddlYear()
    {
        string sql = "Select  distinct CurFinYear from AccountCompanyBudgetMonthyear with(nolock) where Cocode = '" + ddlCompany.SelectedValue + "'";
        DataTable dtCurrentyear = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCurrentyear != null)
        {
            ddlYear.DataSource = dtCurrentyear;
            ddlYear.DataTextField = "CurFinYear";
            ddlYear.DataValueField = "CurFinYear";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("<Select>", ""));
        }
    }

    public void BindddlMonth()
    {
        string sql = "Select distinct Month,LEFT(DateName( MONTH , DateAdd( month , Month , -1 )),3) As MonthName,RowNumber from AccountCompanyBudgetMonthyear with(nolock) where Cocode = '" + ddlCompany.SelectedValue + "' and CurFinYear = '" +ddlYear.SelectedValue+ "' order by RowNumber ASC ";
        DataTable dtCurrentMonth = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCurrentMonth != null)
        {
            ddlMonth.DataSource = dtCurrentMonth;
            ddlMonth.DataTextField = "MonthName";
            ddlMonth.DataValueField = "Month";
            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, new ListItem("<ALL>", "0"));
            ddlMonth.SelectedIndex = 0;
        }
    }

    // Event -----------------------------------------------------------------------
    protected void btnEdit_Task_Click(object sender,EventArgs e)
    {
        TaskID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowTaskDetails();
        dvAddTrackingTask.Visible = true;
    }
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
        BindddlYear();
        string sql = "Select  IsIndianFinacialYear from AccountCompany with(nolock) where Company = '" + ddlCompany.SelectedValue + "'";
        DataTable dtCurrentyear = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCurrentyear != null)
        {
            IndianFinYear = Convert.ToBoolean(dtCurrentyear.Rows[0]["IsIndianFinacialYear"]);
            hdnIsIndianFinYr.Value = dtCurrentyear.Rows[0]["IsIndianFinacialYear"].ToString();
        }
    }
    public void ddlShip_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindMajorCategory();
    }
    public void ddlBudgetType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindMajorCategory();
    }
    public void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BudgetYear = ddlYear.SelectedValue;
        lblBudgetYear.Text = BudgetYear.ToString();
        BindddlMonth();
       //BindMajorCategory();
       
    }

    
    
    public void btnShow_OnClick(object sender, EventArgs e)
    {
        BindMajorCategory();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }
    // Function ---------------------------------------------------------------------
    protected void ShowTaskDetails()
    {
        DataTable dt= Common.Execute_Procedures_Select_ByQuery("select * from tbl_budgettracking where taskid=" + TaskID);
        if(dt.Rows.Count>0)
        {
            txtTtDescription.Text = dt.Rows[0]["TaskDescription"].ToString();
            
            if(dt.Rows[0]["Budgeted"].ToString().ToLower() == "true")
            {
                ddlTaskType.SelectedValue = "1";
            }
            else
            {
                ddlTaskType.SelectedValue = "2";
            }
            ddlTaskType_OnSelectedIndexChanged(new object(), new EventArgs());
	    txtTtAmount.Text = dt.Rows[0]["Amount"].ToString();

            //chkTtJan.Checked = dt.Rows[0]["jan"].ToString().ToLower() == "true";
            //chkTtFeb.Checked = dt.Rows[0]["feb"].ToString().ToLower() == "true";
            //chkTtMar.Checked = dt.Rows[0]["mar"].ToString().ToLower() == "true";
            //chkTtApr.Checked = dt.Rows[0]["apr"].ToString().ToLower() == "true";
            //chkTtMay.Checked = dt.Rows[0]["may"].ToString().ToLower() == "true";
            //chkTtJun.Checked = dt.Rows[0]["jun"].ToString().ToLower() == "true";
            //chkTtJul.Checked = dt.Rows[0]["jul"].ToString().ToLower() == "true";
            //chkTtAug.Checked = dt.Rows[0]["aug"].ToString().ToLower() == "true";
            //chkTtSep.Checked = dt.Rows[0]["sep"].ToString().ToLower() == "true";
            //chkTtOct.Checked = dt.Rows[0]["oct"].ToString().ToLower() == "true";
            //chkTtNov.Checked = dt.Rows[0]["nov"].ToString().ToLower() == "true";
            //chkTtDec.Checked = dt.Rows[0]["dec"].ToString().ToLower() == "true";
        }
    }
    public DataSet GetTrackingData(string Company, string BudgetYear, int BudgetMonth, string VesselCode)
    {
       

        DataSet DS = new DataSet();
        //if (Cache["TrackingDatas"] == null)        
        if (true)
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec getVarianceRepport_vsl_BudgetTracking '" + Company + "'," + BudgetMonth + ",'" + BudgetYear + "','" + VesselCode + "'", con); //???????
            cmd.CommandTimeout = 300;
            cmd.CommandType = CommandType.Text;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
            adp.SelectCommand = cmd;
            adp.Fill(DS);
            Cache["TrackingDatas"] = DS;
        }
        else
        {
            DS = (DataSet)(Cache["TrackingDatas"]);            
        }
        return DS;
    }
    public void BindMajorCategory()
    {
        if (ddlCompany.SelectedIndex <= 0 || ddlShip.SelectedIndex <= 0)
        {
            rptData.DataSource = null;
            rptData.DataBind();
            return;
        }
        //System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Common.ConnectionString);
        //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec getVarianceRepport_vsl_BudgetTracking '" + ddlCompany.SelectedValue + "',"+ DateTime.Today.Month +"," + lblBudgetYear.Text + ",'" + ddlShip.SelectedValue + "'", con); //???????
        //cmd.CommandTimeout = 300;
        //cmd.CommandType = CommandType.Text;

        //System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
        //adp.SelectCommand = cmd;
        //DsValue = new DataSet();
        //adp.Fill(DsValue);
        int Month;
        Month = DateTime.Now.Month;
        int curYear = 0;
        int nextYear = 0;
        string currFyYr = "";
        if (IndianFinYear)
        {
            if (Month >= 1 && Month <= 3)
            {
                curYear = DateTime.Now.Year - 1;
                nextYear = DateTime.Now.Year;
            }
            else
            {
                curYear = DateTime.Now.Year;
                nextYear = DateTime.Now.Year + 1;
            }
             currFyYr = curYear.ToString() + "-" + nextYear.ToString().Substring((nextYear.ToString()).Length - 2).ToString();
        }
        else
        {
            curYear = DateTime.Now.Year;
            currFyYr = curYear.ToString();
        }
       
        
        if (IndianFinYear && ddlYear.SelectedValue != currFyYr && ddlMonth.SelectedValue == "0")
        {
            Month = 3;
        }
        else if (ddlYear.SelectedValue != currFyYr && ddlMonth.SelectedValue != "0")
        {
            Month = Convert.ToInt32(ddlMonth.SelectedValue);
        }
        else if (ddlYear.SelectedValue == currFyYr && ddlMonth.SelectedValue != "0")
        {
            Month = Convert.ToInt32(ddlMonth.SelectedValue);
        }
        else if (ddlYear.SelectedValue == currFyYr && ddlMonth.SelectedValue == "0")
        {
            Month = DateTime.Now.Month;
        }


        GetBudgetDays(ddlCompany.SelectedValue, (lblBudgetYear.Text), ddlShip.SelectedValue);

        DataSet DsValue = GetTrackingData(ddlCompany.SelectedValue,(lblBudgetYear.Text), Month, ddlShip.SelectedValue);

        if (DsValue != null)
        {
            rptData.DataSource = DsValue.Tables[0];
            rptData.DataBind();            
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
        }
    }
    //public DataTable getItems1(object MAJCATID)
    //{
    //    if (true)
    //    {
    //        DataTable Dt = DsValue.Tables[1];
    //        DataView DV = Dt.DefaultView;
    //        DV.RowFilter = "MAJCATID=" + MAJCATID.ToString();

    //        //-------------------
    //        Dt = DV.ToTable();
    //        if (!Dt.Columns.Contains("CommentID"))
    //        {
    //            Dt.Columns.Add("CommentID", typeof(int));
    //            Dt.Columns.Add("Comment", typeof(string));
    //        }
    //        string sql = "";

    //        foreach (DataRow Dr in Dt.Rows)
    //        {
    //            sql = "select CommentID,Comment  from [dbo].tblBudgetLevelComments " +
    //                "where CommCo='" + ddlCompany.SelectedValue + "' and CommYear=" + lblBudgetYear.Text + "and CommPer=" + DateTime.Today.Month + " and CommMajID=" + MAJCATID.ToString() + " and CommShipID='" + ddlShip.SelectedValue + "' and CommMidID= " + Dr["MidCatID"].ToString() + "";
    //            DataTable DtCom = Common.Execute_Procedures_Select_ByQuery(sql);
    //            if (DtCom != null)
    //            {
    //                if (DtCom.Rows.Count > 0)
    //                {
    //                    Dr["CommentID"] = DtCom.Rows[0][0].ToString();
    //                    Dr["Comment"] = DtCom.Rows[0][1].ToString();
    //                }
    //            }

    //        }
    //        //--------------------
    //        return Dt;
    //    }
    //    else
    //    {
    //        DataTable Dt = DsValue.Tables[1];
    //        Dt.Clear();
    //        //Dt.Columns.Contains("ColumnName");
    //        if (!Dt.Columns.Contains("CommentID"))
    //        {
    //            Dt.Columns.Add("CommentID", typeof(int));
    //            Dt.Columns.Add("Comment", typeof(string));
    //        }
    //        return Dt;
    //    }
    //}
    protected void GetBudgetDays(string CoCode,string BudgetYear , string VesselCode)
    {
        lblDays.Text = "0";
        string sql = "SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear = '" + BudgetYear.ToString() + "' AND VDAYS.CoCode = '" + CoCode + "' AND VDAYS.ShipId = '" + VesselCode + "' ORDER BY VDAYS.YEARDAYS DESC ";
        DataTable dtdays = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtdays.Rows.Count > 0)
        {
            lblDays.Text = dtdays.Rows[0]["YEARDAYS"].ToString();
        }
    }
    public string SetColorForVPer(object val)
    {
        if (Convert.ToDecimal(val) < 0)
            return "error_msg";
        else if (Convert.ToDecimal(val) > 0)
            return "msg";
        else
            return "";
    }
    public string SetColorForYearPer(object val)
    {
        if (Convert.ToDecimal(val) >= 100)
            return "error_msg";
        else
            return "";
    }
   
    public string getMidCategory(string Company,string vessel, string Year,int MAJCATID, int Month, bool IsIndianFinYear)
    {
        //if (Month == 0)
        //{
        //    Month = DateTime.Now.Month;
        //}
        int curYear = 0;
        int nextYear = 0;
        string currFyYr = "";
        if (IsIndianFinYear)
        {
            if (Month >= 1 && Month <= 3)
            {
                curYear = DateTime.Now.Year - 1;
                nextYear = DateTime.Now.Year;
            }
            else
            {
                curYear = DateTime.Now.Year;
                nextYear = DateTime.Now.Year + 1;
            }
            currFyYr = curYear.ToString() + "-" + nextYear.ToString().Substring((nextYear.ToString()).Length - 2).ToString();
        }
        else
        {
            curYear = DateTime.Now.Year;
            currFyYr = curYear.ToString();
        }


        if (IsIndianFinYear && Year != currFyYr && Month == 0)
        {
            Month = 3;
        }
        else if (IsIndianFinYear && Year != currFyYr && Month != 0)
        {
            Month = Convert.ToInt32(Month);
        }
        else if (Year == currFyYr && Month != 0)
        {
            Month = Convert.ToInt32(Month);
        }
        else if (Year == currFyYr && Month == 0)
        {
            Month = Convert.ToInt32(DateTime.Now.Month);
        }
        GetBudgetDays(ddlCompany.SelectedValue, (lblBudgetYear.Text), ddlShip.SelectedValue);
        DataSet DsValue = GetTrackingData(Company, Year, Month,vessel);

        DataTable Dt = DsValue.Tables[1];
        DataView DV = Dt.DefaultView;
        DV.RowFilter = "MAJCATID=" + MAJCATID.ToString();
        //-------------------
        rptMidCats.DataSource = DV.ToTable();
        rptMidCats.DataBind();
        StringBuilder sb = new StringBuilder();
        StringWriter TW = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(TW);
        rptMidCats.RenderControl(hw);
        ////----------------------
        //rptMidCats.DataSource = null;
        //rptMidCats.DataBind();

        return sb.ToString();
    }
    public string getMinCategory(string Company, string vessel, string Year, int MAJCATID, int MIDCATID, int Month, bool IsIndianFinYear)
    {

        //System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Common.ConnectionString);
        //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec getVarianceRepport_vsl_BudgetTracking '" + Company + "'," + DateTime.Today.Month + "," + lblBudgetYear.Text + ",'" + vessel + "'", con); //???????
        //cmd.CommandTimeout = 300;
        //cmd.CommandType = CommandType.Text;

        //System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
        //adp.SelectCommand = cmd;
        //DataSet DsValue = new DataSet();
        //adp.Fill(DsValue);
        //if (Month == 0)
        //{
        //    Month = DateTime.Now.Month;
            
        //}
        int curYear = 0;
        int nextYear = 0;
        string currFyYr = "";
        if (IsIndianFinYear)
        {
            if (Month >= 1 && Month <= 3)
            {
                curYear = DateTime.Now.Year - 1;
                nextYear = DateTime.Now.Year;
            }
            else
            {
                curYear = DateTime.Now.Year;
                nextYear = DateTime.Now.Year + 1;
            }
            currFyYr = curYear.ToString() + "-" + nextYear.ToString().Substring((nextYear.ToString()).Length - 2).ToString();
        }
        else
        {
            curYear = DateTime.Now.Year;
            currFyYr = curYear.ToString();
        }


        if (IsIndianFinYear && Year != currFyYr && Month == 0)
        {
            Month = 3;
        }
        else if (Year != currFyYr && Month != 0)
        {
            Month = Convert.ToInt32(Month);
        }
        else if (Year == currFyYr && Month != 0)
        {
            Month = Convert.ToInt32(Month);
        }
        else if (Year == currFyYr && Month == 0)
        {
            Month = DateTime.Now.Month;
        }
        GetBudgetDays(ddlCompany.SelectedValue, (lblBudgetYear.Text), ddlShip.SelectedValue);
        DataSet DsValue = GetTrackingData(Company, Year, Month, vessel);

        DataTable Dt = DsValue.Tables[2];
        DataView DV = Dt.DefaultView;
        DV.RowFilter = "MAJCATID=" + MAJCATID.ToString() + " and MIDCATID=" + MIDCATID.ToString();
        //-------------------
        rptMinCats.DataSource = DV.ToTable();
        rptMinCats.DataBind();
        StringBuilder sb = new StringBuilder();
        StringWriter TW = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(TW);
        rptMinCats.RenderControl(hw);
        ////----------------------
        //rptMidCats.DataSource = null;
        //rptMidCats.DataBind();

        return sb.ToString();
    }
    public string getAccounts(string Company, string vessel, string Year, int MAJCATID, int MIDCATID, int MINCATID, int Month, bool IsIndianFinYear)
    {

        //System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Common.ConnectionString);
        //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec getVarianceRepport_vsl_BudgetTracking '" + Company + "'," + DateTime.Today.Month + "," + lblBudgetYear.Text + ",'" + vessel + "'", con); //???????
        //cmd.CommandTimeout = 300;
        //cmd.CommandType = CommandType.Text;

        //System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
        //adp.SelectCommand = cmd;
        //DataSet DsValue = new DataSet();
        //adp.Fill(DsValue);
        //if (Month == 0)
        //{
        //    Month = DateTime.Now.Month;
        //}

        int curYear = 0;
        int nextYear = 0;
        string currFyYr = "";
        if (IsIndianFinYear)
        {
            if (Month >= 1 && Month <= 3)
            {
                curYear = DateTime.Now.Year - 1;
                nextYear = DateTime.Now.Year;
            }
            else
            {
                curYear = DateTime.Now.Year;
                nextYear = DateTime.Now.Year + 1;
            }
            currFyYr = curYear.ToString() + "-" + nextYear.ToString().Substring((nextYear.ToString()).Length - 2).ToString();
        }
        else
        {
            curYear = DateTime.Now.Year;
            currFyYr = curYear.ToString();
        }


        if (IsIndianFinYear && Year != currFyYr && Month == 0)
        {
            Month = 3;
        }
        else if (Year != currFyYr && Month != 0)
        {
            Month = Convert.ToInt32(Month);
        }
        else if (Year == currFyYr && Month != 0)
        {
            Month = Convert.ToInt32(Month);
        }
        else if (Year == currFyYr && Month == 0)
        {
            Month = DateTime.Now.Month;
        }
        GetBudgetDays(ddlCompany.SelectedValue, (lblBudgetYear.Text), ddlShip.SelectedValue);
        DataSet DsValue = GetTrackingData(Company, Year, Month, vessel);

        DataTable Dt = DsValue.Tables[3];
        DataView DV = Dt.DefaultView;
        DV.RowFilter = "MAJCATID=" + MAJCATID.ToString() + " and MIDCATID=" + MIDCATID.ToString() + " and MINCATID=" + MINCATID.ToString();
        //-------------------
        rptAccounts.DataSource = DV.ToTable();
        rptAccounts.DataBind();
        StringBuilder sb = new StringBuilder();
        StringWriter TW = new StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(TW);
        rptAccounts.RenderControl(hw);
        ////----------------------
        //rptMidCats.DataSource = null;
        //rptMidCats.DataBind();
        
        return sb.ToString();
    }

    // Task Details ----------------------------------------
    //-----------------------------------------------------------------
   
    protected void btnDelete_Task_Click(object sender,EventArgs e)
    {
        int _TaskID = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dtchk = Common.Execute_Procedures_Select_ByQuery("select * from dbo.tbl_budgettracking_orders where itemid=" + _TaskID);
        if (!CheckTaskDeleteAuthority(_TaskID))
        {
            lblMsgTaskListPopup.Text = "You are not authorize to delete this task.";
            return;
        }
        if(dtchk.Rows.Count>0)
        {
            lblMsgTaskListPopup.Text = "Can not delete this task. It has some purchase orders linked.";
            return;
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("delete from dbo.tbl_budgettracking where taskid=" + _TaskID);
            ShowTaskList();
            lblMsgTaskListPopup.Text = "Record deleted successfully.";
        }
    }
    public bool CheckTaskDeleteAuthority(int TaskID)
    {
        bool ret = false;
        string sql = " select 1 from  tbl_BudgetTracking where TaskID="+ TaskID + " and ModifiedBy='"+Session["UserName"] .ToString()+"' ";
        DataTable dtchk = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Session["loginid"].ToString() == "1" )
            ret = true;
        else if(dtchk.Rows.Count > 0)
            ret = true;
        return ret;
    }
    public bool CheckTaskDeleteAuthorityForDeleteBtn(int TaskID)
    {
        bool ret = false;
        ret = CheckTaskDeleteAuthority(TaskID);

        DataTable dtchk = Common.Execute_Procedures_Select_ByQuery("select * from dbo.tbl_budgettracking_orders where itemid=" + TaskID);
        if (dtchk.Rows.Count > 0)
        {
            ret = false;
        }

        return ret;
    }
    //--Add Task---------------------------------------------------------------    
    protected void btnOpenAddTaskPopup_OnClick(object sender, EventArgs e)
    {
	TaskID=0;
        dvAddTrackingTask.Visible = true;
    }
    protected void btnCloseAddTrackingTaskPopup_OnClick(object sender, EventArgs e)
    {
        dvAddTrackingTask.Visible = false;
        ClearTrackingControl();
        TaskID = 0;

        SelCompanyCode = "";
        SelVesselCode = "";
    }
    protected void btnSaveTrackingTask_OnClick(object sender, EventArgs e)
    {
        if (txtTtDescription.Text.Trim() == "")
        {
            lblMsgTrackingTask.Text = "Please enter description.";
            txtTtDescription.Focus();
            return;
        }
        if (txtTtAmount.Text.Trim() == "")
        {
            lblMsgTrackingTask.Text = "Please enter amount.";
            txtTtAmount.Focus();
            return;
        }

        if (txtTtDescription.Text.Trim().Length > 250)
        {
            lblMsgTrackingTask.Text = "Description should be within 250 character.";
            txtTtDescription.Focus();
            return;
        }
        if (ddlTaskType.SelectedIndex==0)
        {
            lblMsgTrackingTask.Text = " Please select task type.";
            ddlTaskType.Focus();
            return;
        }


        Common.Set_Procedures("sp_IU_tbl_BudgetTracking");
        Common.Set_ParameterLength(22);
        Common.Set_Parameters(
            new MyParameter("@TaskID", TaskID),
            new MyParameter("@Company", ddlCompany.SelectedValue),
            new MyParameter("@VesselCode", ddlShip.SelectedValue),
            new MyParameter("@BudgetYear", Convert.ToInt32(DateTime.Today.Year)),
            new MyParameter("@AccountID", Common.CastAsInt32(hfSelAccountID.Value)),
            new MyParameter("@TaskDescription", txtTtDescription.Text.Trim()),
            new MyParameter("@Amount", txtTtAmount.Text.Trim()),
             //new MyParameter("@Jan", (chkTtJan.Checked)),
             //new MyParameter("@Feb", (chkTtFeb.Checked)),
             //new MyParameter("@Mar", (chkTtMar.Checked)),
             //new MyParameter("@Apr", (chkTtApr.Checked)),
             //new MyParameter("@May", (chkTtMay.Checked)),
             //new MyParameter("@Jun", (chkTtJun.Checked)),
             //new MyParameter("@Jul", (chkTtJul.Checked)),
             //new MyParameter("@Aug", (chkTtAug.Checked)),
             //new MyParameter("@Sep", (chkTtSep.Checked)),
             //new MyParameter("@Oct", (chkTtOct.Checked)),
             //new MyParameter("@Nov", (chkTtNov.Checked)),
             //new MyParameter("@Dec", (chkTtDec.Checked)),
            new MyParameter("@Jan", DBNull.Value),
            new MyParameter("@Feb", DBNull.Value),
            new MyParameter("@Mar", DBNull.Value),
            new MyParameter("@Apr", DBNull.Value),
            new MyParameter("@May", DBNull.Value),
            new MyParameter("@Jun", DBNull.Value),
            new MyParameter("@Jul", DBNull.Value),
            new MyParameter("@Aug", DBNull.Value),
            new MyParameter("@Sep", DBNull.Value),
            new MyParameter("@Oct", DBNull.Value),
            new MyParameter("@Nov", DBNull.Value),
            new MyParameter("@Dec", DBNull.Value),
            new MyParameter("@budgeted", (ddlTaskType.SelectedValue=="1")),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString()),
            new MyParameter("@CurFinYear", BudgetYear)
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res == true)
        {
            int c = Common.CastAsInt32(Ds.Tables[0].Rows[0][0]);
            if (c > 0)
            {
                lblMsgTrackingTask.Text = "Record saved successfully.";
                ClearTrackingControl();
                BindMajorCategory();
                ShowTaskList();
            }
            else
            {
                lblMsgTrackingTask.Text = "Please check this task causing total budget amount for this account more than annual budget. It is not allowed.";
            }
        }
        else
        {
            lblMsgTrackingTask.Text = "Record could not saved." + Common.ErrMsg;
        }

    }
    public void ClearTrackingControl()
    {
        TaskID = 0;
        txtTtAmount.Text = "";
        txtTtDescription.Text = "";
        //lblTaskModifiedByOn.Text = "";
        //chkTtJan.Checked = false;
        //chkTtFeb.Checked = false;
        //chkTtMar.Checked = false;
        //chkTtApr.Checked = false;
        //chkTtMay.Checked = false;
        //chkTtJun.Checked = false;
        //chkTtJul.Checked = false;
        //chkTtAug.Checked = false;
        //chkTtSep.Checked = false;
        //chkTtOct.Checked = false;
        //chkTtNov.Checked = false;
        //chkTtDec.Checked = false;
        ddlTaskType.SelectedIndex= 0;
    }

    public string GetBudgetVariance(object b, object c)
    {
        decimal bb = Common.CastAsDecimal(b);
        decimal cc = Common.CastAsDecimal(c);
        if (cc == 0)
            return "0.00";
        else
            return Common.CastAsDecimal(((bb - cc) * 100) / bb).ToString("0.00");

    }

    //--Closure---------------------------------------------------------------
    protected void btnOpenClosuerPopup_OnClick(object sender, EventArgs e)
    {
        dvClosure.Visible = true;
        txtClosureRemarks.Text = "";
    }
    protected void btnCloseClosurePopup_OnClick(object sender, EventArgs e)
    {
        dvClosure.Visible = false;
    }
    protected void btnSaveClosure_OnClick(object sender, EventArgs e)
    {
        if (txtClosureRemarks.Text.Trim() == "")
        {
            lblMsgClosure.Text = "Please enter closure remark";
            return;
        }
        Common.Set_Procedures("sp_TaskClosure");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@TaskID", TaskID),
            new MyParameter("@ClosedBy", Session["UserName"].ToString()),
            new MyParameter("@ClosureRemarks", txtClosureRemarks.Text.Trim())
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res == true)
        {
            lblMsgClosure.Text = "Record saved successfully.";
            BindMajorCategory();
        }
        else
            lblMsgClosure.Text = "Record could not saved." + Common.ErrMsg;
    }

    //--Open Task List---------------------------------------------------------------
    protected void btnTempOpenTaskListPopup_OnClick(object sender, EventArgs e)
    {
        divTaskList.Visible = true;
        ShowTaskList();
       
    }
    protected void btnClosePopupTaskList_OnClick(object sender, EventArgs e)
    {
        divTaskList.Visible = false;
    }


    //protected void lblOrdersCount_Click(object sender, EventArgs e)
    //{
    //    string path= "BudgetTrackingPOList_Unlink.aspx?company=" + ddlCompany.SelectedValue + "&vessel=" + ddlShip.SelectedValue + "&year=" + lblBudgetYear.Text + "&AccountId=" + hfSelAccountID.Value + "&majcatid=" + hfSelMajCatID.Value+ "&midcatid=" + hfSelMidCatID.Value + "&mincatid=" + hfSelMinCatID.Value;
    //    ScriptManager.RegisterStartupScript(this,this.GetType(),"op","window.open('" + path + "','');",true);
    //}
    protected void lblOrdersCount1_Click(object sender, EventArgs e)
    {
        string path = "BudgetTrackingPOList_Unlink.aspx?company=" + ddlCompany.SelectedValue + "&vessel=" + ddlShip.SelectedValue + "&year=" + lblBudgetYear.Text + "&AccountId=" + hfSelAccountID.Value + "&majcatid=" + hfSelMajCatID.Value + "&midcatid=" + hfSelMidCatID.Value + "&mincatid=" + hfSelMinCatID.Value + "&mode=1";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "op", "window.open('" + path + "','');", true);
    }
    public void ShowTaskList()
    {
        int majcatid = Common.CastAsInt32(hfSelMajCatID.Value);
        int midcatid = Common.CastAsInt32(hfSelMidCatID.Value);
        int mincatid = Common.CastAsInt32(hfSelMinCatID.Value);
        int AccountID = Common.CastAsInt32(hfSelAccountID.Value);


        lblCompany.Text = ddlCompany.SelectedItem.Text;
        lblVessel.Text = ddlShip.SelectedItem.Text;
        lblYear.Text = lblBudgetYear.Text;

        //Show header information
        int Month;
        Month = DateTime.Now.Month;
        int curYear = 0;
        int nextYear = 0;
        string currFyYr = "";
        if (IndianFinYear)
        {
            if (Month >= 1 && Month <= 3)
            {
                curYear = DateTime.Now.Year - 1;
                nextYear = DateTime.Now.Year;
            }
            else
            {
                curYear = DateTime.Now.Year;
                nextYear = DateTime.Now.Year + 1;
            }
            currFyYr = curYear.ToString() + "-" + nextYear.ToString().Substring((nextYear.ToString()).Length - 2).ToString();
        }
        else
        {
            curYear = DateTime.Now.Year;
            currFyYr = curYear.ToString();
        }


        if (IndianFinYear && ddlYear.SelectedValue != currFyYr && ddlMonth.SelectedValue == "0")
        {
            Month = 3;
        }
        else if (ddlYear.SelectedValue != currFyYr && ddlMonth.SelectedValue != "0")
        {
            Month = Convert.ToInt32(ddlMonth.SelectedValue);
        }
        else if (ddlYear.SelectedValue == currFyYr && ddlMonth.SelectedValue != "0")
        {
            Month = Convert.ToInt32(ddlMonth.SelectedValue);
        }
        GetBudgetDays(ddlCompany.SelectedValue, (lblBudgetYear.Text), ddlShip.SelectedValue);
        DataSet DsValue = GetTrackingData(ddlCompany.SelectedValue,  lblBudgetYear.Text, Month, ddlShip.SelectedValue);
        DataTable Dt = DsValue.Tables[3];
        DataView DV = Dt.DefaultView;
        DV.RowFilter = "MAJCATID=" + majcatid.ToString() + " and MIDCATID=" + midcatid.ToString() + " and MINCATID=" + mincatid.ToString() + " and AccountID=" + AccountID.ToString(); ;
        if (DV.ToTable().Rows.Count > 0)
        {
            DataRow dr = DV.ToTable().Rows[0];

            lblAccountNoNameTaskList.Text = dr["Accountnumber"].ToString() + " - " + dr["AccountName"].ToString();
            lblYTDActule.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTDAct"]));
            lblYTDCommitted.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTD_Comm"]));
            lblYTDConsumed.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTDCons"]));
            lblYTDBudget.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTDBgt"]));
            lblYTDVariance.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctYTDVar"]));
            lblYTDVariancePer.Text = String.Format("{0:0.00}", Common.CastAsDecimal(dr["Col1"])) + " % ";
            lblYTDAnnualBudget.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(Common.CastAsInt32(dr["AcctFYBudget"]));
            lblYTDAnnualUtilization.Text = String.Format("{0:0.00}", Common.CastAsDecimal(dr["Col2"])) + " % ";
        }
        int per1 = 0;
        int per2 = 0;
        decimal amt1 = Common.CastAsDecimal(lblYTDBudget.Text);
        decimal amt2 = Common.CastAsDecimal(lblYTDConsumed.Text);
        
        decimal max = (amt1>amt2)?amt1:amt2;
        try
        {
            per1 =Common.CastAsInt32((amt1 * 100) / max);
        }
        catch { }
        try
        {
            per2 = Common.CastAsInt32((amt2 * 100) / max);
        }
        catch { }
        dvbud.Style.Add("width", per1.ToString() + "%");
        dvbudcons.Style.Add("width", per2.ToString() + "%");
        decimal varper= Common.CastAsDecimal(lblYTDVariancePer.Text);
        if(varper>0)
        {
            lblYTDVariancePer.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblYTDVariancePer.ForeColor = System.Drawing.Color.Green;
        }

        decimal annutil = Common.CastAsDecimal(lblYTDAnnualUtilization.Text);
        if (annutil > 0)
        {
            lblYTDAnnualUtilization.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblYTDAnnualUtilization.ForeColor = System.Drawing.Color.Green;
        }

        //-------------- unlinked orders
        //string sql1 = " select p.invoiceid, m.BidID,BidPoNum,s.SupplierName,m.BidSMDLevel1ApprovalDate as PoDate,  " +
        //            "   COALESCE( " +
        //            "  (SELECT SUM(TransUSD)FROM DBO.tblApEntries WHERE BIDID = m.bidid AND ScreenName = 'INV'), " +
        //            "  (SELECT TransUSD FROM DBO.tblApEntries WHERE BIDID = m.bidid AND ScreenName = 'APP') " +
        //            "  ) AS poamount, accountnumber, " +
        //            "  bidstatusname, ApproveComments, REfNo, " +
        //            "  (SELECT TOP 1 InAccts FROM DBO.tblApEntries WHERE BIDID = m.bidid ORDER BY INTRAV DESC) AS CA " +
        //            "  from " +
        //            "  dbo.tblsmdpomasterbid m " +
        //            "  inner " +
        //            "  join dbo.tblSMDBIDStatusCodes stat on m.bidstatusid = stat.bidstatusid " +
        //            "  inner " +
        //            "  join dbo.tblsmdpomaster po on po.poid = m.poid " +
        //            "  inner " +
        //            "  join dbo.sql_tblSMDPRAccounts a on a.accountid = po.AccountID " +
        //            "  inner " +
        //            "  join dbo.tblsmdSuppliers s on s.SupplierID = m.SupplierID " +
        //            "  left " +
        //            "  join POS_Invoice_Payment_PO p on p.bidid = m.bidid " +
        //            "  left " +
        //            "  join POS_Invoice i on p.InvoiceId = I.InvoiceId " +
        //            "  left " +
        //            "  join tbl_BudgetTracking_Orders bo on bo.BidID = m.bidid " +
        //            "  left " +
        //            "  join tbl_BudgetTracking t on t.TaskID = bo.ItemID " +
        //            "  where " +
        //            "  SHIPID = '" + ddlShip.SelectedValue + "' AND po.AccountID =" + AccountID + " AND YEAR(m.BidSMDLevel1ApprovalDate) = " + lblBudgetYear.Text + " and m.bidstatusid > 0  and t.taskid is null ";

        //string sql1 = "select count(BidID) from dbo.VW_PO_RECORDS where shipid='" + ddlShip.SelectedValue + "' and accountid=" + AccountID + " and YEAR(BidSMDLevel1ApprovalDate) = " + lblBudgetYear.Text + " and taskid is null";
        //DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql1);
        //lblOrdersCount.Text =  DT.Rows[0][0].ToString();
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //sql1 = "select count(entrynum) from dbo.VW_NONPO_RECORDS where cocode='" + ddlCompany.SelectedValue + "' and shipid='" + ddlShip.SelectedValue + "' and year=" + lblBudgetYear.Text + " And AccountId=" + AccountID + " And TaskId is null";
        //sql1 = "SELECT count(qSPAccountDetails.BidIDCalc) " +
        //       "FROM " +
        //       " dbo.v_GLJournal_Details as qSPAccountDetails " +
        //       "INNER JOIN dbo.v_techCY_Accounts ON qSPAccountDetails.AccountNumber = v_techCY_Accounts.AccountNumber " +
        //       "INNER JOIN dbo.Vessel as Lk_tblSMDPRVessels ON qSPAccountDetails.ShipID = Lk_tblSMDPRVessels.VesselCode AND  qSPAccountDetails.cocode = Lk_tblSMDPRVessels.AccontCompany " +
        //       "LEFT JOIN DBO.tbl_Budet_Tracking_Orders_NonPO NONPO ON (-qSPAccountDetails.BidIDCalc) = NONPO.entrynum " +
        //"LEFT JOIN DBO.tbl_BudgetTracking BT ON qSPAccountDetails.COCODE = BT.COMPANY AND qSPAccountDetails.ShipID = BT.VESSELCODE AND qSPAccountDetails.YEAR = BT.BUDGETYEAR AND qSPAccountDetails.ACCOUNTID = BT.ACCOUNTID " +
        //       "where qSPAccountDetails.BIDID IS NULL AND qSPAccountDetails.cocode = '" + ddlCompany.SelectedValue + "' and qSPAccountDetails.shipid ='" + ddlShip.SelectedValue + "' and qSPAccountDetails.year = " + lblBudgetYear.Text + " And qSPAccountDetails.AccountId = " + AccountID + " " +
        //       "And NONPO.TaskId is null ";

        //DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql1);
        //lblNONPoOrders.Text = dt1.Rows[0][0].ToString();
        //        lblNONPoOrders.Text = "0";

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //string sql = "select company,vesselcode,budgetyear,AccountId,TaskID,budgeted,TaskDescription,Amount,(select SUM(poamount)as TotConsume from VW_TaskLinkedOrders where taskid=tbl_BudgetTracking.taskid) as TotConsume,ModifiedBy,ModifiedOn from tbl_BudgetTracking where Company='" + ddlCompany.SelectedValue + "' and VesselCode='" + ddlShip.SelectedValue + "' and BudgetYear=" + lblBudgetYear.Text + " and AccountID=" + AccountID + "";
        //DataTable DT1 = Common.Execute_Procedures_Select_ByQuery(sql);
        //rptTrackingTaskList.DataSource = DT1;
        //rptTrackingTaskList.DataBind();

        //lbltaskbudgettotal.Text = ProjectCommon.FormatCurrencyWithoutSignNoDecimal(DT1.Compute("SUM(Amount)",""));
        //lblconsumedtotal.Text = ProjectCommon.FormatCurrencyWithoutSign(DT1.Compute("SUM(TotConsume)", ""));
        //lblconsumedtotal_b.Text = ProjectCommon.FormatCurrencyWithoutSign(DT1.Compute("SUM(TotConsume)", "budgeted=1"));
        //lblconsumedtotal_u.Text = ProjectCommon.FormatCurrencyWithoutSign(DT1.Compute("SUM(TotConsume)", "budgeted=0"));

        

        string strPodtls = "EXEC GetAccountwisePODetails '"+ ddlCompany.SelectedValue + "','"+ ddlShip.SelectedValue + "',"+ AccountID + ",'"+ lblBudgetYear.Text + "',"+Month+"";

        DataTable dtPoDetails = Common.Execute_Procedures_Select_ByQuery(strPodtls);
        if (dtPoDetails.Rows.Count > 0)
        {
            rptPoDetails.DataSource = dtPoDetails;
            rptPoDetails.DataBind();
            
            lbltotalPoCommitedAmt.Text = Math.Round(Convert.ToDecimal(dtPoDetails.Compute("Sum(Amount)", "").ToString()),2).ToString();
        }
        else
        {
            rptPoDetails.DataSource = null;
            rptPoDetails.DataBind();
          
            lbltotalPoCommitedAmt.Text = "";
        }

        string strPodtlsActual = "EXEC GetAccountwiseActualPODetails '" + ddlCompany.SelectedValue + "','" + ddlShip.SelectedValue + "'," + AccountID + ",'" + lblBudgetYear.Text + "',"+Month+"";

        DataTable dtActualPoDetails = Common.Execute_Procedures_Select_ByQuery(strPodtlsActual);
        if (dtActualPoDetails.Rows.Count > 0)
        {
            rptActualPoDetails.DataSource = dtActualPoDetails;
            rptActualPoDetails.DataBind();

            lblTotalActualPoDetailsAmt.Text = Math.Round(Convert.ToDecimal(dtActualPoDetails.Compute("Sum(Amount)", "").ToString()), 2).ToString();
        }
        else
        {
            rptActualPoDetails.DataSource = null;
            rptActualPoDetails.DataBind();
            lblTotalActualPoDetailsAmt.Text = "";
        }

        if (mincatid == 10)
        {
            btnCrewlist.Visible = true;
        }
        else
        {
            btnCrewlist.Visible = false;
        }

        rblBudgetValue.SelectedIndex = 0;
        divCommited.Visible = true;
        //lblTotalTaskAmount.Text = DT.Compute("Sum(Amount)", "").ToString();
        //if (Common.CastAsInt32(DT.Compute("Sum(Amount)", "")) > Common.CastAsInt32(lblBudgetAmt.Text))
        //    lblTotalTaskAmount.ForeColor = System.Drawing.Color.Red;

    }
    
        protected void btnClose_Click(object sender, EventArgs e)
    {
        divTaskList.Visible = false;
    }
    protected void ddlTaskType_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        if (ddlTaskType.SelectedValue == "2")
        {
            txtTtAmount.Text = "0";
            txtTtAmount.Enabled = false;
        }
        else
        {
            txtTtAmount.Enabled = true;
        }
    }
    public string GetVariancePerMonthTaskKList(object TaskID, object ConsumptionMonthCount, int M)
    {
        string Ret = "0.0";
        int iTaskID = Common.CastAsInt32(TaskID);
        int iConsumptionMonthCount = Common.CastAsInt32(ConsumptionMonthCount);
        decimal Amount = 0;
        decimal ConsAmount = 0;
        decimal AvgAmount = 0;
        string sql = " select sum(Amount)as Amount,sum( EstShippingUSD+CreditUSD+BidAmt) as ConAmont from vw_BudgetTracking where TaskID=" + iTaskID + " and Month(BidSMDLevel1ApprovalDate)="+M+" ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            Amount = Common.CastAsDecimal(DT.Rows[0]["Amount"]);
            ConsAmount = Common.CastAsDecimal(DT.Rows[0]["ConAmont"]);
            if (iConsumptionMonthCount > 0)
            {
                AvgAmount = Amount / iConsumptionMonthCount;
                Ret = ConsAmount.ToString("0.00");                
            }        
        }

            return Common.CastAsInt32(Ret).ToString();
    }
    public string GetCSSVariancePerMonthTaskKList(object TaskID, object ConsumptionMonthCount, int M,object Budgeted)
    {
        string Ret = "normalcell";
        bool bBudgeted = (bool)(Budgeted);
        int iTaskID = Common.CastAsInt32(TaskID);
        int iConsumptionMonthCount = Common.CastAsInt32(ConsumptionMonthCount);
        decimal Amount = 0;
        decimal ConsAmount = 0;
        decimal AvgAmount = 0;
        
        string sql = " select Amount,EstShippingUSD+CreditUSD+BidAmt as ConAmont from vw_BudgetTracking where TaskID=" + iTaskID + " and Month(BidSMDLevel1ApprovalDate)=" + M + " ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            Amount = Common.CastAsDecimal(DT.Rows[0]["Amount"]);
            ConsAmount = Common.CastAsDecimal(DT.Rows[0]["ConAmont"]);
            if (iConsumptionMonthCount > 0)
            {
                AvgAmount = Amount / iConsumptionMonthCount;
            }
            if (bBudgeted)
            {
                if (ConsAmount > AvgAmount)
                    Ret = "red";
                else
                    Ret = "green";
            }
            else
            {
                Ret = "green";
            }

        }
        else
        {
            if (bBudgeted)
                Ret = "green";            
        }

        return Ret;
    }


    protected void btnCrewlist_Click(object sender, EventArgs e)
    {
        divCrewList.Visible = true;
        ShowCrewList();
    }

    protected void imgbtnCrewlistClose_Click(object sender, ImageClickEventArgs e)
    {
        divCrewList.Visible = false;
    }

    public void ShowCrewList()
    {
        int majcatid = Common.CastAsInt32(hfSelMajCatID.Value);
        int midcatid = Common.CastAsInt32(hfSelMidCatID.Value);
        int mincatid = Common.CastAsInt32(hfSelMinCatID.Value);
        int AccountID = Common.CastAsInt32(hfSelAccountID.Value);
        lblCompanyforCrew.Text = ddlCompany.SelectedItem.Text;
        lblVesselforCrew.Text = ddlShip.SelectedItem.Text;
        lblYearforCrew.Text = lblBudgetYear.Text;
        lblAccountforCrew.Text = lblAccountNoNameTaskList.Text;
        string strcrewlist = "EXEC GetAccountWiseCrewList '" + ddlCompany.SelectedValue + "','" + ddlShip.SelectedValue + "'," + AccountID + ",'" + lblBudgetYear.Text + "'";

        DataTable dtCrewlist = Common.Execute_Procedures_Select_ByQuery(strcrewlist);
        if (dtCrewlist.Rows.Count > 0)
        {
            rptCrewList.DataSource = dtCrewlist;
            rptCrewList.DataBind();
            lblTotalCrewWages.Text = Math.Round(Convert.ToDecimal(dtCrewlist.Compute("SUM(Amount)", "").ToString()), 2).ToString();
        }
        else
        {
            rptCrewList.DataSource = null;
            rptCrewList.DataBind();
            lblTotalCrewWages.Text = "";
        }
    }

    protected void rblBudgetValue_SelectedIndexChanged(object sender, EventArgs e)
    {
        divCommited.Visible = false;
        divActual.Visible = false;

        if (rblBudgetValue.SelectedIndex == 0)
        {
            divCommited.Visible = true;
        }
        else if (rblBudgetValue.SelectedIndex == 1)
        {
            divActual.Visible = true;
        }
        
    }
}
