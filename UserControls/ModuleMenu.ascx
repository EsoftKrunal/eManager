<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleMenu.ascx.cs" Inherits="UserControls_ModuleMenu" %>
<table style="min-height: 100px; text-align: center" cellpadding="2" cellspacing="0" border="0">
    <tr>
        <td>
            <a href="#">
                <asp:Image ID="btn_CMS" runat="server" ImageUrl="~/Modules/HRD/Images/buttons/cms1.png" onmouseover="this.src='Images/buttons/cms2.png'" onmouseout="this.src='Images/buttons/cms1.png'" onclick="return parent.DoPost(1);" /></a>
        </td>
        <td>
            <a href="#">
                <asp:Image ID="btn_VIMS" runat="server" ImageUrl="~/Modules/HRD/Images/buttons/vims1.png" onmouseover="this.src='Images/buttons/vims2.png'" onmouseout="this.src='Images/buttons/vims1.png'" onclick="return parent.DoPost(2);" /></a>
        </td>
        <td>
            <a href="#">
                <asp:Image ID="btn_POS" runat="server" ImageUrl="~/Modules/HRD/Images/buttons/pos1.png" onmouseover="this.src='Images/buttons/pos2.png'" onmouseout="this.src='Images/buttons/pos1.png'" onclick="return parent.DoPost(3);" /></a>
        </td>
        <td>
            <a href="#">
                <asp:Image ID="btn_PMS" runat="server" ImageUrl="~/Modules/HRD/Images/buttons/pms1.png" onmouseover="this.src='Images/buttons/pms2.png'" onmouseout="this.src='Images/buttons/pms1.png'" onclick="return parent.DoPost(4);" /></a>
        </td>
        <td>
            <a href="#">
                <asp:Image ID="btn_HRD" runat="server" ImageUrl="~/Modules/HRD/Images/buttons/hrd1.png" onmouseover="this.src='Images/buttons/hrd2.png'" onmouseout="this.src='Images/buttons/hrd1.png'" onclick="return parent.DoPost(5);" /></a>
        </td>
        <td>
            <a href="#">
                <asp:Image ID="btn_EWeb" runat="server" ImageUrl="~/Modules/HRD/Images/buttons/eweb1.png" onmouseover="this.src='Images/buttons/eweb2.png'" onmouseout="this.src='Images/buttons/eweb1.png'" onclick="return parent.DoPost(5);" /></a>
        </td>
        <td>
            <a href="#">
                <asp:Image ID="btn_ADMIN" runat="server" ImageUrl="~/Modules/HRD/Images/buttons/admin1.png" onmouseover="this.src='Images/buttons/admin2.png'" onmouseout="this.src='Images/buttons/admin1.png'" onclick="return parent.DoPost(5);" /></a>
        </td>
    </tr>
</table>
