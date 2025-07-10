using System;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;

public partial class MRV_VoyageHistory1 : System.Web.UI.Page
{
    public int VoyageId
    {
        get { return Common.CastAsInt32(ViewState["VoyageId"]);}
        set { ViewState["VoyageId"] = value; }
    }
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------------
        ProjectCommon.SessionCheck();
        //------------------------------------
        if (!IsPostBack)
        {
            VesselCode = Request.QueryString["VesselCode"].ToString();
            VoyageId = Common.CastAsInt32(Request.QueryString["VoyageId"]);
            ShowVoyage();
      
		    showfiles();
        }
    }
    
    private void ShowVoyage()
    {
        lblErrorMessage.Text = "";
        string sql = " SELECT * FROM dbo.MRV_VW_MRV_Voyage1 WHERE vesselcode='" + VesselCode + "' AND VoyageId=" + VoyageId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];
            VoyageId = Common.CastAsInt32(Dr["VoyageId"]);

            lblVoyageNo.Text = Dr["VoyageNo"].ToString();
            lblEUVoyage.Text = (Dr["FromPort_EU"].ToString().ToLower() == "true" || Dr["ToPort_EU"].ToString().ToLower() == "true") ? " [ EU Voyage : Yes ]" : " [ EU Voyage : No ]";
            lblFromPort.Text = Dr["FromPort"].ToString();
            lblToPort.Text = Dr["ToPort"].ToString();
            lblCondition.Text = (Dr["Condition"].ToString() == "B") ? "Ballast" : "Laden";
            lblStartDate.Text = FormatDate(Dr["StartDate"]);
            lblEndDate.Text = FormatDate(Dr["EndDate"]);
            lblTotalCO2.Text = Dr["CO2"].ToString();
            lblMasterName.Text = Dr["MasterName"].ToString();
            lblCEName.Text = Dr["CEName"].ToString();
            lblcalcMethod.Text = (Dr["Method"].ToString()=="F")?"Flowmeter":"Tank Sounding";

            TimeSpan ts= Convert.ToDateTime(Dr["EndDate"]).Subtract(Convert.ToDateTime(Dr["StartDate"]));
            Int32 mins =(Int32)ts.TotalMinutes;
            lblTimeAtSea.Text = Convert.ToString(mins/60) + " Hrs " + Convert.ToString((mins%60)) + " Min ";
            lblTimeAtAnchorage.Text = Dr["AnchorageHrs"].ToString() + " Hrs " + Dr["AnchorageHrs"].ToString() + " Mins";

            lblCargoCarried.Text = Dr["CurrentCargoWeight"].ToString();
            lblTotalTransportWork.Text = Dr["TotalTransportWork"].ToString() ;
            lblDistanceToGo.Text = Dr["DistanceToGo"].ToString();
            lblCo2EmissionAtSea.Text = Dr["CO2ATSEA"].ToString();
            lblCo2EmissionInPort.Text = Dr["CO2ATPORT"].ToString();
            if (Dr["Method"].ToString() == "F")
            {
                pnl_FM.Visible = true;
                ShowReportPerVoyage_FM();
                ShowReportPerVoyageInPort_FM();                
            }
            else
            {
                pnl_TS.Visible = true;
                ShowReportPerVoyage_TS();
                ShowReportPerVoyageInPort_TS();
            }
        }
        else
        {
            ShowMessage(true, "Sorry ! selected record does'nt exists in the database.");
        }
    }    
    public void ShowMessage(bool error, string Message)
    {
        lblMsg.Text = Message;
        lblMsg.CssClass = (error) ? "modal_error" : "modal_success";
    }
 
    public string FormatDate(object dt)
    {
        try
        {
            return Convert.ToDateTime(dt).ToString("dd-MMM-yyyy HH:mm");
        }catch
        { return ""; }
    }
    public string FormatDateOnly(object dt)
    {
        try
        {
            return Convert.ToDateTime(dt).ToString("dd-MMM-yyyy");
        }
        catch
        { return ""; }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fsadfa", "closeWindow();", true);
    }

    // Report Per Voyage
    //--------------------------------------------
    public void ShowReportPerVoyage_FM()
    {
        string sql ="select *,FUELCONSUMED1*Factor1 as Co2Emission1, FUELCONSUMED2*Factor2 as Co2Emission2, FUELCONSUMED3*Factor3 as Co2Emission3 " +
                    "from " +
                    "( " +
                    "select EmissionSourceName, " +
                    "(SELECT SUM(FUELCONSUMED) FROM DBO.MRV_VoyageDetails_InPort1 D WHERE VOYAGEID = " + VoyageId + " AND D.VESSELCODE = '" + VesselCode + "' AND FUELTYPEID = 1 AND D.SOURCEID=S.EMISSIONSOURCEID) AS FUELCONSUMED1, " +
                    "(SELECT SUM(FUELCONSUMED) FROM DBO.MRV_VoyageDetails_InPort1 D WHERE VOYAGEID = " + VoyageId + " AND D.VESSELCODE = '" + VesselCode + "' AND FUELTYPEID = 2 AND D.SOURCEID=S.EMISSIONSOURCEID) AS FUELCONSUMED2, " +
                    "(SELECT SUM(FUELCONSUMED) FROM DBO.MRV_VoyageDetails_InPort1 D WHERE VOYAGEID = " + VoyageId + " AND D.VESSELCODE = '" + VesselCode + "' AND FUELTYPEID = 3 AND D.SOURCEID=S.EMISSIONSOURCEID) AS FUELCONSUMED3, " +
                    "(SELECT Co2EmissionPerMT FROM DBO.MRV_FuelTypes WHERE FUELTYPEID = 1) AS Factor1, " +
                    "(SELECT Co2EmissionPerMT FROM DBO.MRV_FuelTypes WHERE FUELTYPEID = 2) AS Factor2, " +
                    "(SELECT Co2EmissionPerMT FROM DBO.MRV_FuelTypes WHERE FUELTYPEID = 3) AS Factor3 " +
                    "from DBO.MRV_EmissionSource s where VesselCode = '" + VesselCode +"' " +
                    ") a";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFuelConsVoyage.DataSource = dt1;
        rptFuelConsVoyage.DataBind();

        lblFC1.Text = dt1.Compute("SUM(FUELCONSUMED1)", "").ToString();
        lblFC2.Text = dt1.Compute("SUM(FUELCONSUMED2)", "").ToString();
        lblFC3.Text = dt1.Compute("SUM(FUELCONSUMED3)", "").ToString();

        lblCO21.Text = dt1.Compute("SUM(Co2Emission1)", "").ToString();
        lblCO22.Text = dt1.Compute("SUM(Co2Emission2)", "").ToString();
        lblCO23.Text = dt1.Compute("SUM(Co2Emission3)", "").ToString();
    }
    public void ShowReportPerVoyageInPort_FM()
    {
        string sql = "select *,FUELCONSUMED1*Factor1 as Co2Emission1, FUELCONSUMED2*Factor2 as Co2Emission2, FUELCONSUMED3*Factor3 as Co2Emission3 " +
                    "from " +
                    "( " +
                    "select EmissionSourceName, " +
                    "(SELECT SUM(FUELCONSUMED) FROM DBO.MRV_VoyageDetails_AtSea1 D WHERE VOYAGEID = " + VoyageId + " AND D.VESSELCODE = '" + VesselCode + "' AND FUELTYPEID = 1 AND D.SOURCEID=S.EMISSIONSOURCEID) AS FUELCONSUMED1, " +
                    "(SELECT SUM(FUELCONSUMED) FROM DBO.MRV_VoyageDetails_AtSea1 D WHERE VOYAGEID = " + VoyageId + " AND D.VESSELCODE = '" + VesselCode + "' AND FUELTYPEID = 2 AND D.SOURCEID=S.EMISSIONSOURCEID) AS FUELCONSUMED2, " +
                    "(SELECT SUM(FUELCONSUMED) FROM DBO.MRV_VoyageDetails_AtSea1 D WHERE VOYAGEID = " + VoyageId + " AND D.VESSELCODE = '" + VesselCode + "' AND FUELTYPEID = 3 AND D.SOURCEID=S.EMISSIONSOURCEID) AS FUELCONSUMED3, " +
                    "(SELECT Co2EmissionPerMT FROM DBO.MRV_FuelTypes WHERE FUELTYPEID = 1) AS Factor1, " +
                    "(SELECT Co2EmissionPerMT FROM DBO.MRV_FuelTypes WHERE FUELTYPEID = 2) AS Factor2, " +
                    "(SELECT Co2EmissionPerMT FROM DBO.MRV_FuelTypes WHERE FUELTYPEID = 3) AS Factor3 " +
                    "from DBO.MRV_EmissionSource s where VesselCode = '" + VesselCode + "' " +
                    ") a";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFuelConsPort.DataSource = dt1;
        rptFuelConsPort.DataBind();

        lblFC1_P.Text = dt1.Compute("SUM(FUELCONSUMED1)", "").ToString();
        lblFC2_P.Text = dt1.Compute("SUM(FUELCONSUMED2)", "").ToString();
        lblFC3_P.Text = dt1.Compute("SUM(FUELCONSUMED3)", "").ToString();

        lblCO21_P.Text = dt1.Compute("SUM(Co2Emission1)", "").ToString();
        lblCO22_P.Text = dt1.Compute("SUM(Co2Emission2)", "").ToString();
        lblCO23_P.Text = dt1.Compute("SUM(Co2Emission3)", "").ToString();
    }
    //--------------------------------------------
    public void ShowReportPerVoyage_TS()
    {
        //string sql = "select ShortName,Consumed,Consumed*Co2EmissionPerMT as Co2Emission from  DBO.MRV_VoaygeDetails_Tank1 c  inner join DBO.MRV_FuelTypes ft on c.FuelTypeId=ft.FuelTypeId where c.VoyageId =" + VoyageId + " and c.VesselCode ='" + VesselCode + "' and location='S'";
        string sql = "SELECT FuelTypeName, Opening, Bunker, Closing, Consumed,Consumed*Co2EmissionPerMT as Co2Emission FROM DBO.MRV_FuelTypes m LEFT JOIN DBO.MRV_VoaygeDetails_Tank1 t on m.FuelTypeId = t.FuelTypeId and t.VesselCode = '" + VesselCode + "' and t.voyageid = " + VoyageId + " and location = 'S'";
        
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFuelConsTank_Sea.DataSource = dt1;
        rptFuelConsTank_Sea.DataBind();

        lblOP.Text = dt1.Compute("SUM(Opening)", "").ToString();
        lblCl.Text = dt1.Compute("SUM(Bunker)", "").ToString();
        lblClo.Text = dt1.Compute("SUM(Closing)", "").ToString();
        lblCons.Text = dt1.Compute("SUM(Consumed)", "").ToString();
        lblConsco2.Text = dt1.Compute("SUM(Co2Emission)", "").ToString();
    }
    public void ShowReportPerVoyageInPort_TS()
    {
        //string sql = "select ShortName,Consumed,Consumed*Co2EmissionPerMT as Co2Emission from  DBO.MRV_VoaygeDetails_Tank1 c  inner join DBO.MRV_FuelTypes ft on c.FuelTypeId=ft.FuelTypeId where c.VoyageId =" + VoyageId + " and c.VesselCode ='" + VesselCode + "' and location='P'";
        string sql = "SELECT FuelTypeName, Opening, Bunker, Closing, Consumed,Consumed*Co2EmissionPerMT as Co2Emission FROM DBO.MRV_FuelTypes m LEFT JOIN DBO.MRV_VoaygeDetails_Tank1 t on m.FuelTypeId = t.FuelTypeId and t.VesselCode = '" + VesselCode + "' and t.voyageid = " + VoyageId + " and location = 'P'";

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        Repeater2.DataSource = dt1;
        Repeater2.DataBind();

        lblOP_P.Text = dt1.Compute("SUM(Opening)", "").ToString();
        lblCl_P.Text = dt1.Compute("SUM(Bunker)", "").ToString();
        lblClo_P.Text = dt1.Compute("SUM(Closing)", "").ToString();
        lblCons_P.Text = dt1.Compute("SUM(Consumed)", "").ToString();
        lblConsco2_P.Text = dt1.Compute("SUM(Co2Emission)", "").ToString();
    }
    //--------------------------------------------
    public void showfiles()
    {
        string sql2 = "select ROW_NUMBER() OVER(ORDER BY FileId) AS Sr,FileId,VoyageId,VesselCode,Description,FileName from DBO.MRV_Voyage1_Attachments where VesselCode = '" + VesselCode + "' AND VoyageId = " + VoyageId + " order by FileId";
        rptFiles.DataSource = Common.Execute_Procedures_Select_ByQuery(sql2);
        rptFiles.DataBind();
    }
    //--------------------------------------------
    protected void imgbtn_Clip_Click(object sender, EventArgs e)
    {
        //download
        int fileid = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt=Common.Execute_Procedures_Select_ByQuery("select filename,filecontent from DBO.MRV_Voyage1_Attachments where VesselCode = '" + VesselCode + "' AND VoyageId = " + VoyageId + " and FileId=" + fileid);
        if(dt.Rows.Count>0)
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition","attachment; filename=\"" + dt.Rows[0]["filename"] + "\"");
            Response.BinaryWrite((byte[])dt.Rows[0]["filecontent"]);
            Response.End();
        }
    }
}
