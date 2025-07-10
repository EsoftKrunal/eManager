<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUpDemo1.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_PopUpLeaveApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Status</title>
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
        <table width="100%">
            <tr>
            <td valign="top" style="border:solid 1px #4371a5; height:100px;">
                <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                     Leave Status
                </div>
                <table id="tblLeaveType" runat="server" width="100%" cellspacing ="0" cellpadding="5" border="0">
                    <tr>
                        <td style ="text-align:right;">
                            EmpCode :
                        </td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblEmpCode" runat="server" Font-Bold="true"></asp:Label>  
                        </td>
                        <td style ="text-align:right;">
                            Emp Name :
                        </td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblEmpName" runat="server" Font-Bold="true"></asp:Label>  
                        </td>
                        <td style ="text-align:right;">
                            Leave Type :
                        </td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblLeaveType" runat="server" Font-Bold="true"></asp:Label>  
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align:right;">
                           Leave From :
                        </td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblLeaveFrom" runat="server" Font-Bold="true"></asp:Label>  
                        </td>
                        <td style ="text-align:right;">
                            Leave To :
                        </td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblLeaveTo" runat="server" Font-Bold="true"></asp:Label>  
                        </td>
                        <td style="text-align:right;">
                           Leave Status :
                        </td>
                        <td style="text-align :left;">
                            <asp:RadioButton ID="rdoLeaveApprove" runat="server" GroupName="LeaveStatus" Text="Approve" />
                            <asp:RadioButton ID="rdoLeaveReject" runat="server" GroupName="LeaveStatus" Text="Reject" />
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align:right;">
                            Reason :</td>
                        <td style="text-align :left;" colspan="3">
                            <asp:Label ID="lblReason" runat="server" Font-Bold="true" Width="400px"></asp:Label>  
                        </td>
                        <td style="text-align:right;">
                            &nbsp;</td>
                        <td style="text-align :left;">
                            &nbsp;</td>
                   </tr>
                   <tr>
                        <td style="text-align:right;" valign="top">
                            Office Remark :                         </td>
                        <td style="text-align :left;" colspan="5">
                            <asp:TextBox ID="txtOfficeReason" runat="server" TextMode="MultiLine" Height="52px" 
                                Width="625px"></asp:TextBox> 
                        </td>
                   </tr>
               </table>
                    
                <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                            <tr>
                                <td style="text-align :right">
                                        <asp:Button ID="btnDone" runat="server" Text="Done" onclick="btnDone_Click" />  
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="CloseWindow();"  />  
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
