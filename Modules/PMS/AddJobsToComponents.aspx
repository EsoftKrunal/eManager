<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddJobsToComponents.aspx.cs" Inherits="AddJobsToComponents"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
      <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 232px;
        }
        .auto-style1 {
            width: 30px;
            height: 20px;
        }
        .auto-style2 {
            width: 500px;
            height: 20px;
        }
        .auto-style3 {
            width: 150px;
            height: 20px;
        }
        .auto-style4 {
            width: 12px;
            height: 20px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function selectAll(invoker) {
            var inputElements = document.getElementsByTagName('input');
            for (var i = 0; i < inputElements.length; i++) {
                if (i < 10) {
                    chkbox = document.getElementById('rptJobs' + '_ctl0' + i + '_chkSelect');
                }
                else {
                    chkbox = document.getElementById('rptJobs' + '_ctl' + i + '_chkSelect');
                }
                if (chkbox) {
                    chkbox.checked = invoker.checked;
                }
            }
        }
        function selectAllJobs(invoker) {
            var inputElements = document.getElementsByTagName('input');
            for (var i = 0; i < inputElements.length; i++) {
                if (i < 10) {
                    chkbox = document.getElementById('rptJobstoassign' + '_ctl0' + i + '_chkSelectJobs');
                }
                else {
                    chkbox = document.getElementById('rptJobstoassign' + '_ctl' + i + '_chkSelectJobs');
                }
                if (chkbox) {
                    chkbox.checked = invoker.checked;
                }
            }
        }
        function refreshonadd() {
            window.opener.reloadme();
        }
    </script>
    <script language="javascript" type="text/javascript">
       
        function DownLoadFile(FormId, FileName) {
            document.getElementById('hfFormID').value = FormId;
            document.getElementById('hfFileName').value = FileName;
            
            document.getElementById('btnDownLoadFile').click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center"  class="text headerband" >
                    
                    <asp:Label ID="lblPageTitle" runat="server" ></asp:Label><%--Add/Edit Jobs to Component--%></td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        
                        <asp:UpdatePanel runat="server" id="up1" >
                        <ContentTemplate>
                           <table cellpadding="0" cellspacing="0" width="100%">
                        <tr><td align="left" style="padding-left:5px;"></td></tr>
                        <tr>
                        <td align="left" style="padding-left:5px;">
                        <table width="100%" border="0">
                        <tr>
                        <td>
                        <table width="100%" >
                        <tr>
                        <td class="style1" style="width:50%"><asp:Label ID="lblComponent" runat="server" Font-Bold="true"></asp:Label></td>
                        <td style="padding-left:5px; text-align:right;width:50%"><span class="required">* Required Fields</span></td>
                        </tr>
                        </table>
                        </td>
                        </tr>
                        <tr>
                        <td id="tdJobs"  runat="server">
                            <table cellspacing="0" border="1" bordercolor="#c2c2c2" rules="rows" cellpadding="4" style="width: 100%; border-collapse: collapse;">
                                <tr>
                                    <td style="text-align: right">
                                        Job Cat. :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlJobType" runat="server" required='yes' AutoPostBack="True" OnSelectedIndexChanged="ddlJobType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">
                                        Department :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" required="yes">
                                        </asp:DropDownList>
                                        <span class="required">*</span></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        Job Schedule :</td>
                                    <td style="text-align: left">
                                        <asp:RadioButtonList ID="rblFixed" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Fixed" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Flexible" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td style="text-align: right">
                                        Interval Type :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="ddlIntervalType" runat="server" AutoPostBack="true" 
                                            OnSelectedIndexChanged="ddlIntervalType_SelectedIndexChanged" required="yes">
                                        </asp:DropDownList>
                                        <span class="required">*</span>
                                    </td>
                                </tr>
                                <tr >
                                    <td style="text-align: right">
                                        Primary Responsibility :&nbsp;
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlAssignTo" runat="server" required="yes">
                                        </asp:DropDownList>
                                        <span class="required">*</span></td>
                                    <td style="text-align: right;">
                                        Assigned Ranks :
                                    </td>
                                    <td style="text-align: left;">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align:left;padding-left:10px;">
                                                   <b> DECK </b>
                                                </td>
                                                <td style="text-align:left;padding-left:10px;">
                                                   <b> ENGINE </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBoxList ID="chkDeckRank" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" ></asp:CheckBoxList>
                                                </td>
                                                <td>
                                                     <asp:CheckBoxList ID="chkEngineRank" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" ></asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        &nbsp;</td>
                                </tr>
                                <%--<tr>
                                    <td style="text-align: right">
                                        Critical :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:CheckBox ID="chkIsCritical" Text="" runat="server" />
                                    </td>
                                </tr>--%>
                                <tr id="trIntParent" runat="server">
                                    <td style="text-align: right; width:165px;">
                                        Add Job to Sub Component :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:CheckBox ID="chkInhParent" runat="server" />
                                    </td>                                            
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        Short Desc. :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtDescrSh" runat="server" Height="151px" MaxLength="50" 
                                            required="yes" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                        <span class="required">*</span></td>
                                    <td style="text-align: right">
                                        Long Desc. :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtDescrM" runat="server" Height="151px" MaxLength="500" 
                                            TextMode="MultiLine" Width="350px"></asp:TextBox>
                                        &nbsp;</td>
                                </tr>
                                <tr runat="server" visible="false">
                                   <td style="text-align: right">
                                        &nbsp;</td>
                                   <td style="text-align: left">
                                       &nbsp;<span style="color: Gray;
                                                font-style: italic;">(Max 250 Characters)</span>
                                   </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        <span style="color: Gray; font-style: italic;">(Max 1000 Characters)</span></td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td style="text-align: right">
                                        Company&nbsp;Form :
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtAttachForm" runat="server" MaxLength="50" Width="350px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr runat="server" visible="false">
                                   <td style="text-align: right">
                                        Risk Assesment :
                                   </td>
                                   <td style="text-align: left">
                                     <asp:TextBox ID="txtRiskAssessment" runat="server" MaxLength="50" Width="350px"></asp:TextBox>
                                   </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                   <td style="text-align: right">
                                        Rating Required :
                                   </td>
                                   <td style="text-align: left">
                                     <asp:CheckBox ID="chkRatingReqd" runat="server" />
                                   </td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                        &nbsp;</td>
                                </tr>
                               <tr>
                                    <td colspan="4">
                                        <div style="width:100%;border:solid 1px gray;">
                                        <table cellpadding="2" cellspacing="" border="0" width="100%" style=" background-color:#c2c2c2; border-collapse:collapse"  >
                                            <tr class= "headerstylegrid">
                                                <td style="width:30px; text-align:center">Sr#</td>
                                                <td style="width:500px;">Description</td>
                                                <td style="width:150px;">File Name</td>
                                                <td style="width:12px;">&nbsp;</td>
                                            </tr>
                                        </table>
                                        </div>
                                        <div style="width:100%;height:150px;overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;">
                                        <table cellpadding="2" cellspacing="" border="0" width="100%" style="border-collapse:collapse"   >
                                            <asp:Repeater ID="rptFiles" runat="server" >
                                                
                                                <ItemTemplate>
                                                  <%--  <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
<ContentTemplate>--%>
                                                  
                                                    <tr>
                                                        <td style="width:30px; text-align:center">&nbsp;<%#Eval("Sno")%></td>
                                                        <td style="width:500px;">&nbsp;<%#Eval("Descr")%></td>
                                                        <td style="width:150px;">
                                                            <%--<a href='UploadFiles/AttachmentForm/<%#Eval("UpFileName")%>' target="_blank" ><%#Eval("UpFileName")%> </a>
                                                            <asp:LinkButton ID="lnlViewVersion" runat="server" Text=" Download " OnClick="lnlViewVersion_OnClick" CommandArgument='<%#Eval("FORMID")%>' ToolTip='<%#Eval("FileName")%>'></asp:LinkButton>--%>
                                                          <a onclick='DownLoadFile(<%#Eval("FORMID")%>,&#039;<%#Eval("FileName")%>&#039;)' style="cursor: pointer;">
                                                    <img src="Images/paperclip.gif" title=" Download " />
                                                </a>        
                                                        </td>  
                                                        <td style="width:12px;">&nbsp;</td>
                                                    </tr>
         <%--   </ContentTemplate>
                                                <Triggers>
                                                            <asp:PostBackTrigger ControlID="lnlViewVersion" />
                                                        </Triggers>
                                                   </asp:UpdatePanel>--%>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                        </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                    
                                        <a href="DocManagement.aspx?CJID=<%=CompJobId %>" target="_blank" style='display:<%=((CompJobId>0)?"block":"none") %> '> Attach Documents  </a>
                                        
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;
                                        <%--<asp:Button ID="btnAdd" runat="server" CssClass="btn"  Text="Add" onclick="btnAdd_Click" />--%>
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click" 
                                            Text="Save" />
                                        <%--<asp:Button ID="btnDelete" Text="Delete" runat="server" CssClass="btn" OnClientClick="javascript:return confirm('Are you sure to delete this Job?\n\rIt will also delete dependent data');" 
                                                Visible="false" onclick="btnDelete_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        </tr>
                        <tr><td><uc1:MessageBox ID="MessageBox1" runat="server" /></td>
                        <%--<td align="right"><asp:Button ID="btnAddJobs" Text="Save" CssClass="btn" runat="server"   onclick="btnAddJobs_Click" />
                            <asp:Button ID="btnEdit" Text="Save" CssClass="btn" runat="server" onclick="btnEdit_Click" /></td>--%></tr>
                        </table> 
                        </td>
                        </tr>                        
                        </table> 
                        </ContentTemplate>
                        <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                       
                        </Triggers>
                        </asp:UpdatePanel> 
                          <asp:ImageButton ID="btnDownLoadFile" runat="server" OnClick="btnDownLoadFile_OnClick" Style="display: none;" />
                         <asp:HiddenField ID="hfFormID" runat="server" Value="" />
                         <asp:HiddenField ID="hfFileName" runat="server" Value="" />
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
