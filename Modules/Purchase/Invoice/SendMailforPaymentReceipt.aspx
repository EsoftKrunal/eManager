<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMailforPaymentReceipt.aspx.cs" Inherits="Modules_Purchase_Invoice_SendMailforPaymentReceipt" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/Common.js"></script>
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
    </style>
     <script type="text/javascript">
         function OpenDocument() {
             // alert('Hi');
             var ID = document.getElementById('hdnDocId').value;
             var PVNo = document.getElementById('hdnPVno').value;
             //alert(ID);
             //alert(PVNo)
             window.open("../Requisition/ShowDocuments.aspx?DocId=" + ID + "&PVNo=" + PVNo + "&PRType=PaymentDocument");
         }
     </script>
</head>
<body>
  <form id="form1" runat="server">
    <div style="font-family:Arial, Helvetica, sans-serif;font-size:12px; ">
    <center>
     <h1><asp:Label ID="lblHeader" runat="server"></asp:Label></h1>
     <h3><asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
         <asp:HiddenField ID="hdnDocId" runat="server" Value="" />
         <asp:HiddenField ID="hdnPVno"  runat="server" Value="" />
     </h3>
        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:800px;border-collapse:collapse;border:solid 1px Black" >
            <col style="text-align:right" />
            <col style="text-align:right" />
            <col style="text-align:left" />
            <col style="text-align:left" />
            <tr>
                <td >&nbsp;</td>
                <td style="text-align :left">From :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtFrom" Width="500px" Enabled="false"></asp:TextBox> </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">To :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtTo" Width="500px" Enabled="false"></asp:TextBox></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">CC :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtCC" Width="500px" Enabled="false"></asp:TextBox> </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">Bcc :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtBCC" Width="500px" Enabled="false"></asp:TextBox> </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">Subject :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtSubject" Width="500px"></asp:TextBox> </td>
                <td>&nbsp;</td>
            </tr>
              <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">Payment Voucher :</td>
                <td style="text-align :left"> <a id="aFile" runat="server" target="_blank"> <img src="../../HRD/Images/paperclip.gif" style="border:solid 0px red;"/></a> </td>
                <td>&nbsp;</td>
            </tr>
              <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">Other Attachment :</td>
                <td style="text-align :left">  <a onclick='OpenDocument()' style="cursor:pointer;">
                <img src="../../HRD/Images/paperclip12.gif" /></a>
                    </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" class="style1"><b>Mail Message :</b></td>
            </tr>
            <tr>
                <td colspan="4" style=" text-align:left" >
                <asp:HiddenField runat="server" ID="hfdMessage" /> 
                    <div contenteditable="true" runat="server" id="dvContent">
                        <%--<asp:Literal runat="server" ID="litMessage"></asp:Literal>--%> 
                        <asp:TextBox runat="server" ID="litMessage" TextMode="MultiLine" Height="200px" Width="98%"></asp:TextBox>
                    </div>
                </td>
            </tr>
              <tr>
                <td colspan="4">
                    <asp:Button runat="server" ID="btnSend" Text="Send Mail" onclick="btnSend_Click" OnClientClick="hfdMessage.value=dvContent.innerHTML;" CssClass="btn" />
                </td>
            </tr>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
