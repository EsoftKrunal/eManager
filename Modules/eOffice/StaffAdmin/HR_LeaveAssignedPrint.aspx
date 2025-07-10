<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_LeaveAssignedPrint.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_HR_LeaveAssignedPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Assingned Report</title>
     <style type="text/css">
        body
        {
    	    font-family:Arial;
    	    font-size:12px; 
        }
    </style> 
    
    <script type="text/javascript">
    window.focus();
    window.moveTo( 0, 0 );
    window.resizeTo( screen.availWidth, screen.availHeight );
    </script>
</head>
<body onclick="document.getElementById('btnprnt').style.display='';">
 <form id="form1" runat="server">
    <div>
        <center> <h3>Assigned Leave Report<asp:Label id="lblCurrentDate" runat="server"></asp:Label> </h3></center>
        <br />
        <b >Employee Details : </b>
        <br /><br />
        <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
                <colgroup>
                <col style="width:80px"/>
                <col />
                <col style="width:80px"/>
                <col />
                <col style="width:80px"/>
                <col />
                <col style="width:60px"/>
                <col />
                </colgroup>
               <tr>
                   <td style="text-align :right">
                       Emp Name :&nbsp;</td>
                   <td style="text-align :left">
                       <asp:Label ID="lblEmpName" runat="server" ></asp:Label>
                   </td>
                   <td style="text-align :right">
                       Position :&nbsp;</td>
                   <td style="text-align :left">
                       <asp:Label ID="lblDesignation" runat="server" ></asp:Label>
                   </td>
                   <td style="text-align :right">
                       Department :&nbsp;</td>
                   <td style="text-align :left">
                      <asp:Label ID="lblDepartment" runat="server"></asp:Label> 
                   </td>
                   <td style="text-align :right">
                       Office :&nbsp;</td>
                   <td>
                        <asp:Label ID="lblOffice" runat="server"></asp:Label>  
                   </td>
               </tr>
        </table>
        <div id="divLeaveSummary" runat="server">
        <br />
        <b>Leave Summary : </b>
        <br /><br />
        <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
            <colgroup>
                <col />
                <col style="width:200px;" />
                <tr>
                    <td align="left" style="font-weight:bold;">Leave Type</td>
                    <td align="center" style="font-weight:bold;">Leave Assigned</td>
                </tr>
            </colgroup>
         </table>   
        <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
            <colgroup>
                    <col />
                    <col style="width:200px;" />
             </colgroup>
            <asp:Repeater ID="rptLeaveDetails" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="left">
                            <%#Eval("LeaveTypeName")%> <asp:HiddenField ID="hdnLeavetypId" runat="server" Value='<%#Eval("LeaveTypeId")%>'/>
                        </td>
                        <td align="center">
                           <asp:Label ID="lblLeaveAssigned" runat="server" Text='<%#Eval("LeaveCount")%>' Width="40px"></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
         </table>
        </div> 
        <div id="divLeaveDetails" runat="server">
            <br />
            <b >Leave Details : </b>
            <br /><br />
        <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
            <colgroup>
                <col />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:80px;"/>
                <tr>
                    <td align="left" style="font-weight:bold;">Leave Type </td>
                    <td align="center" style="font-weight:bold;">FromDate </td>
                    <td align="center" style="font-weight:bold;">ToDate</td>
                    <td align="center" style="font-weight:bold;">Duration</td>
                </tr>
            </colgroup>
        </table>      
        <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" bordercolor="#c2c2c2">
        <colgroup>
                <col />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:80px;"/>
        </colgroup>
        <asp:Repeater ID="RptLeaves" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="left">
                        <%#Eval("LeaveTypeName")%></td>                
                    <td align="center">
                        <%#Eval("LeaveFrom")%></td>
                    <td align="center">
                        <%#Eval("LeaveTo")%></td>
                    <td align="center">
                        <%#Eval("Duration")%></td>    
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
        </div>
    </div>
    <center>
    <br />
        <img src ="../../Images/Emtm/print.jpg" id='btnprnt' onclick="document.getElementById('btnprnt').style.display='none';window.print();"/>
    </center>
 </form>
</body>
</html>
