<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master" AutoEventWireup="true" CodeFile="PortAgent.aspx.cs" Inherits="CrewAccounting_PortAgent" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table cellspacing="0" border="1" width="100%">
<tr>
<td colspan="2" >
    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
    <asp:Label ID="lbl_gdsignoff_Message" runat="server" ForeColor="#C00000" Visible="False"></asp:Label>
    <div style="overflow-y: scroll; overflow-x: hidden; width:100%; height: 170px">
    <asp:GridView ID="GD_Signoff" runat="server" AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting"  AutoGenerateColumns="False" DataKeyNames="CrewId" GridLines="Horizontal" Style="text-align: center"
                Width="98%" OnRowDataBound="GD_Signoff_RowDataBound" OnPreRender="GD_Signoff_PreRender">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                        <asp:CheckBox ID="chk1" runat="Server" />     
                        <asp:Label ID="lblcontractid" runat="server" Text='<%# Eval("Contractid") %>' Visible="false" ></asp:Label>                   
                        <asp:HiddenField ID="hfd_OnOff" runat="server" Value='<%# Eval("OnOff") %>' ></asp:HiddenField>                   
                        </ItemTemplate>
                                           
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RankId" HeaderText="Rank" SortExpression="RankId">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CountryName" SortExpression="CountryName" HeaderText="Nationality">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SeamanBook" SortExpression="SeamanBook" HeaderText="Seaman Book">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Passport" SortExpression="Passport" HeaderText="Passport">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DOI" SortExpression="DOI" HeaderText="DOI">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DOE" SortExpression="DOE" HeaderText="DOE">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DOB" SortExpression="DOB" HeaderText="DOB">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="POB" SortExpression="POB" HeaderText="POB">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle CssClass="rowstyle" />
                    <SelectedRowStyle CssClass="selectedtowstyle" />
                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                </asp:GridView>
    </div>
</td>
</tr>
<tr>
<td align="center" style=" background-color :#e2e2e2;width:300px; vertical-align:top  ">
    <table width="97%" style="padding-right: 3px; height :160px;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 347px; text-align: right">&nbsp;
                </td>
                <td style="width: 185px; text-align: left">
                    </td>
            </tr>
            <tr>
                <td style="width: 347px; text-align: right;">
                    Port Agent:</td>
                <td style="width: 185px; text-align: left">
                    <asp:DropDownList ID="dp_port" runat="server" CssClass="required_box" 
                        AutoPostBack="True" OnSelectedIndexChanged="dp_port_SelectedIndexChanged" 
                        Width="215px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 347px; text-align: right; height: 13px;">
                </td>
                <td style="width: 185px; text-align: left; height: 13px;">
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dp_port"
                        ErrorMessage="Required." MaximumValue="10000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
            </tr>
            <tr>
                <td style="width: 347px; text-align: right; height: 13px;">
                    VSL ETA:</td>
                <td style="width: 185px; text-align: left; height: 13px;">
                    <asp:TextBox ID="txt_VSLETA" runat="server" CssClass="required_box" MaxLength="10" Width="102px"></asp:TextBox><asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_VSLETA" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_VSLETA" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                        
                        </td>
            </tr>
            <tr>
                <td style="width: 347px; text-align: right; height: 13px;">
                    VSL ETD:</td>
                <td style="width: 185px; text-align: left; height: 13px;">
                    <asp:TextBox ID="Txt_VSLETD" runat="server" CssClass="input_box" MaxLength="10" Width="102px"></asp:TextBox><asp:ImageButton ID="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Txt_VSLETD" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="Txt_VSLETD" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                   
            </tr>
            <tr>
                <td style="text-align: center; height: 13px;" colspan="2">
                    Total Crew Change at last Call is
                    <asp:Label ID="txtTotCrew" runat="server" ></asp:Label>&nbsp;and 
                    Previous Costs for this port for this Agent
                    <asp:Label ID="txt_prevCost" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 13px;" colspan="2">
                    &nbsp;&nbsp;</td>
            </tr>
            </table>
            <center >
    <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" OnClick="btn_Save_Click" />
    <asp:Button ID="btn_MakePO_Port" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_MakePO_Port_Click" Text="Make PO" Width="59px" />
    </center>
</td>
<td style=" width :760px;" >
<asp:Label ID="label1" runat="server" Text=""></asp:Label>
<div style="overflow-y: scroll; overflow-x:hidden; width:100%; height: 205px; text-align: left;">
            <asp:GridView ID="gdportcall" runat="server" DataKeyNames="PortAgentBookingId" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" AllowSorting="True" OnSorted="on_Sorted1" OnSorting="on_Sorting1" 
                Width="98%" OnSelectedIndexChanged="gdportcall_SelectedIndexChanged" OnRowDeleting="gdportcall_RowDeleting" OnRowCommand="gdportcall_RowCommand" OnPreRender="gdportcall_PreRender" OnRowDataBound="gdportcall_RowDataBound">
                 <Columns>
  
                                 <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                     
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="35px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                            Text="Delete" />
                                            <asp:HiddenField ID="hfd_Id" runat="Server" Value='<% # Eval("PortAgentBookingId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Port Agent RFQ" SortExpression="RFQNo">
                   <ItemTemplate>
                       <asp:Label ID="lbportAgent"  runat="server" Text='<%# Eval("RFQNo") %>'></asp:Label>
                       <asp:HiddenField ID="hfd_PortAgentName" runat="Server" Value='<% # Eval("PortAgentName") %>' />
                   </ItemTemplate>
                   </asp:TemplateField>
                    
                    <asp:BoundField DataField="ETA" HeaderText="Vsl ETA"  SortExpression="ETA">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="ETD" HeaderText="VSL ETD" SortExpression="ETD">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="Status" HeaderText="Rfq Status" SortExpression="Status">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Select">
                     <ItemStyle Width="30px" HorizontalAlign="Center" />
                     <ItemTemplate>
                      <asp:CheckBox ID="chk_port" runat="server" />
                      <asp:Label ID="lbl_vendorid" runat="server" Text='<%# Eval("PortAgentId") %>' Visible="false" ></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Send Mail">
                   <ItemTemplate>
                   <asp:ImageButton ID="img_mail" runat="server" CommandName="SendMail" ImageUrl="~/Modules/HRD/Images/mail.gif"  CausesValidation="false" />
                   </ItemTemplate>
                       <ItemStyle HorizontalAlign="Center" Width="70px" />
                   </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
        </div>
</td>
</tr>
</table>
<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_VSLETA"></ajaxToolkit:CalendarExtender>
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="Txt_VSLETD"></ajaxToolkit:CalendarExtender>
</asp:Content>






