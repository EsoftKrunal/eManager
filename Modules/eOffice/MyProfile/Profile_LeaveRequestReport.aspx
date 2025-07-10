<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile_LeaveRequestReport.aspx.cs" Inherits="emtm_MyProfile_Emtm_Profile_LeaveRequestReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Request Report</title>
    <style type="text/css">
    body
    {
    	font-family:Arial;
    	font-size:12px; 
    }
    </style> 
</head>
<body onclick="document.getElementById('btnprnt').style.display='';" >
    <form id="form1" runat="server">
    <div>
        <center> <h3>Leave Request</h3></center>
        <b>Employee Details : </b>
        <br /><br />
        <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
        <colgroup>
        <col style="width:15%"/>
        <col />
        <col style="width:15%"/>
        <col />
        </colgroup>
        <tr>
            <td style="text-align :right">Employee Name :</td>
            <td style="text-align :left"><asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
            <td style="text-align :right">Employee Code :</td>
            <td style="text-align :left"><asp:Label ID="lblEmpCode" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align :right">Position :</td>
            <td style="text-align :left"><asp:Label ID="lblPositon" runat="server"></asp:Label></td>
            <td style="text-align :right">Department :</td>
            <td style="text-align :left"><asp:Label ID="lblDepartment" runat="server"></asp:Label></td>
        </tr>
        </table>
        <br />
        <b>Balance Leave Summary : </b>
        <br /><br />
        <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
        <colgroup>
        <col style="width:15%"/>
        <col />
        <col style="width:15%"/>
        <col />
        <col style="width:15%"/>
        <col />
        </colgroup>
        <tr>
            <td style="text-align :right">Balance Leave -<asp:Label ID="lblLastYear" runat="server"></asp:Label> :</td>
            <td style="text-align :left"><asp:Label ID="lblLeaveLastYear" runat="server"></asp:Label></td>
            <td style="text-align :right"><span lang="en-us">Leave Taken -<asp:Label ID="lblCurrYear3" runat="server"></asp:Label> :</span></td>
            <td style="text-align :left"><asp:Label ID="lblLeavesConsumed" runat="server"></asp:Label></td>
            <td style="text-align :right">Annual Entitlement-<asp:Label ID="lblCurrYear" runat="server"></asp:Label> :</span></td>
            <td style="text-align :left"><asp:Label ID="lblLeaveAnnualEntitlement" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align :right">Leave Expired(30th June) :</td>
            <td style="text-align :left"><asp:Label ID="lblLeaveExpired" runat="server"></asp:Label></td>
            <td style="text-align :right">Leave Credit :</td>
            <td style="text-align :left"><asp:Label ID="lblLeaveCredit" runat="server"></asp:Label></td>
            <td style="text-align :right">Balance Leave :</td>
            <td style="text-align :left"><asp:Label ID="lblLeaveBalace" runat="server"></asp:Label></td>
        </tr>
        </table>
        <br />
        <b>Leave Application : </b>
        <br /><br />
        <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
      <colgroup>
        <col style="width:15%"/>
        <col style="width:35%"/>
        <col style="width:15%"/>
        <col style="width:35%"/>
        </colgroup>
        <tr>
            <td style="text-align :right">Leave Type&nbsp;:</td>
            <td style="text-align :left"><asp:Label ID="lblLeaveType" runat="server"></asp:Label></td>
            <td style="text-align :right">Date of Application :</td>
            <td style="text-align :left"><asp:Label ID="lblDateofApplication" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align :right">Leave Period&nbsp;:</td>
            <td style="text-align :left"><span lang="en-us">( </span><asp:Label ID="lblLeaveFrom" runat="server"></asp:Label> 
                <span lang="en-us">&nbsp; : &nbsp;</span> <asp:Label ID="lblLeaveTo" runat="server"></asp:Label>
                <span lang="en-us">&nbsp;)</span>
                
                <asp:Label ID="lblHalfDayText" runat="server" Text="HalfDay"></asp:Label>
                <asp:Label ID="lblHalfDay" runat="server"></asp:Label></td>
            <td style="text-align :right"><span lang="en-us">Leave Duration :</span></td>
            <td style="text-align :left"><asp:Label ID="lblLeaveDays" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align :right" valign="top">Remark&nbsp;:</td>
            <td colspan="3" style="text-align :left"><asp:Label ID="lblUserRemark" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align :right"><span lang="en-us">Sent for Approval</span>&nbsp;:</td>
            <td style="text-align :left">&nbsp;<asp:Label ID="lblForwardedTO" runat="server"></asp:Label></td>
            <td style="text-align :right">Sent On&nbsp;:</td>
            <td style="text-align :left"><asp:Label ID="lblRequestedOn" runat="server"></asp:Label></td>
        </tr>
        </table>
        
        <div id="tblLeaveHOD" runat="server" style="width :100%" >
        <br />
        <b>Leave Approval : </b>
        <br /><br />
        <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
        <colgroup>
         <col style="width:15%"/>
        <col style="width:35%"/>
        <col style="width:15%"/>
        <col style="width:35%"/>
        </colgroup>
         <tr>
            <td style="text-align :right">Approved / Rejected By&nbsp;:</td>
            <td style="text-align :left"><asp:Label ID="lblAppRejBY" runat="server"></asp:Label></td>
            <td style="text-align :right">Approved / Rejected On&nbsp;:</td>
            <td style="text-align :left"><asp:Label ID="lblAppRejOn" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align :right" valign="top">Remark&nbsp;:</td>
            <td style="text-align :left" colspan="3"><asp:Label ID="lblAppRejRemark" runat="server" ></asp:Label></td>
        </tr>
        </table>
        </div>
        <br />
        <br />
        <table cellpadding="1" cellspacing="0" border="0" style="border-collapse:collapse;">
        <tr style ="font-size:15px;">
        <td><b>Status :</b></td>
        <td><asp:Label ID="lblLeaveStatus" runat="server" Width="180px"></asp:Label></td>
        </tr>
        </table> 
    </div>
    <center>
    <br />
    <img src ="../../Images/Emtm/print.jpg" id='btnprnt' onclick="document.getElementById('btnprnt').style.display='none';window.print();"/>
    </center>
    </form>
    <script type="text/javascript">
    window.focus();
    window.moveTo( 0, 0 );
    window.resizeTo( screen.availWidth, screen.availHeight );
    </script> 
</body>
</html>
