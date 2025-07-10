<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupNewCase.aspx.cs" Inherits="PopupNewCase" Title="Case Management" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" language="javascript">
    function fncReadOnly(evnt)
    {
      event.returnValue = false;
    }
    function fncInputNumericValuesOnly(evnt) {
             if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                 event.returnValue = false;
             }
         }
    function refreshparent()
    {
        window.opener.reloadme();
    }    
    function ShowFullComment(obj)
        {
            
            var X= obj.childNodes[0];
            var Y= obj.childNodes[1];
            if(X.style.display=='block' || X.style.display=='' )
            {
                X.style.display='none';  
                Y.style.display='block';
            }
            else
            {
                X.style.display='block';  
                Y.style.display='none';
            }
        }
        
        
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function Casemanagementreport()
        {
            var CaseID= <%=CaseID.ToString()%>;
            window.open('../Reports/CaseManagementReport.aspx?CaseID='+CaseID+'');
        }
        
    </script>
    <style type="text/css">
    .ajax__tab_tab
    {
        font-size:13px;
        font-weight:bold;
        color:Purple;
    }
    .ajax__tab_active
    {
        font-size:13px;
        font-weight:bold;
        color:Purple;
    }
    </style>
     <ajaxToolkit:ToolkitScriptManager  ID="ScriptManager2" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <div style="font-family:Arial;font-size:12px;">
        <table cellpadding="2" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center; background-color: #f9f9f9">
            <tr>
                <td  style=" text-align: center; height: 23px"
                    class="text headerband">
                    
                    Case Management
                </td>
            </tr>
            <tr>
                <td >
                    
                                <asp:UpdatePanel ID="UP8" runat="server"  UpdateMode="Conditional">
                                    <ContentTemplate>
                                    <table cellpadding="4" cellspacing="0" style="width: 100%;">
                                  <tr>
                        <td>
                            <fieldset>
                               <legend style="font-weight:bold">Case & Vessel Details</legend>
                               <table cellpadding="2" cellspacing="0" style="width: 100%;">
                                   <colgroup>
                                       <col width="150px" />
                                       <tr>
                                           <td style="text-align: right;">
                                               UW Case # :</td>
                                           <td style="text-align: left; height: 5px">
                                               <b>
                                               <asp:TextBox ID="txtCaseNumber" runat="server" CssClass="input_box" 
                                                   MaxLength="30"></asp:TextBox>
                                               </b>
                                           </td>
                                           <td style="text-align: right;">
                                               Company Case # :
                                           </td>
                                           <td style="text-align: left; height: 5px">
                                               <b>
                                               <asp:Label ID="txtMTMCaseNumber" runat="server" MaxLength="60" Width="120px"></asp:Label>
                                               </b>
                                           </td>
                                           <td style="text-align: right;">
                                               &nbsp;</td>
                                           <td style="text-align: left; height: 5px">
                                               &nbsp;</td>
                                       </tr>
                                       <tr>
                                           <td style="text-align: right;">
                                               Vessel :
                                           </td>
                                           <td style="text-align: left; height: 5px">
                                               <asp:DropDownList ID="ddl_Vessels" runat="server" AutoPostBack="True" 
                                                   CssClass="input_box" OnSelectedIndexChanged="ddl_Vessels_SelectedIndexChanged" 
                                                   required="yes" Width="160px">
                                               </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                   ControlToValidate="ddl_Vessels" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                                           </td>
                                           <td style="text-align: right;">
                                               INSC Group :
                                           </td>
                                           <td style="text-align: left; height: 5px">
                                               <asp:DropDownList ID="ddl_Groups" runat="server" AutoPostBack="True" 
                                                   CssClass="input_box" onselectedindexchanged="ddl_Groups_SelectedIndexChanged" 
                                                   required="yes" Width="160px">
                                               </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                   ControlToValidate="ddl_Groups" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                                           </td>
                                           <td style="text-align: right;">
                                               Sub Group :</td>
                                           <td style="text-align: left; height: 5px">
                                               <asp:DropDownList ID="ddlSubGroup" runat="server" AutoPostBack="true" 
                                                   CssClass="input_box" 
                                                   OnSelectedIndexChanged="ddlSubGroup_OnSelectedIndexChanged" required="yes" 
                                                   Width="160px">
                                               </asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                   ControlToValidate="ddlSubGroup" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td style="text-align: right;">
                                               Under Writer :
                                           </td>
                                           <td style="text-align: left; ">
                                               <asp:DropDownList ID="ddl_UW" runat="server" AutoPostBack="true" 
                                                   CssClass="input_box" OnSelectedIndexChanged="ddl_UW_OnSelectedIndexChanged" 
                                                   required="yes" Width="160px">
                                               </asp:DropDownList>
                                           </td>
                                           <td style="text-align: right;">
                                               Policy# :
                                           </td>
                                           <td style="text-align: left; height: 5px">
                                               <asp:DropDownList ID="ddlPolicyNumber" runat="server" AutoPostBack="true" 
                                                   CssClass="input_box" 
                                                   OnSelectedIndexChanged="ddlPolicyNumber_OnSelectedIndexChanged" required="yes" 
                                                   Width="160px">
                                               </asp:DropDownList>
                                           </td>
                                           <td style="text-align: right;">
                                               &nbsp;</td>
                                           <td style="text-align: left; height: 5px">
                                               &nbsp;</td>
                                       </tr>
                                   </colgroup>
                               </table>
                            </fieldset>
                        </td>
                    </tr>
                                </table>
                                                            
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            
                </td>
            </tr>
            <tr>
                 <td style="vertical-align:top; height:400px; vertical-align:top;">
                    <asp:UpdatePanel ID="UP5" runat="server">
                        <ContentTemplate>
                    <table cellpadding="4" cellspacing="0" style="width: 100%;">
                   <tr>
                    <td>
                         <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
                         <ajaxToolkit:TabPanel ID="tbCaseDetails" runat="server" >
                         <HeaderTemplate>Case Details</HeaderTemplate>
                         <ContentTemplate>
                           <table width="100%" cellpadding="2" cellspacing="0" border="0">
                                <col width="110px" />
                                <tr>
                                    <td align="right">
                                        Date of Incident :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIncidentDate" runat="server" Width="80px" CssClass="input_box" AutoPostBack="true" OnTextChanged="txtIncidentDate_OnTextChanged"> </asp:TextBox>
                                        <asp:DropDownList ID="ddlHour" runat="server" CssClass="input_box" Width="40px">
                                           </asp:DropDownList>
                                           :
                                           <asp:DropDownList ID="ddlMin" runat="server" CssClass="input_box" Width="40px">
                                           </asp:DropDownList>
                                        GMT/UTC
                                        
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtIncidentDate">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Location :</td>
                                    <td>
                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="input_box" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_OnSelectedIndexChanged">
                                            <asp:ListItem Text="In Port" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Voyage" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLocationLableText" runat="server"  Text="Incident Place :" ></asp:Label>                                        
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIncidentPlace" runat="server" CssClass="input_box" MaxLength="200" ></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetPortTitles" ServicePath="../WebService.asmx" TargetControlID="txtIncidentPlace"></cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr id="trIncidentToPort" runat="server" visible="false" >
                                    <td>To Port :</td>
                                    <td>
                                        <asp:TextBox ID="txtToPort" runat="server" CssClass="input_box" MaxLength="200" ></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="GetPortTitles" ServicePath="../WebService.asmx" TargetControlID="txtToPort"></cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <b> Description</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:TextBox ID="txtCaseDesc" runat="server" TextMode="MultiLine" Width="98%" Height="100px" CssClass="input_box"> </asp:TextBox>
                                    </td>
                                </tr>  
                                <tr>
                                    <td colspan="2">
                                           <table id="tblCrew" cellpadding="2" cellspacing="2"  runat="server" visible="false">
                                                <tr>
                                                    <td style="text-align:right;">Crew # :
                                                    
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCrewNo" runat="server"  CssClass="input_box" AutoPostBack="true" OnTextChanged="txtCrewNo_OnTextChanged" MaxLength="6" Width="70px"> </asp:TextBox>    
                                                    </td>
                                                        
                                                </tr>
                                                <tr>
                                                    <td style="text-align:right;">Name :</td>
                                                    <td ><asp:Label ID="txtCrewName" runat="server" > </asp:Label></td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td style="text-align:right;">Rank :</td>
                                                    <td><asp:Label ID="txtCrewRank" runat="server"  > </asp:Label></td>
                                                </tr>
                                           </table>
                                    </td>
                                </tr>                                                              
                           </table>   
                         </ContentTemplate>
                                                  
                         </ajaxToolkit:TabPanel>
                         <ajaxToolkit:TabPanel ID="tbSynopsis" runat="server" >
                         <HeaderTemplate>Synopsis</HeaderTemplate>
                         <ContentTemplate>
                          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                             <table width="99%" border="0" bordercolor="#C0C0C0"  cellpadding="0" cellspacing="0" rules="none">
                                        <tr>
                                            <td style="text-align:left;">
                                                <asp:Button ID="btnAddSynopsis" runat="server" Text="Add Synopsis" CssClass="btn" OnClick="btnAddSynopsis_OnClick" style="margin:2px;" />
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="border:solid 1px grey">
                                                <div id="div3" runat="server"  style="overflow-y: hidden;overflow-x: hidden;">
                                                    <table width="100%" rules="all" border="1" rules="rows" cellpadding="3" cellspacing="0"  style="border-collapse:collapse">
                                                            <colgroup>
                                                            <col width="90px" />
                                                            <col />
                                                            <col width="30px" />
                                                            <col width="30px" />
                                                            <col width="30px" />
                                                            <col width="30px" />
                                                            <col width="250px" />
                                                            <col width="17px" />
                                                            </colgroup>
                                                            <tr class= "headerstylegrid" style="font-weight:bold;">
                                                                <td>Synopsis Dt.</td> 
                                                                <td>Description</td>
                                                                <td style='text-align:center'><img src="../../HRD/Images/paperclipx12.png" style="border:none"  /></td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>Entered By/On</td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                        
                                             </table>
                                                </div>
                                                <div id="divSynopsis" runat="server"  style="overflow-y: scroll;overflow-x: hidden; height: 300px; vertical-align:top; ">
                                                    <table width="100%" rules="all" border="1" rules="rows"  cellpadding="3" cellspacing="0" style="border-collapse:collapse">
                                                       <colgroup>
                                                            <col width="90px" />
                                                            <col />
                                                            <col width="30px" />
                                                            <col width="30px" />
                                                            <col width="30px" />
                                                            <col width="30px" />
                                                            <col width="250px" />
                                                            <col width="17px" />
                                                            </colgroup>
                                                    <asp:Repeater ID="rptSynopsis" runat="server">
                                                        <ItemTemplate>
                                                            <tr valign="top" >
                                                                <td><%# Common.ToDateString(Eval("SynopsisDate"))%> </td>                                                                
                                                                <td >
                                                                    <table >
                                                                        <tr>
                                                                            <td >
                                                                                <asp:Label ID="lblAuditType" runat="server" Text='<%#Eval("ShortComment")%>' ToolTip="Click to view full comments."></asp:Label>
                                                                                <asp:Label ID="lblSysComments" runat="server" style="display:none; background-color:#ffffe0" Text='<%#Eval("SysComments")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="text-align:center; ">
                                                                    <a ID="ancSysView" runat="server" href='<%#"~/EMANAGERBLOB/LPSQE/Insurance\\" + Eval("SysFileName").ToString() %>' target="_blank" title="Show Doc" visible='<%#Eval("SysFileName").ToString()!=""%>'>
                                                                        <img src="../../HRD/Images/paperclipx12.png" style="border:none"   />
                                                                    </a>
                                                                </td>
                                                                <td style="text-align:center; ">
                                                                    <asp:ImageButton ID="imgSynopsisView" runat="server" ImageUrl="~/Modules/HRD/Images/magnifier.png" OnClick="imgSynopsisView_OnClick" />
                                                                </td>
                                                                <td style="text-align:center; ">
                                                                    <asp:ImageButton ID="imgSynopsisEdit" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgSynopsisEdit_OnClick" />
                                                                </td>
                                                                <td style="text-align:center; ">
                                                                    <asp:ImageButton ID="imgSynopsisDel" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgSynopsisDel_OnClick" OnClientClick="return confirm(&quot;Are you sure to delete?&quot;)" />
                                                                </td>
                                                                <td >
                                                                    <%#Eval("UploadedBy")%>/<%#Eval("UploadedDate")%><asp:HiddenField ID="hfSysID" runat="server" Value='<%#Eval("SysID") %>' />
                                                                    <asp:HiddenField ID="hfDocumentID" runat="server" 
                                                                        Value='<%#Eval("DocumentID") %>' />
                                                                    <asp:HiddenField ID="hfFile" runat="server" Value='<%#Eval("SysFileName") %>' />
                                                                </td>
                                                               <td>&nbsp;</td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    </table>
                                                </div>
                                                </div>
                                            </td>
                                        </tr>                                        
                                    </table>
                            <div id="DivAddSynopsis"  style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" visible="false" >
                            <center>
                            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                            <div style="position :relative; width:700px; height:320px; padding :3px; text-align :center; border :solid 10px black; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                                 <table width="100%" cellpadding="2" cellspacing="2" border="0">
                                     <colgroup>
                                         <col width="110px" />
                                         <col />
                                          <tr>
                                             <td colspan="2">
                                                 <b>Enter Description</b>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td colspan="2">
                                                 <asp:TextBox ID="txtSynopsisText" runat="server" Height="190px" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">
                                                 
                                                 <b>Synopsis Date :</b>
                                             </td>
                                             <td >
                                                 <asp:TextBox ID="txtSynopsisDt" runat="server" CssClass="input_box" Width="80px" ></asp:TextBox> 
                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtSynopsisDt"></ajaxToolkit:CalendarExtender>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td align="right">
                                                 <asp:TextBox ID="txtDocumentID" runat="server" CssClass="input_box" MaxLength="100" style="display:none;"></asp:TextBox>
                                                 <b>Attachment :</b>
                                             </td>
                                             <td style="text-align:left">
                                                 <asp:FileUpload ID="fuSunopsis" runat="server" CssClass="input_box" Width="400px" />
                                                 <a ID="aShoFile" runat="server" style="cursor:pointer;" target="_blank">
                                                 <img src="../../HRD/Images/magnifier.png" style="border:none;" />
                                                 </a>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td colspan="2" style="text-align:center;">
                                                 <asp:Button ID="btnSaveSynopsis" runat="server" CssClass="btn" Width="100px" OnClick="btnSaveSynopsis_OnClick" Text="Save" />
                                                 <asp:Button ID="btnCancelSynopsis" runat="server" CssClass="btn" Width="100px" OnClick="btnCancelSynopsis_OnClick" Text="Cancel" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td colspan="2" style="text-align:left">
                                                 <asp:Label ID="btnMsgSyn" runat="server" 
                                                     style="color:Red; font-weight:bold;float:right;padding-right:120px; float:right;"></asp:Label>
                                             </td>
                                         </tr>
                                     </colgroup>
                                </table>
                            </div>
                            
                        </center>
                            </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveSynopsis" />
                            </Triggers>
                            </asp:UpdatePanel>
                         </ContentTemplate>                         
                         </ajaxToolkit:TabPanel>
                         <ajaxToolkit:TabPanel ID="tbDocuments" runat="server" >
                         <HeaderTemplate>Documents</HeaderTemplate>
                         <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                 <table  id="tblDocument" runat="server"  border="0" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                     <tr style="font-weight:bold;">
                                        <td style="text-align:left">Description :</td>                                        
                                        <td align="right" style="width:250px">Attachment</td>
                                        <td style="width:200px; text-align:right"></td>
                                     </tr>
                                    <tr>
                                        <td style="text-align:left" >
                                             <asp:TextBox ID="txt_Desc" CssClass="input_box" MaxLength="50" runat="server" Width="95%" ></asp:TextBox>
                                         </td>
                                        <td style="text-align:left" >
                                             <asp:FileUpload ID="flAttachDocs" CssClass="input_box" Width="200px" runat="server" />
                                        </td>   
                                        <td style="text-align:right">
                                            <asp:Button ID="btnSaveDoc" Text="Save Document" CssClass="btn" runat="server" CausesValidation="true" onclick="btnSaveDoc_Click" />
                                            <asp:Button ID="btnCancelDoc" Text="Cancel" CssClass="btn" runat="server" CausesValidation="true" onclick="btnCancelDoc_Click" />
                                        </td>                                      
                                    </tr>
                                 </table>
                                 <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Font-Size="11px"></asp:Label>
                                 </ContentTemplate>
                                 <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveDoc" />
                            </Triggers>
                                 </asp:UpdatePanel>
                               <div style="border:solid 1px grey">
                               <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;margin-top:10px;">
                                    <colgroup>
                                        <col style="width: 30px;" />
                                        <col style="width: 50px;" />
                                        <col />
                                        <col style="width: 150px;" />
                                        <col style="width: 100px;"/>
                                        <col style="width: 50px;"/>
                                        <col style="width: 17px;" />
                                        <tr align="center" class= "headerstylegrid" style="font-weight:bold;">                                            
                                            <td>Edit</td>
                                            <td>Delete</td>
                                            <td align="left">Description</td>
                                            <td>Uploaded By</td>
                                            <td>Uploaded On</td>
                                            <td style="text-align:center;">    
                                                <img src="../../HRD/Images/paperclipx12.png" style="border:none"  />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </colgroup>
                                </table>
                               <div id="dvDocs" style="overflow-y: scroll;overflow-x: hidden; width: 100%; height: 260px; text-align: center;">
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
                                                        <asp:ImageButton ID="imgEditDoc" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEditDoc_OnClick" Visible='<%#(Mode!="V") %>' />
                                                        <asp:HiddenField ID="hfDocID" runat="server" Value='<%#Eval("DocID") %>' />
                                                    </td>
                                                    <td style="text-align:center;"><asp:ImageButton ID="imgDelDoc" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDelDoc_OnClick" OnClientClick="return confirm('Are you sure to delete?')" Visible='<%#(Mode!="V") %>'/></td>
                                                    <td><asp:Label ID="lblDesc" runat="server" Text='<%#Eval("Description")%>'></asp:Label></td>
                                                    <td align="center"><%#Eval("UploadedBy")%></td>
                                                    <td align="right"><%#Eval("UploadedDate")%></td>
                                                    <td style="text-align:center;">    
                                                        <a runat="server" ID="ancdoc"  href='<%#"~/EMANAGERBLOB/LPSQE/Insurance\\" + Eval("FileName").ToString() %>' target="_blank"  title="Show Doc" visible='<%#Eval("FileName")!=""%>' >
                                                       <img src="../../HRD/Images/paperclipx12.png" style="border:none"  /></a>  
                                                    </td>
                                                    <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        
                                    </table>
                                </div>
                                </div>                              

                         </ContentTemplate>                         
                         </ajaxToolkit:TabPanel>
                         <ajaxToolkit:TabPanel ID="tbExpensesAndRecovery" runat="server" >
                         <HeaderTemplate>Expenses & Recovery</HeaderTemplate>
                         <ContentTemplate>
                         <table id="tblExpence" runat="server" width="100%" cellpadding="0" cellspacing="0" border="0" >
                                       <tr>
                                           <td>
                                               <div style="font-size:11px; font-weight:bold; padding-bottom:4px; ">Record of Expenses to be Claimed</div>
                                               <div style="overflow-y: scroll;overflow-x: hidden; text-align: center; border:solid 1px #bbbbbb;">
                                                   <table cellpadding="2" cellspacing="0" rules="all"  border="1" width="100%" style="font-weight:bold;border-collapse:collapse">
                                                       <colgroup>
                                                          <col width="30px" />
                                                               <col width="30px" />
                                                               <col width="50px" />
                                                               <col width="85px" />
                                                               <col />
                                                               <col width="80px" />
                                                               <col width="70px" />
                                                               <col width="100px" />
                                                               <col width="100px" />
                                                               <col width="170px" />
                                                               <col width="35px" />
                                                               <col width="60px" />
                                                               <col width="150px" />
                                                               <col width="17px" />
                                                           <tr class= "headerstylegrid">
                                                               <td style="text-align:center;">
                                                               </td>
                                                               <td style="text-align:center;">
                                                               </td>
                                                               <td style="text-align:left;">
                                                                   Sr#</td>
                                                               <td style="text-align:center;">
                                                                   Date</td>
                                                               <td style="text-align:left;">
                                                                   Description</td>
                                                               <td style="text-align:center;">
                                                                   Local Curr</td>
                                                               <td style="text-align:right;">
                                                                   Rate</td>
                                                               <td style="text-align:right;">
                                                                   Amount</td>
                                                               <td style="text-align:right;">
                                                                   Total US$</td>
                                                               <td style="text-align:left;">
                                                                   Status</td>
                                                               <td style="text-align:center;">
                                                                   <img src="../../HRD/Images/paperclipx12.png" style="border:none"  />
                                                               </td>
                                                               <td style="text-align:left;">
                                                                   Select</td>
                                                               <td style="text-align:left;">
                                                                   Voucher#</td>
                                                               <td></td>
                                                           </tr>
                                                       </colgroup>
                                                   </table>
                                               </div>
                                               <asp:UpdatePanel ID="UP1" runat="server">
                                                   <ContentTemplate>
                                                       <div style="overflow-y: scroll;overflow-x: hidden; height: 160px; text-align: center; border:solid 1px #bbbbbb;">
                                                           <table cellpadding="2" cellspacing="0" rules="all" width="100%" border="1" style="border-collapse:collapse">
                                                               <col width="30px" />
                                                               <col width="30px" />
                                                               <col width="50px" />
                                                               <col width="85px" />
                                                               <col />
                                                               <col width="80px" />
                                                               <col width="70px" />
                                                               <col width="100px" />
                                                               <col width="100px" />
                                                               <col width="170px" />
                                                               <col width="35px" />
                                                               <col width="60px" />
                                                               <col width="150px" />
                                                               <col width="17px" />
                                                               
                                                               <asp:Repeater ID="rptExpences" runat="server">
                                                                   <ItemTemplate>
                                                                       <tr>
                                                                           <td style="text-align:center;">
                                                                               <asp:ImageButton ID="imgEditExp" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEditExp_OnClick" Visible='<%#(Mode!="V") %>' />
                                                                               <asp:HiddenField ID="hfExpID" runat="server" Value='<%#Eval("ExpID") %>' />
                                                                               <asp:HiddenField ID="hfExpStatusID" runat="server" Value='<%#Eval("StatusID") %>' />
                                                                           </td>
                                                                           <td style="text-align:center;">
                                                                               <asp:ImageButton ID="imgDelExp" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDelExp_OnClick" OnClientClick="return confirm('Are you sure to delete?')" Visible='<%#(Mode!="V") %>' />
                                                                           </td>
                                                                           <td>
                                                                               <%#Eval("Row")%>
                                                                           </td>
                                                                           <td>
                                                                               <asp:Label ID="lblExpDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                                                           </td>
                                                                           <td>
                                                                               <table >
                                                                                   <tr>
                                                                                       <td onclick="ShowFullComment(this)" style="cursor:pointer;">
                                                                                           <asp:Label ID="lblAuditType" runat="server" Text='<%#Eval("ShortComment")%>' 
                                                                                               ToolTip="Click to view full comments."></asp:Label>
                                                                                           <asp:Label ID="lblExpDesc" runat="server" 
                                                                                               style="display:none; background-color:#ffffe0" Text='<%#Eval("Description")%>'></asp:Label>
                                                                                       </td>
                                                                                   </tr>
                                                                               </table>
                                                                           </td>
                                                                           <td style="text-align:center;">
                                                                               <asp:Label ID="lblExpCurr" runat="server" Text='<%#Eval("LocalCurr")%>'></asp:Label>
                                                                           </td>
                                                                           <td style="text-align:right;">
                                                                               <asp:Label ID="lblRate" runat="server" Text='<%#FormatCurrency(Eval("Rate"))%>'></asp:Label>
                                                                           </td>
                                                                           <td style="text-align:right;">
                                                                               <asp:Label ID="lblExpAmount" runat="server" 
                                                                                   Text='<%#FormatCurrency(Eval("Amount"))%>'></asp:Label>
                                                                           </td>
                                                                           <td style="text-align:right;">
                                                                               <asp:Label ID="lblExpTotUSDoler" runat="server" Text='<%#FormatCurrency(Eval("TotalUSDoler"))%>'></asp:Label>
                                                                           </td>
                                                                           <td>
                                                                               <%#Eval("Status") %>
                                                                           </td>
                                                                           <td style="text-align:center;">
                                                                               <a ID="ancdoc" runat="server" href='<%#"~/EMANAGERBLOB/LPSQE/Insurance\\" + Eval("ClaimFile").ToString() %>' target="_blank" title="Show Doc" visible='<%#Eval("ClaimFile")!=""%>'>
                                                                               <img src="../../HRD/Images/paperclipx12.png" style="border:none"  />
                                                                               </a>
                                                                           </td>
                                                                           <td>
                                                                               <asp:CheckBox ID="chkClaimed" runat="server" Enabled='<%#Eval("Claimed").ToString().Trim()!="1"%>' />
                                                                           </td>
                                                                           <td>
                                                                               <asp:Label ID="lblBoucherNo" runat="server" Text='<%#Eval("BoucherNo")%>'></asp:Label>
                                                                           </td>
                                                                           <td>&nbsp;</td>
                                                                       </tr>
                                                                   </ItemTemplate>
                                                               </asp:Repeater>
                                                           </table>
                                                       </div>
                                                   </ContentTemplate>
                                               </asp:UpdatePanel>
                                               <div style="overflow-y: hidden;overflow-x: hidden; width: 100%; text-align: center; border:solid 1px #bbbbbb;">
                                                   <table cellpadding="2" cellspacing="0" rules="all" width="100%" border="1" style="border-collapse:collapse">
                                                       <colgroup>
                                                           <tr class= "headerstylegrid">
                                                               <td colspan="10" style="text-align:right; font-weight:bold; padding-right:150px;">
                                                                   Total Amount(US$)  &nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalClaimedAmount" runat="server" style="font-weight:bold;"></asp:Label>
                                                               </td>
                                                           </tr>
                                                       </colgroup>
                                                   </table>
                                               </div>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               <div style="text-align:right; padding:2px;">
                                               <asp:Button ID="btnOpenExpensesPopUp" runat="server" CssClass="btn" OnClick="btnOpenExpensesPopUp_OnClick" Text="Add Expense" />
                                               <asp:Button ID="btnSubmitExpensed" runat="server" CssClass="btn" OnClick="btnSubmitExpensed_OnClick" Text="Create Claim" />
                                               </div>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td>
                                               <div style="font-size:11px; font-weight:bold; padding-bottom:4px; ">Record of Submitted Claims</div>
                                               <div style="overflow-y: hidden;overflow-x: hidden; text-align: center; border:solid 1px #bbbbbb;">
                                                   <table cellpadding="2" cellspacing="0" border="1" rules="all" style="font-weight:bold; border-collapse:collapse;" width="100%">
                                                       <colgroup>
                                                           <col width="50px" />
                                                           <col width="160px" />
                                                           <col width="100px" />
                                                           <col width="120px" />
                                                           <col width="120px" />
                                                           <col width="120px" />
                                                           <col />
                                                           <col width="17px" />
                                                           </colgroup>
                                                           <tr class= "headerstylegrid">
                                                               <td style="text-align:center">
                                                                   Sr No</td>
                                                               <td style="text-align:left">
                                                                   Submitted By</td>
                                                               <td style="text-align:center">
                                                                   Submitted On</td>
                                                               <td style="text-align:center">
                                                                   Claim Amt(US$)</td>
                                                               <td style="text-align:center">
                                                                   Recovery Status</td>
                                                               <td style="text-align:center">
                                                                   Amt Recd(US$)</td>
                                                               <td style="text-align:center">
                                                                   Remarks</td>
                                                               <td>
                                                               </td>
                                                           </tr>                                                       
                                                   </table>
                                               </div>
                                               <div style="overflow-y: scroll;overflow-x: hidden; height: 160px; text-align: center; border:solid 1px #bbbbbb;">
                                                   <table cellpadding="2" cellspacing="0" rules="all" width="100%" border="1" style="border-collapse:collapse;">
                                                       <colgroup>
                                                           <col width="50px" />
                                                           <col width="160px" />
                                                           <col width="100px" />
                                                           <col width="120px" />
                                                           <col width="120px" />
                                                           <col width="120px" />
                                                           <col />
                                                       </colgroup>                                                   
                                                   <asp:Repeater ID="rptSubmitedExpenses" runat="server">
                                                       <ItemTemplate>
                                                           <tr style="vertical-align:top;">
                                                               <td style="text-align:center">
                                                                   <asp:LinkButton ID="lnkViewSubmitedClaimDetails" runat="server" 
                                                                       OnClick="lnkViewSubmitedClaimDetails_OnClick" Text='<%#Eval("Row")%> '></asp:LinkButton>
                                                                   <asp:HiddenField ID="hfClaimedID" runat="server" 
                                                                       Value='<%#Eval("ClaimedID")%>' />
                                                               </td>
                                                               <td>
                                                                   <%#Eval("SubmitedBy")%>
                                                               </td>
                                                               <td style="text-align:center;">
                                                                   <%#Eval("SubmitedOn")%>
                                                               </td>
                                                               <td style="text-align:right;">
                                                                   <%#FormatCurrency(Eval("TotalAmountUS$"))%>
                                                               </td>
                                                               <td>
                                                                   <%#Eval("RcvStatus")%>
                                                               </td>
                                                               <td style="text-align:right;">
                                                                   <%#FormatCurrency(Eval("TotalRecoveredAmount"))%>
                                                               </td>
                                                               <td>
                                                                   <table width="450px">
                                                                       <tr>
                                                                           <td onclick="ShowFullComment(this)" style="cursor:pointer;">
                                                                               <asp:Label ID="lblAuditType" runat="server" Text='<%#Eval("ShortComment")%>' 
                                                                                   ToolTip="Click to view full comments."></asp:Label>
                                                                               <asp:Label ID="lblSysComments" runat="server" 
                                                                                   style="display:none; background-color:#ffffe0" Text='<%#Eval("Remarks")%>'></asp:Label>
                                                                           </td>
                                                                       </tr>
                                                                   </table>
                                                               </td>
                                                           </tr>
                                                       </ItemTemplate>
                                                   </asp:Repeater>
                                                   </table>
                                               </div>
                                               <div style="overflow-y: hidden;overflow-x: hidden; width: 100%; text-align: center; border:solid 1px #bbbbbb;">
                                                   <table cellpadding="0" cellspacing="0" rules="all" style="font-weight:bold;" width="100%">
                                                       <tr class= "headerstylegrid">
                                                           <td style="padding-left:150px;">Total Claim Amount(US$) &nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalClaimedAmountOnSubmitedClaim" runat="server"></asp:Label>
                                                           </td>
                                                           <td style="text-align:right;padding-right:150px;">Total Received Amount(US$)&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecoveredAmount" runat="server"></asp:Label>
                                                           </td>
                                                       </tr>
                                                   </table>
                                               </div>
                                           </td>
                                       </tr>
                            </table>
                         <%--Add Expenses Popup --%>
                         <asp:UpdatePanel ID="up3" runat="server" >
                                            <ContentTemplate>
                                            
                                         <div id="divAddExpensesPopup"  style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;display:none" runat="server" >
                                            <center>
                                                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                                <div style="position :relative; width:700px; height:300px; padding :3px; text-align :center; border :solid 10px black;  background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                                                    <div style="text-align:center;font-size:11px ;  font-weight:bold; padding:5px;" class="text headerband"><b>Expenses</b></div>
                                                    <table id="Table1" runat="server" width="100%" cellpadding="2" cellspacing="0" border="0" >
                                                        <tr>
                                                            <td style="width:100px">Date</td>
                                                            <td ><asp:TextBox ID="txtExpDate"  runat="server" CssClass="input_box" Width="80px"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtExpDate">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Description</td>  
                                                            <td ><asp:TextBox ID="txtExpDesc"  runat="server" CssClass="input_box" TextMode="MultiLine" Width="98%" Height="50px" MaxLength="500"></asp:TextBox></td>
                                                        </tr>
                                                         <tr>
                                                            <td>Local Curr</td>  
                                                            <td><asp:DropDownList ID="ddlLocalCurr" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlLocalCurr_OnSelectedIndexChanged" ></asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Rate</td>
                                                            <td><asp:TextBox ID="txtRate" MaxLength="10"  runat="server" CssClass="input_box" onkeypress="fncInputNumericValuesOnly(event)" OnTextChanged="txtRate_txtAmount" AutoPostBack="true" Width="50px" ></asp:TextBox></td>
                                                        </tr>
                                                         <tr>
                                                             <td>Amount</td>
                                                            <td><asp:TextBox ID="txtAmount" MaxLength="10" runat="server" CssClass="input_box" onkeypress="fncInputNumericValuesOnly(event)" OnTextChanged="OnTextChanged_txtAmount" AutoPostBack="true" ></asp:TextBox></td>                                 
                                                        </tr>
                                                        <tr>
                                                            <td>Total US$</td>   
                                                            <td><asp:Label ID="lblTotalUSDoler" runat="server"></asp:Label></td>                                 
                                                         </tr>
                                                         <tr>
                                                            <td>Status</td>      
                                                            <td><asp:DropDownList ID="ddlExpensesStatus" runat="server" CssClass="input_box" Width="130px" >
                                                                <asp:ListItem Text="< Select >" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Committed Cost" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="In Progress" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            </td>                              
                                                        </tr>          
                                                        <tr>
                                                            <td>Voucher#</td>
                                                            <td ><asp:TextBox ID="txtBoucherNo" MaxLength="50"  runat="server" CssClass="input_box" Width="200px" ></asp:TextBox></td>
                                                        </tr>          
                                                        <tr>
                                                            <td>Attachment : </td>
                                                            <td ><asp:FileUpload ID="fuExpence" runat="server" CssClass="input_box" Width="200px"/></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align:center">
                                                                
                                                                <asp:Button ID="btnSaveExpence" runat="server"  OnClick="btnSaveExpence_OnClick" Text="Save" CssClass="btn" Width="100px"/>
                                                                <asp:Button ID="btnCancelExpences" runat="server"  OnClick="btnCancelExpences_OnClick" Text="Cancel" CssClass="btn" Width="100px" />
                                                            </td>
                                                        </tr>    
                                                        <tr>
                                                            <td colspan="2">
                                                            <asp:Label ID="lblMsgExpence" runat="server" ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>      
                                                    </table>                
                                                </div>
                                                
                                            </center>
                                         </div>
                                         </ContentTemplate>
                                         <Triggers >
                                            <asp:PostBackTrigger ControlID="btnSaveExpence" />
                                         </Triggers>
                                        </asp:UpdatePanel>
                         <%--Submit Expenses Popup --%>
                         <asp:UpdatePanel ID="UP7" runat="server" >
                         <ContentTemplate>
                                         <div id="divSubmitExpenses"  style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" visible="false" >
                                            <center>
                                                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                                <div style="position :relative; width:1150px; height:450px; padding :3px; text-align :center;  border :solid 10px black; background : white; z-index:150;top:150px;opacity:1;filter:alpha(opacity=100)">
                                                    <div style="text-align:center;font-size:11px ; font-family:Verdana;padding:5px; background-color:#c2c2c2"><b>Create Claims</b></div>
                                                    <table cellpadding="2" width="100%" cellspacing="1" border="0" style="margin-top:0px;">
                                                        <tr>
                                                            <td>
                                                                <b> Submitted By :&nbsp;&nbsp;</b><asp:Label ID="txtExpSubmetedBy" runat="server"  style="font-weight:bold;" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Submitted On :&nbsp;&nbsp;</b>
                                                                <asp:TextBox ID="txtExpSubmitedOn" runat="server" CssClass="input_box" Width="80px"></asp:TextBox>        
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtExpSubmitedOn">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Remarks :</b> <br />
                                                                <asp:TextBox ID="txtExpRemarks" runat="server" CssClass="input_box" TextMode="MultiLine" Width="95%" Height="70px" style="margin-left:20px;"></asp:TextBox>        
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <br />
                                                                <b> Record Of Expenses :</b><br />
                                                                <div style="overflow-y: hidden;overflow-x: hidden; width: 95%; text-align: center; border:solid 1px #bbbbbb; margin-left:20px;" >
                                                                    <table cellpadding="2" cellspacing="0" rules="all"  border="1" style="border-collapse:collapse; " width="100%" >
                                                                        <colgroup>
                                                                            <col width="50px" />
                                                                            <col width="80px" />
                                                                            <col  />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="17px" />
                                                                            </colgroup>
                                                                            <tr class= "headerstylegrid" style="text-align:center;">
                                                                                <td style="text-align:center;">
                                                                                    Sr No</td>
                                                                                <td style="text-align:center;">
                                                                                    Date</td>
                                                                                <td style="text-align:center;">
                                                                                    Description</td>
                                                                                <td style="text-align:center;">
                                                                                    Currency</td>
                                                                                <td style="text-align:center;">
                                                                                    Local Amount</td>
                                                                                <td style="text-align:center;">
                                                                                    Rate</td>
                                                                                <td style="text-align:center;">
                                                                                    Amount(US$)
                                                                                </td>
                                                                                <td></td>
                                                                            </tr>
                                                                </table>
                                                                </div>
                                                                <div style="overflow-y: scroll;overflow-x: hidden; width: 95%; height: 160px; text-align: center; border:solid 1px #bbbbbb; margin-left:20px;" >
                                                                    <table cellpadding="2" cellspacing="0" border="1" style="border-collapse:collapse;" width="100%" >
                                                                        <colgroup>
                                                                            <col width="50px" />
                                                                            <col width="80px" />
                                                                            <col />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="17px" />
                                                                                
                                                                        </colgroup>                                                                    
                                                                    <asp:Repeater ID="rptViewExpenses" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <%#Eval("Row")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%#Eval("Date")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%#Eval("Description")%>
                                                                                </td>
                                                                                <td style="text-align:center;">
                                                                                    <%#Eval("LocalCurr")%>
                                                                                </td>
                                                                                <td style="text-align:right;">
                                                                                    <%#FormatCurrency(Eval("Amount"))%>
                                                                                </td>
                                                                                <td style="text-align:right;">
                                                                                    <%#FormatCurrency(Eval("Rate"))%>
                                                                                </td>
                                                                                <td style="text-align:right;">
                                                                                    <%#FormatCurrency(Eval("TotalUSDoler"))%>
                                                                                </td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    </table>
                                                                </div>
                                                                <div style="overflow-y: hidden;overflow-x: hidden; width: 95%; text-align: center; border:solid 1px #bbbbbb; margin-left:20px;" >
                                                                    <table cellpadding="2" cellspacing="0" rules="all" style="border-collapse:collapse;" width="100%" border="0">
                                                                        <colgroup>
                                                                            <col width="50px" />
                                                                            <col width="80px" />
                                                                            <col  />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="17px" />
                                                                          </colgroup>
                                                                            <tr class= "headerstylegrid" style="text-align:center;">
                                                                                <td colspan="6" style="text-align:right;">
                                                                                    Total Amount (US$)</td>
                                                                                <td style="text-align:right;">
                                                                                    <asp:Label ID="lblTotAmtSubmited" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>                                                                        
                                                                    </table>
                                                                </div>    
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                
                                                                <asp:Button ID="btnCancelSubmitExpenses" runat="server"  OnClick="btnCancelSubmitExpenses_OnClick" Text="Cancel" CssClass="btn" style="float:right;margin:2px;" />
                                                                <asp:Button ID="btnSaveExpenses" runat="server"  OnClick="btnSaveExpenses_OnClick" Text="Save" CssClass="btn" style="float:right;margin:2px;" />
                                                                <asp:Label ID="lblMsgSubmitClaim" runat="server" ForeColor="Red" style="float:right;" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </center>
                                         </div>
                                         </ContentTemplate>
                         <Triggers >
                         <asp:PostBackTrigger ControlID="btnSaveExpenses" />
                         </Triggers>
                          </asp:UpdatePanel>
                          <%--Recover Expenses Popup per record--%>
                          <div id="divViewSubmitedClaimDetails"  style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" visible="false" >
                                            <center>
                                                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                                <div style="position :relative; width:1150px; height:450px; padding :3px; text-align :center; border :solid 10px black; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                                                    
                                                    <table width="100%" cellpadding="2" cellspacing="2" rules="none" style="font-weight:bold;" >    
                                                        <colgroup>
                                                            <col width="130px;" />
                                                            <col />
                                                            <tr class= "headerstylegrid">
                                                                <td colspan="2" 
                                                                    style="text-align:center;height:22px; font-size:medium ; font-family:Verdana; font-weight:bold;">
                                                                    Record of Submitted Claims</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Submitted By :</td>
                                                                <td>
                                                                    <asp:Label ID="lblExpRcvSubmitedBy" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Submitted On :</td>
                                                                <td>
                                                                    <asp:Label ID="lblExpRcvSubmitedOn" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    Remarks
                                                                    <br />
                                                                    <asp:Label ID="lblExpRcvRemarks" runat="server" 
                                                                        style="font-weight:normal;margin-left:10px;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </colgroup>
                                                    </table>
                                                    <br />
                                                    <table width="100%" cellpadding="2" cellspacing="2" rules="all" style="font-weight:bold;" >    
                                                        <colgroup>
                                                            <col width="40px" />
                                                            <col width="170px" />
                                                            <col />
                                                            <col width="130px" />
                                                            <col width="120px" />
                                                            <col width="120px" />
                                                            <col width="120px" />
                                                            <col width="130px" />
                                                            <tr class= "headerstylegrid">
                                                                <td>
                                                                    Sr No</td>
                                                                <td>
                                                                    Date</td>
                                                                <td>
                                                                    Description</td>
                                                                <td>
                                                                    Currency</td>
                                                                <td>
                                                                    Local Amt</td>
                                                                <td>
                                                                    Rate</td>
                                                                <td>
                                                                    Amount(US$)</td>
                                                                <td>
                                                                    Recovered Amount</td>
                                                            </tr>
                                                        </colgroup>
                                                        </table>
                                                        
                                                        <table width="100%" cellpadding="2" cellspacing="2" rules="all"  >
                                                            <colgroup>
                                                                <col width="40px" />
                                                                <col width="170px" />
                                                                <col />
                                                                <col width="130px" />
                                                                <col width="120px" />
                                                                <col width="120px" />
                                                                <col width="120px" />
                                                                <col width="130px" />
                                                            </colgroup>
                                                        </table>
                                                        
                                                        <asp:Repeater ID="rptRecoverClaim" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%#Eval("Row")%>
                                                                        <asp:HiddenField ID="hfExpID" runat="server" Value='<%#Eval("ExpID")%>' />
                                                                        <asp:HiddenField ID="hfClaimedID" runat="server" 
                                                                            Value='<%#Eval("ClaimedID")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("SubmitedOn")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("Remarks")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("LocalCurr")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#FormatCurrency( Eval("LocalAmount"))%>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("Rate")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#FormatCurrency(Eval("AmountUSDoler"))%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtExpRecoveryAmount" runat="server" CssClass="input_box" 
                                                                            onkeypress="fncInputNumericValuesOnly(event)" 
                                                                            Text='<%# FormatCurrency( Eval("RecoveredAmount")) %>' Width="90px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                    </asp:Repeater>
                                                    <table>
                                                    </table>
                                                        
                                                        <table width="100%" cellpadding="2" cellspacing="2" rules="all" style="font-weight:bold;" >    
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnCancelRecoverClaim" runat="server"  OnClick="btnCancelRecoverClaim_OnClick" Text="Cancel" CssClass="btn" style="float:right; margin:2px;" />
                                                                    <asp:Button ID="btnSaveRecoverClaimed" runat="server"  OnClick="btnSaveRecoverClaimed_OnClick" Text="Save" CssClass="btn" style="float:right; margin:2px;" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                </div>
                                                
                                            </center>
                                         </div>
                          <%--Recover Expenses Popup --%>
                          <div id="DivUpdateRecoverAmount" style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server" visible="false" >
                                            <center>
                                                <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                                                <div style="position :relative; width:1150px; height:580px; padding :3px; text-align :center; border :solid 10px black; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                                                    <div style="text-align:center;font-size:11px ; font-family:Verdana; font-weight:bold; padding:5px; background-color:#c2c2c2"><b>Update Recovery of Claims</b></div>
                                                    
                                                    <table cellpadding="2" width="100%" cellspacing="1" border="0" style="margin-top:0px;" >
                                                        <tr>
                                                            <td>
                                                                <div style="padding:3px;"><b> Record Of Expenses  </b></div>
                                                                <div style="overflow-y: hidden;overflow-x: hidden; width: 95%; text-align: center; border:solid 1px #bbbbbb; margin-left:20px;">
                                                                    <table cellpadding="2" cellspacing="0" rules="rows" style="border-collapse:collapse; " width="100%" border="1" >
                                                                        <colgroup>
                                                                            <col width="50px" />
                                                                            <col width="80px" />
                                                                            <col  />
                                                                             <col width="50px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="120px" />
                                                                            <col width="17px" />
                                                                        </colgroup>
                                                                            <tr class= "headerstylegrid">
                                                                                <td style="text-align:center;">
                                                                                    Sr No</td>
                                                                                <td style="text-align:center;">
                                                                                    Date</td>
                                                                                <td style="text-align:left;">
                                                                                    Description</td>
                                                                                    <td>&nbsp;</td>
                                                                                <td style="text-align:left;">
                                                                                    Currency</td>
                                                                                <td style="text-align:right;">
                                                                                    Local Amount</td>
                                                                                <td style="text-align:right;">
                                                                                    Rate</td>
                                                                                <td style="text-align:right;">
                                                                                    Amount(US$)
                                                                                </td>
                                                                                <td style="text-align:right;">
                                                                                    Amount Recd(US$)
                                                                                </td>
                                                                                <td></td>
                                                                            </tr>
                                                                </table>
                                                                </div>
                                                                <div style="overflow-y: scroll;overflow-x: hidden; width: 95%; height: 160px; text-align: center; border:solid 1px #bbbbbb; margin-left:20px;">
                                                                    <table cellpadding="2" cellspacing="0" rules="rows" style="border-collapse:collapse;" width="100%" border="1" >
                                                                        <colgroup>
                                                                            <col width="50px" />
                                                                            <col width="80px" />
                                                                            <col  />
                                                                             <col width="50px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="120px" />
                                                                            <col width="17px" />
                                                                        </colgroup>
                                                                    <asp:Repeater ID="rptViewRecordOfExpenses" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align:center">
                                                                                    <%#Eval("Row")%>
                                                                                    <asp:HiddenField ID="hfExpID" runat="server" Value='<%#Eval("ExpID")%>' />
                                                                                    <asp:HiddenField ID="hfClaimedID" runat="server" 
                                                                                        Value='<%#Eval("ClaimedID")%>' />
                                                                                </td>
                                                                                <td>
                                                                                    <%#Eval("Date")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%#Eval("Description")%>
                                                                                </td>
                                                                                 <td style="text-align:center;">
                                                                               <a ID="ancdoc" runat="server" href='<%#"~/EMANAGERBLOB/LPSQE/Insurance\\" + Eval("ClaimFile").ToString() %>' target="_blank" title="Show Doc" visible='<%#Eval("ClaimFile")!=""%>'>
                                                                               <img src="../../HRD/Images/paperclipx12.png" style="border:none"  />
                                                                               </a>
                                                                           </td>
                                                                                <td>
                                                                                    <%#Eval("LocalCurr")%>
                                                                                </td>
                                                                                <td style="text-align:right;">
                                                                                    <%#FormatCurrency(Eval("Amount"))%>
                                                                                </td>
                                                                                <td style="text-align:right;">
                                                                                    <%#FormatCurrency(Eval("Rate"))%>
                                                                                </td>
                                                                                <td style="text-align:right;">
                                                                                    <%#FormatCurrency(Eval("TotalUSDoler"))%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtExpRecdAmount" runat="server" AutoPostBack="true" 
                                                                                        CssClass="input_box" onkeypress="fncInputNumericValuesOnly(event)" 
                                                                                        OnTextChanged="txtExpRecdAmount_OnTextChanged" style="text-align:right;" 
                                                                                        Text='<%#FormatCurrency(Eval("RecoveredAmount"))%>' Width="110px"></asp:TextBox>
                                                                                </td>
                                                                                <td></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    </table>
                                                                </div>
                                                                <div style="overflow-y: hidden;overflow-x: hidden; width: 95%; text-align: center; border:solid 1px #bbbbbb; margin-left:20px;">
                                                                    <table cellpadding="2" cellspacing="0" rules="all" style="border-collapse:collapse;" width="100%" border="0">
                                                                        <colgroup>
                                                                            <col width="50px" />
                                                                            <col width="80px" />
                                                                            <col  />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="100px" />
                                                                            <col width="120px" />
                                                                            <col width="17px" />
                                                                            <tr class= "headerstylegrid" style="text-align:center;">
                                                                                <td colspan="6" style="text-align:right;">
                                                                                    Total Amount (US$)</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblTotAmountUpdateRecovery" runat="server" 
                                                                                        style="float:right;padding-right:17px;"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblTotRecoverAmountUR" runat="server" 
                                                                                        style="float:right;padding-right:17px;"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                        </colgroup>
                                                                    </table>
                                                                </div>    
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right; padding-right:60px;">
                                                                    <asp:Label ID="lblRecoveryAmtTxt" runat="server" Text="Var. From Claim(US$)" Width="160px" style="font-weight:bold;" ></asp:Label><b> :  </b>
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblRcvLessRecovered" runat="server" ></asp:Label>
                                                                    <%--<asp:TextBox ID="txtRcvRecoveryAmount" runat="server" CssClass="input_box" Width="90px" style="text-align:right;margin-right:50px;" ></asp:TextBox>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="padding:3px;"><b> Remarks  </b></div>
                                                                    <div style="overflow-y: scroll;overflow-x: hidden; width: 95%; height: 150px; text-align: center; border:solid 1px #bbbbbb; margin-left:20px; text-align:left;">
                                                                        <asp:Label ID="lblRcvRemarks" runat="server" Width="95%" style="padding-left:10px;" ></asp:Label>        
                                                                    </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-right:50px;">
                                                                
                                                                <asp:Button ID="btnRcvCancelRecovery" runat="server"  OnClick="btnRcvCancelRecovery_OnClick" Text="Cancel" CssClass="btn" Width="100px" style="float:right;margin:2px;" />
                                                                <asp:Button ID="btnRcvRecovery" runat="server"  OnClick="btnRcvRecovery_OnClick" Text="Save" CssClass="btn" Width="100px" style="float:right;margin:2px;" />
                                                                <asp:Label ID="lblMsgRecovery" runat="server" ForeColor="Red" style="float:right;" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="display:none;">
                                                            <td>
                                                                <br />
                                                                   <asp:Label ID="lblTotRcv" runat="server"  Text="Total Recovered Amount " Width="160px" style="font-weight:bold;"></asp:Label><b> :  </b>
                                                                &nbsp;&nbsp; <asp:Label ID="lblRcvTotRcvAmt" runat="server" ></asp:Label>
                                                                <br />
                                                                <asp:Label ID="lblLessrcv" runat="server" Text="Less Recovered " Width="160px" style="font-weight:bold;"></asp:Label><b> :  </b>
                                                                &nbsp;&nbsp; <%--<asp:Label ID="lblRcvLessRecovered" runat="server" ></asp:Label>--%>
                                                            </td>
                                                        </tr>
                                                        
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblRcvStatusText" runat="server" Text="Recovery Status" Width="160px" style="font-weight:bold;" ></asp:Label><b> : </b> 
                                                                 &nbsp;
                                                                <asp:DropDownList ID="ddlRcvRecoveryStatus" runat="server"  CssClass="input_box">
                                                                    <asp:ListItem Text="< Select >" Value="0" Selected="True"></asp:ListItem>    
                                                                    <asp:ListItem Text="Approved" Value="1"></asp:ListItem>    
                                                                    <asp:ListItem Text="Partly Approved" Value="2"></asp:ListItem>    
                                                                    <asp:ListItem Text="Not Approved" Value="3"></asp:ListItem>    
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>                                                        
                                                        <tr >
                                                            <td>
                                                                <b> Submitted By :&nbsp;&nbsp;</b>
                                                                <asp:Label ID="lblRcvSubmitedBy" runat="server"   ></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <b>Submitted On :&nbsp;&nbsp;</b>
                                                                <asp:Label ID="lblRcvSubmitedOn" runat="server" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                
                                            </center>
                                         </div>
                          <table width="99%" cellpadding="2" cellspacing="2" border="0" style="display:none;">
                                <col width="80px" />
                                <col width="400px" />
                                <col width="100px" />
                                <col />
                                <tr>
                                    <td colspan="4">Details </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtExpencesClaimedDetails" runat="server" TextMode="MultiLine" Width="100%" Height="80px" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Amount :</td>
                                    <td>
                                        <asp:TextBox ID="txtExpClaimAmount" runat="server" CssClass="input_box" onkeypress="fncInputNumericValuesOnly(event)"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Local Cur :</td>
                                    <td>
                                        <asp:TextBox ID="txtLocalCurr" runat="server" CssClass="input_box" onkeypress="fncInputNumericValuesOnly(event)"></asp:TextBox>
                                    </td>
                                    <td>Total US$ :</td>
                                    <td>
                                        <asp:TextBox ID="txtTotUSDoler" runat="server" CssClass="input_box" onkeypress="fncInputNumericValuesOnly(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td>Claimed :</td>
                                    <td>
                                        <asp:TextBox ID="txtClaimed" runat="server" CssClass="input_box" ></asp:TextBox>
                                    </td>
                                    <td>Date of Claim :</td>
                                    <td>
                                        <asp:TextBox ID="txtDateOfClaim" runat="server" CssClass="input_box" Width="80px" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtDateOfClaim">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                         </ContentTemplate>                         
                         </ajaxToolkit:TabPanel>

                         <ajaxToolkit:TabPanel ID="tbClosure" runat="server" >
                         <HeaderTemplate>Case Closure</HeaderTemplate>
                         <ContentTemplate>
                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td>
                                            <table width="99%">
                                                <tr>
                                                    <td><b> <br /> Closure Comments </b></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtClosureComments" runat="server" TextMode="MultiLine" Width="99%" Height="150px" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:right:">
                                                        <span style="float:left;"> 
                                                            Closure Date : <asp:TextBox ID="txtClosureDate" runat="server" CssClass="input_box" Width="80px" ></asp:TextBox> 
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtClosureDate"></ajaxToolkit:CalendarExtender>
                                                        </span>
                                            
                                            
                                                        <%--<asp:Button ID="btnCloseCaseClosure" runat="server" Text="Close" CssClass="input_box" OnClick="btnCloseCaseClosure_OnClick"  style="width:80px; margin-right:3px;float:right;" />--%>
                                                        <asp:Button ID="btnSaveCaseClosure" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveCaseClosure_OnClick"  style="width:80px; margin-right:3px;float:right;" />
                                                        <asp:Label ID="lblMsgCaseClosure" runat="server" style="color:Red;font-weight:bold;float:right; margin-right:5px;" ></asp:Label>                                            
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                         </ContentTemplate>                         
                         </ajaxToolkit:TabPanel>     
                         <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" >
                         <HeaderTemplate>Office Remarks</HeaderTemplate>
                         <ContentTemplate>
                         <asp:UpdatePanel ID="UP6" runat="server">
                         <ContentTemplate>
                         <div style="text-align:center;font-size:11px ; font-family:Verdana;padding:5px; background-color:#c2c2c2"><b>Office Remarks</b></div>
                         <table width="100%" cellpadding="2" cellspacing="2" border="1" bordercolor="#C0C0C0" style="border-collapse:collapse;" rules="none"   >
                                                <tr>
                                                    <td style="font-weight:bold;font-size:15px; text-align:left;">
                                                         <asp:Button ID="btnEnterOffComment" runat="server" Text="Add Office Remarks" CssClass="btn" OnClick="btnEnterOffComment_OnClick" Width="150px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table width="100%" cellpadding="0" cellspacing="0" >
                                                            <tr>
                                                                <td>
                                                                    <div id="div2" runat="server"  style="overflow-y: hidden;overflow-x: hidden; width: 100%; ">
                                                                        <table cellpadding="2" cellspacing="0" width="100%"  rules="all" border="1" style="border-collapse:collapse;" >
                                                                            <colgroup>
                                                                                <col width="40px" />
                                                                                <col width="40px" />
                                                                                <col />
                                                                                <col width="190px" />
                                                                                <col width="17px" />
                                                                                 </colgroup>
                                                                                <tr class= "headerstylegrid" style="font-weight:bold;">
                                                                                    <td style="text-align:center">Edit</td>
                                                                                    <td style="text-align:center">Del</td>
                                                                                    <td>Description</td>
                                                                                    <td>Entered By/On</td>
                                                                                    <td>&nbsp;</td>
                                                                                </tr>
                                                                     </table>
                                                                     </div>
                                                                     <div id="div1" runat="server"  style="overflow-y: scroll;overflow-x: hidden; width: 100%; height: 130px; ">
                                                                     <table cellpadding="2" cellspacing="0" width="100%" rules="all" border="1" style="border-collapse:collapse;"  >
                                                                         <colgroup>
                                                                             <col width="40px" />
                                                                                <col width="40px" />
                                                                                <col />
                                                                                <col width="190px" />
                                                                                <col width="17px" />
                                                                         </colgroup>
                                                                         <asp:Repeater ID="rptOfficeComment" runat="server">
                                                                             <ItemTemplate>
                                                                                 <tr valign="top">
                                                                                     <td style="text-align:center"><asp:ImageButton ID="imgEditOffRem" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEditOffRem_OnClick" Visible='<%#(Mode!="V") %>' /><asp:HiddenField ID="hfOffID" runat="server" Value='<%#Eval("OffID") %>' /></td>
                                                                                     <td style="text-align:center"><asp:ImageButton ID="imgDelOffRem" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDelOffRem_OnClick" OnClientClick="return confirm('Are you sure to delete?')" /></td>
                                                                                     <td>
                                                                                         <table width="100%" rules="all" border="0" style="border-collapse:collapse;"  >
                                                                                             <tr>
                                                                                                 <td onclick="ShowFullComment(this)" style="cursor:pointer;">
                                                                                                     <asp:Label ID="lblAuditType" runat="server" Text='<%#Eval("ShortComment")%>' 
                                                                                                         ToolTip="Click to view full comments."></asp:Label>
                                                                                                     <asp:Label ID="lblSysComments" runat="server" 
                                                                                                         style="display:none; background-color:#ffffe0" Text='<%#Eval("FullComment")%>'></asp:Label>
                                                                                                 </td>
                                                                                             </tr>
                                                                                         </table>
                                                                                     </td>
                                                                                     <td><%#Eval("UpdatedBy")%>/ <%#Eval("UpdatedOn")%></td>
                                                                                     <td>&nbsp;</td>
                                                                                 </tr>
                                                                             </ItemTemplate>
                                                                         </asp:Repeater>
                                                                         </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                         </table>                                                         
                                                    </td>
                                                </tr>
                                               
                                            </table>
                         <%--Office Remarks  --%>    
                         <div id="divOfficeRemarks" style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100; " runat="server" visible="false" >
                         <center>
                            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
                            <div style="position :relative; width:500px; height:260px; padding :3px; text-align :center; border :solid 10px black; background : white; z-index:150;top:150px;opacity:1;filter:alpha(opacity=100)">
                                 <table width="100%" cellpadding="2" cellspacing="2" id="tblOfficeComment" runat="server" >
                                    <tr>
                                        <td>
                                            <b>Remarks <br /></b> 
                                            <asp:TextBox ID="txtOfficeComm" runat="server" TextMode="MultiLine" Width="99%" Height="190px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right;">
                                            <asp:Label ID="lblMSGOff" runat="server" style="color:Red;"></asp:Label>
                                            <asp:Button ID="btnSaveOffComment" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveOffComment_OnClick"/>
                                            <asp:Button ID="btnCancelOffComment" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancelOffComment_OnClick"/>
                                        </td>
                                    </tr>
                                    
                                </table>
                                 
                            </div>
                            
                        </center>
                     </div>
                    </ContentTemplate>                    
                    </asp:UpdatePanel>
                         </ContentTemplate>
                         </ajaxToolkit:TabPanel>                    
                         </ajaxToolkit:TabContainer>
                    </td>
                   </tr>
                    </table>                    
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
           <tr>
                <td style="vertical-align:top;">
                  <fieldset>
                        
                                     
                        <legend><b>Claim Summary :</b></legend>
                            <table width="100%" cellpadding="2" cellspacing="2" >
                                <col width="150px" />
                                <col  />
                                <tr>
                                    <td style="width:200px;">    
                                        Policy Deductible Amount (US$) :
                                    </td>
                                    <td>
                                        <asp:Label ID="txtDeductibleAmt" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:200px;">    
                                         Case Deductible Amount (US$) :
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCaseDeductibleAmt" MaxLength="12" runat="server" CssClass="input_box" onkeypress="fncInputNumericValuesOnly(event)"></asp:TextBox>
                                    </td>
                                </tr>
                                   <tr>
                                    <td style="width:150px;" align="right">                                        
                                    Claim Amount (US$) : </td>
                                    <td align="left">
                                        <asp:Label ID="txtClaimAmmount" runat="server" > </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Total Received Amount (US$) :</td>
                                    <td>
                                        <asp:Label ID="lblTotalAmountRecd" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                            </table>
                    </fieldset>
                </td>
           </tr>
           <tr>
            <td  style="text-align:left; padding-left:10px;">
                <div style="text-align:right;float:right; ">
                <asp:Label ID="lblPageMessage" runat="server" ForeColor="Red" Width="250px" ></asp:Label>
                <asp:Button ID="btnCaseSave" runat="server" CssClass="btn" Width="80px" Text="Save" OnClick="btnCaseSave_OnClick" />
                <asp:Button ID="BtnCaseCancel" runat="server" CssClass="btn" Text="Cancel" Width="80px" />                
                <input  type="button" value="Print" class="btn" onclick="Casemanagementreport();" style="width:80px" />
                </div>
            </td>
            </tr>            
        </table>

    </div>
    
    
       
    
    </form>
</body>
</html>
