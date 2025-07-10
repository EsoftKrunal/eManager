<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registers.aspx.cs" Inherits="Registers" MasterPageFile="~/MasterPage.master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
         <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <div style=" text-align :center; padding-top :4px;" class="text headerband"  >
        PMS - Registers
        </div>
     <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style=" text-align :left; vertical-align : top;" >
        <table border="5" cellpadding="5" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        
                        <tr>
                            <td style="padding-right: 10px; padding-left: 2px; padding-top:2px">
                            <div style="width:100%; height:417px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                            <asp:UpdatePanel runat="server" id="up1">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 0px solid; border-top:#8fafdb 0px solid;">
                                    <tr>
                                     <td style="text-align :Center " >
                                     <a href="jobTypeMaster.aspx">Job Type</a>
                                        
                                    </td>
                                    <td style="text-align :Center " >
                                       <a href="DryDock/DD_DockingCategoryMaster.aspx">Dry Dock Category</a> 
                                    </td>
                                     <td style="text-align :Center " >
                                       <a href="DryDock/DD_YardMaster.aspx">Yard Master</a> 
                                    </td>
                                    <td style="text-align :Center " >
                                        <a href="Registers/SpareStockLocation.aspx">Spare Stock Location</a> 
                                    </td>
                                    </tr>
                                </table>
                            </ContentTemplate> 
                            </asp:UpdatePanel> 
                            </div> 
                </td>
            </tr>
        </table>
        </td>
        </tr>
        </table>
        </td> 
        </tr>
        </table>
     </div>
     </asp:Content>
