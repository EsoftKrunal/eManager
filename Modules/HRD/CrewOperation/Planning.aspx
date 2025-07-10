<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Planning.aspx.cs" Inherits="CrewOperation_Planning" MasterPageFile="~/Modules/HRD/CrewPlanning.master" %>
<%@ Register Src="SearchSignOff.ascx" TagName="SearchSignOff" TagPrefix="uc2" %>
<%@ Register Src="SearchReliver.ascx" TagName="SearchReliver" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
        <table cellspacing="0" border="0" width="100%" style="background-color:#f9f9f9" >
            <tr>
                <td style="text-align:center" >
                <uc2:SearchSignOff ID="SearchSignOff1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style=" text-align:center;">
                <uc1:SearchReliver ID="SearchReliver1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
    