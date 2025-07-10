<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewListReport.aspx.cs" Inherits="Reporting_CrewListReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
   <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td align="center" valign="top" >
               <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
                    <tr>
                        <td align="center"  class="text headerband">Crew List Reports</td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                 
                                <tr>
                                    <td style="padding-right: 10px; width: 100%; color: red; text-align: center">
                                        <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;text-align: left; height: 143px;" valign="top">
                                       <div id="divPrint">
                                           <table style="width: 100%">
                                               <tr>
                                                   <td style="width: 48px">
                                                       vessel :</td>
                                                   <td style="width: 100px">
                                                       <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="201px">
                                                       </asp:DropDownList></td>
                                                   <td style="width: 71px">
                                                     From Date:</td>
                                                   <td style="width: 100px">
                                                       <asp:TextBox ID="txtfromdate" runat="server" CssClass="input_box" Width="81px"></asp:TextBox><asp:ImageButton
                                                           ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                           TabIndex="79" /></td>
                                                   <td style="width: 100px">
                                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                       </td>
                                                   <td style="width: 71px">
                                                     To Date:</td>
                                                   <td style="width: 100px">
                                                       <asp:TextBox ID="txttodate" runat="server" CssClass="input_box" Width="81px"></asp:TextBox><asp:ImageButton
                                                           ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                                                           TabIndex="79" /></td>
                                                   <td style="width: 100px">
                                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttodate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                       </td>
                                                   <td style="width: 100px">
                                                       <asp:Button ID="btnsearch" runat="server" CssClass="btn" Text="Show Report" OnClick="btnsearch_Click" Width="90px" /></td>
                                               </tr>
                                               <tr>
                                                   <td colspan="9">
                                                   <table cellpadding="0" cellspacing="0" width="100%">
                                                       <tr>
                                                           <td>
                                                               <iframe id="IFRAME1" runat="server" frameborder="1" style="overflow: auto; width: 100%;height: 427px; background-color :White "></iframe>
                                                           </td>
                                                       </tr>
                                                   </table>
                                                   </td>
                                               </tr>
                                           </table>
                                       </div>
                                    </td>
                                </tr>
                            </table>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txtfromdate">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txttodate">
                            </ajaxToolkit:CalendarExtender>
                                                        
                        </td>
                    </tr>
                </table>
         </td>
        </tr>
        </table> 
</td> </tr></table> 
</form>
</body>
</html>
