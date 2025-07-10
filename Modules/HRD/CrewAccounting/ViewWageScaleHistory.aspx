<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewWageScaleHistory.aspx.cs" Inherits="CrewOperation_ViewWageScaleHistory" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HRD - Crew Accounting - Wage Scale History</title>
    <link href="../styles/mystyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.js"></script>
     <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="stickyHeader" style="height:90px">
 <div class="text headerband">Wage Scale History</div>
    <div style="padding:5px">
        <table width="100%" cellpadding="3" cellspacing="0" >
        <tr>
            <td style=" text-align: right;">Wage Scale :&nbsp;</td>
            <td style="text-align: left; "><asp:Label runat="server" ID="lblWageScaleName"></asp:Label></td>
            <td style="text-align: right;">Seniority ( Year ) :&nbsp;</td>
            <td style="text-align: left; "><asp:Label runat="server" ID="lblSeniority"></asp:Label></td>
            <td style="text-align: right;">Effective From :&nbsp;</td>
            <td style="text-align: left;"> <asp:Label runat="server" ID="lblEffectiveFrom"></asp:Label> </td>
        </tr>
        </table>
     </div>
    <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse" >
    <tr class= "headerstylegrid">
    <th>Rank Code</th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label1" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label2" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label3" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label4" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label5" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label6" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label7" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label8" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label9" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label10" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label11" Font-Size="11px"></asp:Label></th>    
    <th style='width:100px'><asp:Label runat="server" ID="Label12" Font-Size="11px"></asp:Label></th>    
    </tr>
    </table>
</div>
<div style='padding-bottom:30px'>
<div style="height:90px">&nbsp;</div>
    <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse:collapse" class="grid">
    <asp:Repeater runat="server" ID="rptWages">
    <ItemTemplate>
    <tr>
        <td style='text-align:left'><%#Eval("RankCode")%></td>
        <td style='text-align:right; width:100px;'><%#FormatCurr(Eval("C1"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C2"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C3"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C4"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C5"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C6"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C7"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C8"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C9"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C10"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C11"))%></td>
        <td style='text-align:right; width:100px;'> <%#FormatCurr(Eval("C12"))%></td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    </table>

</div>

</form>
</body>
</html>
