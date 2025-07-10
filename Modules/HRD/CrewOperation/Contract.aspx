<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master" AutoEventWireup="true" CodeFile="Contract.aspx.cs" Inherits="CrewAccounting_Contract" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function Show_Amount() {
            var Gross ='<%Response.Write(lb_Gross.ClientID);%>';
                    var Ded ='<%Response.Write(lb_deduction.ClientID);%>';
                var Net='<%Response.Write(lb_NewEarning.ClientID);%>';
                var Txt='<%Response.Write(txt_Other_Amount.ClientID);%>';

            var Gross_Val = "0" + document.getElementById(Gross).innerHTML;
            var Ded_Val = "0" + document.getElementById(Ded).innerHTML;
            var Amount = "0" + document.getElementById(Txt).value;

            if (isNaN(Gross_Val)) { Gross_Val = "0"; }
            if (isNaN(Ded_Val)) { Ded_Val = "0"; }
            if (isNaN(Amount)) { Amount = "0"; }

            Amount = parseFloat(Gross_Val) - parseFloat(Ded_Val) + parseFloat(Amount);
            Amount = Math.round(Amount * 100) / 100;
            if (isNaN(Amount)) { Amount = "0"; }
            document.getElementById(Net).innerHTML = Amount;
        }
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
     
    
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
 <table cellpadding="0" cellspacing="0" width="100%" border="1" >
<tr>
    <td colspan="2">
    <div style="width:100%; overflow-y:scroll; overflow-x:hidden; height:120px">
         <asp:GridView ID="GridView1"  OnSorting="on_Sorting" AllowSorting="true" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center"
        Width="98%" OnRowCommand="GridView1_RowCommand">
             <Columns>
                 <asp:TemplateField HeaderText="Emp.#" SortExpression="CrewNumber" >
                 <ItemTemplate>
                    <asp:LinkButton CausesValidation="false" ID="lnk_Crew" Text='<%#Eval("CrewNumber")%>' runat="server" ></asp:LinkButton>
                    <asp:HiddenField ID="Hiddenvesselid" Value='<%#Eval("Vid")%>' runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfd_Crew" Value='<%#Eval("CrewID")%>' runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfd_PortCallID" Value='<%#Eval("PortCallID")%>' runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfd_RankId" Value='<%#Eval("RankId")%>' runat="server"></asp:HiddenField>
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
    <td colspan="2">
    <asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
    </td>
</tr>
    <tr>
        <td style=" background-color :#e2e2e2" >
             <table cellpadding="0" cellspacing="0" width="96%">
                 <tr>
                     <td align="right" style="padding-right: 10px;  text-align: right; width: 166px;">Name :</td>
                     <td align="left" style="width: 299px;"><asp:Label ID="lb_name" runat="server" Width="100%"></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="padding-right: 10px;  height: 3px; width: 166px; text-align: right;">Planned Vessel :</td>
                     <td align="left" style="height: 3px; width: 299px;">
                     <asp:Label ID="lb_PlanVessel" runat="server" Width="100%"></asp:Label></td>
                 </tr>
                 <tr>
                     <td align="right" style="padding-right: 10px;  height: 3px; width: 166px; text-align: right;">Planned Rank :</td>
                     <td align="left" style="height: 3px; width: 299px;">
                     <asp:Label ID="Lb_PlanRank" runat="server" Width="100%"></asp:Label></td>
                 </tr>
                 <tr>
                     <td align="right" 
                         style="padding-right: 10px;  height: 3px; width: 166px; text-align: right;">Ref. No. :</td>
                     <td align="left" style="height: 3px; width: 299px;">
                         <asp:Label ID="lbl_RefNo" runat="server" Width="100%"></asp:Label></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" 
                         style="padding-right: 10px;  width: 166px; text-align: right;" valign="top">
                         Contract Period :</td>
                     <td align="left" style="width: 299px">
                    <asp:TextBox ID="Txt_ContractPeriod" runat="server" 
                             CssClass="required_box"                       Width="88px" MaxLength="2"></asp:TextBox>
                         (Month)
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txt_ContractPeriod"
                             Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" 
                         style="padding-right: 10px;  width: 166px; text-align: right;" valign="top">
                         Seniority :</td>
                     <td align="left" style="width: 299px">
                    <asp:TextBox ID="Txt_Seniority" runat="server" CssClass="required_box" MaxLength="2" 
                             Width="88px" ValidationGroup="abc"></asp:TextBox>
                         (Year)
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_Seniority"
                        Display="Dynamic" ErrorMessage="Required." ValidationGroup="abc"></asp:RequiredFieldValidator></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" 
                         style="padding-right: 10px;  width: 166px; text-align: right;" valign="top">
                         Issue Date :</td>
                     <td align="left" style="width: 299px">
                         <asp:TextBox ID="txt_issuedt" runat="server" CssClass="required_box" MaxLength="10"
                        Width="88px"></asp:TextBox>
                         <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_issuedt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_issuedt" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" 
                         style="padding-right: 10px;  width: 166px; text-align: right;" valign="top">
                         Start Date :</td>
                     <td align="left" style="width: 299px">
                         <asp:TextBox ID="txt_startdt" runat="server" CssClass="required_box" MaxLength="10"
                        Width="88px"></asp:TextBox>
                         <asp:ImageButton ID="img2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_startdt" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txt_startdt" ErrorMessage="Required." ></asp:RequiredFieldValidator>  
                        </td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 8px; text-align: right;" 
                         valign="top">
                         Rank :</td>
                     <td align="left" style="width: 299px; height: 8px">
                         <asp:DropDownList ID="dp_Rank" runat="server" CssClass="required_box" 
                             ValidationGroup="abc" Width="140px">
                         </asp:DropDownList>
                         <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dp_Rank"
                             ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"
                             ValidationGroup="abc"></asp:RangeValidator>
                     </td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 13px; text-align: right;" 
                         valign="top">
                         Other Amount(M) :</td>
                     <td align="left" style="width: 299px; height: 13px;">
                         <asp:TextBox ID="txt_Other_Amount" onkeyup="Show_Amount();" 
                             CssClass="input_box" width="88px" runat="server"></asp:TextBox></td>
                 </tr>
                  <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 13px; text-align: right;" 
                         valign="top">Extra OT Rate (Hourly) :</td>
                     <td align="left" style="width: 299px; height: 13px;">
                         <asp:TextBox ID="txtExtraOTRate"  
                             CssClass="input_box" width="88px" runat="server"></asp:TextBox></td>
                 </tr>
                  <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 13px; text-align: right;" 
                         valign="top">Travel Pay (Per Day) :</td>
                     <td align="left" style="width: 299px; height: 13px;">
                        <asp:DropDownList ID="ddlTravelPay" runat="server" Width="150px">
                                        <asp:ListItem Selected="True" Text="< Select >" Value="0"></asp:ListItem>
                                        <asp:ListItem  Text="Full Salary Basis" Value="1"></asp:ListItem>
                                        <asp:ListItem  Text="Basic Wages Basis" Value="2"></asp:ListItem>
                                        <asp:ListItem  Text="Custom (Wages per day)" Value="3"></asp:ListItem>
                                                                     </asp:DropDownList></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 13px; text-align: right;" 
                         valign="top">
                         Nationality :</td>
                     <td align="left" style="width: 299px; height: 13px;">
                    <asp:DropDownList ID="dp_nationality" runat="server" CssClass="required_box" 
                             ValidationGroup="abc" Width="140px">
                    </asp:DropDownList>
                         <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="dp_nationality"
                             ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"
                             ValidationGroup="abc"></asp:RangeValidator></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 13px; text-align: right;" 
                         valign="top">
                    Wage Scale :</td>
                     <td align="left" style="width: 299px; height: 13px;">
                    <asp:DropDownList ID="dp_wagescale" runat="server" CssClass="required_box" 
                             ValidationGroup="abc" Width="140px">
                    </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dp_wagescale"
                             Display="Dynamic" ErrorMessage="Required." ValidationGroup="abc"></asp:RequiredFieldValidator></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 13px;" 
                         valign="top">
                         &nbsp;</td>
                     <td align="left" style="width: 299px; height: 13px;">
                         <asp:CheckBox ID="chk_SupCert" runat="server" Text="Superior Cert. Allow." /></td>
                 </tr>
                 <tr style="color: #0e64a0">
                     <td align="right" style="padding-right: 10px;  width: 166px; height: 13px;" 
                         valign="top">
                         &nbsp;</td>
                     <td align="left" style="width: 299px; height: 13px; text-align: right;">
                <asp:Button ID="btn_ShowWages" runat="server" CssClass="btn" Text="Show Wages" Width="102px" OnClick="btn_ShowWages_Click" ValidationGroup="abc" />
                    &nbsp;<asp:Button ID="btn_cancel" runat="server" CssClass="btn" Text="Cancel" Width="62px"  Visible="False" />
                                                            </td>
                 </tr>
                 </table>
             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                 FilterType="Custom" ValidChars="0123456789." FilterMode="ValidChars" TargetControlID="txt_Other_Amount">
             </ajaxToolkit:FilteredTextBoxExtender>
          </td>
        <td>
             <div style="overflow-y: scroll; overflow-x: hidden; width:100%; height: 150px; text-align: left;">
               <asp:GridView ID="Gd_AssignWages"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center"
                Width="100%">
                 <Columns>
                   <asp:TemplateField HeaderText="Component Type" Visible="false">
                   <ItemTemplate>
                    <asp:HiddenField ID="hfdEorD" runat="server" Value='<%#Eval("ComponentType")%>' />
                   </ItemTemplate>
                   </asp:TemplateField>
                   <asp:BoundField DataField="ComponentName" HeaderText="Component Name"  />               
                   <asp:BoundField DataField="WageScaleComponentValue" ItemStyle-HorizontalAlign="right" HeaderText="Component Value" DataFormatString="{0:0.00}" HtmlEncode="false" />               
                   <asp:BoundField DataField="ComponentType" HeaderText="Component Type" ItemStyle-HorizontalAlign="Center"/>            
                </Columns>
                <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
        </div>
             <table width="100%" cellpadding="5" cellspacing="0">
                    <tr>
                        <td style="width: 290px; text-align: right">
                            &nbsp;</td>
                        <td style="width: 89px; text-align: right;">
                            &nbsp;</td>
                       <td>Remarks:</td>
                    </tr>
                    <tr>
                        <td style="width: 290px; text-align: right">
                            Gross Earnings:</td>
                        <td style="width: 89px; text-align: right;">
                            <asp:Label ID="lb_Gross" runat="server" Width="122px"></asp:Label></td>
                             <td style="width: 189px; text-align: left;" rowspan="3" valign="top">
                            <asp:TextBox ID="txt_Remark" runat="server" CssClass="input_box" TextMode="MultiLine"
                                Width="266px" Height="67px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 290px; text-align: right">
                            Total Deduction:</td>
                      <td style="width: 89px; text-align: right;">
                            <asp:Label ID="lb_deduction" runat="server" Width="122px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 290px; text-align: right; height: 16px;">
                            Net Earnings(+Other Amount):</td>
                        <td style="width: 89px; text-align: right; height: 16px;">
                            <asp:Label ID="lb_NewEarning" runat="server" Width="122px"></asp:Label></td>
                    </tr>
                </table>
          </td>
        </tr>
         <tr>
             <td align="right" style =" background-color :#e2e2e2; text-align :right " colspan="2" ><asp:Button ID="PrintCheck" runat="server" CssClass="btn" Text="Print CheckList" Width="73pt" CausesValidation="false" OnClick="btn_print_CheckList_Click" />
             <asp:Button ID="btn_cancelContract" runat="server" CssClass="btn" Text="Cancel Contract" Width="100px" OnClick="btn_cancelContract_Click" OnClientClick="javascript:return confirm('Are You Sure to Cancel this Contract?')" />
             <asp:Button ID="btn_CheckList" runat="server" CssClass="btn" Text="CheckList" Width="46pt" CausesValidation="false" OnClick="btn_CheckList_Click" />
             <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="46pt" OnClick="btn_Save_Click" />
             <asp:Button ID="btn_ContLetter" runat="server" CssClass="btn" Text="View Contract" Width="100px" OnClick="btn_ContLetter_Click" />
             </td>
         </tr>
         <tr>
            <td colspan="2" >
            <center>
                Emp.# :
                <asp:TextBox ID="txt_EmpNo" runat="server" CssClass="input_box" MaxLength="6" Width="45px"></asp:TextBox>
                <asp:Button ID="btn_Search" CausesValidation="false" runat="server" CssClass="btn" Text="Search" Width="46pt" OnClick="btn_Search_Click" />
            </center>
            <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                 <td>
                     <div style="overflow-y: scroll; overflow-x: hidden; width:100%; height: 150px; text-align: left;">
                         <asp:GridView ID="gv_Contract" OnSelectedIndexChanged="GridView_SelectIndexChanged"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center" Width="98%">
                             <Columns>
                               <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True">
                                   <ItemStyle Width="30px" />
                               </asp:CommandField>
                               <asp:TemplateField HeaderText="Ref.#" >
                               <ItemTemplate>
                               <asp:Label ID="lbl_Contractid" runat="server" Text='<%# Eval("RefNo") %>'></asp:Label>
                               <asp:HiddenField ID="hfd_ContractId" runat="server" Value='<%# Eval("ContractId") %>'></asp:HiddenField>
                               </ItemTemplate>
                                   <ItemStyle Width="70px" />
                               </asp:TemplateField>
                                 <asp:BoundField DataField="Name" HeaderText="Name" />
                                 <asp:BoundField DataField="RankCode" HeaderText="Planned Rank" />
                                  <asp:BoundField DataField="NewRankCode" HeaderText="Rank" />
                                  <asp:BoundField DataField="VesselName" HeaderText="Vessel" />
                                 <asp:BoundField DataField="IssueDate" HeaderText="IssueDate" />
                                 <asp:BoundField DataField="StartDate" HeaderText="StartDate" />
                                 <asp:BoundField DataField="CountryName" HeaderText="Nationality" />
                             </Columns>
                             <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
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
         
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                    PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_issuedt">
                </ajaxToolkit:CalendarExtender>
<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                    PopupButtonID="img2" PopupPosition="TopRight" TargetControlID="txt_startdt">
                </ajaxToolkit:CalendarExtender>

<ajaxToolkit:FilteredTextBoxExtender ID="f2" runat="server" FilterType="Numbers" TargetControlID="Txt_Seniority"></ajaxToolkit:FilteredTextBoxExtender>
<ajaxToolkit:FilteredTextBoxExtender ID="Fl1" runat="server" FilterType="Numbers" TargetControlID="Txt_ContractPeriod"></ajaxToolkit:FilteredTextBoxExtender>
<script language="javascript"  type="text/javascript">
        Show_Amount();
    </script>
</asp:Content>
