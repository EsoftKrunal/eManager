<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeaIcon.aspx.cs" Inherits="SeaIcon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
<%--<style >
    .mainheading
    {
    	font-family:Verdana;
    	font-size:17px;
    	height:25px;
    	color:White;
    	padding-left:5px;
    	padding-top:2px;
    	background-color:#357EC7;
    	vertical-align:middle; 
    }
    .subheading
    {
    	font-family:Verdana;
    	font-size:13px;
    	height:20px;
    	color:White;
    	padding-left:5px;
    	padding-top:4px;
    	background-color:#38ACEC;
    }
     .subheading2
    {
    	font-family:Verdana;
    	font-size:11px;
    	height:18px;
    	color:White;
    	padding-left:5px;
    	padding-top:4px;
    	background-color:#38ACEC;
    }
    body
    {
    	font-family:Verdana;
    	background-color:White;
    }
    td
    {
    	font-size:11px;
    	border:solid 1px #0371a5;
    	padding :2px;
    }
    table
    {
    	border-collapse:collapse;
    }
</style>--%>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <div class="text headerband"><strong><asp:Label ID="lblVesselName" runat="server"></asp:Label></strong></div>
    <div class="text headerband"><strong><asp:Label ID="lblHeader" runat="server"></asp:Label></strong></div>
    <asp:Image runat="server" ID="imgData" /> 
    <div style=" font-family: Verdana; font-size :smaller">
    <div class="subheading"><strong>Sea Condition :</strong></div>
    <table border="0" width="100%" style=" border-collapse :collapse " >
    <tr>
        <td>&nbsp;</td>
        <td>Course : </td>
        <td><asp:Label ID="lblCourse" runat="server"></asp:Label></td>
        <td style="font-size:12px; font-weight:bold;">Remarks</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>Wind Direction :</td>
        <td><asp:Label ID="lblWDir" runat="server"></asp:Label></td>
        
        <td rowspan="6" colspan="1" style ="vertical-align:top; padding:2px;">     
            <div id="divRem" runat="server" style ="vertical-align:top;" >
            </div>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>Wind Force :</td>
        <td><asp:Label ID="lblWForce" runat="server"></asp:Label></td>
        <%--<td>
            
        </td>--%>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>Sea Direction :</td>
        <td><asp:Label ID="lblSeaDir" runat="server"></asp:Label></td>
        <%--<td></td>--%>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>Sea State :</td>
        <td>    <asp:Label ID="lblSeaState" runat="server"></asp:Label></td>
        <%--<td></td>--%>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>Current Direction :</td>
        <td><asp:Label ID="lblCurrDir" runat="server"></asp:Label></td>
        <%--<td></td>--%>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>Current Strength :</td>
        <td><asp:Label ID="lblCurrStr" runat="server"></asp:Label></td>
        <%--<td></td>--%>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
