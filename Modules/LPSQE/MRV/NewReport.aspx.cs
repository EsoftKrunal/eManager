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

public partial class MRV_New_Report : System.Web.UI.Page
{
    public int FuelTypeId
    {
        get { return Common.CastAsInt32(ViewState["FuelTypeId"]);}
        set { ViewState["FuelTypeId"] = value; }

    }
    public int ReportID
    {
        get { return Common.CastAsInt32(ViewState["_ReportID"]); }
        set { ViewState["_ReportID"] = value; }

    }
    public int ChangeID
    {
        get { return Common.CastAsInt32(ViewState["_ChangeID"]); }
        set { ViewState["_ChangeID"] = value; }

    }
    public int VoyageID
    {
        get { return Common.CastAsInt32(ViewState["_VoyageID"]); }
        set { ViewState["_VoyageID"] = value; }

    }
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value.ToString(); }
    }
    public bool RecordVerified
    {
        get { return Convert.ToBoolean( ViewState["_RecordVerified"]); }
        set { ViewState["_RecordVerified"] = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------------
        ProjectCommon.SessionCheck();
        //------------------------------------
        Session["MM"] = 2;
        
        if (!IsPostBack)
        {
            VesselCode = Page.Request.QueryString["VC"].ToString();
            VoyageID = 0;
            FuelTypeId = 0;
            ChangeID = 0;
            ReportID = Common.CastAsInt32(Page.Request.QueryString["ReportID"]);
            ShowCurrentVoyage();
            BindFinalConsumption();
            if (ReportID > 0)
            {
                ShowMasterData();
            }
        }
    }
    
    
    
    public void BindFuelChanges()
    {        
        string sql = " SELECT * FROM dbo.MRV_Reports_FuelChange fc Where fc.VesselCode = '" + VesselCode + "' and fc.ReportId=" + ReportID + " order by ChangeTime desc";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptReadings.DataSource = dt;
        rptReadings.DataBind();
    }
    public DataTable GetFuelChangeSection(object _ChangeId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.MRV_GetFuelChange '" + VesselCode + "'," + ReportID + "," + _ChangeId);
        return dt;

    }
    
    public void BindFinalConsumption()
    {
        string sql = " Select  ES.EmissionSourceId,ES.EmissionSourceName,FT.FuelTypeName,ReportID,FuelConsumed  from dbo.MRV_Reports_Sources RS " +
                   " inner " +
                   " join MRV_EmissionSource ES on RS.EmissionSourceId = ES.EmissionSourceId " +
                   " inner " +
                   " join MRV_FuelTypes FT on FT.FuelTypeId = RS.FuelTypeId " +
                   " Where ReportID =" + ReportID;

        sql = " SElect SR.FuelTypeId,FT.FuelTypeName,FT.Co2EmissionPerMT,sum(FuelConsumed)FuelConsumed ,sum(FuelConsumed)*(FT.Co2EmissionPerMT) as CO2 "+
              "  from dbo.MRV_Reports_Sources SR " +
              "  inner " +
              "  join dbo.MRV_FuelTypes FT on FT.FuelTypeId = SR.FuelTypeId " +
              "  Where ReportId = "+ ReportID + " and VesselCode = '"+VesselCode+"' " +
              "  Group by SR.FuelTypeId,FT.FuelTypeName,FT.Co2EmissionPerMT ";
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFinalConsumption.DataSource = dt;
        rptFinalConsumption.DataBind();
    }
    public void ShowMasterData()
    {
        string sql = " SElect * from dbo.MRV_Reports Where ReportId=" + ReportID;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];

            txtReportDate.Text = FormatDateOnly(dt.Rows[0]["ReportDate"]);
            txtReportDate.Enabled = false;
            txtReportDate.CssClass = txtReportDate.CssClass.Replace("input ", "input_readonly ");
            

            string[] EtaToPort = FormatDate(dt.Rows[0]["ETAToPort"]).Split(' ');
            txtETAToPortDate.Text = EtaToPort[0];
            txtETAToPortTime.Text = EtaToPort[1];

            
            lblLocation.Text = (dt.Rows[0]["Location"].ToString() == "1") ? "At Sea" : "In Port";

            lblSteamingHours.Text = Common.CastAsInt32( dt.Rows[0]["SteamingHrs"]).ToString("00");

            
            lblSteamingMin.Text = Common.CastAsInt32(dt.Rows[0]["SteamingMin"]).ToString("00");

            txtDMG.Text = dt.Rows[0]["DistanceMadeGoods"].ToString();
            txtDraftFwd.Text = dt.Rows[0]["DraftFwd"].ToString();
            txtDraftAfter.Text = dt.Rows[0]["DraftAfter"].ToString();
            txtDraftMid.Text = dt.Rows[0]["DraftMid"].ToString();
            txtMaterName.Text = dt.Rows[0]["MasterName"].ToString();
            txtChiefEngineerName.Text = dt.Rows[0]["ChiefEngineer"].ToString();

            if (Dr["VerifiedOn"].ToString() != "")
            {
                lblVerifiedByOn.Text =  Dr["CEName"].ToString() + " / " + Common.ToDateString(Dr["VerifiedOn"]);
                RecordVerified = true;
                
                
                
            }
            else
            {
                RecordVerified = false;
                
                
            }

            dvReadings.Visible = true;
            BindFuelChanges();
            BindFinalConsumption();
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
    
    
    
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fsadfa", "closeWindow();", true);
    }
    public void ShowCurrentVoyage()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.MRV_Voyage where Status='O' Order BY VoyageId desc");
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];

            VoyageID = Common.CastAsInt32(Dr["VoyageId"].ToString());
            lblVoyageNo.Text = Dr["VoyageNo"].ToString();
            lblFromPort.Text = Dr["FromPort"].ToString();
            lblToPort.Text = Dr["ToPort"].ToString();
            lblStartDate.Text = Common.ToDateString(Dr["StartDate"]);

            
            //lblEtaToPort.Text = FormatDate(Dr["ETA"].ToString());
            //lblCondition.Text = (Dr["Condition"].ToString() == "B") ? "Ballast" : "Laden";

        }
    }    
    

    
}
