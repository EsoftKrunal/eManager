<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="SignOn.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="CrewOperation_SignOn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript">
function openpage()
{
var url = "../Registers/PopUp_Port.aspx"
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
return false;
}
</script>

     <script language="javascript">
         function openpageManningAgent() {
             var url = "../Registers/ManningAgent.aspx"
             window.open(url, null, 'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
             return false;
         }
     </script>
        <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div>
<asp:Label runat="server" id="lbl_signon_message" Text="" ForeColor="Red" ></asp:Label>
<table cellpadding="0" cellspacing="0" width="100%"  border="1">
<tr>
<td >
 <div style="width:100%; overflow-y:scroll; overflow-x:hidden; height:180px">
<asp:GridView ID="GridView1"  runat="server" AllowSorting="true" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%" OnSorting="GridView1_Sorting" OnRowCommand="GridView1_RowCommand" >
             <Columns>
                 <asp:TemplateField HeaderText="Emp.#" SortExpression="CrewNumber" >
                 <ItemTemplate>
                    <asp:LinkButton CausesValidation="false" ID="lnk_Crew" Text='<%#Eval("CrewNumber")%>' runat="server" ></asp:LinkButton>
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
<td style=" border:solid 1px gray;background-color : #e2e2e2"  >
   <table cellpadding="0" cellspacing="0" 
        style="width: 100%; padding-right: 5px; height: 50px;">
       <tr>
           <td style="text-align: right; ">
               Employee #:</td>
           <td style="text-align: left; width: 149px; ">
               <asp:Label ID="lbl_EmpNo" runat="server" Width="160px"></asp:Label></td>
           <td style="text-align: right; ">
               Name:</td>
           <td style="text-align: left; width: 192px; ">
               <asp:Label ID="lbl_Name" runat="server" Width="180px"></asp:Label></td>
           <td style="text-align: right; ">
               Signed Off:</td>
           <td style="text-align: left; ">
               <asp:Label ID="lbl_SignedOff" runat="server" Text="" Width="100px"></asp:Label>&nbsp;</td>
       </tr>
       <tr>
           <td style="text-align: right; ">
               Last Vessel:</td>
           <td style="text-align: left; width: 149px; ">
               <asp:Label ID="lbl_LastVessel" runat="server" Width="160px"></asp:Label></td>
           <td style="text-align: right; ">
               Pay Rank:</td>
           <td style="text-align: left; width: 192px; ">
               <asp:Label ID="lbl_Rank" runat="server" Width="180px"></asp:Label></td>
           <td style="">
           </td>
           <td style="">
           </td>
       </tr>
       <tr>
           <td style="text-align: right;">
               Relievee's Name:</td>
           <td style="text-align: left; width: 149px;">
               <asp:Label ID="lbl_RelieveesName" runat="server" Width="160px"></asp:Label></td>
           <td style="text-align: right;">
               Current
               Vessel:</td>
           <td style="text-align: left; width: 192px;">
               <asp:Label ID="lbl_Vessel" runat="server" Width="180px"></asp:Label></td>
           <td>
           </td>
           <td>
               <asp:Label ID="lblportcallid" runat="server" Visible="False" Width="1px"></asp:Label></td>
       </tr>
   </table>
   </td>
   </tr>
   <tr>
   <td>
   <table cellpadding="0" cellspacing="0" style="width: 100%; padding-right: 5px; height : 160px;">
       <tr>
           <td style="text-align: right; " colspan="5">
               &nbsp;&nbsp;</td>
       </tr>
       <tr>
           <td style="text-align: right; width: 140px;">
               Sign On as:</td>
           <td style="text-align: left">
               <asp:DropDownList ID="ddl_SignOnas" runat="server" CssClass="required_box" Width="200px">
               </asp:DropDownList>
               <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddl_SignOnas"
                   ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
           <td style="text-align: right; width: 140px;">
               Sign On Date:</td>
           <td style="text-align: left; width: 245px;">
               <asp:TextBox ID="txt_SignOnDate" OnTextChanged="txt_Duration_TextChanged" AutoPostBack="True" runat="server" CssClass="required_box" Width="80px"></asp:TextBox>
               <asp:ImageButton
                   ID="imgsignonas" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_SignOnDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txt_SignOnDate" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
           
               </td>
           <td style="text-align: right; width: 88px;">
               &nbsp;</td>
       </tr>
       <tr>
           <td style="text-align: right; width: 140px;">
               Country:</td>
           <td style="text-align: left">
               <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="required_box"
                   OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="200px">
               </asp:DropDownList></td>
           <td style="text-align: right; width: 140px;">
               Port:</td>
           <td style="text-align: left; width: 245px;">
               <asp:DropDownList ID="ddl_Port" runat="server" CssClass="required_box" Width="163px">
               </asp:DropDownList>
               <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                   OnClientClick="return openpage();" /></td>
           <td style="text-align: right; width: 88px;">
               &nbsp;</td>
       </tr>
       <tr>
           <td style="text-align: right; height: 19px; width: 140px;">
               Duration(Months):</td>
           <td style="text-align: left; height: 19px;">
               <asp:TextBox ID="txt_Duration" runat="server" CssClass="input_box" Width="82px" MaxLength="2" OnTextChanged="txt_Duration_TextChanged" AutoPostBack="True" ReadOnly="True"></asp:TextBox>
                                                                </td>
           <td style="text-align: right; height: 19px; width: 140px;">
               Relief Due:</td>
           <td style="text-align: left; height: 19px; width: 245px;">
               <asp:TextBox ID="txt_ReliefDate" runat="server" CssClass="input_box" Width="82px" ReadOnly="True"></asp:TextBox>
               <asp:ImageButton ID="imgSignOffDate" Visible ="false" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
               
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_ReliefDate" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_ReliefDate" ErrorMessage="Required." Enabled="false" ></asp:RequiredFieldValidator>  
                                  
               </td>
           <td style="text-align: right; height: 19px; width: 88px;">
                                                                </td>
       </tr>
       <tr>
           <td style="text-align: right; width: 140px;" valign="top">
               Remarks:</td>
           <td style="text-align: left">
               <asp:TextBox ID="txt_Remarks" runat="server" CssClass="input_box" 
                   TextMode="MultiLine" MaxLength="99" Height="46px" Width="757px">0</asp:TextBox></td>
           <td style="text-align: right; height: 19px; width: 140px;">
               Manning Agent:
               </td>
           <td>
                 <asp:DropDownList ID="ddl_ManningAgent" runat="server" CssClass="required_box" Width="163px">
                   <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
               </asp:DropDownList>
                 <asp:ImageButton ID="imgAddManningAgent" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                   OnClientClick="return openpageManningAgent();" />
               <asp:CompareValidator ID="cvddl_ManningAgent" runat="server" ControlToValidate="ddl_ManningAgent"
                   ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                 <asp:HiddenField ID="hdnManningAgent" runat="server" ></asp:HiddenField>
           </td>
           <td>
                
           </td>
       </tr>
       <tr>
           <td style="text-align: right; width: 140px;" valign="top">
               Updated By:</td>
                <td style="text-align: left" >
               <asp:Label ID="lbl_UpdatedBy" runat="server" Width="160px"></asp:Label>
               </td>
               <td style=" text-align :right " >Updated On:</td>
               <td style="text-align:left" >
               <asp:Label ID="lbl_UpdatedOn" runat="server" Width="160px" style="text-align: left"></asp:Label></td>
           <td style="text-align: right; width: 88px;" valign="top">
<asp:Button ID="btn_save" runat="server" Text="Save" CssClass="btn" Width="70px" 
                   OnClick="btn_save_Click" />
           </td>
       </tr>
       </table>
</td>
</tr>
</table>    
<asp:HiddenField ID="HiddenFamilyMember" runat="server" />
<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgsignonas" PopupPosition="TopRight" TargetControlID="txt_SignOnDate"></ajaxToolkit:CalendarExtender>
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="imgSignOffDate" PopupPosition="TopRight"  TargetControlID="txt_ReliefDate"> </ajaxToolkit:CalendarExtender>
<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderDuration" runat="server" FilterType="Numbers" TargetControlID="txt_Duration"> </ajaxToolkit:FilteredTextBoxExtender> 
<asp:Label ID="lblcrewid" runat="server" Visible="False" Width="118px" style="display:none"></asp:Label>
</div>
    </asp:Content>

    