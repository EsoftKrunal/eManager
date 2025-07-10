<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateOrderStautsComment.aspx.cs" Inherits="UpdateOrderStautsComment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<link rel="Stylesheet"  type="text/css" href="../../HRD/Styles/StyleSheet.css"/>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;font-family:Arial, Helvetica, sans-serif;">
        <table width="98%" cellpadding="0" cellspacing="0" style="text-align:center ;">
             <tr>
                <td class="text headerband">
                    <b> Order Status Comments</b>
                </td>
            </tr>
        <tr>
                <td>
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
           
            <tr>
                <td>
                        <asp:TextBox ID="txtOrderStatus" runat="server"  TextMode="MultiLine"  Height="90px" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                      
                     <asp:Button ID="imgSave" runat="server" Text="Save" CssClass="btn" OnClick="imgSave_OnClick"  />
                </td>
            </tr>
        </table>
    <div style="padding:5px">
        Updated By : <asp:Label runat="server" ID="lblupdatedby"></asp:Label>
    </div>
        <div style="padding:5px">
        Updated On : <asp:Label runat="server" ID="lblupdatedon"></asp:Label>
    </div>
    </div>
    </form>
</body>
</html>
