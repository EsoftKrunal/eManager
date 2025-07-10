<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master" AutoEventWireup="true" CodeFile="TravelBooking.aspx.cs" Inherits="CrewAccounting_TravelBooking" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">

    <script language="javascript">
function openpage()
{
var url = "../Registers/PopUp_NearestAirPort.aspx"
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
return false;
}
</script>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<asp:Label ID="lbl_gdsignoff_Message" runat="server" ForeColor="#C00000"></asp:Label>
<strong><asp:Label ID="lbl_portcallrefno" runat="server"></asp:Label></strong>
<table cellpadding="0" cellspacing="0" border="1" width="100%" >
    <tr>
    <td colspan="2">
    <asp:Label ID="lbl_gdsignoff" runat="server"></asp:Label>
            <div style="overflow-y: scroll; overflow-x: hidden; height: 162px; width: 100%">
                <asp:GridView ID="GD_Signoff" runat="server"  AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%" OnPreRender="GD_Signoff_PreRender" DataKeyNames="CrewId" OnRowDataBound="GD_Signoff_RowDataBound" CellPadding="0" CellSpacing="0"  >
                    <Columns>
                        <asp:TemplateField HeaderText="Select" SortExpression="Contractid" >
                        <ItemTemplate>
                        <asp:CheckBox ID="chk1" runat="Server" />
                        <asp:Label ID="lblcontractid" runat="server" Text='<%# Eval("Contractid") %>' Visible="false" ></asp:Label>                        
                        <asp:HiddenField ID="hfd_OnOff" runat="server" Value='<%# Eval("OnOff") %>' ></asp:HiddenField>     
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                        <asp:BoundField DataField="RankId" HeaderText="Rank" SortExpression="RankId" ><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>
                        <asp:BoundField DataField="CountryName" HeaderText="Nationality" SortExpression="CountryName" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                        <asp:BoundField DataField="SeaManBook"  SortExpression="SeaManBook" HeaderText="Seaman Book"><ItemStyle HorizontalAlign="Left" Width="100px" /></asp:BoundField>
                        <asp:BoundField DataField="Passport"  SortExpression="Passport" HeaderText="Passport"><ItemStyle HorizontalAlign="Left" Width="100px" /></asp:BoundField>
                        <asp:BoundField DataField="DOI"  SortExpression="DOI" HeaderText="DOI"><ItemStyle HorizontalAlign="Left" Width="90px" /></asp:BoundField>
                        <asp:BoundField DataField="DOE"  SortExpression="DOE" HeaderText="DOE"><ItemStyle HorizontalAlign="Left" Width="90px" /></asp:BoundField>
                        <asp:BoundField DataField="DOB"  SortExpression="DOB" HeaderText="DOB"><ItemStyle HorizontalAlign="Left" Width="90px" /></asp:BoundField>
                        <asp:BoundField DataField="POB"  SortExpression="POB" HeaderText="POB"><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                        <asp:BoundField DataField="Status"  SortExpression="Status" HeaderText="Status"><ItemStyle HorizontalAlign="Left" Width="50px" /></asp:BoundField>
                    </Columns>
                    <RowStyle CssClass="rowstyle" />
                    <SelectedRowStyle CssClass="selectedtowstyle" />
                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                </asp:GridView>
            </div>
    </td>
    </tr>
    <tr><td style="width:350px; background-color:#e2e2e2 ">
    
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: right; padding-right: 15px; width: 100px;">
                    From:</td>
                <td style="text-align: left; ">
                    <asp:DropDownList ID="dp_from" runat="server" CssClass="required_box" 
                        AutoPostBack="True" OnSelectedIndexChanged="dp_from_SelectedIndexChanged" 
                        Width="206px">
                    </asp:DropDownList>
                    <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                        OnClientClick="return openpage();" /></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 15px; width: 100px;">
                    &nbsp;</td>
                <td style="text-align: left; ">
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dp_from"
                        ErrorMessage="Required." MaximumValue="100000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 15px; width: 100px;">
                    To:</td>
                <td style="text-align: left; ">
                    <asp:DropDownList ID="dp_to" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="dp_to_SelectedIndexChanged" Width="206px">
                    </asp:DropDownList>
                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                        OnClientClick="return openpage();" /></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 100px;">
                </td>
                <td style="text-align: left; ">
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dp_to"
                        ErrorMessage="Required." MaximumValue="100000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 100px;">
                    Travel Agent:</td>
                <td style="text-align: left; ">
                    <asp:DropDownList ID="dp_travelAgent" runat="server" CssClass="required_box" 
                        OnSelectedIndexChanged="dp_travelAgent_SelectedIndexChanged" 
                        AutoPostBack="True" Width="206px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 100px;">
                    &nbsp;</td>
                <td style="text-align: left; ">
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="dp_travelAgent"
                        ErrorMessage="Required." MaximumValue="1000000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 100px;">
                    Class:</td>
                <td style="text-align: left; ">
                    <asp:DropDownList ID="dp_class" runat="server" CssClass="input_box">
                        <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                        <asp:ListItem Value="1">Economic</asp:ListItem>
                        <asp:ListItem Value="2">Business</asp:ListItem>
                        <asp:ListItem Value="3">First or Suit</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 100px;">
                    Depart Dt:</td>
                <td style="text-align: left; ">
                    <asp:TextBox ID="txt_depdate" runat="server" CssClass="required_box" MaxLength="10"
                        Width="102px"></asp:TextBox>
                    <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_depdate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_depdate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                    </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 19px; padding-right: 15px">
                    Previous fare for this route for this Travel Agent was:</td>
                <td style="height: 19px; text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="height: 19px; padding-right: 15px">
                    <asp:Label ID="txt_Fare" runat="server" ></asp:Label></td>
                <td style="height: 19px; text-align: left">
                    &nbsp;</td>
            </tr>
            </table>
            <center >
        <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_Click" />
        <asp:Button ID="btn_MakePO" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_MakePO_Click" Text="Make PO" Width="59px" />
        </center>
        </td>
        <td style="width:720px; vertical-align :top"  >
        <asp:Label ID="lbl_portcall" runat="server"></asp:Label>
        <div style="overflow-y: scroll; overflow-x: scroll; width:100%; height: 217px; text-align: left;">
            <asp:GridView ID="gdportcall" runat="server" AutoGenerateColumns="False"  AllowSorting="True" OnSorted="on_Sorted1" OnSorting="on_Sorting1" GridLines="Horizontal" Style="text-align: center" Width="97%" OnSelectedIndexChanged="gdportcall_SelectedIndexChanged" DataKeyNames="TravelBookingId" OnRowDeleting="gdportcall_Row_Deleting" OnPreRender="gdportcall_PreRender" OnRowCommand="gdportcall_RowCommand" CellPadding="0" CellSpacing="0"  >
                 <Columns>
                 <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True">
                 <ItemStyle Width="35px" />
                 </asp:CommandField>
                 <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="35px" />
                   <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                    <asp:HiddenField ID="hfd_Id" runat="Server" Value='<% # Eval("TravelBookingId") %>' />
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="RFQNo" SortExpression="RFQNo" HeaderText="Travel Rfq">
                   <ItemStyle HorizontalAlign="Left" Width="80px"/>
                   </asp:BoundField>
                   <asp:TemplateField HeaderText="From">
                   <ItemStyle HorizontalAlign="Left"/>
                   <ItemTemplate>
                   <asp:Label ID="lbl_FromAirport" runat="server" Text='<%# Eval("FromAirport") %>'></asp:Label>
                   </ItemTemplate>
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="To">
                     <ItemStyle HorizontalAlign="Left"/>
                     <ItemTemplate>
                      <asp:Label ID="lbl_ToAirport" runat="server" Text='<%# Eval("ToAirport") %>'></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Rfq Status">
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Select">
                     <ItemStyle Width="30px" HorizontalAlign="Center" />
                     <ItemTemplate>
                      <asp:CheckBox ID="chk_travel" runat="server" />
                      <asp:Label ID="lbl_vendorid" runat="server" Text='<%# Eval("TravelAgentId") %>' Visible="false" ></asp:Label>
                     </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Send Mail">
                   <ItemStyle Width="80px" />
                   <ItemTemplate>
                   <asp:ImageButton ID="img_mail" runat="server" CommandName="SendMail" ImageUrl="~/Modules/HRD/Images/mail.gif" CausesValidation="false" />
                   </ItemTemplate>
                   </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
        </div>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_depdate"></ajaxToolkit:CalendarExtender>
        </td>
    </tr>
</table>
</asp:Content>






