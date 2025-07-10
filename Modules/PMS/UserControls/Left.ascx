<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Left.ascx.cs" Inherits="UserControls_Left" %>
<div style="text-align : center;background-color : #4371a5;width:177px;min-height:465px; vertical-align : top; padding-bottom :15px;">
<asp:Image ID="Image10" runat="server" ImageUrl="~/Modules/PMS/Images/logo.jpg" Visible ="false"  />
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
<br />
<div style="min-height:200px; width :177px" >
<table style="width:177px; text-align : center " cellpadding="10" cellspacing="0" border="0">
<tr runat="server" id="TrMaintainance">
    <td>
        <a href='<%=Page.ResolveUrl("~/Search.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Modules/PMS/Images/maintenance.jpg")%>'  border="0"/></a>
    </td>
</tr>
<tr runat="server" id="TrShipMaster">
    <td>
        <a href='<%=Page.ResolveUrl("~/Componentlist.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Modules/PMS/Images/shipmaster.jpg")%>'  border="0"/></a> 
    </td>
</tr>
<tr runat="server" id="TrOfficeMaster">
    <td>
        <a href='<%=Page.ResolveUrl("~/JobMaster.aspx")%>'><img alt="" src='<%=Page.ResolveUrl("~/Modules/PMS/Images/officemaster.jpg")%>'  border="0"/></a>
    </td>
</tr>
<tr runat="server" id="TrVesselSetup">
    <td>
        <a href='<%=Page.ResolveUrl("~/VesselSetupMaster.aspx")%>' ><img alt="" src='<%=Page.ResolveUrl("~/Modules/PMS/Images/vesselsetup.jpg")%>'  border="0"/></a> 
    </td>
</tr>
<tr runat="server" id="TrReports">
    <td>
        <img alt="" src='<%=Page.ResolveUrl("~/Modules/PMS/Images/Reports.jpg")%>' border="0"/>
    </td>
</tr>
</table>
</div>
</div>