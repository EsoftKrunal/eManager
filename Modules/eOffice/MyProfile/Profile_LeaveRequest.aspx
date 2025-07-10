<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_LeaveRequest.aspx.cs" Inherits="emtm_Emtm_Profile_LeaveRequest" EnableEventValidation="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Request</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/ecmascript">
        function PopUPWindow(obj,Mode) {
            winref = window.open('../MyProfile/PopUpLeaveApproval.aspx?LeaveTypeId=' + obj + '&Mode=' + Mode, '', 'title=no,toolbars=no,scrollbars=yes,width=760,height=300,left=250,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }
    </script>       
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
         <table width="100%">
                <tr>
                    
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                            Leave Management : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                           </div>
                        <div class="dottedscrollbox">
                        </div>
                        <div id="divTraveldocument" runat="server" style="padding:5px 5px 5px 5px;" >
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col style="width:200px;"/>
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col />
                                     <col style="width:100px;" />
                                    <col style="width:17px;" />
                                    <tr align="left" class="blueheader">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>Leave Type </td>
                                        <td>Leave From </td>
                                        <td>Leave To</td>
                                        <td>Half Day</td>
                                        <td>Reason</td>
                                        <td>Status</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                            </table>      
                            
                        <div id="divLeaveRequest" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 440px; text-align:center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col style="width:200px;"/>
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col />
                                     <col style="width:100px;" />
                                    <col style="width:17px;" />
                            </colgroup>
                            <asp:Repeater ID="RptLeaveRequest" runat="server">
                                <ItemTemplate>
                                    <tr class='<%# (Common.CastAsInt32(Eval("LeaveRequestId"))==SelectedId)?"selectedrow":"row"%>'>
                                        <td align="center">
                                            <asp:ImageButton ID="btnLeaveView" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnLeaveView_Click" ToolTip="View" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btnLeaveEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' Visible='<%#(auth.IsUpdate && (Eval("StatusCode").ToString()=="P"))%>' ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnLeaveEdit_Click" ToolTip="Edit" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btnLeaveDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' Visible='<%#(auth.IsDelete && (Eval("StatusCode").ToString()=="P"))%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  OnClick="btnLeaveDelete_Click" ToolTip="Delete" />
                                         </td>
                                        <td align="left">
                                            <%#Eval("LeaveTypeName")%></td>
                                        <td align="left">
                                            <%#Eval("LeaveFrom")%></td>
                                        <td align="center">
                                            <%#Eval("LeaveTo")%></td>
                                        <td align="left">
                                            <%#Eval("HalfDay")%></td>    
                                        <td align="left">
                                            <%#Eval("Reason")%></td>
                                        <td align="center" style="color:White;" class='<%#(Eval("Status").ToString()=="Approved")?"statuscolor1":((Eval("Status").ToString()=="Rejected")?"statuscolor2":"statuscolor3")%>'>
                                            <%#Eval("Status")%></td>
                                    </tr>
                                </ItemTemplate>
                                
                            </asp:Repeater>
                            <table>
                            </table>
                        </div> 
                        <br /> 
                           <table id="tblview" runat="server" width="100%" cellpadding="2" cellspacing ="0" border="0">
                                <colgroup>
                                   <col align="left" style="text-align :left" width="120px">
                                   <col />
                                   <col align="left" style="text-align :left" width="120px">
                                   <col />
                                   <tr>
                                       <td style="text-align :right">
                                           Leave From :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtLeaveFrom" runat="server" MaxLength="11" Width="90px" required='yes'></asp:TextBox>
                                           <asp:ImageButton ID="imgLeaveFrom" runat="server" 
                                               ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                            <asp:RequiredFieldValidator ID="rfvleavefrom" runat="server" 
                                                ControlToValidate="txtLeaveFrom" ErrorMessage="*"></asp:RequiredFieldValidator>    
                                       </td>
                                       <td style="text-align :right">
                                           Leave To :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtLeaveTo" runat="server" MaxLength="11" Width="90px" required='yes'></asp:TextBox>
                                           <asp:ImageButton ID="imgLeaveTo" runat="server" 
                                               ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                           <asp:RequiredFieldValidator ID="rfvleaveto" runat="server" 
                                            ControlToValidate="txtLeaveTo" ErrorMessage="*"></asp:RequiredFieldValidator>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align :right">
                                           <asp:CheckBox ID="chkHalfDay" runat="server" Text="HalfDay" AutoPostBack="True" 
                                               oncheckedchanged="chkHalfDay_CheckedChanged"/> :
                                       </td>
                                       <td style="text-align :left">
                                           <asp:RadioButton ID="rdoFirstHalf" runat="server" GroupName="LeaveHalfDay" Text="First Half"/>
                                           <asp:RadioButton ID="rdoSecondHalf" runat="server" GroupName="LeaveHalfDay" Text="Second Half"/>
                                       </td>
                                       <td style="text-align :right">
                                           Leave Type :</td>
                                       <td style="text-align :left">
                                           <asp:DropDownList ID="ddlLeaveType" runat="server" required='yes'>
                                           </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="rfvleavetype" runat="server" 
                                            ControlToValidate="ddlLeaveType" ErrorMessage="*"></asp:RequiredFieldValidator>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align :right" valign="top">
                                           Reason&nbsp; :</td>
                                       <td style="text-align :left" colspan="3">
                                           <asp:TextBox ID="txtReason" runat="server" Height="66px" TextMode="MultiLine" 
                                               Width="586px" MaxLength="500"></asp:TextBox>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td>
                                       </td>
                                       <td colspan="2">
                                       </td>
                                       <td>
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                               Format="dd-MMM-yyyy" PopupButtonID="imgLeaveFrom" PopupPosition="TopLeft" 
                                               TargetControlID="txtLeaveFrom">
                                           </ajaxToolkit:CalendarExtender>
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                                               Format="dd-MMM-yyyy" PopupButtonID="imgLeaveTo" PopupPosition="TopLeft" 
                                               TargetControlID="txtLeaveTo">
                                           </ajaxToolkit:CalendarExtender>
                                       </td>
                                   </tr>
                                   </col>
                                   </col>
                               </colgroup>
                            </table>
                            
                            
                            <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnaddnew" CssClass="btn" runat="server" Text="Apply Leave" 
                                        Width="80px" onclick="btnaddnew_Click" CausesValidation="false"></asp:Button>
                                    <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" 
                                        onclick="btnsave_Click"></asp:Button>
                                    <asp:Button ID="btncancel" CssClass="btn"  runat="server" Text="Cancel" 
                                        onclick="btncancel_Click" CausesValidation="false"></asp:Button>
                                </td>
                            </tr>
                            </table>
                        </div> 
                    </td>
                </tr>
         </table> 
           
    </div>
    </form>
</body>
</html>
