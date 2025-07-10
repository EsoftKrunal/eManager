<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleMenu.ascx.cs" Inherits="UserControls_ModuleMenu" %>
<div style="text-align : center;background-color : #4371a5;width:180px;min-height:505px; vertical-align : top; padding-bottom :15px;display:block;" runat="server" id="main_Menu">
<asp:Image ID="Image6" runat="server" ImageUrl="~/Images/logo.jpg" Visible ="false" />
<br />
<%--<div style=" background-image :url(<%=cUrl%>menu_top.jpg); height : 25px; width:180px; " >&nbsp;</div>--%>
<table style="width:180px;min-height:285px; text-align : center " cellpadding="2" cellspacing="0" border="0">
<tr runat="server" id="tr_VIMS">
    <td>
        <a runat="server" href="~/Transactions/InspectionSearch.aspx?Session=Clear"><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/vims.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_Tracker">
    <td>
        <a runat="server" href="~/FormReporting/FollowUpList.aspx"><asp:Image ID="Image2" runat="server" ImageUrl="~/Images/tracker.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_Reports" visible="false">
    <td>
        <a runat="server" href="~/Reports/InspMisc_Report.aspx"><asp:Image ID="Image3" runat="server" ImageUrl="~/Images/reports.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_Vessel_Cert">
    <td>
        <a runat="server" href="~/VesselCertificate/VesselCertificate.aspx"><asp:Image ID="Image4" runat="server" ImageUrl="~/Images/certificates.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_Registers">
    <td>
        <a runat="server" href="~/Registers/InspectionGroup.aspx"><asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Registers.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_PositionRep">
    <td>
        <a id="A1" runat="server" href="~/PositionReporting/Home.aspx"><asp:Image ID="Image7" runat="server" ImageUrl="~/Images/vpos.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_PositionReport">
    <td>
         <a id="A7" runat="server" href="~/PositionReport/PR.aspx"><asp:Image ID="Image13" runat="server" ImageUrl="~/Images/vposnew.jpg" /></a>           
    </td>
</tr>

<tr runat="server" id="tr_PositionRepNew" style="display:none;">
    <td>
         <a id="A3" runat="server" href="~/vpr/Home.aspx"><asp:Image ID="Image9" runat="server" ImageUrl="~/Images/vpos.jpg" /></a>           
    </td>
</tr>
<tr runat="server" id="tr_OfficeVisit">
    <td>
        <a id="A2" runat="server" href="~/OfficeVisit/OfficeVisitSearch.aspx" ><asp:Image ID="Image8" runat="server" ImageUrl="~/Images/officevisit.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_MRV"> 
    <td>
        <a id="A48" runat="server" href="~/MRV/Home.aspx" ><asp:Image ID="Image14" runat="server" ImageUrl="~/Images/mrv.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_InruranceRecMgmt"> 
    <td>
        <a id="A5" runat="server" href="~/InsuranceRecordManagement/InsuranceHome.aspx" ><asp:Image ID="Image11" runat="server" ImageUrl="~/Images/Insurance.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_ManForms"> 
    <td>
        <a id="A6" runat="server" href="~/SMS/ReadManuals.aspx" ><asp:Image ID="Image12" runat="server" ImageUrl="~/Images/manuls_forms.jpg" /></a>
    </td>
</tr>
<tr runat="server" id="tr_Vettingmgmt"> 
    <td>
        <a id="A4" runat="server" href="~/Vetting/VIQHome.aspx" ><asp:Image ID="Image10" runat="server" ImageUrl="~/Images/vetting.jpg" /></a>
    </td>
</tr>



</table>
</div> 
<script type="text/javascript">
    if (window.parent.document.getElementById("IframeDetails") != null) {
        //alert(document.getElementById("ctl00_menu2_main_Menu"));
        document.getElementById("ctl00_menu2_main_Menu").style.display='none';
    }
</script>

