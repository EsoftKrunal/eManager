<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WagePayableToCrew_new.aspx.cs" Inherits="Reporting_WagePayableToCrew_new" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
        <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnsearch">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        &nbsp; &nbsp;
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="820">
<tr>
<td align="center" valign="top" style="height: 224px" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td align="center" class="text" style="width: 100%; height: 23px; background-color: #4371a5">
                    Monthly Wage Pay To Crew</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 825px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; width: 825px; color: red; text-align: center">
                                </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;
                                width: 825px;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="height: 17px; width: 161px; text-align: right; padding-right: 5px;">
                                            Emp No:</td>
                                            <td style="height: 17px; width: 3px;" colspan="1"><asp:DropDownList ID="ddemp" runat="server" CssClass="required_box" Width="141px" >
                                            </asp:DropDownList></td>
                                            <td style="width: 46px"></td>
                                            <td style="text-align:left; width: 134px;">
                                                </td>
                                        <td style="width: 125px; text-align: left">
                                        </td>
                                            
                                      
                                    </tr>
                                     <tr>
                                         <td style="height: 4px; width: 161px;">
                                         </td>
                                         <td style="height: 4px; width: 3px;">
                                             <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddemp"
                                                 ErrorMessage="Required"  Type="Integer" ValueToCompare="0" Operator="NotEqual"></asp:CompareValidator></td>
                                         <td style="height: 4px; width: 46px;">
                                         </td>
                                         <td style="height: 4px; width: 134px;">
                                             &nbsp;</td>
                                         <td style="width: 125px; height: 4px">
                                         </td>
                                       
                                        
                                     </tr>
                                     <tr id="trdate" runat="server">
                                         <td style="height: 4px; width: 161px; text-align: right; padding-right: 5px;">
                                             Month :</td>
                                         <td style="height: 4px; width: 3px;">
                                           <asp:DropDownList ID="ddmonth" runat="server" CssClass="required_box" Width="141px" >
                                                <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">Feburary</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4" >April</asp:ListItem>
                                                <asp:ListItem Value="5" >May</asp:ListItem>
                                                <asp:ListItem Value="6" >June</asp:ListItem>
                                                <asp:ListItem Value="7" >July</asp:ListItem>
                                                <asp:ListItem Value="8" >August</asp:ListItem>
                                                <asp:ListItem Value="9" >September</asp:ListItem>
                                                <asp:ListItem Value="10" >October</asp:ListItem>
                                                <asp:ListItem Value="11" >November</asp:ListItem>
                                                <asp:ListItem Value="12" >December</asp:ListItem>
                                            </asp:DropDownList></td>
                                         <td style="height: 4px; width: 46px; text-align: right; padding-right: 5px;">
                                             Year :</td>
                                         <td style="height: 4px; width: 134px;">
                                             &nbsp;<asp:DropDownList ID="ddyear" runat="server" CssClass="input_box" Width="85px" >
                                             </asp:DropDownList></td>
                                         <td style="width: 125px; height: 4px">
                                                <asp:Button ID="btnsearch" runat="server" CssClass="btn" Text="Show Report" OnClick="btnsearch_Click"   /></td>
                                        
                                
                                     </tr>
                                     <tr>
                                         <td style="height: 4px; width: 161px;">
                                         </td>
                                         <td style="height: 4px; width: 3px;">
                                             <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddmonth"
                                                 ErrorMessage="Required" ValueToCompare="0" Type="Integer" Operator="NotEqual"></asp:CompareValidator>&nbsp;&nbsp;
                                             </td>
                                         <td style="height: 4px; width: 46px;">
                                             </td>
                                         <td style="height: 4px; width: 134px;" colspan="1">
                                             &nbsp;</td>
                                         <td colspan="1" style="width: 125px; height: 4px">
                                         </td>
                                                 
                                     </tr>
                                     <tr>
                                         <td align="center" colspan="5">
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td  colspan="5">
                                          <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:500px; overflow:auto"></iframe>
                                          <%--<cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" ></cr:crystalreportviewer>--%>
                                         </td>
                                        
                                     </tr>
                                  
                                   
                                </table>
                            
                            </td>
                        </tr>
                    </table>
                    &nbsp; &nbsp;
                </td>
            </tr>
        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
    <div>
        &nbsp;</div>
    </form>
</body>
</html>
