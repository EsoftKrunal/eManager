<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" CodeFile="WageScaleComponentMaster.aspx.cs" Inherits="CrewOperation_WageScaleComponentMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <div>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
    <table cellpadding="0" cellspacing="0" width="100%">
     <tr>
      <td align="center" style="height: 14px">
        <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Wage Scale"></asp:Label><br />
          &nbsp;
          </td>
     </tr> 
     <tr>
      <td style="text-align: center">
      <table style="width :100%"  >
      <tr>
      <td style="width :50%" >
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
            padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
            text-align: center;">
            <legend><strong>Wage Scale</strong></legend>
            <div id="div-datagrid" style=" width:100%; height :150px; overflow-y:Scroll; overflow-x:hidden; text-align:center">
                            <asp:GridView ID="GridView_WageScaleComponentMaster" GridLines="Horizontal" runat="server" AutoGenerateColumns="False" Width="96%" DataKeyNames="WageScaleId" OnSelectedIndexChanged="GridView_WageScaleComponentMaster_SelectedIndexChanged" OnRowEditing="GridView_WageScaleComponentMaster_Row_Editing" OnPreRender="GridView_WageScaleComponentMaster_PreRender" OnDataBound="GridView_WageScaleComponentMaster_DataBound" OnRowCancelingEdit="GridView_WageScaleComponentMaster_RowCancelingEdit" OnRowCommand="GridView_WageScaleComponentMaster_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="38px"/></asp:CommandField>
                             <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>--%>
                              <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditWageScaleComponent" CausesValidation="false" OnClick="btnEditWageScaleComponent_Click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("WageScaleId")%>' />
                                                <asp:HiddenField ID="HiddenWageScaleComId" runat="server" Value='<%#Eval("WageScaleId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Wage Scale"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblwadescalename" runat="server" Text='<%#Eval("WageScaleName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenWageScaleComponentMasterId" runat="server" Value='<%#Eval("WageScaleId")%>' />
                                   <asp:HiddenField ID="HiddenWageScaleName" runat="server" Value='<%#Eval("WageScaleName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                             <asp:TemplateField HeaderText="Currency"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblCurrency" runat="server" Text='<%#Eval("Currency")%>'></asp:Label>
                                  </ItemTemplate>
                               </asp:TemplateField>
                         </Columns>
                              <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                   </div>
            <asp:Label id="lbl_GridView_WageScaleComponent" runat="server" Text=""></asp:Label><asp:Label ID="lbl_WageScaleComponent_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
         </td>
         <td style="width :50%">
         
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div class="input_box" style="overflow-y: scroll; overflow-x: hidden;height: 170px; width: 100% ;text-align:left;padding-left: 5px;padding-right:5px;">
                                        <asp:CheckBoxList ID="chklst_Vessel" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" Width="95%" >
                                        </asp:CheckBoxList></div></td>
                            </tr>
                        </table>
         
         </td>
      </tr>
         </table>
     <asp:Panel ID="pnl_WageScaleComponent" runat="server" Width="100%" Visible="False">
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: Left; width: 600px;" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenWageScaleComponent" runat="server" />
                      </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px">
                              Wage Scale:</td>
                          <td style="text-align: left">
                          <asp:TextBox ID="txtComponentName" runat="server" CssClass="required_box" Width="240px" MaxLength="49" TabIndex="1"></asp:TextBox></td>
                          <td style="text-align: left">
                              Currency :
                              </td>
                          <td>
                              <asp:DropDownList ID="ddlCurrency" runat="server" width="100px" AutoPostBack="true">
                                  <asp:ListItem Selected="True" Text="< Select >" Value="0"></asp:ListItem>
                                  <asp:ListItem Text="INR" Value="INR"></asp:ListItem>
                                  <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right"></td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComponentName"
                                  ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                          <td style="text-align: left">
                              </td>
                          <td>
                              <asp:RequiredFieldValidator ID="rfvddlCurrency" runat="server" ErrorMessage="Required." ControlToValidate="ddlCurrency"
    InitialValue="0"  ForeColor="Red" />
                          </td>
                      </tr>
                       <tr>
                          <td align="right" style="text-align: right; padding-right:15px">
                              Total Work Hours:</td>
                          <td style="text-align: left" colspan="3">
                          <asp:TextBox ID="txtTotalWorkHours" runat="server" Width="66px" MaxLength="3" TextMode="Number" TabIndex="3"></asp:TextBox> &nbsp; 
                               <asp:DropDownList ID="ddlTotalWorkHours" runat="server" width="100px" TabIndex="4">
                                  <asp:ListItem Selected="True" Text="< Select >" Value="0"></asp:ListItem>
                                  <asp:ListItem Text="Per Day" Value="Per Day"></asp:ListItem>
                                  <asp:ListItem Text="Per Week" Value="Per Week"></asp:ListItem>
                              </asp:DropDownList>
                          </td>
                          
                      </tr>
                  </table>
              </asp:Panel>
          <table style="width: 100%">
              <tr>
                  <td style="text-align: right;">
                      <asp:Button ID="btn_Add" runat="server" CssClass="btn" OnClick="btn_Add_Click" Text="Add"
                          Width="59px" CausesValidation="False" TabIndex="4" Visible="False" />
                      <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_Click" TabIndex="5" />
                      <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" OnClick="btn_Cancel_Click"
                          Text="Cancel" Width="59px" CausesValidation="False" TabIndex="6" />
                      <asp:Button ID="btn_Print" runat="server" CssClass="btn" OnClick="btn_Print_Click"
                          Text="Print" Width="59px" CausesValidation="False" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_WageScaleComponent');" Visible="False" TabIndex="7" /></td>
              </tr>
          </table>
      </td>
     </tr>
    </table> 
       </td>
     </tr>
    </table>  
   </div>
</asp:Content>


