<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefectReport.aspx.cs" Inherits="DefectReport" %>
<%@ Register Src="UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%@ Register Src="UserControls/HeaderMenu.ascx" TagName="HMenu" TagPrefix="hm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<%@ Register Src="~/Modules/PMS/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
         
        function reloadunits() {
            
            __doPostBack('btnRefresh', '');
        }
        function openadddefects(CC)
        {
           window.open('Popup_BreakDown.aspx?CC='+ CC, '', '');
        }

        function openprint(ctl)
        {
           window.open('Reports/Office_BreakdownDefectReport.aspx?CC='+ CC, '', '');
        }
        function opendefectprint(DN)
        {
            window.open('Popup_BreakDown.aspx?DN='+ DN, '', '');
        }
        function openbreakdownprint(DN) {
            window.open('Popup_BreakDown1.aspx?DN=' + DN, '', '');
        }        
        function openupjprint(VSL,UPId) 
        {
            window.open('Popup_AddUnPlanJob.aspx?VSL=' + VSL + '&UPId=' + UPId, '', '');
        }
        function openaddupj(VSL,CC) {
            window.open('Popup_AddUnPlanJob.aspx?VSL=' + VSL + '&CC=' + CC, '', '');
        }
        function OpenPrintWindow() {
            window.open('Reports/DefectReport_Office.aspx', '', '');
        }
        function openaddbd(CC) {
            window.open('Popup_BreakDown1.aspx?CC=' + CC, '', '');
        }
        function OpenPrintWindowBD() {
            window.open('Reports/BDReport_Office.aspx', '', '');
        }
        
    </script>
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
                              <span style="float :right">
                              <asp:Button ID="btnSearchComponents" ToolTip="Print Jobs" runat="server" 
                                        Text="&nbsp;&nbsp;&nbsp;&nbsp;Search" CssClass="btnorange" Width="70px" 
                                        OnClick="btnSearchComponents_Click" 
                                        style="background-image:url(Images/find.jpg); background-repeat: no-repeat; border :solid 1px gray; background-position-x:3px;background-position-y:3px" 
                                        CausesValidation="False"/>       
                                     </span>
                                     
                              <asp:DropDownList ID="ddlVessels" runat="server" AutoPostBack="true" Width="272px" onselectedindexchanged="ddlVessels_SelectedIndexChanged" ></asp:DropDownList> 
                              
                            </div>                                                            
                                <div ID="dvscroll_Componenttree" class="scrollbox" onscroll="SetScrollPos(this)" style="width :350px; overflow-y:scroll; overflow-x: hidden;  height :417px;">
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
                            </td>
                            <td style="text-align :left; padding-left:5px;">
                             <div>
                            <asp:Label ID="lblMessage" Text="" CssClass="error_msg"  runat="server"></asp:Label>
                            <asp:Button ID="btnRefresh" style="display:none;" runat="server" onclick="btnRefresh_Click" />
                            </div>
                            <div style="height:5px;"></div>
                            <div style="height: 31px; padding-top:1px; text-align :left; " class="dottedscrollbox" >
                                <asp:Button ID="btnDefects" runat="server" CssClass="btnorange" onclick="btnDefects_Click" style="float :left" Text="Defect Report" Width="100px" />
                                <asp:Button ID="btnBreakDowns" runat="server" CssClass="btnorange" onclick="btnBreakDowns_Click" style="float :left" Text="BreakDown Report" Width="150px" />
                                <asp:Button ID="btnUnplanned" runat="server" CssClass="btnorange" onclick="btnUnplanned_Click" style="float :left" Text="Unplanned Jobs" Width="150px" />
                            </div>
                           <div style=" padding-top:1px; text-align :left; " class="dottedscrollbox" >
                             <b>Status :</b>&nbsp;<asp:DropDownList ID="ddlDefectStatus" AutoPostBack="true" runat="server" Width="70px" onselectedindexchanged="ddlDefectStatus_SelectedIndexChanged" >
                             <asp:ListItem Text="< All >" Value="0" ></asp:ListItem>
                             <asp:ListItem Text=" Open " Value="1" Selected="True"></asp:ListItem>
                             <asp:ListItem Text=" Closed " Value="2" ></asp:ListItem>
                           </asp:DropDownList>
                           </div>
                             <asp:Panel ID="plSpecs" runat="server" Width="100%" ScrollBars="None" Visible="false">
                             <table cellpadding="4" cellspacing="2" rules="all" border="1" style="border-collapse:collapse;background-color:#f9f9f9; border: #4371a5 1px solid;" width="99%" >
                                <tr>
                                   <td align="left">
                                   <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                     <colgroup>
                                           <col style="width:90px;" />
                                           <col />
                                           <col style="width:100px;" />
                                           <col style="width:120px;" />
                                           <col style="width:120px;" />
                                           <col style="width:17px;" />
                                           <tr align="left" class= "headerstylegrid">
                                           <td>Comp Code</td>
                                           <td>Component Name</td>
                                           <td align="left">Defect Number</td>
                                           <td>Target Closure Dt.</td>
                                           <td>Close Date</td>
                                           <td></td>    
                                           </tr>
                                     </colgroup>
                                   </table>           
                                   <div id="dvDefects"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 320px ; text-align:center;">
                                    <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                       <colgroup>
                                           <col style="width:90px;" />
                                           <col />
                                           <col style="width:100px;" />
                                           <col style="width:120px;" />
                                           <col style="width:120px;" />
                                           <col style="width:17px;" />
                                            </colgroup>
                                       <asp:Repeater ID="rptDefects" runat="server">
                                          <ItemTemplate>
                                              <tr onclick="opendefectprint('<%#Eval("DefectNo")%>')" class='<%# (Eval("Status").ToString()=="OD")?"highlightrow":"row" %>' >
                                                   <td align="left"><%#Eval("ComponentCode")%></td>
                                                   <td align="left"><%#Eval("ComponentName")%>
                                                   <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                                                   </td>
                                                   <td align="left"><%#Eval("DefectNo")%></td>
                                                   <td align="center"><%#Eval("TargetDt")%></td>
                                                   <td align="center"><%#Eval("CompletionDt")%></td>
                                                   <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                               </tr>
                                           </ItemTemplate>
                                          </asp:Repeater>
                                      </table>
                                   </div>
                                   
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                       <td style="text-align:right; padding-top:3px; padding-right:5px;">
                                           <asp:Button ID="btnAdd" Text="Add" CssClass="btnorange" runat="server" onclick="btnAdd_Click" />
                                           <asp:Button ID="btnPrintDefectReport" Text="Print" CssClass="btnorange" runat="server" Visible="false" onclick="btnPrintDefectReport_Click" />
                                           </td>
                                    </tr>
                                </table>
                             </asp:Panel>
                             <asp:Panel ID="pnUPJ" runat="server" Width="99%" ScrollBars="None" Visible="false">
                             <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                             <colgroup>
                                   <col style="width:90px;" />
                                   <col style="width:300px;" />
                                           <col />
                                   <col style="width:120px;" />
                                   <col style="width:120px;" />
                                   <col style="width:17px;" />
                                   <tr align="left" class= "headerstylegrid">
                                   <td>Comp Code</td>
                                   <td>Component Name</td>
                                   <td align="left">Job Desc</td>
                                   <td>Due Dt.</td>
                                   <td>Done Date</td>
                                   <td></td>    
                                   </tr>
                             </colgroup>
                           </table>
                             <div id="dvUpJobs"  onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 320px ; text-align:center;">
                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                               <colgroup>
                                   <col style="width:90px;" />
                                   <col style="width:300px;" />
                                           <col />
                                   <col style="width:120px;" />
                                   <col style="width:120px;" />
                                   <col style="width:17px;" />
                                    </colgroup>
                               <asp:Repeater ID="rptUpJobs" runat="server">
                                  <ItemTemplate>
                                      <tr onclick="openupjprint('<%#Eval("VESSELCODE")%>','<%#Eval("UPId")%>')"  class='<%# (Eval("ODStatus").ToString()=="OD")?"highlightrow":"row" %>'>
                                           <td align="left"><%#Eval("ComponentCode")%></td>
                                           <td align="left"><%#Eval("ComponentName")%>
                                           <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                                           </td>
                                           <td align="left"><%#Eval("ShortDescr")%></td>
                                           <td align="center"><%#Common.ToDateString(Eval("DueDate"))%></td>
                                           <td align="center"><%#Common.ToDateString(Eval("DoneDate"))%></td>
                                           <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                       </tr>
                                   </ItemTemplate>
                                  </asp:Repeater>
                              </table>
                           </div>
                         <asp:Button ID="btnAddUps" Text="Add" CssClass="btnorange" runat="server" onclick="btnUPJ_Click"  style="float:right" />
                         <asp:Button ID="btnPrintUps" Text="Print" CssClass="btnorange" runat="server" Visible="false" onclick="btnPrintUPReport_Click" style="float:right" />
                         </asp:Panel>
                             <asp:Panel ID="btnBDJ" runat="server" Width="99%" ScrollBars="None" Visible="false">
                                 <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;  HEIGHT: 20px ; text-align:center;">
                             <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                             <colgroup>
                                    <tr align="left" class= "headerstylegrid">
                                        <td style="width:100px;">Comp Code</td>
                                        <td>Component Name</td>
                                        <td  style="width:150px;">Breakdown Number</td>
                                       <%-- <td style="width:100px;">Created By</td>--%>
                                        <td style="width:100px;">Target Closure Dt.</td>
                                        <td style="width:100px;">Close Date</td>
                                    </tr>
                             </colgroup>
                           </table>
                            </div>
                            <div id="dvBDJobs" onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 320px ; text-align:center;">
                            <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                   <asp:Repeater ID="rptBreakDownJobs" runat="server">
                                   <ItemTemplate>
                                        <tr onclick="openbreakdownprint('<%#Eval("BreakDownNo")%>')" class='<%# (Eval("Status").ToString()=="OD")?"highlightrow":"row" %>' >
                                            <td align="left"  style="width:100px;"><%#Eval("ComponentCode")%></td>
                                            <td align="left"><%#Eval("ComponentName")%>
                                            <%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%>
                                            </td>
                                            <td align="left"  style="width:150px;"><%#Eval("BreakDownNo")%></td>
                                           <%-- <td align="left" style="width:100px;"><%#((Eval("OfficeShip").ToString()=="S")?"Ship":"Office")%></td>--%>
                                            <td align="center" style="width:100px;"><%#Eval("TargetDt")%></td>
                                            <td align="center" style="width:100px;"><%#Eval("CompletionDt")%></td>
                                        </tr>
                                    </ItemTemplate>
                                  </asp:Repeater>
                              </table>
                           </div>
                         <asp:Button ID="btnAddBD" Text="Add" CssClass="btnorange" runat="server" onclick="btnAddBD_Click" Visible="false" style="float:right"/>
                         <asp:Button ID="btnPrintBDReport" Text="Print" CssClass="btnorange" runat="server" Visible="false" onclick="btnPrintBDReport_Click"  style="float:right" />
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
     <mtm:footer ID="footer1" runat ="server" />
    </form>
</body>
</html>
