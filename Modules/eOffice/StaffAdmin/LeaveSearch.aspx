<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveSearch.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_LeaveSearch" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" Title="EMANAGER" %>


<%@ Register src="HR_LeaveSearchHeader.ascx" tagname="HR_LeaveSearchHeader" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/ecmascript">
        
        function PopUPWindow(obj) {
            var winref = window.open('../StaffAdmin/PopupLeaveAssigned.aspx?Office=' + obj,'','title=no,toolbars=no,scrollbars=yes,width=800,height=680,left=250,top=150,addressbar=no,resizable=1,status=0');
            winref.focus(); 
             //return false;
        }

         function EnterToClick() {
             if (event.keyCode == 13) {
                 var strId = event.srcElement.id;
                 event.returnValue = false;
                 event.cancel = true;

                 //alert(document.getElementById("<%=ddlOffice.ClientID%>"));
                 
                 if (strId == 'txtEmpName') {
                     document.getElementById('btn_Search').click();
                 }
                 else if (strId == '<%=ddlOffice.ClientID%>') {
                    document.getElementById('btn_Search').click();
                }
                else if (strId == 'ddlDept') {
                    document.getElementById('btn_Search').click();
                }
                else if (strId == "") {
                    document.getElementById('btn_Search').click();
                }
             }
         }

         function SetFocus() {
             document.getElementById('btn_Search').focus();
         }
         
     </script>    


    
    <div style="font-family:Arial;font-size:12px;"><%--onkeydown="javascript:EnterToClick();"--%>
    
                <table width="100%">
                    <tr>
                      
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <div class="text headerband" style=" text-align :center; font-size :14px;  padding :3px; font-weight: bold;">
                            Leave Search 
                        </div>
                        <div class="dottedscrollbox">
                            <uc2:HR_LeaveSearchHeader ID="Emtm_HR_LeaveSearchHeader1" runat="server" />
                        </div>
                        <asp:UpdatePanel id="UpdatePanel" runat="server" >
                        <ContentTemplate>
                            <table border="0" cellspacing="0" cellpadding="1"  style="text-align: center; padding-right:50px; height :25px;" width="100%">
                                <tr>
                                    <td style="text-align :right">
                                        Emp. Name :
                                    </td>
                                    <td style="text-align :left">
                                        <asp:TextBox ID="txtEmpName" runat="server" MaxLength="6"
                                            Width="160px" TabIndex="1"></asp:TextBox></td>
                                    <td style="text-align :right">
                                        Office :
                                    </td>
                                    <td style="text-align :left">
                                        <asp:DropDownList ID="ddlOffice" runat="server" Width="160px" AutoPostBack="true"  
                                            onselectedindexchanged="ddlOffice_SelectedIndexChanged" onchange="SetFocus();"></asp:DropDownList>
                                    </td>
                                    <td style="text-align :right">
                                        Department :</td>
                                    <td style="text-align :left">
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="160px"></asp:DropDownList>
                                    </td>
                                    <td style="text-align :right">
                                        Status :</td>
                                    <td style="text-align :left">
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="160px">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Active" Value="A" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                               </tr>
                             </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                            <table cellpadding="0" cellspacing ="0" width="100%">
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="center" colspan="6" style="padding-right: 5px; text-align: right;">
                                            <a style="float:left;font-weight:bold; margin-left :10px; " href="Emtm_HR_LeaveSummaryReport.aspx" target="_blank">Leave Taken Summary</a>
                                            <strong>Total Filterd Records :&nbsp;<asp:Label ID="EmpCount" runat="server" ></asp:Label>&nbsp;</strong>
                                            <asp:Button ID="btn_Search" runat="server" CausesValidation="true" CssClass="btn" OnClick="btn_Search_Click" Text="Search" Width="75px" TabIndex="0" />
                                            <asp:Button ID="btn_Clear" runat="server" CausesValidation="false" CssClass="btn" OnClick="btn_Clear_Click" Text="Clear" Width="75px" TabIndex="20" />
                                            
                                        </td>
                                    </tr>
                              </table>
                            <div id="divTraveldocument" runat="server" style="padding:5px 5px 5px 5px;" >
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 52px ; text-align:center; border-bottom:none;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:52px;">
                                    <colgroup>
                                        <col style="width:25px;" />
                                        <col style="width:25px;" />
                                        <col style="width:80px;" />
                                        <col />
                                        <col style="width:200px;" />
                                        <col style="width:120px;" />
                                          <col style="width:80px;" />
                                        <col style="width:80px;" />
                                        <col style="width:90px;" />
                                        <col style="width:80px;" />
                                        <col style="width:80px;" />
                                        <col style="width:80px;" />
                                        <col style="width:50px;" />
                                        <col style="width:25px;" />
                                        <tr align="left" class="blueheader">
                                            <td></td>
                                            <td></td>
                                            <td colspan="4"></td>
                                            <td style="text-align:center;" colspan="6">Annual Leave Summary</td>                                                                                                
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr align="left" class= "headerstylegrid">
                                            <td></td>
                                            <td></td>
                                            <td>Emp Code</td>
                                            <td>Employee Name</td>
                                            <td>Position</td>
                                            <td>Department</td>
                                            <td style="text-align:center;">Bal<asp:Label ID="lbllastyear" runat="server" Font-Size="Smaller"></asp:Label></td>
                                            <td>AL<asp:Label ID="lblcurryear" runat="server" Font-Size="Smaller"></asp:Label></td>
                                            <td>Total<asp:Label ID="lblcurryear1" runat="server" Font-Size="Smaller"></asp:Label></td>
                                            <td>Taken<asp:Label ID="lblcurryear2" runat="server" Font-Size="Smaller"></asp:Label></td>    
                                            <td>Credit</td>                                                                                              
                                            <td style="text-align:center;">Bal.<asp:Label ID="lblCurrentYear" runat="server" Font-Size="Smaller"></asp:Label></td> 
                                            <td>Status</td>                                                                                               
                                            <td>&nbsp;</td>
                                        </tr>
                                    </colgroup>
                                </table>
                                </div>      
                                
                            <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 338px; text-align:center;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                      <col style="width:25px;" />
                                        <col style="width:25px;" />
                                        <col style="width:80px;" />
                                        <col />
                                        <col style="width:200px;" />
                                        <col style="width:120px;" />
                                          <col style="width:80px;" />
                                        <col style="width:80px;" />
                                        <col style="width:90px;" />
                                        <col style="width:80px;" />
                                        <col style="width:80px;" />
                                        <col style="width:80px;" />
                                        <col style="width:50px;" />
                                        <col style="width:25px;" />
                                </colgroup>
                                <asp:Repeater ID="RptLeaveSearch" runat="server" OnItemDataBound="RptLeaveSearch_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("EmpId"))==SelectedId)?"selectedrow":"row"%>'>
                                            <td align="center">
                                                <asp:ImageButton ID="btndocView" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("EmpId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" 
                                                    OnClick="btndocView_Click" ToolTip="View" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btndocedit" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("EmpId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" 
                                                    OnClick="btndocedit_Click" ToolTip="Edit" />
                                            </td>
                                            <td align="left">
                                                <%#Eval("EMPCODE")%></td>
                                            <td align="left">
                                                <%#Eval("EMPNAME")%></td>
                                            <td align="left">
                                                <%#Eval("PositionName")%></td>
                                            <td align="left">
                                                <%#Eval("Department")%></td>
                                            <td align="center"><asp:Label runat="server" ID="lblPrevBalance"></asp:Label></td>
                                            <td align="center"><asp:Label runat="server" ID="lblAnnualLeave"></asp:Label></td>
                                            <td align="center"><asp:Label runat="server" ID="lblTotal"></asp:Label></td>
                                            <td align="center"><asp:Label runat="server" ID="lblCons"></asp:Label></td>
                                            <td align="center"><asp:Label runat="server" ID="lblCredit"></asp:Label></td>
                                            <td align="center"><asp:Label runat="server" ID="lblCurrBalance"></asp:Label></td>
                                            <td align="center"><%#Eval("Status")%></td>
                                            <td>&nbsp;</td>       
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                               </table>
                            </div> 
                        </td>
                    </tr>
            </table>      
  
     
    </div>
  </asp:Content>
