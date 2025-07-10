<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverDueInvoiceList.aspx.cs" Inherits="Invoice_OverDueInvoiceList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="JS/AutoComplete/jquery-ui.css" />
     <script src="JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
     <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:12px;
        margin:0px;
    }
    
    </style>
     <script type="text/javascript">
         function viewInv(InvId) {

             winref = window.open('ViewInvoice.aspx?InvoiceId=' + InvId, '', '');
             return false;
         }
         </script> 
  <script type="text/javascript" src="JS/KPIScript.js"></script>
</head>
<body style="font-family:Calibri; font-size:13px;">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div>
    <center>
    <div style="border:solid 1px #008AE6;">
    <%--<asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>--%>
    <div>
    </div>
       <div style="  padding:10px;" class="text headerband">
             Overdue Invoices
       </div>
       <div style=" border-top:solid 2px #c2c2c2">
       <table border="0" cellpadding="0" cellspacing="0" style="width:100%; border-collapse:collapse;">
        <tr>
       <td style="vertical-align:top;" >
       <asp:UpdatePanel runat="server" ID="re">
            <ContentTemplate>
                <div id="dv_grd" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
            <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:100px; text-align:center;"/>
                                                <col />
                                                <col style="width:350px;" />
                                                <col style="width:90px;" />
                                                <col style="width:90px;" />
                                                <col style="width:90px;" />
                                                <col style="width:40px;" />
                                                <col style="width:100px;" />
                                                <col style="width:50px;" />
                                                <col style="width:80px;" />
                                                <col style="width:80px;" />
                                                <col style="width:25px;"/>

                                                <tr align="left" class= "headerstylegrid" >
                                                    <td>Ref #</td>
                                                    <td>Vendor</td>
                                                    <td>Inv #</td>
                                                    <td>Inv Dt.</td>
                                                    <td>Recd Dt.</td>
                                                    <td>Inv Amt</td>
                                                    <td>Curr.</td>
                                                    <td>Payment Due</td>
                                                    <td>Vessel</td>
                                                    <td>Status</td>
                                                    <td>Stage</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                    </div>
            <div id="dv_grd" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 550px ; text-align:center; border-bottom:none;" class="ScrollAutoReset">
            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                                    <col style="width:100px;"/>
                                                    <col />
                                                    <col style="width:350px;" />
                                                    <col style="width:90px;" />
                                                    <col style="width:90px;" />
                                                    <col style="width:90px;" />
                                                    <col style="width:40px;" />
                                                    <col style="width:100px;" />
                                                    <col style="width:50px;" />
                                                    <col style="width:80px;" />
                                                    <col style="width:80px;" />
                                                    <col style="width:25px;"/>
                                                </colgroup>
                                                <asp:Repeater ID="RptMyInvoices" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="left"><a onclick='viewInv(<%#Eval("InvoiceId")%>);' href="#"  > <%#Eval("RefNo")%></a></td>
                                                            <td align="left"><%#Eval("Vendor")%></td>                                                            
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("EnteredOn"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="left"><%#Eval("Currency")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("DueDate"))%></td>
                                                            <td align="left"><%#Eval("INVvesselCode")%> </td>
                                                            <td align="left"><%#Eval("Status")%></td>   
                                                            <td align="left"><%#Eval("StageText")%></td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                       <tr style="background-color:#E6F3FC">
                                                            <td align="left"><a onclick='viewInv(<%#Eval("InvoiceId")%>);' href="#"  > <%#Eval("RefNo")%></a></td>
                                                            <td align="left"><%#Eval("Vendor")%></td>                                                            
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("EnteredOn"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="left"><%#Eval("Currency")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("DueDate"))%></td>
                                                            <td align="left"><%#Eval("INVvesselCode")%> </td>
                                                            <td align="left"><%#Eval("Status")%></td>   
                                                            <td align="left"><%#Eval("StageText")%></td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                             </table>
            </div>
            </ContentTemplate>
       </asp:UpdatePanel>
        </td>
        </tr>
       </table>
       <div style="border-top:solid 1px grey; padding:5px;">
        <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                  <td style="">
                  <asp:Label runat="server" ID="lblCount"></asp:Label>
                     <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" 
                          style=" background-color:red; color:White; border:none; padding:4px;" 
                          OnClientClick="window.close()" />
                  </td>
                </tr>
              </table>
              </div>
       </div>
       <%--</ContentTemplate>
       </asp:UpdatePanel>--%>
    </center>
    </div>
    <script type="text/javascript">
        var last = null;
        function showdetails(ctl, userid, days) {
            
            if (last != null)
                $(last).css({ "background-color": "white" });

            $(ctl).parent().css({ "background-color": "#e5e600" });
            $("#hfdUserId").val(userid);
            $("#hfdDays").val(days);
            $("#btnPost").click();

            last = $(ctl).parent();

        }
    </script>
    </form>
</body>

</html>
