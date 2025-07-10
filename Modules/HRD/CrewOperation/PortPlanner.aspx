<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master" AutoEventWireup="true" CodeFile="PortPlanner.aspx.cs" Inherits="CrewOperation_PortPlanner" Title="Untitled Page" %>
<%@ Register Src="~/Modules/HRD/CrewOperation/CreatePortCall.ascx" TagName="CreatePortCall" TagPrefix="uc1" %>
<%@ Register Src="~/Modules/HRD/CrewOperation/CrewChange.ascx" TagName="CrewChange" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
<table width="100%"> 
<tr><td  style=" text-align:left">
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" Font-Bold="True"
        OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal"
        Width="292px">
        <asp:ListItem>Create Port Call</asp:ListItem>
        <asp:ListItem>Crew Change</asp:ListItem>
    </asp:RadioButtonList></td></tr>
<tr><td>
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="Tab1" runat="server">
                                                <uc1:CreatePortCall ID="CreatePortCall1" runat="server" />
                                        </asp:View>
                                        <asp:View ID="Tab2" runat="server">
                                            <uc2:CrewChange ID="CrewChange1" runat="server"  />
                                        </asp:View>
        </asp:MultiView>
        </td></tr></table>
</asp:Content>

