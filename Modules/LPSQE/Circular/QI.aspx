<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QI.aspx.cs" Inherits="CircularForApproval" Title="EMANAGER" %>
<%@ Register Src="~/Modules/LPSQE/Circular/IncidentReport.ascx" TagName="IncidentReport" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/LPSQE/Circular/Incident.ascx" TagName="Incident" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
.selbtn
{
    background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
	
}
.btn1
{
	   background-color :#c2c2c2;
	border:solid 1px gray;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div class="text headerband"> Circular </div>
    <table cellpadding="0" cellpadding="0" width="100%">
                <tr>
                    <td> 
                       <asp:Button ID="btnCircular" runat="server" OnClick="btnCircular_OnClick" Text="Old Circular" CssClass="btn1" Width="110" Visible="false" /> 
                        <asp:Button ID="btnCircularNew" runat="server" OnClick="btnCircularNew_OnClick" Text="Circular" CssClass="btn1" Width="110" /> 
                        <asp:Button ID="btnSafetyAlert" runat="server" OnClick="btnSafetyAlert_OnClick"  Text="Safety Alert" CssClass="btn1" Width="110" Visible="false"/>
                        <asp:Button ID="btnRegulation" runat="server" OnClick="btnRegulation_OnClick"  Text="Regulation" CssClass="btn1" Width="110" Visible="false"/>  
                    </td>
                </tr>
                <tr>
                    <td >
                        <iframe id="Ifram1" runat="server" style="width:100%;"  width="100%" height="550px" scrolling="no" frameborder="0" >
                        </iframe>
                    </td>
                </tr>
            </table>
    </asp:Content>