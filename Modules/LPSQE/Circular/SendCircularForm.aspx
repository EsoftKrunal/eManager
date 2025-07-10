<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendCircularForm.aspx.cs" Inherits="SendCircularForm" Title="Circular Form" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER </title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
   
    <script type="text/javascript">
        function CloseThisWindow()
        {
            this.close();
        }
        function RefereshParentPage()
        {
            window.opener.Reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager2" runat="server" AsyncPostBackTimeout="300" ></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="UP1"  runat="server" >
        <ContentTemplate>
            <div>
            
            <table cellpadding="2" cellspacing="2" border="0" width="1270px" rules="none" style="margin:auto;">
            <tr>
                <td style=" height: 23px;text-align :center; font-size:15px;" class="text headerband "  >
                    Circular Distribution
                    <asp:Label ID="lblCircularNumber" runat="server"></asp:Label>
                </td>
            </tr>
            <tr valign="top">
                <td >
                    <table id="tblInternal" runat="server" cellpadding="2" cellspacing="2" width="100%" style="border:solid 0px #9abcd7;" border="0"> 
                        
                        <tr valign="top">
                            <td style="width:350px;">
                               <b> Select Recipients  </b>
                               
                               <asp:Button ID="btnTempMail" runat="server" OnClick="btnTempMail_OnClick" Text="Send Temp Mail" Visible="false" />
                            </td>
                            <td valign="middle" style="width:35px;">
                                
                            </td>
                            <td>
                                <asp:Label ID="lblmsg" runat="server" style="color:Red;font-weight:bold;" ></asp:Label>      
                                <%--<asp:Button ID="btnAckRecv" runat="server" Text="Recieve Acknowledge" CssClass="btn" style="float:right;margin:2px;" OnClick="btnAckRecv_btnAckRecv" />--%>
                                
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Select All &nbsp;&nbsp;<asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_OnCheckedChanged" />
                                
                                <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" CssClass="btn" style="float:right; margin:2px;" OnClick="btnSendMail_btnAckRecv" />
                                
                                <div style="border:solid 1px #9abcd7; height:400px;overflow-y: scroll;overflow-x: hidden;"  >
                                    
                                    <asp:CheckBoxList ID="chkMail" runat="server" CssClass="input_box" style="cursor:pointer;width:390px; border:none;">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td >
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                  <ProgressTemplate>
                                      <img src="../../HRD/Images/progress_Icon.gif" />
                                  </ProgressTemplate>
                                 </asp:UpdateProgress>
                                <%--<asp:ImageButton ID="imgAdd" runat="server" ImageUrl="~/Images/right_24.png" OnClick="imgAdd_OnClick" />--%>
                            </td>
                            <td valign="top">                                
                                <table width="100%" cellpadding="3" cellspacing="0" border="1" style="border-collapse:collapse; margin-top:20px;" rules="all" >
                                        <colgroup>
                                            <col />
                                            <col width="150px" />
                                            <col width="80px" />
                                            <col width="80px" />
                                            <col width="100px" />
                                            <col width="100px" />
                                            <col width="17px" />
                                            <tr style="font-weight:bold;" class= "headerstylegrid">
                                                <td>Vessel</td>
                                                <td>Sent By</td>
                                                <td>Sent On</td>
                                                <td>Ack Recd</td>
                                                <td>Ack Recd By</td>
                                                <td>Ack Recd On</td>
                                                <td></td>
                                            </tr>
                                        </colgroup>
                                 </table>
                                 <div style="border:solid 1px #9abcd7;overflow-y: scroll;overflow-x: hidden;height:375px;">
                                    <table width="100%" cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" rules="all">
                                        <colgroup>
                                            <col />
                                            <col width="150px" />
                                            <col width="80px" />
                                            <col width="80px" />
                                            <col width="100px" />
                                            <col width="100px" />
                                        </colgroup>
                                    </table>
                                     <asp:Repeater ID="rptSendEmals" runat="server">
                                         <ItemTemplate>
                                             <tr>
                                                 <%--<td>
                                                        <asp:CheckBox ID="chkAck" runat="server" Visible='<%#Eval("Ack_rec").ToString()=="true"%>' />
                                                    </td>--%>
                                                 <td><%--<%#Eval("SendTo") %>--%><%#Eval("VesselEmail")%>
                                                     <asp:HiddenField ID="hfIntID" runat="server" Value='<%#Eval("IntID")%>' />
                                                     <asp:HiddenField ID="hfMailID" runat="server" Value='<%#Eval("SendTo")%>' />
                                                     <asp:HiddenField ID="hfCircularNumber" runat="server" Value='<%#Eval("CircularNumber")%>' />
                                                     <asp:HiddenField ID="hfFileName" runat="server" Value='<%#Eval("FileName")%>' />
                                                     <asp:HiddenField ID="hfTopic" runat="server" Value='<%#Eval("Topic")%>' />
                                                     <asp:HiddenField ID="hfSendBy" runat="server" Value='<%#Eval("SendBy")%>' />
                                                     <asp:HiddenField ID="hfComments" runat="server" Value='<%#Eval("Remarks")%>' />
                                                 </td>
                                                 <td><%#Eval("SendBy")%></td>
                                                 <td><%#Eval("SendOn")%></td>
                                                 <td style="text-align:center;">
                                                     <asp:CheckBox ID="chkAckReceice" runat="server" AutoPostBack="true" Checked='<%#Eval("Ack_rec").ToString().Trim()!="true"%>' Enabled='<%#Eval("Ack_rec").ToString().Trim()=="true"%>' OnCheckedChanged="chkAckReceice_OnCheckedChanged" style="float:left; padding-left:15px;" ToolTip="Click to Receive Acknowledge" />
                                                     <asp:ImageButton ID="imgViewComments" runat="server" ImageUrl="~/Images/magnifier.png" OnClick="imgViewComments_OnClick" style="float:right; padding-right:15px;" ToolTip="View Comments" Visible='<%#Eval("Ack_rec").ToString().Trim()!="true"%>' />
                                                 </td>
                                                 <td><%#Eval("Ack_by")%></td>
                                                 <td><%#Eval("Ack_on")%></td>
                                                 <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                             </tr>
                                         </ItemTemplate>
                                     </asp:Repeater>
                                     <table>
                                     </table>
                                </div>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">    
                    <%--<asp:Label ID="lblmsg" runat="server" style="color:Red;font-weight:bold;" ></asp:Label>--%>
                </td>
            </tr>
        </table>
            </div>       
            
            
            
            
            
            <div style="position:absolute;top:0px;left:0px; height :250px; width:100%;z-index:100;" runat="server" id="DivAckRecieve" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :400px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:180px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:150px;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td class="text headerband">
                            <b>Remarks</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" style="width:99%; height:100px;" CssClass="input_box" TextMode="MultiLine" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                            <asp:Button ID="btnRcvAckSave" runat="server" OnClick="btnRcvAckSave_OnClick" Text="Save" CssClass="btn" />
                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" Text="Cancel" CssClass="btn" Style="text-align:right;" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
     </div> 
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
