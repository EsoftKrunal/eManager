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
public partial class MRV_BunkerReport : System.Web.UI.Page
{
    public int FuelTypeId
    {
        get { return Common.CastAsInt32(ViewState["FuelTypeId"]);}
        set { ViewState["FuelTypeId"] = value; }

    }
    public int BunkerID
    {
        get { return Common.CastAsInt32(ViewState["_BunkerID"]); }
        set { ViewState["_BunkerID"] = value; }

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
        get { return Convert.ToBoolean(ViewState["_RecordVerified"]); }
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
            VoyageID = 0;
            FuelTypeId = 0;

            VesselCode = Request.QueryString["VC"].ToString();
            BunkerID = Common.CastAsInt32(Page.Request.QueryString["BunkerID"]);
            ShowCurrentVoyage();
            if (BunkerID > 0)
            {
                
                ShowMasterData();
            }
            BindFuelTankReading();
            
        }
    }

    public void BindFuelTankReading()
    {
        string sql = " Select * from MRV_FuelTank	 ";
        sql = " Select Ta.FuelTankId,Ta.FuelTankName,ty.FuelTypeName from MRV_FuelTank Ta left join MRV_FuelTypes ty on ty.FuelTypeId=Ta.FuelTypeId ";
        sql = "  Select Ta.FuelTankId,Ta.FuelTankName,Ta.Capacity,Ta.MeasuringDevice " +
             "  ,ty.FuelTypeName ,BT.Received,BT.DensityAt15Deg " +
             "  from dbo.MRV_FuelTank Ta " +
             "  left join dbo.MRV_FuelTypes ty on ty.FuelTypeId = Ta.FuelTypeId " +
             "  left join dbo.MRV_Bunkers_Tanks BT on BT.FuelTankID = Ta.FuelTankID and BunkerId =" + BunkerID;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

        rptBunkering.DataSource = dt;
        rptBunkering.DataBind();
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
    public void ShowMasterData()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(" Select * from dbo.MRV_Bunkers Where BunkerId=" + BunkerID);
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];
            

            string[] VoyageDateTime = FormatDate(dt.Rows[0]["BunkerDate"]).Split(' ');
            txtReportDate.Text = VoyageDateTime[0];
            txtReportTime.Text = VoyageDateTime[1];
            txtLocation.Text = Dr["BunkerLocation"].ToString();
            txtMaterName.Text = Dr["MasterName"].ToString();
            txtChiefEngineerName.Text = Dr["ChiefEngineer"].ToString();

            if (Dr["VerifiedOn"].ToString() != "")
            {
                lblVerifiedByOn.Text = Dr["ChiefEngineer"].ToString() + " / " + Common.ToDateString(Dr["VerifiedOn"]);
                RecordVerified = true;
                //btnAddChange.Visible = false;
                

            }
            else
            {
                RecordVerified = false;
                

            }
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

    //public string GetFuelConsumptionString(object _key)
    //{
    //    StringBuilder res = new StringBuilder();
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FlowMeterName,* FROM DBO.MRV_EmissionSource_FlowMeters_Calc  C inner join DBO.MRV_FlowMeters FM ON C.FlowMeterId=FM.FlowMeterId WHERE EmissionSourceId=" + _key + " order by multiple desc");
    //    if (dt.Rows.Count <= 0)
    //    {
    //        dt = Common.Execute_Procedures_Select_ByQuery("SELECT calccomments FROM DBO.MRV_EmissionSource WHERE EmissionSourceId=" + _key);
    //        if (Convert.IsDBNull(dt.Rows[0][0]))
    //            res.Append("<b style='color:Red'>Sorry ! No calculation method exists for this emission source.</b>");
    //        else
    //            res.Append("<b style='color:Red'>" + dt.Rows[0][0].ToString() + "</b>");
    //    }
    //    else
    //    {
    //        DataView dv = dt.DefaultView;
    //        dv.RowFilter = "Multiple=1";
    //        DataTable dtplus = dv.ToTable();

    //        if (dtplus.Rows.Count > 0)
    //            res.Append("( ");
    //        for (int i = 0; i <= dtplus.Rows.Count - 1; i++)
    //        {
    //            if (i != 0) { res.Append(" + "); }
    //            res.Append(dtplus.Rows[i]["FlowMeterName"]);
    //        }
    //        if (dtplus.Rows.Count > 0)
    //            res.Append(" )");


    //        dv.RowFilter = "Multiple=-1";
    //        DataTable dtminus = dv.ToTable();

    //        if (dtminus.Rows.Count > 0)
    //            res.Append(" - ( ");
    //        for (int i = 0; i <= dtminus.Rows.Count - 1; i++)
    //        {
    //            if (i != 0) { res.Append(" + "); }
    //            res.Append(dtminus.Rows[i]["FlowMeterName"]);
    //        }
    //        if (dtminus.Rows.Count > 0)
    //            res.Append(" )");
    //    }

    //    return res.ToString();
    //}
    //--------------------------------------------------------------------------------------------------------

    
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fsadfa", "window.close();", true);
    }
    
    
    


    
    
}
