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
using System.Text;
using System.Collections.Generic;

public partial class MRV_NewActivity : System.Web.UI.Page
{
    public int TableId
    {
        get { return Common.CastAsInt32(ViewState["_TableId"]); }
        set { ViewState["_TableId"] = value; }
    }
    public int ActivityId
    {
        get { return Common.CastAsInt32(ViewState["_ActivityId"]); }
        set { ViewState["_ActivityId"] = value; }
    }
    public int VoyageID
    {
        get { return Common.CastAsInt32(ViewState["_VoyageID"]); }
        set { ViewState["_VoyageID"] = value; }

    }
    public int BunkerID
    {
        get { return Common.CastAsInt32(ViewState["_BunkerID"]); }
        set { ViewState["_BunkerID"] = value; }

    }
    
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value.ToString(); }
    }
    public string ReportingType
    {
        get { return ViewState["ReportingType"].ToString(); }
        set { ViewState["ReportingType"] = value.ToString(); }
    }
    public bool RecordVerified
    {
        get { return Convert.ToBoolean(ViewState["_RecordVerified"]); }
        set { ViewState["_RecordVerified"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------------
        ProjectCommon.SessionCheck();
        //------------------------------------
        Session["MM"] = 2;
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            VesselCode = Page.Request.QueryString["VesselCode"].ToString();
            VoyageID = 0;

            TableId = Common.CastAsInt32(Page.Request.QueryString["TableId"]);
            VoyageID = Common.CastAsInt32(Page.Request.QueryString["VoyageID"]);
            ShowVoyageDetails();
            ShowMasterData();
            BindActivity();                        
            BindFuelConsumption_fm();
            BindFuelConsumption_tank();
            BindFuelTankReadingReport();
            BindBunkeringReport();
            
        }
    }


    public void BindActivity()
    {
        string whereclause = " ";
        switch(ReportingType)
        {
            case "V":

                if (ActivityId==10)
                    whereclause = " where ActivityId In (1,2,3,4,7,8) ";
                else
                    whereclause = " where ActivityId In (1,2,3,4,5,7,8) ";

                break;
            case "I":
                whereclause = " where ActivityId In (6,7,8,9) ";
                break;
            default:
                break;

        }
    }
    public void ShowMasterData()
    {
        string sql = " SElect a.ActivityName,va.* from dbo.MRV_Voyage_Activity va inner join dbo.MRV_Activity a on va.ActivityId=a.ActivityId  Where vesselcode='" + VesselCode + "' And TableId=" + TableId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {

            DataRow Dr = dt.Rows[0];
            lblActivityName.Text = dt.Rows[0]["ActivityName"].ToString();
            lblCargoWeight.Text = dt.Rows[0]["CurrentCargoWeight"].ToString();
            ActivityId = Common.CastAsInt32(Dr["ActivityId"]);
            string[] STime = FormatDate(dt.Rows[0]["StartTime"]).Split(' ');
            lblStartTime.Text = STime[0] + " " + STime[1];
            try
            {
                string[] ETime = FormatDate(dt.Rows[0]["EndTime"]).Split(' ');
                lblEndtime.Text = ETime[0] + " " + ETime[1];
            }
            catch { }

            ReportingType = dt.Rows[0]["ReportingType"].ToString();
            lblSteamingHRS.Text = dt.Rows[0]["SteamingHrs"].ToString();
            lblSteamingMin.Text = dt.Rows[0]["SteamingMin"].ToString();

            lblDistanceMadeGood.Text = dt.Rows[0]["DistanceMadeGoods"].ToString();
            lblCargoOperation.Text = (dt.Rows[0]["DistanceMadeGoods"].ToString() == "True") ? "Yes" : "No";
            lblTransportWork.Text = (Common.CastAsDecimal(lblCargoWeight.Text) * Common.CastAsDecimal(lblDistanceMadeGood.Text)).ToString();

            if (ReportingType == "V")
            {
                trCurrentCargoWeight.Visible = true;
                
                trTransportWork.Visible = true;
            }
            else
            {
                trCurrentCargoWeight.Visible = false;
                
                trTransportWork.Visible = false;
            }

            if (lblEndtime.Text.Trim() != "")
            {
                RecordVerified = true;                
                divActivityClosureDetails.Visible = true;
            }
            else
            {
                RecordVerified = false;                
                divActivityClosureDetails.Visible = false;
            }

            ShowFuelChange();
        }
        else
        {
            
        }
    }
    public string FormatDate(object dt)
    {
        try
        {
            return Convert.ToDateTime(dt).ToString("dd-MMM-yyyy HH:mm");
        }
        catch
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
    public void ShowMessage(bool error, string Message)
    {
        lblMsg.Text = Message;
        lblMsg.CssClass = (error) ? "modal_error" : "modal_success";
    }
    

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fsadfa", "closeWindow();", true);
    }
    public void ShowVoyageDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_Voyage where VoyageID="+VoyageID+" and VesselCode='" + VesselCode + "' Order BY VoyageId desc");
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];
            VoyageID = Common.CastAsInt32(Dr["VoyageId"].ToString());
            lblVoyageNo.Text = Dr["VoyageNo"].ToString();
            lblFromPort.Text = Dr["FromPort"].ToString();
            lblToPort.Text = Dr["ToPort"].ToString();
            lblStartDate.Text = FormatDate(Dr["StartDate"]);
        }
    }

    
    
    // Flow Meter Reading -----------------------------------------------------------
    public void ShowFuelChange()
    {
        string sql = " SELECT fm.VesselCode,fm.FlowMeterId,fm.FlowMeterName,ISNULL(fmr.FuelTypeId,0) AS FuelTypeId,ft.FuelTypeName,DensityAt15Deg,Start,[End] " +
           " , FlowMeterTemp, DensityCorrFactor, FuelPassed, FM.ReadingMode,'N' AS OP_MOD, " +
           " fdr.DefectNumber as DefectNo,case when Isnull(fmr.FDID,0)>0  then 1 else 0 end as IsFmDefected " +
           " FROM DBO.MRV_FlowMeters fm " +
           " left join dbo.MRV_Voyage_Activity_FlowMeterReadings fmr on fmr.VesselCode = fm.VesselCode And fmr.FlowMeterId = fm.FlowMeterId and fmr.TableID = " + TableId+" AND VOYAGEID = "+VoyageID+" " +
           " left join dbo.MRV_FuelTypes ft on ft.FuelTypeId = fmr.FuelTypeId " +
           " left join dbo.MRV_FlowMeterDefectReporting fdr on fmr.VesselCode = fdr.VesselCode  And fmr.FDID = fdr.FDID " +
           " Where fm.VesselCode ='"+VesselCode+"' And fmr.FuelPassed is not null ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptDetails.DataSource = dt;
        rptDetails.DataBind();

        if (dt.Rows.Count > 0)
        {
            dvFMReadings.Visible = true;
            DivFuelConsumption_fm.Visible = (ActivityId != 10);            
        }
        else
        {
            dvFMReadings.Visible = false;
            DivFuelConsumption_fm.Visible = false;
        }
        
    }
    
    public void BindFuelConsumption_fm()
    {
        string sql = " Select Row_Number()over(order by FT.FuelTypeName)Sr, FT.FuelTypeName,Sum(FuelConsumed)as FuelConsumed,Sum(Co2Emission)as Co2Emission  " +
                        "  from dbo.MRV_VoyageActivity_Sources VAS inner " +
                        "  join dbo.MRV_FuelTypes FT on FT.FuelTypeId = VAS.FuelTypeId " +
                        "  Where vesselCode = '" + VesselCode + "' and VoyageID = " + VoyageID + " and vas.TableId=" + TableId +
                        "  group by FuelTypeName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFuelConsumption_FM.DataSource = dt;
        rptFuelConsumption_FM.DataBind();
        rptFuelConsumption_FM.Visible = (dt.Rows.Count > 0);

    }
    public void BindFuelConsumption_tank()
    {
        string sql = " Select Row_Number()over(order by FT.FuelTypeName)Sr, FT.FuelTypeName,Sum(FuelConsumed)as FuelConsumed,Sum(Co2Emission)as Co2Emission  " +
                        "  from dbo.MRV_Voyage_Activity_Tank_Consumption VAS inner " +
                        "  join dbo.MRV_FuelTypes FT on FT.FuelTypeId = VAS.FuelTypeId " +
                        "  Where vesselCode = '" + VesselCode + "' and VoyageID = " + VoyageID + " and vas.TableId=" + TableId +
                        "  group by FuelTypeName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFuelConsumption_Tanks.DataSource = dt;
        rptFuelConsumption_Tanks.DataBind();
        rptFuelConsumption_Tanks.Visible = (dt.Rows.Count > 0);

    }

    
    public void BindFuelTankReadingReport()
    {

        string sql = " Exec MRV_GetFuelTank_Activity " + TableId + ",'" + VesselCode + "'," + VoyageID + " ";
        sql = " SELECT fmr.TableID,ft.VesselCode,ft.FuelTankID,ft.FuelTankName,fty.FuelTypeId,fty.FuelTypeName, " +
              " fmr.STARTMT,fmr.Sounding,fmr.Ullage,fmr.LevelGauge,fmr.Volume,fmr.Temp,fmr.SG,fmr.CorrectSG,fmr.ENDMT " +
              " FROM DBO.MRV_FuelTank ft " +
              " left join dbo.MRV_Voyage_Activity_FuelReadings_Tanks fmr on fmr.VesselCode = ft.VesselCode And fmr.FuelTankID = ft.FuelTankID and fmr.TableID =" + TableId + " AND VOYAGEID ="+ VoyageID + " " +
              " left join dbo.MRV_FuelTypes fty on fty.FuelTypeId = fmr.FuelTypeId " +
              " Where ft.VesselCode = '"+ VesselCode + "'  and fmr.ENDMT is not null ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        rptFuleTankReadingReport.DataSource = dt;
        rptFuleTankReadingReport.DataBind();
        if (dt.Rows.Count > 0)
        {
            divFuelTankReadingReport.Visible = true;
            DivFuelConsumption_Tanks.Visible = (ActivityId!=10);
        }
        else
        {
            divFuelTankReadingReport.Visible = false;
            DivFuelConsumption_Tanks.Visible = false;
        }
    }
    public void BindBunkeringReport()
    {
        string sql = " SELECT fmr.TableID,ft.VesselCode ,ft.FuelTankID,ft.FuelTankName,fty.FuelTypeId,fty.FuelTypeName, "+
                     "   fmr.STARTMT,fmr.Received,fmr.ENDMT,fmr.DensityAt15Deg " +
                     "   FROM DBO.MRV_FuelTank ft " +
                     "   left join dbo.MRV_Voyage_Activity_Bunkers_Tanks fmr on fmr.VesselCode = ft.VesselCode And fmr.FuelTankID = ft.FuelTankID and fmr.TableID =" + TableId+" AND VOYAGEID ="+VoyageID+" " +
                     "   left join dbo.MRV_FuelTypes fty on fty.FuelTypeId = fmr.FuelTypeId " +
                     "   Where ft.VesselCode ='"+VesselCode+"' and fmr.STARTMT is not null ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        rptBunkeringReport.DataSource = dt;
        rptBunkeringReport.DataBind();
        if (dt.Rows.Count > 0)
        {
            divBunkeringReport.Visible = true;
            DivFuelConsumption_Tanks.Visible = (ActivityId != 10);
        }
        else
        {
            divBunkeringReport.Visible = false;
            DivFuelConsumption_Tanks.Visible = false;
        }
    }

}
