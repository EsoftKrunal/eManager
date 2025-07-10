<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceApprovalSearchScreen.aspx.cs" Inherits="CrewOperation_InvoiceApprovalSearchScreen" MasterPageFile="~/Modules/HRD/InvoiceRegister.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="text-align:center">
    <form id="form1" runat="server">--%>
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr><td style=" padding :10px; padding-top :0px;"  >
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
            padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
            text-align: center">
            <legend><strong>Invoice Search</strong></legend>
              <table width="100%" cellpadding="0" cellspacing="0">
              <tr><td colspan="6">
                  &nbsp;</td></tr>
                <tr>
                 <td align="right" style="width: 97px">
                     Ref. #:&nbsp;
                 </td>
                    <td align="left" style="width: 165px">
                        <asp:TextBox ID="txtrefno" runat="server" CssClass="input_box"
                            TabIndex="1" Width="116px"></asp:TextBox></td>
                    <td align="right">
                        Inv. #.:&nbsp;
                    </td>
                    <td align="left" style="width: 224px">
                        <asp:TextBox ID="txtInvno" runat="server" CssClass="input_box" TabIndex="2"
                            Width="164px"></asp:TextBox></td>
                    <td align="right">
                        Vendor:&nbsp;
                    </td>
                    <td align="left" colspan="1" style="width: 328px">
                        <asp:DropDownList ID="ddvendor" runat="server" CssClass="input_box" TabIndex="3"
                            Width="327px">
                        </asp:DropDownList></td>
                </tr>
                  <tr>
                      <td align="right" style="width: 97px; height: 13px;">
                      </td>
                      <td align="left" style="width: 165px; height: 13px">
                          &nbsp;
                      </td>
                      <td align="right" style="height: 13px">
                      </td>
                      <td align="left" style="width: 224px; height: 13px">
                      </td>
                      <td align="right" style="height: 13px">
                      </td>
                      <td align="left" style="width: 328px; height: 13px">
                      </td>
                  </tr>
                  <tr>
                      <td align="right" style="width: 97px">
                          Status:&nbsp;
                      </td>
                      <td align="left" style="width: 165px">
                          <asp:DropDownList ID="ddl_Status" runat="server" CssClass="input_box">
                              <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                              <asp:ListItem Value="1">Approved</asp:ListItem>
                              <asp:ListItem Value="2">UnApproved</asp:ListItem>
                              <asp:ListItem Value="3">Paid</asp:ListItem>
                              <asp:ListItem Value="4">UnPaid</asp:ListItem>
                          </asp:DropDownList></td>
                      <td align="right">
                        Vessel:&nbsp;
                      </td>
                      <td align="left" style="width: 224px">
                     <asp:DropDownList ID="ddVessel" runat="server" CssClass="input_box" TabIndex="4"
                         Width="169px">
                     </asp:DropDownList></td>
                      <td align="right">
                      </td>
                      <td align="left" style="padding-right: 15px; width: 328px; text-align: right">
        <asp:Button ID="btn_search" runat="server" CssClass="btn" OnClick="btn_search_Click"
            TabIndex="6" Text="Search" Width="59px" />
        <asp:Button ID="btn_clear" runat="server" CssClass="btn" OnClick="btn_clear_Click"
            TabIndex="7" Text="Clear" Width="59px" />
        <asp:Button ID="Button1" runat="server" CssClass="btn" Visible="false" OnClientClick="javscript:window.open('AllInvoiceList.aspx');return false;"
            TabIndex="8" Text="Enquiry" Width="60px" /></td>
                  </tr>
                  <tr>
                      <td align="right" style="width: 97px">
                          </td>
                      <td align="left" colspan="3" rowspan="1">
                          </td>
                      <td align="right">
                      </td>
                      <td align="left" style="width: 328px">
                      </td>
                  </tr>
              </table>
        </fieldset>
    </td></tr>
            <tr>
            <td style ="text-align:left;padding :10px; width :100%" >
            <asp:Panel ID="panel_invoice" runat="server" Visible="true" Width="100%">
    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
        padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid; padding-left: 5px; padding-right: 5px; text-align: center;">
        <legend><strong>Invoice List</strong></legend>
        <asp:Label ID="lbl_gvinvoice" runat="server"></asp:Label>
        <div id="dd" style="overflow-y: scroll; overflow-x: hidden; width:100%; height: 210px">
            <asp:GridView ID="gvinvoice" runat="server"  AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" OnPreRender="gvinvoice_PreRender" OnSelectedIndexChanged="gvinvoice_selectedIndex" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%" DataKeyNames="InvoiceId" OnRowDataBound="gvinvoice_DataBound">
                <Columns>
                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                    <asp:TemplateField HeaderText="">
                          <ItemTemplate>
                          <asp:Image ID="imgattach" runat="server" ImageUrl="~/Modules/HRD/Images/paperclip.gif" />
                          <asp:HiddenField ID="Hiddenfd11" runat ="server" Value='<%#Eval("Attachment") %>' />
                          </ItemTemplate><ItemStyle HorizontalAlign="Center" Width="25px" />
                     </asp:TemplateField>
                    <asp:BoundField DataField="RefNo" HeaderText="Ref #" SortExpression="RefNo" >
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VendorId" HeaderText="Vendor" SortExpression="VendorId" >
                        <ItemStyle HorizontalAlign="Left" Width="300px"/>
                    </asp:BoundField>
                     <asp:BoundField DataField="InvoiceAmount"  SortExpression="InvoiceAmount" HeaderText="Inv Amt" DataFormatString="{0:0.00}" HtmlEncode="False" >
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CurrencyId"  SortExpression="CurrencyId" HeaderText="Curr.">
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="InvNo"  SortExpression="InvNo" HeaderText="Inv #">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="InvDate"  SortExpression="InvDate" HeaderText="Inv Dt.">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DueDate"  SortExpression="DueDate" HeaderText="Due Date">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PoNo"  SortExpression="PoNo" HeaderText="PO #">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PoAmount"  SortExpression="PoAmount" HeaderText="PO Amt" DataFormatString="{0:0.00}" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VesselId"  SortExpression="VesselId" HeaderText="VSL">
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
            </td></tr>
            <tr><td>
            </td></tr>
    </table>
    </asp:Content>
    <%--</form>
    </body>
    </html>
--%>