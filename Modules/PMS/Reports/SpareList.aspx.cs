using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Reports_SpareList : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            if (Session["UserType"].ToString() == "S")
            {
                ddlVessels.Items.Insert(0, new ListItem("< SELECT >", "0"));
                ddlVessels.Items.Insert(1, new ListItem(Session["CurrentShip"].ToString(), Session["CurrentShip"].ToString()));
                ddlVessels.SelectedIndex = 1;
                ddlVessels.Visible = false;
                tdVessel.Visible = false;
            }
            else
            {
                tdVessel.Visible = true;
                BindVessels();
            }
        }
        ShowReport();
    }

    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE VESSELSTATUSID=1 AND EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode )  and vesselstatusid=1 ORDER BY VesselName ";
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
       ddlVessels.Items.Insert(0, "< SELECT VESSEL >");
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedItem.Text.Trim() == "< SELECT VESSEL >")
        {
            lblMessage.Text = "Please select a vessel.";
            ddlVessels.Focus();
            return;
        }
        //if (txtCompCode.Text.Trim() == "")
        //{
        //    lblMessage.Text = "Please enter component code.";
        //    txtCompCode.Focus();
        //    return;
          
        //}

        ShowReport();
    }
    protected void ShowReport()
    {
        string strSQL = "";
        string WhereCond = "";
        string WhereCondSpare = "";

        if (chkSpare.Checked)
        {
            WhereCondSpare = WhereCondSpare + " AND VSM.Critical = 'C' ";
        }
        
        if (chkMinStock.Checked)
        {
            WhereCondSpare = WhereCondSpare + " AND (dbo.getROB(VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,getdate()) < VSM.MinQty) ";
        }

        if (Session["UserType"].ToString() == "S")
        {
            strSQL = "SELECT CM.ComponentCode,(select ShipName from Settings where ShipCode=VSM.VesselCode)VesselName,CM.ComponentName,VSM.*,'' As LinkedTo,dbo.getROB(VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,getdate()) AS ROB,(SELECT TOP 1 StockLocation FROM VSL_StockInventory VSI WHERE VSI.VesselCode = VSM.VesselCode AND VSI.ComponentId = VSM.ComponentId AND VSI.Office_Ship = VSM.Office_Ship AND VSI.SpareId = VSM.SpareId ORDER BY VSI.UpdatedOn DESC ) AS StockLocation,CM.CriticalType FROM VSL_VesselSpareMaster VSM " +
                     "INNER JOIN ComponentMaster CM ON VSM.ComponentId = CM.ComponentId " +
                     "INNER JOIN VSL_ComponentMasterForVessel VCMV ON VCMV.VESSELCODE=VSM.VesselCode AND VCMV.COMPONENTID=VSM.ComponentId AND VCMV.STATUS='A' " +
                     "WHERE VSM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND VSM.Status = 'A' ";

        }
        else
        {
            strSQL = "SELECT CM.ComponentCode,(select VesselName from Vessel where VesselCode=VSM.VesselCode)VesselName,CM.ComponentName,VSM.*,'' As LinkedTo,dbo.getROB(VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,getdate()) AS ROB,(SELECT TOP 1 StockLocation FROM VSL_StockInventory VSI WHERE VSI.VesselCode = VSM.VesselCode AND VSI.ComponentId = VSM.ComponentId AND VSI.Office_Ship = VSM.Office_Ship AND VSI.SpareId = VSM.SpareId ORDER BY VSI.UpdatedOn DESC ) AS StockLocation,CM.CriticalType " +
		     "FROM ComponentMaster CM INNER JOIN VSL_ComponentMasterForVessel VCMV ON VCMV.VESSELCODE='" + ddlVessels.SelectedValue.ToString().Trim() + "' AND VCMV.COMPONENTID=CM.ComponentId AND VCMV.STATUS='A' left JOIN VSL_VesselSpareMaster VSM on VSM.VESSELCODE=VCMV.VESSELCODE AND VSM.COMPONENTID=VCMV.ComponentId  AND VCMV.STATUS='A' " + WhereCondSpare + " WHERE 1=1 " ;

		     //"FROM VSL_VesselSpareMaster VSM " +
                     //"right JOIN ComponentMaster CM ON VSM.ComponentId = CM.ComponentId " +
                     //"INNER JOIN VSL_ComponentMasterForVessel VCMV ON VCMV.VESSELCODE='" + ddlVessels.SelectedValue.ToString().Trim() + "' AND VCMV.COMPONENTID=CM.ComponentId AND VCMV.STATUS='A' " +
                     //"WHERE VSM.VesselCode = '" + ddlVessels.SelectedValue.ToString().Trim() + "' AND isnull(VSM.Status,'A') = 'A' ";

        }
        if (txtCompCode.Text.Trim() != "")
        {
            WhereCond = WhereCond + "AND LEFT(CM.ComponentCode, LEN('" + txtCompCode.Text.Trim() + "')) = '" + txtCompCode.Text.Trim() + "' ";
        }
        if (chkClass.Checked)
        {
            WhereCond = WhereCond + " AND VCMV.ClassEquip = 1 ";
        }

        if (chkCritical.Checked)
        {
            WhereCond = WhereCond + " AND CM.CriticalEquip = 1 ";
        }
        

        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL + WhereCond);
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("SpareListReport.rpt"));
        rpt.SetDataSource(dtReport);
        
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
