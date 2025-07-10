<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_LeaveStatusOnDateReport.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_HR_LeaveStatusOnDateReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Status On Date Report</title>
    <style type="text/css">
    body
    {
    	font-family:Arial;
    	font-size:12px; 
    }
    </style> 
    
    <script type="text/javascript">
    window.focus();
    window.moveTo( 0, 0 );
    window.resizeTo( screen.availWidth, screen.availHeight );
    </script> 
     
</head>
<body onclick="document.getElementById('btnprnt').style.display='';">
    <form id="form1" runat="server">
       <div>                   
                            <center> <h3>Leave Status</h3></center>
                            <br />
                            <b >Employee <span lang="en-us">Details</span> : </b>
                            <br /><br />
                            <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                                    <colgroup>
                                    <col style="width:80px"/>
                                    <col />
                                    <col style="width:80px"/>
                                    <col />
                                    <col style="width:80px"/>
                                    <col />
                                    <col style="width:60px"/>
                                    <col />
                                    <col style="width:50px"/>
                                    <col />
                                    </colgroup>
                                   <tr>
                                       <td style="text-align :right">
                                           Emp Name :&nbsp;</td>
                                       <td style="text-align :left">
                                           <asp:Label ID="lblEmpName" runat="server" ></asp:Label>
                                       </td>
                                       <td style="text-align :right">
                                           Position :&nbsp;</td>
                                       <td style="text-align :left">
                                           <asp:Label ID="lblDesignation" runat="server" ></asp:Label>
                                       </td>
                                       <td style="text-align :right">
                                           Department :&nbsp;</td>
                                       <td style="text-align :left">
                                          <asp:Label ID="lblDepartment" runat="server"></asp:Label> 
                                       </td>
                                       <td style="text-align :right">
                                           Office :&nbsp;</td>
                                       <td>
                                            <asp:Label ID="lblOffice" runat="server"></asp:Label>  
                                       </td>
                                       <td style="text-align :right">
                                           Year :&nbsp;</td>
                                       <td style="text-align :left">
                                            <asp:Label ID="lblCurrentYear" runat="server"></asp:Label>  
                                       </td>
                                   </tr>
                            </table>
                            <br />
                            <b >Leave Summary : </b>
                            <br /><br />
                            <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                            <colgroup>
                            <col />
                            <col style="width:10%"/>
                            <col />
                            <col style="width:10%"/>
                            <col />
                            <col style="width:10%"/>
                            </colgroup>
                                <tr>
                                   <td style="text-align :right">
                                       Last Year Balance-<asp:Label runat="server" ID="lblLastYear"></asp:Label> :&nbsp;</td>
                                   <td style="text-align :left">
                                       <asp:Label ID="lblBalLeaveLastYear" runat="server" MaxLength="100"></asp:Label>
                                   </td>
                                   <td style="text-align :right">
                                      Leave Taken-<asp:Label ID="lblCurYear1" runat="server"></asp:Label>
                                       :&nbsp;</td>
                                   <td style="text-align :left">
                                       <asp:Label ID="lblLeavesTaken" runat="server" MaxLength="100"></asp:Label>
                                   </td>
                                   <td style="text-align :right">
                                       Annual Entitlement-<asp:Label ID="lblCurYear" runat="server"></asp:Label>
                                       :&nbsp;</td>
                                   <td style="text-align :left">
                                       <asp:Label ID="lblAnnualLeaveEntitlement" runat="server" MaxLength="100"></asp:Label>
                                   </td>
                               </tr>
                                <tr>
                                    <td style="text-align :right">
                                        Expired Leave(30th June)-<asp:Label ID="lblCurYear3" runat="server"></asp:Label>
                                        :&nbsp;</td>
                                    <td style="text-align :left">
                                        <asp:Label ID="lblLeaveExpired" runat="server"></asp:Label>
                                    </td>
                                    <td style="text-align :right">
                                        Credit Leave-<asp:Label 
                                            ID="lblCurYear4" runat="server"></asp:Label>
                                        :&nbsp;</td>
                                    <td style="text-align :left">
                                        <asp:Label ID="lblLeaveCredit" runat="server" MaxLength="100"></asp:Label>
                                    </td>
                                    <td style="text-align :right">
                                        Balance Leave-<asp:Label ID="lblCurYear2" runat="server"></asp:Label>
                                        </td>
                                    <td style="text-align :left">
                                        <asp:Label ID="lblAccruedLeave" runat="server" MaxLength="100"></asp:Label>
                                    </td>
                                </tr>
                        </table>
                            <div id="divLeaveTypeSummary" runat="server">
                            <br />
                            <b >Leave Type Summary : </b>
                            <br /><br />
                            <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                                    <colgroup>
                                        <col />
                                        <col style="width:130px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:45px;" />
                                        <col style="width:50px;" />
                                        <%--<col style="width:50px;" />--%>
                                        <tr align="left">
                                            <td align="center" style="font-weight:bold;">Leave Type</td>
                                            <td align="center" style="font-weight:bold;">Leave Assigned</td>
                                            <td align="center" style="font-weight:bold;">Jan</td>
                                            <td align="center" style="font-weight:bold;">Feb</td>
                                            <td align="center" style="font-weight:bold;">Mar</td>
                                            <td align="center" style="font-weight:bold;">Apr</td>
                                            <td align="center" style="font-weight:bold;">May</td>
                                            <td align="center" style="font-weight:bold;">Jun</td>
                                            <td align="center" style="font-weight:bold;">Jul</td>
                                            <td align="center" style="font-weight:bold;">Aug</td>
                                            <td align="center" style="font-weight:bold;">Sep</td>
                                            <td align="center" style="font-weight:bold;">Oct</td>
                                            <td align="center" style="font-weight:bold;">Nov</td>
                                            <td align="center" style="font-weight:bold;">Dec</td>
                                            <td align="center" style="font-weight:bold;">Total</td>
                                            <%--<td align="center" style="font-weight:bold;">Balance</td>--%>
                                        </tr>
                                    </colgroup>
                                   </table>           
                            <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                                        <colgroup>
                                                <col />
                                                <col style="width:130px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:45px;" />
                                                <col style="width:50px;" />
                                                <%--<col style="width:50px;" />--%>
                                         </colgroup>
                                        <asp:Repeater ID="rptLeaveDetails" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td align="left">
                                                        <%#Eval("LeaveTypeName")%></td>
                                                    <td align="center">
                                                        <%#Eval("LeaveCount")%></td>
                                                    <td align="right">
                                                        <%#Eval("Jan")%></td>
                                                    <td align="right">
                                                        <%#Eval("Feb")%></td>
                                                    <td align="right">
                                                        <%#Eval("March")%></td>
                                                    <td align="right">
                                                        <%#Eval("April")%></td>
                                                    <td align="right">
                                                        <%#Eval("May")%></td>
                                                    <td align="right">
                                                        <%#Eval("June")%></td>
                                                    <td align="right">
                                                        <%#Eval("July")%></td>
                                                    <td align="right">
                                                        <%#Eval("August")%></td>
                                                    <td align="right">
                                                        <%#Eval("September")%></td>
                                                    <td align="right">
                                                        <%#Eval("October")%></td>
                                                    <td align="right">
                                                        <%#Eval("November")%></td>
                                                    <td align="right">
                                                        <%#Eval("December")%></td>        
                                                    <td align="right">
                                                        <%#Eval("Total")%></td>
                                                    <%--<td align="right">
                                                        <%#String.Format("{0:0.0}",Common.CastAsInt32(Eval("LeaveCount"))-Common.CastAsInt32(Eval("Total")))%></td>
                                                    --%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                     </table>
                            </div>
                            <div id="divLeaveDetails" runat="server">
                            <br />
                            <b >Leave Details : </b>
                            <br /><br />
                            <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                                <colgroup>
                                    <col style="width:200px;"/>
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:80px;" />
                                    <col />
                                    <col style="width:150px;" />
                                    <tr>
                                        <td align="left" style="font-weight:bold;">Leave Type </td>
                                        <td align="center" style="font-weight:bold;">Leave From </td>
                                        <td align="center" style="font-weight:bold;">Leave To</td>
                                        <td align="center" style="font-weight:bold;">Duration</td>
                                        <td align="left" style="font-weight:bold;">Reason</td>
                                        <td align="left" style="font-weight:bold;">Status</td>
                                    </tr>
                                </colgroup>
                            </table>      
                            <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                            <colgroup>
                                    <col style="width:200px;"/>
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:80px;" />
                                    <col />
                                    <col style="width:150px;" />
                            </colgroup>
                            <asp:Repeater ID="RptLeaveRequest" runat="server">
                                <ItemTemplate>
                                    <tr style="font-size:11px;">
                                        <td align="left">
                                            <%#Eval("LeaveTypeName")%></td>
                                        <td align="center">
                                            <%#Eval("LeaveFrom")%></td>
                                        <td align="center">
                                            <%#Eval("LeaveTo")%></td>
                                        <td align="center">
                                            <%#Eval("Duration")%></td>
                                        <td align="left">
                                            <%#Eval("Reason")%></td>
                                        <td align="left" >
                                            <%#Eval("Status")%></td>
                                    </tr>
                                </ItemTemplate>
                                
                            </asp:Repeater>
                            </table>
                            </div>   
    </div>
    <center>
    <br />
    <img src ="../../Images/Emtm/print.jpg" id='btnprnt' onclick="document.getElementById('btnprnt').style.display='none';window.print();"/>
    </center>
    </form>
</body>
</html>
