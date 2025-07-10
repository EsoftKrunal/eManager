<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FuelTankReadingReport.aspx.cs" Inherits="MRV_FuelTankReadingReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Fuel Tank Monitoring</title>
     <!-- CSS -->
     <link href="style.css?14" rel="stylesheet" type="text/css" />
     
</head>
<body style=" margin: 0 0 0 0;" >
<form id="form1" runat="server">
    <div class="modal" runat="server" id="dvModal" visible="false"></div>
    <div style="text-align: center">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div class="pagename">
                Fuel Tank Monitoring Report
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
                            <td style="text-align:left">Report Date & Time : </td>
                            <td style="text-align:left">
                                <asp:Label runat="server" ID="txtReportDate"  ></asp:Label> 
                                
                                <asp:Label runat="server" ID="txtReportTime"   ></asp:Label> 
                                
                                <b> <i> ( UTC Time )</i></b>
                            </td>
                             <td style="text-align:left">Location :</td>
                             <td style="text-align:left">
                                 <asp:Label runat="server" ID="lblLocation"   ></asp:Label> 
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
                            <h1>Fuel Tank Reading</h1>
                            <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight" width="100%">
                            <tr>
                                <th style="text-align:left">Tank Name</th>
                                <th style="text-align:left">Fuel Type</th>
                                <th style="text-align:right">Sounding</th>
                                <th style="text-align:right">Ullage</th>
                                <th style="text-align:right">Level Gauge</th>
                                <th style="text-align:right">Volume</th>
                                <th style="text-align:right">Temp ( <sup>0</sup> C )</th>
                                <th style="text-align:right">S.G.</th>
                                <th style="text-align:right">Correct S.G.</th>
                                <th style="text-align:right">MT</th>                                
                            </tr>
                            <asp:Repeater runat="server" ID="rptFuelRankReading">
                            <ItemTemplate>
                            <tr>
                                <td style="text-align:left">
                                    <%#Eval("FuelTankName")%>
                                    <asp:HiddenField ID="hfdFuelTankId" runat="server" Value='<%#Eval("FuelTankId") %>' />
                                </td> 
                                <td style="text-align:left">
                                    <%#Eval("FuelTypeName")%>
                                </td>
                                <td style="text-align:right;"><asp:Label ID="txtSounding" runat="server"    Text='<%#Eval("Sounding") %>'></asp:Label></td>
                                <td style="text-align:right;"><asp:Label ID="txtUllage" runat="server"    Text='<%#Eval("Ullage") %>'></asp:Label></td>
                                <td style="text-align:right;"><asp:Label ID="txtLevelGauge" runat="server"    Text='<%#Eval("LevelGauge") %>'></asp:Label></td>
                                <td style="text-align:right;"><asp:Label ID="txtVolume" runat="server"    Text='<%#Eval("Volume") %>'></asp:Label></td>
                                <td style="text-align:right;"><asp:Label ID="txtTemp" runat="server"    Text='<%#Eval("Temp") %>'></asp:Label></td>
                                <td style="text-align:right;"><asp:Label ID="txtSG" runat="server"    Text='<%#Eval("SG") %>'></asp:Label></td>
                                <td style="text-align:right;"><asp:Label ID="txtCorrectSG" runat="server"    Text='<%#Eval("CorrectSG") %>'></asp:Label></td>
                                <td style="text-align:right;">
                                    <asp:Label ID="txtMT" runat="server"  Text='<%# Math.Abs( Common.CastAsDecimal(Eval("CorrectSG"))*Common.CastAsDecimal(Eval("Volume"))).ToString("0.00") %>'></asp:Label>
                                </td>
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
                <input type="button" class="btn" value="Close" onclick='window.close();'/>
            </div>
            
            
        </div>
    
    
    </form>
     
</body>
</html>
                                        
