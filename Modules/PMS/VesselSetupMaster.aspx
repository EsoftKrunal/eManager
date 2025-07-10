<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselSetupMaster.aspx.cs" Inherits="VesselSetupMaster" MasterPageFile="~/MasterPage.master" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script src="JS/Calender.js" type="text/javascript"></script>
          <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <script type="text/javascript" language="javascript">
         function opensearchwindow() {
             if (typeof (winref) == 'undefined' || winref.closed) {
                 winref = window.open('SearchComponentsForVessel.aspx', '', '');
             }
             else {
                 winref.focus();
             }
         }
         function reloadComponents(CompCode) {
             document.getElementById('hfSearchCode').value = CompCode;
             document.getElementById('ctl00_ContentMainMaster_btnSearchedCode').click();
            // __doPostBack('btnSearchedCode', '');
         }

         function setFocus(ctltitle) {
             ctltitle = ctltitle.toLowerCase().replace(/^\s+|\s+$/g, "");
             ctls = document.getElementsByTagName("a");
             i = 0;
             for (i = 0; i <= ctls.length - 1; i++) {
                 var v = ctls[i].title.toLowerCase().replace(/^\s+|\s+$/g, "");
                 if (v == ctltitle) {
                     ctls[i].focus();
                     dvscroll_Componenttree.scrollLeft = 0;
                     dvscroll_Componenttree.scrollTop = dvscroll_Componenttree.scrollTop + 50;
                 }
             }
         }
         function openaddunitwindow(CompCode, VC) {
             //if (typeof (winref) == 'undefined' || winref.closed) {
             window.open('AddComponentUnits.aspx?CompCode=' + CompCode + '&&VC=' + VC, '', 'status=1,scrollbars=0,toolbar=0,menubar=0,width=700,height=415');
             //}
             // else {
             //    winref.focus();
             //}
         }
         function openeditunitwindow(CompCode, VC, CompIds) {
             //if (typeof (winref) == 'undefined' || winref.closed) {
             window.open('EditComponentUnits.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&CompIds=' + CompIds, '', 'status=1,scrollbars=0,toolbar=0,menubar=0,width=650,height=410');
             //}
             //else {
             //    winref.focus();
             //}
         }
         function openjobwindow(CompCode, VC) {
             //if (typeof (winref) == 'undefined' || winref.closed) {
             window.open('AddJobsToComponentUnits.aspx?CompCode=' + CompCode + '&&VC=' + VC, '', 'status=1,toolbar=0,menubar=0,resizable=1,width=950,,height=490');
             //}
             // else {
             //     winref.focus();
             // }
         }
         function openeditjobwindow(CompCode, VC, JobIds) {
             //if (typeof (wineditref) == 'undefined' || wineditref.closed) {
             window.open('EditUnitJobs.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&JobIds=' + JobIds, '', 'status=1,toolbar=0,menubar=0,resizable=1,width=950,,height=490');
             ///}
             //else {
             //    wineditref.focus();
             //}
         }
         function opendeletejobwindow(CompCode, VC) {
             //if (typeof (windelref) == 'undefined' || windelref.closed) {
             window.open('DeleteUnitJobs.aspx?CompCode=' + CompCode + '&&VC=' + VC, '', 'status=1,toolbar=0,menubar=0,resizable=1,width=1190,,height=450');
             //}
             //else {
             //    windelref.focus();
             //}
         }
         function openaddsparewindow(CompCode, VC, SpareId) {

             window.open('AddSpares.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SPID=' + SpareId, '', 'status=1,scrollbars=0,toolbar=0,menubar=0,width=750,height=390');

         }
         function openstructurewindow(VC, Mode) {
             window.open('VesselSetup.aspx?VC=' + VC + '&&Mode=' + Mode, '', 'status=1,scrollbars=0,toolbar=0,menubar=0,width=1100,height=550');
         }
         function reloadunits() {
             document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();
            // __doPostBack('btnRefresh', '');
         }
         function reloadtree() {
             document.getElementById('ctl00_ContentMainMaster_btnRefTv').click();
             //__doPostBack('btnRefTv', '');
         }
         function fncInputNumericValuesOnly(evnt) {
             if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                 event.returnValue = false;
             }
         }
         function opencompreport(Mode, Vessel, VesselName) {
             window.open('Reports/OfficeComponentReport.aspx?Mode=' + Mode + '&Vessel=' + Vessel + '&VesselName=' + VesselName, '', '');
         }
         function opencompjobreport(Mode, Vessel, VesselName) {
             window.open('Reports/OfficeComponentJobsReport.aspx?Mode=' + Mode + '&Vessel=' + Vessel + '&VesselName=' + VesselName, '', '');
         }
         function showAttachment(file) {
             window.open('UploadFiles/AttachmentForm/' + file);
         }
         function showRisk(file) {
             window.open('UploadFiles/RiskAssessment/' + file);
         }
         function showGuidelines(file, VC) {
             window.open('UploadFiles/' + VC + '/' + file);
         }

     </script>
     <style type="text/css">
.selbtn
{
    background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
    </asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
     <div class="text headerband">
            Vessel Setup Master
        </div>
        <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                       <tr>
                            <td style="padding-right: 10px;padding-left:2px">
                            <div style="width:100%; height:500px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
               <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:200px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
                        <center>
                        <div style="border:dotted 1px blue; height :50px; width :120px;background-color :White;" >
                        <img src="Images/loading.gif" alt="loading"> Loading ...
                        </div>
                        </center>
                    </div>
                </ProgressTemplate> 
             </asp:UpdateProgress>
                            <asp:UpdatePanel runat="server" id="up1" UpdateMode="Always">
                            <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                            <tr>
                            <td id="tdComponentTree" runat="server" style="width:350px; text-align:left; vertical-align:top; padding-top:5px;">  
                             <div style="padding-bottom:5px" >
                              <b>Vessel :</b>&nbsp;<asp:Label ID="lblVesselName" style="font-weight:bold" runat="server"></asp:Label> 
                              <asp:ImageButton ID="btnAddStructure" ImageUrl ="~/Modules/PMS/Images/add.png" runat="server" 
                                     Visible="false" onclick="btnAddStructure_Click"  
                                     style="float:right; margin-right :5px;" CausesValidation="False"/>&nbsp;
                              <asp:ImageButton ID="btnSearchComponents" ImageUrl ="~/Modules/PMS/Images/find.jpg" 
                                     runat="server" onclick="btnSearchComponents_Click" ToolTip="Search Components" 
                                     Visible="false" style="float:right; margin-right :5px;" 
                                     CausesValidation="False"/>                        
                              <asp:DropDownList ID="ddlVessels" runat="server" AutoPostBack="true" Width="272px" onselectedindexchanged="ddlVessels_SelectedIndexChanged" style="display:none" ></asp:DropDownList>
                            </div>                                                            
                                <div ID="dvscroll_Componenttree" class="scrollbox" onscroll="SetScrollPos(this)" style="width :350px; overflow-y:scroll; overflow-x: hidden;  height :395px;">
                                    <asp:TreeView ID="tvComponents" runat="server" ImageSet="Arrows" OnTreeNodePopulate="tvComponents_TreeNodePopulate" 
                                        onselectednodechanged="tvComponents_SelectedNodeChanged" ShowLines="True">
                                        <LevelStyles>
                                            <asp:TreeNodeStyle Font-Underline="False" ForeColor="Purple" />
                                            <asp:TreeNodeStyle Font-Underline="False" ForeColor="DarkGreen" />
                                            <asp:TreeNodeStyle Font-Underline="False"  />
                                            <asp:TreeNodeStyle Font-Underline="False" ForeColor="Black" />
                                        </LevelStyles>
                                        <HoverNodeStyle CssClass="treehovernode" />
                                        <SelectedNodeStyle CssClass="treeselectednode" />
                                    </asp:TreeView>
                                    <asp:HiddenField ID="hfSearchCode" runat="server" />
                                    <asp:Button ID="btnSearchedCode" runat="server" onclick="btnSearchedCode_Click" 
                                        style="display:none" />
                                </div>
                                <div style="width :350px; height: 20px; text-align:left; padding-top:3px;">
                                    <asp:Button ID="btnExport" ToolTip="Export To Ship Master" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Export To Ship Master" Width="150px" 
                                         OnClick="btnExport_Click" ForeColor="Black"
                                        OnClientClick="javascript:return confirm('Are you sure to export ?');" 
                                        style="float:left; background-image:url(Images/export.gif); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" 
                                        runat="server" CausesValidation="False" />
                                    <asp:Button ID="btnPrintJobs" ToolTip="Print Jobs" runat="server" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Jobs" ForeColor="Black" Width="60px" 
                                        OnClick="btnPrintJobs_Click" 
                                        style="float:right; background-image:url(Images/printer16x16.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" 
                                        CausesValidation="False"/>
                                    <asp:Button ID="btnPrintCompList" ToolTip="Print Components" runat="server" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Components" ForeColor="Black" Width="110px" 
                                        OnClick="btnPrintCompList_Click" 
                                        style="float:right; background-image:url(Images/printer16x16.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px  " 
                                        CausesValidation="False" />     
                                    
                                </div>
                                <br/>
                            </td>
                            <td style="text-align :left; padding-left:5px;">                           
                            
                            <div>
                            <asp:Label ID="lblMessage" Text="" CssClass="error_msg"  runat="server"></asp:Label>                            
                            <asp:HiddenField ID="hfCompCode" runat="server" /><asp:Button ID="btnRefresh" style="display:none;" runat="server" onclick="btnRefresh_Click" /><asp:Button ID="btnRefTv" style="display:none;" OnClick="btnRefTv_Click" runat="server" />
                            </div>
                                
                             <div style="height:5px;"></div>
                             <div style="height:30px; padding :3px; text-align :left;" class="dottedscrollbox">
                             <asp:Button ID="btnSpecification" Text="Specification" CssClass="btn1" 
                                     Width="140px" style="float :left" runat="server" 
                                     onclick="btnSpecification_Click" CausesValidation="False"  />                             
                             <asp:Button ID="btnAssignJobs" Text="Jobs" CssClass="btn1" Width="140px" 
                                     style="float :left" runat="server" onclick="btnAssignJobs_Click" 
                                     CausesValidation="False" />
                             <asp:Button ID="btnAssignSpare" Text="Spares" CssClass="btn1" Width="140px" 
                                     style="float :left" runat="server" onclick="btnAssignSpare_Click" 
                                     CausesValidation="False" />
                             <asp:Button ID="btnAssignRunningHour" Text="Running Hour Update" 
                                     CssClass="btn1" Width="160px" style="float :left" runat="server" 
                                     onclick="btnAssignRunningHour_Click" CausesValidation="False"  />
                             
                             </div>
                             <asp:Panel ID="plSpecs" runat="server" Width="100%" ScrollBars="None">
                               <div style="height: 30px; padding :3px; text-align :left;vertical-align:top;"; class="dottedscrollbox">
                               <asp:ImageButton ID="imgbtneditSpec" ImageUrl="~/Modules/PMS/Images/edit1.png" runat="server" 
                                       ToolTip="Edit Specification" onclick="imgbtneditSpec_Click" 
                                       CausesValidation="False"  />
                                <asp:Label ID="lblSpecCompCode"  Font-Bold="true" runat="server" ></asp:Label>                             
                            </div>
                             <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader" visible="false" >
                              Edit Component Specification
                           </div>
                           <table cellpadding="3" cellspacing="1" style="background-color:#f9f9f9;border: #4371a5 1px solid; border-collapse: collapse;" width="99%" >
                           <tr>
                            <td>Linked To:&nbsp;</td>
                            <td>
                                <asp:Label ID="lblLinkedto" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            </tr>
                            <tr>
                            <td >Component Code :</td>
                            <td>
                                <asp:Label ID="txtComponentCode" style="text-align:left" runat="server" Width="60px"></asp:Label>
                                <asp:Label ID="txtUnitCode" style="text-align:left" runat="server" Visible="False" Width="30px"></asp:Label>
                                </td>                                
                            </tr>
                            <tr>
                            <td>Component Name :</td>
                            <td>
                                <asp:Label  ID="txtComponentName" runat="server" Width="350px"></asp:Label>
                            </td>
                            </tr>
                            <tr><td>Maker :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMaker" runat="server" MaxLength="50" Width="350px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaker" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                </td>
                                </tr>
                                <tr><td>Account Code :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccountCodes" runat="server" MaxLength="50" Width="350px"></asp:TextBox>
                                    
                                </td>
                                </tr>
                            <tr> 
                                <td>
                                    Maker Type :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMakerType" runat="server" MaxLength="50" Width="350px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMakerType" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                </td>
                            <tr><td>Description :<asp:HiddenField ID="HiddenField1" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComponentDesc" runat="server" MaxLength="500" 
                                        Width="350px" Height="66px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfCompId" runat="server" />
                                </td>
                                <td style="text-align:left">
                                    <table cellpadding="0" cellspacing="0" width="355px">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkClass" runat="server" AutoPostBack="True" OnCheckedChanged="chkClass_OnCheckedChanged" />
                                                Class EQIP
                                            </td>
                                            <td style="vertical-align:middle">
                                                Class EQIP Code :</td>
                                            <td style=" text-align:right">
                                                <asp:TextBox ID="txtClassCode" runat="server" MaxLength="30" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:CheckBox ID="chkCritical" runat="server" Enabled="false" />
                                                Critical EQIP
                                                <asp:CheckBox ID="chkCE" runat="server" Enabled="false" Text="Critical for Environment" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkInactive" runat="server" Enabled="false" />
                                                Inactive
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSaveSpec" runat="server" CssClass="btn" Text="Save" OnClick="btnSaveSpec_Click" 
                                                        />
                                                </td>
                                                <td style="text-align:left">
                                                    <asp:Button ID="btnCancelSpec" runat="server" CssClass="btn" Text="Cancel" 
                                                        Visible="false" onclick="btnCancelSpec_Click" CausesValidation="False" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                   <td style="height: 50px;" colspan="2">
                                   
                                   </td>
                                </tr>
                                
                           </table>
                             
                             </asp:Panel>
                            <asp:Panel ID="plJobs" runat="server" Width="100%" ScrollBars="None">
                             <div style="height: 30px; padding :3px; text-align :left;vertical-align:top;"; class="dottedscrollbox">
                             <asp:ImageButton ID="btnAddJobs" ImageUrl="~/Modules/PMS/Images/add1.png" runat="server" ToolTip="Assign New Jobs" onclick="btnAddJobs_Click" />
                             <asp:ImageButton ID="imgbtnEditJobs" ImageUrl="~/Modules/PMS/Images/edit1.png" runat="server" ToolTip="Edit Jobs" onclick="imgbtnEditJobs_Click"  />
                             <asp:ImageButton ID="ImgbtnDeleteJobs" ImageUrl="~/Modules/PMS/Images/delete1.png" OnClientClick="javascript:return confirm('Are you sure to delete this job?')" runat="server" ToolTip="Delete Jobs" onclick="ImgbtnDeleteJobs_Click"  />                                                  
                             <asp:Label ID="lblCompCodeJobs"  Font-Bold="true" runat="server"></asp:Label>                             
                            </div>
                             <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader" visible="false" >
                             Add/Modify Component Jobs
                           </div>
                             <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%" >
                             <tr>
          <td>
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                   <col style="width:3%;"  />
                   <col style="width:7%;" />
                   <col style="width:53%;"/>
                   <col style="width:6%;" />
                   <col style="width:7%;" />
                   <col style="width:8%;" />
                   <col style="width:7%;" />
                   <col style="width:7%;" />
                   <col style="width:2%;" />
                   <tr align="left" class="headerstylegrid">
                   <td style="width:3%;"></td>
                   <td style="width:7%;">Job Cat.</td>
                   <td style="width:53%;">Job Name</td>
                   <td style="width:6%;">Dept.</td>
                   <td style="width:7%;">Rank</td>
                   <td style="width:8%;">Int. Type</td>
                   <td style="width:7%;">Interval</td>
                   <td style="width:7%;"><img src="Images/paperclip.gif" style="border:none;" title="Attachment" /></td>
                   <td style="width:2%;"></td>    
                   </tr>
             </colgroup>
           </table>           
           <div id="dvJobs" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 281px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                    <col style="width:3%;"  />
                   <col style="width:7%;" />
                   <col style="width:53%;"/>
                   <col style="width:6%;" />
                   <col style="width:7%;" />
                   <col style="width:8%;" />
                   <col style="width:7%;" />
                   <col style="width:7%;" />
                   <col style="width:2%;" />
                    </colgroup>
               <asp:Repeater ID="rptVesselComponents" runat="server">
                  <ItemTemplate>
                      <tr >
                           <td align="center" style="width:3%;"><asp:CheckBox ID="chkSelectJobs" runat="server" /></td>
                           <td align="left" style="width:7%;"><%#Eval("JobCode")%><asp:HiddenField ID="hfdJobId" Value='<%#Eval("CompJobId")%>' runat="server" /></td>
                           <td align="left" style="width:53%;"><%#Eval("JobName")%>
                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                           <asp:HiddenField ID="hfVcId" Value='<%#Eval("ComponentId")%>' runat="server" /></td>
                           <td align="center" style="width:6%;"><%#Eval("DeptName")%></td>
                           <td align="center" style="width:7%;"><%#Eval("RankCode")%></td>
                           <td align="center" style="width:8%;"><%#Eval("IntervalName")%></td>
                           <td align="center" style="width:7%;"><%#Eval("Interval")%></td>
                           <td style="width:7%;">
                                <a href="VslDocManagement.aspx?CJID=<%#Eval("CompJobID")%>" target="_blank" style='cursor:pointer;Display:<%#((Eval("AttachmentCount").ToString()=="0")?"none":"block")%>' > <img src="Images/paperclip.gif" style="border:none;" title="Attachment"/> </a>
                             <%--<img src="Images/paperclip.gif" title="Company Form" style='cursor:pointer;<%#(Eval("AttachmentForm").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("AttachmentForm")%>' onclick="showAttachment(this.getAttribute('file'))"/>
                             <img src="Images/paperclip.gif" title="Risk Assessment" style='cursor:pointer;<%#(Eval("RiskAssessment").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("RiskAssessment")%>' onclick="showRisk(this.getAttribute('file'))"/>
                             <img src="Images/paperclip.gif" title="Guidelines" style='cursor:pointer;<%#(Eval("Guidelines").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("Guidelines")%>' vcode='<%#Eval("VesselCode")%>' onclick="showGuidelines(this.getAttribute('file'),this.getAttribute('vcode'));"/>--%>
                           </td>
                           <td style="width:2%;"></td>
                       </tr>
                   </ItemTemplate>
                  </asp:Repeater>
              </table>
           </div>
           </td>
          </tr>
                    </table>                        
                    </asp:Panel>
                            <asp:Panel ID="plSpare" runat="server" Width="100%" Visible="false" >
       <div style="height: 30px; padding :3px; text-align :left;vertical-align:top;" class="dottedscrollbox">
       <asp:ImageButton ID="btnAddSpares" ImageUrl="~/Modules/PMS/Images/add1.png" runat="server" ToolTip="Assign New Spares" onclick="btnAddSpares_Click" />&nbsp;
       <asp:Label ID="lblCompCodeSpare"  Font-Bold="true" runat="server"></asp:Label>
       </div>
       <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader">
         Add/Modify Spares
       </div>
       <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%">
         <tr>
          <td>
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                     <%if (Session["VSPEDIT"].ToString().Trim() == "True")
                       {%>
                      <col style="width:3%;" />
                      <% }%>
                      <col style="width:50%"/>
                      <col style="width:9%" />
                      <%--<col style="width:90px;"  />--%>
                      <col style="width:9%;" />
                      <%--<col style="width:100px" />--%>
                      <col style="width:9%;" /> 
                      <col style="width:6%;" />
                      <col style="width:6%;" />
                      <%if (Session["VSPDELETE"].ToString().Trim() == "True")
                        {%>
                      <col style="width:3%;" />
                      <% }%>
                      <col style="width:3%;" />                                               
                      <col style="width:2%;" />
              <tr align="left" class="headerstylegrid">
                      <%if (Session["VSPEDIT"].ToString().Trim() == "True")
                        {%>
                      <td style="width:30px;"></td>
                      <% }%>
                      <td style="width:50%">Spare Name</td>
                      <td style="width:9%;">Maker</td>
                      <%--<td>Maker Type</td>--%>
                      <td style="width:9%;">Part#</td>
                      <%--<td>Part Name</td>--%>
                      <td style="width:9%;">Drawing#</td>
                      <td style="width:6%;">Qty(Min)</td>
                      <td style="width:6%;">Qty(Max)</td>
                      <%if (Session["VSPDELETE"].ToString().Trim() == "True")
                        {%>
                      <td style="width:3%;"></td>
                      <% }%>
                      <td style="width:3%;"></td>
                      <td style="width:2%;" ></td>    
              </tr>
              </colgroup>
           </table>
           <div id="dvSpares" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 294px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all"style="width:100%;border-collapse:collapse;">
             <colgroup>
                     <%if (Session["VSPEDIT"].ToString().Trim() == "True")
                       {%>
                      <col style="width:3%;" />
                      <% }%>
                      <col style="width:50%"/>
                      <col style="width:9%" />
                      <%--<col style="width:90px;"  />--%>
                      <col style="width:9%;" />
                      <%--<col style="width:100px" />--%>
                      <col style="width:9%;" /> 
                      <col style="width:6%;" />
                      <col style="width:6%;" />
                      <%if (Session["VSPDELETE"].ToString().Trim() == "True")
                        {%>
                      <col style="width:3%;" />
                      <% }%>
                      <col style="width:3%;" />                                               
                      <col style="width:2%;" />
             </colgroup>
             <asp:Repeater ID="rptComponentSpares" runat="server">
                 <ItemTemplate>
                         <tr>
                             <%if (Session["VSPEDIT"].ToString().Trim() == "True")
                               {%>
                              <td style="width:3%;"><asp:ImageButton ID="btnEdit" CommandArgument='<%#Eval("SpareId") %>' ToolTip="Edit Spare" ImageUrl="~/Modules/PMS/Images/edit.png" OnClick="btnEdit_Click" runat="server" ></asp:ImageButton>
                                  <asp:HiddenField ID="hf_Off_Ship" Value='<%#Eval("Office_Ship") %>' runat="server" />
                              </td>
                              <% }%>
                              <td  align="left" style="width:50%"><%#Eval("SpareName")%></td>
                              <td  align="left" style="width:9%">
                              <%#Eval("Maker")%>                                                                                     
                              </td>
                              <%--<td align="left"><%#Eval("MakerType")%></td>--%>
                              <td align="left" style="width:9%"><%#Eval("PartNo")%>   </td>
                              <%--<td align="left"><%#Eval("PartName")%>   </td>--%>
                              <td align="left" style="width:9%"><%#Eval("DrawingNo")%>   </td>
                              <td style="width:6%;"><%#Eval("MinQty")%></td>
                              <td style="width:6%;"><%#Eval("MaxQty")%></td>
                              <%if (Session["VSPDELETE"].ToString().Trim() == "True")
                                {%>
                              <td style="width:3%;"><asp:ImageButton ID="lbtnSelect" ImageUrl="~/Modules/PMS/Images/Delete.png" CommandArgument='<%#Eval("SpareId") %>' OnClientClick="javascript:return confirm('Are you sure to delete this Spare?');" ToolTip="Delete Spare" OnClick="lbtnSelect_Click"  runat="server"></asp:ImageButton></td>
                              <% }%>
                              <td style="width:3%;">
                                <a runat="server" ID="ancFile"  href='<%# "~/Modules/PMS/UploadFiles/UploadSpareDocs/" + Eval("Attachment").ToString() %>' target="_blank" visible='<%#Eval("Attachment").ToString()!= "" %>'  title="Show Attached File" >
                                 <img src="Images/paperclip.gif" style="border:none"  /></a>
                                    
                              </td>
                              <td style="width:2%"></td>
                          </tr>
                  </ItemTemplate>
             </asp:Repeater>
      </table>
           </div>
           </td>
          </tr>
        </table>           
     </asp:Panel>
                            <asp:Panel ID="PlRunningHour" runat="server" Width="100%" Visible="false">
     
                            <div  class="text headerband" visible="false" >
           Running Hour Details
     </div>
                            <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%" >
                             <tr>
          <td colspan="2">
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                   <%--<col style="width:40px;" />--%>
                   <col style="text-align:left; width:110px;"/>
                   <col style="text-align:left" />
                   <col style="width:110px;" />
                   <col style="width:100px;" />                   
                   <col style="width:150px;" />
                   <col style="width:17px;" />
                   <tr align="left" class="headerstylegrid">                  
                   <%--<td>Sr.No</td>--%>
                   <td style="text-align:left">Comp. Code</td>
                   <td style="text-align:left">Component Name</td>
                   <td>New Hr.</td>
                   <td>As on Dt.</td>                   
                   <td>Avg Hr./day</td>
                   <td></td>    
                   </tr>
             </colgroup>
           </table>           
           <div id="dvRH" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 310px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                  <%--<col style="width:40px;" />--%>
                   <col style="width:110px;"/>
                   <col  />
                   <col style="width:110px;" />
                   <col style="width:100px;" />
                   <col style="width:150px;" />
                   <col style="width:17px;" />
               </colgroup>
               <asp:Repeater ID="rptRunningHour" runat="server">
                  <ItemTemplate>
                      <tr>                          
                           <%--<td align="center"><%#Eval("SrNo")%></td>--%>
                           <td align="left"><%#Eval("ComponentCode")%><asp:HiddenField ID="HiddenField2" Value='<%#Eval("ComponentId")%>' runat="server" /></td>
                           <td align="left"><%#Eval("ComponentName")%></td>                           
                           <td align="center"><asp:TextBox ID="txtStartupHour" required='yes' Text='<%#Eval("StartupHour")%>' MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)" Width="90px" runat="server" ></asp:TextBox></td>
                           <td align="center"><asp:TextBox ID="txtDueDate" required='yes' Text='<%#Eval("StartDate")%>' onfocus="showCalendar('',this,this,'','holder1',5,22,1)" Width="90px" runat="server"></asp:TextBox></td>
                           <td align="center"><asp:TextBox ID="txtAvgRunHour" required='yes' AutoPostBack="true" Text='<%#Eval("AvgRunningHrPerDay")%>' MaxLength="2" onkeypress="fncInputNumericValuesOnly(event)" Width="100px" runat="server" ></asp:TextBox></td>
                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                       </tr>
                   </ItemTemplate>
                   <AlternatingItemTemplate>
                       <tr class="alternaterow">
                           <%--<td align="center"><%#Eval("SrNo")%></td>--%>
                           <td align="left"><%#Eval("ComponentCode")%><asp:HiddenField ID="HiddenField3" Value='<%#Eval("ComponentId")%>' runat="server" /></td>
                           <td align="left"><%#Eval("ComponentName")%></td>
                           <td align="center"><asp:TextBox ID="TextBox1" required='yes' Text='<%#Eval("StartupHour")%>' MaxLength="5" onkeypress="fncInputNumericValuesOnly(event)" Width="90px" runat="server" ></asp:TextBox></td>
                           <td align="center"><asp:TextBox ID="TextBox2" required='yes' Text='<%#Eval("StartDate")%>' onfocus="showCalendar('',this,this,'','holder1',5,22,1)" Width="90px" runat="server"></asp:TextBox></td>
                           <td align="center"><asp:TextBox ID="TextBox3" required='yes' AutoPostBack="true" Text='<%#Eval("AvgRunningHrPerDay")%>' MaxLength="2" onkeypress="fncInputNumericValuesOnly(event)" Width="100px" runat="server" ></asp:TextBox></td>
                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                           </tr>
                    </AlternatingItemTemplate>
                  </asp:Repeater>
              </table>
           </div>
           </td>
          </tr>
          <tr>
          <td style="width:75%">
            <div style="padding-top:5px;">
                <uc1:MessageBox ID="msgRunHour" runat="server" />
            </div>
          </td>
          <td style="width:25%">
          <asp:Button ID="btnRunHSave" Text="Save" OnClick="btnRunHSave_Click" CssClass="btnorange" Width="100px" style="float :right; padding-right:5px;" runat="server" />
           <div  >
                <asp:UpdatePanel ID="UPPrint" runat="server" >
                    <ContentTemplate>
                        <asp:Button ID="btnPring" Text=" Print" CssClass="btnorange" style="width:80px; float:right; margin-right:7px;" runat="server" onclick="btnPrintRunnongHours_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
          </td>
          </tr>
          </table>
    
    </asp:Panel>
                           
    <div style="padding-top:5px;">
        <uc1:MessageBox ID="MessageBox1" runat="server" />
    </div>
     </td>
     </tr></table>
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
    </asp:Content>
