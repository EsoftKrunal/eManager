<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateRFQ.aspx.cs" Inherits="CreateRFQ" MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Modules/Purchase/UserControls/SpareRFQ.ascx" TagName="Spare" TagPrefix="ucspare"%>
<%@ Register Src="~/Modules/Purchase/UserControls/StoreRFQ.ascx" TagName="Store" TagPrefix="ucstore"%>
<%@ Register Src="~/Modules/Purchase/UserControls/LGRFQ.ascx" TagName="LG" TagPrefix="uclg"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">--%>
   <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../JS/Common.js"></script>
    <script type="text/javascript" src="../JS/jquery_v1.10.2.min.js"></script>
  <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Calender.js" type="text/javascript"></script>
    <script src="../JS/jquery.datetimepicker.js" type="text/javascript"></script>
    <link href="../CSS/jquery.datetimepicker.css" rel="stylesheet" type="text/css"  />
        <script type="text/javascript">
            function ReloadPage()
            {
//                alert('dddddddd');return;
                parent.window.document.getElementById("ctl00_ContentMainMaster_StoreRFQ_BtnReload").click();
            }

            
        </script>

    <style type="text/css">
        .controlarea
{display:inline; text-align:left;  empty-cells:show; }

    </style>
    </asp:Content>
<%--</head>
<body>
    <form id="form1" runat="server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
        <ucspare:Spare ID="SpareRFQ" runat="server" />
        <ucstore:Store ID="StoreRFQ" runat="server" />
        <uclg:LG ID="LGRFQ" runat="server" />
    </div>
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="text-align: left;">
                        
                    </td>
                    <td style="text-align: right;">         
                        <asp:Button ID="imgCancel" runat="server"  OnClick="imgCancel_OnClick" Text ="Close" CssClass="btn" /> &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </asp:Content>
  <%--  </form>
</body>
</html>--%>

