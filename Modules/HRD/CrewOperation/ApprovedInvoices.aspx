<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovedInvoices.aspx.cs" Inherits="CrewAccounting_ApprovedInvoices" MasterPageFile="~/Modules/HRD/InvoiceRegister.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" style="padding-right: 0px; padding-left: 0px;
                padding-bottom: 0px; padding-top: 0px">
                <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
    <tr><td style="width: 100%; padding :10px;padding-top: 0px">
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
            padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
            text-align: center">
            <legend><strong>Invoice Search</strong></legend>
              <table width="100%" cellpadding="0" cellspacing="0">
              <tr><td colspan="6">
                  &nbsp;</td>
                  <td colspan="1" style="width: 203px">
                  </td>
              </tr>
                <tr>
                 <td align="right" style="height: 19px; width: 110px;">
                     Ref. #:&nbsp;
                 </td>
                    <td align="left" style="height: 19px; width: 137px;">
                        <asp:TextBox ID="txtrefno" runat="server" CssClass="input_box"
                            TabIndex="1" Width="116px"></asp:TextBox></td>
                    <td align="right" style="height: 19px">
                        Inv. #:&nbsp;
                    </td>
                    <td align="left" style="height: 19px; width: 180px;">
                        <asp:TextBox ID="txtInvno" runat="server" CssClass="input_box" TabIndex="2"
                            Width="174px"></asp:TextBox></td>
                    <td align="right" style="height: 19px">
                        Vendor:&nbsp;
                    </td>
                    <td align="left" colspan="2" style="height: 19px">
                        <asp:DropDownList ID="ddvendor" runat="server" CssClass="input_box" TabIndex="3"
                            Width="374px">
                        </asp:DropDownList></td>
                </tr>
                  <tr>
                      <td align="right" style="width: 110px">&nbsp;
                      </td>
                      <td align="left" height="1" style="width: 137px">
                      </td>
                      <td align="right">
                      </td>
                      <td align="left" style="width: 180px">
                      </td>
                      <td align="right">
                      </td>
                      <td align="left" style="width: 186px">
                      </td>
                      <td align="left" style="width: 203px">
                      </td>
                  </tr>
                  <tr>
                      <td align="right" style="width: 110px">
                          Status:&nbsp;
                      </td>
                      <td align="left" height="1" style="width: 137px">
                          <asp:DropDownList ID="lblstatus" runat="server" CssClass="input_box" OnSelectedIndexChanged="lblstatus_SelectedIndexChanged"    >
                        
                             </asp:DropDownList></td>
                      <td align="right">
                        Vessel:&nbsp;
                      </td>
                      <td align="left" style="width: 180px">
                     <asp:DropDownList ID="ddVessel" runat="server" CssClass="input_box" TabIndex="4"
                         Width="179px">
                     </asp:DropDownList></td>
                      <td align="right">
                          MTM PV#:&nbsp;
                      </td>
                      <td align="right" colspan="1" style="text-align: left; width: 186px;">
                          <asp:TextBox ID="txtMTMPvNo" runat="server" CssClass="input_box" TabIndex="1" Width="116px"></asp:TextBox></td>
                      <td align="right" colspan="1" style="width: 203px; text-align: left">
        <asp:Button ID="btn_search" runat="server" CssClass="btn" OnClick="btn_search_Click"
            TabIndex="6" Text="Search" Width="59px" />
        <asp:Button ID="btn_clear" runat="server" CssClass="btn" OnClick="btn_clear_Click"
            TabIndex="7" Text="Clear" Width="59px" />
        <asp:Button ID="Button1" runat="server" CssClass="btn" Visible="false" OnClientClick="javscript:window.open('AllInvoiceList.aspx');return false;"
            TabIndex="8" Text="Enquiry" Width="60px" /></td>
                  </tr>
                  <tr>
                      <td align="right" style="width: 110px">
                          </td>
                      <td align="left" colspan="3">
                          </td>
                      <td align="right">
                      </td>
                      <td align="left" style="width: 186px">
                      </td>
                      <td align="left" style="width: 203px">
                      </td>
                  </tr>
              </table>
        </fieldset>
    </td></tr>
            <tr><td style="padding:10px;width :100%;" id="td1">
            <asp:Panel ID="panel_invoice" runat="server" Visible="true" Width="100%">
            <asp:Label id="Label1" runat="server" ForeColor="Red"></asp:Label>
    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
        padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid">
        <legend><strong>Invoice List</strong></legend>
        <div id="dd" style="overflow-y: scroll; overflow-x: scroll; width:100%; height: 210px; vertical-align:top;">
            <asp:GridView ID="gvinvoice" AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" runat="server" OnRowEditing="GridRowEditing" OnSelectedIndexChanged="gvinvoice_selectedIndex"  DataKeyNames="InvoiceId" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%" OnRowDataBound="gvinvoice_RowDataBound" OnPreRender="gvinvoice_PreRender" OnRowCommand="gvinvoice_rowCommand">
                <Columns>
                    <asp:BoundField DataField="RefNo" HeaderText="Ref #" SortExpression="RefNo" >
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Book">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkbook" runat="server" />
                    <asp:Label ID="lblvenid" runat="server" Text='<%# Eval("Vendor_Id") %>' Visible="false" ></asp:Label>
                    <asp:Label ID="lblvendor" runat="server" Text='<%# Eval("VendorId") %>' Visible="false" ></asp:Label>
                     <asp:Label ID="lblcurrency" runat="server" Text='<%# Eval("CurrencyId") %>' Visible="false" ></asp:Label>
                    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status_Id") %>' Visible="false" ></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pay">
                    <ItemTemplate>
                    <asp:CheckBox ID="chkpay" runat="server" />
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="VendorId"  SortExpression="VendorId" HeaderText="Vendor">
                        <ItemStyle HorizontalAlign="Left" Width="300px"/>
                    </asp:BoundField>
                     <asp:BoundField DataField="InvoiceAmount"  SortExpression="InvoiceAmount" HeaderText="Inv Amt" DataFormatString="{0:0.00}" HtmlEncode="false">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="CurrencyId"  SortExpression="CurrencyId" HeaderText="Curr">
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="InvNo" Visible ="false" SortExpression="InvNo" HeaderText="Inv #">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="InvNo." SortExpression="InvNo">
                    <ItemTemplate>
                    <asp:Label runat="Server" ID="l1" Text='<%# Eval("InvNo")%>' Width="70px"></asp:Label>
                    </ItemTemplate> 
                    <ItemStyle HorizontalAlign="Left"/>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Invdate" Visible="true" SortExpression="Invdate" HeaderText="Inv Dt.">
                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Duedate" Visible="true" SortExpression="Duedate" HeaderText="Due Date">
                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                    </asp:BoundField>
                      <asp:TemplateField HeaderText="Inv.Dt. <br/> Due Dt." SortExpression="InvDate" Visible="false" >
                    <ItemTemplate>
                    <%# Eval("InvDate") %><br /><%# Eval("InvDate") %>
                    </ItemTemplate> 
                    </asp:TemplateField>
                    <asp:BoundField DataField="PONo"  SortExpression="PONo" HeaderText="PO #">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PoAmount"  SortExpression="PoAmount" HeaderText="PO Amt" DataFormatString="{0:0.00}" HtmlEncode="false">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VesselId"  SortExpression="VesselId" HeaderText="VSL">
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalInvoiceAmount"  SortExpression="TotalInvoiceAmount" HeaderText="Approved Amount" DataFormatString="{0:0.00}" HtmlEncode="false">
                        <ItemStyle HorizontalAlign="Left" Width="180px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ApprovedBy"  SortExpression="ApprovedBy" HeaderText="Approved By" >
                        <ItemStyle HorizontalAlign="Left"  Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Voucher" SortExpression="VoucherNo" Visible="false"  >
                     <ItemStyle HorizontalAlign="Left"  Width="80px"/>
                     <ItemTemplate>
                      <asp:LinkButton ID="btnvouncherno" runat="server" Text='<%#Eval("VoucherId") %>'  Font-Underline="false" CommandName="Select"></asp:LinkButton>  
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MTMVoucherNo" SortExpression="MTMVoucherNo" > 
                    <ItemTemplate>
                      <asp:LinkButton ID="btnvouncherno1" runat="server" Text='<%#Eval("MTMVoucherNo") %>'  Font-Underline="false" CommandName="Edit"></asp:LinkButton>  
                      <asp:HiddenField ID="hiddenVoucherId" runat="server" Value='<%#Eval("VoucherNo") %>' />
                       </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="StatusId"  SortExpression="StatusId" HeaderText="Status">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True"> <ItemStyle Width="35px" /></asp:CommandField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" />
                            <asp:HiddenField ID="Hiddenfd11" runat ="server" Value='<%#Eval("Attachment") %>' />
                        </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="rowstyle" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
        </div>
      </fieldset>
    </asp:Panel>
            </td></tr>
            <tr><td align="right" style="padding-bottom: 7px; padding-right: 15px; text-align: right;"><asp:Button ID="btn_Book" runat="server" CssClass="btn" OnClick="btn_Book_Click"
            TabIndex="8" Text="Book" Width="59px" />
                <asp:Button ID="btnpay" runat="server" CssClass="btn" OnClick="btn_pay_Click"
            TabIndex="8" Text="Pay" Width="59px" />
                </td></tr>
    </table>
</asp:Content>
   