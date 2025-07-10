<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselOfficerHistory.aspx.cs" Inherits="Reporting_VesselOfficerHistory" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
            <style type="text/css" >  
.fixedbar
{
    position:fixed;
    margin:60px 0px 0px 135px;   
    background-color:#f0f0f0;  
    z-index:100;
    border:solid 1px #5c5c5c;
}
</style>          
</head>
<body>
<form id="form1" runat="server" defaultbutton="btn_Show">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" >
                    Vessel's Officers History</td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 3px; color: red; text-align: center">
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <table cellpadding="2" cellspacing="0" width="100%">
                                    <tr style =" padding :3px;">
                                        <td style="text-align: right; padding-right: 5px">
                                            Vessel :</td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="232px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="Required" ></asp:RequiredFieldValidator></td>
                                        <td style="text-align: right; padding-right: 5px">
                                            &nbsp;</td>
                                        <td style="text-align: left">
                                            &nbsp;</td>
                                        <td style="text-align: left">
                                             <asp:Button ID="btn_Show" runat="server" CssClass="btn"  Text="Show Report" /> </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="text-align: left;">
                                             <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
       </td>
      </tr>
     </table>
</td> </tr> </table>
</form>
</body>
</html>
