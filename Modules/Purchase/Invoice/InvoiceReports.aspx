<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceReports.aspx.cs" Inherits="Invoice_InvoiceReports" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="JS/AutoComplete/jquery-ui.css?11" />
     <script src="JS/AutoComplete/jquery-ui.js?11" type="text/javascript"></script>
     <script type="text/javascript">
         function RegisterAutoComplete() {
             $(function () {
                 //------------
                 function log(message) {
                     $("<div>").text(message).prependTo("#log");
                     $("#log").scrollTop(0);
                 }
                 //------------

                 $("#txtF_Vendor").autocomplete(
                 {
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtF_Vendor").val(), Type: "VENALL" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.SupplierName, value: item.SupplierName, id: item.SupplierId,active:item.Active} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         log(ui.item ? "Selected: " + ui.item.label : "Nothing selected, input was " + this.value);
                         //                         $("#hfdSupplier").val(ui.item.id);
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
                 //---------------
             });
         }
         function getBaseURL() {

             var url = window.location.href.split('/');
             var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
             return baseUrl;
         }
     </script>
     <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:12px;
        margin:0px;
    }
    
         .style2
         {
             width: 99px;
         }
    
    </style>
    <script type="text/javascript" src="JS/KPIScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div>
    <center>
    <div style="border:solid 1px #008AE6;">
    <asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" width="100%" >
         <tr>
         <td style="  padding:6px;" class="text headerband">
             <strong>Invoice Reports</strong></td>
         </tr>
            <%--<tr>
                <td style="background-color: #E6F3FC">
                    
                </td>
            </tr>--%>
         <tr>
         <td style="background-color:#E6F3FC">

            <table id="tblRT_1"  runat="server"  cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
            <td style="text-align:right; width:100px; ">Entered By :</td>
            <td style="text-align:left; width:170px; "><asp:DropDownList ID="ddlEnteredBy" runat="server" Width="170px"></asp:DropDownList></td>
            <td style="text-align:right; width:100px; ">Processed By :</td>
            <td style="text-align:left; width:170px; "><asp:DropDownList ID="ddlProcessedBy" runat="server" Width="170px"></asp:DropDownList></td>
            <td style="text-align:right;" class="style2">Paid By : </td>
            <td style="text-align:left;"><asp:DropDownList ID="ddlPaidBy" runat="server" Width="170px"></asp:DropDownList></td>            
            <td style="width:120px;text-align:right;">Received Period :</td>
            <td style="text-align: left">
                <asp:TextBox ID="txtRecFrom" runat="server" style="border:solid 1px #008AE6;" Width="80px"></asp:TextBox>
                &nbsp;<asp:CalendarExtender ID="txtRecFrom_CalendarExtender" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtRecFrom">
                </asp:CalendarExtender>
                <asp:TextBox ID="txtRecTo" runat="server" style="border:solid 1px #008AE6;" 
                    Width="80px"></asp:TextBox>
                <asp:CalendarExtender ID="txtRecTo_CalendarExtender" runat="server" 
                    Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtRecTo">
                </asp:CalendarExtender>
                </td>
            <td></td>
            </tr>
                <tr>
                    <td style="text-align:right; width:100px; ">Vendor&nbsp; :</td>
                    <td style="text-align:left; " colspan="5"><asp:TextBox ID="txtF_Vendor" runat="server" style="border:solid 1px #008AE6;" Width="98%"></asp:TextBox></td>
                    <td  style="text-align:right;">
                        Type :
                    </td>
                    <td colspan="2" style="text-align:left;">
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="1" Text="Invoice Report"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Analysis Report"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            <tr>
                <td style="text-align:right; width:100px;">Owner : </td>
                <td style="text-align:left; width:170px;"><asp:DropDownList ID="ddlOwner"  runat="server" Width="170px"></asp:DropDownList></td>
                <td style="text-align:right; width:100px;">Vessel : </td>
                <td style="text-align:left; width:170px;"><asp:DropDownList ID="ddlVessel" runat="server" Width="170px"></asp:DropDownList></td>
                <td style="text-align:right; " class="style2">
                    <%--Action Period :--%> 

                </td>
                <td style="text-align:left;">
                   <%-- <asp:TextBox ID="txt_FDate1" runat="server" Width="80px" style="border:solid 1px #008AE6;" ></asp:TextBox>&nbsp;
                    <asp:TextBox ID="txt_FDate2" runat="server" Width="80px" style="border:solid 1px #008AE6;" ></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_FDate1"></asp:CalendarExtender>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_FDate2"></asp:CalendarExtender>--%>
                </td>
                <td style="text-align:right;">Stage : </td>
                <td style="text-align:left">
                <asp:DropDownList ID="ddlStage" runat="server" Width="170px">
                    <asp:ListItem Text="" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Entry" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Approval" Value="1"></asp:ListItem>
<asp:ListItem Text="Verification" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Payment" Value="3"></asp:ListItem>
                </asp:DropDownList></td>
                <td >
                    <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" OnClientClick="this.value='Loading..';" style="  border:none; padding:4px;" Text="Search" Width="70px" CssClass="btn" />
                </td>
                
            </tr>
            </table>
         </td>
         </tr>
         <tr>
           <td>
               <iframe id="frReport" height="500px" width="100%" scrolling="yes"  runat="server"></iframe>
           </td>
         </tr>
       </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </center>
    </div>
    </form>
</body>
<script type="text/javascript">
    function Page_CallAfterRefresh() {
        RegisterAutoComplete();
    }
    $(document).ready(function () {
        RegisterAutoComplete();
    });
</script>
</html>
