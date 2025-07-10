<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManPowerReportHeader.aspx.cs" Inherits="Reporting_ManPowerReportHeader" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
  <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    
    <script language="javascript" type="text/javascript">
 function DoPost()
 {
 var s="ManPowerReport.aspx?StatusID=" + document.getElementById("DropDownList1").value + "&OffCrew=" + document.getElementById("DropDownList2").value;
 document.getElementById("IFRAME1").setAttribute("src",s);
 return false;
 }
 </script>
</head>
<body>
<form id="form1" runat="server" defaultbutton="Button1">
<div style="text-align: center">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; "> Man Power Report</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 100%;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td align="center" colspan="4" style="">
                                             <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                     </tr>
                                     <tr style=" padding:3px; " >
                                         <td colspan="1" style="width: 190px;">
                                             &nbsp;Status :
                                             <asp:DropDownList ID="DropDownList1" runat="server" CssClass="input_box">
                                                 <asp:ListItem Value="3">&lt; All &gt;</asp:ListItem>
                                                 <asp:ListItem Value="0">New Emp</asp:ListItem>
                                                 <asp:ListItem Value="1">On Leave</asp:ListItem>
                                                 <asp:ListItem Value="2">On Board</asp:ListItem>
                                             </asp:DropDownList>
                                          <%--   <select id="ddlType" class ="input_box">
                                                <option value="3" >&lt; All &gt;</option>
                                                <option value="0" >New Emp</option>
                                                <option value="1" >On Leave</option>
                                                <option value="2" >On Board</option>
                                             </select>--%>
                                             </td>
                                         <td colspan="1" style="width: 79px;">
                                             Off Crew :</td>
                                         <td colspan="1" style="">
                                             <asp:DropDownList ID="DropDownList2" runat="server" CssClass="input_box" Width="110px">
                                                 <asp:ListItem Value="A">All</asp:ListItem>
                                                 <asp:ListItem Value="O">Officers</asp:ListItem>
                                                 <asp:ListItem Value="R">Rating</asp:ListItem>
                                             </asp:DropDownList></td>
                                                 <td align="right">
                                             <asp:Button ID="btnExcel" runat="server" CssClass="btn"
                                                 Text="Export to Excel" OnClick="btnExcel_Click" />&nbsp;
                                                     <asp:Button ID="Button1" runat="server" CssClass="btn" OnClientClick="return DoPost();"
                                                 Text="Show Report" />&nbsp;&nbsp;&nbsp;</td>
                                     </tr>
                                    <tr><td colspan="4">
                                    <iframe id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
                                    </td></tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
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
