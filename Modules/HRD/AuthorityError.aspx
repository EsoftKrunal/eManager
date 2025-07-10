<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuthorityError.aspx.cs" Inherits="AuthorityError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
   <div style="text-align: center">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table style="width:825px; background-color: #f9f9f9; border-collapse:collapse " border="1" bordercolor="#4371a5" >
            <tr>
                <td style="background-color:#4371a5; height: 23px; text-align:center" class="text" >
                    SYSTEM MESSAGE</td>
            </tr>
            <tr>
                <td style=" background-color: #f9f9f9; height:178px; vertical-align:middle;text-align:center;" >
                    <br />
                    <div style=" text-align:center; vertical-align:middle; font-size:14px; color:Red;">
                    You Are Not Authorized To View This Page.
                        </div>
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
