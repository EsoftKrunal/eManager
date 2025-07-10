<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeficiencyDetailPopUp.aspx.cs" Inherits="FormReporting_DeficiencyDetailPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
   
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td colspan="4" style=" height:23px" class="text headerband">
                    Deficiencies for FollowUp</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px;padding-top: 5px; text-align: right" valign="top">
                    Cause:</td>
                <td colspan="3" style="text-align: left">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; width: 597px;">
                <asp:CheckBoxList ID="rdbflaws" runat="server" RepeatDirection="Horizontal" Width="493px">
                <asp:ListItem>People</asp:ListItem>
                <asp:ListItem>Process</asp:ListItem>
                <asp:ListItem>Equipment</asp:ListItem>
                </asp:CheckBoxList>
                </fieldset></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" valign="top">
                    Deficiency :</td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txt_DeficiencyText" runat="server"  Height="85px" TextMode="MultiLine"
                        Width="597px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right;" valign="top">
                    Corrective Actions :</td>
                <td style="text-align: left" colspan="3">
                    <asp:TextBox ID="txt_CorrActions" runat="server"  Height="188px" TextMode="MultiLine" Width="597px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right;">
                    Target Closure Date :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_TargetCDate" runat="server" CssClass="input_box" Width="122px" MaxLength="11"></asp:TextBox>&nbsp;<asp:ImageButton
                        ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right;">
                    Responsibility :</td>
                <td style="text-align: left">
                    <asp:CheckBoxList ID="chklst_Respons" runat="server" RepeatDirection="Horizontal"
                        Width="162px">
                        <asp:ListItem>Vessel</asp:ListItem>
                        <asp:ListItem>Office</asp:ListItem>
                    </asp:CheckBoxList></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                    </td>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label></td>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="padding-right: 20px; text-align: right">
                    <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_Click" /> &nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" Text="Clear" OnClick="btn_Cancel_Click" />
                    &nbsp;
                    <asp:Button ID="btn_ClWindow" runat="server" CssClass="btn" Text="Close" OnClientClick="window.close();" />
                    </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                    </td>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Created By :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_CreatedBy" runat="server" BackColor="Gainsboro" CssClass="input_box"
                        ReadOnly="True"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right">
                    Created On :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_CreatedOn" runat="server" BackColor="Gainsboro" CssClass="input_box"
                        ReadOnly="True" Width="96px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                    Modified By :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_FModifiedBy" runat="server" BackColor="Gainsboro" CssClass="input_box"
                        ReadOnly="True"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right">
                    Modified On :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_FModifiedOn" runat="server" BackColor="Gainsboro" CssClass="input_box"
                        ReadOnly="True" Width="96px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                </td>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_TargetCDate">
                    </ajaxToolkit:CalendarExtender>
                    &nbsp; &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
