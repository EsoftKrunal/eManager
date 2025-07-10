<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewTemplate.aspx.cs" Inherits="HSSQE_RiskManagement_ViewTemplate" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <title>View Template</title>
    <style type="text/css">
    .g{background-color:Green;color:White;}
    .r{background-color:red;color:White;}
    .a{background-color:#FFB2B2;}
    .b{background-color:#66CCFF;}
    </style>
</head>
<body style="font-family:Calibri; font-size:13px;">
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div style="position:fixed;top:0px; width:100%; height:135px; border-bottom:Solid 5px #47A3FF; background-color:#eeeeee;">
        <div style="background-color:#47A3FF; padding:10px; font-size:14px; text-align:center; color:White">Risk Template</div>
        <div style="overflow:none;">
        <div style="float:left; width:70%;">
        <table cellspacing="0" rules="none" border="0" cellpadding="5" style="width:100%;border-collapse:collapse;">
        <tr>
        <td style="text-align:right;font-weight:bold;width:120px;">Template Code :&nbsp;</td>
        <td style="text-align:left;"><asp:Label runat="server" ID="lblTempCode" ></asp:Label></td>
        </tr>
        <tr>
        <td style="text-align:right; font-weight:bold; width:120px;">Event Name :&nbsp;</td>
        <td style="text-align:left;"><asp:Label runat="server" ID="lblEventName" ></asp:Label>
        </td>
        </tr>
        </table>
        </div>
         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse; height:50px;">
                <tr style="font-weight:bold; background-color:#dddddd;">
                    <td style="text-align:center;width:30px;">&nbsp;</td>
                    <td style="text-align:left;width:150px; "></td>
                    <td colspan="12" style="text-align:center;width:360px; background-color:#CA911F">INITIAL RISK</td>
                    <td style="text-align:left;width:150px;">CONTROL MEASURES</td>
                    <td colspan="12" style="text-align:center;width:360px; background-color:#6699ff">RESIDUAL RISK</td>
                    <td style="text-align:left;border-left:solid 3px black;width:150px;">Additional Control Measures</td>
                    <td style="text-align:center;width:100px;">Proceed To Work</td>
                    <td style="text-align:center;width:30px;">&nbsp;</td>
                </tr>
                <tr style="font-weight:bold; " class= "headerstylegrid">
                    <td style="text-align:center;width:30px;">View</td>
                    <td style="text-align:left;width:150px; ">Hazard </td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#CA911F">People (P) </td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#CA911F">Environment (E)</td>                                
                    <td colspan="3" style="text-align:center;width:90px;background-color:#CA911F">Asset (A) </td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#CA911F">Reputation (R) </td>
                     <td style="text-align:left;width:150px;"></td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#6699ff">People (P)</td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#6699ff">Environment (E)</td>    
                    <td colspan="3" style="text-align:center;width:90px;background-color:#6699ff">Asset (A)</td>
                    <td colspan="3" style="text-align:center;width:90px;background-color:#6699ff">Reputation (R)</td>
                    <td style="text-align:left; border-left:solid 3px black;width:150px;"></td>
                    <td style="text-align:center;width:100px;background-color:#99CCFF"></td>
                    <td style="text-align:center;width:30px;">Edit</td>
                </tr>
                 <tr style="font-weight:bold; " class= "headerstylegrid">
                    <td style="text-align:center;width:30px;"></td>
                    <td style="text-align:left;width:150px; ">Consequences</td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblSeverityPI" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblLikeliHoodPI" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblRiskLevelPI" runat="server" Text="R" ToolTip="Risk Level"></asp:Label></td>
                   <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblSeverityEI" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblLikeliHoodEI" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblRiskLevelEI" runat="server" Text="R" ToolTip="Risk Level"></asp:Label></td>
                   <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblSeverityAI" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblLikeliHoodAI" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblRiskLevelAI" runat="server" Text="R" ToolTip="Risk Level"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblSeverityRI" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblLikeliHoodRI" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#CA911F"><asp:Label ID="lblRiskLevelRI" runat="server" Text="R" ToolTip="Risk Level"></asp:Label></td> 
                      <td style="text-align:left; width:150px;"></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblSeverityPF" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblLikeliHoodPF" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>                                                        
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblRiskLevelPF" runat="server" Text="R" ToolTip="Risk Level"></asp:Label> </td>
                     <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblSeverityEF" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblLikeliHoodEF" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>                                                        
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblRiskLevelEF" runat="server" Text="R" ToolTip="Risk Level"></asp:Label> </td>
                     <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblSeverityAF" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblLikeliHoodAF" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>                                                        
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblRiskLevelAF" runat="server" Text="R" ToolTip="Risk Level"></asp:Label> </td>
                     <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblSeverityRF" runat="server" Text="S" ToolTip="Severity"></asp:Label></td>
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblLikeliHoodRF" runat="server" Text="L" ToolTip="LikeliHood"></asp:Label></td>                                                        
                    <td style="text-align:center;width:30px;background-color:#6699ff"><asp:Label ID="lblRiskLevelRF" runat="server" Text="R" ToolTip="Risk Level"></asp:Label> </td>
                     <td style="text-align:left; border-left:solid 3px black;width:150px;"></td>
                    <td style="text-align:center;width:100px;background-color:#99CCFF"></td>
                    <td style="text-align:center;width:30px;"></td>
                </tr>
        </table>
        </div>
    </div>
    <div style="padding:0px;padding-top:185px;padding-bottom:100px; ">
    <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse;">
            <asp:Repeater ID="rptHazardsNew" runat="server">
            <ItemTemplate>
                <div style="padding:3px;"><b><%#Eval("Hazardname1")%></b></div>
                <table cellspacing="0" rules="all" border="1" cellpadding="3" style="width:100%;border-collapse:collapse;">
                <asp:Repeater ID="rptHazardsTask" DataSource='<%#BindTasksNew(Common.CastAsInt32(Eval("Hazard_TableId")))%>' runat="server">
                <ItemTemplate>
                 <tr>
                    <td style="text-align:left;width:30px;"></td>
                    <td align="left" style="text-align:left;width:150px;"><%#Eval("ConsequencesName1")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("SeverityPI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("LikeliHoodPI")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("RisklevelPI"))%>'><%#Eval("RisklevelPI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("SeverityEI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("LikeliHoodEI")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("RisklevelEI"))%>'><%#Eval("RisklevelEI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("SeverityAI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("LikeliHoodAI")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("RisklevelAI"))%>'><%#Eval("RisklevelAI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("SeverityRI")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("LikeliHoodRI")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("RisklevelRI"))%>'><%#Eval("RisklevelRI")%></td>
                     <td align="left" style="text-align:left;width:150px;" ><%#Eval("ControlMeasures")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityPF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_LikeliHoodPF")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("Re_RisklevelPF"))%>'><%#Eval("Re_RisklevelPF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityEF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_LikeliHoodEF")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("Re_RisklevelEF"))%>'><%#Eval("Re_RisklevelEF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityAF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_LikeliHoodAF")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("Re_RisklevelAF"))%>'><%#Eval("Re_RisklevelAF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityRF")%></td>
                    <td align="center" style="text-align:center;width:30px;"><%#Eval("Re_SeverityRF")%></td>
                    <td align="center" style="text-align:center;width:30px;" class='<%#GetCSSColor(Eval("Re_RisklevelRF"))%>'><%#Eval("Re_RisklevelRF")%></td>
                    <td align="left" style="border-left:solid 3px black;width:150px;" ><%#Eval("ADD_CONTROL_MEASURES")%></td>
                     <td style="text-align:center;width:100px;"><%#((Eval("Proceed").ToString()=="Y")?"Yes":(Eval("Proceed").ToString()=="N")?"No":"")%></td>
                   
                   <td align="center" style="border:solid 1px #c2c2c2;width:30px;">
                            <asp:ImageButton ID="btnViewHazard" runat="server" CommandArgument='<%#Eval("TableId")%>' OnClick="btnViewHazard_Click" ImageUrl="~/Modules/HRD/Images/search_magnifier_12.png" ToolTip="View Hazard" />
                        </td>
                </tr>

                </ItemTemplate>
                </asp:Repeater>
                </table>
            </ItemTemplate>
            </asp:Repeater>
            </table>
   
    </div>
    <div style="position:fixed;bottom:0px; width:100%; padding:10px; background-color:#DDDDDD;">
        <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;">
        <tr>
            <td style="text-align:right; width:123px; "><b>INITIAL RISK :</b>&nbsp;</td>
            <td style="text-align:center; width:20px;"><asp:Image runat="server" ID="imgER" /></td>
            <td style="text-align:left; width:545px; "><asp:Label ID="lblExtAction" runat="server"></asp:Label></td>
            <td style="text-align:right; width:123px;"><b>Residual Risk :</b>&nbsp;</td>
            <td style="text-align:center; width:20px;"><asp:Image runat="server" ID="imgRR" /></td>
            <td style="text-align:left; "><asp:Label ID="lblResAction" runat="server"></asp:Label></td>
        </tr>
        </table>
         <hr />
        <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
            <tr>
            <td style="text-align:center; width:33%; font-weight:bold;">Created By/ On :&nbsp;<asp:Label runat="server" ID="lblCreatedByOn" ></asp:Label></td>
                <td style="text-align:center; width:34%; font-weight:bold;">Modified By/ On :&nbsp;<asp:Label runat="server" ID="lblModifiedByOn" ></asp:Label></td>
            <td style="text-align:center; width:33%; font-weight:bold;">Approved By/ On :&nbsp;<asp:Label runat="server" ID="lblApprovedByOn" ></asp:Label></td>
        </tr>
        </table>
        <hr />
        <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
        <tr>
            <td>&nbsp;<asp:Label runat="server" ID="Label1" ></asp:Label></td>
            <td style="text-align:right; width:300px; padding-right:20px;">
                <asp:Button runat="server" ID="btnModify" OnClick="btnModify_Click" Text="Modity Template" CssClass="btn" Width="130px" />
                <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="btn" Width="130px" OnClientClick="window.close();" />
            </td>
        </tr>
        </table>
    </div>
    </div>
    <div ID="dv_NewTask" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
       <center>
       <asp:HiddenField ID="hfdTaskIdNew" runat="server" />
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :0px; text-align :center;background : white; z-index:150;top:0px; border:solid 5px black;">
        <center >
                <div class="box3" style='padding:10px 0px 10px  0px'><b>>Add / Edit Risk Management</b></div>
                <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;text-align:left; margin-left:10px;">
                <tr>
                <td style="width:100px">Hazard</td>
                <td style="width:10px;">:</td>
                <td>
                <div style="max-height:100px; overflow-x:hidden; overflow-y:scroll; min-height:20px; width:98%">
                    <asp:Label runat="server" ID="lblHazard" Width="95%"></asp:Label></div>
                </td>
                </tr>
                <tr>
                <td style="width:100px">Consequences</td>
                <td style="width:10px;">:</td>
                <td><asp:Label runat="server" ID="lblTask" Width="95%" ></asp:Label></td>
                </tr>
              
                </table>
                <div class="box3" style='padding:5px 0px 5px  0px'><b>INITIAL RISK</b></div>
                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <colgroup>
                    <col width="10%" />
                    <col width="10%"  />
                    <col width="6%"  />
                    <col width="10%" />
                    <col width="22%" />
                    <col width="10%" />
                    <col width="32%" />
                
                <tr style="font-weight:bold; " class= "headerstylegrid">
                    <td>
                        &nbsp;Harm To
                    </td>
                    <td colspan="2">
                        &nbsp;Severity
                    </td>
                    <td colspan="2">
                        &nbsp;Likelihood
                    </td>
                    <td>
                         &nbsp;Risk level 
                    </td>
                    <td>
                         &nbsp;Risk Text 
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;People (P)  </td>
                                <td style="  text-align:left;width:10%;">
                                     <asp:Label ID="lblR11PI" runat="server" Font-Bold="true" style="text-align:center"></asp:Label>
                                    </td>
                                  <td style="text-align:left;width:6%;">
                                      <%-- <asp:Label ID="lblSeverityTextPI" runat="server"></asp:Label>--%>
                                       <asp:ImageButton ID="imgbtnSeveritypi" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                  </td>
                                   <td style="  text-align:left;width:10%;">
                                       <asp:Label ID="lblR12PI" runat="server" Font-Bold="true"  style="text-align:center"></asp:Label>
                                       </td>
                                   <td style="text-align:left;width:22%;">
                                        <asp:Label ID="lblLikelihoodTextPI" runat="server"></asp:Label>
                                       </td>
                                   <td style="  text-align:left;width:10%;" runat="server" id="rd_RlPI" >
                                        <asp:Label ID="lblR13PI" runat="server" Font-Bold="true" ></asp:Label>
                                       </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblRiskTextPI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                    </tr>
                             </table>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="7">
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Environment (E)  </td>
                                <td style="  text-align:left;width:10%;">
                                     <asp:Label ID="lblR11EI" runat="server" Font-Bold="true" style="text-align:center"></asp:Label>
                                    </td>
                                  <td style="text-align:left;width:6%;">
                                      <%-- <asp:Label ID="lblSeverityTextPI" runat="server"></asp:Label>--%>
                                       <asp:ImageButton ID="imgbtnSeverityEI" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                  </td>
                                   <td style="  text-align:left;width:10%;">
                                       <asp:Label ID="lblR12EI" runat="server" Font-Bold="true"  style="text-align:center"></asp:Label>
                                       </td>
                                   <td style="text-align:left;width:22%;">
                                        <asp:Label ID="lblLikelihoodTextEI" runat="server"></asp:Label>
                                       </td>
                                   <td style="  text-align:left;width:10%;" runat="server" id="rd_RlEI" >
                                        <asp:Label ID="lblR13EI" runat="server" Font-Bold="true" ></asp:Label>
                                       </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblRiskTextEI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                    </tr>
                             </table>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="7">
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Asset (A)  </td>
                                <td style="  text-align:left;width:10%;">
                                     <asp:Label ID="lblR11AI" runat="server" Font-Bold="true" style="text-align:center"></asp:Label>
                                    </td>
                                  <td style="text-align:left;width:6%;">
                                      <%-- <asp:Label ID="lblSeverityTextPI" runat="server"></asp:Label>--%>
                                       <asp:ImageButton ID="imgbtnSeverityAI" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                  </td>
                                   <td style="  text-align:left;width:10%;">
                                       <asp:Label ID="lblR12AI" runat="server" Font-Bold="true"  style="text-align:center"></asp:Label>
                                       </td>
                                   <td style="text-align:left;width:22%;">
                                        <asp:Label ID="lblLikelihoodTextAI" runat="server"></asp:Label>
                                       </td>
                                   <td style="  text-align:left;width:10%;" runat="server" id="rd_RlAI" >
                                        <asp:Label ID="lblR13AI" runat="server" Font-Bold="true" ></asp:Label>
                                       </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblRiskTextAI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                    </tr>
                             </table>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="7">
                         <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Reputation (R)  </td>
                                <td style="  text-align:left;width:10%;">
                                     <asp:Label ID="lblR11RI" runat="server" Font-Bold="true" style="text-align:center"></asp:Label>
                                    </td>
                                  <td style="text-align:left;width:6%;">
                                      <%-- <asp:Label ID="lblSeverityTextPI" runat="server"></asp:Label>--%>
                                       <asp:ImageButton ID="imgbtnSeverityRI" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                  </td>
                                   <td style="  text-align:left;width:10%;">
                                       <asp:Label ID="lblR12RI" runat="server" Font-Bold="true"  style="text-align:center"></asp:Label>
                                       </td>
                                   <td style="text-align:left;width:22%;">
                                        <asp:Label ID="lblLikelihoodTextRI" runat="server"></asp:Label>
                                       </td>
                                   <td style="  text-align:left;width:10%;" runat="server" id="rd_RlRI" >
                                        <asp:Label ID="lblR13RI" runat="server" Font-Bold="true" ></asp:Label>
                                       </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblRiskTextRI" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                    </tr>
                             </table>
                        </td>
                    </tr>
                    </colgroup>
                 </table>

               <%-- <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse; text-align:center;">
                <tr>
                <td >
                    <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style=" background-color:#dddddd; text-align:left; width:100px;">&nbsp;Severity  : </td>
                                <td style="width:160px;">&nbsp;
                                   
                                </td>
                                <td style="text-align:left;">
                                   
                                </td>
                                </tr>
                                <tr>
                                <td style=" background-color:#dddddd; text-align:left;">&nbsp;Likelihood :</td>
                                <td >&nbsp;
                                    
                                </td>
                                <td style="text-align:left;">
                                   
                                </td>
                                </tr>
                                <tr>
                                <td style=" background-color:#dddddd; text-align:left;">&nbsp;Risk level :</td>
                                <td >&nbsp;
                                   
                                </td>
                                <td style="text-align:left;">
                                   
                                </td>
                              </tr>
                            </table>
                </td>
                </tr>
                </table>--%>
                  <table cellpadding="0" cellspacing="0" width="100%" border="0" style="padding:2px 0px 2px 0px;">
                     <tr>
                          <td style="width:120px"><b>Control Measures</b></td>
                <td style="width:20px">:</td>
                <td style="text-align:left">
                   <asp:TextBox runat="server" ID="txtStdCM" Width="99%" TextMode="MultiLine" Height="50px" CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0"></asp:TextBox>
                    </td>
                    </tr>
                </table>
                <div class="box3" style='padding:5px 0px 5px  0px'><b>CONTROL MEASURES</b></div>
               <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <colgroup>
                    <col width="10%" />
                    <col width="10%"  />
                    <col width="6%"  />
                    <col width="10%" />
                    <col width="22%" />
                    <col width="10%" />
                    <col width="32%" />
                <tr style="font-weight:bold; " class= "headerstylegrid">
                    <td>
                        &nbsp; Harm To
                    </td>
                    <td colspan="2">
                        &nbsp;Severity
                    </td>
                    <td colspan="2">
                        &nbsp;Likelihood
                    </td>
                    <td>
                         &nbsp;Risk level 
                    </td>
                    <td>
                         &nbsp;Risk Text 
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                      
                             <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;People (P)  </td>
                                <td style="  text-align:left;width:10%;">
                                    <asp:Label ID="lblReR11PF" runat="server" Font-Bold="true" style="text-align:center"></asp:Label>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    <asp:ImageButton ID="ibReSeverityPF" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:Label ID="lblReR12PF" runat="server" Font-Bold="true"  style="text-align:center"></asp:Label>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblReLikelihoodTextPF" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_ReR1PF">
                                      <asp:Label ID="lblReR13PF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblReRiskTextPF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="7">  
                      
                             <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Environment (E)  </td>
                                <td style="  text-align:left;width:10%;">
                                      <asp:Label ID="lblReR11EF" runat="server" Font-Bold="true" style="text-align:center"></asp:Label>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    <asp:ImageButton ID="ibReSeverityEF" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                     <asp:Label ID="lblReR12EF" runat="server" Font-Bold="true"  style="text-align:center"></asp:Label>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblReLikelihoodTextEF" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_ReR1EF">
                                      <asp:Label ID="lblReR13EF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblReRiskTextEF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                            
                    </td>
                    </tr>
                    <tr>
                    <td colspan="7">  
                             <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Asset (A)  </td>
                                <td style="  text-align:left;width:10%;">
                                      <asp:Label ID="lblReR11AF" runat="server" Font-Bold="true" style="text-align:center"></asp:Label>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    <asp:ImageButton ID="ibReSeverityAF" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                       <asp:Label ID="lblReR12AF" runat="server" Font-Bold="true"  style="text-align:center"></asp:Label>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblReLikelihoodTextAF" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_ReR1AF">
                                      <asp:Label ID="lblReR13AF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblReRiskTextAF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                        
                    </td>
                    </tr>
                    <tr>
                    <td colspan="7">  

                             <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style="  text-align:left;width:10%;">&nbsp;Reputation (R)  </td>
                                <td style="  text-align:left;width:10%;">
                                       <asp:Label ID="lblReR11RF" runat="server" Font-Bold="true" style="text-align:center"></asp:Label>
                                </td>
                                <td style="text-align:center;width:6%;">
                                    <asp:ImageButton ID="ibReSeverityRF" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip="" />
                                </td>
                                 <td style="  text-align:left;width:10%;">
                                      <asp:Label ID="lblReR12RF" runat="server" Font-Bold="true"  style="text-align:center"></asp:Label>
                                 </td>
                                  <td style="text-align:left;width:22%;">
                                    <asp:Label ID="lblReLikelihoodTextRF" runat="server"></asp:Label>
                                </td>
                                  <td style="width:10%;"  runat="server" id="rd_ReR1RF">
                                      <asp:Label ID="lblReR13RF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                  <td style="text-align:left;width:32%;">
                                       <asp:Label ID="lblReRiskTextRF" runat="server" Font-Bold="true"></asp:Label>
                                  </td>
                                </tr>
                                
                               
                            </table>
                          
                    </td>
                    </tr>
                    </colgroup>
                 </table>
                <%--<table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse; text-align:center;">
                <tr>
                <td style="width:50%" >
                  <asp:TextBox runat="server" ID="txtACM" Width="99%" TextMode="MultiLine" Height="125px" CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0"></asp:TextBox>
                </td>
                <td >
                    <table cellspacing="0" rules="all" border="1" cellpadding="3" style="border-collapse:collapse; text-align:center; width:100%;">
                              <tr >
                                <td style=" background-color:#dddddd; text-align:left; width:100px;">&nbsp;Severity  : </td>
                                <td style=" text-align:center; width:160px;">&nbsp;
                                    <asp:Label ID="lblReR11" runat="server" Font-Bold="true" style="text-align:center"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblReSeverityText" runat="server"></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                <td style=" background-color:#dddddd; text-align:left;">&nbsp;Likelihood :</td>
                                <td >&nbsp;
                                    <asp:Label ID="lblReR12" runat="server" Font-Bold="true"  style="text-align:center"></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblReLikelihoodText" runat="server"></asp:Label>
                                </td>
                                </tr>
                                <tr>
                                <td style=" background-color:#dddddd; text-align:left;">&nbsp;Risk level :</td>
                                <td runat="server" id="rd_ReRl" >&nbsp;
                                    <asp:Label ID="lblReR13" runat="server" Font-Bold="true" ></asp:Label>
                                </td>
                                <td style="text-align:left;">
                                    <asp:Label ID="lblReRiskText" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                              </tr>
                            </table>
                </td>
                </tr>
                </table>--%>
               <div class="box3" style='padding:5px 0px 5px  0px'><b>ADDITIONAL CONTROL MEASURES </b></div>
               <table cellspacing="0" rules="none" border="0" cellpadding="2" style="width:100%;border-collapse:collapse;text-align:left; margin-left:10px;">
                      <tr>
               
                <td > <asp:TextBox runat="server" ID="txtACM" Width="99%" TextMode="MultiLine" Height="50px"  CssClass="withborder" ValidationGroup="a1" BackColor="#FFFFE0"></asp:TextBox></td>
                </tr>
                </table>

                <div runat="server" visible="false" style="height:345px; ">
                   <table cellspacing="0"  border="0" cellpadding="1" style="width:100%;border-collapse:collapse; font-size:12px;">
                     <tr>
                         <td style="width:45%">
                        <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                            <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td></td>
                                <td colspan="7">Severity of Consequences</td>
                           </tr>
                            <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td rowspan="7" style="writing-mode: tb-rl;">Likelihood of Consequences</td>
                                <td></td>
                                <td></td>
                                <td>Negligible</td>
                                <td>Minor</td>
                                <td>Major</td>
                                <td>Severe</td>
                                <td>Catastropic</td>
                           </tr>
                           <tr style=" background-color:#c2c2c2; font-weight:bold;">
                                <td></td>
                                <td>Rating</td>
                                <td>1</td>
                                <td>2</td>
                                <td>3</td>
                                <td>4</td>
                                <td>5</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Very Likely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">5</td>
                               <td class="btnact g">5</td>
                               <td class="btnact b">10</td>
                               <td class="btnact a">15</td>
                               <td class="btnact r">20</td>
                               <td class="btnact r">25</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Likely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">4</td>
                               <td class="btnact g">4</td>
                               <td class="btnact b">8</td>
                               <td class="btnact a">12</td>
                               <td class="btnact r">16</td>
                               <td class="btnact r">20</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">3</td>
                               <td class="btnact g">3</td>
                               <td class="btnact g">6</td>
                               <td class="btnact b">9</td>
                               <td class="btnact a">12</td>
                               <td class="btnact a">15</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Highly Unlikely</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">2</td>
                               <td class="btnact g">2</td>
                               <td class="btnact g">4</td>
                               <td class="btnact g">6</td>
                               <td class="btnact b">8</td>
                               <td class="btnact b">10</td>
                           </tr>
                           <tr>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:left;">Almost Nil Chances</td>
                               <td style=" background-color:#c2c2c2; font-weight:bold; text-align:center;">1</td>
                               <td class="btnact g">1</td>
                               <td class="btnact g">2</td>
                               <td class="btnact g">3</td>
                               <td class="btnact g">4</td>
                               <td class="btnact g">5</td>
                           </tr>

                       </table>
                             <br />
                             <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                             <tr style="background-color:#c2c2c2;">
                                  <td style="width:60px">Colour</td>
                                  <td style="width:100px">Risk Score (AxB)</td>
                                  <td style="width:50px">Classification</td>
                                  <td>Action</td>
                             </tr>
                             <tr>
                                  <td class="r">Red</td>
                                  <td>16 - 25</td>
                                  <td class="r">High</td>
                                  <td style="text-align:left">Do not undertake task.If operation is already in progress, abort and inform office.</td>
                             </tr>
                             <tr>
                                  <td class="a">Amber</td>
                                  <td>12 - 15</td>
                                  <td class="a">Warning</td>
                                  <td style="text-align:left">Job to be under taken only with office Approval.</td>
                             </tr>
                             <tr>
                                  <td class="b">Blue</td>
                                  <td>8 - 10</td>
                                  <td class="b">Medium</td>
                                  <td style="text-align:left">Job can be under taken by ship staff with direct supervision of Master and/ or Chief Engineer.</td>
                             </tr>
                             <tr>
                                  <td class="g">Green</td>
                                  <td>1 - 6</td>
                                  <td class="g">Low</td>
                                  <td style="text-align:left">Job can be under taken by ship staff.</td>
                             </tr>
                             </table>
                             
                         </td>
                         <td>
                            <table cellspacing="0"  border="1" cellpadding="3" style="width:100%;border-collapse:collapse; font-size:12px;">
                            <tr style="background-color:#c2c2c2;">
                                  <td colspan="2">Likelihood(of Consequence)</td>
                                  <td colspan="2">Severity(of Consequence) </td>
                             </tr>
                             <tr >
                                  <td>Very Likely</td>
                                  <td style="text-align:left">More frequently than 10% of the time/ cases</td>
                                  <td>Catastrophic</td>
                                  <td style="text-align:left">* Serious loss of reputation which is devastating for trust and respect<br />
                                      * Considerable economic loss which can not be restored<br />
                                      * More than 5 m3 to sea                                
                                  </td>
                             </tr>
                             <tr >
                                  <td>Likely</td>
                                  <td style="text-align:left">Occurs between 1% and 10% of the time/ cases</td>
                                  <td>Severe</td>
                                  <td style="text-align:left">* Serious loss of reputation that will influence trust and respect for a long time<br />
                                      * Lagre economic loss more than US$100,000 that can be restored <br />
                                      * 1 to 5 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>UnLikely</td>
                                  <td style="text-align:left">Occurs between 0.1% and 1% of the time/ cases</td>
                                  <td>Major</td>
                                  <td style="text-align:left">* Reduction of reputation that may influence trust and respect<br />
                                      * Economic loss between US$10,000 and US$100,000 which can be restored <br />
                                      * Less than 1 m3 to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Highly UnLikely</td>
                                  <td style="text-align:left">Occurs less than 0.1% of the time/ cases</td>
                                  <td>Minor</td>
                                  <td style="text-align:left">* Small reduction of reputation in the short run<br />
                                      * Economic loss upto US$10,000 which can be restored <br />
                                      * Sheen on sea : evidance of loss to sea
                                  </td>
                             </tr>
                             <tr >
                                  <td>Almost Nill Chances</td>
                                  <td style="text-align:left">Never heard within the industry</td>
                                  <td>Negligible</td>
                                  <td style="text-align:left">* No effect on reputation<br />
                                      * Negligible economic loss which can be restored <br />
                                      * Nill to sea : contained onboard
                                  </td>
                             </tr>
                             </table>
                         </td>
                     </tr>
                   </table>
                </div>
          </center>
          <div style="padding:3px; text-align:right; border-top:solid 2px #c2c2c2; background-color:#FFFFDB">
            <asp:Button runat="server" ID="btnCancelHazard" Text="Close" OnClick="btnCancelHazard_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
          </div>
          </div>
        </center>
    </div>
    </form>
</body>
</html>
