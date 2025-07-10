using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Diagnostics.Contracts;
using DocumentFormat.OpenXml.ExtendedProperties;

public partial class ReportingAndAnalysis : System.Web.UI.Page
{
    public AuthenticationManager authRecInv;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    DataSet DsValue;
    public bool IndianFinYear
    {
        get
        { return Convert.ToBoolean(ViewState["IndianFinYear"]); }
        set
        { ViewState["IndianFinYear"] = value; }
    }
    public void Manage_Menu()
    {
        AuthenticationManager auth = new AuthenticationManager(1085, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trCurrBudget.Visible = auth.IsView;
        //auth = new AuthenticationManager(28, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trAnalysis.Visible = auth.IsView;
        //auth = new AuthenticationManager(29, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trBudgetForecast.Visible = auth.IsView;
        //auth = new AuthenticationManager(30, int.Parse(Session["loginid"].ToString()), ObjectType.Module);
        //trPublish.Visible = auth.IsView;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
       ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1085, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                Response.Redirect("~/NoPermissionBudget.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermissionBudget.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------
        try
        {
            LblNoRow.Text = "";
            if (!Page.IsPostBack)
            {
                Manage_Menu();
               // BindYear();
                SetMonth();
                try
                {
                    ddlMonth.SelectedIndex = DateTime.Today.Month - 2;
                }
                catch { } 
                BindCompany();
                BindVessel(ddlCompany.SelectedValue);
                BindddlYear();
                //LoadSession();
                ddlReportLevel.SelectedIndex = 2;
                imgPrint.Visible = authRecInv.IsPrint;
            }
        }
        catch { }
    }

    // Event ----------------------------------------------------------------
    // DropDown
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "selECT cmp.[Company Name] as CompanyName,IsIndFyYr FROM vw_sql_tblSMDPRCompany cmp where Company='" + ddlCompany.SelectedValue + "'";
        DataTable DtName = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtName != null)
        {
            if (DtName.Rows.Count > 0)
            {
                ddlCompany.ToolTip = DtName.Rows[0][0].ToString();
                IndianFinYear = Convert.ToBoolean(DtName.Rows[0]["IsIndFyYr"]);
            }
        }
        BindVessel(ddlCompany.SelectedValue);
        BindddlYear();
        if (ddlCompany.SelectedValue == "0")
        {
            return;
        }
        lblSearchText.Text = ddlyear.SelectedItem.Text + " - " + ddlMonth.SelectedItem.Text + " - " + ddlVessel.SelectedValue + " - " + ddlReportLevel.SelectedItem.Text;
        Search();
    }
    protected void ddlReportLevel_OnSelectedIndexChanged(object sener, EventArgs e)
    {
        if (ddlReportLevel.SelectedIndex == 0)
            return;
        if (ddlCompany.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select Company.')", true);
            ddlReportLevel.SelectedIndex = 0;
            return;
        }
        if (ddlVessel.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "b", "alert('Please select Vessel.')", true);
            ddlReportLevel.SelectedIndex = 0;
            return;
        }
        
        if (ddlReportLevel.SelectedIndex == 4)
        {
            string Query = "";
            Query = ddlyear.SelectedValue + "~" + ddlMonth.SelectedValue + "~" + ddlCompany.SelectedItem + "~" + ddlVessel.SelectedItem + "~" + ddlCompany.SelectedValue + "~" + ddlVessel.SelectedValue + "~" + IndianFinYear + "~Account Details~4";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "window.open('PrintVarianceReport.aspx?Query=" + Query + "');", true);
        }
        else if (ddlReportLevel.SelectedIndex == 5)
        {
            int VessNum = GetVesselNum();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Printt", "window.open('Print.aspx?CoCode=" + ddlCompany.SelectedValue + "&VessNum=" + VessNum + "&CompName=" + ddlCompany.SelectedItem + "&VesselName=" + ddlVessel.SelectedItem + "&Month=" + ddlMonth.SelectedValue + "&ToMonth=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "');", true);
            ddlReportLevel.SelectedIndex = 0;
            //Response.Redirect("Print.aspx?CoCode=" + ddlCompany.SelectedValue + "&VessNum=" + VessNum + "&CompName=" + ddlCompany.SelectedItem + "&VesselName=" + ddlVessel.SelectedItem + "&Month=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "");
        }
        else if (ddlReportLevel.SelectedIndex == 6)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Printtt", "window.open('Print.aspx?LumpSum=1&CoCode=" + ddlCompany.SelectedValue + "&Period=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue+ "&VesselName=" + ddlVessel.SelectedItem + "&CompanyName=" + ddlCompany.SelectedItem.Text +"&VessCode="+ddlVessel.SelectedValue+"');", true);
            ddlReportLevel.SelectedIndex = 0;
        }
        //else if (ddlReportLevel.SelectedIndex == 7)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Printtttt", "window.open('Print.aspx?Comment=1&CoCode=" + ddlCompany.SelectedValue + "&Period=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "&VesselName=" + ddlVessel.SelectedItem + "&CompanyName=" + ddlCompany.SelectedItem.Text + "&VessCode=" + ddlVessel.SelectedValue + "');", true);
        //    ddlReportLevel.SelectedIndex = 0;
        //}
    }
    protected void ddlVessel_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue == "0")
        {
            return;
        }
        lblSearchText.Text = ddlyear.SelectedItem.Text + " - " + ddlMonth.SelectedItem.Text + " - " + ddlVessel.SelectedValue + " - " + ddlReportLevel.SelectedItem.Text;
        Search();
    }
    protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue == "0")
        {
            return;
        }
        lblSearchText.Text = ddlyear.SelectedItem.Text + " - " + ddlMonth.SelectedItem.Text + " - " + ddlVessel.SelectedValue + " - " + ddlReportLevel.SelectedItem.Text;
        Search();
    }
    protected void ddlyear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue == "0")
        {
            return;
        }
        lblSearchText.Text = ddlyear.SelectedItem.Text + " - " + ddlMonth.SelectedItem.Text + " - " + ddlVessel.SelectedValue + " - " + ddlReportLevel.SelectedItem.Text;
        Search();
    }
    // Button
    //protected void btnBack_Click(object sender, EventArgs e)
    //    {
    //        Response.Redirect("~/Search.aspx");   
    //    }
    protected void imgClear_Click(object sender, EventArgs e)
        {
            ClearSession();
            ddlyear.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
            BindVessel(ddlCompany.SelectedValue);
            ddlVessel.SelectedIndex = 0;
            
        }
    protected void imgSearch_Click(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue == "0")
        {
            return;
        }
        lblSearchText.Text = ddlyear.SelectedItem.Text + " - " + ddlMonth.SelectedItem.Text + " - " + ddlVessel.SelectedValue+" - "+ddlReportLevel.SelectedItem.Text;
        Search();
    }

    protected void imgBudgetForcasting_Click(object sender, EventArgs e)
    {
        Response.Redirect("BudgetForecasting.aspx");
    }
    protected void imgReportingAndAnalysis_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportingAndAnalysis.aspx");
    }
    protected void imgPrint_Click(object sender, EventArgs e)
    {
        string Query = "";
        if (ddlVessel.SelectedIndex == 0)
            Query = ddlyear.SelectedValue + "~" + ddlMonth.SelectedValue + "~" + ddlCompany.SelectedItem + "~" + "All" + "~" + ddlCompany.SelectedValue + "~" + ddlVessel.SelectedValue;
        else
            Query = ddlyear.SelectedValue + "~" + ddlMonth.SelectedValue + "~" + ddlCompany.SelectedItem + "~" + ddlVessel.SelectedItem + "~" + ddlCompany.SelectedValue + "~" + ddlVessel.SelectedValue + "~" + IndianFinYear + "~" + ddlReportLevel.SelectedItem + "~" + ddlReportLevel.SelectedIndex;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "window.open('PrintVarianceReport.aspx?Query=" + Query + "');", true);
    }
    protected void imgAccountDetails_Click(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex == 0)
        {
            return;
        }
        if (ddlVessel.SelectedIndex == 0)
        {
            return;
        }
        string Query = "";
        Query = ddlyear.SelectedValue + "~" + ddlMonth.SelectedValue + "~" + ddlCompany.SelectedItem + "~" + ddlVessel.SelectedItem + "~" + ddlCompany.SelectedValue + "~" + ddlVessel.SelectedValue + "~" + IndianFinYear + "~Account Details~4";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "window.open('PrintVarianceReport.aspx?Query=" + Query + "');", true);
    }
    protected void imgVariancereport_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select Company.')", true);
            return;
        }
        if (ddlVessel.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "alert('Please select Vessel.')", true);
            return;
        }
    }

    protected void imgBudgetCommentReport_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Printtttt", "window.open('Print.aspx?Comment=1&CoCode=" + ddlCompany.SelectedValue + "&Period=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "&VesselName=" + ddlVessel.SelectedItem + "&CompanyName=" + ddlCompany.SelectedItem.Text + "&VessCode=" + ddlVessel.SelectedValue + "');", true);
        ddlReportLevel.SelectedIndex = 0;
    }
    
    protected void imgView_OnClick(object sender, EventArgs e)
    {
        ImageButton imgView = (ImageButton)sender;
        HiddenField hfCommentID = (HiddenField)imgView.Parent.FindControl("hfCommentID");
        HiddenField hfComment = (HiddenField)imgView.Parent.FindControl("hfComment");
        HiddenField CommMajID = (HiddenField)imgView.Parent.FindControl("hfMajCatID");
        HiddenField CommMidID = (HiddenField)imgView.Parent.FindControl("hfMidCatID");

        String CommDetails = hfCommentID.Value + "," + ddlMonth.SelectedValue;
        string query = ddlyear.SelectedValue+ "," + ddlCompany.SelectedValue + "," + ddlVessel.SelectedValue+ "," + CommMajID.Value + "," + CommMidID.Value + "," + ddlMonth.SelectedValue; ;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewComments", "window.open('ViewUpdateBudgetComments.aspx?Query=" + query + "&IsEdit=0','','');", true);
    }
    protected void btnAdd_OnClick(object sender, EventArgs e)
    {

        ImageButton btnViewComment = (ImageButton)sender;
        HiddenField hfCommentID = (HiddenField)btnViewComment.Parent.FindControl("hfCommentID");
        HiddenField hfComment = (HiddenField)btnViewComment.Parent.FindControl("hfComment");
        HiddenField CommMajID = (HiddenField)btnViewComment.Parent.FindControl("hfMajCatID");
        HiddenField CommMidID = (HiddenField)btnViewComment.Parent.FindControl("hfMidCatID");
        
        int IsEdit = 1;
        String CommDetails = hfCommentID.Value + "," + ddlMonth.SelectedValue;
        string query = ddlyear.SelectedValue + "," + ddlCompany.SelectedValue+ "," + ddlVessel.SelectedValue + "," + CommMajID.Value + "," + CommMidID.Value + "," + ddlMonth.SelectedValue ;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewComments1", "window.open('ViewUpdateBudgetComments.aspx?Query=" + query + "&Commendetails=" + CommDetails + "&IsEdit=" + IsEdit.ToString() + "','','');", true);
    }
    protected void btnViewComment_OnClick(object sender, EventArgs e)
    {

        ImageButton btnViewComment = (ImageButton)sender;
        HiddenField hfCommentID = (HiddenField)btnViewComment.Parent.FindControl("hfCommentID");
        HiddenField hfComment = (HiddenField)btnViewComment.Parent.FindControl("hfComment");
        HiddenField CommMajID = (HiddenField)btnViewComment.Parent.FindControl("hfMajCatID");
        HiddenField CommMidID = (HiddenField)btnViewComment.Parent.FindControl("hfMidCatID");

        int IsEdit = 1;
        String CommDetails = hfCommentID.Value + "," + ddlMonth.SelectedValue;
        string query = ddlyear.SelectedValue + "," + ddlCompany.SelectedValue + "," + ddlVessel.SelectedValue + "," + CommMajID.Value + "," + CommMidID .Value+ "," + ddlMonth.SelectedValue;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewComments2", "window.open('ViewUpdateBudgetComments.aspx?Query=" + query + "&Commendetails=" + CommDetails + "&IsEdit=" + IsEdit.ToString() + "','','');", true);
    }
    protected void rptItems1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        CheckUpdatableRow(e);
    }
    protected void lnkActual_OnClick(object sender, EventArgs e)
    {
        int VessNum = GetVesselNum();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Printt", "window.open('Print.aspx?CoCode=" + ddlCompany.SelectedValue + "&VessNum=" + VessNum + "&CompName=" + ddlCompany.SelectedItem + "&VesselName=" + ddlVessel.SelectedItem + "&Month=" + ddlMonth.SelectedValue + "&ToMonth=" + ddlMonth.SelectedValue + "&Year=" + ddlyear.SelectedValue + "');", true);
        ddlReportLevel.SelectedIndex = 0;
    }
    

    // Function -------------------------------------------------------------
    public int GetVesselNum()
    {
        string sql = "SELECT Vesselno FROM vw_sql_tblSMDPRVessels where shipid='"+ddlVessel.SelectedValue+"'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
        {
            if (dt.Rows .Count> 0)
            {
                return Common.CastAsInt32(dt.Rows[0][0]);
            }
        }
        return 0;
    }
    public void Search()
    {
        //WriteSession();
        //string sql = "SELECT row_number() over(order by AM.MidSeqNo asc) as Row ,BLC.CommCo, BLC.CommYear, BLC.CommPer, BLC.CommShipID, BLC.CommMajID, BLC.CommMidID, BLC.Actual, BLC.Comm, " +
        //            " BLC.Budget, BLC.CommentID, AC.UserName, BLC.CommentDate, AM.MidCat, AC.Comment, " +
        //            " (isnull(BLC.Actual,0)+isnull(BLC.Comm,0)) as Consumed, " +
        //            " (case when len(convert(varchar(5000),AC.Comment))>56 then substring(AC.Comment,1,56)+'....' else AC.Comment end) as  SmallComment, " +
        //            "(isnull(BLC.Actual,0)+isnull(BLC.Comm,0)-isnull( BLC.Budget,0))as V$ , " +
        //            "(case when len(convert(varchar(7000), AC.Comment))>56 then 'bold' else 'none'end ) as IsImageVisible ," +
        //            "(case when isnull( BLC.Budget,0)=0 then 0 " +
        //            " else (isnull(BLC.Actual,0)+isnull(BLC.Comm,0)-isnull( BLC.Budget,0))* 100/isnull( BLC.Budget,0)end)as VPer " +
        //            " FROM 	[dbo].tblBudgetLevelComments BLC INNER JOIN [dbo].tblAccountsMid AM ON BLC.CommMidID = AM.MidCatID" +
        //            " LEFT JOIN [dbo].tblAnalysisComments AC ON BLC.CommCo = AC.CommCo AND BLC.CommYear = AC.CommYear" +
        //            " AND BLC.CommShipID = AC.CommShipID AND BLC.CommMajID = AC.CommMajID AND BLC.CommMidID = AC.CommMidID" +
        //            " WHERE BLC.CommCo= '" + ddlCompany.SelectedValue + "' AND BLC.CommYear=" + ddlyear.SelectedValue + " AND BLC.CommPer=" + ddlMonth.SelectedValue + " AND BLC.CommShipID='" + ddlVessel.SelectedValue + "' ORDER BY AM.MidSeqNo";

        if (ddlCompany.SelectedIndex <= 0 || ddlVessel.SelectedIndex <= 0)
        {
            rptItems.DataSource =null;
            rptItems.DataBind();  
            return;
        }

        lblDays.Text = "0";
        string sql = "SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear = '" + ddlyear.SelectedValue + "' AND VDAYS.CoCode = '" + ddlCompany.SelectedValue + "' AND VDAYS.ShipId = '" + ddlVessel.SelectedValue + "' ORDER BY VDAYS.YEARDAYS DESC ";
        DataTable dtdays = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtdays.Rows.Count > 0)
        {
            lblDays.Text = dtdays.Rows[0]["YEARDAYS"].ToString();
        }

        lblBudgetDays.Text = "0";
        if (ddlyear.SelectedValue != "")
        {
            DataTable dtBudgetdays = Opex.GetYearofDaysfromBudget(Convert.ToInt32(ddlMonth.SelectedValue), ddlyear.SelectedValue, ddlCompany.SelectedValue, ddlVessel.SelectedValue);
            if (dtBudgetdays.Rows.Count > 0)
            {
                lblBudgetDays.Text = dtBudgetdays.Rows[0]["Days"].ToString();
            }
        }

        int MonthDays = 0;
        if (ddlyear.SelectedValue != "")
        {
            MonthDays = Opex.GetNodays(ddlyear.SelectedValue, Convert.ToInt32(ddlMonth.SelectedValue));
        }
        lblMonthDays.Text = MonthDays.ToString();



        //Common.Set_Procedures("getVarianceRepport");
        //Common.Set_ParameterLength(4);
        //Common.Set_Parameters(
        //                    new MyParameter("@COMPCODE ",ddlCompany.SelectedValue),
        //                    new MyParameter("@MNTH", ddlMonth.SelectedValue),
        //                    new MyParameter("@YR", ddlyear.SelectedValue),
        //                    new MyParameter("@VSLCODE", ddlVessel.SelectedValue)
        //    );
        //DsValue = Common.Execute_Procedures_Select();

        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString());
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec getVarianceRepport_vsl '" + ddlCompany.SelectedValue + "'," + ddlMonth.SelectedValue + ",'" + ddlyear.SelectedValue + "','" + ddlVessel.SelectedValue + "'", con);
        cmd.CommandTimeout = 300;
        cmd.CommandType = CommandType.Text;
        
        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
        adp.SelectCommand = cmd;
        DsValue = new DataSet(); 
        adp.Fill(DsValue);  
        
        if (DsValue != null)
        {
            rptItems.DataSource = DsValue.Tables[0];
            rptItems.DataBind();
            //Set Period
            if (DsValue.Tables[0].Rows.Count > 0)
            {
                if (IndianFinYear)
                {
                    int period = Convert.ToInt32(DsValue.Tables[0].Rows[0]["PERIOD"]);
                    int USFinMonth = Opex.GetUSFinMonth(period);
                    lblTargetUtilisation.Text = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(USFinMonth) / 12) * 100), 0) + " %";
                }
                else
                {
                  lblTargetUtilisation.Text = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(DsValue.Tables[0].Rows[0]["PERIOD"]) / 12) * 100), 0) + " %";
                }
            }
            else
            {
                LblNoRow.Text = "No Records Found ";
            }
        }
        else
        {
            LblNoRow.Text = "No Records Found ";  
        }
    }
    public void BindYear()
    {
        //ddlyear.Items.Add(new ListItem("Select","0"));
        for (int i = System.DateTime.Now.Year,j=1; ; i--,j++)
        {
            ddlyear.Items.Add(i.ToString());
            if (j == 11)
                break;
        }
    }
    public void SetMonth()
    {
        ddlMonth.SelectedValue = (Common.CastAsInt32(System.DateTime.Now.Month) - 1).ToString();
    }
    public void BindCompany()
    {
        string sql = "selECT cmp.Company, cmp.[Company Name] as CompanyName, cmp.Active, cmp.InAccts FROM vw_sql_tblSMDPRCompany cmp WHERE (((cmp.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            if (DtCompany .Rows.Count>0)
            {
                ddlCompany.DataSource = DtCompany;
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataValueField = "Company";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("< Select >", ""));
            }
        }
        
    }
    public void BindVessel(string Comp)
    {
        string sql = "SELECT vsl.ShipID, vsl.ShipName, vsl.Company, vsl.VesselNo, vsl.Active FROM vw_sql_tblSMDPRVessels vsl WHERE (((vsl.Active)='A')) and vsl.Company='"+Comp+ "' and vsl.VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString())+")";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
                ddlVessel.DataSource = DtVessel;
                ddlVessel.DataTextField = "ShipName";
                ddlVessel.DataValueField = "ShipID";
                ddlVessel.DataBind();
                ddlVessel.Items.Insert(0, new ListItem("< Select >",""));
        }
        
    }
    public void BindddlYear()
    {
        string sql = "Select  distinct CurFinYear from AccountCompanyBudgetMonthyear with(nolock) where Cocode = '" + ddlCompany.SelectedValue + "'";
        DataTable dtCurrentyear = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCurrentyear != null)
        {
            ddlyear.DataSource = dtCurrentyear;
            ddlyear.DataTextField = "CurFinYear";
            ddlyear.DataValueField = "CurFinYear";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("< Select >", ""));
        }
    }

    public DataTable getItems1(object MAJCATID)
    {
        //if (ddlReportLevel.SelectedIndex > 1 || ddlReportLevel.SelectedIndex == 0) // Old if condition
        if (true)
        {
            DataTable Dt = DsValue.Tables[1];
            DataView DV = Dt.DefaultView;
            DV.RowFilter = "MAJCATID="+MAJCATID.ToString();

            //-------------------
            Dt = DV.ToTable();
            if (!Dt.Columns.Contains("CommentID"))
            {
                Dt.Columns.Add("CommentID", typeof(int));
                Dt.Columns.Add("Comment", typeof(string));
            }
            string sql = "";

            foreach (DataRow Dr in Dt.Rows)
            {
                sql = "select CommentID,Comment  from [dbo].tblBudgetLevelComments " +
                    "where CommCo='" + ddlCompany.SelectedValue + "' and CommYear=" + ddlyear.SelectedValue + "and CommPer=" + ddlMonth.SelectedValue + " and CommMajID=" + MAJCATID.ToString() + " and CommShipID='" + ddlVessel.SelectedValue + "' and CommMidID= " + Dr ["MidCatID"].ToString()+ "";
                DataTable DtCom = Common.Execute_Procedures_Select_ByQuery(sql);
                if (DtCom != null)
                {
                    if (DtCom.Rows.Count > 0)
                       {
                        Dr["CommentID"] = DtCom.Rows[0][0].ToString();
                        Dr["Comment"] = DtCom.Rows[0][1].ToString();
                    }
                }
                
            }
            //--------------------
            return Dt;
        }
        else
        {
            DataTable Dt = DsValue.Tables[1];
            Dt.Clear();            
            //Dt.Columns.Contains("ColumnName");
            if (!Dt.Columns.Contains("CommentID"))
            {
                Dt.Columns.Add("CommentID", typeof(int));
                Dt.Columns.Add("Comment", typeof(string));
            }
            return Dt;
        }
    }
    public DataTable getItems2(object MIDCATID)
    {
        //if (ddlReportLevel.SelectedIndex > 2 || ddlReportLevel.SelectedIndex ==0)
        if (false)
        {
            DataTable Dt = DsValue.Tables[2];
            DataView DV = Dt.DefaultView;
            DV.RowFilter = "MIDCATID=" + MIDCATID.ToString();
            return DV.ToTable();
        }
        else
        {
            DataTable Dt = DsValue.Tables[2];
            Dt.Clear();
            return Dt;
        }
    }

    public string Round(string Val)
    {
            return Math.Round(Convert.ToDecimal( Val),0).ToString();
    }
    public string RoundWith1(string Val)
    {
        return Math.Round(Convert.ToDecimal(Val), 1).ToString();
    }

    //public string SetColorForVPer(object val)
    //{
    //    if (Convert.ToDecimal( val )>= 0)
    //        return "error_msg";
    //    else
    //        return "";
    //}
    //public string SetColorForYearPer(object val)
    //{
    //    if (Convert.ToDecimal(val) >= 100)
    //        return "error_msg";
    //    else
    //        return "";
    //}

    public void ExpandReport()
    {
        if (ddlReportLevel.SelectedIndex == 2)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "show", "ExpandLevel2and3();", true);
        }
        else if (ddlReportLevel.SelectedIndex == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "show", "ExpandLevel2("+rptItems.Items.Count+");", true);
        }
    }

    public void CheckUpdatableRow(RepeaterItemEventArgs Row)
    {
        //-----------------
        DateTime? MailSentDate=null; 
        DataTable dtInformed = Common.Execute_Procedures_Select_ByQuery("select *,isnull(MailSent,'N') AS MailGone from dbo.tbl_Publish_InformSuptd where cocode='" + ddlCompany.SelectedValue + "' and [year]=" + ddlyear.SelectedValue + " and [month]=" + ddlMonth.SelectedValue);
        if (dtInformed.Rows.Count > 0)
        {
            if (dtInformed.Rows[0]["MailGone"].ToString() == "Y")
            {
                MailSentDate = Convert.ToDateTime(dtInformed.Rows[0]["SendOn"]);
            }
        }

        
        //-----------------
        //if (Date >= CurrentDate)
        //{
        //    ImageButton btnAdd = (ImageButton)Row.Item.FindControl("btnAdd");
        //    btnAdd.Visible = true & MailSenttoSuptd;
        //}
        //else if (MailSentDate.AddDays(15) >= CurrentDate)
        //{
        //    ImageButton btnViewComment = (ImageButton)Row.Item.FindControl("btnViewComment");
        //    ImageButton imgView = (ImageButton)Row.Item.FindControl("imgView");
        //    HiddenField hfCommentID = (HiddenField)Row.Item.FindControl("hfCommentID");

        //    Boolean IsLocked = GetLockCondition(Common.CastAsInt32(hfCommentID.Value));
        //    if (IsLocked)
        //    {
        //        imgView.Visible = true;
        //        imgView.ImageUrl = "~/Images/Lock.png";
        //        imgView.ToolTip = "Locked";
        //    }
        //    else
        //    {
        //        btnViewComment.Visible = true & MailSenttoSuptd;
        //    }
        //}
        //else if (Common.CastAsInt32(MM) + 1 == Common.CastAsInt32(CMM) && Common.CastAsInt32(YY) == Common.CastAsInt32(CYY))
        //{
        //    ImageButton btnViewComment = (ImageButton)Row.Item.FindControl("btnViewComment");
        //    ImageButton imgView = (ImageButton)Row.Item.FindControl("imgView");
        //    HiddenField hfCommentID = (HiddenField)Row.Item.FindControl("hfCommentID");

        //    Boolean IsLocked = GetLockCondition(Common.CastAsInt32(hfCommentID.Value));
        //    if (IsLocked)
        //    {
        //        imgView.Visible = true;
        //        imgView.ImageUrl = "~/Images/Lock.png";
        //        imgView.ToolTip = "Locked";
        //    }
        //    else
        //    {
        //        btnViewComment.Visible = true & MailSenttoSuptd;
        //    }
        //}
        //else if (Common.CastAsInt32(MM) == 12 && Common.CastAsInt32(CMM) == 1 && Common.CastAsInt32(YY) + 1 == Common.CastAsInt32(CYY))
        //{
        //    ImageButton btnViewComment = (ImageButton)Row.Item.FindControl("btnViewComment");
        //    ImageButton imgView = (ImageButton)Row.Item.FindControl("imgView");
        //    HiddenField hfCommentID = (HiddenField)Row.Item.FindControl("hfCommentID");

        //    Boolean IsLocked = GetLockCondition(Common.CastAsInt32(hfCommentID.Value));
        //    if (IsLocked)
        //    {
        //        imgView.Visible = true;
        //        imgView.ImageUrl = "~/Images/Lock.png";
        //        imgView.ToolTip = "Locked";
        //    }
        //    else
        //    {
        //        btnViewComment.Visible = true & MailSenttoSuptd;
        //    }
        //}


        ImageButton btnViewComment = (ImageButton)Row.Item.FindControl("btnViewComment");
        ImageButton imgView = (ImageButton)Row.Item.FindControl("imgView");
        HiddenField hfCommentID = (HiddenField)Row.Item.FindControl("hfCommentID");

        if (MailSentDate == null)
            btnViewComment.Visible = false;
        else
        {
            btnViewComment.Visible = (MailSentDate.Value.AddDays(40) >= DateTime.Today);
            btnViewComment.ToolTip = MailSentDate.ToString();
        }

        HiddenField hfComment = (HiddenField)Row.Item.FindControl("hfComment");
        if (hfComment.Value == "")
            imgView.Visible = false;
        else
            imgView.Visible = true;

    }
    public bool GetLockCondition(int CommentID)
    {
        string sql = "select * from BudgetLockTable where CommentID=" + CommentID + "";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT != null)
        {
            if (DT.Rows.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
    // Load Session 
    private void LoadSession()
    {
        string[] Delemeters = { SessionDeleimeter };
        string values = "" + Session["ReportingAnalysis"];
        string[] ValueList = values.Split(Delemeters, StringSplitOptions.None);
        try
        {
            
            ddlyear.SelectedValue = ValueList[0];
            ddlMonth.SelectedValue = ValueList[1];
            ddlCompany.SelectedValue = ValueList[2];
            BindVessel(ddlCompany.SelectedValue);
            ddlVessel.SelectedValue = ValueList[3];

            Search();
        }
        catch { }
    }
    private void WriteSession()
    {
        string values = ddlyear.SelectedValue + SessionDeleimeter +
                        ddlMonth.SelectedValue + SessionDeleimeter +
                        ddlCompany.SelectedValue + SessionDeleimeter +
                        ddlVessel.SelectedValue + SessionDeleimeter;
        Session["ReportingAnalysis"] = values;
    }
    private void ClearSession()
    {
        Session["ReportingAnalysis"] = null;
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



}


