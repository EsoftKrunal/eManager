<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewAcOpForm.aspx.cs" Inherits="NewAcOpForm" Title="Donwload Form" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>On Signer Report</title>
<link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
<style type="text/css" >
.fixedbar
{
    position:fixed;
    margin:84px 0px 0px 118px;   
    background-color:#f0f0f0;  
    z-index:100;
    border:solid 1px #5c5c5c;
}
    .style1
    {
        height: 13px;
        width: 204px;
    }
    .style2
    {
        width: 305px;
    }
</style>
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
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; "> 
                    New Account Opening Form</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 10px; width: 100%; color: red; text-align: center"><asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100%;text-align: center">
                            <center >
                                             <table cellpadding="0" cellspacing="0" border="0"  
                                                 style="height: 68px; width: 407px;">
                                                 <tr>
                                                     <td style="text-align: right; " class="style1">Year :</td>
                                                     <td style="text-align: left" class="style2">
                                                     <asp:DropDownList runat="server" ID="ddl_Year" CssClass="required_box" ></asp:DropDownList>  
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td style="text-align: right; " class="style1">
                                                         Quarter :</td>
                                                     <td style="text-align: left" class="style2">
                                                         <asp:DropDownList CssClass="required_box" runat="server" ID="ddl_Quarter">
                                                         <asp:ListItem Text="Q1" Value="1"></asp:ListItem>   
                                                         <asp:ListItem Text="Q2" Value="2"></asp:ListItem>   
                                                         <asp:ListItem Text="Q3" Value="3"></asp:ListItem>   
                                                         <asp:ListItem Text="Q4" Value="4"></asp:ListItem>   
                                                         </asp:DropDownList> 
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td style="height: 13px; text-align: center; " colspan="2">
                                                         <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" 
                                                             Text="Download Form" Height="20px"  />
                                                     &nbsp;</td>
                                                 </tr>
                                             </table>
                                             </center>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</td>
</tr>
</table>
</td> </tr></table> 
</div>
</form>
</body>
</html>

