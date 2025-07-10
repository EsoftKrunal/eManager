<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspectionObservationPopUp.aspx.cs" Inherits="Transactions_InspectionObservation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>InspectionObservation</title>
 <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    </head> 
<body style="text-align: center">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
    <triggers><asp:PostBackTrigger ControlID="btn_ImportCheckList"/></triggers>
        <contenttemplate>
<asp:Panel id="pnl_Observation" runat="server">

<TABLE style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 0px solid; VERTICAL-ALIGN: top; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; BACKGROUND-COLOR: #f9f9f9; TEXT-ALIGN: right; font-family:Arial;font-size:12px;" cellSpacing=0 cellPadding=0 width="100%" border=0>
<TBODY>
<TR><TD>

    <asp:HiddenField 
        id="HiddenField1" runat="server"></asp:HiddenField> 
    <asp:HiddenField 
        id="HiddenField_ObId" runat="server">
                        </asp:HiddenField>
                                             <asp:HiddenField 
        id="HiddenField_InspId" runat="server">
                        </asp:HiddenField>
                                             <asp:HiddenField id="HiddenField_TotalObs" runat="server"></asp:HiddenField>
                                             <asp:HiddenField 
        id="HiddenField_MTMSupt" runat="server"></asp:HiddenField>
    <asp:HiddenField id="HiddenField_ObsId" runat="server"></asp:HiddenField>
                                             <asp:HiddenField ID="HiddenField_ChapId" runat="server" /></TD></TR>
                                         <TR>
    <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px">
        <FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: right">
                <TABLE cellSpacing=0 cellPadding=3 width="100%">
                    <TBODY>
                    <TR>
                    <TD style="PADDING-RIGHT: 10px; " colspan="8" style="height:23px; text-align :center " class="text headerband"><span lang="en-us" >
                        Update Observation </span></TD>
                        </TR>
                    <tr>
                    <td style="PADDING-RIGHT: 10px; HEIGHT: 15px; TEXT-ALIGN: right" width="10%"><span 
                            lang="en-us">Insp#:</span></td>
                    <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                        <asp:TextBox ID="txtinspno" runat="server" CssClass="input_box" ReadOnly="True" 
                            Width="140px">
                                    </asp:TextBox></td>
                    <td style="PADDING-RIGHT: 10px; HEIGHT: 15px; TEXT-ALIGN: right" width="10%">Vessel Name:</td>
                    <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                        <asp:TextBox ID="txtvessel" runat="server" CssClass="input_box" ReadOnly="True" 
                            Width="140px">
                                    </asp:TextBox></td>
                    <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right; height: 15px;" width="10%">Insp Name:</td>
                    <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                        <asp:TextBox ID="txtinspname" runat="server" CssClass="input_box" 
                            ReadOnly="True" Width="140px">
                                    </asp:TextBox></td>
                    <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; height: 15px;" width="10%">Last Done:</td>
                    <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                        <asp:TextBox ID="txtlastdone" runat="server" CssClass="input_box" 
                            ReadOnly="True" Width="140px">
                                    </asp:TextBox></td></tr>
                     <TR>
                        <TD 
        style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; " width="10%">Planned Port:</TD>
                        <TD style="TEXT-ALIGN: left">
                            <asp:TextBox ID="txtplannedport" 
        runat="server" CssClass="input_box" ReadOnly="True" Width="140px">
                                    </asp:TextBox></TD>
                        <TD 
        style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; " width="10%">
                            Planned Date:</TD>
                        <TD style="TEXT-ALIGN: left">
                            <asp:TextBox ID="txtplanneddate" runat="server" 
        CssClass="input_box" ReadOnly="True" Width="140px">
                                    </asp:TextBox></TD>
                        <TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Status:</TD>
                        <TD style="TEXT-ALIGN: left">
                            <asp:TextBox ID="txt_Status" runat="server" 
        CssClass="input_box" ReadOnly="True" Width="140px">
                                    </asp:TextBox></TD>
                        <TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Next Due:</TD>
                        <TD style="TEXT-ALIGN: left"><asp:TextBox id="txtnextdue" runat="server" ReadOnly="True" Width="140px" CssClass="input_box"></asp:TextBox>
                            </TD></TR>
                            
                        </TBODY></TABLE>
        </FIELDSET> </TD></TR>
        
<TR runat="server" visible="false">
    <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px">
        <FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: right">
                <TABLE cellSpacing=0 cellPadding=0 width="100%">
                    <TBODY>
                    <tr>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right;" width="10%">Start Dt. :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtstartdate" runat="server" Width="84px" CssClass="required_box"></asp:TextBox> 
                            <asp:ImageButton id="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Done Dt :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtdonedt" runat="server" Width="84px" CssClass="required_box" OnTextChanged="txtdonedt_TextChanged" AutoPostBack="True"></asp:TextBox>
                            <asp:ImageButton id="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; " width="10%">Port Done : </td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtportdone" runat="server" Width="210px" CssClass="required_box" OnTextChanged="txtportdone_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Response Due Dt. :</td>
                        <td style="TEXT-ALIGN: left">
                            <asp:TextBox id="txtresponseduedt" runat="server" Width="84px" CssClass="input_box" OnTextChanged="txtresponseduedt_TextChanged" AutoPostBack="True"></asp:TextBox>
                            <asp:ImageButton id="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; "> <asp:Button id="Button1" onclick="btnSave_Click" runat="server" Width="59px" CssClass="btn" Text="Save" OnClientClick="return checkform1();"></asp:Button> </td>
                        </tr>
               </TBODY></TABLE>
               </FIELDSET>
               <ajaxToolkit:CalendarExtender id="CalendarExtender3" runat="server" TargetControlID="txtdonedt" PopupPosition="TopRight" PopupButtonID="ImageButton2" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
               <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" TargetControlID="txtstartdate" PopupPosition="TopRight" PopupButtonID="ImageButton3" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
               <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txtresponseduedt" PopupPosition="TopRight" PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
               <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" TargetControlID="txtportdone" ServicePath="../WebService.asmx" ServiceMethod="GetPortTitles" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters=""></cc1:AutoCompleteExtender>

               </TD></TR>  
                                         <TR>
                            <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px" vAlign=top>
                                <FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #8fafdb 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #8fafdb 1px solid; PADDING-TOP: 0px; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: center"><LEGEND>
                                        <STRONG>Observation</STRONG></LEGEND>
                                        <table  width="100%">
                                        <tr>
                                        <td style="vertical-align: top;">
                                        <table style="width :100%">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="text-align: center">
                                            <table cellpadding="0" cellspacing="0"> 
                                            <tr>
                                               <td style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px" valign="bottom">
                                                <asp:Button ID="lnkadd" runat="server" CssClass="btn" onclick="lnkadd_Click" 
                                                    Text="Add" Width="70px"></asp:Button> 
                                                <asp:Button ID="btn_Edit" runat="server" CssClass="btn" 
                                                    onclick="btn_Edit_Click" Text="Edit" Width="70px"></asp:Button>
                                                <asp:Button ID="btnsave" runat="server" CssClass="btn" onclick="btnsave_Click" 
                                                    Text="Save" Width="70px"></asp:Button> 
                                                <asp:Button ID="ImageButton4" runat="server" CssClass="btn" 
                                                    onclick="ImageButton4_Click" Text="Delete" Width="70px">
                                                
                                                   </asp:Button> 
                                             </td>
                                            <td valign="bottom">
                                                <asp:Button ID="btnFirst" runat="server" CssClass="btn" 
                                                    onclick="btnFirst_Click" Text="|&lt;&lt;"></asp:Button> 
                                                <asp:Button ID="btnPrev" runat="server" CssClass="btn" onclick="btnPrev_Click" 
                                                    Text="&lt;&lt;"></asp:Button> 
                                                <asp:TextBox ID="txtRow" runat="server" CssClass="input_box" 
                                                    style="TEXT-ALIGN: center" Width="69px"></asp:TextBox> 
                                                <asp:Button ID="btnNext" runat="server" CssClass="btn" onclick="btnNext_Click" 
                                                    Text="&gt;&gt;"></asp:Button> 
                                                <asp:Button ID="btnLast" runat="server" CssClass="btn" onclick="btnLast_Click" 
                                                    Text="&gt;&gt;|"></asp:Button>
                                            </td>
                                            </tr>
                                            </table>
                                                </td>
                                            <td style="text-align: left">&nbsp;
                                                </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Question#:</td>
                                            <td style="text-align: left">
                                            <table cellpadding="0" cellspacing="0"  width="500px" border ="0">
                                            <tr>
                                            <td valign="bottom" >
                                                <asp:TextBox ID="txtquestionno" runat="server" AutoPostBack="True" 
                                                    CssClass="input_box" OnTextChanged="txtquestion_check" ReadOnly="True" 
                                                    Width="40px"></asp:TextBox> 
                                                <asp:Button ID="Button2" runat="server" CssClass="btn" 
                                                    onclick="btnfordesc_Click" OnClientClick="return DescPopUp();" Text="Guidance" 
                                                    Width="80px"></asp:Button>
                                            </td>
                                         
                                            <td >
                                                <asp:CheckBox ID="chkObs" runat="server" Text="Observation" Visible="false"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkhighrisk" runat="server" Text="High Risk" Visible="true" style="display:none;">
                                                </asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkncr" runat="server" Text="NCR" Visible="false">
                                                </asp:CheckBox>
                                            </td>
                                            <td style=" padding-left :10px;" >
                                            Comply : 
                                                
                                            <asp:DropDownList ID="ddlyesno" runat="server" AutoPostBack="True" 
                                                    CssClass="input_box" OnSelectedIndexChanged="ddlyesno_SelectedIndexChanged" 
                                                    Width="50px" style="width:100px;">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>NO</asp:ListItem>
                                        <asp:ListItem>NS</asp:ListItem>
                                        <asp:ListItem>NA</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:CheckBox ID="chkHighRisk1" runat="server" Text="High Risk" />--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPscCodeLabel" runat="server" Text="PSC Code :"></asp:Label>
                                                <asp:DropDownList ID="ddlPscCode" runat="server" style="width:100px;" CssClass="input_box"></asp:DropDownList>
                                                <%--<asp:CheckBox ID="chkHighRisk1" runat="server" Text="High Risk" />--%>
                                            </td>
                                            
                                            
                                            </tr>
                                            </table>
                                            </td>
                                            <td style="text-align: left">
                                                &#160;</td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Question:</td>
                                            <td style="text-align: left">
                                                    <asp:TextBox ID="txtquestion" runat="server" CssClass="input_box" 
                                                    Height="75px" Rows="12" TextMode="MultiLine" Width="800px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left"></td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Deficiency:</td>
                                            <td style="text-align: left">
                                                        <asp:TextBox ID="txtdeficiency" runat="server" CssClass="input_box" 
                                                    Height="118px" Rows="8" TextMode="MultiLine" Width="800px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left">
                                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Modules/HRD/Images/pencil-icon.gif" Visible="false" OnClientClick="return OpenLargeDefPopUp();" ToolTip="Click here to open large window."></asp:ImageButton>                                                        
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Comment:</td>
                                            <td style="text-align: left">
                                                        <asp:TextBox ID="txtcomment" runat="server" CssClass="input_box" 
                                                    Height="139px" Rows="12" TextMode="MultiLine" Width="800px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left">
                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Modules/HRD/Images/pencil-icon.gif"  Visible="false"  OnClientClick="window.open('..\\Inspection\\Comment_PopUp.aspx',null,'title=no,toolbars=no,scrollbars=yes,width=680,height=480,left=20,top=20,addressbar=no');" ToolTip="Click here to open large window."></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                        <table>
                                        <tr>
                                        <td>Created By:</td><td>
                                            <asp:TextBox id="txtCreatedBy_DocumentType" tabIndex=-1 runat="server" ReadOnly="True" Width="154px" CssClass="input_box" BackColor="Gainsboro"></asp:TextBox></td>
                                        <td>Created On:</td><td>
                                            <asp:TextBox id="txtCreatedOn_DocumentType" tabIndex=-2 runat="server" ReadOnly="True" Width="72px" CssClass="input_box" BackColor="Gainsboro"></asp:TextBox></td>
                                            <td>
                                                &#160;</td>
                                        </tr>
                                        <tr>
                                        <td>Modified By:</td><td>
                                            <asp:TextBox id="txtModifiedBy_DocumentType" tabIndex=-3 runat="server" ReadOnly="True" Width="154px" CssClass="input_box" BackColor="Gainsboro"></asp:TextBox></td>
                                        <td>Modified On:</td><td>
                                            <asp:TextBox id="txtModifiedOn_DocumentType" tabIndex=-4 runat="server" ReadOnly="True" Width="72px" CssClass="input_box" BackColor="Gainsboro"></asp:TextBox></td>
                                            <td>
                                                &#160;</td>
                                        </tr>
                                        <tr>
                                        <td><asp:Label id="lblmessage" runat="server" ForeColor="#C00000" __designer:wfdid="w6"></asp:Label></td>
                                        <td >&nbsp;</td>
                                        <td>&#160;</td>
                                        <td>&#160;</td>
                                        <td>&#160;</td>
                                        </tr>
                                        </table>
                                        </td>
                                        </tr>
                                        </table>
                                        </td></tr>
                                        </table>
                                </FIELDSET> 
                                <TABLE style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; WIDTH: 100%; PADDING-TOP: 5px" cellSpacing=0 cellPadding=0 border=0>
                                    <TBODY>
                                        <TR>
                                            <TD style="PADDING-RIGHT: 8px; TEXT-ALIGN: left">
                                            <asp:Button runat="server" ID="btnAddQuest" Visible="false" OnClientClick="AddEditCheckList();return false;" Text="Add Question" CssClass="btn"   /> 
                                                <asp:FileUpload ID="flp_CheckList" runat="server"  Enabled="False" Width="450px"></asp:FileUpload></TD>
                                            <TD align="right" style="TEXT-ALIGN: left">
                                                <asp:Button ID="btn_ImportCheckList" runat="server" CssClass="btn" Enabled="False" onclick="btn_ImportCheckList_Click" Text="Import CheckList" Width="127px"></asp:Button></TD>
                                            <TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right" align=right>&#160;</TD>
                                            <TD 
        style="PADDING-RIGHT: 5px; TEXT-ALIGN: right" align=right>
                                                <asp:Button ID="btnUpdateCrew" Visible="false" runat="server" CssClass="btn" OnClientClick="OpenUpdateCrewPopUp();" Text="Update Crew" />&#160;
                                                <asp:Button ID="btnNotify" runat="server" CssClass="btn" Enabled="False" onclick="btnNotify_Click" Text="Notify" Width="59px"></asp:Button>&#160;
                                                <asp:Button id="btn_Print" runat="server" Width="111px" CssClass="btn" Text="Print Checklist" OnClientClick="return PrintObsRPT();" Visible="false"></asp:Button>
                                                <asp:Label id="lbl_GridView_DocumentType" runat="server" Text="" Visible="false"></asp:Label></TD></TR></TBODY></TABLE>
                                                <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" TargetControlID="txtportdone" ServicePath="../WebService.asmx" ServiceMethod="GetPortTitles" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters=""></cc1:AutoCompleteExtender>
                                 </TD></TR>
                        <TR>
    <TD style="PADDING-RIGHT: 10px; COLOR: red; TEXT-ALIGN: center"></TD></TR></TBODY>
 </TABLE>
 </asp:Panel> 
</contenttemplate>
</asp:UpdatePanel>
</form>
</body>
</html>
