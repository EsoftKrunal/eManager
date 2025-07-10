<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertCRM.aspx.cs" Inherits="AlertCRM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>CRM Alert</title>
</head>
<body style="font-family :Verdana; font-size :11px; margin :0px 0px 0px 0px;" >
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr style="height:23px">
                <td align="center">
                <div style="width :1000px;background-color:#4371a5;  height :25px; padding-top :5px; color:White">
                <strong>
                CRM Alerts</strong>
                </div>
                </td>
            </tr>
            <tr style="height:30px">
                <td>
                 <center>
                <div style="width :1000px;" >
                <table style="width:1000px" cellpadding="0" cellspacing="0" border="1"  style="border-collapse:collapse"  >
                <thead style=" background-color : lightgray; border: solid 1px gray; height :25px; font-weight: bold  " > 
                <td style="width:50px" >Emp#</td>
                <td style="width:300px">Name</td>
                <td style="width:300px">Description</td>
                <td style="width:100px">Expiry Date</td>
                <td style="width:250px">Alert By</td>
                </thead>
                </table>
                <div style="height:450px; width:100%;overflow-x:hidden;overflow-y:scroll;" >
                   <asp:GridView ID="GridView1" ShowHeader="false" runat="server" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" GridLines="horizontal" Width="100%">
                   <Columns>
                   <asp:TemplateField Visible="false">
                   <ItemTemplate>
                   <asp:HiddenField ID="hiddenAvalue" runat="server" Value='<%# Eval("AValue") %>' />
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="CrewNumber" HeaderText="Emp.#" ItemStyle-Width="50px"  />
                   <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="left" ItemStyle-Width="300px"  />
                   <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-HorizontalAlign="left" ItemStyle-Width="300px"  />
                   <asp:BoundField DataField="AlertExpiryDate" HeaderText="Expiry Date" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="center" />
                   <asp:BoundField DataField="UserName" HeaderText="Alert By" ItemStyle-HorizontalAlign="left" ItemStyle-Width="250px"  />
                   </Columns>
                        <RowStyle Height="20px" />
                    </asp:GridView>
                    </div>
                </div> 
                </center> 
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
