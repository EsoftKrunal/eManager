<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Profile_PersonalHeaderMenu.ascx.cs" Inherits="Emtm_Profile_PersonalHeaderMenu" %>
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
<table width="100%">
<tr>
<td>
 <table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
    <td><asp:ImageButton ID="btngeneral" runat="server" ImageUrl="~/Modules/eOffice/Images/general_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btncontact" runat="server" ImageUrl="~/Modules/eOffice/Images/contact_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnfamily" runat="server" ImageUrl="~/Modules/eOffice/Images/family_g.jpg" OnClick="menu_Click" CommandArgument="3" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnexperience" runat="server" ImageUrl="~/Modules/eOffice/Images/experience_g.jpg" OnClick="menu_Click" CommandArgument="4" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <%--<td><asp:ImageButton ID="btnTrainingRecord" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/general_g.jpg" OnClick="menu_Click" CommandArgument="5" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>--%>
    <td><asp:Button ID="btnTrainingRecord" runat="server" Text="Training Record" style="height:30px; background-image:url(../../Images/notepad20x20.png); background-repeat:no-repeat; background-position:2px 4px; font-weight:bold; text-align:right;"  OnClick="menu_Click1" CommandArgument="5" BorderStyle="None" CausesValidation="false" Width="140px"></asp:Button></td>
</tr>
</table> 
</td>
<td style="text-align :right">
<table border="1" bordercolor="gray" style=" border-collapse:collapse; float :right ">
   <tr>
      <td ID="td_Documents" runat="server">
        <asp:ImageButton ID="Imgbtn_Documents" style="float:right;" runat="server" CausesValidation="False" ImageUrl="~/Modules/eOffice/Images/tab_Documents.jpg" OnClick="imgbtn_Documents_Click" ToolTip="Employee Documents" />
      </td>
      
   </tr>
</table>
</td>
</tr>
</table>
</div>


