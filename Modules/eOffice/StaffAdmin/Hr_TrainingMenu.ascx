<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Hr_TrainingMenu.ascx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_TrainingMenu" %>
<script type ="text/javascript" >
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}
</script>
<link href="style.css" rel="stylesheet" type="text/css" />
<div style=" text-align :left">
<table width="100%">
<tr>
<td>
 <table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
    <td><asp:Button ID="btnAssignTraining" Text="Training Calender" runat="server" CssClass="MenuSelectedTab"  OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:Button></td>
    <td><asp:Button ID="btnSchudeleTraining" Text="Schedule Training" runat="server" CssClass="MenuTab"  OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false"></asp:Button></td>
    <td><asp:Button ID="btnExecuteTrainining" Text="Execute Trainining" runat="server" CssClass="MenuTab"  OnClick="menu_Click" CommandArgument="3" BorderStyle="None" CausesValidation="false"></asp:Button></td>
    
</tr>
</table> 
</td>
<td style="text-align :right">

</td>
</tr>
</table>
</div>