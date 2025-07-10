<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Office_Admin_JobCorrection.aspx.cs" Inherits="Office_Admin_JobCorrection" Trace="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="JS/JQuery.js"></script>
    <script src="JS/Common.js" type="text/javascript"></script>
	<script src="JS/KPiScript.js" type="text/javascript"></script>
    <style type="text/css">
    td
    { 
        vertical-align:middle; 
    }
        .selRow {
            background-color:#f9ac4c;
        }
    </style>
        <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <table width="100%">
    <tr>
        <td style="width:500px;vertical-align:top;">
                <table width="100%">
                        <tr>
                            <td>
                           
        <div style="padding:1px;">
            <asp:DropDownList ID="ddlVessels" runat="server" AutoPostBack="True" onSelectedIndexChanged="BindCorrections"></asp:DropDownList>
        </div>
    </td>
        <td>
        <div style="padding:1px;">
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" onSelectedIndexChanged="BindCorrections">
                    <asp:ListItem Text="< All Status >" value=""></asp:ListItem>
                    <asp:ListItem Text="Open" value="O" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Processed" value="P"></asp:ListItem>
                    <asp:ListItem Text="Closed" value="C"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </td>
    </tr>
                </table>
        <div style="overflow-y:scroll;height:330px;" id="dv8966444" onscroll="SetScrollPos(this)" class="ScrollAutoReset" >
                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="width:50px;" />
                            <col style="width:50px;" />
                            <col style="width:60px;" />
                            <col />
                            <col style="width:90px;" />
                            <col style="width:80px;" />
                        </colgroup>
                        <tr align="left" class= "headerstylegrid">
                            <td>Select</td>
                            <td>Vessel</td>
                            <td>HistoryId</td>
                            <td>Requested By</td>
                            <td>Requested On</td>
                            <td>Status</td>
                        </tr>
                </table> 
                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="width:50px;" />
                            <col style="width:50px;" />
                            <col style="width:60px;" />
                            <col/>
                            <col style="width:90px;" />
                            <col style="width:80px;" />
                        </colgroup>
                        <asp:Repeater ID="rptCorrections" runat="server">
                        <ItemTemplate>
                            <tr align="left" class='<%# ((CorrectionId== Common.CastAsInt32(Eval("TableId")))?"selRow":"row") %>' >
                                <td align="center">                                    
                                    <asp:LinkButton id="btnSelect" runat="server" onClick="btnSelect_Click" CommandArgument='<%#Eval("TableId")%>' text='Select'></asp:LinkButton></td>
                                <td align="center"><%#Eval("VesselCode")%></td>                            
                                <td align="center"><%#Eval("HistoryId")%></td>
                                <td align="left"><%#Eval("CorrectionBy")%></td>
                                <td align="center"><%#Common.ToDateString(Eval("CorrectioOn"))%></td>
                                <td align="center"><%#Eval("Status")%></td>
                            </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                </table> 
        </div>

        </td>
        <td>
        <!-- <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="up1" ID="UpdateProgress1">
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
         <ContentTemplate> -->
   <div>
       <div style="padding-top:2px">
            
   <table style="background-color: #f9f9f9" border="0" cellpadding="3" cellspacing="0" width="100%">
            <tr>
                <td style="text-align:right" >
                    <asp:Label runat="server" ID="lblheading" style="float:left;font-weight:bold" Font-Size="Larger"></asp:Label>
                    <asp:Button runat="server" CssClass="btnorange"  ID="btnCorreection" Text="Reject" onclick="btnCorrection_Click" Width="150px" Visible="false" />                      
                    <asp:Button runat="server" CssClass="btnorange"  ID="btnCancelCorrection" Text="Cancel" onclick="btnCancelCorrection_Click" Width="150px" Visible="false" />                      
                    <asp:Button ID="btnExportToShip" CssClass="btnorange"  runat="server" Text="Export to Ship" OnClick="btnExportToShip_OnClick" Visible="false" Width="150px" />
                </td>
            </tr>
           
     </table>
 <table style="background-color: #f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                 <asp:Panel ID="plOfficeComponent" runat="server" Visible="true">                                
                            <div class="box1">
                                <asp:Label ID="lblComponentListCounter" runat="server" style="float:right;margin-right:15px;"></asp:Label>
                                <b style="color:red">Following Job History will be deleted after rejection.</b>
                            </div>
                            <table cellpadding="0" cellspacing="0" style="background-color:#f9f9f9;" width="99%">
                             <tr>
                              <td>
                                  <div class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT:25px ; text-align:center;">
                               <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                       <colgroup>
                                           <col style="width:80px;" />
                                           <col style="width:80px;" />
                                           <col style="width:80px;" />
                                           <col style="width:200px;" />
                                           <col />
                                           <col style="width:90px;" />
                                           <col style="width:90px;" />
                                           <col style="width:80px;" />
                                           <col style="width:80px;" />
                                           <%--<col style="width:70px;" />--%>
                                       </colgroup>
                                       <tr align="left" class= "headerstylegrid">
                                           <td>Vessel</td>
                                           <td>HistoryId</td>
                                           <td>Action</td>
                                           <td>Comp. Name</td>
                                           <td>Job Name</td>
                                           <td>Due Date</td>
                                           <td>Done Date</td>
                                           <td>Due Hr.</td>
                                           <td>Done Hr.</td>
                                           <%--<td>Done By</td>--%>
                                       </tr>
                               </table>
                                    </div>
                               <div id="divHistory" onscroll="SetScrollPos(this)" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 200px ; text-align:center;">
                                <table border="1" cellpadding="4" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                   <colgroup>
                                           <col style="width:80px;" />
                                           <col style="width:80px;" />
                                           <col style="width:80px;" />
                                           <col style="width:200px;" />
                                           <col />
                                           <col style="width:90px;" />
                                           <col style="width:90px;" />
                                           <col style="width:80px;" />
                                           <col style="width:80px;" />
                                           <%--<col style="width:70px;" />--%>
                                       </colgroup>
                                   <asp:Repeater ID="rptJobHistory" runat="server">
                                      <ItemTemplate>
                                          <tr onclick="openhistory(this);" vsl='<%#Eval("VesselCode")%>' historyid='<%#Eval("HistoryId")%>' rtype='<%#Eval("rtype")%>' >
                                              <td align="center"><%#Eval("VesselCode")%></td>
                                              <td align="center"><%#Eval("HistoryId")%></td>
                                              <td align="left"><%#((Eval("rtype").ToString()=="R")?"Report":"Postpone")%></td>
                                              <td align="left">
                                                  <b><%#Eval("_ComponentCode")%></b>
                                                  <br />
                                                  <%#Eval("ComponentName")%>
                                              </td>
                                              <td align="left">
                                                 <b><%#Eval("JobCode")%></b>
                                                  <br />
                                                  <%#Eval("JobDesc")%></td>
                                               <td align="left">
                                                   <%#Eval("DueDate")%>
                                                    <asp:HiddenField ID="hfHid" Value='<%#Eval("PK")%>' runat="server" />
                                                    <asp:HiddenField ID="hfVC" Value='<%#Eval("VesselCode")%>' runat="server" />
                                               </td>
                                               <td align="left"><%#Eval("ACTIONDATE")%></td>
                                               <td align="center"><%# Eval("DueHour").ToString() == "0" ? "" : Eval("DueHour").ToString()%></td>
                                               <td align="center"><%# Eval("DoneHour").ToString() == "0" ? "" : Eval("DoneHour").ToString()%></td>
                                              <%-- <td align="left" style=" text-align:center"><%#Eval("DoneBy")%></td>                           --%>
                                          </tr>
                                       </ItemTemplate>
                                      </asp:Repeater>
                                  </table>
                               </div>

                                  
                                <table cellpadding="5" cellspacing="0" width="100%" border="1" rules="all">
                                          <col width="170px" />
                                          <col width="250px" />
                                          <col width="170px" />
                                          <col />
                                          <tr>
                                              <td style="text-align:left;"> <b> Job Interval</b></td>
                                              <td>
                                                  <asp:Label ID="lblJobInterval" runat="server"></asp:Label>
                                              </td>                                              
                                              <td></td>
                                              <td></td>
                                          </tr>
                                          <tr>
                                              <td style="text-align:left;"><b>Last Done Date / Hour</b></td>
                                              <td>
                                                  <asp:Label ID="lblLastDoneDate" runat="server"></asp:Label>/<asp:Label ID="lblLastDoneHour" runat="server"></asp:Label>
                                              </td>
                                              <td style="text-align:left;"><b>Next Due Date / Hour</b></td>
                                              <td>
                                                  <asp:Label ID="lblNextDueDate" runat="server"></asp:Label>/ <asp:Label ID="lblNextDueHour" runat="server"></asp:Label>
                                              </td>
                                          </tr>
                                          <tr>
                                              <td colspan="4" style="text-align:left;">
                                                  <b>Remarks</b> <br />
                                                  <asp:Label ID="lblRemarks" runat="server"></asp:Label>
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
       <div style="position:absolute;top:0px;left:0px; height :510px; width:100%;z-index:100;" runat="server" id="divCorrection" visible="false" >
            <center>
            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:435px; height:235px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:50px; opacity:1;filter:alpha(opacity=100)">
                <!-- <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate> -->
                    <div style="font-size: 15px;padding: 10px;font-weight: bold;">Job Rejection</div>
                    <div style="font-weight:bold;color:red;">
                        <asp:Literal ID="litrecordCount" runat="server"></asp:Literal> Records will be deleted from job history.
                    </div>
                    <table cellpadding="4" cellspacing="3" style="margin-top:10px;" width="100%">
                            <tr>
                                <td style="text-align:left;">
                                    <b>Remarks :</b><br />
                                    <!-- <asp:TextBox ID="txtRemarks" runat="server" Width="99%" TextMode="MultiLine" Height="70px"></asp:TextBox>
                                     -->
                                     <asp:Label ID="lblRRemarks" runat="server" ></asp:Label>
                                </td>
                            </tr>
                                                    
                    </table>
                    
                    <table cellpadding="4" cellspacing="3" width="100%">
                        <tr>
                            <td colspan="2">
                                &nbsp;<asp:Label ID="lblMsgCorrectionRemarks" runat="server" style="color:red;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="btnSave_Correction" runat="server" Text="Save" OnClick="btnSave_Correction_OnClick" CssClass="btn" />
                                <asp:Button ID="btnClose_CorrectionPopup" runat="server" Text="Close" OnClick="btnClose_CorrectionPopup_OnClick" CssClass="btn" />                                                                
                            </td>
                        </tr>
                    </table>
                <!-- </ContentTemplate>                
                </asp:UpdatePanel> -->
            </div> 
            </center>
         </div>
             <div style="padding-top:2px;">
                 <uc1:MessageBox ID="MessageBox1" runat="server" />
             </div>
     </div>
     <!-- </ContentTemplate>
        <Triggers>
        </Triggers>
        </asp:UpdatePanel>  -->
        <script type="text/javascript">
            function openhistory(ctl)
            {
                var vsl = $(ctl).attr('vsl');
                var historyid = $(ctl).attr('historyid');
                var rtype = $(ctl).attr('rtype');
                window.open('./JobCard_Office.aspx?VC=' + vsl + '&&HID=' + historyid + '&&RP=' + rtype, '');
            }
        </script>
        </td>
    </tr>   
    </table>        
    </form>
</body>
</html>
