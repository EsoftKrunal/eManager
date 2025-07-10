<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HR_LeaveSearchHeader.ascx.cs" Inherits="HR_LeaveSearchHeader" %>

<script type ="text/javascript" >
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}
</script>

<div style=" text-align :left">
<table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
    <td><asp:ImageButton ID="btnLeaveSearch" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/Leavesearch_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnHolidayMaster" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/Holiday_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnWeeklyOff" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/woff_g.jpg" OnClick="menu_Click" CommandArgument="3" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnLeaveRequest" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/Leaverequest_g.jpg" OnClick="menu_Click" CommandArgument="4" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnLeaveRegister" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/leaveregister_g.jpg" OnClick="menu_Click" CommandArgument="5" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnperiodClosure" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/period_closure_g.jpg" OnClick="menu_Click" CommandArgument="6" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnAbsencePurpose" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/absense_pur_g.jpg" OnClick="menu_Click" CommandArgument="7" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnComapnyEvent" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/comp_event_g.jpg" OnClick="menu_Click" CommandArgument="8" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnPosition" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/position_g.jpg" OnClick="menu_Click" CommandArgument="9" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
</tr>
</table> 
</div>