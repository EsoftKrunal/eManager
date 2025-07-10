<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HR_TravelDocument.ascx.cs" Inherits="emtm_Emtm_HR_TravelDocument" %>
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
    <td><asp:ImageButton ID="btntraveldoc" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/travel_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnCertificate" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/certificates_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnMedical" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/medical_g.jpg" OnClick="menu_Click" CommandArgument="3" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnOtherDocuments" Visible="false" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/otherdocument_g.jpg" OnClick="menu_Click" CommandArgument="4" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
</tr>
</table> 
</td>
<td style="text-align :right">
<table border="1" bordercolor="gray" style=" border-collapse:collapse; float :right ">
   <tr>
      <td ID="td_Personal" runat="server">
         <asp:ImageButton ID="Imgbtn_Personal" style="float:right;" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Emtm/tab_Personal.jpg" OnClick="Imgbtn_Personal_Click" ToolTip="Personal Details" />
      </td>
      <td ID="td_Activity" runat="server">
        <asp:ImageButton ID="Imgbtn_Activity" style="float:right;" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Emtm/tab_Acitvity.jpg" OnClick="Imgbtn_Activity_Click" ToolTip="Employee Activity" />
      </td>
   </tr>
</table>
</td>
</tr>
</table>
</div>