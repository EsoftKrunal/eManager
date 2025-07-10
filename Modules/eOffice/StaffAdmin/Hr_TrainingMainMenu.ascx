<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Hr_TrainingMainMenu.ascx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_TrainingMainMenu" %>
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
    <td><asp:Button ID="btnTrainingMatrix" Text="Training Matrix" runat="server" CssClass="selbtn"  OnClick="menu_Click" CommandArgument="4" BorderStyle="None" CausesValidation="false"></asp:Button></td>
    <td><asp:Button ID="btnTraining" Text="Training Management" runat="server" CssClass="btn1"  OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:Button></td>
    <td><asp:Button ID="btnRegisters" Text="Registers" runat="server" CssClass="btn1"  OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false"></asp:Button></td>
    <td><asp:Button ID="btnReports" Text="Reports" runat="server" CssClass="btn1"  OnClick="menu_Click" CommandArgument="3" BorderStyle="None" CausesValidation="false"></asp:Button></td>
    
        
</tr>
</table> 
</td>
<td style="text-align :right">
</td>
</tr>
</table>
</div>