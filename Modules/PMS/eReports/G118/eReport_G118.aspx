<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eReport_G118.aspx.cs" Inherits="eReports_G118_eReport_G118" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eReport-G118</title>
     <script type="text/javascript" src="../JS/jquery.min.js"></script>
     <script src="../JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="../JS/AutoComplete/jquery-ui.css" />
     <script src="../JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>

     <script type="text/javascript">
         function RegisterAutoComplete() {
             $(function () {
                 function log(message) {
                     $("<div>").text(message).prependTo("#log");
                     $("#log").scrollTop(0);
                 }

                 $("#txtPort").autocomplete({
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/eReports/getautocompletedata.ashx",
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtPort").val(), Type: "PORT" },
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.PortName, value: item.PortName} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         log(ui.item ?
                          "Selected: " + ui.item.label :
                          "Nothing selected, input was " + this.value);
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
             });
         }
         function getBaseURL() {
             var url = window.location.href.split('/');
             var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
             return baseUrl;
         }
  </script>
    
     
     <script type="text/javascript" src="../JS/KPIScript.js"></script>
    
     <script type="text/javascript">

          function Validate_ITP() {
              var v1 = false;
              var mainresult = new Boolean();
              mainresult = true;

              v1 = ValidateRadio("radsec4CC", "*", "#val_radsec4CC", null);
              mainresult = mainresult && v1;

              if ($("#radsec4Crew").is(":checked")) {                 
                  if ($("#txtsec4IpCrewNo").val() == '') {
                      v1 = true;
                      $("#lblcrewno").html("*");
                  }
                  else {
                      $("#lblcrewno").html("");
                  }
              }
              else {
                  $("#lblcrewno").html("");
              }
              
              v1 = ValidateRadio("radsec4DN", "*", "#val_radsec4DN", null);
              mainresult = mainresult && v1;
              v1 = ValidateRadio("radsec4OFD", "*", "#val_radsec4OFD", null);
              mainresult = mainresult && v1;
              v1 =  ValidateRadio("radsec4RW", "*", "#val_radsec4RW", null);
              mainresult = mainresult && v1;
              v1 = ValidateRadio("radsec4MTR", "*", "#val_radsec4MTR", null);
              mainresult = mainresult && v1;              
              v1 = ValidateRadio("radsec4AOO", "*", "#val_radsec4AOO", "#txtsec4AOOOther");
              mainresult = mainresult && v1;
              v1 = ValidateRadio("radsec4TOA", "*", "#val_radsec4TOA", "#txtsec4TOAOther");
              mainresult = mainresult && v1;
              v1 = Page_ClientValidate('ITP');
              mainresult = mainresult && v1;
              
              return mainresult;

          }
          function OpenGuideLines() {
              window.open('OpenGuideLines.aspx','');
          }
      </script>

      <script type="text/javascript">
          function chkRootCauseOther_onclick() {
              var objtxt = document.getElementById('txtRootCause');
              var RootCauseOther = document.getElementById('chkRootCauseOther');

              if (RootCauseOther.checked)
                  objtxt.style.display = 'block';
              else {
                  objtxt.style.display = 'none';
                  objtxt.value = '';
              }

          }
    </script>

     <style type="text/css" >
      .ui-autocomplete-loading {
        background: white url('images/ui-anim_basic_16x16.gif') right center no-repeat;
      }
      #city { width: 25em; }
      input
      {
       box-shadow:rgba(0, 0, 0, 0.2) 0 2px 10px;
       border:solid 1px #c2c2c2;
       border-radius:4px;
       font-size:13px;
       padding:4px;
      }
      </style>
     <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css"/>
     <link rel="stylesheet" type="text/css" href="../css/StyleSheet.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
         <table cellpadding="0" cellspacing="0" width="100%">
         <tr style=" background-color:#CCE6FF"> <%--#3498db--%>
             <td >
              <table cellpadding="0" cellspacing="0" width="100%">
                 <tr>
                     <td style="text-align:left; width:30%; " class="formname">Vessel : <asp:Label ID="lblVesselName" runat="server" CssClass="formname" ></asp:Label></td>
                     <td style="text-align:center; width:40%; "> <asp:Label ID="lblFormName" runat="server" class="formname"></asp:Label></td>
                     <td style="text-align:right; width:30%" class="formname">Form :&nbsp;G118 ( <asp:Label ID="lblVersionNo" runat="server" class="formname"></asp:Label> )</td>
                     </tr>
                 <tr>
                     <td style="text-align:left; width:30%; " class="formname">&nbsp; [ <asp:Label ID="lblReportNo" runat="server" Font-Bold="true" ForeColor="Brown" Font-Size="14px" ></asp:Label> ]</td>
                     <td style="text-align:center; width:40%; ">
                        <div style="float:left; text-align:left;  color:red; margin-top:2px; display:none;">
                            Note : Immediate report to be followed by all relevant sections within 48 Hrs of an Accident.
                        </div>
                     </td>
                     <td style="text-align:right; width:30%" class="formname">
                        <div style="float:right; text-align:center; width:100px; padding:0px; background-color:Red; color:White; PADDING-top:2px;" runat="server" id="dv_Notice">Incomplete</div>
                     </td>

                 </tr>
             </table>
             </td>
         </tr>
         <tr style=" ">
             <td>
                 <table cellpadding="0" cellspacing="0" width="100%">
                 <tr>
                     <td class="hdng1" style="width:150px; text-align:right;">Area Audited</td>
                     <td class="hdng1" style="width:10px;">:</td>
                     <td class="hdng1" style="text-align:left; width:150px;">
                         <asp:DropDownList ID="ddlAuditedArea" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAuditedArea_SelectedIndexChanged" runat="server" >
                              <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlAuditedArea" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                         
                     </td>
                     <td class="hdng1" style="text-align:left; width:120px;">
                           <asp:TextBox runat="server" ID="txtAreaAuditedOtherVessel" MaxLength="50"  Visible="false"></asp:TextBox>
                     </td>
                     <td class="hdng1">
                        <span class="comment" style="text-align:left;"> (For each area audited use a separate NCR )</span>
                     </td>
                 </tr>
                 </table>
                 <table cellpadding="0" cellspacing="0" width="100%">                 
                 <tr>
                     <td  class="hdng1" style="text-align:right; width:300px;">Identified through Inspections or Audits : </td>
                     <td class="hdng1"  style="text-align:left; width:375px; padding-left:10px;">
                        <asp:RadioButton ID="rdoEXAudit" Text="External" GroupName="rdo" OnCheckedChanged="rdoAudit_CheckedChanged" AutoPostBack="true" runat="server" />
                        <asp:DropDownList ID="ddlEXAudit" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAudit_SelectedIndexChanged" runat="server">
                            <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox  ID="txtExtAuditOther" runat="server" Visible="false" MaxLength="50" ></asp:TextBox>
                        
                     </td>
                     <td class="hdng1"  style="text-align:left;"><asp:RadioButton ID="rdoInternal" GroupName="rdo" Text="Internal Audit" OnCheckedChanged="rdoAudit_CheckedChanged" AutoPostBack="true" runat="server" />
                        <asp:DropDownList ID="ddlIntAudit" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAudit_SelectedIndexChanged" runat="server">
                            <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox  ID="txtIntAuditOther" runat="server" Visible="false" MaxLength="50" ></asp:TextBox>
                     </td>
                 </tr>
                 </table>
             </td>
         </tr>
             <tr>
                 <td>
                 <div>
                  <div style="float:left; width:600px;">
                    <asp:button ID="btnDec" runat="Server" Text="Description" CssClass="color_tab_sel" CommandArgument="Div1" OnClick="btn_Tab_Click" CausesValidation="false" Width="120px"/>
                    <asp:button ID="btnCA" runat="Server" Text="Corrective Actions" CssClass="color_tab" CommandArgument="Div2" OnClick="btn_Tab_Click" CausesValidation="false"  Width="120px"/>
                    <asp:button ID="btnRC" runat="Server" Text="Root Causes" CssClass="color_tab" CommandArgument="Div3" OnClick="btn_Tab_Click" CausesValidation="false"  Width="90px"/>                    
                    <asp:button ID="btnOC" runat="Server" Text="Office Comments" CssClass="color_tab" CommandArgument="Div4" OnClick="btn_Tab_Click" CausesValidation="false"  Width="120px"/>
                 </div>                 
                 <div style="vertical-align:middle; height:30px;background-color:#F5F5FF">
                    <div style="padding-top:5px">&nbsp;<b>Last Exported By / On : </b>
                    <asp:Label runat="server" ID="lblLastExportedByOn"></asp:Label>
                    </div>
                 </div>
                 <div style="clear:both"></div>               
                 </div>
                 <div style="width:100%; background-color:#FFCC80; height:4px;"></div>
                 <div style="height:410px; overflow-y:hidden; overflow-x:hidden; position:relative; border:solid 1px #e2e2e2; border-top:none;" >
                    <div id="Div1" runat="server">
                         <div class='section'>Section 1 : Non – Conformance Description</div>
                         <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td colspan="4"> <span class="comment">(Briefly describe what is not complying with the SMS)</span><br />
                                      <asp:TextBox ID="txtNCD" TextMode="MultiLine" runat="server" Height="125px" Width="98%"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNCD"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="text-align:right; width:300px;">Name of person making the NCR:</td>
                                 <td style="text-align:left; width:250px;"><asp:TextBox ID="txtNameMakingNCR" runat="server"  MaxLength="50" Width="98%"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNameMakingNCR"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                 </td>
                                 <td style="text-align:right; width:200px;">Rank or Position:</td>
                                 <td style="text-align:left; "><asp:TextBox ID="txtRankNCR" runat="server"  MaxLength="50" Width="50%"/>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtRankNCR"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                 </td>
                            </tr>                            
                            <tr><td style="text-align:right; width:300px;">Name of responsible person receiving the NCR :</td>
                                <td style="text-align:left; width:250px;"><asp:TextBox ID="txtPersonReveNCR"  runat="server" MaxLength="50" Width="98%"/>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtPersonReveNCR"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align:right; width:200px;">Rank or Position: </td>
                                <td style="text-align:left; "><asp:TextBox ID="txtRankReveNCR" runat="server"  MaxLength="50" Width="50%"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtRankReveNCR"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>                            
                            <tr>
                                <td style="text-align:right; width:300px;">Name of Master or General Manager (ENERGIOS):</td>
                                <td style="text-align:left; width:250px;"><asp:TextBox ID="txtNameMasterGeneralManager"  runat="server" MaxLength="50" Width="98%"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtNameMasterGeneralManager"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align:right; width:200px;">NCR Date:</td>
                                <td style="text-align:left;"><asp:TextBox ID="NCRDate" CssClass="date_only"  runat="server" MaxLength="11"/>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="NCRDate"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>                        
                                <td style="text-align:right;" >
                                Target date for completion:                                
                                </td>
                                <td colspan="3" style="text-align:left;">
                                     <asp:TextBox ID="txtNCRTargetCompDate" CssClass="date_only"  runat="server" maxlength="11" />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtNCRTargetCompDate"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                     <br />
                                </td>
                        
                            </tr>
                            <tr>
                                <td colspan="4"><span class="comment">(Target date should be as soon as practical but no later than 3 months from date of NCR, unless an SOR is raised for Dry dock)</span></td>
                            </tr>
                         </table>

                         <div style="text-align:center; padding-top:10px;">
                            <asp:Button ID="btnNext1" runat="server" CommandArgument="2" Text="Next >>" CssClass="btn" onclick="btnNext_Click" Width='80px'/>
                         </div>

                     </div>
                     <div id="Div2" runat="server" visible="false">
                         <div class='section'>
                             Section 2 : Immediate Corrective Action/s
                         </div>
                         <div class="comment" style="padding:3px">(Brief explanation of what is the immediate solution to correct the deficiency, even if it is only a temporary solution. For the Ship, consultation with Office may be required, specially if SOR, spares  are required) </div>
                         
                         <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td colspan="2">
                                     <asp:TextBox ID="txtImmediateCorrectiveAction" runat="server" TextMode="MultiLine" Height="125px" Width="98%" />
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtImmediateCorrectiveAction" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                 </td>
                             </tr>
                             <tr>
                                <td style="text-align:right; width:300px;">Verified By (Name):</td>
                                <td style="text-align:left; ">
                                    <asp:TextBox ID="txtICAName" runat="server" MaxLength="50" Width="30%" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtICAName"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right; width:300px;">Rank or Position:</td>
                                <td style="text-align:left; ">
                                    <asp:TextBox ID="txtICAPositionOrRank" runat="server" MaxLength="50" Width="30%" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtICAPositionOrRank"  ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right; width:300px;">Immediate Corrective Action completed on:</td>
                                <td style="text-align:left; "><asp:TextBox ID="txtICACorrectiveActionCompletedOn" runat="server" MaxLength="11" CssClass="date_only" style="width:70px;text-align:left;float:left; margin-left:5px;"/>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtICACorrectiveActionCompletedOn" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator> 
                                </td>
                            </tr>
                            
                        </table>
                         
                         <div style="text-align:center; padding-top:10px;">
                            <asp:Button ID="btnNext2" runat="server" CommandArgument="3" Text="Next >>" CssClass="btn" onclick="btnNext_Click" Width='80px'/>
                         </div>
                     </div>
                     <div id="Div3" runat="server" visible="false">
                         <div class='section'>Section 3 : Root Cause/s </div>
                         <div class="comment" style="padding:3px">(Check one or more of the Root Causes & describe where necessary)</div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                            <tr>
                                 <td>
                                     <asp:CheckBoxList runat="server" AutoPostBack="true" OnSelectedIndexChanged="chklstRC_SelectedIndexChanged" ID="chklstRC" RepeatColumns="3" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                     <asp:TextBox runat="server"  ID="txtRootCauseOther" Visible="false" MaxLength="100" />
                                 </td>
                             </tr>
                             <tr>
                                 <td >
                                    <b>PREVENTIVE ACTION/S </b> <br />
                                    <span class="comment" style="padding:3px; ">(Brief description of the long term solution to the deficiency. For the Ship, consultation with Office may be required, specially if SOR, spares or modification to MTMSQMS is required)</span> <br />
                                    <asp:TextBox ID="txtPreventyAction" runat="server" Height="125px" Width="98%" TextMode="MultiLine"  />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtPreventyAction" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                 </td>
                             </tr>
                             
                         </table>

                         <div style="text-align:center; padding-top:10px;">
                            <asp:Button ID="btnNext3" runat="server" CommandArgument="4" Text="Next >>" CssClass="btn" onclick="btnNext_Click" Width='80px'/>
                         </div>
                         
                     </div>                     
                     <div id="Div4" runat="server" visible="false">
                         <div class='section'>Section 4 : Office Comments</div>
                          <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr >
                                <td colspan="6">
                                    <asp:TextBox ID="txtOfficeComment" runat="server" Height="75px" Width="98%" ReadOnly="true" TextMode="MultiLine" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right; width:150px;">
                                    Verified By(Name):
                                </td>
                                <td style="text-align:left; ">
                                    <asp:Label ID="txtOffCommVarefiedBy" runat="server" Width="150px" />
                                </td>
                              
                                <td style="text-align:right; width:150px;">
                                    Rank or position :
                                </td>
                                <td style="text-align:left; ">
                                    <asp:Label ID="txtVarifiedPOS" runat="server" Width="150px" />
                                </td>
                            
                                <td style="text-align:right; width:150px;">
                                    Verified Date :
                                </td>
                                <td style="text-align:left; ">
                                        <asp:Label ID="txtOffVarefiedDate" runat="server" Width="90px" />
                                </td>
                            </tr>
                         </table>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                         <tr>
                                <td class="hdng1">IS THIS INCIDENT CLOSED?&nbsp;&nbsp;&nbsp;<asp:Label ID="lblIC" runat="server" /></td>
                            </tr>
                            <tr id="trIC" runat="server" visible="false">
                                <td >
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                      <tr>
                                          <td style="width:10%; text-align:right;">Closure Remarks:</td>
                                          <td colspan="5">
                                            <asp:TextBox ID="txtCloserRemarks" runat="server" Height="75px" Width="98%" TextMode="MultiLine" ></asp:TextBox>
                                        </td>
                                      </tr>
                                      <tr>
                                         <td style="width:10%;text-align:right;">Date closed:</td>
                                         <td style="width:10%;text-align:left;"><asp:Label ID="txtCD" runat="server"  ></asp:Label></td>
                                         <td style="width:7%; text-align:right;">Closed by:</td>
                                         <td style="width:15%;text-align:left;"><asp:Label ID="txtCB" Width="98%" runat="server"></asp:Label></td>
                                          <td style="width:10%; text-align:right;">&nbsp;&nbsp;Closure Evidence:</td>
                                          <td style="text-align:left;">
                                            <asp:ImageButton runat="server" ID="btnClip" ImageUrl="~/Modules/PMS/Images/paperclip.gif"  Visible="false" onclick="btnClip_Click"/>
                                            <asp:LinkButton runat="server" ID="btnClipText" Visible="false" onclick="btnClipText_Click"/>
                                        </td>
                                      </tr>
                                    </table>
                                </td>
                            </tr>
                             <tr>
                                <td class="hdng1">IS VESSEL NOTIFIED OF INCIDENT CLOSURE?&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCN" runat="server" /></td>
                            </tr>
                            <tr id="trVN" runat="server" visible="false">
                                <td >
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                      <tr>
                                         <td style="width:10%;">Date of Notification:</td>
                                         <td><asp:Label ID="txtNotifyDate" runat="server"  ></asp:Label></td> 
                                      </tr>
                                    </table>
                                </td>
                            </tr>
                          </table>

                          <div style="text-align:center; padding-top:10px;">
                            <asp:Button ID="btnSaveDraft" runat="server" Text="Save Report" CssClass="btn" onclick="btnSaveDraft_Click" CausesValidation="true" Width='120px'/>
                          </div>

                     </div>

                 </div>
                 </td>
             </tr>
         <tr>
             <td>
                 <div style="border:solid 1px #e2e2e2;padding-bottom:0px; margin-top:3px; background-color:#CCE6FF; height:40px;">
                   <center>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0" >
                        <tr>
                         <td id="tdTabs" visible="false" runat="server" style="width:270px;padding-right:10px" >
                            <asp:Button ID="btnExportToOffice" runat="server" Text="Send for Export" CssClass="btn" style="float:left" onclick="btnExportToOffice_Click" CausesValidation="true" Width='140px' OnClientClick="return confirm('Are you sure to send for export?');"/>
                            
                        </td>
                        <td>
                             &nbsp;&nbsp;<asp:Label ID="lblMsg" ForeColor="Red" Font-Size="16px" runat="server" Text="" CssClass="msg"></asp:Label>
                           <span runat="server" id="spn_Note" style="color:Red; font-size:15px; text-align:left;"> Please fill up all sections to complete the form.</span>
                        </td>
                        
                        </tr>
                        <%--<tr style=" background-color:#FFFFFF">
                        <td >&nbsp;&nbsp;<asp:Label ID="lblMsg" ForeColor="Red" Font-Size="16px" runat="server" Text="" CssClass="msg"></asp:Label>
                           <span runat="server" id="spn_Note" style="color:Red; font-size:15px; text-align:left;"> Please fill up all sections to complete the form.</span>
                        </td>
                        </tr>--%>
                    </table>
                    </center>
                 </div>
             </td>
         </tr>
         </table>
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
        RegisterAutoComplete();
    }
    SetCalender();

    $(document).ready(function () {
        RegisterAutoComplete(); 
    });

</script>
</html>
