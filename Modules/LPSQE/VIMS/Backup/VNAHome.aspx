<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VNAHome.aspx.cs" Inherits="VNAHome"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/LPSQE/VIMS/VIMSMenu.ascx" tagname="VIMSMenu" tagprefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <link rel="stylesheet" type="text/css" href="../css/Style.css" />
  <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
  
</head>

<body>
<form id="form" runat="server">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<mtm:VIMSMenu runat="server" ID="VIMSMenu1" />
<div style="padding:5px;background-color:#CCEEF9; border:solid 1px #00ABE1; font-size:13px; ">
    Select Year : <asp:DropDownList runat="server" ID="ddlYear" CssClass="input_box" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged"></asp:DropDownList>
    <asp:Button runat="server" CssClass="input_box" ID="btnImport" Text="Import" Width="60px" OnClick="btnImport_Click"/>
</div>
<br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
<center>
<table width="80%" border="1" cellpadding="20" cellspacing="0">
<tr>
<td width="25%">
   <div>&nbsp;<asp:LinkButton runat="Server" ID="lblQ1" Text="" Font-Bold="true" Font-Size="16px" OnClick="lblQ_Click"></asp:LinkButton></div>
</td>
<td width="25%">
    <div>&nbsp;<asp:LinkButton runat="Server" ID="lblQ2" Text="" Font-Bold="true" Font-Size="16px" OnClick="lblQ_Click"></asp:LinkButton></div>
</td>
<td width="25%">
    <div>&nbsp;<asp:LinkButton runat="Server" ID="lblQ3" Text="" Font-Bold="true" Font-Size="16px" OnClick="lblQ_Click"></asp:LinkButton></div>
</td>
<td width="25%">
    <div>&nbsp;<asp:LinkButton runat="Server" ID="lblQ4" Text="" Font-Bold="true" Font-Size="16px" OnClick="lblQ_Click"></asp:LinkButton></div>
</td>
</tr>
</table>

<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvImport" runat="server" visible="false">
                        <center>
                            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                            <div style="position:relative;width:400px; height:100px;text-align :center;background : white; z-index:150;top:100px; border:solid 10px black;">
                                <center >
                                <div style="padding:6px; background-color:#7094FF; font-size:14px; color:White;"><b>
                                Import VNA                                
                                </b></div>
                                
                                <div style="margin:5px">
                                Packet to import :&nbsp;<asp:FileUpload runat="server" ID="flp_Upload" ></asp:FileUpload>
                                </div>
                                <div style="padding-left:5px;padding-right:5px; text-align:center;">
                                    <asp:Label runat="server" ForeColor="Red" ID="lblMsgPOP" style="float:left" Font-Bold="true"></asp:Label>
                                    <asp:Button runat="server" ID="btnSaveImport" Text="Import" onclick="btnSaveImport_Click" class="btn" width="100px" OnClientClick="this.value='Processing..';" />   
                                </div>
                                <div style="text-align:right; position:relative; right:-20px; top:-0px;">
                                    <asp:ImageButton runat="server" ID="ImageButton1" Text="Close" onclick="btnClose_Click" ImageUrl="~/Images/close-button.png" CausesValidation="false" title='Close this Window !' OnClientClick="this.value='Processing..';"/>   
                                </div>
                                </center>
                             </div>
                             </center>
                        </div>
</center>
</form>
</body>
</html>
