<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupHolidayMaster.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_PopupHolidayMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    
     <script language="javascript" type="text/javascript">
         function CloseWindow() {
             window.opener.document.getElementById("btnhdn").click();
             window.close();
         }
     </script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
            <tr>
               <td valign="top" style="border:solid 1px #4371a5; height:80px;">
                    <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                        Add/Modify Holiday  
                    </div>
                    <table width="100%" cellpadding="4px" cellspacing ="0" border="0">
                               <tr>
                                   <td style="text-align :right">
                                       From Date :</td>
                                   <td style="text-align :left">
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="100px" MaxLength="11"></asp:TextBox>
                                        <asp:ImageButton ID="imgFromDate" runat="server" 
                                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                   </td>
                                   <td style="text-align :right">
                                       To Date :</td>
                                   <td style="text-align :left">
                                        <asp:TextBox ID="txtToDate" runat="server" Width="100px" MaxLength="11"></asp:TextBox>
                                        <asp:ImageButton ID="imgToDate" runat="server" 
                                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                   </td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                               ControlToValidate="txtFromDate" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                   </td>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left">
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                               ControlToValidate="txtToDate" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                   </td>
                               </tr>
                                <tr>
                               <td style="text-align :right" valign="top">
                                   Holiday Description : </td>
                               <td style="text-align :left" colspan="3">
                                   <asp:TextBox ID="txtReason" runat="server" Width="530px" TextMode="MultiLine" 
                                       MaxLength="450" Height="46px"></asp:TextBox>
                               </td>
                               </tr>
                               <tr>
                                   <td style="text-align :right">
                                       &nbsp;</td>
                                   <td style="text-align :left">
                                       <asp:HiddenField id="hdnOfficeId" runat="server"/></td>
                                   <td style="text-align :left">
                                       <asp:HiddenField id="hdnYear" runat="server"/></td>
                                   <td style="text-align :left">
                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                           Format="dd-MMM-yyyy" PopupButtonID="imgToDate" PopupPosition="TopLeft" 
                                           TargetControlID="txtTodate">
                                       </ajaxToolkit:CalendarExtender>
                                       <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"  
                                           Format="dd-MMM-yyyy" PopupButtonID="imgFromDate" PopupPosition="TopLeft" 
                                           TargetControlID="txtFromDate">
                                       </ajaxToolkit:CalendarExtender>
                                   </td>
                               </tr>
                       </table>
                    <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                            <tr>
                                <td style="text-align :right">
                                        <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" 
                                        onclick="btnsave_Click"></asp:Button>
                                        <asp:Button ID="btnCancel" CssClass="btn"  runat="server" Text="Close" CausesValidation="false"   
                                            onclick="btnCancel_Click" OnClientClick="CloseWindow();">
                                        </asp:Button>
                                 </td>
                            </tr>
                     </table>  
               </td>
            </tr>
     </table>                       
    </div>
    </form>
</body>
</html>
