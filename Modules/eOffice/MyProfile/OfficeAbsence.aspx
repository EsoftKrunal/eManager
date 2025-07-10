<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsence.aspx.cs" Inherits="emtm_OfficeAbsence_Emtm_OfficeAbsence" Async="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Office Absence</title>
    <script type="text/javascript" src="../../JS/jquery.min.js"></script>
    <link href="../style_new.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
         function CloseWindow() {
             //window.opener.document.getElementById("btnhdn").click();
             window.close();
         }

         function showLeaveDays() {
             document.getElementById("btnhdn").click();
         }

         function ShowCurrentMonth(Mnth, obj) {
             document.getElementById('txtMonthId').setAttribute('value', Mnth);
             document.getElementById('hdnShowMonth').click();
         }
         function refreshparent() {
             //window.opener.refresh();
         }
         function openexpensewindow(id,Mode) {
            window.open("Emtm_OfficeExpense.aspx?id=" + id + "&Mode=" + Mode, "expense", "");
         }
         function OpenHotoWindow(id, Mode) {
            window.open("Emtm_OfficeAbsenceHoto.aspx?id=" + id + "&Mode=" + Mode, "expense", "");
         }
         function OpenBriefWindow(id, Mode) {
            window.open("Emtm_OfficeAbsenceBriefing.aspx?id=" + id + "&Mode=" + Mode, "Briefing", "");
         }
         function openreport(id) {
             window.open('../../Reporting/OfficeAbsenceExpense.aspx?id=' + id, 'asdf', '');
         }
         function HideFrame() {
             $("#btnHideFrame").click();
         }
     </script>
     <style type="text/css">
         .stage
         {
             font-size:17px;
             display:block;
             background-color:#cccccc;
             color:White;
             padding:6px;
             text-align:left;
             overflow:auto;
         }
          .stage span 
          { font-size:17px;
              }
         .box
         {
             border:solid 1px #cccccc;
             padding:5px;
             text-align:left;
         }
         .fleft
         {
             float:left;
             display:inline;
         }
         .fright
         {
             float:left;
             display:inline;
         }
         .tab
         {
             background-color:#dddddd;
             padding:5px;
             color:#222222;
         }
         .tabmenu
         {
             cursor:pointer;
         }
         .tabmenu:hover
         {
             background-color:#dddddd;
         }
         .tabpan
         {
             display:none;
         }
         .active
         {
             background-color:#4DB8FF;
             color:white;
         }
         .tablehead td
         {
             font-weight:bold; 
             color:Red;
             background-color:Silver;
         }
         ul
         {
             margin-left:15px;
         }
         li
         {
             list-style-type:none; 
             
         }
         span
         {
             color:Red;
         }
     </style>
</head>
<body >
    <form id="form1" runat="server">
    <div >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
        <div style="height:93px ; position:fixed;top:0px; width:100%; background-color:White;">
        <asp:Button runat="server" ID="btnHideFrame" OnClick="btnHideFrame_Click" style="display:none;"/>
        
         <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Biz. Travel</div>
         <div style='padding:3px;text-align:center; font-size:17px; overflow:auto;'>
             <table border="0" cellpadding="2" cellspacing="0" style="width: 100%;border-collapse: collapse;">
                 <tr>
                      <td style="text-align:left;"><asp:Button ID="btnAdd" runat="server" class="btn" Text="+ Add New" Width="100px" OnClick="btnAdd_Click"/></td>
                      <td style="text-align:left;">Year :  <asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="year_Changed"></asp:DropDownList></td>
                      <td></td>
                 </tr>
             </table>
         </div>
         <table border="1" cellpadding="5" cellspacing="0" rules="all" bordercolor="white" style="width: 100%;border-collapse: collapse;background-color:#5CB8E6; color:White;">
                <colgroup>
                    <col style="width: 25px;" />
                    <col style="width: 25px;" />
                    <col style="width: 25px;" />
                    <col style="width: 160px;" />
                    <col style="width: 80px;" />
                    <col style="width: 80px;" />
                   <%-- <col style="width: 80px;" />
                    <col style="width: 100px;" />
                    <col style="width: 80px;" />--%>
                    <col />
                    <%--<col style="width: 80px;" />--%>
                    <col style="width: 25px;" />
                    </colgroup>
                    <tr align="left">
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>Purpose</td>
                        <td>From</td>
                        <td>To</td>
                        <%--<td>Hoto</td>
                        <td>Brief/De-Brief</td>
                        <td>Expense</td>--%>
                        <td>Remarks</td>
                        <%--<td>Status</td>--%>
                        <td>&nbsp;</td>
                    </tr>
            </table>
        </div>
        <div style="padding-top:110px;">
        <table border="1" cellpadding="5" cellspacing="0" rules="all" bordercolor="#eeeeee" style="width: 100%;border-collapse: collapse; height:26px; ">
            <colgroup>
                <col style="width: 25px;" />
                <col style="width: 25px;" />
                <col style="width: 25px;" />
                <col style="width: 160px;" />
                <col style="width: 80px;" />
                <col style="width: 80px;" />
                <%--<col style="width: 80px;" />
                <col style="width: 100px;" />
                <col style="width: 80px;" />--%>
                <col />
                <%--<col style="width: 80px;" />--%>
                <col style="width: 25px;" />
            </colgroup>
            <asp:Repeater ID="rptOffAbsence" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center" valign="middle">
                            <asp:ImageButton ID="btnView" runat="server" CausesValidation="false" 
                                CommandArgument='<%# Eval("LeaveRequestId") %>' 
                                ImageUrl="~/Modules/HRD/Images/gtk-execute.png" OnClick="btnView_Click" ToolTip="View" Visible='<%#Eval("Status").ToString() == "Approved"%>' />
                        </td>
                        <td align="center" valign="middle">
                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/icon_pencil.gif" OnClick="btnEdit_Click" ToolTip="Edit" Visible='<%#(Convert.IsDBNull(Eval("PayRequestedOn")))%>' />
                        </td>
                        <td align="center" valign="middle">
                            <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/delete1.gif" OnClick="btnDelete_Click" ToolTip="Delete" OnClientClick="return window.confirm('Are you sure to Delete ?');" Visible='<%#Convert.IsDBNull(Eval("PayRequestedOn"))%>' />
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
                        <%--<td>
                            <asp:Button ID="btnHoto" runat="server" 
                                CommandArgument='<%# Eval("LeaveRequestId") %>' CssClass="btn" 
                                OnClick="btnHoto_Click" Text=" HOTO " ToolTip="Handover / TakerOver" 
                                Width="70px" />
                        </td>
                        <td>
                            <asp:Button ID="btnBD" runat="server" 
                                CommandArgument='<%# Eval("LeaveRequestId") %>' CssClass="btn" 
                                OnClick="btnBD_Click" Text=" Brief/De-Brief " ToolTip="Briefing / De-Briefing" 
                                Width="100px" />
                        </td>
                        <td>
                            <asp:Button ID="btnExpense" runat="server" 
                                CommandArgument='<%# Eval("LeaveRequestId") %>' CssClass="btn" 
                                OnClick="btnExpense_Click" Text="Expense" ToolTip="Create Expense Voucher" 
                                Width="70px" />
                        </td>--%>
                        <td align="left">
                            <%#Eval("Reason")%>
                        </td>
                        <%-- <td align="left">
                            <%#Eval("Status")%>
                        </td>--%>
                        <td>
                            &nbsp;</td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        </div>
  
  <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="pnlContent" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:1000px; height:410px; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:150px;  ;opacity:1;filter:alpha(opacity=100)">
        <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:14px;'>Add / Edit Biz. Travel</div>
        <div style="padding:10px;">
            <table id="tblview" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
                                <colgroup>
                                   <col align="left" style="text-align :left" width="100px" />
                                   <col />
                                   <col align="left" />
                                   <col align="left" />
                                   <col />
                                </colgroup>
                                <tr>
                                   <td style="text-align :right; width:130px;">
                                       Location :</td>
                                   <td style="text-align :left">
                                       <asp:DropDownList ID="ddlLocation" runat="server" required='yes' ValidationGroup="planleave" Width="200px">
                                       <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="Local" Value="1"></asp:ListItem>
                                       <asp:ListItem Text="International" Value="2"></asp:ListItem>
                                       </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLocation" ErrorMessage="*" InitialValue="0" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left;">
                                        &nbsp;
                                   </td>
                                   <td></td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">Purpose :</td>
                                   <td style="text-align :left">
                                       <asp:DropDownList ID="ddlLeaveType" AutoPostBack="true" OnSelectedIndexChanged="Purpose_SelectedIndexChanged" runat="server" required='yes' ValidationGroup="planleave" Width="200px"></asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="rfvleavetype" runat="server" ControlToValidate="ddlLeaveType" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left;display:none;">
                                        <asp:Button ID="btnhdn" runat="server" onclick="btnhdn_Click" CausesValidation="false"/> 
                                   </td>
                                   <td>&nbsp;</td>
                               </tr>
                               <tr id="trVessel" runat="server" visible="false">
                                   <td style="text-align :right">Vessel :</td>
                                   <td style="text-align :left">
                                       <asp:DropDownList ID="ddlVessel" runat="server" required='yes' ValidationGroup="planleave" Width="200px"></asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlVessel" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td>&nbsp;</td>
                               </tr>
                               <tr id="trSelInsp" runat="server" visible="false">
                                   <td style="text-align :right">Planned Inspections :<asp:HiddenField ID="hfInspIds" runat ="server" /></td>
                                   <td style="text-align :left" colspan="5">
                                       <asp:Label ID="lblInspections" runat="server" ></asp:Label>&nbsp;
                                       <asp:LinkButton ID="lbSelectInsp" Text="Select Inspections" OnClick="lbSelectInsp_Click" runat="server" ></asp:LinkButton>
                                   </td>
                               </tr>

                               <tr>
                                   <td style="text-align :right">Plan Period :</td>
                                   <td style="text-align :left">
                                       <asp:TextBox ID="txtLeaveFrom" runat="server" MaxLength="11" Width="90px" required='yes' onchange="showLeaveDays();" ValidationGroup="planleave"></asp:TextBox>
                                       <asp:ImageButton ID="imgLeaveFrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       <asp:RequiredFieldValidator ID="rfvleavefrom" runat="server" ControlToValidate="txtLeaveFrom" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>    
                                       to
                                       <asp:TextBox ID="txtLeaveTo" runat="server" MaxLength="11" Width="90px" required='yes' onchange="showLeaveDays();" ValidationGroup="planleave" ></asp:TextBox>
                                       <asp:ImageButton ID="imgLeaveTo" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       <asp:RequiredFieldValidator ID="rfvleaveto" runat="server" ControlToValidate="txtLeaveTo" ErrorMessage="*" ValidationGroup="planleave"></asp:RequiredFieldValidator>
                                       <asp:Label ID="lblLeaveDays" Font-Bold="true" runat="server" Text=""></asp:Label>
                                   </td>
                                   <td style="text-align :left">
                                      <asp:CheckBox ID="chkAfterOfficeHr" runat="server" Text="After Office Hour" />
                                   </td>
                                   <td style="text-align :right">
                                       <asp:CheckBox ID="chkHalfDay" runat="server" Text="HalfDay" AutoPostBack="True" 
                                           oncheckedchanged="chkHalfDay_CheckedChanged"/> <span lang="en-us">&nbsp;:</span></td>
                                   <td style="text-align :left">
                                       <asp:RadioButton ID="rdoFirstHalf" runat="server" Enabled="false" GroupName="LeaveHalfDay" Text="First Half"/>
                                       <asp:RadioButton ID="rdoSecondHalf" runat="server" Enabled="false" GroupName="LeaveHalfDay" Text="Second Half"/>
                                   </td>
                               </tr>
                         
                               <tr>
                                   <td style="text-align :right" valign="top">
                                       Remark&nbsp; :</td>
                                   <td style="text-align :left" colspan="4">
                                       <asp:TextBox ID="txtReason" runat="server" Height="120px" TextMode="MultiLine" 
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
                                    <asp:Button ID="btnsave" CssClass="btn" runat="server" Text="Save" onclick="btnsave_Click" OnClientClick="this.value='Loading..';" Width="100px" ValidationGroup="planleave"></asp:Button>
                                    <asp:Button ID="btnNotify" CssClass="btn" runat="server" Text="Send for Approval" OnClientClick="this.value='Loading..';" onclick="btnNotify_Click" Width="150px" Visible="false" CausesValidation="false"></asp:Button>
                                    <asp:Button ID="btnCancel" CssClass="btn" runat="server" Text="Cancel" onclick="btnCancel_Click" Width="100px" CausesValidation="false" ></asp:Button>
                                    </td>
                                </tr>
                             </table>

        </div>
     </div> 
    </center>
    </div>
       
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dv_ViewBT" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
        <div style="position :relative; width:80%; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:70px;  ;opacity:1;filter:alpha(opacity=100)">
            <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Biz. Travel Details</div>
            <table id="Table1" runat="server" width="100%" cellpadding="5" cellspacing ="0" border="0">
                <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblLocation" Font-Size="Large" ForeColor="Green"></asp:Label> - 
                    <asp:Label runat="server" ID="lblPurpose" Font-Size="Large" ForeColor="orange"></asp:Label>
                 </td>
                 <td style="text-align:left"><asp:Label runat="server" ID="lblPeriod" Font-Size="Large" ForeColor="gray"></asp:Label> </td>
                 <td style="text-align:right">
                     <asp:Label runat="server" ID="lblHalfDay" Font-Size="Large" ForeColor="purple"></asp:Label>
                 </td>
                </tr>
                 <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblVesselName" Font-Size="Large" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblPlannedInspections" Font-Size="Large" ForeColor="gray"></asp:Label>
                 </td>
                </tr>
                <tr>
                <td style='text-align:left; background:#FFFFF0; border:solid 1px #eeeeee' colspan="3">
                    <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                </td>
                </tr>
                </table>
                
                <div style="border-bottom:solid 3px #4DB8FF; "></div>
                <div style="text-align:left; width:100%;">
                    <table width="100%" cellpadding="3" cellspacing ="0" border="1" style="border-collapse:collapse;">
                    <colgroup>
                     <col style="width:150px;" />
                     <col />
                     <col style="width:60px;text-align:center;" />
                     <col style="width:50px; " />
                     <col style="width:10px;" />
                    </colgroup>
                    <tr style="font-weight:bold; background-color:#E6F0FF; height:30px;">
                        <td>&nbsp;Activity</td>
                        <td>Details</td>
                        <td>Status</td>
                        <td>Action</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >Ok To Board</td>
                        <td> Sent - <asp:LinkButton ID="lbSent" OnClick="lbSent_Click" runat="server"></asp:LinkButton> Recd - <asp:LinkButton ID="lbRecd" OnClick="lbSent_Click" runat="server"></asp:LinkButton></td>
                        <td><asp:Image runat="server" ID="imgStatusOTB_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgStatusOTB_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_SentNow" ImageUrl="~/Modules/HRD/Images/gtk-execute.png"  OnClick="lbAction_SentNow_Click" runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="dv_IP" runat="server" visible="false">
                        <td> Inspection Planning</td>
                        <td align="left">Selected Inspections -  <asp:LinkButton ID="lbSelectedInsps" runat="server"></asp:LinkButton></td>                        
                        <td><asp:Image runat="server" ID="imgIP_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgIP_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_SelectInsps" ImageUrl="~/Modules/HRD/Images/gtk-execute.png" OnClick="lbAction_SelectInsps_Click"  runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="dv_DD" runat="server" visible="false">
                        <td> DryDock</td>
                        <td> Planned dd # - <asp:Label ID="lblPlannedDDNo" runat="server"></asp:Label> DD - Status - <asp:Label ID="lblDDStatus" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgDD_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" /><asp:Image runat="server" ID="imgDD_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" /> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_SelectDD" OnClick="lbAction_DD_Click" ImageUrl="~/Modules/HRD/Images/gtk-execute.png"  runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trHO" runat="server">
                        <td> Handover</td>
                        <td> 
                            Primary Handover To - <asp:Label ID="lblPriHandoverTo" runat="server"></asp:Label> ,
                            Backup handover To - <asp:Label ID="lblBackupHandoverTo" runat="server"></asp:Label> ,
                            HandOver Date - <asp:Label ID="lblHandOverDate" runat="server"></asp:Label>
                        </td>
                        <td><asp:Image runat="server" ID="imgHO_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgHO_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_MakeHandOver" ImageUrl="~/Modules/HRD/Images/gtk-execute.png" OnClick="lbAction_MakeHandOver_Click" runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trBriefing" runat="server" >
                        <td> Briefing</td>
                        <td> Briefing Date - <asp:Label ID="lblBriefingDt" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgBrief_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgBrief_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_Startbreifing" OnClick="lbAction_Startbreifing_Click"  ImageUrl="~/Modules/HRD/Images/gtk-execute.png" runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td> Cash Advance</td>
                        <td><asp:Label ID="lblCashTaken" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgCA_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgCA_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" /> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_PayCash" OnClick="lbAction_PayCash_Click" ImageUrl="~/Modules/HRD/Images/gtk-execute.png" runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td>Travel Itenary</td>
                        <td> 
                            <table width="100%" cellpadding="0" cellspacing ="0" border="0" style="border-collapse:collapse;">
                                <tr>
                                    <td align="left">Dep. Date & Time ( Office Name ) - <asp:Label ID="lblDepDateTime" runat="server"></asp:Label></td>
                                    <td align="left">Arrival Date & Time - <asp:Label ID="lblArrivalDatetime" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td><asp:Image runat="server" ID="imgUTI_R" ImageUrl="~/Modules/HRD/Images/red_circle.png"  /><asp:Image runat="server" ID="imgUTI_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_UpdateItinery" OnClick="lbAction_UpdateItinery_Click" ImageUrl="~/Modules/HRD/Images/gtk-execute.png"  runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trTO" runat="server" >
                        <td> TakeOver</td>
                        <td > TakeOver Date - <asp:Label ID="lblTakeoverDate" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgTO_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgTO_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_GetTakeOver" OnClick="lbAction_GetTakeOver_Click" ImageUrl="~/Modules/HRD/Images/gtk-execute.png"  runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="dv_DDR" runat="server" visible="false">
                        <td> DD - Report</td>
                        <td>Publish date - <asp:Label ID="lblDDPublishDate" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="Image5" ImageUrl="~/Modules/HRD/Images/red_circle.png" /><asp:Image runat="server" ID="Image19" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_Auto" ImageUrl="~/Modules/HRD/Images/gtk-execute.png"  runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="dv_IPR" runat="server" visible="false">
                        <td> Inspection Report</td>
                        <td>Notify Date - <asp:Label ID="lblInspNotifyDate" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgIR_R" ImageUrl="~/Modules/HRD/Images/red_circle.png"  /><asp:Image runat="server" ID="imgIR_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_"  ImageUrl="~/Modules/HRD/Images/gtk-execute.png" runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trDeBriefing" runat="server" >
                        <td> De- Briefing</td>
                        <td> De-Briefing Date - <asp:Label ID="lblDEBriefingDt" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgDeBrief_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgDeBrief_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_StartDeBriefing"  OnClick="lbAction_StartDeBriefing_Click" ImageUrl="~/Modules/HRD/Images/gtk-execute.png"  runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td> Expense Report</td>
                        <td>&nbsp;</td>
                        <td><asp:Image runat="server" ID="imgExp_R" ImageUrl="~/Modules/HRD/Images/red_circle.png"  /><asp:Image runat="server" ID="imgExp_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_Expense" OnClick="lbAction_Expense_Click" ImageUrl="~/Modules/HRD/Images/gtk-execute.png"  runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td> Submit Expense to Accounts </td>
                        <td><div style="float:right;">
                        <asp:ImageButton ID="imgDownloadExp" OnClick="imgDownloadExp_Click" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" ToolTip="Download" runat="server" /> </div>
                        Received in Account By / On - <asp:Label ID="lblRecdDateByOn" runat="server"></asp:Label>&nbsp;
                        Payment By / On- <asp:Label ID="lblPaymentByIn" runat="server"></asp:Label>&nbsp;
                        </td>
                        <td><asp:Image runat="server" ID="imgSendAcct_R" ImageUrl="~/Modules/HRD/Images/red_circle.png"  /><asp:Image runat="server" ID="imgSendAcct_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="IbAction_Account" OnClick="IbAction_Account_Click" ImageUrl="~/Modules/HRD/Images/gtk-execute.png"  runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    </table>
                </div>
                
                <div style="text-align:center; padding:5px;">
                <%--<asp:Button ID="btnPrintExpenseReport" CssClass="btn" runat="server" Text="Print Expense Report" Width="150px" CausesValidation="false" style='margin-bottom:5px' ></asp:Button>
                <asp:Button ID="btnPrintTravelAllowance" CssClass="btn" runat="server" Text="Print Travel Allowance" Width="150px" CausesValidation="false" style='margin-bottom:5px' ></asp:Button>--%>
                <asp:Button ID="Button1" CssClass="btn" runat="server" Text="Close" onclick="btnCloseview_Click" Width="100px" CausesValidation="false" style='margin-bottom:5px; background-color:Red;' ></asp:Button>
                </div>
                
                
        </div>
</center> 
</div>
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dv_SentNow" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
       <div style="position :relative; width:80%; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:70px;  ;opacity:1;filter:alpha(opacity=100)">
          <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Send Now</div>
          <div style="padding-top:2px;">&nbsp;</div>
           <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 99%; HEIGHT: 30px ; text-align:center; border-bottom:none;">
            <table border="1" cellpadding="2" cellspacing="0" rules="rows" bordercolor="#eeeeee" style="width:99%; border-collapse:collapse; height:30px">
            <colgroup>
                <col style="width:50px;" />
                <col style="width:70px;" />
                <col />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:25px;"/>
                </colgroup>
                <tr align="left" style="background-color:#4DB8FF ; color:White;" >
                    <td align="center" class="allCheckbox"><asp:CheckBox runat="server" ID="chkAll" Checked="true" /></td>
                    <td>Emp Code</td>
                    <td align="left">Name</td>
                    <td align="center">Sent On</td>
                    <td align="center">Replied On</td>
                    <td>&nbsp;</td>
                </tr>
            
        </table>
        </div>         
            <div id="dv_1" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 99%; HEIGHT: 175px; text-align:center;">
            <table border="1" cellpadding="2" cellspacing="0" rules="rows" style="width:99%;border-collapse:collapse;">
                <colgroup>
                <col style="width:50px;" />
                <col style="width:70px;" />
                <col />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:25px;"/>
                </colgroup>
                <asp:Repeater ID="rptSentNow" runat="server">
                    <ItemTemplate>
                        <tr >
                            <td align="center" class="singleCheckbox" ><asp:CheckBox ID="chkSelect" Checked="true" empid='<%#Eval("EmpId")%>' email='<%#Eval("Email")%>' runat="server" Visible='<%#Eval("Show").ToString() == "1"%>' /></td>
                            <td align="left"><%#Eval("EmpCode")%></td>
                            <td align="left"><%#Eval("Name")%><span style='color:Green;'><i> ( <%#Eval("Email")%> ) </i></span><br /><span style="color:Blue; font-style:italic;"><%#Eval("RepComment")%></span></td>
                            <td align="center"><%#Common.ToDateString(Eval("SentOn"))%></td>
                            <td align="center"><%#Common.ToDateString(Eval("ReplyOn"))%></td>
                            <td>&nbsp;</td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
        </div>
        <div id="dv_SN_Comments" runat="server" style="text-align:left; padding:5px;">
         <b>Comments : </b> <br />
         <asp:TextBox ID="txtReqComments" TextMode="MultiLine" Height="120px" Width="99%" runat="server" ></asp:TextBox>
        </div>
        <div style="text-align:center;">
           <asp:Button ID="btnSendNow" OnClientClick="this.value='Loading..';" OnClick="btnSendNow_Click" CssClass="btn" Text="Send" runat="server" Width="100px" CausesValidation="false" style='margin-bottom:5px' />
           <asp:Button ID="btnBack" OnClick="btnBack_Click" CssClass="btn" Text="Back" runat="server" Width="100px" CausesValidation="false" style='margin-bottom:5px' />
        </div>
        </div>
      </center> 
</div>
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dv_SelectInsp" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
       <div style="position :relative; width:300px; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:70px;  ;opacity:1;filter:alpha(opacity=100)">
          <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Select Inspections</div>
          <div style="padding-top:2px;">&nbsp;</div>
          <div id="Div3" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 300px; text-align:left;">
                  <ul>
                        <asp:Repeater ID="rptInspGroups" runat="server">
                            <ItemTemplate>
                              <li>
                                    <asp:CheckBox ID="lbCheckInspGroup" CommandArgument='<%#Eval("Id")%>' Text='<%#Eval("Code")%>' runat="server" CssClass='paremtchk' mykey='<%#Eval("Id")%>' onclick='checkChilds(this);' ></asp:CheckBox>
                                    <ul id='ul_<%#Eval("Id")%>'>
                                        <asp:Repeater ID="rptInspections" runat="server" DataSource='<%#BindInspections(Eval("Id"))%>'>
                                            <ItemTemplate>
                                               <li><asp:CheckBox ID="chkInsp" InspId='<%#Eval("Id")%>' runat="server" Text='<%#Eval("Code")%>' CssClass='childchk' innerkey='<%#Eval("InspectionGroup")%>' ></asp:CheckBox></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                     </ul>
                                      
                                   </li>
                            </ItemTemplate>
                        </asp:Repeater>
                       </ul>
          </div>
          <div style="text-align:center;">
           <asp:Button ID="Button2" OnClientClick="this.value='Loading..';" OnClick="btnSelectInspctions_Click" CssClass="btn" Text="Submit" runat="server" Width="100px" CausesValidation="false" style='margin-bottom:5px' />
           <asp:Button ID="Button3" OnClick="btnCloseInsp_Click" CssClass="btn" Text="Close" runat="server" Width="100px" CausesValidation="false" style='margin-bottom:5px' />
        </div>
        </div>
      </center> 
</div>
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dv_Inspections" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
       <div style="position :relative; width:80%; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:70px;  ;opacity:1;filter:alpha(opacity=100)">
          <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Inspections</div>
          <div style="padding-top:2px;">&nbsp;</div>
           <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 99%; HEIGHT: 30px ; text-align:center; border-bottom:none;">
            <table border="1" cellpadding="2" cellspacing="0" rules="rows" bordercolor="#eeeeee" style="width:99%; border-collapse:collapse; height:30px">
            <colgroup>
                <col style="width:150px;" />
                <col style="width:150px;" />
                <col style="width:100px;" />
                <col />
                <col style="width:25px;"/>
                </colgroup>
                 <tr align="left" style="background-color:#4DB8FF ; color:White;" >
                    <td>Insp. Group</td>
                    <td>Inspection#</td>
                    <td>Plan Date</td>
                    <td>Plan Remarks</td>
                    <td>&nbsp;</td>
                </tr>
            
        </table>
        </div>         
            <div id="Div2" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 99%; HEIGHT: 175px; text-align:center;">
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:99%;border-collapse:collapse;">
                <colgroup>
                <col style="width:150px;" />
                <col style="width:150px;" />
                <col style="width:100px;" />
                <col />
                <col style="width:25px;"/>
                </colgroup>
                <asp:Repeater ID="rptInsp" runat="server">
                    <ItemTemplate>
                        <tr >
                            <td align="left"><%#Eval("Code")%></td>
                            <td align="left"><%#Eval("InspectionNo")%></td>
                            <td align="center"><%#Common.ToDateString(Eval("PlanDate"))%></td>
                            <td align="left"><%#Eval("PlanRemark")%></td>
                            <td>&nbsp;</td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
        </div>
      
        <div style="text-align:center; padding:5px;">
           <asp:Button ID="btnImportInsp" OnClientClick="this.value='Loading..';" OnClick="btnImportInsp_Click" CssClass="btn" Text="Import Inspections" runat="server" Width="150px" CausesValidation="false" style='margin-bottom:5px' />
           <asp:Button ID="btnBack_Insp" OnClick="btnBack_Insp_Click" CssClass="btn" Text="Back" runat="server" Width="100px" CausesValidation="false" style='margin-bottom:5px' />
        </div>
        <div style="color:Red;text-align:center; padding-bottom:5px;">
            If Inspection is not planned. Please login to VIMS and Plan Inspection First.
        </div>
        </div>
      </center> 
</div>
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dv_SendForApproval" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
       <div style="position :relative; width:450px; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:220px;  ;opacity:1;filter:alpha(opacity=100)">
          <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Send for Approval</div>
          <div style="padding-top:2px;">&nbsp;</div>
          <div style="padding-top:10px;">
               <b>Select Approver : &nbsp;</b><asp:DropDownList ID="ddlApprover" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlApprover" ErrorMessage="*" ValidationGroup="sendapproval"></asp:RequiredFieldValidator>
          </div>
          <div style="text-align:center;padding-top:10px;padding-bottom:10px;">
           <asp:Button ID="Button4" OnClientClick="this.value='Loading..';" OnClick="btnSendApprovalTo_Click" CssClass="btn" Text="Send" runat="server" ValidationGroup="sendapproval" Width="100px" style='margin-bottom:5px' />
           <asp:Button ID="Button5" OnClick="btnCloseInspApproval_Click" CssClass="btn" Text="Close" runat="server" Width="100px" CausesValidation="false" style='margin-bottom:5px' />
        </div>
        </div>
        </center>
</div>
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dv_SendToAccounts" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
       <div style="position :relative; width:450px; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:220px;  ;opacity:1;filter:alpha(opacity=100)">
          <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Send to Accounts</div>
          <div style="padding-top:2px;">&nbsp;</div>
          <div style="padding-top:10px;">
               <b>Select file : &nbsp;</b><asp:FileUpload ID="flpUpload" runat="server" /> <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="flpUpload" ErrorMessage="*" ValidationGroup="acct"></asp:RequiredFieldValidator>
          </div>
          <div style="text-align:center;padding-top:10px;padding-bottom:10px;">
           <asp:Button ID="btnSaveAccount" OnClientClick="this.value='Loading..';" OnClick="btnSaveAccounts_Click" CssClass="btn" Text="Save" runat="server" ValidationGroup="acct" Width="70px" style='margin-bottom:5px' />
           <asp:Button ID="btnSendToAccount" OnClientClick="this.value='Loading..';" OnClick="btnSendToAccounts_Click" CssClass="btn" Text="Send To Account" runat="server" Width="120px" style='margin-bottom:5px' />
           <asp:Button ID="Button7" OnClick="btnCloseAccounts_Click" CssClass="btn" Text="Back" runat="server" Width="70px" CausesValidation="false" style='margin-bottom:5px' />
        </div>
        </div>
        </center>
</div>

<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dv_SelectDD" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
       <div style="position :relative; width:450px; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:220px;  ;opacity:1;filter:alpha(opacity=100)">
          <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Select Docket</div>
          <div style="padding-top:2px;">&nbsp;</div>
          <div style="padding-top:10px;">
               <table cellpadding="0" cellspacing="0" border="0" width="100%">
                 <tr>
                      <td  style="text-align:right; font-weight:bold;">Select Docket : &nbsp;</td>
                      <td colspan="3" style="text-align:left;"><asp:DropDownList ID="ddlDocket" AutoPostBack="true" OnSelectedIndexChanged="ddlDocket_OnSelectedIndexChanged" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDocket" ErrorMessage="*" ValidationGroup="Seldd"></asp:RequiredFieldValidator></td>
                 </tr>

                 <tr>
                        <td style="text-align:right; width:170px;font-weight:bold; ">Docking Arrival Date : </td>
                        <td style="text-align:left">
                         <asp:TextBox ID="txtDocArrivalDt" runat="server" Width="100px" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDocArrivalDt" ErrorMessage="*" ValidationGroup="Seldd"></asp:RequiredFieldValidator>
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtDocArrivalDt" Format="dd-MMM-yyyy" ></ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>                     
                   <tr>
                        <td style="text-align:right;width:170px;font-weight:bold; ">Repair Commenced Date : </td>
                        <td style="text-align:left">
                         <asp:TextBox ID="txtExecFrom" runat="server" Width="100px" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtExecFrom" ErrorMessage="*" ValidationGroup="Seldd"></asp:RequiredFieldValidator>
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtExecFrom" Format="dd-MMM-yyyy" ></ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; width:170px;font-weight:bold; ">Estimated Completion Date : </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtExecTo" runat="server" Width="100px" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtExecTo" ErrorMessage="*" ValidationGroup="Seldd"></asp:RequiredFieldValidator>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtExecTo" Format="dd-MMM-yyyy" ></ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <asp:HiddenField ID="hfdRFQId" runat="server" />
               </table> 
               
          </div>
          <div style="text-align:center;padding-top:10px;padding-bottom:10px;">
           <asp:Button ID="btnSelectDD" OnClientClick="this.value='Loading..';" OnClick="btnSelectDD_Click" CssClass="btn" Text="Save" runat="server" ValidationGroup="Seldd" Width="100px" style='margin-bottom:5px' />
           <asp:Button ID="Button8" OnClick="btnCloseDD_Click" CssClass="btn" Text="Close" runat="server" Width="100px" CausesValidation="false" style='margin-bottom:5px' />
        </div>
        </div>
        </center>
</div>

<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvIframe" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
       <div style="position :relative; width:90%; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:50px;  ;opacity:1;filter:alpha(opacity=100)">
          <iframe src="" runat="server" id="frmlnk" width="100%" height="600px" frameborder="no"></iframe>
        </div>
      
        </center>
</div>

<div style="position:fixed; bottom:0px; padding:4px; background-color:#4DB8FF; width:100%; text-align:right;">
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $(".tabmenu").click(function () {
            $(".tabpan").hide();
            $(".tabmenu").removeClass('active');
            $(this).addClass('active');
            $("#" + $(this).attr('tabname')).show();
        });

        $(".tabpan").first().show();
    });

    function checkChilds(ctl) {
        var key = $(ctl).parent().attr("mykey");
        $("#ul_" + key).find("input").prop("checked", $(ctl).prop("checked"));
    }
</script>
<script type="text/javascript">
    $(function () {
        var $allCheckbox = $('.allCheckbox :checkbox');
        var $checkboxes = $('.singleCheckbox :checkbox');
        $allCheckbox.change(function () {
            if ($allCheckbox.is(':checked')) {
                $checkboxes.attr('checked', 'checked');
            }
            else {
                $checkboxes.removeAttr('checked');
            }
        });
        $checkboxes.change(function () {
            if ($checkboxes.not(':checked').length) {
                $allCheckbox.removeAttr('checked');
            }
            else {
                $allCheckbox.attr('checked', 'checked');
            }
        });
    });
</script>
</div>
</form>
</body>
</html>
