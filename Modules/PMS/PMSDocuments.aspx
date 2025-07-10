<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMSDocuments.aspx.cs" Inherits="Reports_PMSDocuments" %>
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
            if (RP == 'Report') {
                RP = 'R';
                window.open('JobCard.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
            }
            if (RP == 'Postponed') {
                RP = 'P';
                window.open('PopupHistoryJobDetails.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader" visible="false" >
           <b>PMS - JOB Attachments</b>
     </div>
     
                    <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;" >
                        <tr background-color :#F2F2F2" >
                            
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
                           </tr>
                           <tr>
                               <td style="text-align:left; padding-left:2px" >History Id : </td>
                               <td style="text-align:left"><asp:TextBox ID="txtHistoryId" MaxLength="50" 
                                       runat="server" ></asp:TextBox></td>
                           </tr>
                        </table>
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
                                        To : 
                                        <asp:TextBox ID="txtToDt" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlJobStatus"  runat="server" >
                                            <asp:ListItem Text="< All >" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Pending" Value="1"  Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Exported" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
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
                        <col style="width:30px;" />
                        <col style="width:100px;" />
                        <col />
                        <col style="width:250px;" />
                        <col style="width:60px;" />
                        <col style="width:150px;" />
                        <col style="width:150px;" />
                        <col style="width:180px;" />
                        <col style="width:180px;" />
                        <col style="width:30px;" />
                        <col style="width:17px;" />                        
                        <tr align="left" class= "headerstylegrid">
                            <td> <img src="Images/magnifier.png" /></td>
                            <td>Comp.Code</td>
                            <td>Component Name</td>
                            <td>Job Name</td>
                            <td>Interval</td>               
                            <td>Done By</td>               
                            <td>Done Date & Hrs </td>
                            <td>Verified By/On</td>
                            <td>Attachment Exported By/On</td>
                            <td></td>
                        </tr>
                    </colgroup>
                </table>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 550px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:30px;" />
                                    <col style="width:100px;" />
                                    <col />
                                    <col style="width:250px;" />
                                    <col style="width:60px;" />
                                    <col style="width:150px;" />
                                    <col style="width:150px;" />
                                    <col style="width:180px;" />
                                    <col style="width:180px;" />
                                    <col style="width:30px;" />
                                    <col style="width:17px;" /> 
                                </colgroup>
                                <asp:Repeater ID="rptComponentUnits" runat="server">
                                    <ItemTemplate>
                                            <tr  >
                                            <td><img onclick="opendetails(this);" src="Images/magnifier.png" hid='<%#Eval("HistoryId")%>' vsl='<%#Eval("VesselCode")%>' act='<%#Eval("Action")%>'/></td>
                                            <td align="left"><%#Eval("ComponentCode")%></td>
                                            <td align="left"><%#Eval("ComponentName")%>
                                            <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                                            </td>
                                            <td align="left"><b><%#Eval("JobCode")%></b>-<%#Eval("DescrSh")%></td>
                                            <td align="center"><%#Eval("Interval")%>-<%#Eval("IntervalName")%></td>
                                            <td align="left"><%#Eval("DoneBy_Code")%>-<%#Eval("DoneBy_Name")%></td>
                                            <td align="center"><%#Common.ToDateString(Eval("DoneDate"))%> / <%#Eval("DoneHour")%></td>
                                            <td style="text-align:left">
                                                <%#Eval("VerifiedBy")%> / <%#DateString(Eval("VerifiedOn"))%>
                                            </td>
                                            <td style="text-align:left">
                                                <%#Eval("AttachmentCreatedBy")%> / <%#DateString(Eval("AttachmentCreatedOn"))%>
                                                    <span style="color:red" runat="server" visible='<%#(Eval("Verified_Off").ToString()=="False") && (!(Convert.IsDBNull(Eval("Verifiedon_Off"))))  %>'><i>Please read office comments</i></span>
                                            </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="btnExport" OnClick="btnExport_Click"  ImageUrl="~/Images/next.png" CommandArgument='<%#Eval("HistoryId")%>' compcode='<%#Eval("ComponentCode")%>' donedate='<%#Common.ToDateString(Eval("DoneDate"))%>'/>
                                                </td>
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
