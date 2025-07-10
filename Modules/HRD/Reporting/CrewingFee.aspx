<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewingFee.aspx.cs" Inherits="Reporting_CrewingFee" Title="Crewing Fee" %>
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
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; "> Crewing Fee Report</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 10px; width: 100%; color: red; text-align: center"><asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100%;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="padding:3px;border-bottom: #4371a5 1px solid;">
                                             <table cellpadding="0" cellspacing="0" width="100%" style=" height :40px" >
                                                 <tr>
                                                     <td style="height: 13px; width: 70px; text-align : right">
                                                         Owner :</td>
                                                     <td style="height: 13px; text-align: left; width: 204px;">
                                                         <asp:DropDownList ID="ddl_Owner" runat="server" CssClass="input_box" Width="190px">
                                                         </asp:DropDownList></td>
                                                     <td style="width: 80px;text-align: right">
                                                         From Dt. :</td>
                                                     <td style="width: 300px;text-align: left">
                                                         <asp:TextBox ID="txtfromdate" runat="server" Width="75px" CssClass="required_box"></asp:TextBox>
                                                         <asp:ImageButton ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="79" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtfromdate" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                         
                                                     </td>
                                                     <td style="width: 120px;text-align: right">
                                                         To Dt. :</td>
                                                     <td style="width: 300px;text-align: left">
                                                         <asp:TextBox ID="txttodate" runat="server" CssClass="required_box" Width="75px"></asp:TextBox>
                                                         <asp:ImageButton ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="79" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttodate" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttodate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> 
                                                         
                                                     </td>
                                                     <td style="padding-right: 5px;" align="right">
                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                 PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txtfromdate">
                                             </ajaxToolkit:CalendarExtender>
                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                 PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txttodate">
                                             </ajaxToolkit:CalendarExtender>
                                                         <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Show Report" />
                                                         <asp:Button ID="Button2" runat="server" CssClass="btn" OnClick="Button2_Click" Text="Excel Report" />
                                                         </td>
                                                         
                                                 </tr>
                                                 <tr>
                                                     <td style="height: 13px; width: 70px;">
                                                         &nbsp;</td>
                                                     <td style="height: 13px; text-align: left; width: 204px;">
                                                         &nbsp;</td>
                                                     <td style="width: 80px;text-align: right">
                                                         Mgmt Fee :</td>
                                                     <td style="width: 300px;text-align: left">
                                                         <asp:TextBox ID="txtMgtFee" runat="server" CssClass="required_box" 
                                                             MaxLength="7" Width="50px"></asp:TextBox>
                                                     </td>
                                                     <td style="width: 120px;text-align: right">
                                                         Mustering Fee :</td>
                                                     <td style="width: 300px;text-align: left">
                                                         <asp:TextBox ID="txt_Mustfee" runat="server" CssClass="required_box" 
                                                             MaxLength="7" Width="50px"></asp:TextBox>
                                                     </td>
                                                     <td style="padding-right: 5px;" align="right">
                                                         &nbsp;</td>
                                                 </tr>
                                             </table>
                                         </td>
                                     </tr>
                                    <tr><td colspan="2">
                                        <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
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

