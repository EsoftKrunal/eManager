<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SignOff.aspx.cs" Inherits="CrewOperation_SignOff" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript">
function openpage()
{
var url = "../Registers/PopUp_Port.aspx"
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
return false;
}
</script>
        <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div>
<asp:Label ID="lb_msg" runat="server" ForeColor="Red"></asp:Label>
<table cellspacing="0" width="100%" style=" background-color:#f9f9f9" cellpadding="0" border="1">
<tr>
<td colspan="2" >
                <asp:Label ID="lb_crwid" Visible="false" runat="server"></asp:Label>
                <div style="width:100%; overflow-y:scroll; overflow-x:hidden; height:175px">
                 <asp:GridView ID="GridView1" OnSorted="on_Sorted" OnSorting="on_Sorting" AllowSorting="true" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%" OnRowCommand="GridView1_RowCommand">
                     <Columns>
                         <asp:TemplateField HeaderText="Emp.#" SortExpression="CrewNumber" >
                         <ItemTemplate>
                            <asp:LinkButton CausesValidation="false" ID="lnk_Crew" Text='<%#Eval("CrewNumber")%>' runat="server"></asp:LinkButton>
                            <asp:HiddenField ID="hfd_Crew" Value='<%#Eval("CrewID")%>' runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hfd_PortCallID" Value='<%#Eval("PortCallID")%>' runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hfd_Po" Value='<%#Eval("isPO")%>' runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hfd_Country" Value='<%#Eval("Country")%>' runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hfd_Port" Value='<%#Eval("Port")%>' runat="server"></asp:HiddenField>
                         </ItemTemplate>
                             <ItemStyle Width="50px" />
                         </asp:TemplateField>
                         <asp:BoundField DataField="Name" SortExpression="Name" HeaderText="Name" ><ItemStyle HorizontalAlign="Left" /></asp:BoundField>
                         <asp:BoundField DataField="RankCode" SortExpression="RankCode" HeaderText="Rank" ><ItemStyle Width="100px" /></asp:BoundField>
                         <asp:BoundField DataField="Vessel" SortExpression="Vessel" HeaderText="VSL" ><ItemStyle Width="50px" /></asp:BoundField>
                         <asp:BoundField DataField="PortCallNo" SortExpression="PortCallNo" HeaderText="PortCallNo"><ItemStyle Width="250px" /></asp:BoundField>
                     </Columns>
                     <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                     <SelectedRowStyle CssClass="selectedtowstyle" />
                     <HeaderStyle CssClass="headerstylefixedheadergrid" />
                 </asp:GridView>
                 </div>
            </td>
</tr>
<tr>
<td colspan="2" style="width: 50%; height: 16px; text-align: center; background-color : #e2e2e2">
<table cellpadding="0" cellspacing="0" style="width: 94%; height: 35px;">
                    <TR><TD style="WIDTH: 92px; HEIGHT: 2px" 
align=right>
                        &nbsp;</td>
                        <TD style="WIDTH: 172px; HEIGHT: 2px; text-align :right " >
    Employee #:</td>
    <TD 
style="WIDTH: 125px; TEXT-ALIGN: left">
        &nbsp;<asp:Label ID="lb_empno" runat="server" meta:resourcekey="Label24Resource1" Width="108px"></asp:Label></td>
    <TD 
style="WIDTH: 130px; TEXT-ALIGN: right">
        Name:</td>
    <TD 
style="TEXT-ALIGN: left; width: 178px;">
        &nbsp;<asp:Label ID="lb_name" runat="server" meta:resourcekey="Label29Resource1" Width="80%"></asp:Label></td>
    <td style="width: 199px; text-align: right">
            Signed On:&nbsp;
    </td>
                       <td style="width: 199px; text-align: left">
                           <asp:Label ID="lb_signon" runat="server" meta:resourcekey="Label24Resource1" Width="108px"></asp:Label></td>
                       <td style="width: 76px; text-align: left">
                           &nbsp;</td>
</tr>
    <TR>
        <TD style="WIDTH: 92px; HEIGHT: 7px;text-align :right" >
            &nbsp;</td>
        <TD style="WIDTH: 172px; HEIGHT: 7px;text-align :right " >
            Rank:</td>
        <TD 
style="WIDTH: 125px; HEIGHT: 7px; TEXT-ALIGN: left">
            &nbsp;<asp:Label ID="lb_Rank" runat="server" meta:resourcekey="Label24Resource1" Width="108px"></asp:Label></td>
        <TD 
style="WIDTH: 130px; HEIGHT: 7px; TEXT-ALIGN: right">
            Vessel:</td>
        <TD 
style="HEIGHT: 7px; TEXT-ALIGN: left; width: 178px;">
            &nbsp;<asp:Label ID="lb_vessel" runat="server" meta:resourcekey="Label29Resource1"
                Width="80%"></asp:Label></td>
        <td style="width: 199px; height: 7px; text-align: right">
            Relief Due:&nbsp;
        </td>
        <td style="width: 199px; height: 7px; text-align: left">
            <asp:Label ID="lb_relief" runat="server" meta:resourcekey="Label24Resource1" Width="108px"></asp:Label></td>
        <td style="width: 76px; height: 7px; text-align: left">
            &nbsp;</td>
    </tr>
                </table>
                </td>
            </tr>
            <tr>
                <td style="width:50%; text-align:center" colspan="2" >
                    <TABLE style="TEXT-ALIGN: right" cellSpacing=0 cellPadding=0 width="100%" border=0><TBODY>
 
    <tr>
        <td style="text-align :right; width: 50px;" >&nbsp;</td>
        <td style="text-align :right; " >Country:&nbsp;&nbsp; </td>
        <td style="text-align: left; width: 310px;padding-top:2px;">
            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="180px"></asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
        <td style="text-align: right; width: 63px;">Remarks:&nbsp;&nbsp; </td>
        <td style="vertical-align: top; width: 53px;" rowspan="7">
            <asp:TextBox ID="txt_remark" runat="server" CssClass="input_box" Height="153px"
                TextMode="MultiLine" Width="352px" MaxLength="250"></asp:TextBox></td>
        <td style="text-align: left; width: 106px;">&nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; width: 50px;">&nbsp;</td>
        <td style="text-align :right ">Port:&nbsp;&nbsp; </td>
        <td style="text-align: left; width: 310px;">
            <asp:DropDownList ID="dp_port" runat="server" CssClass="required_box" Width="156px">
            </asp:DropDownList>
            <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                OnClientClick="return openpage();" />
            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="dp_port"
                ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
        <td style="width: 63px; text-align: right; height: 20px;">
            &nbsp;</td>
        <td style="width: 106px; text-align: left; height: 20px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; width: 50px;">&nbsp;</td>
        <td style="text-align :right ">Signoff Reason:</td>
        <td style="text-align: left; width: 310px;">
            <asp:DropDownList ID="dp_signreason" runat="server" CssClass="required_box" Width="200px"></asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="dp_signreason" ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
        <td style="width: 63px; text-align: right; height: 20px;">
            &nbsp;</td>
        <td style="width: 106px; text-align: left; height: 20px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; width: 50px;">&nbsp;</td>
        <td style="text-align :right ">Signoff Date:</td>
        <td style="text-align: left; width: 310px;">
            <asp:TextBox ID="txt_signoffdt" CausesValidation="false" runat="server" CssClass="required_box" Width="102px" AutoPostBack="true" OnTextChanged="txt_signoffdt_TextChanged" MaxLength="15"></asp:TextBox>
            <asp:ImageButton ID="img1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
        
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_signoffdt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_signoffdt" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
        
        </td>
        <td style="width: 63px; text-align: right; height: 20px; vertical: ;">
            &nbsp;</td>
        <td style="width: 106px; text-align: left; height: 20px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; width: 50px;">&nbsp;</td>
        <td style="text-align :right ">Leave Period:</td>
        <td style="text-align: left; width: 310px;">
            <asp:Label ID="lb_lvstart" runat="server" ></asp:Label>-<asp:Label ID="lb_lvcomplition" runat="server" ></asp:Label></td>
        <td style="width: 63px; text-align: right; height: 20px;">
            &nbsp;</td>
        <td style="width: 106px; text-align: left; height: 20px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; width: 50px;">&nbsp;</td>
        <td style="text-align :right ">Available From:</td>
        <td style="text-align: left; width: 310px;">
            <asp:TextBox ID="txt_DOA" runat="server" CausesValidation="True"
                CssClass="input_box" OnTextChanged="txt_signoffdt_TextChanged" Width="102px" MaxLength="15"></asp:TextBox>
            <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_DOA" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_DOA" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                     
        </td>
        <td style="width: 63px; text-align: right; height: 20px;">
            &nbsp;</td>
        <td style="width: 106px; text-align: left; height: 20px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align :right; width: 50px;">&nbsp;</td>
        <td style="text-align :right ">Reliever:&nbsp;</td>
        <td style="text-align: left; width: 310px;">
            <asp:Label ID="lb_reliever" runat="server" Width="200px"></asp:Label></td>
        <td style="width: 63px; text-align: right; height: 20px;">
            &nbsp;</td>
        <td style="width: 106px; text-align: left; height: 20px;">
            &nbsp;</td>
    </tr>
</TBODY></TABLE>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_DOA">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="img1" PopupPosition="TopRight" TargetControlID="txt_signoffdt">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2" style=" background-color : #e2e2e2; text-align : right" ><asp:Button ID="btnvisasave" runat="server" CssClass="btn" OnClick="btnvisasave_Click" TabIndex="55" Text="Save" Width="59px" Enabled="False" /></td>
            </tr>
        </table>
       
    </div>
   </asp:Content>

