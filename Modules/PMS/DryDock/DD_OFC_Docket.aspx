<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_OFC_Docket.aspx.cs" Inherits="DD_OFC_Docket" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link href="../CSS/style.css" rel="stylesheet" type="text/css" />
<link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
<link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
<script src="../JS/jquery_v1.10.2.min.js" type="text/javascript"></script>
<script src="../JS/KPIScript.js" type="text/javascript"></script>
    <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/style.css" rel="Stylesheet" type="text/css" />
<title>eMANAGER</title> 
</head> 
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
       
       
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>     
        
        <td style=" text-align :left; vertical-align : top;"> 
            <table style="width :100%" cellpadding="0" cellspacing="0" border="0" height="465px">
            <tr>  
            <td>
             <div style="border:none; padding:3px; font-weight:bold; ">
             <asp:UpdatePanel runat="server" id="up11">
             <ContentTemplate>
             <div class="box1">
             <table style="width :100%; vertical-align:middle;" cellpadding="0" cellspacing="1" border="0" >
                <tr>  
                 <td style="width:50px;vertical-align:middle;">Year :&nbsp;</td>
                 <td style="text-align:left;vertical-align:middle;"><asp:DropDownList ID="ddlYear" Width="70px" runat="server" ></asp:DropDownList></td>
                 <td style="width:50px;vertical-align:middle;">Fleet :&nbsp;</td>
                 <td style="text-align:left;vertical-align:middle;"><asp:DropDownList runat="server" ID="ddlFleet_Search" OnSelectedIndexChanged="ddlFleet_Search_OnSelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList></td>
                 <td style="width:70px;vertical-align:middle;text-align:right;">Vessel :&nbsp;</td>
                 <td style="text-align:left;vertical-align:middle;"><asp:DropDownList runat="server" ID="ddlVessel_Search" ></asp:DropDownList></td>
                 <td><asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" CausesValidation="false" /></td>
             </tr>
             </table>
             </div>
             </ContentTemplate>
             <Triggers>
             <asp:PostBackTrigger ControlID="btnSearch" />
             </Triggers>
             </asp:UpdatePanel>
             <div style="padding-top:2px; ">
                <table style="width :100%" cellpadding="0" cellspacing="0" border="0" >
                <tr>
                <td style="text-align:left;">
                  <asp:Button ID="dvbtnAddDocket" runat="server" style=" padding:3px; border:none; color:White; background-color:Red; " Text="DryDock Planning" OnClientClick="return CallFunction('PLAN');"/>
                  <asp:Button ID="dvbtnUpdateSpecification" runat="server" Visible="false" class="cssbtn" Text="Doc Specification" OnClientClick="return CallFunction('US');"/>
                  <asp:Button ID="dvbtnYardConfirmation" runat="server" Visible="false" class="cssbtn" Text="Yard Confirmation" OnClientClick="return CallFunction('YC');" />
                  <asp:Button ID="dvbtnJobTracking" runat="server" Visible="false" class="cssbtn" Text="Job Tracking" OnClientClick="return CallFunction('JT');" />
                  <asp:Button ID="dvbtnCostTracking" runat="server" Visible="false" class="cssbtn" Text="Cost Tracking" OnClientClick="return CallFunction('CT');" />
                  <asp:Button ID="dvbtnDocuments" runat="server" Visible="false" class="cssbtn" Text="Documents" OnClientClick="return CallFunction('DC');" />
                  <asp:Button ID="dvbtnReports" runat="server" Visible="false" class="cssbtn" Text="Report" OnClientClick="return CallFunction('DR');"/>
                </td>
                <td style="text-align:right;">
                    <asp:Button ID="dvbtnExecute" runat="server" Visible="false" class="cssbtn1" Text="Execute"  OnClientClick="return CallFunction('EX');" />
                    <asp:Button ID="dvbtnDocketMail" runat="server" Visible="false" class="cssbtn1" Text="Daily Report" OnClientClick="return CallFunction('DM');"/>
                    <asp:Button ID="dvbtnClosure" runat="server" Visible="false" class="cssbtn1" Text="Closure" OnClientClick="return CallFunction('CD');"/>
                </td>
                </tr>
                </table>
                <div style="display:none;" >
                    <asp:Button ID="btnAction" runat="server" OnClick="btnAction_Click" ToolTip=""  Text="Afdsa"/>
                    <asp:TextBox runat="server" ID="txtAction"></asp:TextBox>
                </div>
             </div>
             </div>
                        <div class="dvScrollheader">  
                           <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse; height:25px;" bordercolor="white">
                           
                                 <colgroup>
                                    <col style="width:4%;" />
                                    <col style="width:26%;"/>
                                    <col style="width:12%;" />
                                    <col style="width:20%;" />
                                    
                                    <%--<col style="width:200px;" />--%>                                    
                                    <col style="width:20%;" />
                                    <col style="width:8%;" />
                                    <col style="width:8%;" />
                                    <%--<col style="width:100px;" />--%>
                                    <col style="width:2%;" />
                                </colgroup>

                                 <tr class= "headerstylegrid">
                                 
                                    <td style="text-align:center;width:4%;"></td>
                                    <td style="text-align:left;width:26%;"><b>Vessel Name </b></td>
                                    <td style="text-align:left;width:12%;"><b>DD# </b></td>
                                    <td style="text-align:left;width:20%;"><b>DD Type </b></td>
                                    
                                    <%--<td style="text-align:center;"><b>Plan Duration </b></td>--%>                                    
                                    <td style="text-align:center;width:20%;"><b>DD Period </b></td>                                    
                                    <td style="text-align:center;width:8%;"><b>Act. Days</b></td>
                                    <td style="text-align:left;width:8%;"><b>Status</b></td>
                                    <%--<td style="text-align:center;"><b>Action</b></td>--%>                                    
                                    <td style="width:2%;">&nbsp;</td>
                                </tr>
                            </table>
                            </div>
                            <%--, dvScrolldata 368 --%>
             <div  class="ScrollAutoReset dvScrolldata" style="height:368px;text-align:center;" id="fasd125">
                           <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:4%;" />
                                     <col  style="width:26%;"/>
                                    <col style="width:12%;" />
                                    <col style="width:20%;" />
                                   
                                    <%--<col style="width:200px;" />--%>                                    
                                    <col style="width:20%;" />
                                    <col style="width:8%;" />
                                    <col style="width:8%;" />
                                    <%--<col style="width:100px;" />--%>
                                    <col style="width:2%;" />
                                </colgroup>
                                <asp:Repeater ID="rptDocket" runat="server">
                                    <ItemTemplate>
                                            <tr style='<%# (Common.CastAsInt32(Eval("DocketId"))== DocketId)? "background-color:#FFF0B2;" : "" %> '>
                                                <td style="text-align:center;width:4%;"><%--<asp:ImageButton runat="server" ToolTip="Select" ID="btnSelectDocket" OnClick="btnSelectDocket_Click" CommandArgument='<%#Eval("DocketId")%>' ImageUrl="~/Modules/PMS/Images/search_magnifier_12.png" style="float:right"  />--%> <asp:RadioButton ID="rdoSelect" OnCheckedChanged="btnSelectDocket_Click" Checked='<%# (Common.CastAsInt32(Eval("DocketId"))== DocketId) %>'  GroupName="dd" DocketId='<%#Eval("DocketId")%>' AutoPostBack="true"  runat="server" /> </td>                                                
                                                <td style="text-align:left;width:26%;"><%#Eval("VesselName")%></td>
                                                <td style="text-align:left;width:12%;"><%#Eval("DocketNo")%></td>
                                                <td style="text-align:left;width:20%;"><%#Eval("DocketType").ToString() + " - " + Eval("DDType").ToString()%></td>
                                                
                                                <%--<td align="center"><%#Common.ToDateString(Eval("StartDate")) + " To " + Common.ToDateString(Eval("EndDate")) %></td>--%>
                                                <td align="center" style="width:20%;"><%#Common.ToDateString(Eval("Act_StartDate")) + " To " + Common.ToDateString(Eval("Act_EndDate")) %></td>
                                                <td align="center" style="width:8%;"><%#Eval("DAYSGAP")%></td>
                                                <td align="left" style="width:8%;"> <%#Eval("DocketStatus")%></td>
                                                <%--<td align="center"><asp:LinkButton ID="btnExecClose" Text='<%# Eval("Status").ToString() == "E" ? "Close" : "Execute" %>' ForeColor='<%# Eval("Status").ToString() == "E" ? System.Drawing.Color.Red : System.Drawing.Color.Blue %>' OnClick="btnExecClose_Click" CommandArgument='<%#Eval("DocketId").ToString() + "~" + Eval("Status").ToString() %>' Visible='<%#Eval("ActionVisible").ToString() == "true" %>' runat="server"></asp:LinkButton>&nbsp;
                                                                   <asp:ImageButton runat="server" ToolTip="SendMail" ID="btnDocketMail" OnClick="btnDocketMail_Click" CommandArgument='<%#Eval("DocketId")%>' ImageUrl="~/Modules/PMS/Images/email.png" Visible='<%#Eval("Status").ToString() == "E" %>' />
                                                </td>--%>
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
    </div>

    <%-- Create / Edit Docket --%>
         <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%; " id="dv_AddEditDocket" runat="server" visible="false">
                        <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)"></div>
                            <div style="position: relative; width: 500px; height: 275px; padding: 3px; text-align: center;background: white; z-index: 150; top: 150px; border: solid 10px black;">
                               <asp:UpdatePanel runat="server" id="up1">
                                <ContentTemplate>
                                <div>
                                      <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                      <tr>
                                        <td  colspan="2" style="text-align: center;  font-size:14px; " class="text headerband" >
                                            DryDock Planning
                                        </td>
                                    </tr>

                                       <tr >
                                            <td style="text-align:right; width:25%;">Select Vessel :</td>
                                            <td style="text-align:left;"><asp:DropDownList runat="server" ID="ddlVessel" ValidationGroup="v1"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="ddlVessel" ValidationGroup="v1"></asp:RequiredFieldValidator>
                                            </td>
                            
                                        </tr>
                                        <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">Drydock Type :</td>
                                            <td style="text-align:left;"><asp:DropDownList runat="server" ID="ddlDrydockType" AutoPostBack="true" OnSelectedIndexChanged="ddlDrydockType_SelectedIndexChanged"  Width="100px"  ValidationGroup="v1">
                                                                            <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Planned" Value="Planned"></asp:ListItem>
                                                                            <asp:ListItem Text="Unplanned" Value="Unplanned"></asp:ListItem>
                                                                         </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlDrydockType" ValidationGroup="v1"></asp:RequiredFieldValidator>
                                            </td>                  
                                         </tr>
                                         <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">DD Type :</td>
                                            <td style="text-align:left;"><asp:DropDownList runat="server" ID="ddlDDType"  Width="100px"  ValidationGroup="v1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlDDType" ValidationGroup="v1"></asp:RequiredFieldValidator>
                                            </td>                  
                                         </tr>
                                        <tr >
                                            <td style="text-align:right; width:25%;">Start Date :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtStartDate"  Width="100px" MaxLength="15" ValidationGroup="v1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtStartDate" ValidationGroup="v1"></asp:RequiredFieldValidator>
                                            </td>                  
                                         </tr>
                                          <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">End Date :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtEndDate"  Width="100px" MaxLength="15"></asp:TextBox></td>                  
                                         </tr>
                                         <tr >
                                            <td style="text-align:right; width:25%;">Select Jobs From :</td>
                                            <td style="text-align:left;">
                                            <asp:RadioButton ID="rdoMaster"  GroupName="rd" Text="Master" runat="server" />
                                            <asp:RadioButton ID="rdoLastDocking"  GroupName="rd" Text=" Docket # : " runat="server" />
                                            <asp:TextBox runat=server ID="txtDocketNo" Width="200px"></asp:TextBox>
                                                                         
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="rdoJobSelection" ValidationGroup="v1"></asp:RequiredFieldValidator>--%>
                                            </td>                  
                                         </tr>
                                        </table>
                                        <br />
                                        <div style="text-align:center"><asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label></div>
                                        <br />
                                        <div style="text-align:center">
                                            <asp:CalendarExtender runat="server" ID="c1" TargetControlID="txtStartDate" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                                            <asp:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtEndDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                            <asp:Button runat="server" ID="btnSaveDocket" Text="Save" OnClick="btnSaveDocket_Click" ValidationGroup="v1" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                            <asp:Button runat="server" ID="btnCancelDocket" Text="Close" OnClick="btnCancelDocket_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                        </div>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btnCancelDocket" />
                                </Triggers>
                                </asp:UpdatePanel>
                            
                            </div>
                        </center>
         </div>

    <%-- Docket Closing --%>
         <div style="position: absolute; top: 0px; left: 0px; height: 520px; width: 100%; " id="div_DocketClosure" runat="server" visible="false">
                        <center>
                            <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)"></div>
                            <div style="position: relative; width: 600px; height: 335px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px black;">
                               <asp:UpdatePanel runat="server" id="UpdatePanel1">
                                <ContentTemplate>
                                <div>
                                      <table width="100%" cellpadding="4" cellspacing="0" border="0">
                                      <tr>
                                        <td  colspan="2" style="text-align: center;" class="text headerband" >
                                            DryDock Docket Closure
                                        </td>
                                    </tr>

                                       <tr >
                                            <td style="text-align:right; width:25%;">Closure Remarks :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtClosingRemarks" TextMode="MultiLine" Width="100%" Height="150px" ></asp:TextBox>
                                            </td>
                            
                                        </tr>                                        
                                        <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">Next DryDock Date :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtNextDDDate"  Width="100px" MaxLength="12" ValidationGroup="v1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtNextDDDate" ValidationGroup="v1"></asp:RequiredFieldValidator>
                                            </td>                  
                                         </tr>
                                         <tr >
                                            <td style="text-align:right; width:25%;">Report Sent to Ship :</td>
                                            <td style="text-align:left;"><asp:RadioButton ID="rdoReportSent_Y"  GroupName="rd" Text="Yes" runat="server" /><asp:RadioButton ID="rdoReportSent_N"  GroupName="rd" Text="No" runat="server" />                                                
                                            </td>                  
                                         </tr>
                                          <tr style="background-color:#DDF2FF;">
                                            <td style="text-align:right; width:25%;">Sent Date :</td>
                                            <td style="text-align:left;"><asp:TextBox runat="server" ID="txtSentDate"  Width="100px" MaxLength="15"></asp:TextBox></td>                  
                                         </tr>
                                         
                                        </table>
                                        <br />
                                        <div style="text-align:center"><asp:Label ID="lblMsg_Closure" ForeColor="Red" runat="server"></asp:Label></div>
                                        <br />
                                        <div style="text-align:center">
                                            <asp:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtNextDDDate" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                                            <asp:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtSentDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                            <asp:Button runat="server" ID="btnSaveDocketClosure" Text="Save" OnClick="btnSaveDocketClosure_Click" ValidationGroup="v1" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                            <asp:Button runat="server" ID="btnCancelDocketClosure" Text="Close" CausesValidation="false" OnClick="btnCancelDocketClosure_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                                        </div>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                  <asp:PostBackTrigger ControlID="btnSaveDocketClosure" />
                                  <asp:PostBackTrigger ControlID="btnCancelDocketClosure" />
                                </Triggers>
                                </asp:UpdatePanel>
                            
                            </div>
                        </center>
         </div>
    
    <%-- RFQ Execution --%>
         <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_RFQExecution" runat="server" visible="false">
                <center>
                  <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: black; z-index: 100; opacity: 0.6; filter: alpha(opacity=60)"></div>
                  <div style="position: relative; width: 450px; height: 170px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px black;">
            <asp:UpdatePanel runat="server" id="UpdatePanel2">
            <ContentTemplate>
            <div style="padding:3px; background-color:orange; text-align:center; font-weight:bold;" >RFQ Execution</div>
            <div style="border-bottom:none;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                   <tr>
                        <td style="text-align:right; width:170px; ">Docking Arrival Date : </td>
                        <td style="text-align:left">
                         <asp:TextBox ID="txtDocArrivalDt" runat="server" Width="100px" ></asp:TextBox>
                         <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtDocArrivalDt" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                        </td>
                    </tr>                     
                   <tr>
                        <td style="text-align:right;width:170px; ">Repair Commenced Date : </td>
                        <td style="text-align:left">
                         <asp:TextBox ID="txtExecFrom" runat="server" Width="100px" ></asp:TextBox>
                         <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtExecFrom" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right; width:170px; ">Estimated Completion Date : </td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtExecTo" runat="server" Width="100px" ></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtExecTo" Format="dd-MMM-yyyy" ></asp:CalendarExtender>
                        </td>
                    </tr>
                </table>
             </div>           

            <div style="text-align:center; padding:5px;">
                    
                        <asp:Label ID="lblMsg_Exec" ForeColor="Red" runat="server"></asp:Label>
                        <br /><br />
                    
                        <asp:Button runat="server" ID="btnExecuteRFQ" Text="Save" OnClick="btnExecuteRFQ_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                        <asp:Button runat="server" ID="btnCloseExec" Text="Close" OnClick="btnCloseExec_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                    
            </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExecuteRFQ" />
                <asp:PostBackTrigger ControlID="btnCloseExec" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
                </center>
            </div>
     
    <%-- Send Mail --%>
         <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;" id="dv_SendMail" runat="server" visible="false">
                <center>
                  <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Black; z-index: 100; opacity: 0.6 filter: alpha(opacity=60)"></div>
                  <div style="position: relative; width: 450px; height: 120px; padding: 3px; text-align: center;background: white; z-index: 150; top: 50px; border: solid 10px black;">
            <asp:UpdatePanel runat="server" id="UpdatePanel3">
            <ContentTemplate>
            <div style="padding:3px;  text-align:center; " class="text headerband" >Send Mail</div>
            <div style="border-bottom:none;" class="scrollbox">
                <table cellspacing="0" rules="none" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">                    
                   <tr>
                        <td style="text-align:right; width:150px; ">Update for :</td>
                        <td style="text-align:left">
                         <%--<asp:CheckBoxList ID="chkMailType" RepeatDirection="Horizontal" runat="server">
                         <asp:ListItem Text="Job Tracking" Value="1"></asp:ListItem>
                         <asp:ListItem Text="Cost Tracking" Value="2"></asp:ListItem>
                         </asp:CheckBoxList>--%>
                         <asp:CheckBox ID="chkjobTrtacking" Text="Job Tracking" runat="server" />&nbsp;<asp:CheckBox ID="chkCostTracking" Text="Cost Tracking" runat="server" />
                        </td>
                    </tr>
                </table>
             </div>           

            <div style="text-align:center; padding:5px;">
                    <div style="text-align:left;float:left; padding-top:5px;">
                        <asp:Label ID="lblMsg_Mail" ForeColor="Red" runat="server"></asp:Label>
                    </div>
                    <div style="text-align:left;float:right">
                        <asp:Button runat="server" ID="btnSendMail" Text="Send Mail" OnClick="btnSendMail_Click" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                        <asp:Button runat="server" ID="btnCloseMail" Text="Close" OnClick="btnCloseMail_Click" CausesValidation="false" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;"  />
                    </div>
            </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSendMail" />
                <asp:PostBackTrigger ControlID="btnCloseMail" />
            </Triggers>
            </asp:UpdatePanel>
            </div>
                </center>
            </div>


    
    <script type="text/javascript">
        function CallFunction(data) {
            document.getElementById('txtAction').setAttribute("value", data);
            document.getElementById('btnAction').focus();
            document.getElementById('btnAction').click();
            return false;
        }
    </script>
    </form>
</body>
</html>
