<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Company.aspx.cs" Inherits="Registers_Company" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
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
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        
            <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Company "></asp:Label></td>
    </tr> 
        <tr>
            <td>
                <asp:Label ID="lbl_Company_Message" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;
                <asp:Label ID="lbl_report_message" runat="server" ForeColor="#C00000"></asp:Label></td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="companypanel" runat="server" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Company Details</strong></legend>
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
                                                     </td>
                                                 <td align="left" style="height: 19px">
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
                                                     <asp:DropDownList ID="ddstatus_company" runat="server" CssClass="input_box" Width="205px" TabIndex="13">
                                                     </asp:DropDownList></td>
                                                  <td align="right" style="padding-right: 15px; height: 20px">
                                                      Company Logo:</td>
                                                  <td align="left" style="height: 20px">
                                                     <asp:FileUpload ID="FileUpload_CompanyLogo" runat="server" CssClass="input_box" Width="200px" /></td>
                                                  <td align="right" style="padding-right: 15px; height: 20px">
                                                      Report Logo:</td>
                                                  <td align="left" style="height: 20px">
                                                      <asp:FileUpload ID="FileUpload_ReportLogo" runat="server" CssClass="input_box"
                                                          Width="204px" /></td>
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
