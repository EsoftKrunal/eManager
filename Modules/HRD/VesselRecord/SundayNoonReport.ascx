<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SundayNoonReport.ascx.cs" Inherits="SundayNoonReport" %>
<%--<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
</head>--%>
  <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table width="100%" cellpadding="5" cellspacing="0">
  <tr><td style="text-align: center">
  <asp:Label ID="lbl_Msg" runat="Server" ForeColor="Red"></asp:Label></td></tr>
                   <tr><td style=" text-align:center; padding-top: 15px;"> <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                                <legend><strong>Sunday Noon Report</strong></legend>
                                 <div style=" height:100px; overflow-x:hidden; overflow-y:scroll" align="center">
                                <asp:Label ID="lbl_Msg_Grid" runat="server" ></asp:Label>
                                    <asp:GridView ID="gv_Arrival" runat="server" AutoGenerateColumns="False" OnPreRender="gv_PreRender" DataKeyNames="SundayNoonReportId" GridLines="Horizontal" PageSize="3" Style="text-align: center" Width="98%" OnSelectedIndexChanged="gv_Arrival_SelectedIndexChanged" OnRowDeleting="gv_Arrival_RowDeleting" OnRowEditing="gv_Arrival_RowEditing">
                                                <Columns>
                                                   <asp:CommandField ButtonType="Image" HeaderText="View" meta:resourcekey="CommandFieldResource1" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" />
                                                   <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ShowEditButton="True"><ItemStyle Width="30px" /></asp:CommandField>
                                                   <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="30px" /><ItemTemplate><asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" Text="Delete" /></ItemTemplate></asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Date/Time" >
                                                    <ItemStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Display" runat="server" Text='<%# Eval("ArrivalTime")%>'></asp:Label>
                                                            <asp:HiddenField  runat="Server" ID="hfd_SundayNoonId" Value='<%# Eval("SundayNoonReportId")%>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Lat" HeaderText="Lat"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                                    <asp:BoundField DataField="Long" HeaderText="Long"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                                    <asp:BoundField HeaderText="Course" DataField="Course"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                                    <asp:BoundField HeaderText="Present Speed" DataField="Present_speed"><ItemStyle HorizontalAlign="Left"/></asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                <RowStyle CssClass="rowstyle" />
                                            </asp:GridView>
                                                                                        </div> 
    </fieldset></td></tr>
    <tr>
        <td style="padding-top: 15px">
        <asp:Panel ID="pnl_Arrival" runat="server" Width="100%">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;">
                            <legend><strong>Details</strong></legend>
                            <table cellpadding="0" cellspacing="0" width="100%" style="padding-right:3px;" >
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                    </td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                    </td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                        
                                    DATE :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                    <asp:TextBox ID="txt_Arrival" runat="server" CssClass="required_box" MaxLength="10"
                                        TabIndex="1" Width="120px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server"
                                            ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                        
                                    TIME (hh:mm) :</td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                    <asp:TextBox ID="txt_ArrivalHour" runat="server" CssClass="required_box" MaxLength="2"
                                        Width="18px" TabIndex="2"></asp:TextBox>
                                        :
                                    <asp:TextBox ID="txt_ArrivalMinuts" runat="server" CssClass="required_box" MaxLength="2"
                                        Width="18px" TabIndex="3"></asp:TextBox></td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                        
                                                      LAT :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                    <asp:TextBox ID="txt_Lat" CssClass="input_box" runat="server" MaxLength="29" TabIndex="4"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Arrival"
                                            ErrorMessage="Required."></asp:RequiredFieldValidator><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_Arrival"
                                            ErrorMessage="Invalid Date." Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txt_ArrivalHour"
                                            ErrorMessage="Required(00-23)." MaximumValue="23" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator></td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txt_ArrivalMinuts"
                                            ErrorMessage="Required(00-59)." MaximumValue="59" MinimumValue="0" Type="Integer" Display="Dynamic"></asp:RangeValidator></td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                        &nbsp;</td>
                                </tr>
                            <tr>
                                <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                   
                                            LONG :</td>
                                <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        <asp:TextBox ID="txt_Long" CssClass="input_box" runat="server" MaxLength="29" TabIndex="5"></asp:TextBox></td>
                                <td align="left" style="height: 15px; text-align: right; width: 118px;">
                                   
                                        COURSE :</td>
                                <td align="left" style="height: 15px; text-align: left; width: 125px;">
                                        <asp:TextBox ID="txt_Course" CssClass="input_box" runat="server" MaxLength="29" TabIndex="6"></asp:TextBox></td>
                                <td align="left" style="height: 15px; text-align: right; width: 155px;">
                                   
                                        PRESENT SPEED :</td>
                                <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                        <asp:TextBox ID="txt_P_Speed" CssClass="input_box" runat="server" MaxLength="29" TabIndex="7"></asp:TextBox></td>
                            </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                        &nbsp;</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="height: 15px; text-align: right; width: 118px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 125px;">
                                        &nbsp;
                                    </td>
                                    <td align="left" style="height: 15px; text-align: right; width: 155px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                        DIST. SAILED FROM LAST PORT :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        <asp:TextBox ID="txt_Distance_sailed" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="8"></asp:TextBox></td>
                                    <td align="left" style="height: 15px; text-align: right; width: 118px;">
                                        AVG. SPEED :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 125px;">
                                        <asp:TextBox ID="txt_avg_speed" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="9"></asp:TextBox></td>
                                    <td align="left" style="height: 15px; text-align: right; width: 155px;">
                                       
                                        KTS :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                        <asp:TextBox ID="txt_kts" runat="server" CssClass="input_box" MaxLength="29" TabIndex="10"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="height: 15px; text-align: right; width: 118px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 125px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: right; width: 155px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                        DIST. TO GO :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        <asp:TextBox ID="txt_distToGo" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="11"></asp:TextBox></td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                        
                                        NM :</td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left;">
                                        <asp:TextBox ID="txt_NM" runat="server" CssClass="input_box" MaxLength="29" TabIndex="12"></asp:TextBox></td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right;">
                                        M.E. FO CONS :</td>
                                    <td align="left" style="height: 15px; text-align: left;width: 143px;">
                                        <asp:TextBox ID="txt_MEFOCons" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="13"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        &nbsp;</td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                    </td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                        
                                            AVG M.E. FO CONS<br />
                                        (MT/ DAY)
                                            :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        <asp:TextBox ID="txt_AVGMEFOCONS" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="14"></asp:TextBox></td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right;">
                                        
                                            AUX. MDO CONS :</td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left;">
                                        <asp:TextBox ID="txt_auxMDOCONS" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="15"></asp:TextBox></td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right;">
                                       
                                            AVG AUX CONS (MT/DAY) :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                        <asp:TextBox ID="txt_AVGAUXCONSMT" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="16"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        &nbsp;</td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                    </td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                        ROB IFO :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        <asp:TextBox ID="txt_ROBIFO" runat="server" CssClass="input_box" MaxLength="29" TabIndex="17"></asp:TextBox></td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right;">
                                       ROB DO :</td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left;">
                                        <asp:TextBox ID="txt_ROBDO" runat="server" CssClass="input_box" MaxLength="29" TabIndex="18"></asp:TextBox></td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right;">
                                        
                                            ROB FW :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                        <asp:TextBox ID="txt_ROBFW" runat="server" CssClass="input_box" MaxLength="29" TabIndex="19"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        &nbsp;</td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                    </td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                        ETA PORT :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        <asp:DropDownList ID="ddport" runat="server" CssClass="input_box" TabIndex="20" Width="128px">
                                        </asp:DropDownList></td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right;">
                                        
                                            STOPPAGES :</td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                        <asp:TextBox ID="txt_Stoppages" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="21"></asp:TextBox></td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                        
                                            STOPPAGE REASON :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                        <asp:TextBox ID="txt_StoppageReason" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="22"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        &nbsp;</td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                    </td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                        
                                            WEATHER DELAYS TO VSL :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        <asp:TextBox ID="txt_WeathyerDelays" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="23"></asp:TextBox></td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right;">
                                        WIND :</td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left;">
                                        <asp:TextBox ID="txt_wind" runat="server" CssClass="input_box" MaxLength="29" TabIndex="24"></asp:TextBox></td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right;">
                                        DIRECTION :</td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                        <asp:TextBox ID="txt_Direction" runat="server" CssClass="input_box" MaxLength="29"
                                            TabIndex="25"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 15px; text-align: right; width: 235px;">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 177px;">
                                        &nbsp;</td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                    </td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="height: 15px; text-align: left; width: 143px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 235px; height: 15px; text-align: right">
                                        
                                            FORCE :</td>
                                    <td align="left" style="width: 177px; height: 15px; text-align: left;">
                                        <asp:TextBox ID="txt_Force" runat="server" CssClass="input_box" MaxLength="29" TabIndex="26"></asp:TextBox></td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right;">
                                        SEAS :</td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left;">
                                        <asp:TextBox ID="txt_seas" runat="server" CssClass="input_box" MaxLength="29" TabIndex="27"></asp:TextBox></td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 143px; height: 15px; text-align: left;">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 235px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 177px; height: 15px; text-align: left">
                                        &nbsp;</td>
                                    <td align="left" style="width: 118px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 125px; height: 15px; text-align: left">
                                    </td>
                                    <td align="left" style="width: 155px; height: 15px; text-align: right">
                                    </td>
                                    <td align="left" style="width: 143px; height: 15px; text-align: left">
                                    </td>
                                </tr>
                  <tr>
                      <td colspan="6" style="height: 7px; text-align: right">
                          &nbsp;<asp:HiddenField ID="hfd_SundayNoonId" runat="server" />
                          <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true"
                ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Arrival">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                PopupButtonID="ImageButton1" PopupPosition="TopLeft" TargetControlID="txt_Arrival">
            </ajaxToolkit:CalendarExtender>
            &nbsp;&nbsp;</td>
    </tr>
  
    
             </table>
             </fieldset>
        </asp:Panel>
        </td>
    </tr>
   <tr>
        <td style="height: 7px; text-align: right">
           <asp:Button ID="btn_Add" runat="server" CssClass="btn" Text="Add" CausesValidation="False" Width="50px" OnClick="btn_Add_Click" TabIndex="28" />
            <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="50px" OnClick="btn_Save_Click" TabIndex="29" />
            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" Text="Cancel" CausesValidation="False" Width="50px" OnClick="btn_Cancel_Click" TabIndex="30" /></td>
    </tr>
</table>

