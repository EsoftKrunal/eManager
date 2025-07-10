<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotAuthorized.aspx.cs" Inherits="SMS_NotAuthorized" %>
<%@ Register src="ManualMenu.ascx" tagname="ManualMenu" tagprefix="uc2" %>
<%@ Register src="SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<%@ Register src="SMSSubTab.ascx" tagname="SMSSubTab" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <uc3:SMSManualMenu ID="ManualMenu2" runat="server" />
    <uc4:SMSSubTab ID="SMSManualMenu1" runat="server" />
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="color:Red; font-size:22px; font-weight:bold; height:500px; text-align:center; vertical-align:middle;"> You are not authorized to view this page. </td>
     </tr>
    </table>
    </div>
    </form>
</body>
</html>
