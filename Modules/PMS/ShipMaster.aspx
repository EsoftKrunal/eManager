<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ShipMaster.aspx.cs" Inherits="ShipMaster" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
     <link href="CSS/style.css" rel="stylesheet" type="text/css" />
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
            document.getElementById('ctl00_ContentMainMaster_hfSearchCode').value = CompCode;
           // alert(CompCode);
           // window.opener.document.getElementById('btnSearchedCode').click();
            document.getElementById('ctl00_ContentMainMaster_btnSearchedCode').click();
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
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function opencompreport(Mode, Vessel) {
            window.open('Reports/OfficeComponentReport.aspx?Mode=' + Mode + '&Vessel=' + Vessel, '', '');
        }
        function opencompjobreport(Mode, Vessel) {
            window.open('Reports/OfficeComponentJobsReport.aspx?Mode=' + Mode + '&Vessel=' + Vessel, '', '');
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
            window.open('Ship_AddJobsToComponents.aspx?CompCode=' + CompCode + '&&VC=' + VC, '', 'status=1,toolbar=0,menubar=0,resizable=1,width=1200,,height=490');

        }
        function openeditjobwindow(CompCode, VC, JobIds) {
            window.open('Ship_EditComponentJobs.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&JobIds=' + JobIds, '', 'status=1,toolbar=0,menubar=0,resizable=1,width=1200,,height=580');
        }
        function reloadunits() {

           // __doPostBack('btnRefresh', '');
            document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();
        }
        function reloadtree(CompCode) {
            //__doPostBack('btnRefTv', '');
            document.getElementById('ctl00_ContentMainMaster_hfSearchCode').value = CompCode;
            document.getElementById('ctl00_ContentMainMaster_btnRefTv').click();
        }
        function OpenJobDesc(CompJobId, VesselCode) {
            window.open('Reports/VSL_JobDesc.aspx?CompJobId=' + CompJobId + '&VSL=' + VesselCode, '', '');
        }
        //        function ShowHistory(VC,CID,JID)
        //       {
        //           window.open('JobUpdateHistory.aspx?VC='+ VC +'&&CID='+ CID + '&&JID='+ JID,'','');
        //       }
        function opendetails(RP, HID, VC, UT) {
            var UT = '<%=UserType%>';
            if (RP == 'REPORT') {
                RP = 'R';

                if (UT == "S") {
                    window.open('JobCard.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
                }
                else {
                    window.open('JobCard_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
                }
            }
            if (RP == 'POSTPONE') {
                RP = 'P';
                if (UT == "S")
                    window.open('PopupHistoryJobDetails.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
                else {
                    //window.open('JobCard_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
                    window.open('PopupHistoryJobDetails_Office.aspx?VC=' + VC + '&&HID=' + HID + '&&RP=' + RP, '', '');
                }
            }
            if (RP == 'DEFECT') {
                window.open('Popup_BreakDown.aspx?DN=' + HID, '', '');
            }
            if (RP == 'UNPLANNED') {
                window.open('Popup_AddUnPlanJob.aspx?VSL=' + VC + '&UPId=' + HID, '', '');
            }
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
        function openShutdownwindow(CompCode, VC, SdID) {
            var usertype = '<%=Session["UserType"].ToString()%>';
            if (usertype == 'O')
                window.open('Office_CriticalEqpShutdownReq.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SD=' + SdID, '', '');
            else
                window.open('VSL_CriticalEqpShutdownReq.aspx?CompCode=' + CompCode + '&&VC=' + VC + '&&SD=' + SdID, '', '');
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
         .auto-style1 {
             width: 350px;
         }
         </style>
      <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
         <div style=" text-align :center; padding-top :4px;" class="text headerband"  >
           Ship Master
     </div>
        <table style="width :100%" cellpadding="0" cellspacing="0">
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
                            <td id="tdComponentTree" runat="server" style="text-align:left; vertical-align:top; padding-top:5px;" class="auto-style1">  
                             <div style="padding-bottom:5px" >
                             
                             <%-- <asp:ImageButton ID="btnSearchComponents" ImageUrl ="~/Modules/PMS/Images/find.jpg" 
                                     runat="server" onclick="btnSearchComponents_Click" ToolTip="Search Components" 
                                     Visible="false" style="float:right; margin-right :5px;" 
                                     CausesValidation="False"/>--%>
                                   <span style="float :right">
                              <asp:Button ID="btnSearchComponents" ToolTip="Print Jobs" runat="server" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Search" CssClass="btn" ForeColor="Black" Width="70px" 
                                        OnClick="btnSearchComponents_Click" 
                                        style="background-image:url(Images/find.jpg); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" 
                                        CausesValidation="False"/>       
                                     </span>
                              <asp:DropDownList ID="ddlVessels" runat="server" AutoPostBack="true" Width="272px" onselectedindexchanged="ddlVessels_SelectedIndexChanged" ></asp:DropDownList> 
                            </div>                                                            
                                <div ID="dvscroll_Componenttree" class="scrollbox" onscroll="SetScrollPos(this)" style="width :350px; overflow-y:scroll; overflow-x: hidden;  height :390px;">
                                    <asp:TreeView ID="tvComponents" ShowLines="true" runat="server" OnTreeNodePopulate="tvComponents_TreeNodePopulate" onselectednodechanged="tvComponents_SelectedNodeChanged">
                                <LevelStyles>
                                  <asp:TreeNodeStyle Font-Underline="False" ForeColor="Red"  />
                                  <asp:TreeNodeStyle Font-Underline="False" ForeColor="Purple" Font-Size="Medium" />
                                  <asp:TreeNodeStyle Font-Underline="False" ForeColor="DarkGreen" Font-Size="Small"/>
                                  <asp:TreeNodeStyle Font-Underline="False" Font-Size="Small" />
                                  <asp:TreeNodeStyle Font-Underline="False" ForeColor="Black" Font-Size="Small"/>
                                  
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
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Create DB"  Width="90px" ForeColor="Black"
                                        OnClick="btnCreateDB_Click" 
                                        style="background-position: 3px 3px; background-image:url('/Modules/PMS/Images/database.gif'); background-repeat: no-repeat; border :solid 1px gray; height: 27px;" 
                                        CausesValidation="False" />
                                    <asp:Button ID="btnPrintCompList" ToolTip="Print Components" runat="server" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Components"  Width="110px" ForeColor="Black" 
                                        OnClick="btnPrintCompList_Click" 
                                        style="background-image:url(/Modules/PMS/Images/printer16x16.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px  " 
                                        CausesValidation="False" />
                                    <asp:Button ID="btnPrintJobs" ToolTip="Print Jobs" runat="server" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Jobs"  Width="60px" 
                                        OnClick="btnPrintJobs_Click" ForeColor="Black"
                                        style="background-image:url(/Modules/PMS/Images/printer16x16.png); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" 
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
                             <asp:Button ID="btnSpecification" Text="Specification" CssClass="selbtn" 
                                     Width="140px" style="float :left" runat="server" 
                                     onclick="btnSpecification_Click" CausesValidation="False"  />                             
                             <asp:Button ID="btnAssignJobs" Text="Jobs" CssClass="btn1" Width="140px" 
                                     style="float :left" runat="server" onclick="btnAssignJobs_Click" 
                                     CausesValidation="False" />
                             <asp:Button ID="btnAssignSpare" Text="Spares" CssClass="btn1" Width="140px" 
                                     style="float :left" runat="server" onclick="btnAssignSpare_Click" 
                                     CausesValidation="False" />
                             <%--<asp:Button ID="btnAssignRunningHour" Text="Running Hour " CssClass="btn" Width="160px" style="float :left" runat="server" onclick="btnAssignRunningHour_Click"  />--%>
                             <asp:Button ID="btnJobHistory" Text="History" CssClass="btn1" Width="140px" 
                                     style="float :left" runat="server" onclick="btnJobHistory_Click" 
                                     CausesValidation="False" />
                             <asp:Button ID="btnShutdownReq" Text="ShutDown Request" CssClass="btn1" 
                                     Width="140px" style="float :left" runat="server" Visible="false"
                                     onclick="btnShutdownReq_Click" />
                             </div>
                             <asp:Panel ID="plSpecs" runat="server" Width="100%" ScrollBars="None" >
                               <div style="height: 30px; padding :3px; text-align :left;vertical-align:top;"; class="dottedscrollbox">
                                       <asp:UpdatePanel ID="UPPrint" runat="server" >
                                            <ContentTemplate>
                                                <asp:Button ID="btnPring" Text="Print" CssClass="btn" 
                                                    style="width:60px; float:right; margin:1px;" runat="server" 
                                                    onclick="btnPring_Click" CausesValidation="False" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:ImageButton ID="btnAddComponents" ImageUrl="~/Modules/PMS/Images/add1.png" 
                                           runat="server" ToolTip="Add Components" CausesValidation="False" 
                                           onclick="btnAddComponents_Click" />&nbsp;
                               <asp:ImageButton ID="imgbtneditSpec" ImageUrl="~/Modules/PMS/Images/edit1.png" runat="server" 
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
                                    
                                    <asp:LinkButton ID="lnkOpenSuppliers" runat="server" Text="[ + Suppliers ]" OnClick="lnkOpenSuppliers_OnClick" Font-Bold="true" Visible="false"></asp:LinkButton>
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
                                            <td style="text-align:left;" colspan="2">
                                                <asp:CheckBox ID="chkOR" runat="server" Enabled="false" />
                                                OR
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="text-align:left;" colspan="2">
                                                <asp:CheckBox ID="chkRHComponent" runat="server" Enabled="false" /> RHComponent
                                                
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
                             <div style="height: 30px; padding :3px; text-align :left;vertical-align:top;"; class="dottedscrollbox">
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                            <ContentTemplate>
                                                <asp:Button ID="btnPrintJobsReport" Text="Print" CssClass="btn" style="width:80px; float:right; margin:1;" runat="server" onclick="btnPrintJobsReport_Click" />
                                                <div style="float:right;margin-right:5px; ">
                                                    <b> Job Type : </b>
                                                    <%--1111111111111--%>
                                                    <asp:DropDownList ID="ddlJobType" runat="server" style="padding:4px;width:120px;" AutoPostBack="true" OnSelectedIndexChanged="ddlJobType_OnSelectedIndexChanged"></asp:DropDownList>
                                                </div> 
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                             <asp:ImageButton ID="btnAddJobs" ImageUrl="~/Modules/PMS/Images/add1.png" runat="server" ToolTip="Assign New Jobs" onclick="btnAddJobs_Click" />
                             <asp:ImageButton ID="imgbtnEditJobs" ImageUrl="~/Modules/PMS/Images/edit1.png" runat="server" ToolTip="Edit Jobs" onclick="imgbtnEditJobs_Click"  />
                             <%--<asp:ImageButton ID="ImgbtnDeleteJobs" ImageUrl="~/Modules/PMS/Images/delete1.png" OnClientClick="javascript:return confirm('Are you sure to delete this job?')" runat="server" ToolTip="Delete Jobs" onclick="ImgbtnDeleteJobs_Click"  />  --%>                                                
                             
                             
                             <asp:Label ID="lblCompCodeJobs"  Font-Bold="true" runat="server"></asp:Label>                             
                             
                             
                            </div>
                             <div style="height: 20px; text-align :center; padding-top :4px;" class="orangeheader" visible="false" >
                             Component Jobs
                           </div>
                             <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%" >
                             <tr>
          <td>
              <div id="dvJobs"  style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 25px ; text-align:center;">
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                   <col style="width:5%;"  />
                   <col style="width:10%;" />
                   <col style="width:20%;"/>
                   <col style="width:10%;" />
                   <col style="width:10%;" />
                   <col style="width:15%;" />
                   <col style="width:15%;" />
               
                   <col style="width:5%;" />
                   <col style="width:5%;" />
                   <col style="width:5%;" />
                   
                   
                   <tr align="left" class="headerstylegrid">
                   <td style="width:5%;"></td>
                   <td style="width:10%;">Job Cat.</td>
                   <td style="width:20%;">Job Name</td>
                   <td style="width:10%;">Dept.</td>
                   <td style="width:10%;">Rank</td>
                   <td style="width:15%;">Interval</td>
                   <td style="width:15%;">Est. Job Cost(USD)</td>
                   <td style="text-align:center;width:5%;"><img title="Attachments" src="Images/paperclip.gif" style="cursor:pointer;" /></td>
                   <td style="text-align:center;width:5%;"><img title="Spares" src="Images/gtk-spares.png" style="cursor:pointer;" /></td>
                   <td style="text-align:center;width:5%;"><img title="Copy Job" src="Images/copy-blue.png" style="cursor:pointer;"  /></td>
                   
                   </tr>
             </colgroup>
           </table>           
                </div>
           <div id="dvJobs"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 290px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                    <col style="width:5%;"  />
                    <col style="width:10%;" />
                    <col style="width:20%;"/>
                    <col style="width:10%;"/>
                    <col style="width:10%;" />
                    <col style="width:15%;" />
                   <col style="width:15%;" />
                   <col style="width:5%;" />
                   <col style="width:5%;" />
                   <col style="width:5%;" /> 
                    </colgroup>
               <asp:Repeater ID="rptVesselComponents" runat="server">
                  <ItemTemplate>
                      <tr  ondblclick="OpenJobDesc('<%#Eval("CompJobId")%>','<%#Eval("VesselCode")%>')">
                           <td align="center" style="width:5%;"><asp:CheckBox ID="chkSelectJobs" runat="server" /></td>
                           <td align="left" style="width:10%;"><%#Eval("JobCode")%><asp:HiddenField ID="hfdJobId" Value='<%#Eval("CompJobId")%>' runat="server" /></td>
                           <td align="left" style="width:20%;"><%#Eval("JobName")%>
                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                           <asp:HiddenField ID="hfVcId" Value='<%#Eval("ComponentId")%>' runat="server" /></td>
                           <td align="center" style="width:10%;"><%#Eval("DeptName")%></td>
                           <td align="center" style="width:10%;"><%#Eval("RankCode")%></td>
                           <td align="center" style="width:15%;"><%#Eval("Interval")%></td>
                           <td align="center" style="text-align:right;width:15%;"><%#Eval("JobCost")%></td>
                           <td style="width:5%;">
                                <a href="ShipDocManagement.aspx?CJID=<%#Eval("CompJobID")%>&VesselCode=<%#ddlVessels.SelectedValue.ToString().Trim()%>" target="_blank" style='Display:<%#((Eval("AttachmentCount").ToString()=="0")?"none":"block")%>'> <img src="Images/paperclip.gif" style="border:none;" title="Attachment" /> </a>
                             <%--<img src="Images/paperclip.gif" title="Company Form" style='cursor:pointer;<%#(Eval("AttachmentForm").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("AttachmentForm")%>' onclick="showAttachment(this.getAttribute('file'))"/>
                             <img src="Images/paperclip.gif" title="Risk Assessment" style='cursor:pointer;<%#(Eval("RiskAssessment").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("RiskAssessment")%>' onclick="showRisk(this.getAttribute('file'))"/>
                             <img src="Images/paperclip.gif" title="Guidelines" style='cursor:pointer;<%#(Eval("Guidelines").ToString().Trim()=="")?"display:none":""%>' file='<%#Eval("Guidelines")%>' vcode='<%#Eval("VesselCode")%>' onclick="showGuidelines(this.getAttribute('file'),this.getAttribute('vcode'));"/>--%>
                           </td>
                           <td style="width:5%;">
                                <a href="ShipJobSpareRequirement.aspx?ComponentId=<%#Eval("ComponentId")%>&CJID=<%#Eval("CompJobID")%>&VesselCode=<%#ddlVessels.SelectedValue.ToString().Trim()%>" target="_blank"> <img src="Images/gtk-spares.png" style="border:none;" title="Spare Requirement." /> </a>
                           </td>
                          <td style="width:5%;">
                              <div runat="server" visible='<%#(CanUdateJob==true)%>'>
                                <a href="UpdateJobInterval.aspx?ComponentId=<%#Eval("ComponentId")%>&CJID=<%#Eval("CompJobID")%>&VesselCode=<%#ddlVessels.SelectedValue.ToString().Trim()%>" target="_blank"> <img src="Images/copy-blue.png" style="border:none;" title="Copy Jobs" /> </a>
                                  </div>
                           </td>
                          
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
       
       
       <div style="height: 30px; padding :3px; text-align :left;vertical-align:top;" class="dottedscrollbox" >
          
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
        <ContentTemplate>
            <asp:Button ID="btnSparePrint" Text="Print" CssClass="btn" style="width:80px; float:right; margin-left:10px;" runat="server" onclick="btnSparePrint_Click" />
        </ContentTemplate>
        </asp:UpdatePanel>       
          <asp:DropDownList runat="server" ID="dlsparestatus" AutoPostBack="true" style="width:80px; float:right; margin-left:10px; margin-top:0px;" OnSelectedIndexChanged="dlsparestatus_OnSelectedIndexChanged">
                <asp:ListItem Text="Status" Value=""></asp:ListItem>
                <asp:ListItem Text="Active" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="InActive" Value="I"></asp:ListItem>
            </asp:DropDownList>

            <asp:ImageButton ID="btnAddSpares" ImageUrl="~/Modules/PMS/Images/add1.png" runat="server" ToolTip="Assign New Spares" onclick="btnAddSpares_Click" />&nbsp;
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
                       <%if (Session["IsEdit"] != null && (Session["IsEdit"].ToString().Trim() == "True" || Session["IsEdit"].ToString().Trim() == ""))
                         {%>
                      <col style="width:5%;" />
                      <% }%>
                      <col style="width:5%;" />
                      <col style="width:5%;" />
                      <col style="width:23%;"/>
                      <col style="width:15%;" />                  
                      <col style="width:5%;" />                     
                      <col style="width:5%;" />
                      <col style="width:15%;" />
                      <col style="width:10%;" />
                      <col style="width:5%;" />                                              
                      <col style="width:2%;" />
              <tr align="left" class="headerstylegrid">
                      <%if (Session["IsEdit"] != null && (Session["IsEdit"].ToString().Trim() == "True" || Session["IsEdit"].ToString().Trim() == ""))
                        {%> 
                      <td style="width:5%;"></td>
                      <% }%>
                   
                    <td style="width:5%;"></td>
                    <td style="width:5%;"></td>
                      <td style="width:23%;">Spare Name</td>
                      <td style="width:15%;">Part#</td>
                      <td style="width:5%;">Qty(Min)</td>                      
                      <td style="width:5%;">ROB</td>
                      <td style="width:15%;">Location</td>
                      <td style="width:10%;">Status</td>
                      <td style="width:5%;"></td>
                      <td style="width:2%;"></td>    
              </tr>
              </colgroup>
           </table>
           <div id="dvSpares" onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 270px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all"style="width:100%;border-collapse:collapse;">
             <colgroup>
                       <%if (Session["IsEdit"] != null && (Session["IsEdit"].ToString().Trim() == "True" || Session["IsEdit"].ToString().Trim() == ""))
                         {%>
                      <col style="width:5%;" />
                      <% }%>
                    <col style="width:5%;" />
                      <col style="width:5%;" />
                 
                      <col style="width:23%;"/>
                      <col style="width:15%;" />                  
                      <col style="width:5%;" />                     
                      <col style="width:5%;" />
                      <col style="width:15%;" />
                      <col style="width:10%;" />
                      <col style="width:5%;" />                                              
                      <col style="width:2%;" />
             </colgroup>
             <asp:Repeater ID="rptComponentSpares" runat="server">
                 <ItemTemplate>
                         <tr >
                               <%if (Session["IsEdit"] != null && (Session["IsEdit"].ToString().Trim() == "True" || Session["IsEdit"].ToString().Trim() == ""))
                                 {%>
                              <td style="width:5%;"><asp:ImageButton ID="btnEdit" CommandArgument='<%#Eval("SpareId") %>' ToolTip="Edit Spare" ImageUrl="~/Modules/PMS/Images/editX12.jpg" OnClick="btnEdit_Click" runat="server" ></asp:ImageButton></td>
                              <% }%>

                             
                             
                              <td style="width:5%;"><asp:ImageButton ID="btnEditStock" CommandArgument='<%#Eval("SpareId") %>' ToolTip="Edit Stock" ImageUrl="~/Modules/PMS/Images/stock.png" OnClick="btnEditStock_Click" runat="server" ></asp:ImageButton></td>
                             <td style="width:5%;">
                                 <asp:ImageButton ID="btnCopySpareLink" CommandArgument='<%#Eval("SpareId") %>' ToolTip="Move Spare" ImageUrl="~/Modules/PMS/Images/Move1.png" OnClick="btnCopySpareLink_Click" runat="server" ></asp:ImageButton>
                                 
                             </td>

                              <td  align="left" style="width:23%;"><%#Eval("SpareName")%>
                                  <asp:HiddenField ID="hfOffice_Ship" Value='<%#Eval("Office_Ship") %>' runat="server" /> 
                                  <asp:HiddenField ID="hfCompID" Value='<%#Eval("ComponentId") %>' runat="server" /> 
                                  
                                  
                                  <%#"<span class='CriticalType_" + Eval("Critical").ToString() + "'>" + Eval("Critical").ToString() + "</span>"%>
                                                
                              </td>
                              <td align="left" style="width:15%;"><%#Eval("PartNo")%>   </td>
                              <%--<td  align="left">
                              <%#Eval("Maker")%>                                                                                    
                              </td>--%>
                              
                              <%--<td align="left"><%#Eval("PartName")%>   </td>
                              <td align="left"><%#Eval("DrawingNo")%>   </td>--%>
                              <td style="width:5%;"><%#Eval("MinQty")%></td>
                              <%--<td ><%#Eval("MaxQty")%></td>--%>
                              <td style="<%#((Common.CastAsInt32(Eval("ROB")) < Common.CastAsInt32(Eval("MinQty"))) ? "background-color:red;color:white;width:5%;" : "")%>"><%#Eval("ROB")%></td>
                              <td style="width:15%;"><%#Eval("StockLocation")%></td>
                             <td style="width:10%;"><%#((Eval("status1").ToString().Trim()=="I")?"Inactive":"Active")%>

</td>
                              <td style="width:5%;">
                                <a runat="server" ID="ancFile"  href='<%# "~/EMANAGERBLOB/PMS/UploadFiles/UploadSpareDocs/" + Eval("Attachment").ToString() %>' target="_blank" visible='<%#Eval("Attachment").ToString()!= "" %>'  title="Show Attached File" >
                                 <img src="Images/paperclip.gif" style="border:none"  /></a>
                              </td>
                              <td style="width:2%"></td>
                          </tr>
                  </ItemTemplate>
             </asp:Repeater>
            </table>
           
           </div>
               <br />
            <div style="text-align:left;">
                <%--<asp:Repeater ID="rptComponentSparesPaging" runat="server" OnItemCommand="rptComponentSparesPaging_ItemCommand">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkPage" runat="server" CommandArgument="<%# Container.DataItem %>" CommandName="Page"  Style="padding: 5px 3px 5px 3px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-size:11px;"><%# Container.DataItem %>  
            </asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>--%>
<asp:LinkButton ID="lbPrevious" runat="server" Enabled="false" OnClick="lnkbuttonPrev_Click"  Style=" padding: 5px 3px 5px 3px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-size:11px;">Previous page</asp:LinkButton> &nbsp;
<asp:LinkButton ID="lbNext" runat="server" OnClick="lnkbuttonNext_Click" Style=" padding: 5px 3px 5px 3px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-size:11px;">Next page</asp:LinkButton> &nbsp;
             <b> Component Spares Count : </b>   <asp:Label ID="lblSpareCount" runat="server" Text="0" Style=" padding: 5px 3px 5px 3px; margin: 2px; background: lightgray; border: solid 1px #666; color: red; font-size:11px;"></asp:Label> 
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
                     <td colspan="3" class="text headerband"> Move Spare  </td>
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
        <div style="height: 30px; padding :3px; text-align :left;vertical-align:top;" class="dottedscrollbox">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
            <ContentTemplate>
            
                <asp:Button ID="btnPrintHistory" Text="Print" CssClass="btn" style="width:80px; float:right; margin:1;" runat="server" OnClick="btnPrintHistory_Click" />
                    <div style="float:right;margin-right:5px;vertical-align:top; ">
                        <table>
                            <tr>
                                <td>
                                    <b> Job Type : </b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlJobType_H" runat="server" style="padding:4px;width:120px;" AutoPostBack="true" OnSelectedIndexChanged="ddlJobType_H_OnSelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
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
                       <col style="width:6%;" />
                       <col style="width:10%;" />
                       <col style="width:10%;" />
                       <col style="width:7%;" />
                       <col style="width:7%;" />
                       <col style="width:7%;" />
                       <col style="width:24%;"/>
                       <col style="width:7%;" />
                       <col style="width:20%;" />
                       <col style="width:2%;" />
                   </colgroup>
                   <tr align="left" class="headerstylegrid">
                       <td style="width:6%;">Job</td>
                       <td style="width:10%;">Due Date</td>
                       <td style="width:10%;">Done Date</td>
                       <td style="width:7%;">Due Hr.</td>
                       <td style="width:7%;">Done Hr.</td>
                       <td style="width:7%;">Done By</td>
                       <td style="width:24%;">Job Desc</td>
                       <td style="width:7%;">Action</td>
                       <td style="width:20%;">Equip. Condn</td>                                                  
                       <td style="width:2%;"></td>
                   </tr>
           </table>
           <div id="divHistory"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 300px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                       <col style="width:6%;" />
                       <col style="width:10%;" />
                       <col style="width:10%;" />
                       <col style="width:7%;" />
                       <col style="width:7%;" />
                       <col style="width:7%;" />
                       <col style="width:24%;"/>
                       <col style="width:7%;" />
                       <col style="width:20%;" />
                       <col style="width:2%;" />
               </colgroup>
               <asp:Repeater ID="rptJobHistory" runat="server">
                  <ItemTemplate>
                      <tr  title="Click to view details" onclick="opendetails('<%#Eval("Action")%>','<%#Eval("PK")%>','<%#Eval("VesselCode")%>');" style="cursor:pointer">
                           <td align="left" style="width:6%;"><%#Eval("JobCode")%></td>
                           <td align="left" style="width:10%;"><%#Eval("DueDate")%>
                                        <asp:HiddenField ID="hfHid" Value='<%#Eval("PK")%>' runat="server" />
                                        <asp:HiddenField ID="hfVC" Value='<%#Eval("VesselCode")%>' runat="server" /></td>
                           <td align="left" style="width:10%;"><%#Eval("ACTIONDATE")%></td>
                           <td align="center" style="width:7%;"><%# Eval("DueHour").ToString() == "0" ? "" : Eval("DueHour").ToString()%></td>
                           <td align="center" style="width:7%;"><%# Eval("DoneHour").ToString() == "0" ? "" : Eval("DoneHour").ToString()%></td>
                           <td align="left" style=" text-align:center;width:7%;"><%#Eval("DoneBy")%></td>
                           <td align="left" style="width:24%;"><%#Eval("JobDesc")%></td>
                           <td align="left" style="width:7%;"><%#Eval("Action")%></td>
                           <td align="left" style="width:20%;"><%#Eval("ConditionAfter")%></td>
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
      <asp:Panel ID="plShutdown" runat="server" Width="100%" Visible="false" >
      <div id="divSDRequest" runat="server" visible="false" style="height: 30px; padding :3px; text-align :left;vertical-align:top;" class="dottedscrollbox">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
            <ContentTemplate>
                <asp:Button ID="btnAddShutdownReq" Text="Add" CssClass="btn" style="width:80px; float:right; margin:1;" runat="server" CausesValidation="false" OnClick="btnAddShutdownReq_Click" />
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
                       <col style="width:8%;" />
                       <col style="width:40%;"/>
                       <col style="width:12%" />
                       <col style="width:15%" />
                       <col style="width:15%;" />
                       <col style="width:8%;" />
                       <col style="width:2%;" />
                   </colgroup>
                   <tr align="left" class="headerstylegrid">
                       <td style="width:8%;">Request Dt.</td>
                       <td style="width:40%;">Master/CE Name</td>
                       <td style="width:12%">Planned Shutdown (Total Hours)</td>
                       <td style="width:15%">Planned From Date/Time (Ship’s LT)</td>
                       <td style="width:15%;">Planned To Date/Time (Ship’s LT)</td>
                       <td style="width:8%;">Approved</td>                                 
                       <td style="width:2%;"></td>
                   </tr>
           </table>
           <div id="divShutdown"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 300px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                        <col style="width:8%;" />
                       <col style="width:40%;"/>
                       <col style="width:12%" />
                       <col style="width:15%" />
                       <col style="width:15%;" />
                       <col style="width:8%;" />
                       <col style="width:2%;" />
               </colgroup>
               <asp:Repeater ID="rptShutdown" runat="server">
                  <ItemTemplate>
                      <tr  onclick="openShutdownwindow('<%#Eval("CompCode")%>','<%#Eval("VesselCode")%>','<%#Eval("ShutdownId")%>');" style="cursor:pointer">
                           <td align="left" style="width:8%;"><%#Eval("RequestDate")%></td>
                           <td align="left" style="width:40%;"><%#Eval("MasterCEName")%></td>
                           <td align="right" style="width:12%"><%#Eval("Pl_ShutDownTotalHrs")%></td>
                           <td align="center" style="width:15%"><%# Convert.ToDateTime(Eval("Pl_FromDateTime").ToString().Split(' ').GetValue(0).ToString()).ToString("dd-MMM-yyyy") + "/ " + Eval("Pl_FromDateTime").ToString().Split(' ').GetValue(1).ToString()%></td>
                           <td align="center" style="width:15%"><%# Convert.ToDateTime(Eval("Pl_ToDateTime").ToString().Split(' ').GetValue(0).ToString()).ToString("dd-MMM-yyyy") + "/ " + Eval("Pl_ToDateTime").ToString().Split(' ').GetValue(1).ToString()%></td> 
                           <td align="center" style="width:8%;"><%#Eval("Approved")%></td>
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
    </asp:Content>

    


