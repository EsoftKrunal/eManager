<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HR_TravelDocs.aspx.cs" Inherits="emtm_Emtm_TravelDocs" EnableEventValidation="false" %>


<%@ Register src="~/Modules/eOffice/StaffAdmin/HR_TravelDocumentHeader.ascx" tagname="HR_TravelDocumentHeader" tagprefix="uc2" %>

<%@ Register src="~/Modules/eOffice/StaffAdmin/HR_TravelDocument.ascx" tagname="HR_TravelDocument" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Travel Document</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional">
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
                        <div class="dottedscrollbox">
                            <uc2:HR_TravelDocumentHeader ID="Emtm_HR_TravelDocumentHeader1" 
                                runat="server" />
                        </div> 
                        
                        
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
                                    <col style="width:120px;" />
                                    <col style="width:100px;" />
                                    <col style="width:25px;" />
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
                                                Passport #</td>
                                            <td>
                                                Issue Date</td>
                                            <td>
                                                Expiry Date</td>
                                            <td>
                                                Place of Issue</td>
                                            <td>
                                                ECNR</td>
                                            <td>
                                            </td>
                                            <td>&nbsp;</td>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table> 
                             </div>  
                            
                        <div id="divTraveldoc" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 300px; text-align:center;">
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
                               <col style="width:120px;" />
                               <col style="width:100px;" />
                               <col style="width:25px;" />
                               <col style="width:25px;" />
                            </colgroup>
                            <asp:Repeater ID="RptTravelDoc" runat="server">
                                <ItemTemplate>
                                    <tr class='<%# (Common.CastAsInt32(Eval("TravelDocId"))==SelectedId)?"selectedrow":"row"%>'>
                                        <td align="center">
                                            <asp:ImageButton ID="btndocView" runat="server" CausesValidation="false" 
                                                CommandArgument='<%# Eval("TravelDocId") %>' ImageUrl="~/Modules/HRD/Images/HourGlass.gif" 
                                                OnClick="btndocView_Click" ToolTip="View" />
                                        </td>
                                        <% if (Session["EmpMode"].ToString() != "View")
                                       { %>
                                        <td align="center">
                                            <asp:ImageButton ID="btndocedit" runat="server" CausesValidation="false" 
                                                CommandArgument='<%# Eval("TravelDocId") %>' Visible="<%#auth.IsUpdate%>" ImageUrl="~/Modules/HRD/Images/edit.jpg" 
                                                OnClick="btndocedit_Click" ToolTip="Edit" />
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="btndocDelete" runat="server" CausesValidation="false" 
                                            CommandArgument='<%# Eval("TravelDocId") %>' Visible="<%#auth.IsDelete%>" ImageUrl="~/Modules/HRD/Images/delete.jpg" 
                                            OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"  
                                            OnClick="btndocDelete_Click" ToolTip="Delete" />
                                         </td>
                                        <%} %>
                                        <td align="left">
                                            <%#Eval("DocumentNo")%></td>
                                        <td align="center">
                                            <%#Eval("IssueDate")%></td>
                                        <td align="center">
                                            <%#Eval("ExpiryDate")%></td>
                                        <td align="left">
                                            <%#Eval("PlaceofIssue")%></td>
                                        <td align="left">
                                            <%#Eval("ECNR")%></td>
                                        <td align="left">
                                            <a ID="ancFile" runat="server" 
                                                href='<%#"PopupAttachment.aspx?TvlDocID=" + Eval("TravelDocId").ToString() %>' 
                                                target="_blank" title="Show Document" 
                                                visible='<%#Eval("FileName").ToString()!= "" %>'>
                                            <img src="../../Images/paperclip.gif" style="border:none" /></a></td>
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
                                   <tr>
                                       <td style="text-align :right">
                                           Passport # :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtDocno" runat="server" MaxLength="15" required="yes" ></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RfvDocno0" runat="server" 
                                               ControlToValidate="txtDocno" ErrorMessage="*"></asp:RequiredFieldValidator>
                                       </td>
                                       <td style="text-align :right">
                                           Place of Issue :</td>
                                       <td style="text-align :left">
                                           <asp:TextBox ID="txtPlaceofissue" runat="server" MaxLength="100" required="yes"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RfvPlaceofissue0" runat="server" 
                                               ControlToValidate="txtPlaceofissue" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                                           <asp:TextBox ID="txtIssuedate" runat="server" MaxLength="11" required="yes"></asp:TextBox>
                                           
                                           <asp:RequiredFieldValidator ID="RfvIssuedate" runat="server" 
                                               ControlToValidate="txtIssuedate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                           
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
                                           ECNR :</td>
                                       <td style="text-align :left">
                                           <asp:CheckBox ID="ChkECNR" runat="server" Text=""/>
                                       </td>
                                       <td style="text-align :right">
                                           Attach Document :</td>
                                       <td>
                                           <asp:FileUpload ID="fldocument" runat="server" />
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
            <asp:PostBackTrigger  ControlID ="btnsave"/>
         </Triggers> 
      </asp:UpdatePanel> 
    </div>
    </form>
</body>
</html>
