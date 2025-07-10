<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_NavigationAudit.aspx.cs" Inherits="Edit_NavigationAudit" Title="Edit Navigation Audit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Circular Form</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="../Styles/tabs.css" rel="stylesheet" type="text/css" />
    <script src="../js/DivScroll.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        function CloseThisWindow()
        {
            this.close();
        }
        function RefereshParentPage()
        {
            window.opener.Reload();
        }
        function OpenPageReport() {
            var lastno = document.getElementById('lblNANo').innerHTML;            
            window.open('..\\Reports\\RPT_NavigationAudit.aspx?VNANo=' + lastno, '', 'title=no,toolbars=no,scrollbars=yes,width=1000,height=700,left=150,top=180,addressbar=no');
        }
    </script>
    <style type="text/css">
        .selectedrow
        {
            background-color : lightgray;
            color :White; 
            cursor:pointer;
        }
        .row
        {
            background-color : White;
            color :Black;
            cursor:pointer; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UP1"  runat="server" >
        <ContentTemplate>
            <div>
            <center>
              <table cellpadding="2" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center; background-color: #f9f9f9">
            <tr>
                <td style="background-color: #4371a5; text-align: center; height: 23px" class="text">
                    Navigation Audit : <asp:Label runat="server" ID="lblNANo"></asp:Label> 
                    <input type="button" value=" Print Full Report " onclick="OpenPageReport();" style="float:right; margin:5px; font-weight:bold;" class="btn" />
                </td>
            </tr>
            <tr>
            <td>
            </tr>
                  <tr>
                      <td>
                      <table style="width:100%">
                      <tr>
                      <td style="width:400px; height:470px;">
                      <table border="0" rules="rows"  cellpadding="2" cellspacing="0" style="border-collapse:collapse;" width="100%">
                              <tr style=" background-color:#4371a5; ">
                                  <td><b style="color:White">&nbsp;Vessel Observation</b></td>
                              </tr>
                          </table>
                      <div id="divNavigationAuditQUES"  style="width:100%; overflow-x:hidden; overflow-y:scroll;height:470px;" onscroll="SetScrollPos(this)">  <%--height:470px;"--%>
                      <table border="0" rules="rows" cellpadding="2" cellspacing="0" 
                              style="border-collapse:collapse;" width="100%">
                              <asp:Repeater ID="rptData" runat="server">
                                  <ItemTemplate>
                                      <tr class='<%#(Convert.ToInt32(Eval("QID"))!=SelectedQID)?"row":"selectedrow"%>' >
                                          <td style="width:100px; text-align:left;">
                                          <div style="height:20px; overflow:hidden; width:100%;">
                                              <asp:LinkButton runat="server" id="lblQ" Text='<%# "<b>"+ Eval("Qno").ToString() + "</b> : " + Eval("Deficiency").ToString()%>' CommandArgument='<%#Eval("QId")%>' OnClick="SelectQuestion"></asp:LinkButton>
                                          </div>
                                          </td>
                                      </tr>
                                  </ItemTemplate>
                              </asp:Repeater>
                          </table>
                      </div>
                      </td>
                      <td valign="top">
                        <table cellpadding="0" cellspacing="0" width="100%" rules="rows" >
                            <colgroup>
                                <col width="200px" />
                                <col />
                                <tr>
                                    <td>
                                        <b>Question NO :</b>
                                        <asp:Label ID="lblQNo" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <b>Comply : </b>
                                        <asp:Label ID="lblQComply" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <b>Question : </b>
                                        <asp:TextBox ID="lblQText" runat="server" Height="120px" ReadOnly="true" 
                                            style=" background-color:#d2d2d2; " TextMode="MultiLine" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <b>Vessel Observation/Remarks : </b>
                                        <asp:TextBox ID="lbldeficiency" runat="server" Height="120px" ReadOnly="true" 
                                            style=" background-color:#d2d2d2; " TextMode="MultiLine" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <b>Office Remarks : </b>
                                        <asp:TextBox ID="txtRemarks" runat="server" Height="120px" TextMode="MultiLine" 
                                            Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                        <table cellspacing="0" cellpadding="4" width="100%" >
                            <colgroup>
                                <col style="text-align:left" width="50%" />
                                <col style="text-align:right" width="50%" />
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMsgQuesUpdate" runat="server" ForeColor="red" 
                                            style="float:left; padding-left:5px;"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSaveQuesDetails" runat="server" CssClass="btn" 
                                            OnClick="btnSaveQuesDetails_OnClick" style="float:right; height:30px" 
                                            Text=" Update Remarks " />
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                      </td>
                      </tr>
                      </table>                         
                      </td>
                  </tr>
            </table>
                <div style=" font-family:Verdana; font-size:11px;">
                <b> : General Comments from Office:</b>
                <asp:TextBox runat="server" ID="txtGenComments" TextMode="MultiLine" Text='' Width="99%" Height="100px" style="border:solid 1px #59A6F1;"></asp:TextBox>
                <br />
                <table cellpadding="3" cellspacing="0" width="100%" style="margin:auto;">
                    <tr>
                        <td style="width:50%">&nbsp;
                            <asp:Label ID="lblVerifiedText" runat="server"></asp:Label>
                            <asp:Button runat="server" ID="btnVerify" Text="Verify" CssClass="btn" Width="73px" onclick="btnVerify_Click" style="float:right; height:30px"/>        
                        </td>
                        <td style="width:50%">&nbsp;
                            <asp:Label ID="lblClosureText" runat="server"></asp:Label>
                            <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="btn" Width="73px" onclick="btnClose_Click" OnClientClick="return confirm('Are you sure to close this navigation alert?')" style="float:right; height:30px"/>
                        </td>
                    </tr>
                    <tr>
                        
                    </tr>
                </table>
                <asp:Label runat="server" ID="lblMessage" ForeColor="Green"></asp:Label>
                </div>
            </td>
            </tr>
            </table>
                
            </center>                 
            </div>        
        </ContentTemplate>
     </asp:UpdatePanel>
    </form>
</body>
</html>
