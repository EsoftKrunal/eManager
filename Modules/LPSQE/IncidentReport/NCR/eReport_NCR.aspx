<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eReport_NCR.aspx.cs" Inherits="eReports_G118_eReport_G118" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eReport-NCR</title>
     <script type="text/javascript" src="../JS/jquery.min.js"></script>
     <%--<script src="../JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
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
    
     
     <script type="text/javascript" src="../JS/KPIScript.js"></script>--%>
    
     <%--<script type="text/javascript">

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
      </script>--%>

      <%--<script type="text/javascript">
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
    </script>--%>

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
     <link rel="stylesheet" type="text/css" href="../../../HRD/Styles/StyleSheet.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="font-family:Arial;font-size:12px;">
         <table cellpadding="0" cellspacing="0" width="100%">
         <tr style=" background-color:#CCE6FF"> <%--#3498db--%>
             <td >
              <table cellpadding="0" cellspacing="0" width="100%">
                 <tr class="text headerband">
                     <td style="text-align:left; width:30%; " >Vessel : <asp:Label ID="lblVesselName" runat="server" Css ></asp:Label></td>
                     <td style="text-align:center; width:40%; "> <asp:Label ID="lblFormName" runat="server" ></asp:Label></td>
                     <td style="text-align:right; width:30%" >Form :&nbsp;NCR ( <asp:Label ID="lblVersionNo" runat="server" ></asp:Label> )</td>
                     </tr>
                 <tr class="text headerband">
                     <td style="text-align:left; width:30%; " >&nbsp; [ <asp:Label ID="lblReportNo" runat="server" Font-Bold="true" ForeColor="Brown" Font-Size="14px" ></asp:Label> ]</td>
                     <td style="text-align:center; width:40%; ">
                        <div style="float:left; text-align:left;  color:red; margin-top:2px; display:none;">
                            Note : Immediate report to be followed by all relevant sections within 48 Hrs of an Accident.
                        </div>
                     </td>
                     <td style="text-align:right; width:30%" >
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
                         <asp:DropDownList ID="ddlAuditedArea" AppendDataBoundItems="true" runat="server" >
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
                        <asp:RadioButton ID="rdoEXAudit" Text="External" GroupName="rdo"  runat="server" />
                        <asp:DropDownList ID="ddlEXAudit" Enabled="false" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox  ID="txtExtAuditOther" runat="server" Visible="false" MaxLength="50" ></asp:TextBox>
                        
                     </td>
                     <td class="hdng1"  style="text-align:left;"><asp:RadioButton ID="rdoInternal" GroupName="rdo" Text="Internal Audit"  runat="server" />
                        <asp:DropDownList ID="ddlIntAudit" Enabled="false" AppendDataBoundItems="true"  runat="server">
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
                 <div style="height:380px; overflow-y:hidden; overflow-x:hidden; position:relative; border:solid 1px #e2e2e2; border-top:none;" >
                    <div id="Div1" runat="server">
                         <div class='section'>Section 1 : Non – Conformance Description</div>
                         <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td colspan="4"> <span class="comment">(Briefly describe what is not complying with the SMS)</span><br />
                                      <asp:TextBox ID="txtNCD" TextMode="MultiLine" runat="server" Height="125px" Width="98%"></asp:TextBox>
                                 </td>
                             </tr>
                             <tr>
                                 <td style="text-align:right; width:300px;">Name of person making the NCR:</td>
                                 <td style="text-align:left; width:250px;"><asp:Label ID="txtNameMakingNCR" runat="server" Width="98%"/></td>
                                 <td style="text-align:right; width:200px;">Rank or Position:</td>
                                 <td style="text-align:left; "><asp:Label ID="txtRankNCR" runat="server" Width="50%"/></td>
                            </tr>                            
                            <tr><td style="text-align:right; width:300px;">Name of responsible person receiving the NCR :</td>
                                <td style="text-align:left; width:250px;"><asp:Label ID="txtPersonReveNCR" runat="server" Width="98%"/></td>
                                <td style="text-align:right; width:200px;">Rank or Position: </td>
                                <td style="text-align:left; "><asp:Label ID="txtRankReveNCR" runat="server" Width="50%"/></td>
                            </tr>                            
                            <tr>
                                <td style="text-align:right; width:300px;">Name of Master or General Manager (MTMSM):</td>
                                <td style="text-align:left; width:250px;"><asp:Label ID="txtNameMasterGeneralManager" runat="server" Width="98%"/></td>
                                <td style="text-align:right; width:200px;">NCR Date:</td>
                                <td style="text-align:left;"><asp:Label ID="NCRDate" runat="server" /></td>
                            </tr>
                            <tr>                        
                                <td style="text-align:right;" >
                                Target date for completion:                                
                                </td>
                                <td colspan="3" style="text-align:left;">
                                     <asp:Label ID="txtNCRTargetCompDate" runat="server" /><br />
                                </td>
                        
                            </tr>
                            <tr>
                                <td colspan="4"><span class="comment">(Target date should be as soon as practical but no later than 3 months from date of NCR, unless an SOR is raised for Dry dock)</span></td>
                            </tr>
                         </table>
                     </div>
                     <div id="Div2" runat="server" visible="false">
                         <div class='section'>
                             Section 2 : Immediate Corrective Action/s
                         </div>
                         <div class="comment" style="padding:3px">(Brief explanation of what is the immediate solution to correct the deficiency, even if it is only a temporary solution. For the Ship, consultation with Office may be required, specially if SOR, spares or modification to MTMSMQS is required) </div>
                         
                         <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td colspan="2">
                                     <asp:TextBox ID="txtImmediateCorrectiveAction" runat="server" TextMode="MultiLine" Height="125px" Width="98%" />
                                 </td>
                             </tr>
                             <tr>
                                <td style="text-align:right; width:300px;">Verified By (Name):</td>
                                <td style="text-align:left; ">
                                    <asp:Label ID="txtICAName" runat="server" Width="30%" />

                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right; width:300px;">Rank or Position:</td>
                                <td style="text-align:left; ">
                                    <asp:Label ID="txtICAPositionOrRank" runat="server" Width="30%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right; width:300px;">Immediate Corrective Action completed on:</td>
                                <td style="text-align:left; "><asp:Label ID="txtICACorrectiveActionCompletedOn" runat="server" style="text-align:left;float:left; margin-left:5px;"/>  </td>
                            </tr>
                            
                        </table>
                     </div>
                     <div id="Div3" runat="server" visible="false">
                         <div class='section'>Section 3 : Root Cause/s </div>
                         <div class="comment" style="padding:3px">(Check one or more of the Root Causes & describe where necessary)</div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                            <tr>
                                 <td>
                                     <asp:CheckBoxList runat="server"  ID="chklstRC" RepeatColumns="3" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                     <asp:Label runat="server"  ID="txtRootCauseOther" Visible="false" />
                                 </td>
                             </tr>
                             <tr>
                                 <td >
                                    <b>PREVENTIVE ACTION/S </b> <br />
                                    <span class="comment" style="padding:3px; ">(Brief description of the long term solution to the deficiency. For the Ship, consultation with Office may be required, specially if SOR, spares or modification to MTMSQMS is required)</span> <br />
                                    <asp:TextBox ID="txtPreventyAction" runat="server" Height="125px" Width="98%" TextMode="MultiLine"  />
                                 </td>
                             </tr>
                             
                         </table>
                         
                     </div>                     
                     <div id="Div4" runat="server" visible="false">
                         <div class='section'>Section 4 : Office Comments</div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr >
                                <td colspan="6">
                                    <asp:TextBox ID="txtOfficeComment" runat="server" Height="75px" Width="98%" TextMode="MultiLine" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right; width:150px;">
                                    Verified By(Name):
                                </td>
                                <td style="text-align:left; ">
                                    <asp:TextBox ID="txtOffCommVarefiedBy" runat="server" MaxLength="50"  Width="150px" />
                                </td>
                              
                                <td style="text-align:right; width:150px;">
                                    Rank or position :
                                </td>
                                <td style="text-align:left; ">
                                    <asp:TextBox ID="txtVarifiedPOS" runat="server" MaxLength="50"  Width="150px" />
                                </td>
                            
                                <td style="text-align:right; width:150px;">
                                    Verified Date :
                                </td>
                                <td style="text-align:left; ">
                                        <asp:TextBox ID="txtOffVarefiedDate" runat="server" CssClass="date_only" MaxLength="11" Width="90px" />
                                </td>
                            </tr>
                         </table>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                         <tr>
                                <td class="hdng1">IS THIS INCIDENT CLOSED?&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radICyes" runat="server" GroupName="radsec15IC" Text="Yes" OnCheckedChanged="radICyes_OnCheckedChanged" AutoPostBack="true"/><asp:RadioButton ID="radICno" runat="server" GroupName="radsec15IC" Text="No" OnCheckedChanged="radICyes_OnCheckedChanged" AutoPostBack="true"/></td>
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
                                         <td style="width:10%;text-align:left;"><asp:TextBox ID="txtCD" MaxLength="11" runat="server"  CssClass="date_only" ></asp:TextBox></td>
                                         <td style="width:7%; text-align:right;">Closed by:</td>
                                         <td style="width:15%;text-align:left;"><asp:TextBox ID="txtCB" MaxLength="50" Width="98%" runat="server"></asp:TextBox></td>
                                          <td style="width:10%; text-align:right;">&nbsp;&nbsp;Closure Evidence:</td>
                                          <td style="text-align:left;">
                                            <asp:FileUpload runat="server" ID="flpUpload" />&nbsp;<asp:ImageButton runat="server" ID="btnClip" ImageUrl="~/Modules/HRD/Images/paperclip.png"  Visible="false" onclick="btnClip_Click"/>
                                            <asp:LinkButton runat="server" ID="btnClipText" Visible="false" onclick="btnClipText_Click"/>
                                        </td>
                                      </tr>
                                    </table>
                                </td>
                            </tr>
                             <tr>
                                <td class="hdng1">IS VESSEL NOTIFIED OF INCIDENT CLOSURE?&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radCNyes" runat="server" GroupName="radsec15CN" Text="Yes" OnCheckedChanged="radCNyes_OnCheckedChanged" AutoPostBack="true"/><asp:RadioButton ID="radCNno" runat="server" GroupName="radsec15CN" Text="No" OnCheckedChanged="radCNyes_OnCheckedChanged" AutoPostBack="true"/></td>
                            </tr>
                            <tr id="trVN" runat="server" visible="false">
                                <td >
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                      <tr>
                                         <td style="width:10%;">Date of Notification:</td>
                                         <td><asp:TextBox ID="txtNotifyDate" MaxLength="11" runat="server"  CssClass="date_only" ></asp:TextBox></td>
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
                     </div>

                 </div>
                 </td>
             </tr>
         <tr>
             <td>
                 <div style="border:solid 1px #e2e2e2;padding-bottom:0px; margin-top:3px; background-color:#CCE6FF; height:60px;">
                   <center>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0" >
                        <tr>
                         <td id="tdTabs" runat="server" style="padding-right:10px" >
                            <asp:Button ID="btnSaveDraft" runat="server" Text="Save Report" CssClass="btn" style="float:left" onclick="btnSaveDraft_Click" CausesValidation="true" Width='120px'/>
                            <asp:Button ID="btnExportToShip" runat="server" Text="Export to Ship" CssClass="btn" style="float:left" onclick="btnExportToShip_Click" CausesValidation="true" Width='140px' OnClientClick="confirm('Are you sure to export?');"/>
                            <%--<asp:Button ID="btnSendDocs" runat="server" Text="Documents Export" CssClass="btn" style="float:left" onclick="btnExportDocsToOffice_Click" CausesValidation="true" Width='140px' OnClientClick="confirm('Are you sure to export?');"/>--%>
                            <span runat="server" id="spn_Note" style="color:Red; font-size:15px; text-align:left;"> Please fill up all sections to complete the form.</span>
                        </td>
                        
                        </tr>
                        <tr style=" background-color:#FFFFFF">
                        <td >&nbsp;&nbsp;<asp:Label ID="lblMsg" ForeColor="Red" Font-Size="16px" runat="server" Text="" CssClass="msg"></asp:Label>
                           
                        </td>
                        </tr>
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
        //RegisterAutoComplete();
    }
    SetCalender();

//    $(document).ready(function () {
//        RegisterAutoComplete(); 
//    });

</script>
</html>
