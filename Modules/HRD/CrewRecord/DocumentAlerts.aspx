<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentAlerts.aspx.cs" Inherits="CrewRecord_DocumentAlerts" Title="Documents Alert" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
  <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Alert Documents</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server"  >
      <div style="text-align: center">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
         <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; height: 23px; ">Documents Alert</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 825px">
                                &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 825px">
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 825px;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr><td colspan="2" style=" padding-left : 20px"  >
                                        &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="95%" GridLines="None">
                                        <Columns >
                                        <asp:BoundField HeaderText="Document Type" DataField="DocumentType"/>
                                        <asp:BoundField HeaderText="Doc. Number" DataField="DocumentNumber"/>
                                        <asp:BoundField HeaderText="Issue Date" DataField="IssueDate"/>
                                        <asp:BoundField HeaderText="Expiry Date" DataField="ExpiryDate"/>
                                        
                                        </Columns>
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>
                                    </td></tr>
                                </table>
                               <div id="divPrint">
                                    &nbsp;</div>
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
