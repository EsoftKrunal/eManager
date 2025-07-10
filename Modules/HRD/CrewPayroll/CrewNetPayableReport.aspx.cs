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
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using CrystalDecisions.CrystalReports.ViewerObjectModel;

public partial class Modules_HRD_CrewPayroll_CrewNetPayableReport : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionManager.SessionCheck_New();
        //-----------------------------
        auth1 = new AuthenticationManager(32, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);

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

    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        SearchData(VesselId, Month, Year);
        }
        catch (Exception ex)
        {
            lbl_Message.Text = ex.Message;
        }
    }

    protected void SearchData(int VesselId, int Month, int Year)
    {
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.Get_CrewNetPayableDetails " + VesselId + "," + Year.ToString() + "," + Month.ToString());
       

        //Decimal totalEarnings = 0;
        //Decimal totalOtherEarnings = 0;
        //Decimal totalDeductions = 0;
        //Decimal totalOtherDeductions = 0;
        //Decimal totalBalanceofWages = 0;
        //foreach (DataRow dr1 in dt_Data.Rows)
        //{
        //    if (!string.IsNullOrWhiteSpace(dr1["OtherEarnings"].ToString()))
        //    {
        //        totalOtherEarnings += Convert.ToDecimal(dr1["OtherEarnings"].ToString());
        //    }
        //    if (!string.IsNullOrWhiteSpace(dr1["TotalEarnings"].ToString()))
        //    {
        //        totalEarnings += Convert.ToDecimal(dr1["TotalEarnings"].ToString());
        //    }
        //    if (!string.IsNullOrWhiteSpace(dr1["Other_Deductions"].ToString()))
        //    {
        //        totalOtherDeductions += Convert.ToDecimal(dr1["Other_Deductions"].ToString());
        //    }
        //    if (!string.IsNullOrWhiteSpace(dr1["TotalDeductions"].ToString()))
        //    {
        //        totalDeductions += Convert.ToDecimal(dr1["TotalDeductions"].ToString());
        //    }
        //    if (!string.IsNullOrWhiteSpace(dr1["CurMonBal"].ToString()))
        //    {
        //        totalBalanceofWages += Convert.ToDecimal(dr1["CurMonBal"].ToString());
        //    }
            
        //}
        //DataRow dr = dt_Data.NewRow();
        //dr["Sno"] = dt_Data.Rows.Count + 1;
        //for (int i = 1; i <= 22; i++)
        //{
        //    dr[i] = "";
        //}
        //dr[23] = "Total";
        //dr[24] = totalOtherEarnings.ToString();
        //dr[25] = totalEarnings.ToString();
        //for (int i = 26; i <= 33; i++)
        //{
        //    dr[i] = "";
        //}
        //dr[34] = totalOtherDeductions.ToString();
        //dr[35] = totalDeductions.ToString();
        //dr[36] = totalBalanceofWages.ToString();
        //dr[37] = "";
        //dr[38] = "";
        GridView1.DataSource = dt_Data;
        GridView1.DataBind();

        Session["dt_Data"] = dt_Data;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {

                int VesselId, Month, Year;
                string vesselcode = string.Empty;
                VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
                Month = Convert.ToInt32(ddl_Month.SelectedValue);
                Year = Convert.ToInt32(ddl_Year.SelectedValue);
                DataTable dt_Vessel = Common.Execute_Procedures_Select_ByQueryCMS("Select VesselCode from Vessel with(nolock) Where VesselId= " + VesselId);
                if (dt_Vessel.Rows.Count > 0)
                {
                    vesselcode = dt_Vessel.Rows[0]["VesselCode"].ToString();
                }

                DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.Get_CrewNetPayableDetails " + VesselId + "," + Year.ToString() + "," + Month.ToString());
                GridView1.DataSource = dt_Data;
                GridView1.DataBind();
                string Filename = vesselcode + "_Crew_Net_Payable_Report_" + Month + "_" + Year + ".xlsx";
                DataTable dt = new DataTable("Sheet1");   
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                { 
                    dt.Columns.Add(cell.Text);
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    dt.Rows.Add();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Controls.Count > 0)
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = (row.Cells[i].Controls[1] as Label).Text.Replace("&nbsp;", "");
                        }
                        else
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text.Replace("&nbsp;", "");
                        }
                    }
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    var startDate = new DateTime( Convert.ToInt32(ddl_Year.SelectedValue), Convert.ToInt32(ddl_Month.SelectedValue), 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);
                    int rowcount = dt.Rows.Count + 2; 
                    var ws = wb.Worksheets.Add("Sheet1");

                    ws.Range("A2").Value = "Pay Sheet : " + startDate.ToString("dd-MMM-yyyy") + " to " + endDate.ToString("dd-MMM-yyyy");
                    //ws.Cell("H1").Value = "Earnings";
                    //ws.Cell("O1").Value = "Other Earnings";
                    //ws.Cell("AA1").Value = "";
                    //ws.Cell("AB1").Value = "Deductions";
                    //ws.Cell("AH1").Value = "Other Deductions";
                    //ws.Cell("AL1").Value = "";
                    //ws.Cell("AM1").Value = "Balance of Wages";
                    //var range = ws.Range("A1:G1");
                    //var range1 = ws.Range("H1:N1");
                    //var range2 = ws.Range("O1:Z1");
                    //var range3 = ws.Range("AA1:AA1");
                    //var range4 = ws.Range("AB1:AG1");
                    //var range5 = ws.Range("AH1:AK1");
                    //var range6 = ws.Range("AL1:AL1");
                    //var range7 = ws.Range("AM1:AO1");

                    //range.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //range1.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //range2.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //range3.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //range4.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //range5.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //range6.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //range7.Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    ////range.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    ////range1.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    ////range2.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    ////range3.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    ////range4.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    ////range5.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    ////range6.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    ////range7.Merge().Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 12;
                    ////ws.Range("A1:G1,A25:G25").Style.Fill.BackgroundColor = XLColor.LightGreen;
                    ////ws.Range("H1:N1,H25:N25").Style.Fill.BackgroundColor = XLColor.LightPink;

                    //var bg1 = ws.Ranges("A1:A"+ rowcount + ", B1:B"+ rowcount + ", C1:C"+ rowcount + ", D1:D"+ rowcount + ", E1:E"+ rowcount + ", F1:F"+ rowcount + ", G1:G"+ rowcount + "");
                    //var bg2 = ws.Ranges("H1:H" + rowcount + ", I1:I" + rowcount + ", J1:J" + rowcount + ", K1:K" + rowcount + ", L1:L" + rowcount + ", M1:M" + rowcount + ", N1:N" + rowcount + "");
                    //var bg3 = ws.Ranges("O1:O" + rowcount + ", P1:P" + rowcount + ", Q1:Q" + rowcount + ", R1:R" + rowcount + ", S1:S" + rowcount + ", T1:T" + rowcount + ", U1:U" + rowcount + ",V1:V" + rowcount + ",W1:W" + rowcount + ",X1:X" + rowcount + ",Y1:Y" + rowcount + ",Z1:Z" + rowcount + ",AA1:AA" + rowcount + "");
                    //var bg4 = ws.Ranges("AB1:AB" + rowcount + ", AB1:AB" + rowcount + ", AC1:AC" + rowcount + ", AD1:AD" + rowcount + ", AE1:AE" + rowcount + ", AF1:AF" + rowcount + ", AG1:AG" + rowcount + ",AH1:AH" + rowcount + ", AI1:AI" + rowcount + ", AJ1:AJ" + rowcount + ", AK1:AK" + rowcount + ", AL1:AL" + rowcount + "");
                    //var bg5= ws.Ranges("AM1:AM" + rowcount + ", AN1:AN" + rowcount + ", AO1:AO" + rowcount + "");
                    //bg1.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    //bg2.Style.Fill.BackgroundColor = XLColor.BabyPink;
                    //bg3.Style.Fill.BackgroundColor = XLColor.PeachOrange;
                    //bg4.Style.Fill.BackgroundColor = XLColor.PowderBlue;
                    //bg5.Style.Fill.BackgroundColor = XLColor.PeachPuff;

                   // ws.Columns().AdjustToContents();
                    ws.Cell(3, 1).InsertTable(dt);
                    var row2 = ws.Row(2);
                    var row3 = ws.Row(3);
                    row2.Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 11;
                    row3.Style.Font.SetBold().Font.SetFontName("Calibri").Font.FontSize = 11;

                    row2.Style.Font.FontColor = XLColor.FromHtml("#FF010101");
                    row3.Style.Font.FontColor = XLColor.FromHtml("#FF010101");

                    ws.Tables.FirstOrDefault().ShowAutoFilter = false; // Disable AutoFilter.
                    //ws.SheetView.FreezeRows(1);
                    //ws.SheetView.FreezeColumns(2);
                    //ws.SheetView.FreezeColumns(3);
                    //ws.SheetView.FreezeColumns(4);
                    //ws.SheetView.FreezeColumns(5);
                    //ws.SheetView.FreezeColumns(6);
                    //ws.SheetView.FreezeColumns(7);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + Filename);
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

                //Response.ClearContent();
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ClearHeaders();
                //Response.Charset = "";
                ////Response.ContentType = "application/vnd.ms-excel";
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("Content-Disposition", "attachment;Filename=" + Filename);
                //StringWriter str = new StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(str);
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //GridView1.AllowPaging = false;
                //GridView1.GridLines = GridLines.Both;
                //GridView1.HeaderStyle.Font.Bold = true;
                //GridView1.RenderControl(htw);
                //Response.Write(str.ToString());
                //Response.Flush();
                //Response.Close();
                //Response.End();
            }
        }
        catch (Exception ex)
        {
            lbl_Message.Text = ex.Message;
        }
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //}
    //public void HeaderBound(object sender, EventArgs e)
    //{
    //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

    //    TableHeaderCell tec = new TableHeaderCell();
    //    tec = new TableHeaderCell();
    //    tec.ColumnSpan = 7;
    //    tec.Text = " ";
    // //   tec.Width = 600;
    //    tec.HorizontalAlign = HorizontalAlign.Center;
    //    row.Controls.Add(tec);

    //    tec = new TableHeaderCell();
    //    tec.ColumnSpan = 7;
    //    tec.HorizontalAlign = HorizontalAlign.Center;
    //    tec.Text = "Earnings";
    //    row.Controls.Add(tec);

    //    tec = new TableHeaderCell();
    //    tec.ColumnSpan = 12;
    //    tec.HorizontalAlign = HorizontalAlign.Center;
    //    tec.Text = "Other Earnings";
    //    row.Controls.Add(tec);

    //    tec = new TableHeaderCell();
    //    tec.ColumnSpan = 1;
    //    tec.HorizontalAlign = HorizontalAlign.Center;
    //    tec.Text = "";
    //    row.Controls.Add(tec);

    //    tec = new TableHeaderCell();
    //    tec.ColumnSpan = 6;
    //    tec.HorizontalAlign = HorizontalAlign.Center;
    //    tec.Text = "Deductions";
    //    row.Controls.Add(tec);

    //    tec = new TableHeaderCell();
    //    tec.ColumnSpan = 4;
    //    tec.HorizontalAlign = HorizontalAlign.Center;
    //    tec.Text = "Other Deductions";
    //    row.Controls.Add(tec);

    //    tec = new TableHeaderCell();
    //    tec.ColumnSpan = 4;
    //    tec.HorizontalAlign = HorizontalAlign.Center;
    //    tec.Text = "";
    //    row.Controls.Add(tec);
    //    GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    //}
}