<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostPoneJobs.aspx.cs" Inherits="Reports_PostPoneJobs" %>
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
        function opendetails(tr) {
            var RP = tr.getAttribute('act');
            var HID = tr.getAttribute('hid');
            var VC = tr.getAttribute('vsl');
            if (RP == 'R') {
                RP = 'R';
                window.open('JobCard.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
            }
            if (RP == 'P') {
                RP = 'P';
                window.open('PopupHistoryJobDetails.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
            }

        }
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="text-align :center; padding:7px; font-size:14px; " class="orangeheader" >
           <b>PMS - Postpone Jobs Approval</b>
     </div>
     
                    <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                        <tr style="background-color :#F2F2F2" >
                            <td>
                                <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server" style="float:right;" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                        <table cellpadding="2" cellspacing="0" width="100%" border="0" rules="cols" >
                        <tr style=" background-color :#F2F2F2" >
                        <td style="text-align:left"  >
                        <table  cellpadding="0" cellspacing="2" width="100%">
                            <tr>
                               <td style="text-align:left; padding-left:2px">Code : </td>
                               <td style="text-align:left"><asp:TextBox ID="txtCompCode" MaxLength="15" runat="server" ></asp:TextBox></td>
                               <td style="text-align:left">Postpone
                                        From :</td>
                               <td style="text-align:left">
                                        <asp:TextBox ID="txtFromDt" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
                                        To : 
                                        <asp:TextBox ID="txtToDt" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
                                    </td>
                           </tr>
                           <tr>
                               <td style="text-align:left; padding-left:2px" >History Id : </td>
                               <td style="text-align:left"><asp:TextBox ID="txtHistoryId" MaxLength="50" 
                                       runat="server" ></asp:TextBox></td>
                               <td style="text-align:left">Status :
                                    </td>
                               <td style="text-align:left">
                                        <asp:DropDownList ID="ddlJobStatus"  runat="server" >
                                            <asp:ListItem Text="< All >" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Requested" Value="1"  Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Rejected" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Apporved" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                           </tr>
                        </table>
                        </td>                       
                        
                        <td>
                            <asp:Button ID="btnViewReport" CssClass="btnorange" Text="Show Report" runat="server" Width="100px" onclick="btnViewReport_Click" />
                            <br />
                            <asp:Label runat="server" ID="lblCount"></asp:Label>
                        </td>
                        </tr>
                        </table>
                        </td>
                        </tr>
                          </table>
     
    
    <div>
    <div style="background-color:#4E4E4E; color:White; padding-right:17px;">
             <table cellspacing="0" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:30px;" />
                        <col style="width:100px;" />
                        <col />
                        <col style="width:250px;" />
                        <col style="width:60px;" />
                        <col style="width:150px;" />
                        <col style="width:150px;" />
                        <col style="width:180px;" />
                        <col style="width:17px;" />                        
                        <tr align="left">
                            <td style="text-align:center"> <img src="Images/magnifier.png" /></td>
                            <td>Comp.Code</td>
                            <td>Component Name</td>
                            <td>Job Name</td>
                            <td>Interval</td>               
                            <td>Requested By / On</td>               
                            <td>Status</td>
                            <td>Approved / Rejected By/On</td>
                            
                        </tr>
                    </colgroup>
                </table>
</div>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 550px ; text-align:center; padding-right:17px;" class="scrollbox">
                           <table cellspacing="0" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:30px;" />
                                    <col style="width:100px;" />
                                    <col />
                                    <col style="width:250px;" />
                                    <col style="width:60px;" />
                                    <col style="width:150px;" />
                                    <col style="width:150px;" />
                                    <col style="width:180px;" />
                                </colgroup>
                                <asp:Repeater ID="rptComponentUnits" runat="server">
                                    <ItemTemplate>
                                            <tr  >
                                            <td><img onclick="opendetails(this);" style="cursor:pointer" src="Images/magnifier.png" hid='<%#Eval("HistoryId")%>' vsl='<%#Eval("VesselCode")%>' act='P'/></td>
                                            <td align="left"><%#Eval("ComponentCode")%></td>
                                            <td align="left"><%#Eval("ComponentName")%>
                                            <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                                            </td>
                                            <td align="left"><b><%#Eval("JobCode")%></b>-<%#Eval("DescrSh")%></td>
                                            <td align="center"><%#Eval("Interval")%>-<%#Eval("IntervalName")%></td>
                                            <td align="left"><%#Eval("P_DoneBy_Code")%>-<%#Eval("P_DoneBy_Name")%><br /> <%#Common.ToDateString(Eval("RequestedOn"))%></td>
                                            <td align="center"><%#Eval("ApprovalStatus")%></td>
                                            <td style="text-align:left">
                                                <%#Eval("AppRejBy")%> / <%#DateString(Eval("AppRejOn"))%>
                                            </td>
                                            </td>
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
