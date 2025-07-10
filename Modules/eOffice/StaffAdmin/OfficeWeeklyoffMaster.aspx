<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeWeeklyoffMaster.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_OfficeWeeklyoffMaster" MasterPageFile="~/Modules/eOffice/StaffAdmin/StaffAdmin.master" %>


<%@ Register src="HR_LeaveSearchHeader.ascx" tagname="HR_LeaveSearchHeader" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


   
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />

    <div style="font-family:Arial;font-size:12px;">
       
         <table width="100%">
            <tr>
               
                <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="text headerband" style=" text-align :center; font-size :14px; padding :3px; font-weight: bold;">
                            Weekly Off Master 
                        </div>
                        <div class="dottedscrollbox">
                             <uc2:HR_LeaveSearchHeader ID="Emtm_HR_LeaveSearchHeader1" runat="server" />
                        </div>
                         <table width="100%" cellpadding="10px" cellspacing ="0" border="0">
                           <tr>
                               <td style="text-align :right">
                                   Year :</td>
                               <td style="text-align :left">
                                   <asp:DropDownList ID="ddlYear" runat="server" Width="80px" 
                                       AutoPostBack="true" Height="20px" 
                                       onselectedindexchanged="ddlYear_SelectedIndexChanged">
                                   </asp:DropDownList>
                               </td>
                               <td></td>
                               <td></td>
                           </tr>
                           </table>
                        <div style="padding:5px 5px 5px 5px;" >
                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                            <colgroup>
                                <col />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:25px;" />
                                <tr align="left" class= "headerstylegrid">
                                    <td>Offices</td>
                                    <td>Sun</td>
                                    <td>Mon</td>    
                                    <td>Tue</td>    
                                    <td>Wed</td>    
                                    <td>Thr</td>    
                                    <td>Fri</td>    
                                    <td>Sat</td>    
                                    <td>&nbsp;</td>
                                </tr>
                            </colgroup>
                   </table>  
                   </div>         
                    <div id="divfamily" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 338px ; text-align:center;">
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                                <col />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:60px;" />
                                <col style="width:25px;" />
                        </colgroup>
                        <asp:Repeater ID="rptWeeklyOff" runat="server"> 
                            <ItemTemplate>
                                <tr class='row'>
                                    <td align="left">
                                        <%#Eval("OfficeName")%><asp:HiddenField ID="hdnOfficeId" runat="server" Value='<%#Eval("OfficeId")%>' /></td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkSunday" runat="server" Checked='<%#Eval("Sun")%>'/></td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkMonday" runat="server" Checked='<%#Eval("Mon")%>'/></td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkTuesday" runat="server" Checked='<%#Eval("Tue")%>'/></td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkWednesday" runat="server" Checked='<%#Eval("Wed")%>'/></td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkThursday" runat="server" Checked='<%#Eval("Thur")%>'/></td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkFriday" runat="server" Checked='<%#Eval("Fri") %>'/></td>
                                    <td align="left">
                                        <asp:CheckBox ID="chkSaturday" runat="server" Checked='<%#Eval("Sat") %>'/></td>
                                                    
                                   <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        </table>
                    </div> 
                     <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                        <tr>
                            <td align="right">
                            <asp:Button ID="btnSave" CssClass="btn"  runat="server" Text="Save" 
                                    onclick="btnSave_Click">
                            </asp:Button>
                             </td>
                        </tr>
                     </table>  
</div>
                </td>
            </tr>
          </table> 
    </div>
    </asp:Content>
