<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddInvoiceEntry.aspx.cs" Inherits="Invoice_InvoiceEntry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link rel="Stylesheet"  type="text/css" href="../../HRD/Styles/StyleSheet.css"/>
     <script type="text/javascript" src="../JS/jquery.min.js"></script>
     <script src="../JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="../JS/AutoComplete/jquery-ui.css?11" />
     <script src="../JS/AutoComplete/jquery-ui.js?11" type="text/javascript"></script>
     <script type="text/javascript">
         function Clearid() {
             $("#hfdbidid").val("0");
         }
         function RegisterAutoComplete() 
         {
             $(function () {
                 //------------
                 function log(message) {
                     $("<div>").text(message).prependTo("#log");
                     $("#log").scrollTop(0);
                 }
                 var strType = "";
                 if ($('#chkAdvPayment').prop("checked")) {
                     strType = "APPONO";
                 }
                 else { strType = "PONO"; }
                 //------------
                 $("#txtPoNo").autocomplete(
                 {

                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                            /* url: getBaseURL() + "/Purchase/Invoice/getautocompletedata.ashx",*/
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtPoNo").val(), Type: strType },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.bidponum, value: item.bidponum, bidid: item.bidid} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         log(ui.item ?
                          "Selected: " + ui.item.label :
                          "Nothing selected, input was " + this.value);
                         //alert(ui.item.bidid);
                         $("#hfdbidid").val(ui.item.bidid);
                         
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
                 //---------------
                 $("#txtSupplier").autocomplete(
                 {
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                           /*  url: getBaseURL() + "/Purchase/Invoice/getautocompletedata.ashx",*/
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtSupplier").val(), Type: "VENALL" },
                             cache: false,
                             success: function (data) {
                                 //response($.map(data.geonames, function (item) { return { label: item.SupplierName, value: item.SupplierName, id: item.SupplierId } }
                                 response($.map(data.geonames, function (item) { return { label: item.SupplierNameCode, value: item.SupplierName, id: item.SupplierId, active:item.Active} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         log(ui.item ? "Selected: " + ui.item.label : "Nothing selected, input was " + this.value);
                         $("#hfdSupplier").val(ui.item.id);
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
             alert(baseUrl);
         }
         function Refresh() {
           //  window.opener.document.getElementById('btnSearch').click();
         }
     </script>
  <script type="text/javascript" src="JS/KPIScript.js"></script>
    <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:12px;
        margin:0px;
    }
    
        .input_box
        {}
    .error
    {
        background-color:Red;
        color:White;
    }
    </style>
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
             <strong>Invoice Register - New Invoice Entry</strong>
         </td>
         </tr>
            <tr>
                <td>
                    <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;" runat="server" id="tab_Upload" visible="false" >
                     <tr style="background-color:#E6F3FC">
                          <td style="text-align: center;">
                             Upload Attachment:
                              <asp:FileUpload ID="fuAttachment" runat="server" ValidationGroup="f12" />
                              <asp:RequiredFieldValidator runat="server" ID="fasfF" ValidationGroup="f12" ControlToValidate="fuAttachment" ErrorMessage="*"></asp:RequiredFieldValidator>
                              <asp:Button ID="Button1" runat="server" Text="Upload" ValidationGroup="f12" Width="100px" OnClick="btn_Upload_Click" style=" border:none; padding:0px;" CssClass="btn" />
                           </td>
                      </tr>
                      </table>
                </td>
            </tr>
         </table>
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style="width:50%; vertical-align:top; padding-top:10px;padding-left:5px;padding-right:5px;">

    <div style="border:solid 1px #008AE6; " >
    
    

    <asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>
        
         <table cellpadding="6" cellspacing="0" width="100%">
           <tr>
               <td style=" background-color:#FFFFCC">
                 <asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000"></asp:Label>
               </td>
           </tr>
         <tr>
           <td>
           
           <table border="1" cellpadding="0" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
           <tr>
           <td>
              <div style="padding:8px;  color:black;"> <b>Invoice Details </b></div>  
              <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px;">
                              Ref. #:</td>
                          <td style="text-align: left;">
                              <asp:Label ID="lblRefNo" runat="server" CssClass="input_box" ></asp:Label></td>
                      </tr>
                       <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px;">
                              Vendor:</td>
                          <td style="text-align: left;">
                             <asp:TextBox runat="server" ID="txtSupplier" Width="80%"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSupplier"
                                  Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                             <asp:HiddenField runat="server" id="hfdSupplier" />
                             </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px;">
                              Inv. #:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_InvNo" runat="server"  CssClass="required_box" Width="126px" MaxLength="49"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_InvNo"
                                  Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                      </tr>
                      
                     
                     <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px; ">
                              Inv. Date:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_InvDate" runat="server"  CssClass="required_box" Width="126px" AutoPostBack="True" OnTextChanged="txt_InvDate_TextChanged"></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_InvDate" ErrorMessage="*" ></asp:RequiredFieldValidator>  
                          </td>
                      </tr>
                     
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px; ">
                              Due Date:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_DueDate" runat="server" CssClass="input_box" Width="126px"  ></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txt_DueDate" ErrorMessage="*" ></asp:RequiredFieldValidator>  
                          </td>
                      </tr>
                       <tr  style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px;">
                              Inv. Amt.:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_InvAmount"  runat="server" CssClass="required_box" Width="126px" MaxLength="12" style="text-align:right"></asp:TextBox>&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_InvAmount" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                              &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_InvAmount"
                                  ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\d*\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                      </tr>
                       <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px;">
                              Currency:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddCurrency"  runat="server" CssClass="input_box" Width="164px"></asp:DropDownList>&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddCurrency" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                              
                          </td>
                      </tr>
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px; ">
                              Vessel:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddl_Vessel" AutoPostBack="true" OnSelectedIndexChanged="ddl_Vessel_SelectedIndexChanged"  runat="server" CssClass="required_box" Width="164px"></asp:DropDownList>
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator611" runat="server" ControlToValidate="ddl_Vessel" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                              
                          </td>
                      </tr>
                        <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px; ">
                              Advance Payment:</td>
                          <td style="text-align: left;">
                          <asp:CheckBox ID="chkAdvPayment" runat="server" AutoPostBack="True" OnCheckedChanged="chkAdvPayment_CheckedChanged"  />
                              
                          </td>
                      </tr>  
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 110px; ">
                              Non PO :</td>
                          <td style="text-align: left;">
                          <asp:CheckBox ID="chkNonPO" runat="server" AutoPostBack="True" OnCheckedChanged="chkNonPO_CheckedChanged"  />
                              
                          </td>
                      </tr>   
                                 
                            
                    <tr>
                       <td align="right" style="text-align: right; padding-right:15px; width: 110px; ">
                                  Attachment:</td>
                        <td style="text-align: left;">
                           <asp:ImageButton runat="server" ID="btnClip" ImageUrl="~/Modules/HRD/Images/paperclip.gif"  Visible="false" onclick="btnClip_Click"/>
                              <asp:LinkButton runat="server" ID="btnClipText" Visible="false" onclick="btnClipText_Click"/>
                        </td>
                    </tr>

                      <tr style="background-color:#E6F3FC">
                       <td align="right" style="text-align: right; padding-right:15px; width: 110px; ">
                                  Remarks:</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEntryComments" TextMode="MultiLine" runat="server" 
                                CssClass="input_box"  Width="80%" Height="50px"  ></asp:TextBox>
                        </td>
                    </tr>
                  <tr>
                      <td colspan="2">
                         
                      </td>
                  </tr>
                     </table>
           </td>
           <td style="width:300px; vertical-align:top;">
              <div style="padding:8px; color:black;"> <b>Purchase Orders </b></div>  
              <div style="padding:5px;background-color:#E6F3FC">
                <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                <td align="right" style="text-align: right; padding-right:15px; width:45px;">
                    PO #:
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtPoNo" runat="server" CssClass="input_box" Width="180px" ValidationGroup="re1" onkeydown="Clearid();" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="r1" runat="server" ErrorMessage="*" ControlToValidate="txtPoNo" ValidationGroup="re1" ></asp:RequiredFieldValidator>
                    <asp:HiddenField runat="server" ID="hfdbidid" />
                </td>
                <td style="padding:3px">
                <asp:ImageButton runat="server" ID="btnAddPO" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClick="btnAddPO_Click" ValidationGroup="re1" />
                </td>
                </tr>
                </table>
               </div>   
                 <table cellpadding="5" cellspacing="0" width="100%" border="0">
                <tr  class= "headerstylegrid">
                    <td style="text-align:left;">PO#</td>
                    <td style="width:120px">Alert</td>
                    <td style="width:20px"></td>
                </tr>
                <asp:Repeater runat="server" id="rptPo">
                    <ItemTemplate>
                    <tr>
                    <td style="text-align:left;"><a target="_blank" href='../VeiwRFQDetailsForApproval.aspx?BidId=<%#Eval("BIDID")%>'><%#Eval("PONO")%></a></td>
                    <td style="width:120px"><a title='PO# already used in invoice. Click here to open invoice.' target="_blank" href='ViewInvoice.aspx?InvoiceId=<%#Eval("OTHEROTHERINVID")%>' style='color:Red; float:right;' style='<%#((Eval("OTHERREFNO").ToString().Trim()=="")?"display:none":"")%>'>Used in ( <%#Eval("OTHERREFNO")%> )</a></td>
                    <td style="width:20px">
                    <asp:ImageButton runat="server" ID="btndelPo" CausesValidation="false" OnClick="btnPO_delete_Click" CommandArgument=<%#Eval("BIDID")%> ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return window.confirm('are you sure to delete this? you also need to press save to store in database.');" />
                    </td>
                    </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:#E6F3FC; text-align:left;">
                        <td style="text-align:left;" ><a target="_blank" href='../VeiwRFQDetailsForApproval.aspx?BidId=<%#Eval("BIDID")%>'><%#Eval("PONO")%></a></td>
                        <td style="width:120px"><a title='PO# already used in invoice. Click here to open invoice.'  target="_blank" href='ViewInvoice.aspx?InvoiceId=<%#Eval("OTHEROTHERINVID")%>' style='color:Red; float:right;' style='<%#((Eval("OTHERREFNO").ToString().Trim()=="")?"display:none":"")%>'>Used in ( <%#Eval("OTHERREFNO")%> )</a></td>
                        <td style="width:20px">
                        <asp:ImageButton runat="server" ID="btndelPo" CausesValidation="false" OnClick="btnPO_delete_Click" CommandArgument=<%#Eval("BIDID")%> ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return window.confirm('are you sure to delete this? you also need to press save to store in database.');" />
                        </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
                </table>
           </td>
           </tr>
           </table>
                     <br />

              <table id="tblSaveInv" runat="server" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                  <td style=" padding-bottom:10px;">
                      <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imginvdate" PopupPosition="TopRight" TargetControlID="txt_InvDate"></asp:CalendarExtender>
                      <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgduedate" PopupPosition="TopRight" TargetControlID="txt_DueDate"></asp:CalendarExtender>
                      <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" TargetControlID="txt_InvAmount" ValidChars="-."></asp:FilteredTextBoxExtender>
                      <asp:Button ID="btn_Save" runat="server" Text="Save" Width="80px" OnClick="btn_Save_Click" style=" border:none; padding:4px;" CssClass="btn" />
                      <asp:Button ID="btnClose" runat="server" Text="Close" Width="80px" OnClientClick="window.close()" style=" border:none; padding:4px;" CssClass="btn"  />
                  </td>
                </tr>
              </table>
              
               <table id="tblForwardTo" runat="server" visible="false" border="0" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse: collapse; width: 100%;">
                              <tr>
                             <td style="  color:black;" colspan="2">
                                 <strong>Forward to Purchaser for Processing</strong>
                             </td>
                             </tr>
                              <tr >
                                  <td align="right" style="text-align: right; padding-right:15px; width: 110px;">
                                      Forwarded To:
                                  </td>
                                  <td style="text-align: left;">
                                      <asp:DropDownList ID="ddlForwardTo" runat="server" CssClass="required_box" Width="164px">
                                      </asp:DropDownList>
                                      &nbsp;<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlForwardTo"
                                          ErrorMessage="*" MaximumValue="1000000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                  </td>
                              </tr>
                              <tr >
                                  <td colspan="2">
                                      <asp:Button ID="btnForwardTo" runat="server" Text="Save" Width="80px" OnClick="btnForwardTo_Click"
                                          Style=" border: none; padding: 4px;" CssClass="btn" />
                                      <asp:Button ID="btnFTClose" runat="server" Text="Close" Width="80px" OnClientClick="Refresh();window.close();"
                                          Style=" border: none; padding: 4px;" CssClass="btn"  />
                                  </td>
                              </tr>
                          </table>
           </td>
         </tr>
       </table>
       </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="btn_Save"  />
       </Triggers>
     </asp:UpdatePanel>
    </div>
    </td>
    <td style="width:50%; vertical-align:top; padding-top:10px;padding-left:5px;padding-right:5px;" runat="server" id="tr_Frm" visible="false">
     <iframe width="98%" height="700px" src="" runat="server" id="frmInvoice"></iframe>
    </td>
    </tr>
    </table>
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
