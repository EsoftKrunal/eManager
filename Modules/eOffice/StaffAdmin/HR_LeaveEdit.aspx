<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_LeaveEdit.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_HR_LeaveEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Management</title>
     <link href="../style.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
         function CloseWindow() {             
             window.close();
         }

         function showLeaveDays() {
             document.getElementById("btnhdn").click();
         }

         function ShowCurrentMonth(Mnth, obj) {
             document.getElementById('txtMonthId').setAttribute('value', Mnth);
             document.getElementById('hdnShowMonth').click();
         }
         function RefreshParent() {
             window.opener.Refresh();
         }         
     </script>
</head>
<body style=" padding :10px;" >
    <form id="form1" runat="server">
    <div style="border:solid 1px #4371a5" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
       
                      
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style=" background-color :#4371a5; height :30px; font-size :14px; font-weight: bold; text-align:center; color:White">
        &nbsp;Leave Request 
        </td>
        </tr>
        <tr>
        <td style="text-align:left; font-size:13px; color:Red; padding-left:10px;" >
            <asp:Literal runat="server" ID="litNotes"></asp:Literal>
            
        </td>
        </tr>
            <tr>
            <td style=" padding :10px; vertical-align:top;">
                 <fieldset >
                 <legend>Leave Planning </legend> 
                    <table id="tblview" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
                                <colgroup>
                                   <col align="left" style="text-align :left" width="100px">
                                   <col />
                                   <col align="left">
                                   <col align="left">
                                   <col />
                                </colgroup>
                               <tr>
                                   <td style="text-align :right">
                                       Leave Type :</td>
                                   <td style="text-align :left">
                                       <asp:DropDownList ID="ddlLeaveType" runat="server" Enabled="false" required='yes' ValidationGroup="planleave" Width="200px"></asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="rfvleavetype" runat="server" ControlToValidate="ddlLeaveType" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left;display:none;">
                                        <asp:Button ID="btnhdn" runat="server" onclick="btnhdn_Click" CausesValidation="false"/> 
                                   </td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">
                                       Leave Period :</td>
                                   <td style="text-align :left">
                                       <asp:TextBox ID="txtLeaveFrom" runat="server" MaxLength="11" Width="90px" required='yes' onchange="showLeaveDays();" ValidationGroup="planleave"></asp:TextBox>
                                       <asp:ImageButton ID="imgLeaveFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       <asp:RequiredFieldValidator ID="rfvleavefrom" runat="server" ControlToValidate="txtLeaveFrom" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>    
                                       -
                                       <asp:TextBox ID="txtLeaveTo" runat="server" MaxLength="11" Width="90px" required='yes' onchange="showLeaveDays();" ValidationGroup="planleave" ></asp:TextBox>
                                       <asp:ImageButton ID="imgLeaveTo" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       <asp:RequiredFieldValidator ID="rfvleaveto" runat="server" ControlToValidate="txtLeaveTo" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :left">
                                       <asp:Label ID="lblLeaveDays" Font-Bold="true" runat="server" Text=""></asp:Label>
                                   </td>
                                   <td style="text-align :right">
                                       <asp:CheckBox ID="chkHalfDay" runat="server" Text="HalfDay" AutoPostBack="True" 
                                           oncheckedchanged="chkHalfDay_CheckedChanged"/> <span lang="en-us">&nbsp;:</span></td>
                                   <td style="text-align :left">
                                       <asp:RadioButton ID="rdoFirstHalf" runat="server" GroupName="LeaveHalfDay" Text="First Half"/>
                                       <asp:RadioButton ID="rdoSecondHalf" runat="server" GroupName="LeaveHalfDay" Text="Second Half"/>
                                   </td>
                               </tr>
                               <tr>
                                  <td style="text-align :right">Total Office Absence :</td>
                                  <td style="text-align :left" colspan="4">
                                    <asp:Label ID="lblAbsentDays" Font-Bold="true" runat="server" Text=""></asp:Label> 
                                  </td>
                               </tr>
                               <tr>
                                   <td style="text-align :right" valign="top">
                                       Remark&nbsp; :</td>
                                   <td style="text-align :left" colspan="4">
                                       <asp:TextBox ID="txtReason" runat="server" Height="66px" TextMode="MultiLine" 
                                           Width="99%" MaxLength="500"></asp:TextBox>
                                   </td>
                               </tr>
                           
                               </table>
                    <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                    <tr>
                        <td align="right" style="padding-right :10px;" >
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="imgLeaveTo" PopupPosition="TopLeft" 
                                           TargetControlID="txtLeaveTo">
                                       </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="imgLeaveFrom" PopupPosition="TopLeft" 
                                           TargetControlID="txtLeaveFrom">
                                       </ajaxToolkit:CalendarExtender>
                        <asp:Button ID="btnsave" CssClass="btn" runat="server" Text="Save" onclick="btnsave_Click" Width="100px" ValidationGroup="planleave"></asp:Button>
                        <asp:Button ID="btnClose" CssClass="btn" runat="server" Text="Close" Width="100px" OnClientClick="window.close();" /></asp:Button>
                        </td>
                    </tr>
                 </table>
                 </fieldset>
            </td>
            </tr> 
        </table> 
        </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </div>
    </form>
</body>

</html>
