<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreatePortCall.ascx.cs" Inherits="UC_CrewOperation_CreatePortCall" %>
<style type="text/css">
    .style4
    {
        width: 66px;
        text-align: right;
    }
    .style5
    {
        width: 229px;
    }
</style>
 <link rel="stylesheet" href="../../../css/app_style.css" />
     <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script language="javascript">
function openpage()
{
var url = "../Registers/PopUp_Port.aspx";
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=420,height=170,left=50,top=50,addressbar=no');
return false;
}
</script>
<div>
    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
<table cellspacing="0" cellpadding="0" width="100%" style=" background-color:#f9f9f9;font-family:Arial;font-size:12px;" >
<tr>
<td style="text-align:center">
<asp:Label ID="lb_msg" runat="server" ForeColor="Red" Width="100%" ></asp:Label>
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_from"></ajaxToolkit:CalendarExtender>
<ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txt_to"> </ajaxToolkit:CalendarExtender>
<table style="background-color:#f9f9f9" border="1" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td style =" background-color :#e2e2e2;width:270px;  vertical-align :top" rowspan="2">
<table cellpadding="0" cellspacing="0" width="95%">
<tr>
    <td colspan="2" >&nbsp;</td>
</tr>
<tr>
    <td style="text-align: right" >Vessel:</td>
    <td style="text-align: left" class="style5"><asp:DropDownList ID="ddl_VesselName" Width="176px" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_VesselName_SelectedIndexChanged"></asp:DropDownList></td>
</tr>
<tr>
    <td style="text-align: right" >&nbsp;</td>
    <td style="text-align: left" class="style5"><asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddl_VesselName" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator> </td>
</tr>
<tr>
    <td>Country:</td>
    <td style="text-align: left" class="style5" > <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="176px"></asp:DropDownList></td>
</tr>
<tr>
    <td>&nbsp;</td>
    <td style="text-align: left" class="style5" > <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Required." MaximumValue="10000" MinimumValue="1" Type="Integer"></asp:RangeValidator> </td>
</tr>
<tr>
    <td style="text-align: right" >Port:</td>
    <td style="text-align: left" class="style5"><asp:DropDownList ID="ddl_port" Width="176px" runat="server" CssClass="required_box"></asp:DropDownList>
    <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClientClick="return openpage();" Width="16px" /></td>
</tr>
<tr>
    <td >&nbsp;</td>
    <td style="text-align: left" class="style5"> <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_port" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator> </td>
</tr>
<tr>
        <td style="text-align: right" >ETA:</td>
        <td style="text-align: left" class="style5">
            <asp:TextBox ID="txt_from" runat="server" CssClass="required_box" MaxLength="2000" Width="90px"></asp:TextBox>
            <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
    </tr>
<tr>
        <td >&nbsp;</td>
        <td style="text-align: left" class="style5">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_from" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
        </td>
    </tr>
    <tr>
        <td style="text-align: right" >ETD:</td>
        <td style="text-align: left" class="style5">
            <asp:TextBox ID="txt_to" runat="server" CssClass="input_box" MaxLength="2000" Width="90px"></asp:TextBox>
            <asp:ImageButton ID="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
            
            </td>
    </tr>
    
    <tr>
        <td >&nbsp;</td>
        <td style="text-align: left" class="style5">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_to" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                               
        </td>
    </tr>
    
    <tr>
        <td style="text-align: right" class="style4">
            &nbsp;</td>
        <td style="text-align: left; padding-left: 116px;" class="style5">
<asp:Button ID="btn_save" Text="Save" runat="server" Width="59px" OnClick="btn_save_Click" CssClass="btn" />
                                                                </td>
                                                            </tr>
                                                        </table>
                    </td>
                    <td style =" background-color :#e2e2e2">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>&nbsp;</td>
                                <td bgcolor="#C9F4DA" style="width: 31px; border:black 1px solid;">&nbsp;</td>
                                <td style="text-align: left">&nbsp;Sign On</td>
                                <td bgcolor="lightyellow" style="width: 33px; border:black 1px solid;">&nbsp;</td>
                                <td style="text-align: left" width="600">&nbsp;Sign Off</td>
                            </tr>
                         </table>
                    </td>
                    </tr>
                    <tr>
                    <td >
                    <div style=" overflow-x:hidden; overflow-y: scroll; height: 325px; width: 100%">
                        <asp:GridView ID="gvsearch" runat="server"  AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" AutoGenerateColumns="False" DataKeyNames="CrewId"
                            GridLines="Horizontal" OnRowCommand="gvsearch_RowCommand" Style="text-align: center"
                            Width="98%" OnRowDataBound="gvsearch_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Image" Visible="False" HeaderText="Select" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True" >
                                    <ItemStyle Width="30px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Delete" Visible="False">
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="img_Delete" runat="Server" CommandName="img_Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg"
                                            OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CrewNumber"  SortExpression="CrewNumber" HeaderText="Emp. #">
                                    <ItemStyle HorizontalAlign="Left"  Width="60px"/>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Name" SortExpression="CrewName" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CrewName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank" SortExpression="RankName" >
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfd_OnOff" runat="server" Value='<%# Eval("OnOff") %>' />
                                        <asp:Label ID="lblrankname" runat="server" Text='<%# Eval("RankName") %>'></asp:Label>
                                        <asp:Label ID="lblrankid" runat="server" Text='<%# Eval("CurrentRankId") %>' Visible="false" Width="0px"></asp:Label>
                                        <asp:Label ID="lblvesselid" runat="server" Text='<%# Eval("CurrentVesselId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="Lb_CrewID" runat="server" Text='<%# Eval("crewid") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lb_R_ID" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="Lb_ReliverID" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lb_ReliverID1" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="Lb_ReliverRankId" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="Lb_ReliverRankId1" runat="server" Text="" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SignOnDate"  SortExpression="SignOnDate" HeaderText="Sign On Date">
                                    <ItemStyle HorizontalAlign="Left" Width="90px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="ReliefDueDate"  SortExpression="ReliefDueDate" HeaderText="Relief Due Date">
                                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="ExpectedJoinDate"  SortExpression="ExpectedJoinDate" HeaderText="Exp. Join Date">
                                    <ItemStyle HorizontalAlign="Left" Width="90px"  />
                                </asp:BoundField>
                               </Columns>
                            <RowStyle CssClass="rowstyle" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                        </asp:GridView>
                    </div>
    </td>
                    </tr>
                    </table>
    </td>
</tr>
</table>
</div>