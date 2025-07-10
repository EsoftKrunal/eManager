<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master" AutoEventWireup="true" CodeFile="CrewInactiveDetails.aspx.cs" Inherits="CrewOperation_CrewInactiveDetails" Title="Crew Inactive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Record Successfully Saved." Visible="False"></asp:Label>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
 <table width="100%" cellpadding="0" cellspacing ="0" border="1">
   <tr><td>
        <table border="0" cellpadding="3" cellspacing="0" width="100%">
            <tr>
                <td style="padding-right: 5px; width: 153px;" align="right">Employee #:</td>
                <td align="left" ><asp:TextBox ID="txt_empno" runat="server" 
                        CssClass="required_box" Width="100px" AutoPostBack="True" MaxLength="6" 
                        OnTextChanged="txt_empno_TextChanged"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_empno" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                <td align="right" style="padding-right: 5px;">Name:</td>
                <td align="left"><asp:TextBox ID="txt_Name" runat="server" CssClass="input_box" 
                        ReadOnly="True" Width="190px"></asp:TextBox></td>
                <td align="right" style="padding-right: 5px; width: 94px;">Nationality:</td>
                <td align="left"><asp:TextBox ID="txt_Nationality" runat="server" CssClass="input_box" ReadOnly="True" Width="140px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px; width: 153px; ">Present Rank:</td>
                <td align="left"><asp:TextBox ID="txt_PresentRank" runat="server" 
                        CssClass="input_box" ReadOnly="True" Width="100px"></asp:TextBox></td>
                <td align="right" style="padding-right: 5px;"> Last Vessel:</td>
                <td align="left" ><asp:TextBox ID="txt_LastVessel" runat="server" 
                        CssClass="input_box" ReadOnly="True" Width="190px"></asp:TextBox></td>
                <td style="width: 94px; text-align: right">Signed Off:</td>
                <td style="text-align: left" ><asp:TextBox ID="txt_SignedOff" runat="server" CssClass="input_box" ReadOnly="True" Width="140px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px; width: 153px; ">Available Date:</td>
                <td align="left" ><asp:TextBox ID="txt_AvailableDate" runat="server" 
                        CssClass="input_box" ReadOnly="True" Width="100px"></asp:TextBox></td>
                <td align="right" style="padding-right: 5px;">Current Reason:</td>
                <td align="left" >
                    <asp:Label ID="lblLastReason" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 94px; text-align: right;" >Current Status:</td>
                <td style ="text-align : left " >
                    <asp:Label ID="lblLastStatus" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px; width: 153px; height: 6px;">Active/InActive Date:</td>
                <td align="left" style="height: 6px">
                    <asp:TextBox ID="txt_NTBRDate" runat="server" CssClass="required_box" 
                        Width="80px" TabIndex="1"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_NTBRDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_NTBRDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                </td>
                <td align="right" style="padding-right: 5px; height: 6px;">Reason:</td>
                <td align="left" style="height: 6px"><asp:DropDownList ID="dd_NTBRReason" 
                        runat="server" CssClass="required_box" Width="195px" TabIndex="2"></asp:DropDownList>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dd_NTBRReason" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                <td style="padding-right: 5px; width: 94px; height: 6px; text-align :right ">New Status:</td>
                <td style="height: 6px; text-align: left">
                    <asp:DropDownList ID="ddntbr" runat="server" CssClass="required_box" Width="148px" AutoPostBack="True" OnSelectedIndexChanged="ddntbr_SelectedIndexChanged" TabIndex="3">
                    <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                    <asp:ListItem Value="4">InActive</asp:ListItem>
                    <asp:ListItem Value="2">Active</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddntbr" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px; width: 153px;">Remarks:</td>
                <td align="left" colspan="5" rowspan="3"> <asp:TextBox ID="txt_Remarks" runat="server" CssClass="input_box" Height="48px" TextMode="MultiLine" Width="530px" MaxLength="999" TabIndex="4"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px; width: 153px;"></td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px; width: 153px; "></td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 5px; width: 153px;"></td>
                <td align="left" colspan="5" rowspan="1" >
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_NTBRDate"></ajaxToolkit:CalendarExtender>
                </td>
            </tr>
        </table>
    </td></tr>
    <tr>
    <td style=" text-align :right; background-color :#e2e2e2 ">
    <asp:Button ID="btn_save" runat="server" CssClass="btn" OnClick="btn_save_Click" Text="Save" Width="59px" TabIndex="5" /></td></tr>
 </table>
</asp:Content>

