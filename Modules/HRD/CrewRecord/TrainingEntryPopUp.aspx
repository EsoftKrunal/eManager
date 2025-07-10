<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrainingEntryPopUp.aspx.cs" Inherits="CrewRecord_TrainingEntryPopUp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Training Entry</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
   <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        &nbsp;</center>
        <center>
             
    <fieldset style="border-right: #8fafdb 1px solid;border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; height: 246px; width: 640px; font-family:Arial;font-size:12px;">
        <table style="width: 640px;" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" class="text headerband">
                   <%-- <img runat="server" id="imgHelp" moduleid="4" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> --%>
                    Training Details
                </td>
            </tr>

            </table>
                                                        <%--<legend><strong>Training Details</strong></legend>--%>
        <table border="0" cellpadding="0" cellspacing="0" style="padding-right: 5px; padding-bottom: 5px;
                                                            padding-top: 5px; text-align: center; width: 100%;">
                                                            <tr>
                                                                <td style="height: 182px;">
                                                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                                        <tr>
                                                                            <td colspan="4" style="height: 19px; text-align: center">
                                                                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 515px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 216px; text-align: left">
                                                                            </td>
                                                                            <td style="width: 274px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 193px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 515px; height: 19px; text-align: right">
                                                                                Training Type:</td>
                                                                            <td style="width: 216px; height: 19px; text-align: left">
                                                                                <asp:DropDownList ID="ddl_TrainingType" runat="server" CssClass="required_box" Width="150px" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddl_TrainingType_SelectedIndexChanged">
                                                                                </asp:DropDownList></td>
                                                                            <td style="width: 274px; height: 19px; text-align: right">
                                                                                Training Name:</td>
                                                                            <td style="width: 193px; height: 19px; text-align: left">
                                                                            <asp:DropDownList ID="ddl_TrainingReq_Training" runat="server" CssClass="required_box" Width="150px" TabIndex="2"></asp:DropDownList></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td style="width: 515px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 216px; height: 16px; text-align: left">
                                                                                <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddl_TrainingType"
                                                                                    ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                            <td style="width: 274px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 193px; height: 16px; text-align: left">
                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_TrainingReq_Training"
                                                                                    ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 515px; height: 16px; text-align: right">
                                                                                Due Date:</td>
                                                                            <td style="width: 216px; height: 16px; text-align: left">
                                                                                <asp:TextBox ID="txt_DueDate" runat="server" CssClass="input_box" MaxLength="20" TabIndex="3"
                                                                                    Width="90px"></asp:TextBox>
                                                                                <asp:ImageButton ID="ImageButton5" runat="server" CausesValidation="false"
                                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                                            <td style="width: 274px; height: 16px; text-align: right">
                                                                                <%--Training Status:--%></td>
                                                                            <td style="width: 193px; height: 16px; text-align: left">
                                                                                <asp:Label ID="lbl_TrainingStatus" runat="server" TabIndex="4"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 515px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 216px; height: 16px; text-align: left">
                                                                                <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="txt_DueDate"
                                                                                    ErrorMessage="Invalid Date." Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></td>
                                                                            <td style="width: 274px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 193px; height: 16px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="Server" id="tr1" visible="False" >
                                                                            <td style="width: 515px; height: 16px; text-align: right">
                                                                                Training Verified:</td>
                                                                            <td style="width: 216px; height: 16px; text-align: left">
                                                                                <asp:CheckBox ID="chk_TrainingVerified" runat="server" TabIndex="5" /></td>
                                                                            <td style="width: 274px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 193px; height: 16px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="Server" id="tr2" visible="False" >
                                                                            <td style="width: 515px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 216px; height: 16px; text-align: left">
                                                                            </td>
                                                                            <td style="width: 274px; height: 16px; text-align: right">
                                                                            </td>
                                                                            <td style="width: 193px; height: 16px; text-align: left">
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="Server" id="tr3" visible="False" >
                                                                            <td style="text-align: right; width: 515px; height: 53px;" valign="top">
                                                                                Remark:</td>
                                                                            <td colspan="3" style="height: 53px; text-align: left">
                                                                                <asp:TextBox ID="txt_Remark" runat="server" CssClass="input_box"
                                                                                    MaxLength="30" Width="351px" Height="47px" TextMode="MultiLine" TabIndex="6"></asp:TextBox>&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 515px; text-align: right" valign="top">
                                                                            </td>
                                                                            <td colspan="3" style="text-align: left">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" style="text-align: center; height: 18px;" valign="top">
                                                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" AutoComplete="true"
                                                                                    ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_DueDate">
                                                                                </ajaxToolkit:MaskedEditExtender>
                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="MM/dd/yyyy"
                                                                                    PopupButtonID="ImageButton5" PopupPosition="TopLeft" TargetControlID="txt_DueDate">
                                                                                </ajaxToolkit:CalendarExtender>
                                                                                <asp:HiddenField ID="HiddenPK" runat="server" />
                                                                                <asp:Button ID="btn_Save" runat="server" CausesValidation="true" CssClass="btn"
                                                                                    OnClick="btn_Search_Click" TabIndex="7" Text="Save" Width="75px" />&nbsp;
                                                                                    <input type="button" class="btn" value="Close" style="width: 75px" onclick="window.close();"/></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                            
                                                    </fieldset>
        </center>
    </div>
    </form>
</body>
</html>
