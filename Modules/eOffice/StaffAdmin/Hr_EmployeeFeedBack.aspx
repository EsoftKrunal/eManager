<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hr_EmployeeFeedBack.aspx.cs" Inherits="emtm_StaffAdmin_Emtm_Hr_EmployeeFeedBack" %>
<%@ Register src="HR_PersonalHeaderMenu.ascx" tagname="HR_PersonalHeaderMenu" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function openwindow(id) {

            window.open("PopupAttachment.aspx?FBTableId=" + id, "att", "");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up1">
        <ContentTemplate>
          <table width="100%">
                    <tr>
                       
                        <td valign="top" style="border:solid 1px #4371a5; height:500px;" >
                        <table width="100%" cellpadding="0" cellspacing="0">
                         <tr>
                            <td>
                            <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                                FeedBack : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                            </div>
                             <div>
                                <uc2:HR_PersonalHeaderMenu ID="Emtm_HR_PersonalHeaderMenu1" runat="server" /> 
                             </div> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                               <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;height:26px;">
                                <colgroup>
                                    <col style="width:25px;" />                                     
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col style="width:150px;" />     
                                    <col />
                                    <col style="width:175px;" />
                                    <col style="width:100px;" />
                                    <col style="width:25px;" />
                                    <tr align="left" class="blueheader">
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>Category</td>
                                        <td>
                                            FeedBack</td>
                                        <td>
                                            Entered By</td>
                                        <td>
                                            Entered On
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </colgroup>
                            </table>
                            </div>
                        <div id="dvFB" runat="server"  class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 370px; text-align:center;">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                   <col style="width:25px;" />                                     
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <col style="width:150px;" />     
                                    <col />
                                    <col style="width:175px;" />
                                    <col style="width:100px;" />
                                    <col style="width:25px;" />
                               </colgroup>
                                <asp:Repeater ID="rptFeedBack" runat="server">
                                    <ItemTemplate>
                                        <tr class='<%# (Common.CastAsInt32(Eval("TableId"))==TableId)?"selectedrow":"row"%>'>
                                            <td align="center">
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("TableId") %>'  
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnEdit_Click" ToolTip="Edit" />
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("TableId") %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                                    OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" 
                                                    OnClick="btnDelete_Click"
                                                    ToolTip="Delete" />
                                            </td>

                                            <td align="center">
                                                <asp:ImageButton ID="btnAttachment" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Eval("TableId") %>'  
                                                    ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="btnAttachment_Click" ToolTip="Show Attachment" Visible='<%# Eval("FileName").ToString() != "" %>' />
                                            </td>
                                             
                                            <td align="left">
                                                <%#Eval("Category1")%>
                                                <asp:HiddenField ID="hfCategory" Value='<%#Eval("Category")%>' runat="server" /> </td>
                                            <td align="left">
                                                <%#Eval("FeedBack1")%>
                                                <asp:HiddenField ID="hfFeedback" Value='<%#Eval("FeedBack")%>' runat="server" />
                                                </td>
                                            <td align="left">
                                                <%#Eval("EnteredBy")%>
                                                </td>
                                            <td align="center">
                                                <%#Eval("EnteredOn")%>
                                                </td>

                                            <td>&nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                              </table>  
                              </div>
                                <div id="divAddEdit" runat="server" visible="false">
                                    <fieldset style="height: 170px">
                                      <asp:UpdatePanel ID="UPEntry" runat="server">
                                      <ContentTemplate>
                                      
                                        <table width="100%" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td colspan="2">
                                                    <div class="dottedscrollbox" style="text-align: center; font-size: 14px; background-color: #4371a5;
                                                        color: White; padding: 3px; font-weight: bold;">
                                                        Enter FeedBack
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; font-weight: bold;">
                                                    Category :&nbsp;
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlCategory" required='yes' runat="server">
                                                        <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Positive" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Room for improvement" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Critical" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory"
                                                        InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; font-weight: bold;">
                                                    FeedBack :&nbsp;
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtFeedBack" MaxLength="1000" TextMode="MultiLine" required='yes'
                                                        runat="server" Height="74px" Width="950px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFeedBack"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; font-weight: bold;"> Attachment :&nbsp;</td>
                                                <td style="padding-right: 9px;">
                                                     <asp:FileUpload ID="FileUpload1" style="float:left;" runat="server" Width="119px" />
                                                    <asp:Button ID="btnCancel" Text="Cancel" CssClass="btn" style="float:right;" runat="server" CausesValidation="false"
                                                        OnClick="btnCancel_Click" />
                                                    <asp:Button ID="btnsave" Text="Save" CssClass="btn" style="float:right;" runat="server" OnClick="btnsave_Click" />
                                                </td>
                                            </tr>
                                        </table>

                                        </ContentTemplate>
                                        <Triggers  >
                                            <asp:PostBackTrigger ControlID="btnsave" />
                                        </Triggers>
                                      </asp:UpdatePanel>
                                    </fieldset>
                                </div>
                            
                            </td>
                        </tr>
                        <tr id="trAddNew" runat="server">
                           <td style="text-align:right; padding-right:2px;">
                               <asp:Button ID="btnAddNew" Text="Add New" CssClass="btn" runat="server" 
                                   onclick="btnAddNew_Click" Width="100px" />                           
                           </td>
                        
                        </tr>
                        </table>
                    </td>
                    </tr>
            </table>
            
        </ContentTemplate>
        
        </asp:UpdatePanel> 
        
    </div>
    </form>
</body>
</html>
