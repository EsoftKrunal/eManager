<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Arrier.aspx.cs" Inherits="CrewAccounting_Arrier" MasterPageFile="~/Modules/HRD/CrewAccounts.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td colspan="2" style="padding-right: 0px; padding-left: 0px;
                    padding-bottom: 0px; padding-top: 0px; text-align: center">
                <asp:Label ID="lbl_ctm_Message" runat="server" ForeColor="#C00000" >Record Successfully Saved.</asp:Label></td>
            </tr>
            <tr>
                <td style="padding-left:10px; padding-right:10px">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
                        text-align: center">
                          <legend><strong> ARREAR </strong>
                        </legend>&nbsp;&nbsp;
                        <table width="100%">
                            <tr>
                                <td style="width: 174px; text-align: right">
                                    Vessel :</td>
                                <td style="width: 490px; text-align: left">
                                    <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" Width="310px">
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:Button ID="btnSaveCTM" runat="server" CssClass="btn" OnClick="btnSaveCTM_Click"
                        TabIndex="7" Text="Calculate" Width="65px" /></td>
                                <td style="text-align: left">
                                    <asp:Button ID="cmdprint" runat="server" CssClass="btn" OnClick="cmdprint_Click"
                        TabIndex="7" Text="Print" Width="65px" /></td>
                            </tr>
                            <tr>
                                <td style="width: 174px; text-align: right">
                                </td>
                                <td style="width: 490px; text-align: left">
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Required." ControlToValidate="ddl_Vessel" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
                                <td>
                                </td>
                                <td style="text-align: left">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; padding-top: 10px; padding-bottom: 10px;">
                    &nbsp;</td>
            </tr>
        </table>
            </asp:Content>
  