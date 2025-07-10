<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddFollowUpList.aspx.cs" Inherits="FormReporting_AddFollowUpList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td colspan="4" style=" height:23px" class="text headerband ">
                    Add New FollowUp Item</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px; text-align: left">
                    &nbsp;</td>
                <td style="width: 145px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Vessel :</td>
                <td style="width: 294px; text-align: left">
                    <asp:DropDownList ID="ddlvessel" runat="server" CssClass="input_box" Width="226px">
                    </asp:DropDownList></td>
                <td style="padding-right: 5px; width: 145px; text-align: right">
                    FollowUp Category :</td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddl_FollowUpCat" runat="server" CssClass="input_box" Width="96px">
                        <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                        <asp:ListItem Value="1">Audit</asp:ListItem>
                        <asp:ListItem Value="3">Technical</asp:ListItem>
                        <asp:ListItem Value="4">Vetting</asp:ListItem>
                        <asp:ListItem Value="5">Others</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px; text-align: left">
                    &nbsp;</td>
                <td style="width: 145px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Target Date :</td>
                <td style="width: 294px; text-align: left">
                    <asp:TextBox ID="txt_TarClDate" runat="server" CssClass="input_box" Width="90px"></asp:TextBox>&nbsp;<asp:ImageButton
                        ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; width: 145px; text-align: right">
                    Responsibility :</td>
                <td style="text-align: left">
                    <asp:CheckBoxList ID="chklst_Respons" runat="server" RepeatDirection="Horizontal"
                        Width="162px">
                        <asp:ListItem>Vessel</asp:ListItem>
                        <asp:ListItem>Office</asp:ListItem>
                    </asp:CheckBoxList></td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px; text-align: left">
                    &nbsp;</td>
                <td style="width: 145px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Critical :</td>
                <td style="width: 294px; text-align: left">
                    <asp:CheckBox ID="chk_Critical" runat="server" /></td>
                <td style="padding-right: 5px; width: 145px; text-align: right">
                    </td>
                <td style="text-align: left">
                    </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px; text-align: left">
                    &nbsp;</td>
                <td style="width: 145px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" valign="top">
                    Deficiency :</td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txt_DeficiencyText" runat="server"  Height="85px" TextMode="MultiLine"
                        Width="597px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px; text-align: left">
                    &nbsp;</td>
                <td style="width: 145px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right;" valign="top">
                    Corrective Actions :</td>
                <td style="text-align: left" colspan="3">
                    <asp:TextBox ID="txt_CorrActions" runat="server"  Height="85px" TextMode="MultiLine" Width="597px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px; text-align: left">
                    &nbsp;</td>
                <td style="width: 145px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px; text-align: left">
                    <asp:Button ID="btn_Save" runat="server" CssClass="btn" OnClick="btn_Save_Click"
                        Text="Save" Width="59px" /> &nbsp;&nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" OnClick="btn_Cancel_Click"
                        Text="Cancel" Width="59px" /></td>
                <td style="width: 145px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px; text-align: left">
                </td>
                <td style="width: 145px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px; text-align: left">
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_TarClDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td style="width: 145px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 294px">
                </td>
                <td style="width: 145px">
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
