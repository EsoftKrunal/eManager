<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HR_ActivityHeader.ascx.cs" Inherits="emtm_Emtm_HR_ActivityHeader" %>

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
<%--<table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
    <td></td>
    <td></td>
    <td></td>
</tr>
</table>--%> 
</td>
<td style="text-align :right">
<table border="1" bordercolor="gray" style=" border-collapse:collapse; float :right ">
   <tr>
      <td ID="td_Personal" runat="server">
         <asp:ImageButton ID="Imgbtn_Personal" style="float:right;" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Emtm/tab_Personal.jpg" OnClick="Imgbtn_Personal_Click"  ToolTip="Personal Details" />
      </td>
      <td ID="td_Document" runat="server">
        <asp:ImageButton ID="Imgbtn_Documents" style="float:right;" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Emtm/tab_Documents.jpg" OnClick="imgbtn_Documents_Click" ToolTip="Crew Documents" />
      </td>
   </tr>
</table>
</td>
</tr>
</table>
</div>