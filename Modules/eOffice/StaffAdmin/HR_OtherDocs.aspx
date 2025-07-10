<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_OtherDocs.aspx.cs" Inherits="Emtm_HR_OtherDocs" EnableEventValidation="false" %>

<%@ Register src="HR_TravelDocumentHeader.ascx" tagname="HR_TravelDocumentHeader" tagprefix="uc2" %>

<%@ Register src="HR_TravelDocument.ascx" tagname="HR_TravelDocument" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Other Documents</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
             <table width="100%">
                <tr>
                    
                    <td valign="top" style="border:solid 1px #4371a5; height:500px;">
                        <div class="dottedscrollbox" style=" text-align :left; font-size :14px; background-color:#4371a5; color :White; padding :3px; font-weight: bold;">
                            Documents : <asp:Label id="lbl_EmpName" Font-Italic="true" runat="server" Font-Size="Medium"></asp:Label>
                           </div>
                        <div class="dottedscrollbox">
                            <uc3:HR_TravelDocument ID="Emtm_HR_TravelDocument1" runat="server" />
                        </div>
                       <%-- <div id="divTraveldocument" runat="server" style="padding:5px 5px 5px 5px;" ></div>--%>
                        <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 26px ; text-align:center; border-bottom:none;">
                            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:26px;">
                                <colgroup>
                                    <col style="width:25px;" />
                                    <% if (Session["EmpMode"].ToString() != "View")
                               { %>
                                    <col style="width:25px;" />
                                    <col style="width:25px;" />
                                    <%} %>
                                    <col />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:100px;" />
                                    <col style="width:35px;" />
                                    <%--<col style="width:25px;" />--%>
                                    <col style="width:25px;" />
                                    <tr align="left" class="blueheader">
                                        <td>
                                        </td>
                                        <% if (Session["EmpMode"].ToString()!= "View")
                               { %>
                                        <td></td>
                                        <td></td>
                                        <%} %>
                                            <td>
                                                Document Name</td>
                                            <td>
                                                Document #</td>
                                            <td>
                                                Issue Date</td>
                                            <td>
                                                Expiry Date</td>
                                            <%--<td>
                                                Place Of Issue</td>--%>
                                            <td>
                                            </td>
                                            <td>&nbsp;</td>
                                        </td>
                                    </tr>
                               
                            </table>
                               
                            
                        <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 330px; text-align:center;">
                        <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                            <colgroup>
                                <col style="width:25px;" />
                                <% if (Session["EmpMode"].ToString() != "View")
                               { %>
                                <col style="width:25px;" />
                                <col style="width:25px;" />
                                <%} %>
                                <col />
                               <col style="width:100px;" />
                               <col style="width:100px;" />
                               <col style="width:100px;" />
                               <col style="width:35px;" />
                               <%--<col style="width:25px;" />--%>
                               <col style="width:25px;" />
                            </colgroup>
                            <asp:Repeater ID="RptMedicalDoc" runat="server">
                                <ItemTemplate>
                                    <tr class='<%# (Common.CastAsInt32(Eval("OtherDocId"))==SelectedId)?"selectedrow":"row"%>'>
                                        <td align="center">
                                            <asp:ImageButton ID="btndocView" runat="server" CausesValidation="false" 
                                                CommandArgument='<%# Eval("OtherDocId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" 
                                                OnClick="btndocView_Click" ToolTip="View" />
                                        </td>
                                        <% if (Session["EmpMode"].ToString() != "View")
                                       { %>
                                        <td align="center">
                                            <asp:ImageButton ID="btndocedit" runat="server" CausesValidation="false" 
                                                CommandArgument='<%# Eval("OtherDocId") %>' Visible="<%#auth.IsUpdate%>" ImageUrl="~/Modules/HRD/Images/edit.jpg" 
                                                OnClick="btndocedit_Click" ToolTip="Edit" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btndocDelete" runat="server" CausesValidation="false" 
                                            CommandArgument='<%# Eval("OtherDocId") %>'  Visible="<%#auth.IsDelete%>" ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                            OnClientClick="javascript:return window.confirm('Are you sure to delete?');"  
                                            OnClick="btndocDelete_Click" ToolTip="Delete" />
                                         </td>
                                        <%} %>
                                        <td align="left">
                                            <%#Eval("DocumentName")%></td>
                                        <td align="left">
                                            <%#Eval("DocumentNo")%></td>
                                        <td align="center">
                                            <%#Eval("IssueDate")%></td>
                                        <td align="center">
                                            <%#Eval("ExpiryDate")%></td>
                                        <%--<td align="center">
                                            <%#Eval("PlaceOfIssue")%></td>    --%>
                                        <td align="left">
                                            <a ID="ancFile" runat="server" 
                                                href='<%#"PopupAttachment.aspx?OtherDocId=" + Eval("OtherDocId").ToString() %>' 
                                                target="_blank" title="Show Document" 
                                                visible='<%#Eval("FileName").ToString()!= "" %>'>
                                            <img src="../../HRD/Images/paperclip12.gif" style="border:none" /></a></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <table>
                            </table>
                        </div> 
                        <br /> 
                           <table id="tblview" runat="server" width="100%" cellpadding="1" cellspacing ="0" border="0">
                               <colgroup>
                                   <col align="left" style="text-align :left" width="120px">
                                   <col />
                                   <col align="left" style="text-align :left" width="120px">
                                   <col />
                                       </colgroup>
                                   <tr>
                                       <td style="text-align :right">
                                           Document Name :</td>
                                       <td style="text-align :left">
                                       
                                           <%--<asp:DropDownList ID="ddlMedicalDocName" runat="server" Width="190px" required="yes"></asp:DropDownList> --%>
                                           <asp:TextBox ID="txtOtherDocName" runat="server" Width="300px" required="yes" MaxLength="100"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RfvDocName" runat="server" 
                                               ControlToValidate="txtOtherDocName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                           </td>
                                       <td style="text-align :right">
                                           Document # :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtDocumentNo" runat="server" MaxLength="30" ></asp:TextBox>
                                           <%--<asp:RequiredFieldValidator ID="RfvDocumentNo" runat="server" 
                                               ControlToValidate="txtDocumentNo" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align :right">
                                           &nbsp;</td>
                                       <td style="text-align :left">
                                           
                                       </td>
                                       <td style="text-align :right">
                                           &nbsp;</td>
                                       <td style="text-align :left">
                                           
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align :right">
                                           Issue Date :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtIssuedate" runat="server" MaxLength="11" ></asp:TextBox>
                                           
                                           <%--<asp:RequiredFieldValidator ID="RfvIssuedate" runat="server" 
                                               ControlToValidate="txtIssuedate" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                               
                                           <asp:ImageButton ID="imgIssuedate" runat="server" 
                                               ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       </td>
                                       <td style="text-align :right">
                                           Expiry date :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtExpirydate" runat="server" MaxLength="11"></asp:TextBox>
                                           <asp:ImageButton ID="imgExpirydate" runat="server" 
                                               ImageUrl="~/Modules/HRD/Images/Calendar.gif" OnClientClick="return false;" />
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align :right">
                                           &nbsp;</td>
                                       <td style="text-align :left">
                                           
                                       </td>
                                       <td style="text-align :right">
                                           &nbsp;</td>
                                       <td style="text-align :left">
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align :right">
                                           Attach Document :
                                       </td>
                                       <td style="text-align :left">
                                           <asp:FileUpload ID="fldocument" runat="server" />
                                       </td>
                                       <td style="text-align :right">
                                           </td>
                                       <td>
                                           
                                       </td>
                                   </tr>
                                   <tr>
                                       <td valign="top">
                                           &nbsp;</td>
                                       <td valign="top">
                                           &nbsp;</td>
                                       <td>
                                           &nbsp;</td>
                                       <td>
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                               Format="dd-MMM-yyyy" PopupButtonID="imgIssuedate" PopupPosition="TopLeft" 
                                               TargetControlID="txtIssuedate">
                                           </ajaxToolkit:CalendarExtender>
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                               Format="dd-MMM-yyyy" PopupButtonID="imgExpirydate" PopupPosition="TopLeft" 
                                               TargetControlID="txtExpirydate">
                                           </ajaxToolkit:CalendarExtender>
                                       </td>
                                   </tr>
                                   </col>
                                   </col>
                               
                            </table>
                            <table cellpadding="2" cellspacing ="0" border="0" width ="100%">
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnaddnew" CssClass="btn" runat="server" Text="Add New" 
                                        Width="80px" onclick="btnaddnew_Click" CausesValidation="false"></asp:Button>
                                    <asp:Button ID="btnsave" CssClass="btn"  runat="server" Text="Save" 
                                        onclick="btnsave_Click"></asp:Button>
                                    <asp:Button ID="btncancel" CssClass="btn"  runat="server" Text="Cancel" 
                                        onclick="btncancel_Click" CausesValidation="false"></asp:Button>
                                </td>
                            </tr>
                            </table>
                        </div> 
                    </td>
                </tr>
         </table> 
         </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnsave" />
        </Triggers> 
      </asp:UpdatePanel> 
    </div>
    </form>
</body>
</html>
