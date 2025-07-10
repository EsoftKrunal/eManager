<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_LeaveStatus.aspx.cs" Inherits="emtm_Emtm_TravelDocs" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Travel Document</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" >

        function PopUPLeaveStatusPrintWindow(CurrentYear, EmpId) {
            winref = window.open('../StaffAdmin/HR_LeaveStatusOnDateReport.aspx?CurrentYear=' + CurrentYear + '&EmpId=' + EmpId, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }
        function ShowLeaves(Mnth, LType,objtd) {
            document.getElementById('txtQuery').setAttribute('value',Mnth + "|" + LType);
            document.getElementById('hfdShowLeaves').click();

        }

        function show(objtds) {
        //class='<%# (Common.CastAsInt32(Eval("LeaveTypeId"))!=0)?"selectedrow":"row"%>'
            objtds.className = "selectedrow";
        }

        function PopUPPrintWindow(obj, Mode) {
            winref = window.open('../MyProfile/Profile_LeaveRequestReport.aspx?LeaveTypeId=' + obj + '&Mode=' + Mode, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }

        function PopUPLeaveRequestWindow(LeaveRequestId) {
            winref = window.open('HR_LeaveEdit.aspx?LeaveRequestId=' + LeaveRequestId, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0,height=350');
            return false;
        }

        function Refresh() {
            var btn = document.getElementById('btnRefresh');            
            btn.click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Always">
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
                            <td style="text-align:left;font-size :16px;">Leave Details :&nbsp;<asp:Label id="lbl_EmpName" Font-Names="Verdana" runat="server" Font-Size="15px" ></asp:Label></td>
                            <td style="text-align:right;font-size :16px;">
                            <asp:Button ID="btnback1" runat="server" CausesValidation="false" CssClass="btn" Text="Back" Width="50px" PostBackUrl="~/emtm/StaffAdmin/LeaveSearch.aspx"/>
                            <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" CausesValidation="false" style="display:none;" />
                                                        </td>
                        </tr>
                        </table>
                        </div>
                        <div class="dottedscrollbox">
                        </div>
                        
                         <div>
                            <table runat="server" visible="false" width="100%" cellpadding="5" cellspacing ="0" border="0">
                                <tr class="differRow">
                                   <td style="text-align :right">
                                       Last Year Balance-<asp:Label runat="server" ID="lblLastYear"></asp:Label> :&nbsp;</td>
                                   <td style="text-align :left">
                                       <b><asp:Label ID="lblBalLeaveLastYear" runat="server" MaxLength="100"></asp:Label></b>
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
                                       <b>
                                       <asp:Label ID="lblAnnualLeaveEntitlement" runat="server" MaxLength="100"></asp:Label>
                                       </b>
                                   </td>
                               </tr>
                                <tr class="differRow">
                                    <td style="text-align :right">
                                        Expired Leave(30th June) -<asp:Label ID="lblCurYear3" runat="server"></asp:Label>
                                        :&nbsp;</td>
                                    <td style="text-align :left">
                                        <b>
                                        <asp:Label ID="lblLeaveExpired" runat="server"></asp:Label>
                                        </b>
                                    </td>
                                    <td style="text-align :right">
                                        Balance Leave-<asp:Label ID="lblCurYear2" runat="server"></asp:Label>
                                        :&nbsp;</td>
                                    <td style="text-align :left">
                                        <b>
                                        <asp:Label ID="lblAccruedLeave" runat="server" MaxLength="100"></asp:Label>
                                        </b>
                                    </td>
                                    <td style="text-align :right">
                                        </td>
                                    <td style="text-align :left">
                                    </td>
                                </tr>
                        </table>
                           </div>
                           
                           <table id="tblview" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
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
                           <div style="padding:5px 5px 5px 5px;min-height:120px; border:solid 1px gray" >
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
                                                <td>Leave Assigned</td>
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
                               <div class="scrollbox" style="OVERFLOW-Y: hidden; OVERFLOW-X: hidden; WIDTH: 100%; text-align:center; border-bottom:none;">
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
                                                <td align="center" onclick="show(this),ShowLeaves(1,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("Jan")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(2,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("Feb")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(3,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("March")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(4,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("April")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(5,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("May")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(6,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("June")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(7,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("July")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(8,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("August")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(9,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("September")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(10,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("October")%></td>
                                                <td align="center"  onclick="show(this),ShowLeaves(11,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("November")%></td>
                                                <td align="center" onclick="show(this),ShowLeaves(12,<%#Eval("LeaveTypeId")%>,this)">
                                                    <%#Eval("December")%></td>        
                                                <td align="center" onclick="show(this),ShowLeaves(0,<%#Eval("LeaveTypeId")%>,this)">
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
                           <asp:UpdatePanel ID="UpdatePanel2"  runat="server" UpdateMode="Always">
                           <ContentTemplate>
                           <div style="padding:5px 5px 5px 5px;" >
                           <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup>
                                    <col style="width:25px;"/>
                                    <col style="width:25px;"/>
                                    <col style="width:200px;"/>
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:60px;" />
                                    <col />
                                    <col style="width:110px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class="blueheader">
                                        <td></td>
                                        <td></td>
                                        <td>Leave Type </td>
                                        <td>Leave From </td>
                                        <td>Leave To</td>
                                        <td>Duration</td>
                                        <td>Remarks</td>
                                        <td>Status</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                            </table> 
                            </div>     
                        <div id="divLeaveRequest" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 255px; text-align:center; border:solid 1px gray;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                    <col style="width:25px;"/>
                                    <col style="width:25px;"/>
                                    <col style="width:200px;"/>
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
                                            <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' 
                                                ImageUrl="~/Modules/HRD/Images/hourglass.gif" OnClick="btnPrint_Click" ToolTip="Print"/>
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' 
                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnEdit_Click" ToolTip="Edit"/>
                                        </td>
                                        <td align="left">
                                            <%#Eval("LeaveTypeName")%></td>
                                        <td align="left">
                                            <%#Eval("LeaveFrom")%></td>
                                        <td align="center">
                                            <%#Eval("LeaveTo")%></td>
                                        <td align="center">
                                            <%#Eval("Duration")%></td>
                                        <td align="left">
                                            <%#Eval("Reason")%></td>
                                        <td align="center" class='Status_<%#Eval("StatusCode")%>'>
                                            <%#Eval("Status")%></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </ItemTemplate>
                                
                            </asp:Repeater>
                            </table>
                        </div> 
                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                             <tr>
                                 <td align="right">
                                     <asp:Button ID="btnLeavePrint" runat="server" CausesValidation="false" 
                                         CssClass="btn" Text="Print" Width="70px" onclick="btnLeavePrint_Click"/>
                                     <asp:Button ID="btnBack" runat="server" CausesValidation="false"  PostBackUrl="~/emtm/StaffAdmin/LeaveSearch.aspx" 
                                         CssClass="btn" Text="Back" Width="70px"/>
                                 </td>
                             </tr>
                         </table> 
                            </div>
                           </ContentTemplate>
                           </asp:UpdatePanel>  
                       
                    </td>
                </tr>
         </table> 
         </ContentTemplate>
      </asp:UpdatePanel> 
    </div>
    </form>
</body>
</html>
