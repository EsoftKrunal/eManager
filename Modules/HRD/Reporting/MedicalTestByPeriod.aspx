<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicalTestByPeriod.aspx.cs" Inherits="Reporting_MedicalTestByPeriod" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >  
    .fixedbar
    {
        position:fixed;
        margin:104px 0px 0px 135px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
    </style> 
</head>
<script type="text/javascript" language="javascript">
        
function onCalendarShown(sender,args)
{  
    sender._popupDiv.style.top = '0px'; 
}

 </script>
<body>
<form id="form1" runat="server" defaultbutton="Button1" >
<div style="text-align: center">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfrom" TargetControlID="txt_from" PopupPosition="Right" OnClientShown="onCalendarShown">
</ajaxToolkit:CalendarExtender>
<ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgto" PopupPosition="Left" TargetControlID="txt_to" OnClientShown="onCalendarShown">
</ajaxToolkit:CalendarExtender>

<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >

<table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; "> Medical Test to be done by period</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="padding: 3px;border-bottom: #4371a5 1px solid;">
                                             <table cellpadding="0" cellspacing="0" width="100%">
                                                 <tr>
                                                     <td style="padding-right: 10px;" align="right">Vessel :</td>
                                                     <td style="height: 13px" align="left"><asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="265px" TabIndex="1"></asp:DropDownList></td>
                                                     <td style="padding-right: 10px;" align="right">Medical Document:</td>
                                                     <td align="left" colspan="3" style="height: 13px"><asp:DropDownList ID="ddl_MedicalDocuments" runat="server" CssClass="required_box" Width="265px" TabIndex="2"> </asp:DropDownList>
                                                         <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_MedicalDocuments" ErrorMessage="Required." MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                 </tr>
                                                 <tr>
                                                     <td align="right" style="padding-right: 10px; ">From Date :</td>
                                                     <td align="left" style="">
                                                     <asp:TextBox ID="txt_from" runat="server" CssClass="required_box" MaxLength="15" Width="100px" TabIndex="3"></asp:TextBox><asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_from" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                                                            </td>
                                                     <td align="right" style="padding-right: 10px; ">To Date :</td>
                                                     <td align="left" style=""><asp:TextBox ID="txt_to" runat="server" CssClass="required_box" MaxLength="15" Width="100px" TabIndex="4"></asp:TextBox><asp:ImageButton ID="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                     <td align="left" style=" text-align :left">
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_to" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                                                            </td>
                                                     <td align="right" style="padding-right: 5px; "><asp:Button ID="Button1" runat="server" CssClass="btn" Text="Show Report" Width="94px" OnClick="Button1_Click" TabIndex="5"/></td>
                                                 </tr>
                                                 </table>
                                             
                                         </td>
                                     </tr>
                                    <tr><td colspan="2" >
                                      <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:410px; overflow:auto"></iframe>
                                    </td></tr>
                                </table>
                             
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</td>
</tr></table>
</td>
</tr>
</table>
</div>
</form>
</body>
</html>
