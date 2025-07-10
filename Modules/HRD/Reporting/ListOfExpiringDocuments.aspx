<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListOfExpiringDocuments.aspx.cs" Inherits="Reporting_ListOfExpiringDocuments" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
   <script type="text/javascript" language="javascript">
    function onCalendarShown(sender,args)
    {  
        sender._popupDiv.style.top = '0px'; 
    }
    </script> 
</head>
<body>
<form id="form1" runat="server" defaultbutton="Button1">
<div style="text-align: center">
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
                <td align="center" class="text headerband" style="width: 100%; "> List Of Expiring Documents</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 100%;text-align: left">
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td align="center" colspan="2">
                                             <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td colspan="2" style="padding-right: 4px; border-bottom: #4371a5 1px solid;">
                                                 <table cellpadding="2" cellspacing="0" width="100%">
                                                     <tbody>
                                                         <tr>
                                                             <td style="height: 13px">
                                                                 &nbsp;</td>
                                                             <td class="style4">
                                                                 <span lang="en-us">Status :</span></td>
                                                             <td align="left" class="style2">
                                                             <asp:DropDownList CssClass="input_box" runat="server" ID="ddlStatus">
                                                             
                                                             </asp:DropDownList> 
                                                             </td>
                                                             <td style="height: 13px; width: 131px;" class="style1">
                                                                 <span lang="en-us">Doc. Type :</span></td>
                                                             <td class="style3">
                                                                 <asp:DropDownList CssClass="input_box" runat="server" ID="ddlDocType">
                                                                 <asp:ListItem Text="< All >" Value=""></asp:ListItem> 
                                                                 <asp:ListItem Text="Travel" Value="Travel"></asp:ListItem> 
                                                                 <asp:ListItem Text="Professional" Value="Professional"></asp:ListItem> 
                                                                 <asp:ListItem Text="Medical" Value="Medical"></asp:ListItem> 
                                                                 </asp:DropDownList> 
                                                                 </td>
                                                             <td style="height: 13px" class="style1">
                                                                 <span lang="en-us">Rec. Office :</span></td>
                                                             <td style="height: 13px; width: 136px;" align="left">
                                                                 <asp:DropDownList CssClass="input_box" runat="server" ID="ddlReOff"></asp:DropDownList> </td>
                                                             <td style="height: 13px; text-align: center">
                                                                 <asp:CheckBox runat="server" Text="Expired Only" ID="chkExp" Checked="true" />  </td>
                                                             <td style="height: 13px; text-align: center">
                                                                 <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" 
                                                                     Text="Show Report" TabIndex="6" /></td>
                                                         </tr>
                                                     </tbody>
                                                 </table>
                                            </td>
                                     </tr>
                                    <tr><td colspan="2" align="center">
                                        <iframe id="IFRAME1" runat="server" frameborder="1" style="overflow: auto; width: 100%;height: 433px"></iframe>
                                        </td></tr>
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
</td></tr></table>
</div>
</form>
</body>
</html>
