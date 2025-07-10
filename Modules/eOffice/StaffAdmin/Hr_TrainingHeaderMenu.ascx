<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Hr_TrainingHeaderMenu.ascx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_TrainingHeaderMenu" %>
<script type ="text/javascript" >
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}
</script>
<link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<div style=" text-align :left">
<table width="100%">
<tr>
<td>
 <table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
    <td><asp:Button ID="btnTrainingGroup" Text="Training Group" runat="server" CssClass="selbtn"  OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:Button></td>
    <td><asp:Button ID="btnTrainingMaster" Text="Training Master" runat="server" CssClass="btn1"  OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false"></asp:Button></td>
    <td><asp:Button ID="btnTrainingPositionGroup" Text="Training Position Group" runat="server" CssClass="btn1"  OnClick="menu_Click" CommandArgument="3" BorderStyle="None" CausesValidation="false"></asp:Button></td>
</tr>
</table> 
</td>
<td style="text-align :right">
<%--<table border="1" bordercolor="gray" style=" border-collapse:collapse; float :right ">
   <tr>
      <td ID="td_Documents" runat="server">
        <asp:ImageButton ID="Imgbtn_Documents" style="float:right;" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Emtm/tab_Documents.jpg" OnClick="imgbtn_Documents_Click" ToolTip="Crew Documents" />
      </td>
      
   </tr>
</table>--%>
</td>
</tr>
</table>
</div>
