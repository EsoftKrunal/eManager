<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CrewDeploymentHome.aspx.cs" Inherits="Modules_HRD_CrewApproval_CrewDeploymentHome" %>

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
         <img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;
            Deployment </td>
            </tr>
        <tr>
        <td style=" text-align :left; vertical-align : top;border-left:solid 1px white;" >
        <div style="">
            <asp:Button runat="server" ID="btnDeploymentApproval" Text="Deployment Approval" CssClass="btn1" OnClick="Menu_Click"  CommandArgument="0"   />&nbsp;
            <asp:Button runat="server" ID="btnDeploymentOperation" Text="Deployment Operation" CssClass="btn1" OnClick="Menu_Click" CommandArgument="1"    />&nbsp;
           
            
        </div>
        <div style="width:100%; height:3px; background-color:#facc8c"></div>
            <div>

            <iframe runat="server" id="frm" src="~/Modules/HRD/CrewApproval/CrewApprovalScreen.aspx" width="100%" height="800px" scrolling="no" frameborder="1"></iframe>    
            </div>
        </td>
        </tr>
      </table>
      </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainFoot" Runat="Server">
</asp:Content>

