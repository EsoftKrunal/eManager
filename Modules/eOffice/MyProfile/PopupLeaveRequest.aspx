<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupLeaveRequest.aspx.cs" Inherits="emtm_MyProfile_Emtm_PopupLeaveRequest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Management</title>
     <link href="../style.css" rel="stylesheet" type="text/css" />
     <style>
     .monthtd
     {
     	border:solid 1px gray; 
     	border-bottom :none;
     	background-color : #C2C2C2;
     	cursor:pointer;  
     }
     .monthtdselected
     {
     	border:solid 1px gray; 
     	border-bottom :none;
     	background-color : #E5A0FC;
     	cursor:pointer;
     }
     .box_
     {
     	background-color :White; 
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_H
     {
     	background-color :Orange; 
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_W
     {
     	background-color :LightGray;
     	color :Black; 
     	border :solid 1px gray;
     }	
     .box_L
     {
     	background-color :Green;
     	color :White; 
     	border :solid 1px gray;
     }
     .box_P
     {
     	background-color :Yellow;
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_B
     {
     	background-color :Purple;
     	color :White; 
     	border :solid 1px gray;
     }
     .box_C
     {
     	background-color :#B8FF94; 
     	color :Black; 
     	border :solid 1px gray;
     }
     </style> 
     <script language="javascript" type="text/javascript">
         function CloseWindow()
         {
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
     </script>
</head>
<body style=" padding :10px;" >
    <form id="form1" runat="server">
    <div style="border:solid 1px #4371a5" >
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style=" background-color :#4371a5; height :30px; font-size :14px; font-weight: bold; text-align:center; color:White">
        &nbsp;Leave Planner  
        </td>
        </tr>
        </table> 
        <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
        <center>
        <div style="position : absolute; top:100px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
            <center>
            <div style="border:none; height :50px; width :120px;" >
            <img src="../../Images/Emtm/loading.gif" alt="Loading..."/> Loading ...
            </div>
            </center>
        </div>
        </center>
        </ProgressTemplate>
        </asp:UpdateProgress>   
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style =" background-color : #75B2DD;">
        <table cellpadding="5" cellspacing="0" border="0">
        <tr>
        <td>Location : </td><td>
        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="True" onselectedindexchanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
        </td>
        <td>Department : </td><td><asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged" ></asp:DropDownList></td>
        <td>
        <asp:DropDownList runat="server" ID="ddlYear" Width="60px" AutoPostBack="True" onselectedindexchanged="ddlYear_SelectedIndexChanged"></asp:DropDownList> 
        </td>
        <td style="display:none;">
           <asp:TextBox ID="txtMonthId" runat="server"></asp:TextBox>
           <asp:Button ID="hdnShowMonth" runat="server" CausesValidation="false" onclick="hdnShowMonth_Click"/> 
        </td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td style =" background-color : #75B2DD; text-align:center; text-transform :uppercase; font-weight:bold; font-size :13px;">
        <table cellpadding="5" cellspacing="0" border="0" width="100%">
        <tr>
        <td width="201px" style=" text-align:left">&nbsp;</td>
        <td class="box_H" style="border:none;">Holiday</td>
        <td class="box_W" style="border:none;">Weekly Off</td>
        <td style="color:White;border:none; background-color :Green">Approved Leave</td>
        <td class="box_P" style="border:none;">Planned Leave</td>
        <td class="box_B" style="border:none;">Business Trip</td>
        </tr>
        </table>
        </td>
        </tr>
        <tr>
        <td>
            <table cellpadding="3" cellspacing="0" border="0" style="border-collapse :collapse; text-align :center;" width="100%">
            <tr>
            <td id="td" runat="server" class="monthtd" width="200px"></td>
            <td id="tdJan" runat="server" class="monthtd" onclick="ShowCurrentMonth(1,this);">Jan</td>
            <td id="tdFeb" runat="server" class="monthtd" onclick="ShowCurrentMonth(2,this);">Feb</td>
            <td id="tdMar" runat="server" class="monthtd" onclick="ShowCurrentMonth(3,this);">Mar</td>
            <td id="tdApr" runat="server" class="monthtd" onclick="ShowCurrentMonth(4,this);">Apr</td>
            <td id="tdMay" runat="server" class="monthtd" onclick="ShowCurrentMonth(5,this);">May</td>
            <td id="tdJun" runat="server" class="monthtd" onclick="ShowCurrentMonth(6,this);">Jun</td>
            <td id="tdJul" runat="server" class="monthtd" onclick="ShowCurrentMonth(7,this);">Jul</td>
            <td id="tdAug" runat="server" class="monthtd" onclick="ShowCurrentMonth(8,this);">Aug</td>
            <td id="tdSep" runat="server" class="monthtd" onclick="ShowCurrentMonth(9,this);">Sep</td>
            <td id="tdOct" runat="server" class="monthtd" onclick="ShowCurrentMonth(10,this);">Oct</td>
            <td id="tdNov" runat="server" class="monthtd" onclick="ShowCurrentMonth(11,this);">Nov</td>
            <td id="tdDec" runat="server" class="monthtd" onclick="ShowCurrentMonth(12,this);">Dec</td>
            <td class="monthtd" width="200px" >&nbsp;</td>
            </tr>
            </table>
            <asp:Literal ID="MonthView" runat="server"></asp:Literal>  
        </td>
        </tr>
        </table> 
        </ContentTemplate> 
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td colspan="2" style=" background-color :#4371a5; height :30px; font-size :14px; font-weight: bold; text-align:center; color:White">
        &nbsp;Leave Request 
        </td>
        </tr>
        <tr>
        <td colspan="2" style="text-align:left; font-size:13px; color:Red; padding-left:10px;" >
            <asp:Literal runat="server" ID="litNotes"></asp:Literal>
        </td>
        </tr>
            <tr>
            <td style=" padding :10px; vertical-align:top; width:720px;">
                 <fieldset >
                 <legend>Leave Planning </legend> 
                    <table id="tblview" runat="server" width="700px" cellpadding="5" cellspacing ="0" border="0">
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
                                       <asp:DropDownList ID="ddlLeaveType" runat="server" required='yes' ValidationGroup="planleave" Width="200px"></asp:DropDownList>
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
                        <td align="right" style="padding-right :30px;" >
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="imgLeaveTo" PopupPosition="TopLeft" 
                                           TargetControlID="txtLeaveTo">
                                       </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="imgLeaveFrom" PopupPosition="TopLeft" 
                                           TargetControlID="txtLeaveFrom">
                                       </ajaxToolkit:CalendarExtender>
                        <asp:Button ID="btnsave" CssClass="btn" runat="server" Text="Save" onclick="btnsave_Click" Width="100px" ValidationGroup="planleave"></asp:Button>
                        </td>
                    </tr>
                 </table>
                 </fieldset>
            </td>
            <td style=" padding :10px; vertical-align:top;" >
             <div id="divLeavePlan" runat="server" visible="false">
                 <fieldset >
                 <legend>Request<span lang="en-us"> for Approval </span></legend>
                 <asp:UpdatePanel runat="server" ID="UP2">
                 <ContentTemplate>
                 <table width="100%" cellpadding="5" cellspacing ="0" border="0" height="" >
                   <tr runat="server" visible="false">
                       <td style="text-align :right">
                           Location :</td>
                       <td style="text-align :left">
                           <asp:DropDownList ID="ddlOffice" runat="server" required='yes' 
                               AutoPostBack="true" 
                               onselectedindexchanged="ddlOffice_SelectedIndexChanged" 
                               ValidationGroup="RequestLeave" Width="150px">
                           </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="ddlOffice" ErrorMessage="*" ValidationGroup="RequestLeave"></asp:RequiredFieldValidator>
                       </td>
                       <td style="text-align :left">
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td style="text-align :right">
                           Forward To :</td>
                       <td style="text-align :left">
                           <asp:DropDownList ID="ddlUsers" runat="server" required='yes' Width="150px" 
                               ValidationGroup="RequestLeave">
                           </asp:DropDownList>
                           <asp:RequiredFieldValidator ID="rfvusers" runat="server" 
                            ControlToValidate="ddlUsers" ErrorMessage="*" ValidationGroup="RequestLeave"></asp:RequiredFieldValidator>
                       </td>
                       <td style="text-align :left">
                           <asp:Button ID="btnAwaitingApproval" runat="server" CssClass="btn" 
                               onclick="btnAwaitingApproval_Click" OnClientClick="this.value='Please Wait..';" 
                               Text="Send" ValidationGroup="RequestLeave" Width="150px" />
                       </td>
                   </tr>
                 </table>
                 </ContentTemplate>
                 </asp:UpdatePanel>
                 </fieldset>
                 <fieldset >
                 <legend>Discussion</legend>
                  <div style="height:135px; overflow-x:hidden; overflow-y:scroll;">
                    <asp:Repeater runat="server" ID="rptComments">
                    <ItemTemplate>
                        <div style="text-align:left"><span style='color:Red'> ■ <%#Eval("Emp") %> :</span><span style='color:Purple; font-style:italic'><%#Eval("Comments").ToString().Replace("''","'") %></span></div>
                    </ItemTemplate>
                    </asp:Repeater>
                    </div>
                </fieldset>
                 </div> 
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
