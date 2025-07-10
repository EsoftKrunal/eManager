<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficersJoinedFirstTimeWithCompany.aspx.cs" Inherits="Reporting_OfficersJoinedFirstTimeWithCompany" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" /> <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
     <style type="text/css" >
    .fixedbar
    {
        position:fixed;
        margin:70px 0px 0px 140px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
    </style>
    <script type="text/javascript" language="javascript">
    function onCalendarShown(sender,args)
    {  
        sender._popupDiv.style.top = '0px'; 
    }
    </script>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
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
        <td align="center" valign="top" style="">
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
          <tr>
           <td align="center" class="text headerband" style="width: 100%; ">
               New Joiners to Company
           </td>
          </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
             </tr>
             <tr>
                 <td style="border-bottom: #4371a5 1px solid; padding :3px;" valign="top">
                     <table cellpadding="0" cellspacing="0" style="width: 100%">
                         <tr>
                             <td colspan="1">
                                 From Date :</td>
                             <td colspan="1">
                                 <asp:TextBox ID="txt_from" runat="server" CssClass="required_box" MaxLength="2000"
                                     TabIndex="1" Width="100px"></asp:TextBox>
                                 <asp:ImageButton ID="imgfrom" runat="server"
                                         ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="2" /></td>
                             <td>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                         runat="server" ControlToValidate="txt_from" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                 </td>
                             <td>
                                 To Date :</td>
                             <td colspan="1">
                                 <asp:TextBox ID="txt_to" runat="server" CssClass="required_box" MaxLength="2000"
                                     TabIndex="3" Width="100px"></asp:TextBox>
                                 <asp:ImageButton ID="imgto" runat="server"
                                         ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="4" /></td>
                             <td>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                         runat="server" ControlToValidate="txt_to" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                 </td>
                             <td>Current Crew Status : </td>
                             <td colspan="1">
                                 <asp:DropDownList ID="rd_lst" runat="server" RepeatDirection="Horizontal" Width="100px" TabIndex="1">
                                     <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                     <asp:ListItem Value="2">On Leave</asp:ListItem>
                                     <asp:ListItem Value="3">On Board</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td>Crew Type : </td>
                              <td colspan="1">
                                 <asp:DropDownList ID="ddlor" runat="server" RepeatDirection="Horizontal" Width="150px" TabIndex="1">
                                     <asp:ListItem Selected="True" Value=""> All </asp:ListItem>
                                     <asp:ListItem Value="O">Officer</asp:ListItem>
                                     <asp:ListItem Value="R">Rating</asp:ListItem>
                                 </asp:DropDownList>
                             </td>
                             <td colspan="1">
                              <asp:Button ID="btn_show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_show_Click" TabIndex="5" Width="96px" /></td>
                         </tr>
                         </table>
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
                         PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_from">
                     </ajaxToolkit:CalendarExtender>
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
                         PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txt_to">
                     </ajaxToolkit:CalendarExtender>
                     
                 </td>
             </tr>
          <tr>
           <td style="text-align: left;">
           <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:432px; overflow:auto"></iframe>
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
