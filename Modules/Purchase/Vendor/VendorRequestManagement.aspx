<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorRequestManagement.aspx.cs" Inherits="Vendor" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <script type="text/javascript" src="../JS/Common.js"></script>    
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >
    .btnNormal
    {
        font-family: Calibri;
	    cursor: pointer;
	    font-size: 13px;
	    background-color:#eaeaf1;
	    height: 25px;	        
	    border:none;	    
	    color:#666;
    }
    
    .btnSelected
    {
        font-family: Calibri;
	    cursor: pointer;
	    font-size: 13px;	    
	    background-color:#39558F;
	    font-weight: bold;
	    height: 25px;	    
	    border:none;	    
	    color:#fff;
	    
    }
       *
    {
        box-sizing:border-box;
        -moz-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
    }
  
</style>
</asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
      <div class="text headerband">
          Vendors
      </div>
<div style="text-align : center; vertical-align : top; border-bottom:1px solid #000000;">
<table style="text-align:center" cellpadding="0" cellspacing="0" border="0" width="100%">
<tr >
    <td id="tdHome" runat="server" style="border-right: 0px solid #000000; width: 150px;" >
        <asp:LinkButton ID="btnHome"  Width="100%" Text="Home Page" runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="3" Font-Bold="True"  ForeColor="#206020"  />
    </td>
    <%--<td id="tdVendorRequestManagement" runat="server" style="border-right: 1px solid #000000; width: 150px;" >
        <asp:Button ID="btnRequestManagement" CssClass="btnNormal" Width="100%" Text="New Vendor & Evaluation" runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="1"  />
    </td>--%>
    <td id="tdVendorRequest" runat="server" style="border-right: 0px solid #000000;width: 150px;">
        <asp:LinkButton ID="btnVendorRequest"  Text="Approved Vendors" Width="100%" runat="server" CausesValidation="false" OnClick="menu_Click" CommandArgument="2"  Font-Bold="True"  ForeColor="#206020" />
    </td>
    <td style="text-align:right; padding:3px;"> 
        <%--<asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" ImageUrl="~/Images/home.png" style="float :right; padding-right :5px; background-color : Transparent " />--%>  
    </td>
</tr>
</table>
   <div style=" width:100%; height:5px; background-color:#39558F"></div>
</div>  
<div>
    <iframe id="iframe1" src="VendorMgmtHome.aspx" runat="server" width="100%" frameborder="0" scrolling="no" height="490px"></iframe> 
</div>   
</asp:Content>

