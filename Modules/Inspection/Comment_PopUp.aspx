<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comment_PopUp.aspx.cs" Inherits="Transactions_Comment_PopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comment</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function CloseWin()
        {
            var aa = document.getElementById("txtObsComment").value;
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtcomment").value = aa;         
            window.close();
        }
    </script>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <center>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:TextBox ID="txtObsComment" runat="server" Height="407px" TextMode="MultiLine" Width="622px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClientClick="return CloseWin();" /></td>
            </tr>
        </table>   
        </center> 
    </div>
    </form>
</body>
</html>
