<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssessmentQuestionList.aspx.cs"    Inherits="AssessmentQuestionList" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="SMSManualMenu.ascx" TagName="SMSManualMenu" TagPrefix="uc3" %>
<%@ Register Src="SMSAdminSubTab.ascx" TagName="SMSAdminSubTab" TagPrefix="uc4" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/quesiton.css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server" ID="ScriptManager1"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="upMList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <%--<div style="border:solid 1px red; height:450px;">--%>
            <div style=" padding:3px; background-color:#4371A5; text-align:center;">
                <b style="color: White; font-family:Arial; font-size:17px;">
                    <asp:Label ID="lblManualNameHeading" runat="server" style="font-size:20px;font-weight:bold;color:#$c2c2c;" ></asp:Label></b>
                </div>
                <%----------------------------------------------------------------------------------------%>
            
            <%----------------------------------------------------------------------------------------%>
            <div id="DivShowQuestion" runat="server"  >
                <div style=" padding:3px; text-align:center; padding:0px;">
                <table width="100%" border="0" cellpadding="7" cellspacing="0"  >
                    <tr style="background-color:#e2e2e2; font-weight:bold;" runat="server" id="trCopy" visible="false">
                        <td align="left">
                            Copy questions to assessment <asp:DropDownList runat="server" ID="dllCopyAssessment"></asp:DropDownList>                            
                            <asp:Button ID="btnCopyQuestion" runat="server" Text="Copy" OnClick="btnCopyQuestion_OnClick" OnClientClick="return confirm('Are you sure to copy selected questions?');" style="background-color:#222; padding:4px;color:White; width:80px;" />
                            <asp:Label ID="lblCompyMannualMsg" runat="server" style="color:Red;"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" >
                            Show questions for rank :
                <asp:DropDownList runat="server" ID="ddlRanks" AutoPostBack="true" OnSelectedIndexChanged="ddlRanks_OnSelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
                
            </div>    
                <asp:Repeater ID="rptQuestionList" runat="server" >
                        <ItemTemplate>
                            <div style="margin-top:1px">
                            <div style="width:100%;" class="Question <%#Eval("StatusClass")%>" > 
                                <table width="100%" border="0"  >
                                <col width="20px"  />
                                <col  />
                                <col width="60px"  />
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkSelectQuestion" runat="server" Checked="true" />
                                        <asp:HiddenField ID="hfSelectedQuesID" runat="server" Value='<%#Eval("QID")%>'/>
                                    </td>
                                    <td>
                                       <b style="font-size:18px"><%#Eval("RowNo")%>.</b> <asp:Label ID="rLblQuestion" runat="server" Text='<%#Eval("QuestionText")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="rLblStatus" runat="server" Text='<%#Eval("StatusText")%>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lnkEditQuestion" runat="server" Text="Edit" OnClick="lnkEditQuestion_OnClick" CommandArgument='<%#Eval("QID")%>' ></asp:LinkButton>
                                    </td>
                                </tr>                                            
                            </table> 
                            </div >
                            <div style="width:100%;border:solid 0px #E6E6E6;padding:0px;">
                                <table width="100%" border="0" >
                                                <col width="80px" />
                                                <col width="400px" />
                                                <col width="80px" />
                                                <col  />
                                                <tr>
                                                    <td ><b>A :</b></td>
                                                    <td>
                                                        <asp:Label ID="rLblOption1" runat="server" Text='<%#Eval("Option1")%>'></asp:Label>                             
                                                    </td>
                                                    <td style="color:#2E2E2E;"><b>B :</b></td>
                                                    <td>
                                                        <asp:Label ID="rLblOption2" runat="server" Text='<%#Eval("Option2")%>'></asp:Label>                                   
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="color:#2E2E2E;"><b>C :</b></td>
                                                    <td>
                                                        <asp:Label ID="rLblOption3" runat="server" Text='<%#Eval("Option3")%>'></asp:Label>
                                                    </td>
                                                    <td style="color:#2E2E2E;"><b>D :</b></td>
                                                    <td>
                                                        <asp:Label ID="rLblOption4" runat="server" Text='<%#Eval("Option4")%>'></asp:Label>                                   
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="padding:5px; color: maroon" >
                                                        <b>
                                                            <asp:HiddenField ID="hfAns" runat="server" Value='<%#Eval("Ans")%>' />
                                                            [ <asp:Label ID="rLblAns" runat="server" Text='<%#Eval("AnsText")%>'></asp:Label> ] is the correct Answer</b> 
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="padding:5px; color: maroon" >
                                                        <b>
                                                             Applicable for - <%#Eval("Ranks")%>
                                                        </td>
                                                </tr>
                                            </table>                                       
                            </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
            </div>
            <%----------------------------------------------------------------------------------------%>
            <div class="AddQuestion" id="DivAddQuestion" runat="server" visible="false" style="border:solid 1px #c2c2c2;margin:10px;padding:0px;">
            <table width="100%" border="0">
                <col width="250px" />
                <col  />
                <td>
                    <div style="height:400px;overflow-x:hidden;overflow-y:scroll;border:0px;">
                        <asp:CheckBoxList ID="chklistRank" runat="server" RepeatColumns="1" RepeatDirection="Vertical" ></asp:CheckBoxList> 
                    </div>
                </td>
                <td valign="top">
                    <table width="95%" style="margin:0 auto;" border="0" cellpadding="0" cellspacing="5">
                <col width="12%" />
                <col />
                <tr>
                    <td> <b> Question</b></td>
                    <td>
                        <asp:TextBox ID="txtQuesiton" runat="server" Width="75%" TextMode="MultiLine" Height="60px" ></asp:TextBox>
                    </td>                                
                </tr>
                <tr>
                    <td><b>Option 1</b></td>
                    <td>
                        <asp:TextBox ID="txtOption_1" runat="server" Width="50%" MaxLength="1000"></asp:TextBox>
                    </td>                                
                </tr>
                <tr>
                    <td><b>Option 2</b></td>
                    <td>
                        <asp:TextBox ID="txtOption_2" runat="server" Width="50%" MaxLength="1000"></asp:TextBox>
                    </td>                                
                </tr>
                <tr>
                    <td><b>Option 3</b></td>
                    <td>
                        <asp:TextBox ID="txtOption_3" runat="server" Width="50%" MaxLength="1000"></asp:TextBox>
                    </td>                                
                </tr>
                <tr>
                    <td><b>Option 4</b></td>
                    <td>
                        <asp:TextBox ID="txtOption_4" runat="server" Width="50%" MaxLength="1000"></asp:TextBox>
                    </td>                                
                </tr>
                <tr>
                    <td><b>Answer</b></td>
                    <td>
                        <asp:DropDownList ID="ddlAnswer" runat="server" Width="150px" >
                            <asp:ListItem Value="" Text="" ></asp:ListItem>
                            <asp:ListItem Value="1" Text=" Option 1" ></asp:ListItem>
                            <asp:ListItem Value="2" Text=" Option 2" ></asp:ListItem>
                            <asp:ListItem Value="3" Text=" Option 3" ></asp:ListItem>
                            <asp:ListItem Value="4" Text=" Option 4" ></asp:ListItem>
                        </asp:DropDownList>
                    </td>                                
                </tr>
                <tr>
                    <td> <b> Status</b></td>
                    <td>
                        <asp:CheckBox ID="chkStatus" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td> 
                        <asp:Button ID="btnSaveQuestionair" runat="server" OnClick="btnSaveQuestionair_OnClick" Text="Save" CssClass="btncls"/> 
                                    
                    </td>
                    <td>
                        <asp:Button ID="btnCancelSaveQuestion" runat="server" OnClick="btnCancelSaveQuestion_OnClick" Text="Close" CssClass="btncls" />     
                        <asp:Label  ID="lblMsg" runat="server" style="color:Red;" ></asp:Label>
                    </td>
                </tr>
            </table>        
                </td>
            </table>

            
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

