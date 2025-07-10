<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewTraining.master" AutoEventWireup="true" CodeFile="KSTHOME.aspx.cs" Inherits="CrewOperation_KSTHOME" %>
<%@ Register src="~/Modules/HRD/CrewOperation/KSTMenu.ascx" tagname="kstMenu" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <div>
        <uc2:kstMenu ID="kstMenu1" runat="server" />
    </div>
</asp:Content>