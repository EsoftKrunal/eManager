<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseCertificate.aspx.cs" Inherits="Registers_CourseCertificate" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
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
        <%--<br />
        <br />--%>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Course Certificate "></asp:Label></td>
      </tr> 
            <tr>
                <td>
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;
                        text-align: center">
                        <legend><strong>Search</strong></legend>
                        <table cellpadding="0" cellspacing="0" style="padding-top: 3px" width="100%">
                            <tr>
                                <td style="width: 238px; height: 19px; text-align: left">
                                    &nbsp;Course Certificate Name :</td>
                                <td style="width: 709px; height: 19px; text-align: left">
                                    <asp:TextBox ID="txt_Course" runat="server" CssClass="input_box" MaxLength="30" Width="350px" onkeydown="javascript:if(event.keyCode==13){document.getElementById('ctl00$ContentPlaceHolder1$btn_Search').focus();}"></asp:TextBox>
                                </td>
                                <td style="width: 86px; height: 19px">
                                    <asp:Button ID="btn_Search" runat="server" CssClass="input_box" OnClick="btn_Search_Click"
                                        Text="Search" Width="62px" /></td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        <tr>
            <td>
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Course Certificate List</strong></legend>
                    <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="Gvcoursecertificate" runat="server" GridLines="Horizontal" AutoGenerateColumns="False" OnDataBound="Gvcoursecertificate_DataBound" OnPreRender="Gvcoursecertificate_PreRender" OnRowDeleting="Gvcoursecertificate_Row_Deleting" OnRowEditing="Gvcoursecertificate_Row_Editing" OnSelectedIndexChanged="Gvcoursecertificate_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCommand="Gvcoursecertificate_RowCommand">
                            <RowStyle CssClass="rowstyle" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <PagerStyle CssClass="pagerstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="30px" />
                                </asp:CommandField>
                               <%-- <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True">
                                    <ItemStyle Width="25px" />
                                </asp:CommandField>--%>
                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditCourseCertificate" CausesValidation="false" OnClick="btnEditCourseCertificate_click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("CourseCertificateId")%>' />
                                                <asp:HiddenField ID="hdnCourseCertificateId" runat="server" Value='<%#Eval("CourseCertificateId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="35px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                            Text="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course Type">
                                    <ItemStyle Width="80px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblcoursetype" runat="server" Text='<%#Eval("CourseType")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddencourseId" runat="server" Value='<%#Eval("CourseCertificateId")%>' />
                                         <asp:HiddenField ID="HiddencourseName" runat="server" Value='<%#Eval("CourseName")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="CourseName" HeaderText="Course">
                                    <ItemStyle HorizontalAlign="Left"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="OffCrew" HeaderText="Off Crew">
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OffGroup" HeaderText="Off Group">
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Expires" HeaderText="Expires">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mandatory" HeaderText="Mandatory">
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                    <ItemStyle HorizontalAlign="Left"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="StatusId" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblcourse_certificate" runat="server"></asp:Label><asp:Label ID="lbl_CourseCertificate_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    </fieldset>
            </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="coursecertificatepanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Course Certificate Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="6">
                                                     <asp:HiddenField ID="Hiddencoursepk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 95px;">
                                                     Course Type:</td>
                                                 <td align="left" style="height: 19px; width: 220px;">
                                                     <asp:TextBox ID="txtCourseType" runat="server" CssClass="input_box" MaxLength="24"
                                                         TabIndex="1" Width="180px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 87px;">
                                                     Course:</td>
                                                 <td align="left" style="height: 19px; width: 207px;">
                                                     <asp:TextBox ID="txtCourseName" runat="server" CssClass="required_box" MaxLength="49" TabIndex="2"
                                                         Width="276px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 61px;">
                                                     Off Crew:</td>
                                                 <td align="left" style="height: 19px; width: 141px;">
                                                     <asp:DropDownList ID="ddOffCrew" runat="server" CssClass="input_box" Width="103px" TabIndex="3">
                                                     </asp:DropDownList></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px; width: 95px;">
                                                  </td>
                                                  <td align="left" style="height: 13px; width: 220px;">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 13px; width: 87px;">
                                                  </td>
                                                  <td align="left" style="height: 13px; width: 207px;">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCourseName"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 13px; width: 61px;">
                                                  </td>
                                                  <td align="left" style="height: 13px; width: 141px;">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="height: 20px; padding-right:15px; width: 95px;">
                                                     Off Group:</td>
                                                 <td align="left" style="height: 20px; width: 220px;">
                                                     <asp:DropDownList ID="ddOffGroup" runat="server" CssClass="input_box" Width="183px" TabIndex="4">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 87px;">
                                                     Expires:</td>
                                                 <td align="left" style="height: 20px; width: 207px;">
                                                     <asp:CheckBox ID="Chkexpires" runat="server" TabIndex="5" /></td>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 61px;">
                                                     Mandatory:</td>
                                                 <td align="left" style="height: 20px; width: 141px;">
                                                     <asp:CheckBox ID="Chkmandatory" runat="server" TabIndex="6" Width="117px" /></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6" style="height: 13px">
                                                     &nbsp; &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 95px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px; width: 220px;">
                                                     <asp:TextBox ID="txtcreatedby" runat="server" CssClass="input_box" MaxLength="24" TabIndex="-1"
                                                         Width="180px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 87px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px; width: 207px;">
                                                     <asp:TextBox ID="txtcreatedon" runat="server" CssClass="input_box" MaxLength="24" TabIndex="-1"
                                                         Width="180px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="height: 20px; width: 61px;">
                                                     </td>
                                                 <td align="left" style="height: 20px; width: 141px;">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 95px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px; width: 220px;">
                                                     <asp:TextBox ID="txtmodifiedby" runat="server" CssClass="input_box" MaxLength="24" TabIndex="-1"
                                                         Width="180px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 87px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px; width: 207px;">
                                                     <asp:TextBox ID="txtmodifiedon" runat="server" CssClass="input_box" MaxLength="24" TabIndex="-1"
                                                         Width="180px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="height: 20px; width: 61px;">
                                                     </td>
                                                 <td align="left" style="height: 20px; width: 141px;">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px; width: 95px;">
                                                     Status:</td>
                                                 <td align="left" style="height: 20px; width: 220px;">
                                                     <asp:DropDownList ID="ddstatus" runat="server" CssClass="input_box" Width="183px" TabIndex="7">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="height: 20px; width: 87px;">
                                                     </td>
                                                 <td align="left" style="height: 20px; width: 207px;">
                                                     </td>
                                                 <td align="right" style="height: 20px; width: 61px;">
                                                     </td>
                                                 <td align="left" style="height: 20px; width: 141px;">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
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
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 18px">
                    <asp:Button ID="btn_course_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_course_add_Click" Text="Add" Width="59px" TabIndex="8" />
                    <asp:Button ID="btn_course_save" runat="server" CssClass="btn" OnClick="btn_course_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="9" />
                    <asp:Button ID="btn_course_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_course_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="10" />
                    <asp:Button ID="btn_Print_CourseCertificate" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_CourseCertificate_Click" TabIndex="11" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_coursecertificatepanel');" Visible="False" />                
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
