<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CB_Menu.ascx.cs" Inherits="StaffAdmin_Compensation_CB_Menu" %>
<script type ="text/javascript" >
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}
</script>
 <link href="../../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
<div style=" text-align :left; padding:5px 5px 0px 5px; border-bottom:solid 5px #ff6a00;">
<table border="0" style=" border-collapse:collapse;" cellspacing="0" cellpadding="0">
<tr>
    <td>
        <asp:Button ID="btnHome" Text="Home" runat="server" CssClass="selbtn"  OnClick="menu_Click" CommandArgument="1"  CausesValidation="false"></asp:Button>
    </td>
    <td>
        <asp:Button ID="btnRegister" Text="Register" runat="server" CssClass="btn1"  OnClick="menu_Click" CommandArgument="2" CausesValidation="false"></asp:Button>
    </td>
    <td>
        <asp:Button ID="btnPaySlip" Text="PaySlip" runat="server" CssClass="btn1"  OnClick="menu_Click" CommandArgument="3" CausesValidation="false"></asp:Button>
    </td>
    <%--<td>
        <asp:Button ID="btnDocument" Text="Documents" runat="server" CssClass="tabmenu"  OnClick="menu_Click" CommandArgument="5" CausesValidation="false"></asp:Button>
    </td>--%>
    <td>
        <asp:Button ID="btnRevision" Text="Revision" runat="server" CssClass="btn1"  OnClick="menu_Click" CommandArgument="4" CausesValidation="false"></asp:Button>
    </td>
</tr>
</table> 
</div>