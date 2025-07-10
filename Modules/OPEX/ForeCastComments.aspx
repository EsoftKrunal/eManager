<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForeCastComments.aspx.cs" Inherits="ForeCastComments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Budget Forecast Comments</title>
     <link href="CSS/style.css" rel="Stylesheet" type="text/css" />
</head>
<body style="margin:10px" >
    <form id="form1" runat="server">
        <h3>Account Head : <asp:Label ForeColor="Blue" runat="server" ID="lblHeadName"></asp:Label> </h3>
        <h3>Comments :</h3>
        <table border="1" width="746" style=" text-align :center; font-weight: bold ; font-size: 13px; " cellpadding="2" cellspacing="0" rules="all" bordercolor ="navy" >
        <tr style=" background-color : Gray" >
        <td><%=Byear%></td>
        <td><%=Byear%></td>
        <td><%=Byear%></td>
        <td><%=Fyear%></td>
        <td><%=Byear%></td>
        <td colspan="2" ><%=Fyear%>-Forecast Var.% <span style="font-family :Arial Narrow; color :White" >v/s</span></td>
        </tr>
        <tr style=" background-color : Gray" >
        <td>Actual & Comited </td>
        <td>Projected </td>
        <td>Budget </td>
        <td>Forecast</td>
        <td>Var.% ( B <span style="font-family :Arial Narrow; color :White" >v/s</span> P)</td>
        <td><%=Byear%>-[B]</td>
        <td><%=Byear%>-[P]</td> 
        </tr>
        <tr>
        <td><asp:Label runat="server" ID="lblActComm"></asp:Label></td>
        <td><asp:Label runat="server" ID="lblProj"></asp:Label></td>
        <td><asp:Label runat="server" ID="lblBudg"></asp:Label></td>
        <td><asp:Label runat="server" ID="lblFore"></asp:Label></td>
        <td><asp:Label runat="server" ID="lblVar1"></asp:Label>%</td>
        <td><asp:Label runat="server" ID="lblVar3"></asp:Label>%</td>
        <td><asp:Label runat="server" ID="lblVar2"></asp:Label>%</td>
        </tr>
        </table>
        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtComments" 
        Height="348px" Width="746px"></asp:TextBox>
        <br /><br />
        <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Images/save.jpg" OnClick="imgSave_OnClick" />
    </form>
</body>
</html>
