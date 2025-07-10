<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditCheckListPopUp.aspx.cs" Inherits="AddEditCheckListPopUp" Title="Add/Edit CheckList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Add/Edit CheckList</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td style="background-color:#4371a5; height:23px" class="text">  Add/Edit CheckList</td>
            </tr>
            <tr>
                <td>
                <table border="0" cellpadding="3" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td style="height: 15px">
                          &nbsp;</td>
                      <td colspan="5" style="height: 15px">
                          <asp:HiddenField ID="HiddenInspectionCheckList" runat="server" />
                      </td>
                    </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              &nbsp;</td>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Inspection Group:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddl_InspGroup" runat="server" CssClass="input_box" 
                                  Width="250px" TabIndex="1" AutoPostBack="True" 
                                  OnSelectedIndexChanged="ddl_InspGroup_SelectedIndexChanged">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Chapter:</td>
                          <td style="text-align: left;"><asp:DropDownList ID="ddlChapterName" runat="server" 
                                  CssClass="input_box" Width="250px" TabIndex="2" AutoPostBack="True" 
                                  OnSelectedIndexChanged="ddlChapterName_SelectedIndexChanged">
                          </asp:DropDownList></td>
                          <td style="text-align: left;">
                              &nbsp;</td>
                      </tr>
                      
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px;">
                              &nbsp; &nbsp;</td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px;">
                              Sub Chapter:</td>
                          <td style="text-align: left; height: 3px;">
                              <asp:DropDownList ID="ddlSubChapter" runat="server" CssClass="input_box" 
                                  Width="250px" TabIndex="3" AutoPostBack="True">
                          </asp:DropDownList>
                              </td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px;">
                              Vessel Type:</td>
                          <td style="text-align: left; height: 3px;">
                              <asp:DropDownList ID="ddlVesselType" runat="server" CssClass="input_box" 
                                  Width="250px" TabIndex="4" AutoPostBack="True" >
                          </asp:DropDownList>
                              </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      
                      
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              &nbsp;</td>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Question Type:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddlQuestionType" runat="server" CssClass="input_box" 
                                  TabIndex="5" Width="250px">
                                  <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                                  <asp:ListItem Value="3">Desirable</asp:ListItem>
                                  <asp:ListItem Value="2">Recommended</asp:ListItem>
                                  <asp:ListItem Value="1">Statutory</asp:ListItem>
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Question#:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtQuestionNo" runat="server" CssClass="input_box" TabIndex="6"
                                  Width="245px" Enabled="False" MaxLength="49"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorAccountHeadName" runat="server"
                                  ControlToValidate="txtQuestionNo" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td style="text-align: left">
                              &nbsp;</td>
                      </tr>
                     
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right;">
                              &nbsp;</td>
                          <td align="right" style="padding-right: 15px; text-align: right;">
                              Ref#:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtRefNo" runat="server" CssClass="input_box" TabIndex="7"
                                  Width="245px" Enabled="False" MaxLength="49"></asp:TextBox></td>
                          <td align="right" style="padding-right: 15px; text-align: right;">
                              Deficiency Code:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtDeficiencyCode" runat="server" CssClass="input_box" TabIndex="8"
                                  Width="245px" Enabled="False" MaxLength="99"></asp:TextBox></td>
                          <td style="text-align: left">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right;">
                              &nbsp;</td>
                          <td align="right" style="padding-right: 15px; text-align: right;">
                              Sort Order:</td>
                          <td style="text-align: left;">
                              <asp:TextBox id="txtSortOrder" runat="server" CssClass="input_box" tabIndex="9" Width="245px" Enabled="False"></asp:TextBox></td>
                          <td align="right" style="padding-right: 15px; text-align: right;">
                              Version:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddlVersions" runat="server" CssClass="input_box" Width="250px" TabIndex="4" ></asp:DropDownList>
                              <asp:CompareValidator ID="CompareValidator1" runat ="server" ControlToValidate="ddlVersions" ValueToCompare="0" ErrorMessage="Required." Operator="NotEqual" ></asp:CompareValidator> 
                              </td>
                          <td style="text-align: left">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 3px; text-align: right">
                             </td>
                          <td align="right" style="padding-right: 15px; height: 3px; text-align: right">Shell Score
                           </td>
                          </td>
                          <td style="height: 3px; text-align: left"> <asp:TextBox id="txtShellScore" runat="server" CssClass="input_box" tabIndex="9" Width="245px" ></asp:TextBox>
                          </td>
                          <td align="right" style="padding-right: 15px; height: 3px; text-align: right">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 3px; text-align: center">
                              &nbsp;</td>
                          <td align="right" style="padding-right: 15px; height: 3px; text-align: center" 
                              colspan="4">
                              <asp:Label ID="lbl_InspectionCheckList_Message" runat="server" ForeColor="#C00000"></asp:Label>
                          </td>
                          <td style="height: 3px; text-align: left">
                              &nbsp;</td>
                      </tr>
                      <tr>
                      <td>
                          &nbsp;</td>
                      <td colspan=5>
                      <table border=0 width=100% cellpadding="0" cellspacing="0">
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right" valign="top">
                              Question:</td>
                          <td colspan="4" style="padding-right: 5px; text-align: left">
                              <asp:TextBox ID="txtQuestion" runat="server" CssClass="input_box" TabIndex="10"
                                  Width="726px" Enabled="False" MaxLength="199" TextMode="MultiLine" Height="59px"></asp:TextBox></td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px;">
                          </td>
                          <td style="text-align: left; height: 3px;">
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px;">
                          </td>
                          <td style="text-align: left; height: 3px;">
                          </td>
                          <td style="height: 3px; text-align: left;">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right" valign="top">Description :</td>
                          <td colspan="4" style="text-align: left">
                              <asp:TextBox ID="txtDescription" runat="server" CssClass="input_box" TabIndex="11" TextMode="MultiLine" Width="726px" Enabled="False" Height="59px"></asp:TextBox></td>
                          <td style="padding-right:27px; text-align: right" valign="bottom">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right" valign="top">&nbsp;</td>
                          <td colspan="4" style="text-align: left">
                              &nbsp;</td>
                          <td style="padding-right:27px; text-align: right" valign="bottom">
                              &nbsp;</td>
                      </tr>
                            </table></td>
                      </tr>
                                                  </table>
                                                    
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: right; padding-right: 17px;">
                        <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left: 0">
                          <asp:Button ID="btn_New_InspectionCheckList" runat="server" CssClass="btn" Text="New" Width="59px" CausesValidation="False" TabIndex="13" OnClick="btn_New_InspectionCheckList_Click" />
                          &nbsp;<asp:Button ID="btn_Save_InspectionCheckList" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="14" OnClick="btn_Save_InspectionCheckList_Click" Enabled="False" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; padding-right: 17px;">
                            &nbsp;</td>
                    </tr>
                </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenField_ChapId" runat="server" />
    </div>
    </form>
</body>
</html>
