<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertSignOffCrew.aspx.cs" Inherits="AlertSignOffCrew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>SignOff Crew Alert</title>
<link href="Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-family :Verdana; font-size :11px; margin :0px 0px 0px 0px;">
   <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr style="height:23px">
                <td align="center">
                <div style="width :1000px;background-color:#4371a5;  height :25px; padding-top :5px; color:White">
                <strong>
                SignOff Crew Alerts</strong>
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
                <td style="width:630px">Name</td>
                <td style="width:60px">Rank</td>
                <td style="width:60px">Vessel</td>
                <td style="width:200px">Relief Due Date</td>
                </thead>
                </table>
                <div style=" width:100%; height:450px; overflow-x:hidden; overflow-y:scroll;" > 
                   <asp:GridView ID="gv_SignOff" ShowHeader="false" runat="server" OnRowDataBound="gv_SignOff_RowDataBound" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" GridLines="horizontal" Width="98%">
                   <Columns>
                   <asp:TemplateField Visible="false">
                   <ItemTemplate>
                   <asp:HiddenField ID="hiddenAvalue" runat="server" Value='<%# Eval("AValue") %>' />
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="CrewNumber" HeaderText="Emp #" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                   <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="650px"/>
                   <asp:BoundField DataField="Rank" HeaderText="Rank" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                   <asp:BoundField DataField="Vessel" HeaderText="Vessel" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px"/>
                   <asp:BoundField DataField="ReliefDueDate" HeaderText="Relief Due Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                   </Columns>
                   <RowStyle height="20px" />
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
