<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="InspMisc_Report.aspx.cs" Inherits="Reports_InspMisc_Report" Title="Inspection Misc. Report" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />   
    <script language="javascript" type="text/javascript">
        month = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(",");
        function checkDate(theField) {
            dPart = theField.value.split("-");
            if (dPart.length != 3) {
                alert("Enter Date in this format: dd mmm yyyy!");
                theField.focus();
                return false;
            }
            var check = 0;
            for (i = 0; i < month.length; i++) {
                if (dPart[1].toLowerCase() == month[i].toLowerCase()) {
                    check = 1;
                    dPart[1] = i;
                    break;
                }
            }
            if (check == 0) {
                alert("Enter Date in this format: dd mmm yyyy!");
                return false;
            }
            nDate = new Date(dPart[2], dPart[1], dPart[0]);
            // nDate = new Date(dPart[0], dPart[1], dPart[2]);
            if (isNaN(nDate) || dPart[2] != nDate.getFullYear() || dPart[1] != nDate.getMonth() || dPart[0] != nDate.getDate()) {
                alert("Enter1 Date in this format: dd mmm yyyy!");
                theField.select();
                theField.focus();
                return false;
            } else {
                return true;
            }
        }
        function ValidateDate() {
            if (document.getElementById('txtfromdate').value == '') {
                alert("Please Enter From Date!");
                document.getElementById('txtfromdate').focus();
                return false;
            }
            if (!checkDate(document.getElementById('txtfromdate')))
                return false;
            if (document.getElementById('txttodate').value == '') {
                alert("Please Enter To Date!");
                document.getElementById('txttodate').focus();
                return false;
            }
            if (!checkDate(document.getElementById('txttodate')))
                return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family:Arial;font-size:12px;">
        <tr>
            <td align="center" valign="top">
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td align="center" class="text headerband" style="height: 23px; ">
                            Inspection Misc. Report</td>
                    </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-right: 10px; color: red; text-align: center">
                                            <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td rowspan="41" style="padding-left: 10px" valign="top">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>Available Fields</td>
                                                                <td></td>
                                                                <td>Selected Fields</td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:ListBox ID="lst_AvailFlds" runat="server" Width="193px" SelectionMode="Multiple" Height="180px">
                                                                        <asp:ListItem Value="(SELECT FirstName+' '+MiddleName+' '+LastName FROM dbo.CrewpersonalDetails WHERE CrewId=tid.AssistantEngineer) AS FirstAsstEng">1A/E</asp:ListItem>
                                                                        <asp:ListItem Value="(SELECT FirstName+' '+MiddleName+' '+LastName FROM dbo.CrewpersonalDetails WHERE CrewId=tid.SecondOffice) AS SecondOff">2/O</asp:ListItem>
                                                                        <asp:ListItem Value="AttendedByMTM=case when tis.Attending='True' then 'Yes' when tis.Attending='False' then 'No' else ' ' end">Attended By MTM Super</asp:ListItem>
                                                                        <asp:ListItem Value="(SELECT FirstName+' '+MiddleName+' '+LastName FROM dbo.CrewpersonalDetails WHERE CrewId=tid.ChiefEngineer) AS ChiefEng">C/E</asp:ListItem>
                                                                        <asp:ListItem Value="(SELECT FirstName+' '+MiddleName+' '+LastName FROM dbo.CrewpersonalDetails WHERE CrewId=tid.ChiefOfficer) AS ChiefOfficer">C/O</asp:ListItem>
                                                                        <asp:ListItem Value="mc.ChapterName">Chapter Name</asp:ListItem>
                                                                        <asp:ListItem Value="tob.CorrectiveActions">Corrective Actions</asp:ListItem>
                                                                        <asp:ListItem Value="0 as CrewNo">Crew#</asp:ListItem>
                                                                        <asp:ListItem Value="tob.Deficiency">Deficiency</asp:ListItem>
                                                                        <asp:ListItem Value="REPLACE(CONVERT(VARCHAR,tob.ClosedDate,106),' ','-') as DefClosedDate">Deficiency Closed Date</asp:ListItem>
                                                                        <asp:ListItem Value="DATEDIFF(DD,CONVERT(VARCHAR(10),tid.StartDate,101),CONVERT(VARCHAR(10),tid.ActualDate,101)) AS DurationofInsp">Duration of Inspection</asp:ListItem>
                                                                        <asp:ListItem Value="(select count(flaws) from t_Observations tobs where flaws='People' and tobs.InspectionDueId=tid.Id) as FlawsbyPeople">Flaws By People</asp:ListItem>
                                                                        <asp:ListItem Value="(select count(flaws) from t_Observations tobs where flaws='Process' and tobs.InspectionDueId=tid.Id) as FlawsbyProcess">Flaws By Process</asp:ListItem>
                                                                        <asp:ListItem Value="(select count(flaws) from t_Observations tobs where flaws='Technology' and tobs.InspectionDueId=tid.Id) as FlawsbyTechnology">Flaws By Technology</asp:ListItem>
                                                                        <asp:ListItem Value="HighRisk=case when tob.HighRisk='True' then 'Yes' when tob.HighRisk='False' then 'No' else ' ' end">High Risk</asp:ListItem>
                                                                        <asp:ListItem Value="tid.InspectionNo">Inspection#</asp:ListItem>
                                                                        <asp:ListItem Value="REPLACE(CONVERT(VARCHAR,tid.ActualDate,106),' ','-') as ActualDate">Inspection Done Date</asp:ListItem>
                                                                        <asp:ListItem Value="mig.Code as InspGroupCode">Inspection Group</asp:ListItem>
                                                                        <asp:ListItem Value="mis.Code as InspCode">Inspection Name</asp:ListItem>
                                                                        <asp:ListItem Value="InspectionResult=case when tid.InspectionCleared='True' then 'Pass' when tid.InspectionCleared='False' then 'Fail' else ' ' end">Inspection Result</asp:ListItem>
                                                                        <asp:ListItem Value="REPLACE(CONVERT(VARCHAR,tid.StartDate,106),' ','-') as StartDate">Inspection Start Date</asp:ListItem>
                                                                        <asp:ListItem Value="tid.Inspector">Inspector Name</asp:ListItem>
                                                                        <asp:ListItem Value="(SELECT FirstName+' '+MiddleName+' '+LastName FROM dbo.CrewpersonalDetails WHERE CrewId=tid.Master) AS Master">Master</asp:ListItem>
                                                                        <asp:ListItem Value="(ul.FirstName+' '+ul.LastName) as Supt">Supt</asp:ListItem>
                                                                        <asp:ListItem Value="NCR=case when tob.NCR='True' then 'Yes' when tob.NCR='False' then 'No' else ' ' end">NCR</asp:ListItem>
                                                                        <asp:ListItem Value="tob.ObservationStatus">Observation Status</asp:ListItem>
                                                                        <asp:ListItem Value="(select PscCode from [dbo].[m_PscCode] WHERE ID=tob.PscCode) AS PSCCode">PSC Code</asp:ListItem>
                                                                        <asp:ListItem Value="(select Deficiency from t_Observations obs where obs.Closed='False' and obs.InspectionDueId=tid.Id) as OpenDeficiency">Open Deficiency</asp:ListItem>
                                                                        <asp:ListItem Value="tid.ActualLocation as PortDone">Port Done</asp:ListItem>
                                                                        <asp:ListItem Value="(SELECT top 1 CountryNAME FROM DBO.Country C WHERE C.CountryId IN (select top 1 P.COUNTRYID from dbo.port P WHERE P.PORTNAME=tid.ActualLocation)) as Country">Country</asp:ListItem>
                                                                        <asp:ListItem Value="mq.QuestionNo">Question#</asp:ListItem>
                                                                        <asp:ListItem Value="QuestionType=case when mq.QuestionType=1 then 'Statutory' when mq.QuestionType=2 then 'Recommended' when mq.QuestionType=3 then 'Desirable' else ' ' end">Question Type</asp:ListItem>
                                                                        <asp:ListItem Value="tob.Response">Response</asp:ListItem>
                                                                        <asp:ListItem Value="REPLACE(CONVERT(VARCHAR,tob.TargetCloseDt,106),' ','-') as TargetClosedDt">Target Closed Date</asp:ListItem>
                                                                        <asp:ListItem Value="(select sum(amount) from t_SuperIntendentExpenses tse where tse.InspectionDueId=tid.Id) as TotalExpense">Total Expense</asp:ListItem>
                                                                        <asp:ListItem Value="(select count(Deficiency) from t_Observations where InspectionDueId=tid.Id and Deficiency&lt;&gt;'' and ObservationStatus='NO') as TotalObsv">Total Obsv</asp:ListItem>
                                                                        <asp:ListItem Value="(select count(QuestionId) from t_Observations where InspectionDueId=tid.Id) as TotalQuestions">Total Questions</asp:ListItem>
                                                                        <asp:ListItem Value="vsl.VesselName">Vessel Name</asp:ListItem>
                                                                        <asp:ListItem Value="mq.ShellScore">Risk Factor</asp:ListItem>
                                                                        <asp:ListItem Value="(case when tis.attending=1 then 'Yes' Else 'No' End) as InspAttendance">Insp Attendance</asp:ListItem>

                                                                    </asp:ListBox></td>
                                                                <td style="padding-right: 4px; padding-left: 4px">
                                                                    <asp:Button ID="btn_Frwd" runat="server" CssClass="btn" Text=">" OnClick="btn_Frwd_Click" /><br />
                                                                    <br />
                                                                    <asp:Button ID="btn_Rev" runat="server" CssClass="btn" Text="<" OnClick="btn_Rev_Click" /></td>
                                                                <td valign="top">
                                                                    <asp:ListBox ID="lst_SelFlds" runat="server" SelectionMode="Multiple" Width="193px" Height="180px"></asp:ListBox></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="text-align: right">Insp. Group :</td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddl_InspGroup" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_InspGroup_SelectedIndexChanged" Width="228px">
                                                        </asp:DropDownList></td>
                                                    <td style="text-align: right">Owner :</td>
                                                    <td style="text-align: left">
                                                        <asp:DropDownList ID="ddl_Owner" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_Owner_SelectedIndexChanged" Width="228px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top">Insp. Name :</td>
                                                    <td style="text-align: left" valign="top">
                                                        <asp:DropDownList ID="ddl_InspName" runat="server" CssClass="input_box" Width="228px">
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td style="text-align: right" valign="top">Fleet :</td>
                                                    <td style="text-align: left" valign="top">
                                                        <asp:DropDownList ID="ddlFleet" runat="server" CssClass="input_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_Fleet_SelectedIndexChanged" Width="228px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <%--OnClientClick="return ValidateDate();"--%>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top">Period :
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" Width="89px"></asp:TextBox>
                                                        -
                                                <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" Width="89px"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right" valign="top">Vessel :
                                                    </td>
                                                    <td valign="top">
                                                        <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="228px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top">
                                                        <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_Show_Click" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td rowspan="1" style="padding-left: 10px"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                    <td style="text-align: right" valign="top"></td>
                                                    <td valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align: center">
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtfromdate"></ajaxToolkit:CalendarExtender>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txttodate"></ajaxToolkit:CalendarExtender>
                                                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Export To Excel" CssClass="btn" Visible="False" />
                                                        <%--<asp:Button id="Button2" runat="server" OnClick="Button2_Click" Text="Export To PDF" CssClass="btn" Visible="False" />--%></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align: left">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="padding-right: 10px; padding-left: 10px">
                                                        <%--<cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" DisplayGroupTree="False"></cr:crystalreportviewer>--%>
                                                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                            Height="156px" Width="100%">
                                                        </rsweb:ReportViewer>
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="divPrint">
                                                &nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

