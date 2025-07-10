<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeAbsenceExpense.aspx.cs" Inherits="Reporting_OfficeAbsenceExpense" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="825">
       <tr>
        <td align="center" valign="top">
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
             <tr>
                 <td style="text-align: left">
                 </td>
             </tr>
          <tr>
           <td style="text-align: left">
               <br />
               <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" ></cr:crystalreportviewer>
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
