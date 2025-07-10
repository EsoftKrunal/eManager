<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_ExecuteJob.aspx.cs" Inherits="Popup_ExecuteJob" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
      function checkrhavailability()
      {
         alert('Running hour does not exist.\n Please fill running hour first.');
         window.close();
      }
      function refreshparent()
      {
         window.opener.refresh();
      }
      function openadddefects(CC,JID)
        {
           window.open('Popup_BreakDown.aspx?CC='+ CC + '&&JID='+ JID + '&&EJ=T' , '', '');
        }
      function fncInputNumericValuesOnly(evnt) {
             if (!( event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                 event.returnValue = false;
             }
         }
      function openaddsparewindow(CompCode, VC, SpareId, Office_Ship) {

            window.open('Ship_AddEditSpares.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SPID=' + SpareId + '&&OffShip=' + Office_Ship, '', 'status=1,scrollbars=0,toolbar=0,menubar=0');

        }
      function reloadunits()
       {
          __doPostBack('btnRefresh', '');
      }
      function findspares()
      {
          var c = document.getElementById('hfdcompcode').getAttribute('value');
          var c1 = document.getElementById('hfdcompid').getAttribute('value');
          window.open('FindSpares.aspx?ComponentId=' + c1 + '&ComponentCode=' + c, '');
          return false;
      }
      window.onbeforeunload = function(){if((window.event.clientX<0) || (window.event.clientY<0)){refreshparent();}}
    </script>
    <style type="text/css">
        .style2
        {
            width: 75px;
            text-align: right;
        }
        .borderd tr td
        {
            border:solid 1px #c2c2c2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:HiddenField runat="server" ID="hfdcompcode" />
        <asp:HiddenField runat="server" ID="hfdcompid" />
        
        <asp:Panel runat="server" ID="pnlUpload" Visible="true">
            <div style="position:fixed;top:0px;left:0px; bottom:0px;right:0px;background-color:#000; z-index:15;opacity:0.9;filter: alpha(opacity=90);">fasdfafsd</div>
            <div style="position:fixed;top:100px;left:100px; bottom:100px;right:100px;background-color:#fff; z-index:16;">
            <div style="font-size:15px;color:#fff;padding:10px;background-color:#e52b2b;">Following pictures are required to report this job. Please upload the attachments first to continue.</div>
            <div style=" font-size:13px;">
                <table width="100%" cellpadding="6" cellspacing="0" class="borderd" style="border-collapse:collapse">
                <asp:Repeater runat="server" ID="rptAttachments">
                    <ItemTemplate>
                        <tr>
                            <td style="width:40px"><%#Eval("SNO")%>.</td>
                            <td style="text-align:left;"><div><%#Eval("Attachmentdetails")%></div> </td>
                            <td style="width:200px">
                                <asp:FileUpload runat="server" ID="flpfile" Width="180px" CssClass='<%#Eval("AttachmentId")%>' />
                                <asp:RequiredFieldValidator runat="server" ID="rerwe" ControlToValidate="flpfile" ErrorMessage="*" ValidationGroup="ssss"></asp:RequiredFieldValidator>
                            </td>
                        </tr>                            
                    </ItemTemplate>
                </asp:Repeater>
                </table>                
            </div>
            <br />
            <div style="color:blue;padding:8px;">All file(s) must be selected to submit. Only image ( .png, .jpg, .jpeg ) files are allowed.</div>
            <asp:Button runat="server" ID="btnSumbit" Text="Submit" style="background-color:#e52b2b;color:white;border:none;padding:8px 15px 8px 15px;" ValidationGroup="ssss" OnClick="btnSumbit_Click" />
        </div>
        </asp:Panel>
        <div style="padding:3px;text-align:center;background-color:#f2ff7c" >
        <asp:Label runat="server" ID="lblfilesmessage" ForeColor="Red" Font-Size="Larger"></asp:Label>
        </div>
         <div style="height: 23px; text-align :center; padding-top :3px;" class="pagename" >Execute Job&nbsp;</div>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <%--<asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>      --%>                 
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-top:5px;"></td>
                        </tr>
                        <tr>
                          <td>
                              <asp:Panel ID="plUpdateJobs" runat="server">
                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <tr>
                                        <td colspan="2" style=" font-size :14px; color:blue; background-color :tan;">
                                                 <table border="0" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                                        <tr>
                                                            <td style="text-align:left;width:50px;  font-weight:bold">Component :&nbsp;</td>
                                                            <td style="text-align:left;"><asp:Label ID="lblUpdateComponent" runat="server"></asp:Label>
                                                            <br />
                                                              <asp:Label ID="lblParent" style ="color :Blue;font-size:10px; font-style:italic;" runat="server"></asp:Label>
                                                             </td>
                                                            <td style="font-weight:bold" class="style2">Job :&nbsp;</td>
                                                            <td style="text-align:left;"><b>[ <asp:Label ID="lblUpdateInterval" ForeColor="Red" runat="server"></asp:Label>
                                                                &nbsp;]&nbsp; </b>
                                                                <asp:Label ID="lblUpdateJob" runat="server"></asp:Label></td>
                                                        </tr>
                                                 </table>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <fieldset>
                                            <legend style="font-size:14px;color:blue"> Job Done By : </legend>
                                           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                           <%--<tr>
                                               <td style="text-align:left; font-weight:bold">Component :&nbsp;</td>
                                               <td style="text-align:left;font-weight:bold;"><asp:Label ID="lblComponent" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Job :&nbsp;</td>
                                               <td style="text-align:left"><asp:Label ID="lblJobDesc" runat="server"></asp:Label></td>
                                            </tr>--%>
                                            <tr>
                                               <td style="text-align:left;width :163px;">Emp. No &nbsp;/&nbsp;Emp. Name :</td>
                                               <td style="text-align:left;"><asp:TextBox ID="txtEmpNo" MaxLength="6" required='yes' Width="75px" runat="server" ></asp:TextBox>
                                                           &nbsp;<asp:TextBox ID="txtEmpName" MaxLength="50" required='yes' Width="148px" runat="server" ></asp:TextBox>
                                                       </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Rank :&nbsp;</td>
                                               <td style="text-align:left"><asp:DropDownList ID="ddlRank" required='yes' runat="server" ></asp:DropDownList></td>
                                               
                                            </tr>
                                            </table>
                                            </fieldset> 
                                            <fieldset>
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                            <tr>
                                               <td style="text-align:left; width :130px;">Maintenance Reason :&nbsp;</td>
                                               <td style="text-align:left"><asp:DropDownList ID="ddlRemarks" required='yes' AutoPostBack="true" OnSelectedIndexChanged="ddlRemarks_SelectedIndexChanged" Width="135px" runat="server" >
                                                  <asp:ListItem Text="< SELECT >" Value="0" Selected="True"></asp:ListItem>
                                                  <asp:ListItem Text="Planned Job" Value="1"></asp:ListItem>
                                                  <asp:ListItem Text="Break Down" Value="3"></asp:ListItem>
                                                  <asp:ListItem Text="Specify" Value="2"></asp:ListItem>
                                                  </asp:DropDownList> <asp:Button ID="btnAddDefectDetails" Text="Add Defects" 
                                                       CssClass="btnorange" Width="100px" runat="server" 
                                                       onclick="btnAddDefectDetails_Click" /><br />
                                                  <asp:RadioButtonList ID="rdoBreakdownReason" RepeatDirection="Horizontal" runat="server" Visible="false">
                                                   <asp:ListItem Text="Equipment Working" Value="1"></asp:ListItem>
                                                   <asp:ListItem Text="Equipment Not Working" Value="2"></asp:ListItem>
                                                  </asp:RadioButtonList>     
                                               </td>
                                            </tr>
                                            <tr id="trSpecify" runat="server" visible="false">
                                                <td style="text-align:left">Specify :&nbsp;</td>
                                                <td style="text-align:left"><asp:TextBox ID="txtSpecify" required='yes' MaxLength="50" Width="235px" runat="server" ></asp:TextBox></td>
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
                                                        <td style="text-align:left"> Last Hr. Done </td>
                                                        <td style="text-align:left"> Current Hr. Done</td>
                                                        <td style="text-align:left">Next Hr. Due</td>
                                                      </tr>
                                                      <tr style="padding-top:5px;">
                                                         <td style="text-align:left;" ><asp:TextBox ID="txtLastHour" ReadOnly="true" Width="55px" runat="server" ></asp:TextBox></td>
                                                         <td style="text-align:left;"><asp:TextBox ID="txtDoneHour" required='yes' MaxLength="11" Width="60px" runat="server" AutoPostBack="True" ontextchanged="txtDoneHour_TextChanged" ></asp:TextBox></td>
                                                         <td style="text-align:left;"><asp:TextBox ID="txtNextHour" ReadOnly="true" Width="55px" runat="server" ></asp:TextBox></td>
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
                                                         <td style="text-align:left"><asp:TextBox ID="txtDuedt" Width="75px" runat="server"></asp:TextBox></td>
                                                         <td style="text-align:left;"><asp:TextBox ID="txtDoneDate" required='yes' MaxLength="11" Width="75px" runat="server" AutoPostBack="True" OnTextChanged="txtDoneDate_TextChanged"  ></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender1" PopupPosition="TopLeft" runat="server" Format="dd-MMM-yyyy" PopupButtonID="txtDoneDate" TargetControlID="txtDoneDate"></asp:CalendarExtender>
                                                         </td>
                                                         <td style="text-align:left; "><asp:TextBox ID="txtNextDueDt" Width="85px" runat="server"></asp:TextBox></td>
                                                      </tr>
                                                  </table>
                                                  </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </td>          
                                            </tr>
                                         
                                        </table>
                                            </fieldset> 
                                        </td>
                                      <td>
                                      <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse; ">
                                              <tr>
                                               <td style="text-align:left; width:172px; ">Service Report :&nbsp;</td>
                                               <td style="text-align:left">
                                                   <asp:TextBox ID="txtServiceReport" required='yes' TextMode="MultiLine" MaxLength="5000" runat="server" Height="110px" Width="350px" ></asp:TextBox>                                                       
                                               </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Condition Before :&nbsp;</td>
                                               <td style="text-align:left"><asp:TextBox ID="txtCondBefore" required='yes' MaxLength="50" Width="350px" runat="server"></asp:TextBox>
                                               <asp:HiddenField ID="hfIntervalId_H" runat="server" />
                                               <asp:HiddenField ID="hfInterval_H" runat="server" />
                                               </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Condition After :&nbsp;</td>
                                               <td style="text-align:left"><asp:TextBox ID="txtCondAfter" required='yes' MaxLength="50" Width="350px" runat="server"></asp:TextBox>
                                               </td>
                                            </tr>
                                            <tr>
                                               <td style="text-align:left">Updated By&nbsp;/&nbsp;On : </td>
                                               <td style="text-align:left"><asp:Label ID="lblUpdatedByOn" runat="server"></asp:Label></td>
                                            </tr>
                                           <%-- <tr>
                                                <td style="text-align:left"></td>
                                                <td style="text-align:left; ">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnUpdate" Text="Submit" CssClass="btnorange" runat="server" onclick="btnUpdate_Click" /></td>
                                            </tr>--%>
                                            </table>
                                      </td>
                                    </tr>
                                    <tr id="trRating" runat="server" visible="false">
                                        <td colspan="2">
                                        <div style="text-align:left; vertical-align:middle; font-weight:bold;"> <center><i>RATINGS</i></center></div>
                                        <br />
                                          <table cellpadding="2" cellspacing="0" width="100%">
                                             <tr>
                                                 <td style="text-align:right">Coating :&nbsp;</td>
                                                 <td style="text-align:left">
                                                      <asp:DropDownList ID="ddlCoating" runat="server">
                                                        <asp:ListItem Text="< SELECT >" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fair" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Poor" Value="3"></asp:ListItem>
                                                     </asp:DropDownList>
                                                 </td>
                                                 <td style="text-align:right">Corrosion :&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:DropDownList ID="ddlCorrosion" runat="server">
                                                        <asp:ListItem Text="< SELECT >" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="None" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Light" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Medium" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Heavy" Value="4"></asp:ListItem>
                                                     </asp:DropDownList>
                                                 </td>
                                                 <td style="text-align:right">Deformation :&nbsp;</td>
                                                 <td style="text-align:left">
                                                      <asp:DropDownList ID="ddlDeformation" runat="server">
                                                        <asp:ListItem Text="< SELECT >" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="None" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Minor" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Major" Value="3"></asp:ListItem>
                                                     </asp:DropDownList>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td style="text-align:right">Cracks :&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:DropDownList ID="ddlCracks" runat="server">
                                                        <asp:ListItem Text="< SELECT >" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="None" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Visible" Value="2"></asp:ListItem>
                                                        
                                                     </asp:DropDownList>
                                                 </td>
                                                 <td style="text-align:right">Overall Rating :&nbsp;</td>
                                                 <td style="text-align:left">
                                                     <asp:DropDownList ID="ddlORating" runat="server">
                                                        <asp:ListItem Text="< SELECT >" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Good" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Fair" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Poor" Value="3"></asp:ListItem>
                                                     </asp:DropDownList>
                                                 </td>
                                                 <td></td>
                                                 <td></td>
                                             </tr>
                                          </table>
                                        </td>
                                    </tr>
                                     <tr>
                                       <td colspan="2">
                                            <div style=" width :100%; height:200px;">
                                            <div>
                                             <div style="text-align:left; vertical-align:middle; font-weight:bold;">SPARES CONSUMED?&nbsp;
                                    <asp:DropDownList ID="ddlSparesReqd" Width="100px" runat="server" AutoPostBack="true" onselectedindexchanged="ddlSparesReqd_SelectedIndexChanged">
                                           <asp:ListItem Text="< SELECT >" Selected="True" Value="0" ></asp:ListItem> 
                                           <asp:ListItem Text="Yes" Value="1" ></asp:ListItem>
                                           <asp:ListItem Text="No" Value="2" ></asp:ListItem>
                                    </asp:DropDownList> 
                               </div>
                                             <table id="tblSpares" runat="server" cellpadding="0" cellspacing="0" visible="false" style="background-color: #f9f9f9; padding-top:3px;" width="100%">
                                    <tr>
                                        <td>
                                            <%--<asp:UpdatePanel ID="upSpares" runat="server">
                                                        <ContentTemplate>--%>
                                             <table cellpadding="2" cellspacing="0" width="100%">
                                                 <tr>
                                                      <td style="text-align:right; vertical-align:middle;">Select Spare :&nbsp;
                                                          <asp:DropDownList ID="ddlSparesList" AutoPostBack="true" OnSelectedIndexChanged="ddlSparesList_SelectedIndexChanged" runat="server" Width="250px"></asp:DropDownList>&nbsp;
                                                          <asp:ImageButton ID="imgAddSpare" ImageUrl="~/Images/add.png" runat="server" onclick="imgAddSpare_Click" />
                                                          <asp:ImageButton ID="btnfindspare" ImageUrl="~/Images/search_magnifier_12.png" runat="server" OnClientClick="return findspares();"/>
                                                      </td>
                                                      <td style="text-align:right; vertical-align:middle;">Qty(Cons) :&nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;"><asp:TextBox ID="txtQtyCon" runat="server" Width="30px" onkeypress="fncInputNumericValuesOnly(event)" MaxLength="5" ></asp:TextBox></td>
                                                      <td style="text-align:right; vertical-align:middle;">Qty(ROB) :&nbsp;</td>
                                                      <td style="text-align:left; vertical-align:middle;"><asp:TextBox ID="txtQtyRob" runat="server" Width="30px" onkeypress="fncInputNumericValuesOnly(event)" MaxLength="5" ></asp:TextBox></td>
                                                      <td style="vertical-align:middle;">
                                                          <asp:Button ID="btnAddSpare" Text="Add to List" CssClass="btnorange" runat="server" style="width:120px; height:20px; background-color:orange; float:right;" onclick="btnAddSpare_Click" />
                                                          <asp:Button ID="btnRefresh" runat="server" onclick="btnRefresh_Click" style="display:none" /></td>                                                         
                                                 </tr>
                                             </table>
                                             <%--</ContentTemplate>
                                           </asp:UpdatePanel>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>--%>
                                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;border-collapse: collapse;">
                                                <colgroup>
                                                         <col  />
                                                        <col style="width: 150px"/>
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 30px;" />
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
                                                        <td></td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                            <div id="dvSpares" onscroll="SetScrollPos(this)" class="scrollbox" style="overflow-y: scroll;
                                                overflow-x: hidden; width: 100%; height: 130px; text-align: center;">
                                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width: 100%;
                                                    border-collapse: collapse;">
                                                    <colgroup>
                                                        <col  />
                                                        <col style="width: 150px"/>
                                                        <col style="width: 100px" />
                                                        <col style="width: 90px;" />
                                                        <col style="width: 30px;" />
                                                        <col style="width: 17px;" />
                                                    </colgroup>
                                                    <asp:Repeater ID="rptComponentSpares" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="row" visible='<%#Eval("SpareId").ToString() != "" %>'>
                                                                <td align="left">
                                                                    <%#Eval("SpareName")%>
                                                                    <asp:HiddenField ID="hfdComponentId" Value='<%#Eval("ComponentId") %>' runat="server" />
                                                                    <asp:HiddenField ID="hfOffice_Ship" Value='<%#Eval("Office_Ship") %>' runat="server" />
                                                                    <asp:HiddenField ID="hfSpareId" Value='<%#Eval("SpareId") %>' runat="server" />
                                                                    <br />
                                                                    <span style="font-size:9px;color:blue;"><i><%#Eval("Maker")%></i></span>
                                                                </td>
                                                                
                                                                <td align="left">
                                                                    <%#Eval("PartNo")%>
                                                                </td>
                                                                <td align="center">
                                                                   <asp:Label ID="lblQtyCons" Text='<%#Eval("QtyCons")%>' runat="server"></asp:Label>
                                                                <td align="center">
                                                                   <asp:Label ID="lblQtyRob" Text='<%#Eval("QtyRob")%>' runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton runat="server" ID="imgDel" OnClick="imgDel_OnClick" CommandArgument='<%#Eval("RowId") %>' ImageUrl="~/Images/delete.png" Height="12px" OnClientClick="confirm('Are you sure to delete?');"/>
                                                                </td>
                                                                <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                                <%--</ContentTemplate>
                                           </asp:UpdatePanel>--%>
                                        </td>
                                    </tr>
                                </table>
                                            </div>
                                            </div>
                                            <div>
                                            <asp:Button ID="btnUpdate" Text="Save" style="float:right;width:120px;background-color:orange; padding-right:5px;" CssClass="btnorange" runat="server" onclick="btnUpdate_Click" />
                                            <div style="padding:0px; float:right">
                                                <b>
                                                <uc1:MessageBox ID="mbUpdateJob" runat="server" />
                                                </b>
                                            </div> 
                                            </div>
                                            <div>
                                          
                                        </td>
                                     </tr>
                                   
                                </table>
                                <div runat="server" id="dvAttachment" visible="false" >
                                <table cellpadding="2" cellspacing="" border="1" width="100%" style=" background-color:#c2c2c2; border-collapse:collapse" >
                                    <tr>
                                        <td style="width:30px; text-align:center">Sr#</td>
                                        <td>Description</td>
                                        <td style="width:150px;">File Name</td>
                                        <td style="width:12px;">&nbsp;</td>
                                    </tr>
                                    </table>
                                    <div style="width:100%;height:80px;overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;">
                                    <table cellpadding="2" cellspacing="" border="1" width="100%" style="border-collapse:collapse" >
                                        <asp:Repeater ID="rptFiles" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width:30px; text-align:center">&nbsp;<%#Eval("Sno")%></td>
                                                    <td style="text-align:left">&nbsp;<%#Eval("Descr")%></td>
                                                    <td style="width:150px;">
                                                        <a href='UploadFiles/<%#((Eval("VesselCode").ToString().Trim()=="")?"AttachmentForm/":VesselCode+"/")+ Eval("UpFileName").ToString()%>' target="_blank" ><%#Eval("UpFileName")%> </a>
                                                    </td>    
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                </table>
                                </div>
                                </div>    
                                <div style="text-align:left; vertical-align:middle; font-weight:bold;"> <center><i>DOCUMENTS</i></center>
                                <hr />
                                <span style="color:red"><b>[ Do not close this window if you still need to upload the documents. ]</b></span>
                                </div>
                                            
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
                                                              CssClass="btnorange" runat="server" 
                                                              style="width:120px; height:20px; background-color:orange; float:right;" 
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
                                    <tr id="trAttachments" visible="false" runat="server">
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
                                                                <asp:ImageButton runat="server" ID="btnDelAttachment" ImageUrl="~/Images/delete.png" Height="12px" OnClick="DeleteAttachment_OnClick" title='Delete Attachment' CssClass='<%#Eval("VesselCode")%>' CommandArgument='<%#Eval("TableId")%>' OnClientClick="javascript:confirm('Are you sure to remove this attachment?');" />
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
                                </div>
                            </asp:Panel>
                          </td>
                        </tr>
                        </table>                       
                        <%--</ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpdate" />
                        </Triggers>
                        </asp:UpdatePanel> --%>
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
        <td style="width:200px;">
            <div style="padding:10px;background-color:#c2c2c2">Photos Uploaded</div>
            <div style="height:500px;overflow-y:scroll;overflow-x:hidden">
                <asp:Repeater runat="server" ID="rptiamges">
                    <ItemTemplate>
                        <img src='<%#Eval("filename")%>' style="width:170px" onclick="window.open(this.src,'');"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </td>
        </tr>
        </table>
     </div>
    </form>
</body>
</html>
