<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FollowUpPopUp.aspx.cs" Inherits="Transactions_FollowUpPopUp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Response/Mgmt. Comment</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="text-align: center; margin:10px;">
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
     <div>
        <table cellpadding="0" cellspacing="0" width='100%' style=' border:solid 1px #4371a5'>
            <tr>
                <td style="background-color:#4371a5; height:23px; text-align :center " class="text">Inspection FollowUp</td>
            </tr>
            <tr>
                <td>
                <fieldset style="border-right: #8fafdb 1px solid; padding-right: 5px; border-top: #8fafdb 1px solid;padding-left: 5px; padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 0px;border-bottom: #8fafdb 1px solid; text-align: center">
                    <table width="100%" cellpadding="3" cellspacing="0">
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;" >
                                Cause:</td>
                            <td style="text-align: left; width: 845px;">
                                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; width: 618px;">
                                    <asp:CheckBoxList ID="rdbflaws" runat="server" RepeatDirection="Horizontal" Width="493px">
                                        <asp:ListItem>People</asp:ListItem>
                                        <asp:ListItem>Process</asp:ListItem>
                                        <asp:ListItem>Equipment</asp:ListItem>
                                    </asp:CheckBoxList>
                                    </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; width: 14%; text-align: right" valign="top">
                                Deficiency:</td>
                            <td style="width: 845px; text-align: left">
                                <asp:TextBox id="txt_Observation" runat="server" CssClass="input_box" Height="41px"
                                    TextMode="MultiLine" Width="616px">
                                </asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;" valign="top">
                                Corrective Actions:</td>
                            <td style="text-align: left; width: 845px;">
                                <asp:TextBox ID="txtcorrective" runat="server" CssClass="input_box" Width="616px" Height="41px" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;">
                                Target Closer Dt:</td>
                            <td style="text-align: left; width: 845px;" valign="top">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                <asp:TextBox ID="txttargetclosedt" runat="server" CssClass="input_box" Width="122px"></asp:TextBox></td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</td>
                                        <td>
                                            Responsibility:</td>
                                        <td colspan="2">
                                            <asp:CheckBoxList id="chklst_Responsibility" runat="server" RepeatDirection="Horizontal"
                                                Width="162px">
                                                <asp:ListItem>Vessel</asp:ListItem>
                                                <asp:ListItem>Office</asp:ListItem>
                                            </asp:CheckBoxList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;">
                                Closed:</td>
                            <td style="width: 845px; text-align: left">
                                <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                <td>
                                    <asp:RadioButtonList ID="rdbclosed" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                <td>
                                <%--Closure Evidence :--%>
                                <asp:Button ID="btnColsureEvidence" runat="server" Text="Upload/View Document" OnClick="btnColsureEvidence_OnClick"  CssClass="input_box" Width="150px" />
                                </td>
                                <td style="display:none;"> 
                                <ajaxToolkit:AsyncFileUpload OnClientUploadError="uploadError"
                                    OnClientUploadComplete="uploadComplete" runat="server"
                                    ID="flp_COCUpload" Width="300px" UploaderStyle="Modern"
                                    UploadingBackColor="#CCFFFF" ThrobberID="myThrobber" Visible=false/>
                                    <a runat="server" target="_blank"  id="a_file">
                                    <img style=" border:none;display:none;"  src="../Images/paperclip.gif" alt="Attachment" /> </a>
                                    </td>
                                </tr> 
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;">
                                Closed Dt:</td>
                            <td style="width: 845px; text-align: left">
                                <asp:TextBox ID="txtcloseddt" runat="server" CssClass="input_box" Width="122px"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;" valign="top">
                                Remarks:</td>
                            <td style="width: 845px; text-align: left">
                                <asp:TextBox ID="txtremark" runat="server" CssClass="input_box" Height="48px" MaxLength="250"
                                    Rows="10" TextMode="MultiLine" Width="616px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;" valign="top">
                        Created By:</td>
                            <td style="width: 845px; text-align: left">
                        <asp:TextBox ID="txtCreatedBy_DocumentType" runat="server" BackColor="Gainsboro"
                            CssClass="input_box" ReadOnly="True" TabIndex="-1" Width="154px"></asp:TextBox>&nbsp;
                        Created On:<asp:TextBox ID="txtCreatedOn_DocumentType" runat="server" BackColor="Gainsboro"
                            CssClass="input_box" ReadOnly="True" TabIndex="-2" Width="79px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;" valign="top">
                        Modified By:</td>
                            <td style="width: 845px; text-align: left">
                        <asp:TextBox ID="txtModifiedBy_DocumentType" runat="server" BackColor="Gainsboro"
                            CssClass="input_box" ReadOnly="True" TabIndex="-3" Width="154px"></asp:TextBox>&nbsp;
                        Modified On:<asp:TextBox ID="txtModifiedOn_DocumentType" runat="server" BackColor="Gainsboro"
                            CssClass="input_box" ReadOnly="True" TabIndex="-4" Width="82px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;" valign="top">
                                &nbsp;</td>
                            <td style="width: 845px; text-align: left">
                    <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; text-align: right; width: 14%;" valign="top">
                                &nbsp;</td>
                            <td style="width: 845px; text-align: left">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click" Width="59px" />&nbsp;
                                <asp:Button id="btnNotify" runat="server" CssClass="btn" Text="Notify" Width="59px" OnClick="btnNotify_Click" Enabled="False" />
                                &nbsp;<asp:Button id="btn_Print" runat="server" CssClass="btn" Text="Print" Width="59px" OnClientClick="return PrintDefRPT();" /></td>
                        </tr>
                    </table>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txttargetclosedt"> </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtcloseddt"> </ajaxToolkit:CalendarExtender>
            
            <div id="DivClosureDocs"  style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:350px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <fieldset style="width:99%;" >
                                <legend style="font-weight: bold">Documents</legend>
                                 <table  id="tblDocument" runat="server"  border="0" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                            <col />
                                            <col width="250px"/>
                                            <tr style="font-weight:bold;">
                                                <td style="text-align:left">
                                                    Description :</td>
                                                <td align="right">
                                                    Attachment</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:left">
                                                    <asp:TextBox ID="txt_Desc" runat="server" CssClass="input_box" MaxLength="50" 
                                                        Width="550px"></asp:TextBox>
                                                </td>
                                                <td style="text-align:left">
                                                    <asp:FileUpload ID="flAttachDocs" runat="server" CssClass="input_box" 
                                                        Width="200px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align:right">
                                                    <asp:Label ID="lblMsgDoc" runat="server" ForeColor="Red"></asp:Label>
                                                    <asp:Button ID="btnSaveDoc" runat="server" CausesValidation="true" 
                                                        CssClass="btn" onclick="btnSaveDoc_Click" Text="Save Document" />
                                                    <%--<asp:Button ID="btnCancelDoc" Text="Cancel" CssClass="btn" runat="server" 
                                             CausesValidation="true" onclick="btnCancelDoc_Click" />--%>
                                                </td>
                                            </tr>
                                        </colgroup>
                                 </table>
                                 <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;margin-top:10px;">
                                    <colgroup>
                                        <col style="width: 30px;" />
                                        <col style="width: 50px;" />
                                        <col />
                                        <col style="width: 150px;" />
                                        <col style="width: 100px;"/>
                                        <col style="width: 50px;"/>
                                        <col style="width: 17px;" />
                                        <tr align="center" class="headerstyle" style="font-weight:bold;">                                            
                                            <td>Edit</td>
                                            <td>Delete</td>
                                            <td align="left">
                                                Description
                                            </td>
                                            <td>
                                                Uploaded By
                                            </td>
                                            <td>
                                                Uploaded On
                                            </td>
                                            <td style="text-align:center;">    
                                                <img src="../Images/paperclip.gif" style="border:none"  />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </colgroup>
                                </table>
                                
                                <div id="dvDocs" style="overflow-y: scroll;overflow-x: hidden; width: 100%; height: 192px; text-align: center;">
                                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;                                       border-collapse: collapse;">
                                        <colgroup>
                                            <col style="width: 30px;" />
                                            <col style="width: 50px;" />
                                            <col />
                                            <col style="width: 150px;" />
                                            <col style="width: 100px;"/>
                                            <col style="width: 50px;"/>
                                            
                                        </colgroup>
                                        <asp:Repeater ID="rptDocs" runat="server">
                                            <ItemTemplate>
                                                <tr class="row">
                                                    <td style="text-align:center;">    
                                                        <asp:ImageButton ID="imgEditDoc" runat="server" ImageUrl="~/Images/edit.jpg" OnClick="imgEditDoc_OnClick" />  <%--Visible='<%#(Mode!="V") %>' --%>
                                                        <asp:HiddenField ID="hfDocID" runat="server" Value='<%#Eval("DocID") %>' />
                                                    </td>
                                                    <td style="text-align:center;">    
                                                        <asp:ImageButton ID="imgDelDoc" runat="server" ImageUrl="~/Images/delete.jpg" OnClick="imgDelDoc_OnClick" OnClientClick="return confirm('Are you sure to delete?')" />  <%--Visible='<%#(Mode!="V") %>'--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDesc" runat="server" Text='<%#Eval("Description")%>'></asp:Label>                                                        
                                                    </td>
                                                    <td align="center">
                                                      <%#Eval("UploadedBy")%>
                                                    </td>
                                                    <td align="right"> 
                                                       <%#Eval("UploadedDate")%>
                                                    </td>
                                                    <td style="text-align:center;">    
                                                        <a runat="server" ID="ancdoc"  href='<%#"~/UserUploadedDocuments\\Observation\\" + Eval("FileName").ToString() %>' target="_blank"  title="Show Doc" visible='<%#Eval("FileName")!=""%>' >
                                                         
                                                       <img src="../Images/paperclip.gif" style="border:none"  /></a>  
                                                    </td>
                                                    <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        
                                    </table>
                                </div>
                                
                                
                                            
                            </fieldset>
                        </td>
                    </tr>
                    
                </table>
                <asp:Button ID="btnCloseDocuments" runat="server" Text="Close" CssClass="input_box" OnClick="btnCloseDocuments_OnClick"  style="float:right;width:80px; margin-right:3px;" />
            </div>
            
        </center>
       
     </div>
            </fieldset>
                </td>
            </tr>
            </table>    
    </div>
    </form>
</body>
</html>
