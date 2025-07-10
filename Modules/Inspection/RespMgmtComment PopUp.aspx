<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RespMgmtComment PopUp.aspx.cs" Inherits="Transactions_RespMgmtComment_PopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Response/Mgmt. Comment</title>
     <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function SetParent()
        {
            var aa = document.getElementById("txtRespComment").value;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txt_Response").value = aa;         
        }
        function CloseWin()
        {
            SetParent();  
            window.close();
        }
       
    </script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="txtRespComment" runat="server" Height="407px" TextMode="MultiLine" Width="622px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp; &nbsp;</td>
                <td>
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click" Width="59px" />&nbsp;
                    <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Clear" OnClientClick="document.getElementById('txtRespComment').value='';return false;" Width="59px" /></td>
            </tr>
            </table>    
    </div>
    </form>
</body>
</html>
