<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceEntry.aspx.cs" Inherits="CrewOperation_InvoiceEntry" MasterPageFile="~/Modules/HRD/InvoiceRegister.master" %>

<%@ Register Src="../UserControls/InvoiceStatus.ascx" TagName="InvoiceStatus" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<script language="javascript">
    function openpage()
    {
    var url = "../Registers/PopUp_Vendor.aspx"
    window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
    return false;
    }
</script>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table cellpadding="0" cellspacing="0" width="800px">
         <tr>
           <td class="textregisters" style="width: 775px">
             <strong>Invoice Register</strong></td>
         </tr>
           <tr>
               <td style="width: 775px;">
                 <asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000"></asp:Label>
                   &nbsp;
               </td>
           </tr>
         <tr>
           <td>
            <%--<strong>Invoice Entry<br />
            </strong>--%>
             <asp:Panel ID="pnl_InvoiceEntry" runat="server" Width="100%">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center;" class="">
                <legend><strong>Invoice Entry</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="6">
                          <asp:HiddenField ID="HiddenInvoiceEntry" runat="server" />
                      </td>
                    </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 123px; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 84px; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Ref. #.:</td>
                          <td style="text-align: left;">
                             <asp:TextBox ID="txt_RefNo" runat="server" CssClass="input_box" Width="126px" ReadOnly="True"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px; width: 84px;">Vendor:</td>
                          <td colspan="3" style="text-align: left">
                             <asp:DropDownList ID="ddl_Vendor" TabIndex="1" runat="server" CssClass="required_box" Width="373px"></asp:DropDownList>
                              <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                                  OnClientClick="return openpage();" /></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 123px;">
                          </td>
                          <td style="text-align: left;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 84px;">
                          </td>
                          <td style="text-align: left;">
                              <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_Vendor"
                                  ErrorMessage="Required." MaximumValue="1000000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                          <td style="text-align: left;">
                          </td>
                          <td style="text-align: left;">
                              </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Inv. #:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_InvNo" runat="server" TabIndex="2" CssClass="required_box" Width="126px" MaxLength="49"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px; width: 84px;">
                              Inv. Amt.:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_InvAmount" TabIndex="3" runat="server" CssClass="required_box" Width="120px" OnTextChanged="txt_InvAmount_TextChanged" MaxLength="12"></asp:TextBox></td>
                          <td style="padding-right: 5px; text-align: right;">
                              Currency:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddCurrency" TabIndex="4" runat="server" CssClass="input_box" Width="164px">
                          </asp:DropDownList></td>
                      </tr>
                      <tr>
                          <td style="text-align: right; width: 123px;">
                              &nbsp;</td>
                          <td align="left" style="padding-right: 5px;">
                              &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_InvNo"
                                  Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                          <td style="text-align: right; width: 84px;">
                              &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_InvAmount"
                                  Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                          <td style="text-align: left;">
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_InvAmount"
                                  ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic" Width="147px"></asp:RegularExpressionValidator></td>
                          <td style="text-align: left;">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Inv. Date:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_InvDate" runat="server" TabIndex="5" CssClass="required_box" Width="102px" AutoPostBack="True" OnTextChanged="txt_InvDate_TextChanged"></asp:TextBox>
                              <asp:ImageButton ID="imginvdate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                          <td align="right" style="text-align: right; padding-right:15px; width: 84px;">
                              Due Date:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_DueDate" runat="server" CssClass="input_box" Width="98px" TabIndex="6"></asp:TextBox>
                              <asp:ImageButton
                                  ID="imgduedate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                          <td style="padding-right: 5px; text-align: right;">
                              Vessel:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddl_Vessel" TabIndex="7" runat="server" CssClass="required_box" Width="164px">
                              </asp:DropDownList></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 123px;">
                          </td>
                          <td style="text-align: left;">&nbsp;
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_InvDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_InvDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                          </td>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 84px;">
                          </td>
                          <td style="text-align: left;">
                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_DueDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txt_DueDate" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                           </td>
                          <td style="text-align: left;">
                          </td>
                          <td style="text-align: left;">
                              <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_Vessel"
                                  ErrorMessage="Required." MaximumValue="1000000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Forward To Verify:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddl_ForwardTo" runat="server" TabIndex="8" CssClass="required_box" Width="130px">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right; padding-right:15px; width: 84px;">
                              Created By:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_CreatedBy" runat="server" CssClass="input_box" Width="120px" ReadOnly="True"></asp:TextBox></td>
                          <td align="right" style="padding-right: 5px; text-align: right;">
                              Created On:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txt_CreatedOn" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 123px;">
                          </td>
                          <td style="text-align: left;">
                              <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddl_ForwardTo"
                                  ErrorMessage="Required." MaximumValue="1000000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 84px;">
                          </td>
                          <td style="text-align: left;">
                          </td>
                          <td style="text-align: left;">
                          </td>
                          <td style="text-align: left;">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 123px;">
                              Email to Vendor:</td>
                          <td style="text-align: left">
                              <asp:CheckBox ID="ck_Email" runat="server" Width="90px" TabIndex="9" /></td>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 84px;">
                          </td>
                          <td style="text-align: left">
                          </td>
                          <td style="text-align: left">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 123px;">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 84px;">
                          </td>
                          <td style="text-align: left">
                          </td>
                          <td style="text-align: left">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                     </table>
                  </fieldset>
                              <asp:TextBox ID="txt_GST" runat="server" CssClass="required_box" Width="120px" AutoPostBack="True" OnTextChanged="txt_GST_TextChanged" Visible="False">0</asp:TextBox>
                              <asp:TextBox ID="txt_TotalInvAmount" runat="server" CssClass="input_box"
                                  Width="120px" ReadOnly="True" Visible="False">0</asp:TextBox><br />
                 <uc1:InvoiceStatus ID="InvoiceStatus1" runat="server" />
                </asp:Panel>
              <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                  <td align="right" style="padding-top: 15px">
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                          PopupButtonID="imginvdate" PopupPosition="TopRight" TargetControlID="txt_InvDate">
                      </ajaxToolkit:CalendarExtender>
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                          PopupButtonID="imgduedate" PopupPosition="TopRight" TargetControlID="txt_DueDate">
                      </ajaxToolkit:CalendarExtender>
                      &nbsp; <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                          FilterType="Numbers,Custom" TargetControlID="txt_InvAmount" ValidChars="-.">
                      </ajaxToolkit:FilteredTextBoxExtender>
                      &nbsp; &nbsp;
                      &nbsp; &nbsp;
                      &nbsp;&nbsp;&nbsp;
                      <asp:Button ID="btn_Cancel" TabIndex="10" runat="server" Text="Add" CssClass="btn" Width="59px" OnClick="btn_Cancel_Click" CausesValidation="False" Visible="False" />
                      <asp:Button ID="btn_Save" TabIndex="11" runat="server" Text="Save" CssClass="btn" Width="59px" OnClick="btn_Save_Click" />
                      <asp:Button ID="btn_Back" runat="server" CausesValidation="False" CssClass="btn"
                          OnClick="btn_Back_Approval_Click" TabIndex="12" Text="Back" Width="59px" /><br />
                      &nbsp;
                  </td>
                </tr>
              </table>
           </td>
         </tr>
       </table>
</asp:Content>
