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
using Ionic.Zip;
using System.IO;
using System.Text;
using System.Net.Mail;
using CrystalDecisions.ReportAppServer;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using System.Collections.Generic;

public partial class LPSQE_VesselReports : System.Web.UI.Page
{
    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return ViewState["VesselCode"].ToString(); }
    }

    public int  VesselId
    {
        set { ViewState["VesselId"] = value; }
        get { return Convert.ToInt32(ViewState["VesselId"]); }
    }
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            //------------------------------------
            ProjectCommon.SessionCheck_New();
            //------------------------------------
            //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1102);
            if (chpageauth <= 0)
                Response.Redirect("blank.aspx");
            //------------------------------------------------------------------------------------------------------------

            if (Session["loginid"] == null)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
            }
            if (Session["loginid"] != null)
            {
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 6);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }
           // lblMessege.Text = "";

            if (!Page.IsPostBack)
            {
                for(int i=DateTime.Today.Year;i>=2024;i--)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
                    ddlYear1.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddlAdhocReportYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                BindVessel();
                LoadGroup();
                LoadFrequency();
               // BindFleet();
                
                //txtFromDate.Text = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString()).ToString("dd-MMM-yyyy");
                //txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");   
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void LoadFrequency()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[KPI_Period] WHERE PeriodId<=4 OR PeriodName = 'Adhoc'");
        ddlFrequency.DataSource = dt;
        ddlFrequency.DataTextField = "PeriodName";
        ddlFrequency.DataValueField = "PeriodId";
        ddlFrequency.DataBind();
    }
    protected void LoadGroup()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[m_VesselReportGroups]");
        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "GroupName";
        ddlGroup.DataValueField = "GroupId";
        ddlGroup.DataBind();
        ddlGroup.Items.Insert(0, new ListItem("<-- All -->", "0"));
    }
  
    public void BindVessel()
    {
        string WhereClause = "";
        string sql = "SELECT VesselID,Vesselname FROM DBO.Vessel v with(nolock) Where 1=1 AND v.VesselId In (Select VesselId from UserVesselRelation with(nolock) where LoginId = "+ Convert.ToInt32(Session["loginid"].ToString()) + ")";
        sql = sql + WhereClause + "ORDER BY VESSELNAME";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "Vesselname";
        ddlVessel.DataValueField = "VesselID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("< Select >", "0"));

        // ddlVessel1.DataSource = Common.Execute_Procedures_Select_ByQuery(sql); ;
        // ddlVessel1.DataTextField = "Vesselname";
        // ddlVessel1.DataValueField = "VesselID";
        // ddlVessel1.DataBind();
        // ddlVessel1.Items.Insert(0, new ListItem("<-- All -->", "0"));
        //if (dt.Rows.Count > 0)
        // {
        //     lblVessel.Text = dt.Rows[0]["Vesselname"].ToString();
        //     hdnVesselId.Value = dt.Rows[0]["VesselID"].ToString();
        //     VesselId = Convert.ToInt32(hdnVesselId.Value);
        // }
    }
    public void BindReports()
    {
        ddlReport.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM M_VESSELREPORTS WHERE REPORTID IN (SELECT D.REPORTID FROM M_VESSELREPORTS_VESSELS D WHERE D.VESSELID=" + VesselId + ") ORDER BY REPORTNAME");
        ddlReport.DataTextField = "ReportName";
        ddlReport.DataValueField = "ReportId";
        ddlReport.DataBind();
        ddlReport.Items.Insert(0, new ListItem("<-- Select -->", "0"));
    }
    protected void chk_Inactive_OnCheckedChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }

    // Added events and function
    //protected void ddlVessel1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindReports();
    //}
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        tr_Period.Visible = false;
        ddlPeriod.Items.Clear();
        ViewState["ReportFreq"] = 0;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select Frequency from [dbo].[m_VesselReports] where ReportId=" + ddlReport.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            switch (Common.CastAsInt32(dt.Rows[0][0]))
            {
                case 1:
                    ViewState["ReportFreq"] = 1;
                    tr_Period.Visible = true;
                    ddlPeriod.Items.Add(new ListItem("JAN", "1"));
                    ddlPeriod.Items.Add(new ListItem("FEB", "2"));
                    ddlPeriod.Items.Add(new ListItem("MAR", "3"));
                    ddlPeriod.Items.Add(new ListItem("APR", "4"));
                    ddlPeriod.Items.Add(new ListItem("MAY", "5"));
                    ddlPeriod.Items.Add(new ListItem("JUN", "6"));
                    ddlPeriod.Items.Add(new ListItem("JUL", "7"));
                    ddlPeriod.Items.Add(new ListItem("AUG", "8"));
                    ddlPeriod.Items.Add(new ListItem("SEP", "9"));
                    ddlPeriod.Items.Add(new ListItem("OCT", "10"));
                    ddlPeriod.Items.Add(new ListItem("NOV", "11"));
                    ddlPeriod.Items.Add(new ListItem("DEC", "12"));
                    break;
                case 2:
                    ViewState["ReportFreq"] = 2;
                    tr_Period.Visible = true;
                    ddlPeriod.Items.Add(new ListItem("Q1", "1"));
                    ddlPeriod.Items.Add(new ListItem("Q2", "2"));
                    ddlPeriod.Items.Add(new ListItem("Q3", "3"));
                    ddlPeriod.Items.Add(new ListItem("Q4", "4"));
                    break;
                case 3:
                    ViewState["ReportFreq"] = 3;
                    tr_Period.Visible = true;
                    ddlPeriod.Items.Add(new ListItem(" (JAN - JUN) ", "1"));
                    ddlPeriod.Items.Add(new ListItem(" (JUL - DEC) ", "2"));
                    break;
                default:
                    ViewState["ReportFreq"] = 4;
                    tr_Period.Visible = false;
                    ddlPeriod.Items.Add(new ListItem(" " + ddlYear1.SelectedValue + " ", "1"));
                    break;
            }
         
            btnSave.Enabled = true;
        }
        else
        {
            btnSave.Enabled = false;
        }
    }
    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select tableid,AttachmentName from [dbo].[t_VesselReports] WHERE VESSELID=" + VesselId + " AND REPORTID=" + ddlReport.SelectedValue + " AND YEAR(SUBMITTEDON)=" + ddlYear1.SelectedValue + " AND PERIOD=" + ddlPeriod.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            ViewState["TableId"] = dt.Rows[0]["tableid"];
            lnkDownloadFile.Text = dt.Rows[0]["AttachmentName"].ToString();
            lnkDownloadFile.CommandArgument = dt.Rows[0]["tableid"].ToString();
        }
        else
        {
            ViewState["TableId"] = 0;
            lnkDownloadFile.Text = "";
        }
    }
    protected void lnkDownloadFile_Click(object sender, EventArgs e)
    {
       //DataTable dt=Common.Execute_Procedures_Select_VIMS_ByQuery(
        txt_key.Text = ((LinkButton)sender).CommandArgument;
        btnDownloadFile_Click(sender, e);
    }
    
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        BindVessel();
        ddlFrequency.SelectedIndex = 0;
        ddlGroup.SelectedIndex = 0;
        ddlPeriod.SelectedIndex = 0;
        lit_Data.Text = "";
        DivAdhoc.Visible = false;
        rptAdhocReportlist.DataSource = null;
        rptAdhocReportlist.DataBind();
        //txtFromDate.Text = "";
        //txtToDate.Text = "";
        btn_Show_Click(sender, e);
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asd", "alert('Please select vessel.');", true);
            ddlVessel.Focus();
            return;
        }

        int ForYear = Common.CastAsInt32(ddlYear.SelectedValue);

        DateTime StartDate=new DateTime(ForYear,1,1);

        if (ddlFrequency.SelectedItem.Text.ToUpper().Trim() == "ADHOC")
        {
            BindAdhocReport(Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue));
        }
        else
        {
            lit_Data.Text = "";
            DivAdhoc.Visible = false;
            rptAdhocReportlist.DataSource = null;
            rptAdhocReportlist.DataBind();
            int Cols = 0;

            //DateTime[] Period1 = { };
            //DateTime[] Period2 = { };

            if (ddlFrequency.SelectedValue == "1") // Monthly
            {
                Cols = 12;
                //Period1 = new DateTime[Cols];
                //Period2 = new DateTime[Cols];
                //for (int i = 0; i < Cols; i++)
                //{
                //    Period1[i] = StartDate;
                //    Period2[i] = StartDate.AddMonths(12 / Cols);
                //    StartDate = Period2[i];
                //}
            }
            else if (ddlFrequency.SelectedValue == "2") // Quarterly
            {
                Cols = 4;
                //Period1 = new DateTime[Cols];
                //Period2 = new DateTime[Cols];
                //for (int i = 0; i < Cols; i++)
                //{
                //    Period1[i] = StartDate;
                //    Period2[i] = StartDate.AddMonths(12/Cols);
                //    StartDate = Period2[i];
                //}
            }
            else if (ddlFrequency.SelectedValue == "3") // Half Yearly
            {
                Cols = 2;
                //Period1 = new DateTime[Cols];
                //Period2 = new DateTime[Cols];
                //for (int i = 0; i < Cols; i++)
                //{
                //    Period1[i] = StartDate;
                //    Period2[i] = StartDate.AddMonths(12 / Cols);
                //    StartDate = Period2[i];
                //}
            }
            else // Yearly & Others
            {
                Cols = 1;
                //Period1 = new DateTime[Cols];
                //Period2 = new DateTime[Cols];
                //Period1[0] = StartDate;
                //Period2[0] = StartDate.AddYears(1);
            }

            string[] MonthsName = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string[] QuartersName = { "Q-1", "Q-2", "Q-3", "Q-4" };
            string[] HalfYears = { "JAN-JUN", "JUL-DEC" };

            StringBuilder sb = new StringBuilder();
            sb.Append("<div style='width:100%; oveflow-x:hidden; overflow-y:scroll; height:25px;' >");
            sb.Append("<table cellpadding='2' cellspacing='0' width='100%' style='border:solid 1px gray;'>");
            sb.Append("<tr class= 'headerstylegrid'>");
            sb.Append("<td style='width:40px; text-align:center;color:white'>SR#</td>");
            sb.Append("<td style='color:white;text-align:left;'>Report Name</td>");
            int ch = 0;

            for (ch = 0; ch <= Cols - 1; ch++)
            {
                sb.Append("<td style='color:white; width:70px;text-align:center;'>");
                switch (ddlFrequency.SelectedValue)
                {
                    case "1":
                        sb.Append(MonthsName[ch]);
                        break;
                    case "2":
                        sb.Append(QuartersName[ch]);
                        break;
                    case "3":
                        sb.Append(HalfYears[ch]);
                        break;
                    default:
                        sb.Append("Year - " + ForYear);
                        break;
                }
                sb.Append("</td>");
            }
            sb.Append("<td style='width:20px'>&nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</div>");
            sb.Append("<div style='width:100%; oveflow-x:hidden; overflow-y:scroll; height:375px;'>");
            sb.Append("<table cellpadding='2' cellspacing='0' width='100%' style='border:solid 1px gray;'>");
            string WhereClause = "";
            string sql = "SELECT VesselID,Vesselname FROM DBO.Vessel v with(nolock) Where 1=1 AND VesselID = " + ddlVessel.SelectedValue + "";

            if (ddlVessel.SelectedIndex != 0)
            {
                WhereClause = WhereClause + " and VesselID=" + ddlVessel.SelectedValue + "";
            }

            sql = sql + " ORDER BY VESSELNAME";
            DataTable dtVessel = Common.Execute_Procedures_Select_ByQuery(sql);

            string GroupCondition = ddlGroup.SelectedValue.Trim() == "0" ? " and 1=1 " : " and m.GroupId = " + ddlGroup.SelectedValue.Trim();

            if (dtVessel.Rows.Count > 0)
            {
                DataRow drv = dtVessel.Rows[0];
                int _VesselId = 0;
                sb.Append("<tr>");
                sb.Append("<td style='text-align:left;' colspan='" + (Cols + 3).ToString() + "'><b>&nbsp;" + drv["VesselName"].ToString() + "</b></td>");
                sb.Append("</tr>");
                _VesselId = Common.CastAsInt32(drv["VesselId"]);
                DataTable dtReports = Common.Execute_Procedures_Select_ByQuery("select ReportId,ReportName from m_vesselreports m with(nolock) where m.Frequency=" + ddlFrequency.SelectedValue + " and m.ReportId in ( Select vr.ReportId from m_vesselreports_Vessels vr with(nolock) where vr.vesselid=" + _VesselId.ToString() + ") " + GroupCondition + "  order by ReportName");

                int Counter = 1;
                foreach (DataRow dr in dtReports.Rows)
                {
                    int ReportId = Common.CastAsInt32(dr["ReportId"]);
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:center;border:dotted 1px gray;'>" + (Counter++).ToString() + "</td>");
                    sb.Append("<td style='text-align:left;border:dotted 1px gray;padding-left:5px;'>" + dr["ReportName"].ToString() + "</td>");
                    int c = 0;
                    for (c = 0; c <= Cols - 1; c++)
                    {
                        DataTable dtData = Common.Execute_Procedures_Select_ByQuery("select TableId from t_VesselReports t with(nolock) where year(SubmittedOn)=" + ddlYear.SelectedValue + " and vesselid=" + _VesselId.ToString() + " and ReportId=" + ReportId + " and Period=" + (c + 1).ToString());
                        if (dtData.Rows.Count > 0)
                        {
                            sb.Append("<td style='width:70px;border:dotted 1px gray;text-align:center;'><a href='#' onclick='CallPostBack(" + dtData.Rows[0]["TableId"].ToString() + ");'><img src='../../HRD/Images/checked-mark-green.png'/></a></td>");
                        }
                        else
                        {
                            sb.Append("<td style='width:70px;border:dotted 1px gray;text-align:center;'><img src='../../HRD/Images/cancel.png'/></td>");
                        }
                    }
                    sb.Append("<td style='width:20px;border:dotted 1px gray;text-align:center;'>&nbsp;</td>");
                    sb.Append("</tr>");
                }
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center;border:dotted 1px gray;'></td>");
                sb.Append("<td style='text-align:center;border:dotted 1px gray;padding-left:5px;'> &nbsp; </td>");
                int p = 0;
                for (p = 0; p <= Cols - 1; p++)
                {
                    bool IsValidforExport = false;
                    IsValidforExport = AllowReportToExport(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlFrequency.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), (p + 1));
                    bool IsReportVerified = false;
                    IsReportVerified = IsReportToVerified(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlFrequency.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), (p + 1));
                    if (IsValidforExport)
                    {
                       if (!IsReportVerified)
                        {
                            sb.Append("<td style='width:70px;text-align:center;border:dotted 1px gray;'><a href='#' ToolTip='Verify' onclick='VerifyVesselReport(" + ddlYear.SelectedValue.ToString() + "," + ddlGroup.SelectedValue.ToString() + "," + ddlFrequency.SelectedValue.ToString() + "," + (p + 1).ToString() + ");'><img src='../../HRD/Images/export.png'/></a></td>");
                        }
                       else
                        {
                            sb.Append("<td style='width:70px;text-align:center;border:dotted 1px gray;'></td>");
                        }
                        
                    }
                    else
                    {
                        sb.Append("<td style='width:70px;text-align:center;border:dotted 1px gray;'></td>");
                    }
                }
                sb.Append("<td style='width:20px;text-align:center;border:dotted 1px gray;'>&nbsp;</td>");
                sb.Append("</tr>");
                BindReports();
            }

            //foreach (DataRow drv in dtVessel.Rows)
            //{

            //}
            sb.Append("</table>");
            sb.Append("</div>");
            lit_Data.Text = sb.ToString();
        }
          
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        //txtFromDate.Text = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString()).ToString("dd-MMM-yyyy");//daysinmonth.ToString("dd-MMM-yyyy");
        //txtToDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");   //daysinmonth.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
        BindVessel();
        ddlVessel.SelectedIndex = 0;
        btn_Show_Click(sender, e);
    }
    //protected void btn_AddNew_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        // ddlVessel1.SelectedValue = ddlVessel.SelectedValue;
    //        // ddlVessel1_SelectedIndexChanged(sender,e);
    //        lblVessel1.Text = lblVessel.Text;
    //        //BindReports();
    //        ddlYear1.SelectedValue = hdnReportYear.Value;
    //        ddlReport.SelectedIndex = 0;
    //        ddlReport.SelectedValue = hdnReportId.Value;
    //        ddlReport_SelectedIndexChanged(sender, e);
    //        ddlPeriod.SelectedValue = hdnPeriod.Value;
    //        ddlPeriod_SelectedIndexChanged(sender, e);
    //        dvAddReport.Visible = true;
    //        ddlReport.Enabled = false;
    //        ddlYear1.Enabled = false;
    //        ddlPeriod.Enabled = false;
    //    }
    //    catch { }
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //------------------------------------
        //if (ddlVessel1.SelectedIndex <= 0) 
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "asd", "alert('Please select vessel.');", true);
        //    ddlVessel1.Focus();
        //    return; 
        //}
        //------------------------------------
        if (ddlReport.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asd", "alert('Please select report.');", true);
            ddlReport.Focus();
            return;
        }
        //------------------------------------
        if (!upl_File.HasFile)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asd", "alert('Please upload attachment file.');", true);
            upl_File.Focus();
            return;
        }
        else
        { 
             double fileinMB = upl_File.FileBytes.Length / (1024 * 1024.0);
             if (fileinMB > 0.5)
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "asd", "alert('File size must be less than or equal 500 Kb.');", true);
                 upl_File.Focus();
                 return;
             }
        }
        //------------------------------------
        int Period = Common.CastAsInt32(ddlPeriod.SelectedValue);
        int UserId=Common.CastAsInt32(Session["UID"]);
        //string FileName = Path.GetFileName(upl_File.FileName); 
        //Byte[] FileData =new Byte[upl_File.FileContent.Length];
        //upl_File.FileContent.Read(FileData,0,FileData.Length);
        string FileName = Path.GetFileName(upl_File.PostedFile.FileName);
        string fileContent = upl_File.PostedFile.ContentType;
        Stream fs = upl_File.PostedFile.InputStream;
        BinaryReader br = new BinaryReader(fs);
        byte[] bytes = br.ReadBytes((Int32)fs.Length);

        string SubmitDate="01-JAN-" + ddlYear1.SelectedValue;
        

        Common.Set_Procedures("dbo.CreateVesselReport");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(new MyParameter("@TableId", Common.CastAsInt32(ViewState["TableId"])),
                                new MyParameter("@VesselId", ddlVessel.SelectedValue),
                                new MyParameter("@ReportId",ddlReport.SelectedValue),
                                new MyParameter("@LoginId", UserId),
                                new MyParameter("@FileName", FileName),
                                new MyParameter("@FileBytes", bytes),
                                new MyParameter("@ContentType", fileContent),
                                new MyParameter("@Period", Period),
                                new MyParameter("@SubmittedOn", SubmitDate));
        DataSet ds=new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            btn_Show_Click(sender, e);    
            dvAddReport.Visible = false;
            ddlReport.Enabled = true;
            ddlYear1.Enabled = true;
            ddlPeriod.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asd", "alert('Unable to create Report.');", true);
            upl_File.Focus();
            return;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvAddReport.Visible = false;
        ddlReport.Enabled = true;
        ddlYear1.Enabled = true;
        ddlPeriod.Enabled = true;
    }
    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            int _ReportId = Common.CastAsInt32(txt_key.Text);
            string strSQL = "SELECT AttachmentName,AttachmentFile,ContentType FROM dbo.T_VESSELREPORTS with(nolock) WHERE TableId=" + _ReportId;
            DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
            if (dtFileDetails.Rows.Count > 0)
            {
                string contentType = "";
                string FileName = "";

                if (!string.IsNullOrWhiteSpace(dtFileDetails.Rows[0]["ContentType"].ToString()))
                {
                    contentType = dtFileDetails.Rows[0]["ContentType"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(dtFileDetails.Rows[0]["AttachmentName"].ToString()))
                {
                    FileName = dtFileDetails.Rows[0]["AttachmentName"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(contentType))
                {

                    byte[] latestFileContent = (byte[])dtFileDetails.Rows[0]["AttachmentFile"];
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.BinaryWrite(latestFileContent);
                    Response.Flush();
                    Response.End();
                }
            }
        }
       catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to download report.Error : " + ex.Message + "');", true);
        }
        

        //string FileName = dtFileDetails.Rows[0]["AttachmentName"].ToString();
        //byte[] buff = (byte[])dtFileDetails.Rows[0]["AttachmentFile"];
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName );
        //Response.BinaryWrite(buff);
        //Response.Flush();
        //Response.End(); 
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
       try
        {
            int VerifyReportYear = Common.CastAsInt32(hdnExpRptYr.Value);
            int VerifyReportPeriod = Common.CastAsInt32(hdnExpRptPeriod.Value);
            int VerifyReportGroup = Common.CastAsInt32(hdnExpRptGrpId.Value);
            int VerifyReportFreq = Common.CastAsInt32(hdnExpRptFreq.Value);
            string ReportFreqType = "";
            string strSQL = "select * from [dbo].[KPI_Period] WHERE (PeriodId<=4) AND PeriodId =  " + VerifyReportFreq;
            DataTable dtReportFreq = Common.Execute_Procedures_Select_ByQuery(strSQL);
            if (dtReportFreq.Rows.Count > 0)
            {
                ReportFreqType = dtReportFreq.Rows[0]["PeriodName"].ToString();
            }

            string[] MonthsName = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
            string[] QuartersName = { "Q-1", "Q-2", "Q-3", "Q-4" };
            string[] HalfYears = { "JAN-JUN", "JUL-DEC" };
            string PeriodName = "";

            switch (VerifyReportFreq.ToString())
            {
                case "1":
                    PeriodName = MonthsName[VerifyReportPeriod - 1];
                    break;
                case "2":
                    PeriodName = QuartersName[VerifyReportPeriod - 1];
                    break;
                case "3":
                    PeriodName = HalfYears[VerifyReportPeriod - 1];
                    break;
                default:
                    PeriodName = "Year";
                    break;
            }

            string ReportsPK = PeriodName + "_" + ddlYear.SelectedValue;
            string Recordname = PeriodName + "-" + ddlYear.SelectedValue;

            try
            {
                Common.Set_Procedures("[DBO].[UpdateVesselReportVerification]");
                Common.Set_ParameterLength(6);
                Common.Set_Parameters(
                    new MyParameter("@Year", VerifyReportYear),
                    new MyParameter("@Frequency", VerifyReportFreq),
                    new MyParameter("@GroupId", VerifyReportGroup),
                    new MyParameter("@Period", VerifyReportPeriod),
                    new MyParameter("@VesselId", ddlVessel.SelectedValue),
                    new MyParameter("@VefifiedBy", Session["FullName"].ToString().Trim())
                );

                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DataSet ds1 = new DataSet();
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        dt.TableName = "T_VESSELREPORTS";
                        ds1.Tables.Add(dt.Copy());
                       
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dtVessel = new DataTable();

                            string SQLVessel = "SELECT Email,VesselEmailNew,VesselCode FROM [dbo].[Vessel] with(nolock)  WHERE VesselId=" + ddlVessel.SelectedValue + "";
                            dtVessel = Common.Execute_Procedures_Select_ByQuery(SQLVessel);
                            string ZipFileName = "VESSELREPORTS_O_"+ dtVessel.Rows[0]["VesselCode"].ToString() + "_" + Recordname + ".zip";
                          
                            string SchemaFile = Server.MapPath("~/Modules/LPSQE/TEMP/VESSELREPORTS_SCHEMA.xml");
                            string DataFile = Server.MapPath("~/Modules/LPSQE/TEMP/VESSELREPORTS_DATA.xml");
                            string ZipFile = Server.MapPath("~/Modules/LPSQE/TEMP/" + ZipFileName);
                            if (File.Exists(ZipFile)) { File.Delete(ZipFile); }

                            using (ZipFile zip = new ZipFile())
                            {
                                zip.AddFile(SchemaFile);
                                zip.AddFile(DataFile);
                                zip.Save(ZipFile);
                            }

                            StringBuilder sb = new StringBuilder();
                            sb.Append("Dear Captain, ");
                            sb.Append("***********************************************************************");
                            sb.Append("<br/>");
                            sb.Append("<br/>");
                            sb.Append("<b>Please import the attached verified Vessel Reports packet</b>");
                            sb.Append("<br/>");
                            sb.Append("<b>Vessel Report : </b>" + dtVessel.Rows[0]["VesselCode"].ToString() + "-" + Recordname.ToString());
                            sb.Append("<br/>");
                            sb.Append("<br/>");
                            sb.Append("If you find any discrepancy please inform to emanager@energiossolutions.com");
                            sb.Append("<br/>");
                            sb.Append("Thank You, <br/>");
                            sb.Append("***********************************************************************");
                            sb.Append("<br/>");
                            sb.Append("<i>Do not reply to this email as we do not monitor it.</i><br/>");

                            string Subject = "Vessel Report : " + dtVessel.Rows[0]["VesselCode"].ToString() + "-" + Recordname.ToString();
                            List<string> CCMails = new List<string>();
                            List<string> BCCMails = new List<string>();

                           
                            string VesselEmail = ((dtVessel != null & dtVessel.Rows.Count > 0) ? dtVessel.Rows[0]["VesselEmailNew"].ToString() : "");
                            string CCEmail = ((dtVessel != null & dtVessel.Rows.Count > 0) ? dtVessel.Rows[0]["Email"].ToString() : "");
                            if (!string.IsNullOrEmpty(CCEmail))
                            {
                                CCMails.Add(CCEmail);
                            }

                            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
                            string result = SendMail.SendSimpleMail(fromAddress, VesselEmail, CCMails.ToArray(), BCCMails.ToArray(), Subject, sb.ToString(), ZipFile);
                            if (result == "SENT")
                            {
                                btn_Show_Click(sender, e);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "succ", "alert('Vessel Report verified and Sent to ship successfully.');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "err", "alert('Unable to Vessel Report verified.Error : " + Common.getLastError() + "');", true);
                            }
                        }
                            
                    }
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to Vessel Report verified.Error : " + Common.getLastError() + "');", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to Vessel Report verified.Error : " + ex.Message + "');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to Verify Vessel Reports.Error : " + ex.Message + "');", true);
        }


    }

    protected bool AllowReportToExport(int ReportYear,int ReportFquency, int ReportGroup,  int period)
    {
        bool AllowtoExport = false;
        string strSQL = "Select dbo.CheckReportCount ("+ ReportYear + ","+ ReportFquency + "," + ReportGroup + "," + period + ", "+ddlVessel.SelectedValue+" )";
        DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtFileDetails.Rows.Count > 0)
        {
            AllowtoExport = Convert.ToBoolean(dtFileDetails.Rows[0][0]);
        }

        return AllowtoExport;
    }

    protected bool IsReportToVerified(int ReportYear, int ReportFquency, int ReportGroup, int period)
    {
        bool IsVerified = false;
        string strSQL = "Select dbo.IsVesselReportVerified (" + ReportYear + "," + ReportFquency + "," + ReportGroup + "," + period + ", " + ddlVessel.SelectedValue + " )";
        DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtFileDetails.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dtFileDetails.Rows[0][0]))
            {
                IsVerified = true;
            }
            else
            {
                IsVerified = false;
            }
        }
        else
        {
            IsVerified = false;
        }

        return IsVerified;
    }

    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex > 0)
        {
            btn_Show_Click(sender, e);
        }
        else
        {
            lit_Data.Text = "";
        }
       
    }

    protected void ddlFrequency_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedIndex > 0)
        {
            btn_Show_Click(sender, e);
        }
        else
        {
            lit_Data.Text = "";
            DivAdhoc.Visible = false;
        }
    }

    protected void btnCloseAdhocReport_Click(object sender, EventArgs e)
    {
        dvAddAdhocReport.Visible = false;
        ddlAdhocReportList.Enabled = true;
        ddlAdhocReportYear.Enabled = true;
    }

    protected void btnDownloadAdhocAttachment_Click(object sender, EventArgs e)
    {
        try
        {
            int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            int ReportId = Common.CastAsInt32(((ImageButton)sender).Attributes["ReportId"]);
            string strSQL = "SELECT AttachmentName,AttachmentFile,ContentType FROM dbo.T_VESSELREPORTS with(nolock) WHERE TableId=" + TableId + "  AND  ReportId = " + ReportId;
            DataTable dtFileDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
            if (dtFileDetails.Rows.Count > 0)
            {
                string contentType = "";
                string FileName = "";

                if (!string.IsNullOrWhiteSpace(dtFileDetails.Rows[0]["ContentType"].ToString()))
                {
                    contentType = dtFileDetails.Rows[0]["ContentType"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(dtFileDetails.Rows[0]["AttachmentName"].ToString()))
                {
                    FileName = dtFileDetails.Rows[0]["AttachmentName"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(contentType))
                {

                    byte[] latestFileContent = (byte[])dtFileDetails.Rows[0]["AttachmentFile"];
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    Response.BinaryWrite(latestFileContent);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to download report.Error : " + ex.Message + "');", true);
        }
    }

    protected void ddlAdhocReportList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdhocReportList.SelectedIndex > 0)
        {
            //ddlAdhocReportList.SelectedValue = ReportId.ToString();
            ddlAdhocReportList.Enabled = false;
            BindAdhocVesselReportList(Convert.ToInt32(ddlAdhocReportList.SelectedValue), Convert.ToInt32(ddlAdhocReportYear.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue));
        }
        else
        {
            ddlAdhocReportList.Enabled = true;
        }
    }
    protected void BindAdhocVesselReports()
    {
        ddlAdhocReportList.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM M_VESSELREPORTS WHERE REPORTID IN (SELECT D.REPORTID FROM M_VESSELREPORTS_VESSELS D WHERE D.VESSELID=" + ddlVessel.SelectedValue + ") AND Frequency = (Select top 1 PeriodId from KPI_Period with(nolock) where PeriodName = 'Adhoc') ORDER BY REPORTNAME");
        ddlAdhocReportList.DataTextField = "ReportName";
        ddlAdhocReportList.DataValueField = "ReportId";
        ddlAdhocReportList.DataBind();
        ddlAdhocReportList.Items.Insert(0, new ListItem("<-- Select -->", "0"));
    }
    protected void BindAdhocVesselReportList(int ReportId, int ReportYear, int VesselId)
    {
        string sql = "EXEC GetAdhocVesselReportList " + ReportId + ", " + ReportYear + ", " + VesselId + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            rptAdhocVesselReportList.DataSource = dt;
            rptAdhocVesselReportList.DataBind();
            //btnExportAdhoc.Visible = true;
        }
        else
        {
            rptAdhocVesselReportList.DataSource = null;
            rptAdhocVesselReportList.DataBind();
        }
    }
    protected void ddlAdhocReportYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdhocReportYear.SelectedIndex > 0)
        {
            //ddlAdhocReportList.SelectedValue = ReportId.ToString();
            ddlAdhocReportList.Enabled = false;
            BindAdhocVesselReportList(Convert.ToInt32(ddlAdhocReportList.SelectedValue), Convert.ToInt32(ddlAdhocReportYear.SelectedValue), Convert.ToInt32(ddlVessel.SelectedValue));
        }
        else
        {
            ddlAdhocReportList.Enabled = true;
        }
    }

    protected void btnViewAdhocReport_Click(object sender, EventArgs e)
    {
        try
        {
            int ReportId = 0;
            int GroupId = 0;
            ReportId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            GroupId = Common.CastAsInt32(((ImageButton)sender).Attributes["GroupId"]);
            dvAddAdhocReport.Visible = true;
           
            // ddlVessel1.SelectedValue = ddlVessel.SelectedValue;
            // ddlVessel1_SelectedIndexChanged(sender,e);
            lblVesselName.Text = ddlVessel.SelectedItem.Text;
            ddlAdhocReportYear.SelectedIndex = 0;
            BindAdhocVesselReports();
            if (ReportId > 0)
            {
                ddlAdhocReportList.SelectedValue = ReportId.ToString();
                ddlAdhocReportList.Enabled = false;
                BindAdhocVesselReportList(ReportId, Convert.ToInt32(ddlAdhocReportYear.SelectedValue),  Convert.ToInt32(ddlVessel.SelectedValue));
            }
            else
            {
                ddlAdhocReportList.Enabled = true;
            }
        }
        catch { }
    }
    protected void BindAdhocReport(int GroupId, int VesselId)
    {
        lit_Data.Text = "";
        DivAdhoc.Visible = true;
        string sql = "EXEC GetAdhocDocument " + ddlGroup.SelectedValue + ", " + ddlFrequency.SelectedValue + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            rptAdhocReportlist.DataSource = dt;
            rptAdhocReportlist.DataBind();
        }
        else
        {
            rptAdhocReportlist.DataSource = null;
            rptAdhocReportlist.DataBind();
        }
    }
}
