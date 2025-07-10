<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NonPoData.aspx.cs" Inherits="Modules_Purchase_Invoice_AddNonPoData" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link rel="Stylesheet"  type="text/css" href="../../HRD/Styles/StyleSheet.css"/>
     <script type="text/javascript" src="../JS/jquery.min.js"></script>
     <script src="../JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <link rel="stylesheet" href="../JS/AutoComplete/jquery-ui.css?11" />
     <script src="../JS/AutoComplete/jquery-ui.js?11" type="text/javascript"></script>
        <link rel="stylesheet" href="../JS/AutoComplete/jquery-ui.css" />
     <script src="../JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
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
                                 url: getBaseURL() + "/Purchase/Invoice/getautocompletedata.ashx",
                                 dataType: "json",
                                 headers: { "cache-control": "no-cache" },
                                 type: "POST",
                                 data: { Key: $("#txtF_Vendor").val(), Type: "VEN" },
                                 cache: false,
                                 success: function (data) {
                                     response($.map(data.geonames, function (item) { return { label: item.SupplierName, value: item.SupplierName, id: item.SupplierId } }
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
  <script type="text/javascript" src="JS/KPIScript.js"></script>
    <style type="text/css">
    .style1
    {
        text-align :left; 
        font-size :13px;  
        font-family:Arial Unicode MS; 
        color :#222222; 
        padding :5px; 
        border-style:none;
        text-align :left; 
        width:600px;
    }
    .style2
    {
    	text-align :left; 
        font-size :13px;  
        font-family:Comic Sans MS; 
        color :Red; 
    }
    .gridheadings
    {
    	background-color :#008AE6;
    	color : White ;
    	font-size :13px; 
    	border :dotted 1px Black;
    	padding :2px;
    }
    </style>
    <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:12px;
        margin:0px;
    }
    .round
    {
          background: #FFD6D6;
          border-radius: 28px;
          font-family: Arial;
          color: Black;
          font-size: 13px;
          padding: 10px 20px 10px 20px;
          text-decoration: none;
          width:70px;
    }
    .round1
    {
          background: #99CCFF;         
          border-radius: 28px;
          font-family: Arial;
          color: #000000;
          font-size: 13px;
          padding: 10px 20px 10px 20px;
          text-decoration: none;
}    
    </style>
    <script type="text/javascript" >
        //         background: #3498db;
        function OpenAddWindow() {
            winref = window.open('../Invoice/AddInvoiceEntry.aspx', '', '');
            return false;
        }

        function OpenApprovalWindow(w) {
            var InvId = w.getAttribute("invid");
            winref = window.open('../Invoice/InvoiceEntry.aspx?InvId=' + InvId, '', '');
            return false;
        }

        function download(m) {
            var getvalue = m.getAttribute("invid");
            //             alert(getvalue);
            var hf = document.getElementById("hfInvId");
            var btn = document.getElementById("btndownload");
            hf.value = getvalue;
            //             alert(hf.value);
            btn.click();
        }
        function EditInv(InvId) {

            winref = window.open('AddInvoiceEntry.aspx?InvoiceId=' + InvId, '', '');
            return false;
        }
        function viewInv(InvId) {

            winref = window.open('ViewInvoice.aspx?InvoiceId=' + InvId, '', '');
            return false;
        }
        function PrintVoucher(InvId) {

            winref = window.open('PaymentVoucher.aspx?PaymentId=' + InvId + '&PaymentMode=O', '', '');
            return false;
        }


    </script>   
      <script type="text/javascript" language="javascript">
          function refreshParent() {
              if (window.opener.document.getElementById('btnSearch') != null) {
                  window.opener.document.getElementById('btnSearch').click();
              }
          }
      </script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
       <div id="log" style="display:none"></div>
    <div>
    <center>
        <table cellpadding="6" cellspacing="0" width="100%">
         <tr>
         <td style="  padding:10px;" class="text headerband">
             <strong>Search Non-PO Invoices</strong>
         </td>
         </tr>
            <tr>
                <td>
                   <table width="100%" cellpadding="0" cellspacing="3" border="0">
                        <tr>
                            <td valign="top">
                                <%--<div class="gridheadings">My Invoices</div>--%>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>                                        
                                    <div id="divApproval" runat="server" style="text-align:left">
                                      <table border="1" bordercolor="#008AE6" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:60px;  border-collapse:collapse;">
                                            <colgroup>
                                             <col style="width:80px; text-align:center;"/>
                                             <col />
                                             <col style="width:80px;" />
                                             <col style="width:80px;" />
                                             <col style="width:150px;" />
                                             <col style="width:150px;" />
                                             <col style="width:100px;" />
                                             <col style="width:100px;" />
                                             <col style="width:200px;" />
                                             </colgroup>
                                                <tr align="center" style="  color:black; " > <%--background-color:#008AE6--%>
                                                <td>Ref #<asp:TextBox ID="txtF_RefNo" Width="95%"  style="border:solid 1px #008AE6;" runat="server"></asp:TextBox> </td>
                                                <td>Vendor<asp:TextBox ID="txtF_Vendor" Width="98%"  style="border:solid 1px #008AE6;" runat="server"></asp:TextBox> </td>
                                                <td>Inv #<asp:TextBox ID="txtF_InvNo" Width="95%"  style="border:solid 1px #008AE6;" runat="server"></asp:TextBox> </td>
                                                <td>Curr #<asp:DropDownList ID="ddCurrency"  runat="server" CssClass="input_box" Width="60px"></asp:DropDownList></td>
                                                <td>Owner<asp:DropDownList ID="ddlF_Owner" runat="server"  Width="95%" OnSelectedIndexChanged="ddlF_Owner_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                <td>Vessel<asp:DropDownList ID="ddlF_Vessel" runat="server"  Width="95%"></asp:DropDownList></td>
                                                <td>Status<asp:DropDownList ID="ddlF_Status"   runat="server"  Width="95%">
                                                             <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                             <asp:ListItem Text="Paid" Value="Paid"></asp:ListItem>
                                                             <asp:ListItem Text="UnPaid" Value="UnPaid"></asp:ListItem>
                                                             <%--<asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>--%>
                                                        </asp:DropDownList></td>
                                                <td>Stage<asp:DropDownList ID="ddlF_Stage"  runat="server"  Width="95%">
                                                             <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                             <asp:ListItem Text="Entry" Value="0"></asp:ListItem>
                                                             <asp:ListItem Text="Processing" Value="1"></asp:ListItem>
                                                             <asp:ListItem Text="Approval" Value="2"></asp:ListItem>
                                                             <asp:ListItem Text="Payment" Value="3"></asp:ListItem>
                                                        </asp:DropDownList></td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="btnReset" ImageUrl="~/Modules/HRD/Images/reset.png" OnClick="btnReset_Click" ToolTip="Clear" />
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="ShowMyInvoices" CssClass="btn" />&nbsp;
                                                       
                                                    
                                                    </td>
                                                </tr> 
                                                
                                          
                                                </table>
                                    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:7%; text-align:center;"/>
                                                <col style="width:18%;"/>
                                                <col style="width:12%;" />
                                                <col style="width:7%;" />
                                                <col style="width:7%;" />
                                                <col style="width:4%;" />
                                                <col style="width:9%;" />
                                                <col style="width:5%;" />
                                               <%-- <col style="width:11%;" />--%>
                                                <col style="width:7%;" />
                                                <col style="width:2%;"/>
                                                <col style="width:5%;"/>
                                                <tr align="left" class= "headerstylegrid" >
                                                  
                                                    <%--<td></td>--%>
                                                    <td>Ref #</td>
                                                    <td>Vendor</td>
                                                    <td>Inv #</td>
                                                    <td>Inv Dt.</td>
                                                    <td>Inv Amt</td>
                                                    <td>Curr.</td>
                                                    <td>Payment Due</td>
                                                    <td>Vessel</td>
                                                   <%-- <td>Company Voucher #</td>--%>
                                                    <td>Stage</td>
                                                    <td></td>
                                                    <td>AP Entries</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>         
                                            <div id="divLeavesApproval" runat="server" class="ScrollAutoReset" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 245px; text-align:center;">
                                            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                                <%--<col style="width:25px;"/>--%>
                                                <col style="width:7%; text-align:center;"/>
                                                <col style="width:18%;"/>
                                                <col style="width:12%;" />
                                                <col style="width:7%;" />
                                                <col style="width:7%;" />
                                                <col style="width:4%;" />
                                                <col style="width:9%;" />
                                                <col style="width:5%;" />
                                               <%-- <col style="width:11%;" />--%>
                                                <col style="width:7%;" />
                                                <col style="width:2%;"/>
                                                    <col style="width:5%;"/>
                                                </colgroup>
                                                <asp:Repeater ID="RptMyInvoices" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="left"><a onclick='viewInv(<%#Eval("InvoiceId")%>);' href="#"  > <%#Eval("RefNo")%></a></td>
                                                            <td align="left"><%#Eval("Vendor")%> <b style='color:Red'><i> ( <%#Eval("VendorCode")%> ) </i></b></td>                                                            
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="left"><%#Eval("Currency")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("DueDate"))%></td>
                                                            <td align="left"><%#Eval("INVVesselCode")%> </td>
                                                          <%--  <td align="left"><a onclick='PrintVoucher(<%#Eval("PAYMENTID")%>);'  href="#" style='<%#(( Eval("Status").ToString().Trim() == "Paid" && Eval("Stage").ToString().Trim() == "3")) ? "" : "display:none" %>'  ><%#Eval("VoucherNo")%></a></td> --%>  
                                                            <td align="left"><%#Eval("StageText")%></td>
                                                            <td style="text-align:center;">
                                                              <img id="Img3" src="~/Modules/HRD/Images/paperclip12.gif" onclick="download(this);" invid='<%#Eval("InvoiceId")%>' style='<%#(Eval("AttachmentName").ToString() != "") ? "" : "visibility:hidden" %>'  title="download attachment" runat="server" alt=""  />
                                                                
                                                               
                                                             
                                                            </td>
                                                            <td align="left">
                                                                <asp:LinkButton ID="lbApEntries" runat="server" CommandArgument='<%#Eval("NonPoId")%>' Visible='<%#(Eval("jeid").ToString()=="0")%>' OnClick="lbApEntries_Click" Text="ADD" ></asp:LinkButton>
                                                             
                                                             
                                                                
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                       <tr style="background-color:#E6F3FC" >                  
                                                            <td align="left"><a onclick='viewInv(<%#Eval("InvoiceId")%>);' href="#"  > <%#Eval("RefNo")%></a></td>
                                                            <td align="left"><%#Eval("Vendor")%> <b style='color:Red'><i> ( <%#Eval("VendorCode")%> ) </i></b></td> 
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="left"><%#Eval("Currency")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("DueDate"))%></td>
                                                            <td align="left"><%#Eval("INVVesselCode")%> </td>
                                                          <%--  <td align="left"><a onclick='PrintVoucher(<%#Eval("PAYMENTID")%>);'  href="#" style='<%#(( Eval("Status").ToString().Trim() == "Paid" && Eval("Stage").ToString().Trim() == "3")) ? "" : "display:none" %>'  ><%#Eval("VoucherNo")%></a></td>   --%>
                                                            <td align="left"><%#Eval("StageText")%></td>
                                                            <td style="text-align:center;">
                                                              <img id="Img3" src="~/Modules/HRD/Images/paperclip12.gif" onclick="download(this);" invid='<%#Eval("InvoiceId")%>' style='<%#(Eval("AttachmentName").ToString() != "") ? "" : "visibility:hidden" %>'  title="download attachment" runat="server" alt=""  />
                                                                
                                                             
                                                             
                                                            </td>
                                                             <td align="left">
                                                                <asp:LinkButton ID="lbApEntries" runat="server" CommandArgument='<%#Eval("NonPoId")%>' Visible='<%#(Eval("jeid").ToString()=="0")%>' OnClick="lbApEntries_Click" Text="ADD" ></asp:LinkButton>
                                                             
                                                             
                                                                
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                             </table>
                                        </div>
                                    </div>
                                    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="dvNonPoEntry" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:850px;padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:25px; opacity:1;filter:alpha(opacity=100)">
                <center>
                    <div style=" float :right " >
                        <asp:ImageButton runat="server" ID="btnNonPo_ClosePopup" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." OnClick="btnNonPo_ClosePopup_Click" /> </div>
                    <div style="font-size:15px;padding:6px;font-weight:bold;" class="text headerband">
                        ADD AP Entries for Non-PO Invoice Approval [ ID : <asp:Label ID="lblNonPoId" runat="server"></asp:Label>]
                       
                    </div>  
                    <table border="0" cellpadding="6" cellspacing="0" style="height: 250px; text-align: left; border-collapse:collapse; width:100%;">
                                <tr>
                                    <td style="width:15%;">
                                        <b> Ref No # : </b> 
                                    </td>
                                    <td style="width:35%;">
                                        <asp:Label ID="lblInvRefNo" runat="server" ></asp:Label> 
                                    </td>
                                    <td style="width:15%;">
                                        <b> Invoice No : </b>
                                    </td>
                                    <td style="width:35%;">
                                         <asp:Label ID="lblInvoiceNo" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                   <td style="width:15%;">
                                      <b> Supperlier :  </b>
                                    </td>
                                    <td style="width:35%;">
                                        <asp:Label ID="lblSupplierName" runat="server" ></asp:Label> 
                                    </td>
                                    <td style="width:15%;">
                                       <b> Vessel Name :  </b>
                                    </td>
                                    <td style="width:35%;">
                                         <asp:Label ID="lblVesselName" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                   <td style="width:15%;">
                                      <b> Inv Amount :  </b>
                                    </td>
                                    <td style="width:35%;">
                                        <asp:Label ID="lblInvAmount" runat="server" ></asp:Label> 
                                    </td>
                                    <td style="width:15%;">
                                       <b> Inv Currency :  </b>
                                    </td>
                                    <td style="width:35%;">
                                         <asp:Label ID="lblInvCurrency" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                         <tr>
                                   <td style="width:15%;">
                                      <b> Department :  </b>
                                    </td>
                                    <td style="width:35%;" colspan="3">
                                        <asp:DropDownList ID="ddldepartment" runat="server"  Width="174px" 
                                        onselectedindexchanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true" >
                                    </asp:DropDownList> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  InitialValue="0"
                                  ControlToValidate="ddldepartment" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPA"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                 <tr>
                                   <td style="width:15%;">
                                      <b> Account :  </b>
                                    </td>
                                    <td style="width:35%;" colspan="3">
                                        <asp:DropDownList ID="ddlAccount" runat="server" width="300px" ></asp:DropDownList> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  InitialValue="0"
                                  ControlToValidate="ddlAccount" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPA"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                      <b> Remarks :  </b>
                                    </td>
                                     <td style="width:35%;" colspan="3">
                                        <asp:TextBox ID="txtNonPoRemarks" runat="server"  Width="90%" TextMode="MultiLine" Height="120px" MaxLength="500"></asp:TextBox>
                                               &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                  ControlToValidate="txtNonPoRemarks" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPA"></asp:RequiredFieldValidator>
                                         <asp:HiddenField ID="hdnNonPoId" runat="server" Value="0" />
                                         <asp:HiddenField ID="hdnInvoiceId" runat="server" Value="0" />
                                    </td>
                                </tr>
                                  
                                  </table>    
                    <%------------------------------------------------------------------------------%>
                    

                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btnSaveApEntries" runat="server" Text="Save" Width="80px" ValidationGroup="RFPA" style="  border:none; padding:4px;" CssClass="btn" OnClick="btnSaveApEntries_Click" /> &nbsp;
                        <asp:Button ID="btnCloseApEntries" runat="server" Text="Close" CssClass="btn" Width="80px" style="  border:none; padding:4px;" ToolTip="Close this Window." OnClick="btnCloseApEntries_Click" />
                    </div>
                    <div style="text-align:center;padding:5px;background-color:#e5e5e5">
                        <asp:Label ID="lblMsgApEntries" runat="server" CssClass="error"></asp:Label>
                    </div>
                    </center>    
                    </div>
                    </center>
                        </div>
                                </ContentTemplate> 
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSaveApEntries"  />
                                        <asp:PostBackTrigger ControlID="btnCloseApEntries"  />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:HiddenField ID="hfInvId" runat="server" />
                                <asp:Button ID="btndownload" Text=""  OnClick="btnDownloadFile_Click" style="display:none;" runat="server" /> 
                                 
                            </td>
                        </tr>
                       <tr>
                           <td valign="top">
                               <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                           </td>
                       </tr>
                    </table>
                   
                </td>
            </tr>
         </table>
        </center>
        </div>
        
    </form>
</body>
</html>
