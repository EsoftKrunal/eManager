<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_LeaveRegister.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_LeaveRegister" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>


<%@ Register src="HR_LeaveSearchHeader.ascx" tagname="HR_LeaveSearchHeader" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/ecmascript">
     function PopUPWindow(obj)
         {
             winref = window.open('../StaffAdmin/Hr_AddLeaveType.aspx?LeaveTypeId=' + obj,'','title=no,toolbars=no,scrollbars=yes,width=400,height=135,left=250,top=150,addressbar=no,resizable=1,status=0');
             return false;
         }
    </script>       

    <div style="font-family:Arial;font-size:12px;">
    
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate >
             <table width="100%">
                <tr>
                   
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="text headerband" style=" text-align :center; font-size :14px; padding :3px; font-weight: bold;">
                            Leave Register
                        </div>
                        <div class="dottedscrollbox">
                            <uc2:HR_LeaveSearchHeader ID="Emtm_HR_LeaveSearchHeader1" runat="server" />
                        </div>
                        <div>
                        
                        <table width="100%" cellpadding="2" cellspacing="0" rules="all" style="width:100%; border-collapse:collapse;">
                            <tr>
                                <td style="width : 400px;vertical-align :top;" >
                                <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                        <colgroup>
                                            <col style="width:25px;" />
                                            <col style="width:25px;" />
                                            <col style="width:25px;" />
                                            <col />
                                            <col style="width:25px;" />
                                            <tr align="left" class= "headerstylegrid">
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td>Leave Type</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </colgroup>
                                    </table>      
                                    </div>
                                    
                                    <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 375px; text-align:center;">
                                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                            <col style="width:25px;" />
                                            <col style="width:25px;" />
                                            <col style="width:25px;" />
                                            <col />
                                            <col style="width:25px;" />
                                    </colgroup>
                                    <asp:Repeater ID="RptLeaveType" runat="server">
                                        <ItemTemplate>
                                            <tr class='<%# (Common.CastAsInt32(Eval("LeaveTypeId"))==SelectedId)?"selectedrow":"row"%>'>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnLeaveView" runat="server" CausesValidation="false" 
                                                        CommandArgument='<%# Eval("LeaveTypeId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" 
                                                        OnClick="btnLeaveView_Click" ToolTip="View" />
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnLeaveEdit" runat="server" CausesValidation="false" 
                                                        CommandArgument='<%# Eval("LeaveTypeId") %>' ImageUrl="~/Modules/HRD/Images/edit.jpg" 
                                                        OnClick="btnLeaveEdit_Click" ToolTip="Edit" />
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="btnLeaveDelete" runat="server" CausesValidation="false" Visible='<%# Common.CastAsInt32(Eval("LeaveTypeId"))> 2%>'  
                                                        CommandArgument='<%# Eval("LeaveTypeId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg"  OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  
                                                        OnClick="btnLeaveDelete_Click" ToolTip="Edit" />
                                                </td>
                                                <td align="left">
                                                    <%#Eval("LeaveTypeName")%></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                   </table>
                                    </div>
                                
                                   
                                </td>
                                <td style =" vertical-align :top">
                                    <table width="300" cellpadding="3" cellspacing="0" rules="all" style="border-collapse:collapse;">
                                    <tr>
                                    <td colspan="3" style=" text-align :center; padding :3px; font-weight: bold;" class="text headerband">
                                        Office-Wise Leave Mapping
                                    </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="text-align :center"> Select Office :
                                        <asp:DropDownList ID="ddlOffice" runat="server" onselectedindexchanged="ddlOffice_SelectedIndexChanged" AutoPostBack="true" required='yes'></asp:DropDownList></td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align :top;text-align :right">
                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:200px;border-collapse:collapse; height:26px;">
                                        <colgroup>
                                            <col style="width:25px;" />
                                            <col />
                                            <col style="width:25px;" />
                                            <tr align="left" class= "headerstylegrid">
                                                <td></td>
                                                <td>Not Applicable </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </colgroup>
                                    </table>  
                                    </div>
                                    <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; text-align:center; border-bottom:none;">    
                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:200px;border-collapse:collapse;">
                                    <colgroup>
                                            <col style="width:25px;" />
                                            <col />
                                            <col style="width:25px;" />
                                    </colgroup>
                                    <asp:Repeater ID="RptLeaveNotInOffice" runat="server">
                                        <ItemTemplate>
                                            <tr class="row">
                                                <td align="center">
                                                    <asp:CheckBox ID="chkLeaveType" runat="server"/>   
                                                    <asp:HiddenField ID="hdnLeaveTypeid" runat="server" Value='<%#Eval("LeaveTypeId")%>' /> 
                                                </td>
                                                <td align="left">
                                                    <%#Eval("LeaveTypeName")%></td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                   </table>
                                   </div>
                                        </td> 
                                        <td>
                                                <asp:Button id="btnMapToOffice" runat="server" Text=">>" CausesValidation="true" onclick="btnMapToOffice_Click" />
                                                <br /><br />
                                                <asp:Button id="btnUnMapFromOffice" runat="server" Text="<<" CausesValidation="true" onclick="btnUnMapFromOffice_Click" />              
                                        </td>
                                        <td style="vertical-align :top; text-align :left  ">
                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:200px;border-collapse:collapse; height:26px;">
                                            <colgroup>
                                                <col style="width:25px;" />
                                                <col />
                                                <col style="width:25px;" />
                                                <tr align="left" class= "headerstylegrid">
                                                    <td></td>
                                                    <td>Applicable</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>  
                                        </div>
                                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; text-align:center; border-bottom:none;">    
                                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:200px;border-collapse:collapse;">
                                        <colgroup>
                                                <col style="width:25px;" />
                                                <col />
                                                <col style="width:25px;" />
                                        </colgroup>
                                        <asp:Repeater ID="RptLeaveInOffice" runat="server">
                                            <ItemTemplate>
                                                <tr class="row">
                                                    <td align="center">
                                                        <asp:CheckBox ID="chkLeaveType" runat="server"/>   
                                                        <asp:HiddenField ID="hdnLeaveTypeId" runat="server" Value='<%#Eval("LeaveTypeId")%>' /> 
                                                    </td>
                                                    <td align="left">
                                                        <%#Eval("LeaveTypeName")%></td>
                                                    <td>&nbsp;</td>
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
                            <tr>
                            <td>
                             <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                                <tr>
                                    <td align="right" style="display:none;">
                                        <asp:Button ID="btnhdn" runat="server" onclick="btnhdn_Click" /> 
                                    </td>
                                    <td style="text-align :right">
                                            <asp:Button ID="btnAddNew" CssClass="btn"  runat="server" Text="Add Leave Type " CausesValidation="false" onclick="btnAddNew_Click" Width="120px"  ></asp:Button>
                                            <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" onclick="btnsave_Click" Visible="false"></asp:Button>
                                     </td>
                                </tr>
                              </table>  
                            </td>
                            </tr>
                            </table> 
                            </div>  
                           
                            </td>
                            </tr>
                            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        </div> 
    </asp:Content>
