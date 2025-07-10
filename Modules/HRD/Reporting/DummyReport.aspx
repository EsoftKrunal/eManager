<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewReports.master" AutoEventWireup="true" CodeFile="DummyReport.aspx.cs" Inherits="Reporting_DummyReport" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text="You Are Not Authorized To View This Report"></asp:Label></td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 13px">
            </td>
        </tr>
    </table>
</asp:Content>

