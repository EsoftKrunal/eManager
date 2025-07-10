<%@ Page Language="C#" AutoEventWireup="true" CodeFile="COCClosurePopUp.aspx.cs" Inherits="FormReporting_COCClosurePopUp" %>
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
        <br />
        <table cellpadding="1" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td class="text headerband" colspan="4" style="height: 23px;  text-align :center">
                    COC Closure</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: center;" colspan="4">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right;">
                    Closed Date :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_ClosedDate" runat="server" CssClass="input_box" Width="122px" MaxLength="11"></asp:TextBox>&nbsp;<asp:ImageButton
                        ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                <td style="padding-right: 5px; text-align: right">
                    Flaws :</td>
                <td style="text-align: left">
                    <asp:CheckBoxList ID="rdbflaws" runat="server" RepeatDirection="Horizontal" Width="311px">
                        <asp:ListItem>People</asp:ListItem>
                        <asp:ListItem>Process</asp:ListItem>
                        <asp:ListItem>Technology</asp:ListItem>
                    </asp:CheckBoxList></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" valign="top">
                    Remarks :</td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txt_ClosedRemarks" runat="server" CssClass="input_box" Height="140px"
                        TextMode="MultiLine" Width="612px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right" valign="top" class="style3">
                    Closure Evidence :
                </td>
                <td style="text-align: left" class="style2" colspan="3" >
                    <asp:FileUpload runat="server" ID="flp_COCUpload" Width="300px" CssClass="input_box" />
                    <a runat="server" target="_blank"  id="a_file"><img style=" border:none"  src="../../HRD/Images/paperclipx12.png" alt="Attachment"/> </a> 
                    </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: left">
                    <asp:Button ID="btn_Save" runat="server" CssClass="btn" OnClick="btn_Save_Click"
                        Text="Save" Width="59px" />
                    <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" OnClick="btn_Cancel_Click"
                        Text="Cancel" Width="59px" />
                </td>
                <td style="text-align: left">
                </td>
                <td style="text-align: left">
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">Closed By :
                    
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_ClosedBy" runat="server" BackColor="Gainsboro" CssClass="input_box"
                        ReadOnly="True"></asp:TextBox></td>
                <td style="padding-right: 5px; text-align: right">
                    Closed On :</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txt_ClosedOn" runat="server" BackColor="Gainsboro" CssClass="input_box"
                        ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; text-align: right">
                </td>
                <td style="text-align: left" colspan="3">
                    &nbsp;<ajaxToolkit:CalendarExtender ID="CalendarExtender3"
                            runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight"
                            TargetControlID="txt_ClosedDate">
                        </ajaxToolkit:CalendarExtender>
                    <asp:HiddenField ID="HiddenField_VslId" runat="server" />
                    <asp:HiddenField ID="HiddenField_Def" runat="server" />
                    <asp:HiddenField ID="HiddenField_TrClDt" runat="server" />
                    <asp:HiddenField ID="HiddenField_Resp" runat="server" />
                    <asp:HiddenField ID="HiddenField_ModBy" runat="server" />
                    <asp:HiddenField ID="HiddenField_IssuedBy" runat="server" />
                    <asp:HiddenField ID="HiddenField_EmpNo" runat="server" />
                    <asp:HiddenField ID="HiddenField_IssueDate" runat="server" />
                    <asp:HiddenField ID="HiddenField_ImAcDt" runat="server" />
                    <asp:HiddenField ID="HiddenField_PrAcDt" runat="server" />
                    <asp:HiddenField ID="HiddenField_FUDt" runat="server" />
                    <asp:HiddenField ID="HiddenField_Desc" runat="server" />
                    <asp:HiddenField ID="HiddenField_Desc1" runat="server" />
                    <asp:HiddenField ID="HiddenField_FileUpl" runat="server" />
                    <asp:HiddenField ID="HiddenField_IBFirstName" runat="server" />
                    <asp:HiddenField ID="HiddenField_IBLastName" runat="server" />
                    <asp:HiddenField ID="HiddenField_IBRank" runat="server" />
                </td>
            </tr>
        </table>
     </div>
    </form>
</body>
</html>
