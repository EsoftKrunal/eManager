<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WagePayment.aspx.cs" Inherits="CrewAccounting_WagePayment" MasterPageFile="~/Modules/HRD/CrewAccounts.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table cellspacing="0" style="width: 100%">
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 46px;padding: 0px 10px 0px 10px">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
                        text-align: center;">
                        <legend><strong> Wage Payment </strong>
                        </legend>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="text-align: right">
                                </td>
                                <td style="text-align: left; width: 213px;">
                                    &nbsp;</td>
                                <td style="text-align: right">
                                </td>
                                <td style="text-align: left">
                                </td>
                                <td style="text-align: right">
                                </td>
                                <td style="text-align: left">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right; padding-right: 5px;">
                                    Vessel:</td>
                                <td style="height: 19px; text-align: left; width: 213px;">
                                    <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" TabIndex="3"
                                        Width="198px">
                                    </asp:DropDownList></td>
                                <td style="height: 19px; text-align: right; padding-right: 5px;">
                                    Month:</td>
                                <td style="height: 19px; text-align: left">
                                    <asp:DropDownList ID="ddl_Month" runat="server" CssClass="required_box" TabIndex="3"
                                        Width="94px">
                                        <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                        <asp:ListItem Value="7">Jul</asp:ListItem>
                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="height: 19px; text-align: right; padding-right: 5px;">
                                    Year:</td>
                                <td style="height: 19px; text-align: left">
                                    <asp:DropDownList ID="ddl_Year" runat="server" CssClass="required_box" TabIndex="3"
                                        Width="94px">
                                        <asp:ListItem Value="0">&lt; Select &gt; </asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="height: 19px">
                                    <asp:Button ID="btn_search" runat="server" CssClass="btn" OnClick="btn_search_Click"
                                        TabIndex="6" Text="Show" Width="59px" /></td>
                            </tr>
                            <tr>
                                <td style="height: 13px; text-align: right">
                                </td>
                                <td style="height: 13px; text-align: left; width: 213px;">
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_Vessel"
                                        ErrorMessage="Required." MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td style="height: 13px; text-align: right">
                                </td>
                                <td style="height: 13px; text-align: left">
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_Month"
                                        ErrorMessage="Required." MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td style="height: 13px; text-align: right">
                                </td>
                                <td style="height: 13px; text-align: left">
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Year"
                                        ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
                                <td style="height: 13px">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;padding: 0px 10px 0px 10px" id="td_list"> 
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid; text-align: center;">
                            <legend><strong>Wages</strong></legend>
                    <div style="overflow-y: scroll; overflow-x: scroll;width: 800px; height: 150px" id="dvcont">
                        <table cellpadding="0" cellspacing="0" width="300%">
                            <tr>
                                <td valign="top" width="300">
                                    <asp:GridView ID="gv_Main" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                        Style="text-align: center" Width="100%" OnPreRender="gv_Main_PreRender">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Emp.#" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="lbl_CrewId" runat="server" Value='<%# Eval("CrewId") %>' />
                                                      <asp:HiddenField ID="lbl_ContractId" runat="server" Value='<%# Eval("ContractId") %>'></asp:HiddenField>
                                    
                                                    <asp:Label ID="lbl_EmpNo" runat="server" Text='<%# Eval("CrewNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Rank" HeaderText="Rank">
                                                <ItemStyle HorizontalAlign="Left" Width="70px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <RowStyle CssClass="rowstyleAccounts" />
                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                    </asp:GridView>
                                </td>
                                <td style="text-align: right" valign="top">
                                    <asp:GridView ID="gv_Income" runat="server" AutoGenerateColumns="True" GridLines="Horizontal"
                                        Style="text-align: center" Width="100%">
                                        <RowStyle CssClass="rowstyleAccounts" HorizontalAlign="right" />
                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                    </asp:GridView>
                                </td>
                                <td style="text-align: right" valign="top">
                                    <asp:GridView ID="gv_Expence" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                        Style="text-align: center" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="lbl_CrewId" runat="server" Value='<%# Eval("CrewId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UnfixedOt" DataFormatString="{0:0.00}" HeaderText="UnfixedOt" HtmlEncode="false" />
                                            <asp:BoundField DataField="OtherAmount" DataFormatString="{0:0.00}" HeaderText="OtherAmount" HtmlEncode="false" />
                                            <asp:BoundField DataField="PaYDays" HeaderText="PayDays" />
                                            <asp:BoundField DataField="TotalEarning" DataFormatString="{0:0.00}" HeaderText="Total Earning"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="Allotments" DataFormatString="{0:0.00}" HeaderText="Allotments"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="CashAdvance" DataFormatString="{0:0.00}" HeaderText="Cash Advance"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="BondedStores" DataFormatString="{0:0.00}" HeaderText="Bonded Stores"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="RadioTeleCall" DataFormatString="{0:0.00}" HeaderText="RadioTele-Call"
                                                HtmlEncode="false" />
                                            <asp:BoundField DataField="OtherDeductions" DataFormatString="{0:0.00}" HeaderText="Other Deductions"
                                                HtmlEncode="false" />
                                             </Columns>
                                        <RowStyle CssClass="rowstyleAccounts" HorizontalAlign="right" />
                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                    </asp:GridView>
                                </td>
                                 <td valign="top" style=" text-align:right" >
                                    <asp:GridView ID="gv_Expence1" runat="server" AutoGenerateColumns="True" GridLines="Horizontal"
                                    Style="text-align: center" Width="100%">
                                    <Columns>
                                                 
                                    </Columns>
                                    <RowStyle CssClass="rowstyleAccounts" HorizontalAlign="right" />
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            </asp:GridView>
                            </td>
                            <td valign="top" style=" text-align:right" >
                                    <asp:GridView ID="gv_Last" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    Style="text-align: center" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="NetEarning" DataFormatString="{0:0.00}" HtmlEncode="false" HeaderText="Net Earning"></asp:BoundField>
                                        <asp:BoundField DataField="Leave" DataFormatString="{0:0.00}" HtmlEncode="false" HeaderText="Leave"></asp:BoundField>
                                        <asp:BoundField DataField="OpBal" DataFormatString="{0:0.00}" HtmlEncode="false" HeaderText="Op. Bal.(L)"></asp:BoundField>
                                        <asp:BoundField DataField="Bonus" DataFormatString="{0:0.00}" HtmlEncode="false" HeaderText="Bonus"></asp:BoundField>
                                        <asp:BoundField DataField="OpBalBonus" DataFormatString="{0:0.00}" HtmlEncode="false" HeaderText="Op. Bal.(Bonus)"></asp:BoundField>
                                        <asp:BoundField DataField="TotPayable" DataFormatString="{0:0.00}" HtmlEncode="false" HeaderText="Total Payable"></asp:BoundField>
                                        <asp:BoundField DataField="NetPayable" DataFormatString="{0:0.00}" HtmlEncode="false" HeaderText="Net Payable"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Paid" >
                                        <ItemTemplate>
                                        <asp:HiddenField Value='<%# Eval("TotalEarning") %>' id="hfd_TotalEarning" runat="server"/>
                                        <asp:CheckBox ID="chk_Select" runat="server" /></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="rowstyleAccounts" HorizontalAlign="right" />
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            </asp:GridView>
                            </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Label ID="lbl_gv_Main" runat="server"></asp:Label></fieldset>
                    <br />
                    </td>
            </tr>
            <tr>
                <td style="padding-bottom: 10px; text-align: right">
                    <asp:Button ID="btnAddInvoice" runat="server" CssClass="btn" OnClick="btnAddInvoice_Click"
                        TabIndex="7" Text="Save" Width="65px" />
                    <asp:Button ID="btnprint" runat="server" CssClass="btn" OnClick="btnprint_Click1"
                        TabIndex="7" Text="Print" Width="65px" /></td>
            </tr>
           
        </table>
           <script language ="javascript" type="text/javascript"  >
        dvcont.style.width=(document.getElementById('td_list').offsetWidth-30) + 'px'; 
        </script>
        </asp:Content>
  
