<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchPrint.aspx.cs" Inherits="CrewRecord_SearchPrint" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

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
    <form id="form1" runat="server"  >
      <div style="text-align: center">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
         <table align="center" border="0" cellpadding="0" cellspacing="0" width="825">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5"> Crew List</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 825px">
                                &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 825px">
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 825px;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr><td colspan="2" style=" padding-left : 20px"  >
                                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  />
                                        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  />--%>
                                    </td></tr>
                                </table>
                               <div id="divPrint">
                                    &nbsp;</div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
        </div>
    </form>
</body>
</html>
