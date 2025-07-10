<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RespQuestion_Report.aspx.cs" Inherits="Reports_InspCheckList_Report" Title="Question Details Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        .fixedbar {
            position: fixed;
            margin: 0px 0px 0px 0px;
            background-color: #f0f0f0;
            z-index: 100;
            border: solid 1px #5c5c5c;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
            <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center" valign="top" style="height: 235px">
                        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid; border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align: center"
                            width="100%">
                            <tr>
                                <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5">Question Details
               
               
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <table width="500px" cellpadding="2" cellspacing="0" border="0" align="center">
                                        <td>Open Search : </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txt_OpenSerch" MaxLength="50"
                                                CssClass="input_box" Width="273px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnPost" runat="server" CssClass="btn" Text="Show"
                                                OnClick="btnPost_Click" Width="87px" />
                                        </td>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="padding-left: 20px; text-align: left">
                                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" />
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
