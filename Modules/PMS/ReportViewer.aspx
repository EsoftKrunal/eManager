<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer.aspx.cs" Inherits="ReportViewer" MasterPageFile="~/MasterPage.master" %>

<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script src="JS/Common.js" type="text/javascript"></script>
     <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style=" text-align :center; padding-top :4px;" class="text headerband"  >
        Reports
        </div>
        <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
     <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="text-align:left;font-size:13px;">
                <ul>
                    <li><a runat="server" id="ancSparelist" href="~/Modules/PMS/Reports/SpareList.aspx" target="_blank" title="Show Report">Component Spares</a></li>
                    <li><a runat="server" id="ancClassCompJobStatus" href="~/Modules/PMS/Reports/ClassComponentJobStatus.aspx" target="_blank" title="Show Report">Class Component Job Status</a></li>
                    <li><a runat="server" id="ancHullMonitering" href="~/Modules/PMS/Reports/HullMoniteringReport.aspx" target="_blank" title="Show Report">Hull Monitoring</a></li>
                    <li><a runat="server" id="a2" href="~/Modules/PMS/Reports/DryDockJobList.aspx" target="_blank" title="Show Report">Dry Dock Job List</a></li>
                    <li><a runat="server" id="ankCommLog" href="~/Modules/PMS/Reports/PMSCommunicationLog.aspx" target="_blank" title="Show Report">PMS Communication Log</a></li>                    
                    <li><a runat="server" id="ancPMSStatus" href="~/Modules/PMS/Reports/PMSStatusReport.aspx" target="_blank" title="Show Report">PMS Status Report</a></li>                    
                    <li><a runat="server" id="a3" href="MaintenanceKPI_Publish.aspx" target="_blank" title="Show Report"> Maintainance KPI</a></li>
                    <li><a runat="server" id="ancJobHistory" href="~/Modules/PMS/Reports/JobHistory.aspx" target="_blank" title="Show Report">Job History</a></li>
                    <li><a runat="server" id="ancFile" href="~/Modules/PMS/Reports/Jobdonebyperiod.aspx" target="_blank" title="Show Report">Job Update Report By Period</a></li>
                    <li><a runat="server" id="a4" href="~/Modules/PMS/Reports/JOBVerificationReport.aspx" target="_blank" title="Show Report"> Verification Job Report</a></li>
                    <li><a runat="server" id="a1" href="~/Modules/PMS/Reports/JOBPostPoneReport.aspx" target="_blank" title="Show Report" visible="false"> Postpone Job Report</a></li>
                    <li><a runat="server" id="a5" href="~/Modules/PMS/Reports/JOBRejectionReport.aspx" target="_blank" title="Show Report" visible="false"> Jon Rejection Report</a></li>
                </ul>
               
            </td>
        </tr>
        </table>
    
    </div>
    </asp:Content>
