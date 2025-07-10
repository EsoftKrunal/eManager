<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vsl_JobDescription.aspx.cs" Inherits="Vsl_JobDescription" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .style1
        {
            font-weight: bold;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
                            
                            <div style="width:100%;  border:0px solid #000;  overflow:auto; overflow-y:hidden" ><%--height:460px;--%>
                            
                    <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                         <tr>
                            <td style="text-align:left; padding-left: 5px; padding-right: 5px; height:3px">
                              
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; padding-left: 5px; padding-right: 5px;">
                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%; border:#8fafdb 1px solid; border-collapse:collapse;">
                                        <tr class= "headerstylegrid">
                                           <td  >Job Description </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                 <table border="0" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                        <tr>
                                                            <td style="text-align:right;width:50px; font-weight:bold">Vessel :&nbsp;</td>
                                                            <td style="text-align:left;width:100px;"><asp:Label ID="lblDescVessel" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                            <td style="text-align:right;width:50px; font-weight:bold">Component :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblDescComponent" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                            <td style="text-align:right;width:50px; font-weight:bold">Interval :&nbsp;</td>
                                                            <td style="text-align:left;width:50px;"><asp:Label ID="lblDescInterval" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;width:50px; font-weight:bold">Job :&nbsp;</td>
                                                            <td style="text-align:left;" colspan="4"><asp:Label ID="lblDescJob" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                            <td style="text-align:right;width:50px; font-weight:bold">&nbsp;</td>
                                                            <td style="text-align:left;width:50px;">&nbsp;</td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;width:50px; font-weight:bold">&nbsp;</td>
                                                            <td style="text-align:left;" colspan="4"><span class="style1" lang="en-us">Long 
                                                                Description :-</span></td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                            <td style="text-align:right;width:50px; font-weight:bold">&nbsp;</td>
                                                            <td style="text-align:left;width:50px;">&nbsp;</td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                        </table>
                                                            <span lang="en-us">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </span>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td style="text-align:left;">
                                              <asp:Label ID="txtJobLongDescr" runat="server" Height="355px" Width="450px" ></asp:Label>
                                           </td> 
                                        </tr>
                                        
                                </table>
                            </td>                            
                        </tr>
                        
                        </table> 
                                </div>
                             </td>
                             </tr>
                             </table>
                             </div>
    </form>
</body>
</html>
