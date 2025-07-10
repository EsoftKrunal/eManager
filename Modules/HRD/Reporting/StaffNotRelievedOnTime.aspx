<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StaffNotRelievedOnTime.aspx.cs" Inherits="Reporting_StaffNotRelievedOnTime" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
       
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btn_show" >
    <div style="text-align: center">
      <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
      <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
       <tr>
        <td align="center" valign="top">
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
          <tr>
           <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5">
               Crew Not Relieved On ime
           </td>
          </tr>
          <tr>
           <td style="text-align: left">
               <table cellpadding="0" cellspacing="0" width="100%">
                   <tr>
                       <td colspan="6" style="text-align: center">
                           <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>&nbsp;</td>
                   </tr>
                   <tr id="trdate" runat="server">
                       <td style="width: 114px;text-align: right">
                           From Date :</td>
                       <td style="width: 196px;">
                           <asp:TextBox ID="txtfromdate" runat="server" CssClass="required_box"></asp:TextBox>
                           <asp:ImageButton ID="imgfrom" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                               TabIndex="79" /></td>
                       <td style="width: 64px;text-align: right">
                           To Date :</td>
                       <td style="width: 309px;">
                           <asp:TextBox ID="txttodate" runat="server" CssClass="required_box"></asp:TextBox>
                           <asp:ImageButton ID="imgto" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"
                               TabIndex="79" /></td>
                       <td colspan="2" style="">
                           <asp:Button ID="btn_show" runat="server" CssClass="btn" OnClick="btn_show_Click"
                               TabIndex="1" Text="Show Report" /></td>
                   </tr>
                   <tr>
                       <td style="width: 114px; height: 4px;">
                       </td>
                       <td style="width: 196px; height: 4px;">
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromdate"
                               ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                       <td style="width: 64px; height: 4px">
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttodate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                           </td>
                       <td style="width: 309px; height: 4px">
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfromdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttodate"
                               ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                       <td style="height: 4px">
                       </td>
                   </tr>
                   <tr>
                       <td colspan="6">
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                               PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txtfromdate">
                           </ajaxToolkit:CalendarExtender>
                           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                               PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txttodate">
                           </ajaxToolkit:CalendarExtender>
                           
                       </td>
                   </tr>
               </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                   <tr>
                       <td>
                          <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:450px; overflow:auto"></iframe>
                          <%--<cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" ></cr:crystalreportviewer>--%>
                       </td>
                   </tr>
               </table>
           </td>
          </tr>
         </table>
         </td>
        </tr>
       </table>
    </div>
    </form>
</body>
</html>
