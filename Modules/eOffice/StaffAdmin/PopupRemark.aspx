<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupRemark.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_PopupRemark" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Remark</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">
     <script type="text/javascript" language="javascript">
         function refreshparent() {
             window.opener.RefreshRemark();
         }
     
     </script>
     

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
    <ContentTemplate>
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-collapse:collapse">
        <tr style=" background-color:#d2d2d2; color:#000;">
            <td style="text-align:center; font-weight:bold; padding:6px;">Remarks</td>
        </tr>
        <tr>
             <td >
                <asp:TextBox ID="txtAnswerJR"  runat="server" Height="335px" TextMode="MultiLine" Width="100%" ReadOnly="true"></asp:TextBox>
             </td>
        </tr>
       <%--<tr>
           <td style="text-align:right; padding:5px;">
               <asp:Label ID="lblMsg" Style="color:Red;" runat="server"></asp:Label>
               <asp:Button ID="btnSave" Text="Save" CssClass="btn" runat="server" onclick="btnSave_Click" />&nbsp;
               <input type="button" value="Close" CssClass="btn" onclick="javascript:self.close();" />
           </td>
         </tr>--%>
       </table>
       </div>
       </ContentTemplate>
       </asp:UpdatePanel>
    </form>
</body>
</html>
