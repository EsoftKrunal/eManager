<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractCompanyMaster.aspx.cs" Inherits="Registers_Company" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
     <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        
            <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Contract Company"></asp:Label></td>
    </tr> 
        <tr>
            <td>
                <asp:Label ID="lbl_Company_Message" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;
                <asp:Label ID="lbl_report_message" runat="server" ForeColor="#C00000"></asp:Label></td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Contract Company List</strong></legend>
                      <div style="overflow-x:hidden; overflow-y:scroll; width: 100%; height: 150px">
                       <asp:GridView ID="GridView_ContractCompany" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="CompanyId" OnSelectedIndexChanged="GridView_ContractCompany_SelectedIndexChanged" OnRowEditing="GridView_ContractCompany_Row_Editing" OnRowDeleting="GridView_ContractCompany_Row_Deleting" OnPreRender="GridView_ContractCompany_PreRender" OnDataBound="GridView_ContractCompany_DataBound" GridLines="Horizontal" OnRowCommand="GridView_ContractCompany_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="38px"/></asp:CommandField>
                              <%--<asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>--%>
                              <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditCompnayContract" CausesValidation="false" OnClick="btnEditCompnayContract_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("CompanyId")%>' />
                                    <asp:HiddenField ID="hdnContractCompanyId" runat="server" Value='<%#Eval("CompanyId")%>' />
                                          <%-- <asp:HiddenField ID="hdnCompanyName" runat="server" Value='<%#Eval("CompanyName")%>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                            
                               <asp:TemplateField HeaderText="Contract Company"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                                <asp:HiddenField ID="HiddenCompanyId" runat="server" Value='<%#Eval("CompanyId")%>' />
                                   <asp:HiddenField ID="hdnCompanyName" runat="server" Value='<%#Eval("CompanyName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                              
                                <asp:BoundField DataField="CompanyAbbr" HeaderText="Company Abbr">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                                 <asp:BoundField DataField="RegistrationNo" HeaderText="Reg No">
                                 <ItemStyle HorizontalAlign="Left"  />
                               </asp:BoundField>
                             
                               <asp:BoundField DataField="IsShipManager" HeaderText="Ship Manager">
                                 <ItemStyle HorizontalAlign="Center"  Width="80px" />
                               </asp:BoundField>
                                <asp:BoundField DataField="IsOwnerAgent" HeaderText="Owner Agent">
                                 <ItemStyle HorizontalAlign="Center" Width="80px" />
                               </asp:BoundField>
                             <asp:BoundField DataField="IsMLCAgent" HeaderText="MLC Owner">
                                 <ItemStyle HorizontalAlign="Center" Width="80px" />
                               </asp:BoundField>
                              <asp:BoundField DataField="IsManningAgent" HeaderText="Manning Agent">
                                 <ItemStyle HorizontalAlign="Center" Width="80px" />
                               </asp:BoundField>
                              <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Center"  />
                               </asp:BoundField>
                         </Columns>
                            <%-- <SelectedRowStyle CssClass="selectedtowstyle" />
                             <PagerStyle CssClass="pagerstyle" />
                             <HeaderStyle CssClass="headerstyle" HorizontalAlign="Left" />
                             <AlternatingRowStyle CssClass="alternatingrowstyle" />--%>
                              <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_ContractCompany" runat="server" Text=""></asp:Label>
                <asp:Label ID="lbl_ContractCompany_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
                    <asp:Panel ID="Panel_Company" runat="server" Width="100%" Visible="false">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Contract Company Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="6">
                                                     <asp:HiddenField ID="Hiddencompanypk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Company:</td>
                                                 <td align="left" style="height: 19px">
                                                     <asp:TextBox ID="txtcompanyname" runat="server" CssClass="required_box" MaxLength="49" Width="200px" TabIndex="1"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Company Abbr:</td>
                                                 <td align="left" style="height: 19px">
                                                     <asp:TextBox ID="txtCompanyabbr" runat="server" CssClass="input_box" MaxLength="9" TabIndex="2"
                                                         Width="200px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Reg No:
                                                     </td>
                                                 <td align="left" style="height: 19px">
                                                     <asp:TextBox ID="txtRegNo" runat="server" MaxLength="50" Width="200px" TabIndex="3"></asp:TextBox>
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 10px">
                                                  </td>
                                                  <td align="left" style="height: 10px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcompanyname"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 10px">
                                                  </td>
                                                  <td align="left" style="height: 10px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 10px">
                                                  </td>
                                                  <td align="left" style="height: 10px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="height: 20px; padding-right:15px;">
                                                     Address 1:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcompanyaddress1" runat="server" CssClass="input_box" MaxLength="29" TabIndex="4"
                                                         Width="200px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Address 2:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcompanyaddress2" runat="server" CssClass="input_box" MaxLength="29" TabIndex="5"
                                                         Width="200px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Address 3:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcompanyaddress3" runat="server" CssClass="input_box" MaxLength="29" TabIndex="6"
                                                         Width="200px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6" style="height: 13px">
                                                     &nbsp; &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="height: 20px; padding-right:15px;">
                                                     Telephone # 1:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txttelno1" runat="server" CssClass="input_box" MaxLength="14" TabIndex="7"
                                                         Width="200px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Telephone # 2:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txttelno2" runat="server" CssClass="input_box" MaxLength="14" TabIndex="8"
                                                         Width="200px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Fax #:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtfaxno" runat="server" CssClass="input_box" MaxLength="14" TabIndex="9"
                                                         Width="200px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6" style="height: 13px">
                                                     &nbsp; &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="height: 20px; padding-right:15px;">
                                                     Email 1:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtemail1" runat="server" CssClass="input_box" MaxLength="99" TabIndex="10"
                                                         Width="200px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Email 2:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtemail2" runat="server" CssClass="input_box" MaxLength="99" TabIndex="11"
                                                         Width="200px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Website:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtwebsite" runat="server" CssClass="input_box" MaxLength="99" TabIndex="12"
                                                         Width="200px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail1"
                                                          ErrorMessage="Invalid Email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtemail2"
                                                          ErrorMessage="Invalid Email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right:15px; height: 19px;"> Contact Person:</td>
                                                  <td align="left" style="height: 20px"><asp:TextBox ID="txtContactPerson" runat="server" CssClass="input_box" MaxLength="99" TabIndex="13"
                                                         Width="200px"></asp:TextBox></td>
                                                  <td align="right" style="padding-right:15px; height: 19px;">RPSL:</td>
                                                  <td>
                                                      <asp:TextBox ID="txtRPSL" runat="server" MaxLength="50" TabIndex="14" Width="200px"></asp:TextBox>
                                                  </td>
                                                  <td align="right" style="height: 20px; padding-right: 15px">RPSL Validity:</td>
                                                  <td align="left" style="height: 20px">
                                                                                                            <asp:TextBox ID="txtRPSLValidity" runat="server" MaxLength="50" TabIndex="15" Width="100px"></asp:TextBox> <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"  />  
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                        PopupButtonID="ImageButton2" PopupPosition="Left" TargetControlID="txtRPSLValidity">        </ajaxToolkit:CalendarExtender>
                                                  </td>
                                              </tr>
                                               <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      </td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      </td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RegularExpressionValidator ID="CompareValidator12" runat="server" ControlToValidate="txtRPSLValidity" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_company" runat="server" CssClass="input_box" MaxLength="24" TabIndex="-1"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_company" runat="server" CssClass="input_box" MaxLength="24" TabIndex="-1"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="height: 20px; padding-right: 15px">
                                                     &nbsp;</td>
                                                 <td align="left" style="height: 20px">
                                                       &nbsp;</td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedby_company" runat="server" CssClass="input_box" MaxLength="24" TabIndex="-1"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_company" runat="server" CssClass="input_box" MaxLength="24" TabIndex="-1"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="height: 20px">
                                                     </td>
                                                 <td align="left" style="height: 20px">
                                                     &nbsp; &nbsp;&nbsp;  
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 19px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 19px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                  </td>
                                                  <td align="right" style="height: 20px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 20px">
                                                     Status:</td>
                                                  <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddstatus_company" runat="server" CssClass="input_box" Width="205px" TabIndex="16">
                                                     </asp:DropDownList></td>
                                                  <td align="left" style="padding-left: 15px; height: 20px" colspan="4">
                                                      <asp:CheckBox ID="chkIsShipManager" runat="server" Text="Ship Manager" TextAlign="Right" TabIndex="17" /> &nbsp;
                                                      <asp:CheckBox ID="chkIsOwnerAgent" runat="server" Text="Owner Agent" TextAlign="Right" TabIndex="18" /> &nbsp;
                                                      <asp:CheckBox ID="chkIsMLCAgent" runat="server" Text="MLC Owner" TextAlign="Right" TabIndex="19" /> &nbsp; 
                                                      <asp:CheckBox ID="chkIsManningAgent" runat="server" Text="Manning Agent" TextAlign="Right" TabIndex="20" /> &nbsp;
                                                      </td>
                                                  
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 20px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 20px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                      </td>
                                                  <td align="right" style="padding-right: 15px; height: 20px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                      </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6" style="height: 13px">
                                                   &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
                                                      &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                             
                                          </table>
                         </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td align="right" style="height: 18px">
                   
                    <asp:HiddenField ID="hfd_CompanyImage" runat="server" />
                     <asp:Button ID="btn_Add_CompanyContract" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_CompanyContract_Click" CausesValidation="False" TabIndex="6" />
                    <asp:Button ID="btn_company_save" runat="server" CssClass="btn" OnClick="btn_company_save_Click"
                            Text="Save" Width="59px" TabIndex="15" />
                    <asp:Button ID="btn_company_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_company_Cancel_Click" Text="Cancel"
                                Width="59px" TabIndex="16" />
                    <asp:Button ID="btn_Print_Company" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Company_Click" TabIndex="17" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_companypanel');" />            
               </td>
            </tr>
            <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
        </asp:Content>
    <%--</form>
</body>
</html>--%>
