<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TicketCancellation.aspx.cs" Inherits="CrewOperation_TicketCancellation" MasterPageFile="~/Modules/HRD/CrewPlanning.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div>
<asp:Label ID="lbl_Message" runat="server" ForeColor="#C00000" ></asp:Label>
<table cellpadding="0" cellspacing="0" style="width: 100%" border ="1">
<tr>
<td  style="background-color :#e2e2e2"   >
<table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: right; padding-right: 5px; height: 21px; width: 128px;">Vessel :</td>
                            <td style="text-align: left; height: 21px; " colspan="2">
                                <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" 
                                    Width="286px" TabIndex="1"></asp:DropDownList></td>
                            <td style="padding-right: 5px; text-align: right; height: 21px; width: 99px;">Travel Agent :</td>
                            <td style="text-align: left; width: 246px; height: 21px;">
                                <asp:DropDownList ID="ddl_TravelAgents" runat="server" CssClass="input_box" Width="251px" TabIndex="2"><asp:ListItem>&lt; Select &gt;</asp:ListItem></asp:DropDownList></td>
                            <td style="width: 59px; height: 21px; text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 5px; text-align: right; width: 128px;">
                                Po Date
                                From:</td>
                            <td style="text-align: left; width: 173px;">
                                <asp:TextBox ID="txt_from" runat="server" CssClass="input_box" MaxLength="15" Width="84px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_from"
                                    ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date" ValueToCompare="0" Display="Dynamic"></asp:CompareValidator>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                                </td>
                            <td style="text-align: left; width: 250px;">
                                <asp:TextBox ID="txt_to" runat="server" CssClass="input_box" MaxLength="15" Width="84px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_to"
                                    Display="Dynamic" ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date"
                                    ValueToCompare="0"></asp:CompareValidator>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                            <td style="padding-right: 5px; text-align: right; width: 99px;">
                                &nbsp;</td>
                            <td style="width: 246px; text-align: right">
                                <asp:Button ID="btnFilter" runat="server" CssClass="btn" OnClick="btn_Search_Click" TabIndex="6" Text="Search" Width="59px" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btn_Print" runat="server" CssClass="btn" OnClick="btn_Print_Click" TabIndex="6" Text="Print" Width="59px" /></td>
                            <td style="width: 59px; text-align: center">
                            </td>
                        </tr>
                        </table>
</td>
</tr>
<tr>
<td>
<center >
<asp:Label runat="Server" ID="lblRowCount" ForeColor="red" ></asp:Label> 
<div style=" height :320px; overflow-y :scroll; overflow-x:hidden;width:100% " >
 <asp:GridView ID="grid_List" runat="server" AllowSorting="true" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%" >
 <Columns>
     <asp:TemplateField HeaderText="Edit">
     <ItemTemplate>
        <asp:ImageButton CausesValidation="false" ID="ImageButton1" runat="Server" ImageUrl="~/Modules/HRD/Images/edit.jpg" CommandArgument='<% #Eval("PoId")%>' OnClick="Edit_Click" ToolTip='<% #Eval("Remarks")%>'/>    
     </ItemTemplate> 
     <ItemStyle Width="30px" HorizontalAlign="Center"/>
     </asp:TemplateField>
     <asp:BoundField DataField="VesselName" HeaderText="Vessel" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
     <asp:TemplateField HeaderText="PO#" >
        <ItemTemplate>
           <asp:HiddenField ID="hfd_PoId" Value='<%#Eval("PoId")%>' runat="server"></asp:HiddenField>
           <asp:HiddenField ID="hfd_VslId" Value='<%#Eval("VesselId")%>' runat="server"></asp:HiddenField>
           <asp:HiddenField ID="hfd_VendorId" Value='<%#Eval("VendorId")%>' runat="server"></asp:HiddenField>
           <asp:HiddenField ID="hfd_PoDate" Value='<%#Eval("PoDate")%>' runat="server"></asp:HiddenField>
           <asp:LinkButton runat="Server" ID="lnlHLink" Text='<%#Eval("PoNo")%>' OnClick="lnlHLink_Click"></asp:LinkButton>  
        </ItemTemplate>
     </asp:TemplateField>
     <asp:BoundField DataField="Company" HeaderText="Company" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
     <asp:BoundField DataField="Route" HeaderText="Route" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
     <asp:BoundField DataField="MemCount" HeaderText="Crew Count" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
     <asp:BoundField DataField="PoAmount" HeaderText="Po Amt" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
     <asp:BoundField DataField="CancelAmount" HeaderText="Ref. Amt" ><ItemStyle HorizontalAlign="Right" /></asp:BoundField>
     <asp:BoundField DataField="Status" HeaderText="Status" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
     
 </Columns>
 <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
 <SelectedRowStyle CssClass="selectedtowstyle" />
 <HeaderStyle CssClass="headerstylefixedheadergrid" />
</asp:GridView>
</div>
<table width="100%" cellpadding="0" cellspacing="0" style=" font-weight : bold ; background-color : #e2e2e2 ; height : 25px;" >
<tr>
<td style="width: 119px; height: 13px; text-align: right">Po Amount :</td>
<td style="height: 13px; text-align: left"><asp:Label runat="Server" ID="lblTotPoAmt"></asp:Label></td>
<td style="height: 13px; text-align: right">Refund Amount :</td>
<td style="height: 13px; text-align: left"><asp:Label runat="Server" ID="lblTotRefAmount"></asp:Label></td>
<td style="height: 13px; text-align: right">Refunded Amount :</td>
<td style="height: 13px; text-align: left"><asp:Label runat="Server" ID="lblTotRefAmountDone"></asp:Label></td>
<td style="height: 13px; text-align: right">Pending Amount :</td>
<td style="height: 13px; text-align: left"><asp:Label runat="Server" ID="lblPending"></asp:Label></td>
</tr>
</table>
</center> 
    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server" FilterType="Numbers,Custom" TargetControlID="txt_CancelAmt" FilterMode="ValidChars" ValidChars="."></ajaxToolkit:FilteredTextBoxExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgsignonas" PopupPosition="TopRight" TargetControlID="txt_RefDate"></ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_from"></ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txt_to"></ajaxToolkit:CalendarExtender>
</td>
</tr>
<tr>
<td >
<asp:Panel runat="Server" ID="pnl_Update" Width="100%">
<table cellpadding="0" cellspacing="0" width="100%" >
                <tr>
                    <td style="width: 151px; height: 13px;">
                    </td>
                    <td style="text-align: left; width: 112px; height: 13px;">
                        </td>
                    <td style="width: 123px; height: 13px;">
                    </td>
                    <td style="text-align: left; width: 262px; height: 13px;"></td>
                    <td style="text-align: left; height: 13px; width: 90px;">
                    </td>
                    <td style="text-align: left; width: 143px; height: 13px;">
                    </td>
                    <td style="width: 106px; height: 13px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right; width: 151px; height: 19px;">
                        Refund Amount :</td>
                    <td style="text-align: left; width: 112px; height: 19px;">
                        <asp:TextBox ID="txt_CancelAmt" runat="server" CssClass="input_box" TabIndex="3" MaxLength="10" style="text-align :right" ></asp:TextBox > </td>
                    <td style="padding-right: 5px; text-align: right; width: 123px; height: 19px;">
                        Refund Date :&nbsp;</td>
                    <td style="text-align: left; height: 19px; width: 262px;">
                        <asp:TextBox ID="txt_RefDate" runat="server" CssClass="required_box" Width="80px"></asp:TextBox>
                        <asp:ImageButton ID="imgsignonas" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txt_RefDate" ErrorMessage="Required." 
                            meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator10" runat="server" 
                            ControlToValidate="txt_RefDate" Display="Dynamic" ErrorMessage="Invalid Date." 
                            Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                    </td>
                    <td style="height: 19px; text-align: left; width: 90px;">
                        Status :</td>
                    <td style="height: 19px; text-align: left; width: 143px;">
                        <asp:DropDownList ID="ddl_Status" runat="server" CssClass="input_box" Width="106px" TabIndex="4">
                            <asp:ListItem Value="O">Open</asp:ListItem>
                            <asp:ListItem Value="C">Closed</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 106px; height: 19px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 16px; width: 151px;">
                    </td>
                    <td style="text-align: left; height: 16px; width: 112px;">
                        &nbsp;
                    </td>
                    <td style="height: 16px; width: 123px;">
                    </td>
                    <td colspan="1" style="text-align: left; height: 16px; width: 262px;">
                        &nbsp;</td>
                    <td colspan="1" style="height: 16px; text-align: left; width: 90px;">
                    </td>
                    <td colspan="1" style="height: 16px; text-align: left; width: 143px;">
                    </td>
                    <td colspan="1" style="width: 106px; height: 16px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; padding-right: 5px; width: 151px;" valign="top">
                        Remarks :</td>
                    <td colspan="5" style="text-align: left">
                        <asp:TextBox ID="txt_Remarks" runat="server" CssClass="input_box" Height="50px" TextMode="MultiLine"
                            Width="558px" TabIndex="5"></asp:TextBox></td>
                    <td style="width: 106px; text-align: left">
                            </td>
                </tr>
                <tr>
                    <td style="text-align: right; padding :2px;" colspan="7" >
                     <asp:Button ID="btn_Save" runat="server" CssClass="btn" OnClick="btn_Save_Click" TabIndex="6" Text="Save" Width="59px" /><asp:Button ID="btn_Cancel" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Cancel_Click" TabIndex="6" Text="Cancel" Width="59px" />
                    </td>
                </tr>
                       </table>
</asp:Panel>  
</td>
</tr>
</table>
</div>
</asp:Content>
