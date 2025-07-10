<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddJobsToComponentUnits.aspx.cs" Inherits="AddJobsToComponentUnits" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 232px;
        }
    </style>   
    <script type="text/javascript" >
         function fncInputNumericValuesOnly(evnt) {
             if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                 event.returnValue = false;
             }
         }
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
         function refreshonaddjobs() {
             window.opener.reloadunits();
         }
         function opendescrwin(mode,vesselcode,jobid)
         {
            window.open('UpdateJobDescription.aspx?Mode='+ mode + '&VesselCode=' + vesselcode + '&JobId=' + jobid,'','status=1,scrollbars=0,toolbar=0,menubar=0,width=500,height=450');
         }
         function refreshgrid()
         {
           __doPostBack('btnRefreshGrid', '');
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
                <td align="center"  class="text headerband" > 
                    Add Jobs to Components&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <div style="width:100%; height:450px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                        <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr><td align="left" style="padding-left:10px;"><asp:Button ID="btnRefreshGrid" style="display:none"
                                runat="server" onclick="btnRefreshGrid_Click" /></td></tr>
                        <tr>
                        <td align="left" style="padding-left:5px;">
                        <table width="100%" border="0">
                        <tr>
                        <td>
                        <table width="100%" >
                        <tr>
                        <td class="style1" style="width:80%"><asp:Label ID="lblComponent" runat="server" Font-Bold="true"></asp:Label></td>
                        <td style="padding-left:10px;width:20%">
                        <%--<asp:Label ID="lblMessage" Text="" CssClass="error_msg"  runat="server"></asp:Label>--%>
                        </td>
                        </tr>
                       <%-- <tr><td colspan="2">
                                <asp:RadioButtonList ID="rdoSelect" RepeatDirection="Horizontal" 
                                    AutoPostBack="true" runat="server" 
                                    onselectedindexchanged="rdoSelect_SelectedIndexChanged">
                                <asp:ListItem Text="From Office Master" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="From Job Master" Value="2"></asp:ListItem>
                                </asp:RadioButtonList> 
                            </td>
                        </tr>
                        <tr id="trJobType" runat="server" visible="false"><td colspan="2">
                               <asp:DropDownList ID="ddlJobtype" runat="server" AutoPostBack="True" onselectedindexchanged="ddlJobtype_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                            </td>
                        </tr>--%>
                        </table>
                        </td>
                        </tr>
                        <tr>
                        <td>
                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                <col style="text-align :center" width="30px" />
                                <col style="text-align :left" width="70px" />
                                <col style="text-align :left" width="300px"/> 
                                <col style="text-align :left" width="70px" />                               
                                <col style="text-align :left" width="95px" />
                                <col style="text-align :left" width="210px" />
                                <col style="text-align :left" width="95px" />
                                <%--<col style="text-align :left" width="112px" /> --%>
                                <col width="17px" />
                            </colgroup>
                            <tr class="headerstylegrid">
                                    <td style="text-align :center" width="30px"><input type="checkbox" id="chkAll" onclick='selectAll(this)' /> </td>
                                    <td style="text-align :left" width="70px">
                                        Job Cat.</td>
                                    <td style="text-align :left" width="300px">Job Description</td> 
                                    <td style="text-align :left" width="70px">Update Descr.</td>                                           
                                    <td align="left" style="text-align :left" width="95px">Rank</td>
                                    <td style="text-align :left" width="210px">
                                         Job Interval</td>
                                    <td style="text-align :left" width="95px">
                                        Last Done Dt.</td>
                                    <%--<td>Guidelines</td>                    --%>
                                    <td><input type="checkbox" id="chkAll" onclick='selectAll(this)' /> </td>
                                </tr>
                        </table>
                        <div id="dvScroll"  onscroll="SetScrollPos(this)" style="width :100%; overflow-y:scroll; overflow-x:hidden; height :350px;" class="scrollbox" >                        
                            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="text-align :center" width="30px" />
                                    <col style="text-align :left" width="70px" />
                                    <col style="text-align :left" width="300px" /> 
                                    <col style="text-align :left" width="70px" />                               
                                    <col style="text-align :left" width="95px" />
                                    <col style="text-align :left" width="210px"/>
                                    <col style="text-align :left" width="95px"/>
                                    <%--<col style="text-align :left" width="112px" /> --%>
                                    <col width="17px" />
                                </colgroup>                            
                            <asp:Repeater ID="rptJobs" runat="server">
                                <ItemTemplate>
                                    <tr class="rowgrid">
                                        <td><asp:CheckBox ID="chkSelect" runat="server" /></td>
                                        <td><%#Eval("JobCode") %><asp:HiddenField ID="hfJobId" runat="server" Value='<%#Eval("CompJobId") %>' /></td>
                                        <td><%#Eval("JobName")%>
                                        <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                                        </td>
                                        <td style="text-align:center"><asp:ImageButton ID="imgEditDescr" ImageUrl="~/Modules/HRD/Images/edit.png" OnClick="imgEditDescr_Click" CommandArgument='<%#Eval("CompJobId") %>' ToolTip="Update Description" runat="server" />
                                            <asp:HiddenField ID="hfDescr" Value='<%#Eval("DescrM") %>' runat="server" />
                                            
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlAssignTO" runat="server"  DataSource="<%#BindRanks() %>" DataTextField="RankCode" DataValueField="RankId" SelectedValue='<%#Eval("AssignTo") %>' Width="70px"></asp:DropDownList> 
                                           
                                        </td>                                        
                                        <td style=" display :inline" >
                                            <asp:TextBox ID="txtInterval"  runat="server" MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)" Width="40px"></asp:TextBox>
                                            <asp:DropDownList ID="ddlInterval" runat="server"  DataSource="<%#BindInterval() %>" DataTextField="IntervalName" AutoPostBack="true" DataValueField="IntervalId"  SelectedValue='<%#Eval("IntervalId") %>' OnSelectedIndexChanged="ddlInterval_SelectedIndexChanged" Width="40px"></asp:DropDownList>
                                            <asp:Label ID="lblOR" style="width:5px;" runat="server" Visible='<%#Eval("IntervalId").ToString()== "1" %>' Text="OR" Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtORInt"  runat="server" MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)" Width="40px" Visible='<%# Eval("IntervalId").ToString() == "1" %>'></asp:TextBox>
                                            <asp:DropDownList ID="ddlORInt" runat="server"  DataSource="<%#BindORInterval() %>" DataTextField="IntervalName" AutoPostBack="true" DataValueField="IntervalId" Visible='<%# Eval("IntervalId").ToString() == "1" %>'   OnSelectedIndexChanged="ddlInterval_SelectedIndexChanged" Width="40px"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStartDate"  onfocus="showCalendar('',this,this,'','holder1',-205,22,1)" runat="server" MaxLength="11" Visible='<%#Eval("IntervalId").ToString()!= "1" %>' Width="80px"></asp:TextBox> 
                                            
                                            <asp:TextBox ID="txtGuidelines" runat="server" MaxLength="50" Width="100px" Visible="false"></asp:TextBox>
                                            
                                        </td>
                                        <%--<td>                                         
                                        </td>--%>
                                       <td style="width:17px"></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                                 </td>
                             </tr>
                              </table>
                            
                            </table>
                        </div>
                        </td>
                        </tr>
                        <tr><td align="right"><div style="width:90%;float:left;" ><uc1:MessageBox ID="MessageBox1" runat="server" /></div>&nbsp;<asp:Button 
                                ID="btnAddJobs" Text="Save" runat="server" 
                                onclick="btnAddJobs_Click" Width="80px" /></td></tr>
                                <tr><td></td></tr>
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
    <p>
&nbsp;</p>
</body>
</html>
