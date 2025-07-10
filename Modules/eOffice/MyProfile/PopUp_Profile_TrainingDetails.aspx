<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUp_Profile_TrainingDetails.aspx.cs" Inherits="emtm_Profile_Emtm_PopUp_Profile_TrainingDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../StaffAdmin/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function RefreshParent() {
            window.opener.Refresh();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
              <table width="100%">
                    <tr>
                        <td valign="top" style="height:425px;" >
                        <table cellpadding="3" cellspacing="0" width="100%" border="0">
                        <tr>
                        <td colspan="6" style=" height:25px; padding-left:20px; font-size:15px; background-color:#d2d2d2;" align="center">
                            <asp:Label ID="lblTrainingName" style="font-size:15px; font-style:italic; font-weight:bold;" runat="server"></asp:Label>
                        </td>
                        </tr>
                        <tr id="trPlanningDesc" runat="server" >
                            <td colspan="6" style="height: 20px; padding-left: 20px; font-size: 12px; font-weight: bold;
                                background-color: #F2F2F6;" align="left">
                                Planning Details :
                            </td>
                        </tr>
                        <tr>
                            <td align="right"><b>Start Date &amp; Time :</b>&nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lblStartDt" runat="server"></asp:Label>
                                <asp:Label ID="lblStartTime" runat="server"></asp:Label>
                                &nbsp; (Hrs.)
                            </td>
                            <td align="right"><b>End Date &amp; Time :</b>&nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lblEndDt" runat="server"></asp:Label>
                                <asp:Label ID="lblEndTime" runat="server"></asp:Label>
                                &nbsp; (Hrs.)</td>
                           <td align="right"><b>Training Location :</b>&nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trTrainingStatus" runat="server">
                            <td  align="right"><b>Status :</b>&nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lblStatus" runat="server"></asp:Label> 
                            </td>
                            <td align="right"><b><asp:Label ID="lblCancelBy" Text="Cancelled By :" runat="server" Visible="false"></asp:Label></b></td>
                            <td align="left">
                                <asp:Label ID="lblCancelledBy" runat="server" Visible="false"></asp:Label>
                            </td>
                           <td align="right"><b><asp:Label ID="lblCancelOn" Text="Cancelled On :" runat="server" Visible="false" ></asp:Label></b></td>
                            <td align="left">
                                <asp:Label ID="lblCancelledOn" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trCanRemarks" runat="server" visible="false">
                            <td align="right"><b>Remarks :</b>&nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lblremarks" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">&nbsp;</td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="right" colspan="4">&nbsp;
                                <asp:HiddenField ID="hfTrainingId" runat="server" />
                                &nbsp;&nbsp;&nbsp;</td>
                        </tr>
                                
                                <tr id="divComplationDetails" runat="server" visible="false">
                                    <td align="left" colspan="6" 
                                        style=" height:20px; padding-left:20px; font-size:12px; font-weight:bold; background-color:#F2F2F6;">
                                        Training Status : Done
                                    </td>
                                </tr>
                                <tr id="divComplationDetails1" runat="server" visible="false">
                                    <td align="right">
                                        <b>Start Date &amp; Time :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTSDT" runat="server"></asp:Label>
                                        (Hrs.)
                                    </td>
                                    <td align="right">
                                        <b>End Date &amp; Time :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTEDT" runat="server"></asp:Label>
                                        (Hrs.)
                                    </td>
                                    <td align="right">
                                        <b>Training Location :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTLocation" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="divComplationDetails2" runat="server" visible="false">
                                    <td align="right">
                                        <b>Cost :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTCost" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <b>Currency :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTCurr" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <b>Exchange Rate :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTExcRate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="divComplationDetails3" runat="server" visible="false">
                                    <td align="right">
                                        <b>Cost(USD) :</b>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUTUSD" runat="server"></asp:Label>
                                    </td>
                                    <td align="left" colspan="3">
                                    </td>
                                    <td align="left">
                                    </td>
                                </tr>
                                
                            
                        <tr id="trAttendees" runat="server">
                        <td colspan="6" style=" height:25px; padding-left:20px; font-size:15px; background-color:#d2d2d2;" align="center">
                            Attendees List
                        </td>
                        </tr>
                        <tr id="trAttendees1" runat="server">
                          <td colspan="6">
                           <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup>                                
                                    <col style="width:150px;" />                                     
                                    <col  />
                                    <col style="width:150px;" />                                    
                                    <col style="width:150px;" />
                                    <col style="width:150px;" />
                                    <col style="width:17px;" />
                                    <tr align="left" class="blueheader">                                  
                                        <td>EmpCode</td>
                                        <td>Emp Name</td>
                                        <td>Source</td>
                                        <td>Position</td>
                                        <td>Department
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>
                            <div id="dvEmp" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 250px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>                                
                                   <col style="width:150px;" />                                     
                                    <col  />
                                    <col style="width:150px;" />                                    
                                    <col style="width:150px;" />
                                    <col style="width:150px;" />
                                    <col style="width:17px;" />
                               </colgroup>
                                <asp:Repeater ID="rptEmpDetails" runat="server">
                                    <ItemTemplate>
                                          <tr>
                                            <td align="left">
                                                <%#Eval("EmpCode")%>
                                            </td>
                                            
                                            <td align="left">
                                                <%#Eval("EmpName")%>
                                            </td>
                                            <td align="left">
                                                <%#Eval("Source")%>
                                            </td>
                                             
                                            <td align="left">
                                                <%#Eval("PositionName")%>
                                                </td>
                                            <td align="left">
                                                <%#Eval("DepartmentName")%></td>
                                            <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>
                          
                          </td>
                        </tr>
                        </table>
                        </td>
                    </tr>
            </table>
            </ContentTemplate>
        </asp:UpdatePanel>         
    </div>
    </form>
</body>
</html>
