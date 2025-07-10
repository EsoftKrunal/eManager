<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ShipMaster.aspx.cs" Inherits="ShipMaster" %>
<%@ Register Src="~/Modules/PMS/UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%@ Register Src="~/Modules/PMS/UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

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
            __doPostBack('btnSearchedCode', '');
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
        function fncInputNumericValuesOnly(evnt) {
             if (!( event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                 event.returnValue = false;
             }
         }
         function opencompreport(Mode,Vessel) {
            window.open('Reports/OfficeComponentReport.aspx?Mode='+ Mode + '&Vessel='+ Vessel, '', '');
        }
        function opencompjobreport(Mode,Vessel) {
            window.open('Reports/OfficeComponentJobsReport.aspx?Mode='+ Mode + '&Vessel='+ Vessel, '', '');
        }
        function openaddsparewindow(CompCode, VC, SpareId, Office_Ship) {

            window.open('Ship_AddEditSpares.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SPID=' + SpareId + '&&OffShip=' + Office_Ship, '', 'status=1,scrollbars=0,toolbar=0,menubar=0,width=1000,height=600');

        }

        function openaddsparewindow_stock(CompCode, VC, SpareId, Office_Ship) {

            window.open('Ship_AddEditStock.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SPID=' + SpareId + '&&OffShip=' + Office_Ship, '', 'status=1,scrollbars=0,toolbar=0,menubar=0,width=1000,height=600');

        }
        function openaddcomponentwindow(VC, CC) {

            window.open('Popup_Ship_AddComponents.aspx?VC=' + VC + '&&CC=' + CC, '', 'status=1,scrollbars=0,toolbar=0,menubar=0,resizable=1');

        }
        function openjobwindow(CompCode, VC) {
            window.open('Ship_AddJobsToComponents.aspx?CompCode=' + CompCode + '&&VC=' + VC, '', 'status=1,toolbar=0,menubar=0,resizable=1,width=950,,height=490');
            
        }
        function openeditjobwindow(CompCode, VC, JobIds) {            
            window.open('Ship_EditComponentJobs.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&JobIds=' + JobIds, '', 'status=1,toolbar=0,menubar=0,resizable=1,width=950,,height=490');            
        }
        function reloadunits() {
            
            __doPostBack('btnRefresh', '');
        }
        function reloadtree(CompCode)
        {
          __doPostBack('btnRefTv', '');
          document.getElementById('hfSearchCode').value = CompCode;
        }
        function OpenJobDesc(CompJobId,VesselCode)
        {
            window.open('Reports/VSL_JobDesc.aspx?CompJobId=' + CompJobId + '&VSL=' + VesselCode, '', '');
        }
//        function ShowHistory(VC,CID,JID)
//       {
//           window.open('JobUpdateHistory.aspx?VC='+ VC +'&&CID='+ CID + '&&JID='+ JID,'','');
//       }
       function opendetails(RP,HID,VC,UT) {
            var UT = '<%=UserType%>';
         if(RP == 'REPORT')
         {
             RP = 'R';
             
             if (UT == "S") {
                 window.open('JobCard.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP + '&ModifySpare=Y', '', '');
             }
             else {
                 window.open('JobCard_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
             }
         }
         if(RP == 'POSTPONE')
         {
                RP = 'P';
                if (UT == "S")
                    window.open('PopupHistoryJobDetails.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
                else {
                    //window.open('JobCard_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
                    window.open('PopupHistoryJobDetails_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
                }
         }
         if (RP == 'DEFECT') {
             window.open('Popup_BreakDown.aspx?DN=' + HID + '&ModifySpare=Y', '', '');
         }
         if (RP == 'UNPLANNED') {
             window.open('Popup_AddUnPlanJob.aspx?VSL=' + VC + '&UPId=' + HID + '&ModifySpare=Y', '', '');
         }
       }
       function showAttachment(file)
        {
            window.open('UploadFiles/AttachmentForm/' + file);
        }
       function showRisk(file)
        {
           window.open('UploadFiles/RiskAssessment/' + file);
        }
       function showGuidelines(file,VC)
        {
           window.open('UploadFiles/' + VC + '/' + file);
       }
       function openShutdownwindow(CompCode, VC, SdID) {
           var usertype = '<%=Session["UserType"].ToString()%>';
           if (usertype == 'O')
               window.open('Office_CriticalEqpShutdownReq.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SD=' + SdID, '', '');
           else
                window.open('VSL_CriticalEqpShutdownReq.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SD=' + SdID , '', '');
       }
    </script>
     <style type="text/css">
        .CriticalType_C
        {
            background-color:#ff6666;
            display:inline-block;
            padding:2px;
            height:12px;
            width:12px;
            text-align:center;
            word-break: break-all;
            font-size:9px;
        }
         
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr><td>
        <hm:HMenu runat="server" ID="menu2" />
        </td></tr>
        <tr>        
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
            <td>
            <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
            <td style="padding-right: 10px;padding-left:2px">
            <div style="width:100%; height:452px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
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
                             
                             <%-- <asp:ImageButton ID="btnSearchComponents" ImageUrl ="~/Images/find.jpg" 
                                     runat="server" onclick="btnSearchComponents_Click" ToolTip="Search Components" 
                                     Visible="false" style="float:right; margin-right :5px;" 
                                     CausesValidation="False"/>--%>
                                   <span style="float :right">
                              <asp:Button ID="btnSearchComponents" ToolTip="Print Jobs" runat="server" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Search" CssClass="btnorange" Width="70px" 
                                        OnClick="btnSearchComponents_Click" 
                                        style="background-image:url(Images/find.jpg); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" 
                                        CausesValidation="False"/>       
                                     </span>
                              <asp:DropDownList ID="ddlVessels" runat="server" AutoPostBack="true" Width="272px" onselectedindexchanged="ddlVessels_SelectedIndexChanged" ></asp:DropDownList> 
                            </div>                                                            
                                <div ID="dvscroll_Componenttree" class="scrollbox" onscroll="SetScrollPos(this)" style="width :350px; overflow-y:scroll; overflow-x: hidden;  height :390px;">
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
                                <div style="width :350px; height :30px; padding :3px; vertical-align:top;text-align:center; ">
                                    <asp:Button ID="btnCreateDB" ToolTip="Create DB" runat="server" 
                                        OnClientClick="javascript:return confirm('Are you sure to create DB.');" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Create DB" CssClass="btnorange" Width="90px" 
                                        OnClick="btnCreateDB_Click" 
                                        style="background-image:url(Images/database.gif); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px  " 
                                        CausesValidation="False" />
                                    <asp:Button ID="btnPrintCompList" ToolTip="Print Components" runat="server" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Components" CssClass="btnorange" Width="110px" 
                                        OnClick="btnPrintCompList_Click" 
                                        style="background-image:url(Images/printer16x16.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px  " 
                                        CausesValidation="False" />
                                    <asp:Button ID="btnPrintJobs" ToolTip="Print Jobs" runat="server" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Jobs" CssClass="btnorange" Width="60px" 
                                        OnClick="btnPrintJobs_Click" 
                                        style="background-image:url(Images/printer16x16.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" 
                                        CausesValidation="False"/>                                    
                                </div>
                            </td>
                            <td style="text-align :left; padding-left:5px;">                           
                            
                            <div>
                            <asp:Label ID="lblMessage" Text="" CssClass="error_msg"  runat="server"></asp:Label>
                            <asp:Button ID="btnRefresh" style="display:none;" runat="server" onclick="btnRefresh_Click" />
                            <asp:Button ID="btnRefTv" style="display:none;" OnClick="btnRefTv_Click" runat="server" />
                            </div>
                                
                             <div style="height:5px;"></div>
                             <div style="height:30px; padding :3px; text-align :left;" class="dottedscrollbox"  runat="server">
                             <asp:Button ID="btnSpecification" Text="Specification" CssClass="btnorange" 
                                     Width="140px" style="float :left" runat="server" 
                                     onclick="btnSpecification_Click" CausesValidation="False"  />                             
                             <asp:Button ID="btnAssignJobs" Text="Jobs" CssClass="btnorange" Width="140px" 
                                     style="float :left" runat="server" onclick="btnAssignJobs_Click" 
                                     CausesValidation="False" />
                             <asp:Button ID="btnAssignSpare" Text="Spares" CssClass="btnorange" Width="140px" 
                                     style="float :left" runat="server" onclick="btnAssignSpare_Click" 
                                     CausesValidation="False" />
                             <%--<asp:Button ID="btnAssignRunningHour" Text="Running Hour " CssClass="btnorange" Width="160px" style="float :left" runat="server" onclick="btnAssignRunningHour_Click"  />--%>
                             <asp:Button ID="btnJobHistory" Text="History" CssClass="btnorange" Width="140px" 
                                     style="float :left" runat="server" onclick="btnJobHistory_Click" 
                                     CausesValidation="False" />
                             <asp:Button ID="btnShutdownReq" Text="ShutDown Request" CssClass="btnorange" 
                                     Width="140px" style="float :left" runat="server" Visible="false"
                                     onclick="btnShutdownReq_Click" />
                             </div>
                             <asp:Panel ID="plSpecs" runat="server" Width="100%" ScrollBars="None" >
                               <div style="height: 25px; padding :3px; text-align :left"; class="dottedscrollbox">
                                       <asp:UpdatePanel ID="UPPrint" runat="server" >
                                            <ContentTemplate>
                                                <asp:Button ID="btnPring" Text="Print" CssClass="btnorange" 
                                                    style="width:60px; float:right; margin:1px;" runat="server" 
                                                    onclick="btnPring_Click" CausesValidation="False" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:ImageButton ID="btnAddComponents" ImageUrl="~/Images/add1.png" 
                                           runat="server" ToolTip="Add Components" CausesValidation="False" 
                                           onclick="btnAddComponents_Click" />&nbsp;
                               <asp:ImageButton ID="imgbtneditSpec" ImageUrl="~/Images/edit1.png" runat="server" 
                                       ToolTip="Edit Specification" onclick="imgbtneditSpec_Click" 
                                           CausesValidation="False"  />
                               <asp:Label ID="lblSpecCompCode"  Font-Bold="true" runat="server"></asp:Label>                                        
                            </div>
                             <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader" visible="false" >
                              Component Specification
                           </div>
                           <table cellpadding="4" cellspacing="2" rules="all" border="1" style="border-collapse:collapse;background-color:#f9f9f9; border: #4371a5 1px solid;" width="99%" >
                           <tr>
                            <td>Linked To:&nbsp;</td>
                            <td>
                                <asp:Label ID="lblLinkedto" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                            </tr>
                            <tr>
                            <td >Component Code :</td>
                            <td>
                                <asp:Label ID="lblComponentCode" style="text-align:left" runat="server" Width="60px"></asp:Label>
                                <%--<asp:Label ID="lblUnitCode" style="text-align:left" runat="server" Visible="False" Width="30px"></asp:Label>--%>
                                </td>                                
                            </tr>
                            <tr>
                            <td>Component Name :</td>
                            <td>
                                <asp:Label  ID="lblComponentName" runat="server" Width="350px"></asp:Label>
                            </td>
                            </tr>
                            <tr><td>Maker :
                                </td>
                                <td>
                                    <asp:Label ID="lblMaker" runat="server"  ></asp:Label>
                                    <asp:TextBox ID="txtMaker" runat="server" MaxLength="50" Width="350px" Visible="false"></asp:TextBox>
                                    
                                </td>
                                </tr>
                            <tr> 
                                <td>
                                    Maker Type :
                                </td>
                                <td>
                                    <asp:Label ID="lblMakerType" runat="server" ></asp:Label>
                                    <asp:TextBox ID="txtMakerType" runat="server" MaxLength="50" Width="350px" Visible="false"></asp:TextBox>
                                    
                                </td>
                                  <tr> 
                                <td>
                                    Account Codes :
                                </td>
                                <td>
                                    <asp:Label ID="lblAccountCodes" runat="server" ></asp:Label>
                                    <asp:TextBox ID="txtAccountCodes" runat="server" MaxLength="50" Width="350px"  Visible="false"></asp:TextBox>
                                </td>
                            <tr><td>Description :<asp:HiddenField ID="HiddenField1" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblComponentDesc" runat="server" Width="350px" Height="66px" TextMode="MultiLine"></asp:Label>
                                    <asp:TextBox ID="txtComponentDesc" runat="server" Width="350px" Height="66px" TextMode="MultiLine" Visible="false"></asp:TextBox>
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
                                                <asp:CheckBox ID="chkClass" runat="server" Enabled="false" />
                                                Class EQIP
                                            </td>
                                            <td style="vertical-align:middle;text-align:right;">
                                                Class EQIP Code :&nbsp;</td>
                                            <td style="text-align:left">
                                                <asp:Label ID="lblClassCode" runat="server" ></asp:Label>
                                                <asp:TextBox ID="txtClassCode" runat="server" MaxLength="30" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:left;" colspan="3">
                                                <asp:CheckBox ID="chkCritical" runat="server" Enabled="false" />
                                                Critical EQIP
                                                <asp:CheckBox ID="chkCE" runat="server" Enabled="false" Text="Critical for Environment" Visible="false" />
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                            <td style="text-align:left;" colspan="2">
                                                <asp:CheckBox ID="chkInactive" runat="server" Enabled="false" />
                                                Inactive
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSaveSpec" runat="server" CssClass="btn" Text="Save" OnClick="btnSaveSpec_Click" 
                                                        Visible="false" />
                                                </td>
                                                <td style="text-align:left">
                                                    <asp:Button ID="btnCancelSpec" runat="server" CssClass="btn" Text="Cancel" 
                                                        Visible="false" onclick="btnCancelSpec_Click" CausesValidation="False" />
                                                </td>
                                            </tr>
                                        </table>
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </td>
                           </table>
                             
                             </asp:Panel>
                            <asp:Panel ID="plJobs" runat="server" Width="100%" ScrollBars="None" Visible="false">
                             <div style="height: 25px; padding :3px; text-align :left"; class="dottedscrollbox">
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                            <ContentTemplate>
                                                <asp:Button ID="btnPrintJobsReport" Text="Print" CssClass="btnorange" style="width:80px; float:right; margin:1;" runat="server" onclick="btnPrintJobsReport_Click" />
                                                <div style="float:right;margin-right:5px; ">
                                                    <b> Job Type : </b>
                                                    <%--1111111111111--%>
                                                    <asp:DropDownList ID="ddlJobType" runat="server" style="padding:4px;width:120px;" AutoPostBack="true" OnSelectedIndexChanged="ddlJobType_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div> 
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                             <asp:ImageButton ID="btnAddJobs" ImageUrl="~/Images/add1.png" runat="server" ToolTip="Assign New Jobs" onclick="btnAddJobs_Click" />
                             <asp:ImageButton ID="imgbtnEditJobs" ImageUrl="~/Images/edit1.png" runat="server" ToolTip="Edit Jobs" onclick="imgbtnEditJobs_Click"  />
                             <%--<asp:ImageButton ID="ImgbtnDeleteJobs" ImageUrl="~/Images/delete1.png" OnClientClick="javascript:return confirm('Are you sure to delete this job?')" runat="server" ToolTip="Delete Jobs" onclick="ImgbtnDeleteJobs_Click"  />  --%>                                                
                             
                             
                             <asp:Label ID="lblCompCodeJobs"  Font-Bold="true" runat="server"></asp:Label>                             
                             
                             
                            </div>
                             <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader" visible="false" >
                             Component Jobs
                           </div>
                             <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%" >
                             <tr>
          <td>
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                   <col style="width:30px;"  />
                   <col style="width:70px;" />
                   <col />
                   <col style="width:60px;" />
                   <col style="width:70px;" />
                   <col style="width:150px;" />
                   <col style="width:70px;" />
                   <col style="width:17px;" />
                   <tr align="left" class= "headerstylegrid">
                   <td></td>
                   <td>Job Cat.</td>
                   <td>Job Name</td>
                   <td>Dept.</td>
                   <td>Rank</td>
                   <td>Interval</td>
                   <td style="text-align:center;"><img title="Attachments" src="Images/paperclip.gif" style="cursor:pointer;" title="Attachment"/></td>
                   <td></td>
                   </tr>
             </colgroup>
           </table>           
           <div id="dvJobs"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 290px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                    <col style="width:30px;"  />
                    <col style="width:70px;" />
                    <col />
                    <col style="width:60px;" />
                    <col style="width:70px;" />
                    <col style="width:150px;" />
                    <col style="width:70px;" />
                    <col style="width:17px;" />
                    </colgroup>
               <asp:Repeater ID="rptVesselComponents" runat="server">
                  <ItemTemplate>
                      <tr class="row" ondblclick="OpenJobDesc('<%#Eval("CompJobId")%>','<%#Eval("VesselCode")%>')">
                           <td align="center"><asp:CheckBox ID="chkSelectJobs" runat="server" /></td>
                           <td align="left"><%#Eval("JobCode")%><asp:HiddenField ID="hfdJobId" Value='<%#Eval("CompJobId")%>' runat="server" /></td>
                           <td align="left"><%#Eval("JobName")%>
                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                           <asp:HiddenField ID="hfVcId" Value='<%#Eval("ComponentId")%>' runat="server" /></td>
                           <td align="center"><%#Eval("DeptName")%></td>
                           <td align="center"><%#Eval("RankCode")%></td>
                           <td align="center"><%#Eval("Interval")%></td>
                           <td>
                                <a href="ShipDocManagement.aspx?CJID=<%#Eval("CompJobID")%>&VesselCode=<%#ddlVessels.SelectedValue.ToString().Trim()%>" target="_blank" style='Display:<%#((Eval("AttachmentCount").ToString()=="0")?"none":"block")%>'> <img src="Images/paperclip.gif" style="border:none;" title="Attachment" /> </a>
                             <%--<img src="Images/paperclip.gif" title="Company Form" style='cursor:pointer;<%#(Eval("AttachmentForm").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("AttachmentForm")%>' onclick="showAttachment(this.getAttribute('file'))"/>
                             <img src="Images/paperclip.gif" title="Risk Assessment" style='cursor:pointer;<%#(Eval("RiskAssessment").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("RiskAssessment")%>' onclick="showRisk(this.getAttribute('file'))"/>
                             <img src="Images/paperclip.gif" title="Guidelines" style='cursor:pointer;<%#(Eval("Guidelines").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("Guidelines")%>' vcode='<%#Eval("VesselCode")%>' onclick="showGuidelines(this.getAttribute('file'),this.getAttribute('vcode'));"/>--%>
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
      <asp:Panel ID="plSpare" runat="server" Width="100%" Visible="false" >
       
       
       <div style="height: 25px; padding :3px; text-align :left" class="dottedscrollbox">
            
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
        <ContentTemplate>
            <asp:Button ID="btnSparePrint" Text="Print" CssClass="btnorange" style="width:80px; float:right; margin-left:10px;" runat="server" onclick="btnSparePrint_Click" />
        </ContentTemplate>
        </asp:UpdatePanel>       
        
            <asp:ImageButton ID="btnAddSpares" ImageUrl="~/Images/add1.png" runat="server" ToolTip="Assign New Spares" onclick="btnAddSpares_Click" />&nbsp;
            <asp:Label ID="lblCompCodeSpare"  Font-Bold="true" runat="server"></asp:Label>
        
       </div>
       
       <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader">
         Component Spares
       </div>
       <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%">
         <tr>
          <td>
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                       <%if (Session["IsEdit"].ToString().Trim() == "True" || Session["IsEdit"].ToString().Trim() == "")
                         {%>
                      <col style="width:30px;" />
                      <% }%>
                      <col style="width:30px;" />
                      <col style="width:30px;" />
                      <col />
                      <col style="width:200px;" />                  
                      <col style="width:60px;" />                     
                      <col style="width:40px;" />
                      <col style="width:160px;" />
                      <col style="width:25px;" />                                              
                      <col style="width:17px;" />
              <tr align="left" class= "headerstylegrid">
                      <%if (Session["IsEdit"].ToString().Trim() == "True" || Session["IsEdit"].ToString().Trim() == "")
                        {%> 
                      <td></td>
                      <% }%>
                   
                    <td></td>
                    <td></td>
                      <td>Spare Name</td>
                      <td>Part#</td>
                      <td>Qty(Min)</td>                      
                      <td>ROB</td>
                      <td>Location</td>
                      <td></td>
                      <td></td>    
              </tr>
              </colgroup>
           </table>
           <div id="dvSpares" onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 295px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all"style="width:100%;border-collapse:collapse;">
             <colgroup>
                       <%if (Session["IsEdit"].ToString().Trim() == "True" || Session["IsEdit"].ToString().Trim() == "")
                         {%>
                      <col style="width:30px;" />
                      <% }%>
                    <col style="width:30px;" />
                      <col style="width:30px;" />
                 
                      <col />
                      <col style="width:200px;" />  
                       <%--<col style="width:150px" />
                     <col style="width:90px;" />
                      <col style="width:100px" />
                      <col style="width:90px;" /> --%>
                      <col style="width:60px;" />
                     <%-- <col style="width:60px;" />--%>
                      <col style="width:40px;" />
                      <col style="width:160px;" />
                      <col style="width:25px;" />                  
                      <col style="width:17px;" />
             </colgroup>
             <asp:Repeater ID="rptComponentSpares" runat="server">
                 <ItemTemplate>
                         <tr class="row">
                               <%if (Session["IsEdit"].ToString().Trim() == "True" || Session["IsEdit"].ToString().Trim() == "")
                                 {%>
                              <td><asp:ImageButton ID="btnEdit" CommandArgument='<%#Eval("SpareId") %>' ToolTip="Edit Spare" ImageUrl="~/Images/editX12.jpg" OnClick="btnEdit_Click" runat="server" ></asp:ImageButton></td>
                              <% }%>

                             
                             
                              <td><asp:ImageButton ID="btnEditStock" CommandArgument='<%#Eval("SpareId") %>' ToolTip="Edit Stock" ImageUrl="~/Images/stock.png" OnClick="btnEditStock_Click" runat="server" ></asp:ImageButton></td>
                             <td>
                                 <asp:ImageButton ID="btnCopySpareLink" CommandArgument='<%#Eval("SpareId") %>' ToolTip="Move Spare" ImageUrl="~/Images/Move1.png" OnClick="btnCopySpareLink_Click" runat="server" ></asp:ImageButton>
                                 
                             </td>

                              <td  align="left"><%#Eval("SpareName")%>
                                  <asp:HiddenField ID="hfOffice_Ship" Value='<%#Eval("Office_Ship") %>' runat="server" /> 
                                  <asp:HiddenField ID="hfCompID" Value='<%#Eval("ComponentId") %>' runat="server" /> 
                                  
                                  
                                  <%#"<span class='CriticalType_" + Eval("Critical").ToString() + "'>" + Eval("Critical").ToString() + "</span>"%>
                                                
                              </td>
                              <td align="left"><%#Eval("PartNo")%>   </td>
                              <%--<td  align="left">
                              <%#Eval("Maker")%>                                                                                    
                              </td>--%>
                              
                              <%--<td align="left"><%#Eval("PartName")%>   </td>
                              <td align="left"><%#Eval("DrawingNo")%>   </td>--%>
                              <td ><%#Eval("MinQty")%></td>
                              <%--<td ><%#Eval("MaxQty")%></td>--%>
                              <td style="<%#((Common.CastAsInt32(Eval("ROB")) < Common.CastAsInt32(Eval("MinQty"))) ? "background-color:red;color:white;" : "")%>"><%#Eval("ROB")%></td>
                              <td><%#Eval("StockLocation")%></td>
                              <td>
                                <a runat="server" ID="ancFile"  href='<%# "~/UploadFiles/UploadSpareDocs/" + Eval("Attachment").ToString() %>' target="_blank" visible='<%#Eval("Attachment").ToString()!= "" %>'  title="Show Attached File" >
                                 <img src="Images/paperclip.gif" style="border:none"  /></a>
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
          
          <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="dvMoveSpare" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:525px; height:130px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
                <%--<asp:UpdatePanel runat="server" ID="UpdatePanel5">
                <ContentTemplate>--%>
                <center>
                    <table cellpadding="4" cellspacing="3" width="100%">
                    <tr>
                     <td colspan="3" style="background-color:#d2d2d2; font-size:14px; font-weight:bold; height:25px; vertical-align:middle; text-align:center;"> Move Spare  </td>
                    </tr>
                    <tr>
                        <td >
                                <asp:DropDownList ID="ddlComponent" runat="server" style="padding:4px;width:220px;"></asp:DropDownList>
                            
                        </td>
                        <td>
                            <asp:Button ID="btnMoveSpare" runat="server" CssClass="btn" Text="Move" OnClick="btnMoveSpare_OnClick" />
                        </td>
                        <td>
                            <asp:Button ID="btnCloseMoveSpare" Text="Close" CssClass="btn" OnClick="btnCloseMoveSpare_Click" runat="server" />
                        </td>
                    </tr>
                    </table>
                    <div style="padding-top:0px;margin:10px;border:solid 0px red;padding:10px;margin-left:0px;">
                        <uc1:MessageBox ID="MessageBox2" runat="server"  />
                    </div>
                    
                </center>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
                </center>
        </div>        
     </asp:Panel>
      <asp:Panel ID="PlHistory" runat="server" Width="100%" Visible="false" >
        <div style="height: 25px; padding :3px; text-align :left" class="dottedscrollbox">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
            <ContentTemplate>
            
                <asp:Button ID="btnPrintHistory" Text="Print" CssClass="btnorange" style="width:80px; float:right; margin:1;" runat="server" OnClick="btnPrintHistory_Click" />
                    <div style="float:right;margin-right:5px; ">
                    <b> Job Type : </b>
                    <asp:DropDownList ID="ddlJobType_H" runat="server" style="padding:4px;width:120px;" AutoPostBack="true" OnSelectedIndexChanged="ddlJobType_H_OnSelectedIndexChanged"></asp:DropDownList>
                </div> 
            </ContentTemplate>
       </asp:UpdatePanel>
       </div>
       
       <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader">
         History
           <%--2222222222222--%>

           
       </div>
       <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%">
     
         <tr>
          <td>
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                   <colgroup>
                       <col style="width:60px;" />
                       <col style="width:90px;" />
                       <col style="width:90px;" />
                       <col style="width:80px;" />
                       <col style="width:80px;" />
                       <col style="width:80px;" />
                       <col />
                       <col style="width:70px;" />
                       <col style="width:250px;" />
                       <col style="width:17px;" />
                   </colgroup>
                   <tr align="left" class= "headerstylegrid">
                       <td>Job</td>
                       <td>Due Date</td>
                       <td>Done Date</td>
                       <td>Due Hr.</td>
                       <td>Done Hr.</td>
                       <td>Done By</td>
                       <td>Job Desc</td>
                       <td>Action</td>
                       <td>Equip. Condn</td>                                                  
                       <td></td>
                   </tr>
           </table>
           <div id="divHistory"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 300px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                       <col style="width:60px;" />
                       <col style="width:90px;" />
                       <col style="width:90px;" />
                       <col style="width:80px;" />
                       <col style="width:80px;" />
                       <col style="width:80px;" />
                       <col />
                       <col style="width:70px;" />
                       <col style="width:250px;" />
                       <col style="width:17px;" />
               </colgroup>
               <asp:Repeater ID="rptJobHistory" runat="server">
                  <ItemTemplate>
                      <tr class="row" title="Click to view details" onclick="opendetails('<%#Eval("Action")%>','<%#Eval("PK")%>','<%#Eval("VesselCode")%>');" style="cursor:pointer">
                           <td align="left"><%#Eval("JobCode")%></td>
                           <td align="left"><%#Eval("DueDate")%>
                                        <asp:HiddenField ID="hfHid" Value='<%#Eval("PK")%>' runat="server" />
                                        <asp:HiddenField ID="hfVC" Value='<%#Eval("VesselCode")%>' runat="server" /></td>
                           <td align="left"><%#Eval("ACTIONDATE")%></td>
                           <td align="center"><%# Eval("DueHour").ToString() == "0" ? "" : Eval("DueHour").ToString()%></td>
                           <td align="center"><%# Eval("DoneHour").ToString() == "0" ? "" : Eval("DoneHour").ToString()%></td>
                           <td align="left" style=" text-align:center"><%#Eval("DoneBy")%></td>
                           <td align="left"><%#Eval("JobDesc")%></td>
                           <td align="left"><%#Eval("Action")%></td>
                           <td align="left"><%#Eval("ConditionAfter")%></td>
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
      <asp:Panel ID="plShutdown" runat="server" Width="100%" Visible="false" >
      <div id="divSDRequest" runat="server" visible="false" style="height: 25px; padding :3px; text-align :left" class="dottedscrollbox">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
            <ContentTemplate>
                <asp:Button ID="btnAddShutdownReq" Text="Add" CssClass="btnorange" style="width:80px; float:right; margin:1;" runat="server" CausesValidation="false" OnClick="btnAddShutdownReq_Click" />
            </ContentTemplate>
       </asp:UpdatePanel>
       </div>
       <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader">
         Critical Equipment Shutdown Request
       </div>
       <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%">
     
         <tr>
          <td>
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                   <colgroup>
                       <col style="width:90px;" />
                       <col />
                       <col style="width:120px;" />
                       <col style="width:150px;" />
                       <col style="width:150px;" />
                       <col style="width:80px;" />
                       <col style="width:17px;" />
                   </colgroup>
                   <tr align="left" class="blueheader">
                       <td>Request Dt.</td>
                       <td>Master/CE Name</td>
                       <td>Planned Shutdown (Total Hours)</td>
                       <td>Planned From Date/Time (Ship’s LT)</td>
                       <td>Planned To Date/Time (Ship’s LT)</td>
                       <td>Approved</td>                                 
                       <td></td>
                   </tr>
           </table>
           <div id="divShutdown"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 300px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                       <col style="width:90px;" />
                       <col />
                       <col style="width:120px;" />
                       <col style="width:150px;" />
                       <col style="width:150px;" />
                       <col style="width:80px;" />
                       <col style="width:17px;" />
               </colgroup>
               <asp:Repeater ID="rptShutdown" runat="server">
                  <ItemTemplate>
                      <tr class="row" onclick="openShutdownwindow('<%#Eval("CompCode")%>','<%#Eval("VesselCode")%>','<%#Eval("ShutdownId")%>');" style="cursor:pointer">
                           <td align="left"><%#Eval("RequestDate")%></td>
                           <td align="left"><%#Eval("MasterCEName")%></td>
                           <td align="right"><%#Eval("Pl_ShutDownTotalHrs")%></td>
                           <td align="center"><%# Convert.ToDateTime(Eval("Pl_FromDateTime").ToString().Split(' ').GetValue(0).ToString()).ToString("dd-MMM-yyyy") + "/ " + Eval("Pl_FromDateTime").ToString().Split(' ').GetValue(1).ToString()%></td>
                           <td align="center"><%# Convert.ToDateTime(Eval("Pl_ToDateTime").ToString().Split(' ').GetValue(0).ToString()).ToString("dd-MMM-yyyy") + "/ " + Eval("Pl_ToDateTime").ToString().Split(' ').GetValue(1).ToString()%></td> 
                           <td align="center"><%#Eval("Approved")%></td>
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

    <div style="padding-top:5px;">
        <uc1:MessageBox ID="MessageBox1" runat="server" />
    </div>
     </td>
     </tr></table>
     </ContentTemplate>
             <Triggers>
             <asp:PostBackTrigger  ControlID="btnCreateDB" /> 
             </Triggers> 
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

        
     <mtm:footer ID="footer1" runat ="server" />
    </form>
</body>
</html>
