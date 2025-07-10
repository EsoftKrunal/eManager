<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="CrewPlanning_Menu.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="CrewOperation_CrewPlanning_Menu" Title="Untitled Page" %>
<%-- Add content controls here --%>
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
	 <link rel="stylesheet" href="../../../css/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
	<div style="text-align: center">
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
         <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table style="background-color: #f9f9f9; border-collapse:collapse; width :100%" border="1" bordercolor="#4371a5" >
            <tr>
                <td style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband">
                  <img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;
                     Planning 
                </td>
            </tr>
            <tr>
                <td style=" background-color: #f9f9f9; vertical-align:top; overflow:visible;" >
                    <div style=" text-align:left;float:left">
                        <table>
                            <tr>
                                <td><asp:Button runat="server"  CommandArgument="0" Text="Relief Forecast" OnClick="Menu_Click" ID="b1" CssClass="btn1"  Font-Bold="True" Visible="false" /></td>
                                <td><asp:Button runat="server"  CommandArgument="3" Text="Relief Planning" OnClick="Menu_Click"  ID="b4"  CssClass="btn1"  Font-Bold="True"/></td>
                                <td><asp:Button runat="server"  CommandArgument="4" Text="Vessel Takeover" OnClick="Menu_Click" ID="b5"  CssClass="btn1"  Font-Bold="True" /></td>
                                <td><asp:Button runat="server"  CommandArgument="5" Text="Crew Availability" OnClick="Menu_Click" ID="b6"  CssClass="btn1"  Font-Bold="True" Visible="false" /></td>
                                <td><asp:Button runat="server"  CommandArgument="1" Text="Crew Change" OnClick="Menu_Click" ID="b2" Visible="false" CssClass="btn1"  Font-Bold="True" /></td>
                                <td><asp:Button runat="server"  CommandArgument="2" Text="Crew Travel" OnClick="Menu_Click" ID="b3" Visible="false" CssClass="btn1"  Font-Bold="True" /></td>
                                 
                            </tr>
                        </table>
                         
                    </div>
                   
                  
                </td>
            </tr>
        </table>
        </td>
        </tr> 
        </table>  
    </div>
<div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="~/Modules/HRD/CrewOperation/CrewPlanning.aspx" id="frm" frameborder="0" width="100%" height="600px" scrolling="no"></iframe>
</div>

</asp:Content>