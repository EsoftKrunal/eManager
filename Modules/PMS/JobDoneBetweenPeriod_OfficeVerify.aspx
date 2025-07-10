<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobDoneBetweenPeriod_OfficeVerify.aspx.cs" Inherits="JobDoneBetweenPeriod_OfficeVerify" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script src="JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
       function opendetails(tr)
       {
         var RP = tr.getAttribute('act');
         var HID = tr.getAttribute('hid');
         var VC = tr.getAttribute('vsl');
         if(RP == 'Report')
         {
           RP = 'R';
           window.open('JobCard_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
           
         }
         if(RP == 'Postponed')
         {
            RP = 'P';
            window.open('PopupHistoryJobDetails_Office.aspx?VC='+ VC +'&&HID='+ HID + '&&RP='+ RP,'','');
         }
          
       }
       
       function ReloadPage()
       {
            document.getElementById('btnViewReport').click();
        }
        function ShowComments(Comments) {
            alert(Comments);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader" visible="false" >
           <b>Office - Maintenance Jobs Remarks </b>
     </div>
    <%--<table border="0" cellpadding="2" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
    <tr>
       <td colspan="6" style="text-align: left; padding-left :100px;">
       <span id="tdVessel" runat="server">
       Select Vessel :&nbsp;<asp:DropDownList ID="ddlVessels" runat="server" ></asp:DropDownList>
       </span>
       </td>
    </tr>
    <tr>
       <td colspan="6" style="text-align: left; padding-left :100px;">
       <table cellpadding="2" cellspacing="0" width="100%">
       <tr>
       <td style="width:82px; text-align :right">Period :</td>
       <td style="width:300px; text-align :left">
            <asp:TextBox ID="txtFromDt" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtToDt" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
            
            
       </td>
       <td style="width:120px; text-align :right">Include Verified :</td>
       <td>
          <asp:CheckBox ID="chkVerified" runat="server" />
       </td>
       
    </tr>
       <tr>
       <td style="width:82px; text-align :right">&nbsp;</td>
       <td >
    <asp:Button ID="btnViewReport" CssClass="btnorange" Text="Show Report" runat="server" Width="100px" onclick="btnViewReport_Click" />
    <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label>
       </td>
       <td >&nbsp;</td>
    </tr>
    </table>
    </td>
    </tr>
    </table>--%>
    <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid;  " >
                        <tr style="background-color :#F2F2F2" >
                            <td>
                                <asp:Label ID="lblVessel" ForeColor="Red" Font-Size="12px" Font-Bold="true" runat="server" style="margin:2px; margin-left:5px;" Width="40%"></asp:Label>
                                <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server" style="float:left;" Width="40%"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                        <table cellpadding="2" cellspacing="0" width="100%" border="1" rules="all" >
                        <tr style=" background-color :#F2F2F2" >
                        <td style="text-align:left"  >
                        <table  cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                               <td style="text-align:left; padding-left:2px">Code : </td>
                               <td style="text-align:left"><asp:TextBox ID="txtCompCode" MaxLength="15" runat="server" ></asp:TextBox></td>
                           </tr>
                           <tr>
                               <td style="text-align:left; padding-left:2px" >Name : </td>
                               <td style="text-align:left"><asp:TextBox ID="txtCompName" MaxLength="50" runat="server" ></asp:TextBox></td>
                           </tr>
                        </table>
                           <asp:CheckBox Text="Class Equip. Code" ID="chkClass" runat="server"/> 
                        <asp:TextBox runat="server" ID="txtClassCode" Width="75px" MaxLength="20" ></asp:TextBox> 
                        </td>                       
                        
                        <td style="text-align:left" >
                            <table cellpadding="2" cellspacing="0" width="100%" border="0" >
                                <col width="80px" />
                                <col />
                                <tr>
                                    <td>
                                        From :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFromDt" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        To : &nbsp;
                                        <asp:TextBox ID="txtToDt" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--Job Status :--%>
                                    Type :</td>
                                    <td>
                                        <%--<asp:DropDownList ID="ddlJobStatus"  runat="server" >
                                            <asp:ListItem Text="< All >" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Verified" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Not Verified" Value="2" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>--%>
                                                                    
                                 <asp:DropDownList ID="ddlIntType" runat="server" Width="100px" >
                                    <asp:ListItem Text="< All >" Selected="True" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Calender" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Hour" Value="2"></asp:ListItem>
                                 </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: left">
                                        <asp:CheckBox ID="chkOfficeComments" Text="With office comments" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td style="text-align:left" >
                           <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                <td>
                                    &nbsp;</td>
                               <td style="text-align:left">
                                
                                   &nbsp;</td>
                             </tr>
                             <tr>
                                <td></td>
                              <td style="text-align:left">
                              <asp:CheckBox ID="chkCritical" Text="Critical Jobs" runat="server" />
                              <br />
                              <asp:CheckBox ID="chkPostPone" Text="Postpone" runat="server" />
                              </td>                        
                           </tr>
                           </table>
                        </td>
                        <td style="text-align:center">
                        <table >
                        </table>
                        </td>
                        <td>
                            <asp:Button ID="btnViewReport" CssClass="btnorange" Text="Show Report" runat="server" Width="100px" onclick="btnViewReport_Click" />
                            <br /><br />
                            <asp:Label runat="server" ID="lblCount"></asp:Label>
                        </td>
                        </tr>
                        </table>
                        </td>
                        </tr>
                          </table>
    
    <div>
    <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:60px;" />
                        <col style="width:100px;" />
                        <col />
                        <col style="width:250px;" />
                        <col style="width:60px;" />
                        <col style="width:150px;" />
                        <col style="width:150px;" />
                        <col style="width:30px;" />
                        <col style="width:180px;" />
                        <col style="width:17px;" />                        
                        <tr align="left" class= "headerstylegrid">
                            <td>Vessel</td>
                            <td>Comp.Code</td>
                            <td>Component Name</td>
                            <td>Job Name</td>
                            <td>Interval</td>               
                            <td>Done By</td>               
                            <td>Done Date & Hrs </td>
                            <td> <img src="Images/magnifier.png" /></td>
                            <td>Office-Comments</td>
                            <td></td>
                        </tr>
                    </colgroup>
                </table>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 350px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:60px;" />
                                   <col style="width:100px;" />
                                    <col />
                                    <col style="width:250px;" />
                                    <col style="width:60px;" />
                                    <col style="width:150px;" />
                                    <col style="width:150px;" />
                                    <col style="width:30px;" />
                                    <col style="width:180px;" />
                                    <col style="width:17px;" /> 
                                </colgroup>
                                <asp:Repeater ID="rptComponentUnits" runat="server">
                                    <ItemTemplate>
                                            <tr  >
                                            <td align="center"><%#Eval("VesselCode")%></td>
                                            <td align="left"><%#Eval("ComponentCode")%></td>
                                            <td align="left"><%#Eval("ComponentName")%></td>
                                            <td align="left"><b><%#Eval("JobCode")%></b>-<%#Eval("DescrSh")%></td>
                                            <td align="center"><%#Eval("Interval")%>-<%#Eval("IntervalName")%></td>
                                            <td align="left"><%#Eval("DoneBy_Code")%>-<%#Eval("DoneBy_Name")%></td>
                                            <td align="center"><%#Common.ToDateString(Eval("DoneDate"))%> / <%#Eval("DoneHour")%></td>
                                            <td><img onclick="opendetails(this);" src="Images/magnifier.png" hid='<%#Eval("HistoryId")%>' vsl='<%#Eval("VesselCode")%>' act='<%#Eval("Action")%>'/></td>
                                            <td style="text-align:left">
                                            
                                            <%--<asp:CheckBox runat="server" Visible='<%#Eval("Verified_OFF").ToString()=="False"%>' ID="chkVerifed" vsl='<%#Eval("VesselCode")%>' historyid='<%#Eval("HistoryId")%>' AutoPostBack="true" OnCheckedChanged="chkVerifed_OnCheckedChanged"/>--%>
                                            <%--<asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_OnClick" Visible='<%#Eval("Verified_OFF").ToString()=="False"%>' vsl='<%#Eval("VesselCode")%>' historyid='<%#Eval("HistoryId")%>' CompName='<%#Eval("ComponentName")%>' Text="Verify" CssClass="btn" />--%>
                                            
                                            <%#Eval("VerifiedBy_OFF")%> / <%#DateString(Eval("VerifiedOn_OFF"))%>
                                            
                                            <img src="Images/icon_comment.gif" onclick="ShowComments('<%#Eval("Comments")%>')" style='float:right;padding-right:5px; display:<%#((Eval("Verified_OFF").ToString()=="True")?"block":"none") %>' /></td>
                                            <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                           </tr>
                                    </ItemTemplate> 
                                </asp:Repeater>
                            </table>
                           </div>
    </div>
    </div>
    </form>
    </body>
</html>
