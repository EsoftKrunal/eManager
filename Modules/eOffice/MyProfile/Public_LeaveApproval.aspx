<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Public_LeaveApproval.aspx.cs" Inherits="emtm_MyProfile_Public_LeaveApproval" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Approval</title>
    <script type="text/javascript" src="../../JS/jquery.min.js"></script>
    <link href="../style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CloseWindow() {
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <div>
     <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Office Absense Travel Approval</div>
     <table width="100%" cellspacing ="0" cellpadding="5" border="0">
        <tr>
        <td style='text-align:left'>
            <asp:Label runat="server" ID="lblLocation" Font-Size="Large" ForeColor="Green"></asp:Label> - 
            <asp:Label runat="server" ID="lblPurpose" Font-Size="Large" ForeColor="orange"></asp:Label>
            </td>
            <td style="text-align:left"><asp:Label runat="server" ID="lblPeriod" Font-Size="Large" ForeColor="gray"></asp:Label> </td>
            <td style="text-align:right">
                <asp:Label runat="server" ID="lblHalfDay" Font-Size="Large" ForeColor="purple"></asp:Label>
            </td>
        </tr>
         <tr>
        <td style='text-align:left'>
            <asp:Label runat="server" ID="lblVesselName" Font-Size="Large" ForeColor="Green"></asp:Label>
            </td>
            <td style="text-align:left" colspan="2">
                <asp:Label runat="server" ID="lblPlannedInspections" Font-Size="Large" ForeColor="gray"></asp:Label>
            </td>
        </tr>
        <tr>
        <td style='text-align:left; background:#FFFFF0; border:solid 1px #eeeeee' colspan="3">
            <asp:Label runat="server" ID="lblRemarks"></asp:Label>
        </td>
        </tr>
        </table>
        <div style='padding:10px; background:#4DB8FF; color:White; text-align:center; font-size:18px;'>Approve / Reject</div>
            <table width="100%" cellspacing ="0" cellpadding="5" border="0">
                <tr>
                    <td style="text-align :center;">
                        <asp:RadioButton ID="rdoLeaveApprove" runat="server" GroupName="LeaveStatus" Text="Approve" Font-Bold="True" ForeColor="#009933" AutoPostBack="true" OnCheckedChanged="Action_Change"/>
                        <asp:RadioButton ID="rdoLeaveReject" runat="server" GroupName="LeaveStatus" Text="Reject" Font-Bold="True" ForeColor="Red" AutoPostBack="true" OnCheckedChanged="Action_Change"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; " valign="top">
                        <b>Comments / Remarks : </b> <br />                         
                        <asp:TextBox ID="txtAppRejRemarks" runat="server" TextMode="MultiLine" Height="100px" Width="100%"></asp:TextBox> 
                    </td>
                </tr>
                </table> 

            <div style="text-align:center">
                <asp:Button ID="btnDone" CssClass="btn" runat="server" Text="Save" onclick="btnDone_Click" Width="100px" style='margin-bottom:5px' />  
                <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="Close" Width="100px" CausesValidation="false" style='margin-bottom:5px' OnClientClick="CloseWindow();" />  
            </div>
    </div>
    </form>
</body>
</html>
