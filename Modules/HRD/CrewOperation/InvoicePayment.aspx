<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoicePayment.aspx.cs" Inherits="CrewAccounting_InvoicePayment" MasterPageFile="~/Modules/HRD/InvoiceRegister.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="center">
      <table cellpadding="0" cellspacing="0" width="800px">
        <tr>
          <td class="textregisters">
            <strong>Payment</strong>
          </td>
        </tr>
        <tr>
          <td>
            <asp:Panel ID="pnl_InvoicePayment" runat="server" Width="100%">
             <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center;" class="">
                <legend><strong>Invoice Payment</strong></legend>
                <table cellpadding="0" cellspacing="0" width="100%" style="text-align:left">
                  <tr>
                    <td style="padding-left: 15px">
                        <br />
                        Vendor Name:&nbsp; &nbsp;<asp:Label ID="lbl_VendorName" runat="server" Width="666px" TabIndex="1"></asp:Label>&nbsp;</td>
                  </tr>
                    <tr>
                        <td align="center" style="padding-left: 15px">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" TabIndex="1" Width="258px"></asp:Label></td>
                    </tr>
                  <tr>
                    <td style="padding-left: 15px">
                     <div style="overflow-y: scroll; overflow-x:hidden; width:100%; height: 100px">
                      <asp:GridView ID="gvinvoicepayment" runat="server" AutoGenerateColumns="False" DataKeyNames="InvoiceId" GridLines="Horizontal" Style="text-align: center" Width="98%" >
                         <Columns>
                            <asp:BoundField DataField="RefNo" HeaderText="Ref. #">
                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="InvNo" HeaderText="Inv #">
                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Invdate" HeaderText="Inv. Dt.">
                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InvoiceAmount" HeaderText="Inv. Amt." DataFormatString="{0:0.00}" HtmlEncode="false">
                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ApprovedAmount" HeaderText="Approved Amt." DataFormatString="{0:0.00}" HtmlEncode="false">
                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalInvoiceAmount" HeaderText="Total" DataFormatString="{0:0.00}" HtmlEncode="false">
                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                            </asp:BoundField>
                        </Columns>
                            <RowStyle CssClass="rowstyle" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                       
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="text-align: right; padding-right: 15px; width: 100px;">
                                    <strong>
                                    Total:</strong></td>
                                <td style="text-align: left; width: 139px; ">
                                    <asp:Label ID="lbl_Amt" runat="server" Width="106px"></asp:Label></td>
                                <td style="text-align: right; padding-right: 15px; width: 105px; ">
                                    <strong>
                                    </strong></td>
                                <td style="text-align: left; ">
                                    </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 100px;">
                                </td>
                                <td style="text-align: left; width: 139px;">
                                    &nbsp;
                                </td>
                                <td style="text-align: right; width: 105px;">
                                </td>
                                <td style="text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; padding-right: 15px; width: 100px;">
                                    Bank Name:</td>
                                <td style="text-align: left; width: 139px;">
                                    <asp:TextBox ID="txt_BankName" runat="server" CssClass="input_box" Width="266px" MaxLength="50" TabIndex="1"></asp:TextBox></td>
                                <td style="text-align: right; width: 105px; padding-right: 15px;">
                                    Credit Act #.:</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtaccountno" runat="server" CssClass="input_box" Width="250px" MaxLength="30" TabIndex="2"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 100px;">
                                </td>
                                <td style="text-align: left; width: 139px;">
                                    &nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_BankName"
                                        ErrorMessage="Required" Visible="False"></asp:RequiredFieldValidator></td>
                                <td style="text-align: right; width: 105px;">
                                </td>
                                <td style="text-align: left">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtaccountno"
                                        ErrorMessage="Required" Visible="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; padding-right: 15px; width: 100px;">
                                    Credit Act Name:</td>
                                <td style="text-align: left; width: 139px;">
                                    <asp:TextBox ID="txt_CreditAccNameNo" runat="server" CssClass="input_box" Width="150px" MaxLength="50" TabIndex="3"></asp:TextBox></td>
                                <td style="text-align: right; padding-right: 15px; width: 105px;">
                                    Payment Type:</td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddl_Currency" runat="server" CssClass="required_box" TabIndex="4">
                                        <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                        <asp:ListItem Value="1">USD</asp:ListItem>
                                        <asp:ListItem Value="20">SGD</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 100px; ">
                                </td>
                                <td style="text-align: left; width: 139px; ">
                                    &nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_CreditAccNameNo"
                                        ErrorMessage="Required" Visible="False"></asp:RequiredFieldValidator></td>
                                <td style="text-align: right; width: 105px; ">
                                </td>
                                <td style="text-align: left; ">
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddl_Currency"
                                        ErrorMessage="Required" Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; padding-right: 15px; width: 100px;">
                                    Cheque/TT #:</td>
                                <td style="text-align: left; width: 139px;">
                                    <asp:TextBox ID="txt_ChequeNo" runat="server" CssClass="input_box" Width="250px" MaxLength="15" TabIndex="5"></asp:TextBox></td>
                                <td style="text-align: right; padding-right: 15px; width: 105px;">
                                    Cheque/TT Amt.:</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txt_ChequeTTAmt" runat="server" CssClass="input_box" Width="120px" TabIndex="6"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 100px; ">
                                </td>
                                <td style="text-align: left; width: 139px; ">
                                    &nbsp;&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_ChequeNo"
                                        ErrorMessage="Required" Visible="False"></asp:RequiredFieldValidator></td>
                                <td style="text-align: right; width: 105px; ">
                                </td>
                                <td style="text-align: left; ">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_ChequeTTAmt"
                                        ErrorMessage="Required" Visible="False"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_ChequeTTAmt"
                                        Display="Dynamic" ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}"
                                        Width="147px"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; padding-right: 15px; width: 100px; ">
                                    Cheque/TT Dt.:</td>
                                <td style="text-align: left; width: 139px; ">
                                    <asp:TextBox ID="txt_ChequeTTDate" runat="server" CssClass="input_box" Width="120px" TabIndex="7"></asp:TextBox><asp:ImageButton ID="Imageexpiry" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                <td style="text-align: right; width: 105px; padding-right: 15px;">
                                    Bank Charges:</td>
                                <td style="text-align: left; ">
                                    <asp:TextBox ID="txt_BankCharges" runat="server" CssClass="input_box" TabIndex="8"
                                        Width="120px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 100px;">
                                </td>
                                <td style="text-align: left; width: 139px;">
                                    &nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_ChequeTTDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txt_ChequeTTDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                    
                                        </td>
                                <td style="text-align: right; width: 105px;">
                                </td>
                                <td style="text-align: left">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_ChequeTTAmt"
                                        ErrorMessage="Required" Visible="False"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_ChequeTTAmt"
                                            Display="Dynamic" ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}"
                                            Width="147px"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 100px; text-align: right; height: 20px;">
                                    MTM Voucher No.:</td>
                                <td style="width: 139px; text-align: left; height: 20px;">
                                    <asp:TextBox ID="txtMtmVNo" runat="server" CssClass="input_box" MaxLength="50" TabIndex="3"
                                        Width="150px"></asp:TextBox></td>
                                <td style="width: 105px; text-align: right; height: 20px;">
                                    <asp:CheckBox ID="chk_Email" runat="server" TabIndex="9" /></td>
                                <td style="text-align: left; height: 20px;">
                                    Email to Vendor</td>
                            </tr>
                            <tr>
                                <td style="width: 100px; text-align: right">
                                </td>
                                <td style="width: 139px; text-align: left">
                                </td>
                                <td style="width: 105px; text-align: right">
                                </td>
                                <td style="text-align: left">
                                </td>
                            </tr>
                        </table>
                   </td>
                 </tr>
               </table>             
             </fieldset>
                <br />
            </asp:Panel>
              <table cellpadding="0" cellspacing="0" style="width: 100%">
                  <tr>
                      <td style="text-align: right; height: 130px;">
                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                              FilterMode="validChars" FilterType="Custom" TargetControlID="txt_ChequeTTAmt"
                              ValidChars="0123456789.">
                          </ajaxToolkit:FilteredTextBoxExtender><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                              FilterMode="validChars" FilterType="Custom" TargetControlID="txt_BankCharges"
                              ValidChars="0123456789.">
                          </ajaxToolkit:FilteredTextBoxExtender>
                          <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy"
                              PopupButtonID="Imageexpiry" TargetControlID="txt_ChequeTTDate" PopupPosition="Right">
                          </ajaxToolkit:CalendarExtender>
                          <asp:Button ID="btn_Pay" runat="server" Text="Pay" CssClass="btn" Width="59px" TabIndex="10" OnClick="btn_Pay_Click" />
                          <asp:Button ID="btn_Print" runat="server" Text="Print" CssClass="btn" Width="59px" TabIndex="10" CausesValidation="false" OnClick="btn_Print_Click" Enabled="False"/>
                          <asp:Button ID="btn_Cancel" runat="server" Text="Back" CssClass="btn" Width="59px" TabIndex="11" OnClick="btn_Cancel_Click" CausesValidation="False" /><br />
                          &nbsp;</td>
                  </tr>
              </table>
          </td>
        </tr>
      </table>
    </div>
    </asp:Content>
   