<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspSearch_Report.aspx.cs" Inherits="Reports_InspSearch_Report" Title="Search Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
       <tr>
        <td align="center" valign="top" style="height: 235px">
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
          <tr>
           <td align="center" class="text headerband" style="width: 100%; height: 23px; ">
               Search Report</td>
          </tr>
          <tr>
           <td>
               &nbsp;
           </td>
          </tr>
             <tr>
                 <td style="text-align: center">
                     <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
             </tr>
             <%--<tr>
                 <td style="text-align: center">
                 </td>
             </tr>
             <tr>
                 <td style="text-align: center">
                     <table cellpadding="0" cellspacing="0" style="width: 100%">
                         <tr>
                             <td style="text-align: right">
                                 Status :</td>
                             <td style="text-align: left">
                                 <asp:DropDownList ID="ddl_Status" runat="server" CssClass="input_box"
                                     OnSelectedIndexChanged="ddl_Status_SelectedIndexChanged" Width="105px">
                                     <asp:ListItem Value="All">All</asp:ListItem>
                                     <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                     <asp:ListItem Value="Due">Due</asp:ListItem>
                                     <asp:ListItem Value="Fail">Fail</asp:ListItem>
                                     <asp:ListItem>FollowUp</asp:ListItem>
                                     <asp:ListItem Value="Insp. Request">Insp. Request</asp:ListItem>
                                     <asp:ListItem>Observation</asp:ListItem>
                                     <asp:ListItem Value="Over Due">Over Due</asp:ListItem>
                                     <asp:ListItem Value="Pass">Pass</asp:ListItem>
                                     <asp:ListItem>Planned</asp:ListItem>
                                     <asp:ListItem>Response</asp:ListItem>
                                 </asp:DropDownList></td>
                             <td style="text-align: right">
                                 From Date :</td>
                             <td style="text-align: left">
                                 <asp:TextBox ID="txt_FromDt" runat="server" CssClass="input_box" Width="96px"></asp:TextBox>&nbsp;<asp:ImageButton
                                     ID="ImageButton_FromDt" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif" /></td>
                             <td style="text-align: right">
                                 To Date :</td>
                             <td style="text-align: left">
                                 <asp:TextBox ID="txt_ToDt" runat="server" CssClass="input_box" Width="96px"></asp:TextBox>&nbsp;<asp:ImageButton
                                     ID="ImageButton_ToDt" runat="server" CausesValidation="False" ImageUrl="~/Images/Calendar.gif" /></td>
                             <td>
                                 <asp:Button ID="btn_ShowReport" runat="server" CssClass="btn" OnClick="btn_ShowReport_Click"
                                     Text="Show Report" /></td>
                         </tr>
                     </table>
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender_FromDt" runat="server" Format="dd-MMM-yyyy"
                         PopupButtonID="ImageButton_FromDt" PopupPosition="TopRight" TargetControlID="txt_FromDt">
                     </ajaxToolkit:CalendarExtender>
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender_ToDt" runat="server" Format="dd-MMM-yyyy"
                         PopupButtonID="ImageButton_ToDt" PopupPosition="TopRight" TargetControlID="txt_ToDt">
                     </ajaxToolkit:CalendarExtender>
                 </td>
             </tr>--%>
             <tr>
                 <td style="text-align: center">
                 </td>
             </tr>
          <tr>
           <td style="padding-left: 20px; text-align: left">
            <iframe runat="server" id="IFRAME1" frameborder="0" style="width: 100%; height: 610px;"></iframe>
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
