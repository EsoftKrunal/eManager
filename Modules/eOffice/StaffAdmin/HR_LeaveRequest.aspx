<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_LeaveRequest.aspx.cs" Inherits="emtm_Emtm_TravelDocs" EnableEventValidation="false" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>


<%@ Register src="HR_TravelDocumentHeader.ascx" tagname="HR_TravelDocumentHeader" tagprefix="uc2" %>

<%@ Register src="HR_TravelDocument.ascx" tagname="HR_TravelDocument" tagprefix="uc3" %>

<%@ Register src="HR_LeaveSearchHeader.ascx" tagname="HR_LeaveSearchHeader" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    
    
    <script language="javascript" type="text/ecmascript">
        function PopUPWindow(obj) {
             winref = window.open('../StaffAdmin/PopUpLeaveApproval.aspx?LeaveTypeId=' + obj, '', 'title=no,toolbars=no,scrollbars=yes,width=800,height=390,left=250,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }
        
        function PopUPPrintWindow(obj, Mode) {
            winref = window.open('../MyProfile/Profile_LeaveRequestReport.aspx?LeaveTypeId=' + obj + '&Mode=' + Mode, '', 'title=no,toolbars=no,scrollbars=yes,left=150,top=150,addressbar=no,resizable=1,status=0');
            return false;
        }
    </script> 

    <div style="font-family:Arial;font-size:12px;">
       
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Button ID="btnhdn" runat="server" onclick="btnhdn_Click" style="display:none" /> 
             <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="text headerband" style=" text-align :center; font-size :14px;  padding :3px; font-weight: bold;">
                            Leave Request Verification
                        </div>
                        <div class="dottedscrollbox">
                            <uc4:HR_LeaveSearchHeader ID="Emtm_HR_LeaveSearchHeader1" runat="server" />
                        </div>
                        <div>
                            <table width="100%" cellspacing ="0" cellpadding="3" border="0">
                            <tr>
                                <td style ="text-align:right;">
                                    Select Leaves Status :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLeaveStatus" runat="server" AutoPostBack="true"
                                        Width="160px" onselectedindexchanged="ddlLeaveStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align :left;">
                                    
                                </td>
                                </tr>
                            </table>      
                        </div>
                        <div id="divTraveldocument" runat="server" style="padding:5px 5px 5px 5px;" >
                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup>
                                    <col style="width:50px;" />
                                    <col style="width:60px;"/>
                                    <col />
                                    <col style="width:80px;"/>
                                    <col style="width:80px;"/>
                                    <col style="width:170px;"/>
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:60px;"/>
                                    <col style="width:110px;"/>
                                    <col style="width:25px;"/>
                                    <tr align="left" class= "headerstylegrid">
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
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                            </table> 
                            </div>     
                        <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 400px; text-align:center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                    <col style="width:50px;" />
                                    <col style="width:60px;"/>
                                    <col />
                                    <col style="width:80px;"/>
                                    <col style="width:80px;"/>
                                    <col style="width:170px;"/>
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:60px;"/>
                                    <col style="width:110px;"/>
                                    <col style="width:25px;"/>
                            </colgroup>
                            <asp:Repeater ID="RptLeaveRequest" runat="server">
                                <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("LeaveRequestId"))==SelectedId)?"selectedrow":"row"%>'>
                                        <td align="center">
                                            <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnPrint_Click" ToolTip="Print"/>
                                        </td>
                                       <%-- <td align="center">
                                            <asp:ImageButton ID="btnLeaveVerified" runat="server" CausesValidation="false" CommandArgument='<%# Eval("LeaveRequestId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnLeaveVerified_Click" ToolTip="Verify" Visible='<%#(Eval("StatusCode").ToString()=="P") || (Eval("StatusCode").ToString()=="W")%>'/>&nbsp;
                                        </td>--%>
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
                                            <asp:LinkButton ID="LnkStatus" runat="server" CssClass='Status_<%#Eval("StatusCode")%>' Text='<%#Eval("Status")%>' Visible='<%#(Eval("StatusCode").ToString() == "V")%>' CommandArgument='<%#Eval("LeaveRequestId")%>' OnClick="LnkStatus_Click"></asp:LinkButton>      
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>' CssClass='Status_<%#Eval("StatusCode")%>' Visible='<%#(Eval("StatusCode").ToString() != "V")%>'></asp:Label>  
                                         </td>
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
         </ContentTemplate>
      </asp:UpdatePanel> 
    </div>
    </asp:Content>
