<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="Matrix.aspx.cs" Inherits="Registers_Matrix" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
<div style="text-align: center">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0"> 
    <tr>
       <td style="width: 801px">
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Matrix"></asp:Label></td>
    </tr>   
     <tr>
       <td style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="height: 200px; text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Matrix</strong></legend>
                    <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">  
                       <asp:GridView ID="gv_header" GridLines="Horizontal" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="MatrixId" OnRowDeleting="gv_header_RowDeleting" OnRowEditing="gv_header_RowEditing" OnSelectedIndexChanged="gv_header_SelectedIndexChanged" OnDataBound="gv_header_DataBound" OnRowCommand="gv_header_RowCommand"  >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="35px" /></asp:CommandField>
                             <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>--%>
                              <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditMatrix" CausesValidation="false" OnClick="btnEditMatrix_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("MatrixId")%>' />
                                    <asp:HiddenField ID="hdnMatrixId" runat="server" Value='<%#Eval("MatrixId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               
                               <asp:BoundField DataField="MatrixName" HeaderText="Matrix Name">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                              <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                 <ItemStyle HorizontalAlign="Left" Width="70px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>--%>
      <%--                         <asp:TemplateField HeaderText="ModifiedOn">
                               
                               <ItemTemplate>
                               <asp:Label ID="labelmodi" runat="server" Text='<%#Eval("ModifiedOn") %>'></asp:Label>
                               <asp:HiddenField ID="HiddenMatrixId" runat="server" Value='<%#Eval("MatrixId")%>' />
                                         <asp:HiddenField ID="HiddenMatrixName" runat="server" Value='<%#Eval("MatrixName")%>' />
                               </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Left" Width="70px" />
                               </asp:TemplateField>--%>
                               <%--<asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>--%>
                                                        <asp:TemplateField HeaderText="Status">
                               
                               <ItemTemplate>
                               <asp:Label ID="labelmodi" runat="server" Text='<%#Eval("StatusId") %>'></asp:Label>
                               <asp:HiddenField ID="HiddenMatrixId" runat="server" Value='<%#Eval("MatrixId")%>' />
                                         <asp:HiddenField ID="HiddenMatrixName" runat="server" Value='<%#Eval("MatrixName")%>' />
                               </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Left" Width="70px" />
                               </asp:TemplateField>
                  <%--              <asp:BoundField DataField="StatusId" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="50px" />
                               </asp:BoundField>--%>
                               
                               
                         </Columns>
                             <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_VesselType" runat="server" Text=""></asp:Label>
                  <asp:Label ID="lbl_message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
            <asp:Panel ID="pnl_Matrix" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Matrix Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4" style="height: 4px">
                          <asp:HiddenField ID="hiddenmatrixid" runat="server" />
                      </td>
                        <td colspan="1" style="height: 4px; width: 56px;">
                        </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right: 5px; height: 20px; width: 263px;">
                                                                    Matrix Name:</td>
                                                                <td style="text-align: left; height: 20px; width: 190px;">
                                                                    <asp:TextBox ID="txtmatrixname" runat="server" CssClass="required_box" Width="175px" MaxLength="49" TabIndex="1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; height: 20px; width: 219px;">
                                                                    </td>
                                                                <td style="text-align: left; height: 20px; width: 31px;">
                                                                    </td>
                                                                <td style="width: 56px; height: 20px; text-align: left">
                                                                </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 263px; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 190px;">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ControlToValidate="txtmatrixname"></asp:RequiredFieldValidator></td>
                          <td align="right" style="text-align: right; height: 13px; width: 219px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 31px;">
                          </td>
                          <td style="width: 56px; height: 13px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; width: 263px; height: 20px; text-align: right">
                              Rank:</td>
                          <td style="height: 20px; text-align: left; width: 190px;">
                              <asp:DropDownList ID="ddlrank" runat="server" Width="180px" CssClass="required_box">
                              </asp:DropDownList></td>
                          <td align="right" style="padding-right: 5px; height: 20px; text-align: right; width: 219px;">
                              Rank:</td>
                          <td style="height: 20px; text-align: left; width: 31px;">
                              <asp:DropDownList ID="ddlrank2" runat="server" CssClass="required_box" Width="180px">
                              </asp:DropDownList></td>
                          <td style="width: 56px; height: 20px; text-align: left">
                              &nbsp; &nbsp;&nbsp;
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; width: 263px; text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 190px;">
                              <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlrank"
                                  ErrorMessage="Required" Operator="GreaterThan" ValidationGroup="rr" ValueToCompare="0"></asp:CompareValidator></td>
                          <td align="right" style="padding-right: 5px; text-align: right; height: 13px; width: 219px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 31px;">
                              <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlrank2"
                                  ErrorMessage="Required" Operator="GreaterThan" ValidationGroup="rr" ValueToCompare="0"></asp:CompareValidator></td>
                          <td style="width: 56px; height: 13px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; width: 263px; height: 13px; text-align: right">
                              Total Tanker Experience(Month):</td>
                          <td style="width: 190px; height: 13px; text-align: left">
                              <asp:TextBox ID="txtexperience" runat="server" CssClass="required_box" MaxLength="2" Width="36px"></asp:TextBox></td>
                          <td align="right" style="padding-right: 5px; width: 219px; height: 13px; text-align: right">
                          </td>
                          <td style="width: 31px; height: 13px; text-align: left">
                          </td>
                          <td style="width: 56px; height: 13px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; width: 263px; height: 13px; text-align: right">
                          </td>
                          <td style="width: 190px; height: 13px; text-align: left">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtexperience"
                                  ErrorMessage="Required" ValidationGroup="rr"></asp:RequiredFieldValidator></td>
                          <td align="right" style="padding-right: 5px; width: 219px; height: 13px; text-align: right">
                          </td>
                          <td style="width: 31px; height: 13px; text-align: left">
                          </td>
                          <td style="width: 56px; height: 13px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; width: 263px; height: 19px; text-align: right">
                              On Board Actual Rank Experience(Month):</td>
                          <td style="width: 190px; height: 19px; text-align: left">
                              <asp:TextBox ID="txtexperience1" runat="server" CssClass="required_box" MaxLength="2" Width="36px"></asp:TextBox></td>
                          <td align="right" style="padding-right: 5px; width: 219px; height: 19px; text-align: right">
                              Service With the Company(Month):</td>
                          <td style="width: 31px; height: 19px; text-align: left">
                              <asp:TextBox ID="txtexperience2" runat="server" CssClass="required_box" MaxLength="2" Width="36px"></asp:TextBox>
                              </td>
                          <td style="width: 56px; height: 19px; text-align: left">
                              <asp:Button ID="btanaddrank" runat="server" CssClass="btn" 
                    Text="Add Rank" Width="59px" OnClick="btanaddrank_Click" ValidationGroup="rr"/></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; width: 263px; text-align: right">
                          </td>
                          <td style="width: 190px; text-align: left">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtexperience1"
                                  ErrorMessage="Required" ValidationGroup="rr"></asp:RequiredFieldValidator></td>
                          <td align="right" style="padding-right: 5px; width: 219px; text-align: right">
                          </td>
                          <td style="width: 31px; height: 13px; text-align: left">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtexperience2"
                                  ErrorMessage="Required" ValidationGroup="rr"></asp:RequiredFieldValidator></td>
                          <td style="width: 56px; height: 13px; text-align: left">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:5px; width: 263px; height: 19px;">
                                                                    Created By:</td>
                                                                <td style="text-align: left; width: 190px; height: 19px;">
                                                                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="input_box" ReadOnly="True" Width="175px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:5px; width: 219px; height: 19px;">
                                                                    Created On:</td>
                                                                <td style="text-align: left; width: 31px; height: 19px;">
                                                                    <asp:TextBox ID="txtCreatedOn" runat="server" CssClass="input_box" ReadOnly="True" Width="175px"></asp:TextBox></td>
                                                                <td style="width: 56px; height: 19px; text-align: left">
                                                                </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 263px;">
                          </td>
                          <td style="text-align: left; width: 190px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 219px;">
                          </td>
                          <td style="text-align: left; width: 31px; height: 13px;">
                          </td>
                          <td style="width: 56px; height: 13px; text-align: left">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:5px; width: 263px;">
                                                                    Modified By:</td>
                                                                <td style="text-align: left; width: 190px;">
                                                                    <asp:TextBox ID="txtModifiedBy" runat="server" CssClass="input_box" ReadOnly="True" Width="175px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:5px; width: 219px;">
                                                                    Modified On:</td>
                                                                <td style="text-align: left; width: 31px; height: 13px;">
                                                                    <asp:TextBox ID="txtModifiedOn" runat="server" CssClass="input_box" ReadOnly="True" Width="175px"></asp:TextBox></td>
                                                                <td style="width: 56px; height: 13px; text-align: left">
                                                                </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 263px;" colspan="">
                          </td>
                          <td style="text-align: left; width: 190px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 5px; text-align: right; width: 219px;">
                          </td>
                          <td style="text-align: left; width: 31px; height: 13px;">
                          </td>
                          <td style="width: 56px; height: 13px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:5px; width: 263px; height: 18px;">
                              Status:</td>
                          <td style="text-align: left; height: 18px; width: 190px;">
                              <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" Width="180px" TabIndex="2">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right; height: 18px; width: 219px;">
                          </td>
                          <td style="text-align: left; height: 13px; width: 31px;">
                          </td>
                          <td style="width: 56px; height: 13px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; width: 263px; height: 18px; text-align: right">
                          </td>
                          <td style="height: 18px; text-align: left; width: 190px;">
                          </td>
                          <td align="right" style="height: 18px; text-align: right; width: 219px;">
                          </td>
                          <td style="height: 13px; text-align: left; width: 31px;">
                              </td>
                          <td style="width: 56px; height: 13px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="center" colspan="5" style="height: 19px; text-align: center">
                              <asp:GridView ID="gvdetails" GridLines="Horizontal" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="RankId" OnRowDeleting="gvdetails_RowDeleting"  >
                                  <Columns>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                                      <asp:BoundField DataField="RankName" HeaderText="Rank Name">
                                          <ItemStyle HorizontalAlign="Left" />
                                      </asp:BoundField>
                                      <asp:TemplateField HeaderText="Rank Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Rank1" runat="server" Text='<% #Eval("Rank1_Name") %>'></asp:Label>
                                            <asp:HiddenField ID="hfd_Rank1" runat="server" Value='<% #Eval("Rank1") %>' />
                                        </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Left" />
                                      </asp:TemplateField>
                                      <asp:BoundField DataField="Experience" HeaderText="Total Tanker Experience(Month)">
                                          <ItemStyle HorizontalAlign="Left" Width="150px" />
                                      </asp:BoundField>
                                        <asp:BoundField DataField="Experience1" HeaderText="On Board Actual Rank Experience(Month)">
                                          <ItemStyle HorizontalAlign="Left" Width="150px" />
                                      </asp:BoundField>  
                                      <asp:BoundField HeaderText="Service with the Company(Month)" DataField="Experience2">
                                          <ItemStyle HorizontalAlign="Left" Width="150px" />
                                      </asp:BoundField>
                                  </Columns>
                                  <RowStyle CssClass="rowstyle" />
                                  <SelectedRowStyle CssClass="selectedtowstyle" />
                                  <PagerStyle CssClass="pagerstyle" />
                                  <HeaderStyle CssClass="headerstylefixedheadergrid" />
                              </asp:GridView>
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
                       <asp:Button ID="btn_Add" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" TabIndex="3" OnClick="btn_Add_Click"/>
                            <asp:Button ID="btn_Save" runat="server" CssClass="btn" 
                    Text="Save" Width="59px"  TabIndex="4" OnClick="btn_Save_Click" />
                            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px"  CausesValidation="False" TabIndex="5" OnClick="btn_Cancel_Click" />
                            <asp:Button ID="btn_Print" runat="server" CausesValidation="False" CssClass="btn"  TabIndex="22" Text="Print" Width="59px"  Visible="False" />
                        </td>
                    </tr>
                </table>
                
                                                    
          </td>
         </tr>
        </table>
           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
               FilterType="Numbers" TargetControlID="txtexperience" >
           </ajaxToolkit:FilteredTextBoxExtender><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
               FilterType="Numbers" TargetControlID="txtexperience1" >
           </ajaxToolkit:FilteredTextBoxExtender>
           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
               FilterType="Numbers" TargetControlID="txtexperience2" >
           </ajaxToolkit:FilteredTextBoxExtender>
       </td>
      </tr>
     </table>    
    </div>
</asp:Content>

