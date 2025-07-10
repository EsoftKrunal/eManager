<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeHistory.aspx.cs" Inherits="ChangeHistory" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .bordered tr td{
            border:solid 1px #d9edfc;
            padding:6px;
        }
    </style>

</head>
<body style="margin:0px" >
    <form id="form1" runat="server">
        <div style="padding:8px; text-align:center;" class="text headerband">
            <asp:Label ForeColor="White" runat="server" ID="lblPONUM" Font-Size="18px"></asp:Label>
        </div>
        <div style="font-family:Arial;font-size:12px;">
            <div>
                <table width="100%" class="bordered" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
                    <tr style="font-weight:bold;" class= "headerstylegrid">
                        <td style="width:50px;text-align:center">Sr#</td>
                        <td style="width:150px;">UserName</td>
                        <td>Action Taken</td>
                        <td style="width:140px;text-align:center">Action Time</td>
                        <td>User Comments</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rpt">
                        <ItemTemplate>
                             <tr>
                                <td style="text-align:center"><%#Eval("sno")%></td>
                                <td><%#Eval("UserName")%></td>
                                <td><%#Eval("ActionTaken")%></td>
                                <td style="text-align:center"><%#ECommon.ToDateTimeString(Eval("ActionTime"))%></td>
                                <td><%#Eval("UserComments")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
