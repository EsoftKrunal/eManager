<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BunkerReport.aspx.cs" Inherits="MRV_BunkerReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Bunker Readings</title>
     <!-- CSS -->
     <link href="style.css?14" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../eReports/JS/KPIScript.js"></script> 
</head>
<body style=" margin: 0 0 0 0;" >
<form id="form1" runat="server">
    <div class="modal" runat="server" id="dvModal" visible="false"></div>
    <div style="text-align: center">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div class="pagename">
                Bunker Report
            </div>
            <div style="background-color:#ebedef" >
                <div class="padding-1" >
             <table cellpadding="0" cellspacing="0" border="0" class="padded" width="100%">
                     <tr>
                            <td style="text-align:left">Voyage No. :</td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblVoyageNo"></asp:Label> </td>
                            <td style="text-align:left">From - To Port : </td>
                            <td style="text-align:left">
                                <asp:Label runat="server" ID="lblFromPort"></asp:Label> 
                                -
                                <asp:Label runat="server" ID="lblToPort"></asp:Label>
                            </td>
                            <td style="text-align:left">Voyage Start Date : </td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblStartDate"></asp:Label> </td>
                    </tr>  
                 <tr>
                            <td style="text-align:left">Bunker Date & Time : </td>
                            <td style="text-align:left">
                                <asp:Label runat="server" ID="txtReportDate"  Width="90px" ></asp:Label> 
                                
                                <asp:Label runat="server" ID="txtReportTime" Width="50px" ></asp:Label> 
                                
                                <b> <i> ( UTC Time )</i></b>
                            </td>
                            <td style="text-align:left">Bunker Location :</td>
                             <td style="text-align:left">
                                  <asp:Label runat="server" ID="txtLocation"  ></asp:Label>
                                  
                              
                             </td>
                          <td></td>
                     <td></td>
                    </tr>  
                 <tr>
                     
                     <td style="text-align:left">Master Name :</td>
                     <td style="text-align:left">
                         <asp:Label runat="server" ID="txtMaterName"  ></asp:Label>
                                  
                     </td>
                     <td style="text-align:left">Chief Engineer Name :</td>
                     <td style="text-align:left">
                         <asp:Label runat="server" ID="txtChiefEngineerName"  ></asp:Label> 
                                 
                     </td>
                     <td style="text-align:left">
                         Verified By / On :
                     </td>
                     <td style="text-align:left">
                         <asp:Label ID="lblVerifiedByOn" runat="server"></asp:Label>
                     </td>
                 </tr>
            </table>
                </div>
                </div>
            <div>
                <table cellpadding="0" cellspacing="0" border="0" class="" width="100%">
                    <tr>
                        <td style="vertical-align:top;">
                            <h1>Fuel Tank Readings</h1>
                            <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight" width="100%">
                            <tr>
                                <th style="text-align:left">Tank Name</th>
                                <th style="text-align:left">Fuel Type</th>
                                <th style="text-align:right">Received ( MT )</th>
                                <th style="text-align:right">Density At 15<sup>0</sup> C</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptBunkering">
                            <ItemTemplate>
                            <tr>
                                <td style="text-align:left">
                                    <%#Eval("FuelTankName")%>
                                    <asp:HiddenField ID="hfdFuelTankId" runat="server" Value='<%#Eval("FuelTankId") %>' />
                                </td> 
                                <td style="text-align:left">
                                    <%#Eval("FuelTypeName")%>
                                </td>
                                <td style="text-align:right;"><asp:Label ID="txtReceived" runat="server"   Text='<%#Eval("Received") %>'></asp:Label></td>
                                <td style="text-align:right;"><asp:Label ID="txtDensityAt15Deg" runat="server" Text='<%#Eval("DensityAt15Deg") %>'></asp:Label></td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                    </table>
            </div>
             <div style="border-bottom:solid 1px #0094ff">&nbsp;</div>
        <div style="margin:5px;">
                            <%--<asp:Button runat="server" id="btnSave" CssClass="btn" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button runat="server" id="btnVerifyOpenWindow" CssClass="btn" Text="Verify" OnClick="btnVerifyOpenWindow_Click" />--%>
                            <asp:Button runat="server" id="btnClose" CssClass="btn" Text="Close" OnClick="btnClose_Click" OnClientClick="<script> window.close();<script>" />

            </div>
            
            
        </div>
    
    
    </form>
   
</body>
</html>
                                        
