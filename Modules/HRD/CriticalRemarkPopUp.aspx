<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CriticalRemarkPopUp.aspx.cs" Inherits="CriticalRemarkPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Critical Remarks.</title>
    
    <link href="Styles/sddm.css" rel="stylesheet" type="text/css" />
   <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../../css/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;font-family:Arial;font-size:12px;" width="100%">
            <tr style="height:23px">
                <td align="center"  class="text headerband">
                    Critical Remark</td>
            </tr>
            <tr style="height:30px">
                <td>
                   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" GridLines="horizontal" Width="100%">
                   <Columns>
                   <asp:TemplateField Visible="false">
                   <ItemTemplate>
                   <asp:HiddenField ID="hiddenAvalue" runat="server" Value='<%# Eval("AValue") %>' />
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="CrewNumber" HeaderText="Emp.#" >
                       <ItemStyle Width="50px" />
                   </asp:BoundField>
                   <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="left" ItemStyle-Width="350px"/>
                   <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-HorizontalAlign="left" ItemStyle-Width="250px"/>
                   <asp:BoundField DataField="AlertExpiryDate" HeaderText="Expiry Date"  >
                       <ItemStyle Width="80px" />
                   </asp:BoundField>
                   <asp:BoundField DataField="CreatedBy" HeaderText="User Name" ItemStyle-Width="120px" />
                   <asp:BoundField DataField="CreatedOn" HeaderText="Date" ItemStyle-Width="80px" />
                   </Columns>
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <PagerStyle CssClass="pagerstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                        <RowStyle CssClass="rowstyle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
