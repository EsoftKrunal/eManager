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


public partial class Reports_OfficeComponentReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        string OfficeWhereClause = "Where 1=1 ", ShipWhereClause = "", Ship_OuterWhereClause = "";
        if (chkClass.Checked)
        {
            OfficeWhereClause += "And ClassEquip=1 ";
            ShipWhereClause = "And CMV.ClassEquip=1 ";
            Ship_OuterWhereClause = "And CMV2.CLASSEQUIP=1 ";
            //---------------------------
            if(txtCCode.Text.Trim()!="" && txtCCode.Visible==true)
            {
                ShipWhereClause += "And CMV.ClassEquipCode ='" + txtCCode.Text.Trim() + "' ";
            }
        }
        if (chkCritical.Checked)
        {
            OfficeWhereClause += "And CriticalEquip=1 ";
            ShipWhereClause += "And CM.CriticalEquip=1 ";
        }
        if (Request.QueryString["Mode"] == "Office" )
        {
            txtCCode.Text = "";
            txtCCode.Visible = false;

            string strComponentdetails = "SELECT ComponentCode,ComponentName,CriticalEquip,CriticalType,'' as Descr FROM ComponentMaster " + OfficeWhereClause;
            DataTable dtComponent = Common.Execute_Procedures_Select_ByQuery(strComponentdetails);

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("Off_ComponentList.rpt"));

            rpt.SetDataSource(dtComponent);
            rpt.SetParameterValue("Header", "Office Component Master");
            rpt.SetParameterValue("Text1", "Office Component Master");
        }
        if (Request.QueryString["Mode"] == "Vessel" )
        {
            string strComponentdetails = "SELECT MAIN.ComponentId,MAIN.ComponentCode,MAIN.ComponentName,MAIN.CriticalEquip,Main.CriticalType,CMV2.ClassEquipCode AS Descr, CMV2.[Maker], CMV2.[MakerType], CMV2.[Descr] AS Descr1  FROM " +
                                        "( " +
                                        "    SELECT DISTINCT ComponentId,ComponentCode,ComponentName,CriticalEquip,CriticalType " +
	                                    "    FROM " +
	                                    "    ( " +
                                        "    SELECT CM2.ComponentId,CM2.ComponentCode,CM2.ComponentName,CM2.CriticalEquip,CM2.CriticalType " +
	                                    "    FROM ComponentMaster CM  " +
	                                    "    INNER JOIN ComponentMasterForVessel CMV ON CM.ComponentId = CMV.ComponentId " +
	                                    "    INNER JOIN COMPONENTMASTER CM2 ON LEN(LTRIM(RTRIM(CM2.COMPONENTCODE)))<=LEN(LTRIM(RTRIM(CM.ComponentCode))) AND LTRIM(RTRIM(CM.ComponentCode)) LIKE LTRIM(RTRIM(CM2.COMPONENTCODE)) + '%' " +
                                        "    WHERE CMV.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "' " + ShipWhereClause + " " +
	                                    "    ) AA " +
                                        ") MAIN INNER JOIN ComponentMasterForVessel CMV2 ON MAIN.ComponentId = CMV2.ComponentId And CMV2.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "' " + Ship_OuterWhereClause + " " +
                                        "ORDER by MAIN.ComponentCode ";
            //string strComponentdetails = "SELECT CM.ComponentCode,CM.ComponentName,CM.CriticalEquip,CMV.ClassEquipCode as Descr FROM ComponentMaster CM INNER JOIN ComponentMasterForVessel CMV ON CM.ComponentId = CMV.ComponentId " +
            //                             "WHERE CMV.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "'" + ShipWhereClause;
            DataTable dtComponent = Common.Execute_Procedures_Select_ByQuery(strComponentdetails);

            CrystalReportViewer1.ReportSource = rpt;
            if (ddlReportType.SelectedValue.Trim() == "1")
            {
                rpt.Load(Server.MapPath("Off_ComponentList.rpt"));
            }
            else
            {
                rpt.Load(Server.MapPath("Off_ComponentDetailsList.rpt"));
            }

            rpt.SetDataSource(dtComponent);
            rpt.SetParameterValue("Header", "Vessel Component Master");
            rpt.SetParameterValue("Text1", "Vessel : " + Request.QueryString["VesselName"].ToString());
        }
        if (Request.QueryString["Mode"] == "Ship")
        {
            string strComponentdetails = "SELECT MAIN.ComponentId,MAIN.ComponentCode,MAIN.ComponentName,MAIN.CriticalEquip,Main.CriticalType,CMV2.ClassEquipCode AS Descr, CMV2.[Maker], CMV2.[MakerType], CMV2.[Descr] AS Descr1  FROM " +
                                      "( " +
                                      "    SELECT DISTINCT ComponentId,ComponentCode,ComponentName,CriticalEquip,CriticalType " +
                                      "    FROM " +
                                      "    ( " +
                                      "    SELECT CM2.ComponentId,CM2.ComponentCode,CM2.ComponentName,CM2.CriticalEquip,CM2.CriticalType " +
                                      "    FROM ComponentMaster CM  " +
                                      "    INNER JOIN VSL_ComponentMasterForVessel CMV ON CM.ComponentId = CMV.ComponentId " +
                                      "    INNER JOIN COMPONENTMASTER CM2 ON LEN(LTRIM(RTRIM(CM2.COMPONENTCODE)))<=LEN(LTRIM(RTRIM(CM.ComponentCode))) AND LTRIM(RTRIM(CM.ComponentCode)) LIKE LTRIM(RTRIM(CM2.COMPONENTCODE)) + '%' " +
                                      "    WHERE CMV.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "' " + ShipWhereClause + " " +
                                      "    ) AA " +
                                      ") MAIN INNER JOIN VSL_ComponentMasterForVessel CMV2 ON MAIN.ComponentId = CMV2.ComponentId And CMV2.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "' " + Ship_OuterWhereClause + " " +
                                      "ORDER by MAIN.ComponentCode ";
            
            //string strComponentdetails = "SELECT CM.ComponentCode,CM.ComponentName,CM.CriticalEquip,CMV.ClassEquipCode as Descr FROM ComponentMaster CM INNER JOIN VSL_ComponentMasterForVessel CMV ON CM.ComponentId = CMV.ComponentId " +
            //                             "WHERE CMV.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "'" + ShipWhereClause;
            DataTable dtComponent = Common.Execute_Procedures_Select_ByQuery(strComponentdetails);

            CrystalReportViewer1.ReportSource = rpt;
            
            if (ddlReportType.SelectedValue.Trim() == "1")
            {
                rpt.Load(Server.MapPath("Off_ComponentList.rpt"));
            }
            else
            {
                rpt.Load(Server.MapPath("Off_ComponentDetailsList.rpt"));
            }

            rpt.SetDataSource(dtComponent);
            rpt.SetParameterValue("Header", "Vessel Component Master");
            rpt.SetParameterValue("Text1", "Vessel : " + GetVesselNameByCode(Request.QueryString["Vessel"].ToString()));
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

    public string GetVesselNameByCode(string VelCode)
    {
        string sql = "select ShipName from Settings where ShipCode='" + VelCode + "'";
        DataTable dtVessName = Common.Execute_Procedures_Select_ByQuery(sql);
        return dtVessName.Rows[0][0].ToString();
    }
}
