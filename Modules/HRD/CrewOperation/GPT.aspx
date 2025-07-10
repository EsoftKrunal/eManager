<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GPT.aspx.cs" Inherits="GPT" Title="Graphical Planning Tool" %>
<%@ Register Src="../UserControls/GraphicalPlanningTool.ascx" TagName="GraphicalPlanningTool" TagPrefix="uc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">--%>
<body>
    <form id="frm" runat="server">
        <uc1:GraphicalPlanningTool ID="GraphicalPlanningTool1" runat="server" />
    </form> 
    </body>

<%--</asp:Content>--%>

