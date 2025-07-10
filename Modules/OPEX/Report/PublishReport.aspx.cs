using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient; 

public partial class PublishReport : System.Web.UI.Page
{
    AuthenticationManager authRecInv;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    DataSet DsValue;
    string MsgFinal = "";
    //string ConnectionString = ConfigurationManager.AppSettings["NSIPLEMANAGER"];
    public bool IsIndianFinacialYear
    {
        set { ViewState["IsIndianFinacialYear"] = value; }
        get { return Convert.ToBoolean(ViewState["IsIndianFinacialYear"]); }
    }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        lblMessage.Text = "";
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1097, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
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
        Manage_Menu(); 
        if (authRecInv.IsVerify || authRecInv.IsVerify2)
        {
            btnUpdateTransaction.Enabled = true;
            btnUpdateTransaction.CssClass = "btn";
        }
        else
        {
            btnUpdateTransaction.Enabled = false; 
            btnUpdateTransaction.CssClass = "";
        }
        try
        {
            if (!Page.IsPostBack)
            {
                BindCompany();
                BindList();
              //  ddlCompany_OnSelectedIndexChanged(sender, e);
              //  BindYear();
            }
        }
        catch { }
    }
    protected void ShowNextPublish()
    {
        if (ddlCompany.SelectedIndex == 0) 
        { lblMonth.Text = ""; btnPubLish.Visible = false; btnClose.Visible = false; return; }
        //-------------- 
        //DataTable dt=Common.Execute_Procedures_Select_ByQuery("select TOP 1 rptyear,rptperiod,rptLink from dbo.tblperiodmaint where rptyear<=year(getdate()) and cocode='" + ddlCompany.SelectedValue + "' AND PERCLOSED=1 order by rptyear desc,rptPERIOD desc ");
        //if (dt.Rows.Count > 0)
        //{
        //    int Month= Common.CastAsInt32(dt.Rows[0][1]); 
        //    int Year= Common.CastAsInt32(dt.Rows[0][0]);
       
        //    if(Month==12)
        //    {
        //        Year++;
        //        Month=1;
        //    }
        //    else
        //    {
        //        Month ++;
        //    }
        //    lblMonth.Text = ProjectCommon.GetFullMonthName(Month.ToString()) + "  -  " + Year.ToString();
        //    ViewState["PMonth"] = Month;
        //    ViewState["PYear"] = Year;
        //    //-----------------------------
        //    //dt = Common.Execute_Procedures_Select_ByQuery("select rptLink from dbo.tblperiodmaint where rptyear=" + Year.ToString() + " and cocode='" + ddlCompany.SelectedValue + "' AND rptPERIOD =" + Month.ToString());
        //    //if (dt.Rows.Count > 0) // record published 
        //    //{
        //    //    if (dt.Rows[0][0].ToString().Trim() != "") // record published 
        //    //    {
        //    //        btnClose.Visible = true && authRecInv.IsVerify2;
        //    //    }
        //    //    else
        //    //    {
        //    //        btnClose.Visible = false;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    btnClose.Visible = false;
        //    //}
            
        //}
        //else
        //{
            lblMonth.Text = ProjectCommon.GetFullMonthName(DateTime.Today.Month.ToString()) + "  -  " + DateTime.Today.Year.ToString();
            ViewState["PMonth"] = DateTime.Today.Month;
            ViewState["PYear"] = DateTime.Today.Year;
       // }
        btnPubLish.Visible = true && authRecInv.IsVerify;
        //btnPubLish.Visible = true;
        ddlPublishPeriod.Items.Clear();   
        DateTime dtSt = new DateTime( Common.CastAsInt32(ViewState["PYear"]),  Common.CastAsInt32(ViewState["PMonth"]), 1);
        dtSt=dtSt.AddMonths(-1);
        DateTime dtLast = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        //------------------------------ NEW CODE FOR PUBLISH ----------------------
        for (int i = 0; i <= rptItems.Items.Count - 1; i++)
        {
            
            Button _btnPublish = (Button)rptItems.Items[i].FindControl("btnPublish");
            //Button _btnInformSuptd = (Button)rptItems.Items[i].FindControl("btnInformSuptd");
            //Button _btnPublishComm = (Button)rptItems.Items[i].FindControl("btnPublishComm");
            //Button _btnGridPublishPOComm = (Button)rptItems.Items[i].FindControl("btnPublishPOComm");
            
            Button _btnClosure = (Button)rptItems.Items[i].FindControl("btnClosure");

            int Month = Common.CastAsInt32(_btnPublish.CommandArgument);
            HiddenField hdnrptYear = (HiddenField)rptItems.Items[i].FindControl("hdnRptYear");
            HiddenField hdnRptClosed = (HiddenField)rptItems.Items[i].FindControl("hdnRptClosed");
            HiddenField hdnRptPeriod = (HiddenField)rptItems.Items[i].FindControl("hdnRptPeriod");
            DateTime Monthdt = new DateTime(Common.CastAsInt32(hdnrptYear.Value), Month, 1);
            //string link = _btnPublishComm.Attributes["link"].Trim();

            // new line -- MailSenttoSuptd 
            //bool MailSenttoSuptd = false; 
            //DataTable dtInformed= Common.Execute_Procedures_Select_ByQuery("select *,isnull(MailSent,'N') AS MilGone from dbo.tbl_Publish_InformSuptd where cocode='" + ddlCompany.SelectedValue + "' and [year]=" + Common.CastAsInt32(hdnrptYear.Value) + " and [month]="  + Month.ToString());
            //if(dtInformed.Rows.Count>0)
            //if (dtInformed.Rows[0]["MilGone"].ToString() == "Y")
            //    MailSenttoSuptd = true;
            // new line -- MailSenttoSuptd 

            if ((Convert.ToInt32(hdnRptPeriod.Value) <= Convert.ToInt32(ViewState["PMonth"])) && (Convert.ToInt32(hdnrptYear.Value)) <= Convert.ToInt32(ViewState["PYear"])) //Monthdt >= dtSt && Monthdt <= dtLast
            {
              if (authRecInv.IsVerify || authRecInv.IsVerify2)
                {
                    _btnPublish.Enabled = true; //&& (!MailSenttoSuptd)
                    _btnPublish.CssClass = "btn";
                }
              else
                {
                    _btnPublish.Enabled = false; //&& (!MailSenttoSuptd)
                    _btnPublish.CssClass = "";
                }
               
                // _btnPublish.Enabled = true && (!MailSenttoSuptd);
            }
            else
            {
                _btnPublish.Enabled = false;
                _btnPublish.CssClass = "";
            }

            //_btnInformSuptd.Enabled = _btnPublish.Enabled && link != "";

            ////_btnPublishComm.Enabled = _btnPublish.Enabled && link != "";
            //_btnPublishComm.Enabled = MailSenttoSuptd && link != "";
            //_btnGridPublishPOComm.Enabled = _btnPublishComm.Enabled;


            if ((Convert.ToInt32(hdnRptPeriod.Value) <= Common.CastAsInt32(ViewState["PMonth"])) && (Convert.ToInt32(hdnrptYear.Value)) <= Convert.ToInt32(ViewState["PYear"]))
            {
                if (hdnRptClosed.Value == "True")
                {
                    _btnClosure.Enabled = false;
                    _btnClosure.CssClass = "";
                    _btnPublish.Enabled = false;
                    _btnPublish.CssClass = "";
                    _btnClosure.Text = "Closed";
                }
                else
                {
                    if (authRecInv.IsVerify || authRecInv.IsVerify2)
                    {
                        _btnClosure.Enabled = true;
                        _btnClosure.CssClass = "btn";
                    }
                    else
                    {
                        _btnClosure.Enabled = false;
                        _btnClosure.CssClass = "";
                    }
                      
                }
            }
            else
            {
                _btnClosure.Enabled = false;
                _btnClosure.CssClass = "";
            }

           


            //if (_btnClosure.Enabled)
            //       {
            //           _btnGridPublishPOComm.Enabled = true;
            //       }
            //else
            //{
            //           _btnGridPublishPOComm.Enabled = false;
            //}


            //if (!_btnClosure.Enabled)
            //{
            //    _btnGridPublishPOComm.Enabled = false;
            //}

            // _btnClosure.Text =(_btnClosure.Enabled)?"Close Now":"Closed";
            //_btnPublish.Text = (_btnPublish.Enabled) ? "Publish Now" : "Published";
            //_btnPublishComm.Text = (_btnPublishComm.Enabled) ? "Publish Comments" : "Published";
        }
        //------------------------------ NEW CODE FOR PUBLISH ----------------------

        while (dtSt <= dtLast)
        {
            ddlPublishPeriod.Items.Add(new ListItem(dtSt.ToString("MMM - yyyy").ToUpper(), dtSt.ToString("MM-yyyy").ToUpper()));
            dtSt = dtSt.AddMonths(1);
        }
        //------------------------------
        lblMonth.Visible = btnClose.Visible;
        
    }
    // Event ----------------------------------------------------------------
    // Button
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");   
    }
    protected void imgClear_Click(object sender, EventArgs e)
    {
        ddlyear.SelectedIndex = 0;
        ddlCompany.SelectedIndex = 0;
    }
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string Error="";
        if (ddlCompany.SelectedIndex == 0)
        {
            return;
        }
        BindYear(); 
        if (ddlyear.SelectedIndex == 0)
        {
            return;
        }
        GetIndianFinacialYear();
        CreatePeriods(ddlCompany.SelectedValue,ddlyear.SelectedValue);
        //if (ddlCompany.SelectedValue.Trim() != "")
        //{
        //    if (ProjectCommon.VERIFIYPOSTINGS(ddlCompany.SelectedValue, ref Error))
        //    {
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Verifiy posting failed for apentries. Error : " + Error; 
        //        return; 
        //    }
        //}
        BindList();
        ShowNextPublish();
        
    }
    protected void CreatePeriods(string Company,string Year)
    {
        Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[CreatePeriods] '" + Company + "','" + Year + "'"); 
    }
    public void BindList()
    {
        string str = "SELECT tblPeriodMaint.*,rptkey as rptkey2,replace(convert(varchar,dateupdated,106),' ','-') as UpdateOn  FROM dbo.tblPeriodMaint where CurFinYear='" + ddlyear.SelectedValue + "' and cocode='" + ddlCompany.SelectedValue + "'";
        rptItems.DataSource = Common.Execute_Procedures_Select_ByQuery(str);
       rptItems.DataBind();  
    }
    protected void Review_Report(object sender, EventArgs e)
    {
        string Month = ((Button)(sender)).CommandArgument;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('previewreport.aspx?Cocode=" + ddlCompany.SelectedValue + "&Period=" + Month + "&Year=" + ddlyear.SelectedValue + "&CoName=" + ddlCompany.SelectedItem.Text + "')", true);  
    }
    protected void btnPublishReport_Click(object sender, EventArgs e)
    {
        int Year, Month;
        string CurFinYear = "";
        Button _btnPublish = (Button)sender;

        Month=Common.CastAsInt32(_btnPublish.CommandArgument);
        Year = Common.CastAsInt32(((Button)sender).Attributes["rptyear"].ToString());
        CurFinYear = ddlyear.SelectedValue;  
       
        PublishNow(Month, Year, CurFinYear);
       
    }
    protected void WriteLog(string Msg)
    {
        MsgFinal += System.Environment.NewLine + Msg + " , Time :" + DateTime.Now.ToLongTimeString() + System.Environment.NewLine;
    }
    protected void WriteLogTOFile()
    {
        string FilePath = Server.MapPath("~/Modules/OPEX/Publish/publish.log");
        if (File.Exists(FilePath))
        {
            File.WriteAllText(FilePath,MsgFinal);
        }
    }
    
    protected void PublishNow(int Month, int Year, string CurFinYear)
    {
        
        string CoCode, CoName, MonthName;
        CoCode = ddlCompany.SelectedValue;
        CoName = ddlCompany.SelectedItem.Text;
        MonthName = ProjectCommon.GetFullMonthName(Month.ToString());
        string SQl = "Delete from dbo.tblOwnerReportsV2 where ltrim(rtrim(fldReportCocode))='" + CoCode.Trim() + "' and ltrim(rtrim(fldReportYear))='" + Year.ToString() + "' and ltrim(rtrim(fldReportMonth))='" + Month.ToString() + "' and CurFinYear = '" + CurFinYear.ToString() + "'";
        Common.Execute_Procedures_Select_ByQuery(SQl);
        WriteLog("Deleted existing reports.");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString() + ";Connection Timeout=900;");
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        WriteLog("Publish Start.");
        //---------------------------------------------
        //cmd1.Parameters.Add(new SqlParameter("@PdfFile", getFile(1, Month, Year, CoCode, CoName)));
        SqlCommand cmd_InsertBlankComments = new SqlCommand("dbo.INSERTBLANKCOMMENTS", con, trans);
        cmd_InsertBlankComments.CommandTimeout = 300;
        cmd_InsertBlankComments.CommandType = CommandType.StoredProcedure;
        cmd_InsertBlankComments.Parameters.Add(new SqlParameter("@COMMCO", CoCode));
        cmd_InsertBlankComments.Parameters.Add(new SqlParameter("@COMMYEAR", Year));
        cmd_InsertBlankComments.Parameters.Add(new SqlParameter("@COMMPER", Month));
        cmd_InsertBlankComments.Parameters.Add(new SqlParameter("@USERNAME", Session["FullName"].ToString()));
        cmd_InsertBlankComments.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear)); 
        WriteLog("Commands 'Blank Comment' Processed.");
        //---------------------------------------------
        SqlCommand cmd1 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd1.CommandTimeout = 300;
        cmd1.CommandType = CommandType.StoredProcedure;
        cmd1.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd1.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd1.Parameters.Add(new SqlParameter("@Year", Year));
        cmd1.Parameters.Add(new SqlParameter("@Month", Month));
        cmd1.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd1.Parameters.Add(new SqlParameter("@ReportType", 1));
        cmd1.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd1.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp1 = new SqlParameter("@PdfFile", SqlDbType.Image);

        sp1.Value = DBNull.Value;
        cmd1.Parameters.Add(sp1);
        WriteLog("Commands 1 Processed.");
        //---------------------------------------------
        SqlCommand cmd2 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd2.CommandTimeout = 300;
        cmd2.CommandType = CommandType.StoredProcedure;
        cmd2.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd2.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd2.Parameters.Add(new SqlParameter("@Year", Year));
        cmd2.Parameters.Add(new SqlParameter("@Month", Month));
        cmd2.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd2.Parameters.Add(new SqlParameter("@ReportType", 2));
        cmd2.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd2.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp2 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp2.Value = DBNull.Value;
        cmd2.Parameters.Add(sp2);
        WriteLog("Commands 2 Processed.");
        //---------------------------------------------
        SqlCommand cmd3 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd3.CommandTimeout = 300;
        cmd3.CommandType = CommandType.StoredProcedure;
        cmd3.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd3.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd3.Parameters.Add(new SqlParameter("@Year", Year));
        cmd3.Parameters.Add(new SqlParameter("@Month", Month));
        cmd3.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd3.Parameters.Add(new SqlParameter("@ReportType", 3));
        cmd3.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd3.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp3 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp3.Value = DBNull.Value;
        cmd3.Parameters.Add(sp3);
        WriteLog("Commands 3 Processed.");
        //---------------------------------------------
        SqlCommand cmd13 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd13.CommandTimeout = 300;
        cmd13.CommandType = CommandType.StoredProcedure;
        cmd13.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd13.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd13.Parameters.Add(new SqlParameter("@Year", Year));
        cmd13.Parameters.Add(new SqlParameter("@Month", Month));
        cmd13.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd13.Parameters.Add(new SqlParameter("@ReportType", 13));
        cmd13.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd13.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp13 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp13.Value = DBNull.Value;
        cmd13.Parameters.Add(sp13);
        WriteLog("Commands 13 Processed.");
        //---------------------------------------------
        SqlCommand cmd4 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd4.CommandTimeout = 300;
        cmd4.CommandType = CommandType.StoredProcedure;
        cmd4.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd4.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd4.Parameters.Add(new SqlParameter("@Year", Year));
        cmd4.Parameters.Add(new SqlParameter("@Month", Month));
        cmd4.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd4.Parameters.Add(new SqlParameter("@ReportType", 4));
        cmd4.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd4.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp4 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp4.Value = DBNull.Value;
        cmd4.Parameters.Add(sp4);
        WriteLog("Commands 4 Processed.");
        //---------------------------------------------
        SqlCommand cmd5 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd5.CommandTimeout = 300;
        cmd5.CommandType = CommandType.StoredProcedure;
        cmd5.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd5.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd5.Parameters.Add(new SqlParameter("@Year", Year));
        cmd5.Parameters.Add(new SqlParameter("@Month", Month));
        cmd5.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd5.Parameters.Add(new SqlParameter("@ReportType", 5));
        cmd5.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd5.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp5 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp5.Value=DBNull.Value;
        cmd5.Parameters.Add(sp5);
        WriteLog("Commands 5 Processed.");
        //---------------------------------------------
        SqlCommand cmd15 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd15.CommandTimeout = 300;
        cmd15.CommandType = CommandType.StoredProcedure;
        cmd15.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd15.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd15.Parameters.Add(new SqlParameter("@Year", Year));
        cmd15.Parameters.Add(new SqlParameter("@Month", Month));
        cmd15.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd15.Parameters.Add(new SqlParameter("@ReportType", 15));
        cmd15.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd15.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp15 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp15.Value = DBNull.Value;
        cmd15.Parameters.Add(sp15);
        WriteLog("Commands 15 Processed.");
        //---------------------------------------------
        SqlCommand cmd6 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd6.CommandTimeout = 300;
        cmd6.CommandType = CommandType.StoredProcedure;
        cmd6.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd6.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd6.Parameters.Add(new SqlParameter("@Year", Year));
        cmd6.Parameters.Add(new SqlParameter("@Month", Month));
        cmd6.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd6.Parameters.Add(new SqlParameter("@ReportType", 6));
        cmd6.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd6.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp6 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp6.Value = DBNull.Value;
        cmd6.Parameters.Add(sp6);
        WriteLog("Commands 6 Processed.");
        //---------------------------------------------
        //SqlCommand cmd7 = new SqlCommand("dbo.ExportReport", con, trans);
        //cmd7.CommandTimeout = 300;
        //cmd7.CommandType = CommandType.StoredProcedure;
        //cmd7.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        //cmd7.Parameters.Add(new SqlParameter("@CoName", CoName));
        //cmd7.Parameters.Add(new SqlParameter("@Year", Year));
        //cmd7.Parameters.Add(new SqlParameter("@Month", Month));
        //cmd7.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        //cmd7.Parameters.Add(new SqlParameter("@ReportType", 7));
        //cmd7.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        //cmd7.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        //SqlParameter sp7 = new SqlParameter("@PdfFile", SqlDbType.Image);
        //sp7.Value = DBNull.Value;
        //cmd7.Parameters.Add(sp7);
        //WriteLog("Commands 7 Processed.");
        SqlCommand cmd7 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd7.CommandTimeout = 300;
        cmd7.CommandType = CommandType.StoredProcedure;
        cmd7.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd7.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd7.Parameters.Add(new SqlParameter("@Year", Year));
        cmd7.Parameters.Add(new SqlParameter("@Month", Month));
        cmd7.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd7.Parameters.Add(new SqlParameter("@ReportType", 7));
        cmd7.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd7.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp7 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp7.Value = DBNull.Value;
        cmd7.Parameters.Add(sp7);
        WriteLog("Commands 7 Processed.");
        //---------------------------------------------
        SqlCommand cmd8 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd8.CommandTimeout = 600;
        cmd8.CommandType = CommandType.StoredProcedure;
        cmd8.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd8.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd8.Parameters.Add(new SqlParameter("@Year", Year));
        cmd8.Parameters.Add(new SqlParameter("@Month", Month));
        cmd8.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd8.Parameters.Add(new SqlParameter("@ReportType", 8));
        cmd8.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd8.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp8 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp8.Value = DBNull.Value;
        cmd8.Parameters.Add(sp8);
        WriteLog("Commands 8 Processed.");
        //---------------------------------------------
        SqlCommand cmd9 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd9.CommandTimeout = 600;
        cmd9.CommandType = CommandType.StoredProcedure;
        cmd9.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd9.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd9.Parameters.Add(new SqlParameter("@Year", Year));
        cmd9.Parameters.Add(new SqlParameter("@Month", Month));
        cmd9.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd9.Parameters.Add(new SqlParameter("@ReportType", 9));
        cmd9.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd9.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp9 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp9.Value = DBNull.Value;
        cmd9.Parameters.Add(sp9);
        WriteLog("Commands 9 Processed.");
        //---------------------------------------------
        SqlCommand cmd16 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd16.CommandTimeout = 300;
        cmd16.CommandType = CommandType.StoredProcedure;
        cmd16.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd16.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd16.Parameters.Add(new SqlParameter("@Year", Year));
        cmd16.Parameters.Add(new SqlParameter("@Month", Month));
        cmd16.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd16.Parameters.Add(new SqlParameter("@ReportType", 16));
        cmd16.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd16.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp16 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp16.Value = DBNull.Value;
        cmd16.Parameters.Add(sp16);
        WriteLog("Commands 16 Processed.");
        //---------------------------------------------
        SqlCommand cmd17 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd17.CommandTimeout = 300;
        cmd17.CommandType = CommandType.StoredProcedure;
        cmd17.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd17.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd17.Parameters.Add(new SqlParameter("@Year", Year));
        cmd17.Parameters.Add(new SqlParameter("@Month", Month));
        cmd17.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd17.Parameters.Add(new SqlParameter("@ReportType", 17));
        cmd17.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd17.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp17 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp17.Value = DBNull.Value;
        cmd17.Parameters.Add(sp17);
        WriteLog("Commands 17 Processed.");

        //---------------------------------------------
        SqlCommand cmd18 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd18.CommandTimeout = 300;
        cmd18.CommandType = CommandType.StoredProcedure;
        cmd18.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd18.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd18.Parameters.Add(new SqlParameter("@Year", Year));
        cmd18.Parameters.Add(new SqlParameter("@Month", Month));
        cmd18.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd18.Parameters.Add(new SqlParameter("@ReportType", 18));
        cmd18.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd18.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp18 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp18.Value = DBNull.Value;
        cmd18.Parameters.Add(sp18);
        WriteLog("Commands 18 Processed.");
        //--------------------------------------------- 
        SqlCommand cmd19 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd19.CommandTimeout = 300;
        cmd19.CommandType = CommandType.StoredProcedure;
        cmd19.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd19.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd19.Parameters.Add(new SqlParameter("@Year", Year));
        cmd19.Parameters.Add(new SqlParameter("@Month", Month));
        cmd19.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd19.Parameters.Add(new SqlParameter("@ReportType", 19));
        cmd19.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd19.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp19 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp19.Value = DBNull.Value;
        cmd19.Parameters.Add(sp19);
        WriteLog("Commands 19 Processed.");
        //--------------------------------------------- 

        SqlCommand cmd20 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd20.CommandTimeout = 300;
        cmd20.CommandType = CommandType.StoredProcedure;
        cmd20.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd20.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd20.Parameters.Add(new SqlParameter("@Year", Year));
        cmd20.Parameters.Add(new SqlParameter("@Month", Month));
        cmd20.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd20.Parameters.Add(new SqlParameter("@ReportType", 20));
        cmd20.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd20.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp20 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp20.Value = DBNull.Value;
        cmd20.Parameters.Add(sp20);
        WriteLog("Commands 20 Processed.");
        //--------------------------------------------- 

        SqlCommand cmd21 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd21.CommandTimeout = 300;
        cmd21.CommandType = CommandType.StoredProcedure;
        cmd21.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd21.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd21.Parameters.Add(new SqlParameter("@Year", Year));
        cmd21.Parameters.Add(new SqlParameter("@Month", Month));
        cmd21.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd21.Parameters.Add(new SqlParameter("@ReportType", 21));
        cmd21.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd21.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp21 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp21.Value = DBNull.Value;
        cmd21.Parameters.Add(sp21);
        WriteLog("Commands 21 Processed.");
        //--------------------------------------------- Yearly GL Report

        SqlCommand cmd22 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd22.CommandTimeout = 300;
        cmd22.CommandType = CommandType.StoredProcedure;
        cmd22.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd22.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd22.Parameters.Add(new SqlParameter("@Year", Year));
        cmd22.Parameters.Add(new SqlParameter("@Month", Month));
        cmd22.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd22.Parameters.Add(new SqlParameter("@ReportType", 22));
        cmd22.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd22.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp22 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp22.Value = DBNull.Value;
        cmd22.Parameters.Add(sp22);
        WriteLog("Commands 22 Processed.");
        //--------------------------------------------- 

        SqlCommand cmd23 = new SqlCommand("dbo.ExportReport", con, trans);
        cmd23.CommandTimeout = 300;
        cmd23.CommandType = CommandType.StoredProcedure;
        cmd23.Parameters.Add(new SqlParameter("@CoCode", CoCode));
        cmd23.Parameters.Add(new SqlParameter("@CoName", CoName));
        cmd23.Parameters.Add(new SqlParameter("@Year", Year));
        cmd23.Parameters.Add(new SqlParameter("@Month", Month));
        cmd23.Parameters.Add(new SqlParameter("@MonthName", MonthName));
        cmd23.Parameters.Add(new SqlParameter("@ReportType", 23));
        cmd23.Parameters.Add(new SqlParameter("@UserName", Session["FullName"].ToString()));
        cmd23.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear));
        SqlParameter sp23 = new SqlParameter("@PdfFile", SqlDbType.Image);
        sp23.Value = DBNull.Value;
        cmd23.Parameters.Add(sp23);
        WriteLog("Commands 23 Processed.");

        WriteLog("All commands ready to execute.");
        try
        {
            WriteLog("DB 'Blank Comment' Processed.");
            cmd_InsertBlankComments.ExecuteNonQuery(); // INSERTING BLANK COMMENTS TO ALL SHIPS.

            WriteLog("DB Command5 Processed.");
            cmd5.ExecuteNonQuery(); // SOA BY VESSEL 
            WriteLog("DB Command3 Processed.");
            cmd3.ExecuteNonQuery(); // SOA BY FLEET

            WriteLog("DB Command15 Processed.");
            cmd15.ExecuteNonQuery(); // YTD SOA BY VESSEL 
            WriteLog("DB Command13 Processed.");
            cmd13.ExecuteNonQuery(); // YTD SOA BY FLEET

            WriteLog("DB Command4 Processed.");
            cmd4.ExecuteNonQuery(); // DETAIL 

            WriteLog("DB Command8 Processed.");
            cmd8.ExecuteNonQuery(); // ACCRUAL DATA

            //WriteLog("DB Command7 Processed.");
            //cmd7.ExecuteNonQuery(); // VARIANCE COMMENTS

            WriteLog("DB Command7 Processed.");
            cmd7.ExecuteNonQuery(); // VARIANCE COMMENTS

            WriteLog("DB Command2 Processed.");
            cmd2.ExecuteNonQuery(); // BGT SUMMARY
            WriteLog("DB Command1 Processed.");
            cmd1.ExecuteNonQuery(); // BGT SUMMARY - VSL
            WriteLog("DB Command6 Processed.");
            cmd6.ExecuteNonQuery(); // BGT SUMMARY - FLEET

            WriteLog("DB Command9 Processed.");
            cmd9.ExecuteNonQuery(); // PUBLISHET PO COMMITMENT ( CURRENT MONTH ) - NEW REPORT

            WriteLog("DB Command16 Processed.");
            cmd16.ExecuteNonQuery(); // EXCEL

            WriteLog("DB Command17 Processed."); // Monthly GL Report
            cmd17.ExecuteNonQuery(); // PDF

            WriteLog("DB Command18 Processed."); // Excel Invoice Booked Report 
            cmd18.ExecuteNonQuery(); // EXCEL

            WriteLog("DB Command19 Processed."); // Excel Accrual Report 
            cmd19.ExecuteNonQuery(); // EXCEL

            WriteLog("DB Command20 Processed."); // Excel Monthly GL Report 
            cmd20.ExecuteNonQuery(); // EXCEL

            WriteLog("DB Command21 Processed."); // PY YTD Accrual Report
            cmd21.ExecuteNonQuery(); // EXCEL

            WriteLog("DB Command22 Processed."); // Yearly GL Report
            cmd22.ExecuteNonQuery(); // PDF

            WriteLog("DB Command23 Processed."); // Excel Yearly GL Report
            cmd23.ExecuteNonQuery(); // PDF

            WriteLog("DB Commands Processed All.");
            //--------------------------------- DELETING THE REPORT FILES ------------------------
            string[] Files = Directory.GetFiles(Server.MapPath("~/Modules/OPEX/Publish/"));
            for (int i = 0; i < Files.Length - 1; i++)
            {
                File.Delete(Files[i]);
            }
            WriteLog("Report Files Deleted.");
            //--------------------------------- UPDATING THE REPORT FILES ----------------------

            trans.Commit();
            trans = con.BeginTransaction(); 
            SqlCommand cmd = new SqlCommand("select REPORTID from dbo.tblOwnerReportsV2 where fldreportcoCOde='" + CoCode + "' AND FLDREPORTYEAR=" + Year + " AND FLDREPORTMONTH=" + Month + " AND CurFinYear = '"+CurFinYear+"' order by reportid", con,trans);
            cmd.CommandTimeout = 180;
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = cmd;
            DataTable dtReports = new DataTable();
            adp.Fill(dtReports);
          //  int[] Sequence = { 5, 3, 15, 13, 4, 8, 7, 2, 1, 6 };
            int[] Sequence = { 5, 3, 15, 13, 4, 8, 7, 2, 1, 6, 16, 17, 18, 19, 20, 21, 22, 23};
            for (int i = 0; i <= dtReports.Rows.Count - 1; i++)
            {
                SqlCommand cmdUpd = new SqlCommand("ExportReport_PART2", con,trans);
                cmdUpd.CommandType = CommandType.StoredProcedure;
                cmdUpd.CommandTimeout = 300;
                cmdUpd.Parameters.Add(new SqlParameter("@REPORTID", dtReports.Rows[i][0].ToString()));
                cmdUpd.Parameters.Add(new SqlParameter("@COCODE", CoCode));
                cmdUpd.Parameters.Add(new SqlParameter("@YEAR", Year));
                cmdUpd.Parameters.Add(new SqlParameter("@MONTH", Month));
                cmdUpd.Parameters.Add(new SqlParameter("@PUBLISHEDBY", Session["FullName"].ToString()));
                cmdUpd.Parameters.Add(new SqlParameter("@PUBLISHEDON", DateTime.Today.ToString("dd-MMM-yyyy")));
                cmdUpd.Parameters.Add(new SqlParameter("@CurFinYear", CurFinYear.ToString()));
                cmdUpd.Parameters.Add(new SqlParameter("@PdfFile", getFile(Sequence[i], Month, Year, CoCode, CoName, CurFinYear.ToString())));
                cmdUpd.ExecuteNonQuery(); // SOA BY VESSEL 


            }
            trans.Commit();
            WriteLog("Report Files Updated.");
            // ------------------------------------------------------------------------------------
            lblMessage.Text = "Published Successfully.";
            BindList();
            WriteLog("BindList called.");
            ShowNextPublish();
            WriteLog("ShowNextPublish called.");
            // ------------------------------------------------------------------------------------
        }
        catch (Exception ex)
        {
            trans.Rollback();
            lblMessage.Text = "Unable to publish. Error : " + ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
    protected void CloseNow(int Month,int Year, string CurFinYear)
    {
        string CoCode;
        CoCode = ddlCompany.SelectedValue;
        try
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.tblperiodmaint SET PERCLOSED=1 where cocode='" + CoCode + "' AND RPTYEAR=" + Year.ToString() + " AND RPTPERIOD=" + Month.ToString() + " AND CurFinYear = '"+ CurFinYear + "';Update dbo.tblusers set DefaultYear=" + Year.ToString() + ",DefaultQtr=" + Month.ToString() + " where DefaultCo='" + CoCode + "'");
            BindList();
            ShowNextPublish();
            lblMessage.Text = "Period closed successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Unable to Close. Error : " + ex.Message;
        } 
    }
    protected void btnCloseReport_Click(object sender, EventArgs e)
    {
        //int Year, Month;
        //Year = Common.CastAsInt32(ViewState["PYear"]);
        //Month = Common.CastAsInt32(ViewState["PMonth"]);
        //CloseNow(Month, Year);
    }
    // Function -------------------------------------------------------------
    protected byte[] getFile(int ReportType,int Month,int Year,string CoCode,string CoName, string CurFinYear)

    {
        string FileName = "";
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        if (ReportType == 1) // VARIANCE REPORT - BUDGET SUMMARY - BY VSL
        {
            DataSet Ds;
            Common.Set_Procedures("POS_ExportVarianceReport_Vessel");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@COMPCODE", CoCode),
               new MyParameter("@MNTH", Month),
               new MyParameter("@YR", CurFinYear),
               new MyParameter("@VSLCODE", "")
               );
            Ds = Common.Execute_Procedures_Select();
            Ds.Tables[0].TableName = "getVarianceRepportStructure;1";
            //string totalBudgetDays = "0";
            //string yearBudgetDays = "0";
            string monthBudgetDays = "0";

            //string sql = "SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear = '" + CurFinYear + "' AND VDAYS.CoCode = '" + CoCode + "' ORDER BY VDAYS.YEARDAYS DESC ";
            //DataTable dtdays = Common.Execute_Procedures_Select_ByQuery(sql);
            //if (dtdays.Rows.Count > 0)
            //{
            //    totalBudgetDays = dtdays.Rows[0]["YEARDAYS"].ToString();
            //}
            //if (CurFinYear != "")
            //{
            //    DataTable dtBudgetdays = Opex.GetYearofDaysfromBudget(Month, CurFinYear, CoCode, "");
            //    if (dtBudgetdays.Rows.Count > 0)
            //    {
            //        yearBudgetDays = dtBudgetdays.Rows[0]["Days"].ToString();
            //    }
            //}
            int MonthDays = 0;
            if (CurFinYear != "")
            {
                MonthDays = Opex.GetNodays(CurFinYear, Month);
            }
            monthBudgetDays = MonthDays.ToString();
            string TargetUtilisation = "";
            if (IsIndianFinacialYear)
            {
                int period = Convert.ToInt32(Month);
                int USFinMonth = Opex.GetUSFinMonth(period);
                TargetUtilisation = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(USFinMonth) / 12) * 100), 0) + " %";
            }
            else
            {
                TargetUtilisation = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(Month) / 12) * 100), 0) + " %";
            }
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/Export_VarianceReportBudgetSummary.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            rpt.SetParameterValue("MonthDays", MonthDays);
            //rpt.SetParameterValue("YearDays", yearBudgetDays);
            //rpt.SetParameterValue("BudgetDays", totalBudgetDays);
            rpt.SetParameterValue("TargetUtilization", TargetUtilisation);
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/VR_BUDGET_SUMMARY_BYVESSEL.pdf"));
            FileName = "~/Modules/OPEX/Publish/VR_BUDGET_SUMMARY_BYVESSEL.pdf";
        }
        if (ReportType == 2)  // VARIANCE REPORT - ACCOUNT DETAILS
        {
            DataSet Ds;
            Common.Set_Procedures("POS_ExportVarianceReport_Account");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@COMPCODE ", CoCode),
               new MyParameter("@MNTH ", Month),
               new MyParameter("@YR ", CurFinYear),
               new MyParameter("@VSLCODE ", "")
               );
            Ds = Common.Execute_Procedures_Select();
            //string totalBudgetDays = "0";
            //string yearBudgetDays = "0";
            string monthBudgetDays = "0";

            //string sql = "SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear = '" + CurFinYear + "' AND VDAYS.CoCode = '" + CoCode + "' ORDER BY VDAYS.YEARDAYS DESC ";
            //DataTable dtdays = Common.Execute_Procedures_Select_ByQuery(sql);
            //if (dtdays.Rows.Count > 0)
            //{
            //    totalBudgetDays = dtdays.Rows[0]["YEARDAYS"].ToString();
            //}
            //if (CurFinYear != "")
            //{
            //    DataTable dtBudgetdays = Opex.GetYearofDaysfromBudget(Month, CurFinYear, CoCode, "");
            //    if (dtBudgetdays.Rows.Count > 0)
            //    {
            //        yearBudgetDays = dtBudgetdays.Rows[0]["Days"].ToString();
            //    }
            //}
            int MonthDays = 0;
            if (CurFinYear != "")
            {
                MonthDays = Opex.GetNodays(CurFinYear, Month);
            }
            monthBudgetDays = MonthDays.ToString();
            string TargetUtilisation = "";
            if (IsIndianFinacialYear)
            {
                int period = Convert.ToInt32(Month);
                int USFinMonth = Opex.GetUSFinMonth(period);
                TargetUtilisation = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(USFinMonth) / 12) * 100), 0) + " %";
            }
            else
            {
                TargetUtilisation = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(Month) / 12) * 100), 0) + " %";
            }
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/Export_VarianceReportAccountDetails.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + CurFinYear.ToString());
            rpt.SetParameterValue("MonthDays", MonthDays);
            //rpt.SetParameterValue("YearDays", yearBudgetDays);
            //rpt.SetParameterValue("BudgetDays", totalBudgetDays);
            rpt.SetParameterValue("TargetUtilization", TargetUtilisation);
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/VR_BUDGET_SUMMARY_BYACCOUNT.pdf"));
            FileName = "~/Modules/OPEX/Publish/VR_BUDGET_SUMMARY_BYACCOUNT.pdf";
        }
        if (ReportType == 3) // STATEMENT OF ACCOUNTS - FLEET TOTALS
        {
            DataSet Ds;
            Common.Set_Procedures("GetSoaMonthReport");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
               new MyParameter("@Company ", CoCode),
               new MyParameter("@ShipID ", "0"),
               new MyParameter("@Year ", Year),
               new MyParameter("@Period ", Month),
               new MyParameter("@ReportType ", "2"),
               new MyParameter("@CurFinYear ", CurFinYear)
               );
            Ds= Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/SOAReportMonthly.rpt"));
            Ds.Tables[0].TableName = "GetSoaMonthReport;1";
            rpt.SetDataSource(Ds);
            rpt.SetParameterValue("Statement", "STATEMENT OF ACCOUNT FOR " + getStatement(Month, Year));
            rpt.SetParameterValue("DateStr", getRPTDate(Month, Year));
            rpt.SetParameterValue("DatePrevMonth", getRPTPrevDate(Month, Year));
           // rpt.SetParameterValue("OpenCommMonth", Common.CastAsDecimal(Ds.Tables[0].Rows[0]["OpenCommMonth"]));
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/ACCOUNT_STATEMENT_FLEETTOTAL.pdf"));
            FileName = "~/Modules/OPEX/Publish/ACCOUNT_STATEMENT_FLEETTOTAL.pdf";
        }
        if (ReportType == 13) // YTD STATEMENT OF ACCOUNTS - FLEET TOTALS
        {
            DataSet Ds;
            Common.Set_Procedures("GetSoaMonthReport");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
               new MyParameter("@Company ", CoCode),
               new MyParameter("@ShipID ", "0"),
               new MyParameter("@Year ", Year),
               new MyParameter("@Period ", Month),
               new MyParameter("@ReportType ", "2"),
               new MyParameter("@CurFinYear ", CurFinYear)
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/SOAReportYTD_Publish.rpt"));
            Ds.Tables[0].TableName = "GetSoaMonthReport;1";
            rpt.SetDataSource(Ds);
            rpt.SetParameterValue("Statement", "YTD STATEMENT OF ACCOUNT FOR " + getStatement(Month, Year));
            rpt.SetParameterValue("DateStr", getRPTDate(Month, Year));
            rpt.SetParameterValue("DatePrevMonth", getRPTPrevDate(Month, Year));
            rpt.SetParameterValue("OpenCommMonth", Common.CastAsDecimal(Ds.Tables[0].Rows[0]["OpenCommMonth"]));
            var monthStart = DateTime.Now;
            if (IsIndianFinacialYear)
            {
                monthStart = new DateTime(Year, 3, 1);
            }
            else
            {
                monthStart = new DateTime(Year, 1, 1);
            }
            rpt.SetParameterValue("MonthStartDt", monthStart.ToString("MMM dd, yyyy"));
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/YTD_ACCOUNT_STATEMENT_FLEETTOTAL.pdf"));
            FileName = "~/Modules/OPEX/Publish/YTD_ACCOUNT_STATEMENT_FLEETTOTAL.pdf";
        }
        if (ReportType == 4) // DETAIL ACTIVITY REPORT
        {
            DataSet Ds;
            Common.Set_Procedures("DBO.POS_DetailActivityReport_Vessel");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
               new MyParameter("@cocode ", CoCode),
               new MyParameter("@Month ", Month),
               new MyParameter("@Year ", CurFinYear));
            Ds= Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/ActivityReport.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("MonYear", getStatement(Month, Year));
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/InvoiceBookingREPORT.pdf"));
            FileName = "~/Modules/OPEX/Publish/InvoiceBookingREPORT.pdf";
        }
        if (ReportType == 5) // STATEMENT OF ACCOUTS - BY VESSEL
        {
            DataSet Ds;
            Common.Set_Procedures("GetSoaMonthReport");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
               new MyParameter("@Company ", CoCode),
               new MyParameter("@ShipID ", "0"),
               new MyParameter("@Year ", Year),
               new MyParameter("@Period ", Month),
               new MyParameter("@ReportType ", "3"),
               new MyParameter("@CurFinYear ", CurFinYear)
               );
            Ds= Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/SOAReportMonthly.rpt"));
            Ds.Tables[0].TableName = "GetSoaMonthReport;1";
            rpt.SetDataSource(Ds);
            rpt.SetParameterValue("Statement", "STATEMENT OF ACCOUNT FOR " + getStatement(Month, Year));
            rpt.SetParameterValue("DateStr", getRPTDate(Month, Year));
            rpt.SetParameterValue("DatePrevMonth", getRPTPrevDate(Month, Year));
            //rpt.SetParameterValue("OpenCommMonth", Common.CastAsDecimal(Ds.Tables[0].Rows[0]["OpenCommMonth"]));
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/ACCOUNT_STATEMENT_BYVESSEL.pdf"));
            FileName = "~/Modules/OPEX/Publish/ACCOUNT_STATEMENT_BYVESSEL.pdf";
        }
        if (ReportType == 15) // YTD STATEMENT OF ACCOUTS - BY VESSEL
        {
        
            DataSet Ds;
            Common.Set_Procedures("GetSoaMonthReport");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
               new MyParameter("@Company ", CoCode),
               new MyParameter("@ShipID ", "0"),
               new MyParameter("@Year ", Year),
               new MyParameter("@Period ", Month),
               new MyParameter("@ReportType ", "1"),
               new MyParameter("@CurFinYear ", CurFinYear)
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/SOAReportYTD_Publish.rpt"));
            Ds.Tables[0].TableName = "GetSoaMonthReport;1";
            rpt.SetDataSource(Ds);
            rpt.SetParameterValue("Statement", "YTD STATEMENT OF ACCOUNT FOR " + getStatement(Month, Year));
            rpt.SetParameterValue("DateStr", getRPTDate(Month, Year));
            rpt.SetParameterValue("DatePrevMonth", getRPTPrevDate(Month, Year));
            rpt.SetParameterValue("OpenCommMonth", Common.CastAsDecimal(Ds.Tables[0].Rows[0]["OpenCommMonth"]));
            //IsIndianFinacialYear = GetIndianFinacialYear();
            var monthStart = DateTime.Now; 
            if (IsIndianFinacialYear)
            {
                monthStart = new DateTime(Year, 3, 1);
                
            }
            else
            {
                monthStart = new DateTime(Year, 1, 1);
            }
            rpt.SetParameterValue("MonthStartDt", monthStart.ToString("MMM dd, yyyy"));
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/YTD_ACCOUNT_STATEMENT_BYVESSEL.pdf"));
            FileName = "~/Modules/OPEX/Publish/YTD_ACCOUNT_STATEMENT_BYVESSEL.pdf";
        }
        if (ReportType == 6) // VARIANCE REPORT - BUDGET SUMMARY - BY FLEET
        {
            DataSet Ds;
            Common.Set_Procedures("POS_ExportVarianceReport_Vessel");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@COMPCODE ", CoCode),
               new MyParameter("@MNTH ", Month),
               new MyParameter("@YR ", CurFinYear),
               new MyParameter("@VSLCODE ", "ALL")
               );
            Ds = Common.Execute_Procedures_Select();
            //string totalBudgetDays = "0";
            //string yearBudgetDays = "0";
            //string monthBudgetDays = "0";

            //string sql = "SELECT TOP 1 VDAYS.YEARDAYS FROM [dbo].tblSMDBudgetForecastYear as VDAYS WHERE VDAYS.CurFinYear = '" + CurFinYear + "' AND VDAYS.CoCode = '" + CoCode + "' ORDER BY VDAYS.YEARDAYS DESC ";
            //DataTable dtdays = Common.Execute_Procedures_Select_ByQuery(sql);
            //if (dtdays.Rows.Count > 0)
            //{
            //    totalBudgetDays = dtdays.Rows[0]["YEARDAYS"].ToString();
            //}
            //if (CurFinYear != "")
            //{
            //    DataTable dtBudgetdays = Opex.GetYearofDaysfromBudget(Month, CurFinYear, CoCode, "ALL");
            //    if (dtBudgetdays.Rows.Count > 0)
            //    {
            //        yearBudgetDays = dtBudgetdays.Rows[0]["Days"].ToString();
            //    }
            //}
            //int MonthDays = 0;
            //if (CurFinYear != "")
            //{
            //    MonthDays = Opex.GetNodays(CurFinYear, Month);
            //}
            //monthBudgetDays = MonthDays.ToString();
            string TargetUtilisation = "";
            if (IsIndianFinacialYear)
            {
                int period = Convert.ToInt32(Month);
                int USFinMonth = Opex.GetUSFinMonth(period);
                TargetUtilisation = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(USFinMonth) / 12) * 100), 0) + " %";
            }
            else
            {
                TargetUtilisation = "Target Utilisation = " + Math.Round(((Common.CastAsDecimal(Month) / 12) * 100), 0) + " %";
            }
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/Export_VarianceReportBudgetSummaryFleet.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            //rpt.SetParameterValue("MonthDays", MonthDays);
            //rpt.SetParameterValue("YearDays", yearBudgetDays);
            //rpt.SetParameterValue("BudgetDays", totalBudgetDays);
            rpt.SetParameterValue("TargetUtilization", TargetUtilisation);
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/VR_BUDGET_SUMMARY_BYFLEET.pdf"));
            FileName = "~/Modules/OPEX/Publish/VR_BUDGET_SUMMARY_BYFLEET.pdf";
        }
        if (ReportType == 7) // EXPORT ACCURAL DATA - BY MONTH - ALL VESSELS
        {
            DataSet Ds;
            Common.Set_Procedures("SP_GET_tblExpenseAccrual");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@Cocode", CoCode),
               new MyParameter("@glPer", Month),
               new MyParameter("@glYear", CurFinYear),
               new MyParameter("@SHIPID", "ALL")
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/VariancePOReportYTD1.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "YTD ACCRUAL EXPENSES UPTO " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/YTD_ACCRUAL_EXPENSES.pdf"));
            FileName = "~/Modules/OPEX/Publish/YTD_ACCRUAL_EXPENSES.pdf";
        }
        if (ReportType == 8) // EXPORT ACCURAL DATA - BY MONTH - ALL VESSELS
        {
            DataSet Ds;
            Common.Set_Procedures("Get_tblACCRUALDATA");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@Cocode", CoCode),
               new MyParameter("@Month", Month),
               new MyParameter("@CurFyYear", CurFinYear),
               new MyParameter("@VSLCODE", "ALL")
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/ExportAccrualData.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "ACCRUAL EXPENSES FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/Accrual_Expenses.pdf"));
            FileName = "~/Modules/OPEX/Publish/Accrual_Expenses.pdf";
        }
        if (ReportType == 16) // EXCEL WORK SHEET VARIANCE DATA - BY ACCOUNT- ALL VESSELS
        {
            DataSet Ds;
            Common.Set_Procedures("POS_ExportVarianceReport_Account");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@COMPCODE ", CoCode),
               new MyParameter("@MNTH ", Month),
               new MyParameter("@YR ", CurFinYear),
               new MyParameter("@VSLCODE ", "EXL")
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/ExportAccountoExcel.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, Server.MapPath("~/Modules/OPEX/Publish/VR_EXECL_BYACCOUNT.xls"));
            FileName = "~/Modules/OPEX/Publish/VR_EXECL_BYACCOUNT.xls";
        }
        if (ReportType == 17) // MONTHLY GL DATA - BY MONTH - ALL VESSELS
        {
            DataSet Ds;
            Common.Set_Procedures("Get_MonthlyGLDATA");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@Cocode", CoCode),
               new MyParameter("@Month", Month),
               new MyParameter("@CurFyYear", CurFinYear),
               new MyParameter("@VSLCODE", "ALL")
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/ExportGLReport.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "MONTHLY GENERAL LEDGER FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/MonthlyGLReport.pdf"));
            FileName = "~/Modules/OPEX/Publish/MonthlyGLReport.pdf";
        }
        if (ReportType == 18) // EXCEL Invoice Booked Report for Month
        {
            DataSet Ds;
            Common.Set_Procedures("POS_DetailActivityReport_Vessel");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
               new MyParameter("@cocode", CoCode),
               new MyParameter("@Month", Month),
               new MyParameter("@Year", CurFinYear)
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/InvoiceBookedReportExcel.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Server.MapPath("~/Modules/OPEX/Publish/InvoiceBookedReportEXECL.xls"));
            FileName = "~/Modules/OPEX/Publish/InvoiceBookedReportEXECL.xls";
        }
        if (ReportType == 19) // EXCEL WORK SHEET VARIANCE DATA - BY ACCOUNT- ALL VESSELS
        {
            DataSet Ds;
            Common.Set_Procedures("Get_tblACCRUALDATA");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@Cocode", CoCode),
               new MyParameter("@Month", Month),
               new MyParameter("@CurFyYear", CurFinYear),
               new MyParameter("@VSLCODE", "ALL")
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/AccrualExpensesExcel.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "ACCRUAL EXPENSES FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Server.MapPath("~/Modules/OPEX/Publish/AccrualExpensesExcel.xls"));
            FileName = "~/Modules/OPEX/Publish/AccrualExpensesExcel.xls";
        }
        if (ReportType == 20) // EXCEL Mothly GL Report
        {
            DataSet Ds;
            Common.Set_Procedures("Get_MonthlyGLDATA");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@Cocode", CoCode),
               new MyParameter("@Month", Month),
               new MyParameter("@CurFyYear", CurFinYear),
               new MyParameter("@VSLCODE", "ALL")
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/MothlyGLReportExcel.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "MONTHLY GENERAL LEDGER FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, Server.MapPath("~/Modules/OPEX/Publish/MothlyGLReportExcel.xls"));
            FileName = "~/Modules/OPEX/Publish/MothlyGLReportExcel.xls";
        }
        if (ReportType == 21) // EXPORT ACCURAL DATA - BY MONTH - ALL VESSELS
        {
            DataSet Ds;
            Common.Set_Procedures("SP_GET_tblExpenseAccrualPY");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@Cocode", CoCode),
               new MyParameter("@glPer", Month),
               new MyParameter("@glYear", CurFinYear),
               new MyParameter("@SHIPID", "ALL")
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/VariancePOReportPYYTD1.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "PY YTD ACCRUAL EXPENSES UPTO " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/PY_YTD_ACCRUAL_EXPENSES.pdf"));
            FileName = "~/Modules/OPEX/Publish/PY_YTD_ACCRUAL_EXPENSES.pdf";
        }

        if (ReportType == 22) // Yearly GL DATA - BY Year - ALL VESSELS
        {
            DataSet Ds;
            Common.Set_Procedures("Get_YearlyGLDATA");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@Cocode", CoCode),
               new MyParameter("@Month", Month),
               new MyParameter("@CurFyYear", CurFinYear),
               new MyParameter("@VSLCODE", "ALL")
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/ExportGLReportYTD.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "YTD GENERAL LEDGER UPTO " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/YearlyGLReport.pdf"));
            FileName = "~/Modules/OPEX/Publish/YearlyGLReport.pdf";
        }

        if (ReportType == 23) // EXCEL Mothly GL Report
        {
            DataSet Ds;
            Common.Set_Procedures("Get_YearlyGLDATA");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
               new MyParameter("@Cocode", CoCode),
               new MyParameter("@Month", Month),
               new MyParameter("@CurFyYear", CurFinYear),
               new MyParameter("@VSLCODE", "ALL")
               );
            Ds = Common.Execute_Procedures_Select();
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/MothlyGLReportExcel.rpt"));
            rpt.SetDataSource(Ds.Tables[0]);
            rpt.SetParameterValue("Header", "YTD GENERAL LEDGER UPTO " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, Server.MapPath("~/Modules/OPEX/Publish/YearlyGLReportExcel.xls"));
            FileName = "~/Modules/OPEX/Publish/YearlyGLReportExcel.xls";
        }
        //if (ReportType == 7) // VARIANCE COMMENTS - SHIP MANAGER COMMENTS - BY FLEET
        //{
        //    DataSet Ds;
        //    DataTable DTinner;
        //    string[] comments;
        //    string ModString = "";
        //    //string sql = "select (SELECT SHIPNAME FROM [dbo].sql_tblSMDPRVessels MM WHERE MM.SHIPID=commshipid) as ShipName,CommPer,CommMidID,(select majorcat from [dbo].tblAccountsMajor where majcatid=6) as ComMajName, " +
        //    //            "(select midcat from [dbo].tblAccountsMid where midcatid=tblBudgetLevelComments.CommMidID) as ComMidName, " +
        //    //            "Budget,Actual+Comm as Consumed,Comment  " +
        //    //            "from [dbo].tblBudgetLevelComments " +
        //    //            "where CommYear=" + Year.ToString() + " and CommCo='" + CoCode + "' and commPer=" + Month + " and CommMajID=6 order by (select midseqno from [dbo].tblAccountsMid where midcatid=tblBudgetLevelComments.CommMidID)";

        //    //   CHANGE IN SQL BECAUSE IT WAS SHOWING RECORD FOR MAJCATID=6 ONLY

        //    string sql = "select (SELECT VesselName As Shipname FROM Vessel MM with(nolock) WHERE MM.VesselCode=commshipid) as ShipName,CommPer,CommMidID,(select majorcat from [dbo].tblAccountsMajor where majcatid=tblBudgetLevelComments.CommMajID) as ComMajName, " +
        //                "(select midcat from [dbo].tblAccountsMid where midcatid=tblBudgetLevelComments.CommMidID) as ComMidName, " +
        //                "Budget,Actual+Comm as Consumed,Comment  " +
        //                "from [dbo].tblBudgetLevelComments " +
        //                "where CommYear=" + Year.ToString() + " and CommCo='" + CoCode + "' and commPer=" + Month + " AND CurFinYear='" + CurFinYear + "'  order by (select midseqno from [dbo].tblAccountsMid where midcatid=tblBudgetLevelComments.CommMidID)";

        //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        //    DataTable dt1 = new DataTable();
        //    dt1.Columns.Add(new DataColumn("PlainText", typeof(String)));
        //    dt1.Columns.Add(new DataColumn("RTFText", typeof(String)));
        //    dt1.Columns.Add(new DataColumn("HTMLText", typeof(String)));
        //    dt1.Rows.Add(dt1.NewRow());
        //    int MidCat = 0;
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                ModString = "";
        //                MidCat = Common.CastAsInt32(dr["CommMidID"]);
        //                comments = dr["Comment"].ToString().Split('`');
        //                DTinner = Common.Execute_Procedures_Select_ByQuery("SELECT distinct (select minorcat from VW_tblAccountsMinor b where b.mincatid=a.mincatid) as ChildAccounts FROM vw_sql_tblSMDPRAccounts a WHERE MidCatID =" + MidCat + "");

        //                for (int i = 0; i < comments.Length; i++)
        //                {
        //                    if (i == 0)
        //                    {
        //                        //comments[i] = "<br><b>General Comments </b><br>" + comments[i] + "<br>";
        //                        comments[i] = "</br><b>General Comments </b></br>" + comments[i].Replace("\n", "</br>") + "</br>";
        //                    }
        //                    else
        //                    {
        //                        if (comments[i].Trim() != "")
        //                        {
        //                            //comments[i] = "<br> <br><b>" + DTinner.Rows[i - 1][0].ToString() + "</b><br>" + comments[i];
        //                            comments[i] = "</br> </br><b>" + DTinner.Rows[i - 1][0].ToString() + "</b></br>" + comments[i].Replace("\n", "</br>");
        //                        }
        //                    }

        //                }
        //                for (int i = 0; i < comments.Length; i++)
        //                {
        //                    ModString = ModString + comments[i].ToString();
        //                }
        //                //dr["Comment"]="<STRONG>This </STRONG><U><FONT color='#ff3333'>is </FONT></U><EM>HTML </EM><FONT color='#3366cc' size='6'>Text!!!! </FONT>";
        //                dt1.Rows[0][0] = "<html><strong>EMANAGER</strong></html>";
        //                dt1.Rows[0][1] = "<html><strong>EMANAGER</strong></html>";
        //                dt1.Rows[0][2] = "<html><strong>EMANAGER</strong></html>";
        //                dr["Comment"] = ModString;

        //            }
        //        }
        //    }

        //    rpt.Load(Server.MapPath("~/Modules/OPEX/Report/BudgetCommentReport.rpt"));
        //    dt.TableName = "Newreport;1";
        //    rpt.SetDataSource(dt);
        //    rpt.SetParameterValue("PYear", Year.ToString());
        //    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/VR_COMMENTS.pdf"));
        //    FileName = "~/Modules/OPEX/Publish/VR_COMMENTS.pdf";
        //}
        //if (ReportType == 7) // EXPORT ACCURAL DATA - BY MONTH - ALL VESSELS
        //{
        //    DataSet Ds;
        //    Common.Set_Procedures("Get_tblACCRUALDATA");
        //    Common.Set_ParameterLength(4);
        //    Common.Set_Parameters(
        //       new MyParameter("@Cocode", CoCode),
        //       new MyParameter("@Month", Month),
        //       new MyParameter("@CurFyYear", CurFinYear),
        //       new MyParameter("@VSLCODE", "ALL")
        //       );
        //    Ds = Common.Execute_Procedures_Select();
        //    rpt.Load(Server.MapPath("~/Modules/OPEX/Report/ExportAccrualData.rpt"));
        //    rpt.SetDataSource(Ds.Tables[0]);
        //    rpt.SetParameterValue("Header", "TOTAL PO TRANSATIONS FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
        //    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/OPEX/Publish/TOTAL_PO_TRANSATIONS.pdf"));
        //    FileName = "~/Modules/OPEX/Publish/TOTAL_PO_TRANSATIONS.pdf";
        //}

        rpt.Close();
        rpt.Dispose();
        if (FileName.Trim() == "")
        {
            byte[] tmp = {0};
            return (tmp);
        }
        else
            return File.ReadAllBytes(Server.MapPath(FileName));
        
    }
    public void BindYear()
    {
        //ddlyear.Items.Add(new ListItem("Select","0"));
        //for (int i = System.DateTime.Now.Year,j=1; ; i--,j++)
        //{
        //    ddlyear.Items.Add(i.ToString());
        //    if (i<2010)
        //        break;
        //}
        if (ddlCompany.SelectedIndex == 0)
        {
            return;
        }
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
                ddlCompany.Items.Insert(0, new ListItem("<Select>", ""));
            }
        }
        
    }
    public string getStatement(int m, int y)
    {
        try
        {
            DateTime date = Convert.ToDateTime(m.ToString() + "/1" + "/" + y.ToString());
            string str = date.ToString("MMMM yyyy");
            return str;
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }
    public string getRPTDate(int m, int y)
    {
        try
        {
            DateTime date = Convert.ToDateTime(m.ToString() + "/1" + "/" + y.ToString());
            string str = date.ToString("MMMM yyyy");
            return str;
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }
    public string getRPTPrevDate(int m, int y)
    {
        try
        {
            DateTime date = Convert.ToDateTime(m.ToString() + "/1/" + y.ToString());
            date = date.AddDays(-1);
            string str = date.ToString("dd MMM yyyy");
            return str;
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }
    protected void btnGridPublish_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            Button _btnPublish = (Button)sender;
            MsgFinal = "";

            int Year;
            string CurFinYear = "";
            Year = Common.CastAsInt32(((Button)sender).Attributes["rptyear"].ToString());
            CurFinYear = ddlyear.SelectedValue;
            PublishNow(Common.CastAsInt32(_btnPublish.CommandArgument), Common.CastAsInt32(Year), CurFinYear);
            WriteLogTOFile();
        }
       catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
    }
    //protected void btnInformSuptd_Click(object sender, EventArgs e)
    //{
    //    //-------------------------------------------------
    //    string CoCode, CoName, MonthName;
    //    CoCode = ddlCompany.SelectedValue;
    //    CoName = ddlCompany.SelectedItem.Text;
        
    //    Button _btnPublishComm = (Button)sender;
    //    int Month = Common.CastAsInt32(_btnPublishComm.CommandArgument);
    //    // int Year = Common.CastAsInt32(ddlyear.SelectedValue);
    //    int Year;
    //    string CurFinYear = "";
    //    Year = Common.CastAsInt32(((Button)sender).Attributes["rptyear"].ToString());
    //    CurFinYear = ddlyear.SelectedValue;
    //    //--------------------------------------
    //    string str = "</br>Please be advised that vessel's budget variance for subject COMPANY is ready for writing comments</br>Please publish the comments on owner portal once all vessel's comments writing for owner is finished and advise the owners accordingly.</br></br>Kindly refer to the Statement of Accounts of the concerned vessel to know the Funding Status / Deficit- Surplus.</br></br>Thanks</br></br>" + Session["FullName"] + "</br></br>";
    //    char[] Sep = { ';' };
    //    string[] ToAdds = { "emanager@energiossolutions.com" };
    //    string[] CCAdds = { "emanager@energiossolutions.com" };

    //    //string[] ToAdds = { "pankaj.k@esoftech.com" };
    //    //string[] CCAdds = { "emanager@energiossolutions.com" };
    //    string[] BCCAdds = {};
    //    //------------------
    //    string ErrMsg = "";
    //    string Subject ="Budget Published : " + CoCode + " - " + ProjectCommon.GetMonthName(Month.ToString()) + " - " + ddlyear.SelectedValue;

    //    if (ProjectCommon.SendeMail("emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAdds, CCAdds, BCCAdds, Subject, str, out ErrMsg, ""))
    //    {
    //        Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.tbl_Publish_InformSuptd(COCODE,YEAR,MONTH,MAILSENT,SENTBY,SENDON,CurFinYear) VALUES('" + CoCode + "'," + Year + "," + Month.ToString() + ",'Y','" + Session["UserName"].ToString() + "',GETDATE(),'"+ddlyear.SelectedValue+"')");
    //        ShowNextPublish();
    //        lblMessage.Text = "Mail sent successfully.";
    //    }
    //    else
    //    {
    //        lblMessage.Text = "Unable to send Mail. Error : " + ErrMsg;
    //    }
    //}

    //protected void btnGridPublishComm_Click(object sender, EventArgs e)
    //{
    //    //-------------------------------------------------
    //    string CoCode, CoName, MonthName;
    //    CoCode = ddlCompany.SelectedValue;
    //    CoName = ddlCompany.SelectedItem.Text;

    //    Button _btnPublishComm = (Button)sender;
    //    int Month = Common.CastAsInt32(_btnPublishComm.CommandArgument);
    //    //int Year=Common.CastAsInt32(ddlyear.SelectedValue);
    //    int Year;
    //    string CurFinYear = "";
    //    Year = Common.CastAsInt32(((Button)sender).Attributes["rptyear"].ToString());
    //    CurFinYear = ddlyear.SelectedValue;
    //    int ReportId = -1;
    //    DataTable DT = Common.Execute_Procedures_Select_ByQuery("SELECT REPORTID FROM dbo.tblOwnerReportsV2 WHERE FLDREPORTCOCODE='" + CoCode + "' AND FLDREPORTYEAR=" + Year.ToString() + " AND FLDREPORTMONTH=" + Month.ToString() + " AND CurFinYear = '"+ CurFinYear + "' AND LTRIM(RTRIM(FLDREPORTSUBTITLE))='Ship Manager Comments'");
    //    if (DT.Rows.Count > 0)
    //    {
    //        ReportId = Common.CastAsInt32(DT.Rows[0][0]);  
    //    }
    //    //-------------------------------------------------
    //    if (ReportId > 0)
    //    {
            
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString() + ";Connection Timeout=300;");
    //        con.Open();
    //        SqlTransaction trans = con.BeginTransaction();

    //        SqlCommand cmd7 = new SqlCommand("dbo.ExportReport_PART2", con, trans);
    //        cmd7.CommandTimeout = 300;
    //        cmd7.CommandType = CommandType.StoredProcedure;
    //        cmd7.Parameters.Add(new SqlParameter("@REPORTID", ReportId));
    //        cmd7.Parameters.Add(new SqlParameter("@COCODE", CoCode));
    //        cmd7.Parameters.Add(new SqlParameter("@YEAR", Year));
    //        cmd7.Parameters.Add(new SqlParameter("@MONTH", Month));
    //        cmd7.Parameters.Add(new SqlParameter("@PUBLISHEDBY", Session["FullName"].ToString()));
    //        cmd7.Parameters.Add(new SqlParameter("@PUBLISHEDON", DateTime.Today.ToString("dd-MMM-yyyy")));
    //        cmd7.Parameters.Add(new SqlParameter("@PdfFile", getFile(7, Month, Year, CoCode, CoName)));
    //        //-------------------------------------------------
    //        try
    //        {
    //            //string Error = "";
    //            //if (!UpdateAmounts(CoCode,Month,Year,out Error))
    //            //{
    //            //    throw new Exception("Unable to update amounts in database.( " + Error + " )");
    //            //}
    //            cmd7.ExecuteNonQuery();
    //            trans.Commit();
    //            lblMessage.Text = "Published Successfully.";
    //            BindList();
    //            ShowNextPublish();
    //        }
    //        catch (Exception ex)
    //        {
    //            trans.Rollback();
    //            lblMessage.Text = "Unable to publish. Error : " + ex.Message;
    //        }
    //        finally
    //        {
    //            con.Close();
    //        }
    //    }
      
    //}

    //protected void btnGridPublishPOComm_Click(object sender, EventArgs e)
    //{
    //    //-------------------------------------------------
    //    string CoCode = ddlCompany.SelectedValue;
    //    Button _btnGridPublishPOComm = (Button)sender;
    //    int Month = Common.CastAsInt32(_btnGridPublishPOComm.CommandArgument);
    //    //int Year=Common.CastAsInt32(ddlyear.SelectedValue);
    //    int Year;
    //    string CurFinYear = "";
    //    Year = Common.CastAsInt32(((Button)sender).Attributes["rptyear"].ToString());
    //    CurFinYear = ddlyear.SelectedValue;

    //    //-------------------------------------------------

    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString() + ";Connection Timeout=300;");
    //        con.Open();
    //        //SqlTransaction trans = con.BeginTransaction();

    //        SqlCommand cmd7 = new SqlCommand("dbo.PUBLISH_PO_COMMITMENT", con);
    //        cmd7.CommandTimeout = 300;
    //        cmd7.CommandType = CommandType.StoredProcedure;
    //        cmd7.Parameters.Add(new SqlParameter("@CoCode", CoCode));
    //        cmd7.Parameters.Add(new SqlParameter("@Year", Year));
    //        cmd7.Parameters.Add(new SqlParameter("@Month", Month));
    //        //-------------------------------------------------
    //        try
    //        {
    //            cmd7.ExecuteNonQuery();
    //            //trans.Commit();
    //            lblMessage.Text = "PO Commitment report published successfully.";
    //            BindList();
    //            ShowNextPublish();
    //        }
    //        catch (Exception ex)
    //        {
    //            //trans.Rollback();
    //            lblMessage.Text = "Unable to publish PO Commitment. Error : " + ex.Message;
    //        }
    //        finally
    //        {
    //            con.Close();
    //        }
      
    //}
    
    protected bool UpdateAmounts(string CoCode, int Month,int Year,out string Error)
    {
        Error = "";
        bool result;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMANAGER"].ToString() + ";Connection Timeout=60;");
        con.Open();
        SqlTransaction trans = con.BeginTransaction();
        SqlCommand cmd_upd = new SqlCommand("dbo.fn_NEW_UPDATE_COMMENT_AMOUNTS", con, trans);
        cmd_upd.CommandTimeout = 300;
        cmd_upd.CommandType = CommandType.StoredProcedure;
        cmd_upd.Parameters.Add(new SqlParameter("@COMPCODE", CoCode));
        cmd_upd.Parameters.Add(new SqlParameter("@MNTH", Month));
        cmd_upd.Parameters.Add(new SqlParameter("@YR", Year));
        try
        {
            cmd_upd.ExecuteNonQuery(); 
            trans.Commit();
            result = true;
        }
        catch(Exception ex) {
            Error = ex.Message;
            trans.Rollback();
            result = false; 
        }
        finally { con.Close(); }
        return result; 
    }
    protected void btnGridClosure_Click(object sender, EventArgs e)
    {
        try
        {
            Button _btnClosure = (Button)sender;
            int Year;
            string CurFinYear = "";
            Year = Common.CastAsInt32(((Button)sender).Attributes["rptyear"].ToString());
            CurFinYear = ddlyear.SelectedValue;
            //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(*) FROM PUBLISHEDPOCOMMITMENT WHERE RYEAR=" + Year + " AND RMONTH=" + _btnClosure.CommandArgument + " AND COMPANY='" + ddlCompany.SelectedValue + "' AND CurFinYear = '" + CurFinYear + "'");
            //if (Common.CastAsInt32(dt.Rows[0][0]) > 0)
            //{
                CloseNow(Common.CastAsInt32(_btnClosure.CommandArgument), Common.CastAsInt32(Year), CurFinYear);
            //}
            //else
            //{
            //    lblMessage.Text = "Please publish PO Commitment report first.";
            //    return;
            //}
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }
       
    }

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex == 0 || ddlyear.SelectedIndex == 0)
        {
            return;
      
        }
        CreatePeriods(ddlCompany.SelectedValue, ddlyear.SelectedValue);
        //if (ddlCompany.SelectedValue.Trim() != "")
        //{
        //    if (ProjectCommon.VERIFIYPOSTINGS(ddlCompany.SelectedValue, ref Error))
        //    {
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Verifiy posting failed for apentries. Error : " + Error; 
        //        return; 
        //    }
        //}
        BindList();
        ShowNextPublish();
        GetIndianFinacialYear();
    }

    private bool GetIndianFinacialYear()
    {
        IsIndianFinacialYear = false;
        string sql = "Select IsIndianFinacialYear from AccountCompany with(nolock) where Company = '" + ddlCompany.SelectedValue + "'";
        DataTable dtFinYr = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtFinYr.Rows.Count > 0)
        {
            IsIndianFinacialYear = Convert.ToBoolean(dtFinYr.Rows[0]["IsIndianFinacialYear"]);
        }
        return IsIndianFinacialYear;
    }

    protected void btnUpdateTransaction_Click(object sender, EventArgs e)
    {
        try
        {
            //UpdateCrewManningTransaction();
            UpdateBudgetTransaction();
            lblMessage.Text = "Budget Transaction updated sucessfully.";
            UpdateVendorSOATransaction();
            lblMessage.Text = "Budget Transaction & Vendor SOA updated sucessfully.";
        }
        catch(Exception ex)
        {
            lblMessage.Text = "Error on Budget Transaction & Vendor SOA. Error : " + ex.Message.ToString();
        }
    }

    private void UpdateCrewManningTransaction()
    {
        DataSet ds = new DataSet();
        DataSet ds_Ret = new DataSet();
        string con = ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString;
        SqlConnection MyConnection = new SqlConnection(con);
        SqlCommand MyCommand = new SqlCommand();
        MyCommand.Connection = MyConnection;
        MyCommand.CommandType = CommandType.Text;
        try
        {
            MyConnection.Open();
           // AddMessage("Crew Manning Transaction updation Started.");

            MyCommand.CommandText = "sp_NewPR_UpdateBudgetDetailsforManning";
            MyCommand.CommandType = CommandType.StoredProcedure;
            MyCommand.CommandTimeout = 1000;
            MyCommand.ExecuteNonQuery();

           // AddMessage("Crew Manning Transaction updation Completed Successfully.");
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error while Crew Manning Transaction updation. Error : " + ex.Message;
        }
        finally { MyConnection.Close(); }
    }

    private void UpdateBudgetTransaction()
    {
        DataSet ds = new DataSet();
        DataSet ds_Ret = new DataSet();
        string con = ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString;
        SqlConnection MyConnection = new SqlConnection(con);
        SqlCommand MyCommand = new SqlCommand();
        MyCommand.Connection = MyConnection;
        MyCommand.CommandType = CommandType.Text;
        try
        {
            MyConnection.Open();
            //AddMessage("Budget Transaction updation Started.");

            MyCommand.CommandText = "sp_AppendBudgetAll";
            MyCommand.CommandType = CommandType.StoredProcedure;
            MyCommand.CommandTimeout = 1000;
            MyCommand.ExecuteNonQuery();

            //AddMessage("Budget Transaction updation Completed Successfully.");
        }
        catch (Exception ex)
        {
            // AddMessage("Error while Budget Transaction updation. Error : " + ex.Message);
            lblMessage.Text = "Error while Budget Transaction updation. Error : " + ex.Message;
        }
        finally { MyConnection.Close(); }
    }


    private void UpdateVendorSOATransaction()
    {
        DataSet ds = new DataSet();
        DataSet ds_Ret = new DataSet();
        string con = ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString;
        SqlConnection MyConnection = new SqlConnection(con);
        SqlCommand MyCommand = new SqlCommand();
        MyCommand.Connection = MyConnection;
        MyCommand.CommandType = CommandType.Text;
        try
        {
            MyConnection.Open();
            //AddMessage("Budget Transaction updation Started.");

            MyCommand.CommandText = "IU_POS_Invoice_VendorSOA";
            MyCommand.CommandType = CommandType.StoredProcedure;
            MyCommand.CommandTimeout = 1000;
            MyCommand.ExecuteNonQuery();

            //AddMessage("Budget Transaction updation Completed Successfully.");
        }
        catch (Exception ex)
        {
            // AddMessage("Error while Budget Transaction updation. Error : " + ex.Message);
            lblMessage.Text = "Error while Budget Transaction updation. Error : " + ex.Message;
        }
        finally { MyConnection.Close(); }
    }


}


