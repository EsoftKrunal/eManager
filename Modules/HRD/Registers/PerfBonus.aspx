<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PerfBonus.aspx.cs" Inherits="Registers_PerfBonus" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <div style="text-align: center">
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
       <td align="center">
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Performance Bonus"></asp:Label></td>
    </tr> 
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Performance Bonus List</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                      <td style=" padding-top:5px;">                      
                      <div style="overflow: auto; width: 100%; height: 150px">
                       <asp:GridView ID="GridView_PB" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="false" Width="98%" DataKeyNames="TableId" OnSelectedIndexChanged="GridView_PB_SelectedIndexChanged" OnRowEditing="GridView_PB_Row_Editing" OnRowDeleting="GridView_PB_Row_Deleting" OnPreRender="GridView_PB_PreRender" OnDataBound="GridView_PB_DataBound" GridLines="Horizontal" OnRowCommand="GridView_PB_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="30px"/></asp:CommandField>
                              <%--<asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="27px" /></asp:CommandField>--%>
                             <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditPerfBonus" CausesValidation="false" OnClick="btnEditPerfBonus_Click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("TableId")%>' />
                                                <asp:HiddenField ID="hdnTableId" runat="server" Value='<%#Eval("TableId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="35px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                                  <asp:BoundField DataField="AppDate" HeaderText="Start Date" DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false"  ItemStyle-Width="120px">
                                 <ItemStyle HorizontalAlign="Center" />
                               </asp:BoundField>
                                <asp:BoundField DataField="Bonus1" HeaderText="Bonus(0-5 Yrs)" HeaderStyle-Width="120px">
                                 <ItemStyle HorizontalAlign="Right"/>
                               </asp:BoundField>
                              
                                <asp:BoundField DataField="Bonus2" HeaderText="Bonus(6-10 Yrs)">
                                 <ItemStyle HorizontalAlign="Right" Width="120px"/>
                               </asp:BoundField>
                             
                               <asp:BoundField DataField="Bonus3" HeaderText="Bonus(>6 Yrs)" >
                                 <ItemStyle HorizontalAlign="Right"/>
                               </asp:BoundField>
                         </Columns>
                              <RowStyle CssClass="rowstyle" />
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <PagerStyle CssClass="pagerstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_PB" runat="server" Text=""></asp:Label></td>
                 </tr>
                </table>        
                <asp:Label ID="lbl_PB_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
                &nbsp;<br />
            <asp:Panel ID="pnl_PB" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Performance Bonus Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenPB" runat="server" />
                      </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right; width: 202px;">
                          </td>
                          <td style="height: 13px; text-align: left; width: 170px;">
                          </td>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right; width: 223px;">
                          </td>
                          <td style="height: 13px; text-align: left; width: 193px;">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right; width: 202px;">
                              Start Date:</td>
                          <td style="height: 13px; text-align: left; width: 170px;">
                              <asp:TextBox ID="txtStartDt" runat="server" CssClass="input_box" MaxLength="19" TabIndex="3"
                                  Width="200px"></asp:TextBox></td>
                          <td align="right" style="padding-right: 15px; height: 13px; text-align: right; width: 223px;">
                              Bonus(0-5 Yrs):</td>
                          <td style="height: 13px; text-align: left; width: 193px;">
                              <asp:TextBox ID="txtBonus1" runat="server" CssClass="input_box" MaxLength="19"
                                  TabIndex="3" Width="190px"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px; width: 202px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 170px;">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDt" ErrorMessage="Required"></asp:RequiredFieldValidator>
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px; width: 223px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 193px;">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBonus1" ErrorMessage="Required"></asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px; width: 202px;">
                              Bonus(6-10 Yrs):</td>
                          <td style="text-align: left; height: 19px; width: 170px;">
                              <asp:TextBox ID="txtBonus2" runat="server" CssClass="required_box" MaxLength="49"
                                  Width="200px" TabIndex="4"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px; width: 223px;">
                              Bonus( &gt; 10 Yrs):</td>
                          <td style="text-align: left; height: 19px; width: 193px;">
                              <asp:TextBox ID="txtBonus3" runat="server" CssClass="input_box" MaxLength="19" TabIndex="3" Width="190px"></asp:TextBox>
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 16px; width: 202px;">
                          </td>
                          <td style="text-align: left; height: 16px; width: 170px;">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                  ControlToValidate="txtBonus2" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 16px; width: 223px;">
                          </td>
                          <td style="text-align: left; height: 16px; width: 193px;">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                  runat="server" ControlToValidate="txtBonus3" ErrorMessage="Required"></asp:RequiredFieldValidator>
                              </td>
                      </tr>
                                                        </table>
                  <br />
                                                    </fieldset>
                <br />
                                                    </asp:Panel>                               
           <table style="width: 100%">
               <tr>
                   <td style="text-align: right">
                        <asp:Button ID="btn_Add_PB" runat="server" CssClass="btn" Text="Add" Width="59px" OnClick="btn_Add_PB_Click" CausesValidation="False" />
                        <asp:Button ID="btn_Save_PB" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_PB_Click"  />
                        <asp:Button ID="btn_Cancel_PB" runat="server" CssClass="btn" Text="Cancel" Width="59px" OnClick="btn_Cancel_PB_Click" CausesValidation="False" />
                        <asp:Button ID="btn_Print_PB" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_PB_Click" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_AccountHead');" Visible="False" />
                    </td>
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
