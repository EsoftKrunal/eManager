 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_LeaveStatus.aspx.cs" Inherits="emtm_Emtm_Profile_LeaveStatus" EnableEventValidation="false" %>
<%@ Register src="Profile_LeavesHeader.ascx" tagname="Profile_LeavesHeader" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Travel Document</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" >
        function OpenHoto(id) {
            window.open("OfficeAbsenceHoto_L.aspx?id=" + id + "&Mode=B", "expense", "");
        }
        function ShowLeaves(Mnth, LType)
        {
            document.getElementById('txtQuery').setAttribute('value',Mnth + "|" + LType);  
            document.getElementById('hfdShowLeaves').click();
        }

        function PopUPWindow(obj, OfficeId,DepartmentId,Mode) {
            winref = window.open('../MyProfile/PopupLeaveRequest.aspx?LeaveRequestId=' + obj, '', 'title=no,toolbars=no,scrollbars=yes,width=1100,height=625,left=150,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }

        function PopUPPrintWindow(obj,Mode) {
            winref = window.open('../MyProfile/Profile_LeaveRequestReport.aspx?LeaveTypeId=' + obj + '&Mode=' + Mode, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }

        function PopUPLeaveStatusPrintWindow(CurrentYear, EmpId) {
            winref = window.open('../StaffAdmin/HR_LeaveStatusOnDateReport.aspx?CurrentYear=' + CurrentYear + '&EmpId=' + EmpId, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div style="display:none">  
        <asp:Button ID="hfdShowLeaves" runat="server" OnClick="btnShowLeaves_Click" />
        <asp:TextBox ID="txtQuery" runat="server"></asp:TextBox> 
        </div>
         <table width="100%">
            <tr>
                <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                   <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                        <table width="100%">
                            <tr>
                                <td style="text-align:left;font-size :16px;">Leave History :&nbsp;<asp:Label id="lbl_EmpName" Font-Names="Verdana" runat="server" Font-Size="15px" ></asp:Label></td>
                                <td style="text-align:right;font-size :16px;">
                                <asp:Button ID="btnback1" runat="server" CausesValidation="false" 
                                             CssClass="btn" Text="Back" Width="50px" PostBackUrl="~/emtm/MyProfile/Profile_LeaveDetails.aspx"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                   <%-- <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                        Leave Management : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                    </div>--%>
                    <%-- <div class="dottedscrollbox">
                         <uc2:Emtm_Profile_LeavesHeader ID="Emtm_Profile_LeavesHeader1" runat="server" />
                    </div>--%>
                    <div>
                    <table width="100%" cellpadding="5" cellspacing ="0" border="0" runat="server" visible ="false">
                                <tr class="differRow">
                                   <td style="text-align :right">
                                       Balance-<asp:Label runat="server" ID="lblLastYear"></asp:Label> :&nbsp;</td>
                                   <td style="text-align :left">
                                       <b><asp:Label ID="lblBalLeaveLastYear" runat="server" MaxLength="100"></asp:Label></b>
                                   </td>
                                   <td style="text-align :right">
                                       Leave Expired(30th June)-<asp:Label ID="lblCurYear3" runat="server"></asp:Label>
                                       :&nbsp;</td>
                                   <td style="text-align :left">
                                       <asp:Label ID="lblLeaveExpired" runat="server"></asp:Label>
                                   </td>
                                   <td style="text-align :right">
                                       Annual Leave-<asp:Label ID="lblCurYear" runat="server"></asp:Label>
                                       :</td>
                                   <td style="text-align :left">
                                       <b>
                                       <asp:Label ID="lblAnnualLeaveEntitlement" runat="server" MaxLength="100"></asp:Label>
                                       </b>
                                   </td>
                               </tr>
                                <tr class="differRow">
                                    <td style="text-align :right">
                                        Consumed Leave-<asp:Label ID="lblCurYear1" runat="server"></asp:Label>
                                        :&nbsp;</td>
                                    <td style="text-align :left">
                                        <b>
                                        <asp:Label ID="lblLeavesTaken" runat="server" MaxLength="100"></asp:Label>
                                        </b>
                                    </td>
                                    <td style="text-align :right">
                                        Balance Leave-<asp:Label ID="lblCurYear2" runat="server"></asp:Label>
                                        :&nbsp;</td>
                                    <td style="text-align :left">
                                        <b>
                                        <asp:Label ID="lblLeaveBalance" runat="server" MaxLength="100"></asp:Label>
                                        </b>
                                    </td>
                                    <td style="text-align :right">
                                        Accrued Leave-<asp:Label ID="lblCurYear4" runat="server"></asp:Label>
                                        :</td>
                                    <td style="text-align :left">
                                        <b>
                                        <asp:Label ID="lblAccruedLeave" runat="server" MaxLength="100"></asp:Label>
                                        </b>
                                    </td>
                                </tr>
                        </table>
                    </div>
                     <table id="tblview" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0" style=" font-weight:bold " >
                           <colgroup>
                               <col align="left" style="text-align :left" width="120px">
                               <col />
                               <col align="left" style="text-align :left" width="120px">
                               <col />
                               <tr>
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
                                       &nbsp;Office :&nbsp;</td>
                                   <td>
                                        <asp:Label ID="lblOffice" runat="server"></asp:Label>  
                                   </td>
                                   <td style="text-align :right">
                                       Year :&nbsp;</td>
                                   <td style="text-align :left">
                                       <asp:DropDownList ID="ddlCurrentYear" runat="server"  AutoPostBack="true" 
                                           onselectedindexchanged="ddlCurrentYear_SelectedIndexChanged" Height="18px" 
                                           Width="60px">
                                       </asp:DropDownList>
                                   </td>
                               </tr>
                               </col>
                               </col>
                           </colgroup>
                        </table>
                    <div style="padding:5px 5px 5px 5px;min-height:120px;" >
                        <table id="tblMain" runat="server" width="100%">
                       <tr>
                            <td style="vertical-align:top;">
                            <div class="scrollbox" style="OVERFLOW-Y: hidden; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
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
                                        <col style="width:25px;" />
                                        <tr align="left" class="blueheader">
                                            <td>Leave Type</td>
                                            <td>Leave Entitlement</td>
                                            <td>Jan</td>
                                            <td>Feb</td>
                                            <td>Mar</td>
                                            <td>Apr</td>
                                            <td>May</td>
                                            <td>Jun</td>
                                            <td>Jul</td>
                                            <td>Aug</td>
                                            <td>Sep</td>
                                            <td>Oct</td>
                                            <td>Nov</td>
                                            <td>Dec</td>
                                            <td>Total</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </colgroup>
                           </table>  
                           </div>
                            <div class="scrollbox" style="OVERFLOW-Y: hidden; OVERFLOW-X: hidden; WIDTH: 100%; text-align:center;">         
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
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
                                        <col style="width:25px;" />
                                 </colgroup>
                                <asp:Repeater ID="rptLeaveDetails" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("LeaveTypeId"))==SelectedId)?"selectedrow":"row"%>'>
                                            <td align="left">
                                                <%#Eval("LeaveTypeName")%></td>
                                            <td align="center">
                                                <%#Eval("LeaveCount")%></td>
                                            <td align="center" onclick="ShowLeaves(1,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("Jan")%></td>
                                            <td align="center" onclick="ShowLeaves(2,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("Feb")%></td>
                                            <td align="center" onclick="ShowLeaves(3,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("March")%></td>
                                            <td align="center" onclick="ShowLeaves(4,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("April")%></td>
                                            <td align="center" onclick="ShowLeaves(5,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("May")%></td>
                                            <td align="center" onclick="ShowLeaves(6,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("June")%></td>
                                            <td align="center" onclick="ShowLeaves(7,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("July")%></td>
                                            <td align="center" onclick="ShowLeaves(8,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("August")%></td>
                                            <td align="center" onclick="ShowLeaves(9,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("September")%></td>
                                            <td align="center" onclick="ShowLeaves(10,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("October")%></td>
                                            <td align="center" onclick="ShowLeaves(11,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("November")%></td>
                                            <td align="center" onclick="ShowLeaves(12,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("December")%></td>        
                                            <td align="center" onclick="ShowLeaves(0,<%#Eval("LeaveTypeId")%>)">
                                                <%#Eval("Total")%></td>
                                             <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </table>
                                </div>
                            </td>
                       </tr>
                   </table> 
                    </div> 
                    
                    <div style="padding:5px 5px 5px 5px;" >
                    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                           <colgroup>
                                    <col style="width:25px;"/>
                                    <col style="width:25px;"/>
                                    <col style="width:25px;"/>
                                    <col style="width:200px;"/>
                                    <col style="width:60px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:60px;" />
                                    <col />
                                    <col style="width:110px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class="blueheader">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>Leave Type </td>
                                        <td>Hoto</td>
                                        <td>Leave From </td>
                                        <td>Leave To</td>
                                        <td>Duration</td>
                                        <td>Remark</td>
                                        <td>Status</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                        </table> 
                        </div>     
                        <div id="divLeaveRequest" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 240px; text-align:center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                                <col style="width:25px;"/>
                                <col style="width:25px;"/>
                                <col style="width:25px;"/>
                                <col style="width:200px;"/>
                                <col style="width:60px;" />
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col style="width:60px;" />
                                <col />
                                <col style="width:110px;" />
                                <col style="width:25px;" />
                        </colgroup>
                        <asp:Repeater ID="RptLeaveRequest" runat="server">
                            <ItemTemplate>
                                <tr class='<%# (Common.CastAsInt32(Eval("LeaveRequestId"))==SelectedId)?"selectedrow":"row"%>'>
                                    <td align="center">
                                        <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClick="btnLeaveCancel_Click" ToolTip="Cancel" Visible='<%# (Common.CastAsInt32(ddlCurrentYear.SelectedValue)>=DateTime.Today.Year) && (Eval("StatusCode").ToString()!="T") && (Eval("StatusCode").ToString()!="R") && (Eval("StatusCode").ToString()!="C")%>' OnClientClick="return confirm('Are you sure to cancel this leave ?');" />  
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnLeaveEdit_Click" ToolTip="Edit" Visible='<%# (Common.CastAsInt32(ddlCurrentYear.SelectedValue)>=DateTime.Today.Year) && (Eval("StatusCode").ToString()!="T") && (Eval("StatusCode").ToString()!="C")%>' />  
                                        <%--<asp:ImageButton ID="btnLeaveEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnLeaveEdit_Click" ToolTip="Edit" Visible='<%# (ddlCurrentYear.SelectedValue==DateTime.Today.Year.ToString()) && (Eval("StatusCode").ToString()!="T") && (Eval("StatusCode").ToString()!="A") && (Eval("StatusCode").ToString()!="R") && (Eval("StatusCode").ToString()!="C")%>' />  --%>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' 
                                            ImageUrl="~/Modules/HRD/Images/hourglass.gif" OnClick="btnPrint_Click" ToolTip="Print"/>
                                    </td>
                                    <td align="left">
                                        <%#Eval("LeaveTypeName")%></td>
                                    <td>
                                        <a href='#' onclick='<%# "OpenHoto(" + Eval("LeaveRequestId").ToString() + ");" %>' runat="server" Visible='<%#(Eval("StatusCode").ToString()=="T") || (Eval("StatusCode").ToString()=="A")%>'>HOTO</a>
                                    </td>
                                    <td align="left">
                                        <%#Eval("LeaveFrom")%></td>
                                    <td align="center">
                                        <%#Eval("LeaveTo")%></td>
                                    <td align="center">
                                        <%#Eval("Duration")%></td>    
                                    <td align="left">
                                        <%#Eval("Reason")%></td>
                                    <td align="left" class='Status_<%#Eval("StatusCode")%>'>
                                        <%#Eval("Status")%></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                            
                        </asp:Repeater>
                        </table>
                    </div>
                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                             <tr>
                                <td align="right" style="display:none;">
                                    <asp:Button ID="btnhdn" runat="server" onclick="btnhdn_Click" /> 
                                </td>
                                 <td align="right">
                                     <%--<asp:Button ID="btnaddnew" runat="server" CausesValidation="false" CssClass="btn" Text="Apply Leave" Width="100px" onclick="btnaddnew_Click1" />--%>
                                     <asp:Button ID="btnLeavePrint" runat="server" CausesValidation="false" 
                                         CssClass="btn" Text="Print" Width="70px" onclick="btnLeavePrint_Click"/>
                                     <asp:Button ID="btnBack" runat="server" CausesValidation="false" CssClass="btn" Text="Back" Width="50px" PostBackUrl="~/emtm/MyProfile/Emtm_Profile_LeaveDetails.aspx"/>
                                 </td>
                             </tr>
                         </table> 
                    </div>
                  </td>
            </tr>
         </table>
         </ContentTemplate>
      </asp:UpdatePanel> 
    </div>
    </form>
</body>
</html>
