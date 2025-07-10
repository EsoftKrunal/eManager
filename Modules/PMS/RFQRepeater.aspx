<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RFQRepeater.aspx.cs" Inherits="RFQRepeater" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel runat="server" id="pnlContent">
        <div style="font-size:18px; height:24px; overflow:hidden; text-overflow: ellipsis; color:Blue;"><b>Job Code : <asp:Label runat="server" ID="lblJobCode"></asp:Label> | </b><asp:Label runat="server" ID="lblJobName"></asp:Label></div>
        <table style="width:100%; border-collapse:collapse" cellpadding="0" cellspacing="0" border="1" rules="all">
        <tr style='background-color:#3399FF; color:White; height:25px; font-weight:bold;'>
                <td>RFQNo</td>
                <td style="width:100px; text-align:center;">Sup Qty</td>
                <td style="width:100px; text-align:center;">Unit Price ($) </td>
                <td style="width:100px; text-align:center;">Disc(%)</td>
                <td style="width:100px; text-align:center;">Total Amount ($)</td>
        </tr>
        </table>
        <asp:Repeater runat="server" ID="rptRFQs">
        <ItemTemplate>
        <table style="width:100%; border-collapse:collapse" cellpadding="0" cellspacing="0" border="1" rules="all">
            <tr>
                <td><b><%#Eval("RFQNO")%> : <%#Eval("YardName")%></b></td>
                <td style="width:100px"><input class="newinput" type="text" id='QTY' maxlength="10" style='width:95%; background-color:#FFFFE0;' onkeypress='fncInputNumericValuesOnly(event)' onchange='Update_Values(this);' RFQId='<%#Eval("RFQId")%>' value='<%#Eval("SUPQty")%>' /></td>
                <td style="width:100px"><input class="newinput" type="text" id='UP' maxlength="10" style='width:95%; background-color:#FFFFE0;' onkeypress='fncInputNumericValuesOnly(event)' onchange='Update_Values(this);' RFQId='<%#Eval("RFQId")%>' value='<%#Eval("SUPUnitPrice_USD")%>' /></td>
                <td style="width:100px"><input class="newinput" type="text" id='DISC' style='width:95%; background-color:#FFFFE0;' onkeypress='fncInputNumericValuesOnly(event)' onchange='Update_Values(this);' RFQId='<%#Eval("RFQId")%>' maxlength="3" value='<%#Eval("SUPDiscountPer")%>' /></td>
                <td style="text-align:right;width:100px""><span id='NET'><%#Eval("SUPNetAmount_USD")%></span></td>
            </tr>
            <tr>
            <td colspan="5">
                <div style='color:Red; font-style:italic;'>
                    <%#Eval("VendorRemarks")%>
                </div>
                <textarea style='width:98%; background-color:#FFFFE0;' rows="2" id="txRem" onchange='Update_Values(this);' RFQId='<%#Eval("RFQId")%>'><%#Eval("SUPRemarks")%></textarea>            
            </td>
            </tr>
        </table>

        </ItemTemplate>
        </asp:Repeater>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
