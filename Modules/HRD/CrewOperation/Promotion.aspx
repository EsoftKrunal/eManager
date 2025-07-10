<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Promotion.aspx.cs" Inherits="CrewOperation_Promotion" MasterPageFile="~/Modules/HRD/CrewPlanning.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<asp:HiddenField ID="HiddenPromotion" runat="server" />
        <table cellspacing="0" width="100%" border="1" >
            <tr>
                <td colspan="2" style=" text-align :center ">
                    <asp:Label ID="lbl_promotion_message" runat="server" ForeColor="#C00000"></asp:Label>
                    <asp:Label ID="Label1" runat="server" ForeColor="#C00000"></asp:Label>
                    <asp:Label ID="lbl_promotion_master" runat="server" ForeColor="#C00000"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
                        text-align: center">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td colspan="6">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="padding-right: 15px; text-align :right ">
                                    Emp.#:</td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmpNumber" MaxLength="6" runat="server" CssClass="input_box" AutoPostBack="True" OnTextChanged="txtEmpNumber_TextChanged" TabIndex="1"></asp:TextBox></td>
                                <td style="padding-right: 15px;text-align :right ">
                                    Name:</td>
                                <td align="left">
                                    <asp:Label ID="lbl_Name" runat="server" Width="100%"></asp:Label></td>
                                <td style="text-align: right; padding-right: 15px">
                                    Present Rank:</td>
                                <td align="right" style="text-align: left;">
                                    <asp:Label ID="lblPresentRank" runat="server"></asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="right">
                                </td>
                                <td align="left">
                                </td>
                                <td align="left" style="text-align: right">
                                </td>
                                <td align="right" style="text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 15px;text-align :right ">
                                    Status:</td>
                                <td align="left">
                                    <asp:Label ID="lbl_Status" runat="server" Width="122px"></asp:Label></td>
                                <td style="padding-right: 15px;text-align :right ">
                                    Vessel:</td>
                                <td align="left">
                                    <asp:Label ID="lblVessel" runat="server" Width="122px"></asp:Label></td>
                                <td style="text-align: right; padding-right: 15px">
                                    Signed Off:</td>
                                <td align="right" style="text-align: left">
                                    <asp:Label ID="lblSignedOff" runat="server" Width="122px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="right">
                                </td>
                                <td align="left">
                                </td>
                                <td align="left" style="text-align: right">
                                </td>
                                <td align="right" style="text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 15px;text-align :right ">
                                    Available Date:</td>
                                <td align="left">
                                    <asp:Label ID="lblAvailableDate" runat="server" Width="122px"></asp:Label></td>
                                <td style="padding-right: 15px;text-align :right ">
                                    Promotion Rank:</td>
                                <td align="left">
                                    <asp:DropDownList ID="dprank" runat="server" CssClass="input_box" Width="182px" TabIndex="2">
                                    </asp:DropDownList></td>
                                <td style="text-align: right; padding-right: 15px">
                                    Promotion Dt:</td>
                                <td align="right" style="text-align: left;">
                                    <asp:TextBox ID="txt_promotiodate" runat="server" CssClass="required_box" MaxLength="15" Width="80px" TabIndex="3"></asp:TextBox>
                                    <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    </td>
                                <td align="left">
                                    &nbsp;
                                    </td>
                                <td align="right">
                                </td>
                                <td align="left">
                                </td>
                                <td align="left" style="text-align: right">
                                </td>
                                <td align="right" style="text-align: left;">
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_promotiodate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="Req1" ControlToValidate="txt_promotiodate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                         
                                        
                                        </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmpNumber"
                                        ErrorMessage="Atleast 6 Chars Permitted." ValidationExpression="\d{6}" Visible="False"></asp:RegularExpressionValidator></td>
                                <td align="right">
                                </td>
                                <td align="left">
                                </td>
                                <td align="left" style="text-align: right">
                                </td>
                                <td align="right" style="text-align: left;">
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;background-color :#e2e2e2">
                    <asp:HiddenField ID="HiddenCurrentRank" runat="server" />
                    <asp:HiddenField ID="HiddenCompany" runat="server" />
                    <asp:HiddenField ID="HiddenVessel" runat="server" />
                    <asp:HiddenField ID="HiddenSignOnDate" runat="server" />
                    <asp:HiddenField ID="HiddenVesselType" runat="server" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txtEmpNumber" ValidChars="0123456789sSyY">
                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:Button ID="btnCheckPromotionCriteria" runat="server" CssClass="btn"
                        TabIndex="4" Text="Check Promotion Criteria" Width="160px" OnClick="btnCheckPromotionCriteria_Click" CausesValidation="false" Enabled="False"  />
                    <asp:Button ID="btn_SavePromotion" runat="server" CssClass="btn"
                        TabIndex="5" Text="Save" Width="59px" OnClick="btn_SavePromotion_Click" Enabled="False" /><ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_promotiodate">
                    </ajaxToolkit:CalendarExtender>
                        </td>
            </tr>
           
        </table>
</asp:Content>
