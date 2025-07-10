<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMS_AssessmentQuestion.aspx.cs" Inherits="SMS_AssessmentQuestion" %>

<%--<%@ Register src="ManualMenu.ascx" tagname="ManualMenu" tagprefix="uc2" %>--%>
<%@ Register Src="SMSManualMenu.ascx" TagName="SMSManualMenu" TagPrefix="uc3" %>
<%@ Register Src="SMSAdminSubTab.ascx" TagName="SMSAdminSubTab" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
      <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
    <link rel="Stylesheet" href="CSS/style.css" />    
    <script type="text/javascript" src="JS/Common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server" ID="ScriptManager1"></asp:ToolkitScriptManager>
        <uc3:SMSManualMenu ID="SMSManualMenu1" runat="server" />
    <uc4:SMSAdminSubTab ID="ManualMenu2" runat="server" />
    <asp:UpdatePanel ID="upMList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div style="border-bottom:solid 1px #eee; height:450px;">
            <table style="width:100%;margin-top:0px;" border="0" border-collapse:collapse;" cellpadding="0" cellspacing="0" >
                <tr>
                    <td style="width: 375px; vertical-align: top; border: solid 0px black; border-top: none;overflow: hidden;">
                        <div style="width: 375px; height: 449px; ">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <div style="width: 375px; height: 450px; ; border: solid 0px gray; border-right: none;overflow-x: hidden; overflow-y: scroll;" id="d1">
                                                <asp:Repeater runat="server" ID="rptManuals">
                                                    <ItemTemplate>
                                                        <div title='<%#Eval("ManualName")%>' style="width: 375px; margin:2px; background-color:#b3e2ff;color:#333;padding:5px;">
                                                            <span style="text-align:left;padding-left: 2px;"><asp:LinkButton runat="server" ID="lnkManual" CommandArgument='<%#Eval("ManualId")%>' Text='<%#Eval("ManualName")%>' OnClick="SelectManual" style="color:#333"></asp:LinkButton></span>
                                                            <span style="width: 50px; text-align: left; float:right;"><%#Eval("VersionNo")%></span>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="vertical-align:top;">
                        <div id="divContainer" runat="server" visible="false" style="">
                        <div style="padding:10px;background-color:#e5f5ff;">
                        <table width="100%" cellpadding="0" cellspacing="0" >
                            <colgroup>
                                <col />
                                <col width="200px" />
                                <tr>
                                    <td>
                                        <div style="font-size :15px; font-weight:bold;margin-left:20px;">
                                            <asp:Label ID="lblManualNameHeading" runat="server" ForeColor="#333"></asp:Label>
                                        </div>
                                    </td>
                                    <td style="text-align:right;">
                                        <asp:Button runat="server" ID="btnaddnew" Text=" + Add New " CssClass="btn" OnClick="btnAdd_Click" /> 
                                        <a ID="aListQuestion" runat="server" class="QuestList btn" href="" style="text-decoration:none;padding:7px 20px 7px 20px;" target="_blank">List</a>
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                        </div>

                        <%----------------------------------------------------------------------------------------------------%>

                        <div id="divQuesOneByOne" runat="server" style="border:solid 0px red;">
                        <div style="background-color:#ffffcc;">
                        <center>
                        <table cellpadding="2" border="0" >
                                    <colgroup>
                                        <col width="70px" />
                                        <col width="100px" />
                                        <col width="70px" />
                                        <tr>
                                            <td style="width:50px">
                                                <asp:Button ID="btnPrev" runat="server" class="btn" 
                                                    OnClick="btnPrev_OnClick" text="&lt; Prev" />
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblTotalQuestion" runat="server"></asp:Label>
                                            </td>
                                            <td style="width:50px">
                                                <asp:Button ID="btnNext" runat="server" class="btn" 
                                                    OnClick="btnNext_OnClick" text="Next &gt;" />
                                            </td>
                                        </tr>
                                    </colgroup>
                                    </table>
                        </center>
                        </div>
                        <table width="100%" >
                            <tr>
                            <td>
                                <div class="Question">
                                    <%--<span class="QuesNO" >15</span>                            --%>
                                    <asp:Label ID="lblQuestion" runat="server" Font-Bold="true" ></asp:Label>                            
                                </div>
                            </td>
                        </tr>    
                        </table>
                        <table width="100%" class="answers"  style="margin:10px;" >
                        
                        <tr>
                            <td id="tdOptionA" runat="server" style="width:50%">
                            <span class="Option">A</span>
                                <p>
                                    <asp:Label ID="lblOptionA" runat="server" ></asp:Label>
                                </p>
                            </td>
                            <td id="tdOptionB" runat="server">
                            <span class="Option">B</span>
                                <p>
                                    <asp:Label ID="lblOptionB" runat="server" ></asp:Label>
                                 </p>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdOptionC" runat="server">
                            <span class="Option">C</span>
                                <p>
                                    <asp:Label ID="lblOptionC" runat="server" ></asp:Label>
                                 </p>
                            </td>
                            <td id="tdOptionD" runat="server">
                            <span class="Option">D</span>
                                <p>
                                    <asp:Label ID="lblOptionD" runat="server" ></asp:Label>
                                </p>
                            </td>
                        </tr>                          
                        </table>
                        <div><b>Status : <asp:Label ID="lblStatus" runat="server" ></asp:Label> </b></div>
                        </div>
                    </div>
                    </td>
                </tr>
            </table>
        </div>
    <%-- Show Version ----------------------------------------------%>
    <div style="position:absolute;top:0px;left:0px; width:100%;" id="dvFormVersion" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px;height:100%;min-height: 100%;width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; margin:10px 10px 10px 10px; padding :0px; text-align :center;border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                <div style="border:solid 0px red;text-align:right;margin:5px 5px;">
                    <asp:Button ID="btnCloseQuestionList" runat="server" Text=" Close " CssClass="btn" OnClick="btnCloseQuestionList_OnClick" />
                </div>
                <div >
                    <iframe id="iframQuesList" runat="server" src="#" frameborder="0" height="500px" width="100%" scrolling="no"></iframe>
                </div>
                
            </div>
        </center>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

