<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_PositionMaster.aspx.cs" Inherits="StaffAdmin_Hr_PositionMaster" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>



<%@ Register src="~/Modules/eOffice/StaffAdmin/HR_LeaveSearchHeader.ascx" tagname="HR_LeaveSearchHeader" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
   <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/ecmascript">
        function PopUPWindow(obj, Mode) {
            var id = obj.getAttribute("RowId");
            winref = window.open('../StaffAdmin/PopupPositionMaster.aspx?PositionId=' + id + ' &Mode=' + Mode, '', 'title=no,toolbars=no,scrollbars=yes,width=720,height=230,left=250,top=150,addressbar=no,resizable=1,status=0');
            return false;
         }

         function PopUPWindowAdd(obj, Mode, Office) {
             //var id = obj.getAttribute("RowId");
             winref = window.open('../StaffAdmin/PopupPositionMaster.aspx?PositionId=' + obj + ' &Mode=' + Mode + ' &Office=' + Office, '', 'title=no,toolbars=no,scrollbars=yes,width=720,height=230,left=250,top=150,addressbar=no,resizable=1,status=0');
             return false;
          }
    </script>

    <div style="font-family:Arial;font-size:12px;">
       
        <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
        <table width="100%">
            <tr>
               
                <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                    <div class="text headerband" style=" text-align :center; font-size :14px;padding :3px; font-weight: bold;">
                        Position Master 
                    </div>
                    <div class="dottedscrollbox">
                         <uc2:HR_LeaveSearchHeader ID="Emtm_HR_LeaveSearchHeader1" runat="server" />
                    </div>
                        <table width="50%" cellpadding="10px" cellspacing ="0" border="0">
                           <tr>
                               <td style="text-align :right">
                                   Office :</td>
                               <td style="text-align :left">
                                   <asp:DropDownList ID="ddlOffice" runat="server" Width="120px" 
                                       AutoPostBack="true" 
                                       onselectedindexchanged="ddlOffice_SelectedIndexChanged" Height="20px">
                                   </asp:DropDownList>
                               </td>
                               <td style="text-align :left">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOffice" ErrorMessage="Required."></asp:RequiredFieldValidator>
                               </td>
                               <td style="text-align :right">
                                   </td>
                               <td style="text-align :left"></td>
                           </tr>
                           </table>
                          
                        <div>
                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                            <colgroup>
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col style="width:150px;" />
                                <col style="width:100px;" />
                                <col />
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col style="width:25px;" />
                                <tr align="left" class= "headerstylegrid">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>Office Location</td>
                                    <td>Position Code</td>
                                    <td>Position Name</td>
                                    <td>Manager</td>
                                    <td>Inspector</td>
                                    <td>Vessel Position</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </colgroup>
                   </table>  
                   </div>         
                    <div id="divinfo" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 345px ; text-align:center;">
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <col style="width:150px;" />
                                <col style="width:100px;" />
                                <col />
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col style="width:100px;" />
                                <col style="width:25px;" />
                        </colgroup>
                        <asp:Repeater ID="rptHolidayMaster" runat="server"> 
                            <ItemTemplate>
                                <tr class='<%# (Common.CastAsInt32(Eval("PositionId"))==SelectedId)?"selectedrow":"row"%>'>
                                    <td align="center">
                                        <asp:ImageButton ID="btnView" runat="server" CausesValidation="false"  
                                            CommandArgument='<%# Eval("PositionId") %>' 
                                            ImageUrl="~/Modules/HRD/Images/HourGlass.gif"  
                                            OnClientClick="return PopUPWindow(this,'View');"
                                            RowId='<%# Eval("PositionId") %>' ToolTip="View" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton ID="btnedit" runat="server" CausesValidation="false"  
                                            CommandArgument='<%# Eval("PositionId") %>' 
                                            ImageUrl="~/Modules/HRD/Images/edit.jpg"  
                                            OnClientClick="return PopUPWindow(this,'Edit');"
                                            RowId='<%# Eval("PositionId") %>' ToolTip="View" />
                                    </td>
                                    <td align="center">
                                            <asp:ImageButton ID="btndocDelete" runat="server" CausesValidation="false"  
                                            CommandArgument='<%# Eval("PositionId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                            OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  
                                            OnClick="btnDelete_Click" ToolTip="Delete" />
                                    </td>
                                    <td align="left">
                                        &nbsp;<%#Eval("OfficeName")%></td>
                                    <td align="left">
                                        &nbsp;<%#Eval("PositionCode")%></td>
                                    <td align="left">
                                        &nbsp;<%#Eval("PositionName")%></td>
                                    <td align="left">
                                        &nbsp;<%#Eval("ManagerText")%></td>
                                    <td align="left">
                                        &nbsp;<%#Eval("InspectorText")%></td>
                                    <td align="left">
                                        &nbsp;<%#Eval("VesselPositionName")%></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                    </div> 
</div>
                          <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                            <tr>
                               <td align="right" style="display:none;">
                                <asp:Button ID="btnhdn" runat="server" onclick="btnhdn_Click" CausesValidation="false"/> 
                               </td>
                                <td align="right">
                                <asp:Button ID="btnAddNew" CssClass="btn" Width="100px" runat="server" Text="Add New" onclick="btnAddNew_Click" CausesValidation="false">
                                </asp:Button>
                                 </td>
                            </tr>
                            </table>  
                </td>
            </tr>
          </table> 
        </ContentTemplate>
        </asp:UpdatePanel> 
     </div>       
   </asp:Content>
