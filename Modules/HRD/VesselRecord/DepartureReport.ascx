<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartureReport.ascx.cs" Inherits="VesselRecord_DepartureReport" %>
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table width="100%" cellpadding="5" cellspacing="0">
  <tr><td style="text-align: center">
  <asp:Label ID="lbl_Msg" runat="Server" ForeColor="Red"></asp:Label></td></tr>
  <tr><td style="text-align:center; padding-top: 15px;"> <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
  <legend><strong>Departure Report</strong></legend>
  <div style=" height:100px; overflow-x:hidden; overflow-y:scroll" align="center">
  <asp:Label ID="lbl_Msg_Grid" runat="server" ></asp:Label>
  <asp:GridView ID="gv_Departure" runat="server" AutoGenerateColumns="False" OnPreRender="gv_PreRender" DataKeyNames="DepartureReportId" GridLines="Horizontal" Height="9px" PageSize="3" Style="text-align: center" Width="98%" OnSelectedIndexChanged="gv_Departure_SelectedIndexChanged" OnRowDeleting="gv_Departure_RowDeleting" OnRowEditing="gv_Departure_RowEditing">
  <Columns>
       <asp:CommandField ButtonType="Image" HeaderText="View" meta:resourcekey="CommandFieldResource1" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" />
       <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ShowEditButton="True"><ItemStyle Width="30px" /></asp:CommandField>
       <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="30px" /><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" Text="Delete" /></ItemTemplate></asp:TemplateField>
       <asp:TemplateField HeaderText="Date/Time" >
       <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label ID="lbl_Display" runat="server" Text='<%# Eval("ArrivalTime")%>'></asp:Label>
                <asp:HiddenField  runat="Server" ID="hfd_DepartureId" Value='<%# Eval("DepartureReportId")%>'/>
            </ItemTemplate>
       </asp:TemplateField>
       <asp:BoundField DataField="PortName" HeaderText="Port"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
       <asp:BoundField DataField="Lat" HeaderText="Lat"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
       <asp:BoundField DataField="Long" HeaderText="Long"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
       <asp:BoundField HeaderText="Cargo Name" DataField="Name_of_Cargo"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
       <asp:BoundField HeaderText="Stowage" DataField="Stowage"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
    </Columns>
    <SelectedRowStyle CssClass="selectedtowstyle" />
    <HeaderStyle CssClass="headerstylefixedheadergrid" />
    <RowStyle CssClass="rowstyle" />
 </asp:GridView>
 </div> 
    </fieldset></td></tr>
    <tr>
        <td style="padding-top: 15px">
        <asp:Panel ID="pnl_Departure" runat="server" Width="100%">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                            <legend><strong>Details</strong></legend>
                            <table cellpadding="0" cellspacing="0" width="100%" style="padding-right:3px" >
                                <tr>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        &nbsp;</td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                        PORT :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:DropDownList ID="ddl_Port" runat="server" CssClass="required_box" TabIndex="1"
                                            Width="125px">
                                        </asp:DropDownList></td>
                                    <td align="left" style="text-align: right">
                                    DATE :</td>
                                    <td align="left" style="text-align: left">
                                    <asp:TextBox ID="txt_ArrivalDate" runat="server" CssClass="required_box" MaxLength="10"
                                        TabIndex="2" Width="102px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server"
                                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                    <td align="left" style="text-align: right">
                                        TIME(hh:mm) :</td>
                                    <td align="left" style="text-align: left">
                                    <asp:TextBox ID="txt_ArrivalHour" runat="server" CssClass="required_box" MaxLength="2"
                                        Width="18px" TabIndex="3"></asp:TextBox>
                                        :
                                    <asp:TextBox ID="txt_ArrivalMinuts" runat="server" CssClass="required_box" MaxLength="2"
                                        Width="18px" TabIndex="4"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddl_Port"
                                            ErrorMessage="Required." MaximumValue="100000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_ArrivalDate"
                                            ErrorMessage="Required."></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_ArrivalDate"
                                            ErrorMessage="Invalid Date." Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></td>
                                    <td align="left" style="text-align: right">
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txt_ArrivalHour"
                                            ErrorMessage="Required(00-23)." MaximumValue="23" MinimumValue="0" Type="Integer"></asp:RangeValidator></td>
                                    <td align="left" style="text-align: left">
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txt_ArrivalMinuts"
                                            ErrorMessage="Required(00-59)." MaximumValue="59" MinimumValue="0" Type="Integer"></asp:RangeValidator></td>
                                </tr>
                            <tr>
                                <td align="left" style="text-align: right">
                                                      LAT :</td>
                                <td align="left" style="text-align: left">
                                    <asp:TextBox ID="txt_Lat" runat="server" CssClass="input_box" MaxLength="29" TabIndex="5"></asp:TextBox></td>
                                <td align="left" style="text-align: right">
                                            LONG :</td>
                                <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_Long" CssClass="input_box" runat="server" MaxLength="29" TabIndex="6"></asp:TextBox></td>
                                <td align="left" style="text-align: right">
                                    DRAFT FWD(m) :</td>
                                <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_DraftFWD" CssClass="input_box" runat="server" MaxLength="29" TabIndex="7"></asp:TextBox></td>
                            </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                        &nbsp;</td>
                                    <td align="left" style="text-align: left">
                                        &nbsp;</td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                        DRAFT AFT(m) :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_DraftAFT" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="8"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        RECD FUEL(mts) :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_RecdFuel" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="9"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        RECD IFO(mts) :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_RecdIFO" CssClass="input_box" runat="server" MaxLength="29" TabIndex="10"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                        RECD MDO(mts) :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_RecdMDO" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="11"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        RECD FW(mts) :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_RecdFW" runat="server" CssClass="input_box" MaxLength="29" TabIndex="12"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        ROB FUEL(mts) :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_ROBFuel" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="13"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                        ROB IFO :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_RobIFO" runat="server" CssClass="input_box" MaxLength="29" TabIndex="14"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        ROB MDO :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_RobMDO" runat="server" CssClass="input_box" MaxLength="29" TabIndex="15"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        ROB FW :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_RobFWD" runat="server" CssClass="input_box" MaxLength="29" TabIndex="16"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                        ETA NEXT PORT :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_ETANextPort" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="17"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        DIST. NEXT PORT:</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_DistanceNextPort" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="18"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        ACTIVITY :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_CargoActivity" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="19"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                        CARGO NAME :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_CargoName" runat="server" CssClass="input_box" MaxLength="49"
                                            TabIndex="20"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        SHIP'S QTY(mt) :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_ShipsQty" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="21"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        SHORE QTY(mt) :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_ShoreQty" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="22"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                        STOWAGE :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_Stowage" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="23"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        TANK CAPACITY(mt) :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_TankCapacityEach" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="24"></asp:TextBox></td>
                                    <td align="left" style="text-align: right">
                                        TIME LOG :</td>
                                    <td align="left" style="text-align: left">
                                        <asp:TextBox ID="txt_TimeLog" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="25"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                    <td align="left" style="text-align: right">
                                    </td>
                                    <td align="left" style="text-align: left">
                                    </td>
                                </tr>
                  <tr>
                      <td colspan="6" style="text-align: right">
                          &nbsp;<asp:HiddenField ID="hfd_DepartureId" runat="server" />
                          <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true"
                ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_ArrivalDate">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                PopupButtonID="ImageButton1" PopupPosition="TopLeft" TargetControlID="txt_ArrivalDate">
            </ajaxToolkit:CalendarExtender>
            &nbsp;&nbsp;</td>
    </tr>
  
    
             </table>
             </fieldset>
        </asp:Panel>
        </td>
    </tr>
   <tr>
        <td style="text-align: right">
           <asp:Button ID="btn_Add" runat="server" CssClass="btn" Text="Add" CausesValidation="False" Width="50px" OnClick="btn_Add_Click" TabIndex="26" />
            <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="50px" OnClick="btn_Save_Click" TabIndex="27" />
            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" Text="Cancel" CausesValidation="False" Width="50px" OnClick="btn_Cancel_Click" TabIndex="28" /></td>
    </tr>
</table>