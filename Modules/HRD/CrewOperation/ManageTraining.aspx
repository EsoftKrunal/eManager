<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageTraining.aspx.cs" Inherits="ManageTraining" MasterPageFile="~/Modules/HRD/CrewPlanning.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <%-- <table cellpadding="10" cellspacing="0" style="width: 100%">
            <tr>
                <td class="textregisters" colspan="2" style="padding-right: 0px; padding-left: 0px;
                    padding-bottom: 0px; padding-top: 0px;text-align: center;">
                    <strong>Manage Training</strong></td>
            </tr>
               <tr>
                <td style="text-align: left; padding-bottom: 0px; padding-top: 0px;">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid">
                            <legend><strong>Training</strong></legend>
                            <div style="overflow-y: scroll; overflow-x: hidden; width: 780px; height: 150px; padding-left: 8px">
                                <asp:GridView ID="gv_Training1" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    Style="text-a+lign: center" Width="100%">
                                    <Columns>                                       
                                        <asp:BoundField DataField="Training"  HeaderStyle-Width="100" HeaderText="Training">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Batch" HeaderText="Batch#">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Total#">
                                            <ItemStyle HorizontalAlign="Left"/>
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle CssClass="rowstyle" />
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <HeaderStyle CssClass="headerstyle" ForeColor="#0E64A0" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </td>
            </tr>
            <tr><td style=" padding-bottom:0px; padding-top:0px">&nbsp;</td></tr>
            <tr>
                <td style="text-align: left; padding-bottom: 0px; padding-top: 0px;">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                            padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid">
                            <legend><strong>Training</strong></legend>
                         
                            <div style="overflow-y: scroll; overflow-x: scroll; width: 780px; height: 150px; padding-left: 8px">
                                <asp:GridView ID="gv_Training2" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                    Style="text-align: center" Width="120%">
                                    <Columns>
                                        <%--<asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                            ShowSelectButton="True">
                                            <ItemStyle Width="35px" />
                                        </asp:CommandField>
                                       
                                        <asp:BoundField DataField="Empno"  HeaderStyle-Width="100" HeaderText="Emp.#">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Rank" HeaderText="Rank">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FD" HeaderText="From Date">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TD" HeaderText="To Date">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Duration" HeaderText="Duration">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PHC"  HeaderStyle-Width="150" HeaderText="Per Head Cost">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Attended">
                                        <ItemTemplate>
                                        <asp:CheckBox id="CheckBox1" runat="server" />
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                         </Columns>
                                    <RowStyle CssClass="rowstyle" />
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <HeaderStyle CssClass="headerstyle" ForeColor="#0E64A0" />
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </td>
            </tr>
            <tr>
                <td style="text-align: right;  padding-bottom:10px;">
                    <asp:Button ID="btnAddInvoice" runat="server" CssClass="btn" OnClick="btnAddInvoice_Click"
                        TabIndex="7" Text="Save" Width="65px" /></td>
            </tr>
        </table>--%>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
       <table  cellpadding="0" cellspacing="0" width="100%">
       <tr>
                <td class="textregisters" colspan="7" style="padding-right: 0px; padding-left: 0px;
                    padding-bottom: 0px; padding-top: 0px;text-align: center;">
                    <strong>Manage Training</strong></td>
            </tr>
        <tr>
            <td colspan="7">
                <asp:Label ID="lb_msg" runat="server" ForeColor="Red" Width="362px"></asp:Label>
                </td>
        </tr>
        <tr>
            <td colspan="7" style="text-align: left; padding-left:12px">
             <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; width: 98%;">
                <div style="overflow-y:scroll;overflow-x:hidden; width: 100%; height: 125px; text-align: left;">
                             <asp:GridView RowStyle-HorizontalAlign="left" ID="gvbatch" OnRowCreated="gvsearch_RowCreated" OnRowCommand="gvsearch_RowCommand" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Height="32px" Style="text-align: center" Width="100%">
                        <Columns>
                        <asp:TemplateField HeaderText="Training Name">
                        <ItemTemplate>
                         <asp:Label ID="lb_id" Visible="false"  runat="server" Text='<%# Eval("TrainingId") %>'></asp:Label>
               <asp:Label ID="Lb_name" runat="server" Text='<%# Eval("TrainingName") %>'></asp:Label>
                                  
                        </ItemTemplate>
                            <HeaderStyle Width="150px" />                     
                        </asp:TemplateField>
                        <asp:BoundField DataField="fdate1" HeaderText="From Date">
                          <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                        <asp:BoundField DataField="tdate1" HeaderText="To Date">
                         <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                         <asp:TemplateField HeaderText="Batches">
                        <ItemTemplate>
                        <asp:Label ID="Lbplan" Visible="false" runat="server" Text='<%# Eval("TrainingPlanningId") %>'></asp:Label>
                        <asp:Label ID="Lb_btch" Visible="false" runat="server" Text='<%# Eval("BatchNo1") %>'></asp:Label>
                        <asp:LinkButton ID="lnk_batch" CausesValidation="false" CommandName="lnk" runat="Server" Text='<%# Eval("BatchNo") %>'></asp:LinkButton>
                                  
                        </ItemTemplate>
                             <HeaderStyle Width="100px" />
                                              
                        </asp:TemplateField>
                        <asp:BoundField DataField="emp" HeaderText="No.of Emp">
                         <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                         </Columns>
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                          <HeaderStyle CssClass="headerstylefixedheadergrid" />
                    </asp:GridView>
                            
                </div>
                </fieldset> 
            </td>
        </tr>
           <tr>
               <td colspan="7" style="text-align: left">
                   &nbsp;&nbsp;
               </td>
           </tr>
        <tr>
            <td colspan="7" style="text-align: left; padding-left:12px">
             <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; width: 98%">
           <div style="overflow-y:scroll;overflow-x:hidden; width: 100%; height: 125px; text-align: left;">
            <asp:GridView RowStyle-HorizontalAlign="left" ID="GdCrew" OnRowDataBound="gdCrew_Rowbound"  runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Height="32px" Style="text-align: center" Width="100%" OnPreRender="GdCrew_PreRender">
                <Columns>
                    <asp:TemplateField HeaderText="Crew Name">
                        <ItemTemplate>
                            <asp:Label ID="lb_TrainingRequirementID" runat="server" Text='<%# Eval("TrainingRequirementID") %>' Visible="false"></asp:Label>
                           <asp:Label ID="Lbcrewid" runat="server" Text='<%# Eval("CrewID") %>' Visible="false"></asp:Label>
                            <asp:Label ID="Lb_crewname" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                              <asp:Label ID="Lb_attended" runat="server" Text='<%# Eval("Attended") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CrewNumber" HeaderText="Emp.#">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                        <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        
                     <asp:TemplateField HeaderText="Rank">
                        <ItemTemplate>
                            <asp:Label ID="Lbrank" runat="server" Text='<%# Eval("Rank") %>'></asp:Label>
                            
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From Date">
                        <ItemTemplate>
                        <asp:TextBox ID="txt_from" runat="server" CssClass="required_box" MaxLength="10"  Text='<%# Eval("Fromdate") %>'  Width="102px"></asp:TextBox>
                            <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                        <asp:CompareValidator ID="CompareValidator21" runat="server" ControlToValidate="txt_from"
                    Display="Dynamic" ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date"
                    ValueToCompare="0"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" runat="server" ControlToValidate="txt_from"
                    ErrorMessage="Required." Width="59px"></asp:RequiredFieldValidator>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender22" runat="server" Format="MM/dd/yyyy"
        PopupButtonID="imgfrom"  PopupPosition="TopRight" TargetControlID="txt_from">
                            </ajaxToolkit:CalendarExtender>
                      
      <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="false"
        ClearMaskOnLostFocus="true" ClearTextOnInvalid="true" Mask="99/99/9999" MaskType="Date"
        TargetControlID="txt_from">
    </ajaxToolkit:MaskedEditExtender>
                        </ItemTemplate>
                        <HeaderStyle Width="160px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="To Date">
                        <ItemTemplate>
                        <asp:TextBox ID="txt_to" runat="server" CssClass="required_box" Text='<%# Eval("Todate") %>' MaxLength="10"  Width="102px">
                        </asp:TextBox>
                            <asp:ImageButton ID="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                         <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_to"
                    Display="Dynamic" ErrorMessage="Invalid Date" Operator="DataTypeCheck" Type="Date"
                    ValueToCompare="0"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" Display="Dynamic" runat="server" ControlToValidate="txt_to"
                    ErrorMessage="Required." Width="59px"></asp:RequiredFieldValidator>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy"
        PopupButtonID="imgto"  PopupPosition="TopRight" TargetControlID="txt_to">
    </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AutoComplete="false"
        ClearMaskOnLostFocus="true" ClearTextOnInvalid="true" Mask="99/99/9999" MaskType="Date"
        TargetControlID="txt_to">
    </ajaxToolkit:MaskedEditExtender>
                        </ItemTemplate>
                        <HeaderStyle Width="160px" />
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="Per Head Cost" >
                        <ItemTemplate>
                            <asp:TextBox  ID="txtperhead"  CssClass="required_box" Width="100" runat="server" Text='<%# Eval("PerHeadCost") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Width="147px" ErrorMessage="Till 2 decimal places only." ControlToValidate="txtperhead" Display="Dynamic" ValidationExpression="\b\d{1,13}\.?\d{0,2}">
                            </asp:RegularExpressionValidator>
                            <asp:CompareValidator id="CompareValidator22" runat="server" Type="Double" ErrorMessage="Greater than 0." ControlToValidate="txtperhead" Display="Dynamic" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Required." ControlToValidate="txtperhead" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Numbers,Custom" TargetControlID="txtperhead" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                        </ItemTemplate>
                        <HeaderStyle Width="130px" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Attended">
                        <ItemTemplate>
                           <asp:CheckBox ID="chkattended" runat="Server" />
                        </ItemTemplate>
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <RowStyle CssClass="rowstyle" HorizontalAlign="Left" />
                  <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
            </div> 
            </fieldset> 
            </td>
        </tr>
           <tr>
               <td align="right" colspan="7">
                   &nbsp; &nbsp;
               </td>
           </tr>
        <tr>
            <td colspan="7" align="right" style="padding-right:15px">
                <asp:Button ID="btnvisasave" runat="server" CssClass="btn" TabIndex="55" Text="Save" Width="59px" OnClick="btnvisasave_Click" /></td>
        </tr>
           <tr>
               <td align="right" colspan="7">
                   &nbsp; &nbsp;
               </td>
           </tr>
    </table>
    
    
            </asp:Content>
