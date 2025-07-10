<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_AddUnPlanJob.aspx.cs" Inherits="Popup_AddUnPlanJob" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function fncInputNumericValuesOnly(evnt) {
             if (!( event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                 event.returnValue = false;
             }
         }
   function openaddsparewindow(CompCode, VC, SpareId, Office_Ship) {

            window.open('Ship_AddEditSpares.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SPID=' + SpareId + '&&OffShip=' + Office_Ship, '', 'status=1,scrollbars=0,toolbar=0,menubar=0');

        }
   function refreshparent()
   {
     window.opener.reloadunits();
   }
   function reloadunits()
   {
      __doPostBack('btnRefresh', '');
   }
   function OpenReport()
   {
        //window.open('~/Reports/Office_BreakdownDefectReport.aspx?DN=''&fm=', '', '')
   }
    </script>
    <style type="text/css">
        .style6
        {
            height: 34px;
        }
        .style7
        {
            width: 145px;
        }
    </style>
</head>
<body style=" margin:10px;">
    <form id="form1" runat="server">
    <div style="text-align: center;font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:Panel ID="Panel1" runat="server" >
            <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: gray 1px solid; text-align:center; border-collapse:collapse" width="100%">
            <tr>
                <td align="center"  class="text headerband" >                     
                    Add/Edit Random Job </td>
            </tr>
            <tr>
                <td align="left">
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <asp:UpdatePanel runat="server" id="UpdatePanel1">
                        <ContentTemplate>                       
                        <asp:HiddenField runat="server" ID="hfdUPId" />
                        <table border="1" cellpadding="0" cellspacing="0" style="width:100%">
                        <tr>
                          <td align="left">
                                <asp:Panel runat="server" ID="p1">
                               <table cellpadding="4" cellspacing="0" style="width:100%;">
                                 <tr>   
                                     <td style="text-align:left;font-weight:bold;width:120px;">Component Code&nbsp;</td>
                                     <td style="text-align:left;font-weight:bold;width:250px;">Component Name&nbsp;</td>
                                     <td style="text-align:left;font-weight:bold;" class="style7">Component Category&nbsp;</td>
                                     <td style="text-align:left;font-weight:bold;">&nbsp;</td>
                                 </tr>
                                 <tr>
                                    <td style="text-align:left;" class="style6"><asp:Label ID="lblCompCode" runat="server"></asp:Label></td>
                                    <td style="text-align:left;" class="style6"><asp:Label ID="lblCompName" runat="server"></asp:Label></td>
                                    <td style="text-align:left;" class="style6" colspan="2">
                                        <asp:CheckBoxList ID="chkCritical" runat="server" Enabled="false" RepeatDirection="Horizontal" >
                                            <asp:ListItem Text="ClassEquipment" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="CriticalEquipment" Value="2"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>        
                                   <tr>
                                       <td style="text-align:left;">
                                           <b>Department :</b></td>
                                       <td style="text-align:left;">
                                            <asp:DropDownList runat="server" required='yes' ID="ddlDepartment" Width="100px"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="ddlDepartment" ErrorMessage="*"></asp:RequiredFieldValidator>
                                       </td>
                                       <td style="text-align:left;" class="style7">
                                           <b>Assigned To :</b></td>
                                       <td style="text-align:left;">
                                           <asp:DropDownList ID="ddlRank" runat="server" required="yes" Width="100px">
                                           </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                               ControlToValidate="ddlRank" ErrorMessage="*"></asp:RequiredFieldValidator>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="text-align:left;">
                                           <b>Due Date :</b></td>
                                       <td style="text-align:left;">
                                            <asp:TextBox ID="txtDueDate" required='yes' MaxLength="11" Width="75px" runat="server" ></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtDueDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <asp:CalendarExtender ID="CalendarExtender1" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtDueDate" TargetControlID="txtDueDate"></asp:CalendarExtender>
                                           &nbsp;</td>
                                       <td style="text-align:left;" class="style7">
                                           <b>Job Description (Short) :</b></td>
                                       <td style="text-align:left;">
                                           <asp:TextBox ID="txtShort" runat="server" MaxLength="1000" required="yes" 
                                               Width="97%"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                               ControlToValidate="txtShort" ErrorMessage="*"></asp:RequiredFieldValidator>
                                       </td>
                                   </tr>
                                     <tr>
                                        <td colspan="4">
                                            <b>Job Description (Long) :</b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtLong" runat="server" Height="71px" MaxLength="1000" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                          </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <table style="width:100%;">
                                    <tr runat="server" id="trOthers">
                                        <td style="text-align:right; padding-right:7px;">
                                        <div class="text headerband"> Job Execution </div>
                                        <div style="width:49%; float:left">
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <tr>
                                               <td style="text-align:left;width :163px;">Done Date :</td>
                                               <td style="text-align:left;">
                                                <asp:TextBox ID="txtDoneDate" required='yes' MaxLength="11" Width="75px" runat="server" ></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtDoneDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <asp:CalendarExtender ID="CalendarExtender2" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtDoneDate" TargetControlID="txtDoneDate"></asp:CalendarExtender>
                                               </td>
                                            </tr>
                                                <tr>
                                                    <td style="text-align:left;width :163px;">
                                                        Crew # :</td>
                                                    <td style="text-align:left;">
                                                        <asp:TextBox ID="txtEmpNo" runat="server" MaxLength="6" required="yes" Width="75px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtEmpNo" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:left;width :163px;">
                                                        Crew Name :</td>
                                                    <td style="text-align:left;">
                                                        <asp:TextBox ID="txtEmpName" runat="server" MaxLength="50" required="yes" Width="250px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtEmpName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                  <tr>
                                               <td style="text-align:left">Condition Before :&nbsp;</td>
                                               <td style="text-align:left"><asp:TextBox ID="txtCondBefore" MaxLength="50" Width="350px" runat="server"></asp:TextBox>
                                               </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Condition After :&nbsp;</td>
                                               <td style="text-align:left"><asp:TextBox ID="txtCondAfter" MaxLength="50" Width="350px" runat="server"></asp:TextBox>
                                               </td>
                                            </tr>
                                            </table>
                                        </div>
                                        <div style="width:49%; float:right">
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; ">
                                              <tr>
                                               <td style="text-align:left; width:172px; ">Service Report :&nbsp;</td>
                                               <td style="text-align:left">
                                                   <asp:TextBox ID="txtServiceReport" TextMode="MultiLine" MaxLength="5000" runat="server" Height="110px" Width="350px" ></asp:TextBox>                                                       
                                               </td>
                                            </tr>
                                          
                                            </table>
                                        </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right; padding-right:7px;">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn" onclick="Save_Click" Text="Save" />
                                                <asp:Button ID="btnSave2" runat="server" CssClass="btn" onclick="Save2_Click" Text="Save" />
                                        </td>
                                    </tr>
                                   <tr runat="server" id="trSpare">
                                    <td>
                                      <table cellpadding="4" cellspacing="0"  style="width:100%;border-collapse:collapse" border="1">
                                          <tr>
                                          <td>
                                          <b>&nbsp;SPARES REQUIRED?&nbsp;</b>
                                          </td>
                                          <td>  <asp:DropDownList ID="ddlSparesReqd" runat="server" AutoPostBack="true" 
                                                  onselectedindexchanged="ddlSparesReqd_SelectedIndexChanged" Width="100px">
                                                  <asp:ListItem Selected="True" Text="&lt; SELECT &gt;" Value="0"></asp:ListItem>
                                                  <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                  <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                              </asp:DropDownList></td>
                                          <td style="text-align:left;font-weight:bold;width:70px;">
                                                      RQN NO :&nbsp;</td>
                                                  <td style="text-align:left;width:100px;">
                                                      <asp:TextBox ID="txtRqnNo" runat="server" MaxLength="20"></asp:TextBox>
                                                  </td>
                                                  <td>
                                                      &nbsp;</td>
                                                  <td style="text-align:left;width:80px; font-weight:bold">
                                                      RQN DATE :&nbsp;</td>
                                                  <td style="text-align:left;">
                                                      <asp:TextBox ID="txtRqnDt" runat="server" MaxLength="15" Width="90px"></asp:TextBox>
                                                      <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                                                          Format="dd-MMM-yyyy" PopupButtonID="txtRqnDt" PopupPosition="TopLeft" 
                                                          TargetControlID="txtRqnDt">
                                                      </asp:CalendarExtender>
                                                  </td>
                                          </tr>
                                          </table>
                                       <table ID="tblSpares" runat="server" cellpadding="0" cellspacing="0" style="background-color: #f9f9f9; padding-top:3px;border-collapse:collapse" visible="false" width="100%" border="1">
                                          <tr>
                                              <td>
                                                  <asp:UpdatePanel ID="upSpares" runat="server">
                                                      <ContentTemplate>
                                                          <table cellpadding="0" cellspacing="0" width="100%" id="tblAddSpareSection" runat="server">
                                                              <tr>
                                                                  <td style="text-align:right; vertical-align:middle;">
                                                                      Select Spare :&nbsp;<asp:DropDownList ID="ddlSparesList" runat="server" 
                                                                          AutoPostBack="true" onselectedindexchanged="ddlSparesList_SelectedIndexChanged" 
                                                                          Width="250px">
                                                                      </asp:DropDownList>
                                                                      &nbsp;<asp:ImageButton ID="imgAddSpare" runat="server" ImageUrl="~/Modules/HRD/Images/add.png" 
                                                                          onclick="imgAddSpare_Click" />
                                                                  </td>
                                                                  <td style="text-align:right; vertical-align:middle;">
                                                                      Qty(Consumed) :&nbsp;</td>
                                                                  <td style="text-align:left; vertical-align:middle;">
                                                                      <asp:TextBox ID="txtQtyCon" runat="server" MaxLength="5" 
                                                                          onkeypress="fncInputNumericValuesOnly(event)" Width="50px"></asp:TextBox>
                                                                  </td>
                                                                  <td style="text-align:right; vertical-align:middle;">
                                                                      Qty(ROB) :&nbsp;</td>
                                                                  <td style="text-align:left; vertical-align:middle;">
                                                                      <asp:TextBox ID="txtQtyRob" runat="server" MaxLength="5" 
                                                                          onkeypress="fncInputNumericValuesOnly(event)" Width="50px"></asp:TextBox>
                                                                  </td>
                                                                  <td style="vertical-align:middle;">
                                                                      <asp:Button ID="btmCancelSpareAddSection" runat="server" onclick="btmCancelSpareAddSection_Click" Text="Cancel" CssClass="btn"  style="float:right;" />
                                                                      <asp:Button ID="btnAddSpare" runat="server" CssClass="btn" onclick="btnAddSpare_Click" style="width:100px;float:right;margin-right:3px;" Text="Add Spare" />
                                                                      

                                                                      <asp:Button ID="btnRefresh" runat="server" onclick="btnRefresh_Click" style="display:none" />
                                                                  </td>
                                                              </tr>
                                                          </table>
                                                      </ContentTemplate>
                                                  </asp:UpdatePanel>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td>
                                                  <table border="1" cellpadding="4" cellspacing="0" rules="all" 
                                                      style="width: 100%;border-collapse: collapse;">
                                                      <colgroup>
                                                          <col />
                                                          <col style="width: 90px;" />
                                                          <col style="width: 100px" />
                                                          <col style="width: 90px;" />
                                                          <col style="width: 40px;" />
                                                          <col style="width: 17px;" />
                                                          <tr align="left" class= "headerstylegrid">
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
                                                              <td>
                                                              </td>
                                                              <td>
                                                              </td>
                                                          </tr>
                                                      </colgroup>
                                                  </table>
                                                  
                                                  <div ID="dvSpares" class="scrollbox" onscroll="SetScrollPos(this)" style="overflow-y: scroll;
                                                overflow-x: hidden; width: 100%; height: 100px; text-align: center;">
                                                      <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;
                                                    border-collapse: collapse;">
                                                          <colgroup>
                                                              <col />
                                                              <col style="width: 90px;" />
                                                              <col style="width: 100px" />
                                                              <col style="width: 90px;" />
                                                              <col style="width: 40px;" />
                                                              <col style="width: 17px;" />
                                                          </colgroup>
                                                          <asp:Repeater ID="rptComponentSpares" runat="server">
                                                              <ItemTemplate>
                                                                  <tr class="row" visible='<%#Eval("SpareId").ToString() != "" %>'>
                                                                      <td align="left">
                                                                          <%#Eval("SpareName")%>
                                                                          <asp:HiddenField ID="hfdComponentId" runat="server" 
                                                                              Value='<%#Eval("ComponentId") %>' />
                                                                          <asp:HiddenField ID="hfOffice_Ship" runat="server" 
                                                                              Value='<%#Eval("Office_Ship") %>' />
                                                                          <asp:HiddenField ID="hfSpareId" runat="server" Value='<%#Eval("SpareId") %>' />
                                                                          <span style="font-size:9px;color:blue;"><i><%#Eval("Maker")%></i></span>
                                                                      </td>
                                                                      <td align="left">
                                                                          <%#Eval("PartNo")%>
                                                                      </td>
                                                                      <td align="center">
                                                                          <asp:Label ID="lblQtyCons" runat="server" Text='<%#Eval("QtyCons")%>'></asp:Label>
                                                                          <td align="center">
                                                                              <asp:Label ID="lblQtyRob" runat="server" Text='<%#Eval("QtyRob")%>'></asp:Label>
                                                                          </td>
                                                                          <td>
                                                                              <% if(Modify)                                                                                  
                                                                                  { %>
                                                                                        <table cellpadding="2" cellspacing="0" border="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="imgEdit1" runat="server" CommandArgument='<%#Eval("RowId") %>' Height="12px" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEditSpare_OnClick" ToolTip="Edit"  />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="imgDel1" runat="server" CommandArgument='<%#Eval("RowId") %>' Height="12px" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDel_OnClick"  OnClientClick="javascript:return confirm('Are you sure to delete?');" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        
                                                                              <%} else
                                                                                  {%>
                                                                                        
                                                                                    <% if (Request.QueryString["DN"] == null)
                                                                                                   { %>
                                                                                      <table cellpadding="2" cellspacing="0" border="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="imgEdit" runat="server"  Visible='<%#(Session["UserType"]=="S" && txtDoneDate.Text.Trim()=="")%>' CommandArgument='<%#Eval("RowId") %>' Height="12px" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEditSpare_OnClick" ToolTip="Edit"  />
                                                                                            </td>
                                                                                            <td>
                                                                                                  <asp:ImageButton ID="imgDel" runat="server" 
                                                                                                      CommandArgument='<%#Eval("RowId") %>' Height="12px" Visible='<%#(Session["UserType"]=="S" && txtDoneDate.Text.Trim()=="")%>'
                                                                                                      ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDel_OnClick" 
                                                                                                      OnClientClick="javascript:return confirm('Are you sure to delete?');" />
                                                                                                     
                                                                                            </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                       <%} %>
                                                                                                                                                   
                                                                              <%} %>
                                                                           
                                                                          </td>
                                                                          <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                                      </td>
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
                                    <tr id="trAttachments" runat="server">
                                        <td>
                                                    <table cellpadding="0" cellspacing="0" visible="true" style="background-color: #f9f9f9; padding-top:3px;" width="100%">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
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
                                                              CssClass="btn" runat="server" 
                                                              style="width:120px;  float:right;" 
                                                              onclick="btnSaveAttachment_Click" /></td>
                                                 </tr>
                                             </table>
                                             </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSaveAttachment" />
                                                </Triggers>
                                           </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                                <colgroup>
                                                        <col  />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 17px;" />
                                                    <tr align="left" class= "headerstylegrid">
                                                        <td>
                                                            Attachment Text
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
                                                                   <a runat="server" ID="ancFile"  href='<%# ProjectCommon.getLinkFolder(DateTime.Parse(txtDoneDate.Text.Trim())) + Eval("FileName").ToString()  %>' target="_blank" Visible='<%# Eval("FileName").ToString() != "" %>' title="Attachment" >
                                                                    <img src="Images/paperclip.gif" style="border:none"  />
                                                                   </a>
                                                                </td>
                                                                <td>
                                                                <asp:ImageButton runat="server" ID="btnDelAttachment" ImageUrl="~/Modules/HRD/Images/delete.jpg" Height="12px" Visible='<%#(Session["UserType"]=="S")%>' OnClick="DeleteAttachment_OnClick" title='Delete Attachment' CssClass='<%#Eval("VesselCode")%>' CommandArgument='<%#Eval("TableId")%>' OnClientClick="javascript:confirm('Are you sure to remove this attachment?');" />
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
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </td> 
        </tr>
        </table>
        </asp:Panel>
     </div>
    </form>
</body>
</html>
