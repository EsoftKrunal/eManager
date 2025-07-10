<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefonFollowUp_Report.aspx.cs" Inherits="Reports_DefonFollowUp_Report" Title="Follow Up Items Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
           <tr>
            <td align="center" valign="top" >
             <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
              <tr>
               <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5">
                   List of Follow Up Items</td>
              </tr>
              <tr>
               <td>
                   &nbsp;
               </td>
              </tr>
                 <tr>
                     <td style="text-align: center">
                         <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
                 </tr>
              <tr>
               <td style="padding-left: 20px; text-align: left">
                <iframe runat="server" id="IFRAME1" frameborder="0" style="width: 100%; height: 850px;"></iframe>
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
