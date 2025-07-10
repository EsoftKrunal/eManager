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
using System.Configuration;
using ClosedXML.Excel;
using System.Linq;

public partial class Modules_HRD_CrewPayroll_PayrollSheetMonthWise : System.Web.UI.Page
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
        int VesselId, Month, Year;
        VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
        Month = Convert.ToInt32(ddl_Month.SelectedValue);
        Year = Convert.ToInt32(ddl_Year.SelectedValue);
        SearchData(VesselId, Month, Year);




    }
    protected void SearchData(int VesselId, int Month, int Year)
    {
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.GetContractdetailforExportSheet " + VesselId + "," + Year.ToString() + "," + Month.ToString());
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

                DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.GetContractdetailforExportSheet " + VesselId + "," + Year.ToString() + "," + Month.ToString());
                GridView1.DataSource = dt_Data;
                GridView1.DataBind();
                string Filename = vesselcode + "_Portagebill_Deduction_" + Month + "_" + Year + ".xlsx";


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
                    IXLWorksheet ws = wb.Worksheets.Add(dt);
                    ws.Tables.FirstOrDefault().ShowAutoFilter = false; // Disable AutoFilter.
                    ws.SheetView.FreezeRows(1);
                    ws.SheetView.FreezeColumns(2);
                    ws.SheetView.FreezeColumns(3);
                    ws.SheetView.FreezeColumns(4);
                    ws.SheetView.FreezeColumns(5);
                    ws.SheetView.FreezeColumns(6);
                    ws.SheetView.FreezeColumns(7);
                    ws.SheetView.FreezeColumns(8);
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
        catch(Exception ex)
        {
            lbl_Message.Text = ex.Message;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

   

    public void SummaryBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0 , 0, DataControlRowType.Header, DataControlRowState.Normal);

        TableHeaderCell tec = new TableHeaderCell();
        tec = new TableHeaderCell();
        tec.ColumnSpan = 10;
        tec.Text = " ";
        tec.HorizontalAlign = HorizontalAlign.Center;
        row.Controls.Add(tec);

       
        tec = new TableHeaderCell();
        tec.ColumnSpan = 12;
        tec.HorizontalAlign = HorizontalAlign.Center;
        tec.Text = "Other Earnings";
        row.Controls.Add(tec);

        tec = new TableHeaderCell();
        tec.ColumnSpan = 8;
        tec.HorizontalAlign = HorizontalAlign.Center;
        tec.Text = "Other Deductions";
        row.Controls.Add(tec);
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }

   

    
}