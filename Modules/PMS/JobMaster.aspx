<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobMaster.aspx.cs" Inherits="JobMaster" %>
<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/Common.js"></script>
    <script type="text/javascript" language="javascript">
        function openaddwindow(JTID) {
            window.open('AddEditJobs.aspx?Mode=ADD&JTID=' + JTID , '', 'status=1,scrollbars=0,toolbar=0,menubar=0,width=700,height=450');
        }
        function openeditwindow(JTID, JobId) {
            window.open('AddEditJobs.aspx?Mode=EDIT&JTID=' + JTID + '&JID=' + JobId, '', 'status=1,scrollbars=0,toolbar=0,menubar=0,width=700,height=450');
        }
        function openwindow(JTID, JobId) {
            window.open('AddEditJobs.aspx?Mode=View&JTID=' + JTID + '&JID=' + JobId, '', 'status=1,scrollbars=0,toolbar=0,menubar=0,width=700,height=450');
        }
        function reloadme() {
            __doPostBack('btnRefresh', '');
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
     <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr><td>
        <hm:HMenu runat="server" ID="menu2" />  
        </td></tr>
        <tr>
        <%--<td style="width:150px; text-align :left; vertical-align : top;">
            <uc2:Left runat="server" ID="menu2" />  
        </td>--%>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <%--<tr>
                <td align="center" style="height: 23px; text-align :center; padding-top :3px;" class="pagename" >
                    <img runat="server" id="imgHelp" moduleid="10" style ="cursor:pointer;float :right; padding-right :5px;" src="images/help.png" alt="Help ?"/> 
                    Planned Maintenance System ( PMS ) - Office Master</td>
            </tr>--%>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:5px; padding-left:2px;">
                                <div id="header"><%--tabs--%>
                                  <ul>
                                    <li><a href="JobMaster.aspx"><span>Job Master</span></a></li>
                                    <li><a href="Components.aspx"><span>Component Management</span></a></li>                                    
                                    <%--<li><a href="JobsMapping.aspx"><span>Jobs Mapping</span></a></li>--%>                                    
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
                             <asp:Button ID="btnRefresh" style="display:none;" runat="server" onclick="btnRefresh_Click" />   
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style=" padding-left: 2px; padding-top:2px">
                            <div style="width:100%; height:417px; border:0px solid #000;overflow:hidden" >
                            <asp:UpdatePanel runat="server" id="up1">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;">
                                    <tr>
                                     <td align="left" style="width:25%;">
                                        <table cellspacing="0" rules="all" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="100px" />
                                                <col style="text-align :left" />
                                                <col width="17px" />
                                                <tr class="gridheader">
                                                                                                      <td>
                                                        Job Name</td>
                                                </tr>
                                            </colgroup>
                                    </table> 
                                        <div style="width :100%; overflow-y:scroll; overflow-x:hidden; height :390px;" class="scrollbox" >                        
                                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="100px" />
                                                <col style="text-align :left" />
                                            </colgroup>
                                            <asp:Repeater ID="rptItems" runat="server">
                                                <ItemTemplate>
                                                    <tr class='<%# (Eval("JobId").ToString()==JobId.ToString())?"selectedrow":"row" %>'>
                                                        <td>
                                                            <asp:LinkButton ID="btnJobName" runat="server" 
                                                                CommandArgument='<%# Eval("JobId") %>' OnClick="btnJobName_Click" 
                                                                Text='<%# Eval("JobCode") %>'></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("JobName") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class='<%# (Eval("JobId").ToString().Trim()==JobId.ToString().Trim())?"selectedrow":"row" %>'>
                                                        <td>
                                                            <asp:LinkButton ID="btnJobName" runat="server" 
                                                                CommandArgument='<%# Eval("JobId") %>' OnClick="btnJobName_Click" 
                                                                Text='<%# Eval("JobCode") %>'></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("JobName") %></td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                             </table>
                                        </div> 
                                    </td>
                                     <td align="left" style="padding-left :5px; width:75%">
                                        <div style=" padding-top :2px; padding-bottom:2px; float :left " >
                                         <asp:ImageButton ID="btnAddComponents" ImageUrl="~/Modules/PMS/Images/add1.png" 
                                                runat="server"  ToolTip="Add New Job" style="float :left;padding-left :5px;" 
                                                onclick="btnAddComponents_Click" /> 
                                         <asp:ImageButton ID="btnEditComponents" ImageUrl="~/Modules/PMS/Images/edit1.png" 
                                                runat="server" ToolTip="Edit Selected Job" 
                                                style="float :left; padding-left :5px;" onclick="btnEditComponents_Click"/>
                                         <%--<asp:ImageButton ID="btnDeleteComponents" ImageUrl="~/Modules/PMS/Images/delete1.png" runat="server" OnClientClick="javascript:return confirm('Are you sure to delete this component?')" ToolTip="Delete Selected Job" style="float :left; padding-left :5px;"  onclick="btnDeleteComponents_Click" />--%>
                                        </div>
                                        <div style=" padding:5px;" >
                                        <uc1:MessageBox ID="MessageBox1" runat="server" />
                                        </div>
                                        <table cellspacing="0" rules="all" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                            <colgroup>                                                
                                                <col style="text-align :left; width:70px" />
                                                <col style="text-align :left; " />
                                                <col style="text-align :left; width:85px" />
                                                <col style="text-align :left; width:100px" />
                                                <col style="text-align :left; width:80px" />
                                                <col style="text-align :left; width:30px" />                                                                                                                                               
                                                <col width="17px" />
                                                <tr class="gridheader">                                                    
                                                    <td>Job Code</td>
                                                    <td>Job Name</td>
                                                    <td>Department</td>
                                                    <td>Interval Type</td>
                                                    <td>Assign To</td>
                                                    <td></td>
                                                    <td></td>                                                                                                                                                                
                                                </tr>
                                            </colgroup>
                                    </table> 
                                        <div id="dvscroll_JobMaster" style="width :100%; overflow-y:scroll; overflow-x:hidden; height :367px;" class="scrollbox" onscroll="SetScrollPos(this)">                        
                                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left; width:70px" />                                                
                                                <col style="text-align :left;" />
                                                <col style="text-align :left; width:85px" />
                                                <col style="text-align :left; width:100px" />
                                                <col style="text-align :left; width:80px" />
                                                <col style="text-align :center; width:30px" />
                                                <col width="17px" />                
                                            </colgroup>
                                            <asp:Repeater ID="rptJobs" runat="server">
                                                <ItemTemplate>
                                                    <tr class='<%# (Eval("JobId").ToString()==JobId.ToString())?"selectedrow":"row" %>'>                                                        
                                                        <td>
                                                        <asp:LinkButton ID="btnJobCode" runat="server" CommandArgument='<%# Eval("JobId") %>' OnClick="btnJobCode_Click" Text='<%# Eval("JobCode") %>'></asp:LinkButton>
                                                        </td>
                                                        <td><%# Eval("DescrSh")%><span class="critical" title="Critical Job"  style='<%#(Eval("IsCritical").ToString()=="Y")?"":"display:none"%>'>*</span></td>
                                                        <td><%# Eval("DeptName")%></td>
                                                        <td><%# Eval("IntervalName")%></td>
                                                        <td><%# Eval("RankCode")%></td>                                                        
                                                        <td><asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("JobId") %>' ImageUrl="~/Images/magnifier.png" OnClick="btnView_Click" ToolTip="View" /></td>
                                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>                                                     
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class='<%# (Eval("JobId").ToString()==JobId.ToString())?"selectedrow":"row" %>'>                                                        
                                                        <td>
                                                        <asp:LinkButton ID="btnJobCode" runat="server" CommandArgument='<%# Eval("JobId") %>' OnClick="btnJobCode_Click" Text='<%# Eval("JobCode") %>'></asp:LinkButton>
                                                        </td>
                                                        <td><%# Eval("DescrSh")%><span class="critical" title="Critical Job"  style='<%#(Eval("IsCritical").ToString()=="Y")?"":"display:none"%>'>*</span></td>
                                                        <td><%# Eval("DeptName")%></td>
                                                        <td><%# Eval("IntervalName")%></td>
                                                        <td><%# Eval("RankCode")%></td>                                                        
                                                        <td><asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("JobId") %>' ImageUrl="~/Images/magnifier.png" OnClick="btnView_Click" ToolTip="View" /></td>
                                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
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
     <mtm:footer ID="footer1" runat ="server" />
    </form>
</body>
</html>
