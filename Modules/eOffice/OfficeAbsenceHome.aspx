<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsenceHome.aspx.cs" Inherits="emtm_Emtm_OfficeAbsenceHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../JS/jquery.min.js" ></script>
    <link href="style_new.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function HideFrame() {
            $("#btnHideFrame").click();
        }
     </script>
     <style type="text/css">
        span
         {
             color:Red;
         }
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Button runat="server" ID="btnHideFrame" OnClick="btnHideFrame_Click" style="display:none;"/>
         <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">Office Absence</div>
         <div style="padding:5px 5px 5px 5px; " >
             <table border="0" cellpadding="" cellspacing="0" width="100%">
                 <tr>
                      <td style="width:60px; text-align:right;"><b>Office :</b>&nbsp;</td>
                      <td style="width:105px; text-align:left;"><asp:DropDownList ID="ddlOffice" runat="server" Width="100%" AutoPostBack="True" onselectedindexchanged="ddlOffice_SelectedIndexChanged"></asp:DropDownList></td>
                      <td style="width:100px; text-align:right;"><b>&nbsp;Department :&nbsp;</b></td>
                      <td style="width:120px; text-align:left;"><asp:DropDownList ID="ddlDepartment" Width="100%" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged" ></asp:DropDownList></td>
                      <td style="text-align:right;width:80px;"><b>&nbsp;Position :&nbsp;</b></td>
                      <td style=" text-align:left;"><asp:DropDownList ID="ddlPosition" runat="server" Width="200px" AutoPostBack="True" onselectedindexchanged="ddlPosition_SelectedIndexChanged" ></asp:DropDownList></td>
                      <td style="width:80px; text-align:right;"><b>&nbsp;Period :&nbsp;</b></td>
                      <td style="width:210px; text-align:left;"><asp:TextBox ID="txtFrom" MaxLength="12"  Width="72px" AutoPostBack="True" OnTextChanged="txtFrom_TextChanged" ToolTip="Period (From Date)" runat="server"></asp:TextBox> - <asp:TextBox ID="txtTo" AutoPostBack="True" OnTextChanged="txtTo_TextChanged" MaxLength="12" Width="72px" ToolTip="Period (To Date)" runat="server"></asp:TextBox></td>
                      <td style="width:110px; text-align:right;"><b>&nbsp;Absence Type :&nbsp;</b></td>
                      <td style="width:155px; text-align:left;"><asp:DropDownList ID="ddlAbsType" Width="100%" runat="server" AutoPostBack="True" onselectedindexchanged="ddlAbsType_SelectedIndexChanged" >
                                                                   <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                                   <asp:ListItem Text="Leave" Value="Leave"></asp:ListItem>
                                                                   <asp:ListItem Text="Bussiness Travel" Value="Bussiness Travel"></asp:ListItem>
                                                                </asp:DropDownList>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFrom" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtTo" PopupPosition="BottomRight"></ajaxToolkit:CalendarExtender>
                      </td>
                 </tr>
             </table>
         </div>
         <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="padding-right:10px; text-align:center; color:Red">
                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <table cellpadding="0" cellspacing ="0" border ="0" width="100%"  >
                                        <tr>
                                            <td>                                                
                                                <div style="padding:5px 5px 5px 5px;" >
                                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                                           <colgroup>
                                                                <col style="width:25px;" />
                                                                <col />                               
                                                                <col style="width:70px;" />
                                                                <col style="width:150px;" />
                                                                <col style="width:150px;" />
                                                                <col style="width:200px;" />
                                                                <col style="width:90px;"/>
                                                                <col style="width:200px;"/>
                                                                <col style="width:60px;"/>
                                                                <col style="width:25px;" />
                                                           </colgroup>
                                                           <tr align="left" class="blueheader"> 
                                                           <td></td>
                                                           <td>EMP NAME</td>
                                                           <td>Office</td>
                                                           <td>Department</td>
                                                           <td>Absence Type</td>
                                                           <td>Absence Period</td>
                                                           <td>Visit Type</td>
                                                           <td>Vessel</td>
                                                           <td>Status</td>
                                                           <td>&nbsp;</td>
                                                           </tr>
                                                    </table> 
                                                </div>          
                                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 325px ; text-align:center;">
                                                   <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                    <colgroup>
                                                        <col style="width:25px;" />
                                                        <col />                               
                                                        <col style="width:70px;" />
                                                        <col style="width:150px;" />
                                                        <col style="width:150px;" />
                                                        <col style="width:200px;" />
                                                        <col style="width:90px;"/>
                                                        <col style="width:200px;"/>
                                                        <col style="width:60px;"/>
                                                        <col style="width:25px;" />
                                                    </colgroup>
                                                   <asp:Repeater ID="rptData" runat="server" >
                                                   <ItemTemplate>
                                                      <tr > <%--class='<%# (Common.CastAsInt32(Eval("LeaveRequestId"))==LeaveRequestId)?"selectedrow":"row"%>'--%>
                                                           <td align="center"><asp:ImageButton ID="btnView" runat="server" CommandArgument='<%# (Eval("LeaveRequestId").ToString() + "|" + Eval("EmpId").ToString()) %>' OnClick="btnView_Click" ToolTip="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" Visible='<%#(Eval("Type").ToString() == "BT" && Eval("Status").ToString() == "Approved") %>' /></td>
                                                           <td align="left"><%#Eval("Name")%></td>
                                                           <td align="left"><%#Eval("OfficeName")%></td>
                                                           <td align="left"><%#Eval("DeptName")%></td>
                                                           <td align="left"><%#Eval("LeaveTypeName")%></td>
                                                           <td align="center"><%#Common.ToDateString(Eval("LeaveFrom"))%> - <%#Common.ToDateString(Eval("LeaveTo"))%></td>
                                                           <td align="left"><%#Eval("Location")%></td>
                                                           <td align="left"><%#Eval("Vessel")%></td>
                                                           <td align="left"><%#Eval("Status")%></td>
                                                           <td>&nbsp;</td>
                                                       </tr>
                                                   </ItemTemplate>
                                                  </asp:Repeater>
                                                </table>
                                                </div> 
                                                </div>
                                                
                                            </td>
                                        </tr>
                                    </table>
                            </td>
                        </tr> 
                     </table>      
         
         <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dv_ViewBT" visible="false" >
             <center>
              <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
              <div style="position :relative; width:90%; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:10px;  ;opacity:1;filter:alpha(opacity=100)" >
            <div style='padding:5px; background:#4DB8FF; color:White; text-align:center; font-size:14px;'>Biz. Travel Details</div>
            <table id="Table1" runat="server" width="100%" cellpadding="3" cellspacing ="0" border="0">
                <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblLocation" Font-Size="12px" ForeColor="Green"></asp:Label> - 
                    <asp:Label runat="server" ID="lblPurpose" Font-Size="12px" ForeColor="orange"></asp:Label>
                 </td>
                 <td style="text-align:left"><asp:Label runat="server" ID="lblPeriod" Font-Size="12px" ForeColor="gray"></asp:Label> </td>
                 <td style="text-align:right">
                     <asp:Label runat="server" ID="lblHalfDay" Font-Size="12px" ForeColor="purple"></asp:Label>
                 </td>
                </tr>
                 <tr>
                <td style='text-align:left'>
                    <asp:Label runat="server" ID="lblVesselName" Font-Size="12px" ForeColor="Green"></asp:Label>
                 </td>
                 <td style="text-align:left" colspan="2">
                     <asp:Label runat="server" ID="lblPlannedInspections" Font-Size="12px" ForeColor="gray"></asp:Label>
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
                     <col style="width:40px;text-align:center;" />
                     <col style="width:25px;" />
                     <col style="width:10px;" />
                    </colgroup>
                    <tr style="font-weight:bold; background-color:#E6F0FF; height:20px;">
                        <td>&nbsp;Activity</td>
                        <td>Details</td>
                        <td>Status</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >Ok To Board</td>
                        <td> Sent - <asp:LinkButton ID="lbSent" OnClick="lbSent_Click" runat="server"></asp:LinkButton> Recd - <asp:LinkButton ID="lbRecd" OnClick="lbSent_Click" runat="server"></asp:LinkButton></td>
                        <td><asp:Image runat="server" ID="imgStatusOTB_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgStatusOTB_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="dv_IP" runat="server" visible="false">
                        <td> Inspection Planning</td>
                        <td align="left">Selected Inspections -  <asp:LinkButton ID="lbSelectedInsps" runat="server"></asp:LinkButton></td>                        
                        <td><asp:Image runat="server" ID="imgIP_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgIP_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="dv_DD" runat="server" visible="false">
                        <td> DryDock Planning</td>
                        <td> Planned dd # - <asp:Label ID="lblPlannedDDNo" runat="server"></asp:Label> DD - Status - <asp:Label ID="lblDDStatus" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgDD_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" /><asp:Image runat="server" ID="imgDD_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" /> </td>
                        <td>&nbsp;</td>
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
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trBriefing" runat="server" >
                        <td> Briefing</td>
                        <td> Briefing Date - <asp:Label ID="lblBriefingDt" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgBrief_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgBrief_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_Startbreifing" OnClick="lbAction_Startbreifing_Click"  ImageUrl="~/Modules/HRD/Images/gtk-execute.png" ToolTip="Briefing" runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td> Cash Advance</td>
                        <td><asp:Label ID="lblCashTaken" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgCA_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgCA_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" /> </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td> Update Travel Itenary</td>
                        <td> 
                            <table width="100%" cellpadding="0" cellspacing ="0" border="0" style="border-collapse:collapse;">
                                <tr>
                                    <td align="left">Dep. Date & Time ( Office Name ) - <asp:Label ID="lblDepDateTime" runat="server"></asp:Label></td>
                                    <td align="left">Arrival Date & Time - <asp:Label ID="lblArrivalDatetime" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td><asp:Image runat="server" ID="imgUTI_R" ImageUrl="~/Modules/HRD/Images/red_circle.png"  /><asp:Image runat="server" ID="imgUTI_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trTO" runat="server" >
                        <td> TakeOver</td>
                        <td > TakeOver Date - <asp:Label ID="lblTakeoverDate" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgTO_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgTO_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="dv_DDR" runat="server" visible="false">
                        <td> DD - Report</td>
                        <td>Publish date - <asp:Label ID="lblDDPublishDate" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="Image5" ImageUrl="~/Modules/HRD/Images/red_circle.png" /><asp:Image runat="server" ID="Image19" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="dv_IPR" runat="server" visible="false">
                        <td> Inspection Report</td>
                        <td>Notify Date - <asp:Label ID="lblInspNotifyDate" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgIR_R" ImageUrl="~/Modules/HRD/Images/red_circle.png"  /><asp:Image runat="server" ID="imgIR_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trDeBriefing" runat="server" >
                        <td> De-Briefing</td>
                        <td> De-Briefing Date - <asp:Label ID="lblDEBriefingDt" runat="server"></asp:Label></td>
                        <td><asp:Image runat="server" ID="imgDeBrief_R" ImageUrl="~/Modules/HRD/Images/red_circle.png" Visible="false" /><asp:Image runat="server" ID="imgDeBrief_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td align="center"><asp:ImageButton ID="lbAction_StartDeBriefing"  OnClick="lbAction_StartDeBriefing_Click" ImageUrl="~/Modules/HRD/Images/gtk-execute.png" ToolTip="De-Briefing"  runat="server"></asp:ImageButton></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td> Expense</td>
                        <td>&nbsp;</td>
                        <td><asp:Image runat="server" ID="imgExp_R" ImageUrl="~/Modules/HRD/Images/red_circle.png"  /><asp:Image runat="server" ID="imgExp_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td> Send Expense to Accounts </td>
                        <td><div style="float:right;"><asp:ImageButton ID="imgDownloadExp" OnClick="imgDownloadExp_Click" ImageUrl="~/Modules/HRD/Images/paperclip12.gif" ToolTip="Download" runat="server" /> </div>Request Date - <asp:Label ID="lblSEAReqDate" runat="server"></asp:Label>&nbsp;</td>
                        <td><asp:Image runat="server" ID="imgSendAcct_R" ImageUrl="~/Modules/HRD/Images/red_circle.png"  /><asp:Image runat="server" ID="imgSendAcct_G" ImageUrl="~/Modules/HRD/Images/green_circle.gif" Visible="false"/> </td>
                        <td></td>
                        <td>&nbsp;</td>
                    </tr>
                    </table>
                </div>
                
                <div style="text-align:center; padding:2px;">
                    <asp:Button ID="Button1" CssClass="btn" runat="server" Text="Close" onclick="btnCloseview_Click" Width="100px" CausesValidation="false" style='margin-bottom:2px; background-color:Red;' ></asp:Button>
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
                        <col style="width:70px;" />
                        <col />
                        <col style="width:100px;" />
                        <col style="width:100px;" />
                        <col style="width:25px;"/>
                        </colgroup>
                        <tr align="left" style="background-color:#4DB8FF ; color:White;" >                            
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
                        <col style="width:70px;" />
                        <col />
                        <col style="width:100px;" />
                        <col style="width:100px;" />
                        <col style="width:25px;"/>
                        </colgroup>
                        <asp:Repeater ID="rptSentNow" runat="server">
                            <ItemTemplate>
                                <tr >
                                    <td align="left"><%#Eval("EmpCode")%></td>
                                    <td align="left"><%#Eval("Name")%><br /><span style="color:Blue; font-style:italic;"><%#Eval("RepComment")%></span></td>
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
                   <%--<asp:Button ID="btnSendNow" OnClick="btnSendNow_Click" CssClass="btn" Text="Send" runat="server" Width="100px" CausesValidation="false" style='margin-bottom:5px' />--%>
                   <asp:Button ID="btnBack" OnClick="btnBack_Click" CssClass="btn" Text="Back" runat="server" Width="100px" CausesValidation="false" style='margin-bottom:5px' />
                </div>
                </div>
              </center> 
        </div>
         <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" id="dvIframe" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Black; z-index:100; opacity:0.7;filter:alpha(opacity=70)"></div>
               <div style="position :relative; width:90%; padding :0px; text-align :center; border :solid 3px #4DB8FF; background : white; z-index:150;top:10px;  ;opacity:1;filter:alpha(opacity=100)">
                  <iframe src="" runat="server" id="frmlnk" width="100%" height="600px" frameborder="no"></iframe>
                </div>
      
                </center>
        </div>               
    </div>
    </form>
    
</body>
</html>
