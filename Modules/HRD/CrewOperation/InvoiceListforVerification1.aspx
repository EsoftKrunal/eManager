<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceListforVerification1.aspx.cs" Inherits="CrewOperation_InvoiceListforVerification1" MasterPageFile="~/Modules/HRD/InvoiceRegister.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
     <table cellpadding="0" cellspacing="0" width="100%" border="0">
    <tr><td style="padding :10px; padding-top: 0px;" >
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;text-align: center">
            <legend><strong>Invoice Search</strong></legend>
              <table width="100%" cellpadding="0" cellspacing="0">
              <tr><td colspan="6" style="height: 13px">
                  &nbsp;</td></tr>
                <tr>
                 <td align="right" style="width: 76px">
                     Ref. #:&nbsp;
                 </td>
                    <td align="left" style="width: 127px">
                        <asp:TextBox ID="txtrefno" runat="server" CssClass="input_box"
                            TabIndex="1" Width="126px"></asp:TextBox></td>
                    <td align="right">
                        Inv. #.:&nbsp;
                    </td>
                    <td align="left" style="width: 129px">
                        <asp:TextBox ID="txtInvno" runat="server" CssClass="input_box" TabIndex="2"
                            Width="126px"></asp:TextBox></td>
                    <td align="right" style="width: 59px">
                        Vendor:&nbsp;
                    </td>
                    <td align="left" colspan="1" style="">
                        <asp:DropDownList ID="ddvendor" runat="server" CssClass="input_box" TabIndex="3"
                            Width="100%">
                        </asp:DropDownList></td>
                </tr>
                  <tr>
                      <td align="right" style="height: 13px; width: 76px;">
                      </td>
                      <td align="left" style="height: 13px; width: 127px;">
                          &nbsp;</td>
                      <td align="right" style="height: 13px">
                      </td>
                      <td align="left" style="height: 13px; width: 129px;">
                      </td>
                      <td align="right" style="height: 13px; width: 59px;">
                      </td>
                      <td align="left" style="height: 13px">
                      </td>
                  </tr>
  <tr>
       <td align="right" style="height: 19px; width: 76px;">Status :&nbsp;</td>
       <td align="left" style="height: 19px; width: 127px;">
           <asp:DropDownList ID="ddstatus" runat="server" CssClass="input_box" 
               TabIndex="5" Width="130px" AutoPostBack="True" 
               OnSelectedIndexChanged="ddstatus_SelectedIndexChanged"></asp:DropDownList></td>
       <td align="right" style="height: 19px">Vessel:&nbsp;</td>
       <td align="left" style="height: 19px; width: 129px;"><asp:DropDownList ID="ddVessel" runat="server" CssClass="input_box" TabIndex="4" Width="130px"></asp:DropDownList></td>
       <td align="right" style="height: 19px; width: 59px;"></td>
        <td align="left" style="height: 19px; padding-right: 12px; ; text-align: right;">
            <asp:Button ID="btn_search" runat="server" CssClass="btn" OnClick="btn_search_Click" TabIndex="6" Text="Search" Width="59px" CausesValidation="False" />
            <asp:Button ID="btn_clear" runat="server" CssClass="btn" OnClick="btn_clear_Click" TabIndex="7" Text="Clear" Width="59px" CausesValidation="False" />
            <asp:Button ID="Button1" Visible="false" runat="server" CssClass="btn" OnClientClick="javscript:window.open('AllInvoiceList.aspx');return false;" TabIndex="8" Text="Enquiry" Width="60px" />
        </td>
        </tr>
        </table>
        </fieldset>
    </td></tr>
    <tr>
    <td align="right">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: right; width: 146px;">Forward To User :</td>
                <td style="text-align: left; height: 19px;">
                    &nbsp;<asp:DropDownList ID="ddl_User" runat="server" CssClass="required_box" TabIndex="5" Width="145px"></asp:DropDownList>&nbsp;
                    <asp:Button ID="btnAddInvoice" runat="server" CssClass="btn" OnClick="btn_addinvoice_Click" TabIndex="8" Text="Forward" Width="99px" /></td>
            </tr>
            <tr>
                <td style="WIDTH: 146px">
                </td>
                <td style="text-align: left; height: 13px;">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_User"
                        ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
            </tr>
        </table>
    </td>
    </tr>
    <tr>
             <td align="center" style="height: 13px">
                 <asp:Label ID="Label2" runat="server" ForeColor="Red" Visible="False">Can't edit record because Record is already Approved.</asp:Label>
                 <asp:Label ID="lbl_Message" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>
            <tr><td style=" text-align:left;padding:10px;padding-top:0px;">
            <asp:Panel ID="panel_invoice" runat="server" Visible="true" Width="100%">
    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid; padding-left: 5px; padding-right: 5px; text-align: center;">
        <legend><strong>Invoice List</strong></legend>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <div style="overflow-y: scroll; overflow-x: hidden; width:100%; height: 200px">
            <asp:GridView ID="gvinvoice" runat="server"  AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" OnPreRender="gvinvoice_PreRender" OnRowDataBound="gvinvoice_DataBound" OnSelectedIndexChanged="gvinvoice_selectedIndex" OnRowEditing="gvinvoice_Row_Editing" DataKeyNames="InvoiceId" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center;"
                Width="98%">
                 <Columns>
                 <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                    <asp:TemplateField HeaderText="Fwd">
                    <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chk_Ok" Checked="false" />
                    </ItemTemplate><ItemStyle HorizontalAlign="Center" Width="25px" />
                    </asp:TemplateField>
                
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
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VendorId" HeaderText="Vendor" SortExpression="VendorId" >
                        <ItemStyle HorizontalAlign="Left"/>
                    </asp:BoundField>
                     <asp:BoundField DataField="InvoiceAmount" HeaderText="Inv Amt" SortExpression="InvoiceAmount" >
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="CurrencyId"  SortExpression="CurrencyId" HeaderText="Curr">
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="InvNo" Visible="false"  SortExpression="InvNo" HeaderText="Inv #">
                        <ItemStyle HorizontalAlign="Left"/>
                    </asp:BoundField>
                      <asp:TemplateField HeaderText="InvNo." SortExpression="InvNo">
                    <ItemTemplate>
                    <asp:Label runat="Server" ID="l1" Text='<%# Eval("InvNo")%>' style="" Width="150px"></asp:Label>
                    </ItemTemplate> 
                    <ItemStyle HorizontalAlign="Left"/>
                    </asp:TemplateField>
                    <asp:BoundField DataField="InvDate" Visible ="true" SortExpression="InvDate" HeaderText="Inv Dt.">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField> 
                    <asp:TemplateField HeaderText="Inv.Dt. <br/> Due Dt." SortExpression="InvDate" Visible="false" >
                    <ItemTemplate>
                    <%# Eval("InvDate") %><br /><%# Eval("InvDate") %>
                    </ItemTemplate> 
                    </asp:TemplateField>
                    <asp:BoundField DataField="DueDate" Visible ="true" SortExpression="DueDate" HeaderText="Due Date">
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PoNo" Visible="false" SortExpression="PoNo"  HeaderText="PO #">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PoAmount" Visible="false" SortExpression="PoAmount" HeaderText="PO Amt" DataFormatString="{0:0.00}" HtmlEncode="false">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VesselId"  SortExpression="VesselId" HeaderText="VSL">
                        <ItemStyle HorizontalAlign="Left" Width="30px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StatusId"  SortExpression="StatusId" HeaderText="Status">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
            <asp:BoundField DataField="Forwardedto" HeaderText="Forward To">
              <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </asp:BoundField>
                </Columns>
                <RowStyle CssClass="rowstyle" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
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
 