<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CorrectiveActions_PopUp.aspx.cs" Inherits="Transactions_CorrectiveActions_PopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
     <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" >
    var old='';
    function setResponse(chkSame)
    {
        if (chkSame.checked) 
        {
            old=document.getElementById('txt_CorrActions').value; 
            document.getElementById('txt_CorrActions').value=document.getElementById('dvActions').innerHTML;
        } 
        else 
        {
            document.getElementById('txt_CorrActions').value=old;
        }
    }
    </script>  
    </head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <br />
        <center>
        <div runat="server" style="display :none" id="dvActions"></div> 
        <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td colspan="4" style="height:23px; text-align :center " class="text headerband">Follow Up Details</td>
            </tr>
            <tr>
                <td></td>
                <td>&nbsp;</td>
                <td></td>
                <td></td>
            </tr>
             <tr>
                <td style="text-align: right;padding-right: 5px; ">Cause:</td>
                <td colspan="3" style="text-align :left "  >
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; width: 597px;">
                <asp:CheckBoxList ID="rdbflaws" runat="server" RepeatDirection="Horizontal" Width="493px">
                <asp:ListItem>People</asp:ListItem>
                <asp:ListItem>Process</asp:ListItem>
                <asp:ListItem>Equipment</asp:ListItem>
                </asp:CheckBoxList>
                </fieldset>
                </td>
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
                    <input type="checkbox" onclick="setResponse(this)" id="chkSame"/><label for="chkSame">Same as Response/Mgmt. Comment</label> 
                    </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right;" valign="top">
                    Corrective Actions :</td>
                <td style="text-align: left" colspan="3">
                    <asp:TextBox ID="txt_CorrActions" runat="server"  Height="203px" TextMode="MultiLine" Width="597px"></asp:TextBox></td>
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
                    <asp:TextBox ID="txt_TargetCDate" runat="server" CssClass="input_box" Width="122px"></asp:TextBox>&nbsp;<asp:ImageButton
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
                <td>
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: left" colspan="2">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label></td>
                <td style="text-align: right; padding-right: 20px;">
                    <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_Click" />&nbsp;<asp:Button
                        ID="btn_Cancel" runat="server" CssClass="btn" OnClick="btn_Cancel_Click" Text="Clear" />
                    <asp:Button ID="btn_Notify" runat="server" CssClass="btn" Text="Notify" Width="59px" OnClick="btn_Notify_Click" Enabled="False" />&nbsp;</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_TargetCDate">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
        </center>
    </div>
    </form>
</body>
</html>
