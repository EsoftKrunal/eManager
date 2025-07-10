<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComponentList.aspx.cs" Inherits="ComponentList" %>
<%@ Register Src="UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="width:150px; text-align :left; vertical-align : top;">
            <uc2:Left runat="server" ID="menu2" />  
        </td>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="height: 23px; text-align :center; padding-top :3px;" class="pagename" >
                    <img runat="server" id="imgHelp" moduleid="10" style ="cursor:pointer;float :right; padding-right :5px;" src="images/help.png" alt="Help ?"/> 
                    Planned Maintenance System ( PMS ) - Component List&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:10px;  padding-left:10px;">
                                <div id="header"><%--tabs--%>
                                  <ul>
                                    <li><a href="ComponentList.aspx"><span>Component List</span></a></li>
                                  </ul>
                                </div>
                                <script>
                                function tabing(){
                                     var strHref = window.location.href;
	                                 var strQueryString = strHref.split(".aspx");
	                                 var aQueryString = strQueryString[0].split("/");
	                                 var curpage =unescape(aQueryString[aQueryString.length-1]).toLowerCase();
	                                 //alert(document.getElementById('navigation1').getElementsByTagName('li').length);
	                                 var menunum=document.getElementById('header').getElementsByTagName('li').length;
	                                 var menu=document.getElementById('header').getElementsByTagName('li')
//	                                 if(curpage=='inspection'){
//	                                     menu[1].className='current';
//	                                 }else{
                                     for(x=0; x<menunum; x++)
	                                 {
	                                    //alert(unescape(menu[x].childNodes[0]).toLowerCase());
	 	                                if(unescape(menu[x].childNodes[0]).toLowerCase().indexOf(curpage) > -1 ){
	 	                                //alert(menu[x].childNodes[0])
		                                menu[x].className='current';
		                                //}
	                                    }
	                                 }
                                }
                                tabing();
                                </script>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-right: 10px; padding-left: 10px;">
                            <div style="width:100%; height:417px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                            <asp:UpdatePanel runat="server" id="up1">
                            <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;">
                        <tr><td colspan="2" align="left" style="padding-left:5px;"><asp:Label ID="lblMessage" Text="" CssClass="error_msg"  runat="server"></asp:Label></td></tr>
                        <tr><td colspan="2" style="text-align:left; padding:0 0 3px 5px;">Select Vessel :&nbsp;<asp:DropDownList ID="ddlVessels" AutoPostBack="true" runat="server" onselectedindexchanged="ddlVessels_SelectedIndexChanged"></asp:DropDownList></td></tr>
                        <tr>
                        <td align="left" style="width:35%;">
                        <div style="width :100%; overflow-y:scroll; overflow-x:hidden; height :380px;" class="scrollbox" >
                           <asp:TreeView ID="tvComponents" runat="server" ImageSet="Arrows" ShowLines="True" onselectednodechanged="tvComponents_SelectedNodeChanged">
                            <HoverNodeStyle CssClass="treehovernode" />
                            <SelectedNodeStyle CssClass="treeselectednode" />
                            <NodeStyle CssClass="treenode" />
                            </asp:TreeView>
                            </div>
                        </td>
                        <td align="left" style="width:65%; padding-left:5px;">
                         <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:350px;" />
                        <col style="width:300px;" />
                        <col style="width:17px;" />                        
                        <tr align="left" class= "headerstylegrid">
                            <td>Component Units</td>
                            <td>Jobs</td>               
                            <td></td>
                        </tr>
                    </colgroup>
                </table>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:350px;" />
                                    <col style="width:300px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptComponentUnits" runat="server">
                                    <ItemTemplate>
                                            <tr class="row">
                                            <td align="left"><%#Eval("ComponentName")%></td>
                                            <td align="left"><%#Eval("JobName")%></td>
                                           </tr>
                                    </ItemTemplate> 
                                    <AlternatingItemTemplate>
                                          <tr class="alternaterow">
                                            <td align="left"><%#Eval("ComponentName")%></td>
                                            <td align="left"><%#Eval("JobName")%></td>
                                          </tr>
                                    </AlternatingItemTemplate>                       
                                </asp:Repeater>
                            </table>
                           </div>
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
    </form>
</body>
</html>