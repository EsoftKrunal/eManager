<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopUpLeaveApproval.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_PopUpLeaveApproval" %>

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
<body  onunload="CloseWindow();" >
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
            <td valign="top" style="border:solid 1px #4371a5; height:100px;">
                <div class="dottedscrollbox" style=" text-align :center; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                     Approve/Reject Leave 
                </div>
                <table id="tblLeaveType" runat="server" width="100%" cellspacing ="2" cellpadding="5" border="0">
                    <tr>
                        <td style ="text-align:right;">
                            EmpCode :
                        </td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblEmpCode" runat="server" ></asp:Label>  
                        </td>
                        <td style ="text-align:right;">
                            Emp Name :
                        </td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblEmpName" runat="server" ></asp:Label>  
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align:right;">
                           Leave <span lang="en-us">Period</span> :
                        </td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblLeaveFrom" runat="server" ></asp:Label>  
                            <span lang="en-us">&nbsp;: </span>
                            <asp:Label ID="lblLeaveTo" runat="server" ></asp:Label>  
                        </td>
                        <td style ="text-align:right;">
                            Leave Type :
                        </td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblLeaveType" runat="server" ></asp:Label>  
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align:right;">
                            Duration :</td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblduration" runat="server" ></asp:Label>  
                        </td>
                        <td style ="text-align:right;">
                            Total Office Absence :</td>
                        <td style="text-align :left;">
                            <asp:Label ID="lblAbsentDays" runat="server" Text=""></asp:Label> </td>
                   </tr>
                   <tr>
                        <td style="text-align:right;">
                            Employee Remark :</td>
                        <td style="text-align :left;" colspan="3">
                            <asp:Label ID="lblReason" runat="server"  Width="400px"></asp:Label>  
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align:right;">
                            Forwarded To :</td>
                        <td style="text-align :left;" colspan="3">
                            <asp:Label ID="lblForwardedTo" runat="server"  Width="400px"></asp:Label>  
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align:right;" colspan="4">
                        <hr /></td>
                   </tr>
               </table>
               <asp:Panel ID="PanelVerification" runat="server">
               <fieldset>
               <legend>Leave <span lang="en-us">Approval</span></legend> 
               <table cellspacing ="0" cellpadding="4" border="0">
                    <tr>
                        <td style="text-align:right;">
                                                  </td>
                        <td style="text-align :left;">
                            <asp:RadioButton ID="rdoLeaveVerify" runat="server" GroupName="LeaveStatus" 
                                Text="Approve" Font-Bold="True" ForeColor="#009933" />
                            <asp:RadioButton ID="rdoLeaveReject" runat="server" GroupName="LeaveStatus" 
                                Text="Reject" Font-Bold="True" ForeColor="Red" />
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align:right;" valign="top">
                            Remarks :                         </td>
                        <td style="text-align :left;">
                            <asp:TextBox ID="txtVerifyRemarks" runat="server" TextMode="MultiLine" Height="96px" 
                                Width="625px"></asp:TextBox> 
                        </td>
                   </tr>
               </table> 
               </fieldset>
                <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                            <tr>
                                <td style="text-align :right; padding :4px;" >
                                        <asp:Button ID="btnDone" CssClass="btn" runat="server" 
                                            Text="Save & Notify" onclick="btnDone_Click" Width="111px" />  
                            <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" OnClientClick="CloseWindow();"  />  
                                 </td>
                            </tr>
                </table> 
                </asp:Panel>
               <asp:Panel ID="PanelCancelation" runat="server">
               <fieldset>
               <legend>Leave Cancellation</legend> 
               <table cellspacing ="0" cellpadding="4" border="0">
                   <tr>
                        <td style="text-align:right;" valign="top">
                             Remarks :</td>
                        <td style="text-align :left;">
                            <asp:TextBox ID="txtCancelationRemark" runat="server" TextMode="MultiLine" Height="96px" 
                                Width="625px"></asp:TextBox> 
                        </td>
                   </tr>
               </table> 
               </fieldset>
               <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                            <tr>
                                <td style="text-align :right; padding :4px;" >
                                        <asp:Button ID="btnCancellation" CssClass="btn" runat="server" 
                                            Text="Cancelation" Width="111px" onclick="btnCancelation_Click" />  
                            <asp:Button ID="Button2" runat="server" CssClass="btn" Text="Close" OnClientClick="CloseWindow();"  />  
                                 </td>
                            </tr>
                </table> 
               </asp:Panel>
            </td>
        </tr> 
      </table> 
       
    </div>
    </form>
</body>
</html>
