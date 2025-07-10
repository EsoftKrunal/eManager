<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Left.ascx.cs" Inherits="UserControls_Left" %>
<div style="text-align : center;background-color : #4371a5;width:177px;min-height:465px; vertical-align : top; padding-bottom :15px;">
<asp:Image ID="Image10" runat="server" ImageUrl="~/Images/logo.jpg" Visible ="false"  />
<script type ="text/javascript" >
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}

</script>


<br />
<div style="min-height:200px; width :177px" >
<table style="width:177px; text-align : center " cellpadding="0" cellspacing="0" border="0">
<tr runat="server" id="Tr2">
    <td>
        <a href='<%=Page.ResolveUrl("~/Requisitions.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/PRLink.jpg")%>'  border="0"/></a>
    </td>
</tr>
<tr runat="server" id="TrPurchaseRequest">
    <td>
        <a href='<%=Page.ResolveUrl("~/ReqFromVessels.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/RFQLink.jpg")%>'  border="0"/></a>
    </td>
</tr>
<tr runat="server" id="Tr1">
    <td>
        <a href='<%=Page.ResolveUrl("~/ApprovalList1.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/approvepomod.jpg")%>'  border="0"/></a>
    </td>
</tr>

<tr runat="server" id="TrPOs">
    <td>
        <a href='<%=Page.ResolveUrl("~/OrdersList.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/order.jpg")%>'  border="0"/></a><%-- onclick="ddaccordion.toggleone('mypets', 4); return false"--%>
    </td>
</tr>

<tr runat="server" id="ApEntries">
    <td>
        <a href='<%=Page.ResolveUrl("~/ApEntries.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/apexport.jpg")%>'  border="0"/></a><%-- onclick="ddaccordion.toggleone('mypets', 4); return false"--%>
    </td>
</tr>

<tr runat="server" id="TrVendors">
    <td>
        <a href='<%=Page.ResolveUrl("~/VendorRequestManagement.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/VendorLink.jpg")%>' border="0"/></a><%-- onclick="ddaccordion.toggleone('mypets', 1); return false"--%>
    </td>
</tr>
<tr runat="server" id="TrMasters">
    <td>
        <a href='<%=Page.ResolveUrl("~/Registers/VesselMaster.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/Registers.jpg")%>'  border="0"/></a>
    </td>
</tr>
<tr runat="server" id="TrReports">
    <td>
        <a href='<%=Page.ResolveUrl("~/Report/ReportMaster.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/Reports.jpg")%>' border="0"/></a><%-- onclick="ddaccordion.toggleone('mypets', 6); return false"--%>
    </td>
</tr>
<tr runat="server" id="TrNWC">
    <td>
        <a href='<%=Page.ResolveUrl("~/NWC/Category.aspx?S=")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/nwc.jpg")%>' border="0"/></a><%-- onclick="ddaccordion.toggleone('mypets', 6); return false"--%>
    </td>
</tr>
<tr runat="server" id="TrInvoice">
    <td>
        <a href='<%=Page.ResolveUrl("~/Invoice/Home.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Images/invoices.jpg")%>' border="0"/></a><%-- onclick="ddaccordion.toggleone('mypets', 6); return false"--%>
    </td>
</tr>
 
<tr runat="server" id="TrRFQs" style="visibility: hidden;">
    <td>
        <a href='<%=Page.ResolveUrl("~/WelcomeUser.aspx")%>' onclick="ddaccordion.toggleone('mypets', 3); return false"><img alt="" src='<%=Page.ResolveUrl("~/Images/RFQLink.jpg")%>'  border="0"/></a>
    </td>
</tr>
<tr runat="server" id="TrInvs" style="visibility: hidden;">
    <td>
        <a href='<%=Page.ResolveUrl("~/WelcomeUser.aspx")%>' onclick="ddaccordion.toggleone('mypets', 5); return false"><img alt="" src='<%=Page.ResolveUrl("~/Images/invoice&payment.jpg")%>' border="0"/></a>
    </td>
</tr>

    

</table>
</div>
</div>