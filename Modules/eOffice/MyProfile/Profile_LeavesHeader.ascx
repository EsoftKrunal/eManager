<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Profile_LeavesHeader.ascx.cs" Inherits="emtm_MyProfile_Emtm_Profile_LeavesHeader" %>

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
    <td><asp:ImageButton ID="btnLeaveStatus" runat="server" ImageUrl="~/Modules/eOffice/Images/leave_status_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnLeaveApproval" runat="server" ImageUrl="~/Modules/eOffice/Images/leave_approval_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
</tr>
</table> 
</div>