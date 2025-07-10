<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleMenu.ascx.cs" Inherits="HRD_UserControls_ModuleMenu" %>
<div style="text-align:center; background-color:#4371a5; width:177px; min-height:465px; vertical-align:top; padding-bottom:15px;" runat="server" id="m1">
<asp:Image ID="Image10" runat="server" ImageUrl="~/Modules/HRD/Images/logo.jpg" Visible ="false"  />
    
<br />
<div style="min-height:200px; width :177px;" >
<table style="width:177px; text-align : center " cellpadding="0" cellspacing="0" border="0">
<tr runat="server" id="tr_CrewParticular">
    <td >
        <a href="../CrewRecord/CrewSearch.aspx"><asp:Image ID="Image1" runat="server" ImageUrl="~/Modules/HRD/Images/CrewParticulars.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_CrewOperation">
    <td>
        <a href="../CrewOperation/CrewPlanning_Menu.aspx"><asp:Image ID="Image2" runat="server" ImageUrl="~/Modules/HRD/Images/CrewOperations.jpg" /></a>
    </td>
</tr><%-->
<tr runat="server" id="tr_Invoice">
  <td>
        <a href="../Invoice/Default.aspx"><asp:Image ID="Image3" runat="server" ImageUrl="~/Modules/HRD/Images/invoices.jpg" /></a>
   </td> 
</tr><--%>
<tr runat="server" id="tr_Accounts">
    <td>
        <a href="../CrewAccounting/Payroll.aspx"><asp:Image ID="Image4" runat="server" ImageUrl="~/Modules/HRD/Images/CrewAccounts.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_Vessel">
    <td>
        <a href="../VesselRecord/VesselSearch.aspx"><asp:Image ID="Image5" runat="server" ImageUrl="~/Modules/HRD/Images/VesselMaster.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_Applicant">
    <td>
        <a href="../Applicant/ApplicantHome.aspx"><asp:Image ID="Image13" runat="server" ImageUrl="~/Modules/HRD/Images/Applicant.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_CrewApproval">
    <td>
        <a href="../CrewApproval/CrewApprovalHome.aspx"><asp:Image ID="Image6" runat="server" ImageUrl="~/Modules/HRD/Images/CrewApproval.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_CrmActivities">
    <td>
        <a href="../CRMActivities/CRMHome.aspx"><asp:Image ID="Image14" runat="server" ImageUrl="~/Modules/HRD/Images/CrewWelfare.jpg" /></a> 
    </td>
</tr>
<tr runat="server" id="tr_Registers">
    <td>
        <a href="../Registers/Blank.aspx"><asp:Image ID="Image7" runat="server" ImageUrl="~/Modules/HRD/Images/Registers.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_Training">
    <td>
        <a href="../CrewOperation/TrainingMgmt.aspx?reset=1"><asp:Image ID="Image8" runat="server" ImageUrl="~/Modules/HRD/Images/crewtraining.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_CrewAppraisal">
    <td>
        <a href="../CrewAppraisal/CrewAppraisal.aspx"><asp:Image ID="Image11" runat="server" ImageUrl="~/Modules/HRD/Images/crewappraisal.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_CrewRestHr">
    <td>
        <a href="../Resthour/RestHourOldNew.aspx"><asp:Image ID="Image12" runat="server" ImageUrl="~/Modules/HRD/Images/resthours.jpg" /></a>
    </td>
</tr>

    <%--<tr runat="server" id="tr_CrewRestHr_N">
    <td>
        <a href="../Resthour/CrewList_N.aspx"><asp:Image ID="Image12_N" runat="server" ImageUrl="~/Modules/HRD/Images/resthours.jpg" /></a>
    </td>
</tr>--%>

    <tr runat="server" id="tr_Seminar">
    <td>
        <a href="../Seminar/Seminar.aspx" title="Seminar"><asp:Image ID="Image15" runat="server" ImageUrl="~/Modules/HRD/Images/seminar.png" /></a>
    </td>
</tr>



<tr runat="server" id="tr_Reporting">
    <td>
        <a href="../Reporting/Blank.aspx"><asp:Image ID="Image9" runat="server" ImageUrl="~/Modules/HRD/Images/Reports.jpg" /></a>
    </td>
</tr>

</table>
</div>

</div>