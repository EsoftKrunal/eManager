<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VesselDetailsOther.ascx.cs" Inherits="VesselRecord_VesselDetailsOther" %>
<style type="text/css">
    .style1
    {
        width: 190px;
        height: 22px;
    }
    .style2
    {
        height: 22px;
    }
    .style3
    {
        width: 146px;
        height: 22px;
    }
</style>
<script type="text/javascript">
 function ShowPrint()
 {
    var str;
    str='<% Response.Write(this.VesselId.ToString()); %>';
    window.open('../Reporting/Vessel_Details_Report.aspx?VID=' + str);
 }
 </script>
<link rel="stylesheet" type="text/css" href="../Styles/sddm.css" />
      <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<%-- <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
<div style="text-align: center">
    <asp:Label ID="lbl_message_other" runat="server" Text="Record Successfully Saved." Visible=False ForeColor="#C00000"></asp:Label>
    <table align="center" width="500px" border="1" cellpadding="0" cellspacing="0" style="font-family:Arial;font-size:12px;">
    <tr>
       <td style="background-color : #e2e2e2;">
            <table cellpadding="0" cellspacing="0" width="100%" border="0" style="height: 30px; padding-right :10px;">
              <tr>
                 <td style ="padding-left :30px; text-align :right " >Vessel Name:</td>
                 <td style=" text-align :left "><asp:TextBox ID="txtVesselName" ReadOnly="true" BackColor="#e2e2e2" runat="server" CssClass="input_box" MaxLength="49" Width="184px" TabIndex="1"></asp:TextBox></td>
                 <td style=" text-align :right ">Former Name:</td>
                 <td style=" text-align :left "><asp:TextBox ID="txtFormerVesselName" ReadOnly="true" BackColor="#e2e2e2" runat="server" CssClass="input_box" MaxLength="49" Width="122px"></asp:TextBox></td>
                 <td style=" text-align :right ">Flag:</td>
                 <td style=" text-align :left "><asp:DropDownList ID="ddlFlagStateName" Enabled="false" BackColor="#e2e2e2" runat="server" CssClass="input_box" Width="128px" TabIndex="2"></asp:DropDownList></td>
            </tr>
           </table>
       </td>
    </tr>
    <tr>
       <td>
           <table border="0" cellpadding="0" cellspacing="0" style="text-align: center; height :180px;" width="100%">
                       <tr>
                           <td  colspan="2" style="text-align: left; padding-left:15px">
                               <strong>
                             Main Engine:</strong></td>
                           <td  style="padding-right: 15px; text-align: right">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: left;">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: left">
                           </td>
                       </tr>
                     <tr>
                         <td  style="text-align: right; padding-right:10px">
                             &nbsp;Maker:</td>
                         <td style="text-align: left;">
                             <asp:DropDownList ID="ddlMainEngineMaker" runat="server" CssClass="input_box" Width="130px" TabIndex="3">
                             </asp:DropDownList></td>
                         <td  style="text-align: right; padding-right:10px">
                             &nbsp;Model:</td>
                         <td style="text-align: left">
                             <asp:TextBox ID="txtMainEngineModel" runat="server" CssClass="input_box" Width="83px" MaxLength="29" TabIndex="4"></asp:TextBox></td>
                         <td style="text-align: left; padding-right:10px;">
                             &nbsp;Stroke Type:</td>
                         <td style="text-align: left">
                             <asp:DropDownList ID="ddlMainStrokeType" runat="server" CssClass="input_box" Width="80px" TabIndex="5">
                             </asp:DropDownList>
                         </td>
                         <td style="text-align: right; padding-right:10px">
                             &nbsp;KW:</td>
                         <td style="text-align: left">
                             <asp:TextBox ID="txtMainEngineKW" runat="server" Width="50px" CssClass="input_box" MaxLength="5" TabIndex="6"></asp:TextBox></td>
                         <td style="text-align: right; padding-right:10px">
                             &nbsp;BHP:</td>
                         <td style="text-align: left">
                             <asp:TextBox ID="txtMainBHP" runat="server" CssClass="input_box" MaxLength="5" Width="50px" TabIndex="7"></asp:TextBox></td>
                         <td style="text-align: left; padding-right:10px">
                             &nbsp;RPM:</td>
                         <td style="text-align: left">
                             <asp:TextBox ID="txtMainRPM" runat="server" CssClass="input_box" MaxLength="5" Width="50px" TabIndex="8"></asp:TextBox></td>
                     </tr>
                     <tr>
                         <td  style="text-align: left; padding-left: 15px; height: 19px;" 
                             colspan="2">
                             <strong>Aux1 Engine:</strong></td>
                         <td style="text-align: right; padding-right: 15px; height: 19px;" >
                         </td>
                         <td style="text-align: left; height: 19px;">
                         </td>
                         <td style="text-align: left; height: 19px;">
                         </td>
                         <td style="text-align: left; height: 19px;">
                         </td>
                         <td style="text-align: left; height: 19px;">
                         </td>
                         <td style="text-align: left; height: 19px;">
                         </td>
                         <td style="text-align: left; height: 19px;">
                         </td>
                         <td style="text-align: left; height: 19px;">
                         </td>
                         <td style="text-align: left; height: 19px;">
                         </td>
                         <td style="text-align: left; height: 19px;">
                         </td>
                     </tr>
                       <tr>
                           <td  style="text-align: right; padding-right: 10px;">
                               &nbsp;Maker:</td>
                           <td style="text-align: left; ">
                               <asp:DropDownList ID="ddlAux1EngineMaker" runat="server" CssClass="input_box" 
                                   TabIndex="9" Width="130px">
                               </asp:DropDownList>
                           </td>
                           <td style="text-align: right; padding-right: 10px;" >
                               &nbsp;Model:</td>
                           <td style="text-align: left; ">
                               <asp:TextBox ID="txtAux1EngineModel" runat="server" CssClass="input_box" 
                                   MaxLength="29" TabIndex="10" Width="83px"></asp:TextBox>
                           </td>
                           <td style="text-align: left; padding-right: 10px;">
                               &nbsp;Stroke Type:</td>
                           <td style="text-align: left; ">
                               <asp:DropDownList ID="ddlAux1StrokeType" runat="server" CssClass="input_box" 
                                   TabIndex="11" Width="80px">
                               </asp:DropDownList>
                           </td>
                           <td style="text-align: right; padding-right: 10px;">
                               &nbsp;KW:</td>
                           <td style="text-align: left; ">
                               <asp:TextBox ID="txtAux1EngineKW" runat="server" CssClass="input_box" 
                                   MaxLength="5" TabIndex="12" Width="50px"></asp:TextBox>
                           </td>
                           <td style="text-align: right; padding-right: 10px;">
                               &nbsp;BHP:</td>
                           <td style="text-align: left; ">
                               <asp:TextBox ID="txtAux1BHP" runat="server" CssClass="input_box" MaxLength="5" 
                                   TabIndex="13" Width="50px"></asp:TextBox>
                           </td>
                           <td style="text-align: left; padding-right: 10px;">
                               &nbsp;RPM:</td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtAux1RPM" runat="server" CssClass="input_box" MaxLength="5" 
                                   TabIndex="14" Width="50px"></asp:TextBox>
                           </td>
                       </tr>
                     <tr>
                         <td  style="text-align: left; padding-left: 15px;" colspan="2">
                             <strong>Aux2 Engine:</strong></td>
                         <td style="text-align: right; padding-right: 15px;" >
                         </td>
                         <td style="text-align: left">
                         </td>
                         <td style="text-align: left">
                         </td>
                         <td style="text-align: left;">
                         </td>
                         <td style="text-align: right; padding-right: 15px;">
                         </td>
                         <td style="text-align: left">
                         </td>
                         <td style="text-align: right">
                         </td>
                         <td style="text-align: left">
                         </td>
                         <td style="text-align: left">
                         </td>
                         <td style="text-align: left">
                         </td>
                     </tr>
                       <tr>
                           <td  style="text-align: right; padding-right: 10px;">
                               &nbsp;Maker:</td>
                           <td style="text-align: left">
                               <asp:DropDownList ID="ddlAux2EngineMaker" runat="server" CssClass="input_box" 
                                   TabIndex="15" Width="130px">
                               </asp:DropDownList>
                           </td>
                           <td style="text-align: right; padding-right: 10px;" >
                               &nbsp;Model:</td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtAux2EngineModel" runat="server" CssClass="input_box" 
                                   MaxLength="29" TabIndex="16" Width="83px"></asp:TextBox>
                           </td>
                           <td style="text-align: left; padding-right: 10px;">
                               &nbsp;Stroke Type:</td>
                           <td style="text-align: left">
                               <asp:DropDownList ID="ddlAux2StrokeType" runat="server" CssClass="input_box" 
                                   TabIndex="17" Width="80px">
                               </asp:DropDownList>
                           </td>
                           <td style="text-align: right; padding-right: 10px;">
                               &nbsp;KW:</td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtAux2EngineKW" runat="server" CssClass="input_box" 
                                   MaxLength="5" TabIndex="18" Width="50px"></asp:TextBox>
                           </td>
                           <td style="text-align: right; padding-right: 10px;">
                               &nbsp;BHP:</td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtAux2BHP" runat="server" CssClass="input_box" MaxLength="5" 
                                   TabIndex="19" Width="50px"></asp:TextBox>
                           </td>
                           <td style="text-align: left; padding-right: 10px;">
                               &nbsp;RPM:</td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtAux2RPM" runat="server" CssClass="input_box" MaxLength="5" 
                                   TabIndex="20" Width="50px"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td  style="text-align: left; padding-left: 15px;" colspan="2">
                               <strong>Aux3 Engine:</strong></td>
                           <td style="text-align: right; padding-right: 10px;" >
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: left; padding-right: 10px;">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: right; padding-right: 10px;">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: right; padding-right: 10px;">
                           </td>
                           <td style="text-align: left">
                           </td>
                           <td style="text-align: left; padding-right: 10px;">
                           </td>
                           <td style="text-align: left">
                           </td>
                       </tr>
                       <tr>
                           <td  style="text-align: right; padding-right: 10px;">
                               &nbsp;Maker:</td>
                           <td style="text-align: left">
                               <asp:DropDownList ID="ddlAux3EngineMaker" runat="server" CssClass="input_box" 
                                   TabIndex="21" Width="130px">
                               </asp:DropDownList>
                           </td>
                           <td style="text-align: right; padding-right: 10px;" >
                               &nbsp;Model:</td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtAux3EngineModel" runat="server" CssClass="input_box" 
                                   MaxLength="29" TabIndex="22" Width="83px"></asp:TextBox>
                           </td>
                           <td style="text-align: left; padding-right: 10px;">
                               &nbsp;Stroke Type:</td>
                           <td style="text-align: left">
                               <asp:DropDownList ID="ddlAux3StrokeType" runat="server" CssClass="input_box" 
                                   TabIndex="23" Width="80px">
                               </asp:DropDownList>
                           </td>
                           <td style="text-align: right; padding-right: 10px;">
                               &nbsp;KW:</td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtAux3EngineKW" runat="server" CssClass="input_box" 
                                   MaxLength="5" TabIndex="24" Width="50px"></asp:TextBox>
                           </td>
                           <td style="text-align: right; padding-right: 10px;">
                               &nbsp;BHP:</td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtAux3BHP" runat="server" CssClass="input_box" MaxLength="5" 
                                   TabIndex="25" Width="50px"></asp:TextBox>
                           </td>
                           <td style="text-align: left; padding-right: 10px;">
                               &nbsp;RPM:</td>
                           <td style="text-align: left">
                               <asp:TextBox ID="txtAux3RPM" runat="server" CssClass="input_box" MaxLength="5" 
                                   TabIndex="26" Width="50px"></asp:TextBox>
                           </td>
                       </tr>
                       <tr>
                           <td  style="text-align: right; padding-right: 10px;">
                               &nbsp;</td>
                           <td style="text-align: left">
                               &nbsp;</td>
                           <td style="text-align: right; padding-right: 10px;" >
                               &nbsp;</td>
                           <td style="text-align: left">
                               &nbsp;&nbsp;&nbsp;
                           </td>
                           <td style="text-align: left; padding-right: 10px;">
                               &nbsp;</td>
                           <td style="text-align: left">
                               &nbsp;</td>
                           <td style="text-align: right; padding-right: 10px;">
                               &nbsp;</td>
                           <td style="text-align: left">
                               &nbsp;</td>
                           <td style="text-align: right; padding-right: 10px;">
                               &nbsp;</td>
                           <td style="text-align: left">
                               &nbsp;</td>
                           <td style="text-align: left; padding-right: 10px;">
                               &nbsp;</td>
                           <td style="text-align: left">
                               &nbsp;</td>
                       </tr>
                 </table>
       </td>
    </tr>
    <tr>
    <td>
           <table cellpadding="0" cellspacing="0" width="100%" style="height: 130px">
                                              <tr>
                                                 <td style="padding-right:5px; width: 190px; text-align: right;">Inm.Terminal Type:</td>
                                                 <td align="left" style="width: 140px">
                                                     <asp:TextBox ID="txtinmarsatTerminal" runat="server" CssClass="input_box" MaxLength="19" TabIndex="27" Width="170px"></asp:TextBox>
                                                 </td>
                                                 <td style="padding-right:5px; width: 146px; text-align: right;">Call Sign:</td>
                                                 <td align="left" style="width: 133px">
                                                     <asp:TextBox ID="txtcallsign" runat="server" CssClass="input_box" MaxLength="49" TabIndex="28" Width="170px"></asp:TextBox>
                                                 </td>
                                                 <td style="padding-right:5px; width: 143px; text-align: right;">Ins. Valid Days :</td>
                                                 <td align="left" style="width: 259px">
                                                     <asp:TextBox ID="txtInsDays" runat="server" CssClass="input_box" MaxLength="28" TabIndex="31" Width="150px"></asp:TextBox>
                                                 </td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right: 15px; width: 190px; text-align: right;">
                                                      Tel 1:</td>
                                                  <td align="left" style="width: 140px">
                                                      <asp:TextBox ID="txttel1" runat="server" CssClass="input_box" MaxLength="25" TabIndex="29"
                                                          Width="170px"></asp:TextBox></td>
                                                  <td  style="padding-right:5px; width: 146px; text-align: right;">
                                                      Tel 2:</td>
                                                  <td align="left" style="width: 133px">
                                                      <asp:TextBox ID="txttel2" runat="server" CssClass="input_box" MaxLength="25" TabIndex="30"
                                                          Width="170px"></asp:TextBox></td>
                                                  <td  style="padding-right:5px; width: 143px; text-align: right;">
                                                      Mobile:</td>
                                                  <td align="left" style="width: 259px">
                                                      <asp:TextBox ID="txtmobile" runat="server" CssClass="input_box" MaxLength="15" TabIndex="31"
                                                          Width="150px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right:5px; text-align: right;" class="style1">
                                                      Fax:</td>
                                                  <td align="left" class="style2">
                                                      <asp:TextBox ID="txtfax" runat="server" CssClass="input_box" MaxLength="25" TabIndex="32"
                                                          Width="170px"></asp:TextBox></td>
                                                  <td  style="padding-right:5px; text-align: right;" class="style3">
                                                      Shipsoft Email:</td>
                                                  <td align="left"  class="style2">
                                                      <asp:TextBox ID="txtemail" runat="server" CssClass="input_box" MaxLength="99" TabIndex="33"
                                                          Width="404px"></asp:TextBox></td>

                                                  <td  style="padding-right:5px; text-align: right;" class="style3">Manning :</td>
                                                  <td  align="left"> 
                                                      <asp:DropDownList ID="ddlManningOffice" runat="server" CssClass="input_box"></asp:DropDownList>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right:5px; width: 190px; text-align: right;">
                                                      DATA:</td>
                                                  <td align="left">
                                                      <asp:TextBox ID="txtdata" runat="server" CssClass="input_box" MaxLength="49" TabIndex="34"
                                                          Width="170px"></asp:TextBox></td>
                                                  <td  style="padding-right:5px; width: 146px; text-align: right;">
                                                      HSD:</td>
                                                  <td align="left">
                                                      <asp:TextBox ID="txthsd" runat="server" CssClass="input_box" MaxLength="49" TabIndex="35"
                                                          Width="170px"></asp:TextBox></td>
                                                  <td  style="padding-right:5px; width: 143px; text-align: right;">
                                                      Inm.C:</td>
                                                  <td align="left" style="width: 259px">
                                                      <asp:TextBox ID="txtinmarsat" runat="server" CssClass="input_box" 
                                                          MaxLength="49" TabIndex="36"
                                                          Width="150px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td  style="padding-right:5px; width: 190px; text-align: right;">
                                                      Acc. Code:</td>
                                                  <td align="left">
                                                      <asp:TextBox ID="txtAccCode" runat="server" CssClass="input_box" MaxLength="5" TabIndex="30" Width="170px"></asp:TextBox>
                                                      <ajaxToolkit:FilteredTextBoxExtender runat="server" Id="FilteredTextBoxExtender1" FilterMode="ValidChars" TargetControlID="txtAccCode" ValidChars="0123456789"></ajaxToolkit:FilteredTextBoxExtender>   
                                                  </td>
                                                  <td  style="padding-right:5px; width: 146px; text-align: right;">
                                                      Training Fee:</td>
                                                  <td align="left">
                                                      <asp:TextBox ID="txtTrainingFee" runat="server" CssClass="input_box" MaxLength="10" TabIndex="30" Width="170px"></asp:TextBox>
                                                      <ajaxToolkit:FilteredTextBoxExtender runat="server" Id="FilteredTextBoxExtender2" FilterMode="ValidChars" TargetControlID="txtTrainingFee" ValidChars=".0123456789"></ajaxToolkit:FilteredTextBoxExtender>   
                                                  </td>
                                                  <td  style="padding-right:5px; width: 143px; text-align: right;">
                                                      Fleet :</td>
                                                  <td align="left" style="width: 259px">
                              
                                                  </td>
                                              </tr>
                                          </table>
    </td>
    
    </tr>
    <tr>
    <td style =" background-color :#e2e2e2; height :30px; text-align :right ; padding-right :10px;">
                      <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderMainEngineKW" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtMainEngineKW">
                       </ajaxToolkit:FilteredTextBoxExtender><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderMainBHP" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtMainBHP">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderMainRPM" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtMainRPM">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAux1EngineKW" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtAux1EngineKW">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAux1BHP" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtAux1BHP">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAux1RPM" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtAux1RPM">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAux2EngineKW" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtAux2EngineKW">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAux2BHP" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtAux2BHP">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAux2RPM" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtAux2RPM">
                       </ajaxToolkit:FilteredTextBoxExtender><ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAux3EngineKW" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtAux3EngineKW">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAux3BHP" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtAux3BHP">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAux3RPM" runat="server"
                           FilterType="Numbers,Custom" ValidChars="0.0" TargetControlID="txtAux3RPM">
                       </ajaxToolkit:FilteredTextBoxExtender>
                       <asp:Button ID="btn_Save_VesselParticular2" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_VesselParticular2_Click" TabIndex="37" />
                       <asp:Button ID="btn_Print_VesselParticular2" runat="server" CausesValidation="False" CssClass="btn" OnClientClick="javascript:ShowPrint();return false;" Text="Print" Width="59px" TabIndex="38"/>
       </td>
      </tr>
     </table>    
    </div>
                          <asp:HiddenField ID="HiddenVesselDetailsOther" runat="server" />
                      