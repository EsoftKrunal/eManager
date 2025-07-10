<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertCategory.aspx.cs" Inherits="Registers_CertCategory" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" /> 
</head>
<body>
    <form id="form1" runat="server">--%>
    <div style="text-align: center">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <table align="center" width="800px" border="0" cellpadding="0" cellspacing="0">   
    <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Certificate Category"></asp:Label></td>
    </tr>   
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Certificate Category</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                      <td style=" padding-top:5px;">
                      <div style="overflow: auto; width: 780px; height: 150px">
                       <asp:GridView ID="GridView_CertCat" GridLines="Horizontal" runat="server" 
                              AutoGenerateColumns="False" Width="780px" DataKeyNames="CertCatId" 
                              OnSelectedIndexChanged="GridView_RecruitingOffice_SelectedIndexChanged" 
                              OnRowEditing="GridView_RecruitingOffice_Row_Editing" 
                              OnRowDeleting="GridView_RecruitingOffice_Row_Deleting" 
                              OnPreRender="GridView_RecruitingOffice_PreRender" 
                              OnDataBound="GridView_RecruitingOffice_DataBound" OnRowCommand="GridView_CertCat_RowCommand" >
                         <Columns>
                               <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" ><ItemStyle Width="35px" /></asp:CommandField>
                               <%--<asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ><ItemStyle Width="40px" /></asp:CommandField>--%>
                              <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditManningGrade" CausesValidation="false" OnClick="btnEditManningGrade_Click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("CertCatId")%>' />
                                                <asp:HiddenField ID="hdnCertCatId" runat="server" Value='<%#Eval("CertCatId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                               <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                               </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Certificate Category Name"><ItemStyle HorizontalAlign="Left" />
                               <ItemTemplate>  
                                   <asp:Label ID="lblCertCatName" runat="server" Text='<%#Eval("CertCatName")%>'></asp:Label>
                                   <asp:HiddenField ID="CertCatId" runat="server" Value='<%#Eval("CertCatId")%>' />
                               </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="StatusName" HeaderText="Status"><ItemStyle HorizontalAlign="Left" Width="80px" /></asp:BoundField>
                         </Columns>
                            <RowStyle CssClass="rowstyle" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <PagerStyle CssClass="pagerstyle" />
                            <HeaderStyle  CssClass="headerstylefixedheader"  />
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_CertCat" runat="server"></asp:Label>
                  </td>
                 </tr>
                </table>        
                <asp:Label ID="lbl_CertCat_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
                &nbsp;<br />
            <asp:Panel ID="pnl_CertCat" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Recruiting Office Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenCertCatId" runat="server" />
                      </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                        <tr>
                            <td align="right" style="text-align: right; padding-right:15px">
                                Category Name:</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCertCatName" runat="server" CssClass="required_box" 
                                    Width="240px" MaxLength="255" TabIndex="1"></asp:TextBox></td>
                            <td align="right" style="text-align: right">
                                </td>
                            <td style="text-align: left">
                                </td>
                        </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCertCatName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                        <tr>
                            <td align="right" style="text-align: right; padding-right:15px">
                                Created By:</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCreatedBy_CertCat" runat="server" CssClass="input_box" 
                                    ReadOnly="True" Width="240px"></asp:TextBox></td>
                            <td align="right" style="text-align: right; padding-right:15px">
                                Created On:</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCreatedOn_CertCat" runat="server" CssClass="input_box" 
                                    ReadOnly="True" Width="240px"></asp:TextBox></td>
                        </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                    <tr>
                        <td align="right" style="text-align: right; padding-right:15px">
                            Modified By:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtModifiedBy_CertCat" runat="server" CssClass="input_box" 
                                ReadOnly="True" Width="240px"></asp:TextBox></td>
                        <td align="right" style="text-align: right; padding-right:15px">
                            Modified On:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtModifiedOn_CertCat" runat="server" CssClass="input_box" 
                                ReadOnly="True" Width="240px"></asp:TextBox></td>
                    </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px">
                              Status:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddlStatus_CertCat" runat="server" CssClass="input_box" 
                                  Width="145px" TabIndex="2">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style=" text-align: left">
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
                       <asp:Button ID="btn_Add_CertCat" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_RecruitingOffice_Click" 
                                CausesValidation="False" TabIndex="3" />
                            <asp:Button ID="btn_Save_CertCat" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_RecruitingOffice_Click" TabIndex="4" />
                            <asp:Button ID="btn_Cancel_CertCat" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_RecruitingOffice_Click" 
                                CausesValidation="False" TabIndex="5" />
                            <asp:Button ID="btn_Print_CertCat" runat="server" CausesValidation="False" 
                                CssClass="btn" OnClick="btn_Print_RecruitingOffice_Click" TabIndex="6" 
                                Text="Print" Width="59px" 
                                OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_RecruitingOffice');" 
                                Visible="False" />                 
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
    <%--</form>
</body>
</html>--%>