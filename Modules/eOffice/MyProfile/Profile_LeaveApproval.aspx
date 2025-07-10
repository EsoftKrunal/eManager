<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_LeaveApproval.aspx.cs" Inherits="emtm_Emtm_Profile_LeaveApproval" EnableEventValidation="false" %>

<%@ Register src="Profile_LeavesHeader.ascx" tagname="Profile_LeavesHeader" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Travel Document</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/ecmascript">
        function PopUPWindow(obj) {
            winref = window.open('../MyProfile/PopUpLeaveApproval.aspx?LeaveTypeId=' + obj, '', 'title=no,toolbars=no,scrollbars=yes,width=760,height=300,left=250,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }
        function PopUPPrintWindow(obj, Mode) {
            winref = window.open('../MyProfile/Profile_LeaveRequestReport.aspx?LeaveTypeId=' + obj + '&Mode=' + Mode, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }
    </script>       
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Button ID="btnhdn" runat="server" onclick="btnhdn_Click" style="display:none;"/>
             <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                            Leave Management : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                           </div>
                        <div class="dottedscrollbox">
                            <uc2:Profile_LeavesHeader ID="Emtm_Profile_LeavesHeader1" runat="server" />
                        </div>
                        <div id="divTraveldocument" runat="server" style="padding:5px 5px 5px 5px;" >
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:25px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;"/>
                                    <col />
                                    <col style="width:80px;"/>
                                    <col style="width:80px;"/>
                                    <col style="width:180px;"/>
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:60px;"/>
                                    <col style="width:110px;"/>
                                    <col style="width:17px;"/>
                                    <tr align="left" class="blueheader">
                                        <td></td>
                                        <td>Action</td>
                                        <td>EmpCode</td>
                                        <td>Name</td>
                                        <td>Office</td>
                                        <td>Dept.</td>
                                        <td>Leave Type </td>
                                        <td>FromDate </td>
                                        <td>ToDate</td>
                                        <td>Duration</td>
                                        <td>Status</td>
                                        <td></td>
                                    </tr>
                                </colgroup>
                            </table>      
                        <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 390px; text-align:center;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                    <col style="width:25px;" />
                                    <col style="width:60px;" />
                                    <col style="width:60px;"/>
                                    <col />
                                    <col style="width:80px;"/>
                                    <col style="width:80px;"/>
                                    <col style="width:180px;"/>
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:60px;"/>
                                    <col style="width:110px;"/>
                                    <col style="width:17px;"/>
                            </colgroup>
                            <asp:Repeater ID="RptLeaveRequest" runat="server">
                                <ItemTemplate>
                                    <tr class='<%# (Common.CastAsInt32(Eval("LeaveRequestId"))==SelectedId)?"selectedrow":"row"%>'>
                                        <td align="center">
                                            <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/print_16.png" OnClick="btnPrint_Click" ToolTip="Print"/>
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btnLeaveApprove" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/hourglass.gif" OnClick="btnLeaveApprove_Click" ToolTip="Approve" Visible='<%#(Eval("StatusCode").ToString()=="V") || (Eval("StatusCode").ToString()=="W")%>'/>&nbsp;
                                        </td>
                                        <td align="left">
                                            <%#Eval("EmpCode")%></td>
                                        <td align="left">
                                            <%#Eval("Name")%></td>
                                        <td align="left">
                                            <%#Eval("officename")%></td>
                                        <td align="left">
                                            <%#Eval("DeptName")%></td>
                                        <td align="left">
                                            <%#Eval("LeaveTypeName")%></td>                
                                        <td align="left">
                                            <%#Eval("LeaveFrom")%></td>
                                        <td align="center">
                                            <%#Eval("LeaveTo")%></td>
                                        <td align="left">
                                            <%#Eval("Duration")%></td>    
                                        <td align="center" class='Status_<%#Eval("StatusCode")%>'>
                                            <%#Eval("Status")%></td>
                                        <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>    
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                         </table>
                        </div> 
                        <br /> 
                        </div> 
                    </td>
                </tr>
         </table> 
         </ContentTemplate>
      </asp:UpdatePanel> 
    </div>
    </form>
</body>
</html>
