<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewNationalitySummaryHeader.aspx.cs" Inherits="CrewNationalitySummaryHeader" Title="Crew On Vessel Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>Untitled Page</title>
<link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
function DoPost()
{
var s="CrewNationalitySummary.aspx";
//alert(s);
document.getElementById("IFRAME1").setAttribute("src",s);
return false;
}
</script>
</head>
<body>
<form id="form1" runat="server" defaultbutton="btn_show">
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
                <td align="center" class="text headerband" style="width: 100%;"> Crew Nationality Summary</td>
            </tr>
            <tr>
                <td style="width: 100%; height: 60px;">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
<tr>
    <td style="height: 13px">
     <table cellpadding="0" cellspacing="0" style="width: 100%">
                   <tr>
                       <td style="width: 100px">
                       </td>
                       <td style="width: 477px; text-align: center">
                           <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
                       <td style="width: 100px">
                       </td>
                   </tr>
                   <tr>
                       <td style="width: 100px">
                       </td>
                       <td style="width: 477px">
                       </td>
                       <td style="width: 100px; padding :3px;">
                           <asp:Button ID="btn_show" runat="server" CssClass="btn" OnClientClick="return DoPost();" TabIndex="1" Text="Show Report" /></td>
                   </tr>
               </table>
          <iframe id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
    </td>
</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>
</table>
</td> </tr> </table>  
</div>
</form>
</body>
</html>

