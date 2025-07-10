<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTM.aspx.cs" Inherits="CTM" MasterPageFile="~/Modules/HRD/CrewAccounts.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;</div>--%>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="textregisters" colspan="2" style="padding-right: 0px; padding-left: 0px;
                    padding-bottom: 0px; padding-top: 0px;text-align: center;">
                    <strong>CTM</strong></td>
            </tr>
            <tr>
                <td colspan="2" style="padding-right: 0px; padding-left: 0px;
                    padding-bottom: 0px; padding-top: 0px; text-align: center">
                    &nbsp;<asp:Label ID="lbl_ctm_Message" runat="server" ForeColor="#C00000" Visible="False">Record Successfully Saved.</asp:Label></td>
            </tr>
            <tr>
                <td style="padding-left:10px; padding-right:10px">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
                        text-align: center">
                          <legend><strong> CTM </strong>
                        </legend>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="6" style="height: 7px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 10px">
                                    Vessel:</td>
                                <td align="left">
                                    <asp:DropDownList ID="dp_vessel" runat="server" CssClass="required_box" TabIndex="3"
                                        Width="145px" AutoPostBack="True" OnSelectedIndexChanged="dp_vessel_SelectedIndexChanged">
                                </asp:DropDownList></td>
                                <td align="right" style="padding-right: 10px">
                                    Master:</td>
                                <td align="left">
                                    <asp:DropDownList ID="dpname" runat="server" CssClass="required_box" TabIndex="3"
                                        Width="145px">
                                        <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                </asp:DropDownList></td>
                                <td align="right" style="text-align: left">
                    </td>
                                <td align="left">
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="left">
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dp_vessel"
                                        ErrorMessage="Required." MaximumValue="1000000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dpname"
                                        ErrorMessage="Required." MaximumValue="1000000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td align="right" style="text-align: left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 10px">
                                    Request Amt:
                                </td>
                                <td align="left" >
                                    <asp:TextBox ID="txtrequestamount" runat="server" CssClass="required_box" Width="140px" MaxLength="6"></asp:TextBox></td>
                                <td align="right" style="padding-right: 10px">
                                    Requested Date:</td>
                                <td align="left">
                                    <asp:TextBox ID="txtreqDt" runat="server" CssClass="required_box" Width="123px" MaxLength="10"></asp:TextBox>
                                    <asp:ImageButton ID="imgDt" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False" />
                                    </td>
                                <td align="right" style="text-align: left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="height: 13px">
                                    &nbsp;</td>
                                <td align="left" style="height: 13px">
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtrequestamount"
                                        Display="Dynamic" ErrorMessage="Greater than 0." Operator="GreaterThan" Type="Double"
                                        ValueToCompare="0"></asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                            runat="server" ControlToValidate="txtrequestamount" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtrequestamount"
                                                Display="Dynamic" ErrorMessage="Till 2 decimal places only." ValidationExpression="\b\d{1,13}\.?\d{0,2}"
                                                Width="147px"></asp:RegularExpressionValidator></td>
                                <td align="right" style="height: 13px">
                                </td>
                                <td align="left" style="height: 13px">
                                    <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtreqDt"
                                        ErrorMessage="Invalid Date." Operator="DataTypeCheck" Type="Date" Display="Dynamic"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtreqDt"
                                        Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                <td align="right" style="text-align: left; height: 13px;">
                                </td>
                                <td align="left" style="height: 13px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 10px">
                                    Amt Paid (off):</td>
                                <td align="left" style="width: 202px; height: 13px">
                                    <asp:TextBox ID="txt_Amtpaid" runat="server" CssClass="required_box" Width="140px" MaxLength="6"></asp:TextBox></td>
                                <td align="right" style="padding-right: 10px">
                                    Amt. Rcd.(VSL):</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_amtrcd" runat="server" CssClass="required_box" Width="140px" MaxLength="6"></asp:TextBox></td>
                                <td align="right" style="height: 13px; text-align: left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="left">
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_Amtpaid"
                                        Display="Dynamic" ErrorMessage="Greater than 0." Operator="GreaterThan" Type="Double"
                                        ValueToCompare="0"></asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server" ControlToValidate="txt_Amtpaid" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_Amtpaid"
                                                Display="Dynamic" ErrorMessage="Till 2 decimal places only." ValidationExpression="\b\d{1,13}\.?\d{0,2}"
                                                Width="147px"></asp:RegularExpressionValidator></td>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txt_amtrcd"
                                        Display="Dynamic" ErrorMessage="Greater than 0." Operator="GreaterThan" Type="Double"
                                        ValueToCompare="0"></asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                            runat="server" ControlToValidate="txt_amtrcd" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_amtrcd"
                                                Display="Dynamic" ErrorMessage="Till 2 decimal places only." ValidationExpression="\b\d{1,13}\.?\d{0,2}"
                                                Width="147px"></asp:RegularExpressionValidator></td>
                                <td align="right" style="text-align: left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 10px">
                                    Handling Charges:</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_handlingcharge" runat="server" CssClass="input_box" Width="140px" MaxLength="6"></asp:TextBox></td>
                                <td align="right" style="padding-right: 10px">
                                    Previous Bal.:</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_previosbalance" runat="server" CssClass="input_box" Width="140px" ReadOnly="True"></asp:TextBox></td>
                                <td align="right" style=" text-align: left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    &nbsp;</td>
                                <td align="left">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_handlingcharge"
                                        Display="Dynamic" ErrorMessage="Till 2 decimal places only." ValidationExpression="\b\d{1,13}\.?\d{0,2}"
                                        Width="147px"></asp:RegularExpressionValidator></td>
                                <td align="right">
                                </td>
                                <td align="left">
                                </td>
                                <td align="right">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 10px">
                                    Total Balance (VSL):</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_total" runat="server" CssClass="input_box" ReadOnly="True" Width="140px"></asp:TextBox></td>
                                <td align="right">
                                </td>
                                <td align="left">
                                </td>
                                <td align="right" style="text-align: left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" >
                                    &nbsp;</td>
                                <td align="left">
                                    &nbsp;</td>
                                <td align="right">
                                </td>
                                <td align="left">
                                </td>
                                <td align="right" style="text-align: left">
                                </td>
                                <td align="left" >
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 10px">
                                    Remarks:</td>
                                <td align="left" colspan="2" >
                                    <asp:TextBox ID="txt_remarks" runat="server" CssClass="input_box" Height="43px" TextMode="MultiLine"
                                        Width="271px"></asp:TextBox></td>
                                <td align="left" >
                                </td>
                                <td align="right" style=" text-align: left">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; padding-top: 10px; padding-bottom: 10px;">
                    &nbsp;<asp:Button ID="btnSaveCTM" runat="server" CssClass="btn" OnClick="btnSaveCTM_Click"
                        TabIndex="7" Text="Save" Width="65px" /></td>
            </tr>
        </table>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
        PopupButtonID="imgDt" PopupPosition="TopRight" TargetControlID="txtreqDt">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true"
        ClearMaskOnLostFocus="true" ClearTextOnInvalid="true" Mask="99/99/9999" MaskType="Date"
        TargetControlID="txtreqDt">
    </ajaxToolkit:MaskedEditExtender>
    
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
        FilterType="Numbers,Custom" TargetControlID="txtrequestamount" ValidChars=".">
    </ajaxToolkit:FilteredTextBoxExtender>
    
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
        FilterType="Numbers,Custom" TargetControlID="txt_Amtpaid" ValidChars=".">
    </ajaxToolkit:FilteredTextBoxExtender>
   
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
        FilterType="Numbers,Custom" TargetControlID="txt_handlingcharge" ValidChars=".">
    </ajaxToolkit:FilteredTextBoxExtender>
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
        FilterType="Numbers,Custom" TargetControlID="txt_amtrcd" ValidChars=".">
    </ajaxToolkit:FilteredTextBoxExtender>
            </asp:Content>
   <%-- </form>
</body>
</html>--%>
