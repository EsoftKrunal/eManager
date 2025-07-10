<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefectReport_Office.aspx.cs" Inherits="DefectReport_Office" MasterPageFile="~/MasterPage.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script src="JS/Calender.js" type="text/javascript"></script>
    <script src="js/jquery_v1.10.2.min.js" type="text/javascript"></script>
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
            //__doPostBack('btnSearchedCode', '');
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
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        //function addbreakdown(ctl)
        //{
        //    var CC = $("#txtBCompCode").val();
        //    var vsl = $('#ddlBVessel option:selected').attr('value');
        //    if (vsl==0 || CC=='')
        //    {
        //        alert('Please select vessel and enter component code to continue.');
        //    }
        //    else
        //        window.open('Popup_BreakDown1.aspx?mode=strict&VSL=' + vsl + '&CC=' + CC, '', '');            
        //}
        function reloadunits() {
            document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();
            //__doPostBack('btnRefresh', '');
        }
        function opendefectdetails(DN) {
            window.open('Office_Popup_BreakDown.aspx?DN=' + DN + '&FM=1', '', '');
        }
        function opendefectdetails_bd(DN) {
            window.open('Office_Popup_BreakDown1.aspx?DN=' + DN + '&FM=1', '', '');
        }



        function openAddUnPlanJob(VssCode, UPId) {
            //window.open('Reports/Office_BreakdownDefectReport.aspx?DN=' + DN, '', '');
            window.open('Popup_AddUnPlanJob.aspx?VSL=' + VssCode + '&UPId=' + UPId + '', '', '');
        }

        function OpenPrintWindow() {
            window.open('Reports/DefectReport_Office.aspx', '', '');
        }
        function OpenPrintWindow_bd() {
            window.open('Reports/BDReport_Office.aspx', '', '');
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
          Defect Reports
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
                            <div style="width:100%; height:452px; border:0px solid #000;  overflow:auto; overflow-y:hidden" >
             <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
                <ProgressTemplate>
                    <div style="position : absolute; top:399px; left:244px; width:100%; z-index:100;  text-align :center; color :Blue; ">
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
                            <td style="text-align :left; padding-left:1px;">
                             <asp:Panel ID="plSpecs" runat="server" Width="100%" ScrollBars="None" >
                             
                             <div style="height: 25px; text-align :left; padding-top :4px;" visible="false" >
                                 <asp:Button ID="btnDefect" runat="server" Text="Defect Report" CssClass="selbtn" onclick="btnDefect_onclick" style="height:22px; width:130px; vertical-align:middle;"/>
                                 <asp:Button ID="btnBreakDown" runat="server" Text="BreakDown Report" CssClass="btn1" onclick="btnBreakDown_onclick" style="height:22px;width:130px;vertical-align:middle;" Visible="false"/>
                                 <asp:Button ID="btnUnPlannedJobs" runat="server" Text="Random Jobs" CssClass="btn1" onclick="btnUnPlannedJobs_onclick"  Width="130px" style="height:22px;vertical-align:middle;"/>
                                
                             </div>
                           <table id="tblDefect" runat="server" cellpadding="2" cellspacing="0" rules="all" border="1" style="border-collapse:collapse;background-color:#f9f9f9; border: #4371a5 1px solid;" width="100%" >
                           <tr style=" background-color :#F2F2F2" >
                                <td style="text-align:center; font-weight:bold; padding-left:1px">Year &nbsp;</td>
                               <td style="text-align:center; font-weight:bold; padding-left:1px">Fleet &nbsp;</td>
                               <td style="text-align:center; font-weight:bold; padding-left:1px">Vessel &nbsp;</td>
                               <td style="text-align:center; font-weight:bold;">Component Code &nbsp;</td>
                               <td style="text-align:center; font-weight:bold; padding-left:1px" >Component Name &nbsp;</td>                               
                               <td style="text-align:center; font-weight:bold; padding-left:1px" > Defect Status  </td>
                               <td></td>
                           </tr>
                            <tr style=" background-color :#F2F2F2" >
                               <td style="text-align:center; padding-left:2px"><asp:DropDownList ID="ddlYear" runat="server" Width="70px"></asp:DropDownList></td>
                               <td style="text-align:center; padding-left:2px"><asp:DropDownList ID="ddlFleet" runat="server" Width="110px" AutoPostBack="true" onselectedindexchanged="ddlFleet_SelectedIndexChanged" ></asp:DropDownList></td>
                               <td style="text-align:center;">
                                   <asp:DropDownList ID="ddlVessels" runat="server" Width="200px" ></asp:DropDownList>
                               </td>
                               <td style="text-align:center"><asp:TextBox ID="txtCompCode" MaxLength="12" onkeypress="fncInputNumericValuesOnly(this)" runat="server" ></asp:TextBox></td>
                               <td style="text-align:center"><asp:TextBox ID="txtCompName" MaxLength="50" runat="server" ></asp:TextBox></td>
                               <td style="text-align:center">
                                <asp:DropDownList ID="ddlDefectStatus" runat="server" Width="120px" >
                                        <asp:ListItem Text="< All >" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text=" Open " Value="1" ></asp:ListItem>
                                        <asp:ListItem Text=" Closed " Value="2" ></asp:ListItem>
                                    </asp:DropDownList>
                               </td>
                                       
                               <td><asp:Button ID="btnSearch" Text="Search" CssClass="btn" runat="server" 
                                       onclick="btnSearch_Click" /><asp:Button ID="btnClear" Text="Clear" 
                                       CssClass="btn" runat="server" onclick="btnClear_Click" />
                                       <asp:Button ID="btnPrint" Text="Print" CssClass="btn" runat="server" OnClick="btnPrint_Click" />
                                       </td>
                                       
                            </tr>
                    
            <tr>
                <td colspan="7" align="left">
                  
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                   <col style="width:60px;" />
                   <col style="width:120px;" />
                   <col />
                   <col style="width:130px;" />
                   <col style="width:90px;" />
                   <col style="width:130px;" />
                   <col style="width:130px;" />
                   <col style="width:85px;" />
                   <col style="width:60px;" />
                  <%-- <col style="width:90px;" />--%>
                   <col style="width:17px;" />
                   <tr align="left" class= "headerstylegrid">
                   <td>Vessel</td>
                   <td>Component Code</td>
                   <td>Component Name</td>
                   <td>Defect Number</td>
                   <td>Report Dt.</td>
                   <td>Target Closure Dt.</td>
                   <td>Component Status</td>
                   <td>Comp. Dt</td>
                   <td>Status</td>
                   <%--<td>RQN Dt.</td>--%>
                   <td></td>    
                   </tr>
             </colgroup>
           </table>           
           <div id="dvDefects"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 332px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                    <col style="width:60px;" />
                    <col style="width:120px;" />
                    <col />
                    <col style="width:130px;" />
                    <col style="width:90px;" />
                    <col style="width:130px;" />
                    <col style="width:130px;" />
                    <col style="width:85px;" />
                    <col style="width:60px;" />
                    <%--<col style="width:90px;" />--%>
                    <col style="width:17px;" />
                    </colgroup>
               <asp:Repeater ID="rptDefects" runat="server">
                  <ItemTemplate>
                      <tr onclick="opendefectdetails('<%#Eval("DefectNo")%>')" title="Click to view details." class='<%# (Eval("Status").ToString()=="OD")?"highlightrow":"" %>' >
                           <td align="left"><%#Eval("VesselCode")%></td>
                           <td align="left"><%#Eval("ComponentCode")%></td>
                           <td align="left"><%#Eval("ComponentName")%>
                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                           </td>
                           <td align="left"><%#Eval("DefectNo")%></td>
                           <td align="left"><%#Eval("ReportDt")%></td>
                           <td align="center"><%#Eval("TargetDt")%></td>
                           <td align="left"><%#Eval("CompStatus")%></td>
                           
                           <td align="left"><%#Eval("CompletionDt")%></td>
                           
                           <td align="center"><%#Eval("DefectStatus")%></td>
                          <%-- <td align="center"><%#Eval("RqnDate")%></td>--%>
                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                       </tr>
                   </ItemTemplate>
                  </asp:Repeater>
              </table>
           </div>
           <div style="float:right; padding-right:5px;">
                 <asp:Label ID="lblRecordCount" style=" font-weight:bold" runat="server"></asp:Label>
            </div>
                </td>
            </tr>
            
        </table>                   
        
<%--------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
                            <table id="tblUnPlannedJobs" runat="server" cellpadding="2" cellspacing="0" rules="all" border="1" style="border-collapse:collapse;background-color:#f9f9f9; border: #4371a5 1px solid;" width="100%" >
                           <tr style=" background-color :#F2F2F2" >
                                <td style="text-align:center; font-weight:bold; padding-left:1px">Fleet &nbsp;</td>
                                <td style="text-align:center; font-weight:bold; padding-left:1px">Vessel &nbsp;</td>
                               <td style="text-align:center; font-weight:bold;">Component Code &nbsp;</td>
                               <td style="text-align:center; font-weight:bold; padding-left:1px" >Component Name &nbsp;</td>                               
                               <td style="text-align:center; font-weight:bold; padding-left:1px" > Status  </td>
                               <td></td>
                           </tr>
                            <tr style=" background-color :#F2F2F2" >
                               <td style="text-align:center; padding-left:2px"><asp:DropDownList ID="ddlFleetUPJ" runat="server" Width="110px" AutoPostBack="true" onselectedindexchanged="ddlFleetUPJ_SelectedIndexChanged" ></asp:DropDownList></td>
                               <td style="text-align:center;"><asp:DropDownList ID="ddlVesselUPJ" runat="server" Width="200px" ></asp:DropDownList></td>
                               <td style="text-align:center"><asp:TextBox ID="txtCompCodeUPJ" MaxLength="12" onkeypress="fncInputNumericValuesOnly(this)" runat="server" ></asp:TextBox></td>
                               <td style="text-align:center"><asp:TextBox ID="txtCompNameUPJ" MaxLength="50" runat="server" ></asp:TextBox></td>
                               <td style="text-align:center">
                                <asp:DropDownList ID="ddlJobStatusUPJ" runat="server" Width="120px" >
                                        <asp:ListItem Text="< All >" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text=" Open " Value="1" ></asp:ListItem>
                                        <asp:ListItem Text=" Closed " Value="2" ></asp:ListItem>
                                    </asp:DropDownList>
                               </td>
                                       
                               <td><asp:Button ID="btnSearchUPJ" Text="Search" CssClass="btn" runat="server" 
                                       onclick="btnSearchUPJ_Click" /><asp:Button ID="btnUnPlannedCancel" Text="Clear" 
                                       CssClass="btn" runat="server" onclick="btnUnPlannedCancel_Click" /></td>
                                       
                            </tr>
                    
            <tr>
                <td colspan="6" align="left">
                  
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                   <col style="width:60px;" />
                   <col style="width:120px;" />
                   <col />
                   <col style="width:200px;" />
                    <col style="width:85px;" />
                   <col style="width:85px;" />
                   <col style="width:60px;" />
                   <col style="width:17px;" />
                   <tr align="left" class= "headerstylegrid">
                   <td>Vessel</td>
                   <td>Component Code</td>
                   <td>Component Name</td>
                   <td>Description</td>
                   <td>Due Date</td>
                   <td>Done Dt.</td>
                   <td>Status</td>
                   <td></td>    
                   </tr>
             </colgroup>
           </table>           
           <div id="Div1"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 330px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                    <col style="width:60px;" />
                    <col style="width:120px;" />
                    <col />
                    <col style="width:200px;" />
                    <col style="width:85px;" />
                    <col style="width:85px;" />
                    <col style="width:60px;" />
                    <col style="width:17px;" />
                    </colgroup>
               <asp:Repeater ID="rptUnPlannedJobs" runat="server">
                  <ItemTemplate>
                      <tr  onclick="openAddUnPlanJob('<%#Eval("VesselCode")%>','<%#Eval("UPID")%>')" title="Click to view details."  class="row">  
                           <td align="left"><%#Eval("VesselCode")%></td>
                           <td align="left"><%#Eval("ComponentCode")%></td>
                           <td align="left"><%#Eval("ComponentName")%>
                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                           </td>
                           <td align="left"><%#Eval("ShortDescr")%></td>
                           <td align="center"><%#Eval("DueDate")%></td>
                           <td align="center"><%#Eval("CompletionDt")%></td>
                           <td align="center"><%#Eval("CompletionStatus")%></td>
                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                       </tr>
                   </ItemTemplate>
                  </asp:Repeater>
              </table>
           </div>
           <div style="float:right; padding-right:5px;">
                 <asp:Label ID="lblCountUPJ" style=" font-weight:bold" runat="server"></asp:Label>
            </div>
                </td>
            </tr>
            
        </table>                   
        
        <%--------------------------------------------------------------------------------------------------------------------------------------------------------------------%>

                                 <table id="tblBreakDown" runat="server" cellpadding="2" cellspacing="0" rules="all" border="1" style="border-collapse:collapse;background-color:#f9f9f9; border: #4371a5 1px solid;" width="100%" >
                           <tr style=" background-color :#F2F2F2" >
                                <td style="text-align:center; font-weight:bold; padding-left:1px">Year &nbsp;</td>
                               <td style="text-align:center; font-weight:bold; padding-left:1px">Fleet &nbsp;</td>
                               <td style="text-align:center; font-weight:bold; padding-left:1px">Vessel &nbsp;</td>
                               <td style="text-align:center; font-weight:bold;">Component Code &nbsp;</td>
                               <td style="text-align:center; font-weight:bold; padding-left:1px" >Component Name &nbsp;</td>                               
                               <td style="text-align:center; font-weight:bold; padding-left:1px" > Defect Status  </td>
                               <td></td>
                           </tr>
                            <tr style=" background-color :#F2F2F2" >
                               <td style="text-align:center; padding-left:2px"><asp:DropDownList ID="ddlBYear" runat="server" Width="70px"></asp:DropDownList></td>
                               <td style="text-align:center; padding-left:2px"><asp:DropDownList ID="ddlBFleet" runat="server" Width="110px" AutoPostBack="true" onselectedindexchanged="ddlBFleet_SelectedIndexChanged" ></asp:DropDownList></td>
                               <td style="text-align:center;"><asp:DropDownList ID="ddlBVessel" runat="server" Width="200px" ></asp:DropDownList>
                               </td>
                               <td style="text-align:center"><asp:TextBox ID="txtBCompCode" MaxLength="12" onkeypress="fncInputNumericValuesOnly(this)" runat="server" ></asp:TextBox>
                                  <%-- <img src="Images/add1.png" style="height:15px;" onclick="addbreakdown(this);" />--%>
                               </td>
                               <td style="text-align:center"><asp:TextBox ID="txtBCompName" MaxLength="50" runat="server" ></asp:TextBox></td>
                               <td style="text-align:center">
                                <asp:DropDownList ID="ddlBStatus" runat="server" Width="120px" >
                                        <asp:ListItem Text="< All >" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text=" Open " Value="1" ></asp:ListItem>
                                        <asp:ListItem Text=" Closed " Value="2" ></asp:ListItem>
                                    </asp:DropDownList>
                               </td>                                       
                               <td>
                                   <asp:Button ID="btbBSearch" Text="Search" CssClass="btn" runat="server" onclick="btnBSearch_Click" />
                                   <asp:Button ID="btnBClear" Text="Clear" CssClass="btn" runat="server" onclick="btnBClear_Click" />
                                   <asp:Button ID="btnBPrint" Text="Print" CssClass="btn" runat="server" OnClick="btnBPrint_Click" />
                               </td>
                                       
                            </tr>
                    
            <tr>
                <td colspan="7" align="left">
                  
           <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
             <colgroup>
                   <%--<col style="width:30px;" />--%>
                   <col style="width:60px;" />
                   <col style="width:120px;" />
                   <col />
                   <col style="width:130px;" />
                   <col style="width:90px;" />
                   <col style="width:130px;" />
                   <col style="width:130px;" />
                   <col style="width:85px;" />
                   <col style="width:60px;" />
                  <%-- <col style="width:90px;" />--%>
                   <col style="width:17px;" />
                   <tr align="left" class= "headerstylegrid">
                   <%--<td>Edit</td>--%>
                   <td>Vessel</td>
                   <td>Component Code</td>
                   <td>Component Name</td>
                   <td>Defect Number</td>
                   <td>Report Dt.</td>
                   <td>Target Closure Dt.</td>
                   <td>Component Status</td>
                   <td>Comp. Dt</td>
                   <td>Status</td>
                   <%--<td>RQN Dt.</td>--%>
                   <td></td>    
                   </tr>
             </colgroup>
           </table>           
           <div id="dbBreakDowns"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 332px ; text-align:center;">
            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
               <colgroup>
                    <%--<col style="width:30px;" />--%>
                    <col style="width:60px;" />
                    <col style="width:120px;" />
                    <col />
                    <col style="width:130px;" />
                    <col style="width:90px;" />
                    <col style="width:130px;" />
                    <col style="width:130px;" />
                    <col style="width:85px;" />
                    <col style="width:60px;" />
                    <%--<col style="width:90px;" />--%>
                    <col style="width:17px;" />
                    </colgroup>
               <asp:Repeater ID="rptBreakDowns" runat="server">
                  <ItemTemplate>
                      <tr onclick="opendefectdetails_bd('<%#Eval("BreakDownNo")%>')" title="Click to view details." class='<%# (Eval("Status").ToString()=="OD")?"highlightrow":"row" %>' >
                           <%--<td><a href='Popup_BreakDown1.aspx?mode=strict&VSL=<%#Eval("VesselCode")%>&DN=<%#Eval("BreakDownNo")%>&FM=1' target="_blank"><img src="Images/addpencil.gif" /></a></td>--%>
                          
                           <td align="left"><%#Eval("VesselCode")%></td>
                           <td align="left"><%#Eval("ComponentCode")%></td>
                           <td align="left"><%#Eval("ComponentName")%>
                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                           </td>
                           <td align="left"><%#Eval("BreakDownNo")%></td>
                           <td align="left"><%#Eval("ReportDt")%></td>
                           <td align="center"><%#Eval("TargetDt")%></td>
                           <td align="left"><%#Eval("CompStatus")%></td>
                           
                           <td align="left"><%#Eval("CompletionDt")%></td>
                           
                           <td align="center"><%#Eval("DefectStatus")%></td>
                          <%-- <td align="center"><%#Eval("RqnDate")%></td>--%>
                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                       </tr>
                   </ItemTemplate>
                  </asp:Repeater>
              </table>
           </div>
           <div style="float:right; padding-right:5px;">
                 <asp:Label ID="Label1" style=" font-weight:bold" runat="server"></asp:Label>
            </div>
                </td>
            </tr>
            
        </table>
          </asp:Panel>
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
