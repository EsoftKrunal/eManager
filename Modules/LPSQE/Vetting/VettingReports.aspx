<%@ Page Language="C#" MasterPageFile="~/Modules/LPSQE/Vetting/VettingMasterPage.master" AutoEventWireup="true" CodeFile="VettingReports.aspx.cs" Inherits="Vetting_VettingReports" Title="Untitled Page"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
           <asp:Button ID="btn_OR" runat="server" Text="Observation Report" CssClass="Btn1" style="background-color:Orange" onclick="btn_OR_Click" />
           <asp:Button ID="btn_VSR" runat="server" Text="Vetting Status Report" CssClass="Btn1" onclick="btn_VSR_Click"/>
           <asp:Button ID="btn_VP" runat="server" Text="Vetting Performance" CssClass="Btn1" onclick="btn_VP_Click"/>
       </td>
       <td style="text-align: right"><asp:ImageButton runat="server" ID="btnHome" ImageUrl="~/Images/home.png" PostBackUrl="~/Vetting/VIQHome.aspx" CausesValidation="false" /> </td>
    </tr>
     <tr>
       <td style="height:5px; background-color:orange; border:solid 5px orange" colspan="2">
           
       </td>
    </tr>
    </table>
    <iframe runat="server" id="frmFile" width="100%" frameborder="no" scrolling="no" height="465" src="ObservationReporting_Report.aspx"></iframe>
</asp:Content>

