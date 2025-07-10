<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MocRecord.aspx.cs" Inherits="HSSQE_MOC_MocRecord" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%--<%@ Register Src="~/HSSQE/MOC/MocRequest.ascx" TagName="MOCRequest" tagprefix="user" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../js/Common.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <title>EMANAGER</title>
    <style type="text/css">
        .icon-spinner {
            display:block;
        }

       .statusbox {
             width:100%;
            right:0px;
            top:5px;
            float:right;
            
        }
       .statusbox .fa{
             display:none;
        }
        .complete
        {
            
        }
        .complete .fa-check
        {
            display:block;
            color:#128b36;
            font-size:20px;
        }
        .inprogeress
        {
            
        }
        .inprogeress  .fa-spinner{
            display:block;
            color:red;
            font-size:20px;
        }
       .pending
        {
            background-color:#e2e2e2;
        }
       .closureDiv
       {
            display:none;
       }
        .complete .closureDiv {
            display:block;
        }
        .pendingDiv{
            display:none;
        }

        .inprogeress .pendingDiv {
            display:block;
        }
        .answerText
        {
            text-decoration:underline;
        }
    </style>
    <style type="text/css">
        *
        {
            font-family: 'Roboto', sans-serif;
            font-weight:500;
            font-size:13px;
            color:#4a4040;
        }
        a {
            color:#2832eb;
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
        td
        {
            vertical-align:middle;
        }
        .bordered > tbody > tr > td
        {
            padding:5px;
            border:solid 1px #e2e2e2;
        }
        /*.bordered tr:hover td
        {
            padding:5px;
            background-color:#fffedf;
            
        }*/
        .bordered thead tr th
        {
            padding: 5px;
            background-color: #64a1a2;
            border:solid 1px #4e9192;
            color:White;
            font-weight:normal;
        }
        .control
        {
            line-height:25px;
            height:25px;            
        }
         
        
        .stageContainer {
            border:solid 0px red;
            padding-top:5px;
        }
        .section {
            float:left;
            border: solid 1px #a4e4e0;
            margin-left: 5px;
            margin-top: 5px;            
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;  
            min-height: 127px;   
            width:57%;       
        }
        .section-first {
            margin-top:5px;
        }
        .section .sub-section {
            font-size:15px;font-weight:500;color:#324cc8;display:block;padding-left:0px;padding-top:5px;padding-bottom:5px;height:25px;border-bottom:solid 1px #e2e2e2;
            margin-left:8px;
        }
        .section .separator {
            border:solid 1px #e9e8e8;margin-top:10px;margin-bottom:10px;display:none;
        }
        .section .panel_head {
            background-color:#a4e4e0;color:#333;padding:8px;
        }
        .section .panel_body {
            padding:5px;
        }
        .section .mtable {
            border:solid 0px green;width:100%;border-collapse:collapse;margin-top:5px;
              border-spacing: 5px;border-collapse: separate;
        }
        .section .mtable tr td{
            border:solid 0px #82cece;padding:3px;vertical-align:top;
        }
        /*.mtable tr td:first-child {
            font-size:14px;
        }*/
        label {
            font-size:13px;
            font-weight:400;
            color:#ff7006;
            display:block;
            line-height:7px;
        }
    </style>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="Stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,500,700,900" rel="stylesheet">
</head>

    
 <body style="margin:0 0 0 0">
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
            <div style="padding:7px;text-align:center;font-size:17px;" class="text headerband">  
                        MOC Record
                <asp:Label ID="lblMocNumber" runat="server" Text="" style="background-color:#2F4F4F;color:white;padding:7px;text-align:center;font-size:17px;"></asp:Label>
                <asp:HiddenField ID="hfdTopic" runat="server"  />
            </div>  

            <div style='vertical-align: middle; margin: 5px; border: solid 0px #e2e2e2;text-align:right;<%=((StatusID==15)?"display:none":"display:block")%>'>
                    <%--<asp:Button ID="lnlOpenCancelMOC" runat="server" Text="Cancel MOC Request" CssClass="btn" OnClick="lnlOpenCancelMOC_OnClick"  ></asp:Button>--%>
                </div>

            
               <div id="div1MocRequest" runat="server"  class="section section-first" >
                <div class="panel_head">MOC Request</div>
                <div class="panel_body">   
                    <table cellpadding="0" cellspacing="0" class="mtable" >
                    <col width="180px" />
                    <col />
                    <tr>
                        <td><label>Source</label> </td>
                        <td>
                            <asp:Label ID="lblSource" runat="server"></asp:Label>
                            ( <asp:Label ID="lblVesselOffice" runat="server"></asp:Label> )
                        </td>
                        
                    </tr>
                    <tr>
                        <td><label> Impact To</label>  </td>
                        <td>
                            <asp:Label ID="lblImpact" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td> <label> Proposed Timeline</label>  </td>
                        <td>
                            <asp:Label ID="lblTimeLine" runat="server"></asp:Label> <i><b>( for completion of change )</b></i> 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><label> Topic</label><br />
                            <asp:Label ID="lblTopic" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2"><label> Reason for change</label><br />
                            <asp:Label ID="lblReasionForChange" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><label> Brief Description of change </label><br />
                            <asp:Label ID="lblBriefDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                </div>
             </div>
            
        <%---------------------------------------------------------------------------%>
        
            <div id="div2_MOCRequestApproval" runat="server"  visible="false"  class="section section-first" >
                <div class="panel_head">MOC Request Approval</div>
                <div class="panel_body">                              
                    <span class="sub-section"> Approval to Continue  </span>
                    <table cellpadding="0" cellspacing="0" class="mtable" >
                        <col />
                        <col width="100px" />
                        <tr>
                            <td><label> Are the details of change adequately defined?</label> </td>
                            <td>
                                <asp:Label ID="lblDetailsChangeDefined" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><label> Are the reasons of change adequately defined?</label> </td>
                            <td>
                                <asp:Label ID="lblReasonOfChangeDefined" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><label> Is the change necessary?</label> </td>
                            <td>
                                <asp:Label ID="lblIsChangeNecessary" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><label> Is recommendation given to proced with risk assessment?</label> </td>
                            <td>
                                <asp:Label ID="lblIsRecomendationGiven" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><label> Summary of Changes to be completed by</label> </td>
                            <td>
                                <asp:Label ID="lblDateTobeCompleted" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <div class="separator"></div>
                    <span class="sub-section"> Responsible person (Assigned by HOD)</span>
                    <table cellpadding="0" cellspacing="0" class="mtable" border="0" >
                        <col />
                        <col width="220px" />
                        <col width="150px" />
                        <tr>
                            <td>
                                <label> Name  </label> <br /> <asp:Label ID="lblResponsiblePersonaName" runat="server"></asp:Label>
                            </td>
                            <td>
                                <label> Position  </label> <br /> <asp:Label ID="lblResponsiblePosition" runat="server"></asp:Label>
                            </td>
                            <td>
                                <label> Date  </label> <br />  <asp:Label ID="lblApprovalDate" runat="server"></asp:Label>
                            </td>
                            
                        </tr>
                    </table>
                </div>
            </div>
        
        <%---------------------------------------------------------------------------%>
            <div id="div3_MOCDetailUpdate" runat="server"  visible="false" class="section" >                 
                <div class="panel_head">MOC Detail Update / Summary of Changes and Implementation  </div>               
                <div class="panel_body">
                    
                    <table cellpadding="0" cellspacing="0" class="mtable"  >

                        
                        <%--<tr>
                            <td style="width:350px;"><label> Risk Assessment Required </label> </td>
                            <td style="text-align:left;">
                                <asp:Label runat="server" ID="lblRaRecord"  > </asp:Label> 
                            </td>
                        </tr>--%>
                        <tr id="tblRaRecords" runat="server">
                            <td style="width:350px;">
                                <label> Risk Assessment Number</label> 
                            </td>
                            <td style="text-align:left;">
                                <asp:Label runat="server" ID="lblRaNumber"  > </asp:Label> 
                            </td>
                    
                        </tr>
                            <tr id="tblRaRecords1" runat="server">
                                <td>
                                    <label> Risk Assessment File Name</label> 
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblRaFileName"  ></asp:Label> &nbsp; <asp:LinkButton runat="server" ID="btnDownloadAttachment" Text="Download Attachment" OnClick="btnDownloadAttachment_OnClick"  /> 
                                </td>
                            </tr>
                    </table>

                   

                    <table cellpadding="0" cellspacing="0" class="mtable"  >
             
                <tr>
                    <td colspan="2" style="text-align:left;" >
                        <div style="background-color:#e2f1f0;padding:5px;">People</div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; width:50%;border-right:solid 2px #c2c2c2;">                        
                        <label> Communication</label> <br />
                        <asp:Label runat="server" ID="lblCommunication" CssClass="answerText"  ></asp:Label>
                    </td>
                    <td style="text-align:left;">
                        <label> Training</label> <br />
                        <asp:Label runat="server" ID="lblTraining" CssClass="answerText"   ></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:left;">
                        <div style="background-color:#e2f1f0;padding:5px;">Process</div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; width:50%;border-right:solid 2px #c2c2c2;">
                        <label> SMS Review </label> <br />
                        <asp:Label runat="server" ID="lblSMSReview"   CssClass="answerText" ></asp:Label>
                    </td>
                    <td style="text-align:left;">
                        <label> Drawings / Manuals</label>  <br />
                        <asp:Label runat="server" ID="lblDrawing"  CssClass="answerText" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"  style="text-align:left;">                    
                        <label> Documentation</label>  <br />
                        <asp:Label runat="server" ID="lblDocumentation"   CssClass="answerText" ></asp:Label>
                    </td>
                </tr>                
                <tr>
                    <td colspan="2" >
                        <div style="background-color:#e2f1f0;padding:5px;">Equipment</div>
                    </td>
                </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label runat="server" ID="lblEquipment"  CssClass="answerText"  ></asp:Label>
                            </td>
                        </tr>
               <%-- <tr>
                    <td style="text-align:left;">Assesment By</td>                    
                    <td >
                        <asp:Label ID="lblAssessmentBy" runat="server"></asp:Label>
                        <asp:Label ID="lblAssessmentOn" runat="server" Visible="false" ></asp:Label>
                    </td>
                </tr>--%>
                </table>
                
                
                
                
                
                    </div>
            </div>
        
        <%---------------------------------------------------------------------------%>
        <div  id="div4_ApprovalOfMoc" runat="server"  visible="false" class="section" >                 
            <div class="panel_head">Approval of MOC </div>               
            <div class="panel_body">
                <table cellpadding="0" cellspacing="0" class="mtable" >
                    <col />
                    <col width="200px" />
                    <tr>
                        <td><label> Change Type</label> </td>
                        <td>
                            <asp:Label ID="lblChangeType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td><label> Date of risk assessment</label> </td>
                        <td>
                            <asp:Label ID="lblDateOfRiskAssessment" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <label> Result of risk assessment</label> <br />
                            <asp:Label ID="lblResultOfRiskAssessment" runat="server" ></asp:Label>
                        </td>
                        
                        
                    </tr>--%>
                    <tr>
                        <td colspan="2">
                            <label> Impact of change on Safety </label> <br />
                            <asp:Label ID="lblImpactOfChangeOnSafety" runat="server"  CssClass="answerText"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="2">
                            <label> Impact of change on Environment </label>  <br />
                            <asp:Label ID="lblImpactOfChangeOnEnvironment" runat="server" CssClass="answerText"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            <label> List of key mitigating actions? </label> <br />
                            <asp:Label ID="lblKeyMitigatingAction" runat="server" ></asp:Label>
                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td colspan="2">
                            <label> What Manuals/Procedures/Drawings will need an update? </label> <br />
                            <asp:Label ID="lblManualNeedAnUpdated" runat="server" ></asp:Label>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2">
                            <label> Identify person/teams/vessels effected by change </label> <br />
                            <asp:Label ID="lblIdentifyPersonEffectedByChange" runat="server" CssClass="answerText"></asp:Label>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="2"><label> What is estimated cost of change including all mitigating measures?</label><br />
                            <asp:Label ID="lblEstimatedCostOfChange" runat="server" CssClass="answerText"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><label> What is target date for completion?</label> </td>
                        <td>
                            <asp:Label ID="lblTargetDateForCompletion" runat="server"></asp:Label>
                        </td>
                    </tr>                    
                </table>
                </div>
        </div>            
        </div>
        <%---------------------------------------------------------------------------%>
        <div id="div5_ImplementationPahse1" runat="server"  visible="false"  class="section" >  
             <div class="panel_head">Implementation Phase 1 </div>
             <div class="panel_body">
                 <table cellpadding="0" cellspacing="0"  class="mtable" >
                     <col />
                     <col width="200px" style="text-align:right;" />
                     <tr>
                         <td><label> Mitigation measures commenced</label>  </td>
                         <td>
                             <asp:Label ID="lblMitigationMeasure" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Notification of Change completed to all concerned personnel</label> </td>
                         <td>
                             <asp:Label ID="lblNotificationOfChange" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td><label> All appropriate Manuals/Procedures/Drawings updated</label> </td>
                         <td>
                             <asp:Label ID="lblAllAppropriateManualUpdated" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Appropriate training conducted</label> </td>
                         <td>
                             <asp:Label ID="lblAppropriateTrainingConducted" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Details of change communicated to all concerned</label> </td>
                         <td>
                             <asp:Label ID="lblDetailsOfChangeCommunicated" runat="server"  ></asp:Label>
                         </td>
                     </tr>
                 </table>
            </div>
        </div>
        <%---------------------------------------------------------------------------%>
        <div  id="div6_ImplementationPahse2" runat="server"  visible="false"  class="section" >  
             <div class="panel_head">Implementation Phase 2 </div>
             <div class="panel_body">
                 <table cellpadding="0" cellspacing="0"  class="mtable">
                     <col />
                     <col width="200px" style="text-align:right;" />
                     <tr>
                         <td><label> Change process commenced</label> </td>
                         <td>
                             <asp:Label ID="lblChangeProcessComenced" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Change process completed</label> </td>
                         <td>
                             <asp:Label ID="lblChangeprocesscompleted" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Request for extension to target date if any</label> </td>
                         <td>
                             <asp:Label ID="lblRequestForExtedntionToTargetDate" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td><label> Approval to extended target date if applicable</label> </td>
                         <td>
                             <asp:Label ID="lblApprovalToTargetDate" runat="server"></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <label> Additional requirements to target date completed </label> <br />
                             <asp:Label ID="lblAdditionalRequirementToTargetDateCompleted" runat="server" CssClass="answerText"> </asp:Label>
                         </td>
                     </tr>
                 </table>
            </div>
        </div>
        <%---------------------------------------------------------------------------%>
        <div id="div7_EndorsementByHOD" runat="server"  visible="false"   class="section" >  
             <div class="panel_head">Endorsement by HOD/Approver </div>
             <div class="panel_body">
                 
            </div>
        </div>
        <%---------------------------------------------------------------------------%>
         <div  id="div8_ReviewByDPA" runat="server"  visible="false"  class="section" >  
             <div class="panel_head">Review by DPA </div>
             <div class="panel_body">
                 
            </div>
        </div>
        <%---------------------------------------------------------------------------%>
        <div id="div9_EndorsedByMD" runat="server"  visible="false"   class="section" >  
             <div class="panel_head">Endorsed By MD/GM</div>
             <div class="panel_body">
                 
            </div>
        </div>
        
        <%-- Right Div Stage List and progeress -------------------------------------------------------------------------%>
         <div style="position:absolute;top:40px;right:40px; width:35%;">
                <%--<div style='vertical-align: middle; margin: 5px; border: solid 0px #e2e2e2;text-align:right;<%=((StatusID==15)?"display:none":"display:block")%>'>
                    <asp:Button ID="lnlOpenCancelMOC" runat="server" Text="Cancel MOC Request" CssClass="btn" OnClick="lnlOpenCancelMOC_OnClick"></asp:Button>
                </div>--%>
                <asp:Repeater runat="server" ID="rptMOCStages">
                    <ItemTemplate>                        
                        <%--<%#Eval("Stagecss")%>--%>
                    <div class='statusbox <%#Eval("Stagecss")%>'  style="vertical-align: middle; margin: 5px; border: solid 1px #e2e2e2; padding: 10px; -moz-border-radius: 5px; -webkit-border-radius: 5px; border-radius: 5px;">
                       <div style="color:#606060;font-size:14px;"><%#Eval("SNo")%>. <%#Eval("StageName")%> 
                           <span style="float:right;" class="icon-spinner"><i  class="fa fa-spinner fa-2x load-animate"></i> </span>
                           <span style="float:right;"> <i class="fa fa-check" aria-hidden="true"></i> </span>                           
                       </div> 
                        <div style="border-top:solid 1px #e2e2e2;margin-top:5px;"></div>
                        <div style="padding:5px; padding-left:0px;">
                            <div class="closureDiv" >
                                <div style="font-size:11px;color:#6e7100;font-style:italic;margin:5px;text-align:right;"><%#Eval("StageClosedComments")%></div>
                                <div style="color:#2F4F4F; text-align:right; margin:5px;"><%#Eval("ClosedByName")%> ( <%#Eval("StageClosedByPosition")%> )</div>
                                <div style="color:#2F4F4F; text-align:right; margin:5px;"><%#Common.ToDateString(Eval("StageClosedOn"))%></div>
                            </div>
                            <div class="pendingDiv" >
                                <div style="font-size:11px;color:#6e7100;font-style:italic;margin:5px;text-align:right;"><%#Eval("ForwardedComments")%></div>
                               <div style="color:#2F4F4F; text-align:right; margin:5px;"><%#Eval("WaitingByName")%> ( <%#Eval("WaitingPosition")%> )</div>
                               <div style="color:#2F4F4F; text-align:right; margin:5px;">forwarded on <%#Common.ToDateString(Eval("ForwardedOn"))%></div>
                            </div>
                        </div>
                        
                        <div style='text-align:right;border:solid 0px red;<%=((StatusID==15)?"display:none":"display:block")%>'>
                            <asp:Button ID="lnkUPdate" runat="server" Text='<%#((Eval("StageID").ToString()=="15")?"Update":"Approve")%>' CssClass="btn" Visible='<%#(Eval("Stagecss").ToString()=="inprogeress" && Common.CastAsInt32( Eval("StageID"))!=5)%>'  OnClick="lnkUPdate_OnClick"></asp:Button>
                            <asp:Button ID="btnSendBack" runat="server" Text="Revert Stage" CssClass="btn" Visible='<%#(Eval("Stagecss").ToString()=="inprogeress" && Common.CastAsInt32( Eval("StageID"))!=5)%>'  OnClick="btnSendBack_OnClick"></asp:Button>

                            <asp:Button ID="lnlOpenCancelMOC" runat="server" Text="Cancel MOC Request" CssClass="btn" OnClick="lnlOpenCancelMOC_OnClick" Visible='<%#(CurrStageID==10 && Common.CastAsInt32( Eval("StageID"))==10 && Convert.ToString( Eval("StageClosedBy"))=="")%>' ></asp:Button>
                            
                            <asp:Button ID="btnEditMoc" runat="server" Text="Edit" CssClass="btn" OnClick="btnEditMoc_OnClick" Visible='<%#(CurrStageID==5) && Common.CastAsInt32( Eval("StageID"))==5 %>' ></asp:Button>
                            <asp:HiddenField ID="hfdStageID" runat="server" Value='<%#Eval("StageID")%>' />
                            <asp:HiddenField ID="hfdStagename" runat="server" Value='<%#Eval("StageName")%>' />
                        </div>
                    </div>        
                    </ItemTemplate>            
                </asp:Repeater>
                
            </div>        

        <%-- Popup to update stage-------------------------------------------------------------------------%>
        <div ID="dv_UpdateStage" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
            <center>
                <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
                <div style="position:relative;width:950px;padding :0px; text-align :center;background : white; z-index:150;top:5px; border:solid 4px #2a839e; border-top:none;">
                  <center>
                      <div style="padding:10px;font-size:17px; background-color:#2a839e;color:white;font-weight:bold;"><asp:Label ID="lblStagePopupHeading" runat="server"  style="color:white;"></asp:Label> </div>
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
                                    <td style="vertical-align:top;"  >
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td><label> Impact of change on Environment </label> </td>
                                                <td style="vertical-align:top;">   
                                                    <asp:DropDownList ID="ddlImpaceOfChangeOnEnvironment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlImpaceOfChangeOnEnvironment_OnSelectedIndexChanged"  style="margin-left:15px;display:inline-block;">
                                                        <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>    
                                                        <asp:ListItem Value="1" Text="Yes" ></asp:ListItem>
                                                        <asp:ListItem Value="0" Text="No" ></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:TextBox ID="txtImpactOfChangeOnEnvironment" runat="server" TextMode="MultiLine"  Rows="3" Width="99%" style="margin-top:5px;" Visible="false"></asp:TextBox>
                                    </td>
                                        <td style="vertical-align:top;"  >
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td> <label> Impact of change on Safety </label> </td>
                                                    <td style="vertical-align:top;"  >
                                                        <asp:DropDownList ID="ddlImpaceOfChangeOnSafety" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlImpaceOfChangeOnSafety_OnSelectedIndexChanged" style="margin-left:15px;">
                                                            <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>    
                                                            <asp:ListItem Value="1" Text="Yes" ></asp:ListItem>
                                                            <asp:ListItem Value="0" Text="No" ></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:TextBox ID="txtImpactOfChangeOnSafety" runat="server" TextMode="MultiLine"  Rows="3" Width="99%" style="margin-top:5px;"  Visible="false"></asp:TextBox>
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
                                    <label> What is target date for completion?</label> <br />
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
                      <div style="padding:10px;font-size:17px; background-color:#2a839e;color:white;font-weight:bold;"> Cancel MOC Request </div>
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
        
        <%-- Edit Moc Record---------------------------------------------------------------------------------------------------------%>
        <div ID="div_EditNewMOC" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:950px;padding :0px; text-align :center;background : white; z-index:150;top:5px; border:solid 4px #2a839e; border-top:none;">
          <center >
                
                <div>
                  <div style="padding:10px;font-size:17px; background-color:#2a839e;color:#ffffff;font-weight:bold;">Request Details</div>
                  <table cellpadding="5" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;">
                    <tr>
                    <td style="text-align:left; width:48%;">Source :</td>
                    <td style="text-align:left;">&nbsp;</td>
                    <td style="text-align:left; width:48%;">VSL/ Office : 
                    </td>
                    <td style="text-align:left;">&nbsp;</td>
                    </tr>
                    <tr>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlSource" AutoPostBack="true" CssClass="control" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged" runat="server" Width="100px" >
                            <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Vessel" Value="S"></asp:ListItem>
                            <asp:ListItem Text="Office" Value="O"></asp:ListItem>
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="ddlSource" ErrorMessage="*" Display="Dynamic" InitialValue="0" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:left;">
                        &nbsp;</td>
                    <td style="text-align:left;">
                        <asp:DropDownList ID="ddlVessel_Office" runat="server" Width="60%" CssClass="control">
                       </asp:DropDownList>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlVessel_Office" ErrorMessage="*" Display="Dynamic" InitialValue="0" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align:left;">
                        &nbsp;</td>
                    </tr>
                    <tr>
                    <td style="text-align:left;">Impact : 
                     </td>
                    <td style="text-align:left;">&nbsp;</td>
                    <td style="text-align:left;">Topic : 
                     </td>
                    <td style="text-align:left;">&nbsp;</td>
                    </tr>
                    <tr>
                    <td>
                        <asp:CheckBoxList ID="cbImpact" RepeatDirection="Horizontal" runat="server" >
                            <asp:ListItem Text="People" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Process" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Equipment" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Safety" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Environment" Value="5"></asp:ListItem>
                       </asp:CheckBoxList>
                    </td>
                    <td style="text-align:left;">&nbsp;</td>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" ID="txtTopic"  TextMode="MultiLine" Width="98%" Rows="2" ></asp:TextBox>
                        </td>
                    <td style="text-align:left;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTopic" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                    <td style="text-align:left;">Reason for change : 
                     </td>
                    <td style="text-align:left;">&nbsp;</td>
                    <td style="text-align:left;">Brief Description of change : 
                         </td>
                    <td style="text-align:left;">&nbsp;</td>
                     </tr>
                    <tr>
                    <td style="text-align:left;">
                        <asp:TextBox runat="server" ID="txtReasonforChange"  TextMode="MultiLine" Width="98%" Rows="7"></asp:TextBox>
                     </td>
                    <td style="text-align:left;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReasonforChange" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                        </td>
                    <td style="text-align:left;">
                            <asp:TextBox runat="server" ID="txtDescr" TextMode="MultiLine"  Width="98%" Rows="7"></asp:TextBox>
                     </td>
                    <td style="text-align:left;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDescr" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                      </table>
                 <div style="padding:10px;font-size:17px; background-color:#2a839e;color:#fff"><b>Forwared for Approval</b></div>
                       <table cellpadding="5" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;">
                      <tr>
                         <td style="text-align:left; width:48%;">
                             Proposed TimeLine for completion of change : 
                        </td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                          <td style="text-align:left; width:48%;">
                             Comments ( if any )</td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                      </tr>
                      <tr>
                         <td style="text-align:left; width:48%;">
                            <asp:TextBox runat="server" ID="txtPropTL" CssClass="control" MaxLength="15" Width="90px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPropTL" ErrorMessage="*" Display="Dynamic" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                          </td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                          <td style="text-align:left; width:48%;" rowspan="3">
                             <asp:TextBox runat="server" ID="txtForwardedComments" TextMode="MultiLine"  Width="99%" Rows="7"></asp:TextBox></td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                      </tr>
                      <tr>
                         <td style="text-align:left; width:48%;">
                             Forwarded To</td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                         <td style="text-align:left; ">
                             &nbsp;</td>
                      </tr>
                      <tr>
                         <td style="text-align:left; vertical-align:top;">
                            <asp:DropDownList ID="ddlForwardedTo" runat="server" CssClass="control" ></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlForwardedTo" ErrorMessage="*" Display="Dynamic" InitialValue="0" ValidationGroup="V1" ></asp:RequiredFieldValidator>
                             </td>
                          <td></td>
                          <td></td>
                      </tr>
                      
                      </table>
                </div>
          </center>
          <div style="padding:3px; text-align:right; ">
              <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtPropTL" runat="server" Format="dd-MMM-yyyy"></asp:CalendarExtender>
              <asp:Label ID="lblMsgPopup" runat="server" style="color:red;"></asp:Label>
              <asp:Button runat="server" ID="btnSaveNew" Text="Save" ValidationGroup="V1" OnClick="btnSaveNew_Click" CssClass="btn"  />
              <%--<asp:Button runat="server" ID="btnNext" Text="Next >>" CausesValidation="false" OnClick="btnNext_Click" Visible="false" CssClass="btn"/>--%>
              <asp:Button runat="server" ID="btnCloseEditMoc" Text="Close" OnClick="btnCloseEditMoc_Click" CausesValidation="false" CssClass="btn" />
              
          </div>
          </div>
        </center>
        </div>
        <%-----------------------------------------------------------------------------------------------------------%>
    </form>
</body>
    

</html>
