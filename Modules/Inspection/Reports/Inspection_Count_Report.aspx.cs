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

public partial class Inspection_Count_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            BindTotalInspCurrYear_Grid();
        }
    }
    protected void BindTotalInspCurrYear_Grid()
    {
        DataTable dt1 = Home_Page.GetCurrentYearInspections();
        if (dt1.Rows.Count > 0)
        {
            Gv_TotalInspCurrYear.DataSource = dt1;
            Gv_TotalInspCurrYear.DataBind();
        }
        else
            BindBlank_TotalInspCurrYearGrid();
    }
    public void BindBlank_TotalInspCurrYearGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Code");
        dt.Columns.Add("Jan");
        dt.Columns.Add("Feb");
        dt.Columns.Add("Mar");
        dt.Columns.Add("Apr");
        dt.Columns.Add("May");
        dt.Columns.Add("Jun");
        dt.Columns.Add("Jul");
        dt.Columns.Add("Aug");
        dt.Columns.Add("Sep");
        dt.Columns.Add("Oct");
        dt.Columns.Add("Nov");
        dt.Columns.Add("Dec");

        for (int i = 0; i < 1; i++)
        {
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dt.Rows[dt.Rows.Count - 1][0] = "";
            dt.Rows[dt.Rows.Count - 1][1] = "";
            dt.Rows[dt.Rows.Count - 1][2] = "";
            dt.Rows[dt.Rows.Count - 1][3] = "";
            dt.Rows[dt.Rows.Count - 1][4] = "";
            dt.Rows[dt.Rows.Count - 1][5] = "";
            dt.Rows[dt.Rows.Count - 1][6] = "";
            dt.Rows[dt.Rows.Count - 1][7] = "";
            dt.Rows[dt.Rows.Count - 1][8] = "";
            dt.Rows[dt.Rows.Count - 1][9] = "";
            dt.Rows[dt.Rows.Count - 1][10] = "";
            dt.Rows[dt.Rows.Count - 1][11] = "";
            dt.Rows[dt.Rows.Count - 1][12] = "";
        }

        Gv_TotalInspCurrYear.DataSource = dt;
        Gv_TotalInspCurrYear.DataBind();
    }
    protected void Image5_Click(object sender, ImageClickEventArgs e)
    {
        Session["HMLNK"] = null;
        Session["HMINSPID"] = null;
        Session["Insp_Id"] = null;
        //Response.Redirect("SiteFrame.aspx");
        Response.Redirect("Modules/Inspection/InspectionSearch.aspx?VSession=Vimsess");
    }
}
