<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InsuranceRegistersHome.aspx.cs" Inherits="Modules_Inspection_Registers_InsuranceRegistersHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="text-align: center">
        
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9 " width="100%">
            <tr>
                <td align="center" style=" height:23px;text-align :center;" class="text headerband" >
                   Insurance - Registers
                </td>
            </tr>
            <tr>
                <td style="height:485px; background-color:#f9f9f9" valign="top">
                    <table  border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:10px; padding-left:3px;padding-top:3px;padding-bottom:3px;text-align:left;">
                                <div id="header"><%--tabs--%>
                                     <asp:LinkButton ID ="btnInsGroup" runat="server" CommandArgument="0" Text="Insurance Group" OnClick="btnTabs_Click" style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" /> &nbsp;
                                    <asp:LinkButton ID ="btnInsSubGroup" runat="server" CommandArgument="1" Text="Insurance SubGroup" OnClick="btnTabs_Click" style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" /> &nbsp;
                                     <asp:LinkButton ID ="btnInsUW" runat="server" CommandArgument="2" Text="Insurance UW" OnClick="btnTabs_Click" style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" /> &nbsp;
                                    
                                  
                                  
                                  
                                </div>
                                <script>
                                    function tabing() {
                                        var strHref = window.location.href;
                                        var strQueryString = strHref.split(".aspx");
                                        var aQueryString = strQueryString[0].split("/");
                                        var curpage = unescape(aQueryString[aQueryString.length - 1]).toLowerCase();
                                        //alert(curpage); 
                                        //alert(document.getElementById('navigation1').getElementsByTagName('li').length);
                                        var menunum = document.getElementById('header').getElementsByTagName('li').length;
                                        var menu = document.getElementById('header').getElementsByTagName('li')
                                        if (curpage == 'inspection') {
                                            menu[1].className = 'current';
                                        } else {
                                            for (x = 0; x < menunum; x++) {
                                                //alert(unescape(menu[x].childNodes[0]).toLowerCase());

                                                if (unescape(menu[x].childNodes[0]).toLowerCase().indexOf(curpage) > -1) {
                                                    //alert(menu[x].childNodes[0])
                                                    menu[x].className = 'current';
                                                }

                                            }
                                        }
                                    }
                                    tabing();
                                </script>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-right: 10px; padding-left: 10px; height:10px;">
   <div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="~/Modules/Inspection/Registers/IRM_GroupMaster.aspx" id="frm" frameborder="0" width="100%" height="600px" scrolling="no"></iframe>
</div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
         </td> 
    </tr></table> 
        
     </div>
</asp:Content>


