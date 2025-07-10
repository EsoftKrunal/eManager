<%@ Page Title="EMANAGER" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AccountReports.aspx.cs" Inherits="Modules_OPEX_BudgetReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <title>EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <%--<link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
   <%-- <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />--%>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <div style="text-align: center;font-family:Arial;">
       
        <table style="width :100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td  class="text headerband" >
         
            Published Reports </td>
            </tr>
        <tr>
        <td style=" text-align :left; vertical-align : top;border-left:solid 1px white;" >
        <div style="">
            <asp:Button runat="server" ID="btnPublishReport" Text="Month Closing" CssClass="btn1" OnClick="Menu_Click" CommandArgument="0"    />&nbsp;
           
            <asp:Button runat="server" ID="btnPublished" Text="Published Budget" CssClass="btn1" OnClick="Menu_Click" CommandArgument="1"  Visible="false"   />&nbsp;

            <asp:Button runat="server" ID="btnFundsUpdate" Text="Funds Update" CssClass="btn1" OnClick="Menu_Click" CommandArgument="2"     />&nbsp;
            
        </div>
        <div style="width:100%; height:3px; background-color:#facc8c"></div>
            <div>

            <iframe runat="server" id="frm" src="~/Modules/OPEX/Report/PublishReport.aspx" width="100%" height="1000px" scrolling="yes" frameborder="1"></iframe>    
            </div>
        </td>
        </tr>
      </table>
      </div>
</asp:Content>


