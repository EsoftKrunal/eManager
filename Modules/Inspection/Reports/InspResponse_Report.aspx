<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspResponse_Report.aspx.cs" Inherits="InspResponse_Report" Title="Response Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
        <td align="center" valign="top" style="height: 235px">
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
          <%--<tr>
           <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5">
               Response</td>
          </tr>--%>
          <tr>
           <td>
               &nbsp;
           </td>
          </tr>
             <tr>
                 <td style="text-align: center">
                     <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
             </tr>
             <%--<tr>
                 <td>
                     <table cellpadding="0" cellspacing="0" style="width: 660px">
                         <tr>
                             <td style="padding-right: 5px; width: 144px; text-align: right">
                                 Inspection#:</td>
                             <td style="width: 184px; text-align: left">
                                 <asp:DropDownList ID="ddl_inspnum" runat="server" CssClass="required_box" Width="260px" TabIndex="1">
                                 </asp:DropDownList></td>
                             <td style="width: 100px; text-align: right">
            <asp:Button ID="btn_show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_show_Click" TabIndex="2" /></td>
                         </tr>
                         <tr>
                             <td style="padding-right: 5px; width: 144px; text-align: right">
                             </td>
                             <td style="width: 184px; text-align: left">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_inspnum"
                                     ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                             <td style="width: 100px; text-align: right">
                             </td>
                         </tr>
                     </table>
                 </td>
             </tr>--%>
             <%--<tr>
                 <td style="height: 48px">
                     &nbsp; &nbsp;&nbsp;
                 </td>
             </tr>--%>
          <tr>
           <td style="padding-left: 20px; text-align: justify">
            <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedHeaderPopUp" ID="CrystalReportViewer1" runat="server"  />
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
