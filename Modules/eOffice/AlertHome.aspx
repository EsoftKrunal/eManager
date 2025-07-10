<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertHome.aspx.cs" Inherits="emtm_Emtm_AlertHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    
    .hdng
    {
     text-align:left; padding:5px; font-size:14px; background-color:#85E0FF; color:GREY; border-bottom:solid 1px #eeeeee;
    }
     </style>
</head>
<body style="font-family:Calibri; font-size:12px; margin:0px;">
    <form id="form1" runat="server">
   <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>        
        <td style=" text-align :left; vertical-align : top;" > 
    <center>
     <asp:UpdatePanel runat="server" ID="up1">
     <ContentTemplate>
     <table width="100%" align="center" cellspacing="0">
     <tr>
        <td style="width:50%; vertical-align:top;">
          <div style="border:solid 1px #c2c2c2; border-bottom:none; background-color : #777777; color:white; font-size :14px; font-weight:bold; padding:10px; box-sizing:border-box;">My Alerts</div>
          <div class="scrollbox" style="WIDTH: 100%; text-align:center;">
            <asp:Repeater ID="rptAppName_MA" runat="server" >
            <ItemTemplate>
               <div class="hdng"><%#Eval("ApplicationName")%></div>
               <table width="100%" cellpadding="6" border="1" rules="rows" style="border-collapse:collapse">
                <asp:Repeater ID="rptAlerts_MA" DataSource='<%#LoadMyAlerts(Common.CastAsInt32(Eval("ApplicationId")))%>' runat="server" >
                <ItemTemplate>
                    <tr>
                        <td style="width:5px">&nbsp;</td>
                        <td style="width:20px" ><%#Eval("SrNo")%>.</td>
                        <td style=" text-align:left;"><%#Eval("AlertTypeName")%>&nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkMyAlerts" runat="server" CommandArgument='<%#Eval("AlertTypeId")%>' onclick="lnkMyAlerts_Click" Text='<%#GetMyAlertCount(Common.CastAsInt32(Eval("AlertTypeId")))%>'></asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
                </asp:Repeater>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>


          
         <%-- <table width="100%" cellpadding="6" border="1" rules="rows" style="border-collapse:collapse">
                        <tr>
                            <td style="width:5px">&nbsp;</td>
                            <td style="width:20px" >1.</td>
                            <td style=" text-align:left;">MWUC &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkMwucAlerts" runat="server" onclick="lnkMwucAlerts_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >2. </td>
                            <td style=" text-align:left;">Followup &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkFollowupAlerts" runat="server" onclick="lnkFollowupAlerts_Click" Text=""></asp:LinkButton></td>
                        </tr>
                          <tr>
                            <td >&nbsp;</td>
                            <td >3. </td>
                            <td style=" text-align:left;">Motor &nbsp;</td>
                               <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkMotorAlerts" runat="server" onclick="lnkMotorAlerts_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                        <td >&nbsp;</td>
                        <td >4. </td>
                        <td style=" text-align:left;">Vetting Inspection &nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkVI" runat="server" onclick="lnkVIAlerts_Click" Text=""></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td >5. </td>
                        <td style=" text-align:left;">Planned Inspection &nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkPVI" runat="server" onclick="lnkPVIAlerts_Click" Text=""></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td >6. </td>
                        <td style=" text-align:left;">MTM - ISM Inspection &nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkISMAlerts" runat="server" onclick="lnkISMAlerts_Click" Text=""></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td >7. </td>
                        <td style=" text-align:left;">MTM - ISPS Inspection &nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkISPSAlerts" runat="server" onclick="lnkISPSAlerts_Click" Text=""></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td >8. </td>
                        <td style=" text-align:left;">MTM - TECH Inspection &nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkTECHAlerts" runat="server" onclick="lnkTECHAlerts_Click" Text=""></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td >9. </td>
                        <td style=" text-align:left;">MTM - 14001 Inspection &nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnk14001Alerts" runat="server" onclick="lnk14001Alerts_Click" Text=""></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td >10. </td>
                        <td style=" text-align:left;">MTM - SAFETY Inspection &nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkSafetyAlerts" runat="server" onclick="lnkSafetyAlerts_Click" Text=""></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td >11. </td>
                        <td style=" text-align:left;">MTM - NAV Inspection &nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkNAVAlerts" runat="server" onclick="lnkNAVAlerts_Click" Text=""></asp:LinkButton></td>
                    </tr>
           </table>
          <div class="hdng">CMS </div>
          <div class="hdng">PMS </div>
            <table width="100%" cellpadding="6" border="1" rules="rows" style="border-collapse:collapse">
            <tr>
                <td style="width:5px">&nbsp;</td>
                <td style="width:20px" >1.</td>
                <td style=" text-align:left;">Defect Reporting &nbsp;</td>
                <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkDefectAlerts" runat="server" onclick="lnkDefectAlerts_Click" Text=""></asp:LinkButton></td>
            </tr>
            <tr>
                <td >&nbsp;</td>
                <td >2. </td>
                <td style=" text-align:left;">Drill & Trainings &nbsp;</td>
                <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkDrillTrainingAlerts" runat="server" onclick="lnkDrillTrainingAlerts_Click" Text=""></asp:LinkButton></td>
            </tr>
            </table>--%>

        </td>
          <td style="vertical-align:top;">
          
           <div style="border:solid 1px #c2c2c2; border-bottom:none; color:white; background-color : #777777; font-size :14px; font-weight:bold; padding:10px; box-sizing:border-box;">Company Alerts</div>
           <div class="scrollbox" style="WIDTH: 100%; text-align:center;">
            <asp:Repeater ID="rptAppName_CA" runat="server" >
            <ItemTemplate>
               <div class="hdng"><%#Eval("ApplicationName")%></div>
               <table width="100%" cellpadding="6" border="1" rules="rows" style="border-collapse:collapse">
                <asp:Repeater ID="rptAlerts_CA" DataSource='<%#LoadCompAlerts(Common.CastAsInt32(Eval("ApplicationId")))%>' runat="server" >
                <ItemTemplate>
                    <tr>
                        <td style="width:5px">&nbsp;</td>
                        <td style="width:20px" ><%#Eval("SrNo")%>.</td>
                        <td style=" text-align:left;"><%#Eval("AlertTypeName")%>&nbsp;</td>
                        <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkCompAlerts" runat="server" CommandArgument='<%#Eval("AlertTypeId")%>' onclick="lnkCompAlerts_Click" Text='<%#GetCompAlertCount(Common.CastAsInt32(Eval("AlertTypeId")))%>'></asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
                </asp:Repeater>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
          
           <%--<div class="hdng">VIMS </div>
          <table width="100%" cellpadding="6" border="1" rules="rows" style="border-collapse:collapse">
                        <tr>
                            <td style="width:5px">&nbsp;</td>
                            <td style="width:20px" >1.</td>
                            <td style=" text-align:left;">MWUC &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkMwucAlerts_Comp" runat="server" onclick="lnkMwucAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >2. </td>
                            <td style=" text-align:left;">Followup &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkFollowupAlerts_Comp" runat="server" onclick="lnkFollowupAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                          <tr>
                            <td >&nbsp;</td>
                            <td >3. </td>
                            <td style=" text-align:left;">Motor &nbsp;</td>
                               <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkMotorAlerts_Comp" runat="server" onclick="lnkMotorAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >4. </td>
                            <td style=" text-align:left;">Vetting Inspection &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkVI_Comp" runat="server" onclick="lnkVIAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >5. </td>
                            <td style=" text-align:left;">Planned Inspection &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkPVI_Comp" runat="server" onclick="lnkPVIAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >6. </td>
                            <td style=" text-align:left;">MTM - ISM Inspection &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkISMAlerts_Comp" runat="server" onclick="lnkISMAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >7. </td>
                            <td style=" text-align:left;">MTM - ISPS Inspection &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkISPSAlerts_Comp" runat="server" onclick="lnkISPSAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >8. </td>
                            <td style=" text-align:left;">MTM - TECH Inspection &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkTECHAlerts_Comp" runat="server" onclick="lnkTECHAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >9. </td>
                            <td style=" text-align:left;">MTM - 14001 Inspection &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnk14001Alerts_Comp" runat="server" onclick="lnk14001Alerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >10. </td>
                            <td style=" text-align:left;">MTM - SAFETY Inspection &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkSafetyAlerts_Comp" runat="server" onclick="lnkSafetyAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td >11. </td>
                            <td style=" text-align:left;">MTM - NAV Inspection &nbsp;</td>
                            <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkNAVAlerts_Comp" runat="server" onclick="lnkNAVAlerts_Comp_Click" Text=""></asp:LinkButton></td>
                        </tr>
                        </table>
          <div class="hdng">CMS </div>
          <div class="hdng">PMS </div>
             <table width="100%" cellpadding="6" border="1" rules="rows" style="border-collapse:collapse">
            <tr>
                <td style="width:5px">&nbsp;</td>
                <td style="width:20px" >1.</td>
                <td style=" text-align:left;">Defect Reporting &nbsp;</td>
                <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkDefectAlerts_Comp" runat="server" onclick="lnkDefectAlerts_Comp_Click" Text=""></asp:LinkButton></td>
            </tr>
            <tr>
                <td >&nbsp;</td>
                <td >2. </td>
                <td style=" text-align:left;">Drill & Trainings &nbsp;</td>
                <td style="text-align:right; width:70px"><asp:LinkButton ID="lnkDrillTrainingAlerts_Comp" runat="server" onclick="lnkDrillTrainingAlerts_Comp_Click" Text=""></asp:LinkButton></td>
            </tr>
            </table>--%>
          
         </td>
     </tr>
     </table> 
     </ContentTemplate>
    </asp:UpdatePanel>
    </center> 
    </td> 
    </tr>
    </table>
     </div>
    </form>
</body>
</html>
