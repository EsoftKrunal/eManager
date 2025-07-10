<%@ Register Src="../UserControls/InvoiceStatus.ascx" TagName="InvoiceStatus" TagPrefix="uc1" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllInvoiceList.aspx.cs" Inherits="AllInvoiceList" MasterPageFile="~/Modules/HRD/InvoiceRegister.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<table style="width:100%; background-color: #f9f9f9; border-collapse:collapse " border="0" bordercolor="#4371a5" >
       <tr>
       <td style=" background-color: #f9f9f9; height:178px; vertical-align:top; text-align:center;" >
       <table cellpadding="0" cellspacing="0" width="100%">
       <tr><td style="padding-left:10px;padding-right:10px;" >
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;text-align: center">
            <legend><strong>Invoice Search</strong></legend>
              <table width="100%" cellpadding="0" cellspacing="0">
              <tr><td colspan="8">&nbsp;</td></tr>
                <tr>
                 <td align="right" class="style1">
                     Ref. #:</td>
                    <td align="left">
                        <asp:TextBox ID="txtrefno" runat="server" CssClass="input_box"
                            TabIndex="1" Width="129px"></asp:TextBox></td>
                    <td align="right">
                        Inv. #.:</td>
                    <td align="left">
                        <asp:TextBox ID="txtInvno" runat="server" CssClass="input_box" TabIndex="2"
                            Width="157px"></asp:TextBox></td>
                    <td align="right" style="width: 55px">
                        Vendor:</td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddvendor" runat="server" CssClass="input_box" TabIndex="3"
                            Width="336px">
                        </asp:DropDownList></td>
                </tr>
                  <tr>
                      <td align="right" class="style1">
                      </td>
                      <td align="left">
                          &nbsp;</td>
                      <td align="right">
                      </td>
                      <td align="left">
                      </td>
                      <td align="right" style="width: 55px">
                      </td>
                      <td align="left">
                      </td>
                      <td align="right">
                      </td>
                      <td align="left">
                      </td>
                  </tr>
                  <tr>
                      <td align="right" class="style1">
                          Status:</td>
                      <td align="left">
        <asp:DropDownList ID="ddstatus" runat="server" CssClass="input_box" TabIndex="5"
                            Width="133px">
                      </asp:DropDownList></td>
                      <td align="right">
                          Vessel:</td>
                      <td align="left">
                     <asp:DropDownList ID="ddVessel" runat="server" CssClass="input_box" TabIndex="4"
                         Width="162px">
                     </asp:DropDownList></td>
                      <td align="right" style="width: 55px">
                      </td>
                      <td align="left">
                      </td>
                      <td align="right">
                      </td>
                      <td align="left">
                      </td>
                  </tr>
                  <tr>
                      <td align="right" class="style1">
                          </td>
                      <td align="left" colspan="3">
        </td>
                      <td align="right" style="width: 55px">
                      </td>
                      <td align="left">
                      </td>
                      <td align="right">
                      </td>
                      <td align="left">
                      </td>
                  </tr>
              </table>
        </fieldset>
    </td></tr>
    <tr><td style="padding-top: 15px;padding-left:10px;padding-right:10px;" align="right">
    <asp:Label runat ="server" id="lblYear" Text="Year : "></asp:Label> 
        <asp:DropDownList runat="server" CssClass="input_box" ID="ddlYear" Width="60px" ></asp:DropDownList>  
        <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="btn_InvArc_Click" TabIndex="7" Text="Archive Invoices" Width="125px" OnClientClick="javascript:return confirm('Are you sure to move selected year invoice(s) to Archive.')"  />
        <asp:Button ID="btn_search" runat="server" CssClass="btn" OnClick="btn_search_Click" TabIndex="6" Text="Search" Width="59px" />
        <asp:Button ID="btn_clear" runat="server" CssClass="btn" OnClick="btn_clear_Click" TabIndex="7" Text="Clear" Width="59px" />
        </td></tr>  
         <tr>
             <td align="center">
                 <asp:Label ID="Label2" runat="server" ForeColor="Red">Can't edit record because Record is already Approved.</asp:Label></td>
         </tr>
            <tr><td style="text-align:left;padding-left:10px;padding-right:10px;">
            <asp:Panel ID="panel_invoice" runat="server" Visible="true" Width="100%">
    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid; padding-left: 5px; padding-right: 5px; text-align: center;">
        <legend><strong>Invoice List</strong></legend>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <div style="overflow-y: scroll; overflow-x: scroll; width:100%; height: 150px">
            <asp:GridView ID="gvinvoice" runat="server"  AllowSorting="True" OnSorting="on_Sorting" OnPreRender="gvinvoice_PreRender" OnRowDataBound="gvinvoice_DataBound" OnSelectedIndexChanged="gvinvoice_selectedIndex" OnRowEditing="gvinvoice_Row_Editing" DataKeyNames="InvoiceId" AutoGenerateColumns="False" GridLines="Horizontal" Width="98%">
                 <Columns>
                   <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                   <%--<asp:CommandField ButtonType="Image" Visible="true" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ><ItemStyle Width="40px" /></asp:CommandField>--%>
                   <asp:TemplateField HeaderText="Edit">
                   <ItemTemplate>
                  <asp:ImageButton runat ="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" ID="btnEdit" Visible='<%# (Eval("TableType").ToString()=="I") %>'   OnClick="EditInvoiceClick" CommandArgument='<%#Eval("InvoiceId") %>' ToolTip='<%#Eval("Sno") %>' />  
                  </ItemTemplate>
                  </asp:TemplateField> 
                  <asp:TemplateField HeaderText="Cancel">
                  <ItemTemplate>
                  <asp:ImageButton runat ="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" ID="btnCancel" Visible='<%# (Eval("TableType").ToString()=="I") %>' OnClick="CanceInvoiceClick" CommandArgument='<%#Eval("InvoiceId") %>' OnClientClick="javascript:return confirm('Are you sure to cancel this Invoice.')"  />  
                  </ItemTemplate>
                  </asp:TemplateField>  
                  <asp:TemplateField HeaderText="Attacments">
                  <ItemTemplate>
                  <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" />
                  <asp:HiddenField ID="Hiddenfd11" runat ="server" Value='<%#Eval("Attachment") %>' />
                   <asp:HiddenField ID="HiddenInvoicestatusId1" runat="server" Value='<%#Eval("Status_Id")%>' />
                  <asp:HiddenField ID="HiddenInvoicestatusId" runat="server" Value='<%#Eval("StatusId")%>' />
                  <asp:HiddenField ID="HiddenVerifyStatusId" runat="server" Value='<%#Eval("VerifyStatus")%>' />
                  </ItemTemplate><ItemStyle HorizontalAlign="Center" Width="25px" />
                  </asp:TemplateField>
                    <asp:BoundField DataField="RefNo" HeaderText="Ref #" SortExpression="RefNo" >
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VendorId" HeaderText="Vendor" SortExpression="VendorId" >
                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InvoiceAmount" HeaderText="Inv Amt" SortExpression="InvoiceAmount" >
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <%--  <asp:BoundField DataField="GST"  SortExpression="GST" HeaderText="GST" DataFormatString="{0:0.00}" HtmlEncode="false" >
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Totalinvoiceamount"  SortExpression="Totalinvoiceamount" HeaderText="Total Inv Amt" DataFormatString="{0:0.00}" HtmlEncode="false" >
                        <ItemStyle HorizontalAlign="Right" Width="140px" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="CurrencyId"  SortExpression="CurrencyId" HeaderText="Currency">
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InvNo"  SortExpression="InvNo" HeaderText="Inv #">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InvDate"  SortExpression="InvDate" HeaderText="Inv Dt.">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DueDate"  SortExpression="DueDate" HeaderText="Due Date">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Voucher" SortExpression="VoucherNo" >
                     <ItemStyle HorizontalAlign="Left"  Width="100px"/>
                     <ItemTemplate>
                      <asp:LinkButton ID="btnvouncherno" runat="server" Text='<%#Eval("VoucherId") %>' CommandArgument='<%#Eval("VoucherNo") %>' OnClick="Voucher_Click"  Font-Underline="false" CommandName="Select"></asp:LinkButton>  
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MTMVoucherNo" SortExpression="MTMVoucherNo" > 
                    <ItemTemplate>
                      <asp:LinkButton ID="btnvouncherno1" runat="server" Text='<%#Eval("MTMVoucherNo") %>'  Font-Underline="false" CommandName="Select"></asp:LinkButton>  
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="VesselId"  SortExpression="VesselId" HeaderText="VSL">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StatusId"  SortExpression="StatusId" HeaderText="Status">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                </Columns>
                <RowStyle CssClass="rowstyle" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
        </div>
    </fieldset>
    </asp:Panel>
           <asp:Panel runat="server" ID="pnl_Det" Visible ="false" >
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
        padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid; padding-left: 5px; padding-right: 5px">
        <legend><strong>Invoice Details</strong></legend> 
            <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td colspan="6">
                          <asp:HiddenField ID="HiddenEnquiry" runat="server" />
                      </td>
                    </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right;">
                          </td>
                          <td style="text-align: left; width: 218px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 117px;">
                          </td>
                          <td style="text-align: left; width: 118px;">
                          </td>
                          <td style="text-align: right; padding-right: 15px; width: 105px;">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; ">
                              Vendor:</td>
                          <td style="text-align: left; width: 218px;">
                              <asp:Label ID="lbl_Vendor" runat="server" Width="182px"></asp:Label></td>
                          <td align="right" style="text-align: right; padding-right:15px; width: 117px;">
                              Inv. Date:</td>
                          <td style="text-align: left; width: 118px;">
                              <asp:Label ID="lbl_InvoiceDate" runat="server" Width="131px"></asp:Label></td>
                          <td style="padding-right: 15px; text-align: right; width: 105px;">
                              Due Date:</td>
                          <td style="text-align: left;">
                              <asp:Label ID="lbl_DueDate" runat="server" Width="112px"></asp:Label></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 218px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 117px; height: 13px;">
                          </td>
                          <td style="text-align: left; width: 118px; height: 13px;">
                              </td>
                          <td style="text-align: right; padding-right: 15px; width: 105px; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; height: 4px;">
                              Vessel:</td>
                          <td style="text-align: left; height: 4px; width: 218px;">
                              <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" 
                                  TabIndex="7" Width="164px">
                              </asp:DropDownList>
                          </td>
                          <td align="right" style="text-align: right; padding-right:15px; width: 117px; height: 4px;">
                              Currency:</td>
                          <td style="text-align: left; width: 118px; height: 4px;">
                              <asp:DropDownList ID="ddCurrency" runat="server" CssClass="input_box" 
                                  TabIndex="4" Width="164px">
                              </asp:DropDownList>
                          </td>
                          <td style="padding-right: 15px; text-align: right; width: 105px; height: 4px;">
                              Inv. #:</td>
                          <td style="text-align: left; height: 4px;">
                              <asp:TextBox ID="txt_InvNo" runat="server" CssClass="required_box" 
                                  MaxLength="49" TabIndex="2" Width="126px"></asp:TextBox>
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right;">
                          </td>
                          <td style="text-align: left; width: 218px;">
                              <asp:RangeValidator ID="RangeValidator2" runat="server" 
                                  ControlToValidate="ddl_Vessel" ErrorMessage="Required." MaximumValue="1000000" 
                                  MinimumValue="1" Type="Integer"></asp:RangeValidator>
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 117px;">
                          </td>
                          <td style="text-align: left; width: 118px;">
                              </td>
                          <td style="text-align: right; padding-right: 15px; width: 105px;">
                          </td>
                          <td style="text-align: left">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                  ControlToValidate="txt_InvNo" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; height: 13px;">
                              Ref. #:</td>
                          <td style="text-align: left; height: 13px; width: 218px;">
                              <asp:Label ID="lbl_RefNo" runat="server" Width="154px"></asp:Label>
                          </td>
                          <td align="right" 
                              style="text-align: right; padding-right:15px; width: 117px; height: 13px;">
                  Voucher # :</td>
                          <td style="text-align: left; width: 118px; height: 13px;">
                  <asp:Label ID="lbl_VoucherNo" runat="server" Width="154px"></asp:Label></td>
                          <td style="padding-right: 15px; text-align: right; width: 105px; height: 13px;">
                              Inv. Amt.:</td>
                          <td style="text-align: left; height: 13px;">
                              <asp:TextBox ID="txt_InvAmount" runat="server" style="text-align :right " CssClass="required_box" MaxLength="12" TabIndex="3" Width="120px"></asp:TextBox>
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right;">
                          </td>
                          <td style="text-align: left; width: 218px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 117px;">
                          </td>
                          <td style="text-align: left; width: 118px;">
                          </td>
                          <td style="text-align: left; width: 105px;">
                          </td>
                          <td style="text-align: left">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                  ControlToValidate="txt_InvAmount" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                  ControlToValidate="txt_InvAmount" Display="Dynamic" 
                                  ErrorMessage="Till 2 decimal places only." 
                                  ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Width="147px"></asp:RegularExpressionValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; height: 12px;">
                  Total Inv. Amt. :</td>
                          <td style="text-align: left; height: 12px; width: 218px;">
                              <asp:Label ID="lbl_TotalInvAmt" runat="server"></asp:Label></td>
                          <td align="right" style="text-align: right; padding-right:15px; width: 117px; height: 12px;">
                  Remarks :</td>
                          <td style="text-align: left; height: 12px;" colspan="3">
                  <asp:Label ID="lbl_Remarks" runat="server" Width="100%"></asp:Label></td>
                      </tr>
          <tr>
              <td align="right" style="padding-right: 15px; text-align: right; height: 12px;">
              </td>
              <td style="text-align: left; height: 12px; width: 218px;">
                  &nbsp;
              </td>
              <td align="right" style="padding-right: 15px; text-align: right; width: 117px; height: 12px;">
              </td>
              <td style="text-align: left; width: 118px; height: 12px;">
              </td>
              <td style="text-align: right; width: 105px; height: 12px;">
              </td>
              <td style="text-align: left; height: 12px;">
              </td>
          </tr>
                     </table>
            </fieldset>
            <uc1:invoicestatus id="InvoiceStatus1" runat="server"></uc1:invoicestatus>
            <p style ="text-align :right ">
              <asp:Button ID="btn_Save" runat="server" CssClass="btn" OnClick="btn_Save_Click" TabIndex="6" Text="Save" Width="59px" />
            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" OnClick="btn_Cancel_Click" TabIndex="6" Text="Cancel" Width="59px" CausesValidation="false"  />
          </p>
            </asp:Panel> 
            </td></tr>
            <tr><td>
            </td></tr>
            </table>
            </td>
            </tr>
        </table>
</asp:Content>
<%--</form>
</body>
</html>
--%>