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

public partial class MaintenanceKPI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindVessels();
            BindYears();

            ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
        }
    }

    #region ---------------- MKPI ----------------------------

    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();
        }
        ddlVessels.Items.Insert(0, new ListItem("< ALL >", ""));
    }
    private void BindYears()
    {
        for (int i = DateTime.Today.Year; i >= 2012; i--)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
        }

        int CurrYear = DateTime.Today.Year;

        ddlYear.SelectedValue = CurrYear.ToString();

    }
    //private void BindMonths()
    //{
    //    ddlMonth.Items.Add(new ListItem("Jan", "1"));
    //    ddlMonth.Items.Add(new ListItem("Feb", "2"));
    //    ddlMonth.Items.Add(new ListItem("Mar", "3"));
    //    ddlMonth.Items.Add(new ListItem("Apr", "4"));
    //    ddlMonth.Items.Add(new ListItem("may", "5"));
    //    ddlMonth.Items.Add(new ListItem("Jun", "6"));
    //    ddlMonth.Items.Add(new ListItem("Jul", "7"));
    //    ddlMonth.Items.Add(new ListItem("Aug", "8"));
    //    ddlMonth.Items.Add(new ListItem("Sep", "9"));
    //    ddlMonth.Items.Add(new ListItem("Oct", "10"));
    //    ddlMonth.Items.Add(new ListItem("Nov", "11"));
    //    ddlMonth.Items.Add(new ListItem("Dec", "12"));

    //    ddlMonth.SelectedValue = "1";
    //}

    protected void Rad_officeship_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        pnlOffice.Visible = Rad_officeship.SelectedValue == "O";
        pnlShip.Visible = Rad_officeship.SelectedValue == "S";
        BindKPI();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        BindKPI();
    }
    protected void btnPrintKPI_Click(object sender, EventArgs e)
    {
        //if (ddlVessels.SelectedIndex == 0)
        //{
        //    MessageBox1.ShowMessage("Please select vessel.", true);
        //    ddlVessels.Focus();
        //    return;
        //}

        ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "openprintwindow('" + ddlVessels.SelectedValue.Trim() + "','" + ddlYear.SelectedValue.Trim() + "');", true);
    }
    public void BindKPI()
    {
        Common.Set_Procedures("sp_CheckMaintenanceKPIData");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim()),
            new MyParameter("@Year", ddlYear.SelectedValue.Trim()),
            new MyParameter("@Mnth", ddlMonth.SelectedValue.Trim())
            );

        DataSet dsKPI = new DataSet();
        dsKPI.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsKPI);
        if (res)
        {

            rptKPI.DataSource = dsKPI.Tables[0];
            rptKPI.DataBind();
        }

        Common.Set_Procedures("sp_CheckMaintenanceKPIData_ShipData");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", ddlVessels.SelectedValue.Trim()),
            new MyParameter("@Year", ddlYear.SelectedValue.Trim()),
            new MyParameter("@Mnth", ddlMonth.SelectedValue.Trim())
            );

        DataSet dsKPI1 = new DataSet();
        dsKPI1.Clear();
        Boolean res1;
        res1 = Common.Execute_Procedures_IUD(dsKPI1);
        if (res1)
        {
            rpt_ShipData.DataSource = dsKPI1.Tables[0];
            rpt_ShipData.DataBind();
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
                new MyParameter("@shipid", ddlVessels.SelectedValue.Trim()),
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
    protected void btnEditKPI_Click(object sender, EventArgs e)
    {
        string VesselCode = ((Button)sender).CommandArgument.ToString().Split('@').GetValue(0).ToString();
        int Month = Common.CastAsInt32(((Button)sender).CommandArgument.ToString().Split('@').GetValue(1));

        ShowKPIDetails(VesselCode, Month);

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if(ddlVessels.SelectedIndex<=0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadfa", "alert('Please select vessel.');", true);
        }
        else
        {
            // check for last month only

            int lastmonth = DateTime.Today.AddMonths(0).Month;
            int lastyear = DateTime.Today.AddMonths(0).Year;


            string sql = "select * from VSL_MaintenanceKPI where vesselcode='"  + ddlVessels.SelectedValue +  "' AND MONTH=" + ddlMonth.SelectedValue + " AND YEAR=" + ddlYear.SelectedValue;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if(dt.Rows.Count>0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadfa", "alert('KPI for last month alredy exists.');", true);
            }
            else
            {
                int IINDlastmonth = DateTime.Today.AddMonths(-1).Month;
                int IINDlastyear = DateTime.Today.AddMonths(-1).Year;

                Common.Execute_Procedures_Select_ByQuery("INSERT INTO VSL_MaintenanceKPI(VesselCode,Year,Month,TotalSystemJobs,DueJobs,OverDueJobs,OutStandingJobs,PublishedBy,PublishedOn,PostponeOther,PostponeDocking,RecordStatus) select VesselCode," + lastyear +  "," + lastmonth + ",TotalSystemJobs,DueJobs,OutStandingJobs AS OverDueJobs,0 as OutStandingJobs,1,getdate(),PostponeOther,PostponeDocking,0 from VSL_MaintenanceKPI where vesselcode='" + ddlVessels.SelectedValue + "' AND MONTH=" + IINDlastmonth + " AND YEAR=" + IINDlastyear);
            }
        }
    }
    
    public void ShowKPIDetails(string vesselcode, int Month)
    {
        lblvessel.Text = vesselcode;
        ViewState["MnthId"] = Month;
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

        string SQL = "SELECT TotalSystemJobs,DueJobs,OverDueJobs,OD1Week,OD2Week,ODMorethan2week,OutStandingJobs " +
                     "FROM VSL_MaintenanceKPI WHERE VesselCode = '" + vesselcode + "' AND [Year]= " + ddlYear.SelectedValue + " AND [Month]= " + Month;
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
    protected void btnUpdateKPI_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("sp_Update_MaintenanceKPI");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", lblvessel.Text.Trim()),
            new MyParameter("@Year", lblYear.Text.Trim()),
            new MyParameter("@Month", Common.CastAsInt32(ViewState["MnthId"])),
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Maintenance KPI updated successfully.')", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Failure", "alert('Unable to update Maintenance KPI.')", true);
        }


    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearKPIDetails();
        dvKPIEdit.Visible = false;
    }
    public void ClearKPIDetails()
    {
        txtSystemJobs.Text = "";
        txtDueJobs.Text = "";
        lblODJobs.Text = "";
        lblTotalJobs.Text = "";
        txtOutstandingJobs.Text = "";
        txtOD1W.Text = "";
        txtOD2W.Text = "";
        txtODMore2W.Text = "";
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {

    }

    #endregion
}