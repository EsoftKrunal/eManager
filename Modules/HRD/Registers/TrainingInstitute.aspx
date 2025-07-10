<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrainingInstitute.aspx.cs" Inherits="Registers_TrainingInstitute"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family:Arial;font-size:12px;">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td></td>
        </tr> 
          <tr>
            <td style="text-align: center;">
            <table cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                      <td style=" padding-top:5px;">
                      <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 200px">
                       <asp:GridView ID="GridView_TrainingInstitute" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="InstituteId" OnSelectedIndexChanged="GridView_TrainingInstitute_SelectedIndexChanged" OnRowEditing="GridView_TrainingInstitute_Row_Editing" OnRowDeleting="GridView_TrainingInstitute_Row_Deleting" OnPreRender="GridView_TrainingInstitute_PreRender" OnDataBound="GridView_TrainingInstitute_DataBound"  GridLines="Horizontal" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="35px" /></asp:CommandField>
                              <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Training Institute"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblTrainingInstitutename" runat="server" Text='<%#Eval("InstituteName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenTrainingInstituteId" runat="server" Value='<%#Eval("InstituteId")%>' />
                                    <asp:HiddenField ID="HiddenTrainingInstituteName" runat="server" Value='<%#Eval("InstituteName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                                <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                         </Columns>
                              <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_TrainingInstitute" runat="server" Text=""></asp:Label>
                  </td>
                 </tr>
                </table>        
            <asp:Label ID="lbl_TrainingInstitute_Message" runat="server" ForeColor="#C00000"></asp:Label>
            <asp:Panel ID="pnl_TrainingInstitute" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Training Institute Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenTrainingInstitute" runat="server" />
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
                                                                    Training Institute:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtTrainingInstituteName" runat="server" CssClass="required_box" Width="200px" MaxLength="49" TabIndex="1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right">
                                                                    Status :</td>
                                                                <td style="text-align: left">
                                                                <asp:DropDownList ID="ddlStatus_TrainingInstitute" runat="server" CssClass="input_box" TabIndex="2" Width="205px">
                              </asp:DropDownList>
                                                                    </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTrainingInstituteName"
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
                                                                    <asp:TextBox ID="txtCreatedBy_TrainingInstitute" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_TrainingInstitute" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtModifiedBy_TrainingInstitute" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_TrainingInstitute" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                  <br />
                                                    </fieldset>
                <br />
                                                    </asp:Panel>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                       <asp:Button ID="btn_Add_TrainingInstitute" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_TrainingInstitute_Click" CausesValidation="False" TabIndex="3" />
                            <asp:Button ID="btn_Save_TrainingInstitute" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_TrainingInstitute_Click" TabIndex="4" />
                            <asp:Button ID="btn_Cancel_TrainingInstitute" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_TrainingInstitute_Click" CausesValidation="False" TabIndex="5" />
                            <asp:Button ID="btn_Print_TrainingInstitute" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_TrainingInstitute_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_TrainingInstitute');" Visible="False" />                
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
    </form>
</body>
</html>
