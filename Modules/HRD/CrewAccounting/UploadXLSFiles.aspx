<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadXLSFiles.aspx.cs" Inherits="CrewAccounting_UploadXLSFiles" MasterPageFile="~/Modules/HRD/CrewAccounts.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td align="center" colspan="4" style="text-align: right; padding-right:15px;padding-top:15px">
                &nbsp;
                </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="height: 13px">
                <table width="100%" style="text-align: center">
                    <tr>
                        <td style="width: 395px; text-align: right">
                            <strong>File Format :</strong></td>
                        <td style="width: 3px">
                        </td>
                        <td style="text-align: left">
                            <strong>VSL Code-Month-Year.xls</strong></td>
                    </tr>
                    <tr>
                        <td style="width: 395px">
                        </td>
                        <td style="width: 3px">
                        </td>
                        <td style="text-align: left">
                            <strong>e.g (BGI-07-2008.xls)</strong></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" style="padding-left: 180px; height: 13px" colspan="4">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="padding-left: 180px; height: 13px">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: right">
            Upload your XLS File :&nbsp;
                        </td>
                        <td style="width: 100px">
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="required_box" Width="523px" TabIndex="1" /></td>
                    </tr>
                    <tr>
                        <td style="width: 125px; height: 13px;">
                            &nbsp;
                        </td>
                        <td style="width: 100px; height: 13px;">
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text=""></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
                    ErrorMessage="Required" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                        </td>
                        <td style="width: 100px; text-align: left">
                <asp:Button ID="btn_Upload_save" runat="server" CssClass="btn" OnClick="btn_Authority_save_Click"
                    TabIndex="5" Text="Save" Visible="true" Width="59px" /></td>
                    </tr>
                    <tr>
                        <td style="width: 125px">
                        </td>
                        <td style="width: 100px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        <td align="right" style="text-align: center;">
            </td>
            <td align="left" colspan="3">
                </td>
        </tr>
        <tr>
            <td align="right" style="width: 55px">
            </td>
            <td align="left" colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" colspan="4" style="padding-right: 10px;">
                </td>
        </tr>
        <tr>
            <td align="right" colspan="4" style="padding-right: 10px">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
