<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HR_Leave_Header.ascx.cs" Inherits="emtm_Emtm_HR_Leave_Header" %>

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
    <td><asp:ImageButton ID="btnleavestatus" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/leavestatus_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnleavehistory" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/leavehistory_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None"></asp:ImageButton></td>
</tr>
</table> 
</div>