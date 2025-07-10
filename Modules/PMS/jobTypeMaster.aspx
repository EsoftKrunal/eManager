<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jobTypeMaster.aspx.cs" Inherits="jobTypeMaster" MasterPageFile="~/MasterPage.master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
             <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style=" text-align :center; padding-top :4px;" class="text headerband"  >
        Job Type Master
        </div>
     <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">           
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <%--<tr>
                            <td style="padding-right:5px; padding-left:2px;">
                                <div id="header">
                                  <ul>
                                    <li><a href="JobMaster.aspx"><span>Job Master</span></a></li>
                                    <li><a href="Components.aspx"><span>Component Management</span></a></li>
                                  </ul>
                                </div>
                                <script>
                                function tabing(){
                                     var strHref = window.location.href;
	                                 var strQueryString = strHref.split(".aspx");
	                                 var aQueryString = strQueryString[0].split("/");
	                                 var curpage =unescape(aQueryString[aQueryString.length-1]).toLowerCase();
	                                 var menunum=document.getElementById('header').getElementsByTagName('li').length;
	                                 var menu=document.getElementById('header').getElementsByTagName('li')
                                     for(x=0; x<menunum; x++)
	                                 { 
	 	                                if(unescape(menu[x].childNodes[0]).toLowerCase().indexOf(curpage) > -1 ){
		                                menu[x].className='current';
	                                    }
	                                 }
                                }
                                tabing();
                                </script>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2" style="padding-right: 10px; padding-left: 2px; padding-top:2px">
                            <div style="width:100%; height:417px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                            <asp:UpdatePanel runat="server" id="up1">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;">
                                    <tr>
                                     <td align="left" style="width:35%;">
                                     <div class="box1">Jobs Types</div>

                                        <div class="dvScrollheader">  
                                            <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="100px" />
                                                <col style="text-align :left" />
                                                <col width="17px" />
                                             </colgroup>
                                                <tr class= "headerstylegrid">
                                                    <td>Job Code</td>
                                                    <td>Job Name</td>
                                                    <td></td>
                                                </tr>
                                        </table> 
                                        </div>
                                        <div id="dvJM" onscroll="SetScrollPos(this)" style="height :387px;" class="dvScrolldata" >                        
                                        <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="100px" />
                                                <col style="text-align :left" />
                                                <col width="17px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptItems" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="btnJobName" runat="server" CommandArgument='<%# Eval("JobId") %>' OnClick="btnJobName_Click" Text='<%# Eval("JobCode") %>' ></asp:LinkButton>
                                                        </td>
                                                        <td><%# Eval("JobName")%></td>
                                                        <td></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                             </table>
                                        </div> 
                                    </td>
                                    <td style="text-align :right " >
                                        <div class="box1">Add/Modify Jobs <a href="Registers.aspx" style="float:right;">Back</a> </div>
                                        <div style =" padding-left :3px;">
                                        </div> 
                                        <asp:Panel ID="plJobs" runat="server" Width="100%">
                                        <table width="100%" class="formborder" cellpadding="2" cellspacing="0" rules="all" border="1">  
                                        <tr>
                                        <td style="text-align :right">Job Code :</td>
                                        <td style="text-align :left"><asp:TextBox ID="txtJobTypeCode" runat="server" MaxLength="3" Width="55px"></asp:TextBox></td>
                                        </tr>                                        
                                        <tr>
                                        <td style="text-align :right">Job Name :</td>
                                        <td style="text-align :left"><asp:TextBox ID="txtJobTypeName" runat="server" MaxLength="50" Width="350px"></asp:TextBox></td>
                                        </tr>                                       
                                        <tr>
                                        <td>&nbsp;</td>
                                        <td align="center"  style="text-align :left">
                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn"  Text="Add" onclick="btnAdd_Click" />
                                            <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn" onclick="btnSave_Click" />
                                            <asp:Button ID="btnDelete" Text="Delete" runat="server" CssClass="btn" OnClientClick="javascript:return confirm('Are you sure to delete this Job?\n\rIt will also delete dependent data');" Visible="false" onclick="btnDelete_Click" />
                                        </td>
                                        </tr>
                                        </table>  
                                        <div style=" padding:5px;" >
                                        <uc1:MessageBox ID="MessageBox1" runat="server" />
                                        </div>
                                        </asp:Panel>
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
