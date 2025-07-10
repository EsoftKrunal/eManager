<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCrewContract.aspx.cs" Inherits="Reporting_PrintCrewContract" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<html xmlns="http://www.w3.org/1999/xhtml" >--%>
<head runat="server">
    <title>EMANAGER</title>
  <%--<link href="../Styles/style.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />--%>
      <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
  <link href="../../../aspnet_client/system_web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <link href="../../../aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="../../../aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;font-family:Arial;font-size:12px;">
       <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="825">
       <tr>
        <td align="center" valign="top">
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
          <%--<tr>
           <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5; color:White;">
            Print Contract
           </td>
          </tr>--%>
             <tr>
                 <td style="text-align: center" colspan="3">
                     <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>&nbsp;
                 </td>
             </tr>
             <tr>
                 <td style="width:150px;text-align:right;padding-right:10px;">
                     Select Template :
                 </td>
                 <td style="width:200px;text-align:left;padding-left:10px;">
                      <asp:DropDownList ID="ddl_ContractTemplate" runat="server" width="200px">
                          <asp:ListItem Value="0">< Select ></asp:ListItem>
                      </asp:DropDownList>
                 </td>
                 <td style="width:200px;text-align:left;padding-left:10px;">
                     <asp:Button runat="server" ID="btnGo" Text="Print" CssClass="btn" OnClick="btnGo_Click"/>
                 </td>
                
              </tr>
          <tr>
           <td style="text-align: left" colspan="3">
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
