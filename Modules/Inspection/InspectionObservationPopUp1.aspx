<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspectionObservationPopUp1.aspx.cs" Inherits="Transactions_InspectionObservation1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>EMANAGER</title>
       <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    </head> 
<body style="text-align: center">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
        <contenttemplate>
        <asp:Panel id="pnl_Observation" runat="server">
        <TABLE style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 0px solid; VERTICAL-ALIGN: top; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; BACKGROUND-COLOR: #f9f9f9; TEXT-ALIGN: right" cellSpacing=0 cellPadding=0 width="100%" border=0>
<TBODY>
<TR><TD>

    &nbsp;</TD></TR>
                                         <TR>
    <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px">
        <FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: right">
                <TABLE cellSpacing=0 cellPadding=3 width="100%">
                    <TBODY>
                    <TR>
                    <TD style="PADDING-RIGHT: 10px; " colspan="8" style="  height:23px; text-align :center " class="text headerband"><span lang="en-us" >
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
        
                                         <TR>
                            <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px" vAlign=top>
                                <FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #8fafdb 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #8fafdb 1px solid; PADDING-TOP: 0px; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: center"><LEGEND>
                                        <STRONG>Observations</STRONG></LEGEND>
                                        <table  width="100%" >
                                        <tr>
                                        <td style="vertical-align: top;">
                                        <table cellpadding="0" cellspacing="0" border="1" width="100%" rules="all" style="border-collapse:collapse;">
                                        <colgroup>
                                            <col width='30px' />
                                            <col width='50px' />
                                            <col width='50px' />
                                            <col/>
                                            <col width='140px' />
                                            <col width='180px' />
                                            <col width='50px' />
                                            <col width='16px' />
                                        </colgroup>
                                        <tr class= "headerstylegrid"  >
                                            <td style="text-align:center;" >Sr#</td>
                                            <td style="text-align:center;" >Edit</td>
                                            <td style="text-align:center;" >Qno</td>
                                            <td>Deficiency</td>
                                            <td style="text-align:center;" >Target Closure Dt.</td>
                                            <td>Closed By/On</td>
                                            <td style="text-align:center;" >Action</td>
                                            <td></td>
                                        </tr>
                                        </table>
                                        <div style=" height:300px; overflow-y:scroll; overflow-x:hidden;">
                                        <table cellpadding="0" cellspacing="0" border="1" width="100%" rules="all" style="border-collapse:collapse;">
                                        <colgroup>
                                            <col width='30px' />
                                            <col width='50px' />
                                            <col width='50px' />
                                            <col/>
                                            <col width='140px' />
                                            <col width='180px' />
                                            <col width='50px' />
                                        </colgroup>
                                        <asp:Repeater runat="server" ID="rptList">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align:center;" ><%#Eval("SrNo").ToString()%></td>
                                                <td style="text-align:center;" >
                                                    <asp:ImageButton ID="btnEditDeficiency" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnEditDeficiency_OnClick" CommandArgument='<%#Eval("TableID")%>' /> 
                                                </td>
                                                <td style="text-align:center;" ><%#Eval("QuestionNo").ToString()%></td>
                                                <td><%#Eval("Deficiency").ToString()%></td>
                                                <td style="text-align:center;" ><%# Common.ToDateString( Eval("TCLDate").ToString())%></td>
                                                <td>
                                                    <%#Eval("ClosedBy").ToString()%>  
                                                    <%# Common.ToDateString(Eval("ClosedOn").ToString())%> 
                                                </td>
                                                <td style="text-align:center;" >
                                                    <asp:ImageButton ID="btnClosurePopup" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnClosurePopup_OnClick" CommandArgument='<%#Eval("TableID")%>' ToolTip=" Closure " Visible='<%# (Eval("Closure").ToString()=="No") %>' /> 
                                                    <asp:ImageButton ID="btnViewClosure" runat="server" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnViewClosure_OnClick" CommandArgument='<%#Eval("TableID")%>' ToolTip=" View Closure " Visible='<%# (Eval("Closure").ToString()=="Yes") %>' /> 

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        </asp:Repeater>
                                        
                                        </table>
                                        </div>
                                        </td>
                                        </tr>
                                        </table>
                                </FIELDSET> 
                                 </TD></TR>
                        <TR>
    <TD style="PADDING-RIGHT: 10px; COLOR: red; TEXT-ALIGN: center"></TD></TR></TBODY>
 </TABLE>
 <br />
<asp:Button runat="server" ID="btnDeficiencyPopup" Text="Add Deficiency" CssClass="btn" style="float:right" OnClick="btnDeficiencyPopup_OnClick" />
<asp:Button runat="server" ID="btnPrint" Text="Print" CssClass="btn" style="float:right" OnClick="btnPrint_OnClick" />
</asp:Panel> 


        
        <%--Add Update Deficiency--%>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvAddUpdateDeficiency" runat="server" visible="false">
        <center>
        <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:800px; height:320px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:60px;opacity:1;filter:alpha(opacity=100)">
            <div style="border:solid 1px #c2c2c2;">
            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <colgroup>
                    <col width="130px" />
                    <col />
                    <tr>
                        <td class="text headerband" colspan="2" 
                            style=" height:23px; text-align :center;PADDING-RIGHT:10px;">
                            <span lang="en-us">Add/Update Observation </span>
                            <asp:HiddenField ID="hfdMC" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Qno. :</td>
                        <td>
                            <asp:TextBox ID="txtQno" runat="server" CssClass="input_box" MaxLength="50" 
                                Width="90px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Deficiency :</td>
                        <td>
                            <asp:TextBox ID="txtDeficiency" runat="server" CssClass="input_box" 
                                Height="100px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Corrective Action :</td>
                        <td>
                            <asp:TextBox ID="txtCorrAction" runat="server" CssClass="input_box" 
                                Height="100px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Target Closure Dt. :</td>
                        <td>
                            <asp:TextBox ID="txtTCD" runat="server" CssClass="input_box" MaxLength="50" 
                                Width="90px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" 
                                Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopRight" 
                                TargetControlID="txtTCD">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;">
                            <asp:Label ID="lblMsgDeficiency" runat="server" Font-Bold="true" 
                                ForeColor="Red"></asp:Label>
                            <asp:Button ID="btnSaveDeficiency" runat="server" CssClass="btn" 
                                OnClick="btnSaveDeficiency_OnClick" Text=" Save " />
                            <asp:Button ID="btnCloseDeficiency" runat="server" CssClass="btn" 
                                OnClick="btnCloseDeficiency_OnClick" Text=" Close " />
                        </td>
                    </tr>
                </colgroup>
            </table>
            </div>
        </div>
        </center>
        </div>


        <%--Add Closure Deficiency--%>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvClosure" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:800px; height:200px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:60px;opacity:1;filter:alpha(opacity=100)">

            <div>    
            <br />
            <table cellpadding="2" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
                <tr>
                    <td class="text headerband" colspan="4" style="height: 23px;  text-align :center ">Closure</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right;">Closed Date :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txt_ClosedDate" runat="server" CssClass="input_box" Width="122px" MaxLength="11"></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                    <td style="padding-right: 5px; text-align: right">Cause :</td>
                    <td style="text-align: left">
                        <asp:CheckBoxList ID="rdbflaws" runat="server" RepeatDirection="Horizontal" Width="311px">
                            <asp:ListItem>People</asp:ListItem>
                            <asp:ListItem>Process</asp:ListItem>
                            <asp:ListItem>Equipment</asp:ListItem>
                        </asp:CheckBoxList></td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right" valign="top">
                        Remarks :</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txt_ClosedRemarks" runat="server" CssClass="input_box" Height="140px"
                            TextMode="MultiLine" Width="612px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right" valign="top" class="style3">
                        Closure Evidence :
                    </td>
                    <td style="text-align: left" class="style2" colspan="3" >
                        <asp:FileUpload runat="server" ID="fu_ClosureEvidence" Width="300px" CssClass="input_box" />
                        <a runat="server" target="_blank"  id="a_file"><img style=" border:none"  src="../Modules/HRD/Images/paperclip.gif" alt="Attachment"/> </a> 
                        </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td style="text-align: left" class="style1">
                        <asp:Button ID="btnSaveClosure" runat="server" CssClass="btn" OnClick="btnSaveClosure_Click" Text="Save" Width="59px" />
                        <asp:Button ID="btnCloseClosure" runat="server" CssClass="btn" OnClick="btnCloseClosure_Click"
                            Text="Cancel" Width="59px" />
                    </td>
                    <td style="text-align: left" class="style1">
                    </td>
                    <td style="text-align: left" class="style1">
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right">Closed By :
                    
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txt_ClosedBy" runat="server" BackColor="Gainsboro" CssClass="input_box" ReadOnly="True"></asp:TextBox></td>
                    <td style="padding-right: 5px; text-align: right">Closed On :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txt_ClosedOn" runat="server" BackColor="Gainsboro" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                     </td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right">
                    </td>
                    <td style="text-align: left" colspan="3">
                        &nbsp;<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_ClosedDate"> </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
            </table>
        </div>
                <%--<div style="border:solid 1px #c2c2c2;">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <col width="120px" />
                    <tr>
                        <td colspan="2" style="background-color:#4371a5; height:23px; text-align :center;PADDING-RIGHT:10px;" class="text">
                        <span lang="en-us" >Observation Closure</span>
                    </td>
                    </tr>
                    <tr>
                        <td>Remarks :</td>
                        <td>
                            <asp:TextBox ID="txtClosureRemarks" runat="server" TextMode="MultiLine" Width="95%" Height="100px" CssClass="input_box"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Closure Date :</td>
                        <td>
                            <asp:TextBox ID="txtClosureDate" runat="server" Width="90px" CssClass="input_box"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txtClosureDate"></ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right; padding:4px;">
                            <asp:Label ID="lblMsgDefClosure" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
                            <asp:Button ID="btnSaveClosure" runat="server" OnClick="btnSaveClosure_OnClick" Text=" Save " CssClass="btn" />
                            <asp:Button ID="btnCloseClosure" runat="server" OnClick="btnCloseClosure_OnClick" Text=" Close " CssClass="btn" />
                        </td>
                    </tr>
                </table>
                </div>--%>
            </div>
            </center>
        </div>       
</contenttemplate>
<Triggers >
    <asp:PostBackTrigger  ControlID="btnSaveClosure"/>
</Triggers>
</asp:UpdatePanel>
</form>
</body>
</html>
