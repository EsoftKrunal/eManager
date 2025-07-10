<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeleteUnitJobs.aspx.cs" Inherits="DeleteUnitJobs" %>
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
            width: 232px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function selectAll(invoker) {
            var inputElements = document.getElementsByTagName('input');
            for (var i = 0; i < inputElements.length; i++) {
                if (i < 10) {
                    chkbox = document.getElementById('rptJobs' + '_ctl0' + i + '_chkSelect');
                }
                else {
                    chkbox = document.getElementById('rptJobs' + '_ctl' + i + '_chkSelect');
                }
                if (chkbox) {
                    chkbox.checked = invoker.checked;
                }
            }
        }
        function refresh() {
            window.opener.reloadjobs();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="height: 23px; text-align :center; padding-top :3px;" class="pagename" >
                    <img runat="server" id="imgHelp" moduleid="10" style ="cursor:pointer;float :right; padding-right :5px;" src="images/help.png" alt="Help ?"/> 
                    Delete Jobs</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <div style="width:100%; height:412px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                        <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr><td align="left" style="padding-left:5px;"></td></tr>
                        <tr>
                        <td align="left" style="padding-left:5px;">
                        <table width="100%" border="0">
                        <tr>
                        <td>
                        <table width="100%" >
                        <tr>
                        <td class="style1" style="width:50%"><asp:Label ID="lblComponent" runat="server" Font-Bold="true"></asp:Label></td>
                        <td style="padding-left:5px;width:50%"><asp:Label ID="lblMessage" Text="" CssClass="error_msg"  runat="server"></asp:Label></td>
                        </tr>
                        </table>
                        </td>
                        </tr>
                        <tr>
                        <td>
                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                <col style="text-align :center" width="30px" />
                                <col style="text-align :left" width="180px"   />
                                <col style="text-align :left" width="300px" />
                               <%-- <col style="text-align :left" width="150px" />
                                <col style="text-align :left" width="150px" />--%>
                                <col width="17px" />
                            </colgroup>
                            <tr class="gridheader">
                                    <td ><input type="checkbox" id="chkAll" onclick='selectAll(this)' /></td>
                                    <td>
                                        Job Code</td>
                                    <td>
                                        Job Name</td>
                                    <%--<td>
                                        Assign To</td>
                                    <td>
                                        Department</td>--%>
                                        <td></td>
                                </tr>
                        </table>
                        <div style="width :100%; overflow-y:scroll; overflow-x:hidden; height :320px;" class="scrollbox" >                        
                            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="text-align :center" width="30px" />
                                    <col style="text-align :left" width="180px"  />
                                    <col style="text-align :left" width="300px" />
                                   <%-- <col style="text-align :left" width="150px" />
                                    <col style="text-align :left" width="150px" />--%>
                                    <col width="17px" />
                                </colgroup>
                            
                            <asp:Repeater ID="rptJobs" runat="server">
                                <ItemTemplate>
                                    <tr class="row">
                                        <td><asp:CheckBox ID="chkSelect" runat="server" /></td>
                                        <td>
                                            <%# Eval("JobCode")%>
                                            <asp:HiddenField ID="hfJobId" Value='<%#Eval("JobId") %>' runat="server" />
                                        </td>
                                        <td>
                                            <%# Eval("JobName") %></td>
                                        <%--<td>
                                            <asp:DropDownList ID="ddlAssignTo" runat="server" DataSource="<%#BindRanks() %>" DataTextField="RankCode" DataValueField="RankId" selectedValue='<%#Eval("AssignTo") %>' Width="150px"  ></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" DataSource="<%#BindDepartments() %>" DataTextField="DeptName" DataValueField="DeptId" selectedValue='<%#Eval("DeptId") %>' Width="150px" ></asp:DropDownList>
                                        </td>--%>
                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="alternaterow" >
                                        <td ><asp:CheckBox ID="chkSelect" runat="server" /></td>
                                        <td>
                                            <%# Eval("JobCode")%>
                                            <asp:HiddenField ID="hfJobId" Value='<%#Eval("JobId") %>' runat="server" />
                                        </td>
                                        <td>
                                            <%# Eval("JobName") %></td>
                                       <%-- <td>
                                            <asp:DropDownList ID="ddlAssignTo" runat="server" DataSource="<%#BindRanks() %>" DataTextField="RankCode" DataValueField="RankId" selectedValue='<%#Eval("AssignTo") %>' Width="150px"  ></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" DataSource="<%#BindDepartments() %>" DataTextField="DeptName" DataValueField="DeptId" selectedValue='<%#Eval("DeptId") %>' Width="150px" ></asp:DropDownList>
                                        </td>--%>
                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                            </table>
                        </div>
                        </td>
                        </tr>
                        <tr>
                        <td align="right"><asp:Button ID="btnDeleteJobs" Text="Delete Jobs" runat="server" onclick="btnDeleteJobs_Click" style="height: 26px" />
                            
                            </td>
                            </tr>
                        </table> 
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
