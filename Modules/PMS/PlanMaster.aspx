<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanMaster.aspx.cs" Inherits="PlanMaster" %>
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
    <script type="text/javascript" language="javascript">
    function refreshparent()
    {
       window.opener.reloadme();
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
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">                        
                        <tr>
                            <td colspan="2" style=" padding-left: 2px; padding-top:2px">
                            <div style="width:100%; height:450px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                            <asp:UpdatePanel runat="server" id="up1">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;">
                                    <tr>
                                    <td style="text-align :right " >
                                        <div style =" padding-left :3px;">
                                        <div style="height: 20px; text-align :center; padding-top :4px; width:100% " class="orangeheader">
                                        <asp:Label ID="lblPageTitle" runat="server" ></asp:Label>
                                        </div> 
                                        </div> 
                                        <asp:Panel ID="plJobs" runat="server" Width="100%" >
                                        <%--<table cellpadding="4" cellspacing="5" width="100%">  
                                        <tr>
                                        <td style="text-align :right">VesselCode :</td>
                                        <td style="text-align :left">
                                            <asp:Label ID="lblVesselCode" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td style="text-align :right">Component Code :</td>
                                        <td style="text-align :left">
                                            <asp:Label ID="lblCompCode" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td style="text-align :right">Component Name :</td>
                                        <td style="text-align :left">
                                            <asp:Label ID="lblCompName" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr> 
                                        
                                        <tr>
                                        <td style="text-align :right">Job Code :</td>
                                        <td style="text-align :left">&nbsp;<asp:Label ID="lblJobCode" runat="server" 
                                                Text="Label"></asp:Label>
                                             </td>
                                        </tr>
                                        
                                        <tr>
                                        <td style="text-align :right">Description :</td>
                                        <td style="text-align :left">
                                            <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                        <td style="text-align :right">Interval Type :</td>
                                        <td style="text-align :left">
                                            <asp:Label ID="lblIntType" runat="server" Text="Label"></asp:Label>
                                            <asp:HiddenField ID="hfdIntervalId" runat="server" />
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                        <td style="text-align :right">Interval :</td>
                                        <td style="text-align :left">
                                            <asp:Label ID="lblInterval" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                        <td style="text-align :right">Status :</td>
                                        <td style="text-align :left">
                                            <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                        <td style="text-align :right">Next Due Date :</td>
                                        <td style="text-align :left">
                                            <asp:Label ID="lblNextDueDt" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                        <td style="text-align :right">Next Hour :</td>
                                        <td style="text-align :left">
                                            <asp:Label ID="lblNextHour" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                         <tr>
                                        <td style="text-align :right">Department :</td>
                                        <td style="text-align :left">
                                            <asp:DropDownList ID="ddlDept" runat="server" Width="100px">
                                            </asp:DropDownList>
                                             </td>
                                        </tr>
                                        
                                        <tr>
                                        <td style="text-align :right">Assigned To :</td>
                                        <td style="text-align :left">
                                            <asp:DropDownList ID="ddlRank" runat="server" Width="100px">
                                            </asp:DropDownList>
                                        </td>
                                        </tr>                                      
                                        
                                        <tr><td colspan="2"></td></tr>                        
                                        <tr>
                                        <td>&nbsp;</td>
                                        <td align="center"  style="text-align :left">
                                            <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn" onclick="btnSave_Click" />
                                            </td>
                                        </tr>
                                        <tr><td colspan="2"><div style=" padding:5px;" >
                                        <uc1:MessageBox ID="MessageBox1" runat="server" />
                                        </div></td></tr>               
                                        </table>--%>
                                        <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                               <%-- <col style="width: 20px;" />--%>
                                                <col style="width: 85px;" />
                                                <col style="width: 100px;" />
                                                <col style="width: 65px;" />
                                                <col  />
                                                <col style="width: 65px;" />
                                                <col style="width: 40px;" />                                        
                                                <col style="width: 90px;" />
                                                <col style="width: 60px;" />
                                                <col style="width: 85px;" />
                                                <col style="width: 60px;" />
                                                <col style="width: 40px" />
                                                <col style="width: 70px;" />
                                                <col style="width: 80px;" />
                                                <col style="width: 17px;" />
                                                <tr align="left" class= "headerstylegrid">
                                                   <%-- <td>
                                                    </td>--%>
                                                    <td align="left">
                                                        Comp. Code
                                                    </td>
                                                    <td align="left">
                                                        Comp. Name
                                                    </td>
                                                    <td align="left">
                                                        Job Code
                                                    </td>
                                                    <td>
                                                        Job Description
                                                    </td>
                                                    <td align="left">
                                                        Int. Type
                                                    </td>                                            
                                                    <td>
                                                        Int.
                                                    </td>
                                                    <td>
                                                        Last Done Dt.
                                                    </td>
                                                    <td>
                                                        Last Hrs
                                                    </td>
                                                    <td>
                                                        Next Due Dt.
                                                    </td>
                                                    <td>
                                                        Next Hrs
                                                    </td>
                                                    <td>
                                                        Status
                                                    </td>
                                                    <td>
                                                        Rank 
                                                    </td>
                                                    <td align="left">
                                                        Planned Dt.
                                                    </td>
                                                    <td>                                                        
                                                    </td>
                                                </tr>
                                    </colgroup>
                                </table>
                                <div id="dvScroll"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 260px ; text-align:center;">
                                    <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                   <%-- <col style="width: 20px;" />--%>
                    <col style="width:85px;" />
                    <col style="width: 100px;" />
                    <col style="width:65px;" />
                    <col  />
                    <col style="width:65px;" />
                    <col style="width:40px;" />                    
                    <col style="width:90px;" />
                    <col style="width:60px;" />
                    <col style="width:85px;" />
                    <col style="width:60px;" />
                    <col style="width:40px" />
                    <col style="width:70px;" />
                    <col style="width:80px;" />
                    <col style="width:17px;" />
                    </colgroup>
               <asp:Repeater ID="rptPlanJobs" runat="server">
                  <ItemTemplate>
                      <tr class="row">
                           <%--<td align="center"><asp:CheckBox ID="checkSelect" runat="server" /></td>--%>
                           <td align="left"><%#Eval("ComponentCode")%><asp:HiddenField ID="hfCompId" Value='<%#Eval("ComponentId") %>' runat="server" /></td>
                           <td align="left"><%#Eval("ComponentName")%><asp:HiddenField ID="hfVesselCode" Value='<%#Eval("VesselCode") %>' runat="server" /></td>
                           <td align="center"><%#Eval("JobCode")%><asp:HiddenField ID="hfjobId" Value='<%#Eval("JobId") %>' runat="server" /></td>
                           <td align="left"><%#Eval("JobName")%></td>
                           <td align="center"><%#Eval("IntervalName")%></td>
                           <td align="center"><%#Eval("Interval")%></td>                           
                           <td align="center"><%#Eval("LastDone")%></td>
                           <td align="center"><%# Eval("LastHour").ToString() == "0" ? "" : Eval("LastHour")%></td>                           
                           <td align="center"><%#Eval("NextDueDate")%></td>                           
                           <td align="center"><%#Eval("NextHour").ToString() == "0" ? "" : Eval("NextHour")%></td>
                           <td align="left"><%#Eval("WorkOrderStatus")%></td>
                           <td align="left"><asp:DropDownList ID="ddlAssignTO" runat="server" DataSource="<%#BindRanks() %>" DataTextField="RankCode" DataValueField="RankId" SelectedValue='<%#Eval("RankId") %>' Width="60px"></asp:DropDownList></td>
                           <td align="left"><asp:TextBox ID="txtForDate" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="70px" runat="server" ></asp:TextBox></td>
                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                       </tr>
                   </ItemTemplate>
                   <AlternatingItemTemplate>
                       <tr class="alternaterow">
                           <%--<td align="center"><asp:CheckBox ID="checkSelect" runat="server" /></td>--%>
                           <td align="left"><%#Eval("ComponentCode")%><asp:HiddenField ID="hfCompId" Value='<%#Eval("ComponentId") %>' runat="server" /></td>
                           <td align="left"><%#Eval("ComponentName")%><asp:HiddenField ID="hfVesselCode" Value='<%#Eval("VesselCode") %>' runat="server" /></td>
                           <td align="center"><%#Eval("JobCode")%><asp:HiddenField ID="hfjobId" Value='<%#Eval("JobId") %>' runat="server" /></td>
                           <td align="left"><%#Eval("JobName")%></td>
                           <td align="center"><%#Eval("IntervalName")%></td>
                           <td align="center"><%#Eval("Interval")%></td>                           
                           <td align="center"><%#Eval("LastDone")%></td>
                           <td align="center"><%# Eval("LastHour").ToString() == "0" ? "" : Eval("LastHour")%></td>                           
                           <td align="center"><%#Eval("NextDueDate")%></td>                           
                           <td align="center"><%#Eval("NextHour").ToString() == "0" ? "" : Eval("NextHour")%></td>
                           <td align="left"><%#Eval("WorkOrderStatus")%></td>
                           <td align="left"><asp:DropDownList ID="ddlAssignTO" runat="server" DataSource="<%#BindRanks() %>" DataTextField="RankCode" DataValueField="RankId" SelectedValue='<%#Eval("RankId") %>' Width="60px"></asp:DropDownList></td>
                           <td align="left"><asp:TextBox ID="txtForDate" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="70px" runat="server" ></asp:TextBox></td>
                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                           </tr>
                    </AlternatingItemTemplate>
                  </asp:Repeater>
                 </table>
                                </div>
                                        </asp:Panel>
                                    </td>
                                    </tr>
                                    <tr><td>&nbsp;</td></tr>                        
                                        <tr>                                        
                                        <td align="center"  style="text-align :right">
                                            <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn" onclick="btnSave_Click" />
                                            </td>
                                        </tr>
                                        <tr><td><div style=" padding:5px;" >
                                        <uc1:MessageBox ID="MessageBox1" runat="server" />
                                        </div></td></tr> 
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
