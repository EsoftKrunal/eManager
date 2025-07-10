<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportTraverse.aspx.cs" Inherits="CrewAccounting_ExportTraverse" MasterPageFile="~/Modules/HRD/CrewAccounts.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>--%>
        <table cellpadding="10" cellspacing="0" style="width: 795px">
            <tr>
                <td class="textregisters" colspan="2" style="padding-right: 0px; padding-left: 0px;
                    padding-bottom: 0px; padding-top: 0px; height: 15px; text-align:center">
                    <strong>Export To Traverse</strong></td>
            </tr>
            <tr>
                <td>
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
                        text-align: center;">
                        <table cellpadding="5" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="8">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 192px">
                                    Ref. #:</td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="input_box" Width="98px"></asp:TextBox></td>
                                <td align="right" style="text-align: right; width: 261px;">
                                    Inv. #:</td>
                                <td align="left">
                                    <asp:DropDownList ID="dpmonth" runat="server" CssClass="input_box" TabIndex="3" Width="145px">
                                        <asp:ListItem Value="0">< Select ></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td align="right" style="width: 62px; text-align: right">
                                    Vendor:</td>
                                <td align="left" style="width: 150px">
                                    <asp:DropDownList ID="ddvendor" runat="server" CssClass="input_box" TabIndex="3"
                                        Width="119px">
                                        <asp:ListItem Value="0">< Select ></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td align="right" style="text-align: right">
                                    Vessel:</td>
                                <td align="right" style="text-align: left; width: 268px;">
                                    <asp:DropDownList ID="dpvessel" runat="server" CssClass="input_box" TabIndex="3" Width="145px">
                                        <asp:ListItem Value="0">< Select ></asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 192px; height: 43px;">
                                    Status:</td>
                                <td align="left" colspan="3" style="height: 43px">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="input_box" Height="36px" TextMode="MultiLine"
                                        Width="183px"></asp:TextBox></td>
                                <td align="right" style="width: 62px; text-align: right; height: 43px;">
                                </td>
                                <td align="left" style="width: 150px; height: 43px">
                                </td>
                                <td align="right" style="text-align: right; height: 43px;">
                                </td>
                                <td align="right" style="text-align: right; height: 43px; width: 268px;">
                                <asp:Button ID="btn_search" runat="server" CssClass="btn" TabIndex="6" Text="Search" Width="59px" OnClick="btn_search_Click" />
                                    <asp:Button ID="btnAddInvoice" runat="server" CssClass="btn" OnClick="btnAddInvoice_Click" TabIndex="7" Text="Clear" Width="65px" /></td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-top:0px;padding-bottom:10px">
                       <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid">
                            <legend><strong>Invoice List</strong></legend>
                            <div style="overflow-y: scroll; overflow-x: scroll; width: 795px; height: 150px">
                                <asp:GridView ID="gvinvoice" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    Style="text-align: center" Width="120%">
                                    <Columns>
                                        <%--<asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                            ShowSelectButton="True">
                                            <ItemStyle Width="35px" />
                                        </asp:CommandField>--%>
                                        
                                         <asp:CommandField ItemStyle-Width="30px" ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True"></asp:CommandField>
                                        
                                        <asp:BoundField DataField="RefNo" HeaderStyle-Width="100" HeaderText="Ref.#">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvAmt" HeaderText="Inv.Amt">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvNo" HeaderText="Inv.#">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvDt" HeaderText="Inv.Dt.">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DueDate" HeaderText="Due Date">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                                                   <asp:BoundField DataField="PONo" HeaderText="PO.#">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="POAmt" HeaderStyle-Width="150" HeaderText="PO Amt">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VesselCode" HeaderText="Vessel Code">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status" HeaderText="Status">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                       </Columns>
                                    <RowStyle CssClass="rowstyle" />
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                   </td>
            </tr>
        </table>
         </asp:Content>
  <%--  </div>
    </form>
</body>
</html>
--%>