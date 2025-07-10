<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewTraining.master" AutoEventWireup="true" CodeFile="Dummy_Training.aspx.cs" Inherits="Dummy_Training" Title="Exception Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
 <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td >
            <center>
                <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text="You Are Not Authorized To View This Page" Font-Size="12px"></asp:Label>
                </center>
               </td>
                
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

