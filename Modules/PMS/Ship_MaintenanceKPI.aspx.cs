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
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;
using System.IO;

public partial class Ship_MaintenanceKPI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblvessel.Text = Session["CurrentShip"].ToString();
            BindYears();

            ddlYear.SelectedValue = DateTime.Today.Year.ToString();
        }
    }

    #region ---------------- MKPI ----------------------------
    
    private void BindYears()
    {
        for (int i = DateTime.Today.Year; i >= 2012; i--)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
        }

        int CurrYear = DateTime.Today.Year;

        ddlYear.SelectedValue = CurrYear.ToString();

    }    

    protected void btnView_Click(object sender, EventArgs e)
    {
        BindKPI();
    }
    protected void btnPrintKPI_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "openprintwindow('" + Session["CurrentShip"].ToString() + "','" + ddlYear.SelectedValue.Trim() + "');", true);
    }
    public void BindKPI()
    {
        Common.Set_Procedures("sp_GetShipMaintenanceKPIData");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", Session["CurrentShip"].ToString()),
            new MyParameter("@Year", ddlYear.SelectedValue.Trim()));

        DataSet dsKPI = new DataSet();
        dsKPI.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsKPI);
        if (res)
        {
            rptKPI.DataSource = dsKPI.Tables[0];
            rptKPI.DataBind();
        }
        else
        {

        }
    }
    protected void btnSaveKPI_Click(object sender, EventArgs e)
    {

        int Month = Common.CastAsInt32(((Button)sender).CommandArgument);

        try
        {

            Common.Set_Procedures("sp_InsertUpdateMonthwiseMaintenanceKPI");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@shipid", Session["CurrentShip"].ToString()),
                new MyParameter("@year", ddlYear.SelectedValue.Trim()),
                new MyParameter("@month", Month),
                new MyParameter("@PublishedBy", Common.CastAsInt32(Session["loginid"].ToString()))
                );

            DataSet dsKPI = new DataSet();
            dsKPI.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsKPI);
            if (res)
            {
                BindKPI();
                //MessageBox1.ShowMessage("Record Saved Successfully.", false);
                ProjectCommon.ShowMessage("Record Saved Successfully.");
            }

        }
        catch (Exception ex)
        {
            //MessageBox1.ShowMessage("Unable to save record. Error : " + ex.Message, true);
            ProjectCommon.ShowMessage("Unable to save record. Error : " + ex.Message);
        }

    }
    protected void btnReview_Click(object sender, EventArgs e)
    {
        int Month = Common.CastAsInt32(((Button)sender).CommandArgument.ToString());
        ShowKPIDetailsFromDB(Month);

    }
    public void ShowKPIDetailsFromDB(int Month)
    {
        
        switch (Month)
        {
            case 1:
                lblMonth.Text = "Jan";
                break;
            case 2:
                lblMonth.Text = "Feb";
                break;
            case 3:
                lblMonth.Text = "Mar";
                break;
            case 4:
                lblMonth.Text = "Apr";
                break;
            case 5:
                lblMonth.Text = "May";
                break;
            case 6:
                lblMonth.Text = "Jun";
                break;
            case 7:
                lblMonth.Text = "Jul";
                break;
            case 8:
                lblMonth.Text = "Aug";
                break;
            case 9:
                lblMonth.Text = "Sep";
                break;
            case 10:
                lblMonth.Text = "Oct";
                break;
            case 11:
                lblMonth.Text = "Nov";
                break;
            case 12:
                lblMonth.Text = "Dec";
                break;
            default:
                lblMonth.Text = "";
                break;
        }

        lblYear.Text = ddlYear.SelectedValue;
        hfmonth.Value = Month.ToString();

        string SQL = "SELECT TotalSystemJobs,DueJobs,OverDueJobs,OD1Week,OD2Week,ODMorethan2week,OutStandingJobs " +
                     "FROM Ship_MaintenanceKPI WHERE VesselCode = '" + Session["CurrentShip"].ToString() + "' AND [Year]= " + ddlYear.SelectedValue + " AND [Month]= " + Month;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt.Rows.Count > 0)
        {
            int TotalJobs = Common.CastAsInt32(dt.Rows[0]["DueJobs"].ToString()) + Common.CastAsInt32(dt.Rows[0]["OverDueJobs"].ToString());
            int ODJobs = Common.CastAsInt32(dt.Rows[0]["OD1Week"].ToString()) + Common.CastAsInt32(dt.Rows[0]["OD2Week"].ToString()) + Common.CastAsInt32(dt.Rows[0]["ODMorethan2week"].ToString());

            txtSystemJobs.Text = dt.Rows[0]["TotalSystemJobs"].ToString();
            txtDueJobs.Text = dt.Rows[0]["DueJobs"].ToString();
            lblODJobs.Text = ODJobs.ToString();
            lblTotalJobs.Text = TotalJobs.ToString();
            txtOutstandingJobs.Text = dt.Rows[0]["OutStandingJobs"].ToString();
            txtOD1W.Text = dt.Rows[0]["OD1Week"].ToString();
            txtOD2W.Text = dt.Rows[0]["OD2Week"].ToString();
            txtODMore2W.Text = dt.Rows[0]["ODMorethan2week"].ToString();

            dvKPIEdit.Visible = true;
        }
    }
    public void ShowCalculatedKPIDetails(int Month)
    {
            //int TotalJobs = Common.CastAsInt32(dt.Rows[0]["DueJobs"].ToString()) + Common.CastAsInt32(dt.Rows[0]["OverDueJobs"].ToString());
            //int ODJobs = Common.CastAsInt32(dt.Rows[0]["OD1Week"].ToString()) + Common.CastAsInt32(dt.Rows[0]["OD2Week"].ToString()) + Common.CastAsInt32(dt.Rows[0]["ODMorethan2week"].ToString());

            //txtSystemJobs.Text = dt.Rows[0]["TotalSystemJobs"].ToString();
            //txtDueJobs.Text = dt.Rows[0]["DueJobs"].ToString();
            //lblODJobs.Text = ODJobs.ToString();
            //lblTotalJobs.Text = TotalJobs.ToString();
            //txtOutstandingJobs.Text = dt.Rows[0]["OutStandingJobs"].ToString();
            //txtOD1W.Text = dt.Rows[0]["OD1Week"].ToString();
            //txtOD2W.Text = dt.Rows[0]["OD2Week"].ToString();
            //txtODMore2W.Text = dt.Rows[0]["ODMorethan2week"].ToString();

            //dvKPIEdit.Visible = true;
    }
    protected void btnUpdateKPI_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("sp_InsertUpdate_Ship_MaintenanceKPI");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", Session["CurrentShip"].ToString()),
            new MyParameter("@Year", lblYear.Text.Trim()),
            new MyParameter("@Month", hfmonth.Value.Trim()),
            new MyParameter("@TotalSystemJobs", txtSystemJobs.Text.Trim()),
            new MyParameter("@DueJobs", txtDueJobs.Text.Trim()),
            new MyParameter("@OD1Week", txtOD1W.Text.Trim()),
            new MyParameter("@OD2Week", txtOD2W.Text.Trim()),
            new MyParameter("@ODMorethan2week", txtODMore2W.Text.Trim()),
            new MyParameter("@OutStandingJobs", txtOutstandingJobs.Text.Trim())
            );

        DataSet dsKPI = new DataSet();
        dsKPI.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsKPI);
        if (res)
        {
            BindKPI();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Maintenance KPI added/ updated successfully.')", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Failure", "alert('Unable to add/ update Maintenance KPI.')", true);
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvKPIEdit.Visible = false;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string SQL = "SELECT * FROM Ship_MaintenanceKPI WHERE VesselCode = '" + Session["CurrentShip"].ToString() + "' AND [Year]= " + ddlYear.SelectedValue;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        dt.TableName = "Ship_MaintenanceKPI";
        ds.Tables.Add(dt.Copy());

        string SchemaFile = Server.MapPath("~/TEMP/MaintenanceKPISchema.xml");
        string DataFile = Server.MapPath("~/TEMP/MaintenanceKPIData.xml");
        string ZipFile = Server.MapPath("~/TEMP/MaintenanceKPI_" + Session["CurrentShip"].ToString() + ".zip");
        ds.WriteXmlSchema(SchemaFile);
        ds.WriteXml(DataFile);

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipFile);
        }

        byte[] buff = System.IO.File.ReadAllBytes(ZipFile);
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
    #endregion
}