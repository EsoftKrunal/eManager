<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InviceDetailsPopUp.aspx.cs" Inherits="InviceDetailsPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            color: White;
            background-color: #4371a5;
            font-weight: bold;
            text-align: center;
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%; border-collapse : collapse" cellpadding ="2" rules="rows" cellspacing="0" border ="1">
            <tr>
                <td colspan="3" class="text headerband" style="text-align:center; padding-top :5px;" >Invoice Details </td>
            </tr>
            <tr>
                <td style=" width:100px"  >Vendor Name : </td>
                <td>
                    <asp:Label runat="server" id="lblVendor"></asp:Label> </td>
                <td>
                    &nbsp;</td>
            </tr>
           
            <tr>
                <td>Ref. No. :</td>
                <td>
                    <asp:Label runat="server" id="lblRefNo"></asp:Label></td>
                <td>
                    &nbsp;</td>
            </tr>
           
            <tr>
                <td>Invoice No :</td>
                <td>
                    <asp:Label runat="server" id="lblInvNo"></asp:Label></td>
                <td>
                    &nbsp;</td>
            </tr>
           
            <tr>
                <td>Currency :</td>
                <td>
                    <asp:Label runat="server" id="lblCurrency"></asp:Label></td>
                <td>
                    &nbsp;</td>
            </tr>
           
            <tr>
                <td>Due Date :</td>
                <td>
                    <asp:Label runat="server" id="lblDueDt"></asp:Label></td>
                <td>
                    &nbsp;</td>
            </tr>
           
            <tr>
                <td>Invoice Amount :</td>
                <td>
                    <asp:Label runat="server" id="lblInvAmt"></asp:Label></td>
                <td>
                    &nbsp;</td>
            </tr>
        
        </table>
    
    </div>
    </form>
</body>
</html>
