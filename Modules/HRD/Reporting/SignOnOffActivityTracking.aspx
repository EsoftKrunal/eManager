<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignOnOffActivityTracking.aspx.cs" Inherits="Reporting_SignOnOffActivityTracking" MasterPageFile="~/Modules/HRD/Reporting/ActivityTracking.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    <style type="text/css" >  
    .fixedbar
    {
        position:fixed;
        margin:87px 0px 0px 230px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
            .style1
        {
            height: 13px;
            width: 129px;
        }
    </style> 
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
       <tr>
        <td align="center" valign="top">
         <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
          <tr>
             <td>
                 <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
             <tr>
                 <td style="border-bottom: #4371a5 1px solid;">
                                 <table cellpadding="0" cellspacing="0" style="width: 100%">
                                     <tr>
                                         <td style="padding-right: 10px; width: 186px; height: 13px; text-align: right">
                                     <asp:RadioButtonList ID="rd_lst" runat="server" RepeatDirection="Horizontal"
                                         Width="221px" TabIndex="1">
                                         <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                         <asp:ListItem Value="1">SignOn</asp:ListItem>
                                         <asp:ListItem Value="2">SignOff</asp:ListItem>
                                     </asp:RadioButtonList></td>
                                         <td style="padding-right: 10px; height: 13px; text-align: right; width: 73px;">
                                             From:</td>
                                         <td style="padding-right: 10px; text-align: left" class="style1">
                                             <asp:TextBox ID="txt_from" runat="server" CssClass="required_box" TabIndex="2" Width="89px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                 ID="img_from" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="3" /></td>
                                         <td style="padding-right: 10px; width: 128px; height: 13px; text-align: left">
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_from"
                                                 Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                 </td>
                                         <td style="padding-right: 10px; height: 13px; text-align: right; width: 51px;">
                                             To:</td>
                                         <td style="width: 204px; height: 13px; text-align: left">
                                             <asp:TextBox ID="txt_to" runat="server" CssClass="required_box" TabIndex="4" Width="89px"></asp:TextBox>&nbsp;<asp:ImageButton
                                                 ID="img_to" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="5" />
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_to"
                                                 Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                         <td style="width: 100px; height: 13px; text-align: left;">
                                        <asp:Button ID="btn_show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_show_Click" TabIndex="6" /></td>
                                     </tr>
                                     </table>
                 </td>
             </tr>
          <tr>
           <td style="text-align: left; padding-left: 50px">
             <div style="width:100%;overflow-x:hidden;overflow-y:scroll;height:450px;" > 
                <cr:crystalreportviewer ID="CrystalReportViewer1" runat="server" ToolbarStyle-CssClass="fixedbar" AutoDataBind="true"  />
            </div> 
           </td>
          </tr>
         </table>
         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd-MMM-yyyy" OnClientShown="onCalendarShown" PopupButtonID="img_from" PopupPosition="TopRight"  TargetControlID="txt_from" > </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd-MMM-yyyy" OnClientShown="onCalendarShown" PopupButtonID="img_to" PopupPosition="TopRight" TargetControlID="txt_to" > </ajaxToolkit:CalendarExtender>
        </td>
       </tr>
      </table>
</asp:Content>
  