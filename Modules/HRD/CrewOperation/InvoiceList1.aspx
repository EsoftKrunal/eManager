<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceList1.aspx.cs" Inherits="CrewOperation_InvoiceList1" MasterPageFile="~/Modules/HRD/InvoiceRegister.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr><td style="padding :10px; padding-top:0px;" >
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
            padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
            text-align: center">
            <legend><strong>Invoice Search</strong></legend>
              <table width="100%" cellpadding="0" cellspacing="0">
              <tr><td colspan="8">
                  &nbsp;</td></tr>
                <tr>
                 <td align="right" style="width: 56px">
                     </td>
                    <td align="left">
                        </td>
                    <td align="right">
                        </td>
                    <td align="left">
                        </td>
                    <td align="right">
                        </td>
                    <td align="left" colspan="3">
                        </td>
                </tr>
                  <tr>
                      <td align="right" style="width: 56px">
                     Ref. #:&nbsp;
                      </td>
                      <td align="left">
                          &nbsp;<asp:TextBox ID="txtrefno" runat="server" CssClass="input_box"
                            TabIndex="1" Width="106px" MaxLength="10"></asp:TextBox></td>
                      <td align="right">
                        Inv. #.:&nbsp;
                      </td>
                      <td align="left">
                        <asp:TextBox ID="txtInvno" runat="server" CssClass="input_box" TabIndex="2"
                            Width="102px" MaxLength="45"></asp:TextBox></td>
                      <td align="right">
                        Vendor:&nbsp;
                      </td>
                      <td align="left" style="width: 391px">
                        <asp:DropDownList ID="ddvendor" runat="server" CssClass="input_box" TabIndex="3"
                            Width="100%">
                        </asp:DropDownList></td>
                      <td align="right">
                        Vessel:&nbsp;
                      </td>
                      <td align="left">
                     <asp:DropDownList ID="ddVessel" runat="server" CssClass="input_box" TabIndex="4"
                         Width="160px">
                     </asp:DropDownList></td>
                  </tr>
                  <tr>
                      <td align="right" style="width: 56px">
                      </td>
                      <td align="left">
                      </td>
                      <td align="right">
                      </td>
                      <td align="left">
                      </td>
                      <td align="right">
                      </td>
                      <td align="left" style="width: 391px">
                      </td>
                      <td align="right">
                      </td>
                      <td align="left">
                      </td>
                  </tr>
              </table>
        </fieldset>
    </td></tr>
    <tr>
    <td style="padding-top: 0px;padding-right: 12px" align="right">
        <asp:DropDownList ID="ddstatus" runat="server" CssClass="input_box" TabIndex="5" Width="145px" Visible="False"></asp:DropDownList>
        <asp:Button ID="btn_search" runat="server" CssClass="btn" OnClick="btn_search_Click"
            TabIndex="6" Text="Search" Width="59px" />
        <asp:Button ID="btn_clear" runat="server" CssClass="btn" OnClick="btn_clear_Click"
            TabIndex="7" Text="Clear" Width="59px" />
        <asp:Button ID="btnAddInvoice" runat="server" CssClass="btn" OnClick="btn_addinvoice_Click"
            TabIndex="8" Text="Add Invoice" Width="88px" />
        <asp:Button ID="Button1" Visible="false" runat="server" CssClass="btn" OnClientClick="javscript:window.open('AllInvoiceList.aspx');return false;" 
            TabIndex="8" Text="Enquiry" Width="60px" /></td>
     </tr>
         <tr>
             <td align="center">
                 <asp:Label ID="Label2" runat="server" ForeColor="Red" Visible="False">Can't edit record because Record is already Approved.</asp:Label></td>
         </tr>
            <tr>
            <td style="text-align:left; padding :10px;padding-top:0px;">
            <asp:Panel ID="panel_invoice" runat="server" Visible="true" Width="100%">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid; padding-left: 5px; padding-right: 5px">
        <legend><strong>Invoice List</strong></legend>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <div style="overflow-y: scroll; overflow-x: hidden; width:100%; height: 250px">
            <asp:GridView ID="gvinvoice" runat="server"  AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" OnPreRender="gvinvoice_PreRender" OnRowDataBound="gvinvoice_DataBound" OnSelectedIndexChanged="gvinvoice_selectedIndex" OnRowEditing="gvinvoice_Row_Editing" DataKeyNames="InvoiceId" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" CellSpacing="0" CellPadding="0" Width="98%">
                 <Columns>
                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                                 <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>
                               <asp:TemplateField HeaderText="">
                          <ItemTemplate>
                          <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" />
                          <asp:HiddenField ID="Hiddenfd11" runat ="server" Value='<%#Eval("Attachment") %>' />
                           <asp:HiddenField ID="HiddenInvoicestatusId1" runat="server" Value='<%#Eval("Status_Id")%>' />
                          <asp:HiddenField ID="HiddenInvoicestatusId" runat="server" Value='<%#Eval("StatusId")%>' />
                          <asp:HiddenField ID="HiddenVerifyStatusId" runat="server" Value='<%#Eval("VerifyStatus")%>' />
                          </ItemTemplate><ItemStyle HorizontalAlign="Center" Width="25px" />
                     </asp:TemplateField>
                    <asp:BoundField DataField="RefNo" HeaderText="Ref #" SortExpression="RefNo" >
                        <ItemStyle HorizontalAlign="Left" Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VendorId" HeaderText="Vendor" SortExpression="VendorId" >
                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="InvoiceAmount" HeaderText="Inv Amt" SortExpression="InvoiceAmount" DataFormatString="{0:0.00}" HtmlEncode="False" >
                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="CurrencyId"  SortExpression="CurrencyId" HeaderText="Curr.">
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="InvNo"  SortExpression="InvNo" HeaderText="Inv #">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InvDate"  SortExpression="InvDate" HeaderText="Inv Dt.">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DueDate"  SortExpression="DueDate" HeaderText="Due Date">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PoNo" SortExpression="PoNo"  HeaderText="PO #">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PoAmount"  SortExpression="PoAmount" HeaderText="PO Amt" DataFormatString="{0:0.00}" HtmlEncode="False" >
                        <ItemStyle HorizontalAlign="Left" Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VesselId"  SortExpression="VesselId" HeaderText="VSL">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StatusId"  SortExpression="StatusId" HeaderText="Status">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                </Columns>
                <RowStyle CssClass="rowstyle" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid"  />
            </asp:GridView>
        </div>
    </fieldset>
    </asp:Panel>
            </td></tr>
            <tr><td>
                &nbsp;
            </td></tr>
    </table>
    </asp:Content>
 