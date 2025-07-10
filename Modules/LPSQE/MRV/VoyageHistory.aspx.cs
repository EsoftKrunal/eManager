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

public partial class MRV_VoyageHistory : System.Web.UI.Page
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
            
            //BindFuelTankReading();
            //BindBunker();
            BindGrid();
            BindActivity();
            ShowReportPerVoyage();
            ShowReportPerVoyageInPort();
        }
    }
    
    private void ShowVoyage()
    {
        lblErrorMessage.Text = "";
        string sql = " SELECT * FROM dbo.MRV_VW_MRV_Voyage WHERE vesselcode='" + VesselCode + "' AND VoyageId=" + VoyageId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];
            VoyageId = Common.CastAsInt32(Dr["VoyageId"]);

            lblVoyageNo.Text = Dr["VoyageNo"].ToString();
            lblFromPort.Text = Dr["FromPort"].ToString();
            lblToPort.Text = Dr["ToPort"].ToString();
            lblErrorMessage.Text = (Dr["FmDefected"].ToString() == "Y") ? "NOTE : Flowmeter was defected in this voyage." : "";
            lblCondition.Text = (Dr["Condition"].ToString() == "B") ? "Ballast" : "Laden";
            lblStartDate.Text = FormatDate(Dr["StartDate"]);
            lblTotalCO2.Text = Dr["CO2"].ToString();

            //lblTotalSteamingTime.Text = Dr["TotaSteamingHour"].ToString();
            //lblTotalDistanceTravelled.Text = Dr["TotalDistanceMadeGoods"].ToString();
            //if (Common.CastAsDecimal(Dr["TotaSteamingHour"]) <= 0)
            //    lblAvgSpeed.Text = "0";
            //else
            //    lblAvgSpeed.Text = Common.CastAsDecimal(Common.CastAsDecimal(Dr["TotalDistanceMadeGoods"]) / Common.CastAsDecimal(Dr["TotaSteamingHour"])).ToString("0.00");

            //lblTotalCargoCarried.Text = Dr["TotalCargoCaried"].ToString();

            //lblTotalTransportWork.Text = Common.CastAsDecimal(Common.CastAsDecimal(Dr["TotalDistanceMadeGoods"]) * Common.CastAsDecimal(Dr["TotalCargoCaried"])).ToString("0.00");


            lblDistanceToGo.Text = Dr["DistanceToGo"].ToString();
            
        }
        else
        {
            ShowMessage(true, "Sorry ! selected record does'nt exists in the database.");
        }
    }
    
    //public void BindFuelTankReading()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select (Case when VerifiedOn is null then '-' else 'Verified' end)Verified,* from MRV_FuelReadings where VoyageID=" + VoyageId + " order by FRID desc");
    //    rptFuelTankReading.DataSource = dt;
    //    rptFuelTankReading.DataBind();
    //}
    //public void BindBunker()
    //{
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select (Case when VerifiedOn is null then '-' else 'Verified' end)Verified,* from MRV_Bunkers where VoyageID=" + VoyageId + " order by BunkerID desc");
    //    rptBankaring.DataSource = dt;
    //    rptBankaring.DataBind();
    //}
    public void ShowMessage(bool error, string Message)
    {
        lblMsg.Text = Message;
        lblMsg.CssClass = (error) ? "modal_error" : "modal_success";
    }
    public void BindGrid()
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,EU=case when FromPort_EU=1 or ToPort_EU=1 then ' Yes' else 'No' End FROM DBO.MRV_Voyage Where Status<>'O' Order BY VoyageId desc");
        //rptData.DataSource =dt ;
        //rptData.DataBind();
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
    
    
    
    public void BindActivity()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from dbo.MRV_VW_MRV_Voyage_Activity WHERE VESSELCODE='" + VesselCode + "' AND VOYAGEID=" + VoyageId + " order by TableId desc");
        rptActivity.DataSource = dt;
        rptActivity.DataBind();
    }

    //Report Per Voyage-------------------------
    public void ShowReportPerVoyage()
    {
        //--,
        string sql = " Select *,TotalTransportWork / DistanceTravelled as CargoCarried,(TimeAtSeaTot/60) as TimeAtSeaHrs,(TimeAtSeaTot%60) as TimeAtSeaMin from  " +
                   " ( " +
                   "     Select Sum(SteamingHrs * 60 + SteamingMin) as TimeAtSeaTot, " +
                   "     Convert(Decimal(18, 2), Sum(DistanceMadeGoods)) as DistanceTravelled, " +
                   "     Convert(Decimal(18, 2), sum(CurrentCargoWeight * DistanceMadeGoods)) as TotalTransportWork,     " +
                   "     (SElect sum(Co2Emission) from dbo.MRV_VoyageActivity_Sources Where VoyageID = " + VoyageId+" and VesselCOde = '"+VesselCode+"' " +
                   "     and TableID in(Select TableID from dbo.MRV_Voyage_Activity Where vesselCode = '" + VesselCode+"' and VoyageID ="+VoyageId+"" +
                   "     and ReportingType = 'V'  and ActivityId not in (3, 4)))as CO2Emission " +
                   "     from dbo.MRV_Voyage_Activity " +
                   "     Where vesselCode = '"+VesselCode+"' and VoyageID = "+VoyageId+" and ReportingType = 'V'  and ActivityId not in (3,4) " +
                   " ) tbl  ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];
            lblTimeAtSea.Text = Dr["TimeAtSeaHrs"].ToString() + " Hrs " + Dr["TimeAtSeaMin"].ToString() + " Min ";            
            lblDistanceTravelled.Text = Dr["DistanceTravelled"].ToString();
            lblCargoCarried.Text = Common.CastAsDecimal(Dr["CargoCarried"]).ToString("0.00");
            lblTotalTransportWork.Text = Common.CastAsDecimal(Dr["TotalTransportWork"]).ToString("0.00") + " (NM.MT)";
            
            sql = " Select *,(TimeAtSeaTot/60) as TimeAtSeaHrs,(TimeAtSeaTot%60) as TimeAtSeaMin from (Select isnull(Sum(SteamingHrs*60+SteamingMin),0) TimeAtSeaTot from dbo.MRV_Voyage_Activity " +
                  " Where vesselCode = '" + VesselCode + "' and VoyageID = " + VoyageId + " and ReportingType = 'V'  and ActivityId in (3,4) ) tbl";
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
            if(dt1.Rows.Count>0)
                lblTimeAtAnchorage.Text = dt1.Rows[0]["TimeAtSeaHrs"].ToString() + " Hrs " + dt1.Rows[0]["TimeAtSeaMin"].ToString() + " Min ";
        }


        sql = "SELECT F.FuelTypeId,F.FuelTypeName,F.ShortName,SUM(S.FuelConsumed) AS F_FuelConsumed,SUM(S.Co2Emission) AS F_Co2Emission,SUM(T.FuelConsumed) AS T_FuelConsumed,SUM(T.Co2Emission) AS T_Co2Emission " +
                   "FROM dbo.MRV_FuelTypes F " +
                   "INNER JOIN dbo.MRV_Voyage_Activity V ON V.VesselCode = '" + VesselCode + "' AND V.VoyageId = " + VoyageId + " AND V.ReportingType = 'V' " +
                   "LEFT JOIN dbo.MRV_VoyageActivity_Sources S ON F.FueltypeId = S.FuelTypeId AND S.VesselCode = '" + VesselCode + "' AND S.VoyageId = " + VoyageId + " AND S.TABLEID=V.TABLEID " +
                   "LEFT JOIN dbo.MRV_Voyage_Activity_Tank_Consumption T ON F.FueltypeId = T.FuelTypeId AND T.VesselCode = '" + VesselCode + "' AND T.VoyageId = " + VoyageId + " AND T.TABLEID=V.TABLEID " +
                   "GROUP BY F.FuelTypeId,F.FuelTypeName,F.ShortName";

        rptFuelConsVoyage.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFuelConsVoyage.DataBind();
    }
    public void ShowReportPerVoyageInPort()
    {
        //--,
        string sql = " SElect sum(Co2Emission) as Co2Emission from  dbo.MRV_VoyageActivity_Sources Where vesselCode = '" + VesselCode + "' and VoyageID = " + VoyageId + " " +
                     " and TableID in(Select TableID from dbo.MRV_Voyage_Activity Where vesselCode = '" + VesselCode + "' and VoyageID = " + VoyageId + "  and ReportingType = 'I'  )";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];
            lblCo2EmissionInPort.Text = Common.CastAsDecimal(Dr["CO2Emission"]).ToString("0.00");
            lblFuelConsumptionInPort.Text = "NA";
        }

        sql = "SELECT F.FuelTypeId,F.FuelTypeName,F.ShortName,SUM(S.FuelConsumed) AS F_FuelConsumed,SUM(S.Co2Emission) AS F_Co2Emission,SUM(T.FuelConsumed) AS T_FuelConsumed,SUM(T.Co2Emission) AS T_Co2Emission " +
                   "FROM dbo.MRV_FuelTypes F " +
                   "INNER JOIN dbo.MRV_Voyage_Activity V ON V.VesselCode = '" + VesselCode + "' AND V.VoyageId = " + VoyageId + " AND V.ReportingType = 'I' " +
                   "LEFT JOIN dbo.MRV_VoyageActivity_Sources S ON F.FueltypeId = S.FuelTypeId AND S.VesselCode = '" + VesselCode + "' AND S.VoyageId = " + VoyageId + " AND S.TABLEID=V.TABLEID " +
                   "LEFT JOIN dbo.MRV_Voyage_Activity_Tank_Consumption T ON F.FueltypeId = T.FuelTypeId AND T.VesselCode = '" + VesselCode + "' AND T.VoyageId = " + VoyageId + " AND T.TABLEID=V.TABLEID " +
                   "GROUP BY F.FuelTypeId,F.FuelTypeName,F.ShortName";

        rptFuelConsPort.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFuelConsPort.DataBind();


    }
}
