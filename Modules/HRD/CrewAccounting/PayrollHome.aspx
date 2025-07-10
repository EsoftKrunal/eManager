<%@ Page Title="EMANAGER" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PayrollHome.aspx.cs" Inherits="Modules_HRD_CrewAccounting_PayrollHome"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <div style="text-align: center">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
            <tr>
                 <td  class="text headerband" >
         <img runat="server" id="imgHome" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/home.png" alt="Home" onclick="window.location.href='../Dashboard.aspx'" /> &nbsp;
            Payroll</td>
            </tr>
        <tr>
        <td style=" text-align :left; vertical-align : top;border-left:solid 1px white;" >
        <div style="">
            <asp:Button runat="server" ID="btnWageScale" Text="Wage Scale " CssClass="btn1" OnClick="btnWageScale_Click"   />&nbsp;
           <%-- <asp:Button runat="server" ID="btnPayroll" Text="Payroll Old" CssClass="btn1" OnClick="btnPayroll_Click"  Visible="false" />&nbsp;--%>
            <asp:Button runat="server" ID="btnPayrollNew" Text="Payroll" CssClass="btn1" OnClick="btnPayrollNew_Click"  />&nbsp;
             <asp:Button runat="server" ID="btnReports" Text="Reports" CssClass="btn1" OnClick="btnReports_Click"  Visible="false"   />&nbsp;
        </div>
        <div style="width:100%; height:3px; background-color:#facc8c"></div>
            <div>

            <iframe runat="server" id="frm" width="100%" height="800px" scrolling="no" frameborder="1"></iframe>    
            </div>
        </td>
        </tr>
      </table>
      </div>
</asp:Content>

