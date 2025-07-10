<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobCard.aspx.cs" Inherits="JobCard"  EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script>
        function openaddsparewindow(CompCode, VC, SpareId, Office_Ship) {

            window.open('Ship_AddEditSpares.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SPID=' + SpareId + '&&OffShip=' + Office_Ship, '', 'status=1,scrollbars=0,toolbar=0,menubar=0');

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="height: 23px; text-align :center; padding-top :3px;" class="pagename" colspan="2" >                     
                    <asp:Label ID="lblPagetitle" runat="server" ></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <div style="width:100%; height:440x; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-top:5px;"></td>
                        </tr>
                        <tr>
                          <td>
                              <asp:Panel ID="plUpdateJobs" runat="server">
                                <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                   <%--<tr>
                                    <td colspan="2" style=" height:15px; vertical-align :middle; background-color : #C2C2C2; text-align :left">
                                    <span style=" font-size :13px; font-weight :bold;">
                                    Job Description 
                                    </span>
                                    </td>
                                   </tr>--%>
                                     <tr>
                                            <td colspan="2">
                                        
                                                 <table border="0" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                        <tr>
                                                            <%--<td style="text-align:right; font-weight:bold">Vessel :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblUpdateVessel" runat="server"></asp:Label> </td>
                                                            <td>&nbsp;</td>--%>
                                                            <td style="text-align:right;width:150px;  font-weight:bold">Component :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblUpdateComponent" runat="server"></asp:Label> </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                            <td style="text-align:right;width:150px; font-weight:bold">&nbsp;</td>
                                                            <td style="text-align:left;width:50px;">&nbsp;</td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right;width:150px;  font-weight:bold">
                                                                Interval :&nbsp;</td>
                                                            <td style="text-align:left;">
                                                                <asp:Label ID="lblUpdateInterval" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width:5px;">
                                                                &nbsp;</td>
                                                            <td style="text-align:right;width:100px; font-weight:bold">
                                                                &nbsp;</td>
                                                            <td style="text-align:left;width:50px;">
                                                                &nbsp;</td>
                                                            <td style="width:5px;">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                           <td style="text-align:right; width:150px; font-weight:bold">Job :&nbsp;</td>
                                                           <td colspan="4" style="text-align:left; "><asp:Label ID="lblUpdateJob" runat="server"></asp:Label> </td>
                                                           <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align:right; width:150px; font-weight:bold">
                                                                <span lang="en-us">Job Description :</span></td>
                                                            <td colspan="4" style="text-align:left; ">
                                                                <asp:Label ID="lblJobDescr" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width:5px;">&nbsp;</td>
                                                        </tr>
                                                 </table>
                                            </td>
                                        </tr>
                                        <tr>
                                    <td colspan="2" style=" height:15px; vertical-align :middle; background-color : #C2C2C2; text-align :left">
                                    <span style=" font-size :13px; font-weight :bold;">
                                    Maintenance Details
                                    </span>
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
                                               <td style="text-align:left;"><%--<asp:Label ID="lblEmpNo" runat="server" ></asp:Label>
                                                           &nbsp;/&nbsp;<asp:Label ID="lblEmpName" runat="server" ></asp:Label>--%>
                                                           <asp:TextBox ID="txtEmpNo" MaxLength="6" required='yes' Width="75px" runat="server" ></asp:TextBox>
                                                           &nbsp;<asp:TextBox ID="txtEmpName" MaxLength="50" required='yes' Width="148px" runat="server" ></asp:TextBox>
                                                       </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Rank :&nbsp;</td>
                                               <td style="text-align:left"><%--<asp:Label ID="lblRank" runat="server" ></asp:Label>--%>
                                               <asp:DropDownList ID="ddlRank" required='yes' runat="server" ></asp:DropDownList></td>                                               
                                               </td>
                                               
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Maintenance Reason :&nbsp;</td>
                                               <td style="text-align:left"><%--<asp:Label ID="lblRemarks" runat="server" ></asp:Label>
                                                  <br />
                                                  <asp:RadioButtonList ID="rdoBreakdownReason" RepeatDirection="Horizontal" runat="server" Visible="false">
                                                   <asp:ListItem Text="Equipment Working" Value="1"></asp:ListItem>
                                                   <asp:ListItem Text="Equipment Not Working" Value="2"></asp:ListItem>
                                                  </asp:RadioButtonList>     --%>
                                                  <asp:DropDownList ID="ddlRemarks" required='yes' AutoPostBack="true" OnSelectedIndexChanged="ddlRemarks_SelectedIndexChanged" Width="135px" runat="server" >
                                                  <asp:ListItem Text="< SELECT >" Value="0" Selected="True"></asp:ListItem>
                                                  <asp:ListItem Text="Planned Job" Value="1"></asp:ListItem>
                                                  <asp:ListItem Text="Break Down" Value="3"></asp:ListItem>
                                                  <asp:ListItem Text="Specify" Value="2"></asp:ListItem>
                                                  </asp:DropDownList> 
                                                  <br />
                                                  <asp:RadioButtonList ID="rdoBreakdownReason" RepeatDirection="Horizontal" runat="server">
                                                   <asp:ListItem Text="Equipment Working" Value="1"></asp:ListItem>
                                                   <asp:ListItem Text="Equipment Not Working" Value="2"></asp:ListItem>
                                                  </asp:RadioButtonList>
                                               </td>
                                            </tr>
                                            <tr id="trSpecify" runat="server" visible="false">
                                                <td style="text-align:left">Specify :&nbsp;</td>
                                                <td style="text-align:left"><%--<asp:Label ID="lblSpecify" runat="server" ></asp:Label>--%>
                                                <asp:TextBox ID="txtSpecify" required='yes' MaxLength="50" Width="235px" runat="server" ></asp:TextBox>
                                                </td>
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
                                                        <td style="text-align:left">Last Hr. Done </td>
                                                        <td style="text-align:left"> Current Hr. Done</td>
                                                        <td style="text-align:left;display:none;">Next Hr. Due </td>
                                                      </tr>
                                                      <tr style="padding-top:5px;">
                                                         <td style="text-align:left;" ><%--<asp:Label ID="lblLastHour" runat="server" ></asp:Label>--%>
                                                         <asp:TextBox ID="txtLastHour" ReadOnly="true" Width="55px" runat="server" ></asp:TextBox>
                                                         </td>
                                                         <td style="text-align:left;"><%--<asp:Label ID="lblDoneHour" runat="server" ></asp:Label>--%>
                                                         <asp:TextBox ID="txtDoneHour" required='yes' MaxLength="11" Width="60px" runat="server" AutoPostBack="True" ontextchanged="txtDoneHour_TextChanged" ></asp:TextBox>
                                                         </td>
                                                         <td style="text-align:left;display:none;"><%--<asp:Label ID="lblNextHour" runat="server" ></asp:Label>--%>
                                                         <asp:TextBox ID="txtNextHour" ReadOnly="true" Width="55px" runat="server" ></asp:TextBox>
                                                         </td>
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
                                                         <td style="text-align:left;display:none;">Next Due Date</td>
                                                      </tr> 
                                                      <tr>  
                                                         <td style="text-align:left"><%--<asp:Label ID="lblDuedt" runat="server"></asp:Label>--%>
                                                         <asp:TextBox ID="txtDuedt" Width="75px" runat="server"></asp:TextBox>
                                                         </td>
                                                         <td style="text-align:left;"><%--<asp:Label ID="lblDoneDate" runat="server" ></asp:Label>--%>
                                                          <asp:TextBox ID="txtDoneDate" required='yes' MaxLength="11" Width="75px" runat="server" AutoPostBack="True" OnTextChanged="txtDoneDate_TextChanged"  ></asp:TextBox>
                                                          <asp:CalendarExtender ID="CalendarExtender1" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtDoneDate" TargetControlID="txtDoneDate"></asp:CalendarExtender>
  
                                                         </td>
                                                         <td style="text-align:left;display:none; "><%--<asp:Label ID="lblNextDueDt" runat="server"></asp:Label>--%>
                                                         <asp:TextBox ID="txtNextDueDt" Width="85px" runat="server"></asp:TextBox>
                                                         </td>
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
                                                    <%--<asp:TextBox ID="lblServiceReport" TextMode="MultiLine" MaxLength="5000" runat="server" Height="145px" Width="350px" ></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtServiceReport" required='yes' TextMode="MultiLine" MaxLength="5000" runat="server" Height="110px" Width="350px" ></asp:TextBox>                                                    
                                                       </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Condition Before :&nbsp;</td>
                                               <td style="text-align:left"><%--<asp:Label ID="lblCondBefore" runat="server"></asp:Label>--%>
                                               <asp:TextBox ID="txtCondBefore" required='yes' MaxLength="50" Width="350px" runat="server"></asp:TextBox>
                                               <asp:HiddenField ID="hfIntervalId_H" runat="server" />
                                               <asp:HiddenField ID="hfInterval_H" runat="server" />
                                               </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Condition After :&nbsp;</td>
                                               <td style="text-align:left"><%--<asp:Label ID="lblCondAfter" runat="server"></asp:Label>--%>
                                               <asp:TextBox ID="txtCondAfter" required='yes' MaxLength="50" Width="350px" runat="server"></asp:TextBox>
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
                                    <tr>
                                       <td colspan="2" style="text-align:right;">
                                       <asp:Label ID="lblSaveMsg" style="color:Red;" runat="server"></asp:Label>
                                        <asp:Button ID="btnSave" Text="Update" OnClick="btnSave_Click" 
                                               CssClass="btnorange" runat="server" 
                                               style="width:120px; height:20px; background-color:orange;" />
                                       </td>
                                    </tr>
                                   <tr id="trRatingsHeader" runat="server" visible="false">
                                   <td colspan="2" style=" height:15px; vertical-align :middle; background-color : #C2C2C2; text-align :left">
                                   <span style=" font-size :13px; font-weight :bold;">
                                   Ratings
                                   </span>
                                   </td>
                                   </tr>
                                   <tr id="trRating" runat="server" visible="false">
                                        <td colspan="2" style="text-align:left;">
                                          <table cellpadding="2" cellspacing="0" width="100%">
                                             <tr>
                                                 <td style="text-align:right">Coating :&nbsp;</td>
                                                 <td style="text-align:left">
                                                      <asp:Label ID="lblCoating" runat="server"></asp:Label>
                                                 </td>
                                                 <td style="text-align:right">Corrosion :&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:Label ID="lblCorrosion" runat="server"></asp:Label>
                                                 </td>
                                                 <td style="text-align:right">Deformation :&nbsp;</td>
                                                 <td style="text-align:left">
                                                      <asp:Label ID="lblDeformation" runat="server"></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td style="text-align:right">Cracks :&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:Label ID="lblCracks" runat="server"></asp:Label>
                                                 </td>
                                                 <td style="text-align:right">Overall Rating :&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:Label ID="lblOAllRating" runat="server"></asp:Label>
                                                 </td>
                                                 <td></td>
                                                 <td></td>
                                             </tr>
                                          </table>
                                        </td>
                                    </tr>
                                   <tr>
                                   <td colspan="2" style=" height:15px; vertical-align :middle; background-color : #C2C2C2; text-align :left">
                                   <span style=" font-size :13px; font-weight :bold;">
                                   Consumed Spares 
                                   </span>
                                   </td>
                                   </tr>
                                   <tr>
                                        <td colspan="2" style="text-align:left;">
                                            <%-- Add spare here 1111111111111--%>

                                            <%if ( (Session["UserType"].ToString() == "S" && (!IsVerified) ) || (Session["UserType"].ToString() == "S" && Modify)     )  %>
                                            <%{ %>
                                                <table  cellpadding="2" cellspacing="0" width="100%" >
                                                 <tr>
                                                      <td style="text-align:right; vertical-align:middle;">Select Spare :&nbsp;
                                                          <asp:DropDownList ID="ddlSparesList" AutoPostBack="true" OnSelectedIndexChanged="ddlSparesList_SelectedIndexChanged" runat="server" Width="250px"></asp:DropDownList>&nbsp;
                                                          <asp:ImageButton ID="imgAddSpare" ImageUrl="~/Modules/PMS/Images/add.png" runat="server" onclick="imgAddSpare_Click"/>  </td>
                                                      <td style="text-align:right; vertical-align:middle;">Qty(Cons) :&nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;"><asp:TextBox ID="txtQtyCon" runat="server" Width="30px" onkeypress="fncInputNumericValuesOnly(event)" MaxLength="5" ></asp:TextBox></td>
                                                      <td style="text-align:right; vertical-align:middle;">Qty(ROB) :&nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;"><asp:TextBox ID="txtQtyRob" runat="server" Width="30px" onkeypress="fncInputNumericValuesOnly(event)" MaxLength="5" ></asp:TextBox></td>
                                                      <td style="vertical-align:middle;">
                                                          <asp:Button ID="btnClearSpare" Text=" Clear " CssClass="btnorange" runat="server" style="width:120px; height:20px; background-color:orange; float:right;" onclick="btnClearSpare_Click" />
                                                          <asp:Button ID="btnAddSpare" Text=" Add " CssClass="btnorange" runat="server" style="width:120px; height:20px; background-color:orange; float:right;" onclick="btnAddSpare_Click" />

                                                          

                                                          <asp:Button ID="btnRefresh" runat="server" onclick="btnRefresh_Click" style="display:none" /></td>
                                                         
                                                 </tr>
                                                <tr>
                                                    <td colspan="6" style="text-align:center;">
                                                        <asp:Label ID="lblMSgSpare" style="color:Red;" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                             </table>
                                            <%} %>


                                          <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                                <colgroup>
                                                        <col style="width: 30px"/>
                                                    <col style="width: 30px"/>
                                                        <col  />
                                                        <col style="width: 150px"/>
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 17px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td></td>
                                                            <td></td>
                                                        <td>
                                                            Name
                                                        </td>
                                                        <td>
                                                            Part#
                                                        </td>
                                                        <td>
                                                            Qty(Cons.)
                                                        </td>
                                                        <td>
                                                            Qty(ROB)
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            <%--<div id="divAttachment" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 130px; text-align: center;">--%>
                                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;
                                                    border-collapse: collapse;">
                                                    <colgroup>
                                                        <col style="width: 30px"/>
                                                        <col style="width: 30px"/>
                                                        <col  />
                                                        <col style="width: 150px"/>
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 17px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptSpares" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="left">
                                                                        <%if (Session["UserType"].ToString() == "S" && (!IsVerified)  || (Session["UserType"].ToString() == "S" && Modify)  )  %>
                                                                        <%{ %>
                                                                            <asp:ImageButton ID="imgDeleteSpare" runat="server" ImageUrl="~/Modules/PMS/Images/Delete.png" OnClick="imgDeleteSpare_OnClick" OnClientClick="return confirm('Are you sure to delete?');" />
                                                                        <%} %>
                                                                    </td>
                                                                <td align="left">
                                                                    <%if (Session["UserType"].ToString() == "S" && (!IsVerified) || (Session["UserType"].ToString() == "S" && Modify)  )  %>
                                                                        <%{ %>
                                                                            <asp:ImageButton ID="imgEditSpare" runat="server" ImageUrl="~/Modules/PMS/Images/edit.png" OnClick="imgEditSpare_OnClick" />
                                                                        <%} %>
                                                                </td>
                                                                <td align="left">
                                                                    <%#Eval("SpareName")%>
                                                                    <br />
                                                                    <span style="font-size:9px;color:blue;"><i><%#Eval("Maker")%></i></span>
                                                                    <asp:HiddenField ID="SparePK" runat="server" Value='<%#Eval("PKID")%>' />
                                                                    <asp:HiddenField ID="hfHistoryId" runat="server" Value='<%#Eval("HistoryId")%>' />
                                                                    
                                                                </td>
                                                                
                                                                <td align="left">
                                                                    <%#Eval("PartNo")%>
                                                                </td>
                                                                <td align="center">
                                                                   <asp:Label ID="lblQtyCons" Text='<%#Eval("QtyCons")%>' runat="server"></asp:Label>
                                                                <td align="center">
                                                                   <asp:Label ID="lblQtyRob" Text='<%#Eval("QtyRob")%>' runat="server"></asp:Label>
                                                                </td>
                                                                <td></td>
                                                                <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            <%--</div>--%>
                                        </td>
                                    </tr>
                                    
                                   <tr id="trAttachmentHeader" runat="server" visible="false">
                                   <td colspan="2" style=" height:15px; vertical-align :middle; background-color : #C2C2C2; text-align :left">
                                   <span style=" font-size :13px; font-weight :bold;">
                                   Attachments 
                                   </span>
                                   </td>
                                   </tr>
                                       <tr id="trAddAttachments" runat="server">
                                       <td colspan="2">
                                              <table cellpadding="2" cellspacing="0" width="100%">
                                                 <tr>
                                                      <td style="text-align:right; vertical-align:middle;">Description :&nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;">
                                                          <asp:TextBox ID="txtAttachmentText" runat="server" MaxLength="500" 
                                                              Width="470px" ></asp:TextBox></td>
                                                      <td style="text-align:right; vertical-align:middle;">Attachment :&nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;">
                                                      <asp:FileUpload ID="flAttachDocs" runat="server" />
                                                      </td>
                                                      <td style="vertical-align:middle;"><asp:Button ID="btnSaveAttachment" Text="Upload" 
                                                              CssClass="btnorange" runat="server" 
                                                              style="width:120px; height:20px; background-color:orange; float:right;" 
                                                              onclick="btnSaveAttachment_Click" /></td>
                                                 </tr>
                                             </table>
                                       </td>
                                    </tr>
                                   <tr id="trAttachment" runat="server" visible="false">
                                        <td colspan="2" style="text-align:left;">
                                          <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                                <colgroup>
                                                        <col  />
                                                         <col style="width: 30px;" />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 17px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td>
                                                            Description
                                                        </td>
                                                        <td></td>
                                                        <td style='width:17px'></td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            <%--<div id="divAttachment" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 130px; text-align: center;">--%>
                                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;
                                                    border-collapse: collapse;">
                                                    <colgroup>
                                                        <col  />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 17px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptAttachment" runat="server">
                                                        <ItemTemplate>
                                                            <tr >
                                                                <td align="left">
                                                                    <%#Eval("AttachmentText")%>
                                                                    <asp:HiddenField ID="hfFileName" runat="server" Value='<%#Eval("FileName")%>' />
                                                                </td>
                                                                <td>
                                                                   <a runat="server" ID="ancFile"  href='<%# ProjectCommon.getLinkFolder(DateTime.Parse(txtDoneDate.Text.Trim())) + Eval("FileName").ToString()  %>' target="_blank" Visible='<%# Eval("FileName").ToString() != "" %>' title="Show Attachment" >
                                                                    <img src="Images/paperclip.gif" style="border:none"  />
                                                                   </a>
                                                                </td>
                                                                <td>
                                                                <asp:ImageButton runat="server" ID="btnDelAttachment" ImageUrl="~/Modules/PMS/Images/delete.png" Height="12px" OnClick="DeleteAttachment_OnClick" title='Delete Attachment' CssClass='<%#Eval("VesselCode")%>' CommandArgument='<%#Eval("TableId")%>' OnClientClick="javascript:confirm('Are you sure to remove this attachment?');" Visible='<%#(Session["UserType"].ToString() == "S" && (!IsVerified)) %>' />
                                                                </td>
                                                                <td style='width:17px'></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            <%--</div>--%>
                                        </td>
                                    </tr>
                                   <tr>
                                      <td colspan="2" style=" height:15px; vertical-align :middle; background-color : #C2C2C2; text-align :left">
                                         <span style=" font-size :13px; font-weight :bold;">
                                          Ship Verification
                                         </span>
                                      </td>
                                   </tr> 
                                   <tr>
                                     <td colspan="2" style="text-align:left;"> 
                                         <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                          <tr>
                                             <td style="text-align:right; width:150px;">Verified By/On :&nbsp;</td>
                                             <td style="text-align:left"><asp:Label ID="lblVerified" runat="server"></asp:Label></td> 
                                          </tr>
                                          <tr id="trShipVerification" runat="server" visible="false">
                                             <td style="text-align:right; width:150px;"></td>
                                             <td style="text-align:left"><asp:CheckBox runat="server" Text="Verify" ID="chkVerifed" AutoPostBack="true" OnCheckedChanged="chkVerifed_OnCheckedChanged"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="lblVerifyNote" Text="*NOTE : Please Check running hrs." ForeColor="Red" Font-Bold="true" Visible="false" runat="server"></asp:Label>  </td>
                                          </tr>
                                         </table>
                                     </td> 
                                   </tr> 
                                    <tr id="trOffVerifyLabel" runat="server">
                                      <td colspan="2" style=" height:15px; vertical-align :middle; background-color : #C2C2C2; text-align :left">
                                         <span style=" font-size :13px; font-weight :bold;">
                                          Office Verification
                                         </span>
                                      </td>
                                   </tr> 
                                    <tr id="trOffVerify" runat="server">
                                     <td colspan="2" style="text-align:left;"> 
                                         <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                          <tr>
                                             <td style="text-align:right; width:150px;">Verified By/On :&nbsp;</td>
                                             <td style="text-align:left"><asp:Label ID="lblOfficeVerified" runat="server"></asp:Label></td> 
                                          </tr>
                                          <tr>
                                            <td style="text-align:right; width:150px;">Remark :&nbsp;</td>
                                            <td style="text-align:left"><asp:Label ID="lblRemark" runat="server"></asp:Label></td> 
                                          </tr>
                                         </table>
                                     </td> 
                                   </tr>
                                   <tr id="trOffCommLabel" runat="server">
                                      <td colspan="2" style=" height:15px; vertical-align :middle; background-color : #C2C2C2; text-align :left">
                                         <span style=" font-size :13px; font-weight :bold;">
                                          Office Comments
                                         </span>
                                      </td>
                                   </tr> 
                                    <tr id="trOffComm" runat="server">
                                     <td colspan="2" style="text-align:left;"> 
                                        <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                          <tr>
                                             <td style="text-align:right; width:150px;">Comments :&nbsp;</td>
                                             <td style="text-align:left">
                                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" style="width:80%; height:60px;" ></asp:TextBox>
                                             </td> 
                                          </tr>
                                          <tr>
                                            <td style="text-align:right; width:150px;">&nbsp;</td>
                                            <td style="text-align:left">
                                                <asp:Button ID="btnVerify" runat="server" OnClick="btnVerify_OnClick" vsl='<%#Eval("VesselCode")%>' historyid='<%#Eval("HistoryId")%>' CompName='<%#Eval("ComponentName")%>' Text="Save" CssClass="btn" />
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
                        </div>
                        </td>
                        </tr>
                    </table>
                </td>
              <td style="width:200px;">
            <div style="padding:10px;background-color:#c2c2c2">Photos Uploaded</div>
            <div style="height:500px;overflow-y:scroll;overflow-x:hidden">
                <asp:Repeater runat="server" ID="rptiamges">
                    <ItemTemplate>
                        <img src='<%#Eval("filename")%>' style="width:175px" onclick="window.open(this.src,'');"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </td>
            </tr>
        </table>
        </td> 
        </tr>
        </table>
     </div>
    <br />
    <center>
    <asp:Button ID="btnExportAttachments" runat="server" OnClick="btnSendAttachment_OnClick" Text="Export Attachments" CssClass="btn" Width="139px" />
    </center>

        <script type="text/javascript">        
                window.onunload = refreshParent;
                function refreshParent() {
                    window.opener.location.reload();
                }
            
        </script>
    </form>
</body>
</html>
