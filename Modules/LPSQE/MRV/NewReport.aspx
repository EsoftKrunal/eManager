<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewReport.aspx.cs" Inherits="MRV_New_Report" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Report</title>
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
                Daily Report ( Since Last Report )
            </div>
            <div style="background-color:#ebedef" >
                <div class="padding-1" >
             <table cellpadding="0" cellspacing="0" border="0" width="100%" class="padded">
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
                            <td style="text-align:left">Report Date : </td>
                            <td style="text-align:left">
                                <asp:Label runat="server" ID="txtReportDate"  Width="90px"  ></asp:Label> 
                                
                            </td>
                            <td style="text-align:left">ETA To Port : </td>
                            <td style="text-align:left">
                                <asp:Label runat="server" ID="txtETAToPortDate"   ></asp:Label> 
                                
                                <asp:Label runat="server" ID="txtETAToPortTime"  style="margin-left:5px;" ></asp:Label> 
                                
                                <b> <i> ( UTC Time )</i></b>
                            </td>
                          <td></td>
                     <td></td>
                    </tr>  
                 <tr>
                     <td style="text-align:left">Location :</td>
                     <td style="text-align:left">
                         <asp:Label runat="server" ID="lblLocation"   ></asp:Label> 
                          
                              
                     </td>
                     <td style="text-align:left">Steaming HRS : </td>
                            <td style="text-align:left">
                                <asp:Label runat="server" ID="lblSteamingHours"   ></asp:Label> <b> <i> ( Hrs )</i></b>
                                <asp:Label runat="server" ID="lblSteamingMin"  style="margin-left:5px;"   ></asp:Label> 
                             
                            <b> <i> ( Min )</i></b>
                            </td>
                     <td style="text-align:left">Distance Made Goods : </td>
                            <td style="text-align:left">
                                <asp:Label ID="txtDMG"  runat="server" MaxLength="16" style="text-align:right;width:70px"   ></asp:Label>
                                <b> <i> NM</i></b>


                            </td>
                 </tr>

                  <tr>
                     <td style="text-align:left">Draft(Fwd) :</td>
                     <td style="text-align:left">
                          <asp:Label ID="txtDraftFwd" onkeypress="FloatValueOnly(event)" runat="server" MaxLength="16" style="text-align:right;width:70px"  ></asp:Label>
                         <b> <i> MTRS</i></b>
                     </td>
                     <td style="text-align:left">Draft (Aft) :</td>
                     <td style="text-align:left">
                         <asp:Label ID="txtDraftAfter" onkeypress="FloatValueOnly(event)" runat="server" MaxLength="16" style="text-align:right;width:70px"  ></asp:Label>
                                 
                                 <b> <i> MTRS</i></b>
                     </td>
                     <td style="text-align:left">Draft (Mid) :</td>
                     <td style="text-align:left">
                         <asp:Label ID="txtDraftMid" onkeypress="FloatValueOnly(event)" runat="server" MaxLength="16" style="text-align:right;width:70px"  ></asp:Label>                                 
                         <b> <i> MTRS</i></b>
                     </td>
                 </tr>

                 <tr>
                     <td style="text-align:left">Master Name :</td>
                     <td style="text-align:left">
                         <asp:Label runat="server" ID="txtMaterName"  ></asp:Label>
                         
                                  
                     </td>
                     <td style="text-align:left">Chief Engineer Name :</td>
                     <td style="text-align:left">
                         <asp:Label runat="server" ID="txtChiefEngineerName" ></asp:Label> 
                                 
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
         <div style="margin:5px;">
             <input type="button" class="btn" value="Close" onclick='window.close();'/>

            </div>
       
            <div runat="server" id="dvReadings" visible="false">
                <table cellpadding="0" cellspacing="0" border="0" class="" width="100%">
                    <tr>                     
                        <td style="vertical-align:top">
                            <h1>
                                <span style="margin-left:10px;">Flow Meter Readings</span> 
                            </h1>
                            <asp:Repeater runat="server" ID="rptReadings">
                                <ItemTemplate>

                            <div style="margin:10px;">
                            <div style="font-size:17px; background-color:NONE;color:#ff6a00;padding:5px;font-weight:bold;text-align:left;position:relative;">
                               <span ><%#FormatDate(Eval("ChangeTime"))%></span>  ( UTC Time )
                                
                            </div>
                            <div style="border:none;border-top:none;">                            
                             <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight" width="100%">
                            <tr>
                                <th style="text-align:left">Flow Meter Name</th>
                                <th style="text-align:left">Flow Unit</th>
                                <th style="text-align:center">Fuel Type</th>
                                <th style="text-align:right">Density At 15<sup>0</sup> C </th>
                                <th style="text-align:right">Last Reading</th>
                                <th style="text-align:right">Current Reading</th>
                                <th style="text-align:right">Flow Meter Temp ( <sup>0</sup> C )</th> 
                                <th style="text-align:right" title="Formula=(Density At 15 Deg.C - 0.0011 )*(1-(0.00064*(Flow Meter Temp Deg.C - 15 ))))">Density Weight Correction Factor</th>
                                <th style="text-align:right">Fuel Passed ( MT )</th>         
                            </tr>
                            <asp:Repeater runat="server" ID="rptDetails" DataSource='<%#GetFuelChangeSection(Eval("ChangeId"))%>'>
                            <ItemTemplate>
                            <tr>
                                <td style="text-align:left"><%#Eval("FlowMeterName")%></td> 
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
                                <td style="text-align:right;">
                                     <%#Eval("FuelPassed")%>
                                </td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                            </table>
                            </div>
                            </div>
                            
                                </ItemTemplate>
                            </asp:Repeater>
                            
                             
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top:5px;">
                            <h1> <span style="margin-left:10px;">Fuel Consmption Summary</span>  </h1>
                            <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight" width="100%">
                            <tr>
                                
                                <th style="text-align:left;">Fuel Type</th>
                                <th style="text-align:right;">Consumption ( M<sup>3</sup> )</th>
                                <th style="text-align:right;">CO2</th>
                                 
                            </tr>
                            <asp:Repeater runat="server" ID="rptFinalConsumption">
                            <ItemTemplate>
                            <tr>
                                
                                <td style="text-align:left;"><%#Eval("FuelTypeName")%></td>
                                <td style="text-align:right;"><%#Eval("FuelConsumed")%></td>
                                <td style="text-align:right;"><%#Eval("CO2")%></td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                            </table>  
                        </td>
                    </tr>
                    </table>
            </div>
         <div>
            <img src="../Images/formula.png" />
        </div>
             <div style="border-bottom:solid 1px #0094ff">&nbsp;</div>           
            
        </div>
    
 
    </form>
 
</body>
</html>
                                        
