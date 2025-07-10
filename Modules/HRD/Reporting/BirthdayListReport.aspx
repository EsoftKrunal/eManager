<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BirthdayListReport.aspx.cs" Inherits="BirthdayListReport" Title="Crewing Fee" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
    margin:84px 0px 0px 118px;   
    background-color:#f0f0f0;  
    z-index:100;
    border:solid 1px #5c5c5c;
}
</style>
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
                <td align="center" class="text headerband" style="width: 100%;">Crew Birthday List</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 10px; width: 100%; color: red; text-align: center"><asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label>
                                             </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="padding:3px;border-bottom: #4371a5 1px solid;">
                                             <table cellpadding="2" cellspacing="0" width="100%">
                                                 <tr>
                                                     <td style="width: 100px; text-align : right">
                                                         <span lang="en-us">Crew Status :&nbsp;</span></td>
                                                     <td style="text-align: left; width: 110px;">
                                                         <asp:DropDownList ID="ddl_Status" runat="server" CssClass="input_box" Width="100px"></asp:DropDownList></td>
                                                     <td style="width: 90px; text-align : right">
                                                         <span lang="en-us">Recr Off. :&nbsp;</span></td>
                                                     <td style="text-align: left; width: 110px;">
                                                         <asp:DropDownList ID="ddlRecOff" runat="server" CssClass="input_box" Width="100px"></asp:DropDownList></td>
                                                     <td style="width: 70px; text-align : right">
                                                         <span lang="en-us">Off/Rat :&nbsp;</span></td>
                                                     <td style="text-align: left; width: 110px;">
                                                         <asp:DropDownList ID="ddlOffRat" runat="server" CssClass="input_box" Width="100px">
                                                         <asp:ListItem Text="< All >" Value="" ></asp:ListItem>
                                                         <asp:ListItem Text="Oficer" Value="O" ></asp:ListItem>
                                                         <asp:ListItem Text="Ratings" Value="R"></asp:ListItem>
                                                         </asp:DropDownList>
                                                         </td>
                                                     <td style="width: 120px;text-align: right">
                                                         <span lang="en-us">Birthday Month :&nbsp;</span></td>
                                                     <td style="text-align: left">
                                                     <asp:DropDownList runat="server" ID="ddlMonth" CssClass="required_box">
                                                        <asp:ListItem Value="">&lt; Select &gt;</asp:ListItem>
                                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                                        <asp:ListItem Value="7">Jul</asp:ListItem>
                                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                                     </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="R1" runat="server" ControlToValidate="ddlMonth" ErrorMessage="*" ></asp:RequiredFieldValidator>
                                                         </td>
                                                   
                                                     <td style="padding-right: 5px;" align="right">
                                                         <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Show Report" />
                                                    </td>
                                                 </tr>
                                                 </table>
                                         </td>
                                     </tr>
                                    <tr><td colspan="2">
                                        <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:475px; overflow:auto"></iframe>
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
</td> </tr></table> 
</div>
</form>
</body>
</html>

