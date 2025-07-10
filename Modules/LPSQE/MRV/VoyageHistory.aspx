<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoyageHistory.aspx.cs" Inherits="MRV_VoyageHistory" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="mrvmenu.ascx" tagname="mrvmenu" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Crew Member Details</title>
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
    <style type="text/css">
        .OpenVoyage 
        {
            font-size:11px;font-weight:bold;text-align:left;
        }
        .OpenVoyage span
        {
            font-size:12px;font-weight:normal;
        }
    </style>      
    <script type="text/javascript">
        function closeWindow() {
            try {
                window.close();
            }
            catch (err) {

            }
        }
    </script>
</head>
<body style=" margin: 0 0 0 0;" >
<form id="form1" runat="server">
    <div class="modal" runat="server" id="dvModal" visible="false"></div>
    <div style="text-align: center">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div style="border-bottom:solid 5px #4371a5;"></div>
            <h1 style="text-align:center" >
                <span style="margin-left:10px;"> Voyage Details </span> 
                            
            </h1>
            <div class="OpenVoyage" >
                <table cellpadding="5" cellspacing="5" border="0" style="margin:0px auto;width:100%" class="padded bordered">
                    <col width="150px" />
                    <col width="10px" />
                    <col width="300px" />
                    <col width="180px" />
                    <col width="10px" />
                    <col width="300px" />
                    <col width="150px" />
                    <col width="10px" />
                    <col/>
                    <tr>
                        <td style="text-align:left;">Voyage No</td>
                        <td> : </td>
                        <td><asp:Label ID="lblVoyageNo" runat="server"></asp:Label></td>
                        <td style="text-align:left;">Voyage Start Date</td>
                        <td> : </td>
                        <td><asp:Label ID="lblStartDate" runat="server"></asp:Label>
                            <b><i> ( UTC Time )</i></b>
                        </td>
                         <td style="text-align:left;">Voyage Condition</td>
                         <td> : </td>
                        <td><asp:Label ID="lblCondition" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">From - To Port</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblFromPort" runat="server"></asp:Label>
                            -
                            <asp:Label ID="lblToPort" runat="server"></asp:Label>
                        </td>
                        <td style="text-align:left;"> Distance To Go</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblDistanceToGo" runat="server"></asp:Label> <b><i> ( NM )</i></b>
                        </td>
                        <td style="text-align:left;">Total CO<sub>2</sub> Emission</td>
                        <td> : </td>
                        <td>
                            <asp:Label ID="lblTotalCO2" runat="server"></asp:Label><b><i> ( Tonne )</i></b>
                            
                        </td>
                    </tr>
                </table>
                
                </div>    
            <br /> 
        <div>
            <div style="padding:5px;">
                <asp:Label runat="server" ID="lblErrorMessage" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
            </div>
              <table cellpadding="0" cellspacing="1" border="" class="bordered" width="100%">
                  
                  <tr>
                      <td style="width:50%"><h1>Report Per Voyage</h1></td>
                      <td><h1>With In Port</h1></td>
                  </tr>
                  <tr>
                      <td>
                          <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;">
                              <col />
                                <col width="150px;" />

                              <tr>
                                  <td>Time At Sea </td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblTimeAtSea" runat="server"></asp:Label></b>
                                  </td>
                              </tr>
                              <tr>
                                  <td>Time At Anchorage </td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblTimeAtAnchorage" runat="server"></asp:Label>
                                  </td>
                              </tr>
                              <tr>
                                  <td>Distance Travelled</td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblDistanceTravelled" runat="server"></asp:Label></b> <b><i> ( NM )</i></b> 
                                  </td>
                              </tr>
                               <tr>
                                  <td>Cargo Carried </td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblCargoCarried" runat="server"></asp:Label></b> <b><i> ( MT )</i></b> 
                                  </td>
                              </tr>
                               <tr>
                                  <td>Total Transport Work </td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblTotalTransportWork" runat="server"></asp:Label></b>
                                  </td>
                              </tr>
                               
                              </table>
                            <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;">
                              <col />
                                <col width="120px;" />
                                <col width="120px;" />
                                <col width="120px;" />
                                <col width="120px;" />
                                <tr style="background-color:#a0d9fa">
                                   <td><b>Fuel Grade</b></td>
                                       <td style="text-align:center;" colspan="2"><b>Flow Meter Method</b></td>
                                      <td style="text-align:center;" colspan="2"><b>Tank Reading Method</b></td>
                                  </tr>
                                  <tr style="background-color:#a0d9fa">
                                      <td></td>
                                       <td style="text-align:right;">Fuel Consumption</td>
                                      <td style="text-align:right;" >CO<sub>2</sub> Emission</td>
                                       <td style="text-align:right;">Fuel Consumption</td>
                                      <td style="text-align:right;" >CO<sub>2</sub> Emission</td>
                                  </tr>
                                 <asp:Repeater runat="server" ID="rptFuelConsVoyage">
                                 <ItemTemplate>
                                  <tr>
                                    <td><%#Eval("ShortName")%></td>
                                    <td style="text-align:right;"><%#Eval("F_FuelConsumed")%></td>
                                    <td style="text-align:right;"><%#Eval("F_Co2Emission")%></td>
                                      <td style="text-align:right;"><%#Eval("T_FuelConsumed")%></td>
                                    <td style="text-align:right;"><%#Eval("T_Co2Emission")%></td>
                                  </tr>
                                 </ItemTemplate>
                                 </asp:Repeater>
                                </table>
                      </td>
                      <td valign="top">
                          <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;">
                              <col />
                                <col width="150px;" />
                              <tr>
                                  <td>CO<sub>2</sub> Emission</td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblCo2EmissionInPort" runat="server"></asp:Label></b> 
                                  </td>
                              </tr>
                               <tr>
                                  <td>Fuel Consumption </td>
                                  <td style="text-align:right;">
                                      <b><asp:Label ID="lblFuelConsumptionInPort" runat="server"></asp:Label></b> 
                                  </td>
                              </tr>
                            </table>
                          <table cellpadding="0" cellspacing="0" border="0" class="bordered padded" width="100%" style="text-align:left;">
                              <col />
                                <col width="120px;" />
                                <col width="120px;" />
                                <col width="120px;" />
                                <col width="120px;" />
                                <tr style="background-color:#a0d9fa">
                                   <td><b>Fuel Grade</b></td>
                                       <td style="text-align:center;" colspan="2"><b>Flow Meter Method</b></td>
                                      <td style="text-align:center;" colspan="2"><b>Tank Reading Method</b></td>
                                  </tr>
                                  <tr style="background-color:#a0d9fa">
                                      <td></td>
                                       <td style="text-align:right;">Fuel Consumption</td>
                                      <td style="text-align:right;" >CO<sub>2</sub> Emission</td>
                                       <td style="text-align:right;">Fuel Consumption</td>
                                      <td style="text-align:right;" >CO<sub>2</sub> Emission</td>
                                  </tr>
                                 <asp:Repeater runat="server" ID="rptFuelConsPort">
                                 <ItemTemplate>
                                  <tr>
                                    <td><%#Eval("ShortName")%></td>
                                    <td style="text-align:right;"><%#Eval("F_FuelConsumed")%></td>
                                    <td style="text-align:right;"><%#Eval("F_Co2Emission")%></td>
                                      <td style="text-align:right;"><%#Eval("F_FuelConsumed")%></td>
                                    <td style="text-align:right;"><%#Eval("T_Co2Emission")%></td>
                                  </tr>
                                 </ItemTemplate>
                                 </asp:Repeater>
                                </table>
                      </td>
                  </tr>
              </table>
            
        </div>
         <div style="margin:5px;">                
                <asp:Button runat="server" id="btnClose" CssClass="btn" Text="Close" OnClick="btnClose_Click" CausesValidation="false" />
         </div>             
            <div>
                <ul class="menu">
                    <li><a href="#"> Voyage Activity </a></li>                    
                </ul>
                <div style="border-bottom:solid 5px #4371a5;"></div>
            </div>  
            <div>
                <table cellpadding="0" cellspacing="0" border="0" class="bordered hightlight padded" width="100%">
                                 <tr>
                                        <th style="width:50px;">View</th>
                                        <th style="text-align:left">&nbsp;Activity Name</th>
                                        
                                        <th style="text-align:left;width:130px;text-align:center;">Start Time</th>
                                        <th style="text-align:left;width:130px;text-align:center;">End Time</th>
                                        <th style="text-align:left;width:110px;">Steaming Time </th>
                                        <th style="text-align:center;width:140px;">Distance Made Good</th>
                                        <th style="text-align:center;width:140px;">Cargo Weight</th>
                                        <th style="text-align:center;width:140px;">Transport Work</th>
                                        <th style="text-align:center;width:140px;">Fuel Consumed</th>
                                        <th style="text-align:center;width:140px;">CO<sub>2</sub> Emission</th>
                                 </tr>                 
                            <asp:Repeater runat="server" ID="rptActivity">
                   
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align:center">
                                            <a target="_blank" href="NewActivity.aspx?TableId=<%#Eval("TableID") +"&VoyageId="+ Eval("VoyageId")+"&VesselCode="+ Eval("VesselCode") %>"> <img src="../Images/search_magnifier_12.png" alt="" style='margin:0px auto;<%#((Eval("Verified").ToString()=="-")?"display:none":"display:block")%>'/></a>
                                        </td>
                                        <td style="text-align:left">
                                            &nbsp;<span class='<%#((Eval("Verified").ToString()=="-")?"pending":"")%>' > <%#Eval("ActivityName")%> </span> 
                                        </td>                                        
                                        <td style="text-align:center"><%#FormatDate(Eval("StartTime"))%></td>
                                        <td style="text-align:center"><%#FormatDate(Eval("EndTime"))%></td>
                                        <td style="text-align:right">
                                            <%#Eval("SteamingHrs").ToString().PadLeft(2,'0')%>:<%#Eval("SteamingMin").ToString().PadLeft(2,'0')%> ( HRS )                                        
                                        </td>
                                        <td style="text-align:right"><%#Eval("DistanceMadeGoods")%> NM</td>
                                        <td style="text-align:right"><%#Eval("CurrentCargoWeight")%> MT</td>
                                        <td style="text-align:right"><%#Eval("TransportWork")%> NM.MT</td>
                                        <td style="text-align:right">NA</td>
                                        <td style="text-align:right"><%#Eval("CO2")%> Tonne</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </table>            
            </div>
        </div>   
     
    <div class="message">
        <asp:Label runat="server" ID="lblMsg" CssClass="modal_error"></asp:Label>                
    </div>
    </form>
    
</body>
</html>
                                        
