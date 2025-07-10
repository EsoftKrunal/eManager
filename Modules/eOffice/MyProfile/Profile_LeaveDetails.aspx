<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_LeaveDetails.aspx.cs" Inherits="emtm_MyProfile_Emtm_Profile_LeaveDetails" MasterPageFile="~/Modules/eOffice/MyProfile/MyProfile.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <style type="text/css">
    .style1
    {
        text-align :left; 
        font-size :13px;  
        font-family:Arial Unicode MS; 
        color :#222222; 
        padding :5px; 
        border-style:none;
        text-align :left; 
        width:600px;
    }
    .style2
    {
    	text-align :left; 
        font-size :13px;  
        font-family:Comic Sans MS; 
        color :Red; 
    }
    .gridheadings
    {
    	background-color :#C2C2C2;
    	color : Red ;
    	font-size :13px; 
    	border :dotted 1px Black;
    	padding :2px;
    }
    </style>
    <script type="text/javascript" >

         function PopUPLeaveRequestWindow(obj,OfficeId, DepartmentId, Mode) {
             winref = window.open('../MyProfile/PopupLeaveRequest.aspx?LeaveTypeId=' + obj + '&OfficeId=' + OfficeId + '&DepartmentId=' + DepartmentId + '&Mode=' + Mode, '', 'title=no,toolbars=no,scrollbars=yes,width=1100,height=625,left=30,top=100,addressbar=no,resizable=1,status=0');
             return false;
         }

         function PopUPOfficeAbsenceWindow(OfficeId, DepartmentId) {
             winref = window.open('../MyProfile/HR_OfficeAbsence.aspx?OfficeId=' + OfficeId + '&DepartmentId=' + DepartmentId, '', 'title=no,toolbars=no,scrollbars=yes,width=1100,height=510,left=30,top=100,addressbar=no,resizable=1,status=0');
             return false;
         }         
         
         function PopUPWindow(obj) {
             winref = window.open('../MyProfile/PopUpLeaveApproval.aspx?LeaveTypeId=' + obj, '', 'title=no,toolbars=no,scrollbars=yes,width=1100,height=740,left=30,top=60,addressbar=no,resizable=1,status=0');
             return false;
         }
     
         function PopUPPrintWindow(obj, Mode) {
             winref = window.open('../MyProfile/Profile_LeaveRequestReport.aspx?LeaveTypeId=' + obj + '&Mode=' + Mode, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
             return false;
         }

         function WhoIsOff() {
             document.getElementById("hdnWhoIsOff").click();
         }

         function WhoIsOff_Search() {
             document.getElementById("ddlOffice").focus();
         }
         function WhoIsOff_ViewAll() {
             document.getElementById("hdnWhoIsOffViewAll").click();
         }

         function ApplyLeave() {
             document.getElementById("HdnApplyLeave").click();
         }

         function PopUPLeaveStatusPrintWindow(EmpId,DateFrom,DateTo) {
             winref = window.open('../MyProfile/Profile_LeavesBetweenDateReport.aspx?EmpId=' + EmpId + '&DateFrom=' + DateFrom + '&DateTo=' + DateTo, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
             return false;
         }
         function CalPopUPWindow() {
             var loc = document.getElementById("ddlOffice");
             var dep = document.getElementById("ddlDept");
             var yearvalue = document.getElementById("txtLeaveFrom");

             var officeid = loc.options[loc.selectedIndex].value;
             var deptid = dep.options[dep.selectedIndex].value;
             var period = yearvalue.value.split("-");
             var month = period[1];
             var year = period[2];
             
             switch (month) {
                 case "Jan":
                     month = 1;
                     break;
                 case "Feb":
                     month = 2;
                     break;
                 case "Mar":
                     month = 3;
                     break;
                 case "Apr":
                     month = 4;
                     break;
                 case "May":
                     month = 5;
                     break;
                 case "Jun":
                     month = 6;
                     break;
                 case "Jul":
                     month = 7;
                     break;
                 case "Aug":
                     month = 8;
                     break;
                 case "Sep":
                     month = 9;
                     break;
                 case "Oct":
                     month = 10;
                     break;
                 case "Nov":
                     month = 11;
                     break;
                 case "Dec":
                     month = 12;
                     break;
             }

             winref = window.open('../LeaveCalender.aspx?office=' + officeid + '&dept=' + deptid + '&month=' + month + '&year=' + year, '', 'title=no,toolbars=no,scrollbars=no,width=1100,height=700,left=30,top=100,addressbar=no,resizable=1,status=0');
             return false;
         }
         function OnLeavePopUPWindow() {
             var loc = document.getElementById("ddlOffice");
             //var dep = document.getElementById("ddlDept");
             var LeaveFrom = document.getElementById("txtLeaveFrom").value;
             var LeaveTo = document.getElementById("txtLeaveTo").value;
             var officeid = loc.options[loc.selectedIndex].value;
             //var deptid = dep.options[dep.selectedIndex].value;

             winref = window.open('PopupStaffOnLeave.aspx?office=' + officeid + '&From=' + LeaveFrom + '&To=' + LeaveTo, '', 'title=no,toolbars=no,scrollbars=no,width=1100,height=700,left=30,top=100,addressbar=no,resizable=1,status=0');
             return false;
         }
         function OnBusinessTripPopUPWindow() {
             var loc = document.getElementById("ddlOffice");             
             var LeaveFrom = document.getElementById("txtLeaveFrom").value;
             var LeaveTo = document.getElementById("txtLeaveTo").value;
             var officeid = loc.options[loc.selectedIndex].value;

             winref = window.open('PopupStaffsOnBusinessTrip.aspx?office=' + officeid + '&From=' + LeaveFrom + '&To=' + LeaveTo, '', 'title=no,toolbars=no,scrollbars=no,width=1100,height=700,left=30,top=100,addressbar=no,resizable=1,status=0');
             return false;
         }

    </script>

    <div style="font-family:Arial;font-size:12px;">
   
     <table width="100%">
            <tr>
               
                <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                    <div style=" padding :3px; font-weight: bold;" class="text headerband">
                    <table width="100%">
                        <tr>
                            <td style="text-align:left;font-size :16px; width:200px;">Hi,&nbsp;<asp:Label id="lblEmpName" Font-Names="Verdana" runat="server" Font-Size="15px" ></asp:Label></td>
                            <td style="text-align :center"><asp:Label id="lblCurrentYear" Font-Names="Verdana" runat="server" Font-Size="15px" ></asp:Label></td>
                            <td style="text-align:right;font-size :16px; width:200px;">&nbsp;</td>
                        </tr>
                    </table>
                    </div>
                    <div class="style1" >
                        Your balance leave upto <asp:Label id="lblCurrentMonth1" runat="server"></asp:Label> is <asp:Label ID="lblLeaveBalance" runat="server" MaxLength="100" Font-Size="14px" Font-Bold="true"  ForeColor="Green"></asp:Label>
                    </div> 
                    <div style="width :800px" >
                         <asp:Literal ID="LiteralTotalLeaves" runat="server"></asp:Literal> 
                    </div>
                    <div id="DivLeaveHistory" runat="server" style="border-bottom :dotted 1px gray;width :620px; padding-bottom :5px;">
                        <span style="font-size :13px; padding-left :4px;color: #525252;">
                        Your last leave application from <asp:Label ID="lblLvsFrom" runat="server" MaxLength="100" Font-Size="12px" Font-Bold="true"></asp:Label> to <asp:Label ID="lblLvsTO" runat="server" MaxLength="100" Font-Size="12px" Font-Bold="true"></asp:Label> Current Status <asp:Label ID="lblLvsStatus" runat="server" MaxLength="100" Font-Size="12px" Font-Bold="true"></asp:Label>
                        </span>
                    </div>
                    <div style="text-align :left; font-size :14px; color :Black;">
                      <table width="100%" cellpadding="0" cellspacing ="0" border="0">
                            <tr>
                                <td width="70%" valign="top">
                                     <asp:Literal ID="LiteralLeaveView" runat="server"></asp:Literal> 
                                </td>
                                <td width="30%" style="padding-left :10px;" >
                                     <table width="100%" cellpadding="5" cellspacing ="0" border="0">
                                     <colgroup>
                                     <col style="width:25%"/>
                                     <col />
                                     </colgroup>
                                       <tr>
                                            <td style="text-align :left" valign="top">
                                                ► <a href="#" id="ApplyAbsence" onclick="PopUPOfficeAbsenceWindow('0');"><span style="color:#2A5D94; font-size:12px;">Biz. Travel</span></a>
                                                <asp:Button ID="HdnApplyAbsence" runat="server" style="display:none;" onclick="HdnApplyAbsence_Click" CausesValidation="false"/>           
                                                <br />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To report biz. travel. 
                                             </td>
                                        </tr>
                                       <tr>
                                            <td style="text-align :left" valign="top">
                                                ► <a href="#" id="ApplyLeave" onclick="PopUPLeaveRequestWindow('0');"><span style="color:#2A5D94; font-size:12px;">Apply for Leave</span></a>
                                                <asp:Button ID="HdnApplyLeave" runat="server" style="display:none;" onclick="HdnApplyLeave_Click" CausesValidation="false"/>           
                                                <br />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To apply for leave. 
                                             </td>
                                        </tr>
                                         <tr>
                                            <td style="text-align :left" valign="top">
                                                ► <a href="Emtm_Profile_LeaveStatus.aspx" id="LeaveStatus"><span style="color:#2A5D94; font-size:12px;">Leave History</span></a>
                                                <br />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To check leave history. 
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="text-align :left" valign="top">
                                                ► <a runat="server" id="ancHolyday" target="_blank"><span style="color:#2A5D94; font-size:12px;">Company Holiday</span></a>
                                                <br />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To check company holidays. 
                                            </td>
                                        </tr>
                                     </table>
                                </td>
                            </tr>     
                       </table>
                    </div>
                    <table width="100%" cellpadding="0" cellspacing="3" border="0">
                        <tr>
                            <td valign="top" width="50%">
                                <div class="gridheadings">Leave Applications for Approvals</div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="divApproval" runat="server" style="text-align:left">
                                    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                         <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%; height:27px;  border-collapse:collapse; height:26px;">
                                            <colgroup>
                                                <col />
                                                <col style="width:70px;"/>
                                                <col style="width:50px;" />
                                                <col style="width:25px;"/>
                                                <tr align="left" class= "headerstylegrid">
                                                    <td>Name</td>
                                                    <td>Duration</td>
                                                    <td>View</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>         
                                            <div id="divLeavesApproval" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 175px; text-align:center;">
                                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                    <col />
                                                    <col style="width:70px;"/>
                                                    <col style="width:50px;" />
                                                    <col style="width:25px;"/>
                                                </colgroup>
                                                <asp:Repeater ID="RptLeaveApproval" runat="server">
                                                    <ItemTemplate>
                                                        <tr class='<%# (Common.CastAsInt32(Eval("LeaveRequestId"))==SelectedId)?"selectedrow":"row"%>'>
                                                            <td align="left">
                                                                <%#Eval("Name")%></td>
                                                            <td align="center">
                                                                <%#Eval("Duration")%></td>    
                                                            <td align="center">
                                                                <asp:ImageButton ID="btnLeaveApprove" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId").ToString() + "|" + Eval("Type").ToString() + "|" + Eval("EmpId").ToString() %>' ImageUrl="~/Modules/HRD/Images/hourglass.gif" OnClick="btnLeaveApprove_Click" ToolTip="Approve" Visible='<%#(Eval("Type").ToString() == "L" ? (Eval("StatusCode").ToString()=="V") || (Eval("StatusCode").ToString()=="W") : (Eval("StatusCode").ToString()=="R"))%>'/>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                             </table>
                                        </div>
                                        <table border="0" cellpadding="1" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <tr class="Footerheader">
                                                <td>
                                                      ( <asp:Label id="lblAppCount" runat="server" ForeColor="Red"></asp:Label> )
                                                      Approvals are pending.
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate> 
                                </asp:UpdatePanel> 
                            </td>
                          <%--  <td valign="top" width="50%">
                                <div class="gridheadings">Who is off ?</div>
                                <asp:UpdatePanel ID="UpdatePanel2"  runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <div id="div1" runat="server">
                                        <table border="0" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col />
                                                <col style="width:90px;" />
                                                <col style="width:40px;" />
                                                <col style="width:17px;"/>
                                                
                                                <tr align="left" class="blueheader">
                                                    <td>
                                                    <table width="100%" cellpadding="3" cellspacing ="0" border="0">
                                                    <tr>
                                                        <td style="text-align :left">
                                                            <asp:DropDownList ID="ddlOffice" runat="server" Width="90px" AutoPostBack="true" onselectedindexchanged="ddlOffice_SelectedIndexChanged" onchange="WhoIsOff_Search();"></asp:DropDownList>
                                                        </td>
                                                        <td style="text-align :left">
                                                            <asp:DropDownList ID="ddlDept" runat="server" Width="120px" AutoPostBack="true" onselectedindexchanged="UpdateOffGrid"></asp:DropDownList>
                                                        </td>
                                                        <td style="text-align :left">
                                                            <asp:TextBox ID="txtLeaveFrom" runat="server" MaxLength="11" Width="78px" AutoPostBack="true" OnTextChanged="UpdateOffGrid"></asp:TextBox>
                                                            <asp:ImageButton ID="imgLeaveFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                                        </td>
                                                        <td style="text-align :left">
                                                             <asp:TextBox ID="txtLeaveTo" runat="server" MaxLength="11" Width="78px" AutoPostBack="true" OnTextChanged="UpdateOffGrid"></asp:TextBox>
                                                             <asp:ImageButton ID="imgLeaveTo" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                                        </td>
                                                        <td>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgLeaveFrom" PopupPosition="TopLeft" TargetControlID="txtLeaveFrom"></ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgLeaveTo" PopupPosition="TopLeft" TargetControlID="txtLeaveTo"></ajaxToolkit:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                    </td>
                                                </tr>
                                            </colgroup>
                                        </table>          
                                        <div id="div2" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 175px; text-align:center;">
                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col />
                                                <col style="width:90px;" />
                                                <col style="width:40px;"/>
                                                <col style="width:17px;"/>
                                            </colgroup>
                                            <asp:Repeater ID="rptWhoIsOff" runat="server">
                                                <ItemTemplate>
                                                    <tr class='row'>
                                                        <td align="left">
                                                            <%#Eval("Name")%></td>
                                                        <td align="left">
                                                            <%#Eval("DeptName")%></td>    
                                                        <td align="center">
                                                            <asp:ImageButton ID="btnWhoisOffPrint" runat="server" CausesValidation="false" CommandArgument='<%# Eval("EmpId") %>' ImageUrl="~/Modules/HRD/Images/hourglass.gif" OnClick="btnWhoisOffPrint_Click" ToolTip="Print"/>
                                                        </td>   
                                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>   
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                         </table>
                                        </div>
                                        <table border="0" cellpadding="1" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <tr class="Footerheader">
                                                <td>
                                                      ( <asp:Label id="lblWhoIsOffCount" runat="server" ForeColor="Red"></asp:Label> )
                                                      Persons are Off for selected period.
                                                </td>
                                            </tr>
                                        </table>  
                                </div>
                                </ContentTemplate> 
                                </asp:UpdatePanel> 
                            </td>--%>
                          <td valign="top" width="50%">
                                <div class="gridheadings">Office Absence</div>
                                <asp:UpdatePanel ID="UpdatePanel2"  runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                <div id="div1" runat="server">
                                        <table border="0" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col />
                                                <col style="width:90px;" />
                                                <col style="width:40px;" />
                                                <col style="width:17px;"/>
                                                
                                                <tr align="left" class= "headerstylegrid">
                                                    <td>
                                                    <table width="100%" cellpadding="3" cellspacing ="0" border="0">
                                                    <tr>
                                                        <td style="text-align :left">
                                                            <asp:DropDownList ID="ddlOffice" runat="server" Width="90px" AutoPostBack="true" onselectedindexchanged="ddlOffice_SelectedIndexChanged" onchange="WhoIsOff_Search();"></asp:DropDownList>
                                                        </td>
                                                        <td style="text-align :left">
                                                            <asp:DropDownList ID="ddlDept" runat="server" Width="120px" AutoPostBack="true" onselectedindexchanged="UpdateOffGrid"></asp:DropDownList>
                                                        </td>
                                                        <td style="text-align :left">
                                                            <asp:TextBox ID="txtLeaveFrom" runat="server" MaxLength="11" Width="78px" AutoPostBack="true" OnTextChanged="UpdateOffGrid"></asp:TextBox>
                                                            <asp:ImageButton ID="imgLeaveFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                                        </td>
                                                        <td style="text-align :left">
                                                             <asp:TextBox ID="txtLeaveTo" runat="server" MaxLength="11" Width="78px" AutoPostBack="true" OnTextChanged="UpdateOffGrid"></asp:TextBox>
                                                             <asp:ImageButton ID="imgLeaveTo" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                                        </td>
                                                        <td>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgLeaveFrom" PopupPosition="TopLeft" TargetControlID="txtLeaveFrom"></ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgLeaveTo" PopupPosition="TopLeft" TargetControlID="txtLeaveTo"></ajaxToolkit:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                    </td>
                                                </tr>
                                            </colgroup>
                                        </table>          
                                        <div id="div2" runat="server" class="scrollbox" style="OVERFLOW-Y: hidden; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 175px; text-align:center;">
                                        <table border="0" cellpadding="4" cellspacing="4" style="width:100%;">
                                           <tr>
                                             <td style="width:40%; text-align:left;">Staff on Leave:</td>
                                             <td style="text-align:left;">
                                                 <asp:LinkButton ID="lbOnLeave" Text="0" runat="server" OnClientClick="OnLeavePopUPWindow();"  ></asp:LinkButton>
                                             </td>
                                           </tr>
                                           <tr>
                                             <td style="text-align:left;">Staff on Business Travel:</td>
                                             <td style="text-align:left;">
                                                <asp:LinkButton ID="lbOnBT" Text="0" runat="server" OnClientClick="OnBusinessTripPopUPWindow();" ></asp:LinkButton>
                                             </td>
                                           </tr> 
                                           <tr>
                                             <td style="text-align:left;">Staff in the Office:</td>
                                             <td style="text-align:left;">
                                                <asp:LinkButton ID="lbInOffice" Text="0" runat="server"></asp:LinkButton>
                                             </td>
                                           </tr> 
                                           <tr>
                                             <td colspan="2" style="text-align:left;height:70px; vertical-align:bottom; font-style:italic; font-size:16px;"><asp:LinkButton ID="lbViewCalander" Text="View Absence Calender" OnClientClick="CalPopUPWindow();" runat="server"></asp:LinkButton></td>
                                           </tr> 
                                         </table>
                                        </div>
                                        <table border="0" cellpadding="1" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <tr class="Footerheader">
                                                <td>
                                                      ( <asp:Label id="lblWhoIsOffCount" runat="server" ForeColor="Red"></asp:Label> )
                                                      Persons are Off for selected period.
                                                </td>
                                            </tr>
                                        </table>  
                                </div>
                                </ContentTemplate> 
                                </asp:UpdatePanel> 
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnhdnApproval" runat="server" style="display:none;" onclick="btnhdnApproval_Click"/>    
                </td>
            </tr>
       </table>
    </div>
   </asp:Content>
