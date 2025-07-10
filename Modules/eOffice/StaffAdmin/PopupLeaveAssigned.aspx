<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupLeaveAssigned.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_PopupLeaveAssigned" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Assigned</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    
     <script language="javascript" type="text/javascript">
         function CloseWindow() {
             window.opener.document.getElementById("btnhdn").click();
             window.close();
         }

         function SHowLeavesDate() {
             document.getElementById('TabContainer1_TabPanel3_btnhdnLeaves').click();
       }

       function SHowRoundOff() {
           document.getElementById('TabContainer1_TabPanel3_btnhdnRoundOff').click();
       }

       function PopUPPrintWindow(obj, Mode) {
           winref = window.open('../MyProfile/Emtm_Profile_LeaveRequestReport.aspx?LeaveTypeId=' + obj + '&Mode=' + Mode, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
           return false;
       }
       function PopUPLeaveAssingedPrintWindow() {
           winref = window.open('../StaffAdmin/Emtm_HR_LeaveAssignedPrint.aspx', '','title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
           return false;
       }
       function showLeaveDays() {           
           document.getElementById("TabContainer1_TabPanel1_btnhdn1").click();                                    
       }
       function showLCLeaveDays() {
           document.getElementById("TabContainer1_TabPanel2_btnLChdn").click();

       }
       function fncInputNumericValuesOnly(evnt) {
           if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
               event.returnValue = false;
           }
       }
       
     </script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="padding:5px; background-color:#e8e6e6;font-weight:bold;">
        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="radtab" AutoPostBack="true" OnSelectedIndexChanged="radtab_OnSelectedIndexChanged">
            <asp:ListItem Text="Leave Request" Selected="True" Value="0"></asp:ListItem>
            <asp:ListItem Text="Office Absence" Value="1"></asp:ListItem>
            <asp:ListItem Text="Leave Credit" Value="2"></asp:ListItem>
        </asp:RadioButtonList>
            </div>
            <asp:Panel runat="server" ID="Panel1">
                <table cellpadding="0" cellspacing="0" width="100%">
                                                             <tr>
               <td valign="top" style="border:solid 1px #4371a5; height:80px;">
                    <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                        Add/Modify Leave Assigned
                    </div>
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" bordercolor='black' style="width:100%;border-collapse:collapse;">
                    <tr>
                    <td>
                    <span style="float:left;">
                        <asp:Label ID="lblEmpName" runat="server" Font-Bold="true" Font-Size="14px" Width="100%" ></asp:Label>   
                      </span>
                      <span style="float:right;">
                            Year : 
                            <asp:DropDownList ID="ddlyear" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_ddlyear" AutoPostBack="true" required="yes" Width="70px" >
                                
                            </asp:DropDownList>
                      </span>
                    </td>
                    </tr>
                    <%--<tr class="row">
                        <td style=" font-weight : bold; font-size : 13px;" > Balance Leave - <asp:Label runat="server" ID="lblLastyear"></asp:Label>  </td>
                        <td align="center" style="width:145px;">
                        <asp:TextBox ID="txtBalLeaveAssigned" runat="server" Width="40px"></asp:TextBox>
                        <asp:CompareValidator Operator="DataTypeCheck" runat="server" ID="leaveassigned" ControlToValidate="txtBalLeaveAssigned" Type="Integer" ErrorMessage="Invalid"></asp:CompareValidator>   
                        </td> 
                    </tr>--%>
                    </table>
                    <asp:UpdatePanel ID="updatepanel1" runat="server">
                    <ContentTemplate>
                         <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col />
                            <col style="width:150px;" />
                            <tr align="left" class="blueheader">
                                <td>Leave Type</td>
                                <td>Leave Assigned</td>
                            </tr>
                        </colgroup>
                    </table>   
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                                <col />
                                <col style="width:150px;" />
                         </colgroup>
                        <asp:Repeater ID="rptLeaveDetails" runat="server">
                            <ItemTemplate>
                                <tr class='<%# (Common.CastAsInt32(Eval("LeaveTypeId"))==SelectedId)?"selectedrow":"row"%>'>
                                    <td align="left">
                                        <%#Eval("LeaveTypeName")%> <asp:HiddenField ID="hdnLeavetypId" runat="server" Value='<%#Eval("LeaveTypeId")%>' /></td>
                                    <td align="center">
                                       <asp:TextBox ID="txtLeaveAssigned" runat="server" Text='<%#Eval("LeaveCount")%>' Width="40px" onblur="SHowRoundOff();"></asp:TextBox>
                                       <asp:CompareValidator Operator="DataTypeCheck" runat="server" ID="leaveassigned" ControlToValidate="txtLeaveAssigned" Type="Double" ErrorMessage="Invalid"></asp:CompareValidator>   
                                       </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                     </table>
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <tr>
                                <td align="right" style="display:none;">
                                    <asp:Button ID="btnhdnRoundOff" runat="server" onclick="btnhdnRoundOff_Click" 
                                        Width="46px" />
                                    <asp:Button ID="btnhdn" runat="server" onclick="btnhdn_Click" /> 
                                </td>
                                <td style="text-align :right">
                                        <asp:Button ID="btnSave" CssClass="btn"  runat="server" Text="Save" onclick="btnSave_Click"></asp:Button>
                                 </td>
                            </tr>
                     </table>  
                    </ContentTemplate>
                    </asp:UpdatePanel> 
                   
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                   <ContentTemplate>
                      <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <tr>
                                <td style="text-align :center">
                                    <div class="dottedscrollbox" style=" text-align :center; font-size :14px;padding :3px; font-weight: bold;">
                                        Leave Deducted by HR
                                    </div>
                                </td>
                            </tr>
                     </table>  
                     <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                               Format="dd-MMM-yyyy" PopupButtonID="imgLeaveFrom" PopupPosition="Left" 
                                               TargetControlID="txtLeaveFrom">
                                           </ajaxToolkit:CalendarExtender>
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                                               Format="dd-MMM-yyyy" PopupButtonID="imgLeaveTo" PopupPosition="Left" 
                                               TargetControlID="txtLeaveTo">
                                           </ajaxToolkit:CalendarExtender>
                      <table id="tblAddLeaverequest" runat="server" border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <tr>
                                <td style="text-align :right">Leave Type :</td>
                                <td> <asp:DropDownList ID="ddlLeaveType" runat="server" Width="180"></asp:DropDownList>
                                <asp:CompareValidator runat="server" id="CompareValidator1" ControlToValidate="ddlLeaveType" ValueToCompare="0" ErrorMessage="*" Operator="NotEqual" ValidationGroup="s2"></asp:CompareValidator> 
                                </td>
                                <td style="text-align :right">Leave From :</td>
                                <td>
                                    <asp:TextBox ID="txtLeaveFrom" runat="server" Width="80" MaxLength="12" onchange="SHowLeavesDate();"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ControlToValidate="txtLeaveFrom" ValidationGroup="s2" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="imgLeaveFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                </td>
                                <td style="text-align :right">Leave To :</td>
                                <td>
                                    <asp:TextBox ID="txtLeaveTo" runat="server" Width="80" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" ControlToValidate="txtLeaveTo" ValidationGroup="s2" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="imgLeaveTo" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
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
                                   <td></td>
                                   <td>
                                       &nbsp;</td>
                                   <td style="text-align :right">&nbsp;</td>
                                   <td align="left">
                                        <asp:Button ID="btnhdnLeaves" runat="server" onclick="btnhdnLeaves_Click" style="display:none;"  Width="46px"/> 
                                        <asp:Button ID="btnAdd" CssClass="btn"  runat="server" Text="Add Leave" Width="85" ValidationGroup="s2" UseSubmitBehavior="false" onclick="btnAddLeave_Click"></asp:Button>
                                  </td>
                            </tr>
                     </table>  
                     </ContentTemplate> 
                     <Triggers>
                     <asp:PostBackTrigger ControlID="btnAdd" />
                     </Triggers>
                     </asp:UpdatePanel>  
                      <div id="divTraveldocument" runat="server" style="padding:5px 5px 5px 5px;" >
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:25px;"/>
                                    <col style="width:30px;"/>
                                    <col />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:80px;"/>
                                    <col style="width:17px;"/>
                                    <tr align="left" class="blueheader">
                                        <td></td>
                                        <td></td>
                                        <td>Leave Type </td>
                                        <td>FromDate </td>
                                        <td>ToDate</td>
                                        <td>Duration</td>
                                        <td></td>
                                    </tr>
                                </colgroup>
                            </table>      
                            
                        <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 280px; text-align:center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                    <col style="width:25px;"/>
                                    <col style="width:30px;"/>
                                    <col />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:80px;"/>
                                    <col style="width:17px;"/>
                            </colgroup>
                            <asp:Repeater ID="RptLeaves" runat="server" >
                                <ItemTemplate>
                                    <tr class='<%# (Common.CastAsInt32(Eval("LeaveRequestId"))==SelectedId)?"selectedrow":"row"%>'>
                                         <td align="center">
                                            <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' 
                                            ImageUrl="~/Modules/HRD/Images/hourglass.gif" OnClick="btnPrint_Click" ToolTip="Print"/>
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btnLeaveDelete" runat="server" CausesValidation="false" 
                                                CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                OnClick="btnLeaveDelete_Click" ToolTip="Delete"  OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"/>
                                        </td>
                                        <td align="left">
                                            <%#Eval("LeaveTypeName")%></td>                
                                        <td align="left">
                                            <%#Eval("LeaveFrom")%></td>
                                        <td align="center">
                                            <%#Eval("LeaveTo")%></td>
                                        <td align="left">
                                            <%#Eval("Duration")%></td>    
                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>    
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </table>
                        </div> 
                        </div>
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <tr>
                                <td style="text-align :right">
                                        <asp:Button ID="btnLeavePrint" runat="server" CausesValidation="false" 
                                         CssClass="btn" Text="Print" Width="70px" onclick="btnLeavePrint_Click"/>
                                        <input type="button" onclick="window.close();" value="Close" class="btn" />
                                 </td>
                            </tr>
                     </table>                       
                     
                     
               </td>
                                                                
                                                             </tr>
                                                          
                                                          </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel2" Visible="false">
<table cellpadding="0" cellspacing="0" width="100%">
                                                             <tr>
               <td valign="top" style="border:solid 1px #4371a5; height:80px;">    
            <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                <tr>
                    <td style="background-color: #4371a5; height: 30px; font-size: 14px; font-weight: bold;
                        text-align: center; color: White">
                        &nbsp;Absence Record
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="padding: 5px 5px 5px 5px;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                border-collapse: collapse;">
                                <col style="width: 25px;" />
                                <col style="width: 25px;" />
                                <col style="width: 160px;" />
                                <col style="width: 80px;" />
                                <col style="width: 80px;" />
                                <col />
                                <col style="width: 17px;" />
                                <tr align="left" class="blueheader">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        Purpose
                                    </td>
                                    <td>
                                        From
                                    </td>
                                    <td>
                                        To
                                    </td>
                                    <td>
                                        Remarks
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 150px; text-align: center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                    border-collapse: collapse;">
                                    <col style="width: 25px;" />
                                    <col style="width: 25px;" />
                                    <col style="width: 160px;" />
                                    <col style="width: 80px;" />
                                    <col style="width: 80px;" />
                                    <col />
                                    <col style="width: 17px;" />
                                    <asp:Repeater ID="rptOffAbsence" runat="server">
                                        <ItemTemplate>
                                            <tr class='<%# (Common.CastAsInt32(Eval("LeaveRequestId"))==LeaveRequestId)?"selectedrow":"row"%>'>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# Eval("LeaveRequestId") %>'
                                                        OnClick="btnView_Click" ToolTip="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" />
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("LeaveRequestId") %>'
                                                        OnClick="btnDelete_Click" ToolTip="Delete" OnClientClick="return confirm('Are you sure to delete?');"
                                                        ImageUrl="~/Modules/HRD/Images/delete1.gif" />
                                                </td>
                                                <td align="left">
                                                    <%#Eval("Purpose")%>
                                                </td>
                                                <td align="left">
                                                    <%#Eval("LeaveFrom")%>
                                                </td>
                                                <td align="left">
                                                    <%#Eval("LeaveTo")%>
                                                </td>
                                                <td align="left">
                                                    <%#Eval("Reason")%>
                                                </td>
                                                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #4371a5; height: 30px; font-size: 14px; font-weight: bold;
                        text-align: center; color: White">
                        &nbsp;Enter Absence Details
                    </td>
                </tr>
            <tr>
            <td style=" padding :10px; vertical-align:top; ">
                 <fieldset >
                    <table id="tblview" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
                                <colgroup>
                                   <col align="left" style="text-align :left" width="150px">
                                   <col />
                                   <col align="left">
                                   <col align="left">
                                   <col />
                                </colgroup>
                                <tr>
                                   <td style="text-align :right">
                                       Location :</td>
                                   <td style="text-align :left">
                                       <asp:DropDownList ID="ddlLocation" runat="server" required='yes' ValidationGroup="planleave" Width="200px">
                                       <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="Local" Value="1"></asp:ListItem>
                                       <asp:ListItem Text="International" Value="2"></asp:ListItem>
                                       </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlLocation" ErrorMessage="*" InitialValue="0" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left;">
                                        
                                   </td>
                                   <td></td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">
                                       Purpose :</td>
                                   <td style="text-align :left">
                                       <asp:DropDownList ID="ddlPurpose" runat="server" required='yes' ValidationGroup="planleave" Width="200px"></asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="rfvleavepurpose" runat="server" ControlToValidate="ddlPurpose" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left;display:none;">
                                        <asp:Button ID="btnhdn1" runat="server" onclick="btnhdn1_Click" CausesValidation="false"/> 
                                   </td>
                                   <td></td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">
                                      Plan Period :</td>
                                   <td style="text-align :left">
                                       <asp:TextBox ID="txtLeaveFrom1" runat="server" MaxLength="11" Width="90px" required='yes' onchange="showLeaveDays();" ValidationGroup="planleave"></asp:TextBox>
                                       <asp:ImageButton ID="imgLeaveFrom1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       <asp:RequiredFieldValidator ID="rfvleavefrom" runat="server" ControlToValidate="txtLeaveFrom1" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>    
                                       to
                                       <asp:TextBox ID="txtLeaveTo1" runat="server" MaxLength="11" Width="90px" required='yes' onchange="showLeaveDays();" ValidationGroup="planleave" ></asp:TextBox>
                                       <asp:ImageButton ID="imgLeaveTo1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       <asp:RequiredFieldValidator ID="rfvleaveto" runat="server" ControlToValidate="txtLeaveTo1" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                                       <asp:Label ID="lblLeaveDays" Font-Bold="true" runat="server" Text=""></asp:Label>
                                   </td>
                                   <td style="text-align :left">
                                       <asp:CheckBox ID="chkAfterOfficeHr" runat="server" Text="After Office Hour" />
                                   </td>
                                   <td style="text-align :right">
                                       <asp:CheckBox ID="chkHalfDay1" runat="server" Text="HalfDay" AutoPostBack="True" 
                                           oncheckedchanged="chkHalfDay1_CheckedChanged"/> <span lang="en-us">&nbsp;:</span></td>
                                   <td style="text-align :left">
                                       <asp:RadioButton ID="rdoFirstHalf1" runat="server" Enabled="false" GroupName="LeaveHalfDay" Text="First Half"/>
                                       <asp:RadioButton ID="rdoSecondHalf1" runat="server" Enabled="false" GroupName="LeaveHalfDay" Text="Second Half"/>
                                   </td>
                               </tr>
                               <%--<tr>
                                  <td style="text-align :right">Total Office Absence :</td>
                                  <td style="text-align :left" colspan="4">
                                    <asp:Label ID="lblAbsentDays" Font-Bold="true" runat="server" Text=""></asp:Label> 
                                  </td>
                               </tr>
                               <tr>
                                 <td style="text-align :right">
                                       Estimated Return Date :</td>
                                   <td style="text-align :left">
                                       <asp:TextBox ID="txtEJDt" runat="server" MaxLength="11" Width="90px" required='yes' ValidationGroup="planleave"></asp:TextBox>
                                       <asp:ImageButton ID="ImgEJD" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEJDt" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>    
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left;display:none;">
                                   </td>
                                   <td></td>
                               
                               </tr>--%>
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
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="imgLeaveTo1" PopupPosition="TopLeft" 
                                           TargetControlID="txtLeaveTo1">
                                       </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="imgLeaveFrom1" PopupPosition="TopLeft" 
                                           TargetControlID="txtLeaveFrom1">
                                       </ajaxToolkit:CalendarExtender>
                        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="ImgEJD" PopupPosition="TopLeft" 
                                           TargetControlID="txtEJDt">
                                       </ajaxToolkit:CalendarExtender>--%>
                        <asp:Button ID="btnSave1" CssClass="btn" runat="server" Text="Save" onclick="btnsave1_Click" Width="100px" ValidationGroup="planleave"></asp:Button>
                        <asp:Button ID="btnNotify" CssClass="btn" runat="server" Text="Notify" onclick="btnNotify_Click" Width="100px" Visible="false" CausesValidation="false"></asp:Button>
                        <asp:Button ID="btnCancel" CssClass="btn" runat="server" Text="Cancel" onclick="btnCancel_Click" Width="100px" CausesValidation="false" ></asp:Button>
                        </td>
                    </tr>
                 </table>
                 </fieldset>
            </td>
            </tr> 
        </table> 
        </td>
        </tr>
        </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel3" Visible="false">
<table cellpadding="0" cellspacing="0" width="100%">
                                                             <tr>
               <td valign="top" style="border:solid 1px #4371a5; height:80px;">    
            <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                <tr>
                    <td style="background-color: #4371a5; height: 30px; font-size: 14px; font-weight: bold;
                        text-align: center; color: White">
                        &nbsp;Leave Credit
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="padding: 5px 5px 5px 5px;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                border-collapse: collapse;">
                                <col style="width: 25px;" />
                                <col style="width: 25px;" />                                
                                <col style="width: 80px;" />
                                <col style="width: 80px;" />
                                <col />
                                <col style="width: 17px;" />
                                <tr align="left" class="blueheader">
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                       Year
                                    </td>
                                    <td>
                                         No Of Days
                                    </td>
                                    <td>
                                        Remarks
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <div class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%;height: 150px; text-align: center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;
                                    border-collapse: collapse;">
                                    <col style="width: 25px;" />
                                    <col style="width: 25px;" />
                                    <col style="width: 80px;" />
                                    <col style="width: 80px;" />
                                    <col />
                                    <col style="width: 17px;" />
                                    <asp:Repeater ID="rptLeaveCredit" runat="server">
                                        <ItemTemplate>
                                            <tr class='<%# (Common.CastAsInt32(Eval("LeaveCreditId"))==LeaveCreditId)?"selectedrow":"row"%>'>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnLCEdit" runat="server" CommandArgument='<%# Eval("LeaveCreditId") %>'
                                                        OnClick="btnLCEdit_Click" ToolTip="Edit" ImageUrl="~/Modules/HRD/Images/edit.jpg" />
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnLCDelete" runat="server" CommandArgument='<%# Eval("LeaveCreditId") %>'
                                                        OnClick="btnLCDelete_Click" ToolTip="Delete" OnClientClick="return confirm('Are you sure to delete?');"
                                                        ImageUrl="~/Modules/HRD/Images/delete1.gif" />
                                                </td>
                                                <td align="left">
                                                    <%#Eval("Year")%>
                                                </td>
                                                <td align="left">
                                                    <%#Eval("Days")%>
                                                </td>
                                                <td align="left">
                                                    <%#Eval("Reason")%>
                                                </td>
                                                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #4371a5; height: 30px; font-size: 14px; font-weight: bold;
                        text-align: center; color: White">
                        &nbsp;Enter Leave Credit
                    </td>
                </tr>
            <tr>
            <td style=" padding :10px; vertical-align:top; ">
                 <fieldset >
                    <table id="Table1" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
                                <colgroup>
                                   <col align="left" style="text-align :left" width="150px">
                                   <col />
                                   <col align="left">
                                   <col align="left">
                                   <col />
                                </colgroup>
                                <tr>
                                   <td style="text-align :right">
                                       Year :</td>
                                   <td style="text-align :left">
                                       <asp:DropDownList ID="ddlLCYear" runat="server" ValidationGroup="leavecredit" Enabled="false"  Width="200px">                                       
                                       </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlLCYear" ErrorMessage="*" ValidationGroup="leavecredit"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left;">
                                        
                                   </td>
                                   <td></td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">
                                       Period :</td>
                                   <td style="text-align :left">
                                       <asp:TextBox ID="txtLCFrom" runat="server" MaxLength="11" Width="90px" required='yes' onchange="showLCLeaveDays();" ValidationGroup="lcleave"></asp:TextBox>
                                       <asp:ImageButton ID="imgLcFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLCFrom" ErrorMessage="*" ValidationGroup="lcleave"></asp:RequiredFieldValidator>    
                                       -
                                       <asp:TextBox ID="txtLCTo" runat="server" MaxLength="11" Width="90px" required='yes' onchange="showLCLeaveDays();" ValidationGroup="lcleave" ></asp:TextBox>
                                       <asp:ImageButton ID="imgLCTo" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLCTo" ErrorMessage="*" ValidationGroup="lcleave"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :left">
                                       <asp:Label ID="Label1" Font-Bold="true" runat="server" Text=""></asp:Label>
                                   </td>
                                   <td style="text-align :left">
                                       <asp:CheckBox ID="chkLCHalfDay" runat="server" Text="HalfDay" AutoPostBack="True" oncheckedchanged="chkLCHalfDay_CheckedChanged"/></td>
                                   <td style="text-align :left">
                                       <%--<asp:RadioButton ID="RadioButton1" runat="server" Enabled="false" GroupName="LeaveHalfDay" Text="First Half"/>
                                       <asp:RadioButton ID="RadioButton2" runat="server" Enabled="false" GroupName="LeaveHalfDay" Text="Second Half"/>--%>
                                       <asp:Button ID="btnLChdn" runat="server" onclick="btnLChdn_Click" style="display:none" CausesValidation="false"/>
                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="imgLcFrom" PopupPosition="TopLeft" 
                                           TargetControlID="txtLCFrom">
                                       </ajaxToolkit:CalendarExtender>
                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="imgLCTo" PopupPosition="TopLeft" 
                                           TargetControlID="txtLCTo">
                                       </ajaxToolkit:CalendarExtender> 
                                   </td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">
                                       No Of Days :</td>
                                   <td style="text-align :left">
                                       <asp:TextBox ID="txtNoOfDays" Enabled="false" Font-Bold="true"  onkeypress="fncInputNumericValuesOnly(event)" runat="server" MaxLength="4" Width="90px" required='yes' ValidationGroup="leavecredit"></asp:TextBox>                                      
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNoOfDays" ErrorMessage="*" ValidationGroup="leavecredit"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left;display:none;">
                                        <%--<asp:Button ID="Button1" runat="server" onclick="btnhdn1_Click" CausesValidation="false"/>--%> 
                                   </td>
                                   <td></td>
                               </tr>                           
                               
                               <tr>
                                   <td style="text-align :right" valign="top">
                                       Reason&nbsp; :</td>
                                   <td style="text-align :left" colspan="4">
                                       <asp:TextBox ID="txtLCReason" runat="server" Height="66px" TextMode="MultiLine" required='yes' Width="99%" MaxLength="500"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLCReason" ErrorMessage="*" ValidationGroup="leavecredit"></asp:RequiredFieldValidator>
                                   </td>
                               </tr>                           
                               </table>
                               <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                    <tr>
                        <td align="right" style="padding-right :10px;" >
                                                
                        <asp:Button ID="btnLCSave" CssClass="btn" runat="server" Text="Save" onclick="btnLCSave_Click" Width="100px" ValidationGroup="leavecredit"></asp:Button>
                        <%--<asp:Button ID="Button2" CssClass="btn" runat="server" Text="Notify" onclick="btnNotify_Click" Width="100px" Visible="false" CausesValidation="false"></asp:Button>--%>
                        <asp:Button ID="btnLCCancel" CssClass="btn" runat="server" Text="Cancel" onclick="btnLCCancel_Click" Width="100px" CausesValidation="false" ></asp:Button>
                        </td>
                    </tr>
                 </table>
                    
                 </fieldset>
            </td>
            </tr> 
        </table> 
        </td>
        </tr>
        </table>
            </asp:Panel>
            
              
     
    </div>
    </form>
</body>
</html>
