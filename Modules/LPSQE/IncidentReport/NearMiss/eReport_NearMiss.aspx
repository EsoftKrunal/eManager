<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eReport_NearMiss.aspx.cs" Inherits="eReports_S133_eReport_S133" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eReport Near Miss</title>

     <script type="text/javascript" src="../JS/jquery.min.js"></script>
     <script type="text/javascript" src="../JS/KPIScript.js"></script>

      <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css"/>
     <link rel="stylesheet" type="text/css" href="../../../HRD/Styles/StyleSheet.css"/>

<script type="text/javascript">
     function getBaseURL() {
             var url = window.location.href.split('/');
             var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
             return baseUrl;
         }
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
    <table cellpadding="0" cellspacing="0" width="100%">
         <tr style=" background-color:#CCE6FF"> <%--#3498db--%>
             <td >
              <table cellpadding="0" cellspacing="0" width="100%">
                 <tr class= "text headerband">
                     <td style="text-align:left; width:30%; " >Vessel : <asp:Label ID="lblVesselName" runat="server" ></asp:Label></td>
                     <td style="text-align:center; width:40%; "> <asp:Label ID="lblFormName" runat="server" class="formname"></asp:Label></td>
                     <td style="text-align:right; width:30%" >Form :&nbsp;NearMiss</td>
                     </tr>
                 <tr>
                     <td style="text-align:left; width:30%; " >&nbsp; [ <asp:Label ID="lblReportNo" runat="server" Font-Bold="true" ForeColor="Brown" Font-Size="14px" ></asp:Label> ]</td>
                     <td style="text-align:center; width:40%; ">                        
                     </td>
                     <td style="text-align:right; width:30%" >
                        
                     </td>

                 </tr>
             </table>
             </td>
         </tr>
         <tr style=" ">
             <td>
                 <table cellpadding="0" cellspacing="0" width="100%">
                 <tr>
                     <td></td>
                     <td>Date of Occurence</td>
                     <td>:</td>
                     <td colspan="13"><asp:TextBox ID="txtOccurenceDt" MaxLength="11" runat="server" CssClass="date_only" Text="" ></asp:TextBox>
                         <span class="comment">(dd-mmm-yyyy)</span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtOccurenceDt" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>
                 </tr>
                 
                  <tr>
                     <td>
                   <b>Identified By :</b>
                     </td>
                     <td>Family Name</td>
                     <td>:</td>
                     <td>
                         <asp:TextBox ID="txtFamilyName" MaxLength="30" runat="server" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtFamilyName" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>
                     <td>&nbsp;</td>
                     <td>First Name</td>
                     <td>:</td>
                     <td>
                        <asp:TextBox ID="txtFirstName" MaxLength="50" runat="server" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFirstName" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>

                     <td>&nbsp;</td>
                     <td>Rank</td>
                     <td>:</td>
                     <td>
                          <asp:DropDownList ID="ddlRank" runat="server" ></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlRank" InitialValue="0" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>

                     <td>&nbsp;</td>
                     <td>Crew #</td>
                     <td>:</td>
                     <td>
                            <asp:TextBox ID="txtCrewNo" MaxLength="6" runat="server" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtCrewNo" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>
                 </tr>
                 <tr>
                     <td colspan="16">Category : <span class="comment">(Mark one or more categories this Near Miss is related)</span></td>
                 </tr>
                 <tr>
                     <td colspan="16">
                     <asp:CheckBoxList ID="cblCategory" RepeatDirection="Horizontal" runat="server">
                     </asp:CheckBoxList>
                     </td>
                 </tr>
               
                  <tr>
                   <td ><b> Signed Master :</b></td>
                     <td>Family Name</td>
                     <td>:</td>
                     <td>
                         <asp:TextBox ID="txt_SM_FamilyName" MaxLength="30" runat="server" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txt_SM_FamilyName" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>
                     <td>&nbsp;</td>
                     <td>First Name</td>
                     <td>:</td>
                     <td>
                        <asp:TextBox ID="txt_SM_FirstName" MaxLength="50" runat="server" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txt_SM_FirstName" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>

                     <td>&nbsp;</td>
                     <td>Crew #</td>
                     <td>:</td>
                     <td colspan="5">
                            <asp:TextBox ID="txt_SM_CrewNo" MaxLength="6" runat="server" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txt_SM_CrewNo" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>
                 </tr>
                 </table>
             </td>
         </tr>
             <tr>
                 <td>
                 <div>                
                 
                  <div style="float:left; width:600px;">
                    <asp:button ID="btnEventDescription" runat="Server" Text="Event Description" CssClass="color_tab_sel" CommandArgument="Div1" OnClick="btn_Tab_Click" CausesValidation="false" Width="120px"/>
                    <asp:button ID="btnSuggestions" runat="Server" Text="Suggestions" CssClass="color_tab" CommandArgument="Div2" OnClick="btn_Tab_Click" CausesValidation="false"  Width="120px"/>
                    <asp:button ID="btnOfficeComments" runat="Server" Text="Office Comments" CssClass="color_tab" CommandArgument="Div3" OnClick="btn_Tab_Click" CausesValidation="false"  Width="150px"/>
                 </div>                 
                 
                 <div style="clear:both"></div>               
                 </div>
                 <div style="width:100%; background-color:#FFCC80; height:4px;"></div>
                 <div style=" position:relative; border:solid 1px #e2e2e2; border-top:none;" >
                    <div id="Div1" runat="server" >
                         <div class='section'>Section 1 : Event Description</div>
                         <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td>
                                     (What happened?)<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="*" ControlToValidate="txtEventDesc" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:TextBox ID="txtEventDesc" TextMode="MultiLine" runat="server" Height="240px" Width="98%" ReadOnly="True"></asp:TextBox>
                                 </td>
                             </tr>
                         </table>
                         
                     </div>
                    <div id="Div2" runat="server" visible="false">
                        <div class='section'>Section 2 : Suggestions</div>
                         <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td>
                                     Suggestions to  prevent  re-occurrence: <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="*" ControlToValidate="txtSuggestions" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:TextBox ID="txtSuggestions" TextMode="MultiLine" runat="server" 
                                         Height="240px" Width="98%" ReadOnly="True"></asp:TextBox>
                                 </td>
                             </tr>
                         </table>
                      
                    </div>
                    <div id="Div3" runat="server" visible="false">
                         <div class='section'>
                             Section 3 : Office Comments
                         </div>
                         <table width="100%" cellpadding="0" cellspacing="0" border="0" >
                         <tr>
                         <td style="width:50%; border:solid 1px #c2c2c2;">
                         <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                            <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td>
                                     Comments : 
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                      <asp:TextBox ID="txtOfficeComments" TextMode="MultiLine" runat="server" 
                                          Height="240px" Width="98%"></asp:TextBox>
                                 </td>
                             </tr>
                             <%--<tr>
                                <td>
                                <b >Closed By / On :
                                <asp:Label runat="server" ID="lblOfficeCommentByOn"></asp:Label>
                                </b>
                                </td>
                             </tr>--%>
                             <tr>
                                <td class="hdng1">IS THIS NEARMISS CLOSED?&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radsec15ICyes" runat="server" GroupName="radsec15IC" Text="Yes" OnCheckedChanged="radsec15ICyes_OnCheckedChanged" AutoPostBack="true"/><asp:RadioButton ID="radsec15ICno" runat="server" GroupName="radsec15IC" Text="No" OnCheckedChanged="radsec15ICyes_OnCheckedChanged" /></td>
                            </tr>
                            <tr id="trIC" runat="server" visible="false">
                                <td >
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                      <tr>
                                         <td style="width:10%;">Date closed:</td>
                                         <td style="width:15%;"><asp:TextBox ID="txtsec15CD" MaxLength="11" runat="server"  CssClass="date_only" ></asp:TextBox></td>
                                         <td style="width:7%;">Closed by:</td>
                                         <td><asp:TextBox ID="txtsec15CB" MaxLength="50" Width="350px" runat="server"></asp:TextBox></td>
                                      </tr>
                                    </table>
                                </td>
                            </tr>
                             <tr style="display:none;">
                                <td class="hdng1">IS VESSEL NOTIFIED OF INCIDENT CLOSURE?&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radsec15CNyes" runat="server" GroupName="radsec15CN" Text="Yes" OnCheckedChanged="radsec15CNyes_OnCheckedChanged" AutoPostBack="true"/><asp:RadioButton ID="radsec15CNno" runat="server" GroupName="radsec15CN" Text="No" OnCheckedChanged="radsec15CNyes_OnCheckedChanged" AutoPostBack="true"/></td>
                            </tr>
                            <tr style="display:none;" id="trVN" runat="server" visible="false">
                                <td >
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                      <tr>
                                         <td style="width:10%;">Date of Notification:</td>
                                         <td><asp:TextBox ID="txtsec15NotifyDate" MaxLength="11" runat="server"  CssClass="date_only" ></asp:TextBox></td>
                                      </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="hdng1">
                                <asp:Button runat="server" ID="btnLockUnlock" OnClick="btnLockUnlock_Click" Text="Lock for Ship" CssClass="btn"/>
                                </td>
                            </tr>
                         </table>
                            </td>
                            </tr>
                         </table>
                         </td>
                         <td style="width:50%; border:solid 1px #c2c2c2;vertical-align:top;">
                         <asp:UpdatePanel runat="server" ID="up1">
                         <ContentTemplate>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                            <tr>
                            <td>
                             <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td colspan="2">
                                     <b>NM Severity : </b>
                                     <%--<asp:CheckBox runat="server" ID="chksec15INMSeveritySignificant" Text="Significant" OnCheckedChanged="chksec15INMSeveritySignificant_CheckedChanged" AutoPostBack="true" />--%>
                                     <asp:DropDownList ID="ddlsec15INMSeverity" OnSelectedIndexChanged="chksec15INMSeveritySignificant_CheckedChanged" AutoPostBack="true" runat="server" Width="200px" >
                                        <asp:ListItem Value="" Text=" < SELECT >"></asp:ListItem>
                                        <asp:ListItem Value="S" Text="Significant"></asp:ListItem>
                                        <asp:ListItem Value="M" Text="Non Significant"></asp:ListItem>
                                    </asp:DropDownList>
                                 </td>
                             </tr>
                             <tr runat="server" id="IRCauseHeader" visible="false">
                                 <td class="hdng1">
                                     <b>Immediate Cause </b>
                                     <asp:LinkButton runat="server" id="lnkICause" CausesValidation="false" OnClick="lnlIRCause_Click"> ( Modify ) </asp:LinkButton>
                                 </td>
                                 <td class="hdng1">
                                    <b>Root Cause</b>
                                    <asp:LinkButton runat="server" id="lnkRCause" CausesValidation="false"  OnClick="lnlIRCause_Click"> ( Modify ) </asp:LinkButton>
                                 </td>
                             </tr>
                             <tr runat="server" id="IRCauseData" visible="false">
                                 <td>
                                     <ul>
                                     <asp:Repeater runat="server" ID="rptImm_Cause">
                                     <ItemTemplate>
                                     <li style="list-style-type: circle; margin-left:20px;"><%#Eval("OptionText")%></li>
                                     </ItemTemplate>
                                     </asp:Repeater>
                                     </ul>
                                 <asp:Label runat="server" ID="lblOther1" Text=""></asp:Label>
                                 <br />
                                 <asp:Label runat="server" ID="lblOther2" Text=""></asp:Label>
                                 </td>
                                 <td>
                                     <ul>
                                     <asp:Repeater runat="server" ID="rptRoot_Cause">
                                     <ItemTemplate>
                                     <li style="list-style-type: circle; margin-left:20px;"><%#Eval("OptionText")%></li>
                                     </ItemTemplate>
                                     </asp:Repeater>
                                     </ul>
                                 <asp:Label runat="server" ID="lblOther21" Text=""></asp:Label>
                                 <br />
                                 <asp:Label runat="server" ID="lblOther22" Text=""></asp:Label>
                                 </td>
                             </tr>
                             </table>
                            </td>
                            </tr>  
                         </table>
                         </ContentTemplate>
                         <Triggers>
                         <asp:PostBackTrigger ControlID="lnkICause" />
                         <asp:PostBackTrigger ControlID="lnkRCause" />
                         </Triggers>
                         </asp:UpdatePanel>
                         </td>
                         </tr>
                         </table>
                    </div>
                 </div>
                 </td>
             </tr>
         <tr>
             <td>
                 <div style="border:solid 1px #e2e2e2;padding-bottom:0px; margin-top:3px; background-color:#CCE6FF; height:60px;">
                   <center>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0" >
                        <tr >
                         <td id="tdTabs" runat="server" style="padding-right:10px" >
                            <asp:Button ID="btnSaveDraft" runat="server" Text="Save Report" CssClass="btn" style="float:left" onclick="btnSaveDraft_Click" CausesValidation="true" Width='120px'/>
                            <asp:Button ID="btnExportToShip" runat="server" Text="Export to Ship" CssClass="btn" style="float:left" onclick="btnExportToShip_Click" CausesValidation="true" Width='140px' OnClientClick="confirm('Are you sure to export?');"/>
                        </td>
                        </tr>
                        <tr style=" background-color:#FFFFFF">
                        <td>&nbsp;&nbsp;<asp:Label ID="lblMsg" ForeColor="Red" Font-Size="16px" runat="server" Text="" CssClass="msg"></asp:Label>
                        </td>
                        </tr>
                    </table>
                    </center>
                 </div>
             </td>
         </tr>
         </table>
      
    <div style="position:absolute;top:0px;left:0px; height :560px; width:100%; " id="dvIRCause" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:1100px;  height:485px;padding :5px; text-align :center;background : white; z-index:150;top:30px; border:solid 0px black;">
            <center >
            <div class= "text headerband">Immediate & Root Cause</div>
            <div style="min-height:452px">
            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
            
            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                    <td class='hdng1'>IMMEDIATE CAUSES <span class="comment">( Check all that apply )</span></td>
                    <td class='hdng1'>ROOT CAUSES <span class="comment">( Check all that apply )</span></td>
                </tr>
                <tr>
                    <td style="width:50%">
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td style="width:50%">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td class='hdng2'>Human Actions</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left">
                                                <div style="overflow-x:hidden;overflow:scroll; height:340px">
                                                <asp:CheckBoxList runat="server" ID="chklstsec13HA" ></asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                Other (Specify)
                                                <asp:TextBox runat="server" ID="txtsec13HAOther"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td class='hdng2'>Conditions</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left">
                                                <div style="overflow-x:hidden;overflow:scroll; height:340px">
                                                <asp:CheckBoxList runat="server" ID="chklstsec13Cond" ></asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Other (Specify)
                                                <asp:TextBox runat="server" ID="txtsec13CondOther"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td style="width:50%">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td class='hdng2'>Human Factors</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left">
                                                <div style="overflow-x:hidden;overflow:scroll; height:340px">
                                                <asp:CheckBoxList runat="server" ID="chklstsec13HF" ></asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Other (Specify)
                                                <asp:TextBox runat="server" ID="txtsec13HFOther"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td class='hdng2'>Job Factors</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left">
                                                <div style="overflow-x:hidden;overflow:scroll; height:340px">
                                                <asp:CheckBoxList runat="server" ID="chklstsec13JF" ></asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Other (Specify)
                                                <asp:TextBox runat="server" ID="txtsec13JFOther"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr> 
                             
            </table>
            <div>
                <div style="text-align:right;">
                    <asp:Label runat="server" ID="lblMessage_POP" ForeColor="Red" Text=""></asp:Label>
                    <asp:Button ID="btnSaveIRCause" Text="Save" OnClick="btnSaveIRCause_Click" CssClass="btn"  runat="server" CausesValidation="true" ValidationGroup="ITP"/>
                </div>
            </div>
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveIRCause" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
            <div style="text-align:right; position:relative; right:-22px; top:-2px;">
                <asp:Button runat="server" ID="btnIRCause" Text="Close" onclick="btnIRCauseWindow_Click" CausesValidation="false" title='Close this Window !' CssClass="btn"/>   
            </div>
            </center>
         </div>
         </center>
    </div>

    </div>
    </form>
</body>

<script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
<script type="text/javascript">

    function SetCalender() {
        $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
        $('.date_time').datetimepicker({ format: 'd-M-Y H:i', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
    }
    function Page_CallAfterRefresh() {
        SetCalender();
    }
    SetCalender();
</script>
</html>
