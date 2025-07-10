<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MocList.aspx.cs" Inherits="HSSQE_MOC_MocList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../js/Common.js" type="text/javascript"></script>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="Stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500,700,900" rel="stylesheet">
    <title>EMANAGER</title>
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <style type="text/css">
        *
        {
            font-family: 'Roboto', sans-serif;
            font-weight:400;
            font-size:12px;
            
        }
        ul,li
        {
            margin:0px;
            padding:0px;
        }
       /* .btn
        {
            padding:0px;
            height:30px;
            background-color:#333;
            color:White;
            border:solid 0px white;
            padding-left:10px;
            padding-right:10px;
        }*/
        .counterlist li
        {
            color: white;
            border: solid 0px red;
            display:inline;
            margin:0px;
            padding:0px;
        }
        .counterlist li a
        {
            display: inline-block;
            text-decoration:none;                
            color:White;  
            margin:0px;                                        
            padding:10px;
            
        }
        .counterlist .cnt
        {
            font-size:26px;
            display:block;                
        }
        .counterlist .name
        {
            font-size:12px;                
            color:#fff;
            font-style:italic;
        }
        .color1 {background-color:#5F9EA0;}        
        .color2 {background-color:#FF7F50;}
        .color3 {background-color:#6495ED;}
        .color4 {background-color:#8A2BE2;}
        .color5 {background-color:#f32285;}
        .color6 {background-color:#B8860B;}
        .color7 {background-color:#006400;}
        .color8 {background-color:#FF8C00;}
        .color9 {background-color:#8FBC8F;}
        .color10 {background-color:#2F4F4F;}
        .color11 {background-color:#1E90FF;}
        .color12 {background-color:#CD5C5C;}
        .color13 {background-color:#FF4500;}
        .color14 {background-color:#8B4513;}
        
        .bordered
        {
            border-collapse:collapse;
            width:100%;
        }
        /*td
        {
            vertical-align:middle;
        }*/
        .bordered tr td
        {
            padding:5px;
            border:solid 1px #e2e2e2;
        }
      /*  .bordered thead tr th
        {
            padding: 5px;
            background-color: #64a1a2;
            border:solid 1px #4e9192;
            color:White;
            font-weight:normal;
        }*/
        .control,select
        {
            line-height:20px;
            height:20px;            
        }
    </style>
</head>

    
 <body style="margin:0 0 0 0">
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
    <table width='100%' cellpadding="0" cellspacing="0" >
    <tr>
        <td>
             <div style="padding:5px;">  
                     <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                     <tr>
                         <td style="text-align:center;" class="text headerband">                        
                             <asp:Label runat="server" ID="lblStageName"></asp:Label>
                             </td>
                     </tr>
                     </table>
                </div>
           
        </td>
    </tr>
    
    <tr>
        <td style="vertical-align:top">
                <table class="bordered">
                         <colgroup>
                            <col style="text-align: left" width="25px" />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="150px" />
                            <col style="text-align: left" width="200px" /> 
                            <col style="text-align: left" /> 
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="150px" />  
			    <col style="text-align: left" width="150px" />                           
                            <%--<col width="100px" />--%>
                        </colgroup>
                        <thead>
                        <tr class= "headerstylegrid">
                            <th>&nbsp;</th>
                            <th style="text-align:left;">&nbsp;Source</th>
                            <th style="text-align:left;">&nbsp;Location</th>                            
                            <th>&nbsp;MOC#</th>
                            <th style="text-align:left;">&nbsp;Topic</th>
                            <th>&nbsp;Request Date</th>
                            
			    <th>&nbsp;User </th>   
			    <th>&nbsp;Stage </th>                         
                            <%--<th>&nbsp;</th>--%>
                        </tr>
                        </thead>
                    </table>
                    <table cellspacing="0" border="0" cellpadding="0"  class="bordered">
                        <colgroup>
                            <col style="text-align: left" width="25px" />
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="150px" />
                            <col style="text-align: left" width="200px" /> 
                            <col style="text-align: left" /> 
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left" width="150px" />  
			    <col style="text-align: left" width="150px" />                                                    
                            <%--<col width="100px" />--%>
                        </colgroup>
                        <asp:Repeater ID="rptMOC" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <asp:ImageButton ID="btnView" OnClick="btnView_Click" ImageUrl="~/Modules/HRD/Images/search_magnifier_12.png" ToolTip="View" CommandArgument='<%#Eval("MocID")%>' runat="server" />   
                                        <asp:HiddenField ID="hfdMocID" runat="server" Value='<%#Eval("MocID")%>' />
                                        <asp:HiddenField ID="hfdStageID" runat="server" Value='<%#Eval("StageID")%>' />
                                    </td>
                                    <td><%#Eval("Source")%></td>
                                    <td><%#Eval("Location")%></td>
                                    <td><%#Eval("MOCNumber")%></td>
                                    <td>
                                        <asp:Label ID="lblTopic" runat="server" Text='<%#Eval("Topic")%>'></asp:Label>
                                    </td>
                                    <td><%# Common.ToDateString(Eval("RequestDate"))%></td>
                                    
                                    <%--<td>
                                        <asp:LinkButton ID="btnOpenPopupUpdateStage" runat="server" Text="Update" OnClick="btnOpenPopupUpdateStage_OnClick" ></asp:LinkButton> &nbsp;
                                        <asp:LinkButton ID="btnOpenPopupCancelStage" runat="server" Text="Cancel" OnClick="btnOpenPopupCancelStage_Click"></asp:LinkButton>
                                    </td>--%>
				    <td><%#Eval("Waitinguser")%></td>
				    <td><%#Eval("STAGENAME")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
       
    </td>
    </tr>
    </table>
    </div>

           <div ID="dv_UpdateStage" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
            <center>
                <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                <div style="position:relative;width:950px;padding :0px; text-align :center;background : white; z-index:150;top:5px; border:solid 4px #2a839e; border-top:none;">
                  <center>
                      <div style="padding:10px;font-size:17px; background-color:#2a839e;color:#fff"><b> <asp:Label ID="lblStagePopupHeading" runat="server"></asp:Label> </b></div>
                      <div style="padding:10px;font-size:17px; background-color:#dddfe0;color:#1f1d1d"><b> <asp:Label ID="lblTopic" runat="server"></asp:Label> </b></div>
                      
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
                          
                           <table class="bordered">
                            <col width="25%" />
                            <col />
                            <tr>
                                <td>
                                    Risk assessment required
                                </td> 
                                <td>
                                    <asp:DropDownList ID="ddlRiskAssessmentRequired" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRiskAssessmentRequired_OnSelectedIndexChanged"> 
                                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </td> 
                            </tr>
                            <tr>
                                <td>Risk assessment number</td>
                                <td>
                                    <asp:TextBox ID="txtRiskAssessmentNumber" runat="server"></asp:TextBox>
                                    <div style="margin-left:50px;display:inline-block;">
                                         File  :  <asp:FileUpload ID="fuRiskAssessmentFile" runat="server" />
                                    </div>
                                    <div style="margin-left:50px;display:inline-block;">
                                        <asp:LinkButton ID="lnkUploadFile" runat="server" Text="Upload" OnClick="lnkUploadFile_OnClick"></asp:LinkButton>
                                    </div>
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
                              <tr>
                                    <td  style="vertical-align:top;">
                                        <label> Date of risk assessment</label>  <br />
                                    
                                        <asp:TextBox runat="server" ID="txtDateOfRiskAssessment" CssClass="control" MaxLength="15" Width="90px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDateOfRiskAssessment" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                    </td>
                                    <td >
                                        <label> Result of risk assessment</label><br />
                                        <asp:TextBox ID="txtResultOfRiskAssessment" runat="server" TextMode="MultiLine"  Rows="3" Width="99%" ></asp:TextBox>
                                    </td>
                                </tr>
                              <tr>
                                    <td >
                                        <label> Impact of change on Environment </label> 
                                        <asp:DropDownList ID="ddlImpaceOfChangeOnEnvironment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlImpaceOfChangeOnEnvironment_OnSelectedIndexChanged"  style="margin-left:15px;">
                                            <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="No" ></asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:TextBox ID="txtImpactOfChangeOnEnvironment" runat="server" TextMode="MultiLine"  Rows="3" Width="99%" style="margin-top:5px;"></asp:TextBox>
                                    </td>
                                        <td >
                                    <label> Impact of change on Safety </label>
                                                
                                    <asp:DropDownList ID="ddlImpaceOfChangeOnSafety" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlImpaceOfChangeOnSafety_OnSelectedIndexChanged" style="margin-left:15px;">
                                            <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="No" ></asp:ListItem>
                                        </asp:DropDownList>
                                    <br />
                                        <asp:TextBox ID="txtImpactOfChangeOnSafety" runat="server" TextMode="MultiLine"  Rows="3" Width="99%" style="margin-top:5px;"></asp:TextBox>
                                </td>
                                </tr>
                              <tr>
                                    <td >
                                        <label> What Manuals/Procedures/Drawings will need an update? </label><br />
                                            <asp:TextBox ID="txtManualNeedAnUpdated" runat="server" TextMode="MultiLine"  Rows="3" Width="99%"></asp:TextBox>
                                    </td>
                                    <td >
                                        <label> List of key mitigating actions? </label><br />
                                            <asp:TextBox ID="txtKeyMitigatingAction" runat="server" TextMode="MultiLine"  Rows="3" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
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
                                    <label> Comments</label> <br />
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
                                  <td colspan="2"> <label>Comments</label> 
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
                                    Comments
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
                                    Comments
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
                  </center>
                    
                </div>
            </center>
           </div>
        <%--------------------------------------------------------------------------------------%>
         <div ID="divClosurePopup" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
            <center>
                <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                <div style="position:relative;width:950px;padding :0px; text-align :center;background : white; z-index:150;top:5px; border:solid 4px #2a839e; border-top:none;">
                  <center>
                      <div style="padding:10px;font-size:17px; " class="text headerband"><b> Cancel MOC Request </b></div>
                        <table class="bordered">
                        <col width="25%" />
                        <col />
                            <tr>
                                <td colspan="2">
                                    Comments
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
    </form>
</body>
    

</html>

