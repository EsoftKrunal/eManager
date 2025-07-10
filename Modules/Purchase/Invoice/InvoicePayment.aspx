<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoicePayment.aspx.cs" Inherits="Invoice_InvoicePayment" %>
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
    <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:12px;
        margin:0px;
    }
    
        .input_box
        {}
    
        .required_box
        {}
    
        .auto-style1 {
            height: 23px;
        }
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div>
    <center>
    <div style="border:solid 1px #008AE6; ">
    <asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>
        <table cellpadding="6" cellspacing="0" width="100%">
         <tr>
         <td class="text headerband">
             <strong>Invoice Register - Invoice Payment</strong></td>
         </tr>
           <tr>
               <td style=" background-color:#FFFFCC">
               &nbsp;
                 <asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000"></asp:Label>
               </td>
           </tr>
         <tr>
           <td>
              <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                    <tr>
                      <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                               Owner :</td>
                           <td style="text-align: left; ">
                               <asp:Label runat="server" ID="lblOwnerCode"></asp:Label>
                           </td>
                           <td style="text-align: right;">
                               PV No#</td>
                           <td style="text-align: left;width:500px;">
                               <asp:Label runat="server" ID="lblpvno"></asp:Label></td>
                      </tr>
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Vendor:</td>
                          <td style="text-align: left; ">
                              <asp:Label ID="lblVendorName" runat="server" CssClass="input_box" ></asp:Label>
                              <asp:HiddenField runat="server" id="hfdSupplier" />
                          </td>
                          <td style="text-align: right;">
                              Currency:</td>
                          <td style="text-align: left;width:500px;">
                              <asp:Label ID="lblCurrency" runat="server" CssClass="input_box"></asp:Label>
                          </td>
                      </tr>
                       <tr>
                           <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                               Payment Mode :</td>
                           <td style="text-align: left; ">
                               <asp:RadioButton ID="rad_SGD" runat="server" Text="SGD" GroupName="g1" />
                               <asp:RadioButton ID="rad_USD" runat="server" Text="USD"  GroupName="g1"/>
                               <asp:RadioButton ID="rad_INR" runat="server" Text="INR"  GroupName="g1"/>
                           </td>
                           <td style="text-align: right;">
                               &nbsp;</td>
                           <td style="text-align: left;width:500px;">
                               &nbsp;</td>
                      </tr>
                       <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Bank Name:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtBankName" runat="server" CssClass="input_box" Width="302px"></asp:TextBox>
                             </td>
                           <td style="text-align: right;">
                               Credit Act #:</td>
                           <td style="text-align: left;">
                               <asp:TextBox ID="txtCreditActNo" runat="server" CssClass="input_box" Width="159px"></asp:TextBox>
                           </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Credit Act Name:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtCreditActName" runat="server"  CssClass="required_box" 
                                  Width="302px" ></asp:TextBox>&nbsp;
                              </td>
                          <td style="text-align: right;">
                              Voucher #:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="input_box" 
                                  Width="126px"></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                  ControlToValidate="txtVoucherNo" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      
                     
                     <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px; ">
                              Cheque/ TT # :</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtChequeTTNo" runat="server" CssClass="required_box" 
                                  MaxLength="49" Width="126px"></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtChequeTTNo" ErrorMessage="*" ></asp:RequiredFieldValidator>  
                          </td>
                          <td style="text-align: right;">
                              Cheque/ TT Amt.:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtChequeTTAmt"  runat="server" CssClass="required_box" Width="126px" MaxLength="12" style="text-align:right"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtChequeTTAmt" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                              &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtChequeTTAmt"
                                  ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                      </tr>
                     
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px; ">
                              Cheque/ TT Date:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtChequeTTDate" runat="server" CssClass="input_box" Width="126px"  ></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtChequeTTDate" ErrorMessage="*" ></asp:RequiredFieldValidator>  
                          </td>
                          <td style="text-align: right;">
                              Bank Charges:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtBankCharges"  runat="server" CssClass="required_box" Width="126px" MaxLength="12" style="text-align:right"></asp:TextBox>
                              &nbsp;&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBankCharges"
                                  ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                      </tr>
                       <tr  style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Remarks:</td>
                          <td style="text-align: left;" colspan="3">
                              <asp:TextBox ID="txtComments" runat="server" CssClass="input_box" Height="50px" 
                                  TextMode="MultiLine" Width="80%"></asp:TextBox>
                              </td>
                           
                      </tr>
                   <tr>
                       <td colspan="4">
                           <asp:Button ID="btnAddInvoice" runat="server" CausesValidation="false" Text=" + Add Invoice" Width="100px" OnClick="btnAddInvoice_Click" style="  border:none; padding:4px;" Visible="false" CssClass="btn"/>
                       </td>
                       </tr>
                     </table>
                     
                     <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:25px;"/>
                                                <col style=""/>
                                                <col style="width:200px;" />
                                                <col style="width:200px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:50px;text-align:center;" />
                                                <col style="width:25px;"/>
                                                <tr class= "headerstylegrid" >
                                                    <td></td>
                                                    <td>Ref #</td>
                                                    <td>Inv #</td>
                                                    <td>Inv Dt.</td>
                                                    <td>Inv Amt</td>
                                                    <td>Approval Amt</td>
                                                    <td></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                    <div id="divPayment" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 150px; text-align:center;">
                                            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                                <col style="width:25px;"/>
                                                <col style=""/>
                                                <col style="width:200px;" />
                                                <col style="width:200px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:50px;text-align:center;" />
                                                <col style="width:25px;"/>
                                                </colgroup>
                                                <asp:Repeater ID="RptInvoices" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="left"><a href="ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>" target="_blank" > <%#Eval("RefNo")%></a>
                                                                <asp:HiddenField ID="hfdInvId" runat="server" Value='<%#Eval("InvoiceId")%>' />
                                                            </td>                                                                                                                       
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="right"><%#Eval("ApprovalAmount")%></td>
                                                             <td>
                                                                  <asp:ImageButton runat="server" CommandArgument='<%#Eval("InvoiceId")%>' Visible='<%#(lblpvno.Text.Trim()=="")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnDelRow" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnDeleteRow_Click" CausesValidation="false" />
                                                            </td> 
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                       <tr style="background-color:#E6F3FC">
                                                            <td></td>
                                                            <td align="left"><a href="ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>" target="_blank" > <%#Eval("RefNo")%></a>
                                                                <asp:HiddenField ID="hfdInvId" runat="server" Value='<%#Eval("InvoiceId")%>' />
                                                            </td>                                                                                                                       
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="right"><%#Eval("ApprovalAmount")%></td>
                                                            <td>
                                                                  <asp:ImageButton runat="server" CommandArgument='<%#Eval("InvoiceId")%>' Visible='<%#(lblpvno.Text.Trim()=="")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnDelRow" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnDeleteRow_Click" CausesValidation="false" />
                                                            </td> 
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                             </table>
                                        </div>
                                        <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:25px;"/>
                                                <col style=""/>
                                                <col style="width:200px;" />
                                                <col style="width:200px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:50px;text-align:center;" />
                                                <col style="width:25px;"/>
                                                <tr class= "headerstylegrid" >
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>Total:</td>
                                                    <td align="right"><asp:Label ID="lbltotal" runat="server"></asp:Label></td>
                                                    <td></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                <br />
              <table  cellpadding="0" cellspacing="0" width="100%">
                <tr>
                  <td style="">
                      <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                          PopupButtonID="imgduedate" PopupPosition="TopRight" TargetControlID="txtChequeTTDate">
                      </asp:CalendarExtender>
                      <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                          FilterType="Numbers,Custom" TargetControlID="txtChequeTTAmt" ValidChars=".">
                      </asp:FilteredTextBoxExtender>
                      <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                          FilterType="Numbers,Custom" TargetControlID="txtBankCharges" ValidChars=".">
                      </asp:FilteredTextBoxExtender>
                      
                       
                      <asp:Button ID="btn_Save" runat="server" Text="Save" Width="80px" OnClick="btn_Save_Click" style="  border:none; padding:4px;" CssClass="btn" />
                      <asp:Button ID="btn_Approve" runat="server" Text="Create PV" Width="80px" OnClick="btn_Approve_Click" style=" border:none; padding:4px;" CssClass="btn" Visible="false"/>
                      <asp:Button ID="btnClose" runat="server" Text="Close" Width="80px" OnClientClick="Refresh();window.close();" style="  border:none; padding:4px;" CssClass="btn"/>
                      <asp:Button ID="btn_Email" runat="server" Text="Email to Vendor" Width="150px" OnClick="btn_Email_Click" style="  border:none; padding:4px;" Visible="false" CssClass="btn" />
                  </td>
                </tr>
              </table>
           </td>
         </tr>
       </table>

           <!-----------------Update Invoice Stage------------------------>
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_OtherInvoices" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:800px;padding :5px; text-align :center;background : white; z-index:150;top:180px; border:solid 0px black;">
            <center >
             <div style="padding:6px;  font-size:14px; " class="text headerband"><b>Change Invoice Stage</b></div>
             <div style="width:100%; text-align:left; overflow-y:scroll; overflow-x:hidden; height:300px">
                <table cellpadding="3" cellspacing="0" width="100%" border="1" style="border-collapse:collapse">
                     <tr class= "headerstylegrid" >
                        <td class="auto-style1">Select</td>
                        <td class="auto-style1">Ref.No</td>
                        <td class="auto-style1">Inv.No</td>
                        <td class="auto-style1">Inv.Date.</td>
                        <td class="auto-style1">Invoice Amount</td>
                        <td class="auto-style1">Curency</td>
                        <td class="auto-style1">Due Date.</td>
                    </tr>
                <asp:Repeater ID="RptAddInvoices" runat="server">
                   
                <ItemTemplate>
                <tr>
                    <td><asp:RadioButton runat="server" GroupName="g1" CssClass='<%#Eval("InvoiceId")%>' AutoPostBack="true" id="rad_Inv" OnCheckedChanged="rad_Inv_OnCheckedChanged"/></td>
                    <td align="left"><%#Eval("RefNo")%></td>
                    <td align="left"><%#Eval("InvNo")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                    <td align="right"><%#Eval("ApprovalAmount")%></td>
                    <td align="left"><%#Eval("Currency")%></td>
                    <td align="left"><%#Common.ToDateString(Eval("DueDate"))%></td>
                </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                </table>
               
            </div>
                 <div>
                <center>
                <asp:Button ID="btnCloseAddPV" runat="server" Text="Close" Width="150px" CausesValidation="false" style="  border:none; padding:4px;" CssClass="btn" OnClick="btnCloseAddPV_Click"/>
                </center>
                </div>
            </center>
        </div>
    </center>
    </div>
       </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="btn_Save"  />
       

       </Triggers>
     </asp:UpdatePanel>


     

    </div>
    </center>
    </div>
    </form>
</body>
<script type="text/javascript">
    function Refresh() {
        window.opener.document.getElementById('btnSearch').click();
    }

</script>
</html>
