<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_TrainingReports.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_HR_TrainingReports" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>
<%@ Register Src="~/Modules/eOffice/StaffAdmin/Hr_TrainingMainMenu.ascx" TagName="Hr_TrainingMainMenu" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function ViewPlaning(pid) {
            document.getElementById("txtHidden").setAttribute("value", pid);
            document.getElementById("btnHidden").click();
            return false;
        }
        function OpenPlanningDetails(TPId) {
            window.open('../StaffAdmin/Emtm_PopUpTrainingDetails.aspx?TrainingPlanningId=' + TPId, '', '');
        }
        function OpenRecomm(year, month, mode) {
            window.open('../StaffAdmin/Emtm_PopUpRequirement.aspx?Year=' + year + '&Month=' + month + '&Mode=' + mode, '', '');
        }
        function Refresh() {
            var btn = document.getElementById("btnRefresh");
            btn.click();
        }
        function print() {
            var yearobj = document.getElementById('ddlYear');
            var year = yearobj.options[yearobj.selectedIndex].value;

            window.open('../../Reporting/TrainingReport.aspx?Year=' + year + '&EmpId=0', '', '');
        }
        function Employeewisereport() {
            var yearobj = document.getElementById('ddlYear');
            var year = yearobj.options[yearobj.selectedIndex].value;

            window.open('Hr_EmployeewiseTrainingReport.aspx?Year=' + year, '', '');
        }
    </script>

    <div style="font-family:Arial;font-size:12px;">
   
         <asp:UpdatePanel runat="server" ID="up1">
         <ContentTemplate>            
            <table width="100%" cellpadding="2" cellspacing="0" border="0">
                                        <tr>
                        
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                             <div>
                                <uc1:Hr_TrainingMainMenu ID="Emtm_Hr_TrainingMainMenu1" runat="server" />
                             </div> 
                            </td>
                        </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr>
                        <td style=" height:25px; padding-left:20px; background-color:#e2e2e2;">
                            <b>Select Year :</b>&nbsp;<asp:DropDownList ID="ddlYear" runat="server" Font-Size="13px" Height="20px"></asp:DropDownList>                            
                        </td>
                        </tr>
                        </table> 
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                        <tr>
                        <td style="padding:20px; text-align:left;">
                            <asp:LinkButton ID="lnkMonthlytrainingReport" runat="server" Text="Month Wise Training Report" CssClass="btn" OnClientClick="javascript:print();" />
                            
                        </td>
                        <td>
                           <asp:LinkButton ID="lnkEmployeeWiseTrainingReport" runat="server" Text="Employee Wise Training Report" CssClass="btn" OnClientClick="javascript:Employeewisereport();" />
                        </td>
                        </tr>
                        </table>
                        </td>
                    </tr>

            </table>
        </ContentTemplate>
        </asp:UpdatePanel>      

    
    </div>
</asp:Content>
