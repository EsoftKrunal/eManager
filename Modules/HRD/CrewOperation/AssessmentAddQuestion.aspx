<%@ Page Language="C#" AutoEventWireup="true" Title="Add New Question" CodeFile="AssessmentAddQuestion.aspx.cs" MasterPageFile="~/MasterPage.master"  Inherits="AssessmentAddQuestion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <link rel="Stylesheet" href="CSS/style.css" />
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
        }
        .style2
        {
            width: 84px;
        }
    </style>
    </asp:Content>
<%--<%@ Register Src="SMSManualMenu.ascx" TagName="SMSManualMenu" TagPrefix="uc3" %>
<%@ Register Src="SMSAdminSubTab.ascx" TagName="SMSAdminSubTab" TagPrefix="uc4" %>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">

    <asp:ToolkitScriptManager runat="server" ID="ScriptManager1"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="upMList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <%--<div style="border:solid 1px red; height:450px;">--%>
            <div style=" padding:3px; background-color:#4371A5; text-align:center;" 
                class="style1">
                Add New Question</div>
                <table style="width:95%">
                <tr>
                <td>
                    <strong>Ranks</strong></td>
                <td>
                    &nbsp;</td>
                </tr>
                    <tr>
                        <td style="vertical-align:top; width:250px;">
                        <div style="height:300px; overflow-y:scroll; overflow-x:hidden; border:solid 1px #c2c2c2">
                            <asp:CheckBoxList ID="chklistRank" runat="server" RepeatColumns="1" 
                                RepeatDirection="Vertical">
                            </asp:CheckBoxList>
                            </div>
                        </td>
                        <td>
                            <table border="0" cellpadding="5" cellspacing="5" style="margin:0 auto;"width="95%">
                                <colgroup>
                                    <col width="12%" />
                                    <col />
                                    <tr>
                                        <td class="style2">
                                            <b>Question</b></td>
                                        <td>
                                            <asp:TextBox ID="txtQuesiton" runat="server" Height="60px" TextMode="MultiLine" 
                                                Width="75%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <b>A</b></td>
                                        <td>
                                            <asp:TextBox ID="txtOption_1" runat="server" MaxLength="1000" Width="50%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <strong>B</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOption_2" runat="server" MaxLength="1000" Width="50%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <strong>C</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOption_3" runat="server" MaxLength="1000" Width="50%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <strong>D</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOption_4" runat="server" MaxLength="1000" Width="50%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <b>Answer</b></td>
                                        <td>
                                            <asp:DropDownList ID="ddlAnswer" runat="server" Width="150px">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text=" A " Value="1"></asp:ListItem>
                                                <asp:ListItem Text=" B " Value="2"></asp:ListItem>
                                                <asp:ListItem Text=" C " Value="3"></asp:ListItem>
                                                <asp:ListItem Text=" D " Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <b>Status</b></td>
                                        <td>
                                            <asp:CheckBox ID="chkStatus" Checked="true" runat="server" Text=" Active" />
                                        </td>
                                    </tr>
                                   
                                </colgroup>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left">
                            <asp:Button ID="btnSaveQuestionair" runat="server" CssClass="btncls" 
                                OnClick="btnSaveQuestionair_OnClick" Text="Save" />
                            &nbsp;
                            <asp:Button ID="btnCancelSaveQuestion" runat="server" CssClass="btncls" Text="Close" OnClientClick="javascript:window.close();" />
                        </td>
                        <td style="text-align:left">
                            <asp:Label ID="lblMsg" runat="server" style="color:Red;"></asp:Label>
                        </td>
                    </tr>
                    </tr>
                </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
     </asp:Content>

