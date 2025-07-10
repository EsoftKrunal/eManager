<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Incident.ascx.cs" Inherits="Incident" %>
<style type="text/css">
.c1 center table tr td label
{
    color:White;
}
</style>
<table cellpadding="0" cellspacing="0" style="width: 100%" border="0">
    <tr>
        <td style="text-align:center;background-color :#4371a5; color:White; " class="c1">
        <center>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" ForeColor="White" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="1">Near Miss</asp:ListItem>
                <asp:ListItem Value="2">Accidents</asp:ListItem>
            </asp:RadioButtonList>
            </center>
        </td>        
    </tr>
</table>
