<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="PrejoiningDocsMapping.aspx.cs" Inherits="Modules_HRD_Registers_PrejoiningDocsMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
   <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
    <table cellpadding="0" cellspacing="0" width="100%">
     <tr>
      <td align="center" style="height: 14px">
        <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Prejoing Documents and Manning Agent Mapping"></asp:Label><br />
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
            <legend><strong>Manning Agent List</strong></legend>
            <div id="div-datagrid" style=" width:100%; height :150px; overflow-y:Scroll; overflow-x:hidden; text-align:center">
                            <asp:GridView ID="Gv_PrejoinDocsMapping" GridLines="Horizontal" runat="server" AutoGenerateColumns="False" Width="96%" DataKeyNames="Manning_AgentId" OnSelectedIndexChanged="Gv_PrejoinDocsMapping_SelectedIndexChanged" OnRowEditing="Gv_PrejoinDocsMapping_Row_Editing" OnPreRender="Gv_PrejoinDocsMapping_PreRender" OnDataBound="Gv_PrejoinDocsMapping_DataBound" OnRowCancelingEdit="Gv_PrejoinDocsMapping_RowCancelingEdit" OnRowCommand="Gv_PrejoinDocsMapping_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="38px"/></asp:CommandField>
                             <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>--%>
                              <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditPrejoinDocs" CausesValidation="false" OnClick="btnEditPrejoinDocs_Click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("Manning_AgentId")%>' />
                                                <asp:HiddenField ID="hdnManning_AgentId" runat="server" Value='<%#Eval("Manning_AgentId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Manning Agent"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblAgentName" runat="server" Text='<%#Eval("Manning_AgentName")%>'></asp:Label>
                                   <asp:HiddenField ID="hdnAgentId" runat="server" Value='<%#Eval("Manning_AgentId")%>' />
                                   <asp:HiddenField ID="hdnAgentName" runat="server" Value='<%#Eval("Manning_AgentName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                         </Columns>
                              <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                   </div>
            <asp:Label id="lbl_PrejoingDoc_Msg" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lbl_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
         </td>
         <td style="width :50%">
         
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div class="input_box" style="overflow-y: scroll; overflow-x: hidden;height: 170px; width: 100% ;text-align:left;">
                                        <asp:CheckBoxList ID="chklst_Docs" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Width="95%">
                                        </asp:CheckBoxList></div></td>
                            </tr>
                        </table>
         
         </td>
      </tr>
         </table>
     <asp:Panel ID="pnl_PrejoingDocs" runat="server" Width="100%" Visible="False">
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center; width: 400px;" width="100%">
                    <tr>
                      <td colspan="3">
                          <asp:HiddenField ID="hdnManningAgentId" runat="server" />
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
                              Manning Agent:</td>
                          <td style="text-align: left">
                          <asp:DropDownList ID="ddlManningAgent" runat="server" CssClass="required_box" Width="240px" MaxLength="49" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlManningAgent_SelectedIndexChanged"></asp:DropDownList></td>
                          <td style="text-align: left">
                              </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right"></td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddlManningAgent"
                                                          ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                          <td style="text-align: left">
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
</asp:Content>

