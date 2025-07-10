<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintInvoice.aspx.cs" Inherits="Invoice_PrintInvoice" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
               <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedExpensesToolbar" ID="CrystalReportViewer1" runat="server" />
 
        </div>    
         
    </form>
</body>
</html>
