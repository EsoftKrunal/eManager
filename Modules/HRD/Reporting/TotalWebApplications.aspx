<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TotalWebApplications.aspx.cs" Inherits="Reporting_TotalWebApplications" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>Untitled Page</title>
<link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">
function onCalendarShown(sender,args)
{  
sender._popupDiv.style.top = '0px'; 
}
</script> 
<style type="text/css" >
.fixedbar
{
position:fixed;
margin:80px 0px 0px 140px;   
background-color:#f0f0f0;  
z-index:100;
border:solid 1px #5c5c5c;
}
</style>
</head>
<body>
<form id="form1" runat="server" defaultbutton="btn_show" >
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" style="height: 157px" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
                        <tr>
                            <td align="center" class="text headerband" style="width: 100%;">Total Web Applications</td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                                    <tr>
                                        <td style="text-align: center;" valign="top">
                                           <table cellpadding="0" cellspacing="0">
                   <tr>
                       <td style="" colspan="7">
                           <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                       </td>
                   </tr>
                   <tr style=" padding :3px;" >
                       <td style="width: 252px; text-align: right;">
                           From Date :</td>
                       <td style="width: 284px; text-align: left">
                           <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" TabIndex="1"></asp:TextBox>
                           <asp:ImageButton ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                               TabIndex="2" /></td>
                       <td style="width: 246px; text-align: left;">
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                           </td>
                       <td style="width: 246px; text-align: right;">
                           To Date :</td>
                       <td style="width: 396px; text-align: left">
                           <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" TabIndex="3"></asp:TextBox>
                           <asp:ImageButton ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                               TabIndex="4" /></td>
                       <td style="width: 240px; text-align: left;">
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttodate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                           </td>
                       <td style="width: 240px">
                           <asp:Button ID="btn_show" runat="server" CssClass="btn" OnClick="btn_show_Click"
                               TabIndex="5" Text="Show Report" /></td>
                   </tr>
                   </table>
                  <%-- <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" colspan="2" style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;
                                                        padding-top: 10px; border-bottom: #4371a5 1px solid">
                                                        &nbsp;<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"   />
                                                    </td>
                                                </tr>
                                            </table>--%>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                    <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
                                      </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
                                                PopupButtonID="imgfrom" PopupPosition="TopLeft" TargetControlID="txtfromdate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
                                                PopupButtonID="imgto" TargetControlID="txttodate" PopupPosition="TopLeft">
                                            </ajaxToolkit:CalendarExtender>
                                            
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
