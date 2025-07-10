<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConciseMonthlyWagesReport.aspx.cs" Inherits="Reporting_ConciseMonthlyWagesReport" %>
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
        <script type="text/javascript" language="javascript">
    function onCalendarShown(sender,args)
    {  
        sender._popupDiv.style.top = '0px'; 
    }
    </script> 
</head>
<body>
<form id="form1" runat="server" defaultbutton="btn_show" >
<div>
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
       <tr>
        <td align="center" valign="top">
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
          <tr>
            <td align="center" class="text headerband" style="width: 100%; ">Concise Monthly Wage Report</td>
          </tr>
          <tr>
            <td><asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
          </tr>
          <tr>
              <td style="padding:3px;" >
                     <table cellpadding="0" cellspacing="0" style="width: 100%;;">
                         <tr>
                             <td align="right" style="padding-right: 10px; height: 13px; text-align: right">
                                 Emp # :</td>
                             <td align="left">
                                 <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="required_box" TabIndex="1" Width="102px"></asp:TextBox></td>
                             <td style="padding-right: 10px; height: 13px; text-align: left">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_EmpNo"
                                     ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                             <td style="padding-right: 10px; height: 13px; text-align: right">
                                 From:</td>
                             <td style="padding-right: 10px; width: 135px; height: 13px; text-align: left">
                                 <asp:TextBox ID="txt_from" runat="server" CssClass="required_box" Width="102px" TabIndex="1"></asp:TextBox>&nbsp;<asp:ImageButton
                                     ID="img_from" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="2" /></td>
                             <td style="padding-right: 10px; height: 13px; text-align: left">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_from"
                                     Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                 </td>
                             <td style="padding-right: 10px; height: 13px; text-align: right">
                                 To:</td>
                             <td style="width: 143px; height: 13px; text-align: left">
                                 <asp:TextBox ID="txt_to" runat="server" CssClass="required_box" Width="102px" TabIndex="3"></asp:TextBox>&nbsp;<asp:ImageButton
                                     ID="img_to" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="4" /></td>
                             <td style="width: 150px; height: 13px; text-align: left;">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_to" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                 
                                 </td>
                             <td style="width: 100px; height: 13px">
                            <asp:Button ID="btn_show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_show_Click" TabIndex="5" /></td>
                         </tr>
                         </table>
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
                         PopupButtonID="img_from" PopupPosition="TopRight" TargetControlID="txt_from">
                     </ajaxToolkit:CalendarExtender>
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
                         PopupButtonID="img_to" PopupPosition="TopRight" TargetControlID="txt_to">
                     </ajaxToolkit:CalendarExtender>
                    
                 </td>
             </tr>
          <tr>
           <td>
               <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe></td>
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
