<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PeapSummary.aspx.cs" Inherits="Emtm_PeapSummary" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Page</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7">

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

     <script language="javascript" type="text/javascript">
         function focusthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "70px";
         }
        function blurthis(txt) {
             var ctrid = txt.id;
             document.getElementById(ctrid).style.height = "30px";
         }
         function PopUPWindow(EmpId) {
             winref = window.open('../MyProfile/Emtm_Profile_LeaveHistory.aspx?EmpId=' + EmpId, '', 'title=no,toolbars=no,scrollbars=yes,width=1100,height=530,left=150,top=150,addressbar=no,resizable=1,status=0');
             return false;
         }
         function PopUPWindowBT(EmpId) {
             winref = window.open('../StaffAdmin/Popup_BusinessTripDetails.aspx?EmpId=' + EmpId, '', 'title=no,toolbars=no,scrollbars=yes,width=1100,height=530,left=150,top=150,addressbar=no,resizable=1,status=0');
             return false;
         }
     </script> 
     <script type="text/javascript" language="javascript">
         function showReport(PID) {
             window.open('../../Reporting/PeapReport.aspx?PId=' + PID, 'asdf', '');
         }
    </script>
</head>
<body>
    <form id="form1" runat="server" > 
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
          <table width="100%">
                <tr>
               
                <td valign="top" style="border:solid 1px #4371a5; height:500px;background-color:#f9f9f9;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                        <table cellpadding="2" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td style="background-color: #4371a5; text-align: center; height: 23px; font-size:15px;" CssClass="text">
                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                    <col width="310px" />
                                    <col />
                                    <col width="200px" />
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td style="font-size:16px; font-weight:bold; color:White; text-align:center;"> 
                                            Performance Evaluation & Assessment Of Potential (PEAP)</td>
                                        <td style="font-size:10px; color:White;vertical-align:top;"> 
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="padding:10px">
                                <span style="font-size:Large; font-weight:bold; color:#336699;">
                                [ 
                                    <asp:Label ID="txtFirstName" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label>
                                    <asp:Label ID="txtLastName" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label> &nbsp;/ <asp:Label ID="lblPeapLevel" runat="server" style="font-size:Large; font-weight:bold;" CssClass="input_box"></asp:Label>
                                &nbsp;]
                                </span>
                                    <span style="font-size:Large; font-weight:bold; color:#6600CC;">
                                ( 
                                    <asp:Label ID="txtOccasion" style="font-size:Large; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                &nbsp;)
                                </span>
                                <span style="font-size:Large; font-weight:bold; color:#000;">
                                     [
                                      <asp:Label ID="lblPeapPeriod" style="font-size:Large; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                     ] 
                                </span>
                                <span style="float:right"> 
                                    <asp:ImageButton runat="server" ID="btnBack" ImageUrl="~/Modules/HRD/Images/back_button.gif" AlternateText="Back" OnClick="btnBack_Click" />
                                </span>
                                <br /><br />
                                <hr />
                                <span style="font-size:12px; font-weight:bold; color:#FF9900; padding-right:20px;">
                                     [
                                      Department :&nbsp;<asp:Label ID="lblDepartment" style="font-size:12px; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                     ]
                                </span>
                                <span style="font-size:12px; font-weight:bold; color:#009900; padding-right:20px;">
                                     [
                                      Position :&nbsp;<asp:Label ID="lblPosition" style="font-size:12px; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                     ]
                                </span>
                                <span style="font-size:12px; font-weight:bold; color:#0066FF;">
                                     [
                                      Reporting To :&nbsp;<asp:Label ID="lblReportingTo" style="font-size:12px; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                     ]
                                </span>
                                <br /><br />
                                 <span style="font-size:12px; font-weight:bold; color:#660066; padding-right:20px;">
                                     [
                                      Avg. Perf. Score :&nbsp;<asp:Label ID="lblAvgPerfScore" style="font-size:12px; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                     ]
                                </span>
                                 <span style="font-size:12px; font-weight:bold; color:#990000;">
                                     [
                                      Avg. Comp. Score :&nbsp;<asp:Label ID="lblAvgCompScore" style="font-size:12px; font-weight:bold;" runat="server" CssClass="input_box"></asp:Label>
                                     ]
                                </span>
                                <asp:Button ID="btnPeapReport" Text="Print" CssClass="btn" style="float:right; padding-right:5px;" runat="server" OnClick="btnPeapReport_Click" ToolTip="View Peap Report" />
                                <br /><br />
                                <hr />
                                <div style="padding:5px;width:99%;">
                                <%--<asp:Label ID="lblPeapStatus" runat="server" CssClass="input_box" Font-Bold="True" Font-Size="Large" ForeColor="#993300"></asp:Label>--%>
                                       <table cellpadding="0" cellspacing="0" width="100%" border="1" style="border-color:Black;">
                                            <tr>
                                                <td id="td_SA" runat="server" style="text-align:center; " ><b>Self Assessment</b></td>
                                                <td id="td_ABA" runat="server" style="text-align:center; " ><b>Assessment by Appraiser</b></td>
                                                <td id="td_MF" runat="server" style="text-align:center; " ><b>Management FeedBack</b></td>
                                                <td id="td_PC" runat="server" style="text-align:center; " ><b>Peap Closed</b></td>
                                            </tr>
                                         </table>
                                </div>
                                <div style="padding:5px; font-weight:bold; font-size:14px; color:#993300; background-color:#f2f2f2;">
                                    Office Absence Record 
                                </div>
                                <hr />
                                <div class="style1" style="width:99%;" > 
                                    Leave Balance upto <asp:Label id="lblCurrentMonth1" runat="server"></asp:Label> is <asp:Label ID="lblLeaveBalance" runat="server" MaxLength="100" Font-Size="14px" Font-Bold="true"  ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Business Travel :&nbsp;<asp:LinkButton ID="lbOfficeAbsence" ForeColor="Red" Font-Size="13px" runat="server" onclick="lbOfficeAbsence_Click" />
                                    <asp:ImageButton ID="btnLeaveHistory" ToolTip="Leave History" style="float:right; vertical-align:top;" runat="server" ImageUrl="~/Modules/HRD/Images/LeaveHistory.png" Height="30px" Width="120px" onclick="btnLeaveHistory_Click" />
                                    
                                </div> 
                                <div style="width :900px;" > 
                                     <asp:Literal ID="LiteralTotalLeaves" runat="server"></asp:Literal> 
                                </div>
                                <hr />
                                <div id="div1" runat="server" style="border:dotted 1px #9F5F9F; background-color:#FFF0FF;">
                                <table style="width:100%; font-size:14px;"  cellpadding="0" cellspacing="10" >
                                <colgroup>
                                <col width="250px" />
                                <col width="170px"/>
                                <col width="200px" />
                                <col width="80px"/>
                                <col />
                                </colgroup>
                                    <tr id="trSAPending" runat="server" visible="false">
                                        <td>&nbsp;<b>1. Self Assessment</b></td>
                                        <td><asp:Label ID="lblSATot" runat="server"></asp:Label> </td>
                                        <td style="border:solid 1px gray"><div id="divSAPercent" runat="server" style="background-color:#51B751;">&nbsp;</div></td>
                                        <td>&nbsp;<asp:Label runat="server" ID="lblPeapSAPer"></asp:Label> </td>
                                        <td>
                                            <asp:LinkButton ID="lbStart" Font-Size="12px" Font-Bold="true" Text="Start" runat="server" onclick="lbStart_Click"/>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                                <div style=" height:3px;">&nbsp;</div>
                                <div id="trTotAppraisers" runat="server" visible="false" style="border:dotted 1px green; background-color:#F0FFF0; ">
                                <table style="width:100%; font-size:14px;"  cellpadding="0" cellspacing="10" >
                                <colgroup>
                                <col width="250px" />
                                <col width="170px"/>
                                <col width="200px" />
                                <col width="80px"/>
                                <col />
                                </colgroup>
                                <tr id="trARPending" runat="server" visible="false" >                                    
                                       <td >&nbsp;<b>2. Assessment By Appraiser</b></td>
                                       <td><asp:Label ID="lblTotAPR" runat="server" Visible="false"></asp:Label></td>
                                       <td><div id="divAPRPercent" visible="false" runat="server" style="background-color:#51B751;">&nbsp;</div></td>
                                       <td>&nbsp;<asp:Label runat="server" ID="lblARPercent" Visible="false"></asp:Label> </td>
                                       <td>&nbsp;</td>
                                    </tr>
                                    
                                </table>
                                 </div>
                                
                                   <table style="width: 100%; font-size: 14px; "  cellpadding="0" cellspacing="10">
                                            <colgroup>
                                                <col width="250px" />
                                                <col width="170px" />
                                                <col width="200px" />
                                                <col width="80px"/>
                                                <col />
                                            </colgroup>
                                            <div style="overflow-x: hidden; overflow-y: hidden; width: 100%;">
                                                <asp:Repeater runat="server" ID="rptAPRData">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                &nbsp;<asp:Label ID="lblAPRName" Text='<%#Eval("Name") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%--<asp:Label ID="lblAPR_TotScore" Text='<%#Eval("TotalFilled").ToString() + "/" + Eval("TotalQues").ToString() %>' runat="server"></asp:Label>--%>
                                                                <asp:Label ID="lblAPR_TotScore" Text='<%# Eval("CompletedOn").ToString()== "" ? "" : "Completed On " + Eval("CompletedOn").ToString()  %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td style="border: solid 1px gray">
                                                                <div style="width: <%#Eval("FilledPercent").ToString() + "%" %> ; background-color:#51B751;">
                                                                    &nbsp;</div>
                                                            </td>
                                                            <td>
                                                                &nbsp;<asp:Label runat="server" ID="lblAPR_Percent" Text='<%#Eval("FilledPercent").ToString() + "%" %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                              <asp:LinkButton ID="lbAPR_Start" Font-Size="12px" Font-Bold="true" Text='<%# (Eval("Status").ToString() == "2" && EmpID.ToString() == Eval("AppraiserByUser").ToString()) ? "Start" : "View"  %>' CommandArgument='<%#Eval("AppraiserByUser") %>' runat="server" onclick="lbAPR_Start_Click"/>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </table>
                               <div id="divMDRPending" runat="server" visible="false" style="border:dotted 1px #295E93; background-color:#F0FFFF;">
                                <table style="width:100%; font-size:14px;"  cellpadding="0" cellspacing="10" >
                                <colgroup>
                                <col width="250px" />
                                <col width="170px"/>
                                <col width="200px" />
                                <col width="80px"/>
                                <col />
                                </colgroup>
                                <tr id="trMDRPending" runat="server" visible="false">
                                        <td>&nbsp;<b>3. Management FeedBack</b></td>
                                        <td><asp:Label ID="lblTotMangrs" runat="server"></asp:Label></td>
                                        <td style="border:solid 1px gray"><div id="divMFPercent" runat="server" style="background-color:#51B751;">&nbsp;</div></td>
                                        <td>&nbsp;<asp:Label runat="server" ID="lblMFPercent" ></asp:Label> </td>
                                        <td>&nbsp;</td>
                                 </tr>
                                </table>
                               </div> 
                                   <table style="width: 100%; font-size: 14px; "  cellpadding="0" cellspacing="10">
                                            <colgroup>
                                                <col width="250px" />
                                                <col width="170px" />
                                                <col width="200px" />
                                                <col width="80px"/>
                                                <col />
                                            </colgroup>
                                            <div style="overflow-x: hidden; overflow-y: hidden; width: 100%;">
                                                <asp:Repeater runat="server" ID="rptManagementData">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                &nbsp;<asp:Label ID="lblManagerName" Text='<%#Eval("Name") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%--<asp:Label ID="lblAPR_TotScore" Text='<%#Eval("TotalFilled").ToString() + "/" + Eval("TotalQues").ToString() %>' runat="server"></asp:Label>--%>
                                                                <asp:Label ID="lblMF_TotScore" Text='<%# Eval("CompletedOn").ToString()== "" ? "" : "Completed On " + Eval("CompletedOn").ToString()  %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td style="border: solid 1px gray">
                                                                <div style="width: <%#Eval("FilledPercent").ToString() + "%" %> ; background-color:#51B751;">
                                                                    &nbsp;</div>
                                                            </td>
                                                            <td>
                                                                &nbsp;<asp:Label runat="server" ID="lblManager_Percent" Text='<%#Eval("FilledPercent").ToString() + "%" %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                              <asp:LinkButton ID="lbMFB_Start" Font-Size="12px" Font-Bold="true" Text='<%# (Eval("Status").ToString() == "5" && EmpID.ToString() == Eval("ManagerId").ToString()) ? "Start" : "View"  %>' CommandArgument='<%#Eval("ManagerId") %>' runat="server" onclick="lbMFB_Start_Click"/>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </table>
                                
                            </td>
                        </tr>
                        </table>
                        
                       
                        </td>
                        </tr>
                        </table>
                       
                </tr>
                </table>      
    </div>  
    </form>
</body>
</html>
