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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;

public partial class Reports_PositionReport : System.Web.UI.Page
{
    Authority Auth;
    string ReportId = "";
    string VSLCode = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument dep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        try
        {
            VSLCode = "" + Page.Request.QueryString["VSLCode"].ToString();
            if (Page.Request.QueryString["ReportId"].ToString() != "")
                ReportId = Page.Request.QueryString["ReportId"].ToString();
            
        }
        catch { }
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 223);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        Show_Report();
    }
    private string getPortName(string PortId)
    {
        string Query = "select PortName from MTMVslReporting.Dbo.Port Where PortId=" + PortId;
        DataSet ds = VesselReporting.getTable(Query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["PortName"].ToString();
        }
        else
        {
            return "";
        }
    }
    private string getVesselName(string VesselCode)
    {
        string Query = "select VesselName from Dbo.Vessel Where VesselCode='" + VesselCode + "'";
        DataSet ds = VesselReporting.getTable(Query);
        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0]["VesselName"].ToString();
        }
        else
        {
            return "";
        }
    }
    private void Show_Report()
    {
        try
        {
            string Query = "SELECT * FROM ReportsMaster Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
            CrystalReportViewer1.HasPrintButton = Auth.isPrint;   
            DataSet ds=VesselReporting.getTable(Query);
            DataRow drMain = ds.Tables[0].Rows[0];
            DateTime dtMain = Convert.ToDateTime(drMain["ReportTime"].ToString());
            string ReportType = drMain["ReportType"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = dep;

                if (ReportType == "D")
                {
                    dep.Load(Server.MapPath("Departure.rpt"));
                    dep.SetParameterValue("VSLName", getVesselName(drMain["VslCode"].ToString()));

                    #region ------------- DEPARTRUE REPORT --------------
                    Query = " SELECT * FROM VoyageInfo Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    ds = VesselReporting.getTable(Query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("ReportHeader", "Departure Report   from   " + getPortName(dr["DI_FromPort"].ToString()) + " - " + DateTime.Parse(dr["DI_DepDate"].ToString()).ToString("dd-MMM-yyyy"));
                        dep.SetParameterValue("NextPort", ": " + getPortName(dr["DI_ToPort"].ToString()));
                        dep.SetParameterValue("VoyageNo", ": " + dr["VoyageNo"].ToString());

                        if (dr["ENOA"].ToString().Substring(0, 1) == "Y")
                        {
                            dep.SetParameterValue("ENOAHeader", "USCG eNOA Sent");
                            dep.SetParameterValue("ENOAValue", ": " + ((dr["ENOA"].ToString().Substring(1, 1) == "Y") ? "Yes" : "No"));
                        }
                        else
                        {
                            dep.SetParameterValue("ENOAHeader", "");
                            dep.SetParameterValue("ENOAValue", "");
                        }

                        if (dr["AnyRestArea"].ToString().Trim().ToUpper() == "YES")
                        {
                            string area = "";
                            dep.SetParameterValue("FRAHeader", "Transiting to Restricted Area ");
                            if (dr["AreaName1"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + "ECA 1.0 S";
                            }
                            if (dr["AreaName2"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + ((area.Trim() == "") ? "" : ", ") + "CA 0.5 S";
                            }
                            if (dr["AreaName3"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + ((area.Trim() == "") ? "" : ", ") + "EU 0.1 S";
                            }
                            DateTime dt = DateTime.Parse(dr["ETADate"].ToString());
                            dep.SetParameterValue("FRA", ": " + area + "  :  " + dt.ToString("dd-MMM-yyyy"));
                        }
                        else
                        {
                            dep.SetParameterValue("FRAHeader", "");
                            dep.SetParameterValue("FRA", "");
                        }
                        if (dr["eNOA"].ToString().StartsWith("Y"))
                        {
                            dep.SetParameterValue("USCGHeader", "USCG Notification Sent");
                            dep.SetParameterValue("USCGValue", ": " + ((dr["CharterName"].ToString().Substring(1, 1) == "Y") ? "Yes" : "No"));
                        }
                        else
                        {
                            dep.SetParameterValue("USCGHeader", "");
                            dep.SetParameterValue("USCGValue", "");
                        }
                        dep.SetParameterValue("CHName", ": " + dr["CharterName"].ToString());
                        dep.SetParameterValue("CPS", ": " + dr["PartySpeed"].ToString() + " KTS");
                        dep.SetParameterValue("VOS", ": " + dr["OrderSpeed"].ToString() + " KTS");
                        dep.SetParameterValue("VI", dr["VoyInstructions"].ToString());
                        dep.SetParameterValue("VoyCon", ": " + dr["DI_VoyCondition"].ToString());

                        dep.SetParameterValue("CospDateTime", ": " + dr["DI_DepDate"].ToString() + " Hrs");
                        dep.SetParameterValue("ZoneTime", ": " + dr["DI_Zone"].ToString());
                        dep.SetParameterValue("NextPortETA", ": " + dr["DI_ETA"].ToString() + " Hrs");
                        dep.SetParameterValue("DraftFwd", ": " + dr["DI_Fwd"].ToString() + " Mtrs");
                        dep.SetParameterValue("DraftAft", ": " + dr["DI_Aft"].ToString() + " Mtrs");
                        dep.SetParameterValue("DTG", ": " + dr["DI_DTG"].ToString() + " NM");

                        dep.SetParameterValue("AgentName", ": " + dr["DI_AgentName"].ToString());
                        dep.SetParameterValue("PersonInch", ": " + dr["DI_IncName"].ToString());
                        dep.SetParameterValue("Address", ": " + dr["DI_Address"].ToString());
                        dep.SetParameterValue("BussPhone", ": " + dr["DI_BussPhone"].ToString() + ((dr["DI_BussPhone"].ToString().Trim() == "") ? "" : ", ") + dr["DI_BussPhone1"].ToString());
                        dep.SetParameterValue("Mobile", ": " + dr["DI_Mobile"].ToString() + ((dr["DI_Mobile"].ToString().Trim() == "") ? "" : ", ") + dr["DI_Mobile1"].ToString());
                        dep.SetParameterValue("Fax", ": " + dr["DI_Fax"].ToString() + ((dr["DI_Fax"].ToString().Trim() == "") ? "" : ", ") + dr["DI_Fax1"].ToString());
                        dep.SetParameterValue("Email", ": " + dr["DI_Email"].ToString() + ((dr["DI_Email"].ToString().Trim() == "") ? "" : ", ") + dr["DI_Email1"].ToString());
                    }
                    else
                    {
                        dep.SetParameterValue("ReportHeader", ": " + "");
                        dep.SetParameterValue("FromToPort", ": " + "");
                        dep.SetParameterValue("VoyageNo", ": " + "");
                        dep.SetParameterValue("FRA", ": " + "");
                        dep.SetParameterValue("CHName", ": " + "");
                        dep.SetParameterValue("CPS", ": " + "");
                        dep.SetParameterValue("VOS", ": " + "");
                        dep.SetParameterValue("VI", ": " + "");
                        dep.SetParameterValue("VoyCon", ": " + "");

                        dep.SetParameterValue("CospDateTime", ": " + "");
                        dep.SetParameterValue("ZoneTime", ": " + "");
                        dep.SetParameterValue("NextPortETA", ": " + "");
                        dep.SetParameterValue("DraftFwd", ": " + "");
                        dep.SetParameterValue("DraftAft", ": " + "");
                        dep.SetParameterValue("DTG", ": " + "");


                        dep.SetParameterValue("AgentName", ": " + "");
                        dep.SetParameterValue("PersonInch", ": " + "");
                        dep.SetParameterValue("Address", ": " + "");
                        dep.SetParameterValue("BussPhone", ": " + "");
                        dep.SetParameterValue("Mobile", ": " + "");
                        dep.SetParameterValue("Fax", ": " + "");
                        dep.SetParameterValue("Email", ": " + "");
                    }
                    Query = "SELECT * FROM ShipPosition Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    ds = VesselReporting.getTable(Query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        char[] sep = { ',' };
                        string[] Lats = dr["lattitude"].ToString().Split(sep);
                        string[] Longs = dr["Logitude"].ToString().Split(sep);
                        dep.SetParameterValue("Latt", ": " + Lats[0] + "° " + Lats[1] + "' " + Lats[2]);
                        dep.SetParameterValue("Long", ": " + Longs[0] + "° " + Longs[1] + "' " + Longs[2]);
                        dep.SetParameterValue("LocDesc", ": " + dr["Descr"].ToString());
                    }
                    else
                    {
                        dep.SetParameterValue("Latt", ": " + "");
                        dep.SetParameterValue("Long", ": " + "");
                        dep.SetParameterValue("LocDesc", ": " + "");
                    }
                    Query = "SELECT * FROM FuelRecieved Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + " Order By RecType;";
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[1];
                        dep.SetParameterValue("Sulphure1", ": " + dr["IFO1"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure2", ": " + dr["IFO2"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure3", ": " + dr["MGO"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure4", ": " + dr["MGO1"].ToString() + " MT");
                        dep.SetParameterValue("MDO", ": " + dr["MDO"].ToString() + " MT");
                        dep.SetParameterValue("MECC", ": " + dr["MECC"].ToString() + " LTR");
                        dep.SetParameterValue("MECYL", ": " + dr["MRCYL"].ToString() + " LTR");
                        dep.SetParameterValue("AECC", ": " + dr["AECC"].ToString() + " LTR");
                        dep.SetParameterValue("HYD", ": " + dr["HYD"].ToString() + " LTR");
                        dep.SetParameterValue("FW", ": " + dr["FW"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[0];
                        string RankName = dr["RankId"].ToString().Trim();

                        RankName = (RankName == "1") ? "Master" : ((RankName == "2") ? "Chief Officer" : ((RankName == "3") ? "Chief Engineer" : ((RankName == "4") ? "First A/E" : "")));
                        dep.SetParameterValue("ROBVerification", ": " + dr["VerifiedBy"].ToString() + " / " + RankName);
                    }
                    else
                    {
                        dep.SetParameterValue("Sulphure1", ": " + "");
                        dep.SetParameterValue("Sulphure2", ": " + "");
                        dep.SetParameterValue("Sulphure3", ": " + "");
                        dep.SetParameterValue("Sulphure4", ": " + "");
                        dep.SetParameterValue("MDO", ": " + "");
                        dep.SetParameterValue("MECC", ": " + "");
                        dep.SetParameterValue("MECYL", ": " + "");
                        dep.SetParameterValue("AECC", ": " + "");
                        dep.SetParameterValue("HYD", ": " + "");
                        dep.SetParameterValue("FW", ": " + "");


                        dep.SetParameterValue("ROBVerification", "");
                    }
                    #endregion
                }
                else if (ReportType == "N")
                {
                    dep.Load(Server.MapPath("Noon.rpt"));
                    dep.SetParameterValue("VSLName", getVesselName(drMain["VslCode"].ToString()));

                    #region ------------- NOON REPORT --------------
                    Query = " SELECT * FROM VoyageInfo Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    ds = VesselReporting.getTable(Query);
                    string VoyageNo = "";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("ReportHeader", "Noon Report - " + dtMain.ToString("dd-MMM-yyyy"));
                        dep.SetParameterValue("FromToPort", getPortName(dr["DI_FromPort"].ToString()) + "  To  " + getPortName(dr["DI_ToPort"].ToString()));
                        dep.SetParameterValue("VoyageNo", ": " + dr["VoyageNo"].ToString());
                        VoyageNo = dr["VoyageNo"].ToString();
                        if (dr["AnyRestArea"].ToString().Trim().ToUpper() == "YES")
                        {
                            string area = "";
                            dep.SetParameterValue("FRAHeader", "Transiting to Restricted Area ");
                            if (dr["AreaName1"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + "ECA 1.0 S";
                            }
                            if (dr["AreaName2"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + ((area.Trim() == "") ? "" : ", ") + "CA 0.5 S";
                            }
                            if (dr["AreaName3"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + ((area.Trim() == "") ? "" : ", ") + "EU 0.1 S";
                            }
                            DateTime dt = DateTime.Parse(dr["ETADate"].ToString());
                            dep.SetParameterValue("FRA", ": " + area + "  :  " + dt.ToString("dd-MMM-yyyy"));
                        }
                        else
                        {
                            dep.SetParameterValue("FRAHeader", "");
                            dep.SetParameterValue("FRA", "");
                        }
                        if (dr["eNOA"].ToString().StartsWith("Y"))
                        {
                            dep.SetParameterValue("USCGHeader", "USCG Notification Sent");
                            dep.SetParameterValue("USCGValue", ": " + ((dr["CharterName"].ToString().Substring(1, 1) == "Y") ? "Yes" : "No"));
                        }
                        else
                        {
                            dep.SetParameterValue("USCGHeader", "");
                            dep.SetParameterValue("USCGValue", "");
                        }
                        dep.SetParameterValue("CHName", ": " + dr["CharterName"].ToString());
                        dep.SetParameterValue("CPS", ": " + dr["PartySpeed"].ToString() + " KTS");
                        dep.SetParameterValue("VOS", ": " + dr["OrderSpeed"].ToString() + " KTS");
                        dep.SetParameterValue("VI", dr["VoyInstructions"].ToString());
                        dep.SetParameterValue("VoyCon", ": " + dr["DI_VoyCondition"].ToString());

                        dep.SetParameterValue("CospDateTime", ": " + dr["DI_DepDate"].ToString() + " Hrs");
                        dep.SetParameterValue("ZoneTime", ": " + dr["DI_Zone"].ToString());
                        dep.SetParameterValue("NextPortETA", ": " + dr["DI_ETA"].ToString() + " Hrs(LT)");
                        dep.SetParameterValue("DraftFwd", ": " + dr["DI_Fwd"].ToString() + " Mtrs");
                        dep.SetParameterValue("DraftAft", ": " + dr["DI_Aft"].ToString() + " Mtrs");
                        dep.SetParameterValue("DTG", ": " + dr["DI_DTG"].ToString() + " NM");

                        dep.SetParameterValue("AgentName", ": " + dr["DI_AgentName"].ToString());
                        dep.SetParameterValue("PersonInch", ": " + dr["DI_IncName"].ToString());
                        dep.SetParameterValue("Address", ": " + dr["DI_Address"].ToString());
                        dep.SetParameterValue("BussPhone", ": " + dr["DI_BussPhone"].ToString() + ((dr["DI_BussPhone"].ToString().Trim() == "") ? "" : ", ") + dr["DI_BussPhone1"].ToString());
                        dep.SetParameterValue("Mobile", ": " + dr["DI_Mobile"].ToString() + ((dr["DI_Mobile"].ToString().Trim() == "") ? "" : ", ") + dr["DI_Mobile1"].ToString());
                        dep.SetParameterValue("Fax", ": " + dr["DI_Fax"].ToString() + ((dr["DI_Fax"].ToString().Trim() == "") ? "" : ", ") + dr["DI_Fax1"].ToString());
                        dep.SetParameterValue("Email", ": " + dr["DI_Email"].ToString() + ((dr["DI_Email"].ToString().Trim() == "") ? "" : ", ") + dr["DI_Email1"].ToString());
                    }
                    else
                    {
                        dep.SetParameterValue("ReportHeader", ": " + "");
                        dep.SetParameterValue("FromToPort", ": " + "");
                        dep.SetParameterValue("VoyageNo", ": " + "");
                        dep.SetParameterValue("FRA", ": " + "");
                        dep.SetParameterValue("CHName", ": " + "");
                        dep.SetParameterValue("CPS", ": " + "");
                        dep.SetParameterValue("VOS", ": " + "");
                        dep.SetParameterValue("VI", ": " + "");
                        dep.SetParameterValue("VoyCon", ": " + "");

                        dep.SetParameterValue("CospDateTime", ": " + "");
                        dep.SetParameterValue("ZoneTime", ": " + "");
                        dep.SetParameterValue("NextPortETA", ": " + "");
                        dep.SetParameterValue("DraftFwd", ": " + "");
                        dep.SetParameterValue("DraftAft", ": " + "");
                        dep.SetParameterValue("DTG", ": " + "");


                        dep.SetParameterValue("AgentName", ": " + "");
                        dep.SetParameterValue("PersonInch", ": " + "");
                        dep.SetParameterValue("Address", ": " + "");
                        dep.SetParameterValue("BussPhone", ": " + "");
                        dep.SetParameterValue("Mobile", ": " + "");
                        dep.SetParameterValue("Fax", ": " + "");
                        dep.SetParameterValue("Email", ": " + "");
                    }
                    Query = "SELECT * FROM ShipPosition Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    ds = VesselReporting.getTable(Query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        char[] sep = { ',' };
                        string[] Lats = dr["lattitude"].ToString().Split(sep);
                        string[] Longs = dr["Logitude"].ToString().Split(sep);
                        dep.SetParameterValue("Latt", ": " + Lats[0] + "° " + Lats[1] + "' " + Lats[2]);
                        dep.SetParameterValue("Long", ": " + Longs[0] + "° " + Longs[1] + "' " + Longs[2]);
                        dep.SetParameterValue("LocDesc", ": " + dr["Descr"].ToString());

                        dep.SetParameterValue("StmHrs", ": " + dr["SteamHrs"].ToString() + " Hrs");
                        dep.SetParameterValue("DMG", ": " + dr["DMG"].ToString() + " NM");
                        dep.SetParameterValue("AvgSpeed", ": " + dr["AvgSpeed"].ToString() + " KTS");

                        if (dr["StopPages"].ToString().Trim() != "")
                        {
                            dep.SetParameterValue("StopPagesHeader", "Stoppages");
                            dep.SetParameterValue("StopPagesValue", ": " + dr["StopPages"].ToString());
                            dep.SetParameterValue("StopPagesRemark", ": " + dr["STRemark"].ToString());
                        }
                        else
                        {
                            dep.SetParameterValue("StopPagesHeader", "");
                            dep.SetParameterValue("StopPagesValue", "");
                            dep.SetParameterValue("StopPagesRemark", "");
                        }
                    }
                    else
                    {
                        dep.SetParameterValue("Latt", ": " + "");
                        dep.SetParameterValue("Long", ": " + "");
                        dep.SetParameterValue("LocDesc", ": " + "");

                        dep.SetParameterValue("StmHrs", ": " + "");
                        dep.SetParameterValue("DMG", ": " + "");
                        dep.SetParameterValue("AvgSpeed", ": " + "");

                        dep.SetParameterValue("StopPagesHeader", "");
                        dep.SetParameterValue("StopPagesValue", "");
                        dep.SetParameterValue("StopPagesRemark", "");
                    }

                    Query = "SELECT * FROM ShipPosition Where VslCode='" + VSLCode + "' And ReportId IN( Select Reportid from ReportsMaster Where upper(ltrim(rtrim(VoyageNo)))='" + VoyageNo + "' And ReportId<=" + ReportId.ToString() + " )";
                    DataSet ds_Summary = VesselReporting.getTable(Query);
                    if (ds_Summary.Tables[0].Rows.Count > 0)
                    {
                        dep.SetParameterValue("VStmHrs", ": " + getColumnSum(ds_Summary.Tables[0], "SteamHrs") + " Hrs");
                        dep.SetParameterValue("VDMG", ": " + getColumnSum(ds_Summary.Tables[0], "DMG") + " NM");
                        dep.SetParameterValue("VAvgSpeed", ": " + getColumnAvg(ds_Summary.Tables[0], "AVGSpeed") + " KTS");
                    }
                    else
                    {
                        dep.SetParameterValue("VStmHrs", ": " + "");
                        dep.SetParameterValue("VDMG", ": " + "");
                        dep.SetParameterValue("VAvgSpeed", ": " + "");
                    }

                    Query = "SELECT * FROM SeaCondition Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString();
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("Course", ": " + dr["Course"].ToString() + " Deg");
                        dep.SetParameterValue("WindDirection", ": " + dr["WindDirection"].ToString() + " Deg");
                        dep.SetParameterValue("WindForce", ": " + dr["WindForce"].ToString() + " BF");
                        dep.SetParameterValue("SeaDirection", ": " + dr["SeaDirection"].ToString() + " Deg");
                        dep.SetParameterValue("SeaState", ": " + dr["SeaState"].ToString() + " ");
                        dep.SetParameterValue("CurrDirection", ": " + dr["CurrDirection"].ToString() + " Deg");
                        dep.SetParameterValue("CurrStrength", ": " + dr["CurrStrength"].ToString() + " KTS");
                        dep.SetParameterValue("Remarks", ": " + dr["Remarks"].ToString() + " LTR");
                    }
                    else
                    {
                        dep.SetParameterValue("Course", ": " + "");
                        dep.SetParameterValue("WindDirection", ": " + "");
                        dep.SetParameterValue("WindForce", ": " + "");
                        dep.SetParameterValue("SeaDirection", ": " + "");
                        dep.SetParameterValue("SeaState", ": " + "");
                        dep.SetParameterValue("CurrDirection", ": " + "");
                        dep.SetParameterValue("CurrStrength", ": " + "");
                        dep.SetParameterValue("Remarks", ": " + "");
                    }

                    Query = "SELECT * FROM FuelRecieved Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + " Order By RecType;";
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[1];
                        dep.SetParameterValue("Sulphure1", ": " + dr["IFO1"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure2", ": " + dr["IFO2"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure3", ": " + dr["MGO"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure4", ": " + dr["MGO1"].ToString() + " MT");
                        dep.SetParameterValue("MDO", ": " + dr["MDO"].ToString() + " MT");
                        dep.SetParameterValue("MECC", ": " + dr["MECC"].ToString() + " LTR");
                        dep.SetParameterValue("MECYL", ": " + dr["MRCYL"].ToString() + " LTR");
                        dep.SetParameterValue("AECC", ": " + dr["AECC"].ToString() + " LTR");
                        dep.SetParameterValue("HYD", ": " + dr["HYD"].ToString() + " LTR");
                        dep.SetParameterValue("FW", ": " + dr["FW"].ToString() + " MT");
                    }
                    else
                    {
                        dep.SetParameterValue("Sulphure1", ": " + "");
                        dep.SetParameterValue("Sulphure2", ": " + "");
                        dep.SetParameterValue("Sulphure3", ": " + "");
                        dep.SetParameterValue("Sulphure4", ": " + "");
                        dep.SetParameterValue("MDO", ": " + "");
                        dep.SetParameterValue("MECC", ": " + "");
                        dep.SetParameterValue("MECYL", ": " + "");
                        dep.SetParameterValue("AECC", ": " + "");
                        dep.SetParameterValue("HYD", ": " + "");
                        dep.SetParameterValue("FW", ": " + "");
                    }

                    Query = "SELECT * FROM Fuel Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + " Order By RecType;";
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("MEPD1", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD1", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE11", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE12", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE13", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE14", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH1", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL1", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF1", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS1", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB1", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF1", ": " + dr["TOTALIGS"].ToString() + " MT");

                        dep.SetParameterValue("CONMECC", ": " + dr["MECC"].ToString() + " MT");
                        dep.SetParameterValue("CONMECYL", ": " + dr["MECYL"].ToString() + " MT");
                        dep.SetParameterValue("CONAECC", ": " + dr["AECC"].ToString() + " MT");
                        dep.SetParameterValue("CONHYD", ": " + dr["HYD"].ToString() + " MT");
                        dep.SetParameterValue("CONFW", ": " + dr["FW"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[1];
                        dep.SetParameterValue("MEPD2", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD2", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE21", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE22", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE23", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE24", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH2", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL2", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF2", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS2", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB2", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF2", ": " + dr["TOTALIGS"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[2];
                        dep.SetParameterValue("MEPD3", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD3", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE31", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE32", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE33", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE34", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH3", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL3", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF3", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS3", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB3", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF3", ": " + dr["TOTALIGS"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[3];
                        dep.SetParameterValue("MEPD4", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD4", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE41", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE42", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE43", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE44", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH4", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL4", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF4", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS4", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB4", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF4", ": " + dr["TOTALIGS"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[4];
                        dep.SetParameterValue("MEPD5", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD5", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE51", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE52", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE53", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE54", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH5", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL5", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF5", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS5", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB5", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF5", ": " + dr["TOTALIGS"].ToString() + " MT");
                    }
                    else
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            dep.SetParameterValue("MEPD" + i.ToString(), ": " + dr["ME"].ToString() + " MT");
                            dep.SetParameterValue("AEPD" + i.ToString(), ": " + dr["AE"].ToString() + " MT");
                            dep.SetParameterValue("AE" + i.ToString() + "1", ": " + dr["AE1"].ToString() + " Hrs");
                            dep.SetParameterValue("AE" + i.ToString() + "2", ": " + dr["AE2"].ToString() + " Hrs");
                            dep.SetParameterValue("AE" + i.ToString() + "3", ": " + dr["AE3"].ToString() + " Hrs");
                            dep.SetParameterValue("AE" + i.ToString() + "4", ": " + dr["AE4"].ToString() + " Hrs");
                            dep.SetParameterValue("CH" + i.ToString(), ": " + dr["CH"].ToString() + " MT");
                            dep.SetParameterValue("TCL" + i.ToString(), ": " + dr["TC"].ToString() + " MT");
                            dep.SetParameterValue("GF" + i.ToString(), ": " + dr["GF"].ToString() + " MT");
                            dep.SetParameterValue("IGS" + i.ToString(), ": " + dr["IGS"].ToString() + " MT");
                            dep.SetParameterValue("TB" + i.ToString(), ": " + dr["TOTAL"].ToString() + " MT");
                            dep.SetParameterValue("TF" + i.ToString(), ": " + dr["TOTALIGS"].ToString() + " MT");
                        }
                    }
                    Query = "SELECT * FROM Performance Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString();
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("TempMin", ": " + dr["ExchTempMin"].ToString() + " °C");
                        dep.SetParameterValue("TempMax", ": " + dr["ExchTempMix"].ToString() + " °C");
                        dep.SetParameterValue("MERPM", ": " + dr["MERPM"].ToString() + " ");
                        dep.SetParameterValue("ENGDEST", ": " + dr["EngineDist"].ToString() + " NM");
                        dep.SetParameterValue("SLIP", ": " + dr["SLIP"].ToString() + " %");
                        dep.SetParameterValue("MEOUTPUT", ": " + dr["MEMCR"].ToString() + " % MCR");
                        dep.SetParameterValue("METHRMALLOAD", ": " + dr["METHERMALLOAD"].ToString() + " %");
                        dep.SetParameterValue("TC1", ": " + dr["METIC1"].ToString() + " ");
                        dep.SetParameterValue("TC2", ": " + dr["METIC2"].ToString() + " ");
                        dep.SetParameterValue("MESCAV", ": " + dr["MESCAV"].ToString() + " ");
                        dep.SetParameterValue("SCAVTEMP", ": " + dr["SCAVTEMP"].ToString() + " °C");
                        dep.SetParameterValue("LOP", ": " + dr["LP"].ToString() + " BAR");
                        dep.SetParameterValue("SWT", ": " + dr["SWT"].ToString() + " °C");
                        dep.SetParameterValue("ERT", ": " + dr["ERT"].ToString() + " °C");

                        dep.SetParameterValue("AUX1", ": " + dr["AUX1"].ToString() + " KW");
                        dep.SetParameterValue("AUX2", ": " + dr["AUX2"].ToString() + " KW");
                        dep.SetParameterValue("AUX3", ": " + dr["AUX3"].ToString() + " KW");
                        dep.SetParameterValue("AUX4", ": " + dr["AUX4"].ToString() + " KW");
                    }
                    else
                    {
                        dep.SetParameterValue("TempMin", ": " + "");
                        dep.SetParameterValue("TempMax", ": " + "");
                        dep.SetParameterValue("MERPM", ": " + "");
                        dep.SetParameterValue("ENGDEST", ": " + "");
                        dep.SetParameterValue("SLIP", ": " + "");
                        dep.SetParameterValue("MEOUTPUT", ": " + "");
                        dep.SetParameterValue("METHRMALLOAD", ": " + "");
                        dep.SetParameterValue("TC1", ": " + "");
                        dep.SetParameterValue("TC2", ": " + "");
                        dep.SetParameterValue("MESCAV", ": " + "");
                        dep.SetParameterValue("SCAVTEMP", ": " + "");
                        dep.SetParameterValue("LOP", ": " + "");
                        dep.SetParameterValue("SWT", ": " + "");
                        dep.SetParameterValue("ERT", ": " + "");

                        dep.SetParameterValue("AUX1", ": " + "");
                        dep.SetParameterValue("AUX2", ": " + "");
                        dep.SetParameterValue("AUX3", ": " + "");
                        dep.SetParameterValue("AUX4", ": " + "");
                    }
                    #endregion
                }
                else if (ReportType == "A")
                {
                    dep.Load(Server.MapPath("Arrival.rpt"));
                    dep.SetParameterValue("VSLName", getVesselName(drMain["VslCode"].ToString()));

                    #region -------------   ARRIVAL REPORT --------------
                    Query = " SELECT * FROM VoyageInfo Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    ds = VesselReporting.getTable(Query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("ReportHeader", "Arrival Report  " + getPortName(dr["AI_FromPort"].ToString()) + " - " + DateTime.Parse(dr["AI_ETA"].ToString()).ToString("dd-MMM-yyyy"));
                        dep.SetParameterValue("VoyageNo", ": " + dr["VoyageNo"].ToString());
                        if (dr["AnyRestArea"].ToString().Trim().ToUpper() == "YES")
                        {
                            string area = "";
                            dep.SetParameterValue("FRAHeader", "Transiting to Restricted Area ");
                            if (dr["AreaName1"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + "ECA 1.0 S";
                            }
                            if (dr["AreaName2"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + ((area.Trim() == "") ? "" : ", ") + "CA 0.5 S";
                            }
                            if (dr["AreaName3"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + ((area.Trim() == "") ? "" : ", ") + "EU 0.1 S";
                            }
                            DateTime dt = DateTime.Parse(dr["ETADate"].ToString());
                            dep.SetParameterValue("FRA", ": " + area + "  :  " + dt.ToString("dd-MMM-yyyy"));
                        }
                        else
                        {
                            dep.SetParameterValue("FRAHeader", "");
                            dep.SetParameterValue("FRA", "");
                        }

                        dep.SetParameterValue("CHName", ": " + dr["CharterName"].ToString());
                        dep.SetParameterValue("CPS", ": " + dr["PartySpeed"].ToString() + " KTS");
                        dep.SetParameterValue("VOS", ": " + dr["OrderSpeed"].ToString() + " KTS");
                        dep.SetParameterValue("VI", dr["VoyInstructions"].ToString());
                        dep.SetParameterValue("VoyCon", ": " + dr["AI_VoyCondition"].ToString());

                        dep.SetParameterValue("EOSPDateTime", ": " + dr["AI_ETA"].ToString() + " Hrs");
                        dep.SetParameterValue("ZoneTime", ": " + dr["AI_Zone"].ToString());
                        dep.SetParameterValue("DraftFwd", ": " + dr["AI_Fwd"].ToString() + " Mtrs");
                        dep.SetParameterValue("DraftAft", ": " + dr["AI_Aft"].ToString() + " Mtrs");
                        dep.SetParameterValue("ActAftArr", ": " + dr["AI_ActAftInt"].ToString());

                        dep.SetParameterValue("AgentName", ": " + dr["AI_AgentName"].ToString());
                        dep.SetParameterValue("PersonInch", ": " + dr["AI_IncName"].ToString());
                        dep.SetParameterValue("Address", ": " + dr["AI_Address"].ToString());
                        dep.SetParameterValue("BussPhone", ": " + dr["AI_BussPhone"].ToString() + ((dr["AI_BussPhone"].ToString().Trim() == "") ? "" : ", ") + dr["AI_BussPhone1"].ToString());
                        dep.SetParameterValue("Mobile", ": " + dr["AI_Mobile"].ToString() + ((dr["AI_Mobile"].ToString().Trim() == "") ? "" : ", ") + dr["AI_Mobile1"].ToString());
                        dep.SetParameterValue("Fax", ": " + dr["AI_Fax"].ToString() + ((dr["AI_Fax"].ToString().Trim() == "") ? "" : ", ") + dr["AI_Fax1"].ToString());
                        dep.SetParameterValue("Email", ": " + dr["AI_Email"].ToString() + ((dr["AI_Email"].ToString().Trim() == "") ? "" : ", ") + dr["AI_Email1"].ToString());
                    }
                    else
                    {
                        dep.SetParameterValue("ReportHeader", ": " + "");
                        dep.SetParameterValue("FromToPort", ": " + "");
                        dep.SetParameterValue("VoyageNo", ": " + "");
                        dep.SetParameterValue("FRA", ": " + "");
                        dep.SetParameterValue("CHName", ": " + "");
                        dep.SetParameterValue("CPS", ": " + "");
                        dep.SetParameterValue("VOS", ": " + "");
                        dep.SetParameterValue("VI", ": " + "");
                        dep.SetParameterValue("VoyCon", ": " + "");

                        dep.SetParameterValue("EOSPDate", ": " + "");
                        dep.SetParameterValue("ZoneTime", ": " + "");
                        dep.SetParameterValue("DraftFwd", ": " + "");
                        dep.SetParameterValue("DraftAft", ": " + "");
                        dep.SetParameterValue("ActAftArr", ": " + "");

                        dep.SetParameterValue("AgentName", ": " + "");
                        dep.SetParameterValue("PersonInch", ": " + "");
                        dep.SetParameterValue("Address", ": " + "");
                        dep.SetParameterValue("BussPhone", ": " + "");
                        dep.SetParameterValue("Mobile", ": " + "");
                        dep.SetParameterValue("Fax", ": " + "");
                        dep.SetParameterValue("Email", ": " + "");
                    }
                    Query = "SELECT * FROM ShipPosition Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    ds = VesselReporting.getTable(Query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        char[] sep = { ',' };
                        string[] Lats = dr["lattitude"].ToString().Split(sep);
                        string[] Longs = dr["Logitude"].ToString().Split(sep);
                        dep.SetParameterValue("Latt", ": " + Lats[0] + "° " + Lats[1] + "' " + Lats[2]);
                        dep.SetParameterValue("Long", ": " + Longs[0] + "° " + Longs[1] + "' " + Longs[2]);
                        dep.SetParameterValue("LocDesc", ": " + dr["Descr"].ToString());
                    }
                    else
                    {
                        dep.SetParameterValue("Latt", ": " + "");
                        dep.SetParameterValue("Long", ": " + "");
                        dep.SetParameterValue("LocDesc", ": " + "");
                    }
                    Query = "SELECT * FROM FuelRecieved Where VSlCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + " Order By RecType;";
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[1];
                        dep.SetParameterValue("Sulphure1", ": " + dr["IFO1"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure2", ": " + dr["IFO2"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure3", ": " + dr["MGO"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure4", ": " + dr["MGO1"].ToString() + " MT");
                        dep.SetParameterValue("MDO", ": " + dr["MDO"].ToString() + " MT");
                        dep.SetParameterValue("MECC", ": " + dr["MECC"].ToString() + " LTR");
                        dep.SetParameterValue("MECYL", ": " + dr["MRCYL"].ToString() + " LTR");
                        dep.SetParameterValue("AECC", ": " + dr["AECC"].ToString() + " LTR");
                        dep.SetParameterValue("HYD", ": " + dr["HYD"].ToString() + " LTR");
                        dep.SetParameterValue("FW", ": " + dr["FW"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[0];
                        string RankName = dr["RankId"].ToString().Trim();

                        RankName = (RankName == "1") ? "Master" : ((RankName == "2") ? "Chief Officer" : ((RankName == "3") ? "Chief Engineer" : ((RankName == "4") ? "First A/E" : "")));
                        dep.SetParameterValue("ROBVerification", ": " + dr["VerifiedBy"].ToString() + " / " + RankName);

                    }
                    else
                    {
                        dep.SetParameterValue("Sulphure1", ": " + "");
                        dep.SetParameterValue("Sulphure2", ": " + "");
                        dep.SetParameterValue("Sulphure3", ": " + "");
                        dep.SetParameterValue("Sulphure4", ": " + "");
                        dep.SetParameterValue("MDO", ": " + "");
                        dep.SetParameterValue("MECC", ": " + "");
                        dep.SetParameterValue("MECYL", ": " + "");
                        dep.SetParameterValue("AECC", ": " + "");
                        dep.SetParameterValue("HYD", ": " + "");
                        dep.SetParameterValue("FW", ": " + "");

                        dep.SetParameterValue("ROBVerification", "");
                    }
                    #endregion
                }
                else if (ReportType == "P")
                {
                    dep.Load(Server.MapPath("Port.rpt"));
                    #region ------------- PORT REPORT --------------

                    Query = " SELECT * FROM portActivityheader Where VslCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    ds = VesselReporting.getTable(Query);

                    Query = " SELECT * FROM VoyageInfo Where VslCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    DataSet ds1 = VesselReporting.getTable(Query);

                    string VoyageNo = ds1.Tables[0].Rows[0]["VoyageNo"].ToString();

                    //Common.Set_Parameters(new MyParameter("@Query", "SELECT ReportId,Sno,EDate,EFTime,ETTime,Descr FROM PortActivityDetails Where ReportId In (select reportid from reportsmaster where voyageno='" + VoyageNo + "') And ReportId In (select reportid from PortActivityHeader where PortName=" + ds.Tables[0].Rows[0]["PortName"].ToString() + ") Order By convert(smalldatetime,EDate),EFTime" + ";" + "SELECT * FROM portActivityCargoDetails Where VslCode='" + VesselCode + "' And ReportId =" + ReportId.ToString() + ";"));
                    Query = " select * from portactivitydetails where vSLcode='" + VSLCode + "' " +
                                                                "AND REPORTID IN " +
                                                                "( " +
                                                                "SELECT REPORTID FROM portactivityheader where vslcode='" + VSLCode + "' AND PORTNAME=" + ds.Tables[0].Rows[0]["PortName"].ToString() + " AND REPORTID <=" + ReportId.ToString() + " AND REPORTID IN " +
                                                                    "(SELECT REPORTID FROM REPORTSMASTER WHERE REPORTTYPE='P' AND VOYAGENO='" + VoyageNo + "') " +
                                                                ") order by convert(smalldatetime,edate),EFTime " + ";" + "SELECT * FROM portActivityCargoDetails Where VslCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                   DataSet dsMain = VesselReporting.getTable(Query);

                    dsMain.Tables[0].TableName = "portActivityDetails";
                    dsMain.Tables[1].TableName = "portActivityCargoDetails";


                    if (ds.Tables[0].Rows[0]["PortActivity"].ToString().Trim() == "Berthing" || ds.Tables[0].Rows[0]["PortActivity"].ToString().Trim() == "Add Berth")
                    {
                        dep.Subreports[0].SetDataSource(dsMain);
                        dep.Subreports[1].SetDataSource(dsMain);
                    }
                    else
                    {
                        dsMain.Clear();
                        dep.Subreports[0].SetDataSource(dsMain);
                        dep.Subreports[1].SetDataSource(dsMain);
                        // 
                        //foreach (ReportObject Ro in dep.Section1.ReportObjects)
                        //{
                        //    Ro.ObjectFormat.EnableSuppress = true;   
                        //}
                    }
                    dep.SetParameterValue("VSLName", getVesselName(ds1.Tables[0].Rows[0]["VslCode"].ToString()));
                    dep.SetParameterValue("ReportHeader", "Port Report - " + dtMain.ToString("dd-MMM-yyyy"));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("FromToPort", getPortName(dr["PortName"].ToString()));
                        dep.SetParameterValue("PortActAfterArrival", ": " + dr["PortActivity"].ToString());

                        if (dr["PortActivity"].ToString().Trim() == "")
                        {
                            dep.SetParameterValue("Header1", "");
                            dep.SetParameterValue("Header2", "");
                            dep.SetParameterValue("Header3", "");
                            dep.SetParameterValue("Header4", "");
                            dep.SetParameterValue("Header5", "");
                            dep.SetParameterValue("Header6", "");
                            dep.SetParameterValue("Header7", "");
                            dep.SetParameterValue("Value1", "");
                            dep.SetParameterValue("Value2", "");
                            dep.SetParameterValue("Value3", "");
                            dep.SetParameterValue("Value4", "");
                            dep.SetParameterValue("Value5", "");
                            dep.SetParameterValue("Value6", "");
                            dep.SetParameterValue("Value7", "");
                        }
                        else if (dr["PortActivity"].ToString().Trim() == "Drifting")
                        {
                            char[] sep = { ',' };
                            string[] Lats = dr["DP_Latt"].ToString().Split(sep);
                            string[] Longs = dr["DP_Long"].ToString().Split(sep);
                            dep.SetParameterValue("Header1", "Lattitude");
                            dep.SetParameterValue("Header2", "Longitude");
                            dep.SetParameterValue("Header3", "Rate of Drift");
                            dep.SetParameterValue("Header4", "Direction of Drift");
                            dep.SetParameterValue("Header5", "ETB Date&Time");
                            dep.SetParameterValue("Header6", "Time Zone");
                            dep.SetParameterValue("Header7", "Remarks");
                            dep.SetParameterValue("Value1", ": " + Lats[0] + "° " + Lats[1] + "' " + Lats[2] + "");
                            dep.SetParameterValue("Value2", ": " + Longs[0] + "° " + Longs[1] + "' " + Longs[2]);
                            dep.SetParameterValue("Value3", ": " + dr["DP_Rate"].ToString() + " KTS");
                            dep.SetParameterValue("Value4", ": " + dr["DP_Dir"].ToString() + " Deg");
                            if (dr["DP_ETB"].ToString().Trim() == "00:00")
                                dep.SetParameterValue("Value5", ": " + dr["DP_ETB"].ToString());
                            else
                                dep.SetParameterValue("Value5", ": " + dr["DP_ETB"].ToString());
                            dep.SetParameterValue("Value6", ": " + dr["DP_Zone"].ToString());
                            dep.SetParameterValue("Value7", ": " + dr["DP_Rem"].ToString());
                        }
                        else if (dr["PortActivity"].ToString().Trim() == "Dry Dock")
                        {
                            char[] sep = { ',' };
                            string[] Lats = dr["DP_Latt"].ToString().Split(sep);
                            string[] Longs = dr["DP_Long"].ToString().Split(sep);
                            dep.SetParameterValue("Header1", "Remarks");
                            dep.SetParameterValue("Header2", "");
                            dep.SetParameterValue("Header3", "");
                            dep.SetParameterValue("Header4", "");
                            dep.SetParameterValue("Header5", "");
                            dep.SetParameterValue("Header6", "");
                            dep.SetParameterValue("Header7", "");
                            dep.SetParameterValue("Value1", ": " + dr["DP_Rem"].ToString());
                            dep.SetParameterValue("Value2", "");
                            dep.SetParameterValue("Value3", "");
                            dep.SetParameterValue("Value4", "");
                            if (dr["DP_ETB"].ToString().Trim() == "00:00")
                                dep.SetParameterValue("Value5", "");
                            else
                                dep.SetParameterValue("Value5", "");
                            dep.SetParameterValue("Value6", "");
                            dep.SetParameterValue("Value7", "");
                        }
                        else if (dr["PortActivity"].ToString().Trim() == "Anchoring" || dr["PortActivity"].ToString().Trim() == "Add Anchor")
                        {
                            char[] sep = { ',' };
                            string[] Lats = dr["AP_Latt"].ToString().Split(sep);
                            string[] Longs = dr["AP_Long"].ToString().Split(sep);
                            dep.SetParameterValue("Header1", "Lattitude");
                            dep.SetParameterValue("Header2", "Longitude");
                            dep.SetParameterValue("Header3", "ETB Date&Time");
                            dep.SetParameterValue("Header4", "Time Zone");
                            dep.SetParameterValue("Header5", "Remarks");
                            dep.SetParameterValue("Header6", "");
                            dep.SetParameterValue("Header7", "");

                            dep.SetParameterValue("Value1", ": " + Lats[0] + "° " + Lats[1] + "' " + Lats[2] + "");
                            dep.SetParameterValue("Value2", ": " + Longs[0] + "° " + Longs[1] + "' " + Longs[2]);
                            if (dr["AP_ETB"].ToString().Trim() == "00:00")
                                dep.SetParameterValue("Value3", ": " + "");
                            else
                                dep.SetParameterValue("Value3", ": " + dr["AP_ETB"].ToString());
                            dep.SetParameterValue("Value4", ": " + dr["AP_Zone"].ToString());
                            dep.SetParameterValue("Value5", ": " + dr["AP_Rem"].ToString());
                            dep.SetParameterValue("Value6", " " + " ");
                            dep.SetParameterValue("Value7", " " + " ");
                        }
                        else if (dr["PortActivity"].ToString().Trim() == "Berthing")
                        {

                            dep.SetParameterValue("Header1", "Birth Details");
                            dep.SetParameterValue("Header2", "Next Port");
                            dep.SetParameterValue("Header3", "ETD Date&Time");
                            dep.SetParameterValue("Header4", "Time Zone");
                            dep.SetParameterValue("Header5", "");
                            dep.SetParameterValue("Header6", "");
                            dep.SetParameterValue("Header7", "");

                            dep.SetParameterValue("Value1", ": " + dr["BerthDet"].ToString());
                            dep.SetParameterValue("Value2", ": " + getPortName(dr["NPN"].ToString()));
                            dep.SetParameterValue("Value3", ": " + dr["ETDNP"].ToString());
                            dep.SetParameterValue("Value4", ": " + dr["BD_Zone"].ToString());
                            dep.SetParameterValue("Value5", " ");
                            dep.SetParameterValue("Value6", " ");
                            dep.SetParameterValue("Value7", " ");
                        }

                    }
                    else
                    {
                        dep.SetParameterValue("FromToPort", "");

                        dep.SetParameterValue("Header1", "");
                        dep.SetParameterValue("Header2", "");
                        dep.SetParameterValue("Header3", "");
                        dep.SetParameterValue("Header4", "");
                        dep.SetParameterValue("Header5", "");
                        dep.SetParameterValue("Header6", "");
                        dep.SetParameterValue("Header7", "");
                        dep.SetParameterValue("Value1", "");
                        dep.SetParameterValue("Value2", "");
                        dep.SetParameterValue("Value3", "");
                        dep.SetParameterValue("Value4", "");
                        dep.SetParameterValue("Value5", "");
                        dep.SetParameterValue("Value6", "");
                        dep.SetParameterValue("Value7", "");
                    }

                    Query = " SELECT * FROM VoyageInfo Where VslCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    ds = VesselReporting.getTable(Query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("VoyageNo", ": " + dr["VoyageNo"].ToString());
                        VoyageNo = dr["VoyageNo"].ToString();
                        if (dr["AnyRestArea"].ToString().Trim().ToUpper() == "YES")
                        {
                            string area = "";
                            dep.SetParameterValue("FRAHeader", "Transiting to Restricted Area ");
                            if (dr["AreaName1"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + "ECA 1.0 S";
                            }
                            if (dr["AreaName2"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + ((area.Trim() == "") ? "" : ", ") + "CA 0.5 S";
                            }
                            if (dr["AreaName3"].ToString().Trim().ToUpper() == "Y")
                            {
                                area = area + ((area.Trim() == "") ? "" : ", ") + "EU 0.1 S";
                            }
                            DateTime dt = DateTime.Parse(dr["ETADate"].ToString());
                            dep.SetParameterValue("FRA", ": " + area + "  :  " + dt.ToString("dd-MMM-yyyy"));
                        }
                        else
                        {
                            dep.SetParameterValue("FRAHeader", "");
                            dep.SetParameterValue("FRA", "");
                        }
                        if (dr["eNOA"].ToString().StartsWith("Y"))
                        {
                            dep.SetParameterValue("USCGHeader", "USCG Notification Sent");
                            dep.SetParameterValue("USCGValue", ": " + ((dr["CharterName"].ToString().Substring(1, 1) == "Y") ? "Yes" : "No"));
                        }
                        else
                        {
                            dep.SetParameterValue("USCGHeader", "");
                            dep.SetParameterValue("USCGValue", "");
                        }
                        dep.SetParameterValue("CHName", ": " + dr["CharterName"].ToString());
                        dep.SetParameterValue("CPS", ": " + dr["PartySpeed"].ToString() + " KTS");
                        dep.SetParameterValue("VOS", ": " + dr["OrderSpeed"].ToString() + " KTS");
                        dep.SetParameterValue("VI", dr["VoyInstructions"].ToString());
                        dep.SetParameterValue("VoyCon", ": " + dr["DI_VoyCondition"].ToString());

                        dep.SetParameterValue("CospDateTime", ": " + dr["DI_DepDate"].ToString());
                        dep.SetParameterValue("ZoneTime", ": " + dr["DI_Zone"].ToString());
                        dep.SetParameterValue("NextPortETA", ": " + dr["DI_ETA"].ToString());
                        dep.SetParameterValue("DraftFwd", ": " + dr["DI_Fwd"].ToString() + " Mtrs");
                        dep.SetParameterValue("DraftAft", ": " + dr["DI_Aft"].ToString() + " Mtrs");
                        dep.SetParameterValue("DTG", ": " + dr["DI_DTG"].ToString() + " NM");

                        dep.SetParameterValue("AgentName", ": " + dr["DI_AgentName"].ToString());
                        dep.SetParameterValue("PersonInch", ": " + dr["DI_IncName"].ToString());
                        dep.SetParameterValue("Address", ": " + dr["DI_Address"].ToString());
                        dep.SetParameterValue("BussPhone", ": " + dr["DI_BussPhone"].ToString() + ((dr["DI_BussPhone"].ToString().Trim() == "") ? "" : ", ") + dr["DI_BussPhone1"].ToString());
                        dep.SetParameterValue("Mobile", ": " + dr["DI_Mobile"].ToString() + ((dr["DI_Mobile"].ToString().Trim() == "") ? "" : ", ") + dr["DI_Mobile1"].ToString());
                        dep.SetParameterValue("Fax", ": " + dr["DI_Fax"].ToString() + ((dr["DI_Fax"].ToString().Trim() == "") ? "" : ", ") + dr["DI_Fax1"].ToString());
                        dep.SetParameterValue("Email", ": " + dr["DI_Email"].ToString() + ((dr["DI_Email"].ToString().Trim() == "") ? "" : ", ") + dr["DI_Email1"].ToString());
                    }
                    else
                    {
                        dep.SetParameterValue("ReportHeader", ": " + "");
                        dep.SetParameterValue("FromToPort", ": " + "");
                        dep.SetParameterValue("VoyageNo", ": " + "");
                        dep.SetParameterValue("FRA", ": " + "");
                        dep.SetParameterValue("CHName", ": " + "");
                        dep.SetParameterValue("CPS", ": " + "");
                        dep.SetParameterValue("VOS", ": " + "");
                        dep.SetParameterValue("VI", ": " + "");
                        dep.SetParameterValue("VoyCon", ": " + "");

                        dep.SetParameterValue("CospDateTime", ": " + "");
                        dep.SetParameterValue("ZoneTime", ": " + "");
                        dep.SetParameterValue("NextPortETA", ": " + "");
                        dep.SetParameterValue("DraftFwd", ": " + "");
                        dep.SetParameterValue("DraftAft", ": " + "");
                        dep.SetParameterValue("DTG", ": " + "");


                        dep.SetParameterValue("AgentName", ": " + "");
                        dep.SetParameterValue("PersonInch", ": " + "");
                        dep.SetParameterValue("Address", ": " + "");
                        dep.SetParameterValue("BussPhone", ": " + "");
                        dep.SetParameterValue("Mobile", ": " + "");
                        dep.SetParameterValue("Fax", ": " + "");
                        dep.SetParameterValue("Email", ": " + "");
                    }
                    Query = " SELECT * FROM ShipPosition Where VslCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + ";";
                    ds = VesselReporting.getTable(Query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        char[] sep = { ',' };
                        string[] Lats = dr["lattitude"].ToString().Split(sep);
                        string[] Longs = dr["Logitude"].ToString().Split(sep);
                        dep.SetParameterValue("Latt", ": " + Lats[0] + "° " + Lats[1] + "' " + Lats[2]);
                        dep.SetParameterValue("Long", ": " + Longs[0] + "° " + Longs[1] + "' " + Longs[2]);
                        dep.SetParameterValue("LocDesc", ": " + dr["Descr"].ToString());

                        dep.SetParameterValue("StmHrs", ": " + dr["SteamHrs"].ToString() + " Hrs");
                        dep.SetParameterValue("DMG", ": " + dr["DMG"].ToString() + " NM");
                        dep.SetParameterValue("AvgSpeed", ": " + dr["AvgSpeed"].ToString() + " KTS");

                        if (dr["StopPages"].ToString().Trim() != "")
                        {
                            dep.SetParameterValue("StopPagesHeader", "Stoppages");
                            dep.SetParameterValue("StopPagesValue", ": " + dr["StopPages"].ToString());
                            dep.SetParameterValue("StopPagesRemark", ": " + dr["STRemark"].ToString());
                        }
                        else
                        {
                            dep.SetParameterValue("StopPagesHeader", "");
                            dep.SetParameterValue("StopPagesValue", "");
                            dep.SetParameterValue("StopPagesRemark", "");
                        }

                    }
                    else
                    {
                        dep.SetParameterValue("Latt", ": " + "");
                        dep.SetParameterValue("Long", ": " + "");
                        dep.SetParameterValue("LocDesc", ": " + "");

                        dep.SetParameterValue("StmHrs", ": " + "");
                        dep.SetParameterValue("DMG", ": " + "");
                        dep.SetParameterValue("AvgSpeed", ": " + "");

                        dep.SetParameterValue("StopPagesHeader", "");
                        dep.SetParameterValue("StopPagesValue", "");
                        dep.SetParameterValue("StopPagesRemark", "");
                    }

                    Query = " SELECT * FROM ShipPosition Where VslCode='" + VSLCode + "' And ReportId IN( Select Reportid from ReportsMaster Where upper(ltrim(rtrim(VoyageNo)))='" + VoyageNo + "' And ReportId<=" + ReportId.ToString() + " )";
                    DataSet ds_Summary = VesselReporting.getTable(Query);
                    if (ds_Summary.Tables[0].Rows.Count > 0)
                    {
                        dep.SetParameterValue("VStmHrs", ": " + getColumnSum(ds_Summary.Tables[0], "SteamHrs") + " Hrs");
                        dep.SetParameterValue("VDMG", ": " + getColumnSum(ds_Summary.Tables[0], "DMG") + " NM");
                        dep.SetParameterValue("VAvgSpeed", ": " + getColumnAvg(ds_Summary.Tables[0], "AVGSpeed") + " KTS");
                    }
                    else
                    {
                        dep.SetParameterValue("VStmHrs", ": " + "");
                        dep.SetParameterValue("VDMG", ": " + "");
                        dep.SetParameterValue("VAvgSpeed", ": " + "");
                    }

                    Query = " SELECT * FROM SeaCondition Where VslCode='" + VSLCode + "' And ReportId =" + ReportId.ToString();
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("Course", ": " + dr["Course"].ToString() + " Deg");
                        dep.SetParameterValue("WindDirection", ": " + dr["WindDirection"].ToString() + " Deg");
                        dep.SetParameterValue("WindForce", ": " + dr["WindForce"].ToString() + " BF");
                        dep.SetParameterValue("SeaDirection", ": " + dr["SeaDirection"].ToString() + " Deg");
                        dep.SetParameterValue("SeaState", ": " + dr["SeaState"].ToString() + " ");
                        dep.SetParameterValue("CurrDirection", ": " + dr["CurrDirection"].ToString() + " Deg");
                        dep.SetParameterValue("CurrStrength", ": " + dr["CurrStrength"].ToString() + " KTS");
                        dep.SetParameterValue("Remarks", ": " + dr["Remarks"].ToString() + " LTR");
                    }
                    else
                    {
                        dep.SetParameterValue("Course", ": " + "");
                        dep.SetParameterValue("WindDirection", ": " + "");
                        dep.SetParameterValue("WindForce", ": " + "");
                        dep.SetParameterValue("SeaDirection", ": " + "");
                        dep.SetParameterValue("SeaState", ": " + "");
                        dep.SetParameterValue("CurrDirection", ": " + "");
                        dep.SetParameterValue("CurrStrength", ": " + "");
                        dep.SetParameterValue("Remarks", ": " + "");
                    }

                    Query = " SELECT * FROM FuelRecieved Where VslCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + " Order By RecType;";
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("SulphureR1", ": " + dr["IFO1"].ToString() + " MT");
                        dep.SetParameterValue("SulphureR2", ": " + dr["IFO2"].ToString() + " MT");
                        dep.SetParameterValue("SulphureR3", ": " + dr["MGO"].ToString() + " MT");
                        dep.SetParameterValue("SulphureR4", ": " + dr["MGO1"].ToString() + " MT");
                        dep.SetParameterValue("MDOR", ": " + dr["MDO"].ToString() + " MT");
                        dep.SetParameterValue("MECCR", ": " + dr["MECC"].ToString() + " LTR");
                        dep.SetParameterValue("MECYLR", ": " + dr["MRCYL"].ToString() + " LTR");
                        dep.SetParameterValue("AECCR", ": " + dr["AECC"].ToString() + " LTR");
                        dep.SetParameterValue("HYDR", ": " + dr["HYD"].ToString() + " LTR");
                        dep.SetParameterValue("FWR", ": " + dr["FW"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[1];
                        dep.SetParameterValue("Sulphure1", ": " + dr["IFO1"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure2", ": " + dr["IFO2"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure3", ": " + dr["MGO"].ToString() + " MT");
                        dep.SetParameterValue("Sulphure4", ": " + dr["MGO1"].ToString() + " MT");
                        dep.SetParameterValue("MDO", ": " + dr["MDO"].ToString() + " MT");
                        dep.SetParameterValue("MECC", ": " + dr["MECC"].ToString() + " LTR");
                        dep.SetParameterValue("MECYL", ": " + dr["MRCYL"].ToString() + " LTR");
                        dep.SetParameterValue("AECC", ": " + dr["AECC"].ToString() + " LTR");
                        dep.SetParameterValue("HYD", ": " + dr["HYD"].ToString() + " LTR");
                        dep.SetParameterValue("FW", ": " + dr["FW"].ToString() + " MT");
                    }
                    else
                    {
                        dep.SetParameterValue("SulphureR1", ": " + "");
                        dep.SetParameterValue("SulphureR2", ": " + "");
                        dep.SetParameterValue("SulphureR3", ": " + "");
                        dep.SetParameterValue("SulphureR4", ": " + "");
                        dep.SetParameterValue("MDOR", ": " + "");
                        dep.SetParameterValue("MECCR", ": " + "");
                        dep.SetParameterValue("MECYLR", ": " + "");
                        dep.SetParameterValue("AECCR", ": " + "");
                        dep.SetParameterValue("HYDR", ": " + "");
                        dep.SetParameterValue("FWR", ": " + "");

                        dep.SetParameterValue("Sulphure1", ": " + "");
                        dep.SetParameterValue("Sulphure2", ": " + "");
                        dep.SetParameterValue("Sulphure3", ": " + "");
                        dep.SetParameterValue("Sulphure4", ": " + "");
                        dep.SetParameterValue("MDO", ": " + "");
                        dep.SetParameterValue("MECC", ": " + "");
                        dep.SetParameterValue("MECYL", ": " + "");
                        dep.SetParameterValue("AECC", ": " + "");
                        dep.SetParameterValue("HYD", ": " + "");
                        dep.SetParameterValue("FW", ": " + "");
                    }

                    Query = " SELECT * FROM Fuel Where VslCode='" + VSLCode + "' And ReportId =" + ReportId.ToString() + " Order By RecType;";
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("MEPD1", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD1", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE11", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE12", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE13", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE14", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH1", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL1", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF1", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS1", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB1", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF1", ": " + dr["TOTALIGS"].ToString() + " MT");

                        dep.SetParameterValue("CONMECC", ": " + dr["MECC"].ToString() + " MT");
                        dep.SetParameterValue("CONMECYL", ": " + dr["MECYL"].ToString() + " MT");
                        dep.SetParameterValue("CONAECC", ": " + dr["AECC"].ToString() + " MT");
                        dep.SetParameterValue("CONHYD", ": " + dr["HYD"].ToString() + " MT");
                        dep.SetParameterValue("CONFW", ": " + dr["FW"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[1];
                        dep.SetParameterValue("MEPD2", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD2", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE21", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE22", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE23", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE24", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH2", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL2", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF2", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS2", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB2", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF2", ": " + dr["TOTALIGS"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[2];
                        dep.SetParameterValue("MEPD3", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD3", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE31", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE32", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE33", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE34", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH3", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL3", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF3", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS3", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB3", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF3", ": " + dr["TOTALIGS"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[3];
                        dep.SetParameterValue("MEPD4", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD4", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE41", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE42", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE43", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE44", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH4", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL4", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF4", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS4", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB4", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF4", ": " + dr["TOTALIGS"].ToString() + " MT");

                        dr = ds.Tables[0].Rows[4];
                        dep.SetParameterValue("MEPD5", ": " + dr["ME"].ToString() + " MT");
                        dep.SetParameterValue("AEPD5", ": " + dr["AE"].ToString() + " MT");
                        dep.SetParameterValue("AE51", ": " + dr["AE1"].ToString() + " Hrs");
                        dep.SetParameterValue("AE52", ": " + dr["AE2"].ToString() + " Hrs");
                        dep.SetParameterValue("AE53", ": " + dr["AE3"].ToString() + " Hrs");
                        dep.SetParameterValue("AE54", ": " + dr["AE4"].ToString() + " Hrs");
                        dep.SetParameterValue("CH5", ": " + dr["CH"].ToString() + " MT");
                        dep.SetParameterValue("TCL5", ": " + dr["TC"].ToString() + " MT");
                        dep.SetParameterValue("GF5", ": " + dr["GF"].ToString() + " MT");
                        dep.SetParameterValue("IGS5", ": " + dr["IGS"].ToString() + " MT");
                        dep.SetParameterValue("TB5", ": " + dr["TOTAL"].ToString() + " MT");
                        dep.SetParameterValue("TF5", ": " + dr["TOTALIGS"].ToString() + " MT");
                    }
                    else
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            dep.SetParameterValue("MEPD" + i.ToString(), ": " + dr["ME"].ToString() + " MT");
                            dep.SetParameterValue("AEPD" + i.ToString(), ": " + dr["AE"].ToString() + " MT");
                            dep.SetParameterValue("AE" + i.ToString() + "1", ": " + dr["AE1"].ToString() + " Hrs");
                            dep.SetParameterValue("AE" + i.ToString() + "2", ": " + dr["AE2"].ToString() + " Hrs");
                            dep.SetParameterValue("AE" + i.ToString() + "3", ": " + dr["AE3"].ToString() + " Hrs");
                            dep.SetParameterValue("AE" + i.ToString() + "4", ": " + dr["AE4"].ToString() + " Hrs");
                            dep.SetParameterValue("CH" + i.ToString(), ": " + dr["CH"].ToString() + " MT");
                            dep.SetParameterValue("TCL" + i.ToString(), ": " + dr["TC"].ToString() + " MT");
                            dep.SetParameterValue("GF" + i.ToString(), ": " + dr["GF"].ToString() + " MT");
                            dep.SetParameterValue("IGS" + i.ToString(), ": " + dr["IGS"].ToString() + " MT");
                            dep.SetParameterValue("TB" + i.ToString(), ": " + dr["TOTAL"].ToString() + " MT");
                            dep.SetParameterValue("TF" + i.ToString(), ": " + dr["TOTALIGS"].ToString() + " MT");
                        }
                    }
                    Query = " SELECT * FROM Performance Where VslCode='" + VSLCode + "' And ReportId =" + ReportId.ToString();
                    ds = VesselReporting.getTable(Query);
                    //-------------------------
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        dep.SetParameterValue("TempMin", ": " + dr["ExchTempMin"].ToString() + " °C");
                        dep.SetParameterValue("TempMax", ": " + dr["ExchTempMix"].ToString() + " °C");
                        dep.SetParameterValue("MERPM", ": " + dr["MERPM"].ToString() + " ");
                        dep.SetParameterValue("ENGDEST", ": " + dr["EngineDist"].ToString() + " NM");
                        dep.SetParameterValue("SLIP", ": " + dr["SLIP"].ToString() + " %");
                        dep.SetParameterValue("MEOUTPUT", ": " + dr["MEMCR"].ToString() + " % MCR");
                        dep.SetParameterValue("METHRMALLOAD", ": " + dr["METHERMALLOAD"].ToString() + " %");
                        dep.SetParameterValue("TC1", ": " + dr["METIC1"].ToString() + " ");
                        dep.SetParameterValue("TC2", ": " + dr["METIC2"].ToString() + " ");
                        dep.SetParameterValue("MESCAV", ": " + dr["MESCAV"].ToString() + " ");
                        dep.SetParameterValue("SCAVTEMP", ": " + dr["SCAVTEMP"].ToString() + " °C");
                        dep.SetParameterValue("LOP", ": " + dr["LP"].ToString() + " BAR");
                        dep.SetParameterValue("SWT", ": " + dr["SWT"].ToString() + " °C");
                        dep.SetParameterValue("ERT", ": " + dr["ERT"].ToString() + " °C");

                        dep.SetParameterValue("AUX1", ": " + dr["AUX1"].ToString() + " KW");
                        dep.SetParameterValue("AUX2", ": " + dr["AUX2"].ToString() + " KW");
                        dep.SetParameterValue("AUX3", ": " + dr["AUX3"].ToString() + " KW");
                        dep.SetParameterValue("AUX4", ": " + dr["AUX4"].ToString() + " KW");
                    }
                    else
                    {
                        dep.SetParameterValue("TempMin", ": " + "");
                        dep.SetParameterValue("TempMax", ": " + "");
                        dep.SetParameterValue("MERPM", ": " + "");
                        dep.SetParameterValue("ENGDEST", ": " + "");
                        dep.SetParameterValue("SLIP", ": " + "");
                        dep.SetParameterValue("MEOUTPUT", ": " + "");
                        dep.SetParameterValue("METHRMALLOAD", ": " + "");
                        dep.SetParameterValue("TC1", ": " + "");
                        dep.SetParameterValue("TC2", ": " + "");
                        dep.SetParameterValue("MESCAV", ": " + "");
                        dep.SetParameterValue("SCAVTEMP", ": " + "");
                        dep.SetParameterValue("LOP", ": " + "");
                        dep.SetParameterValue("SWT", ": " + "");
                        dep.SetParameterValue("ERT", ": " + "");

                        dep.SetParameterValue("AUX1", ": " + "");
                        dep.SetParameterValue("AUX2", ": " + "");
                        dep.SetParameterValue("AUX3", ": " + "");
                        dep.SetParameterValue("AUX4", ": " + "");
                    }
                    #endregion
                }
            }
            else
            {
                lblmessage.Text = "No Record Found.";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        catch { }
    }
    private string getColumnSum(DataTable dt, string ColumnName)
    {
        double val = 0;
        int Mins = 0;
        int part0 = 0, part1 = 0;
        char Mode = '.';
        foreach (DataRow dr in dt.Rows)
        {
            try
            {
                if (dr[ColumnName].ToString().Contains(":"))
                {
                    char[] sep = { ':' };
                    string[] parts = dr[ColumnName].ToString().Split(sep);
                    part0 = int.Parse(parts[0]);
                    part1 = int.Parse(parts[1]);
                    Mins = Mins + (part0 * 60) + part1;
                    Mode = ':';
                }
                else
                {
                    val = val + double.Parse(dr[ColumnName].ToString());
                }
            }
            catch { }
        }
        if (Mode == ':')
        {
            return Convert.ToString(Convert.ToInt32(Mins / 60)).PadLeft(2, '0') + ":" + Convert.ToString(Mins % 60).PadLeft(2, '0');
        }
        else
        {
            return val.ToString();
        }
    }
    private string getColumnAvg(DataTable dt, string ColumnName)
    {
        double val = 0;
        foreach (DataRow dr in dt.Rows)
        {
            try
            {
                val = val + double.Parse(dr[ColumnName].ToString());
            }
            catch { }
        }
        return Convert.ToString(Math.Round((val / dt.Rows.Count), 2));
    }
        
    protected void Page_Unload(object sender, EventArgs e)
    {
        dep.Close();
        dep.Dispose();
    }
}
