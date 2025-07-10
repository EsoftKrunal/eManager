<%@ Page Language="C#" AutoEventWireup="true" CodeFile="License.aspx.cs" Inherits="Registers_License" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
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
        <table cellspacing="0" cellpadding="0" width="100%">
             <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="License"></asp:Label></td>
    </tr> 
            <tr>
                <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-bottom:10px">
                    <legend><strong>Search</strong></legend>
                    <table width="100%" style="padding-top: 3px" cellpadding="0" cellspacing="0"><tr>
                    <td style=" text-align:left; width: 156px; height: 19px;">
                        &nbsp;Licence Name :</td>
                    <td style=" text-align:left; width: 794px; height: 19px;"><asp:TextBox ID="txt_Licence" runat="server" MaxLength="30" CssClass="input_box" Width="340px" onkeydown="javascript:if(event.keyCode==13){document.getElementById('ctl00$ContentPlaceHolder1$btn_Search').focus();}"></asp:TextBox> </td>
                    <td style="width: 86px; height: 19px"><asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="input_box" OnClick="btn_Search_Click" Width="62px" /></td>
                    </tr></table>
                    
                </fieldset>
                </td>
            </tr>
        <tr>
            <td style="width:100%">
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                    <legend><strong>License List</strong></legend>
                    <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvLicense" runat="server" AutoGenerateColumns="False" OnDataBound="GvLicense_DataBound"
                            OnPreRender="GvLicense_PreRender" OnRowDeleting="GvLicense_Row_Deleting" OnRowEditing="GvLicense_Row_Editing"
                            OnSelectedIndexChanged="GvLicense_SelectIndexChanged" Style="text-align: center" GridLines="Horizontal"
                            Width="98%" OnRowCommand="GvLicense_RowCommand">
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
                                    <ItemStyle Width="30px" />
                                </asp:CommandField>--%>
                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditLicense" CausesValidation="false" OnClick="btnEditLicense_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("LicenseId")%>' />
                                    <asp:HiddenField ID="hdnLicenseId" runat="server" Value='<%#Eval("LicenseId")%>' />
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
                                <asp:TemplateField HeaderText="License Type">
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLicensetype" runat="server" Text='<%#Eval("LicenseType")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenLicenseId" runat="server" Value='<%#Eval("LicenseId")%>' />
                                         <asp:HiddenField ID="HiddenLicenseName" runat="server" Value='<%#Eval("LicenseName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LicenseName" HeaderText="License">
                                    <ItemStyle HorizontalAlign="Left" Width="300px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="RankCode" HeaderText="Rank Code">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OffCrew" HeaderText="Off Crew">
                                    <ItemStyle HorizontalAlign="Left" Width="80px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="OffGroup" HeaderText="Off Group">
                                    <ItemStyle HorizontalAlign="Left" Width="20px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Expires" HeaderText="Expires">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="StatusId" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="left" Width="70px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblLicense" runat="server"></asp:Label><asp:Label ID="lbl_License_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    <asp:Label ID="Label2" runat="server" ForeColor="#C00000"></asp:Label></fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="Licensepanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>License Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="6">
                                                     <asp:HiddenField ID="HiddenLicensepk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     License Type:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtLicenseType" runat="server" CssClass="input_box" MaxLength="9" Width="185px" TabIndex="1"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     License:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtLicenseName" runat="server" CssClass="required_box" MaxLength="49" TabIndex="2"
                                                         Width="185px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Off Crew:</td>
                                                 <td align="left">
                                                     <asp:DropDownList ID="ddOffCrew_License" runat="server" CssClass="input_box" Width="190px" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddOffCrew_License_SelectedIndexChanged">
                                                     </asp:DropDownList></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      &nbsp;
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLicenseName"
                                                          ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Off Group:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddOffGroup_License" runat="server" CssClass="input_box" Width="190px" TabIndex="4" AutoPostBack="True" OnSelectedIndexChanged="ddOffGroup_License_SelectedIndexChanged">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Expires:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:CheckBox ID="Chkexpires_License" runat="server" TabIndex="5" /></td>
                                                 <td align="right" style="height: 20px; padding-right: 15px;">
                                                     Rank:</td>
                                                 <td align="left" style="height: 20px"><asp:DropDownList ID="ddl_Rank_License" runat="server" CssClass="input_box" Width="190px" TabIndex="6">
                                                 </asp:DropDownList></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
                                                     &nbsp; &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_license" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="185px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_license" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="185px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="height: 20px; padding-right: 15px;">
                                                     COC:</td>
                                                 <td align="left" style="height: 20px"><asp:CheckBox ID="chk_COC" runat="server" TabIndex="7" /></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedby_license" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="185px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_license" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="185px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="height: 20px">
                                                     </td>
                                                 <td align="left" style="height: 20px">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Status:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddstatus_license" runat="server" CssClass="input_box" Width="190px" TabIndex="8">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="height: 20px">
                                                     </td>
                                                 <td align="left" style="height: 20px">
                                                     </td>
                                                 <td align="right" style="height: 20px">
                                                     </td>
                                                 <td align="left" style="height: 20px">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 19px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                  </td>
                                                  <td align="right" style="height: 20px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                  </td>
                                                  <td align="right" style="height: 20px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
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
                    <asp:Button ID="btn_License_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_License_add_Click" Text="Add" Width="59px" TabIndex="9" />
                    <asp:Button ID="btn_License_save" runat="server" CssClass="btn" OnClick="btn_License_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="10" />
                    <asp:Button ID="btn_License_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_License_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="11" />
                    <asp:Button ID="btn_Print_License" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_License_Click" TabIndex="12" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Licensepanel');" Visible="False" />                                                 
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
