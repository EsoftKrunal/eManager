<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eReport_S115.aspx.cs" Inherits="eReports_S115_eReport_S115" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eReport-S115</title>
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
                     <td style="text-align:right; width:30%" class="formname">Form :&nbsp;S115 ( <asp:Label ID="lblVersionNo" runat="server" class="formname"></asp:Label> )</td>
                     </tr>
                 <tr>
                     <td style="text-align:left; width:30%; " class="formname">&nbsp; [ <asp:Label ID="lblReportNo" runat="server" Font-Bold="true" ForeColor="Brown" Font-Size="14px" ></asp:Label> ]</td>
                     <td style="text-align:center; width:40%; ">
                        <div style="float:left; text-align:left;  color:red; margin-top:2px;">
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
                     <td>Name of Port</td>
                     <td>:</td>
                     <td>
                         <asp:TextBox ID="txtPort" runat="server" ></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtPort" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>
                     <td>&nbsp;</td>
                     <td>Position ( LAT &amp; Long )&nbsp;</td>
                     <td>:</td>
                     <td>
                         ( Lat )
                         <asp:DropDownList ID="ddlLat1" runat="server" ></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="ddlLat1" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                         <asp:DropDownList ID="ddlLat2" runat="server" ></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="ddlLat2" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                         <asp:DropDownList ID="ddlLat3" runat="server" >
                         <asp:ListItem Value="" Text=""></asp:ListItem>
                         <asp:ListItem Value="N" Text="N"></asp:ListItem>
                         <asp:ListItem Value="S" Text="S"></asp:ListItem>
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlLat3" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>

                         ( Long )
                         <asp:DropDownList ID="ddlLong1" runat="server" ></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlLong1" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                         <asp:DropDownList ID="ddlLong2" runat="server" ></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="ddlLong2" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                         <asp:DropDownList ID="ddlLong3" runat="server" >
                         <asp:ListItem Value="" Text=""></asp:ListItem>
                         <asp:ListItem Value="E" Text="E"></asp:ListItem>
                         <asp:ListItem Value="W" Text="W"></asp:ListItem>
                         </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlLong3" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>
                 </tr>
                 <tr>
                     <td>Report Date</td>
                     <td>:</td>
                     <td><asp:TextBox ID="txtReportDate" MaxLength="11" runat="server" CssClass="date_only" Text="" ></asp:TextBox>
                         <span class="comment">(dd-mmm-yyyy)</span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtReportDate" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>
                     <td></td>
                     <td>Date &amp; Time of Incident (LT)</td>
                     <td>:</td>
                     <td>
                           <span class="comment"><asp:TextBox ID="txtIncidentDate" MaxLength="17" runat="server" CssClass="date_time"></asp:TextBox>
                     (dd-mmm-yyyy)(hh:mm)</span>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtIncidentDate" ErrorMessage="*" ForeColor="Red" runat="server" SetFocusOnError="true"></asp:RequiredFieldValidator>
                     </td>
                 </tr>
                 </table>
             </td>
         </tr>
             <tr>
                 <td>
                 <div>
                  <div style="float:left; width:800px;">
                    <asp:button ID="btnHome" runat="Server" Text="Immediate Report" CssClass="color_tab_sel" CommandArgument="Div1" OnClick="btn_Tab_Click" CausesValidation="false" Width="120px"/>
                    <asp:button ID="btnCOA" runat="Server" Text="Cause Of Accident" CssClass="color_tab" CommandArgument="Div13" OnClick="btn_Tab_Click" CausesValidation="false"  Width="120px"/>
                    <asp:button ID="btnCA" runat="Server" Text="Corrective Actions" CssClass="color_tab" CommandArgument="Div14" OnClick="btn_Tab_Click" CausesValidation="false"  Width="120px"/>
                    <asp:button ID="btnDoc" runat="Server" Text="Documents" CssClass="color_tab" CommandArgument="Div16" OnClick="btn_Tab_Click" CausesValidation="false"  Width="90px"/>
                    <asp:button ID="btnOC" runat="Server" Text="Office Use Only" CssClass="color_tab" CommandArgument="Div15" OnClick="btn_Tab_Click" CausesValidation="false"  Width="120px"/>
                    
                    <asp:button ID="btnRca" runat="Server" Text="RCA" CssClass="color_tab" CommandArgument="Div18" CausesValidation="false"  Width="120px" OnClick="btn_Tab_Click" />
                 </div>                 
                 <div style="vertical-align:middle; height:30px;background-color:#F5F5FF">
                    <div style="padding-top:5px">&nbsp;<b>Last Exported By / On : </b>
                    <asp:Label runat="server" ID="lblLastExportedByOn"></asp:Label>
                    </div>
                 </div>
                 <div style="clear:both"></div>               
                 </div>
                 <div style="width:100%; background-color:#FFCC80; height:4px;"></div>
                 <div style="height:450px; overflow-y:scroll; overflow-x:hidden; position:relative; border:solid 1px #e2e2e2; border-top:none;" >
                    <div id="Div1" runat="server">
                         <div class='section'>Section 1 : Severity & Classification</div>
                         <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td style="width:200px">
                                     <table cellpadding="0" cellspacing="0" width="100%">
                                         <tr>
                                             <td class="hdng1" colspan="2">
                                                 Severity of Accident
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="vertical-align: top;">
                                                 <asp:Label ID="lblsec1SOA" runat="server"></asp:Label>
                                                 <%--<asp:RadioButtonList ID="rdosec1SOA" runat="server" RepeatDirection="Horizontal">
                                                     <asp:ListItem Text="Minor" Value="1"></asp:ListItem>
                                                     <asp:ListItem Text="Major" Value="2"></asp:ListItem>
                                                     <asp:ListItem Text="Severe" Value="3"></asp:ListItem>
                                                 </asp:RadioButtonList>--%>
                                             </td>
                                             <td>
                                                 <%--<asp:RequiredFieldValidator ID="rfsec1SOA" runat="server" ForeColor="Red" ErrorMessage="*" ControlToValidate="rdosec1SOA" SetFocusOnError="true" ></asp:RequiredFieldValidator>--%>
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                                 <td>
                                     <table cellpadding="0" cellspacing="0" width="100%">
                                         <tr>
                                             <td class="hdng1">
                                                 Classification of Accident <span class="comment">(Select more than one if applicable)</span>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:CheckBoxList ID="chksec1COA" RepeatColumns="5" RepeatDirection="Horizontal" runat="server" Width="100%" >
                                                 </asp:CheckBoxList>
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                         </table>
                         <div class='section'>Section 2 : Event Description</div>
                         <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td>
                                     (What happened?)<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ForeColor="Red" ErrorMessage="*" ControlToValidate="txtsec1EventDesc" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:TextBox ID="txtsec1EventDesc" TextMode="MultiLine" runat="server" Height="125px" Width="98%"></asp:TextBox>
                                 </td>
                             </tr>
                         </table>
                         <div class='section'>Section 3 : General Information</div>
                        <table cellpadding="0" cellspacing="0" width="100%">
                             <tr>
                                 <td>
                                     <table cellpadding="0" cellspacing="0" width="100%">
                                         <tr>
                                             <td class="hdng1">
                                                 A. Alcohol & Drug testing carried out post incident?
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:RadioButton ID="radsec3PIyes" runat="server" GroupName="radsec3PI" Text="Yes" />
                                                 <asp:RadioButton ID="radsec3PIno" runat="server" GroupName="radsec3PI" Text="No" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="hdng1">
                                                 B. Vessel Activity at time of accident
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:RadioButtonList ID="rdosec3VA" runat="server" RepeatColumns="3"></asp:RadioButtonList>
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                                 <td>
                                     <table cellpadding="0" cellspacing="0" width="100%">
                                         <tr>
                                             <td class="hdng1">
                                                 C. Was the Vessel Delayed?
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                <asp:RadioButton ID="radsec3VDyes" runat="server" GroupName="radsec3VD" Text="Yes" />                                                      
                                                <asp:RadioButton ID="radsec3VDno" runat="server" GroupName="radsec3VD" Text="No" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="hdng1">
                                                 D. Bad Weather
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:RadioButton ID="radsec3BVyes" runat="server" GroupName="radsec3BV" Text="Yes" />
                                                 <asp:RadioButton ID="radsec3BVno" runat="server" GroupName="radsec3BV" Text="No" />
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                                 <td>
                                     <table cellpadding="0" cellspacing="0" width="100%">
                                         <tr>
                                             <td class="hdng1">
                                                 E. Restricted Visibility
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:RadioButton ID="radsec3RVyes" runat="server" GroupName="radsec3RV" Text="Yes" />
                                                 <asp:RadioButton ID="radsec3RVno" runat="server" GroupName="radsec3RV" Text="No" />
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class="hdng1">
                                                 F. Has there been a breach of Company’s Policy and Regulations?
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:RadioButton ID="radsec3BCyes" runat="server" GroupName="radsec3BC" Text="Yes" />
                                                 <asp:RadioButton ID="radsec3BCno" runat="server" GroupName="radsec3BC" Text="No" />
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                        </table>
                     </div>
                     <div id="Div4" runat="server" visible="false">
                         <div class='section'>
                             Section 4 : Injury to personnel
                             <asp:ImageButton ID="btnAddInjuredCrew" ImageUrl="~/Modules/PMS/Images/add.png" ToolTip="Add new Crew/Contractor" OnClick="btnAddInjuredCrew_Click" style="padding-top:0px" runat="server" />
                             <a href="OCIMF_Injury.pdf" title="OCIMF Guidelines"> <img src="../../Images/paperclipx12.png" /> OCIMF Guidelines </a>
                         </div>
                         <div class="comment" style="padding:3px">( If more than one person is injured, enter details for each person seperately. ) </div>
                         <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 25px ; text-align:center; border-bottom:none;">
                           <table cellspacing="0" rules="all" border="1" bordercolor="white" cellpadding="4" style="width:100%;border-collapse:collapse; background-color:#FF9933; color: #fff;  height:25px;" >
                                <colgroup>
                                    <col style="width:40px;" />
                                    <col style="width:40px;" />
                                    <col style="width:50px;" />
                                    <col style="width:120px;" />
                                    <col style="width:60px;" />
                                    <col />                                    
                                    <col style="width:120px;" />
                                    <col style="width:220px;" />
                                    <col style="width:150px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tr>
                                    <td style="color:White;vertical-align:middle;">view</td>
                                    <td style="color:White;vertical-align:middle;">Edit</td>
                                    <td style="color:White;vertical-align:middle;">Delete</td>
                                    <td style="color:White;vertical-align:middle;">Crew/Contr.</td>
                                    <td style="color:White;vertical-align:middle;">Crew# </td>
                                    <td style="color:White;vertical-align:middle; text-align:left;">Name </td>
                                    <td style="color:White;vertical-align:middle;">Rank </td>
                                    <td style="color:White;vertical-align:middle;">OCIMF Cat.</td>
                                    <td style="color:White;vertical-align:middle;">Sign On Dt/Hrs</td>
                                    <td>&nbsp;</td>
                                </tr>
                                
                                </table>
                           </div>
                         <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 340px ; text-align:center;" class="ScrollAutoReset" id='dv_FRMS'>
                           <table cellspacing="0" rules="ROWS" border="0" bordercolor="white" cellpadding="4" style="width:100%;border-collapse:collapse;" class="newformat">
                               <colgroup>
                                    <col style="width:40px;" />
                                    <col style="width:40px;" />
                                    <col style="width:50px;" />
                                    <col style="width:120px;" />
                                    <col style="width:60px;" />
                                    <col />                                    
                                    <col style="width:120px;" />
                                    <col style="width:220px;" />
                                    <col style="width:150px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tbody>
                                <asp:Repeater ID="rptSec4" runat="server">
                                    <ItemTemplate>
                                           <tr>
                                           <td><asp:ImageButton ID="btnViewCrew" OnClick="btnViewCrew_Click" runat="server" CommandArgument='<%#Eval("TableId")%>' ImageUrl="~/Modules/PMS/Images/search_magnifier_12.png" ToolTip="View" /> </td>                                           
                                           <td><asp:ImageButton ID="btnEditCrew" OnClick="btnEditCrew_Click" runat="server" CommandArgument='<%#Eval("TableId")%>' ImageUrl="~/Modules/PMS/Images/12-em-pencil.png" ToolTip="Edit" /> </td>                                           
                                           <td><asp:ImageButton ID="btnDeleteCrew" OnClick="btnDeleteCrew_Click" runat="server" CommandArgument='<%#Eval("TableId")%>' OnClientClick="return confirm('Are you sure to delete?');" ImageUrl="~/Modules/PMS/Images/Delete.png" ToolTip="Delete" Width="12px" /> </td>                                           
                                           <td align="left">&nbsp;<%#Eval("Is_Crew").ToString() == "Y" ? "Crew" : "Contractor" %></td>
                                           <td align="center">&nbsp;<%#Eval("IP_CrewNo")%></td>
                                           <td align="left">&nbsp;<%#Eval("IP_Name")%></td>
                                           <td align="left">&nbsp;<%#Eval("IP_Rank_Rating")%></td>
                                           <td align="left">&nbsp;<%#Eval("OCIMF")%></td> 
                                           <td align="left">&nbsp;
                                           <%#Eval("Is_Crew").ToString() == "Y" ? Eval("IP_SignedOnDate") : Eval("IP_NoOfHrOnBoard")%>
                                           </td>                                           
                                           <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </tbody>
                            </table> 
                           </div>
                           <div style="text-align:center;" class='section'>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td style="text-align:center;" >
                                        Medical Treatment Case (MTC)=FAC + LWC + RWC :
                                        <asp:TextBox runat="server" ID="txtsec4MTC" style="text-align:center;" ReadOnly="true" BackColor="#E5E5E5" Width="40px" ></asp:TextBox>
                                    </td>
                                    <td style="text-align:center;">
                                        Lost Time Injury (LTI) =Fatalities + PTD + PPD + LWC :
                                        <asp:TextBox runat="server" ID="txtsec4LTI" style="text-align:center;" ReadOnly="true" BackColor="#E5E5E5" Width="40px"></asp:TextBox>
                                    </td>
                                    <td style="text-align:center;">
                                    Total Recordable Cases (TRC) =LTI + RWC + MTC :
                                        <asp:TextBox runat="server" ID="txtsec4TRC" style="text-align:center;" ReadOnly="true" BackColor="#E5E5E5" Width="40px" ></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                          </div>
                     </div>
                     <div id="Div5" runat="server" visible="false">
                         <div class='section'>Section 5 : Cargo </div>
                         <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                         <ContentTemplate>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                             <tr>
                                 <td width="50%"  class='hdng1'>
                                     CARGO CONTAMINATION / DAMAGE? &nbsp;&nbsp;
                                     <asp:RadioButton ID="radsec5ccdyes" runat="server" GroupName="radsec5ccd" Text="Yes"   OnCheckedChanged="radsec5ccd_OnCheckedChanged" AutoPostBack="true"/>
                                     <asp:RadioButton ID="radsec5ccdno" runat="server" GroupName="radsec5ccd" Text="No"   OnCheckedChanged="radsec5ccd_OnCheckedChanged" AutoPostBack="true"/>
                                 </td>
                                 <td  class='hdng1'>
                                 CARGO QUANTITY IN DISPUTE?&nbsp;&nbsp;
                                     <asp:RadioButton ID="radsec5cqdyes" runat="server" GroupName="radsec5cqd" Text="Yes"   OnCheckedChanged="radsec5cqd_OnCheckedChanged" AutoPostBack="true"/>
                                     <asp:RadioButton ID="radsec5cqdno" runat="server" GroupName="radsec5cqd" Text="No"   OnCheckedChanged="radsec5cqd_OnCheckedChanged" AutoPostBack="true"/>
                                 </td>
                             </tr>
                             <tr>
                                 <td width="50%">
                                    <asp:Panel runat="server" ID="pnl_radsec5ccd" Enabled="false">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class='hdng1'>
                                                            A. Name of Charterer
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtsec5Nameofcharterer"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class='hdng1'>
                                                            B. Type of Cargo
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtsec5TypeofCargo"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class='hdng1'>
                                                            C. Tank / Hold Number(s)
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtsec5TankHoldNo"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class='hdng1'>
                                                            D. Tank Coating
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBoxList runat="server" ID="chklstsec5TankCoating">
                                                                <asp:ListItem Text="Stainless Steel" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Zinc" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Epoxy" Value="3"></asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Other (Specify)
                                                            <asp:TextBox runat="server" ID="txtsec5TankCoatingOther"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class='hdng1'>E. Load Port(s)</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:TextBox ID="txtsec5LoadPort" OnTextChanged="txtsec5LoadPort_OnTextChanged" AutoPostBack="true" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td > 
                                                        <center>
                                                        <table width="100%" >
                                                        <tr>
                                                        <td >Terminal / Berth</td>
                                                        <td style="width:100px">Date</td>
                                                        </tr>
                                                         <tr>
                                                        <td><asp:TextBox ID="txtsec5LPTerminalBerth" Enabled="false" runat="server"></asp:TextBox></td>
                                                        <td style="width:100px"><asp:TextBox ID="txtsec5LPTerminalDate" Enabled="false" runat="server"  CssClass="date_only" ></asp:TextBox></td>
                                                        </tr>
                                                        </table>
                                                        </center>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class='hdng1'>F. Discharge Port(s)</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:TextBox ID="txtsec5DP" OnTextChanged="txtsec5DP_OnTextChanged" AutoPostBack="true" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                    <td>
                                                    
                                                     <table width="100%" >
                                                        <tr>
                                                        <td >Terminal</td>
                                                        <td style="width:100px">Date</td>
                                                        </tr>
                                                         <tr>
                                                        <td ><asp:TextBox ID="txtsec5DPTerminal" runat="server" Enabled="false"></asp:TextBox></td>
                                                        <td style="width:100px"> <asp:TextBox ID="txtsec5DPTerminalDate" runat="server" Enabled="false"  CssClass="date_only" ></asp:TextBox></td>
                                                        </tr>
                                                     </table>
                                                    
                                                    </td>
                                                    </tr>
                                                    <tr class="hdng1">
                                                        <td class='hdng1'>G. STS Operation</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RadioButton ID="radsec5stsyes" runat="server" GroupName="radsec5sts" Text="Yes" />
                                                            <asp:RadioButton ID="radsec5stsno" runat="server" GroupName="radsec5sts" Text="No" />
                                                        </td>
                                                    </tr>
                                            </table>
                                            </td>
                                        </tr>
                                        
                                     </table>
                                    </asp:Panel>
                                 </td>                                
                                 <td>
                                    <asp:Panel runat="server" ID="pnl_radsec5cqd" Enabled="false">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class='hdng1'>
                                                            H. Cargo Contaminant
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBoxList runat="server" ID="chklstsec5CargoContaminant" ></asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                           <td>
                                           <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class='hdng1'>
                                                            I. Cargo Quantity
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList runat="server" ID="chklstsec5CargoQty">
                                                                <asp:ListItem Text="Short loaded" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Excess loaded" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Short discharged" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Excess discharged" Value="4"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td  class='hdng1'>J. Tanks / holds inspected prior loading</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RadioButton ID="radsec5TankInspectedyes" runat="server" GroupName="radsec5TankInspected" Text="Yes" />
                                                            <asp:RadioButton ID="radsec5TankInspectedno" runat="server" GroupName="radsec5TankInspected" Text="No" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td  class='hdng1'>K. Wall wash test done?</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RadioButton ID="radsec5WallWashyes" runat="server" GroupName="radsec5WallWash" Text="Yes" />
                                                            <asp:RadioButton ID="radsec5WallWashno" runat="server" GroupName="radsec5WallWash" Text="No" /></td>
                                                    </tr>
                                                </table>
                                           </td>
                                        </tr>
                                    </table>
                                    </asp:Panel>
                                 </td>
                             </tr>
                         </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                     </div>
                     <div id="Div6" runat="server" visible="false">
                         <div class='section'>
                             Section 6 : Navigation
                         </div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                             <tr>
                                 <td>
                                     <asp:CheckBoxList runat="server" ID="chklstsec6" RepeatColumns="3" RepeatDirection="Horizontal">
                                     </asp:CheckBoxList>
                                 </td>
                             </tr>
                         </table>
                     </div>
                     <div id="Div7" runat="server" visible="false">
                         <div class='section'>
                             Section 7 : Environment Pollution</div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                             <tr>
                                 <td width="35%">
                                     <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                         <tr>
                                             <td>
                                                 <span class="comment">( check as applicable )</span>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:CheckBoxList runat="server" ID="chklstsec7EP" RepeatColumns="2" RepeatDirection="Horizontal">
                                                 </asp:CheckBoxList>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 Other (Specify)
                                                 <asp:TextBox runat="server" ID="txtsec7OtherEP"></asp:TextBox>
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                                 <td width="35%">
                                     <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                     <ContentTemplate>
                                     <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                         <tr>
                                             <td class='hdng1'>
                                                 Is there loss of containment ?
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:RadioButton ID="radsec7lossyes" runat="server" GroupName="radsec7loss" Text="Yes"  OnCheckedChanged="radsec7loss_OnCheckedChanged" AutoPostBack="true"/>
                                                 <asp:RadioButton ID="radsec7lossno" runat="server" GroupName="radsec7loss" Text="No / Contained on Board"  OnCheckedChanged="radsec7loss_OnCheckedChanged" AutoPostBack="true"/>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td width="35%">
                                                 <span class="comment">( check as applicable, more than one selection is possible )</span>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td width="35%">
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                 <tr>
                                                 <td>
                                                    <asp:RadioButtonList runat="server" ID="rblsec7LTE" RepeatDirection="Vertical" Enabled="false">
                                                        <asp:ListItem Text="Minor Spill (<25 Ltr.)" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Medium Spill (<1000 Ltr.)" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Major Spill (>1000 Ltr.)" Value="3"></asp:ListItem>
                                                    </asp:RadioButtonList>                                                     
                                                 </td>
                                                 <td>
                                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                         <tr>
                                                             <td>
                                                                 <asp:CheckBoxList runat="server" ID="chklstsecLTE" RepeatColumns="1" RepeatDirection="Horizontal">
                                                                 </asp:CheckBoxList>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td>
                                                                 Other (Specify)
                                                                 <asp:TextBox runat="server" ID="txtsec7OtherLTE"></asp:TextBox>
                                                             </td>
                                                         </tr>
                                                     </table>
                                                 </td>
                                                 </tr>
                                                 </table> 
                                             </td>
                                         </tr>
                                     </table>
                                     </ContentTemplate>
                                     </asp:UpdatePanel>
                                 </td>
                                 <td>
                                     <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                         <tr>
                                             <td class='hdng1'>
                                                 Incident first observered?
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:TextBox runat="server" ID="txtsec7FO" MaxLength="17" CssClass="date_time" ></asp:TextBox>
                                                 &nbsp;<span class="comment">( Date &amp; time )</span>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class='hdng1'>
                                                 Ceased Operations?
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:TextBox runat="server" ID="txtsec7CO" MaxLength="17" CssClass="date_time" ></asp:TextBox>
                                                 &nbsp;<span class="comment">( Date &amp; time )</span>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td class='hdng1'>
                                                 Report to any authority?
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:RadioButton ID="radsec7AAyes" runat="server" GroupName="radsec7AA" Text="Yes"  OnCheckedChanged="radsec7AA_OnCheckedChanged" AutoPostBack="true"/>
                                                 <asp:RadioButton ID="radsec7AAno" runat="server" GroupName="radsec7AA" Text="No"  OnCheckedChanged="radsec7AA_OnCheckedChanged" AutoPostBack="true"/>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:TextBox ID="txtsec7date1" runat="server" MaxLength="17" CssClass="date_time" Enabled="false"></asp:TextBox>
                                                 &nbsp;<span class="comment">( Date &amp; time )</span>
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:TextBox ID="txtsec7AutName" runat="server" MaxLength="50" Enabled="false"></asp:TextBox>
                                                 &nbsp;<span class="comment">( Name of Authority)</span>
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                         </table>
                     </div>
                     <div id="Div8" runat="server" visible="false">
                         <div class='section'>
                             Section 8 : Mooring
                         </div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                             <tr>
                                 <td>
                                     <asp:CheckBoxList runat="server" ID="chklstsec8" RepeatColumns="2" RepeatDirection="Horizontal">
                                     </asp:CheckBoxList>
                                 </td>
                             </tr>
                         </table>
                     </div>
                     <div id="Div9" runat="server" visible="false">
                         <div class='section'>Section 9 : Equipment Failure</div>
                         <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                         <ContentTemplate>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                             <tr>
                                 <td class='hdng1'>
                                     Is Critical Equipment Affected ?
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:RadioButton ID="radsec9CEyes" runat="server" GroupName="radsec9CE" Text="Yes" OnCheckedChanged="radsec9CE_OnCheckedChanged" AutoPostBack="true"/>
                                     <asp:RadioButton ID="radsec9CEno" runat="server" GroupName="radsec9CE" Text="No"  OnCheckedChanged="radsec9CE_OnCheckedChanged" AutoPostBack="true"/>
                                     <span class="comment">( check as applicable, more than one selection is possible )</span>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:CheckBoxList runat="server" ID="chklstsec9EF" RepeatColumns="2" RepeatDirection="Horizontal" Enabled="false">
                                     </asp:CheckBoxList>
                                 </td>
                             </tr>
                         </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                     </div>
                     <div id="Div10" runat="server" visible="false">
                         <div class='section'>Section 10 : Damage to Property</div>
                           <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                             <tr>
                                 <td class='hdng1' width="50%">
                                     Is the damage to a third party ?
                                 </td>
                                 <td class='hdng1' width="50%">
                                     Is the damage to own vesssel or equipment ?
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:RadioButton ID="radsec10TPyes" runat="server" GroupName="radsec10TP" Text="Yes" OnCheckedChanged="radsec10TP_OnCheckedChanged" AutoPostBack="true"/>
                                     <asp:RadioButton ID="radsec10TPno" runat="server" GroupName="radsec10TP" Text="No"  OnCheckedChanged="radsec10TP_OnCheckedChanged" AutoPostBack="true"/>
                                 </td>
                                 <td>
                                     <asp:RadioButton ID="radsec10VEyes" runat="server" GroupName="radsec10VE" Text="Yes" OnCheckedChanged="radsec10VE_OnCheckedChanged" AutoPostBack="true"/>
                                     <asp:RadioButton ID="radsec10VEno" runat="server" GroupName="radsec10VE" Text="No"  OnCheckedChanged="radsec10VE_OnCheckedChanged" AutoPostBack="true"/>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <span class="comment">( check as applicable )</span>
                                 </td>
                                 <td>
                                     <span class="comment">( check as applicable )</span>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:CheckBoxList runat="server" ID="chklstsec10TP" RepeatColumns="2" RepeatDirection="Horizontal" Enabled="false">
                                     </asp:CheckBoxList>
                                 </td>
                                 <td>
                                     <asp:CheckBoxList runat="server" ID="chklstsec10VE" RepeatColumns="2" RepeatDirection="Horizontal" Enabled="false">
                                     </asp:CheckBoxList>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Other (Specify)
                                     <asp:TextBox runat="server" ID="txtsec10OtherTP" Enabled="false"></asp:TextBox>
                                 </td>
                                 <td>
                                     Other (Specify)
                                     <asp:TextBox runat="server" ID="txtsec10OtherVE" Enabled="false"></asp:TextBox>
                                 </td>
                             </tr>
                         </table>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                     </div>
                     <div id="Div11" runat="server" visible="false">
                            <div class='section'>Section 11 : Fire</div>
                             <asp:UpdatePanel runat="server" ID="up1">
                             <ContentTemplate>
                             <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                             <tr>
                                 <td class='hdng1' width="50%">
                                     Was there an Explosion ?
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:RadioButton ID="radsec11Fireyes" runat="server" GroupName="radsec11Fire" Text="Yes" OnCheckedChanged="radsec11Fire_OnCheckedChanged" AutoPostBack="true" />
                                     <asp:RadioButton ID="radsec11Fireno" runat="server" GroupName="radsec11Fire" Text="No" OnCheckedChanged="radsec11Fire_OnCheckedChanged"  AutoPostBack="true"/>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <span class="comment">( check as applicable )</span>
                                 </td>
                             </tr>
                             </table>
                             <asp:Panel runat="server" ID="pnl_radsec11Fire" Enabled="false">
                             <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                             <tr>
                                 <td>
                                     <asp:CheckBoxList runat="server" ID="chklstsec11FE" RepeatColumns="2" RepeatDirection="Horizontal">
                                     </asp:CheckBoxList>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Other (Specify)
                                     <asp:TextBox runat="server" ID="txtsec11OtherFE"></asp:TextBox>
                                 </td>
                             </tr>
                            </table>
                            </asp:Panel>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                     </div>
                     <div id="Div12" runat="server" visible="false">
                         <div class='section'>
                             Section 12 : Security
                         </div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                             <tr>
                                 <td>
                                     <asp:CheckBoxList runat="server" ID="chklstsec12" RepeatColumns="2" RepeatDirection="Horizontal">
                                     </asp:CheckBoxList>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     Other (Specify)
                                     <asp:TextBox runat="server" ID="txtsec12Other"></asp:TextBox>
                                 </td>
                             </tr>
                         </table>
                     </div>
                     <div id="Div13" runat="server" visible="false">
                         <div class='section'>
                             Section 13 : Causes Of Accident (as assessed by the vessel)
                         </div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                 <td class='hdng1'>IMMEDIATE CAUSES <span class="comment">( Check all that apply )</span></td>
                                 <td class='hdng1'>ROOT CAUSES <span class="comment">( Check all that apply )</span></td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class='hdng2'>Human Actions</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBoxList runat="server" ID="chklstsec13HA" >
                                                            </asp:CheckBoxList>
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
                                                        <td>
                                                            <asp:CheckBoxList runat="server" ID="chklstsec13Cond" >
                                                            </asp:CheckBoxList>
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
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class='hdng2'>Human Factors</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBoxList runat="server" ID="chklstsec13HF" >
                                                            </asp:CheckBoxList>
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
                                                        <td>
                                                            <asp:CheckBoxList runat="server" ID="chklstsec13JF" >
                                                            </asp:CheckBoxList>
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
                     </div>
                     <div id="Div14" runat="server" visible="false">
                         <div class='section'>
                             Section 14 : To be filled by vessel
                         </div>
                          <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                            <tr >
                                 <td colspan="3" class="hdng1">WAS IMMEDIATE CORRECTIVE ACTION TAKEN?</td>
                            </tr>
                            <tr>
                                <td style="vertical-align:middle; height:75px; padding-left:5px;" width="25%">
                                   <asp:RadioButton ID="radsec14IAyes" runat="server" GroupName="radsec14IA" Text="Yes" />  
                                    <asp:RadioButton ID="radsec14IAno" runat="server" GroupName="radsec14IA" Text="No" />    
                                </td>
                                <td style="vertical-align:middle;" width="10%">Details :&nbsp;</td>
                                <td>
                                    <asp:TextBox  ID="txtsec14IAYesDesc" TextMode="MultiLine" MaxLength="500" Width="98%" Height="75px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                             <tr>
                                 <td colspan="3" class="hdng1">IS FURTHER ACTION RECOMMENDED TO PREVENT RECURRENCE?</td>
                            </tr>
                            <tr>
                                <td style="vertical-align:middle; height:75px; padding-left:5px;" width="25%">
                                    <asp:RadioButton ID="radsec14PRyes" runat="server" GroupName="radsec14PR" Text="Yes" />  
                                    <asp:RadioButton ID="radsec14PRno" runat="server" GroupName="radsec14PR" Text="No" />    
                                </td>
                                <td style="vertical-align:middle;" width="10%">Details :&nbsp;</td>
                                <td>
                                    <asp:TextBox  ID="txtsec14PrYesRADesc" TextMode="MultiLine" MaxLength="500"  Width="98%" Height="75px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                 <td colspan="3" class="hdng1">CONFIRMATION OF FOLLOW UP ON BOARD</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:UpdatePanel runat="server" id="UpdatePanel9">
                                    <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td colspan="4"><span class="comment">( Training must be given, especially where a procedure or equipment has been used incorrectly. It must be reported at the next on-board safety meeting )</span></td>
                                        </tr>
                                        <tr>
                                            <td style="width:15%;" >Immediate Training given?</td>
                                            <td style="width:10%;"><asp:RadioButton ID="radsec14ITyes" runat="server" GroupName="radsec14IT" Text="Yes" />
                                                <asp:RadioButton ID="radsec14ITNo" runat="server" GroupName="radsec14IT" Text="No" />
                                            </td>
                                            <td style="width:10%;">Details :&nbsp;</td>
                                            <td><asp:TextBox ID="txtsec14ITDetails" TextMode="MultiLine" MaxLength="500" Width="97%" Height="50px" runat="server"></asp:TextBox> </td>
                                        </tr>
                                        <tr>
                                            <td>Further Training required?</td>
                                            <td><asp:RadioButton ID="radsec14FTyes" runat="server" GroupName="radsec14FT" Text="Yes" />
                                                <asp:RadioButton ID="radsec14FTno" runat="server" GroupName="radsec14FT" Text="No" />
                                            </td>
                                            <td>Details :&nbsp;</td>
                                            <td><asp:TextBox ID="txtsec14FTDetails" TextMode="MultiLine" MaxLength="500" Width="97%" Height="50px" runat="server"></asp:TextBox> </td>
                                        </tr>
                                        <tr>
                                            <td>Safety Meeting Convened?</td>
                                            <td><asp:RadioButton ID="radsec14SMyes" runat="server" GroupName="radsec14SM" Text="Yes"  OnCheckedChanged="radsec14SM_OnCheckedChanged" AutoPostBack="true" />
                                                <asp:RadioButton ID="radsec14SMno" runat="server" GroupName="radsec14SM" Text="No"  OnCheckedChanged="radsec14SM_OnCheckedChanged" AutoPostBack="true" />
                                            </td>
                                            <td>Date of Meeting:</td>
                                            <td><asp:TextBox ID="txtsec14SMMeetingDate" MaxLength="11" runat="server" CssClass="date_only" Enabled="false" ></asp:TextBox> </td>
                                        </tr>
                                    </table>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>

                            <tr>
                                 <td colspan="3" class="hdng1">SUPPLEMENTARY INFORMATION ( free style writing )</td>                                
                            </tr>
                            <tr>
                                <td colspan="3">
                                <div class="comment" style="padding:2px">( Have you submitted supplementary information related to this accident? )</div>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align:middle; height:75px; padding-left:5px;" width="25%">
                                    <asp:RadioButton ID="radsec14SIyes" runat="server" GroupName="radsec14SI" Text="Yes" /> 
                                    <asp:RadioButton ID="radsec14SIno" runat="server" GroupName="radsec14SI" Text="No" />  
                                </td>
                                <td style="vertical-align:middle;" width="10%">Details :&nbsp;</td>
                                <td><asp:TextBox  ID="txtsec14SIyesDetails" TextMode="MultiLine" MaxLength="500"  Width="98%" Height="75px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                          </table>
                     </div>
                     <div id="Div15" runat="server" visible="false">
                         <div class='section'>
                             Section 15 : OFFICE USE ONLY
                         </div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                            <tr>
                               <td colspan="4">
                                   <table cellpadding="0" cellspacing="0" width="100%">
                                       <tr>
                                           <td colspan="4" class="hdng1">
                                               WAS ANY INVESTIGATION DONE BY OFFICE OR EXTERNAL PARTY?&nbsp;<asp:Label ID="lblsec15IDyn" runat="server"></asp:Label>
                                           </td>
                                       </tr>
                                       <tr id="trID" runat="server" visible="false">
                                           <td style="width: 10%;">
                                               Investigation Date :&nbsp;
                                           </td>
                                           <td style="width: 15%;">
                                               <asp:Label ID="lblsec15InvDt" runat="server" ForeColor="Blue"></asp:Label>
                                           </td>
                                           <td style="width: 15%; text-align: right">
                                               Name of Person / Organization :
                                           </td>
                                           <td style="text-align: left">
                                               <asp:Label ID="lblsec15NameOfPerson" runat="server" ForeColor="Blue"></asp:Label>
                                           </td>
                                       </tr>
                                     </table>
                                 </td>
                            </tr>
                             <tr>
                                 <td class="hdng1">
                                     Was the Vessel Delayed?&nbsp; : <asp:Label ID="lblsec15VDyn" runat="server" ForeColor="Blue"></asp:Label>
                                 </td>
                             </tr>
                             <tr id="trVD" runat="server" visible="false">
                                 <td>
                                     Days
                                     :
                                     <asp:Label ID="lblsec15VDDays" runat="server" ForeColor="Blue"></asp:Label>&nbsp;Hours
                                     :
                                     <asp:Label ID="lblsec15VDHrs" runat="server" ForeColor="Blue"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                 <td class="hdng1">
                                     Potential for Recurrence :&nbsp;
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:Label ID="lblsec15PR" runat="server" ForeColor="Blue"></asp:Label>
                                 </td>
                             </tr>
                            <tr>
                            <td colspan="4" class="hdng1">CAUSES OF ACCIDENT (As assessed by the office)</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                 <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                 <td class="hdng1">IMMEDIATE CAUSES</td>
                                 <td class="hdng1">ROOT CAUSES</td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class="hdng2">Human Actions</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 100px; text-align: center;"
                                                                class="ScrollAutoReset" id='Div2'>
                                                                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width: 100%;
                                                                    border-collapse: collapse;" class="newformat">
                                                                    <colgroup>
                                                                        <col />
                                                                        <col style="width: 17px;" />
                                                                    </colgroup>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rptOff_HA" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td align="left" class='<%#Eval("css")%>'>
                                                                                        &bull; <%#Eval("OptionText")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr id="trHAOther" runat="server" visible="false" >
                                                        <td>
                                                            Other (Specified) : <asp:Label ID="lblsec15HA_Other" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class="hdng2">Conditions</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 100px; text-align: center;"
                                                                class="ScrollAutoReset" id='Div3'>
                                                                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width: 100%;
                                                                    border-collapse: collapse;" class="newformat">
                                                                    <colgroup>
                                                                        <col />
                                                                        <col style="width: 17px;" />
                                                                    </colgroup>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rptOff_Cond" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td align="left" class='<%#Eval("css")%>'>
                                                                                        &bull; <%#Eval("OptionText")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="trCondOther" runat="server" visible="false">
                                                            Other (Specified) : <asp:Label ID="lblsec15Cond_Other" runat="server"></asp:Label>
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
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class="hdng2">Human Factors</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 100px; text-align: center;"
                                                                class="ScrollAutoReset" id='Div16'>
                                                                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width: 100%;
                                                                    border-collapse: collapse;" class="newformat">
                                                                    <colgroup>
                                                                        <col />
                                                                        <col style="width: 17px;" />
                                                                    </colgroup>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rptOff_HF" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td align="left" class='<%#Eval("css")%>'>
                                                                                        &bull; <%#Eval("OptionText")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr id="trHFOther" runat="server" visible="false">
                                                        <td>
                                                            Other (Specified) : <asp:Label ID="lblsec15HF_Other" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                    <tr>
                                                        <td class="hdng2">Job Factors</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 100px; text-align: center;"
                                                                class="ScrollAutoReset" id='Div17'>
                                                                <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width: 100%;
                                                                    border-collapse: collapse;" class="newformat">
                                                                    <colgroup>
                                                                        <col />
                                                                        <col style="width: 17px;" />
                                                                    </colgroup>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rptOff_JF" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td align="left" class='<%#Eval("css")%>'>
                                                                                        &bull; <%#Eval("OptionText")%>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr id="trJFOther" runat="server" visible="false">
                                                        <td>
                                                            Other (Specified) : <asp:Label ID="lblsec15JF_Other" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                     
                                 </td>
                            </tr> 
                             
                         </table>
                                </td>
                            </tr>
                            <tr id="trRCA" runat="server" visible="false">
                                 <td colspan="5">
                                     <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                         <tr>
                                             <td class="hdng1">
                                                 Reasons for Ship/Office Difference in RCA :
                                             </td>
                                         </tr>
                                         <tr>
                                             <td>
                                                 <asp:TextBox ID="txtsec15ReasonsForDiff" TextMode="MultiLine" Width="98%" Height="50px"
                                                     MaxLength="500" runat="server"></asp:TextBox>
                                             </td>
                                         </tr>
                                        </table>
                                 </td> 
                             </tr>
                            <tr>
                                <td colspan="4" class="hdng1">IS FURTHER FOLLOW UP REQUIRED?&nbsp;<asp:Label ID="lblsec15FRyn" runat="server"></asp:Label></td>
                            </tr>
                            <tr id="trFR" runat="server" visible="false">
                                <td colspan="4">
                                   <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                      <tr>
                                          <td style="width:120px;">Target Date : </td>
                                          <td style="width:100px;"><asp:Label ID="lblsec15TargetDt" runat="server"></asp:Label></td>
                                          <td style="width:120px;">(details of follow up)</td>
                                          <td><asp:TextBox ID="txtsec15YesDetails" TextMode="MultiLine" ReadOnly="true" Width="98%" Height="50px" MaxLength="500" runat="server"></asp:TextBox></td>
                                      </tr>
                                      <tr>
                                          <td>PIC for follow up :</td>
                                          <td colspan="4"><asp:Label ID="lblsec15PIC" runat="server"></asp:Label></td>
                                      </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="hdng1">SUGGESTIONS FOR IMPROVEMENT : </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblsec15Suggestions" runat="server" ForeColor="Blue"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:10%;" >Remarks :</td>
                                            <td>
                                                <asp:TextBox ID="txtsec15Remarks" TextMode="MultiLine" ReadOnly="true" Width="98%" Height="50px" MaxLength="500" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="hdng1">IS THIS INCIDENT CLOSED?&nbsp;<asp:Label ID="lblsec15IncCyn" runat="server"></asp:Label></td>
                            </tr>
                            <tr id="trIC" runat="server" visible="false">
                                <td colspan="4" >
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                      <tr>
                                         <td style="width:10%;">Date closed : </td>
                                         <td style="width:15%;"><asp:Label ID="lblsec15IncCD" runat="server"  ForeColor="Blue"></asp:Label></td>
                                         <td style="width:7%;">Closed by : </td>
                                         <td><asp:Label ID="lblsec15IncCB" runat="server"  ForeColor="Blue"></asp:Label></td>
                                      </tr>
                                    </table>
                                </td>
                            </tr>
                             <tr>
                                <td colspan="4" class="hdng1">IS VESSEL NOTIFIED OF INCIDENT CLOSURE?&nbsp;<asp:Label ID="lblsec15CNyn" runat="server"></asp:Label></td>
                            </tr>
                            <tr id="trVN" runat="server" visible="false">
                                <td colspan="4" >
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                      <tr>
                                         <td style="width:10%;">Date of Notification:</td>
                                         <td><asp:Label ID="lblsec15ND" runat="server"></asp:Label></td>
                                      </tr>
                                    </table>
                                </td>
                            </tr>
                         </table>
                    </div>
                    <div id="Div16" runat="server" visible="false">
                         <div class='section'>
                             Section 16 : Documents
                             <asp:ImageButton ID="imgbtnUploadDocsShowPanel" OnClick="imgbtnUploadDocsShowPanel_Click" ImageUrl="~/Modules/PMS/Images/add.png" ToolTip="Upload Documents" style="padding-top:0px" runat="server" Visible="false" CausesValidation="false" />
                         </div>
                         <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding-top:2px">
                            <tr>
                            <td>
                                 <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 25px ; text-align:center; border-bottom:none;" class="scrollbox">
                           <table cellspacing="0" rules="all" border="1" bordercolor="white" cellpadding="4" style="width:100%;border-collapse:collapse; background-color:#FF9933; color: #fff;  height:25px;" >
                                <colgroup>
                                    <col style="width:30px;" />
                                    <col style="width:30px;" />
                                    <col style="width:30px;" />
                                    <col style="width:90px;" />
                                    <col />
                                    <col style="width:150px;" />
                                    <col style="width:100px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <tr>
                                    <td style="color:White;vertical-align:middle; text-align:center;">Sr#</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td style="color:White;vertical-align:middle; text-align:left;">&nbsp;Location</td>
                                    <td style="color:White;vertical-align:middle; text-align:left;">&nbsp;Description</td>
                                    <td style="color:White;vertical-align:middle;text-align:left;">&nbsp;Uploaded By</td>
                                    <td style="color:White;vertical-align:middle;">&nbsp;Uploaded On</td>
                                    <td style="color:White;vertical-align:middle;"></td>
                                </tr>
                                </table>
                           </div>
                           <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 385px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="none" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:30px;" />
                                    <col style="width:30px;" />
                                    <col style="width:30px;" />
                                    <col style="width:90px;" />
                                    <col />
                                    <col style="width:150px;" />
                                    <col style="width:100px;" />
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptReportDocs" runat="server">
                                    <ItemTemplate>
                                            <tr>
                                            <td style="text-align:right"><%#Eval("SrNo")%>.</td>
                                            <td>
                                                <asp:ImageButton runat="server" Visible='<%#(Eval("Office_Ship").ToString().Trim()=="Ship" && Eval("Edit_Delete").ToString().Trim()=="E")%>' ImageUrl="~/Modules/PMS/Images/cancel.png" ID="btnDelDocument" CommandArgument='<%#Eval("DocId")%>' OnClick="btnDelDocument_Click" OnClientClick="return confirm('Are you sure to delete ?');"/>
                                            </td>
                                            <td><a href='<%# "./Documents/" +Eval("Office_Ship") + "/" + Eval("FileName").ToString()%>' target="_blank"><img src="../../Images/paperclip.gif" /></a></td>
                                            <td style="text-align:left;">&nbsp;<%#Eval("Office_Ship")%></td>
                                            <td align="left">
                                            <%--&nbsp;<asp:ImageButton ID="imgbtnUpdateDocDescr" CommandArgument='<%#Eval("DocId")%>' OnClick="imgbtnUpdateDocDescr_Click" ImageUrl="~/Modules/PMS/Images/AddPencil.gif" ToolTip="Update Description" style="padding-top:0px" runat="server" Visible='<%#Eval("Office_Ship").ToString().Trim()=="Ship"%>' />--%>
                                            &nbsp;<%#Eval("Descr")%></td>                                            
                                            <td align="left"><%#Eval("UploadedBy")%></td>
                                            <td align="center"><%#Common.ToDateString(Eval("UploadedOn"))%></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </ItemTemplate>       
                                    <AlternatingItemTemplate>
                                            <tr style="background-color:#FFF5E6">
                                            <td style="text-align:right"><%#Eval("SrNo")%>.</td>

                                            <td>
                                                <asp:ImageButton runat="server" Visible='<%#(Eval("Office_Ship").ToString().Trim()=="Ship" && Eval("Edit_Delete").ToString().Trim()=="E")%>' ImageUrl="~/Modules/PMS/Images/cancel.png" ID="btnDelDocument" CommandArgument='<%#Eval("DocId")%>' OnClick="btnDelDocument_Click" OnClientClick="return confirm('Are you sure to delete ?');"/>
                                            </td>
                                            <td><a href='<%# "./Documents/" +Eval("Office_Ship") + "/" + Eval("FileName").ToString()%>' target="_blank"><img src="../../Images/paperclip.gif" /></a></td>

                                            <td style="text-align:left;">&nbsp;<%#Eval("Office_Ship")%></td>
                                            <td align="left">
                                            <%--&nbsp;<asp:ImageButton ID="imgbtnUpdateDocDescr" CommandArgument='<%#Eval("DocId")%>' OnClick="imgbtnUpdateDocDescr_Click" ImageUrl="~/Modules/PMS/Images/AddPencil.gif" ToolTip="Update Description" style="padding-top:0px" runat="server" Visible='<%#Eval("Office_Ship").ToString().Trim()=="Ship"%>' />--%>
                                            &nbsp;<%#Eval("Descr")%></td>                                                
                                            <td align="left"><%#Eval("UploadedBy")%></td>
                                            <td align="center"><%#Common.ToDateString(Eval("UploadedOn"))%></td>
                                            <td>&nbsp;</td>
                                           </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </table>
                           </div>
                                
                            </td>
                            </tr>
                         </table>
                    </div>


                     <div id="Div18" runat="server" visible="false">
                         <iframe id="iframRca" runat="server" src="/TEMP/RCA_LIG-2015-S115-001.pdf" frameborder="0" width="100%" height="450px"></iframe>
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
                         <td id="tdTabs" runat="server" style="width:450px;padding-right:10px" >
                            <asp:Button ID="btnSaveDraft" runat="server" Text="Save Report" CssClass="btn" style="float:left" onclick="btnSaveDraft_Click" CausesValidation="true" Width='120px'/>
                            <asp:Button ID="btnExportToOffice" runat="server" Text="Send for Export" CssClass="btn" style="float:left" onclick="btnExportToOffice_Click" CausesValidation="true" Width='140px' OnClientClick="return confirm('Are you sure to send for export?');"/>
                            <asp:Button ID="btnSendDocs" runat="server" Text="Documents Export" CssClass="btn" style="float:left" onclick="btnExportDocsToOffice_Click" CausesValidation="true" Width='140px' OnClientClick="return confirm('Are you sure to export?');"/>
                        </td>
                        <td style="text-align:left">
                          <table width="100%" cellpadding="0" cellspacing="0" border="0" >
                              
                          <tr>
                          <td>
                            <asp:button ID="btnINJ" runat="Server" Text="Injury to People" CssClass="color_tab" CommandArgument="Div4" OnClick="btn_Tab_Click" Visible="false"/>
                            <asp:button ID="btnCC" runat="Server" Text="Cargo Contamination" CssClass="color_tab" CommandArgument="Div5" OnClick="btn_Tab_Click" Visible="false"/>
                            <asp:button ID="btnNav" runat="Server" Text="Navigation" CssClass="color_tab" CommandArgument="Div6" OnClick="btn_Tab_Click" Visible="false"/>
                            <asp:button ID="btnPol" runat="Server" Text="Pollution" CssClass="color_tab" CommandArgument="Div7" OnClick="btn_Tab_Click" Visible="false"/>
                            <asp:button ID="btnMoor" runat="Server" Text="Mooring" CssClass="color_tab" CommandArgument="Div8" OnClick="btn_Tab_Click" Visible="false"/>
                            <asp:button ID="btnEF" runat="Server" Text="Equipment Failure" CssClass="color_tab" CommandArgument="Div9" OnClick="btn_Tab_Click" Visible="false"/>
                            <asp:button ID="btnDP" runat="Server" Text="Damage to Property" CssClass="color_tab" CommandArgument="Div10" OnClick="btn_Tab_Click" Visible="false"/>
                            <asp:button ID="btnFire" runat="Server" Text="Fire" CssClass="color_tab" CommandArgument="Div11" OnClick="btn_Tab_Click" Visible="false"/>
                            <asp:button ID="btnSec" runat="Server" Text="Security" CssClass="color_tab" CommandArgument="Div12" OnClick="btn_Tab_Click" Visible="false"/>
                            <span runat="server" id="spn_Note" style="color:Red; font-size:15px; text-align:left;">< Please fill up these sections to complete the form.</span>
                            </td>
                           </tr>
                           </table>
                        </td>
                        </tr>
                        <tr style=" background-color:#FFFFFF">
                        <td colspan="2">&nbsp;&nbsp;<asp:Label ID="lblMsg" ForeColor="Red" Font-Size="16px" runat="server" Text="" CssClass="msg"></asp:Label>
                        </td>
                        </tr>
                    </table>
                    </center>
                 </div>
             </td>
         </tr>
         </table>
    </div>

    <div style="position:absolute;top:0px;left:0px; height :560px; width:100%; " id="dvAddEditSection4" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:1100px;  height:485px;padding :5px; text-align :center;background : white; z-index:150;top:30px; border:solid 0px black;">
            <center >
            <div class='section'>Add Edit Injury to personnel</div>
            <div style="min-height:452px">
            <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                             <tr>
                                 <td style="text-align:left;">
                                     <asp:RadioButton ID="radsec4Crew" runat="server" GroupName="radsec4CC" Text="Crew"  OnCheckedChanged="radsec4CC_OnCheckedChanged" AutoPostBack="true" />
                                     <asp:RadioButton ID="radsec4Contractor" runat="server" GroupName="radsec4CC" Text="Contractor"  OnCheckedChanged="radsec4CC_OnCheckedChanged" AutoPostBack="true"/>
                                     <label id="val_radsec4CC" class="mndt"></label>
                                     &nbsp;<span class="comment">( if both Crew &amp; Contractor are injured prepare a separate
                                         report for each )</span>
                                     <asp:HiddenField ID="hfCrewTableId" runat="server" />
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                         <tr>
                                             <td width="27%">
                                                 <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                     <tr >
                                                         <td class='hdng1'>
                                                             A. Crew#
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox runat="server" ID="txtsec4IpCrewNo" MaxLength="6" Width="20%" Enabled="false"></asp:TextBox>
                                                             <label id="lblcrewno" class="mndt"></label>
                                                         </td>
                                                     </tr>
                                                     <tr >
                                                         <td class='hdng1'>
                                                             B. Injured Person Name
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox runat="server" ID="txtsec4IpName" MaxLength="50" Width="98%"></asp:TextBox>
                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtsec4IpName" ErrorMessage="*" ForeColor="Red" ValidationGroup="ITP"></asp:RequiredFieldValidator>
                                                         </td>
                                                     </tr>
                                                     <tr >
                                                         <td class='hdng1'>
                                                             C. Date of Birth
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox runat="server" ID="txtsec4DOB" MaxLength="12"  CssClass="date_only" ></asp:TextBox>
                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtsec4DOB" ErrorMessage="*" ForeColor="Red" ValidationGroup="ITP"></asp:RequiredFieldValidator>
                                                         </td>
                                                     </tr>
                                                     <tr >
                                                         <td class='hdng1'>
                                                             D. Nationality / ID No.
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox runat="server" ID="txtsec4Nat" MaxLength="50"></asp:TextBox>
                                                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtsec4Nat" ErrorMessage="*" ForeColor="Red" ValidationGroup="ITP"></asp:RequiredFieldValidator>
                                                        </td>
                                                     </tr>
                                                     <tr >
                                                         <td class='hdng1'>
                                                             E. Rank
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:DropDownList ID="ddlsec4Rank" runat="server" Enabled="false"></asp:DropDownList>
                                                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" InitialValue="0" ControlToValidate="ddlsec4Rank" ErrorMessage="*" ForeColor="Red" ValidationGroup="ITP"></asp:RequiredFieldValidator>
                                                        </td>
                                                     </tr>
                                                     <tr>
                                                         <td class='hdng1'>
                                                             F. Date signed on
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox runat="server" ID="txtsec4SDate" MaxLength="50"  CssClass="date_only" ></asp:TextBox>
                                                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtsec4SDate" ErrorMessage="*" ForeColor="Red" ValidationGroup="ITP"></asp:RequiredFieldValidator>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td class='hdng1'>
                                                             G. For Contractor No of Hours onboard prior to the accident.
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:TextBox runat="server" ID="txtsec4NoOfHr" MaxLength="50" Enabled="false"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                 </table>
                                             </td>
                                             <td width="25%">
                                                 <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                     <tr>
                                                         <td class='hdng1'>
                                                             H. Was the visit to a doctor neccessary.
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:RadioButton ID="radsec4DNyes" runat="server" GroupName="radsec4DN" Text="Yes ( Complete form D110 )" />
                                                             <asp:RadioButton ID="radsec4DNno" runat="server" GroupName="radsec4DN" Text="No" />
                                                             <label id="val_radsec4DN" class="mndt"></label>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td class='hdng1'>
                                                             I. Activity
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:RadioButton ID="radsec4OFD_od" runat="server" GroupName="radsec4OFD" Text="On Duty" />
                                                             <asp:RadioButton ID="radsec4OFD_fd" runat="server" GroupName="radsec4OFD" Text="Off Duty" />
                                                             <label id="val_radsec4OFD" class="mndt"></label>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             No of Hours :
                                                             <asp:TextBox runat="server" ID="txtsec4NOH"></asp:TextBox>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td class='hdng1'>
                                                             J. Restricted Work
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:RadioButton ID="radsec4RWyes" runat="server" GroupName="radsec4RW" Text="Yes" />
                                                             <asp:RadioButton ID="radsec4RWno" runat="server" GroupName="radsec4RW" Text="No" />
                                                             <label id="val_radsec4RW" class="mndt"></label>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td class='hdng1'>
                                                             K. Medical Treatment Required
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:RadioButton ID="radsec4MTRyes" runat="server" GroupName="radsec4MTR" Text="Yes" />
                                                             <asp:RadioButton ID="radsec4MTRno" runat="server" GroupName="radsec4MTR" Text="No" />
                                                             <label id="val_radsec4MTR" class="mndt"></label>
                                                         </td>
                                                     </tr>
                                                 </table>
                                             </td>
                                             <td width="25%">
                                                 <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                     <tr>
                                                         <td class='hdng1'>
                                                             L. Area of Operation
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:RadioButtonList runat="server" ID="radsec4AOO" >
                                                             </asp:RadioButtonList>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                        <td style="text-align:left;">
                                                            Other (Specify)
                                                            <asp:TextBox runat="server" ID="txtsec4AOOOther"></asp:TextBox>
                                                            <label id="val_radsec4AOO" class="mndt"></label>
                                                        </td>
                                                    </tr>
                                                 </table>
                                             </td>
                                             <td width="23%">
                                                 <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                                     <tr>
                                                         <td class='hdng1'>
                                                             M. Type of Accident
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:left;">
                                                             <asp:RadioButtonList runat="server" ID="radsec4TOA"></asp:RadioButtonList>
                                                         </td>
                                                     </tr>
                                                     <tr>
                                                        <td style="text-align:left;">
                                                            Other (Specify)
                                                            <asp:TextBox runat="server" ID="txtsec4TOAOther"></asp:TextBox>
                                                            <label id="val_radsec4TOA" class="mndt"></label>
                                                        </td>
                                                    </tr>
                                                 </table>
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                             <tr>
                                 <td>
                                     <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                         <tr>
                                             <td class='hdng1'>
                                                 OCIMF MARINE INJURY REPORTING FORMAT
                                             </td>
                                         </tr>
                                         <tr>
                                             <td style="text-align:left;">
                                                 <asp:RadioButtonList ID="rdosec4MIRF" RepeatDirection="Horizontal" runat="server"></asp:RadioButtonList>
                                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="rdosec4MIRF" ErrorMessage="*" ForeColor="Red" ValidationGroup="ITP"></asp:RequiredFieldValidator>
                                             </td>
                                                 
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                             <tr>
                              <td style="text-align:right;">
                                <asp:Label runat="server" ID="lblMessage_ITP" ForeColor="Red" Text=""></asp:Label>
                                <asp:Button ID="btnAddEditCrew" Text="Save" OnClick="btnAddEditCrew_Click" CssClass="btn"  runat="server" CausesValidation="true" ValidationGroup="ITP" OnClientClick="return Validate_ITP();"/>
                              </td>
                             </tr>
            </table>
            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnAddEditCrew" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
            <div style="text-align:right; position:relative; right:-22px; top:-2px;">
                <asp:ImageButton runat="server" ID="btnClose" Text="Close" onclick="btnClose_Click" ImageUrl="~/Modules/PMS/Images/close-button.png" CausesValidation="false" title='Close this Window !'/>   
            </div>
            </center>
         </div>
         </center>
    </div>
    
    <div style="position:absolute;top:0px;left:0px; height :560px; width:100%; " id="dvUploadDoc" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:1100px;  height:485px;padding :5px; text-align :center;background : white; z-index:150;top:30px; border:solid 0px black;">
            <center >
            <div class='section'>Upload Documents</div>
            <div style="min-height:452px">
            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%" border="0" >
            <tr>
                <td style="text-align:left;">
                    <iframe runat="server" id="frmDocs" src="" width="100%" scrolling="yes" frameborder="0" height="450px"></iframe>
                </td>
            </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div style="text-align:right; position:relative; right:-22px; top:-2px;">
                <asp:ImageButton runat="server" ID="btnCloseDocWindow" Text="Close" onclick="btnCloseDocWindow_Click" ImageUrl="~/Modules/PMS/Images/close-button.png" CausesValidation="false" title='Close this Window !'/>   
            </div>
            </center>
         </div>
         </center>
    </div>
    <div style="position:absolute;top:0px;left:0px; height :560px; width:100%; " id="dvUpdateDocDescr" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:500px;  height:300px;padding :5px; text-align :center;background : white; z-index:150;top:30px; border:solid 0px black;">
            <center >
            <div class='section'>Update Description
            <asp:HiddenField ID="hfDocId" runat="server" />
            </div>
            <div style="min-height:300px">
            <asp:UpdatePanel runat="server" ID="UpdatePanel7">
            <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%" border="0" >
            <tr>
                <td style="text-align:left;">
                     <asp:TextBox ID="txtDocDescription" TextMode="MultiLine" Height="200px" Width="490px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpdateDocDescr" CssClass="color_tab" Text="Save" OnClick="btnUpdateDescr_Click" runat="server" />
                </td>
            </tr>
            <tr>
                 <td>
                      <asp:Label ID="lblMsg_Doc" ForeColor="Red" runat="server"></asp:Label>
                 </td>
            </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div style="text-align:right; position:relative; right:-22px; top:-2px;">
                <asp:ImageButton runat="server" ID="btnCloseDocUpdate" Text="Close" onclick="btnCloseDocUpdate_Click" ImageUrl="~/Modules/PMS/Images/close-button.png" CausesValidation="false" title='Close this Window !'/>   
            </div>
            </center>
         </div>
         </center>
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
