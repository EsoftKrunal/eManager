<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_CompGuidence.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Popup_CompGuidence" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Guidance</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
        <div style="border:1px solid #d2d2d2;" >
       <table cellpadding="1" cellspacing="1" border="0" width="100%">
         <tr style=" background-color:#d2d2d2; color:#000;">
             <td style="text-align:center;">
                 Guidance
             </td>
         </tr>
         <tr>
             <td style="font-weight:bold;">
                <asp:Label ID="lblComp" runat="server"></asp:Label>
             </td>
         </tr>
         <tr>
             <td >
                <%--<asp:Label ID="lblGuidence" runat="server"></asp:Label>--%>
                <asp:TextBox ID="lblGuidence" ReadOnly="true" TextMode="MultiLine" Height="300px" Width="99%" runat="server"></asp:TextBox>
             </td>
         </tr>
         <tr>
           <td style="text-align:right; padding:5px;">
           <input type="button" value="Close" onclick="javascript:self.close();" />
           </td>
         </tr>

       </table>
       </div>
       </ContentTemplate>
       </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
