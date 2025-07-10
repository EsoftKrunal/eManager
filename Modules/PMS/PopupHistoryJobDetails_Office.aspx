<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupHistoryJobDetails_Office.aspx.cs" Inherits="PopupHistoryJobDetails_Office" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
    *
    {
        font-family:Calibri;
        font-size:13px;
    }
    .pageheader
    {
         text-align :center; 
         padding:8px; 
         background-color:#4da6ff; 
         color:White; 
         font-size:20px;
    }
     .myheading1
    {
        color:black; 
        text-align :left; 
        font-size :16px; 
        padding:4px;
        border-bottom:solid 1px #ffcc80;
        background-color:#fff7e6;
        font-weight:bold;
    }
    .gridheader
    {
        background-color:#c2c2c2;
    }
    .border_table td
    {
        border: solid 1px #e5e5e5;
    }
     
        .btn
        {}
     
    </style>
    <script type="text/javascript">
        function closewin()
        {
            if (window.opener != null)
                window.opener.document.getElementById("btnTemp").click();
            window.close();
        }
    </script>
</head>
<body style="font-family: Calibri; font-size: 14px;">

    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

     <div class="text headerband" ><asp:Label ID="lblPagetitle" runat="server" ></asp:Label></div>
     </div>
        <div style="font-family:Arial;font-size:12px;">
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <div style="width:100%; height:440x; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                        <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>                       
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-top:5px;"></td>
                        </tr>
                        <tr>
                          <td>
                              <asp:Panel ID="plUpdateJobs" runat="server">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                   
                                     <tr>
                                            <td colspan="2">
                                                 <table border="0" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                        <tr>
                                                            <%--<td style="text-align:right; font-weight:bold">Vessel :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblUpdateVessel" runat="server"></asp:Label> </td>
                                                            <td>&nbsp;</td>--%>
                                                            <td style="text-align:left;width:80px;  font-weight:bold">Component :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblUpdateComponent" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                            <td style="text-align:right;width:75px; font-weight:bold">Interval :&nbsp;</td>
                                                            <td style="text-align:left;width:50px;"><asp:Label ID="lblUpdateInterval" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align:right; width:50px; font-weight:bold">Job :&nbsp;</td>
                                                           <td colspan="4" style="text-align:left; "><asp:Label ID="lblUpdateJob" runat="server"></asp:Label> </td>
                                                           <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                 </table>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td>
                                           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; height:225px">
                                           <%--<tr>
                                               <td style="text-align:left; font-weight:bold">Component :&nbsp;</td>
                                               <td style="text-align:left;font-weight:bold;"><asp:Label ID="lblComponent" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Job :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblJobDesc" runat="server"></asp:Label></td>
                                            </tr>--%>
                                            <tr>
                                               <td style="text-align:left">Emp. No &nbsp;/&nbsp;Emp. Name :</td>
                                               <td style="text-align:left;"><asp:Label ID="lblEmpNo" runat="server" ></asp:Label>
                                                           &nbsp;/&nbsp;<asp:Label ID="lblEmpName" runat="server" ></asp:Label>
                                                       </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Rank :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblRank" runat="server" ></asp:Label></td>
                                               
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Maintenance Reason :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblRemarks" runat="server" ></asp:Label>
                                                  <br />
                                                  <asp:RadioButtonList ID="rdoBreakdownReason" RepeatDirection="Horizontal" runat="server" Visible="false">
                                                   <asp:ListItem Text="Equipment Working" Value="1"></asp:ListItem>
                                                   <asp:ListItem Text="Equipment Not Working" Value="2"></asp:ListItem>
                                                  </asp:RadioButtonList>     
                                               </td>
                                            </tr>
                                            <tr id="trSpecify" runat="server" visible="false">
                                                <td style="text-align:left">Specify :&nbsp;</td>
                                                <td style="text-align:left"><asp:Label ID="lblSpecify" runat="server" ></asp:Label></td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Last Done Date :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblLastDoneDt" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Interval :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblInterval" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr id="trHr" runat="server" visible="false">
                                                
                                                <td colspan="2">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                                                <ContentTemplate>
                                                  <table border="0" cellpadding="0" cellspacing="0" rules="all" width="100%";border-collapse:collapse;">
                                                      <tr>
                                                        <td style="text-align:left">Last Hour </td>
                                                        <td style="text-align:left">Done Hour </td>
                                                        <td style="text-align:left">Next Hour </td>
                                                      </tr>
                                                      <tr style="padding-top:5px;">
                                                         <td style="text-align:left;" ><asp:Label ID="lblLastHour" runat="server" ></asp:Label></td>
                                                         <td style="text-align:left;"><asp:Label ID="lblDoneHour" runat="server" ></asp:Label></td>
                                                         <td style="text-align:left;"><asp:Label ID="lblNextHour" runat="server" ></asp:Label></td>
                                                      </tr>
                                                  </table>
                                                   </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </td>        
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                                <ContentTemplate>
                                                  <table border="0" cellpadding="0" cellspacing="0" rules="all" width="100%";border-collapse:collapse;">
                                                      <tr>
                                                         <td style="text-align:left">Due Date</td>
                                                         <td style="text-align:left">Done Date</td>
                                                         <td style="text-align:left">Next Due Date</td>
                                                      </tr> 
                                                      <tr>  
                                                         <td style="text-align:left"><asp:Label ID="lblDuedt" runat="server"></asp:Label></td>
                                                         <td style="text-align:left;"><asp:Label ID="lblDoneDate" runat="server" ></asp:Label>
                                                            
                                                         </td>
                                                         <td style="text-align:left; "><asp:Label ID="lblNextDueDt" runat="server"></asp:Label></td>
                                                      </tr>
                                                  </table>
                                                  </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </td>          
                                            </tr>
                                        </table>
                                        </td>
                                        <td>
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                              <tr>
                                               <td style="text-align:left">Service Report :&nbsp;</td>
                                               <td style="text-align:left">
                                                   <asp:Label ID="lblServiceReport" runat="server" Height="150px" Width="350px" ></asp:Label>                                                       
                                                       </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Condition Before :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblCondBefore" runat="server"></asp:Label>
                                               <asp:HiddenField ID="hfIntervalId_H" runat="server" />
                                               <asp:HiddenField ID="hfInterval_H" runat="server" />
                                               </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Condition After :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblCondAfter" runat="server"></asp:Label>
                                               </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Updated By&nbsp;/&nbsp;On : </td>
                                               <td style="text-align:left"><asp:Label ID="lblUpdatedByOn" runat="server"></asp:Label></td>
                                            </tr>
                                           <%-- <tr id="trDoc" runat="server">
                                                <td style="text-align:left">Document :&nbsp;</td>
                                                <td style="text-align:left; ">
                                                  <a runat="server" ID="ancFile"  target="_blank" visible='<%#Eval("FileName").ToString()!= "" %>'  title="View Attached File" >
                                                       <img src="Images/paperclip.gif" style="border:none"  /></a>
                                                    </td>
                                            </tr>--%>
                                            </table>
                                        </td>
                                    </tr>
                                   <tr id="trAttachment" runat="server" visible="false">
                                       
                                        <td colspan="2" style="text-align:left;">
                                        <div style="font-weight:bold; padding-bottom:3px;">Attachments</div>
                                          <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                                <colgroup>
                                                        <col  />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 17px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td>
                                                            Description
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            <div id="divAttachment" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 130px; text-align: center;">
                                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;
                                                    border-collapse: collapse;">
                                                    <colgroup>
                                                        <col  />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 17px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptAttachment" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="row" >
                                                                <td align="left">
                                                                    <%#Eval("AttachmentText")%>
                                                                </td>
                                                                <td>
                                                                   <a runat="server" ID="ancFile"  href='<%# ProjectCommon.getLinkFolder(DateTime.Parse(lblDoneDate.Text.Trim())) + Eval("FileName").ToString()  %>' target="_blank" Visible='<%# Eval("FileName").ToString() != "" %>' title="Show Attachment" >
                                                                    <img src="Images/paperclip.gif" style="border:none"  />
                                                                   </a>
                                                                </td>
                                                                <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                              <asp:Panel ID="plPostpone" runat="server" >
                              <table border="0" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               
                               <tr>
                                            <td>
                                                 <table border="0" cellpadding="4" cellspacing="2" style="width:100%;border-collapse:collapse;">
                                                        <tr>
                                                            <td style="text-align:right;width:80px; font-weight:bold">Component :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblPostponeComponent" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                            <td style="text-align:right; width:75px; font-weight:bold">Interval :&nbsp;</td>
                                                            <td style="text-align:left; width:50px;"><asp:Label ID="lblPostponeInterval" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align:right;width:50px; font-weight:bold">Job :&nbsp;</td>
                                                            <td colspan="4" style="text-align:left;"><asp:Label ID="lblPostponeJob" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                 </table>
                                            </td>
                                        </tr>               
                               <tr>
                                 <td>
                                   <table cellpadding="4" cellspacing="0" border="1" style="width:100%; border-collapse:collapse;" bordercolor="black">
                                          <tr>
                                              <td style="text-align:right; width:200px;">Postpone Reason :&nbsp;</td>
                                              <td style="text-align:left"><asp:Label ID="lblPpReason" runat="server"></asp:Label> 
                                              </td>
                                              <td style="text-align:right; width:120px;">Requested By :&nbsp;</td>
                                              <td style="text-align:left"><asp:Label ID="lblPRank" runat="server" ></asp:Label></td>
                                          </tr>
                                        <tr>
                                           <td style="text-align:right; width:200px;">Emp. No :&nbsp;</td>
                                           <td style="text-align:left"><asp:Label ID="lblPEmpCode" runat="server" ></asp:Label></td>
                                           <td style="text-align:right; width:120px;">Emp. Name :&nbsp;</td>
                                           <td style="text-align:left"><asp:Label ID="lblPEmpname" runat="server" ></asp:Label></td>
                                        </tr>
                                        <tr>
                                              <td style="text-align:right; width:200px;">Postpone Till Date :&nbsp;</td>
                                              <td style="text-align:left"><asp:Label ID="lblPostponedate"  runat="server" ></asp:Label></td>
                                              <td style="text-align:right;width:120px;">Requested By&nbsp;/&nbsp;On : </td>
                                              <td style="text-align:left"><asp:Label ID="lblPostPonedByOn" runat="server"></asp:Label></td>
                                             </tr>
                                        </table>
                                         <table cellpadding="4" cellspacing="0" border="1" style="width:100%; border-collapse:collapse;" bordercolor="black">
                                          <tr>
                                              <td style="text-align:right; width:200px;">Postpone Remarks :&nbsp;</td>
                                              <td colspan="3" style="text-align:left">
                                                   <asp:Label ID="lblPostponeRemarks" runat="server" Height="185px" Width="370px" ></asp:Label>
                                              </td>
                                          </tr>
                                          </table>
                                         
                                         <table cellpadding="4" cellspacing="0" border="1" style="width:100%; border-collapse:collapse;" bordercolor="black">
                                          <tr>
                                              <td class="style1" colspan="2">
                                                  <strong>Office Comments</strong></td>
                                          </tr>
                                          <tr>
                                              <td style="text-align:right; width:200px;">
                                                  Postpone Approval :</td>
                                              <td style="text-align:left; font-weight:bold;">
                                                <asp:RadioButton runat="server" ID="radApproved" Text="Approved" ForeColor="Green" GroupName="g1"/>
                                                <asp:RadioButton runat="server" ID="radReject" Text="Rejected" ForeColor="Red" GroupName="g1"/>
                                              &nbsp;</td>
                                          </tr>
                                             <tr>
                                                 <td style="text-align:right; width:200px;">
                                                     Office Comments :&nbsp;&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:TextBox ID="txtOfficeComments" runat="server" 
                                                         style="width:80%; height:60px;" TextMode="MultiLine"></asp:TextBox>
                                                 </td>
                                             </tr>
                                          </table>
                                         <table cellpadding="4" cellspacing="0" border="1" style="width:100%; border-collapse:collapse;" bordercolor="black">
                                          <tr>
                                              <td style="text-align:right; width:200px;">
                                                  Office Comments By / On :&nbsp;&nbsp;</td>
                                              <td style="text-align:left">
                                                  <asp:Label ID="lblOfficeCommentByOn" runat="server"></asp:Label>
                                              </td>
                                              <td style="text-align:right;width:200px;">
                                                  <asp:Button ID="btnOfficeComment" runat="server" OnClick="btnOfficeComment_OnClick" Text="Save" CssClass="btn" />
                                              </td>
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
    <div style="padding:5px; text-align:center">
        <asp:Button runat="server" ID="btnClose1" OnClientClick="return closewin();" Text="Close" CssClass="btn" />
    </div>    
    </form>
</body>
</html>
