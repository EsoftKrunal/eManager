<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewAvailibilityReport.aspx.cs" Inherits="Reporting_CrewAvaoilibilityReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Crew Avalilibility Report</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >
    .fixedbar
    {
        position:fixed;
        margin:80px 0px 0px 120px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
             .style9
             {
                 height: 18px;
                 width: 55px;
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
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" > 
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; ">Crew Availibility Report</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
    <tr>
        <td style="width: 100%; padding :3px;" >
        <TABLE cellSpacing=0 cellPadding=0 width="100%" border="0">
        <TBODY>
            <TR><td style="HEIGHT: 18px; width: 80px; text-align: right;">From Date :</td>
            <td align="left" style="height: 18px;width :150px">
                 <asp:TextBox id="txt_from" runat="server" CssClass="input_box" Width="100px" 
                     MaxLength="15"></asp:TextBox> 
                 <asp:ImageButton id="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton></td><td align="right" style="height: 18px">
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                    </td>
            <td align="right" style="width:80px;">To Date :</td>
        <td align="left;width :220px" >
            <asp:TextBox id="txt_to" runat="server" CssClass="input_box" Width="100px" 
                MaxLength="15"></asp:TextBox> 
            <asp:ImageButton id="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton>
            <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txt_from" PopupPosition="TopRight" PopupButtonID="imgfrom" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"></ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender id="CalendarExtender4" runat="server" TargetControlID="txt_to" PopupPosition="TopRight" PopupButtonID="imgto" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"></ajaxToolkit:CalendarExtender>
                        
       </td>
        <td style=" width :80px; text-align : left "  >
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator> 
                         </td>
        <td align="right" class="style9">Status :</td>
        <td align="left" style="height: 18px">
         <asp:DropDownList ID="ddlCrewType" runat="server" CssClass="input_box" 
                Width="69px">
             <asp:ListItem Value="0">&lt; All &gt;</asp:ListItem>
             <asp:ListItem Value="1">New Emp</asp:ListItem>
             <asp:ListItem Value="2">On Leave</asp:ListItem>
             <asp:ListItem Value="3">On Board</asp:ListItem>
         </asp:DropDownList>
      </td>
                <td >
                    Rec. Office :</td>
                <td >
         <asp:DropDownList ID="ddlRecOff" runat="server" CssClass="input_box" Width="90px">
         </asp:DropDownList>
                </td>
<td align="left" style="height: 18px; width :100px;">
<asp:Button id="Button1" runat="server" CssClass="btn" Text="Show Report" OnClick="Button1_Click" TabIndex="4"></asp:Button> </td>
                                        </TR>
</TBODY></TABLE>
        </td>
</tr>
<tr>
    <td style="text-align: left">
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
</td> </tr> </table> 
</div>
</form>
</body>
</html>
