<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OperatorReporting_Crystal.aspx.cs" Inherits="Reports_OperatorReporting_Crystal" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                <tr>
                    <td style="text-align: center;">
                        &nbsp;<asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        
                         <table cellpadding="0" cellspacing="0" width="100%">
                            <tr><td colspan="2" style="HEIGHT: 59px">
                               <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedHeader" ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  />
                            </td></tr>
                        </table>
                       <div id="divPrint">
                            &nbsp;</div>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
