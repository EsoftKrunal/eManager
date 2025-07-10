<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewCOC.aspx.cs" Inherits="FormReporting_AddNewCOC" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
   
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
       <table cellpadding="1" cellspacing="0" 
            style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td colspan="5" 
                    style=" text-align :center ; height:23px" class="text headerband">
                    Add New COC</td>
            </tr>
            <tr>
                <td colspan="4" style=" text-align :center ;">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;</td>
                <td style=" text-align :center ;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style9">
                    COC Issued From :</td>
                <td style="text-align: left" class="style7">
                    <asp:TextBox ID="txt_COCIssuedFrom" runat="server" CssClass="input_box" 
                        MaxLength="49" Width="214px"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right" class="style5">
                    &nbsp;</td>
                <td style="text-align: left" class="style11">
                    &nbsp;</td>
                <td style="text-align: left" class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style9">
                    Vessel :</td>
                <td style="text-align: left" class="style7">
                    <asp:DropDownList ID="ddlvessel" runat="server" CssClass="input_box" 
                        Width="220px">
                    </asp:DropDownList></td>
                <td style="padding-right: 5px; text-align: right" class="style5">
                    &nbsp;</td>
                <td style="text-align: left" class="style11">
                    &nbsp;</td>
                <td style="text-align: left" class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style8">
                    COC Ref # :</td>
                <td style="text-align: left" class="style6">
                    <asp:TextBox  ID="txt_COCNo" runat="server" CssClass="input_box" Width="214px"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right" class="style4">
                    &nbsp;</td>
                <td style="text-align: left" class="style10">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style8">
                    COC Issue Date :</td>
                <td style="text-align: left" class="style6">
                    <asp:TextBox ID="txt_IssueDt" runat="server" CssClass="input_box" Width="90px"></asp:TextBox>
                    &nbsp;<asp:ImageButton
                        ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right" class="style4">
                    &nbsp;</td>
                <td style="text-align: left" class="style10">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style8">
                    Place Issued :</td>
                <td style="text-align: left" class="style6">
                    <asp:TextBox ID="txt_COCIssuedPlace" runat="server" CssClass="input_box" 
                        MaxLength="49" Width="214px"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right" class="style4">
                    &nbsp;</td>
                <td style="text-align: left" class="style10">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style8">
                    Target Closure Date :</td>
                <td style="text-align: left" class="style6">
                    <asp:TextBox ID="txt_TarClDate" runat="server" CssClass="input_box" Width="90px"></asp:TextBox>&nbsp;<asp:ImageButton
                        ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right" class="style4">
                    &nbsp;</td>
                <td style="text-align: left" class="style10">
                    &nbsp;</td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style9">
                    Survey Started :</td>
                <td style="text-align: left" class="style7">
                    <asp:TextBox ID="txt_SurveyStDt" runat="server" CssClass="input_box" 
                        Width="90px"></asp:TextBox>
                    &nbsp;<asp:ImageButton
                        ID="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right" class="style5">
                    &nbsp;</td>
                <td style="text-align: left" class="style11">
                    &nbsp;</td>
                <td style="text-align: left" class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style9">
                    Survery Completed :</td>
                <td style="text-align: left" class="style7">
                    <asp:TextBox ID="txt_SurveyEndDt" runat="server" CssClass="input_box" 
                        Width="90px"></asp:TextBox>
                    &nbsp;<asp:ImageButton
                        ID="ImageButton5" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right" class="style5">
                    &nbsp;</td>
                <td style="text-align: left" class="style11">
                    &nbsp;</td>
                <td style="text-align: left" class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style9">
                    Surveyor&#39;s Name :</td>
                <td style="text-align: left" class="style7">
                    <asp:TextBox ID="txt_COCSurveyor" runat="server" CssClass="input_box" 
                        MaxLength="49" Width="213px"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right" class="style5">
                    &nbsp;</td>
                <td style="text-align: left" class="style11">
                    &nbsp;</td>
                <td style="text-align: left" class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" valign="top" class="style8">
                    What's Wrong (desc.) :</td>
                <td style="text-align: left" colspan="3">
                    <asp:TextBox ID="txt_Desc" runat="server" CssClass="input_box" MaxLength="49" 
                        Width="532px" Height="72px" TextMode="MultiLine"></asp:TextBox></td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" valign="top" class="style8">
                    Corrective Actions (desc.) :</td>
                <td style="text-align: left" colspan="3">
                    <asp:TextBox ID="txt_Desc0" runat="server" CssClass="input_box" MaxLength="49" 
                        Width="531px" Height="72px" TextMode="MultiLine"></asp:TextBox></td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" class="style8">
                    Upload COC :</td>
                <td colspan="3" style="text-align: left">
                    <asp:FileUpload ID="flp_COCUpload" runat="server" CssClass="input_box" Width="512px" /></td>
                <td style="text-align: left">
                    &nbsp;</td>
            </tr>
           <tr>
                <td class="style8">
                </td>
                <td style="text-align: left" class="style6">
                    <asp:Button ID="btn_Save" runat="server" CssClass="btn" OnClick="btn_Save_Click"
                        Text="Save" Width="59px" /><asp:Button ID="btn_Close" runat="server" CssClass="btn"
                            Text="Close" Visible="False" Width="59px" OnClientClick="window.close();" />
                    <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" OnClick="btn_Cancel_Click"
                        Text="Cancel" Width="59px" /></td>
                <td class="style4">
                </td>
                <td class="style10">
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="topright" TargetControlID="txt_IssueDt"></ajaxToolkit:CalendarExtender>

            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="topright" TargetControlID="txt_TarClDate"></ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="topright" TargetControlID="txt_SurveyStDt"></ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton5" PopupPosition="topright" TargetControlID="txt_SurveyEndDt"></ajaxToolkit:CalendarExtender>
    </div>
    </form>
</body>
</html>
