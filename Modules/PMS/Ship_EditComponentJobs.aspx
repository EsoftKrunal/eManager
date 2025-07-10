<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ship_EditComponentJobs.aspx.cs" Inherits="Ship_EditComponentJobs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>eMANAGER</title>
      <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
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
        function fncInputDecimalValuesOnly(evnt) {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57 || event.keyCode == 46)) {
                event.returnValue = false;
            }
        } 
         function opendescrwin(mode,vesselcode,jobid)
         {
            window.open('UpdateJobDescription.aspx?Mode='+ mode + '&VesselCode=' + vesselcode + '&JobId=' + jobid,'','status=1,scrollbars=0,toolbar=0,menubar=0,width=500,height=550');
         }        
         function refreshonaddjobs() {
             window.opener.reloadunits();
         }
         function refreshgrid()
         {
           __doPostBack('btnRefreshGrid', '');
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center"  class="text headerband" >
                    Edit Jobs to Component&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <div style="width:100%; height:550px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
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
                        </table>
                        </td>
                        </tr>
                        <tr>
                        <td>
                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                <col style="text-align :left" width="5%" />
                                <col style="text-align :left" width="22%"/> 
                                <col style="text-align :left" width="5%" />                               
                                <col style="text-align :left" width="8%" />                                
                                <col style="text-align :left" width="21%" />
                                <col style="text-align :left" width="7%" />
                                <col style="text-align :left" width="10%" />
                                <col style="text-align :left" width="10%" />
                                <col style="text-align :left" width="6%"/>
                                <col style="text-align :left" width="4%" />                                
                                <col width="3%" />
                            </colgroup>
                            <tr class= "headerstylegrid">
                                    <td>Job Code</td>
                                    <td>Job Description</td>
                                    <td>Update Descr.</td>                                            
                                    <td>Rank</td>
                                    <td>Job Interval</td>
                                    <td>Last Hour</td>
                                    <td>Last Date</td>
                                    <td>Est. Job Cost(US$)</td>
                                    <td>Class Job</td>
                                    <td style="text-align:center"><img src="Images/paperclip.gif" style="border:none;" title="Attachment"/></td>                                  
                                    <td></td>
                                </tr>
                        </table>
                        <div id="dvScroll"  onscroll="SetScrollPos(this)" style="width :100%; overflow-y:scroll; overflow-x:hidden; height :300px;" class="scrollbox" >                        
                            <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>                                     
                                    <col style="text-align :left" width="5%" />
                                <col style="text-align :left" width="22%"/> 
                                <col style="text-align :left" width="5%" />                               
                                <col style="text-align :left" width="8%" />                                
                                <col style="text-align :left" width="21%" />
                                <col style="text-align :left" width="7%" />
                                <col style="text-align :left" width="10%" />
                                <col style="text-align :left" width="10%" />
                                <col style="text-align :left" width="6%"/>
                                <col style="text-align :left" width="4%" />                                
                                <col width="2%" />
                                                          
                                     </colgroup> 
                                 <asp:Repeater ID="rptJobs" runat="server">
                                     <ItemTemplate>
                                         <tr class="row">
                                             <td><%#Eval("JobCode") %>
                                                 <asp:HiddenField ID="hfJobId" runat="server" Value='<%#Eval("CompJobId") %>' />
                                             </td>
                                             <td><%#Eval("JobName")%><span class="critical" style='<%#(Eval("IsCritical").ToString()=="True")?"":"display:none"%>' title="Critical Job">*</span></td>
                                             <td style="text-align:center">
                                                 <asp:ImageButton ID="imgEditDescr" runat="server" CommandArgument='<%#Eval("CompJobId") %>' ImageUrl="~/Modules/HRD/Images/edit.png" OnClick="imgEditDescr_Click" ToolTip="Update Description" />
                                                 <asp:HiddenField ID="hfDescr" runat="server" Value='<%#Eval("DescrM") %>' />
                                             </td>
                                             <td>
                                                 <asp:DropDownList ID="ddlAssignTO" runat="server" DataSource='<%#BindRanks(Eval("CompJobId"))%>' DataTextField="RankCode" DataValueField="RankId" required="yes" SelectedValue='<%#Eval("AssignTo") %>' Width="80px">
                                                 </asp:DropDownList>
                                             </td>
                                             <td style="text-align :left;display :flex;">
                                                 <asp:TextBox ID="txtInterval" runat="server" MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("Interval") %>' Width="40px"></asp:TextBox>
                                                 <asp:DropDownList ID="ddlInterval" runat="server" AutoPostBack="true" DataSource="<%#BindInterval() %>" DataTextField="IntervalName" DataValueField="IntervalId" OnSelectedIndexChanged="ddlInterval_SelectedIndexChanged" SelectedValue='<%#Eval("IntervalId") %>' Width="40px">
                                                 </asp:DropDownList>
                                                 <asp:Label ID="lblOR" runat="server" Font-Bold="true" style="width:5px;" Text="OR" Visible='<%#Eval("IntervalId").ToString()== "1" %>'></asp:Label>
                                                 <asp:TextBox ID="txtORInt" runat="server" MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)" Text='<%#Eval("Interval_H").ToString()== "0" ? "" : Eval("Interval_H") %>' Visible='<%# Eval("IntervalId").ToString() == "1" %>' Width="40px"></asp:TextBox>
                                                 <asp:DropDownList ID="ddlORInt" runat="server" AutoPostBack="true" DataSource="<%#BindORInterval() %>" DataTextField="IntervalName" DataValueField="IntervalId" OnSelectedIndexChanged="ddlInterval_SelectedIndexChanged" SelectedValue='<%#Eval("IntervalId_H") %>' Visible='<%# Eval("IntervalId").ToString() == "1" %>' Width="40px">
                                                 </asp:DropDownList>
                                             </td>
                                             <td>
                                                 <asp:TextBox ID="txtLastHour" runat="server" MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)" style="text-align:center" Text='<%#Eval("LastHour") %>' Width="40px" Visible='<%# Eval("IntervalId").ToString() == "1" %>'></asp:TextBox>
                                             </td>
                                             <td>
                                                 <asp:TextBox ID="txtStartDate" runat="server" MaxLength="11" onfocus="showCalendar('',this,this,'','holder1',-205,22,1)" style="text-align:center" Text='<%#Eval("StartDate") %>' Width="90px"></asp:TextBox>
                                             </td>
                                             <td>
                                                 <asp:TextBox ID="txtjobcost" runat="server" MaxLength="5" onkeypress="fncInputDecimalValuesOnly(event)" style="text-align:right" Text='<%#Eval("jOBcOST")%>' Width="120px"></asp:TextBox>
                                             </td>
                                             <td style="text-align:center">
                                                 <asp:CheckBox ID="chkClass" runat="server" Checked='<%#Eval("ClassJob").ToString()=="True"%>' />
                                             </td>
                                             <td><a href='ShipDocManagement.aspx?CJID=<%#Eval("CompJobID")%>&amp;VesselCode=<%#Eval("VesselCode")%>' target="_blank">
                                                 <img src="Images/paperclip.gif" style="border:none;" title="Attachment"/>
                                                 </a>
                                                 <asp:TextBox ID="txtGuidelines" runat="server" MaxLength="50" Text='<%#Eval("Guidelines") %>' Visible="false" Width="100px"></asp:TextBox>
                                             </td>
                                             <td></td>
                                         </tr>
                                     </ItemTemplate>
                            </asp:Repeater>
                            
                                 </td>
                             </tr>
                              <table>
                            </table>
                              </table>
                            
                            </div>
                            <tr>
                                <td align="right">
                                    <div style="width:90%;float:left;">
                                        <uc1:MessageBox ID="MessageBox1" runat="server" />
                                    </div>
                                    &nbsp;
                                    <asp:Button ID="btnAddJobs" runat="server" CssClass="btn" onclick="btnAddJobs_Click" Text="Save" Width="80px" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            
                            </table>
                        </td>
                        </tr>                        
                        </table> 
                            <asp:Panel runat="server" ID="pnlvsls" Visible="false">
                                <div style="padding:5px; font-size:13px; color:maroon;font-weight:bold; height:100px; overflow-y:scroll;overflow-x:hidden">
                                    <asp:Literal runat="server" ID="ltmsg"></asp:Literal>
                                    <asp:CheckBoxList id="chkVsls" runat="server"  RepeatDirection="Horizontal" RepeatColumns="15"></asp:CheckBoxList>
                                </div>
                                <div style="text-align:center;">
                                    <asp:Button ID="btnApply" Text="Apply" runat="server" onclick="btnApply_Click" Width="80px" />
                                </div>
                            </asp:Panel>                            
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
