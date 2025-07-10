<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SendMailConfiguartion.aspx.cs" Inherits="Modules_Admin_SendMailConfiguartion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <link rel="stylesheet" href="../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <asp:UpdatePanel ID="upDetails" runat="server">
        <ContentTemplate>

            <section class="content">
                <div class="box-wrapper">
                    <div class="box box-warning">
                        <div class="box-body">
                           
     <table style="width: 100%; text-align: center; ">  
            <tr>
                <td colspan="2" style="text-align:center;background-color:#4c7a6f;color:#fff;font-size: 14px;" class="text headerband" >
                    <%--<img runat="server" id="imgHelp" moduleid="1" style ="cursor:pointer;float :right; padding-right :5px;" src="~/Modules/HRD/Images/help.png" alt="Help ?"/>--%>&nbsp;Send Mail Configuration</td>
            </tr>
             
        </table> 
                             <br />
    <table  align="center" width="100%" border="0" cellpadding="0" cellspacing="0"> 
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Send Mail Configuration List</strong></legend>
                    <table width="100%"><tr><td>
                    <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvSendMail" runat="server" GridLines="Horizontal"  AutoGenerateColumns="False" OnDataBound="GvSendMail_DataBound"
                            OnPreRender="GvSendMail_PreRender" OnRowDeleting="GvSendMail_Row_Deleting" 
                            OnSelectedIndexChanged="GvSendMail_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCancelingEdit="GvSendMail_RowCancelingEdit" OnRowCommand="GvSendMail_RowCommand">
                             <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                              <%--  <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True">
                                    <ItemStyle Width="40px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEditSendMail" CausesValidation="false" OnClick="btnEditSendMail_Click"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                                                CommandArgument='<%#Eval("SMC_Id")%>' />
                                                                            <asp:HiddenField ID="HiddenSMC_Id" runat="server" Value='<%#Eval("SMC_Id")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                            Text="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Process Name">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSMC_ProcessName" runat="server" Text='<%#Eval("SMC_ProcessName")%>'></asp:Label>
                                       
                                        <asp:HiddenField ID="HiddenSMC_ProcessName" runat="server" Value='<%#Eval("SMC_ProcessName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From Mail">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSMC_Frommail" runat="server" Text='<%#Eval("SMC_Frommail")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="BCC Mail">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSMC_BCC" runat="server" Text='<%#Eval("SMC_BCC")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="StatusId" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div></td></tr></table>
                    <asp:Label ID="lblSendMail" runat="server"></asp:Label>
                    <asp:Label ID="lbl_SendMail_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    </fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="SendMailpanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Send Mail Configuartion Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="4">
                                                     <asp:HiddenField ID="HiddenSendMailpk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Process Name:</td>
                                                 <td align="left">
                                                     <asp:DropDownList ID="ddl_ProcessName" runat="server" CssClass="required_box"  TabIndex="1"
                                                         Width="240px"></asp:DropDownList>

                                                     

                                                 </td>
                                                 <td align="right">
                                                     From Mail:
                                                     </td>
                                                 <td align="left">
                                                      <asp:TextBox ID="txtFromMail" runat="server" CssClass="required_box" MaxLength="200" TabIndex="1"
                                                         Width="240px" ></asp:TextBox>
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                     <asp:CompareValidator ID="cv_ProcessName" runat="server" ControlToValidate="ddl_ProcessName" ErrorMessage="Required." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
                                                  <td align="right" style="height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFromMail"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator>
                                                  </td>
                                              </tr>
                                               <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     BCC Mail:</td>
                                                 <td align="left" colspan="3">
                                                     <asp:TextBox ID="txtBCCMail" runat="server"  MaxLength="500" TabIndex="1"
                                                         Width="840px"  CssClass="input_box" ></asp:TextBox></td>
                                              </tr>
                                               <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                     </td>
                                                  <td align="right" style="height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      
                                                  </td>
                                              </tr>
                                               <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Subject :</td>
                                                 <td align="left" colspan="3">
                                                     <asp:TextBox ID="txtSubject" runat="server"  MaxLength="200" TabIndex="1"
                                                         Width="840px" CssClass="input_box" ></asp:TextBox></td>
                                              </tr>
                                               <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                     </td>
                                                  <td align="right" style="height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Mail Body:</td>
                                                 <td align="left" colspan="3" style="height:120px;">
                                                     <asp:TextBox ID="txtBody" runat="server"  CssClass="input_box"  MaxLength="4000" TabIndex="1"
                                                         Width="900px" TextMode="MultiLine" Height="110px" ></asp:TextBox></td>
                                                
                                              </tr>
                                               <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                     </td>
                                                  <td align="right" style="height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="4">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedby" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="4">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Status:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddstatus_SendMail" runat="server" CssClass="input_box" Width="129px" TabIndex="2">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="height: 20px">
                                                     </td>
                                                 <td align="left" style="height: 20px">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="4">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                          </table>
                         </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 18px">
                    <asp:Button ID="btn_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_add_Click" Text="Add" Width="59px" TabIndex="3" />
                    <asp:Button ID="btn_save" runat="server" CssClass="btn" OnClick="btn_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="4" />
                    <asp:Button ID="btn_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn"  Text="Cancel"
                                Width="59px" Visible="False" TabIndex="5" OnClick="btn_Cancel_Click" />
                    <asp:Button ID="btn_Print" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Departmentpanel');" Visible="False" />                                 
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
                             </div>
                    </div>
                </div>

            </section>



        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>