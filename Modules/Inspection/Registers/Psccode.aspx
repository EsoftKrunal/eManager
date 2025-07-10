<%@ Page Language="C#" MasterPageFile="~/Modules/Inspection/Registers//RegistersMasterPage.master" AutoEventWireup="true" CodeFile="Psccode.aspx.cs" Inherits="Psccode" Title="PSC Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
            <asp:Panel ID="pnl_InspectionGroup" runat="server" Width="100%">
                    
                    <table cellpadding="3" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="padding:5px;">
                                <asp:Button ID="btnAddPscCode" runat="server" OnClick="btnAddPscCode_OnClick" Text="Add New PSC Code" CssClass="btn"  style="float:right;"/>
                            </td>
                        </tr>
                    </table>


                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 5px; padding-left: 5px">
                            <div style="width: 100%; height: 255px">
                              <asp:GridView ID="grdPscCode" runat="server" GridLines="Both" AutoGenerateColumns="False" Width="98%" AllowPaging="True" OnPageIndexChanging="GridView_InsGrp_PageIndexChanging" PageSize="8"><RowStyle CssClass="rowstyle" />
                                <Columns>
                                        <asp:TemplateField HeaderText="Sr#">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfPscCode" runat="server" Value='<%# Eval("Id") %>' />
                                            <%#Eval("Row")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="KPI Head" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblPscCode" runat="server" Text='<%#Eval("PSCCODE") %>'></asp:Label>
                                            </ItemTemplate>  
                                            <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="KPI" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label>
                                            </ItemTemplate>  
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="BtnEdit" runat="server" OnClick="BtnEdit_OnClick" ImageUrl="~/Images/edit.jpg" CausesValidation="false" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CausesValidation="False" ImageUrl="~/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" ToolTip="Delete" />
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center" ></ItemStyle>
                                        </asp:TemplateField>
                                </Columns>
                                <pagerstyle horizontalalign="Center" />
                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                <HeaderStyle CssClass="headerstylefixedheader" />
                            </asp:GridView>
                         </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMessege" runat="server" style="color:Red;"></asp:Label>
                        </td>
                    </tr>
                </table>
                &nbsp;
           </asp:Panel></fieldset>                              
          </td>
         </tr>
        </table>
           &nbsp;
       </td>
      </tr>
     </table>
     
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="DivAddPscCode" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:140px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <col width="120px" />
                    <col />
                    <tr>
                        <td class="headerstyle" style="text-align:center;" colspan="2" > <b>Add PSC Code </b> </td>
                    </tr>
                    <tr>
                        <td colspan="2"> &nbsp; </td>
                    </tr>
                    <tr>
                          <td >PSC Code :</td>
                          <td style="text-align: left"> 
                             <asp:TextBox ID="txtPscCode" runat="server" CssClass="input_box" MaxLength="3" Width="60px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RFV1" runat="server" ErrorMessage="*" ControlToValidate="txtPscCode"></asp:RequiredFieldValidator>
                           </td>
                      </tr>
                      <tr>
                          <td >Description :</td>
                          <td style="text-align: left"> 
                             <asp:TextBox ID="txtDescription" runat="server" CssClass="input_box" MaxLength="100" Width="400px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                           </td>
                      </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;"> 
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="7" OnClick="btnSavePscCode_OnClick" />
                            <asp:Button ID="btnCloseAddHeadPopup" runat="server" CssClass="btn" Text="Close" Width="59px" TabIndex="7" OnClick="btnCloseAddHeadPopup_Click"  CausesValidation="false"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;"> 
                            
                        </td>
                    </tr>
                </table>

            </div>
        </center>
    </div>
</asp:Content>

