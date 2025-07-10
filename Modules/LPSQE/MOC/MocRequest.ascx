<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MocRequest.ascx.cs" Inherits="HSSQE_MOC_MocRequest" %>

<%--<asp:ScriptManager ID="sm11" runat="server"></asp:ScriptManager>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<div id="divStage" runat="server" style="vertical-align: middle; margin: 5px; border: solid 1px #e2e2e2; padding: 10px; -moz-border-radius: 5px; -webkit-border-radius: 5px; border-radius: 5px;">
    <div style="color:#606060;font-size:14px;">
        <asp:Label ID="lblStageName" runat="server" ></asp:Label>
        <span style="float:right;" class="icon-spinner"><i class="fa fa-spinner fa-2x load-animate"></i></span>
        <span style="float:right;"> <i class="fa fa-check" aria-hidden="true"></i> </span>
                           
    </div> 
    <div style="border-top:solid 1px #e2e2e2;margin-top:5px;"></div>
    <div style="padding:5px; padding-left:0px;">
        <div class="closureDiv" >
            <div style="font-size:11px;color:#6e7100;font-style:italic;margin:5px;text-align:right;">
                <%--<%#Eval("StageClosedComments")%>--%>
                <asp:Label ID="lblStageClosedComments" runat="server" ></asp:Label>
            </div>
            <div style="color:#2F4F4F; text-align:right; margin:5px;">
                <%--<%#Eval("ClosedByName")%> --%>
                <%--<asp:Label ID="lblClosedByName" runat="server"></asp:Label>--%>
                ( <%--<%#Eval("StageClosedByPosition")%>--%> <asp:Label ID="lblStageClosedByPosition" runat="server" ></asp:Label> )

            </div>
            <div style="color:#2F4F4F; text-align:right; margin:5px;">
                <%--<%#Common.ToDateString(Eval("StageClosedOn"))%>--%>
                <asp:Label ID="lblStageClosedOn" runat="server" ></asp:Label>
            </div>
        </div>
        <div class="pendingDiv" >
            <div style="color:#2F4F4F; text-align:right; margin:5px;">
                <%--<%#Eval("WaitingByName")%> ( <%#Eval("WaitingPosition")%> )--%>
                <asp:Label ID="lblWaitingByName" runat="server" ></asp:Label>
            </div>
            <div style="color:#2F4F4F; text-align:right; margin:5px;">
                forwarded on 
                <%--<%#Common.ToDateString(Eval("ForwardedOn"))%>--%>
                <asp:Label ID="lblForwardedOn" runat="server" ></asp:Label>
            </div>
        </div>
    </div>
    <%--<div style='text-align:right;border:solid 0px red;<%=((StatusID==15)?"display:none":"display:block")%>'>--%>
    <div id="divAction" runat="server" style='text-align:right;border:solid 0px red;'>
        <asp:Button ID="lnkUPdate" runat="server" Text="Approve" CssClass="btn" Visible='<%#(Eval("Stagecss").ToString()=="inprogeress")%>' OnClick="lnkUPdate_OnClick" ></asp:Button>
        <asp:Button ID="btnSendBack" runat="server" Text="Send Back" CssClass="btn" Visible='<%#(Eval("Stagecss").ToString()=="inprogeress")%>' OnClick="btnSendBack_OnClick" ></asp:Button>
        
        <%--<asp:HiddenField ID="hfdStageID" runat="server" Value='<%#Eval("StageID")%>' />
        <asp:HiddenField ID="hfdStagename" runat="server" Value='<%#Eval("StageName")%>' />--%>
    </div>


<%-- Popup to update stage-------------------------------------------------------------------------%>
        <div ID="dv_UpdateStage" runat="server" style="position: fixed; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
            <center>
                <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:0; opacity:0.6;filter:alpha(opacity=60)"></div>
                <div style="position:relative;width:950px;padding :0px; text-align :center;background : white; z-index:0;top:5px; border:solid 4px #2a839e; border-top:none;">
                  <center>
                      <div style="padding:10px;font-size:17px; background-color:#2a839e;color:#fff;"><b> <asp:Label ID="lblStagePopupHeading" runat="server"  style="color:white;"></asp:Label> </b></div>
                      <div style="padding:10px;font-size:17px; background-color:#dddfe0;color:#1f1d1d;text-align:left;">
                            <asp:Label ID="Label1" runat="server" Text="Topic : " style="font-size: 16px;color: #3366b3;"></asp:Label>  <b> <asp:Label ID="lblTopicPopup" runat="server"></asp:Label> </b>
                      </div>
                      
                      <div id="divStage2" runat="server" visible="false">
                          <table class="bordered">
                              <col />
                                <col width="40%" />
                        
                        <tr>
                            <td><label> Are the details of change adequately defined?</label> </td>
                            <td>
                                <asp:DropDownList ID="ddlDetailsChangeDefined" runat="server" >
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><label> Are the reasons of change adequately defined?</label> </td>
                            <td>
                                <asp:DropDownList ID="ddlReasonOfChangeDefined" runat="server" >
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><label> Is the change necessary?</label> </td>
                            <td>
                                <asp:DropDownList ID="ddlIsChangeNecessary" runat="server" >
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><label> Is recommendation given to proced with risk assessment?</label> </td>
                            <td>
                                <asp:DropDownList ID="ddlIsRecomendationGiven" runat="server" >
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><label> Summary of Changes to be completed by</label> </td>
                            <td>
                                
                                <asp:TextBox runat="server" ID="txtDateTobeCompleted" CssClass="control" MaxLength="15" Width="90px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDateTobeCompleted" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Responsible person
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlResponsiblePerson" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                          <tr>
                            <td colspan="2"><label> Remarks (if any)</label> <br />
                                <asp:TextBox ID="txtForwardedByComments" runat="server" TextMode="MultiLine" Rows="4" Width="99%"></asp:TextBox>
                                
                            </td>
                        </tr>
                    </table>
                          <div style="padding:3px; text-align:right; ">
                              <asp:Label ID="lblMsgMOCRequestApproval" runat="server" style="color:red;"></asp:Label>
                              <asp:Button runat="server" ID="btnSaveStageRequestApproval" Text="Save" OnClick="btnSaveStageRequestApproval_Click" CssClass="btn"/>                      
                              <asp:Button runat="server" ID="btnCloseStageRequestApproval" Text="Close" OnClick="btnCloseStageRequestApproval_Click" CssClass="btn" />
                          </div>
                          <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDateTobeCompleted" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                      </div>
                      <div id="divStage3" runat="server" visible="false">
                          
                           <table class="bordered" style="text-align:left;">
                            <col width="25%" />
                            <col />
                            <%--<tr>
                                <td>
                                    Risk assessment required
                                </td> 
                                <td>
                                    <asp:DropDownList ID="ddlRiskAssessmentRequired" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRiskAssessmentRequired_OnSelectedIndexChanged"> 
                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </td> 
                            </tr>--%>
                            <tr id="trRiskAssessmentNo" runat="server">
                                <td>Risk assessment number</td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0" style="display:inline-block;text-align:left;">
                                        <col width="250px;" />
                                        <col width="50px;" />
                                        <col width="300px;" />
                                        <col />
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtRiskAssessmentNumber" runat="server"></asp:TextBox>
                                            </td>
                                            <td>File  :</td>
                                            <td>
                                                <asp:FileUpload ID="fuRiskAssessmentFile" runat="server" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkUploadFile" runat="server" Text="Upload" OnClick="lnkUploadFile_OnClick"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                               </table>
                               <table class="bordered">
                            <col width="50%" />
                            <col />
                               <tr>
                                   <td colspan="2" style="background-color:#82cece;text-align:center;font-weight: bold;">
                                       People
                                   </td>
                               </tr>
                               <tr>
                                <td>
                                    Communication<br />
                                    <asp:TextBox ID="txtCommunication" runat="server" TextMode="MultiLine" Width="99%" Rows="3"></asp:TextBox>
                                </td>
                                <td>
                                    Training <br />
                                    <asp:TextBox ID="txtTraining" runat="server" TextMode="MultiLine" Width="99%" Rows="3"></asp:TextBox>
                                </td>
                            </tr>  
                                   </table>
                               <table class="bordered">  
                                    <col width="50%" />
                            <col />                           
                                    <tr>
                                   <td colspan="2" style="background-color:#82cece;text-align:center;font-weight: bold;">
                                       Process
                                   </td>
                               </tr>
                               <tr>
                                <td >
                                    SMS Review <br />
                                    <asp:TextBox ID="txtSMSReview" runat="server" TextMode="MultiLine" Width="99%" Rows="3"></asp:TextBox>
                                </td>
                                   <td>
                                       Drawings / Manuals <br />
                                       <asp:TextBox ID="txtDrawingsManuals" runat="server" TextMode="MultiLine" Width="99%" Rows="3"></asp:TextBox>
                                   </td>
                            </tr>
                                   <tr>
                                <td colspan="2">
                                    Documentation  <br />
                                    <asp:TextBox ID="txtDocumentation" runat="server" TextMode="MultiLine" Width="99%" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                                </table>

                               <table class="bordered">  
                                    <col width="25%" />
                                    <col />               
                               <tr>
                                   <td colspan="2" style="background-color:#82cece;text-align:center;font-weight: bold;">
                                       Equipment 
                                   </td>
                               <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtEquipment" runat="server" TextMode="MultiLine" Width="99%" Rows="3"></asp:TextBox>
                                </td>
                            </tr>                               
                            </table>

                          <div style="padding:3px; text-align:right; ">
                              <asp:Label ID="lblMsgDetailUpdate" runat="server" style="color:red;"></asp:Label>
                              <asp:Button runat="server" ID="btnSaveRiskAssessment" Text="Save" OnClick="btnSaveRiskAssessment_Click" CssClass="btn"/>
                              <asp:Button runat="server" ID="btnClosePopupRiskAssessmentRequired" Text="Close" OnClick="btnClosePopup_Click" CssClass="btn"/>
                          </div>
                      </div>
                      <div id="divStage4" runat="server" visible="false">
                          <table class="bordered">
                              <col width="50%" />
                              <col  />
                              <tr>
                                  <td colspan="2">
                                      Change Type 
                                      <asp:DropDownList ID="ddlChangeType" runat="server" style="margin-left:15px;">
                                          <asp:ListItem Value="" Text=""></asp:ListItem>
                                          <asp:ListItem Value="P" Text="Permanent"></asp:ListItem>
                                          <asp:ListItem Value="T" Text="Temporary"></asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <%--<tr>
                                    <td  style="vertical-align:top;">
                                        <label> Date of risk assessment</label>  <br />
                                    
                                        <asp:TextBox runat="server" ID="txtDateOfRiskAssessment" CssClass="control" MaxLength="15" Width="90px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDateOfRiskAssessment" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                    </td>
                                    <td >
                                        <label> Result of risk assessment</label><br />
                                        <asp:TextBox ID="txtResultOfRiskAssessment" runat="server" TextMode="MultiLine"  Rows="3" Width="99%" ></asp:TextBox>
                                    </td>
                                </tr>--%>
                              <tr>
                                    <td >
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td><label> Impact of change on Environment </label> </td>
                                                <td>   
                                                    <asp:DropDownList ID="ddlImpaceOfChangeOnEnvironment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlImpaceOfChangeOnEnvironment_OnSelectedIndexChanged"  style="margin-left:15px;display:inline-block;">
                                                            <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="No" ></asp:ListItem>
                                                        </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:TextBox ID="txtImpactOfChangeOnEnvironment" runat="server" TextMode="MultiLine"  Rows="3" Width="99%" style="margin-top:5px;"></asp:TextBox>
                                    </td>
                                        <td >
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td> <label> Impact of change on Safety </label> </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlImpaceOfChangeOnSafety" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlImpaceOfChangeOnSafety_OnSelectedIndexChanged" style="margin-left:15px;">
                                                            <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="No" ></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:TextBox ID="txtImpactOfChangeOnSafety" runat="server" TextMode="MultiLine"  Rows="3" Width="99%" style="margin-top:5px;"></asp:TextBox>
                                </td>
                                </tr>
                              <%--<tr>
                                    <td >
                                        <label> What Manuals/Procedures/Drawings will need an update? </label><br />
                                            <asp:TextBox ID="txtManualNeedAnUpdated" runat="server" TextMode="MultiLine"  Rows="3" Width="99%"></asp:TextBox>
                                    </td>
                                    <td >
                                        <label> List of key mitigating actions? </label><br />
                                            <asp:TextBox ID="txtKeyMitigatingAction" runat="server" TextMode="MultiLine"  Rows="3" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>--%>
                              <tr>
                                    <td >
                                        <label> What is estimated cost of change including all mitigating measures?</label> <br />
                                        <asp:TextBox ID="txtEstimatedCostOfChange" runat="server" TextMode="MultiLine"  Rows="3" Width="99%" ></asp:TextBox>
                                    </td>
                                        <td>
                                        <label> Identify person/teams/vessels effected by change </label><br />
                                            <asp:TextBox ID="txtIdentifyPersonEffectedByChange" runat="server" TextMode="MultiLine"  Rows="3" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
                             <tr>
                                <td >
                                    <label> Remarks</label> <br />
                                    <asp:TextBox runat="server" ID="txtApprovalCommentsDetailsUpdate" TextMode="MultiLine"  Rows="3" Width="99%"></asp:TextBox>                                        
                                </td>
                                <td  style="vertical-align:top;">
                                    <label> What is terget date for completion?</label> <br />
                                    <asp:TextBox runat="server" ID="txtTargetDateForCompletion" CssClass="control" MaxLength="15" Width="80px"></asp:TextBox>
                                </td>
                            </tr>  
                              
                          </table>
                          
                          <div style="padding:3px; text-align:right; ">
                              <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtTargetDateForCompletion" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                              <asp:Label ID="lblMsgStageApprovalOfMOC" runat="server" style="color:red;"></asp:Label>
                              <asp:Button runat="server" ID="btnSaveStageDetailsUpdate" Text="Save" OnClick="btnSaveStageDetailsUpdate_Click" CssClass="btn"/>                      
                              <asp:Button runat="server" ID="btnClosePopDetailsUpdate" Text="Close" OnClick="btnClosePopup_Click" CssClass="btn" />
                          </div>
                      </div>

                      <div id="divStage5_Pahse1" runat="server" visible="false">
                          <table class="bordered">
                              <col width="75%" />
                              <col  />
                              <tr>
                         <td><label> Mitigation measures commenced</label>  </td>
                         <td>
                             <asp:TextBox ID="txtMitigationMeasure" runat="server" Width="80px"></asp:TextBox>                             
                         </td>
                     </tr>
                     <tr>
                         <td><label> Notification of Change completed to all concerned personnel</label> </td>
                         <td>
                             <asp:TextBox ID="txtNotificationOfChange" runat="server" Width="80px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td><label> All appropriate Manuals/Procedures/Drawings updated</label> </td>
                         <td>
                             <asp:TextBox ID="txtAllAppropriateManualUpdated" runat="server" Width="80px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Appropriate training conducted</label> </td>
                         <td>
                             <asp:TextBox ID="txtAppropriateTrainingConducted" runat="server" Width="80px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Details of change communicated to all concerned</label> </td>
                         <td>
                             <asp:TextBox ID="txtDetailsOfChangeCommunicated" runat="server" Width="80px"></asp:TextBox>
                         </td>
                     </tr>
                              <tr>
                                  <td colspan="2"> <label>Remarks</label> <br />
                                      <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="99%" Rows="3"></asp:TextBox>
                                  </td>
                              </tr>
                          </table>

                          <div style="padding:3px; text-align:right; ">
                              <asp:CalendarExtender ID="CE_Phase1_1" TargetControlID="txtMitigationMeasure" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                              <asp:CalendarExtender ID="CE_Phase1_2" TargetControlID="txtNotificationOfChange" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                              <asp:CalendarExtender ID="CE_Phase1_3" TargetControlID="txtAllAppropriateManualUpdated" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                              <asp:CalendarExtender ID="CE_Phase1_4" TargetControlID="txtAppropriateTrainingConducted" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                              <asp:CalendarExtender ID="CE_Phase1_5" TargetControlID="txtDetailsOfChangeCommunicated" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>

                              <asp:Label ID="lblMsgPhase1" runat="server" style="color:red;"></asp:Label>
                              <asp:Button runat="server" ID="btnSavePhase1" Text="Save" OnClick="btnSavePhase1_Click" CssClass="btn"/>
                              <asp:Button runat="server" ID="btnClosePopupPhase1" Text="Close" OnClick="btnClosePopup_Click" CssClass="btn" />
                          </div>
                      </div>

                      <div id="divStage6_Pahse2" runat="server" visible="false">
                          <table class="bordered">
                              <col width="75%" />
                              <col />
                              <tr>
                         <td><label> Change process commenced</label> </td>
                         <td>
                             <asp:TextBox ID="txtChangeProcessComenced" runat="server" Width="80px" ></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Change process completed</label> </td>
                         <td>
                             <asp:TextBox ID="txtChangeprocesscompleted" runat="server" Width="80px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Request for extension to target date if any</label> </td>
                         <td>
                             <asp:TextBox ID="txtRequestForExtedntionToTargetDate" runat="server" Width="80px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Approval to extended target date if applicable</label> </td>
                         <td>
                             <asp:TextBox ID="txtApprovalToTargetDate" runat="server" Width="80px"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <label> Additional requirements to target date completed </label> <br />
                             <asp:TextBox ID="txtAdditionalRequirementToTargetDateCompleted" runat="server" TextMode="MultiLine" Rows="3" Width="99%"> </asp:TextBox>
                         </td>
                     </tr>
                          </table>
                          <div style="padding:3px; text-align:right; ">
                              <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtChangeProcessComenced" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                              <asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txtChangeprocesscompleted" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                              <asp:CalendarExtender ID="CalendarExtender6" TargetControlID="txtRequestForExtedntionToTargetDate" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                              <asp:CalendarExtender ID="CalendarExtender7" TargetControlID="txtApprovalToTargetDate" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>

                              <asp:Label ID="lblMsgPhase2" runat="server" style="color:red;"></asp:Label>
                              <asp:Button runat="server" ID="btnSavePhase2" Text="Save" OnClick="btnSavePhase2_Click" CssClass="btn"/>
                              <asp:Button runat="server" ID="Button2" Text="Close" OnClick="btnClosePopup_Click" CssClass="btn" />
                          </div>
                      </div>

                      <div id="divStage7_Endorsement" runat="server" visible="false">
                          <table class="bordered">
                        <col width="25%" />
                        <col />
                            <tr>
                                <td colspan="2">
                                    Remarks
                                    <asp:TextBox ID="txtCommentsEndorsment" runat="server" TextMode="MultiLine" Rows="10" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                          <div style="padding:3px; text-align:right; ">
                              <asp:Label ID="lblMsgEndorsment" runat="server" style="color:red;"></asp:Label>
                              <asp:Button runat="server" ID="btnSaveStage7_Endorsement" Text="Save" OnClick="btnSaveStage7_Endorsement_Click" CssClass="btn"/>
                              <asp:Button runat="server" ID="Button3" Text="Close" OnClick="btnClosePopup_Click" CssClass="btn" />
                          </div>
                      </div>
                      <div id="divStage8_Review"  runat="server" visible="false">
                          <table class="bordered">
                        <col width="25%" />
                        <col />
                            <tr>
                                <td colspan="2">
                                    Remarks
                                    <asp:TextBox ID="txtCommentsReview" runat="server" TextMode="MultiLine" Rows="10" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                          <div style="padding:3px; text-align:right; ">
                      <asp:Label ID="lblMsgReview" runat="server" style="color:red;"></asp:Label>
                      <asp:Button runat="server" ID="btnSaveReviewComments" Text="Save" OnClick="btnSaveReviewComments_Click" CssClass="btn"/>
                      <asp:Button runat="server" ID="Button4" Text="Close" OnClick="btnClosePopup_Click" CssClass="btn" />
                  </div>
                      </div>

                      <div id="divSendBack"  runat="server" visible="false">
                          <table class="bordered">
                        <col width="25%" />
                        <col />
                            <tr>
                                <td colspan="2">
                                    Remarks
                                    <asp:TextBox ID="txtCommentsSendBack" runat="server" TextMode="MultiLine" Rows="10" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                          <div style="padding:3px; text-align:right; ">
                      <asp:Label ID="lblMsgSedBack" runat="server" style="color:red;"></asp:Label>
                      <asp:Button runat="server" ID="BtnSaveSendBack" Text="Save" OnClick="BtnSaveSendBack_Click" CssClass="btn"/>
                      <asp:Button runat="server" ID="Button5" Text="Close" OnClick="btnClosePopup_Click" CssClass="btn" />
                  </div>
                      </div>
                  </center>
                    
                </div>
            </center>
           </div>

        <%-- Close Moc Request------------------------------------------------------------------------------------%>
         <div ID="divClosurePopup" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
            <center>
                <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                <div style="position:relative;width:950px;padding :0px; text-align :center;background : white; z-index:150;top:5px; border:solid 4px #2a839e; border-top:none;">
                  <center>
                      <div style="padding:10px;font-size:17px; background-color:#2a839e;color:#fff"><b> Cancel MOC Request </b></div>
                        <table class="bordered">
                        <col width="25%" />
                        <col />
                            <tr>
                                <td colspan="2">
                                    Remarks
                                    <asp:TextBox ID="txtClosureComments" runat="server" TextMode="MultiLine" Rows="10" Width="99%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </center>
                     <div style="padding:3px; text-align:right; ">
                      <asp:Label ID="lblMsgCancelRequest" runat="server" style="color:red;"></asp:Label>
                      <asp:Button runat="server" ID="btnCancelRequest" Text="Save" OnClick="btnCancelRequest_Click" CssClass="btn"/>
                      <asp:Button runat="server" ID="btnCloseCancelPopup" Text="Close" OnClick="btnCloseCancelPopup_Click" CssClass="btn" />
                  </div>
                </div>
            </center>
           </div>
        <%-----------------------------------------------------------------------------------------------------------%>

    </div>    