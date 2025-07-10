<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewActivity.aspx.cs" Inherits="MRV_NewActivity"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Voyage Activity Report ( Since Last Activity) </title>
     <!-- CSS -->
     <link href="style.css?14" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../eReports/css/jquery.datetimepicker.css"/>
     
     <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
     <script src="../eReports/JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="../eReports/JS/AutoComplete/jquery-ui.css" />
     <script src="../eReports/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
     <!-- KPI -->
     <script type="text/javascript" src="../eReports/JS/KPIScript.js"></script> 
    <script type="text/javascript">
        function closeWindow() {
            try {
                if (window.opener != null)
                    if (window.opener.ReloadPage != undefined)
                        window.opener.ReloadPage();
                window.close();
            }
            catch (err) {

            }
        }
    </script>
    <style  type="text/css">
        .red
        {
            color:red;font:bold;
        }
    </style>
</head>
<body style=" margin: 0 0 0 0;" >
<form id="form1" runat="server">
    <div class="modal" runat="server" id="dvModal" visible="false"></div>
    <div style="text-align: center;">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div class="pagename">
                Voyage Activity Report ( Since Last Activity) 
            </div>

            <div style="background-color:#fbfbfb;" >
                <div >
                    <h1>Voyage Details</h1>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="padded bordered">
                     <tr>
                            <td style="text-align:left; width:150px;">Voyage No.</td>
                            <td style="text-align:left">: <asp:Label runat="server" ID="lblVoyageNo"></asp:Label> </td>

                          </tr> <tr>
                            <td style="text-align:left">From - To Port</td>
                            <td style="text-align:left">: 
                                <asp:Label runat="server" ID="lblFromPort"></asp:Label> 
                                -
                                <asp:Label runat="server" ID="lblToPort"></asp:Label>
                            </td>
                              </tr> <tr>
                            <td style="text-align:left">Voyage Start Date</td>
                            <td style="text-align:left">: <asp:Label runat="server" ID="lblStartDate"></asp:Label> </td>
                    </tr>  
                 </table>
                    
                    <h1>Activity Details</h1>
                    
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" class="padded bordered">
                      <col width="150px" />
                        <col />
                    <tr>
                          <td style="text-align:left;width:150px;">Reporting Activity</td>
                         <td style="text-align:left">: 
                            <asp:Label ID="lblActivityName" runat="server" ></asp:Label>
                         </td>
                       </tr>  
                      <tr id="trCurrentCargoWeight" runat="server">
                          <td style="text-align:left">Current Cargo Weight</td>
                     <td style="text-align:left"> 
                         : <asp:Label ID="lblCargoWeight" runat="server"></asp:Label>
                         <b> <i> ( MT )</i></b>
                             
                     </td>
                      </tr>
                 <tr>
                       <td style="text-align:left">Start Time</td>
                            <td style="text-align:left">: <asp:Label ID="lblStartTime" runat="server"></asp:Label><b> <i> ( UTC Time )</i></b>
                            </td>                            
                       </tr>  
            </table>
                    <%----------------------------------------------------------------------------------------------------------------------------%>
                    <div runat="server" id="dvFMReadings" visible="false">
                    <h1 style="background-color:#9bd0f7">
                        <asp:Literal runat="server" id="lblFlowMeterReadingsPopup1"></asp:Literal>
                        </h1>                            
                    <div style="border:none;border-top:none;">                            
                            <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight padded" width="100%">
                        <tr>
                            <th style="text-align:left">Flow Meter Name</th>
                            <th style="text-align:left">Flow Unit</th>
                            <th style="text-align:center">Fuel Grade</th>
                            <th style="text-align:center">Density At 15<sup>0</sup> C </th>
                            <th style="text-align:center">Last Reading</th>
                            <th style="text-align:center">Current Reading</th>
                            <th style="text-align:center">Flow Meter Temp ( <sup>0</sup> C )</th> 
                            <th style="text-align:center" title="Formula=(Density At 15 Deg.C - 0.0011 )*(1-(0.00064*(Flow Meter Temp Deg.C - 15 ))))">Density Weight Correction Factor</th>
                            <th style="text-align:center">Fuel Consumed (MT)</th>         
                            <th style="text-align:left;width:100px;"> Defect# </th>         
                        </tr>
                        <asp:Repeater runat="server" ID="rptDetails" >
                        <ItemTemplate>
                        <tr class='<%#(Eval("IsFmDefected").ToString()=="1")?"defect":"" %>'>
                            <td style="text-align:left">
                                <div >
                                    <%#Eval("FlowMeterName")%>
                                </div>
                            </td> 
                            <td style="text-align:left">
                                <%#(Eval("ReadingMode").ToString()=="C")?"Cubic Mtr.":"Ltr."%>                                    
                            </td>
                                <td style="text-align:center;"><%#Eval("FuelTypeName")%></td>
                            <td style="text-align:right;">
                                <%#Eval("DensityAt15Deg")%>
                            </td>
                            <td style="text-align:right;">
                                    <%#Eval("Start")%>
                            </td>
                            <td style="text-align:right;">
                                    <%#Eval("End")%>
                            </td>
                            <td style="text-align:right;">
                                    <%#Eval("FlowMeterTemp")%>
                            </td>
                            <td style="text-align:right;">
                                    <%#Eval("DensityCorrFactor")%>
                            </td>
                            <td style="text-align:right;"><%#Eval("FuelPassed")%></td>
                            <td style="text-align:left;"> <%#Eval("DefectNo")%> </td>
                        </tr>
                        </ItemTemplate>
                        </asp:Repeater>
                        </table>
                        </div>
                    </div>
                 <%----------------------------------------------------------------------------------------------------------------------------%>
                    <div runat="server" id="DivFuelConsumption_fm" visible="false">
                        <h1 style="background-color:#9bd0f7">Fuel Consumption</h1>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="padded bordered">
                            <tr>
                                <th style="width:50px">SR#</th>
                                <th style="text-align:left">Fuel Grade</th>
                                <th style="width:150px">Consumption (MT)</th>
                                <th style="width:150px">Co2 Emission (Tonne)</th>
                            </tr>
                            <asp:Repeater ID="rptFuelConsumption_FM" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Sr") %></td>
                                        <td style="text-align:left"><%#Eval("FuelTypeName") %></td>
                                        <td style="text-align:right"><%#Eval("FuelConsumed") %></td>
                                        <td style="text-align:right"><%#Eval("Co2Emission") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                <%----------------------------------------------------------------------------------------------------------------------------%>
                <div runat="server" id="divFuelTankReadingReport" visible="false">
                    <h1 style="background-color:#fad6a5">Fuel Tank Reading ( As per flow meter readings )</h1>                            
                     <table cellpadding="0" cellspacing="0" border="0" class="bordered padded hightlight" width="100%">
                            <tr>
                                <th style="text-align:left">Tank Name</th>
                                <th style="text-align:left">Fuel Grade</th>
                                <th style="text-align:center">Sounding</th>
                                <th style="text-align:center">Ullage</th>
                                <th style="text-align:center">LevelGauge</th>
                                <th style="text-align:center">Volume</th>
                                <th style="text-align:center">Temp ( <sup>0</sup> C )</th>
                                <th style="text-align:center">S.G.</th>
                                <th style="text-align:center">Correct S.G.</th>
                                <th style="text-align:center">Last ROB( MT )</th>          
                                <th style="text-align:center">Current ROB( MT )</th>                                
                            </tr>
                            <asp:Repeater runat="server" ID="rptFuleTankReadingReport">
                            <ItemTemplate>
                            <tr>
                                <td style="text-align:left">
                                    <asp:Label ID="lblFuelTankName" runat="server" Text='<%#Eval("FuelTankName")%>'></asp:Label>
                                    <asp:HiddenField ID="hfdFuelTankId" runat="server" Value='<%#Eval("FuelTankId") %>' />
                                </td> 
                                <td style="text-align:left">
                                    <asp:Label ID="lblFuelTypeName" runat="server" Text='<%#Eval("FuelTypeName")%>'></asp:Label>
                                    <asp:HiddenField ID="hfdFuelTypeID" runat="server" Value='<%#Eval("FuelTypeId") %>' />
                                </td>
                                <td style="text-align:center;"><asp:Label ID="txtSounding" runat="server" Text='<%#Eval("Sounding") %>'></asp:Label></td>
                                <td style="text-align:center;"><asp:Label ID="txtUllage" runat="server" Text='<%#Eval("Ullage") %>'></asp:Label></td>
                                <td style="text-align:center;"><asp:Label ID="txtLevelGauge" runat="server" Text='<%#Eval("LevelGauge") %>'></asp:Label></td>
                                <td style="text-align:center;"><asp:Label ID="txtVolume" runat="server" Text='<%#Eval("Volume") %>'></asp:Label></td>
                                <td style="text-align:center;"><asp:Label ID="txtTemp" runat="server" Text='<%#Eval("Temp") %>'></asp:Label></td>
                                <td style="text-align:center;"><asp:Label ID="txtSG" runat="server" Text='<%#Eval("SG") %>'></asp:Label></td>
                                <td style="text-align:center;"><asp:Label ID="txtCorrectSG" runat="server" Text='<%#Eval("CorrectSG") %>'></asp:Label></td>
                                <td style="text-align:right;"><asp:Label ID="lblStartMT" runat="server" Text='<%#Eval("StartMT")%>'></asp:Label></td>
                                <td style="text-align:right;"><asp:Label ID="txtEndMT" runat="server" Text='<%#Eval("ENDMT")%>'></asp:Label></td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                            </table>
                </div>
                    <%----------------------------------------------------------------------------------------------------------------------------%>
                    <div runat="server" id="DivFuelConsumption_Tanks" visible="false">
                        <h1 style="background-color:#fad6a5">Fuel Consumption ( As per fuel tank readings )</h1>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="padded bordered">
                            <tr>
                                <th style="width:50px">SR#</th>
                                <th style="text-align:left">Fuel Grade</th>
                                <th style="width:150px">Consumption (MT)</th>
                                <th style="width:150px">Co2 Emission (Tonne)</th>
                            </tr>
                            <asp:Repeater ID="rptFuelConsumption_Tanks" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Sr") %></td>
                                        <td style="text-align:left"><%#Eval("FuelTypeName") %></td>
                                        <td style="text-align:right"><%#Eval("FuelConsumed") %></td>
                                        <td style="text-align:right"><%#Eval("Co2Emission") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                <%----------------------------------------------------------------------------------------------------------------------------%>
                    <div runat="server" id="divBunkeringReport" visible="false">
                        <h1>Bunkering</h1>   
                        <table cellpadding="0" cellspacing="0" border="0" class="padded bordered hightlight" width="100%" style="text-align:right;">
                            <tr>
                                <th style="text-align:left">Tank Name</th>
                                <th style="text-align:left">Fuel Grade</th>
                                <th >Density At 15<sup>0</sup> C</th>
                                <th >Last ROB( MT )</th>
                                <th >Received ( MT )</th>
                                <th >Current ROB( MT )</th>
                            </tr>
                            <asp:Repeater runat="server" ID="rptBunkeringReport">
                            <ItemTemplate>
                            <tr>
                                <td style="text-align:left">
                                    <%#Eval("FuelTankName")%>
                                    <asp:HiddenField ID="hfdFuelTankId" runat="server" Value='<%#Eval("FuelTankId") %>' />
                                </td> 
                                <td style="text-align:left">
                                    <%#Eval("FuelTypeName")%>
                                </td>
                                <td >
                                    <asp:Label ID="txtDensity" runat="server"  Text='<%#Eval("DensityAt15Deg") %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="txtLastROB" runat="server"  Text='<%#Eval("STARTMT") %>'></asp:Label>
                                </td>
                                <td >
                                    <asp:Label ID="txtReceived" runat="server"   Text='<%#Eval("Received") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCurrentROB" runat="server" Text='<%#Eval("ENDMT") %>'></asp:Label>
                                </td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                            </table>
                    </div>
               
                <%----------------------------------------------------------------------------------------------------------------------------%>
                    <div id="divActivityClosureDetails" runat="server">
                        <h1>Activity Closure Details</h1>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="padded bordered" style="text-align:left;">
                            <col width="150px" />
                            <col width="300px" />
                            <col width="150px" />
                            <col />
                            <tr>
                                <td>End Time</td>
                                <td >: <asp:Label ID="lblEndtime" runat="server"></asp:Label> <b> <i> ( UTC Time )</i></b></td>
                                <td>Steaming HRS/Min</td>
                                <td>: 
                                    <asp:Label ID="lblSteamingHRS" runat="server"></asp:Label> <b> <i> Hrs </i></b>
                                    <asp:Label ID="lblSteamingMin" runat="server"></asp:Label> <b> <i> Min</i></b>

                                </td>
                            </tr>
                          
                              <tr>
                                <td>Distance Made Good</td>
                                <td>: <asp:Label ID="lblDistanceMadeGood" runat="server"></asp:Label> <b> <i> ( NM )</i></b></td>
                                <td>Cargo Operation</td>
                                <td>: <asp:Label ID="lblCargoOperation" runat="server"></asp:Label> </td>
                            </tr>
                              <tr id="trTransportWork" runat="server">
                                <td>Transport Work </td>
                                <td>: <asp:Label ID="lblTransportWork" runat="server"></asp:Label> <b> <i> ( MT.NM )</i></b></td>
                                <td></td>
                                <td></td>
                            </tr>
                            
                             
                        </table>
                    </div>
                <%----------------------------------------------------------------------------------------------------------------------------%>
                    
                    </div>               
            </div>
            <div style="margin:5px;">                            
                <asp:Button runat="server" id="btnClose" CssClass="btn" Text="Close" OnClick="btnClose_Click" CausesValidation="false" />
            </div>
             <div style="border-bottom:solid 1px #0094ff">&nbsp;</div>           
            
        </div>
    
    <div class="message" >
        <asp:Label runat="server" ID="lblMsg" CssClass="modal_error"></asp:Label>                
    </div>
    </form>
</body>
</html>
                                        
