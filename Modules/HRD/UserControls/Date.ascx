<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Date.ascx.cs" Inherits="UserControls_Date" %>
<asp:TextBox ID="txtCalendar" runat="server" CssClass="input_box" Width="98px"></asp:TextBox><asp:Image ID="imgCalendar" runat="server" ImageUrl="../images/Calendar.gif" style="cursor:hand;"/>
<ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgCalendar" TargetControlID="txtCalendar">
</ajaxToolKit:CalendarExtender>
<ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1"  runat="server" TargetControlID="txtCalendar" ClearMaskOnLostFocus="false" Mask="99/99/9999" MaskType="Date" AutoComplete="true">
</ajaxToolKit:MaskedEditExtender>
