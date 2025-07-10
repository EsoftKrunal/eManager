<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VSL_CriticalEqpShutdownReq.aspx.cs" Inherits="VSL_CriticalEqpShutdownReq" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function refereshparent() {
            window.opener.reloadunits();
        }
        function printwindow(vsl,shutdownId) {
            window.open('Reports/CriticalShutdownRequest.aspx?VC=' + vsl+ '&SD=' + shutdownId, '', '');
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div style="text-align: center; width:1000px; border:solid 1px black;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" border="0" style="background-color:#f9f9f9; " width="100%">
         <tr style="background-color:#d2d2d2; vertical-align:middle;">
             <td style="width:65%; text-align:center; padding:5px;font-weight:bold; font-size:14px; " >Critical Equipment Shutdown Request</td>
         </tr>
         <tr style="height:30px; vertical-align:middle;">
          <td style="text-align:center; padding:5px; font-weight:bold; width: 65%; font-size: 14px;">
               <asp:Button ID="btnPrint" runat="server" CssClass="btn" OnClick="btnPrint_Click" style="float:right; padding-right:5px;" Text="Print" Visible="false" />
               Vessel :&nbsp;<asp:Label ID="lblSd_VesselCode" runat="server"></asp:Label>
           </td>
          </tr>
          <tr>
                <td style="text-align:left; padding:5px;">
                    <b>&nbsp;Note: Please ensure following are included in your Risk Assessment.</b>
                    <br /><span style="font-size:125%;">&nbsp;•</span>
                    Personal, spares and Tools.
                    <br /><span style="font-size:125%;">&nbsp;•</span> Worst case scenarios.
                    <br /><span style="font-size:125%;">&nbsp;•</span> Recovery and mitigation measures.
                    <br /><span style="font-size:125%;">&nbsp;•</span> Commissioning and testing procedures.
                    <br /><span style="font-size:125%;">&nbsp;•</span> Alternative back-up equipment/systems
                    <br /><span style="font-size:125%;">&nbsp;•</span> Necessary modification in operational procedures as a result.
                    <br /><span style="font-size:125%;">&nbsp;•</span> Additional safety procedures (emergency)
                    <br /><span style="font-size:125%;">&nbsp;•</span> Inform office before and after maintenance 
                </td>
            </tr>
          <tr>
              <td style="padding:10px">
                  <fieldset>
                      <table cellpadding="2" cellspacing="2" width="100%">
                          <tr>
                              <td style="text-align:right;">
                                  Parent Component :
                              </td>
                              <td style="text-align:left;" colspan="3">
                                  <asp:Label ID="lblParent" runat="server" 
                                      style="color :Blue;font-size:12px; font-style:italic;"></asp:Label>
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align:right;">
                                  Component Code :
                              </td>
                              <td style="text-align:left;">
                                  <asp:Label ID="lblSd_CompCode" runat="server"></asp:Label>
                              </td>
                              <td style="text-align:right;">
                                  Component Name :&nbsp;
                              </td>
                              <td style="text-align:left;">
                                  <asp:Label ID="lblSd_CompName" runat="server"></asp:Label>
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align:right;">
                                  Occasion :</td>
                              <td style="text-align:left;">
                                  <asp:DropDownList ID="ddlOccasion" runat="server" AutoPostBack="true" 
                                      onselectedindexchanged="ddlOccasion_SelectedIndexChanged" 
                                      Width="140px">
                                      <asp:ListItem Text="&lt; Select &gt;" Value="0"></asp:ListItem>
                                      <asp:ListItem Text="Routine" Value="1"></asp:ListItem>
                                      <asp:ListItem Text="Break Down" Value="2"></asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                              <td style="text-align:right;">
                                  <span ID="sp_Defect" runat="server" visible="false">Defect No :</span>&nbsp;
                              </td>
                              <td style="text-align:left;">
                                  <asp:TextBox ID="txtDefectNo" runat="server" MaxLength="15" 
                                      Visible="false" Width="150px"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align:right;">
                                  Request Date :
                              </td>
                              <td style="text-align:left;">
                                  <asp:TextBox ID="txtReqdt" runat="server" ></asp:TextBox>
                                  <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                      Format="dd-MMM-yyyy" PopupButtonID="txtReqdt" PopupPosition="TopRight" 
                                      TargetControlID="txtReqdt">
                                  </asp:CalendarExtender>
                              </td>
                              <td style="text-align:right;">
                                  Master/CE Name :&nbsp;
                              </td>
                              <td style="text-align:left;">
                                  <asp:TextBox ID="txtMasterName" runat="server" MaxLength="50"  
                                      Width="150px"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                              <td style="text-align:right;">
                                  Job/Defect :
                              </td>
                              <td colspan="3" style="text-align:left;">
                                  <asp:TextBox ID="txtJobDefect" runat="server" Height="100px" MaxLength="5000" 
                                       TextMode="MultiLine" Width="647px"></asp:TextBox>
                              </td>
                          </tr>
                      </table>
                  </fieldset>
              </td>
          </tr>
             <tr>
                <td style="text-align:left; padding:5px;">
                    <b>Request for Planned Shutdown or Extension of Shutdown:</b>
                    <br />
                    <%--<i>(For any extension of shutdown a new request must be sent to office for approval)</i>--%>
                    <br />
                </td>
            </tr>
            <tr>
               <td style="padding:10px">
               <fieldset>
                    <legend><b>Request</b></legend>
                    <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Always">
                    <ContentTemplate>
                        <table cellpadding="2" cellspacing="2" width="100%">
                     <tr>
                         <td style="text-align:right; width:250px;">Planned Shutdown (Total Hours) :&nbsp; 
                         </td>
                         <td style="text-align:left;">
                            <asp:TextBox ID="txtSd_PlannedSDHrs" AutoPostBack="true" OnTextChanged="txtSd_PlannedFromDate_TextChanged" required='yes' runat="server"></asp:TextBox>    
                         </td>
                      </tr>
                      <tr>
                         <td style="text-align:right;">
                             Planned From Date/Time (Ship’s LT) :&nbsp;
                         </td>
                         <td style="text-align:left;">
                            <asp:TextBox ID="txtSd_PlannedFromDate" AutoPostBack="true" OnTextChanged="txtSd_PlannedFromDate_TextChanged" required='yes' runat="server"></asp:TextBox> 
                            <asp:CalendarExtender ID="CalendarExtender2" PopupButtonID="txtSd_PlannedFromDate" Format="dd-MMM-yyyy" TargetControlID="txtSd_PlannedFromDate" runat="server" PopupPosition="TopRight"></asp:CalendarExtender>
                             &nbsp;/&nbsp;
                             <asp:DropDownList ID="ddlSd_PlannedFromHr" AutoPostBack="true" OnSelectedIndexChanged="txtSd_PlannedFromDate_TextChanged" Width="40px" runat="server" ></asp:DropDownList>
                             &nbsp;:&nbsp;
                             <asp:DropDownList ID="ddlSd_PlannedFromMin" AutoPostBack="true" OnSelectedIndexChanged="txtSd_PlannedFromDate_TextChanged" Width="40px" runat="server" ></asp:DropDownList>
                             [HRS]
                         </td>
                     </tr>
                     <tr>
                         <td style="text-align:right;">
                             Planned To Date/Time (Ship’s LT) :&nbsp;
                         </td>
                         <td style="text-align:left;">
                             <asp:TextBox ID="txtSd_PlannedToDate" runat="server" Enabled="false" ></asp:TextBox>
                             <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtSd_PlannedToDate" PopupPosition="TopRight" TargetControlID="txtSd_PlannedToDate"></asp:CalendarExtender>
                             &nbsp;/&nbsp;
                             <asp:DropDownList ID="ddlSd_PlannedToHr" Enabled="false" runat="server" Width="40px"></asp:DropDownList>
                             &nbsp;:&nbsp;
                             <asp:DropDownList ID="ddlSd_PlannedToMin" Enabled="false" runat="server" Width="40px"></asp:DropDownList>
                             [HRS]
                         </td>
                       </tr>
                       <tr>
                           <td colspan="2" style="text-align: right;">
                               <asp:Button ID="btnSave" runat="server" CssClass="btn" onclick="btnSave_Click" Text="Save" />&nbsp;
                               <asp:Button ID="btnAddExtension" runat="server" CssClass="btn" OnClick="btnAddExtension_Click" Text="Request for extension" Width="150px" />
                           </td>
                       </tr>
                       </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                        <asp:PostBackTrigger ControlID="btnAddExtension" />
                    </Triggers> 
                    </asp:UpdatePanel>
               </fieldset>
               </td>
            </tr>
           
            <tr ID="trOfficeApproval" runat="server" visible="false">
                          <td style="padding:10px">
                               <fieldset>
                                   <legend><b>Approval (For Office use only))</b></legend>
                                   <table cellpadding="2" cellspacing="2" width="100%">
                                       <%--<tr>
                            <td style="text-align:left;">
                               <b>Approval (For Office use only)</b>
                            </td>
                        </tr>--%>
                                       
                                       <tr>
                                           <td style="text-align:right;width:350px;">
                                               Approver Name :</td>
                                           <td style="text-align:left;">
                                               <asp:TextBox ID="txtSd_ApproverName" runat="server" ></asp:TextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td style="text-align:right;">
                                               Approver Position :</td>
                                           <td style="text-align:left;">
                                               <asp:TextBox ID="txtSd_ApproverPosition" runat="server" ></asp:TextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td style="text-align: right;">
                                               Approved On :
                                           </td>
                                           <td style="text-align: left;">
                                               <asp:Label ID="lbl_FReqApproved" runat="server"></asp:Label>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td style="text-align: right;">
                                               Approver’s Remark :</td>
                                           <td style="text-align: left;">
                                               <asp:TextBox ID="txtSd_ApproverRemarks" runat="server" Height="74px" 
                                                   MaxLength="5000"  TextMode="MultiLine" Width="600px"></asp:TextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="2" 
                                               style="text-align: right; padding-right: 5px; padding-left: 5px;">
                                               <asp:Button ID="btnApprove" runat="server" CssClass="btn" 
                                                   onclick="btnApprove_Click" Text="Approve" Width="100px" />
                                           </td>
                                       </tr>
                                   </table>
                               </fieldset>
                           </td>
                       </tr>
                       <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" style="background-color:#f9f9f9; " width="100%">
        <tr>
             <td style="padding:10px">
                <fieldset>
                                <legend> <b>Attachments</b>  </legend>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                    <table border="0" cellpadding="4" cellspacing="0" rules="none" style="width:100%;border-collapse:collapse;">
                                    <col />
                                    <col width="200px" />
                                    <col width="120px" />                                    
                                    <tr>
                                        <td style="text-align:left">
                                            <asp:Label ID="txtDescription" Font-Bold="true" runat="server" Text="Risk Assessment" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:FileUpload id="flpAttachment" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSaveAttachment" runat="server"  Text="Upload Attachment" OnClick="btnSaveAttachment_OnClick" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>

                                  </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSaveAttachment" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                    <br />

                                    <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                        <col />
                                        <col width="150px" />                                        
                                        <col width="30px" />                                        
                                        <tr class= "headerstylegrid" style="text-align:left;">
                                            <td>Description</td>
                                            <td>Attachment</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                        <col />
                                        <col width="150px" />                                        
                                        <col width="30px" />                                        
                                        <asp:Repeater ID="rptAttachment" runat="server" >
                                            <ItemTemplate>
                                                <tr align="left">
                                                    <td> <%#Eval("Description") %> </td>
                                                    <td> 
                                                        <asp:HiddenField ID="hfAttachmentID" runat="server" Value='<%#Eval("AttachmentID")%>' />
                                                        <asp:LinkButton ID="lnkDownloadAttachment" runat="server" Text='<%#Eval("AttachmentName")%>' OnClick="lnkDownloadAttachment_OnClick"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnDeleteAttachment" runat="server" ImageUrl="~/Images/Delete.png" OnClick="btnDeleteAttachment_OnClick" Visible='<%#EditAllowed%>' ToolTip="Delete" OnClientClick="return confirm('Are you sure to delete?')" />
                                                    </td>
                                                </tr>        
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        
                                        </table>

                                    
                            
                            </fieldset>
            </td>
        </tr>

    </table>     
                        </td>
                       </tr>

             <tr>
                <td style="padding:10px">
                <fieldset>
                    <legend> <b>Office Comments / Approval </b>  </legend>
                    <table width="100%" border="0" cellpadding="2" cellspacing="2" >
                        <col width="100px" />
                        <col width="5px" />
                        <col />
                            <tr>
                                <td style="text-align:left;"> <b> Comments By</b></td>
                                <td><b>:</b></td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblApprovedBy" runat="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:left;"><b>Comments</b></td>
                                <td><b>:</b></td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblApproverRemark" runat="server" ></asp:Label>
                                </td>
                            </tr>
                    </table>
                </fieldset>
                    
                </td>
             </tr>
            <tr ID="trClosure" runat="server" >
                         <td style="padding:10px">
                             <fieldset>
                                 <legend><b>Details of Maintenance Completion / Closure : </b></legend>
                                 <table cellpadding="2" cellspacing="2" width="100%">
                                      <tr>
                                         <td style="text-align:right; width:350px;">
                                             Maintenance Commenced From Date/Time (Ship’s LT) :&nbsp;</td>
                                         <td style="text-align:left;">
                                             <asp:TextBox ID="txtSd_MCommencedDate" runat="server" ></asp:TextBox>
                                             &nbsp;/&nbsp;<asp:DropDownList ID="ddlSd_MCommencedHr" runat="server" Width="40px">
                                             </asp:DropDownList>
                                             &nbsp;:&nbsp;
                                             <asp:DropDownList ID="ddlSd_MCommencedMin" runat="server" Width="40px">
                                             </asp:DropDownList>
                                             [HRS]
                                             <asp:CalendarExtender ID="CalendarExtender4" runat="server" 
                                                 Format="dd-MMM-yyyy" PopupButtonID="txtSd_MCommencedDate" 
                                                 PopupPosition="TopRight" TargetControlID="txtSd_MCommencedDate">
                                             </asp:CalendarExtender>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="text-align:right;">
                                             Maintenance Completed Date/Time (Ship’s LT) :&nbsp;</td>
                                         <td style="text-align:left;">
                                             <asp:TextBox ID="txtSd_CompletedDate" runat="server" ></asp:TextBox>
                                             &nbsp;/&nbsp;<asp:DropDownList ID="ddlSd_MCompletedHr" runat="server" Width="40px">
                                             </asp:DropDownList>
                                             &nbsp;:&nbsp;
                                             <asp:DropDownList ID="ddlSd_MCompletedMin" runat="server" Width="40px">
                                             </asp:DropDownList>
                                             [HRS]
                                             <asp:CalendarExtender ID="CalendarExtender5" runat="server" 
                                                 Format="dd-MMM-yyyy" PopupButtonID="txtSd_CompletedDate" 
                                                 PopupPosition="TopRight" TargetControlID="txtSd_CompletedDate">
                                             </asp:CalendarExtender>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="text-align:right;">
                                             Remarks :&nbsp;</td>
                                         <td style="text-align:left;">
                                             <asp:TextBox ID="txtSd_MaintenanceRemarks" runat="server" Height="74px" 
                                                 MaxLength="5000"  TextMode="MultiLine" Width="600px"></asp:TextBox>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td colspan="2" 
                                             style="text-align: right; padding-right: 5px; padding-left: 5px;">
                                             <asp:Button ID="btnClosure" runat="server" CssClass="btn" 
                                                 onclick="btnClosure_Click" Text="Save"  />
                                         </td>
                                     </tr>
                                 </table>
                             </fieldset>
                         </td>
            </tr>
            <tr id="trExtension" runat="server" visible="false">
                         <td style="padding:10px">
                             <fieldset>
                                 <legend><b>Extensions</b></legend>
                                 <table border="1" cellpadding="4" cellspacing="0" rules="all" 
                                     style="width:100%;border-collapse:collapse;">
                                     <colgroup>
                                         <%if (Session["UserType"].ToString().Trim() == "O")
                             {%>
                                         <col style="width:25px;" />
                                         <% }%>
                                         <col style="width:200px;" />
                                         <col />
                                         <col style="width:220px;" />
                                         <col style="width:180px;" />
                                         <col style="width:17px;" />
                                     </colgroup>
                                     <tr align="left" class= "headerstylegrid">
                                         <%if (Session["UserType"].ToString().Trim() == "O")
                             {%>
                                         <td>
                                             &nbsp;</td>
                                         <% }%>
                                         <td>
                                             Planned Shutdown (Total Hours)</td>
                                         <td>
                                             Planned From Date/Time (Ship’s LT)</td>
                                         <td>
                                             Planned To Date/Time (Ship’s LT)</td>
                                         <td>
                                             Approved By/ On</td>
                                         <td>
                                         </td>
                                     </tr>
                                 </table>
                                 <div ID="divShutdownExt" class="scrollbox" onscroll="SetScrollPos(this)" 
                                     style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 100px ; text-align:center;">
                                     <table border="1" cellpadding="4" cellspacing="0" rules="all" 
                                         style="width:100%;border-collapse:collapse;">
                                         <colgroup>
                                             <%if (Session["UserType"].ToString().Trim() == "O")
                             {%>
                                             <col style="width:25px;" />
                                             <% }%>
                                             <col style="width:200px;" />
                                             <col />
                                             <col style="width:220px;" />
                                             <col style="width:180px;" />
                                             <col style="width:17px;" />
                                         </colgroup>
                                         <asp:Repeater ID="rptShutdownExt" runat="server">
                                             <ItemTemplate>
                                                 <tr class="row" style="cursor:pointer">
                                                     <%if (Session["UserType"].ToString().Trim() == "O")
                             {%>
                                                     <td align="center">
                                               <%--          <asp:ImageButton ID="imgbtn_Approve" runat="server" 
                                                             CommandArgument='<%#Eval("ExtensionId") %>' ImageUrl="~/Images/approved.png" 
                                                             OnClick="imgbtn_Approve_Click" ToolTip="Approve" 
                                                             Visible='<%#Eval("Approved").ToString() == "" && ISClosed.ToString()=="False" %>' />--%>
                                                     </td>
                                                     <% }%>
                                                     <td align="right">
                                                         <%#Eval("Ext_ShutDownTotalHrs")%>
                                                     </td>
                                                     <td align="center">
                                                         <%# Convert.ToDateTime(Eval("Ext_FromDateTime").ToString().Split(' ').GetValue(0).ToString()).ToString("dd-MMM-yyyy") + "/ " + Eval("Ext_FromDateTime").ToString().Split(' ').GetValue(1).ToString()%>[HRS]
                                                     </td>
                                                     <td align="center">
                                                         <%# Convert.ToDateTime(Eval("Ext_ToDateTime").ToString().Split(' ').GetValue(0).ToString()).ToString("dd-MMM-yyyy") + "/ " + Eval("Ext_ToDateTime").ToString().Split(' ').GetValue(1).ToString()%>[HRS]
                                                     </td>
                                                     <td align="center">
                                                         <%#Eval("Approved")%>
                                                     </td>
                                                     <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                 </tr>
                                             </ItemTemplate>
                                         </asp:Repeater>
                                     </table>
                                 </div>
                             </fieldset> </td>
                     </tr>
            </table>

         <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="dvExtensions" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:525px; height:200px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <ContentTemplate>
                <center>
                   <table cellpadding="4" cellspacing="2" width="100%">
                    <tr>
                     <td colspan="2" 
                            style="background-color:#d2d2d2; font-size:14px; font-weight:bold; height:25px; vertical-align:middle; text-align:center;"> Add Extensions  </td>
                    </tr>
                     <tr>
                         <td style="text-align:right;">Planned Shutdown (Total Hours) :&nbsp; 
                         </td>
                         <td style="text-align:left;">
                            <asp:TextBox ID="txtExtSDHrs" AutoPostBack="true" OnTextChanged="txtExtFromDate_TextChanged" required='yes' runat="server"></asp:TextBox>
                            </td>
                      </tr>
                      <tr>
                         <td style="text-align:right;">
                             Planned From Date/Time (Ship’s LT) :&nbsp; 
                         </td>
                         <td style="text-align:left;">
                             <asp:TextBox ID="txtExtFromDate" AutoPostBack="true" OnTextChanged="txtExtFromDate_TextChanged" required='yes' runat="server"></asp:TextBox> 
                            <asp:CalendarExtender ID="CalendarExtender6" PopupButtonID="txtExtFromDate" Format="dd-MMM-yyyy" TargetControlID="txtExtFromDate" runat="server" PopupPosition="TopRight" runat="server"></asp:CalendarExtender>
                             &nbsp;/&nbsp;<asp:DropDownList ID="ddlExtFromHr" AutoPostBack="true" OnSelectedIndexChanged="txtExtFromDate_TextChanged" Width="40px" runat="server" ></asp:DropDownList>&nbsp;:&nbsp;
                                <asp:DropDownList ID="ddlExtFromMin"  AutoPostBack="true" OnSelectedIndexChanged="txtExtFromDate_TextChanged" Width="40px" runat="server" ></asp:DropDownList>
                         </td>
                      </tr>
                      <tr>
                         <td style="text-align:right;">
                             Planned To Date/Time (Ship’s LT) :&nbsp; 
                         </td>
                         <td style="text-align:left;">
                            <asp:TextBox ID="txtExtToDate" required='yes' Enabled="false" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender7" PopupButtonID="txtExtToDate" Format="dd-MMM-yyyy" TargetControlID="txtExtToDate" runat="server" PopupPosition="TopRight" runat="server"></asp:CalendarExtender>
                            &nbsp;/&nbsp;<asp:DropDownList ID="ddlExtToHr" Enabled="false" Width="40px" runat="server" ></asp:DropDownList>&nbsp;:&nbsp;
                                <asp:DropDownList ID="ddlExtToMin" Enabled="false" Width="40px" runat="server" ></asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                           <td colspan="2" style="text-align: right; padding-right: 5px; padding-left: 5px;">
                               <asp:Button ID="btnSaveExtension" Text="Save" CssClass="btn" runat="server" 
                                   onclick="btnSaveExtension_Click" />&nbsp;
                               <asp:Button ID="btnCloseExtension" Text="Close" CssClass="btn" OnClick="btnCloseExtension_Click" runat="server" />
                           </td>
                       </tr>

                     </table>
                </center>
                </ContentTemplate>
                <Triggers>
                 <asp:PostBackTrigger ControlID="btnCloseExtension" />
                 </Triggers>
                </asp:UpdatePanel>
            </div> 
            </center>
         </div>
</ContentTemplate>
    </asp:UpdatePanel>
    

        <center>
            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_OnClick" Text="Export" CssClass="btn" Width="139px" />
            <br /> <br />
        </center>
     </div>
     </center>
    </form>
</body>
</html>
