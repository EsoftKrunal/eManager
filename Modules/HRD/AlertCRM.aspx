<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertCRM.aspx.cs" Inherits="AlertCRM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Styles/StyleSheet.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>CRM Alert</title>
</head>

<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr style="height:23px">
                <td align="center" style="background-color:#4371a5;" class="text">
                    CRM Alerts</td>
            </tr>
            <tr style="height:30px">
                <td>
                <div style="height:500px; width:100%;overflow-x:hidden;overflow-y:scroll" >
                   <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" GridLines="horizontal" Width="100%">
                   <Columns>
                   <asp:TemplateField Visible="false">
                   <ItemTemplate>
                   <asp:HiddenField ID="hiddenAvalue" runat="server" Value='<%# Eval("AValue") %>' />
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="CrewNumber" HeaderText="Emp.#" >
                       <ItemStyle Width="50px" />
                   </asp:BoundField>
                   <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="left"/>
                   <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-HorizontalAlign="left"/>
                   <asp:BoundField DataField="AlertExpiryDate" HeaderText="Expiry Date"  >
                       <ItemStyle Width="100px" />
                   </asp:BoundField>
                   <asp:BoundField DataField="UserName" HeaderText="Alert By" ItemStyle-HorizontalAlign="left">
                    <ItemStyle Width="200px" />
                   </asp:BoundField>
                   </Columns>
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <PagerStyle CssClass="pagerstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                        <RowStyle CssClass="rowstyle" />
                    </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
