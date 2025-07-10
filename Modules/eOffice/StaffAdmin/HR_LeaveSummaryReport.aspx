<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_LeaveSummaryReport.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_HR_LeaveSummaryReport" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Summary Report</title>
</head>
<body  style=" font-family:Arial ; font-size:13px; ">
    <form id="form1" runat="server">
    <div>
        <center> <h3>Leave Summary</h3></center>
        <div style=" background-color :#C2C2C2" >
        <table width="100%">
        <tr>
        <td>&nbsp;</td>
        <td> Office : </td>
        <td> <asp:DropDownList runat="server" ID="ddlOffice"></asp:DropDownList>  </td>
        <td> Year : </td>
        <td> <asp:DropDownList runat="server" ID="ddlYear"></asp:DropDownList>  </td>
        <td> Month : </td>
        <td> <asp:DropDownList runat="server" ID="ddlMonth"></asp:DropDownList>  </td>
        <td> Report Type : </td>
        <td> <asp:DropDownList runat="server" ID="ddlReportType">
        <asp:ListItem Text="Leave Summary" Value="L"></asp:ListItem>
        <asp:ListItem Text="Office Absense" Value="A"></asp:ListItem>
        </asp:DropDownList>  </td>
        <td> <asp:Button runat="server" ID="btnShow" Text="Show Report" onclick="btnShow_Click" />   </td>
        </tr>
        </table>
        </div>
        <br />
        <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:600px; overflow:auto"></iframe>
    </div>
    <center>
    <br />
    </center>
    </form>
    <script type="text/javascript">
    window.focus();
    window.moveTo( 0, 0 );
    window.resizeTo( screen.availWidth, screen.availHeight );
    </script> 
</body>
</html>
