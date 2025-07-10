<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Performance_Details.aspx.cs" Inherits="Emtm_Performance_Details" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<script src="../JS/jquery.min.js"></script>
<style>
.grey1
{
    background-color:#666;
    color:White;
}
.grey2
{
    background-color:#EBF0FA;
    border:solid 1px #ddd;
}
.grey1-1
{
    background-color:#fff;
    color:#333;
    border:solid 1px #ddd;
}

.rightSec .row span:first-child {
border-left: 1px solid #d2d2d2;
}
.center
{
    text-align:center;
}
.col0
{background-color:#D1F0FF;color:#333;border:solid 1px #ddd;}
.col1
{background-color:#FFEBE0;color:#333;border:solid 1px #ddd;}
.col2
{background-color:#B2F0D1;color:#333;border:solid 1px #ddd;}
.col3
{background-color:#FFFFB2;color:#333;border:solid 1px #ddd;}

.success
{
    background: #EBFAEB;
    border:solid 1px #ddd;
    overflow:hidden;
    word-break:keep-all;
    /*url('../Images/favicon.png') no-repeat center;*/
}
.error
{
    background: #FFEBE6;
    border:solid 1px #ddd;
    overflow:hidden;
    word-break:keep-all;
    /*url('../Images/message_exclamation.png') no-repeat center;*/
}
.error_can_corrected
{
    background: #FFFF80;
    border:solid 1px #ddd;
    overflow:hidden;
    word-break:keep-all;
    /*url('../Images/message_exclamation.png') no-repeat center;*/
}
</style>
</head>
<body style="font-family:Calibri; font-size:12px; margin:0px;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="position:fixed;top:0px;width:100%; background-color:White; border-bottom:solid 1px grey; height:90px;">
            <div style="padding:10px; text-align:center; background-color:#194775;color:White; font-size:18px;" >
                <asp:Label runat="server" ID="lblpagename"></asp:Label>
            </div>
              <asp:Panel runat="server" ID="pnlFixedHeader" Visible="true">
              <table width="100%" cellpadding="3" cellspacing="0" border="0" style="border-collapse:collapse" >
              <tr>
              <td class="grey1-1 center" style="width:30px">SR#</td>
              <td class="grey1-1 center" style="width:70px"><asp:Label Text="" ID="lblDataType" runat="server"></asp:Label> </td>
              <td class="grey1-1 center" style="width:70px">From Date</td>
              <td class="grey1-1 center" style="width:70px">To Date</td>
              <asp:Literal runat="server" ID="litHeader"></asp:Literal>
              <td>&nbsp;</td>
              </tr>
              </table>
              </asp:Panel>
              <asp:Panel runat="server" ID="pnlRandomHeader" Visible="false">
              <table width="100%" cellpadding="3" cellspacing="0" border="0" style="border-collapse:collapse" >
              <tr>
              <td class="grey1-1 center" style="width:30px">SR#</td>
              <td class="grey1-1 center" style="">Vessel</td>
              <td class="grey1-1 center" style="width:120px">Inspection #</td>
              <td class="grey1-1 center" style="width:80px">Arrival Dt.</td>
              <td class="grey1-1 center" style="width:80px">Notify Date</td>
              <td class="grey1-1 center" style="width:70px">Status</td>
              </tr>
              </table>
              </asp:Panel>
        </div>
        <div style="margin-top:90px;margin-bottom:90px">
            <asp:Panel runat="server" ID="pnlFixed" Visible="true">
            <table width="100%" cellpadding="3" cellspacing="0" border="0" style="border-collapse:collapse">
              
              <asp:Repeater runat="server" ID="rptData">
              <ItemTemplate>
              <tr>
              <td class="grey1-1 center" style="width:30px"><%#Eval("SRNO")%></td>
              <td class="grey1-1 center" style="width:70px"><%#Eval("COLNAME")%></td>
              <td class="grey1-1 center" style="width:70px"><%#Common.ToDateString(Eval("PERIODFROM"))%></td>  
              <td class="grey1-1 center" style="width:70px"><%#Common.ToDateString(Eval("PERIODTO"))%></td>  
              <asp:Literal runat="server" ID="litData" Text='<%#GetVesselDetails(Eval("SRNO"),Eval("PERIODFROM"),Eval("PERIODTO"))%>'></asp:Literal>
              <td>&nbsp;</td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              
              </table>  
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlRandom" Visible="false">
            <table width="100%" cellpadding="3" cellspacing="0" border="0" style="border-collapse:collapse">
            <asp:Repeater runat="server" ID="rptRandom">
              <ItemTemplate>
              <tr>
              <td class="grey1-1 center" style="width:30px"><%#Eval("SRno")%></td>
              <td class="grey1-1 center" style=""><%#Eval("VesselName")%></td>
              <td class="grey1-1 center" style="width:120px"><%#Eval("InspectionNo")%></td>
              <td class="grey1-1 center" style="width:80px"><%#Common.ToDateString(Eval("ActToDt"))%></td>
              <td class="grey1-1 center" style="width:80px"><%#Common.ToDateString(Eval("NotofiedOn"))%></td>
              <td class='grey1-1 center <%#(Common.CastAsInt32(Eval("Status"))==1)?"success":"error"%>' style="width:70px"></td>  
              </tr>
              </ItemTemplate>
              </asp:Repeater>
              </table>
            </asp:Panel>
        </div>
        <div style="position:fixed;bottom:0px;width:100%; background-color:White;">

     <div style="padding:2px; text-align:center; background-color:#194775;color:White; font-size:18px;" ></div>
     <table width="100%" cellpadding="3" cellspacing="0" border="0" style="border-collapse:collapse" >
              <tr>
              <td class="grey1-1 center" style="width:30px">&nbsp;</td>
              <td class="grey1-1 center" style="width:70px">&nbsp;</td>
              <td class="grey1-1 center" style="width:70px"></td>
              <td class="grey1-1 center" style="width:70px">Total</td>
              <asp:Literal runat="server" ID="Literal1"></asp:Literal>
              <td>&nbsp;</td>
              </tr>
              </table>
     <div style="padding:2px; text-align:center; background-color:#194775;color:White; font-size:18px;" ></div>
        <div style="padding:3px; text-align:left;font-size:18px; text-align:left;white-space: nowrap;">
            <div style="width:300px; text-align:left;  background-color:#EBFAEB; overflow:auto; display:inline;">
                <span style=" float:left;padding:5px; width:30px;" class="success"></span>
                <span style="float:left;padding:5px;">Compliance</span>
            </div>
            <div style="width:300px;text-align:left; overflow:auto; display:inline;">
                <span style="display:inline-block;padding:5px; width:30px;" class="error"></span>
                <span style="display:inline-block;padding:5px; ">Non Compliance</span>
            </div>
            <div style="width:300px;text-align:left; overflow:auto; display:inline;">
                <span style="display:inline-block;padding:5px; width:30px;background-color:#EBF0FA"></span>
                <span style="display:inline-block;padding:5px; ">Not Applicable</span>
            </div>
            <div style="width:300px;text-align:left; overflow:auto; display:inline;">
                <span style="display:inline-block;padding:5px; width:30px;background-color:#FFFF80"></span>
                <span style="display:inline-block;padding:5px; ">Due</span>
            </div>
            
        </div>
        </div>
    </div>
    </form>
</body>
</html>
