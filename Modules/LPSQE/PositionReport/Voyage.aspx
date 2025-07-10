<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Voyage.aspx.cs" Inherits="Voyage" Title="Voyage" MasterPageFile="~/MasterPage.master"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <title>EMANAGER</title>
    <script type="text/javascript" src="JS/Common.js"></script>  
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
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
    
}
    </style>
     <link rel="stylesheet" href="../../CSS/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../../CSS/StyleSheet.css" />
	 <link rel="stylesheet" type="text/css" href="../../CSS/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <table style="width :100%; border-collapse:collapse;" cellpadding="0" cellspacing="0">
         <tr>
                <td style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband">
                     Voyage
                </td>
            </tr>
        <tr>
            <td style="padding-top:2px;padding-bottom:2px;">
               <%-- <div style="float:right; white-space:250px;" >
                    <a target="_blank" href="SMS_Download.aspx?File=Manual.pdf">Download Procedures</a>&nbsp;&nbsp;&nbsp;&nbsp;
                </div>--%>
                <asp:Button ID="btnVoyage" runat="server" Text=" Bunkers " OnClick="btnVoyage_OnClick"  CssClass="btn1"  />
                <asp:Button ID="btnPositionReport" runat="server" Text=" Position Report " OnClick="btnPositionReport_OnClick" CssClass="btn1"/>
                 
            </td>
         
        </tr>
        <tr>          
            <td style="padding-top:2px; border:solid 1px #4371a5;">
                <iframe id="ifram1" runat="server" frameborder="0" scrolling="no"  src="BunkerReport.aspx" width="100%" height="460px" ></iframe>
            </td>
        </tr>
    </table>  
   </asp:Content>
