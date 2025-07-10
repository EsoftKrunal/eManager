<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceSubmitforApproval.aspx.cs" Inherits="Modules_Purchase_Invoice_InvoiceSubmitforApproval" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="JS/AutoComplete/jquery-ui.css" />
     <script src="JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
     <script type="text/javascript" src="JS/KPIScript.js"></script>
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
                 //------------
                 $("#txtPoNo").autocomplete(
                 {
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtPoNo").val(), Type: "PONO" },
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
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtSupplier").val(), Type: "VEN" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.SupplierName, value: item.SupplierName, id: item.SupplierId} }
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
         }
         function Refresh() {
             //window.opener.document.getElementById('btnSearch').click();
         }
    </script>

        <style type="text/css">
        body
        {
            font-family:Calibri;
            font-size:12px;
            margin:0px;
        }
    .error
    {
        color:Red;
    }
    .hide
    {
        display:none;
    }
            .style1
            {
                width: 66px;
            }
            .style2
            {
                width: 35px;
            }
            .style3
            {
                width: 258px;
            }
            .auto-style1 {
                height: 29px;
            }
            .auto-style2 {
                margin-left: 40px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
        <div style="font-family:Arial;font-size:12px;">
             <center>
                  <div  class="text headerband"> 
                    Submit Invoice for Approval

                </div>
             <asp:UpdatePanel runat="server">
       <ContentTemplate>
          <%-- <asp:HiddenField ID="hdnStatus" runat="server" />
           <asp:HiddenField ID="hdnInvAmount"  runat="server" />
           <asp:HiddenField ID="hdnCurrency"  runat="server" />
           <asp:HiddenField ID="hdnInvDate"  runat="server" />--%>
                <div style="border:solid 1px #008AE6; ">
                    <table width="100%">
                        <tr>
                            <td style="width:35%;vertical-align:top;">
<table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                          <tr>
                             <td colspan="4"  class="text headerband">
                                 <strong>Invoice Details</strong>
                             </td>
                          </tr>
                          <tr>
                              <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                                  Ref. #:</td>
                              <td style="text-align: left;">
                                  <asp:Label ID="lblRefNo" runat="server" CssClass="input_box" ></asp:Label></td>
                               <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                                  Vendor:</td>
                              <td style="text-align: left;">
                                 <asp:Label runat="server" ID="lblSupplier" ></asp:Label>
                                 [ <asp:Label runat="server" ID="lblVendorCode" ></asp:Label> ]
                              </td>
                          </tr>
                          <tr  style="background-color:#E6F3FC">
                              <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                                  Inv. #:</td>
                              <td style="text-align: left;">
                                  <asp:Label ID="lbl_InvNo" runat="server"></asp:Label> </td>
                             <td align="right" style="text-align: right; padding-right:15px; ">
                                  Inv. Date:</td>
                              <td style="text-align: left;">
                                  <asp:Label ID="lbl_InvDate" runat="server"></asp:Label>
                              </td>
                          </tr>
                          <tr>
                              <td align="right" style="text-align: right; padding-right:15px; ">
                                  Due Date:</td>
                              <td style="text-align: left;">
                                  <asp:Label ID="lbl_DueDate" runat="server"></asp:Label>  
                              </td>
                              <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                                  Vessel:</td>
                              <td style="text-align: left;">
                                  <asp:Label ID="lbl_Vessel" runat="server"></asp:Label>
                          </tr>
                          <tr style="background-color:#E6F3FC">
                               <td align="right" style="text-align: right; padding-right:15px; ">
                               Currency:
                                  </td>
                              <td style="text-align: left;">
                                  <asp:Label ID="lblCurrency"  runat="server" ></asp:Label>
                              </td>
                              <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                              Inv. Amt.:
                                  </td>
                              <td style="text-align: left;">
                              
                                  <asp:Label ID="lbl_InvAmount"  runat="server"></asp:Label> </td>
                                 
                          </tr>
                          
                          
                          
                          
                          </table>
                            </td>
                            <td style="width:65%;">
                                <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: left; border-collapse:collapse; width:100%;">
                <tr style="font-weight:bold" class= "headerstylegrid">
                    <td style="width:50px">Status</td>
                    <td style="width:120px">Stage</td>
                    <td style="width:200px">By/On</td>
                    <td>Remarks</td>                
                    <td style="width:80px;">Update User</td>
                </tr>
                <tr>
                    <td><asp:Image ID="imgEntry" runat="server" Visible="false" /></td>
                    <td>Entry</td>
                    <td><asp:Label ID="lblEnteredBy" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblEnteredOn" runat="server"></asp:Label></td>
                    <td><asp:Label ID="lblEntryComments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Image ID="imgProcessing" runat="server" Visible="false" /></td>
                    <td class="auto-style1">Processing</td>
                    <td class="auto-style1"><asp:Label ID="lblProcessingTo" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblProcessingOn" runat="server"></asp:Label></td>
                    <td class="auto-style1"><asp:Label ID="lblProcessingComments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
                    <td class="auto-style1"><asp:ImageButton runat="server" ID="btnPropose" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="2" CausesValidation="false" Visible="false"/></td>
                </tr>
                <tr>
                    <td><asp:Image ID="imgApproval1" runat="server" Visible="false" /></td>
                    <td>Approval 1</td>
                    <td><asp:Label ID="lblApproval1By" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblApproval1On" runat="server"></asp:Label></td>
                    <td><asp:Label ID="lblApproval1Comments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
                    <td><asp:ImageButton runat="server" ID="btnApp1" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="3" CausesValidation="false" Visible="false"/></td>
                </tr>
                 <tr>
                    <td><asp:Image ID="imgApproval2" runat="server" Visible="false" /></td>
                    <td>Approval 2</td>
                    <td><asp:Label ID="lblApproval2By" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblApproval2On" runat="server"></asp:Label></td>
                    <td><asp:Label ID="lblApproval2Comments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
                    <td><asp:ImageButton runat="server" ID="btnApp2" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="4" CausesValidation="false" Visible="false"/></td>
                </tr>
                <%-- <tr>
                    <td><asp:Image ID="imgApproval3" runat="server" Visible="false" /></td>
                    <td>Approval 3</td>
                    <td><asp:Label ID="lblApproval3By" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblApproval3On" runat="server"></asp:Label></td>
                    <td><asp:Label ID="lblApproval3Comments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
                    <td><asp:ImageButton runat="server" ID="btnApp3" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="5" CausesValidation="false" Visible="false"/></td>
                </tr>
                 <tr>
                    <td><asp:Image ID="imgApproval4" runat="server" Visible="false" /></td>
                    <td>Approval 4</td>
                    <td><asp:Label ID="lblApproval4By" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblApproval4On" runat="server"></asp:Label></td>
                    <td><asp:Label ID="lblApproval4Comments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
                    <td><asp:ImageButton runat="server" ID="btnApp4" ImageUrl="~/Modules/HRD/Images/addpencil.gif"  OnClick="btnUpdateUserClick" CommandArgument="6" CausesValidation="false" Visible="false"/></td>
                </tr>--%>
                <tr>
                    <td><asp:Image ID="imgPayment" runat="server" Visible="false" /></td>
                    <td>Payment</td>
                    <td><asp:Label ID="lblPaymentBy" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblPaymentOn" runat="server"></asp:Label></td>
                    <td><asp:Label ID="lblPaymentComments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
                    <td><asp:ImageButton runat="server" ID="btnPayment" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="7" CausesValidation="false"/></td>
                </tr>
                </table>
                            </td>
                        </tr>
                    </table>
                
                    </div>
                   
        <asp:Panel runat="server" ID="pnl_Verification" Visible="false" >
                <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                       <tr>
                         <td colspan="2" class="text headerband">
                             <strong>Invoice Approval &amp; Foward to Payment</strong></td>
                         </tr>
                       <tr style="background-color:#E6F3FC">
                              <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                                  Remarks:</td>
                              <td style="text-align: left;">
                                  <asp:TextBox ID="txtVerifyComments" runat="server"  Width="80%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                              </td>
                       </tr>
                     <%--  <tr >
                          <td align="right" style="text-align: right; padding-right:15px; ">Forwarded To :</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlPaymentForwardTo"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="verify" ></asp:DropDownList>
                              &nbsp;
                              <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddlPaymentForwardTo" ErrorMessage="*" MaximumValue="1000000" MinimumValue="1" Type="Integer" ValidationGroup="verify" ></asp:RangeValidator>  
                          </td>
                      </tr>--%>
                      
                      </table>
                      <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style="">
                              &nbsp; 
                              <asp:Button ID="btn_VerifySave" runat="server" Text="Save" Width="80px" OnClick="btn_VerifySave_Click" ValidationGroup="verify" style="  border:none; padding:4px;" CssClass="btn" />
                              &nbsp;<asp:Button ID="btn_VerifyCancel" runat="server" Text="Close" Width="80px" CausesValidation="false" OnClientClick="Refresh();window.close();" style="  border:none; padding:4px;" CssClass="btn"/>
                          </td>
                        </tr>
                     </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlApproval" Visible="false" >
               <div style="padding:6px;" class="text headerband">
                <strong>Invoice Approval - <asp:Label ID="lblApprovalLevel" runat="server"></asp:Label></strong>
                             <asp:HiddenField runat="server" ID="hfdAppUserId" />
                            <%-- <asp:HiddenField runat="server" ID="hfdPaymentUserId" />--%>
                             <asp:HiddenField runat="server" ID="hfdAppLevel" />
               </div>
               <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                       <tr style="background-color:#E6F3FC">
                              <td align="right" style="text-align: left; padding-right:15px; width: 140px;">
                                  Remarks:</td>
                              <td style="text-align: left;">
                                  <asp:TextBox ID="txtApprovalRemarks" runat="server"  Width="80%" TextMode="MultiLine" Height="50px" ValidationGroup="apprvng"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rerw" runat="server" ErrorMessage="*" ControlToValidate="txtApprovalRemarks" ValidationGroup="apprvng"></asp:RequiredFieldValidator>
                              </td>
                       </tr>
                       <%--<tr>
                        <td align="right" style="text-align: left; padding-right:15px; width: 140px;">
                                  Forward to Payment :</td>
                              <td style="text-align: left;">
                                  <asp:Label runat="server" ID="lblForwardedToPaymentNew"></asp:Label>
                              </td>
                       
                       </tr>--%>
                      </table>
               <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style="padding:4px;">
                              &nbsp; 
                              <asp:Button ID="btnApprovalSave" runat="server" Text="Save" Width="80px" OnClick="btnApprovalSave_Click" ValidationGroup="apprvng" style="  border:none; padding:4px;" CssClass="btn" />
                              &nbsp;<asp:Button ID="btnApprovalClose" runat="server" Text="Close" Width="80px" CausesValidation="false" OnClientClick="Refresh();window.close();" style="  border:none; padding:4px;" CssClass="btn"/>
                          </td>
                        </tr>
              </table>
        </asp:Panel>
        
        <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td style=" background-color:#FFFFCC;padding:10px; border-bottom:solid 1px #dddddd">
            <asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000"></asp:Label>
        </td>
        </tr>
       </table>

       <div style="position:absolute;top:0px;left:0px; height :470px; width:50%; " id="dvNotes" runat="server" visible="false">
        <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position:relative;width:600px; padding :5px; text-align :right;background : white; z-index:150;top:180px; border:solid 0px black;">
            <div style=" padding:5px; text-align:center;" class="text headerband">
        <strong>NOTES</strong>
        </div>
                <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                <tr style="background-color:#E6F3FC">
                <td style="text-align: left;">
                    <asp:TextBox ID="txtStageComments" runat="server"  Width="100%" TextMode="MultiLine" Height="50px" ValidationGroup="fsfsfs"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStageComments" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            </table>
                <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                          <td style="padding:4px;">
                                      <asp:Button ID="btnSaveStageComments" runat="server" Text="Save" Width="80px" OnClick="btnSaveStageComments_Click" ValidationGroup="fsfsfs" style="  border:none; padding:4px;" CssClass="btn" />
                                      &nbsp;<asp:Button ID="btnCloseStageComments" runat="server" Text="Close" Width="80px" CausesValidation="false" OnClick="btnCloseStageComments_Click" style="  border:none; padding:4px;" CssClass="btn"/>
                          </td>
                        </tr>
                    </table>
            </div>
        </center>
        </div>

        <!-- UPDATE USER -->
        <div style="position:absolute;top:0px;left:0px; height :470px; width:50%; " id="dvUpdateUser" runat="server" visible="false">
      <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:400px;padding :5px; text-align :center;background : white; z-index:150; top:100px; border:solid 0px black;">
            <center >
            <div style="padding:8px; " class="text headerband"> Select New User From List </div>  
              <div style="padding:5px;background-color:#E6F3FC">
                <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                <td>
                   <asp:DropDownList runat="server" id="ddlNewUser" Width="80%" ValidationGroup="rtyu"></asp:DropDownList>
                   <asp:RequiredFieldValidator runat="server" ID="fsdfadfa" ControlToValidate="ddlNewUser" ErrorMessage="*" ValidationGroup="rtyu"></asp:RequiredFieldValidator>
                </td>
                </tr>
                </table>
                   <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align:center; padding:5px;">
                            <asp:Button ID="btnUpdateUserSave" runat="server" Text="Save" Width="80px" OnClick="btnUpdateUserSave_Click" style="  border:none; padding:4px;" ValidationGroup="rtyu" CssClass="btn"/>
                            <asp:Button ID="btnUpdateUserCancel" runat="server" Text="Close" Width="80px" OnClick="btnUpdateUserCancel_Click" CausesValidation="false" style="  border:none; padding:4px;" CssClass="btn"/>
                        </td>
                    </tr>
                    </table>
                </div>
                </center>
         
        </div>
               </center>
               </div>
                 <div style="position:absolute;top:0px;left:0px; height :470px; width:50%; " id="dvDocument" runat="server" visible="false">
        <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position:relative;width:600px; padding :5px; text-align :right;background : white; z-index:150;top:180px; border:solid 0px black;">
                <caption>
                    <iframe id="frmUpload1" runat="server" height="200" width="100%"></iframe>
                    <asp:Button ID="Button1" runat="server" CausesValidation="false" OnClick="btn_DocumentClose_Click" style="margin-top:5px; background-color:red; color:White; border:none; padding:4px;" Text="Close" Width="80px" />
                </caption>
            </div>
        </center>
        </div>
         
              </ContentTemplate>
           <Triggers>
               <asp:PostBackTrigger ControlID="Button1" />
           </Triggers>
       </asp:UpdatePanel>
                   <div style=" padding:5px;" class="text headerband">
                    <asp:LinkButton ID="btnaddnotes" OnClick="btn_AddNotes_Click" CausesValidation="false" Text="Add Notes " style="color:white; font-style:italic;float:left;padding-left:10px;" runat="server" /> 
              
                   <strong>NOTES</strong>
               
        </div>
        <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                         <tr style=" font-weight:bold;" class= "headerstylegrid">
                         <td style="width:100px">Enterd By</td>
                         <td style="width:80px">Entered On</td>
                         <td style="width:100px">Stage</td>
                         <td style="text-align:left">Notes</td>
                         </tr>
                         <asp:Repeater runat="server" ID="rptComments">
                         <ItemTemplate>
                         <tr>
                         <td><%#Eval("UserName")%></td>
                         <td><%#Common.ToDateString(Eval("CommentsOn"))%></td>
                         <td><%#Eval("Stage")%></td>
                         <td style="text-align:left"><%#Eval("Comments")%></td>
                         </tr>
                         </ItemTemplate>
                         </asp:Repeater>
                         
                         </table>
        <div style="  padding:5px;" class="text headerband">
        <asp:LinkButton ID="lnkAddDocuments" OnClick="btn_AddDocuments_Click" CausesValidation="false" Text="Add Documents" style="color:white; font-style:italic;float:left;padding-left:10px;" runat="server" /> <strong>Other Documents</strong>
        </div>
         <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
            <tr class= "headerstylegrid">
            <td>Attachment Name</td>
            <td style="width:80px">Uploaded On</td>
            <td style="width:100px">Download</td>
            <td style="width:40px">Delete</td>
            </tr>
            <asp:Repeater runat="server" ID="Repeater1">
            <ItemTemplate>
            <tr>
            <td style="text-align:left"><%#Eval("AttachmentName")%></td>
            <td><%#Common.ToDateString(Eval("AttachmentOn"))%></td>
            <td>
                <asp:ImageButton runat="server" ID="btnDownload" ImageUrl="~/Modules/HRD/Images/paperclipx12.png" OnClick="btnDownload_Click" CommandArgument='<%#Eval("PK")%>' />
            </td>
            <td>
                <asp:ImageButton runat="server" ID="btnDelete" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClick="btnDelete_Click" Visible='<%#(UserId.ToString()=="1")%>' CommandArgument='<%#Eval("PK")%>' OnClientClick="return confirm('Are you sure to delete ?');" />
            </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
                         
            </table>
                 </center>
        </div>
    </form>
</body>
    <script type="text/javascript">
        function Page_CallAfterRefresh() {
            RegisterAutoComplete();
        }
        function SetImageHeight() {
            var w = $("#frmInvoice").width();
            var h = parseFloat(w) * 1.41;
            if (h > 700)
                h = 700;
            $("#frmInvoice").height(h);
        }
        $(document).ready(function () {
            RegisterAutoComplete();
            SetImageHeight();
        });
        function Refresh() {
            //window.opener.document.getElementById('btnSearch').click();
        }


    </script>
</html>
