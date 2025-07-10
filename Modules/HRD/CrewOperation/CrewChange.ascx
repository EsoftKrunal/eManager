<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CrewChange.ascx.cs" Inherits="CrewOperation_CrewChange" %>
  <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script language="javascript">
function openpage()
{
var url = "../Registers/PopUp_Port.aspx"
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=420,height=170,left=50,top=50,addressbar=no');
return false;
}
</script>
<div style="width:100%" >
<asp:Label ID="Message" runat="server" ForeColor="Red" Width="100%" ></asp:Label>
<table cellpadding="0" cellspacing="0" border="1" style="width :100%;font-family:Arial;font-size:12px;" >
    <tr>
        <td style="background-color :#e2e2e2;"  >
            <table cellpadding="0" cellspacing="0" style="width:100%">
                    <tr><td>Vessel: &nbsp;</td>
                    <td style="text-align: left; ">
                    <asp:DropDownList ID="ddl_VesselName" Width="176px" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_VesselName_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style=" text-align: right;"></td>
                        <td style=" text-align: left;"><asp:RangeValidator ID="RangeValidator2" Enabled="false" runat="server" ControlToValidate="ddl_VesselName" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td>Country: &nbsp;</td>
                        <td style=" text-align: left;"><asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="176px"> </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style=" text-align: right;">&nbsp;</td>
                        <td style=" text-align: left;"><asp:RangeValidator Enabled="false" ID="RangeValidator3" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Required." MaximumValue="10000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td style=" text-align: right;">Port:&nbsp;
                        </td>
                        <td style=" text-align: left;"><asp:DropDownList ID="ddl_port" Width="176px" runat="server" CssClass="required_box" AutoPostBack="True" OnSelectedIndexChanged="ddl_port_SelectedIndexChanged"></asp:DropDownList><asp:ImageButton ID="imgaddport" Visible ="false"  runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif" OnClientClick="return openpage();" /></td>
                    </tr>
                    <tr>
                        <td style=" text-align: right;">&nbsp;</td>
                        <td style=" text-align: left;">
                            <asp:RangeValidator Enabled="false" ID="RangeValidator1" runat="server" ControlToValidate="ddl_port" ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td style=" text-align: right;">
                            Emp.#:</td>
                        <td style=" text-align: left;">
                            <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="input_box" MaxLength="6" Width="45px" OnTextChanged="txt_EmpNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                    </tr>
                    <tr style="display : none" >
                        <td style=" text-align: right;">
                        </td>
                        <td style=" text-align: right;">
                            <asp:Button ID="btn_Show" runat="server" CssClass="btn" OnClick="btn_Show_Click"
                                Text="Show" Width="50px" />
                            <asp:Button ID="btn_Add" runat="server" CssClass="btn" OnClick="btn_Add_Click" Text="Add"
                                Width="50px" /></td>
                    </tr>
                </table>
        </td>
        <td style="width :780px"  >
            <asp:Panel ID="panel_portref" runat="server" Width="100%">
                <div style=" overflow-x:hidden; overflow-y: scroll; height: 150px; width: 100%">
                    <asp:GridView ID="GvRefno" runat="server"  AllowSorting="True" OnSorted="on_Sorted" OnSorting="on_Sorting" AutoGenerateColumns="False" OnSelectedIndexChanged="GvRefno_SelectIndexChanged" 
                        GridLines="Horizontal" OnRowCommand="GvRefno_RowCommand" Style="text-align: center"
                        Width="98%" OnRowEditing="GvRefno_RowEditing" OnRowDeleting="GvRefno_Row_Deleting" OnPreRender="GvRefno_prerender">
                        <Columns>
                          <asp:TemplateField HeaderText="Cancel" ShowHeader="False"><ItemStyle Width="40px" />
                            <ItemTemplate>
                             <asp:ImageButton ID="ImageButtonCancel" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                            </ItemTemplate>
                          </asp:TemplateField>
                               <asp:TemplateField HeaderText="View" Visible="False"  >
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" OnClientClick="document.location='dummy.aspx';" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" ID="Assign" />
                                </ItemTemplate>
                                </asp:TemplateField>
                        
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True" Visible="False">
                                    <ItemStyle Width="40px" />
                                </asp:CommandField>
                                
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="False">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                            Text="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            <asp:TemplateField HeaderText="Port Reference #" SortExpression="PortReferenceNumber" >
                                   <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                               <asp:LinkButton ID="btnrefno" runat="server" Text='<%#Eval("PortReferenceNumber") %>'  Font-Underline="false" CommandName="Select"></asp:LinkButton>  
                                               <asp:HiddenField ID="HiddenPortCallId" runat="server" Value='<%#Eval("PortCallId")%>' />
                                            </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:BoundField DataField="Status" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                            </asp:BoundField>
                           </Columns>
                        <RowStyle CssClass="rowstyle" />
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                    </asp:GridView>
                </div>
                </asp:Panel>        
        </td>
    </tr>
    <tr>
        <td colspan="2" style=" background-color:#e2e2e2" >
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td bgcolor="#C9F4DA" style="border-right: black 1px solid; border-top: black 1px solid;
                        border-left: black 1px solid; width: 31px; border-bottom: black 1px solid">
                        &nbsp;
                    </td>
                    <td style="text-align: left">
                        &nbsp;Sign On</td>
                    <td bgcolor="lightyellow" style="border-right: black 1px solid; border-top: black 1px solid;
                        border-left: black 1px solid; width: 33px; border-bottom: black 1px solid">
                        &nbsp;
                    </td>
                    <td style="text-align: left" width="600">
                        &nbsp;Sign Off</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
         <asp:Panel ID="panel_SignOff" runat="server" Width="100%" Visible="false">
                <div style=" overflow-x:hidden; overflow-y: scroll; height: 175px; width: 100%">
                    <asp:GridView ID="gvsearch" runat="server" AutoGenerateColumns="False" DataKeyNames="CrewId"  AllowSorting="True" OnSorted="on_Sorted1" OnSorting="on_Sorting1" 
                        GridLines="Horizontal" OnRowDeleting="gvsearch_Row_Deleting" OnRowCommand="gvsearch_RowCommand" Style="text-align: center"
                        Width="97%" OnRowDataBound="gvsearch_RowDataBound1">
                        <Columns>
                        
                                <asp:TemplateField HeaderText="Delete">
                                <ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                            <asp:CheckBox ID="chkselect" AutoPostBack="true" OnCheckedChanged="checked_Change" value='<%#Eval("CrewId")%>' runat="server" />
                            </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Emp #" SortExpression="CrewNumber" >
                                   <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                               <asp:LinkButton ID="btncrewnosignoff" runat="server" Text='<%#Eval("CrewNumber") %>'  Font-Underline="false" CommandName="Select"></asp:LinkButton>  
                                               <asp:HiddenField ID="HiddencrewIdsignoff" runat="server" Value='<%#Eval("CrewId")%>' />
                                               <asp:HiddenField ID="HfdCrewFlag" runat="server" Value='<%#Eval("CrewFlag")%>' />
                                            </ItemTemplate>
                                      </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="CrewName">
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyNamesignoff" runat="server" Text='<%# Eval("CrewName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" SortExpression="RankName">
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblranknamesignoff" runat="server" Text='<%# Eval("RankName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SignOnDate" HeaderText="Sign On Date" SortExpression="SignOnDate">
                                <ItemStyle HorizontalAlign="Left" Width="90px"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="ReliefDueDate" HeaderText="Relief Due Date" SortExpression="ReliefDueDate">
                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ExpectedJoinDate" HeaderText="Exp.Join Date" SortExpression="ExpectedJoinDate">
                                <ItemStyle HorizontalAlign="Left" Width="110px" />
                            </asp:BoundField>
                            
                           </Columns>
                        <RowStyle CssClass="rowstyle" />
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                    </asp:GridView>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
</div>

